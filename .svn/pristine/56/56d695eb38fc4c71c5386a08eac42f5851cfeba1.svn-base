using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;


namespace LogicaNegocio.Contingentes
{
    public class clsBitacoraDeMovimientosCuentasExpedientes
    {
        private static readonly ILog llogger_Log = LogManager.GetLogger("BitacoraDeMovimientosCuentasExpedientes");

        public string[] RegistrarBitacoraDeMovimientosCuentasExpedientes(string str_IdExpediente, string str_IdModulo, string str_Sociedad, string str_NroAsiento, string lstr_TipoCuenta, decimal dec_Debe, decimal dec_Haber, decimal dec_Monto, decimal dec_SaldoAcumulado, string str_DetalleTransaccion, string str_UsrCreacion)
        {
            //variables locales
            Datos.ConexionSQL.Procedimientos.Contigentes.clsRegistrarBitacoraDeMovimientosCuentas l_Bitacora = new Datos.ConexionSQL.Procedimientos.Contigentes.clsRegistrarBitacoraDeMovimientosCuentas();
            string[] lstr_result = new string[2];
            //insertar codigo de mappeo de datos
            try
            {
                l_Bitacora.Lstr_IdExpediente = str_IdExpediente;
                l_Bitacora.Lstr_IdModulo = str_IdModulo;
                l_Bitacora.Lstr_IdSociedadGL = str_Sociedad;
                l_Bitacora.Lstr_NroAsiento = str_NroAsiento;
                l_Bitacora.Lstr_TipoCuenta = lstr_TipoCuenta;
                l_Bitacora.Ldec_Debe= dec_Debe;
                l_Bitacora.Ldec_Haber= dec_Haber;
                l_Bitacora.Ldec_Monto = dec_Monto;
                l_Bitacora.Ldec_SaldoAcumulado= dec_SaldoAcumulado;
                l_Bitacora.Lstr_DetalleTransaccion = str_DetalleTransaccion;
                l_Bitacora.Lstr_UsrCreacion = str_UsrCreacion;
           
                //****** Acceso a datos en SQL conexion ******/// 
                bool process = l_Bitacora.ProcesarSPRegistrarBitacoraDeMovimientosCuentas();//Realizamos el mappeo en la BD
                //******* Resultado de procesar store procedure ****//
                lstr_result[0] = "Codigo error:" + l_Bitacora.Lstr_CodigoResultado + "-" + l_Bitacora.Lstr_CodigoResultado;
                lstr_result[1] = "Resultado inserción de la bitacora de movimientos de la cuenta> " + l_Bitacora.Lstr_MensajeRespuesta;

            }
            catch (Exception ex)
            {

                //logger 
                llogger_Log.Error("Error RegsitrarBitacoraCuentas: " + ex.Message + "- BD mensaje " + l_Bitacora.Lstr_MensajeRespuesta);

            }

            return lstr_result;

        }
    }
}