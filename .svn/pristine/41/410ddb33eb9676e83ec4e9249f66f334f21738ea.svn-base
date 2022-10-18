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
    public partial class frmUnidadesConsolidacion : BASE
    {

        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private char cchr_MensajeError = '1';
        private char cchr_MensajeExito = '0';
        protected DataSet gds_UnidadesConsolidacion
        {
            get
            {
                if (ViewState["gds_UnidadesConsolidacion"] == null)
                    ViewState["gds_UnidadesConsolidacion"] = new DataSet();
                return (DataSet)ViewState["gds_UnidadesConsolidacion"];
            }
            set
            {
                ViewState["gds_UnidadesConsolidacion"] = value;
            }
        }
        private void OcultarMensaje()
        {
            this.lblMensaje.Text = String.Empty;
            this.lblMensaje.Visible = false;
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

        private DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("Vista", typeof(string));
            ldt_TablaVacia.Columns.Add("IdUnidadConsolidacion", typeof(string));
            ldt_TablaVacia.Columns.Add("NomCorto", typeof(string));
            ldt_TablaVacia.Columns.Add("NomUnidad", typeof(string));
            DataRow ldr_FilaTabla = ldt_TablaVacia.NewRow();
            ldt_TablaVacia.Rows.Add(ldr_FilaTabla);
            return ldt_TablaVacia;
        }


        private void ConsultarUnidadesConsolidacion(string str_Vista, string str_IdUnidadConsolidacion, string str_NomCorto, string str_NomUnidad)
        {
            gds_UnidadesConsolidacion = ws_SGService.uwsConsultarUnidadesConsolidacion(str_Vista, str_IdUnidadConsolidacion, str_NomCorto, str_NomUnidad);

            if (gds_UnidadesConsolidacion.Tables["Table"].Rows.Count > 0)
            {
                gvpUnidadesConsolidacion.DataSource = gds_UnidadesConsolidacion.Tables["Table"];
                gvpUnidadesConsolidacion.DataBind();
            }
            else
            {
                gvpUnidadesConsolidacion.DataSource = this.LlenarTablaVacia();
                gvpUnidadesConsolidacion.DataBind();
                gvpUnidadesConsolidacion.Rows[0].Visible = false;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                OcultarMensaje();
                ConsultarUnidadesConsolidacion("", "","","");
            }
        }

        protected void btnUnidadesConsolidacionVolver_Click(object sender, EventArgs e)
        {

        }

        protected void gvpUnidadesConsolidacion_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvpUnidadesConsolidacion_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gvpUnidadesConsolidacion_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void gvpUnidadesConsolidacion_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {

        }

        protected void gvpUnidadesConsolidacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvpUnidadesConsolidacion_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }
    }
}