using Presentacion.Compartidas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Seguridad.GestionUsuarios
{
    public partial class DetallesUsuario : BASE
    {
        //Variable reference de servicio web 
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private string lst_idUsuario = String.Empty;
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
                        if (Request.QueryString["ID"] != null)
                        {
                            lst_idUsuario = Request.QueryString["ID"];
                            ConsultarUsuario(lst_idUsuario);
                            ConsultarRolesUsuario(lst_idUsuario);
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
        /// Obtiene los datos del usuario
        /// </summary>
        /// <param name="str_IdUsuario"></param>
        private void ConsultarUsuario(string str_IdUsuario)
        {

            DataSet lds_Usuarios = ws_SGService.uwsConsultarUsuarios(str_IdUsuario, "", "");
            DataTable ldt_Usuario = lds_Usuarios.Tables["Table"];
            if (ldt_Usuario.Rows.Count > 0)
            {
                string lstr_TipoID = ldt_Usuario.Rows[0]["TipoIdUsuario"].ToString();
                switch (lstr_TipoID.Trim())
                {
                    case "DI":
                        {
                            lstr_TipoID = "DIMEX";
                            break;
                        }
                    case "J":
                        {
                            lstr_TipoID = "Jurídico";
                            break;
                        }
                    default:
                        {
                            lstr_TipoID = "Físico";
                            break;
                        }
                }
                txtIdUsuario.Text = str_IdUsuario;
                txtCorreoUsr.Text = ldt_Usuario.Rows[0]["CorreoUsuario"].ToString();
                txtNombreUsuario.Text = ldt_Usuario.Rows[0]["NomUsuario"].ToString();
                txtFchRegistro.Text = ldt_Usuario.Rows[0]["FchCreacion"].ToString();
                txtFchCambioClave.Text = ldt_Usuario.Rows[0]["FchUltimoCambioClave"].ToString();
                txtFchUltimaSesion.Text = ldt_Usuario.Rows[0]["FchUltimaSesion"].ToString();
                txtTipoIDentificacion.Text = lstr_TipoID;
                txtInstitucion.Text = ldt_Usuario.Rows[0]["NomSociedad"].ToString();
                chkActivo.Checked = Convert.ToBoolean(ldt_Usuario.Rows[0]["Activo"].ToString());
                chkAdmin.Checked = Convert.ToBoolean(ldt_Usuario.Rows[0]["Administrador"].ToString());
                chkCtaHabilitada.Checked = Convert.ToBoolean(ldt_Usuario.Rows[0]["CtaHabilitada"].ToString());
            }

        }

        /// <summary>
        /// Metodo para obtener los roles del usuario
        /// </summary>
        /// <param name="str_IdUsuario"></param>
        private void ConsultarRolesUsuario(string str_IdUsuario)
        {

            DataSet lds_RolesUsuario = ws_SGService.uwsConsultarRolesUsuario("", str_IdUsuario);
            if (lds_RolesUsuario.Tables.Count > 0)
            {
                DataTable ldt_RolesUsuario = lds_RolesUsuario.Tables["Table"];
                if (ldt_RolesUsuario.Rows.Count > 0)
                {
                    gvRolesUsuario.DataSource = ldt_RolesUsuario;
                    gvRolesUsuario.DataBind();
                    List<string> lstr_RolesUsr = new List<string>();
                    for (int numfila = 0; numfila < ldt_RolesUsuario.Rows.Count; numfila++)
                    {
                        lstr_RolesUsr.Add(ldt_RolesUsuario.Rows[numfila]["IdRol"].ToString());
                    }
                    String[] last_RolesUsr = lstr_RolesUsr.ToArray();
                    string lstr_Roles = String.Join("|||", last_RolesUsr);
                }
            }
            //else
            //{
            //    gvRolesUsuario.DataSource = this.LlenarTablaVacia();
            //    gvRolesUsuario.DataBind();
            //    gvRolesUsuario.Rows[0].Visible = false;
            //}
        }

        /// <summary>
        /// Genera una tabla de roles nueva vacia
        /// </summary>
        /// <returns></returns>
        public DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("FchModifica", typeof(string));
            ldt_TablaVacia.Columns.Add("IdRol", typeof(string));
            ldt_TablaVacia.Columns.Add("DescRol", typeof(string));
            ldt_TablaVacia.Columns.Add("Habilitado", typeof(Boolean));
            DataRow ldr_FilaTabla = ldt_TablaVacia.NewRow();
            ldr_FilaTabla["Habilitado"] = false;
            ldt_TablaVacia.Rows.Add(ldr_FilaTabla);
            return ldt_TablaVacia;
        }

        /// <summary>
        /// Evento que se dispara al presionar el boton de cancelar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Seguridad/Usuarios.aspx"); 
        }
    }
}