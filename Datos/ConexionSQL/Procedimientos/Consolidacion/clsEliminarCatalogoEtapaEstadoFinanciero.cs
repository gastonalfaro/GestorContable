using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Consolidacion
{
    public class clsEliminarCatalogoEtapaEstadoFinanciero : clsProcedimientoAlmacenado
    {
        #region variables 
        private string lstr_DireccionConfigs = String.Empty;

        private int lint_IdEtapaEstadoFinanciero;

        private string lstr_Estado;
        private string lstr_UsrCreacion;
        #endregion

        #region obtencion y asignacion
        public int Lint_IdEtapaEstadoFinanciero
        {
            get { return lint_IdEtapaEstadoFinanciero; }
            set { lint_IdEtapaEstadoFinanciero = value; }
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
        public clsEliminarCatalogoEtapaEstadoFinanciero(int int_IdEtapaEstadoFinanciero, string str_Estado, string str_UsrCreacion)
        {
            lint_IdEtapaEstadoFinanciero = int_IdEtapaEstadoFinanciero;
            lstr_Estado = str_Estado;
            lstr_UsrCreacion = str_UsrCreacion;

            var appSettings = ConfigurationManager.AppSettings;
            lstr_DireccionConfigs = appSettings["DireccionConfigs"];

            EjecucionSP(lstr_DireccionConfigs + "\\Consolidacion\\EliminarCatalogoEtapaEstadoFinanciero.config", this);
        }
        #endregion



    }
}