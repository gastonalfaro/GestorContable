using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Seguridad
{
    public class clsConsultarSesionesUsuario : clsProcedimientoAlmacenado
    {
        private string lstr_IdSesionUsuario;
        public string Lstr_IdSesionUsuario
        {
            get { return lstr_IdSesionUsuario; }
            set { lstr_IdSesionUsuario = value; }
        }

        private string lstr_IdUsuario;
        public string Lstr_IdUsuario
        {
            get { return lstr_IdUsuario; }
            set { lstr_IdUsuario = value; }
        }

        private string lstr_SesionesActivas;
        public string Lstr_SesionesActivas
        {
            get { return lstr_SesionesActivas; }
            set { lstr_SesionesActivas = value; }
        }

        public clsConsultarSesionesUsuario(string str_IdSesionUsuario, string str_IdUsuario, string str_SesionesActivas)
        {
            lstr_IdSesionUsuario = str_IdSesionUsuario;
            lstr_IdUsuario = str_IdUsuario;
            lstr_SesionesActivas = str_SesionesActivas;
            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\Seguridad\\ConsultarSesionesUsuario.config", this);
        }
    }
}