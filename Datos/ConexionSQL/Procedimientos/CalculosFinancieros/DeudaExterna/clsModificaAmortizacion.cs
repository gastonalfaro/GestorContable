using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna
{
    public class clsModificaAmortizacion : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_IdPrestamo;
        private int? lint_IdTramo;
        private string lstr_IdMoneda;   
        private decimal? ldec_Monto;        
        private DateTime? ldt_FchValorAcreedor;
        private DateTime? ldt_FchRecepcion;
        private DateTime? ldt_FchTipoCambio;
        private string lstr_EstadoSigade;
        private string lstr_Modal;
        private string lstr_UsrModifica;
        private DateTime ldt_FchModifica;
        private int? lint_Secuencia;
        private int? lint_SecuenciaAnt;

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
        public string Lstr_IdMoneda
        {
            get { return lstr_IdMoneda; }
            set { lstr_IdMoneda = value; }
        }
        public decimal? Ldec_Monto
        {
            get { return ldec_Monto; }
            set { ldec_Monto = value; }
        }
        public DateTime? Ldt_FchValorAcreedor
        {
            get { return ldt_FchValorAcreedor; }
            set { ldt_FchValorAcreedor = value; }
        }
        public DateTime? Ldt_FchRecepcion
        {
            get { return ldt_FchRecepcion; }
            set { ldt_FchRecepcion = value; }
        }
        public DateTime? Ldt_FchTipoCambio
        {
            get { return ldt_FchTipoCambio; }
            set { ldt_FchTipoCambio = value; }
        }
        public string Lstr_Modal
        {
            get { return lstr_Modal; }
            set { lstr_Modal = value; }
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
        public DateTime Ldt_FchModifica
        {
            get { return ldt_FchModifica; }
            set { ldt_FchModifica = value; }
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

        #endregion 

        #region Constructor

        public clsModificaAmortizacion(string lstr_IdPrestamo, int? lint_IdTramo, string lstr_IdMoneda, decimal? ldec_Monto,
            DateTime? ldt_FchValorAcreedor, DateTime? ldt_FchRecepcion, DateTime? ldt_FchTipoCambio, string lstr_Modal, string lstr_EstadoSigade, int? lint_Secuencia, int? lint_SecuenciaAnt,
            string lstr_UsrModifica, DateTime ldt_FchModifica)
        {

                    
            this.lstr_IdPrestamo = lstr_IdPrestamo;
            this.lint_IdTramo = lint_IdTramo;
            this.lstr_IdMoneda = lstr_IdMoneda;
            this.ldec_Monto = ldec_Monto;
            this.ldt_FchValorAcreedor = ldt_FchValorAcreedor;
            this.ldt_FchRecepcion = ldt_FchRecepcion;
            this.ldt_FchTipoCambio = ldt_FchTipoCambio;
            this.lstr_EstadoSigade = lstr_EstadoSigade;
            this.lstr_Modal = lstr_Modal;
            this.lstr_UsrModifica = lstr_UsrModifica;
            this.ldt_FchModifica = ldt_FchModifica;
            this.lint_Secuencia = lint_Secuencia;
            this.lint_SecuenciaAnt = lint_SecuenciaAnt;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaExterna\\ModificarAmortizacion.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}