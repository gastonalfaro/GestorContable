using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Diagnostics;
using System.Configuration;
using System.Reflection;

using System.Xml.Linq;
using System.Net;
using Microsoft.Reporting.WebForms;
using LogicaNegocio.Seguridad;
using System.Security.Principal;

namespace Presentacion.Compartidas.VisorReportes
{
    public partial class VisorReportes : System.Web.UI.Page//: BASE
    {
        tSeguridad gstr_Seguridad = new tSeguridad();
        private tSeguridad Seguridad = new tSeguridad();

        protected override void OnError(EventArgs e)
        {
            if (Context.Error != null)
            {
                System.Exception vExcepcion = (Context.Error is System.Web.HttpUnhandledException) ? Context.Error.InnerException : Context.Error;

                if (clsSesion.Current.LoginUsuario.Equals(string.Empty))
                    Seguridad.SaveError(vExcepcion);
                else
                    Seguridad.SaveError(new System.Exception(clsSesion.Current.NomUsuario.ToString() + Environment.NewLine, vExcepcion));
            }
            //
            //fin
            this.Response.Redirect("/Errores.aspx");
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            //{
                if (!Page.IsPostBack)
                {
                    if (Session["ParametrosReporte"] != null)
                    {
                        ParametrosReporte oParam = (ParametrosReporte)Session["ParametrosReporte"];

                        ReportViewerCredentials rpCredentials = new ReportViewerCredentials(ConfigurationManager.AppSettings["ReportUser"], gstr_Seguridad.DescifrarTextoAES(ConfigurationManager.AppSettings["ReportPassword"]), ConfigurationManager.AppSettings["ReportDomain"]);
                        rVisor.ServerReport.ReportServerCredentials = rpCredentials;
                        rVisor.ServerReport.ReportServerUrl = new Uri(oParam._ServidorReportes /*ConfigurationManager.AppSettings["ReportServerUrl"].ToString()*/);
                        rVisor.ServerReport.ReportPath = oParam._DireccionReporte;//ConfigurationManager.AppSettings["ReportPath"].ToString() + ConfigurationManager.AppSettings["ReportName"].ToString();
                        //Microsoft.Reporting.WebForms.ReportParameter[] RptParameters = new Microsoft.Reporting.WebForms.ReportParameter[1];

                        //RptParameters[0] = new Microsoft.Reporting.WebForms.ReportParameter("PARAM_NAME", "PARAM_VALUE");

                        this.rVisor.ServerReport.SetParameters(oParam._oParametros);

                        this.rVisor.ServerReport.Refresh();
                    }
                }
            /*}
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
                #endregion
            }
            finally
            {

            }*/
        }

        protected void rVisor_Init(object sender, EventArgs e)
        {
            //ReportViewerCredentials rvwCreds = new ReportViewerCredentials(
            //                            ConfigurationManager.AppSettings["strUsuario"],
            //                        ConfigurationManager.AppSettings["strPassword"],
            //                        ConfigurationManager.AppSettings["strDominio"]);
            //rvwCreds.                
            //rVisor.ServerReport.ReportServerCredentials = rvwCreds;

            //rVisor.ServerReport.ReportServerCredentials = new ReportViewerServerCredentials();
        }
    }
}