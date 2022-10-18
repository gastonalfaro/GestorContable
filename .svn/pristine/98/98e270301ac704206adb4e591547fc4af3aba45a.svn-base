using Microsoft.Reporting.WebForms;
using Presentacion.Compartidas;
using Presentacion.Compartidas.VisorReportes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.RevelacionNotas
{
    public partial class ImpresionRevPendiente : BASE
    {
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private string lst_IdRevelacion = String.Empty;
        private string str_Usuario = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            str_Usuario = clsSesion.Current.LoginUsuario;
            if (String.IsNullOrEmpty(str_Usuario) || Request.QueryString["Rev"] == null)
            {
                Response.Redirect("~/Login.aspx", true);
            }
            else
            {
                if (!IsPostBack)
                {
                    ReportesContigentesMultiView.ActiveViewIndex = 0;
                    //MostrarElementos(str_Usuario);
                    if (Request.QueryString["Rev"] != null)
                    {
                        lst_IdRevelacion = Request.QueryString["Rev"];
                        GenerarReporte(lst_IdRevelacion);
                    }
                }
            }

        }

        private void GenerarReporte(string str_IdRevelacion)
        {
            string str_mensaje = string.Empty;
            string strUsuario = clsSesion.Current.LoginUsuario;
            try
            {

                //ParametrosReporte oParam = new ParametrosReporte();
                List<ReportParameter> oParametros = new List<ReportParameter>();
                ParametrosReporte oParam;
                if ((!String.IsNullOrEmpty(strUsuario)) && !String.IsNullOrEmpty(Request.QueryString["Rev"]))
                {
                    // oParametros.Add(new ReportParameter("IdUsuarioEjecutaRpt", strUsuario, false));
                    oParametros.Add(new ReportParameter("pIdRevelacionPendiente", str_IdRevelacion, false));
                    oParametros.Add(new ReportParameter("pResultado", "", false));
                    oParametros.Add(new ReportParameter("pMensaje", "", false));
                    oParam = new ParametrosReporte(ConfigurationManager.AppSettings["ReporteRevelacionPendiente"], oParametros, ConfigurationManager.AppSettings["ServidorReportes"]);
                    Session.Add("ParametrosReporte", oParam);
                    
                    ReportesContigentesMultiView.ActiveViewIndex = 0;
                    //this.ViewParametrosLinkButton.Visible = true;
                    //this.ViewReporteLinkButton.Visible = true;
                    //this.SeparadorParametrosReporteLabel.Visible = true;
                }
                else
                {
                    str_mensaje = ConfigurationManager.AppSettings["strErrorParametros"].ToString();
                    MessageBox.Show(str_mensaje);
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

                str_mensaje = "NICSP Excepcion:  " + ex.Message.ToString() + ". ";
                MessageBox.Show(str_mensaje);
                #endregion
            }
        }

        protected void btnAtras_Click(object sender, EventArgs e)
        {
            string lstr_Url = String.Format("~/RevelacionNotas/ConsultaPendiente.aspx?Rev={0}", Request.QueryString["Rev"]);
            Response.Redirect(lstr_Url, false);           
        }
    }
}