using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogicaNegocio.CalculosFinancieros.DeudaExterna
{
    public class clsComisiones
    {
        #region parametros

        /// <summary>
        /// Declaración e inicialización de variables de Clase Comisiones
        /// </summary>
        
        private	string lstr_IdPrestamo;
        private	int	lint_IdTramo;
        private	int	lint_IdComision;
        private	string lstr_TpoComision;
        private	DateTime ldt_FchEfectivoAPartir;
        private	DateTime ldt_FchHasta;
        private	string lstr_MonedaPago;
        private	decimal ldec_Porcentaje;
        private	decimal ldec_MontoPago;
        private	string lstr_MetodoPago;
        private	DateTime ldt_FchPrimerPago;
        private	DateTime ldt_FchUltimoPago;
        private	string lstr_Periodo;
        private	decimal ldec_Ano;
        private	decimal ldec_Mes;
        private	string lstr_TpoPago;


        #endregion

        #region constructores
        /// <summary>
        /// Constructor de la clase Comisiones, permite crear un Comisiones y almacenarlo en sistema
        /// </summary>
        
        public clsComisiones(){}

        /// <summary>
        /// Constructor sobrecargado con información obligatoria de Comisiones
        /// </summary>

        public clsComisiones(string lstr_IdPrestamo, int lint_IdTramo, int lint_IdComision, string lstr_TpoComision, DateTime ldt_FchEfectivoAPartir,
                             DateTime ldt_FchHasta, string lstr_MonedaPago, decimal ldec_Porcentaje, decimal ldec_MontoPago, string lstr_MetodoPago,
                             DateTime ldt_FchPrimerPago, DateTime ldt_FchUltimoPago, string lstr_Periodo, decimal ldec_Ano, decimal ldec_Mes, string lstr_TpoPago)
        {
            this.lstr_IdPrestamo = lstr_IdPrestamo;
            this.lint_IdTramo = lint_IdTramo;
            this.lint_IdComision = lint_IdComision;
            this.lstr_TpoComision = lstr_TpoComision;
            this.ldt_FchEfectivoAPartir = ldt_FchEfectivoAPartir;
            this.ldt_FchHasta = ldt_FchHasta;
            this.lstr_MonedaPago = lstr_MonedaPago;
            this.ldec_Porcentaje = ldec_Porcentaje;
            this.ldec_MontoPago = ldec_MontoPago;
            this.lstr_MetodoPago = lstr_MetodoPago;
            this.ldt_FchPrimerPago = ldt_FchPrimerPago;
            this.ldt_FchUltimoPago = ldt_FchUltimoPago;
            this.lstr_Periodo = lstr_Periodo;
            this.ldec_Ano = ldec_Ano;
            this.ldec_Mes = ldec_Mes;
            this.lstr_TpoPago = lstr_TpoPago;
        }

        #endregion

        #region obtención y asignación

        /// <summary>
        /// Obtención y asignación de datos
        /// </summary>
        
        //public string Lstr_IdPrestamo
        //{
        //    get { return lstr_IdPrestamo; }
        //    set { lstr_IdPrestamo = value; }
        //}

        //public string Lstr_Fuente
        //{
        //    get { return lstr_Fuente; }
        //    set { lstr_Fuente = value; }
        //}

        //public string Lstr_Situacion
        //{
        //    get { return lstr_Situacion; }
        //    set { lstr_Situacion = value; }
        //}

        //public string Lstr_Plazo
        //{
        //    get { return lstr_Plazo; }
        //    set { lstr_Plazo = value; }
        //}

        //public string Lstr_Nombre
        //{
        //    get { return lstr_Nombre; }
        //    set { lstr_Nombre = value; }
        //}

        //public DateTime Ldt_Firmado
        //{
        //    get { return ldt_Firmado; }
        //    set { ldt_Firmado = value; }
        //}

        //public DateTime Ldt_LimiteGiro
        //{
        //    get { return ldt_LimiteGiro; }
        //    set { ldt_LimiteGiro = value; }
        //}

        //public DateTime Ldt_LimiteEfectivo
        //{
        //    get { return ldt_LimiteEfectivo; }
        //    set { ldt_LimiteEfectivo = value; }
        //}

        //public DateTime Ldt_Efectivo
        //{
        //    get { return ldt_Efectivo; }
        //    set { ldt_Efectivo = value; }
        //}

        //public decimal Ldec_Monto
        //{
        //    get { return ldec_Monto; }
        //    set { ldec_Monto = value; }
        //}

        //public string Lstr_IdMoneda
        //{
        //    get { return lstr_IdMoneda; }
        //    set { lstr_IdMoneda = value; }
        //}

        //public string Lstr_TpoTramo
        //{
        //    get { return lstr_TpoTramo; }
        //    set { lstr_TpoTramo = value; }
        //}

        //public string Lstr_Proposito
        //{
        //    get { return lstr_Proposito; }
        //    set { lstr_Proposito = value; }
        //}

        //public string Lstr_GarantiaPublica
        //{
        //    get { return lstr_GarantiaPublica; }
        //    set { lstr_GarantiaPublica = value; }
        //}

        //public string Lstr_OrigenDeuda
        //{
        //    get { return lstr_OrigenDeuda; }
        //    set { lstr_OrigenDeuda = value; }
        //}

        //public int Lint_IdAcreedor
        //{
        //    get { return lint_IdAcreedor; }
        //    set { lint_IdAcreedor = value; }
        //}

        //public int Lint_IdDeudor
        //{
        //    get { return lint_IdDeudor; }
        //    set { lint_IdDeudor = value; }
        //}

        //public string Lstr_TpoPrestamo
        //{
        //    get { return lstr_TpoPrestamo; }
        //    set { lstr_TpoPrestamo = value; }
        //}

        //public decimal Ldec_Tasa
        //{
        //    get { return ldec_Tasa; }
        //    set { ldec_Tasa = value; }
        //}
        #endregion
    }
}