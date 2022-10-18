using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsCrearElementoPEP : clsProcedimientoAlmacenado
    {
        private string lstr_IdElementoPEP;
        public string Lstr_IdElementoPEP
        {
            get { return lstr_IdElementoPEP; }
            set { lstr_IdElementoPEP = value; }
        }
      

        private string lstr_NomElementoPEP;
        public string Lstr_NomElementoPEP
        {
            get { return lstr_NomElementoPEP; }
            set { lstr_NomElementoPEP = value; }
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

        public clsCrearElementoPEP(string str_IdElementoPEP, string str_NomElementoPEP, string str_Estado, string str_UsrCreacion)
        {
            lstr_IdElementoPEP = str_IdElementoPEP;
            lstr_NomElementoPEP = str_NomElementoPEP;            
            lstr_Estado = str_Estado;
            lstr_UsrCreacion = str_UsrCreacion;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\CrearElementoPEP.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}