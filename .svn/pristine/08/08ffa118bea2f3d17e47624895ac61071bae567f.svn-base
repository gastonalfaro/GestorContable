using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna
{
    public class clsConsultarCalculosFlujoEfectivoMensualDE : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_IdPrestamo;
        private int? lint_IdTramo;
        private DateTime? ldt_FechaDesde;
        private DateTime? ldt_FechaHasta;

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
        public DateTime? Ldt_FechaDesde
        {
            get { return ldt_FechaDesde; }
            set { ldt_FechaDesde = value; }
        }
        public DateTime? Ldt_FechaHasta
        {
            get { return ldt_FechaHasta; }
            set { ldt_FechaHasta = value; }
        }

        #endregion

        #region Constructor

        public clsConsultarCalculosFlujoEfectivoMensualDE(string lstr_IdPrestamo, int? lint_IdTramo, DateTime? ldt_FechaDesde = null, DateTime? ldt_FechaHasta = null)
        {
            this.lstr_IdPrestamo = lstr_IdPrestamo;
            this.lint_IdTramo = lint_IdTramo;
            this.ldt_FechaDesde = ldt_FechaDesde;
            this.ldt_FechaHasta = ldt_FechaHasta;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaExterna\\ConsultarCalculosFlujoEfectivoMensualDE.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}