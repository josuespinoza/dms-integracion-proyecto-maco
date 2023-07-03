using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCO.DI.WMSEntities
{
    public class OrdenDespacho
    {
        public string codOwner { get; set; }
        public string codDeposito { get; set; }
        public string nroOrdenSalida { get; set; }
        public string tipoDespacho { get; set; }
        public string tipoEntrega { get; set; }
        public string codCourier { get; set; }
        public string nomCourier { get; set; }
        public string contactoDespacho { get; set; }
        public string direccionDespacho { get; set; }
        public string telefonoDespacho { get; set; }
        public string correoDespacho { get; set; }
        public string comunaDespacho { get; set; }
        public string ciudadDespacho { get; set; }
        public string regionDespacho { get; set; }
        public string paisDespacho { get; set; }
        public string rutaDespacho { get; set; }
        public string ventanaDespacho { get; set; }
        public string horarioDespacho { get; set; }
        public string observacionDespacho { get; set; }
        public DateTime? fechaCompromisoDespacho { get; set; }
    }
}
