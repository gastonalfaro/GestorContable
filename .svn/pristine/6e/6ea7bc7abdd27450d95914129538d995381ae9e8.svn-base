using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
using Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna;

namespace LogicaNegocio.CalculosFinancieros.DeudaExterna
{
    public class clsManejoPrestamos : clsSP_Prestamos
    {
        #region Administración de préstamos

        clsPrestamos lcls_prestamo;

        /// <summary>
        /// Almacena todo el trabajo generado en el objeto Prestamo, en la base de datos
        /// </summary>
        /// <param name="lstr_UsrCreacion">Usuario que genera la acción de almacenamiento en la base de datos</param>
        /// <returns>Retorna un número resultado de la transacción realizada</returns>
        public string registraPrestamoDB(ArrayList larr_InfoPrestamo)
        {
            string lstr_ResultadoRegistroDB = "";
            clsSP_Prestamos lcls_RegistraPrestamo = new clsSP_Prestamos();
            lstr_ResultadoRegistroDB = lcls_RegistraPrestamo.registrarPrestamos(larr_InfoPrestamo);
            return lstr_ResultadoRegistroDB;
        }

        /// <summary>
        /// Elimina un préstamo de la base de datos (inhabilita el registro, no lo elimina físicamente)
        /// </summary>
        /// <param name="lstr_IdPrestamo">Identificador del préstamo que se requiere inhabilitar</param>
        /// <param name="lstr_Estado">Nuevo estado del préstamo (inhabilitado)</param>
        /// <param name="lstr_UsrModifica">Usuario que genera la eliminación del préstamo</param>
        /// <param name="ldt_FchModifica">Fecha en la que se elimina el prestamo</param>
        /// <returns>Retorna un número resultado de la transacción realizada</returns>
        public string eliminaPrestamoDB(string lstr_IdPrestamo, string lstr_Estado, string lstr_UsrModifica, DateTime ldt_FchModifica)
        {
            string lstr_ResultadoEliminacionDB = "";
            clsSP_Prestamos lcls_EliminaPrestamo = new clsSP_Prestamos();
            lstr_ResultadoEliminacionDB = lcls_EliminaPrestamo.eliminarPrestamos(lstr_IdPrestamo, lstr_Estado, lstr_UsrModifica, ldt_FchModifica);
            return lstr_ResultadoEliminacionDB;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lint_Opcion"></param>
        /// <param name="lstr_Valor1"></param>
        /// <param name="lstr_Valor2"></param>
        /// <returns></returns>
        public DataSet consultaPrestamoDB(int lint_Opcion, string lstr_Valor1, string lstr_Valor2)
        {
            DataSet ldta_ResultadoConsultaDB = new DataSet();
            clsSP_Prestamos lcls_consultaPrestamo = new clsSP_Prestamos();
            ldta_ResultadoConsultaDB = lcls_consultaPrestamo.consultarPrestamos(lint_Opcion, lstr_Valor1, lstr_Valor2);
            return ldta_ResultadoConsultaDB;
        }


        public string modificaPrestamoDB(ArrayList larr_InfoPrestamo)
        {
            string lstr_ResultadoModificacionDB = "";
            clsSP_Prestamos lcls_ModificaPrestamo = new clsSP_Prestamos();
            lstr_ResultadoModificacionDB = lcls_ModificaPrestamo.modificaPrestamos(larr_InfoPrestamo);
            return lstr_ResultadoModificacionDB;
        }

        #endregion
    }
}