using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarJerarquias : clsProcedimientoAlmacenado
    {
        private string lstr_IdJerarquia;
        public string Lstr_IdJerarquia
        {
            get { return lstr_IdJerarquia; }
            set { lstr_IdJerarquia = value; }
        }

        private string lstr_Vista;
        public string Lstr_Vista
        {
            get { return lstr_Vista; }
            set { lstr_Vista = value; }
        }

        private string lstr_NomCorto;
        public string Lstr_NomCorto
        {
            get { return lstr_NomCorto; }
            set { lstr_NomCorto = value; }
        }

        private string lstr_NomJerarquia;
        public string Lstr_NomJerarquia
        {
            get { return lstr_NomJerarquia; }
            set { lstr_NomJerarquia = value; }
        }

        public clsConsultarJerarquias(string str_Vista, string str_IdJerarquia, string str_NomCorto, string str_NomJerarquia)
        {
            lstr_IdJerarquia = str_IdJerarquia;
            lstr_Vista = str_Vista;
            lstr_NomCorto = str_NomCorto;
            lstr_NomJerarquia = str_NomJerarquia;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarAreasFuncionales.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}