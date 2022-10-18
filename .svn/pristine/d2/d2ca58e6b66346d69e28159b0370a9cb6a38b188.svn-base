using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsBorrarOficinaCeBe : clsProcedimientoAlmacenado
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

        private string lstr_IdModulo;
        public string Lstr_IdModulo
        {
            get { return lstr_IdModulo; }
            set { lstr_IdModulo = value; }
        }

        private string lstr_IdCentroBeneficio;
        public string Lstr_IdCentroBeneficio
        {
            get { return lstr_IdCentroBeneficio; }
            set { lstr_IdCentroBeneficio = value; }
        }


        public clsBorrarOficinaCeBe(string str_IdOficina, string str_IdSociedadGL, string str_IdModulo, string str_IdCentroBeneficio)
        {
            lstr_IdOficina = str_IdOficina;
            lstr_IdSociedadGL = str_IdSociedadGL;
            lstr_IdModulo = str_IdModulo;
            lstr_IdCentroBeneficio = str_IdCentroBeneficio;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\BorrarOficinaCeBe.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}