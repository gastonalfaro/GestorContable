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

namespace Presentacion.Seguridad
{
    public partial class GestionObjetos : BASE
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
                    if (!clsSeguridadVistas.MostrarElementos(str_Usuario, Master, "OBJ_SG"))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                    {
                        CargarDatosTablaObjetos();
                        CargarDatosModulo();
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

        private void CargarDatosModulo()
        {
            ddlBusqModulo.Items.Clear();
            DataSet vdsModulos = ws_SGService.uwsConsultarModulos("", "");
            if (vdsModulos.Tables.Count > 0 && vdsModulos.Tables[0].Rows.Count > 0) 
            {
                ddlBusqModulo.DataTextField = "NomModulo";
                ddlBusqModulo.DataValueField = "IdModulo";
                ddlBusqModulo.DataSource = vdsModulos;
                ddlBusqModulo.DataBind();
            }//if
            ddlBusqModulo.Items.Insert(0, "");
        }

        /// <summary>
        /// Llena el gridview con los datos extraidos del WS
        /// </summary>
        private void CargarDatosTablaObjetos()
        {
            DataSet lds_Objetos = ws_SGService.uwsConsultaObjetos(txtBusqIdObjeto.Text, ddlBusqModulo.SelectedValue, txtBusqNomObjeto.Text, ddlBusqTipoObjeto.SelectedValue);
            if (lds_Objetos != null && lds_Objetos.Tables.Count > 0 && lds_Objetos.Tables[0].Rows.Count > 0)
            {
                gvObjetos.DataSource = lds_Objetos.Tables[0].DefaultView;
                gvObjetos.DataBind();
            }
            else
            {
                gvObjetos.DataSource = this.LlenarTablaVacia();
                gvObjetos.DataBind();
                gvObjetos.Rows[0].Visible = false;

            }

        }

        /// <summary>
        /// Crea tabla vacia con datos de objetos
        /// </summary>
        /// <returns></returns>
        public DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("FchModifica", typeof(string));
            ldt_TablaVacia.Columns.Add("IdObjeto", typeof(string));
            ldt_TablaVacia.Columns.Add("IdModulo", typeof(string));
            ldt_TablaVacia.Columns.Add("NomModulo", typeof(string));
            ldt_TablaVacia.Columns.Add("TipoObjeto", typeof(string));
            ldt_TablaVacia.Columns.Add("DescObjeto", typeof(string));
            ldt_TablaVacia.Columns.Add("Habilitado", typeof(string));
            DataRow ldr_FilaTabla = ldt_TablaVacia.NewRow();
            ldt_TablaVacia.Rows.Add(ldr_FilaTabla);
            return ldt_TablaVacia;
        }

        /// <summary>
        /// Metodo de agregar nuevo objeto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvObjetos_SelectedIndexChanging(object sender, EventArgs e)
        {

            string lstr_IdObjeto = "0";
            int lint_TamanoTabla = gvObjetos.Rows.Count;
            TextBox ltxt_IdObjeto = (TextBox)gvObjetos.FooterRow.FindControl("txtIdObjeto");
            lstr_IdObjeto = ltxt_IdObjeto.Text.Trim();
            TextBox ltxt_DescObjeto = (TextBox)gvObjetos.FooterRow.FindControl("txtDescObjeto");
            DropDownList lddl_Modulo = (DropDownList)gvObjetos.FooterRow.FindControl("ddlModulo");
            DropDownList lddl_Tipo = (DropDownList)gvObjetos.FooterRow.FindControl("ddlTipoObjeto");
            string lstr_DescObjeto = ltxt_DescObjeto.Text.Trim();
            string lstr_IdModulo = lddl_Modulo.SelectedValue.ToString().Trim();
            string lstr_Tipo = lddl_Tipo.SelectedValue.ToString().Trim();
            if (String.IsNullOrEmpty(lstr_IdModulo) || String.IsNullOrEmpty(lstr_DescObjeto)
                || String.IsNullOrEmpty(lstr_Tipo) || String.IsNullOrEmpty(lstr_IdObjeto))
            {
                lblResultado.Text = "No se han ingresado los datos necesarios.";
                lblResultado.Visible = true;
            }
            else
            {

                string lstr_ResCreacion = ws_SGService.uwsCrearObjeto(lstr_IdObjeto, lstr_IdModulo, lstr_Tipo, lstr_DescObjeto, str_Usuario);
                if (lstr_ResCreacion == "-1")
                {
                    lblResultado.Text = "El nombre del objeto ya ha sido utilizado.";
                    lblResultado.Visible = true;
                }
                if (lstr_ResCreacion == "00")
                {
                    lblResultado.Visible = false;
                    CargarDatosTablaObjetos();
                }
                else
                {
                    lblResultado.Text = "Error al agregar el objeto.";
                    lblResultado.Visible = true;
                }
            }
        }

        protected void gvObjetos_EliminarObjeto(object sender, GridViewDeleteEventArgs e)
        {
            int int_indice = Convert.ToInt32(e.RowIndex);
            Label lblFchModificacion = (Label)gvObjetos.Rows[int_indice].Cells[0].FindControl("lblFchModifica");
            Label lblIdObjeto = (Label)gvObjetos.Rows[int_indice].Cells[4].FindControl("lblIdObjeto");
            Label lblIdModulo = (Label)gvObjetos.Rows[int_indice].Cells[1].FindControl("lblIdModulo");
            string lstr_fecha = lblFchModificacion.Text;
            DateTime ldt_FechaMod = Convert.ToDateTime(lstr_fecha);
            lstr_fecha = ldt_FechaMod.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            bool lboo_ResEliminacion = ws_SGService.uwsEliminarObjeto(lblIdObjeto.Text, lblIdModulo.Text.Trim(), lstr_fecha);
            CargarDatosTablaObjetos();
        }

        protected void gvObjetos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvObjetos.EditIndex = e.NewEditIndex;
            CargarDatosTablaObjetos();
        }

        protected void gvObjetos_CancActualizacion(object sender, GridViewCancelEditEventArgs e)
        {
            gvObjetos.EditIndex = -1;
            CargarDatosTablaObjetos();
        }

        protected void gvObjetos_ActualizarObj(object sender, GridViewUpdateEventArgs e)
        {
            int int_indice = Convert.ToInt32(e.RowIndex);

            Label lblIdObjeto = (Label)gvObjetos.Rows[int_indice].Cells[4].FindControl("lblIdObjeto");
            Label lddl_NomModulo = (Label)gvObjetos.Rows[int_indice].Cells[2].FindControl("lblIdModulo");
            CheckBox lchk_Habilitado = (CheckBox)gvObjetos.Rows[int_indice].Cells[3].FindControl("chkHabilitado");
            TextBox ltxt_txtEditDescObjeto = (TextBox)gvObjetos.Rows[int_indice].Cells[5].FindControl("txtEditDescObjeto");
            bool lboo_ResActualizacion = ws_SGService.uwsActualizarObjeto(lblIdObjeto.Text, lddl_NomModulo.Text.Trim(),
                lchk_Habilitado.Checked.ToString(), ltxt_txtEditDescObjeto.Text, str_Usuario);
            gvObjetos.EditIndex = -1;
            CargarDatosTablaObjetos();

        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvObjetos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //Check if this is our Blank Row being databound, if so make the row invisible
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            //    {
            //        DropDownList ddlModulo = (DropDownList)e.Row.FindControl("ddlModulo");
            //        //Bind status data to dropdownlist
            //        ddlModulo.DataTextField = "NomModulo";
            //        ddlModulo.DataValueField = "IdModulo";
            //        ddlModulo.DataSource = ws_SGService.uwsConsultarModulos("","");
            //        ddlModulo.DataBind();
            //        DataRowView dr = e.Row.DataItem as DataRowView;
            //    }

            //}
        }

        protected void gvObjetos_RowEditing1(object sender, GridViewEditEventArgs e)
        {
            gvObjetos.EditIndex = e.NewEditIndex;
            CargarDatosTablaObjetos();
        }

        protected void gvObjetos_DataBound(object sender, EventArgs e)
        {
            DropDownList ddlModulo = (DropDownList)gvObjetos.FooterRow.FindControl("ddlModulo");
            ddlModulo.DataTextField = "NomModulo";
            ddlModulo.DataValueField = "IdModulo";
            ddlModulo.DataSource = ws_SGService.uwsConsultarModulos("", "");
            ddlModulo.DataBind();
        }

        protected void gvObjetos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvObjetos.PageIndex = e.NewPageIndex;
            this.CargarDatosTablaObjetos();
        }

        protected void btnObjetoConsultar_Click(object sender, EventArgs e)
        {
            this.CargarDatosTablaObjetos();
        }
    }
}