using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace HCO.DI.AgentService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            //Permite definir los nombres de las carpetas que se deben crear en la carpeta C:\ProgramData
            HCO.DI.Folders.CommonPath.CompanyFolder = "HCO Global Advisors";
            HCO.DI.Folders.CommonPath.ApplicationFolder = "HCO Project Integration Service";

            //Permite definir los nombres que utiliza el Servicio de Windows y el nombre del log de Windows
            HCO.DI.Common.Utility.SOURCE = "HCOIntegrationProject";
            HCO.DI.Common.Utility.LOG_NAME = "HCO Project Integration Service";

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new DIAgentService() 
			};
            ServiceBase.Run(ServicesToRun);
        }
    }
}
