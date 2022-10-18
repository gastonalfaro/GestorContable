using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsCrearClaseDocumento : clsProcedimientoAlmacenado
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

       
        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        private string lstr_UsrCreacion;
        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }

        public clsCrearClaseDocumento(string str_IdClaseDocumento, string str_NomClaseDocumento, string str_Estado, string str_UsrCreacion)
        {
            lstr_IdClaseDocumento = str_IdClaseDocumento;
            lstr_NomClaseDocumento = str_NomClaseDocumento;            
            lstr_Estado = str_Estado;
            lstr_UsrCreacion = str_UsrCreacion;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\CrearClaseDocumento.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}