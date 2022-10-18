using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsCrearPais : clsProcedimientoAlmacenado
    {
        private string lstr_IdPais;
        public string Lstr_IdPais
        {
            get { return lstr_IdPais; }
            set { lstr_IdPais = value; }
        }

        private string lstr_Nacionalidad;
        public string Lstr_Nacionalidad
        {
            get { return lstr_Nacionalidad; }
            set { lstr_Nacionalidad = value; }
        }

        private string lstr_NomPais;
        public string Lstr_NomPais
        {
            get { return lstr_NomPais; }
            set { lstr_NomPais = value; }
        }


        private string lstr_IdMoneda;
        public string Lstr_IdMoneda
        {
            get { return lstr_IdMoneda; }
            set { lstr_IdMoneda = value; }
        }

        private string lstr_UsrCreacion;
        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }

        public clsCrearPais(string str_IdPais, string str_NomPais, string str_Nacionalidad, string str_IdMoneda, string str_UsrCreacion)
        {
            lstr_IdPais = str_IdPais;
            lstr_Nacionalidad = str_Nacionalidad;
            lstr_NomPais = str_NomPais;
            lstr_IdMoneda = str_IdMoneda;
            lstr_UsrCreacion = str_UsrCreacion;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\CrearPais.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}