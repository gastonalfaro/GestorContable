using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
using Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna;

namespace LogicaNegocio.CalculosFinancieros.DeudaExterna
{
    public class clsManejoAmortizaciones : clsSP_Amortizaciones
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="larr_InfoAmortizacion"></param>
        /// <returns></returns>
        public string registraAmortizacionDB(ArrayList larr_InfoAmortizacion)
        {
            string lstr_ResultadoRegistroDB = "";
            clsSP_Amortizaciones lcls_RegistraAmortizacion = new clsSP_Amortizaciones();
            lstr_ResultadoRegistroDB = lcls_RegistraAmortizacion.registrarAmortizaciones(larr_InfoAmortizacion);
            return lstr_ResultadoRegistroDB;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="larr_InfoAmortizacion"></param>
        /// <returns></returns>
        public string eliminaAmortizacionDB(ArrayList larr_InfoAmortizacion)
        {
            string lstr_ResultadoEliminacionDB = "";
            clsSP_Amortizaciones lcls_EliminaAmortizacion = new clsSP_Amortizaciones();
            lstr_ResultadoEliminacionDB = lcls_EliminaAmortizacion.eliminarAmortizaciones(larr_InfoAmortizacion);
            return lstr_ResultadoEliminacionDB;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="larr_InfoComsion"></param>
        /// <param name="lint_Opcion"></param>
        /// <returns></returns>
        public DataSet consultaAmortizacionDB(ArrayList larr_InfoComsion, int lint_Opcion)
        {
            DataSet ldta_ResultadoConsultaDB = new DataSet();
            clsSP_Amortizaciones lcls_consultaAmortizacion = new clsSP_Amortizaciones();
            ldta_ResultadoConsultaDB = lcls_consultaAmortizacion.consultarAmortizaciones(larr_InfoComsion, lint_Opcion);
            return ldta_ResultadoConsultaDB;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="larr_InfoAmortizacion"></param>
        /// <returns></returns>
        public string modificaAmortizacionDB(ArrayList larr_InfoAmortizacion)
        {
            string lstr_ResultadoModificacionDB = "";
            clsSP_Amortizaciones lcls_ModificaAmortizacion = new clsSP_Amortizaciones();
            lstr_ResultadoModificacionDB = lcls_ModificaAmortizacion.modificaAmortizaciones(larr_InfoAmortizacion);
            return lstr_ResultadoModificacionDB;
        }
    }
}