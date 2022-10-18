using Presentacion.Compartidas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Presentacion.Mantenimiento.Gestiones
{
    public partial class frmNuevoPrevisionesIncobrables : BASE
    {
        # region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private char gchr_MensajeError;
        private char gchr_MensajeExito;
        private string gstr_Usuario = String.Empty;
        private string gstr_ModuloActual = String.Empty;
        private String[] garr_Modulos;

        //static DataSet gds_Modulos = new DataSet();
        protected DataSet gds_Modulos
        {
            get
            {
                if (ViewState["gds_Modulos"] == null)
                    ViewState["gds_Modulos"] = new DataSet();
                return (DataSet)ViewState["gds_Modulos"];
            }
            set
            {
                ViewState["gds_Modulos"] = value;
            }
        }
        //static DataSet gds_ModulosPermiso = new DataSet();
        protected DataSet gds_ModulosPermiso
        {
            get
            {
                if (ViewState["gds_ModulosPermiso"] == null)
                    ViewState["gds_ModulosPermiso"] = new DataSet();
                return (DataSet)ViewState["gds_ModulosPermiso"];
            }
            set
            {
                ViewState["gds_ModulosPermiso"] = value;
            }
        }
        //static DataSet gds_ClasesDocumento = new DataSet();
        protected DataSet gds_ClasesDocumento
        {
            get
            {
                if (ViewState["gds_ClasesDocumento"] == null)
                    ViewState["gds_ClasesDocumento"] = new DataSet();
                return (DataSet)ViewState["gds_ClasesDocumento"];
            }
            set
            {
                ViewState["gds_ClasesDocumento"] = value;
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
                    garr_Modulos = clsSesion.Current.PermisosModulos;
                    if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmPrevisionesIncobrables"))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }                        
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

        protected void btnVolverPrevisionesIncobrables_Click1(object sender, EventArgs e)
        {
            Response.Redirect("~/Mantenimiento/frmPrevisionesIncobrables.aspx", false);
        }

        protected void btnCrearPrevisionIncobrable_Click(object sender, EventArgs e)
        {
            String[] str_result = new String[3];
            string lstr_Estado = String.Empty;
            
            str_result = ws_SGService.uwsCrearPrevisionIncobrable(
                Convert.ToInt32(this.txtDiasMorosidad.Text), 
                Convert.ToDecimal(this.txtPorcEstimacion.Text), 
                this.txtDescripcion.Text, 
                gstr_Usuario);

            if ((str_result[0].ToString().Equals("00")) || str_result[0].ToString().Equals("True"))
                MostarMensaje("La creación de datos ha sido satisfactoria.", gchr_MensajeExito);
            else
                MostarMensaje("La creación de datos no ha sido satisfactoria.", gchr_MensajeError);
        }
    }
}