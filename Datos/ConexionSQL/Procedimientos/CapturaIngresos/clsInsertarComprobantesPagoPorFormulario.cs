using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CapturaIngresos
{
    public class clsInsertarComprobantesPagoPorFormulario : clsProcedimientoAlmacenado
    {


        #region Parámetros

        private int lint_IdFormulario;
        private int lint_Anno;
        private string lstr_NumComprobante;
        private DateTime ldt_FchComprobante;
        private string lstr_IdBanco;
        private string lstr_IdMoneda;
        private decimal ldec_Monto;
        private string lstr_Observaciones;
        private string lstr_Usuario;
        private DateTime ldt_FchModifica;

        #endregion

        #region Obtención y asignación
                
        public int Lint_IdFormulario
        {
            get { return lint_IdFormulario; }
            set { lint_IdFormulario = value; }
        }

        public int Lint_Anno
        {
            get { return lint_Anno; }
            set { lint_Anno = value; }
        }

        public string Lstr_NumComprobante
        {
            get { return lstr_NumComprobante; }
            set { lstr_NumComprobante = value; }
        }
       
        public DateTime Ldt_FchComprobante
        {
            get { return ldt_FchComprobante; }
            set { ldt_FchComprobante = value; }
        }
        
        public string Lstr_IdBanco
        {
            get { return lstr_IdBanco; }
            set { lstr_IdBanco = value; }
        }

      
        public string Lstr_IdMoneda
        {
            get { return lstr_IdMoneda; }
            set { lstr_IdMoneda = value; }
        }
  
        public decimal Ldec_Monto
        {
            get { return ldec_Monto; }
            set { ldec_Monto = value; }
        }

        public string Lstr_Observaciones
        {
            get { return lstr_Observaciones; }
            set { lstr_Observaciones = value; }
        }
        
        public string Lstr_Usuario
        {
            get { return lstr_Usuario; }
            set { lstr_Usuario = value; }
        }
        
        public DateTime Ldt_FchModifica
        {
            get { return ldt_FchModifica; }
            set { ldt_FchModifica = value; }
        }
        #endregion

        #region Constructor

        public clsInsertarComprobantesPagoPorFormulario( int lint_IdFormulario,
                                            int lint_Anno,                                            
                                            string lstr_NumComprobante,
                                            DateTime ldt_FchComprobante,
                                            string lstr_IdBanco,
                                            string lstr_IdMoneda,
                                            decimal ldec_Monto,
                                            string lstr_Observaciones,
                                            string lstr_Usuario,
                                            DateTime ldt_FchModifica)
        {
            this.lint_IdFormulario = lint_IdFormulario;
            this.lint_Anno = lint_Anno;
            this.lstr_NumComprobante = lstr_NumComprobante;
            this.ldt_FchComprobante = ldt_FchComprobante;
            this.lstr_IdBanco = lstr_IdBanco;
            this.lstr_IdMoneda = lstr_IdMoneda;
            this.ldec_Monto = ldec_Monto;
            this.lstr_Observaciones = lstr_Observaciones;
            this.lstr_Usuario = lstr_Usuario;
            this.ldt_FchModifica = ldt_FchModifica;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CapturaIngresos\\InsertarComprobantesPagoPorFormulario.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }

            //EjecucionSP("C:\\Users\\anajera\\Documents\\Visual Studio 2013\\Projects\\SistemaGestorV3\\Datos\\ConexionSQL\\Configs\\CapturaIngresos\\InsertarComprobantesPago.config", this);
            //EjecucionSP("C:\\Versiones\\SistemaGestor\\ConexionSQL\\Configs\\Mantenimiento\\EstadoGiroEstimado.config", this);
        }

        #endregion

    }
}