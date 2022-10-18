using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
using Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna;

namespace LogicaNegocio.CalculosFinancieros.DeudaExterna
{
    public class clsManejoGirosEstimados : clsSP_GirosEstimados
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="larr_InfoGiroEstimado"></param>
        /// <returns></returns>
        public string registraGiroEstimadoDB(ArrayList larr_InfoGiroEstimado)
        {
            string lstr_ResultadoRegistroDB = "";
            clsSP_GirosEstimados lcls_RegistraGiroEstimado = new clsSP_GirosEstimados();
            lstr_ResultadoRegistroDB = lcls_RegistraGiroEstimado.registrarGirosEstimados(larr_InfoGiroEstimado);
            return lstr_ResultadoRegistroDB;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="larr_InfoGiroEstimado"></param>
        /// <returns></returns>
        public string eliminaGiroEstimadoDB(ArrayList larr_InfoGiroEstimado)
        {
            string lstr_ResultadoEliminacionDB = "";
            clsSP_GirosEstimados lcls_EliminaGiroEstimado = new clsSP_GirosEstimados();
            lstr_ResultadoEliminacionDB = lcls_EliminaGiroEstimado.eliminarGirosEstimados(larr_InfoGiroEstimado);
            return lstr_ResultadoEliminacionDB;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="larr_InfoGiroEstimado"></param>
        /// <param name="lint_Opcion"></param>
        /// <returns></returns>
        public DataSet consultaGiroEstimadoDB(ArrayList larr_InfoGiroEstimado, int lint_Opcion)
        {
            DataSet ldta_ResultadoConsultaDB = new DataSet();
            clsSP_GirosEstimados lcls_consultaGiroEstimado = new clsSP_GirosEstimados();
            ldta_ResultadoConsultaDB = lcls_consultaGiroEstimado.consultarGirosEstimados(larr_InfoGiroEstimado, lint_Opcion);
            return ldta_ResultadoConsultaDB;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="larr_InfoGiroEstimado"></param>
        /// <returns></returns>
        public string modificaGiroEstimadoDB(ArrayList larr_InfoGiroEstimado)
        {
            string lstr_ResultadoModificacionDB = "";
            clsSP_GirosEstimados lcls_ModificaGiroEstimado = new clsSP_GirosEstimados();
            lstr_ResultadoModificacionDB = lcls_ModificaGiroEstimado.modificaGirosEstimados(larr_InfoGiroEstimado);
            return lstr_ResultadoModificacionDB;
        }
    }
}