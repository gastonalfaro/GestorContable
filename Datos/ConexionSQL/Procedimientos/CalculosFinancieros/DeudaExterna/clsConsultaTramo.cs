using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna
{
    public class clsConsultaTramo : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_IdPrestamo;
        private int? lint_IdTramo;
        private string lstr_IdMoneda;
        private decimal? ldec_Monto;
        private string lstr_Reorganizacion;
        private string lstr_TermReorganizacion;
        private string lstr_TerminoCredito;
        private string lstr_TipoAcuerdo;
        private string lstr_TipoFinanciamiento;

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
        public string Lstr_IdMoneda
        {
            get { return lstr_IdMoneda; }
            set { lstr_IdMoneda = value; }
        }
        public decimal? Ldec_Monto
        {
            get { return ldec_Monto; }
            set { ldec_Monto = value; }
        }
        public string Lstr_Reorganizacion
        {
            get { return lstr_Reorganizacion; }
            set { lstr_Reorganizacion = value; }
        }
        public string Lstr_TermReorganizacion
        {
            get { return lstr_TermReorganizacion; }
            set { lstr_TermReorganizacion = value; }
        }
        public string Lstr_TerminoCredito
        {
            get { return lstr_TerminoCredito; }
            set { lstr_TerminoCredito = value; }
        }
        public string Lstr_TipoAcuerdo
        {
            get { return lstr_TipoAcuerdo; }
            set { lstr_TipoAcuerdo = value; }
        }
        public string Lstr_TipoFinanciamiento
        {
            get { return lstr_TipoFinanciamiento; }
            set { lstr_TipoFinanciamiento = value; }
        }

        #endregion 

        #region Constructor

        public clsConsultaTramo(string lstr_IdPrestamo, int? lint_IdTramo, string lstr_TipoAcuerdo, string lstr_TipoFinanciamiento,
            string lstr_TerminoCredito, string lstr_Reorganizacion, string lstr_TermReorganizacion, decimal? ldec_Monto,string lstr_IdMoneda)
        {
            this.lstr_IdPrestamo = lstr_IdPrestamo;
            this.lint_IdTramo = lint_IdTramo;
            this.lstr_IdMoneda = lstr_IdMoneda;
            this.ldec_Monto = ldec_Monto;
            this.lstr_Reorganizacion = lstr_Reorganizacion;
            this.lstr_TermReorganizacion = lstr_TermReorganizacion;
            this.lstr_TerminoCredito = lstr_TerminoCredito;
            this.lstr_TipoAcuerdo = lstr_TipoAcuerdo;
            this.lstr_TipoFinanciamiento = lstr_TipoFinanciamiento;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaExterna\\ConsultarTramo.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}