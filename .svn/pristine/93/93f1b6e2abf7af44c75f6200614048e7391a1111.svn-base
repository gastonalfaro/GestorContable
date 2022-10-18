using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsCrearSociedadGL : clsProcedimientoAlmacenado
    {
        private string lstr_IdSociedadGL;
        public string Lstr_IdSociedadGL
        {
            get { return lstr_IdSociedadGL; }
            set { lstr_IdSociedadGL = value; }
        }

        private string lstr_NomSociedad;
        public string Lstr_NomSociedad
        {
            get { return lstr_NomSociedad; }
            set { lstr_NomSociedad = value; }
        }

        private string lstr_IdPais;
        public string Lstr_IdPais
        {
            get { return lstr_IdPais; }
            set { lstr_IdPais = value; }
        }

        private string lstr_Poblacion;
        public string Lstr_Poblacion
        {
            get { return lstr_Poblacion; }
            set { lstr_Poblacion = value; }
        }

        private string lstr_Calle;
        public string Lstr_Calle
        {
            get { return lstr_Calle; }
            set { lstr_Calle = value; }
        }

        private string lstr_IdMoneda;
        public string Lstr_IdMoneda
        {
            get { return lstr_IdMoneda; }
            set { lstr_IdMoneda = value; }
        }

        private string lstr_IdIdioma;
        public string Lstr_IdIdioma
        {
            get { return lstr_IdIdioma; }
            set { lstr_IdIdioma = value; }
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

        public clsCrearSociedadGL(string str_IdSociedadGL, string str_NomSociedad, string str_IdPais, string str_Poblacion, string str_Calle, string str_IdMoneda, string str_IdIdioma, string str_Estado, string str_UsrCreacion)
        {
            lstr_IdSociedadGL = str_IdSociedadGL;
            lstr_NomSociedad = str_NomSociedad;
            lstr_IdPais = str_IdPais;
            lstr_Poblacion = str_Poblacion;
            lstr_Calle = str_Calle;
            lstr_IdMoneda = str_IdMoneda;
            lstr_IdIdioma = str_IdIdioma;
            lstr_Estado = str_Estado;
            lstr_UsrCreacion = str_UsrCreacion;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\CrearSociedadGL.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            } 
        }
    }
}