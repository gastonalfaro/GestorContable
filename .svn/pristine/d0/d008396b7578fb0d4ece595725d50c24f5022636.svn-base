using Presentacion.Compartidas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Mantenimiento
{
    public partial class frmInstituciones : BASE
    {
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private char gchr_MensajeError;
        private char gchr_MensajeExito;
        private string gstr_usuario = String.Empty;
        //private DataSet gds_Instituciones = new DataSet();
        protected DataSet gds_Instituciones
        {
            get
            {
                if (ViewState["gds_Instituciones"] == null)
                    ViewState["gds_Instituciones"] = new DataSet();
                return (DataSet)ViewState["gds_Instituciones"];
            }
            set
            {
                ViewState["gds_Instituciones"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            gstr_usuario = clsSesion.Current.LoginUsuario;
            gchr_MensajeError = clsSesion.Current.chr_MensajeError;
            gchr_MensajeExito = clsSesion.Current.chr_MensajeExito;
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(gstr_usuario))
                {
                    MostrarElementos(gstr_usuario);
                    OcultarMensaje();
                    ConsultarInstituciones( "","","","","" );
                }
                else
                {
                    Response.Redirect("~/Login.aspx", true);
                }

            }
            else
            {
                if (string.IsNullOrEmpty(gstr_usuario))
                    Response.Redirect("~/Login.aspx", true);
            }
        }


        private void MostrarElementos(string str_usuario)
        {
            DataSet ldt_PermisosUsuario = ws_SGService.uwsConsultarPermisosUsuarios(str_usuario, "");

            for (int i = 0; ldt_PermisosUsuario.Tables["Table"].Rows.Count > i; i++)
            {
                string lstr_IdObjeto = ldt_PermisosUsuario.Tables["Table"].Rows[i]["IdObjeto"].ToString();

                grdvInstituciones.Columns[0].Visible = false;

                if ((bool)ldt_PermisosUsuario.Tables["Table"].Rows[i]["Actualizar"])
                {
                    grdvInstituciones.Columns[0].Visible = true;
                }
            }
        }

        private void ConsultarInstituciones(string str_IdSociedadGL, string str_Denominacion, string str_NbrSociedad, string str_IdPais, string str_Moneda)
        {
            gds_Instituciones = ws_SGService.uwsConsultarSociedadesGL(str_IdSociedadGL, str_Denominacion, str_NbrSociedad, str_IdPais, str_Moneda);

            if (gds_Instituciones.Tables["Table"].Rows.Count > 0)
            {
                grdvInstituciones.DataSource = gds_Instituciones.Tables["Table"];
                grdvInstituciones.DataBind();
            }
            else
            {
                grdvInstituciones.DataSource = this.LlenarTablaVacia();
                grdvInstituciones.DataBind();
                grdvInstituciones.Rows[0].Visible = false;
            }
        }

        private DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("IdSociedadGL", typeof(string));
            ldt_TablaVacia.Columns.Add("Denominacion", typeof(string));
            ldt_TablaVacia.Columns.Add("NbrSociedad", typeof(string));
            ldt_TablaVacia.Columns.Add("IdPais", typeof(string));
            ldt_TablaVacia.Columns.Add("IdMoneda", typeof(string));
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


        protected void btnInstitucionesoNuevo_Click(object sender, EventArgs e)
        {

        }

        protected void btnInstitucionesGuardar_Click(object sender, EventArgs e)
        {

        }

        protected void btnInstitucionesVolver_Click(object sender, EventArgs e)
        {

        }

        protected void btnInstitucionesConsultar_Click(object sender, EventArgs e)
        {
            OcultarMensaje();
            ConsultarInstituciones(txbBusqIdSociedadGL.Text, txbBusqDenominacion.Text, "", "", "");
        }

        protected void grdvInstituciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grdvInstituciones.SelectedIndex < 0)
                return;
        }

        protected void grdvInstituciones_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdvInstituciones.EditIndex = e.NewEditIndex;
            grdvInstituciones.DataSource = gds_Instituciones.Tables["Table"];

            grdvInstituciones.DataBind();
            //ConsultarInstituciones(txbBusqIdSociedadGL.Text, txbBusqDenominacion.Text, "", "", "");
        }

        protected void grdvInstituciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdvInstituciones.PageIndex = e.NewPageIndex;
            grdvInstituciones.DataSource = gds_Instituciones.Tables["Table"];

            grdvInstituciones.DataBind();
            //ConsultarInstituciones(txbBusqIdSociedadGL.Text, txbBusqDenominacion.Text, "", "", "");
        }


        protected void grdvInstituciones_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdvInstituciones.PageIndex = -1;
            grdvInstituciones.DataSource = gds_Instituciones.Tables["Table"];

            grdvInstituciones.DataBind();
            //ConsultarInstituciones(txbBusqIdSociedadGL.Text, txbBusqDenominacion.Text, "", "", "");
        }
    }
}