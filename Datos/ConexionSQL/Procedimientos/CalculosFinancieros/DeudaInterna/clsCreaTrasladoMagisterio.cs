using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaInterna
{
    public class clsCreaTrasladoMagisterio : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_EstadoValor;
        private string lstr_Nemotecnico;
        private string lstr_Tipo;
        private string lstr_TipoNegociacion;
        private int lint_NumValor;
        private string lstr_Moneda;
        private decimal ldec_ValorFacial;
        private DateTime ldt_FchValor;
        private string lstr_PlazoValor;
        private DateTime ldt_FchCancelacion;
        private DateTime ldt_FchVencimiento;
        private decimal ldec_ValorTransadoBruto;
        private decimal ldec_ValorTransadoNeto;
        private decimal ldec_TasaBruta;
        private decimal ldec_TasaNeta;
        private DateTime ldt_FchCreacionT;
        private string lstr_Propietario;
        private string lstr_SistemaNegociacion;
        private string lstr_MotivoAnulacion;
        private decimal ldec_RendimientoPorDescuento;
        private decimal ldec_ImpuestoPagado;
        private decimal ldec_Premio;
        private string lstr_Estado;
        private string lstr_ModuloSINPE;
        private string lstr_UsrCreacion;
        private string lstr_EntidadCustodia;


        #endregion

        #region Obtención y asignación

        public int Lint_NumValor { get { return lint_NumValor; } set { lint_NumValor = value; } }
        public string Lstr_EstadoValor { get { return lstr_EstadoValor; } set { lstr_EstadoValor = value; } }
        public string Lstr_Nemotecnico { get { return lstr_Nemotecnico; } set { lstr_Nemotecnico = value; } }
        public string Lstr_Tipo { get { return lstr_Tipo; } set { lstr_Tipo = value; } }
        public string Lstr_TipoNegociacion { get { return lstr_TipoNegociacion; } set { lstr_TipoNegociacion = value; } }
        public string Lstr_Moneda { get { return lstr_Moneda; } set { lstr_Moneda = value; } }
        public decimal Ldec_ValorFacial { get { return ldec_ValorFacial; } set { ldec_ValorFacial = value; } }
        public DateTime Ldt_FchValor { get { return ldt_FchValor; } set { ldt_FchValor = value; } }
        public string Lstr_PlazoValor { get { return lstr_PlazoValor; } set { lstr_PlazoValor = value; } }
        public DateTime Ldt_FchCancelacion { get { return ldt_FchCancelacion; } set { ldt_FchCancelacion = value; } }
        public DateTime Ldt_FchVencimiento { get { return ldt_FchVencimiento; } set { ldt_FchVencimiento = value; } }
        public decimal Ldec_ValorTransadoBruto { get { return ldec_ValorTransadoBruto; } set { ldec_ValorTransadoBruto = value; } }
        public decimal Ldec_ValorTransadoNeto { get { return ldec_ValorTransadoNeto; } set { ldec_ValorTransadoNeto = value; } }
        public decimal Ldec_TasaBruta { get { return ldec_TasaBruta; } set { ldec_TasaBruta = value; } }
        public decimal Ldec_TasaNeta { get { return ldec_TasaNeta; } set { ldec_TasaNeta = value; } }
        public DateTime Ldt_FchCreacionT { get { return ldt_FchCreacionT; } set { ldt_FchCreacionT = value; } }
        public string Lstr_Propietario { get { return lstr_Propietario; } set { lstr_Propietario = value; } }
        public string Lstr_SistemaNegociacion { get { return lstr_SistemaNegociacion; } set { lstr_SistemaNegociacion = value; } }
        public string Lstr_MotivoAnulacion { get { return lstr_MotivoAnulacion; } set { lstr_MotivoAnulacion = value; } }
        public decimal Ldec_RendimientoPorDescuento { get { return ldec_RendimientoPorDescuento; } set { ldec_RendimientoPorDescuento = value; } }
        public decimal Ldec_ImpuestoPagado { get { return ldec_ImpuestoPagado; } set { ldec_ImpuestoPagado = value; } }
        public decimal Ldec_Premio { get { return ldec_Premio; } set { ldec_Premio = value; } }
        public string Lstr_Estado { get { return lstr_Estado; } set { lstr_Estado = value; } }
        public string Lstr_ModuloSINPE { get { return lstr_ModuloSINPE; } set { lstr_ModuloSINPE = value; } }
        public string Lstr_UsrCreacion { get { return lstr_UsrCreacion; } set { lstr_UsrCreacion = value; } }
        public string Lstr_EntidadCustodia { get { return lstr_EntidadCustodia; } set { lstr_EntidadCustodia = value; } }

        #endregion 

        #region Constructor

        public clsCreaTrasladoMagisterio(string lstr_EstadoValor,string lstr_Nemotecnico, string lstr_Tipo, string lstr_TipoNegociacion,
            int lint_NumValor, string lstr_Moneda, decimal ldec_ValorFacial, DateTime ldt_FchValor, string lstr_PlazoValor, DateTime ldt_FchCancelacion,
            DateTime ldt_FchVencimiento, decimal ldec_ValorTransadoBruto, decimal ldec_ValorTransadoNeto, decimal ldec_TasaBruta,
            decimal ldec_TasaNeta, DateTime ldt_FchCreacionT, string lstr_Propietario, string lstr_SistemaNegociacion, string lstr_MotivoAnulacion,
            decimal ldec_RendimientoPorDescuento, decimal ldec_Premio, decimal ldec_ImpuestoPagado, string lstr_Estado, string lstr_ModuloSINPE, string lstr_UsrCreacion, string lstr_EntidadCustodia)
        {
            this.lint_NumValor = lint_NumValor;
            this.lstr_EstadoValor = lstr_EstadoValor;
            this.lstr_Nemotecnico = lstr_Nemotecnico;
            this.lstr_Tipo = lstr_Tipo;
            this.lstr_TipoNegociacion = lstr_TipoNegociacion;
            this.lstr_Moneda = lstr_Moneda;
            this.ldec_ValorFacial = ldec_ValorFacial;
            this.ldt_FchValor = ldt_FchValor;
            this.lstr_PlazoValor = lstr_PlazoValor;
            this.ldt_FchCancelacion = ldt_FchCancelacion;
            this.ldt_FchVencimiento = ldt_FchVencimiento;
            this.ldec_ValorTransadoBruto = ldec_ValorTransadoBruto;
            this.ldec_ValorTransadoNeto = ldec_ValorTransadoNeto;
            this.ldec_TasaBruta = ldec_TasaBruta;
            this.ldec_TasaNeta = ldec_TasaNeta;
            this.ldt_FchCreacionT = ldt_FchCreacionT;
            this.lstr_Propietario = lstr_Propietario;
            this.lstr_SistemaNegociacion = lstr_SistemaNegociacion;
            this.lstr_MotivoAnulacion = lstr_MotivoAnulacion;
            this.ldec_RendimientoPorDescuento = ldec_RendimientoPorDescuento;
            this.ldec_ImpuestoPagado = ldec_ImpuestoPagado;
            this.ldec_Premio = ldec_Premio;
            this.lstr_Estado = lstr_Estado;
            this.lstr_UsrCreacion = lstr_UsrCreacion;
            this.lstr_ModuloSINPE = lstr_ModuloSINPE;
            this.lstr_EntidadCustodia = lstr_EntidadCustodia;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaInterna\\CrearTrasladoMagisterio.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}