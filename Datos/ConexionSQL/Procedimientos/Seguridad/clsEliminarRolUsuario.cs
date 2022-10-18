using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Seguridad
{
    public class clsEliminarRolUsuario : clsProcedimientoAlmacenado
    {
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

        private string lstr_FchModifica;
        public string Lstr_FchModifica
        {
            get { return lstr_FchModifica; }
            set { lstr_FchModifica = value; }
        }

        public clsEliminarRolUsuario(string str_IdUsuario, string int_IdRol, string str_FchModifica)
        {
            lstr_IdUsuario = str_IdUsuario;
            lint_IdRol = int_IdRol;
            lstr_FchModifica = str_FchModifica;
            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\Seguridad\\EliminarRolUsuario.config", this);
        }
    }
}