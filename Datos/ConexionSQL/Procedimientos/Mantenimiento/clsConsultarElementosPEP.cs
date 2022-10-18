using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarElementosPEP : clsProcedimientoAlmacenado
    {
        private string lstr_IdElementoPEP;
        public string Lstr_IdElementoPEP
        {
            get { return lstr_IdElementoPEP; }
            set { lstr_IdElementoPEP = value; }
        }

        private string lstr_NomElementoPEP;
        public string Lstr_NomElementoPEP
        {
            get { return lstr_NomElementoPEP; }
            set { lstr_NomElementoPEP = value; }
        }


        public clsConsultarElementosPEP(string str_IdElementoPEP, string str_NomElementoPEP)
        {
            lstr_IdElementoPEP = str_IdElementoPEP;
            lstr_NomElementoPEP = str_NomElementoPEP;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarElementosPEP.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}