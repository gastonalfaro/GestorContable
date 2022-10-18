using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna
{
    public class clsCrearCalculosFlujoEfectivoDE : clsProcedimientoAlmacenado
    {
        #region Parámetros


        private string lstr_IdPrestamo;
        private int? lint_IdTramo;
        private DateTime? ldt_Fecha;
        private decimal ? ldec_CostoAmortInicio;
        private decimal ? ldec_Interes;
        private decimal ? ldec_FNE;
        private decimal? ldec_CostoAmortFinal;
        private decimal? ldec_SaldoDevengo;
        private decimal? ldec_Tir;
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
        public decimal? Ldec_CostoAmortInicio
        {
            get { return ldec_CostoAmortInicio; }
            set { ldec_CostoAmortInicio = value; }
        }
        public decimal? Ldec_Interes
        {
            get { return ldec_Interes; }
            set { ldec_Interes = value; }
        }
        public decimal? Ldec_FNE
        {
            get { return ldec_FNE; }
            set { ldec_FNE = value; }
        }
        public decimal? Ldec_CostoAmortFinal
        {
            get { return ldec_CostoAmortFinal; }
            set { ldec_CostoAmortFinal = value; }
        }
        public decimal? Ldec_SaldoDevengo
        {
            get { return ldec_SaldoDevengo; }
            set { ldec_SaldoDevengo = value; }
        }
        public decimal? Ldec_Tir
        {
            get { return ldec_Tir; }
            set { ldec_Tir = value; }
        }
        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }

        #endregion

        #region Constructor

        public clsCrearCalculosFlujoEfectivoDE( string lstr_IdPrestamo,
        int? lint_IdTramo,
        DateTime? ldt_Fecha,
        decimal ? ldec_CostoAmortInicio,
        decimal ? ldec_Interes,
        decimal ? ldec_FNE,
        decimal? ldec_CostoAmortFinal,
        decimal? ldec_SaldoDevengo,
        decimal? ldec_Tir,
        string lstr_UsrCreacion)
        {
            this.lstr_IdPrestamo = lstr_IdPrestamo;
            this.lint_IdTramo = lint_IdTramo;
            this.ldt_Fecha = ldt_Fecha;
            this.ldec_CostoAmortInicio = ldec_CostoAmortInicio;
            this.ldec_Interes = ldec_Interes;
            this.ldec_FNE = ldec_FNE;
            this.ldec_CostoAmortFinal = ldec_CostoAmortFinal;
            this.ldec_SaldoDevengo = ldec_SaldoDevengo;
            this.ldec_Tir = ldec_Tir;
            this.lstr_UsrCreacion = lstr_UsrCreacion;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaExterna\\CrearCalculosFlujoEfectivoDE.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}