using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna
{
    public class clsCreaDesembolso : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private string lstr_IdPrestamo;
        private int lint_IdTramo;
        private decimal ldec_Monto;
        private string lstr_Moneda;
        private DateTime ldt_FchDesembolso;
        private DateTime ldt_FchEstimada;
        private string lstr_TipoDesembolso;
        private string lstr_Descripcion;
        private string lstr_Estado;
        private string lstr_UsrCreacion;
        private int lint_secuencia;

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
        public decimal Ldec_Monto
        {
            get { return ldec_Monto; }
            set { ldec_Monto = value; }
        }
        public string Lstr_Moneda
        {
            get { return lstr_Moneda; }
            set { lstr_Moneda = value; }
        }
        public DateTime Ldt_FchDesembolso
        {
            get { return ldt_FchDesembolso; }
            set { ldt_FchDesembolso = value; }
        }
        public DateTime Ldt_FchEstimada
        {
            get { return ldt_FchEstimada; }
            set { ldt_FchEstimada = value; }
        }
        public string Lstr_TipoDesembolso
        {
            get { return lstr_TipoDesembolso; }
            set { lstr_TipoDesembolso = value; }
        }
        public string Lstr_Descripcion
        {
            get { return lstr_Descripcion; }
            set { lstr_Descripcion = value; }
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

        public clsCreaDesembolso(string lstr_IdPrestamo, int lint_IdTramo, decimal ldec_Monto, string lstr_Moneda,
            DateTime ldt_FchDesembolso, DateTime ldt_FchEstimada, string lstr_TipoDesembolso, string lstr_Descripcion, int lint_secuencia, string lstr_Estado, string lstr_UsrCreacion)
        {
            this.lstr_IdPrestamo = lstr_IdPrestamo;
            this.lint_IdTramo = lint_IdTramo;
            this.ldec_Monto = ldec_Monto;
            this.lstr_Moneda = lstr_Moneda;
            this.ldt_FchDesembolso = ldt_FchDesembolso;
            this.ldt_FchEstimada = ldt_FchEstimada;
            this.lstr_TipoDesembolso = lstr_TipoDesembolso;
            this.lstr_Descripcion = lstr_Descripcion;
            this.lstr_Estado = lstr_Estado;
            this.lstr_UsrCreacion = lstr_UsrCreacion;
            this.lint_secuencia = lint_secuencia;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaExterna\\CrearDesembolso.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}