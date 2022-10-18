using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarProgramas : clsProcedimientoAlmacenado
    {
        private string lstr_IdPrograma;
        public string Lstr_IdPrograma
        {
            get { return lstr_IdPrograma; }
            set { lstr_IdPrograma = value; }
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

        private string lstr_NomPrograma;
        public string Lstr_NomPrograma
        {
            get { return lstr_NomPrograma; }
            set { lstr_NomPrograma = value; }
        }

        public clsConsultarProgramas(string str_IdPrograma, string str_IdEntidadCP, string str_Denominacion, string str_NomPrograma)
        {
            lstr_IdPrograma = str_IdPrograma;
            lstr_IdEntidadCP = str_IdEntidadCP;
            lstr_Denominacion = str_Denominacion;
            lstr_NomPrograma = str_NomPrograma;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarProgramas.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}