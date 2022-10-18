using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna
{
    public class clsConsultaAmortizacion : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_IdPrestamo;
        private int? lint_IdTramo;
        private DateTime? ldt_FchValorAcreedor;
        private DateTime? ldt_FchTipoCambio;
        private DateTime? ldt_FchRecepcion;
        private string lstr_IdMoneda;
        private int? lint_Secuencia;
        private DateTime? ldt_FchHasta;

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
        public DateTime? Ldt_FchValorAcreedor
        {
            get { return ldt_FchValorAcreedor; }
            set { ldt_FchValorAcreedor = value; }
        }
        public DateTime? Ldt_FchTipoCambio
        {
            get { return ldt_FchTipoCambio; }
            set { ldt_FchTipoCambio = value; }
        }
        public DateTime? Ldt_FchRecepcion
        {
            get { return ldt_FchRecepcion; }
            set { ldt_FchRecepcion = value; }
        }
        public string Lstr_IdMoneda
        {
            get { return lstr_IdMoneda; }
            set { lstr_IdMoneda = value; }
        }
        public int? Lint_Secuencia
        {
            get { return lint_Secuencia; }
            set { lint_Secuencia = value; }
        }

        public DateTime? Ldt_FchHasta
        {
            get { return ldt_FchHasta; }
            set { ldt_FchHasta = value; }
        }

        #endregion 

        #region Constructor

        public clsConsultaAmortizacion(string lstr_IdPrestamo, int? lint_IdTramo, DateTime? ldt_FchValorAcreedor, DateTime? ldt_FchTipoCambio, DateTime? ldt_FchRecepcion, string lstr_IdMoneda, int? lint_Secuencia, DateTime? ldt_FchHasta)
        {
            this.lstr_IdPrestamo = lstr_IdPrestamo;
            this.lint_IdTramo = lint_IdTramo;
            this.ldt_FchValorAcreedor = ldt_FchValorAcreedor;
            this.ldt_FchTipoCambio = ldt_FchTipoCambio;
            this.ldt_FchRecepcion = ldt_FchRecepcion;
            this.lstr_IdMoneda = lstr_IdMoneda;
            this.lint_Secuencia = lint_Secuencia;
            this.ldt_FchHasta = ldt_FchHasta;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaExterna\\ConsultarAmortizacion.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}