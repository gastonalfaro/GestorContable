using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarPaises : clsProcedimientoAlmacenado
    {
        private string lstr_IdPais;
        public string Lstr_IdPais
        {
            get { return lstr_IdPais; }
            set { lstr_IdPais = value; }
        }

        private string lstr_NomPais;
        public string Lstr_NomPais
        {
            get { return lstr_NomPais; }
            set { lstr_NomPais = value; }
        }


        public clsConsultarPaises(string str_IdPais, string str_NomPais)
        {
            lstr_IdPais = str_IdPais;
            lstr_NomPais = str_NomPais;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarPaises.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}