using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;
using Presentacion.Compartidas;
using System.Web.UI.HtmlControls;

namespace Presentacion.Mantenimiento
{
    public partial class frmDirecciones : BASE
    {

        # region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private char gchr_MensajeError;
        private char gchr_MensajeExito;
        private string gstr_ModuloActual = String.Empty;
        private string gstr_Usuario = String.Empty;
        private string gstr_NomSociedadGL = String.Empty;
        private string gstr_IdSociedadGL = String.Empty;
        private DateTime gdt_FechaModifica = new DateTime();
        private DateTime gdt_FchModificaSocGL = new DateTime();

        //private static DataSet gds_Direcciones = new DataSet();
        protected DataSet gds_Direcciones
        {
            get
            {
                if (ViewState["gds_Direcciones"] == null)
                    ViewState["gds_Direcciones"] = new DataSet();
                return (DataSet)ViewState["gds_Direcciones"];
            }
            set
            {
                ViewState["gds_Direcciones"] = value;
            }
        }
        //private static DataSet gds_Oficinas = new DataSet();
        protected DataSet gds_Oficinas
        {
            get
            {
                if (ViewState["gds_Oficinas"] == null)
                    ViewState["gds_Oficinas"] = new DataSet();
                return (DataSet)ViewState["gds_Oficinas"];
            }
            set
            {
                ViewState["gds_Oficinas"] = value;
            }
        }
        //private static DataSet gds_Servicios = new DataSet();
        protected DataSet gds_Servicios
        {
            get
            {
                if (ViewState["gds_Servicios"] == null)
                    ViewState["gds_Servicios"] = new DataSet();
                return (DataSet)ViewState["gds_Servicios"];
            }
            set
            {
                ViewState["gds_Servicios"] = value;
            }
        }

        //private static DataSet gds_SociedadGLFI = new DataSet();
        protected DataSet gds_SociedadGLFI
        {
            get
            {
                if (ViewState["gds_SociedadGLFI"] == null)
                    ViewState["gds_SociedadGLFI"] = new DataSet();
                return (DataSet)ViewState["gds_SociedadGLFI"];
            }
            set
            {
                ViewState["gds_SociedadGLFI"] = value;
            }
        }
        //private static DataSet gds_SocFI = new DataSet();
        protected DataSet gds_SocFI
        {
            get
            {
                if (ViewState["gds_SocFI"] == null)
                    ViewState["gds_SocFI"] = new DataSet();
                return (DataSet)ViewState["gds_SocFI"];
            }
            set
            {
                ViewState["gds_SocFI"] = value;
            }
        }
        //private static DataSet gds_SocGL = new DataSet();
        protected DataSet gds_SocGL
        {
            get
            {
                if (ViewState["gds_SocGL"] == null)
                    ViewState["gds_SocGL"] = new DataSet();
                return (DataSet)ViewState["gds_SocGL"];
            }
            set
            {
                ViewState["gds_SocGL"] = value;
            }
        }
        //private static DataSet gds_SociedadesFi = new DataSet();
        protected DataSet gds_SociedadesFi
        {
            get
            {
                if (ViewState["gds_SociedadesFi"] == null)
                    ViewState["gds_SociedadesFi"] = new DataSet();
                return (DataSet)ViewState["gds_SociedadesFi"];
            }
            set
            {
                ViewState["gds_SociedadesFi"] = value;
            }
        }

        # endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            gstr_Usuario = clsSesion.Current.LoginUsuario;
            gchr_MensajeError = clsSesion.Current.chr_MensajeError;
            gchr_MensajeExito = clsSesion.Current.chr_MensajeExito;
            gstr_NomSociedadGL = clsSesion.Current.NomSociedadGL;
            gstr_IdSociedadGL = clsSesion.Current.IdSociedadGL;
            gstr_ModuloActual = clsSesion.Current.gstr_ModuloActual;

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(gstr_Usuario))
                {
                    if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmDirecciones"))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                    {
                        OcultarMensaje();

                        MostrarDatosEncabezado();
                        ConsultarDirecciones("", gstr_IdSociedadGL, "");
                        ConsultarOficinas("", gstr_IdSociedadGL, "", "");

                        ConsultarServicios( gstr_IdSociedadGL);
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

        private void MostrarDatosEncabezado()
        {
            txtInstitucion.Text = gstr_IdSociedadGL.Trim();

            gds_SocGL = ws_SGService.uwsConsultarSociedadesGL(gstr_IdSociedadGL.Trim(), "", "", "", "");

            if (gds_SocGL.Tables["Table"].Rows.Count > 0)
            {
                txtNombreSoc.Text = gds_SocGL.Tables["Table"].Rows[0]["NomSociedad"].ToString();
                txtEstadoS.Text = gds_SocGL.Tables["Table"].Rows[0]["Estado"].ToString();
                //txtCorreo.Text = gds_SocGL.Tables["Table"].Rows[0]["CorreoNotifica"].ToString();
            }

            string gstr_Modulo = "IdModulo IN ('" + gstr_ModuloActual + "')";

            gds_SociedadGLFI = ws_SGService.uwsConsultarSociedadesGLSociedadesFi(gstr_IdSociedadGL.Trim(), gstr_Modulo, "");

            if (gds_SociedadGLFI.Tables["Table"] != null)
            {
                if (gds_SociedadGLFI.Tables["Table"].Rows.Count > 0)
                {
                         
                    //string lstr_IdDirecciones = ldr_DireccionesRow["IdDireccion"].ToString();
                    ddlSociedadesFi.SelectedValue = 
                    txtIdSociedadFI.Text = gds_SociedadGLFI.Tables["Table"].Rows[0]["IdSociedadFi"].ToString();
                }
            }

            gds_SocFI = ws_SGService.uwsConsultarSociedadesFinancieras(txtIdSociedadFI.Text.Trim(), "", "", "", "", "");

            if (gds_SocFI.Tables["Table"].Rows.Count > 0)
            {
                if (!String.IsNullOrEmpty(txtIdSociedadFI.Text.Trim()))
                {
                    txtNombreSocFI.Text = gds_SocFI.Tables["Table"].Rows[0]["Denominacion"].ToString();
                }
            }

            LlenarSociedadesFI();
            
        }

        private void ConsultarDirecciones(string str_IdDireccion, string str_IdSociedadGL, string str_NomDireccion)
        {
            gds_Direcciones = ws_SGService.uwsConsultarDirecciones(str_IdDireccion, str_IdSociedadGL, str_NomDireccion);

            if (gds_Direcciones.Tables["Table"].Rows.Count > 0)
            {
                grdDirecciones.DataSource = gds_Direcciones.Tables["Table"];
                grdDirecciones.DataBind();
            }
            else
            {
                grdDirecciones.DataSource = this.LlenarTablaVaciaDirecciones();
                grdDirecciones.DataBind();
            }
        }

        private void ConsultarOficinas(string str_IdOficina, string str_IdSociedadGL, string str_IdDireccion, string str_NomOficina)
        {
            gds_Oficinas = ws_SGService.uwsConsultarOficinas("",str_IdSociedadGL, str_IdDireccion, str_NomOficina);

            if (gds_Oficinas.Tables["Table"].Rows.Count > 0)
            {
                grdOficinas.DataSource = gds_Oficinas.Tables["Table"];
                grdOficinas.DataBind();
            }
            else
            {
                grdOficinas.DataSource = this.LlenarTablaOficinas();
                grdOficinas.DataBind();
            }
        }

        private void ConsultarServicios(string str_IdSociedadGL)
        {
            gds_Servicios = ws_SGService.uwsConsultarServicios("", str_IdSociedadGL, "", "", "", "");

            if (gds_Servicios.Tables["Table"].Rows.Count > 0)
            {
                grdServicios.DataSource = gds_Servicios.Tables["Table"];
                grdServicios.DataBind();
            }
            else
            {
                grdServicios.DataSource = this.LlenarTablaServicios();
               // grdServicios.DataBind();
            }
        }

        private void LlenarSociedadesFI()
        {
            gds_SociedadesFi = ws_SGService.uwsConsultarSociedadesFinancieras("","","","","","");

            if (gds_SociedadesFi.Tables["Table"].Rows.Count > 0)
            {
                ddlSociedadesFi.DataSource = gds_SociedadesFi.Tables["Table"];
                ddlSociedadesFi.DataTextField = "Denominacion";
                ddlSociedadesFi.DataValueField = "IdSociedadFI";
                ddlSociedadesFi.DataBind();
            }
            else
            {
                ddlSociedadesFi.DataSource = this.LlenarTablaSociedadesFI();
                ddlSociedadesFi.DataBind();
            }
        }

        private DataTable LlenarTablaSociedadesFI()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("IdSociedadFI", typeof(string));
            ldt_TablaVacia.Columns.Add("Denominacion", typeof(string));
            DataRow ldr_FilaTabla = ldt_TablaVacia.NewRow();
            ldt_TablaVacia.Rows.Add(ldr_FilaTabla);
            return ldt_TablaVacia;
        }

        private DataTable LlenarTablaVaciaDirecciones()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("IdDireccion", typeof(string));
            ldt_TablaVacia.Columns.Add("IdSociedadGL", typeof(string));
            ldt_TablaVacia.Columns.Add("NomDireccion", typeof(string));
            ldt_TablaVacia.Columns.Add("Estado", typeof(string));

            DataRow ldr_FilaTabla = ldt_TablaVacia.NewRow();
            ldt_TablaVacia.Rows.Add(ldr_FilaTabla);
            return ldt_TablaVacia;
        }

        private DataTable LlenarTablaOficinas()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("IdOficina", typeof(string));
            ldt_TablaVacia.Columns.Add("IdSociedadGL", typeof(string));
            ldt_TablaVacia.Columns.Add("NomOficina", typeof(string));
            ldt_TablaVacia.Columns.Add("CorreoNotifica", typeof(string));
            ldt_TablaVacia.Columns.Add("IdDireccion", typeof(string));
            ldt_TablaVacia.Columns.Add("UsaExpediente", typeof(string));
            ldt_TablaVacia.Columns.Add("Estado", typeof(string));

            DataRow ldr_FilaTabla = ldt_TablaVacia.NewRow();
            ldt_TablaVacia.Rows.Add(ldr_FilaTabla);
            return ldt_TablaVacia;
        }

        private DataTable LlenarTablaServicios()
        {
            DataTable ldt_TablaVacia = new DataTable();
            
            ldt_TablaVacia.Columns.Add("IdServicio", typeof(string));
            ldt_TablaVacia.Columns.Add("NomServicio", typeof(string));
            ldt_TablaVacia.Columns.Add("Monto", typeof(string));
            ldt_TablaVacia.Columns.Add("CtaContableDebeActualDev", typeof(string));
            ldt_TablaVacia.Columns.Add("CtaContableHaberActualDev", typeof(string));
            ldt_TablaVacia.Columns.Add("IdPosPreActualDev", typeof(string));
            ldt_TablaVacia.Columns.Add("CtaContableDebeActualPer", typeof(string));
            ldt_TablaVacia.Columns.Add("CtaContableHaberActualPer", typeof(string));
            ldt_TablaVacia.Columns.Add("IdPosPreActualPer", typeof(string));
            ldt_TablaVacia.Columns.Add("CtaContableDebeVencidoDev", typeof(string));
            ldt_TablaVacia.Columns.Add("CtaContableHaberVencidoDev", typeof(string));
            ldt_TablaVacia.Columns.Add("IdPosPreVencidoDev", typeof(string));
          //  ldt_TablaVacia.Columns.Add("CtaContableDebeVencidoDev", typeof(string));
            //ldt_TablaVacia.Columns.Add("CtaContableHaberVencidoDev", typeof(string));
            //ldt_TablaVacia.Columns.Add("IdPosPreVencidoDev", typeof(string));
            ldt_TablaVacia.Columns.Add("Estado", typeof(string));
          
            DataRow ldr_FilaTabla = ldt_TablaVacia.NewRow();
            ldt_TablaVacia.Rows.Add(ldr_FilaTabla);
            return ldt_TablaVacia;
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

        protected void btnNuevasDirecciones_Click(object sender, EventArgs e)
        {

        }

        protected void btnGuardarDirecciones_Click(object sender, EventArgs e)
        {
            String[] lstr_result = new String[3];
            bool lbl_resultado = false;
            try
            {
                DateTime ldt_FchModifica = new DateTime();
                string lstr_IdSociedadGL = gstr_IdSociedadGL.Trim();
                string lstr_IdSociedadFi = ddlSociedadesFi.SelectedValue.Trim();

                string gstr_Modulo = "IdModulo IN ('" + gstr_ModuloActual + "')";

                gds_SociedadGLFI = ws_SGService.uwsConsultarSociedadesGLSociedadesFi(lstr_IdSociedadGL, gstr_Modulo, lstr_IdSociedadFi);


                if ((gds_SociedadGLFI.Tables["Table"] != null) && (gds_SociedadGLFI.Tables["Table"].Rows.Count > 0))
                {
                    ldt_FchModifica = Convert.ToDateTime(gds_SociedadGLFI.Tables["Table"].Rows[0]["FchModifica"].ToString());
                    string str_fecha = String.Empty;
                    str_fecha = ldt_FchModifica.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    ldt_FchModifica = Convert.ToDateTime(str_fecha);

                    string str_CodResultado = String.Empty;
                    string str_Mensaje = String.Empty;

                    lbl_resultado = ws_SGService.uwsModificarSociedadGlSociedadFi(lstr_IdSociedadGL, gstr_ModuloActual, lstr_IdSociedadFi, gstr_Usuario, ldt_FchModifica, out str_CodResultado, out str_Mensaje);
                }
                else 
                { 
                    string str_CodResultado = String.Empty;
                    string str_Mensaje = String.Empty;

                    lbl_resultado = ws_SGService.uwsCrearSociedadGlSociedadFi(lstr_IdSociedadGL, gstr_ModuloActual, lstr_IdSociedadFi, gstr_Usuario, out str_CodResultado, out str_Mensaje);
                }

                if (lbl_resultado)
                {
                    txtIdSociedadFI.Text = ddlSociedadesFi.SelectedValue;
                    gds_SocFI = ws_SGService.uwsConsultarSociedadesFinancieras(ddlSociedadesFi.SelectedValue, "", "", "", "", "");

                    if (gds_SocFI.Tables["Table"].Rows.Count > 0)
                    {
                        txtNombreSocFI.Text = gds_SocFI.Tables["Table"].Rows[0]["Denominacion"].ToString();
                    }
                    MostarMensaje("Se modificó la institución correctamente.", gchr_MensajeExito);
                }
                else
                {
                    MostarMensaje("Error al modificar la institución.", gchr_MensajeError);
                }

                gds_SocGL = ws_SGService.uwsConsultarSociedadesGL(lstr_IdSociedadGL,"","","","");

                if (gds_SocGL.Tables["Table"].Rows.Count > 0)
                {
                    ldt_FchModifica = Convert.ToDateTime(gds_SocGL.Tables["Table"].Rows[0]["FchModifica"].ToString());
                    string str_fecha = String.Empty;
                    str_fecha = ldt_FchModifica.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                    ldt_FchModifica = Convert.ToDateTime(str_fecha);

                    string lstr_CodResultado = String.Empty;
                    string lstr_Mensaje = String.Empty;
                    string lstr_Denominacion = gds_SocGL.Tables["Table"].Rows[0]["Denominacion"].ToString();
                    string lstr_Nombre = gds_SocGL.Tables["Table"].Rows[0]["NomSociedad"].ToString();
                    string lstr_Pais = gds_SocGL.Tables["Table"].Rows[0]["IdPais"].ToString();
                    string lstr_Poblacion = gds_SocGL.Tables["Table"].Rows[0]["Poblacion"].ToString();
                    string lstr_Calle = gds_SocGL.Tables["Table"].Rows[0]["Calle"].ToString();
                    string lstr_Moneda = gds_SocGL.Tables["Table"].Rows[0]["IdMoneda"].ToString();
                    string lstr_Idioma = gds_SocGL.Tables["Table"].Rows[0]["IdIdioma"].ToString();
                    string lstr_Estado = txtEstadoS.Text.Trim().ToUpper();// gds_SocGL.Tables["Table"].Rows[0]["Estado"].ToString();

                    lbl_resultado = ws_SGService.uwsModificarSociedadGL(lstr_IdSociedadGL, lstr_Denominacion, lstr_Nombre,
                        lstr_Pais, lstr_Poblacion, lstr_Calle, lstr_Moneda, lstr_Idioma, string.Empty, lstr_Estado,
                        gstr_Usuario, ldt_FchModifica, out lstr_CodResultado, out lstr_Mensaje);

                    if (lbl_resultado)
                    {
                        MostarMensaje("Se modificó la institución correctamente.", gchr_MensajeExito);
                    }
                    else
                    {
                        MostarMensaje("Error al modificar la institución.", gchr_MensajeError);
                    }
                }

                

                grdDirecciones.EditIndex = -1;
                ConsultarDirecciones("", gstr_IdSociedadGL, "");
            }
            catch (Exception ex)
            {
                ConsultarDirecciones("", gstr_IdSociedadGL, "");
                MostarMensaje("Error al modificar la institución.", gchr_MensajeError);

            }
        }

        protected void btnVolverDirecciones_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Mantenimiento/frmSociedadesGL.aspx", true);
        }

        protected void btnConsultarDireccion_Click(object sender, EventArgs e)
        {
            OcultarMensaje();
            //ConsultarDirecciones(txtBusquedaIdDireccion.Text, txtBusquedaIdMoneda.Text, txtBusquedaNomDireccion.Text);
        }

        protected void grdDirecciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            String[] lstr_resultado = new String[3];
          
            string lstr_IdDireccion = (grdDirecciones.FooterRow.FindControl("txtInsertarIdDireccion") as TextBox).Text;
            string lstr_NomDireccion = (grdDirecciones.FooterRow.FindControl("txtInsertarNomDireccion") as TextBox).Text;
            string lstr_Estado = (grdDirecciones.FooterRow.FindControl("txtInsertarEstado") as TextBox).Text;

            lstr_resultado = ws_SGService.uwsCrearDireccion(lstr_IdDireccion, gstr_IdSociedadGL, lstr_NomDireccion, lstr_Estado, gstr_Usuario);
            
            if (lstr_resultado[0].ToString().Equals("00") || lstr_resultado[0].ToString().Equals("True"))
                MostarMensaje(lstr_resultado[1].ToString(), gchr_MensajeExito);
            else
                MostarMensaje(lstr_resultado[1].ToString(), gchr_MensajeError);
            
            ConsultarDirecciones("", gstr_IdSociedadGL, "");

            grdDirecciones.SelectedIndex = -1;
            grdDirecciones.PageIndex = grdDirecciones.PageCount - 1;
        }

        protected void grdDirecciones_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdDirecciones.EditIndex = e.NewEditIndex;
            grdDirecciones.DataSource = gds_Direcciones.Tables["Table"];

            grdDirecciones.DataBind();
            //ConsultarDirecciones("", gstr_IdSociedadGL, "");
        }

        protected void grdDirecciones_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            String[] lstr_result = new String[3];
            try
            {
                DataRow ldr_DireccionesRow = gds_Direcciones.Tables["Table"].NewRow();
                ldr_DireccionesRow = gds_Direcciones.Tables["Table"].Rows[e.RowIndex];

                string lstr_IdDirecciones = ldr_DireccionesRow["IdDireccion"].ToString();
                string lstr_IdSociedadGL = ldr_DireccionesRow["IdSociedadGL"].ToString();
                DateTime ldt_FchModifica = Convert.ToDateTime(ldr_DireccionesRow["FchModifica"].ToString());

                string str_fecha = String.Empty;
                str_fecha = ldt_FchModifica.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                ldt_FchModifica = Convert.ToDateTime(str_fecha);

                GridViewRow row = (GridViewRow)grdDirecciones.Rows[e.RowIndex];

                TextBox txt_Nombre = (TextBox)row.FindControl("txtEditarNomDireccion");
                TextBox txt_Estado = (TextBox)row.FindControl("txtEditarEstado");

                lstr_result = ws_SGService.uwsModificarDireccion(lstr_IdDirecciones, lstr_IdSociedadGL, txt_Nombre.Text, txt_Estado.Text, gstr_Usuario, ldt_FchModifica);

                if (lstr_result[0].ToString().Equals("00"))
                {
                    MostarMensaje(lstr_result[1].ToString(), gchr_MensajeExito);
                }
                else
                {
                    MostarMensaje(lstr_result[1].ToString(), gchr_MensajeError);
                }
                grdDirecciones.EditIndex = -1;
                grdDirecciones.DataSource = gds_Direcciones.Tables["Table"];

                grdDirecciones.DataBind();
                ConsultarDirecciones("", gstr_IdSociedadGL, "");
            }
            catch (Exception ex)
            {
                ConsultarDirecciones("", gstr_IdSociedadGL, "");
                MostarMensaje(ex.ToString(), gchr_MensajeError);

            }
        }

        protected void grdDirecciones_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {

        }

        protected void grdDirecciones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdDirecciones.PageIndex = e.NewPageIndex;
            grdDirecciones.DataSource = gds_Direcciones.Tables["Table"];

            grdDirecciones.DataBind();
            //ConsultarDirecciones("", gstr_IdSociedadGL, "");
        }

        protected void grdDirecciones_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            try
            {
                grdDirecciones.EditIndex = -1;
                grdDirecciones.DataSource = gds_Direcciones.Tables["Table"];

                grdDirecciones.DataBind();
                //ConsultarDirecciones("", gstr_IdSociedadGL, "");
            }
            catch
            {

            }
        }

        protected void grdOficinas_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (grdOficinas.SelectedIndex < 0)
            //    return;
            String[] lstr_resultado = new String[3];
            try
            {
                string lstr_IdOficina = (grdOficinas.FooterRow.FindControl("txtInsertarIdOficina") as TextBox).Text;
                string lstr_NomOficina = (grdOficinas.FooterRow.FindControl("txtInsertarNomOficina") as TextBox).Text;
                string lstr_Estado = (grdOficinas.FooterRow.FindControl("txtInsertarOfiEstado") as TextBox).Text;
                string lstr_CorreoNotifica = (grdOficinas.FooterRow.FindControl("txtInsertarCorreoNotifica") as TextBox).Text;
                string lstr_UsaExpediente = (grdOficinas.FooterRow.FindControl("txtInsertarUsaExpediente") as TextBox).Text;
                string lstr_Direccion = (grdOficinas.FooterRow.FindControl("ddlNuevaDireccion") as DropDownList).SelectedValue.ToString();
                //cucurucho Varo
                if (lstr_UsaExpediente == "")
                {
                    lstr_UsaExpediente = " ";
                }

                lstr_resultado = ws_SGService.uwsCrearOficina(lstr_IdOficina, gstr_IdSociedadGL, lstr_NomOficina, lstr_Direccion, lstr_Estado, gstr_Usuario, lstr_CorreoNotifica, Convert.ToChar(lstr_UsaExpediente));
                if (lstr_resultado[0].ToString().Equals("00") || lstr_resultado[0].ToString().Equals("True"))
                {
                    MostarMensaje(lstr_resultado[1].ToString(), gchr_MensajeError);
                }
                else
                {
                    MostarMensaje(lstr_resultado[1].ToString(), gchr_MensajeExito);
                }

                ConsultarOficinas("", gstr_IdSociedadGL, "", "");

                grdOficinas.SelectedIndex = -1;
                grdOficinas.PageIndex = grdOficinas.PageCount - 1;
            }
            catch { }
        }

        protected void grdOficinas_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                grdOficinas.EditIndex = e.NewEditIndex;
                grdOficinas.DataSource = gds_Oficinas.Tables["Table"];
                //Label lblD = (Label)grdOficinas.Rows[2].FindControl("lblDireccion"); cucurucho Varo
                Label lblD = (Label)grdOficinas.Rows[0].FindControl("lblDireccion");
                hdDireccion.Value = lblD.Text;
                grdOficinas.DataBind();
                //DropDownList ddlDirecciones = (DropDownList)row.FindControl("ddlEditaDireccion");
                //if (ddlDirecciones != null)
                //{
                //    ddlDirecciones.DataSource = gds_Direcciones;
                //    ddlDirecciones.DataTextField = "NomDireccion";
                //    ddlDirecciones.DataValueField = "IdDireccion";
                //    ddlDirecciones.DataBind();
                //    ddlDirecciones.Items.Insert(0, new ListItem(""));

                //    if (e.Row.RowType == DataControlRowType.DataRow)
                //    {
                //        ddlDirecciones.SelectedValue = e.Row.Cells[3].Text;// ((Modelo.Material)(e.Row.DataItem)).IdMedida.ToString();
                //    }//if
                //}//if
                //ConsultarOficinas("", gstr_IdSociedadGL, "", "");
            }
            catch(Exception ex)
            {
                ex.Message.ToString();
            }
        }

        protected void grdOficinas_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            String[] lstr_result = new String[3];
            try
            {
                DataRow ldr_OficinasRow = gds_Oficinas.Tables["Table"].NewRow();
                ldr_OficinasRow = gds_Oficinas.Tables["Table"].Rows[e.RowIndex];

                string lstr_IdOficina = ldr_OficinasRow["IdOficina"].ToString();
                string lstr_IdSociedadGL = ldr_OficinasRow["IdSociedadGL"].ToString();
                DateTime ldt_FchModifica = Convert.ToDateTime(ldr_OficinasRow["FchModifica"].ToString());
           
                string str_fecha = String.Empty;
                str_fecha = ldt_FchModifica.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                ldt_FchModifica = Convert.ToDateTime(str_fecha);

                GridViewRow row = (GridViewRow)grdOficinas.Rows[e.RowIndex];

                TextBox txt_NomOficina = (TextBox)row.FindControl("txtEditarNomOficina");
                TextBox txt_Estado = (TextBox)row.FindControl("txtEditarOfiEstado");
                TextBox txt_CorreoNotifica = (TextBox)row.FindControl("txtEditarCorreoNotifica");
                TextBox txt_UsaExpediente = (TextBox)row.FindControl("txtEditarUsaExpediente");
                DropDownList ddlDirecciones = (DropDownList)row.FindControl("ddlEditaDireccion");
                string val = ddlDirecciones.SelectedValue.ToString();//gds_Direcciones.Tables[0].Rows[ddlDirecciones.SelectedIndex]["IdDireccion"].ToString();
                lstr_result = ws_SGService.uwsModificarOficina(lstr_IdOficina, lstr_IdSociedadGL, txt_NomOficina.Text, val, txt_Estado.Text, gstr_Usuario, ldt_FchModifica, txt_CorreoNotifica.Text, Convert.ToChar(txt_UsaExpediente.Text));

                if (lstr_result[0].ToString().Equals("True"))
                {
                    MostarMensaje(lstr_result[1].ToString(), gchr_MensajeExito);
                }
                else
                {
                    MostarMensaje("Error: " + lstr_result[1].ToString(), gchr_MensajeError);
                }
                grdOficinas.EditIndex = -1;
                grdOficinas.DataSource = gds_Oficinas.Tables["Table"];

                grdOficinas.DataBind();
                ConsultarOficinas("", gstr_IdSociedadGL, "", "");
            }
            catch (Exception ex)
            {
                ConsultarOficinas("", gstr_IdSociedadGL, "", "");
                MostarMensaje(ex.ToString(), gchr_MensajeError);

            }
        }

        protected void grdOficinas_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {

        }

        protected void grdOficinas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdOficinas.PageIndex = e.NewPageIndex;
            grdOficinas.DataSource = gds_Oficinas.Tables["Table"];

            grdOficinas.DataBind();
            //ConsultarOficinas("", gstr_IdSociedadGL, "", "");
        }

        protected void grdOficinas_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdOficinas.EditIndex = -1;
            grdOficinas.DataSource = gds_Oficinas.Tables["Table"];

            grdOficinas.DataBind();
            //ConsultarOficinas("", gstr_IdSociedadGL, "", "");
        }

        protected void grdServicios_SelectedIndexChanged(object sender, EventArgs e)
        {


            String[] lstr_resultado = new String[3];
            try
            {
                string lstr_IdServicio = (grdOficinas.FooterRow.FindControl("txtInsertarIdServicio") as TextBox).Text;
                string lstr_NomServicio = (grdOficinas.FooterRow.FindControl("txtInsertarNomServicio") as TextBox).Text;
                string lstr_Monto = (grdOficinas.FooterRow.FindControl("txtInsertarMonto") as TextBox).Text;
                string lstr_CtaContableDebeActualDev = (grdOficinas.FooterRow.FindControl("txtCtaContableDebeActualDev") as TextBox).Text;
                string lstr_CtaContableHaberActualDev = (grdOficinas.FooterRow.FindControl("txtCtaContableHaberActualDev") as TextBox).Text;
                string lstr_IdPosPreActualDev = (grdOficinas.FooterRow.FindControl("txtIdPosPreActualDev") as TextBox).Text;
                string lstr_CtaContableDebeActualPer = (grdOficinas.FooterRow.FindControl("txtCtaContableDebeActualPer") as TextBox).Text;
                string lstr_CtaContableHaberActualPer = (grdOficinas.FooterRow.FindControl("txtCtaContableHaberActualPer") as TextBox).Text;
                string lstr_IdPosPreActualPer = (grdOficinas.FooterRow.FindControl("txtIdPosPreActualPer") as TextBox).Text;
                string lstr_CtaContableDebeVencidoDev1 = (grdOficinas.FooterRow.FindControl("txtCtaContableDebeVencidoDev1") as TextBox).Text;
                string lstr_CtaContableHaberVencidoDev1 = (grdOficinas.FooterRow.FindControl("txtCtaContableHaberVencidoDev1") as TextBox).Text;
                string lstr_IdPosPreVencidoDev1 = (grdOficinas.FooterRow.FindControl("txtIdPosPreVencidoDev1") as TextBox).Text;
                string lstr_CtaContableDebeVencidoDev2 = (grdOficinas.FooterRow.FindControl("txtCtaContableDebeVencidoDev2") as TextBox).Text;
                string lstr_CtaContableHaberVencidoDev2 = (grdOficinas.FooterRow.FindControl("txtCtaContableHaberVencidoDev2") as TextBox).Text;
                string lstr_IdPosPreVencidoDev2 = (grdOficinas.FooterRow.FindControl("txtIdPosPreVencidoDev2") as TextBox).Text;
                string lstr_Estado = (grdOficinas.FooterRow.FindControl("txtInsertarEstado") as TextBox).Text;

                lstr_resultado = ws_SGService.uwsCrearServicio(
                    lstr_IdServicio, 
                    gstr_IdSociedadGL, 
                    "", 
                    lstr_NomServicio, 
                    Convert.ToDecimal(lstr_Monto), 
                    "", 
                    lstr_CtaContableDebeActualDev, 
                    lstr_CtaContableHaberActualDev, 
                    lstr_IdPosPreActualDev, 
                    lstr_CtaContableDebeActualPer, 
                    lstr_CtaContableHaberActualPer, 
                    lstr_IdPosPreActualPer, 
                    lstr_CtaContableDebeVencidoDev1, 
                    lstr_CtaContableHaberVencidoDev1, 
                    lstr_IdPosPreVencidoDev1, 
                    lstr_CtaContableDebeVencidoDev2, 
                    lstr_CtaContableHaberVencidoDev2, 
                    lstr_IdPosPreVencidoDev2, 
                    lstr_Estado, 
                gstr_Usuario);
                if (lstr_resultado[0].ToString().Equals("00") || lstr_resultado[0].ToString().Equals("True"))
                {
                    MostarMensaje(lstr_resultado[1].ToString(), gchr_MensajeError);
                }
                else
                {
                    MostarMensaje(lstr_resultado[1].ToString(), gchr_MensajeExito);
                }

                ConsultarServicios(gstr_IdSociedadGL);

                grdServicios.SelectedIndex = -1;
                grdServicios.PageIndex = grdServicios.PageCount - 1;
            }
            catch { }
        }

        protected void grdServicios_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdServicios.EditIndex = e.NewEditIndex;
            grdServicios.DataSource = gds_Servicios.Tables["Table"];

            grdServicios.DataBind();
            //ConsultarServicios(gstr_IdSociedadGL);
         }

        protected void grdServicios_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void grdServicios_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {

        }

        protected void grdServicios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdServicios.PageIndex = e.NewPageIndex;
            grdServicios.DataSource = gds_Servicios.Tables["Table"];

            grdServicios.DataBind();
            //ConsultarServicios(gstr_IdSociedadGL);
        }

        protected void grdServicios_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdServicios.EditIndex = -1;
            grdServicios.DataSource = gds_Servicios.Tables["Table"];

            grdServicios.DataBind();
            //ConsultarServicios(gstr_IdSociedadGL);
        }

        protected void grdOficinas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //gds_Direcciones
            DropDownList ddlDirecciones = null;
            Label lblNomDireccion = null;
            TextBox txt = null;

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                ddlDirecciones = e.Row.FindControl("ddlNuevaDireccion") as DropDownList;
                //hdDireccion.Value = "";
            }

            if (e.Row.RowType == DataControlRowType.DataRow) 
            {
                if (e.Row.RowState.Equals(System.Web.UI.WebControls.DataControlRowState.Edit) || e.Row.RowState.ToString().Equals("Alternate, Edit"))
                {
                    ddlDirecciones = e.Row.FindControl("ddlEditaDireccion") as DropDownList;
                }
                else 
                {
                    lblNomDireccion = e.Row.FindControl("lblDireccion") as Label;
                
                    txt = e.Row.FindControl("txtEditarUsaExpediente") as TextBox;
                    string texto = lblNomDireccion.Text;
                    if (!texto.Trim().Equals("")) 
                    {
                        DataRow[] result = gds_Direcciones.Tables[0].Select("IdDireccion= " + texto);
                        texto = result[0][2].ToString();
                    }
                    lblNomDireccion.Text = texto;
                
                }
            }

            if (ddlDirecciones != null)
            {
                ddlDirecciones.DataSource = gds_Direcciones;
                ddlDirecciones.DataTextField = "NomDireccion";
                ddlDirecciones.DataValueField = "IdDireccion";
                ddlDirecciones.DataBind();
                ddlDirecciones.Items.Insert(0, new ListItem(""));

                if (!hdDireccion.Value.Equals(""))
                {
                    ddlDirecciones.SelectedItem.Text = hdDireccion.Value;
                }
                //{
                //    ddlDirecciones.SelectedValue = e.Row.Cells[3].Text;// ((Modelo.Material)(e.Row.DataItem)).IdMedida.ToString();
                //}//if
            }//if
        }


    }
}