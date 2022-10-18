using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Collections;
using System.Data;
using System.Web.UI.WebControls;
using LogicaNegocio.Mantenimiento;

namespace LogicaNegocio.CalculosFinancieros.DeudaExterna
{
    public partial class Prestamos : System.Web.UI.Page
    {
        private DataTable dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            lblValor1.Text = "Tipo de Préstamo:";
        }

        //protected void btnBuscar_Click(object sender, EventArgs e)
        //{
        //    clsManejoPrestamos mp = new clsManejoPrestamos();

        //    if (Convert.ToInt32(opcion.SelectedValue) == 7)
        //    {
        //        dt = mp.consultaPrestamoDB(Convert.ToInt32(opcion.SelectedValue), valor1.Text, valor2.Text).Tables[0];
        //    }
        //    else
        //    {
        //        dt = mp.consultaPrestamoDB(Convert.ToInt32(opcion.SelectedValue), valor1.Text, null).Tables[0];
        //    }
        //    try
        //    {
        //        Session["datos"] = dt;
        //        gv.DataSource = dt;
        //        gv.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        txtFuente.Text = "Error: " + ex.ToString();
        //    }
        //}

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            clsPrestamo mp = new clsPrestamo();

            try
            {
                switch (Convert.ToInt32(opcion.SelectedValue))
                {
                    case 1: dt = mp.ConsultarPrestamo(valor1.Text).Tables[0]; break;
                    case 2: dt = mp.ConsultarPrestamo(null, valor1.Text).Tables[0]; break;
                    case 3: dt = mp.ConsultarPrestamo(null, null, valor1.Text).Tables[0]; break;
                    case 4: dt = mp.ConsultarPrestamo(null, null, null, valor1.Text).Tables[0]; break;
                    case 5: dt = mp.ConsultarPrestamo(null, null, null, null, valor1.Text).Tables[0]; break;
                    case 6: dt = mp.ConsultarPrestamo(null, null, null, null, null, valor1.Text, valor2.Text).Tables[0]; break;
                    case 7: dt = mp.ConsultarPrestamo(null, null, null, null, null, null, null, valor1.Text).Tables[0]; break;
                    case 8: dt = mp.ConsultarPrestamo(null, null, null, null, null, null, null, null, valor1.Text).Tables[0]; break;
                    case 9: dt = mp.ConsultarPrestamo(null, null, null, null, null, null, null, null, null, valor1.Text).Tables[0]; break;
                }
                if (dt.Rows.Count == 0)
                {
                    TextBox1.Text = "No se encontraron registros para la búsqueda realizada.";
                }
                else
                {
                    TextBox1.Text = "Se encontraron " + dt.Rows.Count + " registros que concuerdan con su búsqueda.";
                }
                Session["datos"] = dt;
                gv.DataSource = dt;
                gv.DataBind();
            }
            catch (Exception ex)
            {
                txtFuente.Text = "Error: " + ex.ToString();
            }
        }

        protected void gv_RowCommand(Object sender, GridViewCommandEventArgs e)
        {
            dt = Session["datos"] as DataTable;
            string a = "";
            string b = "";
            int indice = Convert.ToInt32(e.CommandArgument.ToString());
            clsPrestamo mp = new clsPrestamo();

            if (e.CommandName == "Editar")
            {                
                txtIdPrestamo.Text = dt.Rows[indice][0].ToString();
                txtFuente.Text = dt.Rows[indice][1].ToString();
                txtSituacion.Text = dt.Rows[indice][2].ToString();
                txtPlazo.Text = dt.Rows[indice][3].ToString();
                txtNombre.Text = dt.Rows[indice][4].ToString();
                txtFechaFirmado.Text = dt.Rows[indice][5].ToString();
                txtLimiteGiro.Text = dt.Rows[indice][6].ToString();
                txtLimiteEfectivo.Text = dt.Rows[indice][7].ToString();
                txtEfectivo.Text = dt.Rows[indice][8].ToString();
                txtMonto.Text = dt.Rows[indice][9].ToString();
                txtIdMoneda.Text = dt.Rows[indice][10].ToString();
                txtTpoTramo.Text = dt.Rows[indice][11].ToString();
                txtProposito.Text = dt.Rows[indice][12].ToString();
                txtGarantiaPublica.Text = dt.Rows[indice][13].ToString();
                txtOrigenDeuda.Text = dt.Rows[indice][14].ToString();
                txtIdAcreedor.Text = dt.Rows[indice][15].ToString();
                txtIdDeudor.Text = dt.Rows[indice][16].ToString();
                txtTpoPrestamo.Text = dt.Rows[indice][17].ToString();
                txtTasa.Text = dt.Rows[indice][18].ToString();
                Session["fechaModificacion"] = dt.Rows[indice][21].ToString();
            }
            if (e.CommandName == "Eliminar")
            {
                txtIdPrestamo.Text = mp.Lstr_TextoError;

            }
        }

        protected void opcion_SelectedIndexChanged1(object sender, EventArgs e)
        {
            switch (Convert.ToInt32(opcion.SelectedValue))
            {
                case 1: lblValor1.Text = "Tipo de Préstamo:"; break;
                case 3: lblValor1.Text = "Situación:"; break;
                case 4: lblValor1.Text = "Plazo:"; break;
                case 5: lblValor1.Text = "Nombre:"; break;
                case 9: lblValor1.Text = "Id de Préstamo:"; break;
                case 2: lblValor1.Text = "Fuente:"; break;
                case 6: lblValor1.Text = "Fecha Inicio:"; lblValor2.Text = "Fecha Fin:"; break;
                case 8: lblValor1.Text = "Deudor:"; break;
                case 7: lblValor1.Text = "Acreedor:"; break;
            }
            if (Convert.ToInt32(opcion.SelectedValue) == 6)
            {
                valor1.TextMode = TextBoxMode.Date;
                valor2.Visible = true;
                valor2.TextMode = TextBoxMode.Date;
                lblValor2.Visible = true;
            }
            else
            {
                valor1.TextMode = TextBoxMode.SingleLine;
                valor2.Visible = false;
                lblValor2.Visible = false;
            }
        }

        protected void btnGuardarCambios_Click(object sender, EventArgs e)
        {
            clsPrestamo mp = new clsPrestamo();

            string a = "";
            string b = "";
            //ArrayList DatosPrestamo = new ArrayList();

            mp.ModificarPrestamo(txtIdPrestamo.Text, txtFuente.Text, txtSituacion.Text, txtPlazo.Text, txtNombre.Text, Convert.ToDateTime(txtFechaFirmado.Text),
                Convert.ToDateTime(txtLimiteGiro.Text), Convert.ToDateTime(txtLimiteEfectivo.Text), Convert.ToDateTime(txtEfectivo.Text),
                Convert.ToDecimal(txtMonto.Text), txtIdMoneda.Text, txtTpoTramo.Text, txtProposito.Text, txtGarantiaPublica.Text, txtOrigenDeuda.Text,
                Convert.ToInt32(txtIdAcreedor.Text), Convert.ToInt32(txtIdDeudor.Text), txtTpoPrestamo.Text, Convert.ToDecimal(txtTasa.Text), "prueba",
                Convert.ToDateTime(Session["fechaModificacion"] as string), out a, out b);

            TextBox1.Text = b;
        }

        //protected void btnCrearPrestamo_Click(object sender, EventArgs e)
        //{
        //    clsManejoPrestamos mp = new clsManejoPrestamos();
        //    ArrayList DatosPrestamo = new ArrayList();

        //    DatosPrestamo.Add(txtIdPrestamo.Text);
        //    DatosPrestamo.Add(txtFuente.Text);
        //    DatosPrestamo.Add(txtSituacion.Text);
        //    DatosPrestamo.Add(txtPlazo.Text);
        //    DatosPrestamo.Add(txtNombre.Text);
        //    DatosPrestamo.Add(Convert.ToDateTime(txtFechaFirmado.Text));
        //    DatosPrestamo.Add(Convert.ToDateTime(txtLimiteGiro.Text));
        //    DatosPrestamo.Add(Convert.ToDateTime(txtLimiteEfectivo.Text));
        //    DatosPrestamo.Add(Convert.ToDateTime(txtEfectivo.Text));
        //    DatosPrestamo.Add(Convert.ToDecimal(txtMonto.Text));
        //    DatosPrestamo.Add(txtIdMoneda.Text);
        //    DatosPrestamo.Add(txtTpoTramo.Text);
        //    DatosPrestamo.Add(txtProposito.Text);
        //    DatosPrestamo.Add(txtGarantiaPublica.Text);
        //    DatosPrestamo.Add(txtOrigenDeuda.Text);
        //    DatosPrestamo.Add(Convert.ToInt32(txtIdAcreedor.Text));
        //    DatosPrestamo.Add(Convert.ToInt32(txtIdDeudor.Text));
        //    DatosPrestamo.Add(txtTpoPrestamo.Text);
        //    DatosPrestamo.Add(Convert.ToDecimal(txtTasa.Text));
        //    DatosPrestamo.Add("prueba");
        //    //DatosPrestamo.Add(Convert.ToDateTime(Session["fechaModificacion"] as string));

        //    TextBox1.Text = mp.registraPrestamoDB(DatosPrestamo);
        //}

        protected void btnCrearPrestamo_Click(object sender, EventArgs e)
        {
            clsPrestamo mp = new clsPrestamo();
            
            string a="";
            string b="";

            mp.CrearPrestamo(txtIdPrestamo.Text, txtFuente.Text, txtSituacion.Text, txtPlazo.Text, txtNombre.Text,
            Convert.ToDateTime(txtFechaFirmado.Text), Convert.ToDateTime(txtLimiteGiro.Text), Convert.ToDateTime(txtLimiteEfectivo.Text),
            Convert.ToDateTime(txtEfectivo.Text), Convert.ToDecimal(txtMonto.Text), txtIdMoneda.Text, txtTpoTramo.Text, txtProposito.Text,
            txtGarantiaPublica.Text, txtOrigenDeuda.Text, Convert.ToInt32(txtIdAcreedor.Text), Convert.ToInt32(txtIdDeudor.Text),
            txtTpoPrestamo.Text, Convert.ToDecimal(txtTasa.Text), "ACT", "prueba", out a, out b);

            TextBox1.Text = mp.Lstr_TextoError;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                clsCalculosDeudaExterna cls = new clsCalculosDeudaExterna();
                GridView1.DataSource = cls.ActualizaTipoCambio("jgomez");
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                txtFuente.Text = "Error: " + ex.ToString();
                txtIdAcreedor.Text = Convert.ToString(Environment.CurrentDirectory);
            }

            //try
            //{
            //    tTpoCambio cls = new tTpoCambio();                
            //    DateTime ldt_FechaActual = DateTime.UtcNow.Date;
            //    GridView1.DataSource = cls.ConsultarTposCambio("EUR", ldt_FechaActual, "333");
            //    GridView1.DataBind();
            //}
            //catch (Exception ex)
            //{
            //    txtFuente.Text = "Error: " + ex.ToString();
            //}
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gv_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}