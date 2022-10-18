using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Seguridad
{
    public class clsCerrarSesionUsuario:clsProcedimientoAlmacenado
    {
                private string lstr_IdUsuario;
        public string Lstr_IdUsuario
        {
            get { return lstr_IdUsuario; }
            set { lstr_IdUsuario = value; }
        }


        private string lstr_IdSesionUsuario;
        public string Lstr_IdSesionUsuario
        { 
            get { return lstr_IdSesionUsuario; }
            set { lstr_IdSesionUsuario = value; }
        }

        public clsCerrarSesionUsuario (string str_IdUsuario, string str_IdSesionUsuario)
        {
            lstr_IdUsuario = str_IdUsuario;
            lstr_IdSesionUsuario = str_IdSesionUsuario;
            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\Seguridad\\CerrarSesionUsuario.config", this);
        }

    }
}