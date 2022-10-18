using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna
{
    public class clsCreaDevengoInteresDE : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private int lint_IdPrestamo;
        private string lstr_IdTramo;
        private DateTime ldt_Anno;
        private decimal ldec_CostoAmortizacionInicial;
        private decimal ldec_Intereses;
        private decimal ldec_Pago;
        private decimal ldec_CostoAmortizacionFinal;
        private decimal ldec_Devengado;
        private string lstr_Estado;
        private string lstr_UsrCreacion;

        #endregion

        #region Obtención y asignación

        public int Lint_IdPrestamo {
            get { return lint_IdPrestamo; }
            set { lint_IdPrestamo = value; }
        }
        public string Lstr_IdTramo{
            get { return lstr_IdTramo; }
            set { lstr_IdTramo = value; }
        }
        public DateTime Ldt_Anno {
            get { return ldt_Anno; }
            set { ldt_Anno = value; }
        }
        public decimal Ldec_CostoAmortizacionInicial {
            get { return ldec_CostoAmortizacionInicial; }
            set { ldec_CostoAmortizacionInicial = value; }
        }
        public decimal Ldec_Intereses{
            get { return ldec_Intereses; }
            set { ldec_Intereses = value; }
        }
        public decimal Ldec_Pago {
            get { return ldec_Pago; }
            set { ldec_Pago = value; }
        }
        public decimal Ldec_CostoAmortizacionFinal
        {
            get { return ldec_CostoAmortizacionFinal; }
            set { ldec_CostoAmortizacionFinal = value; }
        }
        public decimal Ldec_Devengado
        {
            get { return ldec_Devengado; }
            set { ldec_Devengado = value; }
        }
        public string Lstr_Estado {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }
        public string Lstr_UsrCreacion {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }

        #endregion 

        #region Constructor

        public clsCreaDevengoInteresDE(int lint_IdPrestamo, string lstr_IdTramo, DateTime ldt_Anno, decimal ldec_CostoAmortizacionInicial,
            decimal ldec_Intereses, decimal ldec_Pago, decimal ldec_CostoAmortizacionFinal, decimal ldec_Devengado,
            string lstr_Estado, string lstr_UsrCreacion)
        {
            this.lint_IdPrestamo = lint_IdPrestamo;
            this.lstr_IdTramo = lstr_IdTramo;
            this.ldt_Anno = ldt_Anno;
            this.ldec_CostoAmortizacionInicial = ldec_CostoAmortizacionInicial;
            this.ldec_Intereses = ldec_Intereses;
            this.ldec_Pago = ldec_Pago;
            this.ldec_CostoAmortizacionFinal = ldec_CostoAmortizacionFinal;
            this.ldec_Devengado = ldec_Devengado;
            this.lstr_Estado = lstr_Estado;
            this.lstr_UsrCreacion = lstr_UsrCreacion;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaExterna\\CrearDevengoInteresesDE.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}