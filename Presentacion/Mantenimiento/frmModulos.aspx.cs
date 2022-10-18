using Presentacion.Compartidas;
using System;
using System.Collections.Generic;
using System.Data;
//using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Presentacion.Mantenimiento
{
    public partial class frmModulos : BASE
    {
        #region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private char gchr_MensajeError;
        private char gchr_MensajeExito;
        private string gstr_Usuario = String.Empty;
        private string gstr_ModuloActual = String.Empty;

        //static DataSet gds_Modulos = new DataSet();
        protected DataSet gds_Modulos
        {
            get
            {
                if (ViewState["gds_Modulos"] == null)
                    ViewState["gds_Modulos"] = new DataSet();
                return (DataSet)ViewState["gds_Modulos"];
            }
            set
            {
                ViewState["gds_Modulos"] = value;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            gstr_Usuario = clsSesion.Current.LoginUsuario;
            gchr_MensajeError = clsSesion.Current.chr_MensajeError;
            gchr_MensajeExito = clsSesion.Current.chr_MensajeExito;
            gstr_ModuloActual = "MA";
            clsSesion.Current.gstr_ModuloActual = gstr_ModuloActual;
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
                        ConsultarModulos("", "");
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
        
        private void ConsultarModulos(string str_IdModulo, string str_NomModulo)
        {
            gds_Modulos = ws_SGService.uwsConsultarModulos(str_IdModulo, str_NomModulo);

            if (gds_Modulos.Tables["Table"].Rows.Count > 0)
            {
                grdvModulos.DataSource = gds_Modulos.Tables["Table"];
                grdvModulos.DataBind();
            }
            else
            {
                grdvModulos.DataSource = this.LlenarTablaVacia();
                grdvModulos.DataBind();
                grdvModulos.Rows[0].Visible = false;
            }
        }

        private DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("IdModulo", typeof(string));
            ldt_TablaVacia.Columns.Add("NomModulo", typeof(string));
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

        protected void btnModuloNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Mantenimiento/Gestion Modulos/frmNuevoModulo.aspx", false);            
        }

        protected void btnModuloGuardar_Click(object sender, EventArgs e)
        {

        }

        protected void btnModuloVolver_Click(object sender, EventArgs e)
        {

        }

        protected void btnModuloConsultar_Click(object sender, EventArgs e)
        {
            OcultarMensaje();
            ConsultarModulos(txtBusqIdModulo.Text, txtBusqNomModulo.Text);
        }

        protected void grdvModulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grdvModulos.SelectedIndex < 0)
                return;
        }

        protected void grdvModulos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            String[] lstr_result = new String[3];
            try
            {
                DataRow ldr_ModulosRow = gds_Modulos.Tables["Table"].NewRow();
                ldr_ModulosRow = gds_Modulos.Tables["Table"].Rows[e.RowIndex];

                string lstr_IdModulo = ldr_ModulosRow["IdModulo"].ToString();
                string lstr_Estado = ldr_ModulosRow["Estado"].ToString();
                DateTime ldt_FchModifica = Convert.ToDateTime(ldr_ModulosRow["FchModifica"].ToString());

                string lstr_fecha = String.Empty;
                lstr_fecha = ldt_FchModifica.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                ldt_FchModifica = Convert.ToDateTime(lstr_fecha);

                GridViewRow row = (GridViewRow)grdvModulos.Rows[e.RowIndex];
                TextBox txt_NomModulo = (TextBox)row.FindControl("txtEditNomModulo");
                //TextBox txt_Estado = (TextBox)row.FindControl("txtEditEstado");

                lstr_result = ws_SGService.uwsModificarModulo(lstr_IdModulo, txt_NomModulo.Text, lstr_Estado, gstr_Usuario, ldt_FchModifica);
                
                if ( lstr_result[0].ToString().Equals("00"))
                {
                    MostarMensaje(lstr_result[1].ToString(), gchr_MensajeExito);
                }
                else
                {
                    MostarMensaje(lstr_result[1].ToString(), gchr_MensajeExito);
                }
                grdvModulos.EditIndex = -1;
                grdvModulos.DataSource = gds_Modulos.Tables["Table"];

                grdvModulos.DataBind();
                //ConsultarModulos(txtBusqIdModulo.Text, txtBusqNomModulo.Text);
            }
            catch (Exception ex)
            {
                ConsultarModulos(txtBusqIdModulo.Text, txtBusqNomModulo.Text);
                MostarMensaje(ex.ToString(), gchr_MensajeError);
                
            }
        }

        protected void grdvModulos_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            //DataSet lds_Modulos = ws_SGService.uwsConsultarModulos(txtBusqIdModulo.Text, txtBusqNomModulo.Text);
        }


        protected void grdvModulos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdvModulos.PageIndex = e.NewPageIndex;
            grdvModulos.DataSource = gds_Modulos.Tables["Table"];

            grdvModulos.DataBind();
            //ConsultarModulos(txtBusqIdModulo.Text, txtBusqNomModulo.Text);
        }

        protected void grdvModulos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdvModulos.EditIndex = -1;
            grdvModulos.DataSource = gds_Modulos.Tables["Table"];

            grdvModulos.DataBind();
            //ConsultarModulos(txtBusqIdModulo.Text, txtBusqNomModulo.Text);
        }

        protected void grdvModulos_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }


        
    }
}