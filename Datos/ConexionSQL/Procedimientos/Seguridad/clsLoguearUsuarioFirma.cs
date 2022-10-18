using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Seguridad
{
    public class clsLoguearUsuarioFirma : clsProcedimientoAlmacenado
    {
        private string lstr_IdUsuario;
        public string Lstr_IdUsuario
        {
            get { return lstr_IdUsuario; }
            set { lstr_IdUsuario = value; }
        }

        private string lstr_NomUsuario;
        public string Lstr_NomUsuario
        {
            get { return lstr_NomUsuario; }
            set { lstr_NomUsuario = value; }
        }


        private string lstr_IPMaquina;
        public string Lstr_IPMaquina
        {
            get { return lstr_IPMaquina; }
            set { lstr_IPMaquina = value; }
        }

        public clsLoguearUsuarioFirma(string str_IdUsuario, string str_NomUsuario, string str_IPMaquina)
        {
            lstr_IdUsuario = str_IdUsuario;
            lstr_IPMaquina = str_IPMaquina;
            lstr_NomUsuario = str_NomUsuario;
            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\Seguridad\\LoguearUsuarioFirma.config", this);
        }
    }
}