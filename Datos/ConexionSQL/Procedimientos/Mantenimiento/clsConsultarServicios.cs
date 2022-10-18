using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarServicios : clsProcedimientoAlmacenado
    {
        private string lstr_IdServicio;
        public string Lstr_IdServicio
        {
            get { return lstr_IdServicio; }
            set { lstr_IdServicio = value; }
        }

        private string lstr_IdSociedadGL;
        public string Lstr_IdSociedadGL
        {
            get { return lstr_IdSociedadGL; }
            set { lstr_IdSociedadGL = value; }
        }

        private string lstr_IdOficina;
        public string Lstr_IdOficina
        {
            get { return lstr_IdOficina; }
            set { lstr_IdOficina = value; }
        }
        private string lstr_IdCuentaContable;
        public string Lstr_IdCuentaContable
        {
            get { return lstr_IdCuentaContable; }
            set { lstr_IdCuentaContable = value; }
        }

        private string lstr_IdPosPre;
        public string Lstr_IdPosPre
        {
            get { return lstr_IdPosPre; }
            set { lstr_IdPosPre = value; }
        }

        private string lstr_NomServicio;
        public string Lstr_NomServicio
        {
            get { return lstr_NomServicio; }
            set { lstr_NomServicio = value; }
        }

        public clsConsultarServicios(string str_IdServicio, string str_IdSociedadGL, string str_IdOficina, string str_NomServicio, string str_IdCuentaContable, string str_IdPosPre)
        {
            lstr_IdServicio = str_IdServicio;
            lstr_IdSociedadGL = str_IdSociedadGL;
            lstr_IdOficina = str_IdOficina;
            lstr_IdCuentaContable = str_IdCuentaContable;
            lstr_IdPosPre = str_IdPosPre;
            lstr_NomServicio = str_NomServicio;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarServicios.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}