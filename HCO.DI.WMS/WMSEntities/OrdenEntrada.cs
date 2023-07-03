using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCO.DI.WMSEntities
{
    public class OrdenEntrada
    {
        public string codOwner { get; set; }
        public string codDeposito { get; set; }
        public string nroOrdenEntrada { get; set; }
        public string nroReferencia { get; set; }
        public string nroOrdenCliente { get; set; }
        public string codTipo { get; set; }
        public string codProveedor { get; set; }
        public string codCliente { get; set; }
        public string codSucursal { get; set; }
        public double tipoDeCambio { get; set; }
        public DateTime? fechaEstimadaRecepcion { get; set; }
        public DateTime? fechaExpiracion { get; set; }
        public DateTime? fechaEmisionERP { get; set; }
        public string observacion { get; set; }
        public string origen { get; set; }
        public string codMoneda { get; set; }
        public List<OrdenEntradaDetalle> ordenEntradaDetalle { get; set; }
    }
}
