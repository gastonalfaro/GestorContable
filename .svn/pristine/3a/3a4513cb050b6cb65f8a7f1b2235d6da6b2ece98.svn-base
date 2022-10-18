using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Globalization;
using System.Web.UI.HtmlControls;
using Presentacion.Compartidas;
using LogicaNegocio.Seguridad;
using System.IO;

namespace Presentacion.Mantenimiento
{
    public partial class frmIncluirDatos : BASE
    {
        # region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();


        private string gstr_Usuario = String.Empty;
        //private static DataTable[] ldat_TiposAsiento;
        protected DataTable[] ldat_TiposAsiento
        {
            get
            {
                if (ViewState["ldat_TiposAsiento"] == null)
                    ViewState["ldat_TiposAsiento"] = null;
                return (DataTable[])ViewState["ldat_TiposAsiento"];
            }
            set
            {
                ViewState["ldat_TiposAsiento"] = value;
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
                        if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmCargarTiposAsientos"))
                            Response.Redirect("~/Principal.aspx", true);
                        else
                        {
                            btnCargarInfo.Visible = false;
                            lblMensaje.Visible = false;
                            hlDescargarFormato.NavigateUrl = "../Mantenimiento/ArchivosMantenimiento/Plantillas/TiposDeAsiento_Plantilla.xlsx";
                            //hlDescargarFormato.ToolTip = "Nota Importante: Al descargar el archivo, reemplace (en el nombre del archivo) la palabra plantilla, por su número de cédula. Incluya ceros sin espacios. Ej: TiposDeAsiento_0109990888";
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

        public static DataTable[] exceldata(string filePath)
        {
            DataTable[] dtexcel = new DataTable[0];
            DataTable[] dtexcel2 = new DataTable[0];
            
            // = new DataTable();
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
            System.Data.DataRow[] dr_Rows = schemaTable.Select("TABLE_NAME NOT LIKE '_xlnm#_FilterDatabase'");
            int schemaCount = dr_Rows.Length;// schemaTable.Rows.Count;
            dtexcel = new DataTable[schemaCount];

            try{
                int i = 0;
                foreach (DataRow schemaRow in dr_Rows)//schemaTable.Rows)
                {
                    //DataRow schemaRow = schemaTable.Rows[0];
                    string sheet = schemaRow["TABLE_NAME"].ToString();
                    if (!sheet.EndsWith("_")&&!sheet.StartsWith("_"))
                    {
                        string query = "SELECT  * FROM [" + sheet + "]";
                        OleDbDataAdapter daexcel = new OleDbDataAdapter(query, conn);
                        dtexcel[i] = new DataTable();
                        dtexcel[i].Locale = CultureInfo.CurrentCulture;
                        daexcel.Fill(dtexcel[i]);
                        i++;
                    }
                }
                //Looping a first Sheet of Xl File
                /*dtexcel2 = new DataTable[i];
                for (int y = 0; y < i; y++ )
                {
                    dtexcel2[y] = dtexcel[y];
                }*/
                conn.Close();                
            }
            catch (Exception ex)
            {
                //ex.ToString();
                tSeguridad Seguridad = new tSeguridad();
                Seguridad.SaveError(new System.Exception(clsSesion.Current.NomUsuario.ToString() + Environment.NewLine, ex));
                conn.Close();
            }
          
            return dtexcel;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                wsSG.wsSistemaGestor wsSistemaGestor = new wsSG.wsSistemaGestor();
                string mensaje = String.Empty;
                bool lbol_control = false;
                int lint_Errores = 0;
                int fila = 0;
                txtError.Text = "";

                if (ValidaTiras(ldat_TiposAsiento))
                {
                    for (int hoja = 0; hoja < ldat_TiposAsiento.Length; hoja++)
                    {
                        string[] a;
                        fila = 0;
                        //for (int i = 0; i < ldat_TiposAsiento[j].Rows.Count; i++)
                        foreach (DataRow dr_TipoAsiento in ldat_TiposAsiento[hoja].Rows)
                        {
                            fila++;
                            if (dr_TipoAsiento["IdModulo"].ToString().Replace(@"[^\w\.@-]", "").Trim() != "" && dr_TipoAsiento["IdClaveContable"].ToString().Replace(@"[^\w\.@-]", "").Trim() != "")
                            {
                                a = wsSistemaGestor.uwsCrearTipoAsiento(
                                    dr_TipoAsiento["IdModulo"].ToString().Replace(@"[^\w\.@-]", "").Trim(),
                                    dr_TipoAsiento["IdOperacion"].ToString().Replace(@"[^\w\.@-]", "").Trim(),
                                    dr_TipoAsiento["Codigo"].ToString().Replace(@"[^\w\.@-]", "").Trim(),
                                    dr_TipoAsiento["CodigoAuxiliar"].ToString().Replace(@"[^\w\.@-]", "").Trim(),
                                    dr_TipoAsiento["CodigoAuxiliar2"].ToString().Replace(@"[^\w\.@-]", "").Trim(),
                                    dr_TipoAsiento["CodigoAuxiliar3"].ToString().Replace(@"[^\w\.@-]", "").Trim(),
                                    dr_TipoAsiento["CodigoAuxiliar4"].ToString().Replace(@"[^\w\.@-]", "").Trim(),
                                    dr_TipoAsiento["IdClaveContable"].ToString().Replace(@"[^\w\.@-]", "").Trim(),
                                    dr_TipoAsiento["IdCuentaContable"].ToString().Replace(@"[^\w\.@-]", "").Trim(),
                                    dr_TipoAsiento["IdCentroCosto"].ToString().Replace(@"[^\w\.@-]", "").Trim(),
                                    dr_TipoAsiento["IdCentroBeneficio"].ToString().Replace(@"[^\w\.@-]", "").Trim(),
                                    dr_TipoAsiento["IdElementoPEP"].ToString().Replace(@"[^\w\.@-]", "").Trim(),
                                    dr_TipoAsiento["IdPosPre"].ToString().Replace(@"[^\w\.@-]", "").Trim(),
                                    dr_TipoAsiento["IdCentroGestor"].ToString().Replace(@"[^\w\.@-]", "").Trim(),
                                    dr_TipoAsiento["IdPrograma"].ToString().Replace(@"[^\w\.@-]", "").Trim(),
                                    dr_TipoAsiento["IdFondo"].ToString().Replace(@"[^\w\.@-]", "").Trim(),
                                    dr_TipoAsiento["DocPresupuestario"].ToString().Replace(@"[^\w\.@-]", "").Trim(),
                                    dr_TipoAsiento["PosDocPresupuestario"].ToString().Replace(@"[^\w\.@-]", "").Trim(),
                                    dr_TipoAsiento["FlujoEfectivo"].ToString().Replace(@"[^\w\.@-]", "").Trim(),
                                    dr_TipoAsiento["NICSP24"].ToString().Replace(@"[^\w\.@-]", "").Trim(),
                                    dr_TipoAsiento["IdClaveContable2"].ToString().Replace(@"[^\w\.@-]", "").Trim(),
                                    dr_TipoAsiento["IdCuentaContable2"].ToString().Replace(@"[^\w\.@-]", "").Trim(),
                                    dr_TipoAsiento["IdCentroCosto2"].ToString().Replace(@"[^\w\.@-]", "").Trim(),
                                    dr_TipoAsiento["IdCentroBeneficio2"].ToString().Replace(@"[^\w\.@-]", "").Trim(),
                                    dr_TipoAsiento["IdElementoPEP2"].ToString().Replace(@"[^\w\.@-]", "").Trim(),
                                    dr_TipoAsiento["IdPosPre2"].ToString().Replace(@"[^\w\.@-]", "").Trim(),
                                    dr_TipoAsiento["IdCentroGestor2"].ToString().Replace(@"[^\w\.@-]", "").Trim(),
                                    dr_TipoAsiento["IdPrograma2"].ToString().Replace(@"[^\w\.@-]", "").Trim(),
                                    dr_TipoAsiento["IdFondo2"].ToString().Replace(@"[^\w\.@-]", "").Trim(),
                                    dr_TipoAsiento["DocPresupuestario2"].ToString().Replace(@"[^\w\.@-]", "").Trim(),
                                    dr_TipoAsiento["PosDocPresupuestario2"].ToString().Replace(@"[^\w\.@-]", "").Trim(),
                                    dr_TipoAsiento["FlujoEfectivo2"].ToString().Replace(@"[^\w\.@-]", "").Trim(),
                                    dr_TipoAsiento["NICSP242"].ToString().Replace(@"[^\w\.@-]", "").Trim(),
                                    (string.IsNullOrEmpty(dr_TipoAsiento["Estado"].ToString())) ? "A" : dr_TipoAsiento["Estado"].ToString(),//,
                                    gstr_Usuario,
                                    dr_TipoAsiento["CodigoAuxiliar5"].ToString().Replace(@"[^\w\.@-]", "").Trim(),
                                    dr_TipoAsiento["CodigoAuxiliar6"].ToString().Replace(@"[^\w\.@-]", "").Trim(),
                                    dr_TipoAsiento["Secuencia"].ToString().Replace(@"[^\w\.@-]", "").Trim());
                                if (a[0].ToString() == "False")
                                {
                                    lint_Errores++;
                                }

                                txtError.Text = txtError.Text + "\r\n Hoja " + hoja.ToString() + " fila " + fila.ToString() + " " + a[1].ToString();
                                lbol_control = true;
                            }
                        }
                    }
                    if (lbol_control)
                    {
                        if (lint_Errores == 0)
                        {
                            string script = @"<script type='text/javascript'> alert('Carga de archivo exitosa.\r\n Se cargaron " + fila + " filas. '); </script>";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                        }
                        else
                        {
                            string script = @"<script type='text/javascript'> alert('Carga de archivo con errores. '); </script>";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                        }
                    }
                    else
                    {
                        string script = @"<script type='text/javascript'> alert('La carga no se llevó a cabo, por favor, revise el documento e intente nuevamente.'); </script>";
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                    }
                    btnCargarInfo.Visible = false;
                }//if valida
                else 
                {
                    string script = @"<script type='text/javascript'> alert('Carga de archivo con errores. Las IdCuentaContable no pueden ir vacías, si el campo está lleno en el excel asigne formato Texto a estas celdas. '); </script>";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                }//fin
            }
            catch (Exception ex)
            {
                ex.ToString();
                string script = @"<script type='text/javascript'> alert('El archivo presenta inconsistencias, revise e intente de nuevo.'); </script>";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
            }
        }

        /// <summary>
        /// Valida si los datos de cuentas contables están nulos
        /// </summary>
        public bool ValidaTiras(DataTable[] dtDatos) 
        {
            bool valida = true;
            DataTable table = new DataTable();

            for (int hoja = 0; hoja < dtDatos.Length; hoja++)
            {
                //// Presuming the DataTable has a column named Date.
                string expression;
                expression = "IdCuentaContable IS NULL";
                DataRow[] foundRows;
                table = dtDatos[hoja];
                // Use the Select method to find all rows matching the filter.
                foundRows = table.Select(expression);
                if (foundRows.Count() > 0) 
                {
                    valida = false;
                    break;
                }//if
            }//for
            return valida;
        }//fin

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            txtError.Text = "";
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
                      
                        ldat_TiposAsiento = exceldata(lstr_Directorio);

                        if (ldat_TiposAsiento[0].Rows.Count == 0)
                        {
                            string script = @"<script type='text/javascript'> alert('El archivo no contiene registros.'); </script>";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                        }
                        else
                        {
                            string script = @"<script type='text/javascript'> alert('Archivo cargado y listo para procesar.'); </script>";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);



                            grvCCSS.DataSource = ldat_TiposAsiento;
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
                   string script = @"<script type='text/javascript'> alert('El archivo no pudo ser cargado.'); </script>";
                   ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                   tSeguridad Seguridad = new tSeguridad();
                   Seguridad.SaveError(new System.Exception(clsSesion.Current.NomUsuario.ToString() + Environment.NewLine, ex));
                   txtError.Text = txtError.Text + "\n" + ex.ToString();
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
            grvCCSS.DataSource = ldat_TiposAsiento;
            grvCCSS.DataBind();
        }

        protected void btnDevengo_Click(object sender, EventArgs e)
        {
            wsDeudaInterna.wsDeudaInterna wsDeudaInterna = new wsDeudaInterna.wsDeudaInterna();
            //wsDeudaInterna.CalculaDevengoValores();
        }

    }
}