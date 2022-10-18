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
    public partial class frmOpciones : BASE
    {
        # region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private char gchr_MensajeError;
        private char gchr_MensajeExito;

        private string gstr_ModuloActual = String.Empty;
        private string gstr_Usuario = String.Empty;
        private string gstr_IdCatalogo = String.Empty;
        private string gstr_NomCatalogo = String.Empty;

        private DateTime gdt_FechaModifica = new DateTime();
        private DateTime gdt_FchModificaSocGL = new DateTime();

        //private static DataSet gds_Opciones = new DataSet();
        protected DataSet gds_Opciones
        {
            get
            {
                if (ViewState["gds_Opciones"] == null)
                    ViewState["gds_Opciones"] = new DataSet();
                return (DataSet)ViewState["gds_Opciones"];
            }
            set
            {
                ViewState["gds_Opciones"] = value;
            }
        }
        

        # endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            gstr_Usuario = clsSesion.Current.LoginUsuario;
            gchr_MensajeError = clsSesion.Current.chr_MensajeError;
            gchr_MensajeExito = clsSesion.Current.chr_MensajeExito;

            gstr_ModuloActual = clsSesion.Current.gstr_ModuloActual;

            gstr_IdCatalogo = clsSesion.Current.IdCatalogo;
            gstr_NomCatalogo = clsSesion.Current.NomCatalogo;

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(gstr_Usuario))
                {
                    MostrarElementos(gstr_Usuario);
                    OcultarMensaje();
                    txtCatalogo.Text = gstr_NomCatalogo;
                    ConsultarOpciones(gstr_IdCatalogo, "" , "", "");

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

        public static Control FindControlRecursive(Control root, string id)
        {
            if (id == string.Empty)
                return null;

            if (root.ID == id)
                return root;

            foreach (Control c in root.Controls)
            {
                Control t = FindControlRecursive(c, id);
                if (t != null)
                {
                    return t;
                }
            }
            return null;
        }

        private void MostrarElementos(string str_usuario)
        {
            DataSet ldt_PermisosUsuario = ws_SGService.uwsConsultarPermisosUsuarios(str_usuario, "");

            for (int i = 0; ldt_PermisosUsuario.Tables["Table"].Rows.Count > i; i++)
            {
                string lstr_IdObjeto = ldt_PermisosUsuario.Tables["Table"].Rows[i]["IdObjeto"].ToString();
                bool lbool_Actualizar = (bool)ldt_PermisosUsuario.Tables["Table"].Rows[i]["Actualizar"];
                bool lbool_Consultar = (bool)ldt_PermisosUsuario.Tables["Table"].Rows[i]["Consultar"];
                string lstr_IdliObjeto = "li" + lstr_IdObjeto;

                if (lbool_Consultar)
                {
                    HtmlGenericControl hgcElementoObjeto = (HtmlGenericControl)FindControlRecursive(Master.Page, lstr_IdliObjeto);

                    if (hgcElementoObjeto != null)
                        hgcElementoObjeto.Visible = true;
                }
            }
        }

        private void ConsultarOpciones(string str_IdCatalogo, string str_AbrevCatalogo, string str_IdOpcion, string str_NomOpcion)
        {
            gds_Opciones = ws_SGService.uwsConsultarOpcionesCatalogo(str_IdCatalogo, str_AbrevCatalogo, str_IdOpcion, str_NomOpcion);

            if (gds_Opciones.Tables["Table"].Rows.Count > 0)
            {
                grdvOpciones.DataSource = gds_Opciones.Tables["Table"];

                grdvOpciones.DataBind();
            }
            else
            {
                grdvOpciones.DataSource = this.LlenarTablaVacia();
                grdvOpciones.DataBind();
            }            
        }

        private DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("IdCatalogo", typeof(string));
            ldt_TablaVacia.Columns.Add("IdOpcion", typeof(string));
            ldt_TablaVacia.Columns.Add("ValOpcion", typeof(string));
            ldt_TablaVacia.Columns.Add("NomOpcion", typeof(string));
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

        }

        protected void btnCatalogoGuardar_Click(object sender, EventArgs e)
        {

        }

        protected void btnCatalogoVolver_Click(object sender, EventArgs e)
        {

        }

        protected void grdvOpciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grdvOpciones.SelectedIndex < 0)
                return;
            String[] lstr_resultado = new String[3];
           
            int lstr_IdCatalogo = Convert.ToInt32(gstr_IdCatalogo);
            int lstr_IdOpcion = Convert.ToInt32( (grdvOpciones.FooterRow.FindControl("txtInsertIdOpcion") as TextBox).Text);
            string lstr_NomOpcion = (grdvOpciones.FooterRow.FindControl("txtNomNuevoOpcion") as TextBox).Text;
            string lstr_Valor= (grdvOpciones.FooterRow.FindControl("txtNomNuevoValor") as TextBox).Text;
            string lstr_Estado = (grdvOpciones.FooterRow.FindControl("txtEstado") as TextBox).Text;

            lstr_resultado = ws_SGService.uwsCrearOpcionCatalogo(lstr_IdCatalogo, lstr_IdOpcion, lstr_Valor, lstr_NomOpcion, lstr_Estado, gstr_Usuario);
            if (lstr_resultado[0].ToString().Equals("00") || lstr_resultado[0].ToString().Equals("True"))
            {
                MostarMensaje(lstr_resultado[1].ToString(), gchr_MensajeError);
            }
            else
            {
                MostarMensaje(lstr_resultado[1].ToString(), gchr_MensajeExito);
            }
        }

        protected void grdvOpciones_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdvOpciones.EditIndex = e.NewEditIndex;
            grdvOpciones.DataSource = gds_Opciones.Tables["Table"];

            grdvOpciones.DataBind();
            //ConsultarOpciones(gstr_IdCatalogo, "", "", "");
        }

        protected void grdvOpciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdvOpciones.PageIndex = e.NewPageIndex;
            grdvOpciones.DataSource = gds_Opciones.Tables["Table"];

            grdvOpciones.DataBind();
            //ConsultarOpciones(gstr_IdCatalogo, "", "", "");
        }

        protected void grdvOpciones_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            String[] lstr_result = new String[3];
            try
            {
                DataRow ldr_OpcionesRow = gds_Opciones.Tables["Table"].NewRow();
                ldr_OpcionesRow = gds_Opciones.Tables["Table"].Rows[e.RowIndex];

                int lstr_IdOpcion = Convert.ToInt32( ldr_OpcionesRow["IdOpcion"].ToString());
                int lstr_IdCatalogo = Convert.ToInt32(gstr_IdCatalogo);
                DateTime ldt_FchModifica = Convert.ToDateTime(ldr_OpcionesRow["FchModifica"].ToString());

                string str_fecha = String.Empty;
                str_fecha = ldt_FchModifica.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                ldt_FchModifica = Convert.ToDateTime(str_fecha);

                GridViewRow row = (GridViewRow)grdvOpciones.Rows[e.RowIndex];

                TextBox txt_ValOpcion = (TextBox)row.FindControl("txtEditValor");
                TextBox txt_NomOpcion = (TextBox)row.FindControl("txtEditNombreOpcion");
                TextBox txt_Estado = (TextBox)row.FindControl("txtEditEstado");

                lstr_result = ws_SGService.uwsModificarOpcionCatalogo(lstr_IdCatalogo, lstr_IdOpcion,
                    txt_ValOpcion.Text, txt_NomOpcion.Text, txt_Estado.Text, gstr_Usuario, ldt_FchModifica);

                if (lstr_result[0].ToString().Equals("00"))
                {
                    MostarMensaje(lstr_result[1].ToString(), gchr_MensajeExito);
                }
                else
                {
                    MostarMensaje("Error: " + lstr_result[1].ToString(), gchr_MensajeError);
                }
                grdvOpciones.EditIndex = -1;
                grdvOpciones.DataSource = gds_Opciones.Tables["Table"];

                grdvOpciones.DataBind();
                //ConsultarOpciones(gstr_IdCatalogo, "", "", "");
            }
            catch (Exception ex)
            {
                ConsultarOpciones(gstr_IdCatalogo, "", "", "");
                MostarMensaje(ex.ToString(), gchr_MensajeError);

            }
        }

        protected void grdvOpciones_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdvOpciones.EditIndex = -1;
            grdvOpciones.DataSource = gds_Opciones.Tables["Table"];

            grdvOpciones.DataBind();
            //ConsultarOpciones(gstr_IdCatalogo, "", "", "");
        }
    }
}