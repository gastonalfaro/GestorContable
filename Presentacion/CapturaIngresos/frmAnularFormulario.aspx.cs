using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services.Description;
using System.Data;
using LogicaNegocio.CapturaIngresos;
using LogicaNegocio.Seguridad;
using Presentacion.Compartidas;
using System.Web.UI.HtmlControls;

namespace Presentacion.CapturaIngresos
{
    public partial class frmAnularFormulario : BASE
    {
        //private string gstr_Usuario = "0114180568";
        private int gint_Debug = 0;
       //private static string lstr_separador_decimal = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
        public wrTributa.OrigenConsulta qry_Origen = new wrTributa.OrigenConsulta();
        
        public wrTributa.Service2 srv_Tributacion = new wrTributa.Service2();
        public DataTable tbl_Persona = new DataTable(); 
        private bool gbol_ConFirmaDigital; //= clsSesion.Current.gbol_FirmaDigital;
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
        //private string gstr_CorreoUsuario; // = clsSesion.Current.CorreoUsuario ;
        protected string gstr_CorreoUsuario
        {
            get
            {
                if (ViewState["gstr_CorreoUsuario"] == null)
                    ViewState["gstr_CorreoUsuario"] = string.Empty;
                return (string)ViewState["gstr_CorreoUsuario"];
            }
            set
            {
                ViewState["gstr_CorreoUsuario"] = value;
            }
        }
        //private string gstr_NombreUsuario; // = clsSesion.Current.NomUsuario;
        protected string gstr_NombreUsuario
        {
            get
            {
                if (ViewState["gstr_NombreUsuario"] == null)
                    ViewState["gstr_NombreUsuario"] = string.Empty;
                return (string)ViewState["gstr_NombreUsuario"];
            }
            set
            {
                ViewState["gstr_NombreUsuario"] = value;
            }
        }
        //private string gstr_TipoIdUsuario; // = clsSesion.Current.TipoIdUsuario;
        protected string gstr_TipoIdUsuario
        {
            get
            {
                if (ViewState["gstr_TipoIdUsuario"] == null)
                    ViewState["gstr_TipoIdUsuario"] = string.Empty;
                return (string)ViewState["gstr_TipoIdUsuario"];
            }
            set
            {
                ViewState["gstr_TipoIdUsuario"] = value;
            }
        }
        //private string gstr_CorreoNotificaUPR; //
        protected string gstr_CorreoNotificaUPR
        {
            get
            {
                if (ViewState["gstr_CorreoNotificaUPR"] == null)
                    ViewState["gstr_CorreoNotificaUPR"] = string.Empty;
                return (string)ViewState["gstr_CorreoNotificaUPR"];
            }
            set
            {
                ViewState["gstr_CorreoNotificaUPR"] = value;
            }
        } 


        //private DataTable gdat_Formularios = new DataTable();
        protected System.Data.DataTable gdat_Formularios
        {
            get
            {
                if (ViewState["gdat_Formularios"] == null)
                    ViewState["gdat_Formularios"] = new System.Data.DataTable();
                return (System.Data.DataTable)ViewState["gdat_Formularios"];
            }
            set
            {
                ViewState["gdat_Formularios"] = value;
            }
        }
        //private DataTable gdat_Pagos = new DataTable();
        protected System.Data.DataTable gdat_Pagos
        {
            get
            {
                if (ViewState["gdat_Pagos"] == null)
                    ViewState["gdat_Pagos"] = new System.Data.DataTable();
                return (System.Data.DataTable)ViewState["gdat_Pagos"];
            }
            set
            {
                ViewState["gdat_Pagos"] = value;
            }
        }
        //private DataTable gdat_TiposCambio = new DataTable();
        protected System.Data.DataTable gdat_TiposCambio
        {
            get
            {
                if (ViewState["gdat_TiposCambio"] == null)
                    ViewState["gdat_TiposCambio"] = new System.Data.DataTable();
                return (System.Data.DataTable)ViewState["gdat_TiposCambio"];
            }
            set
            {
                ViewState["gdat_TiposCambio"] = value;
            }
        }
        private wsCaptura.wsCapturaIngreso wsCapturaIngresos = new wsCaptura.wsCapturaIngreso();
        private wsSG.wsSistemaGestor wsSistemaGestor = new wsSG.wsSistemaGestor();        
        private int gint_NroFormulario = 0;
        private int gint_IdFormulario = 0;
        private DateTime gdt_FechaActual = new DateTime();
        //private decimal gdec_MontoColones = 0;
        protected decimal gdec_MontoColones
        {
            get
            {
                if (ViewState["gdec_MontoColones"] == null)
                    ViewState["gdec_MontoColones"] = 0;
                return Convert.ToDecimal(ViewState["gdec_MontoColones"]);
            }
            set
            {
                ViewState["gdec_MontoColones"] = value;
            }
        }
        //private decimal gdec_MontoDolares = 0;
        protected decimal gdec_MontoDolares
        {
            get
            {
                if (ViewState["gdec_MontoDolares"] == null)
                    ViewState["gdec_MontoDolares"] = 0;
                return Convert.ToDecimal(ViewState["gdec_MontoDolares"]);
            }
            set
            {
                ViewState["gdec_MontoDolares"] = value;
            }
        }
        //private decimal gdec_TipoCambioComp = 0;
        protected decimal gdec_TipoCambioComp
        {
            get
            {
                if (ViewState["gdec_TipoCambioComp"] == null)
                    ViewState["gdec_TipoCambioComp"] = 0;
                return Convert.ToDecimal(ViewState["gdec_TipoCambioComp"]);
            }
            set
            {
                ViewState["gdec_TipoCambioComp"] = value;
            }
        }
        //private decimal gdec_TipoCambioVent = 0;
        protected decimal gdec_TipoCambioVent
        {
            get
            {
                if (ViewState["gdec_TipoCambioVent"] == null)
                    ViewState["gdec_TipoCambioVent"] = 0;
                return Convert.ToDecimal(ViewState["gdec_TipoCambioVent"]);
            }
            set
            {
                ViewState["gdec_TipoCambioVent"] = value;
            }
        }
        //private decimal gdec_TipoCambioEur = 0;
        protected decimal gdec_TipoCambioEur
        {
            get
            {
                if (ViewState["gdec_TipoCambioEur"] == null)
                    ViewState["gdec_TipoCambioEur"] = 0;
                return Convert.ToDecimal(ViewState["gdec_TipoCambioEur"]);
            }
            set
            {
                ViewState["gdec_TipoCambioEur"] = value;
            }
        }

        int grid = 0;

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

        protected void Page_Load(object sender, EventArgs e)
        {
            txtAnno.Enabled = false;
            gstr_Usuario = clsSesion.Current.LoginUsuario;
            if (gint_Debug == 0)
            {
                gbol_ConFirmaDigital = clsSesion.Current.gbol_FirmaDigital;
                gstr_CorreoUsuario = clsSesion.Current.CorreoUsuario;
                gstr_NombreUsuario = clsSesion.Current.NomUsuario;
                gstr_TipoIdUsuario = clsSesion.Current.TipoIdUsuario;
            }
            else
            {
                gstr_Usuario = "0110370132";
                gbol_ConFirmaDigital = false;
                gstr_CorreoUsuario = "gabgarcia@gmail.com";
                gstr_NombreUsuario = "GABRIEL GARCIA GRANADOS";
                gstr_TipoIdUsuario = "F";
            }
            if (!IsPostBack)
            {              
                if (!string.IsNullOrEmpty(gstr_Usuario))
                {
                    if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "OBJ_CI"))
                        Response.Redirect("~/Principal.aspx", true);
                    else 
                    {
                        int lint_Anno = 0;
                        lint_Anno = DateTime.Today.Year;
                        txtAnno.Text = lint_Anno.ToString();
                        txtIdPersona.Text = gstr_Usuario;
                        ddlTipoPersona.SelectedValue = gstr_TipoIdUsuario;
                        //txtCorreo.Text = gstr_CorreoUsuario;
                        lblNombre.Text = gstr_NombreUsuario;
                        gdt_FechaActual = DateTime.Today;
                        try
                        {
                            //ddlListaFormularios.Items.Clear();
                            ActualizarFormulariosPorAnular(gstr_Usuario, gstr_TipoIdUsuario);
                            //ddlListaFormularios.DataBind();
                            //ddlListaFormularios.Items.Clear();
                            //ddlListaFormularios.Items.Insert(0, (new ListItem("-- Seleccione--", "0")));
                            
                            //incluido por Alvaro Moreira el 11/10/2018
                            CargarFormularios(gstr_Usuario, gstr_TipoIdUsuario, lint_Anno.ToString());

                            LimpiarFormulario();
                            //ActualizarTipoCambio(lstr_Fecha);
                            //ImprimeTipoCambio();
                            //ResetearPagosTemporales();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                            MostarMensaje(ex.ToString(), '1');
                        }
                        gdat_Formularios = wsCapturaIngresos.ConsultarFormulario(gstr_Usuario, gstr_TipoIdUsuario, ",PEN,IMP,").Tables[0];
                    
                    }
                }
                else
                    Response.Redirect("~/Login.aspx", true);
            }
        }

        private void CargarFormularios(string lstr_IdPersona, string lstr_TipoIdPersona, string lstr_Anno)
        {
            DataSet lds_formularios = wsSistemaGestor.uwsConsultarDinamico("SELECT * FROM ci.formulariosCapturasIngresos WHERE IdPersona = '" + lstr_IdPersona + "' AND TipoIdPersona = '" + lstr_TipoIdPersona + "' AND Anno = " + lstr_Anno + " AND Estado IN ('PEN','IMP');");
            if (lds_formularios.Tables.Count > 0)// && (ddlListaFormularios.Items.Count == 0))
            {
                DataTable ldt_formularios = lds_formularios.Tables["Table"];
                ddlListaFormularios.Items.Clear();
                foreach (DataRow fila in ldt_formularios.Rows) 
                {
                    ddlListaFormularios.Items.Add(new ListItem(fila["IdFormulario"].ToString() + " --- año: " + fila["Anno"].ToString(), fila["IdFormulario"].ToString() + "-" + fila["Anno"].ToString()));
                }

                //ddlListaFormularios.DataSource = ldt_formularios;
                //ddlListaFormularios.DataTextField = "IdFormulario";
                //ddlListaFormularios.DataValueField = "IdFormulario";
                ddlListaFormularios.Items.Insert(0, new ListItem("--- Elegir Opción---", ""));
                ddlListaFormularios.DataBind();
                //for (int i = 0; i < DDLMoneda.Items.Count; i++)
                //{
                //    DDLMoneda.Items[i].Value = DDLMoneda.Items[i].Value.Trim();
                //}
            }

        }
        protected void ddlTipoPersona_SelectedIndexChanged(object sender, EventArgs e)
        {
            CambioLongitudTexto(ddlTipoPersona.SelectedItem.Value);
            //ddlListaFormularios.Items.Clear();
            ActualizarFormulariosPorAnular(txtIdPersona.Text, ddlTipoPersona.SelectedValue);
            //ddlListaFormularios.DataBind();
            //ddlListaFormularios.Items.Clear();
            //ddlListaFormularios.Items.Insert(0, (new ListItem("-- Seleccione--", "0")));
            LimpiarFormulario();
            //grdDatos.DataBind();
            //this.CargaCamposFormularioSeleccionado(sender, e);
        }

        Boolean ValidaLongitudCedula(string str_tipo, out string str_Mensaje)
        {
            Boolean lbl_resultado = true;
            str_Mensaje = "";
            if (str_tipo == "F" && txtIdPersona.Text.Length != 10)
            {
                lbl_resultado = false;
                str_Mensaje = "Cédula debe tener 10 dígitos!";
            }
            if (str_tipo == "D" && txtIdPersona.Text.Length != 12)
            {

                lbl_resultado = false;
                str_Mensaje = "Dimex debe tener 12 dígitos!";
            }
            if (str_tipo == "J" && txtIdPersona.Text.Length != 20)
            {
                str_Mensaje = "Cédula Jurídica debe tener 20 dígitos!";
            }
            return lbl_resultado;
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

        protected void ddlListaFormularios_Load(object sender, EventArgs e)
        {

        }

        protected Boolean ValidaFormulario(out string str_Mensaje)
        {
            Boolean lbln_Resultado = true;
            str_Mensaje = "";
            if (string.IsNullOrEmpty(txtAnno.Text))
            {
                str_Mensaje = "Debe digitar un año del formulario";
                lbln_Resultado = false;
            }
            else if (string.IsNullOrEmpty(txtIdPersona.Text)){
                str_Mensaje = "Debe digitar la identificación.";
                lbln_Resultado = false;
            }
            else if (string.IsNullOrEmpty(ddlListaFormularios.Text)){
                str_Mensaje = "Debe seleccionar un formulario a anular.";
                lbln_Resultado = false;
            }    
            else if (string.IsNullOrEmpty(ddlTipoPersona.Text)){
                str_Mensaje = "Debe seleccionar el tipo de identificación.";
                lbln_Resultado = false;
            }     
            return lbln_Resultado;
        }

        protected void btnAnular_Click(object sender, EventArgs e)
        {
            string lstr_mensaje = "";
            
            if (ValidaFormulario(out lstr_mensaje))
            {

                if (ddlListaFormularios.SelectedIndex > 0) 
                {
                    string[] array = ddlListaFormularios.SelectedValue.ToString().Split('-');

                    lstr_mensaje = wsCapturaIngresos.CambiarEstadoFormulario(Convert.ToInt32(array[0]), Convert.ToInt32(txtAnno.Text), lblEdoFormulario.Text.Trim(), "ANU", "", gstr_Usuario);
                    if (lstr_mensaje.Contains("00"))
                    {
                        lstr_mensaje = "Transacción Procesada";
                    }
                    MostarMensaje(lstr_mensaje, '0');
                    //ddlListaFormularios.Items.Clear();

                    ActualizarFormulariosPorAnular(txtIdPersona.Text, ddlTipoPersona.SelectedValue);
                    wsSistemaGestor.uwsRegistrarAccionBitacoraCo("CI", clsSesion.Current.IdSesion, "Anular Formulario", lblEdoFormulario.Text.Trim() + " a ANU", array[0], "", "");
                    LimpiarFormulario();

                }
               
                //ddlListaFormularios.DataBind();
                //ddlListaFormularios.Items.Clear();
                //ddlListaFormularios.Items.Insert(0, (new ListItem("-- Seleccione--", "0")));
            }
            else
            {
                MostarMensaje(lstr_mensaje, '1');
            }
        }

        protected void CargaCamposFormulario(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlListaFormularios.SelectedValue) || ddlListaFormularios.SelectedValue == "0")
            {
                
                int lint_Anno = 0;
                lint_Anno = DateTime.Today.Year;
                
                lblEdoFormulario.Text = String.Empty;
                lblNomEstadoFormulario.Text = String.Empty;
                
            }
            else
            {
                
                CargarFormulario();
                
            }  
        }

        protected void CargarFormulario()
        {
            //ActualizarFormulariosPorAnular(txtIdPersona.Text, ddlTipoPersona.SelectedValue);
            if (!(string.IsNullOrEmpty(ddlListaFormularios.SelectedValue) || ddlListaFormularios.SelectedValue == "0"))
            {
                string[] array = ddlListaFormularios.SelectedValue.ToString().Split('-');
               
 
                gint_IdFormulario = Convert.ToInt32(array[0]);
                DataRow[] ldr_Formulario = gdat_Formularios.Select("IdFormulario = '" + array[0] +"' and Anno = '"+array[1]+"'");
           

                txtAnno.Text = ldr_Formulario[0]["Anno"].ToString();

                lblEdoFormulario.Text = ldr_Formulario[0]["Estado"].ToString();
                if (ldr_Formulario[0]["Estado"].ToString().Trim() == "PEN")
                {
                    lblNomEstadoFormulario.Text = "Creado";

                }
                else if (ldr_Formulario[0]["Estado"].ToString().Trim() == "IMP")
                {
                    lblNomEstadoFormulario.Text = "Impreso";

                }
            }
        }

        protected void ActualizarFormulariosPorAnular(string str_Usuario, string str_TipoId)
        {

            gdat_Formularios = wsCapturaIngresos.ConsultarFormulario(str_Usuario, str_TipoId, ",PEN,IMP,").Tables[0];


            ddlListaFormularios.Dispose();
            ddlListaFormularios.Items.Clear();
            ddlListaFormularios.DataBind();
            //ddlListaFormularios.Items.Clear();
            //if (gdat_Formularios.Rows.Count > 0)
            //{
            //    this.ddlListaFormularios.DataSource = gdat_Formularios;
            //    this.ddlListaFormularios.DataTextField = "IdFormulario";
            //    this.ddlListaFormularios.DataValueField = "IdFormulario";
            //}//if

            //Inhabilitado por Alvaro Moreira el 11/10/2018
            //ddlListaFormularios.Items.Clear();

            foreach (DataRow fila in gdat_Formularios.Rows)
            {
                ddlListaFormularios.Items.Add(new ListItem(fila["IdFormulario"].ToString() + " --- año: " + fila["Anno"].ToString(), fila["IdFormulario"].ToString() + "-" +fila["Anno"].ToString()));
            }
            ddlListaFormularios.Items.Insert(0, (new ListItem("-- Seleccione--", "0")));

        }

        protected void txtAnno_TextChanged(object sender, EventArgs e)
        {
            //ddlListaFormularios.Items.Clear();
            ActualizarFormulariosPorAnular(txtIdPersona.Text, ddlTipoPersona.SelectedValue);
            //ddlListaFormularios.DataBind();
            //ddlListaFormularios.Items.Clear();
            //ddlListaFormularios.Items.Insert(0, (new ListItem("-- Seleccione--", "0")));
            LimpiarFormulario();
        }

        protected void txtIdPersona_TextChanged(object sender, EventArgs e)
        {
            if (txtIdPersona.Text != "" && !string.IsNullOrEmpty(ddlTipoPersona.SelectedValue))
            {
                this.OcultarMensaje();
                string Mensaje = "";
                if (!this.ValidaLongitudCedula(ddlTipoPersona.SelectedValue.ToString(), out Mensaje))
                {
                    this.MostarMensaje(Mensaje, '1');
                }
                else
                {

                    if (ddlTipoPersona.SelectedValue == "F")
                    {
                        qry_Origen = wrTributa.OrigenConsulta.Fisico;
                    }
                    else if (ddlTipoPersona.SelectedValue == "J")
                    {
                        qry_Origen = wrTributa.OrigenConsulta.Juridico;
                    }
                    else
                    {
                        qry_Origen = wrTributa.OrigenConsulta.DIMEX;
                    }
                    ;

                    tbl_Persona = srv_Tributacion.ObtenerDatos(qry_Origen, txtIdPersona.Text, "", "", "", "", "", "");//string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                    if (tbl_Persona.Rows.Count > 0)
                    {

                        if (ddlTipoPersona.SelectedValue == "J")
                        {
                            lblNombre.Text = tbl_Persona.Select("1 = 1")[0]["NOMBRE"].ToString().Trim();
                        }
                        else
                        {
                            lblNombre.Text = tbl_Persona.Select("1 = 1")[0]["APELLIDO1"].ToString().Trim() + " " + tbl_Persona.Select("1 = 1")[0]["APELLIDO2"].ToString().Trim() + " " + tbl_Persona.Select("1 = 1")[0]["NOMBRE1"].ToString().Trim();
                        }
                        //ddlListaFormularios.Items.Clear();

                        //ddlListaFormularios.Items.Clear();
                        ActualizarFormulariosPorAnular(txtIdPersona.Text, ddlTipoPersona.SelectedValue);
                        //ddlListaFormularios.DataBind();
                        //ddlListaFormularios.Items.Clear();
                        //ddlListaFormularios.Items.Insert(0, (new ListItem("-- Seleccione--", "0")));                       
                        LimpiarFormulario();
                        //grdDatos.DataBind();
                        CargarFormulario();
                        //CargarPagosFormulario();
                        //ActualizarCamposFormulario();
                    }
                    else
                    {
                        ActualizarFormulariosPorAnular(txtIdPersona.Text, ddlTipoPersona.SelectedValue);
                        //ddlListaFormularios.DataBind();
                        //ddlListaFormularios.Items.Clear();
                        //ddlListaFormularios.Items.Insert(0, (new ListItem("-- Seleccione--", "0")));
                        LimpiarFormulario();
                        //DeshabilitaCamposFormulario();
                    }
                }
            }
            else
            {
                ActualizarFormulariosPorAnular(txtIdPersona.Text, ddlTipoPersona.SelectedValue);
                LimpiarFormulario();
                //HabilitaPago(gbol_ConFirmaDigital, txtIdPersona.Text, ddlInstUPR.SelectedValue, lblEdoFormulario.Text);
            }


        }

        protected void LimpiarFormulario()
        {
            //ReiniciarDataTableTemp();
            int lint_Anno = 0;
            lint_Anno = DateTime.Today.Year;
            txtAnno.Text = lint_Anno.ToString();
            //txtIdPersona.Text = String.Empty;
            //lblNombre.Text = String.Empty;
            //lblTotalDolares.Text = String.Empty;
            //lblTotalColones.Text = String.Empty;
            lblEdoFormulario.Text = String.Empty;
            lblNomEstadoFormulario.Text = String.Empty;
            //txtComprbante.Text = String.Empty;
            //txtFecha.Text = String.Empty;
            //txtBanco.Text = String.Empty;
            //ddlListaFormularios.Dispose();
            //ddlListaFormularios.Items.Clear();
            //ddlListaFormularios.DataBind();
            //ddlListaFormularios.Items.Clear();
           // ddlListaFormularios.Items.Insert(0, (new ListItem("-- Seleccione--", "0")));

            //txtMonto.Text = string.Empty;
            //txtObservaciones.Text = String.Empty;
            //refrescarGVPagos();

        }



        protected void ddlListaFormularios_SelectedIndexChanged(object sender, EventArgs e)
        {
                CargaCamposFormulario(sender, e);
                //LimpiarFormulario();
        }

    }
}