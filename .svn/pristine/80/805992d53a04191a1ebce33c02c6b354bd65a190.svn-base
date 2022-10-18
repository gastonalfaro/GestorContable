using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaInterna
{
    public class clsCreaCanjeEmisionDeatlle : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_NroEmisionSerie;
        private int lint_NumValor;
        private string lstr_Nemotecnico;
        private DateTime ldt_FchPago;
        private decimal ldec_CostoAmortizacionInicial;
        private decimal ldec_Intereses;
        private decimal ldec_CostoAmortizacionFinal;
        private decimal ldec_DescuentoDevengado;
        private string lstr_UsrCreacion;

        #endregion

        #region Obtención y asignación

        public string Lstr_NroEmisionSerie {
            get { return lstr_NroEmisionSerie; }
            set { lstr_NroEmisionSerie = value; }
        }

         public int Lint_NumValor {
            get { return lint_NumValor; }
            set { lint_NumValor = value; }
        }
        public string Lstr_Nemotecnico{
            get { return lstr_Nemotecnico; }
            set { lstr_Nemotecnico = value; }
        }

        public DateTime Ldt_FchPago {
            get { return ldt_FchPago; }
            set { ldt_FchPago = value; }
        }
        public decimal Ldec_CostoAmortizacionInicial {
            get { return ldec_CostoAmortizacionInicial; }
            set { ldec_CostoAmortizacionInicial = value; }
        }
        public decimal Ldec_Intereses{
            get { return ldec_Intereses; }
            set { ldec_Intereses = value; }
        }
        public decimal Ldec_CostoAmortizacionFinal
        {
            get { return ldec_CostoAmortizacionFinal; }
            set { ldec_CostoAmortizacionFinal = value; }
        }
        public decimal Ldec_DescuentoDevengado
        {
            get { return ldec_DescuentoDevengado; }
            set { ldec_DescuentoDevengado = value; }
        }
        public string Lstr_UsrCreacion {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }

        #endregion 

        #region Constructor

        public clsCreaCanjeEmisionDeatlle(string lstr_NroEmisionSerie,int lint_NumValor,string lstr_Nemotecnico, DateTime ldt_FchPago, decimal ldec_CostoAmortizacionInicial,
            decimal ldec_Intereses, decimal ldec_CostoAmortizacionFinal, decimal ldec_DescuentoDevengado, string lstr_UsrCreacion)
        {
            this.lstr_NroEmisionSerie = lstr_NroEmisionSerie;
            this.lint_NumValor = lint_NumValor;
            this.lstr_Nemotecnico = lstr_Nemotecnico;
            this.ldt_FchPago = ldt_FchPago;
            this.ldec_CostoAmortizacionInicial = ldec_CostoAmortizacionInicial;
            this.ldec_Intereses = ldec_Intereses;
            this.ldec_CostoAmortizacionFinal = ldec_CostoAmortizacionFinal;
            this.ldec_DescuentoDevengado = ldec_DescuentoDevengado;
            this.lstr_UsrCreacion = lstr_UsrCreacion;

            try
            {
              
                
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaInterna\\CrearCanjeEmisionDetalle.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}