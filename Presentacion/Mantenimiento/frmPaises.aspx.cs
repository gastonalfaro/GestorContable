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
    public partial class frmPaises : BASE
    {
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private char gchr_MensajeError;
        private char gchr_MensajeExito;
        private string gstr_Usuario = String.Empty;
        //private DataSet gds_Paises = new DataSet();
        protected DataSet gds_Paises
        {
            get
            {
                if (ViewState["gds_Paises"] == null)
                    ViewState["gds_Paises"] = new DataSet();
                return (DataSet)ViewState["gds_Paises"];
            }
            set
            {
                ViewState["gds_Paises"] = value;
            }
        }

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
                        ConsultarPaises("", "");
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

        private void ConsultarPaises(string str_IdPaises, string str_Nombre)
        {
            gds_Paises = ws_SGService.uwsConsultarPaises(str_IdPaises, str_Nombre);

            if (gds_Paises.Tables["Table"].Rows.Count > 0)
            {
                grdvPaises.DataSource = gds_Paises.Tables["Table"];
                grdvPaises.DataBind();
            }
            else
            {
                grdvPaises.DataSource = this.LlenarTablaVacia();
                grdvPaises.DataBind();
                grdvPaises.Rows[0].Visible = false;
            }
        }

        private DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("IdPais", typeof(string));
            ldt_TablaVacia.Columns.Add("Denominacion", typeof(string));
            ldt_TablaVacia.Columns.Add("NomPais", typeof(string));
            ldt_TablaVacia.Columns.Add("Nacionalidad", typeof(string));
            ldt_TablaVacia.Columns.Add("IdMoneda", typeof(string));

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


        protected void btnPaisesVolver_Click(object sender, EventArgs e)
        {

        }

        protected void grdvPaises_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grdvPaises.SelectedIndex < 0)
                return;
        }

        protected void grdvPaises_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdvPaises.EditIndex = e.NewEditIndex;
            grdvPaises.DataSource = gds_Paises.Tables["Table"];

            grdvPaises.DataBind();
            //ConsultarPaises(txtBusqIdPais.Text, txtBusqNomPais.Text);
        }

        protected void grdvPaises_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            
        }

        protected void grdvPaises_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdvPaises.PageIndex = e.NewPageIndex;
            grdvPaises.DataSource = gds_Paises.Tables["Table"];

            grdvPaises.DataBind();
            //ConsultarPaises(txtBusqIdPais.Text, txtBusqNomPais.Text);
        }

        protected void grdvPaises_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdvPaises.EditIndex = -1;
            grdvPaises.DataSource = gds_Paises.Tables["Table"];

            grdvPaises.DataBind();
            //ConsultarPaises(txtBusqIdPais.Text, txtBusqNomPais.Text);
        }

        protected void btnPaisesConsultar_Click(object sender, EventArgs e)
        {
            OcultarMensaje();
            ConsultarPaises(txtBusqIdPais.Text, txtBusqNomPais.Text);
        }

        protected void btnPaisNuevo_Click(object sender, EventArgs e)
        {

        }

        protected void btnPaisGuardar_Click(object sender, EventArgs e)
        {

        }
    }
}