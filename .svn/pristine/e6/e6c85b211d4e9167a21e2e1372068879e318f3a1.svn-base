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
    public partial class frmCapturaPeriodicaIngresosUPR : BASE
    {
        private string gstr_Usuario = string.Empty;
        private string gstr_upr = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                
                gstr_Usuario = clsSesion.Current.LoginUsuario;
                gstr_upr = clsSesion.Current.SociedadUsr;
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
                            gstr_upr = clsSesion.Current.SociedadUsr;
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
        /// Método: GetDataSetDropDownList_ConsultarCatalogoAmbitoConsolidacion
        /// Descripción:   Devuelve un DataSet de la consulta de los catalogos de AmbitoConsolidacion Disponibles.
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <returns></returns>
        public DataSet GetDataSetDropDownList_ConsultarCatalogoAmbitoConsolidacion()
        {
            DataSet dsNICSPDatosDropDownList = new DataSet();
            wsSistemaGestor WSINICSP_SG = new wsSistemaGestor();

            try
            {

                dsNICSPDatosDropDownList = WSINICSP_SG.uwsConsultarAmbitosConsolidacion(null, null, null, null);

                //if (dsNICSPDatosDropDownList.Tables["Result"].Rows[0]["status"].ToString() == "00")
                //{
                if (dsNICSPDatosDropDownList.Tables.Count > 0)//Si hizo la consulta pero No retorno nada
                //if (!dsNICSPDatosDropDownList.Tables["Table"].Rows.Count.Equals(0))//Si hizo la consulta pero No retorno nada
                {
                    for (int i = 0; i <= dsNICSPDatosDropDownList.Tables["Table"].Rows.Count - 1; i++)
                    {
                        string strIdAmbitoConsolidacion = (string)dsNICSPDatosDropDownList.Tables["Table"].Rows[i]["IdAmbitoConsolidacion"];
                        string strNomCorto = (string)dsNICSPDatosDropDownList.Tables["Table"].Rows[i]["NomCorto"];

                        dsNICSPDatosDropDownList.Tables["Table"].Rows[i]["IdAmbitoConsolidacion"] = strIdAmbitoConsolidacion.Trim();
                        dsNICSPDatosDropDownList.Tables["Table"].Rows[i]["NomCorto"] = strIdAmbitoConsolidacion.Trim() + "-" + strNomCorto;
                    }
                }
                else
                {
                    dsNICSPDatosDropDownList = GetDataSet_ConsultarCatalogoAmbitoConsolidacion_Vacio();
                }

                //}

                return dsNICSPDatosDropDownList;
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
                dsNICSPDatosDropDownList = null;
                WSINICSP_SG = null;
            }
        }// Fin GetDataSetDropDownList_ConsultarCatalogoEntidades

        /// <summary>
        /// Método: GetDataSet_ConsultarCatalogoAmbitoConsolidacion_Vacio
        /// Descripción:   Devuelve un DataSet Vacio.
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <returns></returns>
        public DataSet GetDataSet_ConsultarCatalogoAmbitoConsolidacion_Vacio()
        {
            DataSet dsNICSPDatosGridView = new DataSet();
            try
            {
                dsNICSPDatosGridView.Tables.Add(GetCatalogoAmbitoConsolidacionVacio());

                return dsNICSPDatosGridView;
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
                dsNICSPDatosGridView = null;
            }
        } // Fin GetDataSet_ConsultarCatalogoAmbitoConsolidacion_Vacio

        /// <summary>
        /// Método: GetCatalogoAmbitoConsolidacionVacio
        /// Descripción:   .
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <returns></returns>
        public DataTable GetCatalogoAmbitoConsolidacionVacio()
        {
            DataTable table = new DataTable();
            try
            {
                table.Columns.Add("IdAmbitoConsolidacion", typeof(string));
                table.Columns.Add("NomCorto", typeof(string));

                table.Rows.Add("");
                return table;
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
                table = null;
            }
        }// Fin GetCatalogoAmbitoConsolidacionVacio

        /// <summary>
        /// Método: GetDataSet_ConsultarCatalogosEntidades_Vacio
        /// Descripción:   Devuelve un DataSet Vacio.
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <returns></returns>
        public DataSet GetDataSet_ConsultarCatalogosEntidades_Vacio()
        {
            DataSet dsNICSPDatosGridView = new DataSet();
            try
            {
                dsNICSPDatosGridView.Tables.Add(GetCatalogosEntidadesVacio());

                return dsNICSPDatosGridView;
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
                dsNICSPDatosGridView = null;
            }
        } // Fin GetDataSet_ConsultarCatalogosEntidades_Vacio

        /// <summary>
        /// Método: GetCatalogosEntidadesVacio
        /// Descripción:   .
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <returns></returns>
        public DataTable GetCatalogosEntidadesVacio()
        {
            DataTable table = new DataTable();
            try
            {
                table.Columns.Add("IdUnidadConsolidacion", typeof(string));
                table.Columns.Add("NomUnidad", typeof(string));

                table.Rows.Add("");
                return table;
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
                table = null;
            }
        }// Fin GetCatalogosEntidadesVacio

        /**/

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void VerReporteButton_Click(object sender, EventArgs e)
        {
            try
            {
                string strUsuario, strInstitucion, strFechaInicio, strFechaFin, strPeriodo, strFormulario, strAnno, strPago, strOficina, strServicio, strMoneda, strEstado;
                DateTime FechaInicio, FechaFin;

                //strUsuario = User.Identity.Name;
                strUsuario = gstr_Usuario;
                strInstitucion = gstr_upr;

                //FechaInicio = FechaInicioCalendarPopup.SelectedDate;
                //FechaFin = FechaFinCalendarPopup.SelectedDate;
                FechaInicio = Convert.ToDateTime(this.txtFechaInicio.Text);
                FechaFin = Convert.ToDateTime(this.txtFechaFin.Text);



                if (this.txtPeriodo.Text == "")
                {
                    strPeriodo = null;
                }
                else
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

                if (this.txtAnno.Text == "")
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

                strFechaInicio = String.Format("{0:yyyy}", FechaInicio) + '/' + String.Format("{0:MM}", FechaInicio) + '/' + String.Format("{0:dd}", FechaInicio) + ' ' +
                    String.Format("{0:HH}", FechaInicio) + ':' +
                    String.Format("{0:mm}", FechaInicio) + ':' +
                    String.Format("{0:ss}", FechaInicio) + '.' +
                    String.Format("{0:fff}", FechaInicio);

                strFechaFin = String.Format("{0:yyyy}", FechaFin) + '/' + String.Format("{0:MM}", FechaFin) + '/' + String.Format("{0:dd}", FechaFin) + ' ' +
                    String.Format("{0:HH}", FechaFin) + ':' +
                    String.Format("{0:mm}", FechaFin) + ':' +
                    String.Format("{0:ss}", FechaFin) + '.' +
                    String.Format("{0:fff}", FechaFin);

                //ParametrosReporte oParam = new ParametrosReporte();
                List<ReportParameter> oParametros = new List<ReportParameter>();

                oParametros.Add(new ReportParameter("IdUsuarioEjecutaRpt", strUsuario, false));
                oParametros.Add(new ReportParameter("pFchPagoDesde", strFechaInicio, false));
                oParametros.Add(new ReportParameter("pFchPagoHasta", strFechaFin, false));
                oParametros.Add(new ReportParameter("pIdInstitucion", strInstitucion, false));


                oParametros.Add(new ReportParameter("pPeriodo", strPeriodo, false));
                oParametros.Add(new ReportParameter("pIdFormulario", strFormulario, false));
                oParametros.Add(new ReportParameter("pAnno", strAnno, false));
                oParametros.Add(new ReportParameter("pIdPago", strPago, false));
                oParametros.Add(new ReportParameter("pIdOficina", strOficina, false));
                oParametros.Add(new ReportParameter("pIdServicio", strServicio, false));
                oParametros.Add(new ReportParameter("pIdMoneda", strMoneda, false));
                oParametros.Add(new ReportParameter("pEstado", strEstado, false));



                ParametrosReporte oParam = new ParametrosReporte(ConfigurationManager.AppSettings["ReporteCapturaIngresosPeriodicoUPR"], oParametros, ConfigurationManager.AppSettings["ServidorReportes"]);

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