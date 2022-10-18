using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Presentacion.Compartidas;
using System.Web.UI.HtmlControls;

namespace Presentacion.Seguridad
{
    public partial class Usuarios : BASE
    {

        //Variable reference de servicio web 
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
                        CargarDatosTablaUsuarios();
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

        /// <summary>
        /// Llena el gridview de usuarios con los datos extraidos del WS
        /// </summary>
        private void CargarDatosTablaUsuarios()
        {
            DataSet lds_Usuarios = ws_SGService.uwsConsultaUsuarios(txtBusqIdUsuario.Text, "", "", txtBusqNomUsuario.Text);
            if (lds_Usuarios.Tables.Count > 0 && lds_Usuarios.Tables["Table"].Rows.Count > 0)
            {
                gvUsuarios.DataSource = lds_Usuarios.Tables["Table"];
                gvUsuarios.DataBind();
            }
            else
            {
                gvUsuarios.DataSource = this.LlenarTablaVacia();
                gvUsuarios.DataBind();
                gvUsuarios.Rows[0].Visible = false;
            }
        }

        /// <summary>
        /// Esconde las columnas que se obtienen del WS pero el usuario no debe visualizar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvUsuarios_RowCreated(object sender, GridViewRowEventArgs e)
        {
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[7].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[13].Visible = false;
            e.Row.Cells[14].Visible = false;
            e.Row.Cells[15].Visible = false;
            e.Row.Cells[16].Visible = false;

        }

        /// <summary>
        /// Metodo que maneja el evento del clic del boton NuevoUsr
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNuevoUsr_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Seguridad/GestionUsuarios/NuevoUsuario.aspx");
        }

        /// <summary>
        /// Metodo que maneja la accion sobre boton de editar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvUsuarios_Edicion(object sender, GridViewEditEventArgs e)
        {
            gvUsuarios.SelectedIndex = e.NewEditIndex;
            Label lblIdUsuario = (Label)gvUsuarios.Rows[e.NewEditIndex].Cells[0].FindControl("lblIdUsuario");
            string lstr_IdUsuario = lblIdUsuario.Text;
            string lstr_Url = String.Format("~/Seguridad/GestionUsuarios/EdicionUsuario.aspx?ID={0}", lstr_IdUsuario);
            Response.Redirect(lstr_Url, false);
        }

        /// <summary>
        /// Crea una tabla vacia con las columnas 
        /// </summary>
        /// <returns></returns>
        private DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("IdUsuario", typeof(string));
            ldt_TablaVacia.Columns.Add("NomUsuario", typeof(string));
            ldt_TablaVacia.Columns.Add("CorreoUsuario", typeof(string));
            ldt_TablaVacia.Columns.Add("CtaHabilitada", typeof(Boolean));
            ldt_TablaVacia.Columns.Add("Administrador", typeof(Boolean));
            ldt_TablaVacia.Columns.Add("Activo", typeof(Boolean));
            DataRow ldr_FilaTabla = ldt_TablaVacia.NewRow();
            ldr_FilaTabla["CtaHabilitada"] = false;
            ldr_FilaTabla["Administrador"] = false;
            ldr_FilaTabla["Activo"] = false;
            ldt_TablaVacia.Rows.Add(ldr_FilaTabla);
            return ldt_TablaVacia;
        }

        //public string test
        //{
        //    get
        //    {
        //        return (string)ViewState["test"] ?? "hi";
        //    }
        //    set
        //    {
        //        ViewState["test"] = value;
        //    }
        //}

        protected void gvUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvUsuarios.PageIndex = e.NewPageIndex;
            this.CargarDatosTablaUsuarios();
        }

        /// <summary>
        /// Eventos que se disparan al seleccionar una opcion del gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //Consulta de usuarios
            if (e.CommandName == "consulta")
            {
                string index = Convert.ToString(e.CommandArgument.ToString());
                string lstr_Url = String.Format("~/Seguridad/GestionUsuarios/DetallesUsuario.aspx?ID={0}", index);
                Response.Redirect(lstr_Url, false);
            }

        }

        protected void btnUsuarioConsultar_Click(object sender, EventArgs e)
        {
            this.CargarDatosTablaUsuarios();
        }
    }
}