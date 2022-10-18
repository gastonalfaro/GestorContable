using Presentacion.Compartidas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Presentacion.Mantenimiento.Gestiones
{
    public partial class frmNuevoCatalogoGeneral : BASE
    {
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private char lchr_MensajeError;
        private char lchr_MensajeExito;
        string lstr_usuario = String.Empty;
        private string gstr_Usuario = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            lstr_usuario = clsSesion.Current.LoginUsuario;
            lchr_MensajeError = clsSesion.Current.chr_MensajeError;
            lchr_MensajeExito = clsSesion.Current.chr_MensajeExito;
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(lstr_usuario))
                {
                    if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, ""))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                    {

                    }
                }
                else
                {
                    Response.Redirect("~/Login.aspx", true);
                }

            }
            else
            {
                if (string.IsNullOrEmpty(lstr_usuario))
                    Response.Redirect("~/Login.aspx", true);
            }

        }

        protected void btnCatalogoVolver_Click(object sender, EventArgs e)
        {
             Response.Redirect("~/Mantenimiento/frmCatalogosGenerales.aspx", false);
        }

        protected void btnCatalogoGuardar_Click(object sender, EventArgs e)
        {
            String[] str_result = new String[3];
            
            //str_result = "m"; ws_SGService.uwsCrearCatalogo(txtNuevoNomCatalogo.Text, txtNuevoNomCatalogo.Text, "", "jnet");
            if (str_result[0].ToString().Equals("00"))
            {
                MostarMensaje(str_result[1].ToString(), lchr_MensajeError);
            }
            else
            {
                MostarMensaje(str_result[1].ToString(), lchr_MensajeExito);
            }
           
        }

        private void MostarMensaje(string str_TextMensaje, char chr_tipo)
        {
            if (chr_tipo == lchr_MensajeError)
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
    }
}