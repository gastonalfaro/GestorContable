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

namespace Presentacion.Contingentes
{
    public partial class ExpedientesReportes : BASE
    {
        # region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private string gstr_Usuario = String.Empty;
        //private static DataTable ldat_ExpedienteReporte = new DataTable();
        protected DataTable ldat_ExpedienteReporte
        {
            get
            {
                if (ViewState["ldat_ExpedienteReporte"] == null)
                    ViewState["ldat_ExpedienteReporte"] = new DataTable();
                return (DataTable)ViewState["ldat_ExpedienteReporte"];
            }
            set
            {
                ViewState["ldat_ExpedienteReporte"] = value;
            }
        }
        private string gstr_ModuloActual = String.Empty;
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
                        if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "ExpedientesReportes"))
                            Response.Redirect("~/Principal.aspx", true);
                        else
                        {
                            btnCargarInfo.Visible = false;
                            hlDescargarFormato.NavigateUrl = "../Contingentes/ArchivosCO/Plantillas/Reporte_Poder_Judicial_Plantilla.xlsx";
                            //hlDescargarFormato.ToolTip = "Nota Importante: Al descargar el archivo, reemplace (en el nombre del archivo) la palabra plantilla, por su número de cédula. Incluya ceros sin espacios. Ej: CCSS_0109990888";
                    
                        }
                    }
                    else
                        Response.Redirect("~/Login.aspx", true);
                }
                else
                {

                    if (String.IsNullOrEmpty(gstr_Usuario))
                    {
                        MessageBox.Show("Sesión de usuario finalizó.");
                        Response.Redirect("~/Login.aspx", true);
                    }
                }
            }
            catch (Exception ex)
            {
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
            try
            {
                wsSG.wsSistemaGestor wsSistemaGestor = new wsSG.wsSistemaGestor();
                string mensaje = String.Empty;
                bool lbol_control = false;

                for (int i = 0; i < ldat_ExpedienteReporte.Rows.Count; i++)
                {
                    wsSistemaGestor.RegistrarExpedienteReporte(
                        ldat_ExpedienteReporte.Rows[i]["Número de expediente"].ToString(),
                        ldat_ExpedienteReporte.Rows[i]["Ministerio"].ToString(),
                        ldat_ExpedienteReporte.Rows[i]["Nombre demandado"].ToString(),
                        Convert.ToDecimal(ldat_ExpedienteReporte.Rows[i]["Monto principal"].ToString()),
                        Convert.ToDecimal(ldat_ExpedienteReporte.Rows[i]["Monto intereses"].ToString()),
                        Convert.ToDecimal(ldat_ExpedienteReporte.Rows[i]["Monto costas"].ToString()),
                        Convert.ToDecimal(ldat_ExpedienteReporte.Rows[i]["Monto intereses moratorios"].ToString()),
                        Convert.ToDecimal(ldat_ExpedienteReporte.Rows[i]["Monto daños y perjuicios"].ToString()),
                        gstr_Usuario);
                    lbol_control = true;
                }
                if (lbol_control)
                {
                    if (mensaje == String.Empty)
                    {
                        string script = @"<script type='text/javascript'> alert('Carga de archivo exitosa.'); </script>";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                    }
                    else
                    {
                        string script = @"<script type='text/javascript'> alert('Carga de archivo parcial. Los siguientes valores no fueron cargados: " + mensaje + "por nemotécnicos inexistentes.'); </script>";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                    }
                }
                else
                {
                    string script = @"<script type='text/javascript'> alert('La carga no se llevó a cabo, por favor, revise el documento e intente nuevamente.'); </script>";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                }
                btnCargarInfo.Visible = false;
            }
            catch (Exception ex)
            {
                ex.ToString();
                string script = @"<script type='text/javascript'> alert('El archivo presenta inconsistencias, revise e intente de nuevo.'); </script>";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
            }
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (fucCargaArchivo.HasFile)
            {
                string lstr_Directorio = String.Empty;
                try
                {
                    string lstr_NbrArchivo = Path.GetFileName(fucCargaArchivo.FileName);
                    //if (lstr_NbrArchivo == "Titulos_" + gstr_Usuario + ".xlsx")
                    //{
                    fucCargaArchivo.SaveAs(Server.MapPath("~/Mantenimiento/ArchivosMantenimiento/") + lstr_NbrArchivo);
                    lstr_Directorio = Server.MapPath("~/Mantenimiento/ArchivosMantenimiento/") + lstr_NbrArchivo;
                    ldat_ExpedienteReporte = exceldata(lstr_Directorio);

                    if (ldat_ExpedienteReporte.Rows.Count == 0)
                    {
                        string script = @"<script type='text/javascript'> alert('El archivo no contiene registros.'); </script>";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                    }
                    else
                    {
                        string script = @"<script type='text/javascript'> alert('Archivo cargado y listo para procesar.'); </script>";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);

                        grvCCSS.DataSource = ldat_ExpedienteReporte;
                        grvCCSS.DataBind();
                        btnCargarInfo.Visible = true;
                    }
                    //}
                    //else
                    //{
                    //    string script = @"<script type='text/javascript'> alert('El archivo no pudo ser cargado. Error: El archivo no es el correcto.'); </script>";
                    //    ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                    //}
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
            grvCCSS.DataSource = ldat_ExpedienteReporte;
            grvCCSS.DataBind();
        }
    }
}