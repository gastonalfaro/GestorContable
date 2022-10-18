using Presentacion.Compartidas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Mantenimiento
{
    public partial class frmTasaVariableTitulos_cp : BASE
    {
        #region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private Presentacion.wsDeudaInterna.wsDeudaInterna ws_DIService = new Presentacion.wsDeudaInterna.wsDeudaInterna();

        private char gchr_MensajeError;
        private char gchr_MensajeExito;
        private string gstr_Usuario = String.Empty;
        private string gstr_ModuloActual = String.Empty;

        //static DataSet gds_TasaVariableTitulos = new DataSet();
        protected DataSet gds_TasaVariableTitulos
        {
            get
            {
                if (ViewState["gds_TasaVariableTitulos"] == null)
                    ViewState["gds_TasaVariableTitulos"] = new DataSet();
                return (DataSet)ViewState["gds_TasaVariableTitulos"];
            }
            set
            {
                ViewState["gds_TasaVariableTitulos"] = value;
            }
        }
        //static DataSet gds_IndicadoresEconomicos = new DataSet();
        protected DataSet gds_IndicadoresEconomicos
        {
            get
            {
                if (ViewState["gds_IndicadoresEconomicos"] == null)
                    ViewState["gds_IndicadoresEconomicos"] = new DataSet();
                return (DataSet)ViewState["gds_IndicadoresEconomicos"];
            }
            set
            {
                ViewState["gds_IndicadoresEconomicos"] = value;
            }
        }
        //static DataSet gds_ValoresIndicadoresEco = new DataSet();
        protected DataSet gds_ValoresIndicadoresEco
        {
            get
            {
                if (ViewState["gds_ValoresIndicadoresEco"] == null)
                    ViewState["gds_ValoresIndicadoresEco"] = new DataSet();
                return (DataSet)ViewState["gds_ValoresIndicadoresEco"];
            }
            set
            {
                ViewState["gds_ValoresIndicadoresEco"] = value;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            gstr_Usuario = clsSesion.Current.LoginUsuario;

            gchr_MensajeError = clsSesion.Current.chr_MensajeError;
            gchr_MensajeExito = clsSesion.Current.chr_MensajeExito;

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(gstr_Usuario))
                {
                    if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmTasaVariableTitulos" ))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                    {
                        OcultarMensaje();
                        ConsultarTasaVariableTitulos("", "", "01/01/1900", "01/01/5000");
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

        private void ConsultarTasaVariableTitulos(string str_NumValor, string str_Nemotecnico, string str_FchInicio, string str_FchFin)
        {
            if (String.IsNullOrEmpty(str_NumValor))
                str_NumValor = "%";
            if (String.IsNullOrEmpty(str_Nemotecnico))
                str_Nemotecnico = "%";
            gds_TasaVariableTitulos = ws_DIService.ConsultarTituloValorMant(str_NumValor, str_Nemotecnico, "%Tasa Variable", str_FchInicio, str_FchFin,"N");

            if (gds_TasaVariableTitulos.Tables["Table"].Rows.Count > 0)
            {
                gds_TasaVariableTitulos.Tables["Table"].DefaultView.Sort = "NroValor";

                grdvTasaVariableTitulos.DataSource = gds_TasaVariableTitulos.Tables["Table"];
                grdvTasaVariableTitulos.DataBind();
            }
            else
            {
                grdvTasaVariableTitulos.DataSource = this.LlenarTablaVacia();
                grdvTasaVariableTitulos.DataBind();
            }
        }

        private DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("NroValor", typeof(string));
            ldt_TablaVacia.Columns.Add("Nemotecnico", typeof(string));
            ldt_TablaVacia.Columns.Add("IDTasaVariable", typeof(string));
            ldt_TablaVacia.Columns.Add("TasaVariableValor", typeof(string));
            ldt_TablaVacia.Columns.Add("Margen", typeof(string));
            ldt_TablaVacia.Columns.Add("TasaVariable", typeof(string));            
            ldt_TablaVacia.Columns.Add("FchModifica", typeof(string));
            
            DataRow ldr_FilaTabla = ldt_TablaVacia.NewRow();
            ldt_TablaVacia.Rows.Add(ldr_FilaTabla);
            return ldt_TablaVacia;
        }

        private void ConsultarIndicadoresEco()
        {
            
            //gds_IndicadoresEconomicos = ws_SGService.uwsConsultarIndicadoresEconomicos("", "", "");

            //if (gds_IndicadoresEconomicos.Tables["Table"].Rows.Count > 0)
            //{
            //    gds_IndicadoresEconomicos.Tables["Table"].DefaultView.Sort = "TipoNemotecnico";
            //    ddlTasaVariable.DataSource = gds_IndicadoresEconomicos.Tables["Table"];
            //    grdNemotecnicos.DataBind();
            //}
            //else
            //{
            //    grdNemotecnicos.DataSource = this.LlenarTablaVacia();
            //    grdNemotecnicos.DataBind();
            //    grdNemotecnicos.Rows[0].Visible = false;
            //}
        }



        protected void btnTasaVariableTituloConsultar_Click(object sender, EventArgs e)
        {
            ConsultarTasaVariableTitulos(this.txtBuscarNroValor.Text, this.txtBusqNomTasaVariableTitulo.Text, "01/01/1900", "01/01/5000");
        }

        protected void grdvTasaVariableTitulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grdvTasaVariableTitulos.SelectedIndex < 0)
                return;
        }

        protected void grdvTasaVariableTitulos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdvTasaVariableTitulos.EditIndex = e.NewEditIndex;
            grdvTasaVariableTitulos.DataSource = gds_TasaVariableTitulos.Tables["Table"];

            grdvTasaVariableTitulos.DataBind();
            //ConsultarTasaVariableTitulos(this.txtBuscarNroValor.Text, this.txtBusqNomTasaVariableTitulo.Text, "01/01/1900", "01/01/5000");
        }

        protected void grdvTasaVariableTitulos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            String[] lstr_result = new String[3];
            String lstr_resultado = String.Empty;
            try
            {
                GridViewRow row = (GridViewRow)grdvTasaVariableTitulos.Rows[e.RowIndex];

                Label lbl_NumValor = (Label)row.FindControl("lblNroValor");
                Label lbl_Nemotecnico = (Label)row.FindControl("lblNemotecnico");
                Label lbl_Margen = (Label)row.FindControl("lblMargen");

                Label lbl_TasaVariableValor = (Label)row.FindControl("lblTasaVariableValor");

                DropDownList ddl_Valor = (DropDownList)row.FindControl("ddlTasaVariable");

                Label lbl_FchModifica = (Label)row.FindControl("lblFchModifica");
                DateTime ldt_FchModifica = Convert.ToDateTime(lbl_FchModifica.Text);

                string str_fecha = String.Empty;
                str_fecha = ldt_FchModifica.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                ldt_FchModifica = Convert.ToDateTime(str_fecha);

                gds_ValoresIndicadoresEco = ws_SGService.uwsConsultarValoresIndicadoresEco(ddl_Valor.SelectedValue, DateTime.Today, "N");
               
                decimal ldec_Valor = Convert.ToDecimal(gds_ValoresIndicadoresEco.Tables["Table"].Rows[0]["Valor"].ToString());
                

                lstr_resultado = ws_DIService.ModificarTituloValorMant(Convert.ToInt32(lbl_NumValor.Text), lbl_Nemotecnico.Text,
                    ddl_Valor.SelectedValue, Convert.ToDecimal(lbl_TasaVariableValor.Text.Equals(string.Empty) ? ldec_Valor.ToString() : lbl_TasaVariableValor.Text), Convert.ToDecimal(lbl_Margen.Text), gstr_Usuario, ldt_FchModifica);

                if (lstr_resultado.Contains("00"))
                    MostarMensaje("La modificación de datos ha sido satisfactoria.", gchr_MensajeExito);
                else
                    MostarMensaje("La modificación de datos no ha sido satisfactoria.", gchr_MensajeError);

                grdvTasaVariableTitulos.EditIndex = -1;
                grdvTasaVariableTitulos.DataSource = gds_TasaVariableTitulos.Tables["Table"];

                grdvTasaVariableTitulos.DataBind();
                //ConsultarTasaVariableTitulos(this.txtBuscarNroValor.Text, this.txtBusqNomTasaVariableTitulo.Text, "01/01/1900", "01/01/5000");
            }
            catch (Exception ex)
            {
                ConsultarTasaVariableTitulos(this.txtBuscarNroValor.Text, this.txtBusqNomTasaVariableTitulo.Text, "01/01/1900", "01/01/5000");
                MostarMensaje("La modificación de datos no ha sido satisfactoria.", gchr_MensajeError);

            }
        }

        protected void grdvTasaVariableTitulos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdvTasaVariableTitulos.PageIndex = e.NewPageIndex;
            grdvTasaVariableTitulos.DataSource = gds_TasaVariableTitulos.Tables["Table"];

            grdvTasaVariableTitulos.DataBind();
            //ConsultarTasaVariableTitulos(this.txtBuscarNroValor.Text, this.txtBusqNomTasaVariableTitulo.Text, "01/01/1900", "01/01/5000");
        }

        protected void grdvTasaVariableTitulos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdvTasaVariableTitulos.EditIndex = -1;
            grdvTasaVariableTitulos.DataSource = gds_TasaVariableTitulos.Tables["Table"];

            grdvTasaVariableTitulos.DataBind();
            //ConsultarTasaVariableTitulos(this.txtBuscarNroValor.Text, this.txtBusqNomTasaVariableTitulo.Text, "01/01/1900", "01/01/5000");
        }

        protected void grdvTasaVariableTitulos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //DataRow ldr_Tasa = gds_TasaVariableTitulos.Tables["Table"].NewRow();
            
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    ldr_Tasa = gds_TasaVariableTitulos.Tables["Table"].Rows[e.Row.DataItemIndex];
            //    string lstr_Eco = ldr_Tasa["TasaVariable"].ToString();
            //    gds_IndicadoresEconomicos = ws_SGService.uwsConsultarIndicadoresEconomicos(lstr_Eco, "", "");
            //    DataRow ldr_Indicadores = gds_IndicadoresEconomicos.Tables["Table"].NewRow();
            //    ldr_Indicadores = gds_IndicadoresEconomicos.Tables["Table"].Rows[0];
            //    string lstr_EcoNom = ldr_Indicadores["NomIndicador"].ToString();

            //    Label lblTasaVariable = (Label) e.Row.FindControl("lblTasaVariable");
            ////    lblTasaVariable.Text = lstr_EcoNom;

            //}
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    string pTasaVariable = (e.Row.FindControl("lblTasaVariable") as Label).Text;

                    DropDownList ddlTasaVariable = (DropDownList)e.Row.FindControl("ddlTasaVariable");
                    ddlTasaVariable.DataTextField = "NomIndicador";
                    ddlTasaVariable.DataValueField = "IdIndicadorEco";

                    ddlTasaVariable.DataSource = ws_SGService.uwsConsultarIndicadoresEconomicos("", "", "");
                    ddlTasaVariable.DataBind();
                    ddlTasaVariable.Items.Insert(0, new ListItem("-"));

                    if (pTasaVariable.Equals(string.Empty))
                        ddlTasaVariable.SelectedValue = "0";
                    else
                        ddlTasaVariable.Items.FindByValue(pTasaVariable).Selected = true;
                }
            }
            
        }

        protected void ddlTasaVariable_SelectedIndexChanged(object sender, EventArgs e){
        
        //        gds_ValoresIndicadoresEco = ws_SGService.uwsConsultarValoresIndicadoresEco(ddl_Valor.SelectedValue.Trim(), DateTime.Today, "");
        //        decimal ldec_Valor = Convert.ToDecimal(gds_ValoresIndicadoresEco.Tables["Table"].Rows[0]["Valor"].ToString());
                
        }
    }
}