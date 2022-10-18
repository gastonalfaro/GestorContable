using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaInterna
{
    public class clsCreaCalculoFlujoEfectivo : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private int lint_NumValor;
        private string lstr_Nemotecnico;
        private string lstr_Periodo;
        private decimal ldec_TasaInteres;
        private decimal ldec_Intereses;
        private decimal ldec_FlujoEfectivo;
        private string lstr_NroAsiento;
        private string lstr_UsrCreacion;

        #endregion

        #region Obtención y asignación

        public int Lint_NumValor
        {
            get { return lint_NumValor; }
            set { lint_NumValor = value; }
        }
        public string Lstr_Nemotecnico
        {
            get { return lstr_Nemotecnico; }
            set { lstr_Nemotecnico = value; }
        }
        public string Lstr_Periodo
        {
            get { return lstr_Periodo; }
            set { lstr_Periodo = value; }
        }
        public decimal Ldec_TasaInteres
        {
            get { return ldec_TasaInteres; }
            set { ldec_TasaInteres = value; }
        }        
        public decimal Ldec_Intereses{
            get { return ldec_Intereses; }
            set { ldec_Intereses = value; }
        }
        public decimal Ldec_FlujoEfectivo
        {
            get { return ldec_FlujoEfectivo; }
            set { ldec_FlujoEfectivo = value; }
        }
        public string Lstr_NroAsiento
        {
            get { return lstr_NroAsiento; }
            set { lstr_NroAsiento = value; }
        }

        public string Lstr_UsrCreacion
        {
            get
            {
                return lstr_UsrCreacion;
            }

            set
            {
                lstr_UsrCreacion = value;
            }
        }

        #endregion

        #region Constructor

        public clsCreaCalculoFlujoEfectivo(int lint_NumValor, string lstr_Nemotecnico, string lstr_Periodo, decimal ldec_TasaInteres,
            decimal ldec_Intereses, decimal ldec_FlujoEfectivo, string lstr_NroAsiento, string lstr_UsrCreacion)
        {
            this.lint_NumValor = lint_NumValor;
            this.lstr_Nemotecnico = lstr_Nemotecnico;
            this.lstr_Periodo = lstr_Periodo;
            this.ldec_TasaInteres = ldec_TasaInteres;
            this.ldec_Intereses = ldec_Intereses;
            this.ldec_FlujoEfectivo = ldec_FlujoEfectivo;
            this.lstr_NroAsiento = lstr_NroAsiento;
            this.Lstr_UsrCreacion = lstr_UsrCreacion;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaInterna\\CrearCalculoFlujoEfectivo.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}