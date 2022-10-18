﻿using System;
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
    public partial class EstadoFinancieroFlujoEfectivoAgregado : System.Web.UI.Page
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

                    PeriodoDropDownList.DataSource = GetDataSetDropDownList_ConsultarPeriodo();
                    PeriodoDropDownList.DataBind();

                    UnidadTiempoPeriodoDropDownList.DataSource = GetDataSetDropDownList_BuscarCatalogoUnidadTiempoPeriodo();
                    UnidadTiempoPeriodoDropDownList.DataBind();

                    CambioPatrimonioNetoAgregadoMultiView.ActiveViewIndex = 0;
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

        protected void ViewCambioPatrimonioNetoAgregadoLinkButton_Click(object sender, EventArgs e)
        {
            CambioPatrimonioNetoAgregadoMultiView.ActiveViewIndex = 0;
            ViewReporteLinkButton.Visible = false;
        }

        protected void ViewReporteLinkButton_Click(object sender, EventArgs e)
        {
            CambioPatrimonioNetoAgregadoMultiView.ActiveViewIndex = 1;
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

        /**/
        /// <summary>
        /// Método: GetDataSet_ConsultarCatalogoAmbitoConsolidacion_Vacio
        /// Descripción:   Devuelve un DataSet Vacio.
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <returns></returns>
        public DataSet GetDataSet_ConsultarPeriodo_Vacio()
        {
            DataSet dsNICSPDatosGridView = new DataSet();
            try
            {
                dsNICSPDatosGridView.Tables.Add(GetConsultarPeriodoVacio());

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
        } // Fin GetDataSet_ConsultarPeriodo_Vacio

        /// <summary>
        /// Método: GetConsultarPeriodoVacio
        /// Descripción:   .
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <returns></returns>
        public DataTable GetConsultarPeriodoVacio()
        {
            DataTable table = new DataTable();
            try
            {
                table.Columns.Add("IdFecha", typeof(string));
                table.Columns.Add("Fecha", typeof(string));

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
        }// Fin GetConsultarPeriodoVacio

        /// <summary>
        /// Método: GetDataSet_BuscarCatalogoUnidadTiempoPeriodo_Vacio
        /// Descripción:   Devuelve un DataSet Vacio.
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <returns></returns>
        public DataSet GetDataSet_BuscarCatalogoUnidadTiempoPeriodo_Vacio()
        {
            DataSet dsNICSPDatosGridView = new DataSet();
            try
            {
                dsNICSPDatosGridView.Tables.Add(GetBuscarCatalogoUnidadTiempoPeriodoVacio());

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
        } // Fin GetDataSet_BuscarCatalogoUnidadTiempoPeriodo_Vacio

        /// <summary>
        /// Método: GetBuscarCatalogoUnidadTiempoPeriodoVacio
        /// Descripción:   .
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <returns></returns>
        public DataTable GetBuscarCatalogoUnidadTiempoPeriodoVacio()
        {
            DataTable table = new DataTable();
            try
            {
                table.Columns.Add("UnidadTiempoPeriodo", typeof(string));
                table.Columns.Add("Descripcion", typeof(string));

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
        }// Fin GetBuscarCatalogoUnidadTiempoPeriodoVacio


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

        /**/

        /// <summary>
        /// Método: GetDataSetDropDownList_ConsultarPeriodo
        /// Descripción:   Devuelve un DataSet de la consulta de los catalogos de Periodo Disponibles.
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <returns></returns>
        public DataTable GetDataSetDropDownList_ConsultarPeriodo()
        {
            //DataSet dsNICSPDatosDropDownList = new DataSet();
            DataTable table = new DataTable();
            try
            {
                table.Columns.Add("IdFecha", typeof(string));
                table.Columns.Add("Fecha", typeof(string));

                // Generate rows and cells.           
                int numrows = int.Parse(ConfigurationManager.AppSettings["TotalAnnosEnConsultas"]);
                int Periodo = DateTime.Now.Year;

                DataRow row;
                for (int i = 0; i < numrows; i++)
                {
                    row = table.NewRow();
                    row["IdFecha"] = i;
                    row["Fecha"] = Periodo;
                    table.Rows.Add(row);
                    Periodo = Periodo - 1;
                }

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
        }// Fin GetDataSetDropDownList_ConsultarPeriodo

        /// <summary>
        /// Método: PeriodoDropDownList_Primero
        /// Descripción:   Carga el ítem inicial del “DropDownList”.
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PeriodoDropDownList_Primero(object sender, EventArgs e)
        {
            try
            {
                PeriodoDropDownList.Items.Remove(PeriodoDropDownList.Items.FindByValue(""));
                PeriodoDropDownList.Items.Insert(0, new ListItem("Seleccione ítem", ""));
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
        }// Fin PeriodoDropDownList_Primero






        /// <summary>
        /// Método: GetDataSetDropDownList_BuscarCatalogoUnidadTiempoPeriodo
        /// Descripción:   Devuelve un DataSet de la consulta de los catalogos de Entidades Disponibles.
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <returns></returns>
        public DataSet GetDataSetDropDownList_BuscarCatalogoUnidadTiempoPeriodo()
        {
            DataSet dsNICSPDatosDropDownList = new DataSet();
            wsPlantillasConsolidacion WSINICSP_PC = new wsPlantillasConsolidacion();
            try
            {

                dsNICSPDatosDropDownList = WSINICSP_PC.BuscarCatalogoUnidadesTiempoPeriodo(ConfigurationManager.AppSettings["UnidadesTiempoPeriodoPermitido"].ToString(), "", gstr_usuario);

                //if (dsNICSPDatosDropDownList.Tables["Result"].Rows[0]["status"].ToString() == "00")
                //{
                if (dsNICSPDatosDropDownList.Tables.Count > 0)//Si hizo la consulta pero No retorno nada
                //if (!dsNICSPDatosDropDownList.Tables["Table"].Rows.Count.Equals(0))//Si hizo la consulta pero No retorno nada
                {
                    for (int i = 0; i <= dsNICSPDatosDropDownList.Tables["Table"].Rows.Count - 1; i++)
                    {
                        string UnidadTiempoPeriodo = (string)dsNICSPDatosDropDownList.Tables["Table"].Rows[i]["UnidadTiempoPeriodo"];
                        string Descripcion = (string)dsNICSPDatosDropDownList.Tables["Table"].Rows[i]["Descripcion"];
                        dsNICSPDatosDropDownList.Tables["Table"].Rows[i]["Descripcion"] = UnidadTiempoPeriodo + "-" + Descripcion;
                    }
                }
                else
                {
                    dsNICSPDatosDropDownList = GetDataSet_BuscarCatalogoUnidadTiempoPeriodo_Vacio();
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
        }// Fin GetDataSetDropDownList_BuscarCatalogoUnidadTiempoPeriodo


        /// <summary>
        /// Método: CatalogoUnidadTiempoPeriodoDropDownList_Primero
        /// Descripción:   Carga el ítem inicial del “DropDownList”.
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CatalogoUnidadTiempoPeriodoDropDownList_Primero(object sender, EventArgs e)
        {
            try
            {
                UnidadTiempoPeriodoDropDownList.Items.Remove(UnidadTiempoPeriodoDropDownList.Items.FindByValue(""));
                UnidadTiempoPeriodoDropDownList.Items.Insert(0, new ListItem("Seleccione ítem", ""));
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
        }// Fin CatalogoUnidadTiempoPeriodoDropDownList_Primero

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
                string strUsuario, strIdEntidad, strPeriodo, strUnidadTiempoPeriodo;

                //strUsuario = User.Identity.Name;
                strUsuario = gstr_usuario;
                strIdEntidad = AmbitoConsolidacionDropDownList.SelectedValue;
                strPeriodo = PeriodoDropDownList.SelectedValue;
                strUnidadTiempoPeriodo = UnidadTiempoPeriodoDropDownList.SelectedValue;

                //ParametrosReporte oParam = new ParametrosReporte();
                List<ReportParameter> oParametros = new List<ReportParameter>();

                if ((!strIdEntidad.Equals(string.Empty)) && (!strPeriodo.Equals(string.Empty)) && (!strUnidadTiempoPeriodo.Equals(string.Empty)))
                {
                    oParametros.Add(new ReportParameter("IdUsuarioEjecutaRpt", strUsuario, false));
                    oParametros.Add(new ReportParameter("IdAmbitoConsolidacion", strIdEntidad, false));
                    oParametros.Add(new ReportParameter("Periodo", strPeriodo, false));
                    oParametros.Add(new ReportParameter("UnidadTiempoPeriodo", strUnidadTiempoPeriodo, false));

                    ParametrosReporte oParam = new ParametrosReporte(ConfigurationManager.AppSettings["ReporteEstadoFinancieroFlujoEfectivoAgregado"], oParametros, ConfigurationManager.AppSettings["ServidorReportes"]);

                    //oParam.DireccionReporte = ConfigurationManager.AppSettings["ReporteBitacoraFlowError"];
                    //oParam.Parametros = oParametros;
                    //oParam.ServidorReportes = ConfigurationManager.AppSettings["ServidorReportes"];
                    Session.Add("ParametrosReporte", oParam);
                    CambioPatrimonioNetoAgregadoMultiView.ActiveViewIndex = 1;
                    this.ViewCambioPatrimonioNetoAgregadoLinkButton.Visible = true;
                    this.ViewReporteLinkButton.Visible = true;
                    this.SeparadorCambioPatrimonioNetoAgregadoReporteLabel.Visible = true;
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