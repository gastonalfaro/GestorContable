using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaInterna
{
    public class clsConsultaTituloReclasificado : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lint_NumValor;
        private string lstr_Nemotecnico;
        private DateTime ldt_FchInicio;
        private DateTime ldt_FchFin;

        #endregion

        #region Obtención y asignación

        public string Lint_NumValor
        {
            get { return lint_NumValor; }
            set { lint_NumValor = value; }
        }
        public string Lstr_Nemotecnico
        {
            get { return lstr_Nemotecnico; }
            set { lstr_Nemotecnico = value; }
        }
        public DateTime Ldt_FchInicio
        {
            get { return ldt_FchInicio; }
            set { ldt_FchInicio = value; }
        }
        public DateTime Ldt_FchFin
        {
            get { return ldt_FchFin; }
            set { ldt_FchFin = value; }
        }


        #endregion 

        #region Constructor

        public clsConsultaTituloReclasificado(string lint_NumValor, string lstr_Nemotecnico, DateTime ldt_FchInicio, DateTime ldt_FchFin)
        {
            this.lint_NumValor = lint_NumValor;
            this.lstr_Nemotecnico = lstr_Nemotecnico;
            this.ldt_FchInicio = ldt_FchInicio;
            this.ldt_FchFin = ldt_FchFin;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaInterna\\ConsultarTituloReclasificado.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}