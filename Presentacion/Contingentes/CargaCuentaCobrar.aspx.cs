using Presentacion.Compartidas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Office.Interop.Excel;

namespace Presentacion.Contingentes
{
    public partial class CargaCuentaCobrar : BASE
    {
        # region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private string gstr_Usuario = String.Empty;
        //private static System.Data.DataTable ldat_Expedientes = new System.Data.DataTable();
        protected System.Data.DataTable ldat_Expedientes
        {
            get
            {
                if (ViewState["ldat_Expedientes"] == null)
                    ViewState["ldat_Expedientes"] = null;
                return (System.Data.DataTable)ViewState["ldat_Expedientes"];
            }
            set
            {
                ViewState["ldat_Expedientes"] = value;
            }
        }
        private string gstr_ModuloActual = String.Empty;
        # endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                txtError.Text = "";
                txtError.Visible = false;
                gstr_Usuario = clsSesion.Current.LoginUsuario;

                if (!IsPostBack)
                {
                    if (!string.IsNullOrEmpty(gstr_Usuario))
                    {
                        if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "OBJ_CT"))
                            Response.Redirect("~/Principal.aspx", true);
                        else
                        {
                            btnCargarInfo.Visible = false;
                            lblMensaje.Visible = false;
                            hlDescargarFormato.NavigateUrl = "../Contingentes/ArchivosContingentes/Plantillas/Reporte_Poder_Judicial.xlsx";
// hlDescargarFormato.ToolTip = "Nota Importante: Al descargar el archivo, reemplace (en el nombre del archivo) la palabra plantilla, por su número de cédula. Incluya ceros sin espacios. Ej: TiposDeAsiento_0109990888";

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

        public System.Data.DataTable Import(String path)
        {
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workBook = app.Workbooks.Open(path, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            Microsoft.Office.Interop.Excel.Worksheet workSheet = (Microsoft.Office.Interop.Excel.Worksheet)workBook.ActiveSheet;

            int index = 0;
            object rowIndex = 2;

            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("Número de expediente");
            dt.Columns.Add("Ministerio");
            dt.Columns.Add("Código");
            dt.Columns.Add("Nombre"); 
            dt.Columns.Add("Moneda");
            dt.Columns.Add("Monto principal");
            dt.Columns.Add("Monto intereses");
            dt.Columns.Add("Monto costas");
            dt.Columns.Add("Monto intereses moratorios");
            dt.Columns.Add("Monto daños y perjuicios");

            DataRow row;

            rowIndex = 2 + index;
            while (!string.IsNullOrEmpty(Convert.ToString(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 1]).Value2) ))
            {
                row = dt.NewRow();
                row[0] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 1]).Value2);
                row[1] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 2]).Value2);
                row[2] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 3]).Value2);
                row[3] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 4]).Value2);
                row[4] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 5]).Value2);
                row[5] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 6]).Value2);
                row[6] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 7]).Value2);
                row[7] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 8]).Value2);
                row[8] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 9]).Value2);
                row[9] = Convert.ToString(((Microsoft.Office.Interop.Excel.Range)workSheet.Cells[rowIndex, 10]).Value2);
                index++;
                rowIndex = 2 + index;
                dt.Rows.Add(row);
            }
            app.Workbooks.Close();
            return dt;
        }

        public static System.Data.DataTable exceldata(string filePath)
        {
            System.Data.DataTable dtexcel = new System.Data.DataTable();
            DataRow[] arr_DrExcel;
            bool hasHeaders = false;
            string HDR = hasHeaders ? "Yes" : "No";
            string strConn;
            if (filePath.Substring(filePath.LastIndexOf('.')).ToLower() == ".xlsx")
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=0\"";
            else
                strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=0\"";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            System.Data.DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            //Looping Total Sheet of Xl File
            /*foreach (DataRow schemaRow in schemaTable.Rows)
            {
            }*/
            //Looping a first Sheet of Xl File
            DataRow schemaRow = schemaTable.Select("TABLE_NAME = 'Expedientes$'")[0];// .Rows[1];
            string sheet = schemaRow["TABLE_NAME"].ToString();
            if (!sheet.EndsWith("_") && !sheet.StartsWith("_"))
            {
                try { 
                string query = "SELECT  * FROM [" + sheet + "] WHERE Moneda <>''";
                OleDbDataAdapter daexcel = new OleDbDataAdapter(query, conn);
                dtexcel.Locale = CultureInfo.CurrentCulture;
                daexcel.Fill(dtexcel);
                //try
                //{
                    //arr_DrExcel = dtexcel.Select("NOT ISNULL([Moneda],'')=''");
                }
                catch (Exception e1)
                {

                }
                //dtexcel.Select("[Número de expediente] is not null and [Número de expediente] != ''");
            }

            conn.Close();
            return dtexcel;
        }

        protected void btnSubirArchivo_Click(object sender, EventArgs e)
        {
            if (fucCargaArchivo.HasFile)
            {
                string lstr_Directorio = String.Empty;
                try
                {
                    string lstr_NbrArchivo = Path.GetFileName(fucCargaArchivo.FileName);
                    //if (lstr_NbrArchivo == "Titulos_" + gstr_Usuario + ".xlsx")
                    //{
                    fucCargaArchivo.SaveAs(Server.MapPath("~/Contingentes/ArchivosContingentes/") + lstr_NbrArchivo);
                    lstr_Directorio = Server.MapPath("~/Contingentes/ArchivosContingentes/") + lstr_NbrArchivo;
                    //ldat_Expedientes = Import(lstr_Directorio);//exceldata(lstr_Directorio);
                    ldat_Expedientes = exceldata(lstr_Directorio);

                    if (ldat_Expedientes.Rows.Count == 0)
                    {
                        string script = @"<script type='text/javascript'> alert('El archivo no contiene registros.'); </script>";
                        ScriptManager.RegisterStartupScript(this, typeof(System.Web.UI.Page), "alerta", script, false);
                        btnCargarInfo.Visible = false;
                    }
                    else
                    {
                        string script = @"<script type='text/javascript'> alert('Archivo cargado y listo para procesar.'); </script>";
                        ScriptManager.RegisterStartupScript(this, typeof(System.Web.UI.Page), "alerta", script, false);

                        grvCCSS.DataSource = ldat_Expedientes;
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
                    ScriptManager.RegisterStartupScript(this, typeof(System.Web.UI.Page), "alerta", script, false);
                }
            }
            else
            {
                string script = @"<script type='text/javascript'> alert('No se ha seleccionado ningun archivo, por favor, seleccione un archivo para continuar.'); </script>";
                ScriptManager.RegisterStartupScript(this, typeof(System.Web.UI.Page), "alerta", script, false);
            }
        }

        protected void btnCargarInfo_Click(object sender, EventArgs e)
        {
            try
            {
                wsSG.wsSistemaGestor wsSistemaGestor = new wsSG.wsSistemaGestor();
                string str_Codigo = string.Empty;
                string str_Mensaje = String.Empty;

                string str_MonedaExp = string.Empty;
                decimal? dec_TipoCambioOrig = null;
                decimal dec_TipoCambioCierreExp = 0;
                decimal? dec_MontoPrincipalExp = null;
                decimal? dec_MontoInteresesExp = null;
                decimal? dec_MontoInteresesMoraExp = null;
                decimal? dec_MontoCostasExp = null;
                decimal? dec_MontoDanosExp = null;
                decimal? dec_MontoPrincipal = null;
                decimal? dec_MontoIntereses = null;
                decimal? dec_MontoInteresesMora = null;
                decimal? dec_MontoCostas = null;
                decimal? dec_MontoDanos = null;

                decimal? dec_DiferenciaPrincipal = null;
                decimal? dec_DiferenciaIntereses = null;
                decimal? dec_DiferenciaInteresesMora = null;
                decimal? dec_DiferenciaCostas = null;
                decimal? dec_DiferenciaDanos = null;
                String[] lstr_Resultado = new String[2];
                String[] lstr_ResultadoLiq = new String[2];
                bool lbol_control = false;
                int lint_IdCPResEnFirme = 0;
                int lint_IdCPResLiq = 0;
                int lint_IdRes = 0;
                string lstr_IdResolucion = string.Empty;
                string lstr_EstadoResolucion = string.Empty;
                string lstr_IdSociedadGL = string.Empty;
                string lstr_IdExpedienteFK = string.Empty;
                bool lbln_ExisteExp = false;

                foreach (DataRow dr_Exp in ldat_Expedientes.Rows)
                {
                    lint_IdCPResEnFirme = 0;
                    lint_IdCPResLiq = 0;
                    lint_IdRes = 0;
                    lstr_IdResolucion = string.Empty;
                    lstr_EstadoResolucion = string.Empty;
                    lstr_IdSociedadGL = string.Empty;
                    lstr_IdExpedienteFK = string.Empty;
                    lbln_ExisteExp = false;
                    if (!string.IsNullOrEmpty(dr_Exp["Número de expediente"].ToString()))
                    {
                        DataSet ds = ws_SGService.uwsConsultarResolucion("", dr_Exp["Número de expediente"].ToString(), dr_Exp["Código"].ToString(), out  str_Codigo, out  str_Mensaje);// .uwsConsultarExpedienteXNumero(dr_Exp["Número de Expediente"].ToString(), dr_Exp["Código"].ToString());

                        System.Data.DataTable dt_Res = ds.Tables[0];
                        foreach (DataRow dr_Res in dt_Res.Rows)
                        {
                            lbln_ExisteExp = true;
                            str_MonedaExp = dr_Res["Moneda"].ToString().Trim();
                            lint_IdRes = Convert.ToInt32(dr_Res["IdRes"].ToString());
                            lstr_IdResolucion = dr_Res["IdResolucion"].ToString();
                            lstr_IdExpedienteFK = dr_Res["IdExpedienteFK"].ToString();//Llave que relaciona las resoluciones dictadas, con los expedientes existentes                       
                            lstr_IdSociedadGL =          dr_Res["IdSociedadGL"].ToString();
                            lstr_EstadoResolucion =      dr_Res["EstadoResolucion"].ToString();//Campo que define qué tipo de resolución es, Resolución Provisional 1 ó 2,  Resolución en Firme (aplica tanto para activos contingentes como para pasivos contingentes), o Declaración Sin Lugar.
                            try
                            {
                              dec_TipoCambioOrig = Convert.ToDecimal(dr_Res["TipoCambio1"]);
                            }
                            catch (Exception ex)
                            {
                                    dec_TipoCambioOrig = 1;
                            }
                            try
                            {
                                    dec_TipoCambioCierreExp = Convert.ToDecimal(dr_Res["TipoCambioCierre"]);
                            }
                            catch (Exception ex)
                            {
                                    dec_TipoCambioCierreExp = 0;
                            }
                            //DataRow dr_Res = dt_Res.Rows[0];
                            if (lstr_EstadoResolucion == "En Firme")
                            {
                                
                                lint_IdCPResEnFirme = Convert.ToInt32 (dr_Res["IdCobroPagoResolucion"]);
                                try
                                {
                                    dec_MontoPrincipalExp = Convert.ToDecimal(dr_Res["MontoPrincipal"]);
                                }
                                catch (Exception ex)
                                {
                                    dec_MontoPrincipalExp = null;
                                }
                            }
                            else if (lstr_EstadoResolucion == "Liquidacion")
                            {
                                
                                lint_IdCPResLiq = Convert.ToInt32 (dr_Res["IdCobroPagoResolucion"]);
                                
                                try
                                {
                                    dec_MontoInteresesExp = Convert.ToDecimal(dr_Res["MontoIntereses"]);
                                }
                                catch (Exception ex)
                                {
                                    dec_MontoInteresesExp = null;
                                }
                                if (dec_MontoInteresesExp == 0)
                                {
                                    try
                                    {
                                        dec_MontoInteresesExp = Convert.ToDecimal(dr_Res["Intereses"]);
                                    }
                                    catch (Exception ex1)
                                    {
                                        dec_MontoInteresesExp = 0;
                                    }
                                }
                                try
                                {
                                    dec_MontoInteresesMoraExp = Convert.ToDecimal(dr_Res["InteresesMoratorios"]);
                                }
                                catch (Exception ex)
                                {
                                    dec_MontoInteresesMoraExp = null;
                                }
                                try
                                {
                                    dec_MontoCostasExp = Convert.ToDecimal(dr_Res["Costas"]);
                                }
                                catch (Exception ex)
                                {
                                    dec_MontoCostasExp = null;
                                }
                                try
                                {
                                    dec_MontoDanosExp = Convert.ToDecimal(dr_Res["DanoMoral"]);
                                }
                                catch (Exception ex)
                                {
                                    dec_MontoDanosExp = null;
                                }
                            }
                        }//foreach
                        if (lbln_ExisteExp)
                        {
                        try
                        {
                                dec_MontoPrincipal = Convert.ToDecimal(dr_Exp["Monto principal"]);
                        }
                        catch (Exception ex)
                        {
                                dec_MontoPrincipal = 0;
                        }
                        try
                        {
                                dec_MontoIntereses = Convert.ToDecimal(dr_Exp["Monto intereses"]);
                        }
                        catch (Exception ex)
                        {
                                dec_MontoIntereses = 0;
                        }
                        try
                        {
                                dec_MontoInteresesMora = Convert.ToDecimal(dr_Exp["Monto intereses moratorios"]);
                        }
                        catch (Exception ex)
                        {
                                dec_MontoInteresesMora = 0;
                        }
                        try
                        {
                                dec_MontoCostas = Convert.ToDecimal(dr_Exp["Monto costas"]);
                        }
                        catch (Exception ex)
                        {
                                dec_MontoCostas = 0;
                        }
                        try
                        {
                                dec_MontoDanos = Convert.ToDecimal(dr_Exp["Monto daños y perjuicios"]);
                        }
                        catch (Exception ex)
                        {
                                dec_MontoDanos = 0;
                        }
                        if (lint_IdCPResLiq!= 0)
                        {
                                lstr_Resultado = ws_SGService.uwsModificarCobrosPagosArchivo(
                                          lint_IdRes,
                                          lint_IdCPResLiq,
                                          lstr_IdResolucion,//Identificador único de la resolución dictada en los tribunales de justicia
                                          lstr_IdExpedienteFK,
                                          lstr_IdSociedadGL,
                                          "Liquidacion",
                                          str_MonedaExp,//dr_Res["Moneda"].ToString(),//La moneda en la cual se recibe el cobro. Campo obligatorio
                                          dec_TipoCambioOrig,//dr_Res["TipoCambio"].ToString(),//El tipo de cambio al momento de incluirlo en el sistema.
                                          0,//Es el monto principal a cobrar/pagar
                                          0,//Monto principal a cobrar/pagar en colones
                                          dec_MontoIntereses,
                                          dec_MontoIntereses,
                                          dec_MontoInteresesMora,
                                          dec_MontoInteresesMora,
                                          dec_MontoCostas,
                                          dec_MontoCostas,
                                          dec_MontoDanos,
                                          dec_MontoDanos,
                                          gstr_Usuario);
                                if (lstr_Resultado[0] == "00")
                                {
                                    ws_SGService.uwsRegistrarAccionBitacora("CT", gstr_Usuario, "Registro de Pago Liquidación", "Exp: " + dr_Exp["Número de expediente"].ToString() + " en " + dr_Exp["Ministerio"].ToString()
                                        + " Principal " + dec_MontoPrincipal.ToString()
                                        + " Intereses " + dec_MontoIntereses.ToString()
                                        + " Moratorios " + dec_MontoInteresesMora.ToString()
                                        + " Costas " + dec_MontoCostas.ToString()
                                        + " Daños " + dec_MontoDanos.ToString()
                                        );
                                }
                                txtError.Text += "\r\n 1 Resultado Expediente " + dr_Exp["Número de expediente"].ToString() + " en " + dr_Exp["Ministerio"].ToString() + " " + lstr_Resultado[1];
                            }
                            if (lint_IdCPResEnFirme!= 0)
                            {
                                lstr_Resultado = ws_SGService.uwsModificarCobrosPagosArchivo(
                                          lint_IdRes,
                                          lint_IdCPResEnFirme,
                                          lstr_IdResolucion,//Identificador único de la resolución dictada en los tribunales de justicia
                                          lstr_IdExpedienteFK,
                                          lstr_IdSociedadGL,
                                          "En Firme",
                                          str_MonedaExp,//dr_Res["Moneda"].ToString(),//La moneda en la cual se recibe el cobro. Campo obligatorio
                                          dec_TipoCambioOrig,//dr_Res["TipoCambio"].ToString(),//El tipo de cambio al momento de incluirlo en el sistema.
                                          dec_MontoPrincipal,//Es el monto principal a cobrar/pagar
                                          dec_MontoPrincipal * dec_TipoCambioOrig,//Monto principal a cobrar/pagar en colones
                                          0,//dec_MontoIntereses,
                                          0,//dec_MontoIntereses,
                                          0,//dec_MontoInteresesMora,
                                          0,//dec_MontoInteresesMora,
                                          0,//dec_MontoCostas,
                                          0,//dec_MontoCostas,
                                          0,//dec_MontoDanos,
                                          0,//dec_MontoDanos,
                                          gstr_Usuario);
                                if (lstr_Resultado[0] == "00")
                                {
                                    ws_SGService.uwsRegistrarAccionBitacora("CT", gstr_Usuario, "Registro de Pago En Firme", "Exp: " + dr_Exp["Número de expediente"].ToString() + " en " + dr_Exp["Ministerio"].ToString()
                                        + " Principal " + dec_MontoPrincipal.ToString()
                                        + " Intereses " + dec_MontoIntereses.ToString()
                                        + " Moratorios " + dec_MontoInteresesMora.ToString()
                                        + " Costas " + dec_MontoCostas.ToString()
                                        + " Daños " + dec_MontoDanos.ToString()
                                        );
                                }
                                txtError.Text += "\r\n 2 Resultado Expediente " + dr_Exp["Número de expediente"].ToString() + " en " + dr_Exp["Ministerio"].ToString() + " " + lstr_Resultado[1];
                            }
                            /*
                            lstr_Resultado = ws_SGService.uwsModificarResolucion(lint_IdRes, 0, gstr_IdResolucion, gstr_IdExpediente, gstr_Sociedad, gstr_EstadoResolucion,gstr_Estado,
                                                 Convert.ToDateTime(gstr_FechaResolucion), Convert.ToDateTime(gstr_PosibleFechaSalida), gdec_MontoPosibleReembolso,
                                                 gdec_MontoPosibleReembolsoColones, gstr_Observaciones, gint_CxCaCxP, gstr_Moneda, gdec_TipoCambio, gdec_Tbp, gdec_Tiempo, gdec_MontoPrincipal, gdec_MontoIntereses,
                                                 gdec_ValorPresentePrincipalColones, gdec_ValorPresenteInteresesColones, gdec_MontoPrincipalColones, gdec_MontoInteresesColones,
                                                 gstr_EstadoProcesal, gint_EstadoPretension, gstr_Usuario);

                            lstr_ResultadoLiq = ws_SGService.uwsModificarLiquidacion(
                                     gstr_IdExpediente, gstr_IdSociedadGL, gstr_EstadoResolucion,
                                     gdt_FechaResolucion,
                                     gdt_PosibleFechaSalida,
                                     gstr_IdResolucionDictada,
                                     gint_CxCaCxP, lstr_Moneda, gdec_TipoCambio,
                                     gdec_Intereses, gdec_InteresesColonesLiq,
                                     gdec_InteresesMoratorios, gdec_InteresesMoratoriosColones,
                                     gdec_Costas, gdec_CostasColones,
                                     gdec_DannoMoral, gdec_DannoMoralColones,
                                     gstr_Observacion,
                                     gstr_Estado,
                                     gstr_Usuario,
                                     gstr_EstadoProcesal,
                                     gstr_TipoTransaccion, gstr_EstadoTransaccion, gstr_Usuario);
                             * */
                        }
                        else
                        {
                            txtError.Text += "\r\n 3 No Existe Expediente " + dr_Exp["Número de expediente"].ToString() + " en " + dr_Exp["Ministerio"].ToString();


                        }
                        //ws_SGService.
                        //    uwsRegistrarExpedientes(
                        //    ldat_Expedientes.Rows[i]["IdExpediente"].ToString(),
                        //    ldat_Expedientes.Rows[i]["IdSociedadGL"].ToString(),
                        //    ldat_Expedientes.Rows[i]["NomDemandado"].ToString(),
                        //    ldat_Expedientes.Rows[i]["MontoPrincipal"].ToString(),
                        //    ldat_Expedientes.Rows[i]["MontoIntereses"].ToString(),
                        //    ldat_Expedientes.Rows[i]["MontoCostas"].ToString(),
                        //    ldat_Expedientes.Rows[i]["MontoIntMoratorios"].ToString(),
                        //    ldat_Expedientes.Rows[i]["MontoDannosPerj"].ToString(),
                        //    ldat_Expedientes.Rows[i]["UsrModifica"].ToString(),
                        //    ldat_Expedientes.Rows[i]["FchModifica"].ToString()
                        //    );
                        lbol_control = true;
                    }
                }
                //if (!string.IsNullOrEmpty(txtError.Text))
                    txtError.Visible = true;

                if (lbol_control)
                {
                    //if (str_Mensaje == String.Empty)
                    //{
                    string script = @"<script type='text/javascript'> alert('Se finalizó el proceso, revise los mensajes adjuntos y la bitácora para esta la transacción.'); </script>";
                        ScriptManager.RegisterStartupScript(this, typeof(System.Web.UI.Page), "alerta", script, false);
                    //}
                    //else
                    //{
                    //    string script = @"<script type='text/javascript'> alert('Carga de archivo parcial. Los siguientes valores no fueron cargados: " + str_Mensaje + ".'); </script>";
                    //    ScriptManager.RegisterStartupScript(this, typeof(System.Web.UI.Page), "alerta", script, false);
                    //}
                }
                else
                {
                    string script = @"<script type='text/javascript'> alert('La carga no se llevó a cabo, por favor, revise el documento e intente nuevamente.'); </script>";
                    ScriptManager.RegisterStartupScript(this, typeof(System.Web.UI.Page), "alerta", script, false);
                }
                btnCargarInfo.Visible = false;
            }
            catch (Exception ex)
            {
                ex.ToString();
                string script = @"<script type='text/javascript'> alert('El archivo presenta inconsistencias, revise e intente de nuevo.'); </script>";
                ScriptManager.RegisterStartupScript(this, typeof(System.Web.UI.Page), "alerta", script, false);
            }
        }

        protected void grvCCSS_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void txtError_TextChanged(object sender, EventArgs e)
        {

        }
    }
}