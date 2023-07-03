using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HCOIntegration
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Permite definir los nombres de las carpetas que se deben crear en la carpeta C:\ProgramData
            HCO.DI.Folders.CommonPath.CompanyFolder = "HCO Global Advisors";
            HCO.DI.Folders.CommonPath.ApplicationFolder = "HCO Integration Service";

            //Permite definir los nombres que utiliza el Servicio de Windows y el nombre del log de Windows
            HCO.DI.Common.Utility.SOURCE = "HCOIntegration";
            HCO.DI.Common.Utility.LOG_NAME = "HCO Integration Service";

            //Permite cambiar los nombres que se muestran a nivel de UI
            HCO.DI.AgentUI.ServiceManager.ApplicationName = "HCO Integration Service";
            HCO.DI.AgentUI.ServiceManager.ConsoleName = "Consola de Administración Integración Enfasys WMS con SAP Business One/DMS";

            //Permite activar el skin de SAP B1 10
            HCO.DI.AgentUI.ServiceManager.ActivateSkin = true;
            HCO.DI.AgentUI.ServiceManager.SkinFormOnly = true;

            //Permite ocultar la pestaña del visor de eventos de Windows a nivel de UI
            HCO.DI.AgentUI.ServiceManager.ShowEventViewer = false;

            //Permite definir  los iconos para mostrar el servicio en ejecución, detenido y neutral
            //HCO.DI.AgentUI.ServiceManager.Neutral = global::HCOIntegration.Properties.Resources.neutral;
            //HCO.DI.AgentUI.ServiceManager.Running = global::HCOIntegration.Properties.Resources.running;
            //HCO.DI.AgentUI.ServiceManager.Stopped = global::HCOIntegration.Properties.Resources.stopped;

            //
            //HCO.DI.AgentUI.ServiceManager.AppIcon = global::HCOIntegration.Properties.Resources.HCOMailer;

            //Permite definir la versión del servcio
            HCO.DI.AgentUI.ServiceManager.Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            //HCO.DI.AgentUI.ServiceManager.ScenarioParameter1label = "";
            //HCO.DI.AgentUI.ServiceManager.ScenarioParameter2label = "";
            //HCO.DI.AgentUI.ServiceManager.ScenarioParameter3label = "";
            //HCO.DI.AgentUI.ServiceManager.ScenarioParameter4label = "";
            //HCO.DI.AgentUI.ServiceManager.ScenarioParameter5label = "";

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new HCO.DI.AgentUI.ServiceManager());
        }
    }
}
