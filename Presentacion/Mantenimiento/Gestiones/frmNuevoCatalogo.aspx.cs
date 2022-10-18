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
    public partial class frmNuevoCatalogo : BASE
    {
        # region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private char gchr_MensajeError;
        private char gchr_MensajeExito;
        // private static string gstr_Usuario = String.Empty;
        private string gstr_Usuario = String.Empty;
        // private static string gstr_ModuloActual = String.Empty;
        // private static String[] garr_Modulos;
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
                    if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmCatalogosGenerales"))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                    {
                        CargarModulos();
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
            //    gds_ClasesDocumento = ws_SGService.uwsConsultarClasesDocumento("", "");
            //    if (gds_Modulos.Tables["Table"].Rows.Count > 0)
            //    {
            //        ddlIdClaseDoc.DataTextField = "NomClaseDoc";
            //        ddlIdClaseDoc.DataValueField = "IdClaseDoc";
            //        ddlIdClaseDoc.DataSource = gds_ClasesDocumento.Tables["Table"];
            //        ddlIdClaseDoc.DataBind();
            //    }
            //    else
            //    {
            //        ddlIdClaseDoc.DataTextField = "NomClaseDoc";
            //        ddlIdClaseDoc.DataValueField = "IdClaseDoc";
            //        ddlIdClaseDoc.DataSource = this.LlenarTablaVaciaClasesDocumento();
            //        ddlIdClaseDoc.DataBind();
            //    }

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

        private void CargarModulos()
        {
            gds_Modulos = ws_SGService.uwsConsultarModulos("", "");
            DataTable dt = new DataTable("Table");
            dt.Columns.Add(new DataColumn("IdModulo", typeof(string)));
            dt.Columns.Add(new DataColumn("NomModulo", typeof(string)));

            if (gds_Modulos.Tables["Table"].Rows.Count > 0)
            {
                for (int i = 0; i < gds_Modulos.Tables["Table"].Rows.Count; i++)
                {
                    DataRow ldr_ModuloRow = gds_Modulos.Tables["Table"].NewRow();
                    ldr_ModuloRow = gds_Modulos.Tables["Table"].Rows[i];

                    for (int j = 0; ((garr_Modulos[j] != null) && (j < garr_Modulos.Count())); j++)
                    {
                        if (garr_Modulos[j].Equals(ldr_ModuloRow["IdModulo"].ToString().Trim()))
                        {
                                DataRow dr = dt.NewRow();
                            dr["IdModulo"] = ldr_ModuloRow["IdModulo"].ToString();
                            dr["NomModulo"] = ldr_ModuloRow["NomModulo"].ToString();
                            dt.Rows.Add(dr);
                        }
                    }

                }
                gds_ModulosPermiso.Tables.Add(dt);
                ddlIdModulo.DataTextField = "NomModulo";
                ddlIdModulo.DataValueField = "IdModulo";
                ddlIdModulo.DataSource = gds_ModulosPermiso.Tables["Table"];
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


        protected void btnVolverOperaciones_Click1(object sender, EventArgs e)
        {
            Response.Redirect("~/Mantenimiento/frmCatalogosGenerales.aspx", false);
        }

        protected void btnCrearCatalogo_Click(object sender, EventArgs e)
        {
            String[] str_result = new String[3];
            string lstr_Estado = String.Empty;
            if ( chkEstado.Checked.ToString().Equals("True"))
            {
                lstr_Estado = "A";
            }

            
            str_result = ws_SGService.uwsCrearCatalogo(txtDescripcion.Text, txtDescripcion.Text,
                //ddlIdModulo.SelectedValue,
                "", lstr_Estado, gstr_Usuario);
                
            if ((str_result[0].ToString().Equals("00")) || str_result[0].ToString().Equals("True"))
            {
                MostarMensaje("La creación de datos ha sido satisfactoria.", gchr_MensajeExito);
            }
            else
            {
                MostarMensaje("La creación de datos no ha sido satisfactoria.", gchr_MensajeError);
            }
        }
    }
}