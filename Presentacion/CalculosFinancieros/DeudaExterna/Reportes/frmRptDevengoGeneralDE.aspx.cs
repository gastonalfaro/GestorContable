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

namespace Presentacion.CalculosFinancieros.DeudaExterna.Reportes
{
    public partial class frmRptDevengoGeneralDE : System.Web.UI.Page
    {
        # region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private string gstr_Usuario = String.Empty;
        private static DataTable ldat_Cancelaciones = new DataTable();
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
                        if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmRptDevengoGeneralDE"))
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
       

        protected void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
              
          
                string FechaFinal = txtFechaFin.Text;
                string Tipo = "";
         
               
                if (!FechaFinal.Equals(string.Empty))
                {

                PanelReporte.Visible = true;

                //string  ldt_FechaInicio, ldt_FechaFin;
                string strUsuario = clsSesion.Current.LoginUsuario;

               



                List<ReportParameter> oParametros = new List<ReportParameter>();
                if (!strUsuario.Equals(string.Empty))// && (!str_Institucion.Equals(string.Empty)))
                {
                   
           
                    oParametros.Add(new ReportParameter("pFchFin", FechaFinal, false));

                    ParametrosReporte oParam = new ParametrosReporte(ConfigurationManager.AppSettings["ReporteGeneralDE"], oParametros, ConfigurationManager.AppSettings["ServidorReportes"]);


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