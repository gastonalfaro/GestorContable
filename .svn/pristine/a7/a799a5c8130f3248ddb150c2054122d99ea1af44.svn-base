using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Seguridad
{
    public class clsEliminarObjeto : clsProcedimientoAlmacenado
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

        private string lstr_FchModificacion;
        public string Lstr_FchModificacion
        {
            get { return lstr_FchModificacion; }
            set { lstr_FchModificacion = value; }
        }

        public clsEliminarObjeto(string str_IdObjeto, string str_IdModulo, string str_FchModificacion)
        {
            lstr_IdObjeto = str_IdObjeto;
            lstr_IdModulo = str_IdModulo;
            lstr_FchModificacion = str_FchModificacion;
            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\Seguridad\\EliminarObjeto.config", this);
        }

    }
}