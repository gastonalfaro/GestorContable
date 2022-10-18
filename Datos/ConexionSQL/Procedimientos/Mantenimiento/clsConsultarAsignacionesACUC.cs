using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarAsignacionesACUC : clsProcedimientoAlmacenado
    {

        private string lstr_Vista;
        public string Lstr_Vista
        {
            get { return lstr_Vista; }
            set { lstr_Vista = value; }
        }


        private string lstr_Version;
        public string Lstr_Version
        {
            get { return lstr_Version; }
            set { lstr_Version = value; }
        }

        private string lstr_IdAmbitoConsolidacion;
        public string Lstr_IdAmbitoConsolidacion
        {
            get { return lstr_IdAmbitoConsolidacion; }
            set { lstr_IdAmbitoConsolidacion = value; }
        }

        private string lstr_IdUnidadConsolidacion;
        public string Lstr_IdUnidadConsolidacion
        {
            get { return lstr_IdUnidadConsolidacion; }
            set { lstr_IdUnidadConsolidacion = value; }
        }

        private string lstr_IdEjercicio;
        public string Lstr_IdEjercicio
        {
            get { return lstr_IdEjercicio; }
            set { lstr_IdEjercicio = value; }
        }

        private string lstr_IdPeriodo;
        public string Lstr_IdPeriodo
        {
            get { return lstr_IdPeriodo; }
            set { lstr_IdPeriodo = value; }
        }

        private Boolean lbln_EsUnidad;
        public Boolean Lbln_EsUnidad
        {
            get { return lbln_EsUnidad; }
            set { lbln_EsUnidad = value; }
        }

        public clsConsultarAsignacionesACUC(string str_Vista, string str_Version, string str_IdAmbitoConsolidacion, string str_IdUnidadConsolidacion, string str_IdEjercicio, string str_IdPeriodo, Boolean bln_EsUnidad)
        {
            lstr_Vista = str_Vista;
            lstr_Version = str_Version;
            lstr_IdAmbitoConsolidacion = str_IdAmbitoConsolidacion;
            lstr_IdUnidadConsolidacion = str_IdUnidadConsolidacion;
            lstr_IdPeriodo = str_IdPeriodo;
            lstr_IdEjercicio = str_IdEjercicio;
            lbln_EsUnidad = bln_EsUnidad;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarAsignacionesACUC.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}