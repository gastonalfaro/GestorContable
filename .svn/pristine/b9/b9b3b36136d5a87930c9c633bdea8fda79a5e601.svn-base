using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaInterna
{
    public class clsConsultaAsientoDeudaInterna :  clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lint_NumValor;
        private string lint_NumCupon;
        private string lstr_Nemotecnico;

        #endregion

        #region Obtención y asignación

        public string Lint_NumValor
        {
            get { return lint_NumValor; }
            set { lint_NumValor = value; }
        }
        public string Lint_NumCupon
        {
            get { return lint_NumCupon; }
            set { lint_NumCupon = value; }
        }
        public string Lstr_Nemotecnico
        {
            get { return lstr_Nemotecnico; }
            set { lstr_Nemotecnico = value; }
        }

        #endregion 

        #region Constructor

        public clsConsultaAsientoDeudaInterna(string lint_NumValor, string lint_NumCupon, string lstr_Nemotecnico)
        {
            this.lint_NumValor = lint_NumValor;
            this.lint_NumCupon = lint_NumCupon;
            this.lstr_Nemotecnico = lstr_Nemotecnico;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaInterna\\ConsultarAsientoDeudaInterna.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}