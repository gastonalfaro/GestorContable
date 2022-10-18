using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna
{
    public class clsCreaComision : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_IdPrestamo;
        private string lint_IdTramo;
        private int lint_IdComision;
        private string lstr_TipoComision;
        private DateTime? ldt_FchEfectivoAPartir;
        private DateTime? ldt_FchHasta;
        private string lstr_MonedaPago;
        private decimal? ldec_Porcentaje;
        private decimal ldec_MontoPago;
        private string lstr_MetodoPago;
        private DateTime? ldt_FchPrimerPago;
        private DateTime? ldt_FchUltimoPago;
        private string lstr_Periodo;
        private string lstr_Anno;
        private string lstr_Mes;
        private string lstr_TipoPago;
        private string lstr_EsPago;
        private DateTime? ldt_FchValorAcreedor;
        private string lstr_Estado;
        private string lstr_EstadoComision;
        private string lstr_UsrCreacion;

        #endregion

        #region Obtención y asignación

        public string Lstr_IdPrestamo
        {
            get { return lstr_IdPrestamo; }
            set { lstr_IdPrestamo = value; }
        }
        public string Lint_IdTramo
        {
            get { return lint_IdTramo; }
            set { lint_IdTramo = value; }
        }
        public int Lint_IdComision
        {
            get { return lint_IdComision; }
            set { lint_IdComision = value; }
        }
        public string Lstr_TipoComision
        {
            get { return lstr_TipoComision; }
            set { lstr_TipoComision = value; }
        }
        public DateTime? Ldt_FchEfectivoAPartir
        {
            get { return ldt_FchEfectivoAPartir; }
            set { ldt_FchEfectivoAPartir = value; }
        }
        public DateTime? Ldt_FchHasta
        {
            get { return ldt_FchHasta; }
            set { ldt_FchHasta = value; }
        }
        public string Lstr_MonedaPago
        {
            get { return lstr_MonedaPago; }
            set { lstr_MonedaPago = value; }
        }
        public decimal? Ldec_Porcentaje
        {
            get { return ldec_Porcentaje; }
            set { ldec_Porcentaje = value; }
        }
        public decimal Ldec_MontoPago
        {
            get { return ldec_MontoPago; }
            set { ldec_MontoPago = value; }
        }
        public string Lstr_MetodoPago
        {
            get { return lstr_MetodoPago; }
            set { lstr_MetodoPago = value; }
        }
        public DateTime? Ldt_FchPrimerPago
        {
            get { return ldt_FchPrimerPago; }
            set { ldt_FchPrimerPago = value; }
        }
        public DateTime? Ldt_FchUltimoPago
        {
            get { return ldt_FchUltimoPago; }
            set { ldt_FchUltimoPago = value; }
        }
        public string Lstr_Periodo
        {
            get { return lstr_Periodo; }
            set { lstr_Periodo = value; }
        }
        public string Lstr_Anno
        {
            get { return lstr_Anno; }
            set { lstr_Anno = value; }
        }
        public string Lstr_Mes
        {
            get { return lstr_Mes; }
            set { lstr_Mes = value; }
        }
        public string Lstr_TipoPago
        {
            get { return lstr_TipoPago; }
            set { lstr_TipoPago = value; }
        }
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }
        public string Lstr_EstadoComision
        {
            get { return lstr_EstadoComision; }
            set { lstr_EstadoComision = value; }
        }
        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }
        //public string Lstr_EsPago
        //{
        //    get { return lstr_EsPago; }
        //    set { lstr_EsPago = value; }
        //}
        //public DateTime Ldt_FchValorAcreedor
        //{
        //    get { return ldt_FchValorAcreedor; }
        //    set { ldt_FchValorAcreedor = value; }
        //}

        #endregion 

        #region Constructor

        public clsCreaComision(string lstr_IdPrestamo, string lint_IdTramo, int lint_IdComision, string lstr_TipoComision,
            DateTime? ldt_FchEfectivoAPartir, DateTime? ldt_FchHasta, string lstr_MonedaPago, decimal? ldec_Porcentaje,
            decimal ldec_MontoPago, string lstr_MetodoPago, DateTime? ldt_FchPrimerPago, DateTime? ldt_FchUltimoPago,
            string lstr_Periodo, string lstr_Anno, string lstr_Mes, string lstr_TipoPago, //string lstr_EsPago, DateTime ldt_FchValorAcreedor,
            string lstr_Estado, string lstr_EstadoComision, string lstr_UsrCreacion)
        {
            this.lstr_IdPrestamo = lstr_IdPrestamo;
            this.lint_IdTramo = string.IsNullOrEmpty(lint_IdTramo) ?  "0" :  lint_IdTramo;
            this.lint_IdTramo = lint_IdTramo;
            this.lint_IdComision = lint_IdComision;
            this.lstr_TipoComision = lstr_TipoComision;
            this.ldt_FchEfectivoAPartir = ldt_FchEfectivoAPartir;
            this.ldt_FchHasta = ldt_FchHasta;
            this.lstr_MonedaPago = lstr_MonedaPago;
            this.ldec_Porcentaje = ldec_Porcentaje;
            this.ldec_MontoPago = ldec_MontoPago;
            this.lstr_MetodoPago = lstr_MetodoPago;
            this.ldt_FchPrimerPago = ldt_FchPrimerPago;
            this.ldt_FchUltimoPago = ldt_FchUltimoPago;
            this.lstr_Periodo = lstr_Periodo;
            this.lstr_Anno = lstr_Anno;
            this.lstr_Mes = lstr_Mes;
            this.lstr_TipoPago = lstr_TipoPago;
            this.lstr_Estado = lstr_Estado;
            this.lstr_EstadoComision = lstr_EstadoComision;
            this.lstr_UsrCreacion = lstr_UsrCreacion;
            //this.lstr_EsPago = lstr_EsPago;
            //this.ldt_FchValorAcreedor = ldt_FchValorAcreedor;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaExterna\\CrearComision.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}