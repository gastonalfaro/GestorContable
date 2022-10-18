using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna
{
    public class clsCreaAmortizacion : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_IdPrestamo;
        private int lint_IdTramo;
        private string lstr_IdMoneda;   
        private decimal ldec_Monto;        
        private DateTime? ldt_FchValorAcreedor;
        private DateTime? ldt_FchRecepcion;
        private DateTime? ldt_FchTipoCambio;
        private string lstr_Modal;
        private string lstr_Estado;
        private string lstr_EstadoSigade;
        private string lstr_UsrCreacion;
        private int lint_secuencia;

        #endregion

        #region Obtención y asignación

        public string Lstr_IdPrestamo
        {
            get { return lstr_IdPrestamo; }
            set { lstr_IdPrestamo = value; }
        }
        public int Lint_IdTramo
        {
            get { return lint_IdTramo; }
            set { lint_IdTramo = value; }
        }
        public string Lstr_IdMoneda
        {
            get { return lstr_IdMoneda; }
            set { lstr_IdMoneda = value; }
        }
        public decimal Ldec_Monto
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
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }
        public string Lstr_EstadoSigade
        {
            get { return lstr_EstadoSigade; }
            set { lstr_EstadoSigade = value; }
        }
        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }
        public int Lint_secuencia
        {
            get { return lint_secuencia; }
            set { lint_secuencia = value; }
        }

        #endregion 

        #region Constructor

        public clsCreaAmortizacion(string lstr_IdPrestamo, int lint_IdTramo, string lstr_IdMoneda, decimal ldec_Monto,
            DateTime? ldt_FchValorAcreedor, DateTime? ldt_FchRecepcion, DateTime? ldt_FchTipoCambio, string lstr_Modal, int lint_secuencia,
            string lstr_Estado, string lstr_EstadoSigade, string lstr_UsrCreacion)
        {

                    
            this.lstr_IdPrestamo = lstr_IdPrestamo;
            this.lint_IdTramo = lint_IdTramo;
            this.lstr_IdMoneda = lstr_IdMoneda;
            this.ldec_Monto = ldec_Monto;
            this.ldt_FchValorAcreedor = ldt_FchValorAcreedor;
            this.ldt_FchRecepcion = ldt_FchRecepcion;
            this.ldt_FchTipoCambio = ldt_FchTipoCambio;
            this.lstr_Modal = lstr_Modal;
            this.lstr_Estado = lstr_Estado;
            this.lstr_EstadoSigade = lstr_EstadoSigade;
            this.lstr_UsrCreacion = lstr_UsrCreacion;
            this.lint_secuencia = lint_secuencia;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaExterna\\CrearAmortizacion.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}