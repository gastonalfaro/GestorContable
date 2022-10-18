using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaInterna
{
    public class clsConsultaColumnaDevengoMensual : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_NroEmision;

        #endregion

        #region Obtención y asignación

        public string Lstr_NroEmision
        {
            get { return lstr_NroEmision; }
            set { lstr_NroEmision = value; }
        }

        #endregion 

        #region Constructor

        public clsConsultaColumnaDevengoMensual(string lstr_NroEmision)
        {
            this.lstr_NroEmision = lstr_NroEmision;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaInterna\\ConsultarColumnaDevengoMensual.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}