using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna
{
    public class clsCreaInteresPunitivoPago : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_IdPrestamo;
        private int lint_IdTramo;
        private DateTime ldt_FchAPartir;
        private int lint_secuencia;
        private decimal ldec_Monto;
        private string lstr_IdMoneda;
        private string lstr_Estado;
        private string lstr_UsrCreacion;

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
        public DateTime Ldt_FchAPartir
        {
            get { return ldt_FchAPartir; }
            set { ldt_FchAPartir = value; }
        }
        public decimal Ldec_Monto
        {
            get { return ldec_Monto; }
            set { ldec_Monto = value; }
        }
        public string Lstr_IdMoneda
        {
            get { return lstr_IdMoneda; }
            set { lstr_IdMoneda = value; }
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
        public int Lint_secuencia
        {
            get { return lint_secuencia; }
            set { lint_secuencia = value; }
        }

        #endregion 

        #region Constructor

        public clsCreaInteresPunitivoPago(string lstr_IdPrestamo, int lint_IdTramo, DateTime ldt_FchAPartir,
            decimal ldec_Monto, string lstr_IdMoneda, int lint_secuencia,
            string lstr_Estado, string lstr_UsrCreacion)
        {
            this.lstr_IdPrestamo = lstr_IdPrestamo;
            this.lint_IdTramo = lint_IdTramo;
            this.ldt_FchAPartir = ldt_FchAPartir;
            this.ldec_Monto = ldec_Monto;
            this.lstr_IdMoneda = lstr_IdMoneda;
            this.lint_secuencia = lint_secuencia;
            this.lstr_Estado = lstr_Estado;
            this.lstr_UsrCreacion = lstr_UsrCreacion;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaExterna\\CrearInteresPunitivoPago.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}