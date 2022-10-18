using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros
{
    public class clsEliminarAsientosLineas : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private int lint_Consecutivo;

        #endregion

        #region Obtención y asignación

        public int Lint_Consecutivo { get { return lint_Consecutivo; } set { lint_Consecutivo = value; } }

        #endregion

        #region Constructor

        public clsEliminarAsientosLineas(
              int lint_Consecutivo
            )
        {
            this.lint_Consecutivo = lint_Consecutivo;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\EliminarAsientosLineas.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}