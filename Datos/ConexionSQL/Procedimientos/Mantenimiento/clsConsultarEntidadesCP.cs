using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarEntidadesCP : clsProcedimientoAlmacenado
    {
        private string lstr_IdEntidadCP;
        public string Lstr_IdEntidadCP
        {
            get { return lstr_IdEntidadCP; }
            set { lstr_IdEntidadCP = value; }
        }

        private string lstr_NomEntidadCP;
        public string Lstr_NomEntidadCP
        {
            get { return lstr_NomEntidadCP; }
            set { lstr_NomEntidadCP = value; }
        }

        private string lstr_IdMoneda;
        public string Lstr_IdMoneda
        {
            get { return lstr_IdMoneda; }
            set { lstr_IdMoneda = value; }
        }


        public clsConsultarEntidadesCP(string str_IdEntidadCP, string str_NomEntidadCP, string str_IdMoneda)
        {
            lstr_IdEntidadCP = str_IdEntidadCP;
            lstr_NomEntidadCP = str_NomEntidadCP;
            lstr_IdMoneda = str_IdMoneda;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarEntidadesCP.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }

        }
    }
}