using Presentacion.Compartidas;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Presentacion.RevelacionNotas
{
    public partial class FormulariosPendientes : BASE
    {
        private List<string> DatosConsulta
        {
            get
            {
                return (List<string>)ViewState["Consulta"];
            }
            set
            {
                ViewState["Consulta"] = value;
            }
        }

        private string gstr_ModuloActual = String.Empty;
        private static Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private string str_Usuario = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            //VARO 09-03-2020
            string vSociedad;
            vSociedad = clsSesion.Current.SociedadUsr;

            str_Usuario = clsSesion.Current.LoginUsuario;

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(str_Usuario))
                {
                    if (!clsSeguridadVistas.MostrarElementos(str_Usuario, Master, "OBJ_RN"))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                    {
                        MostrarElementos(str_Usuario);
                        CargarDDLs();
                    }
                }
                else
                {
                    Response.Redirect("~/Login.aspx", true);
                }

            }
            else
            {
                if (string.IsNullOrEmpty(str_Usuario))
                    Response.Redirect("~/Login.aspx", true);

            }

            pnlSociedad.Visible = true;
        }

        public static Control FindControlRecursive(Control root, string id)
        {
            if (id == string.Empty)
                return null;

            if (root.ID == id)
                return root;

            foreach (Control c in root.Controls)
            {
                Control t = FindControlRecursive(c, id);
                if (t != null)
                {
                    return t;
                }
            }
            return null;
        }

        private void MostrarElementos(string str_usuario)
        {
            DataSet ldt_PermisosUsuario = ws_SGService.uwsConsultarPermisosUsuarios(str_usuario, "");
            gvFormularios.Columns[8].Visible = false;

            for (int i = 0; ldt_PermisosUsuario.Tables["Table"].Rows.Count > i; i++)
            {
                string lstr_IdObjeto = ldt_PermisosUsuario.Tables["Table"].Rows[i]["IdObjeto"].ToString();
                bool lbool_Actualizar = (bool)ldt_PermisosUsuario.Tables["Table"].Rows[i]["Actualizar"];
                bool lbool_Consultar = (bool)ldt_PermisosUsuario.Tables["Table"].Rows[i]["Consultar"];
                string lstr_IdliEncabezado = lstr_IdObjeto;

                if (lbool_Consultar)
                {
                    try
                    {
                        HtmlGenericControl hgcMenuEncabezado = (HtmlGenericControl)FindControlRecursive(Master.Page, lstr_IdliEncabezado);

                        if (hgcMenuEncabezado != null)
                            hgcMenuEncabezado.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        WebControl hgcMenuEncabezado = (WebControl)FindControlRecursive(Master.Page, lstr_IdliEncabezado);

                        if (hgcMenuEncabezado != null)
                            hgcMenuEncabezado.Visible = true;                          
                    }
                }

                /*if (lstr_IdObjeto.Equals("gvFormularios") && lbool_Actualizar)
                {
                    gvFormularios.Columns[8].Visible = true;
                }*/

                if (lstr_IdObjeto.Equals("gvFormularios"))
                {
                    if ((bool)ldt_PermisosUsuario.Tables["Table"].Rows[i]["Actualizar"])
                        gvFormularios.Columns[8].Visible = true;
                    else
                        gvFormularios.Columns[8].Visible = false;
                }
            }            
        }

        private void CargarDDLs()
        {
            string str_PlanCuentas = ConfigurationManager.AppSettings["PlanCuentas"];
            DataSet lds_AuxCuentas = ws_SGService.uwsConsultarCuentasContables("", str_PlanCuentas, "", "", "", "", "", "");
            DataTable ldt_AuxCuentas = lds_AuxCuentas.Tables["Table"];

            DataSet lds_Auxiliares = ws_SGService.uwsConsultarCuentasContablesTipo(ddlClaseCuentas.SelectedValue);
            DataTable ldt_Auxiliares = lds_Auxiliares.Tables["Table"];

            DataSet lds_Instituciones = ws_SGService.uwsConsultarSociedadesGL("", "", "", "", "");
            DataTable ldt_Instituciones = lds_Instituciones.Tables["Table"];

            if (ldt_Auxiliares.Rows.Count > 0)
            {
                DataRow ldr_nuevaFila = ldt_Auxiliares.NewRow();
                DataRow ldr_primeraFila = ldt_Auxiliares.NewRow();

                ldr_primeraFila["NomCuentaContable"] = "";
                ldr_primeraFila["IdCuentaContable"] = "";
                ldr_nuevaFila["NomCuentaContable"] = "Todas";
                ldr_nuevaFila["IdCuentaContable"] = "";

                ldt_Auxiliares.Rows.InsertAt(ldr_primeraFila, 0);
                ldt_Auxiliares.Rows.InsertAt(ldr_nuevaFila, 1);

                ddlAuxCuentas.DataSource = ldt_Auxiliares;
                ddlAuxCuentas.DataTextField = "NomCuentaContable";
                ddlAuxCuentas.DataValueField = "IdCuentaContable";
                ddlAuxCuentas.DataBind();
            }

            if (ldt_Instituciones.Rows.Count > 0)
            {
                DataRow ldr_nuevaFilaIns = ldt_Instituciones.NewRow();
                DataRow ldr_primeraFilaIns = ldt_Instituciones.NewRow();

                ldr_primeraFilaIns["NomSociedad"] = "";
                ldr_primeraFilaIns["IdSociedadGL"] = "";
                ldr_nuevaFilaIns["NomSociedad"] = "Todas";
                ldr_nuevaFilaIns["IdSociedadGL"] = "";

                ldt_Instituciones.Rows.InsertAt(ldr_primeraFilaIns, 0);
                ldt_Instituciones.Rows.InsertAt(ldr_nuevaFilaIns, 1);

                ddlInstitucion.DataSource = ldt_Instituciones;
                ddlInstitucion.DataTextField = "NomSociedad";
                ddlInstitucion.DataValueField = "IdSociedadGL";
                ddlInstitucion.DataBind();
            }

            //VARO 09-03-2020
            ddlInstitucion.SelectedValue = clsSesion.Current.SociedadUsr;
            ddlInstitucion.Enabled = false;
            int i = 0;
            while (i < clsSesion.Current.RolesUsuario.Count())
            {
                if (clsSesion.Current.RolesUsuario[i].ToString() == "36" || clsSesion.Current.RolesUsuario[i].ToString() == "24")
                {
                    ddlInstitucion.SelectedValue = clsSesion.Current.SociedadUsr;
                    ddlInstitucion.Enabled = true;
                }
                i = i + 1;
            }
        }

        private void ConsultaRevelacionPendientes(string str_IdRevelacionPendiente,string str_PerMensual, string str_ClaseCuenta,
            string str_Auxiliar, string str_Institucion, string str_Annio)
        {
            str_Usuario = clsSesion.Current.LoginUsuario;

            List<string> ParametrosConsulta = new List<string>() {str_IdRevelacionPendiente,str_PerMensual,str_ClaseCuenta,
                str_Auxiliar, str_Institucion, str_Annio };
            DatosConsulta = ParametrosConsulta;
            DataSet lds_Revelaciones = new DataSet();
            //lds_Revelaciones = ws_SGService.uwsConsultarRevelacionPendiente(str_IdRevelacionPendiente, str_PerMensual,
            //    str_ClaseCuenta, str_Auxiliar, str_Usuario, str_Institucion);
            if (clsSesion.Current.RolesUsuario.Contains("29") || clsSesion.Current.RolesUsuario.Contains("36"))
            {
                lds_Revelaciones = ws_SGService.uwsConsultarRevelacionPendiente(str_IdRevelacionPendiente, str_PerMensual,
                str_ClaseCuenta, str_Auxiliar, "", str_Institucion, str_Annio);
            }
            else
            {
                lds_Revelaciones = ws_SGService.uwsConsultarRevelacionPendiente(str_IdRevelacionPendiente, str_PerMensual,
                str_ClaseCuenta, str_Auxiliar, str_Usuario, str_Institucion, str_Annio);
            }
            if (lds_Revelaciones.Tables["Table"].Rows.Count > 0)
            {
                lblSinResultados.Visible = false;
                gvFormularios.DataSource = lds_Revelaciones.Tables["Table"];
                gvFormularios.DataBind();
                gvFormularios.Visible = true;
            }
            else
            {
                lblSinResultados.Visible = true;
                gvFormularios.Visible = false;
            }           

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            ConsultaRevelacionPendientes(txtNumero.Text.Trim(), ddlPeriodoMensual.SelectedValue,
                ddlClaseCuentas.SelectedValue, ddlAuxCuentas.SelectedValue,ddlInstitucion.SelectedValue,txtAnnios.Text.Trim());
        }

        protected void gvFormularios_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvFormularios.SelectedIndex = e.NewEditIndex;
            Label lblIdRevelacionPendiente = (Label)gvFormularios.Rows[e.NewEditIndex].Cells[0].FindControl("lblIdRevelacionPendiente");
            string lstr_IdRevelacionPendiente = lblIdRevelacionPendiente.Text;
            string lstr_Url = String.Format("~/RevelacionNotas/CapturaInformacion.aspx?Rev={0}&Tipo={1}", lstr_IdRevelacionPendiente,"Pendiente");
            Response.Redirect(lstr_Url, false);        
        }

        protected void gvFormularios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "consulta")
            {
                string index = Convert.ToString(e.CommandArgument.ToString());
                string lstr_Url = String.Format("~/RevelacionNotas/ConsultaPendiente.aspx?Rev={0}", index);
                Response.Redirect(lstr_Url, false);
            }           
        }

        protected void gvFormularios_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            gvFormularios.PageIndex = e.NewPageIndex;
            ConsultaRevelacionPendientes(DatosConsulta.ElementAt(0), DatosConsulta.ElementAt(1), DatosConsulta.ElementAt(2),
                DatosConsulta.ElementAt(3), DatosConsulta.ElementAt(4), DatosConsulta.ElementAt(5));
        }

        protected void ddlClaseCuentas_SelectedIndexChanged1(object sender, EventArgs e)
        {
            DataSet lds_Auxiliares = ws_SGService.uwsConsultarCuentasContablesTipo(ddlClaseCuentas.SelectedValue);
            DataTable ldt_Auxiliares = lds_Auxiliares.Tables["Table"];

            if (ldt_Auxiliares.Rows.Count > 0)
            {
                DataRow ldr_nuevaFila = ldt_Auxiliares.NewRow();
                DataRow ldr_primeraFila = ldt_Auxiliares.NewRow();

                ldr_primeraFila["NomCuentaContable"] = "";
                ldr_primeraFila["IdCuentaContable"] = "";
                ldr_nuevaFila["NomCuentaContable"] = "Todas";
                ldr_nuevaFila["IdCuentaContable"] = "";

                ldt_Auxiliares.Rows.InsertAt(ldr_primeraFila, 0);
                ldt_Auxiliares.Rows.InsertAt(ldr_nuevaFila, 1);

                ddlAuxCuentas.DataSource = ldt_Auxiliares;
                ddlAuxCuentas.DataTextField = "NomCuentaContable";
                ddlAuxCuentas.DataValueField = "IdCuentaContable";
                ddlAuxCuentas.DataBind();
            }           
        }

        protected void btnNuevaRev_Click(object sender, EventArgs e)
        {
            string lstr_Url = String.Format("~/RevelacionNotas/CapturaInformacion.aspx?Tipo={0}", "Pendiente");
            Response.Redirect(lstr_Url);            
        }
    }
}