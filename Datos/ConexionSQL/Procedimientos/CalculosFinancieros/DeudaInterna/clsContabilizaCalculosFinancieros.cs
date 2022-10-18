using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaInterna
{
    public class clsContabilizaCalculosFinancieros : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_NbrTabla;        
        private string lint_IdCostoTransaccion;
        private string lstr_NroValor;
        private string lstr_Nemotecnico;
        private string lstr_Estado;
        private string lstr_UsrModifica;
        private DateTime ldt_FchModifica;

        #endregion

        #region Obtención y asignación

        public string Lstr_NbrTabla
        {
            get { return lstr_NbrTabla; }
            set { lstr_NbrTabla = value; }
        }
        public string Lstr_IdCostoTransaccion
        {
            get { return lint_IdCostoTransaccion; }
            set { lint_IdCostoTransaccion = value; }
        }
        public string Lstr_NroValor
        {
            get { return lstr_NroValor; }
            set { lstr_NroValor = value; }
        }
        public string Lstr_Nemotecnico
        {
            get { return lstr_Nemotecnico; }
            set { lstr_Nemotecnico = value; }
        }
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }
        public string Lstr_UsrModifica
        {
            get { return lstr_UsrModifica; }
            set { lstr_UsrModifica = value; }
        }
        public DateTime Ldt_FchModifica
        {
            get { return ldt_FchModifica; }
            set { ldt_FchModifica = value; }
        }

        #endregion

        #region Constructor

        public clsContabilizaCalculosFinancieros(string lstr_NbrTabla, string lint_IdCostoTransaccion, string lstr_NroValor, string lstr_Nemotecnico, string lstr_Estado, string lstr_UsrModifica, DateTime ldt_FchModifica)
        {
            this.lstr_NbrTabla = lstr_NbrTabla;
            this.lint_IdCostoTransaccion = lint_IdCostoTransaccion;
            this.lstr_NroValor = lstr_NroValor;
            this.lstr_Nemotecnico = lstr_Nemotecnico;
            this.lstr_Estado = lstr_Estado;
            this.lstr_UsrModifica = lstr_UsrModifica;
            this.ldt_FchModifica = ldt_FchModifica;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaInterna\\ContabilizarCalculosFinancieros.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}