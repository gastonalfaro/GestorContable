using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaInterna
{
    public class clsCreaCanjeEmision : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_NroEmisionSerie;
        private DateTime ldt_FchCanje;
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
        public DateTime Ldt_FchCanje
        {
            get { return ldt_FchCanje; }
            set { ldt_FchCanje = value; }
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

        public clsCreaCanjeEmision(string lstr_NroEmisionSerie, DateTime ldt_FchCanje, decimal ldec_CostoAmortizacionInicial,
            decimal ldec_Intereses, decimal ldec_CostoAmortizacionFinal, decimal ldec_DescuentoDevengado, string lstr_UsrCreacion)
        {
            this.lstr_NroEmisionSerie = lstr_NroEmisionSerie;
            this.ldt_FchCanje = ldt_FchCanje;
            this.ldec_CostoAmortizacionInicial = ldec_CostoAmortizacionInicial;
            this.ldec_Intereses = ldec_Intereses;
            this.ldec_CostoAmortizacionFinal = ldec_CostoAmortizacionFinal;
            this.ldec_DescuentoDevengado = ldec_DescuentoDevengado;
            this.lstr_UsrCreacion = lstr_UsrCreacion;

            try
            {
              
                
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaInterna\\CrearCanjeEmision.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}