using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna
{
    public class clsConsultaDesembolso : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_IdPrestamo;
        private int? lint_IdTramo;
        private int? lint_Secuencia;
        private decimal? ldec_MontoDesde;
        private decimal? ldec_MontoHasta;
        private string lstr_Moneda;
        private DateTime? ldt_FchDesde;
        private DateTime? ldt_FchHasta;
        private DateTime? ldt_FchEstimadaDesde;
        private DateTime? ldt_FchEstimadaHasta;
        private string lstr_TipoDesembolso;
        private string lstr_Descripcion;

        #endregion

        #region Obtención y asignación

        public string Lstr_IdPrestamo
        {
            get { return lstr_IdPrestamo; }
            set { lstr_IdPrestamo = value; }
        }
        public int? Lint_IdTramo
        {
            get { return lint_IdTramo; }
            set { lint_IdTramo = value; }
        }
        public int? Lint_Secuencia
        {
            get { return lint_Secuencia; }
            set { lint_Secuencia = value; }
        }
        public decimal? Ldec_MontoDesde
        {
            get { return ldec_MontoDesde; }
            set { ldec_MontoDesde = value; }
        }
        public decimal? Ldec_MontoHasta
        {
            get { return ldec_MontoHasta; }
            set { ldec_MontoHasta = value; }
        }
        public string Lstr_Moneda
        {
            get { return lstr_Moneda; }
            set { lstr_Moneda = value; }
        }
        public DateTime? Ldt_FchDesde
        {
            get { return ldt_FchDesde; }
            set { ldt_FchDesde = value; }
        }
        public DateTime? Ldt_FchHasta
        {
            get { return ldt_FchHasta; }
            set { ldt_FchHasta = value; }
        }
        public DateTime? Ldt_FchEstimadaDesde
        {
            get { return ldt_FchEstimadaDesde; }
            set { ldt_FchEstimadaDesde = value; }
        }
        public DateTime? Ldt_FchEstimadaHasta
        {
            get { return ldt_FchEstimadaHasta; }
            set { ldt_FchEstimadaHasta = value; }
        }
        public string Lstr_TipoDesembolso
        {
            get { return lstr_TipoDesembolso; }
            set { lstr_TipoDesembolso = value; }
        }
        public string Lstr_Descripcion
        {
            get { return lstr_Descripcion; }
            set { lstr_Descripcion = value; }
        }

        #endregion 

        #region Constructor

        public clsConsultaDesembolso(string lstr_IdPrestamo, int? lint_IdTramo, decimal? ldec_MontoDesde, decimal? ldec_MontoHasta, string lstr_Moneda,
            DateTime? ldt_FchDesde, DateTime? ldt_FchHasta, DateTime? ldt_FchEstimadaDesde, DateTime? ldt_FchEstimadaHasta, string lstr_TipoDesembolso, string lstr_Descripcion, int? lint_Secuencia)
        {
            this.lstr_IdPrestamo = lstr_IdPrestamo;
            this.lint_IdTramo = lint_IdTramo;
            this.ldec_MontoDesde = ldec_MontoDesde;
            this.ldec_MontoHasta = ldec_MontoHasta;
            this.lstr_Moneda = lstr_Moneda;
            this.ldt_FchDesde = ldt_FchDesde;
            this.ldt_FchHasta = ldt_FchHasta;
            this.ldt_FchEstimadaDesde = ldt_FchEstimadaDesde;
            this.ldt_FchEstimadaHasta = ldt_FchEstimadaHasta;
            this.lstr_TipoDesembolso = lstr_TipoDesembolso;
            this.lstr_Descripcion = lstr_Descripcion;
            this.lint_Secuencia = lint_Secuencia;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaExterna\\ConsultarDesembolso.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}