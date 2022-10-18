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
    public partial class frmBancos : BASE
    {
        #region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private char gchr_MensajeError;
        private char gchr_MensajeExito;
        private string gstr_Usuario = String.Empty;
        //private static DataSet gds_Bancos = new DataSet();
        protected DataSet gds_Bancos
        {
            get
            {
                if (ViewState["gds_Bancos"] == null)
                    ViewState["gds_Bancos"] = new DataSet();
                return (DataSet)ViewState["gds_Bancos"];
            }
            set
            {
                ViewState["gds_Bancos"] = value;
            }
        }
        #endregion

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
                        ConsultarBancos("", "", "", "");
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

        private void ConsultarBancos(string str_IdBanco, string str_IdBancoPropio, string str_IdSociedadFi, string str_NomBanco)
        {
            gds_Bancos = ws_SGService.uwsConsultarBancos(str_IdBanco, str_IdBancoPropio, str_IdSociedadFi, str_NomBanco);

            if (gds_Bancos.Tables["Table"].Rows.Count > 0)
            {
                grdvBancos.DataSource = gds_Bancos.Tables["Table"];
                grdvBancos.DataBind();
            }
            else
            {
                grdvBancos.DataSource = this.LlenarTablaVacia();
                grdvBancos.DataBind();
            }
        }

        private DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("IdBanco", typeof(string));
            ldt_TablaVacia.Columns.Add("NomBanco", typeof(string));
            ldt_TablaVacia.Columns.Add("IdBancoPropio", typeof(string));
            ldt_TablaVacia.Columns.Add("IdSociedadFi", typeof(string));
            ldt_TablaVacia.Columns.Add("IdPais", typeof(string));
            ldt_TablaVacia.Columns.Add("Telefono", typeof(string));
            ldt_TablaVacia.Columns.Add("Contacto", typeof(string));
            ldt_TablaVacia.Columns.Add("CtaContable", typeof(string));
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

        protected void btnBancoNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Mantenimiento/Gestiones/frmNuevoBanco.aspx", false);
        }

        protected void btnBancoGuardar_Click(object sender, EventArgs e)
        {

        }

        protected void btnBancoVolver_Click(object sender, EventArgs e)
        {

        }

        protected void btnBancosConsultar_Click(object sender, EventArgs e)
        {
            OcultarMensaje();
            ConsultarBancos(txtBusqIdBanco.Text,"", "" ,txtBusqNomBanco.Text);
        }

        protected void grdvBancos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grdvBancos.SelectedIndex < 0)
                return;
        }

        protected void grdvBancos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            String[] lstr_result = new String[3];
            try
            {
                DataRow ldr_BancosRow = gds_Bancos.Tables["Table"].NewRow();
                ldr_BancosRow = gds_Bancos.Tables["Table"].Rows[e.RowIndex];

                string lstr_IdBanco = ldr_BancosRow["IdBanco"].ToString();
                DateTime ldt_FchModifica = Convert.ToDateTime(ldr_BancosRow["FchModifica"].ToString());

                string lstr_fecha = String.Empty;
                lstr_fecha = ldt_FchModifica.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                ldt_FchModifica = Convert.ToDateTime(lstr_fecha);

                GridViewRow row = (GridViewRow)grdvBancos.Rows[e.RowIndex];
                TextBox txt_NomBanco = (TextBox)row.FindControl("txtEditNomBanco");
                TextBox txt_IdPais = (TextBox)row.FindControl("txtEditIdPais");
                TextBox txt_Telefonos = (TextBox)row.FindControl("txtEdittelefonos");
                TextBox txt_Contacto = (TextBox)row.FindControl("txtEditContacto");
                TextBox txt_Estado = (TextBox)row.FindControl("txtEditEstado");

                lstr_result = ws_SGService.uwsModificarBanco(lstr_IdBanco, txt_NomBanco.Text, txt_IdPais.Text,
                    txt_Telefonos.Text, txt_Contacto.Text, txt_Estado.Text, gstr_Usuario, ldt_FchModifica);
                if (lstr_result[0].ToString().Equals("00") || lstr_result[0].ToString().Equals("True"))
                {
                    MostarMensaje("Se actualizó correctamente el banco.", gchr_MensajeExito);
                }
                else
                {
                    MostarMensaje("Error al modificar el banco.", gchr_MensajeError);
                }
                grdvBancos.EditIndex = -1;
                grdvBancos.DataSource = gds_Bancos.Tables["Table"];

                grdvBancos.DataBind();
                //ConsultarBancos(txtBusqIdBanco.Text, "", "", txtBusqNomBanco.Text);
            }
            catch (Exception ex)
            {
                ConsultarBancos(txtBusqIdBanco.Text, "", "", txtBusqNomBanco.Text);
                MostarMensaje(ex.ToString(), gchr_MensajeError);

            }
        }

        protected void grdvBancos_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            ConsultarBancos(txtBusqIdBanco.Text, "", "", txtBusqNomBanco.Text);
            //DataSet lds_Bancos = ws_SGService.uwsConsultarBancos(txtBusqIdBanco.Text, "", "", txtBusqNomBanco.Text);
        }


        protected void grdvBancos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdvBancos.PageIndex = e.NewPageIndex;
            grdvBancos.DataSource = gds_Bancos.Tables["Table"];

            grdvBancos.DataBind();
            //ConsultarBancos(txtBusqIdBanco.Text, "", "", txtBusqNomBanco.Text);
        }

        protected void grdvBancos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdvBancos.EditIndex = -1;
            grdvBancos.DataSource = gds_Bancos.Tables["Table"];

            grdvBancos.DataBind();
            //ConsultarBancos(txtBusqIdBanco.Text, "", "", txtBusqNomBanco.Text);
        }

        protected void grdvBancos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.Response.Redirect(String.Format("~/Mantenimiento/frmCuentasBancarias.aspx?Num={0}",
                ((Label)grdvBancos.Rows[e.NewEditIndex].FindControl("lblIdBanco")).Text));
        }

        protected void grdvBancos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
        }

    }
}