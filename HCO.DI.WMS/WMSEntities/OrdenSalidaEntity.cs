using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCO.DI.WMSEntities
{
    public class OrdenSalidaEntity
    {
        public string codOwner { get; set; }
        public string codDeposito { get; set; }
        public string nroOrdenSalida { get; set; }
        public OrdenSalida ordenSalida { get; set; }
    }
}
