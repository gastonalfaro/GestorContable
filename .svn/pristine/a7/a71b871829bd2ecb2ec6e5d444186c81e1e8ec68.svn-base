using System;
using System.Globalization;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Datos.ConexionSQL;
using Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna;
using System.Data;
using log4net;
using log4net.Config;
using LogicaNegocio.Mantenimiento;
using LogicaNegocio.Seguridad;

namespace LogicaNegocio.CalculosFinancieros.DeudaExterna
{
    public class clsPrestamo
    {
        private static readonly ILog Log = LogManager.GetLogger("FileAppender");
        //private wsAsientos.ServicioContable asientos = new wsAsientos.ServicioContable();
        private static clsTiposAsiento tasientos = new clsTiposAsiento();
        private static string lstr_formato_fecha = "dd/MM/yyyy";
        private tBitacora reg_Bitacora = new tBitacora();
        private clsTiposAsiento tAsiento = new clsTiposAsiento();

        private string resAsientosLog = string.Empty;
        //private wsSG.wsSistemaGestor ws_SGService = new wsSG.wsSistemaGestor();
        
        #region Métodos

        public DataSet ConsultarPrestamo(string lstr_IdPrestamo = null, DateTime? ldt_FechaInicio = null, DateTime? ldt_FechaFin = null, string lstr_Fuente = null,
            string lstr_Situacion = null, string lstr_Plazo = null, string lstr_Nombre = null, string lstr_NbrAcreedor = null, string lstr_CatAcreedor = null,
            string lstr_TpoAcreedor = null, string lstr_NbrDeudor = null, string lstr_CatDeudor = null, string lstr_TipoPrestamo = null)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultaPrestamo cr_Procedimiento = new clsConsultaPrestamo(lstr_IdPrestamo, ldt_FechaInicio, ldt_FechaFin, lstr_Fuente,
                                                                               lstr_Situacion, lstr_Plazo, lstr_Nombre, lstr_NbrAcreedor, lstr_CatAcreedor,
                                                                               lstr_TpoAcreedor, lstr_NbrDeudor, lstr_CatDeudor, lstr_TipoPrestamo);
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


        public DataSet ConsultarPrestamosReclasificar(DateTime? ldt_FechaFin = null)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarPrestamosReclasificar cr_Procedimiento = new clsConsultarPrestamosReclasificar(ldt_FechaFin);
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

        public bool CrearPrestamo(string lstr_IdPrestamo, string lstr_Fuente, string lstr_Situacion, string lstr_Plazo,
            string lstr_Nombre, DateTime ldt_Firmado, DateTime ldt_LimiteGiro, DateTime ldt_LimiteEfectivo, DateTime ldt_Efectivo,
            decimal ldec_Monto, string lstr_IdMoneda, string lstr_TipoTramo, string lstr_Proposito, string lstr_GarantiaPublica,
            string lstr_OrigenDeuda, int lint_IdAcreedor, int lint_IdDeudor, string lstr_TipoPrestamo, decimal ldec_Tasa,
            string lstr_NbrAcreedor, string lstr_CatAcreedor, string lstr_TpoAcreedor, string lstr_NbrDeudor,
            string lstr_CatDeudor, string lstr_CondicionPrestamo, string lstr_ExisteObligacion,
            string lstr_CondicionMotivo, decimal ldec_CondicionTasa, decimal ldec_CondicionMonto, DateTime ldt_CondicionFchInicio,
            DateTime ldt_CondicionFchFin, string lstr_Estado, string lstr_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                //if (lstr_IdMoneda != "EUR" && lstr_IdMoneda != "CRC" && lstr_IdMoneda != "USD")
                //{
                //    clsCalculosDeudaExterna lcls_TipoCambio = new clsCalculosDeudaExterna();
                //    DateTime ldt_FechaActual = DateTime.UtcNow.Date;
                //    ldec_Monto = lcls_TipoCambio.ConvertirdorDeMoneda(lstr_IdMoneda, ldt_FechaActual, ldec_Monto, out str_Mensaje);
                //    lstr_IdMoneda = "USD";
                //}
                clsCreaPrestamo cr_Procedimiento = new clsCreaPrestamo(lstr_IdPrestamo, lstr_Fuente, lstr_Situacion, lstr_Plazo,
                        lstr_Nombre, ldt_Firmado, ldt_LimiteGiro, ldt_LimiteEfectivo, ldt_Efectivo, ldec_Monto, lstr_IdMoneda, lstr_TipoTramo,
                        lstr_Proposito, lstr_GarantiaPublica, lstr_OrigenDeuda, lint_IdAcreedor, lint_IdDeudor, lstr_TipoPrestamo, ldec_Tasa,
                        lstr_NbrAcreedor, lstr_CatAcreedor, lstr_TpoAcreedor, lstr_NbrDeudor, lstr_CatDeudor, lstr_CondicionPrestamo,
                        lstr_ExisteObligacion, lstr_CondicionMotivo, ldec_CondicionTasa, ldec_CondicionMonto, ldt_CondicionFchInicio, ldt_CondicionFchFin,
                        lstr_Estado, lstr_UsrCreacion);
                str_CodResultado = cr_Procedimiento.Lstr_CodigoResultado;
                str_Mensaje = cr_Procedimiento.Lstr_MensajeRespuesta;

                Log.Info(str_Mensaje);
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_ResCreacion = true;
                }
            }
            catch (Exception ex)
            {
                str_CodResultado = "99";
                str_Mensaje = ex.ToString();
            }
            return bool_ResCreacion;
        }

        public bool ModificarPrestamo(string lstr_IdPrestamo, string lstr_Fuente, string lstr_Situacion, string lstr_Plazo,
            string lstr_Nombre, DateTime ldt_Firmado, DateTime ldt_LimiteGiro, DateTime ldt_LimiteEfectivo, DateTime ldt_Efectivo,
            decimal ldec_Monto, string lstr_IdMoneda, string lstr_TipoTramo, string lstr_Proposito, string lstr_GarantiaPublica,
            string lstr_OrigenDeuda, int lint_IdAcreedor, int lint_IdDeudor, string lstr_TipoPrestamo, decimal ldec_Tasa,
            string lstr_NbrAcreedor, string lstr_CatAcreedor, string lstr_TpoAcreedor, string lstr_NbrDeudor,
            string lstr_CatDeudor, string lstr_CondicionPrestamo, string lstr_ExisteObligacion,
            string lstr_CondicionMotivo, decimal ldec_CondicionTasa, decimal ldec_CondicionMonto, DateTime ldt_CondicionFchInicio,
            DateTime ldt_CondicionFchFin, string lstr_UsrModifica, DateTime ldt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsModificaPrestamo cr_Procedimiento = new clsModificaPrestamo(lstr_IdPrestamo, lstr_Fuente, lstr_Situacion, lstr_Plazo,
                                                                lstr_Nombre, ldt_Firmado, ldt_LimiteGiro, ldt_LimiteEfectivo, ldt_Efectivo,
                                                                ldec_Monto, lstr_IdMoneda, lstr_TipoTramo, lstr_Proposito, lstr_GarantiaPublica,
                                                                lstr_OrigenDeuda, lint_IdAcreedor, lint_IdDeudor, lstr_TipoPrestamo, ldec_Tasa,
                                                                lstr_NbrAcreedor, lstr_CatAcreedor, lstr_TpoAcreedor, lstr_NbrDeudor, lstr_CatDeudor,
                                                                lstr_CondicionPrestamo, lstr_ExisteObligacion, lstr_CondicionMotivo, ldec_CondicionTasa,
                                                                ldec_CondicionMonto, ldt_CondicionFchInicio, ldt_CondicionFchFin,
                                                                lstr_UsrModifica, ldt_FchModifica);
                str_CodResultado = cr_Procedimiento.Lstr_CodigoResultado;
                str_Mensaje = cr_Procedimiento.Lstr_MensajeRespuesta;

                Log.Info(str_Mensaje);
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_ResCreacion = true;
                }
            }
            catch (Exception ex)
            {
                str_CodResultado = "99";
                str_Mensaje = ex.ToString();
            }
            return bool_ResCreacion;
        }

        public bool CambiarEstadoPrestamo(string lstr_IdPrestamo, string lstr_Estado, string lstr_UsrModifica, DateTime ldt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCambiaEstadoPrestamo cr_Procedimiento = new clsCambiaEstadoPrestamo(lstr_IdPrestamo, lstr_Estado, lstr_UsrModifica, ldt_FchModifica);

                str_CodResultado = cr_Procedimiento.Lstr_CodigoResultado;
                str_Mensaje = cr_Procedimiento.Lstr_MensajeRespuesta;

                Log.Info(str_Mensaje);
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_ResCreacion = true;
                }
            }
            catch (Exception ex)
            {
                str_CodResultado = "99";
                str_Mensaje = ex.ToString();
            }
            return bool_ResCreacion;
        }

        /**********************************************************************************************
         * Función utilizada para reclasificar los préstamos.
         * Será ejecutada por el usuario cada fin de mes.
         * La fecha de contabilización deberá ser el último día hábil del mes.
         * Antes de ejecutar este proceso se debe ejecutar la transacción de SIGAF F.05, la cual
         * realiza el cálculo del diferencial cambiario (ambos procesos son ejecutados por el usuario).
        ***********************************************************************************************/
        public bool ReclasificarPrestamos(out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_resContabilizacion = true;
            bool bool_tipoCambioEncontrado = true;
            clsTiposAsiento tiposAsiento = new clsTiposAsiento();
            clsTiposCambio tiposCambio = new clsTiposCambio();
            string lstr_idModulo = "IdModulo IN ('DE')";
            string lstr_sociedad = "G206";
            string str_Moneda = "USD";
            string lstr_IdOperacion = "";
            decimal ldec_Monto = 0;
            string lstr_Moneda = "";
            string lstr_abrevAcreedor = "";
            string lstr_tipoPrestamo = "";
            string lstr_IdPrestamo = "";

            DateTime ldt_FchTipoCambio = DateTime.Now;
            str_CodResultado = "00";
            str_Mensaje = "Contabilizado Correctamente";

            // se consultan los préstamos y sus datos para la reclasificación

            try{
            DataSet ds_Prestamos = new DataSet();

            ds_Prestamos = this.ConsultarPrestamosReclasificar(null);


            // se verifica si se debe realizar el cambio de moneda
            if (lstr_Moneda != "CRC" && lstr_Moneda != "USD")
            {
                // se trae el tipo de cambio y se realiza la conversión a USD
                bool_tipoCambioEncontrado = false;

                DataSet ds_tipoCambio = tiposCambio.ConsultarTiposCambio(lstr_Moneda, ldt_FchTipoCambio, null);
                if (ds_tipoCambio.Tables.Count > 0 && ds_tipoCambio.Tables["Table"].Rows.Count > 0)
                {
                    // se realiza el cambio a dolares para procesar el asiento
                    decimal ldec_valor = Convert.ToDecimal(ds_tipoCambio.Tables["Table"].Rows[0]["Valor"]);

                    ldec_Monto /= ldec_valor;
                    //lstr_Moneda = "USD";

                    bool_tipoCambioEncontrado = true;
                }
            }

            if (bool_tipoCambioEncontrado)
            {
                // se contabiliza la primera parte con el idOperación DE16, reclasificación Cuentas de Deuda, es igual para los 4 tipos de préstamo

                String lstr_codAsiento;// variable por ModificarCodAsiento en DE
                lstr_IdOperacion = "DE16";
                bool_resContabilizacion = tAsiento.EnviarAsientoDE(lstr_sociedad, lstr_idModulo, lstr_IdOperacion, lstr_tipoPrestamo,
                                                        string.Empty, str_Moneda, ldec_Monto, 0, 0, 0, lstr_abrevAcreedor,
                                                            "1.1","",ldt_FchTipoCambio, out str_CodResultado, out str_Mensaje,out lstr_codAsiento);

                // si el préstamo es tipo 2 o 3 se genera el otro asiento de las CxC (idOperación DE17), reclasificación Centas X Cobrar
                if (lstr_tipoPrestamo == "2" || lstr_tipoPrestamo == "3")
                {
                    lstr_IdOperacion = "DE17";
                    bool_resContabilizacion = tAsiento.EnviarAsientoDE(lstr_sociedad, lstr_idModulo, lstr_IdOperacion, lstr_tipoPrestamo,
                                                        string.Empty, str_Moneda, ldec_Monto, 0, 0, 0, lstr_abrevAcreedor,
                                                                "1.1","",ldt_FchTipoCambio, out str_CodResultado, out str_Mensaje,out lstr_codAsiento);
                }
            }
            else
            {
                // error al obtener el tipo de cambio
                str_CodResultado = "01";
                str_Mensaje = "Error al obtener el tipo de cambio para contabilizar reclasificación. Moneda: " + lstr_Moneda + ". Préstamo: " + lstr_IdPrestamo;

                Log.Info(str_Mensaje);

                bool_resContabilizacion = false;
            }
                            }
                catch (Exception ex)
                {
                    str_CodResultado = "01";
                    str_Mensaje = "Error al contabilizar asiento de reclasificación. Operación: " + lstr_IdOperacion + ". Acreedor: " +
                        lstr_abrevAcreedor + ". " + ex.Message;

                    Log.Info(str_Mensaje);
                    bool_resContabilizacion = false;
                }
            return bool_resContabilizacion;
        }

        //private bool EnviarAsiento(string lstr_sociedad, string lstr_idModulo, string lstr_idOperacion, string lstr_tipoPrestamo, string lstr_moneda,
        //    decimal ldec_monto, string lstr_abrevAcreedor, out string lstr_CodResultado, out string lstr_Mensaje)
        //{
        //    // variables locales
        //    bool bool_enviado = true;
        //    clsTiposAsiento tiposAsiento = new clsTiposAsiento();

        //    lstr_CodResultado = "00";
        //    lstr_Mensaje = "Asiento Enviado";

        //    string str_abrevAcreedor = (lstr_tipoPrestamo.Trim() == "4") ? "TITULOS" : lstr_abrevAcreedor;
        //    // se obtienen las tiras del asiento y se itera sobre ellas
        //    DataSet tiposA = tiposAsiento.ConsultarTiposAsiento(lstr_sociedad, lstr_idModulo, lstr_idOperacion, null, null, lstr_moneda, str_abrevAcreedor, null, null);
        //    if (tiposA.Tables.Count > 0 && tiposA.Tables["Table"].Rows.Count > 0)
        //    {
        //        DataTable dt_tiras = tiposA.Tables[0];
        //        DataRow dr_asiento = null;

        //        // se obtiene la cantidad de líneas que componen este asiento
        //        int cantidad_registros = tiposA.Tables[0].Rows.Count * 2;

        //        //Coleccion de asientos y tipos de asientos requeridos en SAP expuestos por la referencia del servicio
        //        wsAsientos.ZfiAsiento[] tabla_asientos = new wsAsientos.ZfiAsiento[cantidad_registros];
        //        wsAsientos.ZfiAsiento item_asiento = new wsAsientos.ZfiAsiento();
        //        wsAsientos.ZfiAsiento item_asiento2 = new wsAsientos.ZfiAsiento();

        //        //variables de proceso de asiento
        //        string[] item_resAsientosLog = new string[10];
        //        string logAsiento = string.Empty;

        //        DateTime fechaContabilizacion = DateTime.Now;
        //        bool encabezadoEnviado = false;

        //        try
        //        {
        //            // se itera sobre las tiras que componen el asiento y se construye el arreglo a enviar a SIGAF
        //            for (int i = 0; i < cantidad_registros; i++)
        //            {
        //                dr_asiento = dt_tiras.Rows[i];
        //                item_asiento = new wsAsientos.ZfiAsiento();
        //                if (dr_asiento["IdClaveContable"].ToString().Trim() != null && dr_asiento["IdClaveContable"].ToString().Trim() != "")
        //                {
        //                    ///*************************************************** Encabezado (solo se contruye una vez) *****************************************************/
        //                    if (i == 0 && !encabezadoEnviado)
        //                    {
        //                        item_asiento.Blart = dr_asiento["CodigoAuxiliar4"].ToString().Trim();//Clase de documento
        //                        item_asiento.Bukrs = lstr_sociedad;//Sociedad
        //                        item_asiento.Bldat = fechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de documento
        //                        item_asiento.Budat = fechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de contabilización

        //                        encabezadoEnviado = true;
        //                    }
        //                    ///***************************************************Cargar cuenta 40 HABER*****************************************************/
        //                    item_asiento.Waers = lstr_moneda;//Moneda 
        //                    //item_asiento.Xblnr = "REF";
        //                    //item_asiento.Bktxt = "Texto_Cabecera";
        //                    //item_asiento.Xref1Hd = idexpediente;//numero expediente 
        //                    item_asiento.Xref2Hd = lstr_idOperacion + '-' + lstr_abrevAcreedor;//CT01-AG operacion+codigoprocesal expediente
        //                    item_asiento.Bschl = dr_asiento["IdClaveContable"].ToString().Trim();//Clave de contabilización
        //                    item_asiento.Hkont = dr_asiento["IdCuentaContable"].ToString().Trim();//Cuenta de mayor
        //                    item_asiento.Wrbtr = ldec_monto;//Importe o monto en colones a contabilizar 
        //                    //item_asiento.Zuonr = "Asig_1";
        //                    //item_asiento.Sgtxt = "SG-Liquidacion";
        //                    //item_asiento.Projk = ldat_TiposAsientos.Rows[i]["IdElementoPEP"].ToString().TrimEnd();
        //                    //item_asiento.Fipex = "NULA_SIN_PRESU";//Posición presupuestaria
        //                    //item_asiento.Kostl = ldat_TiposAsientos.Rows[i]["IdCentroCosto"].ToString();
        //                    //item_asiento.Fistl = ldat_TiposAsientos.Rows[i]["IdCentroGestor"].ToString();
        //                    //item_asiento.Prctr = ldat_TiposAsientos.Rows[i]["IdCentroBeneficio"].ToString();
        //                    //item_asiento.Measure = ldat_TiposAsientos.Rows[i]["IdPrograma"].ToString().TrimEnd();//Programa presupuestario
        //                    item_asiento.Geber = dr_asiento["IdFondo"].ToString().Trim();//Fondo
        //                    //item_asiento.Fkber = "";
        //                    //item_asiento.Xref2 = "";
        //                    tabla_asientos[i] = item_asiento;
        //                }
        //                item_asiento2 = new wsAsientos.ZfiAsiento();
        //                if (dr_asiento["IdClaveContable2"].ToString().Trim() != null && dr_asiento["IdClaveContable2"].ToString().Trim() != "")
        //                {
        //                    ///*************************************************** Encabezado (solo se contruye una vez) *****************************************************/
        //                    if (i == 0 && !encabezadoEnviado)
        //                    {
        //                        item_asiento.Blart = dr_asiento["CodigoAuxiliar4"].ToString().Trim();//Clase de documento
        //                        item_asiento.Bukrs = lstr_sociedad;//Sociedad
        //                        item_asiento.Bldat = fechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de documento
        //                        item_asiento.Budat = fechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de contabilización

        //                        encabezadoEnviado = true;
        //                    }
        //                    ///***************************************************Cargar cuenta 50 DEBE*****************************************************/
        //                    item_asiento2.Waers = lstr_moneda;//Moneda 
        //                    item_asiento2.Bschl = dr_asiento["IdClaveContable2"].ToString().Trim();//Clave de contabilización
        //                    item_asiento2.Hkont = dr_asiento["IdCuentaContable2"].ToString().Trim();//Cuenta de mayor
        //                    item_asiento2.Wrbtr = ldec_monto;//Importe o monto en colones a contabilizar
        //                    //item_asiento2.Zuonr = "Asig_2";
        //                    //item_asiento2.Sgtxt = "SG-Liquidacion";//char 50
        //                    //item_asiento2.Kostl = ldat_TiposAsientos.Rows[i]["IdCentroCosto2"].ToString();
        //                    //item_asiento2.Fistl = ldat_TiposAsientos.Rows[i]["IdCentroGestor2"].ToString();
        //                    //item_asiento2.Prctr = ldat_TiposAsientos.Rows[i]["IdCentroBeneficio2"].ToString();
        //                    item_asiento2.Geber = dr_asiento["IdFondo2"].ToString().Trim();//Fondo
        //                    //item_asiento2.Fkber = "";
        //                    //item_asiento2.Xref2 = "xref2";
        //                    tabla_asientos[i + 1] = item_asiento2;
        //                }
        //                i++;
        //            }

        //            //Carga de Asientos 
        //            //envio de asiento mediante servicio web hacia SIGAF
        //            item_resAsientosLog = asientos.EnviarAsientos(tabla_asientos);
        //            for (int j = 0; j < item_resAsientosLog.Length; j++)
        //            {
        //                int x = j + 1;
        //                logAsiento += "\n" + x + "-" + item_resAsientosLog[j];
        //            }
        //            //MessageBox.Show("Resultado de contabilización: \n\n"+logAsiento);
        //            Log.Info("Resultado de contabilización: \n\n" + logAsiento);
        //        }
        //        catch (Exception ex)
        //        {
        //            lstr_CodResultado = "01";
        //            lstr_Mensaje = "Error al contabilizar asiento de Reclasificación (Deuda Externa). Operación: " + lstr_idOperacion + ". Acreedor: " +
        //                lstr_abrevAcreedor + ". " + ex.Message;

        //            Log.Info(lstr_Mensaje);
        //            bool_enviado = false;
        //        }
        //        resAsientosLog = reg_Bitacora.ufnRegistrarAccionBitacora("DE", "1", "Prestamo", "Resultado de Contabilización: " + logAsiento);
        //    }

        //    return bool_enviado;
        //}

        #endregion

        #region Constructor

        public clsPrestamo()
        { }

        #endregion
    }
}