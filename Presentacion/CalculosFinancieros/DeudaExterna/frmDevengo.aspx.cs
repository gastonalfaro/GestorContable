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
using Microsoft.Reporting.WebForms;
using eWorld.UI;
using Presentacion.Compartidas;
using Presentacion.Compartidas.VisorReportes;
using System.Diagnostics;
using System.Reflection;

namespace Presentacion.CalculosFinancieros.DeudaExterna
{
    public partial class frmDevengo : BASE
    {
        # region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private Presentacion.wsDeudaExterna.wsDeudaExterna ws_DE = new Presentacion.wsDeudaExterna.wsDeudaExterna();
        private string gstr_Usuario = String.Empty;
        //private static DataTable ldat_Cancelaciones = new DataTable();
        protected DataTable ldat_Cancelaciones
        {
            get
            {
                if (ViewState["ldat_Cancelaciones"] == null)
                    ViewState["ldat_Cancelaciones"] = new DataTable();
                return (DataTable)ViewState["ldat_Cancelaciones"];
            }
            set
            {
                ViewState["ldat_Cancelaciones"] = value;
            }
        }
        private string gstr_ModuloActual = String.Empty;
        private char gchr_MensajeExito;
        # endregion

        protected void Page_Load(object sender, EventArgs e)
        {

            gchr_MensajeExito = clsSesion.Current.chr_MensajeExito;

            try
            {
                gstr_Usuario = clsSesion.Current.LoginUsuario;

                if (!IsPostBack)
                {
                    if (!string.IsNullOrEmpty(gstr_Usuario))
                    {
                        CargarDDLs();
                        if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmDevengo"))
                            Response.Redirect("~/Principal.aspx", true);
                        //else
                        //    PanelReporte.Visible = false;
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
        /// <summary>
        /// Llena los dropdownlisto con la informacion respectiva
        /// </summary>
        private void CargarDDLs()
        {
            DataSet lds_Prestamos = ws_SGService.uwsConsultarDinamico("SELECT distinct([IdPrestamo]) FROM [cf].[Prestamos] order by 1");
            if (lds_Prestamos.Tables.Count > 0)
            {
                DataTable ldt_Prestamos = lds_Prestamos.Tables["Table"];
                ddlNroPrestamo.DataSource = ldt_Prestamos;
                ddlNroPrestamo.DataTextField = "IdPrestamo";
                ddlNroPrestamo.DataValueField = "IdPrestamo";
                ddlNroPrestamo.DataBind();
                ddlNroPrestamo.Items.Insert(0, new ListItem("-Todos-", ""));
            }
            //DataSet lds_Tramoes = ws_SGService.uwsConsultarDinamico("select distinct IdTramo from cf.vCalculosDevengo where IdPrestamo = '" + ddlNroPrestamo.SelectedValue + "'  order by 1");
            //if (lds_Tramoes.Tables.Count > 0)
            //{
            //    DataTable ldt_Tramoes = lds_Tramoes.Tables["Table"];
            //    ddlNroTramo.DataSource = ldt_Tramoes;
            //    ddlNroTramo.DataTextField = "IdTramo";
            //    ddlNroTramo.DataValueField = "IdTramo";
            //    ddlNroTramo.DataBind();
            //    ddlNroTramo.Items.Insert(0, new ListItem("-Todos-", ""));
            //}


        }

        /// <summary>
        /// Llena los dropdownlisto con la informacion respectiva
        /// </summary>
        private void CargarTramos()
        {
            DataSet lds_Tramoes = ws_SGService.uwsConsultarDinamico("select distinct IdTramo from cf.vCalculosDevengo where IdPrestamo = '" + ddlNroPrestamo.SelectedValue + "'  order by 1");
            if (lds_Tramoes.Tables.Count > 0)
            {
                DataTable ldt_Tramoes = lds_Tramoes.Tables["Table"];
                ddlNroTramo.DataSource = ldt_Tramoes;
                ddlNroTramo.DataTextField = "IdTramo";
                ddlNroTramo.DataValueField = "IdTramo";
                ddlNroTramo.DataBind();
                ddlNroTramo.Items.Insert(0, new ListItem("-Todos-", ""));
            }


        }
        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            //DataSet ds_Devengo = new DataSet();
            //ds_Devengo = ws_SGService.CalculaDevengoDE(ddlNroPrestamo.SelectedValue, ddlNroTramo.SelectedValue, "");
            try
            {

                string res = "";
                res = ws_DE.Reclasificar(txtFchFin.Text);
                MessageBox.Show(res);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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
            //ddlNroTramo.Items.Clear();
            //ddlNroTramo.DataBind();
            //ddlNroTramo.Items.Insert(0, (new ListItem("-- Seleccione Opción --", "")));
            CargarTramos();
        }

        //protected void ddlNemotecnico_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    RealizarConsulta();
        //}

        //protected void ddlTipoNegociacion_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    RealizarConsulta();
        //}

        //protected void txtFchDesde_TextChanged(object sender, EventArgs e)
        //{
        //    RealizarConsulta();
        //}

        //protected void txtFchHasta_TextChanged(object sender, EventArgs e)
        //{
        //    RealizarConsulta();
        //}

        //protected void chkNroValor_CheckedChanged(object sender, EventArgs e)
        //{
        //    OcultaCampos(chkNroValor.Checked, 2);
        //}

        //protected void chkValorFacial_CheckedChanged(object sender, EventArgs e)
        //{
        //    OcultaCampos(chkValorFacial.Checked, 3);
        //}

        //protected void chkFchValor_CheckedChanged(object sender, EventArgs e)
        //{
        //    OcultaCampos(chkFchValor.Checked, 4);
        //}

        //protected void chkFchColocacion_CheckedChanged(object sender, EventArgs e)
        //{
        //    OcultaCampos(chkFchColocacion.Checked, 5);
        //}

        //protected void chkFchCancelacion_CheckedChanged(object sender, EventArgs e)
        //{
        //    OcultaCampos(chkFchCancelacion.Checked, 6);
        //}

        //protected void chkNemotecnico_CheckedChanged(object sender, EventArgs e)
        //{
        //    OcultaCampos(chkNemotecnico.Checked, 8);
        //}

        //protected void chkMoneda_CheckedChanged(object sender, EventArgs e)
        //{
        //    OcultaCampos(chkMoneda.Checked, 9);
        //}

        //protected void chkPropiedad_CheckedChanged(object sender, EventArgs e)
        //{
        //    OcultaCampos(chkPropiedad.Checked, 10);
        //}

        //protected void chkDescuento_CheckedChanged(object sender, EventArgs e)
        //{
        //    OcultaCampos(chkDescuento.Checked, 11);
        //}

        //protected void chkPremio_CheckedChanged(object sender, EventArgs e)
        //{
        //    OcultaCampos(chkPremio.Checked, 12);
        //}

        //protected void chkTMargen_CheckedChanged(object sender, EventArgs e)
        //{
        //    OcultaCampos(chkMargen.Checked, 13);
        //}

        //protected void chkTransadoBruto_CheckedChanged(object sender, EventArgs e)
        //{
        //    OcultaCampos(chkTransadoBruto.Checked, 14);
        //}

        //protected void chkTransadoNeto_CheckedChanged(object sender, EventArgs e)
        //{
        //    OcultaCampos(chkTransadoNeto.Checked, 15);
        //}

        //protected void chkTasaBruta_CheckedChanged(object sender, EventArgs e)
        //{
        //    OcultaCampos(chkTasaBruta.Checked, 16);
        //}

        //protected void chkTasaNeta_CheckedChanged(object sender, EventArgs e)
        //{
        //    OcultaCampos(chkTasaNeta.Checked, 17);
        //}

        //protected void grvCancelaciones_Sorting(object sender, System.Web.UI.WebControls.GridViewSortEventArgs e)
        //{
        //    RealizarConsulta();
        //    ldat_Cancelaciones.DefaultView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
        //    grvCancelaciones.DataSource = ldat_Cancelaciones;
        //    grvCancelaciones.DataBind();
        //}

        protected void grvCancelaciones_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {

        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void ddlNroTramo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlNroPrestamo_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ddlNroTramo.Items.Clear();
            //ddlNroTramo.DataBind();
            //ddlNroTramo.Items.Insert(0, (new ListItem("-- Seleccione Opción --", "")));
            CargarTramos();
        }

        protected void btnReclasificar_Click(object sender, EventArgs e)
        {
            
            try
            {

                string res = "";
                //res = ws_DE.Reclasificar(txtFchFin.Text);
                //MessageBox.Show(res);
                //DataSet ds_Devengo = new DataSet();
                res = ws_SGService.Reclasificar(ddlNroPrestamo.SelectedValue, ddlNroTramo.SelectedValue, txtFchFin.Text, '0');
                MessageBox.Show(res);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        protected void btnDevengo_Click(object sender, EventArgs e)
        {
            //DataSet ds_Devengo = new DataSet();
            //ds_Devengo = ws_SGService.CalculaDevengoDE(ddlNroPrestamo.SelectedValue, ddlNroTramo.SelectedValue, "");
            try
            {

                string res = "";
                //res = ws_DE.Reclasificar(txtFchFin.Text);
                //MessageBox.Show(res);
              //  DataSet ds_Devengo = new DataSet();
                res = ws_SGService.CalculaDevengoDE(ddlNroPrestamo.SelectedValue, ddlNroTramo.SelectedValue, txtFchFin.Text, '1');
               MessageBox.Show(res);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        protected void btnDiferencialCambiario_Click(object sender, EventArgs e)
        {
            try
            {

                string res = "";
                //res = ws_DE.Reclasificar(txtFchFin.Text);
                //MessageBox.Show(res);
                //  DataSet ds_Devengo = new DataSet();
                res = ws_SGService.DiferencialCambiario(ddlNroPrestamo.SelectedValue, ddlNroTramo.SelectedValue , txtFchFin.Text);
                MessageBox.Show(res);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
           
            

        }

        protected void btnContaDevengo_Click(object sender, EventArgs e)
        {
            //DataSet ds_Devengo = new DataSet();
            //ds_Devengo = ws_SGService.CalculaDevengoDE(ddlNroPrestamo.SelectedValue, ddlNroTramo.SelectedValue, "");
            try
            {

                string res = "";
                //res = ws_DE.Reclasificar(txtFchFin.Text);
                //MessageBox.Show(res);
                //  DataSet ds_Devengo = new DataSet();
                res = ws_SGService.ContabilizaDevengoDE(ddlNroPrestamo.SelectedValue, ddlNroTramo.SelectedValue, txtFchFin.Text);
                MessageBox.Show(res);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        protected void btnReversionDevengo_Click(object sender, EventArgs e)
        {
            try
            {

                string res = "";
                //res = ws_DE.Reclasificar(txtFchFin.Text);
                //MessageBox.Show(res);
                //  DataSet ds_Devengo = new DataSet();
                res = ws_SGService.ReversaDevengoDE(ddlNroPrestamo.SelectedValue, ddlNroTramo.SelectedValue, txtFchFin.Text);
                MessageBox.Show(res);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        protected void btnDTSSIGADE_Click(object sender, EventArgs e)
        {

            //wsDI.wsDeudaInterna wsDInterna = new wsDI.wsDeudaInterna();
            string msj = "";

            msj = this.ws_DE.EjecutarDTSSIGADE("", "");


            string script = @"<script type='text/javascript'> alert('" + msj + "'); </script>";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false);
        }

  /*      private void MostarMensaje(string str_TextMensaje, char chr_TipoMensaje)
        {
            if (chr_TipoMensaje.Equals('1'))
            {
                this.lblMensaje.Text = str_TextMensaje;
                this.lblMensaje.ForeColor = System.Drawing.Color.DarkRed;
                this.lblMensaje.Visible = true;
            }
            else
            {
                this.lblMensaje.Text = str_TextMensaje;
                this.lblMensaje.ForeColor = System.Drawing.Color.DarkGreen;
                this.lblMensaje.Visible = true;
            }

        }*/

      

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