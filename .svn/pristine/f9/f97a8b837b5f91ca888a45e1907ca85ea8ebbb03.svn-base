using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsCrearCentroBeneficio : clsProcedimientoAlmacenado
    {
        private string lstr_IdCentroBeneficio;
        public string Lstr_IdCentroBeneficio
        {
            get { return lstr_IdCentroBeneficio; }
            set { lstr_IdCentroBeneficio = value; }
        }

        private DateTime? ldt_FchVigencia;
        public DateTime? Ldt_FchVigencia
        {
            get { return ldt_FchVigencia; }
            set { ldt_FchVigencia = value; }
        }

        private DateTime? ldt_FchVigenciaHasta;
        public DateTime? Ldt_FchVigenciaHasta
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

        public clsCrearCentroBeneficio(string str_IdCentroBeneficio, DateTime? dt_FchVigencia, DateTime? dt_FchVigenciaHasta, string str_IdSociedadCo, string str_IdSociedadFi, string str_Denominacion, string str_NomCentroBeneficio, string str_Estado, string str_UsrCreacion)
        {
            lstr_IdCentroBeneficio = str_IdCentroBeneficio;
            ldt_FchVigencia = dt_FchVigencia;
            ldt_FchVigenciaHasta = dt_FchVigenciaHasta;
            lstr_IdSociedadCo = str_IdSociedadCo;
            lstr_IdSociedadFi = str_IdSociedadFi;
            lstr_Denominacion = str_Denominacion;
            lstr_NomCentroBeneficio = str_NomCentroBeneficio;            
            lstr_Estado = str_Estado;
            lstr_UsrCreacion = str_UsrCreacion;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\CrearCentroBeneficio.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }

        }
    }
}