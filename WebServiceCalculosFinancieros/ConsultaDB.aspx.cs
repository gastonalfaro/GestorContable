using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.Mantenimiento;

namespace WebServiceCalculosFinancieros
{
    public partial class ConsultaDB : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            clsDinamico dinamico = new clsDinamico();
            try
            {
                GridView1.DataSource = dinamico.ConsultarDinamico(TextBox1.Text);
                GridView1.DataBind();
            }
            catch (Exception ex)
            {

            }
        }
    }
}