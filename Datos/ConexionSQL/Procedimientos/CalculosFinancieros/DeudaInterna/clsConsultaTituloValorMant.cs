using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaInterna
{
    public class clsConsultaTituloValorMant : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lint_NumValor;
        private string lstr_Nemotecnico;
        private string lstr_Tipo;
        private DateTime? ldt_FchInicio;
        private DateTime? ldt_FchFin;
        private string lstr_ExactaFecha;

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
        public string Lstr_Tipo
        {
            get { return lstr_Tipo; }
            set { lstr_Tipo = value; }
        }
        public DateTime? Ldt_FchInicio
        {
            get { return ldt_FchInicio; }
            set { ldt_FchInicio = value; }
        }
        public DateTime? Ldt_FchFin
        {
            get { return ldt_FchFin; }
            set { ldt_FchFin = value; }
        }
        public string Lstr_ExactaFecha
        {
            get { return lstr_ExactaFecha; }
            set { lstr_ExactaFecha = value; }
        }


        #endregion 

        #region Constructor

        public clsConsultaTituloValorMant(string lint_NumValor, string lstr_Nemotecnico, string lstr_Tipo, DateTime? ldt_FchInicio, DateTime? ldt_FchFin, string str_ExactaFecha = null)
        {
            this.lint_NumValor = lint_NumValor;
            this.lstr_Nemotecnico = lstr_Nemotecnico;
            this.lstr_Tipo = lstr_Tipo;
            this.ldt_FchInicio = ldt_FchInicio;
            this.ldt_FchFin = ldt_FchFin;
            this.lstr_ExactaFecha = str_ExactaFecha;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaInterna\\ConsultarTituloValorMant.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}