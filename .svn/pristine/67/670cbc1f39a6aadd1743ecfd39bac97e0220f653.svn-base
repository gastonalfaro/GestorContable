using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna
{
    public class clsEliminarCalculosDevengoDE : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_IdPrestamo;
        private int? lint_IdTramo;
        private DateTime? ldt_Fecha;
        private DateTime? ldt_FechaHasta;
        private string lstr_UsrCreacion;

        #endregion

        #region Obtención y asignación

        public string Lstr_IdPrestamo
        {
            get { return lstr_IdPrestamo; }
            set { lstr_IdPrestamo = value; }
        }
        public int? Lint_IdTramo
        {
            get { return lint_IdTramo; }
            set { lint_IdTramo = value; }
        }
        public DateTime? Ldt_Fecha
        {
            get { return ldt_Fecha; }
            set { ldt_Fecha = value; }
        }
        public DateTime? Ldt_FechaHasta
        {
            get { return ldt_FechaHasta; }
            set { ldt_FechaHasta = value; }
        }
        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }

        #endregion

        #region Constructor

        public clsEliminarCalculosDevengoDE(string lstr_IdPrestamo, int? lint_IdTramo, DateTime? ldt_Fecha = null, DateTime? ldt_FechaHasta = null, string lstr_UsrCreacion = null)
        {
            this.lstr_IdPrestamo = lstr_IdPrestamo;
            this.lint_IdTramo = lint_IdTramo;
            this.ldt_Fecha = ldt_Fecha;
            this.ldt_FechaHasta = ldt_FechaHasta;
            this.lstr_UsrCreacion = lstr_UsrCreacion;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaExterna\\EliminarCalculosDevengoDE.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}