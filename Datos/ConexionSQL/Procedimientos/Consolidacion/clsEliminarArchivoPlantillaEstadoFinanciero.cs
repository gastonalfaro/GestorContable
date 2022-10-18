using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Consolidacion
{
    public class clsEliminarArchivoPlantillaEstadoFinanciero : clsProcedimientoAlmacenado
    {
        #region variables 
        private string lstr_DireccionConfigs = String.Empty;

        private string lstr_IdEstadoFinancieroArchivoPlantilla;

        private string lstr_Estado;
        private string lstr_UsrCreacion;
        #endregion

        #region obtencion y asignacion
        public string Lstr_IdEstadoFinancieroArchivoPlantilla
        {
            get { return lstr_IdEstadoFinancieroArchivoPlantilla; }
            set { lstr_IdEstadoFinancieroArchivoPlantilla = value; }
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
        public clsEliminarArchivoPlantillaEstadoFinanciero(string str_IdEstadoFinancieroArchivoPlantilla, string str_Estado, string str_UsrCreacion)
        {
            lstr_IdEstadoFinancieroArchivoPlantilla = str_IdEstadoFinancieroArchivoPlantilla;
            lstr_Estado = str_Estado;
            lstr_UsrCreacion = str_UsrCreacion;

            var appSettings = ConfigurationManager.AppSettings;
            lstr_DireccionConfigs = appSettings["DireccionConfigs"];

            EjecucionSP(lstr_DireccionConfigs + "\\Consolidacion\\EliminarArchivoPlantillaEstadoFinanciero.config", this);
        }
        #endregion



    }
}