using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Presentacion.Compartidas;
using System.Globalization;

namespace Presentacion.Seguridad.GestRoles
{
    public partial class EditarRol : BASE
    {

        //Variable referencia a servicio web sistema gestor
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private string IdRol = String.Empty;
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
                        IdRol = Request.QueryString["Num"];
                        if (!String.IsNullOrEmpty(IdRol))
                        {
                            string lstr_IdRol = IdRol;
                            ConsultarRol(lstr_IdRol);
                            CargarDatosTablaPermisos(lstr_IdRol);
                        }
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
        /// Carga los datos de la tabla de objetos asociados a cada rol
        /// </summary>
        /// <param name="str_IdRol"></param>
        private void CargarDatosTablaPermisos(string str_IdRol)
        {
            DataSet lds_Permisos = ws_SGService.uwsConsultarRolesObjeto(str_IdRol, "", txtBusqNomObjeto.Text);
            DataTable ldt_Permisos = lds_Permisos.Tables["Table"];
            if (ldt_Permisos.Rows.Count > 0)
            {
                gvPermisosRol.DataSource = lds_Permisos.Tables["Table"];
                gvPermisosRol.DataBind();
            }
            else
            {
                gvPermisosRol.DataSource = this.LlenarTablaVacia();
                gvPermisosRol.DataBind();
                gvPermisosRol.Rows[0].Visible = false;
            }

        }

        protected void gvPermisosRol_EdicionPerm(object sender, GridViewEditEventArgs e)
        {
            IdRol = Request.QueryString["Num"];
            gvPermisosRol.EditIndex = e.NewEditIndex;
            CargarDatosTablaPermisos(IdRol);
        }

        protected void gvPermisos_CancelarEdicion(object sender, GridViewCancelEditEventArgs e)
        {
            IdRol = Request.QueryString["Num"];
            gvPermisosRol.EditIndex = -1;
            CargarDatosTablaPermisos(IdRol);
        }

        protected void gvPermisosRol_ActualizaPerm(object sender, GridViewUpdateEventArgs e)
        {
            IdRol = Request.QueryString["Num"];
            Label lblFchModifica = (Label)gvPermisosRol.Rows[e.RowIndex].FindControl("lblFchModifica");
            Label lblIdObjeto = (Label)gvPermisosRol.Rows[e.RowIndex].FindControl("lblIdObjeto");
            CheckBox chkEdicionConsultar = (CheckBox)gvPermisosRol.Rows[e.RowIndex].FindControl("chkEdicionConsultar");
            CheckBox chkEdicionInsertar = (CheckBox)gvPermisosRol.Rows[e.RowIndex].FindControl("chkEdicionInsertar");
            CheckBox chkEdicionBorrar = (CheckBox)gvPermisosRol.Rows[e.RowIndex].FindControl("chkEdicionBorrar");
            CheckBox chkEdicionActualizar = (CheckBox)gvPermisosRol.Rows[e.RowIndex].FindControl("chkEdicionActualizar");
            CheckBox chkEdicionExportar = (CheckBox)gvPermisosRol.Rows[e.RowIndex].FindControl("chkEdicionExportar");
            CheckBox chkEdicionImprimir = (CheckBox)gvPermisosRol.Rows[e.RowIndex].FindControl("chkEdicionImprimir");
            DateTime ldt_FechaMod = Convert.ToDateTime(lblFchModifica.Text);
            string lstr_FechaMod = ldt_FechaMod.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            //Metodo de actualizacion
            bool lboo_ResActualizacion = ws_SGService.uwsActualizarRolObjeto(IdRol, lblIdObjeto.Text, chkEdicionConsultar.Checked.ToString(),
                chkEdicionInsertar.Checked.ToString(), chkEdicionBorrar.Checked.ToString(), chkEdicionActualizar.Checked.ToString(),
                chkEdicionExportar.Checked.ToString(), chkEdicionImprimir.Checked.ToString(), str_Usuario, lstr_FechaMod);
            gvPermisosRol.EditIndex = -1;
            CargarDatosTablaPermisos(IdRol);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            string lstr_Url = String.Format("~/Seguridad/GestionRoles.aspx");
            Response.Redirect(lstr_Url, false);
        }

        private DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();


            ldt_TablaVacia.Columns.Add("NomModulo", typeof(string));
            ldt_TablaVacia.Columns.Add("FchModifica", typeof(string));
            ldt_TablaVacia.Columns.Add("IdObjeto", typeof(string));
            ldt_TablaVacia.Columns.Add("DescObjeto", typeof(string));
            ldt_TablaVacia.Columns.Add("Consultar", typeof(string));
            ldt_TablaVacia.Columns.Add("Insertar", typeof(string));
            ldt_TablaVacia.Columns.Add("Borrar", typeof(string));
            ldt_TablaVacia.Columns.Add("Actualizar", typeof(string));
            ldt_TablaVacia.Columns.Add("Exportar", typeof(string));
            ldt_TablaVacia.Columns.Add("Imprimir", typeof(string));



            DataRow ldr_FilaTabla = ldt_TablaVacia.NewRow();
            ldt_TablaVacia.Rows.Add(ldr_FilaTabla);
            return ldt_TablaVacia;
        }

        protected void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            bool lboo_ResActualizacion = false;

            //DateTime lstr_FechaModificado = Convert.ToDateTime(clsSesion.Current.FechaUltimaConsulta);
            string lstr_fecha = clsSesion.Current.FechaUltimaConsulta;
            DateTime ldt_FechaMod = Convert.ToDateTime(lstr_fecha);
            //lstr_fecha = ldt_FechaMod.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            lstr_fecha = ldt_FechaMod.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            IdRol = Request.QueryString["Num"];
            string lstr_Descripcion = txtDescripcion.Text.Trim();
            if (ValidacionCambios(IdRol, lstr_Descripcion))
            {
                lboo_ResActualizacion = ws_SGService.uwsActualizarRol(IdRol, txtDescripcion.Text.Trim(), clsSesion.Current.IdSesion, Convert.ToString(chkHabilitado.Checked), str_Usuario, lstr_fecha);
                if (lboo_ResActualizacion)
                {
                    string lstr_Url = String.Format("~/Seguridad/GestionRoles.aspx");
                    Response.Redirect(lstr_Url, false);
                }
                else
                {
                    MessageBox.Show("Error al realizar los cambios.");
                }
            }
            else
            {
                txtDescripcion.Text = lstr_Descripcion;
                MessageBox.Show("Debe ingresar todos los datos solicitados.");
            }
        }

        private bool ValidacionCambios(string str_IdRol, string txtDescripcion)
        {
            bool lboo_ResValidacion = false;
            if (!String.IsNullOrEmpty(str_IdRol) && !String.IsNullOrEmpty(txtDescripcion))
            {
                lboo_ResValidacion = true;
            }
            return lboo_ResValidacion;
        }

        private void ConsultarRol(string str_IdRol)
        {
            DataSet lds_Roles = ws_SGService.uwsConsultarRoles(str_IdRol);
            DataTable ldt_Roles = lds_Roles.Tables["Table"];
            if (ldt_Roles.Rows.Count > 0)
            {
                txtDescripcion.Text = ldt_Roles.Rows[0]["DescRol"].ToString();
                chkHabilitado.Checked = Convert.ToBoolean(ldt_Roles.Rows[0]["Habilitado"].ToString());
            }

        }

        protected void gvPermisosRol_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPermisosRol.PageIndex = e.NewPageIndex;
            this.CargarDatosTablaPermisos(Request.QueryString["Num"]);
        }

        protected void btnObjetoConsultar_Click(object sender, EventArgs e)
        {
            this.CargarDatosTablaPermisos(Request.QueryString["Num"]);
        }

    }
}