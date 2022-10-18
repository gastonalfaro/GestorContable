using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsCrearParametro : clsProcedimientoAlmacenado
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

        private string lstr_Valor;
        public string Lstr_Valor
        {
            get { return lstr_Valor; }
            set { lstr_Valor = value; }
        }

        private string lstr_UsrCreacion;
        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }

        public clsCrearParametro(string str_IdParametro, DateTime dt_FchVigencia, string str_IdModulo, string str_DesParametro, string str_TipoParametro, string str_Valor, string str_UsrCreacion)
        {
            lstr_IdParametro = str_IdParametro;
            lstr_IdModulo = str_IdModulo;
            ldt_FchVigencia = dt_FchVigencia;
            lstr_DesParametro = str_DesParametro;
            lstr_TipoParametro = str_TipoParametro;
            lstr_Valor = str_Valor;
            lstr_UsrCreacion = str_UsrCreacion;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\CrearParametro.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}