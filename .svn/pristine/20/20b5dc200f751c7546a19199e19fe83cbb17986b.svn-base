using Presentacion.Compartidas;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Presentacion.RevelacionNotas
{

    public partial class FormulariosDeuda : BASE
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

        private static Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private string str_Usuario = String.Empty;
        private string str_Categoria = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {  
            str_Usuario = clsSesion.Current.LoginUsuario;
            hdnAux.Value = "";

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(str_Usuario))
                {
                   if (Request.QueryString["Cat"] != null)
                        str_Categoria = Request.QueryString["Cat"];


                    if (!clsSeguridadVistas.MostrarElementos(str_Usuario, Master, "FormulariosDeuda"))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                    {
                        MostrarElementos(str_Usuario);
                        //CargarDDLs();
                        ConsultarFormularios("", "", "", str_Categoria);
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
                            try
                            {
                                WebControl hgcMenuEncabezado = (WebControl)FindControlRecursive(Master.Page, lstr_IdliEncabezado);

                                if (hgcMenuEncabezado != null)
                                    hgcMenuEncabezado.Visible = true;
                            }
                            catch { }
                        }
                    }

                    if (lstr_IdObjeto.Equals("gvFormularios") && lbool_Actualizar)
                    {
                        gvFormularios.Columns[9].Visible = true;
                    }
                }
          
        }

        public void btnNuevoForm_OnClick(object sender, EventArgs e)
        {   
            string lstr_Url = String.Format("~/RevelacionNotas/CapturaInformacion.aspx?Tipo={0}", "Actual");
            Response.Redirect(lstr_Url);
        }

        //private void CargarDDLs()
        //{

        //    string str_PlanCuentas = ConfigurationManager.AppSettings["PlanCuentas"];
        //    DataSet lds_AuxCuentas = ws_SGService.uwsConsultarCuentasContables("", str_PlanCuentas, "", "", "", "", "");
        //    DataTable ldt_AuxCuentas = lds_AuxCuentas.Tables["Table"];

        //    DataSet lds_Auxiliares = ws_SGService.uwsConsultarCuentasContablesTipo(ddlClaseCuentas.SelectedValue);
        //    DataTable ldt_Auxiliares = lds_Auxiliares.Tables["Table"];

        //    DataSet lds_Instituciones = ws_SGService.uwsConsultarSociedadesGL("", "", "", "", "");
        //    DataTable ldt_Instituciones = lds_Instituciones.Tables["Table"];

        //    if (ldt_Auxiliares.Rows.Count > 0)
        //    {
        //        DataRow ldr_nuevaFila = ldt_Auxiliares.NewRow();
        //        DataRow ldr_primeraFila = ldt_Auxiliares.NewRow();

        //        ldr_primeraFila["NomCuentaContable"] = "";
        //        ldr_primeraFila["IdCuentaContable"] = "";
        //        ldr_nuevaFila["NomCuentaContable"] = "Todas";
        //        ldr_nuevaFila["IdCuentaContable"] = "";

        //        ldt_Auxiliares.Rows.InsertAt(ldr_primeraFila, 0);
        //        ldt_Auxiliares.Rows.InsertAt(ldr_nuevaFila, 1);

        //        ddlAuxCuentas.DataSource = ldt_Auxiliares;
        //        ddlAuxCuentas.DataTextField = "NomCuentaContable";
        //        ddlAuxCuentas.DataValueField = "IdCuentaContable";
        //        ddlAuxCuentas.DataBind();
        //    }

        //    if (ldt_Instituciones.Rows.Count > 0)
        //    {
        //        DataRow ldr_nuevaFilaIns = ldt_Instituciones.NewRow();
        //        DataRow ldr_primeraFilaIns = ldt_Instituciones.NewRow();

        //        ldr_primeraFilaIns["NomSociedad"] = "";
        //        ldr_primeraFilaIns["IdSociedadGL"] = "";
        //        ldr_nuevaFilaIns["NomSociedad"] = "Todas";
        //        ldr_nuevaFilaIns["IdSociedadGL"] = "";

        //        ldt_Instituciones.Rows.InsertAt(ldr_primeraFilaIns, 0);
        //        ldt_Instituciones.Rows.InsertAt(ldr_nuevaFilaIns, 1);

        //        ddlInstitucion.DataSource = ldt_Instituciones;
        //        ddlInstitucion.DataTextField = "NomSociedad";
        //        ddlInstitucion.DataValueField = "IdSociedadGL";
        //        ddlInstitucion.DataBind();
        //    }

        //}

        private void ConsultarFormularios(string lstr_Consecutivo, string lstr_Anno, string lstr_Mes, string lstr_Criterio)
        {
           DataTable lds_Datos = new DataTable();
            str_Usuario = clsSesion.Current.LoginUsuario;
            //List<int> ParametrosConsulta = new List<int>() { lint_Consecutivo };
            //DatosConsulta = ParametrosConsulta;
            DataSet lds_Revelaciones = new DataSet();
            if (clsSesion.Current.RolesUsuario.Contains("79") || clsSesion.Current.RolesUsuario.Contains("80"))
                lds_Revelaciones = ws_SGService.uwsConsultarArchivosDeuda(lstr_Consecutivo, lstr_Anno, lstr_Mes, lstr_Criterio);
            else
                lds_Revelaciones = ws_SGService.uwsConsultarArchivosDeuda(lstr_Consecutivo, lstr_Anno, lstr_Mes, lstr_Criterio);
            
            
            if (lds_Revelaciones.Tables["Table"].Rows.Count > 0)
            {
                lblSinResultados.Visible = false;

                 lds_Datos.Columns.Add("IdArchivoDeuda");
                 lds_Datos.Columns.Add("NomModulo");
                 lds_Datos.Columns.Add("NomOpcion");
                 lds_Datos.Columns.Add("FchCreacion");
                 lds_Datos.Columns.Add("UsrCreacion");
                 lds_Datos.Columns.Add("Anno");
                 lds_Datos.Columns.Add("Mes");
                 lds_Datos.Columns.Add("FchModifica");
                
                foreach(DataRow drRevelaciones in lds_Revelaciones.Tables[0].Rows)
                {
                    lds_Datos.Rows.Add(
                        drRevelaciones["IdArchivoDeuda"].ToString(),
                        drRevelaciones["NomModulo"].ToString(),
                        drRevelaciones["NomOpcion"].ToString(),
                        Convert.ToDateTime(drRevelaciones["FchCreacion"].ToString()).ToString("dd/MM/yyyy hh:mm:ss"),
                        ConsultaUsuario(drRevelaciones["UsrCreacion"].ToString()),
                        drRevelaciones["Anno"].ToString(),
                        drRevelaciones["Mes"].ToString(),
                        drRevelaciones["FchModifica"].ToString());
                
                }


                gvFormularios.DataSource = lds_Datos;
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
            ConsultarFormularios(this.txtNumero.Text, this.txtAnno.Text, this.txtMes.Text, str_Categoria);           
        }


        [WebMethod]

        public static object ConsultarAuxiliares(string str_GrupoCuentas) //string str_GrupoCuentas
        {
            var objList = (new[] { new { Text = "", Value = "" } }).ToList();
           
            DataSet lds_Auxiliares = ws_SGService.uwsConsultarCuentasContablesTipo(str_GrupoCuentas);
            DataTable ldt_Auxiliares = lds_Auxiliares.Tables["Table"];
            if (ldt_Auxiliares.Rows.Count > 0)
            {

                for (int i = 0; i < ldt_Auxiliares.Rows.Count; i++)
                {
                    string lstr_Opcion = Convert.ToString(ldt_Auxiliares.Rows[i]["NomCuentaContable"]);
                    string lstr_Valor = Convert.ToString(ldt_Auxiliares.Rows[i]["IdCuentaContable"]);
                    objList.Add(new { Text = lstr_Opcion, Value = lstr_Valor });
                }
            }
            return objList;
        }

        //protected void ddlClaseCuentas_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string str_PlanCuentas = ddlClaseCuentas.SelectedValue;
        //}


        protected void gvFormularios_RowEditing(object sender, GridViewEditEventArgs e)
        {           
            gvFormularios.SelectedIndex = e.NewEditIndex;
            Label lblIdRevelacion = (Label)gvFormularios.Rows[e.NewEditIndex].Cells[0].FindControl("lblIdRevelacion");
            string lstr_IdRevelacion = lblIdRevelacion.Text;
            string lstr_Url = String.Format("~/RevelacionNotas/CapturaInformacionDeuda.aspx?Rev={0}&Tipo={1}", lstr_IdRevelacion, "Normal");
            Response.Redirect(lstr_Url, false);
        }

        protected void gvFormularios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "consulta")
            {
                string index = Convert.ToString(e.CommandArgument.ToString());
                string lstr_Url = String.Format("~/RevelacionNotas/ConsultaDeuda.aspx?Rev={0}", index);
                Response.Redirect(lstr_Url, false);
            }
        }


        //protected void ddlClaseCuentas_SelectedIndexChanged1(object sender, EventArgs e)
        //{
        //    DataSet lds_Auxiliares = ws_SGService.uwsConsultarCuentasContablesTipo(ddlClaseCuentas.SelectedValue);
        //    DataTable ldt_Auxiliares = lds_Auxiliares.Tables["Table"];

        //    if (ldt_Auxiliares.Rows.Count > 0)
        //    {
        //        DataRow ldr_nuevaFila = ldt_Auxiliares.NewRow();
        //        DataRow ldr_primeraFila = ldt_Auxiliares.NewRow();

        //        ldr_primeraFila["NomCuentaContable"] = "";
        //        ldr_primeraFila["IdCuentaContable"] = "";
        //        ldr_nuevaFila["NomCuentaContable"] = "Todas";
        //        ldr_nuevaFila["IdCuentaContable"] = "";

        //        ldt_Auxiliares.Rows.InsertAt(ldr_primeraFila, 0);
        //        ldt_Auxiliares.Rows.InsertAt(ldr_nuevaFila, 1);

        //        ddlAuxCuentas.DataSource = ldt_Auxiliares;
        //        ddlAuxCuentas.DataTextField = "NomCuentaContable";
        //        ddlAuxCuentas.DataValueField = "IdCuentaContable";
        //        ddlAuxCuentas.DataBind();
        //    }
        //}

        protected void gvFormularios_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            gvFormularios.PageIndex = e.NewPageIndex;
            ConsultarFormularios(this.txtNumero.Text, this.txtAnno.Text, this.txtMes.Text, str_Categoria);         
            //   ConsultarFormularios(DatosConsulta.ElementAt(0)/*, DatosConsulta.ElementAt(1), DatosConsulta.ElementAt(2),
          //      DatosConsulta.ElementAt(3), DatosConsulta.ElementAt(4)*/);
        }

        protected void gvFormularios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Label lblUltimoDiaModificacion = (Label)e.Row.FindControl("lblUltimoDiaModificacion");
            LinkButton lnkEditar = (LinkButton)e.Row.FindControl("lnkEditar");
            Image mgEditar = (Image)e.Row.FindControl("mgEditar");
            if (lblUltimoDiaModificacion != null)
            {
                DateTime ldt_FechaLimite = Convert.ToDateTime(lblUltimoDiaModificacion.Text);
                if (ldt_FechaLimite <= DateTime.Today)
                {
                    lnkEditar.Visible = false;
                    mgEditar.Visible = false;
                }
                else
                {
                    lnkEditar.Visible = true;
                    mgEditar.Visible = true;
                }
            }
        }

        private string ConsultaUsuario(string str_IdUsuario) 
        {
            string lstr_nombre = string.Empty;
            DataSet lds_Usuarios = ws_SGService.uwsConsultarUsuarios(str_IdUsuario, string.Empty, string.Empty);

            return lstr_nombre = lds_Usuarios.Tables[0].Rows[0]["NomUsuario"].ToString();
        }
    }
}