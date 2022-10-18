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
    public partial class frmNuevoBanco : BASE
    {
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private char gchr_MensajeError;
        private char gchr_MensajeExito;
        private string gstr_ModuloActual = String.Empty;
        private string gstr_Usuario = String.Empty;
        //static DataSet gds_Paises = new DataSet();
        protected DataSet gds_Paises
        {
            get
            {
                if (ViewState["gds_Paises"] == null)
                    ViewState["gds_Paises"] = new DataSet();
                return (DataSet)ViewState["gds_Paises"];
            }
            set
            {
                ViewState["gds_Paises"] = value;
            }
        }

        DataTable subjects = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            gstr_Usuario = clsSesion.Current.LoginUsuario;
            gstr_ModuloActual = clsSesion.Current.gstr_ModuloActual;
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
                    else
                    {
                        ConsultarPaises("",""); 
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

        private void ConsultarPaises(string str_IdPais, string str_NomModulo)
        {
            gds_Paises = ws_SGService.uwsConsultarPaises(str_IdPais, str_NomModulo);

            if (gds_Paises.Tables["Table"].Rows.Count > 0)
            {
                ddlIdPais.DataSource = gds_Paises.Tables["Table"];
                ddlIdPais.DataTextField = "NomPais";
                ddlIdPais.DataValueField = "IdPais";
                ddlIdPais.DataBind();
            }
            else
            {
                ddlIdPais.DataSource = this.LlenarTablaVacia();
                ddlIdPais.DataBind();
            }
        }

        private DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("NomPais", typeof(string));
            ldt_TablaVacia.Columns.Add("IdPais", typeof(string));
            DataRow ldr_FilaTabla = ldt_TablaVacia.NewRow();
            ldt_TablaVacia.Rows.Add(ldr_FilaTabla);
            return ldt_TablaVacia;
        }

        protected void btnBancoVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("../frmBancos.aspx", false);
        }

        protected void chkCrearEstado_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void btnCrearBanco_Click(object sender, EventArgs e)
        {
            //string str_IdBanco, string str_IdBancoPropio, string str_IdSociedadFi, string str_NomBanco, string str_IdPais, string str_Telefono, string str_Contacto, string str_Estado, string str_UsrCreacion
            string[] str_result = ws_SGService.uwsCrearBanco(txtIdBanco.Text,txtNomModulo.Text, "", "",ddlIdPais.SelectedValue, txtTelefono.Text, txtContacto.Text, chkCrearEstado.Checked.ToString(), gstr_Usuario);
                
            if (str_result[0].ToString().Equals("00") || str_result[0].ToString().Equals("True"))
                MostarMensaje("Error al crear el nuevo Banco.", gchr_MensajeError);
            else
                MostarMensaje("Se creó el nuevo Banco correctamente.", gchr_MensajeExito);
            
        }

    }
}