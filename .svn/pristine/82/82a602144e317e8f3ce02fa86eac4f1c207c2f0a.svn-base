using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CapturaIngresos
{
    public class clsConsultarSociedades : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_Estado;

        #endregion

        #region Obtención y asignación

        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        #endregion 

        #region Constructor

        public clsConsultarSociedades(string lstr_Estado)
        {
            this.lstr_Estado = lstr_Estado;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CapturaIngresos\\ConsultarSociedades.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}