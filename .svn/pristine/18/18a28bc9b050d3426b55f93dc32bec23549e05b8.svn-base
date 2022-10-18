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

namespace Presentacion.CapturaIngresos
{
    public partial class frmFormulariosCaptura : BASE
    {
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private wsCaptura.wsCapturaIngreso wsCapturaIngresos = new wsCaptura.wsCapturaIngreso();
     // private static string lstr_separador_decimal = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
        
        private char gchr_MensajeError;
        private char gchr_MensajeExito;
        //private string gstr_Usuario; //= clsSesion.Current.LoginUsuario;
        protected string gstr_Usuario
        {
            get
            {
                if (ViewState["gstr_Usuario"] == null)
                    ViewState["gstr_Usuario"] = string.Empty;
                return (string)ViewState["gstr_Usuario"];
            }
            set
            {
                ViewState["gstr_Usuario"] = value;
            }
        }
        //private DataSet gds_Formularios = new DataSet();
        protected DataSet gds_Formularios
        {
            get
            {
                if (ViewState["gds_Formularios"] == null)
                    ViewState["gds_Formularios"] = new DataSet(); 
                return (DataSet)ViewState["gds_Formularios"];
            }
            set
            {
                ViewState["gds_Formularios"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
          
            gstr_Usuario = clsSesion.Current.LoginUsuario;
            if (!IsPostBack)
            {

                
                if (!string.IsNullOrEmpty(gstr_Usuario))
                {
                    if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "OBJ_CI"))
                        Response.Redirect("~/Principal.aspx", true);
                    else
                    {
                        gchr_MensajeError = clsSesion.Current.chr_MensajeError;
                        gchr_MensajeExito = clsSesion.Current.chr_MensajeExito;
    
                        OcultarMensaje();
                        ConsultarFormularios("", "","","");
                    }
                }
                else
                    Response.Redirect("~/Login.aspx", true);
            }
        }

        private void ConsultarFormularios(string str_IdFormulario, string str_Anno, string str_TipoIdPersona, string str_IdPersona)
        {
            string lstr_IdFormulario ;
            string lstr_Anno;
                
            lstr_IdFormulario = (string.IsNullOrEmpty(str_IdFormulario)) ? "-1" : str_IdFormulario;

            lstr_Anno = (string.IsNullOrEmpty(str_Anno)) ? "-1" : str_Anno;

            string str_consul = "SELECT f.IdFormulario, f.Anno, f.TipoIdPersona, f.IdPersona, f.NomPersona, f.TipoIdPersonaTramite, f.IdPersonaTramite, f.NomPersonaTramite, f.Correo, f.IdSociedadGL, f.IdOficina, f.IdBanco, f.IdElementoPEP, f.IdReservaPresupuestaria, f.NroExpediente, f.Descripcion, f.CtaCliente, f.Direccion, f.FchIngreso, f.FchImpreso, f.FchPago, f.FchContabilizado, f.FchAnulado, f.Estado, f.Observaciones, f.IdMoneda, f.Monto, f.ReferenciaDTR, f.UsrCreacion, f.FchCreacion, f.UsrModifica, f.FchModifica ,"+
               " o.NomOficina,"+
               " o.CorreoNotifica,"+
               " o.UsaExpediente,"+
               " o.Estado AS EstadoPago,"+
               " o.IdDireccion,"+
               " s.NomSociedad,"+
               " m.NomMoneda,"+
               " CASE"+
               "   WHEN f.TipoIdPersona = 'F' THEN 'Física'"+
               "   WHEN f.TipoIdPersona = 'J' THEN 'Jurídica'"+
               "   ELSE 'Dimex'"+
               " END"+
               "   AS DesTipoIdPersona,"+
               " CASE"+
               "   WHEN f.TipoIdPersonaTramite = 'F' THEN 'Física'"+
               "   WHEN f.TipoIdPersonaTramite = 'J' THEN 'Jurídica'"+
               "   ELSE 'Dimex'"+
               " END"+
               "   AS DesTipoIdPersonaTramite,"+
               " CASE"+
               "   WHEN f.Estado = 'PEN' THEN 'Creado'"+
               "   WHEN f.Estado = 'IMP' THEN 'Impreso'"+
               "   WHEN f.Estado = 'ANU' THEN 'Anulado'"+
               "   WHEN f.Estado = 'CNT' THEN 'Contabilizado'"+
               "   WHEN f.Estado = 'PAG' THEN 'Pagado'"+
               "   ELSE '-Estado?-'"+
               " END"+
               "   AS DesEstado,"+
               " CASE"+
               "   WHEN f.Estado = 'CNT'"+
               "   THEN"+
               "      (SELECT TOP 1 tc.Valor"+
               "         FROM ma.TiposCambio tc"+
               "        WHERE     tc.IdMoneda = 'CRC'"+
               "              AND tc.FchReferencia <= f.fchPago"+
               "              AND tc.TipoTransaccion = '317'"+
               "       ORDER BY tc.FchReferencia DESC)"+
               "   WHEN f.Estado = 'PAG'"+
               "   THEN"+
               "      (SELECT TOP 1 tc.Valor"+
               "         FROM ma.TiposCambio tc"+
               "        WHERE     tc.IdMoneda = 'CRC'"+
               "              AND tc.FchReferencia <= f.fchPago"+
               "              AND tc.TipoTransaccion = '317'"+
               "       ORDER BY tc.FchReferencia DESC)"+
               "   ELSE"+
               "      NULL"+
               " END"+
               "   AS TipoCambioCompraUSD,"+
               " CASE"+
               "   WHEN f.Estado = 'CNT'"+
               "   THEN"+
               "      (SELECT TOP 1 tc.Valor"+
               "         FROM ma.TiposCambio tc"+
               "        WHERE     tc.IdMoneda = 'CRC'"+
               "              AND tc.FchReferencia <= f.fchPago"+
               "              AND tc.TipoTransaccion = '318'"+
               "       ORDER BY tc.FchReferencia DESC)"+
               "   WHEN f.Estado = 'PAG'"+
               "   THEN"+
               "      (SELECT TOP 1 tc.Valor"+
               "         FROM ma.TiposCambio tc"+
               "        WHERE     tc.IdMoneda = 'CRC'"+
               "              AND tc.FchReferencia <= f.fchPago"+
               "              AND tc.TipoTransaccion = '318'"+
               "       ORDER BY tc.FchReferencia DESC)"+
               "   ELSE"+
               "      NULL"+
               " END"+
               "   AS TipoCambioVentaUSD"+
          " FROM ci.FormulariosCapturasIngresos f"+
               " LEFT OUTER JOIN ma.Oficinas o"+
               "   ON o.IdSociedadGL = f.IdSociedadGL AND o.IdOficina = f.IdOficina"+
               " LEFT OUTER JOIN ma.SociedadesGL s"+
               "   ON s.IdSociedadGL = f.IdSociedadGL"+
               " LEFT OUTER JOIN ma.Monedas m" +
               "   ON m.IdMoneda = f.IdMoneda"+
               " WHERE (f.IdFormulario = " + lstr_IdFormulario + " or isnull(" + lstr_IdFormulario + ",-1)=-1)" +
               " AND (f.Anno = " + lstr_Anno + " or isnull(" + lstr_Anno + ",-1)=-1)" +
               " AND (f.TipoIdPersona = '" + str_TipoIdPersona + "' or isnull('" + str_TipoIdPersona + "','')='')" +
               " AND (f.IdPersona = '" + str_IdPersona + "' or isnull('" + str_IdPersona + "','')='')" +
               " order by Anno Desc, IdFormulario Desc";
            gds_Formularios = this.ws_SGService.uwsConsultarDinamico(str_consul);


            //gds_Formularios = wsCapturaIngresos.ConsultarFormulariosCapturaIngresos(Convert.ToInt32 (lstr_IdFormulario), Convert.ToInt16( lstr_Anno),str_TipoIdPersona, str_IdPersona,"","","","","","","");

//                gds_Formularios.Tables["Table"].Columns["Monto"].DataType = typeof(Decimal);

            if (gds_Formularios.Tables["Table"].Rows.Count > 0)
            {
                gds_Formularios.Tables[0].DefaultView.Sort = "Anno Desc, IdFormulario Desc";
                grdvFormularios.DataSource = gds_Formularios.Tables["Table"];
                grdvFormularios.DataBind();
            }
            else
            {
                grdvFormularios.DataSource = this.LlenarTablaVacia();
                grdvFormularios.DataBind();
                grdvFormularios.Rows[0].Visible = false;
            }
        }

        private DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("IdFormulario", typeof(string));
            ldt_TablaVacia.Columns.Add("Anno", typeof(string));
            ldt_TablaVacia.Columns.Add("IdPersona", typeof(string));
            ldt_TablaVacia.Columns.Add("IdPersonaTramite", typeof(string));
            ldt_TablaVacia.Columns.Add("IdSociedadGL", typeof(string));
            ldt_TablaVacia.Columns.Add("IdOficina", typeof(string));
            ldt_TablaVacia.Columns.Add("NroExpediente", typeof(string));
            ldt_TablaVacia.Columns.Add("Descripcion", typeof(string));
            ldt_TablaVacia.Columns.Add("IdMoneda", typeof(string));
            ldt_TablaVacia.Columns.Add("Monto", typeof(string));  //typeof(string));
            ldt_TablaVacia.Columns.Add("DesEstado", typeof(string));
            DataRow ldr_FilaTabla = ldt_TablaVacia.NewRow();
            ldr_FilaTabla["Monto"] = 0;
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

        protected void btnFormulariosConsultar_Click(object sender, EventArgs e)
        {
            OcultarMensaje();
            ConsultarFormularios(txtBusqIdFormularios.Text, txtBusqAnnos.Text, ddlTipoPersona.SelectedValue, txtIdPersona.Text);
        }

        protected void btnFormulariosNuevo_Click(object sender, EventArgs e)
        {

        }

        protected void btnFormulariosGuardar_Click(object sender, EventArgs e)
        {

        }

        protected void btnFormulariosVolver_Click(object sender, EventArgs e)
        {

        }

        protected void grdvFormularios_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grdvFormularios.SelectedIndex < 0)
                return;
        }

        protected void grdvFormularios_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //grdvFormularios.EditIndex = e.NewEditIndex;
            //ConsultarFormularios("", "","","");
            //ConsultarFormularios(txtBusqIdFormularios.Text, txtBusqAnnos.Text, ddlTipoPersona.SelectedValue, txtIdPersona.Text);
            DataRow ldr_FormulariosRow = gds_Formularios.Tables["Table"].NewRow();
            ldr_FormulariosRow = gds_Formularios.Tables["Table"].Rows[15 * (grdvFormularios.PageIndex) + e.NewEditIndex];

            string lint_IdFormulario = ldr_FormulariosRow["IdFormulario"].ToString();
            string lint_AnnoFormulario = ldr_FormulariosRow["Anno"].ToString();
            //string lstr_NomCatalogo = ldr_FormulariosRow["Nombre"].ToString();

            clsSesion.Current.IdFormularioCI = lint_IdFormulario;
            clsSesion.Current.AnnoFormularioCI = lint_AnnoFormulario;

            grdvFormularios.EditIndex = -1;
            Response.Redirect("~/CapturaIngresos/frmCapturaIngresos.aspx", false);
        
        }

        protected void grdvFormularios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdvFormularios.PageIndex = e.NewPageIndex;
            ConsultarFormularios(txtBusqIdFormularios.Text, txtBusqAnnos.Text, ddlTipoPersona.SelectedValue, txtIdPersona.Text);
        }

        protected void grdvFormularios_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            String[] lstr_result = new String[3];
            try
            {
                DataRow ldr_FormulariosRow = gds_Formularios.Tables["Table"].NewRow();
                ldr_FormulariosRow = gds_Formularios.Tables["Table"].Rows[e.RowIndex];

                string lstr_IdFormularios = ldr_FormulariosRow["IdFormulario"].ToString();
                DateTime ldt_FchModifica = Convert.ToDateTime(ldr_FormulariosRow["FchModifica"].ToString());

                string lstr_fecha = String.Empty;
                //lstr_fecha = ldt_FchModifica.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                lstr_fecha = ldt_FchModifica.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                ldt_FchModifica = Convert.ToDateTime(lstr_fecha);

                GridViewRow row = (GridViewRow)grdvFormularios.Rows[e.RowIndex];

                TextBox txt_Nombre = (TextBox)row.FindControl("txtEditAnno");
                TextBox txt_Estado = (TextBox)row.FindControl("txtEditEstado");

                //lstr_result = ws_SGService.uwsModificarFormulario(lstr_IdFormularios, txt_Nombre.Text, txt_Estado.Text, gstr_Usuario, ldt_FchModifica);
                if (lstr_result[0].ToString().Equals("00"))
                {
                    MostarMensaje(lstr_result[1].ToString(), gchr_MensajeExito);
                }
                else
                {
                    MostarMensaje("Error: " + lstr_result[1].ToString(), gchr_MensajeError);
                }
                grdvFormularios.EditIndex = -1;
                ConsultarFormularios(txtBusqIdFormularios.Text, txtBusqAnnos.Text, ddlTipoPersona.SelectedValue, txtIdPersona.Text);
            }
            catch (Exception ex)
            {
                ConsultarFormularios(txtBusqIdFormularios.Text, txtBusqAnnos.Text, ddlTipoPersona.SelectedValue, txtIdPersona.Text);
                MostarMensaje(ex.ToString(), gchr_MensajeError);
            }
        }

        protected void grdvFormularios_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdvFormularios.EditIndex = -1;
            ConsultarFormularios(txtBusqIdFormularios.Text, txtBusqAnnos.Text, ddlTipoPersona.SelectedValue, txtIdPersona.Text);
        }

        public void CambioLongitudTexto(string tipo)
        {
            if (tipo == "F")
            {
                txtIdPersona.MaxLength = 10;
            }
            if (tipo == "D")
            {
                txtIdPersona.MaxLength = 12;
            }
            if (tipo == "J")
            {
                txtIdPersona.MaxLength = 20;
            }
        }

        protected void ddlTipoPersona_SelectedIndexChanged(object sender, EventArgs e)
        {
            CambioLongitudTexto(ddlTipoPersona.SelectedItem.Value);    
        }

        protected void grdvFormularios_RowEditing1(object sender, GridViewEditEventArgs e)
        {

        }

   
    }
}