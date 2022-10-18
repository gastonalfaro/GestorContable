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
    public partial class frmFondos : BASE
    {
        # region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private char gchr_MensajeError;
        private char gchr_MensajeExito;
        private string gstr_Usuario = String.Empty;
        //static DataSet gds_Fondos = new DataSet();
        protected DataSet gds_Fondos
        {
            get
            {
                if (ViewState["gds_Fondos"] == null)
                    ViewState["gds_Fondos"] = new DataSet();
                return (DataSet)ViewState["gds_Fondos"];
            }
            set
            {
                ViewState["gds_Fondos"] = value;
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
                        ConsultarFondos("", "", "", "");
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

        private void ConsultarFondos(string str_IdFondo, string str_IdEntidadCP, string str_Denominacion, string str_NomFondo)
        {
            gds_Fondos = ws_SGService.uwsConsultarFondos(str_IdFondo, str_IdEntidadCP, str_Denominacion, str_NomFondo);

            if (gds_Fondos.Tables["Table"].Rows.Count > 0)
            {
                grdFondos.DataSource = gds_Fondos.Tables["Table"];
                grdFondos.DataBind();
            }
            else
            {
                grdFondos.DataSource = this.LlenarTablaVacia();
                grdFondos.DataBind();
            }
        }

        private DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("IdFondo", typeof(string));
            ldt_TablaVacia.Columns.Add("IdEntidadCP", typeof(string));
            ldt_TablaVacia.Columns.Add("Denominacion", typeof(string));
            ldt_TablaVacia.Columns.Add("NomFondo", typeof(string));
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

        protected void grdFondos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grdFondos.SelectedIndex < 0)
                return;
        }

        protected void grdFondos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdFondos.EditIndex = e.NewEditIndex;
            grdFondos.DataSource = gds_Fondos.Tables["Table"];

            grdFondos.DataBind();
            //ConsultarFondos(txtBusquedaCodigo.Text, txtBusquedaIdEntidadCP.Text, "", txtBusquedaNomFondo.Text);
        }

        protected void grdFondos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            String[] lstr_result = new String[3];
            try
            {
                DataRow ldr_FondosRow = gds_Fondos.Tables["Table"].NewRow();
                ldr_FondosRow = gds_Fondos.Tables["Table"].Rows[e.RowIndex];

                int lint_IdAcreedor = Convert.ToInt32(ldr_FondosRow["NroAcreedor"]);
                DateTime ldt_FchModifica = Convert.ToDateTime(ldr_FondosRow["FchModifica"].ToString());

                string lstr_fecha = String.Empty;
                lstr_fecha = ldt_FchModifica.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                ldt_FchModifica = Convert.ToDateTime(lstr_fecha);

                GridViewRow row = (GridViewRow)grdFondos.Rows[e.RowIndex];
                TextBox txt_TipoIdAcreedor = (TextBox)row.FindControl("txtEditTipoIdAcreedor");
                TextBox txt_Cedula = (TextBox)row.FindControl("txtEditCedula");
                TextBox txt_NomAcreedor = (TextBox)row.FindControl("txtEditNomAcreedor");
                TextBox txt_Abreviatura = (TextBox)row.FindControl("txtEditAbreviatura");
                TextBox txt_Contacto = (TextBox)row.FindControl("txtEditContacto");
                TextBox txt_Telefonos = (TextBox)row.FindControl("txtEditTelefonos");
                TextBox txt_Direccion = (TextBox)row.FindControl("txtEditDireccion");
                TextBox txt_Pais = (TextBox)row.FindControl("txtEditPais");
                TextBox txt_PaisInstitucion = (TextBox)row.FindControl("txtEditPaisInstitucion");
                TextBox txt_TipoAcreedor = (TextBox)row.FindControl("txtEditTipoAcreedor");
                TextBox txt_CatPersona = (TextBox)row.FindControl("txtEditCatPersona");
                TextBox txt_TipoPersona = (TextBox)row.FindControl("txtEditTipoPersona");
                TextBox txt_Estado = (TextBox)row.FindControl("txtEditEstado");

                //lstr_result = ws_SGService.uwsModificarAcreedor(lint_IdAcreedor, txt_Cedula.Text, txt_TipoIdAcreedor.Text, txt_NomAcreedor.Text,
                //    txt_Abreviatura.Text, txt_Contacto.Text, txt_Telefonos.Text, txt_Direccion.Text, txt_Pais.Text, txt_TipoAcreedor.Text,
                //    txt_PaisInstitucion.Text, txt_CatPersona.Text, txt_TipoPersona.Text, txt_Estado.Text, gstr_Usuario, ldt_FchModifica);
                
                if (lstr_result[0].ToString().Equals("00"))
                {
                    MostarMensaje(lstr_result[1].ToString(), gchr_MensajeExito);
                }
                else
                {
                    MostarMensaje(lstr_result[1].ToString(), gchr_MensajeExito);
                }
                grdFondos.EditIndex = -1;
                grdFondos.DataSource = gds_Fondos.Tables["Table"];

                grdFondos.DataBind();
                //ConsultarFondos(txtBusquedaCodigo.Text, txtBusquedaIdEntidadCP.Text, "", txtBusquedaNomFondo.Text);
            }
            catch (Exception ex)
            {
                ConsultarFondos(txtBusquedaCodigo.Text, txtBusquedaIdEntidadCP.Text, "", txtBusquedaNomFondo.Text);
                MostarMensaje(ex.ToString(), gchr_MensajeError);

            }
        }

        protected void grdFondos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdFondos.PageIndex = e.NewPageIndex;
            grdFondos.DataSource = gds_Fondos.Tables["Table"];

            grdFondos.DataBind();
            //ConsultarFondos(txtBusquedaCodigo.Text, txtBusquedaIdEntidadCP.Text, "", txtBusquedaNomFondo.Text);
        }

        protected void grdFondos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdFondos.EditIndex = -1;
            grdFondos.DataSource = gds_Fondos.Tables["Table"];

            grdFondos.DataBind();
            //ConsultarFondos(txtBusquedaCodigo.Text, txtBusquedaIdEntidadCP.Text, "", txtBusquedaNomFondo.Text);
        }

        protected void btnConsultarFondo_Click(object sender, EventArgs e)
        {
            OcultarMensaje();
            ConsultarFondos(txtBusquedaCodigo.Text, txtBusquedaIdEntidadCP.Text, "", txtBusquedaNomFondo.Text);
        }
    }
}