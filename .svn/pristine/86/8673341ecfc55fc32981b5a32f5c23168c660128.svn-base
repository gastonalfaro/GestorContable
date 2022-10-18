using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Datos.ConexionSQL;
using Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaInterna;
using System.Data;
using log4net;
using log4net.Config;
using LogicaNegocio.Mantenimiento;
using LogicaNegocio.Seguridad;

namespace LogicaNegocio.CalculosFinancieros.DeudaInterna
{
    public class clsCostoTransaccion
    {
        #region Variables

        private static readonly ILog Log = LogManager.GetLogger("FileAppender");
        //private wsAsientos.ServicioContable asientos = new wsAsientos.ServicioContable();
        private static clsTiposAsiento tasientos = new clsTiposAsiento();
        private wsSG.wsSistemaGestor ws_SGService = new wsSG.wsSistemaGestor();

        #endregion

        #region Métodos

        //Asientos de costos de transacción:
        //public void GenerarAsientoCostoTransaccion()
        //{
        //    //Coleccion de asientos y tipos de asientos requeridos en SAP expuestos por la referencia del servicio
        //    wsAsientos.ZfiAsiento item_asiento = new wsAsientos.ZfiAsiento();
        //    wsAsientos.ZfiAsiento item_asiento2 = new wsAsientos.ZfiAsiento();
        //    wsAsientos.ZfiAsiento[] tabla_asientos = new wsAsientos.ZfiAsiento[2];
            

        //    //variables de proceso
        //    string[] item_resAsientosLog = new string[10];
        //    string logAsiento = string.Empty;
        //    string flagEstadoAsiento = string.Empty;
            
        //    DateTime fechaContabilizacion = DateTime.Now;

        //    clsTiposAsiento lcls_asiento = new clsTiposAsiento();
        //    DataTable ldat_TiposAsientos = new DataTable();
        //    DataTable ldat_CostosTransaccion = new DataTable();
        //    string lstr_IdModulo = "IdModulo IN ('DI')";
        //    string lstr_IdOperacion = "DI01";

        //    try
        //    {
        //        ldat_CostosTransaccion = ConsultarCostoTransaccion(null, null, null, "ACT").Tables[0];

        //        for (int i = 0; i < ldat_CostosTransaccion.Rows.Count; i++)
        //        {
        //            try
        //            {
        //                string lstr_NumValor = ldat_CostosTransaccion.Rows[i]["NroValor"].ToString();
        //                DateTime ldt_Fecha = Convert.ToDateTime(ldat_CostosTransaccion.Rows[i]["Fecha"].ToString());
        //                string lstr_plazo = ldat_CostosTransaccion.Rows[i]["Plazo"].ToString();
        //                string lstr_propietario = ldat_CostosTransaccion.Rows[i]["Propietario"].ToString();
        //                string lstr_nemotecncio = ldat_CostosTransaccion.Rows[i]["NemoTecnico"].ToString();
        //                string lstr_moneda = ldat_CostosTransaccion.Rows[i]["Moneda"].ToString();
        //                string lstr_Estado = ldat_CostosTransaccion.Rows[i]["Estado"].ToString();
        //                DateTime ldt_FchModifica = Convert.ToDateTime(ldat_CostosTransaccion.Rows[i]["FchModifica"].ToString());

        //                string msjSalida = "";
        //                string numSalida = "";

        //                decimal lstr_Monto = Convert.ToDecimal(ldat_CostosTransaccion.Rows[i]["Monto"].ToString());

        //                ldat_TiposAsientos = lcls_asiento.ConsultarTiposAsiento("G206", lstr_IdModulo, lstr_IdOperacion, null, null, lstr_moneda, (lstr_plazo + '-' + lstr_propietario), lstr_nemotecncio, "ID").Tables[0];

        //                //Segun monto a enviar a SIGAF para contabilizar asiento de provision 
        //                //string montotipo = ldat_TiposAsientos.Rows[1]["CodigoAuxiliar3"].ToString();

        //                ////Llenamos los asientos

        //                if (ldat_TiposAsientos.Rows.Count != 0)
        //                {
        //                    item_asiento = new wsAsientos.ZfiAsiento();

        //                    item_asiento.Blart = ldat_TiposAsientos.Rows[0]["CodigoAuxiliar4"].ToString().Trim();//Clase de documento
        //                    item_asiento.Bukrs = ldat_TiposAsientos.Rows[0]["Codigo"].ToString().Trim();//Sociedad
        //                    item_asiento.Bldat = fechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de documento
        //                    item_asiento.Budat = fechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de contabilización
        //                    ///***************************************************Cargar cuenta 40 HABER*****************************************************/
        //                    item_asiento.Waers = ldat_TiposAsientos.Rows[0]["CodigoAuxiliar"].ToString().Trim();//Moneda 
        //                    //item_asiento.Xblnr = "REF";
        //                    //item_asiento.Bktxt = "Texto_Cabecera";
        //                    //item_asiento.Xref1Hd = idexpediente;//numero expediente 
        //                    item_asiento.Xref2Hd = lstr_IdOperacion + '-' + ldat_TiposAsientos.Rows[0]["CodigoAuxiliar2"].ToString().Trim();//CT01-AG operacion+codigoprocesal expediente
        //                    item_asiento.Bschl = ldat_TiposAsientos.Rows[0]["IdClaveContable"].ToString().Trim();//Clave de contabilización
        //                    item_asiento.Hkont = ldat_TiposAsientos.Rows[0]["IdCuentaContable"].ToString().Trim();//Cuenta de mayor
        //                    item_asiento.Wrbtr = lstr_Monto;//Importe o monto en colones a contabilizar 
        //                    //item_asiento.Zuonr = "Asig_1";
        //                    //item_asiento.Sgtxt = "SG-Liquidacion";
        //                    //item_asiento.Projk = ldat_TiposAsientos.Rows[i]["IdElementoPEP"].ToString().TrimEnd();
        //                    //item_asiento.Fipex = "NULA_SIN_PRESU";//Posición presupuestaria
        //                    //item_asiento.Kostl = ldat_TiposAsientos.Rows[i]["IdCentroCosto"].ToString();
        //                    //item_asiento.Fistl = ldat_TiposAsientos.Rows[i]["IdCentroGestor"].ToString();
        //                    //item_asiento.Prctr = ldat_TiposAsientos.Rows[i]["IdCentroBeneficio"].ToString();
        //                    //item_asiento.Measure = ldat_TiposAsientos.Rows[i]["IdPrograma"].ToString().TrimEnd();//Programa presupuestario
        //                    item_asiento.Geber = ldat_TiposAsientos.Rows[0]["IdFondo"].ToString().Trim();//Fondo
        //                    //item_asiento.Fkber = "";
        //                    //item_asiento.Xref2 = "";
        //                    tabla_asientos[0] = item_asiento;
        //                    ///***************************************************Cargar cuenta 50 DEBE*****************************************************/
        //                    item_asiento2 = new wsAsientos.ZfiAsiento();
        //                    item_asiento2.Waers = ldat_TiposAsientos.Rows[0]["CodigoAuxiliar"].ToString().Trim();//Moneda 
        //                    item_asiento2.Bschl = ldat_TiposAsientos.Rows[0]["IdClaveContable2"].ToString().Trim();//Clave de contabilización
        //                    item_asiento2.Hkont = ldat_TiposAsientos.Rows[0]["IdCuentaContable2"].ToString().Trim();//Cuenta de mayor
        //                    item_asiento2.Wrbtr = lstr_Monto;//Importe o monto en colones a contabilizar
        //                    //item_asiento2.Zuonr = "Asig_2";
        //                    //item_asiento2.Sgtxt = "SG-Liquidacion";//char 50
        //                    //item_asiento2.Kostl = ldat_TiposAsientos.Rows[i]["IdCentroCosto2"].ToString();
        //                    //item_asiento2.Fistl = ldat_TiposAsientos.Rows[i]["IdCentroGestor2"].ToString();
        //                    //item_asiento2.Prctr = ldat_TiposAsientos.Rows[i]["IdCentroBeneficio2"].ToString();
        //                    item_asiento2.Geber = ldat_TiposAsientos.Rows[0]["IdFondo2"].ToString().Trim();//Fondo
        //                    //item_asiento2.Fkber = "";
        //                    //item_asiento2.Xref2 = "xref2";
        //                    tabla_asientos[1] = item_asiento2;

        //                    //Cargar de Asientos 
        //                    string[] concatenado = new string[8];
        //                    //envio de asiento mediante servicio web hacia SIGAF
        //                    item_resAsientosLog = asientos.EnviarAsientos(tabla_asientos);
        //                    for (int j = 0; j < item_resAsientosLog.Length; j++)
        //                    {
        //                        int x = j + 1;
        //                        logAsiento += "\n" + x + "-" + item_resAsientosLog[j];
        //                    }
        //                    //MessageBox.Show("Resultado de contabilización: \n\n"+logAsiento);
        //                    Log.Info("Resultado de contabilización: \n\n" + logAsiento);
        //                    //Registrar en Bitacora de movimientos
        //                    ws_SGService.uwsRegistrarAccionBitacora("DI", "", "", "Resultado de Contabilización: " + logAsiento);
        //                    ContabilizarCostoTransaccion(lstr_NumValor, lstr_nemotecncio, ldt_Fecha, lstr_Estado, "SG", ldt_FchModifica, out numSalida, out msjSalida);
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                ex.ToString();
        //            }
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        ex.ToString();
        //    }
        //}

        public bool CrearCostoTransaccion(string lint_NumValor, string lstr_Nemotecnico, DateTime ldt_Fecha, string lstr_Moneda, decimal ldec_Monto, decimal ldec_MontoColones, decimal ldec_TpoCambio, string lstr_Detalle,
            string lstr_ModuloSINPE, string lstr_Estado, string lstr_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCreaCostoTransaccion cr_Procedimiento = new clsCreaCostoTransaccion(lint_NumValor, lstr_Nemotecnico, ldt_Fecha, lstr_Moneda, ldec_Monto, ldec_MontoColones, ldec_TpoCambio, lstr_Detalle,
                    lstr_ModuloSINPE, lstr_Estado, lstr_UsrCreacion);

                str_CodResultado = cr_Procedimiento.Lstr_CodigoResultado;
                str_Mensaje = cr_Procedimiento.Lstr_MensajeRespuesta;

                Log.Info(str_Mensaje);
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_ResCreacion = true;
                }
            }
            catch (Exception ex)
            { }
            return bool_ResCreacion;
        }

        public bool EliminarCostoTransaccion(int lint_IdCostoTransaccion, string lstr_Usuario, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsEliminaCostoTransaccion cr_Procedimiento = new clsEliminaCostoTransaccion(lint_IdCostoTransaccion, lstr_Usuario);

                str_CodResultado = cr_Procedimiento.Lstr_CodigoResultado;
                str_Mensaje = cr_Procedimiento.Lstr_MensajeRespuesta;

                Log.Info(str_Mensaje);
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_ResCreacion = true;
                }
            }
            catch (Exception ex)
            { }
            return bool_ResCreacion;
        }

        public DataSet ConsultarCostoTransaccion(string lint_IdCostoTransaccion, string lint_NumValor, string lstr_Nemotecnico, string ldt_Fecha, string lstr_Estado)
        {

            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultaCostoTransaccion cr_Procedimiento = new clsConsultaCostoTransaccion(lint_IdCostoTransaccion, lint_NumValor, lstr_Nemotecnico, ldt_Fecha, lstr_Estado);
                if (String.Equals(cr_Procedimiento.Lstr_CodigoResultado, "00"))
                {
                    lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                    lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
                }
            }
            catch (Exception ex)
            { }
            return lds_TablasConsulta;
        }

        public bool ContabilizarCalculosFinancieros(string lstr_NbrTabla, string lint_IdCostoTransaccion, string lstr_NroValor, string lstr_Nemotecnico, string lstr_Estado, string lstr_UsrModifica, DateTime ldt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsContabilizaCalculosFinancieros cr_Procedimiento = new clsContabilizaCalculosFinancieros (lstr_NbrTabla, lint_IdCostoTransaccion, lstr_NroValor, lstr_Nemotecnico, lstr_Estado, lstr_UsrModifica, ldt_FchModifica);

                str_CodResultado = cr_Procedimiento.Lstr_CodigoResultado;
                str_Mensaje = cr_Procedimiento.Lstr_MensajeRespuesta;

                Log.Info(str_Mensaje);
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_ResCreacion = true;
                }
            }
            catch (Exception ex)
            { }
            return bool_ResCreacion;
        }

        public bool ModificarCostoTransaccion(int lint_IdCostoTransaccion, string lint_NumValor, string lstr_Nemotecnico, DateTime ldt_Fecha, string lstr_Moneda, decimal ldec_Monto, decimal ldec_MontoColones, decimal ldec_TpoCambio, string lstr_Detalle,
            string lstr_ModuloSINPE, string lstr_Estado, string lstr_UsrModifica, DateTime ldt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsModificaCostoTransaccion cr_Procedimiento = new clsModificaCostoTransaccion(lint_IdCostoTransaccion, lint_NumValor, lstr_Nemotecnico, ldt_Fecha, lstr_Moneda, ldec_Monto, ldec_MontoColones, ldec_TpoCambio, lstr_Detalle,
                    lstr_ModuloSINPE, lstr_Estado, lstr_UsrModifica, ldt_FchModifica);

                str_CodResultado = cr_Procedimiento.Lstr_CodigoResultado;
                str_Mensaje = cr_Procedimiento.Lstr_MensajeRespuesta;

                Log.Info(str_Mensaje);
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_ResCreacion = true;
                }
            }
            catch (Exception ex)
            { }
            return bool_ResCreacion;
        }

        #endregion

        #region Constructor

        public clsCostoTransaccion()
        { }

        #endregion
    }
}