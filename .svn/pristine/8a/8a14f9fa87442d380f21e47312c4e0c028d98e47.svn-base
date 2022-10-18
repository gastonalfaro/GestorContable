using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.RevelacionNotas
{
    public class clsAutorizarRevelacionPendiente : clsProcedimientoAlmacenado
    {
        private string lstr_IdRevelacionPendiente;
        public string Lstr_IdRevelacionPendiente
        {
            get { return lstr_IdRevelacionPendiente; }
            set { lstr_IdRevelacionPendiente = value; }
        }


        public clsAutorizarRevelacionPendiente(string str_IdRevelacionPendiente)
        {
            lstr_IdRevelacionPendiente = str_IdRevelacionPendiente;
            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\RevelacionNotas\\AutorizarRevelacionPendiente.config", this);
        }
    }
}