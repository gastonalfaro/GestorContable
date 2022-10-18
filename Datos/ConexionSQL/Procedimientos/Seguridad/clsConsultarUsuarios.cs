using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Seguridad
{
    public class clsConsultarUsuarios : clsProcedimientoAlmacenado
    {
        private string lstr_IdUsuario;
        public string Lstr_IdUsuario
        {
            get { return lstr_IdUsuario; }
            set { lstr_IdUsuario = value; }
        }
        private string lstr_TipoIdUsuario;
        public string Lstr_TipoIdUsuario
        {
            get { return lstr_TipoIdUsuario; }
            set { lstr_TipoIdUsuario = value; }
        }
        private string lstr_IdSociedadGL;
        public string Lstr_IdSociedadGL
        {
            get { return lstr_IdSociedadGL; }
            set { lstr_IdSociedadGL = value; }
        }

        private string lstr_NomUsuario;
        public string Lstr_NomUsuario
        {
            get { return lstr_NomUsuario; }
            set { lstr_NomUsuario = value; }
        }

        public clsConsultarUsuarios(string str_IdUsuario, string str_TipoIdUsuario, string str_IdSociedadGL, string str_NomUsuario)
        {
            lstr_IdUsuario = str_IdUsuario;
            lstr_TipoIdUsuario = str_TipoIdUsuario;
            lstr_IdSociedadGL = str_IdSociedadGL;
            lstr_NomUsuario = str_NomUsuario;

            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\Seguridad\\ConsultarUsuarios.config", this);
        }
    }
}