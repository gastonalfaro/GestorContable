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
using Microsoft.Reporting.WebForms;
using System.Data;
using System.Globalization;
using System.Data.SqlClient;

namespace Presentacion.Contingentes
{
    public partial class ReportesContingentes : BASE
    {
        private string str_tipoReport = string.Empty;
        private string gstr_Usuario = String.Empty;
        private static Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        protected void Page_Load(object sender, EventArgs e)
        {

            gstr_Usuario = clsSesion.Current.LoginUsuario;

            if (!string.IsNullOrEmpty(gstr_Usuario))
            {
                if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "OBJ_CT"))
                    Response.Redirect("~/Principal.aspx", true);
                else
                {
                    string reporteAplicar = Request.QueryString["rept"];
                    Session["reporte"] = reporteAplicar;
                    ViewState["Reporte"] = Session["reporte"];
                    string encontrado;

                    str_tipoReport = Convert.ToString(ViewState["Reporte"]);

                    if (str_tipoReport == "Activo")
                    {
                        //VARO 09-07-2020
                        string vSociedad;
                        List<string> vRoles;
                        vRoles = clsSesion.Current.RolesUsuario;
                        vSociedad = clsSesion.Current.SociedadUsr;                        
                        encontrado = "N";
                        foreach(string aux in vRoles)
                        {
                            if (aux.ToString() == "46" || aux.ToString() == "92" || aux.ToString() == "99" || aux.ToString() == "113")
                            {
                                List<ReportParameter> oParametros = new List<ReportParameter>();
                                ParametrosReporte oParam;
                                oParam = new ParametrosReporte(ConfigurationManager.AppSettings["ReporteActivosContigentes"], oParametros, ConfigurationManager.AppSettings["ServidorReportes"]);
                                Session.Add("ParametrosReporte", oParam);
                                ReportesContingentesMultiView.ActiveViewIndex = 1;
                                this.ViewParametrosLinkButton.Visible = true;
                                this.ViewReporteLinkButton.Visible = true;
                                this.SeparadorParametrosReporteLabel.Visible = true;
                                encontrado = "S";
                            }                            
                        }
                        if (encontrado=="N")
                        {
                            List<ReportParameter> oParametros = new List<ReportParameter>();
                            oParametros.Add(new ReportParameter("Sociedad", vSociedad, false));
                            ParametrosReporte oParam;
                            oParam = new ParametrosReporte(ConfigurationManager.AppSettings["ReporteActivosContigentesInstitucion"], oParametros, ConfigurationManager.AppSettings["ServidorReportes"]);
                            Session.Add("ParametrosReporte", oParam);
                            ReportesContingentesMultiView.ActiveViewIndex = 1;
                            this.ViewParametrosLinkButton.Visible = true;
                            this.ViewReporteLinkButton.Visible = true;
                            this.SeparadorParametrosReporteLabel.Visible = true;
                        }                        
                    }
                    if (str_tipoReport == "Pasivo")
                    {
                        //VARO 09-07-2020
                        string vSociedad;
                        List<string> vRoles;
                        vRoles = clsSesion.Current.RolesUsuario;
                        vSociedad = clsSesion.Current.SociedadUsr;                        
                        encontrado = "N";
                        foreach (string aux in vRoles)
                        {
                            if (aux.ToString() == "46" || aux.ToString() == "92" || aux.ToString() == "99" || aux.ToString() == "113")
                            {
                                List<ReportParameter> oParametros = new List<ReportParameter>();
                                ParametrosReporte oParam;
                                oParam = new ParametrosReporte(ConfigurationManager.AppSettings["ReportePasivosContigentes"], oParametros, ConfigurationManager.AppSettings["ServidorReportes"]);
                                Session.Add("ParametrosReporte", oParam);
                                ReportesContingentesMultiView.ActiveViewIndex = 1;
                                this.ViewParametrosLinkButton.Visible = true;
                                this.ViewReporteLinkButton.Visible = true;
                                this.SeparadorParametrosReporteLabel.Visible = true;
                                encontrado = "S";
                            }
                        }
                        if (encontrado == "N")
                        {
                            List<ReportParameter> oParametros = new List<ReportParameter>();
                            oParametros.Add(new ReportParameter("Sociedad", vSociedad, false));
                            ParametrosReporte oParam;
                            oParam = new ParametrosReporte(ConfigurationManager.AppSettings["ReportePasivosContigentesInstitucion"], oParametros, ConfigurationManager.AppSettings["ServidorReportes"]);
                            Session.Add("ParametrosReporte", oParam);
                            ReportesContingentesMultiView.ActiveViewIndex = 1;
                            this.ViewParametrosLinkButton.Visible = true;
                            this.ViewReporteLinkButton.Visible = true;
                            this.SeparadorParametrosReporteLabel.Visible = true;
                        }

                    }
                    if (str_tipoReport == "XCruce")
                    {
                        //VARO 09-07-2020
                        string vSociedad;
                        List<string> vRoles;
                        vRoles = clsSesion.Current.RolesUsuario;
                        vSociedad = clsSesion.Current.SociedadUsr;
                        encontrado = "N";
                        foreach (string aux in vRoles)
                        {
                            if (aux.ToString() == "46" || aux.ToString() == "92" || aux.ToString() == "99" || aux.ToString() == "113")
                            {
                                List<ReportParameter> oParametros = new List<ReportParameter>();
                                ParametrosReporte oParam;
                                oParam = new ParametrosReporte(ConfigurationManager.AppSettings["ReporteCruceVariables"], oParametros, ConfigurationManager.AppSettings["ServidorReportes"]);
                                Session.Add("ParametrosReporte", oParam);
                                ReportesContingentesMultiView.ActiveViewIndex = 1;
                                this.ViewParametrosLinkButton.Visible = true;
                                this.ViewReporteLinkButton.Visible = true;
                                this.SeparadorParametrosReporteLabel.Visible = true;
                                encontrado = "S";
                            }
                        }
                        if (encontrado == "N")
                        {
                            List<ReportParameter> oParametros = new List<ReportParameter>();
                            oParametros.Add(new ReportParameter("Sociedad", vSociedad, false));
                            ParametrosReporte oParam;
                            oParam = new ParametrosReporte(ConfigurationManager.AppSettings["ReporteCruceVariablesInstitucion"], oParametros, ConfigurationManager.AppSettings["ServidorReportes"]);
                            Session.Add("ParametrosReporte", oParam);
                            ReportesContingentesMultiView.ActiveViewIndex = 1;
                            this.ViewParametrosLinkButton.Visible = true;
                            this.ViewReporteLinkButton.Visible = true;
                            this.SeparadorParametrosReporteLabel.Visible = true;
                        }
                    }
                    if (str_tipoReport == "GENERAL")
                    {
                        //VARO 09-07-2020
                        string vSociedad;
                        List<string> vRoles;
                        vRoles = clsSesion.Current.RolesUsuario;
                        vSociedad = clsSesion.Current.SociedadUsr;
                        encontrado = "N";
                        foreach (string aux in vRoles)
                        {
                            if (aux.ToString() == "46" || aux.ToString() == "92" || aux.ToString() == "99" || aux.ToString() == "113")
                            {
                                List<ReportParameter> oParametros = new List<ReportParameter>();
                                ParametrosReporte oParam;
                                oParam = new ParametrosReporte(ConfigurationManager.AppSettings["ReporteExpedientesGeneral"], oParametros, ConfigurationManager.AppSettings["ServidorReportes"]);
                                Session.Add("ParametrosReporte", oParam);
                                ReportesContingentesMultiView.ActiveViewIndex = 1;
                                this.ViewParametrosLinkButton.Visible = true;
                                this.ViewReporteLinkButton.Visible = true;
                                this.SeparadorParametrosReporteLabel.Visible = true;
                                encontrado = "S";
                            }
                        }
                        if (encontrado == "N")
                        {
                            List<ReportParameter> oParametros = new List<ReportParameter>();
                            oParametros.Add(new ReportParameter("Sociedad", vSociedad, false));
                            ParametrosReporte oParam;
                            oParam = new ParametrosReporte(ConfigurationManager.AppSettings["ReporteExpedientesGeneralInstitucion"], oParametros, ConfigurationManager.AppSettings["ServidorReportes"]);
                            Session.Add("ParametrosReporte", oParam);
                            ReportesContingentesMultiView.ActiveViewIndex = 1;
                            this.ViewParametrosLinkButton.Visible = true;
                            this.ViewReporteLinkButton.Visible = true;
                            this.SeparadorParametrosReporteLabel.Visible = true;
                        }
                    }
                    if (str_tipoReport=="Provision")
                    {
                        //VARO 09-07-2020
                        string vSociedad;
                        List<string> vRoles;
                        vRoles = clsSesion.Current.RolesUsuario;
                        vSociedad = clsSesion.Current.SociedadUsr;
                        encontrado = "N";
                        foreach (string aux in vRoles)
                        {
                            if (aux.ToString() == "46" || aux.ToString() == "92" || aux.ToString() == "99" || aux.ToString() == "113")
                            {
                                List<ReportParameter> oParametros = new List<ReportParameter>();
                                ParametrosReporte oParam;
                                oParam = new ParametrosReporte(ConfigurationManager.AppSettings["ReporteProvisiones"], oParametros, ConfigurationManager.AppSettings["ServidorReportes"]);
                                Session.Add("ParametrosReporte", oParam);
                                ReportesContingentesMultiView.ActiveViewIndex = 1;
                                this.ViewParametrosLinkButton.Visible = true;
                                this.ViewReporteLinkButton.Visible = true;
                                this.SeparadorParametrosReporteLabel.Visible = true;
                                encontrado = "S";
                            }
                        }
                        if (encontrado == "N")
                        {
                            List<ReportParameter> oParametros = new List<ReportParameter>();
                            oParametros.Add(new ReportParameter("Sociedad", vSociedad, false));
                            ParametrosReporte oParam;
                            oParam = new ParametrosReporte(ConfigurationManager.AppSettings["ReporteProvisionesInstitucion"], oParametros, ConfigurationManager.AppSettings["ServidorReportes"]);
                            Session.Add("ParametrosReporte", oParam);
                            ReportesContingentesMultiView.ActiveViewIndex = 1;
                            this.ViewParametrosLinkButton.Visible = true;
                            this.ViewReporteLinkButton.Visible = true;
                            this.SeparadorParametrosReporteLabel.Visible = true;
                        }
                    }
                    if (str_tipoReport == "Anulado")
                    {
                        //VARO 09-07-2020
                        string vSociedad;
                        List<string> vRoles;
                        vRoles = clsSesion.Current.RolesUsuario;
                        vSociedad = clsSesion.Current.SociedadUsr;
                        encontrado = "N";
                        foreach (string aux in vRoles)
                        {
                            if (aux.ToString() == "46" || aux.ToString() == "92" || aux.ToString() == "99" || aux.ToString() == "113")
                            {
                                List<ReportParameter> oParametros = new List<ReportParameter>();
                                ParametrosReporte oParam;
                                oParam = new ParametrosReporte(ConfigurationManager.AppSettings["ReporteAnulados"], oParametros, ConfigurationManager.AppSettings["ServidorReportes"]);
                                Session.Add("ParametrosReporte", oParam);
                                ReportesContingentesMultiView.ActiveViewIndex = 1;
                                this.ViewParametrosLinkButton.Visible = true;
                                this.ViewReporteLinkButton.Visible = true;
                                this.SeparadorParametrosReporteLabel.Visible = true;
                                encontrado = "S";
                            }
                        }
                        if (encontrado == "N")
                        {
                            List<ReportParameter> oParametros = new List<ReportParameter>();
                            oParametros.Add(new ReportParameter("Sociedad", vSociedad, false));
                            ParametrosReporte oParam;
                            oParam = new ParametrosReporte(ConfigurationManager.AppSettings["ReporteAnuladosInstitucion"], oParametros, ConfigurationManager.AppSettings["ServidorReportes"]);
                            Session.Add("ParametrosReporte", oParam);
                            ReportesContingentesMultiView.ActiveViewIndex = 1;
                            this.ViewParametrosLinkButton.Visible = true;
                            this.ViewReporteLinkButton.Visible = true;
                            this.SeparadorParametrosReporteLabel.Visible = true;
                        }
                    }
                    if (str_tipoReport == "CXP")
                    {
                        //VARO 09-07-2020
                        string vSociedad;
                        List<string> vRoles;
                        vRoles = clsSesion.Current.RolesUsuario;
                        vSociedad = clsSesion.Current.SociedadUsr;
                        encontrado = "N";
                        foreach (string aux in vRoles)
                        {
                            if (aux.ToString() == "46" || aux.ToString() == "92" || aux.ToString() == "99" || aux.ToString() == "113")
                            {
                                List<ReportParameter> oParametros = new List<ReportParameter>();
                                ParametrosReporte oParam;
                                oParam = new ParametrosReporte(ConfigurationManager.AppSettings["ReporteCXP"], oParametros, ConfigurationManager.AppSettings["ServidorReportes"]);
                                Session.Add("ParametrosReporte", oParam);
                                ReportesContingentesMultiView.ActiveViewIndex = 1;
                                this.ViewParametrosLinkButton.Visible = true;
                                this.ViewReporteLinkButton.Visible = true;
                                this.SeparadorParametrosReporteLabel.Visible = true;
                                encontrado = "S";
                            }
                        }
                        if (encontrado == "N")
                        {
                            List<ReportParameter> oParametros = new List<ReportParameter>();
                            oParametros.Add(new ReportParameter("Sociedad", vSociedad, false));
                            ParametrosReporte oParam;
                            oParam = new ParametrosReporte(ConfigurationManager.AppSettings["ReporteCXPInstitucion"], oParametros, ConfigurationManager.AppSettings["ServidorReportes"]);
                            Session.Add("ParametrosReporte", oParam);
                            ReportesContingentesMultiView.ActiveViewIndex = 1;
                            this.ViewParametrosLinkButton.Visible = true;
                            this.ViewReporteLinkButton.Visible = true;
                            this.SeparadorParametrosReporteLabel.Visible = true;
                        }
                    }
                    if (str_tipoReport == "CXC")
                    {
                        //VARO 09-07-2020
                        string vSociedad;
                        List<string> vRoles;
                        vRoles = clsSesion.Current.RolesUsuario;
                        vSociedad = clsSesion.Current.SociedadUsr;
                        encontrado = "N";
                        foreach (string aux in vRoles)
                        {
                            if (aux.ToString() == "46" || aux.ToString() == "92" || aux.ToString() == "99" || aux.ToString() == "113")
                            {
                                List<ReportParameter> oParametros = new List<ReportParameter>();
                                ParametrosReporte oParam;
                                oParam = new ParametrosReporte(ConfigurationManager.AppSettings["ReporteCXC"], oParametros, ConfigurationManager.AppSettings["ServidorReportes"]);
                                Session.Add("ParametrosReporte", oParam);
                                ReportesContingentesMultiView.ActiveViewIndex = 1;
                                this.ViewParametrosLinkButton.Visible = true;
                                this.ViewReporteLinkButton.Visible = true;
                                this.SeparadorParametrosReporteLabel.Visible = true;
                                encontrado = "S";
                            }
                        }
                        if (encontrado == "N")
                        {
                            List<ReportParameter> oParametros = new List<ReportParameter>();
                            oParametros.Add(new ReportParameter("Sociedad", vSociedad, false));
                            ParametrosReporte oParam;
                            oParam = new ParametrosReporte(ConfigurationManager.AppSettings["ReporteCXCInstitucion"], oParametros, ConfigurationManager.AppSettings["ServidorReportes"]);
                            Session.Add("ParametrosReporte", oParam);
                            ReportesContingentesMultiView.ActiveViewIndex = 1;
                            this.ViewParametrosLinkButton.Visible = true;
                            this.ViewReporteLinkButton.Visible = true;
                            this.SeparadorParametrosReporteLabel.Visible = true;
                        }
                    }
                    if (str_tipoReport == "CIC")
                    {
                        List<ReportParameter> oParametros = new List<ReportParameter>();
                        ParametrosReporte oParam;
                        oParam = new ParametrosReporte(ConfigurationManager.AppSettings["ReportePagosExpedientesCI"], oParametros, ConfigurationManager.AppSettings["ServidorReportes"]);
                        Session.Add("ParametrosReporte", oParam);
                        ReportesContingentesMultiView.ActiveViewIndex = 1;
                        this.ViewParametrosLinkButton.Visible = true;
                        this.ViewReporteLinkButton.Visible = true;
                        this.SeparadorParametrosReporteLabel.Visible = true;
                    }
                    if (str_tipoReport == "BITCON")
                    {
                        //VARO 09-07-2020
                        string vSociedad;
                        List<string> vRoles;
                        vRoles = clsSesion.Current.RolesUsuario;
                        vSociedad = clsSesion.Current.SociedadUsr;
                        encontrado = "N";
                        foreach (string aux in vRoles)
                        {
                            if (aux.ToString() == "46")
                            {
                                List<ReportParameter> oParametros = new List<ReportParameter>();
                                ParametrosReporte oParam;
                                oParam = new ParametrosReporte(ConfigurationManager.AppSettings["ReporteBitacora"], oParametros, ConfigurationManager.AppSettings["ServidorReportes"]);
                                Session.Add("ParametrosReporte", oParam);
                                ReportesContingentesMultiView.ActiveViewIndex = 1;
                                this.ViewParametrosLinkButton.Visible = true;
                                this.ViewReporteLinkButton.Visible = true;
                                this.SeparadorParametrosReporteLabel.Visible = true;
                                encontrado = "S";
                            }
                        }
                    }
                    if (str_tipoReport == "PAGADO")
                    {
                        //VARO 09-07-2020
                        string vSociedad;
                        List<string> vRoles;
                        vRoles = clsSesion.Current.RolesUsuario;
                        vSociedad = clsSesion.Current.SociedadUsr;
                        encontrado = "N";
                        foreach (string aux in vRoles)
                        {
                            if (aux.ToString() == "46" || aux.ToString() == "92" || aux.ToString() == "99" || aux.ToString() == "113" )
                            {
                                List<ReportParameter> oParametros = new List<ReportParameter>();
                                ParametrosReporte oParam;
                                oParam = new ParametrosReporte(ConfigurationManager.AppSettings["ReportePagados"], oParametros, ConfigurationManager.AppSettings["ServidorReportes"]);
                                Session.Add("ParametrosReporte", oParam);
                                ReportesContingentesMultiView.ActiveViewIndex = 1;
                                this.ViewParametrosLinkButton.Visible = true;
                                this.ViewReporteLinkButton.Visible = true;
                                this.SeparadorParametrosReporteLabel.Visible = true;
                                encontrado = "S";
                            }
                        }
                        if (encontrado == "N")
                        {
                            List<ReportParameter> oParametros = new List<ReportParameter>();
                            oParametros.Add(new ReportParameter("Sociedad", vSociedad, false));
                            ParametrosReporte oParam;
                            oParam = new ParametrosReporte(ConfigurationManager.AppSettings["ReportePagadosInstitucion"], oParametros, ConfigurationManager.AppSettings["ServidorReportes"]);
                            Session.Add("ParametrosReporte", oParam);
                            ReportesContingentesMultiView.ActiveViewIndex = 1;
                            this.ViewParametrosLinkButton.Visible = true;
                            this.ViewReporteLinkButton.Visible = true;
                            this.SeparadorParametrosReporteLabel.Visible = true;
                        }
                    }
                    if (str_tipoReport == "Duplicado")
                    {
                        //VARO 09-07-2020
                        string vSociedad;
                        List<string> vRoles;
                        vRoles = clsSesion.Current.RolesUsuario;
                        vSociedad = clsSesion.Current.SociedadUsr;
                        encontrado = "N";
                        foreach (string aux in vRoles)
                        {
                            if (aux.ToString() == "46" || aux.ToString() == "92" || aux.ToString() == "99" || aux.ToString() == "113")
                            {
                                List<ReportParameter> oParametros = new List<ReportParameter>();
                                ParametrosReporte oParam;
                                oParam = new ParametrosReporte(ConfigurationManager.AppSettings["ReporteDuplicados"], oParametros, ConfigurationManager.AppSettings["ServidorReportes"]);
                                Session.Add("ParametrosReporte", oParam);
                                ReportesContingentesMultiView.ActiveViewIndex = 1;
                                this.ViewParametrosLinkButton.Visible = true;
                                this.ViewReporteLinkButton.Visible = true;
                                this.SeparadorParametrosReporteLabel.Visible = true;
                                encontrado = "S";
                            }
                        }
                        if (encontrado == "N")
                        {
                            List<ReportParameter> oParametros = new List<ReportParameter>();
                            oParametros.Add(new ReportParameter("Sociedad", vSociedad, false));
                            ParametrosReporte oParam;
                            oParam = new ParametrosReporte(ConfigurationManager.AppSettings["ReporteDuplicadosInstitucion"], oParametros, ConfigurationManager.AppSettings["ServidorReportes"]);
                            Session.Add("ParametrosReporte", oParam);
                            ReportesContingentesMultiView.ActiveViewIndex = 1;
                            this.ViewParametrosLinkButton.Visible = true;
                            this.ViewReporteLinkButton.Visible = true;
                            this.SeparadorParametrosReporteLabel.Visible = true;
                        }
                    }
                }
            }
            else
            {
                if (String.IsNullOrEmpty(gstr_Usuario))
                {
                    MessageBox.Show("Sesión de usuario finalizó.");
                    Response.Redirect("~/Login.aspx", true);
                }
            }

        }
    }
}