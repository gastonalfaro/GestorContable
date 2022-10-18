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
using LogicaNegocio.Contingentes;
using LogicaNegocio.Seguridad;
using Presentacion.Compartidas;
using Presentacion.wsDTR;
using System.Web.UI.HtmlControls;




namespace Presentacion.CapturaIngresos
{
    public partial class frmFormularioEdit : BASE
    {
        public frmFormularioEdit()
        {
            //if (gint_Debug == 0)
            //{
            //    gbol_ConFirmaDigital = clsSesion.Current.gbol_FirmaDigital;
            //    gstr_Usuario = clsSesion.Current.LoginUsuario;
            //    gstr_CorreoUsuario = clsSesion.Current.CorreoUsuario;
            //    gstr_NombreUsuario = clsSesion.Current.NomUsuario;
            //}
            //else
            //{
            //    gstr_Usuario = "";
            //    gbol_ConFirmaDigital = false;
            //    gstr_CorreoUsuario = "";
            //    gstr_NombreUsuario = "";
            //}
        }
        # region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        //private string gstr_Usuario = String.Empty;
        private static int gint_Debug = 1;

        //private static string gstr_Usuario = "0114180568";
        //private static bool gbol_ConFirmaDigital = false;
        public static wrTributa.OrigenConsulta qry_Origen = new wrTributa.OrigenConsulta();
        public static wrTributa.Service1 srv_Tributacion = new wrTributa.Service1();
        public static DataTable tbl_Persona = new DataTable();
        public static DataTable tbl_PersonaTramite = new DataTable(); 
        private static bool gbol_ConFirmaDigital; //= clsSesion.Current.gbol_FirmaDigital;
        private static string gstr_Usuario; //= clsSesion.Current.LoginUsuario;
        private static string gstr_CorreoUsuario; // = clsSesion.Current.CorreoUsuario ;
        private static string gstr_NombreUsuario; // = clsSesion.Current.NomUsuario;
        private static string gstr_TipoIdUsuario; // = clsSesion.Current.TipoIdUsuario;
        private static string gstr_IdFormulario;
        private static string gstr_IdFormulario_query;
        private static string gstr_AnnoFormulario;
        private static bool gbol_Error = false;
        private static bool gbol_ErrorDTR = false;

        private static DataTable gdat_Formularios = new DataTable();
        private static DataTable gdat_Pagos = new DataTable();
        private static DataTable gdat_PagosTemp = new DataTable();
        private static DataTable gdat_TiposCambio = new DataTable();
        private static wsCaptura.wsCapturaIngreso wsCapturaIngresos = new wsCaptura.wsCapturaIngreso();
        private static wsSG.wsSistemaGestor wsSistemaGestor = new wsSG.wsSistemaGestor();
        private static wsDTR.DTR wsDTR = new wsDTR.DTR();
        private static wsDTR.InformacionCuenta gICInfoCuenta = new wsDTR.InformacionCuenta();
        private static InformacionCuenta gdat_InformacionDestino = new InformacionCuenta();

        private static int gint_CrearFormulario = 0;
        private static int gint_IdFormulario = 0;
        private static DateTime gdt_FechaActual = new DateTime();
        private static int gint_AnnoActual;
        private static decimal gdec_MontoColones = 0;
        private static decimal gdec_MontoDolares = 0;
        private static decimal gdec_TipoCambioComp = 0;
        private static decimal gdec_TipoCambioVent = 0;
        private static decimal gdec_TipoCambioEur = 0;
        
        private static CL_RespuestaTransaccion[] gdat_EnvioContabilidad = new CL_RespuestaTransaccion[1];
        private static CL_Rastro gdat_Rastro = new CL_Rastro();
        private static CL_TransaccionDirecta[] gdat_Transaccion;// = new CL_TransaccionDirecta[];
        private static Moneda gdat_Moneda;

        # endregion

        protected void ObtenerFormularios(string str_Usuario, string str_TipoId)
        {
            if (string.IsNullOrEmpty(gstr_IdFormulario ))
                gdat_Formularios = wsCapturaIngresos.ConsultarFormulario(str_Usuario, str_TipoId, ",PEN,IMP,").Tables[0];
            else
                gdat_Formularios = wsCapturaIngresos.ConsultarFormulario("", "", "").Tables[0];
        }

        protected void ActualizarTipoCambio(string lstr_Fecha)
        {
            this.OcultarMensaje();
            //gdat_TiposCambio = wsSistemaGestor.uwsConsultarTiposCambio(null, Convert.ToDateTime(lstr_Fecha), null).Tables[0];
            gdat_TiposCambio = wsSistemaGestor.uwsConsultarTiposCambio(null, gdt_FechaActual.Date, null, "N").Tables[0];
            gdec_TipoCambioEur = Convert.ToDecimal(gdat_TiposCambio.Select("IdMoneda = 'EUR'")[0]["Valor"].ToString());
            gdec_TipoCambioComp = Convert.ToDecimal(gdat_TiposCambio.Select("TipoTransaccion = '317'")[0]["Valor"].ToString());
            gdec_TipoCambioVent = Convert.ToDecimal(gdat_TiposCambio.Select("TipoTransaccion = '318'")[0]["Valor"].ToString());                

        }

        protected void ImprimeTipoCambio()
        {
                lblEuro.Text = "$" + gdec_TipoCambioEur.ToString("0.##");
                lblCompraDol.Text = "₡" + gdec_TipoCambioComp.ToString("0.##");
                lblVentaDol.Text = "₡" + gdec_TipoCambioVent.ToString("0.##");
                ddlPeriodo.SelectedValue = gdt_FechaActual.Year.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
                this.OcultarMensaje();
                if (!IsPostBack)
                {
                    if (gint_Debug == 0)
                    {
                        gbol_ConFirmaDigital = clsSesion.Current.gbol_FirmaDigital;
                        gstr_Usuario = clsSesion.Current.LoginUsuario;
                        gstr_CorreoUsuario = clsSesion.Current.CorreoUsuario;
                        gstr_NombreUsuario = clsSesion.Current.NomUsuario;
                        gstr_TipoIdUsuario = clsSesion.Current.TipoIdUsuario;
                        gstr_IdFormulario = clsSesion.Current.IdFormularioCI;
                        gstr_AnnoFormulario = clsSesion.Current.AnnoFormularioCI;
                        gstr_IdFormulario_query = clsSesion.Current.IdFormularioCI;
                    }
                    else
                    {
                        gstr_Usuario = "0110370132";
                        gbol_ConFirmaDigital = false;
                        gstr_CorreoUsuario = "gabgarcia@gmail.com";
                        gstr_NombreUsuario = "GABRIEL GARCIA GRANADOS";
                        gstr_TipoIdUsuario = "F";
                        gstr_IdFormulario = "122";
                        gstr_AnnoFormulario = "2016";
                        gstr_IdFormulario_query = "122";
                    }
                    if (!string.IsNullOrEmpty(gstr_Usuario))
                    {
                        if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "OBJ_CI"))
                            Response.Redirect("~/Principal.aspx", true);
                        else
                        {

                            DeshabilitaCamposFormulario();
                            LimpiarFormulario();
                            grdDatos.DataBind();

                            if (gbol_ConFirmaDigital)
                            {
                                txtCuentaCliente.Visible = true;
                                lblCtaCliente.Visible = true;
                            }
                            else
                            {
                                txtCuentaCliente.Visible = false;
                                lblCtaCliente.Visible = false;
                            }

                            if (!(wsDTR.ServicioDisponible()))
                            {
                                gbol_ErrorDTR = true;
                            }
                            else
                            {
                                gbol_ErrorDTR = false;
                            }

                            //ggarcia, es mejor obligar a digitar la cedula
                            //txtIdPersona.Text = gstr_Usuario;
                            //lblNombre.Text = gstr_NombreUsuario;
                            //txtCorreo.Text = gstr_CorreoUsuario;
                            ddlTipoPersona.SelectedValue = gstr_TipoIdUsuario;


                            lblLetrasColones.Text = "";
                            lblLetrasDolares.Text = "";
                            gdt_FechaActual = DateTime.Today;
                            gint_AnnoActual = gdt_FechaActual.Year;
                            txtAnno.Text = gint_AnnoActual.ToString();
                            ddlPeriodo.ClearSelection();
                            ddlPeriodo.Items.Insert(0, (new ListItem("-- Seleccione--", "0")));//Insert(0, "--Seleccione--");
                            for (int i = 0; i < 11; i++)
                                ddlPeriodo.Items.Insert(i + 1, Convert.ToString(gint_AnnoActual - i));

                            string lstr_Fecha = "";
                            lstr_Fecha = gdt_FechaActual.Date.ToString();
                                                          
                                ObtenerFormularios(gstr_Usuario, gstr_TipoIdUsuario);
                                ActualizarTipoCambio(lstr_Fecha);
                                ImprimeTipoCambio();
                                ReiniciarDataTableTemp();
                                if (!string.IsNullOrEmpty(gstr_IdFormulario))
                                {
                                    this.ddlListaFormularios.Visible = false;
                                    this.lblNroFormulario.Text = gstr_IdFormulario;
                                    this.txtAnno.Visible = false;
                                    this.txtAnno.Text = gstr_AnnoFormulario;
                                    this.lblAnno.Text = gstr_AnnoFormulario;

                                    CargaCamposFormularioSeleccionado(sender, e);
                                    HabilitaPago(gbol_ConFirmaDigital, ddlTipoPersona.SelectedValue, txtIdPersona.Text, ddlTipoPersonaTramite.SelectedValue, txtIdPersonaTramite.Text, txtCuentaCliente.Text, ddlInstUPR.SelectedValue, lblEdoFormulario.Text);
                                }      
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

        private void MostrarElementos(string str_usuario)
        {
            DataSet ldt_PermisosUsuario = wsSistemaGestor.uwsConsultarPermisosUsuarios(str_usuario, "");

            for (int i = 0; ldt_PermisosUsuario.Tables["Table"].Rows.Count > i; i++)
            {
                string lstr_IdObjeto = ldt_PermisosUsuario.Tables["Table"].Rows[i]["IdObjeto"].ToString();
                string lstr_IdliEncabezado = "li" + lstr_IdObjeto;

                if ((bool)ldt_PermisosUsuario.Tables["Table"].Rows[i]["Consultar"])
                {
                    HtmlGenericControl hgcMenuEncabezado = (HtmlGenericControl)FindControlRecursive(Master.Page, lstr_IdliEncabezado);
                    if (hgcMenuEncabezado != null)
                    {
                        hgcMenuEncabezado.Style["visibility"] = "visible";
                        hgcMenuEncabezado.Visible = true;
                    }
                }
            }
        }

        public static Control FindControlRecursive(Control root, string id)
        {
            if (id == string.Empty)
                return null;

            if (root.ID == id)
                return root;

            foreach (Control c in root.Controls)
            {
                Control t = FindControlRecursive(c, id);
                if (t != null)
                {
                    return t;
                }
            }
            return null;
        }

        protected void ReiniciarDataTableTemp()
        {
            gdat_PagosTemp.Reset();
            gdat_PagosTemp.Columns.Add("IdPago");
            gdat_PagosTemp.Columns.Add("IdFormulario");
            gdat_PagosTemp.Columns.Add("Anno");
            gdat_PagosTemp.Columns.Add("FchIngreso");
            gdat_PagosTemp.Columns.Add("FchPago");
            gdat_PagosTemp.Columns.Add("IdInstitucion");
            gdat_PagosTemp.Columns.Add("Sociedad");
            gdat_PagosTemp.Columns.Add("IdServicio");
            gdat_PagosTemp.Columns.Add("Servicio");
            gdat_PagosTemp.Columns.Add("CtaMayor");
            gdat_PagosTemp.Columns.Add("IdOficina");
            gdat_PagosTemp.Columns.Add("Oficina");
            gdat_PagosTemp.Columns.Add("IdPosPre");
            gdat_PagosTemp.Columns.Add("IdReservaPresupuestaria");
            gdat_PagosTemp.Columns.Add("NroExpediente");
            gdat_PagosTemp.Columns.Add("IdMoneda");
            gdat_PagosTemp.Columns.Add("Moneda");
            gdat_PagosTemp.Columns.Add("Monto");
            gdat_PagosTemp.Columns.Add("Periodo");
            gdat_PagosTemp.Columns.Add("Estado");
            gdat_PagosTemp.Columns.Add("UsrCreacion");
        }

        protected void ActualizarMontosFormulario()
        {
                lblTotalColones.Text = "";
                lblMontoDolares.Text = "";
                lblLetrasDolares.Text = "";
                lblLetrasColones.Text = "";
                for (int i = 0; i < gdat_PagosTemp.Rows.Count; i++)
                {
                    if (gdat_PagosTemp.Rows[i]["IdMoneda"].ToString().Trim() == "CRC")
                    {
                        gdec_MontoColones += Convert.ToDecimal(gdat_PagosTemp.Rows[i]["Monto"].ToString());
                    }
                    else
                    {
                        gdec_MontoColones += (gdec_TipoCambioComp * Convert.ToDecimal(gdat_PagosTemp.Rows[i]["Monto"].ToString()));
                    }
                }

                if (gdec_TipoCambioComp != 0)
                {
                    gdec_MontoDolares = gdec_MontoColones / gdec_TipoCambioComp;
                }
                lblLetrasDolares.Text = wsCapturaIngresos.uwsConvertirMontoStringLetras(gdec_MontoDolares.ToString(CultureInfo.InvariantCulture), "dólares", "");
                lblLetrasColones.Text = wsCapturaIngresos.uwsConvertirMontoStringLetras(gdec_MontoColones.ToString(CultureInfo.InvariantCulture), "colones", "");
                lblTotalColones.Text = gdec_MontoColones.ToString("N2");
                lblMontoDolares.Text = gdec_MontoDolares.ToString("N2");
                gdec_MontoColones = 0;
                gdec_MontoDolares = 0;



        }

        protected void ActualizarTotalesFormularioSeleccionado()
        {
            this.OcultarMensaje();
            string str_IdFormulario;
            string str_Anno;
            if (string.IsNullOrEmpty(gstr_IdFormulario))
            {
                gstr_IdFormulario_query = ddlListaFormularios.SelectedValue;
                //string[] tokens = ddlListaFormularios.SelectedValue.Split(',');
                //str_Anno = tokens[0];
                //str_IdFormulario = tokens[1];
                str_Anno = txtAnno.Text;
                str_IdFormulario = ddlListaFormularios.SelectedValue;
            }
            else
            {
                gstr_IdFormulario_query = gstr_IdFormulario;
                str_IdFormulario = gstr_IdFormulario;
                str_Anno = gstr_AnnoFormulario;
            }

            if (!string.IsNullOrEmpty(gstr_IdFormulario_query) && gstr_IdFormulario_query != "0" && gstr_IdFormulario_query != "--")
            {
                if (gstr_IdFormulario_query != "0")
                {
                    for (int i = 0; i < gdat_Pagos.Rows.Count; i++)
                    {
                        if (gdat_Pagos.Rows[i]["IdMoneda"].ToString().Trim() == "CRC")
                        {
                            gdec_MontoColones += Convert.ToDecimal(gdat_Pagos.Rows[i]["Monto"].ToString());
                        }
                        else
                        {
                            gdec_MontoColones += (gdec_TipoCambioComp * Convert.ToDecimal(gdat_Pagos.Rows[i]["Monto"].ToString()));
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < gdat_PagosTemp.Rows.Count; i++)
                    {
                        if (gdat_PagosTemp.Rows[i]["IdMoneda"].ToString().Trim() == "CRC")
                        {
                            gdec_MontoColones += Convert.ToDecimal(gdat_PagosTemp.Rows[i]["Monto"].ToString());
                        }
                        else
                        {
                            gdec_MontoColones += (gdec_TipoCambioComp * Convert.ToDecimal(gdat_PagosTemp.Rows[i]["Monto"].ToString()));
                        }
                    }
                }
                if (gdec_TipoCambioComp != 0)
                    gdec_MontoDolares = gdec_MontoColones / gdec_TipoCambioComp;
                lblLetrasDolares.Text = wsCapturaIngresos.uwsConvertirMontoStringLetras(gdec_MontoDolares.ToString(CultureInfo.InvariantCulture), "dólares", "");
                lblLetrasColones.Text = wsCapturaIngresos.uwsConvertirMontoStringLetras(gdec_MontoColones.ToString(CultureInfo.InvariantCulture), "colones", "");
                lblTotalColones.Text = gdec_MontoColones.ToString("N2");
                lblMontoDolares.Text = gdec_MontoDolares.ToString("N2");
                gdec_MontoColones = 0;
                gdec_MontoDolares = 0;
            }
        }

        protected void CargarFormulario(string str_IdFormulario, string str_Anno)
        {
            this.OcultarMensaje();
            if (!string.IsNullOrEmpty(str_IdFormulario))
            {
                //string lstr_NroFormulario = str_IdFormulario;//ddlListaFormularios.SelectedValue;
                gint_IdFormulario = Convert.ToInt32(str_IdFormulario);
                DataRow[] ldr_Formulario = gdat_Formularios.Select("IdFormulario = '" + str_IdFormulario + "' AND Anno = '" + str_Anno +"'");
                //El Id y el tipo de id no se llenan porque son usados como filtro.
                lblNombre.Text = ldr_Formulario[0]["NomPersona"].ToString();
                txtIdPersona.Text = ldr_Formulario[0]["IdPersona"].ToString();//ojo 23-2-2016
                if (!string.IsNullOrEmpty (ldr_Formulario[0]["TipoIdPersonaTramite"].ToString().Trim()))
                    ddlTipoPersonaTramite.SelectedValue = ldr_Formulario[0]["TipoIdPersonaTramite"].ToString().Trim();
                txtIdPersonaTramite.Text = ldr_Formulario[0]["IdPersonaTramite"].ToString();
                lblNombreTramite.Text = ldr_Formulario[0]["NomPersonaTramite"].ToString();
                txtAnno.Text = ldr_Formulario[0]["Anno"].ToString();
                txtCorreo.Text = ldr_Formulario[0]["Correo"].ToString();
                //la reserva depende del servicio del pago y no del formulario
                //txtReserva.Text = ldr_Formulario[0]["IdReservaPresupuestaria"].ToString();
                txtExpediente.Text = ldr_Formulario[0]["NroExpediente"].ToString();
                txtDescripcion.Text = ldr_Formulario[0]["Descripcion"].ToString();
                txtCuentaCliente.Text = ldr_Formulario[0]["CtaCliente"].ToString();
                txtDireccion.Text = ldr_Formulario[0]["Direccion"].ToString();
                lblFechaIngreso.Text = ldr_Formulario[0]["FchIngreso"].ToString();

                ddlInstUPR.SelectedValue = ldr_Formulario[0]["IdSociedadGL"].ToString();
                ddlOficinas.DataBind();
                ddlServicios.DataBind();
                if (ldr_Formulario[0]["IdOficina"].ToString().Trim()!="0")
                    ddlOficinas.SelectedValue = ldr_Formulario[0]["IdOficina"].ToString();

                lblEdoFormulario.Text = ldr_Formulario[0]["Estado"].ToString();
                if (ldr_Formulario[0]["Estado"].ToString().Trim() == "PEN")
                {
                    lblNomEstadoFormulario.Text = "Creado";
                    HabilitaCamposFormulario();
                    btnImprimir.Visible = false;
                    btnAgregarPago.Visible = true;
                    btnInsertarFormulario.Visible = true;
                    btnPrepararImprimir.Visible = true;
                    grdDatos.Enabled = true;

                    lblFormPagos.Visible = true;
                    //lblMoneda.Visible = true;
                    lblMonto.Visible = true;
                    lblPeriodo.Visible = true;
                    lblServicio.Visible = true;
                    //lblBanco.Visible = true;
                    lblReserva.Visible = true;
                    txtReserva.Visible = true;
                    txtExpediente.Visible = true;
                    //ddlMoneda.Visible = true;
                    txtMonto.Visible = true;
                    ddlPeriodo.Visible = true;
                    //ddlBanco.Visible = true;
                    ddlServicios.Visible = true;
                }
                else if (ldr_Formulario[0]["Estado"].ToString().Trim() == "IMP")
                {
                    lblNomEstadoFormulario.Text = "Impreso";
                    DeshabilitaCamposFormulario();
                    btnImprimir.Visible = true;
                    btnAgregarPago.Visible = false;
                    btnInsertarFormulario.Visible = false;
                    btnPrepararImprimir.Visible = false;
                    grdDatos.Enabled = false;

                    lblFormPagos.Visible = false;
                    //lblMoneda.Visible = false;
                    lblMonto.Visible = false;
                    lblPeriodo.Visible = false;
                    lblServicio.Visible = false;
                    lblBanco.Visible = false;
                    lblReserva.Visible = false;
                    txtReserva.Visible = false;
                    txtExpediente.Visible = false;
                    //ddlMoneda.Visible = false;
                    txtMonto.Visible = false;
                    ddlPeriodo.Visible = false;
                    ddlBanco.Visible = false;
                    ddlServicios.Visible = false;
                }
                CargarPagosFormulario();
            }
        }

        protected void CargarPagosFormulario()
        {
            try
            {
                string str_IdFormulario;
                string str_Anno;
                if (string.IsNullOrEmpty(gstr_IdFormulario))
                {
                    gstr_IdFormulario_query = ddlListaFormularios.SelectedValue;
                    //string[] tokens = ddlListaFormularios.SelectedValue.Split(',');
                    //str_Anno = tokens[0];
                    //str_IdFormulario = tokens[1];
                    str_Anno = txtAnno.Text;
                    str_IdFormulario = ddlListaFormularios.SelectedValue;
                }
                else
                {
                    gstr_IdFormulario_query = gstr_IdFormulario;
                    str_IdFormulario = gstr_IdFormulario;
                    str_Anno = gstr_AnnoFormulario;
                }
                DataTable ldat_Temporal = new DataTable();
                ldat_Temporal = wsCapturaIngresos.ConsultarPago(Convert.ToInt32(gstr_IdFormulario_query), Convert.ToInt16(txtAnno.Text) ).Tables[0];
                DataRow[] ldar_Temporal;
                ldar_Temporal = ldat_Temporal.Select("Estado = 'A'");
                gdat_Pagos = ldar_Temporal.CopyToDataTable();
                grdDatos.DataSource = gdat_Pagos;
                grdDatos.DataBind();                
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.ToString());
                MostarMensaje(ex.ToString(), '1');
                gdat_Pagos.Clear();
                grdDatos.DataSource = gdat_Pagos;
                grdDatos.DataBind(); 
            }
         }

        protected void CargaCamposFormularioSeleccionado(object sender, EventArgs e)
        {
            string str_IdFormulario;
            string str_Anno;
            if (string.IsNullOrEmpty(gstr_IdFormulario))
            {
                gstr_IdFormulario_query = ddlListaFormularios.SelectedValue;
                //string[] tokens = ddlListaFormularios.SelectedValue.Split(',');
                //str_Anno = tokens[0];
                //str_IdFormulario = tokens[1];
                str_Anno = txtAnno.Text;
                str_IdFormulario = ddlListaFormularios.SelectedValue;
            }
            else
            {
                gstr_IdFormulario_query = gstr_IdFormulario;
                str_IdFormulario = gstr_IdFormulario;
                str_Anno = gstr_AnnoFormulario;
            }

            if (string.IsNullOrEmpty(str_IdFormulario) || str_IdFormulario == "0" || str_IdFormulario == "--")
            {
                ReiniciarDataTableTemp();
                int lint_Anno = 0;
                lint_Anno = DateTime.Today.Year;
                txtAnno.Text = lint_Anno.ToString();
                txtCorreo.Text = String.Empty;
                txtReserva.Text = String.Empty;
                txtExpediente.Text = String.Empty;
                txtDescripcion.Text = String.Empty;
                txtCuentaCliente.Text = String.Empty;
                txtDireccion.Text = String.Empty;
                lblEdoFormulario.Text = String.Empty;
                lblFechaIngreso.Text = String.Empty;
                lblNomEstadoFormulario.Text = String.Empty;
                DeshabilitaCamposFormulario();
                refrescarGVPagos();
            }
            else
            {
                HabilitaCamposFormulario();
                CargarFormulario(str_IdFormulario, str_Anno);
                CargarPagosFormulario();
                ActualizarTotalesFormularioSeleccionado();
            }            
        }

        protected void ddlListaFormularios_Load(object sender, EventArgs e)
        {

        }

        protected void txtIdPersona_TextChanged(object sender, EventArgs e)
        {
            LimpiarFormulario();
            if (txtIdPersona.Text != "" && !string.IsNullOrEmpty(ddlTipoPersona.SelectedValue))
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
                    else { 
                      lblNombre.Text = tbl_Persona.Select("1 = 1")[0]["APELLIDO1"].ToString().Trim() + " " + tbl_Persona.Select("1 = 1")[0]["APELLIDO2"].ToString().Trim() + " " + tbl_Persona.Select("1 = 1")[0]["NOMBRE1"].ToString().Trim();
                    }
                    txtCorreo.Text = string.Empty;
                    ddlListaFormularios.Items.Clear();
                    HabilitaCamposFormulario();
                    ObtenerFormularios(txtIdPersona.Text, ddlTipoPersona.SelectedValue);
                    ddlListaFormularios.DataBind();
                    LimpiarFormulario();
                    grdDatos.DataBind();
                    CargaCamposFormularioSeleccionado(sender, e);
                    HabilitaPago(gbol_ConFirmaDigital, ddlTipoPersona.SelectedValue, txtIdPersona.Text, ddlTipoPersonaTramite.SelectedValue, txtIdPersonaTramite.Text, txtCuentaCliente.Text, ddlInstUPR.SelectedValue, lblEdoFormulario.Text);
                }
                else
                {
                    ddlListaFormularios.Items.Clear();
                    LimpiarFormulario();
                    DeshabilitaCamposFormulario();
                    ddlListaFormularios.DataBind();
                    LimpiarFormulario();
                    grdDatos.DataBind();
                    this.CargaCamposFormularioSeleccionado(sender, e);
                }

            }
                //if (txtIdPersona.Text == "114180568")
            //{
            //    lblNombre.Text = "Steven Vega Vidal";
            //    txtCorreo.Text = "stevega90@gmail.com";
            //}
        }

        protected void ddlInstUPR_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlOficinas.ClearSelection();
            }
            else 
            {

                ddlOficinas.Items.Insert(0, (new ListItem("-- Seleccione--", "0")));//Insert(0, "<-- Select -->");
 
            }
        }

        protected void lbtnInsertOrders_Click(object sender, EventArgs e)
        {

        }

        protected void btnFuncion_Click(object sender, EventArgs e)
        {

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
            ObtenerFormularios(txtIdPersona.Text, ddlTipoPersona.SelectedValue);
            ddlListaFormularios.DataBind();
            LimpiarFormulario();
            grdDatos.DataBind();
            CargaCamposFormularioSeleccionado(sender, e);
            HabilitaPago(gbol_ConFirmaDigital, ddlTipoPersona.SelectedValue, txtIdPersona.Text, ddlTipoPersonaTramite.SelectedValue, txtIdPersonaTramite.Text, txtCuentaCliente.Text, ddlInstUPR.SelectedValue, lblEdoFormulario.Text);
        }

        protected void refrescarGVPagos()
        {
            if (!string.IsNullOrEmpty(gstr_IdFormulario_query) && gstr_IdFormulario_query != "0" && gstr_IdFormulario_query != "--")
            {
                //grdDatos.Dispose();
                grdDatos.DataSource = gdat_Pagos;
                grdDatos.DataBind();
            }
            else
            {
                //grdDatos.Dispose();
                if (gdat_PagosTemp.Rows.Count != 0)
                {
                    grdDatos.DataSource = gdat_PagosTemp;
                    grdDatos.DataBind();
                }
                else
                {
                    grdDatos.Dispose();
                    grdDatos.DataBind();
                }
            }
        }

        protected void BorrarRubro(object sender, GridViewCommandEventArgs e)
        {
            this.OcultarMensaje();
            int lint_Indice = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName == "Borrar")
            {
                if (!string.IsNullOrEmpty(gstr_IdFormulario_query) && gstr_IdFormulario_query != "0" && gstr_IdFormulario_query != "--")
                {
                    int lint_IdFormulario = 0;
                    int lint_Anno = 0;
                    int lint_IdPago = 0;
                    string lstr_Estado = String.Empty;
                    DateTime ldt_FchModificacion = new DateTime();


                    lint_IdFormulario = Convert.ToInt32(gdat_Pagos.Rows[lint_Indice][0].ToString());
                    lint_Anno = Convert.ToInt32(gdat_Pagos.Rows[lint_Indice][14].ToString());
                    lint_IdPago = Convert.ToInt32(gdat_Pagos.Rows[lint_Indice][1].ToString());
                    lstr_Estado = gdat_Pagos.Rows[lint_Indice][12].ToString();
                    ldt_FchModificacion = Convert.ToDateTime(gdat_Pagos.Rows[lint_Indice][13].ToString());

                    wsCapturaIngresos.DeshabilitarPago(lint_IdFormulario, lint_Anno, lint_IdPago, lstr_Estado, gstr_Usuario, ldt_FchModificacion);
                    gdat_Pagos.Rows.RemoveAt(lint_Indice);
                        
                }
                else
                {
                    gdat_PagosTemp.Rows.RemoveAt(lint_Indice);
                }
                refrescarGVPagos();
                ActualizarTotalesFormularioSeleccionado();
            }
        }

        protected void AlmacenarPagosNuevoForm()
        {
            int lint_Pago = 0;
            for (int i = 0; i < gdat_PagosTemp.Rows.Count; i++)
            {
                wsCapturaIngresos.CrearPago(
                    gint_IdFormulario,
                    Convert.ToInt32(gdat_PagosTemp.Rows[i]["Periodo"].ToString()),
                    0,
                    Convert.ToDateTime("01/01/1900"),
                    Convert.ToDateTime("01/01/1900"),
                    gdat_PagosTemp.Rows[i]["IdInstitucion"].ToString(),
                    gdat_PagosTemp.Rows[i]["IdServicio"].ToString(),
                    "-",
                    gdat_PagosTemp.Rows[i]["IdOficina"].ToString(),
                    "-",
                    gdat_PagosTemp.Rows[i]["IdReservaPresupuestaria"].ToString(),
                    gdat_PagosTemp.Rows[i]["NroExpediente"].ToString(),
                    gdat_PagosTemp.Rows[i]["IdMoneda"].ToString(),
                    Convert.ToDecimal(gdat_PagosTemp.Rows[i]["Monto"].ToString()),
                    gdat_PagosTemp.Rows[i]["Periodo"].ToString(),
                    gdat_PagosTemp.Rows[i]["UsrCreacion"].ToString());
            }
        }

        protected void GuardarPagoSinFormulario()
        {
            decimal ldec_Monto = 0;
            if (txtMonto.Text != "")
            {
                ldec_Monto = Convert.ToDecimal(txtMonto.Text);
            }
            if (ldec_Monto != 0)
            {
                string lstr_Institucion = ddlInstUPR.SelectedItem.Text;
                string lstr_Servicios = ddlServicios.SelectedItem.Text;
                string lstr_Oficinas = ddlOficinas.SelectedItem.Text;
                string lstr_Moneda = ddlMoneda.SelectedItem.Text;

                if (ddlInstUPR.SelectedIndex < 1)
                {
                    lstr_Institucion = String.Empty;
                }
                if (ddlServicios.SelectedIndex < 1)
                {
                    lstr_Servicios = String.Empty;
                }
                if (ddlOficinas.SelectedIndex < 1)
                {
                    lstr_Oficinas = String.Empty;
                }
                if (ddlMoneda.SelectedIndex < 1)
                {
                    lstr_Moneda = String.Empty;
                }

                gdat_PagosTemp.Rows.Add(0, 0, Convert.ToInt32(ddlPeriodo.Text), Convert.ToDateTime("01/01/1900"),
                    Convert.ToDateTime("01/01/1900"), Convert.ToString(ddlInstUPR.SelectedValue), lstr_Institucion,
                    Convert.ToString(ddlServicios.SelectedValue), lstr_Servicios, "-", Convert.ToString(ddlOficinas.SelectedValue),
                    lstr_Oficinas, "-", txtReserva.Text, txtExpediente.Text, Convert.ToString(ddlMoneda.SelectedValue), lstr_Moneda, Convert.ToDecimal(txtMonto.Text),
                    ddlPeriodo.Text, "A", gstr_Usuario);
                refrescarGVPagos();
                ActualizarTotalesFormularioSeleccionado();
            }
        }

        protected void GuardarPagoConFormulario()
        {
            decimal ldec_Monto = 0;
            int lint_IdPagoTemp = 0;
            String[] lstr_resultado = new String[3];
            int lint_Pago = 0;
            if (txtMonto.Text != "")
            {
                ldec_Monto = Convert.ToDecimal(txtMonto.Text);
            }
            if (ldec_Monto != 0)
            {
                lstr_resultado = wsCapturaIngresos.CrearPago(gint_IdFormulario, Convert.ToInt32(ddlPeriodo.Text), 0, Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/1900"), Convert.ToString(ddlInstUPR.SelectedValue), Convert.ToString(ddlServicios.SelectedValue), "-", Convert.ToString(ddlOficinas.SelectedValue), "-", 
                    txtReserva.Text, txtExpediente.Text, Convert.ToString(ddlMoneda.SelectedValue), Convert.ToDecimal(txtMonto.Text), ddlPeriodo.Text, gstr_Usuario);

            }
            CargarPagosFormulario();
            ActualizarTotalesFormularioSeleccionado();
        }

        protected void btnFuncion_Click1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(gstr_IdFormulario_query)  || gstr_IdFormulario_query == "0"  || gstr_IdFormulario_query == "--"  )
            {
                GuardarPagoSinFormulario();
            }
            else
            {
                GuardarPagoConFormulario();
            }
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
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
            else if (string.IsNullOrEmpty(txtIdPersona.Text))
            {
                str_Mensaje = "Debe digitar la identificación.";
                lbln_Resultado = false;
            }
            //else if (string.IsNullOrEmpty(ddlListaFormularios.Text))
            //{
            //    str_Mensaje = "Debe seleccionar un formulario a anular.";
            //    lbln_Resultado = false;
            //}
            else if (string.IsNullOrEmpty(ddlTipoPersona.Text))
            {
                str_Mensaje = "Debe seleccionar el tipo de identificación.";
                lbln_Resultado = false;
            }
            else if (string.IsNullOrEmpty(ddlInstUPR.Text))
            {
                str_Mensaje = "Debe seleccionar la institución.";
                lbln_Resultado = false;
            }
            else if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                str_Mensaje = "Debe digitar la Descripción";
                lbln_Resultado = false;
            }
            else if (!string.IsNullOrEmpty(txtCuentaCliente.Text) && !gbol_ErrorDTR)
            {
                if (wsCapturaIngresos.uwsCalcularDígitoVerificador(txtCuentaCliente.Text) == false)
                {
                    str_Mensaje = "La cuenta cliente no es válida.";
                    lbln_Resultado = false;
                }
            }
            return lbln_Resultado;
        }

        protected void btnInsertarFormulario_Click(object sender, EventArgs e)
        {
            try
            {
                gbol_Error = false;
                string str_IdFormulario;
                string str_Anno;
                if (string.IsNullOrEmpty(gstr_IdFormulario))
                {
                    gstr_IdFormulario_query = ddlListaFormularios.SelectedValue;
                    //string[] tokens = ddlListaFormularios.SelectedValue.Split(',');
                    //str_Anno = tokens[0];
                    //str_IdFormulario = tokens[1];
                    str_Anno = txtAnno.Text;
                    str_IdFormulario = ddlListaFormularios.SelectedValue;
                }
                else
                {
                    gstr_IdFormulario_query = gstr_IdFormulario;
                    str_IdFormulario = gstr_IdFormulario;
                    str_Anno = gstr_AnnoFormulario;
                }

                this.OcultarMensaje();
                string lstr_mensaje = "";
                string lstr_Moneda = Convert.ToString(ddlMoneda.SelectedValue); //ddlMoneda.SelectedItem.Text;
                decimal ldec_Monto;
                if (lstr_Moneda.Trim() == "CRC")
                {
                    ldec_Monto = Convert.ToDecimal(lblTotalColones.Text);
                }
                else
                {
                    ldec_Monto = Convert.ToDecimal(lblMontoDolares.Text);
                }
                if (ValidaFormulario(out lstr_mensaje))
                {
                    String[] lstr_result = new String[3];
                    DateTime ldt_FchIngreso = new DateTime();
                    if (gstr_IdFormulario_query == "0")
                    {
                        ldt_FchIngreso = DateTime.Today;

                        lstr_result = wsCapturaIngresos.CrearFormulario(Convert.ToInt32(gstr_IdFormulario_query), Convert.ToInt32(txtAnno.Text), ddlTipoPersona.SelectedValue, txtIdPersona.Text, lblNombre.Text, ddlTipoPersonaTramite.SelectedValue, txtIdPersonaTramite.Text, lblNombreTramite.Text
                                , txtCorreo.Text, ddlInstUPR.SelectedValue, ddlOficinas.SelectedValue, ddlBanco.SelectedValue, "", "", txtExpediente.Text, txtDescripcion.Text, txtCuentaCliente.Text, txtDireccion.Text
                                , ldt_FchIngreso, Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/1900"), "PEN", "-", lstr_Moneda, ldec_Monto, string.Empty, gstr_Usuario);
                
                        if (lstr_result[0].ToString().Equals("True"))
                        {
                            MostarMensaje(lstr_result[1].ToString(), '0');
                            lblNroFormulario.Text = lstr_result[2].ToString();//Convert.ToString(gint_IdFormulario);
                            gint_IdFormulario = Convert.ToInt32(lblNroFormulario.Text);
                            btnPrepararImprimir.Visible = true;
                            AlmacenarPagosNuevoForm();
                            gdat_PagosTemp.Reset();
                            ddlListaFormularios.Items.Clear();
                            ddlListaFormularios.DataBind();
                            ddlListaFormularios.Items.Insert(1, (new ListItem("-- Selecionar opción --", "0")));
                            gbol_Error = false;
                        }
                        else
                        {
                            MessageBox.Show("Error: " + lstr_result[1].ToString()); 
                            MostarMensaje("Error: " + lstr_result[1].ToString(), '1');
                            gbol_Error = true;
                        }

                    }
                    else
                    {
                        ldt_FchIngreso = Convert.ToDateTime(gdat_Formularios.Select("IdFormulario = '" + gstr_IdFormulario_query + "'")[0]["FchIngreso"]);

                        lstr_result = wsCapturaIngresos.CrearFormulario(Convert.ToInt32(gstr_IdFormulario_query), Convert.ToDateTime(txtAnno.Text).Year, ddlTipoPersona.SelectedValue, txtIdPersona.Text, lblNombre.Text, ddlTipoPersonaTramite.SelectedValue, txtIdPersonaTramite.Text, lblNombreTramite.Text
                                , txtCorreo.Text, ddlInstUPR.SelectedValue, ddlOficinas.SelectedValue, ddlBanco.SelectedValue, "", "", txtExpediente.Text, txtDescripcion.Text, txtCuentaCliente.Text, txtDireccion.Text
                                , ldt_FchIngreso, Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/1900"), "PEN", "-", lstr_Moneda, ldec_Monto, string.Empty, gstr_Usuario);

             
                        if (lstr_result[0].ToString().Equals("True"))
                        {
                            MostarMensaje(lstr_result[1].ToString(), '0');
                            //lblNroFormulario.Text = lstr_result[2].ToString();//Convert.ToString(gint_IdFormulario);
                            ObtenerFormularios(txtIdPersona.Text, ddlTipoPersona.Text);
                            gbol_Error = false;
                        }
                        else
                        {
                            MessageBox.Show("Error: " + lstr_result[1].ToString());
                            MostarMensaje("Error: " + lstr_result[1].ToString(), '1');
                            gbol_Error = true;
                        }
                    }
                }
                else
                {
                    MessageBox.Show(lstr_mensaje);
                    MostarMensaje(lstr_mensaje, '1');
                    gbol_Error = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MostarMensaje(ex.ToString(), '1');
                gbol_Error = true;
            }
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


        protected void btnPrepararImprimir_Click(object sender, EventArgs e)
        {
            this.OcultarMensaje();
            string str_IdFormulario;
            string str_Anno;
            if (string.IsNullOrEmpty(gstr_IdFormulario))
            {
                gstr_IdFormulario_query = ddlListaFormularios.SelectedValue;
                //string[] tokens = ddlListaFormularios.SelectedValue.Split(',');
                //str_Anno = tokens[0];
                //str_IdFormulario = tokens[1];
                str_Anno = txtAnno.Text;
                str_IdFormulario = ddlListaFormularios.SelectedValue;
            }
            else
            {
                gstr_IdFormulario_query = gstr_IdFormulario;
                str_IdFormulario = gstr_IdFormulario;
                str_Anno = gstr_AnnoFormulario;
            }

            if (gint_IdFormulario == 0)
            {
                MessageBox.Show("Error: Antes de imprimir debe guardar el Formulario"); 
                MostarMensaje("Error: Antes de imprimir debe guardar el Formulario", '1');
            }
            else
            {
                string lstr_mensaje = "";
                string lstr_Moneda = Convert.ToString(ddlMoneda.SelectedValue); //ddlMoneda.SelectedItem.Text;
                decimal ldec_Monto;
                if (lstr_Moneda.Trim() == "CRC")
                {
                    ldec_Monto = Convert.ToDecimal(lblTotalColones.Text);
                }
                else
                {
                    ldec_Monto = Convert.ToDecimal(lblMontoDolares.Text);
                }
                if (ValidaFormulario(out lstr_mensaje))
                {
                lblNomEstadoFormulario.Text = "Impreso";
                lblEdoFormulario.Text = "IMP";

                
                    String[] lstr_result = new String[3];
                    DateTime ldt_FchIngreso = new DateTime();
                    ldt_FchIngreso = Convert.ToDateTime(gdat_Formularios.Select("IdFormulario = '" + gstr_IdFormulario_query + "'")[0]["FchIngreso"]);

                    lstr_result = wsCapturaIngresos.CrearFormulario(gint_IdFormulario, Convert.ToInt32(txtAnno.Text), ddlTipoPersona.SelectedValue, txtIdPersona.Text, lblNombre.Text, ddlTipoPersonaTramite.SelectedValue, txtIdPersonaTramite.Text, lblNombreTramite.Text
                            , txtCorreo.Text, ddlInstUPR.SelectedValue, ddlOficinas.SelectedValue, ddlBanco.SelectedValue, "", "", txtExpediente.Text, txtDescripcion.Text, txtCuentaCliente.Text, txtDireccion.Text
                            , ldt_FchIngreso, Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/1900"), lblEdoFormulario.Text, "-", lstr_Moneda, ldec_Monto, string.Empty, gstr_Usuario);
                    
                    if (lstr_result[0].ToString().Equals("True"))
                    {
                        MostarMensaje(lstr_result[1].ToString(), '0');
                        btnImprimir.Visible = true;
                        btnAgregarPago.Visible = false;
                        btnInsertarFormulario.Visible = false;
                        btnPrepararImprimir.Visible = false;
                        grdDatos.Enabled = false;
                        lblFormPagos.Visible = false;
                        //lblMoneda.Visible = false;
                        lblMonto.Visible = false;
                        lblPeriodo.Visible = false;
                        lblServicio.Visible = false;
                        lblBanco.Visible = false;
                        lblReserva.Visible = false;
                        txtReserva.Visible = false;
                        txtExpediente.Visible = false;
                        //ddlMoneda.Visible = false;
                        txtMonto.Visible = false;
                        ddlPeriodo.Visible = false;
                        ddlBanco.Visible = false;
                        ddlServicios.Visible = false;
                        HabilitaPago(gbol_ConFirmaDigital, ddlTipoPersona.SelectedValue, txtIdPersona.Text, ddlTipoPersonaTramite.SelectedValue, txtIdPersonaTramite.Text, txtCuentaCliente.Text, ddlInstUPR.SelectedValue, lblEdoFormulario.Text);
                        if (gbol_ConFirmaDigital)
                        {
                            btnPagoDTR.Visible = true;
                        }
                        //AlmacenarPagosNuevoForm();
                        DeshabilitaCamposFormulario();
                    }
                    else
                    {
                        MessageBox.Show("Error: " + lstr_result[1].ToString()); 
                        MostarMensaje("Error: " + lstr_result[1].ToString(), '1');
                    }

                    //gdat_PagosTemp.Reset();
             
                //Response.Redirect("frmFormularioEdit.aspx");
                }
                else
                {
                    MessageBox.Show(lstr_mensaje);
                    MostarMensaje(lstr_mensaje, '1');
                }
            }
        }

        protected void HabilitaPago(Boolean bln_ConFirmaDigital, string str_TipoIdPago, string str_IdPersonaTramite, string str_TipoIdTramitador, string str_IdTramitador, string str_CtaCliente, string str_SociedadGL, string str_Estado)
        {
            //El botón de pagar solo se habilita:
            //1) Si estoy ingresando con FD
            //2) Si estoy pagando algo propio
            //3) Si estoy tramitando un pago de una empresa juridica y estoy domiciliado en la cuenta y tengo FD
            //4) Si no le estoy pagando a adscritas
            //5) Si está en Estado Pendiente(Creado) o Impreso
            btnPagoDTR.Visible = false;
            if (!(str_SociedadGL == "0"))
            {
                Boolean lbln_EsAdscrita = false;
                Boolean lbln_EsAutorizado = false;

                DataSet ds_SociedadFi = wsSistemaGestor.uwsConsultarSociedadesGLSociedadesFi(str_SociedadGL, "CI", "");

                if (ds_SociedadFi.Tables.Count > 0 && ds_SociedadFi.Tables["Table"].Rows.Count > 0)
                {
                    if (ds_SociedadFi.Tables["Table"].Rows[0]["IdSociedadFi"].ToString().StartsWith("A"))
                    {
                        lbln_EsAdscrita = true;
                    }
                }

                if (str_TipoIdPago == "J")
                {
                    DataSet ds_Autorizado = wsSistemaGestor.uwsConsultarEmpresasAutorizados(str_IdPersonaTramite, str_TipoIdTramitador, str_IdTramitador, "", "", str_CtaCliente, "A");
                    if (ds_Autorizado.Tables.Count > 0 && ds_Autorizado.Tables["Table"].Rows.Count > 0)
                    {
                        lbln_EsAutorizado = true;
                    }

                }
                if (!lbln_EsAdscrita && bln_ConFirmaDigital && (str_Estado == "PEN" || str_Estado == "IMP") && ((gstr_Usuario == str_IdPersonaTramite && str_IdTramitador == str_IdPersonaTramite) || (str_TipoIdPago == "J" && gstr_Usuario == str_IdTramitador && lbln_EsAutorizado)))
                    btnPagoDTR.Visible = true;
                if (!(wsDTR.ServicioDisponible()))
                {
                    gbol_ErrorDTR = true;
                    MessageBox.Show("No se encuentra disponible el servicio de pago por DTR");
                    btnPagoDTR.Visible = false;
                }
                else
                {
                    gbol_ErrorDTR = false;
                }


            }
        }

        protected void HabilitaCamposFormulario()
        {
            //ReiniciarDataTableTemp();
            //ddlTipoPersona.Enabled = true;
            ddlInstUPR.Enabled = true;
            ddlOficinas.Enabled = true;
            //txtIdPersona.Enabled = false;
            //txtIdPersona.ReadOnly = false;

            txtCorreo.ReadOnly = false;
            txtReserva.ReadOnly = true;
            txtExpediente.ReadOnly = true;
            txtDescripcion.ReadOnly = false;
            //txtAnno.ReadOnly = false;
            txtCuentaCliente.ReadOnly = false;
            txtDireccion.ReadOnly = false;
            ddlBanco.Enabled = true;
            ddlServicios.Enabled = true;
            ddlMoneda.Enabled = true;
            //ddlPeriodo.ReadOnly = false;
            txtMonto.ReadOnly = false;            

        }


        protected void DeshabilitaCamposFormulario()
        {
            //ReiniciarDataTableTemp();
            //ddlTipoPersona.Enabled = false;
            ddlInstUPR.Enabled = false;
            ddlOficinas.Enabled = false;
            //txtIdPersona.Enabled = false;
            //txtIdPersona.ReadOnly = true;

            txtCorreo.ReadOnly = true;
            txtReserva.ReadOnly = true;
            txtExpediente.ReadOnly = true;
            txtDescripcion.ReadOnly = true;
            //txtAnno.ReadOnly = true;
            txtCuentaCliente.ReadOnly = true;
            txtDireccion.ReadOnly = true;
            ddlBanco.Enabled = false;
            ddlServicios.Enabled = false;
            ddlMoneda.Enabled = false;
            //ddlPeriodo.ReadOnly = true;
            txtMonto.ReadOnly = true;

        }

        protected void LimpiarFormulario()
        {
            ReiniciarDataTableTemp();
            int lint_Anno = 0;
            lint_Anno = DateTime.Today.Year;
            txtAnno.Text = lint_Anno.ToString();
            txtCorreo.Text = String.Empty;
            txtReserva.Text = String.Empty;
            txtExpediente.Text = String.Empty;
            txtDescripcion.Text = String.Empty;
            txtCuentaCliente.Text = String.Empty;
            txtDireccion.Text = String.Empty;
            lblEdoFormulario.Text = String.Empty;
            lblNomEstadoFormulario.Text = String.Empty;
            lblTotalColones.Text = String.Empty ;
            lblMontoDolares.Text = String.Empty;
            lblLetrasColones.Text = String.Empty;
            lblLetrasDolares.Text = String.Empty;
            lblFormPagos.Visible = true;
            //lblMoneda.Visible = true;
            lblMonto.Visible = true;
            lblPeriodo.Visible = true;
            lblServicio.Visible = true;
            //lblBanco.Visible = true;
            lblReserva.Visible = true;
            txtReserva.Visible = true;
            txtExpediente.Visible = true;
            ddlMoneda.Visible = true;
            txtMonto.Visible = true;
            ddlPeriodo.Visible = true;
            //ddlBanco.Visible = true;
            ddlServicios.Visible = true;

            DeshabilitaCamposFormulario();
            btnImprimir.Visible = false;
            btnAgregarPago.Visible = false;
            btnInsertarFormulario.Visible = false;
            btnPrepararImprimir.Visible = false;
            grdDatos.Enabled = false;
            //refrescarGVPagos();

            ddlTipoPersonaTramite.ClearSelection();
            txtIdPersonaTramite.Text = "";
            lblNombreTramite.Text = "";
        }
        protected void btnAgregarPago_Click(object sender, EventArgs e)
        {
            string str_IdFormulario;
            string str_Anno;
            if (string.IsNullOrEmpty(gstr_IdFormulario))
            {
                gstr_IdFormulario_query = ddlListaFormularios.SelectedValue;
                //string[] tokens = ddlListaFormularios.SelectedValue.Split(',');
                //str_Anno = tokens[0];
                //str_IdFormulario = tokens[1];
                str_Anno = txtAnno.Text;
                str_IdFormulario = ddlListaFormularios.SelectedValue;
            }
            else
            {
                gstr_IdFormulario_query = gstr_IdFormulario;
                str_IdFormulario = gstr_IdFormulario;
                str_Anno = gstr_AnnoFormulario;
            }

            if (gstr_IdFormulario_query == "0")
            {
                GuardarPagoSinFormulario();
            }
            else
            {
                GuardarPagoConFormulario();
            }
        }

        protected void ddlListaFormularios_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargaCamposFormularioSeleccionado(sender, e);
            HabilitaPago(gbol_ConFirmaDigital, ddlTipoPersona.SelectedValue, txtIdPersona.Text, ddlTipoPersonaTramite.SelectedValue, txtIdPersonaTramite.Text, txtCuentaCliente.Text, ddlInstUPR.SelectedValue, lblEdoFormulario.Text);
        }

        protected void ddlPeriodo_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ddlServicios_SelectedIndexChanged(object sender, EventArgs e)
        {
            //se habilita la reserva solo para los servicios de devolucion de anticipos
            //if (ddlServicios.SelectedValue.ToString() == "104" || ddlServicios.SelectedValue.ToString() == "105" ||
            //    ddlServicios.SelectedValue.ToString() == "106" || ddlServicios.SelectedValue.ToString() == "107")
            if (ddlServicios.SelectedItem.Text.Contains("Devolución anticipos de v" ))
            {
                txtReserva.Visible = true;
                txtReserva.ReadOnly = false;
            }
            else
            {
                txtReserva.Visible = false;
                txtReserva.Text = "";
                txtReserva.ReadOnly = true;
            }
            //if (ddlServicios.SelectedItem.Text.Contains("Cobro Judicial"))
            //{
            //    txtExpediente.Visible = true;
            //    txtExpediente.ReadOnly = false;
            //}
            //else
            //{
            //    txtExpediente.Visible = false;
            //    txtExpediente.Text = "";
            //    txtExpediente.ReadOnly = true;
            //}
        }

        protected void txtMonto_TextChanged(object sender, EventArgs e)
        {
            //decimal numero = Convert.ToDecimal(txtMonto.Text);
            //txtMonto.Text = numero.ToString("N2");
        }

        protected void btnPagoDTR_Click(object sender, EventArgs e)
        {
            //paso 1 validar si la cuenta cliente le pertenece
            //ojo, canal 5 es Contabilidad Nacional
            btnInsertarFormulario_Click(sender, e);
            if (gbol_Error == false)
            {
                string resultado = "";
                try
                {
                    this.OcultarMensaje();
                    gdat_InformacionDestino = wsDTR.ObtenerInformacionDestino(txtCuentaCliente.Text, txtIdPersona.Text);
                    if (wsCapturaIngresos.uwsCalcularDígitoVerificador(txtCuentaCliente.Text) == true)
                    {
                        if (wsDTR.ServicioDisponible() == true)
                        {
                            if (gdat_InformacionDestino.codMotivoRechazoField != 0)
                            {
                                MessageBox.Show("Cuenta Cliente No Pertence a la Cédula del Usuario."); 
                                MostarMensaje("Cuenta Cliente No Pertence a la Cédula del Usuario.", '1');
                            }
                            else
                            {
                                //Llenado de clase Rastro que ingresa como parametro
                                gdat_Rastro.Canal = 5;
                                gdat_Rastro.IP = clsSesion.Current.IPSesion;
                                gdat_Rastro.Usuario = clsSesion.Current.LoginUsuario;

                                gdat_Transaccion = new CL_TransaccionDirecta[1];

                                CL_Cliente cliente = new CL_Cliente();
                                cliente.CC = txtCuentaCliente.Text;
                                cliente.Identificacion = txtIdPersona.Text;
                                cliente.Nombre = lblNombre.Text;

                                //LLenado de la clase Transaccion que ingresa como parametro

                                CL_TransaccionDirecta temp = new CL_TransaccionDirecta();

                                temp.Destino = cliente;
                                temp._Documento = "-";
                                temp.CentroCosto = 2646;
                                switch (ddlMoneda.SelectedItem.Text)
                                {
                                    case "Colones":
                                        gdat_Moneda = Moneda.Colones;
                                        break;
                                    case "Dolares":
                                        gdat_Moneda = Moneda.Dolares;
                                        break;
                                    case "Euros":
                                        gdat_Moneda = Moneda.Euros;
                                        break;
                                    default:
                                        Console.WriteLine("Moneda Invalida");
                                        break;
                                }
                                temp.CodigoMoneda = gdat_Moneda;
                                temp.Descripcion = txtDescripcion.Text;
                                temp.EntidadOrigen = 152;
                                temp.Monto = Convert.ToDecimal(lblTotalColones.Text);
                                temp.NodoIntegracion = 1;
                                temp.Servicio = "";
                                temp.CodigoConcepto = 1;
                                temp.CodigoReferencia = "";
                                temp.IdRelacionCliente = lblNroFormulario.Text;
                                temp.MontoComisionCliente = Convert.ToDecimal("0");
                                temp.MontoComisionRepresentada = Convert.ToDecimal("0");

                                gdat_Transaccion[0] = temp;
                                gdat_EnvioContabilidad = wsDTR.EnvioDirectoContabilidad(gdat_Rastro, gdat_Transaccion);

                                if (gdat_EnvioContabilidad[0].MotivoRechazo == 0)
                                {
                                    DataSet InfoFormulario = wsCapturaIngresos.ConsultarFormulariosCapturaIngresos(Convert.ToInt32(lblNroFormulario.Text), Convert.ToInt16(txtAnno.Text), "", "", "", "", "", "", "", "", "");
                                    resultado = wsCapturaIngresos.CambiarEstadoFormulario(Convert.ToInt32(lblNroFormulario.Text), Convert.ToInt32(txtAnno.Text), InfoFormulario.Tables["Table"].Rows[0]["Estado"].ToString(), "PAG", Convert.ToString(gdat_EnvioContabilidad[0].CodigoReferencia), clsSesion.Current.LoginUsuario);
                                    if (resultado == "00")
                                    {
                                        MostarMensaje("Transacción Exitosa. Número de referencia: " + gdat_EnvioContabilidad[0].CodigoReferencia, '2');
                                        lblNomEstadoFormulario.Text = "Pagado";
                                        lblEdoFormulario.Text = "PAG";
                                        HabilitaPago(gbol_ConFirmaDigital, ddlTipoPersona.SelectedValue, txtIdPersona.Text, ddlTipoPersonaTramite.SelectedValue, txtIdPersonaTramite.Text, txtCuentaCliente.Text, ddlInstUPR.SelectedValue, lblEdoFormulario.Text);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Error en la Aplicación: " + resultado);
                                        MostarMensaje("Error en la Aplicación: " + resultado, '1');
                                    }
                                }
                                else
                                {
                                    DataSet MotivoRechazo = wsSistemaGestor.uwsConsultarOpcionesCatalogo("41", "", gdat_EnvioContabilidad[0].MotivoRechazo.ToString(), "");
                                    MessageBox.Show("Transacción Rechazada, Motivo: " + gdat_EnvioContabilidad[0].MotivoRechazo + " " + MotivoRechazo.Tables["Table"].Rows[0]["NomOpcion"].ToString()); 
                                    MostarMensaje("Transacción Rechazada, Motivo: " + gdat_EnvioContabilidad[0].MotivoRechazo + " " + MotivoRechazo.Tables["Table"].Rows[0]["NomOpcion"].ToString(), '1');
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Servicio No se Encuentra Disponible.");
                            MostarMensaje("Servicio No se Encuentra Disponible.", '1');
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cuenta Cliente No Válida.");
                        MostarMensaje("Cuenta Cliente No Válida.", '1');
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Durante el Procesamiento del Pago: " + ex.Message.ToString()); 
                    MostarMensaje("Error Durante el Procesamiento del Pago: " + ex.Message.ToString(), '1');
                }
            }
        }

        protected void ddlInstUPR_SelectedIndexChanged1(object sender, EventArgs e)
        {
            HabilitaPago(gbol_ConFirmaDigital, ddlTipoPersona.SelectedValue, txtIdPersona.Text, ddlTipoPersonaTramite.SelectedValue, txtIdPersonaTramite.Text, txtCuentaCliente.Text, ddlInstUPR.SelectedValue, lblEdoFormulario.Text);
        }

        protected void ddlBanco_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlOficinas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlOficinas.SelectedItem.Text.Contains("Cobro Judicial"))
            {
                txtExpediente.ReadOnly = false;
            }
            else
            {
                txtExpediente.Text = "";
                txtExpediente.ReadOnly = true;
            }
        }

        protected void txtIdPersonaTramite_TextChanged(object sender, EventArgs e)
        {
            if (txtIdPersonaTramite.Text != "" && !string.IsNullOrEmpty(ddlTipoPersonaTramite.SelectedValue))
            {
                if (ddlTipoPersonaTramite.SelectedValue == "F")
                {
                    qry_Origen = wrTributa.OrigenConsulta.Fisico;
                }
                else if (ddlTipoPersonaTramite.SelectedValue == "J")
                {
                    qry_Origen = wrTributa.OrigenConsulta.Juridico;
                }
                else
                {
                    qry_Origen = wrTributa.OrigenConsulta.DIMEX;
                }
                ;

                tbl_PersonaTramite = srv_Tributacion.ObtenerDatos(qry_Origen, txtIdPersonaTramite.Text, "", "", "", "", "", "");//string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                if (tbl_PersonaTramite.Rows.Count > 0)
                {
                    if (ddlTipoPersonaTramite.SelectedValue == "J")
                    {
                        lblNombreTramite.Text = tbl_PersonaTramite.Select("1 = 1")[0]["NOMBRE"].ToString().Trim();
                    }
                    else
                    {
                        lblNombreTramite.Text = tbl_PersonaTramite.Select("1 = 1")[0]["APELLIDO1"].ToString().Trim() + " " + tbl_PersonaTramite.Select("1 = 1")[0]["APELLIDO2"].ToString().Trim() + " " + tbl_PersonaTramite.Select("1 = 1")[0]["NOMBRE1"].ToString().Trim();
                    }
                    HabilitaPago(gbol_ConFirmaDigital, ddlTipoPersona.SelectedValue, txtIdPersona.Text, ddlTipoPersonaTramite.SelectedValue, txtIdPersonaTramite.Text, txtCuentaCliente.Text, ddlInstUPR.SelectedValue, lblEdoFormulario.Text);
                }
                else
                {
                    lblNombreTramite.Text = "";
                    //LimpiarFormulario();
                    //grdDatos.DataBind();                    
                }

            }
        }

        protected void ddlTipoPersonaTramite_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtCuentaCliente_TextChanged(object sender, EventArgs e)
        {
            if (wsCapturaIngresos.uwsCalcularDígitoVerificador(txtCuentaCliente.Text) == false)
            {

                MessageBox.Show("La cuenta cliente no es válida.");
            }
            else
            {

                if (!gbol_ErrorDTR)
                {
                    //gICInfoCuenta = wsDTR.ObtenerInformacionDestino(txtCuentaCliente.Text, txtIdPersona.Text);
                    gdat_InformacionDestino = wsDTR.ObtenerInformacionDestino(txtCuentaCliente.Text, txtIdPersona.Text);

                    if (gdat_InformacionDestino.codMotivoRechazoField != 0)
                    {
                        MostarMensaje("Cuenta Cliente No Pertence a la Cédula del Usuario.", '1');
                        MessageBox.Show("Cuenta Cliente No Pertence a la Cédula del Usuario.");
                    }
                }
            }
        }

        protected void txtReserva_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtReserva.Text))
            {
                DataSet lds_Reserva = ws_SGService.uwsConsultarReservas(txtReserva.Text, "", "", "", "", "");
                //solo itero si el dataset tiene registros
                if (lds_Reserva.Tables.Count == 0 || string.IsNullOrEmpty(lds_Reserva.Tables["Table"].Rows[0]["IdReserva"].ToString()))
                {
                    MessageBox.Show("Advertencia: No se encontró la reserva!");
                }
            }
        }

        protected Boolean ValidaDescripcion(out string str_Mensaje)
        {
            Boolean lbln_Resultado = true;
            str_Mensaje = "";
            if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                str_Mensaje = "Debe digitar la Descripción";
                lbln_Resultado = false;
            }
            return lbln_Resultado;
        }

        protected void txtDescripcion_TextChanged(object sender, EventArgs e)
        {
            gbol_Error = false;
            this.OcultarMensaje();
            string lstr_mensaje = "";
            if (!this.ValidaDescripcion(out lstr_mensaje))
            {
                MostarMensaje(lstr_mensaje, '1');
                MessageBox.Show(lstr_mensaje);
                gbol_Error = true;
            }
        }
    }
}