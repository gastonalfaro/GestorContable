using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.SubirArchivo
{
    public class clsEliminarArchivo:clsProcedimientoAlmacenado
    {
         private string lint_IdArchivo;
        public string Lint_IdArchivo
        {
            get { return lint_IdArchivo; }
            set { lint_IdArchivo = value; }
        }

        private string ldat_FchModifica;
        public string Ldat_FchModifica
        {
            get { return ldat_FchModifica; }
            set { ldat_FchModifica = value; }
        }

        public clsEliminarArchivo(string str_IdArchivo, string dat_FchModifica)
        {
            Lint_IdArchivo = str_IdArchivo;
            ldat_FchModifica = dat_FchModifica;
            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\RevelacionNotas\\EliminarArchivo.config", this);
        }
    }
}