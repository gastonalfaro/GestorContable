using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna
{
    public class clsConsultaPrestamo : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_IdPrestamo;
        private DateTime? ldt_FechaInicio;
        private DateTime? ldt_FechaFin;
        private string lstr_Fuente;
        private string lstr_Situacion;
        private string lstr_Plazo;
        private string lstr_Nombre;
        private string lstr_NbrAcreedor;
        private string lstr_CatAcreedor;
        private string lstr_TpoAcreedor;
        private string lstr_NbrDeudor;
        private string lstr_CatDeudor;
        private string lstr_TipoPrestamo;

        #endregion

        #region Obtención y asignación

        public string Lstr_IdPrestamo
        {
            get { return lstr_IdPrestamo; }
            set { lstr_IdPrestamo = value; }
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
        public string Lstr_Fuente
        {
            get { return lstr_Fuente; }
            set { lstr_Fuente = value; }
        }
        public string Lstr_Situacion
        {
            get { return lstr_Situacion; }
            set { lstr_Situacion = value; }
        }
        public string Lstr_Plazo
        {
            get { return lstr_Plazo; }
            set { lstr_Plazo = value; }
        }
        public string Lstr_Nombre
        {
            get { return lstr_Nombre; }
            set { lstr_Nombre = value; }
        }
        public string Lstr_NbrAcreedor
        {
            get { return lstr_NbrAcreedor; }
            set { lstr_NbrAcreedor = value; }
        }
        public string Lstr_CatAcreedor
        {
            get { return lstr_CatAcreedor; }
            set { lstr_CatAcreedor = value; }
        }
        public string Lstr_TpoAcreedor
        {
            get { return lstr_TpoAcreedor; }
            set { lstr_TpoAcreedor = value; }
        }
        public string Lstr_NbrDeudor
        {
            get { return lstr_NbrDeudor; }
            set { lstr_NbrDeudor = value; }
        }
        public string Lstr_CatDeudor
        {
            get { return lstr_CatDeudor; }
            set { lstr_CatDeudor = value; }
        }
        public string Lstr_TipoPrestamo
        {
            get { return lstr_TipoPrestamo; }
            set { lstr_TipoPrestamo = value; }
        }


        #endregion 

        #region Constructor

        public clsConsultaPrestamo(string lstr_IdPrestamo, DateTime? ldt_FechaInicio, DateTime? ldt_FechaFin, string lstr_Fuente,
            string lstr_Situacion, string lstr_Plazo, string lstr_Nombre, string lstr_NbrAcreedor, string lstr_CatAcreedor,
            string lstr_TpoAcreedor, string lstr_NbrDeudor, string lstr_CatDeudor, string lstr_TipoPrestamo)
        {
            this.lstr_IdPrestamo = lstr_IdPrestamo;
            this.ldt_FechaInicio = ldt_FechaInicio;
            this.ldt_FechaFin = ldt_FechaFin;
            this.lstr_Fuente = lstr_Fuente;
            this.lstr_Situacion = lstr_Situacion;
            this.lstr_Plazo = lstr_Plazo;
            this.lstr_Nombre = lstr_Nombre;
            this.lstr_NbrAcreedor = lstr_NbrAcreedor;
            this.lstr_CatAcreedor = lstr_CatAcreedor;
            this.lstr_TpoAcreedor = lstr_TpoAcreedor;
            this.lstr_NbrDeudor = lstr_NbrDeudor;
            this.lstr_CatDeudor = lstr_CatDeudor;
            this.lstr_TipoPrestamo = lstr_TipoPrestamo;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaExterna\\ConsultarPrestamo.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}