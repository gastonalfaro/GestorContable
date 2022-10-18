using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarAreasFuncionales : clsProcedimientoAlmacenado
    {
        private string lstr_IdAreaFuncional;
        public string Lstr_IdAreaFuncional
        {
            get { return lstr_IdAreaFuncional; }
            set { lstr_IdAreaFuncional = value; }
        }

        private string lstr_NomAreaFuncional;
        public string Lstr_NomAreaFuncional
        {
            get { return lstr_NomAreaFuncional; }
            set { lstr_NomAreaFuncional = value; }
        }


        public clsConsultarAreasFuncionales(string str_IdAreaFuncional, string str_NomAreaFuncional)
        {
            lstr_IdAreaFuncional = str_IdAreaFuncional;
            lstr_NomAreaFuncional = str_NomAreaFuncional;
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