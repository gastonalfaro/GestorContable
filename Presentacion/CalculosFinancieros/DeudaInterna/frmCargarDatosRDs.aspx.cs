using System;
using System.Globalization;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using LogicaNegocio.CalculosFinancieros.DeudaInterna;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Presentacion.Compartidas;
using System.Web.UI;

namespace Presentacion.CalculosFinancieros.DeudaInterna
{
    public partial class frmCargarDatosRDs : BASE
    {
        private static wsDeudaInterna.wsDeudaInterna wsDeudaInterna = new wsDeudaInterna.wsDeudaInterna();

        # region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        //DataTable ldat_CargaRDs
        protected DataTable ldat_CargaRDs
        {
            get
            {
                if (ViewState["ldat_CargaRDs"] == null)
                    ViewState["ldat_CargaRDs"] = new DataTable();
                return (DataTable)ViewState["ldat_CargaRDs"];
            }
            set
            {
                ViewState["ldat_CargaRDs"] = value;
            }
        }
        private string gstr_Usuario = String.Empty;
        private string gstr_ModuloActual = String.Empty;
        # endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                gstr_Usuario = clsSesion.Current.LoginUsuario;

                if (!IsPostBack)
                {

                    DateTime today = DateTime.Now;
                    DateTime FchIni = today.AddDays(-30); //a la fecha actual le resto 30 días para mostrar un rango de 1 mes

                    this.txtFechaInicio.Text = FchIni.ToString("dd/MM/yyyy"); 
                    this.txtFechaFin.Text = DateTime.Now.ToString("dd/MM/yyyy");

                    if (!string.IsNullOrEmpty(gstr_Usuario))
                    {
                        if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmCargarDatosRDs"))
                            Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                        Response.Redirect("~/Login.aspx", true);
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Login.aspx", true);
            }
        }

        protected void cargarGV()
        {
            grvRDs.DataSource = ldat_CargaRDs;
            grvRDs.DataBind();
        }

        protected void btnCargarRDs_Click(object sender, EventArgs e)
        {
            //DataTable ldat_CargaRDs = new DataTable();
            lblResultado.Text = "El procedimiento puede tardar entre 15 a 20 minutos dependiendo del rango de fechas";
            string msj = "";
            string msj2 = "";
            try
            {
                msj2 = wsDeudaInterna.CargarValoresCuponesSINPE(txtFechaInicio.Text, txtFechaFin.Text, "1");
                msj = wsDeudaInterna.CargarValoresCuponesSINPE(txtFechaInicio.Text, txtFechaFin.Text, "3");
                
                //ldat_CargaRDs = wsDeudaInterna.ConsultarTitulosValores("T", txtFechaCarga.Text, txtFechaCarga.Text).Tables[0];   
                DataSet ds_CargaRDs = wsDeudaInterna.ConsultarTitulosValores(String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, txtFechaInicio.Text, txtFechaFin.Text);
                
                if (ds_CargaRDs.Tables.Count > 0 && ds_CargaRDs.Tables["Table"].Rows.Count > 0)
                {
                    ldat_CargaRDs = ds_CargaRDs.Tables[0];
                    cargarGV();//"01/01/1900", "01/01/5000").Tables[0]);
                }
            }
            catch (Exception ex)
            {
                msj = "Error " + ex.ToString();
            }
            string script = @"<script type='text/javascript'> alert('" + msj + msj2 + "'); </script>";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "alerta", script, false); 
            //grvRDs.DataSource = ldat_CargaRDs;
            //grvRDs.DataBind();
        }

        protected void grvRDs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvRDs.PageIndex = e.NewPageIndex;
            cargarGV();
            //cargarGV(wsDeudaInterna.ConsultarTitulosValores(String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, txtFechaInicio.Text, txtFechaFin.Text).Tables[0]);
        }
    }
}