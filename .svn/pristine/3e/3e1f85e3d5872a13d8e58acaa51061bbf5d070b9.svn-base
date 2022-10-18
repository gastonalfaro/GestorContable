using Logica.SubirArchivo;
using Presentacion.Compartidas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.RevelacionNotas
{
    public partial class ObtenerArchivoRev : BASE
    {
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
                        CargarArchivos();
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

        private void CargarArchivos()
        {
            // Get the file id from the query string
            string id = Request.QueryString["IdArchivo"];
            clsArchivoSubir utilidad = new clsArchivoSubir();
            // Get the file from the database
            DataSet file = utilidad.ufnObtenerArchivoPorId(id);
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
}