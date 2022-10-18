using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Consolidacion
{
    public class clsBuscarUnidadesTiempoPeriodoEnCursoPorFecha : clsProcedimientoAlmacenado
    {
        #region variables
        private string lstr_DireccionConfigs = String.Empty;

        private DateTime ldt_Fecha;

        private string lstr_Estado;
        private string lstr_UsrCreacion;
        #endregion

        #region obtencion y asignacion
        public DateTime Ldt_Fecha
        {
            get { return ldt_Fecha; }
            set { ldt_Fecha = value; }
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
        public clsBuscarUnidadesTiempoPeriodoEnCursoPorFecha(DateTime dt_Fecha, string str_Estado, string str_UsrCreacion)
        {
            ldt_Fecha = dt_Fecha;
            lstr_Estado = str_Estado;
            lstr_UsrCreacion = str_UsrCreacion;

            var appSettings = ConfigurationManager.AppSettings;
            lstr_DireccionConfigs = appSettings["DireccionConfigs"];

            EjecucionSP(lstr_DireccionConfigs + "\\Consolidacion\\BuscarUnidadesTiempoPeriodoEnCursoPorFecha.config", this);
        }
        #endregion

    }
}