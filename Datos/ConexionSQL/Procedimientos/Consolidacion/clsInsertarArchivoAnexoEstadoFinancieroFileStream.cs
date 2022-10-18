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
    public class clsInsertarArchivoAnexoEstadoFinancieroFileStream : clsProcedimientoAlmacenado
    {
        #region variables
        private string lstr_DireccionConfigs = String.Empty;

        private string lstr_IdEntidad;
        private int lint_IdEstadoFinanciero;
        private int lint_Periodo;
        private string lstr_UnidadTiempoPeriodo;
        private string lstr_NombreArchivo;
        private string lstr_TipoArchivo;
        private int lint_TamanoByteArchivo;
        private DateTime ldt_FechaArchivo;

        private string lstr_Estado;
        private string lstr_UsrCreacion;
        #endregion

        #region obtencion y asignacion
        public string Lstr_IdEntidad
        {
            get { return lstr_IdEntidad; }
            set { lstr_IdEntidad = value; }
        }

        public int Lint_IdEstadoFinanciero
        {
            get { return lint_IdEstadoFinanciero; }
            set { lint_IdEstadoFinanciero = value; }
        }

        public int Lint_Periodo
        {
            get { return lint_Periodo; }
            set { lint_Periodo = value; }
        }

        public string Lstr_UnidadTiempoPeriodo
        {
            get { return lstr_UnidadTiempoPeriodo; }
            set { lstr_UnidadTiempoPeriodo = value; }
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

        public int Lint_TamanoByteArchivo
        {
            get { return lint_TamanoByteArchivo; }
            set { lint_TamanoByteArchivo = value; }
        }
        
        public DateTime Ldt_FechaArchivo
        {
            get { return ldt_FechaArchivo; }
            set { ldt_FechaArchivo = value; }
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
        public clsInsertarArchivoAnexoEstadoFinancieroFileStream(byte[] Buffer, string str_IdEntidad, int int_IdEstadoFinanciero, int int_Periodo, string str_UnidadTiempoPeriodo, string str_NombreArchivo, string str_TipoArchivo, int int_TamanoByteArchivo, DateTime dt_FechaArchivo, string str_Estado, string str_UsrCreacion)
        {
            lstr_IdEntidad = str_IdEntidad;
            lint_IdEstadoFinanciero = int_IdEstadoFinanciero;
            lint_Periodo = int_Periodo;
            lstr_UnidadTiempoPeriodo = str_UnidadTiempoPeriodo;
            lstr_NombreArchivo = str_NombreArchivo;
            lstr_TipoArchivo = str_TipoArchivo;
            lint_TamanoByteArchivo = int_TamanoByteArchivo;
            ldt_FechaArchivo = dt_FechaArchivo;
            lstr_Estado = str_Estado;
            lstr_UsrCreacion = str_UsrCreacion;

            string str_resultado = String.Empty;
            SqlConnection objSqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            SqlFileStream objSqlFileStream = null;
            try
            {
                objSqlCon.Open();

                SqlTransaction objSqlTran = objSqlCon.BeginTransaction();

                SqlCommand objSqlCmd = new SqlCommand("pc.uspInsertarArchivoAnexoEstadoFinanciero", objSqlCon, objSqlTran);
                objSqlCmd.CommandType = CommandType.StoredProcedure;
                str_resultado = str_resultado + " [0]crea SqlCommand.";
               
                SqlParameter objSqlParamNumEntidad = new SqlParameter("@pIdEntidad", SqlDbType.VarChar, 10);
                objSqlParamNumEntidad.Value = str_IdEntidad;

                SqlParameter objSqlParamIdEstadoFinanciero = new SqlParameter("@pIdEstadoFinanciero", SqlDbType.Int);
                objSqlParamIdEstadoFinanciero.Value = int_IdEstadoFinanciero;

                SqlParameter objSqlParamPeriodo = new SqlParameter("@pPeriodo", SqlDbType.Int);
                objSqlParamPeriodo.Value = int_Periodo;

                SqlParameter objSqlParamIdCicloPeriodo = new SqlParameter("@pUnidadTiempoPeriodo", SqlDbType.VarChar, 2);
                objSqlParamIdCicloPeriodo.Value = str_UnidadTiempoPeriodo;

                SqlParameter objSqlParamNombreArchivo = new SqlParameter("@pNombreArchivo", SqlDbType.VarChar, 100);
                objSqlParamNombreArchivo.Value = System.IO.Path.GetFileNameWithoutExtension(str_NombreArchivo);

                SqlParameter objSqlParamTipoArchivo = new SqlParameter("@pTipoArchivo", SqlDbType.VarChar, 5);
                objSqlParamTipoArchivo.Value = System.IO.Path.GetExtension(str_TipoArchivo);

                SqlParameter objSqlParamSizeByteArchivo = new SqlParameter("@pTamanoByteArchivo", SqlDbType.Int);
                objSqlParamSizeByteArchivo.Value = int_TamanoByteArchivo;

                SqlParameter objSqlParamFechaArchivo = new SqlParameter("@pFechaArchivo", SqlDbType.DateTime);
                objSqlParamFechaArchivo.Value = dt_FechaArchivo;

                SqlParameter objSqlParamUsuario = new SqlParameter("@pUsuario", SqlDbType.VarChar, 50);
                objSqlParamUsuario.Value = str_UsrCreacion;

                SqlParameter objSqlParamStatus = new SqlParameter("@pResultado", SqlDbType.VarChar, 2);
                objSqlParamStatus.Direction = ParameterDirection.Output;

                SqlParameter objSqlParamMessage = new SqlParameter("@pMensaje", SqlDbType.VarChar, 500);
                objSqlParamMessage.Direction = ParameterDirection.Output;

                objSqlCmd.Parameters.Add(objSqlParamNumEntidad);
                objSqlCmd.Parameters.Add(objSqlParamIdEstadoFinanciero);
                objSqlCmd.Parameters.Add(objSqlParamPeriodo);
                objSqlCmd.Parameters.Add(objSqlParamIdCicloPeriodo);
                objSqlCmd.Parameters.Add(objSqlParamNombreArchivo);
                objSqlCmd.Parameters.Add(objSqlParamTipoArchivo);
                objSqlCmd.Parameters.Add(objSqlParamSizeByteArchivo);
                objSqlCmd.Parameters.Add(objSqlParamFechaArchivo);
                objSqlCmd.Parameters.Add(objSqlParamUsuario);
                objSqlCmd.Parameters.Add(objSqlParamStatus);
                objSqlCmd.Parameters.Add(objSqlParamMessage);

                DataSet Data = new DataSet();
                DataTable table = new DataTable();
                table.Load(objSqlCmd.ExecuteReader());
                str_resultado = str_resultado + " [1]ExecuteReader SqlCommand: " + objSqlCmd;
                Data.Tables.Add(table);

                this.Lstr_RespuestaXML = Data.GetXml();
                this.Lstr_RespuestaSchema = Data.GetXmlSchema();

                str_resultado = str_resultado + objSqlCmd.Parameters["@pMensaje"].Value.ToString();

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