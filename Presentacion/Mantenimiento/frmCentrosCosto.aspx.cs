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
    public partial class frmCentrosCosto : BASE
    {
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private char gchr_MensajeError;
        private char gchr_MensajeExito;
        private string gstr_Usuario = String.Empty;

        //static DataSet gds_CentrosCosto = new DataSet();
        protected DataSet gds_CentrosCosto
        {
            get
            {
                if (ViewState["gds_CentrosCosto"] == null)
                    ViewState["gds_CentrosCosto"] = new DataSet();
                return (DataSet)ViewState["gds_CentrosCosto"];
            }
            set
            {
                ViewState["gds_CentrosCosto"] = value;
            }
        }
        DateTime gdt_FechaConsulta = new DateTime(1900, 1, 1);

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
                        ConsultarCentrosCosto("", "", "", "", "", "", "");
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
        
        private void ConsultarCentrosCosto(string str_IdCentroCosto, string str_FchVigenciaHasta, string str_IdSociedadCo, string str_IdSociedadFi, string str_FchConsulta, string str_Denominacion, string str_NomCentroCosto)
        {
            OcultarMensaje();
            

                if (!txtFchVigencia.Text.Equals(""))
                    gdt_FechaConsulta = Convert.ToDateTime(txtFchVigencia.Text);                
                else
                    gdt_FechaConsulta = new DateTime(1900, 1, 1);

                gds_CentrosCosto = ws_SGService.uwsConsultarCentrosCosto(str_IdCentroCosto, gdt_FechaConsulta, str_IdSociedadCo, str_IdSociedadFi, DateTime.Today, str_Denominacion, str_NomCentroCosto);

                if (gds_CentrosCosto.Tables["Table"].Rows.Count > 0)
            {
                grdvCentrosCosto.DataSource = gds_CentrosCosto.Tables["Table"];
                grdvCentrosCosto.DataBind();
            }
            else
            {
                grdvCentrosCosto.DataSource = this.LlenarTablaVacia();
                grdvCentrosCosto.DataBind();
            }
        }

        private DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("IdCentroCosto", typeof(string));
            ldt_TablaVacia.Columns.Add("FchVigencia", typeof(string));
            ldt_TablaVacia.Columns.Add("FchVigenciaHasta", typeof(string));
            ldt_TablaVacia.Columns.Add("IdSociedadCo", typeof(string));
            ldt_TablaVacia.Columns.Add("IdSociedadFi", typeof(string));
            ldt_TablaVacia.Columns.Add("IdCentroBeneficio", typeof(string));
            ldt_TablaVacia.Columns.Add("Denominacion", typeof(string));
            ldt_TablaVacia.Columns.Add("NomCentroCosto", typeof(string));
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

        protected void btnCentrosCostoNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Mantenimiento/Gestion CentrosCosto/frmNuevoCuentasContable.aspx", false);
        }

        protected void btnCentrosCostoGuardar_Click(object sender, EventArgs e)
        {

        }

        protected void btnCentrosCostoVolver_Click(object sender, EventArgs e)
        {

        }

        protected void btnCentrosCostoConsultar_Click(object sender, EventArgs e)
        {
            OcultarMensaje();
            ConsultarCentrosCosto(txtIdCentroCosto.Text, gdt_FechaConsulta.ToString(), txtIdSociedadCo.Text,
                txtIdSociedadFi.Text, "", "", txtNomCentroCosto.Text);
        }

        protected void grdvCentrosCosto_SelectedIndexChanged(object sender, EventArgs e)
        { 
            if (grdvCentrosCosto.SelectedIndex < 0)
                return;
        }

        protected void grdvCentrosCosto_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            String[] lstr_result = new String[3];
            try
            {
                DataRow ldr_CentrosCostoRow = gds_CentrosCosto.Tables["Table"].NewRow();
                ldr_CentrosCostoRow = gds_CentrosCosto.Tables["Table"].Rows[e.RowIndex];

                string lstr_IdCentroCosto = ldr_CentrosCostoRow["IdCentroCosto"].ToString();
                string lstr_FchVigenciaHasta = ldr_CentrosCostoRow["FchVigenciaHasta"].ToString();
                string lstr_IdSociedadCo = ldr_CentrosCostoRow["IdSociedadCo"].ToString();
                string lstr_IdSociedadFi = ldr_CentrosCostoRow["IdSociedadFi"].ToString();
                DateTime ldt_FchModifica = Convert.ToDateTime(ldr_CentrosCostoRow["FchModifica"].ToString());

                string lstr_fecha = String.Empty;
                lstr_fecha = ldt_FchModifica.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                ldt_FchModifica = Convert.ToDateTime(lstr_fecha);

                GridViewRow lgrdv_CentrosCostoRow = (GridViewRow)grdvCentrosCosto.Rows[e.RowIndex];
                TextBox txt_FchVigencia = (TextBox)lgrdv_CentrosCostoRow.FindControl("txtEditFchVigencia");
                TextBox txt_IdCentroBeneficio = (TextBox)lgrdv_CentrosCostoRow.FindControl("txtEditIdCentroBeneficio");
                TextBox txt_Denominacion = (TextBox)lgrdv_CentrosCostoRow.FindControl("txtEditDenominacion");
                TextBox txt_NomCentroCosto = (TextBox)lgrdv_CentrosCostoRow.FindControl("txtEditNomCentroCosto");
                TextBox txt_Estado = (TextBox)lgrdv_CentrosCostoRow.FindControl("txtEditEstado");

                //lstr_result = ws_SGService.uwsModificarC
                if (lstr_result[0].ToString().Equals("00"))
                {
                    MostarMensaje(lstr_result[1].ToString(), gchr_MensajeExito);
                }
                else
                {
                    MostarMensaje(lstr_result[1].ToString(), gchr_MensajeExito);
                }
                grdvCentrosCosto.EditIndex = -1;
                grdvCentrosCosto.DataSource = gds_CentrosCosto.Tables["Table"];

                grdvCentrosCosto.DataBind();
                //ConsultarCentrosCosto("", "", "", "", "", "", "");
            }
            catch (Exception ex)
            {
                ConsultarCentrosCosto("", "", "", "", "", "", "");
                MostarMensaje(ex.ToString(), gchr_MensajeError);

            }
        }

        protected void grdvCentrosCosto_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            ConsultarCentrosCosto("", "", "", "", "", "", "");
            //DataSet lds_CentrosCosto = ws_SGService.uwsConsultarCentrosCosto("", DateTime.Today, "", "", DateTime.Today, "", "");
        }


        protected void grdvCentrosCosto_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdvCentrosCosto.PageIndex = e.NewPageIndex;
            grdvCentrosCosto.DataSource = gds_CentrosCosto.Tables["Table"];

            grdvCentrosCosto.DataBind();
            //ConsultarCentrosCosto(txtIdCentroCosto.Text, gdt_FechaConsulta.ToString(), txtIdSociedadCo.Text,
                //txtIdSociedadFi.Text, "", "", txtNomCentroCosto.Text);
        }

        protected void grdvCentrosCosto_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdvCentrosCosto.EditIndex = -1;
            grdvCentrosCosto.DataSource = gds_CentrosCosto.Tables["Table"];

            grdvCentrosCosto.DataBind();
            //ConsultarCentrosCosto(txtIdCentroCosto.Text, gdt_FechaConsulta.ToString(), txtIdSociedadCo.Text,
            //    txtIdSociedadFi.Text, "", "", txtNomCentroCosto.Text);
        }

        protected void grdvCentrosCosto_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void btnCentroBeneficioConsultar_Click(object sender, EventArgs e)
        {
            OcultarMensaje();

            DateTime ldt_FchVigencia = Convert.ToDateTime( this.txtFchVigencia.Text);

        }

        //protected void btnBusqFchIndicador_Click(object sender, EventArgs e)
        //{
        //    if (cdrBusqFchIndicador.Visible)
        //    {
        //        cdrBusqFchIndicador.Visible = false;
        //    }
        //    else
        //    {
        //        if (FchVigencia.Text.Trim() != "")
        //            cdrBusqFchIndicador.SelectedDate = Convert.ToDateTime(FchVigencia.Text);
        //        cdrBusqFchIndicador.Visible = true;
        //    }
        //}


    }
}