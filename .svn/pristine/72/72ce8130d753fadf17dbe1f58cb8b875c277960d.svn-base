using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
using Datos.ConexionSQL;

namespace LogicaNegocio.CalculosFinancieros.DeudaExterna
{
    public class clsFuncionesDeudaExterna
    {
        #region Administración de préstamos

        clsPrestamos lcls_prestamo;

        /// <summary>
        /// Genera el préstamo en memoria para trabajar sobre el antes de almacenarlo en base de datos
        /// </summary>
        /// <param name="larr_InfoPrestamo">Arreglo que contiene toda la información del préstamo proveniente del webservice</param>
        public void crearPrestamo(ArrayList larr_InfoPrestamo)
        {
            lcls_prestamo = new clsPrestamos();
            lcls_prestamo.Lstr_IdPrestamo = Convert.ToString(larr_InfoPrestamo[0]);
            lcls_prestamo.Lstr_Fuente = Convert.ToString(larr_InfoPrestamo[1]);
            lcls_prestamo.Lstr_Situacion = Convert.ToString(larr_InfoPrestamo[2]);
            lcls_prestamo.Lstr_Plazo = Convert.ToString(larr_InfoPrestamo[3]);
            lcls_prestamo.Lstr_Nombre = Convert.ToString(larr_InfoPrestamo[4]);
            lcls_prestamo.Ldt_Firmado = Convert.ToDateTime(larr_InfoPrestamo[5]);
            lcls_prestamo.Ldt_LimiteGiro = Convert.ToDateTime(larr_InfoPrestamo[6]);
            lcls_prestamo.Ldt_LimiteEfectiva = Convert.ToDateTime(larr_InfoPrestamo[7]);
            lcls_prestamo.Ldt_Efectivo = Convert.ToDateTime(larr_InfoPrestamo[8]);
            lcls_prestamo.Ldec_Monto = Convert.ToDecimal(larr_InfoPrestamo[9]);
            lcls_prestamo.Lstr_Moneda = Convert.ToString(larr_InfoPrestamo[10]);
            lcls_prestamo.Lstr_TipoTramo = Convert.ToString(larr_InfoPrestamo[11]);
            lcls_prestamo.Lstr_Proposito = Convert.ToString(larr_InfoPrestamo[12]);
            lcls_prestamo.Lstr_GarantiaPublica = Convert.ToString(larr_InfoPrestamo[13]);
            lcls_prestamo.Lstr_OrigenDeuda = Convert.ToString(larr_InfoPrestamo[14]);
            lcls_prestamo.Lint_IdAcreedor = Convert.ToInt32(larr_InfoPrestamo[15]);
            lcls_prestamo.Lint_IdDeudor = Convert.ToInt32(larr_InfoPrestamo[16]);
            lcls_prestamo.Lstr_TipoPrestamo = Convert.ToString(larr_InfoPrestamo[17]);
            lcls_prestamo.Ldec_Tasa = Convert.ToDecimal(larr_InfoPrestamo[18]);
        }

        /// <summary>
        /// Almacena todo el trabajo generado en el objeto Prestamo, en la base de datos JOSE
        /// </summary>
        /// <param name="lstr_UsrCreacion">Usuario que genera la acción de almacenamiento en la base de datos</param>
        /// <returns>Retorna un número resultado de la transacción realizada</returns>
<<<<<<< .mine
        /*public string guardarPrestamoDB(string lstr_UsrCreacion)
        {
            string lstr_ResultadoRegistroDB = "";
            clsManejoPrestamos lcls_RegistraPrestamo = new clsManejoPrestamos();
=======
        //public string guardarPrestamoDB(string lstr_UsrCreacion)
        //{
        //    string lstr_ResultadoRegistroDB = "";
        //    clsManejoPrestamos lcls_RegistraPrestamo = new clsManejoPrestamos();
>>>>>>> .r56

        //    lstr_ResultadoRegistroDB = lcls_RegistraPrestamo.registrarPrestamos(lcls_prestamo.Lstr_IdPrestamo, lcls_prestamo.Lstr_Fuente, lcls_prestamo.Lstr_Situacion, lcls_prestamo.Lstr_Plazo,
        //        lcls_prestamo.Lstr_Nombre, lcls_prestamo.Ldt_Firmado, lcls_prestamo.Ldt_LimiteGiro, lcls_prestamo.Ldt_LimiteEfectiva, lcls_prestamo.Ldt_Efectivo,
        //        lcls_prestamo.Ldec_Monto, lcls_prestamo.Lstr_Moneda, lcls_prestamo.Lstr_TipoTramo, lcls_prestamo.Lstr_Proposito, lcls_prestamo.Lstr_GarantiaPublica,
        //        lcls_prestamo.Lstr_OrigenDeuda, lcls_prestamo.Lint_IdAcreedor, lcls_prestamo.Lint_IdDeudor, lcls_prestamo.Lstr_TipoPrestamo, lcls_prestamo.Ldec_Tasa, lstr_UsrCreacion);
            
<<<<<<< .mine
            return lstr_ResultadoRegistroDB;
        }*/
=======
        //    return lstr_ResultadoRegistroDB;
        //}
>>>>>>> .r56

        /// <summary>
        /// Elimina un préstamo de la base de datos (inhabilita el registro, no lo elimina físicamente)
        /// </summary>
        /// <param name="lstr_IdPrestamo">Identificador del préstamo que se requiere inhabilitar</param>
        /// <param name="lstr_Estado">Nuevo estado del préstamo (inhabilitado)</param>
        /// <param name="lstr_UsrModifica">Usuario que genera la eliminación del préstamo</param>
        /// <param name="ldt_FchModifica">Fecha en la que se elimina el prestamo</param>
        /// <returns>Retorna un número resultado de la transacción realizada</returns>
       /* public string eliminaPrestamoDB(string lstr_IdPrestamo, string lstr_Estado, string lstr_UsrModifica, DateTime ldt_FchModifica)
        {
            string lstr_ResultadoEliminacionDB = "";
            clsManejoPrestamos lcls_EliminaPrestamo = new clsManejoPrestamos();
            lstr_ResultadoEliminacionDB = lcls_EliminaPrestamo.eliminarPrestamos(lstr_IdPrestamo, lstr_Estado, lstr_UsrModifica, ldt_FchModifica);
            return lstr_ResultadoEliminacionDB;
        }*/

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lint_Opcion"></param>
        /// <param name="lstr_Valor1"></param>
        /// <param name="lstr_Valor2"></param>
        /// <returns></returns>
      /*  public DataSet consultaPrestamoDB(int lint_Opcion, string lstr_Valor1, string lstr_Valor2)
        {
            DataSet ldta_ResultadoConsultaDB = new DataSet();
            clsManejoPrestamos lcls_consultaPrestamo = new clsManejoPrestamos();
            ldta_ResultadoConsultaDB = lcls_consultaPrestamo.consultarPrestamos(lint_Opcion, lstr_Valor1, lstr_Valor2);
            return ldta_ResultadoConsultaDB;
        }*/


       /* public string modificaPrestamoDB(ArrayList larr_InfoPrestamo)
        {
            string lstr_ResultadoModificacionDB = "";
            clsManejoPrestamos lcls_ModificaPrestamo = new clsManejoPrestamos();

            return lstr_ResultadoModificacionDB;
        }*/

        #endregion
    }
}