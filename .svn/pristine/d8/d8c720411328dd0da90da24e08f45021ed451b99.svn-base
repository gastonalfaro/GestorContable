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

namespace Presentacion.Mantenimiento
{
    public partial class frmTasaVariableTitulos : BASE
    {
        # region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private char gchr_MensajeError;
        private char gchr_MensajeExito;
        private string gstr_Usuario = String.Empty;
        private string gstr_ModuloActual = String.Empty;

        //static DataSet gds_Nemotecnicos = new DataSet();
        protected DataSet gds_Nemotecnicos
        {
            get
            {
                if (ViewState["gds_Nemotecnicos"] == null)
                    ViewState["gds_Nemotecnicos"] = new DataSet();
                return (DataSet)ViewState["gds_Nemotecnicos"];
            }
            set
            {
                ViewState["gds_Nemotecnicos"] = value;
            }
        }
        //static DataSet gds_Modulos = new DataSet();
        protected DataSet gds_Modulos
        {
            get
            {
                if (ViewState["gds_Modulos"] == null)
                    ViewState["gds_Modulos"] = new DataSet();
                return (DataSet)ViewState["gds_Modulos"];
            }
            set
            {
                ViewState["gds_Modulos"] = value;
            }
        }
        //static DataSet gds_ClasesDocumento = new DataSet();
        protected DataSet gds_ClasesDocumento
        {
            get
            {
                if (ViewState["gds_ClasesDocumento"] == null)
                    ViewState["gds_ClasesDocumento"] = new DataSet();
                return (DataSet)ViewState["gds_ClasesDocumento"];
            }
            set
            {
                ViewState["gds_ClasesDocumento"] = value;
            }
        }
        //private static DataSet gds_Monedas = new DataSet();
        protected DataSet gds_Monedas
        {
            get
            {
                if (ViewState["gds_Monedas"] == null)
                    ViewState["gds_Monedas"] = new DataSet();
                return (DataSet)ViewState["gds_Monedas"];
            }
            set
            {
                ViewState["gds_Monedas"] = value;
            }
        }
        # endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            gstr_Usuario = clsSesion.Current.LoginUsuario;

            gchr_MensajeError = clsSesion.Current.chr_MensajeError;
            gchr_MensajeExito = clsSesion.Current.chr_MensajeExito;

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(gstr_Usuario))
                {
                    CargaInidcadoresEco();
                    CargarNemotecnico();
                    if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmNemotecnicos"))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                    {
                        OcultarMensaje();

                        gds_Monedas = ws_SGService.uwsConsultarMonedas("", "");
                        ConsultarIndicadoresTitulos();
                        // gds_Modulos = ws_SGService.uwsConsultarMonedas("", "");
                    }
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

        private void ConsultarIndicadoresTitulos()
        {
            DataSet lds_Tabla = new DataSet();
            DataTable ldt_Tabla = new DataTable();
            lds_Tabla = ws_SGService.uwsConsultarDinamico("Select * from cf.IndicadoresPorTitulo");
            if (lds_Tabla.Tables.Count > 0)
                ldt_Tabla = lds_Tabla.Tables[0];
            else
                MostarMensaje("No hay registros para mostrar", '1');

            gvIndicadorTitulo.DataSource = ldt_Tabla;
            gvIndicadorTitulo.DataBind();
        }
        
        private void MostarMensaje(string str_TextMensaje, char chr_TipoMensaje)
        {
            if (chr_TipoMensaje.Equals('1'))
            {
                this.lblMensaje.Text = str_TextMensaje;
                this.lblMensaje.ForeColor = System.Drawing.Color.DarkRed;
                this.lblMensaje.Visible = true;
            }
            else
            {
                this.lblMensaje.Text = str_TextMensaje;
                this.lblMensaje.ForeColor = System.Drawing.Color.DarkGreen;
                this.lblMensaje.Visible = true;
            }

        }

        private void OcultarMensaje()
        {
            this.lblMensaje.Text = String.Empty;
            this.lblMensaje.Visible = false;
        }

        private void CargaInidcadoresEco()
        {
            DataSet ds_Indicadores = ws_SGService.uwsConsultarIndicadoresEconomicos(string.Empty, string.Empty, string.Empty);
            this.ddlInidcadorEconomico.DataSource = ds_Indicadores;
            this.ddlInidcadorEconomico.DataTextField = "NomIndicador";
            this.ddlInidcadorEconomico.DataValueField = "IdIndicadorEco";

            this.ddlInidcadorEconomico.DataBind();
            this.ddlInidcadorEconomico.Items.Insert(0, (new ListItem("-- Seleccione Valor --", "")));
        }

        private void CargarNemotecnico()
        {
            DataSet lNemotecnicos = ws_SGService.uwsConsultarDinamico("select * from ma.Nemotecnicos where tipoNemotecnico = 'Tasa Variable'");
            this.ddlNemotecnico.DataSource = lNemotecnicos;
            this.ddlNemotecnico.DataTextField = "IdNemotecnico";
            this.ddlNemotecnico.DataValueField = "IdNemotecnico";

            this.ddlNemotecnico.DataBind();
            this.ddlNemotecnico.Items.Insert(0, (new ListItem("-- Seleccione Valor --", "")));
        }

        protected void ddlInidcadorEconomico_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds_Valor = ws_SGService.uwsConsultarValoresIndicadoresEco(ddlInidcadorEconomico.SelectedValue, DateTime.Today, "N");
            if (ds_Valor.Tables.Count > 0)
            {
                if (ds_Valor.Tables[0].Rows.Count > 0)
                    txtValorIndicadorEco.Text = ds_Valor.Tables[0].Rows[0]["Valor"].ToString();
                else
                    txtValorIndicadorEco.Text = "0";
            }
            else
            {
                txtValorIndicadorEco.Text = "0";
            }
        }

        protected void btnConsultarNemotecnicos_Click(object sender, EventArgs e)
        {
            

            string queryTitulos = "select count(*) as cont from cf.titulosvalores where nrovalor = "+txtNroValor.Text+" and Nemotecnico = '"+ddlNemotecnico.SelectedValue.Trim()+"' and indicadorcupon = 'V' and Tipo = 'Tasa Variable'";
            DataSet ds_titulos = ws_SGService.uwsConsultarDinamico(queryTitulos);
            string indicador = ddlInidcadorEconomico.SelectedItem.ToString().Length >= 20 ? ddlInidcadorEconomico.SelectedItem.ToString().Substring(0, 17) + "..." : ddlInidcadorEconomico.SelectedItem.ToString();

            if (ds_titulos.Tables.Count > 0)
            {
                if (ds_titulos.Tables[0].Rows[0]["cont"].ToString().Equals("0"))
                {
                    string queryIndicador = "select count(*) as cont from cf.IndicadoresPorTitulo where nrovalor = "+ txtNroValor.Text + " and Nemotecnico = '" + ddlNemotecnico.SelectedValue.Trim() + "'";
                    DataSet ds_Indicador = ws_SGService.uwsConsultarDinamico(queryIndicador);

                    if (ds_Indicador.Tables.Count > 0)
                    {
                        if (ds_Indicador.Tables[0].Rows[0]["cont"].ToString().Equals("0"))
                        {
                            string query = "Insert into cf.IndicadoresPorTitulo (NroValor, Nemotecnico, IdIndicadorEco, FchReferencia, ValorIndicador, UsrCreacion) values (" + txtNroValor.Text + ", '" + ddlNemotecnico.SelectedValue.Trim() + "', '" + indicador + "', '" + DateTime.Today.ToString("yyyy-MM-dd") + "', " + txtValorIndicadorEco.Text + " , '" + gstr_Usuario + "')";
                            ws_SGService.uwsConsultarDinamico(query);
                            MostarMensaje("Tasa asignada a título de manera exitosa", '2');
                        }
                        else
                        {                            
                            string query = "update cf.IndicadoresPorTitulo set IdIndicadorEco = '" + indicador + "', FchReferencia = '" + DateTime.Today.ToString("yyyy-MM-dd") + "', ValorIndicador = " + txtValorIndicadorEco.Text + ", UsrModifica = '" + gstr_Usuario + "' where nrovalor = " + txtNroValor.Text + " and Nemotecnico = '" + ddlNemotecnico.SelectedValue.Trim() + "'";
                            ws_SGService.uwsConsultarDinamico(query);
                            MostarMensaje("Tasa actualizada de manera exitosa", '2');
                        }
                    }
                }
                else
                {
                    MostarMensaje("Ya existe un nemotécnico con esas características",'1');
                }
            }
            else
            {
                MostarMensaje("Error al consultar Títulos", '1');
            }

            ConsultarIndicadoresTitulos();
        }
    }
}