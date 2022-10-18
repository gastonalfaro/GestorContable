using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarPosicionesPresupuestarias : clsProcedimientoAlmacenado
    {
        private string lstr_IdPosPre;
        public string Lstr_IdPosPre
        {
            get { return lstr_IdPosPre; }
            set { lstr_IdPosPre = value; }
        }

        private string lstr_IdEntidadCP;
        public string Lstr_IdEntidadCP
        {
            get { return lstr_IdEntidadCP; }
            set { lstr_IdEntidadCP = value; }
        }

        private string lstr_IdEjercicio;
        public string Lstr_IdEjercicio
        {
            get { return lstr_IdEjercicio; }
            set { lstr_IdEjercicio = value; }
        }

        private string lstr_Denominacion;
        public string Lstr_Denominacion
        {
            get { return lstr_Denominacion; }
            set { lstr_Denominacion = value; }
        }

        private string lstr_NomPosPre;
        public string Lstr_NomPosPre
        {
            get { return lstr_NomPosPre; }
            set { lstr_NomPosPre = value; }
        }

        public clsConsultarPosicionesPresupuestarias(string str_IdPosPre, string str_IdEntidadCP, string str_IdEjercicio, string str_Denominacion, string str_NomPosPre)
        {
            lstr_IdPosPre = str_IdPosPre;
            lstr_IdEntidadCP = str_IdEntidadCP;
            lstr_IdEjercicio = str_IdEjercicio;
            lstr_Denominacion = str_Denominacion;
            lstr_NomPosPre = str_NomPosPre;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarPosicionesPresupuestarias.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}