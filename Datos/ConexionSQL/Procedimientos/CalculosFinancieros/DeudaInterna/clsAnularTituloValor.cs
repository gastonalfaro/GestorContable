using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaInterna
{
    public class clsAnularTituloValor : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private int lint_NumValor;
        private string lstr_Nemotecnico;
        private string lstr_EstadoValor;
        private string lstr_UsrModifica;
        private DateTime ldt_FchModifica; 

        #endregion

        #region Obtención y asignación

        public int Lint_NumValor { get { return lint_NumValor; } set { lint_NumValor = value; } }
        public string Lstr_Nemotecnico { get { return lstr_Nemotecnico; } set { lstr_Nemotecnico = value; } }
        public string Lstr_EstadoValor { get { return lstr_EstadoValor; } set { lstr_EstadoValor = value; } }
        public string Lstr_UsrModifica { get { return lstr_UsrModifica; } set { lstr_UsrModifica = value; } }
        public DateTime Ldt_FchModifica { get { return ldt_FchModifica; } set { ldt_FchModifica = value; } }

        #endregion

        #region Constructor

        public clsAnularTituloValor(int lint_NumValor, string lstr_Nemotecnico, string lstr_EstadoValor,
            string lstr_UsrModifica, DateTime ldt_FchModifica)
        {
            this.lint_NumValor = lint_NumValor;
            this.lstr_Nemotecnico = lstr_Nemotecnico;
            this.lstr_EstadoValor = lstr_EstadoValor;
            this.lstr_UsrModifica = lstr_UsrModifica;
            this.ldt_FchModifica = ldt_FchModifica;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaInterna\\AnularTituloValor.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}