using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCO.DI.WMSEntities
{
    public class OrdenSalida
    {
        public string codOwner { get; set; }
        public string codDeposito { get; set; }
        public string nroOrdenSalida { get; set; }
        public string nroOrdenCliente { get; set; }
        public int? tipoOrdenSalida { get; set; }
        public string nroReferencia { get; set; }
        public string nroReferencia2 { get; set; }
        public string nroReferenciaMKP { get; set; }
        public int? codMoneda { get; set; }
        public string codCliente { get; set; }
        public string codSucursal { get; set; }
        public DateTime? fechaEmisionERP { get; set; }
        public DateTime? fechaCompromiso { get; set; }
        public string observacion { get; set; }
        public string prioridad { get; set; }
        public string packingList { get; set; }
        public string esCrossDocking { get; set; }
        public string nroCrossDocking { get; set; }
        public string origen { get; set; }
        public string codDepositoDespacho { get; set; }
        public string codDepositoDespacho2 { get; set; }
        public List<OrdenSalidaDetalle> ordenSalidaDetalle { get; set; }
        public OrdenDespacho ordenDespacho { get; set; }
    }
}
