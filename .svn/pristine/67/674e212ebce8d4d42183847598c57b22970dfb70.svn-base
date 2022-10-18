using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna
{
    public class clsCreaComisionPago : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_IdPrestamo;
        private string lint_IdTramo;
        private DateTime ldt_FchPago;
        private Int64 lint_Secuencia;
        private Int64 lint_Consecutivo;
        private DateTime? ldt_FchTipoCambio;
        private decimal ldec_Monto;
        private string lstr_MonedaPago;
        private string lstr_EstadoSigade;
        private string lstr_TipoComision;
        private string lstr_ModalEjecucion;
        private string lstr_Estado;
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
        public DateTime Ldt_FchPago
        {
            get { return ldt_FchPago; }
            set { ldt_FchPago = value; }
        }
        public Int64 Lint_Secuencia
        {
            get { return lint_Secuencia; }
            set { lint_Secuencia = value; }
        }
        public Int64 Lint_Consecutivo
        {
            get { return lint_Consecutivo; }
            set { lint_Consecutivo = value; }
        }
        public DateTime? Ldt_FchTipoCambio
        {
            get { return ldt_FchTipoCambio; }
            set { ldt_FchTipoCambio = value; }
        }
        public decimal Ldec_Monto
        {
            get { return ldec_Monto; }
            set { ldec_Monto = value; }
        }
        public string Lstr_MonedaPago
        {
            get { return lstr_MonedaPago; }
            set { lstr_MonedaPago = value; }
        }
        public string Lstr_EstadoSigade
        {
            get { return lstr_EstadoSigade; }
            set { lstr_EstadoSigade = value; }
        }
        public string Lstr_TipoComision
        {
            get { return lstr_TipoComision; }
            set { lstr_TipoComision = value; }
        }
        public string Lstr_ModalEjecucion
        {
            get { return lstr_ModalEjecucion; }
            set { lstr_ModalEjecucion = value; }
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

        public clsCreaComisionPago(string lstr_IdPrestamo, string lint_IdTramo, DateTime ldt_FchPago, Int64 lint_Secuencia, Int64 lint_Consecutivo, DateTime? ldt_FchTipoCambio,
            decimal ldec_Monto, string lstr_MonedaPago, string lstr_EstadoSigade, string lstr_Estado, string lstr_UsrCreacion, string lstr_TipoComision = null, string lstr_ModalEjecucion = null)
        {
            this.lstr_IdPrestamo = lstr_IdPrestamo;
            this.lint_IdTramo = (string.IsNullOrEmpty(lint_IdTramo)||lint_IdTramo == "-1" ) ? "1" : lint_IdTramo;
            

            //this.lint_IdTramo = lint_IdTramo;
            this.ldt_FchPago = ldt_FchPago;
            this.lint_Secuencia = lint_Secuencia;
            this.lint_Consecutivo = lint_Consecutivo;
            this.ldt_FchTipoCambio = ldt_FchTipoCambio;
            this.ldec_Monto = ldec_Monto;
            this.lstr_MonedaPago = lstr_MonedaPago;
            this.lstr_EstadoSigade = lstr_EstadoSigade;
            this.lstr_Estado = lstr_Estado;
            this.lstr_UsrCreacion = lstr_UsrCreacion;
            this.lstr_TipoComision = lstr_TipoComision;
            this.lstr_ModalEjecucion = lstr_ModalEjecucion;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaExterna\\CrearComisionPago.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}