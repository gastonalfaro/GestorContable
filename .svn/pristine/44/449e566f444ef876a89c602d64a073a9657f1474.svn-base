using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CapturaIngresos
{
    public class clsConsultarComprobantesPagoPorFormulario : clsProcedimientoAlmacenado
    {


        #region Parámetros

        private int lint_IdFormulario;
        private int lint_AnioFormulario;
        private string lstr_NumComprobante;
        private DateTime ldt_FchComprobante;
        private string lstr_IdBanco;
        private string lstr_IdMoneda;


        #endregion

        #region Obtención y asignación

        public int Lint_IdFormulario
        {
            get { return lint_IdFormulario; }
            set { lint_IdFormulario = value; }
        }

        public int Lint_AnioFormulario
        {
            get { return lint_AnioFormulario; }
            set { lint_AnioFormulario = value; }
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

        #endregion

        #region Constructor

        public clsConsultarComprobantesPagoPorFormulario(int lint_IdFormulario,
                                            int lint_AnioFormulario,
                                            string lstr_NumComprobante,
                                            DateTime ldt_FchComprobante,
                                            string lstr_IdBanco,
                                            string lstr_IdMoneda)
        {
            this.lint_IdFormulario = lint_IdFormulario;
            this.lint_AnioFormulario = lint_AnioFormulario;
            this.lstr_NumComprobante = lstr_NumComprobante;
            this.ldt_FchComprobante = ldt_FchComprobante;
            this.lstr_IdBanco = lstr_IdBanco;
            this.lstr_IdMoneda = lstr_IdMoneda;           

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CapturaIngresos\\ConsultarComprobantesPagoPorFormulario.config", this);
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