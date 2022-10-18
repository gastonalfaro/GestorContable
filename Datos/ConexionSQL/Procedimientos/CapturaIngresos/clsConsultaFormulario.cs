using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CapturaIngresos
{
    public class clsConsultaFormulario : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_IdPersona;
        private string lstr_TipoIdPersona;
        private string lstr_Estado;

        #endregion

        #region Obtención y asignación

        public string Lstr_IdPersona
        {
            get { return lstr_IdPersona; }
            set { lstr_IdPersona = value; }
        }

        public string Lstr_TipoIdPersona
        {
            get { return lstr_TipoIdPersona; }
            set { lstr_TipoIdPersona = value; }
        }
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        #endregion 

        #region Constructor

        public clsConsultaFormulario(string lstr_IdPersona, string lstr_TipoIdPersona, string lstr_Estado)
        {
            this.lstr_IdPersona = lstr_IdPersona;
            this.lstr_TipoIdPersona = lstr_TipoIdPersona;
            this.lstr_Estado = lstr_Estado;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CapturaIngresos\\ConsultarFormulario.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion

    }
}