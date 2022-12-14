using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Presentacion.Compartidas;
using DRIVER_XML_XML_XML;
using System.Threading;

namespace Presentacion.Perfil
{
    public partial class Empresas : BASE
    {
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        public wrTributa.OrigenConsulta qry_Origen = new wrTributa.OrigenConsulta();
        public wrTributa.Service2 srv_Tributacion = new wrTributa.Service2();
        public DataTable tbl_Persona = new DataTable();
        private string lst_IdEmpresa = String.Empty;
        //private tParametro lparametro = new tParametro();
        private string str_Usuario = String.Empty;
        //private bool gbol_ConFirmaDigital;
        protected Boolean gbol_ConFirmaDigital
        {
            get
            {
                if (ViewState["gbol_ConFirmaDigital"] == null)
                    ViewState["gbol_ConFirmaDigital"] = false;
                return Convert.ToBoolean(ViewState["gbol_ConFirmaDigital"]);
            }
            set
            {
                ViewState["gbol_ConFirmaDigital"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //this.call_listener_firma_digital();

            //clsSesion.Current.LoginUsuario = "0110370132";
            str_Usuario = clsSesion.Current.LoginUsuario;
            gbol_ConFirmaDigital = clsSesion.Current.gbol_FirmaDigital;
            if (String.IsNullOrEmpty(str_Usuario))
                Response.Redirect("~/Login.aspx", true);
            else
            {
                string str_Texto = string.Empty;
                DataSet ds_Parametro = ws_SGService.uwsConsultarParametros("ADVERTENCIA_EMP", "IdModulo = 'CI'", DateTime.Now, "", "");
                if (ds_Parametro.Tables.Count > 0 && ds_Parametro.Tables["Table"].Rows.Count > 0)
                {
                    str_Texto = ds_Parametro.Tables["Table"].Rows[0]["Valor"].ToString();
                }//if ds_Parametro

                if (!IsPostBack)
                {
                    string script = "confirmar(\"" + str_Texto + "\");";

                    ScriptManager.RegisterStartupScript(this, typeof(Page), "confirmar", script, true);
                }
            }
                if (!IsPostBack)
                    ConsultarEmpresas();

     

            if (gbol_ConFirmaDigital) {
                this.btnNuevaEmpresa.Visible = true;
                this.gvEmpresas.Enabled = true;
            }
            else {
                this.btnNuevaEmpresa.Visible = false;
                this.gvEmpresas.Enabled = false;
            }
        }//FUNCION

        protected void btnNuevaEmpresa_Click(object sender, EventArgs e)
        {
            string lstr_TextoBoton = btnNuevaEmpresa.Text;
            if (String.Equals(lstr_TextoBoton,"Nueva"))
            {
                txtIdentificacion.ReadOnly = false;
                btnNuevaEmpresa.Text = "Ocultar";
                pnlNuevaEmpresa.Visible = true;
            }
            else
            {
                btnNuevaEmpresa.Text = "Nueva";
                txtIdentificacion.Text = String.Empty;
                txtNombreEmp.Text = String.Empty;
                txtTelefonoEmp.Text = String.Empty;
                txtCorreoEmp.Text = String.Empty;
                pnlNuevaEmpresa.Visible = false;
            }
        }

        public void crear_empresa()
        {
            string lstr_IdEmpresa = txtIdentificacion.Text;
            string lstr_NombreEmpresa = txtNombreEmp.Text;
            string lstr_Telefono = txtTelefonoEmp.Text;
            string lstr_Correo = txtCorreoEmp.Text;
            string lstr_CodResultado = String.Empty;
            string lstr_Mensaje = String.Empty;
            txtIdentificacion.ReadOnly = false;
            txtNombreEmp.ReadOnly = true;

            try
            {

                if (ValidarCampos(lstr_IdEmpresa, lstr_NombreEmpresa, lstr_Telefono, lstr_Correo))
                {
                    ws_SGService.uwsCrearEmpresa(lstr_IdEmpresa, lstr_NombreEmpresa, lstr_Correo, lstr_Telefono, clsSesion.Current.TipoIdUsuario,
                        clsSesion.Current.LoginUsuario, clsSesion.Current.LoginUsuario, out lstr_CodResultado, out lstr_Mensaje);

                    if (String.Equals(lstr_CodResultado, "00"))
                    {

                        ws_SGService.uwsRegistrarAccionBitacoraCo("SG", clsSesion.Current.IdSesion, "Cambio/Nueva Empresa", lst_IdEmpresa + " " + lstr_NombreEmpresa, clsSesion.Current.LoginUsuario, "Cambio/Nueva Empresa", "");

                        MessageBox.Show(lstr_Mensaje);
                        txtIdentificacion.Text = String.Empty;
                        txtNombreEmp.Text = String.Empty;
                        txtTelefonoEmp.Text = String.Empty;
                        txtCorreoEmp.Text = String.Empty;
                        btnNuevaEmpresa.Text = "Nueva";
                        pnlNuevaEmpresa.Visible = false;
                        ConsultarEmpresas();
                        #region RAMSES SALVAR EL XML
                        this.guardar_xml();
                        #endregion
                    }
                    else
                    {
                        MessageBox.Show("Error al realizar la transación " + lstr_Mensaje);
                    }
                }
                else
                {
                    MessageBox.Show("Existen datos sin ingresar");
                }
            }
            catch
            {
                MessageBox.Show("Error al realizar la transación");
            }
        }//FUNCION


        #region RAMSES FUNCIONES
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


        private bool ValidarCampos(string str_IdEmpresa, string str_NombreEmpresa, string str_Telefono, string str_CorreoEmpresa)
        {
            bool lboo_Resultado = true;
            if(String.IsNullOrEmpty(str_IdEmpresa) || String.IsNullOrEmpty(str_NombreEmpresa) || String.IsNullOrEmpty(str_Telefono)
                || String.IsNullOrEmpty(str_CorreoEmpresa))
            {
                lboo_Resultado = false;
            }
            return lboo_Resultado;
        }

        protected void gvEmpresas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "autorizados")
                {
                    string index = Convert.ToString(e.CommandArgument.ToString());
                    string lstr_Url = String.Format("~/Perfil/Autorizados.aspx?Empresa={0}", index);
                    Response.Redirect(lstr_Url, false);
                }
            }
            catch (Exception ex)
            { }
        }

        private void ConsultarEmpresas ()
        {
            
            DataSet lds_Empresas = new DataSet();
            lds_Empresas = ws_SGService.uwsConsultarEmpresas("", "", "", "", clsSesion.Current.LoginUsuario);
            if (lds_Empresas != null)
            {
                DataTable ldt_Empresas = new DataTable();
                ldt_Empresas = lds_Empresas.Tables[0];
                if (ldt_Empresas != null)
                {
                    if (ldt_Empresas.Rows.Count > 0)
                    {
                        gvEmpresas.DataSource = ldt_Empresas;
                        gvEmpresas.DataBind();
                        lblSinResultados.Visible = false;
                        gvEmpresas.Visible = true;
                    }
                    else
                    {
                        lblSinResultados.Visible = true;
                        gvEmpresas.Visible = false;
                    }
                }
            }
        }

        protected void gvEmpresas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                int int_indice = Convert.ToInt32(e.RowIndex);
                Label lblIdEmpresa = (Label)gvEmpresas.Rows[int_indice].Cells[0].FindControl("lblIdEmpresa");
                Label lblNombreEmpresa = (Label)gvEmpresas.Rows[int_indice].Cells[4].FindControl("lblNombre");
                Label lblTelefono = (Label)gvEmpresas.Rows[int_indice].Cells[1].FindControl("lblTelefono");
                Label lblCorreo = (Label)gvEmpresas.Rows[int_indice].Cells[1].FindControl("lblCorreo");
                txtIdentificacion.Text = lblIdEmpresa.Text;
                txtNombreEmp.Text = lblNombreEmpresa.Text;
                txtTelefonoEmp.Text = lblTelefono.Text;
                txtCorreoEmp.Text = lblCorreo.Text;
                btnNuevaEmpresa.Text = "Ocultar";
                txtIdentificacion.ReadOnly = true;
                txtNombreEmp.ReadOnly = true;
                pnlNuevaEmpresa.Visible = true;
            }
            catch (Exception ex)
            {

            }
        }

        protected void txtIdentificacion_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdentificacion.Text))
            {

                qry_Origen = wrTributa.OrigenConsulta.Juridico;


                tbl_Persona = srv_Tributacion.ObtenerDatos(qry_Origen, txtIdentificacion.Text, "", "", "", "", "", "");//string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                if (tbl_Persona.Rows.Count > 0)
                {


                    txtNombreEmp.Text = tbl_Persona.Select("1 = 1")[0]["NOMBRE"].ToString().Trim();

                }
                else
                {
                    txtNombreEmp.Text = "";
                    MessageBox.Show("Número de cédula inválido.");
                }
            }
        }


        #region RAMSES FUNCIONES
        protected void guardar_xml()
        {
            #region RAMSES SALVAR EL XML
            XML__DRIVER driverXML = get_xml_driver_empty();
            String path_file = String.Format("C:\\inetpub\\wwwroot\\SistemaGestor\\XML\\{0} - {1}.xml", this.id_empresa.Value, this.cedula_persona.Value);
            //String path_file = String.Format("C:\\Windows\\Temp\\{0} - {1}.xml", this.id_empresa.Value, this.cedula_persona.Value);
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
        protected void funcion_listener_firma_digital()
        {
            while (this.h_listen_firma.Value != "C#234?9$#1$9238478rTXK") { }
            //mtr_msg("Se procedera con el Pago Respectivo !!!");
            //ES NECESARIO RESETEAR EL CAMPO SEGÚN LA LOGICA DEL FLUJO DE TRABAJO
            this.h_listen_firma.Value = "";
            //ACÁ CONTINUARIA CON EL CODIGO DEL PAGO Y TODA LA TRANSACCIÓN
            this.crear_empresa();
        }//FUNCION

        protected void call_listener_firma_digital()
        {
            new Thread(this.funcion_listener_firma_digital).Start();
        }

        protected void btnCrearEmpresa_Click1(object sender, EventArgs e)
        {
            if (this.h_listen_firma.Value.Equals("C#234?9$#1$9238478rTXK"))
            {
                this.crear_empresa();
                this.h_listen_firma.Value = "";
            }
            else
            {
                mtr_msg("No Se Procederá: Se Canceló La Operación De Firma Digital !!!");
            }
        }//FUNCION
        #endregion


    }//CLASE
}//namespace