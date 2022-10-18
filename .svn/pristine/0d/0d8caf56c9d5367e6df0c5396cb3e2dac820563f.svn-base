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
    public partial class frmOpcionesCatalogo : BASE
    {
        # region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private Char gchr_MensajeError;
        private Char gchr_MensajeExito;

        private String gstr_ModuloActual = String.Empty;
        private String gstr_Usuario = String.Empty;
        private String gstr_NomCatalogo = String.Empty;
        private String[] larr_Modulos;
        DataSet dsCatalogo;

        private String gstr_IdCatalogo 
        {
            get 
            {
                if (ViewState["txtCatalogo"] == null)
                    ViewState["txtCatalogo"] = new TextBox();
                return (string)ViewState["txtCatalogo"];
            }
            set 
            {
                ViewState["txtCatalogo"] = value;
            }
        }
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

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(gstr_Usuario))
                {
                    larr_Modulos = clsSesion.Current.PermisosModulos;

                    if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmOpcionesCatalogo"))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                    {
                        gstr_IdCatalogo = clsSesion.Current.IdCatalogo;
                        dsCatalogo= ws_SGService.uwsConsultarCatalogos(Int32.Parse(clsSesion.Current.IdCatalogo), "", "", "");
                        gstr_NomCatalogo = dsCatalogo.Tables[0].Rows[0][2].ToString();//clsSesion.Current.NomCatalogo;

                        txtCatalogo.Text = gstr_IdCatalogo.Trim();
                        txtNomCatalogo.Text = gstr_NomCatalogo.Trim();
                        ckbActivo.Checked = dsCatalogo.Tables[0].Rows[0][4].ToString().Trim().Equals("A");
                        ConsultarOpciones(gstr_IdCatalogo);
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

        private void ConsultarOpciones(string str_IdCatalogo)
        {
            gds_Opciones = ws_SGService.uwsConsultarOpcionesCatalogo(gstr_IdCatalogo.Trim(), "", "", "");


            if (gds_Opciones.Tables["Table"].Rows.Count > 0)
            {
                grvOpciones.DataSource = gds_Opciones.Tables["Table"];
                grvOpciones.DataBind();
            }
            else
            {
                grvOpciones.DataSource = this.LlenarTablaVacia();
                grvOpciones.DataBind();
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
            ldt_TablaVacia.Columns.Add("FchModifica", typeof(string));

            DataRow ldr_FilaTabla = ldt_TablaVacia.NewRow();
            ldt_TablaVacia.Rows.Add(ldr_FilaTabla);
            return ldt_TablaVacia;
        }

        protected void btnVolverCatalogos_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Mantenimiento/frmCatalogosGenerales.aspx", true);
        }

        protected void grvOpciones_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grvOpciones.EditIndex = e.NewEditIndex;
            grvOpciones.DataSource = gds_Opciones.Tables["Table"];

            grvOpciones.DataBind();
            //ConsultarOpciones(gstr_IdCatalogo.Trim());
        }

        protected void grvOpciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvOpciones.PageIndex = e.NewPageIndex + 1;
            grvOpciones.DataSource = gds_Opciones.Tables["Table"];

            grvOpciones.DataBind();
            //ConsultarOpciones(gstr_IdCatalogo.Trim());
        }

        protected void grvOpciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            String[] lstr_resultado = new String[3];
                
            int lint_IdOpcion = Convert.ToInt32( (grvOpciones.FooterRow.FindControl("txtInsertarIdOpcion") as TextBox).Text);
            string lsrt_ValOpcion = (grvOpciones.FooterRow.FindControl("txtInsertarValOpcion") as TextBox).Text;
            string lstr_NomOpcion = (grvOpciones.FooterRow.FindControl("txtInsertarNomOpcion") as TextBox).Text;
            string lstr_Estado = (grvOpciones.FooterRow.FindControl("txtInsertarEstado") as TextBox).Text;

            lstr_resultado = ws_SGService.uwsCrearOpcionCatalogo(Convert.ToInt32(gstr_IdCatalogo.Trim()), lint_IdOpcion,
                lsrt_ValOpcion, lstr_NomOpcion, lstr_Estado,  gstr_Usuario);
            if (lstr_resultado[0].ToString().Equals("00") || lstr_resultado[0].ToString().Equals("True"))
            {
                MostarMensaje("La creación de datos ha sido satisfactoria.", gchr_MensajeExito);
            }
            else
            {  
                MostarMensaje("La creación de datos no ha sido satisfactoria.", gchr_MensajeError);
            }
            ConsultarOpciones(gstr_IdCatalogo.Trim());
        }

        protected void grvOpciones_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            String[] lstr_result = new String[3];
            try
            {
                int lint_IdCatalogo = Convert.ToInt32(gstr_IdCatalogo);

                GridViewRow row = (GridViewRow) grvOpciones.Rows[e.RowIndex];

                Label lbl_IdOpcion = (Label)row.FindControl("lblIdOpcion");
                TextBox txt_Valor = (TextBox) row.FindControl("txtEditarValOpcion");
                TextBox txt_Nombre = (TextBox) row.FindControl("txtEditarNomOpcion");
                TextBox txt_Estado = (TextBox) row.FindControl("txtEditarEstado");
                Label lbl_FchModificacion = (Label)row.FindControl("lblFchModificacion");

                int lint_IdOpcion = Convert.ToInt32(lbl_IdOpcion.Text.Trim());

                DateTime ldt_FchModifica = Convert.ToDateTime(lbl_FchModificacion.Text);

                string str_fecha = String.Empty;
                str_fecha = ldt_FchModifica.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                ldt_FchModifica = Convert.ToDateTime(str_fecha);

                lstr_result = ws_SGService.uwsModificarOpcionCatalogo(lint_IdCatalogo, lint_IdOpcion, txt_Valor.Text.Trim(), txt_Nombre.Text, txt_Estado.Text, gstr_Usuario, ldt_FchModifica);

                if (lstr_result[0].ToString().Equals("00") || lstr_result[0].ToString().Equals("True"))
                {
                    MostarMensaje("La modificación de datos ha sido satisfactoria.", gchr_MensajeExito);
                }
                else
                {
                    MostarMensaje("La modificación de datos no ha sido satisfactoria.", gchr_MensajeError);
                }
                grvOpciones.EditIndex = -1;
                grvOpciones.DataSource = gds_Opciones.Tables["Table"];

                grvOpciones.DataBind();
                //ConsultarOpciones(gstr_IdCatalogo.Trim());
            }
            catch (Exception ex)
            {
                ConsultarOpciones(gstr_IdCatalogo.Trim());
                MostarMensaje("La modificación de datos no ha sido satisfactoria.", gchr_MensajeError);
            }
        }

        protected void grvOpciones_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grvOpciones.EditIndex = -1;
            grvOpciones.DataSource = gds_Opciones.Tables["Table"];

            grvOpciones.DataBind();
            //ConsultarOpciones(gstr_IdCatalogo.Trim());
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            dsCatalogo = ws_SGService.uwsConsultarCatalogos(Int32.Parse(clsSesion.Current.IdCatalogo), "", "", "");
            string vNombre = txtNomCatalogo.Text.Trim();
            string vActivo = ckbActivo.Checked?"A":"I";
            int vID = Int32.Parse(txtCatalogo.Text.Trim());
            string vMod = dsCatalogo.Tables[0].Rows[0][7].ToString();
             ws_SGService.uwsModificarCatalogo(vID, vNombre, vNombre, vMod, vActivo, this.gstr_Usuario, DateTime.Now);
        }
    }
}