using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Seguridad
{
    public class clsCerrarSesionesActivas : clsProcedimientoAlmacenado
    {
         private string lstr_IdUsuario;
        public string Lstr_IdUsuario
        {
            get { return lstr_IdUsuario; }
            set { lstr_IdUsuario = value; }
        }



        public clsCerrarSesionesActivas(string str_IdUsuario)
        {
            lstr_IdUsuario = str_IdUsuario;
            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\Seguridad\\CerrarSesionesActivas.config", this);
        }
    }
}