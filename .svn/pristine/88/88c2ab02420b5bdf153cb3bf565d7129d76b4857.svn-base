using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsCrearJerarquia : clsProcedimientoAlmacenado
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

        private string lstr_NomCorto;
        public string Lstr_NomCorto
        {
            get { return lstr_NomCorto; }
            set { lstr_NomCorto = value; }
        }

        private string lstr_NomJerarquia;
        public string Lstr_NomJerarquia
        {
            get { return lstr_NomJerarquia; }
            set { lstr_NomJerarquia = value; }
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

        public clsCrearJerarquia(string str_Vista, string str_IdJerarquia, string str_NomCorto, string str_NomJerarquia, string str_Estado, string str_UsrCreacion)
        {
            lstr_Vista = str_Vista;
            lstr_IdJerarquia = str_IdJerarquia;
            lstr_NomCorto = str_NomCorto;
            lstr_NomJerarquia = str_NomJerarquia;
            lstr_Estado = str_Estado;
            lstr_UsrCreacion = str_UsrCreacion;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\CrearJerarquia.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}