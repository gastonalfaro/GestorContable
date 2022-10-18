using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarCentrosGestores : clsProcedimientoAlmacenado
    {
        private string lstr_IdCentroGestor;
        public string Lstr_IdCentroGestor
        {
            get { return lstr_IdCentroGestor; }
            set { lstr_IdCentroGestor = value; }
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

        private string lstr_IdEntidadCP;
        public string Lstr_IdEntidadCP
        {
            get { return lstr_IdEntidadCP; }
            set { lstr_IdEntidadCP = value; }
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

        private string lstr_NomCentroGestor;
        public string Lstr_NomCentroGestor
        {
            get { return lstr_NomCentroGestor; }
            set { lstr_NomCentroGestor = value; }
        }


        public clsConsultarCentrosGestores(string str_IdCentroGestor, DateTime dt_FchVigenciaHasta, string str_IdEntidadCP, string str_IdSociedadFi, DateTime dt_FchConsulta, string str_Denominacion, string str_NomCentroGestor)
        {
            lstr_IdCentroGestor = str_IdCentroGestor;
            ldt_FchVigenciaHasta = dt_FchVigenciaHasta;
            lstr_IdEntidadCP = str_IdEntidadCP;
            lstr_IdSociedadFi = str_IdSociedadFi;
            ldt_FchConsulta = dt_FchConsulta;
            lstr_Denominacion = str_Denominacion;
            lstr_NomCentroGestor = str_NomCentroGestor;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarCentrosGestores.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}