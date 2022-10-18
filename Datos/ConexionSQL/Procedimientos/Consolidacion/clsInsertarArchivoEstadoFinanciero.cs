using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Consolidacion
{
    public class clsInsertarArchivoEstadoFinanciero : clsProcedimientoAlmacenado
    {
        #region variables
        private string lstr_DireccionConfigs = String.Empty;

        private string lstr_IdEntidad;
        private int lint_IdEstadoFinanciero;
        private int lint_Periodo;
        private string lstr_UnidadTiempoPeriodo;
        private string lstr_NombreArchivo;
        private string lstr_TipoArchivo;
        private int lint_TamanoByteArchivo;
        private DateTime ldt_FechaArchivo;
        private string lstr_Usuario;

        private string lstr_Estado;
        private string lstr_UsrCreacion;
        #endregion

        #region obtencion y asignacion
        public string Lstr_IdEntidad
        {
            get { return lstr_IdEntidad; }
            set { lstr_IdEntidad = value; }
        }
        public int Lint_IdEstadoFinanciero
        {
            get { return lint_IdEstadoFinanciero; }
            set { lint_IdEstadoFinanciero = value; }
        }
        public int Lint_Periodo
        {
            get { return lint_Periodo; }
            set { lint_Periodo = value; }
        }

        public string Lstr_UnidadTiempoPeriodo
        {
            get { return lstr_UnidadTiempoPeriodo; }
            set { lstr_UnidadTiempoPeriodo = value; }
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
        public int Lint_TamanoByteArchivo
        {
            get { return lint_TamanoByteArchivo; }
            set { lint_TamanoByteArchivo = value; }
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
        public clsInsertarArchivoEstadoFinanciero(string str_IdEntidad, int int_EstadoFinanciero, int int_Periodo, string str_UnidadTiempoPeriodo, string str_NombreArchivo, string str_TipoArchivo, int int_TamanoByteArchivo, DateTime dt_FechaArchivo, string str_Usuario, string str_Estado, string str_UsrCreacion)
        {
            lstr_IdEntidad = str_IdEntidad;
            lint_IdEstadoFinanciero = int_EstadoFinanciero;
            lint_Periodo = int_Periodo;
            lstr_UnidadTiempoPeriodo = str_UnidadTiempoPeriodo;
            lstr_NombreArchivo = str_NombreArchivo;
            lstr_TipoArchivo = str_TipoArchivo;
            lint_TamanoByteArchivo = int_TamanoByteArchivo;
            ldt_FechaArchivo = dt_FechaArchivo;
            lstr_Usuario = str_Usuario;

            lstr_Estado = str_Estado;
            lstr_UsrCreacion = str_UsrCreacion;

            var appSettings = ConfigurationManager.AppSettings;
            lstr_DireccionConfigs = appSettings["DireccionConfigs"];

            EjecucionSP(lstr_DireccionConfigs + "\\Consolidacion\\InsertarArchivoEstadoFinanciero.config", this);
        }
        #endregion

    }
}