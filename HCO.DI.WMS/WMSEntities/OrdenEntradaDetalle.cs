using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCO.DI.WMSEntities
{
    public class OrdenEntradaDetalle
    {
        public string codOwner { get; set; }
        public string codDeposito { get; set; }
        public string nroOrdenEntrada { get; set; }
        public string nroLinea { get; set; }
        public string codItem { get; set; }
        public string numeroLote { get; set; }
        public int codMoneda { get; set; }
        public double cantidadSolicitada { get; set; }
    }
}
