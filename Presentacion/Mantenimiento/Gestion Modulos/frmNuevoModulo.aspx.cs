using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using Presentacion.Compartidas;
using System.Web.UI.HtmlControls;


namespace Presentacion.Mantenimiento.Gestion_Modulos
{
    public partial class frmNuevoModulo : BASE
    {
        # region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private char gchr_MensajeError;
        private char gchr_MensajeExito;
        private string gstr_Usuario = String.Empty;
        //static DataSet gds_Catalogos = new DataSet();
        protected DataSet gds_Catalogos
        {
            get
            {
                if (ViewState["gds_Catalogos"] == null)
                    ViewState["gds_Catalogos"] = new DataSet();
                return (DataSet)ViewState["gds_Catalogos"];
            }
            set
            {
                ViewState["gds_Catalogos"] = value;
            }
        }
        # endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            gstr_Usuario = clsSesion.Current.LoginUsuario;
            gchr_MensajeError = clsSesion.Current.chr_MensajeError;
            gchr_MensajeExito = clsSesion.Current.chr_MensajeExito;
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(gstr_Usuario))
                {
                    if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, ""))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else { }
                }
                else
                {
                    Response.Redirect("~/Login.aspx", true);
                }

            }
            else
            {
                if (string.IsNullOrEmpty(gstr_Usuario))
                    Response.Redirect("~/Login.aspx", true);
            }
        }

        protected void btnCrearModulo_Click(object sender, EventArgs e)
        {
            String[] str_result = new String[3];

            if (this.txtIdModulo.Text.Equals(string.Empty) || this.txtNomModulo.Text.Equals(string.Empty))
            {
                MostarMensaje("Error debe ingresar todos los datos.", gchr_MensajeError);
            }else{
                str_result = ws_SGService.uwsCrearModulo(txtIdModulo.Text, txtNomModulo.Text, this.chkCrearEstado.Checked ? "A" : "I", gstr_Usuario);
                if (str_result[0].ToString().Equals("00") || (str_result[0].ToString().Equals("True")))
                    MostarMensaje("Se creó el nuevo módulo correctamente.", gchr_MensajeExito);
                else
                    MostarMensaje("Error al crear el nuevo módulo.", gchr_MensajeError);
            
            }
        }

        protected void btnModuloVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("../frmModulos.aspx", false);
        }

        private void MostarMensaje(string str_TextMensaje, char chr_tipo)
        {
            if (chr_tipo == '1')
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

        protected void chkCrearEstado_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void btnVolverModulo_Click(object sender, EventArgs e)
        {

        }
    }
}