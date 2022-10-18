using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Consolidacion
{
    public class clsRPTEntidadesEntregaEstadoFinancieroTarde : clsProcedimientoAlmacenado
    {
        #region variables
        private string lstr_DireccionConfigs = String.Empty;

        private DateTime ldt_FechaInicio;
        private DateTime ldt_FechaFin;
        private string lstr_IdEntidad;

        private string lstr_Estado;
        private string lstr_UsrCreacion;
        #endregion

        #region obtencion y asignacion
        public DateTime Ldt_FechaInicio
        {
            get { return ldt_FechaInicio; }
            set { ldt_FechaInicio = value; }
        }
        public DateTime Ldt_FechaFin
        {
            get { return ldt_FechaFin; }
            set { ldt_FechaFin = value; }
        }

        public string Lstr_IdEntidad
        {
            get { return lstr_IdEntidad; }
            set { lstr_IdEntidad = value; }
        }


        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }

        #endregion

        #region procedimientos
        public clsRPTEntidadesEntregaEstadoFinancieroTarde(DateTime dt_FechaInicio, DateTime dt_FechaFin, string str_IdEntidad, string str_Estado, string str_UsrCreacion)
        {
            ldt_FechaInicio = dt_FechaInicio;
            ldt_FechaFin = dt_FechaFin;
            lstr_IdEntidad = str_IdEntidad;

            lstr_Estado = str_Estado;
            lstr_UsrCreacion = str_UsrCreacion;

            var appSettings = ConfigurationManager.AppSettings;
            lstr_DireccionConfigs = appSettings["DireccionConfigs"];

            EjecucionSP(lstr_DireccionConfigs + "\\Consolidacion\\RPTEntidadesEntregaEstadoFinancieroTarde.config", this);
        }
        #endregion

    }
}