using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarMonedas : clsProcedimientoAlmacenado
    {
        private string lstr_IdMoneda;
        public string Lstr_IdMoneda
        {
            get { return lstr_IdMoneda; }
            set { lstr_IdMoneda = value; }
        }

        private string lstr_NomMoneda;
        public string Lstr_NomMoneda
        {
            get { return lstr_NomMoneda; }
            set { lstr_NomMoneda = value; }
        }


        public clsConsultarMonedas(string str_IdMoneda, string str_NomMoneda)
        {
            lstr_IdMoneda = str_IdMoneda;
            lstr_NomMoneda = str_NomMoneda;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarMonedas.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}