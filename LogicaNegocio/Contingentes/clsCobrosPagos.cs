using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Datos.ConexionSQL.Procedimientos.Contigentes;
using System.Data;
using LogicaNegocio.Mantenimiento;

namespace LogicaNegocio.Contingentes
{
    public class clsCobrosPagos
    {
        #region parametros

        private string lstr_IdExpediente;
        private string lstr_IdSociedadGL;
        private Int32 lint_IdRes;

        private String lstr_Moneda;
        private decimal ldec_TipoCambio;
        private decimal ldec_Tbp;
        private decimal ldec_Tiempo;
        private decimal ldec_TipoCambioCierre;

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

        private string lstr_UsrModifica;
        private string lstr_EstadoProcesal;


        private int lint_IdCobroPago;//Identificador único de los registros de cobro y pago
        private string lstr_IdResolucionFK;//Identificador para ligar los pagos a las resoluciones dictadas



        private int lint_EstadoTransaccion;//Indica el estado de la transacción, si aún sigue en cobro, o bien si ya se cerró.
        private int lint_TipoTransaccion;//Es el tipo de transacción que se está realizando, si es un cobro o un pago. Va ligado a la resolución, y al expediente en si, este campo es meramente informativo
        private string lstr_UsrCreacion;//Campo de Auditoría: Indica el usuario que creó el registro


        #endregion

        #region constructores
        public clsCobrosPagos(){}
        
        public int Lint_IdCobroPago
        {
            get { return lint_IdCobroPago; }
            set { lint_IdCobroPago = value; }
        }
        public string Lstr_IdResolucionFK
        {
            get { return lstr_IdResolucionFK; }
            set { lstr_IdResolucionFK = value; }
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

        private decimal Ldec_Tbp
        {
            get { return ldec_Tbp; }
            set { ldec_Tbp = value; }
        }
        private decimal Ldec_Tiempo
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
        public Decimal? Ldec_ValorPresenteoPrincipalColones
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

        public int Lint_TipoTransaccion
        {
            get { return lint_TipoTransaccion; }
            set { lint_TipoTransaccion = value; }
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

        #region Metodos

        public bool ModificarCodigoAsiento(int? lint_IdRes, int? lint_IdCobroPagoResolucion, string lstr_IdResolucion, string lstr_IdExpediente, string lstr_IdSociedadGL, 
            string lstr_CodAsiento, string lstr_UsrModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsModificarCodigosAsientoCo cr_Procedimiento = new clsModificarCodigosAsientoCo(lint_IdRes, lint_IdCobroPagoResolucion, lstr_IdResolucion, lstr_IdExpediente, lstr_IdSociedadGL, 
                "COBROSPAGOS", lstr_CodAsiento, lstr_UsrModifica);

                str_CodResultado = cr_Procedimiento.Lstr_CodigoResultado;
                str_Mensaje = cr_Procedimiento.Lstr_MensajeRespuesta;

                //Log.Info(str_Mensaje);
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_ResCreacion = true;
                }
            }
            catch (Exception ex)
            {
                str_CodResultado = "99";
                str_Mensaje = ex.ToString();
            }
            return bool_ResCreacion;
        }


        public string[] ModificarCobrosPagosArchivo(
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
         string lstr_UsrModifica,
            string lstr_Origen = "Judicial")
        {
            string[] result = new string[2];
            result[0] = "00";

            clsModificarCobrosPagosArchivo resol = new clsModificarCobrosPagosArchivo();

            try
            {
                //****** Acceso a datos en SQL conexion ******/// 
                

                if (lstr_Origen == "Judicial")
                {
                    clsTiposAsiento tasiento = new clsTiposAsiento();
                    result = tasiento.EnviarAsientosCICTJudicial(lstr_IdExpediente,//Llave que relaciona las resoluciones dictadas, con los expedientes existentes
                     lstr_IdSociedadGL,
                     lstr_EstadoResolucion,//Campo que define qué tipo de resolución es, Resolución Provisional 1 ó 2,  Resolución en Firme (aplica tanto para activos contingentes como para pasivos contingentes), o Declaración Sin Lugar.
                     lstr_Moneda,//La moneda en la cual se recibe el cobro. Campo obligatorio
                      ldec_TipoCambio,//El tipo de cambio al momento de incluirlo en el sistema.
                      ldec_MontoPrincipal,//Es el monto principal a cobrar/pagar
                      ldec_MontoPrincipalColones,//Monto principal a cobrar/pagar en colones
                      ldec_Intereses,
                      ldec_InteresesColones,
                      ldec_InteresesMoratorios,
                      ldec_InteresesMoratoriosColones,
                      ldec_Costas,
                      ldec_CostasColones,
                      ldec_DanoMoral,
                      ldec_DanoMoralColones, 
                      "Judicial");
                }
                if (result[0] == "00")
                {
                    bool processR = resol.ModificarCobrosPagosArchivo(
                     lint_IdRes,
                     lint_IdCobroPagoResolucion,
                     lstr_IdResolucion,//Identificador único de la resolución dictada en los tribunales de justicia
                     lstr_IdExpediente,//Llave que relaciona las resoluciones dictadas, con los expedientes existentes
                     lstr_IdSociedadGL,
                     lstr_EstadoResolucion,//Campo que define qué tipo de resolución es, Resolución Provisional 1 ó 2,  Resolución en Firme (aplica tanto para activos contingentes como para pasivos contingentes), o Declaración Sin Lugar.
                     lstr_Moneda,//La moneda en la cual se recibe el cobro. Campo obligatorio
                     ldec_TipoCambio,//El tipo de cambio al momento de incluirlo en el sistema.
                     ldec_MontoPrincipal,//Es el monto principal a cobrar/pagar
                     ldec_MontoPrincipalColones,//Monto principal a cobrar/pagar en colones
                     ldec_Intereses,
                     ldec_InteresesColones,
                     ldec_InteresesMoratorios,
                     ldec_InteresesMoratoriosColones,
                     ldec_Costas,
                     ldec_CostasColones,
                     ldec_DanoMoral,
                     ldec_DanoMoralColones,
                     lstr_UsrModifica);//Realizamos el mappeo en la BD
                    result[0] = resol.Lstr_CodigoResultado;
                    result[1] = resol.Lstr_MensajeRespuesta;
                }
            }
            catch (Exception exc)
            {
                result[0] = "99";
                result[1] = "Error: " + resol.Lstr_MensajeRespuesta + exc.Message;

                //logger 
                //llogger_Log.Error("Error ModificarResolucion en logica de negocio: " + exc.Message + "- BD Mensaje " + resol.Lstr_MensajeRespuesta);

            }
            return result;
        }

        public String[] ModificarCobrosPagos(string str_IdExpediente, string str_IdSociedadGL, Int32 int_IdRes, 
            String str_Moneda, Decimal dec_TipoCambio, Decimal dec_Tbp, Decimal dec_Tiempo, Decimal dec_TipoCambioCierre,
            Decimal? dec_MontoPrincipal, Decimal? dec_MontoPrincipalColones, Decimal? dec_MontoPrincipalCierre,
            Decimal? dec_MontoIntereses, Decimal? dec_MontoInteresesColones, Decimal? dec_MontoInteresesCierre,
           
            Decimal? dec_ValorPresentePrincipal, Decimal? dec_ValorPresentePrincipalColones, Decimal? dec_ValorPresentePrincipalCierre,
            Decimal? dec_ValorPresenteIntereses, Decimal? dec_ValorPresenteInteresesColones, Decimal? dec_ValorPresenteInteresesCierre,
            
            Decimal? dec_Intereses, Decimal? dec_InteresesColones, Decimal? dec_InteresesCierre,
            Decimal? dec_InteresesMoratorios, Decimal? dec_InteresesMoratoriosColones, Decimal? dec_InteresesMoratoriosCierre,
            Decimal? dec_Costas, Decimal? dec_CostasColones, Decimal? dec_CostasCierre,
            Decimal? dec_DanoMoral, Decimal? dec_DanoMoralColones, Decimal? dec_DanoMoralCierre,

            Decimal? dec_MontoPrincipalAnterior, Decimal? dec_MontoInteresesAnterior, Decimal? dec_InteresesAnterior, Decimal? dec_CostasAnterior, Decimal? dec_InteresesMoratoriosAnterior, Decimal? dec_DanoMoralAnterior,
            String str_UsrModifica, String str_EstadoProcesal
            )
        {
            String[] lstr_resultado = new string[2];
            clsModificarCobrosPagos cls_ModificarCobrosPagos = new clsModificarCobrosPagos();

            try
            {
                cls_ModificarCobrosPagos.Lstr_IdExpediente = str_IdExpediente;
                cls_ModificarCobrosPagos.Lstr_IdSociedadGL = str_IdSociedadGL;
                cls_ModificarCobrosPagos.Lint_IdRes = int_IdRes;

                cls_ModificarCobrosPagos.Lstr_Moneda = str_Moneda;
                cls_ModificarCobrosPagos.Ldec_TipoCambio = dec_TipoCambio;
                cls_ModificarCobrosPagos.Ldec_Tbp = dec_Tbp;
                cls_ModificarCobrosPagos.Ldec_Tiempo = dec_Tiempo;
                cls_ModificarCobrosPagos.Ldec_TipoCambioCierre = dec_TipoCambioCierre;

                cls_ModificarCobrosPagos.Ldec_MontoPrincipal = dec_MontoPrincipal;
                cls_ModificarCobrosPagos.Ldec_MontoPrincipalColones = dec_MontoPrincipalColones;
                cls_ModificarCobrosPagos.Ldec_MontoPrincipalCierre = dec_MontoPrincipalCierre;

                cls_ModificarCobrosPagos.Ldec_MontoIntereses = dec_MontoIntereses;
                cls_ModificarCobrosPagos.Ldec_MontoInteresesColones = dec_MontoInteresesColones;
                cls_ModificarCobrosPagos.Ldec_MontoInteresesCierre = dec_MontoInteresesCierre;

                cls_ModificarCobrosPagos.Ldec_ValorPresentePrincipal = dec_ValorPresentePrincipal;
                cls_ModificarCobrosPagos.Ldec_ValorPresentePrincipalColones = dec_ValorPresentePrincipalColones;
                cls_ModificarCobrosPagos.Ldec_ValorPresentePrincipalCierre = dec_ValorPresentePrincipalCierre;

                cls_ModificarCobrosPagos.Ldec_ValorPresenteIntereses = dec_ValorPresenteIntereses;
                cls_ModificarCobrosPagos.Ldec_ValorPresenteInteresesColones = dec_ValorPresenteInteresesColones;
                cls_ModificarCobrosPagos.Ldec_ValorPresenteInteresesCierre = dec_ValorPresenteInteresesCierre;

                cls_ModificarCobrosPagos.Ldec_Intereses = dec_Intereses;
                cls_ModificarCobrosPagos.Ldec_InteresesColones = dec_InteresesColones;
                cls_ModificarCobrosPagos.Ldec_InteresesCierre = dec_InteresesCierre;

                cls_ModificarCobrosPagos.Ldec_InteresesMoratorios = dec_InteresesMoratorios;
                cls_ModificarCobrosPagos.Ldec_InteresesMoratoriosColones = dec_InteresesMoratoriosColones;
                cls_ModificarCobrosPagos.Ldec_InteresesMoratoriosCierre = dec_InteresesMoratoriosCierre;

                cls_ModificarCobrosPagos.Ldec_Costas = dec_Costas;
                cls_ModificarCobrosPagos.Ldec_CostasColones = dec_CostasColones;
                cls_ModificarCobrosPagos.Ldec_CostasCierre = dec_CostasCierre;

                cls_ModificarCobrosPagos.Ldec_DanoMoral = dec_DanoMoral;
                cls_ModificarCobrosPagos.Ldec_DanoMoralColones = dec_DanoMoralColones;
                cls_ModificarCobrosPagos.Ldec_DanoMoralCierre = dec_DanoMoralCierre;
                 

                cls_ModificarCobrosPagos.Ldec_MontoPrincipalAnterior = dec_MontoPrincipalAnterior;
                cls_ModificarCobrosPagos.Ldec_MontoInteresesAnterior  = dec_MontoInteresesAnterior;
                cls_ModificarCobrosPagos.Ldec_InteresesAnterior = dec_InteresesAnterior;
                cls_ModificarCobrosPagos.Ldec_CostasAnterior = dec_CostasAnterior;
                cls_ModificarCobrosPagos.Ldec_InteresesMoratoriosAnterior = dec_InteresesMoratoriosAnterior;
                cls_ModificarCobrosPagos.Ldec_DanoMoralAnterior = dec_DanoMoralAnterior;
                
                cls_ModificarCobrosPagos.Lstr_UsrModifica = str_UsrModifica;
                cls_ModificarCobrosPagos.Lstr_EstadoProcesal = str_EstadoProcesal;

                bool process = cls_ModificarCobrosPagos.ModificarCobrosPagos();

                lstr_resultado[0] = cls_ModificarCobrosPagos.Lstr_CodigoResultado;
                lstr_resultado[1] = cls_ModificarCobrosPagos.Lstr_MensajeRespuesta;
            }
            catch (Exception exp)
            {
                lstr_resultado[0] = "99";
                lstr_resultado[1] = cls_ModificarCobrosPagos.Lstr_MensajeRespuesta;

            }
            return lstr_resultado;
        }


        public string[] RegistrarCobrosPagos(
            string IdResolucion, string IdExpedienteFK, int IDRes,
            string Moneda, decimal TipoCambio, decimal Tbp, decimal Tiempo, decimal TipoCambioCierre,

            decimal MontoPrincipal, decimal MontoPrincipalColones, decimal MontoPrincipalCierre,
            decimal MontoIntereses, decimal MontoInteresesColones, decimal MontoInteresesCierre,

            decimal ValorPresentePrincipal, decimal ValorPresentePrinColones, decimal ValorPresentePrinCierre,
            decimal ValorPresenteIntereses, decimal ValorPresenteIntColones,  decimal ValorPresenteIntCierre,

            decimal Intereses, decimal InteresesColones, decimal InteresesCierre,
            decimal InteresesMoratorios, decimal InteresesMoratoriosColones, decimal InteresesMoratoriosCierre,
            decimal Costas, decimal CostasColones, decimal CostasCierre,
            decimal DanoMoral, decimal DanoMoralColones, decimal DanoMoralCierre,

            Decimal? dec_InteresesAnterior, Decimal? dec_CostasAnterior, Decimal? dec_InteresesMoratoriosAnterior, Decimal? dec_DanoMoralAnterior,
            
            string TipoTransaccion,
            string EstadoTransaccion,
            DateTime? FechFalloResol,
            string Observaciones,
            string UsrCreacion)
        {
            string[] result = new string[2];

            clsRegistrarResolucion resol = new clsRegistrarResolucion();

            try
            {
                resol.Lstr_IdResolucion = IdResolucion;
                resol.Lstr_IdExpediente = IdExpedienteFK;
                resol.Lint_IdRes = IDRes;                
                resol.Lstr_Moneda = Moneda;
                resol.Ldec_TipoCambio = TipoCambio;
                resol.Ldec_Tbp = Tbp;
                resol.Ldec_Tiempo = Tiempo;
                resol.Ldec_TipoCambioCierre = TipoCambioCierre;

                resol.Ldec_MontoPrincipal = MontoPrincipal;
                resol.Ldec_MontoPrincipalColones = MontoPrincipalColones;
                resol.Ldec_MontoPrincipalCierre = MontoPrincipalCierre;     
           
                resol.Ldec_MontoIntereses = MontoIntereses;
                resol.Ldec_MontoInteresesColones = MontoInteresesColones;
                resol.Ldec_MontoInteresesCierre = MontoInteresesCierre;

                resol.Ldec_ValorPresentePrincipal = ValorPresentePrincipal;
                resol.Ldec_ValorPresentePrinColones = ValorPresentePrinColones;
                resol.Ldec_ValorPresentePrinCierre = ValorPresentePrinCierre; 
              
                resol.Ldec_ValorPresenteIntereses = ValorPresenteIntereses;
                resol.Ldec_ValorPresenteIntColones = ValorPresenteIntColones;
                resol.Ldec_ValorPresenteIntCierre = ValorPresenteIntCierre;  
              
                resol.Ldec_Intereses = Intereses;
                resol.Ldec_InteresesColones = InteresesColones;
                resol.Ldec_InteresesCierre = InteresesCierre; 
               
                resol.Ldec_InteresesMoratorios = InteresesMoratorios;
                resol.Ldec_InteresesMoratoriosColones = InteresesMoratoriosColones;
                resol.Ldec_InteresesMoratoriosCierre = InteresesMoratoriosCierre;      
          
                resol.Ldec_Costas = Costas;
                resol.Ldec_CostasColones = CostasColones;
                resol.Ldec_CostasCierre = CostasCierre;

                resol.Ldec_DanoMoral = DanoMoral;
                resol.Ldec_DanoMoralColones = DanoMoralColones;
                resol.Ldec_DanoMoralCierre = DanoMoralCierre;
       
                //resol.Ldec_MontoPrincipalAnterior = ;
                //resol.Ldec_MontoInteresesAnterior = ;
                resol.Ldec_InteresesAnterior = dec_InteresesAnterior;
                resol.Ldec_InteresesMoratoriosAnterior = dec_InteresesMoratoriosAnterior;
                resol.Ldec_CostasAnterior = dec_CostasAnterior;
                resol.Ldec_DanoMoralAnterior = dec_DanoMoralAnterior;

                resol.Lstr_TipoTransaccion = TipoTransaccion;
                resol.Lstr_EstadoTransaccion = EstadoTransaccion;
                resol.Ldt_FechFalloResol = FechFalloResol;
                resol.Lstr_Observacion = Observaciones;
                resol.Lstr_UsrCreacion = UsrCreacion;

                ////if (Moneda != "CRC")
                ////{
                ////    if (MontoPrincipalColones != MontoPrincipal * TipoCambio)
                ////        resol.Ldec_MontoPrincipalColones = MontoPrincipal * TipoCambio;
                ////    if (MontoInteresesColones != MontoIntereses * TipoCambio)
                ////        resol.Ldec_MontoInteresesColones = MontoIntereses * TipoCambio;
                ////    if (InteresesMoratoriosColones != InteresesMoratorios * TipoCambio)
                ////        resol.Ldec_InteresesMoratoriosColones = InteresesMoratorios * TipoCambio;
                ////    if (ValorPresentePrinColones != ValorPresentePrincipal * TipoCambio)
                ////        resol.Ldec_ValorPresentePrinColones = ValorPresentePrincipal * TipoCambio;
                ////    //if (ValorPresenteIntColones != ValorPresenteIntColones * TipoCambio)
                ////    //    resol.Ldec_ValorPresenteIntColones = ValorPresenteIntereses * TipoCambio;
                ////    if (CostasColones != Costas * TipoCambio)
                ////        resol.Ldec_CostasColones = Costas * TipoCambio;
                ////    if (DanoMoralColones != DanoMoral * TipoCambio)
                ////        resol.Ldec_DanoMoralColones = DanoMoral * TipoCambio;
                ////}
                //****** Acceso a datos en SQL conexion ******/// 
                bool processR = resol.ProcesarSPCobrosPagos();//Realizamos el mappeo en la BD

                result[0] = "Codigo:" + resol.Lstr_CodigoResultado;
                result[1] = "Resultado insercion de Cobros Pagos a la Resolucion logica de negocio> " + resol.Lstr_MensajeRespuesta;

            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public DataSet ConsultarCobrosPagos(
            String str_IdExpediente,
            String str_IdSociedadGL,
            Int32 int_IdExp,
            Int32 int_IdRes,
            string str_EstadoResolucion,
            DateTime? dt_FchInicio,
            DateTime? dt_FchFin)
        {
            DataSet lds_TablasConsulta = new DataSet();
            String[] lstr_resultado = new string[2];
            clsConsultarCobrosPagos cls_ConsultarCobrosPagos = new clsConsultarCobrosPagos();

            try
            {
                cls_ConsultarCobrosPagos.Lstr_IdExpediente = str_IdExpediente;
                cls_ConsultarCobrosPagos.Lstr_IdSociedadGL = str_IdSociedadGL;

                cls_ConsultarCobrosPagos.Lint_IdExp = int_IdExp;
                cls_ConsultarCobrosPagos.Lint_IdRes = int_IdRes;
                cls_ConsultarCobrosPagos.Lstr_EstadoResolucion = str_EstadoResolucion;

                cls_ConsultarCobrosPagos.Ldt_FchInicio = dt_FchInicio;
                cls_ConsultarCobrosPagos.Ldt_FchFin = dt_FchFin;

                cls_ConsultarCobrosPagos.ConsultarCobrosPagos();

                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_ConsultarCobrosPagos.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cls_ConsultarCobrosPagos.Lstr_RespuestaXML)));
           

                lstr_resultado[0] = cls_ConsultarCobrosPagos.Lstr_CodigoResultado;
                lstr_resultado[1] = cls_ConsultarCobrosPagos.Lstr_MensajeRespuesta;
            }
            catch (Exception exp)
            {
                lstr_resultado[0] = "99";
                lstr_resultado[1] = cls_ConsultarCobrosPagos.Lstr_MensajeRespuesta;

            }
            return lds_TablasConsulta;
        }
        #endregion
    }
}