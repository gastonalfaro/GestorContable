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
    public partial class frmReporteHistorialTituloValor : BASE
    {
        # region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private Presentacion.wsDeudaInterna.wsDeudaInterna wsDeudaInterna = new Presentacion.wsDeudaInterna.wsDeudaInterna();
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
                        if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmReporteHistorialTituloValor"))
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

 
        protected void btnBusqueda_Click(object sender, EventArgs e)
        {

        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                PanelReporte.Visible = true;
                string lstr_Nemotecnico, lstr_NroValor, lstr_Tipo, lstr_TipoNegociacion;
                DateTime ldt_FechaInicio, ldt_FechaFin;
                string lstr_FechaInicio, lstr_FechaFin;
                string strUsuario = clsSesion.Current.LoginUsuario;

                //strUsuario = User.Identity.Name;
                //strUsuario = gstr_Usuario;
                //strIdEntidad = CatalogoEntidadesDropDownList.SelectedValue;
                //FechaInicio = FechaInicioCalendarPopup.SelectedDate;
                //FechaFin = FechaFinCalendarPopup.SelectedDate;
                //strPeriodo = PeriodoDropDownList.SelectedItem.Text;
                //strUnidadTiempoPeriodo = UnidadTiempoPeriodoDropDownList.SelectedValue;

                if (ddlNemotecnico.SelectedValue == "" || ddlNemotecnico.SelectedValue == "-- Seleccione Valor --")
                {
                    lstr_Nemotecnico = String.Empty;
                }
                else
                {
                    lstr_Nemotecnico = ddlNemotecnico.SelectedValue;
                }


                if (ddlNumValor.SelectedValue == "" || ddlNumValor.SelectedValue == "-- Seleccione Valor --")
                {
                    lstr_NroValor = String.Empty;
                }
                else
                {
                    lstr_NroValor = ddlNumValor.SelectedValue;
                }
               


            
                List<ReportParameter> oParametros = new List<ReportParameter>();
                if (!strUsuario.Equals(string.Empty))// && (!str_Institucion.Equals(string.Empty)))
                {
                    oParametros.Add(new ReportParameter("pNemotecnico", lstr_Nemotecnico.Trim(), false));
                    oParametros.Add(new ReportParameter("pNroValor", lstr_NroValor, false));

                    ParametrosReporte oParam = new ParametrosReporte(ConfigurationManager.AppSettings["ReporteHistorialTitulo"], oParametros, ConfigurationManager.AppSettings["ServidorReportes"]);

                
                    Session.Add("ParametrosReporte", oParam);
               
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

        protected void ddlNemotecnico_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlNumValor.Items.Clear();
            //this.ddlNumValor.DataSource = wsDeudaInterna.ConsultarTitulosValores(string.Empty, this.ddlNemotecnico.SelectedValue, string.Empty, string.Empty, "V", string.Empty, string.Empty, string.Empty, "01/01/1900", "01/01/5000");
            DataSet ds_Dinamico = new DataSet();
            ds_Dinamico = ws_SGService.uwsConsultarDinamico("select distinct(NroValor) from cf.titulosvalores where Nemotecnico = '" + this.ddlNemotecnico.SelectedValue.Trim() + "' and indicadorcupon = 'V' order by nrovalor asc");
            this.ddlNumValor.DataTextField = "NroValor";
            this.ddlNumValor.DataValueField = "NroValor";
            if (ds_Dinamico.Tables.Count > 0 && ds_Dinamico.Tables[0].Rows.Count > 0) 
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