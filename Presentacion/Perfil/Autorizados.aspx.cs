using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Presentacion.Compartidas;
using System.Data;
using DRIVER_XML_XML_XML;
using System.Threading;

namespace Presentacion.Perfil
{
    public partial class Autorizados : BASE
    {
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        public wrTributa.OrigenConsulta qry_Origen = new wrTributa.OrigenConsulta();
        public wrTributa.Service2 srv_Tributacion = new wrTributa.Service2();
        private wsCaptura.wsCapturaIngreso wsCapturaIngresos = new wsCaptura.wsCapturaIngreso();
        //public DataTable tbl_PersonaTramite = new DataTable();

        protected DataTable tbl_PersonaTramite
        {
            get
            {
                if (ViewState["tbl_PersonaTramite"] == null)
                    ViewState["tbl_PersonaTramite"] = new DataTable();
                return (DataTable)ViewState["tbl_PersonaTramite"];
            }
            set
            {
                ViewState["tbl_PersonaTramite"] = value;
            }
        }
        protected DataTable ldt_AutorizadosEmpresa
        {
            get
            {
                if (ViewState["ldt_AutorizadosEmpresa"] == null)
                    ViewState["ldt_AutorizadosEmpresa"] = new DataTable();
                return (DataTable)ViewState["ldt_AutorizadosEmpresa"];
            }
            set
            {
                ViewState["ldt_AutorizadosEmpresa"] = value;
            }
        }
        
        private string lst_IdEmpresa = String.Empty;
        private string str_Usuario = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            str_Usuario = clsSesion.Current.LoginUsuario;
            if (String.IsNullOrEmpty(str_Usuario))
            {

                Response.Redirect("~/Login.aspx", true);
            }
            else
            {

                if (!IsPostBack)
                {
                    if (Request.QueryString["Empresa"] != null)
                    {
                        lst_IdEmpresa = Request.QueryString["Empresa"];
                        ConsultarAutorizados(lst_IdEmpresa);
                    }
                }
            }
        }
        //Posible error de activacion de botones
        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            string lstr_TextoBoton = btnNuevo.Text;
            
            if (String.Equals(lstr_TextoBoton, "Nuevo"))
            {
                btnNuevo.Text = "Ocultar";
                txtIdentificacion.ReadOnly = false;
                txtIdentificacionAutoriza.Text = "";
                lblNombre.Text = "";
                ddlTipoIdentificacion.Enabled = true;
                pnlNuevoAutorizado.Visible = true;
            }
            else
            {
                txtIdentificacion.Text = String.Empty;
                txtCuentaCliente.Text = String.Empty;
                txtPuestoPersona.Text = String.Empty;
                btnNuevo.Text = "Nuevo";
                txtIdentificacion.Text = String.Empty;
                pnlNuevoAutorizado.Visible = false;
            }
        }

        protected void btnCrearAutorizado_Click(object sender, EventArgs e)
        {
            //ESTE PROCESO LO REALIZA UN HILO
        }//FUNCION

        protected void crear_autorizado()
        {
            if (String.IsNullOrEmpty(lblNombre.Text))
            {
                this.txtIdentificacion_TextChanged(null,null);
            }
            if (!String.IsNullOrEmpty(lblNombre.Text))
            {
                string idCuenta = txtCuentaCliente.Text;

                //nuevo DTR
                if (chkHabilitado.Checked)
                {
                    if (idCuenta.Length != 22)
                    {
                        MessageBox.Show("El tamaño de la cuenta IBAN debe ser de 22 caracteres.");
                        return;
                    }
                    else
                    {
                        Bccr.Cuentas.Negocio.CuentasIban.Validacion.IbanNacional iban = new Bccr.Cuentas.Negocio.CuentasIban.Validacion.IbanNacional(idCuenta);
                        if (!iban.EsValida())
                        {
                            MessageBox.Show("La cuenta IBAN no es válida.");
                            return;
                        }
                    }
                }

                //viejo DTR
                //    int idCuentaTama = idCuenta.Length;
                //if (idCuentaTama != 17 && idCuentaTama != 22)
                //{
                //    MessageBox.Show("El tamaño de la cuenta debe ser de 17 ó 22 caracteres.");
                //    return;
                //}
                //else
                //{
                //    if (idCuentaTama == 17)
                //    {
                //        if (wsCapturaIngresos.uwsCalcularDígitoVerificador(txtCuentaCliente.Text) == false)
                //        {
                //            MessageBox.Show("Cuenta No Válida.");
                //            return;
                //        }
                //    }
                //    else
                //    {
                //        //if (idCuentaTama == 22)
                //        //{
                //        //    //MessageBox.Show("Cuenta Válida.");
                //        //}
                //        //else
                //        //{
                //        //    MessageBox.Show("Cuenta No Válida.");
                //        //}
                //    }

                //}

                string lstr_TipoId = ddlTipoIdentificacion.SelectedValue;
                string lstr_TipoIdAutoriza = (string.IsNullOrEmpty(ddlTipoIdentificacionAutoriza.SelectedValue) == true) ? clsSesion.Current.TipoIdUsuario : ddlTipoIdentificacionAutoriza.SelectedValue;
                string lstr_IdAutoriza = (string.IsNullOrEmpty(txtIdentificacionAutoriza.Text) == true) ? clsSesion.Current.LoginUsuario : txtIdentificacionAutoriza.Text;
                string lstr_IdAutorizado = txtIdentificacion.Text;
                string lstr_CuentaCliente = txtCuentaCliente.Text;
                string lstr_PuestoPersona = txtPuestoPersona.Text;
                string lstr_NombrePersona = this.lblNombre.Text;
                string lstr_CodResultado = String.Empty;
                string lstr_Mensaje = String.Empty;
                string lstr_Estado = "I";
                string[] lstr_ArregloRes = new string[2];
                if (!String.IsNullOrEmpty(Request.QueryString["Empresa"]) && ValidarCampos(lstr_TipoId, lstr_IdAutorizado, lstr_CuentaCliente))
                {
                    lst_IdEmpresa = Request.QueryString["Empresa"];

                    if (chkHabilitado.Checked)
                    {
                        lstr_Estado = "A";
                    }

                    lstr_ArregloRes = ws_SGService.uwsCrearEmpresaAutorizado(lst_IdEmpresa, lstr_TipoId, lstr_IdAutorizado, lstr_TipoIdAutoriza,
                        lstr_IdAutoriza, lstr_CuentaCliente, lstr_NombrePersona, lstr_PuestoPersona, lstr_Estado, clsSesion.Current.LoginUsuario);
                    if (String.Equals(lstr_ArregloRes[0], "True"))
                    {

                        ws_SGService.uwsRegistrarAccionBitacoraCo("SG", clsSesion.Current.IdSesion, "Cambio/Nuevo Autorizado", lst_IdEmpresa + " " + lstr_TipoId + " " + lstr_IdAutorizado + " " + lstr_CuentaCliente, clsSesion.Current.LoginUsuario, "Cambio/Nuevo Autorizado", "");
                        MessageBox.Show("Transacción procesada");
                        //Borar campos y ocultar panel           
                        txtIdentificacion.Text = String.Empty;
                        txtIdentificacionAutoriza.Text = String.Empty;
                        txtCuentaCliente.Text = String.Empty;
                        txtPuestoPersona.Text = String.Empty;
                        btnNuevo.Text = "Nuevo";
                        pnlNuevoAutorizado.Visible = false;
                        ConsultarAutorizados(lst_IdEmpresa);

                        #region RAMSES SALVAR XML
                        this.guardar_xml();
                        #endregion
                    }
                    else
                    {
                        MessageBox.Show("Error al realizar la transacción");
                    }
                }
            }
        }//FUNCION

        #region RAMSES FUNCIONES
        protected void guardar_xml()
        {
            #region RAMSES SALVAR XML
            XML__DRIVER driverXML = get_xml_driver_empty();
            String path_file = String.Format("C:\\inetpub\\wwwroot\\SistemaGestor\\XML\\{0} - {1} - {2}.xml", this.CEDULA_PERSONA_QUE_AUTORIZA.Value, Request.QueryString["Empresa"], id_pesona_a_quien_se_autorisa.Value);
            try
            {
                driverXML.MAKE_XML_FILE(path_file, this.txb_out.Value.Replace('°', '<').Replace('|', '>'));
            }
            catch (Exception ABC)
            {
                mtr_msg("ALERTA !!! " + ABC.Message);
            }
            #endregion
        }//FUNCION

        public XML__DRIVER get_xml_driver_empty()
        {
            PARA p = new PARA("", "");
            TRAMITA t = new TRAMITA("", "");
            INFO_PAGO i_p = new INFO_PAGO("", "", "", "");
            INFO_FORMULARIO i_f = new INFO_FORMULARIO(p, t, "", "", "", "", "", "", "", "", "", "");
            return new XML__DRIVER(i_f, i_p);
        }//FUNCION

        public void mtr_msg(String msg)
        {
            Response.Write(String.Format("<script>alert('{0}')</script>", msg));
        }//FUNCION
        #endregion

        private bool ValidarCampos(string str_TipoID, string str_IdAutorizado, string str_CuentaCliente)
        {
            bool lboo_Resultado = true;

            if(String.IsNullOrEmpty(str_TipoID) && String.IsNullOrEmpty(str_IdAutorizado)
                && String.IsNullOrEmpty(str_CuentaCliente))
            {
                lboo_Resultado = false;
            }


            string Mensaje = "";
            if (!this.ValidaLongitudCedula(str_TipoID, str_IdAutorizado, out Mensaje))
            {
                lboo_Resultado = false;
            }

            return lboo_Resultado;
        }


        private Boolean ValidaLongitudCedula(string str_tipo, string str_IdAutorizado, out string str_Mensaje)
        {
            Boolean lbl_resultado = true;
            str_Mensaje = "";
            if (str_tipo == "F" && str_IdAutorizado.Length != 10)
            {
                lbl_resultado = false;
                str_Mensaje = "Cédula debe tener 10 dígitos!";
            }
            if (str_tipo == "D" && str_IdAutorizado.Length != 12)
            {

                lbl_resultado = false;
                str_Mensaje = "Dimex debe tener 12 dígitos!";
            }
            if (str_tipo == "J" && str_IdAutorizado.Length != 20)
            {
                str_Mensaje = "Cédula Jurídica debe tener 20 dígitos!";
            }
            return lbl_resultado;
        }

        private void ConsultarAutorizados (string str_IdEmpresa)
        {
            DataSet lds_AutorizadosEmpresa = new DataSet();
            lds_AutorizadosEmpresa = ws_SGService.uwsConsultarEmpresasAutorizados(str_IdEmpresa, "", "", "", "", "", "");
            if (lds_AutorizadosEmpresa != null)
            {
                 //ldt_AutorizadosEmpresa = new DataTable();
                ldt_AutorizadosEmpresa = lds_AutorizadosEmpresa.Tables[0];
                if (ldt_AutorizadosEmpresa != null)
                {
                    if (ldt_AutorizadosEmpresa.Rows.Count > 0)
                    {
                        gvAutorizados.DataSource = ldt_AutorizadosEmpresa;
                        gvAutorizados.DataBind();
                        lblSinResultados.Visible = false;
                        gvAutorizados.Visible = true;
                    }
                    else
                    {
                        lblSinResultados.Visible = true;
                        gvAutorizados.Visible = false;
                    }
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Perfil/Empresas.aspx", true);
        }

        protected void gvAutorizados_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int int_indice = Convert.ToInt32(e.RowIndex);
                Label lblTipoIdPersonaAutorizada = (Label)gvAutorizados.Rows[int_indice].Cells[0].FindControl("lblTipoId");
                Label lblIdPersona = (Label)gvAutorizados.Rows[int_indice].Cells[1].FindControl("lblIdPersona");
                Label lblTipoIdPersonaAutoriza = (Label)gvAutorizados.Rows[int_indice].Cells[0].FindControl("lblTipoIdPersonaAutoriza");
                Label lblIdPersonaAutoriza = (Label)gvAutorizados.Rows[int_indice].Cells[1].FindControl("lblIdPersonaAutoriza");
                Label lblNombrePersona = (Label)gvAutorizados.Rows[int_indice].Cells[1].FindControl("lblNombrePersona");
                Label lblCtaCliente = (Label)gvAutorizados.Rows[int_indice].Cells[1].FindControl("lblCtaCliente");
                Label lblPuesto = (Label)gvAutorizados.Rows[int_indice].Cells[1].FindControl("lblPuesto");
                Label lblEstado = (Label)gvAutorizados.Rows[int_indice].Cells[1].FindControl("lblEstado");
                pnlNuevoAutorizado.Visible = true;
                try
                {
                    ddlTipoIdentificacion.SelectedValue = lblTipoIdPersonaAutorizada.Text.Trim();
                }
                catch
                {

                }
                txtIdentificacion.Text = lblIdPersona.Text; 
                try
                {
                    ddlTipoIdentificacionAutoriza.SelectedValue = lblTipoIdPersonaAutoriza.Text.Trim();
                }
                catch
                {

                }
                txtIdentificacionAutoriza.Text = lblIdPersonaAutoriza.Text;

                txtCuentaCliente.Text = lblCtaCliente.Text;
                txtPuestoPersona.Text = lblPuesto.Text;
                lblNombre.Text = lblNombrePersona.Text;
                txtIdentificacion.ReadOnly = true;
                ddlTipoIdentificacion.Enabled = false;
                if (String.Equals(lblEstado.Text.Trim(),"A"))
                {
                    chkHabilitado.Checked = true;
                }
                else
                {
                    chkHabilitado.Checked = false;
                }
                btnNuevo.Text = "Ocultar";
            }
            catch (Exception ex)
            {

            }
        }

        protected void txtIdentificacion_TextChanged(object sender, EventArgs e)
        {
            if (txtIdentificacion.Text != "" && !string.IsNullOrEmpty(ddlTipoIdentificacion.SelectedValue))
            {

                string Mensaje = "";
                if (!this.ValidaLongitudCedula(ddlTipoIdentificacion.SelectedValue.ToString(), txtIdentificacion.Text, out Mensaje))
                {
                    MessageBox.Show(Mensaje);
                }
                else
                {

                    if (ddlTipoIdentificacion.SelectedValue == "F")
                    {
                        qry_Origen = wrTributa.OrigenConsulta.Fisico;
                    }
                    else if (ddlTipoIdentificacion.SelectedValue == "J")
                    {
                        qry_Origen = wrTributa.OrigenConsulta.Juridico;
                    }
                    else
                    {
                        qry_Origen = wrTributa.OrigenConsulta.DIMEX;
                    }
                    ;

                    tbl_PersonaTramite = srv_Tributacion.ObtenerDatos(qry_Origen, txtIdentificacion.Text, "", "", "", "", "", "");//string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                    if (tbl_PersonaTramite.Rows.Count > 0)
                    {
                        if (ddlTipoIdentificacion.SelectedValue == "J")
                        {
                            lblNombre.Text = tbl_PersonaTramite.Select("1 = 1")[0]["NOMBRE"].ToString().Trim();
                        }
                        else
                        {
                            lblNombre.Text = tbl_PersonaTramite.Select("1 = 1")[0]["APELLIDO1"].ToString().Trim() + " " + tbl_PersonaTramite.Select("1 = 1")[0]["APELLIDO2"].ToString().Trim() + " " + tbl_PersonaTramite.Select("1 = 1")[0]["NOMBRE1"].ToString().Trim();
                        }

                    }
                    else
                    {
                        lblNombre.Text = "";
                        MessageBox.Show("Número de cédula inválido.");
                        return;

                    }
                }
            }
        }

        #region RAMSES FUNCIONES
        protected void funcion_listener_firma_digital()
        {
            while (this.h_listen_firma.Value != "C#234?9$#1$9238478rTXK") { }
            //mtr_msg("Se procedera con el Pago Respectivo !!!");
            //ES NECESARIO RESETEAR EL CAMPO SEGÚN LA LOGICA DEL FLUJO DE TRABAJO
            this.h_listen_firma.Value = "";
            //ACÁ CONTINUARIA CON EL CODIGO DEL PAGO Y TODA LA TRANSACCIÓN
            this.crear_autorizado();
        }//FUNCION

        protected void call_listener_firma_digital()
        {
            new Thread(this.funcion_listener_firma_digital).Start();
        }

        protected void btnCrearAutorizado_Click1(object sender, EventArgs e)
        {
            if (this.h_listen_firma.Value.Equals("C#234?9$#1$9238478rTXK"))
            {
                this.crear_autorizado();
                this.h_listen_firma.Value = "";
            }
            else
            {
                mtr_msg("No Se Procedera, Se Canceló La Operación De Firma Digital !!!");
            }
        }

        protected void ddlTipoIdentificacion_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (txtIdentificacion.Text != "" && !string.IsNullOrEmpty(ddlTipoIdentificacion.SelectedValue))
            {

                string Mensaje = "";
                if (!this.ValidaLongitudCedula(ddlTipoIdentificacion.SelectedValue.ToString(), txtIdentificacion.Text, out Mensaje))
                {
                    MessageBox.Show(Mensaje);
                }
                else
                {

                    if (ddlTipoIdentificacion.SelectedValue == "F")
                    {
                        qry_Origen = wrTributa.OrigenConsulta.Fisico;
                    }
                    else if (ddlTipoIdentificacion.SelectedValue == "J")
                    {
                        qry_Origen = wrTributa.OrigenConsulta.Juridico;
                    }
                    else
                    {
                        qry_Origen = wrTributa.OrigenConsulta.DIMEX;
                    }
                    ;

                    tbl_PersonaTramite = srv_Tributacion.ObtenerDatos(qry_Origen, txtIdentificacion.Text, "", "", "", "", "", "");//string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                    if (tbl_PersonaTramite.Rows.Count > 0)
                    {
                        if (ddlTipoIdentificacion.SelectedValue == "J")
                        {
                            lblNombre.Text = tbl_PersonaTramite.Select("1 = 1")[0]["NOMBRE"].ToString().Trim();
                        }
                        else
                        {
                            lblNombre.Text = tbl_PersonaTramite.Select("1 = 1")[0]["APELLIDO1"].ToString().Trim() + " " + tbl_PersonaTramite.Select("1 = 1")[0]["APELLIDO2"].ToString().Trim() + " " + tbl_PersonaTramite.Select("1 = 1")[0]["NOMBRE1"].ToString().Trim();
                        }

                    }
                    else
                    {
                        lblNombre.Text = "";
                        MessageBox.Show("Número de cédula inválido.");
                        return;

                    }
                }
            }
        }

        protected void ddlTipoIdentificacionAutoriza_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtIdentificacionAutoriza_TextChanged(object sender, EventArgs e)
        {

        }//FUNCION
        #endregion

    }//CLASE
}//namespace