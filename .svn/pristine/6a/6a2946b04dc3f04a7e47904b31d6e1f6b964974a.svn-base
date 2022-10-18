using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogicaNegocio.CalculosFinancieros.DeudaExterna
{
    public class clsIntereses
    {
        #region parametros

        /// <summary>
        /// Declaración e inicialización de variables de Clase Intereses
        /// </summary>
        
        private string lstr_IdPrestamo;
        private int lint_IdTramo;
        private string lstr_Tasa;
        private DateTime ldt_FchTasaAPartir;
        private decimal ldec_TasaMargen;
        private decimal lint_Ano;
        private decimal lint_Mes;
        private string lstr_FactorConversion;
        private DateTime ldt_FchPagoAPartir;
        private DateTime ldt_FchPagoHasta;
        private decimal ldec_Periodo;
        private decimal ldec_PeriodoDias;
        private decimal ldec_Monto;
        private decimal ldec_DiasGracia;
        private decimal ldec_TasaPunitiva;

        #endregion

        #region constructores

        /// <summary>
        /// Constructor de la clase Acreedores, permite crear un interes y almacenarlo en sistema
        /// </summary>
        
        public clsIntereses(){}

        /// <summary>
        /// Constructor sobrecargado con información obligatoria de intereses
        /// </summary>
        
        public clsIntereses(string lstr_IdPrestamo, int lint_IdTramo, string lstr_Tasa, DateTime ldt_FchTasaAPartir, decimal ldec_TasaMargen,
                            decimal lint_Ano, decimal lint_Mes, string lstr_FactorConversion, DateTime ldt_FchPagoAPartir, DateTime ldt_FchPagoHasta,
                            decimal ldec_Periodo, decimal ldec_PeriodoDias, decimal ldec_Monto, decimal ldec_DiasGracia, decimal ldec_TasaPunitiva)
        {
            this.lstr_IdPrestamo = lstr_IdPrestamo;
            this.lint_IdTramo = lint_IdTramo;
            this.lstr_Tasa = lstr_Tasa;
            this.ldt_FchTasaAPartir = ldt_FchTasaAPartir;
            this.ldec_TasaMargen = ldec_TasaMargen;
            this.lint_Ano = lint_Ano;
            this.lint_Mes = lint_Mes;
            this.lstr_FactorConversion = lstr_FactorConversion;
            this.ldt_FchPagoAPartir = ldt_FchPagoAPartir;
            this.ldt_FchPagoHasta = ldt_FchPagoHasta;
            this.ldec_Periodo = ldec_Periodo;
            this.ldec_PeriodoDias = ldec_PeriodoDias;
            this.ldec_Monto = ldec_Monto;
            this.ldec_DiasGracia = ldec_DiasGracia;
            this.ldec_TasaPunitiva = ldec_TasaPunitiva;
        }
        #endregion

        #region obtención y asignación
        
        /// <summary>
        /// Obtención y asignación de datos
        /// </summary>
        
        public string Lstr_IdPrestamo
        {
            get { return lstr_IdPrestamo;}
            set { lstr_IdPrestamo = value;}
        }

        public int Lint_IdTramo
        {
            get { return lint_IdTramo; }
            set { lint_IdTramo = value; }
        }

        public string Lstr_Tasa
        {
            get { return lstr_Tasa; }
            set { lstr_Tasa = value; }
        }

        public DateTime Ldt_FchTasaAPartir
        {
            get { return ldt_FchTasaAPartir; }
            set { ldt_FchTasaAPartir = value; }
        }

        public decimal Ldec_TasaMargen
        {
            get { return ldec_TasaMargen; }
            set { ldec_TasaMargen = value; }
        }

        public decimal Lint_Ano
        {
            get { return lint_Ano; }
            set { lint_Ano = value; }
        }

        public decimal Lint_Mes
        {
            get { return lint_Mes; }
            set { lint_Mes = value; }
        }

        public string Lstr_FactorConversion
        {
            get { return lstr_FactorConversion; }
            set { lstr_FactorConversion = value; }
        }

        public DateTime Ldt_FchPagoAPartir
        {
            get { return ldt_FchPagoAPartir; }
            set { ldt_FchPagoAPartir = value; }
        }

        public DateTime Ldt_FchPagoHasta
        {
            get { return ldt_FchPagoHasta; }
            set { ldt_FchPagoHasta = value; }
        }

        public decimal Ldec_Periodo
        {
            get { return ldec_Periodo; }
            set { ldec_Periodo = value; }
        }

        public decimal Ldec_PeriodoDias
        {
            get { return ldec_PeriodoDias; }
            set { ldec_PeriodoDias = value; }
        }

        public decimal Ldec_Monto
        {
            get { return ldec_Monto; }
            set { ldec_Monto = value; }
        }

        public decimal Ldec_DiasGracia
        {
            get { return ldec_DiasGracia; }
            set { ldec_DiasGracia = value; }
        }

        public decimal Ldec_TasaPunitiva
        {
            get { return ldec_TasaPunitiva; }
            set { ldec_TasaPunitiva = value; }
        }

        #endregion
    }
}