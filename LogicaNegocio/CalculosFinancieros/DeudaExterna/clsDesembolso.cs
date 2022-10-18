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
    public class clsDesembolso
    {
        #region Variables

        private static readonly ILog Log = LogManager.GetLogger("FileAppender");
        //private wsAsientos.ServicioContable asientos = new wsAsientos.ServicioContable();
        private static clsTiposAsiento tasientos = new clsTiposAsiento();
        private static string lstr_formato_fecha = "dd/MM/yyyy";

        private tBitacora reg_Bitacora = new tBitacora();
        private clsTiposAsiento tAsiento = new clsTiposAsiento();
        private string resAsientosLog= string.Empty;
        //private wsSG.wsSistemaGestor ws_SGService = new wsSG.wsSistemaGestor();

        #endregion

        #region Métodos

        public DataSet ConsultarDesembolso(string lstr_IdPrestamo, int? lint_IdTramo, decimal? ldec_MontoDesde = null, decimal? ldec_MontoHasta = null,  string lstr_Moneda = null,
            DateTime? ldt_FchDesde = null, DateTime? ldt_FchHasta = null, DateTime? ldt_FchEstimadaDesde = null, DateTime? ldt_FchEstimadaHasta = null, string lstr_TipoDesembolso = null, string lstr_Descripcion = null, int? lint_Secuencia = null)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultaDesembolso cr_Procedimiento = new clsConsultaDesembolso(lstr_IdPrestamo, lint_IdTramo, ldec_MontoDesde, ldec_MontoHasta, lstr_Moneda,
                                                            ldt_FchDesde, ldt_FchHasta, ldt_FchEstimadaDesde, ldt_FchEstimadaHasta, lstr_TipoDesembolso, lstr_Descripcion, lint_Secuencia);
                if (String.Equals(cr_Procedimiento.Lstr_CodigoResultado, "00"))
                {
                    lds_TablasConsulta.ReadXmlSchema(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaSchema)));
                    lds_TablasConsulta.ReadXml(new System.Xml.XmlTextReader(new System.IO.StringReader(cr_Procedimiento.Lstr_RespuestaXML)));
                }
            }
            catch (Exception ex)
            {
            }
            return lds_TablasConsulta;
        }

        public bool CrearDesembolso(string lstr_IdPrestamo, int lint_IdTramo, decimal ldec_Monto, string lstr_Moneda,
            DateTime ldt_FchDesembolso, DateTime ldt_FchEstimada, string lstr_TipoDesembolso, string lstr_Descripcion, int lint_secuencia, string lstr_Estado, string lstr_UsrCreacion,
            out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsCreaDesembolso cr_Procedimiento = new clsCreaDesembolso(lstr_IdPrestamo, lint_IdTramo, ldec_Monto, lstr_Moneda,
                                                            ldt_FchDesembolso, ldt_FchEstimada, lstr_TipoDesembolso, lstr_Descripcion, lint_secuencia, lstr_Estado, lstr_UsrCreacion);

                str_CodResultado = cr_Procedimiento.Lstr_CodigoResultado;
                str_Mensaje = cr_Procedimiento.Lstr_MensajeRespuesta;

                Log.Info(str_Mensaje);
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_ResCreacion = true;

                    if (!lstr_TipoDesembolso.ToLower().Contains("capitaliza")) //este tipo de desembolso se contabiliza con otra tira
                    // se contabiliza la creación del desembolso
                        bool_ResCreacion = ContabilizarDesembolso(lstr_IdPrestamo, lint_IdTramo, lint_secuencia, ldec_Monto, lstr_Moneda, ldt_FchDesembolso, ldt_FchEstimada,
                                            lstr_TipoDesembolso, lstr_Descripcion, lstr_Estado, lstr_UsrCreacion,
                                            out str_CodResultado, out str_Mensaje);
                    if (str_CodResultado == "00")
                    {
                        clsGiroEstimado giroest = new clsGiroEstimado();
                        //giroest.ModificarGiroEstimado(lstr_IdPrestamo, lint_IdTramo, ldt_FchDesembolso,)
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

        public bool ModificarCodigoAsiento(string lstr_IdPrestamo, int? lint_IdTramo, Int64? lint_Secuencia, DateTime? ldt_FchProgramada,
            string lstr_IdMoneda, string lstr_CodAsiento, string lstr_UsrModifica, out string str_CodResultado, out string str_Mensaje)
        {
            bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsModificarCodigosAsiento cr_Procedimiento = new clsModificarCodigosAsiento(lstr_IdPrestamo, lint_IdTramo, lint_Secuencia, ldt_FchProgramada,
            lstr_IdMoneda, "DESEMBOLSOS", lstr_CodAsiento, lstr_UsrModifica);

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


        public bool ModificarDesembolso(string lstr_IdPrestamo, int lint_IdTramo, DateTime ldt_FchDesembolso, DateTime ldt_FchEstimada, string lstr_Moneda, int lint_Secuencia, decimal ldec_Monto,
            string lstr_UsrModifica, DateTime ldt_FchModifica,
            out string str_CodResultado, out string str_Mensaje)
        {
              bool bool_ResCreacion = false;
            str_CodResultado = String.Empty;
            str_Mensaje = String.Empty;
            try
            {
                clsModificaGiro cr_Procedimiento = new clsModificaGiro(lstr_IdPrestamo, lint_IdTramo, ldt_FchDesembolso,ldt_FchEstimada, lstr_Moneda, lint_Secuencia, ldec_Monto,
            lstr_UsrModifica, ldt_FchModifica);
                str_CodResultado = cr_Procedimiento.Lstr_CodigoResultado;
                str_Mensaje = cr_Procedimiento.Lstr_MensajeRespuesta;

                Log.Info(str_Mensaje);
                if (String.Equals(str_CodResultado, "00"))
                {
                    bool_ResCreacion = true;
                }
           
                //if (String.Equals(str_CodResultado, "00"))
                //{
                //    bool_ResCreacion = true;

                //    // se contabiliza la creación del desembolso
                //    bool_ResCreacion = ContabilizarDesembolso(lstr_IdPrestamo, lint_IdTramo, ldec_Monto, lstr_Moneda, ldt_FchDesembolso,
                //                            lstr_TipoDesembolso, lstr_Descripcion, lstr_Estado, lstr_UsrCreacion,
                //                            out str_CodResultado, out str_Mensaje);
                //}
            }
            catch (Exception ex)
            {
                str_CodResultado = "99";
                str_Mensaje = ex.ToString();
            }
            return bool_ResCreacion;
        }

        public bool ModificarGiro(string lstr_IdPrestamo, int lint_IdTramo, DateTime ldt_FchDesembolso, DateTime ldt_FchEstimada, string lstr_Moneda, int lint_Secuencia, decimal ldec_Monto,
            string lstr_UsrModifica, DateTime ldt_FchModifica,
            out string str_CodResultado, out string str_Mensaje)
        {
            return this.ModificarDesembolso(lstr_IdPrestamo, lint_IdTramo, ldt_FchDesembolso,ldt_FchEstimada, lstr_Moneda, lint_Secuencia, ldec_Monto,
            lstr_UsrModifica, ldt_FchModifica,
            out str_CodResultado, out str_Mensaje);
        }

        public bool ContabilizarDesembolso(string lstr_IdPrestamo, int lint_IdTramo, int lint_Secuencia, decimal ldec_Monto, string lstr_Moneda,
            DateTime ldt_FchDesembolso, DateTime ldt_FchEstimada, string lstr_TipoDesembolso, string lstr_Descripcion, string lstr_Estado, string lstr_UsrCreacion,
            out string str_CodResultado, out string str_Mensaje)
        {
            // variables locales
            bool bool_ResContabilizacion = true;
            bool bool_tipoCambioEncontrado = true;
            clsTiposAsiento tiposAsiento = new clsTiposAsiento();
            clsTiposCambio tiposCambio = new clsTiposCambio();
            string str_idModulo = "IdModulo IN ('DE')";
            string str_sociedad = "G206";
            string str_Moneda = "USD";
            string str_IdOperacion = "";
            string str_codAsiento = string.Empty;

            str_CodResultado = "00";
            str_Mensaje = "Contabilizado Correctamente";
            try{
            // se revisa la moneda por si se debe realizar un cambio a dolares
            if (lstr_Moneda != "CRC" && lstr_Moneda != "USD" && lstr_Moneda != "EUR")
            {
                // se trae el tipo de cambio y se realiza la conversión a USD
                bool_tipoCambioEncontrado = false;

                DataSet ds_tipoCambio = tiposCambio.ConsultarTiposCambio(lstr_Moneda, ldt_FchDesembolso, null,"N");
                if (ds_tipoCambio.Tables.Count > 0 && ds_tipoCambio.Tables["Table"].Rows.Count > 0)
                {
                    // se realiza el cambio a dolares para procesar el asiento
                    decimal ldec_valor = Convert.ToDecimal(ds_tipoCambio.Tables["Table"].Rows[0]["Valor"]);

                    ldec_Monto /= ldec_valor;
                    //lstr_Moneda = "USD";

                    bool_tipoCambioEncontrado = true;
                }
            }
            else
            {

                str_Moneda = lstr_Moneda;
            }

            // si no se encontró el tipo de cambio se genera el error y se notifica
            if (!bool_tipoCambioEncontrado)
            {
                // error al obtener el tipo de cambio
                str_CodResultado = "01";
                str_Mensaje = "Error al obtener el tipo de cambio para contabilizar desembolso. Moneda: " + lstr_Moneda + " Fecha: " + ldt_FchDesembolso.ToString(lstr_formato_fecha);

                Log.Info(str_Mensaje);

                bool_ResContabilizacion = false;
            }
            else
            {
                // Se revisa el tipo de desembolso y tipo de préstamo, generando el asiento que corresponda
                DataSet miDesembolso = ConsultarDesembolso(lstr_IdPrestamo, lint_IdTramo, null, null,lstr_Moneda, ldt_FchDesembolso, ldt_FchDesembolso,ldt_FchEstimada,ldt_FchEstimada,
                                                            null, null, lint_Secuencia);

                if (miDesembolso.Tables.Count > 0 && miDesembolso.Tables["Table"].Rows.Count > 0)
                {

                    //Obtener abreviatura y tipo de prestamo
                    clsPrestamo prestamo = new clsPrestamo();
                    DataTable ldat_Prestamo = prestamo.ConsultarPrestamo(miDesembolso.Tables["Table"].Rows[0]["IdPrestamo"].ToString().Trim(),
                        null, null, null, null, null, null, null, null, null, null, null, null).Tables[0];
                    //

                    //string abrev_acreedor = Convert.ToString(miDesembolso.Tables["Table"].Rows[0]["AbrevAcreedor"]).Trim();
                    //string tipo_prestamo = Convert.ToString(miDesembolso.Tables["Table"].Rows[0]["TipoPrestamo"]).Trim();

                    string abrev_acreedor = Convert.ToString(ldat_Prestamo.Rows[0]["NbrAcreedor"]).Trim();
                    string tipo_prestamo = Convert.ToString(ldat_Prestamo.Rows[0]["TipoPrestamo"]).Trim();
                    str_IdOperacion = "DESEMB";
                    if (ldec_Monto < 0)
                    {
                        // Devolución de desembolsos en efectivo (tipo 1 y 4, en ocasiones tipo 2) (operación DE02)
                        str_IdOperacion = "DEVOLUC";
                        ldec_Monto *= -1;
                    }
               

                    //String lstr_codAsiento;//varibale para modificar los codigos asiento DE

                    if (bool_ResContabilizacion)
                    // se procesa el asiento según el id de operación asignado
                        bool_ResContabilizacion = tAsiento.EnviarAsientoDE(str_sociedad,
                                                    str_idModulo,
                                                    str_IdOperacion,
                                                    tipo_prestamo,
                                                    lstr_TipoDesembolso,
                                                    str_Moneda,
                                                    ldec_Monto,
                                                    0, 0, 0, 
                                                    abrev_acreedor,
                                                    lstr_IdPrestamo + "." + lint_IdTramo.ToString(),
                                                    lstr_IdPrestamo,
                                                    ldt_FchDesembolso,
                                                    out str_CodResultado,
                                                    out str_Mensaje,
                                                    out str_codAsiento
                                                  );

                    if (str_CodResultado == "00")
                    {
                        ModificarCodigoAsiento(lstr_IdPrestamo, lint_IdTramo, lint_Secuencia, ldt_FchDesembolso, str_Moneda, str_codAsiento, lstr_UsrCreacion, out str_CodResultado, out str_Mensaje);
                    }
                }
                else
                {
                    // no se ha encontrado el desembolso guardado
                    str_CodResultado = "01";
                    str_Mensaje = "Error al obtener el desembolso para su contabilización. Moneda: " + lstr_Moneda + ". Préstamo: " + lstr_IdPrestamo + ". Tipo Desembolso: " + lstr_TipoDesembolso;

                    Log.Info(str_Mensaje);

                    bool_ResContabilizacion = false;
                }
            }
             }
            catch (Exception ex)
            {
                str_CodResultado = "01";
                str_Mensaje = "Error al contabilizar asiento de desembolso. Operación: " + lstr_IdPrestamo + ". Moneda: " +
                    lstr_Moneda + ". " + ex.Message;

                Log.Info(str_Mensaje);
                bool_ResContabilizacion = false;
            }

            return bool_ResContabilizacion;
        }



        #endregion

        #region Constructor

        public clsDesembolso()
        { }

        #endregion
    }
}