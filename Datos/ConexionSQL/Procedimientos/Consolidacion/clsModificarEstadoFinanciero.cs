using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Consolidacion
{
    public class clsModificarEstadoFinanciero : clsProcedimientoAlmacenado
    {
        #region variables
        private string lstr_DireccionConfigs = String.Empty;

        private int lint_IdEstadoFinanciero;
        private string lstr_NombreEstadoFinanciero;
        private string lstr_DescripcionEstadoFinanciero;
        private string lstr_Usuario;

        private string lstr_Estado;
        private string lstr_UsrCreacion;
        #endregion

        #region obtencion y asignacion

        public int Lint_IdEstadoFinanciero
        {
            get { return lint_IdEstadoFinanciero; }
            set { lint_IdEstadoFinanciero = value; }
        }

        public string Lstr_NombreEstadoFinanciero
        {
            get { return lstr_NombreEstadoFinanciero; }
            set { lstr_NombreEstadoFinanciero = value; }
        }
        public string Lstr_DescripcionEstadoFinanciero
        {
            get { return lstr_DescripcionEstadoFinanciero; }
            set { lstr_DescripcionEstadoFinanciero = value; }
        }

        public string Lstr_Usuario
        {
            get { return lstr_Usuario; }
            set { lstr_Usuario = value; }
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
        public clsModificarEstadoFinanciero(int int_IdEstadoFinanciero, string str_NombreEstadoFinanciero, string str_DescripcionEstadoFinanciero, string str_Usuario, string str_Estado, string str_UsrCreacion)
        {
            lint_IdEstadoFinanciero = int_IdEstadoFinanciero;
            lstr_NombreEstadoFinanciero = str_NombreEstadoFinanciero;
            lstr_DescripcionEstadoFinanciero = str_DescripcionEstadoFinanciero;
            lstr_Usuario = str_Usuario;

            lstr_Estado = str_Estado;
            lstr_UsrCreacion = str_UsrCreacion;

            var appSettings = ConfigurationManager.AppSettings;
            lstr_DireccionConfigs = appSettings["DireccionConfigs"];

            EjecucionSP(lstr_DireccionConfigs + "\\Consolidacion\\ModificarEstadoFinanciero.config", this);
        }
        #endregion

    }
}