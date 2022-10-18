using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogicaNegocio.CalculosFinancieros.DeudaExterna
{
    public class clsGirosEstimados
    {                
        #region parametros

        /// <summary>
        /// Declaración e inicialización de variables de Clase GirosEstimados
        /// </summary> 

        private string lstr_IdPrestamo; //número de referencia único para el préstamo
        private int lint_IdTramo; //número del tramo
        private DateTime ldt_FchEstimada; //fecha estimada del giro
        private decimal ldec_Monto; //monto del giro de la moneda del tramo

        #endregion

        #region constructores

        /// <summary>
        /// Constructor de la clase Acreedores, permite crear un giro estimado y almacenarlo en sistema
        /// </summary>
        
        public clsGirosEstimados(){}
        
        /// <summary>
        /// Constructor sobrecargado con información obligatoria de giros estimados
        /// </summary>

        public clsGirosEstimados(string lstr_IdPrestamo, int lint_IdTramo, DateTime ldt_FchEstimada, decimal ldec_Monto)
        {
            this.lstr_IdPrestamo = lstr_IdPrestamo;
            this.lint_IdTramo = lint_IdTramo;
            this.ldt_FchEstimada = ldt_FchEstimada;
            this.ldec_Monto = ldec_Monto;
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

        public int Lint_IdTramo
        {
            get { return lint_IdTramo; }
            set { lint_IdTramo = value; }
        }

        public DateTime Ldt_FchEstimada
        {
            get { return ldt_FchEstimada; }
            set { ldt_FchEstimada = value; }
        }

        public decimal Ldec_Monto
        {
            get { return ldec_Monto; }
            set { ldec_Monto = value; }
        }

        #endregion
    }
}