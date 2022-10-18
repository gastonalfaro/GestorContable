using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Seguridad
{
    public class clsActualizarObjeto : clsProcedimientoAlmacenado
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

        private string lstr_Habilitado;
        public string Lstr_Habilitado
        {
            get { return lstr_Habilitado; }
            set { lstr_Habilitado = value; }
        }

        private string lstr_DescObjeto;
        public string Lstr_DescObjeto
        {
            get { return lstr_DescObjeto; }
            set { lstr_DescObjeto = value; }
        }

        private string lstr_UsrModificacion;
        public string Lstr_UsrModificacion
        {
            get { return lstr_UsrModificacion; }
            set { lstr_UsrModificacion = value; }
        }

        public clsActualizarObjeto(string str_IdObjeto, string str_IdModulo, string str_Habilitado, string str_DescObjeto, string str_UsrModificacion)
        {
            lstr_IdObjeto = str_IdObjeto;
            lstr_IdModulo = str_IdModulo;
            lstr_Habilitado = str_Habilitado;
            lstr_DescObjeto = str_DescObjeto;
            lstr_UsrModificacion = str_UsrModificacion;
            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\Seguridad\\ActualizarObjeto.config", this);
        }
    }
}