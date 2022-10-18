using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Seguridad
{
    public class clsConsultarRol : clsProcedimientoAlmacenado
    {
        private string lint_IdRol;
        public string Lint_IdRol
        {
            get { return lint_IdRol; }
            set { lint_IdRol = value; }
        }

        private string lstr_DescRol;
        public string Lstr_DescRol
        {
            get { return lstr_DescRol; }
            set { lstr_DescRol = value; }
        }

        public clsConsultarRol(string int_IdRol, string str_DescRol)
        {
            lint_IdRol = int_IdRol;
            lstr_DescRol = str_DescRol;

            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\Seguridad\\ConsultarRoles.config", this);
        }

    }
}