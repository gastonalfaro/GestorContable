using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsCrearJerarquiaPeriodo : clsProcedimientoAlmacenado
    {
        private string lstr_Vista;
        public string Lstr_Vista
        {
            get { return lstr_Vista; }
            set { lstr_Vista = value; }
        }

        private string lstr_IdJerarquia;
        public string Lstr_IdJerarquia
        {
            get { return lstr_IdJerarquia; }
            set { lstr_IdJerarquia = value; }
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


        private string lstr_IdAmbitoConsolidacion;
        public string Lstr_IdAmbitoConsolidacion
        {
            get { return lstr_IdAmbitoConsolidacion; }
            set { lstr_IdAmbitoConsolidacion = value; }
        }

        private string lstr_UsrCreacion;
        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }

        public clsCrearJerarquiaPeriodo(string str_Vista, string str_IdJerarquia, string str_IdEjercicio, string str_IdPeriodo, string str_IdAmbitoConsolidacion, string str_UsrCreacion)
        {
            lstr_Vista = str_Vista;
            lstr_IdJerarquia = str_IdJerarquia;
            lstr_IdEjercicio = str_IdEjercicio;
            lstr_IdPeriodo = str_IdPeriodo;
            lstr_IdAmbitoConsolidacion = str_IdAmbitoConsolidacion;
            lstr_UsrCreacion = str_UsrCreacion;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\CrearJerarquiaPeriodo.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}