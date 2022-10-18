using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsCrearAreaFuncional : clsProcedimientoAlmacenado
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

       
        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        private string lstr_UsrCreacion;
        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }

        public clsCrearAreaFuncional(string str_IdAreaFuncional, string str_NomAreaFuncional, string str_Estado, string str_UsrCreacion)
        {
            lstr_IdAreaFuncional = str_IdAreaFuncional;
            lstr_NomAreaFuncional = str_NomAreaFuncional;
            lstr_Estado = str_Estado;
            lstr_UsrCreacion = str_UsrCreacion;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\CrearAreaFuncional.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}