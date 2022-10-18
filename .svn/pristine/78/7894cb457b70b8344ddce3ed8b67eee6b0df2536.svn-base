using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.RevelacionNotas
{
    public class clsConsultarFormulario:clsProcedimientoAlmacenado
    {
        private string lstr_IdRevelacion;
        public string Lstr_IdRevelacion
        {
            get { return lstr_IdRevelacion; }
            set { lstr_IdRevelacion = value; }
        }

        public clsConsultarFormulario(string str_IdRevelacion)
        {
            lstr_IdRevelacion = str_IdRevelacion;
            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\RevelacionNotas\\ConsultarFomulario.config", this);
        }
    }
}