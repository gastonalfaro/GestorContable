using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsCrearAmbitoConsolidacion : clsProcedimientoAlmacenado
    {
        private string lstr_Vista;
        public string Lstr_Vista
        {
            get { return lstr_Vista; }
            set { lstr_Vista = value; }
        }

        private string lstr_IdAmbitoConsolidacion;
        public string Lstr_IdAmbitoConsolidacion
        {
            get { return lstr_IdAmbitoConsolidacion; }
            set { lstr_IdAmbitoConsolidacion = value; }
        }

        private string lstr_NomCorto;
        public string Lstr_NomCorto
        {
            get { return lstr_NomCorto; }
            set { lstr_NomCorto = value; }
        }

        private string lstr_NomAmbito;
        public string Lstr_NomAmbito
        {
            get { return lstr_NomAmbito; }
            set { lstr_NomAmbito = value; }
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

        public clsCrearAmbitoConsolidacion(string str_Vista, string str_IdAmbitoConsolidacion, string str_NomCorto, string str_NomAmbito, string str_Estado, string str_UsrCreacion)
        {
            lstr_Vista = str_Vista;
            lstr_IdAmbitoConsolidacion = str_IdAmbitoConsolidacion;
            lstr_NomCorto = str_NomCorto;
            lstr_NomAmbito = str_NomAmbito;
            lstr_Estado = str_Estado;
            lstr_UsrCreacion = str_UsrCreacion;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\CrearAmbitoConsolidacion.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}