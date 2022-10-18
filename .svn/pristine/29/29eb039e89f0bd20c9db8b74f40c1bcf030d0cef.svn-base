using Presentacion.Compartidas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.CalculosFinancieros.DeudaInterna
{
    public partial class frmAnularTituloValor : BASE
    {
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private Presentacion.wsDeudaInterna.wsDeudaInterna wsDeudaInterna = new Presentacion.wsDeudaInterna.wsDeudaInterna();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
                CargarNemotecnico();
        }

        private void CargarNemotecnico()
        {
            DataSet lNemotecnicos = ws_SGService.uwsConsultarNemotecnicos("", "", "", "", "");
            this.ddlNemotecnico.DataSource = lNemotecnicos;
            this.ddlNemotecnico.DataTextField = "IdNemotecnico";
            this.ddlNemotecnico.DataValueField = "IdNemotecnico";
            this.ddlNemotecnico.DataBind();
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            COnsultarDatos();
        }

        protected void btnAnular_Click(object sender, EventArgs e)
        {
           string[] vResultado = wsDeudaInterna.AnularTituloValor(Convert.ToInt32(this.ddlNumValor.SelectedValue), this.ddlNemotecnico.SelectedValue, "Anulado", clsSesion.Current.LoginUsuario, Convert.ToDateTime(this.txtFecha.Text));
          

           if (vResultado[0].Equals("00"))
           {
               ws_SGService.uwsRegistrarAccionBitacoraCo
                    ("DI",  
                    clsSesion.Current.LoginUsuario, 
                    "Anulación de Titulos de valor",
                    "Se anuló el titulo del valor: " + this.ddlNumValor.SelectedValue + "-" + this.ddlNemotecnico.SelectedValue, 
                    "",
                    this.ddlNumValor.SelectedValue + "-" + this.ddlNemotecnico.SelectedValue, 
                    "G206");
           
           }
            
            MessageBox.Show(vResultado[1]);

           COnsultarDatos();
        }


        protected void ddlNemotecnico_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlNumValor.Items.Clear();

            DataSet ds_TitulosValores = new DataSet();
            ds_TitulosValores = wsDeudaInterna.ConsultarTitulosValores(String.Empty, this.ddlNemotecnico.SelectedValue.Trim(), String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "01/01/1900", "01/01/5000");
            this.ddlNumValor.DataTextField =
            this.ddlNumValor.DataValueField = "NroValor";

            if (ds_TitulosValores.Tables.Count > 0 && ds_TitulosValores.Tables[0].Rows.Count > 0) 
            {
                ///this.ddlNumValor.DataSource = ds_TitulosValores;
                foreach (DataRow vFila in ds_TitulosValores.Tables[0].Rows)
                {
                    if (!ddlNumValor.Items.Contains(new ListItem(vFila["NroValor"].ToString(), vFila["NroValor"].ToString()))) 
                    {
                        ddlNumValor.Items.Add(new ListItem(vFila["NroValor"].ToString(), vFila["NroValor"].ToString()));
                    }
                }
               // this.ddlNumValor.DataBind();
            }//if
           
            this.ddlNumValor.Items.Insert(0, (new ListItem("-- Seleccione Valor --", "")));
        }

        private void COnsultarDatos() 
        {
            DataTable lTitulosValores = new DataTable();
            lTitulosValores = wsDeudaInterna.ConsultarTitulosValores(this.ddlNumValor.SelectedValue, this.ddlNemotecnico.SelectedValue.Trim(), String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "01/01/1900", "01/01/5000").Tables[0];
            
            if (lTitulosValores.Rows.Count > 0)
            {
                grdvTitulosValores.DataSource = lTitulosValores;
                grdvTitulosValores.DataBind();
                this.btnAnular.Visible = true;

                foreach (DataRow dCol in lTitulosValores.Rows)
                {
                    this.txtFecha.Text = dCol["FchModifica"].ToString();
                    break;
                }

            }
        }
    }
}