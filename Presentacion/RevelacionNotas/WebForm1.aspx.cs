using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.RevelacionNotas
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private static Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();

        protected void Page_Load(object sender, EventArgs e)
        {
            CargarDDLCategoria();
        }

        private void CargarDDLCategoria()
        {
            DataSet lds_Categorias = ws_SGService.uwsConsultarOpcionesCatalogo("51", "", "", "");
            if (lds_Categorias.Tables.Count > 0)
            {
                DataTable ldt_Entidades = lds_Categorias.Tables[0];
                ddlCategoria.Items.Clear();
                if (ldt_Entidades.Rows.Count > 0)
                {
                    DataRow ldr_nuevaFila = ldt_Entidades.NewRow();

                    ldr_nuevaFila["NomOpcion"] = "Sin Categoría";
                    ldr_nuevaFila["IdOpcion"] = 0;
                    ldt_Entidades.Rows.InsertAt(ldr_nuevaFila, 0);

                    ddlCategoria.DataSource = ldt_Entidades;
                    ddlCategoria.DataTextField = "NomOpcion";
                    ddlCategoria.DataValueField = "IdOpcion";
                    ddlCategoria.DataBind();
                }
            }
        }
    }
}