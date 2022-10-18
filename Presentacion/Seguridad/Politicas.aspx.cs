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

namespace Presentacion.Seguridad
{
    public partial class Politicas : BASE
    {
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private string str_Usuario = String.Empty;
        
        protected void Page_Load(object sender, EventArgs e)
        {
           
            str_Usuario = clsSesion.Current.LoginUsuario;

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(str_Usuario))
                {
                    if (!clsSeguridadVistas.MostrarElementos(str_Usuario, Master, "OBJ_SG"))
                    {
                        MessageBox.Show("No tiene suficientes permisos.");
                        Response.Redirect("~/Principal.aspx", true);
                    }
                    else
                    {
                        ConsultarPoliticas();
                    }

                }
                else
                {
                    Response.Redirect("~/Login.aspx", true);
                }

            }
            else
            {
                if (string.IsNullOrEmpty(str_Usuario))
                    Response.Redirect("~/Login.aspx", true);

            }
        }

        /// <summary>
        /// Evento que se dispara al hacer clic sobre btnGuardar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string lstr_TiempoOcio = txtTmpOcio.Text;
            string lstr_BloqueoClave = txtBloqueoClave.Text;
            string lstr_CantLetrasCave = txtCantLetrasClave.Text;
            string lstr_CantNumeros = txtCantNumeros.Text;
            string lstr_VigenciaClave = txtVigenciaClave.Text;
            string lstr_CantCaracteres = txtCantCaracteres.Text;
            string lstr_IntentosFallidos = txtMaxIntentosFallidos.Text;
            string lstr_ReutilizacionClave = txtReutilizacionClave.Text;
           
            int lint_TamanoClave = Convert.ToInt32(lstr_CantLetrasCave) + Convert.ToInt32(lstr_CantCaracteres) + Convert.ToInt32(lstr_CantNumeros);
            DateTime lstr_FechaModificado = Convert.ToDateTime(clsSesion.Current.FechaUltimaConsulta);
            string lstr_fecha = String.Empty;
            lstr_fecha = lstr_FechaModificado.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            bool lboo_ResActualizacion = ws_SGService.uwsActualizarPoliticasSeguridad(
                lstr_TiempoOcio, 
                "1", 
                lstr_IntentosFallidos, 
                lstr_VigenciaClave,
                lstr_BloqueoClave, 
                Convert.ToString(lint_TamanoClave), 
                lstr_CantLetrasCave, 
                lstr_CantNumeros,
                lstr_CantCaracteres, 
                lstr_ReutilizacionClave, 
                "0", 
                clsSesion.Current.LoginUsuario, lstr_fecha);
            if (lboo_ResActualizacion)
            {
                MessageBox.Show("Los cambios han sido guardados.");
            }
            else
            {
                MessageBox.Show("Error al realizar los cambios.");
            }
        }

        /// <summary>
        /// Consulta las politicas actuales para el sitio
        /// </summary>
        private void ConsultarPoliticas()
        {            
            DataSet ldst_Politicas = ws_SGService.uwsConsultarPoliticas("", "");
            if (ldst_Politicas.Tables.Count > 0 && ldst_Politicas.Tables[0].Rows.Count > 0)
            {
                DataTable ldt_Politica = ldst_Politicas.Tables["Table"];
                txtMaxIntentosFallidos.Text = ldt_Politica.Rows[0]["MaxNroIntentosFallidos"].ToString();
                txtBloqueoClave.Text = ldt_Politica.Rows[0]["TiempoBloqueoClave"].ToString();
                txtCantLetrasClave.Text = ldt_Politica.Rows[0]["MinLetrasClave"].ToString();
                txtCantNumeros.Text = ldt_Politica.Rows[0]["MinNumerosClave"].ToString();
                txtCantCaracteres.Text = ldt_Politica.Rows[0]["MinCaracteresClave"].ToString();
                txtTmpOcio.Text = ldt_Politica.Rows[0]["TiempoOcio"].ToString();
                txtVigenciaClave.Text = ldt_Politica.Rows[0]["MaxVigenciaClave"].ToString();
                txtReutilizacionClave.Text = ldt_Politica.Rows[0]["NroReutilizacionUltimasClaves"].ToString();
                clsSesion.Current.FechaUltimaConsulta = ldt_Politica.Rows[0]["FchModifica"].ToString();
            }//if
        }

        /// <summary>
        /// Evento que se dispara al hacer clic en atras
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAtras_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Seguridad/Usuarios.aspx");
        }

    }
}