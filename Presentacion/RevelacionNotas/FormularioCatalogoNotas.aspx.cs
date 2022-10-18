using System;
using System.Web;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.Services;
using System.Configuration;
using Presentacion.Compartidas;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

namespace Presentacion.RevelacionNotas
{
    public partial class FormularioCatalogoNotas : BASE
    {

        private static Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
                ConsultarNotas();

            //Mensaje si los datos se crearon correctamente
            if (Request.QueryString["Est"] != null && Request.QueryString["Est"].Equals("true"))
                MessageBox.Show("Se ingresaron los datos correctamente.");
        }

        private void ConsultarNotas() 
        {
            DataSet lds_Revelaciones =  ws_SGService.uwsConsultarCategoriasNotas();
            
            if (lds_Revelaciones.Tables["Table"].Rows.Count > 0)
            {
                gvFormularios.DataSource = lds_Revelaciones.Tables["Table"];
                gvFormularios.DataBind();
                gvFormularios.Visible = true;
            }
            else  gvFormularios.Visible = false;
            
        }

        protected void gvFormularios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "consulta")
            {

                LinkButton lb = (LinkButton)e.CommandSource;
                GridViewRow vGridViewRow = (GridViewRow)lb.NamingContainer;

                string str_dato = gvFormularios.DataKeys[vGridViewRow.RowIndex].Values[0].ToString();

                string lstr_Url = String.Format("~/RevelacionNotas/FormulariosDeuda.aspx?Cat={0}", str_dato);
                this.Response.Redirect(lstr_Url, false);
            }
        }

        protected void gvFormularios_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvFormularios.SelectedIndex = e.NewEditIndex;
            string lstr_Url = String.Format("~/RevelacionNotas/FormulariosDeuda.aspx?Cat={0}", 
                ((Label)gvFormularios.Rows[e.NewEditIndex].Cells[0].FindControl("lblCategoria")).Text);
            this.Response.Redirect(lstr_Url, false);
        }

        protected void gvFormularios_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            gvFormularios.PageIndex = e.NewPageIndex;
            ConsultarNotas();
            
        }
    }
}