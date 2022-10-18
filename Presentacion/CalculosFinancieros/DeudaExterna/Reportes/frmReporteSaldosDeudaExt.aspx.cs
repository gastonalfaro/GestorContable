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
using System.Web.UI.WebControls;
using Presentacion.Compartidas;
using System.IO;
using System.Collections;
using System.Configuration;
//using System.Data.SqlClient;
using System.Drawing;
using Microsoft.Reporting.WebForms;
using eWorld.UI;
using Presentacion.Compartidas;
using Presentacion.Compartidas.VisorReportes;
using System.Diagnostics;
using System.Reflection;

namespace Presentacion.CalculosFinancieros.DeudaExterna.Reportes
{
    public partial class frmReporteSaldosDeudaExt : BASE
    {
        # region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private string gstr_Usuario = String.Empty;
        private string gstr_ModuloActual = String.Empty;
        # endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                gstr_Usuario = clsSesion.Current.LoginUsuario;

                if (!IsPostBack)
                {
                    if (!string.IsNullOrEmpty(gstr_Usuario))
                    {
                        CargarDDLs();//llena los prestamos
                        if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmReporteSaldosDeudaExt"))
                            Response.Redirect("~/Principal.aspx", true);
                        else
                            PanelReporteSaldosDeudaExterna.Visible = false;
                    }
                    else
                        Response.Redirect("~/Login.aspx", true);
                }
                //CargarTramos();
            }
            catch (Exception ex)
            {
    
                EventLog.WriteEntry(ConfigurationManager.AppSettings["EventLogSource"].ToString(),
                    //Obtiene el nombre de la clase.
                "NICSP"
                    //Nombre del método.
                + "." + MethodInfo.GetCurrentMethod().Name
                    //Error especifico.
                + ": Excepcion  " + ex.Message.ToString() + ". ",
                EventLogEntryType.Error);
            }
        }

        /// <summary>
        /// Llena los dropdownlisto con la informacion respectiva a los prestamos
        /// </summary>
        private void CargarDDLs()
        {
            DataSet lds_Prestamos = ws_SGService.uwsConsultarDinamico("SELECT distinct([IdPrestamo]) FROM [cf].[Prestamos] order by 1");
            if (lds_Prestamos.Tables.Count > 0)
            {
                DataTable ldt_Prestamos = lds_Prestamos.Tables["Table"];
                ddlNroPrestamo.DataSource = ldt_Prestamos;
                ddlNroPrestamo.DataTextField = "IdPrestamo";
                ddlNroPrestamo.DataValueField = "IdPrestamo";
                ddlNroPrestamo.DataBind();
                ddlNroPrestamo.Items.Insert(0, new ListItem("-Todos-", ""));
            }
            //DataSet lds_Tramoes = ws_SGService.uwsConsultarDinamico("select distinct IdTramo from cf.vCalculosDevengo where IdPrestamo = '" + ddlNroPrestamo.SelectedValue + "'  order by 1");
            //if (lds_Tramoes.Tables.Count > 0)
            //{
            //    DataTable ldt_Tramoes = lds_Tramoes.Tables["Table"];
            //    ddlNroTramo.DataSource = ldt_Tramoes;
            //    ddlNroTramo.DataTextField = "IdTramo";
            //    ddlNroTramo.DataValueField = "IdTramo";
            //    ddlNroTramo.DataBind();
            //    ddlNroTramo.Items.Insert(0, new ListItem("-Todos-", ""));
            //}


        }//fin

        /// <summary>
        /// Carga la información sobre los tramos en relacion al prestamos seleccionado
        /// </summary>
        private void CargarTramos()
        {
            ddENumeroTramo.Items.Clear();
            DataSet lds_Tramoes = ws_SGService.uwsConsultarDinamico("select distinct IdTramo from cf.vCalculosDevengo where IdPrestamo = '" + ddlNroPrestamo.SelectedValue + "'  order by 1");
            if (lds_Tramoes.Tables.Count > 0)
            {
                DataTable ldt_Tramoes = lds_Tramoes.Tables["Table"];
                ddENumeroTramo.DataSource = ldt_Tramoes;
                ddENumeroTramo.DataTextField = "IdTramo";
                ddENumeroTramo.DataValueField = "IdTramo";
                ddENumeroTramo.DataBind();
                ddENumeroTramo.Items.Insert(0, new ListItem("--Todos--", ""));
            }//if

        }//fin 

        /// <summary>
        /// Responde al Presatamo seleccionado
        /// </summary>
        protected void ddENroPrestamo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarTramos();
        }//fin

        /// <summary>
        /// Genera el reporte
        /// </summary>
        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                PanelReporteSaldosDeudaExterna.Visible = true;
                string lstr_IdPrestamo, lstr_IdTramo, lstr_FechaDesde, lstr_FechHasta,lstr_IdMoneda;
                string strUsuario = clsSesion.Current.LoginUsuario;
                DateTime ldt_FechaDesde, ldt_FechaHasta;
               
               
                if (this.txtFchDesde.Text.Equals(string.Empty))
                {
                    lstr_FechaDesde = null;
                    lstr_FechHasta = null;
                }
                else
                {
                    ldt_FechaDesde = Convert.ToDateTime(this.txtFchDesde.Text);
                    lstr_FechaDesde = String.Format("{0:yyyy}", ldt_FechaDesde) + '/' + String.Format("{0:MM}", ldt_FechaDesde) + '/' + String.Format("{0:dd}", ldt_FechaDesde);

                        /*+ ' ' +
                        String.Format("{0:HH}", ldt_FechaFin) + ':' +
                        String.Format("{0:mm}", ldt_FechaFin) + ':' +
                        String.Format("{0:ss}", ldt_FechaFin) + '.' +
                        String.Format("{0:fff}", ldt_FechaFin)
                          */;
                }

                if (this.txtFchHasta.Text.Equals(string.Empty))
                {
                    lstr_FechHasta = null;
                }
                else
                {
                    ldt_FechaHasta = Convert.ToDateTime(this.txtFchHasta.Text);
                    lstr_FechHasta = String.Format("{0:yyyy}", ldt_FechaHasta) + '/' + String.Format("{0:MM}", ldt_FechaHasta) + '/' + String.Format("{0:dd}", ldt_FechaHasta)

                        /*+ ' ' +
                        String.Format("{0:HH}", ldt_FechaFin) + ':' +
                        String.Format("{0:mm}", ldt_FechaFin) + ':' +
                        String.Format("{0:ss}", ldt_FechaFin) + '.' +
                        String.Format("{0:fff}", ldt_FechaFin)
                          */;
                }

                if (ddlNroPrestamo.SelectedItem == null)
                {
                    lstr_IdPrestamo = "0";
                }
                else 
                {
                    lstr_IdPrestamo = ddlNroPrestamo.SelectedValue;
                }

                if (ddENumeroTramo.SelectedItem == null || ddENumeroTramo.SelectedItem.Equals("--Todos--"))
                {
                    lstr_IdTramo = "0";
                }
                else 
                {
                    lstr_IdTramo = ddENumeroTramo.SelectedValue;
                }

                if (ddlMonedas.SelectedItem == null)
                {
                    lstr_IdMoneda = null;
                }
                else 
                {
                    lstr_IdMoneda = ddlMonedas.SelectedValue;
                }
              
                //ParametrosReporte oParam = new ParametrosReporte();
                List<ReportParameter> oParametros = new List<ReportParameter>();
                if (!strUsuario.Equals(string.Empty))// && (!str_Institucion.Equals(string.Empty)))
                {
                    oParametros.Add(new ReportParameter("IdPrestamo", lstr_IdPrestamo, false));
                    oParametros.Add(new ReportParameter("IdTramo", lstr_IdTramo, false));
                    oParametros.Add(new ReportParameter("FechaDesde", lstr_FechaDesde, false));
                    oParametros.Add(new ReportParameter("FechaHasta", lstr_FechHasta, false));
                    oParametros.Add(new ReportParameter("IdMoneda", lstr_IdMoneda, false));

                    ParametrosReporte oParam = new ParametrosReporte(ConfigurationManager.AppSettings["ReporteSaldosDeudaExterna"], oParametros, ConfigurationManager.AppSettings["ServidorReportes"]);

                    //oParam.DireccionReporte = ConfigurationManager.AppSettings["ReporteBitacoraFlowError"];
                    //oParam.Parametros = oParametros;
                    //oParam.ServidorReportes = ConfigurationManager.AppSettings["ServidorReportes"];
                    Session.Add("ParametrosReporte", oParam);
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
            }//catch
        }//fin
    }
}