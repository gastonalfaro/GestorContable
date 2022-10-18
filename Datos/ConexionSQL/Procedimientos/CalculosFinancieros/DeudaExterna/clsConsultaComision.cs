using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna
{
    public class clsConsultaComision : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_IdPrestamo;
        private string lint_IdTramo;
        private string lstr_TipoPago;
        private string lstr_TipoComision;
        private string lstr_MonedaPago;
        private string lstr_IdComision;
        private decimal? lstr_Porcentaje;
        private string lstr_Periodo;
        private DateTime? lstr_FechaDesde;
        private DateTime? lstr_FechaHasta;
        private string lstr_Anno;
        private string lstr_Mes;

        #endregion

        #region Obtención y asignación

        public string Lstr_IdPrestamo
        {
            get { return lstr_IdPrestamo; }
            set { lstr_IdPrestamo = value; }
        }
        public string Lint_IdTramo
        {
            get { return lint_IdTramo; }
            set { lint_IdTramo = value; }
        }
        public string Lstr_TipoPago
        {
            get { return lstr_TipoPago; }
            set { lstr_TipoPago = value; }
        }
        public string Lstr_TipoComision
        {
            get { return lstr_TipoComision; }
            set { lstr_TipoComision = value; }
        }
        public string Lstr_MonedaPago
        {
            get { return lstr_MonedaPago; }
            set { lstr_MonedaPago = value; }
        }
        public string Lstr_IdComision
        {
            get { return lstr_IdComision; }
            set { lstr_IdComision = value; }
        }
        public decimal? Lstr_Porcentaje
        {
            get { return lstr_Porcentaje; }
            set { lstr_Porcentaje = value; }
        }
        public string Lstr_Periodo
        {
            get { return lstr_Periodo; }
            set { lstr_Periodo = value; }
        }
        public DateTime? Lstr_FechaDesde
        {
            get { return lstr_FechaDesde; }
            set { lstr_FechaDesde = value; }
        }
        public DateTime? Lstr_FechaHasta
        {
            get { return lstr_FechaHasta; }
            set { lstr_FechaHasta = value; }
        }
        public string Lstr_Anno
        {
            get { return lstr_Anno; }
            set { lstr_Anno = value; }
        }
        public string Lstr_Mes
        {
            get { return lstr_Mes; }
            set { lstr_Mes = value; }
        }

        #endregion 

        #region Constructor

        public clsConsultaComision(string lstr_IdPrestamo, string lint_IdTramo, string lstr_TipoPago, string lstr_TipoComision, string lstr_MonedaPago, string lstr_IdComision,
            decimal? lstr_Porcentaje, string lstr_Periodo, DateTime? lstr_FechaDesde, DateTime? lstr_FechaHasta, string lstr_Anno, string lstr_Mes)
        {
            this.lstr_IdPrestamo = lstr_IdPrestamo;
            this.lint_IdTramo = lint_IdTramo;
            this.lstr_TipoPago = lstr_TipoPago;
            this.lstr_TipoComision = lstr_TipoComision;
            this.lstr_MonedaPago = lstr_MonedaPago;
            this.lstr_IdComision = lstr_IdComision;
            this.lstr_Porcentaje = lstr_Porcentaje;
            this.lstr_Periodo = lstr_Periodo;
            this.lstr_FechaDesde = lstr_FechaDesde;
            this.lstr_FechaHasta = lstr_FechaHasta;
            this.lstr_Anno = lstr_Anno;
            this.lstr_Mes = lstr_Mes;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaExterna\\ConsultarComision.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}