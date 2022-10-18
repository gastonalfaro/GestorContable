using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Xml;
using System.Reflection;
using System.Configuration;
using System.Data.SqlClient;
using log4net;
using System.Data.SqlTypes;
using System.IO;

//[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace Datos.ConexionSQL
{
    public class clsProcedimientoAlmacenado
    { 
        private static readonly ILog logInfo = LogManager.GetLogger("clsProcedimientoAlmacenado");
        /// <summary>
        /// Tipo de clase
        /// </summary>
        private Type ltyp_Tipo;
        public Type Ltyp_Tipo
        {
            get { return ltyp_Tipo; }
            set { ltyp_Tipo = value; }
        }

        /// <summary>
        /// Respuesta XML que se obtuvo del procedimiento
        /// </summary>
        private string lstr_RespuestaXML;
        public string Lstr_RespuestaXML
        {
            get { return lstr_RespuestaXML; }
            set { lstr_RespuestaXML = value; }
        }

        /// <summary>
        /// Esquema XML que se obtuvo del procediemiento
        /// </summary>
        private string lstr_RespuestaSchema;
        public string Lstr_RespuestaSchema
        {
            get { return lstr_RespuestaSchema; }
            set { lstr_RespuestaSchema = value; }
        }

        /// <summary>
        /// Codigo de la respuesta recibida del procedimiento
        /// </summary>
        private string lstr_CodigoResultado;
        public string Lstr_CodigoResultado
        {
            get { return lstr_CodigoResultado; }
            set { lstr_CodigoResultado = value; }
        }

        private string lstr_MensajeRespuesta;
        public string Lstr_MensajeRespuesta
        {
            get { return lstr_MensajeRespuesta; }
            set { lstr_MensajeRespuesta = value; }
        }

        /// <summary>
        /// Ejecuta un procedimiento almacenado
        /// </summary>
        public void EjecucionSP(string str_RutaArchivo, clsProcedimientoAlmacenado cpa_Llamado)
        {
            //Dataset donde se almacena el resultado
            DataSet lds_Tablas = new DataSet();
            string lstr_RutaConfig = String.Empty;
            try
            {
                XmlDocument lxml_ArchivoProcedimiento = new XmlDocument();
                //lstr_RutaConfig = HttpContext.Current.Server.MapPath(str_RutaArchivo);
                lxml_ArchivoProcedimiento.Load(str_RutaArchivo);
                XmlNode lxml_nodoBD = lxml_ArchivoProcedimiento.DocumentElement.SelectSingleNode("/CommandDef/Command");
                XmlNode xml_nodoParametros = lxml_ArchivoProcedimiento.DocumentElement.SelectSingleNode("/CommandDef/Parameters");
                string str_Procedimiento = lxml_nodoBD.SelectSingleNode("Text").InnerText;
                string str_CadenaConexion = lxml_nodoBD.SelectSingleNode("DbInstance").InnerText;
                Database ldb_BaseDatos = DatabaseFactory.CreateDatabase("ConexionBD");
                DbCommand ldbc_Comando = ldb_BaseDatos.GetStoredProcCommand(str_Procedimiento);
                ldbc_Comando.CommandTimeout = Convert.ToInt32(lxml_nodoBD.SelectSingleNode("CommandTimeout").InnerText);


                //Mapea y agrega cada uno de los parametros del procedimiento
                foreach (XmlNode xml_Parametro in xml_nodoParametros)
                {
                    string lstr_NombreParametro = xml_Parametro.SelectSingleNode("Name").InnerText;
                    string lstr_Direccion = xml_Parametro.SelectSingleNode("Direction").InnerText;
                    string lstr_Tipo = xml_Parametro.SelectSingleNode("Type").InnerText;
                    int int_Tamano = Convert.ToInt32(xml_Parametro.SelectSingleNode("Size").InnerText);
                    if (lstr_Direccion == "Input")
                    {
                        //Obtiene la variable de mapeo
                        string lstr_VarMapeo = xml_Parametro.SelectSingleNode("VariableMapeo").InnerText;
                        string lstr_Nulo = xml_Parametro.SelectSingleNode("Nullable").InnerText;
                        PropertyInfo info = cpa_Llamado.GetType().GetProperty(lstr_VarMapeo);
                        Object lobj_ValorPropiedad = Convert.ToString(info.GetValue(cpa_Llamado, null));
                        if (String.Equals(lstr_Nulo, "True") & lobj_ValorPropiedad.Equals(""))
                        { 
                            //Anade parametro de entrada en nulo, si el SP tiene un default usa el default, sino toma el valor NULL
                            ldb_BaseDatos.AddInParameter(ldbc_Comando, lstr_NombreParametro, (DbType)Enum.Parse(typeof(DbType), lstr_Tipo, true), int_Tamano);
                            ldb_BaseDatos.SetParameterValue(ldbc_Comando, lstr_NombreParametro, null);                                                       
                        }
                        else
                        {
                            //Anade parametro de entrada
                            ldb_BaseDatos.AddInParameter(ldbc_Comando, lstr_NombreParametro, (DbType)Enum.Parse(typeof(DbType), lstr_Tipo, true), int_Tamano);
                            ldb_BaseDatos.SetParameterValue(ldbc_Comando, lstr_NombreParametro, lobj_ValorPropiedad);
                        }
                    }
                    else
                    {
                        //Anade Parametro de salida
                        ldb_BaseDatos.AddOutParameter(ldbc_Comando, lstr_NombreParametro, (DbType)Enum.Parse(typeof(DbType), lstr_Tipo, true), int_Tamano);
                    }

                }
                logInfo.Info("ldbc_Comando parametro de entrada al ExecuteQuery de SQL: " + ldbc_Comando);
                lds_Tablas = ldb_BaseDatos.ExecuteDataSet(ldbc_Comando);
                if (lds_Tablas.Tables["Table"] != null)
                {
                    if (lds_Tablas.Tables["Table"].Rows.Count > 0)
                    {
                        for (int i = 0; lds_Tablas.Tables["Table"].Columns.Count > i; i++)
                        {
                            if (lds_Tablas.Tables["Table"].Columns[i].DataType.Name.Equals("DateTime"))
                            {
                                lds_Tablas.Tables["Table"].Columns[i].DateTimeMode = DataSetDateTime.Unspecified;
                            }
                        }
                    }
                }

                cpa_Llamado.Lstr_RespuestaXML = lds_Tablas.GetXml();
                cpa_Llamado.Lstr_RespuestaSchema = lds_Tablas.GetXmlSchema();
                cpa_Llamado.Lstr_CodigoResultado = ldbc_Comando.Parameters["@pResultado"].Value.ToString();
                cpa_Llamado.Lstr_MensajeRespuesta = ldbc_Comando.Parameters["@pMensaje"].Value.ToString();
                 logInfo.Info("MensajeRespuesta y CodigoResultado parametros de salida: " + Lstr_CodigoResultado + " " + Lstr_MensajeRespuesta);
                ldbc_Comando.Connection.Dispose();
            }
            catch (FileNotFoundException ex)
            {
                cpa_Llamado.Lstr_MensajeRespuesta = "No se encuentra el archivo de configuración." + " Ruta: " + str_RutaArchivo;
            }
            catch (Exception ex)
            {
                cpa_Llamado.Lstr_MensajeRespuesta = "Error:" + ex.ToString();
                logInfo.Error("ERROR clsProcedimientoAlmacenado(Revisar error en esta clase en dll Datos) : " + cpa_Llamado.Lstr_MensajeRespuesta);
            }
            
        }

        /// <summary>
        /// Ejecuta un procedimiento almacenado
        /// </summary>
        public void EjecucionProcedimientoArchivo(string str_RutaArchivo, clsProcedimientoAlmacenado cpa_Llamado, byte[] Buffer)
        {
            //Dataset donde se almacena el resultado
            DataSet lds_Tablas = new DataSet();
            string IdEstadoFinanciero = string.Empty;
            string path = string.Empty;
            string fileType = string.Empty;
            string fileName = string.Empty;
            DateTime fileDate;

            try
            {

                XmlDocument lxml_ArchivoProcedimiento = new XmlDocument();
                lxml_ArchivoProcedimiento.Load(str_RutaArchivo);
                XmlNode lxml_nodoBD = lxml_ArchivoProcedimiento.DocumentElement.SelectSingleNode("/CommandDef/Command");
                XmlNode xml_nodoParametros = lxml_ArchivoProcedimiento.DocumentElement.SelectSingleNode("/CommandDef/Parameters");
                string str_Procedimiento = lxml_nodoBD.SelectSingleNode("Text").InnerText;
                
                Database ldb_BaseDatos = DatabaseFactory.CreateDatabase("ConnectionString");
                DbCommand ldbc_Comando = ldb_BaseDatos.GetStoredProcCommand(str_Procedimiento);
                ldbc_Comando.CommandTimeout = Convert.ToInt32(lxml_nodoBD.SelectSingleNode("CommandTimeout").InnerText);


                //Mapea y agrega cada uno de los parametros del procedimiento
                foreach (XmlNode xml_Parametro in xml_nodoParametros)
                {
                    string lstr_NombreParametro = xml_Parametro.SelectSingleNode("Name").InnerText;
                    string lstr_Direccion = xml_Parametro.SelectSingleNode("Direction").InnerText;
                    string lstr_Tipo = xml_Parametro.SelectSingleNode("Type").InnerText;
                    int int_Tamano = Convert.ToInt32(xml_Parametro.SelectSingleNode("Size").InnerText);
                    if (lstr_Direccion == "Input")
                    {

                        //Obtiene la variable de mapeo
                        string lstr_VarMapeo = xml_Parametro.SelectSingleNode("VariableMapeo").InnerText;
                        string lstr_Nulo = xml_Parametro.SelectSingleNode("Nullable").InnerText;
                        PropertyInfo info = cpa_Llamado.GetType().GetProperty(lstr_VarMapeo);
                        Object lobj_ValorPropiedad = Convert.ToString(info.GetValue(cpa_Llamado, null));
                        if (String.Equals(lstr_Nulo, "True") & lobj_ValorPropiedad.Equals(""))
                        { }
                        else
                        {
                            //Anade parametro de entrada
                            ldb_BaseDatos.AddInParameter(ldbc_Comando, lstr_NombreParametro, (DbType)Enum.Parse(typeof(DbType), lstr_Tipo, true), int_Tamano);
                            ldb_BaseDatos.SetParameterValue(ldbc_Comando, lstr_NombreParametro, lobj_ValorPropiedad);
                        }
                    }
                    else
                    {
                        //Anade Parametro de salida
                        ldb_BaseDatos.AddOutParameter(ldbc_Comando, lstr_NombreParametro, (DbType)Enum.Parse(typeof(DbType), lstr_Tipo, true), int_Tamano);
                    }

                }
                
                DbDataReader lddr_reader = ldbc_Comando.ExecuteReader();
                while(lddr_reader.Read())
                {
                    IdEstadoFinanciero = lddr_reader["IdEstadoFinanciero"].ToString();
                    path = lddr_reader["SystemPathArchivo"].ToString();
                    fileName = lddr_reader["NombreArchivo"].ToString();
                    fileType = lddr_reader["TipoArchivo"].ToString();
                    fileDate = DateTime.Parse(lddr_reader["FechaArchivo"].ToString());
                }

                ldbc_Comando.CommandText = "SELECT GET_FILESTREAM_TRANSACTION_CONTEXT()";
                byte[] objContext = (byte[])ldbc_Comando.ExecuteScalar();

                SqlFileStream objSqlFileStream = new SqlFileStream(path, objContext, FileAccess.Write);
                objSqlFileStream.Write(Buffer, 0, Buffer.Length);
                objSqlFileStream.Close();


                //lds_Tablas = ldb_BaseDatos.ExecuteDataSet(ldbc_Comando);
                //cpa_Llamado.Lstr_RespuestaXML = lds_Tablas.GetXml();
                //cpa_Llamado.Lstr_RespuestaSchema = lds_Tablas.GetXmlSchema();
                cpa_Llamado.Lstr_CodigoResultado = ldbc_Comando.Parameters["@pResultado"].Value.ToString();
                cpa_Llamado.Lstr_MensajeRespuesta = ldbc_Comando.Parameters["@pMensaje"].Value.ToString();
                ldbc_Comando.Connection.Dispose();

            }
            catch (Exception ex)
            {
                logInfo.Error("" + ex.Message);
                cpa_Llamado.Lstr_MensajeRespuesta = "Ruta: " + str_RutaArchivo + ". Error:" + ex.ToString();
                
                throw ex;
            }
        }

        public clsProcedimientoAlmacenado()
        {
            ltyp_Tipo = this.GetType();
        }

    }
}