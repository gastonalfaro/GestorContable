using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.RevelacionNotas
{
    public class clsAutorizarCambiosRevelacion : clsProcedimientoAlmacenado
    {
        private string lstr_IdRevelacion;
        public string Lstr_IdRevelacion
        {
            get { return lstr_IdRevelacion; }
            set { lstr_IdRevelacion = value; }
        }


        private DateTime lstr_UltimoDiaModificacion;
        public DateTime Lstr_UltimoDiaModificacion
        {
            get { return lstr_UltimoDiaModificacion; }
            set { lstr_UltimoDiaModificacion = value; }
        }

        private DateTime lstr_FchModifica;
        public DateTime Lstr_FchModifica
        {
            get { return lstr_FchModifica; }
            set { lstr_FchModifica = value; }
        }

        private string lstr_UsrModifica;
        public string Lstr_UsrModifica
        {
            get { return lstr_UsrModifica; }
            set { lstr_UsrModifica = value; }
        }



        public clsAutorizarCambiosRevelacion(string str_IdRevelacion, DateTime str_UltimoDiaMod, DateTime str_FchModifica, string str_UsrModifica)
        {
            lstr_IdRevelacion = str_IdRevelacion;
            lstr_UltimoDiaModificacion = str_UltimoDiaMod;
            lstr_FchModifica = str_FchModifica;
            lstr_UsrModifica = str_UsrModifica;
            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\RevelacionNotas\\AutorizarCambiosRevelacion.config", this);
        }
    }
}