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

using System.Web.UI.HtmlControls;

namespace Presentacion.Consolidacion.Reportes
{
    public partial class BitacoraErroresPorFechaProcesoDTSX : BASE
    {
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        //Presentacion.wsPC.wsPlantillasConsolidacion WSINICSP_PC = new Presentacion.wsPC.wsPlantillasConsolidacion();
        private string gstr_Usuario = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                DesplegarError(false);
                gstr_Usuario = clsSesion.Current.LoginUsuario;

                if (String.IsNullOrEmpty(gstr_Usuario))
                {
                    Response.Redirect("~/Login.aspx", true);
                }

                if (!IsPostBack)
                {
                    MostrarElementos(gstr_Usuario);
                    Session["Buscar"] = false;
                    ConsultarBitacoraFlowError_GridView();
                    GridView.SelectedIndex = 0;

                    BitacoraErrorMultiView.ActiveViewIndex = 0;
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

        public static Control FindControlRecursive(Control root, string id)
        {
            try
            {
                if (id == string.Empty)
                    return null;

                if (root.ID == id)
                    return root;

                foreach (Control c in root.Controls)
                {
                    Control t = FindControlRecursive(c, id);
                    if (t != null)
                    {
                        return t;
                    }
                }
                
            }
            catch { }
            return null;
        }

        private void MostrarElementos(string str_usuario)
        {
            try
            {
                DataSet ldt_PermisosUsuario = ws_SGService.uwsConsultarPermisosUsuarios(str_usuario, "");

                for (int i = 0; ldt_PermisosUsuario.Tables["Table"].Rows.Count > i; i++)
                {
                    string lstr_IdObjeto = ldt_PermisosUsuario.Tables["Table"].Rows[i]["IdObjeto"].ToString();
                    bool lbool_Actualizar = (bool)ldt_PermisosUsuario.Tables["Table"].Rows[i]["Actualizar"];
                    bool lbool_Consultar = (bool)ldt_PermisosUsuario.Tables["Table"].Rows[i]["Consultar"];
                    string lstr_IdliEncabezado = "li" + lstr_IdObjeto;

                    if (lbool_Consultar)
                    {
                        try
                        {
                            HtmlGenericControl hgcMenuEncabezado = (HtmlGenericControl)FindControlRecursive(Master.Page, lstr_IdliEncabezado);

                            if (hgcMenuEncabezado != null)
                                hgcMenuEncabezado.Visible = true;
                        }
                        catch { }
                    }
                }
            }
            catch { }
        }


        protected void ViewBitacoraErrorLinkButton_Click(object sender, EventArgs e)
        {
            BitacoraErrorMultiView.ActiveViewIndex = 0;
            //ViewReporteLinkButton.Visible = false;
        }

        protected void ViewReporteLinkButton_Click(object sender, EventArgs e)
        {
            BitacoraErrorMultiView.ActiveViewIndex = 1;
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
        /// Método: ConsultarDataSet_BitacoraFlowError_GridView
        /// Descripción:   Carga el GridView desde un DataSet, modificando el campo del nombre de la oficina (CodigoOficina + NombreOficina).
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <param name="dsBitacoraFlowError"></param>
        /// <returns></returns>
        public Boolean ConsultarDataSet_BitacoraFlowError_GridView(DataSet dsBitacoraFlowError)
        {
            try
            {
                GridView.DataSource = dsBitacoraFlowError;
                GridView.DataBind();

                Session["Buscar"] = true;
                Session["DataSet_GridView"] = dsBitacoraFlowError;

                return true;
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
        } // Fin ConsultarDataSet_BitacoraFlowError_GridView 

        /// <summary>
        /// Método: ConsultarBitacoraFlowError_GridView
        /// Descripción:   
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <returns></returns>
        public Boolean ConsultarBitacoraFlowError_GridView()
        {
            try
            {
                GridView.DataSource = GetDataSetGridView_ConsultarBitacoraFlowError();
                GridView.DataBind();

                return true;
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
        } // Fin ConsultarBitacoraFlowError_GridView  


        /// <summary>
        /// Método: GetDataSetGridView_ConsultarBitacoraFlowError
        /// Descripción:   
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <returns></returns>
        public DataSet GetDataSetGridView_ConsultarBitacoraFlowError()
        {
            DataSet dsNICSPDatosGridView = new DataSet();
            wsPlantillasConsolidacion WSINICSP_PC = new wsPlantillasConsolidacion();
            string Resultado, Mensaje;        
            try
            {
                dsNICSPDatosGridView = WSINICSP_PC.ConsultarBitacoraFlujoErroresDTSX("", gstr_Usuario, out Resultado, out Mensaje);
                //if (dsNICSPDatosGridView.Tables["Result"].Rows[0]["status"].ToString() == "00")
                //{
                    if (!(dsNICSPDatosGridView.Tables.Count > 0))//Si hizo la consulta pero No retorno nada
                    //if (dsNICSPDatosGridView.Tables["Table"].Rows.Count.Equals(0))//Si hizo la consulta pero No retorno nada
                    {
                        dsNICSPDatosGridView = GetDataSetGridView_ConsultarBitacoraFlowError_Vacio();
                    }
                //}
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
                WSINICSP_PC = null;
            }
        } // Fin GetDataSetGridView_ConsultarBitacoraFlowError 

        /// <summary>
        /// Método: GetDataSetGridView_ConsultarBitacoraFlowError_Vacio
        /// Descripción:   Devuelve un DataSet Vacio.
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <returns></returns>
        public DataSet GetDataSetGridView_ConsultarBitacoraFlowError_Vacio()
        {
            DataSet dsNICSPDatosGridView = new DataSet();
            try
            {
                dsNICSPDatosGridView.Tables.Add(GetConsultarBitacoraFlowErrorVacio());

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
        } // Fin GetDataSetGridView_ConsultarBitacoraFlowError_Vacio

        /// <summary>
        /// Método: GetConsultarBitacoraFlowErrorVacio
        /// Descripción:   .
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <returns></returns>
        public DataTable GetConsultarBitacoraFlowErrorVacio()
        {
            DataTable table = new DataTable();
            try
            {
                table.Columns.Add("Fecha", typeof(string));
                table.Columns.Add("NombreProceso", typeof(string));

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
        }// Fin GetConsultarBitacoraFlowErrorVacio

        /*Buscar*/

        /// <summary>
        /// Método: btnSearch_Click
        /// Descripción:   Realiza la carga de los parámetros para después llamar a “Buscar_BitacoraFlowError” 
        /// para que busque la información, después de que retorne una respuesta, carga el GridView con la información pertinente.
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string NombreProceso;
                DateTime Fecha;

                Fecha = ((CalendarPopup)GridView.FooterRow.FindControl("FooterFechaCalendarPopup")).SelectedDate.Date;// .ToShortDateString();// .ToString();

                //Fecha = ((TextBox)GridView.FooterRow.FindControl("FooterFechaTextBox")).Text;

                NombreProceso = ((TextBox)GridView.FooterRow.FindControl("FooterNombreProcesoTextBox")).Text;

                if ((!Fecha.Equals(string.Empty)) || (!NombreProceso.Equals(string.Empty)))
                {
                    DesplegarError(Buscar_BitacoraFlowError(Fecha, NombreProceso));
                }
                else
                {
                    Session["Buscar"] = false;
                    ConsultarBitacoraFlowError_GridView();
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

        } // Fin btnSearch_Click


        /*Editar*/

        /// <summary>
        /// Método: btnSelectDepartamentoLinkButton_Click
        /// Descripción:   
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelectBitacoraLinkButton_Click(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                string strUsuario, strNombreProceso, strFecha;
                DateTime Fecha;

                GridView.SelectedIndex = e.RowIndex;

                strUsuario = gstr_Usuario; // User.Identity.Name;

                strFecha = ((Label)GridView.Rows[e.RowIndex].FindControl("ItemFechaLabel")).Text;
   
                strNombreProceso = ((Label)GridView.Rows[e.RowIndex].FindControl("ItemNombreProcesoLabel")).Text;

                Fecha = DateTime.Parse(strFecha);
                strFecha = Fecha.ToUniversalTime().ToString();

                strFecha = String.Format("{0:yyyy}", Fecha) + '/' + String.Format("{0:MM}", Fecha) + '/' + String.Format("{0:dd}", Fecha) + ' ' +
                    String.Format("{0:HH}", Fecha) + ':' +
                    String.Format("{0:mm}", Fecha) + ':' +
                    String.Format("{0:ss}", Fecha) + '.' +
                    String.Format("{0:fff}", Fecha);

                //ParametrosReporte oParam = new ParametrosReporte();
                List<ReportParameter> oParametros = new List<ReportParameter>();

                if ((!strFecha.Equals(string.Empty)) && (!strNombreProceso.Equals(string.Empty)))
                {
                    oParametros.Add(new ReportParameter("IdUsuarioEjecutaRpt", strUsuario, false));
                    oParametros.Add(new ReportParameter("Fecha", strFecha, false));
                    oParametros.Add(new ReportParameter("NombreProceso", strNombreProceso, false));

                    ParametrosReporte oParam = new ParametrosReporte(ConfigurationManager.AppSettings["ReporteBitacoraFlowError"], oParametros, ConfigurationManager.AppSettings["ServidorReportes"]);

                    //oParam.DireccionReporte = ConfigurationManager.AppSettings["ReporteBitacoraFlowError"];
                    //oParam.Parametros = oParametros;
                    //oParam.ServidorReportes = ConfigurationManager.AppSettings["ServidorReportes"];
                    Session.Add("ParametrosReporte", oParam);
                    BitacoraErrorMultiView.ActiveViewIndex = 1;
                    this.ViewBitacoraErrorLinkButton.Visible = true;
                    this.ViewReporteLinkButton.Visible = true;
                    this.SeparadorBitacoraErrorReporteLabel.Visible = true;
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

        /// <summary>
        /// Método: GridView_RowCancelingEdit
        /// Descripción:   Quita los campos edición y retorna el GridView a su estado original y carga la 
        /// información llamando a “ConsultarBitacoraFlowError_GridView”.
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                GridView.EditIndex = -1;
                ConsultarBitacoraFlowError_GridView();
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
        } // Fin GridView_RowCancelingEdit

        /*Ordenar*/

        /// <summary>
        /// Método: gridView_PageIndexChanging
        /// Descripción:   Despliega en el GridView el nuevo índex solicitado.
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                Boolean Busqueda = false;
                Busqueda = Boolean.Parse(Session["Buscar"].ToString());
                DataSet DataSet;
                DataTable dataTable;

                if (Busqueda)
                {
                    DataSet = (DataSet)Session["DataSet_GridView"];
                    dataTable = DataSet.Tables[0];
                }
                else
                {
                    DataSet = GetDataSetGridView_ConsultarBitacoraFlowError();
                    dataTable = DataSet.Tables[0];
                }

                if (dataTable != null)
                {
                    DataView dataView = new DataView(dataTable);
                    GridView.DataSource = dataView;
                    GridView.PageIndex = e.NewPageIndex;
                    GridView.DataBind();
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
        }// Fin gridView_PageIndexChanging

        /*SP de DB*/

        /// <summary>
        /// Método: Buscar_BitacoraFlowError
        /// Descripción:   Busca la información de los BitacoraFlowError en la DB con respecto al parámetro suministrado, 
        /// después retorna una respuesta dependiendo del resultado de la Búsqueda (true/false).
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <param name="Fecha"></param>
        /// <param name="NombreProceso"></param>
        /// <returns></returns>
        protected Boolean Buscar_BitacoraFlowError(DateTime Fecha, string NombreProceso)
        {
            Boolean boolError = true;

            DataSet dsNICSPDatos = new DataSet();
            wsPlantillasConsolidacion WSINICSP_PC = new wsPlantillasConsolidacion();

            try
            {
                string Resultado, Mensaje;
                dsNICSPDatos = WSINICSP_PC.BuscarBitacoraFlujoErroresDTSX(NombreProceso, Fecha, Fecha, "", gstr_Usuario, out Resultado, out Mensaje);

                //if (dsNICSPDatos.Tables["Result"].Rows[0]["status"].ToString() != "00")
                //{
                //    //Error
                //    lblError.Text = dsNICSPDatos.Tables["Result"].Rows[0]["message"].ToString();
                //    boolError = true;
                //}
                //else
                //{
                    //if (dsNICSPDatos.Tables.Count > 0)//Si hizo la consulta pero No retorno nada
                    if (!dsNICSPDatos.Tables["Table"].Rows.Count.Equals(0))//Si hizo la consulta pero No retorno nada
                    {
                        if (ConsultarDataSet_BitacoraFlowError_GridView(dsNICSPDatos))
                        {
                            boolError = false;
                            Session["Buscar"] = true;
                        }
                        else
                            boolError = true;
                    }
                    else
                    {
                        ConsultarDataSet_BitacoraFlowError_GridView(GetDataSetGridView_ConsultarBitacoraFlowError_Vacio());

                        lblError.Text = ConfigurationManager.AppSettings["strErrorRetornoNulo"].ToString();
                        boolError = true;
                    }
                //}
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
                dsNICSPDatos = null;
                WSINICSP_PC = null;
            }
        }

        protected void Unnamed_ServerClick(object sender, EventArgs e)
        {
            try
            {
                DataSet ldt_PermisosUsuario = ws_SGService.uwsConsultarPermisosUsuarios(gstr_Usuario, "");


                for (int i = 0; ldt_PermisosUsuario.Tables["Table"].Rows.Count > i; i++)
                {
                    string lstr_IdObjeto = ldt_PermisosUsuario.Tables["Table"].Rows[i]["IdObjeto"].ToString();
                    bool lbool_Consultar = (bool)ldt_PermisosUsuario.Tables["Table"].Rows[i]["Consultar"];

                    if (lbool_Consultar)
                    {
                        if (lstr_IdObjeto.Equals("frmCargaEntidad"))
                        {
                            Response.Redirect("~/Consolidacion/frmCargaEntidad.aspx", true);
                        }
                        else if (lstr_IdObjeto.Equals("frmRevisionAnalista"))
                        {
                            Response.Redirect("~/Consolidacion/frmRevisionAnalista.aspx", true);
                        }
                        else if (lstr_IdObjeto.Equals("frmRevisionEntidad"))
                        {
                            Response.Redirect("~/Consolidacion/frmRevisionEntidad.aspx", true);
                        }
                    }
                }
            }
            catch { }
        }// Fin Buscar_BitacoraFlowError

    }
}