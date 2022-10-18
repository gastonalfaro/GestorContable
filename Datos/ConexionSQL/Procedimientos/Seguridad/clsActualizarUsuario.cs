using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Seguridad
{
    public class clsActualizarUsuario : clsProcedimientoAlmacenado
    {
        /// <summary>
        /// Cedula del Usuario
        /// </summary>
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

        private string lstr_NomUsuario;
        public string Lstr_NomUsuario
        {
            get { return lstr_NomUsuario; }
            set { lstr_NomUsuario = value; }
        }

        /// <summary>
        /// Correo electronico
        /// </summary>
        private string lstr_CorreoUsuario;
        public string Lstr_CorreoUsuario
        {
            get { return lstr_CorreoUsuario; }
            set { lstr_CorreoUsuario = value; }
        }

        private string lboo_Activo;
        public string Lboo_Activo
        {
            get { return lboo_Activo; }
            set { lboo_Activo = value; }
        }

        private string lboo_Administrador;
        public string Lboo_Administrador
        {
            get { return lboo_Administrador; }
            set { lboo_Administrador = value; }
        }

        private string lboo_CtaHabilitada;
        public string Lboo_CtaHabilitada
        {
            get { return lboo_CtaHabilitada; }
            set { lboo_CtaHabilitada = value; }
        }

        private string lstr_IdSociedadGL;
        public string Lstr_IdSociedadGL
        {
            get { return lstr_IdSociedadGL; }
            set { lstr_IdSociedadGL = value; }
        }

        private string lstr_UsrModifica;
        public string Lstr_UsrModifica
        {
            get { return lstr_UsrModifica; }
            set { lstr_UsrModifica = value; }
        }

        private string lstr_FchModifica;
        public string Lstr_FchModifica
        {
            get { return lstr_FchModifica; }
            set { lstr_FchModifica = value; }
        }

        public clsActualizarUsuario(string str_IdUsuario, string str_TipoIdUsuario, string str_NomUsuario, string str_CorreoUsuario,
            string boo_Activo, string boo_Administrador, string boo_CtaHabilitada, string str_IdSociedadGL, 
            string str_UsrModifica, string str_FchModifica)
        {
            lstr_IdUsuario = str_IdUsuario;
            lstr_TipoIdUsuario = str_TipoIdUsuario;
            lstr_NomUsuario = str_NomUsuario;
            lstr_CorreoUsuario = str_CorreoUsuario;
            lboo_Activo = boo_Activo;
            lboo_Administrador = boo_Administrador;
            lboo_CtaHabilitada = boo_CtaHabilitada;
            lstr_IdSociedadGL = str_IdSociedadGL;
            lstr_UsrModifica = str_UsrModifica;
            lstr_FchModifica = str_FchModifica;
            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\Seguridad\\ActualizarUsuario.config", this);
        }
    }
}