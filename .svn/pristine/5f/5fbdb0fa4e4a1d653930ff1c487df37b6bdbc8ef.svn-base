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
    public partial class frmAcreedores : BASE
    {
        # region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private char gchr_MensajeError;
        private char gchr_MensajeExito;
        private string gstr_Usuario = String.Empty;
        //static DataSet gds_Acreedores = new DataSet();

        protected DataSet gds_Acreedores
        {
            get
            {
                if (ViewState["gds_Acreedores"] == null)
                    ViewState["gds_Acreedores"] = new DataSet();
                return (DataSet)ViewState["gds_Acreedores"];
            }
            set
            {
                ViewState["gds_Acreedores"] = value;
            }
        }

        //static DataSet gds_CtasContables = new DataSet();
        protected DataSet gds_CtasContables
        {
            get
            {
                if (ViewState["gds_CtasContables"] == null)
                    ViewState["gds_CtasContables"] = new DataSet();
                return (DataSet)ViewState["gds_CtasContables"];
            }
            set
            {
                ViewState["gds_CtasContables"] = value;
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
                    if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmAcreedores"))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                    {
                        OcultarMensaje();
                        ConsultarAcreedores(-1, "");
                        gds_CtasContables = ws_SGService.uwsConsultarCuentasContables("", "","","","","","","");
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

                    //if (lstr_IdObjeto.Equals("frmAcreedores") && lbool_Actualizar)
                    //{
                    //    grdvAcreedores.Columns[0].Visible = true;
                    //}

        private void ConsultarAcreedores(int int_NroAcreedor, string str_NomAcreedor)
        {
            gds_Acreedores = ws_SGService.uwsConsultarAcreedores(int_NroAcreedor, str_NomAcreedor);

            if (gds_Acreedores.Tables["Table"].Rows.Count > 0)
            {
                grdvAcreedores.DataSource = gds_Acreedores.Tables["Table"];
                grdvAcreedores.DataBind();
            }
            else
            {
                grdvAcreedores.DataSource = this.LlenarTablaVacia();
                grdvAcreedores.DataBind();
                grdvAcreedores.Rows[0].Visible = false;
            }
        }

        private DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("NroAcreedor", typeof(string));
            ldt_TablaVacia.Columns.Add("NomAcreedor", typeof(string));
            ldt_TablaVacia.Columns.Add("Abreviatura", typeof(string));
            ldt_TablaVacia.Columns.Add("Contacto", typeof(string));
            ldt_TablaVacia.Columns.Add("Telefono", typeof(string));
            ldt_TablaVacia.Columns.Add("Direccion", typeof(string));
            ldt_TablaVacia.Columns.Add("Pais", typeof(string));
            ldt_TablaVacia.Columns.Add("PaisInstitucion", typeof(string));
            ldt_TablaVacia.Columns.Add("TipoAcreedor", typeof(string));
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

        protected void btnAcreedorNuevo_Click(object sender, EventArgs e)
        {
           Response.Redirect("~/Mantenimiento/Gestiones/frmNuevoAcreedor.aspx", false);
        }

        protected void btnAcreedorGuardar_Click(object sender, EventArgs e)
        {

        }

        protected void btnAcreedorVolver_Click(object sender, EventArgs e)
        {

        }

        protected void btnAcreedorConsultar_Click(object sender, EventArgs e)
        {
            OcultarMensaje();
            //if (string.IsNullOrEmpty(txtBusqIdAcreedor.Text))
            //    txtBusqIdAcreedor.Text = "-1";
            ConsultarAcreedores(this.txtBusqIdAcreedor.Text.Equals(string.Empty) ? -1 : Convert.ToInt32(this.txtBusqIdAcreedor.Text), this.txtBusqNomAcreedor.Text);
        }

        protected void grdvAcreedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            String[] lstr_resultado = new String[3];
            int lint_TamanoTabla = grdvAcreedores.Rows.Count;

            TextBox lstr_IdAcreedor = (TextBox)grdvAcreedores.FooterRow.FindControl("txtInsertIdAcreedor");
            //DropDownList lstr_Modulo = (DropDownList)grdOperaciones.FooterRow.FindControl("ddlModulo");
            TextBox lstr_NomAcreedor = (TextBox)grdvAcreedores.FooterRow.FindControl("txtEditNomAcreedor");
            DropDownList lstr_CtaContable = (DropDownList)grdvAcreedores.FooterRow.FindControl("ddlCtaContable");
            TextBox lstr_Estado = (TextBox)grdvAcreedores.FooterRow.FindControl("txtAgregarEstado");
            //TextBox lstr_IdOperacionReversa = (TextBox)grdvAcreedores.FooterRow.FindControl("txtInsertIdOperacionReversa");

            //lstr_resultado = ws_SGService.uwsCrearOperacion(lstr_IdAcreedor.Text, gstr_Modulos,
                //lstr_NomAcreedor.Text, lstr_ClaseDocumentos.SelectedValue.Trim(), lstr_Estado.Text, lstr_IdOperacionReversa.Text, gstr_Usuario);


            ConsultarAcreedores(-1, "");

            grdvAcreedores.SelectedIndex = -1;
            grdvAcreedores.PageIndex = grdvAcreedores.PageCount - 1;

            if (lstr_resultado[0].ToString().Equals("00") || lstr_resultado[0].ToString().Equals("True"))
                MostarMensaje(lstr_resultado[1].ToString(), gchr_MensajeExito);
            else
                MostarMensaje(lstr_resultado[1].ToString(), gchr_MensajeError);
            return;
        }

        protected void grdvAcreedores_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            String[] lstr_result = new String[3];
            try
            {
                //DataRow ldr_AcreedoresRow = gds_Acreedores.Tables["Table"].NewRow();
                //ldr_AcreedoresRow = gds_Acreedores.Tables["Table"].Rows[e.RowIndex];

                //int lint_IdAcreedor = Convert.ToInt32(ldr_AcreedoresRow["NroAcreedor"]);
                //DateTime ldt_FchModifica = Convert.ToDateTime(ldr_AcreedoresRow["FchModifica"].ToString());

                //string lstr_fecha = String.Empty;
                //lstr_fecha = ldt_FchModifica.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                //ldt_FchModifica = Convert.ToDateTime(lstr_fecha);

                //string str_NomAcreedor = ldr_AcreedoresRow["NroAcreedor"].ToString();
                //string str_Abreviatura = ldr_AcreedoresRow["Abreviatura"].ToString();
                //string str_Contacto = ldr_AcreedoresRow["Contacto"].ToString();
                //string str_Telefonos = ldr_AcreedoresRow["Telefonos"].ToString();
                //string str_Direccion = ldr_AcreedoresRow["Direccion"].ToString();
                //string str_Pais = ldr_AcreedoresRow["Pais"].ToString();
                //string str_PaisInstitucion = ldr_AcreedoresRow["PaisInstitucion"].ToString();
                //string str_TipoAcreedor = ldr_AcreedoresRow["TipoAcreedor"].ToString();
                

                GridViewRow row = (GridViewRow)grdvAcreedores.Rows[e.RowIndex];
                Label lstr_IdAcreedor = (Label)row.FindControl("lblIdAcreedor");
                Label lbl_FchModifica = (Label)row.FindControl("lblIFchModifica");
                string lstr_fecha = String.Empty;
                DateTime ldt_FchModifica = Convert.ToDateTime(lbl_FchModifica.Text);
                lstr_fecha = ldt_FchModifica.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                ldt_FchModifica = Convert.ToDateTime(lstr_fecha);

                TextBox txt_NomAcreedor = (TextBox)row.FindControl("txtEditNomAcreedor");
                TextBox txt_Abreviatura = (TextBox)row.FindControl("txtEditAbreviatura");
                TextBox txt_Contacto = (TextBox)row.FindControl("txtEditContacto");
                TextBox txt_Telefonos = (TextBox)row.FindControl("txtEditTelefonos");
                TextBox txt_Direccion = (TextBox)row.FindControl("txtEditDireccion");
                TextBox txt_Pais = (TextBox)row.FindControl("txtEditPais");
                TextBox txt_PaisInstitucion = (TextBox)row.FindControl("txtEditPaisInstitucion");
                TextBox txt_TipoAcreedor = (TextBox)row.FindControl("txtEditTipoAcreedor");
                //TextBox txt_Estado = (TextBox)row.FindControl("txtEditEstado");
                DropDownList ddl_IdCtaContable = (DropDownList)row.FindControl("ddlEditarIdCtaContable");

                lstr_result = ws_SGService.uwsModificarAcreedor(Convert.ToInt32(lstr_IdAcreedor.Text), txt_NomAcreedor.Text,
                    txt_Abreviatura.Text, txt_Contacto.Text, txt_Telefonos.Text, txt_Direccion.Text, txt_Pais.Text, txt_TipoAcreedor.Text,
                    txt_PaisInstitucion.Text, ddl_IdCtaContable.SelectedValue, gstr_Usuario, ldt_FchModifica);
                if (lstr_result[0].ToString().Equals("00"))
                {
                    MostarMensaje(lstr_result[1].ToString(), gchr_MensajeExito);
                }
                else
                {
                    MostarMensaje(lstr_result[1].ToString(), gchr_MensajeExito);
                }
                grdvAcreedores.EditIndex = -1;
                grdvAcreedores.DataSource = gds_Acreedores.Tables["Table"];

                grdvAcreedores.DataBind();
                //ConsultarAcreedores(-1, "");
            }
            catch (Exception ex)
            {
                ConsultarAcreedores(-1, "");
                MostarMensaje(ex.ToString(), gchr_MensajeError);

            }
        }

        protected void grdvAcreedores_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            ConsultarAcreedores(-1, "");
            //gds_Acreedores = ws_SGService.uwsConsultarAcreedores(this.txtBusqIdAcreedor.Text.Equals(string.Empty) ? -1 : Convert.ToInt32(this.txtBusqIdAcreedor.Text), this.txtBusqNomAcreedor.Text);
        }

        protected void grdvAcreedores_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            OcultarMensaje();
            grdvAcreedores.PageIndex = e.NewPageIndex;

            grdvAcreedores.DataSource = gds_Acreedores.Tables["Table"];

            grdvAcreedores.DataBind();
            //ConsultarAcreedores(this.txtBusqIdAcreedor.Text.Equals(string.Empty) ? -1 : Convert.ToInt32(this.txtBusqIdAcreedor.Text), this.txtBusqNomAcreedor.Text);
        }

        protected void grdvAcreedores_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

                grdvAcreedores.EditIndex = -1;
                grdvAcreedores.DataSource = gds_Acreedores.Tables["Table"];

                grdvAcreedores.DataBind();
                //ConsultarAcreedores(this.txtBusqIdAcreedor.Text.Equals(string.Empty) ? -1 : Convert.ToInt32(this.txtBusqIdAcreedor.Text), this.txtBusqNomAcreedor.Text);
        }

        protected void grdvAcreedores_RowEditing(object sender, GridViewEditEventArgs e)
        {

            grdvAcreedores.EditIndex = e.NewEditIndex;
            grdvAcreedores.DataSource = gds_Acreedores.Tables["Table"];

            grdvAcreedores.DataBind();
            //ConsultarAcreedores(this.txtBusqIdAcreedor.Text.Equals(string.Empty) ? -1 : Convert.ToInt32(this.txtBusqIdAcreedor.Text), this.txtBusqNomAcreedor.Text);
        }


        protected void grdvAcreedores_DataBound(object sender, EventArgs e)
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

        protected void grdvAcreedores_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {

                    string vCtaContable = (e.Row.FindControl("lblIdCtaContable") as Label).Text;

                    DropDownList ddlCtasContables = (DropDownList)e.Row.FindControl("ddlEditarIdCtaContable");

                    gds_CtasContables = ws_SGService.uwsConsultarCuentasContables("", "", "", "", "", "", "", "");

                    if (gds_CtasContables.Tables["Table"].Rows.Count > 0)
                    {
                        ddlCtasContables.DataSource = gds_CtasContables.Tables["Table"];
                        ddlCtasContables.DataTextField = "NomCompleto";
                        ddlCtasContables.DataValueField = "IdCuentaContable";
                        ddlCtasContables.DataSource = gds_CtasContables;
                        ddlCtasContables.DataBind();

                        if (!vCtaContable.Equals(string.Empty))
                            ddlCtasContables.Items.FindByValue(vCtaContable).Selected = true;
                    }

                }

            }
        }

    }
}