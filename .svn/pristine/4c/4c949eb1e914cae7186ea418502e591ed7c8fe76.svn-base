using Presentacion.Compartidas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Mantenimiento
{
    public partial class frmPropietarios : BASE
    {
        #region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();

        private char gchr_MensajeError;
        private char gchr_MensajeExito;

        private String gstr_Usuario = String.Empty;

        private String[] garr_Modulos;
        private String[] garr_Modulo_Unico;

        //static DataSet gds_Propietarios = new DataSet();
        protected DataSet gds_Propietarios
        {
            get
            {
                if (ViewState["gds_Propietarios"] == null)
                    ViewState["gds_Propietarios"] = new DataSet();
                return (DataSet)ViewState["gds_Propietarios"];
            }
            set
            {
                ViewState["gds_Propietarios"] = value;
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
                    garr_Modulos = clsSesion.Current.PermisosModulos;

                    if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmReservas"))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                    {
                        ConsultarPropietarios("", "", "", "");
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

        private void ConsultarPropietarios(String str_IdPropietario, String str_IdSociedadGL, String str_IdSociedadFi, String str_NomPropietario)
        {
            String lstr_modulo = String.Empty;

                gds_Propietarios = ws_SGService.uwsConsultarPropietarios(str_IdPropietario, str_IdSociedadGL, str_IdSociedadFi, str_NomPropietario);

            if (gds_Propietarios.Tables["Table"].Rows.Count > 0)
            {
                grdvPropietarios.DataSource = gds_Propietarios.Tables["Table"];
                grdvPropietarios.DataBind();
            }
            else
            {
                grdvPropietarios.DataSource = this.LlenarTablaVacia();
                grdvPropietarios.DataBind();
            }
        }

        private DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("IdPropietario", typeof(string));
            ldt_TablaVacia.Columns.Add("IdSociedadGL", typeof(string));
            ldt_TablaVacia.Columns.Add("IdSociedadFi", typeof(string));
            ldt_TablaVacia.Columns.Add("NomPropietario", typeof(string));
            ldt_TablaVacia.Columns.Add("Estado", typeof(string));

            DataRow ldr_FilaTabla = ldt_TablaVacia.NewRow();
            ldt_TablaVacia.Rows.Add(ldr_FilaTabla);
            return ldt_TablaVacia;
        }

        protected void btnPropietariosVolver_Click(object sender, EventArgs e)
        {

        }

        protected void grdvPropietarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grdvPropietarios.SelectedIndex < 0)
                return;
        }

        protected void grdvPropietarios_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdvPropietarios.EditIndex = e.NewEditIndex;
            grdvPropietarios.DataSource = gds_Propietarios.Tables["Table"];

            grdvPropietarios.DataBind();
                //ConsultarPropietarios(txtBusqIdPropietario.Text.Trim(), "", "", txtBusqNomPropietario.Text.Trim());
        }

        protected void grdvPropietarios_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            String[] lstr_result = new String[3];
            try
            {
                //DataRow ldr_PropietariosRow = gds_Propietarios.Tables["Table"].NewRow();
                //ldr_PropietariosRow = gds_Propietarios.Tables["Table"].Rows[e.RowIndex];

                //string lstr_IdPropietario = ldr_PropietariosRow["IdPropietario"].ToString();
                //DateTime ldt_FchModifica = Convert.ToDateTime(ldr_PropietariosRow["FchModifica"].ToString());

                //string lstr_fecha = String.Empty;
                //lstr_fecha = ldt_FchModifica.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                //ldt_FchModifica = Convert.ToDateTime(lstr_fecha);

                GridViewRow row = (GridViewRow)grdvPropietarios.Rows[e.RowIndex];

                Label lstr_IdPropietario = (Label)row.FindControl("lblIdPropietario");
                Label ldt_FchModifica = (Label)row.FindControl("lblFchModifica");

                TextBox txt_SociedadGL = (TextBox)row.FindControl("txtEditarSociedadGL");
                TextBox txt_SociedadFi = (TextBox)row.FindControl("txtEditarSociedadFi");
                TextBox txt_NomPropietario = (TextBox)row.FindControl("txtEditarNomPropietario");
                TextBox txt_Estado = (TextBox)row.FindControl("txtEditarEstado");

                string lstr_SociedadGL = String.Empty;
                string lstr_SociedadFi = String.Empty;
                string lstr_NomPropietario = String.Empty;

                if ( txt_SociedadGL != null)
                    lstr_SociedadGL = txt_SociedadGL.Text;
                if (txt_SociedadFi != null)
                    lstr_SociedadFi = txt_SociedadFi.Text;
                if (txt_NomPropietario != null)
                    lstr_NomPropietario = txt_NomPropietario.Text;

                lstr_result = ws_SGService.uwsModificarPropietario(lstr_IdPropietario.Text, lstr_SociedadGL.Trim(),
                    lstr_SociedadFi.Trim(), lstr_NomPropietario.Trim(),
                    txt_Estado.Text.Trim(), gstr_Usuario, Convert.ToDateTime(ldt_FchModifica.Text));

                if (lstr_result[0].ToString().Equals("00") || lstr_result[0].ToString().Equals("True"))
                {
                    MostarMensaje("La modificación de datos ha sido satisfactoria.", gchr_MensajeExito);
                }
                else
                {
                    MostarMensaje("La consulta de datos no ha sido satisfactoria.", gchr_MensajeError);
                }
                grdvPropietarios.EditIndex = -1;
                grdvPropietarios.DataSource = gds_Propietarios.Tables["Table"];


                ConsultarPropietarios(txtBusqIdPropietario.Text.Trim(), "", "", txtBusqNomPropietario.Text.Trim());
               // grdvPropietarios.DataBind();
                //ConsultarPropietarios(txtBusqIdPropietario.Text.Trim(), "", "", txtBusqNomPropietario.Text.Trim());
            }
            catch (Exception ex)
            {
                ConsultarPropietarios(txtBusqIdPropietario.Text.Trim(), "", "", txtBusqNomPropietario.Text.Trim());
                MostarMensaje("Error al finalizar la modificación de datos.", gchr_MensajeError);

            }
        }

        protected void grdvPropietarios_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {

        }

        protected void grdvPropietarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdvPropietarios.PageIndex = e.NewPageIndex;
            grdvPropietarios.DataSource = gds_Propietarios.Tables["Table"];

            grdvPropietarios.DataBind();
            //ConsultarPropietarios(txtBusqIdPropietario.Text.Trim(), "", "", txtBusqNomPropietario.Text.Trim());
        }

        protected void grdvPropietarios_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdvPropietarios.EditIndex = -1;
            grdvPropietarios.DataSource = gds_Propietarios.Tables["Table"];

            grdvPropietarios.DataBind();
            //ConsultarPropietarios(txtBusqIdPropietario.Text.Trim(), "", "", txtBusqNomPropietario.Text.Trim());
        }

        protected void btnPropietariosConsultar_Click(object sender, EventArgs e)
        {
            ConsultarPropietarios(txtBusqIdPropietario.Text.Trim(), "" , "", txtBusqNomPropietario.Text.Trim());
        }

        protected void btnPropietarioNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Mantenimiento/Gestiones/frmNuevoPropietario.aspx", false);
        }
        
    }
}