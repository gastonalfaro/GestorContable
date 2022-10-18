﻿using System;
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
using System.Web.UI.WebControls;
using Presentacion.Compartidas;
using System.IO;
using System.Net;

namespace Presentacion.CalculosFinancieros.DeudaInterna
{
    public partial class frmTrasladosMagisterio : BASE
    {
        # region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private string gstr_Usuario = String.Empty;
        //private static DataTable ldat_TrasladoMagisterio = new DataTable();
        protected DataTable ldat_TrasladoMagisterio
        {
            get
            {
                if (ViewState["ldat_TrasladoMagisterio"] == null)
                    ViewState["ldat_TrasladoMagisterio"] = new DataTable();
                return (DataTable)ViewState["ldat_TrasladoMagisterio"];
            }
            set
            {
                ViewState["ldat_TrasladoMagisterio"] = value;
            }
        }
        private string gstr_ModuloActual = String.Empty;
        private wsAsientos.ServicioContable asientos = new wsAsientos.ServicioContable();
        //private static List<LogicaNegocio.CalculosFinancieros.Magisterio> arregloMagisterio;
        //arregloMagisterio
        protected List<LogicaNegocio.CalculosFinancieros.Magisterio> arregloMagisterio
        {
            get
            {
                if (ViewState["arregloMagisterio"] == null)
                    ViewState["arregloMagisterio"] = new List<LogicaNegocio.CalculosFinancieros.Magisterio>();
                return (List<LogicaNegocio.CalculosFinancieros.Magisterio>)ViewState["arregloMagisterio"];
            }
            set
            {
                ViewState["arregloMagisterio"] = value;
            }
        }
        private static Presentacion.wsDI.wsDeudaInterna wsDeudaInterna = new Presentacion.wsDI.wsDeudaInterna();
        private static Presentacion.wsSG.wsSistemaGestor wsSistemaGestor = new Presentacion.wsSG.wsSistemaGestor();
        //RAMSES
       
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
                        if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmTrasladosMagisterio"))
                            Response.Redirect("~/Principal.aspx", true);
                        else
                        {
                            btnCargarInfo.Visible = false;
                            hlDescargarFormato.NavigateUrl = "../DeudaInterna/ArchivosDI/Plantillas/Magisterio_Plantilla.xlsx";
                            hlDescargarFormato.ToolTip = "Nota Importante: Al descargar el archivo, reemplace (en el nombre del archivo) la palabra plantilla, por su número de cédula. Incluya ceros sin espacios. Ej: Magisterio_0109990888";
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
                string query = "SELECT  * FROM [" + sheet + "]";
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

            arregloMagisterio = new List<LogicaNegocio.CalculosFinancieros.Magisterio>();

            DataTable ldat_Nemotecnicos = new DataTable();
            ldat_Nemotecnicos = wsSistemaGestor.uwsConsultarNemotecnicos(null, null, null, null, null).Tables[0];
            string mensaje = String.Empty;
            bool lbol_control1 = false;
            bool lbol_control2 = false;

            string lstr_SinNemotecnico = "";
            string lstr_Duplicados = "";
            int tam = ldat_TrasladoMagisterio.Rows.Count;
            //dataTable para obtener registros de titulos valores de la base de datos
            DataTable dtTitulosValores = new DataTable(); 
            for (int i = 0; i < ldat_TrasladoMagisterio.Rows.Count; i++)
            {
                dtTitulosValores = wsDeudaInterna.ConsultarTitulosValores(ldat_TrasladoMagisterio.Rows[i]["NumeroValor"].ToString().Trim(), ldat_TrasladoMagisterio.Rows[i]["Nemotecnico"].ToString().Trim(), String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "01/01/1900", "01/01/5000").Tables[0];

                //if (ldat_Nemotecnicos.Select("IdNemotecnico = '" + ldat_PagosCCSS.Rows[i]["Nemotecnico"].ToString() + "'").Count() != 0)
                //{
                if (ldat_TrasladoMagisterio.Rows[i]["NumeroValor"].ToString().Trim().Equals("") || ldat_TrasladoMagisterio.Rows[i]["Nemotecnico"].ToString().Trim().Equals(""))
                {
                    continue;
                }

                if (dtTitulosValores.Rows.Count == 0)
                {
                    try
                    {
                        string lstr_moneda = ldat_Nemotecnicos.Select("IdNemotecnico = '" + ldat_TrasladoMagisterio.Rows[i]["Nemotecnico"].ToString().Trim() + "'")[0]["IdMoneda"].ToString().Trim().Equals("CRCN") ? "CRC" : ldat_Nemotecnicos.Select("IdNemotecnico = '" + ldat_TrasladoMagisterio.Rows[i]["Nemotecnico"].ToString().Trim() + "'")[0]["IdMoneda"].ToString().Trim();

                        if (ldat_Nemotecnicos.Select("IdNemotecnico = '" + ldat_TrasladoMagisterio.Rows[i]["Nemotecnico"].ToString().Trim() + "'")[0]["Estado"].ToString().Trim() == "A" && lstr_moneda == ldat_TrasladoMagisterio.Rows[i]["CodigoMoneda"].ToString())
                        {
                            if (ldat_TrasladoMagisterio.Rows[i]["EstadoValor"].ToString() == "Vigente")
                            {
                                #region insertar
                                texto = wsDeudaInterna.CrearTrasladoMagisterio(
                                    ldat_TrasladoMagisterio.Rows[i]["EstadoValor"].ToString().Trim(),
                                    ldat_TrasladoMagisterio.Rows[i]["Nemotecnico"].ToString().Trim(),
                                    ldat_TrasladoMagisterio.Rows[i]["Tipo"].ToString(),
                                    ldat_TrasladoMagisterio.Rows[i]["TipoNegociacion"].ToString(),
                                    Convert.ToInt32(ldat_TrasladoMagisterio.Rows[i]["NumeroValor"].ToString().Trim()),
                                    ldat_TrasladoMagisterio.Rows[i]["CodigoMoneda"].ToString().Equals("CRC") ? "CRCN" : ldat_TrasladoMagisterio.Rows[i]["CodigoMoneda"].ToString(),
                                    Convert.ToDecimal(ldat_TrasladoMagisterio.Rows[i]["ValorFacial"].ToString()),
                                    ldat_TrasladoMagisterio.Rows[i]["FechaValor"].ToString().Substring(0, 10),
                                    ldat_TrasladoMagisterio.Rows[i]["PlazoValor"].ToString(),
                                    ldat_TrasladoMagisterio.Rows[i]["FechaCancelacion"].ToString().Substring(0, 10),
                                    ldat_TrasladoMagisterio.Rows[i]["FechaVencimiento"].ToString().Substring(0, 10),
                                    Convert.ToDecimal(ldat_TrasladoMagisterio.Rows[i]["ValorTransadoBruto"].ToString()),
                                    Convert.ToDecimal(ldat_TrasladoMagisterio.Rows[i]["ValorTransadoNeto"].ToString()),
                                    Convert.ToDecimal(ldat_TrasladoMagisterio.Rows[i]["TasaBruta"].ToString()),
                                    Convert.ToDecimal(ldat_TrasladoMagisterio.Rows[i]["TasaNeta"].ToString()),
                                    ldat_TrasladoMagisterio.Rows[i]["FechaCreacion"].ToString().Substring(0, 10),
                                    ldat_TrasladoMagisterio.Rows[i]["Propietario"].ToString(),
                                    "Traslado Cuotas Magisterio Nacional",
                                    ldat_TrasladoMagisterio.Rows[i]["MotivoAnulacion"].ToString(),
                                    Convert.ToDecimal(ldat_TrasladoMagisterio.Rows[i]["RendimientoPorDescuento"].ToString()),
                                    Convert.ToDecimal(ldat_TrasladoMagisterio.Rows[i]["Premio"].ToString()),
                                    Convert.ToDecimal(ldat_TrasladoMagisterio.Rows[i]["CuponCorrido"].ToString()),
                                    gstr_Usuario,
                                    ldat_TrasladoMagisterio.Rows[i]["ModuloSINPE"].ToString(),
                                    ldat_TrasladoMagisterio.Rows[i]["Propietario"].ToString());
                                //RAMSES
                                ws_SGService.uwsConsultarDinamico("UPDATE cf.titulosvalores SET Descripcion = 'Traslado Magisterio' where NroCupon = 0 and nrovalor = " + Convert.ToInt32(ldat_TrasladoMagisterio.Rows[i]["NumeroValor"].ToString().Trim()) + " and Nemotecnico = '" + ldat_TrasladoMagisterio.Rows[i]["Nemotecnico"].ToString().Trim() + "'");
                                ws_SGService.uwsConsultarDinamico("UPDATE cf.titulosvalores SET indicadorcupon = 'V'  where NroCupon = 0 and nrovalor = " + Convert.ToInt32(ldat_TrasladoMagisterio.Rows[i]["NumeroValor"].ToString().Trim()) + " and Nemotecnico = '" + ldat_TrasladoMagisterio.Rows[i]["Nemotecnico"].ToString().Trim() + "'");
                                //if (texto[0] == "00")
                                //{
                                    //wsDeudaInterna.CCSS CCSS = new wsDeudaInterna.CCSS();
                                    //LogicaNegocio.CalculosFinancieros.Magisterio Magisterio = new LogicaNegocio.CalculosFinancieros.Magisterio();
                                    //Magisterio.Lstr_Nemotecnico = ldat_TrasladoMagisterio.Rows[i]["Nemotecnico"].ToString().Trim();
                                    //Magisterio.Lint_NumValor = Convert.ToInt32(ldat_TrasladoMagisterio.Rows[i]["NumeroValor"].ToString().Trim());
                                    arregloMagisterio.Add(GetMagisterio(i));
                                    lbol_control1 = true;
                                //}
                                //else
                                //{
                                //   ws_SGService.uwsRegistrarAccionBitacoraCo("DI", "123", "Traslado Magisterio", "Resultado de Contabilización: \n 1 - [E] Monto superior al total de las reservas de la Deuda Interna. Reservas utilizadas: \n" + "", "", "", "G206");
                                //}
                                #endregion
                            }
                        }
                        else
                        {
                            lstr_SinNemotecnico = lstr_SinNemotecnico + ldat_TrasladoMagisterio.Rows[i]["NumeroValor"].ToString() + ", ";
                            lbol_control1 = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        lstr_SinNemotecnico = lstr_SinNemotecnico + ldat_TrasladoMagisterio.Rows[i]["NumeroValor"].ToString() + ", ";
                        lbol_control1 = true;
                    }
                }//IF
                else if (dtTitulosValores.Rows[0]["Estado"].ToString().Trim().Equals("ACT"))
                { //valida que el registro existe pero no está contabilizado
                    arregloMagisterio.Add(GetMagisterio(i));
                    lbol_control1 = true;
                }//else if
                else
                {
                    lstr_Duplicados = lstr_Duplicados + ldat_TrasladoMagisterio.Rows[i]["NumeroValor"].ToString() + ", ";
                    lbol_control2 = true;
                }//else
            }//fin del for

            string mensajefinal = "";

            if (lbol_control2)
            {
                mensajefinal = "Carga de archivo parcial. Los siguientes valores no fueron cargados: " + lstr_Duplicados + ". Estos registros ya existen en el sistema. - ";
            }
            else if (lbol_control1)
            {
                //despliega los que no fueron procesados por duplicados
                //if (lstr_Duplicados != String.Empty)
                //{
                //    mensajefinal = "Carga de archivo parcial. Los siguientes valores no fueron cargados: " + lstr_Duplicados + ". Estos registros ya existen en el sistema. - ";
                //}

                //despliega si la carga fue exitosa, o bien si hubo nemotécnicos inexistentes y cuales fueron
                if (lstr_SinNemotecnico == String.Empty)
                {
                    mensajefinal += "Carga de archivo exitosa. - ";
                }
                else
                {
                    mensajefinal += "Carga de archivo parcial. Los siguientes valores no fueron cargados: " + lstr_SinNemotecnico + "por nemotécnicos inexistentes. - ";
                }

                //string script = @"<script type='text/javascript'> alert('"+mensajefinal+"'); </script>";
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
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

        /// <summary>
        ///  Retorna el magisterio de la tabla
        /// </summary>
        private LogicaNegocio.CalculosFinancieros.Magisterio GetMagisterio(int pPosicion)
        {
            LogicaNegocio.CalculosFinancieros.Magisterio Magisterio = new LogicaNegocio.CalculosFinancieros.Magisterio();

            Magisterio.Lstr_Nemotecnico = ldat_TrasladoMagisterio.Rows[pPosicion]["Nemotecnico"].ToString().Trim();
            Magisterio.Lint_NumValor = Convert.ToInt32(ldat_TrasladoMagisterio.Rows[pPosicion]["NumeroValor"].ToString().Trim());
            return Magisterio;
        }//fin

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (fucCargaArchivo.HasFile)
            {
                string lstr_Directorio = String.Empty;
                try
                {
                    string lstr_NbrArchivo = Path.GetFileName(fucCargaArchivo.FileName);
                    if (lstr_NbrArchivo == "Magisterio_" + gstr_Usuario + ".xlsx")
                    {
                        fucCargaArchivo.SaveAs(Server.MapPath("~/CalculosFinancieros/DeudaInterna/ArchivosDI/") + lstr_NbrArchivo);
                        lstr_Directorio = Server.MapPath("~/CalculosFinancieros/DeudaInterna/ArchivosDI/") + lstr_NbrArchivo;
                        ldat_TrasladoMagisterio = exceldata(lstr_Directorio);

                        if (ldat_TrasladoMagisterio.Rows.Count == 0)
                        {
                            string script = @"<script type='text/javascript'> alert('El archivo no contiene registros.'); </script>";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
                        }
                        else
                        {
                            string script = @"<script type='text/javascript'> alert('Archivo cargado y listo para procesar.'); </script>";
                            ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);

                            grvMagisterio.DataSource = ldat_TrasladoMagisterio;
                            grvMagisterio.DataBind();
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
            grvMagisterio.PageIndex = e.NewPageIndex;
            grvMagisterio.DataSource = ldat_TrasladoMagisterio;
            grvMagisterio.DataBind();
        }

        public DataTable RegistroContable()
        {
            DataTable ldat_Asiento = new DataTable();

            try
            {
                ldat_Asiento.Columns.Add("Referencia");
                ldat_Asiento.Columns.Add("Fecha");
                ldat_Asiento.Columns.Add("Cuenta");
                ldat_Asiento.Columns.Add("ClaveContable");
                ldat_Asiento.Columns.Add("Moneda");
                ldat_Asiento.Columns.Add("TextoInfo");
                ldat_Asiento.Columns.Add("CentroCosto");
                ldat_Asiento.Columns.Add("CentroBeneficio");
                ldat_Asiento.Columns.Add("ElementoPEP");
                ldat_Asiento.Columns.Add("PosPre");
                ldat_Asiento.Columns.Add("CentroGestor");
                ldat_Asiento.Columns.Add("Fondo");
                ldat_Asiento.Columns.Add("DocPres");
                ldat_Asiento.Columns.Add("PosDocPres");
                ldat_Asiento.Columns.Add("Monto");
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return ldat_Asiento;
        }

        public void ColocaTrasladoMag(
            string lstr_Tipo,
            string lstr_EstadoValor,
            DateTime ldt_FchValor,
            DateTime ldt_FchVencimiento,
            string lstr_Propietario,
            string lstr_Plazo,
            decimal ldec_ValorTransadoBruto,

            string lstr_Moneda,
            string lstr_NroValor,
            string lstr_Nemotecnico,
            decimal ldec_ValorFacial,
            decimal ldec_RendimientoXDescuento,
            decimal ldec_CuponCorrido,
            decimal ldec_ValorTransadoNeto,
            string lstr_Detalle)
        {
            wsDeudaInterna.wsDeudaInterna wsDeudaInterna = new wsDeudaInterna.wsDeudaInterna();
            DataTable ldat_Asiento = RegistroContable();
            DataSet ldat_Reservas = new DataSet();
            DataTable ldat_Tira = new DataTable();
            int lint_EsPublico = 1;
            string lstr_Operacion = string.Empty;
            //string lstr_Moneda = wsDeudaInterna.ConsultarTitulosValores(lstr_NroValor, lstr_Nemotecnico, "%", "%", "%", "01/01/1900", "01/01/5000").Tables[0].Rows[0]["Moneda"].ToString();
            decimal ldec_monto = 0;
            if (ws_SGService.uwsConsultarPropietarios(string.Empty, string.Empty, string.Empty, lstr_Propietario).Tables[0].Rows.Count == 0)
            {
                lint_EsPublico = 2;
            }

            //se tratan las colocaciones de las tres diferentes operaciones, títulos cero cupón, de tasa fija y tasa variable.
            if (lstr_EstadoValor == "Vigente")
            {
                #region Magisterio
                //Define si no trasciende en el periodo
                if ((ldt_FchValor.Year == ldt_FchVencimiento.Year))
                {
                    //Solo se manejan UDES, hay que convertirlo a colones para almacenarlo
                    lstr_Operacion = "ID49";
                    decimal ldec_Udes = Math.Round(Convert.ToDecimal(ws_SGService.uwsConsultarTiposCambio("UDE", DateTime.Today, "", "").Tables[0].Rows[0]["Valor"].ToString()), 2);

                    ldat_Tira = ws_SGService.uwsConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "CRC", lstr_Nemotecnico, "", "ID").Tables[0].Select("EsNemotecnico='5' AND EsPubPriv='" + lint_EsPublico + "'").CopyToDataTable();

                    foreach (DataRow ldr_Row in ldat_Tira.Rows)
                    {
                        int index = ldat_Tira.Rows.IndexOf(ldr_Row);

                        switch (index)
                        {
                            case 0: { ldec_monto = ldec_ValorFacial * ldec_Udes; break; }
                            case 1: { ldec_monto = ldec_CuponCorrido * ldec_Udes; break; }
                            case 2: { ldec_monto = ldec_RendimientoXDescuento * ldec_Udes; break; }
                            case 3: { ldec_monto = ldec_ValorTransadoNeto * ldec_Udes; break; }
                        }

                        string lstr_NuevoPosPreDevengo = string.Empty;
                        if (ldat_Tira.Rows[0]["IdPosPre"].ToString().Trim().Equals("PP_Balance"))
                        {
                            ldat_Reservas = ws_SGService.uwsConsultarReservaDetallado(string.Empty, string.Empty, string.Empty, string.Empty
                                , string.Empty, string.Empty, string.Empty, string.Empty
                                , string.Empty, string.Empty, "N","N","N",string.Empty);
                            DataView dv = ldat_Reservas.Tables[0].DefaultView;
                            dv.Sort = "OrdenDeudaInterna ASC";

                            foreach (DataRow drForm in dv.ToTable().Rows)
                            {
                                if (!drForm["OrdenDeudaInterna"].ToString().Equals(string.Empty))
                                    //ldat_AsientoDevengo
                                    if (Convert.ToInt32(drForm["Monto"]) > 0 && !drForm["OrdenDeudaInterna"].ToString().Equals("0")
                                        && Convert.ToDecimal(drForm["Monto"]) >= ldec_monto)
                                    {
                                        lstr_NuevoPosPreDevengo = drForm["IdPosPre"].ToString().Trim();
                                        break;
                                    }
                            }
                        }


                        ldat_Asiento.Rows.Add(
                            lstr_NroValor + " " + lstr_Nemotecnico,
                            ldt_FchValor.ToString("dd.MM.yyyy"),
                            ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                            ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                            ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                            lstr_Detalle.Trim(),
                            ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                            ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                            ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                            lstr_NuevoPosPreDevengo.Equals(string.Empty) ? ldat_Tira.Rows[0]["IdPosPre"].ToString().Trim() : lstr_NuevoPosPreDevengo,
                            ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                            ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                            ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                            ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                            Math.Round(ldec_monto, 2));
                    }
                }
                //Define si el título es a corto plazo, pero trasciende en el periodo
                else if ((ldt_FchValor.Year != ldt_FchVencimiento.Year))
                {
                    //Solo se manejan UDES, hay que convertirlo a colones para almacenarlo
                    lstr_Operacion = "ID50";
                    decimal ldec_Udes = Math.Round(Convert.ToDecimal(ws_SGService.uwsConsultarTiposCambio("UDE", DateTime.Today, "", "").Tables[0].Rows[0]["Valor"].ToString()), 2);

                    ldat_Tira = ws_SGService.uwsConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "CRC", lstr_Nemotecnico, "", "ID").Tables[0].Select("EsNemotecnico='5' AND EsPubPriv='" + lint_EsPublico + "'").CopyToDataTable();

                    foreach (DataRow ldr_Row in ldat_Tira.Rows)
                    {
                        int index = ldat_Tira.Rows.IndexOf(ldr_Row);

                        switch (index)
                        {
                            case 0: { ldec_monto = ldec_ValorFacial * ldec_Udes; break; }
                            case 1: { ldec_monto = ldec_CuponCorrido * ldec_Udes; break; }
                            case 2: { ldec_monto = ldec_RendimientoXDescuento * ldec_Udes; break; }
                            case 3: { ldec_monto = ldec_ValorTransadoNeto * ldec_Udes; break; }
                        }

                        string lstr_NuevoPosPreDevengo = string.Empty;
                        if (ldat_Tira.Rows[0]["IdPosPre"].ToString().Trim().Equals("PP_Balance"))
                        {
                            ldat_Reservas = ws_SGService.uwsConsultarReservaDetallado(string.Empty, string.Empty, string.Empty, string.Empty
                                , string.Empty, string.Empty, string.Empty, string.Empty
                                , string.Empty, string.Empty, "N","N","N",string.Empty);
                            DataView dv = ldat_Reservas.Tables[0].DefaultView;
                            dv.Sort = "OrdenDeudaInterna ASC";

                            foreach (DataRow drForm in dv.ToTable().Rows)
                            {
                                if (!drForm["OrdenDeudaInterna"].ToString().Equals(string.Empty))
                                    //ldat_AsientoDevengo
                                    if (Convert.ToInt32(drForm["Monto"]) > 0 && !drForm["OrdenDeudaInterna"].ToString().Equals("0")
                                        && Convert.ToDecimal(drForm["Monto"]) >= ldec_monto)
                                    {
                                        lstr_NuevoPosPreDevengo = drForm["IdPosPre"].ToString().Trim();
                                        break;
                                    }
                            }
                        }


                        ldat_Asiento.Rows.Add(
                            lstr_NroValor + " " + lstr_Nemotecnico,
                            ldt_FchValor.ToString("dd.MM.yyyy"),
                            ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                            ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                            ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                            lstr_Detalle.Trim(),
                            ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                            ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                            ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                            ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                            ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                            ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                            ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                            ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                            Math.Round(ldec_monto, 2));
                    }
                }
                #endregion
                GenerarAsientoAjuste(ldat_Asiento, lstr_Operacion, lstr_NroValor, lstr_Nemotecnico);
            }
        }

        public string GenerarAsientoAjuste(DataTable ldat_Asiento, string lstr_IdOperacion, string lstr_NroValor, string lstr_Nemotecnico)
        {
            //Coleccion de asientos y tipos de asientos requeridos en SAP expuestos por la referencia del servicio
            wsAsientos.ZfiAsiento item_asiento = new wsAsientos.ZfiAsiento();
            wsAsientos.ZfiAsiento[] tabla_asientos = new wsAsientos.ZfiAsiento[ldat_Asiento.Rows.Count];

            //variables de proceso
            string[] item_resAsientosLog = new string[10];
            string logAsiento = string.Empty;
            string flagEstadoAsiento = string.Empty;

            int cont = 0;

            //string lstr_Moneda = dbMoneda.SelectedValue;
            //string lstr_Referencia = txtReferencia.Text;

            try
            {
                foreach (DataRow ldr_Row in ldat_Asiento.Rows)
                {
                    item_asiento = new wsAsientos.ZfiAsiento();
                    int index = ldat_Asiento.Rows.IndexOf(ldr_Row);

                    if (index == 0)
                    {
                        item_asiento.Blart = "ID";//Clase de documento
                        item_asiento.Bukrs = "G206";//Sociedad
                        //item_asiento.Werks = ldat_Asiento.Rows[0]["Referencia"].ToString();
                        item_asiento.Bldat = ldat_Asiento.Rows[ldat_Asiento.Rows.IndexOf(ldr_Row)]["Fecha"].ToString();//Fecha de documento
                        item_asiento.Budat = ldat_Asiento.Rows[ldat_Asiento.Rows.IndexOf(ldr_Row)]["Fecha"].ToString();//Fecha de contabilización
                    }

                    item_asiento.Waers = ldat_Asiento.Rows[index]["Moneda"].ToString();//Moneda 
                    item_asiento.Bschl = ldat_Asiento.Rows[index]["ClaveContable"].ToString();//Clave de contabilización
                    item_asiento.Hkont = ldat_Asiento.Rows[index]["Cuenta"].ToString();//Cuenta de mayor
                    item_asiento.Wrbtr = Convert.ToDecimal(ldat_Asiento.Rows[index]["Monto"].ToString());//Importe
                    item_asiento.Sgtxt = ldat_Asiento.Rows[index]["TextoInfo"].ToString();//Texto Informativo (50 caracteres)
                    item_asiento.Kostl = ldat_Asiento.Rows[index]["CentroCosto"].ToString();//Centro de Costo
                    item_asiento.Prctr = ldat_Asiento.Rows[index]["CentroBeneficio"].ToString();//Centro de Beneficio
                    item_asiento.Projk = ldat_Asiento.Rows[index]["ElementoPEP"].ToString();//Elemento PEP
                    item_asiento.Xref2Hd = ldat_Asiento.Rows[index]["PosPre"].ToString();//Posición Presupuestaria
                    item_asiento.Fistl = ldat_Asiento.Rows[index]["CentroGestor"].ToString();//Centro Gestor
                    item_asiento.Geber = ldat_Asiento.Rows[index]["Fondo"].ToString();//Fondo
                    item_asiento.Kblnr = ldat_Asiento.Rows[index]["DocPres"].ToString();//Documento Presupuestario
                    item_asiento.Kblpos = ldat_Asiento.Rows[index]["PosDocPres"].ToString();//Posición de documento presupuestario

                    tabla_asientos[index] = item_asiento;
                }

                //Cargar de Asientos 
                string[] concatenado = new string[8];
                //envio de asiento mediante servicio web hacia SIGAF
                //item_resAsientosLog = asientos.EnviarAsientos(tabla_asientos); *cucurucho
                item_resAsientosLog = asientos.EnviarAsientos(tabla_asientos, "");
                for (int j = 0; j < item_resAsientosLog.Length; j++)
                {
                    int x = j + 1;
                    logAsiento += x + " - " + item_resAsientosLog[j] + " - ";
                }
                //Registrar en Bitacora de movimientos
                ws_SGService.uwsRegistrarAccionBitacoraCo("DI", gstr_Usuario, "Colocación Magisterio", "Resultado de Contabilización: " + logAsiento, lstr_IdOperacion, lstr_NroValor + "-" + lstr_Nemotecnico, "");
                return logAsiento;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        protected void btnContabilizar_Click(object sender, EventArgs e)
        {
            string script = "";
            string Msj = "";
            string resultado = "";
            try
            {
                foreach (LogicaNegocio.CalculosFinancieros.Magisterio magisterio in arregloMagisterio)
                {
                    Msj = wsDeudaInterna.ContabilizaMagisterio(magisterio.Lint_NumValor, magisterio.Lstr_Nemotecnico);
                    ws_SGService.uwsRegistrarAccionBitacoraCo("DI", "123", "Traslado Magisterio", magisterio.Lint_NumValor + " - " + magisterio.Lstr_Nemotecnico, "", "", "G206");
                }
                 resultado = (arregloMagisterio.Count > 0) ? "Registros contabilizados correctamente" : "Operación finalizada, no habían datos por contabilizar."; 
                 //script = @"<script type='text/javascript'> alert('Contabilizado correctamente'); </script>";
            }
            catch (Exception ex)
            {
                resultado="Error de contabilización."+ex.ToString(); 
                //script = @"<script type='text/javascript'> alert('Error de contabilización.'"+ex.ToString()+"); </script>";
            }
            script = @"<script type='text/javascript'> alert('"+resultado+"'); </script>";
            
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
        }
    }
}