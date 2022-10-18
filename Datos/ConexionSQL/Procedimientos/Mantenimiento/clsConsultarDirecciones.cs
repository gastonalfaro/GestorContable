using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarDirecciones : clsProcedimientoAlmacenado
    {
        private string lstr_IdDireccion;
        public string Lstr_IdDireccion
        {
            get { return lstr_IdDireccion; }
            set { lstr_IdDireccion = value; }
        }

        private string lstr_IdSociedadGL;
        public string Lstr_IdSociedadGL
        {
            get { return lstr_IdSociedadGL; }
            set { lstr_IdSociedadGL = value; }
        }

        private string lstr_NomDireccion;
        public string Lstr_NomDireccion
        {
            get { return lstr_NomDireccion; }
            set { lstr_NomDireccion = value; }
        }


        public clsConsultarDirecciones(string str_IdDireccion, string str_IdSociedadGL, string str_NomDireccion)
        {
            lstr_IdDireccion = str_IdDireccion;
            lstr_NomDireccion = str_NomDireccion;
            lstr_IdSociedadGL = str_IdSociedadGL;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarDirecciones.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}