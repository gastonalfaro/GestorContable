using Presentacion.Compartidas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Perfil
{
    public partial class PerfilUsuario : BASE
    {
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private string lst_idUsuario = String.Empty;
        private string str_Usuario = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            str_Usuario = clsSesion.Current.LoginUsuario;
            if (String.IsNullOrEmpty(str_Usuario))
            {

                Response.Redirect("~/Login.aspx", true);
            }
            else
            {
                if (!IsPostBack)
                {
                    ConsultarUsuario(str_Usuario);
                    ConsultarRolesUsuario(str_Usuario);
                }
            }

        }

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
                txtTipoIDentificacion.Text = lstr_TipoID;
                txtInstitucion.Text = String.Format("{0}-{1}",ldt_Usuario.Rows[0]["IdSociedadGL"].ToString().Trim(), ldt_Usuario.Rows[0]["NomSociedad"].ToString());
                chkActivo.Checked = Convert.ToBoolean(ldt_Usuario.Rows[0]["Activo"].ToString());
                chkAdmin.Checked = Convert.ToBoolean(ldt_Usuario.Rows[0]["Administrador"].ToString());
                chkCtaHabilitada.Checked = Convert.ToBoolean(ldt_Usuario.Rows[0]["CtaHabilitada"].ToString());
                clsSesion.Current.FechaUltimaConsulta = ldt_Usuario.Rows[0]["FchModifica"].ToString();
            }


        }

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

        private DataTable LlenarTablaVacia()
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

        protected void btnGuardarPerfil_Click(object sender, EventArgs e)
        {
            string lstr_Institucion = txtInstitucion.Text;
            string[] lArrayInst = lstr_Institucion.Split('-');
            string[] ResultadoActualizacion = new string[2];
            ResultadoActualizacion[0] = "99";
            ResultadoActualizacion[1] = "Error al realizar la actualizacion";
            string lstr_IdUsuario = txtIdUsuario.Text;
            string lstr_NomUsuario = txtNombreUsuario.Text;
            string lstr_CorreoUsuario = txtCorreoUsr.Text;
            string lstr_Activo = Convert.ToString(chkActivo.Checked);
            string lstr_Administrador = Convert.ToString(chkAdmin.Checked);
            string lst_CtaHabilitada = Convert.ToString(chkCtaHabilitada.Checked);
            string lstr_FchModifica = clsSesion.Current.FechaUltimaConsulta;
            string lstr_TipoID = txtTipoIDentificacion.Text;
            string lstr_IDInstitucion = lArrayInst[0];
            DateTime lstr_FechaModificado = Convert.ToDateTime(lstr_FchModifica);
            string lstr_fecha = String.Empty;
            bool lboo_CamposCompletos = ValidacionCambios(lstr_IdUsuario, lstr_TipoID, lstr_NomUsuario, lstr_CorreoUsuario,
                lstr_Activo, lstr_Administrador, lst_CtaHabilitada, lstr_IDInstitucion);
            switch (lstr_TipoID.Trim())
            {
                case "Físico":
                    {
                        lstr_TipoID = "F";
                        break;
                    }
                case "Jurídico":
                    {
                        lstr_TipoID = "J";
                        break;
                    }
                default:
                    {
                        lstr_TipoID = "DI";
                        break;
                    }
            }
            if (lboo_CamposCompletos)
            {
                lstr_fecha = lstr_FechaModificado.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                ResultadoActualizacion = ws_SGService.uwsActualizarUsuario(lstr_IdUsuario, lstr_TipoID, lstr_NomUsuario, lstr_CorreoUsuario,
                    lstr_Activo, lstr_Administrador, lst_CtaHabilitada, lstr_IDInstitucion, clsSesion.Current.LoginUsuario, lstr_fecha);
                if (ResultadoActualizacion[0] == "00")
                {
                    MessageBox.Show("Se han guardado los cambios.");
                    ConsultarUsuario(str_Usuario);
                }

                else if (ResultadoActualizacion[0] == "-2")
                {  
                    MessageBox.Show(ResultadoActualizacion[1]);
                    ConsultarUsuario(str_Usuario);

                }
                else
                {
                    MessageBox.Show("Error al realizar los cambios.");
                }

            }
            else
            {
                MessageBox.Show("Debe ingresar todos los datos solicitados.");
            }
        }

        private bool ValidacionCambios(string str_IdUSuario, string str_TipoID, string str_NomUsuario,
           string str_CorreoUsuario, string str_Activo, string str_Administrador, string str_CtaHabilitada, string str_Institucion)
        {
            bool lboo_ResValidacion = false;
            if (!String.IsNullOrEmpty(str_IdUSuario) && !String.IsNullOrEmpty(str_TipoID)
                && !String.IsNullOrEmpty(str_NomUsuario) && !String.IsNullOrEmpty(str_CorreoUsuario)
                && !String.IsNullOrEmpty(str_Activo) && !String.IsNullOrEmpty(str_Administrador)
                && !String.IsNullOrEmpty(str_CtaHabilitada) && !String.IsNullOrEmpty(str_Institucion))
            {
                lboo_ResValidacion = true;
            }
            return lboo_ResValidacion;
        }

        protected void gvRolesUsuario_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRolesUsuario.PageIndex = e.NewPageIndex;
            this.ConsultarRolesUsuario(str_Usuario);
        }

    }
}