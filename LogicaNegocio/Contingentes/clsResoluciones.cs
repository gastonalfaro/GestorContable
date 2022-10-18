using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using log4net.Config;
using Datos.ConexionSQL.Procedimientos.Contigentes;
using System.Data;

namespace LogicaNegocio.Contingentes
{
    public class clsResoluciones
    {
        #region parametros
        private static readonly ILog llogger_Log = LogManager.GetLogger("ContingentesLog");//varibale de bitacoreo
        private int lint_IdRes;
        private int lint_IdCobroPagoResolucion;
        private string lstr_IdResolucion;//Identificador único de la resolución dictada en los tribunales de justicia
        private string lstr_IdExpedienteFK;//Llave que relaciona las resoluciones dictadas, con los expedientes existentes

        private string lstr_IdExpediente;//Llave que relaciona las resoluciones dictadas, con los expedientes existentes
        private string lstr_NumExpediente;
        private string lstr_IdSociedadGL;


        private string lstr_EstadoResolucion;//Campo que define qué tipo de resolución es, Resolución Provisional 1 ó 2,  Resolución en Firme (aplica tanto para activos contingentes como para pasivos contingentes), o Declaración Sin Lugar.
        private DateTime? ldt_FechaResolucion;//Fecha en la que se dio el fallo campo 
        private DateTime? ldec_PosibleFecSalidaRec;//Posible fecha de la salida de los recursos (aplica para resoluciones con EstadoResolucion en Resolucion Provisional 1 ó 2).
        private decimal ldec_MontoPosibleReembolso;//Monto del posible reembolso en caso de que la resolución estipulara un monto menor al indicado previamente.
        private decimal ldec_MontoPosReemColones;//Monto en colones del posible reembolso en caso de que la resolución estipulara un monto menor al indicado previamente.
        private string lstr_Observacion;//Breve observación que justifica los hechos de la resolución.
        private int lint_CxCaCxP;//Definición de estatus del expediente, si es un activo contingente o pasivo contingente. Es un auxiliar del campo TipoExpediente del expediente, conserva el estado de la resolución anterior para consulta, y permite el cambio de estado del expediente.
        private string lstr_UsrCreacion;//Campo de Auditoría: Indica el usuario que creó el registro
        private string lstr_UsrModifica;//Campo de Auditoría: Indica el último usuario que modificó el registro
        private DateTime? lstr_FechFalloResol;
        private string lstr_Sociedad;
        public string lstr_Estado;
        private Int32? lint_EstadoPretension;
       
        #endregion

        #region constructores

        /// <summary>
        /// Constructor de la clase Expedientes, permite crear un expediente y almacenarlo en sistema
        /// </summary>
        
        public clsResoluciones(){}

        /// <summary>
        /// Constructor sobrecargado con información obligatoria de acreedores
        /// </summary>

       /* public clsResoluciones(int lint_IdResolucion,string lstr_IdExpedienteFK,string lstr_EstadoResolucion,DateTime ldt_FechaResolucion,DateTime ldec_PosibleFecSalidaRec,decimal ldec_MontoPosibleReembolso,decimal ldec_MontoPosReemColones,string lstr_Observacion,int lint_CxCaCxP,string lstr_UsrCreacion,string lstr_UsrModifica)
        {
            this.lint_IdResolucion = lint_IdResolucion;
            this.lstr_IdExpedienteFK = lstr_IdExpedienteFK;
            this.lstr_EstadoResolucion = lstr_EstadoResolucion;
            this.ldt_FechaResolucion = ldt_FechaResolucion;
            this.ldec_PosibleFecSalidaRec= ldec_PosibleFecSalidaRec;
            this.ldec_MontoPosibleReembolso = ldec_MontoPosibleReembolso;
            this.ldec_MontoPosReemColones = ldec_MontoPosReemColones;
            this.lstr_Observacion=lstr_Observacion;
            this.lint_CxCaCxP=lint_CxCaCxP;
            this.lstr_UsrCreacion=lstr_UsrCreacion;
            this.lstr_UsrModifica=lstr_UsrModifica;
            
        }*/
       
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

        public string Lstr_IdSociedadGL
        {
            get { return lstr_IdSociedadGL; }
            set { lstr_IdSociedadGL = value; }
        }

        public string Lstr_Sociedad
        {
            get { return lstr_Sociedad; }
            set { lstr_Sociedad = value; }
        }
        public DateTime? Lstr_FechFalloResol
        {
            get { return lstr_FechFalloResol; }
            set { lstr_FechFalloResol = value; }
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
        public string Lstr_EstadoResolucion
        {
            get { return lstr_EstadoResolucion; }
            set { lstr_EstadoResolucion = value; }
        }
        public DateTime? Ldt_FechaResolucion
        {
            get { return ldt_FechaResolucion; }
            set { ldt_FechaResolucion = value; }
        }
        public decimal Ldec_MontoPosibleReembolso
        {
            get { return ldec_MontoPosibleReembolso; }
            set { ldec_MontoPosibleReembolso = value; }
        }
        public DateTime? Ldec_PosibleFecSalidaRec
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

        public Nullable<Int32> Lint_EstadoPretension
        {
            get { return lint_EstadoPretension; }
            set { lint_EstadoPretension = value; }
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
        /// Registra una resolucion del expediente 
        /// </summary>
        /// <param name="IdResolucion"></param>
        /// <param name="IdExpedienteFK"></param>
        /// <param name="EstadoResolucion"></param>
        /// <param name="FechaResolucion"></param>
        /// <param name="MontoPosibleReembolso"></param>
        /// <param name="Observacion"></param>
        /// <param name="CxCaCxP"></param>
        /// <param name="UsrCreacion"></param>
        /// <returns>string[] result</returns>
        public string[] RegistrarResolucion(string IdResolucion, string IdExpedienteFK, string IdSociedadGL, string EstadoResolucion, string Estado, DateTime FechaResolucion,
            DateTime? PosibleFecSalidaRec, decimal MontoPosibleReembolso, decimal MontoPosReemColones, string Observacion, int CxCaCxP, string UsrCreacion, string Moneda,
            string EstadoTransaccion, decimal TipoCambio, Decimal Tbp, Decimal Tiempo, decimal MontoPrincipal, decimal MontoIntereses, decimal InteresesMoratorios, 
            decimal InteresesMoratoriosColones, decimal MontoInteresesColones, decimal MontoPrincipalColones, decimal ValorPresenteIntColones,
            decimal ValorPresentePrincipal, decimal ValorPresenteIntereses, decimal ValorPresentePrinColones, decimal Costas, decimal CostasColones, decimal DanoMoral, decimal DanoMoralColones,
            string TipoTransaccion, Nullable<Int32> int_EstadoPretension, string EstadoProcesal)

        {
            string[] result=new string[2];

            clsRegistrarResolucion resol = new clsRegistrarResolucion();
            
            try
            {
  
                resol.Lstr_IdResolucion = IdResolucion;
                resol.Lstr_IdExpedienteFK = IdExpedienteFK;
                resol.Lstr_IdSociedadGL = IdSociedadGL;
                resol.Lstr_EstadoResolucion = EstadoResolucion;
                resol.Lstr_Estado = Estado;
                resol.Ldt_FechaResolucion = FechaResolucion;
                resol.Ldt_PosibleFecSalidaRec = PosibleFecSalidaRec;
                resol.Ldec_MontoPosibleReembolso = MontoPosibleReembolso;
                resol.Ldec_MontoPosReemColones = MontoPosReemColones;
                resol.Lstr_Observacion = Observacion;
                resol.Lint_CxCaCxP = CxCaCxP;
                resol.Lstr_Moneda = Moneda;
                resol.Lstr_EstadoTransaccion = EstadoTransaccion;
                resol.Ldec_TipoCambio = TipoCambio;
                resol.Ldec_Tbp = Tbp;
                resol.Ldec_Tiempo = Tiempo;

                resol.Ldec_MontoPrincipal = MontoPrincipal;
                resol.Ldec_MontoPrincipalColones = MontoPrincipalColones;
                resol.Ldec_MontoIntereses = MontoIntereses;
                resol.Ldec_MontoInteresesColones = MontoInteresesColones;
                resol.Ldec_InteresesMoratorios = InteresesMoratorios;
                resol.Ldec_InteresesMoratoriosColones = InteresesMoratoriosColones;
                resol.Ldec_Costas = Costas;
                resol.Ldec_CostasColones = CostasColones;
                resol.Ldec_DanoMoral = DanoMoral;
                resol.Ldec_DanoMoralColones = DanoMoralColones;

                resol.Ldec_ValorPresenteIntereses = ValorPresenteIntereses;
                resol.Ldec_ValorPresenteIntColones = ValorPresenteIntColones;
                resol.Ldec_ValorPresentePrincipal = ValorPresentePrincipal;
                resol.Ldec_ValorPresentePrinColones = ValorPresentePrinColones;
               
                resol.Lstr_TipoTransaccion = TipoTransaccion;
                resol.Lstr_UsrCreacion = UsrCreacion;
                resol.Lint_EstadoPretension = int_EstadoPretension;
                resol.Lstr_EstadoProcesal = EstadoProcesal;
                //****** Acceso a datos en SQL conexion ******/// 
                bool processR = resol.ProcesarSPRegistrarResolucion();//Realizamos el mappeo en la BD

                result[0] = "Codigo :" + resol.Lstr_CodigoResultado;
                result[1] = "Resultado insercion de Resolucion logica de negocio> " + resol.Lstr_MensajeRespuesta;

            }
            catch (Exception ex) {

                //logger 
                llogger_Log.Error("Error RegsitrarResolucion en logica de negocio: " + ex.Message + "- BD Mensaje " + resol.Lstr_MensajeRespuesta);
            
            }
            return result;
        }

        /// <summary>
        /// Registra una resolucion del expediente 
        /// </summary>
        /// <param name="IdResolucion"></param>
        /// <param name="IdExpedienteFK"></param>
        /// <param name="EstadoResolucion"></param>
        /// <param name="FechaResolucion"></param>
        /// <param name="MontoPosibleReembolso"></param>
        /// <param name="Observacion"></param>
        /// <param name="CxCaCxP"></param>
        /// <param name="UsrCreacion"></param>
        /// <returns>string[] result</returns>
        public string[] ModificarResolucion(int IdRes, int IdCobroPagoResolucion, string IdResolucion, string IdExpediente, string IdSociedadGL, string EstadoResolucion, string Estado, DateTime FechaResolucion,
                                            DateTime? PosibleFecSalidaRec, decimal MontoPosibleReembolso, decimal MontoPosReemColones, string Observacion, int CxCaCxP, string Moneda,
                                            decimal TipoCambio, Decimal Tbp, Decimal Tiempo, decimal MontoPrincipal, decimal MontoIntereses,
                                            decimal ValorPresentePrincipal, decimal ValorPresenteIntereses, decimal MontoPrincipalColones, decimal MontoInteresesColones,
                                            string EstadoProcesal, Nullable<Int32> int_EstadoPretension, string UsrModifica)
        {
            string[] result = new string[2];

            clsRegistrarResolucion resol = new clsRegistrarResolucion();

            try
            {
                resol.Lint_IdRes = IdRes;
                resol.Lint_IdCobroPagoResolucion = IdCobroPagoResolucion;
                resol.Lstr_IdResolucion = IdResolucion;
                resol.Lstr_IdExpediente = IdExpediente;
                resol.Lstr_IdSociedadGL = IdSociedadGL;
                resol.Lstr_EstadoResolucion = EstadoResolucion;
                resol.Lstr_Estado = Estado;
                resol.Ldt_FechaResolucion = FechaResolucion;
                resol.Ldt_PosibleFecSalidaRec = PosibleFecSalidaRec;
                resol.Ldec_MontoPosibleReembolso = MontoPosibleReembolso;
                resol.Ldec_MontoPosReemColones = MontoPosReemColones;
                resol.Lstr_Observacion = Observacion;
                resol.Lint_CxCaCxP = CxCaCxP;
                resol.Lstr_Moneda = Moneda;
                resol.Ldec_TipoCambio = TipoCambio;
                resol.Ldec_MontoPrincipal = MontoPrincipal;
                resol.Ldec_MontoIntereses = MontoIntereses;
                resol.Ldec_ValorPresentePrincipal = ValorPresentePrincipal;
                resol.Ldec_ValorPresenteIntereses= ValorPresenteIntereses;
                resol.Ldec_MontoPrincipalColones = MontoPrincipalColones;
                resol.Ldec_MontoInteresesColones = MontoInteresesColones;
                resol.Lint_EstadoPretension = int_EstadoPretension;
                resol.Lstr_EstadoProcesal = EstadoProcesal;
                resol.Lstr_UsrModifica = UsrModifica;
                //****** Acceso a datos en SQL conexion ******/// 
                bool processR = resol.ProcesarSPModificarResolucion();//Realizamos el mappeo en la BD

                result[0] = "Codigo: " + resol.Lstr_CodigoResultado;
                result[1] = "Resultado de modificar Resolucion logica de negocio> " + resol.Lstr_MensajeRespuesta;

            }
            catch (Exception exc)
            {
                result[0] = "Codigo: 99";
                result[1] = "Error: " + resol.Lstr_MensajeRespuesta + exc.Message;

                //logger 
                llogger_Log.Error("Error ModificarResolucion en logica de negocio: " + exc.Message + "- BD Mensaje " + resol.Lstr_MensajeRespuesta);

            }
            return result;
        }
        /// <summary>
        /// Registra una resolucion del expediente 
        /// </summary>
        /// <param name="IdResolucion"></param>
        /// <param name="IdExpedienteFK"></param>
        /// <param name="EstadoResolucion"></param>
        /// <param name="FechaResolucion"></param>
        /// <param name="MontoPosibleReembolso"></param>
        /// <param name="Observacion"></param>
        /// <param name="CxCaCxP"></param>
        /// <param name="UsrCreacion"></param>
        /// <returns>string[] result</returns>
        public string[] ModificarResolucionDeta(int IdRes, int IdCobroPagoResolucion, string IdResolucion, string IdExpediente, string IdSociedadGL, string EstadoResolucion, string Estado, DateTime FechaResolucion,
                                            DateTime? PosibleFecSalidaRec, decimal MontoPosibleReembolso, decimal MontoPosReemColones, string Observacion, int CxCaCxP, string Moneda,
                                            decimal TipoCambio, Decimal Tbp, Decimal Tiempo, decimal MontoPrincipal, decimal MontoIntereses,
                                            decimal ValorPresentePrincipal, decimal ValorPresenteIntereses, decimal MontoPrincipalColones, decimal MontoInteresesColones,
                                            string EstadoProcesal, Nullable<Int32> int_EstadoPretension, string UsrModifica, decimal? InteresesMoratorios=null, decimal ? Costas = null, decimal ? DanoMoral=null,
                                            decimal ?  InteresesMoratoriosColones = null, decimal ? CostasColones=null, decimal ? DanoMoralColones = null, decimal ? ValorPresentePrinColones = null, decimal? ValorPresenteIntColones  = null,
                                            string TipoTransaccion = null, string EstadoTransaccion = null                   
            )
        {
            string[] result = new string[2];

            clsRegistrarResolucion resol = new clsRegistrarResolucion();

            try
            {
                resol.Lint_IdRes = IdRes;
                resol.Lint_IdCobroPagoResolucion = IdCobroPagoResolucion;
                resol.Lstr_IdResolucion = IdResolucion;
                resol.Lstr_IdExpediente = IdExpediente;
                resol.Lstr_IdSociedadGL = IdSociedadGL;
                resol.Lstr_EstadoResolucion = EstadoResolucion;
                resol.Lstr_Estado = Estado;
                resol.Ldt_FechaResolucion = FechaResolucion;
                resol.Ldt_PosibleFecSalidaRec = PosibleFecSalidaRec;
                resol.Ldec_MontoPosibleReembolso = MontoPosibleReembolso;
                resol.Ldec_MontoPosReemColones = MontoPosReemColones;
                resol.Lstr_Observacion = Observacion;
                resol.Lint_CxCaCxP = CxCaCxP;
                resol.Lstr_Moneda = Moneda;
                resol.Ldec_TipoCambio = TipoCambio;
                resol.Ldec_Tbp = Tbp;
                resol.Ldec_Tiempo = Tiempo;
                resol.Ldec_MontoPrincipal = MontoPrincipal;
                resol.Ldec_MontoIntereses = MontoIntereses;
                resol.Ldec_InteresesMoratorios = Convert.ToDecimal( InteresesMoratorios);
                resol.Ldec_Costas = Convert.ToDecimal( Costas);
                resol.Ldec_DanoMoral = Convert.ToDecimal( DanoMoral);
                resol.Ldec_ValorPresentePrincipal = ValorPresentePrincipal;
                resol.Ldec_ValorPresenteIntereses = ValorPresenteIntereses;
                resol.Ldec_MontoPrincipalColones = MontoPrincipalColones;
                resol.Ldec_MontoInteresesColones = MontoInteresesColones;
                resol.Ldec_InteresesMoratoriosColones = Convert.ToDecimal( InteresesMoratoriosColones);
                resol.Ldec_CostasColones = Convert.ToDecimal( CostasColones);
                resol.Ldec_DanoMoralColones = Convert.ToDecimal( DanoMoralColones);
                resol.Ldec_ValorPresentePrinColones = Convert.ToDecimal( ValorPresentePrinColones);
                resol.Ldec_ValorPresenteIntColones = Convert.ToDecimal( ValorPresenteIntColones);
                resol.Lstr_TipoTransaccion = TipoTransaccion;
                resol.Lstr_EstadoTransaccion = EstadoTransaccion;
                resol.Lint_EstadoPretension = int_EstadoPretension;
                resol.Lstr_EstadoProcesal = EstadoProcesal;
                resol.Lstr_UsrModifica = UsrModifica;
                //****** Acceso a datos en SQL conexion ******/// 
                bool processR = resol.ProcesarSPModificarResolucion();//Realizamos el mappeo en la BD

                result[0] = "Codigo: " + resol.Lstr_CodigoResultado;
                result[1] = "Resultado de modificar Resolucion logica de negocio> " + resol.Lstr_MensajeRespuesta;

            }
            catch (Exception exc)
            {
                result[0] = "Codigo: 99";
                result[1] = "Error: " + resol.Lstr_MensajeRespuesta + exc.Message;

                //logger 
                llogger_Log.Error("Error ModificarResolucion en logica de negocio: " + exc.Message + "- BD Mensaje " + resol.Lstr_MensajeRespuesta);

            }
            return result;
        }

        public DataSet ConsultarExpendientesResoluciones(string NumExpediente, string IdSociedadGL, string IdResolucion, out string str_Codigo, out string str_Mensaje)
        {
            DataSet lds_ConsultaResoluciones = new DataSet();
            clsRegistrarResolucion resoluciones = new clsRegistrarResolucion();
            str_Codigo = String.Empty;
            str_Mensaje = String.Empty;

            try
            {
                resoluciones.Lstr_IdExpediente = NumExpediente;
                resoluciones.Lstr_IdSociedadGL = IdSociedadGL;
                resoluciones.Lstr_IdResolucion = IdResolucion;
                
                //****** Acceso a datos en SQL conexion ******/// `
                bool processR = resoluciones.ConsultarExpendientesResoluciones();//Realizamos el mappeo en la BD

                str_Codigo = "Codigo: " + resoluciones.Lstr_CodigoResultado;
                str_Mensaje = "Consulta Resoluciones: " + resoluciones.Lstr_MensajeRespuesta;

                lds_ConsultaResoluciones.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(resoluciones.Lstr_RespuestaSchema)));
                lds_ConsultaResoluciones.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(resoluciones.Lstr_RespuestaXML)));
                return lds_ConsultaResoluciones;
            }
            catch (Exception ex)
            {
                llogger_Log.Error("Error ModificarResolucion en logica de negocio: " + ex.Message + "- BD Mensaje " + resoluciones.Lstr_MensajeRespuesta);
            }

            return lds_ConsultaResoluciones;
        }


        public DataSet ConsultarResolucion(string IdResolucion, string IdExpediente, string IdSociedadGL, out string str_Codigo, out string str_Mensaje)
        {
            DataSet lds_ConsultaResoluciones = new DataSet();
            clsRegistrarResolucion resoluciones = new clsRegistrarResolucion();
            str_Codigo = String.Empty;
            str_Mensaje = String.Empty;

            try
            {
                resoluciones.Lstr_IdResolucion = IdResolucion;
                resoluciones.Lstr_IdExpediente = IdExpediente;
                resoluciones.Lstr_IdSociedadGL = IdSociedadGL;
                //****** Acceso a datos en SQL conexion ******/// 
                bool processR = resoluciones.ConsultarResolucion();//Realizamos el mappeo en la BD

                str_Codigo = "Codigo: " + resoluciones.Lstr_CodigoResultado;
                str_Mensaje = "Consulta Resoluciones: " + resoluciones.Lstr_MensajeRespuesta;

                lds_ConsultaResoluciones.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(resoluciones.Lstr_RespuestaSchema)));
                lds_ConsultaResoluciones.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(resoluciones.Lstr_RespuestaXML)));
                return lds_ConsultaResoluciones;
            }
            catch (Exception ex)
            {
                llogger_Log.Error("Error ModificarResolucion en logica de negocio: " + ex.Message + "- BD Mensaje " + resoluciones.Lstr_MensajeRespuesta);
            }

            return lds_ConsultaResoluciones;
        }

        /// <summary>
        /// Declaracion sin lugar del Expediente
        /// </summary>
        /// <param name="IdExpediente"></param>
        /// <param name="EstadoResolucion"></param>
        /// 
        /// <returns></returns>
        public string[] DeclararsinLugar(string str_IdExpediente,string str_TipoExpediente, string str_EstadoResolucion,int int_CxCaCxP,string Sociedad,string str_UsrCreacion)
       {
           clsRegistrarResolucion regResol = new clsRegistrarResolucion();
           string[] resultado = new string[2];
           
           try
           {
               regResol.Lstr_IdExpedienteFK = str_IdExpediente;
               regResol.Lstr_EstadoResolucion = str_EstadoResolucion;
               regResol.Lstr_TipoExpediente = str_TipoExpediente;
               regResol.Lint_CxCaCxP = int_CxCaCxP;
               regResol.Lstr_Sociedad = Sociedad;
               regResol.Lstr_UsrCreacion = str_UsrCreacion;
               bool  result = regResol.ProcesarSPDeclararSinLugar();
               resultado[0] = "Codigo: " + regResol.Lstr_CodigoResultado;
               resultado[1] = "Resultado declarar sin lugar > " + regResol.Lstr_MensajeRespuesta + " - " + regResol.Lstr_MensajeRespuesta;

           }
           catch (Exception ex)
           {

               //logger 
               llogger_Log.Error("Error RegsitrarResolucion en logica de negocio: " + ex.Message + "- BD Mensaje " + regResol.Lstr_MensajeRespuesta);

           }
           return resultado;
       }


        public DataSet ConsultarCobrosPagos(string lstr_IdExpediente,
            string lstr_IdSociedadGL,
            int lint_IdExp,
            int lint_IdRes,
            string lstr_EstadoResolucion,
            DateTime? ldt_FchInicio,
            DateTime? ldt_FchFin)
        {

            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarCobrosPagos cr_Procedimiento = new clsConsultarCobrosPagos(lstr_IdExpediente, lstr_IdSociedadGL, lint_IdExp, lint_IdRes, lstr_EstadoResolucion, ldt_FchInicio, ldt_FchFin);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch (Exception ex)
            {

                //logger 
                llogger_Log.Error("Error Consultar Cobros Pagos en logica de negocio: " + ex.Message );
            }
            return lds_TablasConsulta;
        }


        #endregion

        #region Metodos Asientos
        public string VerificarExisteProvision(string provisionEstado)
        {
            clsRegistrarResolucion resol = new clsRegistrarResolucion();
            string result = string.Empty;
            try { 
            
                 result=resol.VerificarExisteProvision(provisionEstado);
            
            }catch(Exception err){

                result = "Error al ejecutar la verificacion de existencia de la provision. -" + err.Message;
            }
           

            return result;

        }

        public decimal[] ObtenerMontoResolucion(string idExpediente, string estadopProvision) {
            clsRegistrarResolucion resol = new clsRegistrarResolucion();
            decimal[] result = new decimal[3];
            try {

                result=resol.ObtenerMontoResolucion(idExpediente, estadopProvision);
           
            }catch(Exception err){

                result[0] = 0;
                result[1] = 0;
                result[2] = 0;
            }
            return result;
        }
        #endregion
    }
}