using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Mantenimiento
{
    public class clsConsultarBancos : clsProcedimientoAlmacenado
    {
    //@IdPrestamo varchar(15),
    //@Fuente varchar(2),	
    //@Situacion varchar(20),
    //@Plazo varchar(2),
    //@Nombre varchar(30),
    //@FechaInicio date,
    //@FechaFin date,
    //@IdAcreedor int,
    //@IdDeudor int,
    //@TipoPrestamo varchar(20),

        private string lstr_IdBanco;
        public string Lstr_IdBanco
        {
            get { return lstr_IdBanco; }
            set { lstr_IdBanco = value; }
        }

        private string lstr_IdBancoPropio;
        public string Lstr_IdBancoPropio
        {
            get { return lstr_IdBancoPropio; }
            set { lstr_IdBancoPropio = value; }
        }

        private string lstr_IdSociedadFi;
        public string Lstr_IdSociedadFi
        {
            get { return lstr_IdSociedadFi; }
            set { lstr_IdSociedadFi = value; }
        }

        private string lstr_NomBanco;
        public string Lstr_NomBanco
        {
            get { return lstr_NomBanco; }
            set { lstr_NomBanco = value; }
        }


        public clsConsultarBancos(string str_IdBanco, string str_IdBancoPropio, string str_IdSociedadFi, string str_NomBanco)
        {
            lstr_IdBanco = str_IdBanco;
            lstr_IdBancoPropio = str_IdBancoPropio;
            lstr_IdSociedadFi = str_IdSociedadFi;
            lstr_NomBanco = str_NomBanco;
            
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "\\Mantenimiento\\ConsultarBancos.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }
    }
}