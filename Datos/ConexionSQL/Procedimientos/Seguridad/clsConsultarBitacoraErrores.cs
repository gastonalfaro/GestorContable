using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Seguridad
{
    public class clsConsultarBitacoraErrores : clsProcedimientoAlmacenado
    {
        private DateTime? lstr_FechaInicio;
        public DateTime? Lstr_FechaInicio
        {
            get { return lstr_FechaInicio; }
            set { lstr_FechaInicio = value; }
        }

        private DateTime? lstr_FechaFinal;
        public DateTime? Lstr_FechaFinal
        {
            get { return lstr_FechaFinal; }
            set { lstr_FechaFinal = value; }
        }

        private string lstr_IdRegistro;
        public string Lstr_IdRegistro
        {
            get { return lstr_IdRegistro; }
            set { lstr_IdRegistro = value; }
        }

        private string lstr_IdUsuario;
        public string Lstr_IdUsuario
        {
            get { return lstr_IdUsuario; }
            set { lstr_IdUsuario = value; }
        }

        private string lstr_IdModulo;
        public string Lstr_IdModulo
        {
            get { return lstr_IdModulo; }
            set { lstr_IdModulo = value; }
        }

        private string lstr_Accion;
        public string Lstr_Accion
        {
            get { return lstr_Accion; }
            set { lstr_Accion = value; }
        }

        public clsConsultarBitacoraErrores(DateTime? str_FechaInicio, DateTime? str_FechaFinal, string str_IdRegistro,
            string str_IdUsuario, string str_IdModulo, string str_Accion)
        {
            lstr_FechaInicio = str_FechaInicio;
            lstr_FechaFinal = str_FechaFinal;
            lstr_IdRegistro = str_IdRegistro;
            lstr_IdUsuario = str_IdUsuario;
            lstr_IdModulo = str_IdModulo;
            lstr_Accion = str_Accion;
            string str_DireccionConfigs = ConfigurationManager.AppSettings["DireccionConfigs"];
            EjecucionSP(str_DireccionConfigs + "\\Seguridad\\ConsultarBitacoraErrores.config", this);
        }

    }
}