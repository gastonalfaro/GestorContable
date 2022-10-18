using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna
{
    public class clsCreaTramo : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_IdPrestamo;
        private int lint_IdTramo;
        private string lstr_TipoAcuerdo;
        private string lstr_TipoFinanciamiento;
        private string lstr_TipoInstrumento;
        private string lstr_TerminoCredito;
        private string lstr_Reorganizacion;
        private string lstr_TerminoReorganizado;
        private decimal ldec_Monto;
        private string lstr_IdMoneda;
        private decimal ldec_Tasa;
        private string lstr_Estado;
        private string lstr_UsrCreacion;

        #endregion

        #region Obtención y asignación

        public string Lstr_IdPrestamo
        {
            get { return lstr_IdPrestamo; }
            set { lstr_IdPrestamo = value; }
        }
        public int Lint_IdTramo
        {
            get { return lint_IdTramo; }
            set { lint_IdTramo = value; }
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
        public string Lstr_TipoInstrumento
        {
            get { return lstr_TipoInstrumento; }
            set { lstr_TipoInstrumento = value; }
        }
        public string Lstr_TerminoCredito
        {
            get { return lstr_TerminoCredito; }
            set { lstr_TerminoCredito = value; }
        }
        public string Lstr_Reorganizacion
        {
            get { return lstr_Reorganizacion; }
            set { lstr_Reorganizacion = value; }
        }
        public string Lstr_TerminoReorganizado
        {
            get { return lstr_TerminoReorganizado; }
            set { lstr_TerminoReorganizado = value; }
        }
        public decimal Ldec_Monto
        {
            get { return ldec_Monto; }
            set { ldec_Monto = value; }
        }
        public string Lstr_IdMoneda
        {
            get { return lstr_IdMoneda; }
            set { lstr_IdMoneda = value; }
        }
        public decimal Ldec_Tasa
        {
            get { return ldec_Tasa; }
            set { ldec_Tasa = value; }
        }
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }
        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }

        #endregion 

        #region Constructor

        public clsCreaTramo(string lstr_IdPrestamo, int lint_IdTramo, string lstr_TipoAcuerdo, string lstr_TipoFinanciamiento, string lstr_TipoInstrumento,
            string lstr_TerminoCredito, string lstr_Reorganizacion, string lstr_TerminoReorganizado, decimal ldec_Monto,
            string lstr_IdMoneda, decimal ldec_Tasa, string lstr_Estado, string lstr_UsrCreacion)
        {
            this.lstr_IdPrestamo = lstr_IdPrestamo;
            this.lint_IdTramo = lint_IdTramo;
            this.lstr_TipoAcuerdo = lstr_TipoAcuerdo;
            this.lstr_TipoFinanciamiento = lstr_TipoFinanciamiento;
            this.lstr_TipoInstrumento = lstr_TipoInstrumento;
            this.lstr_TerminoCredito = lstr_TerminoCredito;
            this.lstr_Reorganizacion = lstr_Reorganizacion;
            this.lstr_TerminoReorganizado = lstr_TerminoReorganizado;
            this.ldec_Monto = ldec_Monto;
            this.lstr_IdMoneda = lstr_IdMoneda;
            this.ldec_Tasa = ldec_Tasa;
            this.lstr_Estado = lstr_Estado;
            this.lstr_UsrCreacion = lstr_UsrCreacion;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaExterna\\CrearTramo.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}