using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaInterna
{
    public class clsEliminaTituloGarantia : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private int lint_NumValor;
        private string lstr_Nemotecnico;
        private string lstr_Usuario;

        #endregion

        #region Obtención y asignación

        public int Lint_NumValor{get { return lint_NumValor; }set { lint_NumValor = value; } }
        public string Lstr_Nemotecnico {get { return lstr_Nemotecnico; }set { lstr_Nemotecnico = value; } }
        public string Lstr_Usuario { get { return lstr_Usuario; } set { lstr_Usuario = value; } }

        #endregion 

        #region Constructor

        public clsEliminaTituloGarantia(int lint_NumValor, string lstr_Nemotecnico, string lstr_Usuario)
        {
            this.lint_NumValor = lint_NumValor;
            this.lstr_Nemotecnico = lstr_Nemotecnico;
            this.lstr_Usuario = lstr_Usuario;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaInterna\\EliminarTituloGarantia.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}