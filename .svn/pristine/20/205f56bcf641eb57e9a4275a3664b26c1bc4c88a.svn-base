using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Datos.ConexionSQL;
using Datos.ConexionSQL.Procedimientos;

namespace LogicaNegocio.Ejemplo
{
    public class tEjemplo
    {
        #region Atributos y Propiedades

        /// <summary>
        /// Atributo Dato1 de Ejemplo
        /// </summary>
        private int gint_Dato1 = 0;
        public int Dato1
        {
            get { return gint_Dato1; }
            set { gint_Dato1 = value; }
        }

        /// <summary>
        /// Atributo Dato2 de Ejemplo
        /// </summary>
        private int gint_Dato2 = 0;
        public int Dato2
        {
            get { return gint_Dato2; }
            set { gint_Dato2 = value; }
        }
        #endregion
        #region Metodos

        /// <summary>
        /// Divide el dato1 con el dato2
        /// </summary>
        /// <returns>Cociente de la division</returns>
        public int Division()
        {
            //Resultado de division
            int lint_resultado = 0;
            try
            {
                lint_resultado = gint_Dato1 / gint_Dato2;
            }
            catch (Exception ex)
            {
                //throw new Exception("alerta",ex;
            }
            return lint_resultado;

        }

        public void LlamarSP()
        {
            //ConsultarRol lccr_Procedimiento = new ConsultarRol("D:\\PwC\\Prop\\Ejemplo.config");
        }

        #region Constructores
        public tEjemplo()
        { }

        public tEjemplo(int int_Dato1, int int_Dato2)
        {
            gint_Dato1 = int_Dato1;
            gint_Dato2 = int_Dato2;
        }
        #endregion

        #endregion

        #region Validaciones
        /// <summary>
        /// Verifica si el divisor es cero
        /// </summary>
        /// <returns>Indica si el divisor es cero o no</returns>
        private bool DivisorEsCero ()
        {
            bool boo_Resultado = false;
            if (gint_Dato2 == 0)
            {
                boo_Resultado = true;
            }
            return boo_Resultado;
        }
        #endregion


    }
}