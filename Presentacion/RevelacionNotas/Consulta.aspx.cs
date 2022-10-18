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
    public partial class Consulta : BASE
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
                            ConsultarRevelacion(lst_IdRevelacion);
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


        private void ConsultarRevelacion(string str_IdRevelacion)
        {
           
            DateTime ldt_Fecha = DateTime.Now;
            DataSet lds_Revelacion = ws_SGService.uwsConsultarFormulario(str_IdRevelacion);
            if (lds_Revelacion != null)
            {
                DataTable ldt_Revelacion = lds_Revelacion.Tables["Table"];
                if (ldt_Revelacion.Rows.Count > 0)
                {
                    clsSesion.Current.FechaUltimaConsulta = ldt_Revelacion.Rows[0]["FchModifica"].ToString();
                    lblConsecutivo.Text = str_IdRevelacion;
                    lblConceptoConsulta.Text = ldt_Revelacion.Rows[0]["Concepto"].ToString();
                    lblJustificacionConsulta.Text = ldt_Revelacion.Rows[0]["Justificacion"].ToString();
                    lblEntidadCons.Text = ldt_Revelacion.Rows[0]["NomEntidadCP"].ToString();
                    lblInsCons.Text = ldt_Revelacion.Rows[0]["NomSociedad"].ToString();
                    lblOficinaCons.Text = ldt_Revelacion.Rows[0]["NomOficina"].ToString();
                    lblClaseCuentasCons.Text = ldt_Revelacion.Rows[0]["ClaseCuentas"].ToString();
                    lblGrupoCons.Text = ldt_Revelacion.Rows[0]["NomGrupoCuenta"].ToString();
                    lblAuxCuentasConsulta.Text = ldt_Revelacion.Rows[0]["NomCuentaContable"].ToString();
                    lblPAnualCons.Text = ldt_Revelacion.Rows[0]["PeriodoAnual"].ToString();
                    lblPMesCons.Text = ldt_Revelacion.Rows[0]["PeriodoMensual"].ToString();
//                        lblEstado.Text = ldt_Revelacion.Rows[0]["EstadoRevelacion"].ToString();
                    hdnFecha.Value = ldt_Revelacion.Rows[0]["UltimoDiaModificacion"].ToString();
                    //calFechaLimiteEdicion.SelectedDate = Convert.ToDateTime(ldt_Revelacion.Rows[0]["UltimoDiaModificacion"].ToString());
                    //calFechaLimiteEdicion.Enabled = false;
                    //txtFecha.TodaysDate = Convert.ToDateTime(ldt_Revelacion.Rows[0]["UltimoDiaModificacion"].ToString());
                    //txtFecha.SelectedDate = txtFecha.TodaysDate;
                    txtFecha.Text = ldt_Revelacion.Rows[0]["UltimoDiaModificacion"].ToString();
                    txtFecha.Enabled = false;
                }
            }
                
            DataSet fileList = ws_SGService.uwsObtenerArchivoPorIdRvelacion(str_IdRevelacion);
            if (fileList.Tables.Count > 0)
            {
                gvFiles.DataSource = fileList;
                gvFiles.DataBind();
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/RevelacionNotas/Formularios.aspx"); 
        }

        protected void btnHabilitar_Click(object sender, EventArgs e)
        {
            lblMensaje.Visible = false;

            if (btnHabilitar.Text == "Habilitar cambios")
            {
                btnCambiarFecha.Visible = true;
                //calFechaLimiteEdicion.Enabled = true;
                txtFecha.Enabled = true;
                btnHabilitar.Text = "Cancelar";
            }
            else
            {
                btnCambiarFecha.Visible = false;
                btnHabilitar.Text = "Habilitar cambios";
               //calFechaLimiteEdicion.SelectedDate = Convert.ToDateTime(hdnFecha.Value);
               //calFechaLimiteEdicion.Enabled = false;
                //txtFecha.SelectedDate = Convert.ToDateTime(hdnFecha.Value);
                txtFecha.Text = hdnFecha.Value;
                txtFecha.Enabled = false;

            }


        }

        protected void btnCambiarFecha_Click(object sender, EventArgs e)
        {
            string lstr_ResHabilitacion = String.Empty;
            
            string lstr_IdRev = Request.QueryString["Rev"];
            string str_SQL = "select FchModifica from rn.Revelaciones where IdRevelacion = " + lstr_IdRev;
            DataSet ds_SQL =ws_SGService.uwsConsultarDinamico(str_SQL);
            DateTime lstr_FechaModificado = Convert.ToDateTime( ds_SQL.Tables[0].Rows[0]["FchModifica"]); //Convert.ToDateTime(clsSesion.Current.FechaUltimaConsulta);
            string lstr_Fecha = lstr_FechaModificado.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

            //string lstr_FechaLimite = calFechaLimiteEdicion.SelectedDate.AddDays(1).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            //lstr_ResHabilitacion = ws_SGService.uwsAutorizarCambiosRevelacion(lstr_IdRev, calFechaLimiteEdicion.SelectedDate.AddDays(1), lstr_FechaModificado, str_Usuario);//clsSesion.Current.LoginUsuario);
            DateTime fechalim = Convert.ToDateTime(txtFecha.Text.ToString());//,CultureInfo.InvariantCulture);
            //DateTime fechalim = Convert.ToDateTime(txtFecha.SelectedDate, CultureInfo.InvariantCulture);
            string lstr_FechaLimite = fechalim.AddDays(1).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            lstr_ResHabilitacion = ws_SGService.uwsAutorizarCambiosRevelacion(lstr_IdRev, fechalim.AddDays(1), lstr_FechaModificado, str_Usuario);//clsSesion.Current.LoginUsuario);
            switch (lstr_ResHabilitacion)
            {
                case "00":
                    {
                        lblMensaje.Text = "Fecha Actualizada";
                        lblMensaje.Visible = true;
                        hdnFecha.Value = lstr_FechaLimite;
                        btnCambiarFecha.Visible = false;
                        btnHabilitar.Text = "Habilitar cambios";
                        //calFechaLimiteEdicion.Enabled = false;
                        txtFecha.Enabled = false;
                        DataSet lds_Revelacion = ws_SGService.uwsConsultarFormulario(lstr_IdRev);
                        if (lds_Revelacion != null)
                        {
                            DataTable ldt_Revelacion = lds_Revelacion.Tables["Table"];
                            if (ldt_Revelacion.Rows.Count > 0)
                            {
                                clsSesion.Current.FechaUltimaConsulta = ldt_Revelacion.Rows[0]["FchModifica"].ToString();
                            }
                        }
                        break;
                    }
                case "-1":
                    {
                        lblMensaje.Text = "Error -1" + lstr_FechaModificado.ToString() + "---" + clsSesion.Current.FechaUltimaConsulta;
                        lblMensaje.Visible = true;
                        break;
                    }
                case "-2":
                    {
                        lblMensaje.Text = "Error -2" + lstr_ResHabilitacion;
                        lblMensaje.Visible = true;
                        break;
                    }
                default:
                    {
                        lblMensaje.Text = "Error " + lstr_ResHabilitacion;
                        lblMensaje.Visible = true;
                        break;
                    }
            }
           
        }

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

        protected void hdnFecha_ValueChanged(object sender, EventArgs e)
        {

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