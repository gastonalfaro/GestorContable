using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna
{
    public class clsCrearCalculosDevengoDE : clsProcedimientoAlmacenado
    {
        #region Parámetros


        private string lstr_IdPrestamo;
        private int? lint_IdTramo;
        private DateTime? ldt_Fecha;
        private decimal? ldec_MontoDesembolso;
        private decimal? ldec_MontoAmortizacion;
        private decimal? ldec_MontoIntereses;
        private decimal? ldec_MontoComision;
        private decimal? ldec_MontoFlujo;
        private string lstr_TipoPrestamo;
        private string lstr_IdMoneda;
        private string lstr_NbrAcreedor;
        private string lstr_AbrevAcreedor;
        private string lstr_UsrCreacion;

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
        public DateTime? Ldt_Fecha
        {
            get { return ldt_Fecha; }
            set { ldt_Fecha = value; }
        }
        public decimal? Ldec_MontoDesembolso
        {
            get { return ldec_MontoDesembolso; }
            set { ldec_MontoDesembolso = value; }
        }
        public decimal? Ldec_MontoAmortizacion
        {
            get { return ldec_MontoAmortizacion; }
            set { ldec_MontoAmortizacion = value; }
        }
        public decimal? Ldec_MontoIntereses
        {
            get { return ldec_MontoIntereses; }
            set { ldec_MontoIntereses = value; }
        }
        public decimal? Ldec_MontoComision
        {
            get { return ldec_MontoComision; }
            set { ldec_MontoComision = value; }
        }
        public decimal? Ldec_MontoFlujo
        {
            get { return ldec_MontoFlujo; }
            set { ldec_MontoFlujo = value; }
        }
        public string Lstr_TipoPrestamo
        {
            get { return lstr_TipoPrestamo; }
            set { lstr_TipoPrestamo = value; }
        }
        public string Lstr_IdMoneda
        {
            get { return lstr_IdMoneda; }
            set { lstr_IdMoneda = value; }
        }
        public string Lstr_NbrAcreedor
        {
            get { return lstr_NbrAcreedor; }
            set { lstr_NbrAcreedor = value; }
        }
        public string Lstr_AbrevAcreedor
        {
            get { return lstr_AbrevAcreedor; }
            set { lstr_AbrevAcreedor = value; }
        }
        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }

        #endregion

        #region Constructor

        public clsCrearCalculosDevengoDE(string lstr_IdPrestamo,
        int? lint_IdTramo,
        DateTime? ldt_Fecha,
        decimal? ldec_MontoDesembolso,
        decimal? ldec_MontoAmortizacion,
        decimal? ldec_MontoIntereses,
        decimal? ldec_MontoComision,
        decimal? ldec_MontoFlujo,
        string lstr_TipoPrestamo,
        string lstr_IdMoneda,
        string lstr_NbrAcreedor,
        string lstr_AbrevAcreedor,
        string lstr_UsrCreacion)
        {
            this.lstr_IdPrestamo = lstr_IdPrestamo;
            this.lint_IdTramo = lint_IdTramo;
            this.ldt_Fecha = ldt_Fecha;
            this.ldec_MontoDesembolso = ldec_MontoDesembolso;
            this.ldec_MontoAmortizacion = ldec_MontoAmortizacion;
            this.ldec_MontoIntereses = ldec_MontoIntereses;
            this.ldec_MontoComision = ldec_MontoComision;
            this.ldec_MontoFlujo = ldec_MontoFlujo;
            this.lstr_TipoPrestamo = lstr_TipoPrestamo;
            this.lstr_IdMoneda = lstr_IdMoneda;
            this.lstr_NbrAcreedor = lstr_NbrAcreedor;
            this.lstr_AbrevAcreedor = lstr_AbrevAcreedor;
            this.lstr_UsrCreacion = lstr_UsrCreacion;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaExterna\\CrearCalculosDevengoDE.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}