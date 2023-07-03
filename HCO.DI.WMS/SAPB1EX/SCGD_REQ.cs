using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCO.DI.SAPB1EX
{
    public class SCGD_REQ
    {
        public int? DocNum { get; set; }
        public int? Period { get; set; }
        public int? Instance { get; set; }
        public int? Series { get; set; }
        public string Handwrtten { get; set; }
        public string Status { get; set; }
        public string RequestStatus { get; set; }
        public string Creator { get; set; }
        public object Remark { get; set; }
        public int? DocEntry { get; set; }
        public string Canceled { get; set; }
        public string Object { get; set; }
        public object LogInst { get; set; }
        public int? UserSign { get; set; }
        public string Transfered { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateTime { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateTime { get; set; }
        public string DataSource { get; set; }
        public string U_SCGD_NoOrden { get; set; }
        public string U_SCGD_CodCliente { get; set; }
        public string U_SCGD_NombCliente { get; set; }
        public int? U_SCGD_CodTipoReq { get; set; }
        public string U_SCGD_TipoReq { get; set; }
        public string U_SCGD_TipoDoc { get; set; }
        public string U_SCGD_Usuario { get; set; }
        public string U_SCGD_Comm { get; set; }
        public object U_SCGD_Data { get; set; }
        public string U_SCGD_Est { get; set; }
        public int? U_SCGD_CodEst { get; set; }
        public object U_SCGD_Entregado { get; set; }
        public string U_SCGD_IDSuc { get; set; }
        public object U_SCGD_Placa { get; set; }
        public object U_SCGD_Marca { get; set; }
        public object U_SCGD_Estilo { get; set; }
        public object U_SCGD_VIN { get; set; }
        public object U_SCGD_TipArt { get; set; }
        public object U_SCGD_Comen { get; set; }
        public int? U_Serie { get; set; }
        public object U_ActualizaDoc { get; set; }
        public string U_SCGD_EstWms { get; set; }
        public List<SCGDLINEASREQCollection> SCGD_LINEAS_REQCollection { get; set; }
        public List<SCGDMOVSREQCollection> SCGD_MOVS_REQCollection { get; set; }
        //public List<object> SCGD_SERIES_REQCollection { get; set; }
    }
}
