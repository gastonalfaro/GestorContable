using Datos.ConexionSQL.Procedimientos.Contigentes;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using log4net;
using log4net.Config;
using System.Data.SqlClient;
using System.Collections;



//Log4Net inicializa en WebApplication
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace LogicaNegocio.Contingentes
{
       
    public class clsExpedientes
    {


        #region parametros

        /// <summary>
        /// Declaración e inicialización de variables de Clase Expedientes
        /// </summary>
        //private static readonly ILog llogger_Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly ILog llogger_Log = LogManager.GetLogger("clsExpedientes");
        private static readonly ILog Log = LogManager.GetLogger("FileAppender");
        private static readonly ILog log = LogManager.GetLogger(typeof(clsRegistrarExpediente));
        private string lstr_IdExpediente = string.Empty;
        private string lstr_NumExOrigen = string.Empty; //número de expediente origen
        private string lstr_TipoExpediente = string.Empty; //tipo de expediente
        private string lstr_EstadoExpediente = string.Empty; //estado de expediente
        private DateTime ldt_FechaDemanda = new DateTime(); //fecha de la demanda del expediente
        private string lstr_TipoProceso = string.Empty; //tipo de proceso del expediente
        private string lstr_MotivoDemanda = string.Empty; //motivo de la demanda
        private string lstr_MonedaPretension = string.Empty; //nombre de la moneda con la que se pretende cobrar ¢ o $
        private decimal ldec_TipoCambio = 0; //valor del tipo de cambio vigente
        private decimal ldec_MontoPretension = 0;//Corresponde al monto estimado de la demanda en la moneda que se haya declarado. 
        private decimal ldec_MontoPretColones = 0;
        private int lint_EstadoPretension = 0;//estado de la pretension 
        private DateTime? ldt_PosibleFecEntRec = new DateTime(); //Moneda base del tramo
        private decimal ldec_ValorPresente = 0;//Valor presente 
        private string lstr_EstadoProcesal = string.Empty;//estado del proceso en el expediente respectivo
        private string lstr_UsrCreacion = string.Empty;//
        private string lstr_UsrModificacion = string.Empty;
        private string lstr_FchModifica = string.Empty;
        private string lstr_ObservacionesPretension = string.Empty;
        //

        private string ldt_FechInicio = string.Empty;
        private string ldt_FechFin = string.Empty;

        private string lstr_IdSociedadGL = string.Empty;
        private string lstr_NomDemandado = string.Empty;
        private decimal ldec_MontoPrincipal = 0;
        private decimal ldec_MontoInteres = 0;
        private decimal ldec_MontoCostas = 0;
        private decimal ldec_MontoIntMoratorios = 0;
        private decimal ldec_MontoDannosPerj = 0;
        public string Lstr_ObservacionesPretension
        {
            get { return lstr_ObservacionesPretension; }
            set { lstr_ObservacionesPretension = value; }
        }
        
       
        #endregion

        #region constructores

        /// <summary>
        /// Constructor de la clase Expedientes, permite crear un expediente y almacenarlo en sistema
        /// </summary>
        
        public clsExpedientes(){}

        /// <summary>
        /// Constructor sobrecargado con información obligatoria de acreedores
        /// </summary>
       
      /* public clsExpedientes(string lstr_IdExpediente,int lint_NumExOrigen,string lstr_TipoExpediente,string lstr_TipoFinanciamiento, string lstr_EstadoExpediente,
        DateTime ldt_FechaDemanda,string lstr_TipoProceso,string lstr_MotivoDemanda,decimal lstr_MonedaPretension,decimal ldec_TipoCambio,decimal ldec_MontoPretension,decimal ldec_MontoPretColones,
        int lint_EstadoPretension,DateTime ldt_PosibleFecEntRec,string lstr_ValorPresente,string lstr_EstadoProcesal,string lstr_UsrCreacion,string lstr_UsrModifica)
        {
            this.lstr_IdExpediente = lstr_IdExpediente;
            this.lint_NumExOrigen = lint_NumExOrigen;
            this.lstr_TipoExpediente = lstr_TipoExpediente;
            this.lstr_EstadoExpediente = lstr_EstadoExpediente;
            this.ldt_FechaDemanda = ldt_FechaDemanda;
            this.lstr_TipoProceso = lstr_TipoProceso ;
            this.lstr_MotivoDemanda = lstr_MotivoDemanda;
            this.lstr_MonedaPretension=lstr_MonedaPretension;
            this.ldec_TipoCambio=ldec_TipoCambio;
            this.ldec_MontoPretension=ldec_MontoPretension;
            this.ldec_MontoPretColones=ldec_MontoPretColones;
            this.lint_EstadoPretension=lint_EstadoPretension;
            this.ldt_PosibleFecEntRec=ldt_PosibleFecEntRec;
            this.lstr_ValorPresente = lstr_ValorPresente;
            this.lstr_EstadoProcesal=lstr_EstadoProcesal;
            this.lstr_UsrCreacion=lstr_UsrCreacion;
            this.lstr_UsrModifica=lstr_UsrModifica;
        }*/

        #endregion

        #region obtención y asignación

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

        public string Ldt_FechInicio
        {
            get { return ldt_FechInicio; }
            set { ldt_FechInicio = value; }
        }

        public string Ldt_FechFin
        {
            get { return ldt_FechInicio; }
            set { ldt_FechFin = value; }
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

        public string Lstr_UsrModificacion
        {
            get { return lstr_UsrModificacion; }
            set { lstr_UsrModificacion = value; }
        }
        
        #endregion

        #region Metodos


        public string CrearExpedientesReportes(string lstr_IdExpediente,
                                                    string lstr_IdSociedadGL,
                                                    string lstr_NomDemandado,
                                                    decimal ldec_MontoPrincipal,
                                                    decimal ldec_MontoInteres,
                                                    decimal ldec_MontoCostas,
                                                    decimal ldec_MontoIntMoratorios,
                                                    decimal ldec_MontoDannosPerj,
                                                    string lstr_UsrCreacion,
                                                    out string str_CodResultado,
                                                    out string str_Mensaje)
        {
            string lstr_ResCreacion = "";
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsRegistrarExpediente cr_Procedimiento = new clsRegistrarExpediente();

                cr_Procedimiento.CrearExpedienteReporte(lstr_IdExpediente,
                                                    lstr_IdSociedadGL,
                                                    lstr_NomDemandado,
                                                    ldec_MontoPrincipal,
                                                    ldec_MontoInteres,
                                                    ldec_MontoCostas,
                                                    ldec_MontoIntMoratorios,
                                                    ldec_MontoDannosPerj,
                                                    lstr_UsrCreacion);

                str_CodResultado = cr_Procedimiento.Lstr_CodigoResultado;
                str_Mensaje = cr_Procedimiento.Lstr_MensajeRespuesta;

                Log.Info(str_Mensaje);
                if (String.Equals(str_CodResultado, "00"))
                {
                    lstr_ResCreacion = "Código: " + str_CodResultado + ". Mensaje: " + str_Mensaje;
                }
            }
            catch (Exception ex)
            {
                lstr_ResCreacion = ex.ToString();
            }
            return lstr_ResCreacion;
        }


        /// <summary>
        /// Registra expedientes en el sistema gestor
        /// </summary>
        /// <param name="str_NumExOrigen"></param>
        /// <param name="str_TipoExpediente"></param>
        /// <param name="str_EstadoExpediente"></param>
        /// <param name="dt_FechaDemanda"></param>
        /// <param name="str_TipoProceso"></param>
        /// <param name="str_MotivoDemanda"></param>
        /// <param name="str_MonedaPretension"></param>
        /// <param name="dec_TipoCambio"></param>
        /// <param name="dec_MontoPretension"></param>
        /// <param name="dec_MontoPretColones"></param>
        /// <param name="int_EstadoPretension"></param>
        /// <param name="dt_PosibleFecEntRec"></param>
        /// <param name="dec_ValorPresente"></param>
        /// <param name="str_EstadoProcesal"></param>
        /// <param name="str_UsrCreacion"></param>
        /// <param name="str_UsrModifica"></param>
        /// <returns>array string[] parametros devueltos por procesamiento de transaccion</returns>
        public string[] RegsitrarExpedientes(string str_IdExpediente, string str_IdSociedadGL, string str_NumExOrigen, string str_TipoExpediente, string str_EstadoExpediente,
         DateTime dt_FechaDemanda, string str_TipoProceso, string str_MotivoDemanda, string str_EstadoProcesal, string str_UsrCreacion,
         string str_CedActor, string str_CedDemandado, string str_NombreActor, string str_NombreDemandado, string str_TipoEntidadPersona, string str_Porcentaje)
          
         {

            //variables locales
            clsRegistrarExpediente l_Exp = new clsRegistrarExpediente();
            clsRegistrarEntidadPersona l_EP = new clsRegistrarEntidadPersona();
            
            string[] lstr_result=new string[2];
            //insertar codigo de mappeo de datos
            try
            {
                l_Exp.Lstr_IdExpediente =str_IdExpediente;
                l_Exp.Lstr_NumExOrigen =  str_NumExOrigen;
                l_Exp.Ldt_FechaDemanda = dt_FechaDemanda;
                l_Exp.Lstr_EstadoProcesal = str_EstadoProcesal;
                l_Exp.Lstr_EstadoExpediente = str_EstadoExpediente;
                l_Exp.Lstr_CedActor = str_CedActor;
                //Datos persona/entidad

                l_Exp.Lstr_CedDemandado = str_CedDemandado;
                l_Exp.Lstr_NombreActor = str_NombreActor;
                l_Exp.Lstr_NombreDemandado = str_NombreDemandado;
                l_Exp.Lstr_TipoEntidadPersona = str_TipoEntidadPersona;
                l_Exp.Lstr_Porcentaje = str_Porcentaje;
                //Ingreso de pretension inicial
                l_Exp.Lstr_MotivoDemanda = str_MotivoDemanda;
                l_Exp.Lstr_TipoExpediente = str_TipoExpediente;
                l_Exp.Lstr_TipoProceso = str_TipoProceso;
                l_Exp.Lstr_SociedadGL = str_IdSociedadGL;
                //Bitacoreo
                l_Exp.Lstr_UsrCreacion = str_UsrCreacion;//expediente registro
               
                

                //****** Acceso a datos en SQL conexion ******/// 
                bool process=l_Exp.ProcesarSPRegitsrarExpediente();//Realizamos el mappeo en la BD
                //bool processEP = l_EP.ProcesarSPRegitsrarEntidadPersona();//


                lstr_result[0] = "Codigo:" + l_Exp.Lstr_CodigoResultado;// +"-" + l_EP.Lstr_CodigoResultado;
                lstr_result[1] = "Resultado insercion de Expediente en logica de negocio > " + l_Exp.Lstr_MensajeRespuesta;// +"-Insercion de EntidadPersona" + l_EP.Lstr_MensajeRespuesta;

   
               
            }catch(Exception ex){
                lstr_result[0] = "Codigo:" + ex.StackTrace;
                lstr_result[1] = "Resultado insercion de Expediente en logica de negocio> " + ex.Message;
                //logger 
                llogger_Log.Error("Error RegsitrarExpedientes: " + ex.Message + "- BD mensaje " + l_Exp.Lstr_MensajeRespuesta);

            }

            return lstr_result;
            
        }
     
        /// <summary>
        /// Resgistrar pretension incial
        /// </summary>
        /// <param name="str_IdExpediente"></param>
        /// <param name="str_MonedaPretension"></param>
        /// <param name="dec_TipoCambio"></param>
        /// <param name="dec_MontoPretension"></param>
        /// <param name="dec_MontoPretColones"></param>
        /// <param name="int_EstadoPretension"></param>
        /// <param name="dt_PosibleFecEntRec"></param>
        /// <param name="dec_ValorPresente"></param>
        /// <param name="str_EstadoProcesal"></param>
        /// <param name="str_UsrModifica"></param>
        /// <returns></returns>
        public string[] RegistrarPretensionInicial(string str_IdExpediente,string str_Sociedad, string str_TipoProceso, string str_MonedaPretension, decimal dec_TipoCambio, 
            decimal dec_MontoPretension, decimal dec_MontoPretColones, decimal dec_MontoPosibleReembolso, int int_EstadoPretension, DateTime? dt_PosibleFecEntRec, decimal dec_ValorPresente,string str_ObservacionesPretension, string str_UsrModifica)
        {
            //variables locales
            clsRegistrarPretensionInicial l_ExpIni = new clsRegistrarPretensionInicial();
            string[] lstr_result = new string[2];
            //insertar codigo de mappeo de datos
            try
            {
                l_ExpIni.Lstr_IdExpediente = str_IdExpediente;
                l_ExpIni.Lstr_TipoProceso = str_TipoProceso;
                l_ExpIni.Ldec_MontoPretColones=dec_MontoPretColones;
                l_ExpIni.Ldec_MontoPretension = dec_MontoPretension;
                l_ExpIni.Lstr_MonedaPretension = str_MonedaPretension;
                l_ExpIni.Ldec_TipoCambio = dec_TipoCambio;
                l_ExpIni.Ldec_ValorPresente = dec_ValorPresente;
                l_ExpIni.Ldt_PosibleFecEntRec = dt_PosibleFecEntRec;
                l_ExpIni.Lint_EstadoPretension = int_EstadoPretension;
                l_ExpIni.Ldec_MontoPosibleReembolso = dec_MontoPosibleReembolso;
                l_ExpIni.Lstr_Sociedad = str_Sociedad;
                l_ExpIni.Lstr_UsrModificar = str_UsrModifica;
                l_ExpIni.Lstr_ObservacionesPretension = str_ObservacionesPretension;
                //****** Acceso a datos en SQL conexion ******/// 
                bool process = l_ExpIni.ProcesarSPRegitsrarPretesnionInicial();//Realizamos el mappeo en la BD
                //******* Resultado de procesar store procedure ****//
                lstr_result[0] = "Codigo error:" + l_ExpIni.Lstr_CodigoResultado + "-" + l_ExpIni.Lstr_CodigoResultado;
                lstr_result[1] = "Resultado insercion de pretetension inicial en logica de negocio> " + l_ExpIni.Lstr_MensajeRespuesta;

            }
            catch (Exception ex)
            {

                //logger 
                llogger_Log.Error("Error RegsitrarPretenionIncial: " + ex.Message + "- BD mensaje " + l_ExpIni.Lstr_MensajeRespuesta);

            }

            return lstr_result;
        
        }
        /// <summary>
        /// Modificar Expediente
        /// </summary>
        /// <param name="str_NumExOrigen"></param>
        /// <param name="str_TipoExpediente"></param>
        /// <param name="str_EstadoExpediente"></param>
        /// <param name="dt_FechaDemanda"></param>
        /// <param name="str_TipoProceso"></param>
        /// <param name="str_MotivoDemanda"></param>
        /// <param name="str_MonedaPretension"></param>
        /// <param name="dec_TipoCambio"></param>
        /// <param name="dec_MontoPretension"></param>
        /// <param name="dec_MontoPretColones"></param>
        /// <param name="int_EstadoPretension"></param>
        /// <param name="dt_PosibleFecEntRec"></param>
        /// <param name="str_ValorPresente"></param>
        /// <param name="str_EstadoProcesal"></param>
        /// <param name="str_UsrModifica"></param>
        /// <returns></returns>
        public string[] ModificarExpedientes(string str_IdExpediente, String str_IdSociedadGL, string str_NumExOrigen, string str_TipoExpediente, string str_EstadoExpediente,
         DateTime dt_FechaDemanda, string str_TipoProceso, string str_MotivoDemanda, string str_EstadoProcesal, string str_UsrModifica,
         string str_CedActor, string str_CedDemandado, string str_NombreActor, string str_NombreDemandado, string str_TipoEntidadPersona, string str_Porcentaje)
        {

            //variables locales
            clsRegistrarExpediente l_Exp = new clsRegistrarExpediente();
            clsRegistrarEntidadPersona l_EP = new clsRegistrarEntidadPersona();

            string[] lstr_result = new string[2];
            //insertar codigo de mappeo de datos
            try
            {
                l_Exp.Lstr_IdExpediente = str_IdExpediente;
                l_Exp.Lstr_IdSociedadGL = str_IdSociedadGL;
                l_Exp.Lstr_NumExOrigen = str_NumExOrigen;
                l_Exp.Ldt_FechaDemanda = dt_FechaDemanda;
                l_Exp.Lstr_EstadoProcesal = str_EstadoProcesal;
                l_Exp.Lstr_EstadoExpediente = str_EstadoExpediente;
                //Datos persona/entidad
                l_Exp.Lstr_IdExpediente = str_IdExpediente;
                l_Exp.Lstr_CedActor = str_CedActor;
                l_Exp.Lstr_CedDemandado = str_CedDemandado;
                l_Exp.Lstr_NombreActor = str_NombreActor;
                l_Exp.Lstr_NombreDemandado = str_NombreDemandado;
                l_Exp.Lstr_TipoEntidadPersona = str_TipoEntidadPersona;
                l_Exp.Lstr_Porcentaje = str_Porcentaje;
                //Ingreso de pretension inicial
                l_Exp.Lstr_MotivoDemanda = str_MotivoDemanda;
                l_Exp.Lstr_TipoExpediente = str_TipoExpediente;
                l_Exp.Lstr_TipoProceso = str_TipoProceso;
                //Bitacoreo
                l_Exp.Lstr_UsrModifica = str_UsrModifica;//expediente registro
                l_Exp.Lstr_FchModifica=DateTime.Now.Date.ToString();//expediente registro
                
                //****** Acceso a datos en SQL conexion ******/// 
                bool process = l_Exp.ProcesarSPModificarExpediente();//Realizamos el mappeo en la BD
                //bool processEP = l_EP.ProcesarSPRegitsrarEntidadPersona();//

                lstr_result[0] = "Codigo error:" + l_Exp.Lstr_CodigoResultado;
                lstr_result[1] = "Resultado de modificar Expediente en logica de negocio > " + l_Exp.Lstr_MensajeRespuesta;
            }
            catch (Exception ex)
            {
                lstr_result[0] = "Codigo error:" + ex.StackTrace;
                lstr_result[1] = "Resultado de modificar Expediente en logica de negocio> " + ex.Message;
                //logger 
                llogger_Log.Error("Error ModificarExpedientes: " + ex.Message + "- BD mensaje " + l_Exp.Lstr_MensajeRespuesta);
            }

            return lstr_result;
        }
        /// <summary>
        /// Eliminar Expedientes
        /// </summary>
        /// <param name="lstr_IdExpediente"></param>
        /// <returns></returns>
       
       /// <summary>
       /// Crear una revelacion en nota
       /// </summary>
       /// <param name="IdExpedient"></param>
       /// <param name="EstadoPretension"></param>
       /// <returns></returns>
       public bool CrearRevelacion(string IdExpediente, int EstadoPretension)
       {
           clsRegistrarExpediente regExp=new clsRegistrarExpediente();
           bool result=false;
           try {
               
             regExp.Lstr_IdExpediente = IdExpediente;
             regExp.Lint_EstadoPretension = EstadoPretension;
             result = regExp.ProcesarSPCrearRevelacion();
           }
           catch(Exception ex){
               result = false;
           }
          
           return result;

       }
        /// <summary>
        /// Cerrar una revelacion en nota
        /// </summary>
        /// <param name="IdExpedient"></param>
        /// <param name="EstadoPretension"></param>
        /// <returns></returns>
       public bool CerrarRevelacion(string IdExpediente, int EstadoPretension)
       {
           clsRegistrarExpediente regExp = new clsRegistrarExpediente();
           bool result = false;
           try
           {
               regExp.Lstr_IdExpediente = IdExpediente;
               regExp.Lint_EstadoPretension = EstadoPretension;
               result = regExp.ProcesarSPCerrarRevelacion();
           }
           catch (Exception ex)
           {
               result = false;
           }

           return result;
       }
      
       /// <summary>
       /// Anular un Expediente
       /// </summary>
       /// <param name="IdExpediente"></param>
       /// <param name="EstadoExpediente"></param>
       /// <returns></returns>
       public string[] AnularExpediente(string IdExpediente, string EstadoExpediente,string Sociedad)
       {
           clsRegistrarExpediente regExp = new clsRegistrarExpediente();
           string[] lstr_result = new string[2];
           try
           {
               regExp.Lstr_IdExpediente = IdExpediente;
               regExp.Lstr_EstadoExpediente = EstadoExpediente;
               regExp.Lstr_SociedadGL = Sociedad;
             bool   result = regExp.ProcesarSPAnularExpediente();


             lstr_result[0] = "Codigo:" + regExp.Lstr_CodigoResultado;
             lstr_result[1] = "Resultado de modificar Expediente en logica de negocio > " + regExp.Lstr_MensajeRespuesta;



            }
            catch (Exception ex)
            {
                lstr_result[0] = "Codigo:" + ex.StackTrace;
                lstr_result[1] = "Resultado de anular Expediente en logica de negocio> " + ex.Message;
                //logger 
                llogger_Log.Error("Error anularExpedientes: " + ex.Message + "- BD mensaje " + regExp.Lstr_MensajeRespuesta);

            }

            return lstr_result;
       }

       /// <summary>
       /// Consultar Expedientes 
       /// </summary>
       /// <returns>Dataset</returns>
       public DataSet ConsultarExpedientesEntidadesPersonas(string sociedad)
       {
           DataSet lds_TablasConsulta = new DataSet();
           clsRegistrarExpediente cr_Procedimiento = new clsRegistrarExpediente();
           cr_Procedimiento.Lstr_SociedadGL = sociedad;
           cr_Procedimiento.ProcesarSPConsultarExpedientesEntidadesPersonas();
           lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
           lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
           return lds_TablasConsulta;
       }
        /// <summary>
       /// Consultar por numero de expediente 
       /// </summary>
       /// <param name="IdExpediente"></param>
       /// <returns></returns>
        public DataSet ConsultarExpedienteXNumExp(string IdExpediente,string sociedad, string EstadoExpediente="Activo")
       {
           DataSet lds_TablasConsulta = new DataSet();
           clsRegistrarExpediente cr_Procedimiento = new clsRegistrarExpediente();
           cr_Procedimiento.Lstr_IdExpediente = IdExpediente;//emviamos parametro
           cr_Procedimiento.Lstr_SociedadGL = sociedad;
           cr_Procedimiento.Lstr_EstadoExpediente = EstadoExpediente;
           cr_Procedimiento.ProcessarSPConsultaPorNumExpediente();
           lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
           lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
           return lds_TablasConsulta;
       }
        /// <summary>
        /// Consultar por numero de expediente 
        /// </summary>
        /// <param name="IdExpediente"></param>
        /// <returns></returns>
        public DataSet ConsultarExpedienteXFecha(string FechaInicio, string FechaFin,string sociedad)
        {
            DataSet lds_TablasConsulta = new DataSet();
            clsRegistrarExpediente cr_Procedimiento = new clsRegistrarExpediente();
            cr_Procedimiento.Ldt_FechInicio= FechaInicio;//emviamos parametro
            cr_Procedimiento.Ldt_FechFin = FechaFin;
            cr_Procedimiento.Lstr_SociedadGL = sociedad;
            cr_Procedimiento.ProcessarSPConsultaExpedientePorFecha();
            lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
            lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            return lds_TablasConsulta;
        }

        //Realizar consultas generales para el modulo de contigentes
        public DataTable ConsultasGeneralesExpedientes(string consulta)
        {

            clsObtieneDatosGeneralContigentes l_consulta = new clsObtieneDatosGeneralContigentes();
            DataTable dt_Consulta = l_consulta.ObtenerDatos(consulta);
            return dt_Consulta;

        }
        
        #endregion

        #region Validaciones y operaciones utiles 
        /// <summary>
        /// Devuelve el tipo de cmabio segun 
        /// </summary>
        /// <returns></returns>
      
        #endregion


        
    }
}