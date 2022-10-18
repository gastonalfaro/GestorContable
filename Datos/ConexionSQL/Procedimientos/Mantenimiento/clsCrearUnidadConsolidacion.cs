using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsCrearUnidadConsolidacion : clsProcedimientoAlmacenado
    {
        private string lstr_Vista;
        public string Lstr_Vista
        {
            get { return lstr_Vista; }
            set { lstr_Vista = value; }
        }

        private string lstr_IdUnidadConsolidacion;
        public string Lstr_IdUnidadConsolidacion
        {
            get { return lstr_IdUnidadConsolidacion; }
            set { lstr_IdUnidadConsolidacion = value; }
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

        public clsCrearUnidadConsolidacion(string str_Vista, string str_IdUnidadConsolidacion, string str_NomCorto, string str_NomUnidad, string str_Estado, string str_UsrCreacion)
        {
            lstr_Vista = str_Vista;
            lstr_IdUnidadConsolidacion = str_IdUnidadConsolidacion;
            lstr_NomCorto = str_NomCorto;
            lstr_NomUnidad = str_NomUnidad;
            lstr_Estado = str_Estado;
            lstr_UsrCreacion = str_UsrCreacion;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\CrearUnidadConsolidacion.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            } 
        }
    }
}