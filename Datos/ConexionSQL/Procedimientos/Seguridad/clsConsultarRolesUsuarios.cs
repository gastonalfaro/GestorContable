using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Seguridad
{
    public class clsConsultarRolesUsuarios : clsProcedimientoAlmacenado
    {
        private string lstr_IdRol;
        public string Lstr_IdRol
        {
            get { return lstr_IdRol; }
            set { lstr_IdRol = value; }
        }

        private string lstr_IdUsuario;
        public string Lstr_IdUsuario
        {
            get { return lstr_IdUsuario; }
            set { lstr_IdUsuario = value; }
        }

        public clsConsultarRolesUsuarios(string str_IdRol, string str_IdUsuario)
        {
            lstr_IdRol = str_IdRol;
            lstr_IdUsuario = str_IdUsuario;
            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\Seguridad\\ConsultarRolesUsuarios.config", this);
        }
    }
}