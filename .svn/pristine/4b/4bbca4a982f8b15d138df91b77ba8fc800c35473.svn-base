using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
using Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna;

namespace LogicaNegocio.CalculosFinancieros.DeudaExterna
{
    public class clsManejoTasasFlotantes : clsSP_TasasFlotantes
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="larr_InfoTasaFlotante"></param>
        /// <returns></returns>
        public string registraTasaFlotanteDB(ArrayList larr_InfoTasaFlotante)
        {
            string lstr_ResultadoRegistroDB = "";
            clsSP_TasasFlotantes lcls_RegistraTasaFlotante = new clsSP_TasasFlotantes();
            lstr_ResultadoRegistroDB = lcls_RegistraTasaFlotante.registrarTasasFlotantes(larr_InfoTasaFlotante);
            return lstr_ResultadoRegistroDB;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="larr_InfoTasaFlotante"></param>
        /// <returns></returns>
        public string eliminaTasaFlotanteDB(ArrayList larr_InfoTasaFlotante)
        {
            string lstr_ResultadoEliminacionDB = "";
            clsSP_TasasFlotantes lcls_EliminaTasaFlotante = new clsSP_TasasFlotantes();
            lstr_ResultadoEliminacionDB = lcls_EliminaTasaFlotante.eliminarTasasFlotantes(larr_InfoTasaFlotante);
            return lstr_ResultadoEliminacionDB;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="larr_InfoComsion"></param>
        /// <param name="lint_Opcion"></param>
        /// <returns></returns>
        public DataSet consultaTasaFlotanteDB(ArrayList larr_InfoComsion, int lint_Opcion)
        {
            DataSet ldta_ResultadoConsultaDB = new DataSet();
            clsSP_TasasFlotantes lcls_consultaTasaFlotante = new clsSP_TasasFlotantes();
            ldta_ResultadoConsultaDB = lcls_consultaTasaFlotante.consultarTasasFlotantes(larr_InfoComsion, lint_Opcion);
            return ldta_ResultadoConsultaDB;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="larr_InfoTasaFlotante"></param>
        /// <returns></returns>
        public string modificaTasaFlotanteDB(ArrayList larr_InfoTasaFlotante)
        {
            string lstr_ResultadoModificacionDB = "";
            clsSP_TasasFlotantes lcls_ModificaTasaFlotante = new clsSP_TasasFlotantes();
            lstr_ResultadoModificacionDB = lcls_ModificaTasaFlotante.modificaTasasFlotantes(larr_InfoTasaFlotante);
            return lstr_ResultadoModificacionDB;
        }
    }
}