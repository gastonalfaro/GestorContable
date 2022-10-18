using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Consolidacion
{
    public class clsRPTEstadoFinancieroFlujoEfectivoDesagregado : clsProcedimientoAlmacenado
    {
        #region variables
        private string lstr_DireccionConfigs = String.Empty;

        private string lstr_IdEntidad;
        private int lint_Periodo;
        private string lstr_UnidadTiempoPeriodo;

        private string lstr_Estado;
        private string lstr_UsrCreacion;
        #endregion

        #region obtencion y asignacion
        public string Lstr_IdEntidad
        {
            get { return lstr_IdEntidad; }
            set { lstr_IdEntidad = value; }
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
        public clsRPTEstadoFinancieroFlujoEfectivoDesagregado(string str_IdEntidad, int int_Periodo, string str_UnidadTiempoPeriodo, string str_Estado, string str_UsrCreacion)
        {
            lstr_IdEntidad = str_IdEntidad;
            lint_Periodo = int_Periodo;
            lstr_UnidadTiempoPeriodo = str_UnidadTiempoPeriodo;

            lstr_Estado = str_Estado;
            lstr_UsrCreacion = str_UsrCreacion;

            var appSettings = ConfigurationManager.AppSettings;
            lstr_DireccionConfigs = appSettings["DireccionConfigs"];

            EjecucionSP(lstr_DireccionConfigs + "\\Consolidacion\\RPTEstadoFinancieroFlujoEfectivoDesagregado.config", this);
        }
        #endregion

    }
}