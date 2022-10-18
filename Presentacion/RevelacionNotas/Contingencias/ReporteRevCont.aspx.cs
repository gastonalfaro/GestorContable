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

namespace Presentacion.RevelacionNotas.Contingencias
{
    public partial class ReporteRevCont : BASE
    {
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private string lst_IdRevelacion = String.Empty;
        private string lstr_Ministerio = String.Empty;
        private string lstr_Tipo = String.Empty;
        private string str_Usuario = String.Empty;
        private string str_Anno = String.Empty;
        private string str_Mes = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            str_Usuario = clsSesion.Current.LoginUsuario;
            if (String.IsNullOrEmpty(str_Usuario) || Request.QueryString["Rev"] == null)
            {
                MessageBox.Show("Sesión de usuario finalizó.");
                Response.Redirect("~/Login.aspx", true);
            }
            else
            {
                if (!IsPostBack)
                {
                    if (!string.IsNullOrEmpty(str_Usuario))
                    {
                        if (!clsSeguridadVistas.MostrarElementos(str_Usuario, Master, "OBJ_RN"))
                        {
                            MessageBox.Show("No tiene suficientes permisos.");
                            Response.Redirect("~/Principal.aspx", true);
                        }
                        else
                        {
                            ReportesContigentesMultiView.ActiveViewIndex = 0;
                            //MostrarElementos(str_Usuario);
                            if (Request.QueryString["Rev"] != null)
                            {
                                lst_IdRevelacion = Request.QueryString["Rev"];
                                lstr_Ministerio = Request.QueryString["Soc"];
                                lstr_Tipo = Request.QueryString["Tipo"];
                                str_Anno = Request.QueryString["Ano"];
                                str_Mes = Request.QueryString["Mes"];
                                GenerarReporte(lst_IdRevelacion, lstr_Ministerio, lstr_Tipo, str_Anno, str_Mes);
                            }
                        }
                    }
                }
            }
        }

         private void GenerarReporte(string str_IdRevelacion, string str_Sociedad, string str_TipoProceso, string str_Ano, string str_Mes)
        {
            string str_mensaje = string.Empty;
            string strUsuario = clsSesion.Current.LoginUsuario;
            try
            {

                //ParametrosReporte oParam = new ParametrosReporte();
                List<ReportParameter> oParametros = new List<ReportParameter>();
                ParametrosReporte oParam;
                if ((!String.IsNullOrEmpty(strUsuario)) && !String.IsNullOrEmpty(str_IdRevelacion))
                {
                    oParametros.Add(new ReportParameter("pIdRevCont", str_IdRevelacion, false));
                    if (!String.IsNullOrEmpty(str_Sociedad))
                    {
                        oParametros.Add(new ReportParameter("pIdSociedadGL", str_Sociedad, false));
                        
                    }
                    if (!String.IsNullOrEmpty(str_TipoProceso))
                    {
                        oParametros.Add(new ReportParameter("pTipoProceso", str_TipoProceso, false));
                    }
                    oParametros.Add(new ReportParameter("pPeriodoAnual", str_Anno, false));
                    oParametros.Add(new ReportParameter("pPeriodoMensual", str_Mes, false));

                    oParametros.Add(new ReportParameter("pResultado", "", false));
                    oParametros.Add(new ReportParameter("pMensaje", "", false));
                    oParam = new ParametrosReporte(ConfigurationManager.AppSettings["ReporteRevelacionContingente"], oParametros, ConfigurationManager.AppSettings["ServidorReportes"]);
                    Session.Add("ParametrosReporte", oParam);
                    ReportesContigentesMultiView.ActiveViewIndex = 0;
                }
                else
                {
                    str_mensaje = ConfigurationManager.AppSettings["strErrorParametros"].ToString();
                    MessageBox.Show(str_mensaje);
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

                str_mensaje = "NICSP Excepcion:  " + ex.Message.ToString() + ". ";
                MessageBox.Show(str_mensaje);
                #endregion
            }
        }

        protected void btnAtras_Click(object sender, EventArgs e)
        {
            string lstr_Url = String.Format("~/RevelacionNotas/Contingencias/InformeNota.aspx?Rev={0}", Request.QueryString["Rev"]);
            Response.Redirect(lstr_Url, false);         
        }
    }
}