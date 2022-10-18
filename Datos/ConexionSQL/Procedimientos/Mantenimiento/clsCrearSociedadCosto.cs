using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsCrearSociedadCosto : clsProcedimientoAlmacenado
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


        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        private string lstr_UsrCreacion;
        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }

        public clsCrearSociedadCosto(string str_IdSociedadCo, string str_NomSociedad, string str_Estado, string str_UsrCreacion)
        {
            lstr_IdSociedadCo = str_IdSociedadCo;
            lstr_NomSociedad = str_NomSociedad;
            lstr_Estado = str_Estado;
            lstr_UsrCreacion = str_UsrCreacion;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\CrearSociedadCosto.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            } 
        }
    }
}