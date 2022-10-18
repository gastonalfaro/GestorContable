using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogicaNegocio.CalculosFinancieros.DeudaExterna
{
    public class clsPrestamos
    {
        #region parametros

        /// <summary>
        /// Declaración e inicialización de variables de Clase Préstamos
        /// </summary>
        
        private string lstr_IdPrestamo; //número de referencia único para el préstamo
        private string lstr_Fuente; //residencia del acreedor, externa o interna
        private string lstr_Situacion; //significa estado o fase, activo, reembolsado, anulado, etc
        private string lstr_Plazo; //es la suma del periodo de gracia(firma del prestamo y el primer reembolso) y periodo de reembolso
        private string lstr_Nombre; //nombre del préstamo, como lo asigna el acreedor o el deudor
        private DateTime ldt_Firmado; //fecha de firma del préstamo
        private DateTime ldt_LimiteGiro; //fecha de cierre actual para los giros
        private DateTime ldt_LimiteEfectivo; //fecha de cierre para hacer efectivo el préstamo
        private DateTime ldt_Efectivo; //la fecha más temprana en la que se puede ejecutar un giro
        private decimal ldec_Monto; //monto original del préstamo mencionado en el acuerdo del préstamo
        private string lstr_IdMoneda; //moneda en la que el monto del prestamo se expresa
        private string lstr_TpoTramo; //opciones de gestión de tramos, un tramo, multiples tramos conocidos, y desconocidos
        private string lstr_Proposito; //texto con el objetivo descrito del préstamo
        private string lstr_GarantiaPublica; //el prestamo esta garantizado por el gobierno u otra entidad publica
        private string lstr_OrigenDeuda; //clasifica un prestamo de acuerdo al compromiso original
        private int lint_IdAcreedor;
        private int lint_IdDeudor;
        private string lstr_TpoPrestamo; //hay cuatro tipos de prestamo, categoría regular, Transf. Instituciones/Escriturados, devolución UE y Emisión Títulos Valores en Mercado Internacional
        private decimal ldec_Tasa;
        private string lstr_Estado;
        private string lstr_UsrCreacion;

        #endregion

        #region constructores
        /// <summary>
        /// Constructor de la clase Acreedores, permite crear un préstamo y almacenarlo en sistema
        /// </summary>
        
        public clsPrestamos(){}

        /// <summary>
        /// Constructor sobrecargado con información obligatoria de préstamos
        /// </summary>
        
        public clsPrestamos
            (string lstr_IdPrestamo, string lstr_Fuente, string lstr_Situacion, string lstr_Plazo, string lstr_Nombre,
            DateTime ldt_Firmado, DateTime ldt_LimiteGiro, DateTime ldt_LimiteEfectivo, DateTime ldt_Efectivo,
            decimal ldec_Monto, string lstr_IdMoneda, string lstr_TpoTramo, string lstr_Proposito, string lstr_GarantiaPublica,
            string lstr_OrigenDeuda, int lint_IdAcreedor, int lint_IdDeudor, string lstr_TpoPrestamo, decimal ldec_Tasa)
        {
            this.lstr_IdPrestamo = lstr_IdPrestamo;
            this.lstr_Fuente = lstr_Fuente;
            this.lstr_Situacion = lstr_Situacion;
            this.lstr_Plazo = lstr_Plazo;
            this.lstr_Nombre = lstr_Nombre;
            this.ldt_Firmado = ldt_Firmado;
            this.ldt_LimiteGiro = ldt_LimiteGiro;
            this.ldt_LimiteEfectivo = ldt_LimiteEfectivo;
            this.ldt_Efectivo = ldt_Efectivo;
            this.ldec_Monto = ldec_Monto;
            this.lstr_IdMoneda = lstr_IdMoneda;
            this.lstr_TpoTramo = lstr_TpoTramo;
            this.lstr_Proposito = lstr_Proposito;
            this.lstr_GarantiaPublica = lstr_GarantiaPublica;
            this.lstr_OrigenDeuda = lstr_OrigenDeuda;
            this.lint_IdAcreedor = lint_IdAcreedor;
            this.lint_IdDeudor = lint_IdDeudor;
            this.lstr_TpoPrestamo = lstr_TpoPrestamo;
            this.ldec_Tasa = ldec_Tasa;
        }

        #endregion

        #region obtención y asignación

        /// <summary>
        /// Obtención y asignación de datos
        /// </summary>
        
        public string Lstr_IdPrestamo
        {
            get { return lstr_IdPrestamo; }
            set { lstr_IdPrestamo = value; }
        }

        public string Lstr_Fuente
        {
            get { return lstr_Fuente; }
            set { lstr_Fuente = value; }
        }

        public string Lstr_Situacion
        {
            get { return lstr_Situacion; }
            set { lstr_Situacion = value; }
        }

        public string Lstr_Plazo
        {
            get { return lstr_Plazo; }
            set { lstr_Plazo = value; }
        }

        public string Lstr_Nombre
        {
            get { return lstr_Nombre; }
            set { lstr_Nombre = value; }
        }

        public DateTime Ldt_Firmado
        {
            get { return ldt_Firmado; }
            set { ldt_Firmado = value; }
        }

        public DateTime Ldt_LimiteGiro
        {
            get { return ldt_LimiteGiro; }
            set { ldt_LimiteGiro = value; }
        }

        public DateTime Ldt_LimiteEfectivo
        {
            get { return ldt_LimiteEfectivo; }
            set { ldt_LimiteEfectivo = value; }
        }

        public DateTime Ldt_Efectivo
        {
            get { return ldt_Efectivo; }
            set { ldt_Efectivo = value; }
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

        public string Lstr_TpoTramo
        {
            get { return lstr_TpoTramo; }
            set { lstr_TpoTramo = value; }
        }

        public string Lstr_Proposito
        {
            get { return lstr_Proposito; }
            set { lstr_Proposito = value; }
        }

        public string Lstr_GarantiaPublica
        {
            get { return lstr_GarantiaPublica; }
            set { lstr_GarantiaPublica = value; }
        }

        public string Lstr_OrigenDeuda
        {
            get { return lstr_OrigenDeuda; }
            set { lstr_OrigenDeuda = value; }
        }

        public int Lint_IdAcreedor
        {
            get { return lint_IdAcreedor; }
            set { lint_IdAcreedor = value; }
        }

        public int Lint_IdDeudor
        {
            get { return lint_IdDeudor; }
            set { lint_IdDeudor = value; }
        }

        public string Lstr_TpoPrestamo
        {
            get { return lstr_TpoPrestamo; }
            set { lstr_TpoPrestamo = value; }
        }

        public decimal Ldec_Tasa
        {
            get { return ldec_Tasa; }
            set { ldec_Tasa = value; }
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

        #endregion
    }
}