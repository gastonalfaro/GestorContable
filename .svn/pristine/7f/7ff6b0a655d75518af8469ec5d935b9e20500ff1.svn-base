using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using Presentacion.Compartidas;
using System.Web.UI.HtmlControls;

namespace Presentacion.Mantenimiento
{
    public partial class frmClavesContables : BASE
    {
        # region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private char gchr_MensajeError;
        private char gchr_MensajeExito;
        private string gstr_Usuario = String.Empty;
        //static DataSet gds_ClavesContables = new DataSet();
        protected DataSet gds_ClavesContables
        {
            get
            {
                if (ViewState["gds_ClavesContables"] == null)
                    ViewState["gds_ClavesContables"] = new DataSet();
                return (DataSet)ViewState["gds_ClavesContables"];
            }
            set
            {
                ViewState["gds_ClavesContables"] = value;
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
                        ConsultarClavesContables(0, "");

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
        
        private void ConsultarClavesContables(int int_IdClavesContables, string str_Nombre)
        {
            if (int_IdClavesContables == null)
                int_IdClavesContables = 0;

            //gds_ClavesContables = ws_SGService.uwsConsultarClasesDocumento();

            if (gds_ClavesContables.Tables["Table"].Rows.Count > 0)
            {
                grdvClavesContables.DataSource = gds_ClavesContables.Tables["Table"];
                grdvClavesContables.DataBind();
            }
            else
            {
                grdvClavesContables.DataSource = this.LlenarTablaVacia();
                grdvClavesContables.DataBind();
                grdvClavesContables.Rows[0].Visible = false;
            }
            
        }

        private DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("IdClaveContable", typeof(string));
            ldt_TablaVacia.Columns.Add("Nombre", typeof(string));
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

        protected void btnClaveContableNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Mantenimiento/Gestiones/frmNuevoClaveContableGeneral.aspx", false);
        }

        protected void btnClaveContableGuardar_Click(object sender, EventArgs e)
        {

        }

        protected void btnClaveContableVolver_Click(object sender, EventArgs e)
        {

        }

        protected void btnClaveContableConsultar_Click(object sender, EventArgs e)
        {
            OcultarMensaje();
            ConsultarClavesContables(Convert.ToInt32(txtBusqIdClaveContable.Text), txtBusqNomClaveContable.Text);
        }

        protected void grdvClavesContables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grdvClavesContables.SelectedIndex < 0)
                return;
        }

        protected void grdvClavesContables_RowEditing(object sender, GridViewEditEventArgs e)
        {
                grdvClavesContables.EditIndex = e.NewEditIndex;
                ConsultarClavesContables(0, "");
        }

        protected void grdvClavesContables_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            String[] lstr_result = new String[3];
            try
            {
                DataRow ldr_ClavesContablesRow = gds_ClavesContables.Tables["Table"].NewRow();
                ldr_ClavesContablesRow = gds_ClavesContables.Tables["Table"].Rows[e.RowIndex];

                int lint_IdClavesContables = (int)ldr_ClavesContablesRow["IdClaveContable"];
                DateTime ldt_FchModifica = Convert.ToDateTime(ldr_ClavesContablesRow["FchModifica"].ToString());

                string str_fecha = String.Empty;
                str_fecha = ldt_FchModifica.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                ldt_FchModifica = Convert.ToDateTime(str_fecha);

                GridViewRow row = (GridViewRow)grdvClavesContables.Rows[e.RowIndex];

                TextBox txt_Nombre = (TextBox)row.FindControl("txtEditNombre");
                TextBox txt_Estado = (TextBox)row.FindControl("txtEditEstado");

                //lstr_result = ws_SGService.uwsModificarClaveContable(lint_IdClavesContables, txt_Nombre.Text, txt_Nombre.Text, txt_Estado.Text, "jnet", ldt_FchModifica);

                if (lstr_result[0].ToString().Equals("00"))
                {
                    MostarMensaje(lstr_result[1].ToString(), gchr_MensajeExito);
                }
                else
                {
                    MostarMensaje("Error: " + lstr_result[1].ToString(), gchr_MensajeError);
                }
                grdvClavesContables.EditIndex = -1;
                grdvClavesContables.DataSource = gds_ClavesContables.Tables["Table"];

                grdvClavesContables.DataBind();
                //ConsultarClavesContables(0, "");
            }
            catch (Exception ex)
            {
                ConsultarClavesContables(0, "");
                MostarMensaje(ex.ToString(), gchr_MensajeError);

            }
        }

        protected void grdvClavesContables_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdvClavesContables.PageIndex = e.NewPageIndex;
            grdvClavesContables.DataSource = gds_ClavesContables.Tables["Table"];

            grdvClavesContables.DataBind();
            //ConsultarClavesContables(0, "");
        }

        protected void grdvClavesContables_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdvClavesContables.EditIndex = -1;
            grdvClavesContables.DataSource = gds_ClavesContables.Tables["Table"];

            grdvClavesContables.DataBind();
            //ConsultarClavesContables(0, "");
        }

    }
}