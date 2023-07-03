using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCO.DI.WMSEntities;
using HCO.DI.SAPB1EX;
using RestSharp;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net;
using HCO.DI.Entities;
using HCO.SB1ServiceLayerSDK.SAPB1;

namespace HCO.DI
{
    public class WMSClientAPI
    {
        private string _token;
        private string _url;

        public  WMSClientAPI (string url, string token)
        {
            _url = url;
            _token = token;
        }

        public WMSAPIResponse CreateOrdenSalida(SCGD_REQ oRequisicion, ref Settings.ScenarioRow oScenarioSettings, ref Settings.InterfaceRow oInterfaceSettings,ref List<HCO_DMSWMS> oBodegasList)
        {
            WMSAPIResponse response = new WMSAPIResponse();
            OrdenSalidaEntity oOrdenSalidaEntity = new OrdenSalidaEntity();
            OrdenSalida oOrdenSalida = new OrdenSalida();
            OrdenSalidaDetalle oDetalle;
            List<SCGDLINEASREQCollection> oLineasReq = new List<SCGDLINEASREQCollection>();
            try
            {
                oLineasReq = oRequisicion.SCGD_LINEAS_REQCollection;
                // Orden Salida Root
                oOrdenSalidaEntity.codOwner = oScenarioSettings.Param1;
                oOrdenSalidaEntity.codDeposito = oBodegasList.Find(x => x.U_BodSAP == oLineasReq.First().U_SCGD_CodBodOrigen).U_BodWMS;
                oOrdenSalidaEntity.nroOrdenSalida =  oRequisicion.DocEntry.ToString();
                //Orden Salida
                oOrdenSalida.codOwner= oScenarioSettings.Param1;
                oOrdenSalida.codDeposito = oBodegasList.Find(x => x.U_BodSAP == oLineasReq.First().U_SCGD_CodBodOrigen).U_BodWMS;
                oOrdenSalida.nroOrdenSalida =  oRequisicion.DocEntry.ToString();
                oOrdenSalida.tipoOrdenSalida = 2;
                oOrdenSalida.codCliente = oRequisicion.U_SCGD_CodCliente;//"C79740840"
                oOrdenSalida.codMoneda = 1;
                oOrdenSalida.fechaEmisionERP= oRequisicion.CreateDate;
                oOrdenSalida.fechaCompromiso = oRequisicion.CreateDate; 
                oOrdenSalida.nroReferencia= oRequisicion.DocEntry.ToString();

                oOrdenSalida.ordenSalidaDetalle = new List<OrdenSalidaDetalle>();
                foreach (SCGDLINEASREQCollection linea in oRequisicion.SCGD_LINEAS_REQCollection)
                {
                    oDetalle = new OrdenSalidaDetalle();
                    oDetalle.codOwner= oScenarioSettings.Param1;
                    oDetalle.codDeposito = oBodegasList.Find(x => x.U_BodSAP == linea.U_SCGD_CodBodOrigen).U_BodWMS;
                    oDetalle.nroOrdenSalida=  oRequisicion.DocEntry.ToString();
                    oDetalle.nroLinea = linea.LineId.ToString();
                    oDetalle.nroLote =  linea.U_SCGD_ID;
                    oDetalle.codItem =  linea.U_SCGD_CodArticulo;// "C4899226"
                    oDetalle.cantidad = linea.U_SCGD_CantSol;
                    oDetalle.inventariable = "S";
                    oOrdenSalida.ordenSalidaDetalle.Add(oDetalle);
                }
                oOrdenSalidaEntity.ordenSalida = oOrdenSalida;

                response=PostOrdenSalida(ref oOrdenSalidaEntity);
            }
            catch (Exception ex)
            {
                response = new WMSAPIResponse
                {
                    status = 400,
                    title = ex.Message 
                };
            }
    
            return response;
        }

        public WMSAPIResponse PostOrdenSalida(ref OrdenSalidaEntity oOrdenSalidaEntity)
        {
            WMSAPIResponse responseMessage = new WMSAPIResponse();
            RestClient client;
            RestRequest request;
            IRestResponse response;
            JObject resp;
            string jsonString = string.Empty;
            try
            {
                jsonString = JsonConvert.SerializeObject(oOrdenSalidaEntity);

                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                client = new RestClient(_url+"/WMS_Outbound/CreateOrdenSalida");
                request = new RestRequest(Method.POST);
                request.AddHeader("dataAuth", _token);
                request.AddJsonBody(jsonString);

                response = client.Execute(request);
                resp = JObject.Parse(response.Content);

                responseMessage.type = resp.GetValue("type").ToString();
                responseMessage.status = Convert.ToInt32(resp.GetValue("status"));
                responseMessage.title = resp.GetValue("title").ToString();
                responseMessage.url = resp.GetValue("url").ToString();

                return responseMessage;
            }
            catch (Exception ex)
            {
                responseMessage.type = string.Empty;
                responseMessage.status = 400;
                responseMessage.title = "Internal error";
                responseMessage.url = string.Empty;

                return responseMessage;
            }
        }

        public WMSAPIResponse CreateOrdenEntrada(SCGD_REQ oRequisicion, ref Settings.ScenarioRow oScenarioSettings, ref Settings.InterfaceRow oInterfaceSettings, ref List<HCO_DMSWMS> oBodegasList)
        {
            WMSAPIResponse response = new WMSAPIResponse();
            OrdenEntradaEntity oOrdenEntradaEntity = new OrdenEntradaEntity();
            OrdenEntrada oOrdenEntrada = new OrdenEntrada();
            OrdenEntradaDetalle oDetalle;
            List<SCGDLINEASREQCollection> oLineasReq = new List<SCGDLINEASREQCollection>();
            try
            {
                oLineasReq = oRequisicion.SCGD_LINEAS_REQCollection;
                // Orden Entrada Root
                oOrdenEntradaEntity.codOwner = oScenarioSettings.Param1;
                oOrdenEntradaEntity.codDeposito = oBodegasList.Find(x => x.U_BodSAP == oLineasReq.First().U_SCGD_CodBodOrigen).U_BodWMS;
                oOrdenEntradaEntity.nroOrdenEntrada =  oRequisicion.DocEntry.ToString();
                //Orden Entrada
                oOrdenEntrada.codOwner = oScenarioSettings.Param1;
                oOrdenEntrada.codDeposito = oBodegasList.Find(x => x.U_BodSAP == oLineasReq.First().U_SCGD_CodBodOrigen).U_BodWMS;
                oOrdenEntrada.nroOrdenEntrada =  oRequisicion.DocEntry.ToString();
                oOrdenEntrada.codTipo = "4";
                oOrdenEntrada.tipoDeCambio = 0;
                oOrdenEntrada.codMoneda = "1";
                oOrdenEntrada.fechaEmisionERP = oRequisicion.CreateDate;
                oOrdenEntrada.fechaEstimadaRecepcion = oRequisicion.CreateDate;
                oOrdenEntrada.nroReferencia= oRequisicion.DocEntry.ToString();

                oOrdenEntrada.ordenEntradaDetalle = new List<OrdenEntradaDetalle>();
                foreach (SCGDLINEASREQCollection linea in oRequisicion.SCGD_LINEAS_REQCollection)
                {
                    oDetalle = new OrdenEntradaDetalle();
                    oDetalle.codOwner = oScenarioSettings.Param1;
                    oDetalle.codDeposito = oBodegasList.Find(x => x.U_BodSAP == linea.U_SCGD_CodBodOrigen).U_BodWMS;
                    oDetalle.nroOrdenEntrada =  oRequisicion.DocEntry.ToString();
                    oDetalle.nroLinea = linea.LineId.ToString();
                    oDetalle.codItem =linea.U_SCGD_CodArticulo;//"C4899226"
                    oDetalle.codMoneda = 1;
                    oDetalle.numeroLote = linea.U_SCGD_ID;
                    oDetalle.cantidadSolicitada = linea.U_SCGD_CantSol;
                    oOrdenEntrada.ordenEntradaDetalle.Add(oDetalle);
                }
                oOrdenEntradaEntity.ordenEntrada = oOrdenEntrada;

                response = PostOrdenEntrada(ref oOrdenEntradaEntity);
            }
            catch (Exception ex)
            {
                response = new WMSAPIResponse
                {
                    status = 400,
                    title = ex.Message 
                };
            }
            return response;
        }

        public WMSAPIResponse PostOrdenEntrada(ref OrdenEntradaEntity oOrdenEntradaEntity)
        {
            WMSAPIResponse responseMessage = new WMSAPIResponse();
            RestClient client;
            RestRequest request;
            IRestResponse response;
            JObject resp;
            string jsonString = string.Empty;
            try
            {
                jsonString = JsonConvert.SerializeObject(oOrdenEntradaEntity);

                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

                client = new RestClient(_url+ "/WMS_Inbound/CreateOrdenEntrada");
                request = new RestRequest(Method.POST);
                request.AddHeader("dataAuth", _token);
                request.AddJsonBody(jsonString);

                response = client.Execute(request);
                resp = JObject.Parse(response.Content);

                responseMessage.type = resp.GetValue("type").ToString();
                responseMessage.status = Convert.ToInt32(resp.GetValue("status"));
                responseMessage.title = resp.GetValue("title").ToString();
                responseMessage.url = resp.GetValue("url").ToString();
                responseMessage.json = jsonString;

                return responseMessage;
            }
            catch (Exception ex)
            {
                responseMessage.type = string.Empty; 
                responseMessage.status = 400;
                responseMessage.title = "Internal error";
                responseMessage.url = string.Empty;

                return responseMessage;
            }
        }
    }
}
