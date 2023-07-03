using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCO.DI.WMSEntities
{
    public class OrdenSalidaDetalle
    {
        public string codOwner { get; set; }
        public string codDeposito { get; set; }
        public string nroOrdenSalida { get; set; }
        public string nroLinea { get; set; } 
        public string pickID { get; set; }
        public string codItem { get; set; }
        public string nroLote { get; set; }
        public DateTime? fechaVencimiento { get; set; }
        public DateTime? fechaFabricacion { get; set; }
        public string nroSerie { get; set; }
        public double cantidad { get; set; }
        public int montoCosto { get; set; }
        public int montoNeto { get; set; }
        public int porcentajeIVA { get; set; }
        public int montoIVA { get; set; }
        public int porcentajeDescuento { get; set; }
        public int montoDescuento { get; set; }
        public int montoTotal { get; set; }
        public string inventariable { get; set; }
    }
}
