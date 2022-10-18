using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using Presentacion.Compartidas;
using System.Web.UI.HtmlControls;

namespace Presentacion.Mantenimiento
{
    public partial class frmCentrosGestores : BASE
    {
        # region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private char gchr_MensajeError;
        private char gchr_MensajeExito;
        private string gstr_Usuario = String.Empty;
        //static DataSet gds_CentrosGestores = new DataSet();
        protected DataSet gds_CentrosGestores
        {
            get
            {
                if (ViewState["gds_CentrosGestores"] == null)
                    ViewState["gds_CentrosGestores"] = new DataSet();
                return (DataSet)ViewState["gds_CentrosGestores"];
            }
            set
            {
                ViewState["gds_CentrosGestores"] = value;
            }
        }
        DateTime gdt_FechaConsulta  = new DateTime(1900, 1, 1);
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
                    gdt_FechaConsulta = new DateTime(1900, 1, 1);
                    if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, ""))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                    {
                        OcultarMensaje();
                        ConsultarCentrosGestores("", gdt_FechaConsulta, "", "", DateTime.Today, "", "");
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
        
        private void ConsultarCentrosGestores(string str_IdCentroGestor, DateTime dt_FchVigenciaHasta,
            string str_IdEntidadCP, string str_IdSociedadFi, DateTime dt_FchConsulta, string str_Denominacion, string str_NomCentroGestor)
        {
            gds_CentrosGestores = ws_SGService.uwsConsultarCentrosGestores(str_IdCentroGestor, dt_FchVigenciaHasta,
                str_IdEntidadCP, str_IdSociedadFi, dt_FchConsulta, str_Denominacion, str_NomCentroGestor);

            if (gds_CentrosGestores.Tables["Table"].Rows.Count > 0)
            {
                grdCentrosGestores.DataSource = gds_CentrosGestores.Tables["Table"];
                grdCentrosGestores.DataBind();
            }
            else
            {
                grdCentrosGestores.DataSource = this.LlenarTablaVacia();
                grdCentrosGestores.DataBind();
            }
        }

        private DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("IdCentroGestor", typeof(string));
            ldt_TablaVacia.Columns.Add("FchVigencia", typeof(string));
            ldt_TablaVacia.Columns.Add("FchVigenciaHasta", typeof(string));
            ldt_TablaVacia.Columns.Add("IdEntidadCP", typeof(string));
            ldt_TablaVacia.Columns.Add("IdSociedadFi", typeof(string));
            ldt_TablaVacia.Columns.Add("Denominacion", typeof(string));
            ldt_TablaVacia.Columns.Add("NomCentroGestor", typeof(string));
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

        protected void btnCentroGestorNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Mantenimiento/Gestiones/frmNuevoCentroGestor.aspx", false);
        }

        protected void btnCentroGestorGuardar_Click(object sender, EventArgs e)
        {

        }

        protected void btnCentroGestorVolver_Click(object sender, EventArgs e)
        {

        }

        protected void btnCentroGestorConsultar_Click(object sender, EventArgs e)
        {
            OcultarMensaje();
           
            if (!txtFchVigenciaHasta.Text.Equals(""))
                gdt_FechaConsulta = Convert.ToDateTime(txtFchVigenciaHasta.Text);
            
            else gdt_FechaConsulta = new DateTime(1900, 1, 1);
            
            ConsultarCentrosGestores(txtBusqIdCentroGestor.Text, gdt_FechaConsulta, "", "", DateTime.Today, "", txtBusqNomCentroGestor.Text);
            
        }

        protected void grdCentrosGestores_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grdCentrosGestores.SelectedIndex < 0)
                return;
        }

        protected void grdCentrosGestores_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdCentrosGestores.EditIndex = e.NewEditIndex;
            grdCentrosGestores.DataSource = gds_CentrosGestores.Tables["Table"];

            grdCentrosGestores.DataBind();
            //ConsultarCentrosGestores("", gdt_FechaConsulta, "", "", DateTime.Today, "", "");
           
        }

        protected void grdCentrosGestores_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void grdCentrosGestores_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCentrosGestores.PageIndex = e.NewPageIndex;
            grdCentrosGestores.DataSource = gds_CentrosGestores.Tables["Table"];

            grdCentrosGestores.DataBind();
            //ConsultarCentrosGestores("", gdt_FechaConsulta, "", "", DateTime.Today, "", "");
        }

        protected void grdCentrosGestores_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdCentrosGestores.EditIndex = -1;
            grdCentrosGestores.DataSource = gds_CentrosGestores.Tables["Table"];

            grdCentrosGestores.DataBind();
            //ConsultarCentrosGestores("", gdt_FechaConsulta, "", "", DateTime.Today, "", "");
        }


    }
}