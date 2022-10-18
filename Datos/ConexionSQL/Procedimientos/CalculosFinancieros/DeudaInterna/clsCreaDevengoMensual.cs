using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaInterna
{
    public class clsCreaDevengoMensual : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private int lint_NumValor;
        private string lstr_Nemotecnico;
        private string lstr_Periodo;
        private int lint_IdDevengoIntFK;
        private int lint_DiasPeriodo;
        private decimal ldec_InteresTotal;
        private decimal ldec_Cupon;
        private decimal ldec_Descuento;
        private string lstr_UsrCreacion;

        #endregion

        #region Obtención y asignación

        public int Lint_NumValor
        {
            get { return lint_NumValor; }
            set { lint_NumValor = value; }
        }
        public string Lstr_Nemotecnico
        {
            get { return lstr_Nemotecnico; }
            set { lstr_Nemotecnico = value; }
        }
        public string Lstr_Periodo
        {
            get { return lstr_Periodo; }
            set { lstr_Periodo = value; }
        }
        public int Lint_IdDevengoIntFK
        {
            get { return lint_IdDevengoIntFK; }
            set { lint_IdDevengoIntFK = value; }
        }
        public int Lint_DiasPeriodo
        {
            get { return lint_DiasPeriodo; }
            set { lint_DiasPeriodo = value; }
        }
        public decimal Ldec_InteresTotal
        {
            get { return ldec_InteresTotal; }
            set { ldec_InteresTotal = value; }
        }
        public decimal Ldec_Cupon
        {
            get { return ldec_Cupon; }
            set { ldec_Cupon = value; }
        }
        public decimal Ldec_Descuento
        {
            get { return ldec_Descuento; }
            set { ldec_Descuento = value; }
        }
        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }

        #endregion 

        #region Constructor

        public clsCreaDevengoMensual(int lint_NumValor, string lstr_Nemotecnico, string lstr_Periodo, int lint_IdDevengoIntFK, int lint_DiasPeriodo,
            decimal ldec_InteresTotal, decimal ldec_Cupon, decimal ldec_Descuento, string lstr_UsrCreacion)
        {
            this.lint_NumValor = lint_NumValor;
            this.lstr_Nemotecnico = lstr_Nemotecnico;
            this.lstr_Periodo = lstr_Periodo;
            this.lint_IdDevengoIntFK = lint_IdDevengoIntFK;
            this.lint_DiasPeriodo = lint_DiasPeriodo;
            this.ldec_InteresTotal = ldec_InteresTotal;
            this.ldec_Cupon = ldec_Cupon;
            this.ldec_Descuento = ldec_Descuento;
            this.lstr_UsrCreacion = lstr_UsrCreacion;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaInterna\\CrearDevengoMensual.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}