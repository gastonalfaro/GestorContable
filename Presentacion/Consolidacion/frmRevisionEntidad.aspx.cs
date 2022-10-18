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
//using System.Data.SqlClient;
using System.Data.SqlTypes;

using System.Globalization;

using Microsoft.Reporting.WebForms;
using eWorld.UI;
using Presentacion.Compartidas;

using Presentacion.wsPC;
using Presentacion.wsSG;

using System.Xml;
using System.Web.UI.HtmlControls;

using System.Net.Mail;
using System.Text;
using System.Net.Mime;

using System.Threading;

namespace Presentacion.Consolidacion
{
    public partial class frmRevisionEntidad : BASE
    {
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private string gstr_Usuario = String.Empty;
        private string gstr_Institucion = String.Empty;
        private string EtadaActual = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                gstr_Usuario = clsSesion.Current.LoginUsuario;
                gstr_Institucion = clsSesion.Current.SociedadUsr;

                if (!IsPostBack)
                {
                    if (!string.IsNullOrEmpty(gstr_Usuario))
                    {
                        if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "OBJ_PC"))
                            Response.Redirect("~/Principal.aspx", true);
                        else
                        {

                            txtError.Text = "";
                            DesplegarError(false);
                            gstr_Institucion = clsSesion.Current.SociedadUsr;

                            ViewCargaEntidadLinkButton.Visible = true;

                            SeparadorCargaEntidadArchivosAnexosLabel.Visible = true;
                            ViewArchivosAnexosLinkButton.Visible = true;

                            SeparadorArchivosAnexosArchivosPlantillasLabel.Visible = true;
                            ViewArchivosPlantillasLinkButton.Visible = true;

                            SeparadorCargaEntidadCorreosAutorizacionLabel.Visible = true;
                            ViewCorreosAutorizacionLinkButton.Visible = true;

                            AmbitoConsolidacionDropDownList.DataSource = GetDataSetDropDownList_ConsultarCatalogoAmbitoConsolidacion();
                            AmbitoConsolidacionDropDownList.DataBind();

                            CatalogoEntidadesDropDownList.Items.Insert(0, new ListItem("Seleccione Ámbito de Consolidación", ""));
                            CatalogoEntidadesDropDownList.DataBind();

                            PeriodoDropDownList.DataSource = GetDataSetDropDownList_ConsultarPeriodo();
                            PeriodoDropDownList.DataBind();

                            UnidadTiempoPeriodoDropDownList.DataSource = GetDataSetDropDownList_BuscarCatalogoUnidadTiempoPeriodo();
                            UnidadTiempoPeriodoDropDownList.DataBind();

                            /*View archivos anexos*/
                            //EstadoFinancieroDropDownList.DataSource = GetDataSetDropDownList_ConsultarEstadoFinanciero();
                            //EstadoFinancieroDropDownList.DataBind();

                            /*View Archivos Plantillas*/
                            //EstadoFinancieroPlantillasDropDownList.DataSource = GetDataSetDropDownList_ConsultarEstadoFinanciero();
                            //EstadoFinancieroPlantillasDropDownList.DataBind();

                            //ConsultarEstadosFinancierosCargados_GridView();
                            EstadosFinancierosArchivosGridView.DataSource = GetDataSetGridView_ConsultarEstadosFinancierosCargados_Vacio();
                            EstadosFinancierosArchivosGridView.DataBind();

                            //ConsultarArchivosAnexosEstadosFinancierosCargados_GridView();
                            EstadosFinancierosArchivosAnexosGridView.DataSource = GetDataSetGridView_ConsultarArchivosAnexosEstadosFinancierosCargados_Vacio();
                            EstadosFinancierosArchivosAnexosGridView.DataBind();

                            //ConsultarArchivosPlantillasEstadosFinancierosCargados_GridView();
                            EstadosFinancierosArchivosPlantillasGridView.DataSource = GetDataSetGridView_ConsultarArchivosPlantillasEstadosFinancierosCargados_Vacio();
                            EstadosFinancierosArchivosPlantillasGridView.DataBind();

                            //ConsultarCorreosAutorizacionEstadosFinancierosCargados_GridView();
                            CorreosAutorizacionAnexosGridView.DataSource = GetDataSetGridView_ConsultarArchivosAnexosEstadosFinancierosCargados_Vacio();
                            CorreosAutorizacionAnexosGridView.DataBind();

                            //FlujoEfectivoFileUpload.Enabled = false;
                            //CambioPatrimonioNetoFileUpload.Enabled = false;
                            //NotasEstadosFinancierosFileUpload.Enabled = false;
                            //BalanceComprobacionFileUpload.Enabled = false;
                            //EstadoVariosFileUpload.Enabled = false;

                            //CargarArchivosButton.Enabled = false;
                            //RevisionInstitucionButton.Enabled = false;


                            //cambio ******************************************************************
                            //****************************************************************
                            RechazadoInstitucionButton.Enabled = false;
                            AprobarInstitucionButton.Enabled = false;

                            CargaEntidadMultiView.ActiveViewIndex = 0;
                        }
                    }
                    else
                        Response.Redirect("~/Login.aspx", true);
                }
                ValidarVisibilidadEstadosEtapas();
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

                txtError.Text = txtError.Text + "\r\n" + "NICSP Excepcion:  " + ex.Message.ToString() + ". ";
                DesplegarError(true);
                #endregion
            }
            finally
            {

            }
        }

        public static Control FindControlRecursive(Control root, string id)
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

        protected void ViewCargaEntidadLinkButton_Click(object sender, EventArgs e)
        {
            CargaEntidadMultiView.ActiveViewIndex = 0;
            //VisibleParametrosPrincipales(true);

            HabilitarMenuPrincipalCombos(true);
        }

        protected void ViewArchivosAnexosLinkButton_Click(object sender, EventArgs e)
        {
            CargaEntidadMultiView.ActiveViewIndex = 1;
            //VisibleParametrosPrincipales(true);

            HabilitarMenuPrincipalCombos(false);
        }

        protected void ViewArchivosPlantillasLinkButton_Click(object sender, EventArgs e)
        {
            CargaEntidadMultiView.ActiveViewIndex = 2;
            //VisibleParametrosPrincipales(false);

            HabilitarMenuPrincipalCombos(false);
        }

        protected void ViewCorreosAutorizacionLinkButton_Click(object sender, EventArgs e)
        {
            CargaEntidadMultiView.ActiveViewIndex = 3;
            //VisibleParametrosPrincipales(true);

            HabilitarMenuPrincipalCombos(false);
        }

        public void HabilitarMenuPrincipalCombos(bool Enabled)
        {
            try
            {
                AmbitoConsolidacionDropDownList.Enabled = Enabled;
                CatalogoEntidadesDropDownList.Enabled = Enabled;
                PeriodoDropDownList.Enabled = Enabled;
                UnidadTiempoPeriodoDropDownList.Enabled = Enabled;
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

                txtError.Text = txtError.Text + "\r\n" + "NICSP Excepcion:  " + ex.Message.ToString() + ". ";
                DesplegarError(true);
                #endregion
            }
            finally
            {

            }
        }

        /**/
        /// <summary>
        /// Método: ConsultarEstadosFinancierosCargados_GridView
        /// Descripción:   Carga el GridView con el DataSet que retorna GetDataSetGridView_ConsultarEstadosFinancierosCargados
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <returns></returns>
        public Boolean ConsultarEstadosFinancierosCargados_GridView()
        {
            try
            {
                EstadosFinancierosArchivosGridView.DataSource = GetDataSetGridView_ConsultarEstadosFinancierosCargados();
                EstadosFinancierosArchivosGridView.DataBind();

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
        } // Fin ConsultarEstadosFinancierosCargados_GridView  

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string BuscarUnidadTiempoPeriodoActual()
        {
            try
            {

                return "";
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

        }

        /// <summary>
        /// Método: GetDataSetGridView_ConsultarEstadosFinancierosCargados
        /// Descripción:   Devuelve un DataSet de la consulta de los Estados Financieros Cargados
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <returns></returns>
        public DataSet GetDataSetGridView_ConsultarEstadosFinancierosCargados()
        {
            string strAuxCod = "", strAuxDes = "";
            DataSet dsNICSPDatosGridView = new DataSet();
            wsPlantillasConsolidacion WSINICSP_PC = new wsPlantillasConsolidacion();
            try
            {
                string strUsuario, strIdEntidad, strPeriodo, strUnidadTiempoPeriodo;

                strUsuario = gstr_Usuario;
                strIdEntidad = CatalogoEntidadesDropDownList.SelectedValue;
                strPeriodo = PeriodoDropDownList.SelectedItem.Text;
                strUnidadTiempoPeriodo = UnidadTiempoPeriodoDropDownList.SelectedValue;

                string CodigoEstadosFinancieros_EstadoFlujoEfectivo, CodigoEstadosFinancieros_EstadoCambioPatrimonioNeto,
                    CodigoEstadosFinancieros_ConsolidacionNotas, CodigoEstadosFinancieros_EstadoBalanceComprobacion, CodigoEstadosFinancieros_EstadoVarios;

                string Resultado, Mensaje;

                CodigoEstadosFinancieros_EstadoFlujoEfectivo = ConfigurationManager.AppSettings["CodigoEstadosFinancieros_EstadoFlujoEfectivo"].ToString();
                CodigoEstadosFinancieros_EstadoCambioPatrimonioNeto = ConfigurationManager.AppSettings["CodigoEstadosFinancieros_EstadoCambioPatrimonioNeto"].ToString();
                CodigoEstadosFinancieros_ConsolidacionNotas = ConfigurationManager.AppSettings["CodigoEstadosFinancieros_ConsolidacionNotas"].ToString();
                CodigoEstadosFinancieros_EstadoBalanceComprobacion = ConfigurationManager.AppSettings["CodigoEstadosFinancieros_EstadoBalanceComprobacion"].ToString();
                CodigoEstadosFinancieros_EstadoVarios = ConfigurationManager.AppSettings["CodigoEstadosFinancieros_EstadoVarios"].ToString();

                if (strPeriodo.Equals(string.Empty))
                    strPeriodo = DateTime.Now.Year.ToString();

                //if (strIdEntidad.Equals(string.Empty))
                //    strIdEntidad = gstr_Institucion;

                //if (strUnidadTiempoPeriodo.Equals(string.Empty))
                //    strUnidadTiempoPeriodo = BuscarUnidadTiempoPeriodoActual();

                if ((!strIdEntidad.Equals(string.Empty)) && (!strPeriodo.Equals("Seleccione ítem")) && (!strUnidadTiempoPeriodo.Equals(string.Empty)))
                {

                    dsNICSPDatosGridView = WSINICSP_PC.ConsultarEstadoFinancierosCargados(strIdEntidad, int.Parse(strPeriodo), strUnidadTiempoPeriodo, "", strUsuario, out Resultado, out Mensaje);

                    //if (dsNICSPDatosGridView.Tables["Result"].Rows[0]["status"].ToString() == "00")
                    //{
                    //if (dsNICSPDatosGridView.Tables.Count > 0)//Si hizo la consulta pero No retorno nada
                    if (!dsNICSPDatosGridView.Tables["Table"].Rows.Count.Equals(0))//Si hizo la consulta pero No retorno nada
                    {
                        //FlujoEfectivoFileUpload.Enabled = true;
                        //CambioPatrimonioNetoFileUpload.Enabled = true;
                        //NotasEstadosFinancierosFileUpload.Enabled = true;
                        //BalanceComprobacionFileUpload.Enabled = true;
                        //EstadoVariosFileUpload.Enabled = true;

                        for (int i = 0; i <= dsNICSPDatosGridView.Tables["Table"].Rows.Count - 1; i++)
                        {
                            strAuxCod = dsNICSPDatosGridView.Tables["Table"].Rows[i]["IdEstadoFinanciero"].ToString();
                            strAuxDes = dsNICSPDatosGridView.Tables["Table"].Rows[i]["NombreEstadoFinanciero"].ToString();
                            if ((!strAuxCod.Equals("")) && (!strAuxDes.Equals("")))
                            {
                                dsNICSPDatosGridView.Tables["Table"].Rows[i]["NombreEstadoFinanciero"] = strAuxCod + "-" + strAuxDes;

                                //if (strAuxCod.Equals(CodigoEstadosFinancieros_EstadoFlujoEfectivo))
                                //    FlujoEfectivoFileUpload.Enabled = false;
                                //else
                                //    if (strAuxCod.Equals(CodigoEstadosFinancieros_EstadoCambioPatrimonioNeto))
                                //        CambioPatrimonioNetoFileUpload.Enabled = false;
                                //    else
                                //        if (strAuxCod.Equals(CodigoEstadosFinancieros_ConsolidacionNotas))
                                //            NotasEstadosFinancierosFileUpload.Enabled = false;
                                //        else
                                //            if (strAuxCod.Equals(CodigoEstadosFinancieros_EstadoBalanceComprobacion))
                                //                BalanceComprobacionFileUpload.Enabled = false;
                                //            else
                                //                if (strAuxCod.Equals(CodigoEstadosFinancieros_EstadoVarios))
                                //                    EstadoVariosFileUpload.Enabled = false;

                                strAuxCod = "";
                                strAuxDes = "";

                                strAuxCod = dsNICSPDatosGridView.Tables["Table"].Rows[i]["UnidadTiempoPeriodo"].ToString();
                                strAuxDes = dsNICSPDatosGridView.Tables["Table"].Rows[i]["DescripcionUnidadTiempoPeriodo"].ToString();
                                if ((!strAuxCod.Equals("")) && (!strAuxDes.Equals("")))
                                    dsNICSPDatosGridView.Tables["Table"].Rows[i]["DescripcionUnidadTiempoPeriodo"] = strAuxCod + "-" + strAuxDes;

                                strAuxCod = "";
                                strAuxDes = "";
                            }
                        }
                        //CargarArchivosButton.Enabled = true;
                        //if ((FlujoEfectivoFileUpload.Enabled.Equals(false)) && (CambioPatrimonioNetoFileUpload.Enabled.Equals(false)) && (NotasEstadosFinancierosFileUpload.Enabled.Equals(false)) && (BalanceComprobacionFileUpload.Enabled.Equals(false)) && (EstadoVariosFileUpload.Enabled.Equals(false)))
                        //{
                        //    //RevisionInstitucionButton.Enabled = true;
                        //    RechazadoInstitucionButton.Enabled = true;
                        //    AprobarInstitucionButton.Enabled = true;
                        //}
                        //else
                        //{
                        //    //RevisionInstitucionButton.Enabled = false;
                        //    RechazadoInstitucionButton.Enabled = false;
                        //    AprobarInstitucionButton.Enabled = false;
                        //}
                    }
                    else
                    {
                        //CargarArchivosButton.Enabled = true;

                        //FlujoEfectivoFileUpload.Enabled = true;
                        //CambioPatrimonioNetoFileUpload.Enabled = true;
                        //NotasEstadosFinancierosFileUpload.Enabled = true;
                        //BalanceComprobacionFileUpload.Enabled = true;
                        //EstadoVariosFileUpload.Enabled = true;
                        dsNICSPDatosGridView = GetDataSetGridView_ConsultarEstadosFinancierosCargados_Vacio();
                    }
                }
                else
                {
                    DesplegarError(true);
                    txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorParametros"].ToString();
                    dsNICSPDatosGridView = GetDataSetGridView_ConsultarEstadosFinancierosCargados_Vacio();
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
        } // Fin GetDataSetGridView_ConsultarEstadosFinancierosCargados 

        /// <summary>
        /// Método: GetDataSetGridView_ConsultarEstadosFinancierosCargados_Vacio
        /// Descripción:   Devuelve un DataSet Vacio.
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <returns></returns>
        public DataSet GetDataSetGridView_ConsultarEstadosFinancierosCargados_Vacio()
        {
            DataSet dsNICSPDatosGridView = new DataSet();
            try
            {
                dsNICSPDatosGridView.Tables.Add(GetConsultarEstadosFinancierosCargadosVacio());

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
        } // Fin GetDataSetGridView_ConsultarEstadosFinancierosCargados_Vacio

        /// <summary>
        /// Método: GetConsultarEstadosFinancierosCargadosVacio
        /// Descripción:   .
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <returns></returns>
        public DataTable GetConsultarEstadosFinancierosCargadosVacio()
        {
            DataTable table = new DataTable();
            try
            {
                table.Columns.Add("IdEntidad", typeof(string));
                table.Columns.Add("IdEstadoFinanciero", typeof(string));
                table.Columns.Add("Periodo", typeof(string));
                table.Columns.Add("UnidadTiempoPeriodo", typeof(string));
                table.Columns.Add("IdEstadoFinancieroArchivo", typeof(string));
                table.Columns.Add("NombreEstadoFinanciero", typeof(string));
                table.Columns.Add("DescripcionUnidadTiempoPeriodo", typeof(string));
                table.Columns.Add("NombreArchivo", typeof(string));
                table.Columns.Add("TipoArchivo", typeof(string));
                table.Columns.Add("TamanoByteArchivo", typeof(string));
                table.Columns.Add("FechaArchivoCarga", typeof(string));
                table.Columns.Add("FechaPresentacion", typeof(string));

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
        }// Fin GetConsultarEstadosFinancierosCargadosVacio

        /**/



        /// <summary>
        /// Método: ConsultarArchivosAnexosEstadosFinancierosCargados_GridView
        /// Descripción:   Carga el GridView con el DataSet que retorna GetDataSetGridView_ConsultarArchivosAnexosEstadosFinancierosCargados
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <returns></returns>
        public Boolean ConsultarArchivosAnexosEstadosFinancierosCargados_GridView()
        {
            try
            {
                EstadosFinancierosArchivosAnexosGridView.DataSource = GetDataSetGridView_ConsultarArchivosAnexosEstadosFinancierosCargados();
                EstadosFinancierosArchivosAnexosGridView.DataBind();

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
        } // Fin ConsultarArchivosAnexosEstadosFinancierosCargados_GridView  

        /// <summary>
        /// Método: GetDataSetGridView_ConsultarArchivosAnexosEstadosFinancierosCargados
        /// Descripción:   Devuelve un DataSet de la consulta de los archivos anexos de los Estados Financieros Cargados
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <returns></returns>
        public DataSet GetDataSetGridView_ConsultarArchivosAnexosEstadosFinancierosCargados()
        {
            string strAuxCod = "", strAuxDes = "";
            DataSet dsNICSPDatosGridView = new DataSet();
            wsPlantillasConsolidacion WSINICSP_PC = new wsPlantillasConsolidacion();
            try
            {
                string strUsuario, strIdEntidad, strPeriodo, strUnidadTiempoPeriodo, Resultado, Mensaje; ;

                strUsuario = gstr_Usuario;
                strIdEntidad = CatalogoEntidadesDropDownList.SelectedValue;
                strPeriodo = PeriodoDropDownList.SelectedItem.Text;
                strUnidadTiempoPeriodo = UnidadTiempoPeriodoDropDownList.SelectedValue;

                if (strPeriodo.Equals(string.Empty))
                    strPeriodo = DateTime.Now.Year.ToString();

                if ((!strIdEntidad.Equals(string.Empty)) && (!strPeriodo.Equals("Seleccione ítem")) && (!strUnidadTiempoPeriodo.Equals(string.Empty)))
                {
                    dsNICSPDatosGridView = WSINICSP_PC.ConsultarArchivosAnexosEstadosFinancierosCargados(strIdEntidad, int.Parse(strPeriodo), strUnidadTiempoPeriodo, "", strUsuario, out Resultado, out Mensaje);

                    //if (dsNICSPDatosGridView.Tables["Result"].Rows[0]["status"].ToString() == "00")
                    //{
                    if (dsNICSPDatosGridView.Tables.Count > 0)//Si hizo la consulta pero No retorno nada
                    //if (!dsNICSPDatosGridView.Tables["Table"].Rows.Count.Equals(0))//Si hizo la consulta pero No retorno nada
                    {
                        for (int i = 0; i <= dsNICSPDatosGridView.Tables["Table"].Rows.Count - 1; i++)
                        {
                            strAuxCod = dsNICSPDatosGridView.Tables["Table"].Rows[i]["IdEstadoFinanciero"].ToString();
                            strAuxDes = dsNICSPDatosGridView.Tables["Table"].Rows[i]["NombreEstadoFinanciero"].ToString();
                            if ((!strAuxCod.Equals("")) && (!strAuxDes.Equals("")))
                                dsNICSPDatosGridView.Tables["Table"].Rows[i]["NombreEstadoFinanciero"] = strAuxCod + "-" + strAuxDes;

                            if (strAuxCod.Equals(ConfigurationManager.AppSettings["CodigoEstadosFinancieros_CorreoAutorizacion"].ToString())) // se borra porque para los correos de autorizacion se tiene un view por separado en la interface
                                dsNICSPDatosGridView.Tables["Table"].Rows[i].Delete();
                            else
                            {

                                strAuxCod = "";
                                strAuxDes = "";

                                strAuxCod = dsNICSPDatosGridView.Tables["Table"].Rows[i]["UnidadTiempoPeriodo"].ToString();
                                strAuxDes = dsNICSPDatosGridView.Tables["Table"].Rows[i]["DescripcionUnidadTiempoPeriodo"].ToString();
                                if ((!strAuxCod.Equals("")) && (!strAuxDes.Equals("")))
                                    dsNICSPDatosGridView.Tables["Table"].Rows[i]["DescripcionUnidadTiempoPeriodo"] = strAuxCod + "-" + strAuxDes;
                            }
                            strAuxCod = "";
                            strAuxDes = "";
                        }
                    }
                    else
                    {
                        dsNICSPDatosGridView = GetDataSetGridView_ConsultarArchivosAnexosEstadosFinancierosCargados_Vacio();
                    }
                }
                else
                {
                    DesplegarError(true);
                    txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorParametros"].ToString();
                    dsNICSPDatosGridView = GetDataSetGridView_ConsultarArchivosAnexosEstadosFinancierosCargados_Vacio();
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
        } // Fin GetDataSetGridView_ConsultarArchivosAnexosEstadosFinancierosCargados 

        /// <summary>
        /// Método: GetDataSetGridView_ConsultarArchivosAnexosEstadosFinancierosCargados_Vacio
        /// Descripción:   Devuelve un DataSet Vacio.
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <returns></returns>
        public DataSet GetDataSetGridView_ConsultarArchivosAnexosEstadosFinancierosCargados_Vacio()
        {
            DataSet dsNICSPDatosGridView = new DataSet();
            try
            {
                dsNICSPDatosGridView.Tables.Add(GetConsultarArchivosAnexosEstadosFinancierosCargadosVacio());

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
        } // Fin GetDataSetGridView_ConsultarArchivosAnexosEstadosFinancierosCargados_Vacio

        /// <summary>
        /// Método: GetConsultarArchivosAnexosEstadosFinancierosCargadosVacio
        /// Descripción:   .
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <returns></returns>
        public DataTable GetConsultarArchivosAnexosEstadosFinancierosCargadosVacio()
        {
            DataTable table = new DataTable();
            try
            {
                table.Columns.Add("IdEntidad", typeof(string));
                table.Columns.Add("IdEstadoFinanciero", typeof(string));
                table.Columns.Add("Periodo", typeof(string));
                table.Columns.Add("UnidadTiempoPeriodo", typeof(string));
                table.Columns.Add("IdEstadoFinancieroArchivoAnexo", typeof(string));
                table.Columns.Add("NombreEstadoFinanciero", typeof(string));
                table.Columns.Add("DescripcionUnidadTiempoPeriodo", typeof(string));
                table.Columns.Add("NombreArchivo", typeof(string));
                table.Columns.Add("TipoArchivo", typeof(string));
                table.Columns.Add("TamanoByteArchivo", typeof(string));
                table.Columns.Add("FechaArchivoCarga", typeof(string));
                table.Columns.Add("FechaPresentacion", typeof(string));

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
        }// Fin GetConsultarEstadosArchivosAnexosFinancierosCargadosVacio



        /**/

        /// <summary>
        /// Método: ConsultarArchivosPlantillasEstadosFinancierosCargados_GridView
        /// Descripción:   Carga el GridView con el DataSet que retorna GetDataSetGridView_ConsultarArchivosPlantillasEstadosFinancierosCargados
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <returns></returns>
        public Boolean ConsultarArchivosPlantillasEstadosFinancierosCargados_GridView()
        {
            try
            {
                EstadosFinancierosArchivosPlantillasGridView.DataSource = GetDataSetGridView_ConsultarArchivosPlantillasEstadosFinancierosCargados();
                EstadosFinancierosArchivosPlantillasGridView.DataBind();

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
        } // Fin ConsultarArchivosPlantillasEstadosFinancierosCargados_GridView  

        /// <summary>
        /// Método: GetDataSetGridView_ConsultarArchivosPlantillasEstadosFinancierosCargados
        /// Descripción:   Devuelve un DataSet de la consulta de los Archivos Plantillas de los Estados Financieros Cargados
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <returns></returns>
        public DataSet GetDataSetGridView_ConsultarArchivosPlantillasEstadosFinancierosCargados()
        {
            string strAuxCod = "", strAuxDes = "";
            DataSet dsNICSPDatosGridView = new DataSet();
            wsPlantillasConsolidacion WSINICSP_PC = new wsPlantillasConsolidacion();
            try
            {
                string strUsuario, strIdEntidad, strPeriodo, strUnidadTiempoPeriodo;
                string Resultado, Mensaje;

                strUsuario = gstr_Usuario;
                strIdEntidad = CatalogoEntidadesDropDownList.SelectedValue;
                strPeriodo = PeriodoDropDownList.SelectedItem.Text;
                strUnidadTiempoPeriodo = UnidadTiempoPeriodoDropDownList.SelectedValue;

                if (strPeriodo.Equals(string.Empty))
                    strPeriodo = DateTime.Now.Year.ToString();

                //if ((!strIdEntidad.Equals(string.Empty)) && (!strPeriodo.Equals("Seleccione ítem")) && (!strUnidadTiempoPeriodo.Equals(string.Empty)))
                //{

                dsNICSPDatosGridView = WSINICSP_PC.ConsultarArchivosPlantillasEstadosFinancierosCargados("", strUsuario, out Resultado, out Mensaje);

                //if (dsNICSPDatosGridView.Tables["Result"].Rows[0]["status"].ToString() == "00")
                //{
                if (dsNICSPDatosGridView.Tables.Count > 0)//Si hizo la consulta pero No retorno nada
                //if (!dsNICSPDatosGridView.Tables["Table"].Rows.Count.Equals(0))//Si hizo la consulta pero No retorno nada
                {
                    for (int i = 0; i <= dsNICSPDatosGridView.Tables["Table"].Rows.Count - 1; i++)
                    {
                        strAuxCod = dsNICSPDatosGridView.Tables["Table"].Rows[i]["IdEstadoFinanciero"].ToString();
                        strAuxDes = dsNICSPDatosGridView.Tables["Table"].Rows[i]["NombreEstadoFinanciero"].ToString();
                        dsNICSPDatosGridView.Tables["Table"].Rows[i]["NombreEstadoFinanciero"] = strAuxCod + "-" + strAuxDes;
                    }
                }
                else
                {
                    dsNICSPDatosGridView = GetDataSetGridView_ConsultarArchivosPlantillasEstadosFinancierosCargados_Vacio();
                }
                //}
                //else
                //{
                //    DesplegarError(true);
                //    txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorParametros"].ToString());
                //    dsNICSPDatosGridView = GetDataSetGridView_ConsultarArchivosPlantillasEstadosFinancierosCargados_Vacio();
                //}

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
        } // Fin GetDataSetGridView_ConsultarArchivosPlantillasEstadosFinancierosCargados 

        /// <summary>
        /// Método: GetDataSetGridView_ConsultarArchivosPlantillasEstadosFinancierosCargados_Vacio
        /// Descripción:   Devuelve un DataSet Vacio.
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <returns></returns>
        public DataSet GetDataSetGridView_ConsultarArchivosPlantillasEstadosFinancierosCargados_Vacio()
        {
            DataSet dsNICSPDatosGridView = new DataSet();
            try
            {
                dsNICSPDatosGridView.Tables.Add(GetConsultarArchivosPlantillasEstadosFinancierosCargadosVacio());

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
        } // Fin GetDataSetGridView_ConsultarArchivosPlantillasEstadosFinancierosCargados_Vacio

        /// <summary>
        /// Método: GetConsultarArchivosPlantillasEstadosFinancierosCargadosVacio
        /// Descripción:   .
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <returns></returns>
        public DataTable GetConsultarArchivosPlantillasEstadosFinancierosCargadosVacio()
        {
            DataTable table = new DataTable();
            try
            {
                table.Columns.Add("IdEstadoFinanciero", typeof(string));
                table.Columns.Add("NombreEstadoFinanciero", typeof(string));
                table.Columns.Add("IdEstadoFinancieroArchivoPlantilla", typeof(string));
                table.Columns.Add("NombreArchivo", typeof(string));
                table.Columns.Add("TipoArchivo", typeof(string));
                table.Columns.Add("FechaArchivoCarga", typeof(string));

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
        }// Fin GetConsultarArchivosPlantillasEstadosFinancierosCargadosVacio





        /**/

        /// <summary>
        /// Método: ConsultarCorreosAutorizacionEstadosFinancierosCargados_GridView
        /// Descripción:   Carga el GridView con el DataSet que retorna GetDataSetGridView_ConsultarCorreosAutorizacionEstadosFinancierosCargados
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <returns></returns>
        public Boolean ConsultarCorreosAutorizacionEstadosFinancierosCargados_GridView()
        {
            try
            {
                CorreosAutorizacionAnexosGridView.DataSource = GetDataSetGridView_ConsultarCorreosAutorizacionEstadosFinancierosCargados();
                CorreosAutorizacionAnexosGridView.DataBind();

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
        } // Fin ConsultarCorreosAutorizacionEstadosFinancierosCargados_GridView  

        /// <summary>
        /// Método: GetDataSetGridView_ConsultarCorreosAutorizacionEstadosFinancierosCargados
        /// Descripción:   Devuelve un DataSet de la consulta de los archivos anexos de los Estados Financieros Cargados
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <returns></returns>
        public DataSet GetDataSetGridView_ConsultarCorreosAutorizacionEstadosFinancierosCargados()
        {
            string strAuxCod = "", strAuxDes = "";
            DataSet dsNICSPDatosGridView = new DataSet();
            wsPlantillasConsolidacion WSINICSP_PC = new wsPlantillasConsolidacion();
            try
            {
                string strUsuario, strIdEntidad, strPeriodo, strUnidadTiempoPeriodo, Resultado, Mensaje;

                strUsuario = gstr_Usuario;
                strIdEntidad = CatalogoEntidadesDropDownList.SelectedValue;
                strPeriodo = PeriodoDropDownList.SelectedItem.Text;
                strUnidadTiempoPeriodo = UnidadTiempoPeriodoDropDownList.SelectedValue;

                if (strPeriodo.Equals(string.Empty))
                    strPeriodo = DateTime.Now.Year.ToString();

                if ((!strIdEntidad.Equals(string.Empty)) && (!strPeriodo.Equals("Seleccione ítem")) && (!strUnidadTiempoPeriodo.Equals(string.Empty)))
                {
                    dsNICSPDatosGridView = WSINICSP_PC.ConsultarCorreosAutorizacionEstadosFinancierosCargados(strIdEntidad, int.Parse(strPeriodo), strUnidadTiempoPeriodo, "", strUsuario, out Resultado, out Mensaje);
                    //dsNICSPDatosGridView = WSINICSP_PC.ConsultarCorreosAutorizacionEstadosFinancierosCargados();

                    //if (dsNICSPDatosGridView.Tables["Result"].Rows[0]["status"].ToString() == "00")
                    //{
                    if (dsNICSPDatosGridView.Tables.Count > 0)//Si hizo la consulta pero No retorno nada
                    //if (!dsNICSPDatosGridView.Tables["Table"].Rows.Count.Equals(0))//Si hizo la consulta pero No retorno nada
                    {
                        for (int i = 0; i <= dsNICSPDatosGridView.Tables["Table"].Rows.Count - 1; i++)
                        {
                            strAuxCod = dsNICSPDatosGridView.Tables["Table"].Rows[i]["IdEstadoFinanciero"].ToString();
                            strAuxDes = dsNICSPDatosGridView.Tables["Table"].Rows[i]["NombreEstadoFinanciero"].ToString();
                            if ((!strAuxCod.Equals("")) && (!strAuxDes.Equals("")))
                                dsNICSPDatosGridView.Tables["Table"].Rows[i]["NombreEstadoFinanciero"] = strAuxCod + "-" + strAuxDes;

                            if (!strAuxCod.Equals(ConfigurationManager.AppSettings["CodigoEstadosFinancieros_CorreoAutorizacion"].ToString())) // se borra porque para los correos de autorizacion se tiene un view por separado en la interface
                                dsNICSPDatosGridView.Tables["Table"].Rows[i].Delete();
                            else
                            {

                                strAuxCod = "";
                                strAuxDes = "";

                                strAuxCod = dsNICSPDatosGridView.Tables["Table"].Rows[i]["UnidadTiempoPeriodo"].ToString();
                                strAuxDes = dsNICSPDatosGridView.Tables["Table"].Rows[i]["DescripcionUnidadTiempoPeriodo"].ToString();
                                if ((!strAuxCod.Equals("")) && (!strAuxDes.Equals("")))
                                    dsNICSPDatosGridView.Tables["Table"].Rows[i]["DescripcionUnidadTiempoPeriodo"] = strAuxCod + "-" + strAuxDes;
                            }
                            strAuxCod = "";
                            strAuxDes = "";
                        }
                    }
                    else
                    {
                        dsNICSPDatosGridView = GetDataSetGridView_ConsultarCorreosAutorizacionEstadosFinancierosCargados_Vacio();
                    }

                }
                else
                {
                    DesplegarError(true);
                    txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorParametros"].ToString();
                    dsNICSPDatosGridView = GetDataSetGridView_ConsultarCorreosAutorizacionEstadosFinancierosCargados_Vacio();
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
        } // Fin GetDataSetGridView_ConsultarCorreosAutorizacionEstadosFinancierosCargados 

        /// <summary>
        /// Método: GetDataSetGridView_ConsultarCorreosAutorizacionEstadosFinancierosCargados_Vacio
        /// Descripción:   Devuelve un DataSet Vacio.
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <returns></returns>
        public DataSet GetDataSetGridView_ConsultarCorreosAutorizacionEstadosFinancierosCargados_Vacio()
        {
            DataSet dsNICSPDatosGridView = new DataSet();
            try
            {
                dsNICSPDatosGridView.Tables.Add(GetConsultarCorreosAutorizacionEstadosFinancierosCargadosVacio());

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
        } // Fin GetDataSetGridView_ConsultarCorreosAutorizacionEstadosFinancierosCargados_Vacio

        /// <summary>
        /// Método: GetConsultarCorreosAutorizacionEstadosFinancierosCargadosVacio
        /// Descripción:   .
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <returns></returns>
        public DataTable GetConsultarCorreosAutorizacionEstadosFinancierosCargadosVacio()
        {
            DataTable table = new DataTable();
            try
            {
                table.Columns.Add("IdEntidad", typeof(string));
                table.Columns.Add("IdEstadoFinanciero", typeof(string));
                table.Columns.Add("Periodo", typeof(string));
                table.Columns.Add("UnidadTiempoPeriodo", typeof(string));
                table.Columns.Add("IdEstadoFinancieroArchivoAnexo", typeof(string));
                table.Columns.Add("NombreEstadoFinanciero", typeof(string));
                table.Columns.Add("DescripcionUnidadTiempoPeriodo", typeof(string));
                table.Columns.Add("NombreArchivo", typeof(string));
                table.Columns.Add("TipoArchivo", typeof(string));
                table.Columns.Add("TamanoByteArchivo", typeof(string));
                table.Columns.Add("FechaArchivoCarga", typeof(string));

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
        }// Fin GetConsultarCorreosAutorizacionEstadosFinancierosCargadosVacio





        /**/
        public bool ValidarPermitirAccion(int Rol, int Accion)
        {
            return true;
        }

        ///// <summary>
        ///// Método: EstadosFinancierosArchivosGridView_RowDeleting
        ///// Descripción:   
        ///// </summary>
        ///// <remarks>
        ///// Historia de cambios:
        ///// Fecha        Persona         Motivo
        ///// </remarks>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void EstadosFinancierosArchivosGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    try
        //    {
        //        string strIdEntidad, strIdEstadoFinanciero, strPeriodo, strUnidadTiempoPeriodo, strIdEstadoFinancieroArchivo;

        //        strIdEntidad = ((Label)EstadosFinancierosArchivosGridView.Rows[e.RowIndex].FindControl("CodigoIdEntidadLabel")).Text;
        //        strIdEstadoFinanciero = ((Label)EstadosFinancierosArchivosGridView.Rows[e.RowIndex].FindControl("CodigoIdEstadoFinancieroLabel")).Text;
        //        strPeriodo = ((Label)EstadosFinancierosArchivosGridView.Rows[e.RowIndex].FindControl("CodigoPeriodoLabel")).Text;
        //        strUnidadTiempoPeriodo = ((Label)EstadosFinancierosArchivosGridView.Rows[e.RowIndex].FindControl("CodigoUnidadTiempoPeriodoLabel")).Text;

        //        strIdEstadoFinancieroArchivo = ((Label)EstadosFinancierosArchivosGridView.Rows[e.RowIndex].FindControl("CodigoIdEstadoFinancieroArchivoLabel")).Text;

        //        if (DesplegarError(!Eliminar_ArchivoEstadoFinanciero(strIdEstadoFinancieroArchivo)))
        //        {
        //            txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorBorrarArchivo"].ToString());
        //        }
        //        else
        //        {
        //            if (DesplegarError(!Eliminar_EstadoFinanciero(strIdEstadoFinanciero, strIdEntidad, int.Parse(strPeriodo), strUnidadTiempoPeriodo)))
        //            {
        //                txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorBorrarArchivo"].ToString());
        //            }
        //        }

        //        EstadosFinancierosArchivosGridView.EditIndex = -1;
        //        //Session["Buscar"] = false;
        //        ConsultarEstadosFinancierosCargados_GridView();
        //    }
        //    catch (Exception ex)
        //    {
        //        #region MensajeError
        //        EventLog.WriteEntry(ConfigurationManager.AppSettings["EventLogSource"].ToString(),
        //            //Obtiene el nombre de la clase.
        //        "NICSP"
        //            //Nombre del método.
        //        + "." + MethodInfo.GetCurrentMethod().Name
        //            //Error especifico.
        //        + ": Excepcion  " + ex.Message.ToString() + ". ",
        //        EventLogEntryType.Error);

        //        txtError.Text = txtError.Text + "\r\n" + "NICSP Excepcion:  " + ex.Message.ToString() + ". ");
        //        DesplegarError(true);
        //        #endregion
        //    }
        //    finally
        //    {
        //    }
        //} // Fin EstadosFinancierosArchivosGridView_RowDeleting
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str_Ruta"></param>
        /// <param name="str_NbrArchivo"></param>
        /// <param name="str_Mensaje"></param>
        /// <returns></returns>
        protected Boolean Eliminar_Archivo(string str_Ruta, string str_NbrArchivo, out string str_Mensaje)
        {
            Boolean bln_Resultado = false;
            wsPlantillasConsolidacion WSINICSP_PC = new wsPlantillasConsolidacion();
            string[] str_Result = new string[2];
            try
            {
                str_Result = WSINICSP_PC.uwsEliminarArchivo(str_Ruta, str_NbrArchivo);
            }
            catch (Exception ex)
            {
                str_Result[0] = "99";
                str_Result[1] = ex.ToString();
            }
            bln_Resultado = (str_Result[0] == "00") ? true : false;
            str_Mensaje = str_Result[1];

            return bln_Resultado;


        }
        /// <summary>
        /// Método: Eliminar_ArchivoEstadoFinanciero
        /// Descripción:   Elimina la información de del Archivo Estado Financiero en la DB con respecto al parámetro suministrado.

        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <param name="TipoIDCuenta"></param>
        /// <param name="TipoMoneda"></param>
        /// <returns></returns>
        protected Boolean Eliminar_ArchivoEstadoFinanciero(string strIdEstadoFinancieroArchivo)
        {
            Boolean boolEliminado = false;
            string Resultado, Mensaje;

            //DataSet dsNICSPDatos = new DataSet();
            wsPlantillasConsolidacion WSINICSP_PC = new wsPlantillasConsolidacion();
            try
            {
                //dsNICSPDatos = WSINICSP_PC.EliminarArchivoEstadoFinanciero(strIdEstadoFinancieroArchivo, "", gstr_Usuario);

                //if (dsNICSPDatos.Tables["Result"].Rows[0]["status"].ToString() != "00")
                //{
                //    //Error
                //    txtError.Text = txtError.Text + "\r\n" +  dsNICSPDatos.Tables["Result"].Rows[0]["message"].ToString();
                //    boolError = true;
                //}
                //else
                //{
                //    boolError = false;
                //}

                boolEliminado = WSINICSP_PC.EliminarArchivoEstadoFinanciero(strIdEstadoFinancieroArchivo, "", gstr_Usuario, out Resultado, out Mensaje);

                return boolEliminado;
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
                //dsNICSPDatos = null;
                WSINICSP_PC = null;
            }
        }// Fin Eliminar_ArchivoEstadoFinanciero

        ///// </summary>
        ///// <remarks>
        ///// Historia de cambios:
        ///// Fecha        Persona         Motivo
        ///// </remarks>
        ///// <returns></returns>
        //protected Boolean Eliminar_EstadoFinanciero(string IdEstadoFinanciero, string IdEntidad, int Periodo, string UnidadTiempoPeriodo)
        //{
        //    Boolean boolEliminado = false;
        //    string Resultado, Mensaje;

        //    //DataSet dsNICSPDatos = new DataSet();
        //    wsPlantillasConsolidacion WSINICSP_PC = new wsPlantillasConsolidacion();
        //    try
        //    {
        //        //dsNICSPDatos = WSINICSP_PC.EliminarArchivoEstadoFinanciero(strIdEstadoFinancieroArchivo, "", gstr_Usuario);

        //        //if (dsNICSPDatos.Tables["Result"].Rows[0]["status"].ToString() != "00")
        //        //{
        //        //    //Error
        //        //    txtError.Text = txtError.Text + "\r\n" +  dsNICSPDatos.Tables["Result"].Rows[0]["message"].ToString();
        //        //    boolError = true;
        //        //}
        //        //else
        //        //{
        //        //    boolError = false;
        //        //}

        //        string CodigoEstadosFinancieros_EstadoFlujoEfectivo, CodigoEstadosFinancieros_EstadoCambioPatrimonioNeto, CodigoEstadosFinancieros_EstadoBalanceComprobacion;

        //        CodigoEstadosFinancieros_EstadoFlujoEfectivo = ConfigurationManager.AppSettings["CodigoEstadosFinancieros_EstadoFlujoEfectivo"].ToString();
        //        CodigoEstadosFinancieros_EstadoCambioPatrimonioNeto = ConfigurationManager.AppSettings["CodigoEstadosFinancieros_EstadoCambioPatrimonioNeto"].ToString();
        //        CodigoEstadosFinancieros_EstadoBalanceComprobacion = ConfigurationManager.AppSettings["CodigoEstadosFinancieros_EstadoBalanceComprobacion"].ToString();

        //        if (CodigoEstadosFinancieros_EstadoFlujoEfectivo.Equals(IdEstadoFinanciero.ToString()))
        //        {
        //            boolEliminado = WSINICSP_PC.EliminarEstadoFinancieroFlujoEfectivo(byte.Parse(IdEstadoFinanciero), IdEntidad, Periodo, UnidadTiempoPeriodo, "", gstr_Usuario, out Resultado, out Mensaje);
        //        }
        //        else
        //            if (CodigoEstadosFinancieros_EstadoCambioPatrimonioNeto.Equals(IdEstadoFinanciero.ToString()))
        //            {
        //                boolEliminado = WSINICSP_PC.EliminarEstadoFinancieroCambioPatrimonioNeto(byte.Parse(IdEstadoFinanciero), IdEntidad, Periodo, UnidadTiempoPeriodo, "", gstr_Usuario, out Resultado, out Mensaje);
        //            }
        //            else
        //                if (CodigoEstadosFinancieros_EstadoBalanceComprobacion.Equals(IdEstadoFinanciero.ToString()))
        //                {
        //                    boolEliminado = WSINICSP_PC.EliminarEstadoFinancieroBalanceComprobacion(byte.Parse(IdEstadoFinanciero), IdEntidad, Periodo, UnidadTiempoPeriodo, "", gstr_Usuario, out Resultado, out Mensaje);
        //                }
        //                else
        //                {
        //                boolEliminado = true;
        //                }

        //        return boolEliminado;
        //    }
        //    catch (Exception ex)
        //    {
        //        #region MensajeError
        //        EventLog.WriteEntry(ConfigurationManager.AppSettings["EventLogSource"].ToString(),
        //            //Obtiene el nombre de la clase.
        //        "NICSP"
        //            //Nombre del método.
        //        + "." + MethodInfo.GetCurrentMethod().Name
        //            //Error especifico.
        //        + ": Excepcion  " + ex.Message.ToString() + ". ",
        //        EventLogEntryType.Error);
        //        #endregion

        //        throw (ex);
        //    }
        //    finally
        //    {
        //        //dsNICSPDatos = null;
        //        WSINICSP_PC = null;
        //    }
        //}// Fin Eliminar_ArchivoEstadoFinanciero

        /**/

        ///// <summary>
        ///// Método: EstadosFinancierosArchivosAnexosGridView_RowDeleting
        ///// Descripción:   
        ///// </summary>
        ///// <remarks>
        ///// Historia de cambios:
        ///// Fecha        Persona         Motivo
        ///// </remarks>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void EstadosFinancierosArchivosAnexosGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    try
        //    {
        //        string strIdEstadoFinancieroArchivosAnexos;

        //        strIdEstadoFinancieroArchivosAnexos = ((Label)EstadosFinancierosArchivosAnexosGridView.Rows[e.RowIndex].FindControl("CodigoIdEstadoFinancieroArchivoAnexoLabel")).Text;

        //        DesplegarError(!Eliminar_ArchivosAnexosEstadoFinanciero(strIdEstadoFinancieroArchivosAnexos));

        //        EstadosFinancierosArchivosAnexosGridView.EditIndex = -1;

        //        //ConsultarEstadosFinancierosCargados_GridView();
        //        ConsultarArchivosAnexosEstadosFinancierosCargados_GridView();
        //    }
        //    catch (Exception ex)
        //    {
        //        #region MensajeError
        //        EventLog.WriteEntry(ConfigurationManager.AppSettings["EventLogSource"].ToString(),
        //            //Obtiene el nombre de la clase.
        //        "NICSP"
        //            //Nombre del método.
        //        + "." + MethodInfo.GetCurrentMethod().Name
        //            //Error especifico.
        //        + ": Excepcion  " + ex.Message.ToString() + ". ",
        //        EventLogEntryType.Error);

        //        txtError.Text = txtError.Text + "\r\n" + "NICSP Excepcion:  " + ex.Message.ToString() + ". ");
        //        DesplegarError(true);
        //        #endregion
        //    }
        //    finally
        //    {

        //    }
        //} // Fin EstadosFinancierosArchivosAnexosGridView_RowDeleting

        ///// <summary>
        ///// Método: Eliminar_ArchivoPlantillasEstadoFinanciero
        ///// Descripción:   Elimina la información de del Archivo Estado Financiero en la DB con respecto al parámetro suministrado.

        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <param name="TipoIDCuenta"></param>
        /// <param name="TipoMoneda"></param>
        /// <returns></returns>
        protected Boolean Eliminar_ArchivosAnexosEstadoFinanciero(string strIdEstadoFinancieroArchivoPlantilla)
        {
            //Boolean boolError = true;

            //DataSet dsNICSPDatos = new DataSet();
            wsPlantillasConsolidacion WSINICSP_PC = new wsPlantillasConsolidacion();
            string Resultado, Mensaje;
            try
            {
                return WSINICSP_PC.EliminarArchivoAnexoEstadoFinanciero(strIdEstadoFinancieroArchivoPlantilla, "", gstr_Usuario, out Resultado, out Mensaje);
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
                //dsNICSPDatos = null;
                WSINICSP_PC = null;
            }
        }// Fin Eliminar_ArchivosAnexosEstadoFinanciero

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EstadosFinancierosArchivosAnexosGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            wsPlantillasConsolidacion WSINICSP_PC = new wsPlantillasConsolidacion();
            DataSet dsNICSPDatos = new DataSet();
            try
            {
                string Resultado, Mensaje;

                if (e.CommandName == "Select")
                {
                    string strIdEstadoFinancieroArchivosAnexos, strFileName, strFileType;

                    int index = Convert.ToInt32(e.CommandArgument);
                    strIdEstadoFinancieroArchivosAnexos = ((Label)EstadosFinancierosArchivosAnexosGridView.Rows[index].FindControl("CodigoIdEstadoFinancieroArchivoAnexoLabel")).Text;
                    strFileName = ((Label)EstadosFinancierosArchivosAnexosGridView.Rows[index].FindControl("ItemNombreArchivoLabel")).Text;
                    strFileType = ((Label)EstadosFinancierosArchivosAnexosGridView.Rows[index].FindControl("ItemTipoArchivoLabel")).Text;

                    if ((!strIdEstadoFinancieroArchivosAnexos.Equals(string.Empty)))
                    {

                        dsNICSPDatos = WSINICSP_PC.BuscarArchivoAnexoEstadoFinanciero(strIdEstadoFinancieroArchivosAnexos, "", gstr_Usuario, out Resultado, out Mensaje);
                        byte[] buffer = new byte[(int)0];

                        if (dsNICSPDatos.Tables.Count > 0)//Si hizo la consulta pero No retorno nada
                        //if (!dsNICSPDatosGridView.Tables["Table"].Rows.Count.Equals(0))//Si hizo la consulta pero No retorno nada
                        {
                            for (int i = 0; i <= dsNICSPDatos.Tables["Table1"].Rows.Count - 1; i++)
                            {
                                buffer = (byte[])dsNICSPDatos.Tables["Table1"].Rows[i]["Buffer"];
                            }


                            //byte[] buffer = new byte[(int)objSqlFileStream.Length];
                            //byte[] buffer = new byte[(int)WSINICSP_PC.BuscarArchivoPlantillaEstadoFinanciero(strIdEstadoFinancieroArchivo, "", gstr_Usuario)];

                            Response.Clear();
                            Response.Buffer = true;
                            Response.Charset = "";
                            Response.Cache.SetCacheability(HttpCacheability.NoCache);
                            Response.ContentType = strFileType.Substring(1);
                            Response.AppendHeader("Content-Disposition", "attachment; filename=" + strFileName + strFileType);
                            Response.BinaryWrite(buffer);
                            Response.Flush();
                            Response.End();
                        }
                        else
                        {
                            DesplegarError(true);
                            txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorRetornoNulo"].ToString();
                        }

                    }
                    else
                    {
                        DesplegarError(true);
                        txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorParametros"].ToString();
                    }

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

                txtError.Text = txtError.Text + "\r\n" + "NICSP Excepcion:  " + ex.Message.ToString() + ". ";
                DesplegarError(true);
                #endregion
            }
            finally
            {
                WSINICSP_PC = null;
                dsNICSPDatos = null;
            }
        }

        /// <summary>
        /// Método: EstadosFinancierosArchivosAnexosGridView_PageIndexChanging
        /// Descripción:   Despliega en el GridView el nuevo índex solicitado.
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EstadosFinancierosArchivosAnexosGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {

                DataSet DataSet;
                DataTable dataTable;

                //DataSet = GetDataSetGridView_ConsultarArchivosPlantillasEstadosFinancierosCargados();
                DataSet = GetDataSetGridView_ConsultarArchivosAnexosEstadosFinancierosCargados();
                dataTable = DataSet.Tables[0];


                if (dataTable != null)
                {
                    DataView dataView = new DataView(dataTable);
                    EstadosFinancierosArchivosAnexosGridView.DataSource = dataView;
                    EstadosFinancierosArchivosAnexosGridView.PageIndex = e.NewPageIndex;
                    EstadosFinancierosArchivosAnexosGridView.DataBind();
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

                txtError.Text = txtError.Text + "\r\n" + "NICSP Excepcion:  " + ex.Message.ToString() + ". ";
                DesplegarError(true);
                #endregion
            }
            finally
            {

            }
        }// Fin EstadosFinancierosArchivosAnexosGridView_PageIndexChanging

        /**/

        /*...*/

        /// <summary>
        /// Método: EstadosFinancierosCorreosAutorizacionGridView_RowDeleting
        /// Descripción:   
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EstadosFinancierosCorreosAutorizacionGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                if (EtadaActual.Equals("2"))
                {
                    string strIdEstadoFinancieroArchivosAnexos;

                    strIdEstadoFinancieroArchivosAnexos = ((Label)CorreosAutorizacionAnexosGridView.Rows[e.RowIndex].FindControl("CodigoIdEstadoFinancieroArchivoAnexoLabel")).Text;

                    DesplegarError(!Eliminar_ArchivosAnexosEstadoFinanciero(strIdEstadoFinancieroArchivosAnexos));

                    CorreosAutorizacionAnexosGridView.EditIndex = -1;

                    //ConsultarEstadosFinancierosCargados_GridView();
                    ConsultarCorreosAutorizacionEstadosFinancierosCargados_GridView();
                }
                else
                {
                    DesplegarError(true);
                    txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorBorrarArchivoEtapaNovalida"].ToString();
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

                txtError.Text = txtError.Text + "\r\n" + "NICSP Excepcion:  " + ex.Message.ToString() + ". ";
                DesplegarError(true);
                #endregion
            }
            finally
            {

            }
        } // Fin EstadosFinancierosArchivosAnexosGridView_RowDeleting

        /// <summary>
        /// Método: Eliminar_ArchivoPlantillasEstadoFinanciero
        /// Descripción:   Elimina la información de del Archivo Estado Financiero en la DB con respecto al parámetro suministrado.


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EstadosFinancierosCorreosAutorizacionGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            wsPlantillasConsolidacion WSINICSP_PC = new wsPlantillasConsolidacion();
            DataSet dsNICSPDatos = new DataSet();
            try
            {
                string Resultado, Mensaje;

                if (e.CommandName == "Select")
                {
                    string strIdEstadoFinancieroArchivoAnexo, strFileName, strFileType;

                    int index = Convert.ToInt32(e.CommandArgument);
                    strIdEstadoFinancieroArchivoAnexo = ((Label)CorreosAutorizacionAnexosGridView.Rows[index].FindControl("CodigoIdEstadoFinancieroArchivoAnexoLabel")).Text;
                    strFileName = ((Label)CorreosAutorizacionAnexosGridView.Rows[index].FindControl("ItemNombreArchivoLabel")).Text;
                    strFileType = ((Label)CorreosAutorizacionAnexosGridView.Rows[index].FindControl("ItemTipoArchivoLabel")).Text;

                    if ((!strIdEstadoFinancieroArchivoAnexo.Equals(string.Empty)))
                    {

                        // los Correos de Autorizacion estan en la tabla de anexos con el cod 12 de estado financiero.
                        dsNICSPDatos = WSINICSP_PC.BuscarArchivoAnexoEstadoFinanciero(strIdEstadoFinancieroArchivoAnexo, "", gstr_Usuario, out Resultado, out Mensaje);
                        byte[] buffer = new byte[(int)0];

                        if (dsNICSPDatos.Tables.Count > 0)//Si hizo la consulta pero No retorno nada
                        //if (!dsNICSPDatosGridView.Tables["Table"].Rows.Count.Equals(0))//Si hizo la consulta pero No retorno nada
                        {
                            for (int i = 0; i <= dsNICSPDatos.Tables["Table1"].Rows.Count - 1; i++)
                            {
                                buffer = (byte[])dsNICSPDatos.Tables["Table1"].Rows[i]["Buffer"];
                            }


                            //byte[] buffer = new byte[(int)objSqlFileStream.Length];
                            //byte[] buffer = new byte[(int)WSINICSP_PC.BuscarArchivoPlantillaEstadoFinanciero(strIdEstadoFinancieroArchivo, "", gstr_Usuario)];

                            Response.Clear();
                            Response.Buffer = true;
                            Response.Charset = "";
                            Response.Cache.SetCacheability(HttpCacheability.NoCache);
                            Response.ContentType = strFileType.Substring(1);
                            Response.AppendHeader("Content-Disposition", "attachment; filename=" + strFileName + strFileType);
                            Response.BinaryWrite(buffer);
                            Response.Flush();
                            Response.End();
                            Response.End();
                        }
                        else
                        {
                            DesplegarError(true);
                            txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorRetornoNulo"].ToString();
                        }
                    }
                    else
                    {
                        DesplegarError(true);
                        txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorParametros"].ToString();
                    }
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

                txtError.Text = txtError.Text + "\r\n" + "NICSP Excepcion:  " + ex.Message.ToString() + ". ";
                DesplegarError(true);
                #endregion
            }
            finally
            {
                WSINICSP_PC = null;
                dsNICSPDatos = null;
            }
        }

        /// <summary>
        /// Método: EstadosFinancierosCorreosAutorizacionGridView_PageIndexChanging
        /// Descripción:   Despliega en el GridView el nuevo índex solicitado.
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EstadosFinancierosCorreosAutorizacionGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {

                DataSet DataSet;
                DataTable dataTable;

                //DataSet = GetDataSetGridView_ConsultarArchivosAnexosEstadosFinancierosCargados();
                DataSet = GetDataSetGridView_ConsultarCorreosAutorizacionEstadosFinancierosCargados();
                dataTable = DataSet.Tables[0];


                if (dataTable != null)
                {
                    DataView dataView = new DataView(dataTable);
                    CorreosAutorizacionAnexosGridView.DataSource = dataView;
                    CorreosAutorizacionAnexosGridView.PageIndex = e.NewPageIndex;
                    CorreosAutorizacionAnexosGridView.DataBind();
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

                txtError.Text = txtError.Text + "\r\n" + "NICSP Excepcion:  " + ex.Message.ToString() + ". ";
                DesplegarError(true);
                #endregion
            }
            finally
            {

            }
        }// Fin EstadosFinancierosCorreosAutorizacionGridView_PageIndexChanging

        /**/
        ///// <summary>
        ///// Método: EstadosFinancierosArchivosPlantillasGridView_RowDeleting
        ///// Descripción:   
        ///// </summary>
        ///// <remarks>
        ///// Historia de cambios:
        ///// Fecha        Persona         Motivo
        ///// </remarks>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void EstadosFinancierosArchivosPlantillasGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    try
        //    {
        //        string strIdEstadoFinancieroArchivoPlantilla;

        //        strIdEstadoFinancieroArchivoPlantilla = ((Label)EstadosFinancierosArchivosPlantillasGridView.Rows[e.RowIndex].FindControl("CodigoIdEstadoFinancieroArchivoPlantillaLabel")).Text;

        //        DesplegarError(!Eliminar_ArchivoPlantillasEstadoFinanciero(strIdEstadoFinancieroArchivoPlantilla));

        //        EstadosFinancierosArchivosPlantillasGridView.EditIndex = -1;
        //        //Session["Buscar"] = false;
        //        ConsultarArchivosPlantillasEstadosFinancierosCargados_GridView();
        //    }
        //    catch (Exception ex)
        //    {
        //        #region MensajeError
        //        EventLog.WriteEntry(ConfigurationManager.AppSettings["EventLogSource"].ToString(),
        //            //Obtiene el nombre de la clase.
        //        "NICSP"
        //            //Nombre del método.
        //        + "." + MethodInfo.GetCurrentMethod().Name
        //            //Error especifico.
        //        + ": Excepcion  " + ex.Message.ToString() + ". ",
        //        EventLogEntryType.Error);

        //        txtError.Text = txtError.Text + "\r\n" + "NICSP Excepcion:  " + ex.Message.ToString() + ". ");
        //        DesplegarError(true);
        //        #endregion
        //    }
        //    finally
        //    {

        //    }
        //} // Fin EstadosFinancierosArchivosPlantillasGridView_RowDeleting

        /// <summary>
        /// Método: Eliminar_ArchivoPlantillasEstadoFinanciero
        /// Descripción:   Elimina la información de del Archivo Estado Financiero en la DB con respecto al parámetro suministrado.

        ///// </summary>
        ///// <remarks>
        ///// Historia de cambios:
        ///// Fecha        Persona         Motivo
        ///// </remarks>
        ///// <param name="TipoIDCuenta"></param>
        ///// <param name="TipoMoneda"></param>
        ///// <returns></returns>
        //protected Boolean Eliminar_ArchivoPlantillasEstadoFinanciero(string strIdEstadoFinancieroArchivoPlantilla)
        //{
        //    //Boolean boolError = true;

        //    //DataSet dsNICSPDatos = new DataSet();
        //    wsPlantillasConsolidacion WSINICSP_PC = new wsPlantillasConsolidacion();

        //    string Resultado, Mensaje;
        //    try
        //    {
        //        return WSINICSP_PC.EliminarArchivoPlantillaEstadoFinanciero(strIdEstadoFinancieroArchivoPlantilla, "", gstr_Usuario, out Resultado, out Mensaje);
        //    }
        //    catch (Exception ex)
        //    {
        //        #region MensajeError
        //        EventLog.WriteEntry(ConfigurationManager.AppSettings["EventLogSource"].ToString(),
        //            //Obtiene el nombre de la clase.
        //        "NICSP"
        //            //Nombre del método.
        //        + "." + MethodInfo.GetCurrentMethod().Name
        //            //Error especifico.
        //        + ": Excepcion  " + ex.Message.ToString() + ". ",
        //        EventLogEntryType.Error);
        //        #endregion

        //        throw (ex);
        //    }
        //    finally
        //    {
        //        //dsNICSPDatos = null;
        //        WSINICSP_PC = null;
        //    }
        //}// Fin Eliminar_ArchivoPlantillasEstadoFinanciero

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EstadosFinancierosArchivoPlantillasGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            wsPlantillasConsolidacion WSINICSP_PC = new wsPlantillasConsolidacion();
            DataSet dsNICSPDatos = new DataSet();
            try
            {
                if (e.CommandName == "Select")
                {
                    string strIdEstadoFinancieroArchivoPlantillas, strFileName, strFileType;

                    int index = Convert.ToInt32(e.CommandArgument);
                    strIdEstadoFinancieroArchivoPlantillas = ((Label)EstadosFinancierosArchivosPlantillasGridView.Rows[index].FindControl("CodigoIdEstadoFinancieroArchivoPlantillaLabel")).Text;
                    strFileName = ((Label)EstadosFinancierosArchivosPlantillasGridView.Rows[index].FindControl("ItemNombreArchivoLabel")).Text;
                    strFileType = ((Label)EstadosFinancierosArchivosPlantillasGridView.Rows[index].FindControl("ItemTipoArchivoLabel")).Text;

                    string strUsuario, strIdEntidad, strPeriodo, strUnidadTiempoPeriodo;
                    string Resultado, Mensaje;

                    strUsuario = gstr_Usuario;
                    strIdEntidad = CatalogoEntidadesDropDownList.SelectedValue;
                    strPeriodo = PeriodoDropDownList.SelectedItem.Text;
                    strUnidadTiempoPeriodo = UnidadTiempoPeriodoDropDownList.SelectedValue;

                    if ((!strIdEntidad.Equals(string.Empty)) && (!strPeriodo.Equals("Seleccione ítem")) && (!strUnidadTiempoPeriodo.Equals(string.Empty)))
                    {
                        dsNICSPDatos = WSINICSP_PC.BuscarArchivoPlantillaEstadoFinanciero(strIdEstadoFinancieroArchivoPlantillas, "", gstr_Usuario, out Resultado, out Mensaje);

                        byte[] buffer = new byte[(int)0];

                        if (dsNICSPDatos.Tables.Count > 0)//Si hizo la consulta pero No retorno nada
                        //if (!dsNICSPDatosGridView.Tables["Table"].Rows.Count.Equals(0))//Si hizo la consulta pero No retorno nada
                        {
                            for (int i = 0; i <= dsNICSPDatos.Tables["Table1"].Rows.Count - 1; i++)
                            {
                                //byte[] bufferX = new byte[(int)objSqlFileStreamX.Length];
                                //byte[] bufferX = new byte[(int)objSqlFileStreamX.Length];
                                buffer = (byte[])dsNICSPDatos.Tables["Table1"].Rows[i]["Buffer"];
                            }

                            //byte[] buffer = new byte[(int)objSqlFileStream.Length];
                            //byte[] buffer = new byte[(int)WSINICSP_PC.BuscarArchivoEstadoFinanciero(strIdEstadoFinancieroArchivo, "", gstr_Usuario)];

                            string PreNombre = strIdEntidad + strUnidadTiempoPeriodo + strPeriodo;
                            Response.Clear();
                            Response.Buffer = true;
                            Response.Charset = "";
                            Response.Cache.SetCacheability(HttpCacheability.NoCache);
                            Response.ContentType = strFileType.Substring(1);
                            Response.AppendHeader("Content-Disposition", "attachment; filename=" + PreNombre + strFileName + strFileType);
                            Response.BinaryWrite(buffer);
                            Response.Flush();
                            Response.End();

                        }
                        else
                        {
                            DesplegarError(true);
                            txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorRetornoNulo"].ToString();
                        }





                        //byte[] buffer = new byte[(int)0];

                        //if (dsNICSPDatos.Tables.Count > 0)//Si hizo la consulta pero No retorno nada
                        ////if (!dsNICSPDatosGridView.Tables["Table"].Rows.Count.Equals(0))//Si hizo la consulta pero No retorno nada
                        //{
                        //    for (int i = 0; i <= dsNICSPDatos.Tables["Table"].Rows.Count - 1; i++)
                        //    {
                        //        buffer = (byte[])dsNICSPDatos.Tables["Table"].Rows[i]["buffer"];
                        //    }
                        //}

                        ////byte[] buffer = new byte[(int)objSqlFileStream.Length];
                        ////byte[] buffer = new byte[(int)WSINICSP_PC.BuscarArchivoPlantillaEstadoFinanciero(strIdEstadoFinancieroArchivo, "", gstr_Usuario)];

                        //string PreNombre = strIdEntidad + strUnidadTiempoPeriodo + strPeriodo;
                        //Response.Clear();
                        //Response.Buffer = true;
                        //Response.Charset = "";
                        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        //Response.ContentType = strFileType.Substring(1);
                        //Response.AppendHeader("Content-Disposition", "attachment; filename=" + PreNombre + strFileName + strFileType);
                        //Response.BinaryWrite(buffer);
                        //Response.Flush();
                        //Response.End();

                    }
                    else
                    {
                        DesplegarError(true);
                        txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorParametros"].ToString();
                    }

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

                txtError.Text = txtError.Text + "\r\n" + "NICSP Excepcion:  " + ex.Message.ToString() + ". ";
                DesplegarError(true);
                #endregion
            }
            finally
            {
                WSINICSP_PC = null;
                dsNICSPDatos = null;
            }
        }

        /// <summary>
        /// Método: EstadosFinancierosArchivosPlantillasGridView_PageIndexChanging
        /// Descripción:   Despliega en el GridView el nuevo índex solicitado.
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EstadosFinancierosArchivosPlantillasGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {

                DataSet DataSet;
                DataTable dataTable;


                //DataSet = GetDataSetGridView_ConsultarEstadosFinancierosCargados();
                DataSet = GetDataSetGridView_ConsultarArchivosPlantillasEstadosFinancierosCargados();
                dataTable = DataSet.Tables[0];


                if (dataTable != null)
                {
                    DataView dataView = new DataView(dataTable);
                    EstadosFinancierosArchivosPlantillasGridView.DataSource = dataView;
                    EstadosFinancierosArchivosPlantillasGridView.PageIndex = e.NewPageIndex;
                    EstadosFinancierosArchivosPlantillasGridView.DataBind();
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

                txtError.Text = txtError.Text + "\r\n" + "NICSP Excepcion:  " + ex.Message.ToString() + ". ";
                DesplegarError(true);
                #endregion
            }
            finally
            {

            }
        }// Fin EstadosFinancierosArchivosPlantillasGridView_PageIndexChanging

        /**/

        /// <summary>
        /// Método: EstadosFinancierosArchivosGridView_PageIndexChanging
        /// Descripción:   Despliega en el GridView el nuevo índex solicitado.
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EstadosFinancierosArchivosGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                //Boolean Busqueda = false;
                //Busqueda = Boolean.Parse(Session["Buscar"].ToString());
                DataSet DataSet;
                DataTable dataTable;

                //if (Busqueda)
                //{
                //    DataSet = (DataSet)Session["DataSet_GridView"];
                //    dataTable = DataSet.Tables[0];
                //}
                //else
                //{
                DataSet = GetDataSetGridView_ConsultarEstadosFinancierosCargados();
                dataTable = DataSet.Tables[0];
                //}

                if (dataTable != null)
                {
                    DataView dataView = new DataView(dataTable);
                    EstadosFinancierosArchivosGridView.DataSource = dataView;
                    EstadosFinancierosArchivosGridView.PageIndex = e.NewPageIndex;
                    EstadosFinancierosArchivosGridView.DataBind();
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

                txtError.Text = txtError.Text + "\r\n" + "NICSP Excepcion:  " + ex.Message.ToString() + ". ";
                DesplegarError(true);
                #endregion
            }
            finally
            {

            }
        }// Fin EstadosFinancierosArchivosGridView_PageIndexChanging

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void EstadosFinancierosArchivosGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            wsPlantillasConsolidacion WSINICSP_PC = new wsPlantillasConsolidacion();
            DataSet dsNICSPDatos = new DataSet();

            try
            {
                if (e.CommandName == "Select")
                {
                    string strIdEstadoFinancieroArchivo, /*strPath, */strFileName, strFileType;
                    string Resultado, Mensaje;

                    //strIdEstadoFinancieroArchivo = ((Label)EstadosFinancierosArchivosGridView.Rows[e.RowIndex].FindControl("CodigoIdEstadoFinancieroArchivoLabel")).Text;
                    //strIdEstadoFinancieroArchivo = e.CommandArgument.ToString();
                    int index = Convert.ToInt32(e.CommandArgument);
                    strIdEstadoFinancieroArchivo = ((Label)EstadosFinancierosArchivosGridView.Rows[index].FindControl("CodigoIdEstadoFinancieroArchivoLabel")).Text;
                    strFileName = ((Label)EstadosFinancierosArchivosGridView.Rows[index].FindControl("ItemNombreArchivoLabel")).Text;
                    strFileType = ((Label)EstadosFinancierosArchivosGridView.Rows[index].FindControl("ItemTipoArchivoLabel")).Text;

                    if (!strIdEstadoFinancieroArchivo.Equals(string.Empty))
                    {
                        //.BuscarArchivoEstadoFinanciero(strIdEstadoFinancieroArchivo, "", gstr_Usuario/*, out Resultado, out Mensaje*/);

                        dsNICSPDatos = WSINICSP_PC.BuscarArchivoEstadoFinanciero(strIdEstadoFinancieroArchivo, "", gstr_Usuario, out Resultado, out Mensaje);

                        byte[] buffer = new byte[(int)0];

                        if (dsNICSPDatos.Tables.Count > 0)//Si hizo la consulta pero No retorno nada
                        //if (!dsNICSPDatosGridView.Tables["Table"].Rows.Count.Equals(0))//Si hizo la consulta pero No retorno nada
                        {
                            for (int i = 0; i <= dsNICSPDatos.Tables["Table1"].Rows.Count - 1; i++)
                            {
                                //byte[] bufferX = new byte[(int)objSqlFileStreamX.Length];
                                //byte[] bufferX = new byte[(int)objSqlFileStreamX.Length];
                                buffer = (byte[])dsNICSPDatos.Tables["Table1"].Rows[i]["Buffer"];
                            }

                            //byte[] buffer = new byte[(int)objSqlFileStream.Length];
                            //byte[] buffer = new byte[(int)WSINICSP_PC.BuscarArchivoEstadoFinanciero(strIdEstadoFinancieroArchivo, "", gstr_Usuario)];

                            Response.Clear();
                            Response.Buffer = true;
                            Response.Charset = "";
                            Response.Cache.SetCacheability(HttpCacheability.NoCache);
                            Response.ContentType = strFileType.Substring(1);
                            Response.AppendHeader("Content-Disposition", "attachment; filename=" + strFileName + strFileType);
                            Response.BinaryWrite(buffer);
                            Response.Flush();
                            Response.End();
                        }
                        else
                        {
                            DesplegarError(true);
                            txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorRetornoNulo"].ToString();
                        }
                    }
                    else
                    {
                        DesplegarError(true);
                        txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorParametros"].ToString();
                    }


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

                txtError.Text = txtError.Text + "\r\n" + "NICSP Excepcion:  " + ex.Message.ToString() + ". ";
                DesplegarError(true);
                #endregion
            }
            finally
            {
                WSINICSP_PC = null;
                dsNICSPDatos = null;
            }
        }


        /**/
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

                txtError.Text = txtError.Text + "\r\n" + "NICSP Excepcion:  " + ex.Message.ToString() + ". ";
                DesplegarError(true);
                #endregion
            }
            finally
            {

            }
        }// Fin AmbitoConsolidacionDropDownList_Primero



        ///// <summary>
        ///// Método: EstadoFinancieroDropDownList_Primero
        ///// Descripción:   Carga el ítem inicial del “DropDownList”.
        ///// </summary>
        ///// <remarks>
        ///// Historia de cambios:
        ///// Fecha        Persona         Motivo
        ///// </remarks>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //public void EstadoFinancieroDropDownList_Primero(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        EstadoFinancieroDropDownList.Items.Remove(EstadoFinancieroDropDownList.Items.FindByValue(""));
        //        EstadoFinancieroDropDownList.Items.Insert(0, new ListItem("Seleccione ítem", ""));
        //    }
        //    catch (Exception ex)
        //    {
        //        #region MensajeError
        //        EventLog.WriteEntry(ConfigurationManager.AppSettings["EventLogSource"].ToString(),
        //            //Obtiene el nombre de la clase.
        //        "NICSP"
        //            //Nombre del método.
        //        + "." + MethodInfo.GetCurrentMethod().Name
        //            //Error especifico.
        //        + ": Excepcion  " + ex.Message.ToString() + ". ",
        //        EventLogEntryType.Error);

        //        txtError.Text = txtError.Text + "\r\n" + "NICSP Excepcion:  " + ex.Message.ToString() + ". ");
        //        DesplegarError(true);
        //        #endregion
        //    }
        //    finally
        //    {

        //    }
        //}// Fin EstadoFinancieroDropDownList_Primero


        ///// <summary>
        ///// Método: EstadoFinancieroPlantillasDropDownList_Primero
        ///// Descripción:   Carga el ítem inicial del “DropDownList”.
        ///// </summary>
        ///// <remarks>
        ///// Historia de cambios:
        ///// Fecha        Persona         Motivo
        ///// </remarks>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //public void EstadoFinancieroPlantillasDropDownList_Primero(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        EstadoFinancieroPlantillasDropDownList.Items.Remove(EstadoFinancieroPlantillasDropDownList.Items.FindByValue(""));
        //        EstadoFinancieroPlantillasDropDownList.Items.Insert(0, new ListItem("Seleccione ítem", ""));
        //    }
        //    catch (Exception ex)
        //    {
        //        #region MensajeError
        //        EventLog.WriteEntry(ConfigurationManager.AppSettings["EventLogSource"].ToString(),
        //            //Obtiene el nombre de la clase.
        //        "NICSP"
        //            //Nombre del método.
        //        + "." + MethodInfo.GetCurrentMethod().Name
        //            //Error especifico.
        //        + ": Excepcion  " + ex.Message.ToString() + ". ",
        //        EventLogEntryType.Error);

        //        txtError.Text = txtError.Text + "\r\n" + "NICSP Excepcion:  " + ex.Message.ToString() + ". ");
        //        DesplegarError(true);
        //        #endregion
        //    }
        //    finally
        //    {

        //    }
        //}// Fin EstadoFinancieroPlantillasDropDownList_Primero

        /// <summary>
        /// Método: AmbitoConsolidacionDropDownList_SelectedIndexChanged
        /// Descripción:   
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

                txtError.Text = txtError.Text + "\r\n" + "NICSP Excepcion:  " + ex.Message.ToString() + ". ";
                DesplegarError(true);
                #endregion
            }
            finally
            {

            }
        }// Fin AmbitoConsolidacionDropDownList_SelectedIndexChanged

        ///// <summary>
        ///// Método: DropDownList_SelectedIndexChanged
        ///// Descripción:   
        ///// </summary>
        ///// <remarks>
        ///// Historia de cambios:
        ///// Fecha        Persona         Motivo
        ///// </remarks>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //public void DropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        ConsultarEstadosFinancierosCargados_GridView();
        //        ConsultarArchivosAnexosEstadosFinancierosCargados_GridView();
        //        ConsultarArchivosPlantillasEstadosFinancierosCargados_GridView();
        //        ConsultarCorreosAutorizacionEstadosFinancierosCargados_GridView();
        //    }
        //    catch (Exception ex)
        //    {
        //        #region MensajeError
        //        EventLog.WriteEntry(ConfigurationManager.AppSettings["EventLogSource"].ToString(),
        //            //Obtiene el nombre de la clase.
        //        "NICSP"
        //            //Nombre del método.
        //        + "." + MethodInfo.GetCurrentMethod().Name
        //            //Error especifico.
        //        + ": Excepcion  " + ex.Message.ToString() + ". ",
        //        EventLogEntryType.Error);

        //        txtError.Text = txtError.Text + "\r\n" + "NICSP Excepcion:  " + ex.Message.ToString() + ". ");
        //        DesplegarError(true);
        //        #endregion
        //    }
        //    finally
        //    {

        //    }
        //}// Fin DropDownList_SelectedIndexChanged

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
                string Resultado, Mensaje;
                dsNICSPDatosDropDownList = WSINICSP_PC.uwsBuscarEntidadesDeUnAmbito(IdAmbitoConsolidacion, "", gstr_Usuario, out Resultado, out Mensaje);

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

                txtError.Text = txtError.Text + "\r\n" + "NICSP Excepcion:  " + ex.Message.ToString() + ". ";
                DesplegarError(true);
                #endregion
            }
            finally
            {

            }
        }// Fin CatalogoUnidadTiempoPeriodoDropDownList_Primero

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

                txtError.Text = txtError.Text + "\r\n" + "NICSP Excepcion:  " + ex.Message.ToString() + ". ";
                DesplegarError(true);
                #endregion
            }
            finally
            {

            }
        }// Fin PeriodoDropDownList_Primero



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
                string Resultado, Mensaje;
                dsNICSPDatosDropDownList = WSINICSP_PC.BuscarCatalogoUnidadesTiempoPeriodo(ConfigurationManager.AppSettings["UnidadesTiempoPeriodoPermitido"].ToString(), "", gstr_Usuario, out Resultado, out Mensaje);

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
        /// Método: GetDataSetDropDownList_ConsultarEstadoFinanciero
        /// Descripción:   Devuelve un DataSet de la consulta de los Estados Financieros Disponibles.
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <returns></returns>
        public DataSet GetDataSetDropDownList_ConsultarEstadoFinanciero()
        {
            DataSet dsNICSPDatosDropDownList = new DataSet();
            wsPlantillasConsolidacion WSINICSP_PC = new wsPlantillasConsolidacion();
            string Resultado, Mensaje;
            try
            {

                dsNICSPDatosDropDownList = WSINICSP_PC.ConsultarEstadoFinanciero("", gstr_Usuario, out Resultado, out Mensaje);

                //if (dsNICSPDatosDropDownList.Tables["Result"].Rows[0]["status"].ToString() == "00")
                //{
                if (dsNICSPDatosDropDownList.Tables.Count > 0)//Si hizo la consulta pero No retorno nada
                //if (!dsNICSPDatosDropDownList.Tables["Table"].Rows.Count.Equals(0))//Si hizo la consulta pero No retorno nada
                {
                    for (int i = 0; i <= dsNICSPDatosDropDownList.Tables["Table"].Rows.Count - 1; i++)
                    {
                        string IdEstadoFinanciero = dsNICSPDatosDropDownList.Tables["Table"].Rows[i]["IdEstadoFinanciero"].ToString();
                        string NombreEstadoFinanciero = (string)dsNICSPDatosDropDownList.Tables["Table"].Rows[i]["NombreEstadoFinanciero"];
                        dsNICSPDatosDropDownList.Tables["Table"].Rows[i]["NombreEstadoFinanciero"] = IdEstadoFinanciero + "-" + NombreEstadoFinanciero;

                        if (IdEstadoFinanciero.Equals(ConfigurationManager.AppSettings["CodigoEstadosFinancieros_CorreoAutorizacion"].ToString())) // se borra porque para los correos de autorizacion se tiene un view por separado en la interface
                            dsNICSPDatosDropDownList.Tables["Table"].Rows[i].Delete();
                    }

                }
                else
                {
                    dsNICSPDatosDropDownList = GetDataSet_ConsultarEstadoFinanciero_Vacio();
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
        }// Fin GetDataSetDropDownList_ConsultarEstadoFinanciero

        /// <summary>
        /// Método: GetDataSet_ConsultarEstadoFinanciero_Vacio
        /// Descripción:   Devuelve un DataSet Vacio.
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <returns></returns>
        public DataSet GetDataSet_ConsultarEstadoFinanciero_Vacio()
        {
            DataSet dsNICSPDatosGridView = new DataSet();
            try
            {
                dsNICSPDatosGridView.Tables.Add(GetConsultarEstadoFinancieroVacio());

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
        } // Fin GetDataSet_ConsultarEstadoFinanciero_Vacio

        /// <summary>
        /// Método: GetConsultarEstadoFinancieroVacio
        /// Descripción:   .
        /// </summary>
        /// <remarks>
        /// Historia de cambios:
        /// Fecha        Persona         Motivo
        /// </remarks>
        /// <returns></returns>
        public DataTable GetConsultarEstadoFinancieroVacio()
        {
            DataTable table = new DataTable();
            try
            {
                table.Columns.Add("IdEstadoFinanciero", typeof(string));
                table.Columns.Add("NombreEstadoFinanciero", typeof(string));

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
        }// Fin GetConsultarEstadoFinancieroVacio

        /**/

        /**/

        //protected void ArchivosAnexosGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    try
        //    {
        //        if (e.CommandName == "GetFile")
        //        {

        //            SqlConnection objSqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        //            objSqlCon.Open();
        //            SqlTransaction objSqlTran = objSqlCon.BeginTransaction();

        //            SqlCommand objSqlCmd = new SqlCommand("pc.uspBuscarArchivoEstadoFinanciero", objSqlCon, objSqlTran);
        //            objSqlCmd.CommandType = CommandType.StoredProcedure;

        //            SqlParameter objSqlParam1 = new SqlParameter("@pIdEstadoFinancieroArchivo", SqlDbType.VarChar);
        //            objSqlParam1.Value = e.CommandArgument;

        //            objSqlCmd.Parameters.Add(objSqlParam1);
        //            string path = string.Empty;
        //            string fileType = string.Empty;
        //            string fileName = string.Empty;

        //            using (SqlDataReader sdr = objSqlCmd.ExecuteReader())
        //            {
        //                while (sdr.Read())
        //                {
        //                    path = sdr["RutaSistemaArchivo"].ToString();
        //                    fileName = sdr["NombreArchivo"].ToString();
        //                    fileType = sdr["TipoArchivo"].ToString();
        //                }

        //            }

        //            objSqlCmd = new SqlCommand("SELECT GET_FILESTREAM_TRANSACTION_CONTEXT()", objSqlCon, objSqlTran);

        //            byte[] objContext = (byte[])objSqlCmd.ExecuteScalar();

        //            SqlFileStream objSqlFileStream = new SqlFileStream(path, objContext, FileAccess.Read);

        //            byte[] buffer = new byte[(int)objSqlFileStream.Length];
        //            objSqlFileStream.Read(buffer, 0, buffer.Length);
        //            objSqlFileStream.Close();

        //            objSqlTran.Commit();

        //            Response.Clear();
        //            Response.Buffer = true;
        //            Response.Charset = "";
        //            Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //            Response.ContentType = fileType.Substring(1);
        //            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + fileType);
        //            Response.BinaryWrite(buffer);
        //            Response.Flush();
        //            Response.End();

        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        #region MensajeError
        //        EventLog.WriteEntry(ConfigurationManager.AppSettings["EventLogSource"].ToString(),
        //            //Obtiene el nombre de la clase.
        //        "NICSP"
        //            //Nombre del método.
        //        + "." + MethodInfo.GetCurrentMethod().Name
        //            //Error especifico.
        //        + ": Excepcion  " + ex.Message.ToString() + ". ",
        //        EventLogEntryType.Error);

        //        txtError.Text = txtError.Text + "\r\n" +  "NICSP Excepcion:  " + ex.Message.ToString() + ". ");
        //        DesplegarError(true);
        //        #endregion
        //    }
        //    finally
        //    {

        //    }
        //}

        //protected void CorreosAutorizacionAnexosGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    try
        //    { 
        //        if (e.CommandName == "GetFile")
        //        {

        //            SqlConnection objSqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        //            objSqlCon.Open();
        //            SqlTransaction objSqlTran = objSqlCon.BeginTransaction();

        //            SqlCommand objSqlCmd = new SqlCommand("pc.uspBuscarArchivoEstadoFinanciero", objSqlCon, objSqlTran);
        //            objSqlCmd.CommandType = CommandType.StoredProcedure;

        //            SqlParameter objSqlParam1 = new SqlParameter("@pIdEstadoFinancieroArchivo", SqlDbType.VarChar);
        //            objSqlParam1.Value = e.CommandArgument;

        //            objSqlCmd.Parameters.Add(objSqlParam1);
        //            string path = string.Empty;
        //            string fileType = string.Empty;
        //            string fileName = string.Empty;

        //            using (SqlDataReader sdr = objSqlCmd.ExecuteReader())
        //            {
        //                while (sdr.Read())
        //                {
        //                    path = sdr["RutaSistemaArchivo"].ToString();
        //                    fileName = sdr["NombreArchivo"].ToString();
        //                    fileType = sdr["TipoArchivo"].ToString();
        //                }

        //            }

        //            objSqlCmd = new SqlCommand("SELECT GET_FILESTREAM_TRANSACTION_CONTEXT()", objSqlCon, objSqlTran);

        //            byte[] objContext = (byte[])objSqlCmd.ExecuteScalar();

        //            SqlFileStream objSqlFileStream = new SqlFileStream(path, objContext, FileAccess.Read);

        //            byte[] buffer = new byte[(int)objSqlFileStream.Length];
        //            objSqlFileStream.Read(buffer, 0, buffer.Length);
        //            objSqlFileStream.Close();

        //            objSqlTran.Commit();

        //            Response.Clear();
        //            Response.Buffer = true;
        //            Response.Charset = "";
        //            Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //            Response.ContentType = fileType.Substring(1);
        //            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName + fileType);
        //            Response.BinaryWrite(buffer);
        //            Response.Flush();
        //            Response.End();

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        #region MensajeError
        //        EventLog.WriteEntry(ConfigurationManager.AppSettings["EventLogSource"].ToString(),
        //            //Obtiene el nombre de la clase.
        //        "NICSP"
        //            //Nombre del método.
        //        + "." + MethodInfo.GetCurrentMethod().Name
        //            //Error especifico.
        //        + ": Excepcion  " + ex.Message.ToString() + ". ",
        //        EventLogEntryType.Error);

        //        txtError.Text = txtError.Text + "\r\n" + "NICSP Excepcion:  " + ex.Message.ToString() + ". ");
        //        DesplegarError(true);
        //        #endregion
        //    }
        //    finally
        //    {

        //    }
        //}


        /**/

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
                    txtError.Visible = true;
                else
                    txtError.Visible = false;

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
        }

        //public string _Left(string str, int length)
        //{
        //    return str.Substring(0, Math.Min(length, str.Length));
        //}

        //protected void CargarArchivosButton_Click(object sender, EventArgs e)
        //{

        //    DataSet dsNICSPDatos = new DataSet();
        //    wsPlantillasConsolidacion WSINICSP_PC = new wsPlantillasConsolidacion();

        //    byte[] bufferFlujoEfectivo = new byte[(int)FlujoEfectivoFileUpload.FileContent.Length];
        //    FlujoEfectivoFileUpload.FileContent.Read(bufferFlujoEfectivo, 0, bufferFlujoEfectivo.Length);

        //    byte[] bufferCambioPatrimonioNeto = new byte[(int)CambioPatrimonioNetoFileUpload.FileContent.Length];
        //    CambioPatrimonioNetoFileUpload.FileContent.Read(bufferCambioPatrimonioNeto, 0, bufferCambioPatrimonioNeto.Length);

        //    byte[] bufferBalanceComprobacion = new byte[(int)BalanceComprobacionFileUpload.FileContent.Length];
        //    BalanceComprobacionFileUpload.FileContent.Read(bufferBalanceComprobacion, 0, bufferBalanceComprobacion.Length);

        //    byte[] bufferEstadoVarios = new byte[(int)EstadoVariosFileUpload.FileContent.Length];
        //    EstadoVariosFileUpload.FileContent.Read(bufferEstadoVarios, 0, bufferEstadoVarios.Length);

        //    byte[] bufferNotasEstadosFinancieros = new byte[(int)NotasEstadosFinancierosFileUpload.FileContent.Length];
        //    NotasEstadosFinancierosFileUpload.FileContent.Read(bufferNotasEstadosFinancieros, 0, bufferNotasEstadosFinancieros.Length);

        //    try
        //    {
        //        string strUsuario, strIdEntidad, strPeriodo, strUnidadTiempoPeriodo;
        //        string strExcelIdEntidad, strExcelPeriodo, strExcelUnidadTiempoPeriodo;
        //        string strNombreArchivo, strInfoNombreArchivo;
        //        string CodigoEstadosFinancieros_EstadoFlujoEfectivo, CodigoEstadosFinancieros_EstadoCambioPatrimonioNeto,
        //            CodigoEstadosFinancieros_ConsolidacionNotas, CodigoEstadosFinancieros_EstadoBalanceComprobacion, CodigoEstadosFinancieros_EstadoVarios;
        //        string NombreArchivo = "", TipoArchivo = "";

        //        bool bCargaArchivo = false;
        //        string VarDTSX, Resultado, Mensaje, DTSX_PaqueteURL, DTSX_RutaDescargaArchivo, DTSX_DescargaArchivoEnServer, DTSX_CargaEstadoFinancieroFlujoEfectivo, DTSX_CargaEstadoFinancieroCambioPatrimonioNeto, DTSX_CargaEstadoFinancieroBalanceComprobacion;
        //        string IdEstadoFinancieroArchivo = "";
        //        bool Run64BitRuntime_DTSX_DescargaArchivoEnServer, Run64BitRuntime_DTSX_CargaEstadoFinancieroFlujoEfectivo, Run64BitRuntime_DTSX_CargaEstadoFinancieroCambioPatrimonioNeto, Run64BitRuntime_DTSX_CargaEstadoFinancieroBalanceComprobacion;
        //        strUsuario = gstr_Usuario;
        //        if (strUsuario.Equals(""))
        //            strUsuario = "Anonimo";
        //        strIdEntidad = CatalogoEntidadesDropDownList.SelectedValue;
        //        strPeriodo = PeriodoDropDownList.SelectedItem.Text;
        //        strUnidadTiempoPeriodo = UnidadTiempoPeriodoDropDownList.SelectedValue;

        //        CodigoEstadosFinancieros_EstadoFlujoEfectivo = ConfigurationManager.AppSettings["CodigoEstadosFinancieros_EstadoFlujoEfectivo"].ToString();
        //        CodigoEstadosFinancieros_EstadoCambioPatrimonioNeto = ConfigurationManager.AppSettings["CodigoEstadosFinancieros_EstadoCambioPatrimonioNeto"].ToString();
        //        CodigoEstadosFinancieros_EstadoBalanceComprobacion = ConfigurationManager.AppSettings["CodigoEstadosFinancieros_EstadoBalanceComprobacion"].ToString();
        //        CodigoEstadosFinancieros_EstadoVarios = ConfigurationManager.AppSettings["CodigoEstadosFinancieros_EstadoVarios"].ToString();
        //        CodigoEstadosFinancieros_ConsolidacionNotas = ConfigurationManager.AppSettings["CodigoEstadosFinancieros_ConsolidacionNotas"].ToString();

        //        if ((!strIdEntidad.Equals(string.Empty)) && (!strPeriodo.Equals("Seleccione ítem")) && (!strUnidadTiempoPeriodo.Equals(string.Empty)))
        //        {
        //            dsNICSPDatos = WSINICSP_PC.ValidarUnidadTiempoPeriodoCorrectoCorreoAutorizacion(strIdEntidad, int.Parse(strPeriodo), strUnidadTiempoPeriodo, "", gstr_Usuario, out Resultado, out Mensaje);

        //            if (dsNICSPDatos.Tables.Count > 0)//Si hizo la consulta pero No retorno nada
        //            //if (!dsNICSPDatosDropDownList.Tables["Table"].Rows.Count.Equals(0))//Si hizo la consulta pero No retorno nada
        //            {
        //                bool bCargaUnidadTiempoPeriodoValido = false;
        //                string Descripcion = "";

        //                for (int i = 0; i <= dsNICSPDatos.Tables["Table"].Rows.Count - 1; i++)
        //                {
        //                    bCargaUnidadTiempoPeriodoValido = (bool)dsNICSPDatos.Tables["Table"].Rows[i]["CargaUnidadTiempoPeriodoValido"];
        //                    Descripcion = dsNICSPDatos.Tables["Table"].Rows[i]["Descripcion"].ToString();
        //                }

        //                DTSX_PaqueteURL = ConfigurationManager.AppSettings["DTSX_PaqueteURL"].ToString();
        //                DTSX_RutaDescargaArchivo = ConfigurationManager.AppSettings["DTSX_RutaDescargaArchivo"].ToString();

        //                DTSX_DescargaArchivoEnServer = ConfigurationManager.AppSettings["DTSX_DescargaArchivoEnServer"].ToString();
        //                Run64BitRuntime_DTSX_DescargaArchivoEnServer = bool.Parse(ConfigurationManager.AppSettings["Run64BitRuntime_DTSX_DescargaArchivoEnServer"].ToString());

        //                if (bCargaUnidadTiempoPeriodoValido)
        //                {

        //                    /*Flujo Efectivo*/
        //                    if (FlujoEfectivoFileUpload.FileContent.Length > 0)
        //                    {
        //                        strNombreArchivo = FlujoEfectivoFileUpload.FileName;
        //                        //21103T32019_ESTADO_DE_FLUJO_DE_EFECTIVO.xlsx

        //                        strInfoNombreArchivo = strNombreArchivo.Substring(0, strNombreArchivo.IndexOf("_"));
        //                        strExcelPeriodo = strInfoNombreArchivo.Substring(strInfoNombreArchivo.Length - 4, 4);

        //                        strExcelUnidadTiempoPeriodo = strInfoNombreArchivo.Substring(strInfoNombreArchivo.Length - 6, 2);
        //                        strInfoNombreArchivo = strInfoNombreArchivo.Replace(strExcelUnidadTiempoPeriodo + strExcelPeriodo, "");
        //                        strExcelIdEntidad = strInfoNombreArchivo;

        //                        if (strIdEntidad.Equals(strExcelIdEntidad) && strPeriodo.Equals(strExcelPeriodo) && strUnidadTiempoPeriodo.Equals(strExcelUnidadTiempoPeriodo))
        //                        {
        //                            if (validaSize(bufferFlujoEfectivo.Length, strIdEntidad, int.Parse(strPeriodo), strUnidadTiempoPeriodo))
        //                            {
        //                                dsNICSPDatos = WSINICSP_PC.InsertarArchivoEstadoFinancieroFileStream(bufferFlujoEfectivo, strIdEntidad, int.Parse(CodigoEstadosFinancieros_EstadoFlujoEfectivo),
        //                                              int.Parse(strPeriodo), strUnidadTiempoPeriodo, Path.GetFileNameWithoutExtension(strNombreArchivo),
        //                                              Path.GetExtension(strNombreArchivo), bufferFlujoEfectivo.Length, DateTime.Now, gstr_Usuario, "", gstr_Usuario, out Resultado, out Mensaje);

        //                                if (dsNICSPDatos.Tables.Count > 0)//Si hizo la consulta pero No retorno nada
        //                                //if (!dsNICSPDatosDropDownList.Tables["Table"].Rows.Count.Equals(0))//Si hizo la consulta pero No retorno nada
        //                                {
        //                                    for (int i = 0; i <= dsNICSPDatos.Tables["Table1"].Rows.Count - 1; i++)
        //                                    {
        //                                        IdEstadoFinancieroArchivo = dsNICSPDatos.Tables["Table1"].Rows[i]["IdEstadoFinancieroArchivo"].ToString();
        //                                        NombreArchivo = dsNICSPDatos.Tables["Table1"].Rows[i]["NombreArchivo"].ToString();
        //                                        TipoArchivo = dsNICSPDatos.Tables["Table1"].Rows[i]["TipoArchivo"].ToString();
        //                                    }
        //                                }

        //                                if (Resultado.Equals("01"))
        //                                    bCargaArchivo = true;

        //                                if (DesplegarError(!bCargaArchivo))
        //                                {
        //                                    if (DesplegarError(!Eliminar_ArchivoEstadoFinanciero(IdEstadoFinancieroArchivo)))
        //                                    {
        //                                        txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorBorrarArchivo"].ToString());
        //                                    }
        //                                    DesplegarError(true);
        //                                    txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorCargaArchivo"].ToString() + strNombreArchivo);
        //                                }
        //                                else
        //                                {
        //                                    VarDTSX = "/SET \\Package.Variables[User::IdArchivo].Properties[Value];\"" + IdEstadoFinancieroArchivo + "\" /SET \\Package.Variables[User::RutaDescargaArchivo].Properties[Value];\"" + DTSX_RutaDescargaArchivo + "\"";

        //                                    WSINICSP_PC.uwsEjecutarDTSX(DTSX_PaqueteURL, DTSX_DescargaArchivoEnServer, VarDTSX, Run64BitRuntime_DTSX_DescargaArchivoEnServer, "", gstr_Usuario, out Resultado, out Mensaje);

        //                                    if (DesplegarError(!Resultado.Equals("00")))
        //                                    {
        //                                        if (DesplegarError(!Eliminar_ArchivoEstadoFinanciero(IdEstadoFinancieroArchivo)))
        //                                        {
        //                                            txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorBorrarArchivo"].ToString());
        //                                        }
        //                                        DesplegarError(true);
        //                                        txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorCargaArchivo"].ToString() + " (Bajar en Carpeta) " + strNombreArchivo);
        //                                    }
        //                                    else
        //                                    {
        //                                        DTSX_CargaEstadoFinancieroFlujoEfectivo = ConfigurationManager.AppSettings["DTSX_CargaEstadoFinancieroFlujoEfectivo"].ToString();
        //                                        Run64BitRuntime_DTSX_CargaEstadoFinancieroFlujoEfectivo = bool.Parse(ConfigurationManager.AppSettings["Run64BitRuntime_DTSX_CargaEstadoFinancieroFlujoEfectivo"].ToString());

        //                                        //VarDTSX = "/SET \\Package.Variables[User::PathExcelFile].Properties[Value];\"L:\\SistemaGestor\\Archivos_SistemaGestor\\\\\" /SET \\Package.Variables[User::NameExcelFile].Properties[Value];\"" + NombreArchivo + TipoArchivo + "\" /SET \\Package.Variables[User::UsuarioCarga].Properties[Value];\"dmendez\"";
        //                                        VarDTSX = "/SET \\Package.Variables[User::PathExcelFile].Properties[Value];\"" + DTSX_RutaDescargaArchivo + "\" /SET \\Package.Variables[User::NameExcelFile].Properties[Value];\"" + NombreArchivo + TipoArchivo + "\" /SET \\Package.Variables[User::UsuarioCarga].Properties[Value];\"" + strUsuario + "\"";

        //                                        WSINICSP_PC.uwsEjecutarDTSX(DTSX_PaqueteURL, DTSX_CargaEstadoFinancieroFlujoEfectivo, VarDTSX, Run64BitRuntime_DTSX_CargaEstadoFinancieroFlujoEfectivo, "", gstr_Usuario, out Resultado, out Mensaje);
        //                                        ///*dsNICSPDatos*/ string asd = WSINICSP_PC.uwsPrueba2("", ""/*,out Resultado, out Mensaje*/);
        //                                        //dsNICSPDatos = WSINICSP_PC.ValidarEstadoFinancieroFlujoEfectivo(strIdEntidad, int.Parse(strPeriodo), strUnidadTiempoPeriodo, "", gstr_Usuario, out Resultado, out Mensaje);

        //                                        if (DesplegarError(!Resultado.Equals("00")))
        //                                        {
        //                                            if (DesplegarError(!Eliminar_ArchivoEstadoFinanciero(IdEstadoFinancieroArchivo)))
        //                                            {
        //                                                txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorBorrarArchivo"].ToString());
        //                                            }

        //                                            if (DesplegarError(!Eliminar_EstadoFinanciero(CodigoEstadosFinancieros_EstadoFlujoEfectivo, strIdEntidad, int.Parse(strPeriodo), strUnidadTiempoPeriodo)))
        //                                            {
        //                                                txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorBorrarArchivo"].ToString());
        //                                            }

        //                                            DesplegarError(true);
        //                                            txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorCargaArchivo"].ToString() + " (Bajar en Tabla) " + strNombreArchivo);
        //                                        }
        //                                        else
        //                                        {
        //                                            dsNICSPDatos = WSINICSP_PC.ValidarEstadoFinancieroFlujoEfectivo(strIdEntidad,
        //                                                int.Parse(strPeriodo), strUnidadTiempoPeriodo, "", gstr_Usuario, out Resultado, out Mensaje);

        //                                            if (DesplegarError(!Resultado.Equals("00")))
        //                                            {
        //                                                if (DesplegarError(!Eliminar_ArchivoEstadoFinanciero(IdEstadoFinancieroArchivo)))
        //                                                {
        //                                                    txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorBorrarArchivo"].ToString());
        //                                                }

        //                                                if (DesplegarError(!Eliminar_EstadoFinanciero(CodigoEstadosFinancieros_EstadoFlujoEfectivo, strIdEntidad, int.Parse(strPeriodo), strUnidadTiempoPeriodo)))
        //                                                {
        //                                                    txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorBorrarArchivo"].ToString());
        //                                                }

        //                                                DesplegarError(true);
        //                                                txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorValidaPeriodoActual"].ToString() + " (Bajar en Tabla) " + strNombreArchivo);
        //                                            }
        //                                            else
        //                                            {
        //                                                //if (dsNICSPDatos.Tables.Count > 0)//Si hizo la consulta pero No retorno nada
        //                                                if (!dsNICSPDatos.Tables["Table"].Rows.Count.Equals(0))//Si hizo la consulta pero No retorno nada
        //                                                {
        //                                                    string Consecutivo, ErrorDescripcion, IdEntidad, Periodo, UnidadTiempoPeriodo, NumConsecutivo, NumCuenta, CuentaConcepto, MontoPeriodoActual, MontoPeriodoAnterior;

        //                                                    txtError.Text = txtError.Text + "\r\n" + "Validación de los Datos del Flujo de Efectivo...");
        //                                                    txtError.Text = txtError.Text + "\r\n" + "Consecutivo, ErrorDescripcion, IdEntidad, Periodo, UnidadTiempoPeriodo, NumConsecutivo, NumCuenta, CuentaConcepto, MontoPeriodoActual, MontoPeriodoAnterior");
        //                                                    for (int i = 0; i <= dsNICSPDatos.Tables["Table"].Rows.Count - 1; i++)
        //                                                    {
        //                                                        Consecutivo = dsNICSPDatos.Tables["Table"].Rows[i]["Consecutivo"].ToString();
        //                                                        ErrorDescripcion = dsNICSPDatos.Tables["Table"].Rows[i]["ErrorDescripcion"].ToString();
        //                                                        IdEntidad = dsNICSPDatos.Tables["Table"].Rows[i]["IdEntidad"].ToString();
        //                                                        Periodo = dsNICSPDatos.Tables["Table"].Rows[i]["Periodo"].ToString();
        //                                                        UnidadTiempoPeriodo = dsNICSPDatos.Tables["Table"].Rows[i]["UnidadTiempoPeriodo"].ToString();
        //                                                        NumConsecutivo = dsNICSPDatos.Tables["Table"].Rows[i]["NumConsecutivo"].ToString();
        //                                                        NumCuenta = dsNICSPDatos.Tables["Table"].Rows[i]["NumCuenta"].ToString();
        //                                                        CuentaConcepto = dsNICSPDatos.Tables["Table"].Rows[i]["CuentaConcepto"].ToString();
        //                                                        MontoPeriodoActual = dsNICSPDatos.Tables["Table"].Rows[i]["MontoPeriodoActual"].ToString();
        //                                                        MontoPeriodoAnterior = dsNICSPDatos.Tables["Table"].Rows[i]["MontoPeriodoAnterior"].ToString();

        //                                                        txtError.Text = txtError.Text + "\r\n" + Consecutivo + ", " + ErrorDescripcion + ", " + IdEntidad + ", " + Periodo + ", " + UnidadTiempoPeriodo + ", " + NumConsecutivo + ", " + NumCuenta + ", " + CuentaConcepto + ", " + MontoPeriodoActual + ", " + MontoPeriodoAnterior);
        //                                                        DesplegarError(true);
        //                                                    }

        //                                                    if (DesplegarError(!Eliminar_ArchivoEstadoFinanciero(IdEstadoFinancieroArchivo)))
        //                                                    {
        //                                                        txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorBorrarArchivo"].ToString());
        //                                                    }

        //                                                    if (DesplegarError(!Eliminar_EstadoFinanciero(CodigoEstadosFinancieros_EstadoFlujoEfectivo, strIdEntidad, int.Parse(strPeriodo), strUnidadTiempoPeriodo)))
        //                                                    {
        //                                                        txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorBorrarArchivo"].ToString());
        //                                                    }

        //                                                    DesplegarError(true);
        //                                                }
        //                                            }
        //                                        }
        //                                    }


        //                                }
        //                            }
        //                            else
        //                            {
        //                                DesplegarError(true);
        //                                txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["ErrorSizeByteMaxTotalArchivos"].ToString().Replace("X", ((Double.Parse(ConfigurationManager.AppSettings["SizeByteMaxTotalArchivos"].ToString()) / 1024.00) / 1024.00).ToString()));
        //                            }
        //                        }
        //                        else
        //                        {
        //                            DesplegarError(true);
        //                            txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorDatosWebContraExcel"].ToString());
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (FlujoEfectivoFileUpload.Enabled)
        //                        {
        //                            DesplegarError(true);
        //                            txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorNoExisteArchivoFlujoEfectivo"].ToString());
        //                        }
        //                    }

        //                    /*Cambio Patrimonio Neto*/
        //                    if (CambioPatrimonioNetoFileUpload.FileContent.Length > 0)
        //                    {
        //                        strNombreArchivo = CambioPatrimonioNetoFileUpload.FileName;
        //                        //21103T32019_ESTADO_DE_FLUJO_DE_EFECTIVO.xlsx

        //                        strInfoNombreArchivo = strNombreArchivo.Substring(0, strNombreArchivo.IndexOf("_"));
        //                        strExcelPeriodo = strInfoNombreArchivo.Substring(strInfoNombreArchivo.Length - 4, 4);

        //                        strExcelUnidadTiempoPeriodo = strInfoNombreArchivo.Substring(strInfoNombreArchivo.Length - 6, 2);
        //                        strInfoNombreArchivo = strInfoNombreArchivo.Replace(strExcelUnidadTiempoPeriodo + strExcelPeriodo, "");
        //                        strExcelIdEntidad = strInfoNombreArchivo;

        //                        if (strIdEntidad.Equals(strExcelIdEntidad) && strPeriodo.Equals(strExcelPeriodo) && strUnidadTiempoPeriodo.Equals(strExcelUnidadTiempoPeriodo))
        //                        {
        //                            if (validaSize(bufferCambioPatrimonioNeto.Length, strIdEntidad, int.Parse(strPeriodo), strUnidadTiempoPeriodo))
        //                            {
        //                                dsNICSPDatos = WSINICSP_PC.InsertarArchivoEstadoFinancieroFileStream(bufferCambioPatrimonioNeto, strIdEntidad, int.Parse(CodigoEstadosFinancieros_EstadoCambioPatrimonioNeto),
        //                                              int.Parse(strPeriodo), strUnidadTiempoPeriodo, Path.GetFileNameWithoutExtension(strNombreArchivo),
        //                                              Path.GetExtension(strNombreArchivo), bufferCambioPatrimonioNeto.Length, DateTime.Now, gstr_Usuario, "", gstr_Usuario, out Resultado, out Mensaje);

        //                                if (dsNICSPDatos.Tables.Count > 0)//Si hizo la consulta pero No retorno nada
        //                                //if (!dsNICSPDatosDropDownList.Tables["Table"].Rows.Count.Equals(0))//Si hizo la consulta pero No retorno nada
        //                                {
        //                                    for (int i = 0; i <= dsNICSPDatos.Tables["Table1"].Rows.Count - 1; i++)
        //                                    {
        //                                        IdEstadoFinancieroArchivo = dsNICSPDatos.Tables["Table1"].Rows[i]["IdEstadoFinancieroArchivo"].ToString();
        //                                        NombreArchivo = dsNICSPDatos.Tables["Table1"].Rows[i]["NombreArchivo"].ToString();
        //                                        TipoArchivo = dsNICSPDatos.Tables["Table1"].Rows[i]["TipoArchivo"].ToString();
        //                                    }
        //                                }

        //                                if (Resultado.Equals("01"))
        //                                    bCargaArchivo = true;

        //                                if (DesplegarError(!bCargaArchivo))
        //                                {
        //                                    if (DesplegarError(!Eliminar_ArchivoEstadoFinanciero(IdEstadoFinancieroArchivo)))
        //                                    {
        //                                        txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorBorrarArchivo"].ToString());
        //                                    }
        //                                    DesplegarError(true);
        //                                    txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorCargaArchivo"].ToString() + strNombreArchivo);
        //                                }
        //                                else
        //                                {
        //                                    VarDTSX = "/SET \\Package.Variables[User::IdArchivo].Properties[Value];\"" + IdEstadoFinancieroArchivo + "\" /SET \\Package.Variables[User::RutaDescargaArchivo].Properties[Value];\"" + DTSX_RutaDescargaArchivo + "\"";

        //                                    WSINICSP_PC.uwsEjecutarDTSX(DTSX_PaqueteURL, DTSX_DescargaArchivoEnServer, VarDTSX, Run64BitRuntime_DTSX_DescargaArchivoEnServer, "", gstr_Usuario, out Resultado, out Mensaje);

        //                                    if (DesplegarError(!Resultado.Equals("00")))
        //                                    {
        //                                        if (DesplegarError(!Eliminar_ArchivoEstadoFinanciero(IdEstadoFinancieroArchivo)))
        //                                        {
        //                                            txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorBorrarArchivo"].ToString());
        //                                        }
        //                                        DesplegarError(true);
        //                                        txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorCargaArchivo"].ToString() + " (Bajar en Carpeta) " + strNombreArchivo);
        //                                    }
        //                                    else
        //                                    {
        //                                        DTSX_CargaEstadoFinancieroCambioPatrimonioNeto = ConfigurationManager.AppSettings["DTSX_CargaEstadoFinancieroCambioPatrimonioNeto"].ToString();
        //                                        Run64BitRuntime_DTSX_CargaEstadoFinancieroCambioPatrimonioNeto = bool.Parse(ConfigurationManager.AppSettings["Run64BitRuntime_DTSX_CargaEstadoFinancieroCambioPatrimonioNeto"].ToString());

        //                                        //VarDTSX = "/SET \\Package.Variables[User::PathExcelFile].Properties[Value];\"L:\\SistemaGestor\\Archivos_SistemaGestor\\\\\" /SET \\Package.Variables[User::NameExcelFile].Properties[Value];\"" + NombreArchivo + TipoArchivo + "\" /SET \\Package.Variables[User::UsuarioCarga].Properties[Value];\"dmendez\"";
        //                                        VarDTSX = "/SET \\Package.Variables[User::PathExcelFile].Properties[Value];\"" + DTSX_RutaDescargaArchivo + "\" /SET \\Package.Variables[User::NameExcelFile].Properties[Value];\"" + NombreArchivo + TipoArchivo + "\" /SET \\Package.Variables[User::UsuarioCarga].Properties[Value];\"" + strUsuario + "\"";

        //                                        WSINICSP_PC.uwsEjecutarDTSX(DTSX_PaqueteURL, DTSX_CargaEstadoFinancieroCambioPatrimonioNeto, VarDTSX, Run64BitRuntime_DTSX_CargaEstadoFinancieroCambioPatrimonioNeto, "", gstr_Usuario, out Resultado, out Mensaje);
        //                                        ///*dsNICSPDatos*/ string asd = WSINICSP_PC.uwsPrueba2("", ""/*,out Resultado, out Mensaje*/);
        //                                        //dsNICSPDatos = WSINICSP_PC.ValidarEstadoFinancieroFlujoEfectivo(strIdEntidad, int.Parse(strPeriodo), strUnidadTiempoPeriodo, "", gstr_Usuario, out Resultado, out Mensaje);

        //                                        if (DesplegarError(!Resultado.Equals("00")))
        //                                        {
        //                                            if (DesplegarError(!Eliminar_ArchivoEstadoFinanciero(IdEstadoFinancieroArchivo)))
        //                                            {
        //                                                txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorBorrarArchivo"].ToString());
        //                                            }

        //                                            if (DesplegarError(!Eliminar_EstadoFinanciero(CodigoEstadosFinancieros_EstadoCambioPatrimonioNeto, strIdEntidad, int.Parse(strPeriodo), strUnidadTiempoPeriodo)))
        //                                            {
        //                                                txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorBorrarArchivo"].ToString());
        //                                            }

        //                                            DesplegarError(true);
        //                                            txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorCargaArchivo"].ToString() + " (Bajar en Tabla) " + strNombreArchivo);
        //                                        }
        //                                        else
        //                                        {
        //                                            dsNICSPDatos = WSINICSP_PC.ValidarEstadoFinancieroCambioPatrimonioNeto(strIdEntidad,
        //                                                int.Parse(strPeriodo), strUnidadTiempoPeriodo, "", gstr_Usuario, out Resultado, out Mensaje);

        //                                            if (DesplegarError(!Resultado.Equals("00")))
        //                                            {
        //                                                if (DesplegarError(!Eliminar_ArchivoEstadoFinanciero(IdEstadoFinancieroArchivo)))
        //                                                {
        //                                                    txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorBorrarArchivo"].ToString());
        //                                                }

        //                                                if (DesplegarError(!Eliminar_EstadoFinanciero(CodigoEstadosFinancieros_EstadoCambioPatrimonioNeto, strIdEntidad, int.Parse(strPeriodo), strUnidadTiempoPeriodo)))
        //                                                {
        //                                                    txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorBorrarArchivo"].ToString());
        //                                                }

        //                                                DesplegarError(true);
        //                                                txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorValidaPeriodoActual"].ToString() + " (Bajar en Tabla) " + strNombreArchivo);
        //                                            }
        //                                            else
        //                                            {
        //                                                //if (dsNICSPDatos.Tables.Count > 0)//Si hizo la consulta pero No retorno nada
        //                                                if (!dsNICSPDatos.Tables["Table"].Rows.Count.Equals(0))//Si hizo la consulta pero No retorno nada
        //                                                {
        //                                                    string Consecutivo, ErrorDescripcion, IdEntidad, Periodo, UnidadTiempoPeriodo, NumConsecutivo, Concepto,
        //                                                            Capital, TransferenciasCapital, ReservasRevaluacionBienes, ReservasOtrasreservas,
        //                                                            VariacionesNoAsignablesReservas, ResultadosAcumulados, Totales, InteresesMinoritarios, TotalPatrimonio;

        //                                                    txtError.Text = txtError.Text + "\r\n" + "Validación de los Datos del Cambio Patrimonio Neto...");
        //                                                    txtError.Text = txtError.Text + "\r\n" + "Consecutivo, ErrorDescripcion, IdEntidad, Periodo, UnidadTiempoPeriodo, NumConsecutivo, Concepto, Capital, TransferenciasCapital, ReservasRevaluacionBienes, ReservasOtrasreservas, VariacionesNoAsignablesReservas, ResultadosAcumulados, Totales, InteresesMinoritarios, TotalPatrimonio ");
        //                                                    for (int i = 0; i <= dsNICSPDatos.Tables["Table"].Rows.Count - 1; i++)
        //                                                    {
        //                                                        Consecutivo = dsNICSPDatos.Tables["Table"].Rows[i]["Consecutivo"].ToString();
        //                                                        ErrorDescripcion = dsNICSPDatos.Tables["Table"].Rows[i]["ErrorDescripcion"].ToString();
        //                                                        IdEntidad = dsNICSPDatos.Tables["Table"].Rows[i]["IdEntidad"].ToString();
        //                                                        Periodo = dsNICSPDatos.Tables["Table"].Rows[i]["Periodo"].ToString();
        //                                                        UnidadTiempoPeriodo = dsNICSPDatos.Tables["Table"].Rows[i]["UnidadTiempoPeriodo"].ToString();
        //                                                        NumConsecutivo = dsNICSPDatos.Tables["Table"].Rows[i]["NumConsecutivo"].ToString();
        //                                                        Concepto = dsNICSPDatos.Tables["Table"].Rows[i]["Concepto"].ToString();
        //                                                        Capital = dsNICSPDatos.Tables["Table"].Rows[i]["Capital"].ToString();
        //                                                        TransferenciasCapital = dsNICSPDatos.Tables["Table"].Rows[i]["TransferenciasCapital"].ToString();
        //                                                        ReservasRevaluacionBienes = dsNICSPDatos.Tables["Table"].Rows[i]["ReservasRevaluacionBienes"].ToString();
        //                                                        ReservasOtrasreservas = dsNICSPDatos.Tables["Table"].Rows[i]["ReservasOtrasreservas"].ToString();
        //                                                        VariacionesNoAsignablesReservas = dsNICSPDatos.Tables["Table"].Rows[i]["VariacionesNoAsignablesReservas"].ToString();
        //                                                        ResultadosAcumulados = dsNICSPDatos.Tables["Table"].Rows[i]["ResultadosAcumulados"].ToString();
        //                                                        Totales = dsNICSPDatos.Tables["Table"].Rows[i]["Totales"].ToString();
        //                                                        InteresesMinoritarios = dsNICSPDatos.Tables["Table"].Rows[i]["InteresesMinoritarios"].ToString();
        //                                                        TotalPatrimonio = dsNICSPDatos.Tables["Table"].Rows[i]["TotalPatrimonio"].ToString();

        //                                                        txtError.Text = txtError.Text + "\r\n" + Consecutivo + ", " + ErrorDescripcion + ", " + IdEntidad + ", " + Periodo + ", " + UnidadTiempoPeriodo + ", " + NumConsecutivo + ", " + Concepto + ", " + Capital + ", " + TransferenciasCapital + ", " + ReservasRevaluacionBienes + ", " + ReservasOtrasreservas + ", " + VariacionesNoAsignablesReservas + ", " + ResultadosAcumulados + ", " + Totales + ", " + InteresesMinoritarios + ", " + TotalPatrimonio);
        //                                                        DesplegarError(true);
        //                                                    }

        //                                                    if (DesplegarError(!Eliminar_ArchivoEstadoFinanciero(IdEstadoFinancieroArchivo)))
        //                                                    {
        //                                                        txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorBorrarArchivo"].ToString());
        //                                                    }

        //                                                    if (DesplegarError(!Eliminar_EstadoFinanciero(CodigoEstadosFinancieros_EstadoCambioPatrimonioNeto, strIdEntidad, int.Parse(strPeriodo), strUnidadTiempoPeriodo)))
        //                                                    {
        //                                                        txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorBorrarArchivo"].ToString());
        //                                                    }

        //                                                    DesplegarError(true);
        //                                                }
        //                                            }
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                            else
        //                            {
        //                                DesplegarError(true);
        //                                txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["ErrorSizeByteMaxTotalArchivos"].ToString().Replace("X", ((Double.Parse(ConfigurationManager.AppSettings["SizeByteMaxTotalArchivos"].ToString()) / 1024.00) / 1024.00).ToString()));
        //                            }
        //                        }
        //                        else
        //                        {
        //                            DesplegarError(true);
        //                            txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorDatosWebContraExcel"].ToString());
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (FlujoEfectivoFileUpload.Enabled)
        //                        {
        //                            DesplegarError(true);
        //                            txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorNoExisteArchivoCambioPatrimonioNeto"].ToString());
        //                        }
        //                    }

        //                    /*Balance Comprobacion*/
        //                    if (BalanceComprobacionFileUpload.FileContent.Length > 0)
        //                    {
        //                        strNombreArchivo = BalanceComprobacionFileUpload.FileName;
        //                        //14110T12015_ESTADO_BALANCE_COMPROBACION.xlsx

        //                        strInfoNombreArchivo = strNombreArchivo.Substring(0, strNombreArchivo.IndexOf("_"));
        //                        strExcelPeriodo = strInfoNombreArchivo.Substring(strInfoNombreArchivo.Length - 4, 4);

        //                        strExcelUnidadTiempoPeriodo = strInfoNombreArchivo.Substring(strInfoNombreArchivo.Length - 6, 2);
        //                        strInfoNombreArchivo = strInfoNombreArchivo.Replace(strExcelUnidadTiempoPeriodo + strExcelPeriodo, "");
        //                        strExcelIdEntidad = strInfoNombreArchivo;

        //                        if (strIdEntidad.Equals(strExcelIdEntidad) && strPeriodo.Equals(strExcelPeriodo) && strUnidadTiempoPeriodo.Equals(strExcelUnidadTiempoPeriodo))
        //                        {
        //                            if (validaSize(bufferBalanceComprobacion.Length, strIdEntidad, int.Parse(strPeriodo), strUnidadTiempoPeriodo))
        //                            {
        //                                dsNICSPDatos = WSINICSP_PC.InsertarArchivoEstadoFinancieroFileStream(bufferBalanceComprobacion, strIdEntidad, int.Parse(CodigoEstadosFinancieros_EstadoBalanceComprobacion),
        //                                              int.Parse(strPeriodo), strUnidadTiempoPeriodo, Path.GetFileNameWithoutExtension(strNombreArchivo),
        //                                              Path.GetExtension(strNombreArchivo), bufferBalanceComprobacion.Length, DateTime.Now, gstr_Usuario, "", gstr_Usuario, out Resultado, out Mensaje);

        //                                if (dsNICSPDatos.Tables.Count > 0)//Si hizo la consulta pero No retorno nada
        //                                //if (!dsNICSPDatosDropDownList.Tables["Table"].Rows.Count.Equals(0))//Si hizo la consulta pero No retorno nada
        //                                {
        //                                    for (int i = 0; i <= dsNICSPDatos.Tables["Table1"].Rows.Count - 1; i++)
        //                                    {
        //                                        IdEstadoFinancieroArchivo = dsNICSPDatos.Tables["Table1"].Rows[i]["IdEstadoFinancieroArchivo"].ToString();
        //                                        NombreArchivo = dsNICSPDatos.Tables["Table1"].Rows[i]["NombreArchivo"].ToString();
        //                                        TipoArchivo = dsNICSPDatos.Tables["Table1"].Rows[i]["TipoArchivo"].ToString();
        //                                    }
        //                                }

        //                                if (Resultado.Equals("01"))
        //                                    bCargaArchivo = true;

        //                                if (DesplegarError(!bCargaArchivo))
        //                                {
        //                                    if (DesplegarError(!Eliminar_ArchivoEstadoFinanciero(IdEstadoFinancieroArchivo)))
        //                                    {
        //                                        txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorBorrarArchivo"].ToString());
        //                                    }
        //                                    DesplegarError(true);
        //                                    txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorCargaArchivo"].ToString() + strNombreArchivo);
        //                                }
        //                                else
        //                                {
        //                                    VarDTSX = "/SET \\Package.Variables[User::IdArchivo].Properties[Value];\"" + IdEstadoFinancieroArchivo + "\" /SET \\Package.Variables[User::RutaDescargaArchivo].Properties[Value];\"" + DTSX_RutaDescargaArchivo + "\"";

        //                                    WSINICSP_PC.uwsEjecutarDTSX(DTSX_PaqueteURL, DTSX_DescargaArchivoEnServer, VarDTSX, Run64BitRuntime_DTSX_DescargaArchivoEnServer, "", gstr_Usuario, out Resultado, out Mensaje);

        //                                    if (DesplegarError(!Resultado.Equals("00")))
        //                                    {
        //                                        if (DesplegarError(!Eliminar_ArchivoEstadoFinanciero(IdEstadoFinancieroArchivo)))
        //                                        {
        //                                            txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorBorrarArchivo"].ToString());
        //                                        }
        //                                        DesplegarError(true);
        //                                        txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorCargaArchivo"].ToString() + " (Bajar en Carpeta) " + strNombreArchivo);
        //                                    }
        //                                    else
        //                                    {
        //                                        DTSX_CargaEstadoFinancieroBalanceComprobacion = ConfigurationManager.AppSettings["DTSX_CargaEstadoFinancieroBalanceComprobacion"].ToString();
        //                                        Run64BitRuntime_DTSX_CargaEstadoFinancieroBalanceComprobacion = bool.Parse(ConfigurationManager.AppSettings["Run64BitRuntime_DTSX_CargaEstadoFinancieroBalanceComprobacion"].ToString());

        //                                        //VarDTSX = "/SET \\Package.Variables[User::PathExcelFile].Properties[Value];\"L:\\SistemaGestor\\Archivos_SistemaGestor\\\\\" /SET \\Package.Variables[User::NameExcelFile].Properties[Value];\"" + NombreArchivo + TipoArchivo + "\" /SET \\Package.Variables[User::UsuarioCarga].Properties[Value];\"dmendez\"";
        //                                        VarDTSX = "/SET \\Package.Variables[User::PathExcelFile].Properties[Value];\"" + DTSX_RutaDescargaArchivo + "\" /SET \\Package.Variables[User::NameExcelFile].Properties[Value];\"" + NombreArchivo + TipoArchivo + "\" /SET \\Package.Variables[User::UsuarioCarga].Properties[Value];\"" + strUsuario + "\"";

        //                                        WSINICSP_PC.uwsEjecutarDTSX(DTSX_PaqueteURL, DTSX_CargaEstadoFinancieroBalanceComprobacion, VarDTSX, Run64BitRuntime_DTSX_CargaEstadoFinancieroBalanceComprobacion, "", gstr_Usuario, out Resultado, out Mensaje);
        //                                        ///*dsNICSPDatos*/ string asd = WSINICSP_PC.uwsPrueba2("", ""/*,out Resultado, out Mensaje*/);
        //                                        //dsNICSPDatos = WSINICSP_PC.ValidarEstadoFinancieroFlujoEfectivo(strIdEntidad, int.Parse(strPeriodo), strUnidadTiempoPeriodo, "", gstr_Usuario, out Resultado, out Mensaje);

        //                                        if (DesplegarError(!Resultado.Equals("00")))
        //                                        {
        //                                            if (DesplegarError(!Eliminar_ArchivoEstadoFinanciero(IdEstadoFinancieroArchivo)))
        //                                            {
        //                                                txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorBorrarArchivo"].ToString());
        //                                            }

        //                                            if (DesplegarError(!Eliminar_EstadoFinanciero(CodigoEstadosFinancieros_EstadoBalanceComprobacion, strIdEntidad, int.Parse(strPeriodo), strUnidadTiempoPeriodo)))
        //                                            {
        //                                                txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorBorrarArchivo"].ToString());
        //                                            }

        //                                            DesplegarError(true);
        //                                            txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorCargaArchivo"].ToString() + " (Bajar en Tabla) " + strNombreArchivo);
        //                                        }
        //                                        else
        //                                        {
        //                                            dsNICSPDatos = WSINICSP_PC.ValidarEstadoFinancieroBalanceComprobacion(strIdEntidad,
        //                                                int.Parse(strPeriodo), strUnidadTiempoPeriodo, "", gstr_Usuario, out Resultado, out Mensaje);

        //                                            if (DesplegarError(!Resultado.Equals("00")))
        //                                            {
        //                                                if (DesplegarError(!Eliminar_ArchivoEstadoFinanciero(IdEstadoFinancieroArchivo)))
        //                                                {
        //                                                    txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorBorrarArchivo"].ToString());
        //                                                }

        //                                                if (DesplegarError(!Eliminar_EstadoFinanciero(CodigoEstadosFinancieros_EstadoBalanceComprobacion, strIdEntidad, int.Parse(strPeriodo), strUnidadTiempoPeriodo)))
        //                                                {
        //                                                    txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorBorrarArchivo"].ToString());
        //                                                }

        //                                                DesplegarError(true);
        //                                                txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorValidaPeriodoActual"].ToString() + " (Bajar en Tabla) " + strNombreArchivo);
        //                                            }
        //                                            else
        //                                            {
        //                                                //if (dsNICSPDatos.Tables.Count > 0)//Si hizo la consulta pero No retorno nada
        //                                                if (!dsNICSPDatos.Tables["Table"].Rows.Count.Equals(0))//Si hizo la consulta pero No retorno nada
        //                                                {
        //                                                    string Consecutivo, ErrorDescripcion, IdEntidad, Periodo, UnidadTiempoPeriodo, NumConsecutivo, NumCuenta,
        //                                                        NumCuentaDepurada, SociedadGL, CuentaConcepto, SaldoInicial, DebitosPeriodo, CreditosPeriodo, SaldoFinal;

        //                                                    txtError.Text = txtError.Text + "\r\n" + "Validación de los Datos del Balance de Comprobación...");
        //                                                    txtError.Text = txtError.Text + "\r\n" + "Consecutivo, ErrorDescripcion, IdEntidad, Periodo, UnidadTiempoPeriodo, NumConsecutivo, NumCuenta, CuentaConcepto, MontoPeriodoActual, MontoPeriodoAnterior");
        //                                                    for (int i = 0; i <= dsNICSPDatos.Tables["Table"].Rows.Count - 1; i++)
        //                                                    {
        //                                                        Consecutivo = dsNICSPDatos.Tables["Table"].Rows[i]["Consecutivo"].ToString();
        //                                                        ErrorDescripcion = dsNICSPDatos.Tables["Table"].Rows[i]["ErrorDescripcion"].ToString();
        //                                                        IdEntidad = dsNICSPDatos.Tables["Table"].Rows[i]["IdEntidad"].ToString();
        //                                                        Periodo = dsNICSPDatos.Tables["Table"].Rows[i]["Periodo"].ToString();
        //                                                        UnidadTiempoPeriodo = dsNICSPDatos.Tables["Table"].Rows[i]["UnidadTiempoPeriodo"].ToString();
        //                                                        NumConsecutivo = dsNICSPDatos.Tables["Table"].Rows[i]["NumConsecutivo"].ToString();
        //                                                        NumCuenta = dsNICSPDatos.Tables["Table"].Rows[i]["NumCuenta"].ToString();
        //                                                        NumCuentaDepurada = dsNICSPDatos.Tables["Table"].Rows[i]["NumCuentaDepurada"].ToString();
        //                                                        SociedadGL = dsNICSPDatos.Tables["Table"].Rows[i]["SociedadGL"].ToString();
        //                                                        CuentaConcepto = dsNICSPDatos.Tables["Table"].Rows[i]["CuentaConcepto"].ToString();
        //                                                        SaldoInicial = dsNICSPDatos.Tables["Table"].Rows[i]["SaldoInicial"].ToString();
        //                                                        DebitosPeriodo = dsNICSPDatos.Tables["Table"].Rows[i]["DebitosPeriodo"].ToString();
        //                                                        CreditosPeriodo = dsNICSPDatos.Tables["Table"].Rows[i]["CreditosPeriodo"].ToString();
        //                                                        SaldoFinal = dsNICSPDatos.Tables["Table"].Rows[i]["SaldoFinal"].ToString();

        //                                                        txtError.Text = txtError.Text + "\r\n" + Consecutivo + ", " + ErrorDescripcion + ", " + IdEntidad + ", " + Periodo + ", " + UnidadTiempoPeriodo + ", " + NumConsecutivo + ", " + NumCuenta + ", " + NumCuentaDepurada + ", " + SociedadGL + ", " + CuentaConcepto + ", " + SaldoInicial + ", " + DebitosPeriodo + ", " + CreditosPeriodo + ", " + SaldoFinal);
        //                                                        DesplegarError(true);
        //                                                    }

        //                                                    if (DesplegarError(!Eliminar_ArchivoEstadoFinanciero(IdEstadoFinancieroArchivo)))
        //                                                    {
        //                                                        txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorBorrarArchivo"].ToString());
        //                                                    }

        //                                                    if (DesplegarError(!Eliminar_EstadoFinanciero(CodigoEstadosFinancieros_EstadoBalanceComprobacion, strIdEntidad, int.Parse(strPeriodo), strUnidadTiempoPeriodo)))
        //                                                    {
        //                                                        txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorBorrarArchivo"].ToString());
        //                                                    }
        //                                                }
        //                                            }
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                            else
        //                            {
        //                                DesplegarError(true);
        //                                txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["ErrorSizeByteMaxTotalArchivos"].ToString().Replace("X", ((Double.Parse(ConfigurationManager.AppSettings["SizeByteMaxTotalArchivos"].ToString()) / 1024.00) / 1024.00).ToString()));
        //                            }
        //                        }
        //                        else
        //                        {
        //                            DesplegarError(true);
        //                            txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorDatosWebContraExcel"].ToString());
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (BalanceComprobacionFileUpload.Enabled)
        //                        {
        //                            DesplegarError(true);
        //                            txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorNoExisteArchivoBalanceComprobacion"].ToString());
        //                        }
        //                    }

        //                    /*Estado Varios*/
        //                    if (EstadoVariosFileUpload.FileContent.Length > 0)
        //                    {
        //                        strNombreArchivo = EstadoVariosFileUpload.FileName;
        //                        //14110T12015_ESTADO_VARIOS.xlsx

        //                        strInfoNombreArchivo = strNombreArchivo.Substring(0, strNombreArchivo.IndexOf("_"));
        //                        strExcelPeriodo = strInfoNombreArchivo.Substring(strInfoNombreArchivo.Length - 4, 4);

        //                        strExcelUnidadTiempoPeriodo = strInfoNombreArchivo.Substring(strInfoNombreArchivo.Length - 6, 2);
        //                        strInfoNombreArchivo = strInfoNombreArchivo.Replace(strExcelUnidadTiempoPeriodo + strExcelPeriodo, "");
        //                        strExcelIdEntidad = strInfoNombreArchivo;

        //                        if (strIdEntidad.Equals(strExcelIdEntidad) && strPeriodo.Equals(strExcelPeriodo) && strUnidadTiempoPeriodo.Equals(strExcelUnidadTiempoPeriodo))
        //                        {
        //                            if (validaSize(bufferEstadoVarios.Length, strIdEntidad, int.Parse(strPeriodo), strUnidadTiempoPeriodo))
        //                            {
        //                                dsNICSPDatos = WSINICSP_PC.InsertarArchivoEstadoFinancieroFileStream(bufferEstadoVarios, strIdEntidad, int.Parse(CodigoEstadosFinancieros_EstadoVarios),
        //                                              int.Parse(strPeriodo), strUnidadTiempoPeriodo, Path.GetFileNameWithoutExtension(strNombreArchivo),
        //                                              Path.GetExtension(strNombreArchivo), bufferEstadoVarios.Length, DateTime.Now, gstr_Usuario, "", gstr_Usuario, out Resultado, out Mensaje);

        //                                if (dsNICSPDatos.Tables.Count > 0)//Si hizo la consulta pero No retorno nada
        //                                //if (!dsNICSPDatosDropDownList.Tables["Table"].Rows.Count.Equals(0))//Si hizo la consulta pero No retorno nada
        //                                {
        //                                    for (int i = 0; i <= dsNICSPDatos.Tables["Table1"].Rows.Count - 1; i++)
        //                                    {
        //                                        IdEstadoFinancieroArchivo = dsNICSPDatos.Tables["Table1"].Rows[i]["IdEstadoFinancieroArchivo"].ToString();
        //                                        NombreArchivo = dsNICSPDatos.Tables["Table1"].Rows[i]["NombreArchivo"].ToString();
        //                                        TipoArchivo = dsNICSPDatos.Tables["Table1"].Rows[i]["TipoArchivo"].ToString();
        //                                    }
        //                                }

        //                                if (Resultado.Equals("01"))
        //                                    bCargaArchivo = true;

        //                                if (DesplegarError(!bCargaArchivo))
        //                                {
        //                                    if (DesplegarError(!Eliminar_ArchivoEstadoFinanciero(IdEstadoFinancieroArchivo)))
        //                                    {
        //                                        txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorBorrarArchivo"].ToString());
        //                                    }
        //                                    DesplegarError(true);
        //                                    txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorCargaArchivo"].ToString() + strNombreArchivo);
        //                                }
        //                            }
        //                            else
        //                            {
        //                                DesplegarError(true);
        //                                txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["ErrorSizeByteMaxTotalArchivos"].ToString().Replace("X", ((Double.Parse(ConfigurationManager.AppSettings["SizeByteMaxTotalArchivos"].ToString()) / 1024.00) / 1024.00).ToString()));
        //                            }
        //                        }
        //                        else
        //                        {
        //                            DesplegarError(true);
        //                            txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorDatosWebContraExcel"].ToString());
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (EstadoVariosFileUpload.Enabled)
        //                        {
        //                            DesplegarError(true);
        //                            txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorNoExisteArchivoEstadoVarios"].ToString());
        //                        }
        //                    }

        //                    /*Notas Estados Financieros*/
        //                    if (NotasEstadosFinancierosFileUpload.FileContent.Length > 0)
        //                    {
        //                        strNombreArchivo = NotasEstadosFinancierosFileUpload.FileName;
        //                        //14110T12015_ESTADO_VARIOS.xlsx

        //                        strInfoNombreArchivo = strNombreArchivo.Substring(0, strNombreArchivo.IndexOf("_"));
        //                        strExcelPeriodo = strInfoNombreArchivo.Substring(strInfoNombreArchivo.Length - 4, 4);

        //                        strExcelUnidadTiempoPeriodo = strInfoNombreArchivo.Substring(strInfoNombreArchivo.Length - 6, 2);
        //                        strInfoNombreArchivo = strInfoNombreArchivo.Replace(strExcelUnidadTiempoPeriodo + strExcelPeriodo, "");
        //                        strExcelIdEntidad = strInfoNombreArchivo;

        //                        if (strIdEntidad.Equals(strExcelIdEntidad) && strPeriodo.Equals(strExcelPeriodo) && strUnidadTiempoPeriodo.Equals(strExcelUnidadTiempoPeriodo))
        //                        {
        //                            if (validaSize(bufferNotasEstadosFinancieros.Length, strIdEntidad, int.Parse(strPeriodo), strUnidadTiempoPeriodo))
        //                            {
        //                                dsNICSPDatos = WSINICSP_PC.InsertarArchivoEstadoFinancieroFileStream(bufferNotasEstadosFinancieros, strIdEntidad, int.Parse(CodigoEstadosFinancieros_ConsolidacionNotas),
        //                                              int.Parse(strPeriodo), strUnidadTiempoPeriodo, Path.GetFileNameWithoutExtension(strNombreArchivo),
        //                                              Path.GetExtension(strNombreArchivo), bufferNotasEstadosFinancieros.Length, DateTime.Now, gstr_Usuario, "", gstr_Usuario, out Resultado, out Mensaje);

        //                                if (dsNICSPDatos.Tables.Count > 0)//Si hizo la consulta pero No retorno nada
        //                                //if (!dsNICSPDatosDropDownList.Tables["Table"].Rows.Count.Equals(0))//Si hizo la consulta pero No retorno nada
        //                                {
        //                                    for (int i = 0; i <= dsNICSPDatos.Tables["Table1"].Rows.Count - 1; i++)
        //                                    {
        //                                        IdEstadoFinancieroArchivo = dsNICSPDatos.Tables["Table1"].Rows[i]["IdEstadoFinancieroArchivo"].ToString();
        //                                        NombreArchivo = dsNICSPDatos.Tables["Table1"].Rows[i]["NombreArchivo"].ToString();
        //                                        TipoArchivo = dsNICSPDatos.Tables["Table1"].Rows[i]["TipoArchivo"].ToString();
        //                                    }
        //                                }

        //                                if (Resultado.Equals("01"))
        //                                    bCargaArchivo = true;

        //                                if (DesplegarError(!bCargaArchivo))
        //                                {
        //                                    if (DesplegarError(!Eliminar_ArchivoEstadoFinanciero(IdEstadoFinancieroArchivo)))
        //                                    {
        //                                        txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorBorrarArchivo"].ToString());
        //                                    }
        //                                    DesplegarError(true);
        //                                    txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorCargaArchivo"].ToString() + strNombreArchivo);
        //                                }
        //                            }
        //                            else
        //                            {
        //                                DesplegarError(true);
        //                                txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["ErrorSizeByteMaxTotalArchivos"].ToString().Replace("X", ((Double.Parse(ConfigurationManager.AppSettings["SizeByteMaxTotalArchivos"].ToString()) / 1024.00) / 1024.00).ToString()));
        //                            }
        //                        }
        //                        else
        //                        {
        //                            DesplegarError(true);
        //                            txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorDatosWebContraExcel"].ToString());
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (NotasEstadosFinancierosFileUpload.Enabled)
        //                        {
        //                            DesplegarError(true);
        //                            txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorNoExisteArchivoNotasEstadosFinancieros"].ToString());
        //                        }
        //                    }



        //                    if (!lsbError.Items.Count.Equals(0))
        //                    {
        //                        DesplegarError(true);
        //                    }
        //                }
        //                else
        //                {
        //                    txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorValidaPeriodoActual2"].ToString());
        //                    txtError.Text = txtError.Text + "\r\n" + Descripcion);
        //                    DesplegarError(true);
        //                }
        //            }
        //            else
        //            {
        //                DesplegarError(true);
        //                txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorValidaPeriodoActual"].ToString());
        //            }

        //        }
        //        else
        //        {
        //            DesplegarError(true);
        //            txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorParametros"].ToString());
        //        }



        //        ConsultarEstadosFinancierosCargados_GridView();

        //    }
        //    catch (Exception ex)
        //    {
        //        #region MensajeError
        //        EventLog.WriteEntry(ConfigurationManager.AppSettings["EventLogSource"].ToString(),
        //            //Obtiene el nombre de la clase.
        //        "NICSP"
        //            //Nombre del método.
        //        + "." + MethodInfo.GetCurrentMethod().Name
        //            //Error especifico.
        //        + ": Excepcion  " + ex.Message.ToString() + ". ",
        //        EventLogEntryType.Error);
        //        #endregion

        //        throw (ex);
        //    }
        //    finally
        //    {
        //        dsNICSPDatos = null;
        //        WSINICSP_PC = null;
        //        bufferFlujoEfectivo = null;
        //        bufferCambioPatrimonioNeto = null;
        //        bufferNotasEstadosFinancieros = null;
        //        bufferBalanceComprobacion = null;
        //        EstadoVariosFileUpload = null;
        //    }
        //}

        public Boolean validaSize(int NewFileSizeByte, string strIdIdentidad, int nPeriodo, string strUnidadTiempoPeriodo)
        {

            DataSet dsNICSPDatos = new DataSet();
            wsPlantillasConsolidacion WSINICSP_PC = new wsPlantillasConsolidacion();

            try
            {
                Boolean SizeValido = false;
                string Resultado, Mensaje;
                dsNICSPDatos = WSINICSP_PC.BuscarArchivoEstadoFinancieroTamanoByte(strIdIdentidad, nPeriodo, strUnidadTiempoPeriodo, "", gstr_Usuario, out Resultado, out Mensaje);

                int numSizeByteArchivo = 0;

                if (dsNICSPDatos.Tables.Count > 0)//Si hizo la consulta pero No retorno nada
                //if (!dsNICSPDatos.Tables["Table"].Rows.Count.Equals(0))//Si hizo la consulta pero No retorno nada
                {
                    for (int i = 0; i <= dsNICSPDatos.Tables["Table"].Rows.Count - 1; i++)
                    {
                        numSizeByteArchivo = int.Parse(dsNICSPDatos.Tables["Table"].Rows[i]["TamanoByteArchivo"].ToString());
                    }
                }

                //numSizeByteArchivo = numSizeByteArchivo + NewFileSizeByte;
                numSizeByteArchivo = NewFileSizeByte;
                //5242880 bites = 5 megas -->
                if (numSizeByteArchivo <= int.Parse(ConfigurationManager.AppSettings["SizeByteMaxTotalArchivos"].ToString()))
                {
                    SizeValido = true;
                }
                return SizeValido;

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

        //protected void CargarArchivosAnexosButton_Click(object sender, EventArgs e)
        //{
        //    DataSet dsNICSPDatos = new DataSet();
        //    wsPlantillasConsolidacion WSINICSP_PC = new wsPlantillasConsolidacion();

        //    byte[] bufferArchivoAnexo = new byte[(int)ArchivoAnexoFileUpload.FileContent.Length];
        //    FlujoEfectivoFileUpload.FileContent.Read(bufferArchivoAnexo, 0, bufferArchivoAnexo.Length);

        //    try
        //    {
        //        string strUsuario, strIdEntidad, strPeriodo, strUnidadTiempoPeriodo, strEstadoFinanciero;
        //        string strNombreArchivo;
        //        bool bCargaArchivo = false;
        //        string Resultado, Mensaje;
        //        string IdEstadoFinancieroArchivo = "", NombreArchivo = "", TipoArchivo = "";

        //        strUsuario = gstr_Usuario;
        //        strIdEntidad = CatalogoEntidadesDropDownList.SelectedValue;
        //        strPeriodo = PeriodoDropDownList.SelectedItem.Text;
        //        strUnidadTiempoPeriodo = UnidadTiempoPeriodoDropDownList.SelectedValue;
        //        strEstadoFinanciero = EstadoFinancieroDropDownList.SelectedValue;

        //        /**/

        //        /*Archivo Anexo*/
        //        if (ArchivoAnexoFileUpload.FileContent.Length > 0)
        //        {
        //            strNombreArchivo = ArchivoAnexoFileUpload.FileName;
        //            //abc.dfg

        //            //strInfoNombreArchivo = strNombreArchivo.Substring(0, strNombreArchivo.IndexOf("_"));
        //            //strExcelPeriodo = strInfoNombreArchivo.Substring(strInfoNombreArchivo.Length - 4, 4);

        //            //strExcelUnidadTiempoPeriodo = strInfoNombreArchivo.Substring(strInfoNombreArchivo.Length - 6, 2);
        //            //strInfoNombreArchivo = strInfoNombreArchivo.Replace(strExcelUnidadTiempoPeriodo + strExcelPeriodo, "");
        //            //strExcelIdEntidad = strInfoNombreArchivo;

        //            if ((!strIdEntidad.Equals(string.Empty)) && (!strPeriodo.Equals("Seleccione ítem")) && (!strUnidadTiempoPeriodo.Equals(string.Empty)) && (!strEstadoFinanciero.Equals(string.Empty)))
        //            {
        //                if (validaSize(bufferArchivoAnexo.Length, strIdEntidad, int.Parse(strPeriodo), strUnidadTiempoPeriodo))
        //                {
        //                    dsNICSPDatos = WSINICSP_PC.InsertarArchivoAnexoEstadoFinancieroFilestream(bufferArchivoAnexo, strIdEntidad, int.Parse(strEstadoFinanciero),
        //                        int.Parse(strPeriodo), strUnidadTiempoPeriodo, Path.GetFileNameWithoutExtension(strNombreArchivo),
        //                        Path.GetExtension(strNombreArchivo), bufferArchivoAnexo.Length, DateTime.Now, "", gstr_Usuario, out Resultado, out Mensaje);

        //                    if (dsNICSPDatos.Tables.Count > 0)//Si hizo la consulta pero No retorno nada
        //                    //if (!dsNICSPDatosDropDownList.Tables["Table"].Rows.Count.Equals(0))//Si hizo la consulta pero No retorno nada
        //                    {
        //                        for (int i = 0; i <= dsNICSPDatos.Tables["Table1"].Rows.Count - 1; i++)
        //                        {
        //                            IdEstadoFinancieroArchivo = dsNICSPDatos.Tables["Table1"].Rows[i]["IdEstadoFinancieroArchivoAnexo"].ToString();
        //                            NombreArchivo = dsNICSPDatos.Tables["Table1"].Rows[i]["NombreArchivo"].ToString();
        //                            TipoArchivo = dsNICSPDatos.Tables["Table1"].Rows[i]["TipoArchivo"].ToString();
        //                        }
        //                    }

        //                    if (Resultado.Equals("01"))
        //                        bCargaArchivo = true;

        //                    if (DesplegarError(!bCargaArchivo))
        //                    {
        //                        if (DesplegarError(!Eliminar_ArchivoEstadoFinanciero(IdEstadoFinancieroArchivo)))
        //                        {
        //                            txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorBorrarArchivo"].ToString());
        //                        }
        //                        DesplegarError(true);
        //                        txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorCargaArchivo"].ToString() + strNombreArchivo);
        //                    }
        //                }
        //                else
        //                {
        //                    DesplegarError(true);
        //                    txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["ErrorSizeByteMaxTotalArchivos"].ToString().Replace("X", ((Double.Parse(ConfigurationManager.AppSettings["SizeByteMaxTotalArchivos"].ToString()) / 1024.00) / 1024.00).ToString()));
        //                }
        //            }
        //            else
        //            {
        //                DesplegarError(true);
        //                txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorParametros"].ToString());
        //            }
        //        }
        //        else
        //        {
        //            if (ArchivoAnexoFileUpload.Enabled)
        //            {
        //                DesplegarError(true);
        //                txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorNoExisteArchivoNotasEstadosFinancieros"].ToString());
        //            }
        //        }

        //        if (!lsbError.Items.Count.Equals(0))
        //        {
        //            DesplegarError(true);
        //        }

        //        ConsultarArchivosAnexosEstadosFinancierosCargados_GridView();

        //    }
        //    catch (Exception ex)
        //    {
        //        #region MensajeError
        //        EventLog.WriteEntry(ConfigurationManager.AppSettings["EventLogSource"].ToString(),
        //            //Obtiene el nombre de la clase.
        //        "NICSP"
        //            //Nombre del método.
        //        + "." + MethodInfo.GetCurrentMethod().Name
        //            //Error especifico.
        //        + ": Excepcion  " + ex.Message.ToString() + ". ",
        //        EventLogEntryType.Error);
        //        #endregion

        //        throw (ex);
        //    }
        //    finally
        //    {
        //        dsNICSPDatos = null;
        //        WSINICSP_PC = null;
        //        bufferArchivoAnexo = null;
        //    }
        //}

        public bool VisibleParametrosPrincipales(bool Visible)
        {
            try
            {
                AmbitoConsolidacionLabel.Visible = Visible;
                AmbitoConsolidacionDropDownList.Visible = Visible;
                CatalogoEntidadesLabel.Visible = Visible;
                CatalogoEntidadesDropDownList.Visible = Visible;
                PeriodoLabel.Visible = Visible;
                PeriodoDropDownList.Visible = Visible;
                UnidadTiempoPeriodoLabel.Visible = Visible;
                UnidadTiempoPeriodoDropDownList.Visible = Visible;
                InfoCargadaEstadosFinancierosArchivosButton.Visible = Visible;

                return Visible;
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
        }

        //protected void ArchivosPlantillasButton_Click(object sender, EventArgs e)
        //{
        //    DataSet dsNICSPDatos = new DataSet();
        //    wsPlantillasConsolidacion WSINICSP_PC = new wsPlantillasConsolidacion();

        //    byte[] bufferArchivosPlantillas = new byte[(int)ArchivosPlantillasFileUpload.FileContent.Length];
        //    ArchivosPlantillasFileUpload.FileContent.Read(bufferArchivosPlantillas, 0, bufferArchivosPlantillas.Length);

        //    try
        //    {
        //        string strUsuario/*, strIdEntidad, strPeriodo, strUnidadTiempoPeriodo*/, strEstadoFinanciero;
        //        string strNombreArchivo, Resultado, Mensaje;
        //        string IdEstadoFinancieroArchivo = "", NombreArchivo = "", TipoArchivo = "";
        //        bool bCargaArchivo = false;

        //        strUsuario = gstr_Usuario;
        //        strEstadoFinanciero = EstadoFinancieroPlantillasDropDownList.SelectedValue;

        //        /*Archivos Plantillas*/
        //        if (ArchivosPlantillasFileUpload.FileContent.Length > 0)
        //        {
        //            strNombreArchivo = ArchivosPlantillasFileUpload.FileName;
        //            //abc.dfg

        //            if ((!strEstadoFinanciero.Equals(string.Empty)))
        //            {
        //                dsNICSPDatos = WSINICSP_PC.InsertarArchivoPlantillaEstadoFinancieroFilestream(bufferArchivosPlantillas, int.Parse(strEstadoFinanciero),
        //                    Path.GetFileNameWithoutExtension(strNombreArchivo), Path.GetExtension(strNombreArchivo), DateTime.Now,
        //                    gstr_Usuario, "", gstr_Usuario, out Resultado, out Mensaje);

        //                if (dsNICSPDatos.Tables.Count > 0)//Si hizo la consulta pero No retorno nada
        //                //if (!dsNICSPDatosDropDownList.Tables["Table"].Rows.Count.Equals(0))//Si hizo la consulta pero No retorno nada
        //                {
        //                    for (int i = 0; i <= dsNICSPDatos.Tables["Table1"].Rows.Count - 1; i++)
        //                    {
        //                        IdEstadoFinancieroArchivo = dsNICSPDatos.Tables["Table1"].Rows[i]["IdEstadoFinancieroArchivoPlantilla"].ToString();
        //                        NombreArchivo = dsNICSPDatos.Tables["Table1"].Rows[i]["NombreArchivo"].ToString();
        //                        TipoArchivo = dsNICSPDatos.Tables["Table1"].Rows[i]["TipoArchivo"].ToString();
        //                    }
        //                }

        //                if (Resultado.Equals("01"))
        //                    bCargaArchivo = true;

        //                if (DesplegarError(!bCargaArchivo))
        //                {
        //                    if (DesplegarError(!Eliminar_ArchivoPlantillasEstadoFinanciero(IdEstadoFinancieroArchivo)))
        //                    {
        //                        txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorBorrarArchivo"].ToString());
        //                    }
        //                    DesplegarError(true);
        //                    txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorCargaArchivo"].ToString() + strNombreArchivo);
        //                }
        //            }
        //            else
        //            {
        //                DesplegarError(true);
        //                txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorParametros"].ToString());
        //            }
        //        }
        //        else
        //        {
        //            if (ArchivoAnexoFileUpload.Enabled)
        //            {
        //                DesplegarError(true);
        //                txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorNoExisteArchivoPlantillasEstadosFinancieros"].ToString());
        //            }
        //        }

        //        if (!lsbError.Items.Count.Equals(0))
        //        {
        //            DesplegarError(true);
        //        }

        //        /**/

        //        ConsultarArchivosPlantillasEstadosFinancierosCargados_GridView();

        //    }
        //    catch (Exception ex)
        //    {
        //        #region MensajeError
        //        EventLog.WriteEntry(ConfigurationManager.AppSettings["EventLogSource"].ToString(),
        //            //Obtiene el nombre de la clase.
        //        "NICSP"
        //            //Nombre del método.
        //        + "." + MethodInfo.GetCurrentMethod().Name
        //            //Error especifico.
        //        + ": Excepcion  " + ex.Message.ToString() + ". ",
        //        EventLogEntryType.Error);
        //        #endregion

        //        throw (ex);
        //    }
        //    finally
        //    {
        //        dsNICSPDatos = null;
        //        WSINICSP_PC = null;
        //        bufferArchivosPlantillas = null;
        //    }
        //}

        protected void CorreosAutorizacionButton_Click(object sender, EventArgs e)
        {
            DataSet dsNICSPDatos = new DataSet();
            wsPlantillasConsolidacion WSINICSP_PC = new wsPlantillasConsolidacion();

            byte[] bufferCorreosAutorizacion = new byte[(int)CorreosAutorizacionFileUpload.FileContent.Length];
            CorreosAutorizacionFileUpload.FileContent.Read(bufferCorreosAutorizacion, 0, bufferCorreosAutorizacion.Length);

            try
            {
                string strUsuario, strIdEntidad, strPeriodo, strUnidadTiempoPeriodo, strEstadoFinanciero;

                string strNombreArchivo, Resultado, Mensaje;
                string IdEstadoFinancieroArchivo = "", NombreArchivo = "", TipoArchivo = "";

                bool bCargaArchivo = false;

                strUsuario = gstr_Usuario;
                strIdEntidad = CatalogoEntidadesDropDownList.SelectedValue;
                strPeriodo = PeriodoDropDownList.SelectedItem.Text;
                strUnidadTiempoPeriodo = UnidadTiempoPeriodoDropDownList.SelectedValue;
                strEstadoFinanciero = ConfigurationManager.AppSettings["CodigoEstadosFinancieros_CorreoAutorizacion"].ToString();

                /*Correo Autorizacion*/
                if (CorreosAutorizacionFileUpload.FileContent.Length > 0)
                {
                    strNombreArchivo = CorreosAutorizacionFileUpload.FileName;
                    //abc.dfg

                    if ((!strIdEntidad.Equals(string.Empty)) && (!strPeriodo.Equals("Seleccione ítem")) && (!strUnidadTiempoPeriodo.Equals(string.Empty)) && (!strEstadoFinanciero.Equals(string.Empty)))
                    {
                        if (validaSize(bufferCorreosAutorizacion.Length, strIdEntidad, int.Parse(strPeriodo), strUnidadTiempoPeriodo))
                        {
                            dsNICSPDatos = WSINICSP_PC.InsertarArchivoAnexoEstadoFinancieroFilestream(bufferCorreosAutorizacion, strIdEntidad, int.Parse(strEstadoFinanciero),
                                int.Parse(strPeriodo), strUnidadTiempoPeriodo, Path.GetFileNameWithoutExtension(strNombreArchivo),
                                Path.GetExtension(strNombreArchivo), bufferCorreosAutorizacion.Length, DateTime.Now, "", gstr_Usuario, out Resultado, out Mensaje);

                            if (dsNICSPDatos.Tables.Count > 0)//Si hizo la consulta pero No retorno nada
                            //if (!dsNICSPDatosDropDownList.Tables["Table"].Rows.Count.Equals(0))//Si hizo la consulta pero No retorno nada
                            {
                                for (int i = 0; i <= dsNICSPDatos.Tables["Table1"].Rows.Count - 1; i++)
                                {
                                    IdEstadoFinancieroArchivo = dsNICSPDatos.Tables["Table1"].Rows[i]["IdEstadoFinancieroArchivoAnexo"].ToString();
                                    NombreArchivo = dsNICSPDatos.Tables["Table1"].Rows[i]["NombreArchivo"].ToString();
                                    TipoArchivo = dsNICSPDatos.Tables["Table1"].Rows[i]["TipoArchivo"].ToString();
                                }
                            }

                            if (Resultado.Equals("01"))
                                bCargaArchivo = true;

                            if (DesplegarError(!bCargaArchivo))
                            {
                                if (DesplegarError(!Eliminar_ArchivoEstadoFinanciero(IdEstadoFinancieroArchivo)))
                                {
                                    txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorBorrarArchivo"].ToString();
                                }
                                DesplegarError(true);
                                txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorCargaArchivo"].ToString() + strNombreArchivo;
                            }

                        }
                        else
                        {
                            DesplegarError(true);
                            txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["ErrorSizeByteMaxTotalArchivos"].ToString().Replace("X", ((Double.Parse(ConfigurationManager.AppSettings["SizeByteMaxTotalArchivos"].ToString()) / 1024.00) / 1024.00).ToString());
                        }
                    }
                    else
                    {
                        DesplegarError(true);
                        txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorParametros"].ToString();
                    }
                }
                else
                {
                    if (CorreosAutorizacionFileUpload.Enabled) //if (ArchivoAnexoFileUpload.Enabled)
                    {
                        DesplegarError(true);
                        txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorNoExisteArchivoAnexosEstadosFinancieros"].ToString();
                    }
                }

                if (!txtError.Text.Length.Equals(0))
                {
                    DesplegarError(true);
                }

                ConsultarCorreosAutorizacionEstadosFinancierosCargados_GridView();
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
                bufferCorreosAutorizacion = null;
            }
        }

        protected void InfoCargadaEstadosFinancierosArchivosButton_Click(object sender, EventArgs e)
        {
            try
            {
                ConsultarEstadosFinancierosCargados_GridView();
                ConsultarArchivosAnexosEstadosFinancierosCargados_GridView();
                ConsultarArchivosPlantillasEstadosFinancierosCargados_GridView();
                ConsultarCorreosAutorizacionEstadosFinancierosCargados_GridView();
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

        protected void RevisionInstitucionButton_Click(object sender, EventArgs e)
        {
            DataSet dsNICSPDatos = new DataSet();
            wsPlantillasConsolidacion WSINICSP_PC = new wsPlantillasConsolidacion();
            try
            {
                string strUsuario, strIdEntidad, strPeriodo, strUnidadTiempoPeriodo;

                string Resultado, Mensaje, NotaRazon = "", CodigoCatalogoEtapasEstadoFinancieroUsuarioInstitucion, CodigoCatalogoEtapasEstadoFinancieroEnTramiteRevisionInstitucion;

                strUsuario = gstr_Usuario;
                strIdEntidad = CatalogoEntidadesDropDownList.SelectedValue;
                strPeriodo = PeriodoDropDownList.SelectedItem.Text;
                strUnidadTiempoPeriodo = UnidadTiempoPeriodoDropDownList.SelectedValue;

                CodigoCatalogoEtapasEstadoFinancieroUsuarioInstitucion = ConfigurationManager.AppSettings["CodigoCatalogoEtapasEstadoFinanciero_UsuarioInstitucion"].ToString();
                CodigoCatalogoEtapasEstadoFinancieroEnTramiteRevisionInstitucion = ConfigurationManager.AppSettings["CodigoCatalogoEtapasEstadoFinanciero_EnTramiteRevisionInstitucion"].ToString();

                bool bCambioEstado = false;
                if ((!strIdEntidad.Equals(string.Empty)) && (!strPeriodo.Equals("Seleccione ítem")) && (!strUnidadTiempoPeriodo.Equals(string.Empty)))
                {

                    bCambioEstado = WSINICSP_PC.InsertarEtapaEstadoFinanciero(strIdEntidad, int.Parse(strPeriodo), strUnidadTiempoPeriodo, int.Parse(CodigoCatalogoEtapasEstadoFinancieroUsuarioInstitucion),
                    NotaRazon, gstr_Usuario, DateTime.Now, "", gstr_Usuario, out Resultado, out Mensaje);

                    if (DesplegarError(!bCambioEstado))
                    {
                        txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorCambioEstado"].ToString() + ". Etapa:" + CodigoCatalogoEtapasEstadoFinancieroUsuarioInstitucion;
                    }
                    else
                    {
                        bCambioEstado = WSINICSP_PC.InsertarEtapaEstadoFinanciero(strIdEntidad, int.Parse(strPeriodo), strUnidadTiempoPeriodo, int.Parse(CodigoCatalogoEtapasEstadoFinancieroEnTramiteRevisionInstitucion),
                        NotaRazon, gstr_Usuario, DateTime.Now, "", gstr_Usuario, out Resultado, out Mensaje);

                        if (DesplegarError(!bCambioEstado))
                        {
                            txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorCambioEstado"].ToString() + ". Etapa:" + CodigoCatalogoEtapasEstadoFinancieroEnTramiteRevisionInstitucion;
                        }
                        else
                        {
                            //RevisionInstitucionButton.
                        }
                    }
                }
                else
                {
                    DesplegarError(true);
                    txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorParametros"].ToString();
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
                dsNICSPDatos = null;
                WSINICSP_PC = null;
            }
        }

        protected void RechazadoInstitucionButton_Click(object sender, EventArgs e)
        {
            DataSet dsNICSPDatos = new DataSet();
            wsPlantillasConsolidacion WSINICSP_PC = new wsPlantillasConsolidacion();
            try
            {
                gstr_Institucion = clsSesion.Current.SociedadUsr;
                gstr_Usuario = clsSesion.Current.LoginUsuario;

                string strUsuario, strIdEntidad, strPeriodo, strUnidadTiempoPeriodo;

                string Resultado, Mensaje, NotaRazon = "", CodigoCatalogoEtapasEstadoFinancieroRechazadoInstitucion, CodigoCatalogoEtapasEstadoFinancieroUsuarioInstitucion;

                strUsuario = gstr_Usuario;
                strIdEntidad = CatalogoEntidadesDropDownList.SelectedValue;
                strPeriodo = PeriodoDropDownList.SelectedItem.Text;
                strUnidadTiempoPeriodo = UnidadTiempoPeriodoDropDownList.SelectedValue;

                CodigoCatalogoEtapasEstadoFinancieroRechazadoInstitucion = ConfigurationManager.AppSettings["CodigoCatalogoEtapasEstadoFinanciero_RechazadoInstitucion"].ToString();
                CodigoCatalogoEtapasEstadoFinancieroUsuarioInstitucion = ConfigurationManager.AppSettings["CodigoCatalogoEtapasEstadoFinanciero_UsuarioInstitucion"].ToString();

                bool bCambioEstado = false;
                if ((!strIdEntidad.Equals(string.Empty)) && (!strPeriodo.Equals("Seleccione ítem")) && (!strUnidadTiempoPeriodo.Equals(string.Empty)))
                {

                    bCambioEstado = WSINICSP_PC.InsertarEtapaEstadoFinanciero(strIdEntidad, int.Parse(strPeriodo), strUnidadTiempoPeriodo, int.Parse(CodigoCatalogoEtapasEstadoFinancieroRechazadoInstitucion),
                    NotaRazon, gstr_Usuario, DateTime.Now, "", gstr_Usuario, out Resultado, out Mensaje);

                    //este codigo es para atrasar un segundo el registro de las etapas 4 y 1
                    Thread.Sleep(1000);
                    //**************************************************************************************

                    if (DesplegarError(!bCambioEstado))
                    {
                        txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorCambioEstado"].ToString() + ". Etapa:" + CodigoCatalogoEtapasEstadoFinancieroRechazadoInstitucion;
                    }
                    else
                    {
                        bCambioEstado = WSINICSP_PC.InsertarEtapaEstadoFinanciero(strIdEntidad, int.Parse(strPeriodo), strUnidadTiempoPeriodo, int.Parse(CodigoCatalogoEtapasEstadoFinancieroUsuarioInstitucion),
                        NotaRazon, gstr_Usuario, DateTime.Now, "", gstr_Usuario, out Resultado, out Mensaje);


                        if (DesplegarError(!bCambioEstado))
                        {
                            txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorCambioEstado"].ToString() + ". Etapa:" + CodigoCatalogoEtapasEstadoFinancieroUsuarioInstitucion;
                        }
                        else
                        {

                            DesplegarError(true);
                            txtError.Text = txtError.Text + "\r\n" + " Enviado a Revisión. ";

                            AprobarInstitucionButton.Enabled = false;
                            RechazadoInstitucionButton.Enabled = false;

                            //Envia correo
                            string str_CorreoFrom = "", str_CorreoTo = "", str_NombreUsuario = "";
                            string str_IDUsuario = gstr_Usuario;
                            string str_RolTo = ConfigurationManager.AppSettings["IdRolCargaEntidad"].ToString();
                            string str_Asunto = ConfigurationManager.AppSettings["CorreoAsuntoRechazoInstitucion"].ToString();
                            string str_Mensaje = ConfigurationManager.AppSettings["CorreoRechazo_RevisionInstitucion_Usuario"].ToString();
                            string Anexo_UsuarioCorreo = ConfigurationManager.AppSettings["Anexo_UsuarioCorreo"].ToString();                            
                            bool bErrorCorreo = false;

                            str_Mensaje = str_Mensaje.Replace("XXXXXXX", gstr_Institucion);
                            dsNICSPDatos = WSINICSP_PC.BuscarUsuario(gstr_Usuario, "", gstr_Usuario, out Resultado, out Mensaje);

                            if (dsNICSPDatos.Tables.Count > 0)//Si hizo la consulta pero No retorno nada
                            //if (!dsNICSPDatosDropDownList.Tables["Table"].Rows.Count.Equals(0))//Si hizo la consulta pero No retorno nada
                            {
                                for (int i = 0; i <= dsNICSPDatos.Tables["Table"].Rows.Count - 1; i++)
                                {
                                    str_CorreoFrom = dsNICSPDatos.Tables["Table"].Rows[i]["CorreoUsuario"].ToString();
                                    str_NombreUsuario = dsNICSPDatos.Tables["Table"].Rows[i]["NomUsuario"].ToString();
                                }
                                bErrorCorreo = true;

                                Anexo_UsuarioCorreo = Anexo_UsuarioCorreo.Replace("XXXXX", str_NombreUsuario);
                                Anexo_UsuarioCorreo = Anexo_UsuarioCorreo.Replace("YYYYY", str_CorreoFrom);

                                str_Mensaje = str_Mensaje + Anexo_UsuarioCorreo;
                            }
                            else
                            {
                                bErrorCorreo = false;
                            }

                            dsNICSPDatos = WSINICSP_PC.BuscarUsuariosPorRol(int.Parse(str_RolTo), gstr_Institucion, "", gstr_Usuario, out Resultado, out Mensaje);

                            if (dsNICSPDatos.Tables.Count > 0)//Si hizo la consulta pero No retorno nada
                            //if (!dsNICSPDatosDropDownList.Tables["Table"].Rows.Count.Equals(0))//Si hizo la consulta pero No retorno nada
                            {
                                for (int i = 0; i <= dsNICSPDatos.Tables["Table"].Rows.Count - 1; i++)
                                {
                                    str_CorreoTo = str_CorreoTo + dsNICSPDatos.Tables["Table"].Rows[i]["CorreoUsuario"].ToString() + ",";
                                    
                                }
                                str_CorreoTo = str_CorreoTo.Substring(0, str_CorreoTo.Length - 1);
                                bErrorCorreo = true;
                            }
                            else
                            {
                                bErrorCorreo = false;
                            }


                            if (bErrorCorreo)
                                WSINICSP_PC.EnviarCorreoPC(int.Parse(ConfigurationManager.AppSettings["CorreoClientePort"].ToString()), ConfigurationManager.AppSettings["CorreoClienteHost"].ToString(), ConfigurationManager.AppSettings["CorreoNetworkCredentialUsuario"].ToString(), ConfigurationManager.AppSettings["CorreoNetworkCredentialPassWord"].ToString(), ConfigurationManager.AppSettings["UsuarioSistemaConsolidacion"].ToString(), str_CorreoTo, str_CorreoFrom, str_Mensaje, str_Asunto);

                            if (!bErrorCorreo)
                            {

                                bErrorCorreo = false;
                                DesplegarError(true);
                                txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorCorreo"].ToString();

                            }
                        }
                    }
                }
                else
                {
                    DesplegarError(true);
                    txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorParametros"].ToString();
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
                dsNICSPDatos = null;
                WSINICSP_PC = null;
            }
        }

        protected void AprobarInstitucionButton_Click(object sender, EventArgs e)
        {
            DataSet dsNICSPDatos = new DataSet();
            wsPlantillasConsolidacion WSINICSP_PC = new wsPlantillasConsolidacion();
            try
            {
                string strUsuario, strIdEntidad, strPeriodo, strUnidadTiempoPeriodo;

                string Resultado, Mensaje, NotaRazon = "", CodigoCatalogoEtapasEstadoFinancieroAprobadoInstitucion, CodigoCatalogoEtapasEstadoFinancieroEnTramiteRevisionAnalista;



                strUsuario = gstr_Usuario;
                strIdEntidad = CatalogoEntidadesDropDownList.SelectedValue;
                strPeriodo = PeriodoDropDownList.SelectedItem.Text;
                strUnidadTiempoPeriodo = UnidadTiempoPeriodoDropDownList.SelectedValue;

                CodigoCatalogoEtapasEstadoFinancieroAprobadoInstitucion = ConfigurationManager.AppSettings["CodigoCatalogoEtapasEstadoFinanciero_AprobadoInstitucion"].ToString();
                CodigoCatalogoEtapasEstadoFinancieroEnTramiteRevisionAnalista = ConfigurationManager.AppSettings["CodigoCatalogoEtapasEstadoFinanciero_EnTramiteRevisionAnalista"].ToString();

                bool bCambioEstado = false;
                if ((!strIdEntidad.Equals(string.Empty)) && (!strPeriodo.Equals("Seleccione ítem")) && (!strUnidadTiempoPeriodo.Equals(string.Empty)))
                {

                    bCambioEstado = WSINICSP_PC.InsertarEtapaEstadoFinanciero(strIdEntidad, int.Parse(strPeriodo), strUnidadTiempoPeriodo, int.Parse(CodigoCatalogoEtapasEstadoFinancieroAprobadoInstitucion),
                    NotaRazon, gstr_Usuario, DateTime.Now, "", gstr_Usuario, out Resultado, out Mensaje);

                    if (DesplegarError(!bCambioEstado))
                    {
                        txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorCambioEstado"].ToString() + ". Etapa:" + CodigoCatalogoEtapasEstadoFinancieroAprobadoInstitucion;
                    }
                    else
                    {
                        bCambioEstado = WSINICSP_PC.InsertarEtapaEstadoFinanciero(strIdEntidad, int.Parse(strPeriodo), strUnidadTiempoPeriodo, int.Parse(CodigoCatalogoEtapasEstadoFinancieroEnTramiteRevisionAnalista),
                        NotaRazon, gstr_Usuario, DateTime.Now, "", gstr_Usuario, out Resultado, out Mensaje);

                        if (DesplegarError(!bCambioEstado))
                        {
                            txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorCambioEstado"].ToString() + ". Etapa:" + CodigoCatalogoEtapasEstadoFinancieroEnTramiteRevisionAnalista;
                        }
                        else
                        {
                            AprobarInstitucionButton.Enabled = false;
                            RechazadoInstitucionButton.Enabled = false;

                            //Envia correo
                            string str_CorreoFrom = "", str_CorreoTo = "", str_NombreUsuario = "";
                            string str_IDUsuario = gstr_Usuario;
                            string str_RolTo = ConfigurationManager.AppSettings["IdRolRevisionAnalista"].ToString();
                            string str_Asunto = ConfigurationManager.AppSettings["CorreoAsuntoRevisionContabilidad"].ToString();
                            string str_Mensaje = ConfigurationManager.AppSettings["CorreoRevision_RevisionInstitucion_RevisionContabilidad"].ToString();
                            string Anexo_UsuarioCorreo = ConfigurationManager.AppSettings["Anexo_UsuarioCorreo"].ToString();
                            bool bErrorCorreo = false;

                            str_Mensaje = str_Mensaje.Replace("XXXXXXX", gstr_Institucion);
                            dsNICSPDatos = WSINICSP_PC.BuscarUsuario(gstr_Usuario, "", gstr_Usuario, out Resultado, out Mensaje);

                            if (dsNICSPDatos.Tables.Count > 0)//Si hizo la consulta pero No retorno nada
                            //if (!dsNICSPDatosDropDownList.Tables["Table"].Rows.Count.Equals(0))//Si hizo la consulta pero No retorno nada
                            {
                                for (int i = 0; i <= dsNICSPDatos.Tables["Table"].Rows.Count - 1; i++)
                                {
                                    str_CorreoFrom = dsNICSPDatos.Tables["Table"].Rows[i]["CorreoUsuario"].ToString();
                                    str_NombreUsuario = dsNICSPDatos.Tables["Table"].Rows[i]["NomUsuario"].ToString();
                                }
                                bErrorCorreo = true;

                                Anexo_UsuarioCorreo = Anexo_UsuarioCorreo.Replace("XXXXX", str_NombreUsuario);
                                Anexo_UsuarioCorreo = Anexo_UsuarioCorreo.Replace("YYYYY", str_CorreoFrom);

                                str_Mensaje = str_Mensaje + Anexo_UsuarioCorreo;
                            }
                            else
                            {
                                bErrorCorreo = false;
                            }

                            dsNICSPDatos = WSINICSP_PC.BuscarUsuariosPorRol(int.Parse(str_RolTo), gstr_Institucion, "", gstr_Usuario, out Resultado, out Mensaje);

                            if (dsNICSPDatos.Tables.Count > 0)//Si hizo la consulta pero No retorno nada
                            //if (!dsNICSPDatosDropDownList.Tables["Table"].Rows.Count.Equals(0))//Si hizo la consulta pero No retorno nada
                            {
                                for (int i = 0; i <= dsNICSPDatos.Tables["Table"].Rows.Count - 1; i++)
                                {
                                    str_CorreoTo = str_CorreoTo + dsNICSPDatos.Tables["Table"].Rows[i]["CorreoUsuario"].ToString() + ",";
                                }
                                str_CorreoTo = str_CorreoTo.Substring(0, str_CorreoTo.Length - 1);
                                 //str_CorreoTo = str_CorreoTo + ",alfarouga@hacienda.go.cr";
                                bErrorCorreo = true;
                            }
                            else
                            {
                                bErrorCorreo = false;
                            }

                            if (bErrorCorreo)
                                WSINICSP_PC.EnviarCorreoPC(int.Parse(ConfigurationManager.AppSettings["CorreoClientePort"].ToString()), ConfigurationManager.AppSettings["CorreoClienteHost"].ToString(), ConfigurationManager.AppSettings["CorreoNetworkCredentialUsuario"].ToString(), ConfigurationManager.AppSettings["CorreoNetworkCredentialPassWord"].ToString(), ConfigurationManager.AppSettings["UsuarioSistemaConsolidacion"].ToString(), str_CorreoTo, str_CorreoFrom, str_Mensaje, str_Asunto);

                            if (!bErrorCorreo)
                            {

                                bErrorCorreo = false;
                                DesplegarError(true);
                                txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorCorreo"].ToString();

                            }

                        }
                    }
                }
                else
                {
                    DesplegarError(true);
                    txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorParametros"].ToString();
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
                dsNICSPDatos = null;
                WSINICSP_PC = null;
            }
        }

        protected void RechazadoAnalistaButton_Click(object sender, EventArgs e)
        {
            DataSet dsNICSPDatos = new DataSet();
            wsPlantillasConsolidacion WSINICSP_PC = new wsPlantillasConsolidacion();
            try
            {
                string strUsuario, strIdEntidad, strPeriodo, strUnidadTiempoPeriodo;

                string Resultado, Mensaje, NotaRazon = "", CodigoCatalogoEtapasEstadoFinancieroRechazadoAnalista, CodigoCatalogoEtapasEstadoFinancieroUsuarioInstitucion;

                strUsuario = gstr_Usuario;
                strIdEntidad = CatalogoEntidadesDropDownList.SelectedValue;
                strPeriodo = PeriodoDropDownList.SelectedItem.Text;
                strUnidadTiempoPeriodo = UnidadTiempoPeriodoDropDownList.SelectedValue;

                CodigoCatalogoEtapasEstadoFinancieroRechazadoAnalista = ConfigurationManager.AppSettings["CodigoCatalogoEtapasEstadoFinanciero_RechazadoAnalista"].ToString();
                CodigoCatalogoEtapasEstadoFinancieroUsuarioInstitucion = ConfigurationManager.AppSettings["CodigoCatalogoEtapasEstadoFinanciero_UsuarioInstitucion"].ToString();

                bool bCambioEstado = false;
                if ((!strIdEntidad.Equals(string.Empty)) && (!strPeriodo.Equals("Seleccione ítem")) && (!strUnidadTiempoPeriodo.Equals(string.Empty)))
                {

                    bCambioEstado = WSINICSP_PC.InsertarEtapaEstadoFinanciero(strIdEntidad, int.Parse(strPeriodo), strUnidadTiempoPeriodo, int.Parse(CodigoCatalogoEtapasEstadoFinancieroRechazadoAnalista),
                    NotaRazon, gstr_Usuario, DateTime.Now, "", gstr_Usuario, out Resultado, out Mensaje);

                    if (DesplegarError(!bCambioEstado))
                    {
                        txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorCambioEstado"].ToString() + ". Etapa:" + CodigoCatalogoEtapasEstadoFinancieroRechazadoAnalista;
                    }
                    else
                    {
                        bCambioEstado = WSINICSP_PC.InsertarEtapaEstadoFinanciero(strIdEntidad, int.Parse(strPeriodo), strUnidadTiempoPeriodo, int.Parse(CodigoCatalogoEtapasEstadoFinancieroUsuarioInstitucion),
                        NotaRazon, gstr_Usuario, DateTime.Now, "", gstr_Usuario, out Resultado, out Mensaje);

                        if (DesplegarError(!bCambioEstado))
                        {
                            txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorCambioEstado"].ToString() + ". Etapa:" + CodigoCatalogoEtapasEstadoFinancieroUsuarioInstitucion;
                        }
                        else
                        {
                            //RechazadoAnalistaButton.
                        }
                    }
                }
                else
                {
                    DesplegarError(true);
                    txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorParametros"].ToString();
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
                dsNICSPDatos = null;
                WSINICSP_PC = null;
            }
        }

        protected void AprobarAnalistaButton_Click(object sender, EventArgs e)
        {
            DataSet dsNICSPDatos = new DataSet();
            wsPlantillasConsolidacion WSINICSP_PC = new wsPlantillasConsolidacion();
            try
            {
                string strUsuario, strIdEntidad, strPeriodo, strUnidadTiempoPeriodo;

                string Resultado, Mensaje, NotaRazon = "", CodigoCatalogoEtapasEstadoFinancieroAprobadoAnalista;

                strUsuario = gstr_Usuario;
                strIdEntidad = CatalogoEntidadesDropDownList.SelectedValue;
                strPeriodo = PeriodoDropDownList.SelectedItem.Text;
                strUnidadTiempoPeriodo = UnidadTiempoPeriodoDropDownList.SelectedValue;

                CodigoCatalogoEtapasEstadoFinancieroAprobadoAnalista = ConfigurationManager.AppSettings["CodigoCatalogoEtapasEstadoFinanciero_AprobadoAnalista"].ToString();

                bool bCambioEstado = false;
                if ((!strIdEntidad.Equals(string.Empty)) && (!strPeriodo.Equals("Seleccione ítem")) && (!strUnidadTiempoPeriodo.Equals(string.Empty)))
                {

                    bCambioEstado = WSINICSP_PC.InsertarEtapaEstadoFinanciero(strIdEntidad, int.Parse(strPeriodo), strUnidadTiempoPeriodo, int.Parse(CodigoCatalogoEtapasEstadoFinancieroAprobadoAnalista),
                    NotaRazon, gstr_Usuario, DateTime.Now, "", gstr_Usuario, out Resultado, out Mensaje);

                    if (DesplegarError(!bCambioEstado))
                    {
                        txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorCambioEstado"].ToString() + ". Etapa:" + CodigoCatalogoEtapasEstadoFinancieroAprobadoAnalista;
                    }
                    else
                    {
                        //AprobarAnalistaButton
                    }
                }
                else
                {
                    DesplegarError(true);
                    txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorParametros"].ToString();
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
                dsNICSPDatos = null;
                WSINICSP_PC = null;
            }
        }

        public string ValidarVisibilidadEstadosEtapas()
        {
            DataSet dsNICSPDatos = new DataSet();
            wsPlantillasConsolidacion WSINICSP_PC = new wsPlantillasConsolidacion();
            try
            {
                string strUsuario, strIdEntidad, strPeriodo, strUnidadTiempoPeriodo, IdEtapaEstadoFinanciero = "0";

                string Resultado, Mensaje, NotaRazon = "",
                    CodigoCatalogoEtapasEstadoFinancieroUsuarioInstitucion,
                    CodigoCatalogoEtapasEstadoFinancieroEnTramiteRevisionInstitucion,
                    CodigoCatalogoEtapasEstadoFinancieroAprobadoInstitucion,
                    CodigoCatalogoEtapasEstadoFinancieroRechazadoInstitucion,
                    CodigoCatalogoEtapasEstadoFinancieroEnTramiteRevisionAnalista,
                    CodigoCatalogoEtapasEstadoFinancieroAprobadoAnalista,
                    CodigoCatalogoEtapasEstadoFinancieroRechazadoAnalista;

                strUsuario = gstr_Usuario;
                strIdEntidad = CatalogoEntidadesDropDownList.SelectedValue;
                strPeriodo = PeriodoDropDownList.SelectedItem.Text;
                strUnidadTiempoPeriodo = UnidadTiempoPeriodoDropDownList.SelectedValue;

                CodigoCatalogoEtapasEstadoFinancieroUsuarioInstitucion = ConfigurationManager.AppSettings["CodigoCatalogoEtapasEstadoFinanciero_UsuarioInstitucion"].ToString();
                CodigoCatalogoEtapasEstadoFinancieroEnTramiteRevisionInstitucion = ConfigurationManager.AppSettings["CodigoCatalogoEtapasEstadoFinanciero_EnTramiteRevisionInstitucion"].ToString();
                CodigoCatalogoEtapasEstadoFinancieroAprobadoInstitucion = ConfigurationManager.AppSettings["CodigoCatalogoEtapasEstadoFinanciero_AprobadoInstitucion"].ToString();
                CodigoCatalogoEtapasEstadoFinancieroRechazadoInstitucion = ConfigurationManager.AppSettings["CodigoCatalogoEtapasEstadoFinanciero_RechazadoInstitucion"].ToString();
                CodigoCatalogoEtapasEstadoFinancieroEnTramiteRevisionAnalista = ConfigurationManager.AppSettings["CodigoCatalogoEtapasEstadoFinanciero_EnTramiteRevisionAnalista"].ToString();
                CodigoCatalogoEtapasEstadoFinancieroAprobadoAnalista = ConfigurationManager.AppSettings["CodigoCatalogoEtapasEstadoFinanciero_AprobadoAnalista"].ToString();
                CodigoCatalogoEtapasEstadoFinancieroRechazadoAnalista = ConfigurationManager.AppSettings["CodigoCatalogoEtapasEstadoFinanciero_RechazadoAnalista"].ToString();

                if ((!strIdEntidad.Equals(string.Empty)) && (!strPeriodo.Equals("Seleccione ítem")) && (!strUnidadTiempoPeriodo.Equals(string.Empty)))
                {
                    //uspConsultarEtapaEstadoFinanciero
                    dsNICSPDatos = WSINICSP_PC.ConsultarEtapaEstadoFinanciero(strIdEntidad, int.Parse(strPeriodo), strUnidadTiempoPeriodo,
                    "", gstr_Usuario, out Resultado, out Mensaje);

                    if (Resultado.Equals("00"))
                    {
                        if (dsNICSPDatos.Tables.Count > 0)//Si hizo la consulta pero No retorno nada                    
                        {
                            if (!dsNICSPDatos.Tables["Table"].Rows.Count.Equals(0))//Si hizo la consulta pero No retorno nada
                            {                            
                                DataRow lastRow = dsNICSPDatos.Tables["Table"].Rows[dsNICSPDatos.Tables["Table"].Rows.Count - 1];
                                IdEtapaEstadoFinanciero = lastRow["IdEtapaEstadoFinanciero"].ToString();

                                if (IdEtapaEstadoFinanciero.Equals("2"))//EnTramiteRevisionInstitucion
                                {
                                    RechazadoInstitucionButton.Enabled = true;
                                    AprobarInstitucionButton.Enabled = true;
                                }
                                else
                                {
                                    RechazadoInstitucionButton.Enabled = false;
                                    AprobarInstitucionButton.Enabled = false;
                                }
                                
                            }
                        }
                    }
                    else
                    {
                        DesplegarError(true);
                        txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorConsultaEtapaEstadoFinanciero"].ToString();
                    }
                }
                //else
                //{
                //    DesplegarError(true);
                //    txtError.Text = txtError.Text + "\r\n" + ConfigurationManager.AppSettings["strErrorParametros"].ToString());
                //}

                EtadaActual = IdEtapaEstadoFinanciero;
                return IdEtapaEstadoFinanciero;
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

                    if ((lstr_IdObjeto.Equals("frmCargaEntidad")) && lbool_Consultar)
                    {
                        Response.Redirect("~/Consolidacion/frmCargaEntidad.aspx", true);
                    }
                    else if ((lstr_IdObjeto.Equals("frmRevisionAnalista")) && lbool_Consultar)
                    {
                        Response.Redirect("~/Consolidacion/frmRevisionAnalista.aspx", true);
                    }
                    else if ((lstr_IdObjeto.Equals("frmRevisionEntidad")) && lbool_Consultar)
                    {
                        Response.Redirect("~/Consolidacion/frmRevisionEntidad.aspx", true);
                    }
                }
            }
            catch { }
        }

        //public bool EnviarCorreo(string str_CorreoFrom, string str_CorreoTo, string str_Mensaje, string str_Asunto)
        //{
        //    string str_Resultado = String.Empty;
        //    try
        //    {
        //        // Command line argument must the the SMTP host.
        //        SmtpClient client = new SmtpClient();
        //        client.Port = int.Parse(ConfigurationManager.AppSettings["CorreoClientePort"].ToString()); //25;
        //        client.Host = ConfigurationManager.AppSettings["CorreoClienteHost"].ToString(); //"172.18.100.11";

        //        //client.EnableSsl = true;
        //        client.Timeout = 10000;
        //        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        //        client.UseDefaultCredentials = true;
        //        //client.Credentials = new System.Net.NetworkCredential("hacienda\\scan", "hacienda01*");
        //        client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["CorreoNetworkCredentialUsuario"].ToString(), ConfigurationManager.AppSettings["CorreoNetworkCredentialPassWord"].ToString());

        //        MailMessage mm = new MailMessage();
        //        mm.From = new MailAddress(str_CorreoFrom);
        //        mm.To.Add(str_CorreoTo);
        //        mm.Subject = str_Asunto;
        //        AlternateView htmlView = AlternateView.CreateAlternateViewFromString(str_Mensaje,
        //             Encoding.UTF8, MediaTypeNames.Text.Html);
        //        mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        //        mm.AlternateViews.Add(htmlView);
        //        mm.IsBodyHtml = true;
        //        mm.Priority = MailPriority.Normal;
        //        client.Send(mm);

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        #region MensajeError
        //        EventLog.WriteEntry(ConfigurationManager.AppSettings["EventLogSource"].ToString(),
        //            //Obtiene el nombre de la clase.
        //        "NICSP"
        //            //Nombre del método.
        //        + "." + MethodInfo.GetCurrentMethod().Name
        //            //Error especifico.
        //        + ": Excepcion  " + ex.Message.ToString() + ". ",
        //        EventLogEntryType.Error);
        //        #endregion

        //        throw;

        //    }
        //    finally
        //    {

        //    }

        //}
    }
}