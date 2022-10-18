using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.RevelacionNotas
{
    public class clsEliminarRevelacionPendiente : clsProcedimientoAlmacenado
    {
        private string lstr_IdRevelacionPendiente;
        public string Lstr_IdRevelacionPendiente
        {
            get { return lstr_IdRevelacionPendiente; }
            set { lstr_IdRevelacionPendiente = value; }
        }


        public clsEliminarRevelacionPendiente(string str_IdRevelacionPendiente)
        {
            lstr_IdRevelacionPendiente = str_IdRevelacionPendiente;
            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\RevelacionNotas\\EliminarRevelacionPendiente.config", this);
        }
    }
}