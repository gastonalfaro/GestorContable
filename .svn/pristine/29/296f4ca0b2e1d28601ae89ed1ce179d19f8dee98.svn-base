using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaInterna
{
    public class clsCreaCostoTransaccion : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lint_NumValor;
        private string lstr_Nemotecnico;
        private DateTime ldt_Fecha;
        private string lstr_Moneda;
        private decimal ldec_Monto;
        private decimal ldec_MontoColones;
        private decimal ldec_TpoCambio;
        private string lstr_Detalle;
        private string lstr_ModuloSINPE;
        private string lstr_Estado;
        private string lstr_UsrCreacion;

        #endregion

        #region Obtención y asignación

        public string Lint_NumValor {
            get { return lint_NumValor; }
            set { lint_NumValor = value; }
        }
        public string Lstr_Nemotecnico {
            get { return lstr_Nemotecnico; }
            set { lstr_Nemotecnico = value; }
        }
        public string Lstr_ModuloSINPE {
            get { return lstr_ModuloSINPE; }
            set { lstr_ModuloSINPE = value; }
        }
        public string Lstr_Moneda
        {
            get { return lstr_Moneda; }
            set { lstr_Moneda = value; }
        }
        public string Lstr_Detalle
        {
            get { return lstr_Detalle; }
            set { lstr_Detalle = value; }
        }
        public decimal Ldec_Monto {
            get { return ldec_Monto; }
            set { ldec_Monto = value; }
        }
        public decimal Ldec_MontoColones
        {
            get { return ldec_MontoColones; }
            set { ldec_MontoColones = value; }
        }
        public decimal Ldec_TpoCambio
        {
            get { return ldec_TpoCambio; }
            set { ldec_TpoCambio = value; }
        }
        public DateTime Ldt_Fecha {
            get { return ldt_Fecha; }
            set { ldt_Fecha = value; }
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

        public clsCreaCostoTransaccion(string lint_NumValor, string lstr_Nemotecnico, DateTime ldt_Fecha, string lstr_Moneda, decimal ldec_Monto, decimal ldec_MontoColones, decimal ldec_TpoCambio, string lstr_Detalle,
            string lstr_ModuloSINPE, string lstr_Estado, string lstr_UsrCreacion)
        {
            this.lint_NumValor = lint_NumValor;
            this.lstr_Nemotecnico = lstr_Nemotecnico;
            this.ldt_Fecha = ldt_Fecha;
            this.lstr_Moneda = lstr_Moneda;
            this.ldec_Monto = ldec_Monto;
            this.ldec_MontoColones = ldec_MontoColones;
            this.ldec_TpoCambio = ldec_TpoCambio;
            this.lstr_Detalle = lstr_Detalle;
            this.lstr_ModuloSINPE = lstr_ModuloSINPE;
            this.lstr_Estado = lstr_Estado;
            this.lstr_UsrCreacion = lstr_UsrCreacion;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaInterna\\CrearCostoTransaccion.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}