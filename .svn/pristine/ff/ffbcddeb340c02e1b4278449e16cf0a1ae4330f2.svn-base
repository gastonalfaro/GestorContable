using Presentacion.Compartidas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Seguridad
{
    public partial class Bitacora : BASE
    {
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private string gstr_Usuario = String.Empty;

        private List<string> DatosConsulta
        {
            get
            {
                return (List<string>)ViewState["Consulta"];
            }
            set
            {
                ViewState["Consulta"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {            
            gstr_Usuario = clsSesion.Current.LoginUsuario;

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(gstr_Usuario))
                {
                    if (!clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "OBJ_SG"))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                    {
                        CargarDDLs();
                        this.txtFechaDesde.Text = DateTime.Today.AddYears(-1).ToString("dd/MM/yyyy");
                        this.txtFechaHasta.Text = DateTime.Today.ToString("dd/MM/yyyy");
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

        /// <summary>
        /// Realiza la consulta de los datos en bitacora
        /// </summary>
        /// <param name="str_FechaInicio"></param>
        /// <param name="str_FechaFinal"></param>
        /// <param name="str_IdRegistro"></param>
        /// <param name="str_IdUsuario"></param>
        /// <param name="str_IdModulo"></param>
        /// <param name="str_Accion"></param>
        private void ConsultarBitacora(string str_FechaInicio, string str_FechaFinal, string str_IdRegistro,
            string str_IdUsuario, string str_IdModulo, string str_Accion)
        {
          
            gvBitacora.Visible = false;
            if (Convert.ToDateTime(str_FechaInicio) > Convert.ToDateTime(str_FechaFinal))
            {
                lblSinResultados.Text = "El rango de fechas es incorrecto.";
                lblSinResultados.Visible = true;
            }
            else
            {
                    
                DataSet lds_Bitacora = ws_SGService.uwsConsultarBitacoras(str_FechaInicio, str_FechaFinal, str_IdRegistro,
                    str_IdUsuario, str_IdModulo, str_Accion);
                if (lds_Bitacora.Tables.Count > 0) {
                    if (lds_Bitacora.Tables["Table"].Rows.Count > 0)
                    {
                        gvBitacora.DataSource = lds_Bitacora.Tables["Table"];
                        gvBitacora.DataBind();
                        lblSinResultados.Visible = false;
                        gvBitacora.Visible = true;
                    }
                    else
                    {
                        lblSinResultados.Text = "La búsqueda no produjo resultados";
                        lblSinResultados.Visible = true;
                    }
                }
                else
                {
                    lblSinResultados.Text = "La búsqueda no produjo resultados";
                    lblSinResultados.Visible = true;
                }
            }

        }

        /// <summary>
        /// Llena los dropdownlisto con la informacion respectiva
        /// </summary>
        private void CargarDDLs()
        {
            ddlModulo.Items.Clear();
            DataSet lds_Modulos = ws_SGService.uwsConsultarModulos("", "");
            if (lds_Modulos.Tables.Count > 0)
            {
                DataTable ldt_Modulos = lds_Modulos.Tables["Table"];
                ddlModulo.DataSource = ldt_Modulos;
                ddlModulo.DataTextField = "NomModulo";
                ddlModulo.DataValueField = "IdModulo";
                ddlModulo.DataBind();
                ddlModulo.Items.Insert(0, new ListItem("", ""));
            }

            DataSet lds_Acciones = ws_SGService.uwsConsultarDinamico("select distinct b.Accion from  sg.BitacoraErrores b  order by 1");
            if (lds_Acciones.Tables.Count > 0)
            {
                DataTable ldt_Acciones = lds_Acciones.Tables["Table"];
                ddlAccion.DataSource = ldt_Acciones;
                ddlAccion.DataTextField = "Accion";
                ddlAccion.DataValueField = "Accion";
                ddlAccion.DataBind();
                ddlAccion.Items.Insert(0, new ListItem("", ""));
            }

        }

        /// <summary>
        /// Evento que se realiza al hacer clic sobre el btnConsultar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            //string lstr_FechaDesde = calDesde.SelectedDate.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            //string lstr_FechaHasta = calHasta.SelectedDate.ToString("dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            string lstr_UsuarioBusqueda = txtUsuario.Text.Trim();
            string lstr_Modulo = ddlModulo.SelectedValue.Trim();
            string lstr_Accion = ddlAccion.SelectedValue.Trim();
            ConsultarBitacora(this.txtFechaDesde.Text, this.txtFechaHasta.Text, "", lstr_UsuarioBusqueda, lstr_Modulo, lstr_Accion);
            List<string> ParametrosConsulta = new List<string>() { this.txtFechaDesde.Text, this.txtFechaHasta.Text, "", lstr_UsuarioBusqueda, lstr_Modulo, lstr_Accion };
            DatosConsulta = ParametrosConsulta;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvBitacora_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBitacora.PageIndex = e.NewPageIndex;
            ConsultarBitacora(DatosConsulta.ElementAt(0), DatosConsulta.ElementAt(1), DatosConsulta.ElementAt(2),
                DatosConsulta.ElementAt(3), DatosConsulta.ElementAt(4), DatosConsulta.ElementAt(5));
        }

    }
}