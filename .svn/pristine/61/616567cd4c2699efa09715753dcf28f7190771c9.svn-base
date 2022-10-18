using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaInterna
{
    public class clsConsultaCalculoFlujoEfectivo : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lint_NumValor;
        private string lstr_Nemotecnico;
        private string periodo;
        private int idFlujoEfectivo;
        private decimal tasaInteres;
        private decimal tir;
        private decimal interes;
        private decimal flujoEfectivo;
        private string nroAsiento;
         

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

        public string Periodo
        {
            get
            {
                return periodo;
            }

            set
            {
                periodo = value;
            }
        }

        public int IdFlujoEfectivo
        {
            get
            {
                return idFlujoEfectivo;
            }

            set
            {
                idFlujoEfectivo = value;
            }
        }

        public decimal TasaInteres
        {
            get
            {
                return tasaInteres;
            }

            set
            {
                tasaInteres = value;
            }
        }

        public decimal Tir
        {
            get
            {
                return tir;
            }

            set
            {
                tir = value;
            }
        }

        public decimal Interes
        {
            get
            {
                return interes;
            }

            set
            {
                interes = value;
            }
        }

        public decimal FlujoEfectivo
        {
            get
            {
                return flujoEfectivo;
            }

            set
            {
                flujoEfectivo = value;
            }
        }

        public string NroAsiento
        {
            get
            {
                return nroAsiento;
            }

            set
            {
                nroAsiento = value;
            }
        }

        #endregion

        #region Constructor

        public clsConsultaCalculoFlujoEfectivo()
        {

        }
        public clsConsultaCalculoFlujoEfectivo(string lint_NumValor, string lstr_Nemotecnico)
        {
            this.lint_NumValor = lint_NumValor;
            this.lstr_Nemotecnico = lstr_Nemotecnico;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaInterna\\ConsultarCalculoFlujoEfectivo.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}