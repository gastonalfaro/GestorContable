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
    public class clsInsertarArchivoPlantillaEstadoFinancieroFileStream : clsProcedimientoAlmacenado
    {
        #region variables
        private string lstr_DireccionConfigs = String.Empty;

        private int lint_IdEstadoFinanciero;
        private string lstr_NombreArchivo;
        private string lstr_TipoArchivo;
        private DateTime ldt_FechaArchivo;
        private string lstr_Usuario;

        private string lstr_Estado;
        private string lstr_UsrCreacion;
        #endregion

        #region obtencion y asignacion
        public int Lint_IdEstadoFinanciero
        {
            get { return lint_IdEstadoFinanciero; }
            set { lint_IdEstadoFinanciero = value; }
        }
        public string Lstr_NombreArchivo
        {
            get { return lstr_NombreArchivo; }
            set { lstr_NombreArchivo = value; }
        }
        public string Lstr_TipoArchivo
        {
            get { return lstr_TipoArchivo; }
            set { lstr_TipoArchivo = value; }
        }
        public DateTime Ldt_FechaArchivo
        {
            get { return ldt_FechaArchivo; }
            set { ldt_FechaArchivo = value; }
        }

        public string Lstr_Usuario
        {
            get { return lstr_Usuario; }
            set { lstr_Usuario = value; }
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
        public clsInsertarArchivoPlantillaEstadoFinancieroFileStream(byte[] Buffer, int int_IdEstadoFinanciero, string str_NombreArchivo, string str_TipoArchivo, DateTime dt_FechaArchivo, string str_Usuario, string str_Estado, string str_UsrCreacion)
        {
            lint_IdEstadoFinanciero = int_IdEstadoFinanciero;
            lstr_NombreArchivo = str_NombreArchivo;
            lstr_TipoArchivo = str_TipoArchivo;
            ldt_FechaArchivo = dt_FechaArchivo;
            lstr_Estado = str_Estado;
            lstr_UsrCreacion = str_UsrCreacion;
            lstr_Usuario = str_Usuario;

            string str_resultado = String.Empty;
            try
            {
                SqlConnection objSqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                SqlFileStream objSqlFileStream = null;
            
                try
                {
                    objSqlCon.Open();

                    SqlTransaction objSqlTran = objSqlCon.BeginTransaction();

                    SqlCommand objSqlCmd = new SqlCommand("pc.uspInsertarArchivoPlantillaEstadoFinanciero", objSqlCon, objSqlTran);
                    objSqlCmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter objSqlParamIdEstadoFinanciero = new SqlParameter("@pIdEstadoFinanciero", SqlDbType.Int);
                    objSqlParamIdEstadoFinanciero.Value = int_IdEstadoFinanciero;

                    SqlParameter objSqlParamNombreArchivo = new SqlParameter("@pNombreArchivo", SqlDbType.VarChar);
                    objSqlParamNombreArchivo.Value = str_NombreArchivo;

                    SqlParameter objSqlParamTipoArchivo = new SqlParameter("@pTipoArchivo", SqlDbType.VarChar);
                    objSqlParamTipoArchivo.Value = str_TipoArchivo;

                    SqlParameter objSqlParamFechaArchivo = new SqlParameter("@pFechaArchivo", SqlDbType.DateTime);
                    objSqlParamFechaArchivo.Value = dt_FechaArchivo;

                    SqlParameter objSqlParamUsuario = new SqlParameter("@pUsuario", SqlDbType.VarChar, 50);
                    objSqlParamUsuario.Value = lstr_UsrCreacion;

                    SqlParameter objSqlParamStatus = new SqlParameter("@pResultado", SqlDbType.VarChar, 2);
                    objSqlParamStatus.Direction = ParameterDirection.Output;

                    SqlParameter objSqlParamMessage = new SqlParameter("@pMensaje", SqlDbType.VarChar, 500);
                    objSqlParamMessage.Direction = ParameterDirection.Output;

                    objSqlCmd.Parameters.Add(objSqlParamIdEstadoFinanciero);
                    objSqlCmd.Parameters.Add(objSqlParamNombreArchivo);
                    objSqlCmd.Parameters.Add(objSqlParamTipoArchivo);
                    objSqlCmd.Parameters.Add(objSqlParamFechaArchivo);
                    objSqlCmd.Parameters.Add(objSqlParamUsuario);
                    objSqlCmd.Parameters.Add(objSqlParamStatus);
                    objSqlCmd.Parameters.Add(objSqlParamMessage);

                    //objSqlCmd.ExecuteNonQuery();
                    //string Path = objSqlCmd.Parameters["@PathArchivo"].Value.ToString();


                    DataSet Data = new DataSet();
                    DataTable table = new DataTable();
                    table.Load(objSqlCmd.ExecuteReader());
                    Data.Tables.Add(table);

                    this.Lstr_RespuestaXML = Data.GetXml();
                    this.Lstr_RespuestaSchema = Data.GetXmlSchema();
                    this.Lstr_MensajeRespuesta = str_resultado;
                    this.Lstr_CodigoResultado = objSqlCmd.Parameters["@pResultado"].Value.ToString();
                    string Path = "";

                    if (objSqlCmd.Parameters["@pResultado"].Value.ToString() == "00")
                    {
                        if (!Data.Tables["Table1"].Rows.Count.Equals(0))
                        {
                            for (int i = 0; i <= Data.Tables["Table1"].Rows.Count - 1; i++)
                            {
                                Path = Data.Tables["Table1"].Rows[i]["RutaArchivo"].ToString();
                            }
                        }

                        objSqlCmd = new SqlCommand("SELECT GET_FILESTREAM_TRANSACTION_CONTEXT()", objSqlCon, objSqlTran);

                        byte[] objContext = (byte[])objSqlCmd.ExecuteScalar();

                        objSqlFileStream = new SqlFileStream(Path, objContext, FileAccess.Write);

                        objSqlFileStream.Write(Buffer, 0, Buffer.Length);

                        objSqlFileStream.Close();
                        objSqlTran.Commit();

                        this.Lstr_MensajeRespuesta = "Inserción satisfactoria.";
                        this.Lstr_CodigoResultado = "01";

                    }
                }
                catch (Exception ex)
                {

                    this.Lstr_CodigoResultado = "89";
                    this.Lstr_MensajeRespuesta = str_resultado + " [Error] " + ex.ToString();
                    objSqlFileStream.Close();
                }
            }
            catch(Exception e1)
            {
                
                this.Lstr_CodigoResultado = "89";
                this.Lstr_MensajeRespuesta = str_resultado + " [Error] " + e1.ToString();
            }
        }
        #endregion

    }
}