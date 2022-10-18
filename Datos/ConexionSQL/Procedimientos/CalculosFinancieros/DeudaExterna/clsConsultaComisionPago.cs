using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna
{
    public class clsConsultaComisionPago : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_IdPrestamo;
        private string lint_IdTramo;
        private DateTime? ldt_FchPago;
        private Int64? lint_Secuencia;
        private Int64? lint_Consecutivo;

        #endregion

        #region Obtención y asignación

        public string Lstr_IdPrestamo
        {
            get { return lstr_IdPrestamo; }
            set { lstr_IdPrestamo = value; }
        }
        public string Lint_IdTramo
        {
            get { return lint_IdTramo; }
            set { lint_IdTramo = value; }
        }
        public DateTime? Ldt_FchPago
        {
            get { return ldt_FchPago; }
            set { ldt_FchPago = value; }
        }
        public Int64? Lint_Secuencia
        {
            get { return lint_Secuencia; }
            set { lint_Secuencia = value; }
        }
        public Int64? Lint_Consecutivo
        {
            get { return lint_Consecutivo; }
            set { lint_Consecutivo = value; }
        }

        #endregion 

        #region Constructor

        public clsConsultaComisionPago(string lstr_IdPrestamo, string lint_IdTramo, DateTime? ldt_FchPago, Int64? lint_Secuencia, Int64? lint_Consecutivo)
        {
            this.lstr_IdPrestamo = lstr_IdPrestamo;
            this.lint_IdTramo = lint_IdTramo;
            this.ldt_FchPago = ldt_FchPago;
            this.lint_Secuencia = lint_Secuencia;
            this.lint_Consecutivo = lint_Consecutivo;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaExterna\\ConsultarComisionPago.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}