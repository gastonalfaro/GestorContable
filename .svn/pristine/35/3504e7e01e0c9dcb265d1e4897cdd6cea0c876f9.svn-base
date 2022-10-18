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
    public partial class frmReservaDetalle : BASE
    {
        # region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();

        private char gchr_MensajeError;
        private char gchr_MensajeExito;

        private string gstr_Usuario = String.Empty;
        private string gstr_IdReserva = String.Empty;

        private String[] garr_Modulos;
        private String[] garr_Modulo_Unico;

        //static DataSet gds_DetalleReservas = new DataSet();
        protected DataSet gds_DetalleReservas
        {
            get
            {
                if (ViewState["gds_DetalleReservas"] == null)
                    ViewState["gds_DetalleReservas"] = new DataSet();
                return (DataSet)ViewState["gds_DetalleReservas"];
            }
            set
            {
                ViewState["gds_DetalleReservas"] = value;
            }
        }

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
                    garr_Modulos = clsSesion.Current.PermisosModulos;

                    if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmReservas"))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                    {
                        gstr_IdReserva = clsSesion.Current.IdReserva;
                        MostrarData();
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

        protected void btnVolverReservas_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Mantenimiento/frmReservas.aspx", false);
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

        private void MostrarData()
        {
            ConsultarDetalle(gstr_IdReserva);

            if (gds_DetalleReservas.Tables["Table"].Rows.Count > 0)
            {
                txtNomReserva.Text = gds_DetalleReservas.Tables["Table"].Rows[0]["Detalle"].ToString();
                txtIdReserva.Text = gstr_IdReserva;
            }
        }

        private void ConsultarDetalle(string str_IdReserva )
        {
            string lstr_modulo = String.Empty;

            gds_DetalleReservas = ws_SGService.uwsConsultarReservaDetalle(str_IdReserva);
            if (gds_DetalleReservas.Tables["Table"].Rows.Count > 0)
            {
                grdDetalles.DataSource = gds_DetalleReservas.Tables["Table"];
                grdDetalles.DataBind();
            }
            else
            {
                grdDetalles.DataSource = this.LlenarTablaDetalle();
                grdDetalles.DataBind();
            }
        }

        private DataTable LlenarTablaDetalle()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("Posicion", typeof(string));
            ldt_TablaVacia.Columns.Add("Detalle", typeof(string));
            ldt_TablaVacia.Columns.Add("IdPosPre", typeof(string));
            ldt_TablaVacia.Columns.Add("IdCentroGestor", typeof(string));
            ldt_TablaVacia.Columns.Add("IdFondo", typeof(string));
            ldt_TablaVacia.Columns.Add("Segmento", typeof(string));
            ldt_TablaVacia.Columns.Add("IdPrograma", typeof(string));
            ldt_TablaVacia.Columns.Add("IdCuentaContable", typeof(string));
            ldt_TablaVacia.Columns.Add("IdCentroCosto", typeof(string));
            ldt_TablaVacia.Columns.Add("IdElementoPEP", typeof(string));
            ldt_TablaVacia.Columns.Add("IdMoneda", typeof(string));
            ldt_TablaVacia.Columns.Add("Monto", typeof(string));
            ldt_TablaVacia.Columns.Add("Bloqueado", typeof(string));
            ldt_TablaVacia.Columns.Add("Estado", typeof(string));

            DataRow ldr_FilaTabla = ldt_TablaVacia.NewRow();
            ldt_TablaVacia.Rows.Add(ldr_FilaTabla);
            return ldt_TablaVacia;
        }


        protected void grdDetalles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdDetalles.PageIndex = e.NewPageIndex;
            grdDetalles.DataSource = gds_DetalleReservas.Tables["Table"];

            grdDetalles.DataBind();
            //ConsultarDetalle(gstr_IdReserva);
        }


    }
}