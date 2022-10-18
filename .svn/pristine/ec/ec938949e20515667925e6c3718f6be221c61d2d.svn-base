using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna
{
    public class clsConsultarPrestamosReclasificar  : clsProcedimientoAlmacenado
    {
      #region Parámetros


        private DateTime? ldt_FechaFin;
 
        public DateTime? Ldt_FechaFin
        {
            get { return ldt_FechaFin; }
            set { ldt_FechaFin = value; }
        }
 
        #endregion 

        #region Constructor

        public clsConsultarPrestamosReclasificar(DateTime? ldt_FechaFin)
        {

            this.ldt_FechaFin = ldt_FechaFin;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaExterna\\ConsultarPrestamosReclasificar.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}