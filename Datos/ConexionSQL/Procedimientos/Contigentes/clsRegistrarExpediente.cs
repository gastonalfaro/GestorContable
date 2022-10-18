using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using log4net.Config;
using System.Configuration;

//Log4Net inicializa en WebApplication
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace Datos.ConexionSQL.Procedimientos.Contigentes
{
    public class clsRegistrarExpediente : clsProcedimientoAlmacenado
    {
        #region Variables 

        /// log4Net variable 
        private static readonly ILog log = LogManager.GetLogger(typeof(clsRegistrarExpediente));
        private string lstr_IdExpediente = string.Empty;
        private string lstr_NumExOrigen = string.Empty; //número de expediente origen
        private string lstr_TipoExpediente = string.Empty; //tipo de expediente
        private string lstr_EstadoExpediente = string.Empty; //estado de expediente
        private DateTime ldt_FechaDemanda = new DateTime(); //fecha de la demanda del expediente
        private string ldt_FechInicio = string.Empty;

       
        private string lstr_TipoProceso = string.Empty; //tipo de proceso del expediente
        private string lstr_MotivoDemanda = string.Empty; //motivo de la demanda
        private string lstr_MonedaPretension = string.Empty; //nombre de la moneda con la que se pretende cobrar ¢ o $
        private decimal ldec_TipoCambio = 0; //valor del tipo de cambio vigente
        private decimal ldec_MontoPretension = 0;//Corresponde al monto estimado de la demanda en la moneda que se haya declarado. 
        private decimal ldec_MontoPretColones = 0;
        private int lint_EstadoPretension = 0;//estado de la pretension 
        private DateTime? ldt_PosibleFecEntRec = new DateTime(); //Moneda base del tramo
        private decimal ldec_ValorPresente=0;//Valor presente 
        private string lstr_EstadoProcesal = string.Empty;//estado del proceso en el expediente respectivo
        private string lstr_UsrCreacion=string.Empty;//
        private string lstr_UsrModifica = string.Empty;
        private string lstr_FchModifica = string.Empty;
        private string lstr_CedDemandado = string.Empty;
        private string lstr_NombreDemandado = string.Empty;
        private string lstr_CedActor = string.Empty;
        private string lstr_NombreActor = string.Empty;
        private string lstr_TipoEntidadPersona = string.Empty;
        private string lstr_Porcentaje = string.Empty;
        private string lstr_SociedadGL = string.Empty;
        private string lstr_ObservacionesPretension = string.Empty;

        //campos nuevos para reporte
        private string lstr_IdSociedadGL = string.Empty;
        private string lstr_NomDemandado = string.Empty;
        private decimal ldec_MontoPrincipal = 0;
        private decimal ldec_MontoInteres = 0;
        private decimal ldec_MontoCostas = 0;
        private decimal ldec_MontoIntMoratorios = 0;
        private decimal ldec_MontoDannosPerj = 0;

        #endregion
       
        #region asingacion y obtencion

        //////
        public string Lstr_IdSociedadGL
        {
            get { return lstr_IdSociedadGL; }
            set { lstr_IdSociedadGL = value; }
        }
        public string Lstr_NomDemandado
        {
            get { return lstr_NomDemandado; }
            set { lstr_NomDemandado = value; }
        }
        public decimal Ldec_MontoPrincipal
        {
            get { return ldec_MontoPrincipal; }
            set { ldec_MontoPrincipal = value; }
        }
        public decimal Ldec_MontoInteres
        {
            get { return ldec_MontoInteres; }
            set { ldec_MontoInteres = value; }
        }
        public decimal Ldec_MontoCostas
        {
            get { return ldec_MontoCostas; }
            set { ldec_MontoCostas = value; }
        }
        public decimal Ldec_MontoIntMoratorios
        {
            get { return ldec_MontoIntMoratorios; }
            set { ldec_MontoIntMoratorios = value; }
        }
        public decimal Ldec_MontoDannosPerj
        {
            get { return ldec_MontoDannosPerj; }
            set { ldec_MontoDannosPerj = value; }
        }
        //////
        public string Lstr_ObservacionesPretension
        {
            get { return lstr_ObservacionesPretension; }
            set { lstr_ObservacionesPretension = value; }
        }
        public string Ldt_FechInicio
        {
            get { return ldt_FechInicio; }
            set { ldt_FechInicio = value; }
        }
        private string ldt_FechFin = string.Empty;

        public string Ldt_FechFin
        {
            get { return ldt_FechFin; }
            set { ldt_FechFin = value; }
        }
       
        public string Lstr_IdExpediente
        {
            get { return lstr_IdExpediente; }
            set { lstr_IdExpediente = value; }
        }
        public string Lstr_MonedaPretension
        {
            get { return lstr_MonedaPretension; }
            set { lstr_MonedaPretension = value; }
        }
        public string Lstr_NumExOrigen
        {
            get { return lstr_NumExOrigen; }
            set { lstr_NumExOrigen = value; }
        }

        public string Lstr_TipoExpediente
        {
            get { return lstr_TipoExpediente; }
            set { lstr_TipoExpediente = value; }
        }

        public string Lstr_EstadoExpediente
        {
            get { return lstr_EstadoExpediente; }
            set { lstr_EstadoExpediente = value; }
        }

        public DateTime Ldt_FechaDemanda
        {
            get { return ldt_FechaDemanda; }
            set { ldt_FechaDemanda = value; }
        }

        public string Lstr_TipoProceso
        {
            get { return lstr_TipoProceso; }
            set { lstr_TipoProceso = value; }
        }

        public string Lstr_MotivoDemanda
        {
            get { return lstr_MotivoDemanda; }
            set { lstr_MotivoDemanda = value; }
        }

        public decimal Ldec_TipoCambio
        {
            get { return ldec_TipoCambio; }
            set { ldec_TipoCambio = value; }
        }

        public decimal Ldec_MontoPretension
        {
            get { return ldec_MontoPretension; }
            set { ldec_MontoPretension = value; }
        }

        public decimal Ldec_MontoPretColones
        {
            get { return ldec_MontoPretColones; }
            set { ldec_MontoPretColones = value; }
        }

        public int Lint_EstadoPretension
        {
            get { return lint_EstadoPretension; }
            set { lint_EstadoPretension = value; }
        }

        public DateTime? Ldt_PosibleFecEntRec
        {
            get { return ldt_PosibleFecEntRec; }
            set { ldt_PosibleFecEntRec = value; }

        }

        public decimal Ldec_ValorPresente
        {

            get { return ldec_ValorPresente; }
            set { ldec_ValorPresente = value; }
        }
        
        public string Lstr_EstadoProcesal
        {
            get { return lstr_EstadoProcesal; }
            set { lstr_EstadoProcesal = value; }
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
       
        public string Lstr_FchModifica
        {
            get { return lstr_FchModifica; }
            set { lstr_FchModifica = value; }
        }

        public string Lstr_CedDemandado
        {
            get { return lstr_CedDemandado; }
            set { lstr_CedDemandado = value; }
        }
        public string Lstr_NombreDemandado
        {
            get { return lstr_NombreDemandado; }
            set { lstr_NombreDemandado = value; }
        }
        public string Lstr_CedActor
        {
            get { return lstr_CedActor; }
            set { lstr_CedActor = value; }
        }
        public string Lstr_NombreActor
        {
            get { return lstr_NombreActor; }
            set { lstr_NombreActor = value; }
        }
        public string Lstr_TipoEntidadPersona
        {
            get { return lstr_TipoEntidadPersona; }
            set { lstr_TipoEntidadPersona = value; }
        }
        public string Lstr_Porcentaje
        {
            get { return lstr_Porcentaje; }
            set { lstr_Porcentaje = value; }
        }
        public string Lstr_SociedadGL
        {
            get { return lstr_SociedadGL; }
            set { lstr_SociedadGL = value; }
        }
        #endregion
        
        #region Metodos

        public clsRegistrarExpediente()
        {
           
        }

        public void CrearExpedienteReporte(string lstr_IdExpediente,
                                                    string lstr_IdSociedadGL,
                                                    string lstr_NomDemandado,
                                                    decimal ldec_MontoPrincipal,
                                                    decimal ldec_MontoInteres,
                                                    decimal ldec_MontoCostas,
                                                    decimal ldec_MontoIntMoratorios,
                                                    decimal ldec_MontoDannosPerj,
                                                    string lstr_UsrCreacion)
        {
            this.lstr_IdExpediente = lstr_IdExpediente;
            this.lstr_IdSociedadGL = lstr_IdSociedadGL;
            this.lstr_NomDemandado = lstr_NomDemandado;
            this.ldec_MontoPrincipal = ldec_MontoPrincipal;
            this.ldec_MontoInteres = ldec_MontoInteres;
            this.ldec_MontoCostas = ldec_MontoCostas;
            this.ldec_MontoIntMoratorios = ldec_MontoIntMoratorios;
            this.ldec_MontoDannosPerj = ldec_MontoDannosPerj;
            this.lstr_UsrCreacion = lstr_UsrCreacion;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Contigentes\\Expedientes\\CrearExpedienteReporte.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }


        /// <summary>
        /// Consultar Expedientes Configs
        /// </summary>
        /// <returns></returns>
        public bool ProcesarSPConsultarExpedientesEntidadesPersonas()
        {
            string str_RutaArchivo=string.Empty;
            string str_DireccionConfigs = string.Empty;
            bool resultFlag = false;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                str_DireccionConfigs = appSettings["DireccionConfigs"];
                str_RutaArchivo = str_DireccionConfigs + "\\Contigentes\\Expedientes\\ConsultarExpedientesEntidadesPersonas.config";
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
        /// Consultar por ExpNumero
        /// </summary>
        /// <returns></returns>
        public bool ProcessarSPConsultaPorNumExpediente()
        {
            //string str_RutaArchivo = @"C:\PwcProyectos\SistemaGestor\Datos\ConexionSQL\Configs\Contigentes\Expedientes\ConsultarExpedientesXNumeroExp.config";
            string str_RutaArchivo=string.Empty;
            string str_DireccionConfigs = string.Empty;
            bool resultFlag=false;
            try { 
                  
                var appSettings = ConfigurationManager.AppSettings;
                str_DireccionConfigs = appSettings["DireccionConfigs"];
                str_RutaArchivo = str_DireccionConfigs+"\\Contigentes\\Expedientes\\ConsultarExpedientesXNumeroExp.config";
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
        /// Consultar por ExpNumero
        /// </summary>
        /// <returns></returns>
        public bool ProcessarSPConsultaExpedientePorFecha()
        {
            //string str_RutaArchivo = @"C:\PwcProyectos\SistemaGestor\Datos\ConexionSQL\Configs\Contigentes\Expedientes\ConsultarExpedientesXNumeroExp.config";
            string str_RutaArchivo = string.Empty;
            string str_DireccionConfigs = string.Empty;
            bool resultFlag = false;
            try
            {

                var appSettings = ConfigurationManager.AppSettings;
                str_DireccionConfigs = appSettings["DireccionConfigs"];
                str_RutaArchivo = str_DireccionConfigs + "\\Contigentes\\Expedientes\\ConsultarExpedientesXNumeroExpFecha.config";
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
        public bool ProcesarSPRegitsrarExpediente (){
            //string str_RutaArchivo = @"C:\PwcProyectos\SistemaGestor\Datos\ConexionSQL\Configs\Contigentes\Expedientes\RegistrarExpediente.config";
            string str_RutaArchivo = string.Empty;
            string str_DireccionConfigs = string.Empty;
            bool resultFlag = false;
            try
            {

                var appSettings = ConfigurationManager.AppSettings;
                str_DireccionConfigs = appSettings["DireccionConfigs"];
                str_RutaArchivo = str_DireccionConfigs + "\\Contigentes\\Expedientes\\RegistrarExpediente.config";
                  EjecucionSP(str_RutaArchivo, this);
                  resultFlag=true;
            }catch(Exception ex){
                resultFlag = false;
            }

            return resultFlag;
         }
        /// <summary>
        /// Modificar Expdiente Configs
        /// </summary>
        /// <returns></returns>
        public bool ProcesarSPModificarExpediente() {
            bool resultFlag = false;
            string str_RutaArchivo = string.Empty;
            string str_DireccionConfigs = string.Empty;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                str_DireccionConfigs = appSettings["DireccionConfigs"];
                str_RutaArchivo = str_DireccionConfigs + "\\Contigentes\\Expedientes\\ModificarExpediente.config";
                EjecucionSP(str_RutaArchivo, this);
                resultFlag = true;
            }
            catch (Exception exc)
            {
                resultFlag = false;
                this.Lstr_MensajeRespuesta = exc.ToString();
            }

            return resultFlag;
        }

        /// <summary>
        /// Crear Revelacion Nota SP
        /// </summary>
        /// <returns></returns>
        public bool ProcesarSPCrearRevelacion() {
            //string str_RutaArchivo = @"C:\PwcProyectos\SistemaGestor\Datos\ConexionSQL\Configs\Contigentes\Expedientes\CrearRevelacionNota.config";
            bool resultFlag = false;
            string str_RutaArchivo = string.Empty;
            string str_DireccionConfigs = string.Empty;
            try
            {

                var appSettings = ConfigurationManager.AppSettings;
                str_DireccionConfigs = appSettings["DireccionConfigs"];
                str_RutaArchivo = str_DireccionConfigs + "\\Contigentes\\Expedientes\\CrearRevelacionNota.config";
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
        /// Cerrar Revelacion Nota SP
        /// </summary>
        /// <returns></returns>
        public bool ProcesarSPCerrarRevelacion()
        {
            //string str_RutaArchivo = @"C:\PwcProyectos\SistemaGestor\Datos\ConexionSQL\Configs\Contigentes\Expedientes\CerrarRevelacion.config";
            bool resultFlag = false;
            string str_RutaArchivo = string.Empty;
            string str_DireccionConfigs = string.Empty;
            try
            {

                var appSettings = ConfigurationManager.AppSettings;
                str_DireccionConfigs = appSettings["DireccionConfigs"];
                str_RutaArchivo = str_DireccionConfigs + "\\Contigentes\\Expedientes\\CerrarRevelacion.config";
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
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ProcesarSPAnularExpediente()
        {
            //string str_RutaArchivo = @"C:\PwcProyectos\SistemaGestor\Datos\ConexionSQL\Configs\Contigentes\Expedientes\AnularExpediente.config";
            bool resultFlag = false;
            string str_RutaArchivo = string.Empty;
            string str_DireccionConfigs = string.Empty;
            try
            {

                var appSettings = ConfigurationManager.AppSettings;
                str_DireccionConfigs = appSettings["DireccionConfigs"];
                str_RutaArchivo = str_DireccionConfigs + "\\Contigentes\\Expedientes\\AnularExpediente.config";
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