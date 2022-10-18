using System;
using System.Globalization;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using LogicaNegocio.CalculosFinancieros.DeudaExterna;
using LogicaNegocio.Seguridad;

namespace WebServiceCalculosFinancieros
{
    /// <summary>
    /// Summary description for wsDeudaExterna
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class wsDeudaExterna : System.Web.Services.WebService
    {
        #region Variables 

        private static string lstr_UsrModifica = "SG";
        private static string lstr_UsrCreacion = "SG";
        private static string lstr_formato_fecha = "dd/MM/yyyy";
        private static string lstr_separador_decimal = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
        
        #endregion

        #region Procesos

        [WebMethod]
        public string Reclasificar(string ldt_Fecha)
        {
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";

            DateTime? Fecha = null;
            try
            {
                if (!string.IsNullOrEmpty(ldt_Fecha))
                    Fecha = DateTime.ParseExact(ldt_Fecha, lstr_formato_fecha, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
            }
            if (mensaje == "")
            {
                try
                {

                    clsAmortizacion lcls_Amortizacion = new clsAmortizacion();
                    lcls_Amortizacion.Reclasificar(Fecha, out codSalida, out txtSalida);


                    codSalida = ((codSalida == "00") || (codSalida == "01")) ? codSalida : "99"; mensaje = "Código " + codSalida + ": " + txtSalida;
                }
                catch (Exception e)
                {
                    mensaje = "Código 99: " + e.ToString();
                }
            }
            return mensaje;
        }
        #endregion Procesos

        #region TipoCambio
        [WebMethod]
        public string ActualizarTipoCambio()
        {

            string mensaje = "Código 00: Proceso Exitoso";
            try
            {
                clsCalculosDeudaExterna cls = new clsCalculosDeudaExterna();
                cls.ActualizaTipoCambio("SG");
                //cls.ActualizaIndicadoresEconomicos("SG");
            }
            catch (Exception ex) {
                mensaje = "Código 99: " + ex.ToString();
            }
            return mensaje;
        }
        [WebMethod]
        public string ActualizarIndicadores()
        {

            string mensaje = "Código 00: Proceso Exitoso";
            try
            {
                clsCalculosDeudaExterna cls = new clsCalculosDeudaExterna();
                //cls.ActualizaTipoCambio("SG");
                cls.ActualizaIndicadoresEconomicos("SG");
            }
            catch (Exception ex)
            {
                mensaje = "Código 99: " + ex.ToString();
            }
            return mensaje;
        }
        #endregion

        #region DTSSIGADE
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstr_pack_name"></param>
        /// <param name="lstr_proj_name"></param>
        /// <returns></returns>
        [WebMethod]
        public string EjecutarDTSSIGADE(string lstr_pack_name = "Ejecuta_DTS.dtsx", string lstr_proj_name = "SIGADE")
        {
            DataSet ldas_DTS = new DataSet();
            string mensaje = "00: Finalizado.";

            string str_CodResultado = String.Empty;
            string str_Mensaje = String.Empty;
            try
            {
                if (string.IsNullOrEmpty(lstr_pack_name))
                    lstr_pack_name = "Ejecuta_DTS.dtsx";
                if (string.IsNullOrEmpty(lstr_proj_name))
                    lstr_proj_name = "SIGADE";

                clsDTSSIGADE lcls_DTSSIGADE = new clsDTSSIGADE();
                ldas_DTS = lcls_DTSSIGADE.EjecutarDTSSIGADE(lstr_pack_name, lstr_proj_name, out str_CodResultado, out str_Mensaje);
                mensaje = str_CodResultado + ": " + str_Mensaje;
                //aqui extraer del data set los mensajes del procedimiento
            }
            catch (Exception e)
            {
                mensaje = "99: " + e.ToString();
            }

            return mensaje;
        }
        #endregion DTSSIGADE

        #region Acreedores

        /// <summary>
        /// Crea un acreedor en el sistema gestor
        /// </summary>
        /// <param name="lstr_Cedula">Cédula del acreedor</param>
        /// <param name="lstr_TipoIdAcreedor">Tipo de acreedor</param>
        /// <param name="lstr_NomAcreedor">Nombre del acreedor</param>
        /// <param name="lstr_Abreviatura">Abreviatura de acreedor</param>
        /// <param name="lstr_Contacto">Contacto de acreedor</param>
        /// <param name="lstr_Telefono">Teléfonos de acreedor</param>
        /// <param name="lstr_Direccion">Dirección de acreedor</param>
        /// <param name="lstr_Pais">País de acreedor</param>
        /// <param name="lstr_TipoAcreedor">Tipo de acreedor</param>
        /// <param name="lstr_PaisInstitucion">País de institución</param>
        /// <param name="lstr_CatPersona">Categoría de persona</param>
        /// <param name="lstr_TipoPersona">Tipo de persona</param>
        /// <returns>Retorna un mensaje con el código y texto de la transacción</returns>
        [WebMethod]
        //public string CrearAcreedor(int lint_NroAcreedor, string lstr_Cedula, string lstr_TipoIdAcreedor, string lstr_NomAcreedor, string lstr_Abreviatura,
        //    string lstr_Contacto, string lstr_Telefono, string lstr_Direccion, string lstr_Pais, string lstr_TipoAcreedor,
        //    string lstr_PaisInstitucion, string lstr_CatPersona, string lstr_TipoPersona)

        public string CrearAcreedor(string lint_NroAcreedor, string lstr_NomAcreedor, string lstr_Abreviatura,
            string lstr_Contacto, string lstr_Telefono, string lstr_Direccion, string lstr_Pais, string lstr_TipoAcreedor,
            string lstr_PaisInstitucion)//, string lstr_IdCtaContable)
        {
            string lstr_Estado = "ACT";
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";

            try
            {
                clsAcreedor lcls_Acreedor = new clsAcreedor();
                lcls_Acreedor.CrearAcreedor(Convert.ToInt32( lint_NroAcreedor), "-", "-", lstr_NomAcreedor, lstr_Abreviatura, lstr_Contacto,
                    lstr_Telefono, lstr_Direccion, lstr_Pais, lstr_TipoAcreedor, lstr_PaisInstitucion,
                    "-", "-", string.Empty, lstr_Estado, lstr_UsrCreacion, out codSalida, out txtSalida);

                
                codSalida = ((codSalida == "00")||(codSalida == "01")) ? codSalida : "99"; mensaje = "Código " + codSalida + ": " + txtSalida;
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return mensaje;
        }

        /// <summary>
        /// Consulta un acreedor en el sistema gestor
        /// </summary>
        /// <param name="lint_opcion">Tipo de operación a realizar: 
        /// -1 Consulta por Cédula
        /// -2 Consulta por Tipo de Acreedor
        /// -3 Consulta por País
        /// -4 Consulta por Número de Acreedor
        /// -5 Consulta por Nombre de Acreedor
        /// -6 Consulta por Fecha de inicio y fecha de Fin</param>
        /// <param name="lstr_valor1">Valor de entrada 1</param>
        /// <param name="lstr_valor2">Valor de entrada 2</param>
        /// <returns>Retorna un dataset con el resultado de la consulta</returns>
        //[WebMethod]
        //public DataSet ConsultarAcreedor(int lint_opcion, string lstr_valor1, string lstr_valor2)
        //{
        //    DataSet ldas_Acreedor = new DataSet();
        //    string mensaje = "";

        //    try
        //    {
        //        clsAcreedor lcls_Acreedor = new clsAcreedor();

        //        switch (lint_opcion)
        //        {
        //            case 1: ldas_Acreedor = lcls_Acreedor.ConsultarAcreedor(lstr_valor1); break;
        //            case 2: ldas_Acreedor = lcls_Acreedor.ConsultarAcreedor(null, lstr_valor1); break;
        //            case 3: ldas_Acreedor = lcls_Acreedor.ConsultarAcreedor(null, null, lstr_valor1); break;
        //            case 4: ldas_Acreedor = lcls_Acreedor.ConsultarAcreedor(null, null, null, lstr_valor1); break;
        //            case 5: ldas_Acreedor = lcls_Acreedor.ConsultarAcreedor(null, null, null, null, lstr_valor1); break;
        //            case 6: ldas_Acreedor = lcls_Acreedor.ConsultarAcreedor(null, null, null, null, null, lstr_valor1, lstr_valor2); break;
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        mensaje = "Código 99: " + e.ToString();
        //    }

        //    return ldas_Acreedor;
        //}

        [WebMethod]
        public DataSet ConsultarAcreedor(string lstr_NroAcreedor, string lstr_NbrAcreedor, string lstr_FchInicio, string lstr_FchFinal, string lstr_Pais, string lstr_Tipo, string lstr_Estado="ACT")
        {
            DataSet ldas_Acreedor = new DataSet();
            string mensaje = "";

            DateTime? FchInicio = null;
            DateTime? FchFinal = null;
            try
            {
                try
                {
                    if (!string.IsNullOrEmpty(lstr_FchInicio))
                        FchInicio = DateTime.ParseExact(lstr_FchInicio, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(lstr_FchFinal))
                        FchFinal = DateTime.ParseExact(lstr_FchFinal, lstr_formato_fecha, CultureInfo.InvariantCulture);
                }
                catch(Exception ex)
                {
                    mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
                }

                if (mensaje == "")
                {
                    clsAcreedor lcls_Acreedor = new clsAcreedor();
                    ldas_Acreedor = lcls_Acreedor.ConsultarAcreedor(lstr_Tipo, lstr_Pais, lstr_NroAcreedor, lstr_NbrAcreedor, FchInicio, FchFinal, lstr_Estado);
                }
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return ldas_Acreedor;
        }

        /// <summary>
        /// Modifica un acreedor en el sistema gestor
        /// </summary>
        /// <param name="lstr_Cedula">Cédula del acreedor</param>
        /// <param name="lstr_TipoIdAcreedor">Tipo de acreedor</param>
        /// <param name="lstr_NomAcreedor">Nombre del acreedor</param>
        /// <param name="lstr_Abreviatura">Abreviatura de acreedor</param>
        /// <param name="lstr_Contacto">Contacto de acreedor</param>
        /// <param name="lstr_Telefono">Teléfonos de acreedor</param>
        /// <param name="lstr_Direccion">Dirección de acreedor</param>
        /// <param name="lstr_Pais">País de acreedor</param>
        /// <param name="lstr_TipoAcreedor">Tipo de acreedor</param>
        /// <param name="lstr_PaisInstitucion">País de institución</param>
        /// <param name="lstr_CatPersona">Categoría de persona</param>
        /// <param name="lstr_TipoPersona">Tipo de persona</param>
        /// <returns>Retorna un mensaje con el código y texto de la transacción</returns>
        [WebMethod]
        //public string ModificarAcreedor(int lint_NroAcreedor, string lstr_Cedula, string lstr_TipoIdAcreedor, string lstr_NomAcreedor, string lstr_Abreviatura,
        //    string lstr_Contacto, string lstr_Telefono, string lstr_Direccion, string lstr_Pais, string lstr_TipoAcreedor,
        //    string lstr_PaisInstitucion, string lstr_CatPersona, string lstr_TipoPersona)

        public string ModificarAcreedor(string lint_NroAcreedor, string lstr_NomAcreedor, string lstr_Abreviatura,
            string lstr_Contacto, string lstr_Telefono, string lstr_Direccion, string lstr_Pais, string lstr_TipoAcreedor,
            string lstr_PaisInstitucion)
        {
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";
            DateTime ldt_FchModifica = new DateTime();
            try 
            {
                try
                {
                    ldt_FchModifica = Convert.ToDateTime(ConsultarAcreedor(lint_NroAcreedor, null, null, null, null, null).Tables[0].Rows[0]["FchModifica"].ToString());
                }
                catch (Exception ex){
                    mensaje = "Código 99: Error al obtener registro por modificar " + ex.ToString();
                }
                if (mensaje == "")
                {
                    clsAcreedor lcls_Acreedor = new clsAcreedor();
                    lcls_Acreedor.ModificarAcreedor(Convert.ToInt32(lint_NroAcreedor), "-", "-", lstr_NomAcreedor, lstr_Abreviatura, lstr_Contacto, lstr_Telefono,
                        lstr_Direccion, lstr_Pais, lstr_TipoAcreedor, lstr_PaisInstitucion, "-", "-", lstr_UsrModifica, ldt_FchModifica, out codSalida, out txtSalida);

                    codSalida = ((codSalida == "00")||(codSalida == "01")) ? codSalida : "99"; mensaje = "Código " + codSalida + ": " + txtSalida;
                }
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return mensaje;
        }


        [WebMethod]
        public string EliminarAcreedor(string lint_NroAcreedor)
        {
            DataTable ldat_Acreedores = new DataTable();
            DateTime ldt_FchModifica = new DateTime();
            string lstr_Estado = "";
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";
            ldat_Acreedores = ConsultarAcreedor(lint_NroAcreedor, null, null, null, null, null).Tables[0];

            try
            {
                lstr_Estado = ldat_Acreedores.Rows[0]["Estado"].ToString();
                ldt_FchModifica = Convert.ToDateTime(ldat_Acreedores.Rows[0]["FchModifica"].ToString());
                clsAcreedor lcls_Acreedor = new clsAcreedor();
                lcls_Acreedor.CambiarEstadoAcreedor(Convert.ToInt32(lint_NroAcreedor), lstr_Estado, lstr_UsrModifica, ldt_FchModifica, out codSalida, out txtSalida);
                codSalida = ((codSalida == "00")||(codSalida == "01")) ? codSalida : "99"; mensaje = "Código " + codSalida + ": " + txtSalida;
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }
            return mensaje;
        }

        #endregion

        #region Amortizaciones

        /// <summary>
        /// Crea una amortización en el sistema gestor
        /// </summary>
        /// <param name="lstr_IdPrestamo">Identificación del préstamo</param>
        /// <param name="lint_IdTramo">Identificación del tramo</param>
        /// <param name="lstr_IdMoneda">Identificador de moneda</param>
        /// <param name="ldec_Monto">Monto de amortización</param>
        /// <param name="ldt_FchValorAcreedor">Fecha del valor de acreedor</param>
        /// <param name="ldt_FchRecepcion">Fecha de recepción</param>
        /// <param name="ldt_FchTipoCambio">Fecha de tipo de cambio</param>
        /// <param name="lstr_Modal">Modal de amortización</param>
        /// <param name="lint_secuencia">Identificar único que diferencia la amortización</param>
        /// <returns>Retorna un mensaje con el código y texto de transacción</returns>
        [WebMethod]
        public string CrearAmortizacion(string lstr_IdPrestamo, string lint_IdTramo, string ldec_Monto, 
            string ldt_FchValorAcreedor, string ldt_FchRecepcion, string ldt_FchTipoCambio,
            string lstr_IdMoneda, string lstr_Modal, string lstr_EstadoSigade, string lint_secuencia)
        {
            string lstr_Estado = "ACT";
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";
            DateTime? FchValorAcreedor = null;
            DateTime? FchRecepcion = null;
            DateTime? FchTipoCambio = null;

            try
            {
                try
                {
                    if (!string.IsNullOrEmpty(ldt_FchValorAcreedor) && ldt_FchValorAcreedor != "01/01/1900")   
                    FchValorAcreedor = DateTime.ParseExact(ldt_FchValorAcreedor, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(ldt_FchRecepcion) && ldt_FchRecepcion != "01/01/1900")   
                    FchRecepcion = DateTime.ParseExact(ldt_FchRecepcion, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(ldt_FchTipoCambio) && ldt_FchTipoCambio != "01/01/1900")   
                    FchTipoCambio = DateTime.ParseExact(ldt_FchTipoCambio, lstr_formato_fecha, CultureInfo.InvariantCulture);
                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
                }
                if (mensaje == ""){
                    ldec_Monto = ldec_Monto.Replace(",", lstr_separador_decimal);
                    ldec_Monto = ldec_Monto.Replace(".", lstr_separador_decimal);
                    clsAmortizacion lcls_Amortizacion = new clsAmortizacion();
                    lcls_Amortizacion.CrearAmortizacion(lstr_IdPrestamo, Convert.ToInt32(lint_IdTramo), lstr_IdMoneda, Convert.ToDecimal(ldec_Monto),
                    FchValorAcreedor, FchRecepcion, FchTipoCambio, lstr_Modal, Convert.ToInt32(lint_secuencia), lstr_Estado, lstr_EstadoSigade, lstr_UsrCreacion, out codSalida, out txtSalida);

                    codSalida = ((codSalida == "00")||(codSalida == "01")) ? codSalida : "99"; mensaje = "Código " + codSalida + ": " + txtSalida;
                }

            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return mensaje;
        }

        /// <summary>
        /// Modifica una amortización en el sistema gestor
        /// </summary>
        /// <param name="lstr_IdPrestamo">Identificación del préstamo</param>
        /// <param name="lint_IdTramo">Identificación del tramo</param>
        /// <param name="lstr_IdMoneda">Identificador de moneda</param>
        /// <param name="ldec_Monto">Monto de amortización</param>
        /// <param name="ldt_FchValorAcreedor">Fecha del valor de acreedor</param>
        /// <param name="ldt_FchRecepcion">Fecha de recepción</param>
        /// <param name="ldt_FchTipoCambio">Fecha de tipo de cambio</param>
        /// <param name="lstr_Modal">Modal de amortización</param>
        /// <returns>Retorna un mensaje con el código y texto de transacción</returns>
        [WebMethod]
        public string ModificarAmortizacion(string lstr_IdPrestamo, string lint_IdTramo, string ldec_Monto,
            string ldt_FchValorAcreedor, string ldt_FchRecepcion, string ldt_FchTipoCambio,
            string lstr_IdMoneda, string lstr_Modal, string lstr_EstadoSigade, string lint_Secuencia, string lint_SecuenciaAnt)
        {
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";
            DateTime ldt_FchModifica = new DateTime();
            DateTime? FchValorAcreedor = null;
            DateTime? FchRecepcion = null;
            DateTime? FchTipoCambio = null;

            int? id_tramo = null;
            int? Secuencia = null;
            int? SecuenciaAnt = null;
            Decimal? Monto = null;
            Decimal? MontoAntes = null;
            try
            {
                try
                {
                    if (!string.IsNullOrEmpty(ldt_FchValorAcreedor) && ldt_FchValorAcreedor != "01/01/1900")   
                    FchValorAcreedor = DateTime.ParseExact(ldt_FchValorAcreedor, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(ldt_FchRecepcion) && ldt_FchRecepcion != "01/01/1900")   
                    FchRecepcion = DateTime.ParseExact(ldt_FchRecepcion, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(ldt_FchTipoCambio) && ldt_FchTipoCambio != "01/01/1900")   
                    FchTipoCambio = DateTime.ParseExact(ldt_FchTipoCambio, lstr_formato_fecha, CultureInfo.InvariantCulture);
                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
                }
                try
                {
                    if (!string.IsNullOrEmpty(lint_IdTramo))
                        id_tramo = Convert.ToInt32(lint_IdTramo);
                    if (!string.IsNullOrEmpty(lint_Secuencia))
                        Secuencia = Convert.ToInt32(lint_Secuencia);
                    if (!string.IsNullOrEmpty(lint_SecuenciaAnt))
                        SecuenciaAnt = Convert.ToInt32(lint_SecuenciaAnt);
                    else
                        SecuenciaAnt = Secuencia;
                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: Formato incorrecto de campo ";
                }
                try
                {

                    ldec_Monto = ldec_Monto.Replace(",", lstr_separador_decimal);
                    ldec_Monto = ldec_Monto.Replace(".", lstr_separador_decimal);
                    if (!string.IsNullOrEmpty(ldec_Monto))
                        Monto = Convert.ToDecimal(ldec_Monto);
                    //if (!string.IsNullOrEmpty(ldec_PeriodoDias))
                    //    PeriodoDias = Convert.ToDecimal(ldec_PeriodoDias);
                    //if (!string.IsNullOrEmpty(ldec_Monto))
                    //    Monto = Convert.ToDecimal(ldec_Monto);
                    //if (!string.IsNullOrEmpty(ldec_DiasGracia))
                    //    DiasGracia = Convert.ToDecimal(ldec_DiasGracia);
                    //if (!string.IsNullOrEmpty(ldec_TasaPunitiva))
                    //    TasaPunitiva = Convert.ToDecimal(ldec_TasaPunitiva);
                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: Formato incorrecto de campo ";
                }
                
                if (mensaje == "")
                {
                    try{
                        DataSet ds_Amortizacion = ConsultarAmortizacion(lstr_IdPrestamo, lint_IdTramo, null, null, ldt_FchRecepcion, null, lint_SecuenciaAnt);
                         ldt_FchModifica = Convert.ToDateTime(ds_Amortizacion.Tables[0].Rows[0]["FchModifica"].ToString());
                         MontoAntes = Convert.ToDecimal(ds_Amortizacion.Tables[0].Rows[0]["Monto"].ToString());
                    }
                    catch (Exception ex){
                        mensaje = "Código 99: Error al obtener registro por modificar " + ex.ToString();
                    }
                    if (mensaje == "")
                    {
                        clsAmortizacion lcls_Amortizacion = new clsAmortizacion();
                        lcls_Amortizacion.ModificarAmortizacion(lstr_IdPrestamo, Convert.ToInt32(id_tramo), lstr_IdMoneda, Monto, MontoAntes,
                        FchValorAcreedor, FchRecepcion, FchTipoCambio, lstr_Modal, lstr_EstadoSigade, Secuencia, SecuenciaAnt, lstr_UsrModifica, ldt_FchModifica, out codSalida, out txtSalida);

                        codSalida = ((codSalida == "00")||(codSalida == "01")) ? codSalida : "99"; mensaje = "Código " + codSalida + ": " + txtSalida;
                    }
                }
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return mensaje;
        }

        /// <summary>
        /// Cambia el estado de una amortización en el sistema gestor
        /// </summary>
        /// <param name="lstr_IdPrestamo">Identificación del préstamo</param>
        /// <param name="lint_IdTramo">Identificación del tramo</param>
        /// <param name="ldt_FchValorAcreedor">Fecha de valor de acreedor</param>
        /// <param name="ldt_FchRecepcion">Fecha de recepción</param>
        /// <param name="ldt_FchTipoCambio">Fecha de tipo de cambio</param>
        /// <returns>Retorna un mensaje con el código y texto de transacción</returns>
        [WebMethod]
        public string EliminarAmortizacion(string lstr_IdPrestamo, string lint_IdTramo, string ldt_FchRecepcion,
            string lstr_IdMoneda, string lint_secuencia)
        {
            DataTable ldat_Amortizacion = new DataTable();
            DateTime ldt_FchModifica = new DateTime();

            DateTime FchRecepcion = new DateTime();

            string lstr_Estado = "";
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";
            try
                {
                    if (!string.IsNullOrEmpty(ldt_FchRecepcion) && ldt_FchRecepcion != "01/01/1900")
                        FchRecepcion = DateTime.ParseExact(ldt_FchRecepcion, lstr_formato_fecha, CultureInfo.InvariantCulture);
                }
                catch(Exception ex)
                {
                    mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
                }

            if (mensaje == "")
            {

                ldat_Amortizacion = ConsultarAmortizacion(lstr_IdPrestamo, lint_IdTramo, null, null, ldt_FchRecepcion, lstr_IdMoneda, lint_secuencia).Tables[0];

                try
                {
                    lstr_Estado = ldat_Amortizacion.Rows[0]["Estado"].ToString();
                    ldt_FchModifica = Convert.ToDateTime(ldat_Amortizacion.Rows[0]["FchModifica"].ToString());
                    clsAmortizacion lcls_Amortizacion = new clsAmortizacion();

                    //DateTime FchValorAcreedor = DateTime.ParseExact(ldt_FchValorAcreedor, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    //DateTime FchRecepcion = DateTime.ParseExact(ldt_FchRecepcion, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    //DateTime FchTipoCambio = DateTime.ParseExact(ldt_FchTipoCambio, lstr_formato_fecha, CultureInfo.InvariantCulture);

                    lcls_Amortizacion.CambiarEstadoAmortizacion(lstr_IdPrestamo, Convert.ToInt32(lint_IdTramo),
                        FchRecepcion, lstr_IdMoneda, Convert.ToInt32(lint_secuencia), lstr_Estado, lstr_UsrModifica, ldt_FchModifica, out codSalida, out txtSalida);
                    codSalida = ((codSalida == "00")||(codSalida == "01")) ? codSalida : "99"; mensaje = "Código " + codSalida + ": " + txtSalida;
                }
                catch (Exception e)
                {
                    mensaje = "Código 99: " + e.ToString();
                }
            }
            return mensaje;
        }

        /// <summary>
        /// Consulta una amortización del sistema gestor
        /// </summary>
        /// <param name="lint_opcion">Tipo de operación a realizar: 
        /// -1 Consulta por Fecha valor de acreedor
        /// -2 Consulta por Fecha de tipo de cambio
        /// -3 Consulta por Fecha de recepción
        /// -4 Consulta por Id de Préstamo y Id de Tramo</param>
        /// <param name="lstr_IdPrestamo">Identificación del préstamo</param>
        /// <param name="lint_id_tramo">Identificación del tramo</param>
        /// <param name="lstr_valor1">Valor para generar la consulta</param>
        /// <returns>Retorna un mensaje con el código y texto de transacción</returns>
        [WebMethod]
        public DataSet ConsultarAmortizacion(string lstr_IdPrestamo, string lint_id_tramo, string ldt_FchValorAcreedor, string ldt_FchTipoCambio, string ldt_FchRecepcion, string lstr_IdMoneda, string lint_Secuencia)
        {
            DataSet ldas_Amortizacion = new DataSet();
            string mensaje = "";

            DateTime? FchValorAcreedor = null;
            DateTime? FchTipoCambio = null;
            DateTime? FchRecepcion = null;
            int? id_tramo = null;
            int? secuencia = null;
            try
            {
                try
                {
                    if (!string.IsNullOrEmpty(ldt_FchValorAcreedor))
                        FchValorAcreedor = DateTime.ParseExact(ldt_FchValorAcreedor, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(ldt_FchTipoCambio))
                        FchTipoCambio = DateTime.ParseExact(ldt_FchTipoCambio, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(ldt_FchRecepcion))
                        FchRecepcion = DateTime.ParseExact(ldt_FchRecepcion, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    //DateTime FchValorAcreedor = DateTime.ParseExact(ldt_FchValorAcreedor, lstr_formato_fecha, CultureInfo.InvariantCulture);
                }
                catch(Exception ex)
                {
                    mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
                }
                try
                {
                    if (!string.IsNullOrEmpty(lint_id_tramo))
                        id_tramo = Convert.ToInt32 (lint_id_tramo);
                    if (!string.IsNullOrEmpty(lint_Secuencia))
                        secuencia = Convert.ToInt32(lint_Secuencia);
                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: Formato incorrecto de campo ";
                }
                if (mensaje == "")
                {
                    clsAmortizacion lcls_Amortizacion = new clsAmortizacion();
                    ldas_Amortizacion = lcls_Amortizacion.ConsultarAmortizacion(lstr_IdPrestamo, id_tramo, FchValorAcreedor, FchTipoCambio, FchRecepcion, lstr_IdMoneda, secuencia);
                }
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return ldas_Amortizacion;
        }

        #endregion

        #region Asiento Ajuste


        [WebMethod]
        public string CrearAsientoAjuste(string lstr_IdAsiento, string lstr_UsrCreacion, string lstr_IdCuenta, string lstr_NombreCuenta,
            string lstr_ClaveContable, decimal ldec_MontoContable, decimal ldec_MontoDebe, decimal ldec_MontoHaber, string lstr_Moneda)
        {
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";

            try
            {

                //ldec_MontoContable = ldec_MontoContable.Replace(",", lstr_separador_decimal);
                //ldec_MontoContable = ldec_MontoContable.Replace(".", lstr_separador_decimal);
                //ldec_MontoDebe = ldec_MontoDebe.Replace(",", lstr_separador_decimal);
                //ldec_MontoDebe = ldec_MontoDebe.Replace(".", lstr_separador_decimal);
                //ldec_MontoHaber = ldec_MontoHaber.Replace(",", lstr_separador_decimal);
                //ldec_MontoHaber = ldec_MontoHaber.Replace(".", lstr_separador_decimal);
                clsAsientoAjuste lcls_AsientoAjuste = new clsAsientoAjuste();
                lcls_AsientoAjuste.CrearAsiento(lstr_IdAsiento, lstr_UsrCreacion, lstr_IdCuenta, lstr_NombreCuenta, lstr_ClaveContable,
                    ldec_MontoContable, ldec_MontoDebe, ldec_MontoHaber, lstr_Moneda, out codSalida, out txtSalida);

                codSalida = ((codSalida == "00")||(codSalida == "01")) ? codSalida : "99"; mensaje = "Código " + codSalida + ": " + txtSalida;
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return mensaje;
        }


        [WebMethod]
        public DataSet ConsultarAsientoAjuste(string lstr_valor1)
        {
            DataSet ldas_AsientoAjuste = new DataSet();
            string mensaje = "";

            try
            {
                clsAsientoAjuste lcls_AsientoAjuste = new clsAsientoAjuste();
                ldas_AsientoAjuste = lcls_AsientoAjuste.ConsultarAsiento(lstr_valor1);
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return ldas_AsientoAjuste;
        }

        #endregion

        #region Comisiones

        /// <summary>
        /// Crea una comisión en el sistema gestor
        /// </summary>
        /// <param name="lstr_IdPrestamo">Identificación del préstamo</param>
        /// <param name="lint_IdTramo">Identificación del tramo</param>
        /// <param name="lint_IdComision">Identificación de la comisión</param>
        /// <param name="lstr_TipoComision">Tipo de comisión</param>
        /// <param name="ldt_FchEfectivoAPartir">Fecha efectivo a partir de</param>
        /// <param name="ldt_FchHasta">Fecha hasta</param>
        /// <param name="lstr_MonedaPago">Moneda del pago de comisión</param>
        /// <param name="ldec_Porcentaje">Porcentaje de la comisión</param>
        /// <param name="ldec_MontoPago">Monto del pago de comisión</param>
        /// <param name="lstr_MetodoPago">Método de pago de comisión</param>
        /// <param name="ldt_FchPrimerPago">Fecha de primer pago de comisión</param>
        /// <param name="ldt_FchUltimoPago">Fecha de último pago de comisión</param>
        /// <param name="lstr_Periodo">Periodo de comision</param>
        /// <param name="lstr_Anno">Año de comisión</param>
        /// <param name="lstr_Mes">Mes de comisión</param>
        /// <param name="lstr_TipoPago">Tipo de pago de la comisión</param>
        /// <param name="lstr_EsPago">Indica si corresponde a un pago o una proyección</param>
        /// <param name="ldt_FchValorAcreedor">Indica la fecha de pago de la comisión</param>
        /// <returns>Retorna un mensaje con el código y texto de la transacción</returns>
        [WebMethod]
        public string CrearComision(string lstr_IdPrestamo, string lint_IdTramo, string lint_IdComision, string lstr_TipoComision,
            string ldt_FchEfectivoAPartir, string ldt_FchHasta, string lstr_MonedaPago, string ldec_Porcentaje,
            string ldec_MontoPago, string lstr_MetodoPago, string ldt_FchPrimerPago, string ldt_FchUltimoPago,
            string lstr_Periodo, string lstr_Anno, string lstr_Mes, string lstr_TipoPago,
            //string lstr_EsPago, string ldt_FchValorAcreedor, 
            string lstr_EstadoComision)
        {
            string lstr_Estado = "ACT";
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";
            decimal? Porcentaje = null;
            DateTime? FchEfectivoAPartir = null;
                DateTime? FchHasta = null;
                DateTime? FchPrimerPago = null;
                DateTime? FchUltimoPago = null;
            try
            {
                try
                {
                    ldec_Porcentaje = ldec_Porcentaje.Replace(",", lstr_separador_decimal);
                    ldec_Porcentaje = ldec_Porcentaje.Replace(".", lstr_separador_decimal);
                    ldec_MontoPago = ldec_MontoPago.Replace(",", lstr_separador_decimal);
                    ldec_MontoPago = ldec_MontoPago.Replace(".", lstr_separador_decimal);

                    if (!string.IsNullOrEmpty(ldt_FchEfectivoAPartir))                      
                        FchEfectivoAPartir = DateTime.ParseExact(ldt_FchEfectivoAPartir, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(ldt_FchHasta))                       
                        FchHasta = DateTime.ParseExact(ldt_FchHasta, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(ldt_FchPrimerPago))                         
                        FchPrimerPago = DateTime.ParseExact(ldt_FchPrimerPago, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(ldt_FchUltimoPago))                         
                        FchUltimoPago = DateTime.ParseExact(ldt_FchUltimoPago, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    //DateTime FchValorAcreedor = DateTime.ParseExact(ldt_FchValorAcreedor, lstr_formato_fecha, CultureInfo.InvariantCulture);
                }
                catch(Exception ex)
                {
                    mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
                }
                try
                {
                    if (!string.IsNullOrEmpty(ldec_Porcentaje))                        
                        Porcentaje = Convert.ToDecimal( ldec_Porcentaje);
                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: Formato incorrecto de campo ";
                }
                if (mensaje == "")
                {
                    clsComision lcls_Comision = new clsComision();
                    lcls_Comision.CrearComision(lstr_IdPrestamo, lint_IdTramo, Convert.ToInt32(lint_IdComision), lstr_TipoComision,
                        FchEfectivoAPartir, FchHasta, lstr_MonedaPago, Porcentaje, Convert.ToDecimal(ldec_MontoPago), lstr_MetodoPago,
                        FchPrimerPago, FchUltimoPago, lstr_Periodo, lstr_Anno, lstr_Mes, lstr_TipoPago, //lstr_EsPago, FchValorAcreedor, 
                        lstr_Estado, lstr_EstadoComision,
                        lstr_UsrCreacion, out codSalida, out txtSalida);

                    codSalida = ((codSalida == "00")||(codSalida == "01")) ? codSalida : "99"; mensaje = "Código " + codSalida + ": " + txtSalida;
                }
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return mensaje;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstr_IdPrestamo"></param>
        /// <param name="lint_IdTramo"></param>
        /// <param name="ldt_FchPago"></param>
        /// <param name="lstr_Secuencia"></param>
        /// <param name="ldec_Monto"></param>
        /// <param name="lstr_MonedaPago"></param>
        /// <param name="lstr_EstadoSigade"></param>
        /// <param name="lstr_Estado"></param>
        /// <returns></returns>
        [WebMethod]
        public string CrearComisionPago(string lstr_IdPrestamo, string lint_IdTramo, string ldt_FchPago, string lstr_Secuencia, string lstr_Consecutivo, string ldt_FchTipoCambio, 
            string ldec_Monto, string lstr_MonedaPago, string lstr_EstadoSigade, string lstr_TipoComision,string lstr_ModalEjecucion)
        {
            string lstr_Estado = "ACT";
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";
            DateTime FchPago = new DateTime();
            DateTime? FchTipoCambio = null;
            Int64? Secuencia = null;
            Int64? Consecutivo = null;
            decimal Monto = new decimal();
            try
            {               
                try
                {

                    ldec_Monto = ldec_Monto.Replace(",", lstr_separador_decimal);
                    ldec_Monto = ldec_Monto.Replace(".", lstr_separador_decimal);
                if (!string.IsNullOrEmpty(ldec_Monto))
                        Monto = Convert.ToDecimal(ldec_Monto);
                if (!string.IsNullOrEmpty(ldt_FchTipoCambio) && ldt_FchTipoCambio != "01/01/1900")
                    FchTipoCambio = Convert.ToDateTime(ldt_FchTipoCambio);
                    //if (!string.IsNullOrEmpty(ldec_PeriodoDias))
                    //    PeriodoDias = Convert.ToDecimal(ldec_PeriodoDias);
                    //if (!string.IsNullOrEmpty(ldec_Monto))
                    //    Monto = Convert.ToDecimal(ldec_Monto);
                    //if (!string.IsNullOrEmpty(ldec_DiasGracia))
                    //    DiasGracia = Convert.ToDecimal(ldec_DiasGracia);
                    //if (!string.IsNullOrEmpty(ldec_TasaPunitiva))
                    //    TasaPunitiva = Convert.ToDecimal(ldec_TasaPunitiva);
                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: Formato incorrecto de campo ";
                }
                
                try
                {
                    FchPago = DateTime.ParseExact(ldt_FchPago, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    
                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
                }

               

                if (mensaje == "")
                {
                    clsComision lcls_Comision = new clsComision();
                    lcls_Comision.CrearComisionPago(lstr_IdPrestamo, lint_IdTramo, FchPago, Convert.ToInt64(lstr_Secuencia), Convert.ToInt64(lstr_Consecutivo), FchTipoCambio,
                        Monto, lstr_MonedaPago, lstr_EstadoSigade, lstr_Estado,
                        lstr_UsrCreacion, lstr_TipoComision,lstr_ModalEjecucion, out codSalida, out txtSalida);

                    codSalida = ((codSalida == "00")||(codSalida == "01")) ? codSalida : "99"; mensaje = "Código " + codSalida + ": " + txtSalida;
                }
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return mensaje;
        }

        [WebMethod]
        public string ModificarComisionPago(
            string lstr_IdPrestamo, string lint_IdTramo, string lint_Secuencia, string lint_Consecutivo, string ldt_FchTipoCambio, string lstr_TipoComision, string lstr_ModalEjecucion,
            string lstr_MonedaPago, string ldec_Monto, string ldt_FchPago,
            string lstr_EstadoSigade, string lint_SecuenciaAnt)
        {
            string lstr_Estado = "ACT";
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";
            DateTime? FchPago = null;
            DateTime? FchTipoCambio = null;
            decimal? Monto = null;
            decimal? MontoAntes = null;
            Int64? Secuencia = null;
            Int64? Consecutivo = null;
            Int64? SecuenciaAnt = null;
            int? Tramo = null;
            DateTime ldt_FchModifica = new DateTime();
            try
            {
                try
                {

                    ldec_Monto = ldec_Monto.Replace(",", lstr_separador_decimal);
                    ldec_Monto = ldec_Monto.Replace(".", lstr_separador_decimal);
                    if (!string.IsNullOrEmpty(ldec_Monto))
                        Monto = Convert.ToDecimal(ldec_Monto);

                    if (!string.IsNullOrEmpty(lint_Secuencia))
                        Secuencia = Convert.ToInt64(lint_Secuencia);

                    if (!string.IsNullOrEmpty(lint_Consecutivo))
                        Consecutivo = Convert.ToInt64(lint_Consecutivo);
                    if (!string.IsNullOrEmpty(lint_SecuenciaAnt))
                        SecuenciaAnt = Convert.ToInt32(lint_SecuenciaAnt);
                    else
                        SecuenciaAnt = Secuencia;
                    if (!string.IsNullOrEmpty(lint_IdTramo))
                        Tramo = Convert.ToInt32 (lint_IdTramo);
                    //if (!string.IsNullOrEmpty(ldec_PeriodoDias))
                    //    PeriodoDias = Convert.ToDecimal(ldec_PeriodoDias);
                    //if (!string.IsNullOrEmpty(ldec_Monto))
                    //    Monto = Convert.ToDecimal(ldec_Monto);
                    //if (!string.IsNullOrEmpty(ldec_DiasGracia))
                    //    DiasGracia = Convert.ToDecimal(ldec_DiasGracia);
                    //if (!string.IsNullOrEmpty(ldec_TasaPunitiva))
                    //    TasaPunitiva = Convert.ToDecimal(ldec_TasaPunitiva);
                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: Formato incorrecto de campo ";
                }

                try
                {
                    FchPago = DateTime.ParseExact(ldt_FchPago, lstr_formato_fecha, CultureInfo.InvariantCulture);

                    if (!string.IsNullOrEmpty(ldt_FchTipoCambio) && ldt_FchTipoCambio != "01/01/1900")
                    FchTipoCambio = DateTime.ParseExact(ldt_FchTipoCambio, lstr_formato_fecha, CultureInfo.InvariantCulture);

                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
                }
                    if (mensaje == "")
                    {
                        try{
                            DataSet ds_ComisionesPagos = ConsultarComisionPago(lstr_IdPrestamo, lint_IdTramo, ldt_FchPago, lint_Secuencia, lint_Consecutivo);
                            ldt_FchModifica = Convert.ToDateTime(ds_ComisionesPagos.Tables[0].Rows[0]["FchModifica"].ToString());
                            MontoAntes = Convert.ToDecimal(ds_ComisionesPagos.Tables[0].Rows[0]["Monto"].ToString());
                        }
                        catch (Exception ex){
                            mensaje = "Código 99: Error al obtener registro por modificar " + ex.ToString();
                        }

                        if (mensaje == "")
                        {
                            clsComision lcls_Comision = new clsComision();
                            lcls_Comision.ModificarComisionPago(
                                lstr_IdPrestamo, Tramo, Secuencia, Convert.ToInt64(Consecutivo), FchTipoCambio, lstr_TipoComision,lstr_ModalEjecucion,
                                lstr_MonedaPago, Monto, MontoAntes, FchPago,
                                lstr_EstadoSigade, lstr_UsrModifica, ldt_FchModifica,
                                 SecuenciaAnt, out codSalida, out txtSalida);

                            codSalida = ((codSalida == "00") || (codSalida == "01")) ? codSalida : "99"; mensaje = "Código " + codSalida + ": " + txtSalida;
                        }
                   }
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return mensaje;
        }

        /// <summary>
        /// Modifica una comisión en el sistema gestor
        /// </summary>
        /// <param name="lstr_IdPrestamo">Identificación del préstamo</param>
        /// <param name="lint_IdTramo">Identificación del tramo</param>
        /// <param name="lint_IdComision">Identificación de la comisión</param>
        /// <param name="lstr_TipoComision">Tipo de comisión</param>
        /// <param name="ldt_FchEfectivoAPartir">Fecha efectivo a partir de</param>
        /// <param name="ldt_FchHasta">Fecha hasta</param>
        /// <param name="lstr_MonedaPago">Moneda del pago de comisión</param>
        /// <param name="ldec_Porcentaje">Porcentaje de la comisión</param>
        /// <param name="ldec_MontoPago">Monto del pago de comisión</param>
        /// <param name="lstr_MetodoPago">Método de pago de comisión</param>
        /// <param name="ldt_FchPrimerPago">Fecha de primer pago de comisión</param>
        /// <param name="ldt_FchUltimoPago">Fecha de último pago de comisión</param>
        /// <param name="lstr_Periodo">Periodo de comision</param>
        /// <param name="lstr_Anno">Año de comisión</param>
        /// <param name="lstr_Mes">Mes de comisión</param>
        /// <param name="lstr_TipoPago">Tipo de pago de la comisión</param>
        /// <returns>Retorna un mensaje con el código y texto de la transacción</returns>
        [WebMethod]
        public string ModificarComision(string lstr_IdPrestamo, string lint_IdTramo, string lint_IdComision, string lstr_TipoComision,
            string ldt_FchEfectivoAPartir, string ldt_FchHasta, string lstr_MonedaPago, string ldec_Porcentaje,
            string ldec_MontoPago, string lstr_MetodoPago, string ldt_FchPrimerPago, string ldt_FchUltimoPago,
            string lstr_Periodo, string lstr_Anno, string lstr_Mes, string lstr_TipoPago, string lstr_EstadoComision)
        {
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";
            decimal? Porcentaje = null;
            decimal? MontoPago = null;
            DateTime? FchEfectivoAPartir = null;
                DateTime? FchHasta = null;
                DateTime? FchPrimerPago = null;
                DateTime? FchUltimoPago = null;
                DateTime ldt_FchModifica = new DateTime();
                try
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(ldt_FchEfectivoAPartir))
                            FchEfectivoAPartir = DateTime.ParseExact(ldt_FchEfectivoAPartir, lstr_formato_fecha, CultureInfo.InvariantCulture);
                        if (!string.IsNullOrEmpty(ldt_FchHasta))
                            FchHasta = DateTime.ParseExact(ldt_FchHasta, lstr_formato_fecha, CultureInfo.InvariantCulture);
                        if (!string.IsNullOrEmpty(ldt_FchPrimerPago))
                            FchPrimerPago = DateTime.ParseExact(ldt_FchPrimerPago, lstr_formato_fecha, CultureInfo.InvariantCulture);
                        if (!string.IsNullOrEmpty(ldt_FchUltimoPago))
                            FchUltimoPago = DateTime.ParseExact(ldt_FchUltimoPago, lstr_formato_fecha, CultureInfo.InvariantCulture);
                        //DateTime FchValorAcreedor = DateTime.ParseExact(ldt_FchValorAcreedor, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    }
                    catch (Exception ex)
                    {
                        mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
                    }
                    try
                    {
                        ldec_MontoPago = ldec_MontoPago.Replace(",", lstr_separador_decimal);
                        ldec_MontoPago = ldec_MontoPago.Replace(".", lstr_separador_decimal);
                        ldec_Porcentaje = ldec_Porcentaje.Replace(",", lstr_separador_decimal);
                        ldec_Porcentaje = ldec_Porcentaje.Replace(".", lstr_separador_decimal);
                        if (!string.IsNullOrEmpty(ldec_Porcentaje))
                            Porcentaje = Convert.ToDecimal(ldec_Porcentaje);

                        if (!string.IsNullOrEmpty(ldec_MontoPago))
                            MontoPago = Convert.ToDecimal(ldec_MontoPago);
                    }
                    catch (Exception ex)
                    {
                        mensaje = "Código 99: Formato incorrecto de campo ";
                    }
                    if (mensaje == "")
                    {
                        try{
                            ldt_FchModifica = Convert.ToDateTime(ConsultarComision(lstr_IdPrestamo, "", lint_IdComision, "", "", "", "", "", "", "", "", "").Tables[0].Rows[0]["FchModifica"].ToString());
                        }
                        catch (Exception ex){
                            mensaje = "Código 99: Error al obtener registro por modificar " + ex.ToString();
                        }

                        if (mensaje == "")
                        {
                            clsComision lcls_Comision = new clsComision();
                            lcls_Comision.ModificarComision(lstr_IdPrestamo, lint_IdTramo, Convert.ToInt32(lint_IdComision), lstr_TipoComision,
                                                                             FchEfectivoAPartir, FchHasta, lstr_MonedaPago, Porcentaje,
                                                                             MontoPago, lstr_MetodoPago, FchPrimerPago, FchUltimoPago,
                                                                             lstr_Periodo, lstr_Anno, lstr_Mes, lstr_TipoPago, lstr_EstadoComision, lstr_UsrModifica, ldt_FchModifica, out codSalida, out txtSalida);

                            codSalida = ((codSalida == "00") || (codSalida == "01")) ? codSalida : "99"; mensaje = "Código " + codSalida + ": " + txtSalida;
                        }
                    }
                }
                catch (Exception e)
                {
                    mensaje = "Código 99: " + e.ToString();
                }

            return mensaje;
        }

        /// <summary>
        /// Consulta una comisión del sistema gestor
        /// </summary>
        /// <param name="lint_opcion">Tipo de operación a realizar: 
        /// 1- Consultar por tipo de pago
        /// 2- Consultar por Id de Comisión
        /// 3- Consultar por porcentaje
        /// 4- Consultar por periodo
        /// 5- Consultar por rango de fechas
        /// 6- Consultar por año y mes
        /// 7- Consultar por Id de Préstamo y Id de Tramo</param>
        /// <param name="lstr_IdPrestamo">Identificación de préstamo</param>
        /// <param name="lint_IdTramo">Identificación de tramo</param>
        /// <param name="lstr_valor1">Valor de entrada 1</param>
        /// <param name="lstr_valor2">Valor de entrada 2</param>
        /// <param name="lstr_EsPago">Indica si corresponde a un pago o una proyección</param>
        /// <param name="ldt_FchValorAcreedor">Indica la fecha de pago de la comisión</param>
        /// <returns>Retorna un dataset con el resultado de la consulta</returns>
        [WebMethod]
        public DataSet ConsultarComision(string lstr_IdPrestamo, string lint_IdTramo, string lstr_IdComsion, string lstr_TipoComision,
            string ldt_FchDesde, string ldt_FchHasta, string lstr_MonedaPago, string lstr_Porcentaje, string lstr_Periodo, string lstr_Anno,
            string lstr_Mes, string lstr_TpoPago)
        {
            DataSet ldas_Comisiones = new DataSet();
            string mensaje = "";


            decimal? Porcentaje = null;
            DateTime? FchDesde = null;
                DateTime? FchHasta = null;
            try
            {
                try
                {
                    if (!string.IsNullOrEmpty(ldt_FchDesde))
                        FchDesde = DateTime.ParseExact(ldt_FchDesde, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(ldt_FchHasta))                       
                        FchHasta = DateTime.ParseExact(ldt_FchHasta, lstr_formato_fecha, CultureInfo.InvariantCulture);
                }
                catch(Exception ex)
                {
                    mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
                }
                try
                {
                    lstr_Porcentaje = lstr_Porcentaje.Replace(",", lstr_separador_decimal);
                    lstr_Porcentaje = lstr_Porcentaje.Replace(".", lstr_separador_decimal);
                    if (!string.IsNullOrEmpty(lstr_Porcentaje))
                        Porcentaje = Convert.ToDecimal(lstr_Porcentaje);
                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: Formato incorrecto de campo ";
                }
                if (mensaje == "")
                {
                    clsComision lcls_Comision = new clsComision();
                    ldas_Comisiones = lcls_Comision.ConsultarComision(lstr_IdPrestamo, lint_IdTramo, lstr_TpoPago, lstr_TipoComision, lstr_MonedaPago, lstr_IdComsion, Porcentaje, lstr_Periodo, FchDesde, FchHasta, lstr_Anno, lstr_Mes);
                }
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return ldas_Comisiones;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstr_IdPrestamo"></param>
        /// <param name="lint_IdTramo"></param>
        /// <param name="ldt_FchPago"></param>
        /// <param name="lstr_Secuencia"></param>
        /// <returns></returns>
        [WebMethod]
        public DataSet ConsultarComisionPago(string lstr_IdPrestamo, string lint_IdTramo, string ldt_FchPago, string lstr_Secuencia, string lstr_Consecutivo)
        {
            DataSet ldas_Comisiones = new DataSet();
            string mensaje = "";
            DateTime? FchPago = null;
            Int64? Secuencia = null;
            Int64? Consecutivo = null;
            try
            {
                try
                {
                    if (!string.IsNullOrEmpty(ldt_FchPago))
                        FchPago = DateTime.ParseExact(ldt_FchPago, lstr_formato_fecha, CultureInfo.InvariantCulture);
                }
                catch(Exception ex)
                {
                    mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
                }

                try
                {
                    if (!string.IsNullOrEmpty(lstr_Consecutivo ))
                        Consecutivo = Convert.ToInt64(lstr_Consecutivo) ;
                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: Formato incorrecto de campo ";
                }


                try
                {
                    if (!string.IsNullOrEmpty(lstr_Secuencia))
                        Secuencia = Convert.ToInt64(lstr_Secuencia);
                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: Formato incorrecto de campo ";
                }


                if (mensaje == "")
                {
                    clsComision lcls_Comision = new clsComision();
                    ldas_Comisiones = lcls_Comision.ConsultarComisionPago(lstr_IdPrestamo, lint_IdTramo, FchPago, Secuencia, Consecutivo);
                }
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return ldas_Comisiones;
        }

        [WebMethod]
        public string EliminarComision(string lstr_IdPrestamo, string lint_IdTramo, string lstr_IdComsion)
        {
            DataTable ldat_Comision = new DataTable();
            DateTime ldt_FchModifica = new DateTime();

            string lstr_Estado = "";
            string txtSalida = "";
            string codSalida = "";
            string mensaje = ""; 
            bool bool_ResCreacion = false;

            ldat_Comision = ConsultarComision(lstr_IdPrestamo, lint_IdTramo, lstr_IdComsion, "",
            "", "", "", "", "", "",
            "", "").Tables[0];

            try
            {
                lstr_Estado = ldat_Comision.Rows[0]["Estado"].ToString();
                ldt_FchModifica = Convert.ToDateTime(ldat_Comision.Rows[0]["FchModifica"].ToString());
                clsComision lcls_Comision = new clsComision();

                //DateTime FchEstimada = DateTime.ParseExact(ldt_FchEstimada, lstr_formato_fecha, CultureInfo.InvariantCulture);

                bool_ResCreacion = lcls_Comision.CambiarEstadoComision(lstr_IdPrestamo, Convert.ToInt32(lint_IdTramo), Convert.ToInt32(lstr_IdComsion), lstr_Estado, lstr_UsrModifica, ldt_FchModifica, out codSalida, out txtSalida);
                codSalida = ((codSalida == "00") || (codSalida == "01")) ? codSalida : "99"; mensaje = "Código " + codSalida + ": " + txtSalida;
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }
            return mensaje;
        }

        #endregion

        #region Giros ######

        /// <summary>
        /// Crea un desembolso en el sistema gestor
        /// </summary>
        /// <param name="lstr_IdPrestamo">Identificación del préstamo</param>
        /// <param name="lint_IdTramo">Identificación del tramo</param>
        /// <param name="ldec_Monto">Monto del desembolso</param>
        /// <param name="lstr_Moneda">Moneda del desembolso</param>
        /// <param name="ldt_FchDesembolso">Fecha del desembolso</param>
        /// <param name="lstr_TipoDesembolso">Tipo de desembolso</param>
        /// <param name="lstr_Descripcion">Descripción del desembolso</param>
        /// <returns>Retorna un mensaje con el código y texto de la transacción</returns>
        [WebMethod]
        public string CrearGiro(string lstr_IdPrestamo, string lint_IdTramo, string ldec_Monto, string lstr_Moneda,
            string ldt_FchDesembolso, string ldt_FchEstimada, string lstr_TipoDesembolso, string lstr_Descripcion, string lint_secuencia)
        {
            string lstr_Estado = "ACT";
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";
            DateTime FchDesembolso = new DateTime();
            DateTime FchEstimada = new DateTime();
            try
            {
                try
                {
                    FchDesembolso = DateTime.ParseExact(ldt_FchDesembolso, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    FchEstimada = DateTime.ParseExact(ldt_FchEstimada, lstr_formato_fecha, CultureInfo.InvariantCulture);
                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
                }
                if (mensaje == "")
                {
                    ldec_Monto = ldec_Monto.Replace(",", lstr_separador_decimal);
                    ldec_Monto = ldec_Monto.Replace(".", lstr_separador_decimal);
                    clsDesembolso lcls_Desembolso = new clsDesembolso();
                    lcls_Desembolso.CrearDesembolso(lstr_IdPrestamo, Convert.ToInt32(lint_IdTramo), Convert.ToDecimal(ldec_Monto), lstr_Moneda,
                        FchDesembolso, FchEstimada, lstr_TipoDesembolso, lstr_Descripcion, Convert.ToInt32(lint_secuencia), lstr_Estado, lstr_UsrCreacion, out codSalida, out txtSalida);

                    codSalida = ((codSalida == "00")||(codSalida == "01")) ? codSalida : "99"; mensaje = "Código " + codSalida + ": " + txtSalida;
                }
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return mensaje;
        }


        [WebMethod]
        public string CrearFlujoEfectivoMensualDE(string lstr_IdPrestamo, string lint_IdTramo, string ldt_FechaHasta)
        {
            string mensaje = "";
            DateTime FchHasta = new DateTime();
            try
            {
                try
                {
                    FchHasta = DateTime.ParseExact(ldt_FechaHasta, lstr_formato_fecha, CultureInfo.InvariantCulture);
                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
                }
                if (mensaje == "")
                {
                    clsCalculosDeudaExterna lcls_DeudaExterna = new clsCalculosDeudaExterna();
                    mensaje = lcls_DeudaExterna.CalculoPeriodosMensuales(lstr_IdPrestamo, Convert.ToInt32(lint_IdTramo), FchHasta);
                }
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return mensaje;
        }

        [WebMethod]
        public string ModificarGiro(string lstr_IdPrestamo, string lint_IdTramo, string ldt_FchDesembolso, string ldt_FchEstimada, string lstr_Moneda, string lint_Secuencia, string ldec_Monto)
        {
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";
            DateTime ldt_FchModifica = new DateTime();
            int IdTramo = new int();
            int Secuencia = new int();
            decimal Monto = new decimal();
            DateTime FchDesembolso = new DateTime();
            DateTime FchEstimada = new DateTime();
            try
            {
                try
                {
                    if (!string.IsNullOrEmpty(ldt_FchDesembolso))
                        FchDesembolso = DateTime.ParseExact(ldt_FchDesembolso, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(ldt_FchEstimada))
                        FchEstimada = DateTime.ParseExact(ldt_FchEstimada, lstr_formato_fecha, CultureInfo.InvariantCulture);
                }
                catch(Exception ex)
                {
                    mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
                }
                try
                {
                    if (!string.IsNullOrEmpty(lint_IdTramo))
                        IdTramo = Convert.ToInt32(lint_IdTramo);
                    if (!string.IsNullOrEmpty(lint_Secuencia))
                        Secuencia = Convert.ToInt32(lint_Secuencia);

                    ldec_Monto = ldec_Monto.Replace(",", lstr_separador_decimal);
                    ldec_Monto = ldec_Monto.Replace(".", lstr_separador_decimal);
                    if (!string.IsNullOrEmpty(ldec_Monto))
                        Monto = Convert.ToDecimal(ldec_Monto);
                    //if (!string.IsNullOrEmpty(ldec_PeriodoDias))
                    //    PeriodoDias = Convert.ToDecimal(ldec_PeriodoDias);
                    //if (!string.IsNullOrEmpty(ldec_Monto))
                    //    Monto = Convert.ToDecimal(ldec_Monto);
                    //if (!string.IsNullOrEmpty(ldec_DiasGracia))
                    //    DiasGracia = Convert.ToDecimal(ldec_DiasGracia);
                    //if (!string.IsNullOrEmpty(ldec_TasaPunitiva))
                    //    TasaPunitiva = Convert.ToDecimal(ldec_TasaPunitiva);
                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: Formato incorrecto de campo ";
                }
                if (mensaje == "")
                {
                    try
                    {
                        ldt_FchModifica = Convert.ToDateTime(ConsultarGiro(lstr_IdPrestamo, lint_IdTramo, null, null, lstr_Moneda, ldt_FchDesembolso, ldt_FchDesembolso, ldt_FchEstimada, ldt_FchEstimada,null, null, lint_Secuencia).Tables[0].Rows[0]["FchModifica"].ToString());
                    }
                    catch (Exception ex){
                        mensaje = "Código 99: Error al obtener registro por modificar " + ex.ToString();
                    }
                    if (mensaje == "")
                    {
                        clsDesembolso lcls_Giro = new clsDesembolso();
                        lcls_Giro.ModificarGiro(lstr_IdPrestamo, IdTramo, FchDesembolso,FchEstimada, lstr_Moneda, Secuencia, Monto, lstr_UsrModifica, ldt_FchModifica, out codSalida, out txtSalida);

                        codSalida = ((codSalida == "00")||(codSalida == "01")) ? codSalida : "99"; mensaje = "Código " + codSalida + ": " + txtSalida;
                    }
                }
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return mensaje;
        }

        [WebMethod]
        public string EliminarGiro(string lstr_IdPrestamo, string lint_IdTramo, string ldt_FchDesembolso, string ldt_FchEstimada, string lstr_Moneda, string lint_Secuencia)
        {
            DataTable ldat_GiroEstimado = new DataTable();
            DateTime ldt_FchModifica = new DateTime();

            string lstr_Estado = "";
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";

            ldat_GiroEstimado = ConsultarGiro(lstr_IdPrestamo, Convert.ToString(lint_IdTramo), null, null, lstr_Moneda, ldt_FchDesembolso, ldt_FchDesembolso, ldt_FchEstimada, ldt_FchEstimada,null, null, lint_Secuencia).Tables[0];

            try
            {
                lstr_Estado = ldat_GiroEstimado.Rows[0]["Estado"].ToString();
                ldt_FchModifica = Convert.ToDateTime(ldat_GiroEstimado.Rows[0]["FchModifica"].ToString());
                clsGiroEstimado lcls_GiroEstimado = new clsGiroEstimado();

                DateTime FchEstimada = DateTime.ParseExact(ldt_FchDesembolso, lstr_formato_fecha, CultureInfo.InvariantCulture);

                lcls_GiroEstimado.CambiaEstadoGiro(lstr_IdPrestamo, Convert.ToInt32(lint_IdTramo), ldt_FchDesembolso,ldt_FchEstimada, lstr_Moneda, Convert.ToInt32(lint_Secuencia), "INACT", lstr_UsrModifica, ldt_FchModifica, out codSalida, out txtSalida);
                codSalida = ((codSalida == "00") || (codSalida == "01")) ? codSalida : "99"; mensaje = "Código " + codSalida + ": " + txtSalida;
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }
            return mensaje;
        }


        /// <summary>
        /// Consulta un desembolso en el sistema gestor
        /// </summary>
        /// <param name="lint_opcion">Tipo de operación a realizar: 
        /// -1 Consulta por rango de montos
        /// -2 Consulta por rango de fechas
        /// -3 Consulta por tipo de desembolso</param>
        /// <param name="lstr_valor1">Valor de entrada 1</param>
        /// <param name="lstr_valor2">Valor de entrada 2</param>
        /// <returns>Retorna un dataset con el resultado de la consulta</returns>
        [WebMethod]
        public DataSet ConsultarGiro(string lstr_IdPrestamo, string lint_IdTramo, string ldec_MontoDesde, string ldec_MontoHasta, string lstr_Moneda,
            string ldt_FchDesde, string ldt_FchHasta, string ldt_FchEstimadaDesde, string ldt_FchEstimadaHasta, string lstr_TipoDesembolso, string lstr_Descripcion, string lint_Secuencia)
        {
            DataSet ldas_Desembolso = new DataSet();
            string mensaje = "";
            Int32? IdTramo = null;
            Int32? Secuencia = null;
            decimal? MontoDesde = null;
            decimal? MontoHasta = null;
            DateTime? FchDesde = null;
            DateTime? FchHasta = null;
            DateTime? FchEstimadaDesde = null;
            DateTime? FchEstimadaHasta = null;
            try
            {
                try
                {
                    if (!string.IsNullOrEmpty(ldt_FchDesde))
                        FchDesde = DateTime.ParseExact(ldt_FchDesde, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(ldt_FchHasta))                       
                        FchHasta = DateTime.ParseExact(ldt_FchHasta, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(ldt_FchEstimadaDesde))
                        FchEstimadaDesde = DateTime.ParseExact(ldt_FchEstimadaDesde, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(ldt_FchEstimadaHasta))
                        FchEstimadaHasta = DateTime.ParseExact(ldt_FchEstimadaHasta, lstr_formato_fecha, CultureInfo.InvariantCulture);
                }
                catch(Exception ex)
                {
                    mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
                }
                try
                {
                    if (!string.IsNullOrEmpty(lint_IdTramo))
                        IdTramo = Convert.ToInt32(lint_IdTramo);
                    if (!string.IsNullOrEmpty(lint_Secuencia))
                        Secuencia = Convert.ToInt32(lint_Secuencia);

                    ldec_MontoDesde = ldec_MontoDesde.Replace(",", lstr_separador_decimal);
                    ldec_MontoDesde = ldec_MontoDesde.Replace(".", lstr_separador_decimal);
                    ldec_MontoHasta = ldec_MontoHasta.Replace(",", lstr_separador_decimal);
                    ldec_MontoHasta = ldec_MontoHasta.Replace(".", lstr_separador_decimal);
                    if (!string.IsNullOrEmpty(ldec_MontoDesde))
                        MontoDesde = Convert.ToDecimal(ldec_MontoDesde);
                    if (!string.IsNullOrEmpty(ldec_MontoHasta))
                        MontoHasta = Convert.ToDecimal(ldec_MontoHasta);
                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: Formato incorrecto de campo ";
                }
                if (mensaje == "")
                {
                    clsDesembolso lcls_Desembolso = new clsDesembolso();
                    ldas_Desembolso = lcls_Desembolso.ConsultarDesembolso(lstr_IdPrestamo, IdTramo, MontoDesde, MontoHasta, lstr_Moneda, FchDesde,
                                                                          FchHasta,FchEstimadaDesde,FchEstimadaHasta, lstr_TipoDesembolso, lstr_Descripcion, Secuencia);
                }
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return ldas_Desembolso;
        }

        #endregion

        #region Giros Estimados

        /// <summary>
        /// Crea un giro estimado en el sistema gestor
        /// </summary>
        /// <param name="lstr_IdPrestamo">Identificación del préstamo</param>
        /// <param name="lint_IdTramo">Identificación del tramo</param>
        /// <param name="ldt_FchEstimada">Fecha estimada del giro</param>
        /// <param name="ldec_Monto">Monto del giro</param>
        /// <returns>Retorna un mensaje con el código y texto de la transacción</returns>
        [WebMethod]
        public string CrearGiroEstimado(string lstr_IdPrestamo, string lint_IdTramo, string ldt_FchEstimada, string ldec_Monto)
        {
            string lstr_Estado = "ACT";
            string txtSalida = "";
            string codSalida = "";
            string mensaje = ""; 
            DateTime FchEstimada = new DateTime();

            try
            {
                try
                {
                    FchEstimada = DateTime.ParseExact(ldt_FchEstimada, lstr_formato_fecha, CultureInfo.InvariantCulture);
                }
                catch(Exception ex) {
                    mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
                }
                if (mensaje == "")
                {
                    ldec_Monto = ldec_Monto.Replace(",", lstr_separador_decimal);
                    ldec_Monto = ldec_Monto.Replace(".", lstr_separador_decimal);
                    clsGiroEstimado lcls_GiroEstimado = new clsGiroEstimado();
                    lcls_GiroEstimado.CrearGiroEstimado(lstr_IdPrestamo, Convert.ToInt32(lint_IdTramo), FchEstimada, Convert.ToDecimal(ldec_Monto), lstr_Estado, lstr_UsrCreacion, out codSalida, out txtSalida);

                    codSalida = ((codSalida == "00")||(codSalida == "01")) ? codSalida : "99"; mensaje = "Código " + codSalida + ": " + txtSalida;
                }
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return mensaje;
        }

        /// <summary>
        /// Modifica un giro estimado en el sistema gestor
        /// </summary>
        /// <param name="lstr_IdPrestamo">Identificación del préstamo</param>
        /// <param name="lint_IdTramo">Identificación del tramo</param>
        /// <param name="ldt_FchEstimada">Fecha estimada del giro</param>
        /// <param name="ldec_Monto">Monto del giro</param>
        /// <returns>Retorna un mensaje con el código y texto de la transacción</returns>
        [WebMethod]
        public string ModificarGiroEstimado(string lstr_IdPrestamo, string lint_IdTramo, string ldt_FchEstimada, string ldec_Monto)
        {
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";
            DateTime ldt_FchModifica = new DateTime();
            try
            {
                DateTime FchEstimada = DateTime.ParseExact(ldt_FchEstimada, lstr_formato_fecha, CultureInfo.InvariantCulture);
                try{
                    ldt_FchModifica = Convert.ToDateTime(ConsultarGiroEstimado(lstr_IdPrestamo, Convert.ToString(lint_IdTramo), ldt_FchEstimada).Tables[0].Rows[0]["FchModifica"].ToString());
                }
                catch (Exception ex){
                    mensaje = "Código 99: Error al obtener registro por modificar " + ex.ToString();
                }
                if (mensaje == "")
                {
                    ldec_Monto = ldec_Monto.Replace(",", lstr_separador_decimal);
                    ldec_Monto = ldec_Monto.Replace(".", lstr_separador_decimal);
                    clsGiroEstimado lcls_GiroEstimado = new clsGiroEstimado();
                    lcls_GiroEstimado.ModificarGiroEstimado(lstr_IdPrestamo, Convert.ToInt32(lint_IdTramo), FchEstimada, Convert.ToDecimal(ldec_Monto), lstr_UsrModifica, ldt_FchModifica, out codSalida, out txtSalida);

                    codSalida = ((codSalida == "00")||(codSalida == "01")) ? codSalida : "99"; mensaje = "Código " + codSalida + ": " + txtSalida;
                }
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return mensaje;
        }

        /// <summary>
        /// Cambia el estado de un giro estimado en el sistema gestor
        /// </summary>
        /// <param name="lstr_IdPrestamo">Identificación del préstamo</param>
        /// <param name="lint_IdTramo">Identificación del tramo</param>
        /// <param name="ldt_FchEstimada">Fecha estimada del giro estimado</param>
        /// <returns>Retorna un mensaje con el código y texto de la transacción</returns>
        [WebMethod]
        public string EliminarGiroEstimado(string lstr_IdPrestamo, string lint_IdTramo, string ldt_FchEstimada)
        {
            DataTable ldat_GiroEstimado = new DataTable();
            DateTime ldt_FchModifica = new DateTime();

            string lstr_Estado = "";
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";

            ldat_GiroEstimado = ConsultarGiroEstimado(lstr_IdPrestamo, Convert.ToString(lint_IdTramo), ldt_FchEstimada).Tables[0];

            try
            {
                lstr_Estado = ldat_GiroEstimado.Rows[0]["Estado"].ToString();
                ldt_FchModifica = Convert.ToDateTime(ldat_GiroEstimado.Rows[0]["FchModifica"].ToString());
                clsGiroEstimado lcls_GiroEstimado = new clsGiroEstimado();

                DateTime FchEstimada = DateTime.ParseExact(ldt_FchEstimada, lstr_formato_fecha, CultureInfo.InvariantCulture);

                lcls_GiroEstimado.CambiaEstadoGiroEstimado(lstr_IdPrestamo, Convert.ToInt32(lint_IdTramo), ldt_FchEstimada, "INACT", lstr_UsrModifica, ldt_FchModifica, out codSalida, out txtSalida);
                codSalida = ((codSalida == "00")||(codSalida == "01")) ? codSalida : "99"; mensaje = "Código " + codSalida + ": " + txtSalida;
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }
            return mensaje;
        }

        /// <summary>
        /// Consulta un giro estimado en el sistema gestor
        /// </summary>
        /// <param name="lint_opcion">Tipo de operación a realizar: 
        /// -1 Consulta por Fecha estimada
        /// -2 Consulta por Tipo de préstamo</param>
        /// <param name="lstr_valor1">Valor de entrada 1</param>
        /// <returns>Retorna un dataset con el resultado de la consulta</returns>
        [WebMethod]
        public DataSet ConsultarGiroEstimado(string lstr_IdPrestamo, string lint_IdTramo, string ldt_Fecha)
        {
            DataSet ldas_GiroEstimado = new DataSet();
            string mensaje = "";
            Int32? IdTramo = null;
            DateTime? Fecha = null;
            try
            {
                try
                {
                    if (!string.IsNullOrEmpty(ldt_Fecha))
                        Fecha = DateTime.ParseExact(ldt_Fecha, lstr_formato_fecha, CultureInfo.InvariantCulture);
                }
                catch(Exception ex)
                {
                    mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
                }
                try
                {
                    if (!string.IsNullOrEmpty(lint_IdTramo))
                        IdTramo = Convert.ToInt32(lint_IdTramo);

                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: Formato incorrecto de campo ";
                }
                if (mensaje == "")
                {
                    clsGiroEstimado lcls_GiroEstimado = new clsGiroEstimado();
                    ldas_GiroEstimado = lcls_GiroEstimado.ConsultarGiroEstimado(lstr_IdPrestamo, IdTramo, Fecha);
                }
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return ldas_GiroEstimado;
        }

        #endregion

        #region Intereses


        [WebMethod]
        public string CrearIntereses(string lstr_IdPrestamo, string lint_IdTramo, string lstr_Tasa, string ldt_FchTasaAPartir, string ldec_TasaMargen, string ldec_Anno,
            string ldec_Mes, string lstr_FactorConversion, string ldt_FchPagoAPartir, string ldt_FchPagoHasta, string ldec_Periodo, string ldec_PeriodoDias,
            string ldec_Monto, string ldec_DiasGracia, string ldec_TasaPunitiva, string lint_Secuencia)
        {
            string lstr_Estado = "ACT";
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";

            int? IdTramo = null;
            int? Secuencia = null;
            decimal? TasaMargen = null;
            decimal? PeriodoDias = null;
            decimal? Monto = null;
            decimal? DiasGracia = null;
            decimal? TasaPunitiva = null;

            DateTime? FchPagoAPartir = null;
            DateTime? FchTasaAPartir = null;
            DateTime? FchPagoHasta = null;
            try
            {
                try
                {
                    if (!string.IsNullOrEmpty(ldt_FchPagoAPartir))
                        FchPagoAPartir = DateTime.ParseExact(ldt_FchPagoAPartir, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(ldt_FchTasaAPartir))
                        FchTasaAPartir = DateTime.ParseExact(ldt_FchTasaAPartir, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(ldt_FchPagoHasta))
                        FchPagoHasta = DateTime.ParseExact(ldt_FchPagoHasta, lstr_formato_fecha, CultureInfo.InvariantCulture);
                }
                catch(Exception ex)
                {
                    mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
                }
                try
                {
                    if (!string.IsNullOrEmpty(lint_IdTramo))
                        IdTramo = Convert.ToInt32(lint_IdTramo);
                    if (!string.IsNullOrEmpty(lint_Secuencia))
                        Secuencia = Convert.ToInt32(lint_Secuencia);

                    ldec_TasaMargen = ldec_TasaMargen.Replace(",", lstr_separador_decimal);
                    ldec_TasaMargen = ldec_TasaMargen.Replace(".", lstr_separador_decimal);
                    ldec_PeriodoDias = ldec_PeriodoDias.Replace(",", lstr_separador_decimal);
                    ldec_PeriodoDias = ldec_PeriodoDias.Replace(".", lstr_separador_decimal);
                    ldec_Monto = ldec_Monto.Replace(",", lstr_separador_decimal);
                    ldec_Monto = ldec_Monto.Replace(".", lstr_separador_decimal);
                    ldec_DiasGracia = ldec_DiasGracia.Replace(",", lstr_separador_decimal);
                    ldec_DiasGracia = ldec_DiasGracia.Replace(".", lstr_separador_decimal);
                    ldec_TasaPunitiva = ldec_TasaPunitiva.Replace(",", lstr_separador_decimal);
                    ldec_TasaPunitiva = ldec_TasaPunitiva.Replace(".", lstr_separador_decimal);
                    if (!string.IsNullOrEmpty(ldec_TasaMargen))
                        TasaMargen = Convert.ToDecimal(ldec_TasaMargen);
                    if (!string.IsNullOrEmpty(ldec_PeriodoDias))
                        PeriodoDias = Convert.ToDecimal(ldec_PeriodoDias);
                    if (!string.IsNullOrEmpty(ldec_Monto))
                        Monto = Convert.ToDecimal(ldec_Monto);
                    if (!string.IsNullOrEmpty(ldec_DiasGracia))
                        DiasGracia = Convert.ToDecimal(ldec_DiasGracia);
                    if (!string.IsNullOrEmpty(ldec_TasaPunitiva))
                        TasaPunitiva = Convert.ToDecimal(ldec_TasaPunitiva);
                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: Formato incorrecto de campo ";
                }
                if (mensaje == "")
                {

                    clsInteres lcls_Intereses = new clsInteres();
                    lcls_Intereses.CrearInteres(lstr_IdPrestamo, IdTramo, lstr_Tasa, FchTasaAPartir, TasaMargen, ldec_Anno,
                        ldec_Mes, lstr_FactorConversion, FchPagoAPartir, FchPagoHasta, ldec_Periodo, PeriodoDias,
                        Monto, DiasGracia, TasaPunitiva, Secuencia,// lstr_EsPago, FchValorAcreedor, lstr_MonedaPago,
                        lstr_Estado, lstr_UsrCreacion, out codSalida, out txtSalida);

                    codSalida = ((codSalida == "00")||(codSalida == "01")) ? codSalida : "99"; mensaje = "Código " + codSalida + ": " + txtSalida;
                }
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return mensaje;
        }


        [WebMethod]
        public string ModificarIntereses(string lstr_IdPrestamo, string lint_IdTramo, string lstr_Tasa, string ldt_FchTasaAPartir, string ldec_TasaMargen, string ldec_Anno,
            string ldec_Mes, string lstr_FactorConversion, string ldt_FchPagoAPartir, string ldt_FchPagoHasta, string ldec_Periodo, string ldec_PeriodoDias,
            string ldec_Monto, string ldec_DiasGracia, string ldec_TasaPunitiva, string lint_Secuencia//, string lstr_EsPago, string ldt_FchValorAcreedor, string lstr_MonedaPago
            )
        {
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";
            DateTime ldt_FchModifica = new DateTime();
            decimal? DiasGracia = null;
            decimal? PeriodoDias = null;
            decimal? TasaMargen = null;

            decimal? TasaPunitiva = null;
            DateTime? FchTasaAPartir = null;
            DateTime? FchPagoAPartir = null;
            DateTime? FchPagoHasta = null;
            try
            {
                try
                {
                    if (!string.IsNullOrEmpty(ldt_FchTasaAPartir))                      
                        FchTasaAPartir = DateTime.ParseExact(ldt_FchTasaAPartir, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(ldt_FchPagoAPartir))                       
                        FchPagoAPartir = DateTime.ParseExact(ldt_FchPagoAPartir, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(ldt_FchPagoHasta))                         
                        FchPagoHasta = DateTime.ParseExact(ldt_FchPagoHasta, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    
                }
                catch(Exception ex)
                {
                    mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
                }
                try
                {
                    ldec_TasaMargen = ldec_TasaMargen.Replace(",", lstr_separador_decimal);
                    ldec_TasaMargen = ldec_TasaMargen.Replace(".", lstr_separador_decimal);
                    ldec_PeriodoDias = ldec_PeriodoDias.Replace(",", lstr_separador_decimal);
                    ldec_PeriodoDias = ldec_PeriodoDias.Replace(".", lstr_separador_decimal);
                    ldec_Monto = ldec_Monto.Replace(",", lstr_separador_decimal);
                    ldec_Monto = ldec_Monto.Replace(".", lstr_separador_decimal);
                    ldec_DiasGracia = ldec_DiasGracia.Replace(",", lstr_separador_decimal);
                    ldec_DiasGracia = ldec_DiasGracia.Replace(".", lstr_separador_decimal);
                    ldec_TasaPunitiva = ldec_TasaPunitiva.Replace(",", lstr_separador_decimal);
                    ldec_TasaPunitiva = ldec_TasaPunitiva.Replace(".", lstr_separador_decimal);
                    if (!string.IsNullOrEmpty(ldec_TasaMargen))
                        TasaMargen = Convert.ToDecimal(ldec_TasaMargen);
                    if (!string.IsNullOrEmpty(ldec_PeriodoDias))
                        PeriodoDias = Convert.ToDecimal(ldec_PeriodoDias);

                    if (!string.IsNullOrEmpty(ldec_DiasGracia))
                        DiasGracia = Convert.ToDecimal(ldec_DiasGracia);
                    if (!string.IsNullOrEmpty(ldec_TasaPunitiva))
                        TasaPunitiva = Convert.ToDecimal(ldec_TasaPunitiva);
                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: Formato incorrecto de campo ";
                }
                if (mensaje == "")
                {
                    try{
                      ldt_FchModifica = Convert.ToDateTime(ConsultarIntereses(lstr_IdPrestamo, lint_IdTramo.ToString(), ldt_FchPagoAPartir.ToString(), ldt_FchTasaAPartir.ToString(), lint_Secuencia.ToString()).Tables[0].Rows[0]["FchModifica"].ToString());
                    //DateTime FchValorAcreedor = DateTime.ParseExact(ldt_FchValorAcreedor, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    }
                    catch (Exception ex){
                        mensaje = "Código 99: Error al obtener registro por modificar " + ex.ToString();
                    }
                    if (mensaje == "")
                    {
                        clsInteres lcls_Intereses = new clsInteres();
                        lcls_Intereses.ModificarInteres(lstr_IdPrestamo, Convert.ToInt32(lint_IdTramo), lstr_Tasa, FchTasaAPartir, TasaMargen, ldec_Anno,
                            ldec_Mes, lstr_FactorConversion, FchPagoAPartir, FchPagoHasta, ldec_Periodo, PeriodoDias,
                            Convert.ToDecimal(ldec_Monto), DiasGracia, TasaPunitiva, Convert.ToInt32(lint_Secuencia), //lstr_EsPago, FchValorAcreedor, lstr_MonedaPago,
                            lstr_UsrModifica, ldt_FchModifica, out codSalida, out txtSalida);

                        codSalida = ((codSalida == "00")||(codSalida == "01")) ? codSalida : "99"; mensaje = "Código " + codSalida + ": " + txtSalida;
                    }
                }
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return mensaje;
        }

        
        [WebMethod]
        public string EliminarIntereses(string lstr_IdPrestamo, string lint_IdTramo, string ldt_FchPagoAPartir, string ldt_FchTasaAPartir, string lint_Secuencia)
        {
            DataTable ldat_Intereses = new DataTable();
            DateTime ldt_FchModifica = new DateTime();
            string lstr_Estado = "";
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";
            int IdTramo = new int();
            int Secuencia = new int();
            DateTime FchPagoAPartir = new DateTime();
            DateTime FchTasaAPartir = new DateTime();
            try
            {
                try
                {
                    if (!string.IsNullOrEmpty(ldt_FchPagoAPartir))
                        FchPagoAPartir = DateTime.ParseExact(ldt_FchPagoAPartir, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(ldt_FchTasaAPartir))
                        FchTasaAPartir = DateTime.ParseExact(ldt_FchTasaAPartir, lstr_formato_fecha, CultureInfo.InvariantCulture);
                }
                catch(Exception ex)
                {
                    mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
                }
                try
                {
                    if (!string.IsNullOrEmpty(lint_IdTramo))
                        IdTramo = Convert.ToInt32(lint_IdTramo);
                    if (!string.IsNullOrEmpty(lint_Secuencia))
                        Secuencia = Convert.ToInt32(lint_Secuencia);

                    //if (!string.IsNullOrEmpty(ldec_TasaMargen))
                    //    TasaMargen = Convert.ToDecimal(ldec_TasaMargen);
                    //if (!string.IsNullOrEmpty(ldec_PeriodoDias))
                    //    PeriodoDias = Convert.ToDecimal(ldec_PeriodoDias);
                    //if (!string.IsNullOrEmpty(ldec_Monto))
                    //    Monto = Convert.ToDecimal(ldec_Monto);
                    //if (!string.IsNullOrEmpty(ldec_DiasGracia))
                    //    DiasGracia = Convert.ToDecimal(ldec_DiasGracia);
                    //if (!string.IsNullOrEmpty(ldec_TasaPunitiva))
                    //    TasaPunitiva = Convert.ToDecimal(ldec_TasaPunitiva);
                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: Formato incorrecto de campo ";
                }
                if (mensaje == "")
                {


                    ldat_Intereses = ConsultarIntereses(lstr_IdPrestamo, lint_IdTramo, ldt_FchPagoAPartir, ldt_FchTasaAPartir, lint_Secuencia).Tables[0];


                    lstr_Estado = ldat_Intereses.Rows[0]["Estado"].ToString();
                    ldt_FchModifica = Convert.ToDateTime(ldat_Intereses.Rows[0]["FchModifica"].ToString());
                    clsInteres lcls_Intereses = new clsInteres();
                    lcls_Intereses.CambiarInteres(lstr_IdPrestamo, IdTramo, FchPagoAPartir, FchTasaAPartir, Secuencia, lstr_Estado, lstr_UsrModifica, ldt_FchModifica, out codSalida, out txtSalida);
                    codSalida = ((codSalida == "00")||(codSalida == "01")) ? codSalida : "99"; mensaje = "Código " + codSalida + ": " + txtSalida;
                }
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }
            return mensaje;
        }

        #endregion

        #region InteresesPagos
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstr_IdPrestamo"></param>
        /// <param name="lint_IdTramo"></param>
        /// <returns></returns>
        [WebMethod]
        public DataSet ConsultarInteresesPagos(string lstr_IdPrestamo, string lint_IdTramo, string ldt_FchValorAcreedor, string lint_Secuencia)
        {
            DataSet ldas_Intereses = new DataSet();
            string mensaje = "";
            int? IdTramo = null;
            int? Secuencia = null;
            DateTime? FchValorAcreedor = null;
            try
            {
                try
                {
                    if (!string.IsNullOrEmpty(ldt_FchValorAcreedor))
                        FchValorAcreedor = DateTime.ParseExact(ldt_FchValorAcreedor, lstr_formato_fecha, CultureInfo.InvariantCulture);
                }
                catch(Exception ex)
                {
                    mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
                }
                try
                {
                    if (!string.IsNullOrEmpty(lint_IdTramo))
                        IdTramo = Convert.ToInt32(lint_IdTramo);
                    if (!string.IsNullOrEmpty(lint_Secuencia))
                        Secuencia = Convert.ToInt32(lint_Secuencia);

                    //if (!string.IsNullOrEmpty(ldec_TasaMargen))
                    //    TasaMargen = Convert.ToDecimal(ldec_TasaMargen);
                    //if (!string.IsNullOrEmpty(ldec_PeriodoDias))
                    //    PeriodoDias = Convert.ToDecimal(ldec_PeriodoDias);
                    //if (!string.IsNullOrEmpty(ldec_Monto))
                    //    Monto = Convert.ToDecimal(ldec_Monto);
                    //if (!string.IsNullOrEmpty(ldec_DiasGracia))
                    //    DiasGracia = Convert.ToDecimal(ldec_DiasGracia);
                    //if (!string.IsNullOrEmpty(ldec_TasaPunitiva))
                    //    TasaPunitiva = Convert.ToDecimal(ldec_TasaPunitiva);
                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: Formato incorrecto de campo ";
                }
                if (mensaje == "")
                {

                    clsInteres lcls_Intereses = new clsInteres();
                    ldas_Intereses = lcls_Intereses.ConsultarInteresPago(lstr_IdPrestamo, IdTramo, FchValorAcreedor, Secuencia);
                }
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return ldas_Intereses;
        }

        [WebMethod]
        public string CrearInteresesPago(string lstr_IdPrestamo, string lint_IdTramo, string lint_Secuencia, string ldt_FchValorAcreedor, string ldt_FchTipoCambio,
            string ldec_Monto, string lstr_MonedaPago, string ldec_EstadoSigade)
        {

            string lstr_Estado = "ACT";
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";
            int? IdTramo = null;
            int? Secuencia = null;
            decimal? Monto = null;
            DateTime? FchValorAcreedor = null;
            DateTime? FchTipoCambio = null;
            try
            {
                try
                {
                    if (!string.IsNullOrEmpty(ldt_FchValorAcreedor) && ldt_FchValorAcreedor != "01/01/1900")
                        FchValorAcreedor = DateTime.ParseExact(ldt_FchValorAcreedor, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(ldt_FchTipoCambio) && ldt_FchTipoCambio != "01/01/1900")
                        FchTipoCambio = DateTime.ParseExact(ldt_FchTipoCambio, lstr_formato_fecha, CultureInfo.InvariantCulture);
                }
                catch(Exception ex)
                {
                    mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
                }
                try
                {
                    if (!string.IsNullOrEmpty(lint_IdTramo))
                        IdTramo = Convert.ToInt32(lint_IdTramo);
                    if (!string.IsNullOrEmpty(lint_Secuencia))
                        Secuencia = Convert.ToInt32(lint_Secuencia);

                    ldec_Monto = ldec_Monto.Replace(",", lstr_separador_decimal);
                    ldec_Monto = ldec_Monto.Replace(".", lstr_separador_decimal);
                    if (!string.IsNullOrEmpty(ldec_Monto))
                        Monto = Convert.ToDecimal(ldec_Monto);
                    //if (!string.IsNullOrEmpty(ldec_PeriodoDias))
                    //    PeriodoDias = Convert.ToDecimal(ldec_PeriodoDias);
                    //if (!string.IsNullOrEmpty(ldec_Monto))
                    //    Monto = Convert.ToDecimal(ldec_Monto);
                    //if (!string.IsNullOrEmpty(ldec_DiasGracia))
                    //    DiasGracia = Convert.ToDecimal(ldec_DiasGracia);
                    //if (!string.IsNullOrEmpty(ldec_TasaPunitiva))
                    //    TasaPunitiva = Convert.ToDecimal(ldec_TasaPunitiva);
                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: Formato incorrecto de campo ";
                }
                if (mensaje == "")
                {

                    clsInteres lcls_Intereses = new clsInteres();
                    lcls_Intereses.CrearInteresPago(lstr_IdPrestamo, IdTramo, Secuencia, FchValorAcreedor,FchTipoCambio,
                Monto, lstr_MonedaPago, ldec_EstadoSigade, lstr_Estado, lstr_UsrCreacion, out codSalida, out txtSalida);

                    codSalida = ((codSalida == "00")||(codSalida == "01")) ? codSalida : "99"; mensaje = "Código " + codSalida + ": " + txtSalida;
                }
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return mensaje;
        }


        [WebMethod]
        public string ModificarInteresesPago(string lstr_IdPrestamo, string lint_IdTramo, string lint_Secuencia, string ldt_FchValorAcreedor, string ldt_FchTipoCambio,
            string ldec_Monto, string lstr_MonedaPago, string ldec_EstadoSigade, string lint_SecuenciaAnt
            )
        {
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";
            DateTime FchModifica = new DateTime();
            DateTime? FchValorAcreedor = null;
            DateTime? FchTipoCambio = null;

            int? IdTramo = null;
            int? Secuencia = null;
            int? SecuenciaAnt = null;
            decimal? Monto = null;
            decimal? MontoAntes = null;
            try
            {
                try
                {
                    if (!string.IsNullOrEmpty(ldt_FchValorAcreedor) && ldt_FchValorAcreedor != "01/01/1900")
                        FchValorAcreedor = DateTime.ParseExact(ldt_FchValorAcreedor, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(ldt_FchTipoCambio) && ldt_FchTipoCambio != "01/01/1900")
                        FchTipoCambio = DateTime.ParseExact(ldt_FchTipoCambio, lstr_formato_fecha, CultureInfo.InvariantCulture);
                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
                }
                try
                {
                    if (!string.IsNullOrEmpty(lint_IdTramo))
                        IdTramo = Convert.ToInt32(lint_IdTramo);
                    if (!string.IsNullOrEmpty(lint_Secuencia))
                        Secuencia = Convert.ToInt32(lint_Secuencia);
                    if (!string.IsNullOrEmpty(lint_SecuenciaAnt))
                        SecuenciaAnt = Convert.ToInt32(lint_SecuenciaAnt);
                    else
                        SecuenciaAnt = Secuencia;
                    ldec_Monto = ldec_Monto.Replace(",", lstr_separador_decimal);
                    ldec_Monto = ldec_Monto.Replace(".", lstr_separador_decimal);
                    if (!string.IsNullOrEmpty(ldec_Monto))
                        Monto = Convert.ToDecimal(ldec_Monto);
                    //if (!string.IsNullOrEmpty(ldec_PeriodoDias))
                    //    PeriodoDias = Convert.ToDecimal(ldec_PeriodoDias);
                    //if (!string.IsNullOrEmpty(ldec_Monto))
                    //    Monto = Convert.ToDecimal(ldec_Monto);
                    //if (!string.IsNullOrEmpty(ldec_DiasGracia))
                    //    DiasGracia = Convert.ToDecimal(ldec_DiasGracia);
                    //if (!string.IsNullOrEmpty(ldec_TasaPunitiva))
                    //    TasaPunitiva = Convert.ToDecimal(ldec_TasaPunitiva);
                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: Formato incorrecto de campo ";
                }
                if (mensaje == "")
                {
                     try
                    {
                      DataSet ds_InteresesPago = ConsultarInteresesPagos(lstr_IdPrestamo, Convert.ToString(lint_IdTramo),ldt_FchValorAcreedor,lint_SecuenciaAnt.ToString() );
                      FchModifica = Convert.ToDateTime(ds_InteresesPago.Tables[0].Rows[0]["FchModifica"].ToString());
                      MontoAntes = Convert.ToDecimal(ds_InteresesPago.Tables[0].Rows[0]["Monto"].ToString());
                    //DateTime FchValorAcreedor = DateTime.ParseExact(ldt_FchValorAcreedor, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    }
                    catch (Exception ex){
                        mensaje = "Código 99: Error al obtener registro por modificar " + ex.ToString();
                    }
                     if (mensaje == "")
                     {
                         clsInteres lcls_Intereses = new clsInteres();
                         lcls_Intereses.ModificarInteresPago(lstr_IdPrestamo, IdTramo, Secuencia, FchValorAcreedor,FchTipoCambio,
                             Monto, MontoAntes, lstr_MonedaPago, ldec_EstadoSigade, lstr_UsrModifica, FchModifica, SecuenciaAnt, out codSalida, out txtSalida);

                         codSalida = ((codSalida == "00")||(codSalida == "01")) ? codSalida : "99"; mensaje = "Código " + codSalida + ": " + txtSalida;
                     }
                }
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return mensaje;
        }

        #endregion

        #region InteresesPunitivos

        /// <summary>
        /// Consulta un interés en el sistema gestor
        /// </summary>
        /// <param name="lstr_IdPrestamo">Identificación del préstamo</param>
        /// <param name="lint_IdTramo">Identificación del tramo</param>
        /// <returns>Retorna un dataset con el resultado de la consulta</returns>
        [WebMethod]
        public DataSet ConsultarIntereses(string lstr_IdPrestamo, string lint_IdTramo, string ldt_FchPagoAPartir, string ldt_FchTasaAPartir, string lint_Secuencia)
        {
            DataSet ldas_Intereses = new DataSet();
            string mensaje = "";

            int? IdTramo = null;
            int? Secuencia = null;
            DateTime? FchPagoAPartir = null;
            DateTime? FchTasaAPartir = null;
            try
            {
                try
                {
                    if (!string.IsNullOrEmpty(ldt_FchPagoAPartir))
                        FchPagoAPartir = DateTime.ParseExact(ldt_FchPagoAPartir, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(ldt_FchTasaAPartir))
                        FchTasaAPartir = DateTime.ParseExact(ldt_FchTasaAPartir, lstr_formato_fecha, CultureInfo.InvariantCulture);
                }
                catch(Exception ex)
                {
                    mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
                }
                try
                {
                    if (!string.IsNullOrEmpty(lint_IdTramo))
                        IdTramo = Convert.ToInt32(lint_IdTramo);
                    if (!string.IsNullOrEmpty(lint_Secuencia))
                        Secuencia = Convert.ToInt32(lint_Secuencia);

                    //if (!string.IsNullOrEmpty(ldec_TasaMargen))
                    //    TasaMargen = Convert.ToDecimal(ldec_TasaMargen);
                    //if (!string.IsNullOrEmpty(ldec_PeriodoDias))
                    //    PeriodoDias = Convert.ToDecimal(ldec_PeriodoDias);
                    //if (!string.IsNullOrEmpty(ldec_Monto))
                    //    Monto = Convert.ToDecimal(ldec_Monto);
                    //if (!string.IsNullOrEmpty(ldec_DiasGracia))
                    //    DiasGracia = Convert.ToDecimal(ldec_DiasGracia);
                    //if (!string.IsNullOrEmpty(ldec_TasaPunitiva))
                    //    TasaPunitiva = Convert.ToDecimal(ldec_TasaPunitiva);
                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: Formato incorrecto de campo ";
                }
                if (mensaje == "")
                {

                    clsInteres lcls_Intereses = new clsInteres();
                    ldas_Intereses = lcls_Intereses.ConsultarInteres(lstr_IdPrestamo, IdTramo, FchPagoAPartir, FchTasaAPartir, Secuencia);
                }
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return ldas_Intereses;
        }

        #endregion

        #region InteresesPunitivos



        [WebMethod]
        public string CrearInteresPunitivo(string lstr_IdPrestamo, string lint_IdTramo, string ldt_FchPagoAPartir, string ldt_FchTasaAPartir, string lstr_Tasa,
            string ldec_TasaMargen, string ldec_Anno, string ldec_Mes, string lstr_FactorConversion, string ldt_FchPagoHasta, string lstr_Periodo, string ldec_PeriodoDias,
            string ldec_Monto, string ldec_DiasGracia, string ldec_TasaPunitiva)
        {
            string lstr_Estado = "ACT";
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";

            int? IdTramo = null;
            decimal? TasaMargen = null;
            decimal? PeriodoDias = null;
            decimal? Monto = null;
            decimal? DiasGracia = null;
            decimal? TasaPunitiva = null;

            DateTime? FchPagoAPartir = null;
            DateTime? FchTasaAPartir = null;
            DateTime? FchPagoHasta = null;
            try
            {
                try
                {
                    if (!string.IsNullOrEmpty(ldt_FchPagoAPartir))
                        FchPagoAPartir = DateTime.ParseExact(ldt_FchPagoAPartir, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(ldt_FchTasaAPartir))
                        FchTasaAPartir = DateTime.ParseExact(ldt_FchTasaAPartir, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(ldt_FchPagoHasta))
                        FchPagoHasta = DateTime.ParseExact(ldt_FchPagoHasta, lstr_formato_fecha, CultureInfo.InvariantCulture);
                }
                catch(Exception ex)
                {
                    mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
                }
                try
                {
                    if (!string.IsNullOrEmpty(lint_IdTramo))
                        IdTramo = Convert.ToInt32(lint_IdTramo);

                    ldec_TasaMargen = ldec_TasaMargen.Replace(",", lstr_separador_decimal);
                    ldec_TasaMargen = ldec_TasaMargen.Replace(".", lstr_separador_decimal);
                    ldec_PeriodoDias = ldec_PeriodoDias.Replace(",", lstr_separador_decimal);
                    ldec_PeriodoDias = ldec_PeriodoDias.Replace(".", lstr_separador_decimal);
                    ldec_Monto = ldec_Monto.Replace(",", lstr_separador_decimal);
                    ldec_Monto = ldec_Monto.Replace(".", lstr_separador_decimal);
                    ldec_DiasGracia = ldec_DiasGracia.Replace(",", lstr_separador_decimal);
                    ldec_DiasGracia = ldec_DiasGracia.Replace(".", lstr_separador_decimal);
                    ldec_TasaPunitiva = ldec_TasaPunitiva.Replace(",", lstr_separador_decimal);
                    ldec_TasaPunitiva = ldec_TasaPunitiva.Replace(".", lstr_separador_decimal);
                    if (!string.IsNullOrEmpty(ldec_TasaMargen))
                        TasaMargen = Convert.ToDecimal(ldec_TasaMargen);
                    if (!string.IsNullOrEmpty(ldec_PeriodoDias))
                        PeriodoDias = Convert.ToDecimal(ldec_PeriodoDias);
                    if (!string.IsNullOrEmpty(ldec_Monto))
                        Monto = Convert.ToDecimal(ldec_Monto);
                    if (!string.IsNullOrEmpty(ldec_DiasGracia))
                        DiasGracia = Convert.ToDecimal(ldec_DiasGracia);
                    if (!string.IsNullOrEmpty(ldec_TasaPunitiva))
                        TasaPunitiva = Convert.ToDecimal(ldec_TasaPunitiva);
                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: Formato incorrecto de campo ";
                }
                if (mensaje == "")
                {
                    

                    clsInteresPunitivo lcls_InteresPunitivo = new clsInteresPunitivo();
                    lcls_InteresPunitivo.CrearInteresPunitivo(lstr_IdPrestamo, IdTramo, FchPagoAPartir, FchTasaAPartir, lstr_Tasa,
                TasaMargen, ldec_Anno, ldec_Mes, lstr_FactorConversion, FchPagoHasta, lstr_Periodo, PeriodoDias,
                Monto, DiasGracia, TasaPunitiva,
                lstr_Estado, lstr_UsrCreacion, out codSalida, out txtSalida);

                    codSalida = ((codSalida == "00")||(codSalida == "01")) ? codSalida : "99"; mensaje = "Código " + codSalida + ": " + txtSalida;
                }
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return mensaje;
        }


        [WebMethod]
        public string ModificarInteresPunitivo(string lstr_IdPrestamo, string lint_IdTramo, string ldt_FchPagoAPartir, string ldt_FchTasaAPartir, string lstr_Tasa,
            string ldec_TasaMargen, string ldec_Anno, string ldec_Mes, string lstr_FactorConversion, string ldt_FchPagoHasta, string lstr_Periodo, string ldec_PeriodoDias,
            string ldec_Monto, string ldec_DiasGracia, string ldec_TasaPunitiva)
        {
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";
            DateTime ldt_FchModifica = new DateTime();
            int? IdTramo = null;
            decimal? TasaMargen = null;
            decimal? PeriodoDias = null;
            decimal? Monto = null;
            decimal? DiasGracia = null;
            decimal? TasaPunitiva = null;

            DateTime? FchPagoAPartir = null;
            DateTime? FchTasaAPartir = null;
            DateTime? FchPagoHasta = null;
            try
            {
                try
                {
                    if (!string.IsNullOrEmpty(ldt_FchPagoAPartir))
                        FchPagoAPartir = DateTime.ParseExact(ldt_FchPagoAPartir, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(ldt_FchTasaAPartir))
                        FchTasaAPartir = DateTime.ParseExact(ldt_FchTasaAPartir, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(ldt_FchPagoHasta))
                        FchPagoHasta = DateTime.ParseExact(ldt_FchPagoHasta, lstr_formato_fecha, CultureInfo.InvariantCulture);
                }
                catch(Exception ex)
                {
                    mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
                }
                try
                {
                    if (!string.IsNullOrEmpty(lint_IdTramo))
                        IdTramo = Convert.ToInt32(lint_IdTramo);

                    ldec_TasaMargen = ldec_TasaMargen.Replace(",", lstr_separador_decimal);
                    ldec_TasaMargen = ldec_TasaMargen.Replace(".", lstr_separador_decimal);
                    ldec_PeriodoDias = ldec_PeriodoDias.Replace(",", lstr_separador_decimal);
                    ldec_PeriodoDias = ldec_PeriodoDias.Replace(".", lstr_separador_decimal);
                    ldec_Monto = ldec_Monto.Replace(",", lstr_separador_decimal);
                    ldec_Monto = ldec_Monto.Replace(".", lstr_separador_decimal);
                    ldec_DiasGracia = ldec_DiasGracia.Replace(",", lstr_separador_decimal);
                    ldec_DiasGracia = ldec_DiasGracia.Replace(".", lstr_separador_decimal);
                    ldec_TasaPunitiva = ldec_TasaPunitiva.Replace(",", lstr_separador_decimal);
                    ldec_TasaPunitiva = ldec_TasaPunitiva.Replace(".", lstr_separador_decimal);
                    if (!string.IsNullOrEmpty(ldec_TasaMargen))
                        TasaMargen = Convert.ToDecimal(ldec_TasaMargen);
                    if (!string.IsNullOrEmpty(ldec_PeriodoDias))
                        PeriodoDias = Convert.ToDecimal(ldec_PeriodoDias);
                    if (!string.IsNullOrEmpty(ldec_Monto))
                        Monto = Convert.ToDecimal(ldec_Monto);
                    if (!string.IsNullOrEmpty(ldec_DiasGracia))
                        DiasGracia = Convert.ToDecimal(ldec_DiasGracia);
                    if (!string.IsNullOrEmpty(ldec_TasaPunitiva))
                        TasaPunitiva = Convert.ToDecimal(ldec_TasaPunitiva);
                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: Formato incorrecto de campo ";
                } 
                if (mensaje == "")
                {
                    try
                    {

                    ldt_FchModifica = Convert.ToDateTime(ConsultarInteresPunitivo(lstr_IdPrestamo, lint_IdTramo, ldt_FchPagoAPartir, ldt_FchTasaAPartir).Tables[0].Rows[0]["FchModifica"].ToString());
                    }
                    catch (Exception ex){
                        mensaje = "Código 99: Error al obtener registro por modificar " + ex.ToString();
                    }
                    if (mensaje == "")
                    {
                        clsInteresPunitivo lcls_InteresPunitivo = new clsInteresPunitivo();
                        lcls_InteresPunitivo.ModificarInteresPunitivo(lstr_IdPrestamo, IdTramo, FchPagoAPartir, FchTasaAPartir, lstr_Tasa,
                    TasaMargen, ldec_Anno, ldec_Mes, lstr_FactorConversion, FchPagoHasta, lstr_Periodo, PeriodoDias,
                    Monto, DiasGracia, TasaPunitiva,
                            lstr_UsrModifica, ldt_FchModifica, out codSalida, out txtSalida);

                        codSalida = ((codSalida == "00")||(codSalida == "01")) ? codSalida : "99"; mensaje = "Código " + codSalida + ": " + txtSalida;
                    }
                }
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return mensaje;
        }


        [WebMethod]
        public string EliminarInteresPunitivo(string lstr_IdPrestamo, int lint_IdTramo, string ldt_FchAPartir, string ldt_FchTasaAPartir)
        {
            DataTable ldat_InteresPunitivo = new DataTable();
            DateTime ldt_FchModifica = new DateTime();

            string lstr_Estado = "";
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";

            ldat_InteresPunitivo = ConsultarInteresPunitivo(lstr_IdPrestamo, Convert.ToString(lint_IdTramo), ldt_FchAPartir, ldt_FchTasaAPartir).Tables[0];

            try
            {
                lstr_Estado = ldat_InteresPunitivo.Rows[0]["Estado"].ToString();
                ldt_FchModifica = Convert.ToDateTime(ldat_InteresPunitivo.Rows[0]["FchModifica"].ToString());
                clsInteresPunitivo lcls_InteresPunitivo = new clsInteresPunitivo();

                DateTime FchAPartir = DateTime.ParseExact(ldt_FchAPartir, lstr_formato_fecha, CultureInfo.InvariantCulture);

                DateTime FchTasaAPartir = DateTime.ParseExact(ldt_FchTasaAPartir, lstr_formato_fecha, CultureInfo.InvariantCulture);

                lcls_InteresPunitivo.CambiarInteresPunitivo(lstr_IdPrestamo, lint_IdTramo, FchAPartir, FchTasaAPartir, lstr_Estado, lstr_UsrModifica, ldt_FchModifica, out codSalida, out txtSalida);
                codSalida = ((codSalida == "00")||(codSalida == "01")) ? codSalida : "99"; mensaje = "Código " + codSalida + ": " + txtSalida;

                //mensaje = "Estado: " + lstr_Estado + " Fecha Mod: " + ldt_FchModifica + " A partir:" + FchAPartir;
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }
            return mensaje;
        }


        [WebMethod]
        public DataSet ConsultarInteresPunitivo(string lstr_IdPrestamo, string lint_id_tramo, string ldt_FchPagoAPartir, string ldt_FchTasaAPartir)
        {
            DataSet ldas_InteresPunitivo = new DataSet();
            string mensaje = "";
            Int32? IdTramo = null;
            DateTime? FchPagoAPartir = null;

            DateTime? FchTasaAPartir = null;
            try
            {
                try
                {
                    if (!string.IsNullOrEmpty(ldt_FchPagoAPartir))
                        FchPagoAPartir = DateTime.ParseExact(ldt_FchPagoAPartir, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(ldt_FchTasaAPartir))
                        FchTasaAPartir = DateTime.ParseExact(ldt_FchTasaAPartir, lstr_formato_fecha, CultureInfo.InvariantCulture);
                }
                catch(Exception ex)
                {
                    mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
                }
                try
                {
                    if (!string.IsNullOrEmpty(lint_id_tramo))
                        IdTramo = Convert.ToInt32(lint_id_tramo);

                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: Formato incorrecto de campo ";
                }
                if (mensaje == "")
                {
                    clsInteresPunitivo lcls_InteresPunitivo = new clsInteresPunitivo();
                    ldas_InteresPunitivo = lcls_InteresPunitivo.ConsultarInteresPunitivo(lstr_IdPrestamo, IdTramo, FchPagoAPartir, FchTasaAPartir);
                }
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return ldas_InteresPunitivo;
        }

        #endregion
        
        #region InteresesPunitivosPagos



        [WebMethod]
        public string CrearInteresPunitivoPago(string lstr_IdPrestamo, string lint_IdTramo, string ldt_FchAPartir,
            string ldec_Monto, string lstr_IdMoneda, string lint_secuencia)
        {
            string lstr_Estado = "ACT";
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";

            try
            {
                DateTime FchAPartir = DateTime.ParseExact(ldt_FchAPartir, lstr_formato_fecha, CultureInfo.InvariantCulture);

                ldec_Monto = ldec_Monto.Replace(",", lstr_separador_decimal);
                ldec_Monto = ldec_Monto.Replace(".", lstr_separador_decimal);
                clsInteresPunitivo lcls_InteresPunitivo = new clsInteresPunitivo();
                lcls_InteresPunitivo.CrearInteresPunitivoPago(lstr_IdPrestamo, Convert.ToInt32(lint_IdTramo), FchAPartir,
            Convert.ToDecimal(ldec_Monto), lstr_IdMoneda, Convert.ToInt32(lint_secuencia),
            lstr_Estado, lstr_UsrCreacion, out codSalida, out txtSalida);

                codSalida = ((codSalida == "00")||(codSalida == "01")) ? codSalida : "99"; mensaje = "Código " + codSalida + ": " + txtSalida;
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return mensaje;
        }


        [WebMethod]
        public string ModificarInteresPunitivoPago(string lstr_IdPrestamo, string lint_IdTramo, string ldt_FchAPartir, string ldec_Monto, string lstr_IdMoneda,
            string lint_secuencia)
        {
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";
            DateTime ldt_FchModifica = new DateTime();
            try
            {
                DateTime FchAPartir = DateTime.ParseExact(ldt_FchAPartir, lstr_formato_fecha, CultureInfo.InvariantCulture);
                try{
                ldt_FchModifica = Convert.ToDateTime(ConsultarInteresPunitivoPago(lstr_IdPrestamo, lint_IdTramo, ldt_FchAPartir, lint_secuencia).Tables[0].Rows[0]["FchModifica"].ToString());
                }
                    catch (Exception ex){
                        mensaje = "Código 99: Error al obtener registro por modificar " + ex.ToString();
                    }
                if (mensaje == "")
                {
                    ldec_Monto = ldec_Monto.Replace(",", lstr_separador_decimal);
                    ldec_Monto = ldec_Monto.Replace(".", lstr_separador_decimal);
                    clsInteresPunitivo lcls_InteresPunitivo = new clsInteresPunitivo();
                    lcls_InteresPunitivo.ModificarInteresPunitivoPago(lstr_IdPrestamo, Convert.ToInt32(lint_IdTramo), FchAPartir,Convert.ToDecimal( ldec_Monto), lstr_IdMoneda,
                Convert.ToInt32(lint_secuencia),
                        lstr_UsrModifica, ldt_FchModifica, out codSalida, out txtSalida);

                    codSalida = ((codSalida == "00")||(codSalida == "01")) ? codSalida : "99"; mensaje = "Código " + codSalida + ": " + txtSalida;
                }
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return mensaje;
        }


        //[WebMethod]
        //public string EliminarInteresPunitivoPago(string lstr_IdPrestamo, int lint_IdTramo, string ldt_FchAPartir, int lint_secuencia)
        //{
        //    DataTable ldat_InteresPunitivo = new DataTable();
        //    DateTime ldt_FchModifica = new DateTime();

        //    string lstr_Estado = "";
        //    string txtSalida = "";
        //    string codSalida = "";
        //    string mensaje = "";

        //    ldat_InteresPunitivo = ConsultarInteresPunitivo(lstr_IdPrestamo, Convert.ToString(lint_IdTramo), ldt_FchAPartir).Tables[0];

        //    try
        //    {
        //        lstr_Estado = ldat_InteresPunitivo.Rows[0]["Estado"].ToString();
        //        ldt_FchModifica = Convert.ToDateTime(ldat_InteresPunitivo.Rows[0]["FchModifica"].ToString());
        //        clsInteresPunitivo lcls_InteresPunitivo = new clsInteresPunitivo();

        //        DateTime FchAPartir = DateTime.ParseExact(ldt_FchAPartir, lstr_formato_fecha, CultureInfo.InvariantCulture);

        //        lcls_InteresPunitivo.CambiarInteresPunitivo(lstr_IdPrestamo, lint_IdTramo, FchAPartir, lint_secuencia, lstr_Estado, lstr_UsrModifica, ldt_FchModifica, out codSalida, out txtSalida);
        //        codSalida = ((codSalida == "00")||(codSalida == "01")) ? codSalida : "99"; mensaje = "Código " + codSalida + ": " + txtSalida;

        //        //mensaje = "Estado: " + lstr_Estado + " Fecha Mod: " + ldt_FchModifica + " A partir:" + FchAPartir;
        //    }
        //    catch (Exception e)
        //    {
        //        mensaje = "Código 99: " + e.ToString();
        //    }
        //    return mensaje;
        //}


        [WebMethod]
        public DataSet ConsultarInteresPunitivoPago(string lstr_IdPrestamo, string lint_id_tramo, string ldt_FchAPartir, string lint_Secuencia)
        {
            DataSet ldas_InteresPunitivo = new DataSet();
            string mensaje = "";

            DateTime? FchAPartir = null;

            int? id_tramo = null;
            int? secuencia = null;
            try
            {
                try
                {
                    if (!string.IsNullOrEmpty(ldt_FchAPartir))
                        FchAPartir = DateTime.ParseExact(ldt_FchAPartir, lstr_formato_fecha, CultureInfo.InvariantCulture);                    
                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
                }
                try
                {
                    if (!string.IsNullOrEmpty(lint_id_tramo))
                        id_tramo = Convert.ToInt32(lint_id_tramo);
                    if (!string.IsNullOrEmpty(lint_Secuencia))
                        secuencia = Convert.ToInt32(lint_Secuencia);
                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: Formato incorrecto de campo ";
                }
                if (mensaje == "")
                {
                    clsInteresPunitivo lcls_InteresPunitivo = new clsInteresPunitivo();
                    ldas_InteresPunitivo = lcls_InteresPunitivo.ConsultarInteresPunitivoPago(lstr_IdPrestamo, id_tramo, FchAPartir, secuencia);
                }
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return ldas_InteresPunitivo;
        }

        #endregion

        #region Pagos


        [WebMethod]
        public string CrearPago(string lstr_IdPrestamo, string lint_IdTramo, string lint_IdPago, string lstr_IdMoneda, string lint_IdAcreedor,
            string lstr_RefAcreedor, string ldec_MontoInteres, string ldec_MontoComisiones, string ldec_MontoPrincipal,
            string ldt_FechaValor, string ldt_FechaOperacion, string ldt_FechaTipoCambio)
        {
            string lstr_Estado = "ACT";
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";

            int? IdTramo = null;
            int? IdPago = null;
            int? IdAcreedor = null;
            decimal? MontoInteres = null;
            decimal? MontoComisiones = null;
            decimal? MontoPrincipal = null;

            DateTime? FechaValor = null;
            DateTime? FechaOperacion = null;
            DateTime? FechaTipoCambio = null;
            try
            {
                try
                {
                    if (!string.IsNullOrEmpty(ldt_FechaValor))
                        FechaValor = DateTime.ParseExact(ldt_FechaValor, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(ldt_FechaOperacion))
                        FechaOperacion = DateTime.ParseExact(ldt_FechaOperacion, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(ldt_FechaTipoCambio))
                        FechaTipoCambio = DateTime.ParseExact(ldt_FechaTipoCambio, lstr_formato_fecha, CultureInfo.InvariantCulture);
                }
                catch(Exception ex)
                {
                    mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
                }
                try
                {
                    if (!string.IsNullOrEmpty(lint_IdTramo))
                        IdTramo = Convert.ToInt32(lint_IdTramo);
                    if (!string.IsNullOrEmpty(lint_IdPago))
                        IdPago = Convert.ToInt32(lint_IdPago);
                    if (!string.IsNullOrEmpty(lint_IdAcreedor))
                        IdAcreedor = Convert.ToInt32(lint_IdAcreedor);

                    ldec_MontoInteres = ldec_MontoInteres.Replace(",", lstr_separador_decimal);
                    ldec_MontoInteres = ldec_MontoInteres.Replace(".", lstr_separador_decimal);
                    ldec_MontoComisiones = ldec_MontoComisiones.Replace(",", lstr_separador_decimal);
                    ldec_MontoComisiones = ldec_MontoComisiones.Replace(".", lstr_separador_decimal);
                    ldec_MontoPrincipal = ldec_MontoPrincipal.Replace(",", lstr_separador_decimal);
                    ldec_MontoPrincipal = ldec_MontoPrincipal.Replace(".", lstr_separador_decimal);
                    if (!string.IsNullOrEmpty(ldec_MontoInteres))
                        MontoInteres = Convert.ToDecimal(ldec_MontoInteres);
                    if (!string.IsNullOrEmpty(ldec_MontoComisiones))
                        MontoComisiones = Convert.ToDecimal(ldec_MontoComisiones);
                    if (!string.IsNullOrEmpty(ldec_MontoPrincipal))
                        MontoPrincipal = Convert.ToDecimal(ldec_MontoPrincipal);
                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: Formato incorrecto de campo ";
                }
                if (mensaje == "")
                {

                    clsPago lcls_Pago = new clsPago();
                    lcls_Pago.CrearPago(lstr_IdPrestamo, IdTramo, IdPago,
                        lstr_IdMoneda, IdAcreedor, lstr_RefAcreedor, MontoInteres, MontoComisiones, MontoPrincipal,
                        FechaValor, FechaOperacion, FechaTipoCambio, lstr_Estado, lstr_UsrCreacion, out codSalida, out txtSalida);

                    codSalida = ((codSalida == "00")||(codSalida == "01")) ? codSalida : "99"; mensaje = "Código " + codSalida + ": " + txtSalida;
                }
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return mensaje;
        }
        [WebMethod]
        public string ModificarPago(string lstr_IdPrestamo, string lint_IdTramo, string lint_IdPago, string lstr_IdMoneda, string lint_IdAcreedor,
            string lstr_RefAcreedor, string ldec_MontoInteres, string ldec_MontoComisiones, string ldec_MontoPrincipal,
            string ldt_FechaValor, string ldt_FechaOperacion, string ldt_FechaTipoCambio)
        {
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";
            DateTime ldt_FchModifica = new DateTime();
            int? IdTramo = null;
            int? IdPago = null;
            int? IdAcreedor = null;
            decimal? MontoInteres = null;
            decimal? MontoComisiones = null;
            decimal? MontoPrincipal = null;

            DateTime? FechaValor = null;
            DateTime? FechaOperacion = null;
            DateTime? FechaTipoCambio = null;
            try
            {
                try
                {
                    if (!string.IsNullOrEmpty(ldt_FechaValor))
                        FechaValor = DateTime.ParseExact(ldt_FechaValor, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(ldt_FechaOperacion))
                        FechaOperacion = DateTime.ParseExact(ldt_FechaOperacion, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(ldt_FechaTipoCambio))
                        FechaTipoCambio = DateTime.ParseExact(ldt_FechaTipoCambio, lstr_formato_fecha, CultureInfo.InvariantCulture);
                }
                catch(Exception ex)
                {
                    mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
                }
                try
                {
                    if (!string.IsNullOrEmpty(lint_IdTramo))
                        IdTramo = Convert.ToInt32(lint_IdTramo);
                    if (!string.IsNullOrEmpty(lint_IdPago))
                        IdPago = Convert.ToInt32(lint_IdPago);
                    if (!string.IsNullOrEmpty(lint_IdAcreedor))
                        IdAcreedor = Convert.ToInt32(lint_IdAcreedor);

                    ldec_MontoInteres = ldec_MontoInteres.Replace(",", lstr_separador_decimal);
                    ldec_MontoInteres = ldec_MontoInteres.Replace(".", lstr_separador_decimal);
                    ldec_MontoComisiones = ldec_MontoComisiones.Replace(",", lstr_separador_decimal);
                    ldec_MontoComisiones = ldec_MontoComisiones.Replace(".", lstr_separador_decimal);
                    ldec_MontoPrincipal = ldec_MontoPrincipal.Replace(",", lstr_separador_decimal);
                    ldec_MontoPrincipal = ldec_MontoPrincipal.Replace(".", lstr_separador_decimal);
                    if (!string.IsNullOrEmpty(ldec_MontoInteres))
                        MontoInteres = Convert.ToDecimal(ldec_MontoInteres);
                    if (!string.IsNullOrEmpty(ldec_MontoComisiones))
                        MontoComisiones = Convert.ToDecimal(ldec_MontoComisiones);
                    if (!string.IsNullOrEmpty(ldec_MontoPrincipal))
                        MontoPrincipal = Convert.ToDecimal(ldec_MontoPrincipal);
                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: Formato incorrecto de campo ";
                }
                if (mensaje == "")
                {
                    try { 
                     ldt_FchModifica = Convert.ToDateTime(ConsultarPago(lstr_IdPrestamo, Convert.ToString(lint_IdTramo)).Tables[0].Rows[0]["FchModifica"].ToString());
                    }
                    catch (Exception ex){
                        mensaje = "Código 99: Error al obtener registro por modificar " + ex.ToString();
                    }
                    if (mensaje == "")
                    {
                        clsPago lcls_Pago = new clsPago();
                        lcls_Pago.ModificarPago(lstr_IdPrestamo, IdTramo, IdPago,
                            lstr_IdMoneda, IdAcreedor, lstr_RefAcreedor, MontoInteres, MontoComisiones, MontoPrincipal,
                            FechaValor, FechaOperacion, FechaTipoCambio, lstr_UsrModifica, ldt_FchModifica, out codSalida, out txtSalida);

                        codSalida = ((codSalida == "00")||(codSalida == "01")) ? codSalida : "99"; mensaje = "Código " + codSalida + ": " + txtSalida;
                    }
                }
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return mensaje;
        }


        [WebMethod]
        public DataSet ConsultarPago(string lstr_IdPrestamo, string lint_id_tramo)
        {
            DataSet ldas_Pago = new DataSet();
            string mensaje = "";
            int? id_tramo = null;
            
            try
            {
                //try
                //{
                //    if (!string.IsNullOrEmpty(ldt_FchValorAcreedor))   
                //    FchValorAcreedor = DateTime.ParseExact(ldt_FchValorAcreedor, lstr_formato_fecha, CultureInfo.InvariantCulture);
                //    if (!string.IsNullOrEmpty(ldt_FchRecepcion))   
                //    FchRecepcion = DateTime.ParseExact(ldt_FchRecepcion, lstr_formato_fecha, CultureInfo.InvariantCulture);
                //    if (!string.IsNullOrEmpty(ldt_FchTipoCambio))   
                //    FchTipoCambio = DateTime.ParseExact(ldt_FchTipoCambio, lstr_formato_fecha, CultureInfo.InvariantCulture);
                //}
                //catch (Exception ex)
                //{
                //    mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
                //}
                try
                {
                    if (!string.IsNullOrEmpty(lint_id_tramo))
                        id_tramo = Convert.ToInt32(lint_id_tramo);
                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: Formato incorrecto de campo ";
                }
                if (mensaje == "")
                {
                    clsPago lcls_Pago = new clsPago();
                    ldas_Pago = lcls_Pago.ConsultarPago(lstr_IdPrestamo, id_tramo);
                }
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return ldas_Pago;
        }

        #endregion

        #region Préstamos
        
        /// <summary>
        /// Crea un préstamo en el sistema gestor
        /// </summary>
        /// <param name="lstr_IdPrestamo">Identificación del préstamo</param>
        /// <param name="lstr_Fuente">Fuente del préstamo</param>
        /// <param name="lstr_Situacion">Situación del préstamo</param>
        /// <param name="lstr_Plazo">Plazo del préstamo</param>
        /// <param name="lstr_Nombre">Nombre del préstamo</param>
        /// <param name="ldt_Firmado">Fecha de firma del préstamo</param>
        /// <param name="ldt_LimiteGiro">Fecha límite del giro</param>
        /// <param name="ldt_LimiteEfectivo">Fecha límite de efectivo</param>
        /// <param name="ldt_Efectivo">Fecha de efectivo</param>
        /// <param name="ldec_Monto">Monto del préstamo</param>
        /// <param name="lstr_IdMoneda">Moneda del préstamo</param>
        /// <param name="lstr_TipoTramo">Tipo de tramo del préstamo</param>
        /// <param name="lstr_Proposito">Propósito del préstamo</param>
        /// <param name="lstr_GarantiaPublica">Garantía pública</param>
        /// <param name="lstr_OrigenDeuda">Origen de la deuda</param>
        /// <param name="lint_IdAcreedor">Identificación de acreedor</param>
        /// <param name="lint_IdDeudor">Identificación de deudor</param>
        /// <param name="lstr_TipoPrestamo">Tipo de préstamo</param>
        /// <param name="ldec_Tasa">Tasa del préstamo</param>
        /// <returns>Retorna un mensaje con el código y texto de la transacción</returns>
        [WebMethod]
        public string CrearPrestamo(string lstr_IdPrestamo, string lstr_Fuente, string lstr_Situacion, string lstr_Plazo,
            string lstr_Nombre, string ldt_Firmado, string ldt_LimiteGiro, string ldt_LimiteEfectivo, string ldt_Efectivo,
            string ldec_Monto, string lstr_IdMoneda, string lstr_TipoTramo, string lstr_Proposito, string lstr_GarantiaPublica,
            string lstr_OrigenDeuda, /*int lint_IdAcreedor, int lint_IdDeudor, decimal ldec_Tasa,*/
            string lstr_NbrAcreedor, string lstr_CatAcreedor, string lstr_TpoAcreedor, string lstr_NbrDeudor,
            string lstr_CatDeudor, string lstr_TipoPrestamo, string lstr_CondicionPrestamo, string lstr_ExisteObligacion,
            string lstr_CondicionMotivo, string ldec_CondicionTasa, string ldec_CondicionMonto, string ldt_CondicionFchInicio,
            string ldt_CondicionFchFin)
        {
            string lstr_Estado = "ACT";
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";
            DateTime firmado = new DateTime();
                DateTime limiteGiro = new DateTime();
                DateTime limiteEfectivo = new DateTime();
                DateTime efectivo = new DateTime();
                DateTime fchInicioCondicion = new DateTime();
                DateTime fchFinCondicion = new DateTime();

            try
            {
                try { 
                firmado = DateTime.ParseExact(ldt_Firmado, lstr_formato_fecha, CultureInfo.InvariantCulture);
                limiteGiro = DateTime.ParseExact(ldt_LimiteGiro, lstr_formato_fecha, CultureInfo.InvariantCulture);
                limiteEfectivo = DateTime.ParseExact(ldt_LimiteEfectivo, lstr_formato_fecha, CultureInfo.InvariantCulture);
                efectivo = DateTime.ParseExact(ldt_Efectivo, lstr_formato_fecha, CultureInfo.InvariantCulture);
                fchInicioCondicion = DateTime.ParseExact(ldt_CondicionFchInicio, lstr_formato_fecha, CultureInfo.InvariantCulture);
                fchFinCondicion = DateTime.ParseExact(ldt_CondicionFchFin, lstr_formato_fecha, CultureInfo.InvariantCulture);
                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
                }
                if (mensaje == "")
                {
                    ldec_Monto = ldec_Monto.Replace(",", lstr_separador_decimal);
                    ldec_Monto = ldec_Monto.Replace(".", lstr_separador_decimal);
                    ldec_CondicionTasa = ldec_CondicionTasa.Replace(",", lstr_separador_decimal);
                    ldec_CondicionTasa = ldec_CondicionTasa.Replace(".", lstr_separador_decimal);
                    ldec_CondicionMonto = ldec_CondicionMonto.Replace(",", lstr_separador_decimal);
                    ldec_CondicionMonto = ldec_CondicionMonto.Replace(".", lstr_separador_decimal);
                    clsPrestamo lcls_Prestamo = new clsPrestamo();
                    lcls_Prestamo.CrearPrestamo(lstr_IdPrestamo, lstr_Fuente, lstr_Situacion, lstr_Plazo,
                    lstr_Nombre, firmado, limiteGiro, limiteEfectivo, efectivo,
                    Convert.ToDecimal(ldec_Monto), lstr_IdMoneda, lstr_TipoTramo, lstr_Proposito, lstr_GarantiaPublica,
                    lstr_OrigenDeuda, 0, 0, lstr_TipoPrestamo, 0,
                    lstr_NbrAcreedor, lstr_CatAcreedor, lstr_TpoAcreedor, lstr_NbrDeudor, lstr_CatDeudor,
                    lstr_CondicionPrestamo, lstr_ExisteObligacion, lstr_CondicionMotivo, Convert.ToDecimal(ldec_CondicionTasa),
                    Convert.ToDecimal(ldec_CondicionMonto), fchInicioCondicion, fchFinCondicion,
                    lstr_Estado, lstr_UsrCreacion, out codSalida, out txtSalida);

                    codSalida = ((codSalida == "00")||(codSalida == "01")) ? codSalida : "99"; mensaje = "Código " + codSalida + ": " + txtSalida;
                }
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return mensaje;
        }

        /// <summary>
        /// Modifica un préstamo en el sistema gestor
        /// </summary>
        /// <param name="lstr_IdPrestamo">Identificación del préstamo</param>
        /// <param name="lstr_Fuente">Fuente del préstamo</param>
        /// <param name="lstr_Situacion">Situación del préstamo</param>
        /// <param name="lstr_Plazo">Plazo del préstamo</param>
        /// <param name="lstr_Nombre">Nombre del préstamo</param>
        /// <param name="ldt_Firmado">Fecha de firma del préstamo</param>
        /// <param name="ldt_LimiteGiro">Fecha límite del giro</param>
        /// <param name="ldt_LimiteEfectivo">Fecha límite de efectivo</param>
        /// <param name="ldt_Efectivo">Fecha de efectivo</param>
        /// <param name="ldec_Monto">Monto del préstamo</param>
        /// <param name="lstr_IdMoneda">Moneda del préstamo</param>
        /// <param name="lstr_TipoTramo">Tipo de tramo del préstamo</param>
        /// <param name="lstr_Proposito">Propósito del préstamo</param>
        /// <param name="lstr_GarantiaPublica">Garantía pública</param>
        /// <param name="lstr_OrigenDeuda">Origen de la deuda</param>
        /// <param name="lint_IdAcreedor">Identificación de acreedor</param>
        /// <param name="lint_IdDeudor">Identificación de deudor</param>
        /// <param name="lstr_TipoPrestamo">Tipo de préstamo</param>
        /// <param name="ldec_Tasa">Tasa del préstamo</param>
        /// <returns>Retorna un mensaje con el código y texto de la transacción</returns>
        [WebMethod]
        public string ModificarPrestamo(string lstr_IdPrestamo, string lstr_Fuente, string lstr_Situacion, string lstr_Plazo,
            string lstr_Nombre, string ldt_Firmado, string ldt_LimiteGiro, string ldt_LimiteEfectivo, string ldt_Efectivo,
            string ldec_Monto, string lstr_IdMoneda, string lstr_TipoTramo, string lstr_Proposito, string lstr_GarantiaPublica,
            string lstr_OrigenDeuda, /*int lint_IdAcreedor, int lint_IdDeudor, decimal ldec_Tasa,*/
            string lstr_NbrAcreedor, string lstr_CatAcreedor, string lstr_TpoAcreedor, string lstr_NbrDeudor,
            string lstr_CatDeudor, string lstr_TipoPrestamo, string lstr_CondicionPrestamo, string lstr_ExisteObligacion,
            string lstr_CondicionMotivo, string ldec_CondicionTasa, string ldec_CondicionMonto, string ldt_CondicionFchInicio,
            string ldt_CondicionFchFin)
        {
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";
            DateTime ldt_FchModifica = new DateTime();
            try
            {
                DateTime firmado = DateTime.ParseExact(ldt_Firmado, lstr_formato_fecha, CultureInfo.InvariantCulture);
                DateTime limiteGiro = DateTime.ParseExact(ldt_LimiteGiro, lstr_formato_fecha, CultureInfo.InvariantCulture);
                DateTime limiteEfectivo = DateTime.ParseExact(ldt_LimiteEfectivo, lstr_formato_fecha, CultureInfo.InvariantCulture);
                DateTime efectivo = DateTime.ParseExact(ldt_Efectivo, lstr_formato_fecha, CultureInfo.InvariantCulture);
                
                DateTime fchInicioCondicion = DateTime.ParseExact(ldt_CondicionFchInicio, lstr_formato_fecha, CultureInfo.InvariantCulture);
                DateTime fchFinCondicion = DateTime.ParseExact(ldt_CondicionFchFin, lstr_formato_fecha, CultureInfo.InvariantCulture);
                try {
                 ldt_FchModifica = Convert.ToDateTime(ConsultarPrestamo(lstr_IdPrestamo, null, null, null, null, null, null, null, null, null, null, null, null).Tables[0].Rows[0]["FchModifica"].ToString());
                }
                catch (Exception ex){
                    mensaje = "Código 99: Error al obtener registro por modificar " + ex.ToString();
                }
                if (mensaje == "")
                {

                    ldec_Monto = ldec_Monto.Replace(",", lstr_separador_decimal);
                    ldec_Monto = ldec_Monto.Replace(".", lstr_separador_decimal);
                    ldec_CondicionTasa = ldec_CondicionTasa.Replace(",", lstr_separador_decimal);
                    ldec_CondicionTasa = ldec_CondicionTasa.Replace(".", lstr_separador_decimal);
                    ldec_CondicionMonto = ldec_CondicionMonto.Replace(",", lstr_separador_decimal);
                    ldec_CondicionMonto = ldec_CondicionMonto.Replace(".", lstr_separador_decimal);
                    clsPrestamo lcls_Prestamo = new clsPrestamo();
                    lcls_Prestamo.ModificarPrestamo(lstr_IdPrestamo, lstr_Fuente, lstr_Situacion, lstr_Plazo,
                    lstr_Nombre, firmado, limiteGiro, limiteEfectivo, efectivo,
                    Convert.ToDecimal(ldec_Monto), lstr_IdMoneda, lstr_TipoTramo, lstr_Proposito, lstr_GarantiaPublica,
                    lstr_OrigenDeuda, 0, 0, lstr_TipoPrestamo, 0,
                    lstr_NbrAcreedor, lstr_CatAcreedor, lstr_TpoAcreedor, lstr_NbrDeudor, lstr_CatDeudor,
                    lstr_CondicionPrestamo, lstr_ExisteObligacion, lstr_CondicionMotivo, Convert.ToDecimal(ldec_CondicionTasa),
                    Convert.ToDecimal(ldec_CondicionMonto), fchInicioCondicion, fchFinCondicion,
                    lstr_UsrModifica, ldt_FchModifica, out codSalida, out txtSalida);

                    codSalida = ((codSalida == "00")||(codSalida == "01")) ? codSalida : "99"; mensaje = "Código " + codSalida + ": " + txtSalida;
                }
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return mensaje;
        }

        /// <summary>
        /// Cambia el estado de un préstamo en el sistema gestor.
        /// </summary>
        /// <param name="lstr_IdPrestamo">Identificador único del préstamo a habilitar/inhabilitar</param>
        /// <returns>Retorna un mensaje con el código y texto de la transacción</returns>
        [WebMethod]
        public string EliminarPrestamo(string lstr_IdPrestamo)
        {
            DataTable ldat_Prestamo = new DataTable();
            DateTime ldt_FchModifica = new DateTime();
            string lstr_Estado = "";
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";
            ldat_Prestamo = ConsultarPrestamo(lstr_IdPrestamo, null, null, null, null, null, null, null, null, null, null, null, null).Tables[0];

            try
            {
                lstr_Estado = ldat_Prestamo.Rows[0]["Estado"].ToString();
                ldt_FchModifica = Convert.ToDateTime(ldat_Prestamo.Rows[0]["FchModifica"].ToString());
                clsPrestamo lcls_Prestamo = new clsPrestamo();
                lcls_Prestamo.CambiarEstadoPrestamo(lstr_IdPrestamo, lstr_Estado, lstr_UsrModifica, ldt_FchModifica, out codSalida, out txtSalida);
                codSalida = ((codSalida == "00")||(codSalida == "01")) ? codSalida : "99"; mensaje = "Código " + codSalida + ": " + txtSalida;
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }
            return mensaje;
        }

        /// <summary>
        /// Consulta un préstamo en el sistema gestor
        /// </summary>
        /// <param name="lint_opcion">Tipo de operación a realizar: 
        /// -1 Consulta por Id de préstamo
        /// -2 Consulta por Fuente
        /// -3 Consulta por Situación
        /// -4 Consulta por Plazo
        /// -5 Consulta por Nombre
        /// -6 Consulta por Fecha de inicio y fecha de Fin
        /// -7 Consulta por Id de acreedor
        /// -8 Consulta por Id de Deudor
        /// -9 Consulta por Tipo de préstamo</param>
        /// <param name="lstr_valor1">Valor de entrada 1</param>
        /// <param name="lstr_valor2">Valor de entrada 2</param>
        /// <returns>Retorna un dataset con el resultado de la consulta</returns>
        [WebMethod]
        //public DataSet ConsultarPrestamo(int lint_opcion, string lstr_valor1, string lstr_valor2)
        public DataSet ConsultarPrestamo(string lstr_IdPrestamo, string ldt_FechaInicio, string ldt_FechaFin, string lstr_Fuente,
            string lstr_Situacion, string lstr_Plazo, string lstr_Nombre, string lstr_NbrAcreedor, string lstr_CatAcreedor,
            string lstr_TpoAcreedor, string lstr_NbrDeudor, string lstr_CatDeudor, string lstr_TipoPrestamo)
        {
            DataSet ldas_Prestamos = new DataSet();
            string mensaje = "";


            DateTime? FchInicio = null;
            DateTime? FchFin = null;
            try
            {
                try
                {
                    if (!string.IsNullOrEmpty(ldt_FechaInicio))
                        FchInicio = DateTime.ParseExact(ldt_FechaInicio, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(ldt_FechaFin))
                        FchFin = DateTime.ParseExact(ldt_FechaFin, lstr_formato_fecha, CultureInfo.InvariantCulture);
                }
                catch(Exception ex)
                {
                    mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
                }

                if (mensaje == "")
                {
                    clsPrestamo lcls_Prestamo = new clsPrestamo();
                    ldas_Prestamos = lcls_Prestamo.ConsultarPrestamo(lstr_IdPrestamo, FchInicio, FchFin, lstr_Fuente,
                                                                     lstr_Situacion, lstr_Plazo, lstr_Nombre, lstr_NbrAcreedor, lstr_CatAcreedor,
                                                                     lstr_TpoAcreedor, lstr_NbrDeudor, lstr_CatDeudor, lstr_TipoPrestamo);
                }
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return ldas_Prestamos;
        }

        #endregion

        #region Tasa Flotante


        [WebMethod]
        public string CrearTasaFlotante(string lstr_IdPrestamo, string lint_IdTramo, string ldt_APartir, string ldec_Tasa)
        {
            string lstr_Estado = "ACT";
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";

            try
            {
                ldec_Tasa = ldec_Tasa.Replace(",", lstr_separador_decimal);
                ldec_Tasa = ldec_Tasa.Replace(".", lstr_separador_decimal);
                DateTime APartir = DateTime.ParseExact(ldt_APartir, lstr_formato_fecha, CultureInfo.InvariantCulture);

                clsTasaFlotante lcls_TasaFlotante = new clsTasaFlotante();
                lcls_TasaFlotante.CrearTasaFlotante(lstr_IdPrestamo, Convert.ToInt32(lint_IdTramo), APartir, Convert.ToDecimal(ldec_Tasa), lstr_Estado, lstr_UsrCreacion, out codSalida, out txtSalida);

                codSalida = ((codSalida == "00")||(codSalida == "01")) ? codSalida : "99"; mensaje = "Código " + codSalida + ": " + txtSalida;
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return mensaje;
        }


        [WebMethod]
        public string ModificarTasaFlotante(string lstr_IdPrestamo, string lint_IdTramo, string ldt_APartir, string ldec_Tasa)
        {
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";
            DateTime ldt_FchModifica = new DateTime();
            try
            {
                DateTime APartir = DateTime.ParseExact(ldt_APartir, lstr_formato_fecha, CultureInfo.InvariantCulture);
                try
                {
                 ldt_FchModifica = Convert.ToDateTime(ConsultarTasaFlotante(lstr_IdPrestamo, Convert.ToString(lint_IdTramo), ldt_APartir).Tables[0].Rows[0]["FchModifica"].ToString());
                }
                catch (Exception ex){
                    mensaje = "Código 99: Error al obtener registro por modificar " + ex.ToString();
                }
                if (mensaje == "")
                {
                    ldec_Tasa = ldec_Tasa.Replace(",", lstr_separador_decimal);
                    ldec_Tasa = ldec_Tasa.Replace(".", lstr_separador_decimal);

                    clsTasaFlotante lcls_TasaFlotante = new clsTasaFlotante();
                    lcls_TasaFlotante.ModificarTasaFlotante(lstr_IdPrestamo, Convert.ToInt32(lint_IdTramo), APartir, Convert.ToDecimal(ldec_Tasa), lstr_UsrModifica, ldt_FchModifica, out codSalida, out txtSalida);

                    codSalida = ((codSalida == "00")||(codSalida == "01")) ? codSalida : "99"; mensaje = "Código " + codSalida + ": " + txtSalida;
                }
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return mensaje;
        }


        [WebMethod]
        public string EliminarTasaFlotante(string lstr_IdPrestamo, string lint_IdTramo, string ldt_APartir)
        {
            DataTable ldat_TasaFlotante = new DataTable();
            DateTime ldt_FchModifica = new DateTime();

            string lstr_Estado = "";
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";

            ldat_TasaFlotante = ConsultarTasaFlotante(lstr_IdPrestamo, lint_IdTramo, ldt_APartir).Tables[0];

            try
            {
                lstr_Estado = ldat_TasaFlotante.Rows[0]["Estado"].ToString();
                ldt_FchModifica = Convert.ToDateTime(ldat_TasaFlotante.Rows[0]["FchModifica"].ToString());
                clsTasaFlotante lcls_TasaFlotante = new clsTasaFlotante();

                DateTime APartir = DateTime.ParseExact(ldt_APartir, lstr_formato_fecha, CultureInfo.InvariantCulture);

                lcls_TasaFlotante.CambiarTasaFlotante(lstr_IdPrestamo, Convert.ToInt32(lint_IdTramo), APartir, lstr_Estado, lstr_UsrModifica, ldt_FchModifica, out codSalida, out txtSalida);
                codSalida = ((codSalida == "00")||(codSalida == "01")) ? codSalida : "99"; mensaje = "Código " + codSalida + ": " + txtSalida;
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }
            return mensaje;
        }


        [WebMethod]
        public DataSet ConsultarTasaFlotante(string lstr_IdPrestamo, string lint_id_tramo, string ldt_FchAPartir)
        {
            DataSet ldas_TasaFlotante = new DataSet();
            string mensaje = "";
           DateTime? FchAPartir = null;

            int? id_tramo = null;
            try
            {
                try
                {
                    if (!string.IsNullOrEmpty(ldt_FchAPartir))
                        FchAPartir = DateTime.ParseExact(ldt_FchAPartir, lstr_formato_fecha, CultureInfo.InvariantCulture);
                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
                }
                try
                {
                    if (!string.IsNullOrEmpty(lint_id_tramo))
                        id_tramo = Convert.ToInt32(lint_id_tramo);
                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: Formato incorrecto de campo ";
                }
                if (mensaje == "")
                {
                    clsTasaFlotante lcls_TasaFlotante = new clsTasaFlotante();
                    ldas_TasaFlotante = lcls_TasaFlotante.ConsultarTasaFlotante(lstr_IdPrestamo, id_tramo, FchAPartir);
                }
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return ldas_TasaFlotante;
        }

        #endregion

        #region Tramo


        [WebMethod]
        public string CrearTramo(string lstr_IdPrestamo, string lint_IdTramo, string lstr_TipoAcuerdo, string lstr_TipoFinanciamiento, string lstr_TipoInstrumento,
            string lstr_TerminoCredito, string lstr_Reorganizacion, string lstr_TerminoReorganizado, string ldec_Monto,
            string lstr_IdMoneda, string ldec_Tasa = "0")
        {
            string lstr_Estado = "ACT";
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";

            try
            {
                ldec_Monto = ldec_Monto.Replace(",", lstr_separador_decimal);
                ldec_Monto = ldec_Monto.Replace(".", lstr_separador_decimal);
                ldec_Tasa = ldec_Tasa.Replace(",", lstr_separador_decimal);
                ldec_Tasa = ldec_Tasa.Replace(".", lstr_separador_decimal);
                clsTramo lcls_Tramo = new clsTramo();
                lcls_Tramo.CrearTramo(lstr_IdPrestamo, Convert.ToInt32(lint_IdTramo), lstr_TipoAcuerdo, lstr_TipoFinanciamiento, lstr_TipoInstrumento,
                    lstr_TerminoCredito, lstr_Reorganizacion, lstr_TerminoReorganizado, Convert.ToDecimal(ldec_Monto),
                    lstr_IdMoneda, Convert.ToDecimal(ldec_Tasa), lstr_Estado, lstr_UsrCreacion, out codSalida, out txtSalida);

                codSalida = ((codSalida == "00")||(codSalida == "01")) ? codSalida : "99"; mensaje = "Código " + codSalida + ": " + txtSalida;
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return mensaje;
        }


        [WebMethod]
        public string ModificarTramo(string lstr_IdPrestamo, string lint_IdTramo, string lstr_TipoAcuerdo, string lstr_TipoFinanciamiento, string lstr_TipoInstrumento,
            string lstr_TerminoCredito, string lstr_Reorganizacion, string lstr_TerminoReorganizado, string ldec_Monto,
            string lstr_IdMoneda, string ldec_Tasa = "0")
        {
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";
            DateTime ldt_FchModifica = new DateTime();
            try
            {
                try{
                 ldt_FchModifica = Convert.ToDateTime(ConsultarTramo(lstr_IdPrestamo, Convert.ToString(lint_IdTramo), null, null, null, null, null, null, null).Tables[0].Rows[0]["FchModifica"].ToString());
                                }
                catch (Exception ex){
                    mensaje = "Código 99: Error al obtener registro por modificar " + ex.ToString();
                }
                if (mensaje == "")
                {
                    ldec_Monto = ldec_Monto.Replace(",", lstr_separador_decimal);
                    ldec_Monto = ldec_Monto.Replace(".", lstr_separador_decimal);
                    ldec_Tasa = ldec_Tasa.Replace(",", lstr_separador_decimal);
                    ldec_Tasa = ldec_Tasa.Replace(".", lstr_separador_decimal);
                    clsTramo lclsTramo = new clsTramo();
                    lclsTramo.ModificarTramo(lstr_IdPrestamo, Convert.ToInt32(lint_IdTramo), lstr_TipoAcuerdo, lstr_TipoFinanciamiento, lstr_TipoInstrumento,
                        lstr_TerminoCredito, lstr_Reorganizacion, lstr_TerminoReorganizado, Convert.ToDecimal(ldec_Monto),
                        lstr_IdMoneda, Convert.ToDecimal(ldec_Tasa), lstr_UsrModifica, ldt_FchModifica, out codSalida, out txtSalida);

                    codSalida = ((codSalida == "00")||(codSalida == "01")) ? codSalida : "99"; mensaje = "Código " + codSalida + ": " + txtSalida;
                }
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return mensaje;
        }


        [WebMethod]
        public string EliminarTramo(string lstr_IdPrestamo, string lint_IdTramo)
        {
            DataTable ldat_InteresPunitivo = new DataTable();
            DateTime ldt_FchModifica = new DateTime();

            string lstr_Estado = "";
            string txtSalida = "";
            string codSalida = "";
            string mensaje = "";

            ldat_InteresPunitivo = ConsultarTramo(lstr_IdPrestamo, lint_IdTramo, null, null, null, null, null, null, null).Tables[0];

            try
            {
                lstr_Estado = ldat_InteresPunitivo.Rows[0]["Estado"].ToString();
                ldt_FchModifica = Convert.ToDateTime(ldat_InteresPunitivo.Rows[0]["FchModifica"].ToString());
                clsTramo lclsTramo = new clsTramo();

                lclsTramo.CambiarTramo(lstr_IdPrestamo, Convert.ToInt32(lint_IdTramo), lstr_Estado, lstr_UsrModifica, ldt_FchModifica, out codSalida, out txtSalida);
                codSalida = ((codSalida == "00")||(codSalida == "01")) ? codSalida : "99"; mensaje = "Código " + codSalida + ": " + txtSalida;
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }
            return mensaje;
        }


        [WebMethod]
        public DataSet ConsultarTramo(string lstr_IdPrestamo, string lint_IdTramo, string lstr_TipoAcuerdo, string lstr_TipoFinanciamiento,
            string lstr_TerminoCredito, string lstr_Reorganizacion, string lstr_TermReorganizacion, string ldec_Monto, string ldec_IdMoneda)
        {
            DataSet ldas_Tramo = new DataSet();
            string mensaje = "";
            Int32? IdTramo = null;
            decimal? Monto = null;
            try
            {
                //try
                //{
                //    if (!string.IsNullOrEmpty(ldt_FchDesde))
                //        FchDesde = DateTime.ParseExact(ldt_FchDesde, lstr_formato_fecha, CultureInfo.InvariantCulture);
                //    if (!string.IsNullOrEmpty(ldt_FchHasta))                       
                //        FchHasta = DateTime.ParseExact(ldt_FchHasta, lstr_formato_fecha, CultureInfo.InvariantCulture);
                //}
                //catch(Exception ex)
                //{
                //    mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
                //}
                try
                {
                    if (!string.IsNullOrEmpty(lint_IdTramo))
                        IdTramo = Convert.ToInt32(lint_IdTramo);

                    ldec_Monto = ldec_Monto.Replace(",", lstr_separador_decimal);
                    ldec_Monto = ldec_Monto.Replace(".", lstr_separador_decimal);
                    if (!string.IsNullOrEmpty(ldec_Monto))
                        Monto = Convert.ToDecimal(ldec_Monto);
                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: Formato incorrecto de campo ";
                }
                if (mensaje == "")
                {
                    clsTramo lclsTramo = new clsTramo();
                    ldas_Tramo = lclsTramo.ConsultarTramo(lstr_IdPrestamo, IdTramo, lstr_TipoAcuerdo, lstr_TipoFinanciamiento,
                                                          lstr_TerminoCredito, lstr_Reorganizacion, lstr_TermReorganizacion, Monto, ldec_IdMoneda);
                }
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();
            }

            return ldas_Tramo;
        }

        #endregion

        [WebMethod]
        public string DiferencialCambiario(string lstr_IdPrestamo, int lint_IdTramo, DateTime ldt_FchFin)
        {
            clsDiferencialCamb clsDiferencialCamb = new clsDiferencialCamb();
            
            clsDiferencialCamb.Diferencial(lstr_IdPrestamo, lint_IdTramo, ldt_FchFin);
            return "";
        }

        #region Saldos
        [WebMethod]
        public DataSet ConsultaSaldoDeudaExt(string lstr_IdPrestamo, string lint_IdTramo, string ldt_FechaDesde, string ldt_FechaHasta)
        {
            DataSet ldas_SaldoDeudaExt = new DataSet();
            string mensaje = "";

            DateTime? FechaDesde = null;
            DateTime? FechaHasta = null;
            int? IdTramo = null;

            try
            {
                try
                {
                    if (!string.IsNullOrEmpty(lint_IdTramo))
                        IdTramo = Convert.ToInt32(lint_IdTramo);
                    if (!string.IsNullOrEmpty(ldt_FechaDesde))
                        FechaDesde = DateTime.ParseExact(ldt_FechaDesde, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    if (!string.IsNullOrEmpty(ldt_FechaHasta))
                        FechaHasta = DateTime.ParseExact(ldt_FechaHasta, lstr_formato_fecha, CultureInfo.InvariantCulture);
                }
                catch (Exception ex)
                {
                    mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
                }

                if (mensaje == "")
                {
                    clsSaldoDeudaExt lcls_SaldoDeudaExt = new clsSaldoDeudaExt();
                    ldas_SaldoDeudaExt = lcls_SaldoDeudaExt.ConsultarSaldosDeudaExt(lstr_IdPrestamo, IdTramo, FechaDesde, FechaHasta);
                }
            }
            catch (Exception e)
            {
                mensaje = "Código 99: " + e.ToString();

            }

            return ldas_SaldoDeudaExt;
        }
        [WebMethod]
        public string CalculaDevengoDE(string lstr_IdPrestamo = "", string lint_IdTramo = "", string ldt_FechaHasta = "", char lchr_Xtir = '1')
        {
            DateTime? FechaHasta = null;
            int? IdTramo = null;
            string mensaje = "";
            try
            {
                if (!string.IsNullOrEmpty(ldt_FechaHasta))
                    FechaHasta = DateTime.ParseExact(ldt_FechaHasta, lstr_formato_fecha, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
            }
            try
            {
                if (!string.IsNullOrEmpty(lint_IdTramo))
                    IdTramo = Convert.ToInt32(lint_IdTramo);
            }
            catch (Exception ex)
            {
                mensaje = "Código 99: Formato incorrecto de campo ";
            }
            if (mensaje == "")
            {
                clsCalculosDeudaExterna lcls_DeudaExterna = new clsCalculosDeudaExterna();

                lcls_DeudaExterna.DevengoDE(out mensaje, lstr_IdPrestamo, IdTramo, FechaHasta, lchr_Xtir);

                return mensaje;
            }
            return null;

        }
        [WebMethod]
        public string ContabilizaDevengoDE(string lstr_IdPrestamo = "", string lint_IdTramo = "", string ldt_FechaHasta = "")
        {
            DateTime? FechaHasta = null;
            int? IdTramo = null;
            string mensaje = "";
            try
            {
                if (!string.IsNullOrEmpty(ldt_FechaHasta))
                    FechaHasta = DateTime.ParseExact(ldt_FechaHasta, lstr_formato_fecha, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
            }
            try
            {
                if (!string.IsNullOrEmpty(lint_IdTramo))
                    IdTramo = Convert.ToInt32(lint_IdTramo);
            }
            catch (Exception ex)
            {
                mensaje = "Código 99: Formato incorrecto de campo ";
            }
            if (mensaje == "")
            {
                clsCalculosDeudaExterna lcls_DeudaExterna = new clsCalculosDeudaExterna();

                lcls_DeudaExterna.ContabilizarDevengoDE(out mensaje, lstr_IdPrestamo, IdTramo, FechaHasta);

                return mensaje;
            }
            return null;

        }
        [WebMethod]
        public string uwsReversarAsiento(int Consecutivo, string CodAsiento, string ldt_FechaHasta = "")
        {
            string CorResultado = string.Empty;
            string Mensaje = string.Empty;
            bool Resultado = false;
            DateTime? FechaHasta = null;
            try
            {
                if (!string.IsNullOrEmpty(ldt_FechaHasta))
                    FechaHasta = DateTime.ParseExact(ldt_FechaHasta, lstr_formato_fecha, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                Mensaje = "Código 99: La fecha debe ser en formato: " + lstr_formato_fecha;
            }
            if (Mensaje == "")
            {
                try
                {
                    clsCalculosDeudaExterna lcls_DeudaExterna = new clsCalculosDeudaExterna();
                    Resultado = lcls_DeudaExterna.ReversarAsiento(Consecutivo,"", Convert.ToDateTime(FechaHasta), out CorResultado, out Mensaje);
                }
                catch (Exception e)
                {
                    Mensaje = "Error al reversar asiento " + e.ToString();
                }
            }
            return Mensaje;
        }

        #endregion
    }
}
