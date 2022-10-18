using Presentacion.Compartidas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.RevelacionNotas.Contingencias
{
    public partial class InformeNota : BASE
    {
        private List<string> DatosConsulta
        {
            get
            {
                return (List<string>)ViewState["Consulta"];
            }
            set
            {
                ViewState["Consulta"] = value;
            }
        }

        private static Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private string lst_IdRevelacion = String.Empty;
        private string str_Usuario = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            str_Usuario = clsSesion.Current.LoginUsuario;

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
                        if (Request.QueryString["Rev"] != null)
                        {
                            CargarDDLs();
                            lst_IdRevelacion = Request.QueryString["Rev"];
                            ConsultarRevelacionNota(lst_IdRevelacion, "", "", "", "");
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/Login.aspx", true);
                }

            }
            else
            {
                if (string.IsNullOrEmpty(str_Usuario))
                    Response.Redirect("~/Login.aspx", true);
            }
        }

        protected void gvRevSoc_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvRevSoc.SelectedIndex = e.NewEditIndex;
            Label lblTipoProceso = (Label)gvRevSoc.Rows[e.NewEditIndex].Cells[0].FindControl("lblTipoProceso");
            Label lblIdSociedad = (Label)gvRevSoc.Rows[e.NewEditIndex].Cells[0].FindControl("lblIdSociedad");
            string lstr_Revelacion = hdnRev.Value;
            string lstr_TipoProc = lblTipoProceso.Text;
            string lstr_SociedadGL = lblIdSociedad.Text;
            string lstr_Url = String.Format("~/RevelacionNotas/Contingencias/EdicionRevContingente.aspx?Rev={0}&Min={1}&Tip={2}", lstr_Revelacion, lstr_SociedadGL, lstr_TipoProc);
            Response.Redirect(lstr_Url, false);
        }

        private void ConsultarRevelacionNota(string str_Consecutivo, string str_PerAnual, string str_PerMensual,
            string str_SociedadGL, string str_TipoProceso)
        {
            List<string> ParametrosConsulta = new List<string>() { str_Consecutivo, str_PerAnual, str_PerMensual, 
                str_SociedadGL, str_TipoProceso };
            DatosConsulta = ParametrosConsulta;

            DataSet lds_Revelaciones = ws_SGService.uwsConsultarRevelacionContSoc(str_Consecutivo, str_PerAnual,
                str_PerMensual, str_SociedadGL, str_TipoProceso);
        
            DataSet dsPeriodo = ws_SGService.uwsBuscarRevelacionContingente(str_Consecutivo, str_PerAnual, str_PerMensual);
            if (dsPeriodo.Tables.Count > 0) 
            {
                lblPAnual.Text = String.Format("Periodo Anual: {0}", dsPeriodo.Tables["Table"].Rows[0]["PeriodoAnual"].ToString());
                lblPMensual.Text = String.Format("Periodo Mensual: {0}", dsPeriodo.Tables["Table"].Rows[0]["PeriodoMensual"].ToString());
            }

            if (lds_Revelaciones.Tables.Count > 0)
            {
                if (lds_Revelaciones.Tables["Table"].Rows.Count > 0)
                {
                    //lblSinResultados.Visible = false;

                    gvRevSoc.DataSource = lds_Revelaciones.Tables["Table"];
                    gvRevSoc.DataBind();
                    gvRevSoc.Visible = true;
                    //lblPAnual.Text = String.Format("Periodo Anual: {0}", lds_Revelaciones.Tables["Table"].Rows[0]["PeriodoAnual"].ToString());
                    //lblPMensual.Text = String.Format("Periodo Mensual: {0}", lds_Revelaciones.Tables["Table"].Rows[0]["PeriodoMensual"].ToString());
                    hdnRev.Value = lds_Revelaciones.Tables["Table"].Rows[0]["IdRevCont"].ToString();
                }
            }
            else
            {
                //lblSinResultados.Visible = true;
                gvRevSoc.Visible = false;
            }
        }

        private void CargarDDLs()
        {
            DataTable ldt_Instituciones = new DataTable();
            DataTable ldt_Opcion = new DataTable();

            ldt_Instituciones = ws_SGService.uwsConsultarSociedadesGL("", "", "", "", "").Tables[0];
            ldt_Opcion = ws_SGService.uwsConsultarOpcionesCatalogo("30", "", "", "").Tables[0];

            if (ldt_Instituciones != null)
            {
                DataTable ldt_InstitucionesDDL = new DataTable();
                ldt_InstitucionesDDL.Clear();
                ldt_InstitucionesDDL.Columns.Add("NomSociedad");
                ldt_InstitucionesDDL.Columns.Add("IdSociedadGL");

                int int_TamanoTablaInst = ldt_Instituciones.Rows.Count;
                for (int i = 0; i < int_TamanoTablaInst; i++)
                {
                    DataRow ldr_Sociedad = ldt_InstitucionesDDL.NewRow();
                    DataRow ldr_FilaInstituciones = ldt_Instituciones.Rows[i];
                    int int_IdSociedadGL = Convert.ToInt32(ldr_FilaInstituciones["IdSociedadGL"].ToString().Trim());
                    if (int_IdSociedadGL > 11200 && int_IdSociedadGL <= 11999)
                    {
                        ldr_Sociedad["NomSociedad"] = ldr_FilaInstituciones["NomSociedad"].ToString();
                        ldr_Sociedad["IdSociedadGL"] = ldr_FilaInstituciones["IdSociedadGL"].ToString();
                        ldt_InstitucionesDDL.Rows.Add(ldr_Sociedad);
                    }
                }
                ddlMinisterio.DataSource = ldt_InstitucionesDDL;
                ddlMinisterio.DataTextField = "NomSociedad";
                ddlMinisterio.DataValueField = "IdSociedadGL";
                ddlMinisterio.DataBind();
                ddlMinisterio.Items.Insert(0, new ListItem("", ""));
            }

            if (ldt_Opcion != null)
            {
                ddlOpciones.DataSource = ldt_Opcion;
                ddlOpciones.DataTextField = "NomOpcion";
                ddlOpciones.DataValueField = "IdOpcion";
                ddlOpciones.DataBind();
                ddlOpciones.Items.Insert(0, new ListItem("", ""));
            }
        }

        protected void gvRevSoc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRevSoc.PageIndex = e.NewPageIndex;
            ConsultarRevelacionNota(DatosConsulta.ElementAt(0), DatosConsulta.ElementAt(1), DatosConsulta.ElementAt(2),
                DatosConsulta.ElementAt(3), DatosConsulta.ElementAt(4));
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["Rev"] != null)
            {
                lst_IdRevelacion = Request.QueryString["Rev"];
                ConsultarRevelacionNota(lst_IdRevelacion, lblPAnual.Text.Substring(15), lblPMensual.Text.Substring(17), ddlMinisterio.SelectedValue.Trim(), ddlOpciones.SelectedValue.Trim());
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string lstr_IdRev = DatosConsulta[0];
            string lstr_Ministerio = DatosConsulta[3];
            string lstr_Tipo = DatosConsulta[4];
            string lstr_Url = String.Format("~/RevelacionNotas/Contingencias/ReporteRevCont.aspx?Rev={0}&Soc={1}&Tipo={2}&Ano={3}&Mes={4}", lstr_IdRev, lstr_Ministerio, lstr_Tipo, lblPAnual.Text.Substring(15), lblPMensual.Text.Substring(17));
            Response.Redirect(lstr_Url);           
        }

    }
}