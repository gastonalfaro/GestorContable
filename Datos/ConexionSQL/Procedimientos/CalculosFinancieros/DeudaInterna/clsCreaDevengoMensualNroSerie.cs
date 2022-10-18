using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaInterna
{
    public class clsCreaDevengoMensualNroSerie : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_NroEmision;
        private string lstr_Periodo;
        private int lint_DiasPeriodo;
        private decimal ldec_Columna1;
        private decimal ldec_Columna2;
        private decimal ldec_Columna3;
        private decimal ldec_Columna4;
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
        public int Lint_DiasPeriodo
        {
            get { return lint_DiasPeriodo; }
            set { lint_DiasPeriodo = value; }
        }
        public decimal Ldec_Columna1
        {
            get { return ldec_Columna1; }
            set { ldec_Columna1 = value; }
        }
        public decimal Ldec_Columna2
        {
            get { return ldec_Columna2; }
            set { ldec_Columna2 = value; }
        }
        public decimal Ldec_Columna3
        {
            get { return ldec_Columna3; }
            set { ldec_Columna3 = value; }
        }
        public decimal Ldec_Columna4
        {
            get { return ldec_Columna4; }
            set { ldec_Columna4 = value; }
        }
        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }

        #endregion 

        #region Constructor

        public clsCreaDevengoMensualNroSerie(string lstr_NroEmision, string lstr_Periodo, int lint_DiasPeriodo,
            decimal ldec_Columna1, decimal ldec_Columna2, decimal ldec_Columna3, decimal ldec_Columna4, string lstr_UsrCreacion)
        {
            this.lstr_NroEmision = lstr_NroEmision;
            this.lstr_Periodo = lstr_Periodo;
            this.lint_DiasPeriodo = lint_DiasPeriodo;
            this.ldec_Columna1 = ldec_Columna1;
            this.ldec_Columna2 = ldec_Columna2;
            this.ldec_Columna3 = ldec_Columna3;
            this.ldec_Columna4 = ldec_Columna4;
            this.lstr_UsrCreacion = lstr_UsrCreacion;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaInterna\\CrearDevengoMensualNroSerie.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}