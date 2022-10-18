using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Seguridad
{
    public class clsLoguearUsuario : clsProcedimientoAlmacenado
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

        private string lstr_IPMaquina;
        public string Lstr_IPMaquina
        { 
            get { return lstr_IPMaquina; }
            set { lstr_IPMaquina = value; }
        }

        public clsLoguearUsuario (string str_IdUsuario, string str_Clave, string str_IPMaquina)
        {
            lstr_IdUsuario = str_IdUsuario;
            lstr_Clave = str_Clave;
            lstr_IPMaquina = str_IPMaquina;
            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\Seguridad\\LoguearUsuario.config", this);
        }
    }
}