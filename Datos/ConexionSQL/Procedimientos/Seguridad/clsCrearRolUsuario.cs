using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Seguridad
{
    public class clsCrearRolUsuario : clsProcedimientoAlmacenado
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

        private string lint_IdRol;
        public string Lint_IdRol
        { 
            get { return lint_IdRol; }
            set { lint_IdRol = value; }
        }

        private string lstr_UsuarioAdmin;
        public string Lstr_UsuarioAdmin
        {
            get { return lstr_UsuarioAdmin; }
            set { lstr_UsuarioAdmin = value; }
        }

        public clsCrearRolUsuario(string str_IdSesionUsuario, string str_IdUsuario, string int_IdRol, string str_UsuarioAdmin)
        {
            lstr_IdSesionUsuario = str_IdSesionUsuario;
            lstr_IdUsuario = str_IdUsuario;
            lint_IdRol = int_IdRol;
            lstr_UsuarioAdmin = str_UsuarioAdmin;
            try
            {
                string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
                EjecucionSP(str_DireccionConfigs + "\\Seguridad\\CrearRolUsuario.config", this);
            }
            catch
            { }
        }
    }
}