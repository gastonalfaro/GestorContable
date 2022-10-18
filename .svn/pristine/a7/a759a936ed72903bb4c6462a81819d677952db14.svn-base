using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using log4net.Config;
using System.Configuration;
using System.Data;

namespace Datos.ConexionSQL.Procedimientos.Contigentes
{
    public class clsModificarResolucion : clsProcedimientoAlmacenado
    {
        #region parametros
        private Int32? lint_IdRes;
        public Int32? Lint_IdRes
        {
            get { return lint_IdRes; }
            set { lint_IdRes = value; }
        }
        private Int32? lint_IdCobroPagoResolucion;
        public Int32? Lint_IdCobroPagoResolucion
        {
            get { return lint_IdCobroPagoResolucion; }
            set { lint_IdCobroPagoResolucion = value; }
        }

        private string lstr_IdResolucion;
        public string Lstr_IdResolucion
        {
            get { return lstr_IdResolucion; }
            set { lstr_IdResolucion = value; }
        }
        
        private string lstr_IdExpediente;        
        public string Lstr_IdExpediente
        {
            get { return lstr_IdExpediente; }
            set { lstr_IdExpediente = value; }
        }

        private string lstr_IdSociedadGL;
        public string Lstr_IdSociedadGL
        {
            get { return lstr_IdSociedadGL; }
            set { lstr_IdSociedadGL = value; }
        }

        private string lstr_EstadoResolucion;
        public string Lstr_EstadoResolucion
        {
            get { return lstr_EstadoResolucion; }
            set { lstr_EstadoResolucion = value; }
        }
        private DateTime? ldt_FechaResolucion;
        public DateTime? Ldt_FechaResolucion
        {
            get { return ldt_FechaResolucion; }
            set { ldt_FechaResolucion = value; }
        }
        private DateTime? ldt_PosibleFecSalida;
        public DateTime? Ldt_PosibleFecSalida
        {
            get { return ldt_PosibleFecSalida; }
            set { ldt_PosibleFecSalida = value; }
        }

        private Decimal? ldec_MontoPosibleReembolso;
        public Decimal? Ldec_MontoPosibleReembolso
        {
            get { return ldec_MontoPosibleReembolso; }
            set { ldec_MontoPosibleReembolso = value; }
        }
        private Decimal? ldec_MontoPosReemColones;
        public Decimal? Ldec_MontoPosReemColones
        {
            get { return ldec_MontoPosReemColones; }
            set { ldec_MontoPosReemColones = value; }
        }

        private string lstr_Observacion;
        public string Lstr_Observacion
        {
            get { return lstr_Observacion; }
            set { lstr_Observacion = value; }
        }
        private Int32? lint_CxCaCxP;
        public Int32? Lint_CxCaCxP
        {
            get { return lint_CxCaCxP; }
            set { lint_CxCaCxP = value; }
        }

        private String lstr_Moneda;
        public String Lstr_Moneda
        {
            get { return lstr_Moneda; }
            set { lstr_Moneda = value; }
        }


        private Decimal? ldec_TipoCambio;
        public Decimal? Ldec_TipoCambio
        {
            get { return ldec_TipoCambio; }
            set { ldec_TipoCambio = value; }
        }
        private Decimal? ldec_Tbp;
        public Decimal? Ldec_Tbp
        {
            get { return ldec_Tbp; }
            set { ldec_Tbp = value; }
        }
        private Decimal? ldec_Tiempo;
        public Decimal? Ldec_Tiempo
        {
            get { return ldec_Tiempo; }
            set { ldec_Tiempo = value; }
        }
        //private Decimal ldec_TipoCambioCierre;

        private Decimal? ldec_MontoPrincipal;
        public Decimal? Ldec_MontoPrincipal
        {
            get { return ldec_MontoPrincipal; }
            set { ldec_MontoPrincipal = value; }
        }
        private Decimal? ldec_MontoIntereses;
        public Decimal? Ldec_MontoIntereses
        {
            get { return ldec_MontoIntereses; }
            set { ldec_MontoIntereses = value; }
        }
        private Decimal? ldec_InteresesMoratorios;
        public Decimal? Ldec_InteresesMoratorios
        {
            get { return ldec_InteresesMoratorios; }
            set { ldec_InteresesMoratorios = value; }
        }
        private Decimal? ldec_Costas;
        public Decimal? Ldec_Costas
        {
            get { return ldec_Costas; }
            set { ldec_Costas = value; }
        }
        private Decimal? ldec_DanoMoral;
        public Decimal? Ldec_DanoMoral
        {
            get { return ldec_DanoMoral; }
            set { ldec_DanoMoral = value; }
        }
        private Decimal? ldec_ValorPresentePrincipal;
        public Decimal? Ldec_ValorPresentePrincipal
        {
            get { return ldec_ValorPresentePrincipal; }
            set { ldec_ValorPresentePrincipal = value; }
        }
        private Decimal? ldec_ValorPresenteIntereses;
        public Decimal? Ldec_ValorPresenteIntereses
        {
            get { return ldec_ValorPresenteIntereses; }
            set { ldec_ValorPresenteIntereses = value; }
        }
        private Decimal? ldec_MontoPrincipalColones;
        public Decimal? Ldec_MontoPrincipalColones
        {
            get { return ldec_MontoPrincipalColones; }
            set { ldec_MontoPrincipalColones = value; }
        }
        private Decimal? ldec_MontoInteresesColones;
        public Decimal? Ldec_MontoInteresesColones
        {
            get { return ldec_MontoInteresesColones; }
            set { ldec_MontoInteresesColones = value; }
        }
        private Decimal? ldec_InteresesMoratoriosColones;
        public Decimal? Ldec_InteresesMoratoriosColones
        {
            get { return ldec_InteresesMoratoriosColones; }
            set { ldec_InteresesMoratoriosColones = value; }
        }
        private Decimal? ldec_CostasColones;
        public Decimal? Ldec_CostasColones
        {
            get { return ldec_CostasColones; }
            set { ldec_CostasColones = value; }
        }
        private Decimal? ldec_DanoMoralColones;
        public Decimal? Ldec_DanoMoralColones
        {
            get { return ldec_DanoMoralColones; }
            set { ldec_DanoMoralColones = value; }
        }
        private Decimal? ldec_ValorPresentePrinColones;
        public Decimal? Ldec_ValorPresentePrinColones
        {
            get { return ldec_ValorPresentePrinColones; }
            set { ldec_ValorPresentePrinColones = value; }
        }
        private Decimal? ldec_ValorPresenteIntColones;
        public Decimal? Ldec_ValorPresenteIntColones
        {
            get { return ldec_ValorPresenteIntColones; }
            set { ldec_ValorPresenteIntColones = value; }
        }

        private string lstr_TipoTransaccion;
        public String Lstr_TipoTransaccion
        {
            get { return lstr_TipoTransaccion; }
            set { lstr_TipoTransaccion = value; }
        }

        private string lstr_EstadoTransaccion;
        public String Lstr_EstadoTransaccion
        {
            get { return lstr_EstadoTransaccion; }
            set { lstr_EstadoTransaccion = value; }
        }
        private string lstr_EstadoProcesal;
        public string Lstr_EstadoProcesal
        {
            get { return lstr_EstadoProcesal; }
            set { lstr_EstadoProcesal = value; }
        }
        private Int32? lint_EstadoPretension;
        public Int32? Lint_EstadoPretension
        {
            get { return lint_EstadoPretension; }
            set { lint_EstadoPretension = value; }
        }
        private string lstr_Estado;
        public String Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }


        //private Decimal? ldec_MontoPrincipalCierre;

        //private Decimal? ldec_MontoInteresesCierre;

        //private Decimal? ldec_ValorPresentePrincipalCierre;

        //private Decimal? ldec_ValorPresenteInteresesCierre;

        //private Decimal? ldec_Intereses;
        //private Decimal? ldec_InteresesColones;
        //private Decimal? ldec_InteresesCierre;

        //private Decimal? ldec_InteresesMoratoriosCierre;

        //private Decimal? ldec_CostasCierre;

        //private Decimal? ldec_DanoMoralCierre;

        //private Decimal? ldec_MontoPrincipalAnterior;
        //private Decimal? ldec_MontoInteresesAnterior;
        //private Decimal? ldec_InteresesAnterior;
        //private Decimal? ldec_CostasAnterior;
        //private Decimal? ldec_InteresesMoratoriosAnterior;
        //private Decimal? ldec_DanoMoralAnterior;

        private string lstr_UsrModifica;
        public string Lstr_UsrModifica
        {
            get { return lstr_UsrModifica; }
            set { lstr_UsrModifica = value; }
        }
        #endregion

        #region constructores
        public clsModificarResolucion() { }



        //public decimal Ldec_TipoCambioCierre
        //{
        //    get { return ldec_TipoCambioCierre; }
        //    set { ldec_TipoCambioCierre = value; }
        //}

        //public Decimal? Ldec_MontoPrincipalCierre
        //{
        //    get { return ldec_MontoPrincipalCierre; }
        //    set { ldec_MontoPrincipalCierre = value; }
        //}

        //public Decimal? Ldec_MontoInteresesCierre
        //{
        //    get { return ldec_MontoInteresesCierre; }
        //    set { ldec_MontoInteresesCierre = value; }
        //}

        //public Decimal? Ldec_ValorPresentePrincipalCierre
        //{
        //    get { return ldec_ValorPresentePrincipalCierre; }
        //    set { ldec_ValorPresentePrincipalCierre = value; }
        //}

        //public Decimal? Ldec_ValorPresenteInteresesCierre
        //{
        //    get { return ldec_ValorPresenteInteresesCierre; }
        //    set { ldec_ValorPresenteInteresesCierre = value; }
        //}

        //public Decimal? Ldec_Intereses
        //{
        //    get { return ldec_Intereses; }
        //    set { ldec_Intereses = value; }
        //}
        //public Decimal? Ldec_InteresesColones
        //{
        //    get { return ldec_InteresesColones; }
        //    set { ldec_InteresesColones = value; }
        //}
        //public Decimal? Ldec_InteresesCierre
        //{
        //    get { return ldec_InteresesCierre; }
        //    set { ldec_InteresesCierre = value; }
        //}

        //public Decimal? Ldec_InteresesMoratoriosCierre
        //{
        //    get { return ldec_InteresesMoratoriosCierre; }
        //    set { ldec_InteresesMoratoriosCierre = value; }
        //}

        //public Decimal? Ldec_CostasCierre
        //{
        //    get { return ldec_CostasCierre; }
        //    set { ldec_CostasCierre = value; }
        //}

        //public Decimal? Ldec_DanoMoralCierre
        //{
        //    get { return ldec_DanoMoralCierre; }
        //    set { ldec_DanoMoralCierre = value; }
        //}



        //public Decimal? Ldec_MontoPrincipalAnterior
        //{
        //    get { return ldec_MontoPrincipalAnterior; }
        //    set { ldec_MontoPrincipalAnterior = value; }
        //}

        //public Decimal? Ldec_MontoInteresesAnterior
        //{
        //    get { return ldec_MontoInteresesAnterior; }
        //    set { ldec_MontoInteresesAnterior = value; }
        //}

        //public Decimal? Ldec_InteresesAnterior
        //{
        //    get { return ldec_InteresesAnterior; }
        //    set { ldec_InteresesAnterior = value; }
        //}
        //public Decimal? Ldec_CostasAnterior
        //{
        //    get { return ldec_CostasAnterior; }
        //    set { ldec_CostasAnterior = value; }
        //}
        //public Decimal? Ldec_InteresesMoratoriosAnterior
        //{
        //    get { return ldec_InteresesMoratoriosAnterior; }
        //    set { ldec_InteresesMoratoriosAnterior = value; }
        //}
        //public Decimal? Ldec_DanoMoralAnterior
        //{
        //    get { return ldec_DanoMoralAnterior; }
        //    set { ldec_DanoMoralAnterior = value; }
        //}
        #endregion

        public bool ModificarResolucion()
        {
            string str_RutaArchivo = string.Empty;
            string str_DireccionConfigs = string.Empty;
            bool resultFlag = false;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                str_DireccionConfigs = appSettings["DireccionConfigs"];
                str_RutaArchivo = str_DireccionConfigs + "\\Contigentes\\Resolucion\\ModificarResolucion.config";
                EjecucionSP(str_RutaArchivo, this);
                resultFlag = true;
            }

            catch (Exception ex)
            {
                resultFlag = false;
            }

            return resultFlag;
        }

    }
}