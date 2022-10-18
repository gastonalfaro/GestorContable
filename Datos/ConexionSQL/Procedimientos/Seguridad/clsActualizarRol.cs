using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Seguridad
{
    public class clsActualizarRol : clsProcedimientoAlmacenado
    {
        private string lint_IdRol;
        public string Lint_IdRol
        {
            get { return lint_IdRol; }
            set { lint_IdRol = value; }
        }

        private string lstr_DescRol;
        public string Lstr_DescRol
        {
            get { return lstr_DescRol; }
            set { lstr_DescRol = value; }
        }

        private string lstr_IdSesionUsuario;
        public string Lstr_IdSesionUsuario
        {
            get { return lstr_IdSesionUsuario; }
            set { lstr_IdSesionUsuario = value; }
        }

        private string lstr_Habilitado;
        public string Lstr_Habilitado
        {
            get { return lstr_Habilitado; }
            set { lstr_Habilitado = value; }
        }

        private string lstr_Usuario;
        public string Lstr_Usuario
        {
            get { return lstr_Usuario; }
            set { lstr_Usuario = value; }
        }

        private string ldat_FchModifica;
        public string Ldat_FchModifica
        {
            get { return ldat_FchModifica; }
            set { ldat_FchModifica = value; }
        }

        public clsActualizarRol(string int_IdRol, string str_DescRol, string str_IdSesionUsuario, string str_Habilitado, string str_Usuario, string dat_FchModifica)
        {
            lint_IdRol = int_IdRol;
            lstr_DescRol = str_DescRol;
            lstr_IdSesionUsuario = str_IdSesionUsuario;
            lstr_Habilitado = str_Habilitado;
            lstr_Usuario = str_Usuario;
            ldat_FchModifica = dat_FchModifica;
            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\Seguridad\\ActualizarRol.config", this);
        }
    }
}