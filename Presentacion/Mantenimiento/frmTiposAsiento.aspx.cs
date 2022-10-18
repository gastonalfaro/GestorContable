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
    public partial class frmTiposAsiento : BASE
    {
        #region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private char gchr_MensajeError;
        private char gchr_MensajeExito;
        private string gstr_Usuario = String.Empty;
        private string gstr_Modulos = String.Empty;
        private DateTime gdt_FechaConsulta;
        private String[] garr_Modulos;

        //private static DataSet gds_Parametros = new DataSet();
        //private DataSet gds_TpoAsientos = new DataSet();
        //protected DataSet gds_TpoAsientos
        //{
        //    get
        //    {
        //        if (ViewState["gds_TpoAsientos"] == null)
        //            ViewState["gds_TpoAsientos"] = new DataSet();
        //        return (DataSet)ViewState["gds_TpoAsientos"];
        //    }
        //    set
        //    {
        //        ViewState["gds_TpoAsientos"] = value;
        //    }
        //}
        protected DataSet gds_TpoAsientos
        {
            get
            {
                if (Session["gds_TpoAsientos"] == null)
                    Session["gds_TpoAsientos"] = new DataSet();
                return (DataSet)Session["gds_TpoAsientos"];
            }
            set
            {
                Session["gds_TpoAsientos"] = value;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            gchr_MensajeError = clsSesion.Current.chr_MensajeError;
            gchr_MensajeExito = clsSesion.Current.chr_MensajeExito;

            gstr_Usuario = clsSesion.Current.LoginUsuario;
            garr_Modulos = clsSesion.Current.PermisosModulos;


            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(gstr_Usuario))
                {

                    if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmTiposAsiento"))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                    {
                        OcultarMensaje();

                        string lstr_IdOperacion = Request.QueryString["IdOperacion"];
                        if (!string.IsNullOrEmpty(lstr_IdOperacion))
                            ConsultarTpoAsientos("", "", lstr_IdOperacion, "", "", "", "", "", "", "", "", ""); 
                        CargarDatosModulo();
                        CargarDatosOperaciones();
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



        private void CargarDatosModulo()
        {
            DataSet vDatos = ws_SGService.uwsConsultarModulos("", "");
            ddlModulo.DataTextField = "NomModulo";
            ddlModulo.DataValueField = "IdModulo";

            if (vDatos.Tables.Count > 0 && vDatos.Tables[0].Rows.Count > 0) 
            {
                ddlModulo.DataSource = vDatos;
                ddlModulo.DataBind();
            }//if

            ddlModulo.Items.Insert(0, "");
        }


        private void CargarDatosOperaciones(string lstr_IdModulo = "")
        {
            DataSet vDatos = ws_SGService.uwsConsultarDinamico("select * from (select IdOperacion, (IdOperacion + NomOperacion) as NomOperacion, IdModulo from ma.Operaciones)as ta Where (idmodulo = '" + lstr_IdModulo + "' or isnull('" + lstr_IdModulo + "','')='') order by NomOperacion");
            ddlOperacion.DataTextField = "NomOperacion";
            ddlOperacion.DataValueField = "IdOperacion";

            if (vDatos.Tables.Count > 0 && vDatos.Tables[0].Rows.Count > 0) 
            {
                ddlOperacion.DataSource = vDatos;
                //.uwsConsultarOperaciones("", this.ddlModulo.SelectedValue, "");
                ddlOperacion.DataBind();
            }//if
            ddlOperacion.Items.Insert(0, "");
        }

        private void ConsultarTpoAsientos(string str_Codigo, string str_IdModulos, string str_IdOperacion, string str_IdCuentaContable, string str_IdPosPre, string CodigoAuxiliar, string CodigoAuxiliar2, string CodigoAuxiliar3, string CodigoAuxiliar4, string CodigoAuxiliar5, string CodigoAuxiliar6, string Secuencia)
        {
            string lstr_modulo = String.Empty;

            if (string.IsNullOrEmpty(str_IdModulos))
            {
                for (int i = 0; garr_Modulos.Count() > i; i++)
                {
                    if ((i == 0) && (garr_Modulos[i] != null))
                        lstr_modulo = "'" + garr_Modulos[i] + "'";
                    else if (garr_Modulos[i] != null)
                    {
                        lstr_modulo = lstr_modulo + ",'" + garr_Modulos[i] + "'";
                    }
                }
                gstr_Modulos = "IdModulo IN (" + lstr_modulo + ")";
            }
            else gstr_Modulos = "IdModulo IN ('" + str_IdModulos + "')";

            //diferenciacion entre CI y otros modulos
            if (str_IdModulos.Trim().Equals("CI"))
            {
                str_IdOperacion = txtIDOp.Text.Trim().Equals("") ? str_IdOperacion : txtIDOp.Text.Trim();
                if (txtIDOp.Text.Trim().Equals(""))
                {
                    gds_TpoAsientos = ws_SGService.uwsConsultarTiposAsientoDetalle(str_Codigo, gstr_Modulos, null, str_IdCuentaContable, str_IdPosPre, CodigoAuxiliar, CodigoAuxiliar3, CodigoAuxiliar6, CodigoAuxiliar4, CodigoAuxiliar5, CodigoAuxiliar2, Secuencia, null, "N");
                }
                else 
                {
                    gds_TpoAsientos = ws_SGService.uwsConsultarTiposAsientoDetalle(str_Codigo, gstr_Modulos, str_IdOperacion, str_IdCuentaContable, str_IdPosPre, CodigoAuxiliar, CodigoAuxiliar3, CodigoAuxiliar6, CodigoAuxiliar4, CodigoAuxiliar5, CodigoAuxiliar2, Secuencia, null, "N");
                }
            }
            else
            {
                gds_TpoAsientos = ws_SGService.uwsConsultarTiposAsientoDetalle(str_Codigo, gstr_Modulos, str_IdOperacion, str_IdCuentaContable, str_IdPosPre, CodigoAuxiliar, CodigoAuxiliar3, CodigoAuxiliar6, CodigoAuxiliar4, CodigoAuxiliar5, CodigoAuxiliar2, Secuencia, null, "N");
            }
            if (gds_TpoAsientos != null && gds_TpoAsientos.Tables.Count > 0 && gds_TpoAsientos.Tables["Table"].Rows.Count > 0)
            {
                if (str_IdModulos.Trim().Equals("CI"))
               {
                   if (str_IdOperacion.Equals("") || str_IdOperacion == null || !txtIDOp.Text.Trim().Equals(""))
                   {
                       grdvTpoAsiento.DataSource = gds_TpoAsientos.Tables["Table"];
                   }
                   else
                   {//grdvTpoAsiento.DataSource = gds_TpoAsientos.Tables["Table"].Select(" 'ID' + trim(idoperacion) + trim(codigo) + trim(codigoauxiliar2) = '" + str_IdOperacion + "'").CopyToDataTable();
                       grdvTpoAsiento.DataSource = gds_TpoAsientos.Tables["Table"].Select(" 'ID' + trim(idoperacion) + trim(codigo)  = '" + str_IdOperacion + "'").CopyToDataTable();
                   }
               }
                else
               {
                   grdvTpoAsiento.DataSource = gds_TpoAsientos.Tables["Table"];
               }
                grdvTpoAsiento.DataBind();
            }
            else
            {
                grdvTpoAsiento.DataSource = this.LlenarTablaVacia();
                grdvTpoAsiento.DataBind();
                grdvTpoAsiento.Rows[0].Visible = false;
            }
        }

        private DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();

            ldt_TablaVacia.Columns.Add("FchModifica", typeof(string));
            ldt_TablaVacia.Columns.Add("IdModulo", typeof(string));
            ldt_TablaVacia.Columns.Add("NomOperacion", typeof(string));
            ldt_TablaVacia.Columns.Add("IdOperacion", typeof(string));
            ldt_TablaVacia.Columns.Add("Codigo", typeof(string));
            ldt_TablaVacia.Columns.Add("CodigoAuxiliar", typeof(string));
            ldt_TablaVacia.Columns.Add("CodigoAuxiliar2", typeof(string));
            ldt_TablaVacia.Columns.Add("CodigoAuxiliar3", typeof(string));
            ldt_TablaVacia.Columns.Add("CodigoAuxiliar4", typeof(string));
            ldt_TablaVacia.Columns.Add("CodigoAuxiliar5", typeof(string));
            ldt_TablaVacia.Columns.Add("CodigoAuxiliar6", typeof(string));
            ldt_TablaVacia.Columns.Add("Secuencia", typeof(string));

            ldt_TablaVacia.Columns.Add("IdClaveContable", typeof(string));
            ldt_TablaVacia.Columns.Add("IdCuentaContable", typeof(string));
            ldt_TablaVacia.Columns.Add("IdCentroCosto", typeof(string));
            ldt_TablaVacia.Columns.Add("IdCentroBeneficio", typeof(string));
            ldt_TablaVacia.Columns.Add("IdElementoPEP", typeof(string));
            ldt_TablaVacia.Columns.Add("IdPosPre", typeof(string));
            ldt_TablaVacia.Columns.Add("IdCentroGestor", typeof(string));
            ldt_TablaVacia.Columns.Add("IdPrograma", typeof(string));
            ldt_TablaVacia.Columns.Add("IdFondo", typeof(string));
            ldt_TablaVacia.Columns.Add("DocPresupuestario", typeof(string));
            ldt_TablaVacia.Columns.Add("PosDocPresupuestario", typeof(string));
            ldt_TablaVacia.Columns.Add("FlujoEfectivo", typeof(string));
            ldt_TablaVacia.Columns.Add("NICSP24", typeof(string));
            ldt_TablaVacia.Columns.Add("Estado", typeof(Boolean));

            ldt_TablaVacia.Columns.Add("IdClaveContable2", typeof(string));
            ldt_TablaVacia.Columns.Add("IdCuentaContable2", typeof(string));
            ldt_TablaVacia.Columns.Add("IdCentroCosto2", typeof(string));
            ldt_TablaVacia.Columns.Add("IdCentroBeneficio2", typeof(string));
            ldt_TablaVacia.Columns.Add("IdElementoPEP2", typeof(string));
            ldt_TablaVacia.Columns.Add("IdPosPre2", typeof(string));
            ldt_TablaVacia.Columns.Add("IdCentroGestor2", typeof(string));
            ldt_TablaVacia.Columns.Add("IdPrograma2", typeof(string));
            ldt_TablaVacia.Columns.Add("IdFondo2", typeof(string));
            ldt_TablaVacia.Columns.Add("DocPresupuestario2", typeof(string));
            ldt_TablaVacia.Columns.Add("PosDocPresupuestario2", typeof(string));
            ldt_TablaVacia.Columns.Add("FlujoEfectivo2", typeof(string));
            ldt_TablaVacia.Columns.Add("NICSP242", typeof(string));
            DataRow ldr_FilaTabla = ldt_TablaVacia.NewRow();
            ldr_FilaTabla["Estado"] = false;
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

        protected void btnTpoAsientoConsultar_Click(object sender, EventArgs e)
        {
            OcultarMensaje();
            ConsultarTpoAsientos(this.txtBusqIdTpoAsiento.Text, this.ddlModulo.SelectedValue , this.ddlOperacion.SelectedValue, this.txtCuentaContable.Text, this.txtPosPre.Text, this.txtCodigoAux1.Text, this.txtCodigoAux2.Text, this.txtCodigoAux3.Text, this.txtCodigoAux4.Text, this.txtCodigoAux5.Text, this.txtCodigoAux6.Text, this.txtSecuencia.Text);
        }

        protected void btnTpoAsientoVolver_Click(object sender, EventArgs e)
        {

        }

        protected void grdvTpoAsiento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grdvTpoAsiento.SelectedIndex < 0)
                return;
        }

        protected void grdvTpoAsiento_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdvTpoAsiento.EditIndex = e.NewEditIndex;
            //ConsultarTpoAsientos(this.txtBusqIdTpoAsiento.Text, this.ddlModulo.SelectedValue, this.ddlOperacion.SelectedValue, this.txtCuentaContable.Text, this.txtPosPre.Text, this.txtCodigoAux1.Text, this.txtCodigoAux2.Text, this.txtCodigoAux3.Text, this.txtCodigoAux4.Text, this.txtCodigoAux5.Text, this.txtCodigoAux6.Text, this.txtSecuencia.Text);
            grdvTpoAsiento.DataSource = gds_TpoAsientos.Tables["Table"];

            grdvTpoAsiento.DataBind();
            
        }

        protected void grdvTpoAsiento_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdvTpoAsiento.PageIndex = e.NewPageIndex;
            //ConsultarTpoAsientos(this.txtBusqIdTpoAsiento.Text, this.ddlModulo.SelectedValue, this.ddlOperacion.SelectedValue, this.txtCuentaContable.Text, this.txtPosPre.Text, this.txtCodigoAux1.Text, this.txtCodigoAux2.Text, this.txtCodigoAux3.Text, this.txtCodigoAux4.Text, this.txtCodigoAux5.Text, this.txtCodigoAux6.Text, this.txtSecuencia.Text);
            grdvTpoAsiento.DataSource = gds_TpoAsientos.Tables["Table"];

            grdvTpoAsiento.DataBind();
            //ConsultarTpoAsientos(this.txtBusqIdTpoAsiento.Text, this.ddlModulo.SelectedValue, this.ddlOperacion.SelectedValue, this.txtCuentaContable.Text, this.txtPosPre.Text, this.txtCodigoAux1.Text, this.txtCodigoAux2.Text, this.txtCodigoAux3.Text, this.txtCodigoAux4.Text, this.txtCodigoAux5.Text, this.txtCodigoAux6.Text, this.txtSecuencia.Text);
        }

        protected void grdvTpoAsiento_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                String[] lstr_result = new String[3];
                //DataRow ldr_TpoAsientoRow = gds_TpoAsientos.Tables["Table"].NewRow();
                //ldr_TpoAsientoRow = gds_TpoAsientos.Tables["Table"].Rows[e.RowIndex];

                //string lstr_IdModulo = ldr_TpoAsientoRow["IdModulo"].ToString().Trim();
                //string lstr_IdOperacion = ldr_TpoAsientoRow["IdOperacion"].ToString().Trim();
                //string lstr_Codigo = ldr_TpoAsientoRow["Codigo"].ToString().Trim();
                //string lstr_CodigoAux = ldr_TpoAsientoRow["CodigoAuxiliar"].ToString().Trim();
                //string lstr_CodigoAux2 = ldr_TpoAsientoRow["CodigoAuxiliar2"].ToString().Trim();
                //string lstr_CodigoAux3 = ldr_TpoAsientoRow["CodigoAuxiliar3"].ToString().Trim();
                //string lstr_CodigoAux4 = ldr_TpoAsientoRow["CodigoAuxiliar4"].ToString().Trim();
                //DateTime ldt_FchModifica = Convert.ToDateTime(ldr_TpoAsientoRow["FchModifica"].ToString());

                //string lstr_fecha = String.Empty;
                //lstr_fecha = ldt_FchModifica.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                //ldt_FchModifica = Convert.ToDateTime(lstr_fecha);

                GridViewRow row = (GridViewRow)grdvTpoAsiento.Rows[e.RowIndex];


                Label lstr_IdModulo = (Label)row.FindControl("lblIdModulo");
                Label lstr_IdOperacion = (Label)row.FindControl("lblIdOperacion");
                Label lstr_Codigo = (Label)row.FindControl("lblIdCodigo");
                Label lstr_CodigoAux = (Label)row.FindControl("lblIdCodigoAux");
                Label lstr_CodigoAux2 = (Label)row.FindControl("lblIdCodigoAux2");
                Label lstr_CodigoAux3 = (Label)row.FindControl("lblIdCodigoAux3");
                Label lstr_CodigoAux4 = (Label)row.FindControl("lblIdCodigoAux4");
                Label lstr_CodigoAux5 = (Label)row.FindControl("lblIdCodigoAux5");
                Label lstr_CodigoAux6 = (Label)row.FindControl("lblIdCodigoAux6");
                Label lstr_Secuencia = (Label)row.FindControl("lblIdSecuencia");
                Label lstr_FchModifica = (Label)row.FindControl("lblFechaModifica");


                TextBox lstr_IdClaveContable = (TextBox)row.FindControl("txtEditarIdClaveContable");
                TextBox lstr_IdCuentaContable = (TextBox)row.FindControl("txtEditarIdCuentaContable");
                TextBox lstr_IdCentroCosto = (TextBox)row.FindControl("txtEditarIdCentroCosto");
                TextBox lstr_IdCentroBeneficio = (TextBox)row.FindControl("txtEditarIdCentroBeneficio");
                TextBox lstr_IdElementoPEP = (TextBox)row.FindControl("txtEditarIdElementoPEP");
                TextBox lstr_IdPosPre = (TextBox)row.FindControl("txtEditarIdPosPre");
                TextBox lstr_IdCentroGestor = (TextBox)row.FindControl("txtEditarIdCentroGestor");
                TextBox lstr_IdPrograma = (TextBox)row.FindControl("txtEditarIdPrograma");
                TextBox lstr_IdFondo = (TextBox)row.FindControl("txtEditarIdFondo");
                TextBox lstr_DocPresupuestario = (TextBox)row.FindControl("txtEditarDocPresupuestario");
                TextBox lstr_PosDocPresupuestario = (TextBox)row.FindControl("txtEditarPosDocPresupuestario");
                TextBox lstr_FlujoEfectivo = (TextBox)row.FindControl("txtEditarFlujoEfectivo");
                TextBox lstr_NICSP24 = (TextBox)row.FindControl("txtEditarNICSP24");

                TextBox lstr_IdClaveContable2 = (TextBox)row.FindControl("txtEditarIdClaveContable2");
                TextBox lstr_IdCuentaContable2 = (TextBox)row.FindControl("txtEditarIdCuentaContable2");
                TextBox lstr_IdCentroCosto2 = (TextBox)row.FindControl("txtEditarIdCentroCosto2");
                TextBox lstr_IdCentroBeneficio2 = (TextBox)row.FindControl("txtEditarIdCentroBeneficio2");
                TextBox lstr_IdElementoPEP2 = (TextBox)row.FindControl("txtEditarIdElementoPEP2");
                TextBox lstr_IdPosPre2 = (TextBox)row.FindControl("txtEditarIdPosPre2");
                TextBox lstr_IdCentroGestor2 = (TextBox)row.FindControl("txtEditarIdCentroGestor2");
                TextBox lstr_IdPrograma2 = (TextBox)row.FindControl("txtEditarIdPrograma2");
                TextBox lstr_IdFondo2 = (TextBox)row.FindControl("txtEditarIdFondo2");
                TextBox lstr_DocPresupuestario2 = (TextBox)row.FindControl("txtEditarDocPresupuestario2");
                TextBox lstr_PosDocPresupuestario2 = (TextBox)row.FindControl("txtEditarPosDocPresupuestario2");
                TextBox lstr_FlujoEfectivo2 = (TextBox)row.FindControl("txtEditarFlujoEfectivo2");
                TextBox lstr_NICSP242 = (TextBox)row.FindControl("txtEditarNICSP242");
                CheckBox lbool_Estado = (CheckBox)row.FindControl("cbEditarEstado");

                string lol = lstr_DocPresupuestario.Text.Trim();

                lstr_result = ws_SGService.uwsModificarTipoAsiento(
                    lstr_IdModulo.Text, lstr_IdOperacion.Text, lstr_Codigo.Text, lstr_CodigoAux.Text, lstr_CodigoAux2.Text, lstr_CodigoAux3.Text, lstr_CodigoAux4.Text,
                    lstr_IdClaveContable.Text.Trim(), lstr_IdCuentaContable.Text.Trim(), lstr_IdCentroCosto.Text.Trim(), lstr_IdCentroBeneficio.Text.Trim(),
                    lstr_IdElementoPEP.Text.Trim(), lstr_IdPosPre.Text.Trim(), lstr_IdCentroGestor.Text.Trim(), lstr_IdPrograma.Text.Trim(),
                    lstr_IdFondo.Text.Trim(), lstr_DocPresupuestario.Text.Trim(), lstr_PosDocPresupuestario.Text.Trim(), lstr_FlujoEfectivo.Text.Trim(),
                    lstr_NICSP24.Text.Trim(), lstr_IdClaveContable2.Text.Trim(), lstr_IdCuentaContable2.Text.Trim(), lstr_IdCentroCosto2.Text.Trim(), lstr_IdCentroBeneficio2.Text.Trim(),
                    lstr_IdElementoPEP2.Text.Trim(), lstr_IdPosPre2.Text.Trim(), lstr_IdCentroGestor2.Text.Trim(), lstr_IdPrograma2.Text.Trim(), lstr_IdFondo2.Text.Trim(),
                    lstr_DocPresupuestario2.Text.Trim(), lstr_PosDocPresupuestario2.Text.Trim(), lstr_FlujoEfectivo2.Text.Trim(), lstr_NICSP242.Text.Trim(), lbool_Estado.Checked ? "A" : "I",
                    gstr_Usuario, Convert.ToDateTime(lstr_FchModifica.Text), lstr_CodigoAux5.Text, lstr_CodigoAux6.Text, lstr_Secuencia.Text);

                if (lstr_result[0].ToString().Equals("00") || lstr_result[0].ToString().Equals("True"))
                    MostarMensaje("La modificación de datos ha sido satisfactoria.", gchr_MensajeExito);
                else
                    MostarMensaje("La modificación de datos no ha sido satisfactoria.", gchr_MensajeError);

                //ConsultarTpoAsientos(this.txtBusqIdTpoAsiento.Text, this.ddlModulo.SelectedValue, this.ddlOperacion.SelectedValue, this.txtCuentaContable.Text, this.txtPosPre.Text, this.txtCodigoAux1.Text, this.txtCodigoAux2.Text, this.txtCodigoAux3.Text, this.txtCodigoAux4.Text, this.txtCodigoAux5.Text, this.txtCodigoAux6.Text, this.txtSecuencia.Text);
                grdvTpoAsiento.EditIndex = -1;
                //grdvTpoAsiento.DataSource = gds_TpoAsientos.Tables["Table"];
                ConsultarTpoAsientos(this.txtBusqIdTpoAsiento.Text, this.ddlModulo.SelectedValue, this.ddlOperacion.SelectedValue, this.txtCuentaContable.Text, this.txtPosPre.Text, this.txtCodigoAux1.Text, this.txtCodigoAux2.Text, this.txtCodigoAux3.Text, this.txtCodigoAux4.Text, this.txtCodigoAux5.Text, this.txtCodigoAux6.Text, this.txtSecuencia.Text);

                //grdvTpoAsiento.DataBind();
                //ConsultarTpoAsientos(this.txtBusqIdTpoAsiento.Text, this.ddlModulo.SelectedValue, this.ddlOperacion.SelectedValue, this.txtCuentaContable.Text, this.txtPosPre.Text, this.txtCodigoAux1.Text, this.txtCodigoAux2.Text, this.txtCodigoAux3.Text, this.txtCodigoAux4.Text, this.txtCodigoAux5.Text, this.txtCodigoAux6.Text, this.txtSecuencia.Text);
            }
            catch (Exception ex)
            {

                ConsultarTpoAsientos(this.txtBusqIdTpoAsiento.Text, this.ddlModulo.SelectedValue, this.ddlOperacion.SelectedValue, this.txtCuentaContable.Text, this.txtPosPre.Text, this.txtCodigoAux1.Text, this.txtCodigoAux2.Text, this.txtCodigoAux3.Text, this.txtCodigoAux4.Text, this.txtCodigoAux5.Text, this.txtCodigoAux6.Text, this.txtSecuencia.Text);

                MostarMensaje("Error al finalizar la modificación de datos.", gchr_MensajeError);
            }
        }

        protected void grdvTpoAsiento_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdvTpoAsiento.EditIndex = -1;
            //ConsultarTpoAsientos(this.txtBusqIdTpoAsiento.Text, this.ddlModulo.SelectedValue, this.ddlOperacion.SelectedValue, this.txtCuentaContable.Text, this.txtPosPre.Text, this.txtCodigoAux1.Text, this.txtCodigoAux2.Text, this.txtCodigoAux3.Text, this.txtCodigoAux4.Text, this.txtCodigoAux5.Text, this.txtCodigoAux6.Text, this.txtSecuencia.Text);
            grdvTpoAsiento.DataSource = gds_TpoAsientos.Tables["Table"];

            grdvTpoAsiento.DataBind();

            //ConsultarTpoAsientos(this.txtBusqIdTpoAsiento.Text, this.ddlModulo.SelectedValue, this.ddlOperacion.SelectedValue, this.txtCuentaContable.Text, this.txtPosPre.Text, this.txtCodigoAux1.Text, this.txtCodigoAux2.Text, this.txtCodigoAux3.Text, this.txtCodigoAux4.Text, this.txtCodigoAux5.Text, this.txtCodigoAux6.Text, this.txtSecuencia.Text);
        }

        protected void btnTpoAsientoNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Mantenimiento/Gestiones/frmNuevoTipoAsiento.aspx", true);
        }
        protected void grdvTpoAsiento_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    CheckBox cbEditarEstado = (CheckBox)e.Row.FindControl("cbEditarEstado");
                    cbEditarEstado.Checked = (e.Row.FindControl("lblEstado") as Label).Text.Trim().Equals("A") ? true : false;
                }

        }

        protected void grdvTpoAsiento_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                GridViewRow row = (GridViewRow)grdvTpoAsiento.Rows[e.RowIndex];

                Label lstr_IdModulo = (Label)row.FindControl("lblIdModulo");
                Label lstr_IdOperacion = (Label)row.FindControl("lblIdOperacion");
                Label lstr_Codigo = (Label)row.FindControl("lblIdCodigo");
                Label lstr_CodigoAux = (Label)row.FindControl("lblIdCodigoAux");
                Label lstr_CodigoAux2 = (Label)row.FindControl("lblIdCodigoAux2");
                Label lstr_CodigoAux3 = (Label)row.FindControl("lblIdCodigoAux3");
                Label lstr_CodigoAux4 = (Label)row.FindControl("lblIdCodigoAux4");
                Label lstr_CodigoAux5 = (Label)row.FindControl("lblIdCodigoAux5");
                Label lstr_CodigoAux6 = (Label)row.FindControl("lblIdCodigoAux6");
                Label lstr_Secuencia = (Label)row.FindControl("lblIdSecuencia");

                string[] lstr_result = ws_SGService.uwsEliminarTipoAsiento(lstr_IdModulo.Text, lstr_IdOperacion.Text,
                    lstr_Codigo.Text, lstr_CodigoAux.Text, lstr_CodigoAux2.Text, lstr_CodigoAux3.Text,
                    lstr_CodigoAux4.Text, lstr_CodigoAux5.Text, lstr_CodigoAux6.Text,
                    lstr_Secuencia.Text);

                if (lstr_result[0].ToString().Equals("00") || lstr_result[0].ToString().Equals("True"))
                    MostarMensaje("La eliminación de datos ha sido satisfactoria.", gchr_MensajeExito);
                else
                    MostarMensaje("La eliminación de datos no ha sido satisfactoria.", gchr_MensajeError);
                //ConsultarTpoAsientos(this.txtBusqIdTpoAsiento.Text, this.ddlModulo.SelectedValue, this.ddlOperacion.SelectedValue, this.txtCuentaContable.Text, this.txtPosPre.Text, this.txtCodigoAux1.Text, this.txtCodigoAux2.Text, this.txtCodigoAux3.Text, this.txtCodigoAux4.Text, this.txtCodigoAux5.Text, this.txtCodigoAux6.Text, this.txtSecuencia.Text);
                grdvTpoAsiento.EditIndex = -1;
                grdvTpoAsiento.DataSource = gds_TpoAsientos.Tables["Table"];

                grdvTpoAsiento.DataBind();
                //ConsultarTpoAsientos(this.txtBusqIdTpoAsiento.Text, this.ddlModulo.SelectedValue, this.ddlOperacion.SelectedValue, this.txtCuentaContable.Text, this.txtPosPre.Text, this.txtCodigoAux1.Text, this.txtCodigoAux2.Text, this.txtCodigoAux3.Text, this.txtCodigoAux4.Text, this.txtCodigoAux5.Text, this.txtCodigoAux6.Text, this.txtSecuencia.Text);
            }
            catch (Exception ex)
            {
                MostarMensaje("Error al eliminar los datos.", gchr_MensajeError);
            }
            finally
            {
                ConsultarTpoAsientos(this.txtBusqIdTpoAsiento.Text, this.ddlModulo.SelectedValue, this.ddlOperacion.SelectedValue, this.txtCuentaContable.Text, this.txtPosPre.Text, this.txtCodigoAux1.Text, this.txtCodigoAux2.Text, this.txtCodigoAux3.Text, this.txtCodigoAux4.Text, this.txtCodigoAux5.Text, this.txtCodigoAux6.Text, this.txtSecuencia.Text);
            }
        }

        protected void ddlModulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarDatosOperaciones(this.ddlModulo.SelectedValue );
            divCod.Visible = dviRelleno.Visible = ddlModulo.SelectedValue.Trim().Equals("CI");
        }

        protected void ddlOperacion_TextChanged(object sender, EventArgs e)
        {
            if (ddlModulo.SelectedValue.Trim().Equals("CI"))
                txtIDOp.Text = "";
        }

    }
}