using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarClasesDocumento : clsProcedimientoAlmacenado
    {
        private string lstr_IdClaseDocumento;
        public string Lstr_IdClaseDocumento
        {
            get { return lstr_IdClaseDocumento; }
            set { lstr_IdClaseDocumento = value; }
        }

        private string lstr_NomClaseDocumento;
        public string Lstr_NomClaseDocumento
        {
            get { return lstr_NomClaseDocumento; }
            set { lstr_NomClaseDocumento = value; }
        }


        public clsConsultarClasesDocumento(string str_IdClaseDocumento, string str_NomClaseDocumento)
        {
            lstr_IdClaseDocumento = str_IdClaseDocumento;
            lstr_NomClaseDocumento = str_NomClaseDocumento;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarClasesDocumento.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}