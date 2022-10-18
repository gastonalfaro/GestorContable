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
    public class clsModificarLiquidacion : clsProcedimientoAlmacenado
    {

        #region parametros
        
        private string lstr_IdExpediente;
        private string lstr_IdSociedadGL;
        private string lstr_EstadoResolucion;
        
        private DateTime? ldt_FechaResolucion;//Fecha en la que se dio el fallo campo 

        private String lstr_ResolucionDictada;
        private DateTime? ldt_FchFallo;

        private int lint_CxCaCxP;//Definición de estatus del expediente, si es un activo contingente o pasivo contingente. Es un auxiliar del campo TipoExpediente del expediente, conserva el estado de la resolución anterior para consulta, y permite el cambio de estado del expediente.
        
        private string lstr_Moneda;//La moneda en la cual se recibe el cobro. Campo obligatorio
        private decimal ldec_TipoCambio;//El tipo de cambio al momento de incluirlo en el sistema.
        
        private decimal ldec_Intereses;
        private decimal ldec_InteresesColones;

        private decimal ldec_InteresesMoratorios;
        private decimal ldec_InteresesMoratoriosColones;

        private decimal ldec_Costas;
        private decimal ldec_CostasColones;

        private decimal ldec_DanoMoral;
        private decimal ldec_DanoMoralColones;

        private string lstr_ObservacionesLiq;

        private string lstr_Estado;
        private string lstr_UsrModifica;
        private string lstr_EstadoProcesal;
        #endregion

        #region constructores
        public clsModificarLiquidacion() { }

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

        public string Lstr_ObservacionesLiq
        {
            get { return lstr_ObservacionesLiq; }
            set { lstr_ObservacionesLiq = value; }
        }
        
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
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

        #endregion

        public bool ModificarLiquidacion()
        {
            string str_RutaArchivo = string.Empty;
            string str_DireccionConfigs = string.Empty;
            bool resultFlag = false;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                str_DireccionConfigs = appSettings["DireccionConfigs"];
                str_RutaArchivo = str_DireccionConfigs + "\\Contigentes\\Resoluciones\\ModificarLiquidacion.config";
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