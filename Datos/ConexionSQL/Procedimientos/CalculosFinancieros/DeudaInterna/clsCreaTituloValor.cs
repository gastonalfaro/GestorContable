using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaInterna
{
    public class clsCreaTituloValor : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private int lint_NumValor;
        private int lint_NumCupon;
        private string lstr_EstadoValor;
        private string lstr_Nemotecnico;
        private string lstr_Tipo;
        private string lstr_TipoNegociacion;
        private string lstr_Moneda;
        private decimal ldec_ValorFacial;
        private DateTime ldt_FchValor;
        private string lstr_PlazoValor;
        private DateTime ldt_FchCancelacion;
        private DateTime ldt_FchVencimiento;
        private DateTime ldt_FchConstitucion;
        private decimal ldec_ValorTransadoBruto;
        private decimal ldec_ValorTransadoNeto;
        private decimal ldec_TasaBruta;
        private decimal ldec_TasaNeta;
        private decimal ldec_Margen;
        private string lint_NumEmisionSerie;
        private DateTime ldt_FchCreacionT;
        private DateTime ldt_FchInicio;
        private string lstr_Propietario;
        private string lstr_EntidadCustodia;
        private string lstr_SistemaNegociacion;
        private string lstr_MotivoAnulacion;
        private decimal ldec_RendimientoPorDescuento;
        private decimal ldec_InteresBruto;
        private decimal ldec_InteresBrutoEfectivo;
        private decimal ldec_InteresNeto;
        private decimal ldec_InteresNetoAcumulado;
        private decimal ldec_ImpuestoVencido;
        private decimal ldec_ImpuestoEfectivo;
        private decimal ldec_ImpuestoPagado;
        private decimal ldec_Premio;
        private string lstr_ModuloSINPE;
        private string lstr_IndicadorGarantia;
        private string lstr_IndicadorCupon;
        private string lstr_Origen;
        private string lstr_Estado;
        private string lstr_UsrCreacion;
        private string lstr_DescripcionNegociacion;
        private string lstr_NumeroIdentificacion;
        private string lstr_TipoIdentificacion;


        #endregion

        #region Obtención y asignación

        public int Lint_NumValor { get { return lint_NumValor; } set { lint_NumValor = value; } }
        public int Lint_NumCupon { get { return lint_NumCupon; } set { lint_NumCupon = value; } }
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
        public DateTime Ldt_FchConstitucion { get { return ldt_FchConstitucion; } set { ldt_FchConstitucion = value; } }
        public decimal Ldec_ValorTransadoBruto { get { return ldec_ValorTransadoBruto; } set { ldec_ValorTransadoBruto = value; } }
        public decimal Ldec_ValorTransadoNeto { get { return ldec_ValorTransadoNeto; } set { ldec_ValorTransadoNeto = value; } }
        public decimal Ldec_TasaBruta { get { return ldec_TasaBruta; } set { ldec_TasaBruta = value; } }
        public decimal Ldec_TasaNeta { get { return ldec_TasaNeta; } set { ldec_TasaNeta = value; } }
        public decimal Ldec_Margen { get { return ldec_Margen; } set { ldec_Margen = value; } }
        public string Lint_NumEmisionSerie { get { return lint_NumEmisionSerie; } set { lint_NumEmisionSerie = value; } }
        public DateTime Ldt_FchCreacionT { get { return ldt_FchCreacionT; } set { ldt_FchCreacionT = value; } }
        public DateTime Ldt_FchInicio { get { return ldt_FchInicio; } set { ldt_FchInicio = value; } }
        public string Lstr_Propietario { get { return lstr_Propietario; } set { lstr_Propietario = value; } }
        public string Lstr_EntidadCustodia { get { return lstr_EntidadCustodia; } set { lstr_EntidadCustodia = value; } }
        public string Lstr_SistemaNegociacion { get { return lstr_SistemaNegociacion; } set { lstr_SistemaNegociacion = value; } }
        public string Lstr_MotivoAnulacion { get { return lstr_MotivoAnulacion; } set { lstr_MotivoAnulacion = value; } }
        public decimal Ldec_RendimientoPorDescuento { get { return ldec_RendimientoPorDescuento; } set { ldec_RendimientoPorDescuento = value; } }
        public decimal Ldec_InteresBruto { get { return ldec_InteresBruto; } set { ldec_InteresBruto = value; } }
        public decimal Ldec_InteresBrutoEfectivo { get { return ldec_InteresBrutoEfectivo; } set { ldec_InteresBrutoEfectivo = value; } }
        public decimal Ldec_InteresNeto { get { return ldec_InteresNeto; } set { ldec_InteresNeto = value; } }
        public decimal Ldec_InteresNetoAcumulado { get { return ldec_InteresNetoAcumulado; } set { ldec_InteresNetoAcumulado = value; } }
        public decimal Ldec_ImpuestoVencido { get { return ldec_ImpuestoVencido; } set { ldec_ImpuestoVencido = value; } }
        public decimal Ldec_ImpuestoEfectivo { get { return ldec_ImpuestoEfectivo; } set { ldec_ImpuestoEfectivo = value; } }
        public decimal Ldec_ImpuestoPagado { get { return ldec_ImpuestoPagado; } set { ldec_ImpuestoPagado = value; } }
        public decimal Ldec_Premio { get { return ldec_Premio; } set { ldec_Premio = value; } }
        public string Lstr_ModuloSINPE { get { return lstr_ModuloSINPE; } set { lstr_ModuloSINPE = value; } }
        public string Lstr_IndicadorGarantia { get { return lstr_IndicadorGarantia; } set { lstr_IndicadorGarantia = value; } }
        public string Lstr_IndicadorCupon { get { return lstr_IndicadorCupon; } set { lstr_IndicadorCupon = value; } }
        public string Lstr_Origen { get { return lstr_Origen; } set { lstr_Origen = value; } }
        public string Lstr_Estado { get { return lstr_Estado; } set { lstr_Estado = value; } }
        public string Lstr_UsrCreacion { get { return lstr_UsrCreacion; } set { lstr_UsrCreacion = value; } }

        public string Lstr_DescripcionNegociacion
        {
            get
            {
                return lstr_DescripcionNegociacion;
            }

            set
            {
                lstr_DescripcionNegociacion = value;
            }
        }

        public string Lstr_NumeroIdentificacion
        {
            get
            {
                return lstr_NumeroIdentificacion;
            }

            set
            {
                lstr_NumeroIdentificacion = value;
            }
        }

        public string Lstr_TipoIdentificacion
        {
            get
            {
                return lstr_TipoIdentificacion;
            }

            set
            {
                lstr_TipoIdentificacion = value;
            }
        }


        #endregion

        #region Constructor

        public clsCreaTituloValor(int lint_NumValor, int lint_NumCupon, string lstr_EstadoValor, string lstr_Nemotecnico, string lstr_Tipo, string lstr_TipoNegociacion,
            string lstr_Moneda, decimal ldec_ValorFacial, DateTime ldt_FchValor, string lstr_PlazoValor, DateTime ldt_FchCancelacion,
            DateTime ldt_FchVencimiento, DateTime ldt_FchConstitucion, decimal ldec_ValorTransadoBruto, decimal ldec_ValorTransadoNeto,
            decimal ldec_TasaBruta, decimal ldec_TasaNeta, decimal ldec_Margen, string lint_NumEmisionSerie, DateTime ldt_FchCreacionT, DateTime ldt_FchInicio,
            string lstr_Propietario, string lstr_EntidadCustodia, string lstr_SistemaNegociacion, string lstr_MotivoAnulacion, decimal ldec_RendimientoPorDescuento,
            decimal ldec_InteresBruto, decimal ldec_InteresBrutoEfectivo, decimal ldec_InteresNeto, decimal ldec_InteresNetoAcumulado,
            decimal ldec_ImpuestoVencido, decimal ldec_ImpuestoEfectivo, decimal ldec_ImpuestoPagado, decimal ldec_Premio, string lstr_ModuloSINPE,
            string lstr_IndicadorGarantia, string lstr_IndicadorCupon, string lstr_Origen, string lstr_Estado, string lstr_UsrCreacion, string lstr_DescripcionNegociacion, string lstr_NumeroIdentificacion, string lstr_TipoIdentificacion)
        {
            this.lint_NumValor = lint_NumValor;
            this.lint_NumCupon = lint_NumCupon;
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
            this.ldt_FchConstitucion = ldt_FchConstitucion;
            this.ldec_ValorTransadoBruto = ldec_ValorTransadoBruto;
            this.ldec_ValorTransadoNeto = ldec_ValorTransadoNeto;
            this.ldec_TasaBruta = ldec_TasaBruta;
            this.ldec_TasaNeta = ldec_TasaNeta;
            this.ldec_Margen = ldec_Margen;
            this.lint_NumEmisionSerie = lint_NumEmisionSerie;
            this.ldt_FchCreacionT = ldt_FchCreacionT;
            this.ldt_FchInicio = ldt_FchInicio;
            this.lstr_Propietario = lstr_Propietario;
            this.lstr_EntidadCustodia = lstr_EntidadCustodia;
            this.lstr_SistemaNegociacion = lstr_SistemaNegociacion;
            this.lstr_MotivoAnulacion = lstr_MotivoAnulacion;
            this.ldec_RendimientoPorDescuento = ldec_RendimientoPorDescuento;
            this.ldec_InteresBruto = ldec_InteresBruto;
            this.ldec_InteresBrutoEfectivo = ldec_InteresBrutoEfectivo;
            this.ldec_InteresNeto = ldec_InteresNeto;
            this.ldec_InteresNetoAcumulado = ldec_InteresNetoAcumulado;
            this.ldec_ImpuestoVencido = ldec_ImpuestoVencido;
            this.ldec_ImpuestoEfectivo = ldec_ImpuestoEfectivo;
            this.ldec_ImpuestoPagado = ldec_ImpuestoPagado;
            this.ldec_Premio = ldec_Premio;
            this.lstr_ModuloSINPE = lstr_ModuloSINPE;
            this.lstr_IndicadorGarantia = lstr_IndicadorGarantia;
            this.lstr_IndicadorCupon = lstr_IndicadorCupon;
            this.lstr_Origen = lstr_Origen;
            this.lstr_Estado = lstr_Estado;
            this.lstr_UsrCreacion = lstr_UsrCreacion;
            this.lstr_DescripcionNegociacion = lstr_DescripcionNegociacion;
            this.lstr_NumeroIdentificacion = lstr_NumeroIdentificacion;
            this.Lstr_TipoIdentificacion = lstr_NumeroIdentificacion;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaInterna\\CrearTituloValor.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}