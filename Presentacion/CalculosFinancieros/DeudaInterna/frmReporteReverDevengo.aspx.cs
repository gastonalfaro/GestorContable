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

namespace Presentacion.CalculosFinancieros.DeudaInterna
{
    public partial class frmReporteReverDevengo : BASE
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
                    CargarCuentas();
                    this.txtFchInicio.Text = "01/01/1990";
                    this.txtFchFin.Text = DateTime.Now.ToString("dd/MM/yyyy");

                    if (!string.IsNullOrEmpty(gstr_Usuario))
                    {
                        if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmReporteReverDevengo"))
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
            this.ddlNemotecnico.DataTextField =
            this.ddlNemotecnico.DataValueField = "IdNemotecnico";
            this.ddlNemotecnico.DataBind();
        }

        private void CargarCuentas()
        {
            DataSet lCuentas = ws_SGService.uwsConsultarCuentasContables("","OPER","","","","","","");
            this.ddlCuentas.DataSource = lCuentas;
            this.ddlCuentas.DataTextField =
            this.ddlCuentas.DataValueField = "IdCuentaContable";
            this.ddlCuentas.DataBind();
        }

        protected void btnBusqueda_Click(object sender, EventArgs e)
        {

        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                PanelReporte.Visible = true;
                string lstr_Nemotecnico, lstr_Tipo, lstr_Moneda, lstr_Cuenta = string.Empty;
                int lint_NroValor= -1;
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
                lstr_Nemotecnico = ddlNemotecnico.SelectedValue.Trim();
                lstr_Nemotecnico = string.IsNullOrEmpty(lstr_Nemotecnico) ? string.Empty : lstr_Nemotecnico;

                lint_NroValor = string.IsNullOrEmpty(ddlNroValor.SelectedValue) ? -1 : Convert.ToInt32(ddlNroValor.SelectedValue);
                lstr_Tipo = ddlTipo.SelectedValue;
                lstr_Tipo = string.IsNullOrEmpty(lstr_Tipo) ? string.Empty : lstr_Tipo;
                lstr_Moneda = ddlMoneda.SelectedValue;
                lstr_Moneda = string.IsNullOrEmpty(lstr_Moneda) ? string.Empty : lstr_Moneda;
                lstr_Cuenta = ddlCuentas.SelectedValue;
                lstr_Cuenta = string.IsNullOrEmpty(lstr_Cuenta) ? string.Empty : lstr_Cuenta;
                lstr_FechaInicio = txtFchInicio.Text;
                lstr_FechaFin = txtFchFin.Text;

                ldt_FechaInicio = Convert.ToDateTime(lstr_FechaInicio);
                ldt_FechaFin = Convert.ToDateTime(lstr_FechaFin);

                lstr_FechaInicio = String.Format("{0:yyyy}", ldt_FechaInicio) + '/' + String.Format("{0:MM}", ldt_FechaInicio) + '/' + String.Format("{0:dd}", ldt_FechaInicio) + ' ' +
                    String.Format("{0:HH}", ldt_FechaInicio) + ':' +
                    String.Format("{0:mm}", ldt_FechaInicio) + ':' +
                    String.Format("{0:ss}", ldt_FechaInicio) + '.' +
                    String.Format("{0:fff}", ldt_FechaInicio);

                lstr_FechaFin = String.Format("{0:yyyy}", ldt_FechaFin) + '/' + String.Format("{0:MM}", ldt_FechaFin) + '/' + String.Format("{0:dd}", ldt_FechaFin) + ' ' +
                    String.Format("{0:HH}", ldt_FechaFin) + ':' +
                    String.Format("{0:mm}", ldt_FechaFin) + ':' +
                    String.Format("{0:ss}", ldt_FechaFin) + '.' +
                    String.Format("{0:fff}", ldt_FechaFin);

                //ParametrosReporte oParam = new ParametrosReporte();
                List<ReportParameter> oParametros = new List<ReportParameter>();
                if (!strUsuario.Equals(string.Empty))// && (!str_Institucion.Equals(string.Empty)))
                {
                    oParametros.Add(new ReportParameter("pNroValor", lint_NroValor.ToString() , false));
                    oParametros.Add(new ReportParameter("pNemotecnico", lstr_Nemotecnico, false));
                    oParametros.Add(new ReportParameter("pTipo", lstr_Tipo, false));
                    oParametros.Add(new ReportParameter("pMoneda", lstr_Moneda, false));
                    oParametros.Add(new ReportParameter("pCuenta", lstr_Cuenta, false));
                    oParametros.Add(new ReportParameter("pFchInicio", lstr_FechaInicio, false));
                    oParametros.Add(new ReportParameter("pFchFin", lstr_FechaFin, false));
                    oParametros.Add(new ReportParameter("pMensaje", string.Empty, false));
                    oParametros.Add(new ReportParameter("pResultado", string.Empty, false));

                    ParametrosReporte oParam = new ParametrosReporte(ConfigurationManager.AppSettings["ReporteReversionDevengo"], oParametros, ConfigurationManager.AppSettings["ServidorReportes"]);

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

            ddlNemotecnico.Dispose();
            ddlNemotecnico.Items.Clear();
            ddlNemotecnico.DataBind();
            ddlNemotecnico.Items.Insert(0, (new ListItem("-- Seleccione Opción --", "")));
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
            DataSet lNroValores = new DataSet();
            lNroValores = ws_SGService.uwsConsultarDinamico("select distinct(NroValor) from cf.titulosvalores where Nemotecnico = '" + ddlNemotecnico.SelectedValue + "' order by NroValor Asc");
            this.ddlNroValor.Dispose();
            this.ddlNroValor.DataTextField = 
            this.ddlNroValor.DataValueField = "NroValor";
            {
                this.ddlNroValor.DataSource = lNroValores; 
                this.ddlNroValor.DataBind();
            }//if
            ddlNroValor.Items.Insert(0, (new ListItem("-- Seleccione Opción --", "")));
        }
    }
}