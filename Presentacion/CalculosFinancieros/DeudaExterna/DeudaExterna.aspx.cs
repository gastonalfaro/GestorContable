using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.CalculosFinancieros.DeudaExterna;

namespace Presentacion.CalculosFinancieros.DeudaExterna
{
    public partial class DeudaExterna : BASE
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            clsCalculosDeudaExterna cls = new clsCalculosDeudaExterna();            
            GridView1.DataSource = cls.ActualizaTipoCambio("jgomez");
            GridView1.DataBind();
        }
    }
}