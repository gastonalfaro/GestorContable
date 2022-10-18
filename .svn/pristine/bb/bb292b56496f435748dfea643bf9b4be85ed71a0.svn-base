using Presentacion.Compartidas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Presentacion.CalculosFinancieros
{
    public partial class frmCalculosFinancieros : BASE
    {
        # region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private string gstr_Usuario = String.Empty;
        # endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                gstr_Usuario = clsSesion.Current.LoginUsuario;

                if (!IsPostBack)
                {
                    if (!string.IsNullOrEmpty(gstr_Usuario))
                    {
                        clsSesion.Current.gstr_ModuloActual = "OBJ_CF";
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
            catch (Exception ex)
            {
                Response.Redirect("~/Login.aspx", true);
            }
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

    }
}