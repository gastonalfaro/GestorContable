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
    public partial class frmIndicadoresEconomicos : BASE
    {
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private Presentacion.wsDeudaExterna.wsDeudaExterna wsDE = new Presentacion.wsDeudaExterna.wsDeudaExterna();
        private char gchr_MensajeError;
        private char gchr_MensajeExito;
        private string gstr_Usuario = String.Empty;

        //static DataSet gds_IndicadoresEconomicos = new DataSet();
        protected DataSet gds_IndicadoresEconomicos
        {
            get
            {
                if (ViewState["gds_IndicadoresEconomicos"] == null)
                    ViewState["gds_IndicadoresEconomicos"] = new DataSet();
                return (DataSet)ViewState["gds_IndicadoresEconomicos"];
            }
            set
            {
                ViewState["gds_IndicadoresEconomicos"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            gstr_Usuario = clsSesion.Current.LoginUsuario;
            gchr_MensajeError = clsSesion.Current.chr_MensajeError;
            gchr_MensajeExito = clsSesion.Current.chr_MensajeExito;
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(gstr_Usuario))
                {
                    if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, ""))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                    {
                        OcultarMensaje();
                        ConsultarIndicadoresEconomicos("", "", "");
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

        private void ConsultarIndicadoresEconomicos(string str_IdIndicadorEconomico, string str_Transaccion, string str_NomIndicadorEconomico)
        {
            gds_IndicadoresEconomicos = ws_SGService.uwsConsultarIndicadoresEconomicos(str_IdIndicadorEconomico, str_Transaccion, str_NomIndicadorEconomico);


            if (gds_IndicadoresEconomicos.Tables["Table"].Rows.Count > 0)
            {
                gvpIndicadoresEconomicos.DataSource = gds_IndicadoresEconomicos.Tables["Table"];
                gvpIndicadoresEconomicos.DataBind();
                //gds_IndicadoresEconomicos = lds_IndicadoresEconomicos;
            }
            else
            {
                gvpIndicadoresEconomicos.DataSource = this.LlenarTablaVacia();
                gvpIndicadoresEconomicos.DataBind();
                gvpIndicadoresEconomicos.Rows[0].Visible = false;
            }
        }

        private DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("IdIndicadorEco", typeof(string));
            ldt_TablaVacia.Columns.Add("Transaccion", typeof(string));
            ldt_TablaVacia.Columns.Add("NomIndicador", typeof(string));
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

        protected void btnIndicadoresConsultar_Click(object sender, EventArgs e)
        {
            OcultarMensaje();
            ConsultarIndicadoresEconomicos(txtBusqIdIndicadores.Text, txtBusqTransaccion.Text, txtBusqNomIndicadores.Text);
        
        }

        protected void gvpIndicadoresEconomicos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvpIndicadoresEconomicos.PageIndex = e.NewPageIndex;
            gvpIndicadoresEconomicos.DataSource = gds_IndicadoresEconomicos.Tables["Table"];

            gvpIndicadoresEconomicos.DataBind();
            //ConsultarIndicadoresEconomicos(txtBusqIdIndicadores.Text, txtBusqTransaccion.Text, txtBusqNomIndicadores.Text);
        }

        protected void gvpIndicadoresEconomicos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvpIndicadoresEconomicos.EditIndex = e.NewEditIndex;
            gvpIndicadoresEconomicos.DataSource = gds_IndicadoresEconomicos.Tables["Table"];

            gvpIndicadoresEconomicos.DataBind();
            //ConsultarIndicadoresEconomicos("", "", "");
        }

        protected void gvpIndicadoresEconomicos_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            DataSet lds_IndicadoresEconomicos = ws_SGService.uwsConsultarIndicadoresEconomicos(txtBusqIdIndicadores.Text, txtBusqTransaccion.Text, txtBusqNomIndicadores.Text);
        }

        protected void gvpIndicadoresEconomicos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gvpIndicadoresEconomicos.SelectedIndex < 0)
                return;
        }

        protected void gvpIndicadoresEconomicos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvpIndicadoresEconomicos.EditIndex = -1;
            gvpIndicadoresEconomicos.DataSource = gds_IndicadoresEconomicos.Tables["Table"];

            gvpIndicadoresEconomicos.DataBind();
            //ConsultarIndicadoresEconomicos(txtBusqIdIndicadores.Text, txtBusqTransaccion.Text, txtBusqNomIndicadores.Text);
        }

        protected void gvpIndicadoresEconomicos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            String[] lstr_result = new String[3];
            try
            {
                DataRow ldr_IndicadoresEconomicosRow = gds_IndicadoresEconomicos.Tables["Table"].NewRow();
                ldr_IndicadoresEconomicosRow = gds_IndicadoresEconomicos.Tables["Table"].Rows[e.RowIndex];

                string lstr_IdIndicadorEconomico = ldr_IndicadoresEconomicosRow["IdIndicadorEco"].ToString();
                DateTime ldt_FchModifica = Convert.ToDateTime(ldr_IndicadoresEconomicosRow["FchModifica"].ToString());

                string lstr_fecha = String.Empty;
                lstr_fecha = ldt_FchModifica.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                ldt_FchModifica = Convert.ToDateTime(lstr_fecha);

                GridViewRow row = (GridViewRow)gvpIndicadoresEconomicos.Rows[e.RowIndex];
                TextBox lbl_NomIndicadorEconomico = (TextBox)row.FindControl("txtEditNomIndicadores");
                TextBox lbl_Transaccion = (TextBox)row.FindControl("txtEditTransaccion");
                TextBox lbl_Estado = (TextBox)row.FindControl("txtEditarEstado");

                lstr_result = ws_SGService.uwsModificarIndicadorEconomico(lstr_IdIndicadorEconomico, lbl_Transaccion.Text,lbl_NomIndicadorEconomico.Text, lbl_Estado.Text, gstr_Usuario, ldt_FchModifica);

                if (lstr_result[0].ToString().Equals("00"))
                {
                    MostarMensaje(lstr_result[1].ToString(), gchr_MensajeExito); 
                }
                else
                {
                    MostarMensaje(lstr_result[1].ToString(), gchr_MensajeExito);
                }
                gvpIndicadoresEconomicos.EditIndex = -1;
                gvpIndicadoresEconomicos.DataSource = gds_IndicadoresEconomicos.Tables["Table"];

                gvpIndicadoresEconomicos.DataBind();
                //ConsultarIndicadoresEconomicos(txtBusqIdIndicadores.Text, txtBusqTransaccion.Text, txtBusqNomIndicadores.Text);
            }
            catch (Exception ex)
            {
                ConsultarIndicadoresEconomicos(txtBusqIdIndicadores.Text, txtBusqTransaccion.Text, txtBusqNomIndicadores.Text);
                MostarMensaje(ex.ToString(), gchr_MensajeError);

            }
        }


        protected void btnCatalogoNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Mantenimiento/Gestiones/frmNuevoIndicadorEconomico.aspx", false);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {

        }

        protected void gvpIndicadoresEconomicos_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }


        protected void btnIndicadoresActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show(wsDE.ActualizarIndicadores());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


    }
}