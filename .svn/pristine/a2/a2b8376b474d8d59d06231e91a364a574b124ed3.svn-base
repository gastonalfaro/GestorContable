using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Datos.ConexionSQL.Procedimientos.Mantenimiento;

namespace Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna
{
    public class clsTipoCambioBCCR
    {
        SqlConnection lsql_Conexion;
        SqlDataReader lsql_LectordDatos;

        /// <summary>
        /// Constantes que determinan los nombres de los procedimientos almacenados de la base de datos
        /// </summary>
        private const string lstr_CrearTipoCambioBCCR = "ma.uspCrearTipoCambio",
        lstr_ConsultaISOconBCCR = "ma.uspConsultaISOconBCCR";
        private const string lstr_ConsultaIndicadoresBCCR = "ma.uspConsultarIndicadoresEconomicos";

        /// <summary>
        /// Método encargado de almacenar los tipos de cambio en la base de datos
        /// </summary>
        /// <param name="larr_InfoTipoCambioBCCR"></param>
        /// <returns>Retorna el estado de la transacción</returns>
        public string registrarTipoCambioBCCR(ArrayList larr_InfoTipoCambioBCCR)
        {
            string lstr_IdMoneda = Convert.ToString(larr_InfoTipoCambioBCCR[0]);
            DateTime ldt_FchReferencia = Convert.ToDateTime(larr_InfoTipoCambioBCCR[1]);
            string lstr_TipoTransaccion = Convert.ToString(larr_InfoTipoCambioBCCR[2]);
            decimal ldec_Valor = Convert.ToDecimal(larr_InfoTipoCambioBCCR[3]);
            string lstr_UsrCreacion = Convert.ToString(larr_InfoTipoCambioBCCR[4]);

            clsCrearTipoCambio ltc_tipoCambio = new clsCrearTipoCambio(lstr_IdMoneda, ldt_FchReferencia, lstr_TipoTransaccion, ldec_Valor, lstr_UsrCreacion);

            return "Tipo de cambio registrado en base de datos";
        }


        /// <summary>
        /// Método encargado de almacenar los indicadores economicos en la base de datos
        /// </summary>
        /// <param name="larr_InfoIndicadoresEcoBCCR"></param>
        /// <returns>Retorna el estado de la transacción</returns>
        public string registrarIndicadoresEcoBCCR(ArrayList larr_InfoIndicadoresEcoBCCR)
        {
            string lstr_IdIndicadorEco = Convert.ToString(larr_InfoIndicadoresEcoBCCR[0]);
            DateTime ldt_FchReferencia = Convert.ToDateTime(larr_InfoIndicadoresEcoBCCR[1]);
            //string lstr_TipoTransaccion = Convert.ToString(larr_InfoIndicadoresEcoBCCR[2]);
            decimal ldec_Valor = Convert.ToDecimal(larr_InfoIndicadoresEcoBCCR[2]);
            string lstr_UsrCreacion = Convert.ToString(larr_InfoIndicadoresEcoBCCR[3]);

            clsCrearValorIndicadorEco ltc_IndicadorEco = new clsCrearValorIndicadorEco(lstr_IdIndicadorEco, ldt_FchReferencia, ldec_Valor, lstr_UsrCreacion);

            return "Indicador Economico registrado en base de datos";
        }

        /// <summary>
        /// Método encargado de traer los códigos cruzados del ISO con el BCCR
        /// </summary>
        /// <returns>Retorna un dataset con los códigos del ISO y el BCCR</returns>
        public DataSet consultarISOconBCCR()
        {
            DataSet ldas_ConsultaISOconBCCR = new DataSet();
            lsql_Conexion = new SqlConnection(Properties.Settings.Default.SqlString);
            
            SqlDataAdapter ldad_Consultar = new SqlDataAdapter(lstr_ConsultaISOconBCCR, lsql_Conexion);
            ldad_Consultar.SelectCommand.CommandType = CommandType.StoredProcedure;

            try
            {
                lsql_Conexion.Open();
                ldad_Consultar.Fill(ldas_ConsultaISOconBCCR);
            }
            catch (Exception e)
            {
                ldas_ConsultaISOconBCCR.Tables[0].Rows[0][0] = e.ToString();
            }
            finally
            {
                if (lsql_Conexion != null)
                {
                    lsql_Conexion.Close();
                    lsql_Conexion.Dispose();
                }
            }
            return ldas_ConsultaISOconBCCR;
        }

        /// <summary>
        /// Método encargado de traer los Indicadores Economicos del BCCR
        /// </summary>
        /// <returns>Retorna un dataset con los Indicadores Economicos del BCCR</returns>
        public DataSet consultarIndicadoresEcoBCCR()
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarIndicadoresEconomicos cr_Procedimiento = new clsConsultarIndicadoresEconomicos(null, null, null);
                lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return lds_TablasConsulta;
            /*DataSet ldas_ConsultaIndicadoresBCCR = new DataSet();
            lsql_Conexion = new SqlConnection(Properties.Settings.Default.SqlString);

            SqlDataAdapter ldad_Consultar = new SqlDataAdapter(lstr_ConsultaIndicadoresBCCR, lsql_Conexion);
            ldad_Consultar.SelectCommand.CommandType = CommandType.StoredProcedure;

            try
            {
                lsql_Conexion.Open();
                ldad_Consultar.Fill(ldas_ConsultaIndicadoresBCCR);
            }
            catch (Exception e)
            {
                ldas_ConsultaIndicadoresBCCR.Tables[0].Rows[0][0] = e.ToString();
            }
            finally
            {
                if (lsql_Conexion != null)
                {
                    lsql_Conexion.Close();
                    lsql_Conexion.Dispose();
                }
            }
            return ldas_ConsultaIndicadoresBCCR;*/
        }

    }
}