using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaInterna
{
    public class clsConsultaCostoTransaccion :  clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lint_IdCostoTransaccion;
        private string lint_NumValor;
        private string lstr_Nemotecnico;
        private string ldt_Fecha;
        private string lstr_Estado;

        #endregion

        #region Obtención y asignación

        public string Lint_IdCostoTransaccion
        {
            get { return lint_IdCostoTransaccion; }
            set { lint_IdCostoTransaccion = value; }
        }

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
        public string Ldt_Fecha
        {
            get { return ldt_Fecha; }
            set { ldt_Fecha = value; }
        }
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        #endregion 

        #region Constructor

        public clsConsultaCostoTransaccion(string lint_IdCostoTransaccion, string lint_NumValor, string lstr_Nemotecnico, string ldt_Fecha, string lstr_Estado)
        {
            this.lint_IdCostoTransaccion = lint_IdCostoTransaccion;
            this.lint_NumValor = lint_NumValor;
            this.lstr_Nemotecnico = lstr_Nemotecnico;
            this.ldt_Fecha = ldt_Fecha;
            this.lstr_Estado = lstr_Estado;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaInterna\\ConsultarCostoTransaccion.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}