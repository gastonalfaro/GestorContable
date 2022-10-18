using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsCrearAsignacionACUC : clsProcedimientoAlmacenado
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

        private string lstr_UsrCreacion;
        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }

        public clsCrearAsignacionACUC(string str_Vista, string str_Version, string str_IdAmbitoConsolidacion, string str_IdUnidadConsolidacion, string str_IdEjercicio, string str_IdPeriodo, Boolean bln_EsUnidad, string str_UsrCreacion)
        {
            lstr_Vista = str_Vista;
            lstr_Version = str_Version;
            lstr_IdAmbitoConsolidacion = str_IdAmbitoConsolidacion;
            lstr_IdUnidadConsolidacion = str_IdUnidadConsolidacion;
            lstr_IdEjercicio = str_IdEjercicio;
            lstr_IdPeriodo = str_IdPeriodo;
            lbln_EsUnidad = bln_EsUnidad;
            lstr_UsrCreacion = str_UsrCreacion;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\CrearAsignacionACUC.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}