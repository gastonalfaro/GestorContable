using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaInterna
{
    public class clsConsultaTituloValorValores : clsProcedimientoAlmacenado
    {
        #region Parámetros

        //private string lint_NumValor;
        //private string lint_NumCupon;
        //private string lstr_TipoFecha;
        //private string ldt_FchInicio;
        //private string ldt_FchFin;
        private string lint_NumValor;
        private string lstr_Nemotecnico;
        private string lint_NumCupon;
        private string lstr_Garantia;
        private string lstr_Tipo;
        private string ldt_FchInicio;
        private string ldt_FchFin;

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
        public string Lint_NumCupon
        {
            get { return lint_NumCupon; }
            set { lint_NumCupon = value; }
        }
        public string Lstr_Garantia
        {
            get { return lstr_Garantia; }
            set { lstr_Garantia = value; }
        }
        public string Lstr_Tipo
        {
            get { return lstr_Tipo; }
            set { lstr_Tipo = value; }
        }
        //public string Lstr_TipoFecha
        //{
        //    get { return lstr_TipoFecha; }
        //    set { lstr_TipoFecha = value; }
        //}
        public string Ldt_FchInicio
        {
            get { return ldt_FchInicio; }
            set { ldt_FchInicio = value; }
        }
        public string Ldt_FchFin
        {
            get { return ldt_FchFin; }
            set { ldt_FchFin = value; }
        }


        #endregion 

        #region Constructor

        public clsConsultaTituloValorValores(string lint_NumValor, string lstr_Nemotecnico, string lint_NumCupon, string lstr_Garantia, string lstr_Tipo, string ldt_FchInicio, string ldt_FchFin)
        {
            this.lint_NumValor = lint_NumValor;
            this.lstr_Nemotecnico = lstr_Nemotecnico;
            this.lint_NumCupon = lint_NumCupon;
            this.lstr_Garantia = lstr_Garantia;
            this.lstr_Tipo = lstr_Tipo;
            this.ldt_FchInicio = ldt_FchInicio;
            this.ldt_FchFin = ldt_FchFin;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaInterna\\ConsultarTituloValorValores.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}