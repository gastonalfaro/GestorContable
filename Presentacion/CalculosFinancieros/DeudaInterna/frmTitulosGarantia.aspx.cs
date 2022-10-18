using Presentacion.Compartidas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Presentacion.CalculosFinancieros.DeudaInterna
{
    public partial class frmTitulosGarantia : BASE
    {
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private Presentacion.wsDeudaInterna.wsDeudaInterna wsDeudaInterna = new wsDeudaInterna.wsDeudaInterna();
        private string gstr_Usuario = String.Empty;
        private string gstr_ModuloActual = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                gstr_Usuario = clsSesion.Current.LoginUsuario;

                if (!IsPostBack)
                {
                    CargarNemotecnico();
                    if (!string.IsNullOrEmpty(gstr_Usuario))
                    {
                        if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmTitulosGarantia"))
                            Response.Redirect("~/Principal.aspx", true);
                        else
                        {
                            ConsultaTitulosGarantia();
                            this.ddlNroValor.Items.Clear();
                            this.ddlNroValor.Items.Add(new ListItem("--Seleccione un valor--", "00"));
                            this.ddlNroValor.Items.Add(new ListItem("Insertar Valor", "01"));
                        }
                    }
                    else
                        Response.Redirect("~/Login.aspx", true);
                }
            }
            catch (Exception ex)
            {
                //lblEstatus.Text = ex.ToString();
                //Response.Redirect("~/Login.aspx", true);
            }
        }

        private void CargarNemotecnico()
        {
            DataSet lNemotecnicos = new DataSet();
            lNemotecnicos =  ws_SGService.uwsConsultarNemotecnicos("", "", "", "", "");
            this.ddlNemotecnico.DataTextField = "IdNemotecnico";
            this.ddlNemotecnico.DataValueField = "IdNemotecnico";
            if(lNemotecnicos.Tables.Count>0 && lNemotecnicos.Tables[0].Rows.Count>0)
            {
                this.ddlNemotecnico.DataSource = lNemotecnicos;
                this.ddlNemotecnico.DataBind();
            }//if
        }

        protected void ConsultaTitulosGarantia()
        {
            try
            {
                DataTable ldt_Garantia = new DataTable();
                //ldt_Garantia = wsDeudaInterna.ConsultarTitulosValores("Garantia", "01/01/1900", "01/01/5000").Tables[0];
                ldt_Garantia = wsDeudaInterna.ConsultarTitulosValores(String.Empty, String.Empty, String.Empty, "G", String.Empty, String.Empty, String.Empty, String.Empty, "01/01/1900", "01/01/5000").Tables[0];
                if (ldt_Garantia.Rows.Count > 0)
                {
                    grvTituloGar.DataSource = ldt_Garantia;
                    grvTituloGar.DataBind();
                }//if
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }

        protected void btnTituloGar_Click(object sender, EventArgs e)
        {
            try
            {
                string lstr_Crear = wsDeudaInterna.CrearTituloGarantia(this.ddlNroValor.SelectedIndex == 1 ? Convert.ToInt32(this.txtNroValor1.Text) : Convert.ToInt32(this.ddlNroValor.SelectedValue), txtIndicador.Text, ddlNemotecnico.SelectedValue.Trim(), "-", 0, DateTime.Today.ToString("dd/MM/yyyy"), "", "G", gstr_Usuario);
                lblEstadoTrans.Text = "Estado de la transacción: Transacción procesada.";
                ConsultaTitulosGarantia();
            }
            catch (Exception ex)
            {
                lblEstadoTrans.Text = "Estado de la transacción: Transacción fallida, revise los datos e intente nuevamente.";
            }
        }
        
        private void LimpiarCampos()
        {
            this.ddlNroValor.SelectedIndex = 
            this.ddlNemotecnico.SelectedIndex = 0;
            this.txtNroValor1.Text = string.Empty;
        }

        protected void grvTituloGar_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Elimina")
            {
                string[] lstr_ResEliminacion = new string[2];

                if (grvTituloGar.Rows.Count > 0)
                {
                    LinkButton lb = (LinkButton)e.CommandSource;
                    GridViewRow vGridViewRow = (GridViewRow)lb.NamingContainer;

                    string lstr_NroValor = grvTituloGar.DataKeys[vGridViewRow.RowIndex].Values[0].ToString();
                    string lstr_Nemotecnico = grvTituloGar.DataKeys[vGridViewRow.RowIndex].Values[1].ToString();

                    lstr_ResEliminacion = wsDeudaInterna.EliminarTituloGarantia(int.Parse(lstr_NroValor), lstr_Nemotecnico, gstr_Usuario);
                    if (lstr_ResEliminacion[0] == "00")
                    {
                        ConsultaTitulosGarantia();
                        MessageBox.Show("El registro fue eliminado satisfactoriamente.");

                    }
                    else MessageBox.Show(lstr_ResEliminacion[1]);
                }
            }
         
        }

        protected void CambiarNemotecnico(object sender, EventArgs e)
        {            
            List<string> lstr_NroValor = new List<string>();
            this.ddlNroValor.Items.Clear();
            this.ddlNroValor.Items.Add(new ListItem("--Seleccione un valor--", "00"));
            this.ddlNroValor.Items.Add(new ListItem("Insertar Valor", "01"));

            DataTable dt_Titulos = new DataTable();
            dt_Titulos = wsDeudaInterna.ConsultarTitulosValores(string.Empty, this.ddlNemotecnico.SelectedValue.Trim(), String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "01/01/1900", "01/01/5000").Tables[0];
            this.ddlNroValor.DataTextField =
            this.ddlNroValor.DataValueField = "NroValor";

            if (dt_Titulos.Rows.Count > 0)
            {
                //this.ddlNroValor.DataSource = ds_TitulosValores;
                foreach (DataRow vFila in dt_Titulos.Rows)
                {
                    if (!ddlNroValor.Items.Contains(new ListItem(vFila["NroValor"].ToString(), vFila["NroValor"].ToString())))
                    {
                        ddlNroValor.Items.Add(new ListItem(vFila["NroValor"].ToString(), vFila["NroValor"].ToString()));
                    }
                }
                // this.ddlNumValor.DataBind();
            }//if
            //if (dt_Titulos.Rows.Count > 0) 
            //{
            //    this.ddlNroValor.DataSource = dt_Titulos;
            //    this.ddlNroValor.DataBind();
            //}//if
            
        }

        protected void ddlNroValor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlNroValor.SelectedIndex == 1)
            {
                this.txtNroValor1.Text = string.Empty;
                this.txtNroValor1.Visible = true;
            }
            else
                this.txtNroValor1.Visible = false;
        }

    }
}