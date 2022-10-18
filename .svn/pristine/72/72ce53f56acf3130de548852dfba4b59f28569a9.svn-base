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
    public partial class frmClasesDocumento : BASE
    {
        # region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private char gchr_MensajeError;
        private char gchr_MensajeExito;
        private string gstr_Usuario = String.Empty;
        //static DataSet gds_ClasesDocumento = new DataSet();
        protected DataSet gds_ClasesDocumento
        {
            get
            {
                if (ViewState["gds_ClasesDocumento"] == null)
                    ViewState["gds_ClasesDocumento"] = new DataSet();
                return (DataSet)ViewState["gds_ClasesDocumento"];
            }
            set
            {
                ViewState["gds_ClasesDocumento"] = value;
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
                        ConsultarClasesDocumento("", "");
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
        
        private void ConsultarClasesDocumento(string str_IdClaseDocumento, string str_NomClaseDocumento)
        {
            gds_ClasesDocumento = ws_SGService.uwsConsultarClasesDocumento(str_IdClaseDocumento, str_NomClaseDocumento);

            if (gds_ClasesDocumento.Tables["Table"].Rows.Count > 0)
            {
                grdvClasesDocumento.DataSource = gds_ClasesDocumento.Tables["Table"];
                grdvClasesDocumento.DataBind();
            }
            else
            {
                grdvClasesDocumento.DataSource = this.LlenarTablaVacia();
                grdvClasesDocumento.DataBind();
            }
        }

        private DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("IdClaseDoc", typeof(string));
            ldt_TablaVacia.Columns.Add("NomClaseDoc", typeof(string));
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

        protected void btnClaseDocumentoNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Mantenimiento/Gestiones/frmNuevoClaseDocumentoGeneral.aspx", false);
        }

        protected void btnClaseDocumentoGuardar_Click(object sender, EventArgs e)
        {

        }

        protected void btnClaseDocumentoVolver_Click(object sender, EventArgs e)
        {

        }

        protected void btnClaseDocumentoConsultar_Click(object sender, EventArgs e)
        {
            OcultarMensaje();
            ConsultarClasesDocumento(txtBusqIdClaseDocumento.Text, txtBusqNomClaseDocumento.Text);
        }

        protected void grdvClasesDocumento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grdvClasesDocumento.SelectedIndex < 0)
                return;
        }

        protected void grdvClasesDocumento_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdvClasesDocumento.EditIndex = e.NewEditIndex;
            grdvClasesDocumento.DataSource = gds_ClasesDocumento.Tables["Table"];

            grdvClasesDocumento.DataBind();
            //ConsultarClasesDocumento(txtBusqIdClaseDocumento.Text, txtBusqNomClaseDocumento.Text);
          
        }

        protected void grdvClasesDocumento_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            String[] lstr_result = new String[3];
            try
            {
                DataRow ldr_ClasesDocumentoRow = gds_ClasesDocumento.Tables["Table"].NewRow();
                ldr_ClasesDocumentoRow = gds_ClasesDocumento.Tables["Table"].Rows[e.RowIndex];

                string lint_IdClasesDocumento = ldr_ClasesDocumentoRow["IdClaseDocumento"].ToString();
                DateTime ldt_FchModifica = Convert.ToDateTime(ldr_ClasesDocumentoRow["FchModifica"].ToString());

                string str_fecha = String.Empty;
                str_fecha = ldt_FchModifica.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                ldt_FchModifica = Convert.ToDateTime(str_fecha);

                GridViewRow row = (GridViewRow)grdvClasesDocumento.Rows[e.RowIndex];

                TextBox txt_Nombre = (TextBox)row.FindControl("txtEditNombre");
                TextBox txt_Estado = (TextBox)row.FindControl("txtEditEstado");

                //lstr_result = ws_SGService.uwsModificarClaseDocumento(lint_IdClasesDocumento, txt_Nombre.Text, "jnet", ldt_FchModifica);

                if (lstr_result[0].ToString().Equals("00"))
                {
                    MostarMensaje(lstr_result[1].ToString(), gchr_MensajeExito);
                }
                else
                {
                    MostarMensaje("Error: " + lstr_result[1].ToString(), gchr_MensajeError);
                }
                grdvClasesDocumento.EditIndex = -1;
                grdvClasesDocumento.DataSource = gds_ClasesDocumento.Tables["Table"];

                grdvClasesDocumento.DataBind();
                //ConsultarClasesDocumento(txtBusqIdClaseDocumento.Text, txtBusqNomClaseDocumento.Text);
            }
            catch (Exception ex)
            {
                ConsultarClasesDocumento(txtBusqIdClaseDocumento.Text, txtBusqNomClaseDocumento.Text);
                MostarMensaje(ex.ToString(), gchr_MensajeError);

            }
        }

        protected void grdvClasesDocumento_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdvClasesDocumento.PageIndex = e.NewPageIndex;
            grdvClasesDocumento.DataSource = gds_ClasesDocumento.Tables["Table"];

            grdvClasesDocumento.DataBind();
            //ConsultarClasesDocumento(txtBusqIdClaseDocumento.Text, txtBusqNomClaseDocumento.Text);
        }

        protected void grdvClasesDocumento_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdvClasesDocumento.EditIndex = -1;
            grdvClasesDocumento.DataSource = gds_ClasesDocumento.Tables["Table"];

            grdvClasesDocumento.DataBind();
            //ConsultarClasesDocumento(txtBusqIdClaseDocumento.Text, txtBusqNomClaseDocumento.Text);
        }



    }
}