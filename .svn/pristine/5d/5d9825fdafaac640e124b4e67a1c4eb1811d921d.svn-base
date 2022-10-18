using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Seguridad
{
    public class clsConsultarRolesObjetos : clsProcedimientoAlmacenado
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

        private string lstr_DescObjeto;
        public string Lstr_DescObjeto
        {
            get { return lstr_DescObjeto; }
            set { lstr_DescObjeto = value; }
        }

        public clsConsultarRolesObjetos(string int_IdRol, string str_IdObjeto, string str_DescObjeto)
        {
            lint_IdRol = int_IdRol;
            lstr_IdObjeto = str_IdObjeto;
            lstr_DescObjeto = str_DescObjeto;

            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\Seguridad\\ConsultarRolesObjetos.config", this);
        }
    }
}