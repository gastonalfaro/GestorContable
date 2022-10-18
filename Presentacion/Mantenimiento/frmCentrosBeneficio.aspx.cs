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
    public partial class frmCentrosBeneficio : BASE
    {
        # region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private char gchr_MensajeError;
        private char gchr_MensajeExito;
        private string gstr_Usuario = String.Empty;
        //static DataSet gds_CentrosBeneficio = new DataSet();
        protected DataSet gds_CentrosBeneficio
        {
            get
            {
                if (ViewState["gds_CentrosBeneficio"] == null)
                    ViewState["gds_CentrosBeneficio"] = new DataSet();
                return (DataSet)ViewState["gds_CentrosBeneficio"];
            }
            set
            {
                ViewState["gds_CentrosBeneficio"] = value;
            }
        }
        DateTime gdt_FechaConsulta = new DateTime(1900, 1, 1);
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
                    if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, ""))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                    {

                        OcultarMensaje();
                        ConsultarCentrosBeneficio("", "", gdt_FechaConsulta, "", "", DateTime.Today, "", "");
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
        
        private void ConsultarCentrosBeneficio(string str_IdCentroBeneficio, string str_NomCentroBeneficio, DateTime dt_FchVigenciaHasta,
            string str_IdSociedadCo, string str_IdSociedadFi, DateTime dt_FchConsulta, string str_Denominacion, string str_NomCentroBenefico )
        {
            
            gds_CentrosBeneficio = ws_SGService.uwsConsultarCentrosBeneficio(str_IdCentroBeneficio, dt_FchVigenciaHasta,
                str_IdSociedadCo, str_IdSociedadFi, dt_FchConsulta, str_Denominacion, str_NomCentroBeneficio);

            if (gds_CentrosBeneficio.Tables["Table"].Rows.Count > 0)
            {
                grdvCentrosBeneficio.DataSource = gds_CentrosBeneficio.Tables["Table"];
                grdvCentrosBeneficio.DataBind();
            }
            else
            {
                grdvCentrosBeneficio.DataSource = this.LlenarTablaVacia();
                grdvCentrosBeneficio.DataBind();
            }
        }

        private DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("IdCentroBeneficio", typeof(string));
            ldt_TablaVacia.Columns.Add("FchVigencia", typeof(string));
            ldt_TablaVacia.Columns.Add("FchVigenciaHasta", typeof(string));
            ldt_TablaVacia.Columns.Add("IdSociedadCo", typeof(string));
            ldt_TablaVacia.Columns.Add("IdSociedadFi", typeof(string));
            ldt_TablaVacia.Columns.Add("Denominacion", typeof(string));
            ldt_TablaVacia.Columns.Add("NomCentroBeneficio", typeof(string));
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

        protected void btnCentroBeneficioNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Mantenimiento/Gestiones/frmNuevoCentroBeneficio.aspx", false);
        }

        protected void btnCentroBeneficioGuardar_Click(object sender, EventArgs e)
        {

        }

        protected void btnCentroBeneficioVolver_Click(object sender, EventArgs e)
        {

        }

        protected void btnCentroBeneficioConsultar_Click(object sender, EventArgs e)
        {
            OcultarMensaje();
            
            if (!txtFchVigencia.Text.Equals(""))
                gdt_FechaConsulta = Convert.ToDateTime(txtFchVigencia.Text);
            else
                gdt_FechaConsulta = new DateTime(1900, 1, 1);

            ConsultarCentrosBeneficio(txtBusqIdCentroBeneficio.Text, txtBusqNomCentroBeneficio.Text, gdt_FechaConsulta, txtIdSociedadCo.Text, txtIdSociedadFi.Text, DateTime.Today, "", "");


        }

        protected void grdvCentrosBeneficio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grdvCentrosBeneficio.SelectedIndex < 0)
                return;
        }

        protected void grdvCentrosBeneficio_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdvCentrosBeneficio.EditIndex = e.NewEditIndex;

            grdvCentrosBeneficio.DataSource = gds_CentrosBeneficio.Tables["Table"];

            grdvCentrosBeneficio.DataBind();
            //if (!txtFchVigencia.Text.Equals(""))
            //    gdt_FechaConsulta = Convert.ToDateTime(txtFchVigencia.Text);
            //else
            //    gdt_FechaConsulta = new DateTime(1900, 1, 1);

            //ConsultarCentrosBeneficio(txtBusqIdCentroBeneficio.Text, txtBusqNomCentroBeneficio.Text, gdt_FechaConsulta, txtIdSociedadCo.Text, txtIdSociedadFi.Text, DateTime.Today, "", "");

        }

        protected void grdvCentrosBeneficio_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void grdvCentrosBeneficio_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdvCentrosBeneficio.PageIndex = e.NewPageIndex;
            grdvCentrosBeneficio.DataSource = gds_CentrosBeneficio.Tables["Table"];

            grdvCentrosBeneficio.DataBind();

            //if (!txtFchVigencia.Text.Equals(""))
            //    gdt_FechaConsulta = Convert.ToDateTime(txtFchVigencia.Text);
            //else
            //    gdt_FechaConsulta = new DateTime(1900, 1, 1);

            //ConsultarCentrosBeneficio(txtBusqIdCentroBeneficio.Text, txtBusqNomCentroBeneficio.Text, gdt_FechaConsulta, txtIdSociedadCo.Text, txtIdSociedadFi.Text, DateTime.Today, "", "");
   
        }

        protected void grdvCentrosBeneficio_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdvCentrosBeneficio.EditIndex = -1;
            grdvCentrosBeneficio.DataSource = gds_CentrosBeneficio.Tables["Table"];

            grdvCentrosBeneficio.DataBind();

            //if (!txtFchVigencia.Text.Equals(""))
            //    gdt_FechaConsulta = Convert.ToDateTime(txtFchVigencia.Text);
            //else
            //    gdt_FechaConsulta = new DateTime(1900, 1, 1);

            //ConsultarCentrosBeneficio(txtBusqIdCentroBeneficio.Text, txtBusqNomCentroBeneficio.Text, gdt_FechaConsulta, txtIdSociedadCo.Text, txtIdSociedadFi.Text, DateTime.Today, "", "");

        }

        //protected void btnBusqFchIndicador_Click(object sender, EventArgs e)
        //{
        //    if (cdrBusqFchIndicador.Visible)
        //    {
        //        cdrBusqFchIndicador.Visible = false;
        //    }
        //    else
        //    {
        //        if (txtFchVigencia.Text.Trim() != "")
        //            cdrBusqFchIndicador.SelectedDate = Convert.ToDateTime(txtFchVigencia.Text);
        //        cdrBusqFchIndicador.Visible = true;
        //    }
        //}

    }
}