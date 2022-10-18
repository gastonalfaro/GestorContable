
using Presentacion.Compartidas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Presentacion.CalculosFinancieros.DeudaExterna
{
    public partial class frmCrearTitulosCanjeSubasta : BASE
    {
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private wsDI.wsDeudaInterna wsDI = new wsDI.wsDeudaInterna();

        private string gstr_Usuario = String.Empty;
        private DataSet gds_Formularios = new DataSet();


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                gstr_Usuario = clsSesion.Current.LoginUsuario;

                if (!IsPostBack)
                {
                    if (!string.IsNullOrEmpty(gstr_Usuario))
                    {
                        if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmCrearTitulosCanjeSubasta"))
                        {
                            Response.Redirect("~/Principal.aspx", true);
                        }
                        else
                        {
                            CargarNumeroSerie();

                        }

                    }
                    else
                        Response.Redirect("~/Login.aspx", true);
                }
                else 
                {
                    CargarDatosTitulosSubasta();
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Login.aspx", true);
            }
        }

        private void CargarNumeroSerie()
        {

            this.ddlNumSerie.DataSource = ws_SGService.uwsConsultarDinamico("SELECT DISTINCT Convert(Varchar(10),FchValor,103) as FchValor FROM [cf].[TitulosValores] WHERE [IndicadorCupon] = 'V' AND [TipoNegociacion] = 'Compra' and DescripcionNegociacion in  ('Canje/Lici/Precio','Canje/Lici/Rend','Canje/Inversa/Precio','Canje/Inversa/Rend') ");
            this.ddlNumSerie.DataTextField = "FchValor";
            this.ddlNumSerie.DataValueField = "FchValor";
            this.ddlNumSerie.DataBind();
        }

        protected void btnInsertarTituloCanjeSubasta_Click(object sender, EventArgs e)
        {
            try
            {
                string mens1 = String.Empty;
                string mens2 = String.Empty;
                string fecha = ddlNumSerie.SelectedValue;

                wsDI.CrearTituloCanjeSubasta(txtNroEmision.Text, Convert.ToInt32(txtNumeroValor.Text), txtNemotecnico.Text, fecha.Trim(), txtNroEmision.Text, "SG");
                CargarDatosTitulosSubasta();
                txtNemotecnico.Text = "";
                txtNroEmision.Text = "";
                txtNumeroValor.Text = "";
                //txtFechaCanje.Text = "";
            }
            catch (Exception ex)
            {
              
            }
           
        }

        private void CargarDatosTitulosSubasta()
        {
            DataSet lds_TitulosSubasta = ws_SGService.uwsConsultarDinamico("SELECT NroEmisionSerie,NroValor,Nemotecnico,FchCanje  FROM [cf].[TitulosCanjeSubasta] "  +
                                                                           "WHERE Convert(varchar(8),FchCanje,112) = Convert(varchar(8),Convert(datetime,'" + ddlNumSerie.SelectedValue + "',103),112)");
            if (lds_TitulosSubasta.Tables["Table"].Rows.Count > 0)
            {
                grdvTitulos.DataSource = lds_TitulosSubasta.Tables["Table"];
                grdvTitulos.DataBind();
            }
            else
            {
                grdvTitulos.DataSource = this.LlenarTablaVacia();
                grdvTitulos.DataBind();
                grdvTitulos.Rows[0].Visible = false;
            }
        }

     

        private DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();

            ldt_TablaVacia.Columns.Add("FchCanje", typeof(string));
            ldt_TablaVacia.Columns.Add("NroEmisionSerie", typeof(string));
            ldt_TablaVacia.Columns.Add("NroValor", typeof(string));
            ldt_TablaVacia.Columns.Add("Nemotecnico", typeof(string));

            DataRow ldr_FilaTabla = ldt_TablaVacia.NewRow();
            ldt_TablaVacia.Rows.Add(ldr_FilaTabla);
            return ldt_TablaVacia;
        }

        protected void grdvFormularios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void ddlNumSerie_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarDatosTitulosSubasta();
        }

        protected void btnContabilizarCanje_Click(object sender, EventArgs e)
        {
            if (grdvTitulos.Rows.Count > 0)
            {
                ws_SGService.uwsConsultarDinamico("Exec [cf].[uspRegistrarResumenCanje] '" + ddlNumSerie.SelectedValue  + "','C' ");
                //wsDI.CrearAsientoCanje(ddlNumSerie.SelectedValue);
            }

        }

        /// <summary>
        /// Elimina los títulos que se asocian por error
        /// </summary>
        protected void lbtEditarDireccion_Click(object sender, EventArgs e)
        {
            LinkButton lnk = sender as LinkButton;
            GridViewRow vGridViewRow = (GridViewRow)lnk.NamingContainer;
            string vConsulta = "";
            string vNroSerie = grdvTitulos.DataKeys[vGridViewRow.RowIndex].Values[0].ToString();
            int vNrovalor = Int32.Parse(grdvTitulos.DataKeys[vGridViewRow.RowIndex].Values[1].ToString());
            string vNemotecnico = grdvTitulos.DataKeys[vGridViewRow.RowIndex].Values[2].ToString();
            DateTime vFchCanje = Convert.ToDateTime(grdvTitulos.DataKeys[vGridViewRow.RowIndex].Values[3].ToString());

            vConsulta = "DELETE FROM cf.TitulosCanjeSubasta WHERE  NroEmisionSerie= '"+vNroSerie+"' and  NroValor ="+vNrovalor+" and Nemotecnico = '"+vNemotecnico+"' and FchCanje = '"+vFchCanje.ToString("yyyy-MM-dd")+"'";
            ws_SGService.uwsConsultarDinamico(vConsulta);
            CargarDatosTitulosSubasta();
           
        }

    }
}