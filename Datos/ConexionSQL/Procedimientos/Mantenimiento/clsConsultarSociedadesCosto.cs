using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarSociedadesCosto : clsProcedimientoAlmacenado
    {
        private string lstr_IdSociedadCo;
        public string Lstr_IdSociedadCo
        {
            get { return lstr_IdSociedadCo; }
            set { lstr_IdSociedadCo = value; }
        }

        private string lstr_NomSociedad;
        public string Lstr_NomSociedad
        {
            get { return lstr_NomSociedad; }
            set { lstr_NomSociedad = value; }
        }


        public clsConsultarSociedadesCosto(string str_IdSociedadCo, string str_NomSociedad)
        {
            lstr_IdSociedadCo = str_IdSociedadCo;
            lstr_NomSociedad = str_NomSociedad;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarSociedadesCosto.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}