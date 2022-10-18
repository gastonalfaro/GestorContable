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
    public partial class frmReporteSubastaEmision : BASE
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
                    CargarNumeroSerie();
                    if (!string.IsNullOrEmpty(gstr_Usuario))
                    {
                        if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmReporteSubastaEmision"))
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

        private void CargarNumeroSerie()
        {

            this.ddlNumSerie.DataSource = ws_SGService.uwsConsultarDinamico("SELECT DISTINCT([NroEmisionSerie]) FROM [cf].[TitulosValores] WHERE [IndicadorCupon] = 'V' AND [TipoNegociacion] = 'Compra' and DescripcionNegociacion in ('Canje/Inversa/Precio','Canje/Inversa/Rend')");
            this.ddlNumSerie.DataTextField = "NroEmisionSerie";
            this.ddlNumSerie.DataValueField = "NroEmisionSerie";
            this.ddlNumSerie.DataBind();
        }


        private void CargarFechaCanje()
        {
            this.ddlFchCanje.Items.Clear();
            this.ddlFchCanje.DataSource = ws_SGService.uwsConsultarDinamico("select convert(varchar(10),fchvalor,103) as FchCanje, convert(varchar(10),fchvalor) as fchvalor from cf.titulosvalores where nroemisionserie='" + ddlNumSerie.SelectedValue + "' and tipoNegociacion='Compra'");
            this.ddlFchCanje.DataTextField = "FchCanje";
            this.ddlFchCanje.DataValueField = "fchvalor";
            this.ddlFchCanje.DataBind();
        }


        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                PanelReporte.Visible = true;
                string lstr_NroEmisionSerie;
                string lstr_FchCanje;
                //string  ldt_FechaInicio, ldt_FechaFin;
                string strUsuario = clsSesion.Current.LoginUsuario;

                lstr_NroEmisionSerie = ddlNumSerie.SelectedValue;
                lstr_FchCanje = ddlFchCanje.SelectedItem.Value;
       
        
                List<ReportParameter> oParametros = new List<ReportParameter>();
                if (!strUsuario.Equals(string.Empty))// && (!str_Institucion.Equals(string.Empty)))
                {
                    oParametros.Add(new ReportParameter("pNroEmisionSerie", lstr_NroEmisionSerie, false));
                    oParametros.Add(new ReportParameter("pFchCanje", lstr_FchCanje, false));

                    ParametrosReporte oParam = new ParametrosReporte(ConfigurationManager.AppSettings["ReporteSubastaEmision"], oParametros, ConfigurationManager.AppSettings["ServidorReportes"]);

                  
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

        protected void ddlNumSerie_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarFechaCanje();
        }

    }
}