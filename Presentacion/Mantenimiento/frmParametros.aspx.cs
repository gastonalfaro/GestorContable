using eWorld.UI;
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
    public partial class frmParametros : BASE
    {

        #region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private char gchr_MensajeError;
        private char gchr_MensajeExito;
        //private string gstr_Usuario = String.Empty;
        protected string gstr_Usuario
        {
            get
            {
                if (ViewState["gstr_Usuario"] == null)
                    ViewState["gstr_Usuario"] = string.Empty;
                return (string)ViewState["gstr_Usuario"];
            }
            set
            {
                ViewState["gstr_Usuario"] = value;
            }
        }
        //private string gstr_Modulos = String.Empty;
        protected string gstr_Modulos
        {
            get
            {
                if (ViewState["gstr_Modulos"] == null)
                    ViewState["gstr_Modulos"] = string.Empty;
                return (string)ViewState["gstr_Modulos"];
            }
            set
            {
                ViewState["gstr_Modulos"] = value;
            }
        }

        //private static DataSet gds_Parametros = new DataSet();
        protected DataSet gds_Parametros
        {
            get
            {
                if (ViewState["gds_Parametros"] == null)
                    ViewState["gds_Parametros"] = new DataSet();
                return (DataSet)ViewState["gds_Parametros"];
            }
            set
            {
                ViewState["gds_Parametros"] = value;
            }
        }
        private DateTime gdt_FechaConsulta;
        //private String[] garr_Modulos;
        protected String[] garr_Modulos
        {
            get
            {
                if (ViewState["garr_Modulos"] == null)
                    ViewState["garr_Modulos"] = new String[20];
                return (String[])ViewState["garr_Modulos"];
            }
            set
            {
                ViewState["garr_Modulos"] = value;
            }
        }
        //private String[] garr_Modulo_Unico = new String[2];
        protected String[] garr_Modulo_Unico
        {
            get
            {
                if (ViewState["garr_Modulo_Unico"] == null)
                    ViewState["garr_Modulo_Unico"] = new String[2];
                return (String[])ViewState["garr_Modulo_Unico"];
            }
            set
            {
                ViewState["garr_Modulo_Unico"] = value;
            }
        }
        //private bool gbool_accion = false;
        protected Boolean gbool_accion
        {
            get
            {
                if (ViewState["gbool_accion"] == null)
                    ViewState["gbool_accion"] = false;
                return (Boolean)ViewState["gbool_accion"];
            }
            set
            {
                ViewState["gbool_accion"] = value;
            }
        }

 
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gstr_Usuario = clsSesion.Current.LoginUsuario;
                if (!string.IsNullOrEmpty(gstr_Usuario))
                {
                    garr_Modulos = clsSesion.Current.PermisosModulos;

                    if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "frmParametros"))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                    {
                        gchr_MensajeError = clsSesion.Current.chr_MensajeError;
                        gchr_MensajeExito = clsSesion.Current.chr_MensajeExito;

                        OcultarMensaje();

                        gdt_FechaConsulta = this.txtBusqFchVigencia.Text.Equals(string.Empty) ?  DateTime.Today : Convert.ToDateTime(this.txtBusqFchVigencia.Text);

                        ConsultarParametros(this.txtBusqIdParametro.Text, garr_Modulos, gdt_FechaConsulta, this.txtBusqDesParametro.Text, this.txtBusqTipoParametro.Text);
                        CargarDatosModulo();          
        
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
            ddlBusqIdModulo.DataTextField = "NomModulo";
            ddlBusqIdModulo.DataValueField = "IdModulo";
            ddlBusqIdModulo.DataSource = ws_SGService.uwsConsultarModulos("", "");
            ddlBusqIdModulo.DataBind();

            ddlBusqIdModulo.Items.Insert(0, "");
        }

        private void ConsultarParametros(string str_IdParametro, String[] str_IdModulos, DateTime dt_FchVigencia, string str_DesParametro, string str_TipoParametro)
        {
            string lstr_modulo = String.Empty;

            for (int i = 0; str_IdModulos.Count() > i; i++ )
            {
                if ((i == 0) && (str_IdModulos[i] != null)) 
                    lstr_modulo = "'" + str_IdModulos[i] + "'";
                else if (str_IdModulos[i] != null)
                {
                    lstr_modulo = lstr_modulo + ",'" + str_IdModulos[i] + "'";
                }
            }

            if (!string.IsNullOrEmpty(lstr_modulo))
                    gstr_Modulos = "IdModulo IN (" + lstr_modulo + ")";

            gds_Parametros = ws_SGService.uwsConsultarParametros(str_IdParametro, gstr_Modulos, dt_FchVigencia, str_DesParametro, str_TipoParametro);

            if (gds_Parametros.Tables["Table"].Rows.Count > 0)
            {
                grdvParametros.DataSource = gds_Parametros.Tables["Table"];
                grdvParametros.DataBind();
            }
            else
            {
                grdvParametros.DataSource = this.LlenarTablaVacia();
                grdvParametros.DataBind();
            }

        }

        private DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("IdParametro", typeof(string));
            ldt_TablaVacia.Columns.Add("FchVigencia", typeof(string));
            ldt_TablaVacia.Columns.Add("IdModulo", typeof(string));
            ldt_TablaVacia.Columns.Add("DesParametro", typeof(string));
            ldt_TablaVacia.Columns.Add("TipoParametro", typeof(string));
            ldt_TablaVacia.Columns.Add("Valor", typeof(string));

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

        protected void btnParametroNuevo_Click(object sender, EventArgs e)
        {
            clsSesion.Current.NomParametro = string.Empty;
            Response.Redirect("~/Mantenimiento/Gestiones/frmNuevoParametro.aspx", false);
        }

        protected void btnParametroGuardar_Click(object sender, EventArgs e)
        {

        }

        protected void btnParametroVolver_Click(object sender, EventArgs e)
        {

        }

        protected void btnParametroConsultar_Click(object sender, EventArgs e)
        {
            garr_Modulo_Unico[0] = ddlBusqIdModulo.SelectedValue;
            
            OcultarMensaje();
            gdt_FechaConsulta = this.txtBusqFchVigencia.Text.Equals(string.Empty) ? DateTime.Today : Convert.ToDateTime(this.txtBusqFchVigencia.Text);
                        
            ConsultarParametros(this.txtBusqIdParametro.Text, this.ddlBusqIdModulo.SelectedValue.Equals(string.Empty) ? garr_Modulos : garr_Modulo_Unico  ,gdt_FechaConsulta, this.txtBusqDesParametro.Text, this.txtBusqTipoParametro.Text);            
        

        }

        protected void grdvParametros_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grdvParametros.SelectedIndex < 0)
                return;
        }

        protected void grdvParametros_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdvParametros.EditIndex = e.NewEditIndex;
            grdvParametros.DataSource = gds_Parametros.Tables["Table"];

            grdvParametros.DataBind();
            //garr_Modulo_Unico[0] = ddlBusqIdModulo.SelectedValue ;
            //gdt_FechaConsulta = this.txtBusqFchVigencia.Text.Equals(string.Empty) ? DateTime.Today : Convert.ToDateTime(this.txtBusqFchVigencia.Text);

            //ConsultarParametros(this.txtBusqIdParametro.Text, this.ddlBusqIdModulo.SelectedValue.Equals(string.Empty) ? garr_Modulos : garr_Modulo_Unico, gdt_FechaConsulta, this.txtBusqDesParametro.Text, this.txtBusqTipoParametro.Text);            
        
        }

        protected void grdvParametros_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            String[] lstr_result = new String[3];
            try
            {
               

               // DataRow ldr_ParametrosRow = gds_Parametros.Tables["Table"].NewRow();
               // ldr_ParametrosRow = gds_Parametros.Tables["Table"].Rows[e.RowIndex];

               // string lstr_IdParametros = ldr_ParametrosRow["IdParametro"].ToString();
               // string lstr_IdModulo = ldr_ParametrosRow["IdModulo"].ToString();
                
               // DateTime ldt_FchModifica = Convert.ToDateTime(ldr_ParametrosRow["FchModifica"].ToString());
                
               //// DateTime ldt_FchVigencia = Convert.ToDateTime(ldr_ParametrosRow["FchVigencia"].ToString());

               // string lstr_fecha = String.Empty;
               // lstr_fecha = ldt_FchModifica.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
               // ldt_FchModifica = Convert.ToDateTime(lstr_fecha);

                //lstr_fecha = ldt_FchVigencia.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                //ldt_FchVigencia = Convert.ToDateTime(lstr_fecha);

                GridViewRow lgrdvr_Parametros = (GridViewRow)grdvParametros.Rows[e.RowIndex];


                Label lbl_FchVigencia = (Label)lgrdvr_Parametros.FindControl("lblFchVigencia");
                Label lbl_FchModifica = (Label)lgrdvr_Parametros.FindControl("lblFchModifica");


                Label lbl_Codigo = (Label)lgrdvr_Parametros.FindControl("lblIdParametro");
                Label lbl_Modulo = (Label)lgrdvr_Parametros.FindControl("lblIdModulo");
                Label lbl_DesParametro = (Label)lgrdvr_Parametros.FindControl("lblDesParametro");
                //garr_Modulo_Unico[0] = lbl_Modulo.Text;
                garr_Modulo_Unico[0] = ddlBusqIdModulo.SelectedItem.Text;//ggarcia

                TextBox txt_Nombre = (TextBox)lgrdvr_Parametros.FindControl("txtEditNombre");
               
                
                
                TextBox txt_Estado = (TextBox)lgrdvr_Parametros.FindControl("txtEditEstado");

                TextBox txt_DesParametro = (TextBox)lgrdvr_Parametros.FindControl("txtEditDesParametro");
                TextBox txt_TipoParametro = (TextBox)lgrdvr_Parametros.FindControl("txtEditTipoParametro");
                TextBox txt_Valor = (TextBox)lgrdvr_Parametros.FindControl("txtEditValor");
                //CalendarPopup cal_FchVigencia = (CalendarPopup)lgrdvr_Parametros.FindControl("calEditFchVigencia");

                if (gbool_accion)
                    lstr_result = ws_SGService.uwsModificarParametro(lbl_Codigo.Text, Convert.ToDateTime(lbl_FchVigencia.Text), lbl_Modulo.Text, txt_DesParametro.Text, txt_TipoParametro.Text, txt_Valor.Text, gstr_Usuario, Convert.ToDateTime(lbl_FchModifica.Text));
                else
                {
                    //ldt_FchVigencia = DateTime.Today;
                   // lstr_result = ws_SGService.uwsCrearParametro(lbl_Codigo.Text, Convert.ToDateTime(Convert.ToDateTime(lbl_FchVigencia.Text).ToString("dd/MM/yyyy")), lbl_Modulo.Text, txt_DesParametro.Text, txt_TipoParametro.Text, txt_Valor.Text, gstr_Usuario);



                    clsSesion.Current.NomParametro = lbl_DesParametro.Text;
                    this.Response.Redirect("~/Mantenimiento/Gestiones/frmNuevoParametro.aspx");
                }

                if (lstr_result[0].ToString().Equals("00") || lstr_result[0].ToString().Equals("True"))
                {
                    MostarMensaje("Se actualizó correctamente el parámetro.", gchr_MensajeExito);
                    //MostarMensaje(lstr_result[1].ToString(), gchr_MensajeExito);
                }
                else
                {
                    MostarMensaje("Error al modificar el parámetro.", gchr_MensajeError);
                    //MostarMensaje("Error: " + lstr_result[1].ToString(), gchr_MensajeError);
                }
                grdvParametros.EditIndex = -1;
                grdvParametros.DataSource = gds_Parametros.Tables["Table"];

                grdvParametros.DataBind();

                gdt_FechaConsulta = this.txtBusqFchVigencia.Text.Equals(string.Empty) ? DateTime.Today : Convert.ToDateTime(this.txtBusqFchVigencia.Text);

                ConsultarParametros(this.txtBusqIdParametro.Text, this.ddlBusqIdModulo.SelectedValue.Equals(string.Empty) ? garr_Modulos : garr_Modulo_Unico, gdt_FechaConsulta, this.txtBusqDesParametro.Text, this.txtBusqTipoParametro.Text);            
        
            }
            catch (Exception ex)
            {

                ConsultarParametros(this.txtBusqIdParametro.Text, this.ddlBusqIdModulo.SelectedValue.Equals(string.Empty) ? garr_Modulos : garr_Modulo_Unico, gdt_FechaConsulta, this.txtBusqDesParametro.Text, this.txtBusqTipoParametro.Text);            
        
                MostarMensaje("Error al modificar el parámetro.", gchr_MensajeError);
                //MostarMensaje(ex.ToString(), gchr_MensajeError);

            }

        }

        protected void grdvParametros_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            //gds_Parametros = ws_SGService.uwsConsultarParametros("", gstr_Modulos, gdt_FechaConsulta, "", "");
        }

        protected void grdvParametros_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdvParametros.PageIndex = e.NewPageIndex;
            grdvParametros.DataSource = gds_Parametros.Tables["Table"];

            grdvParametros.DataBind();
            //garr_Modulo_Unico[0] = this.ddlBusqIdModulo.SelectedValue ;
            //gdt_FechaConsulta = this.txtBusqFchVigencia.Text.Equals(string.Empty) ? DateTime.Today : Convert.ToDateTime(this.txtBusqFchVigencia.Text);
                        
            //ConsultarParametros(this.txtBusqIdParametro.Text, this.ddlBusqIdModulo.Text.Equals(string.Empty) ? garr_Modulos : garr_Modulo_Unico, gdt_FechaConsulta, this.txtBusqDesParametro.Text, this.txtBusqTipoParametro.Text);
        }

        protected void grdvParametros_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdvParametros.EditIndex = -1;
            grdvParametros.DataSource = gds_Parametros.Tables["Table"];

            grdvParametros.DataBind();
            //garr_Modulo_Unico[0] = this.ddlBusqIdModulo.SelectedValue;
            //gdt_FechaConsulta = this.txtBusqFchVigencia.Text.Equals(string.Empty) ? DateTime.Today : Convert.ToDateTime(this.txtBusqFchVigencia.Text);
                        
            //ConsultarParametros(this.txtBusqIdParametro.Text, this.ddlBusqIdModulo.Text.Equals(string.Empty) ? garr_Modulos : garr_Modulo_Unico, gdt_FechaConsulta, this.txtBusqDesParametro.Text, this.txtBusqTipoParametro.Text);            
        
        }

        //protected void btnBusqFchVigencia_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (cdrBusqFchVigencia.Visible)
        //        {
        //            cdrBusqFchVigencia.Visible = false; 
        //        }
        //        else
        //        {
        //            if (txtBusqFchVigencia.Text.Trim() != "")
        //                cdrBusqFchVigencia.SelectedDate = Convert.ToDateTime(txtBusqFchVigencia.Text);
        //            cdrBusqFchVigencia.Visible = true; 
        //        }
        //    }
        //    catch
        //    { }
        //} 

        //protected void cdrBusqFchVigencia_SelectionChanged(object sender, EventArgs e)
        //{
        //    txtBusqFchVigencia.Text = cdrBusqFchVigencia.SelectedDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        //    cdrBusqFchVigencia.Visible = false;

            
        //}

        protected void grdvParametros_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
        }

        protected void grdvParametros_Sorting(object sender, GridViewSortEventArgs e)
        {
            DataTable dt = (DataTable)gds_Parametros.Tables["Table"];

            DataView dv = new DataView(dt);

            dv.Sort = e.SortExpression;

            grdvParametros.DataSource = dv;
            grdvParametros.DataBind();
        }

        protected void lbtEditarParametro_Click(object sender, EventArgs e)
        {
                gbool_accion = true;
        }

        protected void lbtDuplicarParametro_Click(object sender, EventArgs e)
        {
            gbool_accion = false;
        }


    }
}