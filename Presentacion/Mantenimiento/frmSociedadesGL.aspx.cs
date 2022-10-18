﻿using System;
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
    public partial class frmSociedadesGL : BASE
    {

        # region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private char gchr_MensajeError;
        private char gchr_MensajeExito;
        private string gstr_NomSociedadGL = String.Empty;
        private string gstr_IdSociedadGL = String.Empty;
        private string gstr_Usuario = String.Empty;
        //static DataSet gds_SociedadesGL = new DataSet();
        protected DataSet gds_SociedadesGL
        {
            get
            {
                if (ViewState["gds_SociedadesGL"] == null)
                    ViewState["gds_SociedadesGL"] = new DataSet();
                return (DataSet)ViewState["gds_SociedadesGL"];
            }
            set
            {
                ViewState["gds_SociedadesGL"] = value;
            }
        }
        private string gstr_ModuloActual = String.Empty;
        private string gstr_Modulo = String.Empty;
        # endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            gstr_Usuario = clsSesion.Current.LoginUsuario;
            gchr_MensajeError = clsSesion.Current.chr_MensajeError;
            gchr_MensajeExito = clsSesion.Current.chr_MensajeExito;
            gstr_Modulo = clsSesion.Current.gstr_ModuloActual;

            ConsultaDatos(); //gastoneliminar

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(gstr_Usuario))
                {
                    if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmSociedadesGL"))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                    {

                        OcultarMensaje();
                        ConsultarSociedadesGL("", "", "", "", "");
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

        private void ConsultaDatos()
        {

            DataSet dsRegistrosIngresados = ws_SGService.uwsConsultarDinamico("select * from ma.Direcciones");
            int x = 0;
            //string RegistrosIncluidos = dsRegistrosIngresados.Tables[0].Rows[0][0].ToString();


        }

        private void ConsultarSociedadesGL(string str_IdSociedadGL, string str_Denominacion, string str_NomSociedad, string str_IdPais, string str_IdMoneda)
        {
            gds_SociedadesGL = ws_SGService.uwsConsultarSociedadesGL(str_IdSociedadGL, str_Denominacion, str_NomSociedad, str_IdPais, str_IdMoneda);

            if (gds_SociedadesGL.Tables["Table"].Rows.Count > 0)
            {
                grdSociedadesGL.DataSource = gds_SociedadesGL.Tables["Table"];
                grdSociedadesGL.DataBind();
            }
            else
            {
                grdSociedadesGL.DataSource = this.LlenarTablaVacia();
                grdSociedadesGL.DataBind();
                grdSociedadesGL.Rows[0].Visible = false;
            }
        }

        private DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("IdSociedadGL", typeof(string));
            ldt_TablaVacia.Columns.Add("Denominacion", typeof(string));
            ldt_TablaVacia.Columns.Add("NomSociedad", typeof(string));
            ldt_TablaVacia.Columns.Add("IdPais", typeof(string));
            ldt_TablaVacia.Columns.Add("Poblacion", typeof(string));
            ldt_TablaVacia.Columns.Add("Calle", typeof(string));
            ldt_TablaVacia.Columns.Add("IdMoneda", typeof(string));
            ldt_TablaVacia.Columns.Add("IdIdioma", typeof(string));
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

        protected void btnVolverSociedadesGL_Click(object sender, EventArgs e)
        {

        }

        protected void btnConsultarSociedadesGL_Click(object sender, EventArgs e)
        {
            OcultarMensaje();
            ConsultarSociedadesGL(txtBusqIdSociedadGL.Text, "", txtBusqNomSociedad.Text, txtIdPais.Text, txtBusqIdMoneda.Text);
        
        }

        protected void grdSociedadesGL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grdSociedadesGL.SelectedIndex < 0)
                return;
        }

        protected void grdSociedadesGL_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdSociedadesGL.EditIndex = e.NewEditIndex;
            grdSociedadesGL.DataSource = gds_SociedadesGL.Tables["Table"];

            grdSociedadesGL.DataBind();
            //ConsultarSociedadesGL(txtBusqIdSociedadGL.Text, "", txtBusqNomSociedad.Text, txtIdPais.Text, txtBusqIdMoneda.Text);
          
            //DataRow ldr_SociedadesGLRow  = gds_SociedadesGL.Tables["Table"].Rows[e.NewEditIndex];

            //string lint_IdSociedadesGL = ldr_SociedadesGLRow["IdSociedadGL"].ToString();
            //string lint_NomSociedadesGL = ldr_SociedadesGLRow["NomSociedad"].ToString();

            //clsSesion.Current.IdSociedadGL = lint_IdSociedadesGL;
            //clsSesion.Current.NomSociedadGL = lint_NomSociedadesGL;


          //  Response.Redirect("~/Mantenimiento/frmDirecciones.aspx", false);
        }

        protected void grdSociedadesGL_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DataRow ldr_SociedadesGLRow = gds_SociedadesGL.Tables["Table"].NewRow();
            ldr_SociedadesGLRow = gds_SociedadesGL.Tables["Table"].Rows[e.RowIndex];

            string lint_IdSociedadesGL = ldr_SociedadesGLRow["IdSociedadGL"].ToString();
            string lint_NomSociedadesGL = ldr_SociedadesGLRow["NomSociedad"].ToString();

            clsSesion.Current.IdSociedadGL = lint_IdSociedadesGL;
            clsSesion.Current.NomSociedadGL = lint_NomSociedadesGL;

            grdSociedadesGL.EditIndex = -1;
            grdSociedadesGL.DataSource = gds_SociedadesGL.Tables["Table"];

            grdSociedadesGL.DataBind();
            //ConsultarSociedadesGL("", "", "", "", "");
            Response.Redirect("~/Mantenimiento/frmDirecciones.aspx", false);

            //String[] lstr_result = new String[3];
            //try
            //{
            //    DataRow ldr_SociedadesGLRow = gds_SociedadesGL.Tables["Table"].NewRow();
            //    ldr_SociedadesGLRow = gds_SociedadesGL.Tables["Table"].Rows[e.RowIndex];

            //    int lint_IdSociedadesGL = (int)ldr_SociedadesGLRow["IdSociedadGL"];
            //    DateTime ldt_FchModifica = Convert.ToDateTime(ldr_SociedadesGLRow["FchModifica"].ToString());

            //    string str_fecha = String.Empty;
            //    str_fecha = ldt_FchModifica.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            //    ldt_FchModifica = Convert.ToDateTime(str_fecha);

            //    GridViewRow row = (GridViewRow)grdSociedadesGL.Rows[e.RowIndex];

            //    TextBox txt_Nombre = (TextBox)row.FindControl("txtEditNombre");
            //    TextBox txt_Estado = (TextBox)row.FindControl("txtEditEstado");

            //    //lstr_result = ws_SGService.uwsModificarSociedadGL(lint_IdSociedadesGL, txt_Nombre.Text, txt_Nombre.Text, txt_Estado.Text, "jnet", ldt_FchModifica);

            //    if (lstr_result[0].ToString().Equals("00"))
            //    {
            //        MostarMensaje(lstr_result[1].ToString(), gchr_MensajeExito);
            //    }
            //    else
            //    {
            //        MostarMensaje("Error: " + lstr_result[1].ToString(), gchr_MensajeError);
            //    }
                
            //}
            //catch (Exception ex)
            //{
            //    ConsultarSociedadesGL("", "", "", "", "");
            //    MostarMensaje(ex.ToString(), gchr_MensajeError);

            //}
        }

        protected void grdSociedadesGL_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            grdSociedadesGL.EditIndex = -1;
            grdSociedadesGL.DataSource = gds_SociedadesGL.Tables["Table"];

            grdSociedadesGL.DataBind();
            //ConsultarSociedadesGL(txtBusqIdSociedadGL.Text, "", txtBusqNomSociedad.Text, txtIdPais.Text, txtBusqIdMoneda.Text);
        }

        protected void grdSociedadesGL_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdSociedadesGL.PageIndex = e.NewPageIndex;
            grdSociedadesGL.DataSource = gds_SociedadesGL.Tables["Table"];

            grdSociedadesGL.DataBind();
            //ConsultarSociedadesGL(txtBusqIdSociedadGL.Text, "", txtBusqNomSociedad.Text, txtIdPais.Text, txtBusqIdMoneda.Text);
        }
        protected void grdSociedadesGL_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdSociedadesGL.EditIndex = -1;
            grdSociedadesGL.DataSource = gds_SociedadesGL.Tables["Table"];

            grdSociedadesGL.DataBind();
            //ConsultarSociedadesGL(txtBusqIdSociedadGL.Text, "", txtBusqNomSociedad.Text, txtIdPais.Text, txtBusqIdMoneda.Text);
        }


        protected void grdSociedadesGL_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                LinkButton lb = (LinkButton)e.CommandSource;
                GridViewRow vGridViewRow = (GridViewRow)lb.NamingContainer;

                clsSesion.Current.IdSociedadGL = grdSociedadesGL.DataKeys[vGridViewRow.RowIndex].Values[0].ToString();
                clsSesion.Current.NomSociedadGL = grdSociedadesGL.DataKeys[vGridViewRow.RowIndex].Values[1].ToString(); 

                Response.Redirect("~/Mantenimiento/frmDirecciones.aspx", false);
                
            }

        }

    }
}