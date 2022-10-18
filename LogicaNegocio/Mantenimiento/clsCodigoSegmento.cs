using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace LogicaNegocio.Mantenimiento
{
    public class clsCodigoSegmento
    {

        public string CrearActualizarCodSegmento(string pAccion, string pIdEntidad, string pIdSegmento, string @pIdUsuario)
        {
            SqlCommand _comando = MetodosBD.CrearComandoProc("ma.uspCrearActualizarCodigoSegmento");
            _comando.Parameters.AddWithValue("@pAccion", pAccion);
            _comando.Parameters.AddWithValue("@pIdEntidad", pIdEntidad);
            _comando.Parameters.AddWithValue("@pIdSegmento", pIdSegmento);
            _comando.Parameters.AddWithValue("@pUsuario", @pIdUsuario);
            _comando.Parameters.Add("@pResultado", SqlDbType.Char, 5);
            _comando.Parameters["@pResultado"].Direction = ParameterDirection.Output;
            _comando.Parameters.Add("@pMensaje", SqlDbType.Char, 500);
            _comando.Parameters["@pMensaje"].Direction = ParameterDirection.Output;
            string ResultEjecuta = MetodosBD.EjecutarComando(_comando).ToString();

            string respuesta = _comando.Parameters["@pResultado"].Value.ToString();
            string mensaje = _comando.Parameters["@pMensaje"].Value.ToString();

            return respuesta + "-" + mensaje;

        }

        public DataSet GetCodigosSegmento(string pIdEntidad, string pBuscar)
        {
            DataSet ResultEjecuta = null;
            try
            {
                SqlCommand _comando = MetodosBD.CrearComandoProc("ma.uspConsultarCodSegmento");
                _comando.Parameters.AddWithValue("@pIdSociedadGL", pIdEntidad);
                _comando.Parameters.AddWithValue("@pBuscar", pBuscar);
                ResultEjecuta = MetodosBD.EjecutarSP(_comando);
            }
            catch(Exception e)
            {
                string err = e.Message.ToString();
            }

            return ResultEjecuta;
        }
    }
}