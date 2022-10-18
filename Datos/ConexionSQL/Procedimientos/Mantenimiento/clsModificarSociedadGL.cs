using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsModificarSociedadGL : clsProcedimientoAlmacenado
    {
        private string lstr_IdSociedadGL;
        public string Lstr_IdSociedadGL
        {
            get { return lstr_IdSociedadGL; }
            set { lstr_IdSociedadGL = value; }
        }

        private string lstr_Denominacion;
        public string Lstr_Denominacion
        {
            get { return lstr_Denominacion; }
            set { lstr_Denominacion = value; }
        }

        private string lstr_NomSociedad;
        public string Lstr_NomSociedad
        {
            get { return lstr_NomSociedad; }
            set { lstr_NomSociedad = value; }
        }

        private string lstr_IdPais;
        public string Lstr_IdPais
        {
            get { return lstr_IdPais; }
            set { lstr_IdPais = value; }
        }

        private string lstr_Poblacion;
        public string Lstr_Poblacion
        {
            get { return lstr_Poblacion; }
            set { lstr_Poblacion = value; }
        }

        private string lstr_Calle;
        public string Lstr_Calle
        {
            get { return lstr_Calle; }
            set { lstr_Calle = value; }
        }

        private string lstr_IdMoneda;
        public string Lstr_IdMoneda
        {
            get { return lstr_IdMoneda; }
            set { lstr_IdMoneda = value; }
        }

        private string lstr_IdIdioma;
        public string Lstr_IdIdioma
        {
            get { return lstr_IdIdioma; }
            set { lstr_IdIdioma = value; }
        }

        private string lstr_CorreoNotifica;
        public string Lstr_CorreoNotifica
        {
            get { return lstr_CorreoNotifica; }
            set { lstr_CorreoNotifica = value; }
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

        public clsModificarSociedadGL(string str_IdSociedadGL, string str_Denominacion, string str_NomSociedad, string str_IdPais, string str_Poblacion, string str_Calle, string str_IdMoneda, string str_IdIdioma, string str_CorreoNotifica, string str_Estado, string str_UsrModifica, DateTime dt_FchModifica)
        {
            lstr_IdSociedadGL = str_IdSociedadGL;
            lstr_Denominacion = str_Denominacion;
            lstr_NomSociedad = str_NomSociedad;
            lstr_IdPais = str_IdPais;
            lstr_Poblacion = str_Poblacion;
            lstr_Calle = str_Calle;
            lstr_IdMoneda = str_IdMoneda;
            lstr_IdIdioma = str_IdIdioma;
            lstr_CorreoNotifica = str_CorreoNotifica;
            lstr_Estado = str_Estado;
            lstr_UsrModifica = str_UsrModifica;
            ldt_FchModifica = dt_FchModifica;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ModificarSociedadGL.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}