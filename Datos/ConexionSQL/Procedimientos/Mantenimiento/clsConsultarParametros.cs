using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarParametros : clsProcedimientoAlmacenado
    {
        private string lstr_IdParametro;
        public string Lstr_IdParametro
        {
            get { return lstr_IdParametro; }
            set { lstr_IdParametro = value; }
        }

        private string lstr_IdModulo;
        public string Lstr_IdModulo
        {
            get { return lstr_IdModulo; }
            set { lstr_IdModulo = value; }
        }

        private DateTime ldt_FchVigencia;
        public DateTime Ldt_FchVigencia
        {
            get { return ldt_FchVigencia; }
            set { ldt_FchVigencia = value; }
        }

        private string lstr_DesParametro;
        public string Lstr_DesParametro
        {
            get { return lstr_DesParametro; }
            set { lstr_DesParametro = value; }
        }

        private string lstr_TipoParametro;
        public string Lstr_TipoParametro
        {
            get { return lstr_TipoParametro; }
            set { lstr_TipoParametro = value; }
        }

        public clsConsultarParametros(string str_IdParametro, string str_IdModulo, DateTime dt_FchVigencia, string str_DesParametro, string str_TipoParametro)
        {
            lstr_IdParametro = str_IdParametro;
            lstr_IdModulo = str_IdModulo;
            ldt_FchVigencia = dt_FchVigencia;
            lstr_DesParametro = str_DesParametro;
            lstr_TipoParametro = str_TipoParametro;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarParametros.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}