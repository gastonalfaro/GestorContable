using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Seguridad
{
    public class clsCrearObjeto : clsProcedimientoAlmacenado
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

        private string lstr_TipoObjeto;
        public string Lstr_TipoObjeto
        {
            get { return lstr_TipoObjeto; }
            set { lstr_TipoObjeto = value; }
        }

        private string lstr_DescObjeto;
        public string Lstr_DescObjeto
        {
            get { return lstr_DescObjeto; }
            set { lstr_DescObjeto = value; }
        }

        private string lstr_UsrCreacion;
        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }

        public clsCrearObjeto (string str_IdObjeto, string str_IdModulo, string str_TipoObjeto, string str_DescObjeto, string str_UsrCreacion)
        {
            lstr_IdObjeto = str_IdObjeto;
            lstr_IdModulo = str_IdModulo;
            lstr_TipoObjeto = str_TipoObjeto;
            lstr_DescObjeto = str_DescObjeto;
            lstr_UsrCreacion = str_UsrCreacion;
            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\Seguridad\\CrearObjeto.config", this);
        }
    }
}