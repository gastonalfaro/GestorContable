using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using Presentacion.Compartidas;
using System.Collections;
using System.Text;
using System.Configuration;
//using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI.HtmlControls;
//Log4Net inicializa en WebApplication
//[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace Presentacion.Contingentes
{
    public partial class NuevoExpediente : BASE
    {
        #region Variables
        //Variable reference de servicio web 
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        //Logg4Net Botacoreo
        private static readonly ILog Log = LogManager.GetLogger("Contingentes");
        //sacar resultado de WS de los catalogos para llenado de combos
        private DataSet opcionescatalogoLista = new DataSet();
        private DataSet opcionescatalogoLista2 = new DataSet();
        private DataTable dt_opcionescatalogoLista = new DataTable();
        private DataSet opcionescatalogoCB = new DataSet();
        //tipo proceso
        private DataSet dsTP = new DataSet();
        //Entidad persona
        private DataSet dsTEP = new DataSet();
        //Estado procesal
        private DataSet dsPEstado = new DataSet();
        //Centro beneficio
        private DataSet dsCE = new DataSet();
        //Tipo Expediente 
        private DataSet dsMotivo = new DataSet();
        //sociedades financieras
        private DataSet dsSoc1 = new DataSet();
        //sociedades financieras
        private DataSet dsSoc2 = new DataSet();
       //bandera de indicador si es agredado o modificado un registro segun seleccion de usuario
        private bool IsAdded;
        private String gstr_Usuario;
        private ArrayList arraysociedad = new ArrayList();
        StringBuilder sb = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();
        clsSesion sessUser;
        private string Confirma = string.Empty;

        //private String gstr_Sociedad = String.Empty;
        protected String gstr_Sociedad
        {
            get
            {
                if (ViewState["gstr_Sociedad"] == null)
                    ViewState["gstr_Sociedad"] = String.Empty;
                return (String)ViewState["gstr_Sociedad"];
            }
            set
            {
                ViewState["gstr_Sociedad"] = value;
            }
        }

        //private String gstr_IdExpediente = String.Empty;
        protected String gstr_IdExpediente
        {
            get
            {
                if (ViewState["gstr_IdExpediente"] == null)
                    ViewState["gstr_IdExpediente"] = String.Empty;
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
                    ViewState["gstr_TipoExpediente"] = String.Empty;
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
                    ViewState["gstr_TipoProceso"] = String.Empty;
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
                    ViewState["gstr_IdRevelacion"] = String.Empty;
                return (String)ViewState["gstr_IdRevelacion"];
            }
            set
            {
                ViewState["gstr_IdRevelacion"] = value;
            }
        }

        //private String gstr_MontoPretension = String.Empty;
        protected String gstr_MontoPretension
        {
            get
            {
                if (ViewState["gstr_MontoPretension"] == null)
                    ViewState["gstr_MontoPretension"] = String.Empty;
                return (String)ViewState["gstr_MontoPretension"];
            }
            set
            {
                ViewState["gstr_MontoPretension"] = value;
            }
        }

        //private Decimal gdec_MontoPretension = 0;
        protected decimal gdec_MontoPretension
        {
            get
            {
                if (ViewState["gdec_MontoPretension"] == null)
                    ViewState["gdec_MontoPretension"] = 0;
                return Convert.ToDecimal(ViewState["gdec_MontoPretension"]);
            }
            set
            {
                ViewState["gdec_MontoPretension"] = value;
            }
        }
      #endregion
        
        protected void Page_Load(object sender, EventArgs e) 
        {
            System.Web.UI.ScriptManager.GetCurrent(this).RegisterPostBackControl(btnGuardarActualizar);    
            gstr_Usuario = clsSesion.Current.LoginUsuario;
            //btnGuardarActualizar.Attributes.Add("onclick", "getMessage()");//boton de confirmacion --- no sirve
            MensajeConfirmacion();//Si es activado el mensaje de confirmacion se verifica esto, al guardar expedientes duplicados.

            gstr_Usuario = clsSesion.Current.LoginUsuario;
            gstr_Sociedad = clsSesion.Current.SociedadUsr;

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
                        CargarDDLs();
                        Session["IdExp"] = Request.QueryString["id"];//Recibimos y almacenamos en la Session object para no perder valor entre postbacks
                        String idExpediente = Convert.ToString(Session["IdExp"]);
                        Session["IsAdded"] = Request.QueryString["isAdd"];//Recibimos y almacenamos en la Session object para no perder valor entre postbacks
                        bool Nuevo = Convert.ToBoolean(Session["IsAdded"]);
                        ViewState["Nuevo"] = Nuevo;
                        ViewState["IdExp"] = idExpediente;
                        //bool IsAdded = Convert.ToBoolean(Session["IsAdded"]);

                        //Bloquea el campo de expediente si se está modificando un expediente
                        if (!Nuevo)
                        {
                            this.ddlManejadoComo.Enabled =
                            this.txtExpedientes.Enabled = false;
                        }
                        //Tipo Entidad Persona
                        dsTEP = CargarOpcionesCatalogos("35", "", "", "");//entidad persona
                        this.ddlManejadoComo.DataSource = dsTEP;
                        this.ddlManejadoComo.DataTextField = "NomOpcion";
                        this.ddlManejadoComo.DataValueField = "NomOpcion";
                        this.ddlManejadoComo.DataBind();
                        //Tipo proceso combo
                        dsTP = CargarOpcionesCatalogos("30", "", "","");//tipo proceso
                        this.DDLTipoProceso.DataSource = dsTP;
                        this.DDLTipoProceso.DataTextField = "NomOpcion";
                        this.DDLTipoProceso.DataValueField = "NomOpcion";
                        this.DDLTipoProceso.DataBind();
              
                        //Estado procesal 
                        dsPEstado = CargarOpcionesCatalogos("34", "", "", "");//estado proceso
                        this.DDLEstadoProcesal.DataSource = dsPEstado;
                        this.DDLEstadoProcesal.DataTextField = "NomOpcion";
                        this.DDLEstadoProcesal.DataValueField = "ValOpcion";
                        this.DDLEstadoProcesal.DataBind();
                        //Motivo demanda
                        dsMotivo = CargarOpcionesCatalogos("32", "", "", "");//tipo expediente
                        this.ddlMotivoDemanda.DataSource = dsMotivo;
                        this.ddlMotivoDemanda.DataTextField = "NomOpcion";
                        this.ddlMotivoDemanda.DataValueField = "NomOpcion";
                        this.ddlMotivoDemanda.DataBind();


                        this.txtFechaDemanda.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        //bind GridView here

                        if (!Convert.ToBoolean(ViewState["Nuevo"]) && ViewState["IdExp"]!=null)
                        {
                  
                            CargarDatosExpedientesModificar(ViewState["IdExp"].ToString());
                        }
                        else {
                            // IsAdded = true;
                        }
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

        private void CargarDDLs()
        {
            DataSet lds_CeBe = ws_SGService.uwsConsultarDinamico("SELECT * FROM [CentrosBeneficio] order by 1");
            this.DDLCentroBeneficio.Items.Clear();
            if (lds_CeBe.Tables.Count > 0)
            {
                DataTable ldt_CeBe = lds_CeBe.Tables["Table"];
                DDLCentroBeneficio.DataSource = ldt_CeBe;
                DDLCentroBeneficio.DataTextField = "NomCentroBeneficio";
                DDLCentroBeneficio.DataValueField = "NomCentroBeneficio";
                DDLCentroBeneficio.DataBind();
                DDLCentroBeneficio.Items.Insert(0, new ListItem("-Todos-", ""));
            }


        }
        private void MensajeConfirmacion() 
        {
            string eventTarget = Request["__EVENTTARGET"] ?? string.Empty;
            string eventArgument = Request["__EVENTARGUMENT"] ?? string.Empty;
           
            switch (eventTarget)
            {
                case "MyConfirmationPostBackEventTarget":
                    if (Convert.ToBoolean(eventArgument))
                    {
                        GuardarExpediente();//Si usuario da clic en aceptar guardamos el expediente.
                    }
                    break;
            }
        }

        private void MensajeConfirmaProceso(string msg) {
            //Inicializa el modal
            var sb = new StringBuilder();
            sb.AppendFormat("var x='" + msg+"';");
            sb.AppendFormat("var msg = window.confirm('¿Desea ingresar el expediente? '+\n x);\n");
            sb.Append("if (msg)\n");
            sb.Append("__doPostBack('MyConfirmationPostBackEventTarget', msg);\n");
            ClientScript.RegisterStartupScript(GetType(), "MyScriptKey", sb.ToString(), true);
        }
        
        protected void BtnGuardarActualizar_Click(object sender, EventArgs e)
        {
            //string confirmValue = Request.Form["confirm_value"];
            if(ValidaExpediente(txtExpedientes.Text))
            {
                string str_consultaSocExp = "SELECT e.IdExpediente,e.FchCreacion,s.NomSociedad FROM co.Expedientes AS e INNER JOIN ma.SociedadesFinancieras AS s ON e.IdSociedadGL=s.IdSociedadGL where e.EstadoExpediente='Activo' and e.IdExpediente='" + this.txtExpedientes.Text.Trim().Trim() + "'";
                DataTable exped = GetData(str_consultaSocExp);
                if (exped.Rows.Count > 0)
                {
                    ArrayList sociedades = new ArrayList();
                    string val = string.Empty;

                    //MessageBox.Show("Ya existe un <<NÚMERO DE EXPEDIENTE>> registrado, para el(los) siguiente(s) Institución(es)/Ministerio(s)  " + val + " con número expediente " + campo["IdExpediente"] + "");
                    for (int i = 0; i < exped.Rows.Count; i++)
                    {
                        DataRow campo = exped.Rows[i];
                        DateTime fecha = Convert.ToDateTime(campo["FchCreacion"]);
                        val += campo["NomSociedad"].ToString() + " fecha creación " + String.Format("{0:dd/MM/yyyy}", fecha);
                    }
                    string menj = "Ya existe un expediente con número <" + this.txtExpedientes.Text.Trim() + "> registrado, para el(los) siguiente(s) Institución(es)/Ministerio(s) listadas a continuación: " + val + " ";//con número expediente " + this.txtExpedientes.Text.Trim() + "";

                    //MessageBox.Show(menj);
                    MensajeConfirmaProceso(menj);//Confirmar si desea guardar unchecked MensajeConfirm


                }
                else {

                    GuardarExpediente();
                }//
            }
            else
            {
                MessageBox.Show("El expediente " + txtExpedientes.Text + " no tiene el formato correcto o contiene menos de 17 caracteres.");
            }//fin
      

            
        }

        /// <summary>
        /// Valida que el expediente tenga 17 campos y que no ingresen caracteres especiales
        /// </summary>
        private bool ValidaExpediente(string pExpediente) 
        {
            if (pExpediente.Trim().Length == 17 && (!pExpediente.Contains(".") && !pExpediente.Contains(",")))
                return true;
            return false;
        }//fin

        protected void DDLTipoExpediente_SelectedIndexChanged1(object sender, EventArgs e)
        {
           // this.txtTipoExpediente.Text = this.DDLTipoExpediente.SelectedItem.Value;

        }

        protected void lkbuttonPretension_Click(object sender, EventArgs e)
        {
            Response.Redirect("Pretenciones.aspx?arg1=" + this.txtExpedientes.Text.Trim() + "&arg2=" + this.txtFechaDemanda.Text); //this.calFechaResolucion.SelectedDate.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        }

        protected void btnSociedadActor_Click(object sender, EventArgs e)
        {
            //this.txtNombre2.Text += "\n" + this.DDLSociedades.SelectedItem.Value;

        }

        protected void btnSociedadDemandante_Click(object sender, EventArgs e)
        {
            ///this.txtNombreDemandado.Text += "\n" + this.DDLSociedades2.SelectedItem.Value;
        }

        protected void btnSociedadActor_Click1(object sender, EventArgs e)
        {
            //sb2.AppendLine(this.DDLSociedades.SelectedItem.Value);
            //this.txtNombreActor.Text = sb2.ToString();
        }

        protected void btnAnular_Click(object sender, EventArgs e)
        {
            string[] lstr_resultado = new string[2];
            lstr_resultado = ws_SGService.uwsAnularExpediente(this.txtExpedientes.Text.Trim(), "Inactivo",clsSesion.Current.SociedadUsr);

            if (lstr_resultado[0].Contains("Codigo error:00"))
            {
                this.panelMensajes.Visible = true;
                this.lblMsgCorrecto.Visible = true;
                this.IMGInCorrectoMsg.Visible = false;
                this.lblMsgInCorrecto.Visible = false;
                this.lblMsgCorrecto.Text = "Anulación de Expediente Número '" + this.txtExpedientes.Text.Trim() + "' satisfactoriamente.";
            }
            else if (lstr_resultado[0].Contains("99") || lstr_resultado[0].Contains("Codigo error:-") || lstr_resultado[0].Contains("Codigo error:00-") || lstr_resultado[0].Contains("Codigo error:-00") || lstr_resultado[0].Contains("Codigo error:"))
            {
                this.panelMensajes.Visible = true;
                this.lblMsgCorrecto.Visible = false;
                this.IMGCorrectoMsg.Visible = false;
                this.lblMsgInCorrecto.Visible = true;
                Log.Error("Error :" + lstr_resultado[0] + " y " + lstr_resultado[1]);
                this.lblMsgInCorrecto.Text = "Error en el proceso de anulación del expediente, no se pudo anular el Expediente.";
            }
        }

        protected void ddlManejadoComo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlManejadoComo.SelectedItem.Value.Equals("Actor"))
            {

                this.lblCedula.Text = "Demandado";
                this.lblNombre1.Text = " de Actor"; // "Demandado(s)";
                this.lblNombre2.Text = " de Demandado(s)"; //"Actor";
                this.lblNombre2.Visible=true;
                this.txtNombre2.Text = clsSesion.Current.SociedadUsr + " " + clsSesion.Current.NombreSociedadGL;
            }
            else if (this.ddlManejadoComo.SelectedItem.Value.Equals("Demandado"))
            {
                this.lblCedula.Text = "Actor";
                this.lblNombre1.Text = " de Demandado"; // "Actor(es)";
                this.lblNombre2.Text = " de Actores";//"Demandado(s)";
                this.lblNombre2.Visible = true;
                this.txtNombre2.Text = clsSesion.Current.SociedadUsr + " " + clsSesion.Current.NombreSociedadGL;
            }
        }

        private DataSet CargarSociedades()
        {
            DataSet sociedadeslista = ws_SGService.uwsConsultarSociedadesFinancieras("","","","","","");
            return sociedadeslista;
        }
        
        private DataSet CargarOpcionesCatalogos(string idCatalago, string ValOpc,string idOpcion,string nombre)
        {
            opcionescatalogoLista2 = ws_SGService.uwsConsultarOpcionesCatalogo(idCatalago, ValOpc, idOpcion, nombre);

            var dv = opcionescatalogoLista2.Tables[0].DefaultView;
            dv.RowFilter = "Estado = 'A'";
            //var newDS = new DataSet();
            dt_opcionescatalogoLista = dv.ToTable();
            opcionescatalogoLista = new DataSet();
            opcionescatalogoLista.Tables.Add(dt_opcionescatalogoLista);

            return opcionescatalogoLista;

        }
        
        private void CargarDatosExpedientesModificar(string idExpediente)
        {
              
                DataSet lds_Expedientes = new DataSet();
                DataTable tabla = new DataTable();
                string fecha = DateTime.Now.ToString("dd/MM/yyyy");
                lds_Expedientes = ws_SGService.uwsConsultarExpedienteXNumero(idExpediente, clsSesion.Current.SociedadUsr);
                tabla = lds_Expedientes.Tables[0];
               
               //Llenamos grid de expedientes
               //RECORRER EL DATASET PARA LLENAR LOS CAMPOS
                foreach (DataRow campo in tabla.Rows)
                {
                    this.txtExpedientes.Text = campo["IdExpediente"].ToString();
                    this.txtOrigenExp.Text = campo["ExpedienteOrigen"].ToString();

                    this.txtFechaDemanda.Text = Convert.ToDateTime(campo["FechaDemanda"].ToString()).ToString("dd/MM/yyyy");
                   // this.calFechaDemanda.SelectedValue= (DateTime)campo["FechaDemanda"];
                    //this.txtFechaDemanda.Text = laFecha.ToString("dd/MM/yyyy");
                    
                    this.ddlMotivoDemanda.SelectedValue = campo["MotivoDemanda"].ToString().ToString();
                    
                    this.ddlTipoPersona.SelectedValue = campo["TipoPersonaDemandante"].ToString();
                   
                    this.DDLTipoProceso.SelectedValue= campo["TipoProcesoExpediente"].ToString();
                    
                    //this.DDLEstadoExpediente.SelectedValue = campo["EstadoExpediente"].ToString();
                   
                    this.DDLEstadoProcesal.SelectedValue = campo["EstadoProcesal"].ToString();
                   
                    this.ddlManejadoComo.SelectedValue = campo["TipoExpediente"].ToString();
                    
                    if (this.ddlManejadoComo.SelectedItem.Value.Equals("Actor"))
                    {
                        this.txtCedulaActor.Text = campo["CedDemandado"].ToString();
                        this.txtNombreActor.Text = campo["NomDemandado"].ToString();
                        this.txtNombre2.Text = campo["NomActor"].ToString();
                        this.lblCedula.Text = "Demandado";
                        this.lblNombre1.Text = "Demandado";
                        this.lblNombre2.Text = "Actor";
                    }
                    else if (this.ddlManejadoComo.SelectedItem.Value.Equals("Demandado"))
                    {
                        this.txtCedulaActor.Text = campo["CedActor"].ToString();
                        this.txtNombreActor.Text = campo["NomActor"].ToString();
                        this.txtNombre2.Text = campo["NomDemandado"].ToString();
                        this.lblCedula.Text = "Actor";
                        this.lblNombre1.Text = "Actor";
                        this.lblNombre2.Text = "Demandado(s)";
                    } 
                }
                          
                
        }

        //Guardar Expediente en BD
        protected void GuardarExpediente()
        {
            gstr_Usuario = clsSesion.Current.LoginUsuario;
            gstr_Sociedad = clsSesion.Current.SociedadUsr;

            string[] result_save = new string[3];
            //bool IsAdded = false;
            string idExpediente = this.txtExpedientes.Text.Trim(); //(TextBox) this.FindControl("txtExpedientes");
            string numExOrigen = this.txtOrigenExp.Text;
            string tipoProceso = this.DDLTipoProceso.SelectedItem.Value;
            string cedulaActor = string.Empty;
            string nombreActor = string.Empty;
            string cedulaDemandado = string.Empty;
            string nombreDemandado = string.Empty;

            if (this.ddlManejadoComo.SelectedItem.Value.Equals("Actor"))
            {
                cedulaDemandado = "";// this.txtCedulaActor.Text;
                nombreDemandado = this.txtNombreActor.Text;//Request.Form[txtNombreActor.UniqueID]; //this.txtNombreActor.Text;
                nombreActor = this.txtNombre2.Text;
                cedulaActor = clsSesion.Current.SociedadUsr;
            }
            else if (this.ddlManejadoComo.SelectedItem.Value.Equals("Demandado"))
            {
                cedulaActor = "";// this.txtCedulaActor.Text;
                nombreActor = this.txtNombreActor.Text;//this.txtConcatena.Text; //Request.Form[txtNombreActor.UniqueID];//this.txtNombreActor.Text;
                nombreDemandado = this.txtNombre2.Text;
                cedulaDemandado = clsSesion.Current.SociedadUsr;
            }

            string estadoProcesal = this.DDLEstadoProcesal.SelectedItem.Value;
            string motivoDemanda = this.ddlMotivoDemanda.SelectedItem.Value;
            string tipoPersona = this.ddlTipoPersona.SelectedItem.Value;
            string tipoExpediente = this.ddlManejadoComo.SelectedItem.Value;//this.DDLTipoPersona.SelectedItem.Value;// a favor o en contra
            //string fechaDemanda = this.calFechaDemanda.SelectedDate.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);//this.calFechaResolucion.SelectedValue //(this.txtFechaDemanda.Text == null) ? (DateTime.Now).ToString("yyyy/MM/dd") : this.txtFechaDemanda.Text;
            string fechaDemanda = (this.txtFechaDemanda.Text == null) ? (DateTime.Now).ToString("yyyy/MM/dd") : Convert.ToDateTime(this.txtFechaDemanda.Text).ToString("yyyy/MM/dd");

            //string estadoExpediente = this.DDLEstadoExpediente.SelectedItem.Value;//this.txtEstadoExpediente.Text;
            string usuarioCreacion = (clsSesion.Current.LoginUsuario == null) ? "usrDesconocido" : clsSesion.Current.LoginUsuario;
            string sociedadGL ="";
            if (clsSesion.Current.SociedadUsr == null && usuarioCreacion.Equals("usrDesconocido")) 
            {
                sociedadGL= "usrDesconocido";
            }
            else if(clsSesion.Current.SociedadUsr == null)
            {
                sociedadGL = gstr_Sociedad;
            }
            else
            {
               sociedadGL = clsSesion.Current.SociedadUsr;
            }
            
            
            bool strNuevo = (bool)ViewState["Nuevo"];

            if (Convert.ToDateTime(fechaDemanda) <= DateTime.Today)
            {

                if (strNuevo)//Nuevo
                {
                    //Insertamos en BD
                    result_save = ws_SGService.uwsRegistrarExpedientes(idExpediente, sociedadGL, numExOrigen,
                        tipoExpediente,
                        "Activo",
                        Convert.ToDateTime(fechaDemanda),
                        tipoProceso.ToString(),
                        motivoDemanda.ToString(),
                        estadoProcesal.ToString(),
                        usuarioCreacion.ToString(),
                        cedulaActor,
                        cedulaDemandado,
                        nombreActor,
                        nombreDemandado,
                        tipoPersona.ToString(),
                        "");

                    if (result_save[0].Contains("00"))
                    {
                        MessageBox.Show("La creación del expediente número '" + this.txtExpedientes.Text.Trim() + "' fue satisfactoriamente.");
                    }
                    else if (result_save[0].Contains("99") || result_save[0].Contains("Codigo error:-") || result_save[0].Contains("Codigo error:00-") || result_save[0].Contains("Codigo error:-00"))
                    {
                        MessageBox.Show("La modificación del expediente no fue satisfactoria.\n");
                    }
                }
                else if (!strNuevo)//Editar
                {
                    String[] revelac_resultado = new String[2];
                    String str_NumMinisterio = string.Empty;
                    String str_Ministerio = string.Empty;
                    String str_PosibleFecEntRec = string.Empty;
                    DateTime fechaRevelacion = new DateTime();
                    DateTime fechaactual = DateTime.Now;
                    DataSet lds_ConsultarExpediente = new DataSet();
                    DataRow ldr_revelacion = null;
                    DataRow ldr_ConsultarExpediente = null;

                    pnlPretensionModificar.Visible = true;

                    lds_ConsultarExpediente = ws_SGService.uwsConsultarExpedienteXNumero(idExpediente.ToString(), gstr_Sociedad);
                    if ((lds_ConsultarExpediente.Tables["Table"] != null) && (lds_ConsultarExpediente.Tables.Count > 0))
                    {
                        ldr_ConsultarExpediente = lds_ConsultarExpediente.Tables["Table"].Rows[0];
                        gstr_MontoPretension = ldr_ConsultarExpediente["MontoPretensionColones"].ToString();
                        gstr_TipoProceso = ldr_ConsultarExpediente["TipoProcesoExpediente"].ToString();

                        if (String.IsNullOrEmpty(gstr_MontoPretension))
                            gstr_MontoPretension = "0.00";
                        gdec_MontoPretension = Convert.ToDecimal(gstr_MontoPretension);
                    }

                    if (!gstr_TipoProceso.Equals(tipoProceso.ToString()))
                    {
                        #region Revelación
                        DataSet lds_Revelacion = ws_SGService.uwsConsultarRevelacionContingente("", Convert.ToString(fechaactual.Year), Convert.ToString(fechaactual.Month));
                        if (lds_Revelacion.Tables.Count > 0)
                        {
                            ldr_revelacion = lds_Revelacion.Tables[0].Rows[0];
                            gstr_IdRevelacion = ldr_revelacion["IdRevCont"].ToString();

                            fechaRevelacion = Convert.ToDateTime(ldr_revelacion["FchModifica"].ToString());
                            String str_fechModificaRevelac = fechaRevelacion.ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture);
                            str_Ministerio = (clsSesion.Current.SociedadUsr == null) ? "Ministerio desconocido." : clsSesion.Current.SociedadUsr;

                            String str_consultaTP = "SELECT IdCatalogo,oc.IdOpcion,oc.NomOpcion FROM ma.OpcionesCatalogos oc WHERE oc.IdCatalogo='30' AND Estado = 'A'  AND oc.NomOpcion='" + gstr_TipoProceso + "'";
                            DataTable dt_tipoProceso = GetData(str_consultaTP);
                            DataRow dr_tipoProceso = (dt_tipoProceso.Rows.Count > 0) ? dt_tipoProceso.Rows[0] : dt_tipoProceso.NewRow();
                            String str_tipoprocesoAnterior = dr_tipoProceso["IdOpcion"].ToString();

                            String str_consultaTP2 = "SELECT IdCatalogo,oc.IdOpcion,oc.NomOpcion FROM ma.OpcionesCatalogos oc WHERE oc.IdCatalogo='30' AND Estado = 'A'  AND oc.NomOpcion='" + tipoProceso.ToString() + "'";
                            DataTable dt_tipoProceso2 = GetData(str_consultaTP2);
                            DataRow dr_tipoProceso2 = (dt_tipoProceso2.Rows.Count > 0) ? dt_tipoProceso2.Rows[0] : dt_tipoProceso2.NewRow();
                            String str_tipoproceso = dr_tipoProceso2["IdOpcion"].ToString();

                            //String str_consultaMonto = "SELECT * FROM rn.RevelacionesContingentesSociedadesGL RC WHERE RC.IdSociedadGL='" + gstr_Sociedad + "' AND RN.IdRevCont='" + gstr_IdRevelacion + "' AND RN.TipoProceso=" + str_tipoprocesoAnterior + "'";
                            //DataTable dt_Monto = GetData(str_consultaMonto);
                            //DataRow dr_Monto = (dt_Monto.Rows.Count > 0) ? dt_Monto.Rows[0] : dt_Monto.NewRow();
                            //String str_montoActivos = dr_Monto["MontoTotalActivos"].ToString();
                            //String str_montoPasivos = dr_Monto["MontoTotalPasivos"].ToString();

                            if (tipoExpediente.ToString().Equals("Actor"))
                            {
                                revelac_resultado = ws_SGService.uwsActualizarRevConTotalActivos(gstr_IdRevelacion, str_Ministerio,
                                    str_tipoprocesoAnterior, gstr_MontoPretension, "1", "0.00", fechaRevelacion, 3);

                                revelac_resultado = ws_SGService.uwsActualizarRevConTotalActivos(gstr_IdRevelacion, str_Ministerio,
                                    str_tipoproceso, gstr_MontoPretension, "1", "0.00", fechaRevelacion, 1);

                            }
                            else if (tipoExpediente.ToString().Equals("Demandado"))
                            {
                                revelac_resultado = ws_SGService.uwsActualizarRevConTotalPasivos(gstr_IdRevelacion, str_Ministerio,
                                    str_tipoprocesoAnterior, gstr_MontoPretension, "1", "0.00", fechaRevelacion, 3);

                                revelac_resultado = ws_SGService.uwsActualizarRevConTotalPasivos(gstr_IdRevelacion, str_Ministerio,
                                    str_tipoproceso, gstr_MontoPretension, "1", "0.00", fechaRevelacion, 1);
                            }


                            if (revelac_resultado[0].Contains("00"))
                            {
                                result_save = ws_SGService.uwsModificarExpediente(
                                    idExpediente.ToString(),
                                    gstr_Sociedad,
                                    numExOrigen.ToString(),
                                    tipoExpediente.ToString(),
                                    "Activo",
                                    Convert.ToDateTime(fechaDemanda),
                                    tipoProceso.ToString(),
                                    motivoDemanda.ToString(),
                                    estadoProcesal.ToString(),
                                    usuarioCreacion.ToString(),
                                    cedulaActor.ToString(),
                                    cedulaDemandado,
                                    nombreActor.ToString(),
                                    nombreDemandado,
                                    tipoPersona.ToString(), "");

                                if (result_save[0].Contains("00"))
                                {
                                    MessageBox.Show("La modificación del expediente número '" + this.txtExpedientes.Text.Trim() + "' fue satisfactoriamente.");
                                }
                                else if (result_save[0].Contains("99") || result_save[0].Contains("Codigo error:-") || result_save[0].Contains("Codigo error:00-") || result_save[0].Contains("Codigo error:-00"))
                                {
                                    MessageBox.Show("La modificación del expediente no fue satisfactoria.\n");
                                }
                            }
                            else
                            {
                                MessageBox.Show("La modificación del expediente no fue satisfactoria. [Registro Revelación]\n");
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        result_save = ws_SGService.uwsModificarExpediente(
                                    idExpediente.ToString(),
                                    gstr_Sociedad,
                                    numExOrigen.ToString(),
                                    tipoExpediente.ToString(),
                                    "Activo",
                                    Convert.ToDateTime(fechaDemanda),
                                    tipoProceso.ToString(),
                                    motivoDemanda.ToString(),
                                    estadoProcesal.ToString(),
                                    usuarioCreacion.ToString(),
                                    cedulaActor.ToString(),
                                    cedulaDemandado,
                                    nombreActor.ToString(),
                                    nombreDemandado,
                                    tipoPersona.ToString(), "");

                        if (result_save[0].Contains("00"))
                        {
                            MessageBox.Show("La modificación del expediente número '" + this.txtExpedientes.Text.Trim() + "' fue satisfactoriamente.");
                        }
                        else if (result_save[0].Contains("99") || result_save[0].Contains("Codigo error:-") || result_save[0].Contains("Codigo error:00-") || result_save[0].Contains("Codigo error:-00"))
                        {
                            MessageBox.Show("La modificación del expediente no fue satisfactoria.\n");
                        }

                    }
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar una fecha menor.");
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

        protected void btnSociedadActor_Click2(object sender, EventArgs e)
        {
            if (this.ddlManejadoComo.SelectedItem.Value.Equals("Actor"))
            {

               this.txtNombreActor.Text += "\n" + this.txtConcatena.Text;
            }
            else if (this.ddlManejadoComo.SelectedItem.Value.Equals("Demandado"))
            {

               txtNombreActor.Text += "\n" + this.txtConcatena.Text;
            }
            
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            this.txtNombreActor.Text = "";
        }

    }
}