using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsCrearEntidadCP : clsProcedimientoAlmacenado
    {
        private string lstr_IdEntidadCP;
        public string Lstr_IdEntidadCP
        {
            get { return lstr_IdEntidadCP; }
            set { lstr_IdEntidadCP = value; }
        }


        private string lstr_NomEntidadCP;
        public string Lstr_NomEntidadCP
        {
            get { return lstr_NomEntidadCP; }
            set { lstr_NomEntidadCP = value; }
        }

        private string lstr_IdMoneda;
        public string Lstr_IdMoneda
        {
            get { return lstr_IdMoneda; }
            set { lstr_IdMoneda = value; }
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

        public clsCrearEntidadCP(string str_IdEntidadCP, string str_NomEntidadCP, string str_IdMoneda, string str_Estado, string str_UsrCreacion)
        {
            lstr_IdEntidadCP = str_IdEntidadCP;
            lstr_NomEntidadCP = str_NomEntidadCP;
            lstr_IdMoneda = str_IdMoneda;
            lstr_Estado = str_Estado;
            lstr_UsrCreacion = str_UsrCreacion;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\CrearEntidadCP.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}