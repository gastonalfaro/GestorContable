using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Consolidacion
{
    public class clsBuscarEstadoFinanciero : clsProcedimientoAlmacenado
    {
        #region variables
        private string lstr_DireccionConfigs = String.Empty;

        private byte lbt_IdEstadoFinanciero;

        private string lstr_Estado;
        private string lstr_UsrCreacion;
        #endregion

        #region obtencion y asignacion
        public byte Lbt_IdEstadoFinanciero
        {
            get { return lbt_IdEstadoFinanciero; }
            set { lbt_IdEstadoFinanciero = value; }
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
        public clsBuscarEstadoFinanciero(byte bt_IdEstadoFinanciero, string str_Estado, string str_UsrCreacion)
        {
            lbt_IdEstadoFinanciero = bt_IdEstadoFinanciero;
            lstr_Estado = str_Estado;
            lstr_UsrCreacion = str_UsrCreacion;

            var appSettings = ConfigurationManager.AppSettings;
            lstr_DireccionConfigs = appSettings["DireccionConfigs"];

            EjecucionSP(lstr_DireccionConfigs + "\\Consolidacion\\BuscarEstadoFinanciero.config", this);
        }
        #endregion

    }
}