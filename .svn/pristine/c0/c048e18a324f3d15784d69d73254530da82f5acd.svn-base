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

namespace Presentacion.CapturaIngresos.Reportes
{
    public partial class frmRptPagosExpedientes : BASE
    {
        private string gstr_Usuario = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                gstr_Usuario = clsSesion.Current.LoginUsuario;

                if (!IsPostBack)
                {
                    this.txtFechaInicio.Text =
                    this.txtFechaFin.Text = DateTime.Now.ToString("dd/MM/yyyy");

                    if (!string.IsNullOrEmpty(gstr_Usuario))
                    {
                        if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "OBJ_CI"))
                            Response.Redirect("~/Principal.aspx", true);
                        else
                        {
                            
                        }
                    }
                    else
                        Response.Redirect("~/Login.aspx", true);
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
                #endregion
            }
            finally
            {

            }
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                PanelReporte.Visible = true;
                DateTime ldt_FechaInicio, ldt_FechaFin;
                string lstr_FechaInicio, lstr_FechaFin;
                string strUsuario = clsSesion.Current.LoginUsuario;

                //strUsuario = User.Identity.Name;
                //strUsuario = gstr_Usuario;
                //strIdEntidad = CatalogoEntidadesDropDownList.SelectedValue;
                //ldt_FechaInicio = FechaInicioCalendarPopup.SelectedDate;
                //ldt_FechaFin = FechaFinCalendarPopup.SelectedDate;

                ldt_FechaInicio = Convert.ToDateTime(this.txtFechaInicio.Text);
                ldt_FechaFin = Convert.ToDateTime(this.txtFechaFin.Text);

                //strPeriodo = PeriodoDropDownList.SelectedItem.Text;
                //strUnidadTiempoPeriodo = UnidadTiempoPeriodoDropDownList.SelectedValue;


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
                    oParametros.Add(new ReportParameter("pFchDesde", lstr_FechaInicio, false));
                    oParametros.Add(new ReportParameter("pFchHasta", lstr_FechaFin, false));
                    oParametros.Add(new ReportParameter("pExiste", ddlSinExpediente.SelectedValue, false));
                    string lstr_DirecccionReporte = ConfigurationManager.AppSettings["ReportePagosExpedientesCISedeJudicial"];
                    string lstr_ServidorReporte = ConfigurationManager.AppSettings["ServidorReportes"];
                    ParametrosReporte oParam = new ParametrosReporte(lstr_DirecccionReporte, oParametros, lstr_ServidorReporte);

                    //oParam.DireccionReporte = ConfigurationManager.AppSettings["ReporteBitacoraFlowError"];
                    //oParam.Parametros = oParametros;
                    //oParam.ServidorReportes = ConfigurationManager.AppSettings["ServidorReportes"];
                    Session.Add("ParametrosReporte", oParam);
                    PanelReporte.Visible = true;
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
    }
}