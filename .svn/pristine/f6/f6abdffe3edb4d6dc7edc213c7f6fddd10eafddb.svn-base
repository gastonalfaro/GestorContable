using Presentacion.Compartidas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.Seguridad;

namespace Presentacion.Seguridad.GestionUsuarios
{
    public partial class RegistroUsuario : BASE
    {
        private tSeguridad gcls_Seguridad = new tSeguridad();
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                DataSet lds_Politicas = ws_SGService.uwsConsultarPoliticas("", "");
                if (lds_Politicas.Tables.Count > 0)
                {
                    if (lds_Politicas.Tables["Table"].Rows.Count > 0)
                    {
                        clsSesion.Current.gint_CantLetrasContrasena = Convert.ToInt32(lds_Politicas.Tables["Table"].Rows[0]["MinLetrasClave"].ToString());
                        clsSesion.Current.gint_CantNumContrasena = Convert.ToInt32(lds_Politicas.Tables["Table"].Rows[0]["MinNumerosClave"].ToString());
                        clsSesion.Current.gint_CantSimbolosContrasena = Convert.ToInt32(lds_Politicas.Tables["Table"].Rows[0]["MinCaracteresClave"].ToString());

                        clsSesion.Current.gint_CantLetrasClave  = Convert.ToInt32(lds_Politicas.Tables["Table"].Rows[0]["MinLetrasClave"].ToString());
                        clsSesion.Current.gint_CantNumerosClave = Convert.ToInt32(lds_Politicas.Tables["Table"].Rows[0]["MinNumerosClave"].ToString());
                        clsSesion.Current.gint_CantCaracteresClave = Convert.ToInt32(lds_Politicas.Tables["Table"].Rows[0]["MinCaracteresClave"].ToString());
                    }
                }
                if (!String.IsNullOrEmpty(clsSesion.Current.LoginUsuario))
                {
                    pnlContrasena.Visible = false;
                    ddlOrigen.Enabled = false;
                    txtCedula.Text = clsSesion.Current.LoginUsuario;
                    ddlOrigen.SelectedValue = "Fisico";
                }
                else
                {
                    //finding the div associated with id 
                    System.Web.UI.HtmlControls.HtmlGenericControl divCerrarSesion = (System.Web.UI.HtmlControls.HtmlGenericControl)Master.FindControl("divCerrarSesion");
                    divCerrarSesion.Style.Add("display", "none");
                    pnlContrasena.Visible = true;
                    ddlOrigen.Enabled = true;
                    txtCedula.Text = String.Empty;
                }
            }
        }
        

        private string GenerateString(int size, string Alphabet = "abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789")
        {
            Random rand = new Random();
            char[] chars = new char[size];
            for (int i = 0; i < size; i++)
            {
                chars[i] = Alphabet[rand.Next(Alphabet.Length)];
            }
            return new string(chars);
        }

        /// <summary>
        /// Evento al hacer clic en el boton de registrar usuario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRegistrarUsuario_Click(object sender, EventArgs e)
        {         
            string lstr_Cedula = txtCedula.Text.Trim();
            string lstr_Nombre = Request.Form[txtNombre.UniqueID];
            string lstr_Contrasena = txtContrasena.Text.Trim();
            string lstr_Email = txtEmail.Text.Trim();
            string lstr_TipoID = ddlOrigen.SelectedValue;
            if (txtEmail.Text.Trim() == txtConfCorreo.Text.Trim() && verificarFormatoCorreo(txtEmail.Text.Trim()))
            {
                if (!String.IsNullOrEmpty(lstr_Email) && !String.IsNullOrEmpty(txtConfCorreo.Text.Trim()))
                {

                    if (String.IsNullOrEmpty(clsSesion.Current.LoginUsuario))
                    {
                        if (!String.IsNullOrEmpty(lstr_Contrasena) && !String.IsNullOrEmpty(txtConfirmarContrasena.Text.Trim()))
                        {
                            if (!String.IsNullOrEmpty(lstr_Nombre))
                            {

                                if (lstr_Contrasena == txtConfirmarContrasena.Text.Trim())
                                {
                                    lblMensajeConfirmacion.Visible = false;
                                    RegistrarUsuario(lstr_Cedula, lstr_Nombre, lstr_Contrasena, lstr_Email, lstr_TipoID);

                                }
                                else
                                {
                                    lblMensajeConfirmacion.Text = "Las contraseñas deben ser iguales.";
                                    lblMensajeConfirmacion.Visible = true;
                                }
                            }
                            else
                            {
                                lblMensajeConfirmacion.Text = "No se ha encontrado ningún nombre para la cédula insertada.";
                                lblMensajeConfirmacion.Visible = true;
                            }
                        }
                        else
                        {
                            lblMensajeConfirmacion.Text = "Especifique una contraseña.";
                            lblMensajeConfirmacion.Visible = true;
                        }
                    }
                    else
                    {
                        lblMensajeConfirmacion.Visible = false;
                        //RegistrarUsuarioFirma(lstr_Cedula, lstr_Nombre, lstr_Email);

                        lstr_Contrasena = this.GenerateString(5, "abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ");
                        lstr_Contrasena += this.GenerateString(3, "0123456789");
                        lstr_Contrasena += this.GenerateString(1, "!#$%&.,*+");

                        string str_Encriptado = gcls_Seguridad.CifrarTextoAES(lstr_Cedula + lstr_Contrasena);

                        RegistrarUsuarioFirma(lstr_Cedula, lstr_Nombre, str_Encriptado, lstr_Email);
                    }
                }
                else
                {
                    lblMensajeConfirmacion.Text = "Especifique una dirección de correo electrónico.";
                    lblMensajeConfirmacion.Visible = true;
                }
            }
            else
            {
                lblMensajeConfirmacion.Text = "Las direcciones de correo no concuerdan o no son validas.";
                lblMensajeConfirmacion.Visible = true;
            }
        }

        /// <summary>
        /// Se registra un usuario en el sistema
        /// </summary>
        /// <param name="str_Cedula"></param>
        /// <param name="str_Nombre"></param>
        /// <param name="str_Contrasena"></param>
        /// <param name="str_Email"></param>
        /// <param name="str_TipoID"></param>
        private void RegistrarUsuario(string str_Cedula, string str_Nombre, string str_Contrasena, 
            string str_Email, string str_TipoID)
        {
            string lstr_Resultado = "99";
           
            if (ContrasenaCumplePoliticas(str_Contrasena))
            {
                switch (str_TipoID)
                {
                    case "Fisico":
                        {
                            str_TipoID = "F";
                            break;
                        }
                    case "Juridico":
                        {
                            str_TipoID = "J";
                            break;
                        }
                    case "DIMEX":
                        {
                            str_TipoID = "DI";
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }

                //string Prueba = gcls_Seguridad.CifrarTextoAES(str_Cedula + str_Contrasena);
                string str_Encriptado = gcls_Seguridad.CifrarTextoAES(str_Cedula + str_Contrasena);
                if (str_Encriptado.Length > 100)
                {
                    MessageBox.Show("Error al encriptar contraseña"); //en la bd se almacena solo 100 
                }
                else
                {
                    lstr_Resultado = ws_SGService.uwsRegistrarUsuario(str_Cedula, str_TipoID, str_Nombre, str_Encriptado, str_Email);
                    switch (lstr_Resultado)
                    {
                        case "-1":
                            {

                                lblMensajeConfirmacion.Text = "El usuario específicado ya esta registrado.";
                                lblMensajeConfirmacion.Visible = true;
                                break;
                            }
                        case "-2":
                            {
                                lblMensajeConfirmacion.Text = "La dirección de correo está siendo utilizada por otro usuario.";
                                lblMensajeConfirmacion.Visible = true;
                                break;
                            }
                        case "99":
                            {
                                lblMensajeConfirmacion.Text = "Error al realizar el registro. Intente nuevamente.";
                                lblMensajeConfirmacion.Visible = true;
                                break;

                            }
                        default:
                            {
                                lblMensajeConfirmacion.Text = "Se ha enviado un mensaje al correo otorgado.";
                                lblMensajeConfirmacion.Visible = true;
                                break;
                            }
                    }
                }
            }
            else
            {
                lblMensajeConfirmacion.Text = String.Format("La contraseña debe contener {0} letras, {1} números y {2} símbolos.",
                    Convert.ToString(clsSesion.Current.gint_CantLetrasContrasena), Convert.ToString(clsSesion.Current.gint_CantNumContrasena),
                    Convert.ToString(clsSesion.Current.gint_CantSimbolosContrasena)); //.gint_CantCaracteresClave));
                lblMensajeConfirmacion.Visible = true;
            }            
        }

        /// <summary>
        /// Se registra un usuario mediante el uso de la firma
        /// </summary>
        /// <param name="str_Cedula"></param>
        /// <param name="str_Nombre"></param>
        /// <param name="str_Email"></param>
        private void RegistrarUsuarioFirma(string str_Cedula, string str_Nombre, string str_Contrasena, string str_Email)
        {
            string lstr_Resultado = "99";
            try
            {
                lstr_Resultado = ws_SGService.uwsRegistrarUsuarioFirma(str_Cedula, "F", str_Nombre, str_Contrasena, str_Email);
                switch (lstr_Resultado)
                {
                    case "-1":
                        {
                            lblMensajeConfirmacion.Text = "El usuario especificado ya está registrado.";
                            lblMensajeConfirmacion.Visible = true;
                            break;
                        }
                    case "-2":
                        {
                            lblMensajeConfirmacion.Text = "La dirección de correo está siendo utilizada por otro usuario.";
                            lblMensajeConfirmacion.Visible = true;
                            break;
                        }
                    case "00":
                        {
                            
                            clsSesion.Current.LoginUsuario = str_Cedula;
                            Response.Redirect("~/Principal.aspx", false);
                            break;
                        }
                    default:
                        {
                            lblMensajeConfirmacion.Text = "Error al realizar el registro. Intente nuevamente.";
                            lblMensajeConfirmacion.Visible = true;
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                lblMensajeConfirmacion.Text = "Error al realizar el registro. Intente nuevamente.";
                lblMensajeConfirmacion.Visible = true;
            }
        }

        /// <summary>
        /// Evento que se dispara al seleccionar la opcion de atras
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAtras_Click(object sender, EventArgs e)
        {
            clsSesion.Current.LoginUsuario = String.Empty;
            Response.Redirect("~/Login.aspx", true);
        }

        /// <summary>
        /// Validacion de politicas de constrasena
        /// </summary>
        /// <param name="str_Contrasena"></param>
        /// <returns></returns>
        private bool ContrasenaCumplePoliticas(string str_Contrasena)
        {
            bool lboo_Resultado = false;
            int lint_Largo = 8;
            int lint_CantLetras = clsSesion.Current.gint_CantLetrasContrasena;
            int lint_CantNumeros = clsSesion.Current.gint_CantNumContrasena;
            int lint_CantSimbolos = clsSesion.Current.gint_CantSimbolosContrasena;// .gint_CantCaracteresClave;

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

        private bool verificarFormatoCorreo(string str_correo)
        {
            if (str_correo.Contains("@") && str_correo.Contains("."))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void txtCedula_TextChanged(object sender, EventArgs e)
        {

        }
    }
}