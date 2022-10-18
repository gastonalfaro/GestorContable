using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaInterna
{
    public class clsCreaDevengoInteresNroSerie : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_NroEmision;
        private DateTime ldt_Anno;
        private decimal ldec_CostoAmortizacionInicial;
        private decimal ldec_Intereses;
        private decimal ldec_Pago;
        private decimal ldec_CostoAmortizacionFinal;
        private decimal ldec_DescuentoDevengado;
        private string lstr_Estado;
        private string lstr_UsrCreacion;

        #endregion

        #region Obtención y asignación

        public string Lstr_NroEmision{
            get { return lstr_NroEmision; }
            set { lstr_NroEmision = value; }
        }
        public DateTime Ldt_Anno {
            get { return ldt_Anno; }
            set { ldt_Anno = value; }
        }
        public decimal Ldec_CostoAmortizacionInicial {
            get { return ldec_CostoAmortizacionInicial; }
            set { ldec_CostoAmortizacionInicial = value; }
        }
        public decimal Ldec_Intereses{
            get { return ldec_Intereses; }
            set { ldec_Intereses = value; }
        }
        public decimal Ldec_Pago {
            get { return ldec_Pago; }
            set { ldec_Pago = value; }
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
        public string Lstr_Estado {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }
        public string Lstr_UsrCreacion {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }

        #endregion 

        #region Constructor

        public clsCreaDevengoInteresNroSerie(string lstr_NroEmision, DateTime ldt_Anno, decimal ldec_CostoAmortizacionInicial,
            decimal ldec_Intereses, decimal ldec_Pago, decimal ldec_CostoAmortizacionFinal, decimal ldec_DescuentoDevengado,
            string lstr_Estado, string lstr_UsrCreacion)
        {
            this.lstr_NroEmision = lstr_NroEmision;
            this.ldt_Anno = ldt_Anno;
            this.ldec_CostoAmortizacionInicial = ldec_CostoAmortizacionInicial;
            this.ldec_Intereses = ldec_Intereses;
            this.ldec_Pago = ldec_Pago;
            this.ldec_CostoAmortizacionFinal = ldec_CostoAmortizacionFinal;
            this.ldec_DescuentoDevengado = ldec_DescuentoDevengado;
            this.lstr_Estado = lstr_Estado;
            this.lstr_UsrCreacion = lstr_UsrCreacion;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaInterna\\CrearDevengoInteresesNroSerie.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}