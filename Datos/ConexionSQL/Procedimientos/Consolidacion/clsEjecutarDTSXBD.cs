using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Consolidacion
{
    public class clsEjecutarDTSXBD : clsProcedimientoAlmacenado
    {
        #region variables
        private string lstr_DireccionConfigs = String.Empty;

        private string lstr_DTSXPaqueteURL;
        private string lstr_DTSXPaqueteNombre;
        private string lstr_DTSXPaqueteVariable;
        private bool lb_bEjecutar64Bit;
        private string lstr_Ruta32Bit;
        private string lstr_DTSXFolderName;
        private string lstr_DTSXProyecto;

        private string lstr_Estado;
        private string lstr_UsrCreacion;
        #endregion

        #region obtencion y asignacion
        public string Lstr_DTSXPaqueteURL
        {
            get { return lstr_DTSXPaqueteURL; }
            set { lstr_DTSXPaqueteURL = value; }
        }

        public string Lstr_DTSXPaqueteNombre
        {
            get { return lstr_DTSXPaqueteNombre; }
            set { lstr_DTSXPaqueteNombre = value; }
        }

        public string Lstr_DTSXPaqueteVariable
        {
            get { return lstr_DTSXPaqueteVariable; }
            set { lstr_DTSXPaqueteVariable = value; }
        }

        public bool Lb_bEjecutar64Bit
        {
            get { return lb_bEjecutar64Bit; }
            set { lb_bEjecutar64Bit = value; }
        }

        public string Lstr_Ruta32Bit
        {
            get { return lstr_Ruta32Bit; }
            set { lstr_Ruta32Bit = value; }
        }

        public string Lstr_DTSXFolderName
        {
            get { return lstr_DTSXFolderName; }
            set { lstr_DTSXFolderName = value; }
        }

        public string Lstr_DTSXProyecto
        {
            get { return lstr_DTSXProyecto; }
            set { lstr_DTSXProyecto = value; }
        }

        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }

        #endregion

        #region procedimientos
        public clsEjecutarDTSXBD(string str_DTSXPaqueteURL, string str_DTSXPaqueteNombre, string str_DTSXPaqueteVariable, bool b_bEjecutar64Bit, string str_Ruta32Bit, string str_Estado, string str_UsrCreacion, string str_DTSXFolderName = null, string str_DTSXProyecto = null)
        {
            lstr_DTSXPaqueteURL = str_DTSXPaqueteURL;
            lstr_DTSXPaqueteNombre = str_DTSXPaqueteNombre;
            lstr_DTSXPaqueteVariable = str_DTSXPaqueteVariable;
            lb_bEjecutar64Bit = b_bEjecutar64Bit;
            lstr_Ruta32Bit = str_Ruta32Bit;
            lstr_Estado = str_Estado;
            lstr_UsrCreacion = str_UsrCreacion;
            lstr_DTSXFolderName = str_DTSXFolderName;
            lstr_DTSXProyecto = str_DTSXProyecto;

            var appSettings = ConfigurationManager.AppSettings;
            lstr_DireccionConfigs = appSettings["DireccionConfigs"];

            EjecucionSP(lstr_DireccionConfigs + "\\Consolidacion\\EjecutarDTSXBD.config", this);
        }
        #endregion

    }
}