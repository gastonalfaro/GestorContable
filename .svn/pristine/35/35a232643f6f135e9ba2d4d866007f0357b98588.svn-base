 using Presentacion.Compartidas;
using System;
using System.Collections.Generic;
using System.Data;
//using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Presentacion.Mantenimiento
{
    public partial class frmOperaciones : BASE
    {
        #region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private char gchr_MensajeError;
        private char gchr_MensajeExito;
        private string gstr_Usuario = String.Empty;
        private string gstr_Modulos = String.Empty;

        private String[] garr_Modulos;
        private String[] garr_Modulo_Unico;

        //static DataSet gds_Operaciones = new DataSet();
        protected DataSet gds_Operaciones
        {
            get
            {
                if (ViewState["gds_Operaciones"] == null)
                    ViewState["gds_Operaciones"] = new DataSet();
                return (DataSet)ViewState["gds_Operaciones"];
            }
            set
            {
                ViewState["gds_Operaciones"] = value;
            }
        }
        //static DataSet gds_Modulos = new DataSet();
        protected DataSet gds_Modulos
        {
            get
            {
                if (ViewState["gds_Modulos"] == null)
                    ViewState["gds_Modulos"] = new DataSet();
                return (DataSet)ViewState["gds_Modulos"];
            }
            set
            {
                ViewState["gds_Modulos"] = value;
            }
        }
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

                    if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, ""))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                    {

                        OcultarMensaje();

                        gds_Modulos = ws_SGService.uwsConsultarClasesDocumento("", "");
                        ConsultarOperaciones("", gstr_Modulos, "");
                        CargarDatosModulo();
                        gds_ClasesDocumento = ws_SGService.uwsConsultarClasesDocumento("", "");
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



        private void CargarDatosModulo()
        {
            ddlBusquedaModulo.DataTextField = "NomModulo";
            ddlBusquedaModulo.DataValueField = "IdModulo";
            ddlBusquedaModulo.DataSource = ws_SGService.uwsConsultarModulos("", "");
            ddlBusquedaModulo.DataBind();

            ddlBusquedaModulo.Items.Insert(0, "");
        }

        private void ConsultarOperaciones(string str_IdOperacion, string str_IdModulo, string str_NomOperacion)
        {
            //if (str_IdModulo.Equals("MA"))
            //    str_IdModulo = "";
            //else
            //    str_IdModulo = gstr_Modulo.Remove(0, 4);

       //     string lstr_modulo = String.Empty;

            if (!string.IsNullOrEmpty(str_IdModulo))
                str_IdModulo = "IdModulo IN ('" + str_IdModulo + "')";
            else {
                if (String.IsNullOrEmpty(gstr_Modulos))
                {
                    for (int i = 0; garr_Modulos.Count() > i; i++)
                    {
                        if ((i == 0) && (garr_Modulos[i] != null))
                            str_IdModulo = "'" + garr_Modulos[i] + "'";
                        else if (garr_Modulos[i] != null)
                        {
                            str_IdModulo = str_IdModulo + ",'" + garr_Modulos[i] + "'";
                        }
                    }
                    str_IdModulo = "IdModulo IN (" + str_IdModulo + ")";
                }
            }

            gds_Operaciones = ws_SGService.uwsConsultarOperaciones(str_IdOperacion, str_IdModulo, str_NomOperacion);

            if (gds_Operaciones.Tables["Table"].Rows.Count > 0)
            {
                grdOperaciones.DataSource = gds_Operaciones.Tables["Table"];
                grdOperaciones.DataBind();
            }
            else
            {
                grdOperaciones.DataSource = this.LlenarTablaVacia();
                grdOperaciones.DataBind();
                grdOperaciones.Rows[0].Visible = false;
            }
        }

        private DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("IdOperacion", typeof(string));
            ldt_TablaVacia.Columns.Add("IdModulo", typeof(string));
            ldt_TablaVacia.Columns.Add("NomOperacion", typeof(string));
            ldt_TablaVacia.Columns.Add("IdOperacionReversa", typeof(string));
            ldt_TablaVacia.Columns.Add("IdClaseDoc", typeof(string));
            ldt_TablaVacia.Columns.Add("Estado", typeof(string));
            ldt_TablaVacia.Columns.Add("FchModifica", typeof(string));
                        
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

        protected void btnOperacionNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("../Mantenimiento/Gestiones/frmNuevaOperacion.aspx", false);          
        }

        protected void btnOperacionGuardar_Click(object sender, EventArgs e)
        {

        }

        protected void btnOperacionVolver_Click(object sender, EventArgs e)
        {

        }

        protected void btnOperacionConsultar_Click(object sender, EventArgs e)
        {
            OcultarMensaje();
            ConsultarOperaciones(this.txtBusquedaIdOperacion.Text, this.ddlBusquedaModulo.SelectedValue , this.txtBusquedaNomOperacion.Text);
            
        }

        protected void grdOperaciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            String[] lstr_resultado = new String[3];
            int lint_TamanoTabla = grdOperaciones.Rows.Count;
           
            TextBox lstr_IdOperacion = (TextBox)grdOperaciones.FooterRow.FindControl("txtInsertarIdOperacion");
            //DropDownList lstr_Modulo = (DropDownList)grdOperaciones.FooterRow.FindControl("ddlModulo");
            TextBox lstr_NomOperacion = (TextBox)grdOperaciones.FooterRow.FindControl("txtInsertarNomOperacion");
            DropDownList lstr_ClaseDocumentos = (DropDownList)grdOperaciones.FooterRow.FindControl("ddlClaseDocumentos");
            TextBox lstr_Estado = (TextBox)grdOperaciones.FooterRow.FindControl("txtAgregarEstado");
            TextBox lstr_IdOperacionReversa = (TextBox)grdOperaciones.FooterRow.FindControl("txtInsertIdOperacionReversa");

            lstr_resultado = ws_SGService.uwsCrearOperacion(lstr_IdOperacion.Text, gstr_Modulos,
                lstr_NomOperacion.Text, lstr_ClaseDocumentos.SelectedValue.Trim(), lstr_Estado.Text, lstr_IdOperacionReversa.Text, gstr_Usuario);


            ConsultarOperaciones("", gstr_Modulos, "");

            grdOperaciones.SelectedIndex = -1;
            grdOperaciones.PageIndex = grdOperaciones.PageCount - 1;

            if (lstr_resultado[0].ToString().Equals("00") || lstr_resultado[0].ToString().Equals("True"))
                MostarMensaje(lstr_resultado[1].ToString(), gchr_MensajeExito);
            else
                MostarMensaje(lstr_resultado[1].ToString(), gchr_MensajeError);
            return;
        }

        protected void grdOperaciones_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            String[] lstr_result = new String[3];
            try
            {
                //DataRow ldr_OperacionesRow = gds_Operaciones.Tables["Table"].NewRow();
                //ldr_OperacionesRow = gds_Operaciones.Tables["Table"].Rows[e.RowIndex];

                //string lstr_IdModulo = ldr_OperacionesRow["IdModulo"].ToString();
                //string lstr_IdOperacion = ldr_OperacionesRow["IdOperacion"].ToString();
                //DateTime ldt_FchModifica = Convert.ToDateTime(ldr_OperacionesRow["FchModifica"].ToString());
                
                GridViewRow row = (GridViewRow)grdOperaciones.Rows[e.RowIndex];

                Label lstr_Modulo = (Label)row.FindControl("lblModulo");
                Label lstr_IdOperacion = (Label)row.FindControl("lblIdOperacion");
                Label lbl_FchModifica = (Label)row.FindControl("lblIFchModifica");
                string lstr_fecha = String.Empty;
                DateTime ldt_FchModifica = Convert.ToDateTime( lbl_FchModifica.Text);
                lstr_fecha = ldt_FchModifica.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                ldt_FchModifica = Convert.ToDateTime(lstr_fecha);

                DropDownList ddl_IdClaseDoc = (DropDownList)row.FindControl("ddlEditarClaseDocumentos");
                TextBox txt_NomOperacion = (TextBox)row.FindControl("txtEditarNomOperacion");
                TextBox txt_EditarEstado = (TextBox)row.FindControl("txtEditarEstado");
                TextBox txt_Estado = (TextBox)row.FindControl("txtEditarEstado");
                TextBox txt_IdOperacionReserva = (TextBox)row.FindControl("txtEditarIdOperacionReversa");

                lstr_result = ws_SGService.uwsModificarOperacion(lstr_IdOperacion.Text, lstr_Modulo.Text, txt_NomOperacion.Text, ddl_IdClaseDoc.SelectedValue, txt_EditarEstado.Text, txt_IdOperacionReserva.Text, gstr_Usuario,  ldt_FchModifica);

                if (lstr_result[0].ToString().Equals("00") || lstr_result[0].ToString().Equals("True"))
                    MostarMensaje("La modificación de datos ha sido satisfactoria.", gchr_MensajeExito);
                else
                    MostarMensaje("La modificación de datos no ha sido satisfactoria.", gchr_MensajeError);

                grdOperaciones.EditIndex = -1;
                grdOperaciones.DataSource = gds_Operaciones.Tables["Table"];

                grdOperaciones.DataBind();
                //ConsultarOperaciones(this.txtBusquedaIdOperacion.Text, this.ddlBusquedaModulo.SelectedValue , this.txtBusquedaNomOperacion.Text);
            }
            catch (Exception ex)
            {
                ConsultarOperaciones(this.txtBusquedaIdOperacion.Text, this.ddlBusquedaModulo.SelectedValue, this.txtBusquedaNomOperacion.Text);
                MostarMensaje("Error al finalizar la modificación de datos.", gchr_MensajeError);

            }
        }

        protected void grdOperaciones_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            //gds_Operaciones = ws_SGService.uwsConsultarOperaciones(this.txtBusquedaIdOperacion.Text, this.ddlBusquedaModulo.SelectedValue.Equals(string.Empty) ? gstr_Modulos : this.ddlBusquedaModulo.SelectedValue , this.txtBusquedaNomOperacion.Text);
        }


        protected void grdOperaciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            OcultarMensaje();
            grdOperaciones.PageIndex = e.NewPageIndex;
            grdOperaciones.DataSource = gds_Operaciones.Tables["Table"];

            grdOperaciones.DataBind();
            //ConsultarOperaciones(this.txtBusquedaIdOperacion.Text, this.ddlBusquedaModulo.SelectedValue , this.txtBusquedaNomOperacion.Text);
        }

        protected void grdOperaciones_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdOperaciones.EditIndex = -1;
            grdOperaciones.DataSource = gds_Operaciones.Tables["Table"];

            grdOperaciones.DataBind();
            //ConsultarOperaciones(this.txtBusquedaIdOperacion.Text, this.ddlBusquedaModulo.SelectedValue, this.txtBusquedaNomOperacion.Text);       
        }

        protected void grdOperaciones_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdOperaciones.EditIndex = e.NewEditIndex;
            grdOperaciones.DataSource = gds_Operaciones.Tables["Table"];

            grdOperaciones.DataBind();
            //ConsultarOperaciones(this.txtBusquedaIdOperacion.Text, this.ddlBusquedaModulo.SelectedValue, this.txtBusquedaNomOperacion.Text);
        }


        protected void grdOperaciones_DataBound(object sender, EventArgs e)
        {
            //DropDownList ddlModulo = (DropDownList)grdOperaciones.FooterRow.FindControl("ddlModulo");
            //ddlModulo.DataTextField = "NomModulo";
            //ddlModulo.DataValueField = "IdModulo";
            //ddlModulo.DataSource = gds_Modulos.Tables["Table"];
            //ddlModulo.DataBind();

            //DropDownList ddlClaseDocumentos = (DropDownList)grdOperaciones.FooterRow.FindControl("ddlClaseDocumentos");
            //ddlClaseDocumentos.DataTextField = "NomClaseDoc";
            //ddlClaseDocumentos.DataValueField = "IdClaseDoc";
            //ddlClaseDocumentos.DataSource = gds_ClasesDocumento;
            //ddlClaseDocumentos.DataBind();

          
        }

        protected void grdOperaciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {

                    string vClaseDocumento = (e.Row.FindControl("lblClaseDocumento") as Label).Text;

                    DropDownList ddlClaseDocumentos = (DropDownList)e.Row.FindControl("ddlEditarClaseDocumentos");
                    
                    gds_ClasesDocumento = ws_SGService.uwsConsultarClasesDocumento("", "");

                    if (gds_ClasesDocumento.Tables["Table"].Rows.Count > 0)
                    {
                        ddlClaseDocumentos.DataSource = gds_ClasesDocumento.Tables["Table"];
                        ddlClaseDocumentos.DataTextField = "NomClaseDoc";
                        ddlClaseDocumentos.DataValueField = "IdClaseDoc";
                        ddlClaseDocumentos.DataSource = gds_ClasesDocumento;
                        ddlClaseDocumentos.DataBind();

                        if (!vClaseDocumento.Equals(string.Empty))
                        {
                            try
                            {
                                ddlClaseDocumentos.Items.FindByValue(vClaseDocumento).Selected = true;
                            }
                            catch (Exception ex1)
                            {
                                
                            }
                        }
                    }

                }

            }
        }

    }
}