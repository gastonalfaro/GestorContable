using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaInterna
{
    public class clsCreaCancelacion : clsProcedimientoAlmacenado
    {
        #region Parámetros

        private int lint_NumValor;
        private decimal ldec_ValorFacial;
        private decimal ldec_ValorTransadoBruto;
        private decimal ldec_RendimientoPorDescuento;
        private DateTime ldt_FchColocacion;
        private DateTime ldt_FchCancelacion;
        private string lstr_Nemotecnico;
        private string lstr_Moneda;
        private string lstr_Propiedad;
        private decimal ldec_TasaBruta;
        private decimal ldec_Margen;
        private decimal ldec_ValorTransadoNeto;
        private decimal ldec_TasaNeta;
        private decimal ldec_Premio;
        private string lstr_Estado;
        private string lstr_UsrCreacion;


        #endregion

        #region Obtención y asignación

        public int Lint_NumValor {
            get { return lint_NumValor; }
            set { lint_NumValor = value; }
        }
        public string Lstr_Nemotecnico {
            get { return lstr_Nemotecnico; }
            set { lstr_Nemotecnico = value; }
        }
        public string Lstr_Moneda {
            get { return lstr_Moneda; }
            set { lstr_Moneda = value; }
        }
        public decimal Ldec_ValorFacial {
            get { return ldec_ValorFacial; }
            set { ldec_ValorFacial = value; }
        }
        public DateTime Ldt_FchColocacion {
            get { return ldt_FchColocacion; }
            set { ldt_FchColocacion = value; }
        }
        public DateTime Ldt_FchCancelacion {
            get { return ldt_FchCancelacion; }
            set { ldt_FchCancelacion = value; }
        }
        public decimal Ldec_Margen {
            get { return ldec_Margen; }
            set { ldec_Margen = value; }
        }
        public decimal Ldec_ValorTransadoBruto {
            get { return ldec_ValorTransadoBruto; }
            set { ldec_ValorTransadoBruto = value; }
        }
        public decimal Ldec_ValorTransadoNeto {
            get { return ldec_ValorTransadoNeto; }
            set { ldec_ValorTransadoNeto = value; }
        }
        public decimal Ldec_TasaBruta {
            get { return ldec_TasaBruta; }
            set { ldec_TasaBruta = value; }
        }
        public decimal Ldec_TasaNeta {
            get { return ldec_TasaNeta; }
            set { ldec_TasaNeta = value; }
        }
        public decimal Ldec_RendimientoPorDescuento {
            get { return ldec_RendimientoPorDescuento; }
            set { ldec_RendimientoPorDescuento = value; }
        }
        public decimal Ldec_Premio {
            get { return ldec_Premio; }
            set { ldec_Premio = value; }
        }
        public string Lstr_Propiedad {
            get { return lstr_Propiedad; }
            set { lstr_Propiedad = value; }
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

        public clsCreaCancelacion(int lint_NumValor, decimal ldec_ValorFacial, decimal ldec_ValorTransadoBruto, decimal ldec_RendimientoPorDescuento, DateTime ldt_FchColocacion,
            DateTime ldt_FchCancelacion, string lstr_Nemotecnico, string lstr_Moneda, string lstr_Propiedad, decimal ldec_TasaBruta, decimal ldec_Margen, decimal ldec_ValorTransadoNeto,
            decimal ldec_TasaNeta, decimal ldec_Premio, string lstr_Estado, string lstr_UsrCreacion)
        {
            this.lint_NumValor = lint_NumValor;
            this.lstr_Nemotecnico = lstr_Nemotecnico;
            this.lstr_Moneda = lstr_Moneda;
            this.ldec_ValorFacial = ldec_ValorFacial;
            this.ldt_FchColocacion = ldt_FchColocacion;
            this.ldt_FchCancelacion = ldt_FchCancelacion;
            this.ldec_ValorTransadoBruto = ldec_ValorTransadoBruto;
            this.ldec_ValorTransadoNeto = ldec_ValorTransadoNeto;
            this.ldec_TasaBruta = ldec_TasaBruta;
            this.ldec_TasaNeta = ldec_TasaNeta;
            this.ldec_RendimientoPorDescuento = ldec_RendimientoPorDescuento;
            this.ldec_Margen = ldec_Margen;
            this.lstr_Propiedad = lstr_Propiedad;
            this.ldec_Premio = ldec_Premio;
            this.lstr_Estado = lstr_Estado;
            this.lstr_UsrCreacion = lstr_UsrCreacion;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                string str_DireccionConfigs = appSettings["DireccionConfigs"];

                EjecucionSP(str_DireccionConfigs + "CalculosFinancieros\\DeudaInterna\\CrearCancelacion.config", this);
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = ex.ToString();
            }
        }

        #endregion
    }
}