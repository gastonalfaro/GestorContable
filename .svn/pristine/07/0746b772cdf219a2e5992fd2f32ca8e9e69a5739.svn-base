using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsCrearModulo : clsProcedimientoAlmacenado
    {
        private string lstr_IdModulo;
        public string Lstr_IdModulo
        {
            get { return lstr_IdModulo; }
            set { lstr_IdModulo = value; }
        }


        private string lstr_NomModulo;
        public string Lstr_NomModulo
        {
            get { return lstr_NomModulo; }
            set { lstr_NomModulo = value; }
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

        public clsCrearModulo(string str_IdModulo, string str_NomModulo, string str_Estado, string str_UsrCreacion)
        {
            lstr_IdModulo = str_IdModulo;
            lstr_NomModulo = str_NomModulo;
            lstr_Estado = str_Estado;
            lstr_UsrCreacion = str_UsrCreacion;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\CrearModulo.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}