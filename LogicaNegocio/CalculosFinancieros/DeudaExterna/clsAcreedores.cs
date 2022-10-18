using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogicaNegocio.CalculosFinancieros.DeudaExterna
{
    public class clsAcreedores
    {
        #region parametros

        /// <summary>
        /// Declaración e inicialización de variables de Clase Acreedores
        /// </summary>
        
        private int lint_Numero; //número único de hasta 6 dígitos
        private string lstr_Nombre; //nombre del acreedor
        private string lstr_Abreviatura; //abreviatura del acreedor
        private string lstr_Contacto; //nombre del contacto principal del acreedor
        private string lstr_Telefono; //número de teléfono con el código de país
        private string lstr_Direccion; //dirección del acreedor
        private string lstr_Pais; //código de 3 dígitos del país
        private string lstr_Tipo; //indica la naturaleza del participante, gobierno local, central, etc
        private string lstr_PaisInstitucion; //

        #endregion

        #region constructores

        /// <summary>
        /// Constructor de la clase Acreedores, permite crear un acreedor y almacenarlo en sistema
        /// </summary>
        
        public clsAcreedores(){}

        /// <summary>
        /// Constructor sobrecargado con información obligatoria de acreedores
        /// </summary>
        
        public clsAcreedores(int lint_Numero, string lstr_Nombre, string lstr_Abreviatura, string lstr_Direccion, string lstr_Pais, string lstr_Tipo, string lstr_PaisInstitucion)
        {
            this.lint_Numero = lint_Numero;
            this.lstr_Nombre = lstr_Nombre;
            this.lstr_Abreviatura = lstr_Abreviatura;
            this.lstr_Direccion = lstr_Direccion;
            this.lstr_Pais = lstr_Pais;
            this.lstr_Tipo = lstr_Tipo;
            this.lstr_PaisInstitucion = lstr_PaisInstitucion;
        }

        #endregion

        #region obtención y asignación

        /// <summary>
        /// Obtención y asignación de datos
        /// </summary>
        
        public int Lint_Numero
        {
            get { return lint_Numero; }
            set { lint_Numero = value; }
        }

        public string Lstr_Nombre
        {
            get { return lstr_Nombre; }
            set { lstr_Nombre = value; }
        }

        public string Lstr_Abreviatura
        {
            get { return lstr_Abreviatura; }
            set { lstr_Abreviatura = value; }
        }

        public string Lstr_Contacto
        {
            get { return lstr_Contacto; }
            set { lstr_Contacto = value; }
        }
        public string Lstr_Telefono
        {
            get { return lstr_Telefono; }
            set { lstr_Telefono = value; }
        }

        public string Lstr_Direccion
        {
            get { return lstr_Direccion; }
            set { lstr_Direccion = value; }
        }

        public string Lstr_Pais
        {
            get { return lstr_Pais; }
            set { lstr_Pais = value; }
        }

        public string Lstr_Tipo
        {
            get { return lstr_Tipo; }
            set { lstr_Tipo = value; }
        }
        public string Lstr_PaisInstitucion
        {
            get { return lstr_PaisInstitucion; }
            set { lstr_PaisInstitucion = value; }
        }

        #endregion
    }
}