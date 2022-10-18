using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.CalculosFinancieros.DeudaInterna;
using System.Data.OleDb;
using System.Globalization;
using System.Web.UI.HtmlControls;
using Presentacion.Compartidas;
using System.IO;
using System.Threading;

namespace Presentacion.CalculosFinancieros.DeudaInterna
{
    public partial class frmCancelacionAnticipada : BASE
    {
        # region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private string gstr_Usuario = String.Empty;
        //private static DataTable ldat_TitulosValores = new DataTable();
        protected DataTable ldat_TitulosValores
        {
            get
            {
                if (ViewState["ldat_TitulosValores"] == null)
                    ViewState["ldat_TitulosValores"] = new DataTable();
                return (DataTable)ViewState["ldat_TitulosValores"];
            }
            set
            {
                ViewState["ldat_TitulosValores"] = value;
            }
        }
        private string gstr_ModuloActual = String.Empty;
        private wsAsientos.ServicioContable asientos = new wsAsientos.ServicioContable();

        # endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                gstr_Usuario = clsSesion.Current.LoginUsuario;

                if (!IsPostBack)
                {

                    if (!string.IsNullOrEmpty(gstr_Usuario))
                    {
                        if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmCancelacionAnticipada"))
                            Response.Redirect("~/Principal.aspx", true);
                        else
                        {
                            btnCargarInfo.Visible = false;
                            lblMensaje.Visible = false;
                            hlDescargarFormato.NavigateUrl = "../DeudaInterna/ArchivosDI/Plantillas/Anticipadas_Plantilla.xlsx";
                            hlDescargarFormato.ToolTip = "Nota Importante: Al descargar el archivo, reemplace (en el nombre del archivo) la palabra plantilla, por su número de cédula. Incluya ceros sin espacios. Ej: Anticipadas_0109990888";
                        }
                    }
                    else
                        Response.Redirect("~/Login.aspx", true);
                }
            }
            catch (Exception ex)
            {
                lblEstatus.Text = ex.ToString();
                //Response.Redirect("~/Login.aspx", true);
            }
        }

        public static DataTable exceldata(string filePath)
        {
            DataTable dtexcel = new DataTable();
            bool hasHeaders = false;
            string HDR = hasHeaders ? "Yes" : "No";
            string strConn;
            if (filePath.Substring(filePath.LastIndexOf('.')).ToLower() == ".xlsx")
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=0\"";
            else
                strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=0\"";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            //Looping Total Sheet of Xl File
            /*foreach (DataRow schemaRow in schemaTable.Rows)
            {
            }*/
            //Looping a first Sheet of Xl File
            DataRow schemaRow = schemaTable.Rows[0];
            string sheet = schemaRow["TABLE_NAME"].ToString();
            if (!sheet.EndsWith("_"))
            {
                string query = "SELECT  * FROM ["+sheet+"]";
                OleDbDataAdapter daexcel = new OleDbDataAdapter(query, conn);
                dtexcel.Locale = CultureInfo.CurrentCulture;
                daexcel.Fill(dtexcel);
            }

            conn.Close();
            return dtexcel;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Thread.Sleep(2000);
            string script = @"<script type='text/javascript'> alert('Carga de archivo exitosa. - '); </script>";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
            btnCargarInfo.Visible = false;
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (fucCargaArchivo.HasFile)
            {
                string lstr_Directorio = String.Empty;
                try
                {
                    string lstr_NbrArchivo = Path.GetFileName(fucCargaArchivo.FileName);
                    if (lstr_NbrArchivo == "Anticipadas_" + gstr_Usuario + ".xlsx")
                    {
                        fucCargaArchivo.SaveAs(Server.MapPath("~/CalculosFinancieros/DeudaInterna/ArchivosDI/") + lstr_NbrArchivo);
                        lstr_Directorio = Server.MapPath("~/CalculosFinancieros/DeudaInterna/ArchivosDI/") + lstr_NbrArchivo;
                        ldat_TitulosValores = exceldata(lstr_Directorio);

                        if (ldat_TitulosValores.Rows.Count == 0)
                        {
                            string script = @"<script type='text/javascript'> alert('El archivo no contiene registros.'); </script>";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                        }
                        else
                        {
                            string script = @"<script type='text/javascript'> alert('Archivo cargado y listo para procesar.'); </script>";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);

                            grvCCSS.DataSource = ldat_TitulosValores;
                            grvCCSS.DataBind();
                            btnCargarInfo.Visible = true;
                        }
                    }
                    else
                    {
                        string script = @"<script type='text/javascript'> alert('El archivo no pudo ser cargado. Error: El archivo no es el correcto.'); </script>";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                    }
                }
                catch (Exception ex)
                {
                    string script = @"<script type='text/javascript'> alert('El archivo no pudo ser cargado. Error: " + ex.ToString() + "'); </script>";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                }
            }
            else
            {
                string script = @"<script type='text/javascript'> alert('No se ha seleccionado ningun archivo, por favor, seleccione un archivo para continuar.'); </script>";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
            }
        }

        protected void grvCCSS_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            grvCCSS.PageIndex = e.NewPageIndex;
            grvCCSS.DataSource = ldat_TitulosValores;
            grvCCSS.DataBind();
        }

        protected void btnDevengo_Click(object sender, EventArgs e)
        {
            Presentacion.wsDI.wsDeudaInterna wsDeudaInterna = new Presentacion.wsDI.wsDeudaInterna();
            //wsDeudaInterna.CalculaDevengoValores();
        }

        protected void btnContabilizar_Click(object sender, EventArgs e)
        {
            Presentacion.wsDI.wsDeudaInterna vWebServiceCalculosFinancieros = new Presentacion.wsDI.wsDeudaInterna();
            String result = "El proceso de contabilización ha terminado. Consulte su bitácora para ver más detalles.";
            try{

                foreach (DataRow dr in ldat_TitulosValores.Rows)
                {
                    String ws_result = vWebServiceCalculosFinancieros.ContabilizarCancelacionAnticipada(
                        dr["Nemotecnico"].ToString().Trim(),
                        Convert.ToDecimal("0" + dr["NumeroValor"].ToString().Trim()),
                        dr["FechaCancelacion"].ToString().Trim().Substring(0,10),
                        Convert.ToDecimal("0" + dr["ValorFacial"].ToString().Trim()),
                        Convert.ToDecimal("0" + dr["TransadoBruto"].ToString().Trim()),
                        Convert.ToDecimal("0" + dr["RendimientoPorDescuento"].ToString().Trim()),
                        Convert.ToDecimal("0" + dr["Premio"].ToString().Trim()),
                        Convert.ToDecimal("0" + dr["ImpuestoPagado"].ToString().Trim())
                        );
                }
            }
            catch(Exception ex)
            {
                result = "Error: " + ex.Message;
            }
            result = result.Replace("'", "\\'");
            string script = @"<script type='text/javascript'> alert('" + result + "'); </script>";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
        }
    }
}