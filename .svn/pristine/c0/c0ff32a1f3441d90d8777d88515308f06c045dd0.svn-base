using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Datos.ConexionSQL.Procedimientos.Contigentes;
using log4net;
using log4net.Config;
using System.Configuration;
using System.Data;


namespace Datos.ConexionSQL.Procedimientos.Contigentes
{
    public class clsRegistrarLiquidacion : clsProcedimientoAlmacenado
    {

        #region parametros
        
        private string lstr_IdExpediente;
        private string lstr_IdSociedadGL;
        private string lstr_EstadoResolucion;
        
        private DateTime? ldt_FechaResolucion;//Fecha en la que se dio el fallo campo 

        private String lstr_ResolucionDictada;
        private DateTime? ldt_FchFallo;

        private int lint_CxCaCxP;//Definición de estatus del expediente, si es un activo contingente o pasivo contingente. Es un auxiliar del campo TipoExpediente del expediente, conserva el estado de la resolución anterior para consulta, y permite el cambio de estado del expediente.
        private string lstr_EstadoProcesal;
        private string lstr_Estado;

        private string lstr_Moneda;//La moneda en la cual se recibe el cobro. Campo obligatorio
        private decimal ldec_TipoCambio;//El tipo de cambio al momento de incluirlo en el sistema.
        
        private decimal ldec_Intereses;//Es el monto de intereses a cobrar/pagar
        private decimal ldec_InteresesColones;//Monto de interese a cobrar/pagar en colones
        
        private decimal ldec_InteresesMoratorios;//Intereses causados por la moratoria
        private decimal ldec_InteresesMoratoriosColones;//Interese moratorios en colones
        
        private decimal ldec_Costas;//Costas
        private decimal ldec_CostasColones;//Costas en colones
        
        private decimal ldec_DanoMoral;//Daños morales
        private decimal ldec_DanoMoralColones;//Daño moral en colones
        
        private string lstr_TipoTransaccion;//Es el tipo de transacción que se está realizando, si es un cobro o un pago. Va ligado a la resolución, y al expediente en si, este campo es meramente informativo
        private string lstr_EstadoTransaccion;//Indica el estado de la transacción, si aún sigue en cobro, o bien si ya se cerró.

        private string lstr_Observacion;
        private string lstr_UsrCreacion;//Campo de Auditoría: Indica el usuario que creó el registro

        #endregion

        #region constructores

        public clsRegistrarLiquidacion(){}


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

        

        public DateTime? Ldt_FechaResolucion
        {
            get { return ldt_FechaResolucion; }
            set { ldt_FechaResolucion = value; }
        }

        public String Lstr_ResolucionDictada
        {
            get { return lstr_ResolucionDictada; }
            set { lstr_ResolucionDictada = value; }
        }

        public DateTime? Ldt_FchFallo
        {
            get { return ldt_FchFallo; }
            set { ldt_FchFallo = value; }
        }

        public int Lint_CxCaCxP
        {
            get { return lint_CxCaCxP; }
            set { lint_CxCaCxP = value; }
        }

        public string Lstr_EstadoProcesal
        {
            get { return lstr_EstadoProcesal; }
            set { lstr_EstadoProcesal = value; }
        }
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
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
        
        public decimal Ldec_Intereses
        {
            get { return ldec_Intereses; }
            set { ldec_Intereses = value; }
        }
        public decimal Ldec_InteresesColones
        {
            get { return ldec_InteresesColones; }
            set { ldec_InteresesColones = value; }
        }

        public decimal Ldec_InteresesMoratorios
        {
            get { return ldec_InteresesMoratorios; }
            set { ldec_InteresesMoratorios = value; }
        }
        public decimal Ldec_InteresesMoratoriosColones
        {
            get { return ldec_InteresesMoratoriosColones; }
            set { ldec_InteresesMoratoriosColones = value; }
        }

        public decimal Ldec_Costas
        {
            get { return ldec_Costas; }
            set { ldec_Costas = value; }
        }
        public decimal Ldec_CostasColones
        {
            get { return ldec_CostasColones; }
            set { ldec_CostasColones = value; }
        }

        public decimal Ldec_DanoMoral
        {
            get { return ldec_DanoMoral; }
            set { ldec_DanoMoral = value; }
        }
        public decimal Ldec_DanoMoralColones
        {
            get { return ldec_DanoMoralColones; }
            set { ldec_DanoMoralColones = value; }
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


        public string Lstr_Observacion
        {
            get { return lstr_Observacion; }
            set { lstr_Observacion = value; }
        }

        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }
  
        #endregion

        #region metodos
        
        public bool RegistrarLiquidacion()
        {
            //string str_RutaArchivo = @"C:\PwcProyectos\SistemaGestor\Datos\ConexionSQL\Configs\Contigentes\Resoluciones\RegistrarResolucion.config";
            string str_RutaArchivo=string.Empty;
            string str_DireccionConfigs = string.Empty;
            bool resultFlag=false;
            try { 
                  
                var appSettings = ConfigurationManager.AppSettings;
                str_DireccionConfigs = appSettings["DireccionConfigs"];
                str_RutaArchivo = str_DireccionConfigs + "\\Contigentes\\Resoluciones\\RegistrarLiquidacion.config";
                EjecucionSP(str_RutaArchivo, this);
                resultFlag = true;
            }

            catch (Exception ex)
            {
                resultFlag = false;
                this.Lstr_CodigoResultado = "99";
                this.Lstr_MensajeRespuesta = "Error en Conexion: " + ex.Message;
            }

            return resultFlag;
        }

        #endregion
        
        
   }
}