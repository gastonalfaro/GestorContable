using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Logica.SubirArchivo;
using log4net;
using Presentacion.Compartidas;
using System.Configuration;
//using System.Data.SqlClient;

namespace Presentacion.Contingentes
{
    public partial class ObtenerArchivo : BASE
    {

        //Logg4Net Botacoreo
        // private static readonly ILog Log = LogManager.GetLogger("Contingentes");
        private readonly ILog Log = LogManager.GetLogger("Contingentes");
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            string flag = Request.QueryString["Flag"];
            if (!String.IsNullOrEmpty(flag))
            {
                if (flag.Equals("Pretension"))
                {

                    CargarArchivos2();

                } if (flag.Equals("Liquidacion"))
                {
                    CargarArchivos2();

                }
                else
                {
                    CargarArchivos();
                }
            }
            else
            {
                CargarArchivos();
            }
          
        }

        private void CargarArchivos2()
        {
            // Get the file id from the query string
            
            string idArch = Request.QueryString["id"];
            clsArchivoSubir utilidad = new clsArchivoSubir();


            // Get the file from the database
            string consulta = "Select * from rn.Archivos where IdArchivo ='" + idArch + "'";
            DataTable dt = GetData(consulta);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                Log.Info("Info >>>Datos a procesar " + (string)row["NombreArchivo"].ToString() + (string)row["TipoContenido"].ToString());

                string name = (string)row["NombreArchivo"];
                string contentType = (string)row["TipoContenido"];
                Byte[] data = (Byte[])row["Dato"];

                // Send the file to the browser
                Response.AddHeader("Content-type", contentType);
                Response.AddHeader("Content-Disposition", "attachment; filename=" + name);
                Response.BinaryWrite(data);//BinaryWrite
                Response.Flush();
                Response.End();
            }
            else
            {

                Log.Error("Tabla no posee datos al realiar la consulta.");
            }


        }
        private DataTable GetData(string lstr_query)
        {
            /*string lstr_ConnString = ConfigurationManager.ConnectionStrings["GestNICSPDEVConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(lstr_ConnString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = lstr_query;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataSet ds = new DataSet())
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }*/
            DataSet ds = new DataSet();
            ds = ws_SGService.uwsConsultarDinamico(lstr_query);
            if (ds.Tables.Count > 0)
            {
                return ds.Tables["Table"];
            }
            return null;
        }
        private void CargarArchivos()
        {
            // Get the file id from the query string
            string id = Request.QueryString["IdResolucion"];
            String str_IdSociedad = clsSesion.Current.SociedadUsr;
            clsArchivoSubir utilidad = new clsArchivoSubir();
 
        // Get the file from the database
            //DataSet file = utilidad.ufnObtenerArchivoPorIdResolucion(id);
            DataSet file = ws_SGService.uwsObtenerArchivoPorIdResolucion(id, str_IdSociedad, ConsultarIdExpediente(id));

            DataRow row = (file.Tables.Count > 0) ? file.Tables[0].Rows[0] : file.Tables[0].Rows[0];
            Log.Info("Info >>>ufnObtenerArchivoPorIdResolucion " + (string)row["NombreArchivo"].ToString() + (string)row["TipoContenido"].ToString());
                    
                
            string name = (string)row["NombreArchivo"];
            string contentType = (string)row["TipoContenido"];
            Byte[] data = (Byte[])row["Dato"];

            // Send the file to the browser
            Response.AddHeader("Content-type", contentType);
            Response.AddHeader("Content-Disposition", "attachment; filename=" + name);
            Response.BinaryWrite(data);//BinaryWrite
            Response.Flush();
            Response.End();
           

        }
        
        private int ConsultarIdExpediente(string idexpediente)
        {
            string str_consul = "SELECT IdExp FROM co.Expedientes where IdExpediente='" + idexpediente + "' and IdSociedadGL='" + clsSesion.Current.SociedadUsr + "' AND EstadoExpediente = 'Activo'";
            int IdExp = 0;
            //Consultar ID Expedientes 
            DataTable exped = GetData(str_consul);
            if (exped.Rows.Count > 0)
            {
                DataRow campo = exped.Rows[0];
                IdExp = Convert.ToInt32(campo["IdExp"].ToString());
            }

            return IdExp;
        }
    }
}