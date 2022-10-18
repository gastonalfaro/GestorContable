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
    public class clsInsertarArchivoEstadoFinancieroFileStream : clsProcedimientoAlmacenado
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
        private string lstr_Usuario;

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
        public clsInsertarArchivoEstadoFinancieroFileStream(byte[] Buffer, string str_IdEntidad, int int_EstadoFinanciero, int int_Periodo, string str_UnidadTiempoPeriodo, string str_NombreArchivo, string str_TipoArchivo, int int_TamanoByteArchivo, DateTime dt_FechaArchivo, string str_Usuario, string str_Estado, string str_UsrCreacion)
        {

            string str_resultado = String.Empty;
            try
            {
                lstr_IdEntidad = str_IdEntidad;
                lint_IdEstadoFinanciero = int_EstadoFinanciero;
                lint_Periodo = int_Periodo;
                lstr_UnidadTiempoPeriodo = str_UnidadTiempoPeriodo;
                lstr_NombreArchivo = str_NombreArchivo;
                lstr_TipoArchivo = str_TipoArchivo;
                lint_TamanoByteArchivo = int_TamanoByteArchivo;
                ldt_FechaArchivo = dt_FechaArchivo;
                lstr_Usuario = str_Usuario;

                lstr_Estado = str_Estado;
                lstr_UsrCreacion = str_UsrCreacion;

                clsProcedimientoAlmacenado cpa_Llamado = new clsProcedimientoAlmacenado();
                str_resultado = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;
                
                SqlConnection objSqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                str_resultado = str_resultado+" 1. ";
                SqlFileStream objSqlFileStream = null;
                str_resultado = str_resultado +" 2. ";
                try
                {
                    objSqlCon.Open();
                    str_resultado = str_resultado + "[1]Open SqlConnection";

                    SqlTransaction objSqlTran = objSqlCon.BeginTransaction();
                    str_resultado = str_resultado + " [2]creó SqlTransaction";

                    SqlCommand objSqlCmd = new SqlCommand("pc.uspInsertarArchivoEstadoFinanciero", objSqlCon, objSqlTran);
                    objSqlCmd.CommandType = CommandType.StoredProcedure;
                    str_resultado = str_resultado + " [3]creó SqlCommand";

                    SqlParameter objSqlParamNumEntidad = new SqlParameter("@pIdEntidad", SqlDbType.Int);
                    objSqlParamNumEntidad.Value = str_IdEntidad;

                    SqlParameter objSqlParamIdEstadoFinanciero = new SqlParameter("@pIdEstadoFinanciero", SqlDbType.Int);
                    objSqlParamIdEstadoFinanciero.Value = int_EstadoFinanciero;

                    SqlParameter objSqlParamPeriodo = new SqlParameter("@pPeriodo", SqlDbType.Int);
                    objSqlParamPeriodo.Value = int_Periodo;

                    SqlParameter objSqlParamIdCicloPeriodo = new SqlParameter("@pUnidadTiempoPeriodo", SqlDbType.VarChar, 2);
                    objSqlParamIdCicloPeriodo.Value = str_UnidadTiempoPeriodo;

                    SqlParameter objSqlParamNombreArchivo = new SqlParameter("@pNombreArchivo", SqlDbType.VarChar, 100);
                    objSqlParamNombreArchivo.Value = System.IO.Path.GetFileNameWithoutExtension(str_NombreArchivo);

                    SqlParameter objSqlParamTipoArchivo = new SqlParameter("@pTipoArchivo", SqlDbType.VarChar, 5);
                    objSqlParamTipoArchivo.Value = System.IO.Path.GetExtension(str_TipoArchivo);

                    SqlParameter objSqlParamSizeByteArchivo = new SqlParameter("@pTamanoByteArchivo", SqlDbType.Int);
                    objSqlParamSizeByteArchivo.Value = Buffer.Length;

                    SqlParameter objSqlParamFechaArchivo = new SqlParameter("@pFechaArchivo", SqlDbType.DateTime);
                    objSqlParamFechaArchivo.Value = dt_FechaArchivo;

                    SqlParameter objSqlParamUsuario = new SqlParameter("@pUsuario", SqlDbType.VarChar, 50);
                    objSqlParamUsuario.Value = str_Usuario;

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
                    str_resultado = str_resultado + " [4]ExecuteReader SqlCommand: " +
                        objSqlCmd;
                    SqlDataReader lol = objSqlCmd.ExecuteReader();

                    str_resultado = str_resultado + " [4.1]ExecuteReader SqlCommand";
                    table.Load(lol);
                    str_resultado = str_resultado + " [4.2]ExecuteReader SqlCommand";
                    Data.Tables.Add(table);

                    this.Lstr_RespuestaXML = Data.GetXml();
                    this.Lstr_RespuestaSchema = Data.GetXmlSchema();

                    string Path = "";
                    str_resultado = str_resultado + " [4.3]" + objSqlCmd.Parameters["@pResultado"].Value.ToString() + "[M]" + objSqlCmd.Parameters["@pMensaje"].Value.ToString();
                    this.Lstr_CodigoResultado = objSqlCmd.Parameters["@pResultado"].Value.ToString();
     
                    if ((objSqlCmd.Parameters["@pResultado"].Value.ToString() == "00") ||
                        (objSqlCmd.Parameters["@pResultado"].Value.ToString() == "True"))
                    {
                        if (!Data.Tables["Table1"].Rows.Count.Equals(0))//Si hizo la consulta pero No retorno nada
                        {
                            for (int i = 0; i <= Data.Tables["Table1"].Rows.Count - 1; i++)
                            {
                                Path = Data.Tables["Table1"].Rows[i]["RutaArchivo"].ToString();
                                str_resultado = str_resultado + " [4.4]" + Path;
                            }
                        }

                        objSqlCmd = new SqlCommand("SELECT GET_FILESTREAM_TRANSACTION_CONTEXT()", objSqlCon, objSqlTran);
                        str_resultado = str_resultado + " [5]creó SqlCommand";

                        byte[] objContext = (byte[])objSqlCmd.ExecuteScalar();
                        str_resultado = str_resultado + " [6]ExecuteScalar SqlCommand";

                        objSqlFileStream = new SqlFileStream(Path, objContext, FileAccess.Write);
                        str_resultado = str_resultado + " [7]creó sqlfilestream";

                        objSqlFileStream.Write(Buffer, 0, Buffer.Length);
                        str_resultado = str_resultado + " [8]escribió";

                        objSqlFileStream.Close();
                        str_resultado = str_resultado + " [9]cerró";
                        objSqlTran.Commit();
                        str_resultado = str_resultado + " [10]hizo commit";

                        this.Lstr_MensajeRespuesta = str_resultado + " [Fin!]";
                        this.Lstr_CodigoResultado = "01";
     
                    }
                }
                catch (Exception ex)
                {

                    this.Lstr_MensajeRespuesta = str_resultado + " [Error!!] " + ex.ToString();
                    try
                    {
                        objSqlFileStream.Close();
                    }
                    catch (Exception ex2)
                    {
                        this.Lstr_MensajeRespuesta = str_resultado + " [Error!!] " + ex2.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                this.Lstr_MensajeRespuesta = str_resultado + " [Error!!] " + ex.ToString();
            }
        }
        #endregion

    }
}