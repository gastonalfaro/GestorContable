using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna
{
    public class clsModificaGiro : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_IdPrestamo;
        private int lint_IdTramo;
        private DateTime ldt_FchDesembolso;
        private DateTime ldt_FchEstimada;
        private string lstr_Moneda;
        private int lint_Secuencia;
        private decimal ldec_Monto;
        private string lstr_UsrModifica;
        private DateTime ldt_FchModifica;

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
        public DateTime Ldt_FchDesembolso
        {
            get { return ldt_FchDesembolso; }
            set { ldt_FchDesembolso = value; }
        }
        public DateTime Ldt_FchEstimada
        {
            get { return ldt_FchEstimada; }
            set { ldt_FchEstimada = value; }
        }
        public string Lstr_Moneda
        {
            get { return lstr_Moneda; }
            set { lstr_Moneda = value; }
        }
        public int Lint_Secuencia
        {
            get { return lint_Secuencia; }
            set { lint_Secuencia = value; }
        }
        public decimal Ldec_Monto
        {
            get { return ldec_Monto; }
            set { ldec_Monto = value; }
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

        public clsModificaGiro(string lstr_IdPrestamo, int lint_IdTramo, DateTime ldt_FchDesembolso, DateTime ldt_FchEstimada, string lstr_Moneda, int lint_Secuencia, decimal ldec_Monto,
            string lstr_UsrModifica, DateTime ldt_FchModifica)
        {
            this.lstr_IdPrestamo = lstr_IdPrestamo;
            this.lint_IdTramo = lint_IdTramo;
            this.ldt_FchDesembolso = ldt_FchDesembolso;
            this.ldt_FchEstimada = ldt_FchEstimada;
            this.lstr_Moneda = lstr_Moneda;
            this.lint_Secuencia = lint_Secuencia;
            this.ldec_Monto = ldec_Monto;
            this.lstr_UsrModifica = lstr_UsrModifica;
            this.ldt_FchModifica = ldt_FchModifica;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaExterna\\ModificarGiro.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}