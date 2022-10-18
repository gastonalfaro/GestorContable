using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna
{
    public class clsCreaPago : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_IdPrestamo;
        private int? lint_IdTramo;
        private int? lint_IdPago;
        private string lstr_IdMoneda;
        private int? lint_IdAcreedor;
        private string lstr_RefAcreedor;
        private decimal? ldec_MontoInteres;
        private decimal? ldec_MontoComisiones;
        private decimal? ldec_MontoPrincipal;
        private DateTime? ldt_FechaValor;
        private DateTime? ldt_FechaOperacion;
        private DateTime? ldt_FechaTipoCambio;
        private string lstr_Estado;
        private string lstr_UsrCreacion;

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
        public int? Lint_IdPago
        {
            get { return lint_IdPago; }
            set { lint_IdPago = value; }
        }
        public string Lstr_IdMoneda
        {
            get { return lstr_IdMoneda; }
            set { lstr_IdMoneda = value; }
        }
        public int? Lint_IdAcreedor
        {
            get { return lint_IdAcreedor; }
            set { lint_IdAcreedor = value; }
        }
        public string Lstr_RefAcreedor
        {
            get { return lstr_RefAcreedor; }
            set { lstr_RefAcreedor = value; }
        }
        public decimal? Ldec_MontoInteres
        {
            get { return ldec_MontoInteres; }
            set { ldec_MontoInteres = value; }
        }
        public decimal? Ldec_MontoComisiones
        {
            get { return ldec_MontoComisiones; }
            set { ldec_MontoComisiones = value; }
        }
        public decimal? Ldec_MontoPrincipal
        {
            get { return ldec_MontoPrincipal; }
            set { ldec_MontoPrincipal = value; }
        }
        public DateTime? Ldt_FechaValor
        {
            get { return ldt_FechaValor; }
            set { ldt_FechaValor = value; }
        }
        public DateTime? Ldt_FechaOperacion
        {
            get { return ldt_FechaOperacion; }
            set { ldt_FechaOperacion = value; }
        }
        public DateTime? Ldt_FechaTipoCambio
        {
            get { return ldt_FechaTipoCambio; }
            set { ldt_FechaTipoCambio = value; }
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

        #region Constructor

        public clsCreaPago(string lstr_IdPrestamo, int? lint_IdTramo, int? lint_IdPago, string lstr_IdMoneda, int? lint_IdAcreedor, string lstr_RefAcreedor,
            decimal? ldec_MontoInteres, decimal? ldec_MontoComisiones, decimal? ldec_MontoPrincipal, DateTime? ldt_FechaValor, DateTime? ldt_FechaOperacion,
            DateTime? ldt_FechaTipoCambio, string lstr_Estado, string lstr_UsrCreacion)
        {
            this.lstr_IdPrestamo = lstr_IdPrestamo;
            this.lint_IdTramo = lint_IdTramo;
            this.lint_IdPago = lint_IdPago;
            this.lstr_IdMoneda = lstr_IdMoneda;
            this.lint_IdAcreedor = lint_IdAcreedor;
            this.lstr_RefAcreedor = lstr_RefAcreedor;
            this.ldec_MontoInteres = ldec_MontoInteres;
            this.ldec_MontoComisiones = ldec_MontoComisiones;
            this.ldec_MontoPrincipal = ldec_MontoPrincipal;
            this.ldt_FechaValor = ldt_FechaValor;
            this.ldt_FechaOperacion = ldt_FechaOperacion;
            this.ldt_FechaTipoCambio = ldt_FechaTipoCambio;
            this.lstr_Estado = lstr_Estado;
            this.lstr_UsrCreacion = lstr_UsrCreacion;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaExterna\\CrearPago.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}