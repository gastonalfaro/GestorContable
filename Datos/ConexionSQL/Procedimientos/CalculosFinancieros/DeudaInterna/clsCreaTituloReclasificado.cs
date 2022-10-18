using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaInterna
{
    public class clsCreaTituloReclasificado : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private int lint_NumValor;
        private string lstr_Nemotecnico;
        private string lstr_Tipo;
        private string lstr_Moneda;
        private decimal ldec_ValorFacial;
        private decimal ldec_ValorTransadoBruto;
        private decimal ldec_ValorTransadoNeto;
        private DateTime ldt_FchValor;
        private DateTime ldt_FchCancelacion;
        private DateTime ldt_FchVencimiento;
        private string lstr_SistemaNegociacion;
        private string lstr_Estado;
        private string lstr_UsrCreacion;
        
        #endregion

        #region Obtención y asignación

        public int Lint_NumValor { get { return lint_NumValor; } set { lint_NumValor = value; } }
        public string Lstr_Nemotecnico { get { return lstr_Nemotecnico; } set { lstr_Nemotecnico = value; } }
        public string Lstr_Tipo { get { return lstr_Tipo; } set { lstr_Tipo = value; } }
        public string Lstr_Moneda { get { return lstr_Moneda; } set { lstr_Moneda = value; } }
        public decimal Ldec_ValorFacial { get { return ldec_ValorFacial; } set { ldec_ValorFacial = value; } }
        public decimal Ldec_ValorTransadoBruto { get { return ldec_ValorTransadoBruto; } set { ldec_ValorTransadoBruto = value; } }
        public decimal Ldec_ValorTransadoNeto { get { return ldec_ValorTransadoNeto; } set { ldec_ValorTransadoNeto = value; } }
        public DateTime Ldt_FchValor { get { return ldt_FchValor; } set { ldt_FchValor = value; } }
        public DateTime Ldt_FchCancelacion { get { return ldt_FchCancelacion; } set { ldt_FchCancelacion = value; } }
        public DateTime Ldt_FchVencimiento { get { return ldt_FchVencimiento; } set { ldt_FchVencimiento = value; } }
        public string Lstr_SistemaNegociacion { get { return lstr_SistemaNegociacion; } set { lstr_SistemaNegociacion = value; } }
        public string Lstr_Estado { get { return lstr_Estado; } set { lstr_Estado = value; } }
        public string Lstr_UsrCreacion { get { return lstr_UsrCreacion; } set { lstr_UsrCreacion = value; } }

        #endregion 

        #region Constructor

        public clsCreaTituloReclasificado(int lint_NumValor, string lstr_Nemotecnico, string lstr_Tipo, string lstr_Moneda, decimal ldec_ValorFacial,
            decimal ldec_ValorTransadoBruto, decimal ldec_ValorTransadoNeto, DateTime ldt_FchValor, DateTime ldt_FchCancelacion, DateTime ldt_FchVencimiento, 
            string lstr_SistemaNegociacion, string lstr_Estado, string lstr_UsrCreacion)
        {
            this.lint_NumValor = lint_NumValor;
            this.lstr_Nemotecnico = lstr_Nemotecnico;
            this.lstr_Tipo = lstr_Tipo;
            this.lstr_Moneda = lstr_Moneda;
            this.ldec_ValorFacial = ldec_ValorFacial;
            this.ldec_ValorTransadoBruto = ldec_ValorTransadoBruto;
            this.ldec_ValorTransadoNeto = ldec_ValorTransadoNeto;
            this.ldt_FchValor = ldt_FchValor;
            this.ldt_FchCancelacion = ldt_FchCancelacion;
            this.ldt_FchVencimiento = ldt_FchVencimiento;
            this.lstr_SistemaNegociacion = lstr_SistemaNegociacion;
            this.lstr_Estado = lstr_Estado;
            this.lstr_UsrCreacion = lstr_UsrCreacion;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaInterna\\CrearTituloReclasificado.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}