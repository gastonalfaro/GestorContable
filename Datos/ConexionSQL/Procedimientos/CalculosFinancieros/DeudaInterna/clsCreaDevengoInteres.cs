using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaInterna
{
    public class clsCreaDevengoInteres : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private int lint_NumValor;
        private string lstr_Nemotecnico;
        private DateTime ldt_Anno;
        private int lint_IdFlujoEfectivoFK;
        private decimal ldec_CostoAmortizacionInicial;
        private decimal ldec_Intereses;
        private decimal ldec_Pago;
        private decimal ldec_CostoAmortizacionFinal;
        private decimal ldec_DescuentoDevengado;
        private decimal ldec_TIR;
        private string lstr_Estado;
        private string lstr_UsrCreacion;

        #endregion

        #region Obtención y asignación

        public int Lint_NumValor {
            get { return lint_NumValor; }
            set { lint_NumValor = value; }
        }
        public string Lstr_Nemotecnico{
            get { return lstr_Nemotecnico; }
            set { lstr_Nemotecnico = value; }
        }
        public DateTime Ldt_Anno {
            get { return ldt_Anno; }
            set { ldt_Anno = value; }
        }
        public int Lint_IdFlujoEfectivoFK
        {
            get { return lint_IdFlujoEfectivoFK; }
            set { lint_IdFlujoEfectivoFK = value; }
        }
        public decimal Ldec_CostoAmortizacionInicial {
            get { return ldec_CostoAmortizacionInicial; }
            set { ldec_CostoAmortizacionInicial = value; }
        }
        public decimal Ldec_Intereses{
            get { return ldec_Intereses; }
            set { ldec_Intereses = value; }
        }
        public decimal Ldec_Pago {
            get { return ldec_Pago; }
            set { ldec_Pago = value; }
        }
        public decimal Ldec_CostoAmortizacionFinal
        {
            get { return ldec_CostoAmortizacionFinal; }
            set { ldec_CostoAmortizacionFinal = value; }
        }
        public decimal Ldec_DescuentoDevengado
        {
            get { return ldec_DescuentoDevengado; }
            set { ldec_DescuentoDevengado = value; }
        }
        public decimal Ldec_TIR
        {
            get { return ldec_TIR; }
            set { ldec_TIR = value; }
        }
        public string Lstr_Estado {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }
        public string Lstr_UsrCreacion {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }

        #endregion 

        #region Constructor

        public clsCreaDevengoInteres(int lint_NumValor, string lstr_Nemotecnico, DateTime ldt_Anno, int lint_IdFlujoEfectivoFK, decimal ldec_CostoAmortizacionInicial,
            decimal ldec_Intereses, decimal ldec_Pago, decimal ldec_CostoAmortizacionFinal, decimal ldec_DescuentoDevengado, decimal ldec_TIR,
            string lstr_Estado, string lstr_UsrCreacion)
        {
            this.lint_NumValor = lint_NumValor;
            this.lstr_Nemotecnico = lstr_Nemotecnico;
            this.ldt_Anno = ldt_Anno;
            this.lint_IdFlujoEfectivoFK = lint_IdFlujoEfectivoFK;
            this.ldec_CostoAmortizacionInicial = ldec_CostoAmortizacionInicial;
            this.ldec_Intereses = ldec_Intereses;
            this.ldec_Pago = ldec_Pago;
            this.ldec_CostoAmortizacionFinal = ldec_CostoAmortizacionFinal;
            this.ldec_DescuentoDevengado = ldec_DescuentoDevengado;
            this.ldec_TIR = ldec_TIR;
            this.lstr_Estado = lstr_Estado;
            this.lstr_UsrCreacion = lstr_UsrCreacion;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaInterna\\CrearDevengoIntereses.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}