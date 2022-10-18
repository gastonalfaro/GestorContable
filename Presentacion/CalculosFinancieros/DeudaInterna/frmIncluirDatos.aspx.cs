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
    public partial class frmIncluirDatos : BASE
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
                        if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmIncluirDatos"))
                            Response.Redirect("~/Principal.aspx", true);
                        else
                        {
                            btnCargarInfo.Visible = false;
                            lblMensaje.Visible = false;
                            hlDescargarFormato.NavigateUrl = "../DeudaInterna/ArchivosDI/Plantillas/Titulos_Plantilla.xlsx";
                            hlDescargarFormato.ToolTip = "Nota Importante: Al descargar el archivo, reemplace (en el nombre del archivo) la palabra plantilla, por su número de cédula. Incluya ceros sin espacios. Ej: Titulos_0109990888";
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
            Presentacion.wsDI.wsDeudaInterna _wsDeudaInterna = new Presentacion.wsDI.wsDeudaInterna();
            Presentacion.wsSG.wsSistemaGestor _wsSistemaGestor = new wsSG.wsSistemaGestor();
            DataTable ldat_Nemotecnicos = new DataTable();
            ldat_Nemotecnicos = _wsSistemaGestor.uwsConsultarNemotecnicos(null, null, null, null, null).Tables[0];
            string mensaje = String.Empty;
            bool lbol_control1 = false;
            bool lbol_control2 = false;

            string lstr_SinNemotecnico = "";
            string lstr_Duplicados = "";

            //for (int i = 0; i < ldat_TitulosValores.Rows.Count; i++)
            foreach(DataRow ldr_TitulosValores in ldat_TitulosValores.Rows)
            {
                if (_wsDeudaInterna.ConsultarTitulosValores(ldr_TitulosValores["NumeroValor"].ToString(), ldr_TitulosValores["Nemotecnico"].ToString(), String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "01/01/1900", "01/01/5000").Tables[0].Rows.Count == 0)
                {
                    try
                    {
                        string lstr_moneda = ldat_Nemotecnicos.Select("IdNemotecnico = '" + ldr_TitulosValores["Nemotecnico"].ToString() + "'")[0]["IdMoneda"].ToString().Trim().Equals("CRCN") ? "CRC" : ldat_Nemotecnicos.Select("IdNemotecnico = '" + ldr_TitulosValores["Nemotecnico"].ToString() + "'")[0]["IdMoneda"].ToString().Trim();
                        bool lbol_NemoActivo = ldat_Nemotecnicos.Select("IdNemotecnico = '" + ldr_TitulosValores["Nemotecnico"].ToString() + "'")[0]["Estado"].ToString().Trim().Equals("A");

                        if ( lbol_NemoActivo && lstr_moneda.Equals(ldr_TitulosValores["CodigoMoneda"].ToString()) )
                        {
                            #region insertar
                            _wsDeudaInterna.CrearTituloValor(
                                Convert.ToInt32(ldr_TitulosValores["NumeroValor"].ToString()),
                                ldr_TitulosValores["EstadoValor"].ToString(),
                                ldr_TitulosValores["Nemotecnico"].ToString(),
                                ldr_TitulosValores["Tipo"].ToString(),
                                ldr_TitulosValores["TipoNegociacion"].ToString(),
                                ldr_TitulosValores["CodigoMoneda"].ToString().Equals("CRC") ? "CRCN" : ldr_TitulosValores["CodigoMoneda"].ToString(),
                                Convert.ToDecimal(ldr_TitulosValores["ValorFacial"].ToString()),
                                ldr_TitulosValores["FechaValor"].ToString().Substring(0, 10),
                                ldr_TitulosValores["PlazoValor"].ToString(),
                                ldr_TitulosValores["FechaCancelacion"].ToString().Substring(0, 10),
                                ldr_TitulosValores["FechaVencimiento"].ToString().Substring(0, 10),
                                Convert.ToDecimal(ldr_TitulosValores["ValorTransadoBruto"].ToString()),
                                Convert.ToDecimal(ldr_TitulosValores["ValorTransadoNeto"].ToString()),
                                Convert.ToDecimal(ldr_TitulosValores["TasaBruta"].ToString()),
                                Convert.ToDecimal(ldr_TitulosValores["TasaNeta"].ToString()),
                                Convert.ToDecimal(ldr_TitulosValores["Margen"].ToString()),
                                ldr_TitulosValores["NroEmisionSerie"].ToString(),
                                ldr_TitulosValores["Propietario"].ToString(),
                                ldr_TitulosValores["EntidadCustodia"].ToString(),
                                ldr_TitulosValores["FchCreacion"].ToString().Substring(0, 10),
                                ldr_TitulosValores["SistemaNegociacion"].ToString(),
                                ldr_TitulosValores["MotivoAnulacion"].ToString(),
                                Convert.ToDecimal(ldr_TitulosValores["RendimientoPorDescuento"].ToString()),
                                Convert.ToDecimal(ldr_TitulosValores["ImpuestoPagado"].ToString()),
                                Convert.ToDecimal(ldr_TitulosValores["Premio"].ToString()),
                                ldr_TitulosValores["ModuloSINPE"].ToString(),
                                gstr_Usuario, 
                                ldr_TitulosValores["DescripcionNegociacion"].ToString(), 
                                ldr_TitulosValores["NumeroIdentificacion"].ToString(),
                                ldr_TitulosValores["TipoIdentificacion"].ToString());
                            #endregion

                            lbol_control1 = true;
                        }
                        else
                        {
                            lstr_SinNemotecnico = lstr_SinNemotecnico + ldr_TitulosValores["NumeroValor"].ToString() + ", ";
                            lbol_control1 = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        lstr_SinNemotecnico = lstr_SinNemotecnico + ldr_TitulosValores["NumeroValor"].ToString() + "-" + ldr_TitulosValores["Nemotecnico"].ToString() + ", ";
                        lbol_control1 = true;
                    }
                }
                else
                {
                    lstr_Duplicados = lstr_Duplicados + ldr_TitulosValores["NumeroValor"].ToString() + ", ";
                    lbol_control2 = true;
                }
            }

            string mensajefinal = "";

            if (lbol_control2)
            {
                mensajefinal = "Carga de archivo parcial. Los siguientes valores no fueron cargados: " + lstr_Duplicados + ". Estos registros ya existen en el sistema. - ";
            }
            if (!lbol_control2 && lbol_control1)
            {
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

            //recordar que despues de agregado un título, hay que generarle el devengo

        }

        

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (fucCargaArchivo.HasFile)
            {
                string lstr_Directorio = String.Empty;
                try
                {
                    string lstr_NbrArchivo = Path.GetFileName(fucCargaArchivo.FileName);
                    if (lstr_NbrArchivo == "Titulos_" + gstr_Usuario + ".xlsx")
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
            wsDeudaInterna.wsDeudaInterna wsDeudaInterna = new wsDeudaInterna.wsDeudaInterna();
            //wsDeudaInterna.CalculaDevengoValores();
        }

        protected void btnContabilizar_Click(object sender, EventArgs e)
        {
            //SGAsientosDeudaInterna.Cancelacion SGCancelacion = new  SGAsientosDeudaInterna.Cancelacion();
            Presentacion.wsDI.wsDeudaInterna _wsDeudaInterna = new Presentacion.wsDI.wsDeudaInterna();
            //_wsDeudaInterna.ContabilizarColocacion()
            //vWebServiceCalculosFinancieros.ContabilizarDeudaInterna(3, String.Empty, String.Empty, String.Empty);
        }
    }
}