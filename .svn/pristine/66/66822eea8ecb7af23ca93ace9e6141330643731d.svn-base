using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna
{
    public class clsModificaComisionPago : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_IdPrestamo;
        private int? lint_IdTramo;
        private DateTime? ldt_FchPago;
        private Int64? lint_Secuencia;
        private Int64 lint_Consecutivo;
        private DateTime? ldt_FchTipoCambio;
        private decimal? ldec_Monto;
        private string lstr_MonedaPago;
        private string lstr_EstadoSigade;
        private string lstr_TipoComision;
        private string lstr_ModalEjecucion;

        private string lstr_UsrModifica;
        private DateTime? ldt_FchModifica;

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
        public Int64? Lint_Secuencia
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
        public string Lstr_MonedaPago
        {
            get { return lstr_MonedaPago; }
            set { lstr_MonedaPago = value; }
        }
        public decimal? Ldec_Monto
        {
            get { return ldec_Monto; }
            set { ldec_Monto = value; }
        }
        public DateTime? Ldt_FchPago
        {
            get { return ldt_FchPago; }
            set { ldt_FchPago = value; }
        }
        public string Lstr_EstadoSigade
        {
            get { return lstr_EstadoSigade; }
            set { lstr_EstadoSigade = value; }
        }
        public string Lstr_UsrModifica
        {
            get { return lstr_UsrModifica; }
            set { lstr_UsrModifica = value; }
        }
        public DateTime? Ldt_FchModifica
        {
            get { return ldt_FchModifica; }
            set { ldt_FchModifica = value; }
        }

        #endregion

        #region Constructor

        public clsModificaComisionPago(string lstr_IdPrestamo, int? lint_IdTramo, Int64? lint_Secuencia, Int64 lint_Consecutivo, DateTime? ldt_FchTipoCambio,
            string lstr_TipoComision, string lstr_ModalEjecucion,
            string lstr_MonedaPago, decimal? ldec_Monto, DateTime? ldt_FchPago, 
            string lstr_EstadoSigade, string lstr_UsrModifica, DateTime? ldt_FchModifica)
        {
            this.lstr_IdPrestamo = lstr_IdPrestamo;
            this.lint_IdTramo = lint_IdTramo;
            this.lint_Secuencia = lint_Secuencia; 
            this.lint_Consecutivo = lint_Consecutivo;
            this.ldt_FchTipoCambio = ldt_FchTipoCambio;
            this.lstr_TipoComision = lstr_TipoComision;
            this.lstr_ModalEjecucion = lstr_ModalEjecucion;
            this.lstr_MonedaPago = lstr_MonedaPago;
            this.ldec_Monto = ldec_Monto;
            this.ldt_FchPago = ldt_FchPago;
            this.lstr_EstadoSigade = lstr_EstadoSigade;
            this.lstr_UsrModifica = lstr_UsrModifica;
            this.ldt_FchModifica = ldt_FchModifica;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaExterna\\ModificarComisionPago.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}