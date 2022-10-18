using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarValoresIndicadoresEco : clsProcedimientoAlmacenado
    {
        private string lstr_IdIndicadorEco;
        public string Lstr_IdIndicadorEco
        {
            get { return lstr_IdIndicadorEco; }
            set { lstr_IdIndicadorEco = value; }
        }


        private DateTime ldt_FchReferencia;
        public DateTime Ldt_FchReferencia
        {
            get { return ldt_FchReferencia; }
            set { ldt_FchReferencia = value; }
        }

        private string lstr_ExactaFecha;
        public string Lstr_ExactaFecha
        {
            get { return lstr_ExactaFecha; }
            set { lstr_ExactaFecha = value; }
        }

        public clsConsultarValoresIndicadoresEco(string str_IdIndicadorEco, DateTime dt_FchReferencia, string str_ExactaFecha = "S")
        {
            lstr_IdIndicadorEco = str_IdIndicadorEco;
            ldt_FchReferencia = dt_FchReferencia;
            lstr_ExactaFecha = str_ExactaFecha;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarValoresIndicadoresEco.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}