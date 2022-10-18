﻿using Presentacion.Compartidas;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using log4net;
using LogicaNegocio.Seguridad;
using LogicaNegocio.CapturaIngresos;

namespace Presentacion.CapturaIngresos
{
	public partial class frmRealizarPago : BASE
	{
		# region Variables

		private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
	    //private static string lstr_separador_decimal = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
		//private string gstr_Usuario = "0114180568";        
		private int gint_Debug = 0;
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
        //private DataTable gdat_PagosTemp = new DataTable();
        protected System.Data.DataTable gdat_PagosTemp
        {
            get
            {
                if (ViewState["gdat_PagosTemp"] == null)
                    ViewState["gdat_PagosTemp"] = new System.Data.DataTable();
                return (System.Data.DataTable)ViewState["gdat_PagosTemp"];
            }
            set
            {
                ViewState["gdat_PagosTemp"] = value;
            }
        }
		//private DataTable gdat_TiposCambio = new DataTable();
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
		//Logg4Net Botacoreo
		private static readonly ILog Log = LogManager.GetLogger("CapturaIngresos");

		int grid = 0;
		# endregion

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
				gstr_Usuario = "";
				gbol_ConFirmaDigital = false;
				gstr_CorreoUsuario = "";
				gstr_NombreUsuario = "";
				gstr_TipoIdUsuario = "";
			}

            if (!IsPostBack)
            {

			ActualizarFormulariosImpresos(gstr_Usuario, ddlTipoPersona.SelectedValue);

			this.OcultarMensaje();
			gdt_FechaActual = DateTime.Today;
			int lint_Anno = gdt_FechaActual.Year;
			string lstr_Fecha = "";
			lstr_Fecha = gdt_FechaActual.Date.ToString();

			ActualizarTipoCambio(lstr_Fecha);

				if (!string.IsNullOrEmpty(gstr_Usuario))
				{
					if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "OBJ_CI"))
						Response.Redirect("~/Principal.aspx", true);
					else
					{
						LimpiarFormulario();
						grdDatos.DataBind();
						MostrarElementos(gstr_Usuario);
						lint_Anno = 0;
						lint_Anno = DateTime.Today.Year;
						txtAnno.Text = lint_Anno.ToString();
						txtIdPersona.Text = gstr_Usuario;
						ddlTipoPersona.SelectedValue = gstr_TipoIdUsuario;
						//txtCorreo.Text = gstr_CorreoUsuario;
						lblNombre.Text = gstr_NombreUsuario;
						gdt_FechaActual = DateTime.Today;
						lstr_Fecha = "";
						//calFecha.Text =  Convert.ToString (gdt_FechaActual);
						this.txtFecha.Text = gdt_FechaActual.ToString("dd/MM/yyyy");
						lstr_Fecha = gdt_FechaActual.Date.ToString();
						try
						{
                            //ddlListaFormularios.Items.Clear();
							ActualizarFormulariosImpresos(gstr_Usuario, gstr_TipoIdUsuario);
                            //ddlListaFormularios.DataBind();
							//ddlListaFormularios.Items.Clear();
                            //ddlListaFormularios.Items.Insert(0, (new ListItem("-- Seleccione--", "0")));
							//LimpiarFormulario();
							ActualizarTipoCambio(lstr_Fecha);
							ImprimeTipoCambio();
							ResetearPagosTemporales();
						}
						catch (Exception ex)
						{
							MessageBox.Show(ex.ToString());
							MostarMensaje(ex.ToString(), '1');
						}
						gdat_Formularios = wsCapturaIngresos.ConsultarFormulario(gstr_Usuario, gstr_TipoIdUsuario, "IMP").Tables[0];

						CargaCamposFormularioSeleccionado(sender, e);
					}

                    DataSet dsBancos = ws_SGService.uwsConsultarDinamico("SELECT DISTINCT b.NomBanco, b.IdBanco FROM ma.Bancos b ");
                    foreach (DataRow vFila in dsBancos.Tables[0].Rows) 
                    {
                        ddlBanco.Items.Add(new ListItem(vFila[0].ToString(), vFila[1].ToString()));
                    }
                    ddlBanco.Items.Insert(0, new ListItem("-- Seleccione --", "0"));
				}
				else
					Response.Redirect("~/Login.aspx", true);
			}

			//if (!IsPostBack)
			//{
			//    try
			//    {

			//        if (!string.IsNullOrEmpty(gstr_Usuario))
			//        {
			//            LimpiarFormulario();
			//            grdDatos.DataBind();
			//            MostrarElementos(gstr_Usuario);
			//            int lint_Anno = 0;
			//            lint_Anno = DateTime.Today.Year;
			//            txtAnno.Text = lint_Anno.ToString();
			//            txtIdPersona.Text = gstr_Usuario;
			//            ddlTipoPersona.SelectedValue = gstr_TipoIdUsuario;
			//            //txtCorreo.Text = gstr_CorreoUsuario;
			//            lblNombre.Text = gstr_NombreUsuario;
			//            gdt_FechaActual = DateTime.Today;
			//            string lstr_Fecha = "";
			//            //calFecha.Text =  Convert.ToString (gdt_FechaActual);
			//            calFecha.SelectedDate = gdt_FechaActual;
			//            lstr_Fecha = gdt_FechaActual.Date.ToString();
			//            try
			//            {
			//                ActualizarFormulariosImpresos(gstr_Usuario, gstr_TipoIdUsuario);
			//                ActualizarTipoCambio(lstr_Fecha);
			//                ImprimeTipoCambio();
			//                ResetearPagosTemporales();
			//            }
			//            catch (Exception ex)
			//            {
			//                MessageBox.Show(ex.ToString());
			//                MostarMensaje(ex.ToString(), '1');
			//            }
			//            gdat_Formularios = wsCapturaIngresos.ConsultarFormulario(gstr_Usuario, gstr_TipoIdUsuario, "Estado IN ('IMP')").Tables[0];

			//        }
			//        else
			//        {
			//            Response.Redirect("~/Login.aspx", true);
			//        }
			//    }

			//    catch (Exception ex)
			//    {
			//        MessageBox.Show(ex.ToString()); 
			//        MostarMensaje(ex.ToString(), '1');
			//    }
			//}
		}

		protected void ImprimeTipoCambio()
		{
			lblEuro.Text = "$" + gdec_TipoCambioEur.ToString("N2");//("0,##");
			lblCompraDol.Text = "₡" + gdec_TipoCambioComp.ToString("N2");//("0,##");
			lblVentaDol.Text = "₡" + gdec_TipoCambioVent.ToString("N2");//("0,##");
			//txtPeriodo.Text = gdt_FechaActual.Year.ToString();
		}

		protected void ddlTipoPersona_SelectedIndexChanged(object sender, EventArgs e)
		{
			CambioLongitudTexto(ddlTipoPersona.SelectedItem.Value);
            //ddlListaFormularios.Items.Clear();
			ActualizarFormulariosImpresos(txtIdPersona.Text, ddlTipoPersona.SelectedValue);
            //ddlListaFormularios.DataBind();
			//ddlListaFormularios.Items.Clear();
            //ddlListaFormularios.Items.Insert(0, (new ListItem("-- Seleccione--", "0")));
			LimpiarFormulario();
			grdDatos.DataBind();
			this.CargaCamposFormularioSeleccionado(sender, e);
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

		private void MostrarElementos(string str_usuario)
		{

			DataSet ldt_PermisosUsuario = ws_SGService.uwsConsultarPermisosUsuarios(str_usuario, "");

			for (int i = 0; ldt_PermisosUsuario.Tables["Table"].Rows.Count > i; i++)
			{
				string lstr_IdObjeto = ldt_PermisosUsuario.Tables["Table"].Rows[i]["IdObjeto"].ToString();
				bool lbool_Actualizar = (bool)ldt_PermisosUsuario.Tables["Table"].Rows[i]["Actualizar"];
				bool lbool_Consultar = (bool)ldt_PermisosUsuario.Tables["Table"].Rows[i]["Consultar"];
				string lstr_IdliEncabezado = "li" + lstr_IdObjeto;

				if (lbool_Consultar)
				{
					try
					{
						HtmlGenericControl hgcMenuEncabezado = (HtmlGenericControl)FindControlRecursive(Master.Page, lstr_IdliEncabezado);

						if (hgcMenuEncabezado != null)
							hgcMenuEncabezado.Visible = true;
					}
					catch { }
				}
			}
		}


		protected void ActualizarTotalesFormularioSeleccionado()
		{
			//if (!(string.IsNullOrEmpty(ddlListaFormularios.SelectedValue) || ddlListaFormularios.SelectedValue == "0"))
			//{
			//    if (ddlListaFormularios.SelectedValue != "0")
			//    {
			//        for (int i = 0; i < gdat_Pagos.Rows.Count; i++)
			//        {
			//            if (gdat_Pagos.Rows[i]["IdMoneda"].ToString().Trim() == "CRC")
			//            {
			//                gdec_MontoColones += Convert.ToDecimal(gdat_Pagos.Rows[i]["Monto"].ToString());
			//            }
			//            else
			//            {
			//                gdec_MontoColones += (gdec_TipoCambioComp * Convert.ToDecimal(gdat_Pagos.Rows[i]["Monto"].ToString()));
			//            }
			//        }
			//    }
			//    else
			//    {
			//        for (int i = 0; i < gdat_PagosTemp.Rows.Count; i++)
			//        {
			//            if (gdat_PagosTemp.Rows[i]["IdMoneda"].ToString().Trim() == "CRC")
			//            {
			//                gdec_MontoColones += Convert.ToDecimal(gdat_PagosTemp.Rows[i]["Monto"].ToString());
			//            }
			//            else
			//            {
			//                gdec_MontoColones += (gdec_TipoCambioComp * Convert.ToDecimal(gdat_PagosTemp.Rows[i]["Monto"].ToString()));
			//            }                       
			//        }
			//    }
			//    if (gdec_TipoCambioComp != 0)
			//        gdec_MontoDolares = gdec_MontoColones / gdec_TipoCambioComp;
			//    lblTotalColones.Text = gdec_MontoColones.ToString("N2");
			//    lblTotalDolares.Text = gdec_MontoDolares.ToString("N2");
			//    gdec_MontoColones = 0;
			//    gdec_MontoDolares = 0;
			//}
			//lblLetrasDolares.Text = wsCapturaIngresos.uwsConvertirMontoStringLetras(txtMontoDolares.Text);
			//lblLetrasColones.Text = wsCapturaIngresos.uwsConvertirMontoStringLetras(txtTotalColones.Text);


		}

		protected void CargaCamposFormularioSeleccionado(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(ddlListaFormularios.SelectedValue) || ddlListaFormularios.SelectedValue == "0")
			{
				ResetearPagosTemporales();
				int lint_Anno = 0;
				lint_Anno = DateTime.Today.Year;
				//LimpiarFormulario();
				grdDatos.DataBind();
				//calFecha.Text = lint_Anno.ToString();
				//txtCorreo.Text = String.Empty;
				//txtReserva.Text = String.Empty;
				//txtExpediente.Text = String.Empty;
				//txtDescripcion.Text = String.Empty;
				//txtCuentaCliente.Text = String.Empty;
				//txtDireccion.Text = String.Empty;
				//lblEdoFormulario.Text = String.Empty;
				//lblNomEstadoFormulario.Text = String.Empty;
				//DeshabilitaCamposFormulario();
				//refrescarGVPagos();
			}
			else
			{
				//HabilitaCamposFormulario();
				CargarFormularioSeleccionado();
				CargarPagosFormularioSeleccionado();
				ActualizarTotalesFormularioSeleccionado();
			}
           
        
		}

		protected void CargarFormularioSeleccionado()
		{
			if (!(string.IsNullOrEmpty(ddlListaFormularios.SelectedValue) || ddlListaFormularios.SelectedValue == "0"))
			{
				string lstr_NroFormulario = ddlListaFormularios.SelectedValue;
				if (string.IsNullOrEmpty(lstr_NroFormulario) == false)
				{
					gint_IdFormulario = Convert.ToInt32(lstr_NroFormulario);
					DataRow[] ldr_Formulario = gdat_Formularios.Select("IdFormulario = '" + lstr_NroFormulario + "'");

					lblEdoFormulario.Text = ldr_Formulario[0]["Estado"].ToString();
					lblFchModifica.Text = ldr_Formulario[0]["FchModifica"].ToString();
                    txtAnno.Text = ldr_Formulario[0]["Anno"].ToString();
					ddlMonedaFormulario.SelectedValue = ldr_Formulario[0]["IdMoneda"].ToString().Trim();
                  //  gdt_FechaActual = DateTime.Today;
                    this.txtFecha.Text = txtFecha.Text.Trim().Equals("") ? gdt_FechaActual.ToString("dd/MM/yyyy") : txtFecha.Text;
                    ddlMoneda.SelectedValue = ldr_Formulario[0]["IdMoneda"].ToString().Trim();

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
					lblTotalDolares.Text = gdec_MontoDolares.ToString("N2");
					gdec_MontoColones = 0;
					gdec_MontoDolares = 0;

					if (ldr_Formulario[0]["Estado"].ToString().Trim() == "PAG")
					{
						lblNomEstadoFormulario.Text = "Pagado";

					}
					else if (ldr_Formulario[0]["Estado"].ToString().Trim() == "IMP")
					{
						lblNomEstadoFormulario.Text = "Impreso";

					}
					else if (ldr_Formulario[0]["Estado"].ToString().Trim() == "CNT")
					{
						lblNomEstadoFormulario.Text = "Contabilizado";

					}
				}
			}

		}

		protected void ActualizarFormulariosImpresos(string str_Usuario, string str_TipoId)
		{
         
                gdat_Formularios = wsCapturaIngresos.ConsultarFormulario(str_Usuario, str_TipoId, "IMP").Tables[0];

                ddlListaFormularios.Dispose();
                ddlListaFormularios.Items.Clear();

                foreach (DataRow vfila in gdat_Formularios.Rows) 
                {
                    ddlListaFormularios.Items.Add(new ListItem(vfila["IdFormulario"].ToString().Trim(), vfila["IdFormulario"].ToString().Trim()));
                }
                ddlListaFormularios.Items.Insert(0, (new ListItem("-- Seleccione--", "0")));
         
		}

		protected void ddlListaFormularios_SelectedIndexChanged(object sender, EventArgs e)
		{
            int vValor = Int32.Parse(ddlListaFormularios.SelectedValue.ToString());
			ActualizarFormulariosImpresos(txtIdPersona.Text, ddlTipoPersona.SelectedValue);
            ddlListaFormularios.SelectedValue = vValor.ToString();
			CargaCamposFormularioSeleccionado(sender, e);
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
					}
					else
					{
						lblNombre.Text = "";
					}
                    //ddlListaFormularios.Items.Clear();

					ActualizarFormulariosImpresos(txtIdPersona.Text, ddlTipoPersona.SelectedValue);
                    //ddlListaFormularios.DataBind();
					//ddlListaFormularios.Items.Clear();
                    //ddlListaFormularios.Items.Insert(0, (new ListItem("-- Seleccione--", "0")));
					LimpiarFormulario();
					grdDatos.DataBind();
					this.CargaCamposFormularioSeleccionado(sender, e);
				}
			}
			else
			{
                //ddlListaFormularios.Items.Clear();

				ActualizarFormulariosImpresos(txtIdPersona.Text, ddlTipoPersona.SelectedValue);
                //ddlListaFormularios.DataBind();
				//ddlListaFormularios.Items.Clear();
                //ddlListaFormularios.Items.Insert(0, (new ListItem("-- Seleccione--", "0")));
				LimpiarFormulario();
				grdDatos.DataBind();
				this.CargaCamposFormularioSeleccionado(sender, e);
			}
			//txtCorreo.Text = string.Empty;
			//if (txtIdPersona.Text == "114180568")
			//{
			//    lblNombre.Text = "Steven Vega Vidal";
			//    txtCorreo.Text = "stevega90@gmail.com";
			//}


		}

		protected Boolean ValidaFormulario(out string str_Mensaje)
		{
			Boolean lbln_Resultado = true;
			str_Mensaje = "";
			//string lstr_monto = "";

			//if (lstr_separador_decimal == ",")
			//    lstr_monto = txtMonto.Text.Replace(".", "");
			//else
			//    lstr_monto = txtMonto.Text.Replace(",", "");
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
			else if (string.IsNullOrEmpty(ddlListaFormularios.Text))
			{
				str_Mensaje = "Debe seleccionar un formulario a anular.";
				lbln_Resultado = false;
			}
			else if (string.IsNullOrEmpty(ddlTipoPersona.Text))
			{
				str_Mensaje = "Debe seleccionar el tipo de identificación.";
				lbln_Resultado = false;
			}
			else if (string.IsNullOrEmpty(txtComprbante.Text))
			{
				str_Mensaje = "Debe digitar el número de comprobante.";
				lbln_Resultado = false;
			}
			else if (ddlBanco.SelectedValue.Equals("0"))
			{
				str_Mensaje = "Debe indicar el Banco.";
				lbln_Resultado = false;
			}
			else if (string.IsNullOrEmpty(txtMonto.Text))
			{
				str_Mensaje = "Debe indicar el monto del pago.";
				lbln_Resultado = false;
			}
			else if (string.IsNullOrEmpty(ddlMoneda.Text))
			{
				str_Mensaje = "Debe seleccionar la moneda.";
				lbln_Resultado = false;
			}
			else if (ddlMonedaFormulario.SelectedValue != ddlMoneda.SelectedValue)
			{
				str_Mensaje = "El pago debe ingresarse en la misma moneda del formulario.";
				lbln_Resultado = false;
			}
			else if (!ValidaMonto(Convert.ToDecimal(txtMonto.Text), ddlMoneda.SelectedValue))
			{
				MessageBox.Show("El monto debe ser exacto para permitir el pago");
				MostarMensaje("El monto debe ser exacto para permitir el pago", '1');
				lbln_Resultado = false;
			}
			return lbln_Resultado;
		}

		protected Boolean CambiarEstadoFormulario()
		{
			Boolean lbln_Resultado = true;
			try
			{

				string lstr_mensaje = wsCapturaIngresos.CambiarEstadoFormulario(Convert.ToInt32(ddlListaFormularios.SelectedValue), Convert.ToInt32(txtAnno.Text), lblEdoFormulario.Text.Trim(), "PAG", "", gstr_Usuario);
				if (lstr_mensaje != "00")
				{
					lbln_Resultado = false;
				}
				else
				{

                    //ddlListaFormularios.Items.Clear();
					ActualizarFormulariosImpresos(txtIdPersona.Text, ddlTipoPersona.SelectedValue);
                    //ddlListaFormularios.DataBind();
					//ddlListaFormularios.Items.Clear();
                    //ddlListaFormularios.Items.Insert(0, (new ListItem("-- Seleccione--", "0")));
				}
				MostarMensaje(lstr_mensaje, '0');
				return lbln_Resultado;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
				MostarMensaje(ex.ToString(), '1');
				return false;
			}


		}


		protected void btnAdjuntar_Click(object sender, EventArgs e)
		{
            if (this.respuesta_user.Value.Equals("true"))
            {
                this.respuesta_user.Value = "";
                this.adjuntar_documento(true);
            }
            else
            {
                this.respuesta_user.Value = "";
                this.adjuntar_documento(false);
            }
		}//FUNCION

        protected void adjuntar_documento(bool mas_de_un_documento_adjunto)
        {
            Request.ContentType = "multipart/form-data";
            HttpFileCollection files = HttpContext.Current.Request.Files;
            btnRealizarPago.Visible = false;
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
                    if (this.txtAnno.Text == "" || this.ddlListaFormularios.Text == "")
                    {
                        MessageBox.Show("Requerido indicar el formulario y el Año antes de subir el comprobante de Pago.");
                    }
                    else
                    {
                        if (mas_de_un_documento_adjunto)
                        {
                            #region ADJUNTAR DOCUMENTO SIN VALIDACION DE DOCUMENTOS ADJUNTOS YA EXISTENTES PREVIAMENTE
                            ws_SGService.uwsGuardarArchivo(lstr_nombre, tipoContenido,
                                    lint_tamano, archivoDato, 0, "", Convert.ToInt32(this.ddlListaFormularios.Text), "", 0, "", Convert.ToInt16(this.txtAnno.Text), (clsSesion.Current.LoginUsuario == null) ? "usrDesconocido" : clsSesion.Current.LoginUsuario);
                            MessageBox.Show("Se adjuntó un comprobante de pago al formulario<<" + this.ddlListaFormularios.Text + ">>");
                            #endregion
                        }
                        else
                        {
                            #region VALIDAR QUE NO EXISTA UN COMPROBANTE DE PAGO O DOCUMENTO YA ADJUNTO
                            DataSet ExisteArchivo = wsCapturaIngresos.uwsObtenerArchivoCapturaIngresos(this.ddlListaFormularios.Text, this.txtAnno.Text, string.Empty, string.Empty);
                            if (ExisteArchivo.Tables.Count > 0)
                            {
                                MessageBox.Show("Ya existe un archivo para el formulario " + this.ddlListaFormularios.Text);
                            }
                            else
                            {
                                ws_SGService.uwsGuardarArchivo(lstr_nombre, tipoContenido,
                                    lint_tamano, archivoDato, 0, "", Convert.ToInt32(this.ddlListaFormularios.Text), "", 0, "", Convert.ToInt16(this.txtAnno.Text), (clsSesion.Current.LoginUsuario == null) ? "usrDesconocido" : clsSesion.Current.LoginUsuario);
                                MessageBox.Show("Se adjuntó un comprobante de pago al formulario<<" + this.ddlListaFormularios.Text + ">>");
                            }
                            #endregion
                        }
                        #region VALIDAR QUE GUARDO EL DOCUMENTO ADJUNTO
                        DataSet fileList = wsCapturaIngresos.uwsObtenerArchivoCapturaIngresos(this.ddlListaFormularios.Text, this.txtAnno.Text, string.Empty, string.Empty);
                        if (fileList.Tables.Count > 0)
                        {

                            //gvFiles.DataSource = fileList;
                            //gvFiles.DataBind();
                            MostarMensaje("Archivo enviado exitosamente", '0');
                            lblNombre.Text = "";
                            btnRealizarPago.Visible = true;
                            wsCapturaIngresos.uwsEnviarCorreoCI("Comprobante de Pago cargado al Sistema Gestor", "Se ha recibido el comprobante de pago del formulario " + this.ddlListaFormularios.Text + " del Año " + this.txtAnno.Text, lstr_nombre, archivoDato, Convert.ToInt32(this.ddlListaFormularios.Text), Convert.ToInt16(this.txtAnno.Text), clsSesion.Current.LoginUsuario);
                        }
                        else
                        {
                            Log.Error("Consulta no produjo resultados, la tabla viene vacia al realizar la consulta para uwsObtenerArchivoPorIdResolucion.");
                        }
                        #endregion
                    }

                }
            }
        }//FUNCION

		protected void refrescarGVPagos()
		{
			if (!(string.IsNullOrEmpty(ddlListaFormularios.SelectedValue) || ddlListaFormularios.SelectedValue == "0"))
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
		protected void ResetearPagosTemporales()
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
			gdat_PagosTemp.Columns.Add("IdMoneda");
			gdat_PagosTemp.Columns.Add("Moneda");
			gdat_PagosTemp.Columns.Add("Monto");
			gdat_PagosTemp.Columns.Add("Periodo");
			gdat_PagosTemp.Columns.Add("Estado");
			gdat_PagosTemp.Columns.Add("UsrCreacion");
		}

		protected void CargarPagosFormularioSeleccionado()
		{
			try
			{
				if (!(string.IsNullOrEmpty(ddlListaFormularios.SelectedValue) || ddlListaFormularios.SelectedValue == "0"))
				{
					DataTable ldat_Temporal = new DataTable();
					ldat_Temporal = wsCapturaIngresos.ConsultarPago(Convert.ToInt32(ddlListaFormularios.SelectedValue), Convert.ToInt16(txtAnno.Text)).Tables[0];
                    if (ldat_Temporal.Rows.Count > 0)
                    {
                        DataRow[] ldar_Temporal;
                        ldar_Temporal = ldat_Temporal.Select("Estado = 'A'");
                        gdat_Pagos = ldar_Temporal.CopyToDataTable();
                        grdDatos.DataSource = gdat_Pagos;
                        grdDatos.DataBind();
                    }
				}
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
		protected void ActualizarTipoCambio(string lstr_Fecha)
		{
			/*
			gdat_TiposCambio = wsSistemaGestor.uwsConsultarTiposCambio(null, gdt_FechaActual.Date, null, "N").Tables[0];
			gdec_TipoCambioEur = Convert.ToDecimal(gdat_TiposCambio.Select("IdMoneda = 'EUR'")[0]["Valor"].ToString());
			gdec_TipoCambioComp = Convert.ToDecimal(gdat_TiposCambio.Select("TipoTransaccion = '317'")[0]["Valor"].ToString());
			gdec_TipoCambioVent = Convert.ToDecimal(gdat_TiposCambio.Select("TipoTransaccion = '318'")[0]["Valor"].ToString());
			*/
			
			if ((!string.IsNullOrEmpty(clsSesion.Current.TpoCambioEUR)) && (!string.IsNullOrEmpty(clsSesion.Current.TpoCambioCompra)) && (!string.IsNullOrEmpty(clsSesion.Current.TpoCambioVenta)))
            {
                gdec_TipoCambioEur = Convert.ToDecimal(clsSesion.Current.TpoCambioEUR);
                gdec_TipoCambioComp = Convert.ToDecimal(clsSesion.Current.TpoCambioCompra);
                gdec_TipoCambioVent = Convert.ToDecimal(clsSesion.Current.TpoCambioVenta);
            }
            else
            {
                gdat_TiposCambio = wsSistemaGestor.uwsConsultarTiposCambio(null, gdt_FechaActual.Date, null, "N").Tables[0];
                gdec_TipoCambioEur = Convert.ToDecimal(gdat_TiposCambio.Select("IdMoneda = 'EUR'")[0]["Valor"].ToString());
                gdec_TipoCambioComp = Convert.ToDecimal(gdat_TiposCambio.Select("TipoTransaccion = '317'")[0]["Valor"].ToString());
                gdec_TipoCambioVent = Convert.ToDecimal(gdat_TiposCambio.Select("TipoTransaccion = '318'")[0]["Valor"].ToString());

                clsSesion.Current.TpoCambioEUR = gdat_TiposCambio.Select("IdMoneda = 'EUR'")[0]["Valor"].ToString();
                clsSesion.Current.TpoCambioCompra = gdat_TiposCambio.Select("TipoTransaccion = '317'")[0]["Valor"].ToString();
                clsSesion.Current.TpoCambioVenta = gdat_TiposCambio.Select("TipoTransaccion = '318'")[0]["Valor"].ToString();
            }			 
		}

		protected void ActualizarMontosFormulario()
		{

			lblTotalColones.Text = "";
			lblTotalDolares.Text = "";
			//lblLetrasDolares.Text = "";
			//lblLetrasColones.Text = "";
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

			if (gdec_TipoCambioComp != 0)
			{
				gdec_MontoDolares = gdec_MontoColones / gdec_TipoCambioComp;
			}
			//lblLetrasDolares.Text = wsCapturaIngresos.uwsConvertirMontoStringLetras(gdec_MontoDolares.ToString(CultureInfo.InvariantCulture));
			//lblLetrasColones.Text = wsCapturaIngresos.uwsConvertirMontoStringLetras(gdec_MontoColones.ToString(CultureInfo.InvariantCulture));
			lblTotalColones.Text = gdec_MontoColones.ToString("N2");
			lblTotalDolares.Text = gdec_MontoDolares.ToString("N2");
			gdec_MontoColones = 0;
			gdec_MontoDolares = 0;

		}

		protected Boolean ValidaMonto(decimal dec_Monto, string str_Moneda)
		{
			//Si no es el monto exacto no debe permitir registrar el pago
			Boolean lbln_Resultado = true;
			if (ddlMonedaFormulario.SelectedValue != str_Moneda)
			{
				lbln_Resultado = false;
			}
			else
				if (str_Moneda.Trim() == "CRC")
				{
					//string lstr_monto = "";
					//if (lstr_separador_decimal == ",")
					//    lstr_monto = lblTotalColones.Text.Replace(".", "");
					//else
					//    lstr_monto = lblTotalColones.Text.Replace(",", "");
					if (dec_Monto != Convert.ToDecimal(lblTotalColones.Text))
					{
						lbln_Resultado = false;
					}
				}
				else
				{
					//string lstr_monto = "";
					//if (lstr_separador_decimal == ",")
					//    lstr_monto = lblTotalDolares.Text.Replace(".", "");
					//else
					//    lstr_monto = lblTotalDolares.Text.Replace(",", "");
					decimal dec_TotalDolares = 0;
					decimal.TryParse(lblTotalDolares.Text, out dec_TotalDolares);

					if (dec_Monto != dec_TotalDolares)
					{
						lbln_Resultado = false;
					}
				}
			return lbln_Resultado;
		}

		protected void txtMonto_TextChanged(object sender, EventArgs e)
		{
			//string lstr_monto = "";
			//if (lstr_separador_decimal == ",")
			//    lstr_monto = txtMonto.Text.Replace(".", "");
			//else
			//    lstr_monto = txtMonto.Text.Replace(",", "");
			decimal numero = Convert.ToDecimal(txtMonto.Text);
			if (!ValidaMonto(numero, ddlMoneda.SelectedValue))
			{
				//txtMonto.Text = numero.ToString("N2");

				MostarMensaje("El monto debe ser exacto y en la misma moneda del formulario para permitir el pago", '1');
				MessageBox.Show("El monto debe ser exacto y en la misma moneda del formulario para permitir el pago");
				btnAdjuntar.Visible = false;
			}
			else
			{
				btnAdjuntar.Visible = true;
			}

		}

		protected void LimpiarFormulario()
		{
			//ReiniciarDataTableTemp();
			int lint_Anno = 0;
			lint_Anno = DateTime.Today.Year;
			txtAnno.Text = (txtAnno.Text.Equals(""))?lint_Anno.ToString():txtAnno.Text;
			//txtIdPersona.Text = String.Empty;
			//lblNombre.Text = String.Empty;
			lblTotalDolares.Text = String.Empty;
			lblTotalColones.Text = String.Empty;
			lblNomEstadoFormulario.Text = String.Empty;
			txtComprbante.Text = String.Empty;
			//calFecha.Text = String.Empty;
			txtFecha.Text = gdt_FechaActual.ToString("dd/MM/yyyy");
			ddlBanco.ClearSelection();
			//ddlBanco.Text = String.Empty;
			//ddlListaFormularios.Dispose();
			//ddlListaFormularios.Items.Clear();
			//ddlListaFormularios.DataBind();
			//ddlListaFormularios.Items.Clear();
			//ddlListaFormularios.Items.Insert(0, (new ListItem("-- Seleccione77--", "0")));
			txtMonto.Text = string.Empty;
			txtObservaciones.Text = String.Empty;
			grdDatos.Enabled = false;
			btnAdjuntar.Visible = false;
			btnRealizarPago.Visible = false;
			btnEnviarAsiento.Visible = false;
			refrescarGVPagos();
            grdDatos.DataSource = null;
            grdDatos.DataBind();

		}

		protected void btnRealizarPago_Click(object sender, EventArgs e)
		{
			string lstr_mensaje = "";
			 if (ValidaFormulario(out lstr_mensaje))
			{
                int vFormulario = Convert.ToInt32(ddlListaFormularios.SelectedValue.ToString());
				//string lstr_monto = "";
				//if (lstr_separador_decimal == ",")
				//    lstr_monto = txtMonto.Text.Replace(".", "");
				//else
				//    lstr_monto = txtMonto.Text.Replace(",", "");
				//lstr_mensaje = wsCapturaIngresos.CrearComprobantePago(Convert.ToInt32(ddlListaFormularios.SelectedValue), Convert.ToInt32(txtAnno.Text), txtComprbante.Text, Convert.ToDateTime(calFecha.Text),
                lstr_mensaje = wsCapturaIngresos.CrearComprobantePago(vFormulario, Convert.ToInt32(txtAnno.Text), txtComprbante.Text, Convert.ToDateTime(this.txtFecha.Text),
						ddlBanco.SelectedValue.ToString(), Convert.ToString(ddlMoneda.SelectedValue), Convert.ToDecimal(txtMonto.Text), txtObservaciones.Text, gstr_Usuario, Convert.ToDateTime(lblFchModifica.Text));
				MostarMensaje(lstr_mensaje, '0');
				//despues de crear el comprobante de pago se cambia el estado del formullario a pagado
				CambiarEstadoFormulario();
				txtIdPersona.Text = "";

                wsSistemaGestor.uwsRegistrarAccionBitacoraCo("CI", clsSesion.Current.IdSesion, "Guardar Formulario", vFormulario.ToString() + " " + lblEdoFormulario.Text.Trim(), vFormulario.ToString(), "", "");
                MessageBox.Show("Información enviada satisfactoriamente! \n"+ "Envío de asiento: " +  EnviarAsiento(vFormulario));
                //ddlListaFormularios.Items.Clear();
                //ddlListaFormularios.Items.Insert(0, (new ListItem("-- Seleccione--", "0")));
				LimpiarFormulario();
                ActualizarFormulariosImpresos(txtIdPersona.Text, ddlTipoPersona.SelectedValue);
				//grdDatos.DataBind();
				//btnEnviarAsiento.Visible = true;

                
			}
			else
			{
				MessageBox.Show(lstr_mensaje);
				MostarMensaje(lstr_mensaje, '1');
				btnEnviarAsiento.Visible = false;
			}

		}

		protected void txtAnno_TextChanged(object sender, EventArgs e)
		{
            //ddlListaFormularios.Items.Clear();
			ActualizarFormulariosImpresos(txtIdPersona.Text, ddlTipoPersona.SelectedValue);
            //ddlListaFormularios.DataBind();
			//ddlListaFormularios.Items.Clear();
            //ddlListaFormularios.Items.Insert(0, (new ListItem("-- Seleccione--", "0")));
			LimpiarFormulario();
			grdDatos.DataBind();
			this.CargaCamposFormularioSeleccionado(sender, e);
		}

		protected void ddlMoneda_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (ddlMonedaFormulario.SelectedValue != ddlMoneda.SelectedValue)
			{
				MostarMensaje("El pago debe ingresarse en la misma moneda del formulario.", '1');
				MessageBox.Show("El pago debe ingresarse en la misma moneda del formulario.");
				btnAdjuntar.Visible = false;
			}
			else
			{
				btnAdjuntar.Visible = true;

			}

		}

		protected void btnEnviarAsiento_Click(object sender, EventArgs e)
		{
            EnviarAsiento(0);
        }

        /// <summary>
        /// Hace el envío de asiento para el pago realizado
        /// </summary>
        private string EnviarAsiento(int pIdFormulario) 
        {
            string res = "";
            try
            {
                if (string.IsNullOrEmpty(txtAnno.Text) || pIdFormulario < 1)
                    res = wsCapturaIngresos.EnviarAsientosCI(null, "-1");
                else
                    res = wsCapturaIngresos.EnviarAsientosCI(txtAnno.Text, pIdFormulario.ToString());

                btnEnviarAsiento.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                btnEnviarAsiento.Visible = false;
            }
            return res;
        }//fin

        #region RAMSES FUNCIONES
        protected String mrt_input_smg(String msg)
        {
            MessageBox.Show(msg);
            return "";
        }//FUNCION

        protected void btn1_Click(object sender, EventArgs e)
        {
            this.mrt_input_smg("Mensaje De Testing !!!");
        }//FUNCION
        #endregion

    }
}