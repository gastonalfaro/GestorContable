using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna
{
    public class clsModificaInteresPunitivo : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_IdPrestamo;
        private int? lint_IdTramo;
        private DateTime? ldt_FchPagoAPartir;
        private DateTime? ldt_FchTasaAPartir;
        private string lstr_Tasa;
        private decimal? ldec_TasaMargen;
        private string ldec_Anno;
        private string ldec_Mes;
        private string lstr_FactorConversion;
        private DateTime? ldt_FchPagoHasta;
        private string lstr_Periodo;
        private decimal? ldec_PeriodoDias;
        private decimal? ldec_Monto;
        private decimal? ldec_DiasGracia;
        private decimal? ldec_TasaPunitiva;
        private string lstr_UsrModifica;
        private DateTime ldt_FchModifica;

        #endregion

        #region Obtención y asignación

        public string Lstr_IdPrestamo
        {
            get { return lstr_IdPrestamo; }
            set { lstr_IdPrestamo = value; }
        }
        public int? Lint_IdTramo
        {
            get { return lint_IdTramo; }
            set { lint_IdTramo = value; }
        }
        public DateTime? Ldt_FchPagoAPartir
        {
            get { return ldt_FchPagoAPartir; }
            set { ldt_FchPagoAPartir = value; }
        }
        public DateTime? Ldt_FchTasaAPartir
        {
            get { return ldt_FchTasaAPartir; }
            set { ldt_FchTasaAPartir = value; }
        }
        public string Lstr_Tasa
        {
            get { return lstr_Tasa; }
            set { lstr_Tasa = value; }
        }
        public decimal? Ldec_TasaMargen
        {
            get { return ldec_TasaMargen; }
            set { ldec_TasaMargen = value; }
        }
        public string Ldec_Anno
        {
            get { return ldec_Anno; }
            set { ldec_Anno = value; }
        }
        public string Ldec_Mes
        {
            get { return ldec_Mes; }
            set { ldec_Mes = value; }
        }
        public string Lstr_FactorConversion
        {
            get { return lstr_FactorConversion; }
            set { lstr_FactorConversion = value; }
        }
        public DateTime? Ldt_FchPagoHasta
        {
            get { return ldt_FchPagoHasta; }
            set { ldt_FchPagoHasta = value; }
        }
        public string Lstr_Periodo
        {
            get { return lstr_Periodo; }
            set { lstr_Periodo = value; }
        }
        public decimal? Ldec_PeriodoDias
        {
            get { return ldec_PeriodoDias; }
            set { ldec_PeriodoDias = value; }
        }
        public decimal? Ldec_Monto
        {
            get { return ldec_Monto; }
            set { ldec_Monto = value; }
        }
        public decimal? Ldec_DiasGracia
        {
            get { return ldec_DiasGracia; }
            set { ldec_DiasGracia = value; }
        }
        public decimal? Ldec_TasaPunitiva
        {
            get { return ldec_TasaPunitiva; }
            set { ldec_TasaPunitiva = value; }
        }
        public string Lstr_UsrModifica
        {
            get { return lstr_UsrModifica; }
            set { lstr_UsrModifica = value; }
        }
        public DateTime Ldt_FchModifica
        {
            get { return ldt_FchModifica; }
            set { ldt_FchModifica = value; }
        }

        #endregion 

        #region Constructor

        public clsModificaInteresPunitivo(string lstr_IdPrestamo, int? lint_IdTramo, DateTime? ldt_FchPagoAPartir, DateTime? ldt_FchTasaAPartir, string lstr_Tasa,
            decimal? ldec_TasaMargen, string ldec_Anno, string ldec_Mes, string lstr_FactorConversion, DateTime? ldt_FchPagoHasta, string lstr_Periodo, decimal? ldec_PeriodoDias,
            decimal? ldec_Monto, decimal? ldec_DiasGracia, decimal? ldec_TasaPunitiva,
            string lstr_UsrModifica, DateTime ldt_FchModifica)
        {
            this.lstr_IdPrestamo = lstr_IdPrestamo;
            this.lint_IdTramo = lint_IdTramo;
            this.ldt_FchPagoAPartir = ldt_FchPagoAPartir;
            this.ldt_FchTasaAPartir = ldt_FchTasaAPartir;
            this.lstr_Tasa = lstr_Tasa;
            this.ldec_TasaMargen = ldec_TasaMargen;
            this.ldec_Anno = ldec_Anno;
            this.ldec_Mes = ldec_Mes;
            this.lstr_FactorConversion = lstr_FactorConversion;
            this.ldt_FchPagoHasta = ldt_FchPagoHasta;
            this.lstr_Periodo = lstr_Periodo;
            this.ldec_PeriodoDias = ldec_PeriodoDias;
            this.ldec_Monto = ldec_Monto;
            this.ldec_DiasGracia = ldec_DiasGracia;
            this.ldec_TasaPunitiva = ldec_TasaPunitiva;
            this.lstr_UsrModifica = lstr_UsrModifica;
            this.ldt_FchModifica = ldt_FchModifica;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaExterna\\ModificarInteresPunitivo.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}