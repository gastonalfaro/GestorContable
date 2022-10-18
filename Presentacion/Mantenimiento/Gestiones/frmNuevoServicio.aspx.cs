using Presentacion.Compartidas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Presentacion.Mantenimiento.Gestiones
{
    public partial class frmNuevoServicio : BASE
    {
        #region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private char lchr_MensajeError;
        private char lchr_MensajeExito;

        private string gstr_ModuloActual = String.Empty;
        private string gstr_Usuario = String.Empty;
        // static DataSet gds_Sociedades = new DataSet();
        private DataSet gds_Sociedades;
        // static DataSet gds_Oficinas = new DataSet();
        private DataSet gds_Oficinas;
        private String campo_requerido_no_encontrado = String.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            gds_Sociedades = new DataSet();
            gds_Oficinas = new DataSet();
            gstr_Usuario = clsSesion.Current.LoginUsuario;
            lchr_MensajeError = clsSesion.Current.chr_MensajeError;
            lchr_MensajeExito = clsSesion.Current.chr_MensajeExito;
            gstr_ModuloActual = clsSesion.Current.gstr_ModuloActual;

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(gstr_Usuario))
                {
                    if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, ""))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                    {
                        clsSesion.Current.IdServicio = String.Empty;
                        ConsultarSociedades();
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

        private void OcultarMensaje()
        {
            this.lblMensaje.Text = String.Empty;
            this.lblMensaje.Visible = false;
        }

        private void ConsultarSociedades()
        {
            gds_Sociedades = ws_SGService.uwsConsultarSociedadesGL("", "", "", "", "");

            if (gds_Sociedades.Tables["Table"].Rows.Count > 0)
            {
                ddlSociedades.DataSource = gds_Sociedades.Tables["Table"];
                ddlSociedades.DataTextField = "NomSociedad";
                ddlSociedades.DataValueField = "IdSociedadGL";
                ddlSociedades.DataBind();
            }
            else
            {
                ddlSociedades.DataSource = this.LlenarTablaSociedades();
                ddlSociedades.DataBind();
            }
            
        }

        private void ConsultarOficinas(string str_IdSociedadGL)
        {
            ddlOficinas.Items.Clear();
            gds_Oficinas = ws_SGService.uwsConsultarOficinas("", str_IdSociedadGL, "", "");

            if (gds_Oficinas.Tables["Table"].Rows.Count > 0)
            {
                ddlOficinas.DataSource = gds_Oficinas.Tables["Table"];
                ddlOficinas.DataTextField = "NomOficina";
                ddlOficinas.DataValueField = "IdOficina";
                ddlOficinas.DataBind();
            }
            /*else
            {
                ddlOficinas.DataSource = this.LlenarTablaOficinas();
                ddlOficinas.DataBind();
            }*/
        }

        private DataTable LlenarTablaSociedades()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("IdSociedadGL", typeof(string));
            ldt_TablaVacia.Columns.Add("Denominacion", typeof(string));

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

            DataRow ldr_FilaTabla = ldt_TablaVacia.NewRow();
            ldt_TablaVacia.Rows.Add(ldr_FilaTabla);
            return ldt_TablaVacia;
        }

        protected void btnServicioVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Mantenimiento/frmServicios.aspx", true);
        }

        protected void btnCrearServicio_Click(object sender, EventArgs e)
        {

        }

        /*protected void btnCrearServicio_Click1(object sender, EventArgs e)
        {
            String[] str_result = new String[3];
            string str_Reserva = String.Empty;
            string str_Estado = String.Empty;

            if (ckbReserva.Checked)
                str_Reserva = "1";
            if (ckbEstado.Checked)
                str_Estado = "1";
         
            str_result = ws_SGService.uwsCrearServicio(txtIdServicio.Text,
            ddlSociedades.SelectedValue.Trim(), ddlOficinas.SelectedValue.Trim(), txtDesServicio.Text,
            Convert.ToDecimal(txtMonto.Text), str_Reserva,

            txtCCDebeActualDev.Text, txtCCHaberActualDev.Text, txtIdPosPreActualDev.Text,
            txtCCDebeActualPer.Text, txtCCHaberActualPer.Text, txtIdPosPreActualPer.Text,

            txtCCDebeVencidoDev.Text, txtCCDebeVencidoDev.Text, txtIdPosPreVencidoDev.Text,
            txtCCDebeVencidoPer.Text, txtCCDebeVencidoPer.Text, txtIdPosPreVencidoPer.Text,
            this.ckbEstado.Checked ? "A" : "I", gstr_Usuario);


            if (str_result[0].ToString().Equals("00") || str_result[0].ToString().Equals("True"))
            {
                clsSesion.Current.IdServicio = txtIdServicio.Text.Trim();
                MostarMensaje("La creación de datos ha sido satisfactoria.", lchr_MensajeExito);
                Response.Redirect("~/Mantenimiento/frmServicios.aspx", true);
            }
            else
            {
                if (String.IsNullOrEmpty(ddlOficinas.SelectedValue.Trim()))
                    MostarMensaje("La creación de datos no ha sido satisfactoria." +
                        " No existe oficina para crear el Servicio.", lchr_MensajeError);
                else
                    MostarMensaje("La creación de datos no ha sido satisfactoria.", lchr_MensajeError);

            }
        }//FUNCION*/

        protected void btnCrearServicio_Click1(object sender, EventArgs e)
        {
            bool monto_valido = this.monto_valido();
            if (monto_valido)
            {
                if (this.validar_campos_requeridos_minimos())
                {
                    this.crear_servicio();
                }
                else
                {
                    mtr_msg("Olvido ingresar el campo " + this.campo_requerido_no_encontrado + ". Este campo es requerido  !!!");
                }
            }
            else
            {
                mtr_msg("Ingrese un monto valido !!!");
            }
        }//FUNCION

        protected void crear_servicio()
        {
            String[] str_result = new String[3];
            string str_Reserva = String.Empty;
            string str_Estado = String.Empty;

            if (ckbReserva.Checked)
                str_Reserva = "1";
            if (ckbEstado.Checked)
                str_Estado = "1";

            try
            {
                str_result = ws_SGService.uwsCrearServicio(txtIdServicio.Text,
                ddlSociedades.SelectedValue.Trim(), ddlOficinas.SelectedValue.Trim(), txtDesServicio.Text,
                this.get_monto(), str_Reserva,

                txtCCDebeActualDev.Text, txtCCHaberActualDev.Text, txtIdPosPreActualDev.Text,
                txtCCDebeActualPer.Text, txtCCHaberActualPer.Text, txtIdPosPreActualPer.Text,

                txtCCDebeVencidoDev.Text, txtCCDebeVencidoDev.Text, txtIdPosPreVencidoDev.Text,
                txtCCDebeVencidoPer.Text, txtCCDebeVencidoPer.Text, txtIdPosPreVencidoPer.Text,
                this.ckbEstado.Checked ? "A" : "I", gstr_Usuario);
                if (str_result[0].ToString().Equals("00") || str_result[0].ToString().Equals("True"))
                {
                    String[] res_oficina = this.ws_SGService.uwsCrearServicioOficina(this.txtIdServicio.Text, this.ddlSociedades.SelectedValue, this.ddlOficinas.SelectedValue, this.gstr_Usuario);
                    if (res_oficina[0].ToString().Equals("00") || str_result[0].ToString().Equals("True"))
                    {
                        clsSesion.Current.IdServicio = txtIdServicio.Text.Trim();
                        MostarMensaje("La creación de datos ha sido satisfactoria.", lchr_MensajeExito);
                        Response.Redirect("~/Mantenimiento/frmServicios.aspx", true);
                    }
                    else
                    {
                        MostarMensaje("No se pudo crear el servicio de oficina.", lchr_MensajeError);
                    }
                }
                else
                {
                    if (String.IsNullOrEmpty(ddlOficinas.SelectedValue.Trim()))
                    {
                        MostarMensaje("La creación de datos no ha sido satisfactoria." +
                            " No existe oficina para crear el Servicio.", lchr_MensajeError);
                    }
                    else if (str_result[1].ToString().Contains("Violation of PRIMARY KEY"))
                    {
                        this.mtr_msg("Ingrese Un Codigo Servicio Distinto !!!");
                    }
                    else
                    {
                        MostarMensaje("La creación de datos no ha sido satisfactoria.", lchr_MensajeError);
                    }
                }
            }
            catch (Exception ee)
            {
                this.mtr_msg("ERROR >> " + ee.Message);
            }
        }//FUNCION

        protected void ddlSociedades_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSociedades.SelectedIndex < 0)
                return;
            OcultarMensaje();
            ConsultarOficinas(ddlSociedades.SelectedValue);
        }

        protected void ddlSociedades_TextChanged(object sender, EventArgs e)
        {
        }

        protected void ddlSociedades_DataBinding(object sender, EventArgs e)
        {
        }

        #region RAMSES FUNCIONES
        protected void mtr_msg(String msg)
        {
            Response.Write(String.Format("<script>alert('{0}');</script>", msg));
        }//FUNCION

        protected decimal get_monto()
        {
            try
            {
                return Convert.ToDecimal(txtMonto.Text);
            }
            catch (Exception)
            {
                return 0;
            }
        }//FUNCION

        protected bool validar_campos_requeridos_minimos()
        {
            try
            {
                if (!this.txtIdServicio.Text.Trim().Equals("") && !this.txtDesServicio.Text.Trim().Equals("") && !this.ddlOficinas.SelectedValue.ToString().Equals(""))
                {
                    return true;
                }
                else
                {
                    if (this.txtIdServicio.Text.Trim().Equals(""))
                    {
                        this.campo_requerido_no_encontrado = "Código Servicio";
                    } 
                    else if (this.ddlOficinas.SelectedValue.ToString().Equals(""))
                    {
                        this.campo_requerido_no_encontrado = "Oficina";
                    }
                    else if (this.txtDesServicio.Text.Trim().Equals(""))
                    {
                        this.campo_requerido_no_encontrado = "Descripción";
                    }
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }//FUNCION

        protected bool monto_valido()
        {
            try
            {
                if (!this.txtMonto.Text.Trim().Equals(""))
                {
                    if (this.get_monto().Equals(0))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }//FUNCION
        #endregion

    }//CLASE
}//namespace