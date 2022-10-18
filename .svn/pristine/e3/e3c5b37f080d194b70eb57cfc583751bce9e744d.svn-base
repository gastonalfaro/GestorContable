using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Seguridad
{
    public class clsActualizarPerfilUsuario : clsProcedimientoAlmacenado
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

        private string lstr_ClaveActual;
        public string Lstr_ClaveActual
        {
            get { return lstr_ClaveActual; }
            set { lstr_ClaveActual = value; }
        }

        private string lstr_NuevaClave;
        public string Lstr_NuevaClave
        {
            get { return lstr_NuevaClave; }
            set { lstr_NuevaClave = value; }
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

        public clsActualizarPerfilUsuario(string str_CedUsuario, string str_ClaveActual, string str_NuevaClave, string str_Usuario,
            string str_FchModifica)
        {
            lstr_CedUsuario = str_CedUsuario;
            lstr_ClaveActual = str_ClaveActual;
            lstr_NuevaClave = str_NuevaClave;
            lstr_Usuario = str_Usuario;
            ldat_FchModifica = str_FchModifica;
            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\Seguridad\\ActualizarPerfilUsuario.config", this);
        }
    }
}