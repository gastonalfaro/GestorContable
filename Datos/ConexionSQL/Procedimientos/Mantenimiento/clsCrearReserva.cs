using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsCrearReserva : clsProcedimientoAlmacenado
    {
        private string lstr_IdReserva;
        public string Lstr_IdReserva
        {
            get { return lstr_IdReserva; }
            set { lstr_IdReserva = value; }
        }

        private string lstr_IdEntidadCP;
        public string Lstr_IdEntidadCP
        {
            get { return lstr_IdEntidadCP; }
            set { lstr_IdEntidadCP = value; }
        }

        private string lstr_IdSociedadFi;
        public string Lstr_IdSociedadFi
        {
            get { return lstr_IdSociedadFi; }
            set { lstr_IdSociedadFi = value; }
        }

        private string lstr_IdClaseDocPsto;
        public string Lstr_IdClaseDocPsto
        {
            get { return lstr_IdClaseDocPsto; }
            set { lstr_IdClaseDocPsto = value; }
        }

        private string lstr_IdMoneda;
        public string Lstr_IdMoneda
        {
            get { return lstr_IdMoneda; }
            set { lstr_IdMoneda = value; }
        }

        private string lstr_NomReserva;
        public string Lstr_NomReserva
        {
            get { return lstr_NomReserva; }
            set { lstr_NomReserva = value; }
        }


        private string lstr_Estado;
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        private string lstr_Bloqueado;
        public string Lstr_Bloqueado
        {
            get { return lstr_Bloqueado; }
            set { lstr_Bloqueado = value; }
        }

        private string lstr_UsrCreacion;
        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }

        public clsCrearReserva(string str_IdReserva, string str_IdEntidadCP, string str_IdSociedadFi, string str_IdClaseDocPsto, string str_IdMoneda, string str_NomReserva, string str_Estado, string str_Bloqueado, string str_UsrCreacion)
        {
            lstr_IdReserva = str_IdReserva;
            lstr_IdEntidadCP = str_IdEntidadCP;
            lstr_IdSociedadFi = str_IdSociedadFi;
            lstr_IdClaseDocPsto = str_IdClaseDocPsto;
            lstr_IdMoneda = str_IdMoneda;
            lstr_NomReserva = str_NomReserva;
            lstr_Estado = str_Estado;
            lstr_Bloqueado = str_Bloqueado;
            lstr_UsrCreacion = str_UsrCreacion;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\CrearReserva.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}