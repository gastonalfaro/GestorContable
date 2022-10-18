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
using System.Web.UI.WebControls;
using Presentacion.Compartidas;
using System.IO;
using System.Collections;
using System.Drawing;
using System.Threading;

namespace Presentacion.CalculosFinancieros.DeudaInterna
{
    public partial class frmReporteDevengo : BASE
    {
        # region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor(); 
        private wsDI.wsDeudaInterna wsDInterna = new wsDI.wsDeudaInterna();
        private string gstr_Usuario = String.Empty;
        private DataTable ldat_DevengoInteres = new DataTable();
        private DataTable ldat_FlujoEfectivo = new DataTable();
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
                        if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "OBJ_DI"))
                            Response.Redirect("~/Principal.aspx", true);
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

        protected void RealizarConsulta()
        {
            try
            {
                wsDeudaInterna.wsDeudaInterna wsDeudaInterna = new Presentacion.wsDeudaInterna.wsDeudaInterna();
                ldat_DevengoInteres = wsDeudaInterna.ConsultarDevengoIntereses(ddlNroValor.Text, ddlNemotecnico.Text).Tables[0];
                ldat_FlujoEfectivo = wsDeudaInterna.ConsultarFlujoEfectivo(ddlNroValor.Text, ddlNemotecnico.Text).Tables[0];
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {

        }

        protected void grvOperacionesEspeciales_Sorted(object sender, System.Web.UI.WebControls.GridViewSortEventArgs e)
        {
            RealizarConsulta();
            ldat_DevengoInteres.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
            grvDevengo.DataSource = ldat_DevengoInteres;
            grvDevengo.DataBind();
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

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        public void ExportarExcel(GridView grvExportar, string NombreReporte)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=" + NombreReporte + ".xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);

                    //To Export all pages
                    grvExportar.AllowPaging = false;
                    //this.BindGrid();

                    grvExportar.HeaderRow.BackColor = Color.White;
                    foreach (TableCell cell in grvExportar.HeaderRow.Cells)
                    {
                        cell.BackColor = grvExportar.HeaderStyle.BackColor;
                    }
                    foreach (GridViewRow row in grvExportar.Rows)
                    {
                        row.BackColor = Color.White;
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = grvExportar.AlternatingRowStyle.BackColor;
                            }
                            else
                            {
                                cell.BackColor = grvExportar.RowStyle.BackColor;
                            }
                            cell.CssClass = "textmode";
                        }
                    }

                    grvExportar.RenderControl(hw);

                    //style to format numbers to string
                    string style = @"<style> .textmode { } </style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
            }
            catch (ThreadAbortException ex)
            {
                ex.ToString();
            }
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                ExportarExcel(grvFlujo, "ReporteFlujoEfectivo");
            }
            catch(Exception ex)
            {
                ex.ToString();
            }
        }

        protected void btnReporte_Click(object sender, EventArgs e)
        {
            RealizarConsulta();
            grvDevengo.DataSource = ldat_DevengoInteres;
            grvDevengo.DataBind();
            grvFlujo.DataSource = ldat_FlujoEfectivo;
            grvFlujo.DataBind();


        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlNemotecnico_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlNroValor.ClearSelection();
            this.ddlNroValor.Dispose();
            this.ddlNroValor.Items.Clear();
            this.ddlNroValor.Items.Insert(0, (new ListItem("-- Seleccione--", "")));
            if (!string.IsNullOrEmpty(this.ddlNemotecnico.SelectedValue.Trim()))
            {
                DataSet ds_Titulos = new DataSet();
                ds_Titulos = wsDInterna.ConsultarTitulosValores(String.Empty, this.ddlNemotecnico.SelectedValue.Trim(), String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "01/01/1900", "01/01/5000");
                this.ddlNroValor.DataTextField =
                this.ddlNroValor.DataValueField = "NroValor";
                if (ds_Titulos.Tables.Count > 0 && ds_Titulos.Tables[0].Rows.Count > 0)
                {
                    this.ddlNroValor.DataSource = ds_Titulos;
                    this.ddlNroValor.DataBind();
                }//if
            }
        }

        protected void btnExcelDevengo_Click(object sender, EventArgs e)
        {
            try
            {
                ExportarExcel(grvDevengo, "ReporteDevengo");
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        protected void btnExcelDevengoMens_Click(object sender, EventArgs e)
        {
            try
            {
                ExportarExcel(grvDevengoMens, "ReporteDevengoMensual");
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
    }
}