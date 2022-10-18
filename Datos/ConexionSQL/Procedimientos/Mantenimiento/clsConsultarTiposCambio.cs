using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarTiposCambio : clsProcedimientoAlmacenado
    {
        private string lstr_IdMoneda;
        public string Lstr_IdMoneda
        {
            get { return lstr_IdMoneda; }
            set { lstr_IdMoneda = value; }
        }


        private DateTime? ldt_FchReferencia;
        public DateTime? Ldt_FchReferencia
        {
            get { return ldt_FchReferencia; }
            set { ldt_FchReferencia = value; }
        }


        private string lstr_TipoTransaccion;
        public string Lstr_TipoTransaccion
        {
            get { return lstr_TipoTransaccion; }
            set { lstr_TipoTransaccion = value; }
        }

        private string lstr_ExactaFecha;
        public string Lstr_ExactaFecha
        {
            get { return lstr_ExactaFecha; }
            set { lstr_ExactaFecha = value; }
        }
        public clsConsultarTiposCambio(string str_IdMoneda, DateTime? dt_FchReferencia, string str_TipoTransaccion, string str_ExactaFecha = "S")
        {
            lstr_IdMoneda = str_IdMoneda;
            ldt_FchReferencia = dt_FchReferencia;
            lstr_TipoTransaccion = str_TipoTransaccion;
            lstr_ExactaFecha = str_ExactaFecha;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarTiposCambio.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}