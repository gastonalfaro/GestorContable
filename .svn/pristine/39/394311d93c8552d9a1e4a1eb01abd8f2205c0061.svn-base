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
    public partial class frmNuevoParametro : BASE
    {
        #region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private char lchr_MensajeError;
        private char lchr_MensajeExito;
        private string gstr_ModuloActual = String.Empty;
        private string gstr_Modulo = String.Empty;
        private string gstr_Modulos = String.Empty;
        private string gstr_Usuario = String.Empty;
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

        DataTable subjects = new DataTable();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            
            gstr_Usuario = clsSesion.Current.LoginUsuario;
            lchr_MensajeError = clsSesion.Current.chr_MensajeError;
            lchr_MensajeExito = clsSesion.Current.chr_MensajeExito;
            gstr_ModuloActual = clsSesion.Current.gstr_ModuloActual;

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(gstr_Usuario))
                {
                    garr_Modulos = clsSesion.Current.PermisosModulos;

                   
                    if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmParametros"))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                        ConsultarModulos("","");
                    
                     string lstr_NomParametro = clsSesion.Current.NomParametro;

                    if(!lstr_NomParametro.Equals(string.Empty))
                    {
                        this.txtDesParametro.Text = lstr_NomParametro;
                        this.txtDesParametro.Enabled = false;
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
         
        protected void btnCrearParametro_Click(object sender, EventArgs e)
        {
            String[] str_result = new String[3];

            str_result = ws_SGService.uwsCrearParametro(txtIdParametro.Text, Convert.ToDateTime(this.txtFchVigencia.Text.Equals(string.Empty)? "01/01/1900" : this.txtFchVigencia.Text), ddlModulo.SelectedValue, txtDesParametro.Text, txtTipoParametro.Text, txtValor.Text, gstr_Usuario);
                

            if (str_result[0].ToString().Equals("00") || str_result[0].ToString().Equals("True"))
            {
                MostarMensaje("La creación de datos ha sido satisfactoria.", lchr_MensajeExito);
            }
            else
            {
                MostarMensaje("La creación de datos no ha sido satisfactoria.", lchr_MensajeError);
            }

            clsSesion.Current.NomParametro = string.Empty;
        }

        protected void btnParametroVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Mantenimiento/frmParametros.aspx", false);
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

        private void ConsultarModulos(string str_IdModulo, string str_NomModulo)
        {
            
            gds_Modulos = ws_SGService.uwsConsultarModulos(str_IdModulo, str_NomModulo);

            Boolean lbool_Existe = false;

            if (gds_Modulos.Tables["Table"].Rows.Count > 0)
            {
                for (int i = 0; gds_Modulos.Tables["Table"].Rows.Count > i; i++)
                {
                    DataRow ldr_ModulosRow = gds_Modulos.Tables["Table"].NewRow();
                    ldr_ModulosRow = gds_Modulos.Tables["Table"].Rows[i];

                    string lstr_ModuloActual = ldr_ModulosRow["IdModulo"].ToString().Trim();

                    for (int j = 0; garr_Modulos.Count() > j; j++)
                    {
                        if ((garr_Modulos[j] != null) && (garr_Modulos[j].Equals(lstr_ModuloActual)))
                        {
                            lbool_Existe = true;
                        }
                    }
                    if (!lbool_Existe)
                    {
                        gds_Modulos.Tables["Table"].Rows[i].Delete();
                        i--;
                    }
                    lbool_Existe = false;
                }

                ddlModulo.DataSource = gds_Modulos.Tables["Table"];
                ddlModulo.DataTextField = "NomModulo";
                ddlModulo.DataValueField = "IdModulo";
                ddlModulo.DataBind();
            }
            else
            {
                ddlModulo.DataSource = this.LlenarTablaVacia();
                ddlModulo.DataBind();
            }
        }

        private DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("IdModulo", typeof(string));
            ldt_TablaVacia.Columns.Add("NomModulo", typeof(string));
            ldt_TablaVacia.Columns.Add("Estado", typeof(string));
            DataRow ldr_FilaTabla = ldt_TablaVacia.NewRow();
            ldt_TablaVacia.Rows.Add(ldr_FilaTabla);
            return ldt_TablaVacia;
        }

        protected void btnParametroVolver_Click1(object sender, EventArgs e)
        {
            Response.Redirect("~/Mantenimiento/frmParametros.aspx", false);
        }
    }
}