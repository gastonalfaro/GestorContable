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
    public partial class frmCatalogosGenerales : BASE
    {
        # region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private char gchr_MensajeError;
        private char gchr_MensajeExito;
        private string gstr_Usuario = String.Empty;
        private string gstr_Modulos = String.Empty;
        private String[] garr_Modulos;

        //private DataSet gds_Catalogos = new DataSet();

        protected DataSet gds_Catalogos
        {
            get
            {
                if (ViewState["gds_Catalogos"] == null)
                    ViewState["gds_Catalogos"] = new DataSet();
                return (DataSet)ViewState["gds_Catalogos"];
            }
            set
            {
                ViewState["gds_Catalogos"] = value;
            }
        }
        # endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            gstr_Usuario = clsSesion.Current.LoginUsuario;
            gchr_MensajeError = clsSesion.Current.chr_MensajeError;
            gchr_MensajeExito = clsSesion.Current.chr_MensajeExito;
            garr_Modulos = clsSesion.Current.PermisosModulos;


            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(gstr_Usuario))
                {

                    if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmCatalogosGenerales"))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                    {
                        OcultarMensaje();
                        ConsultarCatalogos(null, "");
                    }
                }
                else
                {
                    Response.Redirect("~/Login.aspx", true);
                }

            }
            else
            {


                gds_Catalogos = (DataSet)ViewState["gds_Catalogos"];
                if (string.IsNullOrEmpty(gstr_Usuario))
                    Response.Redirect("~/Login.aspx", true);
            }
        }

        private void ConsultarCatalogos(int? int_IdCatalogos, string str_Nombre)
        {
            string lstr_submodulo = String.Empty;

            for (int i = 0; garr_Modulos.Count() > i; i++)
            {
                if ((i == 0) && (garr_Modulos[i] != null))
                    lstr_submodulo = "'" + garr_Modulos[i] + "'";
                else if (garr_Modulos[i] != null)
                {
                    lstr_submodulo = lstr_submodulo + ",'" + garr_Modulos[i] + "'";
                }
            }

            if (!string.IsNullOrEmpty(lstr_submodulo))
                gstr_Modulos = "IdModulo IN (" + lstr_submodulo + ")";

            //int? lint_IdCatalogo = null;

            gds_Catalogos = ws_SGService.uwsConsultarCatalogos(int_IdCatalogos, "", str_Nombre, "1=1");//gstr_Modulos);

            ViewState["gds_Catalogos"] = gds_Catalogos;


            if (gds_Catalogos.Tables["Table"].Rows.Count > 0)
            {
                grdvCatalogos.DataSource = gds_Catalogos.Tables["Table"];
                grdvCatalogos.DataBind();
            }
            else
            {
                grdvCatalogos.DataSource = this.LlenarTablaVacia();
                grdvCatalogos.DataBind();                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                        
            }
        }

        private DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("IdCatalogo", typeof(string));
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

        protected void btnCatalogoNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Mantenimiento/Gestiones/frmNuevoCatalogo.aspx", false);
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
            string lstr_codigo = txtBusqIdCatalogo.Text;

            if (string.IsNullOrEmpty(lstr_codigo))
                ConsultarCatalogos(null, txtBusqNomCatalogo.Text.Trim());
            else
                ConsultarCatalogos(Convert.ToInt32(lstr_codigo), txtBusqNomCatalogo.Text);
        }

        protected void grdvCatalogos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grdvCatalogos.SelectedIndex < 0)
                return;
        }

        protected void grdvCatalogos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            /*DataRow ldr_CatalogosRow = gds_Catalogos.Tables["Table"].NewRow();
            ldr_CatalogosRow = gds_Catalogos.Tables["Table"].Rows[(22 * e.NewPageIndex) + e.NewEditIndex];
            */

            GridViewRow ldr_CatalogosRow = (GridViewRow)grdvCatalogos.Rows[e.NewEditIndex];



            Label lint_IdCatalogo = (Label)grdvCatalogos.Rows[e.NewEditIndex].Cells[0].FindControl("lblIdCatalogo");

            //Label lblIdRevelacion = (Label)gvFormularios.Rows[e.NewEditIndex].Cells[0].FindControl("lblIdRevelacion");
            Label lstr_NomCatalogo = (Label)grdvCatalogos.Rows[e.NewEditIndex].Cells[1].FindControl("lblNombre");
            //string lint_IdCatalogo = ldr_CatalogosRow.Cells[0] .ToString();// .FindControl("Código").ToString();
            //string lstr_NomCatalogo = ldr_CatalogosRow.FindControl("Descripción").ToString();

            clsSesion.Current.IdCatalogo = lint_IdCatalogo.Text;
            clsSesion.Current.NomCatalogo = lstr_NomCatalogo.Text;

            grdvCatalogos.EditIndex = -1;
            Response.Redirect("~/Mantenimiento/frmOpcionesCatalogo.aspx", false);       
        }

        protected void grdvCatalogos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //String[] lstr_result = new String[3];
            //try
            //{
            //    DataRow ldr_CatalogosRow = gds_Catalogos.Tables["Table"].NewRow();
            //    ldr_CatalogosRow = gds_Catalogos.Tables["Table"].Rows[e.RowIndex];

            //    int lint_IdCatalogos = (int)ldr_CatalogosRow["IdCatalogo"];
            //    DateTime ldt_FchModifica = Convert.ToDateTime(ldr_CatalogosRow["FchModifica"].ToString());

            //    string str_fecha = String.Empty;
            //    str_fecha = ldt_FchModifica.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            //    ldt_FchModifica = Convert.ToDateTime(str_fecha);

            //    GridViewRow row = (GridViewRow)grdvCatalogos.Rows[e.RowIndex];

            //    TextBox txt_Nombre = (TextBox)row.FindControl("txtEditNombre");
            //    TextBox txt_Estado = (TextBox)row.FindControl("txtEditEstado");

            //    lstr_result = ws_SGService.uwsModificarCatalogo(lint_IdCatalogos, txt_Nombre.Text, txt_Nombre.Text,"*", txt_Estado.Text, "jnet", ldt_FchModifica);

            //    if (lstr_result[0].ToString().Equals("00"))
            //    {
            //        MostarMensaje(lstr_result[1].ToString(), gchr_MensajeExito);
            //    }
            //    else
            //    {
            //        MostarMensaje("Error: " + lstr_result[1].ToString(), gchr_MensajeError);
            //    }
            //    grdvCatalogos.EditIndex = -1;
            //    ConsultarCatalogos(0, "");
            //}
            //catch (Exception ex)
            //{
            //    ConsultarCatalogos(0, "");
            //    MostarMensaje(ex.ToString(), gchr_MensajeError);

            //}

            

        }

        protected void grdvCatalogos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdvCatalogos.PageIndex = e.NewPageIndex;
            grdvCatalogos.DataSource = gds_Catalogos.Tables["Table"];

            grdvCatalogos.DataBind();
            //string lstr_codigo = txtBusqIdCatalogo.Text;

            //if (string.IsNullOrEmpty(lstr_codigo))
            //    ConsultarCatalogos(null, txtBusqNomCatalogo.Text.Trim());
            //else
            //    ConsultarCatalogos(Convert.ToInt32(lstr_codigo), txtBusqNomCatalogo.Text);            
        }
        
        protected void grdvCatalogos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdvCatalogos.EditIndex = -1;
            grdvCatalogos.DataSource = gds_Catalogos.Tables["Table"];

            grdvCatalogos.DataBind();
            //string lstr_codigo = txtBusqIdCatalogo.Text;

            //if (string.IsNullOrEmpty(lstr_codigo))
            //    ConsultarCatalogos(null, txtBusqNomCatalogo.Text.Trim());
            //else
            //    ConsultarCatalogos(Convert.ToInt32(lstr_codigo), txtBusqNomCatalogo.Text);         
        }

    }
}