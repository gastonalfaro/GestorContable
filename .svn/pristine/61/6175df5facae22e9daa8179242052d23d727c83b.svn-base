using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarCentrosBeneficio : clsProcedimientoAlmacenado
    {
        private string lstr_IdCentroBeneficio;
        public string Lstr_IdCentroBeneficio
        {
            get { return lstr_IdCentroBeneficio; }
            set { lstr_IdCentroBeneficio = value; }
        }

        private DateTime ldt_FchConsulta;
        public DateTime Ldt_FchConsulta
        {
            get { return ldt_FchConsulta; }
            set { ldt_FchConsulta = value; }
        }

        private DateTime ldt_FchVigenciaHasta;
        public DateTime Ldt_FchVigenciaHasta
        {
            get { return ldt_FchVigenciaHasta; }
            set { ldt_FchVigenciaHasta = value; }
        }

        private string lstr_IdSociedadCo;
        public string Lstr_IdSociedadCo
        {
            get { return lstr_IdSociedadCo; }
            set { lstr_IdSociedadCo = value; }
        }

        private string lstr_IdSociedadFi;
        public string Lstr_IdSociedadFi
        {
            get { return lstr_IdSociedadFi; }
            set { lstr_IdSociedadFi = value; }
        }

        private string lstr_Denominacion;
        public string Lstr_Denominacion
        {
            get { return lstr_Denominacion; }
            set { lstr_Denominacion = value; }
        }

        private string lstr_NomCentroBeneficio;
        public string Lstr_NomCentroBeneficio
        {
            get { return lstr_NomCentroBeneficio; }
            set { lstr_NomCentroBeneficio = value; }
        }


        public clsConsultarCentrosBeneficio(string str_IdCentroBeneficio, DateTime dt_FchVigenciaHasta, string str_IdSociedadCo, string str_IdSociedadFi, DateTime dt_FchConsulta, string str_Denominacion, string str_NomCentroBeneficio)
        {
            lstr_IdCentroBeneficio = str_IdCentroBeneficio;
            ldt_FchVigenciaHasta = dt_FchVigenciaHasta;
            lstr_IdSociedadCo = str_IdSociedadCo;
            lstr_IdSociedadFi = str_IdSociedadFi;
            ldt_FchConsulta = dt_FchConsulta;
            lstr_Denominacion = str_Denominacion;
            lstr_NomCentroBeneficio = str_NomCentroBeneficio;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarCentrosBeneficio.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}