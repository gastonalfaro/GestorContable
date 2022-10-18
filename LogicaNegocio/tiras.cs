using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogicaNegocio
{
    public class tiras
    {
        private static Mantenimiento.clsDinamico dinamica = new Mantenimiento.clsDinamico();

        public string get_operation_name(string operation_id, string module_id)
        {
            string operation_name = operation_id;
            try
            {
                string query = string.Format("SELECT substring(NomOperacion,0,25) AS NomOperacion FROM ma.Operaciones where idoperacion = '{0}' and idmodulo = '{1}' and estado = 'A'", operation_id, module_id);
                string temp_name = dinamica.ConsultarDinamico(query).Tables[0].Rows[0]["NomOperacion"].ToString();
                if (!string.IsNullOrEmpty(temp_name))
                {
                    operation_name = temp_name;
                }
            }
            catch (Exception ex)
            {
                //No existe nombre de operacion
            }
            return operation_name;            
        }
    }
}