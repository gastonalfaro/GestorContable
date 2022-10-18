using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Presentacion.Compartidas;
using System.Data;
using Presentacion.wsSG;
using System.Globalization;

namespace Presentacion.CalculosFinancieros.DeudaInterna
{
    public partial class frmCostoTransaccion : BASE
    {
        # region Variables
        private static wsDeudaInterna.wsDeudaInterna wsDeudaInterna = new wsDeudaInterna.wsDeudaInterna();
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private static wsSG.wsSistemaGestor wsSistemaGestor = new wsSG.wsSistemaGestor();
        private string gstr_Usuario = String.Empty;
        //private static DateTime gdt_FechaActual = new DateTime();
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
        //private static DateTime gdt_FechaActualCol = new DateTime();
        protected DateTime gdt_FechaActualCol
        {
            get
            {
                if (ViewState["gdt_FechaActualCol"] == null)
                    ViewState["gdt_FechaActualCol"] = new DateTime();
                return Convert.ToDateTime(ViewState["gdt_FechaActualCol"]);
            }
            set
            {
                ViewState["gdt_FechaActualCol"] = value;
            }
        }
        //private static DataTable gdat_TiposCambio = new DataTable();
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
        //private static DataTable gdat_TiposCambioCol = new DataTable();
        protected DataTable gdat_TiposCambioCol
        {
            get
            {
                if (ViewState["gdat_TiposCambioCol"] == null)
                    ViewState["gdat_TiposCambioCol"] = new DataTable();
                return (DataTable)ViewState["gdat_TiposCambioCol"];
            }
            set
            {
                ViewState["gdat_TiposCambioCol"] = value;
            }
        }
        //private static decimal gdec_TipoCambioComp = 0;
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
        //private static decimal gdec_TipoCambioVent = 0;
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
        //private static decimal gdec_TipoCambioEur = 0;
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
        //private static decimal gdec_TipoCambioYen = 0;
        protected decimal gdec_TipoCambioYen
        {
            get
            {
                if (ViewState["gdec_TipoCambioYen"] == null)
                    ViewState["gdec_TipoCambioYen"] = 0;
                return Convert.ToDecimal(ViewState["gdec_TipoCambioYen"]);
            }
            set
            {
                ViewState["gdec_TipoCambioYen"] = value;
            }
        }
        //private static decimal gdet_TipoCambioUde = 0;
        protected decimal gdet_TipoCambioUde
        {
            get
            {
                if (ViewState["gdet_TipoCambioUde"] == null)
                    ViewState["gdet_TipoCambioUde"] = 0;
                return Convert.ToDecimal(ViewState["gdet_TipoCambioUde"]);
            }
            set
            {
                ViewState["gdet_TipoCambioUde"] = value;
            }
        }
        private string gstr_ModuloActual = String.Empty;
        private wsAsientos.ServicioContable asientos = new wsAsientos.ServicioContable();

        # endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                gstr_Usuario = clsSesion.Current.LoginUsuario;

                if (!IsPostBack)
                {
                    CargarDDLs();
                    this.txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    if (!string.IsNullOrEmpty(gstr_Usuario))
                    {
                        if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmCostoTransaccion"))
                            Response.Redirect("~/Principal.aspx", true);
                        else
                        {
                            consultaCostoTransaccion();
                            //ActualizarTipoCambio(DateTime.Today.AddDays(-1));
                            //ActualizarTipoCambio(calFecha.SelectedDate.AddDays(-1));
                            if (Convert.ToDateTime(this.txtFecha.Text).Date == DateTime.Today.Date)
                            {
                                if (Convert.ToDateTime(this.txtFecha.Text).DayOfWeek.ToString("d") == "1")
                                    ActualizarTipoCambio(Convert.ToDateTime(this.txtFecha.Text), Convert.ToDateTime(this.txtFecha.Text).AddDays(-3));
                                else
                                    ActualizarTipoCambio(Convert.ToDateTime(this.txtFecha.Text), Convert.ToDateTime(this.txtFecha.Text).AddDays(-1));

                            }
                            else ActualizarTipoCambio(Convert.ToDateTime(this.txtFecha.Text), Convert.ToDateTime(this.txtFecha.Text));

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



        private void CargarDDLs()
        {
            DataSet lNemotecnicos = ws_SGService.uwsConsultarNemotecnicos("", "", "", "", "");
            this.ddlNemotecnico.DataSource = lNemotecnicos;
            this.ddlNemotecnico.DataTextField = "IdNemotecnico";
            this.ddlNemotecnico.DataValueField = "IdNemotecnico";
            this.ddlNemotecnico.DataBind();

            DataSet lds_Monedas = ws_SGService.uwsConsultarDinamico("SELECT [NomMoneda], rtrim(ltrim([IdMoneda])) as IdMoneda FROM [ma].[Monedas] where [IdMoneda] in ('JPY', 'CRCN', 'USD', 'EUR','UDE') order by [NomMoneda]");
            if (lds_Monedas.Tables.Count > 0)
            {
                DataTable ldt_Monedas = lds_Monedas.Tables["Table"];
                ddlMoneda.DataSource = ldt_Monedas;
                ddlMoneda.DataTextField = "NomMoneda";
                ddlMoneda.DataValueField = "IdMoneda";
                ddlMoneda.DataBind();
                ddlMoneda.Items.Insert(0, new ListItem("-Todos-", ""));
            }
        }

        //protected void ActualizarTipoCambio(DateTime ldt_TipoCambioAct, DateTime ldt_TipoCambioPas)
        protected void ActualizarTipoCambio(DateTime ldt_TipoCambioAct, DateTime ldt_TipoCambioPas)
        {
            try
            {
                //this.OcultarMensaje();
                //gdat_TiposCambio = wsSistemaGestor.uwsConsultarTiposCambio(null, Convert.ToDateTime(lstr_Fecha), null).Tables[0];
                //gdt_FechaActualCol = DateTime.Today.AddDays(-1);
                //gdt_FechaActual = DateTime.Today;

                //gdat_TiposCambio = wsSistemaGestor.uwsConsultarTiposCambio(null, gdt_FechaActual.Date, null, null).Tables[0];
                //gdat_TiposCambioCol = wsSistemaGestor.uwsConsultarTiposCambio(null, gdt_FechaActualCol.Date, null, null).Tables[0];

                gdat_TiposCambio = wsSistemaGestor.uwsConsultarTiposCambio(null, ldt_TipoCambioAct.Date, null, "N").Tables[0];
                gdat_TiposCambioCol = wsSistemaGestor.uwsConsultarTiposCambio(null, ldt_TipoCambioPas.Date, null, "N").Tables[0];

                gdec_TipoCambioComp = Convert.ToDecimal(gdat_TiposCambioCol.Select("TipoTransaccion = '3280'")[0]["Valor"].ToString());
                gdec_TipoCambioVent = Convert.ToDecimal(gdat_TiposCambioCol.Select("TipoTransaccion = '3140'")[0]["Valor"].ToString());

                gdec_TipoCambioEur = gdec_TipoCambioVent * Convert.ToDecimal(gdat_TiposCambio.Select("IdMoneda = 'EUR'")[0]["Valor"].ToString());
                gdet_TipoCambioUde = Convert.ToDecimal(gdat_TiposCambio.Select("IdMoneda = 'UDE'")[0]["Valor"].ToString());

                try
                {
                    gdec_TipoCambioYen = gdec_TipoCambioVent / Convert.ToDecimal(gdat_TiposCambio.Select("IdMoneda = 'JPY'")[0]["Valor"].ToString());
                }
                catch (Exception ex)
                {
                    gdec_TipoCambioYen = 0;
                    ex.ToString();
                }
            }
            catch (Exception ex)
            {
                gdec_TipoCambioComp = 0;
                gdec_TipoCambioVent = 0;
                gdec_TipoCambioYen = 0;
                gdec_TipoCambioEur = 0;
                gdet_TipoCambioUde = 0;
                ex.ToString();
            }


            //DataTable dttc = wsSistemaGestor.uwsConsultarTiposCambio(null, ldt_TipoCambioAct.Date, null, "N").Tables[0];

            //foreach (DataRow r in dttc.Rows)
            //{
            //    if (r["TipoTransaccion"].ToString().Equals("318"))
            //    {
            //        dolGas.Text = r["Valor"].ToString();
            //        break;
            //    }
            //}

            //Tipo cambio operaciones Sector Público no bancario
            //dolGas.Text = dr[""];
            lblCompCol.Text = gdec_TipoCambioComp.ToString("0.000");
            lblVentCol.Text = gdec_TipoCambioVent.ToString("0.000");
            lblEuroCol.Text = (gdec_TipoCambioEur).ToString("0.000");
            lblYenCol.Text = (gdec_TipoCambioYen).ToString("0.000");
            lblUdesCol.Text = (gdet_TipoCambioUde).ToString("0.000");

            if (this.lblCompCol.Text.Equals("0.000") ||
                this.lblVentCol.Text.Equals("0.000"))
                MessageBox.Show("No hay tipo de cambio para el día de hoy.");
        }

        protected void consultaCostoTransaccion()
        {
            DataTable ldat_CostosTransaccion = new DataTable();
            string lstr_estadoCosto = "C";
            if (chkEstadoCostoTransaccion.Checked)
                lstr_estadoCosto = "A";

            ldat_CostosTransaccion = wsDeudaInterna.ConsultarCostoTransaccion(null, null, null, null, lstr_estadoCosto).Tables[0];
            grvCostoTransaccion.DataSource = ldat_CostosTransaccion;
            grvCostoTransaccion.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                string lint_NumValor = null;

                lint_NumValor = ddlNumValor.SelectedIndex == 1 ? txtNroValor.Text : ddlNumValor.SelectedValue;

                DataSet lConsulta = wsDeudaInterna.ConsultarCostoTransaccion("", lint_NumValor, ddlNemotecnico.SelectedValue, "", "");

                foreach (DataTable table in lConsulta.Tables)
                    foreach (DataRow dr in table.Rows)
                    {
                        if (lConsulta.Tables[0].Rows[0]["NroValor"].ToString() != "00")
                        {
                            if (lConsulta.Tables[0].Rows[0]["NroValor"].ToString() == lint_NumValor
                                && lConsulta.Tables[0].Rows[0]["Nemotecnico"].ToString() == this.ddlNemotecnico.SelectedValue)
                            {
                                MessageBox.Show("El número de valor ya existe para ese nemotécnico.");
                                break;
                            }
                            else
                            {
                                wsDeudaInterna.CrearCostoTransaccion(lint_NumValor, ddlNemotecnico.SelectedValue, DateTime.Today.ToString("dd/MM/yyyy"), ddlMoneda.SelectedValue, Convert.ToDecimal(txtMonto.Text), Convert.ToDecimal(txtColones.Text), ddlMoneda.SelectedValue.Equals("USD") ? Convert.ToDecimal(lblVentCol.Text) : Convert.ToDecimal(lblUdesCol.Text), txtDetalle.Text, ddlModSinpe.SelectedValue, gstr_Usuario);
                                string lstr_Detalle = lint_NumValor.Equals("00") ? "Se ha generado un costo de transacción no asociado a un título valor, el nemotécnico es " + ddlNemotecnico.SelectedValue : "Se ha generado un costo de transacción para el valor " + lint_NumValor + " y nemotécnico " + ddlNemotecnico.SelectedValue;

                                ws_SGService.uwsRegistrarAccionBitacoraCo("DI", gstr_Usuario, "Creación de Costo de Transacción", lstr_Detalle, "", "", "G206");
                                // PrepararContabilizacionCosto(Convert.ToDecimal(txtMonto.Text), ddlNemotecnico.SelectedValue.Trim(), ddlMoneda.SelectedValue, lint_NumValor, txtDetalle.Text, DateTime.Today.ToString("dd/MM/yyyy"));
                                lblMensaje.ForeColor = System.Drawing.Color.Green;
                                lblMensaje.Text = "Estado de la transacción: Transacción procesada con éxito";
                            }
                        }
                    }
                if (lConsulta.Tables.Count == 1)
                {
                    wsDeudaInterna.CrearCostoTransaccion(lint_NumValor, ddlNemotecnico.SelectedValue, DateTime.Today.ToString("dd/MM/yyyy"), ddlMoneda.SelectedValue, Convert.ToDecimal(txtMonto.Text), Convert.ToDecimal(txtColones.Text), ddlMoneda.SelectedValue.Equals("USD") ? Convert.ToDecimal(lblVentCol.Text) : Convert.ToDecimal(lblUdesCol.Text), txtDetalle.Text, ddlModSinpe.SelectedValue, gstr_Usuario);
                    ///  PrepararContabilizacionCosto(Convert.ToDecimal(txtMonto.Text), ddlNemotecnico.SelectedValue.Trim(), ddlMoneda.SelectedValue, lint_NumValor, txtDetalle.Text, DateTime.Today.ToString("dd/MM/yyyy"));
                    lblMensaje.ForeColor = System.Drawing.Color.Green;
                    lblMensaje.Text = "Estado de la transacción: Transacción procesada con éxito";
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                lblMensaje.ForeColor = System.Drawing.Color.Red;
                lblMensaje.Text = "Estado de la transacción: Valores erróneos, revise e intente nuevamente";
            }
            consultaCostoTransaccion();
        }

        protected void ddlTipoValor_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlNumValor.Items.Clear();
            ddlNumValor.DataBind();
            ddlNumValor.Items.Insert(0, (new ListItem("-- Seleccione Opción --", "")));
            ddlNumValor.Items.Insert(1, (new ListItem("Insertar Valor", "")));

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtMonto_TextChanged(object sender, EventArgs e)
        {
            AjustaTipoCambio();
            //string stch = ddlMoneda.SelectedValue;
            //try
            //{
            //    switch (stch)
            //    {
            //        case "CRCN": txtColones.Text = (Convert.ToDecimal(txtMonto.Text)).ToString("0.000"); break;
            //        case "USD": txtColones.Text = (Convert.ToDecimal(txtMonto.Text) * gdec_TipoCambioVent).ToString("0.000"); break;
            //        case "EUR": txtColones.Text = (Convert.ToDecimal(txtMonto.Text) * gdec_TipoCambioEur).ToString("0.000"); break;
            //        case "JPY": txtColones.Text = (Convert.ToDecimal(txtMonto.Text) * gdec_TipoCambioYen).ToString("0.000"); break;
            //        case "UDE": txtColones.Text = (Convert.ToDecimal(txtMonto.Text) * gdet_TipoCambioUde).ToString("0.000"); break;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ex.ToString();
            //}
        }

        public void AjustaTipoCambio()
        {
            string stch = ddlMoneda.SelectedValue;
            try
            {
                switch (stch)
                {
                    case "CRCN": txtColones.Text = (Convert.ToDecimal(txtMonto.Text)).ToString("0.000"); break;
                    case "USD": txtColones.Text = (Convert.ToDecimal(txtMonto.Text) * gdec_TipoCambioVent).ToString("0.000"); break;
                    case "EUR": txtColones.Text = (Convert.ToDecimal(txtMonto.Text) * gdec_TipoCambioEur).ToString("0.000"); break;
                    case "JPY": txtColones.Text = (Convert.ToDecimal(txtMonto.Text) * gdec_TipoCambioYen).ToString("0.000"); break;
                    case "UDE": txtColones.Text = (Convert.ToDecimal(txtMonto.Text) * gdet_TipoCambioUde).ToString("0.000"); break;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        protected void ddlMoneda_SelectedIndexChanged(object sender, EventArgs e)
        {
            AjustaTipoCambio();

            if (!this.ddlMoneda.SelectedValue.Equals(this.txtIdMonedaNemotecnico.Text.Trim()) && this.ddlMoneda.SelectedIndex != 0)
                MessageBox.Show("La moneda del nemotécnico es: " + this.txtIdMonedaNemotecnico.Text + " y no coincide con la moneda seleccionada.");


            //string stch = ddlMoneda.SelectedValue;
            //try
            //{
            //    switch (stch)
            //    {
            //        case "CRCN": txtColones.Text = (Convert.ToDecimal(txtMonto.Text)).ToString("0.000"); break;
            //        case "USD": txtColones.Text = (Convert.ToDecimal(txtMonto.Text) * gdec_TipoCambioVent).ToString("0.000"); break;
            //        case "EUR": txtColones.Text = (Convert.ToDecimal(txtMonto.Text) * gdec_TipoCambioEur).ToString("0.000"); break;
            //        case "JPY": txtColones.Text = (Convert.ToDecimal(txtMonto.Text) * gdec_TipoCambioYen).ToString("0.000"); break;
            //        case "UDE": txtColones.Text = (Convert.ToDecimal(txtMonto.Text) * gdet_TipoCambioUde).ToString("0.000"); break;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ex.ToString();
            //}

        }

        protected void grvCostoTransaccion_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
        {
            grvCostoTransaccion.PageIndex = e.NewPageIndex;
            consultaCostoTransaccion();
        }

        protected void ddlNumValor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlNumValor.SelectedIndex == 1)
                txtNroValor.Visible = true;
            else
                txtNroValor.Visible = false;
        }

        protected void calFecha_DateChanged(object sender, EventArgs e)
        {
            if (Convert.ToDateTime(this.txtFecha.Text).Date == DateTime.Today.Date)
            {
                if (Convert.ToDateTime(this.txtFecha.Text).DayOfWeek.ToString("d") == "1")
                    ActualizarTipoCambio(Convert.ToDateTime(this.txtFecha.Text), Convert.ToDateTime(this.txtFecha.Text).AddDays(-3));
                else
                    ActualizarTipoCambio(Convert.ToDateTime(this.txtFecha.Text), Convert.ToDateTime(this.txtFecha.Text).AddDays(-1));
            }
            else
            {
                ActualizarTipoCambio(Convert.ToDateTime(this.txtFecha.Text), Convert.ToDateTime(this.txtFecha.Text));
            }
            AjustaTipoCambio();
        }

        protected void ddlNemotecnico_SelectedIndexChanged1(object sender, EventArgs e)
        {
            this.ddlNumValor.Items.Clear();
            this.ddlNumValor.Items.Add(new ListItem("-- Seleccione Valor --", "00"));
            this.ddlNumValor.Items.Add(new ListItem("Insertar Valor", "01"));
            try
            {
                this.ddlNumValor.DataSource = wsDeudaInterna.ConsultarTitulosValores(string.Empty, this.ddlNemotecnico.SelectedValue.Trim(), String.Empty, String.Empty, "V", String.Empty, String.Empty, String.Empty, "01/01/1900", "01/01/5000").Tables[0];
                this.ddlNumValor.DataTextField = 
                this.ddlNumValor.DataValueField = "NroValor";
                this.ddlNumValor.DataBind();
            }
            catch
            {
                this.ddlNumValor.DataBind();
            }

            this.txtNroValor.Text = string.Empty;
            this.txtNroValor.Visible = false;

            DataSet lNemotecnico = ws_SGService.uwsConsultarNemotecnicos(this.ddlNemotecnico.SelectedValue, "", "", "", "");

            foreach (DataTable table in lNemotecnico.Tables)
                foreach (DataRow dr in table.Rows)
                    this.txtIdMonedaNemotecnico.Text = lNemotecnico.Tables[0].Rows[0]["IdMoneda"].ToString();

        }

        protected void grvCostoTransaccion_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        //protected void grvCostoTransaccion_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    this.lblMensaje.Visible =
        //    this.grvCostoTransaccion.Visible = 
        //    this.btnAnadirCosto.Visible = false;
        //    this.lblMensajeTran.Visible =
        //    this.btnActualizarCosto.Visible = true;


        //    string lstr_IdCostoTransaccion = grvCostoTransaccion.DataKeys[e.NewEditIndex].Value.ToString();

        //    DataSet vCostoTransaccion = wsDeudaInterna.ConsultarCostoTransaccion(lstr_IdCostoTransaccion, "", "","","");

        //    foreach (DataTable table in vCostoTransaccion.Tables)
        //        foreach (DataRow dr in table.Rows)
        //        {
        //            this.ddlNemotecnico.SelectedValue = vCostoTransaccion.Tables[0].Rows[0]["Nemotecnico"].ToString();
        //            this.txtFecha.Text = vCostoTransaccion.Tables[0].Rows[0]["Fecha"].ToString();
        //            this.ddlMoneda.SelectedValue = vCostoTransaccion.Tables[0].Rows[0]["IdMoneda"].ToString();
        //            this.txtMonto.Text = vCostoTransaccion.Tables[0].Rows[0]["Monto"].ToString();
        //            this.txtColones.Text = vCostoTransaccion.Tables[0].Rows[0]["MontoColones"].ToString();
        //            this.txtDetalle.Text = vCostoTransaccion.Tables[0].Rows[0]["Detalle"].ToString();
        //            this.ddlModSinpe.SelectedValue = vCostoTransaccion.Tables[0].Rows[0]["ModuloSINPE"].ToString();               
        //        }
        //}

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            this.lblMensaje.Visible =
            this.grvCostoTransaccion.Visible =
            this.btnAnadirCosto.Visible = true;
            this.lblMensajeTran.Visible =
            this.btnActualizarCosto.Visible = false;

            string[] lstr_resultado = wsDeudaInterna.ModificarCostoTransaccion(
                Convert.ToInt32(this.txtIdCostoTransaccion.Text),
                this.ddlNumValor.SelectedValue.Equals("01") ? this.txtNroValor.Text : this.ddlNumValor.SelectedValue,
                this.ddlNemotecnico.SelectedValue,
                Convert.ToDateTime(this.txtFecha.Text),
                this.ddlMoneda.SelectedValue,
                Convert.ToDecimal(this.txtMonto.Text),
                Convert.ToDecimal(this.txtColones.Text),
                Convert.ToDecimal(lblVentCol.Text),
                this.txtDetalle.Text,
                this.ddlModSinpe.SelectedValue,
                this.txtEstado.Text,
                gstr_Usuario,
                Convert.ToDateTime(this.txtFechaModifica.Text));


            this.lblMensajeTran.Text = lstr_resultado[1];
            this.lblMensajeTran.Visible = true;

            LimpiarCampos();
            consultaCostoTransaccion();

        }

        private void LimpiarCampos()
        {
            this.txtFecha.Text =
            this.txtMonto.Text =
            this.txtEstado.Text =
            this.txtColones.Text =
            this.txtNroValor.Text =
            this.txtFechaModifica.Text =
            this.txtIdCostoTransaccion.Text =
            this.txtDetalle.Text = string.Empty;

            this.ddlMoneda.SelectedIndex =
            this.ddlNumValor.SelectedIndex =
            this.ddlModSinpe.SelectedIndex =
            this.ddlNemotecnico.SelectedIndex = 0;

            this.txtNroValor.Visible = false;

        }

        protected void grvCostoTransaccion_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Elimina")
            {
                string[] lstr_ResEliminacion = new string[2];

                if (grvCostoTransaccion.Rows.Count > 0)
                {
                    LinkButton lb = (LinkButton)e.CommandSource;
                    GridViewRow vGridViewRow = (GridViewRow)lb.NamingContainer;

                    string lstr_IdCostoTransaccion = grvCostoTransaccion.DataKeys[vGridViewRow.RowIndex].Values[0].ToString();

                    lstr_ResEliminacion = wsDeudaInterna.EliminarCostoTransaccion(int.Parse(lstr_IdCostoTransaccion), gstr_Usuario);
                    if (lstr_ResEliminacion[0] == "00")
                    {
                        consultaCostoTransaccion();
                        Presentacion.Compartidas.MessageBox.Show("El registro fue eliminado satisfactoriamente.");

                    }
                    else MessageBox.Show(lstr_ResEliminacion[1]);
                }
            }
            if (e.CommandName == "Seleccionar")
            {
                LinkButton lb = (LinkButton)e.CommandSource;
                GridViewRow vGridViewRow = (GridViewRow)lb.NamingContainer;

                string lstr_IdCostoTransaccion = grvCostoTransaccion.DataKeys[vGridViewRow.RowIndex].Values[0].ToString();

                this.lblMensaje.Visible =
                this.grvCostoTransaccion.Visible =
                this.btnAnadirCosto.Visible = false;
                this.lblMensajeTran.Visible =
                this.btnActualizarCosto.Visible = true;

                DataSet vCostoTransaccion = wsDeudaInterna.ConsultarCostoTransaccion(lstr_IdCostoTransaccion, "", "", "", "");

                foreach (DataTable table in vCostoTransaccion.Tables)
                    foreach (DataRow dr in table.Rows)
                    {
                        this.ddlNemotecnico.SelectedValue = vCostoTransaccion.Tables[0].Rows[0]["Nemotecnico"].ToString();
                        //  
                        if (this.ddlNumValor.Items.FindByValue(vCostoTransaccion.Tables[0].Rows[0]["NroValor"].ToString()) != null)
                            this.ddlNumValor.SelectedValue = vCostoTransaccion.Tables[0].Rows[0]["NroValor"].ToString();
                        else
                        {
                            this.ddlNumValor.SelectedIndex = 1;
                            this.txtNroValor.Visible = true;
                            this.txtNroValor.Text = vCostoTransaccion.Tables[0].Rows[0]["NroValor"].ToString();
                        }

                        this.txtFecha.Text = vCostoTransaccion.Tables[0].Rows[0]["Fecha"].ToString();
                        this.ddlMoneda.SelectedValue = vCostoTransaccion.Tables[0].Rows[0]["IdMoneda"].ToString();
                        this.txtMonto.Text = vCostoTransaccion.Tables[0].Rows[0]["Monto"].ToString();
                        this.txtColones.Text = vCostoTransaccion.Tables[0].Rows[0]["MontoColones"].ToString();
                        this.txtDetalle.Text = vCostoTransaccion.Tables[0].Rows[0]["Detalle"].ToString();
                        this.ddlModSinpe.SelectedValue = vCostoTransaccion.Tables[0].Rows[0]["ModuloSINPE"].ToString();

                        this.txtEstado.Text = vCostoTransaccion.Tables[0].Rows[0]["Estado"].ToString();
                        this.txtFechaModifica.Text = vCostoTransaccion.Tables[0].Rows[0]["FchModifica"].ToString();
                        this.txtIdCostoTransaccion.Text = lstr_IdCostoTransaccion;

                    }

            }
        }

        protected void grvCostoTransaccion_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string p = e.Row.Cells[10].Text.Trim();
                if (p.Equals("C"))
                {
                    LinkButton lbtnEditar = (LinkButton)e.Row.FindControl("lbtEditarCosto");
                    lbtnEditar.Visible = false;

                    LinkButton lbtnEliminar = (LinkButton)e.Row.FindControl("lbtEliminar");
                    lbtnEliminar.Visible = false;

                }
            }
        }

        protected void btnContabilizar_Click(object sender, EventArgs e)
        {
            wsDeudaInterna.ContabilizarCostoTransaccion();
            consultaCostoTransaccion();
        }

        protected void chkEstadoCostoTransaccion_CheckedChanged(object sender, EventArgs e)
        {
            consultaCostoTransaccion();
        }
    }
}