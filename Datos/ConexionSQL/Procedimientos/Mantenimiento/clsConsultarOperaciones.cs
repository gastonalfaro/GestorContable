using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarOperaciones : clsProcedimientoAlmacenado
    {
        private string lstr_IdOperacion;
        public string Lstr_IdOperacion
        {
            get { return lstr_IdOperacion; }
            set { lstr_IdOperacion = value; }
        }

        private string lstr_IdModulo;
        public string Lstr_IdModulo
        {
            get { return lstr_IdModulo; }
            set { lstr_IdModulo = value; }
        }

        private string lstr_NomOperacion;
        public string Lstr_NomOperacion
        {
            get { return lstr_NomOperacion; }
            set { lstr_NomOperacion = value; }
        }


        public clsConsultarOperaciones(string str_IdOperacion, string str_IdModulo, string str_NomOperacion)
        {
            lstr_IdOperacion = str_IdOperacion;
            lstr_IdModulo = str_IdModulo;
            lstr_NomOperacion = str_NomOperacion;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarOperaciones.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}