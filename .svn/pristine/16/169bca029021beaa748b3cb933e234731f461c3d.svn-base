using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogicaNegocio.CalculosFinancieros.DeudaExterna
{
    public class clsTasasFlotantes
    {
        #region parametros
        
        /// <summary>
        /// Declaración e inicialización de variables de Clase TasasFlotantes
        /// </summary>
        
        private string lstr_IdPrestamo;
        private int lint_IdTramo;
        private DateTime ldt_APartir;
        private decimal ldec_Tasa;

        #endregion

        #region constructores

        /// <summary>
        /// 
        /// </summary>
        
        public clsTasasFlotantes(){}

        /// <summary>
        /// 
        /// </summary>

        public clsTasasFlotantes(string lstr_IdPrestamo, int lint_IdTramo, DateTime ldt_APartir, decimal ldec_Tasa)
        {
            this.lstr_IdPrestamo = lstr_IdPrestamo;
            this.lint_IdTramo = lint_IdTramo;
            this.ldt_APartir = ldt_APartir;
            this.ldec_Tasa = ldec_Tasa;
        }

        #endregion

        #region obtención y asignación

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

        public DateTime Ldt_APartir
        {
            get { return ldt_APartir; }
            set { ldt_APartir = value; }
        }

        public decimal Ldec_Tasa
        {
            get { return ldec_Tasa; }
            set { ldec_Tasa = value; }
        }

        #endregion
    }
}