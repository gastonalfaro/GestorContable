using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
using Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna;

namespace LogicaNegocio.CalculosFinancieros.DeudaExterna
{
    public class clsManejoInteresesPunitivos : clsSP_InteresesPunitivos
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="larr_InfoInteresPunitivo"></param>
        /// <returns></returns>
        public string registraInteresDB(ArrayList larr_InfoInteresPunitivo)
        {
            string lstr_ResultadoRegistroDB = "";
            clsSP_InteresesPunitivos lcls_RegistraInteres = new clsSP_InteresesPunitivos();
            lstr_ResultadoRegistroDB = lcls_RegistraInteres.registrarInteresesPunitivos(larr_InfoInteresPunitivo);
            return lstr_ResultadoRegistroDB;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="larr_InfoInteresPunitivo"></param>
        /// <returns></returns>
        public string eliminaInteresDB(ArrayList larr_InfoInteresPunitivo)
        {
            string lstr_ResultadoEliminacionDB = "";
            clsSP_InteresesPunitivos lcls_EliminaInteres = new clsSP_InteresesPunitivos();
            lstr_ResultadoEliminacionDB = lcls_EliminaInteres.eliminarInteresesPunitivos(larr_InfoInteresPunitivo);
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
            clsSP_InteresesPunitivos lcls_consultaInteres = new clsSP_InteresesPunitivos();
            ldta_ResultadoConsultaDB = lcls_consultaInteres.consultarInteresesPunitivos(larr_InfoComsion, lint_Opcion);
            return ldta_ResultadoConsultaDB;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="larr_InfoInteresPunitivo"></param>
        /// <returns></returns>
        public string modificaInteresDB(ArrayList larr_InfoInteresPunitivo)
        {
            string lstr_ResultadoModificacionDB = "";
            clsSP_InteresesPunitivos lcls_ModificaInteres = new clsSP_InteresesPunitivos();
            lstr_ResultadoModificacionDB = lcls_ModificaInteres.modificaInteresesPunitivos(larr_InfoInteresPunitivo);
            return lstr_ResultadoModificacionDB;
        }
    }
}