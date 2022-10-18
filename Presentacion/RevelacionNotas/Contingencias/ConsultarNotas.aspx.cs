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
    public partial class ConsultarNotas : BASE
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

        protected void gvNotas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "consulta")
            {
                string index = Convert.ToString(e.CommandArgument.ToString());
                string lstr_Url = String.Format("~/RevelacionNotas/Contingencias/InformeNota.aspx?Rev={0}", index);
                Response.Redirect(lstr_Url, false);
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {           
             ConsultarFormularios(txtNumero.Text.Trim(), txtAnno.Text.Trim(), ddlPeriodoMensual.SelectedValue);           
        }

        private void ConsultarFormularios(string str_Consecutivo, string str_PerAnual, string str_PerMensual)
        {
            List<string> ParametrosConsulta = new List<string>() {str_Consecutivo, str_PerAnual, str_PerMensual};
            DatosConsulta = ParametrosConsulta;
            DataSet lds_Revelaciones = ws_SGService.uwsBuscarRevelacionContingente(str_Consecutivo, str_PerAnual, 
                str_PerMensual);
            if (lds_Revelaciones.Tables["Table"].Rows.Count > 0)
            {
                lblSinResultados.Visible = false;
                gvNotas.DataSource = lds_Revelaciones.Tables["Table"];
                gvNotas.DataBind();
                gvNotas.Visible = true;
            }
            else
            {
                lblSinResultados.Visible = true;
                gvNotas.Visible = false;
            }            
        }

        protected void gvNotas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvNotas.PageIndex = e.NewPageIndex;
            ConsultarFormularios(txtNumero.Text.Trim(), txtAnno.Text.Trim(), ddlPeriodoMensual.SelectedValue);   

        }

    }
}