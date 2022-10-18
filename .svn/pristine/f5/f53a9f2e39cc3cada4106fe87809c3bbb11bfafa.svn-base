using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Consolidacion
{
    public class clsInsertarArchivoPlantillaEstadoFinanciero : clsProcedimientoAlmacenado
    {
        #region variables
        private string lstr_DireccionConfigs = String.Empty;

        private int lint_IdEstadoFinanciero;
        private string lstr_NombreArchivo;
        private string lstr_TipoArchivo;
        private DateTime ldt_FechaArchivo;
        private string lstr_Usuario;

        private string lstr_Estado;
        private string lstr_UsrCreacion;
        #endregion

        #region obtencion y asignacion
        public int Lint_IdEstadoFinanciero
        {
            get { return lint_IdEstadoFinanciero; }
            set { lint_IdEstadoFinanciero = value; }
        }
        public string Lstr_NombreArchivo
        {
            get { return lstr_NombreArchivo; }
            set { lstr_NombreArchivo = value; }
        }
        public string Lstr_TipoArchivo
        {
            get { return lstr_TipoArchivo; }
            set { lstr_TipoArchivo = value; }
        }
        public DateTime Ldt_FechaArchivo
        {
            get { return ldt_FechaArchivo; }
            set { ldt_FechaArchivo = value; }
        }

        public string Lstr_Usuario
        {
            get { return lstr_Usuario; }
            set { lstr_Usuario = value; }
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
        public clsInsertarArchivoPlantillaEstadoFinanciero(int int_IdEstadoFinanciero, string str_NombreArchivo, string str_TipoArchivo, DateTime dt_FechaArchivo, string str_Usuario, string str_Estado, string str_UsrCreacion)
        {
            lint_IdEstadoFinanciero = int_IdEstadoFinanciero;
            lstr_NombreArchivo = str_NombreArchivo;
            lstr_TipoArchivo = str_TipoArchivo;
            ldt_FechaArchivo = dt_FechaArchivo;
            lstr_Estado = str_Estado;
            lstr_UsrCreacion = str_UsrCreacion;
            lstr_Usuario = str_Usuario;

            var appSettings = ConfigurationManager.AppSettings;
            lstr_DireccionConfigs = appSettings["DireccionConfigs"];

            EjecucionSP(lstr_DireccionConfigs + "\\Consolidacion\\InsertarArchivoPlantillaEstadoFinanciero.config", this);
        }
        #endregion

    }
}