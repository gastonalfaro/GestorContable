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
using System.Collections;
using System.Configuration;
//using System.Data.SqlClient;
using System.Drawing;
using Microsoft.Reporting.WebForms;
using eWorld.UI;
using Presentacion.Compartidas;
using Presentacion.Compartidas.VisorReportes;
using System.Diagnostics;
using System.Reflection;

namespace Presentacion.CalculosFinancieros.DeudaInterna
{
    public partial class frmReporteDevengosInt : BASE
    {
        # region Variables
        private Presentacion.wsDeudaInterna.wsDeudaInterna wsDeudaInterna = new Presentacion.wsDeudaInterna.wsDeudaInterna();
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private string gstr_Usuario = String.Empty;
      //private static DataTable ldat_Cancelaciones = new DataTable();
        private string gstr_ModuloActual = String.Empty;
        # endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                gstr_Usuario = clsSesion.Current.LoginUsuario;

                if (!IsPostBack)
                {
                    CargarNemotecnico();
                    if (!string.IsNullOrEmpty(gstr_Usuario))
                    {
                        if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmReporteDevensInt"))
                            Response.Redirect("~/Principal.aspx", true);
                        else
                            PanelReporte.Visible = false;
                    }
                    else
                        Response.Redirect("~/Login.aspx", true);
                }
            }
            catch (Exception ex)
            {
                //lblEstatus.Text = ex.ToString();
                //Response.Redirect("~/Login.aspx", true);
            }
        }

        private void CargarNemotecnico()
        {
            DataSet lNemotecnicos = ws_SGService.uwsConsultarNemotecnicos("", "", "", "", "");
            this.ddlNemotecnico.DataSource = lNemotecnicos;
            this.ddlNemotecnico.DataTextField = "IdNemotecnico";
            this.ddlNemotecnico.DataValueField = "IdNemotecnico";
            this.ddlNemotecnico.DataBind();
        }

        //protected void RealizarConsulta()
        //{
        //    wsDeudaInterna.wsDeudaInterna wsDeudaInterna = new Presentacion.wsDeudaInterna.wsDeudaInterna();
        //    ldat_Cancelaciones.Clear();
        //    DataTable ldat_Temp = new DataTable();
        //    bool lbol_filtrado = false;
        //    DataRow[] ldar_Temp;

        //    try
        //    {
        //        string lstr_FchInicio = "01/01/1000";
        //        string lstr_FchFin = "01/01/5000";
        //        ArrayList lint_NrosEliminar = new ArrayList();

        //        if (txtFchDesde.Text != "")
        //        {
        //            lstr_FchInicio = txtFchDesde.Text;
        //        }
        //        if (txtFchHasta.Text != "")
        //        {
        //            lstr_FchFin = txtFchHasta.Text;
        //        }

        //        ldat_Cancelaciones = wsDeudaInterna.ConsultarTitulosValores(txtDescripcion.Text, lstr_FchInicio, lstr_FchFin).Tables[0];

        //        if (ddlTipoNegociacion.SelectedValue != "0" && ddlNemotecnico.SelectedValue != "0")
        //        {
        //            ldar_Temp = ldat_Cancelaciones.Select("TipoNegociacion = '" + ddlTipoNegociacion.SelectedValue + "' and Nemotecnico = '" + ddlNemotecnico.SelectedValue + "'");
        //            ldat_Temp = ldar_Temp.CopyToDataTable();
        //            lbol_filtrado = true;
        //        }
        //        else
        //        {
        //            if (ddlTipoNegociacion.SelectedValue != "0" && ddlNemotecnico.SelectedValue == "0")
        //            {
        //                ldar_Temp = ldat_Cancelaciones.Select("TipoNegociacion = '" + ddlTipoNegociacion.SelectedValue + "'");
        //                ldat_Temp = ldar_Temp.CopyToDataTable();
        //                lbol_filtrado = true;
        //            }
        //            else
        //            {
        //                if (ddlTipoNegociacion.SelectedValue == "0" && ddlNemotecnico.SelectedValue != "0")
        //                {
        //                    ldar_Temp = ldat_Cancelaciones.Select("Nemotecnico = '" + ddlNemotecnico.SelectedValue + "'");
        //                    ldat_Temp = ldar_Temp.CopyToDataTable();
        //                    lbol_filtrado = true;
        //                }
        //            }

        //        }

        //        grvCancelaciones.Dispose();

        //        if (lbol_filtrado)
        //        {
        //            ldat_Cancelaciones = ldat_Temp;
        //        }


        //        //Código para agregar totales:
        //        decimal totalValorFacial = 0;
        //        decimal totalTransadoBruto = 0;
        //        decimal totalTransadoNeto = 0;
        //        decimal totalDescuentoPrima = 0;

        //        for (int i = 0; i < ldat_Cancelaciones.Rows.Count; i++)
        //        {
        //            totalValorFacial += Convert.ToDecimal(ldat_Cancelaciones.Rows[i]["ValorFacial"].ToString());
        //            totalTransadoBruto += Convert.ToDecimal(ldat_Cancelaciones.Rows[i]["ValorTransadoBruto"].ToString());
        //            totalTransadoNeto += Convert.ToDecimal(ldat_Cancelaciones.Rows[i]["ValorTransadoNeto"].ToString());
        //            totalDescuentoPrima += Convert.ToDecimal(ldat_Cancelaciones.Rows[i]["RendimientoPorDescuento"].ToString());
        //        }

        //        lblFacial.Text = totalValorFacial.ToString("N2");
        //        lblBruto.Text = totalTransadoBruto.ToString("N2");
        //        lblNeto.Text = totalTransadoNeto.ToString("N2");
        //        lblDescuento.Text = totalDescuentoPrima.ToString("N2");


        //        grvCancelaciones.DataSource = ldat_Cancelaciones;
        //        grvCancelaciones.DataBind();
        //    }
        //    catch
        //    {
        //        grvCancelaciones.Dispose();
        //        grvCancelaciones.DataBind();
        //    }
        //}

        //protected void OcultaCampos(bool lchk_control, int lint_columna)
        //{
        //    if (lchk_control)
        //    {
        //        grvCancelaciones.Columns[lint_columna].Visible = true;
        //    }
        //    else
        //    {
        //        grvCancelaciones.Columns[lint_columna].Visible = false;
        //    }
        //    RealizarConsulta();
        //}

        protected void btnBusqueda_Click(object sender, EventArgs e)
        {

        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                PanelReporte.Visible = true;
                string lstr_Nemotecnico, lstr_NroValor;
                //string  ldt_FechaInicio, ldt_FechaFin;
                string strUsuario = clsSesion.Current.LoginUsuario;

                //strUsuario = User.Identity.Name;
                //strUsuario = gstr_Usuario;
                //strIdEntidad = CatalogoEntidadesDropDownList.SelectedValue;
                //FechaInicio = FechaInicioCalendarPopup.SelectedDate;
                //FechaFin = FechaFinCalendarPopup.SelectedDate;
                //strPeriodo = PeriodoDropDownList.SelectedItem.Text;
                //strUnidadTiempoPeriodo = UnidadTiempoPeriodoDropDownList.SelectedValue;
                lstr_Nemotecnico = ddlNemotecnico.SelectedValue;
                lstr_NroValor = ddlNumValor.SelectedValue;
                //lstr_Tipo = ddlTipoValor.SelectedValue;
                //lstr_TipoNegociacion = ddlTipoNegociacion.SelectedValue;
                //ldt_FechaInicio = calFechaDesde.SelectedDate.ToString();
                //ldt_FechaFin = calFechaHasta.SelectedDate.ToString();


                //strFechaInicio = String.Format("{0:yyyy}", ldt_FechaInicio) + '/' + String.Format("{0:MM}", ldt_FechaInicio) + '/' + String.Format("{0:dd}", ldt_FechaInicio) + ' ' +
                //    String.Format("{0:HH}", ldt_FechaInicio) + ':' +
                //    String.Format("{0:mm}", ldt_FechaInicio) + ':' +
                //    String.Format("{0:ss}", ldt_FechaInicio) + '.' +
                //    String.Format("{0:fff}", ldt_FechaInicio);

                //strFechaFin = String.Format("{0:yyyy}", ldt_FechaFin) + '/' + String.Format("{0:MM}", ldt_FechaFin) + '/' + String.Format("{0:dd}", ldt_FechaFin) + ' ' +
                //    String.Format("{0:HH}", ldt_FechaFin) + ':' +
                //    String.Format("{0:mm}", ldt_FechaFin) + ':' +
                //    String.Format("{0:ss}", ldt_FechaFin) + '.' +
                //    String.Format("{0:fff}", ldt_FechaFin);

                //ParametrosReporte oParam = new ParametrosReporte();
                List<ReportParameter> oParametros = new List<ReportParameter>();
                if (!strUsuario.Equals(string.Empty))// && (!str_Institucion.Equals(string.Empty)))
                {
                    oParametros.Add(new ReportParameter("pNemotecnico", lstr_Nemotecnico, false));
                    oParametros.Add(new ReportParameter("pNumValor", lstr_NroValor, false));
                    //oParametros.Add(new ReportParameter("pTipo", lstr_Tipo, false));
                    //oParametros.Add(new ReportParameter("pTipoNegociacion", lstr_TipoNegociacion, false));
                    //oParametros.Add(new ReportParameter("pFchInicio", ldt_FechaInicio, false));
                    //oParametros.Add(new ReportParameter("pFchFin", ldt_FechaFin, false));

                    ParametrosReporte oParam = new ParametrosReporte(ConfigurationManager.AppSettings["ReporteDevengo"], oParametros, ConfigurationManager.AppSettings["ServidorReportes"]);

                    //oParam.DireccionReporte = ConfigurationManager.AppSettings["ReporteBitacoraFlowError"];
                    //oParam.Parametros = oParametros;
                    //oParam.ServidorReportes = ConfigurationManager.AppSettings["ServidorReportes"];
                    Session.Add("ParametrosReporte", oParam);
                    //EntregaATiempoMultiView.ActiveViewIndex = 1;
                    //this.ViewEntregaATiempoLinkButton.Visible = true;
                    //this.ViewReporteLinkButton.Visible = true;
                    //this.SeparadorEntregaATiempoReporteLabel.Visible = true;
                }
            }
            catch (Exception ex)
            {
                #region MensajeError
                EventLog.WriteEntry(ConfigurationManager.AppSettings["EventLogSource"].ToString(),
                    //Obtiene el nombre de la clase.
                "NICSP"
                    //Nombre del método.
                + "." + MethodInfo.GetCurrentMethod().Name
                    //Error especifico.
                + ": Excepcion  " + ex.Message.ToString() + ". ",
                EventLogEntryType.Error);

                //lblError.Text = "NICSP Excepcion:  " + ex.Message.ToString() + ". ";
                //DesplegarError(true);
                #endregion
            }
            finally
            {

            }
        }

        protected void ddlNemotecnico_DataBinding(object sender, EventArgs e)
        {

        }

        private string GetSortDirection(string column)
        {

            // By default, set the sort direction to ascending.
            string sortDirection = "ASC";

            // Retrieve the last column that was sorted.
            string sortExpression = ViewState["SortExpression"] as string;

            if (sortExpression != null)
            {
                // Check if the same column is being sorted.
                // Otherwise, the default value can be returned.
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }

            // Save new values in ViewState.
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;

            return sortDirection;
        }

        protected void ddlTipoValor_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlNemotecnico.Items.Clear();
            ddlNemotecnico.DataBind();
            ddlNemotecnico.Items.Insert(0, (new ListItem("-- Seleccione Opción --", "0")));
        }

        protected void grvCancelaciones_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {

        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void ddlNemotecnico_SelectedIndexChanged(object sender, EventArgs e)
        {
           // DataSet ds = wsDeudaInterna.ConsultarTitulosValores(string.Empty, this.ddlNemotecnico.SelectedValue.Trim(), String.Empty, String.Empty, "V", String.Empty, String.Empty, "Vigente", "01/01/1900", "01/01/5000");

            this.ddlNumValor.Items.Clear();
            //this.ddlNumValor.DataSource = wsDeudaInterna.ConsultarTitulosValores(string.Empty, this.ddlNemotecnico.SelectedValue, string.Empty, string.Empty, "V", string.Empty, string.Empty, string.Empty, "01/01/1900", "01/01/5000");
            DataSet ds_Dinamico = new DataSet();
            ds_Dinamico= ws_SGService.uwsConsultarDinamico("select distinct(NroValor) from cf.titulosvalores where Nemotecnico = '" + this.ddlNemotecnico.SelectedValue.Trim() + "' and indicadorcupon = 'V' order by nrovalor asc");
            this.ddlNumValor.DataTextField =
            this.ddlNumValor.DataValueField = "NroValor";
            if(ds_Dinamico.Tables.Count>0 && ds_Dinamico.Tables[0].Rows.Count>0)
            {
                this.ddlNumValor.DataSource = ds_Dinamico; 
                this.ddlNumValor.DataBind();
            }//if
            this.ddlNumValor.Items.Insert(0, (new ListItem("-- Seleccione Valor --", "")));
        }
        //protected void btnExcel_Click(object sender, EventArgs e)
        //{
        //    Response.Clear();
        //    Response.Buffer = true;
        //    Response.AddHeader("content-disposition", "attachment;filename=ReporteCancelaciones.xls");
        //    Response.Charset = "";
        //    Response.ContentType = "application/vnd.ms-excel";
        //    using (StringWriter sw = new StringWriter())
        //    {
        //        HtmlTextWriter hw = new HtmlTextWriter(sw);

        //        //To Export all pages
        //        grvCancelaciones.AllowPaging = false;
        //        //this.BindGrid();

        //        grvCancelaciones.HeaderRow.BackColor = Color.White;
        //        foreach (TableCell cell in grvCancelaciones.HeaderRow.Cells)
        //        {
        //            cell.BackColor = grvCancelaciones.HeaderStyle.BackColor;
        //        }
        //        foreach (GridViewRow row in grvCancelaciones.Rows)
        //        {
        //            row.BackColor = Color.White;
        //            foreach (TableCell cell in row.Cells)
        //            {
        //                if (row.RowIndex % 2 == 0)
        //                {
        //                    cell.BackColor = grvCancelaciones.AlternatingRowStyle.BackColor;
        //                }
        //                else
        //                {
        //                    cell.BackColor = grvCancelaciones.RowStyle.BackColor;
        //                }
        //                cell.CssClass = "textmode";
        //            }
        //        }

        //        grvCancelaciones.RenderControl(hw);

        //        //style to format numbers to string
        //        string style = @"<style> .textmode { } </style>";
        //        Response.Write(style);
        //        Response.Output.Write(sw.ToString());
        //        Response.Flush();
        //        Response.End();
        //    }
        //}
    }
}