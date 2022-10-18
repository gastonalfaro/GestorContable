using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaInterna
{
    public class clsCreaCalculoFlujoEfectivoNroSerie : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_NroEmision;
        private string lstr_Periodo;
        private decimal ldec_TasaInteres;
        private decimal ldec_Intereses;
        private decimal ldec_FlujoEfectivo;
        private string lstr_UsrCreacion;

        #endregion

        #region Obtención y asignación

        public string Lstr_NroEmision
        {
            get { return lstr_NroEmision; }
            set { lstr_NroEmision = value; }
        }
        public string Lstr_Periodo
        {
            get { return lstr_Periodo; }
            set { lstr_Periodo = value; }
        }
        public decimal Ldec_TasaInteres
        {
            get { return ldec_TasaInteres; }
            set { ldec_TasaInteres = value; }
        }
        public decimal Ldec_Intereses{
            get { return ldec_Intereses; }
            set { ldec_Intereses = value; }
        }
        public decimal Ldec_FlujoEfectivo
        {
            get { return ldec_FlujoEfectivo; }
            set { ldec_FlujoEfectivo = value; }
        }
        public string Lstr_UsrCreacion {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }

        #endregion 

        #region Constructor

        public clsCreaCalculoFlujoEfectivoNroSerie(string lstr_NroEmision, string lstr_Periodo, decimal ldec_TasaInteres, 
            decimal ldec_Intereses, decimal ldec_FlujoEfectivo, string lstr_UsrCreacion)
        {
            this.lstr_NroEmision = lstr_NroEmision;
            this.lstr_Periodo = lstr_Periodo;
            this.ldec_TasaInteres = ldec_TasaInteres;
            this.ldec_Intereses = ldec_Intereses;
            this.ldec_FlujoEfectivo = ldec_FlujoEfectivo;
            this.lstr_UsrCreacion = lstr_UsrCreacion;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaInterna\\CrearCalculoFlujoEfectivoNroSerie.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}