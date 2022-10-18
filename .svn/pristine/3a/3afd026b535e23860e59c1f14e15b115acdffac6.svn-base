using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarReservas : clsProcedimientoAlmacenado
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

        private string lstr_OrderBy;
        public string Lstr_OrderBy
        {
            get { return lstr_OrderBy; }
            set { lstr_OrderBy = value; }
        }


        public clsConsultarReservas(string str_IdReserva, string str_IdEntidadCP, string str_IdSociedadFi, string str_IdMoneda, string str_NomReserva, string str_OrderBy = null)
        {
            lstr_IdReserva = str_IdReserva;
            lstr_IdEntidadCP = str_IdEntidadCP;
            lstr_IdSociedadFi = str_IdSociedadFi;
            lstr_IdMoneda = str_IdMoneda;
            lstr_NomReserva = str_NomReserva;
            lstr_OrderBy = str_OrderBy;

            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarReservas.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}