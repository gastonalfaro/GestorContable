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
    public partial class frmGiros : BASE
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
                        if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmGirosDE"))
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

        private void ConsultarFormularios(string str_IdPrestamo, string str_IdTramo, string dt_Fecha,string dt_FechaEstimada)
        {

            gds_Formularios = wsDE.ConsultarGiro(str_IdPrestamo, str_IdTramo, "", "", "", dt_Fecha, dt_Fecha,dt_FechaEstimada,dt_FechaEstimada, "", "", "");

            //                gds_Formularios.Tables["Table"].Columns["Monto"].DataType = typeof(Decimal);

            if (gds_Formularios.Tables["Table"].Rows.Count > 0)
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
            ldt_TablaVacia.Columns.Add("IdTramo", typeof(string));
            ldt_TablaVacia.Columns.Add("FchEstimada", typeof(string));
            ldt_TablaVacia.Columns.Add("FchDesembolso", typeof(string));
            ldt_TablaVacia.Columns.Add("Monto", typeof(string));
            ldt_TablaVacia.Columns.Add("Moneda", typeof(string));
            ldt_TablaVacia.Columns.Add("Secuencia", typeof(string));
            ldt_TablaVacia.Columns.Add("TipoDesembolso", typeof(string));
            ldt_TablaVacia.Columns.Add("Descripcion", typeof(string));
            ldt_TablaVacia.Columns.Add("Estado", typeof(char));
           
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
            ConsultarFormularios(txtBusqIdPrestamo.Text, txtBusqIdTramo.Text, txtFecha.Text, txtFechaEstimada.Text);
        }

        protected void grdvFormularios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdvFormularios.PageIndex = e.NewPageIndex;
            grdvFormularios.DataSource = gds_Formularios.Tables["Table"];

            grdvFormularios.DataBind();
            //this.ConsultarFormularios(txtBusqIdPrestamo.Text, txtBusqIdTramo.Text, txtFecha.Text, txtFechaEstimada.Text); 
        }



    }

}