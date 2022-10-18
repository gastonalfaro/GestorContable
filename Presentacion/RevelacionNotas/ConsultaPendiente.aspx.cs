using Logica.SubirArchivo;
using Presentacion.Compartidas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Presentacion.RevelacionNotas
{
    public partial class ConsultaPendiente : BASE
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
                        if (Request.QueryString["Rev"] != null)
                        {
                            MostrarElementos(str_Usuario);
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
            DataSet lds_Revelacion = ws_SGService.uwsConsultarRevelacionPendiente(str_IdRevelacion,"","","","","","");
            if (lds_Revelacion != null)
            {
                DataTable ldt_Revelacion = lds_Revelacion.Tables["Table"];
                if (ldt_Revelacion.Rows.Count > 0)
                {
                    clsSesion.Current.FechaUltimaConsulta = ldt_Revelacion.Rows[0]["FchModifica"].ToString();
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
                    lblEstado.Text = ldt_Revelacion.Rows[0]["EstadoRevelacion"].ToString();
                    hdnFecha.Value = ldt_Revelacion.Rows[0]["UltimoDiaModifica"].ToString();
                }
            }
                
            DataSet fileList = ws_SGService.uwsObtenerArchivoPorIdRvelacionPendiente(str_IdRevelacion);
            if (fileList.Tables.Count > 0)
            {
                gvFiles.DataSource = fileList;
                gvFiles.DataBind();
            }
             
        }

        protected void btnAutorizar_Click(object sender, EventArgs e)
        {
            string[] lstr_Resultado = new string[2];
            lstr_Resultado[0] = "99";
           
            if (Request.QueryString["Rev"] != null)
            {
                lstr_Resultado = ws_SGService.uwsAutorizarRevelacionPendiente(Request.QueryString["Rev"]);
                if (lstr_Resultado[0] == "00")
                {
                    MessageBox.Show("Se ha aprobado la cración de esta revelación. ");
                    Response.Redirect("~/RevelacionNotas/FormulariosPendientes.aspx"); 
                }
                else
                {
                    MessageBox.Show("Error al autorizar la revelación.");
                }
            }
           
        }

        protected void btnRechazar_Click(object sender, EventArgs e)
        {
            string[] lstr_Resultado = new string[2];
            lstr_Resultado[0] = "99";
            
            if (Request.QueryString["Rev"] != null)
            {
                lstr_Resultado = ws_SGService.uwsEliminarRevelacionPendiente(Request.QueryString["Rev"]);
                if (lstr_Resultado[0] == "00")
                {
                    MessageBox.Show("Se ha rechazado la creación de esta revelación. ");
                    Response.Redirect("~/RevelacionNotas/FormulariosPendientes.aspx");
                }
                else
                {
                    MessageBox.Show("Error al rechazar la revelación.");
                }
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/RevelacionNotas/FormulariosPendientes.aspx"); 
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
            string lstr_Url = String.Format("~/RevelacionNotas/ImpresionRevPendiente.aspx?Rev={0}", lstr_IdRev);
            Response.Redirect(lstr_Url);
        }
    }
}