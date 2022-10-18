using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsCrearMoneda : clsProcedimientoAlmacenado
    {
        private string lstr_IdMoneda;
        public string Lstr_IdMoneda
        {
            get { return lstr_IdMoneda; }
            set { lstr_IdMoneda = value; }
        }


        private string lstr_NomMoneda;
        public string Lstr_NomMoneda
        {
            get { return lstr_NomMoneda; }
            set { lstr_NomMoneda = value; }
        }


        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        private char lstr_ConversionUSD;
        public char Lstr_ConversionUSD
        {
            get { return lstr_ConversionUSD; }
            set { lstr_ConversionUSD = value; }
        }

        private string lstr_UsrCreacion;
        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }

        public clsCrearMoneda(string str_IdMoneda, string str_NomMoneda, string str_Estado, string str_UsrCreacion, char str_ConversionUSD = '/')
        {
            lstr_IdMoneda = str_IdMoneda;
            lstr_NomMoneda = str_NomMoneda;
            lstr_Estado = str_Estado;
            lstr_UsrCreacion = str_UsrCreacion;
            lstr_ConversionUSD = str_ConversionUSD;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\CrearMoneda.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}