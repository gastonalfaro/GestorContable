using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsModificarMoneda : clsProcedimientoAlmacenado
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

        private string lstr_UsrModifica;
        public string Lstr_UsrModifica
        {
            get { return lstr_UsrModifica; }
            set { lstr_UsrModifica = value; }
        }

        private DateTime ldt_FchModifica;
        public DateTime Ldt_FchModifica
        {
            get { return ldt_FchModifica; }
            set { ldt_FchModifica = value; }
        }

        public clsModificarMoneda(string str_IdMoneda, string str_NomMoneda, string str_Estado, string str_UsrModifica, DateTime dt_FchModifica, char str_ConversionUSD = '/')
        {
            lstr_IdMoneda = str_IdMoneda;
            lstr_NomMoneda = str_NomMoneda;
            lstr_Estado = str_Estado;
            lstr_UsrModifica = str_UsrModifica;
            ldt_FchModifica = dt_FchModifica;
            lstr_ConversionUSD = str_ConversionUSD;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ModificarMoneda.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}