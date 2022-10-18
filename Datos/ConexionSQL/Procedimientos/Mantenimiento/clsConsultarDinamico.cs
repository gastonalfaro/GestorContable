using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarDinamico : clsProcedimientoAlmacenado
    {      

        private string lstr_SQL;
        public string Lstr_SQL
        {
            get { return lstr_SQL; }
            set { lstr_SQL = value; }
        }
      

        public clsConsultarDinamico(string str_SQL)
        {
            lstr_SQL = str_SQL;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarDinamico.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}