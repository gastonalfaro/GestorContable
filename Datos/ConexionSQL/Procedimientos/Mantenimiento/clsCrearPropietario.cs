using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsCrearPropietario : clsProcedimientoAlmacenado
    {
        private string lstr_IdPropietario;
        public string Lstr_IdPropietario
        {
            get { return lstr_IdPropietario; }
            set { lstr_IdPropietario = value; }
        }

        private string lstr_IdSociedadGL;
        public string Lstr_IdSociedadGL
        {
            get { return lstr_IdSociedadGL; }
            set { lstr_IdSociedadGL = value; }
        }

        private string lstr_IdSociedadFi;
        public string Lstr_IdSociedadFi
        {
            get { return lstr_IdSociedadFi; }
            set { lstr_IdSociedadFi = value; }
        }

        private string lstr_NomPropietario;
        public string Lstr_NomPropietario
        {
            get { return lstr_NomPropietario; }
            set { lstr_NomPropietario = value; }
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

        public clsCrearPropietario(string str_IdPropietario, string str_IdSociedadGL, string str_IdSociedadFi, string str_NomPropietario, string str_Estado, string str_UsrCreacion)
        {
            lstr_IdPropietario = str_IdPropietario;
            lstr_IdSociedadGL = str_IdSociedadGL;
            lstr_IdSociedadFi = str_IdSociedadFi;
            lstr_NomPropietario = str_NomPropietario;
            lstr_Estado = str_Estado;
            lstr_UsrCreacion = str_UsrCreacion;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\CrearPropietario.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}