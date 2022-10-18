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
    public partial class GestionRoles : BASE
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
                        CargarDatosTablaRoles();
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
        /// Llena el gridview con los roles obtenidos del WS
        /// </summary>
        private void CargarDatosTablaRoles()
        {
            DataSet lds_Roles = ws_SGService.uwsConsultaRoles(txtBusqIdRol.Text, txtBusqNomRol.Text);
            if (lds_Roles.Tables.Count > 0 && lds_Roles.Tables["Table"].Rows.Count > 0)
            {
                gvRoles.DataSource = lds_Roles.Tables["Table"];
                gvRoles.DataBind();
            }
            else
            {
                gvRoles.DataSource = this.LlenarTablaVacia();
                gvRoles.DataBind();
                gvRoles.Rows[0].Visible = false;
            }
        }


        protected void gvRoles_RowCreated(object sender, GridViewRowEventArgs e)
        {
        }

        /// <summary>
        /// Evento al editar rol, redirige a la pagina de edicion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvRoles_EditarRol(object sender, GridViewEditEventArgs e)
        {
            Label lblIdRol = (Label)gvRoles.Rows[e.NewEditIndex].Cells[1].FindControl("lblIdRol");
            Label lblDescRol = (Label)gvRoles.Rows[e.NewEditIndex].Cells[2].FindControl("lblDescRol");
            Label lblFchModificacon = (Label)gvRoles.Rows[e.NewEditIndex].Cells[0].FindControl("lblFchModificacion");
            DateTime ldt_Fecha = Convert.ToDateTime(lblFchModificacon.Text);
            string lstr_IdRol = lblIdRol.Text;
            string lstr_Rol = lblDescRol.Text;
            clsSesion.Current.FechaUltimaConsulta = ldt_Fecha.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            string enlace = String.Format("~/Seguridad/GestRoles/EditarRol.aspx?Num={0}", lstr_IdRol);
            clsSesion.Current.DescripcionRol = lblDescRol.Text;
            hdnDescRol.Value = lstr_Rol;
            Response.Redirect(enlace, false);

        }

        /// <summary>
        /// Crea tabla vacia
        /// </summary>
        /// <returns></returns>
        private DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("IdRol", typeof(string));
            ldt_TablaVacia.Columns.Add("DescRol", typeof(string));
            ldt_TablaVacia.Columns.Add("FchModifica", typeof(string));
            ldt_TablaVacia.Columns.Add("Habilitado", typeof(Boolean));
            DataRow ldr_FilaTabla = ldt_TablaVacia.NewRow();
            ldr_FilaTabla["Habilitado"] = false;
            ldt_TablaVacia.Rows.Add(ldr_FilaTabla);
            return ldt_TablaVacia;
        }

        /// <summary>
        /// Evento al annadir rol
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            string lstr_DescRol = (gvRoles.FooterRow.FindControl("txtDescNuevoRol") as TextBox).Text;
            if (!String.IsNullOrEmpty(lstr_DescRol.Trim()) && !String.IsNullOrEmpty(clsSesion.Current.IdSesion))
            {
                bool lboo_ResCreacion = ws_SGService.uwsCrearRol(lstr_DescRol, clsSesion.Current.IdSesion, str_Usuario);
                if (lboo_ResCreacion)
                {
                    MessageBox.Show("Se ha creado un nuevo rol.");
                    Response.Redirect(Request.RawUrl);
                }
            }
        }


        /// <summary>
        /// Evento al eliminar un rol
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvRoles_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int int_indice = Convert.ToInt32(e.RowIndex);
            Label lblFchModificacion = (Label)gvRoles.Rows[int_indice].Cells[0].FindControl("lblFchModificacion");
            Label lblIdRol = (Label)gvRoles.Rows[int_indice].Cells[1].FindControl("lblIdRol");
            DateTime ldt_FchModificacion = Convert.ToDateTime(lblFchModificacion.Text);
            string lstr_Fecha = ldt_FchModificacion.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            bool lboo_ResCreacion = ws_SGService.uwsEliminarRol(lblIdRol.Text, lstr_Fecha);
            CargarDatosTablaRoles();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvRoles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRoles.PageIndex = e.NewPageIndex;
            this.CargarDatosTablaRoles();
        }

        protected void btnRolesConsultar_Click(object sender, EventArgs e)
        {
            this.CargarDatosTablaRoles();
        }
    }
}