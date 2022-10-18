using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Seguridad
{
    public class clsAsignarRolUsuario : clsProcedimientoAlmacenado
    {
        private string lstr_IdUsuario;
        public string Lstr_IdUsuario
        {
            get { return lstr_IdUsuario; }
            set { lstr_IdUsuario = value; }
        }

        private int lint_IdRol;
        public int Lint_IdRol
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

        public clsAsignarRolUsuario(string str_IdUsuario, int int_IdRol, string str_UsuarioAdmin)
        {
            lstr_IdUsuario = str_IdUsuario;
            lint_IdRol = int_IdRol;
            lstr_UsuarioAdmin = str_UsuarioAdmin;
            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\Seguridad\\AsignarRolUsuario.config", this);
        }
    }
}