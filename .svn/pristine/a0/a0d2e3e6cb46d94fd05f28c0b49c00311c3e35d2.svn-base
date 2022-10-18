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
    public partial class frmServicios : BASE
    {
        # region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private char gchr_MensajeError;
        private char gchr_MensajeExito;
        private string gstr_Usuario = String.Empty;
        private string gstr_IdServicio = String.Empty;
        //static DataSet gds_Servicios = new DataSet();
        protected DataSet gds_Servicios
        {
            get
            {
                if (ViewState["gds_Servicios"] == null)
                    ViewState["gds_Servicios"] = new DataSet();
                return (DataSet)ViewState["gds_Servicios"];
            }
            set
            {
                ViewState["gds_Servicios"] = value;
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
                if (!String.IsNullOrEmpty(gstr_Usuario))
                {
                    if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmServicios"))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                    {
                        OcultarMensaje();
                        gstr_IdServicio = clsSesion.Current.IdServicio;
                        ConsultarServicios(gstr_IdServicio, "", "", "", "");
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

        private void ConsultarServicios(string str_IdServicio, string str_IdSociedadGL, string str_NomServicio, string str_IdCuentaContable, string str_IdPosPre)
        {
            gds_Servicios = ws_SGService.uwsConsultarServicios(str_IdServicio, str_IdSociedadGL, "", str_NomServicio, str_IdCuentaContable, str_IdPosPre);

            clsSesion.Current.IdServicio = String.Empty;

            if (gds_Servicios.Tables["Table"].Rows.Count > 0)
            {
                grdServicios.DataSource = gds_Servicios.Tables["Table"];
                grdServicios.DataBind();
            }
            else
            {
                grdServicios.DataSource = this.LlenarTablaVacia();
                grdServicios.DataBind();
                grdServicios.Rows[0].Visible = false;
            }
        }

        private DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("IdServicio", typeof(string));            
            ldt_TablaVacia.Columns.Add("IdSociedadGL", typeof(string));            
            ldt_TablaVacia.Columns.Add("IdOficina", typeof(string));
            ldt_TablaVacia.Columns.Add("NomServicio", typeof(string));            
            ldt_TablaVacia.Columns.Add("Monto", typeof(string));            
            ldt_TablaVacia.Columns.Add("PermiteReserva", typeof(string));            
            ldt_TablaVacia.Columns.Add("CtaContableDebeActualDev", typeof(string));            
            ldt_TablaVacia.Columns.Add("CtaContableHaberActualDev", typeof(string));
            ldt_TablaVacia.Columns.Add("IdPosPreActualDev", typeof(string));
            ldt_TablaVacia.Columns.Add("CtaContableDebeActualPer", typeof(string));
            ldt_TablaVacia.Columns.Add("CtaContableHaberActualPer", typeof(string));
            ldt_TablaVacia.Columns.Add("IdPosPreActualPer", typeof(string));
            ldt_TablaVacia.Columns.Add("CtaContableDebeVencidoDev", typeof(string));
            ldt_TablaVacia.Columns.Add("CtaContableHaberVencidoDev", typeof(string));
            ldt_TablaVacia.Columns.Add("IdPosPreVencidoDev", typeof(string));
            ldt_TablaVacia.Columns.Add("CtaContableDebeVencidoPer", typeof(string));
            ldt_TablaVacia.Columns.Add("CtaContableHaberVencidoPer", typeof(string));
            ldt_TablaVacia.Columns.Add("IdPosPreVencidoPer", typeof(string));
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

        protected void grdServicios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grdServicios.SelectedIndex < 0)
                return;
        }

        protected void grdServicios_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdServicios.EditIndex = e.NewEditIndex;
            grdServicios.DataSource = gds_Servicios.Tables["Table"];

            grdServicios.DataBind();


            //GridViewRow row = (GridViewRow)grdServicios.Rows[e.NewEditIndex];

            //Label txt_Estado = (Label)row.FindControl("lblEstado");
            //CheckBox ckb_Estado = (CheckBox)row.FindControl("ckbEstado");

            //if (txt_Estado.Text.Trim().Equals("A"))
            //    ckb_Estado.Checked = true;

            //ConsultarServicios(txtBusquedaIdServicio.Text.Trim(), txtBusquedaSociedad.Text, txtBusquedaNomServicio.Text, txtCuentaContable.Text, txtPosPre.Text);
        
        }

        protected void grdServicios_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            String[] lstr_result = new String[3];
            try
            {
                //DataRow ldr_ServiciosRow = gds_Servicios.Tables["Table"].NewRow();
                //ldr_ServiciosRow = gds_Servicios.Tables["Table"].Rows[e.RowIndex];

                //string lstr_IdServicio = ldr_ServiciosRow["IdServicio"].ToString();
                //string lstr_IdSociedadGL = ldr_ServiciosRow["IdSociedadGL"].ToString();
                //string lstr_IdOficina = ldr_ServiciosRow["IdOficina"].ToString();
                //string lstr_Estado = ldr_ServiciosRow["Estado"].ToString();
                //DateTime ldt_FchModifica = Convert.ToDateTime(ldr_ServiciosRow["FchModifica"].ToString());

                //string lstr_fecha = String.Empty;
                //lstr_fecha = ldt_FchModifica.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                //ldt_FchModifica = Convert.ToDateTime(lstr_fecha);

                GridViewRow row = (GridViewRow)grdServicios.Rows[e.RowIndex];

                Label lstr_IdServicio = (Label)row.FindControl("lblIdServicio");
                Label lstr_IdSociedadGL = (Label)row.FindControl("lblIdSociedad");
                Label lstr_IdOficina = (Label)row.FindControl("lblIdOficina");
                Label lstr_Estado = (Label)row.FindControl("lblEstado");
                Label ldt_FchModifica = (Label)row.FindControl("lblFechaModifica");


                TextBox txt_NomServicio = (TextBox)row.FindControl("txtEditarNomServicio");
                TextBox txt_Monto = (TextBox)row.FindControl("txtEditarMonto");
                CheckBox cb_PermiteReserva = (CheckBox)row.FindControl("cbEditarReserva");
                TextBox txt_CtaContableDebeActualDev = (TextBox)row.FindControl("txtEditarCtaContableDebeActualDev");
                TextBox txt_CtaContableHaberActualDev = (TextBox)row.FindControl("txtEditarCtaContableHaberActualDev");
                TextBox txt_IdPosPreActualDev = (TextBox)row.FindControl("txtEditarIdPosPreActualDev");
                TextBox txt_CtaContableDebeActualPer = (TextBox)row.FindControl("txtEditarCtaContableDebeActualPer");
                TextBox txt_CtaContableHaberActualPer = (TextBox)row.FindControl("txtEditarCtaContableHaberActualPer");
                TextBox txt_IdPosPreActualPer = (TextBox)row.FindControl("txtEditarIdPosPreActualPer");
                TextBox txt_CtaContableDebeVencidoDev = (TextBox)row.FindControl("txtEditarCtaContableDebeVencidoDev");
                TextBox txt_CtaContableHaberVencidoDev = (TextBox)row.FindControl("txtEditarCtaContableHaberVencidoDev");
                TextBox txt_IdPosPreVencidoDev = (TextBox)row.FindControl("txtEditarIdPosPreVencidoDev");
                TextBox txt_CtaContableDebeVencidoPer = (TextBox)row.FindControl("txtEditarCtaContableDebeVencidoPer");
                TextBox txt_CtaContableHaberVencidoPer = (TextBox)row.FindControl("txtEditarCtaContableHaberVencidoPer");
                TextBox txt_IdPosPreVencidoPer = (TextBox)row.FindControl("txtEditarIdPosPreVencidoPer");
                TextBox txt_Estado = (TextBox)row.FindControl("txtEditarEstado");
                
                //string str_Reserva = String.Empty;
                //string str_Estado = String.Empty;
                //CheckBox ckb_Reserva = (CheckBox)row.FindControl("ckbReserva");
                //CheckBox ckb_Estado = (CheckBox)row.FindControl("ckbEstado");

                //if (ckb_Reserva.Checked)
                //    str_Reserva = "1";
                //if (ckb_Estado.Checked)
                //    str_Estado = "A";


                lstr_result = ws_SGService.uwsModificarServicio(lstr_IdServicio.Text, lstr_IdSociedadGL.Text, lstr_IdOficina.Text, txt_NomServicio.Text, txt_Monto.Text, cb_PermiteReserva.Checked ? "S" : "N",
                    txt_CtaContableDebeActualDev.Text, txt_CtaContableHaberActualDev.Text, txt_IdPosPreActualDev.Text,
                    txt_CtaContableDebeActualPer.Text, txt_CtaContableHaberActualPer.Text, txt_IdPosPreActualPer.Text,
                    txt_CtaContableDebeVencidoDev.Text, txt_CtaContableHaberVencidoPer.Text, txt_IdPosPreVencidoDev.Text,
                    txt_CtaContableDebeVencidoPer.Text, txt_CtaContableHaberVencidoPer.Text, txt_IdPosPreVencidoPer.Text,
                    txt_Estado.Text.Trim(), gstr_Usuario, Convert.ToDateTime(ldt_FchModifica.Text));

                if (lstr_result[0].ToString().Equals("00") || lstr_result[0].ToString().Equals("True"))
                {
                    MostarMensaje("La modificación de datos ha sido satisfactoria.", gchr_MensajeExito);
                }
                else
                {
                    MostarMensaje("La consulta de datos no ha sido satisfactoria.", gchr_MensajeError);
                }
                grdServicios.EditIndex = -1;
                grdServicios.DataSource = gds_Servicios.Tables["Table"];

                grdServicios.DataBind();
                //ConsultarServicios(txtBusquedaIdServicio.Text.Trim(), txtBusquedaSociedad.Text, txtBusquedaNomServicio.Text, txtCuentaContable.Text, txtPosPre.Text);
        
            }
            catch (Exception ex)
            {
                ConsultarServicios(txtBusquedaIdServicio.Text.Trim(), txtBusquedaSociedad.Text, txtBusquedaNomServicio.Text, txtCuentaContable.Text, txtPosPre.Text);
        
                MostarMensaje("Error al finalizar la modificación de datos.", gchr_MensajeError);

            }
        }

        protected void grdServicios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdServicios.PageIndex = e.NewPageIndex;
            grdServicios.DataSource = gds_Servicios.Tables["Table"];

            grdServicios.DataBind();
            //ConsultarServicios(txtBusquedaIdServicio.Text.Trim(), txtBusquedaSociedad.Text, txtBusquedaNomServicio.Text, txtCuentaContable.Text, txtPosPre.Text);
        
        }

        protected void grdServicios_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdServicios.EditIndex = -1;
            grdServicios.DataSource = gds_Servicios.Tables["Table"];

            grdServicios.DataBind();
            //ConsultarServicios(txtBusquedaIdServicio.Text.Trim(), txtBusquedaSociedad.Text, txtBusquedaNomServicio.Text, txtCuentaContable.Text, txtPosPre.Text);
        
        }

        protected void btnConsultarServicio_Click(object sender, EventArgs e)
        {
            OcultarMensaje();
            ConsultarServicios(txtBusquedaIdServicio.Text.Trim(), txtBusquedaSociedad.Text, txtBusquedaNomServicio.Text, txtCuentaContable.Text, txtPosPre.Text);
        
        }

        protected void btnNuevoServicio_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Mantenimiento/Gestiones/frmNuevoServicio.aspx", false);
        }

        protected void btnGuardarServicio_Click(object sender, EventArgs e)
        {

        }

        protected void btnVolverServicios_Click(object sender, EventArgs e)
        {

        }


        protected void grdServicios_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {

        }

        protected void grdServicios_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView HeaderGrid = (GridView)sender;
                GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell HeaderCell = new TableCell();
                HeaderCell.Text = "";
                HeaderCell.ColumnSpan = 5;
                HeaderCell.BackColor = System.Drawing.ColorTranslator.FromHtml("#F7F6F3");
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Periodo Actual";
                HeaderCell.ColumnSpan = 6;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.Cells.Add(HeaderCell);

                HeaderCell = new TableCell();
                HeaderCell.Text = "Periodo Vencido";
                HeaderCell.ColumnSpan = 6;
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderGridRow.Cells.Add(HeaderCell);

                grdServicios.Controls[0].Controls.AddAt(0, HeaderGridRow);

            }
        }

        protected void grdServicios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    CheckBox cbEditarEstado = (CheckBox)e.Row.FindControl("cbEditarReserva");
                    cbEditarEstado.Checked = (e.Row.FindControl("lblReserva") as Label).Text.Trim().Equals("S") ? true : false;
                }

        }
    }
}