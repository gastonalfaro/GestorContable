using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Contigentes
{
    public class clsRegistrarCobroPago : clsProcedimientoAlmacenado
    {
         #region parametros

        private int lint_IdCobroPago;//Identificador único de los registros de cobro y pago
        private string lstr_IdResolucion;//Identificador para ligar los pagos a las resoluciones dictadas
        private string lstr_IdExpediente;

        private string lstr_Moneda;//La moneda en la cual se recibe el cobro. Campo obligatorio
        private decimal ldec_TipoCambio;//El tipo de cambio al momento de incluirlo en el sistema.
        private decimal ldec_TipoCambioCierre;
        private decimal ldec_Tbp;
        private decimal ldec_Tiempo;

        private decimal ldec_MontoPrincipal;//Es el monto principal a cobrar/pagar
        private decimal ldec_MontoIntereses;//Es el monto de intereses a cobrar/pagar
        private decimal ldec_Intereses;//Es el monto de intereses a cobrar/pagar
        private decimal ldec_InteresesMoratorios;//Intereses causados por la moratoria
        private decimal ldec_Costas;//Costas
        private decimal ldec_DanoMoral;//Daños morales

        private decimal ldec_ValorPresentePrincipal;//Valor presente del monto principal
        private decimal ldec_ValorPresenteIntereses;//Valor presente del monto de intereses

        private decimal ldec_MontoPrincipalColones;//Monto principal a cobrar/pagar en colones
        private decimal ldec_MontoInteresesColones;//Monto de interese a cobrar/pagar en colones
        private decimal ldec_InteresesColones;
        private decimal ldec_InteresesMoratoriosColones;//Interese moratorios en colones
        private decimal ldec_CostasColones;//Costas en colones
        private decimal ldec_DanoMoralColones;//Daño moral en colones

        private decimal ldec_ValorPresentePrinColones;//Valor presente del monto principal en colones
        private decimal ldec_ValorPresenteIntColones;//Valor presente del monto de intereses en colones

        private decimal ldec_MontoPrincipalCierre;//Monto principal a cobrar/pagar en colones
        private decimal ldec_MontoInteresesCierre;//Monto de interese a cobrar/pagar en colones
        private decimal ldec_InteresesCierre;
        private decimal ldec_InteresesMoratoriosCierre;//Interese moratorios en colones
        private decimal ldec_CostasCierre;//Costas en colones
        private decimal ldec_DanoMoralCierre;//Daño moral en colones

        private decimal ldec_ValorPresentePrinCierre;//Valor presente del monto principal en colones
        private decimal ldec_ValorPresenteIntCierre;//Valor presente del monto de intereses en colones

        private Decimal? ldec_MontoPrincipalAnterior;
        private Decimal? ldec_MontoInteresesAnterior;
        private Decimal? ldec_InteresesAnterior;
        private Decimal? ldec_CostasAnterior;
        private Decimal? ldec_InteresesMoratoriosAnterior;
        private Decimal? ldec_DanoMoralAnterior;

        private int lint_EstadoTransaccion;//Indica el estado de la transacción, si aún sigue en cobro, o bien si ya se cerró.
        private string lstr_TipoTransaccion;//Es el tipo de transacción que se está realizando, si es un cobro o un pago. Va ligado a la resolución, y al expediente en si, este campo es meramente informativo
        private DateTime lstr_FechFalloResol;

       
        private string lstr_UsrCreacion;//Campo de Auditoría: Indica el usuario que creó el registro
        private string lstr_UsrModifica;//Campo de Auditoría: Indica el último usuario que modificó el registro


        #endregion

        #region constructores
        public clsRegistrarCobroPago() { }
        
        #endregion

        #region obtención y asignación
        public string Lstr_IdExpediente
        {
            get { return lstr_IdExpediente; }
            set { lstr_IdExpediente = value; }
        }
        public DateTime Lstr_FechFalloResol
        {
            get { return lstr_FechFalloResol; }
            set { lstr_FechFalloResol = value; }
        }
        public int Lint_IdCobroPago
        {
            get { return lint_IdCobroPago; }
            set { lint_IdCobroPago = value; }
        }
        public string Lstr_IdResolucion
        {
            get { return lstr_IdResolucion; }
            set { lstr_IdResolucion = value; }
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
        public decimal Ldec_TipoCambioCierre
        {
            get { return ldec_TipoCambioCierre; }
            set { ldec_TipoCambioCierre = value; }
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

        #region Monto
        public decimal Ldec_MontoPrincipal
        {
            get { return ldec_MontoPrincipal; }
            set { ldec_MontoPrincipal = value; }
        }
        public decimal Ldec_MontoIntereses
        {
            get { return ldec_MontoIntereses; }
            set { ldec_MontoIntereses = value; }
        }
        public decimal Ldec_Intereses
        {
            get { return ldec_Intereses; }
            set { ldec_Intereses = value; }
        }
        public decimal Ldec_InteresesMoratorios
        {
            get { return ldec_InteresesMoratorios; }
            set { ldec_InteresesMoratorios = value; }
        }
        public decimal Ldec_Costas
        {
            get { return ldec_Costas; }
            set { ldec_Costas = value; }
        }
        public decimal Ldec_DanoMoral
        {
            get { return ldec_DanoMoral; }
            set { ldec_DanoMoral = value; }
        }
        public decimal Ldec_ValorPresentePrincipal
        {
            get { return ldec_ValorPresentePrincipal; }
            set { ldec_ValorPresentePrincipal = value; }
        }
        public decimal Ldec_ValorPresenteIntereses
        {
            get { return ldec_ValorPresenteIntereses; }
            set { ldec_ValorPresenteIntereses = value; }
        }
        #endregion

        #region MontoColones
        public decimal Ldec_MontoPrincipalColones
        {
            get { return ldec_MontoPrincipalColones; }
            set { ldec_MontoPrincipalColones = value; }
        }
        public decimal Ldec_MontoInteresesColones
        {
            get { return ldec_MontoInteresesColones; }
            set { ldec_MontoInteresesColones = value; }
        }
        public decimal Ldec_InteresesColones
        {
            get { return ldec_InteresesColones; }
            set { ldec_InteresesColones = value; }
        }
        public decimal Ldec_InteresesMoratoriosColones
        {
            get { return ldec_InteresesMoratoriosColones; }
            set { ldec_InteresesMoratoriosColones = value; }
        }
        public decimal Ldec_CostasColones
        {
            get { return ldec_CostasColones; }
            set { ldec_CostasColones = value; }
        }
        public decimal Ldec_DanoMoralColones
        {
            get { return ldec_DanoMoralColones; }
            set { ldec_DanoMoralColones = value; }
        }
        public decimal Ldec_ValorPresentePrinColones
        {
            get { return ldec_ValorPresentePrinColones; }
            set { ldec_ValorPresentePrinColones = value; }
        }
        public decimal Ldec_ValorPresenteIntColones
        {
            get { return ldec_ValorPresenteIntColones; }
            set { ldec_ValorPresenteIntColones = value; }
        }
        #endregion

        #region Monto Cierre
        public decimal Ldec_MontoPrincipalCierre
        {
            get { return ldec_MontoPrincipalCierre; }
            set { ldec_MontoPrincipalCierre = value; }
        }
        public decimal Ldec_MontoInteresesCierre
        {
            get { return ldec_MontoInteresesCierre; }
            set { ldec_MontoInteresesCierre = value; }
        }
        public decimal Ldec_InteresesCierre
        {
            get { return ldec_InteresesCierre; }
            set { ldec_InteresesCierre = value; }
        }
        public decimal Ldec_InteresesMoratoriosCierre
        {
            get { return ldec_InteresesMoratoriosCierre; }
            set { ldec_InteresesMoratoriosCierre = value; }
        }
        public decimal Ldec_CostasCierre
        {
            get { return ldec_CostasCierre; }
            set { ldec_CostasCierre = value; }
        }
        public decimal Ldec_DanoMoralCierre
        {
            get { return ldec_DanoMoralCierre; }
            set { ldec_DanoMoralCierre = value; }
        }
        public decimal Ldec_ValorPresentePrinCierre
        {
            get { return ldec_ValorPresentePrinCierre; }
            set { ldec_ValorPresentePrinCierre = value; }
        }
        public decimal Ldec_ValorPresenteIntCierre
        {
            get { return ldec_ValorPresenteIntCierre; }
            set { ldec_ValorPresenteIntCierre = value; }
        }
        #endregion

        #region Monto Anterior
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

        public string Lstr_TipoTransaccion
        {
            get { return lstr_TipoTransaccion; }
            set { lstr_TipoTransaccion = value; }
        }
        public int Lint_EstadoTransaccion
        {
            get { return lint_EstadoTransaccion; }
            set { lint_EstadoTransaccion = value; }
        }

        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }
        public string Lstr_UsrModifica
        {
            get { return lstr_UsrModifica; }
            set { lstr_UsrModifica = value; }
        }

        #endregion

        #region metodos 
        /// <summary>
        /// Resgistrar Cobros Pagos de Resoluciones Configs
        /// </summary>
        /// <returns></returns>
        public bool ProcesarSPRegistrarCobroPago()
        {
            
            string str_RutaArchivo=string.Empty;
            string str_DireccionConfigs = string.Empty;
            bool resultFlag=false;
            try { 
                  
                var appSettings = ConfigurationManager.AppSettings;
                str_DireccionConfigs = appSettings["DireccionConfigs"];
                str_RutaArchivo = str_DireccionConfigs+"\\Contigentes\\CobrosPagos\\RegistrarCobroPago.config";
                EjecucionSP(str_RutaArchivo, this);
                resultFlag = true;
            }

            catch (Exception ex)
            {
                resultFlag = false;
            }

            return resultFlag;
        }

      #endregion
    }
}