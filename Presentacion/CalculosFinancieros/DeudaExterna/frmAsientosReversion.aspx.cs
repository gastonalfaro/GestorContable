using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Collections;
using System.Data;
using System.Web.UI.WebControls;
using LogicaNegocio.CalculosFinancieros.DeudaExterna;
using System.Web.UI.HtmlControls;
using Presentacion.Compartidas;

namespace Presentacion.CalculosFinancieros.DeudaExterna.Mantenimiento
{
    public partial class frmAsientosReversion : BASE
    {
        # region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private wsAsientos.ServicioContable asientos = new wsAsientos.ServicioContable();

        //private static DataTable gdat_Asiento = new DataTable();
        protected DataTable gdat_Asiento
        {
            get
            {
                if (ViewState["gdat_Asiento"] == null)
                    ViewState["gdat_Asiento"] = new DataTable();
                return (DataTable)ViewState["gdat_Asiento"];
            }
            set
            {
                ViewState["gdat_Asiento"] = value;
            }
        }
        //private static DataTable gdat_AsientoSigaf = new DataTable();
        protected DataTable gdat_AsientoSigaf
        {
            get
            {
                if (ViewState["gdat_AsientoSigaf"] == null)
                    ViewState["gdat_AsientoSigaf"] = new DataTable();
                return (DataTable)ViewState["gdat_AsientoSigaf"];
            }
            set
            {
                ViewState["gdat_AsientoSigaf"] = value;
            }
        }
        
        private string gstr_ModuloActual = String.Empty;
        private string gstr_Usuario = String.Empty;

        private string gstr_SociedadFI = "G206";
        private string gstr_EntidadCP = "PEJC";

        # endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                gstr_Usuario = clsSesion.Current.LoginUsuario;

                if (!IsPostBack)
                {
                    txtFecha.Text = DateTime.Today.ToShortDateString();
                    refrescarGV();
                    CargarDDLs();                    

                    if (!string.IsNullOrEmpty(gstr_Usuario))
                    {
                        if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmAsientosReversion"))
                        {
                            Response.Redirect("~/Principal.aspx", true);
                        }
                    }
                    else
                        Response.Redirect("~/Login.aspx", true);
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Login.aspx", true);
            }
        }

        protected void refrescarGV()
        {
            gvAsiento.DataSource = gdat_Asiento;
            gvAsiento.DataBind();
        }

        protected void btnAgregarRubro_Click(object sender, EventArgs e)
        {
            if (chkBloqueoEncabezado.Checked)
            {
                decimal ldec_Monto = 0;
                if (txtMonto.Text != "")
                {
                    ldec_Monto = Convert.ToDecimal(txtMonto.Text);
                }
                if (gdat_Asiento.Columns.Count == 0)
                {
                    gdat_Asiento.Columns.Add("Fecha");
                    gdat_Asiento.Columns.Add("Cuenta");
                    gdat_Asiento.Columns.Add("Nombre");
                    gdat_Asiento.Columns.Add("ClaveContable");
                    gdat_Asiento.Columns.Add("Debe");
                    gdat_Asiento.Columns.Add("Haber");
                    gdat_Asiento.Columns.Add("Moneda");

                    gdat_Asiento.Columns.Add("TextoInfo");
                    gdat_Asiento.Columns.Add("CentroCosto");
                    gdat_Asiento.Columns.Add("CentroBeneficio");
                    gdat_Asiento.Columns.Add("ElementoPEP");
                    gdat_Asiento.Columns.Add("PosPre");
                    gdat_Asiento.Columns.Add("CentroGestor");
                    gdat_Asiento.Columns.Add("Fondo");
                    gdat_Asiento.Columns.Add("DocPres");
                    gdat_Asiento.Columns.Add("PosDocPres");
                }
                DateTime ldt_FechaActual = DateTime.UtcNow.Date;
                switch (dbDebeHaber.SelectedValue)
                {
                    case "40": gdat_Asiento.Rows.Add(
                        ldt_FechaActual.ToString("dd/MM/yyyy"),
                        dbCuentas.SelectedValue,
                        dbCuentas.SelectedItem,
                        dbDebeHaber.SelectedValue,
                        ldec_Monto,
                        0,
                        dbMoneda.SelectedValue,
                        txtTextoInfo.Text,
                        ddlCentroCosto.SelectedValue,
                        ddlCentroBeneficio.SelectedValue,
                        ddlElementoPEP.SelectedValue,
                        ddlPosPre.SelectedValue,
                        ddlCentroGestor.SelectedValue,
                        ddlFondo.SelectedValue,
                        ddlDocPres.SelectedValue,
                        txtPosDocPres.Text
                        ); break;
                    case "50": gdat_Asiento.Rows.Add(
                        ldt_FechaActual.ToString("dd/MM/yyyy"),
                        dbCuentas.SelectedValue,
                        dbCuentas.SelectedItem,
                        dbDebeHaber.SelectedValue,
                        0,
                        ldec_Monto,
                        dbMoneda.SelectedValue,
                        txtTextoInfo.Text,
                        ddlCentroCosto.SelectedValue,
                        ddlCentroBeneficio.SelectedValue,
                        ddlElementoPEP.SelectedValue,
                        ddlPosPre.SelectedValue,
                        ddlCentroGestor.SelectedValue,
                        ddlFondo.SelectedValue,
                        ddlDocPres.SelectedValue,
                        txtPosDocPres.Text
                        ); break;
                }
                refrescarGV();
            }
            else
            {
                Response.Write("<script>alert('Debe de bloquear el encabezado antes de continuar.')</script>");
            }
        }

        public void CargaCuentas(String lstr_GrupoCuenta)
        {
            List<Object> Cuentas = new List<Object> { };
            foreach (DataRow dr_Cuenta in ws_SGService.uwsConsultarCuentasContables(string.Empty, "OPER", lstr_GrupoCuenta, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty).Tables[0].Rows)
            {
                Cuentas.Add(new { NomCuentaContable = dr_Cuenta["IdCuentaContable"].ToString().Trim() + "-" + dr_Cuenta["NomCuentaContable"].ToString(), IdCuentaContable = dr_Cuenta["IdCuentaContable"].ToString().Trim() });
            }

            this.dbCuentas.DataSource = Cuentas;
            this.dbCuentas.DataBind();
            this.dbCuentas.Items.Insert(0, new ListItem("-Seleccione Opción-", ""));
        }

        public void CargarDDLs()
        {
            
            try
            {
                List<Object> GrupoCuentas = new List<Object> { };
                foreach (DataRow dr_GrupoCuenta in ws_SGService.uwsConsultarGruposCuentas(string.Empty, string.Empty, string.Empty, string.Empty).Tables[0].Rows)
                    GrupoCuentas.Add(new { NomGrupoCuenta = dr_GrupoCuenta["IdGrupoCuenta"].ToString().Trim() + "-" + dr_GrupoCuenta["NomGrupoCuenta"].ToString(), IdGrupoCuenta = dr_GrupoCuenta["IdGrupoCuenta"].ToString().Trim() });

                List<Object> CentroCosto = new List<Object> { };
                foreach (DataRow dr_CentroCosto in ws_SGService.uwsConsultarCentrosCosto(string.Empty, Convert.ToDateTime("01/01/1900"), string.Empty, gstr_SociedadFI, DateTime.Today, string.Empty, string.Empty).Tables[0].Rows)
                    CentroCosto.Add(new { NomCentroCosto = dr_CentroCosto["IdCentroCosto"].ToString().Trim() + "-" + dr_CentroCosto["NomCentroCosto"].ToString(), IdCentroCosto = dr_CentroCosto["IdCentroCosto"].ToString().Trim() });
                 
                List<Object> CentroBeneficio = new List<Object> { };
                foreach (DataRow dr_CentroBeneficio in ws_SGService.uwsConsultarCentrosBeneficio(string.Empty, Convert.ToDateTime("01/01/1900"), string.Empty, gstr_SociedadFI, DateTime.Today, string.Empty, string.Empty).Tables[0].Rows)
                    CentroBeneficio.Add(new { NomCentroBeneficio = dr_CentroBeneficio["IdCentroBeneficio"].ToString().Trim() + "-" + dr_CentroBeneficio["NomCentroBeneficio"].ToString(), IdCentroBeneficio = dr_CentroBeneficio["IdCentroBeneficio"].ToString().Trim() });

                List<Object> ElementoPEP = new List<Object> { };
                foreach (DataRow dr_ElementoPEP in ws_SGService.uwsConsultarElementosPEP(string.Empty, string.Empty).Tables[0].Rows)
                    ElementoPEP.Add(new { NomElementoPEP = dr_ElementoPEP["IdElementoPEP"].ToString().Trim() + "-" + dr_ElementoPEP["NomElementoPEP"].ToString(), IdElementoPEP = dr_ElementoPEP["IdElementoPEP"].ToString().Trim() });

                List<Object> PosPre = new List<Object> { };
                foreach (DataRow dr_PosPre in ws_SGService.uwsConsultarPosicionesPresupuestarias(string.Empty, gstr_EntidadCP, Convert.ToDateTime(txtFecha.Text).Year.ToString(), string.Empty, string.Empty).Tables[0].Rows)
                    PosPre.Add(new { NomPosPre = dr_PosPre["IdPosPre"].ToString().Trim() + "-" + dr_PosPre["NomPosPre"].ToString(), IdPosPre = dr_PosPre["IdPosPre"].ToString().Trim() });

                List<Object> CentroGestor = new List<Object> { };
                foreach (DataRow dr_CentroGestor in ws_SGService.uwsConsultarCentrosGestores(string.Empty, Convert.ToDateTime("01/01/1900"), string.Empty, gstr_SociedadFI, DateTime.Today, string.Empty, string.Empty).Tables[0].Rows)
                    CentroGestor.Add(new { NomCentroGestor = dr_CentroGestor["IdCentroGestor"].ToString().Trim() + "-" + dr_CentroGestor["NomCentroGestor"].ToString(), IdCentroGestor = dr_CentroGestor["IdCentroGestor"].ToString().Trim() });

                List<Object> Fondo = new List<Object> { };
                foreach (DataRow dr_Fondo in ws_SGService.uwsConsultarFondos(string.Empty, gstr_EntidadCP, string.Empty, string.Empty).Tables[0].Rows)
                    Fondo.Add(new { NomFondo = dr_Fondo["IdFondo"].ToString().Trim() + "-" + dr_Fondo["NomFondo"].ToString(), IdFondo = dr_Fondo["IdFondo"].ToString().Trim() });

                List<Object> DocPres = new List<Object> { };
                foreach (DataRow dr_DocPres in ws_SGService.uwsConsultarReservas(string.Empty, string.Empty, gstr_SociedadFI, string.Empty, string.Empty, string.Empty).Tables[0].Rows)
                    DocPres.Add(new { NomReserva = dr_DocPres["IdReserva"].ToString().Trim() + "-" + dr_DocPres["NomReserva"].ToString(), IdReserva = dr_DocPres["IdReserva"].ToString().Trim() });

                this.dbCuentas.DataBind();
                this.dbCuentas.Items.Insert(0, new ListItem("-Seleccione Opción-", ""));

                this.dbGrupoCuentas.DataSource = GrupoCuentas;
                this.dbGrupoCuentas.DataBind();
                this.dbGrupoCuentas.Items.Insert(0, new ListItem("-Seleccione Opción-", ""));

                this.ddlCentroCosto.DataSource = CentroCosto;
                this.ddlCentroCosto.DataBind();
                this.ddlCentroCosto.Items.Insert(0, new ListItem("-Seleccione Opción-", ""));

                this.ddlCentroBeneficio.DataSource = CentroBeneficio;
                this.ddlCentroBeneficio.DataBind();
                this.ddlCentroBeneficio.Items.Insert(0, new ListItem("-Seleccione Opción-", ""));


                this.ddlElementoPEP.DataSource = ElementoPEP;
                this.ddlElementoPEP.DataBind();
                this.ddlElementoPEP.Items.Insert(0, new ListItem("-Seleccione Opción-", ""));

                this.ddlPosPre.DataSource = PosPre;
                this.ddlPosPre.DataBind();
                this.ddlPosPre.Items.Insert(0, new ListItem("-Seleccione Opción-", ""));

                this.ddlCentroGestor.DataSource = CentroGestor;
                this.ddlCentroGestor.DataBind();
                this.ddlCentroGestor.Items.Insert(0, new ListItem("-Seleccione Opción-", ""));

                this.ddlFondo.DataSource = Fondo;
                this.ddlFondo.DataBind();
                this.ddlFondo.Items.Insert(0, new ListItem("-Seleccione Opción-", ""));

                this.ddlDocPres.DataSource = DocPres;
                this.ddlDocPres.DataBind();
                this.ddlDocPres.Items.Insert(0, new ListItem("-Seleccione Opción-", ""));


                DataSet lds_Monedas = ws_SGService.uwsConsultarDinamico("SELECT [IdMoneda], [NomMoneda] FROM [ma].[Monedas] where [IdMoneda] in ('EUR','USD','CRC','JPY') ORDER BY [NomMoneda]");
                if (lds_Monedas.Tables.Count > 0)// && (DDLExpedientes.Items.Count == 0))
                {
                    DataTable ldt_Monedas = lds_Monedas.Tables["Table"];
                    dbMoneda.DataSource = ldt_Monedas;
                    dbMoneda.DataTextField = "NomMoneda";
                    dbMoneda.DataValueField = "IdMoneda";
                    dbMoneda.Items.Insert(0, new ListItem("-Seleccione Opción-", ""));
                    dbMoneda.DataBind();
                    //for (int i = 0; i < DDLMoneda.Items.Count; i++)
                    //{
                    //    DDLMoneda.Items[i].Value = DDLMoneda.Items[i].Value.Trim();
                    //}
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        protected void BorrarRubro(object sender, GridViewCommandEventArgs e)
        {
            int lint_Indice = Convert.ToInt32(e.CommandArgument.ToString());
            if (e.CommandName == "Borrar")
            {
                gdat_Asiento.Rows.RemoveAt(lint_Indice);
            }
            refrescarGV();
        }

        public void LimpiarGrid()
        {
            gdat_Asiento.Rows.Clear();
            refrescarGV();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarGrid();
        }

        protected void btnEnviarAsiento_Click(object sender, EventArgs e)
        {
            wsDeudaExterna.wsDeudaExterna wsDeudaExterna = new wsDeudaExterna.wsDeudaExterna();
            int lint_IdAsiento = Convert.ToInt32(wsDeudaExterna.ConsultarAsientoAjuste(gstr_Usuario).Tables[0].Rows[0]["IdAsiento"].ToString()) + 1;
            decimal ldec_MontoContable = 0;

            try
            {
                for (int i = 0; i < gdat_Asiento.Rows.Count; i++)
                {
                    if (gdat_Asiento.Rows[i]["ClaveContable"].ToString() == "40")
                    {
                        ldec_MontoContable = Convert.ToDecimal(gdat_Asiento.Rows[i]["Debe"].ToString());
                    }
                    else
                    {
                        ldec_MontoContable = Convert.ToDecimal(gdat_Asiento.Rows[i]["Haber"].ToString());
                    }
                    wsDeudaExterna.CrearAsientoAjuste(
                        Convert.ToString(lint_IdAsiento), //identificador del asiento
                        gstr_Usuario, //usuario que generó el asiento
                        gdat_Asiento.Rows[i]["Cuenta"].ToString(), //Código de la cuenta contable
                        gdat_Asiento.Rows[i]["Nombre"].ToString(), //Nombre de la cuenta contable
                        gdat_Asiento.Rows[i]["ClaveContable"].ToString(), //Clave contable
                        ldec_MontoContable, //Monto a contabilizar
                        Convert.ToDecimal(gdat_Asiento.Rows[i]["Debe"].ToString()), //Monto del debe
                        Convert.ToDecimal(gdat_Asiento.Rows[i]["Haber"].ToString()), //Monto del haber
                        gdat_Asiento.Rows[i]["Moneda"].ToString()); //Moneda de la transacción
                }
                string logAsiento = "<script>alert('Resultado de Contabilización: "+GenerarAsientoAjuste()+"')</script>";
                Response.Write(logAsiento);
                gdat_Asiento.Rows.Clear();
                refrescarGV();
            }
            catch(Exception ex)
            {
                ex.ToString();
            }
        }

        public string GenerarAsientoAjuste()
        {
            //Coleccion de asientos y tipos de asientos requeridos en SAP expuestos por la referencia del servicio
            wsAsientos.ZfiAsiento item_asiento = new wsAsientos.ZfiAsiento();
            wsAsientos.ZfiAsiento[] tabla_asientos = new wsAsientos.ZfiAsiento[gdat_Asiento.Rows.Count];

            //variables de proceso
            string[] item_resAsientosLog = new string[10];
            string logAsiento = string.Empty;
            string flagEstadoAsiento = string.Empty;

            DateTime ldt_FchContabilizacion = Convert.ToDateTime(txtFecha.Text);
            string lstr_Moneda = dbMoneda.SelectedValue;
            string lstr_Referencia = txtReferencia.Text;
         
            try
            {
                for (int i = 0; i < gdat_Asiento.Rows.Count; i++)
                {
                    string lstr_DebeHaber = string.Empty;

                    if (gdat_Asiento.Rows[i]["ClaveContable"].ToString() == "40")
                    {
                        lstr_DebeHaber = "Debe";
                    }
                    else
                    {
                        lstr_DebeHaber = "Haber";
                    }
                    
                    item_asiento = new wsAsientos.ZfiAsiento();

                    if (i == 0)
                    {
                        item_asiento.Blart = "ED";//Clase de documento
                        item_asiento.Bukrs = "G206";//Sociedad
                        item_asiento.Bldat = ldt_FchContabilizacion.ToString("dd.MM.yyyy");//Fecha de documento
                        item_asiento.Budat = ldt_FchContabilizacion.ToString("dd.MM.yyyy");//Fecha de contabilización
                        item_asiento.Xblnr = lstr_Referencia;//Referencia
                    }
                    item_asiento.Waers = lstr_Moneda;//Moneda 
                    item_asiento.Bschl = gdat_Asiento.Rows[i]["ClaveContable"].ToString();//Clave de contabilización
                    item_asiento.Hkont = gdat_Asiento.Rows[i]["Cuenta"].ToString();//Cuenta de mayor
                    item_asiento.Wrbtr = Convert.ToDecimal(gdat_Asiento.Rows[i][lstr_DebeHaber].ToString());//Importe
                    item_asiento.Sgtxt = gdat_Asiento.Rows[i]["TextoInfo"].ToString();//Texto Informativo (50 caracteres)
                    item_asiento.Kostl = gdat_Asiento.Rows[i]["CentroCosto"].ToString();//Centro de Costo
                    item_asiento.Prctr = gdat_Asiento.Rows[i]["CentroBeneficio"].ToString();//Centro de Beneficio
                    item_asiento.Projk = gdat_Asiento.Rows[i]["ElementoPEP"].ToString();//Elemento PEP
                    item_asiento.Fipex = gdat_Asiento.Rows[i]["PosPre"].ToString();//Posición Presupuestaria
                    item_asiento.Fistl = gdat_Asiento.Rows[i]["CentroGestor"].ToString();//Centro Gestor
                    item_asiento.Geber = gdat_Asiento.Rows[i]["Fondo"].ToString();//Fondo
                    item_asiento.Kblnr = gdat_Asiento.Rows[i]["DocPres"].ToString();//Documento Presupuestario
                    item_asiento.Kblpos = gdat_Asiento.Rows[i]["PosDocPres"].ToString();//Posición de documento presupuestario

                    tabla_asientos[i] = item_asiento;
                }

                
                

                //Cargar de Asientos 
                string[] concatenado = new string[8];
                //envio de asiento mediante servicio web hacia SIGAF
                //item_resAsientosLog = asientos.EnviarAsientos(tabla_asientos); *modifc cucurucho
                item_resAsientosLog = asientos.EnviarAsientos(tabla_asientos, "");
                for (int j = 0; j < item_resAsientosLog.Length; j++)
                {
                    int x = j + 1;
                    logAsiento += x + " - " + item_resAsientosLog[j]+" - ";
                }
                //Registrar en Bitacora de movimientos
                //bitacora.ufnRegistrarAccionBitacora("DE", "123", lstr_TipoCancelacion, "Resultado de Contabilización: " + logAsiento, lstr_IdOperacion, lstr_NroValor + "-" + lstr_Nemotecnico, "");
                ws_SGService.uwsRegistrarAccionBitacoraCo("DE", gstr_Usuario, "Asiento Manual", "Resultado de Contabilización: " + logAsiento,"","","G206");
                return logAsiento;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        protected void chkBloqueoEncabezado_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkBloqueoEncabezado.Checked)
            {
                if (txtFecha.Text == "")
                {
                    Response.Write("<script>alert('El encabezado requiere de una fecha de contabilización.')</script>");
                    chkBloqueoEncabezado.Checked = false;
                }
                else
                {
                    txtFecha.Enabled = false;
                    dbMoneda.Enabled = false;
                    txtReferencia.Enabled = false;
                }
            }
            else            
            {
                Response.Write("<script>alert('Ha desbloqueado el encabezado, se reiniciará el asiento.')</script>");
                txtFecha.Enabled = true;
                dbMoneda.Enabled = true;
                txtReferencia.Enabled = true;
                LimpiarGrid();
            }
        }

        protected void dbGrupoCuentas_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargaCuentas(this.dbGrupoCuentas.SelectedValue);
        }
    }
}