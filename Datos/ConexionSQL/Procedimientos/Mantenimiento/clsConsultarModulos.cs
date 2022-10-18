using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarModulos : clsProcedimientoAlmacenado
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


        public clsConsultarModulos(string str_IdModulo, string str_NomModulo)
        {
            lstr_IdModulo = str_IdModulo;
            lstr_NomModulo = str_NomModulo;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarModulos.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}