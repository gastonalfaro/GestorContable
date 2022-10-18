using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.Consolidacion
{
    public class clsBuscarArchivoAnexoEstadoFinancieroFilestream : clsProcedimientoAlmacenado
    {
        #region variables
        private string lstr_DireccionConfigs = String.Empty;

        private string lstr_IdEstadoFinancieroArchivoAnexo;
        
        private string lstr_Estado;
        private string lstr_UsrCreacion;
        #endregion

        #region obtencion y asignacion
        public string Lstr_IdEstadoFinancieroArchivoAnexo
        {
            get { return lstr_IdEstadoFinancieroArchivoAnexo; }
            set { lstr_IdEstadoFinancieroArchivoAnexo = value; }
        }

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
        public clsBuscarArchivoAnexoEstadoFinancieroFilestream(string str_IdEstadoFinancieroArchivo, string str_Estado, string str_UsrCreacion)
        {
            lstr_IdEstadoFinancieroArchivoAnexo = str_IdEstadoFinancieroArchivo;
            lstr_Estado = str_Estado;
            lstr_UsrCreacion = str_UsrCreacion;

            string str_resultado = String.Empty;
            SqlConnection objSqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);//"ConnectionString"
            SqlFileStream objSqlFileStream = null;
            try
            {
                objSqlCon.Open();

                SqlTransaction objSqlTran = objSqlCon.BeginTransaction();

                SqlCommand objSqlCmd = new SqlCommand("pc.uspBuscarArchivoAnexoEstadoFinanciero", objSqlCon, objSqlTran);
                objSqlCmd.CommandType = CommandType.StoredProcedure;

                SqlParameter objSqlParamIdEstadoFinanciero = new SqlParameter("@pIdEstadoFinancieroArchivoAnexo", SqlDbType.VarChar, 50);
                objSqlParamIdEstadoFinanciero.Value = str_IdEstadoFinancieroArchivo;

                SqlParameter objSqlParamStatus = new SqlParameter("@pResultado", SqlDbType.VarChar, 2);
                objSqlParamStatus.Direction = ParameterDirection.Output;

                SqlParameter objSqlParamMessage = new SqlParameter("@pMensaje", SqlDbType.VarChar, 500);
                objSqlParamMessage.Direction = ParameterDirection.Output;

                objSqlCmd.Parameters.Add(objSqlParamIdEstadoFinanciero);
                objSqlCmd.Parameters.Add(objSqlParamStatus);
                objSqlCmd.Parameters.Add(objSqlParamMessage);

                DataSet Data = new DataSet();
                DataTable table = new DataTable();
                table.Load(objSqlCmd.ExecuteReader());
                Data.Tables.Add(table);

                this.Lstr_RespuestaXML = Data.GetXml();
                this.Lstr_RespuestaSchema = Data.GetXmlSchema();

                str_resultado = str_resultado + objSqlCmd.Parameters["@pMensaje"].Value.ToString();

                this.Lstr_MensajeRespuesta = str_resultado;
                this.Lstr_CodigoResultado = objSqlCmd.Parameters["@pResultado"].Value.ToString();

                string path = string.Empty;
                string fileType = string.Empty;
                string fileName = string.Empty;


                DataRow drNew = Data.Tables["Table1"].NewRow();

                if (objSqlCmd.Parameters["@pResultado"].Value.ToString() == "00")
                {
                    if (!Data.Tables["Table1"].Rows.Count.Equals(0))
                    {
                        for (int i = 0; i <= Data.Tables["Table1"].Rows.Count - 1; i++)
                        {
                            path = Data.Tables["Table1"].Rows[i]["RutaSistemaArchivo"].ToString();
                            fileName = Data.Tables["Table1"].Rows[i]["NombreArchivo"].ToString();
                            fileType = Data.Tables["Table1"].Rows[i]["TipoArchivo"].ToString();
                        }
                    }

                    objSqlCmd = new SqlCommand("SELECT GET_FILESTREAM_TRANSACTION_CONTEXT()", objSqlCon, objSqlTran);


                    byte[] objContext = (byte[])objSqlCmd.ExecuteScalar();

                    objSqlFileStream = new SqlFileStream(path, objContext, FileAccess.Read);

                    byte[] buffer = new byte[(int)objSqlFileStream.Length];
                    objSqlFileStream.Read(buffer, 0, buffer.Length);
                    objSqlFileStream.Close();


                    objSqlTran.Commit();


                    Data.Tables["Table1"].Columns.Add("Buffer", typeof(byte[]));
                    Data.Tables["Table1"].Rows[0]["Buffer"] = buffer;

                    this.Lstr_RespuestaXML = Data.GetXml();
                    this.Lstr_RespuestaSchema = Data.GetXmlSchema();

                    this.Lstr_MensajeRespuesta = "Busqueda satisfactoria.";
                    this.Lstr_CodigoResultado = "01";

                }
            }
            catch (Exception ex)
            {

                this.Lstr_CodigoResultado = "89";
                str_resultado = str_resultado + " [Error] " + ex.ToString();
                this.Lstr_MensajeRespuesta = str_resultado;
                try
                {
                    objSqlFileStream.Close();
                }
                catch (Exception e)
                {
                    this.Lstr_MensajeRespuesta = str_resultado + " [Error 2] " + ex.ToString();
                }
            }
        }
        #endregion



    }
}