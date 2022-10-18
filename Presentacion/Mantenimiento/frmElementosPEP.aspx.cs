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
    public partial class frmElementosPEP : BASE
    {
        # region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private char gchr_MensajeError;
        private char gchr_MensajeExito;
        private string gstr_Usuario = String.Empty;
        //static DataSet gds_ElementosPEP = new DataSet();
        protected DataSet gds_ElementosPEP
        {
            get
            {
                if (ViewState["gds_ElementosPEP"] == null)
                    ViewState["gds_ElementosPEP"] = new DataSet();
                return (DataSet)ViewState["gds_ElementosPEP"];
            }
            set
            {
                ViewState["gds_ElementosPEP"] = value;
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
                        ConsultarElementosPEP("", "");
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

        private void ConsultarElementosPEP(string str_IdElementoPEP, string str_NomElementoPEP)
        {
            gds_ElementosPEP = ws_SGService.uwsConsultarElementosPEP(str_IdElementoPEP, str_NomElementoPEP);

            if (gds_ElementosPEP.Tables["Table"].Rows.Count > 0)
            {
                grdElementosPEP.DataSource = gds_ElementosPEP.Tables["Table"];
                grdElementosPEP.DataBind();
            }
            else
            {
                grdElementosPEP.DataSource = this.LlenarTablaVacia();
                grdElementosPEP.DataBind();
                grdElementosPEP.Rows[0].Visible = false;
            }
        }

        private DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("IdElementoPEP", typeof(string));
            ldt_TablaVacia.Columns.Add("NomElementoPEP", typeof(string));
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

        protected void btnCatalogoNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Mantenimiento/Gestiones/frmNuevaElementoPEP.aspx", false);
        }

        protected void btnCatalogoGuardar_Click(object sender, EventArgs e)
        {

        }

        protected void btnCatalogoVolver_Click(object sender, EventArgs e)
        {

        }

        protected void btnCatalogoConsultar_Click(object sender, EventArgs e)
        {
            OcultarMensaje();
            ConsultarElementosPEP(txtBusquedaIdElementoPEP.Text, txtBusquedaNomElementoPEP.Text);
        }

        protected void grdElementosPEP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grdElementosPEP.SelectedIndex < 0)
                return;
        }

        protected void grdElementosPEP_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdElementosPEP.EditIndex = e.NewEditIndex;
            grdElementosPEP.DataSource = gds_ElementosPEP.Tables["Table"];

            grdElementosPEP.DataBind();
            //ConsultarElementosPEP(txtBusquedaIdElementoPEP.Text, txtBusquedaNomElementoPEP.Text);
        }

        protected void grdElementosPEP_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            String[] lstr_result = new String[3];
            try
            {
                DataRow ldr_ElementosPEPRow = gds_ElementosPEP.Tables["Table"].NewRow();
                ldr_ElementosPEPRow = gds_ElementosPEP.Tables["Table"].Rows[e.RowIndex];

                int lint_IdAcreedor = Convert.ToInt32(ldr_ElementosPEPRow["NroAcreedor"]);
                DateTime ldt_FchModifica = Convert.ToDateTime(ldr_ElementosPEPRow["FchModifica"].ToString());

                string lstr_fecha = String.Empty;
                lstr_fecha = ldt_FchModifica.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                ldt_FchModifica = Convert.ToDateTime(lstr_fecha);

                GridViewRow row = (GridViewRow)grdElementosPEP.Rows[e.RowIndex];
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
                grdElementosPEP.EditIndex = -1;
                grdElementosPEP.DataSource = gds_ElementosPEP.Tables["Table"];

                grdElementosPEP.DataBind();
                //ConsultarElementosPEP(txtBusquedaIdElementoPEP.Text, txtBusquedaNomElementoPEP.Text);
            }
            catch (Exception ex)
            {
                ConsultarElementosPEP(txtBusquedaIdElementoPEP.Text, txtBusquedaNomElementoPEP.Text);
                MostarMensaje(ex.ToString(), gchr_MensajeError);

            }
        }

        protected void grdElementosPEP_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdElementosPEP.PageIndex = e.NewPageIndex;
            grdElementosPEP.DataSource = gds_ElementosPEP.Tables["Table"];

            grdElementosPEP.DataBind();
            //ConsultarElementosPEP(txtBusquedaIdElementoPEP.Text, txtBusquedaNomElementoPEP.Text);
        }

        protected void grdElementosPEP_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdElementosPEP.EditIndex = -1;
            grdElementosPEP.DataSource = gds_ElementosPEP.Tables["Table"];

            grdElementosPEP.DataBind();
            //ConsultarElementosPEP(txtBusquedaIdElementoPEP.Text, txtBusquedaNomElementoPEP.Text);
        }

        protected void btnConsultarElementoPEP_Click(object sender, EventArgs e)
        {
            OcultarMensaje();
            ConsultarElementosPEP(txtBusquedaIdElementoPEP.Text, txtBusquedaNomElementoPEP.Text);
        }

    }
}