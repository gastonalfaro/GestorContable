using Presentacion.Compartidas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Mantenimiento.Gestiones
{
    public partial class frmNuevaReserva : BASE
    {
        # region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();

        private char gchr_MensajeError;
        private char gchr_MensajeExito;

        private string gstr_Usuario = String.Empty;

        private String[] garr_Modulos;
        private String[] garr_Modulo_Unico;

        //static DataSet gds_DetalleReservas = new DataSet();
        protected DataSet gds_DetalleReservas
        {
            get
            {
                if (ViewState["gds_DetalleReservas"] == null)
                    ViewState["gds_DetalleReservas"] = new DataSet();
                return (DataSet)ViewState["gds_DetalleReservas"];
            }
            set
            {
                ViewState["gds_DetalleReservas"] = value;
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
                    if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmParametros"))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                    {

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

        protected void btnVolverRevelaciones_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Mantenimiento/frmReservas.aspx", false);
        }

        protected void btnCrearReserva_Click(object sender, EventArgs e)
        {
            String[] str_result = new String[3];
            
            //str_result = ws_SGService.uwsCrearReservas( txtIdReserva.Text, txtIdEntidadCP.Text,
            //    txtIdSociedadFi.Text, txtIdCuentaContable);


            if (str_result[0].ToString().Equals("00") || str_result[0].ToString().Equals("True"))
            {
                MostarMensaje("Se creó el nuevo parámetro correctamente.", gchr_MensajeExito);
            }
            else
            {
                MostarMensaje("Error al crear el nuevo parámetro.", gchr_MensajeError);
            }
        }

        private void ConsultarModulos(string str_IdModulo, string str_NomModulo)
        {
            //    gds_DetalleReservas = ws_SGService.uwsConsultarModulos(str_IdModulo, str_NomModulo);

            //    if (gds_DetalleReservas.Tables["Table"].Rows.Count > 0)
            //    {
            //        ddlModulo.DataSource = gds_DetalleReservas.Tables["Table"];
            //        ddlModulo.DataTextField = "NomModulo";
            //        ddlModulo.DataValueField = "IdModulo";
            //        ddlModulo.DataBind();
            //    }
            //    else
            //    {
            //        ddlModulo.DataSource = this.LlenarDetalleReserva();
            //        ddlModulo.DataBind();
            //    }
        }

        private DataTable LlenarDetalleReserva()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("Posicion", typeof(string));
            ldt_TablaVacia.Columns.Add("Detalle", typeof(string));
            ldt_TablaVacia.Columns.Add("IdPosPre", typeof(string));
            ldt_TablaVacia.Columns.Add("IdCentroGestor", typeof(string));
            ldt_TablaVacia.Columns.Add("IdFondo", typeof(string));
            ldt_TablaVacia.Columns.Add("Segmento", typeof(string));
            ldt_TablaVacia.Columns.Add("IdPrograma", typeof(string));
            ldt_TablaVacia.Columns.Add("IdCuentaContable", typeof(string));
            ldt_TablaVacia.Columns.Add("IdCentroCosto", typeof(string));
            ldt_TablaVacia.Columns.Add("IdElementoPEP", typeof(string));
            ldt_TablaVacia.Columns.Add("IdMoneda", typeof(string));
            ldt_TablaVacia.Columns.Add("Monto", typeof(string));
            ldt_TablaVacia.Columns.Add("Bloqueado", typeof(string));
            ldt_TablaVacia.Columns.Add("Estado", typeof(string));
            DataRow ldr_FilaTabla = ldt_TablaVacia.NewRow();
            ldt_TablaVacia.Rows.Add(ldr_FilaTabla);
            return ldt_TablaVacia;
        }


        private void MostarMensaje(string str_TextMensaje, char chr_tipo)
        {
            if (chr_tipo == '1')
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

    }
}