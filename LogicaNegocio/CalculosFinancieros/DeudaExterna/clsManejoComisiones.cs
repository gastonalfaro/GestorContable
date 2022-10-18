using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
using Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna;

namespace LogicaNegocio.CalculosFinancieros.DeudaExterna
{
    public class clsManejoComisiones : clsSP_Comisiones
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="larr_InfoComision"></param>
        /// <returns></returns>
        public string registraComisionDB(ArrayList larr_InfoComision)
        {
            string lstr_ResultadoRegistroDB = "";
            clsSP_Comisiones lcls_RegistraComision = new clsSP_Comisiones();
            lstr_ResultadoRegistroDB = lcls_RegistraComision.registrarComisiones(larr_InfoComision);
            return lstr_ResultadoRegistroDB;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="larr_InfoComision"></param>
        /// <returns></returns>
        public string eliminaComisionDB(ArrayList larr_InfoComision)
        {
            string lstr_ResultadoEliminacionDB = "";
            clsSP_Comisiones lcls_EliminaComision = new clsSP_Comisiones();
            lstr_ResultadoEliminacionDB = lcls_EliminaComision.eliminarComisiones(larr_InfoComision);
            return lstr_ResultadoEliminacionDB;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="larr_InfoComsion"></param>
        /// <param name="lint_Opcion"></param>
        /// <returns></returns>
        public DataSet consultaComisionDB(ArrayList larr_InfoComsion, int lint_Opcion)
        {
            DataSet ldta_ResultadoConsultaDB = new DataSet();
            clsSP_Comisiones lcls_consultaComision = new clsSP_Comisiones();
            ldta_ResultadoConsultaDB = lcls_consultaComision.consultarComisiones(larr_InfoComsion, lint_Opcion);
            return ldta_ResultadoConsultaDB;
        }
    }
}