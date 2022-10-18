using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarSociedadesGLSociedadesFi : clsProcedimientoAlmacenado
    {
        private string lstr_IdSociedadGL;
        public string Lstr_IdSociedadGL
        {
            get { return lstr_IdSociedadGL; }
            set { lstr_IdSociedadGL = value; }
        }

        private string lstr_IdModulo;
        public string Lstr_IdModulo
        {
            get { return lstr_IdModulo; }
            set { lstr_IdModulo = value; }
        }

        private string lstr_IdSociedadFi;
        public string Lstr_IdSociedadFi
        {
            get { return lstr_IdSociedadFi; }
            set { lstr_IdSociedadFi = value; }
        }

        public clsConsultarSociedadesGLSociedadesFi(string str_IdSociedadGL, string str_IdModulo, string str_IdSociedadFi)
        {
            lstr_IdSociedadGL = str_IdSociedadGL;            
            lstr_IdModulo = str_IdModulo;
            lstr_IdSociedadFi = str_IdSociedadFi;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarSociedadesGLSociedadesFi.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}