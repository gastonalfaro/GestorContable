using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Consolidacion
{
    public class clsBuscarBitacoraFlujoErroresDTSX : clsProcedimientoAlmacenado
    {
        #region variables
        private string lstr_DireccionConfigs = String.Empty;

        private string lstr_NombreProceso;
        private DateTime ldt_FechaDe;
        private DateTime ldt_FechaHasta;

        private string lstr_Estado; 
        private string lstr_UsrCreacion;
        #endregion

        #region obtencion y asignacion
        public string Lstr_NombreProceso
        {
            get { return lstr_NombreProceso; }
            set { lstr_NombreProceso = value; }
        }

        public DateTime Ldt_FechaDe
        {
            get { return ldt_FechaDe; }
            set { ldt_FechaDe = value; }
        }

        public DateTime Ldt_FechaHasta
        {
            get { return ldt_FechaHasta; }
            set { ldt_FechaHasta = value; }
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
        public clsBuscarBitacoraFlujoErroresDTSX(string str_NombreProceso, DateTime dt_FechaDe, DateTime dt_FechaHasta, string str_Estado, string str_UsrCreacion)
        {
            lstr_NombreProceso = str_NombreProceso;
            ldt_FechaDe = dt_FechaDe;
            ldt_FechaHasta = dt_FechaHasta;
            lstr_Estado = str_Estado;
            lstr_UsrCreacion = str_UsrCreacion;

            var appSettings = ConfigurationManager.AppSettings;
            lstr_DireccionConfigs = appSettings["DireccionConfigs"];

            EjecucionSP(lstr_DireccionConfigs + "\\Consolidacion\\BuscarBitacoraFlujoErroresDTSX.config", this);
        }
        #endregion

    }
}