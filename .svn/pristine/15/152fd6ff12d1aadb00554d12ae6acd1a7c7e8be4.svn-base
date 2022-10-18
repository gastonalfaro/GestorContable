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
    public partial class frmNuevaOperacion : BASE
    {
        # region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private char gchr_MensajeError;
        private char gchr_MensajeExito;
        private string gstr_Usuario = String.Empty;
        private string gstr_ModuloActual = String.Empty;
        private string gstr_Modulos = String.Empty;
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
        //private static DataSet gds_Catalogos = new DataSet();
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
            gstr_ModuloActual = clsSesion.Current.gstr_ModuloActual;
            gchr_MensajeError = clsSesion.Current.chr_MensajeError;
            gchr_MensajeExito = clsSesion.Current.chr_MensajeExito;
           // gstr_Modulo = gstr_ModuloActual.Remove(0, 4);

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(gstr_Usuario))
                {
                    garr_Modulos = clsSesion.Current.PermisosModulos;

                    if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, ""))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                    {
                        CargarModulos();
                        ConsultarCatalogos(null, "");
                        CargarClasesDocumento();
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
        
        private void CargarClasesDocumento()
        {
            gds_ClasesDocumento = ws_SGService.uwsConsultarClasesDocumento("", "");
            if (gds_Modulos.Tables["Table"].Rows.Count > 0)
            {
                ddlIdClaseDoc.DataTextField = "NomClaseDoc";
                ddlIdClaseDoc.DataValueField = "IdClaseDoc";
                ddlIdClaseDoc.DataSource = gds_ClasesDocumento.Tables["Table"];
                ddlIdClaseDoc.DataBind();
            }
            else
            {
                ddlIdClaseDoc.DataTextField = "NomClaseDoc";
                ddlIdClaseDoc.DataValueField = "IdClaseDoc";
                ddlIdClaseDoc.DataSource = this.LlenarTablaVaciaClasesDocumento();
                ddlIdClaseDoc.DataBind();
            }

        }

        private DataTable LlenarTablaVaciaClasesDocumento()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("IdClaseDoc", typeof(string));
            ldt_TablaVacia.Columns.Add("NomClaseDoc", typeof(string));
            ldt_TablaVacia.Columns.Add("Estado", typeof(string));
            DataRow ldr_FilaTabla = ldt_TablaVacia.NewRow();
            ldt_TablaVacia.Rows.Add(ldr_FilaTabla);
            return ldt_TablaVacia;
        }

        private void ConsultarCatalogos(Nullable<int> int_IdCatalogos, string str_Nombre)
        {
            string lstr_submodulo = String.Empty;

            for (int i = 0; garr_Modulos.Count() > i; i++)
            {
                if ((i == 0) && (garr_Modulos[i] != null))
                    lstr_submodulo = "'" + garr_Modulos[i] + "'";
                else if (garr_Modulos[i] != null)
                {
                    lstr_submodulo = lstr_submodulo + ",'" + garr_Modulos[i] + "'";
                }
            }

            if (!string.IsNullOrEmpty(lstr_submodulo))
                gstr_Modulos = "IdModulo IN (" + lstr_submodulo + ")";

            Nullable<int> lint_IdCatalogo = null;

            CargarModulos();
        }

        private void CargarModulos()
        {
            gds_Modulos = ws_SGService.uwsConsultarModulos("", "");
            if (gds_Modulos.Tables["Table"].Rows.Count > 0)
            {
                ddlIdModulo.DataTextField = "NomModulo";
                ddlIdModulo.DataValueField = "IdModulo";
                ddlIdModulo.DataSource = gds_Modulos.Tables["Table"];
                ddlIdModulo.DataBind();
            }
            else
            {
                ddlIdModulo.DataTextField = "NomModulo";
                ddlIdModulo.DataValueField = "IdModulo";
                ddlIdModulo.DataSource = this.LlenarTablaVaciaModulo();
                ddlIdModulo.DataBind();
            }

        }

        private DataTable LlenarTablaVaciaModulo()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("IdModulo", typeof(string));
            ldt_TablaVacia.Columns.Add("NomModulo", typeof(string));
            ldt_TablaVacia.Columns.Add("Estado", typeof(string));
            DataRow ldr_FilaTabla = ldt_TablaVacia.NewRow();
            ldt_TablaVacia.Rows.Add(ldr_FilaTabla);
            return ldt_TablaVacia;
        }

        protected void btnVolverOperaciones_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Mantenimiento/frmOperaciones.aspx", false);
        }

        protected void btnCrearOperacion_Click(object sender, EventArgs e)
        {
            String[] str_result = new String[3];
          
            if (txtIdOperacion.Text.Equals("") || gstr_ModuloActual.Equals("") ||
                txtDescripcion.Text.Equals("") || ddlIdClaseDoc.SelectedValue.Equals("") ||
                chkEstados.Checked.ToString().Equals(""))
            {
                MostarMensaje("No se han ingresado los datos necesarios.", gchr_MensajeError);
            }
            else
            {
                if (!gstr_ModuloActual.Equals("MA"))
                    str_result = ws_SGService.uwsCrearOperacion(txtIdOperacion.Text, gstr_Modulos, txtDescripcion.Text, ddlIdClaseDoc.SelectedValue.Trim(), chkEstados.Checked ? "A" : "I", this.txtOperacionReserva.Text, gstr_Usuario);
                else
                    str_result = ws_SGService.uwsCrearOperacion(txtIdOperacion.Text, ddlIdModulo.SelectedValue, txtDescripcion.Text, ddlIdClaseDoc.SelectedValue.Trim(), chkEstados.Checked ? "A" : "I", this.txtOperacionReserva.Text, gstr_Usuario);

                if ((str_result[0].ToString().Equals("00")) || str_result[0].ToString().Equals("True"))
                    MostarMensaje("Se ingresó la Operación satisfactoriamente.", gchr_MensajeExito);
                //MostarMensaje(str_result[1].ToString(), gchr_MensajeExito);
                else
                    MostarMensaje("Error al agregar la Operación.", gchr_MensajeError);
                //MostarMensaje(str_result[1].ToString(), gchr_MensajeError);
            }
        }

        protected void btnVolverOperaciones_Click1(object sender, EventArgs e)
        {
            Response.Redirect("~/Mantenimiento/frmOperaciones.aspx", false);
        }
    }
}