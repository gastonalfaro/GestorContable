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
    public partial class frmNemotecnicos : BASE
    {
        # region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private char gchr_MensajeError;
        private char gchr_MensajeExito;
        private string gstr_Usuario = String.Empty;
        private string gstr_ModuloActual = String.Empty;

        //static DataSet gds_Nemotecnicos = new DataSet();
        protected DataSet gds_Nemotecnicos
        {
            get
            {
                if (ViewState["gds_Nemotecnicos"] == null)
                    ViewState["gds_Nemotecnicos"] = new DataSet();
                return (DataSet)ViewState["gds_Nemotecnicos"];
            }
            set
            {
                ViewState["gds_Nemotecnicos"] = value;
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
        //private static DataSet gds_Monedas = new DataSet();
        protected DataSet gds_Monedas
        {
            get
            {
                if (ViewState["gds_Monedas"] == null)
                    ViewState["gds_Monedas"] = new DataSet();
                return (DataSet)ViewState["gds_Monedas"];
            }
            set
            {
                ViewState["gds_Monedas"] = value;
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
                    if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmNemotecnicos"))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                    {
                        OcultarMensaje();

                        gds_Monedas = ws_SGService.uwsConsultarMonedas("", "");
                        ConsultarNemotecnicos("", "", "", "", "");
                        // gds_Modulos = ws_SGService.uwsConsultarMonedas("", "");
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

        private void ConsultarNemotecnicos(string str_IdNemotecnico, string str_IdSociedadFi, string str_NomNemotecnico, string str_IdMoneda, string str_TipoNemotecnico)
        {
            gds_Nemotecnicos = ws_SGService.uwsConsultarNemotecnicos(str_IdNemotecnico, str_IdSociedadFi, str_NomNemotecnico, str_IdMoneda, str_TipoNemotecnico);
                
            if (gds_Nemotecnicos.Tables["Table"].Rows.Count > 0)
            {
                gds_Nemotecnicos.Tables["Table"].DefaultView.Sort = "TipoNemotecnico";
                grdNemotecnicos.DataSource = gds_Nemotecnicos.Tables["Table"];
                grdNemotecnicos.DataBind();
            }
            else
            {
                grdNemotecnicos.DataSource = this.LlenarTablaVacia();
                grdNemotecnicos.DataBind();
                grdNemotecnicos.Rows[0].Visible = false;
            }
        }

        private DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();

            ldt_TablaVacia.Columns.Add("FchModifica", typeof(string));
            ldt_TablaVacia.Columns.Add("IdNemotecnico", typeof(string));
            ldt_TablaVacia.Columns.Add("NomNemotecnico", typeof(string));
            ldt_TablaVacia.Columns.Add("IdSociedadFi", typeof(string));
            ldt_TablaVacia.Columns.Add("IdMoneda", typeof(string));
            ldt_TablaVacia.Columns.Add("TipoNemotecnico", typeof(string));
            ldt_TablaVacia.Columns.Add("ModuloSINPE", typeof(string));   
            ldt_TablaVacia.Columns.Add("IdCuentaContableCP", typeof(string));
            ldt_TablaVacia.Columns.Add("IdCuentaContableLP", typeof(string));     
            ldt_TablaVacia.Columns.Add("Estado", typeof(Boolean));


            DataRow ldr_FilaTabla = ldt_TablaVacia.NewRow();
            ldr_FilaTabla["Estado"] = false;
            ldt_TablaVacia.Rows.Add(ldr_FilaTabla);
            return ldt_TablaVacia;
        }

        private void ConsultarIndicadoresEco()
        {
            gds_Nemotecnicos = ws_SGService.uwsConsultarIndicadoresEconomicos("", "", "");

            if (gds_Nemotecnicos.Tables["Table"].Rows.Count > 0)
            {
                gds_Nemotecnicos.Tables["Table"].DefaultView.Sort = "TipoNemotecnico";
                grdNemotecnicos.DataSource = gds_Nemotecnicos.Tables["Table"];
                grdNemotecnicos.DataBind();
            }
            else
            {
                grdNemotecnicos.DataSource = this.LlenarTablaVacia();
                grdNemotecnicos.DataBind();
                grdNemotecnicos.Rows[0].Visible = false;
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

        protected void btnCatalogoConsultar_Click(object sender, EventArgs e)
        {
            OcultarMensaje();
            ConsultarNemotecnicos(txtBusquedaIdNemotecnico.Text, txtBusquedaIdSociedadFi.Text, txtBusquedaNomNemotecnico.Text, txtBusquedaIdMoneda.Text, txtBusquedaTipoNemotecnico.Text);
        
        }

        protected void grdNemotecnicos_SelectedIndexChanged(object sender, EventArgs e)
        {
            String[] lstr_resultado = new String[3];
            int lint_TamanoTabla = grdNemotecnicos.Rows.Count;
            try
            {
                TextBox lstr_IdNemotecnico = (TextBox)grdNemotecnicos.FooterRow.FindControl("txtInsertarIdNemotecnico");
                TextBox lstr_NomNemotecnico = (TextBox)grdNemotecnicos.FooterRow.FindControl("txtInsertarNomNemotecnico");
                DropDownList lstr_IdSociedadFi = (DropDownList)grdNemotecnicos.FooterRow.FindControl("ddlInsertarIdSociedadFi");
                DropDownList lstr_IdMoneda = (DropDownList)grdNemotecnicos.FooterRow.FindControl("ddlInsertarIdMoneda");
                DropDownList lstr_TipoNemotecnico = (DropDownList)grdNemotecnicos.FooterRow.FindControl("ddlInsertarTipoNemotecnico");
                DropDownList lstr_ModuloSINPE = (DropDownList)grdNemotecnicos.FooterRow.FindControl("ddlInsertarModuloSINPE");

                TextBox lstr_IDCuentaContableCP = (TextBox)grdNemotecnicos.FooterRow.FindControl("txtInsertarContableCP");
                TextBox lstr_IDCuentaContableCL = (TextBox)grdNemotecnicos.FooterRow.FindControl("txtInsertarIdCuentaContableLP");


                TextBox lstr_NomNemotecnio = (TextBox)grdNemotecnicos.FooterRow.FindControl("txtInsertarNomNemotecnico");

                CheckBox lbool_Estado = (CheckBox)grdNemotecnicos.FooterRow.FindControl("cbInsertarEstado");

                if (lstr_IdNemotecnico.Text.Equals(string.Empty)
                    || lstr_NomNemotecnico.Text.Equals(string.Empty)
                    || lstr_IdMoneda.SelectedItem.Text.Equals(string.Empty)
                    || lstr_TipoNemotecnico.SelectedItem.Text.Equals(string.Empty)
                    || lstr_IdSociedadFi.SelectedItem.Text.Equals(string.Empty)
                    || lstr_ModuloSINPE.SelectedItem.Text.Equals(string.Empty))
                
                    MostarMensaje("Ingrese los datos necesarios.", gchr_MensajeError);
                else{
                
                    lstr_resultado = ws_SGService.uwsCrearNemotecnico(lstr_IdNemotecnico.Text, lstr_IdSociedadFi.SelectedValue.Trim(),
                        lstr_NomNemotecnico.Text, lstr_IdMoneda.SelectedValue.Trim(), lstr_TipoNemotecnico.SelectedValue.Trim(), "", lstr_ModuloSINPE.SelectedValue.Trim(), lstr_IDCuentaContableCP.Text, lstr_IDCuentaContableCL.Text, lbool_Estado.Checked ? "A" : "I", gstr_Usuario);

                    if (lstr_resultado[0].ToString().Equals("00") || lstr_resultado[0].ToString().Equals("True"))
                        MostarMensaje(lstr_resultado[1].ToString(), gchr_MensajeExito);
                    else
                        MostarMensaje("No se ingresaron los datos.", gchr_MensajeError);

                    ConsultarNemotecnicos(txtBusquedaIdNemotecnico.Text, txtBusquedaIdSociedadFi.Text, txtBusquedaNomNemotecnico.Text, txtBusquedaIdMoneda.Text, txtBusquedaTipoNemotecnico.Text);

                    grdNemotecnicos.SelectedIndex = -1;
                    grdNemotecnicos.DataSource = gds_Nemotecnicos.Tables["Table"];

                    grdNemotecnicos.DataBind();
                    //grdNemotecnicos.PageIndex = grdNemotecnicos.PageCount - 1;
                }
            }
            catch {
                ConsultarNemotecnicos(txtBusquedaIdNemotecnico.Text, txtBusquedaIdSociedadFi.Text, txtBusquedaNomNemotecnico.Text, txtBusquedaIdMoneda.Text, txtBusquedaTipoNemotecnico.Text);
        
                MostarMensaje("No se ingresaron los datos.", gchr_MensajeError);
            
            }
        }

        protected void grdNemotecnicos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdNemotecnicos.EditIndex = e.NewEditIndex;
            grdNemotecnicos.DataSource = gds_Nemotecnicos.Tables["Table"];

            grdNemotecnicos.DataBind();
            //ConsultarNemotecnicos(this.txtBusquedaIdNemotecnico.Text, this.txtBusquedaIdSociedadFi.Text, this.txtBusquedaNomNemotecnico.Text, this.txtBusquedaIdMoneda.Text, this.txtBusquedaTipoNemotecnico.Text);
        }

        protected void grdNemotecnicos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            String[] lstr_result = new String[3];
            try
            {
                GridViewRow row = (GridViewRow) grdNemotecnicos.Rows[e.RowIndex];

                Label lstr_FechaModifica = (Label)row.FindControl("lblFechaModifica");
                Label lstr_IdNemotecnico = (Label)row.FindControl("lblIdNemotecnico");
                TextBox lstr_NomNemotecnico = (TextBox) row.FindControl("txtEditarNomNemotecnico");
                DropDownList lstr_IdSociedadFi = (DropDownList) row.FindControl("ddlEditarIdSociedadFi");
                DropDownList lstr_IdMoneda = (DropDownList) row.FindControl("ddlEditarIdMoneda");
                DropDownList lstr_TipoNemotecnico = (DropDownList)row.FindControl("ddlEditarTipoNemotecnico");
                DropDownList lstr_ModuloSINPE = (DropDownList)row.FindControl("ddlEditarModuloSINPE");
                TextBox lstr_IdCuentaContableCP = (TextBox)row.FindControl("txtEditarContableCP");
                TextBox lstr_IdCuentaContableLP = (TextBox)row.FindControl("txtEditarIdCuentaContableLP");
                CheckBox lbool_Estado = (CheckBox) row.FindControl("cbEditarEstado");
                TextBox txt_Estado = (TextBox) row.FindControl("txtEditEstado");

                lstr_result = ws_SGService.uwsModificarNemotecnico(lstr_IdNemotecnico.Text, lstr_IdSociedadFi.SelectedValue,
                    lstr_NomNemotecnico.Text, lstr_IdMoneda.SelectedValue, lstr_TipoNemotecnico.SelectedValue, "", lstr_ModuloSINPE.SelectedValue.Trim(), lstr_IdCuentaContableCP.Text, lstr_IdCuentaContableLP.Text, lbool_Estado.Checked ? "A" : "I", gstr_Usuario, Convert.ToDateTime(lstr_FechaModifica.Text));

                if (lstr_result[0].ToString().Equals("00") || lstr_result[0].Contains("True"))
                    MostarMensaje(lstr_result[1].ToString(), gchr_MensajeExito);
                else
                    MostarMensaje("Ocurrió un error al modificar.", gchr_MensajeError);


                grdNemotecnicos.EditIndex = -1;
                grdNemotecnicos.DataSource = gds_Nemotecnicos.Tables["Table"];

                grdNemotecnicos.DataBind();
                //ConsultarNemotecnicos(txtBusquedaIdNemotecnico.Text, txtBusquedaIdSociedadFi.Text, txtBusquedaNomNemotecnico.Text, txtBusquedaIdMoneda.Text, txtBusquedaTipoNemotecnico.Text);
        
            }
            catch (Exception ex)
            {
                ConsultarNemotecnicos(txtBusquedaIdNemotecnico.Text, txtBusquedaIdSociedadFi.Text, txtBusquedaNomNemotecnico.Text, txtBusquedaIdMoneda.Text, txtBusquedaTipoNemotecnico.Text);
                MostarMensaje(ex.ToString(), gchr_MensajeError);

            }
        }

        protected void grdNemotecnicos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdNemotecnicos.PageIndex = e.NewPageIndex;
            grdNemotecnicos.DataSource = gds_Nemotecnicos.Tables["Table"];

            grdNemotecnicos.DataBind();
            //ConsultarNemotecnicos(txtBusquedaIdNemotecnico.Text, txtBusquedaIdSociedadFi.Text, txtBusquedaNomNemotecnico.Text, txtBusquedaIdMoneda.Text, txtBusquedaTipoNemotecnico.Text);
        }

        protected void grdNemotecnicos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdNemotecnicos.EditIndex = -1;
            grdNemotecnicos.DataSource = gds_Nemotecnicos.Tables["Table"];

            grdNemotecnicos.DataBind();
            //ConsultarNemotecnicos(txtBusquedaIdNemotecnico.Text, txtBusquedaIdSociedadFi.Text, txtBusquedaNomNemotecnico.Text, txtBusquedaIdMoneda.Text, txtBusquedaTipoNemotecnico.Text);
        }

        protected void btnConsultarNemotecnicos_Click(object sender, EventArgs e)
        {
            OcultarMensaje();
            ConsultarNemotecnicos(txtBusquedaIdNemotecnico.Text, txtBusquedaIdSociedadFi.Text, txtBusquedaNomNemotecnico.Text, txtBusquedaIdMoneda.Text, txtBusquedaTipoNemotecnico.Text);
        
        }

        protected void grdNemotecnicos_DataBound(object sender, EventArgs e)
        {

            DropDownList ddlEditarIdSociedadFi = (DropDownList)grdNemotecnicos.FooterRow.FindControl("ddlInsertarIdSociedadFi");
            ddlEditarIdSociedadFi.DataTextField = "Denominacion";
            ddlEditarIdSociedadFi.DataValueField = "IdSociedadFi";
            ddlEditarIdSociedadFi.DataSource = ws_SGService.uwsConsultarSociedadesFinancieras("", "","","","","");
            
            ddlEditarIdSociedadFi.DataBind();
            ddlEditarIdSociedadFi.Items.Insert(0, new ListItem("-"));

            DropDownList ddlInsertarIdMoneda = (DropDownList)grdNemotecnicos.FooterRow.FindControl("ddlInsertarIdMoneda");
            ddlInsertarIdMoneda.DataTextField = "NomMoneda";
            ddlInsertarIdMoneda.DataValueField = "IdMoneda";
            ddlInsertarIdMoneda.DataSource = gds_Monedas.Tables["Table"];
            
            ddlInsertarIdMoneda.DataBind();
            ddlInsertarIdMoneda.Items.Insert(0, new ListItem("-"));

            DropDownList ddlInsertarTipoNemotecnico = (DropDownList)grdNemotecnicos.FooterRow.FindControl("ddlInsertarTipoNemotecnico");
            ddlInsertarTipoNemotecnico.DataTextField = "NomOpcion";
            ddlInsertarTipoNemotecnico.DataValueField = "ValOpcion";
            ddlInsertarTipoNemotecnico.DataSource = ws_SGService.uwsConsultarOpcionesCatalogo("38", "", "", "");            
            ddlInsertarTipoNemotecnico.DataBind();
            ddlInsertarTipoNemotecnico.Items.Insert(0, new ListItem("-"));

            
            DropDownList ddlInsertarModuloSINPE = (DropDownList)grdNemotecnicos.FooterRow.FindControl("ddlInsertarModuloSINPE");
            ddlInsertarModuloSINPE.DataTextField = "NomOpcion";
            ddlInsertarModuloSINPE.DataValueField = "ValOpcion";
            ddlInsertarModuloSINPE.DataSource = ws_SGService.uwsConsultarOpcionesCatalogo("56", "", "", "");
            ddlInsertarModuloSINPE.DataBind();
            ddlInsertarModuloSINPE.Items.Insert(0, new ListItem("-"));
        }

        protected void grdNemotecnicos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //DataRow ldr_NemotecnicosRow = gds_Nemotecnicos.Tables["Table"].NewRow();
            //ldr_NemotecnicosRow = gds_Nemotecnicos.Tables["Table"].Rows[e.Row.DataItemIndex];

            //string lstr_IdMoneda = ldr_NemotecnicosRow["IdMoneda"].ToString();
            //string lstr_IdSociedadFi = ldr_NemotecnicosRow["IdSociedadFi"].ToString();
            //string lstr_TipoNemotecnico = ldr_NemotecnicosRow["TipoNemotecnico"].ToString();

            if (e.Row.RowState == DataControlRowState.Insert)
            {
                
                DropDownList ddlTipoNemotecnico = (DropDownList)e.Row.FindControl("ddlInsertarTipoNemotecnico");
                ddlTipoNemotecnico.DataTextField = "NomOpcion";
                ddlTipoNemotecnico.DataValueField = "ValOpcion";
                ddlTipoNemotecnico.DataSource = ws_SGService.uwsConsultarOpcionesCatalogo("38", "", "", "");
                ddlTipoNemotecnico.DataBind();
                ddlTipoNemotecnico.Items.Insert(0, new ListItem(""));
                //ddlTipoNemotecnico.SelectedValue = lstr_TipoNemotecnico.ToString().Trim();

                DropDownList ddlItemIdMoneda = (DropDownList)e.Row.FindControl("ddlInsertarIdMoneda");
                ddlItemIdMoneda.DataTextField = "NomMoneda";
                ddlItemIdMoneda.DataValueField = "IdMoneda";
                ddlItemIdMoneda.DataSource = ws_SGService.uwsConsultarMonedas("", "");
                ddlItemIdMoneda.DataBind();
                ddlItemIdMoneda.Items.Insert(0, new ListItem(""));
                //ddlItemIdMoneda.SelectedValue = lstr_IdMoneda.ToString();

                DropDownList ddlItemIdSociedadFi = (DropDownList)e.Row.FindControl("ddlInsertarIdSociedadFi");
                ddlItemIdSociedadFi.DataTextField = "Denominacion";
                ddlItemIdSociedadFi.DataValueField = "IdSociedadFi";
                ddlItemIdSociedadFi.DataSource = ws_SGService.uwsConsultarSociedadesFinancieras("", "", "", "", "", "");
                ddlItemIdSociedadFi.DataBind();
                ddlItemIdSociedadFi.Items.Insert(0, new ListItem("-"));
                //ddlItemIdSociedadFi.SelectedValue = lstr_IdSociedadFi.ToString();

                DropDownList ddlInsertarModuloSINPE = (DropDownList)grdNemotecnicos.FooterRow.FindControl("ddlInsertarModuloSINPE");
                ddlInsertarModuloSINPE.DataTextField = "NomOpcion";
                ddlInsertarModuloSINPE.DataValueField = "ValOpcion";
                ddlInsertarModuloSINPE.DataSource = ws_SGService.uwsConsultarOpcionesCatalogo("56", "", "", "");
                ddlInsertarModuloSINPE.DataBind();
                ddlInsertarModuloSINPE.Items.Insert(0, new ListItem(""));
                                          
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DropDownList ddlEditarIdSociedadFi = (DropDownList)e.Row.FindControl("ddlEditarIdSociedadFi");
                    ddlEditarIdSociedadFi.DataTextField = "Denominacion";
                    ddlEditarIdSociedadFi.DataValueField = "IdSociedadFi";
                    ddlEditarIdSociedadFi.DataSource = ws_SGService.uwsConsultarSociedadesFinancieras("", "", "", "", "", "");
                    ddlEditarIdSociedadFi.DataBind();
                    ddlEditarIdSociedadFi.Items.Insert(0, new ListItem("Seleccionar"));
                    ddlEditarIdSociedadFi.Items.FindByValue((e.Row.FindControl("lblIdSociedadFi") as Label).Text).Selected = true;

                    DropDownList ddlEditarIdMoneda = (DropDownList)e.Row.FindControl("ddlEditarIdMoneda");
                    ddlEditarIdMoneda.DataTextField = "NomMoneda";
                    ddlEditarIdMoneda.DataValueField = "IdMoneda";
                    ddlEditarIdMoneda.DataSource = gds_Monedas.Tables["Table"];
                    ddlEditarIdMoneda.DataBind();
                    ddlEditarIdMoneda.Items.Insert(0, new ListItem("Seleccionar"));
                    ddlEditarIdMoneda.Items.FindByValue((e.Row.FindControl("lblIdMoneda") as Label).Text).Selected = true;
                    
                    DropDownList ddlEditarTipoNemotecnico = (DropDownList)e.Row.FindControl("ddlEditarTipoNemotecnico");
                    ddlEditarTipoNemotecnico.DataTextField = "NomOpcion";
                    ddlEditarTipoNemotecnico.DataValueField = "ValOpcion";
                    ddlEditarTipoNemotecnico.DataSource = ws_SGService.uwsConsultarOpcionesCatalogo("38", "", "", "");
                    ddlEditarTipoNemotecnico.DataBind();
                    ddlEditarTipoNemotecnico.Items.Insert(0, new ListItem("Seleccionar"));
                    ddlEditarTipoNemotecnico.Items.FindByValue(((e.Row.FindControl("lblTipoNemotecnico") as Label).Text.Trim())).Selected = true;

                    DropDownList ddlEditarModuloSINPE = (DropDownList)e.Row.FindControl("ddlEditarModuloSINPE");
                    ddlEditarModuloSINPE.DataTextField = "NomOpcion";
                    ddlEditarModuloSINPE.DataValueField = "ValOpcion";
                    ddlEditarModuloSINPE.DataSource = ws_SGService.uwsConsultarOpcionesCatalogo("56", "", "", "");
                    ddlEditarModuloSINPE.DataBind();
                    ddlEditarModuloSINPE.Items.Insert(0, new ListItem(""));
                    ddlEditarModuloSINPE.Items.FindByValue((e.Row.FindControl("lblModuloSINPE") as Label).Text.Trim()).Selected = true;

                     CheckBox cbEditarEstado = (CheckBox)e.Row.FindControl("cbEditarEstado");
                     cbEditarEstado.Checked = (e.Row.FindControl("lblEstado") as Label).Text.Trim().Equals("A") ? true : false;
                }
            }
        }

        protected void grdNemotecnicos_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dt = (DataTable) gds_Nemotecnicos.Tables["Table"];

            DataView dv = new DataView(dt);

            dv.Sort = e.SortExpression;

            grdNemotecnicos.DataSource = dv;
            grdNemotecnicos.DataBind();
        }

       


        
    }
}