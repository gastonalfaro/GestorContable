using Presentacion.Compartidas;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
//using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Presentacion.Contingentes
{
    public partial class ResolucionesConsultar : BASE
    {
        private string gstr_expedienteId = string.Empty;
        private GridView grdResoluciones = new GridView();
        private string lstr_idResolucion = string.Empty;
        private string lstr_Consulta = string.Empty;
        private GridView grdCobrosPagos = new GridView();
        private string gstr_Usuario = String.Empty;
        private String gstr_IdResExp = String.Empty;
        private String gstr_IdSociedad = String.Empty;
        private String idres_anterior = String.Empty;
        private String gstr_IdRes = String.Empty;

        //Variable reference de servicio web 
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        protected void Page_Load(object sender, EventArgs e)
        {
            gstr_Usuario = clsSesion.Current.LoginUsuario;
            gstr_IdSociedad = clsSesion.Current.SociedadUsr;
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(gstr_Usuario))
                {
                    if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "OBJ_CT"))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                    {
                        CargaInicial(); 
                    }
                }
                else
                {
                    MessageBox.Show("Sesión de usuario finalizó.");
                    Response.Redirect("~/Login.aspx", true);
                }
            }
            else
            {

                if (String.IsNullOrEmpty(gstr_Usuario))
                {
                    MessageBox.Show("Sesión de usuario finalizó.");
                    Response.Redirect("~/Login.aspx", true);
                }
            }

        }

        /// <summary>
        /// Carga incial de los componentes
        /// </summary>
        private void CargaInicial() 
        {
            String Resoluciones_query = string.Format("SELECT * FROM co.Expedientes exp WHERE exp.IdSociedadGL='{0}' AND exp.EstadoExpediente ='Activo' and exp.idexpediente in ( select idexpedientefk from co.Resoluciones)", gstr_IdSociedad);
            grdExpedientes.DataSource = GetData(Resoluciones_query);
            grdExpedientes.DataBind();
            grdResoluciones = FindControl("grdResoluciones") as GridView;
        }

        //protected void gvResoluciones_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    if (e.CommandName == "Modificar")
        //    {
        //        int id = Convert.ToInt32(grdResoluciones.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);
        //        Response.Redirect("~/Contingentes/Resoluciones.aspx?id=" + lstr_idResolucion + "&isAdd=false");
        //        //......
        //    } if (e.CommandName == "DeclaradoSinLugar")
        //    { }
        //}

        private void DetailsView1_ItemUpdated(object sender, DetailsViewUpdatedEventArgs e){

        grdResoluciones.DataBind();
        //DropDownList1.DataBind()
        }

        private DataTable GetData(string lstr_query)
        {
            /*string lstr_ConnString = ConfigurationManager.ConnectionStrings["GestNICSPDEVConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(lstr_ConnString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = lstr_query;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataSet ds = new DataSet())
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }*/
            DataSet ds = new DataSet();
            ds = ws_SGService.uwsConsultarDinamico(lstr_query);
            if (ds.Tables.Count > 0)
            {
                return ds.Tables["Table"];
            }
            return null;
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarDatosExpedientes();
        }

        private void CargarDatosExpedientes()
        {
            string numeroExp = this.txtNumExp.Text;
            //string fechaInicio = calDesde.SelectedDate.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            //string fechaFin = calHasta.SelectedDate.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

            DataSet lds_Expedientes = new DataSet();

            if (!String.IsNullOrEmpty(numeroExp))
            {
                lds_Expedientes = ws_SGService.uwsConsultarExpedienteXNumero(numeroExp, clsSesion.Current.SociedadUsr);
            }
            else if (!String.IsNullOrEmpty(this.txtFechaDesde.Text) && !String.IsNullOrEmpty(this.txtFechaHasta.Text))
            {
                string fechaInicio = this.txtFechaDesde.Text;
                string fechaFin = this.txtFechaHasta.Text;

                lds_Expedientes = ws_SGService.uwsConsultarExpedienteXFecha(fechaInicio, fechaFin, clsSesion.Current.SociedadUsr);
            }
            else
            {
                lds_Expedientes = ws_SGService.uwsConsultarExpediente(clsSesion.Current.SociedadUsr);
            }

            // grdExpedientes.Attributes.Keys = "";
            if (lds_Expedientes.Tables.Count != 0)
            {
                //Llenamos grid de expedientes
                if (lds_Expedientes.Tables["Table"].Rows.Count > 0)
                {
                    grdExpedientes.DataSource = lds_Expedientes; //lds_Expedientes.Tables["Table"];
                    grdExpedientes.DataBind();
                }
                else
                {
                    grdExpedientes.DataSource = lds_Expedientes; //this.LlenarTablaVacia();
                    grdExpedientes.DataBind();
                    //grdExpedientes.Rows[0].Visible = false;
                }
            }
         

            // foreach(grdExpedientes.)
        }

        protected void grdExpedientes_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                grdResoluciones = e.Row.FindControl("grdResoluciones") as GridView;
                gstr_expedienteId = grdExpedientes.DataKeys[e.Row.RowIndex].Value.ToString();
                //gstr_IdResExp = grdExpedientes.DataKeys[e.Row.RowIndex].Value.ToString();
                String Resoluciones_query = string.Format("SELECT *,res.EstadoProcesal as EstadoProcesalNom FROM co.Expedientes exp INNER JOIN co.Resoluciones res ON exp.IdExp = res.IdExp WHERE exp.IdExpediente ='{0}' AND exp.IdSociedadGL='{1}' AND exp.EstadoExpediente ='Activo'", gstr_expedienteId, gstr_IdSociedad);
                grdResoluciones.DataSource = GetData(Resoluciones_query);

                grdResoluciones.DataBind();
            }
        }

        protected void grdResoluciones_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }
        protected void grdResoluciones_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                grdCobrosPagos = e.Row.FindControl("grdCobrosPagos") as GridView;
                //string gstr_resolucionId = grdResoluciones.DataKeys[e.Row.RowIndex].Value.ToString();
                //CargarCobrosPagos(gstr_resolucionId);

                string gstr_IdRes = grdResoluciones.DataKeys[e.Row.RowIndex].Value.ToString();

                string gstr_Estado = grdResoluciones.DataKeys[e.Row.RowIndex].Values[1].ToString();
                CargarCobrosPagos(gstr_IdRes, gstr_Estado);
            }
        }


        protected void grdResoluciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Modificar")
            {
                lstr_idResolucion = e.CommandArgument.ToString();
                lstr_Consulta = "SELECT * FROM co.Resoluciones WHERE IdResolucion='" + lstr_idResolucion + "' AND IdSociedadGL='" + gstr_IdSociedad + "'";
                GetData(lstr_Consulta);
            }
        }

        

        protected void CargarResoluciones(string idRes)
        {

            grdResoluciones.DataSource = GetData("SELECT IdResolucion, IdExpedienteFK, EstadoResolucion, FechResolucion, PosibleFechSalidaRecursos, MontoPosibleReembolso, MontoPosibleReembolsoColones, Observacion " +
                "FROM co.Resoluciones  where co.Resoluciones.IdRes='" + idRes + "' " +
                "co.Resoluciones.IdSociedadGL='" + gstr_IdSociedad + "'");
            grdResoluciones.DataBind();
        }

        protected void CargarCobrosPagos(string idRes, string gstr_Estado)
        {
            if (!gstr_Estado.Equals("Liquidacion"))
            {
                grdCobrosPagos.DataSource = GetData(string.Format("SELECT Moneda, TipoCambio, MontoPrincipal,MontoPrincipalColones, MontoIntereses,MontoInteresesColones " +
                    "FROM  co.CobrosPagos cp Where cp.IdCobroPagoResolucion = (SELECT max (CP1.IdCobroPagoResolucion) FROM co.CobrosPagos CP1 " +
                       "WHERE CP1.Moneda != 'REV' AND CP1.IdRes = '" + idRes + "') AND cp.IdRes ='{0}' AND cp.TipoTransaccion != 'tipotra' ", idRes));
                grdCobrosPagos.DataBind();
            }
            else
            {
                grdCobrosPagos.DataKeyNames = new String[6] { "Moneda", "TipoCambio", "Intereses", "InteresesMoratorios", "Costas", "DanoMoral" };

                grdCobrosPagos.DataSource = GetData(string.Format("SELECT Moneda, TipoCambio, Intereses, InteresesMoratorios ,Costas, DanoMoral " + 
                "FROM  co.CobrosPagos cp Where cp.IdCobroPagoResolucion = (SELECT max (CP1.IdCobroPagoResolucion) FROM co.CobrosPagos CP1 "+
                "WHERE CP1.Moneda != 'REV' AND CP1.IdRes = '" + idRes + "') AND cp.IdRes ='{0}' AND cp.TipoTransaccion != 'tipotra' ", idRes));
                grdCobrosPagos.DataBind();
            }
            
        }

        protected void DeclararSinLugar(string idExpediente,string tipoExpediente,string estadoResolucion,int CxCxCxP,string Sociedad,string userCrea)
        {
            string lstr_Estado = "Sin lugar";
            string[] lstr_respuesta = new string[2];
            //string lstr_idExpediente = ((LinkButton)sender).CommandArgument.ToString();
            //lstr_idExpediente = this.grdExpedientes.SelectedDataKey.Value.ToString();

            lstr_respuesta = ws_SGService.uwsDeclararSinLugarResolucion(idExpediente, tipoExpediente, estadoResolucion, CxCxCxP,Sociedad, userCrea);
            //Validamos resultado para desplegar mensaje al usuario
            if (lstr_respuesta[0].Contains("00"))
            {

                //string strMsg = "Se declaro sin lugar el expediente.";
                //Response.Write("<script>alert('" + strMsg + "')</script>");
                MessageBox.Show("Se declaró sin lugar la resolución. " + lstr_respuesta[0]);

            }
            else if (lstr_respuesta[0].Contains("99") || lstr_respuesta[0].Contains("Codigo :-") || lstr_respuesta[0].Contains("Codigo:") || lstr_respuesta[0].Contains("Codigo :") || lstr_respuesta[0].Contains("Codigo :-6"))
            {

                //string strMsg = "No se pudó, declarar sin lugar el expediente.";
                //Response.Write("<script>alert('" + strMsg + "')</script>");

                MessageBox.Show("No se pudo declarar sin lugar la resolución." + lstr_respuesta[0]);
            }

        }

        protected void btn_Modificar_Click(object sender, EventArgs e)
        {
        }

        protected void grdExpedientes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdExpedientes.PageIndex = e.NewPageIndex;
            CargaInicial();
        }

        
    }      
}