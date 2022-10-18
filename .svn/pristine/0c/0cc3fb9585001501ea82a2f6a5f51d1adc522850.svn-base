using Microsoft.Reporting.WebForms;
using Presentacion.Compartidas;
using Presentacion.Compartidas.VisorReportes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.CalculosFinancieros.DeudaInterna
{
    public partial class frmRptConciliaSaldos : BASE
    {
        # region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private string gstr_Usuario = String.Empty;
       //private static DataTable ldat_Cancelaciones = new DataTable();
        private string gstr_ModuloActual = String.Empty;
        # endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                gstr_Usuario = clsSesion.Current.LoginUsuario;

                if (!IsPostBack)
                {
                    CargarTipo();
                    if (!string.IsNullOrEmpty(gstr_Usuario))
                    {
                        if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmRptConciliaSaldos"))
                            Response.Redirect("~/Principal.aspx", true);
                        else
                            PanelReporte.Visible = false;
                    }
                    else
                        Response.Redirect("~/Login.aspx", true);
                }
            }
            catch (Exception ex)
            {
                //lblEstatus.Text = ex.ToString();
                //Response.Redirect("~/Login.aspx", true);
            }
        }
        private void CargarTipo()
        {

            this.ddlTipo.DataSource = ws_SGService.uwsConsultarDinamico("SELECT DISTINCT([Tipo]) FROM [cf].[TitulosValores] WHERE Tipo IS NOT NULL ORDER BY Tipo");
            this.ddlTipo.DataTextField = "Tipo";
            this.ddlTipo.DataValueField = "Tipo";
            this.ddlTipo.DataBind();
        }

        private void ReporteConsolidacion(string FechaFinal,string Tipo)
        {
            int lint_NroValor = 0;
            string lstr_Nemotecnico = "";
            decimal ldec_ValorFacial = 0;
            decimal ldec_TransadoBruto = 0;
            decimal ldec_Prima = 0;
            decimal ldec_Descuento = 0;
            decimal ldec_Cupon = 0;
            decimal ldec_InteresTotal = 0;
            decimal ldec_Interes = 0;
            decimal ldec_Total = 0;
            decimal ldec_Pagado = 0;
            DateTime lstr_Fecha = System.DateTime.Today;
            string lstr_Usuario = "SG";
            Boolean lstr_Insertar = false;
        
            DateTime lstr_FechaFinal = Convert.ToDateTime(FechaFinal);
            DataTable ldat_TablaAbajo = new DataTable();
            DataSet ldas_TablaAbajo = new DataSet();
            DataTable ldat_Devengo = new DataTable();
            DataSet ldas_Devengo = new DataSet();
            //try
            //{
                ws_SGService.uwsConsultarDinamico("DELETE FROM [cf].[ReversionesTemporal]");

                ldas_TablaAbajo = ws_SGService.uwsConsultarDinamico("SELECT [NroValor],[Nemotecnico],[ValorFacial],[ValorTransadoBruto] FROM [cf].[TitulosValores] WHERE [FchValor] between '1900-01-01' and '" + lstr_FechaFinal.ToString("yyyy.MM.dd") + "' AND ([Tipo] = '" + Tipo + "' OR ISNULL('" + Tipo + "','')='') AND IndicadorCupon = 'V' AND [EstadoValor] = 'Vigente' ORDER BY Nemotecnico");
                ldat_TablaAbajo = ldas_TablaAbajo.Tables["Table"];

                foreach (DataRow row in ldat_TablaAbajo.Rows)
                {
                    lint_NroValor = Convert.ToInt32(row["NroValor"]);
                    lstr_Nemotecnico = row["Nemotecnico"].ToString();
                    ldec_ValorFacial = Convert.ToDecimal(row["ValorFacial"]);
                    ldec_TransadoBruto = Convert.ToDecimal(row["ValorTransadoBruto"]);

                    if (ldec_TransadoBruto > ldec_ValorFacial)
                    {
                        ldec_Prima = ldec_ValorFacial - ldec_TransadoBruto;
                        ldec_Prima = Math.Abs(ldec_Prima);
                        ldec_Descuento = 0;
                    }
                    else
                    {
                        ldec_Prima = 0;
                        ldec_Descuento = ldec_ValorFacial - ldec_TransadoBruto;
                        ldec_Descuento = Math.Abs(ldec_Descuento);
                    }

                    ldas_Devengo = ws_SGService.uwsConsultarDinamico("SELECT SUM([Descuento]) AS Descuento,SUM([Cupon]) AS Cupon,SUM([InteresTotal]) AS InteresTotal FROM [cf].[DevengosMensuales] WHERE [NroValor] = " + lint_NroValor + " AND [Nemotecnico] = '" + lstr_Nemotecnico + "' AND CONVERT(DATE,Periodo,103) <= '" + lstr_FechaFinal.ToString("yyyy.MM.dd") + "'");
                    ldat_Devengo = ldas_Devengo.Tables["Table"];

                    foreach (DataRow row1 in ldat_Devengo.Rows)
                    {
                        if (row1["Cupon"].ToString() != "")
                        {
                            ldec_Cupon = Convert.ToDecimal(row1["Cupon"]);
                            ldec_InteresTotal = Convert.ToDecimal(row1["InteresTotal"]);
                            ldec_Interes = Convert.ToDecimal(row1["Descuento"]);
                            lstr_Insertar = true;
                        }
                        else
                        {
                            lstr_Insertar = false;
                        }

                    }

                        ldas_Devengo = ws_SGService.uwsConsultarDinamico("SELECT SUM([Pago]) AS Pagados FROM [cf].[DevengosIntereses] WHERE [NroValor] = " + lint_NroValor + " AND [Nemotecnico] = '" + lstr_Nemotecnico + "' AND CONVERT(DATE,Anno,103) <= '" + lstr_FechaFinal.ToString("yyyy.MM.dd") + "'");
                        ldat_Devengo = ldas_Devengo.Tables["Table"];

                        foreach (DataRow row1 in ldat_Devengo.Rows)
                        {
                            try
                            {
                                ldec_Pagado = Convert.ToDecimal(row1["Pagados"]);
                                //lstr_Insertar = true;
                            }
                            catch(Exception ex1)
                            {
                                ldec_Pagado = 0;
                            }



                        }
                   

                    if (lstr_Insertar == true)
                    {
                        ldec_Total = ldec_ValorFacial + ldec_Descuento + ldec_Prima + ldec_Interes + ldec_Cupon + ldec_Cupon;
                        ws_SGService.uwsConsultarDinamico("INSERT INTO [cf].[ReversionesTemporal] ([NroValor] ,[Nemotecnico] ,[ValorFacial] ,[Descuento],[Prima] ,[Interes],[Total],[InteresTotal],[Cupon],[FchCreacion],[UsrCreacion]) VALUES (" + lint_NroValor + ",'" + lstr_Nemotecnico + "'," + ldec_ValorFacial + "," + ldec_Descuento + "," + ldec_Prima + "," + ldec_Interes + "," + ldec_Total + "," + ldec_InteresTotal + "," + Convert.ToString(ldec_Cupon - ldec_Pagado) + ",'" + lstr_Fecha.ToString("yyyy.MM.dd") + "','" + lstr_Usuario + "')");
                        lstr_Insertar = false;
                    }

                }
            //}
            //catch (Exception e1)
            //{
            //    string x = e1.ToString();
            //}

        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
              
          
                string FechaFinal = txtFechaFin.Text;
                string Tipo = "";
         
                if (ddlTipo.Text == "" || ddlTipo.Text == "-- Seleccione Opción --")
                {
                    Tipo = String.Empty; 
                }
                else
                {
                    Tipo = ddlTipo.Text;
                }
                if (!FechaFinal.Equals(string.Empty))
                {

                PanelReporte.Visible = true;

                //string  ldt_FechaInicio, ldt_FechaFin;
                string strUsuario = clsSesion.Current.LoginUsuario;

                ReporteConsolidacion(txtFechaFin.Text, Tipo);



                List<ReportParameter> oParametros = new List<ReportParameter>();
                if (!strUsuario.Equals(string.Empty))// && (!str_Institucion.Equals(string.Empty)))
                {
                   
           
                    oParametros.Add(new ReportParameter("pFecha", FechaFinal, false));

                    ParametrosReporte oParam = new ParametrosReporte(ConfigurationManager.AppSettings["ReporteConsolidacion"], oParametros, ConfigurationManager.AppSettings["ServidorReportes"]);


                    Session.Add("ParametrosReporte", oParam);

                }
               }
                else
                {
                    PanelReporte.Visible = false;
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