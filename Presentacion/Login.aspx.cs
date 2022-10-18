﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Presentacion.Compartidas;
using System.Data;
using System.Globalization;
using System.Web.Services;
using System.Configuration;
using System.Xml;
using System.Web.Caching;
using System.Text;
using LogicaNegocio.Seguridad;

namespace Presentacion.Seguridad.GestionUsuarios
{
	public partial class Login : BASE
	{
		//Variable referencia a servicio web sistema gestor
		private static Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
		private tSeguridad gcls_Seguridad = new tSeguridad();
		private string gstr_Usuario = String.Empty;
		private string gstr_TipoIdUsuario = String.Empty;
		private string gstr_Clave = String.Empty;
		private string gstr_IPSesion = String.Empty;
		private string gstr_CorreoUsuario = String.Empty;

		protected void Page_Load(object sender, EventArgs e)
		{

			//finding the div associated with id
			System.Web.UI.HtmlControls.HtmlGenericControl divCerrarSesion = (System.Web.UI.HtmlControls.HtmlGenericControl)Master.FindControl("divCerrarSesion");

			//hiding the div
			divCerrarSesion.Style.Add("display", "none");

			if (!IsPostBack)
			{

				FormsAuthentication.SignOut();
				clsSesion.Current.BorrarDatosSesion();

				if (ViewState["Contrasena"] == null)
					ViewState["Contrasena"] = string.Empty;
			}

		}

		/// <summary>
		/// Metodo para inicio de sesion
		/// </summary>
		/// <param name="str_Usuario">Id de usuario</param>
		/// <param name="str_Clave">Contrasena de usuario</param>
		private void IniciarSesionUsuario(string str_Usuario, string str_Clave)
		{
			string[] lastr_Resultado = new string[8];
			string lstr_Codigo = "99";
			string lstr_SociedadGL = String.Empty;
			string lstr_TipoIdUsuario = String.Empty;
			string lstr_IdSesion = String.Empty;
			string lstr_CorreoUsuario = String.Empty;
			gstr_Clave = str_Clave;//txtContrasena.Text.Trim();
			gstr_Usuario = str_Usuario;//txtUsuario.Text.Trim();
			ViewState["TipoLogueo"] = "Clave";

			string lstr_IpUsuario = ObtenerIPUsuario();

			lastr_Resultado = ws_SGService.uwsValidarUsuario(str_Usuario, gcls_Seguridad.CifrarTextoAES(str_Usuario + str_Clave), lstr_IpUsuario);
			lstr_Codigo = lastr_Resultado[0];
			switch (lstr_Codigo)
			{
				case "00":
					{

						lstr_IdSesion = lastr_Resultado[3];
						lstr_CorreoUsuario = lastr_Resultado[2];
						lstr_TipoIdUsuario = lastr_Resultado[7];
						lstr_SociedadGL = lastr_Resultado[5];
						string lstr_NomSociedad = lastr_Resultado[6];
						clsSesion.Current.gbol_FirmaDigital = false;
						clsSesion.Current.NombreSociedadGL = lstr_NomSociedad;
						clsSesion.Current.NomUsuario = lastr_Resultado[1];
						clsSesion.Current.CorreoUsuario = lstr_CorreoUsuario;
						clsSesion.Current.IdSesion = lstr_IdSesion;
						clsSesion.Current.IPSesion = lstr_IpUsuario;
						clsSesion.Current.TipoIdUsuario = lstr_TipoIdUsuario;
						clsSesion.Current.SociedadUsr = lstr_SociedadGL;
						LoguearUsuario(str_Usuario);
						DataSet lds_RolesUsuario = ws_SGService.uwsConsultarRolesUsuario("", str_Usuario);
						EstablecerRoles(lds_RolesUsuario);
						// Validación de IE
						double ver = getInternetExplorerVersion();
						if (ver > 0.0)
							if (ver < 11.0)
								Response.Write("<script>alert('Actualice su versión de Internet Explorer ya que podría presentar problemas en la página.')</script>");

						break;
					}
				case "-1":
					{
						lblMsg.Text = "La contraseña y/o el usuario no es correcta";
						lblMsg.Visible = true;
						break;
					}
				case "-2":
					{
						ViewState["Contrasena"] = gstr_Clave;
						ConsultarUsuario(gstr_Usuario);
						pnlDatosLogin.Visible = false;
						pnlConfirmacion.Visible = true;
						pnlCambioClave.Visible = false;
						break;
					}
				case "-3":
					{
						lblMsg.Text = "La cuenta se encuentra bloqueada.";
						lblMsg.Visible = true;
						break;
					}
				case "-4":
					{
						lblMsg.Text = "La contraseña y/o el usuario no es correcta";
						lblMsg.Visible = true;
						break;
					}
				case "-5":
					{
						//
						// Validación de IE
						double ver = getInternetExplorerVersion();
						if (ver > 0.0)
							if (ver < 11.0)
								Response.Write("<script>alert('Actualice su versión de Internet Explorer ya que podría presentar problemas en la página.')</script>");

						ViewState["Contrasena"] = gstr_Clave;
                        MensajeConfirmacion();
						break;

					}
				case "-6":
					{
						ViewState["Contrasena"] = gstr_Clave;
						pnlDatosLogin.Visible = false;
						pnlConfirmacion.Visible = false;
						pnlCambioClave.Visible = true;
						break;
					}
				default:
					{
						Response.Redirect("~/Login.aspx", true);
						break;
					}
			}
		}

		/// <summary>
		/// Evento de boton de inicio de sesion
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnInicioSesion_Click(object sender, EventArgs e)
		{
			ViewState["IdUsuario"] = txtUsuario.Text.Trim();
			IniciarSesionUsuario(txtUsuario.Text.Trim(), txtContrasena.Text.Trim());
		}

		/// <summary>
		/// Establece los roles del usuario
		/// </summary>
		/// <param name="ds_Roles">Dataset con roles de usuario</param>
		private void EstablecerRoles(DataSet ds_Roles)
		{
			if (ds_Roles.Tables.Count > 0)
			{
				for (int i = 0; ds_Roles.Tables["Table"].Rows.Count > i; i++)
				{
					string lstr_IdObjeto = ds_Roles.Tables["Table"].Rows[i]["IdRol"].ToString();
					clsSesion.Current.RolesUsuario.Add(lstr_IdObjeto);
				}
			}
		}

		/// <summary>
		/// Evento de boton de firma digital
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnFirmaDigital_Click(object sender, EventArgs e)
		{
			clsSesion.Current.gbol_FirmaDigital = true;
			if (btnFirmaDigital.Text == "Firma Digital")
			{
				//  lblContrasena.Visible = false;
				txtContrasena.Visible = false;
				txtContrasena.Text = "";
				pnlAppletFirma.Visible = true;

				btnFirmaDigital.Text = "No usar firma";
			}
			else
			{
				//lblContrasena.Visible = true;
				txtContrasena.Visible = true;
				pnlAppletFirma.Visible = false;

				btnFirmaDigital.Text = "Firma Digital";
			}
		}

		protected void Button1_Click(object sender, EventArgs e)
		{
			string strRedirect = "~/Principal.aspx";
			clsSesion.Current.LoginUsuario = txtUsuario.Text;
			Response.Redirect(strRedirect, true);
		}

		//Evento de efecto Porback al aceptar el mensaje de confirmacion
		private void MensajeConfirmacion()
		{
			string lastr_Resultado = ws_SGService.uwsCerrarSesionesActivas(txtUsuario.Text.Trim());//Si usuario da clic en aceptar guardamos el expediente.
            //txtContrasena.Text = ViewState["Contrasena"].ToString();
            if (lastr_Resultado == "00")
            {
                IniciarSesionUsuario(txtUsuario.Text.Trim(), txtContrasena.Text);
            }
		}

		//Inicializa el modal
		private void MensajeConfirmaProceso(string msg)
		{
			//Inicializa el modal
			var sb = new StringBuilder();
			sb.AppendFormat("var x='" + msg + "';");
			sb.AppendFormat("var msg = window.confirm(' Se han detectado sesiones activas. '+\n x);\n");
			sb.Append("if (msg)\n");
			sb.Append("__doPostBack('MyConfirmationPostBackEventTarget', msg);\n");
			ClientScript.RegisterStartupScript(GetType(), "MyScriptKey", sb.ToString(), true);
		}

		/// <summary>
		/// Metodo para el ingreso a la aplicacion con firma digital
		/// </summary>
		/// <param name="str_CedUsuario">Cedula Usuario</param>
		/// <param name="str_CedCertficado">Cedula en certificado</param>
		private void IngresoFirma(string str_CedUsuario, string str_CedCertficado)
		{
			if (str_CedUsuario.Trim() == str_CedCertficado.Trim())
			{
				string strRedirect = "~/Principal.aspx";
				clsSesion.Current.LoginUsuario = str_CedCertficado.Trim();
				Response.Redirect(strRedirect, true);
			}
		}

		/// <summary>
		/// Metodo para loguear al usuario
		/// </summary>
		/// <param name="str_Usuario">Id de usuario</param>
		private void LoguearUsuario(string str_Usuario)
		{
			FormsAuthenticationTicket tkt;
			string cookiestr;
			HttpCookie ck;
			tkt = new FormsAuthenticationTicket(1, str_Usuario, DateTime.Now,
					DateTime.Now.AddMinutes(1), chkPersistCookie.Checked, "La sesion ha expirado");
			cookiestr = FormsAuthentication.Encrypt(tkt);
			ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);

			//Solución a estudio vulnerabilidades 2020, 
			//Vulnerabilidad reportada: SSL/TLS: Missing `secure` Cookie Attribute
			//Solución: Establezca el atributo 'seguro' para cualquier cookie que se envíe a través de una conexión SSL / TLS
			ck.Secure = true;

			if (chkPersistCookie.Checked)
				ck.Expires = tkt.Expiration;
			ck.Path = FormsAuthentication.FormsCookiePath;
			Response.Cookies.Add(ck);
			string strRedirect;
			strRedirect = Request["ReturnUrl"];
			if (strRedirect == null)
				strRedirect = "~/Principal.aspx";

			clsSesion.Current.LoginUsuario = str_Usuario;
			FormsAuthentication.RedirectFromLoginPage(str_Usuario, chkPersistCookie.Checked); // Response.Redirect(strRedirect, true);

			if (chkPersistCookie.Checked)
				clsSesion.Current.gint_TiempoOcio = 30;
			else clsSesion.Current.gint_TiempoOcio = 30;

			ConsultarPoliticas();
		}

		/// <summary>
		/// Evento de boton al confirmar credenciales
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnConfirmacion_Click(object sender, EventArgs e)
		{
			string lstr_Usuario = txtUsuario.Text;
			string lstr_Clave = ViewState["Contrasena"].ToString();
			string[] lastr_Resultado = new string[9];
			string lstr_Mensaje = "Mensaje generico";

			string lstr_CodConfirmacion = "99";
			DataSet lds_Usuarios = ws_SGService.uwsConsultarUsuarios(lstr_Usuario, "", "");
			DateTime lstr_FechaModificado = Convert.ToDateTime(lds_Usuarios.Tables["Table"].Rows[0]["FchModifica"].ToString());
			string str_fecha = String.Empty;
			str_fecha = lstr_FechaModificado.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
			string lstr_IpUsuario = ObtenerIPUsuario();
			lastr_Resultado = ws_SGService.uwsConfirmarUsuario(lstr_Usuario, gcls_Seguridad.CifrarTextoAES(lstr_Usuario + lstr_Clave), txtCodigo.Text.Trim(), str_fecha, lstr_IpUsuario);
			lstr_CodConfirmacion = lastr_Resultado[0];
			lstr_Mensaje = lastr_Resultado[8];
			switch (lstr_CodConfirmacion)
			{
				case "00":
					{
						string lstr_IdSesion = lastr_Resultado[3];
						string lstr_SociedadGL = lastr_Resultado[5];
						string lstr_NomSociedad = lastr_Resultado[6];
						string lstr_CorreoUsuario = lastr_Resultado[2];
						string lstr_TipoIdUsuario = lastr_Resultado[7];
						clsSesion.Current.IdSesion = lstr_IdSesion;
						clsSesion.Current.NomUsuario = lastr_Resultado[1];
						clsSesion.Current.IPSesion = lstr_IpUsuario;
						clsSesion.Current.SociedadUsr = lstr_SociedadGL;
						clsSesion.Current.NombreSociedadGL = lstr_NomSociedad;
						clsSesion.Current.gbol_FirmaDigital = false;
						clsSesion.Current.CorreoUsuario = lstr_CorreoUsuario;
						clsSesion.Current.TipoIdUsuario = lstr_TipoIdUsuario;
						LoguearUsuario(lstr_Usuario);
						break;
					}
				case "-1":
					{
						//Se realizo un cambio previo
						lblMensajeConfirmacion.Text = "El usuario otorgado fue modificado en otra sesión";
						lblMensajeConfirmacion.Visible = true;
						break;
					}
				case "-2":
					{
						lblMensajeConfirmacion.Text = "El usuario no existe";
						lblMensajeConfirmacion.Visible = true;
						break;
					}
				case "-3":
					{
						//La contraseña es incorrecta
						lblMensajeConfirmacion.Text = "La contraseña es incorrecta";
						lblMensajeConfirmacion.Visible = true;
						break;
					}
				case "-4":
					{
						//El còdigo no concuerda
						lblMensajeConfirmacion.Text = "El código es incorrecto";
						lblMensajeConfirmacion.Visible = true;
						break;
					}
				case "-5":
					{
						//El còdigo no concuerda
						lblMensajeConfirmacion.Text = "La cuenta se encuentra bloqueada.";
						lblMensajeConfirmacion.Visible = true;
						break;
					}
				default:
					{
						//Ocurrió un problema
						lblMensajeConfirmacion.Text = "Error al realizar la acción";
						lblMensajeConfirmacion.Visible = true;
						break;
					}
			}
		}


		/// <summary>
		/// Metodo para obtener la IP de la maquina del cliente
		/// </summary>
		/// <returns></returns>
		private static string ObtenerIPUsuario()
		{
			string DireccionIP = string.Empty;
			if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
			{
				DireccionIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
			}
			else if (HttpContext.Current.Request.UserHostAddress.Length != 0)
			{
				DireccionIP = HttpContext.Current.Request.UserHostAddress;
			}
			return DireccionIP;
		}


		/// <summary>
		/// Metodo utilizado en el ingreso por firma digital
		/// </summary>
		/// <param name="str_NombreUsuario"></param>
		/// <param name="str_Cedula"></param>
		/// <returns></returns>
		[WebMethod]
		public static string AutenticarFirma(string str_NombreUsuario, string str_Cedula)
		{
			string lstr_CodLogueo = String.Empty;
			string[] lastr_Resultado = new string[6];
			string lastr_ResultadoCierre = String.Empty;

			string[] lstr_CedulaArreglo = str_Cedula.Split('-');
			string lstr_CedulaPersona = lstr_CedulaArreglo[1] + lstr_CedulaArreglo[2] + lstr_CedulaArreglo[3];
			string lstr_IpUsuario = ObtenerIPUsuario();
			lastr_Resultado = ws_SGService.uwsLoguearUsuarioFirma(lstr_CedulaPersona, str_NombreUsuario, lstr_IpUsuario);
			lstr_CodLogueo = lastr_Resultado[0];
			switch (lstr_CodLogueo)
			{
				case "-1":
					{
						//El usuario no se encuentra registrado en el sistema
						//Response.Redirect("~/Seguridad/Usuarios.aspx", false);
						string lstr_IdSesion = lastr_Resultado[3];
						string lstr_SociedadGL = lastr_Resultado[5];
						string lstr_NomSociedad = lastr_Resultado[6];
						string lstr_CorreoUsuario = lastr_Resultado[2];
						string lstr_TipoIdUsuario = lastr_Resultado[7];
						clsSesion.Current.NombreSociedadGL = lstr_NomSociedad;
						clsSesion.Current.NomUsuario = lastr_Resultado[1];
						clsSesion.Current.IdSesion = lstr_IdSesion;
						clsSesion.Current.IPSesion = lstr_IpUsuario;
						clsSesion.Current.SociedadUsr = lstr_SociedadGL;
						clsSesion.Current.LoginUsuario = lstr_CedulaPersona;
						clsSesion.Current.gbol_FirmaDigital = true;
						clsSesion.Current.CorreoUsuario = lstr_CorreoUsuario;
						clsSesion.Current.TipoIdUsuario = lstr_TipoIdUsuario;
						break;
					}
				case "-2":
					{
						MessageBox.Show("La cuenta se encuentra bloqueada. Contacte al administrador");//La cuenta del usuario se encuentra bloqueada
						break;
					}
				case "-3":
					{
						lastr_ResultadoCierre = ws_SGService.uwsCerrarSesionesActivas(str_NombreUsuario);//Si usuario da clic en aceptar guardamos el expediente.
						if (lastr_ResultadoCierre == "00")
						{
							//MensajeConfirmaProceso("¿Desea cerrarla e iniciar sesión nuevamente?");
							AutenticarFirma(str_NombreUsuario, str_Cedula);
						}
						break;
					}
				case "00":
					{
						string lstr_IdSesion = lastr_Resultado[3];
						string lstr_SociedadGL = lastr_Resultado[5];
						string lstr_NomSociedad = lastr_Resultado[6];
						string lstr_CorreoUsuario = lastr_Resultado[2];
						string lstr_TipoIdUsuario = lastr_Resultado[7];
						clsSesion.Current.NombreSociedadGL = lstr_NomSociedad;
						clsSesion.Current.NomUsuario = lastr_Resultado[1];
						clsSesion.Current.IdSesion = lstr_IdSesion;
						clsSesion.Current.IPSesion = lstr_IpUsuario;
						clsSesion.Current.SociedadUsr = lstr_SociedadGL;
						clsSesion.Current.LoginUsuario = lstr_CedulaPersona;
						clsSesion.Current.gbol_FirmaDigital = true;
						clsSesion.Current.CorreoUsuario = lstr_CorreoUsuario;
						clsSesion.Current.TipoIdUsuario = lstr_TipoIdUsuario;
						break;
					}
				default:
					{

						break;
					}

			}

			return lstr_CodLogueo;
		}

		/// <summary>
		/// Evento que se dispara al seleccionar cancelar
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnCancelar_Click(object sender, EventArgs e)
		{
			ViewState["Contrasena"] = "";
			pnlDatosLogin.Visible = true;
			pnlConfirmacion.Visible = false;
		}

		/// <summary>
		/// Evento que se dispara al seleccionar la opcion de registrar usuario
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnRegistrar_Click(object sender, EventArgs e)
		{
			Response.Redirect("~/RegistroUsuario.aspx", false);
		}

		/// <summary>
		/// Consulta las politicas existentes y las aplica al sistema
		/// </summary>
		private void ConsultarPoliticas()
		{
			DataSet ldst_Politicas = ws_SGService.uwsConsultarPoliticas("", "");
			DataTable ldt_Politica = ldst_Politicas.Tables["Table"];

			clsSesion.Current.gint_MaxIntentosFallidos = Convert.ToInt32(ldt_Politica.Rows[0]["MaxNroIntentosFallidos"].ToString());
			clsSesion.Current.gint_TiempoBloqueoClave = Convert.ToInt32(ldt_Politica.Rows[0]["TiempoBloqueoClave"].ToString());
			clsSesion.Current.gint_CantLetrasContrasena = Convert.ToInt32(ldt_Politica.Rows[0]["MinLetrasClave"].ToString());
			clsSesion.Current.gint_CantNumContrasena = Convert.ToInt32(ldt_Politica.Rows[0]["MinNumerosClave"].ToString());
            clsSesion.Current.gint_CantSimbolosContrasena = Convert.ToInt32(ldt_Politica.Rows[0]["MinCaracteresClave"].ToString());
            clsSesion.Current.gint_CantLetrasClave = Convert.ToInt32(ldt_Politica.Rows[0]["MinLetrasClave"].ToString());
            clsSesion.Current.gint_CantNumerosClave = Convert.ToInt32(ldt_Politica.Rows[0]["MinNumerosClave"].ToString());
			clsSesion.Current.gint_CantCaracteresClave = Convert.ToInt32(ldt_Politica.Rows[0]["MinCaracteresClave"].ToString());
			//  clsSesion.Current.gint_TiempoOcio = Convert.ToInt32(ldt_Politica.Rows[0]["TiempoOcio"].ToString());
			clsSesion.Current.gint_VigenciaClave = Convert.ToInt32(ldt_Politica.Rows[0]["MaxVigenciaClave"].ToString());
			clsSesion.Current.gint_ReutilizacionClave = Convert.ToInt32(ldt_Politica.Rows[0]["NroReutilizacionUltimasClaves"].ToString());
			EstablecerValorTiempoOcio(clsSesion.Current.gint_TiempoOcio);
			Session.Timeout = clsSesion.Current.gint_TiempoOcio.Equals(0) ? 3 : clsSesion.Current.gint_TiempoOcio;

		}

		/// <summary>
		/// Establece el tiempo de ocio en el sistema
		/// </summary>
		/// <param name="int_TiempoOcio"></param>
		private void EstablecerValorTiempoOcio(int int_TiempoOcio)
		{
			XmlDocument archivoConfig = new XmlDocument();
			archivoConfig.Load(Server.MapPath("~/Web.config"));
			XmlNode aNode = archivoConfig.DocumentElement.SelectSingleNode("/configuration/system.web/authentication/forms");
			XmlAttribute idAttribute = aNode.Attributes["timeout"];
			if (idAttribute != null)
			{
				string currentValue = idAttribute.Value;
				idAttribute.Value = Convert.ToString(int_TiempoOcio * 1000);
			}
		}

		protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
		{
			Response.Redirect("https://www.java.com/es/download/", true);

		}

		/// <summary>
		/// Evento que se dispara al seleccionar opcion de guardar contrasena nueva
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void btnAceptarCambio_Click(object sender, EventArgs e)
		{
			string lstr_ContrasenaActual = txtContrasenaActual.Text.Trim();
			string lstr_NuevaContrasena = txtNuevaContrasena.Text.Trim();
			string lstr_Confirmacion = txtConfirmacion.Text.Trim();
			if (lstr_NuevaContrasena == lstr_Confirmacion && lstr_ContrasenaActual != lstr_NuevaContrasena)
			{
				ConsultarUsuario(ViewState["IdUsuario"].ToString());
				CambiarContrasenaUsuario(ViewState["IdUsuario"].ToString(), lstr_ContrasenaActual, lstr_NuevaContrasena);
			}
			else
				MessageBox.Show("La nueva contraseña debe ser diferente a la actual.");
		}

		/// <summary>
		/// Metodo para cambiar la contrasena utilizada por un usuario
		/// </summary>
		/// <param name="str_Usuario">Id de usuario</param>
		/// <param name="str_ContrasenaActual">Contrasena vigente</param>
		/// <param name="str_NuevaContrasena">Nueva Contrasena</param>
		private void CambiarContrasenaUsuario(string str_Usuario, string str_ContrasenaActual, string str_NuevaContrasena)
		{
			string[] lstr_ResCambio = new string[2];
			if (ContrasenaCumplePoliticas(str_NuevaContrasena))
			{
				string lstr_FchModifica = clsSesion.Current.FechaUltimaConsulta;
				DateTime lstr_FechaModificado = Convert.ToDateTime(lstr_FchModifica);
				string lstr_fecha = lstr_FechaModificado.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
				lstr_ResCambio = ws_SGService.uwsActualizarPerfilUsuario(str_Usuario, str_ContrasenaActual, str_NuevaContrasena,
														str_Usuario, lstr_fecha);
				if (lstr_ResCambio[0] == "00")
				{
					IniciarSesionUsuario(str_Usuario, str_NuevaContrasena);
					//MessageBox.Show("Se han guardado los cambios");
				}
				else if (lstr_ResCambio[0] == "99")
				{
					MessageBox.Show("Error al guardar los cambios");
				}
				else
				{
					MessageBox.Show(lstr_ResCambio[1]);
				}
			}
			else
			{
                string str_MsjError = String.Format("La contraseña debe contener {0} letras, {1} números y {2} símbolos.",
                                Convert.ToString(clsSesion.Current.gint_CantLetrasContrasena),
                                Convert.ToString(clsSesion.Current.gint_CantNumContrasena),
                                Convert.ToString(clsSesion.Current.gint_CantSimbolosContrasena)); //.gint_CantCaracteresClave));
				MessageBox.Show(str_MsjError);
			}
		}

		/// <summary>
		/// Metodo que verifica si la contrasena cumple las politicas
		/// </summary>
		/// <param name="str_Contrasena"></param>
		/// <returns></returns>
		private bool ContrasenaCumplePoliticas(string str_Contrasena)
		{
			bool lboo_Resultado = false;
			int lint_Largo = 8;
			int lint_CantLetras = clsSesion.Current.gint_CantLetrasContrasena;
			int lint_CantNumeros = clsSesion.Current.gint_CantNumContrasena;
            int lint_CantSimbolos = clsSesion.Current.gint_CantSimbolosContrasena; //.gint_CantCaracteresClave;

			int lint_LargoContrasena = str_Contrasena.Count();
			int lint_CantLetrasContrasena = str_Contrasena.Count(Char.IsLetter);
			int lint_CantNumerosContrasena = str_Contrasena.Count(Char.IsNumber);
			int lint_CantSimbolosContrasena = str_Contrasena.Count(Char.IsPunctuation) + str_Contrasena.Count(Char.IsSymbol);
			if (lint_CantLetrasContrasena >= lint_CantLetras && lint_CantNumerosContrasena >= lint_CantNumeros &&
					lint_CantSimbolosContrasena >= lint_CantSimbolos && lint_LargoContrasena >= lint_Largo)
			{
				lboo_Resultado = true;
			}
			return lboo_Resultado;
		}

		/// <summary>
		/// Obtiene los datos del usuario
		/// </summary>
		/// <param name="str_IdUsuario"></param>
		private void ConsultarUsuario(string str_IdUsuario)
		{
			DataSet lds_Usuarios = ws_SGService.uwsConsultarUsuarios(str_IdUsuario, "", "");
			DataTable ldt_Usuario = lds_Usuarios.Tables["Table"];
			if (ldt_Usuario.Rows.Count > 0)
				clsSesion.Current.FechaUltimaConsulta = ldt_Usuario.Rows[0]["FchModifica"].ToString();
		}


		private float getInternetExplorerVersion()
		{
			// Retorna la version del Internet Explorer y si no es IE retorna -1
			float vVersion = -1;
			System.Web.HttpBrowserCapabilities browser = Request.Browser;
			if (browser.Browser == "IE" || browser.Browser == "InternetExplorer")
				vVersion = (float)(browser.MajorVersion + browser.MinorVersion);
			return vVersion;
		}

        //ejecuta para recurar el código de activacion
        protected void lkbRecuperar_Click(object sender, EventArgs e)
        {
            string str_Usuario = txtUsuario.Text.Trim();
            string str_Codigo = "";
            string str_Respuesta = "";
            str_Respuesta = ws_SGService.uwsConsultarCodigo(str_Usuario);
                if (str_Respuesta == "" || str_Respuesta== null)
                    MessageBox.Show("Error en la operación");
                else
                    MessageBox.Show("Se ha enviado un corero electrónico a su cuentra con el código de activación");
        }
            

	}
}