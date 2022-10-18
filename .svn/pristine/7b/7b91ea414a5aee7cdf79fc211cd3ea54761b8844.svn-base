using Logica.SubirArchivo;
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

namespace Presentacion.RevelacionNotas
{
    public partial class ConsultaDeuda : BASE
    {
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private string lst_IdRevelacion = String.Empty;
        private string str_Usuario = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {           
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
                        if (Request.QueryString["Rev"] != null)
                        {
                            lst_IdRevelacion = Request.QueryString["Rev"];
                            ConsultarNotaDeuda(lst_IdRevelacion, "","","");
                        }
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
            }
        }


        private void ConsultarNotaDeuda(string lstr_IdArchivoDeuda, string lstr_Anno, string lstr_Mes, string lstr_Criterio)
        {
           
            DateTime ldt_Fecha = DateTime.Now;
            DataSet lds_Revelacion = ws_SGService.uwsConsultarArchivosDeuda(lstr_IdArchivoDeuda, lstr_Anno, lstr_Mes, lstr_Criterio);
            if (lds_Revelacion != null)
            {
                DataTable ldt_Revelacion = lds_Revelacion.Tables["Table"];
                if (ldt_Revelacion.Rows.Count > 0)
                {
                    clsSesion.Current.FechaUltimaConsulta = ldt_Revelacion.Rows[0]["FchModifica"].ToString();
                    lblConsecutivo.Text = lstr_IdArchivoDeuda.ToString();
                    lblTipoDeuda.Text = ldt_Revelacion.Rows[0]["NomModulo"].ToString();
                    lblCategorias.Text = ldt_Revelacion.Rows[0]["NomOpcion"].ToString();
                    lblPeriodoMensual.Text = ldt_Revelacion.Rows[0]["Mes"].ToString();
                    lblPeriodoAnual.Text = ldt_Revelacion.Rows[0]["Anno"].ToString();
                    //lblOficinaCons.Text = ldt_Revelacion.Rows[0]["NomOficina"].ToString();
                    //lblClaseCuentasCons.Text = ldt_Revelacion.Rows[0]["ClaseCuentas"].ToString();
                    //lblGrupoCons.Text = ldt_Revelacion.Rows[0]["NomGrupoCuenta"].ToString();
                    //lblAuxCuentasConsulta.Text = ldt_Revelacion.Rows[0]["NomCuentaContable"].ToString();
                    //lblPAnualCons.Text = ldt_Revelacion.Rows[0]["PeriodoAnual"].ToString();
                    //lblPMesCons.Text = ldt_Revelacion.Rows[0]["PeriodoMensual"].ToString();
//                        lblEstado.Text = ldt_Revelacion.Rows[0]["EstadoRevelacion"].ToString();
                    hdnFecha.Value = ldt_Revelacion.Rows[0]["FchModifica"].ToString();
                    this.txtFechaLimiteEdicion.Text = ldt_Revelacion.Rows[0]["FchModifica"].ToString();
                    this.txtFechaLimiteEdicion.Enabled = false;
                }
            }

            DataSet fileList = ws_SGService.uwsObtenerArchivoPorIdArchivoDeuda(lstr_IdArchivoDeuda);
            if (fileList.Tables.Count > 0)
            {
                gvFiles.DataSource = fileList;
                gvFiles.DataBind();
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/RevelacionNotas/FormulariosDeuda.aspx"); 
        }

        protected void btnHabilitar_Click(object sender, EventArgs e)
        {
            lblMensaje.Visible = false;

            if (btnHabilitar.Text == "Habilitar cambios")
            {
                //btnCambiarFecha.Visible = true;
                txtFechaLimiteEdicion.Enabled = true;
                btnHabilitar.Text = "Cancelar";
            }
            else
            {
                //btnCambiarFecha.Visible = false;
                btnHabilitar.Text = "Habilitar cambios";
                txtFechaLimiteEdicion.Text = hdnFecha.Value;
                txtFechaLimiteEdicion.Enabled = false;

            }


        }

        //protected void btnCambiarFecha_Click(object sender, EventArgs e)
        //{
        //    string lstr_ResHabilitacion = String.Empty;
            
        //    string lstr_IdRev = Request.QueryString["Rev"];
        //    DateTime lstr_FechaModificado = Convert.ToDateTime(clsSesion.Current.FechaUltimaConsulta);
        //    string lstr_Fecha = lstr_FechaModificado.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        //    string lstr_FechaLimite = txtFechaLimiteEdicion.SelectedDate.AddDays(1).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        //    lstr_ResHabilitacion = ws_SGService.uwsAutorizarCambiosRevelacion(lstr_IdRev, lstr_FechaLimite, lstr_Fecha, str_Usuario);//clsSesion.Current.LoginUsuario);
        //    switch (lstr_ResHabilitacion)
        //    {
        //        case "00":
        //            {
        //                lblMensaje.Text = "Fecha Actualizada";
        //                lblMensaje.Visible = true;
        //                hdnFecha.Value = lstr_FechaLimite;
        //                btnCambiarFecha.Visible = false;
        //                btnHabilitar.Text = "Habilitar cambios";
        //                txtFechaLimiteEdicion.Enabled = false;
        //                DataSet lds_Revelacion = ws_SGService.uwsConsultarFormulario(lstr_IdRev);
        //                if (lds_Revelacion != null)
        //                {
        //                    DataTable ldt_Revelacion = lds_Revelacion.Tables["Table"];
        //                    if (ldt_Revelacion.Rows.Count > 0)
        //                    {
        //                        clsSesion.Current.FechaUltimaConsulta = ldt_Revelacion.Rows[0]["FchModifica"].ToString();
        //                    }
        //                }
        //                break;
        //            }
        //        case "-1":
        //            {

        //                break;
        //            }
        //        case "-2":
        //            {
        //                break;
        //            }
        //        default:
        //            {
        //                break;
        //            }
        //    }
           
        //}

        protected void gvFiles_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "consulta")
            {
                string index = Convert.ToString(e.CommandArgument.ToString());
                clsArchivoSubir utilidad = new clsArchivoSubir();
                // Get the file from the database
                DataSet file = utilidad.ufnObtenerArchivoPorId(index);
                DataRow row = file.Tables[0].Rows[0];

                string name = (string)row["NombreArchivo"];
                string contentType = (string)row["TipoContenido"];
                Byte[] data = (Byte[])row["Dato"];

                // Send the file to the browser
                Response.AddHeader("Content-type", contentType);
                Response.AddHeader("Content-Disposition", "attachment; filename=" + name);
                Response.BinaryWrite(data);
                Response.Flush();
                Response.End();
            }
        }

        protected void btnImprimir_Click(object sender, EventArgs e)
        {  
            string lstr_IdRev = Request.QueryString["Rev"];
            string lstr_Url = String.Format("~/RevelacionNotas/ImpresionFormulario.aspx?Rev={0}", lstr_IdRev);
            Response.Redirect(lstr_Url);           
        }

        //protected void Button1_Click(object sender, EventArgs e)
        //{
        //    // Get the file id from the query string
        //    string id = "17";// Request.QueryString["17"];
        //    clsArchivoSubir utilidad = new clsArchivoSubir();
        //    // Get the file from the database
        //    DataSet file = utilidad.ufnObtenerArchivoPorId(id);
        //    DataRow row = file.Tables[0].Rows[0];

        //    string name = (string)row["NombreArchivo"];
        //    string contentType = (string)row["TipoContenido"];
        //    Byte[] data = (Byte[])row["Dato"];

        //    // Send the file to the browser
        //    Response.AddHeader("Content-type", contentType);
        //    Response.AddHeader("Content-Disposition", "attachment; filename=" + name);
        //    Response.BinaryWrite(data);
        //    Response.Flush();
        //    Response.End();
        //}
    }
}