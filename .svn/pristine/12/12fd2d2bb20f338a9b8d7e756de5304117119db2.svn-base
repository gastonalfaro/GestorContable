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
    public partial class frmTiposCambio : BASE
    {
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private Presentacion.wsDeudaExterna.wsDeudaExterna wsDE = new Presentacion.wsDeudaExterna.wsDeudaExterna();
        private char gchr_MensajeError;
        private char gchr_MensajeExito;
        private string gstr_Usuario = String.Empty;

        //static DataSet gds_TiposCambio = new DataSet();
        protected DataSet gds_TiposCambio
        {
            get
            {
                if (ViewState["gds_TiposCambio"] == null)
                    ViewState["gds_TiposCambio"] = new DataSet();
                return (DataSet)ViewState["gds_TiposCambio"];
            }
            set
            {
                ViewState["gds_TiposCambio"] = value;
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
                    if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmTiposCambio"))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                    {

                        OcultarMensaje();
                        ConsultarTiposCambio("", "", "");
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

        private void ConsultarTiposCambio(string str_IdMoneda, string str_FchReferencia, string str_TipoTransaccion)
        {
            DateTime ldt_FchReferencia = DateTime.Today;
            if (!string.IsNullOrEmpty(str_FchReferencia))
                ldt_FchReferencia = Convert.ToDateTime(str_FchReferencia);

            gds_TiposCambio = ws_SGService.uwsConsultarTiposCambio(str_IdMoneda, ldt_FchReferencia, str_TipoTransaccion, "N");

            if (gds_TiposCambio.Tables["Table"].Rows.Count > 0)
            {
                grdvTiposCambio.DataSource = gds_TiposCambio.Tables["Table"];
                grdvTiposCambio.DataBind();
            }
            else
            {
                grdvTiposCambio.DataSource = this.LlenarTablaVacia();
                grdvTiposCambio.DataBind();
                grdvTiposCambio.Rows[0].Visible = false;
            }
        }

        private DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("IdMoneda", typeof(string));
            ldt_TablaVacia.Columns.Add("FchReferencia", typeof(string));
            ldt_TablaVacia.Columns.Add("TipoTransaccion", typeof(string));
            ldt_TablaVacia.Columns.Add("Valor", typeof(string));
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

        protected void btnTipoCambioNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Mantenimiento/Gestion TiposCambio/frmNuevoTipoCambio.aspx", false);
        }

        protected void btnTipoCambioGuardar_Click(object sender, EventArgs e)
        {

        }

        protected void btnTiposCambioVolver_Click(object sender, EventArgs e)
        {

        }

        protected void btnTipoCambioConsultar_Click(object sender, EventArgs e)
        {
            OcultarMensaje();
            ConsultarTiposCambio(txtBusqMoneda.Text, txtBusqFchReferencia.Text, txtBusqTransaccion.Text);
        }

        protected void grdvTiposCambio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grdvTiposCambio.SelectedIndex < 0)
                return;
        }

        protected void grdvTiposCambio_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            String[] lstr_result = new String[3];
            try
            {
                DataRow ldr_TiposCambioRow = gds_TiposCambio.Tables["Table"].NewRow();
                ldr_TiposCambioRow = gds_TiposCambio.Tables["Table"].Rows[e.RowIndex];

                string lstr_IdTipoCambio = ldr_TiposCambioRow["IdTipoCambio"].ToString();
                DateTime ldt_FchModifica = Convert.ToDateTime(ldr_TiposCambioRow["FchModifica"].ToString());

                string lstr_fecha = String.Empty;
                lstr_fecha = ldt_FchModifica.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                ldt_FchModifica = Convert.ToDateTime(lstr_fecha);

                GridViewRow row = (GridViewRow)grdvTiposCambio.Rows[e.RowIndex];
                TextBox txt_FchReferencia = (TextBox)row.FindControl("txtFchReferencia");
                TextBox txt_TipoTransaccion = (TextBox)row.FindControl("txtEditTipoTransaccion");
                TextBox txt_Valor = (TextBox)row.FindControl("txtEditValor");

                lstr_result = ws_SGService.uwsModificarTipoCambio(lstr_IdTipoCambio, Convert.ToDateTime(txt_FchReferencia.Text),
                    txt_TipoTransaccion.Text, Convert.ToDecimal(txt_Valor.Text), gstr_Usuario, ldt_FchModifica);
                if ( lstr_result[0].ToString().Equals("00"))
                {
                    MostarMensaje(lstr_result[1].ToString(), gchr_MensajeExito);
                }
                else
                {
                    MostarMensaje(lstr_result[1].ToString(), gchr_MensajeExito);
                }
                grdvTiposCambio.EditIndex = -1;
                grdvTiposCambio.DataSource = gds_TiposCambio.Tables["Table"];

                grdvTiposCambio.DataBind();
                //ConsultarTiposCambio(txtBusqMoneda.Text, txtBusqFchReferencia.Text, txtBusqTransaccion.Text);
            }
            catch (Exception ex)
            {
                ConsultarTiposCambio(txtBusqMoneda.Text, txtBusqFchReferencia.Text, txtBusqTransaccion.Text);
                MostarMensaje(ex.ToString(), gchr_MensajeError);
                
            }
        }

        protected void grdvTiposCambio_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            //DataSet lds_TiposCambio = ws_SGService.uwsConsultarTiposCambio("", DateTime.Today, "", null);
        }


        protected void grdvTiposCambio_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdvTiposCambio.PageIndex = e.NewPageIndex;
            grdvTiposCambio.DataSource = gds_TiposCambio.Tables["Table"];

            grdvTiposCambio.DataBind();
            //ConsultarTiposCambio(this.txtBusqMoneda.Text, this.txtBusqFchReferencia.Text, this.txtBusqTransaccion.Text);
        }

        protected void grdvTiposCambio_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdvTiposCambio.EditIndex = -1;
            grdvTiposCambio.DataSource = gds_TiposCambio.Tables["Table"];

            grdvTiposCambio.DataBind();
            //ConsultarTiposCambio(this.txtBusqMoneda.Text, this.txtBusqFchReferencia.Text, this.txtBusqTransaccion.Text);
        }

        protected void grdvTiposCambio_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void btnTiposCambioConsultar_Click(object sender, EventArgs e)
        {
            OcultarMensaje();
            ConsultarTiposCambio(txtBusqMoneda.Text, txtBusqFchReferencia.Text, txtBusqTransaccion.Text);
        }

        protected void btnTiposCambioActualizar_Click(object sender, EventArgs e)
        {
            wsSG.wsSistemaGestor wsSG = new wsSG.wsSistemaGestor();
            //try
            //{
            //    MessageBox.Show(wsDE.ActualizarTipoCambio());
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}

            try
            {
                wsSG.CargarTiposCambio("");
                wsSG.ActualizarTiposCambio();
                MessageBox.Show("Tipos de cambio actualizados");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}