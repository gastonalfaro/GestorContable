using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Consolidacion
{
    public class clsBuscarArchivoEstadoFinanciero : clsProcedimientoAlmacenado
    {
        #region variables
        private string lstr_DireccionConfigs = String.Empty;

        private string lstr_IdEstadoFinancieroArchivo;

        private string lstr_Estado;
        private string lstr_UsrCreacion;
        #endregion

        #region obtencion y asignacion
        public string Lstr_IdEstadoFinancieroArchivo
        {
            get { return lstr_IdEstadoFinancieroArchivo; }
            set { lstr_IdEstadoFinancieroArchivo = value; }
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
        public clsBuscarArchivoEstadoFinanciero(string str_IdEstadoFinancieroArchivo, string str_Estado, string str_UsrCreacion)
        {
            lstr_IdEstadoFinancieroArchivo = str_IdEstadoFinancieroArchivo;
            lstr_Estado = str_Estado;
            lstr_UsrCreacion = str_UsrCreacion;

            var appSettings = ConfigurationManager.AppSettings;
            lstr_DireccionConfigs = appSettings["DireccionConfigs"];

            EjecucionSP(lstr_DireccionConfigs + "\\Consolidacion\\BuscarArchivoEstadoFinanciero.config", this);
        }

        #endregion

    }
}