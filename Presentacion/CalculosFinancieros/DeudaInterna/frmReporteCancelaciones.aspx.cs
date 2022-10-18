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
using System.Configuration;
//using System.Data.SqlClient;
using System.Drawing;

namespace Presentacion.CalculosFinancieros.DeudaInterna
{
    public partial class frmReporteCancelaciones : BASE
    {
        # region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private string gstr_Usuario = String.Empty;
        private static DataTable ldat_Cancelaciones = new DataTable();
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
                        CargarDDLs();
                        if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "OBJ_DI"))
                            Response.Redirect("~/Principal.aspx", true);
                        else
                            RealizarConsulta();
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

        private void CargarNemotecnicos(string lstr_TipoValor)
        {
            DataSet lNemotecnicos = ws_SGService.uwsConsultarDinamico("SELECT [IdNemotecnico], CONCAT ([NomNemotecnico], ' (', REPLACE([IdNemotecnico],' ', ''), ')') AS [NomNemotecnico]FROM [ma].[Nemotecnicos] WHERE [TipoNemotecnico] = '" + lstr_TipoValor + "'");
            this.ddlNemotecnico.DataSource = lNemotecnicos;
            this.ddlNemotecnico.DataTextField = "NomNemotecnico";
            this.ddlNemotecnico.DataValueField = "IdNemotecnico";
            this.ddlNemotecnico.DataBind();
            ddlTipoValor.Items.Insert(0, new ListItem("-Seleccione-", ""));
        }
        private void CargarDDLs()
        {

            DataSet lds_TipoValor = ws_SGService.uwsConsultarDinamico("SELECT [ValOpcion], [NomOpcion] FROM [cf].[vTiposValores]");
            if (lds_TipoValor.Tables.Count > 0)
            {
                DataTable ldt_TipoValor = lds_TipoValor.Tables["Table"];
                ddlTipoValor.DataSource = ldt_TipoValor;
                ddlTipoValor.DataTextField = "NomOpcion";
                ddlTipoValor.DataValueField = "ValOpcion";
                ddlTipoValor.DataBind();
                ddlTipoValor.Items.Insert(0, new ListItem("-Seleccione-", ""));
            }
            DataSet lds_TipoNegociacion = ws_SGService.uwsConsultarDinamico("SELECT [NomOpcion], [ValOpcion] FROM [cf].[vTiposNegociacion]");
            if (lds_TipoNegociacion.Tables.Count > 0)
            {
                DataTable ldt_TipoNegociacion = lds_TipoNegociacion.Tables["Table"];
                ddlTipoNegociacion.DataSource = ldt_TipoNegociacion;
                ddlTipoNegociacion.DataTextField = "NomOpcion";
                ddlTipoNegociacion.DataValueField = "ValOpcion";
                ddlTipoNegociacion.DataBind();
                ddlTipoNegociacion.Items.Insert(0, new ListItem("-Seleccione-", ""));
            }
            //DataSet lds_Monedas = ws_SGService.uwsConsultarDinamico("SELECT [IdMoneda], [NomMoneda] FROM [ma].[Monedas] WHERE [IdMoneda] IN ('USD','CRC','UDE','EUR')  order by [NomMoneda]");
            //if (lds_Monedas.Tables.Count > 0)
            //{
            //    DataTable ldt_Monedas = lds_Monedas.Tables["Table"];
            //    ddlMoneda.DataSource = ldt_Monedas;
            //    ddlMoneda.DataTextField = "NomMoneda";
            //    ddlMoneda.DataValueField = "IdMoneda";
            //    ddlMoneda.DataBind();
            //    ddlMoneda.Items.Insert(0, new ListItem("-Seleccione-", ""));
            //}
        }
        protected void RealizarConsulta()
        {
            wsDeudaInterna.wsDeudaInterna wsDeudaInterna = new Presentacion.wsDeudaInterna.wsDeudaInterna();
            ldat_Cancelaciones.Clear();
            DataTable ldat_Temp = new DataTable();
            bool lbol_filtrado = false;
            DataRow[] ldar_Temp;

            try
            {
                string lstr_FchInicio = "01/01/1000";
                string lstr_FchFin = "01/01/5000";
                ArrayList lint_NrosEliminar = new ArrayList();

                if (txtFchDesde.Text != "")
                {
                    lstr_FchInicio = txtFchDesde.Text;
                }
                if (txtFchHasta.Text != "")
                {
                    lstr_FchFin = txtFchHasta.Text;
                }

                ldat_Cancelaciones = null;
                //ldat_Cancelaciones = wsDeudaInterna.ConsultarTitulosValores(txtDescripcion.Text, lstr_FchInicio, lstr_FchFin).Tables[0];

                if (ddlTipoNegociacion.SelectedValue != "0" && ddlNemotecnico.SelectedValue != "0")
                {
                    ldar_Temp = ldat_Cancelaciones.Select("TipoNegociacion = '" + ddlTipoNegociacion.SelectedValue + "' and Nemotecnico = '" + ddlNemotecnico.SelectedValue + "'");
                    ldat_Temp = ldar_Temp.CopyToDataTable();
                    lbol_filtrado = true;
                }
                else
                {
                    if (ddlTipoNegociacion.SelectedValue != "0" && ddlNemotecnico.SelectedValue == "0")
                    {
                        ldar_Temp = ldat_Cancelaciones.Select("TipoNegociacion = '" + ddlTipoNegociacion.SelectedValue + "'");
                        ldat_Temp = ldar_Temp.CopyToDataTable();
                        lbol_filtrado = true;
                    }
                    else
                    {
                        if (ddlTipoNegociacion.SelectedValue == "0" && ddlNemotecnico.SelectedValue != "0")
                        {
                            ldar_Temp = ldat_Cancelaciones.Select("Nemotecnico = '" + ddlNemotecnico.SelectedValue + "'");
                            ldat_Temp = ldar_Temp.CopyToDataTable();
                            lbol_filtrado = true;
                        }
                    }

                }

                grvCancelaciones.Dispose();

                if (lbol_filtrado)
                {
                    ldat_Cancelaciones = ldat_Temp;
                }


                //Código para agregar totales:
                decimal totalValorFacial = 0;
                decimal totalTransadoBruto = 0;
                decimal totalTransadoNeto = 0;
                decimal totalDescuentoPrima = 0;

                for (int i = 0; i < ldat_Cancelaciones.Rows.Count; i++)
                {
                    totalValorFacial += Convert.ToDecimal(ldat_Cancelaciones.Rows[i]["ValorFacial"].ToString());
                    totalTransadoBruto += Convert.ToDecimal(ldat_Cancelaciones.Rows[i]["ValorTransadoBruto"].ToString());
                    totalTransadoNeto += Convert.ToDecimal(ldat_Cancelaciones.Rows[i]["ValorTransadoNeto"].ToString());
                    totalDescuentoPrima += Convert.ToDecimal(ldat_Cancelaciones.Rows[i]["RendimientoPorDescuento"].ToString());
                }

                lblFacial.Text = totalValorFacial.ToString("N2");
                lblBruto.Text = totalTransadoBruto.ToString("N2");
                lblNeto.Text = totalTransadoNeto.ToString("N2");
                lblDescuento.Text = totalDescuentoPrima.ToString("N2");


                grvCancelaciones.DataSource = ldat_Cancelaciones;
                grvCancelaciones.DataBind();
            }
            catch
            {
                grvCancelaciones.Dispose();
                grvCancelaciones.DataBind();
            }
        }

        protected void OcultaCampos(bool lchk_control, int lint_columna)
        {
            if (lchk_control)
            {
                grvCancelaciones.Columns[lint_columna].Visible = true;
            }
            else
            {
                grvCancelaciones.Columns[lint_columna].Visible = false;
            }
            RealizarConsulta();
        }

        protected void btnBusqueda_Click(object sender, EventArgs e)
        {

        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {

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
            CargarNemotecnicos(ddlTipoValor.SelectedValue);
            //ddlNemotecnico.Items.Insert(0, (new ListItem("-- Seleccione Opción --", "0")));
        }

        protected void ddlNemotecnico_SelectedIndexChanged(object sender, EventArgs e)
        {
            RealizarConsulta();
        }

        protected void ddlTipoNegociacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            RealizarConsulta();
        }

        protected void txtFchDesde_TextChanged(object sender, EventArgs e)
        {
            RealizarConsulta();
        }

        protected void txtFchHasta_TextChanged(object sender, EventArgs e)
        {
            RealizarConsulta();
        }

        protected void chkNroValor_CheckedChanged(object sender, EventArgs e)
        {
            OcultaCampos(chkNroValor.Checked, 2);
        }

        protected void chkValorFacial_CheckedChanged(object sender, EventArgs e)
        {
            OcultaCampos(chkValorFacial.Checked, 3);
        }

        protected void chkFchValor_CheckedChanged(object sender, EventArgs e)
        {
            OcultaCampos(chkFchValor.Checked, 4);
        }

        protected void chkFchColocacion_CheckedChanged(object sender, EventArgs e)
        {
            OcultaCampos(chkFchColocacion.Checked, 5);
        }

        protected void chkFchCancelacion_CheckedChanged(object sender, EventArgs e)
        {
            OcultaCampos(chkFchCancelacion.Checked, 6);
        }

        protected void chkNemotecnico_CheckedChanged(object sender, EventArgs e)
        {
            OcultaCampos(chkNemotecnico.Checked, 8);
        }

        protected void chkMoneda_CheckedChanged(object sender, EventArgs e)
        {
            OcultaCampos(chkMoneda.Checked, 9);
        }

        protected void chkPropiedad_CheckedChanged(object sender, EventArgs e)
        {
            OcultaCampos(chkPropiedad.Checked, 10);
        }

        protected void chkDescuento_CheckedChanged(object sender, EventArgs e)
        {
            OcultaCampos(chkDescuento.Checked, 11);
        }

        protected void chkPremio_CheckedChanged(object sender, EventArgs e)
        {
            OcultaCampos(chkPremio.Checked, 12);
        }

        protected void chkTMargen_CheckedChanged(object sender, EventArgs e)
        {
            OcultaCampos(chkMargen.Checked, 13);
        }

        protected void chkTransadoBruto_CheckedChanged(object sender, EventArgs e)
        {
            OcultaCampos(chkTransadoBruto.Checked, 14);
        }

        protected void chkTransadoNeto_CheckedChanged(object sender, EventArgs e)
        {
            OcultaCampos(chkTransadoNeto.Checked, 15);
        }

        protected void chkTasaBruta_CheckedChanged(object sender, EventArgs e)
        {
            OcultaCampos(chkTasaBruta.Checked, 16);
        }

        protected void chkTasaNeta_CheckedChanged(object sender, EventArgs e)
        {
            OcultaCampos(chkTasaNeta.Checked, 17);
        }

        protected void grvCancelaciones_Sorting(object sender, System.Web.UI.WebControls.GridViewSortEventArgs e)
        {
            RealizarConsulta();
            ldat_Cancelaciones.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
            grvCancelaciones.DataSource = ldat_Cancelaciones;
            grvCancelaciones.DataBind();
        }

        protected void grvCancelaciones_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {

        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }
        protected void btnExcel_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=ReporteCancelaciones.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                grvCancelaciones.AllowPaging = false;
                //this.BindGrid();

                grvCancelaciones.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in grvCancelaciones.HeaderRow.Cells)
                {
                    cell.BackColor = grvCancelaciones.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in grvCancelaciones.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = grvCancelaciones.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = grvCancelaciones.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                grvCancelaciones.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }
    }
}