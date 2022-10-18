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
    public class clsModificarCobrosPagos : clsProcedimientoAlmacenado
    {
        #region parametros
        private string lstr_IdExpediente;
        private string lstr_IdSociedadGL;
        private Int32 lint_IdRes;
        
        private String lstr_Moneda;
        private Decimal ldec_TipoCambio;
        private Decimal ldec_Tbp;
        private Decimal ldec_Tiempo;
        private Decimal ldec_TipoCambioCierre;

        private Decimal? ldec_MontoPrincipal;
        private Decimal? ldec_MontoPrincipalColones;
        private Decimal? ldec_MontoPrincipalCierre;

        private Decimal? ldec_MontoIntereses;
        private Decimal? ldec_MontoInteresesColones;
        private Decimal? ldec_MontoInteresesCierre;

        private Decimal? ldec_ValorPresentePrincipal;
        private Decimal? ldec_ValorPresentePrincipalColones;
        private Decimal? ldec_ValorPresentePrincipalCierre;

        private Decimal? ldec_ValorPresenteIntereses;
        private Decimal? ldec_ValorPresenteInteresesColones;
        private Decimal? ldec_ValorPresenteInteresesCierre;

        private Decimal? ldec_Intereses;
        private Decimal? ldec_InteresesColones;
        private Decimal? ldec_InteresesCierre;

        private Decimal? ldec_InteresesMoratorios;
        private Decimal? ldec_InteresesMoratoriosColones;
        private Decimal? ldec_InteresesMoratoriosCierre;

        private Decimal? ldec_Costas;
        private Decimal? ldec_CostasColones;
        private Decimal? ldec_CostasCierre;

        private Decimal? ldec_DanoMoral;
        private Decimal? ldec_DanoMoralColones;
        private Decimal? ldec_DanoMoralCierre;

        private Decimal? ldec_MontoPrincipalAnterior;
        private Decimal? ldec_MontoInteresesAnterior;
        private Decimal? ldec_InteresesAnterior;
        private Decimal? ldec_CostasAnterior;
        private Decimal? ldec_InteresesMoratoriosAnterior;
        private Decimal? ldec_DanoMoralAnterior;

        private string lstr_UsrModifica;
        private string lstr_EstadoProcesal;
        #endregion

        #region constructores
        public clsModificarCobrosPagos() { }

        public string Lstr_IdExpediente
        {
            get { return lstr_IdExpediente; }
            set { lstr_IdExpediente = value; }
        }
        public string Lstr_IdSociedadGL
        {
            get { return lstr_IdSociedadGL; }
            set { lstr_IdSociedadGL = value; }
        }
        public Int32 Lint_IdRes
        {
            get { return lint_IdRes; }
            set { lint_IdRes = value; }
        }

        public string Lstr_Moneda
        {
            get { return lstr_Moneda; }
            set { lstr_Moneda = value; }
        }
        public decimal Ldec_TipoCambio
        {
            get { return ldec_TipoCambio; }
            set { ldec_TipoCambio = value; }
        }
        public decimal Ldec_Tbp
        {
            get { return ldec_Tbp; }
            set { ldec_Tbp = value; }
        }
        public decimal Ldec_Tiempo
        {
            get { return ldec_Tiempo; }
            set { ldec_Tiempo = value; }
        }
        public decimal Ldec_TipoCambioCierre
        {
            get { return ldec_TipoCambioCierre; }
            set { ldec_TipoCambioCierre = value; }
        }

        public Decimal? Ldec_MontoPrincipal
        {
            get { return ldec_MontoPrincipal; }
            set { ldec_MontoPrincipal = value; }
        }
        public Decimal? Ldec_MontoPrincipalColones
        {
            get { return ldec_MontoPrincipalColones; }
            set { ldec_MontoPrincipalColones = value; }
        }
        public Decimal? Ldec_MontoPrincipalCierre
        {
            get { return ldec_MontoPrincipalCierre; }
            set { ldec_MontoPrincipalCierre = value; }
        }

        public Decimal? Ldec_MontoIntereses
        {
            get { return ldec_MontoIntereses; }
            set { ldec_MontoIntereses = value; }
        }
        public Decimal? Ldec_MontoInteresesColones
        {
            get { return ldec_MontoInteresesColones; }
            set { ldec_MontoInteresesColones = value; }
        }
        public Decimal? Ldec_MontoInteresesCierre
        {
            get { return ldec_MontoInteresesCierre; }
            set { ldec_MontoInteresesCierre = value; }
        }

        public Decimal? Ldec_ValorPresentePrincipal
        {
            get { return ldec_ValorPresentePrincipal; }
            set { ldec_ValorPresentePrincipal = value; }
        }
        public Decimal? Ldec_ValorPresentePrincipalColones
        {
            get { return ldec_ValorPresentePrincipalColones; }
            set { ldec_ValorPresentePrincipalColones = value; }
        }
        public Decimal? Ldec_ValorPresentePrincipalCierre
        {
            get { return ldec_ValorPresentePrincipalCierre; }
            set { ldec_ValorPresentePrincipalCierre = value; }
        }

        public Decimal? Ldec_ValorPresenteIntereses
        {
            get { return ldec_ValorPresenteIntereses; }
            set { ldec_ValorPresenteIntereses = value; }
        }
        public Decimal? Ldec_ValorPresenteInteresesColones
        {
            get { return ldec_ValorPresenteInteresesColones; }
            set { ldec_ValorPresenteInteresesColones = value; }
        }
        public Decimal? Ldec_ValorPresenteInteresesCierre
        {
            get { return ldec_ValorPresenteInteresesCierre; }
            set { ldec_ValorPresenteInteresesCierre = value; }
        }

        public Decimal? Ldec_Intereses
        {
            get { return ldec_Intereses; }
            set { ldec_Intereses = value; }
        }
        public Decimal? Ldec_InteresesColones
        {
            get { return ldec_InteresesColones; }
            set { ldec_InteresesColones = value; }
        }
        public Decimal? Ldec_InteresesCierre
        {
            get { return ldec_InteresesCierre; }
            set { ldec_InteresesCierre = value; }
        }

        public Decimal? Ldec_InteresesMoratorios
        {
            get { return ldec_InteresesMoratorios; }
            set { ldec_InteresesMoratorios = value; }
        }
        public Decimal? Ldec_InteresesMoratoriosColones
        {
            get { return ldec_InteresesMoratoriosColones; }
            set { ldec_InteresesMoratoriosColones = value; }
        }
        public Decimal? Ldec_InteresesMoratoriosCierre
        {
            get { return ldec_InteresesMoratoriosCierre; }
            set { ldec_InteresesMoratoriosCierre = value; }
        }

        public Decimal? Ldec_Costas
        {
            get { return ldec_Costas; }
            set { ldec_Costas = value; }
        }
        public Decimal? Ldec_CostasColones
        {
            get { return ldec_CostasColones; }
            set { ldec_CostasColones = value; }
        }
        public Decimal? Ldec_CostasCierre
        {
            get { return ldec_CostasCierre; }
            set { ldec_CostasCierre = value; }
        }

        public Decimal? Ldec_DanoMoral
        {
            get { return ldec_DanoMoral; }
            set { ldec_DanoMoral = value; }
        }
        public Decimal? Ldec_DanoMoralColones
        {
            get { return ldec_DanoMoralColones; }
            set { ldec_DanoMoralColones = value; }
        }
        public Decimal? Ldec_DanoMoralCierre
        {
            get { return ldec_DanoMoralCierre; }
            set { ldec_DanoMoralCierre = value; }
        }

        public string Lstr_UsrModifica
        {
            get { return lstr_UsrModifica; }
            set { lstr_UsrModifica = value; }
        }
        public string Lstr_EstadoProcesal
        {
            get { return lstr_EstadoProcesal; }
            set { lstr_EstadoProcesal = value; }
        }


        public Decimal? Ldec_MontoPrincipalAnterior
        {
            get { return ldec_MontoPrincipalAnterior; }
            set { ldec_MontoPrincipalAnterior = value; }
        }

        public Decimal? Ldec_MontoInteresesAnterior
        {
            get { return ldec_MontoInteresesAnterior; }
            set { ldec_MontoInteresesAnterior = value; }
        }

        public Decimal? Ldec_InteresesAnterior
        {
            get { return ldec_InteresesAnterior; }
            set { ldec_InteresesAnterior = value; }
        }
         public Decimal? Ldec_CostasAnterior
        {
            get { return ldec_CostasAnterior; }
            set { ldec_CostasAnterior = value; }
        }
         public Decimal? Ldec_InteresesMoratoriosAnterior
        {
            get { return ldec_InteresesMoratoriosAnterior; }
            set { ldec_InteresesMoratoriosAnterior = value; }
        }
         public Decimal? Ldec_DanoMoralAnterior
        {
            get { return ldec_DanoMoralAnterior; }
            set { ldec_DanoMoralAnterior = value; }
        }
        #endregion

        public bool ModificarCobrosPagos()
        {
            string str_RutaArchivo = string.Empty;
            string str_DireccionConfigs = string.Empty;
            bool resultFlag = false;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                str_DireccionConfigs = appSettings["DireccionConfigs"];
                str_RutaArchivo = str_DireccionConfigs + "\\Contigentes\\CobrosPagos\\ModificarCobrosPagos.config";
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