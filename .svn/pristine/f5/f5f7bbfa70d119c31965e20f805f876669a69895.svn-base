using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarCentrosCosto : clsProcedimientoAlmacenado
    {
        private string lstr_IdCentroCosto;
        public string Lstr_IdCentroCosto
        {
            get { return lstr_IdCentroCosto; }
            set { lstr_IdCentroCosto = value; }
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

        private string lstr_NomCentroCosto;
        public string Lstr_NomCentroCosto
        {
            get { return lstr_NomCentroCosto; }
            set { lstr_NomCentroCosto = value; }
        }


        public clsConsultarCentrosCosto(string str_IdCentroCosto, DateTime dt_FchVigenciaHasta, string str_IdSociedadCo, string str_IdSociedadFi, DateTime dt_FchConsulta, string str_Denominacion, string str_NomCentroCosto)
        {
            lstr_IdCentroCosto = str_IdCentroCosto;
            ldt_FchVigenciaHasta = dt_FchVigenciaHasta;
            lstr_IdSociedadCo = str_IdSociedadCo;
            lstr_IdSociedadFi = str_IdSociedadFi;
            ldt_FchConsulta = dt_FchConsulta;
            lstr_Denominacion = str_Denominacion;
            lstr_NomCentroCosto = str_NomCentroCosto;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarCentrosCosto.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}