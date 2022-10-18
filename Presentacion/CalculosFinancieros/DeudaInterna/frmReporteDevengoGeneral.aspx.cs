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
    public partial class frmReporteDevengoGeneral : BASE
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
                    CargarDDLs();
                    this.txtFchInicio.Text = "01/01/1990";
                    this.txtFchFin.Text = DateTime.Now.ToString("dd/MM/yyyy");

                    if (!string.IsNullOrEmpty(gstr_Usuario))
                    {
                        if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmReporteDevenGeneral"))
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

        private void CargarDDLs()
        {
            DataSet lNemotecnicos = ws_SGService.uwsConsultarNemotecnicos("", "", "", "", "");
            this.ddlNemotecnico.DataSource = lNemotecnicos;
            this.ddlNemotecnico.DataTextField = "IdNemotecnico";
            this.ddlNemotecnico.DataValueField = "IdNemotecnico";
            this.ddlNemotecnico.DataBind();

            DataSet lds_TipoValor = ws_SGService.uwsConsultarDinamico("SELECT [ValOpcion], [NomOpcion] FROM [cf].[vTiposValores]");
            if (lds_TipoValor.Tables.Count > 0)
            {
                DataTable ldt_TipoValor = lds_TipoValor.Tables["Table"];
                ddlTipo.DataSource = ldt_TipoValor;
                ddlTipo.DataTextField = "NomOpcion";
                ddlTipo.DataValueField = "ValOpcion";
                ddlTipo.DataBind();
                ddlTipo.Items.Insert(0, new ListItem("-Seleccione-", ""));
            }
            //DataSet lds_TipoNegociacion = ws_SGService.uwsConsultarDinamico("SELECT [NomOpcion], [ValOpcion] FROM [cf].[vTiposNegociacion]");
            //if (lds_TipoNegociacion.Tables.Count > 0)
            //{
            //    DataTable ldt_TipoNegociacion = lds_TipoNegociacion.Tables["Table"];
            //    ddlTipoNegociacion.DataSource = ldt_TipoNegociacion;
            //    ddlTipoNegociacion.DataTextField = "NomOpcion";
            //    ddlTipoNegociacion.DataValueField = "ValOpcion";
            //    ddlTipoNegociacion.DataBind();
            //    ddlTipoNegociacion.Items.Insert(0, new ListItem("-Seleccione-", ""));
            //}
            DataSet lds_Monedas = ws_SGService.uwsConsultarDinamico("SELECT [IdMoneda], [NomMoneda] FROM [ma].[Monedas] WHERE [IdMoneda] IN ('USD','CRC','UDE','EUR')  order by [NomMoneda]");
            if (lds_Monedas.Tables.Count > 0)
            {
                DataTable ldt_Monedas = lds_Monedas.Tables["Table"];
                ddlMoneda.DataSource = ldt_Monedas;
                ddlMoneda.DataTextField = "NomMoneda";
                ddlMoneda.DataValueField = "IdMoneda";
                ddlMoneda.DataBind();
                ddlMoneda.Items.Insert(0, new ListItem("-Seleccione-", ""));
            }
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                PanelReporte.Visible = true;
                string lstr_Nemotecnico, lstr_NroValor;
                DateTime ldt_FechaInicio, ldt_FechaFin;
                string strUsuario = clsSesion.Current.LoginUsuario;
                string strFechaInicio, strFechaFin;



                if (ddlNemotecnico.Text == "" || ddlNemotecnico.Text == "-- Seleccione Opción --" || ddlNemotecnico.Text == "0")
                {
                    lstr_Nemotecnico = String.Empty;
                }else
                {
                    lstr_Nemotecnico = ddlNemotecnico.SelectedValue;
                }

                if (ddlNumValor.Text == "" || ddlNumValor.Text == "-- Seleccione Opción --" || ddlNumValor.Text == "0")
                {
                    lstr_NroValor = String.Empty;
                }
                else
                {
                    lstr_NroValor = ddlNumValor.SelectedValue;
                }
              
                //lstr_Tipo = ddlTipoValor.SelectedValue;
                //lstr_TipoNegociacion = ddlTipoNegociacion.SelectedValue;

                ldt_FechaInicio = Convert.ToDateTime(txtFchInicio.Text);
                ldt_FechaFin = Convert.ToDateTime(txtFchFin.Text);

                strFechaInicio = String.Format("{0:yyyy}", ldt_FechaInicio) + '/' + String.Format("{0:MM}", ldt_FechaInicio) + '/' + String.Format("{0:dd}", ldt_FechaInicio) + ' ' +
                    String.Format("{0:HH}", ldt_FechaInicio) + ':' +
                    String.Format("{0:mm}", ldt_FechaInicio) + ':' +
                    String.Format("{0:ss}", ldt_FechaInicio) + '.' +
                    String.Format("{0:fff}", ldt_FechaInicio);

                strFechaFin = String.Format("{0:yyyy}", ldt_FechaFin) + '/' + String.Format("{0:MM}", ldt_FechaFin) + '/' + String.Format("{0:dd}", ldt_FechaFin) + ' ' +
                    String.Format("{0:HH}", ldt_FechaFin) + ':' +
                    String.Format("{0:mm}", ldt_FechaFin) + ':' +
                    String.Format("{0:ss}", ldt_FechaFin) + '.' +
                    String.Format("{0:fff}", ldt_FechaFin);

                List<ReportParameter> oParametros = new List<ReportParameter>();
                if (!strUsuario.Equals(string.Empty))// && (!str_Institucion.Equals(string.Empty)))
                {
                    oParametros.Add(new ReportParameter("pNemotecnico", lstr_Nemotecnico.Trim(), false));
                    oParametros.Add(new ReportParameter("pNroValor", lstr_NroValor, false));
                    oParametros.Add(new ReportParameter("pTipo", this.ddlTipo.SelectedValue.Trim(), false));
                    oParametros.Add(new ReportParameter("pMoneda", this.ddlMoneda.SelectedValue.Trim(), false));
                    oParametros.Add(new ReportParameter("pFchInicio", strFechaInicio, false));
                    oParametros.Add(new ReportParameter("pFchFin", strFechaFin, false));

                    ParametrosReporte oParam = new ParametrosReporte(ConfigurationManager.AppSettings["ReporteDevengoGeneral"], oParametros, ConfigurationManager.AppSettings["ServidorReportes"]);

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
            //CargarDDLs();
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

 

        protected void grvCancelaciones_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {

        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void ddlTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlNemotecnico.Items.Clear();
            ddlNemotecnico.DataBind();
            ddlNemotecnico.Items.Insert(0, (new ListItem("-- Seleccione Opción --", "0")));
        }

        protected void ddlNemotecnico_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlNumValor.Items.Clear();
            DataTable dt_Titulos = new DataTable();
            dt_Titulos= wsDeudaInterna.ConsultarTitulosValores(string.Empty, this.ddlNemotecnico.SelectedValue.Trim(), String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "01/01/1900", "01/01/5000").Tables[0];
            this.ddlNumValor.DataTextField = "NroValor";
            this.ddlNumValor.DataValueField = "NroValor";
            if (dt_Titulos.Rows.Count > 0) 
            {
                this.ddlNumValor.DataSource = dt_Titulos;
                this.ddlNumValor.DataBind();
            }//if
            this.ddlNumValor.Items.Insert(0, (new ListItem("-- Seleccione Opción --", "0")));
        }

    }
}