using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Presentacion.Compartidas;
//using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.HtmlControls;
using System.Globalization;
using Logica.SubirArchivo;
using LogicaNegocio.Seguridad;

namespace Presentacion.Contingentes
{
    public partial class Pretenciones : BASE
    {
        #region variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private tSeguridad Seguridad = new tSeguridad();

        private TextBox SourceTextBox;
        private const String dataFmt = "{0,26}{1,8}{2,26}";

        private String str_Mensaje_Error = String.Empty;
        private String gstr_Usuario = String.Empty;
        private String gstr_Sociedad = String.Empty;

        //private String gstr_IdExpediente = String.Empty;
        protected String gstr_IdExpediente
        {
            get
            {
                if (ViewState["gstr_IdExpediente"] == null)
                    ViewState["gstr_IdExpediente"] = null;
                return (String)ViewState["gstr_IdExpediente"];
            }
            set
            {
                ViewState["gstr_IdExpediente"] = value;
            }
        }
        //private String gstr_TipoExpediente = String.Empty;
        protected String gstr_TipoExpediente
        {
            get
            {
                if (ViewState["gstr_TipoExpediente"] == null)
                    ViewState["gstr_TipoExpediente"] = null;
                return (String)ViewState["gstr_TipoExpediente"];
            }
            set
            {
                ViewState["gstr_TipoExpediente"] = value;
            }
        }
        //private String gstr_TipoProceso = String.Empty;
        protected String gstr_TipoProceso
        {
            get
            {
                if (ViewState["gstr_TipoProceso"] == null)
                    ViewState["gstr_TipoProceso"] = null;
                return (String)ViewState["gstr_TipoProceso"];
            }
            set
            {
                ViewState["gstr_TipoProceso"] = value;
            }
        }
        //private String gstr_IdRevelacion = String.Empty;
        protected String gstr_IdRevelacion
        {
            get
            {
                if (ViewState["gstr_IdRevelacion"] == null)
                    ViewState["gstr_IdRevelacion"] = null;
                return (String)ViewState["gstr_IdRevelacion"];
            }
            set
            {
                ViewState["gstr_IdRevelacion"] = value;
            }
        }

        //private String gstr_Moneda = String.Empty;
        protected String gstr_Moneda
        {
            get
            {
                if (ViewState["gstr_Moneda"] == null)
                    ViewState["gstr_Moneda"] = null;
                return (String)ViewState["gstr_Moneda"];
            }
            set
            {
                ViewState["gstr_Moneda"] = value;
            }
        }
        //private String gstr_ValorPresente = String.Empty;
        protected String gstr_ValorPresente
        {
            get
            {
                if (ViewState["gstr_ValorPresente"] == null)
                    ViewState["gstr_ValorPresente"] = null;
                return (String)ViewState["gstr_ValorPresente"];
            }
            set
            {
                ViewState["gstr_ValorPresente"] = value;
            }
        }
        //private String gstr_MontoPretensionCol = String.Empty;
        protected String gstr_MontoPretensionCol
        {
            get
            {
                if (ViewState["gstr_MontoPretensionCol"] == null)
                    ViewState["gstr_MontoPretensionCol"] = null;
                return (String)ViewState["gstr_MontoPretensionCol"];
            }
            set
            {
                ViewState["gstr_MontoPretensionCol"] = value;
            }
        }
        //private String gstr_MontoPretensionAnt = String.Empty;
        protected String gstr_MontoPretensionAnt
        {
            get
            {
                if (ViewState["gstr_MontoPretensionAnt"] == null)
                    ViewState["gstr_MontoPretensionAnt"] = null;
                return (String)ViewState["gstr_MontoPretensionAnt"];
            }
            set
            {
                ViewState["gstr_MontoPretensionAnt"] = value;
            }
        }
        //private String gstr_MontoAjustado = String.Empty;
        protected String gstr_MontoAjustado
        {
            get
            {
                if (ViewState["gstr_MontoAjustado"] == null)
                    ViewState["gstr_MontoAjustado"] = null;
                return (String)ViewState["gstr_MontoAjustado"];
            }
            set
            {
                ViewState["gstr_MontoAjustado"] = value;
            }
        }

        //private Int32 gint_EstadoPretension;
        protected Int32 gint_EstadoPretension
        {
            get
            {
                if (ViewState["gint_EstadoPretension"] == null)
                    ViewState["gint_EstadoPretension"] = 0;
                return Convert.ToInt32(ViewState["gint_EstadoPretension"]);
            }
            set
            {
                ViewState["gint_EstadoPretension"] = value;
            }
        }

        //private Boolean gbool_TienePretencion;
        protected Boolean gbool_TienePretencion
        {
            get
            {
                if (ViewState["gbool_TienePretencion"] == null)
                    ViewState["gbool_TienePretencion"] = false;
                return (Boolean)ViewState["gbool_TienePretencion"];
            }
            set
            {
                ViewState["gbool_TienePretencion"] = value;
            }
        }
        //private Boolean gbool_TieneResolucion;
        protected Boolean gbool_TieneResolucion
        {
            get
            {
                if (ViewState["gbool_TieneResolucion"] == null)
                    ViewState["gbool_TieneResolucion"] = false;
                return (Boolean)ViewState["gbool_TieneResolucion"];
            }
            set
            {
                ViewState["gbool_TieneResolucion"] = value;
            }
        }

        //private Decimal gdec_Moneda = 0;
        protected Decimal gdec_Moneda
        {
            get
            {
                if (ViewState["gdec_Moneda"] == null)
                    ViewState["gdec_Moneda"] = 0.0;

                return Convert.ToDecimal(ViewState["gdec_Moneda"].ToString());
            }
            set
            {
                ViewState["gdec_Moneda"] = value;
            }
        }
        //private Decimal gdec_MontoPretensionAnt = 0;
        protected Decimal gdec_MontoPretensionAnt
        {
            get
            {
                if (ViewState["gdec_MontoPretensionAnt"] == null)
                    ViewState["gdec_MontoPretensionAnt"] = 0.0;

                return Convert.ToDecimal(ViewState["gdec_MontoPretensionAnt"].ToString());
            }
            set
            {
                ViewState["gdec_MontoPretensionAnt"] = value;
            }
        }
        //private Decimal gdec_MontoPretensionCol = 0;
        protected Decimal gdec_MontoPretensionCol
        {
            get
            {
                if (ViewState["gdec_MontoPretensionCol"] == null)
                    ViewState["gdec_MontoPretensionCol"] = 0.0;

                return Convert.ToDecimal(ViewState["gdec_MontoPretensionCol"].ToString());
            }
            set
            {
                ViewState["gdec_MontoPretensionCol"] = value;
            }
        }
        //private Decimal gdec_MontoAjustado = 0;
        protected Decimal gdec_MontoAjustado
        {
            get
            {
                if (ViewState["gdec_MontoAjustado"] == null)
                    ViewState["gdec_MontoAjustado"] = 0.0;

                return Convert.ToDecimal(ViewState["gdec_MontoAjustado"].ToString());
            }
            set
            {
                ViewState["gdec_MontoAjustado"] = value;
            }
        }
        //private Decimal gdec_MontoPretension = 0;
        protected Decimal gdec_MontoPretension
        {
            get
            {
                if (ViewState["gdec_MontoPretension"] == null)
                    ViewState["gdec_MontoPretension"] = 0.0;

                return Convert.ToDecimal(ViewState["gdec_MontoPretension"].ToString());
            }
            set
            {
                ViewState["gdec_MontoPretension"] = value;
            }
        }
        //private Decimal gdec_ValorPresente = 0;
        protected Decimal gdec_ValorPresente
        {
            get
            {
                if (ViewState["gdec_ValorPresente"] == null)
                    ViewState["gdec_ValorPresente"] = 0.0;

                return Convert.ToDecimal(ViewState["gdec_ValorPresente"].ToString());
            }
            set
            {
                ViewState["gdec_ValorPresente"] = value;
            }
        }
        //private Decimal gdec_TipoCambio = 0;
        protected Decimal gdec_TipoCambio
        {
            get
            {
                if (ViewState["gdec_TipoCambio"] == null)
                    ViewState["gdec_TipoCambio"] = 0.0;

                return Convert.ToDecimal(ViewState["gdec_TipoCambio"].ToString());
            }
            set
            {
                ViewState["gdec_TipoCambio"] = value;
            }
        }

        //Boolean gbool_TieneRP1 = false;
        protected Boolean gbool_TieneRP1
        {
            get
            {
                if (ViewState["gbool_TieneRP1"] == null)
                    ViewState["gbool_TieneRP1"] = false;
                return (Boolean)ViewState["gbool_TieneRP1"];
            }
            set
            {
                ViewState["gbool_TieneRP1"] = value;
            }
        }
        //Boolean gbool_TieneRP2 = false;
        protected Boolean gbool_TieneRP2
        {
            get
            {
                if (ViewState["gbool_TieneRP2"] == null)
                    ViewState["gbool_TieneRP2"] = false;
                return (Boolean)ViewState["gbool_TieneRP2"];
            }
            set
            {
                ViewState["gbool_TieneRP2"] = value;
            }
        }
        //Boolean gbool_TieneRF = false;
        protected Boolean gbool_TieneRF
        {
            get
            {
                if (ViewState["gbool_TieneRF"] == null)
                    ViewState["gbool_TieneRF"] = false;
                return (Boolean)ViewState["gbool_TieneRF"];
            }
            set
            {
                ViewState["gbool_TieneRF"] = value;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            gstr_Usuario = clsSesion.Current.LoginUsuario; 
            gstr_Sociedad = clsSesion.Current.SociedadUsr; 
            //gstr_Sociedad = "11208"; 
            //gstr_IdExpediente = Request.QueryString["id"];
            string[] tipocambio = new string[4];
            
            string Nuevo = Request.QueryString["isAdd"]; // fechaDemanda
            Session["isAdd"] = Nuevo;
            ViewState["Nuevo"] = Session["isAdd"];
            bool strNuevo = Convert.ToBoolean(ViewState["Nuevo"]);

            string idExpediente = Request.QueryString["id"]; // idExpediente
            Session["IdExp"] = Request.QueryString["id"];//Recibimos y almacenamos en la Session object para no perder valor entre postbacks
            string idExp = Convert.ToString(Session["IdExp"]);
            ViewState["IdExp"] = idExp;
            this.lblNumExpediente.Text = idExp;

            if (String.IsNullOrEmpty(gstr_IdExpediente))
                gstr_IdExpediente = DDLExpedientes.SelectedValue;

            gstr_TipoExpediente = ConsultarTipoExpediente(gstr_IdExpediente);

            if (this.DDLMoneda.SelectedItem.Value.Contains("USD"))
            {
                if (gstr_TipoExpediente.Equals("Actor"))
                    gdec_TipoCambio = this.txtCompra.Text == "" ? 0 : Decimal.Parse(this.txtCompra.Text);
                else if (gstr_TipoExpediente.Equals("Demandado"))
                    gdec_TipoCambio = this.txtVenta.Text == "" ? 0 : Decimal.Parse(this.txtVenta.Text);
            }
            else if (this.DDLMoneda.SelectedItem.Value.Contains("EUR"))
            {
                if (gstr_TipoExpediente.Equals("Actor"))
                {
                    Decimal ldec_compra = this.txtCompra.Text == "" ? 0 : Decimal.Parse(this.txtCompra.Text);
                    Decimal ldec_euro = this.txtEuro.Text == "" ? 0 : Decimal.Parse(this.txtEuro.Text);
                    gdec_TipoCambio = ldec_compra * ldec_euro;
                }
                else if (gstr_TipoExpediente.Equals("Demandado"))
                {
                    Decimal ldec_venta = this.txtVenta.Text == "" ? 0 : Decimal.Parse(this.txtVenta.Text);
                    Decimal ldec_euro = this.txtEuro.Text == "" ? 0 : Decimal.Parse(this.txtEuro.Text);
                    gdec_TipoCambio = ldec_venta * ldec_euro;
                }
            }
            else
                gdec_TipoCambio = 1;

                    if (Page.PreviousPage != null) // tomar valores de pagina anteriores 
                    {
                        SourceTextBox = (TextBox)PreviousPage.Form.FindControl("txtExpedientes");
                        if (SourceTextBox != null)
                        {
                            // idExpediente = SourceTextBox.Text;
                        }
                    }

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(gstr_Usuario))
                {
                    if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "OBJ_CT"))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else 
                    {
                        CargarMonedas();
                        CargarExpedientes();

                        tipocambio = CargarIndicadoresEco();
                        //cargar valores de tipo de cambio e indicadores económicos
                        this.txtCompra.Text = tipocambio[0];
                        this.txtVenta.Text = tipocambio[1];
                        this.txtEuro.Text = tipocambio[2];
                        this.txtTBP.Text = tipocambio[3];

                        //Setear valor anterior de numero de expediente a la pretension inicial
                        this.DDLExpedientes.Visible = false;
                        //Modificaamos resolucion
                        if (!strNuevo)//Viene del select del grid a modificar
                        {
                            gstr_IdExpediente = idExpediente;
                            this.DDLExpedientes.SelectedValue = gstr_IdExpediente;
                            CargarDatosPretensionesModificar(idExpediente);
                            this.lblNumExpediente.Visible = true;

                        }
                        else if (strNuevo)
                        {
                            this.DDLExpedientes.Visible = true;
                            this.lblNumExpediente.Visible = false;

                        }
                        gbool_TieneResolucion = false;
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

        private string[] CargarIndicadoresEco()
        {
            DataSet resultCompraUSD = new DataSet();
            DataSet resultVentaUSD = new DataSet();
            DataSet resultCompraEU = new DataSet();
            DataSet resultVentaEU = new DataSet();
            DataSet resultTBP = new DataSet();
            string[] resultado = new string[4];
            //invoacion a servicios
            resultCompraUSD = ws_SGService.uwsConsultarTiposCambio("CRCN", DateTime.Today, "3280", "N");//compra antes: 317
            resultVentaUSD = ws_SGService.uwsConsultarTiposCambio("CRCN", DateTime.Today, "3140", "N");//venta antes: 318
            resultCompraEU = ws_SGService.uwsConsultarTiposCambio("EUR", DateTime.Now, "333", "N");//euro
            resultTBP = ws_SGService.uwsConsultarValoresIndicadoresEco("TBP", DateTime.Now, "N");//TBP
            //seteo de resultados en array de strings
            ////DataSet1.Tables["Tu_Tabla"].Rows[0]["Nombre_Columna_1"].ToString(); obetenemos los datos del dataset de resultado
            resultado[0] = String.Format("{0:0.00}", resultCompraUSD.Tables[0].Rows.Count > 0 ? resultCompraUSD.Tables[0].Rows[0]["Valor"] : "00.00"); //condition ? first_expression : second_expression;
            resultado[1] = String.Format("{0:0.00}", resultVentaUSD.Tables[0].Rows.Count > 0 ? resultVentaUSD.Tables[0].Rows[0]["Valor"] : "00.00");
            resultado[2] = String.Format("{0:0.00}", resultCompraEU.Tables[0].Rows.Count > 0 ? resultCompraEU.Tables[0].Rows[0]["Valor"] : "00.00");
            resultado[3] = String.Format("{0:0.00}", resultTBP.Tables[0].Rows.Count > 0 ? resultTBP.Tables[0].Rows[0]["Valor"] : "00.00");
            return resultado;

        }

        protected void DDLExpedientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet lds_ConsultarResolucion = new DataSet();
            DataRow ldr_ConsultarResolucion = null;

            String lstr_Codigo = String.Empty;
            String lstr_Mensaje = String.Empty;

            gstr_IdExpediente = DDLExpedientes.SelectedValue;

            if (!gstr_IdExpediente.Equals("0"))
            {
                lds_ConsultarResolucion = ws_SGService.uwsConsultarResolucion("", gstr_IdExpediente, gstr_Sociedad, out lstr_Codigo, out lstr_Mensaje);
                if (lstr_Codigo.Contains("00"))
                {
                    gbool_TieneResolucion = true;
                    CargarDatosPretensionesModificar(gstr_IdExpediente);
                    BloqueaCampo(false);
                }
                else
                {
                    gbool_TieneResolucion = false;
                    BloqueaCampo(true);
                    CargarDatosPretensionesModificar(gstr_IdExpediente);
                }
            }
        }

        protected void DDLMoneda_SelectedIndexChanged(object sender, EventArgs e)
        {
            decimal venta = Decimal.Parse(this.txtVenta.Text);
            decimal compra = Decimal.Parse(this.txtCompra.Text);
            gdec_MontoPretension = this.txtMontoPretension.Text == "" ? 0 : Decimal.Parse(this.txtMontoPretension.Text);
            this.txtMontoPretension.Text = gdec_MontoPretension.ToString("N2");
            decimal euro = Decimal.Parse(this.txtEuro.Text);
            decimal TBP = Decimal.Parse(this.txtTBP.Text);

            if (this.txtPretensionColones.Text.Equals(""))
                this.txtPretensionColones.Text = "0";

            string expedienteid;
            if (this.DDLExpedientes.Visible == false)
            {
                expedienteid = this.lblNumExpediente.Text;
            }
            else
            {
                expedienteid = this.DDLExpedientes.SelectedValue;
            }

            //string campo = gstr_TipoExpediente;

            if (this.DDLMoneda.SelectedItem.Value.Contains("USD"))
            {
                if (gstr_TipoExpediente.Equals("Actor"))
                {
                    gdec_MontoPretensionCol = compra * gdec_MontoPretension;
                    this.txtPretensionColones.Text = gdec_MontoPretensionCol.ToString("N2"); //String.Format("{0:#,##0.##}", montoColPretension);
                    gdec_TipoCambio = Decimal.Parse(this.txtCompra.Text);
                }
                else if (gstr_TipoExpediente.Equals("Demandado"))
                {
                    gdec_MontoPretensionCol = venta * gdec_MontoPretension;
                    this.txtPretensionColones.Text = gdec_MontoPretensionCol.ToString("N2"); //String.Format("{0:#,##0.##}", montoColPretension);
                    gdec_TipoCambio = Decimal.Parse(this.txtVenta.Text);
                }
            }
            else if (this.DDLMoneda.SelectedItem.Value.Contains("EUR"))
            {
                if (gstr_TipoExpediente.Equals("Actor"))
                {
                    gdec_MontoPretensionCol = euro * compra * gdec_MontoPretension;
                    this.txtPretensionColones.Text = gdec_MontoPretensionCol.ToString("N2"); //String.Format("{0:#,##0.##}", montoColPretension);
                    gdec_TipoCambio = euro * compra;
                }
                else if (gstr_TipoExpediente.Equals("Demandado"))
                {

                    gdec_MontoPretensionCol = euro * venta * gdec_MontoPretension;
                    this.txtPretensionColones.Text = gdec_MontoPretensionCol.ToString("N2"); //String.Format("{0:#,##0.##}", montoColPretension);
                    gdec_TipoCambio = euro * venta;
                }
            }
            else if (this.DDLMoneda.SelectedItem.Value.Contains("CRC"))
            {

                gdec_MontoPretensionCol = this.txtMontoPretension.Text == "" ? 0 : Decimal.Parse(this.txtMontoPretension.Text);
                this.txtPretensionColones.Text = gdec_MontoPretensionCol.ToString("N2"); //String.Format("{0:#,##0.##}", montoColPretension);
                gdec_TipoCambio = 1;
            }
            //Valor presente
            DataSet dataParam = ws_SGService.uwsConsultarParametros("CT_Tiempo", "", Convert.ToDateTime("2016-01-05"), "", "");//str_IdParametro,str_IdModulo,dt_FchVigencia,str_DesParametro,str_TipoParametro
            if (dataParam.Tables.Count > 0)
            {
                DataTable dt = dataParam.Tables[0];
                DataRow campoT = dt.Rows[0];
                txtTiempo.Text = campoT["Valor"].ToString();
            }
            if (this.txtTiempo.Text == "0" || this.txtTiempo.Text == "")
            {
                gdec_ValorPresente = 0;//VF/1+i*n es n = tiempo de contabilidad Nacional
                txtValorPresente.Text = gdec_ValorPresente.ToString("N2"); //String.Format("{0:#,##0.##}", valorPresente);
            }
            else
            {
                //Valor presente principal colones
                double calc = Convert.ToDouble(1 + (TBP / 100));
                double tiempo = Convert.ToDouble(txtTiempo.Text);
                double divMonto = Math.Pow(calc, tiempo);
                double valorPresentePresentePricipal = Convert.ToDouble(this.txtPretensionColones.Text) / divMonto;//VF/1+i*n es n = tiempo de contabilidad Nacional
                txtValorPresente.Text = valorPresentePresentePricipal.ToString("N2");
                gdec_ValorPresente = decimal.Parse(txtValorPresente.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);

                //tiempoN = Convert.ToInt32(this.txtTiempo.Text);
                //valorPresente = montopretension / ((1 + TBP) * tiempoN);//VF/1+i*n es n = tiempo de contabilidad Nacional
                //txtValorPresente.Text = valorPresente.ToString("N2"); //String.Format("{0:#,##0.##}", valorPresente);
            }
        }

        private void LimpiarCampos()
        {
            this.txtMontoRembolsoPos.Text = "";
            this.DDLMoneda.SelectedValue = "0";
            this.txtMontoPretension.Text = "";
            this.CKEditorObservaciones.Text = "";
            this.txtFchVigencia.Text = string.Empty;
            gvFiles.DataSource = null;
            gvFiles.DataBind();
        }

        private void TieneResolucion(DataTable lds_ConsultarResolucion)
        {
            DataRow ldr_ConsultarResolucion;

            #region tiene RP2?
            if (lds_ConsultarResolucion.Rows.Count > 1)
            {
                ldr_ConsultarResolucion = lds_ConsultarResolucion.Rows[1];

                if (ldr_ConsultarResolucion["EstadoResolucion"].ToString().Equals("Provisional 2"))
                    gbool_TieneRP2 = true;
                else if (ldr_ConsultarResolucion["EstadoResolucion"].ToString().Equals("En Firme"))
                {
                    gbool_TieneRP1 = false;
                    gbool_TieneRP2 = false;
                    gbool_TieneRF = true;
                }
                else
                    gbool_TieneRP2 = false;
            }
            #endregion

            #region tiene RP1?
            else if (lds_ConsultarResolucion.Rows.Count == 1)
            {
                ldr_ConsultarResolucion = lds_ConsultarResolucion.Rows[0];
                if (ldr_ConsultarResolucion["EstadoResolucion"].ToString().Equals("Provisional 1"))
                    gbool_TieneRP1 = true;
                else if (ldr_ConsultarResolucion["EstadoResolucion"].ToString().Equals("Provisional 2"))
                {
                    gbool_TieneRP1 = false;
                    gbool_TieneRP2 = true;
                }
                else if (ldr_ConsultarResolucion["EstadoResolucion"].ToString().Equals("En Firme"))
                {
                    gbool_TieneRP1 = false;
                    gbool_TieneRP2 = false;
                    gbool_TieneRF = true;
                }
                else
                    gbool_TieneRP1 = false;
            }
            #endregion
        }

        private void CargarDatosPretensionesModificar(String idExpediente)
        {
            //InicializarCampos();
            string lstr_Codigo;
            string lstr_Mensaje;
            LimpiarCampos();
            DataSet lds_ConsultarResolucion = new DataSet();
            DataSet lds_Expedientes = new DataSet();
            DataTable ldt_Expedeinte = new DataTable();
            string fecha = DateTime.Now.ToString("dd/MM/yyyy");
                            
            lds_Expedientes = ws_SGService.uwsConsultarExpedienteXNumero(idExpediente, clsSesion.Current.SociedadUsr); 
            //lds_Expedientes = ws_SGService.uwsConsultarExpedienteXNumero(idExpediente,"11208");
            ldt_Expedeinte = lds_Expedientes.Tables[0];

            //Consulta los si la pretensión tiene RF, RP2, RP1
            lds_ConsultarResolucion = ws_SGService.uwsConsultarResolucion("", idExpediente, clsSesion.Current.SociedadUsr, out lstr_Codigo, out lstr_Mensaje); 
            //lds_ConsultarResolucion = ws_SGService.uwsConsultarResolucion("", idExpediente, "11208", out lstr_Codigo, out lstr_Mensaje);
            TieneResolucion(lds_ConsultarResolucion.Tables[0]);

            if (gbool_TieneRF || gbool_TieneRP2 || gbool_TieneRP1)
                BloqueaCampo(false);
            else
                BloqueaCampo(true);

            foreach (DataRow ldr_Expediente in ldt_Expedeinte.Rows)
            {
                if (ldr_Expediente["MontoPretension"].ToString() == "" && ldr_Expediente["MontoPretension"].ToString() == "" && ldr_Expediente["MontoPretensionColones"].ToString() == "" && ldr_Expediente["MonedaPretension"].ToString() == "")
                {
                    // this.lblNumExpediente.Visible = true;
                    gstr_TipoExpediente = ldr_Expediente["TipoExpediente"].ToString();
                    if (gstr_TipoExpediente.Contains("Actor"))
                        this.lbl_Fecha.Text = "Posible Fecha de Entrada de Recursos";
                    else
                        this.lbl_Fecha.Text = "Posible Fecha de Salida de Recursos";
                }
                else
                {
                    MessageBox.Show("El expediente " + this.lblNumExpediente.Text + " tiene una Pretensión Inicial asociada. Si se cambia algún dato se modificará, la Pretensión Inicial de éste expediente.");
                    this.lblNumExpediente.Text = ldr_Expediente["IdExpediente"].ToString();

                    string str_montopretencion = ldr_Expediente["MontoPretension"].ToString();
                    string str_montopretencioncolones = ldr_Expediente["MontoPretensionColones"].ToString();
                    string str_valorpresente = ldr_Expediente["MontoValorPresente"].ToString();

                    if (String.IsNullOrEmpty(str_montopretencion))
                        str_montopretencion = "0";
                    if (String.IsNullOrEmpty(str_montopretencioncolones))
                        str_montopretencioncolones = "0";
                    if (String.IsNullOrEmpty(str_valorpresente))
                        str_valorpresente = "0";

                    this.txtMontoPretension.Text = Decimal.Parse(str_montopretencion).ToString("N2");
                    this.txtPretensionColones.Text = Decimal.Parse(str_montopretencioncolones).ToString("N2");
                    this.txtValorPresente.Text = Decimal.Parse(str_valorpresente).ToString("N2");

                    this.DDLMoneda.SelectedValue = ldr_Expediente["MonedaPretension"].ToString().Equals(string.Empty) ? "0" : ldr_Expediente["MonedaPretension"].ToString();
                    this.DDLExpedientes.SelectedValue = idExpediente;
                    gstr_IdExpediente = idExpediente;

                    string Pruebba = ldr_Expediente["PosibleFechEntradaRecursos"].ToString();

                    if (!String.IsNullOrEmpty(ldr_Expediente["PosibleFechEntradaRecursos"].ToString()))
                    {
                        this.txtFchVigencia.Text = Convert.ToDateTime(ldr_Expediente["PosibleFechEntradaRecursos"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                    }

                    this.CKEditorObservaciones.Text = ldr_Expediente["ObservacionesPretension"].ToString();
                    gstr_TipoExpediente = ldr_Expediente["TipoExpediente"].ToString();

                    if (gstr_TipoExpediente.Contains("Actor"))
                        this.lbl_Fecha.Text = "Posible Fecha de Entrada de Recursos";
                    else
                        this.lbl_Fecha.Text = "Posible Fecha de Salida de Recursos";

                    // this.lblNumExpediente.Visible = true;
                }

                string consulta = "SELECT * from rn.Archivos WHERE IdExpediente='" + ldr_Expediente["IdExpediente"].ToString() + "' AND IdSociedadGL='" + gstr_Sociedad + "'  AND IdFormulario=" + ConsultarIdExpediente(ldr_Expediente["IdExpediente"].ToString());
           
                DataTable listaArchivos = GetData(consulta);
                if (listaArchivos.Rows.Count > 0)
                {
                    gvFiles.DataSource = listaArchivos;
                    gvFiles.DataBind();
                }
                else
                {
                    //Log.Error("Consulta no arrojó resultados, la tabla viene vacia al realizar la consulta.");
                }
            }
        }

        // Guardar datos de pretension inicial
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            String lstr_resultado = String.Empty;
            if (!String.IsNullOrEmpty(txtMontoPretension.Text) && !String.IsNullOrEmpty(txtPretensionColones.Text) && DDLExpedientes.SelectedIndex != 0 && DDLMoneda.SelectedIndex != 0)
            {
                lstr_resultado = GuardarPretensionInicial();
            }
            else
            {
                MessageBox.Show("No se puede guardar la pretensión, completar los datos obligatorios. \n");
            }
        }

        private void InicializarCampos()
        {
            DataSet lds_ConsultarExpediente = new DataSet();
            DataRow ldr_ConsultarExpediente = null;

            lds_ConsultarExpediente = ws_SGService.uwsConsultarExpedienteXNumero(gstr_IdExpediente, gstr_Sociedad);

            if ((lds_ConsultarExpediente.Tables["Table"] != null) && (lds_ConsultarExpediente.Tables.Count > 0))
            {
                ldr_ConsultarExpediente = lds_ConsultarExpediente.Tables["Table"].Rows[0];

                gstr_MontoPretensionAnt = ldr_ConsultarExpediente["MontoPretensionColones"].ToString();
                gstr_TipoExpediente = ldr_ConsultarExpediente["TipoExpediente"].ToString();
                gstr_TipoProceso = ldr_ConsultarExpediente["TipoProcesoExpediente"].ToString();
                gstr_ValorPresente = ldr_ConsultarExpediente["MontoValorPresente"].ToString();

                if (String.IsNullOrEmpty(gstr_MontoPretensionAnt))
                {
                    gbool_TienePretencion = false;
                    gstr_MontoPretensionAnt = "0.00";
                }
                else
                    gbool_TienePretencion = true;

                gdec_MontoPretensionAnt = Decimal.Parse(gstr_MontoPretensionAnt);

                if (gstr_TipoExpediente.Equals("Actor"))
                    gint_EstadoPretension = 1;
                else if (gstr_TipoExpediente.Equals("Demandado"))
                    gint_EstadoPretension = 0;
            }
            else
                gbool_TienePretencion = false;
        }

        private String GuardarPretensionInicial()
        {
            #region variables
            String[] larrstr_Resultado = new String[3];
            String[] revelac_resultado = new String[2];
            String str_resulRevelacionNota = String.Empty;
            String lstr_resultado = String.Empty;

            String str_NumMinisterio = string.Empty;
            String str_Ministerio = string.Empty;
            String str_PosibleFecEntRec = string.Empty;

            DateTime fechaRevelacion = new DateTime();
            DateTime fechaactual = DateTime.Now;

            Decimal dec_MontoPosibleReembolso;

            DataRow ldr_revelacion = null;
            #endregion

            try
            {
                #region Data Monedas
                String str_ObservacionesPI = this.CKEditorObservaciones.Text;

                if (this.DDLExpedientes.SelectedItem.Value == "0") 
                    gstr_IdExpediente = this.lblNumExpediente.Text; 

                NumberStyles style;
                gstr_Moneda = this.DDLMoneda.SelectedItem.Value;
                gdec_MontoPretension = txtMontoPretension.Text == "" ? 0 : Decimal.Parse(txtMontoPretension.Text);
                style=NumberStyles.None;
                gdec_MontoPretensionCol = this.txtPretensionColones.Text==""? 0 : Decimal.Parse(this.txtPretensionColones.Text, NumberStyles.AllowThousands | NumberStyles.Number);
                gstr_MontoPretensionCol = ((double)gdec_MontoPretensionCol).ToString();
                dec_MontoPosibleReembolso = 0;
                gdec_ValorPresente = this.txtValorPresente.Text == "" ? 0 : Decimal.Parse(this.txtValorPresente.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Number);//Formula: VP= VF/(1+i)n


                DateTime? ldt_PosibleFecEntRec;
                if (!String.IsNullOrEmpty(this.txtFchVigencia.Text))
                {
                    str_PosibleFecEntRec = Convert.ToDateTime(this.txtFchVigencia.Text).ToString();
                    ldt_PosibleFecEntRec = Convert.ToDateTime(str_PosibleFecEntRec);
                }
                else
                {
                    ldt_PosibleFecEntRec = null;
                }
                #endregion

                InicializarCampos();


                str_Ministerio = (clsSesion.Current.SociedadUsr == null) ? "Ministerio desconocido." : clsSesion.Current.SociedadUsr; 
                //str_Ministerio = ("11208" == null) ? "Ministerio desconocido." : "11208";
                String str_consultaTP = "SELECT IdCatalogo,oc.IdOpcion,oc.NomOpcion FROM ma.OpcionesCatalogos oc WHERE oc.IdCatalogo='30' AND Estado = 'A' AND oc.NomOpcion='" + gstr_TipoProceso + "'";

                DataTable dt_tipoProceso = GetData(str_consultaTP);
                DataRow dr_tipoProceso = (dt_tipoProceso.Rows.Count > 0) ? dt_tipoProceso.Rows[0] : dt_tipoProceso.NewRow();
                String str_tipoproceso = dr_tipoProceso["IdOpcion"].ToString();
                

                DataSet lds_Revelacion = ws_SGService.uwsConsultarRevelacionContSoc("", Convert.ToString(fechaactual.Year), Convert.ToString(fechaactual.Month), str_Ministerio, str_tipoproceso); //.uwsConsultarRevelacionContingente("", Convert.ToString(fechaactual.Year), Convert.ToString(fechaactual.Month));
                if (lds_Revelacion.Tables.Count > 0 && lds_Revelacion.Tables["Table"].Rows.Count > 0) 
                {
                    #region data Revelacion
                    ldr_revelacion = lds_Revelacion.Tables[0].Rows[0];
                    gstr_IdRevelacion = ldr_revelacion["IdRevCont"].ToString();
                    fechaRevelacion = Convert.ToDateTime(ldr_revelacion["FchModifica"].ToString());


                    String str_fechModificaRevelac = fechaRevelacion.ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture);
                    #endregion

                    #region Revelacion
                    if (!gbool_TienePretencion)
                    {
                        if (gstr_TipoExpediente.Equals("Actor"))
                            revelac_resultado = ws_SGService.uwsActualizarRevConTotalActivos(gstr_IdRevelacion, str_Ministerio, str_tipoproceso, gstr_MontoPretensionCol, "1", "0.00", fechaRevelacion, 1);
                        else if (gstr_TipoExpediente.Equals("Demandado"))
                            revelac_resultado = ws_SGService.uwsActualizarRevConTotalPasivos(gstr_IdRevelacion, str_Ministerio, str_tipoproceso, gstr_MontoPretensionCol, "1", "0.00", fechaRevelacion, 1);
                    }
                    else
                    {
                        if (gdec_MontoPretensionCol > gdec_MontoPretensionAnt)
                        {
                            gdec_MontoAjustado = gdec_MontoPretensionCol - gdec_MontoPretensionAnt;
                            gstr_MontoAjustado = ((double)gdec_MontoAjustado).ToString();

                            if (gstr_TipoExpediente.Equals("Actor"))
                                revelac_resultado = ws_SGService.uwsActualizarRevConTotalActivos(gstr_IdRevelacion, str_Ministerio, str_tipoproceso, gstr_MontoAjustado, "1", gstr_MontoPretensionCol, fechaRevelacion, 4);
                            else if (gstr_TipoExpediente.Equals("Demandado"))
                                revelac_resultado = ws_SGService.uwsActualizarRevConTotalPasivos(gstr_IdRevelacion, str_Ministerio, str_tipoproceso, gstr_MontoAjustado, "1", gstr_MontoPretensionCol, fechaRevelacion, 4);
                        }
                        else if (gdec_MontoPretensionAnt > gdec_MontoPretensionCol)
                        {
                            gdec_MontoAjustado = gdec_MontoPretensionAnt - gdec_MontoPretensionCol;
                            gstr_MontoAjustado = ((double)gdec_MontoAjustado).ToString();

                            if (gstr_TipoExpediente.Equals("Actor"))
                                revelac_resultado = ws_SGService.uwsActualizarRevConTotalActivos(gstr_IdRevelacion, str_Ministerio, str_tipoproceso, gstr_MontoAjustado, "1", gstr_MontoPretensionCol, fechaRevelacion, 5);
                            else if (gstr_TipoExpediente.Equals("Demandado"))
                                revelac_resultado = ws_SGService.uwsActualizarRevConTotalPasivos(gstr_IdRevelacion, str_Ministerio, str_tipoproceso, gstr_MontoAjustado, "1", gstr_MontoPretensionCol, fechaRevelacion, 5);
                        }
                        else
                        {
                            gstr_MontoPretensionCol = ((double)gdec_MontoPretensionCol).ToString();
                            revelac_resultado[0] = "00";
                        }
                    }
                    #endregion


                    if (String.IsNullOrEmpty(revelac_resultado[0]))
                    {
                        lstr_resultado = "fallo";
                        MessageBox.Show("Proceso de Actualizar Revelación no produjo resultado");
                    }
                    else if (revelac_resultado[0].Contains("00"))
                    {
                        larrstr_Resultado = ws_SGService.uwsRegistrarPretensionInicial(gstr_IdExpediente, gstr_Sociedad,
                            gstr_TipoProceso,
                            gstr_Moneda, gdec_TipoCambio, gdec_MontoPretension, gdec_MontoPretensionCol,
                            dec_MontoPosibleReembolso, gint_EstadoPretension, ldt_PosibleFecEntRec,
                            gdec_ValorPresente, str_ObservacionesPI, gstr_Usuario);

                        if (larrstr_Resultado[0].Equals("00") || larrstr_Resultado[0].Equals("Codigo error:00-00"))
                        {
                            this.LimpiarCampos();
                            lstr_resultado = "exito";
                            ws_SGService.uwsRegistrarAccionBitacoraCo("CT", gstr_Usuario, "Registro Pretención", "Exp: " + gstr_IdExpediente + ":" + gstr_Sociedad + "\n Respuesta: Satisfactorio.", gstr_IdExpediente, "", gstr_Sociedad);
                            MessageBox.Show("Se ingresó satisfactoriamente la pretensión inicial para el expediente número '" + gstr_IdExpediente + "'.\n");
                        }
                        else
                        {
                            lstr_resultado = "fallo";
                            ws_SGService.uwsRegistrarAccionBitacora("CT", gstr_Usuario, "Registro Pretencion", "Exp: " + larrstr_Resultado[0] + " y " + larrstr_Resultado[1]);
                            MessageBox.Show("Error en el proceso de inserción de la pretensión inicial del expediente, no se pudo registrar en el Expediente.\n");
                        }
                    }
                    else
                    {
                        lstr_resultado = "fallo";
                        MessageBox.Show("Error en el proceso de revelación " + revelac_resultado[0]+" "+ revelac_resultado[1]);
                    }
                }
                else
                {
                    lstr_resultado = "fallo";
                    MessageBox.Show("El proceso de creación de pretenciones no fue satisfactorio. No se encontró registro de Revelación");
                }
            }
            catch (Exception err)
            {
                lstr_resultado = "fallo";
                ws_SGService.uwsRegistrarAccionBitacora("CT", gstr_Usuario, "Crear Pretencion", err.Message);
                Seguridad.SaveError(err);
                MessageBox.Show("El proceso de creación de pretenciones no fue satisfactorio.\nComuniquese con Soporte Técnico de Informática para revisión del LOG de errores. \n");
            }

            return lstr_resultado;
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            //ModificarPretension();
        }

        //private String[] ModificarMontoExpedientes(Decimal pstr_MontoPretension, Decimal pstr_MontoPretensionColones)
        //{
        //    String[] larrstr_Resultado = new String[2];
        //    DataSet lds_Expediente = new DataSet();
        //    DataRow ldr_Expediente = null;

        //    try
        //    {
        //        lds_Expediente = ws_SGService.uwsConsultarExpedienteXNumero(gstr_IdExpediente, gstr_Sociedad);

        //        if ((lds_Expediente.Tables["Table"] != null) && (lds_Expediente.Tables.Count > 0))
        //        {
        //            ldr_Expediente = lds_Expediente.Tables["Table"].Rows[0];

        //            String lstr_IdExpediente = ldr_Expediente["IdExpediente"].ToString();
        //            String str_MonedaPretension = ldr_Expediente["MonedaPretension"].ToString();
        //            Decimal ldec_TipoCambio = Decimal.Parse(ldr_Expediente["TipoCambio"]);
        //            Decimal ldec_MontoPretension = pstr_MontoPretension; //Decimal.Parse(ldr_Expediente["MontoPretension"]);
        //            Decimal ldec_MontoPretColones = pstr_MontoPretensionColones; //Decimal.Parse(ldr_Expediente["MontoPretensionColones"]);
        //            Int32 lint_EstadoPretension = Convert.ToInt32(ldr_Expediente["EstadoPretension"]);
        //            DateTime ldt_PosibleFecEntRec = Convert.ToDateTime(ldr_Expediente["PosibleFechEntradaRecursos"]);
        //            Decimal ldec_ValorPresente = Decimal.Parse(ldr_Expediente["MontoValorPresente"]);
        //            String lstr_ObservacionesPretension = ldr_Expediente["ObservacionesPretension"].ToString();

        //            String lstr_Sociedad = gstr_Sociedad;
        //            Decimal ldec_MontoPosibleReembolso = Decimal.Parse("0.00");
        //            String lstr_UsrModifica = gstr_Usuario;

        //            larrstr_Resultado = ws_SGService.uwsRegistrarPretensionInicial(lstr_IdExpediente, lstr_Sociedad, str_MonedaPretension,
        //                ldec_TipoCambio, ldec_MontoPretension, ldec_MontoPretColones, ldec_MontoPosibleReembolso,
        //                lint_EstadoPretension, ldt_PosibleFecEntRec, ldec_ValorPresente,
        //                lstr_ObservacionesPretension, lstr_UsrModifica);

        //        }
        //    }
        //    catch (Exception exc)
        //    {
        //        larrstr_Resultado[0] = "99";
        //        larrstr_Resultado[1] = "Error al modificar montos.";
        //        ws_SGService.uwsRegistrarAccionBitacora("CT", gstr_Usuario, "ModificarMontosResolucion", "Exp:" + gstr_IdExpediente + " Error: " + exc.ToString());

        //    }
        //    return larrstr_Resultado;
        //}

        protected void btnUpload_Click1(object sender, EventArgs e)
        {
            Request.ContentType = "multipart/form-data";
            HttpFileCollection files = HttpContext.Current.Request.Files;
            string idexpediente = string.Empty;
            string[] result = new string[2];
            foreach (string fileTagName in files)
            {
                HttpPostedFile file = Request.Files[fileTagName];
                if (file.ContentLength > 0)
                {
                    // Due to the limit of the max for a int type, the largest file can be
                    // uploaded is 2147483647, which is very large anyway.
                    int lint_tamano = file.ContentLength;
                    string lstr_nombre = file.FileName;
                    int position = lstr_nombre.LastIndexOf("\\");
                    lstr_nombre = lstr_nombre.Substring(position + 1);
                    string tipoContenido = file.ContentType;
                    byte[] archivoDato = new byte[lint_tamano];
                    file.InputStream.Read(archivoDato, 0, lint_tamano);

                    if (this.lblNumExpediente.Text != "")
                    {
                        idexpediente = this.lblNumExpediente.Text;
                    }
                    else if (this.DDLExpedientes.SelectedValue != "0")
                    {
                        idexpediente = this.DDLExpedientes.SelectedValue;
                    }
                    //if (this.lblNumExpediente.Text == "")
                    //{

                    //    MessageBox.Show("Requerido adjuntar un expediente antes de subir el archivo a una pretención.");
                    //}
                    //else if(this.DDLExpedientes.SelectedValue!="0")
                    //{
                    //    MessageBox.Show("Requerido adjuntar un expediente antes de subir el archivo a una pretención.");

                    //result = ws_SGService.uwsGuardarArchivo(lstr_nombre, tipoContenido, lint_tamano, archivoDato, 0, "", 0, idexpediente, 0, "", 0, (clsSesion.Current.LoginUsuario == null) ? "usrDesconocido" : clsSesion.Current.LoginUsuario);
                    result = ws_SGService.uwsGuardarArchivoContingente(lstr_nombre, tipoContenido, lint_tamano, archivoDato, 0, "", gstr_Sociedad, ConsultarIdExpediente(idexpediente), idexpediente, 0, "", 0, (clsSesion.Current.LoginUsuario == null) ? "usrDesconocido" : clsSesion.Current.LoginUsuario);
                    if(result[0].Contains("00"))
                        MessageBox.Show("Se adjunto un nuevo archivo a la pretensión inicial del expediente <<" + idexpediente + ">>.");
                    else
                        MessageBox.Show("No se adjunto el archivo a la pretensión inicial del expediente <<" + idexpediente + ">>.");
                    
                   
                    //}
                }
            }

            string archivosDato = "Select * from rn.Archivos where IdExpediente ='" + idexpediente + "' AND IdSociedadGL='" + gstr_Sociedad + "'  AND IdFormulario=" + ConsultarIdExpediente(idexpediente);
            DataTable listaArchivos = GetData(archivosDato);
            if (listaArchivos.Rows.Count > 0)
            {

                gvFiles.DataSource = listaArchivos;
                gvFiles.DataBind();
            }
            else
            {
                //Log.Error("Consulta no arrojo resultados, la tabla viene vacia al realizar la consulta para uwsObtenerArchivoPorIdResolucion.");

            }
        }

        private void CargarArchivos()
        {
            string idExp = Request.QueryString["IdExpediente"];
            string idArch = Request.QueryString["Idarchivo"];
            clsArchivoSubir utilidad = new clsArchivoSubir();
            int int_idExp = ConsultarIdExpediente(idExp);
            string consulta = "Select * from rn.Archivos where IdExpediente ='" + idExp + "' AND IdSociedadGL='" + gstr_Sociedad + "'  AND IdFormulario=" + int_idExp;
            DataTable dt = GetData(consulta);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                string name = (string)row["NombreArchivo"];
                string contentType = (string)row["TipoContenido"];
                Byte[] data = (Byte[])row["Dato"];

                // Send the file to the browser
                Response.AddHeader("Content-type", contentType);
                Response.AddHeader("Content-Disposition", "attachment; filename=" + name);
                Response.BinaryWrite(data);//BinaryWrite
                Response.Flush();
                Response.End();
            }
            else
            {
                //Log.Error("Tabla no posee datos al realizcar la consulta."); 
            }
        }
        
        protected void gvFiles_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string[] lstr_ResEliminacion = new string[2];

                if (gvFiles.Rows.Count > 0)
                {

                    string str_idArchivo = e.Keys["IdArchivo"].ToString();
                    string str_idExp = string.Empty;// this.txtResolucionNum.Text;
                    int int_indice = Convert.ToInt32(e.RowIndex);
                    Label lblFchModificacion = (Label)gvFiles.Rows[int_indice].Cells[4].FindControl("lblFchModifica");
                    LinkButton lblIdArchivo = (LinkButton)gvFiles.Rows[int_indice].Cells[0].FindControl("lnkEliminar");
                    //string lblIdArchivo = lblIdArchivo.
                    DateTime lstr_FechaModificado = Convert.ToDateTime(lblFchModificacion.Text);// DateTime.Now.Date;
                    string lstr_fecha = String.Empty;
                    lstr_fecha = lstr_FechaModificado.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                    lstr_ResEliminacion = ws_SGService.uwsEliminarArchivo(str_idArchivo, lstr_fecha);
                    if (lstr_ResEliminacion[0] == "00")//Request.QueryString["Rev"] != null && 
                    {
                        //    string lst_IdRevelacion = Request.QueryString["Rev"];
                 
                            if (this.lblNumExpediente.Text != "")
                            {
                                str_idExp = this.lblNumExpediente.Text;
                            }
                            else if (this.DDLExpedientes.SelectedValue != "0")
                            {
                                str_idExp = this.DDLExpedientes.SelectedValue;
                            }
                          
                            string archivosDato = "Select * from rn.Archivos where IdExpediente ='" + str_idExp + "' AND IdSociedadGL='" + gstr_Sociedad + "'  AND IdFormulario=" + ConsultarIdExpediente(str_idExp);
               
                            DataTable listaArchivos = GetData(archivosDato);
                            if (listaArchivos.Rows.Count > 0)
                            {

                                gvFiles.DataSource = listaArchivos;
                                gvFiles.DataBind();
                            }
                            else
                            {
                                gvFiles.DataSource = null;
                                gvFiles.DataBind();
                                //Log.Error("Consulta no arrojo resultados, la tabla viene vacia al realizar la consulta para uwsObtenerArchivoPorIdResolucion.");

                            }
                            MessageBox.Show("El archivo fue eliminado satisfactoriamente.");

                    }
                }
                else
                {

                }


        }

        private DataSet ValidarTablas(DataSet dts)
        {
            
            if(dts.Tables.Count==0){

                dts = new DataSet("");
                return dts;
            }

            return dts;
            
        }

        public string NombreMes(int mes)
        {
            DateTimeFormatInfo dtinfo = new CultureInfo("es-ES", false).DateTimeFormat;
            return dtinfo.GetMonthName(mes);
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

        protected void txtMontoPretension_TextChanged(object sender, EventArgs e)
        {
            //decimal montopretension = this.txtMontoPretension.Text == "" ? 0 : Decimal.Parse(this.txtMontoPretension.Text);
            //this.txtMontoPretension.Text = montopretension.ToString("N2");
            DDLMoneda_SelectedIndexChanged(sender, e);
        }

        protected void cusTextTiempo_ServerValidate(object sender, ServerValidateEventArgs e)
        {
            if (this.txtTiempo.Text != "")
                e.IsValid = true;
            else
                this.txtTiempo.Text = "0";
                e.IsValid = false;
        }

        private void BloqueaCampo(bool pEstado)
        {
            this.CKEditorObservaciones.Enabled =
            this.btnGuardar.Visible =
            this.DDLMoneda.Enabled =
            this.btnGuardar.Enabled =
            this.txtMontoPretension.Enabled =
            this.txtFchVigencia.Enabled = pEstado; 
        }

        private void CargarExpedientes()
        {
            String str_consul = "SELECT IdExpediente,IdExpediente+'-'+TipoExpediente AS NomExpediente FROM co.Expedientes where co.Expedientes.EstadoExpediente='Activo' and IdSociedadGL='" + clsSesion.Current.SociedadUsr + "'"; 
            //String str_consul = "SELECT IdExpediente,IdExpediente+'-'+TipoExpediente AS NomExpediente FROM co.Expedientes where co.Expedientes.EstadoExpediente='Activo' and IdSociedadGL='11208' order by IdExpediente";
            DataTable exped = GetData(str_consul);
            this.DDLExpedientes.Items.Clear();
            if (exped.Rows.Count > 0)
            {
                DataRow campo = exped.Rows[0];
                this.DDLExpedientes.DataSource = exped;
                this.DDLExpedientes.DataTextField = "NomExpediente";
                this.DDLExpedientes.DataValueField = "IdExpediente";
                this.DDLExpedientes.Items.Insert(0, new ListItem("--- Elegir Expediente---", "0"));
                this.DDLExpedientes.DataBind();
            }
        }

        private void CargarMonedas()
        {
            String str_consul = "SELECT IdMoneda,IdMoneda+' - '+NomMoneda as Nombre from [ma].[Monedas] where IdMoneda IN ('USD', 'CRC' ,'EUR')";
            DataTable exped = GetData(str_consul);
            this.DDLMoneda.Items.Clear();
            if (exped.Rows.Count > 0)
            {
                DataRow campo = exped.Rows[0];
                this.DDLMoneda.DataSource = exped;
                this.DDLMoneda.DataTextField = "Nombre";
                this.DDLMoneda.DataValueField = "IdMoneda";
                this.DDLMoneda.Items.Insert(0, new ListItem("--- Elegir Moneda---", "0"));
                this.DDLMoneda.DataBind();
            }
        }

        private string ConsultarTipoExpediente(string idexpediente)
        {
            string str_consul = "SELECT TipoExpediente FROM co.Expedientes WHERE IdExpediente='" + idexpediente + "' and IdSociedadGL='" + clsSesion.Current.SociedadUsr + "' and EstadoExpediente='Activo'"; 
            //string str_consul = "SELECT TipoExpediente FROM co.Expedientes WHERE IdExpediente='" + idexpediente + "' and IdSociedadGL='11208' and EstadoExpediente='Activo'";
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
            //string str_consul = "SELECT IdExp FROM co.Expedientes where IdExpediente='" + idexpediente + "' and IdSociedadGL='11208' AND EstadoExpediente = 'Activo'";
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
    
    }
}