using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Seguridad
{
    public class clsConsultarBitacoraAsientos : clsProcedimientoAlmacenado
    {
        private string lstr_FechaInicio;
        public string Lstr_FechaInicio
        {
            get { return lstr_FechaInicio; }
            set { lstr_FechaInicio = value; }
        }

        private string lstr_FechaFinal;
        public string Lstr_FechaFinal
        {
            get { return lstr_FechaFinal; }
            set { lstr_FechaFinal = value; }
        }

        private string lstr_IdOperacion;
        public string Lstr_IdOperacion
        {
            get { return lstr_IdOperacion; }
            set { lstr_IdOperacion = value; }
        }

        private string lstr_IdSociedadGL;
        public string Lstr_IdSociedadGL
        {
            get { return lstr_IdSociedadGL; }
            set { lstr_IdSociedadGL = value; }
        }

        private string lstr_IdTransaccion;
        public string Lstr_IdTransaccion
        {
            get { return lstr_IdTransaccion; }
            set { lstr_IdTransaccion = value; }
        }

        private string lstr_IdModulo;
        public string Lstr_IdModulo
        {
            get { return lstr_IdModulo; }
            set { lstr_IdModulo = value; }
        }

        public clsConsultarBitacoraAsientos(string str_FechaInicio, string str_FechaFinal,
            string str_IdOperacion, string str_IdSociedadGL, string str_IdTransaccion, string str_IdModulo)
        {
            lstr_FechaInicio = str_FechaInicio;
            lstr_FechaFinal = str_FechaFinal;
            lstr_IdOperacion = str_IdOperacion;
            lstr_IdSociedadGL = str_IdSociedadGL;
            lstr_IdTransaccion = str_IdTransaccion;
            lstr_IdModulo = str_IdModulo;
            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\Seguridad\\ConsultarBitacoraAsientos.config", this);
        }

    }
}