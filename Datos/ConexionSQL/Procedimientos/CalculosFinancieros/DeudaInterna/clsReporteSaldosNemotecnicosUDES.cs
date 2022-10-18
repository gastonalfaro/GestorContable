using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaInterna
{
    public class clsReporteSaldosNemotecnicosUDES : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_Nemotecnico;
        private string lstr_Plazo;
        private string lstr_Propietario;
        private string lstr_CuentaAfectada;
        private DateTime? ldt_FechaDesde;
        private DateTime? ldt_FechaHasta;


        #endregion

        #region Obtención y asignación

        public string Lstr_Nemotecnico
        {
            get { return lstr_Nemotecnico; }
            set { lstr_Nemotecnico = value; }
        }

        public string Lstr_Plazo
        {
            get { return lstr_CuentaAfectada; }
            set { lstr_CuentaAfectada = value; }
        }

        public string Lstr_Propietario
        {
            get { return lstr_Propietario; }
            set { lstr_Propietario = value; }
        }

        public string Lstr_CuentaAfectada
        {
            get { return lstr_Propietario; }
            set { lstr_Propietario = value; }
        }
        public DateTime? Ldt_FechaDesde
        {
            get
            {
                return ldt_FechaDesde;
            }

            set
            {
                ldt_FechaDesde = value;
            }
        }

        public DateTime? Ldt_FechaHasta
        {
            get
            {
                return ldt_FechaHasta;
            }

            set
            {
                ldt_FechaHasta = value;
            }
        }



        #endregion

        #region Constructor
        public clsReporteSaldosNemotecnicosUDES()
        {

        }

        public clsReporteSaldosNemotecnicosUDES(string lstr_Nemotecnico, string lstr_Plazo, string lstr_Propietario, string lstr_CuentaAfectada, DateTime? ldt_FechaDesde, DateTime? ldt_FechaHasta)
        {

            this.lstr_Nemotecnico = lstr_Nemotecnico;
            this.lstr_Plazo = lstr_Plazo;
            this.lstr_Propietario = lstr_Propietario;
            this.lstr_CuentaAfectada = lstr_CuentaAfectada;
            this.ldt_FechaDesde = ldt_FechaDesde;
            this.ldt_FechaHasta = ldt_FechaHasta;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaInterna\\ReporteSaldosNemotecnicosUDES.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}