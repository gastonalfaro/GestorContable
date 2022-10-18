using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarUnidadesConsolidacion : clsProcedimientoAlmacenado
    {
        private string lstr_IdUnidadConsolidacion;
        public string Lstr_IdUnidadConsolidacion
        {
            get { return lstr_IdUnidadConsolidacion; }
            set { lstr_IdUnidadConsolidacion = value; }
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

        private string lstr_NomUnidad;
        public string Lstr_NomUnidad
        {
            get { return lstr_NomUnidad; }
            set { lstr_NomUnidad = value; }
        }

        public clsConsultarUnidadesConsolidacion(string str_Vista, string str_IdUnidadConsolidacion, string str_NomCorto, string str_NomUnidad)
        {
            lstr_IdUnidadConsolidacion = str_IdUnidadConsolidacion;
            lstr_Vista = str_Vista;
            lstr_NomCorto = str_NomCorto;
            lstr_NomUnidad = str_NomUnidad;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarAreasFuncionales.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}