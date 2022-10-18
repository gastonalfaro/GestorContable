using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace wsSGPC
{
    /// <summary>
    /// Summary description for wsSGPC
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class wsSGPC : System.Web.Services.WebService
    {


        [WebMethod]
        public String uwsPrueba()
        {
            string lstr_salida = string.Empty;

            /*  
DECLARE  @pvar NVARCHAR(500)  = 
'/SET \Package.Variables[User::PathExcelFile].Properties[Value];"'+'L:\SistemaGestor\Archivos_SistemaGestor\\'+'"' 
+ ' /SET \Package.Variables[User::NameExcelFile].Properties[Value];"21103T32018_ESTADO_BALANCE_COMPROBACION.xlsx"'
+ ' /SET \Package.Variables[User::UsuarioCarga].Properties[Value];"dmendez"'

SELECT @pvar
EXEC pc.uspEjecutarDTSX 'L:/SistemaGestor/DTSX_SistemaGestor/', 
'CargaEstadoFinancieroBalanceComprobacion.dtsx',
@pvar, 
0,
'', '' 
            */

            try
            {
                SqlConnection objSqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                objSqlCon.Open();
                SqlTransaction objSqlTran = objSqlCon.BeginTransaction();

                SqlCommand objSqlCmd = new SqlCommand("pc.uspEjecutarDTSX", objSqlCon, objSqlTran);
                objSqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter objSqlParamstrDTSXPaqueteURL = new SqlParameter("@pstrDTSXPaqueteURL", SqlDbType.VarChar, 200);
                objSqlParamstrDTSXPaqueteURL.Value = "L:/SistemaGestor/DTSX_SistemaGestor/";

                SqlParameter objSqlParamstrDTSXPaqueteNombre = new SqlParameter("@pstrDTSXPaqueteNombre", SqlDbType.VarChar, 100);
                objSqlParamstrDTSXPaqueteNombre.Value = "CargaEstadoFinancieroBalanceComprobacion.dtsx";// "CargaEstadoFinancieroBalanceComprobacion.dtsx";

                SqlParameter objSqlParamstrDTSXPaqueteVariable = new SqlParameter("@pstrDTSXPaqueteVariable", SqlDbType.VarChar, 500);
                objSqlParamstrDTSXPaqueteVariable.Value = "/SET \\Package.Variables[User::PathExcelFile].Properties[Value];\"L:\\SistemaGestor\\Archivos_SistemaGestor\\\\\" /SET \\Package.Variables[User::NameExcelFile].Properties[Value];\"" + "21103T32018_ESTADO_BALANCE_COMPROBACION.xlsx" + "\" /SET \\Package.Variables[User::UsuarioCarga].Properties[Value];\"dmendez\"";

                SqlParameter objSqlParamEjecutar64Bit = new SqlParameter("@pbEjecutar64Bit", SqlDbType.Bit);
                objSqlParamEjecutar64Bit.Value = 0;

                SqlParameter objSqlParamStatus = new SqlParameter("@pResultado", SqlDbType.VarChar, 2);
                objSqlParamStatus.Direction = ParameterDirection.Output;

                SqlParameter objSqlParamMessage = new SqlParameter("@pMensaje", SqlDbType.VarChar, 500);
                objSqlParamMessage.Direction = ParameterDirection.Output;

                objSqlCmd.Parameters.Add(objSqlParamstrDTSXPaqueteURL);
                objSqlCmd.Parameters.Add(objSqlParamstrDTSXPaqueteNombre);
                objSqlCmd.Parameters.Add(objSqlParamstrDTSXPaqueteVariable);
                objSqlCmd.Parameters.Add(objSqlParamEjecutar64Bit);
                objSqlCmd.Parameters.Add(objSqlParamStatus);
                objSqlCmd.Parameters.Add(objSqlParamMessage);

                DataSet Data = new DataSet();
                DataTable table = new DataTable();
                table.Load(objSqlCmd.ExecuteReader());
                Data.Tables.Add(table);

                //lstr_salida = new String[Data.Tables["Table1"].Rows.Count];

                //for (int i = 0; i <= Data.Tables["Table1"].Rows.Count - 1; i++)
                //{
                //    lstr_salida[i] = Data.Tables["Table1"].Rows[i]["FlowLine"].ToString();
                //}


                objSqlTran.Commit();
            }
            catch (Exception ex)
            {
                lstr_salida = "Excepcion:  " + ex.Message.ToString();
                return lstr_salida;
            }

            //catch (Exception ex)
            //{
            //    #region MensajeError
            //    EventLog.WriteEntry(ConfigurationManager.AppSettings["EventLogSource"].ToString(),
            //        //Obtiene el nombre de la clase.
            //    "NICSP"
            //        //Nombre del método.
            //    + "." + MethodInfo.GetCurrentMethod().Name
            //        //Error especifico.
            //    + ": Excepcion  " + ex.Message.ToString() + ". ",
            //    EventLogEntryType.Error);

            //    lstr_salida = "NICSP Excepcion:  " + ex.Message.ToString() + ". ";
            //    return lstr_salida;
            //    #endregion
            //}

            return lstr_salida;
        }
    }
}
