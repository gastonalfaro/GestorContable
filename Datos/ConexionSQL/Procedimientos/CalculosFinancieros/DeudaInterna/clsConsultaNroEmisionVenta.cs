using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaInterna
{
    public class clsConsultaNroEmisionVenta : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_SistemaNegociacion;

        #endregion

        #region Obtención y asignación

        public string Lstr_SistemaNegociacion
        {
            get { return lstr_SistemaNegociacion; }
            set { lstr_SistemaNegociacion = value; }
        }

        #endregion 

        #region Constructor

        public clsConsultaNroEmisionVenta(string lstr_SistemaNegociacion)
        {
            this.lstr_SistemaNegociacion = lstr_SistemaNegociacion;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaInterna\\ConsultarNroEmisionVenta.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}