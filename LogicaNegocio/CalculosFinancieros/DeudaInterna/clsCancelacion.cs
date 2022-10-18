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

namespace LogicaNegocio.CalculosFinancieros.DeudaInterna
{
    public class clsCancelacion
    {
        #region Variables

        private static readonly ILog Log = LogManager.GetLogger("FileAppender");
        //private wsAsientos.ServicioContable asientos = new wsAsientos.ServicioContable();
        private static clsTiposAsiento tasientos = new clsTiposAsiento();
        private static clsOperaciones loperacion = new clsOperaciones();
        private wsSG.wsSistemaGestor ws_SGService = new wsSG.wsSistemaGestor();

        #endregion

        #region Métodos

        public bool CrearCancelacion(int lint_NumValor, decimal ldec_ValorFacial, decimal ldec_ValorTransadoBruto, decimal ldec_RendimientoPorDescuento, DateTime ldt_FchColocacion,
            DateTime ldt_FchCancelacion, string lstr_Nemotecnico, string lstr_Moneda, string lstr_Propiedad, decimal ldec_TasaBruta, decimal ldec_Margen, decimal ldec_ValorTransadoNeto,
            decimal ldec_TasaNeta, decimal ldec_Premio, string lstr_Estado, string lstr_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCreaCancelacion cr_Procedimiento = new clsCreaCancelacion(lint_NumValor, ldec_ValorFacial, ldec_ValorTransadoBruto, ldec_RendimientoPorDescuento, ldt_FchColocacion,
                    ldt_FchCancelacion, lstr_Nemotecnico, lstr_Moneda, lstr_Propiedad, ldec_TasaBruta, ldec_Margen, ldec_ValorTransadoNeto,
                    ldec_TasaNeta, ldec_Premio, lstr_Estado, lstr_UsrCreacion);

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

        

        public void GenerarAsientoCancelacion(string lstr_NumValor,
                        DateTime ldt_Fecha,
                        string lstr_plazo,
            //string lstr_propietario,
                        string lstr_nemotecnico,
                        string lstr_moneda,
                        string lstr_Estado//,
            //DateTime ldt_FchModifica
            )
        {
            //Coleccion de asientos y tipos de asientos requeridos en SAP expuestos por la referencia del servicio
            wrSigafAsientos.ZfiAsiento item_asiento = new wrSigafAsientos.ZfiAsiento();
            wrSigafAsientos.ZfiAsiento item_asiento2 = new wrSigafAsientos.ZfiAsiento();
            wrSigafAsientos.ZfiAsiento[] tabla_asientos = new wrSigafAsientos.ZfiAsiento[2];


            //variables de proceso
            string[] item_resAsientosLog = new string[10];
            string logAsiento = string.Empty;
            string flagEstadoAsiento = string.Empty;

            DateTime fechaContabilizacion = DateTime.Now;

            clsTiposAsiento lcls_asiento = new clsTiposAsiento();
            DataTable ldat_TiposAsientos = new DataTable();
            DataTable ldat_CostosTransaccion = new DataTable();
            string lstr_IdModulo = "IdModulo IN ('DI')";
            string lstr_IdOperacion = "DI01";
            string lstr_NomOperacion = String.Empty;

            DataSet lds_Operaciones = loperacion.ConsultarOperaciones(lstr_IdOperacion, "IdModulo IN ('DI')", "");
            if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
            {
                lstr_NomOperacion = lds_Operaciones.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
            }

            try
            {
                //ldat_CostosTransaccion = ConsultarCostoTransaccion(null, null, null, "ACT").Tables[0];

                for (int i = 0; i < ldat_CostosTransaccion.Rows.Count; i++)
                {
                    try
                    {
                        //string lstr_NumValor = ldat_CostosTransaccion.Rows[i]["NroValor"].ToString();
                        //DateTime ldt_Fecha = Convert.ToDateTime(ldat_CostosTransaccion.Rows[i]["Fecha"].ToString());
                        //string lstr_plazo = ldat_CostosTransaccion.Rows[i]["Plazo"].ToString();
                        //string lstr_propietario = ldat_CostosTransaccion.Rows[i]["Propietario"].ToString();
                        //string lstr_nemotecnico = ldat_CostosTransaccion.Rows[i]["NemoTecnico"].ToString();
                        //string lstr_moneda = ldat_CostosTransaccion.Rows[i]["Moneda"].ToString();
                        //string lstr_Estado = ldat_CostosTransaccion.Rows[i]["Estado"].ToString();
                        //DateTime ldt_FchModifica = Convert.ToDateTime(ldat_CostosTransaccion.Rows[i]["FchModifica"].ToString());

                        string msjSalida = "";
                        string numSalida = "";

                        decimal lstr_Monto = Convert.ToDecimal(ldat_CostosTransaccion.Rows[i]["Monto"].ToString());

                        ldat_TiposAsientos = lcls_asiento.ConsultarTiposAsiento("G206", lstr_IdModulo, lstr_IdOperacion, null, null, lstr_moneda, (lstr_plazo), lstr_nemotecnico, "ID").Tables[0];

                        //Segun monto a enviar a SIGAF para contabilizar asiento de provision 
                        //string montotipo = ldat_TiposAsientos.Rows[1]["CodigoAuxiliar3"].ToString();

                        ////Llenamos los asientos

                        if (ldat_TiposAsientos.Rows.Count != 0)
                        {
                            item_asiento = new wrSigafAsientos.ZfiAsiento();

                            item_asiento.Blart = ldat_TiposAsientos.Rows[0]["CodigoAuxiliar4"].ToString().Trim();//Clase de documento
                            item_asiento.Bukrs = ldat_TiposAsientos.Rows[0]["Codigo"].ToString().Trim();//Sociedad
                            item_asiento.Bldat = fechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de documento
                            item_asiento.Budat = fechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de contabilización
                            ///***************************************************Cargar cuenta 40 HABER*****************************************************/
                            item_asiento.Waers = ldat_TiposAsientos.Rows[0]["CodigoAuxiliar"].ToString().Trim();//Moneda 
                            item_asiento.Xblnr = lstr_NumValor+"."+lstr_nemotecnico;
                            item_asiento.Bktxt = "";
                            item_asiento.Xref1Hd = lstr_moneda;
                            //item_asiento.Xref1Hd = idexpediente;//numero expediente 
                            item_asiento.Sgtxt = lstr_IdOperacion + '.' + ldat_TiposAsientos.Rows[0]["CodigoAuxiliar2"].ToString().Trim();//CT01-AG operacion+codigoprocesal expediente
                            item_asiento.Xref2Hd = lstr_IdOperacion + "." + lstr_NomOperacion;
                            item_asiento.Bschl = ldat_TiposAsientos.Rows[0]["IdClaveContable"].ToString().Trim();//Clave de contabilización
                            item_asiento.Hkont = ldat_TiposAsientos.Rows[0]["IdCuentaContable"].ToString().Trim();//Cuenta de mayor
                            item_asiento.Wrbtr = lstr_Monto;//Importe o monto en colones a contabilizar 
                            //item_asiento.Zuonr = "Asig_1";
                            //item_asiento.Sgtxt = "SG-Liquidacion";
                            //item_asiento.Projk = ldat_TiposAsientos.Rows[i]["IdElementoPEP"].ToString().TrimEnd();
                            //item_asiento.Fipex = "NULA_SIN_PRESU";//Posición presupuestaria
                            //item_asiento.Kostl = ldat_TiposAsientos.Rows[i]["IdCentroCosto"].ToString();
                            //item_asiento.Fistl = ldat_TiposAsientos.Rows[i]["IdCentroGestor"].ToString();
                            //item_asiento.Prctr = ldat_TiposAsientos.Rows[i]["IdCentroBeneficio"].ToString();
                            //item_asiento.Measure = ldat_TiposAsientos.Rows[i]["IdPrograma"].ToString().TrimEnd();//Programa presupuestario
                            item_asiento.Geber = ldat_TiposAsientos.Rows[0]["IdFondo"].ToString().Trim();//Fondo
                            //item_asiento.Fkber = "";
                            //item_asiento.Xref2 = "";
                            tabla_asientos[0] = item_asiento;
                            ///***************************************************Cargar cuenta 50 DEBE*****************************************************/
                            item_asiento2 = new wrSigafAsientos.ZfiAsiento();
                            item_asiento2.Waers = ldat_TiposAsientos.Rows[0]["CodigoAuxiliar"].ToString().Trim();//Moneda 
                            item_asiento2.Bschl = ldat_TiposAsientos.Rows[0]["IdClaveContable2"].ToString().Trim();//Clave de contabilización
                            item_asiento2.Hkont = ldat_TiposAsientos.Rows[0]["IdCuentaContable2"].ToString().Trim();//Cuenta de mayor
                            item_asiento2.Wrbtr = lstr_Monto;//Importe o monto en colones a contabilizar
                            //item_asiento2.Zuonr = "Asig_2";
                            //item_asiento2.Sgtxt = "SG-Liquidacion";//char 50
                            //item_asiento2.Kostl = ldat_TiposAsientos.Rows[i]["IdCentroCosto2"].ToString();
                            //item_asiento2.Fistl = ldat_TiposAsientos.Rows[i]["IdCentroGestor2"].ToString();
                            //item_asiento2.Prctr = ldat_TiposAsientos.Rows[i]["IdCentroBeneficio2"].ToString();
                            item_asiento2.Geber = ldat_TiposAsientos.Rows[0]["IdFondo2"].ToString().Trim();//Fondo
                            //item_asiento2.Fkber = "";
                            //item_asiento2.Xref2 = "xref2";
                            tabla_asientos[1] = item_asiento2;

                            //Cargar de Asientos 
                            string[] concatenado = new string[8];
                            //envio de asiento mediante servicio web hacia SIGAF
                            item_resAsientosLog = tasientos.EnviarAsientos(tabla_asientos, "");
                            for (int j = 0; j < item_resAsientosLog.Length; j++)
                            {
                                int x = j + 1;
                                logAsiento += "\n" + x + "-" + item_resAsientosLog[j];
                            }
                            //MessageBox.Show("Resultado de contabilización: \n\n"+logAsiento);
                            Log.Info("Resultado de contabilización: \n\n" + logAsiento);
                            //Registrar en Bitacora de movimientos
                            ws_SGService.uwsRegistrarAccionBitacora("DI", "", "", "Resultado de Contabilización: " + logAsiento);
                            //ContabilizarCostoTransaccion(lstr_NumValor, lstr_nemotecnico, ldt_Fecha, lstr_Estado, "SG", ldt_FchModifica, out numSalida, out msjSalida);
                        }
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        #endregion
    }
}