using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogicaNegocio.Seguridad
{
    public class tSeguridad
    {
        private string aArchivo = string.Empty;
        private string aContrasena = System.Configuration.ConfigurationManager.AppSettings["ContrasenaReporting"];

        public tSeguridad() { this.aArchivo = System.Configuration.ConfigurationManager.AppSettings["ErrorLog"]; }
     
        public bool PoseePermiso(List<string> lstr_Permisos, string str_IdObjeto)
        {
            foreach (string str_Permiso in lstr_Permisos)
            {
                //if str_Permiso.
            }
            bool lbool_EstaAutorizado = false;
            return lbool_EstaAutorizado;
        }

        /// <summary>
        /// Guarda asyncronamente la excepcion en un archivo local para su porterior revision y la envia por correo electronico
        /// </summary>

        public void SaveError(System.Exception pExcepcion)
        {
            try
            {
                using (System.IO.StreamWriter vArchivo = new System.IO.StreamWriter(this.aArchivo, true, System.Text.Encoding.Unicode))
                {
                    vArchivo.WriteLine("██████████████████████████████████████████████████████████████████████████████████████████████████████████");
                    vArchivo.WriteLine("█");
                    vArchivo.WriteLine("█ " + System.DateTime.Now + "             " + pExcepcion.Message.ToUpper());
                    vArchivo.WriteLine("█ ");
                    vArchivo.WriteLine("█████████████████████████████████████████████████████████████████████████████████████████████████████████");
                    vArchivo.WriteLine();
                    vArchivo.WriteLine(pExcepcion.ToString());
                    vArchivo.WriteLine();
                    vArchivo.Close();
                    vArchivo.Dispose();
                }
            }
            catch { }
        }
        /// <summary>
        /// Decifra el texto ingresado
        /// </summary>   
        public string DescifrarTextoAES(string pTextoDescifrar)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(pTextoDescifrar);
            result = System.Text.Encoding.Unicode.GetString(decryted);
            return result;
        }

        /// <summary>
        /// Cifra el texto recibido
        /// </summary>
        public string CifrarTextoAES(string pTextoCifrar)
        {
            string result = string.Empty;
            byte[] encryted = System.Text.Encoding.Unicode.GetBytes(pTextoCifrar);
            result = Convert.ToBase64String(encryted);
            return result;
        }
    }
}