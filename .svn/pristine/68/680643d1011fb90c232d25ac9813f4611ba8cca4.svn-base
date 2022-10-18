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
    public partial class frmCuentasContables : BASE
    {
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private char gchr_MensajeError;
        private char gchr_MensajeExito;
        private string gstr_Usuario = String.Empty;

        //static DataSet gds_CuentasContables = new DataSet();
        protected DataSet gds_CuentasContables
        {
            get
            {
                if (ViewState["gds_CuentasContables"] == null)
                    ViewState["gds_CuentasContables"] = new DataSet();
                return (DataSet)ViewState["gds_CuentasContables"];
            }
            set
            {
                ViewState["gds_CuentasContables"] = value;
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
                        ConsultarCuentasContables("", "", "", "", "", "", "", "");
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

        private void ConsultarCuentasContables(string str_IdCuentaContable, string str_IdPlanCuenta, string str_IdGrupoCuenta, string str_NomCuenta, string str_CuentaGrupo, string str_IndTotales, string str_IndConsolidacion, string str_IdSociedadFi)
        {
            gds_CuentasContables = ws_SGService.uwsConsultarCuentasContables(str_IdCuentaContable, str_IdPlanCuenta, str_IdGrupoCuenta, str_NomCuenta, str_CuentaGrupo, str_IndTotales, str_IndConsolidacion, str_IdSociedadFi);

            if (gds_CuentasContables.Tables["Table"].Rows.Count > 0)
            {
                grdvCuentasContables.DataSource = gds_CuentasContables.Tables["Table"];
                grdvCuentasContables.DataBind();
            }
            else
            {
                grdvCuentasContables.DataSource = this.LlenarTablaVacia();
                grdvCuentasContables.DataBind();
            }
        }

        private DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("IdCuentaContable", typeof(string));
            ldt_TablaVacia.Columns.Add("IdPlanCuenta", typeof(string));
            ldt_TablaVacia.Columns.Add("IdGrupoCuenta", typeof(string));
            ldt_TablaVacia.Columns.Add("NomCorto", typeof(string));
            ldt_TablaVacia.Columns.Add("NomCuentaContable", typeof(string));
            ldt_TablaVacia.Columns.Add("CuentaGrupo", typeof(string));
            ldt_TablaVacia.Columns.Add("IndTotales", typeof(string));
            ldt_TablaVacia.Columns.Add("IndConsolidacion", typeof(string));
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

        protected void btnCuentasContablesNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Mantenimiento/Gestion CuentasContables/frmNuevoCuentasContable.aspx", false);
        }

        protected void btnCuentasContablesGuardar_Click(object sender, EventArgs e)
        {

        }

        protected void btnCuentasContablesVolver_Click(object sender, EventArgs e)
        {

        }

        protected void btnCuentasContablesConsultar_Click(object sender, EventArgs e)
        {
            OcultarMensaje();

            ConsultarCuentasContables(this.txtIdCuentaContable.Text, this.txtIdPlanCuenta.Text, this.txtIdGrupoCuenta.Text,
                this.txtNomCuentaContable.Text, this.txtCuentaGrupo.Text, "", "", this.txtSociedad.Text);

        }

        protected void grdvCuentasContables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grdvCuentasContables.SelectedIndex < 0)
                return;
        }

        protected void grdvCuentasContables_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            String[] lstr_result = new String[3];
            try
            {
                DataRow ldr_CuentasContablesRow = gds_CuentasContables.Tables["Table"].NewRow();
                ldr_CuentasContablesRow = gds_CuentasContables.Tables["Table"].Rows[e.RowIndex];

                string lstr_IdCuentaContable = ldr_CuentasContablesRow["IdCuentaContable"].ToString();
                string lstr_IdPlanCuenta = ldr_CuentasContablesRow["IdPlanCuenta"].ToString();
                DateTime ldt_FchModifica = Convert.ToDateTime(ldr_CuentasContablesRow["FchModifica"].ToString());

                string lstr_fecha = String.Empty;
                lstr_fecha = ldt_FchModifica.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                ldt_FchModifica = Convert.ToDateTime(lstr_fecha);

                GridViewRow lgrdv_CuentasContablesRow = (GridViewRow)grdvCuentasContables.Rows[e.RowIndex];
                TextBox txt_IdGrupoCuenta = (TextBox)lgrdv_CuentasContablesRow.FindControl("txtEditIdGrupoCuenta");
                TextBox txt_NomCorto = (TextBox)lgrdv_CuentasContablesRow.FindControl("txtEditNomCorto");
                TextBox txt_NomCuentaContable = (TextBox)lgrdv_CuentasContablesRow.FindControl("txtEditNomCuentaContable");
                TextBox txt_CuentaGrupo = (TextBox)lgrdv_CuentasContablesRow.FindControl("txtEditCuentaGrupo");
                TextBox txt_Estado = (TextBox)lgrdv_CuentasContablesRow.FindControl("txtEditEstado");

                //lstr_result = ws_SGService.uwsModificar
                if (lstr_result[0].ToString().Equals("00"))
                {
                    MostarMensaje(lstr_result[1].ToString(), gchr_MensajeExito);
                }
                else
                {
                    MostarMensaje(lstr_result[1].ToString(), gchr_MensajeExito);
                }
                grdvCuentasContables.EditIndex = -1;
                grdvCuentasContables.DataSource = gds_CuentasContables.Tables["Table"];

                grdvCuentasContables.DataBind();
                //ConsultarCuentasContables("", "", "", "", "", "", "", "");
            }
            catch (Exception ex)
            {
                ConsultarCuentasContables("", "", "", "", "", "", "", "");
                MostarMensaje(ex.ToString(), gchr_MensajeError);

            }
        }

        protected void grdvCuentasContables_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            //DataSet lds_CuentasContables = ws_SGService.uwsConsultarCuentasContables("", "", "", "", "", "", "", "");
        }


        protected void grdvCuentasContables_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdvCuentasContables.PageIndex = e.NewPageIndex;
            grdvCuentasContables.DataSource = gds_CuentasContables.Tables["Table"];

            grdvCuentasContables.DataBind();

            //ConsultarCuentasContables(this.txtIdCuentaContable.Text, this.txtIdPlanCuenta.Text, this.txtIdGrupoCuenta.Text,
            //    this.txtNomCuentaContable.Text, this.txtCuentaGrupo.Text, "", "", this.txtSociedad.Text);

        }

        protected void grdvCuentasContables_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdvCuentasContables.EditIndex = -1;
            grdvCuentasContables.DataSource = gds_CuentasContables.Tables["Table"];

            grdvCuentasContables.DataBind();

            //ConsultarCuentasContables(this.txtIdCuentaContable.Text, this.txtIdPlanCuenta.Text, this.txtIdGrupoCuenta.Text,
            //    this.txtNomCuentaContable.Text, this.txtCuentaGrupo.Text, "", "", this.txtSociedad.Text);
        }

        protected void grdvCuentasContables_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }


    }
}