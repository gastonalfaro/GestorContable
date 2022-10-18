using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarSociedadesFinancieras : clsProcedimientoAlmacenado
    {
        private string lstr_IdSociedadFi;
        public string Lstr_IdSociedadFi
        {
            get { return lstr_IdSociedadFi; }
            set { lstr_IdSociedadFi = value; }
        }

        private string lstr_IdSociedadGL;
        public string Lstr_IdSociedadGL
        {
            get { return lstr_IdSociedadGL; }
            set { lstr_IdSociedadGL = value; }
        }

        private string lstr_Denominacion;
        public string Lstr_Denominacion
        {
            get { return lstr_Denominacion; }
            set { lstr_Denominacion = value; }
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

        private string lstr_IdMoneda;
        public string Lstr_IdMoneda
        {
            get { return lstr_IdMoneda; }
            set { lstr_IdMoneda = value; }
        }

        public clsConsultarSociedadesFinancieras(string str_IdSociedadFi, string str_IdSociedadGL, string str_Denominacion, string str_NomSociedad, string str_IdPais, string str_IdMoneda)
        {
            lstr_IdSociedadFi = str_IdSociedadFi;
            lstr_IdSociedadGL = str_IdSociedadGL;
            lstr_Denominacion = str_Denominacion;
            lstr_NomSociedad = str_NomSociedad;
            lstr_IdPais = str_IdPais;
            lstr_IdMoneda = str_IdMoneda;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarSociedadesFinancieras.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}