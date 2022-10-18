using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna
{
    public class clsModificaPrestamo : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_IdPrestamo;
        private string lstr_Fuente;
        private string lstr_Situacion;
        private string lstr_Plazo;
        private string lstr_Nombre;
        private DateTime ldt_Firmado;
        private DateTime ldt_LimiteGiro;
        private DateTime ldt_LimiteEfectivo;
        private DateTime ldt_Efectivo;
        private decimal ldec_Monto;
        private string lstr_IdMoneda;
        private string lstr_TipoTramo;
        private string lstr_Proposito;
        private string lstr_GarantiaPublica;
        private string lstr_OrigenDeuda;
        private int lint_IdAcreedor;
        private int lint_IdDeudor;
        private string lstr_TipoPrestamo;
        private decimal ldec_Tasa;
        private string lstr_UsrModifica;
        private DateTime ldt_FchModifica;
        //------o------//
        private string lstr_NbrAcreedor;
        private string lstr_CatAcreedor;
        private string lstr_TpoAcreedor;
        private string lstr_NbrDeudor;
        private string lstr_CatDeudor;
        //------o------//
        private string lstr_CondicionPrestamo;
        private string lstr_ExisteObligacion;
        private string lstr_CondicionMotivo;
        private decimal ldec_CondicionTasa;
        private decimal ldec_CondicionMonto;
        private DateTime ldt_CondicionFchInicio;
        private DateTime ldt_CondicionFchFin;

        #endregion

        #region Obtención y asignación

        public string Lstr_IdPrestamo
        {
            get { return lstr_IdPrestamo; }
            set { lstr_IdPrestamo = value; }
        }
        public string Lstr_Fuente
        {
            get { return lstr_Fuente; }
            set { lstr_Fuente = value; }
        }
        public string Lstr_Situacion
        {
            get { return lstr_Situacion; }
            set { lstr_Situacion = value; }
        }
        public string Lstr_Plazo
        {
            get { return lstr_Plazo; }
            set { lstr_Plazo = value; }
        }
        public string Lstr_Nombre
        {
            get { return lstr_Nombre; }
            set { lstr_Nombre = value; }
        }
        public DateTime Ldt_Firmado
        {
            get { return ldt_Firmado; }
            set { ldt_Firmado = value; }
        }
        public DateTime Ldt_LimiteGiro
        {
            get { return ldt_LimiteGiro; }
            set { ldt_LimiteGiro = value; }
        }
        public DateTime Ldt_LimiteEfectivo
        {
            get { return ldt_LimiteEfectivo; }
            set { ldt_LimiteEfectivo = value; }
        }
        public DateTime Ldt_Efectivo
        {
            get { return ldt_Efectivo; }
            set { ldt_Efectivo = value; }
        }
        public decimal Ldec_Monto
        {
            get { return ldec_Monto; }
            set { ldec_Monto = value; }
        }
        public string Lstr_IdMoneda
        {
            get { return lstr_IdMoneda; }
            set { lstr_IdMoneda = value; }
        }
        public string Lstr_TipoTramo
        {
            get { return lstr_TipoTramo; }
            set { lstr_TipoTramo = value; }
        }
        public string Lstr_Proposito
        {
            get { return lstr_Proposito; }
            set { lstr_Proposito = value; }
        }
        public string Lstr_GarantiaPublica
        {
            get { return lstr_GarantiaPublica; }
            set { lstr_GarantiaPublica = value; }
        }
        public string Lstr_OrigenDeuda
        {
            get { return lstr_OrigenDeuda; }
            set { lstr_OrigenDeuda = value; }
        }
        public int Lint_IdAcreedor
        {
            get { return lint_IdAcreedor; }
            set { lint_IdAcreedor = value; }
        }
        public int Lint_IdDeudor
        {
            get { return lint_IdDeudor; }
            set { lint_IdDeudor = value; }
        }
        public string Lstr_TipoPrestamo
        {
            get { return lstr_TipoPrestamo; }
            set { lstr_TipoPrestamo = value; }
        }
        public decimal Ldec_Tasa
        {
            get { return ldec_Tasa; }
            set { ldec_Tasa = value; }
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
        //------o------//
        public string Lstr_NbrAcreedor
        {
            get { return lstr_NbrAcreedor; }
            set { lstr_NbrAcreedor = value; }
        }
        public string Lstr_CatAcreedor
        {
            get { return lstr_CatAcreedor; }
            set { lstr_CatAcreedor = value; }
        }
        public string Lstr_TpoAcreedor
        {
            get { return lstr_TpoAcreedor; }
            set { lstr_TpoAcreedor = value; }
        }
        public string Lstr_NbrDeudor
        {
            get { return lstr_NbrDeudor; }
            set { lstr_NbrDeudor = value; }
        }
        public string Lstr_CatDeudor
        {
            get { return lstr_CatDeudor; }
            set { lstr_CatDeudor = value; }
        }
        //------o------//
        public string Lstr_CondicionPrestamo
        {
            get { return lstr_CondicionPrestamo; }
            set { lstr_CondicionPrestamo = value; }
        }
        public string Lstr_ExisteObligacion
        {
            get { return lstr_ExisteObligacion; }
            set { lstr_ExisteObligacion = value; }
        }
        public string Lstr_CondicionMotivo
        {
            get { return lstr_CondicionMotivo; }
            set { lstr_CondicionMotivo = value; }
        }
        public decimal Ldec_CondicionTasa
        {
            get { return ldec_CondicionTasa; }
            set { ldec_CondicionTasa = value; }
        }
        public decimal Ldec_CondicionMonto
        {
            get { return ldec_CondicionMonto; }
            set { ldec_CondicionMonto = value; }
        }
        public DateTime Ldt_CondicionFchInicio
        {
            get { return ldt_CondicionFchInicio; }
            set { ldt_CondicionFchInicio = value; }
        }
        public DateTime Ldt_CondicionFchFin
        {
            get { return ldt_CondicionFchFin; }
            set { ldt_CondicionFchFin = value; }
        }
        #endregion 

        #region Constructor

        public clsModificaPrestamo(string lstr_IdPrestamo, string lstr_Fuente, string lstr_Situacion, string lstr_Plazo, 
            string lstr_Nombre, DateTime ldt_Firmado, DateTime ldt_LimiteGiro, DateTime ldt_LimiteEfectivo, DateTime ldt_Efectivo, 
            decimal ldec_Monto, string lstr_IdMoneda, string lstr_TipoTramo, string lstr_Proposito, string lstr_GarantiaPublica, 
            string lstr_OrigenDeuda, int lint_IdAcreedor, int lint_IdDeudor, string lstr_TipoPrestamo, decimal ldec_Tasa,
            string lstr_NbrAcreedor, string lstr_CatAcreedor, string lstr_TpoAcreedor, string lstr_NbrDeudor,
            string lstr_CatDeudor, string lstr_CondicionPrestamo, string lstr_ExisteObligacion,
            string lstr_CondicionMotivo, decimal ldec_CondicionTasa, decimal ldec_CondicionMonto, DateTime ldt_CondicionFchInicio,
            DateTime ldt_CondicionFchFin, string lstr_UsrModifica, DateTime ldt_FchModifica)
        {
            this.lstr_IdPrestamo = lstr_IdPrestamo;
            this.lstr_Fuente = lstr_Fuente;
            this.lstr_Situacion = lstr_Situacion;
            this.lstr_Plazo = lstr_Plazo;
            this.lstr_Nombre = lstr_Nombre;
            this.ldt_Firmado = ldt_Firmado;
            this.ldt_LimiteGiro = ldt_LimiteGiro;
            this.ldt_LimiteEfectivo = ldt_LimiteEfectivo;
            this.ldt_Efectivo = ldt_Efectivo;
            this.ldec_Monto = ldec_Monto;
            this.lstr_IdMoneda = lstr_IdMoneda;
            this.lstr_TipoTramo = lstr_TipoTramo;
            this.lstr_Proposito = lstr_Proposito;
            this.lstr_GarantiaPublica = lstr_GarantiaPublica;
            this.lstr_OrigenDeuda = lstr_OrigenDeuda;
            this.lint_IdAcreedor = lint_IdAcreedor;
            this.lint_IdDeudor = lint_IdDeudor;
            this.lstr_TipoPrestamo = lstr_TipoPrestamo;
            this.ldec_Tasa = ldec_Tasa;
            this.lstr_NbrAcreedor = lstr_NbrAcreedor;
            this.lstr_CatAcreedor = lstr_CatAcreedor;
            this.lstr_TpoAcreedor = lstr_TpoAcreedor;
            this.lstr_NbrDeudor = lstr_NbrDeudor;
            this.lstr_CatDeudor = lstr_CatDeudor;
            this.lstr_CondicionPrestamo = lstr_CondicionPrestamo;
            this.lstr_ExisteObligacion = lstr_ExisteObligacion;
            this.lstr_CondicionMotivo = lstr_CondicionMotivo;
            this.ldec_CondicionTasa = ldec_CondicionTasa;
            this.ldec_CondicionMonto = ldec_CondicionMonto;
            this.ldt_CondicionFchInicio = ldt_CondicionFchInicio;
            this.ldt_CondicionFchFin = ldt_CondicionFchFin;
            this.lstr_UsrModifica = lstr_UsrModifica;
            this.ldt_FchModifica = ldt_FchModifica;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaExterna\\ModificarPrestamo.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}