using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna
{
    public class clsModificaInteresPago : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_IdPrestamo;
        private int? lint_IdTramo;
        private int? lint_Secuencia;
        private int? lint_SecuenciaAnt;
        private DateTime? ldt_FchValorAcreedor;
        private DateTime? ldt_FchTipoCambio;
        private decimal? ldec_Monto;
        private string lstr_MonedaPago;
        private string ldec_EstadoSigade;
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
        public int? Lint_Secuencia
        {
            get { return lint_Secuencia; }
            set { lint_Secuencia = value; }
        }
        public int? Lint_SecuenciaAnt
        {
            get { return lint_SecuenciaAnt; }
            set { lint_SecuenciaAnt = value; }
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
        public decimal? Ldec_Monto
        {
            get { return ldec_Monto; }
            set { ldec_Monto = value; }
        }
        public string Lstr_MonedaPago
        {
            get { return lstr_MonedaPago; }
            set { lstr_MonedaPago = value; }
        }
        public string Ldec_EstadoSigade
        {
            get { return ldec_EstadoSigade; }
            set { ldec_EstadoSigade = value; }
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

        public clsModificaInteresPago(string lstr_IdPrestamo, int? lint_IdTramo, int? lint_Secuencia, DateTime? ldt_FchValorAcreedor, DateTime? ldt_FchTipoCambio,
            decimal? ldec_Monto, string lstr_MonedaPago, string ldec_EstadoSigade, string lstr_UsrModifica, DateTime ldt_FchModifica, int? lint_SecuenciaAnt = null)
        {
            this.lstr_IdPrestamo = lstr_IdPrestamo;
            this.lint_IdTramo = lint_IdTramo;
            this.lint_Secuencia = lint_Secuencia;
            this.ldt_FchValorAcreedor = ldt_FchValorAcreedor;
            this.ldt_FchTipoCambio = ldt_FchTipoCambio;
            this.ldec_Monto = ldec_Monto;
            this.lstr_MonedaPago = lstr_MonedaPago;
            this.ldec_EstadoSigade = ldec_EstadoSigade;
            this.lstr_UsrModifica = lstr_UsrModifica;
            this.ldt_FchModifica = ldt_FchModifica;
            this.lint_SecuenciaAnt = lint_SecuenciaAnt;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaExterna\\ModificarInteresPago.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}