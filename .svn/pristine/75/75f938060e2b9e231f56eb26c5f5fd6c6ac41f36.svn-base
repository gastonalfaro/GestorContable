using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros
{
    public class clsConsultarAsientos : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private int? lint_Consecutivo;
        private string lstr_IdModulo;
        private string lstr_IdMovimiento;
        private string lstr_IdOperacion;
        private string lstr_CodAsiento;
        private DateTime? ldt_FchDesde;
        private DateTime? ldt_FchHasta;

        #endregion

        #region Obtención y asignación

        public int? Lint_Consecutivo { get { return lint_Consecutivo; } set { lint_Consecutivo = value; } }
        public DateTime? Ldt_FchDesde { get { return ldt_FchDesde; } set { ldt_FchDesde = value; } }
        public DateTime? Ldt_FchHasta { get { return ldt_FchHasta; } set { ldt_FchHasta = value; } }
        public string Lstr_IdModulo { get { return lstr_IdModulo; } set { lstr_IdModulo = value; } }
        public string Lstr_IdMovimiento { get { return lstr_IdMovimiento; } set { lstr_IdMovimiento = value; } }
        public string Lstr_IdOperacion { get { return lstr_IdOperacion; } set { lstr_IdOperacion = value; } }
        public string Lstr_CodAsiento { get { return lstr_CodAsiento; } set { lstr_CodAsiento = value; } }

        #endregion

        #region Constructor

        public clsConsultarAsientos(
              int? lint_Consecutivo,
            string lstr_IdModulo,
            string lstr_IdMovimiento,
            string lstr_IdOperacion,
            string lstr_CodAsiento,
            DateTime? ldt_FchDesde,
            DateTime? ldt_FchHasta
            )
        {
            this.lint_Consecutivo = lint_Consecutivo;
            this.lstr_IdModulo = lstr_IdModulo;
            this.lstr_IdMovimiento = lstr_IdMovimiento;
            this.lstr_IdOperacion = lstr_IdOperacion;
            this.lstr_CodAsiento = lstr_CodAsiento;
            this.ldt_FchDesde = ldt_FchDesde;
            this.ldt_FchHasta = ldt_FchHasta;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\ConsultarAsientos.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}