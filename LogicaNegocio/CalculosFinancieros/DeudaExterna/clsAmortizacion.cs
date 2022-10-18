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
    public class clsAmortizacion
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

        public DataSet ConsultarAmortizacion(string lstr_IdPrestamo, int? lint_IdTramo, DateTime? ldt_FchValorAcreedor = null,
            DateTime? ldt_FchTipoCambio = null, DateTime? ldt_FchRecepcion = null, string lstr_IdMoneda = null, int? lint_Secuencia = -1, DateTime? ldt_FchHasta = null)
        {

            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultaAmortizacion cr_Procedimiento = new clsConsultaAmortizacion(lstr_IdPrestamo, lint_IdTramo, ldt_FchValorAcreedor, ldt_FchTipoCambio,
                                                            ldt_FchRecepcion, lstr_IdMoneda, lint_Secuencia, ldt_FchHasta);
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

        public bool Reclasificar(DateTime? ldt_FechaFin, out string str_CodResultado, out string str_Mensaje)
        {

            bool bool_ResCreacion = true;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;

            DataSet lds_Amortizaciones = this.ConsultarPrestamosReclasificar(ldt_FechaFin);

            DataTable ldt_Amortizaciones = lds_Amortizaciones.Tables[0];

            int i = 0;

            foreach (DataRow dr_Amortizacion in ldt_Amortizaciones.Rows)
            {
                
                // se contabiliza la creación de la amortización
                bool_ResCreacion = ContabilizarReclasificacion(dr_Amortizacion["IdPrestamo"].ToString(), Convert.ToInt32(dr_Amortizacion["IdTramo"].ToString()), Convert.ToInt32(dr_Amortizacion["Secuencia"].ToString()), dr_Amortizacion["IdMoneda"].ToString(),
                    Convert.ToDecimal( dr_Amortizacion["Monto"].ToString()), ldt_FechaFin, dr_Amortizacion["NbrAcreedor"].ToString(), string.Empty/*dr_Amortizacion["TipoPrestamo"].ToString()*/, dr_Amortizacion["Modal"].ToString(),
                    dr_Amortizacion["Estado"].ToString(), "", out str_CodResultado, out str_Mensaje);
                if (!String.Equals(str_CodResultado, "00"))
                {
                    bool_ResCreacion = false;
                }
            }   

            return bool_ResCreacion;
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

        public bool CrearAmortizacion(string lstr_IdPrestamo, int lint_IdTramo, string lstr_IdMoneda, decimal ldec_Monto,
            DateTime? ldt_FchValorAcreedor, DateTime? ldt_FchRecepcion, DateTime? ldt_FchTipoCambio, string lstr_Modal, int lint_secuencia, string lstr_Estado, string lstr_EstadoSigade,
            string lstr_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCreaAmortizacion cr_Procedimiento = new clsCreaAmortizacion(lstr_IdPrestamo, lint_IdTramo, lstr_IdMoneda, ldec_Monto,
                                                                ldt_FchValorAcreedor, ldt_FchRecepcion, ldt_FchTipoCambio, lstr_Modal, lint_secuencia, lstr_Estado, lstr_EstadoSigade,
                                                                lstr_UsrCreacion);

                str_CodResultado = cr_Procedimiento.Lstr_CodigoResultado;
                str_Mensaje = cr_Procedimiento.Lstr_MensajeRespuesta;

                Log.Info(str_Mensaje);
                if (String.Equals(str_CodResultado, "00") && lint_IdTramo != 0 && lint_secuencia != 0)
                {
                    bool_ResCreacion = true;
                    // se contabiliza la creación de la amortización
                    bool_ResCreacion = ContabilizarAmortizacion(lstr_IdPrestamo, lint_IdTramo, lint_secuencia, lstr_IdMoneda, ldec_Monto,
                                            ldt_FchValorAcreedor, ldt_FchRecepcion, ldt_FchTipoCambio, lstr_Modal,
                                            lstr_Estado, lstr_UsrCreacion, out str_CodResultado, out str_Mensaje);
                    if (!String.Equals(str_CodResultado, "00"))
                    {
                        bool_ResCreacion = false;
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

        public bool ModificarAmortizacion(string lstr_IdPrestamo, int? lint_IdTramo, string lstr_IdMoneda, decimal? ldec_Monto, decimal? ldec_MontoAntes,
            DateTime? ldt_FchValorAcreedor, DateTime? ldt_FchRecepcion, DateTime? ldt_FchTipoCambio, string lstr_Modal, string lstr_EstadoSigade, int? lint_Secuencia, int? lint_SecuenciaAnt, 
            string lstr_UsrModifica,DateTime ldt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsModificaAmortizacion cr_Procedimiento = new clsModificaAmortizacion(lstr_IdPrestamo, lint_IdTramo, lstr_IdMoneda, ldec_Monto,
                                                                ldt_FchValorAcreedor, ldt_FchRecepcion, ldt_FchTipoCambio, lstr_Modal, lstr_EstadoSigade, lint_Secuencia, lint_SecuenciaAnt,
                                                                lstr_UsrModifica,ldt_FchModifica);
                str_CodResultado = cr_Procedimiento.Lstr_CodigoResultado;
                str_Mensaje = cr_Procedimiento.Lstr_MensajeRespuesta;

                Log.Info(str_Mensaje);
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_ResCreacion = true;
                    if ((lint_Secuencia != 0 && lint_IdTramo != 0) && ((lint_SecuenciaAnt == 0 || lint_SecuenciaAnt == null) || (ldec_Monto != ldec_MontoAntes)))
                    //if (String.Equals(str_CodResultado, "00") && ((lint_secuenciaAnt == 0 || lint_secuenciaAnt == null) && lint_secuencia != 0 && lint_IdTramo != 0))
                    {                      
                        // se contabiliza la creación de la amortización
                        bool_ResCreacion = ContabilizarAmortizacion(lstr_IdPrestamo, Convert.ToInt32(lint_IdTramo), Convert.ToInt32(lint_Secuencia), lstr_IdMoneda, Convert.ToDecimal(ldec_Monto),
                                                ldt_FchValorAcreedor, ldt_FchRecepcion, ldt_FchTipoCambio, lstr_Modal,
                                                "", lstr_UsrModifica, out str_CodResultado, out str_Mensaje);
                        if (!String.Equals(str_CodResultado, "00"))
                        {
                            bool_ResCreacion = false;
                        }
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

        public bool CambiarEstadoAmortizacion(string lstr_IdPrestamo, int lint_IdTramo, DateTime ldt_FchRecepcion, string lstr_IdMoneda, int lint_secuencia, string lstr_Estado, string lstr_UsrModifica, DateTime ldt_FchModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCambiaEstadoAmortizacion cr_Procedimiento = new clsCambiaEstadoAmortizacion(lstr_IdPrestamo, lint_IdTramo, 
                                                                    ldt_FchRecepcion, lstr_IdMoneda, lint_secuencia, lstr_Estado, lstr_UsrModifica, ldt_FchModifica);

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

        public bool ModificarCodigoAsiento(string lstr_IdPrestamo, int? lint_IdTramo, Int64? lint_Secuencia, DateTime? ldt_FchProgramada,
            string lstr_IdMoneda, string lstr_CodAsiento, string lstr_UsrModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsModificarCodigosAsiento cr_Procedimiento = new clsModificarCodigosAsiento(lstr_IdPrestamo, lint_IdTramo, lint_Secuencia, ldt_FchProgramada,
            lstr_IdMoneda, "AMORTIZACIONES", lstr_CodAsiento, lstr_UsrModifica);

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

        public bool ContabilizarAmortizacion(string lstr_IdPrestamo, int lint_IdTramo, int lint_Secuencia, string lstr_IdMoneda, decimal ldec_Monto,
                DateTime? ldt_FchValorAcreedor, DateTime? ldt_FchRecepcion, DateTime? ldt_FchTipoCambio, string lstr_Modal, string lstr_Estado,
                string lstr_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {
            
                bool bool_resContabilizacion = true;
                bool bool_tipoCambioEncontrado = true;
                clsTiposAsiento tiposAsiento = new clsTiposAsiento();
                clsTiposCambio tiposCambio = new clsTiposCambio();
                string str_idModulo = "IdModulo IN ('DE')";
                string str_sociedad = "G206";
                string str_Moneda = "USD";
                string str_IdOperacion = "";
                String lstr_codAsiento = string.Empty;

                str_CodResultado = "00";
                str_Mensaje = "Contabilizado Correctamente";
                try
                {
                // se revisa la moneda por si se debe realizar un cambio a dolares
                if (lstr_IdMoneda != "CRC" && lstr_IdMoneda != "USD" && lstr_IdMoneda != "EUR")
                {
                    // se trae el tipo de cambio y se realiza la conversión a USD
                    bool_tipoCambioEncontrado = false;

                    DataSet ds_tipoCambio = tiposCambio.ConsultarTiposCambio(lstr_IdMoneda, ldt_FchTipoCambio, null);
                    if (ds_tipoCambio.Tables.Count > 0 && ds_tipoCambio.Tables["Table"].Rows.Count > 0)
                    {
                        // se realiza el cambio a dolares para procesar el asiento
                        decimal ldec_valor = Convert.ToDecimal(ds_tipoCambio.Tables["Table"].Rows[0]["Valor"]);

                        ldec_Monto /= ldec_valor;
                        //lstr_IdMoneda = "USD";

                        bool_tipoCambioEncontrado = true;
                    }
                }
                else
                {

                    str_Moneda = lstr_IdMoneda;
                }

                // si no se encontró el tipo de cambio se genera el error y se notifica
                if (!bool_tipoCambioEncontrado)
                {
                    // error al obtener el tipo de cambio
                    str_CodResultado = "01";
                    str_Mensaje = "Error al obtener el tipo de cambio para contabilizar amortización. Moneda: " + lstr_IdMoneda + " Fecha: " + ldt_FchValorAcreedor + ". Préstamo: " + lstr_IdPrestamo;

                    Log.Info(str_Mensaje);

                    bool_resContabilizacion = false;
                }
                else
                {
                    // se consulta la amortización y se revisa el tipo de préstamo
                    DataSet ds_amortizacion = ConsultarAmortizacion(lstr_IdPrestamo, lint_IdTramo, null, null, ldt_FchRecepcion, null, lint_Secuencia);

                    if (ds_amortizacion.Tables.Count > 0 && ds_amortizacion.Tables["Table"].Rows.Count > 0)
                    {
                        string str_abrevAcreedor = ds_amortizacion.Tables["Table"].Rows[0]["NbrAcreedor"].ToString().Trim();
                        string str_tipoPrestamo = ds_amortizacion.Tables["Table"].Rows[0]["TipoPrestamo"].ToString().Trim();
                        str_IdOperacion = "AMORTIZ";
                       
                
                        if (bool_resContabilizacion)
                        // se procesa la amortización según el id de operación asignado
                        bool_resContabilizacion = tAsiento.EnviarAsientoDE(str_sociedad,
                                                        str_idModulo,
                                                        str_IdOperacion,
                                                        str_tipoPrestamo,
                                                        string.Empty,
                                                        str_Moneda,
                                                        ldec_Monto,
                                                        0, 0, 0, 
                                                        str_abrevAcreedor,
                                                        lstr_IdPrestamo+"."+lint_IdTramo.ToString(),
                                                        lstr_IdPrestamo,
                                                        ldt_FchTipoCambio,
                                                        out str_CodResultado,
                                                        out str_Mensaje,
                                                        out lstr_codAsiento
                                                      );

                        if (str_CodResultado == "00")
                        {
                            ModificarCodigoAsiento(lstr_IdPrestamo, lint_IdTramo, lint_Secuencia, ldt_FchValorAcreedor, lstr_IdMoneda, lstr_codAsiento, lstr_UsrCreacion, out str_CodResultado, out str_Mensaje);
                        }
                    }
                    else
                    {
                        // no se ha encontrado el desembolso guardado
                        str_CodResultado = "01";
                        str_Mensaje = "Error al obtener la amortización para su contabilización. Moneda: " + lstr_IdMoneda + ". Préstamo: " + lstr_IdPrestamo + ". Fecha: " + ldt_FchValorAcreedor.ToString();

                        Log.Info(str_Mensaje);

                        bool_resContabilizacion = false;
                    }

                   
                }

            }
            catch (Exception ex)
            {
                str_CodResultado = "01";
                str_Mensaje = "Error al contabilizar asiento de amortización. Operación: " + lstr_IdPrestamo + ". Moneda: " +
                    lstr_IdMoneda + ". " + ex.Message;

                Log.Info(str_Mensaje);
                bool_resContabilizacion = false;
            }

                return bool_resContabilizacion;
        }

        public bool ContabilizarReclasificacion(string lstr_IdPrestamo, int lint_IdTramo, int lint_Secuencia, string lstr_IdMoneda, decimal ldec_Monto,
                DateTime? ldt_FchTipoCambio, string str_abrevAcreedor, string str_tipoPrestamo, string lstr_Modal, string lstr_Estado,
                string lstr_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {

            bool bool_resContabilizacion = true;
            bool bool_tipoCambioEncontrado = true;
            clsTiposAsiento tiposAsiento = new clsTiposAsiento();
            clsTiposCambio tiposCambio = new clsTiposCambio();
            string str_idModulo = "IdModulo IN ('DE')";
            string str_sociedad = "G206";
            string str_Moneda = "USD";
            string str_IdOperacion = "";
            string lstr_codAsiento=string.Empty;

            str_CodResultado = "00";
            str_Mensaje = "Contabilizado Correctamente";
            try
            {
                // se revisa la moneda por si se debe realizar un cambio a dolares
                if (lstr_IdMoneda != "CRC" && lstr_IdMoneda != "USD" && lstr_IdMoneda != "EUR")
                {
                    // se trae el tipo de cambio y se realiza la conversión a USD
                    bool_tipoCambioEncontrado = false;

                    DataSet ds_tipoCambio = tiposCambio.ConsultarTiposCambio(lstr_IdMoneda, ldt_FchTipoCambio, null);
                    if (ds_tipoCambio.Tables.Count > 0 && ds_tipoCambio.Tables["Table"].Rows.Count > 0)
                    {
                        // se realiza el cambio a dolares para procesar el asiento
                        decimal ldec_valor = Convert.ToDecimal(ds_tipoCambio.Tables["Table"].Rows[0]["Valor"]);

                        ldec_Monto /= ldec_valor;
                        //lstr_IdMoneda = "USD";

                        bool_tipoCambioEncontrado = true;
                    }
                }
                else
                {

                    str_Moneda = lstr_IdMoneda;
                }

                // si no se encontró el tipo de cambio se genera el error y se notifica
                if (!bool_tipoCambioEncontrado)
                {
                    // error al obtener el tipo de cambio
                    str_CodResultado = "01";
                    str_Mensaje = "Error al obtener el tipo de cambio para contabilizar reclasificación. Moneda: " + lstr_IdMoneda + " Fecha: " + ldt_FchTipoCambio + ". Préstamo: " + lstr_IdPrestamo;

                    Log.Info(str_Mensaje);

                    bool_resContabilizacion = false;
                }
                else
                {
                    

                    str_IdOperacion = "RECLA CXP";

                        if (bool_resContabilizacion)
                            // se procesa la amortización según el id de operación asignado
                            bool_resContabilizacion = tAsiento.EnviarAsientoDE(str_sociedad,
                                                            str_idModulo,
                                                            str_IdOperacion,
                                                            str_tipoPrestamo,
                                                            string.Empty,
                                                            str_Moneda,
                                                            ldec_Monto,
                                                            0, 0, 0, 
                                                            str_abrevAcreedor,
                                                            lstr_IdPrestamo + "." + lint_IdTramo.ToString(),
                                                            lstr_IdPrestamo,
                                                            ldt_FchTipoCambio,
                                                            out str_CodResultado,
                                                            out str_Mensaje,out lstr_codAsiento
                                                          );


                        if (str_CodResultado == "00")
                        {
                            ModificarCodigoAsiento(lstr_IdPrestamo, lint_IdTramo, lint_Secuencia,ldt_FchTipoCambio, lstr_IdMoneda, lstr_codAsiento, lstr_UsrCreacion, out str_CodResultado, out str_Mensaje);
                        }
                }

            }
            catch (Exception ex)
            {
                str_CodResultado = "01";
                str_Mensaje = "Error al contabilizar asiento de reclasificación. Operación: " + lstr_IdPrestamo + ". Moneda: " +
                    lstr_IdMoneda + ". " + ex.Message;

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
        //            lstr_Mensaje = "Error al contabilizar asiento de Comisión (Deuda Externa). Operación: " + lstr_idOperacion + ". Acreedor: " +
        //                lstr_abrevAcreedor + ". " + ex.Message;

        //            Log.Info(lstr_Mensaje);
        //            bool_enviado = false;
        //        }
        //        resAsientosLog = reg_Bitacora.ufnRegistrarAccionBitacora("DE", "1", "Amortización", "Resultado de Contabilización: " + logAsiento);
        //    }

        //    return bool_enviado;
        //}

        #endregion

        #region Constructor

        public clsAmortizacion()
        { }

        #endregion
    }
}