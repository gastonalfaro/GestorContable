using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;


namespace Datos.ConexionSQL.Procedimientos.Consolidacion
{
    public class clsEliminarArchivo : clsProcedimientoAlmacenado
    {
        #region variables
        private string lstr_DireccionConfigs = String.Empty;

        private string lstr_DTSXPaqueteURL;
        private string lstr_DTSXPaqueteVariable;
        
        #endregion

        #region obtencion y asignacion
        public string Lstr_DTSXPaqueteURL
        {
            get { return lstr_DTSXPaqueteURL; }
            set { lstr_DTSXPaqueteURL = value; }
        }
        
        public string Lstr_DTSXPaqueteVariable
        {
            get { return lstr_DTSXPaqueteVariable; }
            set { lstr_DTSXPaqueteVariable = value; }
        }

        #endregion

        #region procedimientos
        public clsEliminarArchivo(string str_DTSXPaqueteURL, string str_DTSXPaqueteVariable)
        {
            lstr_DTSXPaqueteURL = str_DTSXPaqueteURL;
            lstr_DTSXPaqueteVariable = str_DTSXPaqueteVariable;
           

            var appSettings = ConfigurationManager.AppSettings;
            lstr_DireccionConfigs = appSettings["DireccionConfigs"];

            EjecucionSP(lstr_DireccionConfigs + "\\Consolidacion\\EliminarArchivo.config", this);
        }
        #endregion

    }
}