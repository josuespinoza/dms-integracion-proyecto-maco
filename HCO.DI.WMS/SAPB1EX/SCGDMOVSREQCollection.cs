using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCO.DI.SAPB1EX
{
    public class SCGDMOVSREQCollection
    {
        public int? DocEntry { get; set; }
        public int? LineId { get; set; }
        public int? VisOrder { get; set; }
        public string Object { get; set; }
        public object LogInst { get; set; }
        public string U_SCGD_DescArticulo { get; set; }
        public double? U_SCGD_CantTransf { get; set; }
        public string U_SCGD_TipoDoc { get; set; }
        public DateTime? U_SCGD_FechaDoc { get; set; }
        public string U_SCGD_CodArticulo { get; set; }
        public int? U_SCGD_DocEntry { get; set; }
        public int? U_SCGD_DocNum { get; set; }
    }
}
