using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaInterna
{
    public class clsCreaAsientoDeudaInterna : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private int lint_NumValor;
        private int lint_NumCupon;
        private string lstr_Nemotecnico;
        private string lstr_NroAsiento;
        private DateTime ldt_FchContabilizacion;
        private string lstr_TipoTransaccio;
        private string lstr_UsrCreacion;

        #endregion

        #region Obtención y asignación

        public int Lint_NumValor { get { return lint_NumValor; } set { lint_NumValor = value; } }
        public int Lint_NumCupon { get { return lint_NumCupon; } set { lint_NumCupon = value; } }
        public string Lstr_Nemotecnico { get { return lstr_Nemotecnico; } set { lstr_Nemotecnico = value; } }
        public string Lstr_NroAsiento { get { return lstr_NroAsiento; } set { lstr_NroAsiento = value; } }
        public DateTime Ldt_FchContabilizacion { get { return ldt_FchContabilizacion; } set { ldt_FchContabilizacion = value; } }
        public string Lstr_TipoTransaccio { get { return lstr_TipoTransaccio; } set { lstr_TipoTransaccio = value; } }
        public string Lstr_UsrCreacion { get { return lstr_UsrCreacion; } set { lstr_UsrCreacion = value; } }

        #endregion 

        #region Constructor

        public clsCreaAsientoDeudaInterna(int lint_NumValor, int lint_NumCupon, string lstr_Nemotecnico, string lstr_NroAsiento,
            DateTime ldt_FchContabilizacion, string lstr_TipoTransaccio, string lstr_UsrCreacion)
        {
            this.lint_NumValor = lint_NumValor;
            this.lint_NumCupon = lint_NumCupon;
            this.lstr_Nemotecnico = lstr_Nemotecnico;
            this.lstr_NroAsiento = lstr_NroAsiento;
            this.ldt_FchContabilizacion = ldt_FchContabilizacion;
            this.lstr_TipoTransaccio = lstr_TipoTransaccio;
            this.lstr_UsrCreacion = lstr_UsrCreacion;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaInterna\\CrearAsientoDeudaInterna.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}