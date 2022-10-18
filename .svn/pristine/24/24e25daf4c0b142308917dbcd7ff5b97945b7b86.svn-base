using Presentacion.Compartidas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Mantenimiento.Gestiones
{
    public partial class frmNuevoPropietario : BASE
    {
        #region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();

        private Char lchr_MensajeError;
        private Char lchr_MensajeExito;

        private String gstr_Usuario = String.Empty;
        private String[] garr_Modulos;

        //private static DataSet gds_SociedadesGL = new DataSet();
        protected DataSet gds_SociedadesGL
        {
            get
            {
                if (ViewState["gds_SociedadesGL"] == null)
                    ViewState["gds_SociedadesGL"] = new DataSet();
                return (DataSet)ViewState["gds_SociedadesGL"];
            }
            set
            {
                ViewState["gds_SociedadesGL"] = value;
            }
        }
        //private static DataSet gds_SociedadesFi = new DataSet();
        protected DataSet gds_SociedadesFi
        {
            get
            {
                if (ViewState["gds_SociedadesFi"] == null)
                    ViewState["gds_SociedadesFi"] = new DataSet();
                return (DataSet)ViewState["gds_SociedadesFi"];
            }
            set
            {
                ViewState["gds_SociedadesFi"] = value;
            }
        }

        DataTable subjects = new DataTable();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
           
            gstr_Usuario = clsSesion.Current.LoginUsuario;
            lchr_MensajeError = clsSesion.Current.chr_MensajeError;
            lchr_MensajeExito = clsSesion.Current.chr_MensajeExito;

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(gstr_Usuario))
                {
                    garr_Modulos = clsSesion.Current.PermisosModulos;

                    if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmPropietarios"))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                    {
                        ConsultarSociedadesGL();
                        ConsultarSociedadesFi();
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

        protected void btnPropietarioVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Mantenimiento/frmPropietarios.aspx", false);
        }

        private void MostarMensaje(string str_TextMensaje, char chr_tipo)
        {
            if (chr_tipo == '1')
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

        private void ConsultarSociedadesGL()
        {
            gds_SociedadesFi = ws_SGService.uwsConsultarSociedadesGL("", "", "", "", "");

            if (gds_SociedadesFi.Tables["Table"].Rows.Count > 0)
            {

                ddlIdSociedadFi.DataSource = gds_SociedadesFi.Tables["Table"];
                ddlIdSociedadFi.DataTextField = "NomSociedad";
                ddlIdSociedadFi.DataValueField = "IdSociedadGL";
                ddlIdSociedadFi.DataBind();
            }
            else
            {
                ddlIdSociedadFi.DataSource = this.LlenarTablaSociedadesGL();
                ddlIdSociedadFi.DataBind();
            }
        }

        private DataTable LlenarTablaSociedadesGL()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("IdSociedadGL", typeof(string));
            ldt_TablaVacia.Columns.Add("NomSociedad", typeof(string));
            DataRow ldr_FilaTabla = ldt_TablaVacia.NewRow();
            ldt_TablaVacia.Rows.Add(ldr_FilaTabla);
            return ldt_TablaVacia;
        }

        private void ConsultarSociedadesFi()
        {
            gds_SociedadesFi = ws_SGService.uwsConsultarSociedadesFinancieras("","","","","","");

            if (gds_SociedadesFi.Tables["Table"].Rows.Count > 0)
            {

                ddlIdSociedadFi.DataSource = gds_SociedadesFi.Tables["Table"];
                ddlIdSociedadFi.DataTextField = "NomSociedad";
                ddlIdSociedadFi.DataValueField = "IdSociedadFi";
                ddlIdSociedadFi.DataBind();
            }
            else
            {
                ddlIdSociedadFi.DataSource = this.LlenarTablaSociedadesFi();
                ddlIdSociedadFi.DataBind();
            }
        }

        private DataTable LlenarTablaSociedadesFi()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("IdSociedadFi", typeof(string));
            ldt_TablaVacia.Columns.Add("NomSociedad", typeof(string));
            DataRow ldr_FilaTabla = ldt_TablaVacia.NewRow();
            ldt_TablaVacia.Rows.Add(ldr_FilaTabla);
            return ldt_TablaVacia;
        }

        protected void btnCrearPropietario_Click(object sender, EventArgs e)
        {
            String[] str_result = new String[3];
           
            str_result = ws_SGService.uwsCrearPropietario(txtIdPropietario.Text.Trim(), ddlIdSociedadGL.SelectedValue,
                ddlIdSociedadFi.SelectedValue, txtDesPropietario.Text.Trim(), this.cbEstado.Checked ? "A" : "I", gstr_Usuario);


            if (str_result[0].ToString().Equals("00") || str_result[0].ToString().Equals("True"))
            {
                MostarMensaje("La creación de datos ha sido satisfactoria.", lchr_MensajeExito);
            }
            else
            {
                MostarMensaje("La creación de datos no ha sido satisfactoria.", lchr_MensajeError);
            }
        }

    }
}