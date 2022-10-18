using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Seguridad
{
    public class clsRegistrarUsuarioFirma : clsProcedimientoAlmacenado
    {
         private string lstr_CedUsuario;
        public string Lstr_CedUsuario
        {
            get { return lstr_CedUsuario; }
            set { lstr_CedUsuario = value; }
        }

        private string lstr_TipoID;
        public string Lstr_TipoID
        {
            get { return lstr_TipoID; }
            set { lstr_TipoID = value; }
        }
        /// <summary>
        /// Contrasena de registro
        /// </summary>
        private string lstr_Clave;
        public string Lstr_Clave
        {
            get { return lstr_Clave; }
            set { lstr_Clave = value; }
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

        private string lstr_NomUsuario;
        public string Lstr_NomUsuario
        {
            get { return lstr_NomUsuario; }
            set { lstr_NomUsuario = value; }
        }

        public clsRegistrarUsuarioFirma(string str_Cedula, string str_TipoID, string str_Nombre, string str_Contrasena, 
            string str_Correo)
        {
            lstr_CedUsuario = str_Cedula;
            lstr_TipoID = str_TipoID;
            lstr_NomUsuario = str_Nombre;
            lstr_Clave = str_Contrasena;
            lstr_Correo = str_Correo;
            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\Seguridad\\RegistrarUsuarioFirma.config", this);
        }
    }
}