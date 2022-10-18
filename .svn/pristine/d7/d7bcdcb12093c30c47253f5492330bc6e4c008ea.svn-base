using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Datos.ConexionSQL.Procedimientos.RevelacionNotas
{
    public class clsSubirArchivoRevelacion
    {
        public string SubirArchivo(string str_Nombre, string str_Tipo, byte[] byte_datos, int int_IdRevelacion, string str_Usuario)
        {
            string lstr_Resultado = String.Empty;
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["ConexionBD"].ConnectionString;
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand("rn.uspAdjuntarArchivoRevelacion", con))
                    {
                        SqlParameter id = new SqlParameter("@pIdRevelacion", SqlDbType.Int);
                        cmd.Parameters.Add(new SqlParameter("@pNombreArchivo", SqlDbType.VarChar,200));
                        cmd.Parameters.Add(id);
                        cmd.Parameters.Add(new SqlParameter("@pArchivo", SqlDbType.VarChar,200));
                        cmd.Parameters.Add(new SqlParameter("@pDatos",SqlDbType.VarBinary,-1));
                        cmd.Parameters.Add(new SqlParameter("@pUsrCreacion", SqlDbType.VarChar, 15));
                        cmd.Parameters["@pIdRevelacion"].Value = int_IdRevelacion;
                        cmd.Parameters["@pNombreArchivo"].Value = str_Nombre;
                        cmd.Parameters["@pArchivo"].Value = str_Tipo;
                        cmd.Parameters["@pDatos"].Value = byte_datos;
                        cmd.Parameters["@pUsrCreacion"].Value = str_Usuario;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
                lstr_Resultado = "00";
            }
            catch (Exception ex)
            {
                lstr_Resultado = "99";
            }
            return lstr_Resultado;
        }

        public clsSubirArchivoRevelacion()
        {
           
        }
    }
}