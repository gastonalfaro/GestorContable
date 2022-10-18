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
using LogicaNegocio;

namespace Presentacion.Mantenimiento.Gestiones
{
    public partial class frmNuevoTipoAsiento : BASE
    {
        # region Variables
        private LogicaNegocio.Seguridad.tObjeto gObjeto = new LogicaNegocio.Seguridad.tObjeto();
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private char gchr_MensajeError;
        private char gchr_MensajeExito;
        private string gstr_Usuario = String.Empty;
        private string gstr_ModuloActual = String.Empty;
        private String[] garr_Modulos;
        DataSet dsCentroCosto;
        DataSet dsCentroBeneficio;
        DataSet dsElementoPEP;
        DataSet dsPosPre;
        DataSet dsCentroGestor;
        DataSet dsFondo;


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
        # endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            
            gstr_Usuario = clsSesion.Current.LoginUsuario;
            gstr_ModuloActual = clsSesion.Current.gstr_ModuloActual;
            gchr_MensajeError = clsSesion.Current.chr_MensajeError;
            gchr_MensajeExito = clsSesion.Current.chr_MensajeExito;

            if (!IsPostBack)
            {
                garr_Modulos = clsSesion.Current.PermisosModulos;
                dsCentroCosto = new DataSet();
                dsCentroBeneficio = new DataSet();
                dsCentroGestor = new DataSet();
                dsElementoPEP = new DataSet();
                dsPosPre = new DataSet();
                dsFondo = new DataSet();

                //Entidad CP
                this.ddlEntidadCP.DataSource = ws_SGService.uwsConsultarEntidadesCP("", "", "");
                this.ddlEntidadCP.DataBind();
                this.ddlEntidadCP.Items.Insert(0, new ListItem("-Seleccione Opción-", ""));
                //Sociedad CO
                this.ddlSociedadCO.DataSource = ws_SGService.uwsConsultarSociedadesCosto("", "");
                this.ddlSociedadCO.DataBind();
                this.ddlSociedadCO.Items.Insert(0, new ListItem("-Seleccione Opción-", ""));
                //Sociedad FI
                this.ddlSociedadFi.DataSource = ws_SGService.uwsConsultarSociedadesFinancieras("", "","", "", "","");
                this.ddlSociedadFi.DataBind();
                this.ddlSociedadFi.Items.Insert(0, new ListItem("-Seleccione Opción-", ""));
                //Modulos
                List<string> vModulos = new List<string>();
                foreach (string vDato in garr_Modulos)
                    if(vDato != null)
                        vModulos.Add(vDato);
                this.ddlModulos.DataSource = vModulos;
                this.ddlModulos.DataBind();
                this.ddlModulos.Items.Insert(0, new ListItem("-Seleccione Opción-", ""));

                CargarDatos();

                if (!string.IsNullOrEmpty(gstr_Usuario))
                {
                    if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, ""))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                    {
                        CargarModulos();
                        CargarClasesDocumento();
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

        protected void btnCrearParametro_Click(object sender, EventArgs e)
        {
            String[] str_result = new String[3];
            
            str_result = ws_SGService.uwsCrearParametro("", Convert.ToDateTime(DateTime.Today), "", "", "", "", gstr_Usuario);
            if (str_result[0].ToString().Equals("00"))
            {
                MostarMensaje(str_result[1].ToString(), gchr_MensajeError);
            }
            else
            {
                MostarMensaje(str_result[1].ToString(), gchr_MensajeExito);
            }
            
        }

        private void CargarDatos()
        {
            dsCentroCosto = ws_SGService.uwsConsultarCentrosCosto(string.Empty, Convert.ToDateTime("01/01/1900"), this.ddlSociedadCO.SelectedValue, ddlSociedadFi.SelectedValue, DateTime.Today, string.Empty, string.Empty);
            dsCentroBeneficio = ws_SGService.uwsConsultarCentrosBeneficio(string.Empty, Convert.ToDateTime("01/01/1900"), this.ddlSociedadCO.SelectedValue, ddlSociedadFi.SelectedValue, DateTime.Today, string.Empty, string.Empty);
            dsElementoPEP = ws_SGService.uwsConsultarElementosPEP(string.Empty, string.Empty);
            dsCentroGestor = ws_SGService.uwsConsultarCentrosGestores(string.Empty, Convert.ToDateTime("01/01/1900"), this.ddlEntidadCP.SelectedValue, this.ddlSociedadFi.SelectedValue, DateTime.Today, string.Empty, string.Empty);
            dsPosPre = ws_SGService.uwsConsultarPosicionesPresupuestarias(string.Empty, this.ddlEntidadCP.SelectedValue, DateTime.Now.Year.ToString(), string.Empty, string.Empty);
            dsFondo = ws_SGService.uwsConsultarFondos(string.Empty, this.ddlEntidadCP.SelectedValue, string.Empty, string.Empty);
            
            ddlCentroBeneficio1.Items.Clear();
            ddlCentroBeneficio2.Items.Clear();
            ddlCentroCosto1.Items.Clear();
            ddlCentroCosto2.Items.Clear();
            ddlCentroGestor1.Items.Clear();
            ddlCentroGestor2.Items.Clear();
            ddlElementoPEP1.Items.Clear();
            ddlElementoPEP2.Items.Clear();
            ddlFondo1.Items.Clear();
            ddlFondo2.Items.Clear();
            ddlPosPre1.Items.Clear();
            ddlPosPre2.Items.Clear();

            ListItem vli = new ListItem("-Seleccione Opción-", "");
            //Costos
            foreach (DataRow vFila in dsCentroCosto.Tables[0].Rows) 
            {
                ddlCentroCosto1.Items.Add(new ListItem(vFila["IdCentroCosto"].ToString() + "-" + vFila["NomCentroCosto"].ToString(), vFila["IdCentroCosto"].ToString()));
                ddlCentroCosto2.Items.Add(new ListItem(vFila["IdCentroCosto"].ToString() + "-" + vFila["NomCentroCosto"].ToString(), vFila["IdCentroCosto"].ToString()));
            }
            this.ddlCentroCosto1.Items.Insert(0,vli);
            this.ddlCentroCosto2.Items.Insert(0, vli);
           
            //Beneficios
            foreach (DataRow vFila in dsCentroBeneficio.Tables[0].Rows)
            {
                ddlCentroBeneficio1.Items.Add(new ListItem(vFila["IdCentroBeneficio"].ToString() + "-" + vFila["NomCentroBeneficio"].ToString(), vFila["IdCentroBeneficio"].ToString()));
                ddlCentroBeneficio2.Items.Add(new ListItem(vFila["IdCentroBeneficio"].ToString() + "-" + vFila["NomCentroBeneficio"].ToString(), vFila["IdCentroBeneficio"].ToString()));
            }
            this.ddlCentroBeneficio1.Items.Insert(0,vli);
            this.ddlCentroBeneficio2.Items.Insert(0, vli);
           
            //Elementos PEP
            foreach (DataRow vFila in dsElementoPEP.Tables[0].Rows)
            {
                ddlElementoPEP1.Items.Add(new ListItem(vFila["IdElementoPEP"].ToString() + "-" + vFila["NomElementoPEP"].ToString(), vFila["IdElementoPEP"].ToString()));
                ddlElementoPEP2.Items.Add(new ListItem(vFila["IdElementoPEP"].ToString() + "-" + vFila["NomElementoPEP"].ToString(), vFila["IdElementoPEP"].ToString()));
            }
            this.ddlElementoPEP1.Items.Insert(0, vli);
            this.ddlElementoPEP2.Items.Insert(0, vli);
            
            //Posición Presupuestaria
            foreach (DataRow vFila in dsPosPre.Tables[0].Rows)
            {
                ddlPosPre1.Items.Add(new ListItem(vFila["IdPosPre"].ToString() + "-" + vFila["NomPosPre"].ToString(), vFila["IdPosPre"].ToString()));
                ddlPosPre2.Items.Add(new ListItem(vFila["IdPosPre"].ToString() + "-" + vFila["NomPosPre"].ToString(), vFila["IdPosPre"].ToString()));
            }
            this.ddlPosPre1.Items.Insert(0, vli);
            this.ddlPosPre2.Items.Insert(0, vli);
            
            //Centro Gestor
            foreach (DataRow vFila in dsCentroGestor.Tables[0].Rows)
            {
                ddlCentroGestor1.Items.Add(new ListItem(vFila["IdEntidadCP"].ToString() + " " + vFila["IdSociedadFi"].ToString() + " "+ vFila["NomCentroGestor"].ToString(), vFila["IdCentroGestor"].ToString()));
                ddlCentroGestor2.Items.Add(new ListItem(vFila["IdEntidadCP"].ToString() + " " + vFila["IdSociedadFi"].ToString() + " " + vFila["NomCentroGestor"].ToString(), vFila["IdCentroGestor"].ToString()));
            }
           
            this.ddlCentroGestor1.Items.Insert(0, vli);
            this.ddlCentroGestor2.Items.Insert(0,vli);
            
            //Fondo
            foreach (DataRow vFila in dsFondo.Tables[0].Rows)
            {
                ddlFondo1.Items.Add(new ListItem(vFila["IdFondo"].ToString() + "-" + vFila["NomFondo"].ToString(), vFila["IdFondo"].ToString()));
                ddlFondo2.Items.Add(new ListItem(vFila["IdFondo"].ToString() + "-" + vFila["NomFondo"].ToString(), vFila["IdFondo"].ToString()));
            }
            this.ddlFondo1.Items.Insert(0, new ListItem("-Seleccione Opción-", ""));
            this.ddlFondo2.Items.Insert(0, new ListItem("-Seleccione Opción-", ""));
   

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
        private void CargarClasesDocumento()
        {
            gds_ClasesDocumento = ws_SGService.uwsConsultarClasesDocumento("", "");
            if (gds_Modulos.Tables["Table"].Rows.Count > 0)
            {
                //ddlIdClaseDoc.DataTextField = "NomClaseDoc";
                //ddlIdClaseDoc.DataValueField = "IdClaseDoc";
                //ddlIdClaseDoc.DataSource = gds_ClasesDocumento.Tables["Table"];
                //ddlIdClaseDoc.DataBind();
            }
            else
            {
                //ddlIdClaseDoc.DataTextField = "NomClaseDoc";
                //ddlIdClaseDoc.DataValueField = "IdClaseDoc";
                //ddlIdClaseDoc.DataSource = this.LlenarTablaVaciaClasesDocumento();
                //ddlIdClaseDoc.DataBind();
            }

        }

        private DataTable LlenarTablaVaciaClasesDocumento()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("IdClaseDoc", typeof(string));
            ldt_TablaVacia.Columns.Add("NomClaseDoc", typeof(string));
            ldt_TablaVacia.Columns.Add("Estado", typeof(string));
            DataRow ldr_FilaTabla = ldt_TablaVacia.NewRow();
            ldt_TablaVacia.Rows.Add(ldr_FilaTabla);
            return ldt_TablaVacia;
        }

        private void CargarModulos()
        {
           
            gds_Modulos = ws_SGService.uwsConsultarModulos("", "");
            if (gds_Modulos.Tables["Table"].Rows.Count > 0)
            {
                //ddlIdModulo.DataTextField = "NomModulo";
                //ddlIdModulo.DataValueField = "IdModulo";
                //ddlIdModulo.DataSource = gds_Modulos.Tables["Table"];
                //ddlIdModulo.DataBind();
            }
            else
            {
                //ddlIdModulo.DataTextField = "NomModulo";
                //ddlIdModulo.DataValueField = "IdModulo";
                //ddlIdModulo.DataSource = this.LlenarTablaVaciaModulo();
                //ddlIdModulo.DataBind();
            }

        }

        private DataTable LlenarTablaVaciaModulo()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("IdModulo", typeof(string));
            ldt_TablaVacia.Columns.Add("NomModulo", typeof(string));
            ldt_TablaVacia.Columns.Add("Estado", typeof(string));
            DataRow ldr_FilaTabla = ldt_TablaVacia.NewRow();
            ldt_TablaVacia.Rows.Add(ldr_FilaTabla);
            return ldt_TablaVacia;
        }

        protected void btnCrearOperacion_Click(object sender, EventArgs e)
        {
            String[] str_result = new String[3];
            
            str_result = null;// ws_SGService.uwsCrearOperacion(txtIdOperacion.Text, ddlIdModulo.SelectedValue, txtDescripcion.Text, ddlIdClaseDoc.SelectedValue.Trim(), chkEstado.Checked.ToString(), gstr_Usuario);
            if ((str_result[0].ToString().Equals("00")) || str_result[0].ToString().Equals("True"))
            {
                MostarMensaje(str_result[1].ToString(), gchr_MensajeExito);
            }
            else
            {
                MostarMensaje(str_result[1].ToString(), gchr_MensajeError);
            }
        }

        protected void btnServicioVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Mantenimiento/frmTiposAsiento.aspx", false);
        }

        protected void btnCrearServicio_Click1(object sender, EventArgs e)
        {
          string[] lstr_resultado =  ws_SGService.uwsCrearTipoAsiento( 
                this.ddlModulos.SelectedValue,
                this.txtIDOperacion.Text,
                this.txtCodigo.Text,
                this.txtCodigoAuxiliar1.Text,
                this.txtCodigoAuxiliar2.Text,
                this.txtCodigoAuxiliar3.Text,
                this.txtCodigoAuxiliar4.Text, 
                this.txtIDClaveContable1.Text,
                this.txtIDCuentaContable1.Text,
                this.ddlCentroCosto1.SelectedValue,
                this.ddlCentroBeneficio1.SelectedValue, 
                this.ddlElementoPEP1.SelectedValue, 
                this.ddlPosPre1.SelectedValue,
                this.ddlCentroGestor1.SelectedValue,
                this.txtPrograma1.Text,
                this.ddlFondo1.SelectedValue,
                this.ddlDocPre1.SelectedValue,
                this.txtDocPres1.Text,
                this.txtFlujoEfectivo1.Text,
                this.txtNICSP241.Text,

                this.txtIDClaveContable2.Text,
                this.txtIDCuentaContable2.Text,
                this.ddlCentroCosto2.SelectedValue,
                this.ddlCentroBeneficio2.SelectedValue, 
                this.ddlElementoPEP2.SelectedValue, 
                this.ddlPosPre2.SelectedValue,
                this.ddlCentroGestor2.SelectedValue,
                this.txtPrograma2.Text,
                this.ddlFondo2.SelectedValue,
                this.ddlDocPre2.SelectedValue,
                this.txtDocPres2.Text,
                this.txtFlujoEfectivo2.Text,
                this.txtNICSP242.Text,
                this.ckbEstado.Checked.ToString(),
                gstr_Usuario,
                this.txtCodigoAuxiliar5.Text,                
                this.txtCodigoAuxiliar6.Text,
                this.txtSecuencia.Text);


            if (lstr_resultado[0].ToString().Equals("00") || lstr_resultado[0].ToString().Equals("True"))
                MostarMensaje(lstr_resultado[1].ToString(), gchr_MensajeExito);
            else
                MostarMensaje("No se ingresaron los datos.", gchr_MensajeError);           
        }

        protected void CambioEntidadCP(object sender, EventArgs e) { CargarDatos(); }

        protected void CambioSociedadCo(object sender, EventArgs e) { CargarDatos(); }

        protected void CambioSociedadFi(object sender, EventArgs e) { CargarDatos(); }

    }
}