using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
using Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna;

namespace LogicaNegocio.CalculosFinancieros.DeudaExterna
{
    public class clsManejoTramos : clsSP_Tramos
    {
        public string registraTramoDB(ArrayList larr_InfoTramo)
        {
            string lstr_ResultadoRegistroDB = "";
            clsSP_Tramos lcls_RegistraTramo = new clsSP_Tramos();
            lstr_ResultadoRegistroDB = lcls_RegistraTramo.registrarTramos(larr_InfoTramo);
            return lstr_ResultadoRegistroDB;
        }

        /// <summary>
        /// Elimina un préstamo de la base de datos (inhabilita el registro, no lo elimina físicamente)
        /// </summary>
        /// <param name="lstr_IdTramo">Identificador del préstamo que se requiere inhabilitar</param>
        /// <param name="lstr_Estado">Nuevo estado del préstamo (inhabilitado)</param>
        /// <param name="lstr_UsrModifica">Usuario que genera la eliminación del préstamo</param>
        /// <param name="ldt_FchModifica">Fecha en la que se elimina el Tramo</param>
        /// <returns>Retorna un número resultado de la transacción realizada</returns>
        public string eliminaTramoDB(ArrayList larr_InfoTramo)
        {
            string lstr_ResultadoEliminacionDB = "";
            clsSP_Tramos lcls_EliminaTramo = new clsSP_Tramos();
            lstr_ResultadoEliminacionDB = lcls_EliminaTramo.eliminarTramos(larr_InfoTramo);
            return lstr_ResultadoEliminacionDB;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lint_Opcion"></param>
        /// <param name="lstr_Valor1"></param>
        /// <param name="lstr_Valor2"></param>
        /// <returns></returns>
        public DataSet consultaTramoDB(ArrayList larr_InfoTramo, int lint_Opcion)
        {
            DataSet ldta_ResultadoConsultaDB = new DataSet();
            clsSP_Tramos lcls_consultaTramo = new clsSP_Tramos();
            ldta_ResultadoConsultaDB = lcls_consultaTramo.consultarTramos(larr_InfoTramo, lint_Opcion);
            return ldta_ResultadoConsultaDB;
        }


        public string modificaTramoDB(ArrayList larr_InfoTramo)
        {
            string lstr_ResultadoModificacionDB = "";
            clsSP_Tramos lcls_ModificaTramo = new clsSP_Tramos();
            lstr_ResultadoModificacionDB = lcls_ModificaTramo.modificaTramos(larr_InfoTramo);
            return lstr_ResultadoModificacionDB;
        }
    }
}