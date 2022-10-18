using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarFondos : clsProcedimientoAlmacenado
    {
        private string lstr_IdFondo;
        public string Lstr_IdFondo
        {
            get { return lstr_IdFondo; }
            set { lstr_IdFondo = value; }
        }

        private string lstr_IdEntidadCP;
        public string Lstr_IdEntidadCP
        {
            get { return lstr_IdEntidadCP; }
            set { lstr_IdEntidadCP = value; }
        }


        private string lstr_Denominacion;
        public string Lstr_Denominacion
        {
            get { return lstr_Denominacion; }
            set { lstr_Denominacion = value; }
        }

        private string lstr_NomFondo;
        public string Lstr_NomFondo
        {
            get { return lstr_NomFondo; }
            set { lstr_NomFondo = value; }
        }


        public clsConsultarFondos(string str_IdFondo, string str_IdEntidadCP, string str_Denominacion, string str_NomFondo)
        {
            lstr_IdFondo = str_IdFondo;
            lstr_IdEntidadCP = str_IdEntidadCP;
            lstr_Denominacion = str_Denominacion;
            lstr_NomFondo = str_NomFondo;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarFondos.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}