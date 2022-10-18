using System;
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
    public class clsComision
    {
        #region Variables

        private static readonly ILog Log = LogManager.GetLogger("FileAppender");
        //private wsAsientos.ServicioContable asientos = new wsAsientos.ServicioContable();
        private static clsTiposAsiento tasientos = new clsTiposAsiento();
        private static string lstr_formato_fecha = "dd/MM/yyyy";
        private tBitacora reg_Bitacora = new tBitacora();
        private clsTiposAsiento tAsiento = new clsTiposAsiento();

        private string resAsientosLog = string.Empty;
        //private wsSG.wsSistemaGestor ws_SGService = new wsSG.wsSistemaGestor();

        #endregion

        #region Métodos

        public DataSet ConsultarComision(string lstr_IdPrestamo, string lint_IdTramo, string lstr_TipoPago = null, string lstr_TipoComision = null, string lstr_MonedaPago = null, string lstr_IdComision = null,
            decimal? lstr_Porcentaje = null, string lstr_Periodo = null, DateTime? lstr_FechaDesde = null, DateTime? lstr_FechaHasta = null, string lstr_Anno = null, string lstr_Mes = null)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultaComision cr_Procedimiento = new clsConsultaComision(lstr_IdPrestamo, lint_IdTramo, lstr_TipoPago, lstr_TipoComision, lstr_MonedaPago, lstr_IdComision,
                                                                lstr_Porcentaje , lstr_Periodo , lstr_FechaDesde , lstr_FechaHasta , lstr_Anno , lstr_Mes);
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

        public DataSet ConsultarComisionPago(string lstr_IdPrestamo, string lint_IdTramo, DateTime? ldt_FchPago, Int64? lint_Secuencia, Int64? lint_Consecutivo)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultaComisionPago cr_Procedimiento = new clsConsultaComisionPago(lstr_IdPrestamo, lint_IdTramo, ldt_FchPago, lint_Secuencia, lint_Consecutivo);
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


        public bool ModificarCodigoAsiento(string lstr_IdPrestamo, int? lint_IdTramo, Int64? lint_Secuencia, DateTime? ldt_FchProgramada,
            string lstr_IdMoneda, string lstr_CodAsiento, string lstr_UsrModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsModificarCodigosAsiento cr_Procedimiento = new clsModificarCodigosAsiento(lstr_IdPrestamo, lint_IdTramo, lint_Secuencia, ldt_FchProgramada,
            lstr_IdMoneda, "COMISIONESPAGOS", lstr_CodAsiento, lstr_UsrModifica);

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


        public bool ModificarComisionPago(
            string lstr_IdPrestamo, int? lint_IdTramo, Int64? lint_Secuencia, Int64 lint_Consecutivo, DateTime? ldt_FchTipoCambio,
            string lstr_TipoComision, string lstr_ModalEjecucion,
            string lstr_MonedaPago, decimal? ldec_Monto, decimal? ldec_MontoAntes, DateTime? ldt_FchPago,
            string lstr_EstadoSigade, string lstr_UsrModifica, DateTime? ldt_FchModifica, Int64? lint_SecuenciaAnt, 
            out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsModificaComisionPago cr_Procedimiento = new clsModificaComisionPago(lstr_IdPrestamo, lint_IdTramo, lint_Secuencia, 
                    lint_Consecutivo, ldt_FchTipoCambio,
                    lstr_TipoComision, lstr_ModalEjecucion,
            lstr_MonedaPago, ldec_Monto, ldt_FchPago, 
            lstr_EstadoSigade, lstr_UsrModifica, ldt_FchModifica);
                str_CodResultado = cr_Procedimiento.Lstr_CodigoResultado;
                str_Mensaje = cr_Procedimiento.Lstr_MensajeRespuesta;

                //Log.Info(str_Mensaje);
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_ResCreacion = true;
                    if ((lint_Secuencia != 0 && lint_IdTramo != 0) && ((lint_SecuenciaAnt == 0 || lint_SecuenciaAnt == null) || (ldec_Monto != ldec_MontoAntes)))
                    {
                        ContabilizarComision(lstr_IdPrestamo, lint_IdTramo.ToString(), Convert.ToInt64( lint_Secuencia), lint_Consecutivo,
                                lstr_TipoComision, lstr_ModalEjecucion,Convert.ToDateTime(ldt_FchPago.ToString()), lstr_MonedaPago, Convert.ToDecimal(ldec_Monto), lstr_EstadoSigade, lstr_UsrModifica,
                                out str_CodResultado, out str_Mensaje);
                    }
                }
            }
            catch (Exception ex)
            {
                str_CodResultado = "99";
                str_Mensaje = ex.ToString();
            }
            return bool_ResCreacion;
        }
        public bool CrearComision(string lstr_IdPrestamo, string lint_IdTramo, int lint_IdComision, string lstr_TipoComision, 
            DateTime? ldt_FchEfectivoAPartir, DateTime? ldt_FchHasta, string lstr_MonedaPago, decimal? ldec_Porcentaje,
            decimal ldec_MontoPago, string lstr_MetodoPago, DateTime? ldt_FchPrimerPago, DateTime? ldt_FchUltimoPago,
            string lstr_Periodo, string lstr_Anno, string lstr_Mes, string lstr_TipoPago, //string lstr_EsPago, DateTime ldt_FchValorAcreedor, 
            string lstr_Estado, string lstr_EstadoComision, string lstr_UsrCreacion,
            out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCreaComision cr_Procedimiento = new clsCreaComision(lstr_IdPrestamo, lint_IdTramo, lint_IdComision, lstr_TipoComision, 
                                                                ldt_FchEfectivoAPartir, ldt_FchHasta, lstr_MonedaPago, ldec_Porcentaje,
                                                                ldec_MontoPago, lstr_MetodoPago, ldt_FchPrimerPago, ldt_FchUltimoPago,
                                                                lstr_Periodo, lstr_Anno, lstr_Mes, lstr_TipoPago, //lstr_EsPago, ldt_FchValorAcreedor, 
                                                                lstr_Estado, lstr_EstadoComision, lstr_UsrCreacion);

                str_CodResultado = cr_Procedimiento.Lstr_CodigoResultado;
                str_Mensaje = cr_Procedimiento.Lstr_MensajeRespuesta;

                //Log.Info(str_Mensaje);
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_ResCreacion = true;

                //    // se envía la comisión para su contabilización
                //    ContabilizarComision(lstr_IdPrestamo, lint_IdTramo, lint_IdComision, lstr_TipoComision, ldt_FchEfectivoAPartir,
                //            ldt_FchHasta, lstr_MonedaPago, ldec_Porcentaje, ldec_MontoPago, lstr_MetodoPago, ldt_FchPrimerPago,
                //            ldt_FchUltimoPago, lstr_Periodo, lstr_Anno, lstr_Mes, lstr_TipoPago, lstr_Estado, lstr_UsrCreacion,
                //            out str_CodResultado, out str_Mensaje);
                }
            }
            catch (Exception ex)
            {
                str_CodResultado = "99";
                str_Mensaje = ex.ToString(); 
            }
            return bool_ResCreacion;
        }

        public bool CrearComisionPago(string lstr_IdPrestamo, string lint_IdTramo, DateTime ldt_FchPago, Int64 lint_Secuencia, Int64 lint_Consecutivo, DateTime? ldt_FchTipoCambio,
            decimal ldec_Monto, string lstr_MonedaPago, string lstr_EstadoSigade, string lstr_Estado, string lstr_UsrCreacion, string lstr_TipoComision, string lstr_ModalEjecucion,
             out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCreaComisionPago cr_Procedimiento = new clsCreaComisionPago(lstr_IdPrestamo, lint_IdTramo, ldt_FchPago, lint_Secuencia, lint_Consecutivo, ldt_FchTipoCambio,
                                                                                ldec_Monto, lstr_MonedaPago, lstr_EstadoSigade, lstr_Estado, lstr_UsrCreacion, lstr_TipoComision, lstr_ModalEjecucion);

                str_CodResultado = cr_Procedimiento.Lstr_CodigoResultado;
                str_Mensaje = cr_Procedimiento.Lstr_MensajeRespuesta;

                Log.Info(str_Mensaje);
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_ResCreacion = true;

                     //se envía la comisión para su contabilización
                    ContabilizarComision(lstr_IdPrestamo, lint_IdTramo, lint_Secuencia, lint_Consecutivo,
                            lstr_TipoComision, lstr_ModalEjecucion, ldt_FchPago, lstr_MonedaPago, ldec_Monto, lstr_Estado, lstr_UsrCreacion,
                            out str_CodResultado, out str_Mensaje);
                }
            }
            catch (Exception ex)
            {
                str_CodResultado = "99";
                str_Mensaje = ex.ToString();
            }
            return bool_ResCreacion;
        }

        public bool ModificarComision(string lstr_IdPrestamo, string lint_IdTramo, int? lint_IdComision, string lstr_TipoComision,
            DateTime? ldt_FchEfectivoAPartir, DateTime? ldt_FchHasta, string lstr_MonedaPago, decimal? ldec_Porcentaje,
            decimal? ldec_MontoPago, string lstr_MetodoPago, DateTime? ldt_FchPrimerPago, DateTime? ldt_FchUltimoPago,
            string lstr_Periodo, string lstr_Anno, string lstr_Mes, string lstr_TipoPago, string lstr_EstadoComision, string lstr_UsrModifica, DateTime? ldt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsModificaComision cr_Procedimiento = new clsModificaComision( lstr_IdPrestamo, lint_IdTramo, lint_IdComision,  lstr_TipoComision,
                                                                 ldt_FchEfectivoAPartir,  ldt_FchHasta,  lstr_MonedaPago, ldec_Porcentaje,
                                                                 ldec_MontoPago,  lstr_MetodoPago,  ldt_FchPrimerPago,  ldt_FchUltimoPago,
                                                                 lstr_Periodo,  lstr_Anno,  lstr_Mes,  lstr_TipoPago, lstr_EstadoComision,  lstr_UsrModifica,  ldt_FchModifica);
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

        public bool CambiarEstadoComision(string lstr_IdPrestamo, int? lint_IdTramo, int? lint_IdComision, string lstr_Estado, string lstr_UsrModifica, DateTime ldt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCambiaEstadoComision cr_Procedimiento = new clsCambiaEstadoComision(lstr_IdPrestamo, lint_IdTramo, lint_IdComision, lstr_Estado, lstr_UsrModifica, ldt_FchModifica);

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

        public bool ContabilizarComision(string lstr_IdPrestamo, string lint_IdTramo, Int64 lint_Secuencia, Int64 lint_Consecutivo, string lstr_TipoComision, string lstr_ModalEjecucion,
            DateTime ldt_FchPago, string lstr_MonedaPago, 
            decimal ldec_Monto, string lstr_Estado, string lstr_UsrCreacion,
            out string str_CodResultado, out string str_Mensaje)
        {
            // variables locales
            bool bool_ResContabilizacion = true;
            bool bool_tipoCambioEncontrado = true;
            DateTime fechaContabilizacion = DateTime.Now;
            clsTiposCambio tiposCambio = new clsTiposCambio();
            clsOpcionesCatalogo oc = new clsOpcionesCatalogo();
            DataSet ds_opciones = new DataSet();
            string str_idModulo = "IdModulo IN ('DE')";
            string str_sociedad = "G206";
            string str_Moneda = "USD";
            string str_IdOperacion = "";
            decimal ldec_Monto2 = 0;

            String lstr_codAsiento = string.Empty;

            str_CodResultado = "00";
            str_Mensaje = "Contabilizado Correctamente";
            try{
            // se revisa la moneda por si se debe realizar un cambio a dolares
            if (lstr_MonedaPago != "CRC" && lstr_MonedaPago != "USD" && lstr_MonedaPago != "EUR")
            {
                // se trae el tipo de cambio y se realiza la conversión a USD
                bool_tipoCambioEncontrado = false;

                DataSet ds_tipoCambio = tiposCambio.ConsultarTiposCambio(lstr_MonedaPago, fechaContabilizacion, null);
                if (ds_tipoCambio.Tables.Count > 0 && ds_tipoCambio.Tables["Table"].Rows.Count > 0)
                {
                    // se realiza el cambio a dolares para procesar el asiento
                    decimal ldec_valor = Convert.ToDecimal(ds_tipoCambio.Tables["Table"].Rows[0]["Valor"]);

                    ldec_Monto /= ldec_valor;
                    //lstr_MonedaPago = "USD";

                    bool_tipoCambioEncontrado = true;
                }
            } 
            else
            {

                str_Moneda = lstr_MonedaPago;
            }

            // si no se encontró el tipo de cambio se genera el error y se notifica
            if (!bool_tipoCambioEncontrado)
            {
                // error al obtener el tipo de cambio
                str_CodResultado = "01";
                str_Mensaje = "Error al obtener el tipo de cambio para contabilizar comisión. Moneda: " + lstr_MonedaPago + " Fecha: " + fechaContabilizacion.ToString(lstr_formato_fecha);

                Log.Info(str_Mensaje);

                bool_ResContabilizacion = false;
            }
            else
            {
                // Se revisa el tipo de desembolso y tipo de préstamo, generando el asiento que corresponda
                //DataSet ds_Comision = ConsultarComision(lstr_IdPrestamo, null, lstr_TipoPago, lint_IdComision.ToString(),
                //                            ldec_Porcentaje, lstr_Periodo, null,null, lstr_Anno, lstr_Mes);
                DataSet ds_Comision = ConsultarComisionPago(lstr_IdPrestamo.ToString(), null, ldt_FchPago, lint_Secuencia, lint_Consecutivo);

                if (ds_Comision.Tables.Count > 0 && ds_Comision.Tables["Table"].Rows.Count > 0)
                {
                    string abrev_acreedor = ds_Comision.Tables["Table"].Rows[0]["NbrAcreedor"].ToString().Trim();
                    string str_tipo_prestamo = ds_Comision.Tables["Table"].Rows[0]["TipoPrestamo"].ToString().Trim();
                    if (lstr_ModalEjecucion.ToLower().Contains("capitaliza"))
                    {
                        str_IdOperacion = "COM DESC";//son comisiones descontadas en el desembolso
                        //se debe consultar el desembolso y almacenar el valor en ldec_monto2
                        clsDesembolso desembolso = new clsDesembolso();
                        DataSet ds_Desembolso = desembolso.ConsultarDesembolso(lstr_IdPrestamo, Convert.ToInt32(lint_IdTramo), null, null, lstr_MonedaPago, ldt_FchPago, ldt_FchPago, null, null, "CAPITALIZACIÓN", null, null);
                        if (!(ds_Desembolso.Tables.Count > 0 && ds_Desembolso.Tables["Table"].Rows.Count > 0))
                        {
                            str_CodResultado = "01";
                            str_Mensaje = "Error al obtener el desembolso asociado a la comisión. Moneda: " + lstr_MonedaPago + ". Préstamo: " + lstr_IdPrestamo + ". Tipo Comisión: " + lstr_TipoComision;

                            Log.Info(str_Mensaje);

                            bool_ResContabilizacion = false;
                        }
                        else
                        {

                            DataTable dt_Desembolso = ds_Desembolso.Tables[0];
                            ldec_Monto2 = Convert.ToDecimal(dt_Desembolso.Rows[0]["Monto"]);
                        }

                    }
                    else
                    {
                        ds_opciones = oc.ConsultarOpcionesCatalogo(null, "COMISIONES_CT", null, lstr_TipoComision);
                        if (ds_opciones.Tables.Count > 0 && ds_opciones.Tables["Table"].Rows.Count > 0)
                        {
                            str_IdOperacion = "COMCT";//son costos de transaccion
                        }
                        else
                        {

                            str_IdOperacion = "COMNOCT";//son comisiones
                        }
                    }
                        //if (str_tipo_prestamo == "1" || str_tipo_prestamo == "2" || str_tipo_prestamo == "3")
                    //{
                    //    // Comisión préstamos tipo 1, 2 y 3 (operación DE13)
                    //    str_IdOperacion = "DE13";
                    //}
                    //else if (str_tipo_prestamo == "4")
                    //{
                    //    // Comisión préstamos tipo 4 (operación DE14)
                    //    str_IdOperacion = "DE14";
                    //}
                    //else
                    //{
                    //    str_CodResultado = "01";
                    //    str_Mensaje = "Error al obtener el código de operación, asiento de comisión, '" + str_IdOperacion + "'. Moneda: " + lstr_MonedaPago + " Acreedor: " + abrev_acreedor;

                    //    Log.Info(str_Mensaje);

                    //    bool_ResContabilizacion = false;
                    //}

                   // variable por ModificarCodigoAsiento

                    if (bool_ResContabilizacion)
                    {
                        if (str_IdOperacion == "COM DESC")
                            // se procesa el asiento según el id de operación asignado
                            bool_ResContabilizacion = tAsiento.EnviarAsientoDE(str_sociedad,
                                                        str_idModulo,
                                                        str_IdOperacion,
                                                        str_tipo_prestamo,
                                                        string.Empty,
                                                        str_Moneda,
                                                        ldec_Monto2,
                                                        ldec_Monto, 0, 0,
                                                        abrev_acreedor,
                                                        lstr_IdPrestamo + "." + lint_IdTramo,
                                                        lstr_IdPrestamo,
                                                        ldt_FchPago,
                                                        out str_CodResultado,
                                                        out str_Mensaje,
                                                        out lstr_codAsiento
                                                        );
                        else
                            // se procesa el asiento según el id de operación asignado
                            bool_ResContabilizacion = tAsiento.EnviarAsientoDE(str_sociedad,
                                                        str_idModulo,
                                                        str_IdOperacion,
                                                        str_tipo_prestamo,
                                                        string.Empty,
                                                        str_Moneda,
                                                        ldec_Monto,
                                                        ldec_Monto2, 0, 0,
                                                        abrev_acreedor,
                                                        lstr_IdPrestamo + "." + lint_IdTramo,
                                                        lstr_IdPrestamo,
                                                        ldt_FchPago,
                                                        out str_CodResultado,
                                                        out str_Mensaje,
                                                        out lstr_codAsiento
                                                        );
                    }
                    if (str_CodResultado == "00")
                    {
                        ModificarCodigoAsiento(lstr_IdPrestamo,Convert.ToInt32(lint_IdTramo), Convert.ToInt64(lint_Secuencia), fechaContabilizacion, str_Moneda, lstr_codAsiento, lstr_UsrCreacion, out str_CodResultado, out str_Mensaje);
                    }

                }
                else
                {
                    // no se ha encontrado el desembolso guardado
                    str_CodResultado = "01";
                    str_Mensaje = "Error al obtener la comisión para su contabilización. Moneda: " + lstr_MonedaPago + ". Préstamo: " + lstr_IdPrestamo + ". Tipo Comisión: " + lstr_TipoComision;

                    Log.Info(str_Mensaje);

                    bool_ResContabilizacion = false;
                }
            }
            
            }
            catch (Exception ex)
            {
                str_CodResultado = "01";
                str_Mensaje = "Error al contabilizar asiento de comisión. Operación: " + lstr_IdPrestamo + ". Moneda: " +
                    lstr_MonedaPago + ". " + ex.Message;

                Log.Info(str_Mensaje);
                bool_ResContabilizacion = false;
            }

            return bool_ResContabilizacion;
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
        //            lstr_Mensaje = "Error al contabilizar asiento de Comisión (Deuda Externa). Operación: " + lstr_idOperacion + ". Acreedor: " +
        //                lstr_abrevAcreedor + ". " + ex.Message;

        //            Log.Info(lstr_Mensaje);
        //            bool_enviado = false;
        //        }

        //        resAsientosLog = reg_Bitacora.ufnRegistrarAccionBitacora("DE", "1", "Comisión", "Resultado de Contabilización: " + logAsiento);
        //    }

        //    return bool_enviado;
        //}

        #endregion

        #region Constructor

        public clsComision()
        { }

        #endregion
    }
}