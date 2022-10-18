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
    public class clsRegistrarResolucion : clsProcedimientoAlmacenado
    {

        #region parametros
        private static readonly ILog llogger_Log = LogManager.GetLogger("clsRegistrarResolucion");//varibale de bitacoreo
        private int lint_IdRes;
        private int lint_IdCobroPagoResolucion;
        private string lstr_IdResolucion;//Identificador único de la resolución dictada en los tribunales de justicia
        private string lstr_IdExpedienteFK;//Llave que relaciona las resoluciones dictadas, con los expedientes existentes
        private string lstr_IdExpediente;//Llave que relaciona las resoluciones dictadas, con los expedientes existentes
        private string lstr_NumExpediente;
        private string lstr_IdSociedadGL;

        private string lstr_EstadoResolucion;//Campo que define qué tipo de resolución es, Resolución Provisional 1 ó 2,  Resolución en Firme (aplica tanto para activos contingentes como para pasivos contingentes), o Declaración Sin Lugar.
        private DateTime ldt_FechaResolucion;//Fecha en la que se dio el fallo campo 
        private DateTime? ldec_PosibleFecSalidaRec;//Posible fecha de la salida de los recursos (aplica para resoluciones con EstadoResolucion en Resolucion Provisional 1 ó 2).
        private decimal ldec_MontoPosibleReembolso;//Monto del posible reembolso en caso de que la resolución estipulara un monto menor al indicado previamente.
        private decimal ldec_MontoPosReemColones;//Monto en colones del posible reembolso en caso de que la resolución estipulara un monto menor al indicado previamente.
        private string lstr_Observacion;//Breve observación que justifica los hechos de la resolución.
        private int lint_CxCaCxP;//Definición de estatus del expediente, si es un activo contingente o pasivo contingente. Es un auxiliar del campo TipoExpediente del expediente, conserva el estado de la resolución anterior para consulta, y permite el cambio de estado del expediente.
        private string lstr_Estado;

        private string lstr_TipoExpediente;
        private DateTime? ldt_FechFalloResol;
        private string Observaciones;

        public DateTime? Ldt_FechFalloResol
        {
            get { return ldt_FechFalloResol; }
            set { ldt_FechFalloResol = value; }
        }
        public string Lstr_TipoExpediente
        {
            get { return lstr_TipoExpediente; }
            set { lstr_TipoExpediente = value; }
        }

        //CobrosPagos
        private int lint_IdCobroPago;//Identificador único de los registros de cobro y pago
        private string lstr_Moneda;//La moneda en la cual se recibe el cobro. Campo obligatorio
        private decimal ldec_TipoCambio;//El tipo de cambio al momento de incluirlo en el sistema.
        private Decimal ldec_Tbp;
        private Decimal ldec_Tiempo;
        private decimal ldec_TipoCambioCierre;

        private decimal ldec_MontoPrincipal;//Es el monto principal a cobrar/pagar
        private decimal ldec_MontoIntereses;//Es el monto de intereses a cobrar/pagar
        private decimal ldec_Intereses;
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

        
        private decimal ldec_MontoPrincipalCierre;
        private decimal ldec_MontoInteresesCierre;
        private decimal ldec_InteresesCierre;
        private decimal ldec_InteresesMoratoriosCierre;
        private decimal ldec_CostasCierre;
        private decimal ldec_DanoMoralCierre;

        private decimal ldec_ValorPresentePrinCierre;
        private decimal ldec_ValorPresenteIntCierre;

        private decimal? ldec_MontoPrincipalAnterior;
        private decimal? ldec_MontoInteresesAnterior;
        private decimal? ldec_InteresesAnterior;
        private decimal? ldec_InteresesMoratoriosAnterior;
        private decimal? ldec_CostasAnterior;
        private decimal? ldec_DanoMoralAnterior;

        private int lint_IdArchivo;
        private string lstr_EstadoTransaccion;//Indica el estado de la transacción, si aún sigue en cobro, o bien si ya se cerró.
        private string lstr_TipoTransaccion;//Es el tipo de transacción que se está realizando, si es un cobro o un pago. Va ligado a la resolución, y al expediente en si, este campo es meramente informativo
        private string lstr_EstadoProcesal;
        private Int32? lint_EstadoPretension;

        //Bitacoreo
        private string lstr_UsrCreacion;//Campo de Auditoría: Indica el usuario que creó el registro
        private string lstr_UsrModifica;//Campo de Auditoría: Indica el último usuario que modificó el registro
        private string lstr_FchModifica;
        private string lstr_Sociedad;

        public string Lstr_Sociedad
        {
            get { return lstr_Sociedad; }
            set { lstr_Sociedad = value; }
        }
       

        #endregion

        #region constructores

        /// <summary>
        /// Constructor de la clase Expedientes, permite crear un expediente y almacenarlo en sistema
        /// </summary>
        
        public clsRegistrarResolucion(){}

        /// <summary>
        /// Constructor sobrecargado con información obligatoria de acreedores
        /// </summary>

        #endregion

        #region obtención y asignación
        public int Lint_IdRes
        {
            get { return lint_IdRes; }
            set { lint_IdRes = value; }
        }
        public int Lint_IdCobroPagoResolucion
        {
            get { return lint_IdCobroPagoResolucion; }
            set { lint_IdCobroPagoResolucion = value; }
        }
        public string Lstr_IdResolucion
        {
            get { return lstr_IdResolucion; }
            set { lstr_IdResolucion = value; }
        }
        public string Lstr_IdExpedienteFK
        {
            get { return lstr_IdExpedienteFK; }
            set { lstr_IdExpedienteFK = value; }
        }
        public string Lstr_IdExpediente
        {
            get { return lstr_IdExpediente; }
            set { lstr_IdExpediente = value; }
        }
        public string Lstr_NumExpediente
        {
            get { return lstr_NumExpediente; }
            set { lstr_NumExpediente = value; }
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
        public DateTime Ldt_FechaResolucion
        {
            get { return ldt_FechaResolucion; }
            set { ldt_FechaResolucion = value; }
        }
        public decimal Ldec_MontoPosibleReembolso
        {
            get { return ldec_MontoPosibleReembolso; }
            set { ldec_MontoPosibleReembolso = value; }
        }
        public DateTime? Ldt_PosibleFecSalidaRec
        {
            get { return ldec_PosibleFecSalidaRec; }
            set { ldec_PosibleFecSalidaRec = value; }
        }
        public decimal Ldec_MontoPosReemColones
        {
            get { return ldec_MontoPosReemColones; }
            set { ldec_MontoPosReemColones = value; }
        }
        public string Lstr_Observacion
        {
            get { return lstr_Observacion; }
            set { lstr_Observacion = value; }
        }
        public int Lint_CxCaCxP
        {
            get { return lint_CxCaCxP; }
            set { lint_CxCaCxP = value; }
        }

        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }
       //Cobros/Pagos
        public int Lint_IdCobroPago
        {
            get { return lint_IdCobroPago; }
            set { lint_IdCobroPago = value; }
        }
        //public string Lstr_IdResolucionFK
        //{
        //    get { return lstr_IdResolucionFK; }
        //    set { lstr_IdResolucionFK = value; }
        //}
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
        public Decimal Ldec_Tbp
        {
            get { return ldec_Tbp; }
            set { ldec_Tbp= value; }
        }
        public Decimal Ldec_Tiempo
        {
            get { return ldec_Tiempo; }
            set { ldec_Tiempo = value; }
        }


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


        public decimal? Ldec_MontoPrincipalAnterior
        {
            get { return ldec_MontoPrincipalAnterior; }
            set { ldec_MontoPrincipalAnterior = value; }
        }
        public decimal? Ldec_MontoInteresesAnterior
        {
            get { return ldec_MontoInteresesAnterior; }
            set { ldec_MontoInteresesAnterior = value; }
        }
        public decimal? Ldec_InteresesAnterior
        {
            get { return ldec_InteresesAnterior; }
            set { ldec_InteresesAnterior = value; }
        }
        public decimal? Ldec_InteresesMoratoriosAnterior
        {
            get { return ldec_InteresesMoratoriosAnterior; }
            set { ldec_InteresesMoratoriosAnterior = value; }
        }
        public decimal? Ldec_CostasAnterior
        {
            get { return ldec_CostasAnterior; }
            set { ldec_CostasAnterior = value; }
        }
        public decimal? Ldec_DanoMoralAnterior
        {
            get { return ldec_DanoMoralAnterior; }
            set { ldec_DanoMoralAnterior = value; }
        }


        public string Lstr_TipoTransaccion
        {
            get { return lstr_TipoTransaccion; }
            set { lstr_TipoTransaccion = value; }
        }
        public string Lstr_EstadoTransaccion
        {
            get { return lstr_EstadoTransaccion; }
            set { lstr_EstadoTransaccion = value; }
        }
        public string Lstr_EstadoProcesal
        {
            get { return lstr_EstadoProcesal; }
            set { lstr_EstadoProcesal = value; }
        }
        public Int32 ? Lint_EstadoPretension
        {
            get { return lint_EstadoPretension; }
            set { lint_EstadoPretension = value; }
        }
        
        public DateTime? Ldt_FchCreacion
        {
            get { return Ldt_FchCreacion; }
            set { Ldt_FchCreacion = value; }
        }
        public DateTime? Ldt_FchModifica
        {
            get { return Ldt_FchModifica; }
            set { Ldt_FchModifica = value; }
        }
        public int Lint_IdArchivo
        {
            get { return lint_IdArchivo; }
            set { lint_IdArchivo = value; }
        }
        public string Lstr_ObservacionesLiquidacion
        {
            get { return Lstr_ObservacionesLiquidacion; }
            set { Lstr_ObservacionesLiquidacion = value; }
        }

        //Bitacora
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
        public string Lstr_FchModifica
        {
            get { return lstr_FchModifica; }
            set { lstr_FchModifica = value; }
        }
        #endregion

        #region metodos
        /// <summary>
        /// Resgistrar Expedientes Configs
        /// </summary>
        /// <returns></returns>
        public bool ProcesarSPRegistrarResolucion()
        {
            //string str_RutaArchivo = @"C:\PwcProyectos\SistemaGestor\Datos\ConexionSQL\Configs\Contigentes\Resoluciones\RegistrarResolucion.config";
            string str_RutaArchivo=string.Empty;
            string str_DireccionConfigs = string.Empty;
            bool resultFlag=false;
            try { 
                  
                var appSettings = ConfigurationManager.AppSettings;
                str_DireccionConfigs = appSettings["DireccionConfigs"];
                str_RutaArchivo = str_DireccionConfigs+"\\Contigentes\\Resoluciones\\RegistrarResolucion.config";
                EjecucionSP(str_RutaArchivo, this);
                resultFlag = true;
            }

            catch (Exception ex)
            {
                resultFlag = false;
            }

            return resultFlag;
        }

        /// <summary>
        /// Resgistrar Expedientes Configs
        /// </summary>
        /// <returns></returns>
        public bool ProcesarSPModificarResolucion()
        {
            //string str_RutaArchivo = @"C:\PwcProyectos\SistemaGestor\Datos\ConexionSQL\Configs\Contigentes\Resoluciones\RegistrarResolucion.config";
            string str_RutaArchivo = string.Empty;
            string str_DireccionConfigs = string.Empty;
            bool resultFlag = false;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                str_DireccionConfigs = appSettings["DireccionConfigs"];
                str_RutaArchivo = str_DireccionConfigs + "\\Contigentes\\Resoluciones\\ModificarResolucion.config";
                EjecucionSP(str_RutaArchivo, this);
                resultFlag = true;
            }

            catch (Exception ex)
            {
                resultFlag = false;
            }

            return resultFlag;
        }

        public bool ConsultarExpendientesResoluciones()
        {
            string str_RutaArchivo = string.Empty;
            string str_DireccionConfigs = string.Empty;
            bool resultFlag = false;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Contigentes\\Resoluciones\\ConsultarExpendientesResoluciones.config", this);
                resultFlag = true;
            }
            catch (Exception ex)
            {
                resultFlag = false;
                this.Lstr_MensajeRespuesta = ex.ToString();
            }


            return resultFlag;
        }

        public bool ConsultarResolucion()
        {
            string str_RutaArchivo = string.Empty;
            string str_DireccionConfigs = string.Empty;
            bool resultFlag = false;
           
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Contigentes\\Resoluciones\\ConsultarResolucion.config", this);
                resultFlag = true;
            }
            catch (Exception ex)
            {
                resultFlag = false;
                this.Lstr_MensajeRespuesta = ex.ToString();
            }


            return resultFlag;
        }

        /// <summary>
        /// Mappeo de Resolucion datos con SP
        /// </summary>
        /// <returns></returns>
        public bool ProcesarSPDeclararSinLugar()
        {
            //string str_RutaArchivo = @"C:\PwcProyectos\SistemaGestor\Datos\ConexionSQL\Configs\Contigentes\Resoluciones\DeclararSinLugar.config";
            string str_RutaArchivo=string.Empty;
            string str_DireccionConfigs = string.Empty;
            bool resultFlag=false;
            try { 
                  
                var appSettings = ConfigurationManager.AppSettings;
                str_DireccionConfigs = appSettings["DireccionConfigs"];
                str_RutaArchivo = str_DireccionConfigs+"\\Contigentes\\Resoluciones\\DeclararSinLugar.config";
                EjecucionSP(str_RutaArchivo, this);
                resultFlag = true;
            }

            catch (Exception ex)
            {
                resultFlag = false;
            }

            return resultFlag;
        }

        /// <summary>
        /// Mappeo de CobrosPagos Resolucion datos con SP
        /// </summary>
        /// <returns></returns>
        public bool ProcesarSPCobrosPagos()
        {
            //string str_RutaArchivo = @"C:\PwcProyectos\SistemaGestor\Datos\ConexionSQL\Configs\Contigentes\Resoluciones\DeclararSinLugar.config";
            string str_RutaArchivo = string.Empty;
            string str_DireccionConfigs = string.Empty;
            bool resultFlag = false;
            try
            {

                var appSettings = ConfigurationManager.AppSettings;
                str_DireccionConfigs = appSettings["DireccionConfigs"];
                str_RutaArchivo = str_DireccionConfigs + "\\Contigentes\\CobrosPagos\\RegistrarCobroPago.config";
                EjecucionSP(str_RutaArchivo, this);
                resultFlag = true;
            }

            catch (Exception ex)
            {
                resultFlag = false;
            }

            return resultFlag;
        }

        #region Metodos Asientos
        public string VerificarExisteProvision(string provision)
        {
            clsObtieneDatosGeneralContigentes consultar = new clsObtieneDatosGeneralContigentes();

            string flag = string.Empty;
            string result = string.Empty;
            string consulta="Select * from co.Resoluciones where EstadoResolucion='"+provision+"'";
            DataTable ds=consultar.ObtenerDatos(consulta);

            if (ds.Rows.Count > 0)//Existe resolucion
            {
                flag = "Existe";
            }
            else {
                flag = "No Existe";
            }

            return result;

        }

        public decimal[] ObtenerMontoResolucion(string idExpediente, string estadopProvision)
        {
            clsObtieneDatosGeneralContigentes obDatos=new clsObtieneDatosGeneralContigentes();
            decimal montoPrincipalColones;
            decimal montoInteresColones;
            decimal[] arrayMontos = new decimal[3];
            string consulta="Select * from co.Resoluciones as r inner join co.CobrosPagos as cp on r.IdResolucion=cp.IdResolucionFK where r.IdExpedienteFK='"+idExpediente+"' and r.EstadoResolucion='"+estadopProvision+"'";
            DataTable ds=obDatos.ObtenerDatos(consulta);

            if(ds.Rows.Count>0){
                foreach(DataRow item in ds.Rows){
                    montoPrincipalColones=Convert.ToDecimal(item["MontoPrincipalColones"].ToString());
                    montoInteresColones=Convert.ToDecimal(item["MontoInteresesColones"].ToString());
                    arrayMontos[0]=montoPrincipalColones;//Monto 1
                    arrayMontos[1]=montoInteresColones;//Monto 2
                    arrayMontos[2]=Convert.ToDateTime(item["FchCreacion"].ToString()).Year;//sacamos periodo de registro ya ingresado
                }
            }

            return arrayMontos;
        }

        #endregion
        
        #endregion
        
        
   }
}