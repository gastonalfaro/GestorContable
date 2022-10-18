using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
using Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna;

namespace LogicaNegocio.CalculosFinancieros.DeudaExterna
{
    public class clsManejoIntereses : clsSP_Intereses
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="larr_InfoInteres"></param>
        /// <returns></returns>
        public string registraInteresDB(ArrayList larr_InfoInteres)
        {
            string lstr_ResultadoRegistroDB = "";
            clsSP_Intereses lcls_RegistraInteres = new clsSP_Intereses();
            lstr_ResultadoRegistroDB = lcls_RegistraInteres.registrarIntereses(larr_InfoInteres);
            return lstr_ResultadoRegistroDB;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="larr_InfoInteres"></param>
        /// <returns></returns>
        public string eliminaInteresDB(ArrayList larr_InfoInteres)
        {
            string lstr_ResultadoEliminacionDB = "";
            clsSP_Intereses lcls_EliminaInteres = new clsSP_Intereses();
            lstr_ResultadoEliminacionDB = lcls_EliminaInteres.eliminarIntereses(larr_InfoInteres);
            return lstr_ResultadoEliminacionDB;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="larr_InfoComsion"></param>
        /// <param name="lint_Opcion"></param>
        /// <returns></returns>
        public DataSet consultaInteresDB(ArrayList larr_InfoComsion, int lint_Opcion)
        {
            DataSet ldta_ResultadoConsultaDB = new DataSet();
            clsSP_Intereses lcls_consultaInteres = new clsSP_Intereses();
            ldta_ResultadoConsultaDB = lcls_consultaInteres.consultarIntereses(larr_InfoComsion, lint_Opcion);
            return ldta_ResultadoConsultaDB;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="larr_InfoInteres"></param>
        /// <returns></returns>
        public string modificaInteresDB(ArrayList larr_InfoInteres)
        {
            string lstr_ResultadoModificacionDB = "";
            clsSP_Intereses lcls_ModificaInteres = new clsSP_Intereses();
            lstr_ResultadoModificacionDB = lcls_ModificaInteres.modificaIntereses(larr_InfoInteres);
            return lstr_ResultadoModificacionDB;
        }
    }
}