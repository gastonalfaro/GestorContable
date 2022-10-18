using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Presentacion.Compartidas;
using System.Data;

namespace Presentacion.Seguridad.GestionUsuarios
{
    public partial class NuevoUsuario : BASE
    {
        private string str_Usuario = String.Empty;
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        
        protected void Page_Load(object sender, EventArgs e)
        {

            str_Usuario = clsSesion.Current.LoginUsuario;

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(str_Usuario))
                {
                    if (!clsSeguridadVistas.MostrarElementos(str_Usuario, Master, "OBJ_SG"))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                    {
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
        }

        /// <summary>
        /// Evento que dispara al oprimir el boton de Atras
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Seguridad/Usuarios.aspx"); 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCrearUsuario_Click(object sender, EventArgs e)
        {
                //Boolean lboo_ResCreacion = ws_SGService.
        }

        private void CargarDDLs()
        {
            ddlSociedad.Items.Clear();
            DataSet lds_Sociedades = ws_SGService.uwsConsultarSociedadesGL("", "", "", "", "");
            //la consulta generó resultados
            if (lds_Sociedades.Tables.Count > 0 && lds_Sociedades.Tables[0].Rows.Count > 0)
            {
                DataTable ldt_Sociedades = lds_Sociedades.Tables["Table"];
                DataRow ldr_nuevaFila = ldt_Sociedades.NewRow();
                ldr_nuevaFila["NomSociedad"] = "Ciudadano"; //["Denominacion"]
                ldr_nuevaFila["IdSociedadGL"] = "0";
                ldt_Sociedades.Rows.InsertAt(ldr_nuevaFila, 0);
                ddlSociedad.DataSource = ldt_Sociedades;
                ddlSociedad.DataTextField = "Denominacion";
                ddlSociedad.DataValueField = "IdSociedadGL";
                ddlSociedad.DataBind();
            }//if
        }

    }
}