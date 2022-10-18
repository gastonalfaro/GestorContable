using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsCrearOperacion : clsProcedimientoAlmacenado
    {
        private string lstr_IdOperacion;
        public string Lstr_IdOperacion
        {
            get { return lstr_IdOperacion; }
            set { lstr_IdOperacion = value; }
        }

        private string lstr_IdOperacionReversa;
        public string Lstr_IdOperacionReversa
        {
            get { return lstr_IdOperacionReversa; }
            set { lstr_IdOperacionReversa = value; }
        }

        private string lstr_IdModulo;
        public string Lstr_IdModulo
        {
            get { return lstr_IdModulo; }
            set { lstr_IdModulo = value; }
        }

        private string lstr_NomOperacion;
        public string Lstr_NomOperacion
        {
            get { return lstr_NomOperacion; }
            set { lstr_NomOperacion = value; }
        }

        private string lstr_IdClaseDoc;
        public string Lstr_IdClaseDoc
        {
            get { return lstr_IdClaseDoc; }
            set { lstr_IdClaseDoc = value; }
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

        public clsCrearOperacion(string str_IdOperacion, string str_IdModulo, string str_NomOperacion, string str_IdClaseDoc, string str_Estado, string str_IdOperacionReversa, string str_UsrCreacion)
        {
            lstr_IdOperacion = str_IdOperacion;
            lstr_IdModulo = str_IdModulo;
            lstr_NomOperacion = str_NomOperacion;
            lstr_IdClaseDoc = str_IdClaseDoc;
            lstr_Estado = str_Estado;
            lstr_IdOperacionReversa = str_IdOperacionReversa;
            lstr_UsrCreacion = str_UsrCreacion;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\CrearOperacion.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}