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
    public partial class frmPrevisionesIncobrables : BASE
    {
        # region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private char gchr_MensajeError;
        private char gchr_MensajeExito;
        private string gstr_Usuario = String.Empty;
        //static DataSet gds_PrevisionesIncobrables = new DataSet();
        protected DataSet gds_PrevisionesIncobrables
        {
            get
            {
                if (ViewState["gds_PrevisionesIncobrables"] == null)
                    ViewState["gds_PrevisionesIncobrables"] = new DataSet();
                return (DataSet)ViewState["gds_PrevisionesIncobrables"];
            }
            set
            {
                ViewState["gds_PrevisionesIncobrables"] = value;
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
                    if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, ""))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                    {

                        OcultarMensaje();
                        ConsultarPrevisionesIncobrables(null, null, string.Empty);
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

        private void ConsultarPrevisionesIncobrables(Nullable<int> int_DiasMorosidad, Nullable<Decimal> dec_PorcEstimacion, string str_Denominacion)
        {
            gds_PrevisionesIncobrables = ws_SGService.uwsConsultarPrevisionesIncobrables(int_DiasMorosidad, dec_PorcEstimacion, str_Denominacion);

            if (gds_PrevisionesIncobrables.Tables["Table"].Rows.Count > 0)
            {
                grdPrevisionesIncobrables.DataSource = gds_PrevisionesIncobrables.Tables["Table"];
                grdPrevisionesIncobrables.DataBind();
            }
            else
            {
                grdPrevisionesIncobrables.DataSource = this.LlenarTablaVacia();
                grdPrevisionesIncobrables.DataBind();
                grdPrevisionesIncobrables.Rows[0].Visible = false;
            }
        }

        private DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("DiasMorosidad", typeof(string));
            ldt_TablaVacia.Columns.Add("PorcEstimacion", typeof(string));
            ldt_TablaVacia.Columns.Add("Descripcion", typeof(string));
            ldt_TablaVacia.Columns.Add("FchModifica", typeof(string));

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
        
        protected void grdPrevisionesIncobrables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grdPrevisionesIncobrables.SelectedIndex < 0)
                return;
        }

        protected void grdPrevisionesIncobrables_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdPrevisionesIncobrables.EditIndex = e.NewEditIndex;
            grdPrevisionesIncobrables.DataSource = gds_PrevisionesIncobrables.Tables["Table"];

            grdPrevisionesIncobrables.DataBind();
            //Int32? DiasMorosidad = null;
            //Decimal? Porcentaje = null;
            //try { Porcentaje = Convert.ToDecimal(this.txtPorcEstimacion.Text); }
            //catch (Exception) { }
            //try { DiasMorosidad = Convert.ToInt32(this.txtDiasMorosidad.Text); }
            //catch (Exception) { }
            //ConsultarPrevisionesIncobrables(DiasMorosidad, Porcentaje, this.txtDescripcion.Text);    
        }

        protected void grdPrevisionesIncobrables_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            String[] lstr_result = new String[3];
            try
            {
                
                GridViewRow row = (GridViewRow)grdPrevisionesIncobrables.Rows[e.RowIndex];
                Label lbl_DiasMorosidad = (Label)row.FindControl("lblDiasMorosidad");
                TextBox txt_Descripcion = (TextBox)row.FindControl("txtEditarDescripcion");
                Label lbl_FchModifica = (Label)row.FindControl("lblFchModifica");

                TextBox txt_PorcEstimacion = (TextBox)row.FindControl("txtPorcEstimacion");
 
                string lstr_fecha = String.Empty;
                lstr_fecha = Convert.ToDateTime(lbl_FchModifica.Text).ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
             
                lstr_result = ws_SGService.uwsModificarPrevisionIncobrable(
                    Convert.ToInt32(lbl_DiasMorosidad.Text),
                    Convert.ToDecimal(txt_PorcEstimacion.Text),
                    txt_Descripcion.Text, gstr_Usuario, 
                    Convert.ToDateTime(lstr_fecha));
                
                MostarMensaje(lstr_result[1].ToString(), gchr_MensajeExito);

                grdPrevisionesIncobrables.EditIndex = -1;
                grdPrevisionesIncobrables.DataSource = gds_PrevisionesIncobrables.Tables["Table"];

                ConsultarPrevisionesIncobrables(null, null, string.Empty);
                //grdPrevisionesIncobrables.DataBind();
                //Int32? DiasMorosidad = null;
                //Decimal? Porcentaje = null;
                //try { Porcentaje = Convert.ToDecimal(this.txtPorcEstimacion.Text); }
                //catch (Exception) { }
                //try { DiasMorosidad = Convert.ToInt32(this.txtDiasMorosidad.Text); }
                //catch (Exception) { }
                //ConsultarPrevisionesIncobrables(DiasMorosidad, Porcentaje, this.txtDescripcion.Text);
            }
            catch (Exception ex)
            {
                Int32? DiasMorosidad = null;
                Decimal? Porcentaje = null;
                try { Porcentaje = Convert.ToDecimal(this.txtPorcEstimacion.Text); }
                catch (Exception) { }
                try { DiasMorosidad = Convert.ToInt32(this.txtDiasMorosidad.Text); }
                catch (Exception) { }
                ConsultarPrevisionesIncobrables(DiasMorosidad, Porcentaje, this.txtDescripcion.Text);
                MostarMensaje(ex.ToString(), gchr_MensajeError);

            }
        }

        protected void grdPrevisionesIncobrables_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdPrevisionesIncobrables.PageIndex = e.NewPageIndex;
            grdPrevisionesIncobrables.DataSource = gds_PrevisionesIncobrables.Tables["Table"];

            grdPrevisionesIncobrables.DataBind();
            //ConsultarPrevisionesIncobrables(Convert.ToInt32(this.txtDiasMorosidad.Text), Convert.ToDecimal(this.txtPorcEstimacion.Text), this.txtDescripcion.Text);
        }

        protected void grdPrevisionesIncobrables_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdPrevisionesIncobrables.EditIndex = -1;
            grdPrevisionesIncobrables.DataSource = gds_PrevisionesIncobrables.Tables["Table"];

            grdPrevisionesIncobrables.DataBind();

            //Int32? DiasMorosidad = null;
            //Decimal? Porcentaje = null;
            //try { Porcentaje = Convert.ToDecimal(this.txtPorcEstimacion.Text); }
            //catch (Exception) { }
            //try { DiasMorosidad = Convert.ToInt32(this.txtDiasMorosidad.Text); }
            //catch (Exception) { }
            //ConsultarPrevisionesIncobrables(DiasMorosidad, Porcentaje, this.txtDescripcion.Text);
        }

        protected void btnNuevosPrevisionesIncobrables_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Mantenimiento/Gestiones/frmNuevoPrevisionesIncobrables.aspx", false);
        }

        protected void btnVolverPrevisionesIncobrables_Click(object sender, EventArgs e)
        {
           
        }

        protected void btnConsultarPrevisionIncobrable_Click(object sender, EventArgs e)
        {
            Int32? DiasMorosidad = null;
            Decimal? Porcentaje = null;
            try { Porcentaje = Convert.ToDecimal(this.txtPorcEstimacion.Text); } catch(Exception){}
            try { DiasMorosidad = Convert.ToInt32(this.txtDiasMorosidad.Text); } catch (Exception) { }
            ConsultarPrevisionesIncobrables(DiasMorosidad, Porcentaje, this.txtDescripcion.Text);
            
        }
        
    }
}