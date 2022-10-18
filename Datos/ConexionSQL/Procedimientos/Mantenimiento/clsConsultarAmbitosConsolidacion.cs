using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarAmbitosConsolidacion : clsProcedimientoAlmacenado
    {
        private string lstr_IdAmbitoConsolidacion;
        public string Lstr_IdAmbitoConsolidacion
        {
            get { return lstr_IdAmbitoConsolidacion; }
            set { lstr_IdAmbitoConsolidacion = value; }
        }

        private string lstr_Vista;
        public string Lstr_Vista
        {
            get { return lstr_Vista; }
            set { lstr_Vista = value; }
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

        public clsConsultarAmbitosConsolidacion(string str_Vista, string str_IdAmbitoConsolidacion, string str_NomCorto, string str_NomAmbito)
        {
            lstr_IdAmbitoConsolidacion = str_IdAmbitoConsolidacion;
            lstr_Vista = str_Vista;
            lstr_NomCorto = str_NomCorto;
            lstr_NomAmbito = str_NomAmbito;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarAmbitosConsolidacion.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}