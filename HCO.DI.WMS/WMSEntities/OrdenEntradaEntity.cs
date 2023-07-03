using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCO.DI.WMSEntities
{
    public class OrdenEntradaEntity
    {
        public string codOwner { get; set; }
        public string codDeposito { get; set; }
        public string nroOrdenEntrada { get; set; }
        public OrdenEntrada ordenEntrada { get; set; }
    }
}
