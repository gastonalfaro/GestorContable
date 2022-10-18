using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna
{
    public class clsCambiaEstadoInteres : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_IdPrestamo;
        private int lint_IdTramo;
        private string lstr_Estado;
        private string lstr_UsrModifica;
        private DateTime ldt_FchModifica;
        private DateTime ldt_FchPagoAPartir;
        private DateTime ldt_FchTasaAPartir;
        private int lint_Secuencia;

        #endregion

        #region Obtención y asignación

        public string Lstr_IdPrestamo
        {
            get { return lstr_IdPrestamo; }
            set { lstr_IdPrestamo = value; }
        }
        public int Lint_IdTramo
        {
            get { return lint_IdTramo; }
            set { lint_IdTramo = value; }
        }
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }
        public string Lstr_UsrModifica
        {
            get { return lstr_UsrModifica; }
            set { lstr_UsrModifica = value; }
        }
        public DateTime Ldt_FchModifica
        {
            get { return ldt_FchModifica; }
            set { ldt_FchModifica = value; }
        }
        public int Lint_Secuencia
        {
            get { return lint_Secuencia; }
            set { lint_Secuencia = value; }
        }
        public DateTime Ldt_FchPagoAPartir
        {
            get { return ldt_FchPagoAPartir; }
            set { ldt_FchPagoAPartir = value; }
        }
        public DateTime Ldt_FchTasaAPartir
        {
            get { return ldt_FchTasaAPartir; }
            set { ldt_FchTasaAPartir = value; }
        }


        #endregion

        #region Constructor

        public clsCambiaEstadoInteres(string lstr_IdPrestamo, int lint_IdTramo, DateTime ldt_FchPagoAPartir, DateTime ldt_FchTasaAPartir, int lint_Secuencia, string lstr_Estado, string lstr_UsrModifica, DateTime ldt_FchModifica)
        {
            this.lstr_IdPrestamo = lstr_IdPrestamo;
            this.lint_IdTramo = lint_IdTramo;
            this.lint_Secuencia = lint_Secuencia;
            this.ldt_FchPagoAPartir = ldt_FchPagoAPartir;
            this.ldt_FchTasaAPartir = ldt_FchTasaAPartir;
            this.lstr_Estado = lstr_Estado;
            this.lstr_UsrModifica = lstr_UsrModifica;
            this.ldt_FchModifica = ldt_FchModifica;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaExterna\\EstadoInteres.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}