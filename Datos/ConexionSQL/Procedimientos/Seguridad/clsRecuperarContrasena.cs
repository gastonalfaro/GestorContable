using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Seguridad
{
    public class clsRecuperarContrasena:clsProcedimientoAlmacenado
    {
         /// <summary>
        /// Cedula del Usuario
        /// </summary>
        private string lstr_CedUsuario;
        public string Lstr_CedUsuario
        {
            get { return lstr_CedUsuario; }
            set { lstr_CedUsuario = value; }
        }


        /// <summary>
        /// Correo electronico
        /// </summary>
        private string lstr_Correo;
        public string Lstr_Correo
        {
            get { return lstr_Correo; }
            set { lstr_Correo = value; }
        }

        public clsRecuperarContrasena(string str_Cedula, string str_Correo)
        {
            lstr_CedUsuario = str_Cedula;
            lstr_Correo = str_Correo;
            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\Seguridad\\RecuperarContasena.config", this);
        }
    }
}