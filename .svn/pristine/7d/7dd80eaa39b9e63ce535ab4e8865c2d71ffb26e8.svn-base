using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarOficinas : clsProcedimientoAlmacenado
    {
        private string lstr_IdOficina;
        public string Lstr_IdOficina
        {
            get { return lstr_IdOficina; }
            set { lstr_IdOficina = value; }
        }

        private string lstr_IdSociedadGL;
        public string Lstr_IdSociedadGL
        {
            get { return lstr_IdSociedadGL; }
            set { lstr_IdSociedadGL = value; }
        }

        private string lstr_IdDireccion;
        public string Lstr_IdDireccion
        {
            get { return lstr_IdDireccion; }
            set { lstr_IdDireccion = value; }
        }

        private string lstr_NomOficina;
        public string Lstr_NomOficina
        {
            get { return lstr_NomOficina; }
            set { lstr_NomOficina = value; }
        }


        public clsConsultarOficinas(string str_IdOficina, string str_IdSociedadGL, string str_IdDireccion, string str_NomOficina)
        {
            lstr_IdOficina = str_IdOficina;
            lstr_IdSociedadGL = str_IdSociedadGL;
            lstr_IdDireccion = str_IdDireccion;
            lstr_NomOficina = str_NomOficina;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarOficinas.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}