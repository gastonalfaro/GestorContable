using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsModificarDireccion : clsProcedimientoAlmacenado
    {
        private string lstr_IdDireccion;
        public string Lstr_IdDireccion
        {
            get { return lstr_IdDireccion; }
            set { lstr_IdDireccion = value; }
        }

        private string lstr_IdSociedadGL;
        public string Lstr_IdSociedadGL
        {
            get { return lstr_IdSociedadGL; }
            set { lstr_IdSociedadGL = value; }
        }

        private string lstr_NomDireccion;
        public string Lstr_NomDireccion
        {
            get { return lstr_NomDireccion; }
            set { lstr_NomDireccion = value; }
        }


        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
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

        public clsModificarDireccion(string str_IdDireccion, string str_IdSociedadGL, string str_NomDireccion, string str_Estado, string str_UsrModifica, DateTime dt_FchModifica)
        {
            lstr_IdDireccion = str_IdDireccion;
            lstr_IdSociedadGL = str_IdSociedadGL;
            lstr_NomDireccion = str_NomDireccion;
            lstr_Estado = str_Estado;
            lstr_UsrModifica = str_UsrModifica;
            ldt_FchModifica = dt_FchModifica;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ModificarDireccion.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}