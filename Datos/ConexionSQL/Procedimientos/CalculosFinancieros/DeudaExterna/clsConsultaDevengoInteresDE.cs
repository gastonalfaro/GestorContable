using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna
{
    public class clsConsultaDevengoInteresDE : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lint_IdPrestamo;
        private string lstr_IdTramo;
        private string ldt_Anno;

        #endregion

        #region Obtención y asignación

        public string Lint_IdPrestamo
        {
            get { return lint_IdPrestamo; }
            set { lint_IdPrestamo = value; }
        }
        public string Lstr_IdTramo
        {
            get { return lstr_IdTramo; }
            set { lstr_IdTramo = value; }
        }
        public string Ldt_Anno
        {
            get { return ldt_Anno; }
            set { ldt_Anno = value; }
        }

        #endregion 

        #region Constructor

        public clsConsultaDevengoInteresDE(string lint_IdPrestamo, string lstr_IdTramo, string ldt_Anno)
        {
            this.lint_IdPrestamo = lint_IdPrestamo;
            this.lstr_IdTramo = lstr_IdTramo;
            this.ldt_Anno = ldt_Anno;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaExterna\\ConsultarDevengoInteresesDE.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}