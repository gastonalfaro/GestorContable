using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Web.UI.HtmlControls;
using Presentacion.Compartidas;
using System.Globalization;
using System.Configuration;
//using System.Data.SqlClient;
using LogicaNegocio.Contingentes;
using Presentacion.Contingentes.ArchivosCO;
//Log4Net inicializa en WebApplication

namespace Presentacion.Contingentes
{
    public partial class ConsultarExpedientes : BASE
    {
        #region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private Presentacion.wsAsientos.ServicioContable ws_ContabilizaAsientos = new Presentacion.wsAsientos.ServicioContable();


        private String gstr_IdSociedadGL = String.Empty;
        private String gstr_Usuario = String.Empty;
        private String gstr_IdExpediente = String.Empty;
        //private static Int32 gint_Periodo;

        protected Int32 gint_Periodo
        {
            get
            {
                if (ViewState["gint_Periodo"] == null)
                    ViewState["gint_Periodo"] = 0;
                return Convert.ToInt32(ViewState["gint_Periodo"]);
            }
            set
            {
                ViewState["gint_Periodo"] = value;
            }
        }
        //private static Decimal gdec_MontoPrincipalColRev;
        protected Decimal gdec_MontoPrincipalColRev
        {
            get
            {
                if (ViewState["gdec_MontoPrincipalColRev"] == null)
                    ViewState["gdec_MontoPrincipalColRev"] = null;
                return Convert.ToDecimal(ViewState["gdec_MontoPrincipalColRev"]);
            }
            set
            {
                ViewState["gdec_MontoPrincipalColRev"] = value;
            }
        }
        //private static Decimal gdec_MontoInteresesColRev;
        protected Decimal gdec_MontoInteresesColRev
        {
            get
            {
                if (ViewState["gdec_MontoInteresesColRev"] == null)
                    ViewState["gdec_MontoInteresesColRev"] = null;
                return Convert.ToDecimal(ViewState["gdec_MontoInteresesColRev"]);
            }
            set
            {
                ViewState["gdec_MontoInteresesColRev"] = value;
            }
        }
        //private static Decimal gdec_MontoPrincipalRP2;
        protected Decimal gdec_MontoPrincipalRP2
        {
            get
            {
                if (ViewState["gdec_MontoPrincipalRP2"] == null)
                    ViewState["gdec_MontoPrincipalRP2"] = null;
                return Convert.ToDecimal(ViewState["gdec_MontoPrincipalRP2"]);
            }
            set
            {
                ViewState["gdec_MontoPrincipalRP2"] = value;
            }
        }
        //private static Decimal gdec_MontoInteresesRP2;
        protected Decimal gdec_MontoInteresesRP2
        {
            get
            {
                if (ViewState["gdec_MontoInteresesRP2"] == null)
                    ViewState["gdec_MontoInteresesRP2"] = null;
                return Convert.ToDecimal(ViewState["gdec_MontoInteresesRP2"]);
            }
            set
            {
                ViewState["gdec_MontoInteresesRP2"] = value;
            }
        }

        private String gstr_AsientosResultado;
        private Asiento asiento = new Asiento();
        private Boolean gbool_CambioAno;
        Boolean lbool_TienePretensionInicial = false;
        Boolean lbool_TieneRP1 = false;
        Boolean lbool_TieneRP2 = false;
        Boolean lbool_TieneRF = false;

        DataSet lds_ConsultarResolucion = new DataSet();
        DataSet lds_ConsultarExpediente = new DataSet();
        DataSet lds_ConsultarBitacora = new DataSet();

        DataRow ldr_ConsultarResolucion = null;
        DataRow ldr_ConsultarExpediente = null;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            gstr_Usuario = clsSesion.Current.LoginUsuario;
            gstr_IdSociedadGL = clsSesion.Current.SociedadUsr;
            //lkBotonAnular.Text = get_link_text();
            
            
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(gstr_Usuario))
                {
                    if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "OBJ_CT"))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                }
                else
                {
                    MessageBox.Show("Sesión de usuario finalizó.");
                    Response.Redirect("~/Login.aspx", true);
                }
            }
            else
            {

                if (String.IsNullOrEmpty(gstr_Usuario))
                {
                    MessageBox.Show("Sesión de usuario finalizó.");
                    Response.Redirect("~/Login.aspx", true);
                }
            }
            
        }

        private void CargarDatosExpedientes()
        {
            string numeroExp=this.txtNumExp.Text;
            //string fechaInicio = calDesde.SelectedDate.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            //string fechaFin = calHasta.SelectedDate.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

            DataSet lds_Expedientes = new DataSet();

            if (!String.IsNullOrEmpty(numeroExp))
            {
                lds_Expedientes = ws_SGService.uwsConsultarExpedienteXNumero(numeroExp, clsSesion.Current.SociedadUsr);
            }
            else if (!String.IsNullOrEmpty(this.txtFechaDesde.Text) && !String.IsNullOrEmpty(this.txtFechaHasta.Text))
            {
                string fechaInicio = this.txtFechaDesde.Text;
                string fechaFin = this.txtFechaHasta.Text;

                lds_Expedientes = ws_SGService.uwsConsultarExpedienteXFecha(fechaInicio, fechaFin, clsSesion.Current.SociedadUsr);
            }
            else
            {
                lds_Expedientes = ws_SGService.uwsConsultarExpediente(clsSesion.Current.SociedadUsr);
            }

           // grdExpedientes.Attributes.Keys = "";
                if (lds_Expedientes.Tables.Count != 0)
                {
                    //Llenamos grid de expedientes
                    if (lds_Expedientes.Tables["Table"].Rows.Count > 0)
                    {
                        grdExpedientes.DataSource = lds_Expedientes; //lds_Expedientes.Tables["Table"];
                        grdExpedientes.DataBind();
                    }
                    else
                    {
                        grdExpedientes.DataSource = lds_Expedientes; //this.LlenarTablaVacia();
                        grdExpedientes.DataBind();
                        //grdExpedientes.Rows[0].Visible = false;
                    }
                }
            string text_anular = get_link_text();
            foreach(GridViewRow row in grdExpedientes.Rows)
            {
                ((LinkButton)row.Cells[9].Controls[1]).Text = text_anular;
                ((LinkButton)row.Cells[9].Controls[1]).OnClientClick = String.Format("return confirm('¿Usted va {0} un expediente?');", text_anular.ToLower());
            }
            
           // foreach(grdExpedientes.)
        }

        protected void grdExpedientes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Anular")
            {
                LinkButton lkBotonAnular = (LinkButton)e.CommandSource;
                lkBotonAnular.Text = get_link_text();
                gstr_IdExpediente = lkBotonAnular.CommandArgument;
            }

        }

        private string get_link_text()
        {
            string text = "Anular";
            DataSet lds_RolesUsuario = ws_SGService.uwsConsultarRolesUsuario("", gstr_Usuario);
            if (lds_RolesUsuario.Tables.Count > 0)
            {
                foreach (DataRow row in lds_RolesUsuario.Tables[0].Rows)
                {
                    if (row["DescRol"].ToString().Contains("CT") && row["DescRol"].ToString().Contains("Finan"))
                    {
                        text = "Pagar";
                        break;
                    }
                }
            }
            return text;
        }

        protected void grdExp_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdExpedientes.PageIndex = e.NewPageIndex;
            CargarDatosExpedientes();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarDatosExpedientes();
        }

        private void TieneResolucion()
        {
            String lstr_Mensaje = string.Empty;
            String lstr_Codigo = String.Empty;

            lds_ConsultarResolucion = ws_SGService.uwsConsultarResolucion("", gstr_IdExpediente, gstr_IdSociedadGL, out lstr_Codigo, out lstr_Mensaje);

            lbool_TieneRP1 = false;
            lbool_TieneRP2 = false;
            lbool_TieneRF = false;

            foreach (DataRow ldr_ConsultarResolucion in lds_ConsultarResolucion.Tables["Table"].Rows)
            {
                if (ldr_ConsultarResolucion["EstadoResolucion"].ToString().Contains("En Firme"))
                {
                    lbool_TieneRF = true;
                }
                if (ldr_ConsultarResolucion["EstadoResolucion"].ToString().Contains("Provisional 2"))
                {
                    lbool_TieneRP2 = true;
                    if (lbool_TieneRF)
                    {
                        break;
                    }
                }
                if (ldr_ConsultarResolucion["EstadoResolucion"].ToString().Contains("Provisional 1"))
                {
                    lbool_TieneRP1 = true;
                    if (lbool_TieneRP2 || lbool_TieneRF)
                    {
                        break;
                    }
                }
                if (lbool_TieneRF || lbool_TieneRP2 || lbool_TieneRP1)
                {
                    gdec_MontoPrincipalColRev = Convert.ToDecimal(ldr_ConsultarResolucion["MontoPrincipal"], CultureInfo.InvariantCulture);
                    gdec_MontoInteresesColRev = Convert.ToDecimal(ldr_ConsultarResolucion["MontoIntereses"], CultureInfo.InvariantCulture);
                    gint_Periodo = Convert.ToDateTime(ldr_ConsultarResolucion["FchCreacion"].ToString()).Year;
                }
            }

        }


        protected void lkBotonAnular_Command(object sender, CommandEventArgs e)
        {
            Boolean lbool_Reinicio = false;

            String lstr_ResultadoCaso = String.Empty;



            String lstr_Codigo = String.Empty;
            String lstr_Mensaje = String.Empty;

            String lstr_ResEnviarRev = String.Empty;

            String lstr_envio = String.Empty;
            String lstr_Resultado = String.Empty;

            string lstr_Estado = get_link_text().ToLower().Equals("anular") ? "Inactivo" : "InactivoFinanciero";//"Inactivo";
            string[] lstr_respuesta=new string[2];
            gstr_IdExpediente = ((LinkButton)sender).CommandArgument.ToString();
            //gstr_IdExpediente = this.grdExpedientes.SelectedDataKey.Value.ToString();

            #region existe Pretención Inicial??
            lds_ConsultarExpediente = ws_SGService.uwsConsultarExpedienteXNumero(gstr_IdExpediente, clsSesion.Current.SociedadUsr);
            if ((lds_ConsultarExpediente.Tables["Table"] != null) && (lds_ConsultarExpediente.Tables.Count > 0))
            //&& (!lbool_TieneRP1))
            {
                ldr_ConsultarExpediente = lds_ConsultarExpediente.Tables["Table"].Rows[0];
                if (!String.IsNullOrEmpty(ldr_ConsultarExpediente["MontoPretensionColones"].ToString()))
                {
                    lbool_TienePretensionInicial = true; // Sí existe Pretension Inicial [ePI]
                    if (!lbool_TieneRP1)
                        gdec_MontoPrincipalColRev = Convert.ToDecimal(ldr_ConsultarExpediente["MontoPretensionColones"], CultureInfo.InvariantCulture);
                }
                else
                {
                    lbool_TienePretensionInicial = false; // No existe Pretension Inicial [nePI]
                }
            }
            #endregion

            #region existe Resolución?
            TieneResolucion();
            #endregion

            String lstr_query = "SELECT IdExp FROM co.Expedientes exp " +
                            "WHERE exp.IdExpediente ='" + gstr_IdExpediente + "' " +
                            "AND exp.IdSociedadGL ='" + gstr_IdSociedadGL + "' " +
                            "AND exp.EstadoExpediente = 'Activo'";
            DataTable dt_Resoluciones = GetData(lstr_query);

            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            try
            {
                Int32 lint_IdExp = 0;

                foreach (DataRow dr_Resolucion in dt_Resoluciones.Rows)
                {
                    lint_IdExp = Convert.ToInt32(dr_Resolucion["IdExp"]);
                }

                DataTable ldt_CobrosPagos = ConsultarCobrosPagos(gstr_IdExpediente, lint_IdExp); // Falta el id Res

                if (ldt_CobrosPagos.Rows.Count==0)
                {
                    lstr_Resultado = "Contabilizado";
                }

                foreach (DataRow dr_CobrosPagos in ldt_CobrosPagos.Rows)
                {
                    String lstr_IdOperacion = dr_CobrosPagos["IdResolucionFK"].ToString();

                    Decimal lstr_Monto1 = Convert.ToDecimal("0" + dr_CobrosPagos["MontoPrincipal"], CultureInfo.InvariantCulture);
                    Decimal lstr_Monto2 = Convert.ToDecimal("0" + dr_CobrosPagos["MontoPrincipalColones"], CultureInfo.InvariantCulture);
                    Decimal lstr_Monto3 = Convert.ToDecimal("0" + dr_CobrosPagos["MontoPrincipalColonesCierre"], CultureInfo.InvariantCulture);
                    Decimal lstr_Monto4 = Convert.ToDecimal("0" + dr_CobrosPagos["MontoIntereses"], CultureInfo.InvariantCulture);
                    Decimal lstr_Monto5 = Convert.ToDecimal("0" + dr_CobrosPagos["MontoInteresesColones"], CultureInfo.InvariantCulture);
                    Decimal lstr_Monto6 = Convert.ToDecimal("0" + dr_CobrosPagos["MontoInteresesColonesCierre"], CultureInfo.InvariantCulture);
                    Decimal lstr_Monto7 = Convert.ToDecimal("0" + dr_CobrosPagos["ValorPresentePrincipal"], CultureInfo.InvariantCulture);
                    Decimal lstr_Monto8 = Convert.ToDecimal("0" + dr_CobrosPagos["ValorPresentePrinColones"], CultureInfo.InvariantCulture);
                    Decimal lstr_Monto9 = Convert.ToDecimal("0" + dr_CobrosPagos["ValorPresentePrinColonesCierre"], CultureInfo.InvariantCulture);
                    Decimal lstr_Monto10 = Convert.ToDecimal("0" + dr_CobrosPagos["ValorPresenteIntereses"], CultureInfo.InvariantCulture);
                    Decimal lstr_Monto11 = Convert.ToDecimal("0" + dr_CobrosPagos["ValorPresenteInteresColones"], CultureInfo.InvariantCulture);
                    Decimal lstr_Monto12 = Convert.ToDecimal("0" + dr_CobrosPagos["ValorPresenteInteresColonesCierre"], CultureInfo.InvariantCulture);
                    Decimal lstr_Monto13 = Convert.ToDecimal("0" + dr_CobrosPagos["Intereses"], CultureInfo.InvariantCulture);
                    Decimal lstr_Monto14 = Convert.ToDecimal("0" + dr_CobrosPagos["InteresesColones"], CultureInfo.InvariantCulture);
                    Decimal lstr_Monto15 = Convert.ToDecimal("0" + dr_CobrosPagos["InteresesColonesCierre"], CultureInfo.InvariantCulture);
                    Decimal lstr_Monto16 = Convert.ToDecimal("0" + dr_CobrosPagos["InteresesMoratorios"], CultureInfo.InvariantCulture);

                    String gstr_InModuloCT = "IdModulo In ('CT')";
                    String gstr_Transaccion = "Reversion";
                    String gstr_Leyenda = String.Empty;
                    Decimal[] garrdec_Montos;
                    Boolean glbool_cambioMonto = false;
                    Int32 gint_CantidadLineasAsiento = 16;

                    garrdec_Montos = new Decimal[16];
                    garrdec_Montos[0] = lstr_Monto1;
                    garrdec_Montos[1] = lstr_Monto2;
                    garrdec_Montos[2] = lstr_Monto3;
                    garrdec_Montos[3] = lstr_Monto4;
                    garrdec_Montos[4] = lstr_Monto5;
                    garrdec_Montos[5] = lstr_Monto6;
                    garrdec_Montos[6] = lstr_Monto7;
                    garrdec_Montos[7] = lstr_Monto8;
                    garrdec_Montos[8] = lstr_Monto9;
                    garrdec_Montos[9] = lstr_Monto10;
                    garrdec_Montos[10] = lstr_Monto11;
                    garrdec_Montos[11] = lstr_Monto12;

                    String lstr_Operacion = RetornaAsientos(lstr_IdOperacion);
                    asiento.definirExpediente(gstr_IdExpediente, gstr_IdSociedadGL, gstr_Usuario);
                    string lstr_CodAsiento = "";
                    lstr_Resultado = asiento.enviar(gstr_InModuloCT, lstr_Operacion, gstr_Transaccion, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, lstr_Monto2, lstr_Monto4, out lstr_CodAsiento);
                    //lstr_Resultado = EnviarAsientos(gstr_InModuloCT, lstr_Operacion, gstr_IdExpediente, gstr_Transaccion, gstr_Leyenda, glbool_cambioMonto, garrdec_Montos, gint_CantidadLineasAsiento, 0, 0, 0, null, 0, null);
                                         
                }
            }
            catch(Exception exp)
            {}
            // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            if (lstr_Resultado.Contains("Contabilizado"))
            {
                lds_ConsultarBitacora = ws_SGService.uwsConsultarBitacorasAsientos("", "", gstr_IdExpediente, gstr_IdSociedadGL, "", "CT");

                if (!lbool_TieneRF && (lbool_TienePretensionInicial || lbool_TieneRP1 || lbool_TieneRP2))
                {
                    lstr_envio = EnviarRevelacion(gstr_IdExpediente, 3, gdec_MontoPrincipalColRev, gdec_MontoPrincipalColRev); // suma monto y cant

                    if (lstr_envio.Contains("exito"))
                    {
                        //    ws_SGService.uwsModificarCodigoAsientoCo(lint_IdRes, 0, gstr_AsientosResultado, gstr_IdExpediente, gstr_IdSociedadGL, lstr_CodAsiento, gstr_Usuario);
                        lstr_respuesta = ws_SGService.uwsAnularExpediente(gstr_IdExpediente, lstr_Estado, clsSesion.Current.SociedadUsr);
                    }
                    else
                    {
                        lstr_respuesta[0] = "99";
                        lstr_respuesta[1] = lstr_envio;
                    }
                }
                else
                {
                    lstr_respuesta = ws_SGService.uwsAnularExpediente(gstr_IdExpediente, lstr_Estado, clsSesion.Current.SociedadUsr);
                }
            }
            else
            {
                lstr_respuesta[0] = "99";
                lstr_respuesta[1] = lstr_Resultado;
            }

            if (lstr_respuesta[0].Contains("00"))
            {
                string strMsg = String.Format("Se {0} satisfactoriamente el expediente.", get_link_text().ToLower().Equals("anular")?"anuló":"pago");
                Response.Write("<script>alert('" + strMsg + "')</script>");
            }
            else if (lstr_respuesta[0].Contains("99") || lstr_respuesta[0].Contains("Codigo :-") || lstr_respuesta[0].Contains("Codigo:") || lstr_respuesta[0].Contains("Codigo :") || lstr_respuesta[0].Contains("Codigo :-6"))
            {
                string strMsg = String.Format("No se pudo {0} el expediente.", get_link_text().ToLower());
                Response.Write("<script>alert('" + strMsg + "')</script>");
            }
            CargarDatosExpedientes();
        }

        private string EnviarRevelacion(String str_NumExpediente, int int_proceso,
            Decimal dec_Monto, Decimal dec_MontoReversar)
        {
            #region Variables
            string lstr_Respuesta = string.Empty;
            string lstr_ErrorConsolidado = string.Empty;

            string lstr_MontoTotalColones = string.Empty;
            string lstr_ValorPresente = string.Empty;
            string lstr_TipoProceso = string.Empty;

            string lstr_IdTipoProceso = string.Empty;
            string lstr_Ministerio = string.Empty;

            string[] larrstr_RevelacionResultado = new string[2];

            Decimal ldec_MontoAjuste = 0;
            string lstr_MontoAjuste = string.Empty;
            string lstr_Monto = string.Empty;
            string lstr_MontoReversar = string.Empty;
            #endregion

            try
            {
                #region PreEnvio
                string lstr_TipoExpediente = ConsultarTipoExpediente(str_NumExpediente);

                #region existe Pretención Inicial??
                DataSet lds_ConsultarExpediente = new DataSet();
                DataRow ldr_ConsultarExpediente = null;

                lds_ConsultarExpediente = ws_SGService.uwsConsultarExpedienteXNumero(str_NumExpediente, clsSesion.Current.SociedadUsr);

                if ((lds_ConsultarExpediente.Tables["Table"] != null) && (lds_ConsultarExpediente.Tables.Count > 0))
                {
                    ldr_ConsultarExpediente = lds_ConsultarExpediente.Tables["Table"].Rows[0];
                    lstr_TipoProceso = ldr_ConsultarExpediente["TipoProcesoExpediente"].ToString();
                    lstr_ValorPresente = ldr_ConsultarExpediente["MontoValorPresente"].ToString();
                    lstr_MontoTotalColones = ldr_ConsultarExpediente["MontoPretensionColones"].ToString();

                    if (String.IsNullOrEmpty(lstr_MontoTotalColones))
                        lstr_MontoTotalColones = "0.00";
                }
                #endregion

                // Se Obtiene el  Id de TipoProceso que tiene el Expediente
                string lstr_ConsultaTP = "SELECT IdCatalogo,oc.IdOpcion,oc.NomOpcion FROM ma.OpcionesCatalogos oc WHERE oc.IdCatalogo='30' AND Estado = 'A' AND oc.NomOpcion='" + lstr_TipoProceso + "'";
                DataTable ldt_TipoProceso = GetData(lstr_ConsultaTP);//Sacamos el tipo de proceso
                DataRow ldr_TipoProceso = (ldt_TipoProceso.Rows.Count > 0) ? ldt_TipoProceso.Rows[0] : ldt_TipoProceso.NewRow();//Asignamos y validamos dataTables
                lstr_IdTipoProceso = ldr_TipoProceso["IdOpcion"].ToString();
                lstr_Ministerio = (clsSesion.Current.SociedadUsr == null) ? "Ministerio desconocido." : clsSesion.Current.SociedadUsr;

                // Se verifica la existencia de periodo en Revelación y Notas
                DateTime ldt_FechaActual = DateTime.Now;
                DateTime ldt_FchRevelacion = new DateTime();

                DataSet lds_RevelacionInfo = ws_SGService.uwsConsultarRevelacionContingente("", Convert.ToString(ldt_FechaActual.Year), Convert.ToString(ldt_FechaActual.Month));
                DataRow ldr_RevelacionInfo = null;

                if (lds_RevelacionInfo.Tables.Count > 0)
                {
                    ldr_RevelacionInfo = lds_RevelacionInfo.Tables[0].Rows[0];
                    ldt_FchRevelacion = Convert.ToDateTime(ldr_RevelacionInfo["FchModifica"].ToString());
                }
                else
                {
                    lstr_ErrorConsolidado = "No se encontró Periódo de Revelación en Nota. ";
                }

                //Insertamos en BD Revelacion
                //string lstr_FchModificaRevelacion = ldt_FchRevelacion.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                string lstr_FchModificaRevelacion = ldt_FchRevelacion.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                #endregion

                if (int_proceso == 0) //nuevo monto cantidad
                {
                    lstr_MontoAjuste = ((double)dec_Monto).ToString();

                    if (lstr_TipoExpediente.Equals("Actor"))
                        larrstr_RevelacionResultado = ws_SGService.uwsActualizarRevConTotalActivos(ldr_RevelacionInfo["IdRevCont"].ToString(), lstr_Ministerio, lstr_IdTipoProceso, lstr_MontoAjuste, "1", "0.00", ldt_FchRevelacion, 1); //suma monto cant
                    else if (lstr_TipoExpediente.Equals("Demandado"))
                        larrstr_RevelacionResultado = ws_SGService.uwsActualizarRevConTotalPasivos(ldr_RevelacionInfo["IdRevCont"].ToString(), lstr_Ministerio, lstr_IdTipoProceso, lstr_MontoAjuste, "1", "0.00", ldt_FchRevelacion, 1);
                }
                else if (int_proceso == 1) // Ajustar monto
                {
                    if (dec_Monto < dec_MontoReversar)
                    {
                        ldec_MontoAjuste = dec_MontoReversar - dec_Monto;
                        lstr_MontoAjuste = ((double)ldec_MontoAjuste).ToString();

                        if (lstr_TipoExpediente.Equals("Actor"))
                            larrstr_RevelacionResultado = ws_SGService.uwsActualizarRevConTotalActivos(ldr_RevelacionInfo["IdRevCont"].ToString(), lstr_Ministerio, lstr_IdTipoProceso, lstr_MontoAjuste, "1", "0.00", ldt_FchRevelacion, 4); //suma monto
                        else if (lstr_TipoExpediente.Equals("Demandado"))
                            larrstr_RevelacionResultado = ws_SGService.uwsActualizarRevConTotalPasivos(ldr_RevelacionInfo["IdRevCont"].ToString(), lstr_Ministerio, lstr_IdTipoProceso, lstr_MontoAjuste, "1", "0.00", ldt_FchRevelacion, 4);
                    }
                    else if (dec_Monto > dec_MontoReversar)
                    {
                        ldec_MontoAjuste = dec_Monto - dec_MontoReversar;
                        lstr_MontoAjuste = ((double)ldec_MontoAjuste).ToString();

                        if (lstr_TipoExpediente.Equals("Actor"))
                            larrstr_RevelacionResultado = ws_SGService.uwsActualizarRevConTotalActivos(ldr_RevelacionInfo["IdRevCont"].ToString(), lstr_Ministerio, lstr_IdTipoProceso, lstr_MontoAjuste, "1", "0.00", ldt_FchRevelacion, 5); //resta monto
                        else if (lstr_TipoExpediente.Equals("Demandado"))
                            larrstr_RevelacionResultado = ws_SGService.uwsActualizarRevConTotalPasivos(ldr_RevelacionInfo["IdRevCont"].ToString(), lstr_Ministerio, lstr_IdTipoProceso, lstr_MontoAjuste, "1", "0.00", ldt_FchRevelacion, 5); //resta monto
                    }
                    else
                    {
                        larrstr_RevelacionResultado[0] = "00";
                    }
                }
                if (int_proceso == 2) // Intercambio de monto
                {
                    lstr_Monto = ((double)dec_Monto).ToString();
                    lstr_MontoReversar = ((double)dec_MontoReversar).ToString();

                    if (lstr_TipoExpediente.Equals("Actor"))
                    {
                        larrstr_RevelacionResultado = ws_SGService.uwsActualizarRevConTotalActivos(ldr_RevelacionInfo["IdRevCont"].ToString(), lstr_Ministerio, lstr_IdTipoProceso, lstr_Monto, "1", lstr_MontoReversar, ldt_FchRevelacion, 2); // resta, suma : monto, cant
                    }
                    else if (lstr_TipoExpediente.Equals("Demandado"))
                    {
                        larrstr_RevelacionResultado = ws_SGService.uwsActualizarRevConTotalPasivos(ldr_RevelacionInfo["IdRevCont"].ToString(), lstr_Ministerio, lstr_IdTipoProceso, lstr_Monto, "1", lstr_MontoReversar, ldt_FchRevelacion, 2); // resta, suma : monto, cant
                    }
                }
                if (int_proceso == 3) // Resta monto y cant, PI
                {
                    lstr_MontoTotalColones = String.Empty;
                    lstr_MontoTotalColones = ((double)dec_Monto).ToString();

                    if (lstr_TipoExpediente.Equals("Actor"))
                    {
                        larrstr_RevelacionResultado = ws_SGService.uwsActualizarRevConTotalActivos(ldr_RevelacionInfo["IdRevCont"].ToString(), lstr_Ministerio, lstr_IdTipoProceso, lstr_MontoTotalColones, "1", "0.00", ldt_FchRevelacion, 3);
                    }
                    else if (lstr_TipoExpediente.Equals("Demandado"))
                    {
                        larrstr_RevelacionResultado = ws_SGService.uwsActualizarRevConTotalPasivos(ldr_RevelacionInfo["IdRevCont"].ToString(), lstr_Ministerio, lstr_IdTipoProceso, lstr_MontoTotalColones, "1", "0.00", ldt_FchRevelacion, 3);
                    }

                }

                //Validamos resultado para desplegar mensaje al usuario
                if (!String.IsNullOrEmpty(larrstr_RevelacionResultado[0]))
                {
                    //Crea la revelacion en  nota del expediente despues de crear la pretension
                    if (larrstr_RevelacionResultado[0].Contains("00"))
                    {
                        ws_SGService.uwsRegistrarAccionBitacora("CT", gstr_Usuario, "Envio Revelación", str_NumExpediente + ":" + clsSesion.Current.SociedadUsr +
                        "\nMonto Actual: " + dec_Monto + " Monto Anterior: " + dec_MontoReversar + "\nRespuesta: Satisfactorio.");
                        //lstr_Respuesta = "Se generó, la revelación en nota del expediente satisfactoriamente.";
                        lstr_Respuesta = "exito";
                    }
                    else if (larrstr_RevelacionResultado[0].Contains("99"))
                    {
                        //lstr_Respuesta = "No se generó, la revelación en nota del expediente. Falló la comunicación con el módulo de Revelación en Nota.";
                        lstr_Respuesta = "fallo";
                        ws_SGService.uwsRegistrarAccionBitacora("CT", gstr_Usuario, "Reg. Rev.", str_NumExpediente + ":" + clsSesion.Current.SociedadUsr +
                        "\nError: " + lstr_ErrorConsolidado + " | " + larrstr_RevelacionResultado[0] + ": " + larrstr_RevelacionResultado[1]);
                    }
                }
                else
                {
                    lstr_Respuesta = "fallo";

                    ws_SGService.uwsRegistrarAccionBitacora("CT", gstr_Usuario, "Envio Revelación", str_NumExpediente + ":" + clsSesion.Current.SociedadUsr +
                        "\nError: " + lstr_ErrorConsolidado + " | " + larrstr_RevelacionResultado[0] + " " + larrstr_RevelacionResultado[1]);
                }
            }
            catch (Exception err)
            {
                lstr_Respuesta = "fallo";

                ws_SGService.uwsRegistrarAccionBitacora("CT", gstr_Usuario, "Envio Revelación", str_NumExpediente + ":" + clsSesion.Current.SociedadUsr +
                        "\nError: " + larrstr_RevelacionResultado + " : " + err.Message);
                lstr_Respuesta = "No se generó, la revelación en nota del expediente. Falló la comunicación con el módulo de Revelación en Nota.";

            }

            return lstr_Respuesta;
        }

        private string ConsultarTipoExpediente(string idexpediente)
        {
            string str_consul = "SELECT TipoExpediente FROM co.Expedientes where IdExpediente='" + idexpediente + "' and IdSociedadGL='" + clsSesion.Current.SociedadUsr + "' AND EstadoExpediente = 'Activo'";
            string tipoExp = string.Empty;
            //Consultar Expedientes
            DataTable exped = GetData(str_consul);
            if (exped.Rows.Count > 0)
            {
                DataRow campo = exped.Rows[0];
                tipoExp = campo["TipoExpediente"].ToString();
            }

            return tipoExp;
        }

        private string ConsultarExpedientesAnular(string idexpediente)
        {
            string str_consul = "SELECT IdExp FROM co.Expedientes where IdExpediente='" + idexpediente + "' and IdSociedadGL='" + clsSesion.Current.SociedadUsr + "' AND EstadoExpediente = 'Activo'";
            string tipoExp = string.Empty;
            //Consultar Expedientes
            DataTable exped = GetData(str_consul);
            if (exped.Rows.Count > 0)
            {
                DataRow campo = exped.Rows[0];
                tipoExp = campo["TipoExpediente"].ToString();
            }

            return tipoExp;
        }

        private int ConsultarIdExpediente(string idexpediente)
        {
            string str_consul = "SELECT IdExp FROM co.Expedientes where IdExpediente='" + idexpediente + "' and IdSociedadGL='" + clsSesion.Current.SociedadUsr + "' AND EstadoExpediente = 'Activo'";
            int IdExp = 0;
            //Consultar ID Expedientes 
            DataTable exped = GetData(str_consul);
            if (exped.Rows.Count > 0)
            {
                DataRow campo = exped.Rows[0];
                IdExp = Convert.ToInt32(campo["IdExp"].ToString());
            }

            return IdExp;
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

        private String RetornaAsientos(string str_Asiento)
        {
            #region Reversiones
            string lstr_AsientoFinal = string.Empty;
            gbool_CambioAno = (gint_Periodo < DateTime.Today.Year);
            switch (str_Asiento)
            {
                case "CT01":
                    {
                        lstr_AsientoFinal = gbool_CambioAno? "CT61":"CT60";
                    }
                    break;
                case "CT02":
                    {
                        lstr_AsientoFinal = gbool_CambioAno ? "CT63" : "CT62";  
                    }
                    break;
                case "CT03":
                    {
                        lstr_AsientoFinal = "CT64";
                    }
                    break;
                case "CT04":
                    {
                        lstr_AsientoFinal = "CT65";
                    }
                    break;
                case "CT05":
                    {
                        lstr_AsientoFinal = "CT66";
                    }
                    break;
                case "CT06":
                    {
                        lstr_AsientoFinal = gbool_CambioAno ? "CT68" : "CT67";
                    }
                    break;
                case "CT07":
                    {
                        lstr_AsientoFinal = "CT69";
                    }
                    break;
                case "CT08":
                    {
                        lstr_AsientoFinal = "CT70";
                    }
                    break;
                case "CT09":
                    {
                        lstr_AsientoFinal = gbool_CambioAno ? "CT72" : "CT71";
                    }
                    break;
                case "CT10":
                    {
                        lstr_AsientoFinal = gbool_CambioAno ? "CT74" : "CT73";
                    }
                    break;
                case "CT13":
                    {
                        lstr_AsientoFinal = gbool_CambioAno ? "CT76" : "CT75";
                    }
                    break;
                case "CT14":
                    {
                        lstr_AsientoFinal = "CT77";
                    }
                    break;
                case "CT15":
                    {
                        lstr_AsientoFinal = "CT78";
                    }
                    break;
                case "CT16":
                    {
                        lstr_AsientoFinal = "CT79";
                    }
                    break;
                case "CT17":
                    {
                        lstr_AsientoFinal = "CT80";
                    }
                    break;
                case "CT18":
                    {
                        lstr_AsientoFinal = "CT81";
                    }
                    break;
                case "CT19":
                    {
                        lstr_AsientoFinal = "CT82";
                    }
                    break;
                case "CT20":
                    {
                        lstr_AsientoFinal = "CT83";
                    }
                    break;
                case "CT21":
                    {
                        lstr_AsientoFinal = "CT84";
                    }
                    break;
                case "CT22":
                    {
                        lstr_AsientoFinal = gbool_CambioAno ? "CT86" : "CT85";
                    }
                    break;
                case "CT23":
                    {
                        lstr_AsientoFinal = gbool_CambioAno ? "CT88" : "CT87";
                    }
                    break;
                case "CT24":
                    {
                        lstr_AsientoFinal = gbool_CambioAno ? "CT90" : "CT89";
                    }
                    break;
                case "CT25":
                    {
                        lstr_AsientoFinal = gbool_CambioAno ? "CT92" : "CT91";
                    }
                    break;
                case "CT26":
                    {
                        lstr_AsientoFinal = "CT93";
                    }
                    break;
                case "CT27":
                    {
                        lstr_AsientoFinal = "CT94";
                    }
                    break;
                case "CT28":
                    {
                        lstr_AsientoFinal = gbool_CambioAno ? "CT96" : "CT95";
                    }
                    break;
                case "CT29":
                    {
                        lstr_AsientoFinal = gbool_CambioAno ? "CT98" : "CT97";
                    }
                    break;
                case "CT30":
                    {
                        lstr_AsientoFinal = gbool_CambioAno ? "CT100" : "CT99";
                    }
                    break;
                case "CT32":
                    {
                        lstr_AsientoFinal = gbool_CambioAno ? "CT102" : "CT101";
                    }
                    break;
                case "CT34":
                    {
                        lstr_AsientoFinal = "CT103";
                    }
                    break;
                case "CT35":
                    {
                        lstr_AsientoFinal = "CT104";
                    }
                    break;
                case "CT36":
                    {
                        lstr_AsientoFinal = "CT105";
                    }
                    break;
                case "CT38":
                    {
                        lstr_AsientoFinal = "CT106";
                    }
                    break;
                case "CT39":
                    {
                        lstr_AsientoFinal = "CT107";
                    }
                    break;                    
                case "CT60":
                    {
                        lstr_AsientoFinal = "CT01";
                    }
                    break;
                case "CT61":
                    {
                        lstr_AsientoFinal = "CT01";
                    }
                    break;
                case "CT62":
                    {
                        lstr_AsientoFinal = "CT02";
                    }
                    break;
                case "CT63":
                    {
                        lstr_AsientoFinal = "CT02";
                    }
                    break;
                case "CT64":
                    {
                        lstr_AsientoFinal = "CT03";
                    }
                    break;
                case "CT65":
                    {
                        lstr_AsientoFinal = "CT04";
                    }
                    break;
                case "CT66":
                    {
                        lstr_AsientoFinal = "CT05";
                    }
                    break;
                case "CT67":
                    {
                        lstr_AsientoFinal = "CT06";
                    }
                    break;
                case "CT68":
                    {
                        lstr_AsientoFinal = "CT06";
                    }
                    break;
                case "CT69":
                    {
                        lstr_AsientoFinal = "CT07";
                    }
                    break;
                case "CT70":
                    {
                        lstr_AsientoFinal = "CT08";
                    }
                    break;
                case "CT71":
                    {
                        lstr_AsientoFinal = "CT09";
                    }
                    break;
                case "CT72":
                    {
                        lstr_AsientoFinal = "CT09";
                    }
                    break;
                case "CT73":
                    {
                        lstr_AsientoFinal = "CT10";
                    }
                    break;
                case "CT74":
                    {
                        lstr_AsientoFinal = "CT10";
                    }
                    break;
                case "CT75":
                    {
                        lstr_AsientoFinal = "CT13";
                    }
                    break;
                case "CT76":
                    {
                        lstr_AsientoFinal = "CT13";
                    }
                    break;
                case "CT77":
                    {
                        lstr_AsientoFinal = "CT14";
                    }
                    break;
                case "CT78":
                    {
                        lstr_AsientoFinal = "CT15";
                    }
                    break;
                case "CT79":
                    {
                        lstr_AsientoFinal = "CT16";
                    }
                    break;
                case "CT80":
                    {
                        lstr_AsientoFinal = "CT17";
                    }
                    break;
                case "CT81":
                    {
                        lstr_AsientoFinal = "CT18";
                    }
                    break;
                case "CT82":
                    {
                        lstr_AsientoFinal = "CT19";
                    }
                    break;
                case "CT83":
                    {
                        lstr_AsientoFinal = "CT20";
                    }
                    break;
                case "CT84":
                    {
                        lstr_AsientoFinal = "CT21";
                    }
                    break;
                case "CT85":
                    {
                        lstr_AsientoFinal = "CT22";
                    }
                    break;
                case "CT86":
                    {
                        lstr_AsientoFinal = "CT22";
                    }
                    break;
                case "CT87":
                    {
                        lstr_AsientoFinal = "CT23";
                    }
                    break;
                case "CT88":
                    {
                        lstr_AsientoFinal = "CT23";
                    }
                    break;
                case "CT89":
                    {
                        lstr_AsientoFinal = "CT24";
                    }
                    break;
                case "CT90":
                    {
                        lstr_AsientoFinal = "CT24";
                    }
                    break;
                case "CT91":
                    {
                        lstr_AsientoFinal = "CT25";
                    }
                    break;
                case "CT92":
                    {
                        lstr_AsientoFinal = "CT25";
                    }
                    break;
                case "CT93":
                    {
                        lstr_AsientoFinal = "CT26";
                    }
                    break;
                case "CT94":
                    {
                        lstr_AsientoFinal = "CT27";
                    }
                    break;
                case "CT95":
                    {
                        lstr_AsientoFinal = "CT28";
                    }
                    break;
                case "CT96":
                    {
                        lstr_AsientoFinal = "CT28";
                    }
                    break;
                case "CT97":
                    {
                        lstr_AsientoFinal = "CT29";
                    }
                    break;
                case "CT98":
                    {
                        lstr_AsientoFinal = "CT29";
                    }
                    break;
                case "CT99":
                    {
                        lstr_AsientoFinal = "CT30";
                    }
                    break;
                case "CT100":
                    {
                        lstr_AsientoFinal = "CT30";
                    }
                    break;
                case "CT101":
                    {
                        lstr_AsientoFinal = "CT32";
                    }
                    break;
                case "CT102":
                    {
                        lstr_AsientoFinal = "CT32";
                    }
                    break;
                case "CT103":
                    {
                        lstr_AsientoFinal = "CT34";
                    }
                    break;
                case "CT104":
                    {
                        lstr_AsientoFinal = "CT35";
                    }
                    break;
                case "CT105":
                    {
                        lstr_AsientoFinal = "CT36";
                    }
                    break;
                case "CT106":
                    {
                        lstr_AsientoFinal = "CT38";
                    }
                    break;
                case "CT107":
                    {
                        lstr_AsientoFinal = "CT39";
                    }
                    break;
            }
            #endregion
            return lstr_AsientoFinal;
        }

        private DataTable ConsultarCobrosPagos(string str_IdExpedienteFK, int int_IdRes) 
        {
            return GetData("SELECT * FROM co.CobrosPagos where IdExpedienteFK = '" + str_IdExpedienteFK + "' and IdRes = " + int_IdRes );
        }


    }
}