using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna
{
    public class clsCreaInteresPago : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_IdPrestamo;
        private int? lint_IdTramo;
        private int? lint_Secuencia;
        private DateTime? ldt_FchValorAcreedor;
        private DateTime? ldt_FchTipoCambio;
        private decimal? ldec_Monto;
        private string lstr_MonedaPago;
        private string lstr_EstadoSigade;
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
        public int? Lint_Secuencia
        {
          get { return lint_Secuencia; }
          set { lint_Secuencia = value; }
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
        public string Lstr_EstadoSigade
        {
          get { return lstr_EstadoSigade; }
          set { lstr_EstadoSigade = value; }
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

        public clsCreaInteresPago(string lstr_IdPrestamo, int? lint_IdTramo, int? lint_Secuencia, DateTime? ldt_FchValorAcreedor, DateTime? ldt_FchTipoCambio,
            decimal? ldec_Monto, string lstr_MonedaPago, string lstr_EstadoSigade, string lstr_Estado, string lstr_UsrCreacion)
        {
            this.lstr_IdPrestamo = lstr_IdPrestamo;
            this.lint_IdTramo = lint_IdTramo;
            this.lint_Secuencia = lint_Secuencia;
            this.ldt_FchValorAcreedor = ldt_FchValorAcreedor;
            this.ldt_FchTipoCambio = ldt_FchTipoCambio;
            this.ldec_Monto = ldec_Monto;
            this.lstr_MonedaPago = lstr_MonedaPago;
            this.lstr_EstadoSigade = lstr_EstadoSigade;
            this.lstr_Estado = lstr_Estado;
            this.lstr_UsrCreacion = lstr_UsrCreacion;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaExterna\\CrearInteresPago.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}