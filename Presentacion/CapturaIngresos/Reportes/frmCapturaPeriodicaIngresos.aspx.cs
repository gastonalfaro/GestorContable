using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Diagnostics;
using System.Configuration;
using System.Reflection;

using System.Data;
using System.IO;
using System.Net;

using System.Globalization;

using Microsoft.Reporting.WebForms;
using eWorld.UI;
using Presentacion.Compartidas;
using Presentacion.Compartidas.VisorReportes;

using Presentacion.wsSG;

namespace Presentacion.CapturaIngresos.Reportes
{
    public partial class frmCapturaPeriodicaIngresos : BASE
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
                            DesplegarError(false);
                            EntregaATiempoMultiView.ActiveViewIndex = 0;
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

                lblError.Text = "NICSP Excepcion:  " + ex.Message.ToString() + ". ";
                DesplegarError(true);
                #endregion
            }
            finally
            {

            }
        }

        protected void ViewEntregaATiempoLinkButton_Click(object sender, EventArgs e)
        {
            EntregaATiempoMultiView.ActiveViewIndex = 0;
            ViewReporteLinkButton.Visible = false;
        }

        protected void ViewReporteLinkButton_Click(object sender, EventArgs e)
        {
            EntregaATiempoMultiView.ActiveViewIndex = 1;
        }

        /// <summary>
        /// Método: DesplegarError
        /// Descripción:   Despliega error en pantalla dependiendo del parámetro de entrada True/False.
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <param name="boolError"></param>
        /// <returns></returns>
        public Boolean DesplegarError(Boolean boolError)
        {
            try
            {
                if (boolError)
                    lblError.Visible = true;
                else
                    lblError.Visible = false;

                return boolError;
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

                throw (ex);
            }
            finally
            {

            }
        } // Fin DesplegarError

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void VerReporteButton_Click(object sender, EventArgs e)
        {
            try
            {
                string strUsuario, strFechaInicio, strFechaFin,strPeriodo,strFormulario,strAnno,strPago,strInstitucion,strOficina,strServicio,strMoneda,strEstado;
                DateTime FechaInicio, FechaFin;
                strFechaInicio = "";
                strFechaFin = "";
                //strUsuario = User.Identity.Name;
                strUsuario = gstr_Usuario;
                //FechaInicio = FechaInicioCalendarPopup.SelectedDate;
                //FechaFin = FechaFinCalendarPopup.SelectedDate;

                strFechaInicio = this.txtFechaInicio.Text;
                strFechaFin = this.txtFechaFin.Text;
                if (this.txtPeriodo .Text == "")
                {
                    strPeriodo = null;
                }else
                {
                    strPeriodo = this.txtPeriodo.Text;
                }
                if (this.txtFormulario.Text == "")
                {
                    strFormulario = null;
                }
                else
                {
                    strFormulario = this.txtFormulario.Text;
                }

                if (this.txtAnno .Text == "")
                {
                    strAnno = null;
                }
                else
                {
                    strAnno = this.txtAnno.Text;
                }

                if (this.txtPago.Text == "")
                {
                    strPago = null;
                }
                else
                {
                    strPago = this.txtPago.Text;
                }

                if (this.txtInstitucion.Text == "")
                {
                    strInstitucion = null;
                }
                else
                {
                    strInstitucion = this.txtInstitucion.Text;
                }

                if (this.txtOficina.Text == "")
                {
                    strOficina = null;
                }
                else
                {
                    strOficina = this.txtOficina.Text;
                }

                if (this.txtServicio.Text == "")
                {
                    strServicio = null;
                }
                else
                {
                    strServicio = this.txtServicio.Text;
                }

                if (this.txtMoneda.Text == "")
                {
                    strMoneda = null;
                }
                else
                {
                    strMoneda = this.txtMoneda.Text;
                }

                if (this.txtEstado.Text == "")
                {
                    strEstado = null;
                }
                else
                {
                    strEstado = this.txtEstado.Text;
                }
               

                //strFechaInicio = String.Format("{0:yyyy}", FechaInicio) + '/' + String.Format("{0:MM}", FechaInicio) + '/' + String.Format("{0:dd}", FechaInicio) + ' ' +
                //    String.Format("{0:HH}", FechaInicio) + ':' +
                //    String.Format("{0:mm}", FechaInicio) + ':' +
                //    String.Format("{0:ss}", FechaInicio) + '.' +
                //    String.Format("{0:fff}", FechaInicio);

                //strFechaFin = String.Format("{0:yyyy}", FechaFin) + '/' + String.Format("{0:MM}", FechaFin) + '/' + String.Format("{0:dd}", FechaFin) + ' ' +
                //    String.Format("{0:HH}", FechaFin) + ':' +
                //    String.Format("{0:mm}", FechaFin) + ':' +
                //    String.Format("{0:ss}", FechaFin) + '.' +
                //    String.Format("{0:fff}", FechaFin);

                //ParametrosReporte oParam = new ParametrosReporte();
                List<ReportParameter> oParametros = new List<ReportParameter>();

                    oParametros.Add(new ReportParameter("IdUsuarioEjecutaRpt", strUsuario, false));
                    oParametros.Add(new ReportParameter("pFchPagoDesde", strFechaInicio, false));
                    oParametros.Add(new ReportParameter("pFchPagoHasta", strFechaFin, false));
                    oParametros.Add(new ReportParameter("pPeriodo", strPeriodo, false));
                    oParametros.Add(new ReportParameter("pIdFormulario", strFormulario, false));
                    oParametros.Add(new ReportParameter("pAnno", strAnno, false));
                    oParametros.Add(new ReportParameter("pIdPago", strPago, false));
                    oParametros.Add(new ReportParameter("pIdInstitucion", strInstitucion, false));
                    oParametros.Add(new ReportParameter("pIdOficina", strOficina, false));
                    oParametros.Add(new ReportParameter("pIdServicio", strServicio, false));
                    oParametros.Add(new ReportParameter("pIdMoneda", strMoneda, false));
                    oParametros.Add(new ReportParameter("pEstado", strEstado, false));
                    ParametrosReporte oParam = new ParametrosReporte(ConfigurationManager.AppSettings["ReporteCapturaIngresosPeriodico"], oParametros, ConfigurationManager.AppSettings["ServidorReportes"]);

                    //oParam.DireccionReporte = ConfigurationManager.AppSettings["ReporteBitacoraFlowError"];
                    //oParam.Parametros = oParametros;
                    //oParam.ServidorReportes = ConfigurationManager.AppSettings["ServidorReportes"];
                    Session.Add("ParametrosReporte", oParam);
                    EntregaATiempoMultiView.ActiveViewIndex = 1;
                    this.ViewEntregaATiempoLinkButton.Visible = true;
                    this.ViewReporteLinkButton.Visible = true;
                    this.SeparadorEntregaATiempoReporteLabel.Visible = true;

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

                lblError.Text = "NICSP Excepcion:  " + ex.Message.ToString() + ". ";
                DesplegarError(true);
                #endregion
            }
            finally
            {

            }
        }


    }
}