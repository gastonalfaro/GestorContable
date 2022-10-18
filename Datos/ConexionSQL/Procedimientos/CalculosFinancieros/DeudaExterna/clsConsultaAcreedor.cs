using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna
{
    public class clsConsultaAcreedor : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_TipoAcreedor;
        private string lstr_Pais;
        private string lint_NroAcreedor;
        private string lstr_NomAcreedor;
        private DateTime? ldt_FechaInicio;
        private DateTime? ldt_FechaFin;
        private string lstr_Estado;
        #endregion

        #region Obtención y asignación

        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }
        public string Lstr_TipoAcreedor
        {
            get { return lstr_TipoAcreedor; }
            set { lstr_TipoAcreedor = value; }
        }
        public string Lstr_Pais
        {
            get { return lstr_Pais; }
            set { lstr_Pais = value; }
        }
        public string Lint_NroAcreedor
        {
            get { return lint_NroAcreedor; }
            set { lint_NroAcreedor = value; }
        }
        public string Lstr_NomAcreedor
        {
            get { return lstr_NomAcreedor; }
            set { lstr_NomAcreedor = value; }
        }
        public DateTime? Ldt_FechaInicio
        {
            get { return ldt_FechaInicio; }
            set { ldt_FechaInicio = value; }
        }
        public DateTime? Ldt_FechaFin
        {
            get { return ldt_FechaFin; }
            set { ldt_FechaFin = value; }
        }

        #endregion 

        #region Constructor

        public clsConsultaAcreedor(string lstr_TipoAcreedor, string lstr_Pais, string lint_NroAcreedor, 
            string lstr_NomAcreedor, DateTime? ldt_FechaInicio, DateTime? ldt_FechaFin,string lstr_Estado = "ACT" )
        {
            this.lstr_Estado = lstr_Estado;
            this.lstr_TipoAcreedor = lstr_TipoAcreedor;
            this.lstr_Pais = lstr_Pais;
            this.lint_NroAcreedor = lint_NroAcreedor;
            this.lstr_NomAcreedor = lstr_NomAcreedor;
            this.ldt_FechaInicio = ldt_FechaInicio;
            this.ldt_FechaFin = ldt_FechaFin;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaExterna\\ConsultarAcreedor.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
        #endregion
    }
}