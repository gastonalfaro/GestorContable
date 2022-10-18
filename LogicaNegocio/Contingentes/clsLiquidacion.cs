using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Datos.ConexionSQL.Procedimientos.Contigentes;
using log4net;
using log4net.Config;
using System.Configuration;
using System.Data;

namespace LogicaNegocio.Contingentes
{
    public class clsLiquidacion
    {

        #region parametros
        
        private string lstr_IdExpediente;
        private string lstr_IdSociedadGL;
        private string lstr_EstadoResolucion;

        private String lstr_ResolucionDictada;
        private DateTime? ldt_FchFallo;
        
        private DateTime? ldt_FechaResolucion;
        private int lint_CxCaCxP;
        
        private string lstr_Moneda;
        private decimal ldec_TipoCambio;
        
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

        private string lstr_TipoTransaccion;
        private string lstr_EstadoTransaccion;

        private string lstr_Observacion;

        private string lstr_UsrCreacion;

        #endregion

        #region constructores
        public clsLiquidacion() { }

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

        private String Lstr_ResolucionDictada
        {
            get { return lstr_ResolucionDictada; }
            set { lstr_ResolucionDictada = value; }
        }

        private DateTime? Ldt_FchFallo
        {
            get { return ldt_FchFallo; }
            set { ldt_FchFallo = value; }
        }

        public DateTime? Ldt_FechaResolucion
        {
            get { return ldt_FechaResolucion; }
            set { ldt_FechaResolucion = value; }
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

        private string Lstr_TipoTransaccion
        {
            get { return lstr_TipoTransaccion; }
            set { lstr_TipoTransaccion = value; }
        }

        private string Lstr_EstadoTransaccion
        {
            get { return lstr_EstadoTransaccion; }
            set { lstr_EstadoTransaccion = value; }
        }


        private string Lstr_Observacion
        {
            get { return lstr_Observacion; }
            set { lstr_Observacion = value; }
        }

        private string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }


        #endregion

        public string[] ModificarLiquidacion(
            string str_IdExpediente, string str_IdSociedadGL, string str_EstadoResolucion, 
            DateTime? dt_FchResolucion, 
            DateTime? dt_FchFallo,
            string str_ResolucionDictada,
            int int_CxCaCxP, 
            string srt_Moneda, decimal dec_TipoCambio,
            decimal dec_Intereses, decimal dec_InteresesColones,
            decimal dec_InteresesMoratorios, decimal dec_InteresesMoratoriosColones,
            decimal dec_Costas, decimal dec_CostasColones,
            decimal dec_DannoMoral, decimal dec_DannoMoralColones,
            string str_ObservacionesLiq, string str_Estado, string str_UsrModifica,
            string str_EstadoProcesal, string str_TipoTransaccion, string str_EstadoTransaccion,
            string str_UsrCreacion)
        {
            //variables locales
            clsModificarLiquidacion cls_ModificarLiquidacion = new clsModificarLiquidacion();
            string[] lstr_result = new string[2];
            //insertar codigo de mappeo de datos
            try
            {
                cls_ModificarLiquidacion.Lstr_IdExpediente = str_IdExpediente;
                cls_ModificarLiquidacion.Lstr_IdSociedadGL = str_IdSociedadGL;
                cls_ModificarLiquidacion.Lstr_EstadoResolucion = str_EstadoResolucion;

                cls_ModificarLiquidacion.Ldt_FechaResolucion = dt_FchResolucion;

                cls_ModificarLiquidacion.Ldt_FchFallo = dt_FchFallo;
                cls_ModificarLiquidacion.Lstr_ResolucionDictada = str_ResolucionDictada;

                cls_ModificarLiquidacion.Lint_CxCaCxP = int_CxCaCxP;
                cls_ModificarLiquidacion.Lstr_Moneda = srt_Moneda;
                cls_ModificarLiquidacion.Ldec_TipoCambio = dec_TipoCambio;

                cls_ModificarLiquidacion.Ldec_Intereses = dec_Intereses;
                cls_ModificarLiquidacion.Ldec_InteresesColones = dec_InteresesColones;

                cls_ModificarLiquidacion.Ldec_InteresesMoratorios = dec_InteresesMoratorios;
                cls_ModificarLiquidacion.Ldec_InteresesMoratoriosColones = dec_InteresesMoratoriosColones;

                cls_ModificarLiquidacion.Ldec_Costas = dec_Costas;
                cls_ModificarLiquidacion.Ldec_CostasColones = dec_CostasColones;

                cls_ModificarLiquidacion.Ldec_DanoMoral = dec_DannoMoral;
                cls_ModificarLiquidacion.Ldec_DanoMoralColones = dec_DannoMoralColones;

                cls_ModificarLiquidacion.Lstr_ObservacionesLiq = str_ObservacionesLiq;
                cls_ModificarLiquidacion.Lstr_Estado = str_Estado;
                cls_ModificarLiquidacion.Lstr_UsrModifica = str_UsrModifica;
                cls_ModificarLiquidacion.Lstr_EstadoProcesal = str_EstadoProcesal;

                //****** Acceso a datos en SQL conexion ******/// 
                bool process = cls_ModificarLiquidacion.ModificarLiquidacion();//Realizamos el mappeo en la BD
                //******* Resultado de procesar store procedure ****//
                lstr_result[0] = cls_ModificarLiquidacion.Lstr_CodigoResultado;
                lstr_result[1] = cls_ModificarLiquidacion.Lstr_MensajeRespuesta;

            }
            catch (Exception ex)
            {
                lstr_result[0] = "Codigo error: 99";
                lstr_result[1] = "Resultado insercion de pretetension inicial en logica de negocio> " + cls_ModificarLiquidacion.Lstr_MensajeRespuesta;

            }

            return lstr_result;

        }

        public string[] RegistrarLiquidacion(
            string str_IdExpediente, string str_IdSociedadGL, string str_EstadoResolucion,
            DateTime? dt_FchResolucion,
            DateTime? dt_FchFallo,
            string str_ResolucionDictada,
            int int_CxCaCxP,
            string str_EstadoProcesal, string str_Estado, 
            string srt_Moneda, decimal dec_TipoCambio,
            decimal dec_Intereses, decimal dec_InteresesColones,
            decimal dec_InteresesMoratorios, decimal dec_InteresesMoratoriosColones,
            decimal dec_Costas, decimal dec_CostasColones,
            decimal dec_DannoMoral, decimal dec_DannoMoralColones,
            string str_TipoTransaccion, string str_EstadoTransaccion,
            string str_Observacion,
            string str_UsrCreacion
            )
        {
            //variables locales
            clsRegistrarLiquidacion cls_RegistrarLiquidacion = new clsRegistrarLiquidacion();
            string[] lstr_result = new string[2];
            //insertar codigo de mappeo de datos
            try
            {
                cls_RegistrarLiquidacion.Lstr_IdExpediente = str_IdExpediente;
                cls_RegistrarLiquidacion.Lstr_IdSociedadGL = str_IdSociedadGL;
                cls_RegistrarLiquidacion.Lstr_EstadoResolucion = str_EstadoResolucion;

                cls_RegistrarLiquidacion.Ldt_FechaResolucion = dt_FchResolucion;

                cls_RegistrarLiquidacion.Ldt_FchFallo = dt_FchFallo;
                cls_RegistrarLiquidacion.Lstr_ResolucionDictada = str_ResolucionDictada;

                cls_RegistrarLiquidacion.Lint_CxCaCxP = int_CxCaCxP;
                cls_RegistrarLiquidacion.Lstr_Moneda = srt_Moneda;
                cls_RegistrarLiquidacion.Ldec_TipoCambio = dec_TipoCambio;

                cls_RegistrarLiquidacion.Ldec_Intereses = dec_Intereses;
                cls_RegistrarLiquidacion.Ldec_InteresesColones = dec_InteresesColones;

                cls_RegistrarLiquidacion.Ldec_InteresesMoratorios = dec_InteresesMoratorios;
                cls_RegistrarLiquidacion.Ldec_InteresesMoratoriosColones = dec_InteresesMoratoriosColones;

                cls_RegistrarLiquidacion.Ldec_Costas = dec_Costas;
                cls_RegistrarLiquidacion.Ldec_CostasColones = dec_CostasColones;

                cls_RegistrarLiquidacion.Ldec_DanoMoral = dec_DannoMoral;
                cls_RegistrarLiquidacion.Ldec_DanoMoralColones = dec_DannoMoralColones;

                cls_RegistrarLiquidacion.Lstr_Estado = str_Estado;
                cls_RegistrarLiquidacion.Lstr_EstadoProcesal = str_EstadoProcesal;

                cls_RegistrarLiquidacion.Lstr_Observacion = str_Observacion;

                bool process = cls_RegistrarLiquidacion.RegistrarLiquidacion();

                if (String.IsNullOrEmpty(cls_RegistrarLiquidacion.Lstr_CodigoResultado))
                    lstr_result[0] = "99";
                else
                    lstr_result[0] = cls_RegistrarLiquidacion.Lstr_CodigoResultado;
                lstr_result[1] = cls_RegistrarLiquidacion.Lstr_MensajeRespuesta;

            }
            catch (Exception ex)
            {
                lstr_result[0] = "99";
                lstr_result[1] = "Error en Logica: " + cls_RegistrarLiquidacion.Lstr_MensajeRespuesta;
            }

            return lstr_result;

        }
        


        
        
   }
}