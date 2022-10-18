using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Presentacion.Compartidas;
using System.Globalization;
using System.Web.UI.HtmlControls;

namespace Presentacion.Mantenimiento
{
    public partial class frmEmisiones : BASE
    {
        //Variable referencia a servicio web sistema gestor
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private string str_Usuario = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            str_Usuario = clsSesion.Current.LoginUsuario;

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(str_Usuario))
                {
                    if (!clsSeguridadVistas.MostrarElementos(str_Usuario, Master, ""))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                    {
                        CargarDatosTablaEmisiones();
                        CargarDatosIndicadorEco();
                    }

                }
                else
                {
                    Response.Redirect("~/Login.aspx", true);
                }

            }
            else
            {
                if (string.IsNullOrEmpty(str_Usuario))
                    Response.Redirect("~/Login.aspx", true);

            }

        }

        private void CargarDatosIndicadorEco()
        {
            ddlBusqIndicadorEco.DataTextField = "NomIndicador";
            ddlBusqIndicadorEco.DataValueField = "IdIndicadorEco";
            ddlBusqIndicadorEco.DataSource = ws_SGService.uwsConsultarIndicadoresEconomicos(string.Empty, string.Empty, string.Empty);//.uwsConsultarIndicadorEcos("", "");
            ddlBusqIndicadorEco.DataBind();

            ddlBusqIndicadorEco.Items.Insert(0, "");
        }
        /// <summary>
        /// Llena el gridview con los datos extraidos del WS
        /// </summary>
        private void CargarDatosTablaEmisiones()
        {
            DataSet lds_Emisiones = ws_SGService.uwsConsultarDinamico("Select * from cf.IndicadoresPorTitulo");//.uwsConsultaEmisiones(txtBusqNroValor.Text, ddlBusqIndicadorEco.SelectedValue, txtBusqNemotecnico.Text, ddlBusqTipoEmision.SelectedValue);
            if (lds_Emisiones != null && lds_Emisiones.Tables.Count > 0 && lds_Emisiones.Tables[0].Rows.Count > 0)
            {
                gvEmisiones.DataSource = lds_Emisiones.Tables[0].DefaultView;
                gvEmisiones.DataBind();
            }
            else
            {
                gvEmisiones.DataSource = this.LlenarTablaVacia();
                gvEmisiones.DataBind();
                gvEmisiones.Rows[0].Visible = false;

            }

        }

        /// <summary>
        /// Crea tabla vacia con datos de Emisiones
        /// </summary>
        /// <returns></returns>
        public DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("NroValor", typeof(string));
            ldt_TablaVacia.Columns.Add("Nemotecnico", typeof(string));
            ldt_TablaVacia.Columns.Add("IdIndicadorEco", typeof(string));
            ldt_TablaVacia.Columns.Add("FchReferencia", typeof(string));
            ldt_TablaVacia.Columns.Add("ValorIndicador", typeof(string));
            ldt_TablaVacia.Columns.Add("FchModifica", typeof(string));
            DataRow ldr_FilaTabla = ldt_TablaVacia.NewRow();
            ldt_TablaVacia.Rows.Add(ldr_FilaTabla);
            return ldt_TablaVacia;
        }

        /// <summary>
        /// Metodo de agregar nuevo Emision
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvEmisiones_SelectedIndexChanging(object sender, EventArgs e)
        {

            string lstr_NroValor = "0";
            int lint_TamanoTabla = gvEmisiones.Rows.Count;
            TextBox ltxt_NroValor = (TextBox)gvEmisiones.FooterRow.FindControl("txtNroValor");
            lstr_NroValor = ltxt_NroValor.Text.Trim();
            TextBox ltxt_Nemotecnico = (TextBox)gvEmisiones.FooterRow.FindControl("txtNemotecnico");
            DropDownList lddl_IndicadorEco = (DropDownList)gvEmisiones.FooterRow.FindControl("ddlIndicadorEco");
            DropDownList lddl_Tipo = (DropDownList)gvEmisiones.FooterRow.FindControl("ddlTipoEmision");
            string lstr_Nemotecnico = ltxt_Nemotecnico.Text.Trim();
            string lstr_IdIndicadorEco = lddl_IndicadorEco.SelectedValue.ToString().Trim();
            string lstr_Tipo = lddl_Tipo.SelectedValue.ToString().Trim();
            if (String.IsNullOrEmpty(lstr_IdIndicadorEco) || String.IsNullOrEmpty(lstr_Nemotecnico)
                || String.IsNullOrEmpty(lstr_Tipo) || String.IsNullOrEmpty(lstr_NroValor))
            {
                lblResultado.Text = "No se han ingresado los datos necesarios.";
                lblResultado.Visible = true;
            }
            else
            {
                string query = "Insert into cf.IndicadoresPorTitulo (NroValor, Nemotecnico, IdIndicadorEco, FchReferencia, ValorIndicador, UsrCreacion) values (" + txtNroValor.Text + ", '" + ddlNemotecnico.SelectedValue.Trim() + "', '" + indicador + "', '" + DateTime.Today.ToString("yyyy-MM-dd") + "', " + txtValorIndicadorEco.Text + " , '" + gstr_Usuario + "')";
                ws_SGService.uwsConsultarDinamico(query);
                //string lstr_ResCreacion = ws_SGService.uwsCrearEmision(lstr_NroValor, lstr_IdIndicadorEco, lstr_Tipo, lstr_Nemotecnico, str_Usuario);
                
                if (lstr_ResCreacion == "00")
                {
                    lblResultado.Visible = false;
                    CargarDatosTablaEmisiones();
                }
                else
                {
                    lblResultado.Text = "Error al agregar la Emisión.";
                    lblResultado.Visible = true;
                }
            }
        }

        protected void gvEmisiones_EliminarEmision(object sender, GridViewDeleteEventArgs e)
        {
            int int_indice = Convert.ToInt32(e.RowIndex);
            Label lblFchModificacion = (Label)gvEmisiones.Rows[int_indice].Cells[0].FindControl("lblFchModifica");
            Label lblNroValor = (Label)gvEmisiones.Rows[int_indice].Cells[4].FindControl("lblNroValor");
            Label lblIdIndicadorEco = (Label)gvEmisiones.Rows[int_indice].Cells[1].FindControl("lblIdIndicadorEco");
            string lstr_fecha = lblFchModificacion.Text;
            DateTime ldt_FechaMod = Convert.ToDateTime(lstr_fecha);
            lstr_fecha = ldt_FechaMod.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            //bool lboo_ResEliminacion = ws_SGService.uwsEliminarEmision(lblNroValor.Text, lblIdIndicadorEco.Text.Trim(), lstr_fecha);
            CargarDatosTablaEmisiones();
        }

        protected void gvEmisiones_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEmisiones.EditIndex = e.NewEditIndex;
            CargarDatosTablaEmisiones();
        }

        protected void gvEmisiones_CancActualizacion(object sender, GridViewCancelEditEventArgs e)
        {
            gvEmisiones.EditIndex = -1;
            CargarDatosTablaEmisiones();
        }

        protected void gvEmisiones_ActualizarObj(object sender, GridViewUpdateEventArgs e)
        {
            int int_indice = Convert.ToInt32(e.RowIndex);

            Label lblNroValor = (Label)gvEmisiones.Rows[int_indice].Cells[4].FindControl("lblNroValor");
            Label lddl_NomIndicadorEco = (Label)gvEmisiones.Rows[int_indice].Cells[2].FindControl("lblIdIndicadorEco");
            CheckBox lchk_Habilitado = (CheckBox)gvEmisiones.Rows[int_indice].Cells[3].FindControl("chkHabilitado");
            TextBox ltxt_txtEditNemotecnico = (TextBox)gvEmisiones.Rows[int_indice].Cells[5].FindControl("txtEditNemotecnico");
            //bool lboo_ResActualizacion = ws_SGService.uwsActualizarEmision(lblNroValor.Text, lddl_NomIndicadorEco.Text.Trim(),
            //    lchk_Habilitado.Checked.ToString(), ltxt_txtEditNemotecnico.Text, str_Usuario);
            gvEmisiones.EditIndex = -1;
            CargarDatosTablaEmisiones();

        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvEmisiones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Check if this is our Blank Row being databound, if so make the row invisible
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            //    {
            //        DropDownList ddlIndicadorEco = (DropDownList)e.Row.FindControl("ddlIndicadorEco");
            //        //Bind status data to dropdownlist
            //        ddlIndicadorEco.DataTextField = "NomIndicadorEco";
            //        ddlIndicadorEco.DataValueField = "IdIndicadorEco";
            //        ddlIndicadorEco.DataSource = ws_SGService.uwsConsultarIndicadorEcos("","");
            //        ddlIndicadorEco.DataBind();
            //        DataRowView dr = e.Row.DataItem as DataRowView;
            //    }

            //}
        }

        protected void gvEmisiones_RowEditing1(object sender, GridViewEditEventArgs e)
        {
            gvEmisiones.EditIndex = e.NewEditIndex;
            CargarDatosTablaEmisiones();
        }

        protected void gvEmisiones_DataBound(object sender, EventArgs e)
        {
            DropDownList ddlIndicadorEco = (DropDownList)gvEmisiones.FooterRow.FindControl("ddlIndicadorEco");
            ddlIndicadorEco.DataTextField = "NomIndicador";
            ddlIndicadorEco.DataValueField = "IdIndicadorEco";
            ddlIndicadorEco.DataSource = ws_SGService.uwsConsultarIndicadoresEconomicos(string.Empty, string.Empty, string.Empty);//.uwsConsultarIndicadorEcos("", "");
            ddlIndicadorEco.DataBind();
        }

        protected void gvEmisiones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEmisiones.PageIndex = e.NewPageIndex;
            this.CargarDatosTablaEmisiones();
        }

        protected void btnEmisionConsultar_Click(object sender, EventArgs e)
        {
            this.CargarDatosTablaEmisiones();
        }
    }
}