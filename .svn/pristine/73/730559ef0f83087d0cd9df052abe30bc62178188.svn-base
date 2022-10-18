using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Presentacion.Compartidas;
using System.Globalization;

namespace Presentacion.RevelacionNotas.Contingencias
{
    public partial class EdicionRevContingente : BASE
    {
        private static Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private string lst_IdRevelacion = String.Empty;
        private string lst_Ministerio = String.Empty;
        private string lst_Tipo = String.Empty;
        private string str_Usuario = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            str_Usuario = clsSesion.Current.LoginUsuario;

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(str_Usuario))
                {
                    if (!clsSeguridadVistas.MostrarElementos(str_Usuario, Master, "OBJ_RN"))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                    {
                        if (Request.QueryString["Rev"] != null && Request.QueryString["Min"] != null && Request.QueryString["Tip"] != null)
                        {
                            lst_IdRevelacion = Request.QueryString["Rev"];
                            lst_Ministerio = Request.QueryString["Min"];
                            lst_Tipo = Request.QueryString["Tip"];
                            ConsultarRevelacionNota(lst_IdRevelacion, "", "", lst_Ministerio, lst_Tipo);
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/Login.aspx", true);
                }

            }
            else
            {
                if (string.IsNullOrEmpty(str_Usuario))
                    Response.Redirect("~/Login.aspx", true);
            }
        }

        private void ConsultarRevelacionNota(string str_Consecutivo, string str_PerAnual, string str_PerMensual,
            string str_SociedadGL, string str_TipoProceso)
        {            
            List<string> ParametrosConsulta = new List<string>() { str_Consecutivo, str_PerAnual, str_PerMensual, 
                str_SociedadGL, str_TipoProceso };
            DataSet lds_Revelaciones = ws_SGService.uwsConsultarRevelacionContSoc(str_Consecutivo, str_PerAnual,
                str_PerMensual, str_SociedadGL, str_TipoProceso);
                
            if (lds_Revelaciones.Tables.Count > 0)
            {
                if (lds_Revelaciones.Tables["Table"].Rows.Count > 0)
                {
                    //lblSinResultados.Visible = false;
                    lblPAnual.Text =  lds_Revelaciones.Tables["Table"].Rows[0]["PeriodoAnual"].ToString();
                    lblPMensual.Text = lds_Revelaciones.Tables["Table"].Rows[0]["PeriodoMensual"].ToString();
                    lbltextoConsecutivo.Text = lds_Revelaciones.Tables["Table"].Rows[0]["IdRevCont"].ToString();
                    lblMinisterio.Text = lds_Revelaciones.Tables["Table"].Rows[0]["NomSociedad"].ToString();
                    lblTipo.Text = lds_Revelaciones.Tables["Table"].Rows[0]["NomOpcion"].ToString();
                    lblMontoActivos.Text = lds_Revelaciones.Tables["Table"].Rows[0]["MontoTotalActivos"].ToString();
                    lblExpedientesActivos.Text = lds_Revelaciones.Tables["Table"].Rows[0]["TotalExpActivos"].ToString();
                    lblMontoPasivos.Text = lds_Revelaciones.Tables["Table"].Rows[0]["MontoTotalPasivos"].ToString();
                    lblExpedientesPasivos.Text = lds_Revelaciones.Tables["Table"].Rows[0]["TotalExpPasivos"].ToString();
                    txtObservaciones.Text = lds_Revelaciones.Tables["Table"].Rows[0]["Observaciones"].ToString();
                    clsSesion.Current.FechaUltimaConsulta = lds_Revelaciones.Tables["Table"].Rows[0]["FchModifica"].ToString();
                }
            }
            else
            {
                //lblSinResultados.Visible = true;
            }
        }

        private void ActualizarObservacion(string str_IdRevCont, string str_IdSociedadGL,
            string str_TipoProceso, string str_Observacion, string str_UsrModifica)
        {
            string[] lstr_Resultado = new string[2];
            lstr_Resultado[0] = "99";
           
            string lstr_Fecha = clsSesion.Current.FechaUltimaConsulta;
            DateTime lstr_FechaModificado = Convert.ToDateTime(lstr_Fecha);
            lstr_Fecha = lstr_FechaModificado.ToString("yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture);
            lstr_Resultado = ws_SGService.uwsActualizarObservacionesRevCont(str_IdRevCont, str_IdSociedadGL, str_TipoProceso,
                str_Observacion, str_UsrModifica, lstr_Fecha);
            if (lstr_Resultado[0] == "00")
                MessageBox.Show("Los datos se han actualizado");
            else
                MessageBox.Show("Error al realizar la operación");
            
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            lst_IdRevelacion = Request.QueryString["Rev"];
            lst_Ministerio = Request.QueryString["Min"];
            lst_Tipo = Request.QueryString["Tip"];
            string lstr_Observaciones = txtObservaciones.Text;
            ActualizarObservacion(lst_IdRevelacion, lst_Ministerio, lst_Tipo, lstr_Observaciones, str_Usuario);
        }

        protected void btnAtras_Click(object sender, EventArgs e)
        {
            lst_IdRevelacion = Request.QueryString["Rev"];
            Response.Redirect(String.Format("~/RevelacionNotas/Contingencias/InformeNota.aspx?Rev={0}", lst_IdRevelacion)); 
        }

    }
}