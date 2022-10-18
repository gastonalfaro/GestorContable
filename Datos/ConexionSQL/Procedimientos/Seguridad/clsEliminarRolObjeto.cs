using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Seguridad
{
    public class clsEliminarRolObjeto : clsProcedimientoAlmacenado
    {
        private string lint_IdRol;
        public string Lint_IdRol
        {
            get { return lint_IdRol; }
            set { lint_IdRol = value; }
        }

        private string lstr_IdObjeto;
        public string Lstr_IdObjeto
        {
            get { return lstr_IdObjeto; }
            set { lstr_IdObjeto = value; }
        }

        private string lstr_FchModifica;
        public string Lstr_FchModifica
        {
            get { return lstr_FchModifica; }
            set { lstr_FchModifica = value; }
        }

        public clsEliminarRolObjeto(string int_IdRol, string str_IdObjeto, string str_FchModifica)
        {
            lint_IdRol = int_IdRol;
            lstr_IdObjeto = str_IdObjeto;
            lstr_FchModifica = str_FchModifica;
            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\Seguridad\\EliminarRolObjeto.config", this);
        }
    }
}