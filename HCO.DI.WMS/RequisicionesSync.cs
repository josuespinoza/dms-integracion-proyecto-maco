using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using HCO.DI.IntegrationFramework.Contracts;
using HCO.DI.Common;
using HCO.DI.Entities;
using HCO.DI.DA;
using HCO.SB1ServiceLayerSDK.SAPB1;
using Quartz;
using Quartz.Impl;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Reflection;
using System.Diagnostics;
using HCO.DI.SAPB1EX;
using HCO.DI.WMSEntities;

namespace HCO.DI
{
    [DisallowConcurrentExecution]

    public class RequisicionesSync : IJob
    {
        private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public Task Execute(IJobExecutionContext context)
        {
            JobKey key;
            JobDataMap dataMap;

            Settings.ScenarioRow scenarioParams = null;
            Settings.InterfaceRow interfaceParams = null;

            int scenarioId = 0;
            string scenarioName = string.Empty;
            string sourceName = string.Empty;
            string destinationName = string.Empty;
            int interfaceId = 0;
            string interfaceName = string.Empty;
            string refKey = string.Empty;
            string refValue = string.Empty;
            string request = string.Empty;
            string response = string.Empty;
            string message = string.Empty;
            bool writeInfoToLog = true;
            bool writeErroToLog = true;
            bool upsertLog = false;

            string interfaceParam1 = string.Empty;
            string scenarioParam1 = string.Empty;
            HCO.SB1ServiceLayerSDK.ServiceLayerClient slClient = null;
            Configuracion oConfiguracion = new Configuracion();
            string nextLink = null; 
            string queryOptions = string.Empty;
            try
            {
                key = context.JobDetail.Key;
                dataMap = context.JobDetail.JobDataMap;
  
                scenarioParams = (Settings.ScenarioRow)dataMap.Get("scenarioParams");
                interfaceParams = (Settings.InterfaceRow)dataMap.Get("interfaceParams");

                scenarioId = scenarioParams.Id;
                scenarioName = scenarioParams.ScenarioName;
                sourceName = scenarioParams.Sb1_Database;
                destinationName = scenarioParams.RestAPI_Description;
                scenarioParam1=scenarioParams.Param1;
                interfaceId = interfaceParams.Id;
                interfaceName = interfaceParams.Name;
                interfaceParam1 = interfaceParams.Param1;
                refKey = "SCGD_REQUISICIONES.DocNum";
                refValue = string.Empty;
                request = string.Empty;
                response = string.Empty;
                message = string.Empty;
                writeInfoToLog = scenarioParams.LogInfo;
                writeErroToLog = scenarioParams.LogError;
                
                if (scenarioParams.IsRestAPI_Endpoint1Null() || string.IsNullOrEmpty(scenarioParams.RestAPI_Endpoint1))
                    throw new Exception("El parámetro Endpoint1 (url del api del WMS) del escenario no se ha definido");

                if (scenarioParams.IsRestAPI_APIKeyNull() || string.IsNullOrEmpty(scenarioParams.RestAPI_APIKey))
                    throw new Exception("El parámetro APIKey (token del api del WMS) del escenario no se ha definido");

                string url = scenarioParams.RestAPI_Endpoint1;
                string token = Utility.Decrypt(scenarioParams.RestAPI_APIKey);

                slClient = new SB1ServiceLayerSDK.ServiceLayerClient(scenarioParams.Sb1_ServiceLayerEndpoint);
                slClient.Login(scenarioParams.Sb1_Database, scenarioParams.Sb1_User, Utility.Decrypt(scenarioParams.Sb1_Password));


                //Carga Mapeo Bodegas
                nextLink = null; //Se usa como parámetro para llevar el control del siguiente paginad
                queryOptions = string.Empty;

                List<HCO_DMSWMS> bodegasList = new List<HCO_DMSWMS>();
                do
                {
                    List<HCO_DMSWMS> bodegasResult = slClient.Get<List<HCO_DMSWMS>>("HCO_DMS_WMS", queryOptions, out nextLink, 20);
                    bodegasList.AddRange(bodegasResult);

                    queryOptions = nextLink;
                }
                while (nextLink != null);
                //Carga Requisiciones
                nextLink = null; //Se usa como parámetro para llevar el control del siguiente paginad
                queryOptions = String.Format("select=*&$filter=(U_SCGD_EstWms eq '{0}') and (U_SCGD_IDSuc eq '{1}')", "P", interfaceParam1);

                List<SCGD_REQ> requisicionesList = new List<SCGD_REQ>();
                do
                {
                    List<SCGD_REQ> requisicionesResult = slClient.Get<List<SCGD_REQ>>("SCGD_REQ", queryOptions, out nextLink, 20);
                    requisicionesList.AddRange(requisicionesResult);

                    queryOptions = nextLink;
                }
                while (nextLink != null);

                if (requisicionesList.Count > 0)
                {

                    WMSClientAPI wmsClientAPI = new WMSClientAPI(url, token);

                    foreach (SCGD_REQ requisicion in requisicionesList)
                    {
                        try
                        {
                            refValue = requisicion.DocNum.ToString();
                            WMSAPIResponse createOrdenResponse=new WMSAPIResponse();
                            createOrdenResponse.status = -1;
                            //Código de integración con el WMS
                            if (requisicion.U_SCGD_CodTipoReq == 1)
                            {
                                createOrdenResponse = wmsClientAPI.CreateOrdenSalida(requisicion, ref scenarioParams, ref interfaceParams, ref bodegasList);
                            }
                            else if(requisicion.U_SCGD_CodTipoReq == 2)
                            {
                                createOrdenResponse = wmsClientAPI.CreateOrdenEntrada(requisicion, ref scenarioParams, ref interfaceParams, ref bodegasList);
                            }
                            
                            if (createOrdenResponse.status != 200) //En caso de error
                            {
                                message = createOrdenResponse.title;
                            }
                            else //Todo OK
                            {
                                message = "Operación exitosa";

                                slClient.Update<SCGD_REQ>(requisicion.DocEntry, new SCGD_REQ { U_SCGD_EstWms = "S" }, "SCGD_REQ");

                            }

                            if (writeInfoToLog)
                                Utility.WriteToLog(scenarioId, scenarioName, sourceName, destinationName, interfaceId, interfaceName, LogDA.Status.Successful, refKey, refValue, LogDA.ContenTypes.Json, request, response, message, string.Empty, upsertLog);

                            //string nametxt = "Log" + DateTime.Now.ToString("yyyyMMdd HHmmss") + ".txt";
                            //System.IO.File.WriteAllText(@scenarioParams.Param2+ nametxt, createOrdenResponse.json + "**"+ message);
                        }
                        catch (Exception ex)
                        {
                            _logger.Error(ex, ex.Message);
                            message = ex.Message;

                            if (writeErroToLog)
                                Utility.WriteToLog(scenarioId, scenarioName, sourceName, destinationName, interfaceId, interfaceName, LogDA.Status.Failed, refKey, refValue, LogDA.ContenTypes.Json, request, response, message, string.Empty, upsertLog);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex, ex.Message);
                message = string.Format("Error Code: {0} - Message: {1}", ex.HResult, ex.Message);

                if (writeErroToLog)
                    Utility.WriteToLog(scenarioId, scenarioName, sourceName, destinationName, interfaceId, interfaceName, LogDA.Status.Failed, String.Empty, String.Empty, LogDA.ContenTypes.Other, String.Empty, String.Empty, message, string.Empty, upsertLog);
            }
            finally
            {
                if (slClient != null)
                    slClient.Logout();
            }

            return Task.CompletedTask;

        }
    }
}
