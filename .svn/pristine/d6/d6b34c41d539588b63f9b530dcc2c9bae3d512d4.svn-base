using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogicaNegocio.CalculosFinancieros.DeudaExterna
{
    public class clsTramos
    {
        #region parametros

        /// <summary>
        /// Declaración e inicialización de variables de Clase Tramos
        /// </summary>
        
        private int lint_IdTramo; //número de tramo
        private string lstr_IdPrestamo; //número de referencia único para el préstamo
        private string lstr_TpoAcuerdo; //texto que se refiere a las caracteristicas financieras
        private string lstr_TpoFinanciamiento; //texto que indica el instrumento de crédito utilizado para el acuerdo
        private string lstr_TerminoCredito; //texto que se refiere a la concesionalidad de la financiación
        private string lstr_Reorganizacion; //texto con el foro en el que debe acordarse una reorganización
        private string lstr_TerminoReorganizado; //texto con información complementaria sobre los términos de reorganización del Club de Paris
        private decimal ldec_Monto; //monto del tramo
        private string lstr_IdMoneda; //Moneda base del tramo
        
        #endregion

        #region constructores
        
        /// <summary>
        /// Constructor de la clase Acreedores, permite crear un tramo y almacenarlo en sistema
        /// </summary>
        
        public clsTramos(){}

        /// <summary>
        /// Constructor sobrecargado con información obligatoria de tramos
        /// </summary>
        
        public clsTramos
            (int lint_IdTramo, string lstr_IdPrestamo, string lstr_TpoAcuerdo, string lstr_TpoFinanciamiento, string lstr_TerminoCredito,
            string lstr_Reorganizacion, string lstr_TerminoReorganizado, decimal ldec_Monto, string lstr_IdMoneda)
        {
            this.lint_IdTramo = lint_IdTramo;
            this.lstr_IdPrestamo = lstr_IdPrestamo;
            this.lstr_TpoAcuerdo = lstr_TpoAcuerdo;
            this.lstr_TpoFinanciamiento = lstr_TpoFinanciamiento;
            this.lstr_TerminoCredito = lstr_TerminoCredito;
            this.lstr_Reorganizacion = lstr_Reorganizacion;
            this.lstr_TerminoReorganizado = lstr_TerminoReorganizado;
            this.ldec_Monto = ldec_Monto;
            this.lstr_IdMoneda = lstr_IdMoneda;
        }

        #endregion

        #region obtención y asignación
                
        /// <summary>
        /// Obtención y asignación de datos
        /// </summary>
    
        public int Lint_IdTramo
        {
            get { return lint_IdTramo; }
            set { lint_IdTramo = value; }
        }

        public string Lstr_IdPrestamo
        {
            get { return lstr_IdPrestamo; }
            set { lstr_IdPrestamo = value; }
        }

        public string Lstr_TpoAcuerdo
        {
            get { return lstr_TpoAcuerdo; }
            set { lstr_TpoAcuerdo = value; }
        }

        public string Lstr_TpoFinanciamiento
        {
            get { return lstr_TpoFinanciamiento; }
            set { lstr_TpoFinanciamiento = value; }
        }

        public string Lstr_TerminoCredito
        {
            get { return lstr_TerminoCredito; }
            set { lstr_TerminoCredito = value; }
        }

        public string Lstr_Reorganizacion
        {
            get { return lstr_Reorganizacion; }
            set { lstr_Reorganizacion = value; }
        }

        public string Lstr_TerminoReorganizado
        {
            get { return lstr_TerminoReorganizado; }
            set { lstr_TerminoReorganizado = value; }
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

        #endregion
    }
}