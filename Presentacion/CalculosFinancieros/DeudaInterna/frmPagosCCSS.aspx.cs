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

namespace Presentacion.CalculosFinancieros.DeudaInterna
{
    public partial class frmPagosCCSS : BASE
    {
        # region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private string gstr_Usuario = String.Empty;
        //private static DataTable ldat_PagosCCSS = new DataTable();
        protected DataTable ldat_PagosCCSS
        {
            get
            {
                if (ViewState["ldat_PagosCCSS"] == null)
                    ViewState["ldat_PagosCCSS"] = new DataTable();
                return (DataTable)ViewState["ldat_PagosCCSS"];
            }
            set
            {
                ViewState["ldat_PagosCCSS"] = value;
            }
        }
        private string gstr_ModuloActual = String.Empty;
        private wsAsientos.ServicioContable asientos = new wsAsientos.ServicioContable();
        //private static List<LogicaNegocio.CalculosFinancieros.CCSS> arregloCCSS;
        protected List<LogicaNegocio.CalculosFinancieros.CCSS> arregloCCSS
        {
            get
            {
                if (ViewState["arregloCCSS"] == null)
                    ViewState["arregloCCSS"] = null;
                return (List<LogicaNegocio.CalculosFinancieros.CCSS>)ViewState["arregloCCSS"];
            }
            set
            {
                ViewState["arregloCCSS"] = value;
            }
        }
        //private static Presentacion.wsDI.wsDeudaInterna _wsDeudaInterna = new Presentacion.wsDI.wsDeudaInterna();
        
        
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
                        if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmPagosCCSS"))
                            Response.Redirect("~/Principal.aspx", true);
                        else
                        {
                            btnCargarInfo.Visible = false;
                            hlDescargarFormato.NavigateUrl = "../DeudaInterna/ArchivosDI/Plantillas/CCSS_Plantilla.xlsx";
                            hlDescargarFormato.ToolTip = "Nota Importante: Al descargar el archivo, reemplace (en el nombre del archivo) la palabra plantilla, por su número de cédula. Incluya ceros sin espacios. Ej: CCSS_0109990888";
                    
                        }
                    }
                    else
                        Response.Redirect("~/Login.aspx", true);
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
            string[] texto;

            arregloCCSS = new List<LogicaNegocio.CalculosFinancieros.CCSS>();
            Presentacion.wsSG.wsSistemaGestor wsSistemaGestor = new Presentacion.wsSG.wsSistemaGestor();
            Presentacion.wsDI.wsDeudaInterna _wsDeudaInterna = new Presentacion.wsDI.wsDeudaInterna();
            DataTable ldat_Nemotecnicos = new DataTable();
            ldat_Nemotecnicos = wsSistemaGestor.uwsConsultarNemotecnicos(null, null, null, null, null).Tables[0];
            string mensaje = String.Empty;
            bool lbol_control1 = false;
            bool lbol_control2 = false;

            string lstr_SinNemotecnico = "";
            string lstr_Duplicados = "";
            int tam = ldat_PagosCCSS.Rows.Count;
            for (int i = 0; i < ldat_PagosCCSS.Rows.Count; i++)
            {
                //if (ldat_Nemotecnicos.Select("IdNemotecnico = '" + ldat_PagosCCSS.Rows[i]["Nemotecnico"].ToString() + "'").Count() != 0)
                //{
                //ldat_PagosCCSS.Rows[i]["Nemotecnico"] = ldat_PagosCCSS.Rows[i]["Nemotecnico"] + "_CCSS";
                if (ldat_PagosCCSS.Rows[i]["NumeroValor"].ToString().Trim().Equals("") || ldat_PagosCCSS.Rows[i]["Nemotecnico"].ToString().Trim().Equals(""))
                {
                    continue;
                }
                if (true)
                {
                    try
                    {
                        string lstr_moneda = ldat_Nemotecnicos.Select("IdNemotecnico = '" + ldat_PagosCCSS.Rows[i]["Nemotecnico"].ToString() + "'")[0]["IdMoneda"].ToString().Trim().Equals("CRCN") ? "CRC" : ldat_Nemotecnicos.Select("IdNemotecnico = '" + ldat_PagosCCSS.Rows[i]["Nemotecnico"].ToString() + "'")[0]["IdMoneda"].ToString().Trim();
                        if (ldat_Nemotecnicos.Select("IdNemotecnico = '" + ldat_PagosCCSS.Rows[i]["Nemotecnico"].ToString() + "'")[0]["Estado"].ToString().Trim() == "A" && lstr_moneda == ldat_PagosCCSS.Rows[i]["CodigoMoneda"].ToString())
                        {
                            if (ldat_PagosCCSS.Rows[i]["EstadoValor"].ToString().Trim() == "Vigente")
                            {
                                #region insertar
                                texto = _wsDeudaInterna.CrearPagoCCSS(
                                    ldat_PagosCCSS.Rows[i]["EstadoValor"].ToString().Trim(),
                                    ldat_PagosCCSS.Rows[i]["Nemotecnico"].ToString().Trim(),
                                    ldat_PagosCCSS.Rows[i]["Tipo"].ToString().Trim(),
                                    ldat_PagosCCSS.Rows[i]["TipoNegociacion"].ToString(),
                                    Convert.ToInt32(ldat_PagosCCSS.Rows[i]["NumeroValor"].ToString().Trim()),
                                    ldat_PagosCCSS.Rows[i]["CodigoMoneda"].ToString().Equals("CRC") ? "CRCN" : ldat_PagosCCSS.Rows[i]["CodigoMoneda"].ToString(),
                                    Convert.ToDecimal(ldat_PagosCCSS.Rows[i]["ValorFacial"].ToString().Trim()),
                                    ldat_PagosCCSS.Rows[i]["FechaValor"].ToString().Substring(0, 10),
                                    ldat_PagosCCSS.Rows[i]["PlazoValor"].ToString().Trim(),
                                    ldat_PagosCCSS.Rows[i]["FechaCancelacion"].ToString().Substring(0, 10),
                                    ldat_PagosCCSS.Rows[i]["FechaVencimiento"].ToString().Substring(0, 10),
                                    Convert.ToDecimal(ldat_PagosCCSS.Rows[i]["ValorTransadoBruto"].ToString().Trim()),
                                    Convert.ToDecimal(ldat_PagosCCSS.Rows[i]["ValorTransadoNeto"].ToString().Trim()),
                                    Convert.ToDecimal(ldat_PagosCCSS.Rows[i]["TasaBruta"].ToString().Trim()),
                                    Convert.ToDecimal(ldat_PagosCCSS.Rows[i]["TasaNeta"].ToString().Trim()),
                                    ldat_PagosCCSS.Rows[i]["NroEmisionSerie"].ToString().Trim(),
                                    ldat_PagosCCSS.Rows[i]["FechaCreacion"].ToString().Substring(0, 10),
                                    "Pagos CCSS",
                                    ldat_PagosCCSS.Rows[i]["MotivoAnulacion"].ToString(),
                                    Convert.ToDecimal(ldat_PagosCCSS.Rows[i]["RendimientoPorDescuento"].ToString().Trim()),
                                    Convert.ToDecimal(ldat_PagosCCSS.Rows[i]["Premio"].ToString().Trim()),
                                    Convert.ToDecimal(ldat_PagosCCSS.Rows[i]["ImpuestoPagado"].ToString().Trim()),
                                    gstr_Usuario,
                                    ldat_PagosCCSS.Rows[i]["ModuloSINPE"].ToString());

                                    ws_SGService.uwsConsultarDinamico("update cf.titulosvalores set indicadorcupon = 'V'  where NroCupon = 0 and nrovalor = " + Convert.ToInt32(ldat_PagosCCSS.Rows[i]["NumeroValor"].ToString().Trim()) + " and Nemotecnico = '" + ldat_PagosCCSS.Rows[i]["Nemotecnico"].ToString().Trim() + "'");
                                    
                                    //RAMSES
                                    ws_SGService.uwsConsultarDinamico("UPDATE cf.titulosvalores SET Descripcion = 'Pagos CCSS' where NroCupon = 0 and nrovalor = " + Convert.ToInt32(ldat_PagosCCSS.Rows[i]["NumeroValor"].ToString().Trim()) + " and Nemotecnico = '" + ldat_PagosCCSS.Rows[i]["Nemotecnico"].ToString().Trim() + "'");
                                if (true)
                                {
                                    //wsDeudaInterna.CCSS CCSS = new wsDeudaInterna.CCSS();

                                    LogicaNegocio.CalculosFinancieros.CCSS CCSS = new LogicaNegocio.CalculosFinancieros.CCSS();

                                    CCSS.Lstr_EstadoValor = ldat_PagosCCSS.Rows[i]["EstadoValor"].ToString();
                                    CCSS.Lstr_Nemotecnico = ldat_PagosCCSS.Rows[i]["Nemotecnico"].ToString();
                                    CCSS.Lstr_Tipo = ldat_PagosCCSS.Rows[i]["Tipo"].ToString();
                                    CCSS.Lstr_TipoNegociacion = ldat_PagosCCSS.Rows[i]["TipoNegociacion"].ToString();
                                    CCSS.Lint_NumValor = Convert.ToInt32(ldat_PagosCCSS.Rows[i]["NumeroValor"].ToString());
                                    CCSS.Lstr_Moneda = ldat_PagosCCSS.Rows[i]["CodigoMoneda"].ToString().Equals("CRC") ? "CRCN" : ldat_PagosCCSS.Rows[i]["CodigoMoneda"].ToString();
                                    CCSS.Ldec_ValorFacial = Convert.ToDecimal(ldat_PagosCCSS.Rows[i]["ValorFacial"].ToString());
                                    CCSS.Ldt_FchValor = ldat_PagosCCSS.Rows[i]["FechaValor"].ToString().Substring(0, 10);
                                    CCSS.Lstr_PlazoValor = ldat_PagosCCSS.Rows[i]["PlazoValor"].ToString();
                                    CCSS.Ldt_FchCancelacion = ldat_PagosCCSS.Rows[i]["FechaCancelacion"].ToString().Substring(0, 10);
                                    CCSS.Ldt_FchVencimiento = ldat_PagosCCSS.Rows[i]["FechaVencimiento"].ToString().Substring(0, 10);
                                    CCSS.Ldec_ValorTransadoBruto = Convert.ToDecimal(ldat_PagosCCSS.Rows[i]["ValorTransadoBruto"].ToString());
                                    CCSS.Ldec_ValorTransadoNeto = Convert.ToDecimal(ldat_PagosCCSS.Rows[i]["ValorTransadoNeto"].ToString());
                                    CCSS.Ldec_TasaBruta = Convert.ToDecimal(ldat_PagosCCSS.Rows[i]["TasaBruta"].ToString());
                                    CCSS.Ldec_TasaNeta = Convert.ToDecimal(ldat_PagosCCSS.Rows[i]["TasaNeta"].ToString());
                                    CCSS.Lstr_NroEmisionSerie = ldat_PagosCCSS.Rows[i]["NroEmisionSerie"].ToString();
                                    CCSS.Ldt_FchCreacionT = ldat_PagosCCSS.Rows[i]["FechaCreacion"].ToString().Substring(0, 10);
                                    CCSS.Lstr_SistemaNegociacion = "Pagos CCSS";
                                    CCSS.Lstr_MotivoAnulacion = ldat_PagosCCSS.Rows[i]["MotivoAnulacion"].ToString();
                                    CCSS.Ldec_RendimientoPorDescuento = Convert.ToDecimal(ldat_PagosCCSS.Rows[i]["RendimientoPorDescuento"].ToString());
                                    CCSS.Ldec_Premio = Convert.ToDecimal(ldat_PagosCCSS.Rows[i]["Premio"].ToString());
                                    CCSS.Ldec_ImpuestoPagado = Convert.ToDecimal(ldat_PagosCCSS.Rows[i]["ImpuestoPagado"].ToString());
                                    CCSS.Lstr_UsrCreacion = gstr_Usuario;
                                    CCSS.Lstr_ModuloSINPE = ldat_PagosCCSS.Rows[i]["ModuloSINPE"].ToString();
                                    CCSS.Lstr_EntidadCustodia = ldat_PagosCCSS.Rows[i]["EntidadCustodia"].ToString();
                                    CCSS.Ldec_monto_pagado_efectivo = Convert.ToDecimal(ldat_PagosCCSS.Rows[i]["MontoPagadoEfectivo"].ToString());
                                    CCSS.Lstr_2110201041 = ldat_PagosCCSS.Rows[i]["2110201041"].ToString();
                                    CCSS.Lstr_2110201050 = ldat_PagosCCSS.Rows[i]["2110201050"].ToString();
                                    CCSS.Lstr_2110201070 = ldat_PagosCCSS.Rows[i]["2110201070"].ToString();
                                    CCSS.Lstr_2110201080 = ldat_PagosCCSS.Rows[i]["2110201080"].ToString();
                                    CCSS.Lstr_2110201090 = ldat_PagosCCSS.Rows[i]["2110201090"].ToString();
                                    CCSS.Lstr_2110201100 = ldat_PagosCCSS.Rows[i]["2110201100"].ToString();
                                    CCSS.Lstr_2116421000 = ldat_PagosCCSS.Rows[i]["2116421000"].ToString();
                                    CCSS.Lstr_2116422000 = ldat_PagosCCSS.Rows[i]["2116422000"].ToString();
                                    CCSS.Lstr_2116423000 = ldat_PagosCCSS.Rows[i]["2116423000"].ToString();
                                    CCSS.Lstr_2116424000 = ldat_PagosCCSS.Rows[i]["2116424000"].ToString();
                               
                                    arregloCCSS.Add(CCSS);
                                }

                                #endregion
                            }
                            lbol_control1 = true;
                        }
                        else
                        {
                            lstr_SinNemotecnico = lstr_SinNemotecnico + ldat_PagosCCSS.Rows[i]["NumeroValor"].ToString() + ", ";
                            lbol_control1 = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        lstr_SinNemotecnico = lstr_SinNemotecnico + ldat_PagosCCSS.Rows[i]["NumeroValor"].ToString() + ", ";
                        lbol_control1 = true;
                    }
                }
                else
                {
                    lstr_Duplicados = lstr_Duplicados + ldat_PagosCCSS.Rows[i]["NumeroValor"].ToString() + ", ";
                    lbol_control2 = true;
                }
            }
            
            string mensajefinal = "";

            if (lbol_control2)
            {
                //despliega los que no fueron procesados por duplicados
                mensajefinal = "Carga de archivo parcial. Los siguientes valores no fueron cargados: " + lstr_Duplicados + ". Estos registros ya existen en el sistema. - ";
            }
            else if (lbol_control1){

                //despliega si la carga fue exitosa, o bien si hubo nemotécnicos inexistentes y cuales fueron
                if (lstr_SinNemotecnico == String.Empty)
                {
                    mensajefinal += "Carga de archivo exitosa. - ";
                }
                else
                {
                    mensajefinal += "Carga de archivo parcial. Los siguientes valores no fueron cargados: " + lstr_SinNemotecnico + "por nemotécnicos inexistentes. - ";
                }
            }

            else //hubo un fallo en el archivo y no se carga la información
            {
                mensajefinal += "La carga no se llevó a cabo, por favor, revise el documento e intente nuevamente.";
            }

            string script = @"<script type='text/javascript'> alert('" + mensajefinal + "'); </script>";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
            btnCargarInfo.Visible = false;

            //wsDeudaInterna.CalculaDevengoValores();
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (fucCargaArchivo.HasFile)
            {
                string lstr_Directorio = String.Empty;
                try
                {
                    string lstr_NbrArchivo = Path.GetFileName(fucCargaArchivo.FileName);
                    if (lstr_NbrArchivo == "CCSS_"+gstr_Usuario+".xlsx")
                    {
                        fucCargaArchivo.SaveAs(Server.MapPath("~/CalculosFinancieros/DeudaInterna/ArchivosDI/") + lstr_NbrArchivo);
                        lstr_Directorio = Server.MapPath("~/CalculosFinancieros/DeudaInterna/ArchivosDI/") + lstr_NbrArchivo;
                        ldat_PagosCCSS = exceldata(lstr_Directorio);

                        if (ldat_PagosCCSS.Rows.Count == 0)
                        {
                            string script = @"<script type='text/javascript'> alert('El archivo no contiene registros.'); </script>";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                        }
                        else
                        {
                            string script = @"<script type='text/javascript'> alert('Archivo cargado y listo para procesar.'); </script>";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);

                            grvCCSS.DataSource = ldat_PagosCCSS;
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
                catch(Exception ex)
                {
                    string script = @"<script type='text/javascript'> alert('El archivo no pudo ser cargado. Error: " + ex.ToString()+ "'); </script>";
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
            grvCCSS.DataSource = ldat_PagosCCSS;
            grvCCSS.DataBind();
        }

        protected void btnContabilizar_Click(object sender, EventArgs e)
        {
            Presentacion.wsDI.wsDeudaInterna wsDI = new Presentacion.wsDI.wsDeudaInterna();
            String msj = "El proceso de contabilización ha terminado. Consulte su bitácora para ver más detalles.";
            foreach (LogicaNegocio.CalculosFinancieros.CCSS ccss in arregloCCSS)
            {
                String result = wsDI.ContabilizaCCSS(
                                                        ccss.Lstr_Nemotecnico,
                                                        ccss.Lint_NumValor,
                                                        ccss.Ldec_monto_pagado_efectivo,
                                                        Convert.ToDecimal(string.IsNullOrEmpty(ccss.Lstr_2110201041) ? "0" : ccss.Lstr_2110201041),
                                                        Convert.ToDecimal(string.IsNullOrEmpty(ccss.Lstr_2110201050) ? "0" : ccss.Lstr_2110201050),
                                                        Convert.ToDecimal(string.IsNullOrEmpty(ccss.Lstr_2110201070) ? "0" : ccss.Lstr_2110201070),
                                                        Convert.ToDecimal(string.IsNullOrEmpty(ccss.Lstr_2110201080) ? "0" : ccss.Lstr_2110201080),
                                                        Convert.ToDecimal(string.IsNullOrEmpty(ccss.Lstr_2110201090) ? "0" : ccss.Lstr_2110201090),
                                                        Convert.ToDecimal(string.IsNullOrEmpty(ccss.Lstr_2110201100) ? "0" : ccss.Lstr_2110201100),
                                                        Convert.ToDecimal(string.IsNullOrEmpty(ccss.Lstr_2116421000) ? "0" : ccss.Lstr_2116421000),
                                                        Convert.ToDecimal(string.IsNullOrEmpty(ccss.Lstr_2116422000) ? "0" : ccss.Lstr_2116422000),
                                                        Convert.ToDecimal(string.IsNullOrEmpty(ccss.Lstr_2116423000) ? "0" : ccss.Lstr_2116423000),
                                                        Convert.ToDecimal(string.IsNullOrEmpty(ccss.Lstr_2116424000) ? "0" : ccss.Lstr_2116424000)
                );
                /*if (!result.Contains("[S]"))
                {
                    //msj = result;
                }*/
            }
            this.mtr_msg(msj);
            string script = @"<script type='text/javascript'> alert('"+msj+"'); </script>";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
            btnContabilizar.Enabled = false;
        }


        public void mtr_msg(String msg)
        {
            Response.Write(String.Format("<script>alert('{0}');</script>", msg));
        }//FUNCION

    }
}