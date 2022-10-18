using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaInterna
{
    public class clsConsultaDevengoInteres : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lint_NumValor;
        private string lstr_Nemotecnico;
        private DateTime Anno;
        private int IdDevengoInteres;
        private int IdFlujoEfectivoFK;
        private decimal CostoAmortizacionInicial;
        private decimal Intereses;
        private decimal Pago;
        private decimal CostoAmortizacionFinal;
        private decimal DescuentoDevengado;
        private decimal Tir;

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

        public DateTime Anno1
        {
            get
            {
                return Anno;
            }

            set
            {
                Anno = value;
            }
        }

        public int IdDevengoInteres1
        {
            get
            {
                return IdDevengoInteres;
            }

            set
            {
                IdDevengoInteres = value;
            }
        }

        public int IdFlujoEfectivoFK1
        {
            get
            {
                return IdFlujoEfectivoFK;
            }

            set
            {
                IdFlujoEfectivoFK = value;
            }
        }

        public decimal CostoAmortizacionInicial1
        {
            get
            {
                return CostoAmortizacionInicial;
            }

            set
            {
                CostoAmortizacionInicial = value;
            }
        }

        public decimal Intereses1
        {
            get
            {
                return Intereses;
            }

            set
            {
                Intereses = value;
            }
        }

        public decimal Pago1
        {
            get
            {
                return Pago;
            }

            set
            {
                Pago = value;
            }
        }

        public decimal CostoAmortizacionFinal1
        {
            get
            {
                return CostoAmortizacionFinal;
            }

            set
            {
                CostoAmortizacionFinal = value;
            }
        }

        public decimal DescuentoDevengado1
        {
            get
            {
                return DescuentoDevengado;
            }

            set
            {
                DescuentoDevengado = value;
            }
        }

        public decimal Tir1
        {
            get
            {
                return Tir;
            }

            set
            {
                Tir = value;
            }
        }



        #endregion

        #region Constructor
        public clsConsultaDevengoInteres()
        {

        }

        public clsConsultaDevengoInteres(string lint_NumValor, string lstr_Nemotecnico)
        {
            this.lint_NumValor = lint_NumValor;
            this.lstr_Nemotecnico = lstr_Nemotecnico;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaInterna\\ConsultarDevengoIntereses.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}