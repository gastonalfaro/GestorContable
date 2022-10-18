using Presentacion.Compartidas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Presentacion.CalculosFinancieros.DeudaExterna
{
    public partial class frmPrestamos : BASE
    {
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private wsDeudaExterna.wsDeudaExterna wsDE = new wsDeudaExterna.wsDeudaExterna();

        private char gchr_MensajeError;
        private char gchr_MensajeExito;
        private string gstr_Usuario = String.Empty;
        //private DataSet gds_Formularios = new DataSet();
        protected DataSet gds_Formularios
        {
            get
            {
                if (ViewState["gds_Formularios"] == null)
                    ViewState["gds_Formularios"] = new DataSet();
                return (DataSet)ViewState["gds_Formularios"];
            }
            set
            {
                ViewState["gds_Formularios"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                gstr_Usuario = clsSesion.Current.LoginUsuario;

                if (!IsPostBack)
                {
                    if (!string.IsNullOrEmpty(gstr_Usuario))
                    {
                        if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmPrestamosDE"))
                        {
                            Response.Redirect("~/Principal.aspx", true);
                        }
                    }
                    else
                        Response.Redirect("~/Login.aspx", true);
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Login.aspx", true);
            }
        }

        private void ConsultarFormularios(string str_IdPrestamo, string dt_FechaInicio, string dt_FechaFin, string str_Fuente,
            string str_Situacion, string str_Plazo, string str_Nombre, string str_NbrAcreedor, string str_CatAcreedor,
            string str_TpoAcreedor, string str_NbrDeudor, string str_CatDeudor, string str_TipoPrestamo)
        {

            gds_Formularios = wsDE.ConsultarPrestamo(str_IdPrestamo, dt_FechaInicio, dt_FechaFin,  str_Fuente, str_Situacion, str_Plazo, str_Nombre, str_NbrAcreedor,
                str_CatAcreedor, str_TpoAcreedor, str_NbrDeudor, str_CatDeudor, str_TipoPrestamo);

            //                gds_Formularios.Tables["Table"].Columns["Monto"].DataType = typeof(Decimal);

            if (gds_Formularios.Tables.Count > 0 && gds_Formularios.Tables["Table"].Rows.Count > 0)
            {
                grdvFormularios.DataSource = gds_Formularios.Tables["Table"];
                grdvFormularios.DataBind();
            }
            else
            {
                grdvFormularios.DataSource = this.LlenarTablaVacia();
                grdvFormularios.DataBind();
                grdvFormularios.Rows[0].Visible = false;
            }
        }

        private DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("IdPrestamo", typeof(string));
            ldt_TablaVacia.Columns.Add("Fuente", typeof(string));
            ldt_TablaVacia.Columns.Add("Situacion", typeof(string));
            ldt_TablaVacia.Columns.Add("Plazo", typeof(string));
            ldt_TablaVacia.Columns.Add("Nombre", typeof(string));
            ldt_TablaVacia.Columns.Add("FchFirmado", typeof(string));
            ldt_TablaVacia.Columns.Add("LimiteGiro", typeof(string));
            ldt_TablaVacia.Columns.Add("LimiteEfectivo", typeof(string));
            ldt_TablaVacia.Columns.Add("Efectivo", typeof(string));
            ldt_TablaVacia.Columns.Add("Monto", typeof(string));
            ldt_TablaVacia.Columns.Add("IdMoneda", typeof(string));
            ldt_TablaVacia.Columns.Add("TipoTramo", typeof(string));
            ldt_TablaVacia.Columns.Add("Proposito", typeof(string));
            ldt_TablaVacia.Columns.Add("GarantiaPublica", typeof(string));
            ldt_TablaVacia.Columns.Add("OrigenDeuda", typeof(string));
            ldt_TablaVacia.Columns.Add("NbrAcreedor", typeof(string));
            ldt_TablaVacia.Columns.Add("CatAcreedor", typeof(string));
            ldt_TablaVacia.Columns.Add("TipoAcreedor", typeof(string));
            ldt_TablaVacia.Columns.Add("NbrDeudor", typeof(string));
            ldt_TablaVacia.Columns.Add("CatDeudor", typeof(string));
            ldt_TablaVacia.Columns.Add("TipoPrestamo", typeof(string));
            ldt_TablaVacia.Columns.Add("Estado", typeof(string));
            ldt_TablaVacia.Columns.Add("CondicionPrestamo", typeof(string));
            ldt_TablaVacia.Columns.Add("ExisteObligacion", typeof(string));
            ldt_TablaVacia.Columns.Add("CondicionMotivo", typeof(string));
            ldt_TablaVacia.Columns.Add("CondicionTasa", typeof(string));
            ldt_TablaVacia.Columns.Add("CondicionMonto", typeof(string));
            ldt_TablaVacia.Columns.Add("CondicionFchInicio", typeof(string));
            ldt_TablaVacia.Columns.Add("CondicionFchFin", typeof(string));
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

        protected void btnFormulariosConsultar_Click(object sender, EventArgs e)
        {
            OcultarMensaje();
            ConsultarFormularios(txtBusqIdPrestamo.Text,txtFechaInicio.Text, txtFechaFin.Text, txtFuente.Text, txtSituacion.Text, txtPlazo.Text,txtNombre.Text,txtNombreAcreedor.Text, txtCatAcreedor.Text, txtTipoAcreedor.Text, txtNombreAcreedor.Text, txtCatDuedor.Text, txtTipoPrestamo.Text);
        }

        protected void grdvFormularios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdvFormularios.PageIndex = e.NewPageIndex;
            grdvFormularios.DataSource = gds_Formularios.Tables["Table"];

            grdvFormularios.DataBind();
            //this.ConsultarFormularios(txtBusqIdPrestamo.Text, txtFechaInicio.Text, txtFechaFin.Text, txtFuente.Text, txtSituacion.Text, txtPlazo.Text, txtNombre.Text, txtNombreAcreedor.Text, txtCatAcreedor.Text, txtTipoAcreedor.Text, txtNombreAcreedor.Text, txtCatDuedor.Text, txtTipoPrestamo.Text);
        }

    }
}