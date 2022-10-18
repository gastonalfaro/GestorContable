using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaInterna
{
    public class clsConsultaTituloValor : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private int? lint_NumValor;
        private string lstr_Nemotecnico;
        private string lint_NumCupon;
        private string lstr_Garantia;
        private string lstr_IndicadorCupon;
        private string lstr_Tipo;
        private string lstr_TipoNegociacion;
        private string lstr_EstadoValor;
        private DateTime ldt_FchInicio;
        private DateTime ldt_FchFin;
        private string lstr_NroEmisionSerie;

        #endregion

        #region Obtención y asignación

        public int? Lint_NumValor
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
        public string Lstr_TipoNegociacion
        {
            get { return lstr_TipoNegociacion; }
            set { lstr_TipoNegociacion = value; }
        }
        public string Lstr_IndicadorCupon
        {
            get { return lstr_IndicadorCupon; }
            set { lstr_IndicadorCupon = value; }
        }
        public string Lstr_EstadoValor
        {
            get { return lstr_EstadoValor; }
            set { lstr_EstadoValor = value; }
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
        public string Lstr_NroEmisionSerie
        {
            get { return lstr_NroEmisionSerie; }
            set { lstr_NroEmisionSerie = value; }
        }

        #endregion 

        #region Constructor

        public clsConsultaTituloValor(int? lint_NumValor, string lstr_Nemotecnico, string lint_NumCupon, string lstr_Garantia, string lstr_IndicadorCupon, string lstr_Tipo, string lstr_TipoNegociacion, string lstr_EstadoValor, DateTime ldt_FchInicio, DateTime ldt_FchFin, string lstr_NroEmisionSerie)
        {
            this.lint_NumValor = lint_NumValor;
            this.lstr_Nemotecnico = lstr_Nemotecnico;
            this.lint_NumCupon = lint_NumCupon;
            this.lstr_Garantia = lstr_Garantia;
            this.lstr_IndicadorCupon = lstr_IndicadorCupon;
            this.lstr_Tipo = lstr_Tipo;
            this.lstr_TipoNegociacion = lstr_TipoNegociacion;
            this.lstr_EstadoValor = lstr_EstadoValor;
            this.ldt_FchInicio = ldt_FchInicio;
            this.ldt_FchFin = ldt_FchFin;
            this.lstr_NroEmisionSerie = lstr_NroEmisionSerie;


            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaInterna\\ConsultarTituloValor.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}