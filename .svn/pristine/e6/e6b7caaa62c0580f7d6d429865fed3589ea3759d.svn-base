using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna
{
    public class clsConsultarPagosDE : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_IdPrestamo;
        private int? lint_IdTramo;
        private DateTime? ldt_FchDesde;
        private DateTime? ldt_FchHasta;

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
        public DateTime? Ldt_FchDesde
        {
            get { return ldt_FchDesde; }
            set { ldt_FchDesde = value; }
        }

        public DateTime? Ldt_FchHasta
        {
            get { return ldt_FchHasta; }
            set { ldt_FchHasta = value; }
        }

        #endregion

        #region Constructor

        public clsConsultarPagosDE(string lstr_IdPrestamo, int? lint_IdTramo, DateTime? ldt_FchDesde,  DateTime? ldt_FchHasta)
        {
            this.lstr_IdPrestamo = lstr_IdPrestamo;
            this.lint_IdTramo = lint_IdTramo;
            this.ldt_FchDesde = ldt_FchDesde;
            this.ldt_FchHasta = ldt_FchHasta;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaExterna\\ConsultarPagosDE.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}