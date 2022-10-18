using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Seguridad
{
    public class clsConsultarObjetos : clsProcedimientoAlmacenado
    {
        private string lstr_IdObjeto;
        public string Lstr_IdObjeto
        {
            get { return lstr_IdObjeto; }
            set { lstr_IdObjeto = value; }
        }

        private string lstr_IdModulo;
        public string Lstr_IdModulo
        {
            get { return lstr_IdModulo; }
            set { lstr_IdModulo = value; }
        }

        private string lstr_DescObjeto;
        public string Lstr_DescObjeto
        {
            get { return lstr_DescObjeto; }
            set { lstr_DescObjeto = value; }
        }

        private string lstr_TipoObjeto;
        public string Lstr_TipoObjeto
        {
            get { return lstr_TipoObjeto; }
            set { lstr_TipoObjeto = value; }
        }

        public clsConsultarObjetos(string str_IdObjeto, string str_IdModulo, string str_DescObjeto, string str_TipoObjeto)
        {
            lstr_IdObjeto = str_IdObjeto;
            lstr_IdModulo = str_IdModulo;
            lstr_DescObjeto = str_DescObjeto;
            lstr_TipoObjeto = str_TipoObjeto;

            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\Seguridad\\ConsultarObjetos.config", this);
        }
    }
}