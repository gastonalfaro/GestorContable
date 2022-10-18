using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class RecuperarContrasena : BASE
    {
        private static Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Web.UI.HtmlControls.HtmlGenericControl divCerrarSesion = (System.Web.UI.HtmlControls.HtmlGenericControl)Master.FindControl("divCerrarSesion");
            divCerrarSesion.Style.Add("display", "none");
        }
        /// <summary>
        /// Evento que se dispara al hacer clic en btnAceptar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            string lstr_ResRecuperacion = String.Empty;

            lstr_ResRecuperacion = ws_SGService.uwsRecuperarContrasena(txtUsuario.Text.Trim(), txtCorreo.Text.Trim());
            switch(lstr_ResRecuperacion)
            {
                case "-1":
                {
                    lblMensajeConf.Text = "El usuario no existe.";
                    break;
                }
                case "-2":
                {
                    lblMensajeConf.Text = "El email no corresponde al otorgado en el registro";
                    break;
                }
                case "99":
                {
                    lblMensajeConf.Text = "Error al realizar la opereación. Intente nuevamente.";
                    break;
                }
                default:
                {
                    lblMensajeConf.Text = "Se ha enviado un mensaje a su correo eléctronico.";
                    break;
                }
            }
        }

        /// <summary>
        /// Evento que se dispara al seleccionar btnAtras
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAtras_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx", true);
        }
    }
}