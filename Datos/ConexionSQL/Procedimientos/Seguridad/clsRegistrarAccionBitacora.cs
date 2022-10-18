using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Seguridad
{
    public class clsRegistrarAccionBitacora : clsProcedimientoAlmacenado
    {
        /// <summary>
        /// Cedula del Usuario
        /// </summary>

        private string lstr_IdSesionUsuario;
        public string Lstr_IdSesionUsuario
        {
            get { return lstr_IdSesionUsuario; }
            set { lstr_IdSesionUsuario = value; }
        }

        private string lstr_IdModulo;
        public string Lstr_IdModulo
        {
            get { return lstr_IdModulo; }
            set { lstr_IdModulo = value; }
        }
        private string lstr_IdOperacion;
        public string Lstr_IdOperacion
        {
            get { return lstr_IdOperacion; }
            set { lstr_IdOperacion = value; }
        }
        private string lstr_IdTransaccion;
        public string Lstr_IdTransaccion
        {
            get { return lstr_IdTransaccion; }
            set { lstr_IdTransaccion = value; }
        }
        private string lstr_IdSociedadGL;
        public string Lstr_IdSociedadGL
        {
            get { return lstr_IdSociedadGL; }
            set { lstr_IdSociedadGL = value; }
        }
        /// <summary>
        /// Contrasena de registro
        /// </summary>
        private string lstr_Accion;
        public string Lstr_Accion
        {
            get { return lstr_Accion; }
            set { lstr_Accion = value; }
        }

        /// <summary>
        /// Correo electronico
        /// </summary>
        private string lstr_Detalle;
        public string Lstr_Detalle
        {
            get { return lstr_Detalle; }
            set { lstr_Detalle = value; }
        }

        public clsRegistrarAccionBitacora(string str_IdModulo, string str_IdSesionUsuario,
            string str_Accion, string str_Detalle, string str_IdOperacion = null, string str_IdTransaccion = null, string str_IdSociedadGL = null)
        {
            lstr_IdModulo = str_IdModulo;
            lstr_IdSesionUsuario = str_IdSesionUsuario;
            lstr_Accion = str_Accion;
            lstr_Detalle = str_Detalle;
            lstr_IdOperacion = str_IdOperacion;
            lstr_IdTransaccion = str_IdTransaccion;
            lstr_IdSociedadGL = str_IdSociedadGL;
            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\Seguridad\\RegistrarAccionBitacora.config", this);
        }
    }
}