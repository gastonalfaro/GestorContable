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
    public partial class frmSociedadesFi : BASE
    {
        # region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private char gchr_MensajeError;
        private char gchr_MensajeExito;
        private string gstr_Usuario = String.Empty;
        //static DataSet gds_SociedadesFi = new DataSet();
        protected DataSet gds_SociedadesFi
        {
            get
            {
                if (ViewState["gds_SociedadesFi"] == null)
                    ViewState["gds_SociedadesFi"] = new DataSet();
                return (DataSet)ViewState["gds_SociedadesFi"];
            }
            set
            {
                ViewState["gds_SociedadesFi"] = value;
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
                    if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmSociedadesFi"))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                    {

                        OcultarMensaje();
                        ConsultarSociedadesFi("", "", "", "", "", "");
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

        private void ConsultarSociedadesFi(string str_IdSociedadFi, string str_IdSociedadGL, string str_Denominacion, string str_NomSociedad, string str_IdPais, string str_IdMoneda)
        {
            gds_SociedadesFi = ws_SGService.uwsConsultarSociedadesFinancieras(str_IdSociedadFi, str_IdSociedadGL, str_Denominacion, str_NomSociedad, str_IdPais, str_IdMoneda);

            if (gds_SociedadesFi.Tables["Table"].Rows.Count > 0)
            {
                gvpSociedadesFi.DataSource = gds_SociedadesFi.Tables["Table"];
                gvpSociedadesFi.DataBind();
            }
            else
            {
                gvpSociedadesFi.DataSource = this.LlenarTablaVacia();
                gvpSociedadesFi.DataBind();
                gvpSociedadesFi.Rows[0].Visible = false;
            }
        }

        private DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("IdSociedadFi", typeof(string));
            ldt_TablaVacia.Columns.Add("IdSociedadGL", typeof(string));
            ldt_TablaVacia.Columns.Add("Denominacion", typeof(string));
            ldt_TablaVacia.Columns.Add("NomSociedad", typeof(string));
            ldt_TablaVacia.Columns.Add("IdPais", typeof(string));
            ldt_TablaVacia.Columns.Add("Poblacion", typeof(string));
            ldt_TablaVacia.Columns.Add("IdMoneda", typeof(string));
            ldt_TablaVacia.Columns.Add("IdIdioma", typeof(string));
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
            Response.Redirect("~/Mantenimiento/Gestiones/frmNuevaSociedadFi.aspx", false);
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
            ConsultarSociedadesFi(txtBusqIdSociedadFi.Text, "", "", txtBusqNomSociedad.Text, txtIdPais.Text, txtBusqIdMoneda.Text);
        }

        protected void gvpSociedadesFi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gvpSociedadesFi.SelectedIndex < 0)
                return;
        }

        protected void gvpSociedadesFi_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvpSociedadesFi.EditIndex = e.NewEditIndex;
            gvpSociedadesFi.DataSource = gds_SociedadesFi.Tables["Table"];

            gvpSociedadesFi.DataBind();
            //ConsultarSociedadesFi("", "", "", "", "", "");
        }

        protected void gvpSociedadesFi_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            String[] lstr_result = new String[3];
            try
            {
                DataRow ldr_SociedadesFiRow = gds_SociedadesFi.Tables["Table"].NewRow();
                ldr_SociedadesFiRow = gds_SociedadesFi.Tables["Table"].Rows[e.RowIndex];

                int lint_IdAcreedor = Convert.ToInt32(ldr_SociedadesFiRow["NroAcreedor"]);
                DateTime ldt_FchModifica = Convert.ToDateTime(ldr_SociedadesFiRow["FchModifica"].ToString());

                string lstr_fecha = String.Empty;
                lstr_fecha = ldt_FchModifica.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                ldt_FchModifica = Convert.ToDateTime(lstr_fecha);

                GridViewRow row = (GridViewRow)gvpSociedadesFi.Rows[e.RowIndex];
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
                gvpSociedadesFi.EditIndex = -1;
                gvpSociedadesFi.DataSource = gds_SociedadesFi.Tables["Table"];

                gvpSociedadesFi.DataBind();
                //ConsultarSociedadesFi("", "", "", "", "", "");
            }
            catch (Exception ex)
            {
                ConsultarSociedadesFi("", "", "", "", "", "");
                MostarMensaje(ex.ToString(), gchr_MensajeError);

            }
        }

        protected void gvpSociedadesFi_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvpSociedadesFi.PageIndex = e.NewPageIndex;
            gvpSociedadesFi.DataSource = gds_SociedadesFi.Tables["Table"];

            gvpSociedadesFi.DataBind();
            //ConsultarSociedadesFi("", "", "", "", "", "");
        }

        protected void gvpSociedadesFi_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvpSociedadesFi.EditIndex = -1;
            gvpSociedadesFi.DataSource = gds_SociedadesFi.Tables["Table"];

            gvpSociedadesFi.DataBind();
            //ConsultarSociedadesFi("", "", "", "", "", "");
        }

        protected void btnSociedadFiConsultar_Click(object sender, EventArgs e)
        {
            OcultarMensaje();
            ConsultarSociedadesFi(txtBusqIdSociedadFi.Text, txtBusqIdSociedadGL.Text, "", txtBusqNomSociedad.Text, txtIdPais.Text, txtBusqIdMoneda.Text);
        
        }

        protected void btnSociedadesFiVolver_Click(object sender, EventArgs e)
        {

        }

        protected void gvpSociedadesFi_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {

        }
    }
}