using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarNemotecnicos : clsProcedimientoAlmacenado
    {
        private string lstr_IdNemotecnico;
        public string Lstr_IdNemotecnico
        {
            get { return lstr_IdNemotecnico; }
            set { lstr_IdNemotecnico = value; }
        }

        private string lstr_IdSociedadFi;
        public string Lstr_IdSociedadFi
        {
            get { return lstr_IdSociedadFi; }
            set { lstr_IdSociedadFi = value; }
        }

        private string lstr_NomNemotecnico;
        public string Lstr_NomNemotecnico
        {
            get { return lstr_NomNemotecnico; }
            set { lstr_NomNemotecnico = value; }
        }

        private string lstr_IdMoneda;
        public string Lstr_IdMoneda
        {
            get { return lstr_IdMoneda; }
            set { lstr_IdMoneda = value; }
        }

        private string lstr_TipoNemotecnico;
        public string Lstr_TipoNemotecnico
        {
            get { return lstr_TipoNemotecnico; }
            set { lstr_TipoNemotecnico = value; }
        }

        public clsConsultarNemotecnicos(string str_IdNemotecnico, string str_IdSociedadFi, string str_NomNemotecnico, string str_IdMoneda, string str_TipoNemotecnico)
        {
            lstr_IdNemotecnico = str_IdNemotecnico;
            lstr_IdSociedadFi = str_IdSociedadFi;
            lstr_NomNemotecnico = str_NomNemotecnico;
            lstr_IdMoneda = str_IdMoneda;
            lstr_TipoNemotecnico = str_TipoNemotecnico;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarNemotecnicos.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}