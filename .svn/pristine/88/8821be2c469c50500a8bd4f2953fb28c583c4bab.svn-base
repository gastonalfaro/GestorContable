using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using log4net;
namespace Datos.ConexionSQL.Procedimientos.SubirArchivo
{
    public class clsArchivo
    {
        public clsArchivo() { }
        
        //private string GetConnectionString()
        //{
        //    return ConfigurationManager.ConnectionStrings["ConexionBD"];
        //}

        private  void OpenConnection(SqlConnection connection)
        {
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["ConexionBD"].ToString();
            connection.Open();
        }

        private void OpenConnectionNoWS(SqlConnection connection)
        {
            connection.ConnectionString = ConfigurationManager.ConnectionStrings["GestNICSPDEVConnectionString"].ToString();
            connection.Open();
        }

        // Obtiene la lista de archivos
        public DataSet ObtenerListaArchivo()
        {
            DataSet fileList = new DataSet();
            using (SqlConnection connection = new SqlConnection())
            {
                OpenConnection(connection);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandTimeout = 0;

               // cmd.CommandText = "SELECT IdArchivo,IdResolucion,IdSociedadGL,IdRevelacion,IdFormulario,IdExpediente,IdCobroPago,Anno,NombreArchivo,Dato,Tamano FROM rn.Archivos";
                cmd.CommandText = "SELECT * FROM rn.Archivos";
               
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter adapter = new SqlDataAdapter();

                adapter.SelectCommand = cmd;
                adapter.Fill(fileList);

                connection.Close();
            }

            return fileList;
        }

        // Salvar archivos a BD
        public string[] GuardarArchivo(string str_nombre, string str_tipoContenido,
            int int_size, byte[] b_data, long int_IdRevelacion, string str_IdResolucion, int int_IdFormulario, string str_IdExpediente,long lg_IdRevelacionPendiente,string str_IdCobroPago, Int16 int_Anno, string str_userCreacion)
        {
            string lstr_Codigo = string.Empty;
            string lstr_Resultado = string.Empty;
            lstr_Codigo = "00";
            lstr_Resultado = "Inserción satisfactoria de archivo";
            string[] lstr_resultInsertar = new string[2];
            lstr_resultInsertar[0] = lstr_Codigo;
            lstr_resultInsertar[1] = lstr_Resultado;

            using (SqlConnection connection = new SqlConnection())
            {
                try {

                    OpenConnection(connection);
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandTimeout = 0;
                    if (!String.IsNullOrEmpty(str_IdResolucion))
                    {
                        string commandText = "INSERT INTO rn.Archivos (IdResolucion,NombreArchivo,TipoContenido,Tamano,Dato,UsrCreacion) ";
                        commandText = commandText + "VALUES(@IdResolucion,@NombreArchivo,@TipoContenido,@Tamano,@Dato,@UsrCreacion)";
                        cmd.CommandText = commandText;
                        cmd.CommandType = CommandType.Text;
                        //Declaracion de tipos
                        cmd.Parameters.Add("@IdResolucion", SqlDbType.VarChar, 30);
                        cmd.Parameters["@IdResolucion"].Value = str_IdResolucion;
                    }
                    if (!String.IsNullOrEmpty(str_IdExpediente))
                    {
                        string commandText = "INSERT INTO rn.Archivos (IdExpediente,NombreArchivo,TipoContenido,Tamano,Dato,UsrCreacion) ";
                        commandText = commandText + "VALUES(@IdExpediente,@NombreArchivo,@TipoContenido,@Tamano,@Dato,@UsrCreacion)";
                        cmd.CommandText = commandText;
                        cmd.CommandType = CommandType.Text;
                        //Declaracion de tipos
                        cmd.Parameters.Add("@IdExpediente", SqlDbType.VarChar, 30);
                        cmd.Parameters["@IdExpediente"].Value = str_IdExpediente;
                    }
                    if (!String.IsNullOrEmpty(str_IdCobroPago))
                    {
                        string commandText = "INSERT INTO rn.Archivos (IdCobroPago,NombreArchivo,TipoContenido,Tamano,Dato,UsrCreacion) ";
                        commandText = commandText + "VALUES(@IdCobroPago,@NombreArchivo,@TipoContenido,@Tamano,@Dato,@UsrCreacion)";
                        cmd.CommandText = commandText;
                        cmd.CommandType = CommandType.Text;
                        //Declaracion de tipos
                        cmd.Parameters.Add("@IdCobroPago", SqlDbType.VarChar, 30);
                        cmd.Parameters["@IdCobroPago"].Value = str_IdCobroPago;
                    }
                    else if (int_IdRevelacion > 0)
                    {
                        string commandText = "INSERT INTO rn.Archivos (IdRevelacion,NombreArchivo,TipoContenido,Tamano,Dato,UsrCreacion) ";
                        commandText = commandText + "VALUES(@IdRevelacion,@NombreArchivo,@TipoContenido,@Tamano,@Dato,@UsrCreacion)";
                        cmd.CommandText = commandText;
                        cmd.CommandType = CommandType.Text;
                        //Declaracion de tipos
                        cmd.Parameters.Add("@IdRevelacion", SqlDbType.BigInt);
                        cmd.Parameters["@IdRevelacion"].Value = int_IdRevelacion;
                    }
                    else if (lg_IdRevelacionPendiente > 0)
                    {
                        string commandText = "INSERT INTO rn.Archivos (IdRevelacionPendiente,NombreArchivo,TipoContenido,Tamano,Dato,UsrCreacion) ";
                        commandText = commandText + "VALUES(@IdRevelacionPendiente,@NombreArchivo,@TipoContenido,@Tamano,@Dato,@UsrCreacion)";
                        cmd.CommandText = commandText;
                        cmd.CommandType = CommandType.Text;
                        //Declaracion de tipos
                        cmd.Parameters.Add("@IdRevelacionPendiente", SqlDbType.BigInt);
                        cmd.Parameters["@IdRevelacionPendiente"].Value = lg_IdRevelacionPendiente;
                    }
                    else if (int_IdFormulario > 0)//Es archivo de Captura de Ingresos
                    {
                        string commandText = "INSERT INTO rn.Archivos (IdFormulario,Anno,NombreArchivo,TipoContenido,Tamano,Dato,UsrCreacion) ";
                        commandText = commandText + "VALUES(@IdFormulario,@Anno,@NombreArchivo,@TipoContenido,@Tamano,@Dato,@UsrCreacion)";
                        cmd.CommandText = commandText;
                        cmd.CommandType = CommandType.Text;
                        //Declaracion de tipos
                        cmd.Parameters.Add("@IdFormulario", SqlDbType.Int);
                        cmd.Parameters.Add("@Anno", SqlDbType.SmallInt);
                        cmd.Parameters["@IdFormulario"].Value = int_IdFormulario;
                        cmd.Parameters["@Anno"].Value = int_Anno;
                    }
                    //Declaracion de tipos
                    cmd.Parameters.Add("@NombreArchivo", SqlDbType.NVarChar, 100);
                    cmd.Parameters.Add("@TipoContenido", SqlDbType.VarChar, 50);
                    cmd.Parameters.Add("@Tamano", SqlDbType.Int);
                    cmd.Parameters.Add("@Dato", SqlDbType.VarBinary);
                    cmd.Parameters.Add("@UsrCreacion",SqlDbType.VarChar,15);
                    //asignacion de valor
                    cmd.Parameters["@NombreArchivo"].Value = str_nombre;
                    cmd.Parameters["@TipoContenido"].Value = str_tipoContenido;
                    cmd.Parameters["@Tamano"].Value = int_size;
                    cmd.Parameters["@Dato"].Value = b_data;
                    cmd.Parameters["@Tamano"].Value = int_size;
                    cmd.Parameters["@UsrCreacion"].Value = str_userCreacion;
                    //Output paramtros
                    cmd.Parameters.Add("@pResultado", SqlDbType.VarChar, 2);
                    cmd.Parameters["@pResultado"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@pMensaje", SqlDbType.VarChar, 500);
                    cmd.Parameters["@pMensaje"].Direction = ParameterDirection.Output;

                    //Ejecutamos
                    cmd.ExecuteNonQuery();
                    //lstr_resultInsertar[0] = "Insercion satisfactoria de archivo";
                    //asignamos los resultados que vienen de los parametros de salida
                    //lstr_Codigo = (string)cmd.Parameters["@pResultado"].Value;
                    //lstr_Resultado = (string)cmd.Parameters["@pMensaje"].Value;
                    lstr_Codigo = "00";
                    lstr_Resultado = "Inserción satisfactoria de archivo";
                    lstr_resultInsertar[0] = lstr_Codigo;
                    lstr_resultInsertar[1] = lstr_Resultado;

                    //cerramos conexion
                    connection.Close();
                
                }catch(SqlException sqlerr){
                    lstr_resultInsertar[0] = "99"; 
                    lstr_resultInsertar[1] = "Error " + sqlerr;

                }
                
                
                return lstr_resultInsertar;
            }
        }

        // Salvar archivos a BD
        public string[] GuardarArchivoContingente(string str_nombre, string str_tipoContenido,
            int int_size, byte[] b_data, long int_IdRevelacion, string str_IdResolucion, string str_IdSociedadGL, int int_IdFormulario, string str_IdExpediente, long lg_IdRevelacionPendiente, string str_IdCobroPago, Int16 int_Anno, string str_userCreacion)
        {
            string[] lstr_resultInsertar = new string[2];

            using (SqlConnection connection = new SqlConnection())
            {
                try
                {

                    OpenConnection(connection);
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandTimeout = 0;
                    if (!String.IsNullOrEmpty(str_IdExpediente))
                    {
                        string commandText = "INSERT INTO rn.Archivos (IdExpediente,IdSociedadGL,IdResolucion,NombreArchivo,TipoContenido,Tamano,Dato,UsrCreacion,IdFormulario) ";
                        commandText = commandText + "VALUES(@IdExpediente,@IdSociedadGL,@IdResolucion,@NombreArchivo,@TipoContenido,@Tamano,@Dato,@UsrCreacion,@IdFormulario)";
                        cmd.CommandText = commandText;
                        cmd.CommandType = CommandType.Text;
                        //Declaracion de tipos
                        cmd.Parameters.Add("@IdExpediente", SqlDbType.VarChar, 30);
                        cmd.Parameters["@IdExpediente"].Value = str_IdExpediente;

                        cmd.Parameters.Add("@IdSociedadGL", SqlDbType.VarChar, 10);
                        cmd.Parameters["@IdSociedadGL"].Value = str_IdSociedadGL;

                        cmd.Parameters.Add("@IdResolucion", SqlDbType.VarChar, 30);
                        cmd.Parameters["@IdResolucion"].Value = str_IdResolucion;

                        cmd.Parameters.Add("@IdFormulario", SqlDbType.Int); // Es el ID Exp
                        cmd.Parameters["@IdFormulario"].Value = int_IdFormulario;
                    }
                    
                    else if (!String.IsNullOrEmpty(str_IdCobroPago))
                    {
                        string commandText = "INSERT INTO rn.Archivos (IdCobroPago,NombreArchivo,TipoContenido,Tamano,Dato,UsrCreacion) ";
                        commandText = commandText + "VALUES(@IdCobroPago,@NombreArchivo,@TipoContenido,@Tamano,@Dato,@UsrCreacion)";
                        cmd.CommandText = commandText;
                        cmd.CommandType = CommandType.Text;
                        //Declaracion de tipos
                        cmd.Parameters.Add("@IdCobroPago", SqlDbType.VarChar, 30);
                        cmd.Parameters["@IdCobroPago"].Value = str_IdCobroPago;
                    }
                    else if (int_IdRevelacion > 0)
                    {
                        string commandText = "INSERT INTO rn.Archivos (IdRevelacion,NombreArchivo,TipoContenido,Tamano,Dato,UsrCreacion) ";
                        commandText = commandText + "VALUES(@IdRevelacion,@NombreArchivo,@TipoContenido,@Tamano,@Dato,@UsrCreacion)";
                        cmd.CommandText = commandText;
                        cmd.CommandType = CommandType.Text;
                        //Declaracion de tipos
                        cmd.Parameters.Add("@IdRevelacion", SqlDbType.BigInt);
                        cmd.Parameters["@IdRevelacion"].Value = int_IdRevelacion;
                    }
                    else if (lg_IdRevelacionPendiente > 0)
                    {
                        string commandText = "INSERT INTO rn.Archivos (IdRevelacionPendiente,NombreArchivo,TipoContenido,Tamano,Dato,UsrCreacion) ";
                        commandText = commandText + "VALUES(@IdRevelacionPendiente,@NombreArchivo,@TipoContenido,@Tamano,@Dato,@UsrCreacion)";
                        cmd.CommandText = commandText;
                        cmd.CommandType = CommandType.Text;
                        //Declaracion de tipos
                        cmd.Parameters.Add("@IdRevelacionPendiente", SqlDbType.BigInt);
                        cmd.Parameters["@IdRevelacionPendiente"].Value = lg_IdRevelacionPendiente;
                    }
                    else if (int_IdFormulario > 0)//Es archivo de Captura de Ingresos
                    {
                        string commandText = "INSERT INTO rn.Archivos (IdFormulario,Anno,NombreArchivo,TipoContenido,Tamano,Dato,UsrCreacion) ";
                        commandText = commandText + "VALUES(@IdFormulario,@Anno,@NombreArchivo,@TipoContenido,@Tamano,@Dato,@UsrCreacion)";
                        cmd.CommandText = commandText;
                        cmd.CommandType = CommandType.Text;
                        //Declaracion de tipos
                        cmd.Parameters.Add("@IdFormulario", SqlDbType.Int);
                        cmd.Parameters.Add("@Anno", SqlDbType.SmallInt);
                        cmd.Parameters["@IdFormulario"].Value = int_IdRevelacion;
                        cmd.Parameters["@Anno"].Value = int_Anno;
                    }
                    //Declaracion de tipos
                    cmd.Parameters.Add("@NombreArchivo", SqlDbType.NVarChar, 100);
                    cmd.Parameters.Add("@TipoContenido", SqlDbType.VarChar, 50);
                    cmd.Parameters.Add("@Tamano", SqlDbType.Int);
                    cmd.Parameters.Add("@Dato", SqlDbType.VarBinary, -1);
                    cmd.Parameters.Add("@UsrCreacion", SqlDbType.VarChar, 15);
                    //asignacion de valor
                    cmd.Parameters["@NombreArchivo"].Value = str_nombre;
                    cmd.Parameters["@TipoContenido"].Value = str_tipoContenido;
                    cmd.Parameters["@Tamano"].Value = int_size;
                    cmd.Parameters["@Dato"].Value = b_data;
                    cmd.Parameters["@Tamano"].Value = int_size;
                    cmd.Parameters["@UsrCreacion"].Value = str_userCreacion;
                    //Output paramtros
                    cmd.Parameters.Add("@pResultado", SqlDbType.VarChar, 2);
                    cmd.Parameters["@pResultado"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@pMensaje", SqlDbType.VarChar, 500);
                    cmd.Parameters["@pMensaje"].Direction = ParameterDirection.Output;

                    //Ejecutamos
                    cmd.ExecuteNonQuery();
                    //lstr_resultInsertar[0] = "Insercion satisfactoria de archivo";
                    //asignamos los resultados que vienen de los parametros de salida
                    //lstr_Codigo = (string)cmd.Parameters["@pResultado"].Value;
                    //lstr_Resultado = (string)cmd.Parameters["@pMensaje"].Value;
                    lstr_resultInsertar[0] = "00";
                    lstr_resultInsertar[1] = "Inserción satisfactoria de archivo";

                    //cerramos conexion
                    connection.Close();

                }
                catch (SqlException sqlerr)
                {
                    lstr_resultInsertar[0] = "99";
                    lstr_resultInsertar[1] = "Error " + sqlerr;

                }


                return lstr_resultInsertar;
            }
        }

        // Salvar archivos a BD
        public string[] GuardarArchivos(string str_nombre, string str_tipoContenido,
            int int_size, byte[] b_data, long int_IdRevelacion, string str_IdResolucion, int int_IdFormulario, string str_IdExpediente, long lg_IdRevelacionPendiente, long lg_IdArchivoDeuda, string str_IdCobroPago, Int16 int_Anno, string str_userCreacion)
        {
            string lstr_Codigo = string.Empty;
            string lstr_Resultado = string.Empty;
            string[] lstr_resultInsertar = new string[2];

            using (SqlConnection connection = new SqlConnection())
            {
                try
                {

                    OpenConnection(connection);
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandTimeout = 0;
                    if (!String.IsNullOrEmpty(str_IdResolucion))
                    {
                        string commandText = "INSERT INTO rn.Archivos (IdResolucion,NombreArchivo,TipoContenido,Tamano,Dato,UsrCreacion) ";
                        commandText = commandText + "VALUES(@IdResolucion,@NombreArchivo,@TipoContenido,@Tamano,@Dato,@UsrCreacion)";
                        cmd.CommandText = commandText;
                        cmd.CommandType = CommandType.Text;
                        //Declaracion de tipos
                        cmd.Parameters.Add("@IdResolucion", SqlDbType.VarChar, 30);
                        cmd.Parameters["@IdResolucion"].Value = str_IdResolucion;
                    }
                    if (!String.IsNullOrEmpty(str_IdExpediente))
                    {
                        string commandText = "INSERT INTO rn.Archivos (IdExpediente,NombreArchivo,TipoContenido,Tamano,Dato,UsrCreacion) ";
                        commandText = commandText + "VALUES(@IdExpediente,@NombreArchivo,@TipoContenido,@Tamano,@Dato,@UsrCreacion)";
                        cmd.CommandText = commandText;
                        cmd.CommandType = CommandType.Text;
                        //Declaracion de tipos
                        cmd.Parameters.Add("@IdExpediente", SqlDbType.VarChar, 30);
                        cmd.Parameters["@IdExpediente"].Value = str_IdExpediente;
                    }
                    if (!String.IsNullOrEmpty(str_IdCobroPago))
                    {
                        string commandText = "INSERT INTO rn.Archivos (IdCobroPago,NombreArchivo,TipoContenido,Tamano,Dato,UsrCreacion) ";
                        commandText = commandText + "VALUES(@IdCobroPago,@NombreArchivo,@TipoContenido,@Tamano,@Dato,@UsrCreacion)";
                        cmd.CommandText = commandText;
                        cmd.CommandType = CommandType.Text;
                        //Declaracion de tipos
                        cmd.Parameters.Add("@IdCobroPago", SqlDbType.VarChar, 30);
                        cmd.Parameters["@IdCobroPago"].Value = str_IdCobroPago;
                    }
                    else if (int_IdRevelacion > 0)
                    {
                        string commandText = "INSERT INTO rn.Archivos (IdRevelacion,NombreArchivo,TipoContenido,Tamano,Dato,UsrCreacion) ";
                        commandText = commandText + "VALUES(@IdRevelacion,@NombreArchivo,@TipoContenido,@Tamano,@Dato,@UsrCreacion)";
                        cmd.CommandText = commandText;
                        cmd.CommandType = CommandType.Text;
                        //Declaracion de tipos
                        cmd.Parameters.Add("@IdRevelacion", SqlDbType.BigInt);
                        cmd.Parameters["@IdRevelacion"].Value = int_IdRevelacion;
                    }
                    else if (lg_IdRevelacionPendiente > 0)
                    {
                        string commandText = "INSERT INTO rn.Archivos (IdRevelacionPendiente,NombreArchivo,TipoContenido,Tamano,Dato,UsrCreacion) ";
                        commandText = commandText + "VALUES(@IdRevelacionPendiente,@NombreArchivo,@TipoContenido,@Tamano,@Dato,@UsrCreacion)";
                        cmd.CommandText = commandText;
                        cmd.CommandType = CommandType.Text;
                        //Declaracion de tipos
                        cmd.Parameters.Add("@IdRevelacionPendiente", SqlDbType.BigInt);
                        cmd.Parameters["@IdRevelacionPendiente"].Value = lg_IdRevelacionPendiente;
                    }
                    else if (lg_IdArchivoDeuda > 0)
                    {
                        string commandText = "INSERT INTO rn.Archivos (IdArchivoDeuda,NombreArchivo,TipoContenido,Tamano,Dato,UsrCreacion) ";
                        commandText = commandText + "VALUES(@IdArchivoDeuda,@NombreArchivo,@TipoContenido,@Tamano,@Dato,@UsrCreacion)";
                        cmd.CommandText = commandText;
                        cmd.CommandType = CommandType.Text;
                        //Declaracion de tipos
                        cmd.Parameters.Add("@IdArchivoDeuda", SqlDbType.BigInt);
                        cmd.Parameters["@IdArchivoDeuda"].Value = lg_IdArchivoDeuda;
                    }
                    else if (int_IdFormulario > 0)//Es archivo de Captura de Ingresos
                    {
                        string commandText = "INSERT INTO rn.Archivos (IdFormulario,Anno,NombreArchivo,TipoContenido,Tamano,Dato,UsrCreacion) ";
                        commandText = commandText + "VALUES(@IdFormulario,@Anno,@NombreArchivo,@TipoContenido,@Tamano,@Dato,@UsrCreacion)";
                        cmd.CommandText = commandText;
                        cmd.CommandType = CommandType.Text;
                        //Declaracion de tipos
                        cmd.Parameters.Add("@IdFormulario", SqlDbType.Int);
                        cmd.Parameters.Add("@Anno", SqlDbType.SmallInt);
                        cmd.Parameters["@IdFormulario"].Value = int_IdRevelacion;
                        cmd.Parameters["@Anno"].Value = int_Anno;
                    }
                    //Declaracion de tipos
                    cmd.Parameters.Add("@NombreArchivo", SqlDbType.NVarChar, 100);
                    cmd.Parameters.Add("@TipoContenido", SqlDbType.VarChar, 50);
                    cmd.Parameters.Add("@Tamano", SqlDbType.Int);
                    cmd.Parameters.Add("@Dato", SqlDbType.VarBinary);
                    cmd.Parameters.Add("@UsrCreacion", SqlDbType.VarChar, 15);
                    //asignacion de valor
                    cmd.Parameters["@NombreArchivo"].Value = str_nombre;
                    cmd.Parameters["@TipoContenido"].Value = str_tipoContenido;
                    cmd.Parameters["@Tamano"].Value = int_size;
                    cmd.Parameters["@Dato"].Value = b_data;
                    cmd.Parameters["@Tamano"].Value = int_size;
                    cmd.Parameters["@UsrCreacion"].Value = str_userCreacion;
                    //Output paramtros
                    cmd.Parameters.Add("@pResultado", SqlDbType.VarChar, 2);
                    cmd.Parameters["@pResultado"].Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@pMensaje", SqlDbType.VarChar, 500);
                    cmd.Parameters["@pMensaje"].Direction = ParameterDirection.Output;

                    //Ejecutamos
                    cmd.ExecuteNonQuery();
                    //lstr_resultInsertar[0] = "Insercion satisfactoria de archivo";
                    //asignamos los resultados que vienen de los parametros de salida
                    //lstr_Codigo = (string)cmd.Parameters["@pResultado"].Value;
                    //lstr_Resultado = (string)cmd.Parameters["@pMensaje"].Value;
                    lstr_Codigo = "00";
                    lstr_Resultado = "Inserción satisfactoria de archivo";
                    lstr_resultInsertar[0] = lstr_Codigo;
                    lstr_resultInsertar[1] = lstr_Resultado;

                    //cerramos conexion
                    connection.Close();

                }
                catch (SqlException sqlerr)
                {

                    lstr_resultInsertar[0] = "Error " + sqlerr;

                }


                return lstr_resultInsertar;
            }
        }

        // Get a file from the database by ID
        public DataSet ObtenerArchivoPorIdResolucion(string str_IdExpediente, string str_IdSociedad, int int_IdFormulario)
        {
            
             DataSet fileList = new DataSet();
            using (SqlConnection connection = new SqlConnection())
            {
                OpenConnection(connection);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandTimeout = 0;

               // cmd.CommandText = "SELECT IdArchivo,IdResolucion,NombreArchivo,TipoContenido,Tamano,Dato FROM rn.Archivos WHERE IdExpediente='" + str_IdExpediente + "' AND IdSociedadGL='" + str_IdSociedad + "'";
                cmd.CommandText = "SELECT * FROM rn.Archivos WHERE IdExpediente='" + str_IdExpediente + "' AND IdSociedadGL='" + str_IdSociedad + "' AND IdFormulario=" + int_IdFormulario;
                
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter adapter = new SqlDataAdapter();

                adapter.SelectCommand = cmd;
                adapter.Fill(fileList);

                connection.Close();
            }

            return fileList;
        }

        // Get a file from the database by ID
        public DataSet ObtenerArchivoCapturaIngresos(int? int_IdFormulario, Int16? int_Anno, string str_TipoIdPersona = null, string str_IdPersona = null )
        {

            DataSet fileList = new DataSet();
            using (SqlConnection connection = new SqlConnection())
            {
                OpenConnection(connection);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandTimeout = 0;

                string str_TextoIdFormulario;
                string str_TextoAnno;
                string str_TextoTipoIdPersona;
                string str_TextoIdPersona;

                str_TextoIdFormulario = (int_IdFormulario.ToString() == "") ? "null" : int_IdFormulario.ToString();
                str_TextoAnno = (int_Anno.ToString() == "") ? "null" : int_Anno.ToString();
                str_TextoTipoIdPersona = (str_TipoIdPersona == "") ? "null" : str_TipoIdPersona;
                str_TextoIdPersona = (str_IdPersona == "") ? "null" : str_IdPersona;    

                if (int_IdFormulario.ToString() == "")
                    str_TextoIdFormulario = "null";

                cmd.CommandText = "SELECT A.IdFormulario, A.Anno, F.Estado,F.IdMoneda,F.Monto, F.Descripcion, F.IdSociedadGL, F.TipoIdPersona, F.IdPersona, F.TipoIdPersonaTramite, F.IdPersonaTramite, IdArchivo,NombreArchivo,TipoContenido,Tamano,Dato "
                                    + "FROM rn.Archivos A INNER JOIN ci.FormulariosCapturasIngresos F "
                                    + "ON f.IdFormulario = a.IdFormulario "
                                    + "AND f.Anno = a.Anno "
                                    + "WHERE (A.IdFormulario=" + str_TextoIdFormulario + " OR ISNULL(" + str_TextoIdFormulario + ",-1) = -1) "
                                    + "AND (A.Anno=" + str_TextoAnno + " OR ISNULL(" + str_TextoAnno + ",-1) = -1) "
                                    + "AND (f.TipoIdPersona = '" + str_TextoTipoIdPersona + "' OR ISNULL(" + str_TextoTipoIdPersona + ",'' ) = '') "
                                    + "AND (f.IdPersona = '" + str_TextoIdPersona + "' OR ISNULL(" + str_TextoIdPersona + ",'' ) = '') "
                                    + "AND (f.TipoIdPersonaTramite = '" + str_TextoTipoIdPersona + "' OR ISNULL(" + str_TextoTipoIdPersona + ",'' ) = '') "
                                    + "AND (f.IdPersonaTramite = '" + str_TextoIdPersona + "' OR ISNULL(" + str_TextoIdPersona + ",'' ) = '') "
                                    + " ORDER BY IdArchivo Desc";
                
                
                cmd.CommandType = CommandType.Text;
                SqlDataAdapter adapter = new SqlDataAdapter();

                adapter.SelectCommand = cmd;
                adapter.Fill(fileList);

                connection.Close();
            }

            return fileList;
        }

        public DataSet ObtenerArchivoPorIdRevelacion(string int_IdRevelacion)
        {
             DataSet fileList = new DataSet();
             try
             {

                 using (SqlConnection connection = new SqlConnection())
                 {
                     OpenConnection(connection);
                     SqlCommand cmd = new SqlCommand();
                     cmd.Connection = connection;
                     cmd.CommandTimeout = 0;
                    // string str_Consulta = String.Format("SELECT IdArchivo,IdRevelacion,NombreArchivo,TipoContenido,Tamano,Dato,FchCreacion FROM rn.Archivos WHERE IdRevelacion={0}", int_IdRevelacion);
                     string str_Consulta = String.Format("SELECT * FROM rn.Archivos WHERE IdRevelacion={0}", int_IdRevelacion);
                     
                     cmd.CommandText = str_Consulta;
                     cmd.CommandType = CommandType.Text;
                     SqlDataAdapter adapter = new SqlDataAdapter();

                     adapter.SelectCommand = cmd;
                     adapter.Fill(fileList);

                     connection.Close();
                 }
             }
            catch(Exception ex)
             { }

            return fileList;
        }

        public DataSet ObtenerArchivoPorIdRevelacionPendiente(string int_IdRevelacionPendiente)
        {
            DataSet fileList = new DataSet();
            try
            {

                using (SqlConnection connection = new SqlConnection())
                {
                    OpenConnection(connection);
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandTimeout = 0;
                    string str_Consulta = String.Format("SELECT IdArchivo,IdRevelacionPendiente,NombreArchivo,TipoContenido,Tamano,Dato,FchCreacion FROM rn.Archivos WHERE IdRevelacionPendiente={0}", int_IdRevelacionPendiente);
                    cmd.CommandText = str_Consulta;
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter adapter = new SqlDataAdapter();

                    adapter.SelectCommand = cmd;
                    adapter.Fill(fileList);

                    connection.Close();
                }
            }
            catch (Exception ex)
            { }

            return fileList;
        }

        public DataSet ObtenerArchivoPorIdArchivo(string int_IdArchivo)
        {
            DataSet fileList = new DataSet();
            try
            {

                using (SqlConnection connection = new SqlConnection())
                {
                    OpenConnectionNoWS(connection);
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandTimeout = 0;
                    string str_Consulta = String.Format("SELECT IdArchivo,IdRevelacion,NombreArchivo,TipoContenido,Tamano,Dato,FchCreacion FROM rn.Archivos WHERE IdArchivo={0}", int_IdArchivo);
                    cmd.CommandText = str_Consulta;
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter adapter = new SqlDataAdapter();

                    adapter.SelectCommand = cmd;
                    adapter.Fill(fileList);

                    connection.Close();
                }
            }
            catch (Exception ex)
            { }

            return fileList;
        }

        public DataSet ObtenerArchivoPorIdArchivoDeuda(string int_IdArchivoDeuda)
        {
            DataSet fileList = new DataSet();
            try
            {

                using (SqlConnection connection = new SqlConnection())
                {
                    OpenConnection(connection);
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = connection;
                    cmd.CommandTimeout = 0;
                    string str_Consulta = String.Format("SELECT IdArchivo,IdRevelacion,NombreArchivo,TipoContenido,Tamano,Dato,FchCreacion FROM rn.Archivos WHERE IdArchivoDeuda={0}", int_IdArchivoDeuda);
                    cmd.CommandText = str_Consulta;
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter adapter = new SqlDataAdapter();

                    adapter.SelectCommand = cmd;
                    adapter.Fill(fileList);

                    connection.Close();
                }
            }
            catch (Exception ex)
            {  }

            return fileList;
        }
    }
}