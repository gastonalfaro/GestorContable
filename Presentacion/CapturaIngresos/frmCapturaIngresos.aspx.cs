﻿using System;
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
//using Presentacion.wsDTR;
using Presentacion.wsDTR1;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;
using System.Diagnostics;
using System.Reflection;
using Presentacion.Compartidas.VisorReportes;
using System.Configuration;
using DRIVER_XML_XML_XML;
using System.Threading;

namespace Presentacion.CapturaIngresos
{
    public partial class frmCapturaIngresos : BASE
    {        
        # region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
       //private static string lstr_separador_decimal = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
        internal protected object DataTemp { set { this.Session["DATA" + clsSesion.Current.LoginUsuario] = value; } get { return this.Session["DATA" + clsSesion.Current.LoginUsuario]; } }
        //private string gstr_Usuario = String.Empty;
        private int gint_Debug = 0;

        //private string gstr_Usuario = "0114180568";
        //private bool gbol_ConFirmaDigital = false;
        public wrTributa.OrigenConsulta qry_Origen = new wrTributa.OrigenConsulta();
        public wrTributa.Service2 srv_Tributacion = new wrTributa.Service2();
        //public DataTable tbl_Persona = new DataTable();
        protected DataTable tbl_Persona
        {
            get
            {
                if (ViewState["tbl_Persona"] == null)
                    ViewState["tbl_Persona"] = new DataTable();
                return (DataTable)ViewState["tbl_Persona"];
            }
            set
            {
                ViewState["tbl_Persona"] = value;
            }
        }
        //public DataTable tbl_PersonaTramite = new DataTable();
        protected DataTable tbl_PersonaTramite
        {
            get
            {
                if (ViewState["tbl_PersonaTramite"] == null)
                    ViewState["tbl_PersonaTramite"] = new DataTable();
                return (DataTable)ViewState["tbl_PersonaTramite"];
            }
            set
            {
                ViewState["tbl_PersonaTramite"] = value;
            }
        }
        //private bool gbol_ConFirmaDigital; //= clsSesion.Current.gbol_FirmaDigital;
        protected Boolean gbol_ConFirmaDigital
        {
            get
            {
                if (ViewState["gbol_ConFirmaDigital"] == null)
                    ViewState["gbol_ConFirmaDigital"] = false;
                return Convert.ToBoolean(ViewState["gbol_ConFirmaDigital"]);
            }
            set
            {
                ViewState["gbol_ConFirmaDigital"] = value;
            }
        }
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
        //private string gstr_IdFormulario;
        protected string gstr_IdFormulario
        {
            get
            {
                if (ViewState["gstr_IdFormulario"] == null)
                    ViewState["gstr_IdFormulario"] = null;
                return (string)ViewState["gstr_IdFormulario"];
            }
            set
            {
                ViewState["gstr_IdFormulario"] = value;
            }
        }
        //private string gstr_IdFormulario_query;
        protected string gstr_IdFormulario_query
        {
            get
            {
                if (ViewState["gstr_IdFormulario_query"] == null)
                    ViewState["gstr_IdFormulario_query"] = null;
                return (string)ViewState["gstr_IdFormulario_query"];
            }
            set
            {
                ViewState["gstr_IdFormulario_query"] = value;
            }
        }
        //private string gstr_AnnoFormulario;
        protected string gstr_AnnoFormulario
        {
            get
            {
                if (ViewState["gstr_AnnoFormulario"] == null)
                    ViewState["gstr_AnnoFormulario"] = null;
                return (string)ViewState["gstr_AnnoFormulario"];
            }
            set
            {
                ViewState["gstr_AnnoFormulario"] = value;
            }
        }
        //private string gstr_Letras;
        protected string gstr_Letras
        {
            get
            {
                if (ViewState["gstr_Letras"] == null)
                    ViewState["gstr_Letras"] = null;
                return (string)ViewState["gstr_Letras"];
            }
            set
            {
                ViewState["gstr_Letras"] = value;
            }
        }
        //private bool gbol_Error = false;
        protected Boolean gbol_Error
        {
            get
            {
                if (ViewState["gbol_Error"] == null)
                    ViewState["gbol_Error"] = false;
                return Convert.ToBoolean(ViewState["gbol_Error"]);
            }
            set
            {
                ViewState["gbol_Error"] = value;
            }
        }
        //private bool gbol_ErrorDTR = false;
        protected Boolean gbol_ErrorDTR
        {
            get
            {
                if (ViewState["gbol_ErrorDTR"] == null)
                    ViewState["gbol_ErrorDTR"] = false;
                return Convert.ToBoolean(ViewState["gbol_ErrorDTR"]);
            }
            set
            {
                ViewState["gbol_ErrorDTR"] = value;
            }
        } 

        //private DataTable gdat_Formularios = new DataTable();
        protected DataTable gdat_Formularios
        {
            get
            {
                if (ViewState["gdat_Formularios"] == null)
                    ViewState["gdat_Formularios"] = new DataTable();
                return (DataTable)ViewState["gdat_Formularios"];
            }
            set
            {
                ViewState["gdat_Formularios"] = value;
            }
        }
        //private DataTable gdat_Pagos = new DataTable();
        protected DataTable gdat_Pagos
        {
            get
            {
                if (ViewState["gdat_Pagos"] == null)
                    ViewState["gdat_Pagos"] = new DataTable();
                return (DataTable)ViewState["gdat_Pagos"];
            }
            set
            {
                ViewState["gdat_Pagos"] = value;
            }
        }
        //private DataTable gdat_PagosTemp = new DataTable();
        protected DataTable gdat_PagosTemp
        {
            get
            {
                if (ViewState["gdat_PagosTemp"] == null)
                    ViewState["gdat_PagosTemp"] = new DataTable();
                return (DataTable)ViewState["gdat_PagosTemp"];
            }
            set
            {
                ViewState["gdat_PagosTemp"] = value;
            }
        }
        //private DataTable gdat_TiposCambio = new DataTable();
        protected DataTable gdat_TiposCambio
        {
            get
            {
                if (ViewState["gdat_TiposCambio"] == null)
                    ViewState["gdat_TiposCambio"] = new DataTable();
                return (DataTable)ViewState["gdat_TiposCambio"];
            }
            set
            {
                ViewState["gdat_TiposCambio"] = value;
            }
        }
        protected DataTable ldt_Instituciones
        {
            get
            {
                if (ViewState["ldt_Instituciones"] == null)
                    ViewState["ldt_Instituciones"] = new DataTable();
                return (DataTable)ViewState["ldt_Instituciones"];
            }
            set
            {
                ViewState["ldt_Instituciones"] = value;
            }
        }
        private wsCaptura.wsCapturaIngreso wsCapturaIngresos = new wsCaptura.wsCapturaIngreso();
        private wsSG.wsSistemaGestor wsSistemaGestor = new wsSG.wsSistemaGestor();

        // wsDTR viejo *************************************************************************
        //private wsDTR.DTR wsDTR = new wsDTR.DTR();
        //private wsDTR.InformacionCuenta gICInfoCuenta = new wsDTR.InformacionCuenta();
        //private wsDTR.InformacionCuenta gdat_InformacionDestino = new wsDTR.InformacionCuenta();
        //**************************************************************************************

        // wsDTR nuevo *************************************************************************

        private wsDTR1.DTR wsDTRNuevo = new wsDTR1.DTR();
        
        private wsDTR1.InformacionCuenta gICInfoCuenta1 = new wsDTR1.InformacionCuenta();
        private wsDTR1.InformacionCuenta gdat_InformacionDestino1 = new wsDTR1.InformacionCuenta();
        //private wsDTR1.Cliente cliente1 = new wsDTR1.Cliente();
        
           
        //**************************************************************************************

        private int gint_CrearFormulario = 0;
        //private int gint_IdFormulario = 0;
        protected int gint_IdFormulario
        {
            get
            {
                if (ViewState["gint_IdFormulario"] == null)
                    ViewState["gint_IdFormulario"] = 0;
                return (int)ViewState["gint_IdFormulario"];
            }
            set
            {
                ViewState["gint_IdFormulario"] = value;
            }
        } 
        //private DateTime gdt_FechaActual = new DateTime();
        protected DateTime gdt_FechaActual
        {
            get
            {
                if (ViewState["gdt_FechaActual"] == null)
                    ViewState["gdt_FechaActual"] = new DateTime();
                return Convert.ToDateTime(ViewState["gdt_FechaActual"]);
            }
            set
            {
                ViewState["gdt_FechaActual"] = value;
            }
        }
        //private int gint_AnnoActual;
        protected int gint_AnnoActual
        {
            get
            {
                if (ViewState["gint_AnnoActual"] == null)
                    ViewState["gint_AnnoActual"] = 0;
                return (int)ViewState["gint_AnnoActual"];
            }
            set
            {
                ViewState["gint_AnnoActual"] = value;
            }
        } 
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
        
        //wsDTR viejo ***********************************************************************************
        //private CL_RespuestaTransaccion[] gdat_EnvioContabilidad = new CL_RespuestaTransaccion[1];
        //private CL_Rastro gdat_Rastro = new CL_Rastro();
        //private CL_TransaccionDirecta[] gdat_Transaccion;// = new CL_TransaccionDirecta[];
        //private Moneda gdat_Moneda;
        //************************************************************************************************

        //wsDTR nuevo *************************************************************************************************
        private wsDTR1.RespuestaTransaccion[] gdat_EnvioContabilidad1 = new wsDTR1.RespuestaTransaccion[1];
        private wsDTR1.Rastro gdat_Rastro1 = new wsDTR1.Rastro();        
        private wsDTR1.Transferencia[] gdat_Tranferencia1;        
        //**************************************************************************************************************

        #region RAMSES VARIABLES FIRMA FORMULARIO
        List<INFO_PAGO> LISTA_DE_PAGOS = null;
        #endregion 

        # endregion

        public void mtr_msg(String msj)
        {
            Response.Write(String.Format("<script>alert('{0}')</script>", msj));
        }//FUNCION

        public XML__DRIVER get_xml_driver_empty()
        {
            PARA p = new PARA("", "");
            TRAMITA t = new TRAMITA("", "");
            INFO_PAGO i_p = new INFO_PAGO("", "", "", "");
            INFO_FORMULARIO i_f = new INFO_FORMULARIO(p, t, "", "", "", "", "", "", "", "", "", "");
            return new XML__DRIVER(i_f, i_p);
        }//FUNCION

        protected void btn_save_xml_Click(object sender, EventArgs e)
        {
            #region RAMSES SALVAR XML
            XML__DRIVER driverXML = get_xml_driver_empty();
            #region PATH DONDE SE GUARDARÁ EL XML
            String path_file = String.Format("C:\\inetpub\\wwwroot\\SistemaGestor\\XML\\{0}.xml", lblNroFormulario.Text + " - " + (String)Session["FILE_XML_NAME"]);
            #endregion
            #region CREAR EL XML
            try
            {
                driverXML.MAKE_XML_FILE(path_file, this.h_str_signed_form.Value.Replace('°', '<').Replace('|', '>').Replace("####", lblNroFormulario.Text));
                mtr_msg("Formulario Firmado Y Guardado En El Sistema !!!");
            }
            catch (Exception ABC)
            {
                mtr_msg("Alerta !!! >> " + ABC.Message);
            }
            #endregion
            #endregion
            #region ACTIVAR BOTON DE PAGOS
            btnPagoDTR_Con_Firma_Digital.Visible = true; //btnPagoDTR.Visible = true;
            #endregion
        }//FUNCION

        protected void ObtenerFormularios(string str_Usuario, string str_TipoId)
        {
            if (string.IsNullOrEmpty(gstr_IdFormulario))
                gdat_Formularios = wsCapturaIngresos.ConsultarFormulario(str_Usuario, str_TipoId, ",PEN,IMP,").Tables[0];
            else
                gdat_Formularios = wsCapturaIngresos.ConsultarFormulario("", "", "").Tables[0];
        }

        protected void ActualizarTipoCambio(DateTime ldt_Fecha)
        {
            this.OcultarMensaje();
            //nuevo para utilizar variable de sesión
            if ((!string.IsNullOrEmpty(clsSesion.Current.TpoCambioEUR)) && (!string.IsNullOrEmpty(clsSesion.Current.TpoCambioCompra)) && (!string.IsNullOrEmpty(clsSesion.Current.TpoCambioVenta)))
            {
                gdec_TipoCambioEur = Convert.ToDecimal(clsSesion.Current.TpoCambioEUR);
                gdec_TipoCambioComp = Convert.ToDecimal(clsSesion.Current.TpoCambioCompra);
                gdec_TipoCambioVent = Convert.ToDecimal(clsSesion.Current.TpoCambioVenta);
            }
            else
            {
                gdat_TiposCambio = wsSistemaGestor.uwsConsultarTiposCambio(null, ldt_Fecha, null, "N").Tables[0];
                gdec_TipoCambioEur = Convert.ToDecimal(gdat_TiposCambio.Select("IdMoneda = 'EUR'")[0]["Valor"].ToString());
                gdec_TipoCambioComp = Convert.ToDecimal(gdat_TiposCambio.Select("TipoTransaccion = '317'")[0]["Valor"].ToString());
                gdec_TipoCambioVent = Convert.ToDecimal(gdat_TiposCambio.Select("TipoTransaccion = '318'")[0]["Valor"].ToString());

                clsSesion.Current.TpoCambioEUR = gdat_TiposCambio.Select("IdMoneda = 'EUR'")[0]["Valor"].ToString();
                clsSesion.Current.TpoCambioCompra = gdat_TiposCambio.Select("TipoTransaccion = '317'")[0]["Valor"].ToString();
                clsSesion.Current.TpoCambioVenta = gdat_TiposCambio.Select("TipoTransaccion = '318'")[0]["Valor"].ToString();
            }

           // gdat_TiposCambio = wsSistemaGestor.uwsConsultarTiposCambio(null, ldt_Fecha, null, "N").Tables[0];  gaston descomentar
            //gdat_TiposCambio = wsSistemaGestor.uwsConsultarTiposCambio(null, gdt_FechaActual.Date, null, "N").Tables[0];
            //string lstr_monto = "";
            //if (lstr_separador_decimal == ",")
            //    lstr_monto = gdat_TiposCambio.Select("IdMoneda = 'EUR'")[0]["Valor"].ToString().Replace(".", "");
            //else
            //    lstr_monto = gdat_TiposCambio.Select("IdMoneda = 'EUR'")[0]["Valor"].ToString().Replace(",", "");
            //gdec_TipoCambioEur = Convert.ToDecimal(gdat_TiposCambio.Select("IdMoneda = 'EUR'")[0]["Valor"].ToString()); gaston descomentar
            //if (lstr_separador_decimal == ",")
            //    lstr_monto = gdat_TiposCambio.Select("TipoTransaccion = '317'")[0]["Valor"].ToString().Replace(".", "");
            //else
            //    lstr_monto = gdat_TiposCambio.Select("TipoTransaccion = '317'")[0]["Valor"].ToString().Replace(",", "");
            //gdec_TipoCambioComp = Convert.ToDecimal(gdat_TiposCambio.Select("TipoTransaccion = '317'")[0]["Valor"].ToString()); gaston descomentar
            //if (lstr_separador_decimal == ",")
            //    lstr_monto = gdat_TiposCambio.Select("TipoTransaccion = '318'")[0]["Valor"].ToString().Replace(".", "");
            //else
            //    lstr_monto = gdat_TiposCambio.Select("TipoTransaccion = '318'")[0]["Valor"].ToString().Replace(",", "");
            //gdec_TipoCambioVent = Convert.ToDecimal(gdat_TiposCambio.Select("TipoTransaccion = '318'")[0]["Valor"].ToString()); gaston descomentar

            DataSet ds_SociedadGL = wsSistemaGestor.uwsConsultarSociedadesGL(ddlInstUPR.SelectedValue, "", "", "", "");
            gstr_CorreoNotificaUPR = "";
            if (ds_SociedadGL.Tables.Count > 0)
            {
                if (ds_SociedadGL.Tables["Table"].Rows.Count > 0)
                {
                    if (ds_SociedadGL.Tables.Count > 0)
                    {
                        if (ds_SociedadGL.Tables["Table"].Rows.Count > 0)
                        {


                            DataSet ds_Oficinas = wsSistemaGestor.uwsConsultarOficinas(ddlOficinas.SelectedValue, ddlInstUPR.SelectedValue, "", "");

                            if (ds_Oficinas.Tables.Count > 0)
                            {
                                if (ds_Oficinas.Tables["Table"].Rows.Count > 0)
                                {
                                    gstr_CorreoNotificaUPR = ds_Oficinas.Tables["Table"].Rows[0]["CorreoNotifica"].ToString();

                                    //if (string.IsNullOrEmpty(gstr_CorreoNotificaUPR))
                                    //    gstr_CorreoNotificaUPR = ds_SociedadGL.Tables["Table"].Rows[0]["CorreoNotifica"].ToString();
                                }
                            }
                        }
                    }
                }
            }
            ImprimeTipoCambio();
               
        }

        protected void ImprimeTipoCambio()
        {
            lblEuro.Text = "$" + gdec_TipoCambioEur.ToString("N2");//("0,##");
            lblCompraDol.Text = "₡" + gdec_TipoCambioComp.ToString("N2");//("0,##");
            lblVentaDol.Text = "₡" + gdec_TipoCambioVent.ToString("N2");//("0,##");
                //ddlPeriodo.SelectedValue = gdt_FechaActual.Year.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (gint_Debug == 0)
            {
                gbol_ConFirmaDigital = clsSesion.Current.gbol_FirmaDigital;
                 if (Session["FIRMA_DIGITAL_LOGIN"] != null && (bool)Session["FIRMA_DIGITAL_LOGIN"])
                {
                    gbol_ConFirmaDigital = true;
                    div_applet.Visible = true;
                }
                 else
                 {
                     div_applet.Visible = false;
                 }

                gstr_Usuario = clsSesion.Current.LoginUsuario;
                gstr_CorreoUsuario = clsSesion.Current.CorreoUsuario;
                gstr_NombreUsuario = clsSesion.Current.NomUsuario;
                gstr_TipoIdUsuario = clsSesion.Current.TipoIdUsuario;
                gstr_IdFormulario = clsSesion.Current.IdFormularioCI;
                gstr_AnnoFormulario = clsSesion.Current.AnnoFormularioCI;
                gstr_IdFormulario_query = clsSesion.Current.IdFormularioCI;
                if (string.IsNullOrEmpty(gstr_IdFormulario)) {
                    if (!string.IsNullOrEmpty(lblNroFormulario.Text))
                    {
                        gstr_IdFormulario_query = lblNroFormulario.Text;                         
                    }
                    else
                    {
                        gstr_IdFormulario_query = ddlListaFormularios.SelectedValue;
                    }
                }
                clsSesion.Current.IdFormularioCI = "";
                clsSesion.Current.AnnoFormularioCI = "";
               
            }
            else
            {
                gstr_Usuario = "";
                gbol_ConFirmaDigital = false;
                gstr_CorreoUsuario = "";
                gstr_NombreUsuario = "";
                gstr_TipoIdUsuario = "";
                gstr_IdFormulario = "";
                gstr_AnnoFormulario = "";
                gstr_IdFormulario_query = "";
            }                
            this.OcultarMensaje();
            //string lstr_Fecha = "";
            //lstr_Fecha = gdt_FechaActual.Date.ToString();
            if (!IsPostBack)
            {
                gdt_FechaActual = DateTime.Today;
                gint_AnnoActual = gdt_FechaActual.Year;

                if (!string.IsNullOrEmpty(gstr_Usuario))
                {
                    if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "OBJ_CI"))
                        Response.Redirect("~/Principal.aspx", true);
                    else
                    {

                        if (string.IsNullOrEmpty(lblEdoFormulario.Text) || (lblEdoFormulario.Text != "PAG" && lblEdoFormulario.Text != "CNT"))
                            ActualizarTipoCambio(gdt_FechaActual);
                        else
                            ActualizarTipoCambio(Convert.ToDateTime(lblFechaPago));
                        if (!string.IsNullOrEmpty(txtIdPersona.Text) && !string.IsNullOrEmpty(ddlTipoPersona.SelectedValue))
                        {
                            ObtenerFormularios(txtIdPersona.Text, ddlTipoPersona.SelectedValue);
                        }
                        else
                        {
                            ObtenerFormularios(gstr_Usuario, gstr_TipoIdUsuario);
                        }
                        CargarInstituciones();
                        //DeshabilitaCamposFormulario();
                        LimpiarFormulario();
                        //HabilitaCamposFormulario();//
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

                        try
                        {
                            //DTR viejo *********************************************************
                            //if (!(wsDTR.ServicioDisponible()))
                            //{
                            //    gbol_ErrorDTR = true;
                            //}
                            //else
                            //{
                            //    gbol_ErrorDTR = false;
                            //}
                            //*********************************************************************

                            //DTR nuevo *************************************************************
                            bool servDisponible = true;
                            bool servDisponibleResult = true;
                                                       
                            wsDTRNuevo.ServicioDisponible(out servDisponible, out servDisponibleResult);

                            if (servDisponible && servDisponibleResult){
                                gbol_ErrorDTR = false;
                            }
                            else
                            {
                                gbol_ErrorDTR = true;
                            }
                            //***********************************************************************

                        }
                        catch (Exception ex)
                        {
                            gbol_ErrorDTR = true;
                            MessageBox.Show("Error: " + ex.ToString());
                            MostarMensaje("Error: " + ex.ToString(), '1');
                        }                         

                        //ggarcia, es mejor obligar a digitar la cedula
                        //txtIdPersona.Text = gstr_Usuario;
                        //lblNombre.Text = gstr_NombreUsuario;
                        //txtCorreo.Text = gstr_CorreoUsuario;
                        ddlTipoPersona.SelectedValue = gstr_TipoIdUsuario;
                        lblLetrasColones.Text = "";
                        lblLetrasDolares.Text = "";
                        //gdt_FechaActual = DateTime.Today;
                        //gint_AnnoActual = gdt_FechaActual.Year;
                        txtAnno.Text = gint_AnnoActual.ToString();
                        ddlPeriodo.ClearSelection();
                        ddlPeriodo.Items.Insert(0, (new ListItem("-- Seleccione--", "0")));//Insert(0, "--Seleccione--");
                        for (int i = 0; i < 21; i++)
                            ddlPeriodo.Items.Insert(i + 1, Convert.ToString(gint_AnnoActual - i));
                        ddlPeriodo.SelectedValue = gdt_FechaActual.Year.ToString();
                        //string lstr_Fecha = "";
                        //lstr_Fecha = gdt_FechaActual.Date.ToString();
                        //try
                        //{
                            ObtenerFormularios(gstr_Usuario, gstr_TipoIdUsuario);
                            //ActualizarTipoCambio(gdt_FechaActual);
                                
                            ReiniciarDataTableTemp();
                            if (!string.IsNullOrEmpty(gstr_IdFormulario))
                            {
                                this.ddlListaFormularios.Visible = false;
                                this.lblNroFormulario.Text = gstr_IdFormulario;
                                this.txtAnno.Visible = false;
                                this.txtAnno.Text = gstr_AnnoFormulario;
                                this.lblAnno.Text = gstr_AnnoFormulario;

                                CargaCamposFormularioSeleccionado(sender, e);
                                HabilitaPago(gbol_ConFirmaDigital, ddlTipoPersona.SelectedValue, txtIdPersona.Text, ddlTipoPersonaTramite.SelectedValue, txtIdPersonaTramite.Text, txtCuentaCliente.Text, ddlInstUPR.SelectedValue, lblEdoFormulario.Text, lblTotalColones.Text);
                            }                                
                        //}
                        //catch (Exception ex)
                        //{
                        //    MessageBox.Show(ex.ToString());
                        //    MostarMensaje(ex.ToString(), '1');
                        //}
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
            //lblTotalColones.Text = gdec_MontoColones.ToString("N2");
            reserva.Visible = false;
        }//FUNCION
        private void CargarInstituciones()
        {
            string str_consul = "SELECT IdSociedadGL, NomSociedad FROM ma.SociedadesGL So WHERE EXISTS (SELECT 1 FROM ma.Servicios Se where Se.IdSociedadGL = So.IdSociedadGL)";
            DataSet lds_Result = this.wsSistemaGestor.uwsConsultarDinamico(str_consul);
            ddlInstUPR.ClearSelection();
            ddlInstUPR.Dispose();
            ddlInstUPR.Items.Clear();
            if (lds_Result.Tables.Count > 0)// && (DDLExpedientes.Items.Count == 0))
            {
                //DataTable ldt_Result
                ldt_Instituciones = lds_Result.Tables["Table"];
                this.ddlInstUPR.DataSource = ldt_Instituciones;
                this.ddlInstUPR.DataTextField = "NomSociedad";
                this.ddlInstUPR.DataValueField = "IdSociedadGL";
                this.ddlInstUPR.Items.Insert(0, new ListItem("--Seleccione--", ""));
                this.ddlInstUPR.DataBind();
                //for (int i = 0; i < DDLMoneda.Items.Count; i++)
                //{
                //    DDLMoneda.Items[i].Value = DDLMoneda.Items[i].Value.Trim();
                //}
            }
        }

        private void CargarOficinas(string lstr_IdInstitucion)
        {
            string str_consul = "SELECT [IdOficina], [NomOficina] FROM [ma].[Oficinas] WHERE (([IdSociedadGL] = '" + lstr_IdInstitucion + "') AND ([Estado] = 'A'))";
            DataSet lds_Result = this.wsSistemaGestor.uwsConsultarDinamico(str_consul);
            ddlOficinas.ClearSelection();
            ddlServicios.ClearSelection();
            ddlOficinas.Dispose();
            ddlServicios.Dispose();
            ddlServicios.Items.Clear();
            ddlOficinas.Items.Clear();

            if (lds_Result.Tables.Count > 0)// && (DDLExpedientes.Items.Count == 0))
            {
                DataTable ldt_Result = lds_Result.Tables["Table"];
                this.ddlOficinas.DataSource = ldt_Result;
                this.ddlOficinas.DataTextField = "NomOficina";
                this.ddlOficinas.DataValueField = "IdOficina";
                this.ddlOficinas.Items.Insert(0, new ListItem("--Seleccione--", ""));
                this.ddlOficinas.DataBind();
                //for (int i = 0; i < DDLMoneda.Items.Count; i++)
                //{
                //    DDLMoneda.Items[i].Value = DDLMoneda.Items[i].Value.Trim();
                //}
            }
        }

        private void CargarServicios(string lstr_IdInstitucion, string lstr_IdOficina)
        {
            string str_consul = "SELECT distinct s.IdServicio, s.NomServicio FROM ma.Servicios s INNER JOIN  ma.ServiciosOficinas OS ON  OS.idSociedadGL = s.IdSociedadGL AND OS.IdServicio = s.IdServicio WHERE  (s.IdSociedadGL = '" + lstr_IdInstitucion + "') AND (OS.IdOficina = '" + lstr_IdOficina + "'  OR os.IdOficina='') AND s.ESTADO ='A'";
            ddlServicios.ClearSelection();
            ddlServicios.Dispose();
            ddlServicios.Items.Clear();
            DataSet lds_Result = this.wsSistemaGestor.uwsConsultarDinamico(str_consul);
            if (lds_Result.Tables.Count > 0)// && (DDLExpedientes.Items.Count == 0))
            {
                DataTable ldt_Result = lds_Result.Tables["Table"];
                this.ddlServicios.DataSource = ldt_Result;
                this.ddlServicios.DataTextField = "NomServicio";
                this.ddlServicios.DataValueField = "IdServicio";
                this.ddlServicios.Items.Insert(0, new ListItem("--Seleccione--", ""));
                this.ddlServicios.DataBind();
                //for (int i = 0; i < DDLMoneda.Items.Count; i++)
                //{
                //    DDLMoneda.Items[i].Value = DDLMoneda.Items[i].Value.Trim();
                //}
            }
        }
        private DataTable GetData(string lstr_query)
        {
            /*string lstr_ConnString = ConfigurationManager.ConnectionStrings["GestNICSPDEVConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(lstr_ConnString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = lstr_query;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataSet ds = new DataSet())
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }*/
            DataSet ds = new DataSet();
            ds = ws_SGService.uwsConsultarDinamico(lstr_query);
            if (ds.Tables.Count > 0)
            {
                return ds.Tables["Table"];
            }
            return null;
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
        }//FUNCION

        public Control FindControlRecursive(Control root, string id)
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
            //try
            //{
                lblTotalColones.Text = "0";
                lblMontoDolares.Text = "0";
                gdec_MontoColones = 0;
                lblLetrasDolares.Text = "";
                lblLetrasColones.Text = "";
                foreach (List<object> vDatos in (List<object>)this.DataTemp)
                {
                    //string lstr_monto = "";
                    //if (lstr_separador_decimal == ",")
                    //    lstr_monto = vDatos[9].ToString().Replace(".", "");
                    //else
                    //    lstr_monto = vDatos[9].ToString().Replace(",", "");
                    if (vDatos[8].ToString().Trim() == "CRC") //IdMoneda                    
                        gdec_MontoColones += Convert.ToDecimal(vDatos[9].ToString()); //Monto
                    else
                        gdec_MontoColones += (gdec_TipoCambioComp * Convert.ToDecimal(vDatos[9].ToString())); //Monto
                    //gdec_MontoColones += (gdec_TipoCambioComp * Convert.ToDecimal(vDatos[9].ToString())); //Monto

                }

                // if (gdec_TipoCambioComp != 0) 
                //   gdec_MontoDolares = gdec_MontoColones / gdec_TipoCambioComp;
                if (gdec_TipoCambioComp != 0)
                    gdec_MontoDolares = gdec_MontoColones / gdec_TipoCambioComp;

                lblLetrasDolares.Text = wsCapturaIngresos.uwsConvertirMontoStringLetras(gdec_MontoDolares.ToString(CultureInfo.InvariantCulture), "dólares", "");
                lblLetrasColones.Text = wsCapturaIngresos.uwsConvertirMontoStringLetras(gdec_MontoColones.ToString(CultureInfo.InvariantCulture), "colones", "");
                lblTotalColones.Text = gdec_MontoColones.ToString("N2");
                lblMontoDolares.Text = gdec_MontoDolares.ToString("N2");
                gdec_MontoColones = 0;
                gdec_MontoDolares = 0;

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //    MostarMensaje(ex.ToString(), '1');
            //    //lblLetrasDolares.Text = ex.ToString();
            //    //lblLetrasColones.Text = "Monto excedido, no se puede mostrar";
            //}

        }

        protected void ActualizarTotalesFormularioSeleccionado()
        {            
            this.OcultarMensaje();
            string str_IdFormulario;
            string str_Anno;
            gdec_MontoColones = 0;
            if (string.IsNullOrEmpty(gstr_IdFormulario))
            {
                if (!string.IsNullOrEmpty(lblNroFormulario.Text))
                {
                    gstr_IdFormulario_query = lblNroFormulario.Text;
                    str_Anno = lblAnno.Text;
                    str_IdFormulario = lblNroFormulario.Text;
                }
                else
                {
                    gstr_IdFormulario_query = ddlListaFormularios.SelectedValue;
                    str_Anno = txtAnno.Text;
                    str_IdFormulario = ddlListaFormularios.SelectedValue;
                }
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
                    if (gdat_Pagos.Rows.Count > 0)
                        for (int i = 0; i < gdat_Pagos.Rows.Count; i++)
                        {
                            //string lstr_monto = "";
                            //if (lstr_separador_decimal == ",")
                            //    lstr_monto = gdat_Pagos.Rows[i]["Monto"].ToString().Replace(".", "");
                            //else
                            //    lstr_monto = gdat_Pagos.Rows[i]["Monto"].ToString().Replace(",", "");
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
                    if (gdat_PagosTemp.Rows.Count > 0)
                    for (int i = 0; i < gdat_PagosTemp.Rows.Count; i++)
                    {
                        //string lstr_monto = "";
                        //if (lstr_separador_decimal == ",")
                        //    lstr_monto = gdat_PagosTemp.Rows[i]["Monto"].ToString().Replace(".", "");
                        //else
                        //    lstr_monto = gdat_PagosTemp.Rows[i]["Monto"].ToString().Replace(",", "");
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
                //lblLetrasDolares.Text = wsCapturaIngresos.uwsConvertirMontoStringLetras(gdec_MontoDolares.ToString(CultureInfo.InvariantCulture), "dólares", "");
                //lblLetrasColones.Text = wsCapturaIngresos.uwsConvertirMontoStringLetras(gdec_MontoColones.ToString(CultureInfo.InvariantCulture), "colones", "");
                //lblTotalColones.Text = gdec_MontoColones.ToString("N2");
                //lblMontoDolares.Text = gdec_MontoDolares.ToString("N2");
                //gdec_MontoColones = 0;
                //gdec_MontoDolares = 0;
            }            
        }

        protected void CargarFormulario(string str_IdFormulario, string str_Anno)
        {
                this.OcultarMensaje();
                if (!string.IsNullOrEmpty(str_IdFormulario))
                {
                    //string lstr_NroFormulario = ddlListaFormularios.SelectedValue;
                    gint_IdFormulario = Convert.ToInt32(str_IdFormulario);
                    DataRow[] ldr_Formulario = gdat_Formularios.Select("IdFormulario = '" + str_IdFormulario + "' AND Anno = '" + str_Anno + "'");
                    //El Id y el tipo de id no se llenan porque son usados como filtro.
                    lblNombre.Text = ldr_Formulario[0]["NomPersona"].ToString();
                    if (!string.IsNullOrEmpty(gstr_IdFormulario_query))
                    {
                        ddlTipoPersona.SelectedValue = ldr_Formulario[0]["TipoIdPersona"].ToString().Trim();
                        txtIdPersona.Text = ldr_Formulario[0]["IdPersona"].ToString().Trim();//ojo 23-2-2016
                    }

                    if (!string.IsNullOrEmpty(ldr_Formulario[0]["TipoIdPersonaTramite"].ToString().Trim()))
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
                    lblFechaPago.Text = ldr_Formulario[0]["FchPago"].ToString();
                    ddlMoneda.SelectedValue = ldr_Formulario[0]["IdMoneda"].ToString().Trim();
                    txtReferencia.Text = ldr_Formulario[0]["ReferenciaDTR"].ToString();
                    CargarInstituciones();

                    //ddlInstUPR.ClearSelection();
                    //ddlOficinas.ClearSelection();
                    //ddlServicios.ClearSelection();
                    //ddlInstUPR.Dispose();
                    //ddlOficinas.Dispose();
                    //ddlServicios.Dispose();
                    //ddlInstUPR.Items.Clear();
                    //ddlInstUPR.Items.Insert(0, (new ListItem("-- Seleccione--", "0")));//Insert(0, "--Seleccione--");
                    //ddlServicios.Items.Clear();
                    //ddlServicios.Items.Insert(0, (new ListItem("-- Seleccione--", "0")));//Insert(0, "--Seleccione--");
                    //ddlOficinas.Items.Clear();
                    //ddlOficinas.Items.Insert(0, (new ListItem("-- Seleccione--", "0")));//Insert(0, "--Seleccione--");
                    //ddlInstUPR.DataBind();
                    ddlInstUPR.SelectedValue = ldr_Formulario[0]["IdSociedadGL"].ToString();
                    CargarOficinas(ddlInstUPR.SelectedValue);
                    ddlOficinas.DataBind();
                    if (ldr_Formulario[0]["IdOficina"].ToString().Trim() != "0")
                    {
                        ddlOficinas.SelectedValue = ldr_Formulario[0]["IdOficina"].ToString();
                    }

                    CargarServicios(ddlInstUPR.SelectedValue, ddlOficinas.SelectedValue);
                    ddlServicios.DataBind();

                    lblEdoFormulario.Text = ldr_Formulario[0]["Estado"].ToString();
                    //string lstr_monto = "";
                    //if (lstr_separador_decimal == ",")
                    //    lstr_monto = ldr_Formulario[0]["Monto"].ToString().Replace(".", "");
                    //else
                    //    lstr_monto = ldr_Formulario[0]["Monto"].ToString().Replace(",", "");
                    if (ldr_Formulario[0]["IdMoneda"].ToString().Trim() == "CRC")
                    {
                        gdec_MontoColones = Convert.ToDecimal(ldr_Formulario[0]["Monto"].ToString());
                        if (gdec_TipoCambioComp != 0)
                            gdec_MontoDolares = gdec_MontoColones / gdec_TipoCambioComp;
                    }
                    else
                    {
                        gdec_MontoDolares = Convert.ToDecimal(ldr_Formulario[0]["Monto"].ToString());
                        if (gdec_TipoCambioComp != 0)
                            gdec_MontoColones = gdec_MontoDolares * gdec_TipoCambioComp;
                    }

                    lblTotalColones.Text = gdec_MontoColones.ToString("N2");
                    lblMontoDolares.Text = gdec_MontoDolares.ToString("N2");

                    lblLetrasDolares.Text = wsCapturaIngresos.uwsConvertirMontoStringLetras(gdec_MontoDolares.ToString(CultureInfo.InvariantCulture), "dólares", "");
                    lblLetrasColones.Text = wsCapturaIngresos.uwsConvertirMontoStringLetras(gdec_MontoColones.ToString(CultureInfo.InvariantCulture), "colones", "");
                    gdec_MontoColones = 0;
                    gdec_MontoDolares = 0;

                    switch (ldr_Formulario[0]["Estado"].ToString().Trim())
                    {
                        case "PEN":
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
                            if (ddlOficinas.SelectedItem.Text.Contains("Judicial"))
                            {
                                txtExpediente.ReadOnly = false;
                            }
                            else
                            {
                                //txtExpediente.Text = "";
                                txtExpediente.ReadOnly = true;
                            }
                            break;


                        default:
                            switch (ldr_Formulario[0]["Estado"].ToString().Trim())
                            {
                                case "IMP":
                                    lblNomEstadoFormulario.Text = "Impreso";
                                    break;
                                case "PAG":
                                    lblNomEstadoFormulario.Text = "Pagado";
                                    this.ActualizarTipoCambio(Convert.ToDateTime( ldr_Formulario[0]["FchPago"].ToString()));
                                    break;
                                case "CNT":
                                    lblNomEstadoFormulario.Text = "Contabilizado";
                                    this.ActualizarTipoCambio(Convert.ToDateTime( ldr_Formulario[0]["FchPago"].ToString()));
                                    break;
                                case "ANU":
                                    lblNomEstadoFormulario.Text = "Anulado";
                                    break;
                            }
                            DeshabilitaCamposFormulario();
                            CongelaCamposFormulario();
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
                            //lblBanco.Visible = false;
                            lblReserva.Visible = false;
                            txtReserva.Visible = false;
                            //txtExpediente.Visible = false;
                            //ddlMoneda.Visible = false;
                            txtMonto.Visible = false;
                            ddlPeriodo.Visible = false;
                            //ddlBanco.Visible = false;
                            ddlServicios.Visible = false;
                            break;

                    }
                    ddlInstUPR.Enabled = false;
                    ddlOficinas.Enabled = false;
                    ddlMoneda.Enabled = false;
                    ddlTipoPersona.Enabled = false;
                    ddlTipoPersonaTramite.Enabled = false;
                    txtIdPersona.Enabled = false;
                    txtIdPersonaTramite.Enabled = false;
                    //CargarPagosFormulario();
                }
        }

        protected void CargarPagosFormulario()
        {
            //try
            //{
                string str_IdFormulario;
                string str_Anno;
                if (string.IsNullOrEmpty(gstr_IdFormulario))
                {
                    if (!string.IsNullOrEmpty(lblNroFormulario.Text))
                    {
                        gstr_IdFormulario_query = lblNroFormulario.Text;
                        str_Anno = lblAnno.Text;
                        str_IdFormulario = lblNroFormulario.Text;
                    }
                    else
                    {
                        gstr_IdFormulario_query = ddlListaFormularios.SelectedValue;
                        str_Anno = txtAnno.Text;
                        str_IdFormulario = ddlListaFormularios.SelectedValue;
                    }
                }
                else
                {
                    gstr_IdFormulario_query = gstr_IdFormulario;
                    str_IdFormulario = gstr_IdFormulario;
                    str_Anno = gstr_AnnoFormulario;
                }
                DataTable ldat_Temporal = new DataTable();
                ldat_Temporal = wsCapturaIngresos.ConsultarPago(Convert.ToInt32(gstr_IdFormulario_query), Convert.ToInt16(txtAnno.Text)).Tables[0];
                if (ldat_Temporal.Rows.Count > 0)
                {
                    DataRow[] ldar_Temporal;
                    ldar_Temporal = ldat_Temporal.Select("Estado = 'A'");

                    gdat_Pagos = ldar_Temporal.CopyToDataTable();
                    gdat_PagosTemp = gdat_Pagos;
                    if (gdat_Pagos.Rows.Count > 0)
                    {
                        ////Cargar datos
                        List<object> vLista = new List<object>();
                        
                        
                        foreach (DataRow row in gdat_Pagos.Rows)
                        {
                            vLista.Add(new List<object> { //this.ddlServicios.SelectedItem.Value, pServicio, pMonto, pPeriodo, pReserva });
                                Convert.ToInt32(str_IdFormulario),
                                Convert.ToInt32(str_Anno),
                                this.ddlInstUPR.SelectedValue,
                                row["IdServicio"],//this.ddlServicios.SelectedValue,
                                row["Servicio"],//this.ddlServicios.SelectedItem.Text,
                                this.ddlOficinas.SelectedValue,
                                row["IdReservaPresupuestaria"],//this.txtReserva.Text, //Revisar xq es el ID
                                this.txtExpediente.Text,
                                this.ddlMoneda.SelectedValue,
                                row["Monto"],//Convert.ToDecimal(this.txtMonto.Text),
                                row["Periodo"],//this.ddlPeriodo.SelectedItem.Text, //El nombre o el ID
                                clsSesion.Current.LoginUsuario,
                                vLista.Count()
                                });
                        }                        
                        //if (this.DataTemp == null || !(this.DataTemp is List<object>))
                        this.DataTemp = vLista;                        
                    }
                    else
                    {
                        gdat_Pagos = this.LlenarTablaVacia();
                        this.DataTemp = null;
                    }
                    grdDatos.DataSource = gdat_Pagos;
                    grdDatos.DataBind();
                }
                else
                {
                    gdat_Pagos = this.LlenarTablaVacia();
                    this.DataTemp = null;
                    grdDatos.DataSource = gdat_Pagos;
                    grdDatos.DataBind();
                    //grdDatos.Rows[0].Visible = false;
                }
            //}
            //catch (Exception ex)
            //{                
            //    MessageBox.Show(ex.ToString());
            //    MostarMensaje(ex.ToString(), '1');
            //    gdat_Pagos.Clear();
            //    grdDatos.DataSource = gdat_Pagos;
            //    grdDatos.DataBind(); 
            //}
         }

        protected void CargaCamposFormularioSeleccionado(object sender, EventArgs e)
        {
            string str_IdFormulario;
            string str_Anno;
            if (string.IsNullOrEmpty(gstr_IdFormulario))
            {
                if (!string.IsNullOrEmpty(lblNroFormulario.Text))
                {
                    gstr_IdFormulario_query = lblNroFormulario.Text;
                    str_Anno = lblAnno.Text;
                    str_IdFormulario = lblNroFormulario.Text;
                }
                else
                {
                    gstr_IdFormulario_query = ddlListaFormularios.SelectedValue;
                    str_Anno = txtAnno.Text;
                    str_IdFormulario = ddlListaFormularios.SelectedValue;
                }
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
                lblFechaPago.Text = String.Empty;
                lblNomEstadoFormulario.Text = String.Empty;
                //DeshabilitaCamposFormulario();
                LimpiarFormulario();//
                //HabilitaCamposFormulario();//
                refrescarGVPagos();
            }
            else
            {
                HabilitaCamposFormulario();
                CargarFormulario(str_IdFormulario, str_Anno);
                CargarPagosFormulario();
                ActualizarTotalesFormularioSeleccionado();
                HabilitaPago(gbol_ConFirmaDigital, ddlTipoPersona.SelectedValue, txtIdPersona.Text, ddlTipoPersonaTramite.SelectedValue, txtIdPersonaTramite.Text, txtCuentaCliente.Text, ddlInstUPR.SelectedValue, lblEdoFormulario.Text, lblTotalColones.Text);
            }            
        }

        protected void txtIdPersona_TextChanged(object sender, EventArgs e)
        {
            //LimpiarFormulario();
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
                        txtCorreo.Text = string.Empty;
                        ddlListaFormularios.Items.Clear();
                        HabilitaCamposFormulario();
                        ObtenerFormularios(txtIdPersona.Text, ddlTipoPersona.SelectedValue);

                        this.ddlListaFormularios.DataSource = gdat_Formularios;
                        this.ddlListaFormularios.DataTextField = "IdFormulario";
                        this.ddlListaFormularios.DataValueField = "IdFormulario";
                        this.ddlListaFormularios.Items.Insert(0, new ListItem("--Seleccione--", ""));

                        ddlListaFormularios.DataBind();
                        LimpiarFormulario();
                        grdDatos.DataBind();
                        CargaCamposFormularioSeleccionado(sender, e);
                        HabilitaPago(gbol_ConFirmaDigital, ddlTipoPersona.SelectedValue, txtIdPersona.Text, ddlTipoPersonaTramite.SelectedValue, txtIdPersonaTramite.Text, txtCuentaCliente.Text, ddlInstUPR.SelectedValue, lblEdoFormulario.Text, lblTotalColones.Text);
                    }
                    else
                    {
                        ddlListaFormularios.Items.Clear();
                        //LimpiarFormulario();
                        //DeshabilitaCamposFormulario();                        
                        ddlListaFormularios.DataBind();
                        LimpiarFormulario();
                        //HabilitaCamposFormulario();
                        grdDatos.DataBind();
                        this.CargaCamposFormularioSeleccionado(sender, e);
                    }
                }
            }
                //if (txtIdPersona.Text == "114180568")
            //{
            //    lblNombre.Text = "Steven Vega Vidal";
            //    txtCorreo.Text = "stevega90@gmail.com";
            //}
        }

        //protected void ddlInstUPR_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {
        //        ddlOficinas.ClearSelection();
        //    }
        //    else 
        //    {

        //        ddlOficinas.Items.Insert(0, (new ListItem("-- Seleccione--", "0")));//Insert(0, "<-- Select -->");
 
        //    }
        //}

        protected void lbtnInsertOrders_Click(object sender, EventArgs e)
        {

        }

        protected void btnFuncion_Click(object sender, EventArgs e)
        {

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

        protected void ddlTipoPersona_SelectedIndexChanged(object sender, EventArgs e)
        {
            CambioLongitudTexto(ddlTipoPersona.SelectedItem.Value);                        
            /*ObtenerFormularios(txtIdPersona.Text, ddlTipoPersona.SelectedValue);
            ddlListaFormularios.DataBind();
            LimpiarFormulario();
            grdDatos.DataBind();
            CargaCamposFormularioSeleccionado(sender, e);
            HabilitaPago(gbol_ConFirmaDigital, ddlTipoPersona.SelectedValue, txtIdPersona.Text, ddlTipoPersonaTramite.SelectedValue, txtIdPersonaTramite.Text, txtCuentaCliente.Text, ddlInstUPR.SelectedValue, lblEdoFormulario.Text, lblTotalColones.Text);
        */}

        protected void refrescarGVPagos()
        {
            string str_IdFormulario;
            string str_Anno;
            if (string.IsNullOrEmpty(gstr_IdFormulario))
            {
                if (!string.IsNullOrEmpty(lblNroFormulario.Text))
                {
                    gstr_IdFormulario_query = lblNroFormulario.Text;
                    str_Anno = lblAnno.Text;
                    str_IdFormulario = lblNroFormulario.Text;
                }
                else
                {
                    gstr_IdFormulario_query = ddlListaFormularios.SelectedValue;
                    str_Anno = txtAnno.Text;
                    str_IdFormulario = ddlListaFormularios.SelectedValue;
                }
            }
            else
            {
                gstr_IdFormulario_query = gstr_IdFormulario;
                str_IdFormulario = gstr_IdFormulario;
                str_Anno = gstr_AnnoFormulario;
            }
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
            //try
            //{
                this.OcultarMensaje();

                string str_IdFormulario;
                string str_Anno;
                if (string.IsNullOrEmpty(gstr_IdFormulario))
                {
                    if (!string.IsNullOrEmpty(lblNroFormulario.Text))
                    {
                        gstr_IdFormulario_query = lblNroFormulario.Text;
                        str_Anno = lblAnno.Text;
                        str_IdFormulario = lblNroFormulario.Text;
                    }
                    else
                    {
                        gstr_IdFormulario_query = ddlListaFormularios.SelectedValue;
                        str_Anno = txtAnno.Text;
                        str_IdFormulario = ddlListaFormularios.SelectedValue;
                    }
                }
                else
                {
                    gstr_IdFormulario_query = gstr_IdFormulario;
                    str_IdFormulario = gstr_IdFormulario;
                    str_Anno = gstr_AnnoFormulario;
                }
                int lint_Indice = Convert.ToInt32(e.CommandArgument.ToString());
                if (e.CommandName == "Borrar")
                {
                    //if (!string.IsNullOrEmpty(gstr_IdFormulario_query) && gstr_IdFormulario_query != "0" && gstr_IdFormulario_query != "--")
                    //{
                    //    int lint_IdFormulario = 0;
                    //    int lint_Anno = 0;
                    //    int lint_IdPago = 0;
                    //    string lstr_Estado = String.Empty;
                    //    DateTime ldt_FchModificacion = new DateTime();

                    //    //lint_IdFormulario = Convert.ToInt32(grdDatos.Rows[grdDatos.SelectedRow.RowIndex].Cells[0].ToString());
                    //    lint_IdFormulario =  Convert.ToInt32(gdat_Pagos.Rows[lint_Indice][0].ToString());
                    //    lint_Anno = Convert.ToInt32(gdat_Pagos.Rows[lint_Indice][14].ToString());
                    //    lint_IdPago = Convert.ToInt32(gdat_Pagos.Rows[lint_Indice][1].ToString());
                    //    lstr_Estado = gdat_Pagos.Rows[lint_Indice][12].ToString();
                    //    ldt_FchModificacion = Convert.ToDateTime(gdat_Pagos.Rows[lint_Indice][13].ToString());

                    //    wsCapturaIngresos.DeshabilitarPago(lint_IdFormulario, lint_Anno, lint_IdPago, lstr_Estado, gstr_Usuario, ldt_FchModificacion);
                    //    gdat_Pagos.Rows.RemoveAt(lint_Indice);
                        
                    //}
                    //else
                    //{
                        //gdat_PagosTemp.Rows.RemoveAt(lint_Indice);
                    List<object> vLista = new List<object>();
                    vLista.Add(new List<object> { this.ddlServicios.SelectedItem.Text, this.txtMonto.Text, this.ddlPeriodo.SelectedItem.Text, this.txtReserva.Text });

                    if (this.DataTemp == null || !(this.DataTemp is List<object>))
                        this.DataTemp = vLista;
                    else
                    {
                        (this.DataTemp as List<object>).Remove(((List<object>)this.DataTemp)[Convert.ToInt32(e.CommandArgument)]);


                        DataTable dt = new DataTable();

                        dt.Columns.Add("Servicio", typeof(string));
                        dt.Columns.Add("Monto", typeof(string));
                        dt.Columns.Add("Periodo", typeof(string));
                        dt.Columns.Add("IdReservaPresupuestaria", typeof(string));


                        foreach (List<object> vDatos in (List<object>)this.DataTemp)
                        {
                            DataRow row = dt.NewRow();

                            row["Servicio"] = vDatos[4];
                            row["Monto"] = vDatos[9];
                            row["Periodo"] = vDatos[10];
                            row["IdReservaPresupuestaria"] = vDatos[6];
                            dt.Rows.Add(row);
                        }
                        this.grdDatos.DataSource = dt;
                        this.grdDatos.DataBind();

                        ActualizarMontosFormulario();

                        //this.pnlDatos.Update();
                    } 
                    //}
                    //refrescarGVPagos();
                    //ActualizarTotalesFormularioSeleccionado();
                }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString()); 
            //    MostarMensaje(ex.ToString(), '1');
            //}
            
        }

        protected void AlmacenarPagosNuevoForm()
        {
            int lint_Pago = 0;
            string str_IdFormulario;
            string str_Anno;

            if (string.IsNullOrEmpty(gstr_IdFormulario))
            {
                if (!string.IsNullOrEmpty(lblNroFormulario.Text))
                {
                    gstr_IdFormulario_query = lblNroFormulario.Text;
                    str_Anno = lblAnno.Text;
                    str_IdFormulario = lblNroFormulario.Text;
                }
                else
                {
                    gstr_IdFormulario_query = ddlListaFormularios.SelectedValue;
                    str_Anno = txtAnno.Text;
                    str_IdFormulario = ddlListaFormularios.SelectedValue;
                }
            }
            else
            {
                gstr_IdFormulario_query = gstr_IdFormulario;
                str_IdFormulario = gstr_IdFormulario;
                str_Anno = gstr_AnnoFormulario;
            }

            wsCapturaIngresos.DeshabilitarPago(Convert.ToInt32(gstr_IdFormulario_query), Convert.ToInt32(str_Anno), -1, "", "", this.gdt_FechaActual);

            

            foreach (List<object> vDatos in (List<object>)this.DataTemp)
            {
                //string lstr_monto = "";
                //if (lstr_separador_decimal == ",")
                //    lstr_monto = vDatos[9].ToString().Replace(".", "");
                //else
                //    lstr_monto = vDatos[9].ToString().Replace(",", "");
                wsCapturaIngresos.CrearPago(
                    //gint_IdFormulario,
                    Convert.ToInt32(gstr_IdFormulario_query),
                    Convert.ToInt32(str_Anno),//vDatos[1]), //IDPeriodo
                    0,
                    Convert.ToDateTime("01/01/1900"),
                    Convert.ToDateTime("01/01/1900"),
                    vDatos[2].ToString(), //Institución
                    vDatos[3].ToString(), //Servicio
                    "-",
                    vDatos[5].ToString(), //IDOficinas
                    "-",
                    vDatos[6].ToString(), //Reserva
                    vDatos[7].ToString(), //Número de expediente
                    vDatos[8].ToString(), //IDMoneda
                    Convert.ToDecimal(vDatos[9].ToString()), //Monto
                    vDatos[10].ToString(), //Periodo
                    vDatos[11].ToString()); //Usuario
            }
            
            //for (int i = 0; i < gdat_PagosTemp.Rows.Count; i++)
            //{
            //    wsCapturaIngresos.CrearPago(
            //        //gint_IdFormulario,
            //        Convert.ToInt32(gstr_IdFormulario_query),
            //        Convert.ToInt32(gdat_PagosTemp.Rows[i]["Periodo"].ToString()),
            //        0,
            //        Convert.ToDateTime("01/01/1900"),
            //        Convert.ToDateTime("01/01/1900"),
            //        gdat_PagosTemp.Rows[i]["IdInstitucion"].ToString(),
            //        gdat_PagosTemp.Rows[i]["IdServicio"].ToString(),
            //        "-",
            //        gdat_PagosTemp.Rows[i]["IdOficina"].ToString(),
            //        "-",
            //        gdat_PagosTemp.Rows[i]["IdReservaPresupuestaria"].ToString(),
            //        gdat_PagosTemp.Rows[i]["NroExpediente"].ToString(),
            //        gdat_PagosTemp.Rows[i]["IdMoneda"].ToString(),
            //        Convert.ToDecimal(gdat_PagosTemp.Rows[i]["Monto"].ToString()),
            //        gdat_PagosTemp.Rows[i]["Periodo"].ToString(),
            //        gdat_PagosTemp.Rows[i]["UsrCreacion"].ToString());
            //}
        }

        protected void GuardarPagoSinFormulario()
        {
            this.GuardarPagoConFormulario();
            //decimal ldec_Monto = 0;
            //if (txtMonto.Text != "")
            //{
            //    ldec_Monto = Convert.ToDecimal(txtMonto.Text);
            //}
            //if (ldec_Monto != 0)
            //{
            //    string lstr_Institucion = ddlInstUPR.SelectedItem.Text;
            //    string lstr_Servicios = ddlServicios.SelectedItem.Text;
            //    string lstr_Oficinas = ddlOficinas.SelectedItem.Text;
            //    string lstr_Moneda = ddlMoneda.SelectedItem.Text;

            //    if (ddlInstUPR.SelectedIndex < 1)
            //    {
            //        lstr_Institucion = String.Empty;
            //    }
            //    if (ddlServicios.SelectedIndex < 1)
            //    {
            //        lstr_Servicios = String.Empty;
            //    }
            //    if (ddlOficinas.SelectedIndex < 1)
            //    {
            //        lstr_Oficinas = String.Empty;
            //    }
            //    if (ddlMoneda.SelectedIndex < 1)
            //    {
            //        lstr_Moneda = String.Empty;
            //    }

            //    gdat_PagosTemp.Rows.Add(0, 0, Convert.ToInt32(txtAnno.Text), Convert.ToDateTime("01/01/1900"),
            //        Convert.ToDateTime("01/01/1900"), Convert.ToString(ddlInstUPR.SelectedValue), lstr_Institucion,
            //        Convert.ToString(ddlServicios.SelectedValue), lstr_Servicios, "-", Convert.ToString(ddlOficinas.SelectedValue),
            //        lstr_Oficinas, "-", txtReserva.Text, txtExpediente.Text, Convert.ToString(ddlMoneda.SelectedValue), lstr_Moneda, Convert.ToDecimal(txtMonto.Text),
            //        ddlPeriodo.Text, "A", gstr_Usuario);
            //    refrescarGVPagos();
            //    ActualizarTotalesFormularioSeleccionado();
            //}
        }

        protected void GuardarPagoConFormulario()
        {
            decimal ldec_Monto = 0;
            int lint_IdPagoTemp = 0;
            String[] lstr_resultado = new String[3];
            int lint_Pago = 0;
            string str_IdFormulario;
            string str_Anno;
            if (string.IsNullOrEmpty(gstr_IdFormulario))
            {
                if (!string.IsNullOrEmpty(lblNroFormulario.Text))
                {
                    gstr_IdFormulario_query = lblNroFormulario.Text;
                    str_Anno = lblAnno.Text;
                    str_IdFormulario = lblNroFormulario.Text;
                }
                else
                {
                    gstr_IdFormulario_query = ddlListaFormularios.SelectedValue;
                    str_Anno = txtAnno.Text;
                    str_IdFormulario = ddlListaFormularios.SelectedValue;
                }
            }
            else
            {
                gstr_IdFormulario_query = gstr_IdFormulario;
                str_IdFormulario = gstr_IdFormulario;
                str_Anno = gstr_AnnoFormulario;
            }
            string lstr_monto = "";
            ldec_Monto = 0;
            if (txtMonto.Text != "")
            {
               
                //if (lstr_separador_decimal == ",")
                //    lstr_monto = txtMonto.Text.Replace(".", "");
                //else
                //    lstr_monto = txtMonto.Text.Replace(",", "");
                ldec_Monto = Convert.ToDecimal(txtMonto.Text);
            }
            if (ldec_Monto != 0)
            {

                lstr_resultado = wsCapturaIngresos.CrearPago(/*gint_IdFormulario*/Convert.ToInt32(gstr_IdFormulario_query), Convert.ToInt32(txtAnno.Text), 0, Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/1900"), Convert.ToString(ddlInstUPR.SelectedValue), Convert.ToString(ddlServicios.SelectedValue), "-", Convert.ToString(ddlOficinas.SelectedValue), "-",
                    txtReserva.Text, txtExpediente.Text, Convert.ToString(ddlMoneda.SelectedValue), Convert.ToDecimal(txtMonto.Text), ddlPeriodo.Text, gstr_Usuario);
               
            }
            CargarPagosFormulario();
            ActualizarTotalesFormularioSeleccionado();
        }

        protected void btnFuncion_Click1(object sender, EventArgs e)
        {

            string str_IdFormulario;
            string str_Anno;
            if (string.IsNullOrEmpty(gstr_IdFormulario))
            {
                if (!string.IsNullOrEmpty(lblNroFormulario.Text))
                {
                    gstr_IdFormulario_query = lblNroFormulario.Text;
                    str_Anno = lblAnno.Text;
                    str_IdFormulario = lblNroFormulario.Text;
                }
                else
                {
                    gstr_IdFormulario_query = ddlListaFormularios.SelectedValue;
                    str_Anno = txtAnno.Text;
                    str_IdFormulario = ddlListaFormularios.SelectedValue;
                }
            }
            else
            {
                gstr_IdFormulario_query = gstr_IdFormulario;
                str_IdFormulario = gstr_IdFormulario;
                str_Anno = gstr_AnnoFormulario;
            }
            if (string.IsNullOrEmpty(gstr_IdFormulario_query) || gstr_IdFormulario_query == "0" || gstr_IdFormulario_query == "--")
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
            clsSesion.Current.Letras = lblLetrasColones.Text;
            if (!String.IsNullOrEmpty(this.lblNroFormulario.Text) && !String.IsNullOrWhiteSpace(this.lblNroFormulario.Text))
            {
                clsSesion.Current.IdFormularioCI = lblNroFormulario.Text;
                clsSesion.Current.AnnoFormularioCI = lblAnno.Text;
            }
            else
            {
                clsSesion.Current.IdFormularioCI = this.ddlListaFormularios.SelectedItem.ToString();
                clsSesion.Current.AnnoFormularioCI = txtAnno.Text;
            }
            
           
            Response.Redirect("~/CapturaIngresos/frmReporteNuevoFormulario.aspx", false);
        }

        protected Boolean ValidaFormulario(out string str_Mensaje, bool pValidaMonto = true)
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
            else if (ddlInstUPR.SelectedIndex < 1)
            {
                str_Mensaje += " Debe seleccionar la institución.";
                lbln_Resultado = false;
            }
            else if (ddlOficinas.SelectedIndex < 1)
            {
                str_Mensaje += " Debe seleccionar la Dependencia.";
                lbln_Resultado = false;
            }
            else if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                str_Mensaje = "Debe digitar la Descripción";
                lbln_Resultado = false;
            }
            //else if (string.IsNullOrEmpty(txtCorreo.Text))
            //{
            //    str_Mensaje += " Debe digitar el Correo Electrónico";
            //    lbln_Resultado = false;
            //}
            else if (string.IsNullOrEmpty(txtDireccion.Text))
            {
                str_Mensaje += " Debe digitar la dirección.";
                lbln_Resultado = false;
            }
            else if ((string.IsNullOrEmpty(lblTotalColones.Text) || Convert.ToDecimal (lblTotalColones.Text) == 0) && pValidaMonto)
            {
                str_Mensaje = "Debe agregar pagos al formulario";
                lbln_Resultado = false;
            }
            else if (!string.IsNullOrEmpty(txtCuentaCliente.Text) && !gbol_ErrorDTR)
            {

                if (txtCuentaCliente.Text.Length != 22)
                {
                    str_Mensaje = "La cuenta IBAN no es válida.";
                    lbln_Resultado = false;
                }
                else
                {
                    Bccr.Cuentas.Negocio.CuentasIban.Validacion.IbanNacional iban = new Bccr.Cuentas.Negocio.CuentasIban.Validacion.IbanNacional(txtCuentaCliente.Text);
                    if (!iban.EsValida())
                    {
                        str_Mensaje += " La cuenta IBAN no es válida.";
                        lbln_Resultado = false;
                    }
                }

                //if (txtCuentaCliente.Text.Length == 22)
                //{
                //    //MessageBox.Show("La cuenta IBAN es válida.");
                //}
                //else
                //{
                //    if (wsCapturaIngresos.uwsCalcularDígitoVerificador(txtCuentaCliente.Text) == false)
                //    {
                //        str_Mensaje = "La cuenta cliente no es válida.";
                //        lbln_Resultado = false;
                //    }
                //}
            }
            else if (!string.IsNullOrEmpty(txtReserva.Text))
            {
                DataSet lds_Reserva = ws_SGService.uwsConsultarReservas(txtReserva.Text, "", "", "", "", "");
                if (!(lds_Reserva.Tables.Count > 0 && lds_Reserva.Tables["Table"].Rows.Count > 0))
                //if (lds_Reserva.Tables.Count == 0 || string.IsNullOrEmpty( lds_Reserva.Tables["Table"].Rows[0]["IdReserva"].ToString()))
                {
                    str_Mensaje += "No se encontró la reserva!";
                    lbln_Resultado = false;
                }
            }
            return lbln_Resultado;
        }

        protected void btnInsertarFormulario_Click(object sender, EventArgs e)//BOTON DE GUARDAR FORMULARIO
        {
            this.guardar_formulario();
        }//FUNCION

        protected void guardar_formulario()
        {
            //try
            //{
            gbol_Error = false;
            string str_IdFormulario;
            string str_Anno;
            if (string.IsNullOrEmpty(gstr_IdFormulario))
            {
                if (!string.IsNullOrEmpty(lblNroFormulario.Text))
                {
                    gstr_IdFormulario_query = lblNroFormulario.Text;
                    str_Anno = lblAnno.Text;
                    str_IdFormulario = lblNroFormulario.Text;
                }
                else
                {
                    gstr_IdFormulario_query = ddlListaFormularios.SelectedValue;
                    str_IdFormulario = ddlListaFormularios.SelectedValue;
                    str_Anno = txtAnno.Text;
                }

            }
            else
            {
                gstr_IdFormulario_query = gstr_IdFormulario;
                str_IdFormulario = gstr_IdFormulario;
                str_Anno = gstr_AnnoFormulario;
            }

            this.OcultarMensaje();
            string lstr_mensaje = "";
            if (ValidaFormulario(out lstr_mensaje))
            {
                string lstr_Moneda = Convert.ToString(ddlMoneda.SelectedValue); //ddlMoneda.SelectedItem.Text;
                decimal ldec_Monto;
                if (lstr_Moneda.Trim() == "CRC")
                {
                    //string lstr_monto = "";

                    //if (lstr_separador_decimal == ",")
                    //    lstr_monto = lblTotalColones.Text.Replace(".", "");
                    //else
                    //    lstr_monto = lblTotalColones.Text.Replace(",", "");
                    ldec_Monto = Convert.ToDecimal(lblTotalColones.Text);
                }
                else
                {
                    //string lstr_monto = "";

                    //if (lstr_separador_decimal == ",")
                    //    lstr_monto = lblMontoDolares.Text.Replace(".", "");
                    //else
                    //    lstr_monto = lblMontoDolares.Text.Replace(",", "");
                    ldec_Monto = Convert.ToDecimal(lblMontoDolares.Text);
                }
                String[] lstr_result = new String[3];
                DateTime ldt_FchIngreso = new DateTime();
                if (string.IsNullOrEmpty(gstr_IdFormulario_query) || gstr_IdFormulario_query == "0" || gstr_IdFormulario_query == "--")
                {
                    ldt_FchIngreso = DateTime.Now;

                    lstr_result = wsCapturaIngresos.CrearFormulario(Convert.ToInt32(gstr_IdFormulario_query), Convert.ToInt32(txtAnno.Text), ddlTipoPersona.SelectedValue, txtIdPersona.Text, lblNombre.Text, ddlTipoPersonaTramite.SelectedValue, txtIdPersonaTramite.Text, lblNombreTramite.Text
                            , txtCorreo.Text, ddlInstUPR.SelectedValue, ddlOficinas.SelectedValue, "", "", "", txtExpediente.Text, txtDescripcion.Text, txtCuentaCliente.Text, txtDireccion.Text
                            , ldt_FchIngreso, Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/1900"), "PEN", "-", lstr_Moneda, ldec_Monto, string.Empty, gstr_Usuario);

                    if (lstr_result[0].ToString().Equals("True"))
                    {
                        MostarMensaje("Señor usuario, en el momento que finalice el pago debe enviar al comprobante de pago vía correo electrónico " + gstr_CorreoNotificaUPR + " o apersonarse a la oficina correspondiente " + ddlInstUPR.SelectedItem.Text + ", de lo contrario iniciará un cobro judicial sobre su trámite", '0');
                        //MostarMensaje(lstr_result[1].ToString(), '0');
                        lblNroFormulario.Text = lstr_result[2].ToString();//Convert.ToString(gint_IdFormulario);
                        gint_IdFormulario = Convert.ToInt32(lblNroFormulario.Text);
                        btnPrepararImprimir.Visible = true;
                        ddlMoneda.Enabled = false;
                        AlmacenarPagosNuevoForm();
                        gdat_PagosTemp.Reset();
                        ddlListaFormularios.Items.Clear();
                        ddlListaFormularios.DataBind();
                        ddlListaFormularios.Items.Insert(1, (new ListItem("-- Selecionar opción --", "0")));
                        gbol_Error = false;
                        if (true)
                        {

                        }
                        HabilitaPago(gbol_ConFirmaDigital, ddlTipoPersona.SelectedValue, txtIdPersona.Text, ddlTipoPersonaTramite.SelectedValue, txtIdPersonaTramite.Text, txtCuentaCliente.Text, ddlInstUPR.SelectedValue, lblEdoFormulario.Text, lblTotalColones.Text);
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

                    lstr_result = wsCapturaIngresos.CrearFormulario(Convert.ToInt32(gstr_IdFormulario_query), Convert.ToInt32(txtAnno.Text), ddlTipoPersona.SelectedValue, txtIdPersona.Text, lblNombre.Text, ddlTipoPersonaTramite.SelectedValue, txtIdPersonaTramite.Text, lblNombreTramite.Text
                            , txtCorreo.Text, ddlInstUPR.SelectedValue, ddlOficinas.SelectedValue, "", "", "", txtExpediente.Text, txtDescripcion.Text, txtCuentaCliente.Text, txtDireccion.Text
                            , ldt_FchIngreso, Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/1900"), "PEN", "-", lstr_Moneda, ldec_Monto, string.Empty, gstr_Usuario);
                    wsSistemaGestor.uwsRegistrarAccionBitacoraCo("CI", clsSesion.Current.IdSesion, "Guardar Formulario", ddlListaFormularios.SelectedValue + " " + lblEdoFormulario.Text.Trim(), ddlListaFormularios.SelectedValue, "", "");

                    if (lstr_result[0].ToString().Equals("True"))
                    {
                        MostarMensaje(lstr_result[1].ToString(), '0');
                        //lblNroFormulario.Text = lstr_result[2].ToString();//Convert.ToString(gint_IdFormulario);
                        ObtenerFormularios(txtIdPersona.Text, ddlTipoPersona.Text);
                        AlmacenarPagosNuevoForm();
                        gbol_Error = false;
                        HabilitaPago(gbol_ConFirmaDigital, ddlTipoPersona.SelectedValue, txtIdPersona.Text, ddlTipoPersonaTramite.SelectedValue, txtIdPersonaTramite.Text, txtCuentaCliente.Text, ddlInstUPR.SelectedValue, lblEdoFormulario.Text, lblTotalColones.Text);
                        
                        //RAMSES_RAMSES
                        //this.btnPagoDTR_Con_Firma_Digital.Visible = true;
                        //================================================
                        //================================================
                        #region RAMSES CAPTURAR FORMULARIO INFO PARA FIRMA DIGITAL
                        this.capturar_formulario_para_firma_digital_xml();
                        #endregion
                        //================================================
                        //================================================
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
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //    MostarMensaje(ex.ToString(), '1');
            //    gbol_Error = true;
            //}
        }//FUNCION

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
            int lint_NroFormulario = 0;
            if (!string.IsNullOrEmpty(lblNroFormulario.Text))
            {
                lint_NroFormulario = Convert.ToInt32(lblNroFormulario.Text);
            }
            if (string.IsNullOrEmpty(gstr_IdFormulario))
            {
                if (!string.IsNullOrEmpty(lblNroFormulario.Text))
                {
                    gstr_IdFormulario_query = lblNroFormulario.Text;
                    str_Anno = lblAnno.Text;
                    str_IdFormulario = lblNroFormulario.Text;
                }
                else
                {
                    gstr_IdFormulario_query = ddlListaFormularios.SelectedValue;
                    str_Anno = txtAnno.Text;
                    str_IdFormulario = ddlListaFormularios.SelectedValue;
                }
            }
            else
            {
                gstr_IdFormulario_query = gstr_IdFormulario;
                str_IdFormulario = gstr_IdFormulario;
                str_Anno = gstr_AnnoFormulario;
            }

            if (string.IsNullOrEmpty(gstr_IdFormulario_query) || gstr_IdFormulario_query == "0")
            {
                MessageBox.Show("Error: Antes de imprimir debe guardar el Formulario"); 
                MostarMensaje("Error: Antes de imprimir debe guardar el Formulario", '1');
            }
            else
            {
                string lstr_mensaje = "";
                if (ValidaFormulario(out lstr_mensaje))
                {
                    string lstr_Moneda = Convert.ToString(ddlMoneda.SelectedValue); //ddlMoneda.SelectedItem.Text;
                    decimal ldec_Monto;
                    if (lstr_Moneda.Trim() == "CRC")
                    {
                        //string lstr_monto = "";

                        //if (lstr_separador_decimal == ",")
                        //    lstr_monto = lblTotalColones.Text.Replace(".", "");
                        //else
                        //    lstr_monto = lblTotalColones.Text.Replace(",", "");
                        ldec_Monto = Convert.ToDecimal(lblTotalColones.Text);
                    }
                    else
                    {
                        //string lstr_monto = "";

                        //if (lstr_separador_decimal == ",")
                        //    lstr_monto = lblMontoDolares.Text.Replace(".", "");
                        //else
                        //    lstr_monto = lblMontoDolares.Text.Replace(",", "");
                        ldec_Monto = Convert.ToDecimal(lblMontoDolares.Text);
                    }


                //try
                //{
                    String[] lstr_result = new String[3];
                    DateTime ldt_FchIngreso = new DateTime();
                    ldt_FchIngreso = Convert.ToDateTime(gdat_Formularios.Select("IdFormulario = '" + gstr_IdFormulario_query + "'")[0]["FchIngreso"]);

                    lstr_result = wsCapturaIngresos.CrearFormulario(Convert.ToInt32(gstr_IdFormulario_query), Convert.ToInt32(txtAnno.Text), ddlTipoPersona.SelectedValue, txtIdPersona.Text, lblNombre.Text, ddlTipoPersonaTramite.SelectedValue, txtIdPersonaTramite.Text, lblNombreTramite.Text
                            , txtCorreo.Text, ddlInstUPR.SelectedValue, ddlOficinas.SelectedValue, "", "", "", txtExpediente.Text, txtDescripcion.Text, txtCuentaCliente.Text, txtDireccion.Text
                            , ldt_FchIngreso, Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/1900"), "IMP", "-", lstr_Moneda, ldec_Monto, string.Empty, gstr_Usuario);
                    
                    if (lstr_result[0].ToString().Equals("True"))
                    {
                        lblNomEstadoFormulario.Text = "Impreso";
                        lblEdoFormulario.Text = "IMP";
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
                        //lblBanco.Visible = false;
                        lblReserva.Visible = false;
                        txtReserva.Visible = false;
                        //txtExpediente.Visible = false;
                        //ddlMoneda.Visible = false;
                        txtMonto.Visible = false;
                        ddlPeriodo.Visible = false;
                        //ddlBanco.Visible = false;
                        ddlServicios.Visible = false;
                        CongelaCamposFormulario();
                        HabilitaPago(gbol_ConFirmaDigital, ddlTipoPersona.SelectedValue, txtIdPersona.Text, ddlTipoPersonaTramite.SelectedValue, txtIdPersonaTramite.Text, txtCuentaCliente.Text, ddlInstUPR.SelectedValue, lblEdoFormulario.Text, lblTotalColones.Text);
                        if (gbol_ConFirmaDigital)
                        {
                            btnPagoDTR_Con_Firma_Digital.Visible = true; //btnPagoDTR.Visible = true;
                        }
                        AlmacenarPagosNuevoForm();
                        DeshabilitaCamposFormulario();
                    }
                    else
                    {
                        MessageBox.Show("Error: " + lstr_result[1].ToString()); 
                        MostarMensaje("Error: " + lstr_result[1].ToString(), '1');
                    }

                    //gdat_PagosTemp.Reset();
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.ToString());
                //    MostarMensaje(ex.ToString(), '1');
                //}
                //Response.Redirect("frmCapturaIngresos.aspx");
                }
                else
                {
                    MessageBox.Show(lstr_mensaje);
                    MostarMensaje(lstr_mensaje, '1');
                }
            }
        }

        protected void HabilitaPago(Boolean bln_ConFirmaDigital, string str_TipoIdPago, string str_IdPersonaTramite, string str_TipoIdTramitador, string str_IdTramitador, string str_CtaCliente, string str_SociedadGL, string str_Estado, string str_Monto)
        {
            //El botón de pagar solo se habilita:
            //1) Si estoy ingresando con FD
            //2) Si estoy pagando algo propio
            //3) Si estoy tramitando un pago de una empresa juridica y estoy domiciliado en la cuenta y tengo FD
            //4) Si no le estoy pagando a adscritas
            //5) Si está en Estado Pendiente(Creado) o Impreso
            btnPagoDTR.Visible = false;
            btnPagoDTR_Con_Firma_Digital.Visible = false;
            if (!(str_SociedadGL == "0") && !(string.IsNullOrEmpty(str_Monto)))   
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
                    else
                    {
                        MessageBox.Show("Atención, el tramitador del pago cédula: " + str_IdTramitador + ", no está autorizado por la empresa.");
                    }

                }
                #region RAMSES ACTIVAR EL BOTON PAGAR

                if (!lbln_EsAdscrita /*&& bln_ConFirmaDigital*/ && (str_Estado.Trim() == "PEN" || str_Estado.Trim() == "IMP") && ((gstr_Usuario == str_IdPersonaTramite && str_IdTramitador == str_IdPersonaTramite) || (str_TipoIdPago == "J" && gstr_Usuario == str_IdTramitador && lbln_EsAutorizado)))
                {
                    if (Session["FIRMA_DIGITAL_LOGIN"] != null && (bool)Session["FIRMA_DIGITAL_LOGIN"])
                    {
                        btnPagoDTR_Con_Firma_Digital.Visible = true;
                    }
                    /*else
                    {
                        btnPagoDTR.Visible = true;//ojo esta linea
                    }*/
                }
                #endregion
                
                try
                {
                    // wsDTR viejo *******************************************************************
                    //if (!(wsDTR.ServicioDisponible()))
                    //{
                    //    gbol_ErrorDTR = true;
                    //    MessageBox.Show("No se encuentra disponible el servicio de pago por DTR");
                    //    btnPagoDTR.Visible = false;
                    //    btnPagoDTR_Con_Firma_Digital.Visible = false;
                    //}
                    //else
                    //{
                    //    gbol_ErrorDTR = false;
                    //}
                    //**********************************************************************************
                    //DTR nuevo *************************************************************
                    bool servDisponible = true;
                    bool servDisponibleResult = true;
                    wsDTRNuevo.ServicioDisponible(out servDisponible, out servDisponibleResult);

                    if (servDisponible && servDisponibleResult)
                    {
                        gbol_ErrorDTR = false;
                    }
                    else
                    {
                        gbol_ErrorDTR = true;
                        MessageBox.Show("No se encuentra disponible el servicio de pago por DTR");
                        btnPagoDTR.Visible = false;
                        btnPagoDTR_Con_Firma_Digital.Visible = false;
                    }
                    //***********************************************************************
                }
                catch (Exception ex)
                {
                    gbol_ErrorDTR = true;
                    btnPagoDTR_Con_Firma_Digital.Visible = false;
                    btnPagoDTR.Visible = false;
                    MessageBox.Show("Error: " + ex.ToString());
                    MostarMensaje("Error: " + ex.ToString(), '1');
                }    
                         
            }
        }

        protected void HabilitaCamposFormulario()
        {
            //ReiniciarDataTableTemp();
            ddlTipoPersona.Enabled = true;
            ddlTipoPersonaTramite.Enabled = true;
            ddlInstUPR.Enabled = true;
            ddlOficinas.Enabled = true;
            txtIdPersona.ReadOnly = false;
            txtIdPersonaTramite.ReadOnly = false;
            //txtIdPersona.ReadOnly = false;

            txtCorreo.ReadOnly = false;
            txtReserva.ReadOnly = true;
            txtExpediente.ReadOnly = true;
            txtDescripcion.ReadOnly = false;
            //txtAnno.ReadOnly = false;
            txtCuentaCliente.ReadOnly = false;
            txtDireccion.ReadOnly = false;
            //ddlBanco.Enabled = true;
            ddlServicios.Enabled = true;
            ddlMoneda.Enabled = true;
            //ddlPeriodo.ReadOnly = false;
            txtMonto.ReadOnly = false;            

        }

        protected void CongelaCamposFormulario()
        {

            ddlTipoPersona.Enabled = false;
            ddlTipoPersonaTramite.Enabled = false;

            txtIdPersona.ReadOnly = true;
            txtIdPersonaTramite.ReadOnly = true;
        }

        protected void DeshabilitaCamposFormulario()
        {
            //ReiniciarDataTableTemp();
            //ddlTipoPersona.Enabled = false;
            //ddlTipoPersonaTramite.Enabled = false;
            ddlInstUPR.Enabled = false;
            ddlOficinas.Enabled = false;
            //txtIdPersona.ReadOnly = true;
            //txtIdPersonaTramite.ReadOnly = true;
            //txtIdPersona.ReadOnly = true;

            txtCorreo.ReadOnly = true;
            txtReserva.ReadOnly = true;
            txtExpediente.ReadOnly = true;
            txtDescripcion.ReadOnly = true;
            //txtAnno.ReadOnly = true;
            txtCuentaCliente.ReadOnly = true;
            txtDireccion.ReadOnly = true;
            //ddlBanco.Enabled = false;
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
            lblMoneda.Visible = true;
            lblMonto.Visible = true;
            lblPeriodo.Visible = true;
            lblServicio.Visible = true;
            //lblBanco.Visible = true;
            lblReserva.Visible = true;
            txtReserva.Visible = true;
            txtExpediente.Visible = true;
            ddlMoneda.Enabled = true;
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

        protected Boolean ValidaPago(out string str_Mensaje)
        {
            Boolean vResultado = true;
            str_Mensaje = "";
            if (ddlMoneda.SelectedIndex < 1)
            {
                vResultado = false;
                str_Mensaje = "No se seleccionó la moneda";
            }
            else if (ddlServicios.SelectedIndex < 1)
            {
                vResultado = false;
                str_Mensaje = "No se seleccionó el Servicio";
            }
            else if (ddlPeriodo.SelectedIndex < 1)
            {
                vResultado = false;
                str_Mensaje = "No se seleccionó el periodo";
            }
            else if (string.IsNullOrEmpty(txtMonto.Text))
            {
                vResultado = false;
                str_Mensaje = "No se indicó el monto de pago";
            }
            //else if (string.IsNullOrEmpty(txtReserva.Text))
            //{
            //    DataSet ds_Servicio = wsSistemaGestor.uwsConsultarServicios(ddlServicios.SelectedValue, ddlInstUPR.SelectedValue, "", "", "", "");
            //    if ((ds_Servicio.Tables["Table"].Rows[0]["PermiteReserva"].ToString() == "S") && Convert.ToInt32(ddlPeriodo.Text) == gint_AnnoActual)
            //    {
            //        vResultado = false;
            //        str_Mensaje = "El servicio requiere que ingrese una Reserva.";
            //    }
            //}

            return vResultado;
        }

        protected void btnAgregarPago_Click(object sender, EventArgs e)
        {
            string str_IdFormulario;
            string str_Anno;
            if (string.IsNullOrEmpty(gstr_IdFormulario))
            {
                if (!string.IsNullOrEmpty(lblNroFormulario.Text))
                {
                    gstr_IdFormulario_query = lblNroFormulario.Text;
                    str_Anno = lblAnno.Text;
                    str_IdFormulario = lblNroFormulario.Text;
                }
                else
                {
                    gstr_IdFormulario_query = ddlListaFormularios.SelectedValue;
                    str_Anno = txtAnno.Text;
                    str_IdFormulario = ddlListaFormularios.SelectedValue;
                }
            }
            else
            {
                gstr_IdFormulario_query = gstr_IdFormulario;
                str_IdFormulario = gstr_IdFormulario;
                str_Anno = gstr_AnnoFormulario;
            }
            decimal ldec_Monto = 0;
            string lstr_mensaje = "";
            if (!this.ValidaFormulario(out lstr_mensaje, false))
            {
                MessageBox.Show(lstr_mensaje);
                MostarMensaje(lstr_mensaje, '1');
            }
            else
            {
                if (this.ValidaPago(out lstr_mensaje))
                {
                    //string lstr_monto = "";

                    //if (lstr_separador_decimal == ",")
                    //    lstr_monto = txtMonto.Text.Replace(".", "");
                    //else
                    //    lstr_monto = txtMonto.Text.Replace(",", "");
                    ldec_Monto = Convert.ToDecimal(txtMonto.Text);
                    if (ldec_Monto != 0)
                    {
                        if (gstr_IdFormulario_query == "0")
                        {
                            GuardarPagoConFormulario();
                        }
                        else
                        {
                            //GuardarPagoConFormulario();
                            CargaGrid();

                        }

                        ActualizarMontosFormulario();
                        ddlMoneda.Enabled = false;
                    }
                }
                else
                {
                    MessageBox.Show(lstr_mensaje);
                    MostarMensaje(lstr_mensaje, '1');
                }
            }
        }
        private DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();
            
            ldt_TablaVacia.Columns.Add("IdPago", typeof(string ));
            ldt_TablaVacia.Columns.Add("IdFormulario", typeof(string));
            ldt_TablaVacia.Columns.Add("Anno", typeof(string));
            ldt_TablaVacia.Columns.Add("FchIngreso", typeof(string));
            ldt_TablaVacia.Columns.Add("FchPago", typeof(string));
            ldt_TablaVacia.Columns.Add("IdInstitucion", typeof(string));
            ldt_TablaVacia.Columns.Add("Sociedad", typeof(string));
            ldt_TablaVacia.Columns.Add("IdServicio", typeof(string));
            ldt_TablaVacia.Columns.Add("Servicio", typeof(string));
            ldt_TablaVacia.Columns.Add("CtaMayor", typeof(string));
            ldt_TablaVacia.Columns.Add("IdOficina", typeof(string));
            ldt_TablaVacia.Columns.Add("Oficina", typeof(string));
            ldt_TablaVacia.Columns.Add("IdPosPre", typeof(string));
            ldt_TablaVacia.Columns.Add("IdReservaPresupuestaria", typeof(string));
            ldt_TablaVacia.Columns.Add("NroExpediente", typeof(string));
            ldt_TablaVacia.Columns.Add("IdMoneda", typeof(string));
            ldt_TablaVacia.Columns.Add("Moneda", typeof(string));
            ldt_TablaVacia.Columns.Add("Monto", typeof(string));
            ldt_TablaVacia.Columns.Add("Periodo", typeof(string));
            ldt_TablaVacia.Columns.Add("Estado", typeof(string));

            //DataRow ldr_FilaTabla = ldt_TablaVacia.NewRow();
            //ldt_TablaVacia.Rows.Add(ldr_FilaTabla);
            return ldt_TablaVacia;
        }

        protected void ddlListaFormularios_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargaCamposFormularioSeleccionado(sender, e);
            HabilitaPago(gbol_ConFirmaDigital, ddlTipoPersona.SelectedValue, txtIdPersona.Text, ddlTipoPersonaTramite.SelectedValue, txtIdPersonaTramite.Text, txtCuentaCliente.Text, ddlInstUPR.SelectedValue, lblEdoFormulario.Text, lblTotalColones.Text);
            //if (!string.IsNullOrEmpty(ddlListaFormularios.Text) && ddlListaFormularios.SelectedValue != "--" && ddlListaFormularios.SelectedValue != "0")
            //lblNroFormulario.Text = ddlListaFormularios.SelectedValue.ToString() ;

        }

        protected void ddlServicios_SelectedIndexChanged(object sender, EventArgs e)
        {
            //se habilita la reserva solo para los servicios de devolucion de anticipos
            //if (ddlServicios.SelectedValue.ToString() == "104" || ddlServicios.SelectedValue.ToString() == "105" ||
            //    ddlServicios.SelectedValue.ToString() == "106" || ddlServicios.SelectedValue.ToString() == "107")

            DataSet ds_Servicio = wsSistemaGestor.uwsConsultarServicios(ddlServicios.SelectedValue, ddlInstUPR.SelectedValue, "", "", "", "");

            //if (ds_Servicio.Tables.Count > 0 && ds_Servicio.Tables["Table"].Rows.Count > 0)
            //{
            //    ds_Servicio = ds_Servicio.Tables["Table"].Rows[0]["CorreoNotifica"].ToString();
            //}
            if ((ds_Servicio.Tables["Table"].Rows[0]["PermiteReserva"].ToString() == "S") && Convert.ToInt32(ddlPeriodo.Text) == gint_AnnoActual )
            {
                txtReserva.Visible = true;
                txtReserva.ReadOnly = false;
            }
            else
            {
                //txtReserva.Visible = false;
                txtReserva.Text = "";
                txtReserva.ReadOnly = true;
            }
            //if (ddlServicios.SelectedItem.Text.Contains("Judicial"))
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
            this.tramitar_pago();
        }//FUNCION

 
        protected void tramitar_pago(/*object sender, EventArgs e*/)
        {
            //paso 1 validar si la cuenta cliente le pertenece
            //ojo, canal 23 es Contabilidad Nacional
            //this.mtr_msg("Se validara la información para tramitar el pago !!!");
            //ACA SE ESTABA LLAMANDO A LA FUNCION DEL EVENTO CLICK DE GUARDAR FORMULARIO DICHA LINEA ESTA COMENTADA
            //this.guardar_formulario();
            //ESTA ESE LA LINEA COMENTADA YA MENCIONADA QUE LLAMA A LA FUNCION DEL EVENTO CLICK
            //btnInsertarFormulario_Click(sender, e);
            
            if (gbol_Error == false)
            {
                string resultado = "";
                try
                {                 
                    this.OcultarMensaje();   
                    
                    //el nuevo DTR requiere las cuentas bancarias de Hacienda por lo que se obtienen del web.config
                    string ctaColonesHacienda = ConfigurationManager.AppSettings["ctaColonesHacienda"].ToString();//"CR49015100010010525180";
                    string ctaDolaresHacienda = ConfigurationManager.AppSettings["ctaDolaresHacienda"].ToString();//"CR63015106120020557381";
                    string ctaIbanHacienda = "";
                   
                    //DTR nuevo******************************************************************************************
                    if (txtCuentaCliente.Text.Length == 22)
                    {
                        Bccr.Cuentas.Negocio.CuentasIban.Validacion.IbanNacional iban = new Bccr.Cuentas.Negocio.CuentasIban.Validacion.IbanNacional(txtCuentaCliente.Text);
                        if (iban.EsValida())
                        {
                            try
                            {                                
                                //Llenado de clase Rastro que ingresa como parametro
                                gdat_Rastro1.Canal = 23;
                                gdat_Rastro1.IP = clsSesion.Current.IPSesion;
                                gdat_Rastro1.Usuario = clsSesion.Current.LoginUsuario;
                                                                
                                gdat_Tranferencia1 = new wsDTR1.Transferencia[1];
                                wsDTR1.Cliente clienteDestino = new wsDTR1.Cliente();
                                wsDTR1.Cliente clienteOrigen = new wsDTR1.Cliente();
                                
                                clienteDestino.IBAN = txtCuentaCliente.Text;
                                clienteDestino.Identificacion = get_cedula_DTR();
                                clienteDestino.Nombre = lblNombre.Text;
                                
                                //LLenado de la clase Transaccion que ingresa como parametro                                
                                wsDTR1.Transferencia temp1 = new wsDTR1.Transferencia();
                                wsDTR1.Transaccion temp2 = new Transaccion(); 
                                
                                temp2.CentroCosto = 21;
                                switch (ddlMoneda.SelectedItem.Text)
                                {
                                    case "Colones":
                                        temp2.Moneda = E_Monedas.Colones;
                                        ctaIbanHacienda = ctaColonesHacienda;
                                        break;
                                    case "Dolares":                                        
                                        temp2.Moneda = E_Monedas.Dolares;
                                        ctaIbanHacienda = ctaDolaresHacienda;
                                        break;
                                    case "Euros":                                        
                                        temp2.Moneda = E_Monedas.Euros;
                                        break;
                                    default:
                                        Console.WriteLine("Moneda Invalida");
                                        break;
                                }
                                
                                temp2.Descripcion = "Sistema Gestor, Entero Digital #" + lblNroFormulario.Text + "-" + lblAnno.Text;
                                temp2.EntidadOrigen = 739;
                                temp2.Monto = Convert.ToDecimal(lblTotalColones.Text);
                                temp2.PuntoIntegracion = 6;
                                temp2.CodigoConcepto = 1;
                                temp2.IDCorrelation = lblNroFormulario.Text;//viejo DTR era temp.IdRelacionCliente
                                temp2.Servicio = "S4-" + txtIdPersona.Text;
                                
                                //esto es nuevo para el nuevo DTR.svc
                                clienteOrigen.IBAN = ctaIbanHacienda;
                                clienteOrigen.Identificacion = get_cedula_DTR();
                                clienteOrigen.Nombre = "Ministerio de Hacienda";
                                
                                temp1.ClienteDestino = clienteDestino;
                                temp1.ClienteOrigen = clienteOrigen;
                                temp1.DatosDebito = temp2;
                                
                                AlmacenarPagosNuevoForm();
                                
                                //Consumo web service                                   
                                wsDTR1.Transferencia[] gdat_Transferencia4; 
                                gdat_Transferencia4 = new Transferencia[1]; 
                                gdat_Transferencia4[0] = temp1;
                                
                                wsDTR1.EnvioDirectoCreditoCuentaRequestBody EnvDir = new EnvioDirectoCreditoCuentaRequestBody();
                                EnvDir.Rastro = gdat_Rastro1;
                                EnvDir.Transacciones = gdat_Transferencia4;
                                
                                wsDTR1.EnvioDirectoCreditoCuentaResponseBody resEnvDir = new EnvioDirectoCreditoCuentaResponseBody();
                                resEnvDir = wsDTRNuevo.EnvioDirectoCreditoCuenta(EnvDir);

                                if (resEnvDir.EnvioDirectoCreditoCuentaResult[0].MotivoError == 0)
                                {
                                    string idFormulario = ddlListaFormularios.SelectedValue.Trim();
                                    string annno = txtAnno.Text.Trim();
                                    DataSet InfoFormulario = wsCapturaIngresos.ConsultarFormulariosCapturaIngresos(Convert.ToInt32(ddlListaFormularios.SelectedValue.Trim()), Convert.ToInt16(txtAnno.Text.Trim()), "", "", "", "", "", "", "", "", "");
                                    //DataSet InfoFormulario = wsCapturaIngresos.ConsultarFormulariosCapturaIngresos(Convert.ToInt32(lblNroFormulario.Text), Convert.ToInt16(lblAnno.Text), "", "", "", "", "", "", "", "", "");

                                    resultado = wsCapturaIngresos.CambiarEstadoFormulario(Convert.ToInt32(idFormulario), Convert.ToInt32(annno), InfoFormulario.Tables["Table"].Rows[0]["Estado"].ToString(), "PAG", Convert.ToString(resEnvDir.EnvioDirectoCreditoCuentaResult[0].CodigoReferencia), clsSesion.Current.LoginUsuario);
                                    //resultado = wsCapturaIngresos.CambiarEstadoFormulario(Convert.ToInt32(lblNroFormulario.Text), Convert.ToInt32(lblAnno.Text), InfoFormulario.Tables["Table"].Rows[0]["Estado"].ToString(), "PAG", Convert.ToString(resEnvDir.EnvioDirectoCreditoCuentaResult[0].CodigoReferencia), clsSesion.Current.LoginUsuario);
                                    
                                    if (resultado == "00")
                                    {                                       
                                        MostarMensaje("Transacción Exitosa. Número de referencia: " + resEnvDir.EnvioDirectoCreditoCuentaResult[0].CodigoReferencia, '2');
                                        lblNomEstadoFormulario.Text = "Pagado";
                                        lblEdoFormulario.Text = "PAG";
                                        wsSistemaGestor.uwsRegistrarAccionBitacoraCo("CI", clsSesion.Current.IdSesion, "Pago Formulario", ddlListaFormularios.SelectedValue + " " + lblEdoFormulario.Text.Trim(), ddlListaFormularios.SelectedValue, "", "");
                                        btnAgregarPago.Visible = false;
                                        btnInsertarFormulario.Visible = false;
                                        btnPrepararImprimir.Visible = false;
                                        btnImprimir.Visible = true;
                                        
                                        //TODO: Quitar siguiente linea
                                        //HabilitaPago(gbol_ConFirmaDigital, ddlTipoPersona.SelectedValue, txtIdPersona.Text, ddlTipoPersonaTramite.SelectedValue, txtIdPersonaTramite.Text, txtCuentaCliente.Text, ddlInstUPR.SelectedValue, lblEdoFormulario.Text, lblTotalColones.Text);
                                        DeshabilitaCamposFormulario();
                                        
                                        wsCapturaIngresos.uwsEnviarCorreoCI("Pago por DTR", "Se ha recibido el pago del formulario " + idFormulario + " del Año " + annno + ". Número de referencia: " + resEnvDir.EnvioDirectoCreditoCuentaResult[0].CodigoReferencia, null, null, Convert.ToInt32(idFormulario), Convert.ToInt16(annno), clsSesion.Current.LoginUsuario);
                                       // wsCapturaIngresos.uwsEnviarCorreoCI("Pago por DTR", "Se ha recibido el pago del formulario " + this.lblNroFormulario.Text + " del Año " + this.lblAnno.Text + ". Número de referencia: " + resEnvDir.EnvioDirectoCreditoCuentaResult[0].CodigoReferencia, null, null, Convert.ToInt32(this.lblNroFormulario.Text), Convert.ToInt16(this.lblAnno.Text), clsSesion.Current.LoginUsuario);
                                        
                                        #region RAMSES SAVE_XML FILE
                                        this.save_xml_signed();
                                        #endregion
                                        
                                        this.btnPagoDTR.Visible = false;
                                        this.btnPagoDTR_Con_Firma_Digital.Visible = false;
                                        
                                        //contabiliza
                                        wsSistemaGestor.EnviarAsientosCI(annno, idFormulario);
                                        //wsSistemaGestor.EnviarAsientosCI(lblAnno.Text.Trim(), lblNroFormulario.Text.Trim());                                        
                                    }
                                    else
                                    {
                                        MessageBox.Show("Error en la Aplicación: " + resultado);
                                        MostarMensaje("Error en la Aplicación: " + resultado, '1');
                                    }
                                }
                                else
                                {
                                    DataSet MotivoRechazo = wsSistemaGestor.uwsConsultarOpcionesCatalogo("41", "", resEnvDir.EnvioDirectoCreditoCuentaResult[0].MotivoError.ToString(), "");
                                    MessageBox.Show("Transacción Rechazada, Motivo: " + resEnvDir.EnvioDirectoCreditoCuentaResult[0].MotivoError + " " + MotivoRechazo.Tables["Table"].Rows[0]["NomOpcion"].ToString());
                                    MostarMensaje("Transacción Rechazada, Motivo: " + resEnvDir.EnvioDirectoCreditoCuentaResult[0].MotivoError + " " + MotivoRechazo.Tables["Table"].Rows[0]["NomOpcion"].ToString(), '1');
                                }
                            }
                            catch (Exception ee)
                            {
                                string menj = ee.Message.ToString();
                                MessageBox.Show("Se ha producido un error en el proceso del pago");
                                MostarMensaje("Error en la Aplicación: ", '1');
                            }
                        }
                        else
                        {
                            MessageBox.Show("Cuenta IBAN No Válida.");
                            MostarMensaje("Cuenta IBAN No Válida.", '1');
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cuenta IBAN No Válida, debe contener 22 caracteres.");
                        MostarMensaje("Cuenta IBAN No Válida, debe contener 22 caracteres.", '1');
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Durante el Procesamiento del Pago: " + ex.Message.ToString());
                    MostarMensaje("Error Durante el Procesamiento del Pago: " + ex.Message.ToString(), '1');
                }
            }
        }//FUNCION

        protected void ddlInstUPR_SelectedIndexChanged1(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
                //HabilitaPago(gbol_ConFirmaDigital, ddlTipoPersona.SelectedValue, txtIdPersona.Text, ddlTipoPersonaTramite.SelectedValue, txtIdPersonaTramite.Text, txtCuentaCliente.Text, ddlInstUPR.SelectedValue, lblEdoFormulario.Text, lblTotalColones.Text);
                //ddlOficinas.ClearSelection();
                //ddlServicios.ClearSelection();
                //ddlOficinas.Dispose();
                //ddlServicios.Dispose();
                //ddlServicios.Items.Clear();
                //ddlOficinas.Items.Clear();
                CargarOficinas(ddlInstUPR.SelectedValue);
                //ddlServicios.Items.Insert(0, (new ListItem("-- Seleccione--", "0")));//Insert(0, "--Seleccione--");


                //ddlOficinas.Items.Insert(0, (new ListItem("-- Seleccione--", "0")));//Insert(0, "--Seleccione--");
                //ddlOficinas.DataBind();
                //ddlServicios.DataBind();
                DataSet ds_SociedadGL = wsSistemaGestor.uwsConsultarSociedadesGL(ddlInstUPR.SelectedValue, "", "", "", "");
                gstr_CorreoNotificaUPR = "";
                if (ds_SociedadGL.Tables.Count > 0 && ds_SociedadGL.Tables["Table"].Rows.Count > 0)
                {
                    gstr_CorreoNotificaUPR = ds_SociedadGL.Tables["Table"].Rows[0]["CorreoNotifica"].ToString();
                }
            //}
        }

        protected void ddlOficinas_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds_Oficinas;// = new DataSet();
            ds_Oficinas = wsSistemaGestor.uwsConsultarOficinas(ddlOficinas.SelectedValue, ddlInstUPR.SelectedValue, "", "");
            if (ds_Oficinas.Tables["Table"].Rows[0]["UsaExpediente"].ToString() == "S")
            {
                txtExpediente.ReadOnly = false;
            }
            else
            {
                txtExpediente.Text = "";
                txtExpediente.ReadOnly = true;
            }


            CargarServicios(ddlInstUPR.SelectedValue, ddlOficinas.SelectedValue);
            //ddlServicios.Items.Insert(0, (new ListItem("-- Seleccione--", "0")));//Insert(0, "--Seleccione--");
            //ddlServicios.DataBind();
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
                    //HabilitaPago(gbol_ConFirmaDigital, ddlTipoPersona.SelectedValue, txtIdPersona.Text, ddlTipoPersonaTramite.SelectedValue, txtIdPersonaTramite.Text, txtCuentaCliente.Text, ddlInstUPR.SelectedValue, lblEdoFormulario.Text, lblTotalColones.Text);
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

            if (txtCuentaCliente.Text.Length == 22)
            {
                Bccr.Cuentas.Negocio.CuentasIban.Validacion.IbanNacional iban = new Bccr.Cuentas.Negocio.CuentasIban.Validacion.IbanNacional(txtCuentaCliente.Text);
                if (iban.EsValida())
                {
                    if (!gbol_ErrorDTR)
                    {
                        try
                        {
                            wsDTR1.ObtenerInformacionCuentaRequestBody objInfo = new ObtenerInformacionCuentaRequestBody();
                            objInfo.IBAN = txtCuentaCliente.Text;
                            objInfo.Identificacion = get_cedula_DTR();
                            wsDTR1.ObtenerInformacionCuentaResponseBody resInfoCta = new ObtenerInformacionCuentaResponseBody();
                            resInfoCta = wsDTRNuevo.ObtenerInformacionCuenta(objInfo);
                            if(resInfoCta.ObtenerInformacionCuentaResult.MotivoRechazo != 0)
                            {
                                MostarMensaje("Cuenta IBAN No Pertenece a la Cédula del Usuario.", '1');
                                MessageBox.Show("Cuenta IBAN No Pertenece a la Cédula del Usuario.");
                            }
                            else
                            {
                                lblNombre.Text = resInfoCta.ObtenerInformacionCuentaResult.Nombre == null ? "" : resInfoCta.ObtenerInformacionCuentaResult.Nombre; 
                            }
                        }
                        catch (Exception ed)
                        {
                            MessageBox.Show("Imposible validar la informacion de destino.");

                        }
                    }
                }
                else
                {
                    MessageBox.Show("La cuenta IBAN no es válida.");
                }
            }
            else
            {
                MessageBox.Show("La cuenta IBAN no es válida, debe contener 22 caracteres.");
            }                         
        }

        protected void txtReserva_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtReserva.Text))
            {
                DataSet lds_Reserva = ws_SGService.uwsConsultarReservas(txtReserva.Text, "", "", "", "", "");
                if (!(lds_Reserva.Tables.Count > 0 && lds_Reserva.Tables["Table"].Rows.Count > 0))
                //if (lds_Reserva.Tables.Count == 0 || string.IsNullOrEmpty(lds_Reserva.Tables["Table"].Rows[0]["IdReserva"].ToString()))
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

        private void CargaGrid()
        {
            int lint_NroFormulario = 0;
            //string lstr_monto = "";

            //if (lstr_separador_decimal == ",")
            //    lstr_monto = this.txtMonto.Text.Replace(".", "");
            //else
            //    lstr_monto = this.txtMonto.Text.Replace(",", "");
            if (!string.IsNullOrEmpty(lblNroFormulario.Text))
            {
                lint_NroFormulario = Convert.ToInt32(lblNroFormulario.Text);
            }
            DataTable dt = new DataTable();
            List<object> vLista = new List<object>();
            vLista.Add(new List<object> {                 
                lint_NroFormulario,
                Convert.ToInt32(this.ddlPeriodo.SelectedItem.Text),
                this.ddlInstUPR.SelectedValue,
                this.ddlServicios.SelectedValue,
                this.ddlServicios.SelectedItem.Text,
                this.ddlOficinas.SelectedValue,
                this.txtReserva.Text, //Revisar xq es el ID
                this.txtExpediente.Text,
                this.ddlMoneda.SelectedValue,
                Convert.ToDecimal(this.txtMonto.Text),
                this.ddlPeriodo.SelectedItem.Text, //El nombre o el ID
                clsSesion.Current.LoginUsuario,
                vLista.Count()
            });
            if (this.DataTemp == null || !(this.DataTemp is List<object>))
                this.DataTemp = vLista;
            else ((List<object>)this.DataTemp).Add(new List<object> { 
                lint_NroFormulario,
                Convert.ToInt32(this.ddlPeriodo.SelectedItem.Text),
                this.ddlInstUPR.SelectedValue,
                this.ddlServicios.SelectedValue,
                this.ddlServicios.SelectedItem.Text,
                this.ddlOficinas.SelectedValue,
                this.txtReserva.Text, //Revisar xq es el ID
                this.txtExpediente.Text,
                this.ddlMoneda.SelectedValue,
                Convert.ToDecimal(this.txtMonto.Text),
                this.ddlPeriodo.SelectedItem.Text, //El nombre o el ID
                clsSesion.Current.LoginUsuario,
                vLista.Count()
             });

            dt.Columns.Add("Servicio", typeof(string));
            dt.Columns.Add("Monto", typeof(string));
            dt.Columns.Add("Periodo", typeof(string));
            dt.Columns.Add("IdReservaPresupuestaria", typeof(string));

            int vContador = 0;

            #region RAMSES LISTA DE PAGOS ENCONTRADOS
            List<INFO_PAGO> lista = new List<INFO_PAGO>();
            #endregion

            foreach (List<object> vDatos in (List<object>)this.DataTemp)
            {
                #region RAMSES LISTA DE PAGOS ENCONTRADOS
                lista.Add(new INFO_PAGO(vDatos[4].ToString(), vDatos[10].ToString(), vDatos[9].ToString(), vDatos[6].ToString()));
                #endregion
                DataRow row = dt.NewRow();
                //  Session["IDPosicion"] = vContador;
                row["Servicio"] = vDatos[4];
                row["Monto"] = vDatos[9];
                row["Periodo"] = vDatos[10];
                row["IdReservaPresupuestaria"] = vDatos[6];
                dt.Rows.Add(row);
                vContador++;
            }

            #region RAMSES LISTA DE PAGOS ENCONTRADOS
            if (lista.Count > 0)
            {
                this.LISTA_DE_PAGOS = lista;
            }
            #endregion

            this.grdDatos.DataSource = dt;
            this.grdDatos.DataBind();
            //string pServicio =  this.ddlServicios.SelectedItem.Text;
            //string pMonto = this.txtMonto.Text;
            //string pPeriodo = this.ddlPeriodo.SelectedItem.Text;
            //string pReserva = this.txtReserva.Text;

            //DataTable dt = new DataTable();
            //List<object> vLista = new List<object>();
            //vLista.Add(new List<object> { this.ddlServicios.SelectedItem.Value, pServicio, pMonto, pPeriodo, pReserva });
            // if (this.DataTemp == null || !(this.DataTemp is List<object>))
            //    this.DataTemp = vLista;
            // else ((List<object>)this.DataTemp).Add(new List<object> { this.ddlServicios.SelectedItem.Value, pServicio, pMonto, pPeriodo, pReserva });

            // dt.Columns.Add("Servicio", typeof(string));
            // dt.Columns.Add("Monto", typeof(string));
            // dt.Columns.Add("Periodo", typeof(string));
            // dt.Columns.Add("IdReservaPresupuestaria", typeof(string));

            // int vContador = 0;

            // foreach (List<object> vDatos in (List<object>)this.DataTemp)
            // {
            //     DataRow row = dt.NewRow();
            //   //  Session["IDPosicion"] = vContador;
            //     row["Servicio"] = vDatos[1];
            //     row["Monto"] = vDatos[2];
            //     row["Periodo"] = vDatos[3];
            //     row["IdReservaPresupuestaria"] = vDatos[4];
            //     dt.Rows.Add(row);
            //     vContador++;
            //}
            // grdDatos.DataSource = dt;
            // grdDatos.DataBind();
        }

        protected void ddlPeriodo_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds_Servicio = wsSistemaGestor.uwsConsultarServicios(ddlServicios.SelectedValue, ddlInstUPR.SelectedValue, "", "", "", "");
            if ((ds_Servicio.Tables["Table"].Rows[0]["PermiteReserva"].ToString() == "S") && Convert.ToInt32(ddlPeriodo.Text) == gint_AnnoActual)
            {
                //txtReserva.Visible = true;
                txtReserva.ReadOnly = false;
            }
            else
            {
                //txtReserva.Visible = false;
                txtReserva.Text = "";
                txtReserva.ReadOnly = true;
            }
        }



        #region RAMSES FUNCIONES
        protected void funcion_listener_firma_digital()
        {
            while (this.h_listen_firma.Value != "C#234?9$#1$9238478rTXK") { }
            //mtr_msg("Se procedera con el Pago Respectivo !!!");
            //ES NECESARIO RESETEAR EL CAMPO SEGÚN LA LOGICA DEL FLUJO DE TRABAJO
            this.h_listen_firma.Value = "";
            //ACÁ CONTINUARIA CON EL CODIGO DEL PAGO Y TODA LA TRANSACCIÓN...
            this.tramitar_pago();
        }//FUNCION
        protected void capturar_formulario_para_firma_digital_xml()
        {
            if (Session["FIRMA_DIGITAL_LOGIN"] != null && (bool)Session["FIRMA_DIGITAL_LOGIN"])
            {
                #region SAVE FORM ON XML RAMSES
                //=============================================================================================================
                //SALVAR EL FORMULARIO EN XML PARA ENVIAR AL APPLET QUE LO FIRMA DIGITALMENTE
                //=============================================================================================================
                PARA para = new PARA(ddlTipoPersonaTramite.SelectedValue, this.txtIdPersona.Text);
                TRAMITA tramita = new TRAMITA(ddlTipoPersona.SelectedValue, this.txtIdPersonaTramite.Text);
                INFO_FORMULARIO info_formulario = new INFO_FORMULARIO(para, tramita, "####", this.txtAnno.Text, this.lblFechaIngreso.Text, this.txtCorreo.Text, this.txtDireccion.Text, this.ddlInstUPR.SelectedValue, this.ddlOficinas.SelectedValue, this.txtDescripcion.Text, this.txtExpediente.Text, this.ddlMoneda.SelectedValue);
                INFO_PAGO info_pago = new INFO_PAGO(this.ddlServicios.SelectedValue, this.ddlPeriodo.SelectedValue, this.txtMonto.Text, this.txtReserva.Text);
                XML__DRIVER xml_str = null;
                //SALVAR EL NOMBRE DEL DOC XML
                String name_xml_file = DateTime.Now.Year + " - " + txtIdPersonaTramite.Text;
                Session["FILE_XML_NAME"] = name_xml_file;
                //this.h_file_name.Value = name_xml_file;
                //VER SI HAY UNA LISTA DE PAGOS
                if (this.LISTA_DE_PAGOS == null)
                {
                    xml_str = new XML__DRIVER(info_formulario, info_pago);
                }
                else
                {
                    xml_str = new XML__DRIVER(info_formulario, info_pago, this.LISTA_DE_PAGOS.ToArray());
                }
                xml_str.init_XML_STR();
                this.h_str_form.Value = xml_str.XML_STR.Replace('<', '°').Replace('>', '|');
                /*this.applet_box.Visible = true;
                this.btn_save_xml.Visible = true;*/
                #endregion
            }
        }//FUNCION
        protected void call_listener_firma_digital()
        {
            new Thread(this.funcion_listener_firma_digital).Start();
        }//FUNCION
        protected void save_xml_signed()
        {
            #region RAMSES SALVAR XML
            XML__DRIVER driverXML = get_xml_driver_empty();
            #region PATH DONDE SE GUARDARÁ EL XML
            String path_file = String.Format("C:\\Windows\\Temp\\{0}.xml", lblNroFormulario.Text + " - " + (String)Session["FILE_XML_NAME"]);
            //String path_file = String.Format("C:\\inetpub\\wwwroot\\SistemaGestor\\XML\\{0}.xml", lblNroFormulario.Text + " - " + (String)Session["FILE_XML_NAME"]);
            #endregion
            #region CREAR EL XML
            try
            {
                driverXML.MAKE_XML_FILE(path_file, this.h_str_signed_form.Value.Replace('°', '<').Replace('|', '>').Replace("####", this.ddlListaFormularios.SelectedValue));
                mtr_msg("Formulario Firmado Y Guardado En El Sistema !!!");
            }
            catch (Exception ABC)
            {
                mtr_msg("Alerta !!! >> " + ABC.Message);
            }
            #endregion
            #endregion
        }
        protected void btnPagoDTR_Con_Firma_Digital_Click(object sender, EventArgs e)
        {           
            if (this.h_listen_firma.Value == "C#234?9$#1$9238478rTXK")
            {
                this.tramitar_pago();
                this.h_listen_firma.Value = "";
            }
            else
            {
                mtr_msg("Se Canceló La Operación De Firma Digital. No Se Procederá Con El Pago !!!");
            }
        }//FUNCION

        private string get_cedula_DTR()
        {
            //formato de cedulas mh http://www.hacienda.go.cr/consultapagos/ayuda_cedulas.htm
            string cedula = txtIdPersona.Text;

            if (ddlTipoPersona.SelectedValue == "F")
            {
                //Fisico;  0P-TTTT-AAAA
                cedula = txtIdPersona.Text.Insert(6, "-").Insert(2, "-");
            }
            else if (ddlTipoPersona.SelectedValue == "J")
            {
                //Juridico;  3-TTT-CCCCCC    ya no txtIdPersona.Text.Insert(5, "-").Insert(1, "-");
                cedula = txtIdPersona.Text.Insert(4, "-").Insert(1, "-");
            }

            return cedula;
        }
        //private string get_cedula_DTR1()
        //se comenta pq está malo
        //{
        //    string cedula = txtIdPersona.Text;

        //    if (ddlTipoPersona.SelectedValue == "F")
        //    {
        //        //Fisico;
        //        cedula = txtIdPersona.Text.Insert(6, "-").Insert(2, "-");
        //    }
        //    else if (ddlTipoPersona.SelectedValue == "J")
        //    {
        //        //Juridico;
        //        cedula = txtIdPersona.Text.Insert(5, "-").Insert(1, "-");
        //    }

        //    return cedula;
        //}

        #endregion


    }//CLASE
}//namespace