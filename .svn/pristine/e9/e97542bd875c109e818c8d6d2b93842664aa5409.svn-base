using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsCrearCentroGestor : clsProcedimientoAlmacenado
    {
        private string lstr_IdCentroGestor;
        public string Lstr_IdCentroGestor
        {
            get { return lstr_IdCentroGestor; }
            set { lstr_IdCentroGestor = value; }
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

        public clsCrearCentroGestor(string str_IdCentroGestor, DateTime? dt_FchVigencia, DateTime? dt_FchVigenciaHasta, string str_IdEntidadCP, string str_IdSociedadFi, string str_Denominacion, string str_NomCentroGestor, string str_Estado, string str_UsrCreacion)
        {
            lstr_IdCentroGestor = str_IdCentroGestor;
            ldt_FchVigencia = dt_FchVigencia;
            ldt_FchVigenciaHasta = dt_FchVigenciaHasta;
            lstr_IdEntidadCP = str_IdEntidadCP;
            lstr_IdSociedadFi = str_IdSociedadFi;
            lstr_Denominacion = str_Denominacion;            
            lstr_NomCentroGestor = str_NomCentroGestor;            
            lstr_Estado = str_Estado;
            lstr_UsrCreacion = str_UsrCreacion;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\CrearCentroGestor.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}