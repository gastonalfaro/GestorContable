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
    public partial class frmMonedas : BASE
    {
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private char gchr_MensajeError;
        private char gchr_MensajeExito;
        private string gstr_Usuario = String.Empty;
        //private DataSet gds_Monedas = new DataSet();
        protected DataSet gds_Monedas
        {
            get
            {
                if (ViewState["gds_Monedas"] == null)
                    ViewState["gds_Monedas"] = new DataSet();
                return (DataSet)ViewState["gds_Monedas"];
            }
            set
            {
                ViewState["gds_Monedas"] = value;
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
                        ConsultarMonedas("", "");
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

        private void ConsultarMonedas(string str_IdMoneda, string str_NomMoneda)
        {
            gds_Monedas = ws_SGService.uwsConsultarMonedas(str_IdMoneda, str_NomMoneda);

            if (gds_Monedas.Tables["Table"].Rows.Count > 0)
            {
                grdvMonedas.DataSource = gds_Monedas.Tables["Table"];
                grdvMonedas.DataBind();
            }
            else
            {
                grdvMonedas.DataSource = this.LlenarTablaVacia();
                grdvMonedas.DataBind();
                grdvMonedas.Rows[0].Visible = false;
            }
        }

        private DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("IdMoneda", typeof(string));
            ldt_TablaVacia.Columns.Add("NomMoneda", typeof(string));
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

        protected void btnMonedasConsultar_Click(object sender, EventArgs e)
        {
            OcultarMensaje();
            ConsultarMonedas(txtBusqIdMonedas.Text, txtBusqNomMonedas.Text);
        }

        protected void btnMonedasNuevo_Click(object sender, EventArgs e)
        {

        }

        protected void btnMonedasGuardar_Click(object sender, EventArgs e)
        {

        }

        protected void btnMonedasVolver_Click(object sender, EventArgs e)
        {

        }

        protected void grdvMonedas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grdvMonedas.SelectedIndex < 0)
                return;
        }

        protected void grdvMonedas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdvMonedas.EditIndex = e.NewEditIndex;
            grdvMonedas.DataSource = gds_Monedas.Tables["Table"];

            grdvMonedas.DataBind();
            //ConsultarMonedas(txtBusqIdMonedas.Text, txtBusqNomMonedas.Text);
        }

        protected void grdvMonedas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdvMonedas.PageIndex = e.NewPageIndex;
            grdvMonedas.DataSource = gds_Monedas.Tables["Table"];

            grdvMonedas.DataBind();
            //ConsultarMonedas(txtBusqIdMonedas.Text, txtBusqNomMonedas.Text);
        }

        protected void grdvMonedas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            String[] lstr_result = new String[3];
            try
            {

                DataRow ldr_MonedasRow = gds_Monedas.Tables["Table"].NewRow();
                ldr_MonedasRow = gds_Monedas.Tables["Table"].Rows[e.RowIndex];

                string lstr_IdMonedas = ldr_MonedasRow["IdMoneda"].ToString();
                DateTime ldt_FchModifica = Convert.ToDateTime(ldr_MonedasRow["FchModifica"].ToString());

                string lstr_fecha = String.Empty;
                lstr_fecha = ldt_FchModifica.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                ldt_FchModifica = Convert.ToDateTime(lstr_fecha);

                GridViewRow row = (GridViewRow)grdvMonedas.Rows[e.RowIndex];

                TextBox txt_Nombre = (TextBox)row.FindControl("txtEditNomMoneda");
                TextBox txt_Estado = (TextBox)row.FindControl("txtEditEstado");

                lstr_result = ws_SGService.uwsModificarMoneda(lstr_IdMonedas, txt_Nombre.Text, txt_Estado.Text, gstr_Usuario, ldt_FchModifica,'/');
                if (lstr_result[0].ToString().Equals("00"))
                {
                    MostarMensaje(lstr_result[1].ToString(), gchr_MensajeExito);
                }
                else
                {
                    MostarMensaje("Error: " + lstr_result[1].ToString(), gchr_MensajeError);
                }
                grdvMonedas.EditIndex = -1;
                grdvMonedas.DataSource = gds_Monedas.Tables["Table"];

                grdvMonedas.DataBind();
                //ConsultarMonedas(txtBusqIdMonedas.Text, txtBusqNomMonedas.Text);
            }
            catch (Exception ex)
            {
                ConsultarMonedas(txtBusqIdMonedas.Text, txtBusqNomMonedas.Text);
                MostarMensaje(ex.ToString(), gchr_MensajeError);
            }
        }

        protected void grdvMonedas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdvMonedas.EditIndex = -1;
            grdvMonedas.DataSource = gds_Monedas.Tables["Table"];

            grdvMonedas.DataBind();
            //ConsultarMonedas(txtBusqIdMonedas.Text, txtBusqNomMonedas.Text);
        }
   
    }
}