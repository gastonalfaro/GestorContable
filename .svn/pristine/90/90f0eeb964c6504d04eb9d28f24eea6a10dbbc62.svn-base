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
    public partial class frmValoresIndicadoresEco : BASE
    {
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private char gchr_MensajeError;
        private char gchr_MensajeExito;
        private string gstr_Usuario = String.Empty;

        //static DataSet gds_ValoresIndicadoresEco = new DataSet();
        protected DataSet gds_ValoresIndicadoresEco
        {
            get
            {
                if (ViewState["gds_ValoresIndicadoresEco"] == null)
                    ViewState["gds_ValoresIndicadoresEco"] = new DataSet();
                return (DataSet)ViewState["gds_ValoresIndicadoresEco"];
            }
            set
            {
                ViewState["gds_ValoresIndicadoresEco"] = value;
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
                    if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmValoresIndicadoresEco"))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                    {

                        OcultarMensaje();

                        txtBusqFchIndicador.Text = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        ConsultarValoresIndicadoresEco("", Convert.ToDateTime(txtBusqFchIndicador.Text));
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
            ldt_TablaVacia.Columns.Add("IdIndicadorEco", typeof(string));
            ldt_TablaVacia.Columns.Add("FchReferencia", typeof(DateTime));
            ldt_TablaVacia.Columns.Add("Valor", typeof(string));
            DataRow ldr_FilaTabla = ldt_TablaVacia.NewRow();
            ldt_TablaVacia.Rows.Add(ldr_FilaTabla);
            return ldt_TablaVacia;
        }

        private void ConsultarValoresIndicadoresEco(string str_IdIndicadorEco, DateTime dt_FchReferencia)
        {
            gds_ValoresIndicadoresEco = ws_SGService.uwsConsultarValoresIndicadoresEco(str_IdIndicadorEco, dt_FchReferencia, "N");

            if (gds_ValoresIndicadoresEco.Tables["Table"].Rows.Count > 0)
            {
                gvpValoresIndicadoresEco.DataSource = gds_ValoresIndicadoresEco.Tables["Table"];
                gvpValoresIndicadoresEco.DataBind();
            }
            else
            {
                gvpValoresIndicadoresEco.DataSource = this.LlenarTablaVacia();
                gvpValoresIndicadoresEco.DataBind();
                gvpValoresIndicadoresEco.Rows[0].Visible = false;
            }
        }

        protected void btnIndicadoresVolver_Click(object sender, EventArgs e)
        {

        }

        protected void btnIndicadoresConsultar_Click(object sender, EventArgs e)
        {
            OcultarMensaje();
            try
            {
                ConsultarValoresIndicadoresEco(txtBusqIdIndicador.Text, Convert.ToDateTime(txtBusqFchIndicador.Text));
            }
            catch
            {
                //txtBusqFchIndicador.Text = cdrBusqFchIndicador.SelectedDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                //txtBusqFchIndicador.Text = DateTime.Today.ToShortDateString();
                ConsultarValoresIndicadoresEco(txtBusqIdIndicador.Text, DateTime.Today);
            }
        }

        protected void btnIndicadoresNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Mantenimiento/Gestion Modulos/frmNuevoValorIndicadorEco.aspx", false);
        }

        protected void btnIndicadoresGuardar_Click(object sender, EventArgs e)
        {

        }

        protected void gvpValoresIndicadoresEco_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gvpValoresIndicadoresEco.SelectedIndex < 0)
                return;
        }

        protected void gvpValoresIndicadoresEco_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gvpValoresIndicadoresEco_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //String[] lstr_result = new String[3];
            //try
            //{
            //    DataRow ldr_ModulosRow = gds_Modulos.Tables["Table"].NewRow();
            //    ldr_ModulosRow = gds_Modulos.Tables["Table"].Rows[e.RowIndex];

            //    string lstr_IdModulo = ldr_ModulosRow["IdModulo"].ToString();
            //    DateTime ldt_FchModifica = Convert.ToDateTime(ldr_ModulosRow["FchModifica"].ToString());

            //    string lstr_fecha = String.Empty;
            //    lstr_fecha = ldt_FchModifica.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            //    ldt_FchModifica = Convert.ToDateTime(lstr_fecha);

            //    GridViewRow row = (GridViewRow)grdvModulos.Rows[e.RowIndex];
            //    TextBox lbl_NomModulo = (TextBox)row.FindControl("txtEditNomModulo");
            //    TextBox lbl_Estado = (TextBox)row.FindControl("txtEditEstado");

            //    lstr_result = ws_SGService.uwsModificarModulo(lstr_IdModulo, lbl_NomModulo.Text, lbl_Estado.Text, gstr_Usuario, ldt_FchModifica);

            //    if (lstr_result[0].ToString().Equals("00"))
            //    {
            //        MostarMensaje(lstr_result[1].ToString(), gchr_MensajeExito);
            //    }
            //    else
            //    {
            //        MostarMensaje(lstr_result[1].ToString(), gchr_MensajeExito);
            //    }
            //    grdvModulos.EditIndex = -1;
            //    ConsultarModulos("", "");
            //}
            //catch (Exception ex)
            //{
            //    ConsultarModulos("", "");
            //    MostarMensaje(ex.ToString(), gchr_MensajeError);

            //}
        }

        protected void gvpValoresIndicadoresEco_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            //DataSet lds_Modulos = ws_SGService.uwsConsultarValoresIndicadoresEco("", DateTime.Now,"N" );
        }

        protected void gvpValoresIndicadoresEco_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvpValoresIndicadoresEco.PageIndex = e.NewPageIndex;
            gvpValoresIndicadoresEco.DataSource = gds_ValoresIndicadoresEco.Tables["Table"];

            gvpValoresIndicadoresEco.DataBind();
            //ConsultarValoresIndicadoresEco(this.txtBusqIdIndicador.Text, this.txtBusqFchIndicador.Text.Equals(string.Empty) ? DateTime.Now : Convert.ToDateTime(this.txtBusqFchIndicador.Text));
        }

        protected void gvpValoresIndicadoresEco_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvpValoresIndicadoresEco.EditIndex = -1;
            gvpValoresIndicadoresEco.DataSource = gds_ValoresIndicadoresEco.Tables["Table"];

            gvpValoresIndicadoresEco.DataBind();
            //ConsultarValoresIndicadoresEco(this.txtBusqIdIndicador.Text, this.txtBusqFchIndicador.Text.Equals(string.Empty) ? DateTime.Now : Convert.ToDateTime(this.txtBusqFchIndicador.Text));
        }

        //protected void btnBusqFchIndicador_Click(object sender, EventArgs e)
        //{
        //    if (cdrBusqFchIndicador.Visible)
        //    {
        //        cdrBusqFchIndicador.Visible = false;
        //    }
        //    else
        //    {
        //        if (txtBusqFchIndicador.Text.Trim() != "")
        //            cdrBusqFchIndicador.SelectedDate = Convert.ToDateTime(txtBusqFchIndicador.Text);
        //        cdrBusqFchIndicador.Visible = true;
        //    }
        //}

     

        //protected void btnBusqFchIndicador_Click1(object sender, EventArgs e)
        //{
        //    if (cdrBusqFchIndicador.Visible)
        //    {
        //        cdrBusqFchIndicador.Visible = false;
        //    }
        //    else
        //    {
        //        if (txtBusqFchIndicador.Text.Trim() != "")
        //            cdrBusqFchIndicador.SelectedDate = Convert.ToDateTime(txtBusqFchIndicador.Text);
        //        cdrBusqFchIndicador.Visible = true;
        //    }
        //}

        protected void cdrBusqFchIndicador_SelectionChanged1(object sender, EventArgs e)
        {

        }
    }
}