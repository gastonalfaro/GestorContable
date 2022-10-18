using eWorld.UI;
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

namespace Presentacion.Mantenimiento
{
    public partial class frmReservas : BASE
    {
        #region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();

        private Boolean gbool_ver;
        private char gchr_MensajeError;
        private char gchr_MensajeExito;
        private string gstr_usuario = String.Empty;
        private string gstr_Modulos = String.Empty;

        //private static DataSet gds_Reservas = new DataSet();
        protected DataSet gds_Reservas
        {
            get
            {
                if (ViewState["gds_Reservas"] == null)
                    ViewState["gds_Reservas"] = new DataSet();
                return (DataSet)ViewState["gds_Reservas"];
            }
            set
            {
                ViewState["gds_Reservas"] = value;
            }
        }
        DateTime gdt_FechaConsulta;
        private string gstr_Usuario = String.Empty;
        private String[] larr_Modulos;

        DataSet ds = new DataSet();
        public SortDirection dir
        {
            get
            {
                if (ViewState["dirState"] == null)
                {
                    ViewState["dirState"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["dirState"];
            }
            set
            {
                ViewState["dirState"] = value;
            }

        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            gstr_Usuario = clsSesion.Current.LoginUsuario;
            gchr_MensajeError = clsSesion.Current.chr_MensajeError;
            gchr_MensajeExito = clsSesion.Current.chr_MensajeExito;

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(gstr_Usuario))
                {
                    larr_Modulos = clsSesion.Current.PermisosModulos;

                    if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmReservas"))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                    {
                        gdt_FechaConsulta = new DateTime(1900, 1, 1);
                        OcultarMensaje();
                        ConsultarReservas(string.Empty, string.Empty, string.Empty, string.Empty
                                , string.Empty, string.Empty, string.Empty, string.Empty
                                , string.Empty, string.Empty, string.Empty);
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

        private void ConsultarReservas(string str_IdReserva, string str_IdEntidadCP, string str_IdSociedadFi, string str_IdMoneda, string str_NomReserva,
            string str_IdCentroGestor = null, string str_IdCentroCosto = null, string str_IdCuentaContable = null, string str_IdElementoPEP = null,
            string str_IdPosPre = null, string str_OrderBy = null)
        {
            string lstr_modulo = String.Empty;


            gds_Reservas = ws_SGService.uwsConsultarReservaDetallado(str_IdReserva, str_IdEntidadCP, str_IdSociedadFi, str_IdMoneda, 
                str_NomReserva,str_IdCentroGestor, str_IdCentroCosto, str_IdCuentaContable, 
                str_IdElementoPEP,str_IdPosPre, "N","N","N",str_OrderBy);

            if (gds_Reservas.Tables["Table"].Rows.Count > 0)
            {
                grdReservas.DataSource = gds_Reservas.Tables["Table"];
                grdReservas.DataBind();
            }
            else
            {
                grdReservas.DataSource = this.LlenarTablaVacia();
                grdReservas.DataBind();
            }
        }

        private DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("IdReserva", typeof(string));
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
            ldt_TablaVacia.Columns.Add("Estado", typeof(string));
            ldt_TablaVacia.Columns.Add("Bloqueado", typeof(string));
            ldt_TablaVacia.Columns.Add("OrdenContingentes", typeof(string));
            ldt_TablaVacia.Columns.Add("OrdenDeudaInterna", typeof(string));
            ldt_TablaVacia.Columns.Add("OrdenDeudaExterna", typeof(string));
            ldt_TablaVacia.Columns.Add("IdMoneda", typeof(string));
            ldt_TablaVacia.Columns.Add("Monto", typeof(string));
            ldt_TablaVacia.Columns.Add("FchModifica", typeof(string));
            ldt_TablaVacia.Columns.Add("NomReserva", typeof(string));
                       
            DataRow ldr_FilaTabla = ldt_TablaVacia.NewRow();
            ldt_TablaVacia.Rows.Add(ldr_FilaTabla);
            return ldt_TablaVacia;
        }

        protected void btnNuevaReserva_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Mantenimiento/Gestiones/frmNuevaReserva.aspx", false);
        }

        protected void btnConsultarReserva_Click(object sender, EventArgs e)
        {   
            OcultarMensaje();
            ConsultarReservas(this.txtBusqIdReserva.Text, string.Empty, string.Empty, this.txtBusqIdMoneda.Text
                                , this.txtBusqNomReserva.Text, string.Empty, this.txtBusqIdCentroCosto.Text, string.Empty
                                , this.txtBusqIdElementoPEP.Text, this.txtBusqIdPosPre.Text, string.Empty);
         }

        protected void grdReservas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grdReservas.SelectedIndex < 0)
                return;
            clsSesion.Current.IdReserva = grdReservas.SelectedIndex.ToString();
            Response.Redirect("~/Mantenimiento/frmReservaDetalle.aspx", false);
        }

        protected void grdReservas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdReservas.EditIndex = e.NewEditIndex;
            grdReservas.DataSource = gds_Reservas.Tables["Table"];

            grdReservas.DataBind();
            //ConsultarReservas(this.txtBusqIdReserva.Text, string.Empty, string.Empty, this.txtBusqIdMoneda.Text
            //                    , this.txtBusqNomReserva.Text, string.Empty, this.txtBusqIdCentroCosto.Text, string.Empty
            //                    , this.txtBusqIdElementoPEP.Text, this.txtBusqIdPosPre.Text, string.Empty);

            DataRow ldr_ReservasRow = gds_Reservas.Tables["Table"].NewRow();
            ldr_ReservasRow = gds_Reservas.Tables["Table"].Rows[e.NewEditIndex];

            string lint_IdReserva = ldr_ReservasRow["IdReserva"].ToString();
            //string lstr_NomCatalogo = ldr_ReservasRow["Nombre"].ToString();

            clsSesion.Current.IdReserva = lint_IdReserva;

            //grdReservas.EditIndex = -1;
            if (gbool_ver)
            {
                Response.Redirect("~/Mantenimiento/frmReservaDetalle.aspx", false);
            }
        }

        protected void grdReservas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdReservas.PageIndex = e.NewPageIndex;
            grdReservas.DataSource = gds_Reservas.Tables["Table"];

            grdReservas.DataBind();
             //ConsultarReservas(this.txtBusqIdReserva.Text, string.Empty, string.Empty, this.txtBusqIdMoneda.Text
             //                   , this.txtBusqNomReserva.Text, string.Empty, this.txtBusqIdCentroCosto.Text, string.Empty
             //                   , this.txtBusqIdElementoPEP.Text, this.txtBusqIdPosPre.Text, string.Empty);
        }

        protected void grdReservas_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            gds_Reservas = ws_SGService.uwsConsultarReservaDetalle(this.txtBusqIdReserva.Text);
        }

        protected void grdReservas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            String[] lstr_result = new String[3];
            try
            {
                //DataRow ldr_ReservasRow = gds_Reservas.Tables["Table"].NewRow();
                //ldr_ReservasRow = gds_Reservas.Tables["Table"].Rows[e.RowIndex];

                //string lstr_IdReservas = ldr_ReservasRow["IdReserva"].ToString();
                //DateTime ldt_FchModifica = Convert.ToDateTime(ldr_ReservasRow["FchModifica"].ToString());

                //string lstr_fecha = String.Empty;
                //lstr_fecha = ldt_FchModifica.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                //ldt_FchModifica = Convert.ToDateTime(lstr_fecha);

                GridViewRow lgrdvr_Reservas = (GridViewRow)grdReservas.Rows[e.RowIndex];

                Label lstr_IdReservas = (Label)lgrdvr_Reservas.FindControl("lblIdReserva");
                Label ldt_FchModifica = (Label)lgrdvr_Reservas.FindControl("lblFchModifica");
                Label ldt_Posicion = (Label)lgrdvr_Reservas.FindControl("lblPosicion");

                
                TextBox txt_OrdenContingentes = (TextBox)lgrdvr_Reservas.FindControl("txtEditarOrdenContingentes");
                TextBox txt_OrdenDeudaInterna = (TextBox)lgrdvr_Reservas.FindControl("txtEditarOrdenDeudaInterna");
                TextBox txt_OrdenDeudaExterna = (TextBox)lgrdvr_Reservas.FindControl("txtEditarOrdenDeudaExterna");

                int lint_OrdenContingentes = 0;
                int lint_OrdenDeudaInterna = 0;
                int lint_OrdenDeudaExterna = 0;

                if (!string.IsNullOrEmpty(txt_OrdenContingentes.Text))
                    lint_OrdenContingentes = Convert.ToInt32(txt_OrdenContingentes.Text);

                if (!string.IsNullOrEmpty(txt_OrdenDeudaInterna.Text))
                    lint_OrdenDeudaInterna = Convert.ToInt32(txt_OrdenDeudaInterna.Text);

                if (!string.IsNullOrEmpty(txt_OrdenDeudaExterna.Text))
                    lint_OrdenDeudaExterna = Convert.ToInt32(txt_OrdenDeudaExterna.Text);


                lstr_result = ws_SGService.uwsModificarReservaDetalle(lstr_IdReservas.Text, ldt_Posicion.Text,
                    lint_OrdenContingentes, lint_OrdenDeudaInterna, lint_OrdenDeudaExterna, 
                    gstr_usuario, Convert.ToDateTime(ldt_FchModifica.Text));

                 if (lstr_result[0].ToString().Equals("00") || lstr_result[0].ToString().Equals("True"))
                {
                    MostarMensaje("Se actualizó correctamente la reserva.", gchr_MensajeExito);
                    //MostarMensaje(lstr_result[1].ToString(), gchr_MensajeExito);
                }
                else
                {
                    MostarMensaje("Error al modificar la reserva, verifique los datos ingresados.", gchr_MensajeError);
                    //MostarMensaje("Error: " + lstr_result[1].ToString(), gchr_MensajeError);
                }
                 grdReservas.EditIndex = -1;
                 grdReservas.DataSource = gds_Reservas.Tables["Table"];

                 //grdReservas.DataBind();
                 //gdt_FechaConsulta = new DateTime(1900, 1, 1);
                 ConsultarReservas(this.txtBusqIdReserva.Text, string.Empty, string.Empty, this.txtBusqIdMoneda.Text
                                 , this.txtBusqNomReserva.Text, string.Empty, this.txtBusqIdCentroCosto.Text, string.Empty
                                 , this.txtBusqIdElementoPEP.Text, this.txtBusqIdPosPre.Text, string.Empty);
            }
            catch (Exception ex)
            {
                ConsultarReservas(this.txtBusqIdReserva.Text, string.Empty, string.Empty, this.txtBusqIdMoneda.Text
                                , this.txtBusqNomReserva.Text, string.Empty, this.txtBusqIdCentroCosto.Text, string.Empty
                                , this.txtBusqIdElementoPEP.Text, this.txtBusqIdPosPre.Text, string.Empty);
                MostarMensaje("Error al modificar el parámetro.", gchr_MensajeError);

            }
        }

        protected void grdReservas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdReservas.EditIndex = -1;
            grdReservas.DataSource = gds_Reservas.Tables["Table"];

            grdReservas.DataBind();
            //ConsultarReservas(this.txtBusqIdReserva.Text, string.Empty, string.Empty, this.txtBusqIdMoneda.Text
            //                    , this.txtBusqNomReserva.Text, string.Empty, this.txtBusqIdCentroCosto.Text, string.Empty
            //                    , this.txtBusqIdElementoPEP.Text, this.txtBusqIdPosPre.Text, string.Empty);
        }

        protected void grdReservas_Sorting(object sender, GridViewSortEventArgs e)
        {
            //ConsultarReservas(this.txtBusqIdReserva.Text, string.Empty, string.Empty, this.txtBusqIdMoneda.Text
            //                    , this.txtBusqNomReserva.Text, string.Empty, this.txtBusqIdCentroCosto.Text, string.Empty
            //                    , this.txtBusqIdElementoPEP.Text, this.txtBusqIdPosPre.Text, string.Empty);
            DataTable dt = new DataTable();
            dt = gds_Reservas.Tables[0];
            {
                string SortDir = string.Empty;
                if (dir == SortDirection.Ascending)
                {
                    dir = SortDirection.Descending;
                    SortDir = "Desc";
                }
                else
                {
                    dir = SortDirection.Ascending;
                    SortDir = "Asc";
                }
                DataView sortedView = new DataView(dt);
                sortedView.Sort = e.SortExpression + " " + SortDir;
                grdReservas.DataSource = sortedView;
                grdReservas.DataBind();
            }
      }


        protected void lbtEditarReserva_Click(object sender, EventArgs e)
        {
            gbool_ver = false;
        }

        protected void lbtIraReservaDetalle_Click(object sender, EventArgs e)
        {
            gbool_ver = true;
        }
    }
}