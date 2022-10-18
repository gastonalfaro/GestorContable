using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaInterna
{
    public class clsConsultaCanjeEmision : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_NroEmisionSerie;
        private string lint_NumValor;
        private string lstr_Nemotecnico;
        private DateTime FchCanje;
        private DateTime FchPago;
        private decimal CostoAmortizacionInicial;
        private decimal Intereses;
        private decimal CostoAmortizacionFinal;
        private decimal DescuentoDevengado;
  

        #endregion

        #region Obtención y asignación

        public string Lstr_NroEmisionSerie
        {
            get { return lstr_NroEmisionSerie; }
            set { lstr_NroEmisionSerie = value; }
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

        public DateTime FchCanje1
        {
            get
            {
                return FchCanje;
            }

            set
            {
                FchCanje = value;
            }
        }

        public DateTime FchPago1
        {
            get
            {
                return FchPago;
            }

            set
            {
                FchPago = value;
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

     


        #endregion

        #region Constructor
        public clsConsultaCanjeEmision()
        {

        }

        public clsConsultaCanjeEmision(string lstr_NroEmisionSerie)
        {

            this.lstr_NroEmisionSerie = lstr_NroEmisionSerie;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaInterna\\ConsultarCanjeEmision.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}