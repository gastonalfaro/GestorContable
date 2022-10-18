using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Seguridad
{
    public class clsConfirmarUsuario : clsProcedimientoAlmacenado
    {
        private string lstr_IdUsuario;
        public string Lstr_IdUsuario
        {
            get { return lstr_IdUsuario; }
            set { lstr_IdUsuario = value; }
        }

        private string lstr_Clave;
        public string Lstr_Clave
        {
            get { return lstr_Clave; }
            set { lstr_Clave = value; }
        }

        private string lstr_CodActivacion;
        public string Lstr_CodActivacion
        {
            get { return lstr_CodActivacion; }
            set { lstr_CodActivacion = value; }
        }

        private string ldat_FchModifica;
        public string Ldat_FchModifica
        {
            get { return ldat_FchModifica; }
            set { ldat_FchModifica = value; }
        }

        private string lstr_IPMaquina;
        public string Lstr_IPMaquina
        {
            get { return lstr_IPMaquina; }
            set { lstr_IPMaquina = value; }
        }

        public clsConfirmarUsuario(string str_IdUsuario, string str_Clave, string str_CodActivacion, string dat_FchModifica, string str_IPMaquina)
        {
            lstr_IdUsuario = str_IdUsuario;
            lstr_Clave = str_Clave;
            lstr_CodActivacion = str_CodActivacion;
            lstr_IPMaquina = str_IPMaquina;
            ldat_FchModifica = dat_FchModifica;
            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\Seguridad\\ConfirmarUsuario.config", this);
        }
    }
}