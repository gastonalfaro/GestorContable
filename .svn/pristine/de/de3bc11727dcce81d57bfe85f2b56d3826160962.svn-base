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
    public partial class frmPosicionesPresupuestarias : BASE
    {
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private char gchr_MensajeError;
        private char gchr_MensajeExito;
        private string gstr_Usuario = String.Empty;
        //private DataSet gds_PosicionesPresupuestarias = new DataSet();
        protected DataSet gds_PosicionesPresupuestarias
        {
            get
            {
                if (ViewState["gds_PosicionesPresupuestarias"] == null)
                    ViewState["gds_PosicionesPresupuestarias"] = new DataSet();
                return (DataSet)ViewState["gds_PosicionesPresupuestarias"];
            }
            set
            {
                ViewState["gds_PosicionesPresupuestarias"] = value;
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
                        ConsultarPosicionesPresupuestarias("", "", "", "", "");
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

        private void ConsultarPosicionesPresupuestarias(string str_IdPosPre, string str_IdEntidadCP, string str_IdEjercicio, string str_Denominacion, string str_NomPosPre)
        {
            gds_PosicionesPresupuestarias = ws_SGService.uwsConsultarPosicionesPresupuestarias(str_IdPosPre, str_IdEntidadCP, str_IdEjercicio, str_Denominacion, str_NomPosPre);

            if (gds_PosicionesPresupuestarias.Tables["Table"].Rows.Count > 0)
            {
                gvpPosicionesPresupuestarias.DataSource = gds_PosicionesPresupuestarias.Tables["Table"];
                gvpPosicionesPresupuestarias.DataBind();
            }
            else
            {
                gvpPosicionesPresupuestarias.DataSource = this.LlenarTablaVacia();
                gvpPosicionesPresupuestarias.DataBind();
            }
        }

        private DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("IdPosPre", typeof(string));
            ldt_TablaVacia.Columns.Add("IdEntidadCP", typeof(string));
            ldt_TablaVacia.Columns.Add("IdEjercicio", typeof(string));
            ldt_TablaVacia.Columns.Add("Denominacion", typeof(string));
            ldt_TablaVacia.Columns.Add("NomPosPre", typeof(string));
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

        protected void btnPosicionesPresupuestariasConsultar_Click(object sender, EventArgs e)
        {
            OcultarMensaje();
            ConsultarPosicionesPresupuestarias("", "", "", "", "");
        }

        protected void btnPosicionesPresupuestariasNuevo_Click(object sender, EventArgs e)
        {

        }

        protected void btnPosicionesPresupuestariasGuardar_Click(object sender, EventArgs e)
        {

        }

        protected void btnPosicionesPresupuestariasVolver_Click(object sender, EventArgs e)
        {

        }

        protected void gvpPosicionesPresupuestarias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gvpPosicionesPresupuestarias.SelectedIndex < 0)
                return;
        }

        protected void gvpPosicionesPresupuestarias_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvpPosicionesPresupuestarias.EditIndex = e.NewEditIndex;
            gvpPosicionesPresupuestarias.DataSource = gds_PosicionesPresupuestarias.Tables["Table"];

            gvpPosicionesPresupuestarias.DataBind();
            //ConsultarPosicionesPresupuestarias(this.txtBusqIdPosPre.Text, this.txtIdEntidadCP.Text, this.txtEjercicio.Text, "", this.txtBusqNomPosPre.Text);
        }

        protected void gvpPosicionesPresupuestarias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvpPosicionesPresupuestarias.PageIndex = e.NewPageIndex;
            gvpPosicionesPresupuestarias.DataSource = gds_PosicionesPresupuestarias.Tables["Table"];

            gvpPosicionesPresupuestarias.DataBind();
            //ConsultarPosicionesPresupuestarias(this.txtBusqIdPosPre.Text, this.txtIdEntidadCP.Text, this.txtEjercicio.Text, "", this.txtBusqNomPosPre.Text);
        }

        protected void gvpPosicionesPresupuestarias_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            String[] lstr_result = new String[3];
            try
            {

                DataRow ldr_PosicionesPresupuestariasRow = gds_PosicionesPresupuestarias.Tables["Table"].NewRow();
                ldr_PosicionesPresupuestariasRow = gds_PosicionesPresupuestarias.Tables["Table"].Rows[e.RowIndex];

                string lstr_IdPosicionesPresupuestarias = ldr_PosicionesPresupuestariasRow["IdPosicionPresupuestaria"].ToString();
                DateTime ldt_FchModifica = Convert.ToDateTime(ldr_PosicionesPresupuestariasRow["FchModifica"].ToString());

                string lstr_fecha = String.Empty;
                lstr_fecha = ldt_FchModifica.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                ldt_FchModifica = Convert.ToDateTime(lstr_fecha);

                GridViewRow row = (GridViewRow)gvpPosicionesPresupuestarias.Rows[e.RowIndex];

                TextBox txt_Nombre = (TextBox)row.FindControl("txtEditNomPosicionPresupuestaria");
                TextBox txt_Estado = (TextBox)row.FindControl("txtEditEstado");

                //lstr_result = ws_SGService.uwsModificarPosicionPresupuestaria(lstr_IdPosicionesPresupuestarias, txt_Nombre.Text, txt_Estado.Text, gstr_Usuario, ldt_FchModifica);
                if (lstr_result[0].ToString().Equals("00"))
                {
                    MostarMensaje(lstr_result[1].ToString(), gchr_MensajeExito);
                }
                else
                {
                    MostarMensaje("Error: " + lstr_result[1].ToString(), gchr_MensajeError);
                }
                gvpPosicionesPresupuestarias.EditIndex = -1;
                gvpPosicionesPresupuestarias.DataSource = gds_PosicionesPresupuestarias.Tables["Table"];

                gvpPosicionesPresupuestarias.DataBind();
                //ConsultarPosicionesPresupuestarias("", "", "", "", "");
            }
            catch (Exception ex)
            {
                ConsultarPosicionesPresupuestarias("", "", "", "", "");
                MostarMensaje(ex.ToString(), gchr_MensajeError);
            }
        }

        protected void gvpPosicionesPresupuestarias_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvpPosicionesPresupuestarias.EditIndex = -1;
            gvpPosicionesPresupuestarias.DataSource = gds_PosicionesPresupuestarias.Tables["Table"];

            gvpPosicionesPresupuestarias.DataBind();
            //ConsultarPosicionesPresupuestarias("", "", "", "", "");
        }

        protected void btnParametroConsultar_Click(object sender, EventArgs e)
        {
            ConsultarPosicionesPresupuestarias(this.txtBusqIdPosPre.Text, this.txtIdEntidadCP.Text, this.txtEjercicio.Text, "", this.txtBusqNomPosPre.Text);
        }

    }
}