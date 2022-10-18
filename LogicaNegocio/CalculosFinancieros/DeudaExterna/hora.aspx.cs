using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;

namespace LogicaNegocio.CalculosFinancieros.DeudaExterna
{
    public partial class hora : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            clsCalculosDeudaExterna cls = new clsCalculosDeudaExterna();
            string salida = "";
            try
            {
                DateTime ldt_FechaActual = DateTime.ParseExact(fch.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                mntd.Text = cls.ConvertirdorDeMoneda(ind.Text, ldt_FechaActual, Convert.ToDecimal(mnt.Text), out salida).ToString();
            }
            catch (Exception ex)
            {
                salida = ex.ToString();
            }
            Label1.Text = salida;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            double[] arreglo = new double[7];
            arreglo[0] = 25000;
            arreglo[1] = -13500;
            arreglo[2] = -1650;
            arreglo[3] = -10000;
            arreglo[4] = 600;
            arreglo[5] = 200;
            arreglo[6] = -8800;

            clsCalculosDeudaExterna cls = new clsCalculosDeudaExterna();
            irr.Text = cls.CalculoTIR(arreglo).ToString();
        }
    }
}