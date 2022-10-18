using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaInterna
{
    public class clsModificaTituloValorMant : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private int lint_NumValor;
        private string lstr_Nemotecnico;
        private string lstr_TasaVariable;
        private decimal ldec_TasaVariableValor;
        private decimal ldec_Margen;
        private string lstr_UsrModifica;
        private DateTime ldt_FchModifica;

        #endregion

        #region Obtención y asignación

        public int Lint_NumValor{get { return lint_NumValor; }set { lint_NumValor = value; } }
        public string Lstr_Nemotecnico {get { return lstr_Nemotecnico; }set { lstr_Nemotecnico = value; } }
        public string Lstr_TasaVariable { get { return lstr_TasaVariable; } set { lstr_TasaVariable = value; } }
        public decimal Ldec_TasaVariableValor { get { return ldec_TasaVariableValor; } set { ldec_TasaVariableValor = value; } }
        public decimal Ldec_Margen { get { return ldec_Margen; } set { ldec_Margen = value; } }
        public string Lstr_UsrModifica { get { return lstr_UsrModifica; } set { lstr_UsrModifica = value; } }
        public DateTime Ldt_FchModifica { get { return ldt_FchModifica; } set { ldt_FchModifica = value; } }

        #endregion 

        #region Constructor

        public clsModificaTituloValorMant(int lint_NumValor, string lstr_Nemotecnico, string lstr_TasaVariable, 
            decimal ldec_TasaVariableValor, decimal ldec_Margen, string lstr_UsrModifica, DateTime ldt_FchModifica)
        {
            this.lint_NumValor = lint_NumValor;
            this.lstr_Nemotecnico = lstr_Nemotecnico;
            this.lstr_TasaVariable = lstr_TasaVariable;
            this.ldec_TasaVariableValor = ldec_TasaVariableValor;
            this.ldec_Margen = ldec_Margen;
            this.lstr_UsrModifica = lstr_UsrModifica;
            this.ldt_FchModifica = ldt_FchModifica;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaInterna\\ModificarTituloValorMant.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}