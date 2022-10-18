using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Consolidacion
{
    public class clsEjecutarDTSXPrueba : clsProcedimientoAlmacenado
    {
        #region variables
        private string lstr_DireccionConfigs = String.Empty;

        private string lstr_Estado;
        private string lstr_UsrCreacion;
        #endregion

        #region obtencion y asignacion
       
        public string Lstr_Estado
        {
            get { return lstr_Estado; }
            set { lstr_Estado = value; }
        }

        public string Lstr_UsrCreacion
        {
            get { return lstr_UsrCreacion; }
            set { lstr_UsrCreacion = value; }
        }

        #endregion

        #region procedimientos
        public clsEjecutarDTSXPrueba(string str_Estado, string str_UsrCreacion)
        {
            lstr_Estado = str_Estado;
            lstr_UsrCreacion = str_UsrCreacion;
            String bandera = String.Empty;
            try
            {
                SqlConnection objSqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);//"ConnectionString"
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
                bandera = "1.";
                table.Load(objSqlCmd.ExecuteReader());
                bandera = bandera + "2." + objSqlCmd.Parameters["@pMensaje"].Value.ToString();
               
                Data.Tables.Add(table);

                this.Lstr_RespuestaXML = Data.GetXml();
                this.Lstr_RespuestaSchema = Data.GetXmlSchema();

                 this.Lstr_CodigoResultado = objSqlCmd.Parameters["@pResultado"].Value.ToString();

                objSqlTran.Commit();
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = bandera + "[error!] " + ex.Message.ToString();
            }  
        }
        #endregion

    }
}