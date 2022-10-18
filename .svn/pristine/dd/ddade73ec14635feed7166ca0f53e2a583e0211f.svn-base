using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Datos.ConexionSQL.Procedimientos.Contigentes;
using log4net;
using log4net.Config;
using System.Configuration;
using System.Data;

//Log4Net inicializa en WebApplication
//[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace Datos.ConexionSQL.Procedimientos.Contigentes
{
    public class clsModificarCobrosPagosArchivo : clsProcedimientoAlmacenado
    {

        #region parametros

        private int? lint_IdRes;
        private int? lint_IdCobroPagoResolucion;
        private string lstr_IdResolucion;//Identificador único de la resolución dictada en los tribunales de justicia
        private string lstr_IdExpediente;//Llave que relaciona las resoluciones dictadas, con los expedientes existentes
        private string lstr_IdSociedadGL;
        private string lstr_EstadoResolucion;//Campo que define qué tipo de resolución es, Resolución Provisional 1 ó 2,  Resolución en Firme (aplica tanto para activos contingentes como para pasivos contingentes), o Declaración Sin Lugar.
        private string lstr_Moneda;//La moneda en la cual se recibe el cobro. Campo obligatorio
        private decimal? ldec_TipoCambio;//El tipo de cambio al momento de incluirlo en el sistema.
        private decimal? ldec_MontoPrincipal;//Es el monto principal a cobrar/pagar
        private decimal? ldec_MontoPrincipalColones;//Monto principal a cobrar/pagar en colones
        private decimal? ldec_Intereses;
        private decimal? ldec_InteresesColones;
        private decimal? ldec_InteresesMoratorios;
        private decimal? ldec_InteresesMoratoriosColones;
        private decimal? ldec_Costas;
        private decimal? ldec_CostasColones;
        private decimal? ldec_DanoMoral;
        private decimal? ldec_DanoMoralColones;
        private string lstr_UsrModifica;

        #endregion

        #region constructores
        public clsModificarCobrosPagosArchivo() { }


        public int? Lint_IdRes
        {
            get { return lint_IdRes; }
            set { lint_IdRes = value; }
        }
        public int? Lint_IdCobroPagoResolucion
        {
            get { return lint_IdCobroPagoResolucion; }
            set { lint_IdCobroPagoResolucion = value; }
        }
        public string Lstr_IdResolucion
        {
            get { return lstr_IdResolucion; }
            set { lstr_IdResolucion = value; }
        }
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

        public string Lstr_EstadoResolucion
        {
            get { return lstr_EstadoResolucion; }
            set { lstr_EstadoResolucion = value; }
        }

        public string Lstr_Moneda
        {
            get { return lstr_Moneda; }
            set { lstr_Moneda = value; }
        }

        public decimal? Ldec_TipoCambio
        {
            get { return ldec_TipoCambio; }
            set { ldec_TipoCambio = value; }
        }

        public decimal? Ldec_MontoPrincipal
        {
            get { return ldec_MontoPrincipal; }
            set { ldec_MontoPrincipal = value; }
        }

        public decimal? Ldec_MontoPrincipalColones
        {
            get { return ldec_MontoPrincipalColones; }
            set { ldec_MontoPrincipalColones = value; }
        }
        public decimal? Ldec_Intereses
        {
            get { return ldec_Intereses; }
            set { ldec_Intereses = value; }
        }
        public decimal? Ldec_InteresesColones
        {
            get { return ldec_InteresesColones; }
            set { ldec_InteresesColones = value; }
        }


        public decimal? Ldec_InteresesMoratorios
        {
            get { return ldec_InteresesMoratorios; }
            set { ldec_InteresesMoratorios = value; }
        }
        public decimal? Ldec_InteresesMoratoriosColones
        {
            get { return ldec_InteresesMoratoriosColones; }
            set { ldec_InteresesMoratoriosColones = value; }
        }

        public decimal? Ldec_Costas
        {
            get { return ldec_Costas; }
            set { ldec_Costas = value; }
        }
        public decimal? Ldec_CostasColones
        {
            get { return ldec_CostasColones; }
            set { ldec_CostasColones = value; }
        }

        public decimal? Ldec_DanoMoral
        {
            get { return ldec_DanoMoral; }
            set { ldec_DanoMoral = value; }
        }
        public decimal? Ldec_DanoMoralColones
        {
            get { return ldec_DanoMoralColones; }
            set { ldec_DanoMoralColones = value; }
        }

        public string Lstr_UsrModifica
        {
            get { return lstr_UsrModifica; }
            set { lstr_UsrModifica = value; }
        }


        #endregion

        public bool ModificarCobrosPagosArchivo(
         int? lint_IdRes,
         int? lint_IdCobroPagoResolucion,
         string lstr_IdResolucion,//Identificador único de la resolución dictada en los tribunales de justicia
         string lstr_IdExpediente,//Llave que relaciona las resoluciones dictadas, con los expedientes existentes
         string lstr_IdSociedadGL,
         string lstr_EstadoResolucion,//Campo que define qué tipo de resolución es, Resolución Provisional 1 ó 2,  Resolución en Firme (aplica tanto para activos contingentes como para pasivos contingentes), o Declaración Sin Lugar.
         string lstr_Moneda,//La moneda en la cual se recibe el cobro. Campo obligatorio
         decimal? ldec_TipoCambio,//El tipo de cambio al momento de incluirlo en el sistema.
         decimal? ldec_MontoPrincipal,//Es el monto principal a cobrar/pagar
         decimal? ldec_MontoPrincipalColones,//Monto principal a cobrar/pagar en colones
         decimal? ldec_Intereses,
         decimal? ldec_InteresesColones,
         decimal? ldec_InteresesMoratorios,
         decimal? ldec_InteresesMoratoriosColones,
         decimal? ldec_Costas,
         decimal? ldec_CostasColones,
         decimal? ldec_DanoMoral,
         decimal? ldec_DanoMoralColones,
         string lstr_UsrModifica)
        {
            this.lint_IdRes = lint_IdRes;
            this.lint_IdCobroPagoResolucion = lint_IdCobroPagoResolucion;
            this.lstr_IdResolucion = lstr_IdResolucion;//Identificador único de la resolución dictada en los tribunales de justicia
            this.lstr_IdExpediente =lstr_IdExpediente;//Llave que relaciona las resoluciones dictadas, con los expedientes existentes
            this.lstr_IdSociedadGL = lstr_IdSociedadGL ;
            this.lstr_EstadoResolucion =lstr_EstadoResolucion ;//Campo que define qué tipo de resolución es, Resolución Provisional 1 ó 2,  Resolución en Firme (aplica tanto para activos contingentes como para pasivos contingentes), o Declaración Sin Lugar.
            this.lstr_Moneda = lstr_Moneda ;//La moneda en la cual se recibe el cobro. Campo obligatorio
            this.ldec_TipoCambio = ldec_TipoCambio;//El tipo de cambio al momento de incluirlo en el sistema.
            this.ldec_MontoPrincipal =ldec_MontoPrincipal ;//Es el monto principal a cobrar/pagar
            this.ldec_MontoPrincipalColones = ldec_MontoPrincipalColones ;//Monto principal a cobrar/pagar en colones
            this.ldec_Intereses = ldec_Intereses;
            this.ldec_InteresesColones = ldec_InteresesColones ;
            this.ldec_InteresesMoratorios = ldec_InteresesMoratorios ;
            this.ldec_InteresesMoratoriosColones = ldec_InteresesMoratoriosColones ;
            this.ldec_Costas = ldec_Costas ;
            this.ldec_CostasColones =ldec_CostasColones ;
            this.ldec_DanoMoral = ldec_DanoMoral ;
            this.ldec_DanoMoralColones = ldec_DanoMoralColones;
            this.lstr_UsrModifica = lstr_UsrModifica;

            string str_RutaArchivo = string.Empty;
            string str_DireccionConfigs = string.Empty;
            bool resultFlag = false;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                str_DireccionConfigs = appSettings["DireccionConfigs"];
                str_RutaArchivo = str_DireccionConfigs + "\\Contigentes\\CobrosPagos\\ModificarCobrosPagosArchivo.config";
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