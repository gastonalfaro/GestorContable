using Presentacion.Compartidas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Presentacion.Mantenimiento
{
    public partial class frmAreasFuncionales : BASE
    {
        # region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private char gchr_MensajeError;
        private char gchr_MensajeExito;
        private string gstr_Usuario = String.Empty;
        //static DataSet gds_AreasFuncionales = new DataSet();
        protected DataSet gds_AreasFuncionales
        {
            get
            {
                if (ViewState["gds_AreasFuncionales"] == null)
                    ViewState["gds_AreasFuncionales"] = new DataSet();
                return (DataSet)ViewState["gds_AreasFuncionales"];
            }
            set
            {
                ViewState["gds_AreasFuncionales"] = value;
            }
        }
        # endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            gstr_Usuario = clsSesion.Current.LoginUsuario;
            gchr_MensajeError = clsSesion.Current.chr_MensajeError;
            gchr_MensajeExito = clsSesion.Current.chr_MensajeExito;
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(gstr_Usuario))
                {
                    if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, ""))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                    {
                        OcultarMensaje();
                        ConsultarAreasFuncionales("", "");
                    }
                        
                }
                else
                {
                    Response.Redirect("~/Login.aspx", true);
                }

            }
            else
            {
                if (string.IsNullOrEmpty(gstr_Usuario))
                    Response.Redirect("~/Login.aspx", true);
            }
        }

        private void ConsultarAreasFuncionales(string str_IdAreaFuncional, string str_NomAreaFuncional)
        {
            gds_AreasFuncionales = ws_SGService.uwsConsultarAreasFuncionales(str_IdAreaFuncional, str_NomAreaFuncional);

            if (gds_AreasFuncionales.Tables["Table"].Rows.Count > 0)
            {
                grdAreasFuncionales.DataSource = gds_AreasFuncionales.Tables["Table"];
                grdAreasFuncionales.DataBind();
            }
            else
            {
                grdAreasFuncionales.DataSource = this.LlenarTablaVacia();
                grdAreasFuncionales.DataBind();
                grdAreasFuncionales.Rows[0].Visible = false;
            }          
        }

        private DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("IdAreaFuncional", typeof(string));
            ldt_TablaVacia.Columns.Add("NomAreaFuncional", typeof(string));
            ldt_TablaVacia.Columns.Add("Estado", typeof(string));

            DataRow ldr_FilaTabla = ldt_TablaVacia.NewRow();
            ldt_TablaVacia.Rows.Add(ldr_FilaTabla);
            return ldt_TablaVacia;
        }

        private void MostarMensaje(string str_TextMensaje, char chr_TipoMensaje)
        {
            if (chr_TipoMensaje.Equals('1'))
            {
                this.lblMensaje.Text = str_TextMensaje;
                this.lblMensaje.ForeColor = System.Drawing.Color.DarkRed;
                this.lblMensaje.Visible = true;
            }
            else
            {
                this.lblMensaje.Text = str_TextMensaje;
                this.lblMensaje.ForeColor = System.Drawing.Color.DarkGreen;
                this.lblMensaje.Visible = true;
            }

        }

        private void OcultarMensaje()
        {
            this.lblMensaje.Text = String.Empty;
            this.lblMensaje.Visible = false;
        }

        protected void btnAreaFuncionalConsultar_Click(object sender, EventArgs e)
        {
            OcultarMensaje();
            ConsultarAreasFuncionales(txtBusqIdAreaFuncional.Text, txtBusqNomAreaFuncional.Text);
        }

        protected void grdAreasFuncionales_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grdAreasFuncionales.SelectedIndex < 0)
                return;
        }

        protected void grdAreasFuncionales_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdAreasFuncionales.EditIndex = e.NewEditIndex;
            grdAreasFuncionales.DataSource = gds_AreasFuncionales.Tables["Table"];

            grdAreasFuncionales.DataBind();
            //ConsultarAreasFuncionales("", "");
        }

        protected void grdAreasFuncionales_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void grdAreasFuncionales_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdAreasFuncionales.PageIndex = e.NewPageIndex;
            grdAreasFuncionales.DataSource = gds_AreasFuncionales.Tables["Table"];

            grdAreasFuncionales.DataBind();
            //ConsultarAreasFuncionales("", "");
        }

        protected void grdAreasFuncionales_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdAreasFuncionales.EditIndex = -1;
            grdAreasFuncionales.DataSource = gds_AreasFuncionales.Tables["Table"];

            grdAreasFuncionales.DataBind();
            //ConsultarAreasFuncionales("", "");
        }


    }
}