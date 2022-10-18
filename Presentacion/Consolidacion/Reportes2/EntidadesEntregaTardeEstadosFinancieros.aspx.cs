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

using Presentacion.wsPC;
using Presentacion.wsSG;

namespace Presentacion.Consolidacion.Reportes
{
    public partial class EntidadesEntregaTardeEstadosFinancieros : System.Web.UI.Page
    {
        private string gstr_usuario = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DesplegarError(false);
                if (!IsPostBack)
                {
                    gstr_usuario = clsSesion.Current.LoginUsuario;

                    AmbitoConsolidacionDropDownList.DataSource = GetDataSetDropDownList_ConsultarCatalogoAmbitoConsolidacion();
                    AmbitoConsolidacionDropDownList.DataBind();

                    //CatalogoEntidadesDropDownList.Items.Remove(CatalogoEntidadesDropDownList.Items.FindByValue(""));
                    CatalogoEntidadesDropDownList.Items.Insert(0, new ListItem("Seleccione Ámbito de Consolidación", ""));
                    CatalogoEntidadesDropDownList.DataBind();

                    EntregaATiempoMultiView.ActiveViewIndex = 0;
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
        /// Método: AmbitoConsolidacionDropDownList_Primero
        /// Descripción:   Carga el ítem inicial del “DropDownList”.
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AmbitoConsolidacionDropDownList_Primero(object sender, EventArgs e)
        {
            try
            {
                AmbitoConsolidacionDropDownList.Items.Remove(AmbitoConsolidacionDropDownList.Items.FindByValue(""));
                AmbitoConsolidacionDropDownList.Items.Insert(0, new ListItem("Seleccione ítem", ""));
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
        }// Fin AmbitoConsolidacionDropDownList_Primero

        /// <summary>
        /// Método: CatalogoEntidadesDropDownList_Primero
        /// Descripción:   Carga el ítem inicial del “DropDownList”.
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CatalogoEntidadesDropDownList_Primero(object sender, EventArgs e)
        {
            try
            {
                CatalogoEntidadesDropDownList.Items.Remove(CatalogoEntidadesDropDownList.Items.FindByValue(""));
                CatalogoEntidadesDropDownList.Items.Insert(0, new ListItem("Seleccione Ámbito de Consolidación", ""));
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
        }// Fin CatalogoEntidadesDropDownList_Primero


        /// <summary>
        /// Método: AmbitoConsolidacionDropDownList_SelectedIndexChanged
        /// Descripción:   Al Seleccionar un banco del “DropDownList” del EditItem, este genera la información correspondiente en un DataSet 
        /// de las oficinas que corresponden al banco elegido, para cargarlo en el “DropDownList” de las oficinas.
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AmbitoConsolidacionDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                CatalogoEntidadesDropDownList.DataSource = GetDataSetDropDownList_BuscarCatalogosEntidades(AmbitoConsolidacionDropDownList.SelectedValue);

                CatalogoEntidadesDropDownList.DataBind();

                CatalogoEntidadesDropDownList.Items.Remove(CatalogoEntidadesDropDownList.Items.FindByValue(""));
                CatalogoEntidadesDropDownList.Items.Insert(0, new ListItem("Seleccione ítem", ""));
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
        }// Fin AmbitoConsolidacionDropDownList_SelectedIndexChanged

        /// <summary>
        /// Método: GetDataSetDropDownList_BuscarCatalogosEntidades
        /// Descripción: Devuelve un DataSet de la busqueda de los catalogos de entidades, modificando el campo del nombre de la entidad (CodigoEntidad + NombreEntidad).  
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <param name="IdAmbitoConsolidacion"></param>
        /// <returns></returns>
        public DataSet GetDataSetDropDownList_BuscarCatalogosEntidades(string IdAmbitoConsolidacion)
        {
            DataSet dsNICSPDatosDropDownList = new DataSet();

            wsPlantillasConsolidacion WSINICSP_PC = new wsPlantillasConsolidacion();
            try
            {
                dsNICSPDatosDropDownList = WSINICSP_PC.uwsBuscarEntidadesDeUnAmbito(IdAmbitoConsolidacion, "", gstr_usuario);

                //if (dsNICSPDatosDropDownList.Tables["Result"].Rows[0]["status"].ToString() == "00")
                //{
                if (dsNICSPDatosDropDownList.Tables.Count > 0)//Si hizo la consulta pero No retorno nada
                //if (!dsNICSPDatosDropDownList.Tables["Table"].Rows.Count.Equals(0))//Si hizo la consulta pero No retorno nada
                {
                    for (int i = 0; i <= dsNICSPDatosDropDownList.Tables["Table"].Rows.Count - 1; i++)
                    {
                        string strIdUnidadConsolidacion = (string)dsNICSPDatosDropDownList.Tables["Table"].Rows[i]["IdUnidadConsolidacion"];
                        string strNomUnidad = (string)dsNICSPDatosDropDownList.Tables["Table"].Rows[i]["NomUnidad"];
                        dsNICSPDatosDropDownList.Tables["Table"].Rows[i]["IdUnidadConsolidacion"] = strIdUnidadConsolidacion.Trim();
                        dsNICSPDatosDropDownList.Tables["Table"].Rows[i]["NomUnidad"] = strIdUnidadConsolidacion.Trim() + "-" + strNomUnidad;
                    }
                }
                else
                {
                    dsNICSPDatosDropDownList = GetDataSet_ConsultarCatalogosEntidades_Vacio();
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
                WSINICSP_PC = null;
            }
        }// Fin GetDataSetDropDownList_BuscarCatalogosEntidades


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void VerReporteButton_Click(object sender, EventArgs e)
        {
            try
            {
                string strUsuario, strIdEntidad, strFechaInicio, strFechaFin;
                DateTime FechaInicio, FechaFin;

                //strUsuario = User.Identity.Name;
                strUsuario = gstr_usuario;
                strIdEntidad = CatalogoEntidadesDropDownList.SelectedValue;
                FechaInicio = FechaInicioCalendarPopup.SelectedDate;
                FechaFin = FechaFinCalendarPopup.SelectedDate;

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

                if ((!strUsuario.Equals(string.Empty)) && (!strIdEntidad.Equals(string.Empty)))
                {
                    oParametros.Add(new ReportParameter("IdUsuarioEjecutaRpt", strUsuario, false));
                    oParametros.Add(new ReportParameter("FechaInicio", strFechaInicio, false));
                    oParametros.Add(new ReportParameter("FechaFin", strFechaFin, false));
                    oParametros.Add(new ReportParameter("IdEntidad", strIdEntidad, false));

                    ParametrosReporte oParam = new ParametrosReporte(ConfigurationManager.AppSettings["ReporteEntidadesEntregaTardeEstadosFinancieros"], oParametros, ConfigurationManager.AppSettings["ServidorReportes"]);

                    //oParam.DireccionReporte = ConfigurationManager.AppSettings["ReporteBitacoraFlowError"];
                    //oParam.Parametros = oParametros;
                    //oParam.ServidorReportes = ConfigurationManager.AppSettings["ServidorReportes"];
                    Session.Add("ParametrosReporte", oParam);
                    EntregaATiempoMultiView.ActiveViewIndex = 1;
                    this.ViewEntregaATiempoLinkButton.Visible = true;
                    this.ViewReporteLinkButton.Visible = true;
                    this.SeparadorEntregaATiempoReporteLabel.Visible = true;
                }
                else
                {
                    lblError.Text = ConfigurationManager.AppSettings["strErrorParametros"].ToString();
                    DesplegarError(true);
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
        } // Fin btnSelectDepartamentoLinkButton_Click
    }
}