using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros
{
    public class clsCrearAsiento : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private int lint_Consecutivo;
        private int? lint_ConsecutivoOrigen;
        private string lstr_IdModulo;
        private string lstr_IdOperacion;
        private DateTime ldt_Fecha;
        private string lstr_IdMovimiento;
        private string lstr_Detalle;
        private string lstr_Estado;
        private string lstr_CodAsiento;
        private string lstr_LogAsiento;
        private string lstr_UsrCreacion;
        private int lint_TmpConsecutivo;

        #endregion

        #region Obtención y asignación

        public int Lint_Consecutivo { get { return lint_Consecutivo; } set { lint_Consecutivo = value; } }
        public int? Lint_ConsecutivoOrigen { get { return lint_ConsecutivoOrigen; } set { lint_ConsecutivoOrigen = value; } }
        public string Lstr_IdModulo { get { return lstr_IdModulo; } set { lstr_IdModulo = value; } }
        public string Lstr_IdOperacion { get { return lstr_IdOperacion; } set { lstr_IdOperacion = value; } }
        public DateTime Ldt_Fecha { get { return ldt_Fecha; } set { ldt_Fecha = value; } }
        public string Lstr_IdMovimiento { get { return lstr_IdMovimiento; } set { lstr_IdMovimiento = value; } }
        public string Lstr_Detalle { get { return lstr_Detalle; } set { lstr_Detalle = value; } }
        public string Lstr_Estado { get { return lstr_Estado; } set { lstr_Estado = value; } }
        public string Lstr_CodAsiento { get { return lstr_CodAsiento; } set { lstr_CodAsiento = value; } }
        public string Lstr_LogAsiento { get { return lstr_LogAsiento; } set { lstr_LogAsiento = value; } }
        public string Lstr_UsrCreacion { get { return lstr_UsrCreacion; } set { lstr_UsrCreacion = value; } }
        public int Lint_TmpConsecutivo { get { return lint_TmpConsecutivo; } set { lint_TmpConsecutivo = value; } }

        #endregion

        #region Constructor

        public clsCrearAsiento(int lint_Consecutivo, string lstr_IdModulo, string lstr_IdOperacion, int? lint_ConsecutivoOrigen,
            string lstr_IdMovimiento, DateTime ldt_Fecha, string lstr_Detalle, string lstr_Estado,  string lstr_CodAsiento, string lstr_LogAsiento, string lstr_UsrCreacion)
        {
            this.lint_Consecutivo = lint_Consecutivo;
            this.lint_ConsecutivoOrigen = lint_ConsecutivoOrigen;
            this.lstr_IdModulo = lstr_IdModulo;
            this.lstr_IdOperacion = lstr_IdOperacion;
            this.ldt_Fecha = ldt_Fecha;
            this.lstr_IdMovimiento = lstr_IdMovimiento;
            this.lstr_Detalle = lstr_Detalle;  
            this.lstr_Estado = lstr_Estado;  
            this.lstr_CodAsiento = lstr_CodAsiento;
            this.lstr_LogAsiento = lstr_LogAsiento;
            this.lstr_UsrCreacion = lstr_UsrCreacion;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\CrearAsiento.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}