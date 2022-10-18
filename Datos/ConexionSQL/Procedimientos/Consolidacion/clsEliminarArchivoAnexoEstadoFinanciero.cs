using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Consolidacion
{
    public class clsEliminarArchivoAnexoEstadoFinanciero : clsProcedimientoAlmacenado
    {
        #region variables
        private string lstr_DireccionConfigs = String.Empty;

        private string lstr_IdEstadoFinancieroArchivoAnexo;

        private string lstr_Estado;
        private string lstr_UsrCreacion;
        #endregion

        #region obtencion y asignacion
        public string Lstr_IdEstadoFinancieroArchivoAnexo
        {
            get { return lstr_IdEstadoFinancieroArchivoAnexo; }
            set { lstr_IdEstadoFinancieroArchivoAnexo = value; }
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
        public clsEliminarArchivoAnexoEstadoFinanciero(string str_IdEstadoFinancieroArchivo, string str_Estado, string str_UsrCreacion)
        {
            lstr_IdEstadoFinancieroArchivoAnexo = str_IdEstadoFinancieroArchivo;
            lstr_Estado = str_Estado;
            lstr_UsrCreacion = str_UsrCreacion;

            var appSettings = ConfigurationManager.AppSettings;
            lstr_DireccionConfigs = appSettings["DireccionConfigs"];

            EjecucionSP(lstr_DireccionConfigs + "\\Consolidacion\\EliminarArchivoAnexoEstadoFinanciero.config", this);
        }
        #endregion



    }
}