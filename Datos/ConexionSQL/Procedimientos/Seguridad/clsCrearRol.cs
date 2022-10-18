using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Seguridad
{
    public class clsCrearRol : clsProcedimientoAlmacenado
    {
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

        private string lstr_UsrCreacion;
        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }

        public clsCrearRol(string str_DescRol, string str_IdSesionUsuario, string str_UsrCreacion)
        {
            lstr_DescRol = str_DescRol;
            lstr_IdSesionUsuario = 
            lstr_UsrCreacion = str_UsrCreacion;
            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\Seguridad\\CrearRol.config", this);
        }
    }
}