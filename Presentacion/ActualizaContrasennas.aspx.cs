using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.Seguridad;
using LogicaNegocio.Mantenimiento;
using System.Configuration;
//using System.Data.SqlClient;

namespace Presentacion
{
    public partial class ActualizaContrasennas : BASE
    {
        private tSeguridad gcls_Seguridad = new tSeguridad();
        private clsDinamico gDinamico = new clsDinamico();
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();

        protected void Page_Load(object sender, EventArgs e) {  }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            DataSet vDatos = ws_SGService.uwsConsultarUsuarios("", "", "");
           
            foreach (DataTable table in vDatos.Tables)
            {
                foreach (DataRow dr in table.Rows)
                {
                    string vNombre = dr["IdUsuario"].ToString();

                    //string vContraseña = gcls_Seguridad.CifrarTextoAES(vNombre + "123");//dr["Clave"].ToString());
                    string vContraseña = gcls_Seguridad.CifrarTextoAES(vNombre + dr["Clave"].ToString()); //cucurucho

                    string str_consul = "UPDATE sg.Usuarios SET Clave = '" +  vContraseña + "' WHERE IdUsuario='" + vNombre + "'";
                    EjecutarComando(str_consul);

                }
            }

        }


        private object EjecutarComando(string lstr_query)
        {
            /*string lstr_ConnString = ConfigurationManager.ConnectionStrings["GestNICSPDEVConnectionString"].ConnectionString;
           
            using (SqlConnection vConexion = new SqlConnection(lstr_ConnString))
            using (SqlCommand vComando = new SqlCommand(lstr_query, vConexion))
            {
                vComando.CommandTimeout = vComando.Connection.ConnectionTimeout;

                if (vConexion.State == ConnectionState.Closed) vConexion.Open();

                return vComando.ExecuteScalar();
            }*/
            DataSet ds = new DataSet();
            ds = ws_SGService.uwsConsultarDinamico(lstr_query);
            if (ds.Tables.Count > 0)
            {
                return ds.Tables["Table"];
            }
            return null;
            
        }

        /// <summary>
        /// Obtener consultas rapidas
        /// </summary>
        /// <param name="lstr_query"></param>
        /// <returns></returns>
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


        protected void btnActualizarAntiguas_Click(object sender, EventArgs e)
        {
            DateTime vFecha = Convert.ToDateTime("2016-05-12");
//            string str_consulSel = "SELECT IdUsuario, Clave FROM sg.antiguasclavesusuario where fchmodifica < '2016-05-12'";
            string str_consulSel = "SELECT IdUsuario, Clave FROM sg.antiguasclavesusuario";


            DataTable vDatosClaves = GetData(str_consulSel);
            if (vDatosClaves.Rows.Count > 0)
            {
                foreach (DataRow campo in vDatosClaves.Rows)
                {
                    string vNombre = campo["IdUsuario"].ToString();

                    string vContraseña = gcls_Seguridad.CifrarTextoAES(vNombre + campo["Clave"].ToString());

                    string str_consul = "UPDATE sg.antiguasclavesusuario SET Clave = '" + vContraseña + "' WHERE IdUsuario='" + vNombre + "'";
                    EjecutarComando(str_consul);
                }
            }

        }

        protected void btnEncriptaContrasena_Click(object sender, EventArgs e)
        {

            this.lblTexto.Text = gcls_Seguridad.CifrarTextoAES(this.txtContrasena.Text);

        }

        protected void btnDesencriptaContrasena_Click(object sender, EventArgs e)
        {
            this.lblDesencriptado.Text = gcls_Seguridad.DescifrarTextoAES(this.txtDesencripta.Text);
        }

    }
}