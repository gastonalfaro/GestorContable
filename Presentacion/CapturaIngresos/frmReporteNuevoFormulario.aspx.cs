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

using Presentacion.Compartidas.VisorReportes;
using System.Diagnostics;
using System.Reflection;

namespace Presentacion.CapturaIngresos
{
    public partial class frmReporteNuevoFormulario : BASE
    {
        # region Variables
        private Presentacion.wsDeudaInterna.wsDeudaInterna wsDeudaInterna = new Presentacion.wsDeudaInterna.wsDeudaInterna();
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private string gstr_Usuario = String.Empty;
      //private static DataTable ldat_Cancelaciones = new DataTable();
        private string gstr_ModuloActual = String.Empty;
        private string gstr_IdFormulario;
        private string gstr_AnnoFormulario;
        private string gstr_LetrasColones;
        # endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                gstr_Usuario = clsSesion.Current.LoginUsuario;
                gstr_IdFormulario = clsSesion.Current.IdFormularioCI;
                gstr_AnnoFormulario = clsSesion.Current.AnnoFormularioCI;
                gstr_LetrasColones = clsSesion.Current.Letras;
                /*#region RAMSES COMENTAR
                this.mtr_msg(String.Format("Valor de gstr_Usuario >> " + gstr_Usuario));
                this.mtr_msg(String.Format("Valor de gstr_AnnoFormulario >> " + gstr_AnnoFormulario));
                this.mtr_msg(String.Format("Valor de gstr_LetrasColones >> " + gstr_LetrasColones));
                #endregion*/
                GenerarReporte();
                if (!IsPostBack)
                {
                   
                    if (!string.IsNullOrEmpty(gstr_Usuario))
                    {
                        //if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmReporteNuevoFormulario"))
                        //    Response.Redirect("~/Principal.aspx", true);
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

        private void GenerarReporte()
        {
            try
            {

                PanelReporte.Visible = true;
                //string  ldt_FechaInicio, ldt_FechaFin;
                string strUsuario = clsSesion.Current.LoginUsuario;
                //this.mtr_msg(String.Format("Valor de strUsuario >> " + strUsuario));
                List<ReportParameter> oParametros = new List<ReportParameter>();
                if (!strUsuario.Equals(string.Empty))// && (!str_Institucion.Equals(string.Empty)))
                {
                    oParametros.Add(new ReportParameter("pIdFormulario", gstr_IdFormulario, false));
                    oParametros.Add(new ReportParameter("pAnno", gstr_AnnoFormulario, false));
                    oParametros.Add(new ReportParameter("pLetrasColones", gstr_LetrasColones, false));
                    oParametros.Add(new ReportParameter("pMensaje", string.Empty, false));
                    oParametros.Add(new ReportParameter("pResultado", string.Empty, false));

                    //mtr_msg("Se enviarán los parametros !!!");
                    ParametrosReporte oParam = new ParametrosReporte(ConfigurationManager.AppSettings["ReporteNuevoFormulario"], oParametros, ConfigurationManager.AppSettings["ServidorReportes"]);

                    //mtr_msg("Se enviarón los parametros Correctamente !!!");
                    Session.Add("ParametrosReporte", oParam);
                }
                /*#region RAMSES COMENTAR
                else
                {
                    mtr_msg("El valor para strUsuario es igual a >> NULL");
                }
                #endregion*/

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
                /*#region RAMSES COMENTAR
                this.mtr_msg("ERROR >> " + ex.Message);
                #endregion*/
            }
            finally
            {

            }
        }

        protected void mtr_msg(String msg)
        {
            Response.Write(String.Format("<script>alert('{0}')</script>", msg));
        }//FUNCION
     

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

    }
}