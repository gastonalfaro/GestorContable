using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using LogicaNegocio.Mantenimiento;
using LogicaNegocio.Seguridad;


namespace LogicaNegocio.CalculosFinancieros.DeudaInterna
{
    public class clsContabilizarPrescripciones
    {
        private static Mantenimiento.clsDinamico dinamica = new Mantenimiento.clsDinamico();
        private static Mantenimiento.clsNemotecnicos nemotecnico = new Mantenimiento.clsNemotecnicos();
        private static Mantenimiento.clsTiposCambio tipocambio = new Mantenimiento.clsTiposCambio();
        private static Mantenimiento.clsPropietarios propietario = new Mantenimiento.clsPropietarios();
        private static Mantenimiento.clsTiposAsiento tipoasiento = new Mantenimiento.clsTiposAsiento();
        private static Mantenimiento.clsOperaciones loperacion = new clsOperaciones();
        private static Seguridad.tBitacora bitacora = new Seguridad.tBitacora();
        private static tiras tira = new tiras();
        //private static wsAsientos.ServicioContable wsAsientos = new wsAsientos.ServicioContable();
        private static clsTiposAsiento tasientos = new clsTiposAsiento();
        private static clsCostoTransaccion lcls_CostoTransaccion = new clsCostoTransaccion();

        public string contabiliza_prescipciones(DateTime? lstr_Fch = null)
        {
            string lstr_Mensaje = string.Empty;
            try
            {
                //DataTable ldat_TitulosValores = titulo.ConsultarTituloValor("%", "%", "%", "%", "%", "01/01/1900", "01/01/5000").Tables[0].Select("IndicadorCupon ='V'").CopyToDataTable();
                //Consultar titulos
                //TODO: Hay que cambiar el filtro de la fecha FchCancelacion por FchVencimiento
                DateTime? _fch = lstr_Fch == null ? DateTime.Today : lstr_Fch;
                string query = "select * from cf.titulosvalores where (Descripcion is null or Descripcion != 'PrescritoGestor') and estadovalor='Prescrita' and indicadorcupon = 'V' and YEAR(FchModifica) = (" + _fch.Value.Year + ") and MONTH(FchModifica) = " + _fch.Value.Month + " and DAY(FchModifica) = " + _fch.Value.Day + "";
                DataSet totalTitulosValores = dinamica.ConsultarDinamico(query);
                DataTable ldat_TitulosValores = totalTitulosValores.Tables[0];
                DataTable ldat_Nemotecnicos = nemotecnico.ConsultarNemotecnicos(null, null, null, null, null).Tables[0];

                for (int i = 0; i < ldat_TitulosValores.Rows.Count; i++)
                {
                    try
                    {
                        string lstr_moneda = ldat_Nemotecnicos.Select("IdNemotecnico = '" + ldat_TitulosValores.Rows[i]["NemoTecnico"].ToString() + "'")[0]["IdMoneda"].ToString().Trim().Equals("CRCN")
                           ? "CRC" : ldat_Nemotecnicos.Select("IdNemotecnico = '" + ldat_TitulosValores.Rows[i]["NemoTecnico"].ToString() + "'")[0]["IdMoneda"].ToString().Trim();


                        //if (ldat_Nemotecnicos.Select("IdNemotecnico = '" + ldat_TitulosValores.Rows[i]["NemoTecnico"].ToString() + "'")[0]["Estado"].ToString().Trim() == "A"
                        //    && lstr_moneda == ldat_TitulosValores.Rows[i]["Moneda"].ToString())

                        if (ldat_Nemotecnicos.Select("IdNemotecnico = '" + ldat_TitulosValores.Rows[i]["NemoTecnico"].ToString() + "'")[0]["Estado"].ToString().Trim() == "A"
                            && lstr_moneda == (ldat_TitulosValores.Rows[i]["Moneda"].ToString().Equals("CRCN") ? "CRC" : ldat_TitulosValores.Rows[i]["Moneda"].ToString()))
                        {
                            if (ldat_TitulosValores.Rows[i]["EstadoValor"].ToString() == "Prescrita") //TODO: verificar si es "Prescrito"
                            {
                                /**
                                 * Valores:
                                 * 
                                 *  • Valor Facial
                                    • Valor transado bruto
                                    • Valor transado neto
                                    • Fecha de vencimiento
                                    • Numero de valor
                                    • Nemotécnico
                                    • Moneda
                                    • Propietario
                                    • Estado Valor
                                 * 
                                 * */
                                lstr_Mensaje = PrescribirTituloCupon(
                                     ldat_TitulosValores.Rows[i]["Tipo"].ToString(),
                                     ldat_TitulosValores.Rows[i]["EstadoValor"].ToString(),
                                    //Convert.ToDateTime(ldat_TitulosValores.Rows[i]["FchValor"].ToString()),
                                     (DateTime)_fch,//Convert.ToDateTime(ldat_TitulosValores.Rows[i]["FchVencimiento"].ToString()),
                                    //Convert.ToDateTime(ldat_TitulosValores.Rows[i]["FchCancelacion"].ToString()),
                                     ldat_TitulosValores.Rows[i]["Propietario"].ToString(),
                                     ldat_TitulosValores.Rows[i]["PlazoValor"].ToString(),

                                     Convert.ToDecimal(ldat_TitulosValores.Rows[i]["ValorTransadoBruto"].ToString()),
                                     ldat_TitulosValores.Rows[i]["Moneda"].ToString().Equals("CRCN") ? "CRC" : ldat_TitulosValores.Rows[i]["Moneda"].ToString(),

                                     ldat_TitulosValores.Rows[i]["NroValor"].ToString(),
                                     ldat_TitulosValores.Rows[i]["NemoTecnico"].ToString(),
                                     Convert.ToDecimal(ldat_TitulosValores.Rows[i]["ValorFacial"].ToString()),
                                     Convert.ToDecimal(ldat_TitulosValores.Rows[i]["InteresBruto"].ToString()),
                                    //Convert.ToDecimal(ldat_TitulosValores.Rows[i]["RendimientoPorDescuento"].ToString()),
                                    //Convert.ToDecimal(ldat_TitulosValores.Rows[i]["ImpuestoPagado"].ToString()),
                                     Convert.ToDecimal(ldat_TitulosValores.Rows[i]["ValorTransadoNeto"].ToString()),
                                    //Convert.ToDecimal(ldat_TitulosValores.Rows[i]["Premio"].ToString()),

                                     "SINPE: " + ldat_TitulosValores.Rows[i]["ModuloSINPE"].ToString().Trim() + "-" + "T.B: " +
                                     ldat_TitulosValores.Rows[i]["TasaBruta"].ToString().Trim() + "-" + "T.N: " +
                                     ldat_TitulosValores.Rows[i]["TasaNeta"].ToString().Trim() + "-" + "Plazo: " +
                                     ldat_TitulosValores.Rows[i]["PlazoValor"].ToString().Trim(),

                                     ldat_TitulosValores.Rows[i]["ModuloSINPE"].ToString().Trim(),
                                     ldat_TitulosValores.Rows[i]["IndicadorCupon"].ToString().Trim(),
                                     Convert.ToDateTime(ldat_TitulosValores.Rows[i]["FchModifica"].ToString()));
                                if (lstr_Mensaje.Contains("[S]"))
                                {
                                    dinamica.ConsultarDinamico(String.Format("UPDATE cf.TitulosValores SET Descripcion = 'PrescritoGestor' WHERE nrocupon={0} AND nrovalor = {1} AND nemotecnico = '{2}'", ldat_TitulosValores.Rows[i]["NroCupon"].ToString().Trim(), ldat_TitulosValores.Rows[i]["NroValor"].ToString().Trim(), ldat_TitulosValores.Rows[i]["nemotecnico"].ToString().Trim()));
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;//TODO: Insertar mensaje de error
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return lstr_Mensaje;
        }

        public string contabiliza_prescipcionesCupones(DateTime? lstr_Fch = null)
        {
            string lstr_Mensaje = string.Empty;
            try
            {
                //DataTable ldat_TitulosValores = titulo.ConsultarTituloValor("%", "%", "%", "%", "%", "01/01/1900", "01/01/5000").Tables[0].Select("IndicadorCupon ='V'").CopyToDataTable();
                //Consultar titulos
                //TODO: Hay que cambiar el filtro de la fecha FchCancelacion por FchVencimiento
                DateTime? _fch = lstr_Fch == null ? DateTime.Today : lstr_Fch;
                string query = "select * from cf.titulosvalores where (Descripcion is null or Descripcion != 'PrescritoGestor') and estadovalor='Prescrita' and indicadorcupon = 'C' and YEAR(FchModifica) = (" + _fch.Value.Year + ") and MONTH(FchModifica) = " + _fch.Value.Month + " and DAY(FchModifica) = " + _fch.Value.Day + "";
                DataSet totalTitulosValores = dinamica.ConsultarDinamico(query);
                DataTable ldat_TitulosValores = totalTitulosValores.Tables[0];
                DataTable ldat_Nemotecnicos = nemotecnico.ConsultarNemotecnicos(null, null, null, null, null).Tables[0];

                for (int i = 0; i < ldat_TitulosValores.Rows.Count; i++)
                {
                    try
                    {
                        string lstr_moneda = ldat_Nemotecnicos.Select("IdNemotecnico = '" + ldat_TitulosValores.Rows[i]["NemoTecnico"].ToString() + "'")[0]["IdMoneda"].ToString().Trim().Equals("CRCN")
                           ? "CRC" : ldat_Nemotecnicos.Select("IdNemotecnico = '" + ldat_TitulosValores.Rows[i]["NemoTecnico"].ToString() + "'")[0]["IdMoneda"].ToString().Trim();


                        //if (ldat_Nemotecnicos.Select("IdNemotecnico = '" + ldat_TitulosValores.Rows[i]["NemoTecnico"].ToString() + "'")[0]["Estado"].ToString().Trim() == "A"
                        //    && lstr_moneda == ldat_TitulosValores.Rows[i]["Moneda"].ToString())

                        if (ldat_Nemotecnicos.Select("IdNemotecnico = '" + ldat_TitulosValores.Rows[i]["NemoTecnico"].ToString() + "'")[0]["Estado"].ToString().Trim() == "A"
                            && lstr_moneda == (ldat_TitulosValores.Rows[i]["Moneda"].ToString().Equals("CRCN") ? "CRC" : ldat_TitulosValores.Rows[i]["Moneda"].ToString()))
                        {
                            if (ldat_TitulosValores.Rows[i]["EstadoValor"].ToString() == "Prescrita") //TODO: verificar si es "Prescrito"
                            {
                                /**
                                 * Valores:
                                 * 
                                 *  • Valor Facial
                                    • Valor transado bruto
                                    • Valor transado neto
                                    • Fecha de vencimiento
                                    • Numero de valor
                                    • Nemotécnico
                                    • Moneda
                                    • Propietario
                                    • Estado Valor
                                 * 
                                 * */
                                lstr_Mensaje = PrescribirTituloCupon(
                                     ldat_TitulosValores.Rows[i]["Tipo"].ToString(),
                                     ldat_TitulosValores.Rows[i]["EstadoValor"].ToString(),
                                    //Convert.ToDateTime(ldat_TitulosValores.Rows[i]["FchValor"].ToString()),
                                     (DateTime)_fch,//Convert.ToDateTime(ldat_TitulosValores.Rows[i]["FchVencimiento"].ToString()),
                                    //Convert.ToDateTime(ldat_TitulosValores.Rows[i]["FchCancelacion"].ToString()),
                                     ldat_TitulosValores.Rows[i]["Propietario"].ToString(),
                                     ldat_TitulosValores.Rows[i]["PlazoValor"].ToString(),

                                     Convert.ToDecimal(ldat_TitulosValores.Rows[i]["ValorTransadoBruto"].ToString()),
                                     ldat_TitulosValores.Rows[i]["Moneda"].ToString().Equals("CRCN") ? "CRC" : ldat_TitulosValores.Rows[i]["Moneda"].ToString(),

                                     ldat_TitulosValores.Rows[i]["NroValor"].ToString(),
                                     ldat_TitulosValores.Rows[i]["NemoTecnico"].ToString(),
                                     Convert.ToDecimal(ldat_TitulosValores.Rows[i]["ValorFacial"].ToString()),
                                     Convert.ToDecimal(ldat_TitulosValores.Rows[i]["InteresBruto"].ToString()),
                                    //Convert.ToDecimal(ldat_TitulosValores.Rows[i]["RendimientoPorDescuento"].ToString()),
                                    //Convert.ToDecimal(ldat_TitulosValores.Rows[i]["ImpuestoPagado"].ToString()),
                                     Convert.ToDecimal(ldat_TitulosValores.Rows[i]["ValorTransadoNeto"].ToString()),
                                    //Convert.ToDecimal(ldat_TitulosValores.Rows[i]["Premio"].ToString()),

                                     "SINPE: " + ldat_TitulosValores.Rows[i]["ModuloSINPE"].ToString().Trim() + "-" + "T.B: " +
                                     ldat_TitulosValores.Rows[i]["TasaBruta"].ToString().Trim() + "-" + "T.N: " +
                                     ldat_TitulosValores.Rows[i]["TasaNeta"].ToString().Trim() + "-" + "Plazo: " +
                                     ldat_TitulosValores.Rows[i]["PlazoValor"].ToString().Trim(),

                                     ldat_TitulosValores.Rows[i]["ModuloSINPE"].ToString().Trim(),
                                     ldat_TitulosValores.Rows[i]["IndicadorCupon"].ToString().Trim(),
                                     Convert.ToDateTime(ldat_TitulosValores.Rows[i]["FchModifica"].ToString()));
                                if (lstr_Mensaje.Contains("[S]"))
                                {
                                    dinamica.ConsultarDinamico(String.Format("UPDATE cf.TitulosValores SET Descripcion = 'PrescritoGestor' WHERE nrocupon={1} AND nrovalor = {2} AND nemotecnico = '{3}'", ldat_TitulosValores.Rows[i]["NroCupon"].ToString().Trim(), ldat_TitulosValores.Rows[i]["NroValor"].ToString().Trim(), ldat_TitulosValores.Rows[i]["nemotecnico"].ToString().Trim()));
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;//TODO: Insertar mensaje de error
                    }
                }
            }
            catch (Exception ex)
            {
                throw;// lstr_Mensaje = ex.ToString();
            }
            return lstr_Mensaje;
        }

        private decimal Truncate(decimal value, int length)
        {
            return Math.Truncate(value * 100) / 100;
        }

        //TODO: Ajustar parametros, copiar metodo registrocontable RAMSES
        public string PrescribirTituloCupon(
            string lstr_Tipo,
            string lstr_EstadoValor,
            //DateTime ldt_FchValor,
            DateTime ldt_FchVencimiento,
            //DateTime ldt_FchCancelacion,
            string lstr_Propietario,

            string lstr_Plazo,

            decimal ldec_ValorTransadoBruto,
            string lstr_Moneda,
            string lstr_NroValor,
            string lstr_Nemotecnico,
            decimal ldec_ValorFacial,
            decimal ldec_InteresBruto,
            //decimal ldec_RendimientoXDescuento,//descuento
            //decimal ldec_ImpuestoPagado,
            decimal ldec_ValorTransadoNeto,
            //decimal ldec_Premio,//prima
            string lstr_Detalle,
            string lstr_Origen,
            string lstr_IndicadorCupon,
            DateTime ldt_FchModifica)
        {
            string lstr_Mensaje = string.Empty;
            wsDeudaInterna.wsDeudaInterna wsDeudaInterna = new wsDeudaInterna.wsDeudaInterna();
            DataTable ldat_Asiento = RegistroContable();
            DataTable ldat_Tira = new DataTable();
            string lint_EsPublico = "PUBLICO";
            string lstr_Operacion = string.Empty;
            string lstr_NomOperacion = string.Empty;
            decimal ldec_TipoCambioColones = Convert.ToDecimal(tipocambio.ConsultarTiposCambio("CRCN", /*ldt_FchVencimiento*/DateTime.Today, "3140", "N").Tables[0].Rows[0]["Valor"].ToString());
            decimal ldec_TipoCambioUDE = Convert.ToDecimal(tipocambio.ConsultarTiposCambio("UDE", /*ldt_FchVencimiento*/DateTime.Today, "", "N").Tables[0].Rows[0]["Valor"].ToString());
            decimal ldec_monto = 0;
            string lstr_Referencia = "";
            //Boolean esCortoPlazo = Convert.ToDecimal(lstr_Plazo) <= 360 ? true : false;
            
            //Define si el propietario es público o privado
            if (propietario.ConsultarPropietarios(string.Empty, string.Empty, string.Empty, lstr_Propietario, "S").Tables[0].Rows.Count == 0)
            {
                lint_EsPublico = "PRIVADO";
            }

            decimal ldec_TipoCambio = lstr_Moneda.Equals("USD") ? ldec_TipoCambioColones : (lstr_Moneda.Equals("CRC") ? 1 : ldec_TipoCambioUDE);

            bool SinError = true;

            if (lstr_IndicadorCupon.Equals("V"))
            {
                #region Títulos prescritos

                #region Prescripción
                //TODO: quitar if
                if (true)
                {
                    lstr_Operacion = lstr_Moneda.Equals("USD") ? "ID52" : (lstr_Moneda.Equals("CRC") ? "ID51" : "ID51");

                    DataSet lds_Operaciones = loperacion.ConsultarOperaciones(lstr_Operacion, "IdModulo IN ('DI')", "");
                    if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
                    {
                        lstr_NomOperacion = lds_Operaciones.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
                    }

                    ldat_Tira = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, null, null, null, null, null, "ID").Tables[0].Clone();
                    foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, null, null, null, lstr_Nemotecnico, lint_EsPublico, "ID", "CP", "CAPITAL").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico + "'").CopyToDataTable().Rows)
                        ldat_Tira.ImportRow(dr_tira);

                    foreach (DataRow ldr_Row in ldat_Tira.Rows)
                    {
                        string operacion = ldr_Row["CodigoAuxiliar2"].ToString();

                        int index = ldat_Tira.Rows.IndexOf(ldr_Row);


                        ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorFacial : (lstr_Moneda.Equals("CRC") ? ldec_ValorFacial : ldec_ValorFacial * ldec_TipoCambioUDE);

                        lstr_Referencia = lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Prescripción Título";

                        SinError = true;

                        #region pospre debe
                        //Validar que el pos pre sea diferente de PP_Balance y que el monto sea mayor al seleccionado
                        string lstr_Monto = string.Empty;
                        DataTable lds_Datos = new DataTable();
                        decimal ldec_MontoTotal = 0;
                        string reservasError = "";
                        string lstr_NuevoPosPrePago = string.Empty;
                        DataSet ldat_Reservas = new DataSet();

                        ldat_Reservas = dinamica.ConsultarDinamico("select * from ma.reservasdetalle where idcuentacontable = '" + ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim() + "' and idpospre = '" + ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim() + "' and LEFT(idprograma, 4) = year(getdate()) order by idprograma desc");
                        //ldat_Reservas = reservas.ConsultarReservasDetallado(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(), string.Empty, ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(), string.Empty, string.Empty, string.Empty, string.Empty);
                        if (ldat_Reservas.Tables[0].Rows.Count != 0)
                        {
                            DataView dv = ldat_Reservas.Tables[0].DefaultView;
                            dv.Sort = "OrdenDeudaInterna ASC";

                            lds_Datos.Columns.Add("IdReserva");
                            lds_Datos.Columns.Add("OrdenDeudaInterna");
                            lds_Datos.Columns.Add("IdPosPre");
                            lds_Datos.Columns.Add("Posicion");
                            lds_Datos.Columns.Add("Monto");

                            foreach (DataRow drForm in dv.ToTable().Rows)
                            {
                                //if (drForm["IdMoneda"].ToString().Trim().Equals(ldat_AsientoPago.Rows[0]["CodigoAuxiliar"].ToString().Trim()))
                                if (!drForm["OrdenDeudaInterna"].ToString().Equals(string.Empty) && !drForm["OrdenDeudaInterna"].ToString().Equals("0"))
                                {
                                    lstr_Monto = wsDeudaInterna.ConsultaMontoReservaSAP(drForm["IdReserva"].ToString().Trim(), drForm["Posicion"].ToString().Trim());
                                    lds_Datos.Rows.Add(
                                        drForm["IdReserva"].ToString(),
                                        drForm["OrdenDeudaInterna"].ToString(),
                                        drForm["IdPosPre"].ToString(),
                                        drForm["Posicion"].ToString(),
                                        lstr_Monto);
                                    reservasError += "Reserva :" + drForm["IdPosPre"].ToString().Trim() + ", Posición: " + drForm["Posicion"].ToString().Trim() + "\n";
                                    ldec_MontoTotal += Convert.ToDecimal(lstr_Monto);
                                }
                            }

                            if (Convert.ToDecimal(ldec_MontoTotal) >= (ldec_monto * ldec_TipoCambio))
                            {
                                //Genera el asiento
                                decimal ldec_Saldo = ldec_monto;

                                foreach (DataRow drForm in lds_Datos.Rows)
                                {
                                    if (Convert.ToDecimal(drForm["Monto"]) > 0 && ldec_Saldo >= 0)
                                    {
                                        //lstr_Referencia = lstr_NroValor.Trim().Equals("00") ? "No Asociado" : lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Costo Transaccion";

                                        ldat_Asiento.Rows.Add(
                                            lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                            ldt_FchVencimiento.ToString("dd.MM.yyyy"),
                                            ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim(),
                                            lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim(),
                                            ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                            drForm["IdReserva"].ToString().Trim(),
                                            drForm["Posicion"].ToString().Trim(),
                                            Truncate(ldec_Saldo > Convert.ToDecimal(drForm["Monto"].ToString()) ? Convert.ToDecimal(drForm["Monto"].ToString()) : ldec_Saldo, 2),
                                            lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                            tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                            lstr_Moneda,//tipo
                                            lstr_Operacion +"."+ lstr_NomOperacion //operacion
                                            );
                                    }

                                    //Resta el saldo    
                                    ldec_Saldo = ldec_Saldo - Convert.ToDecimal(drForm["Monto"]);
                                }
                            }
                            else
                            {
                                //Almacena en bitácora de que no lo hizo
                                bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion, "DI"), "Resultado de Contabilización: \n 1 - [E] Monto superior al total de las reservas de la Deuda Interna. Reservas utilizadas: \n" + reservasError, lstr_Operacion, lstr_NroValor + "-" + lstr_Nemotecnico, "G206");
                                SinError = false;
                                break;
                            }
                        }
                        else
                        {
                            if (!ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim().StartsWith("E"))
                            {
                                ldat_Asiento.Rows.Add(
                                    lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                    ldt_FchVencimiento.ToString("dd.MM.yyyy"),
                                    ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                    ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                    ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim(),
                                    lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim(),
                                    ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                    ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                    ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                    ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                    ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                    ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                    ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                    ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                    Truncate(ldec_monto, 2),
                                    lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                    tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                    lstr_Moneda,//tipo
                                    lstr_Operacion +"."+ lstr_NomOperacion//operacion
                                    );
                            }
                            else
                            {
                                bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion, "DI"), "Resultado de Contabilización: \n 1 - [E] No hay reservas correspondientes a la cuenta " + ldat_Tira.Rows[0]["IdCuentaContable"].ToString().Trim() + " con fondo " + ldat_Tira.Rows[0]["IdPosPre"].ToString().Trim(), lstr_Operacion, lstr_NroValor + "-" + lstr_Nemotecnico, "G206");
                                SinError = false;
                                break;
                            }
                        }

                        #endregion

                        #region pospre haber

                        lstr_Monto = string.Empty;
                        lds_Datos = new DataTable();
                        ldec_MontoTotal = 0;
                        reservasError = "";
                        lstr_NuevoPosPrePago = string.Empty;
                        ldat_Reservas = new DataSet();

                        if (SinError)
                        {
                            ldat_Reservas = dinamica.ConsultarDinamico("select * from ma.reservasdetalle where idcuentacontable = '" + ldat_Tira.Rows[index]["IdCuentaContable2"].ToString().Trim() + "' and idpospre = '" + ldat_Tira.Rows[index]["IdPosPre2"].ToString().Trim() + "'  and LEFT(idprograma, 4) = year(getdate()) order by idprograma desc");
                            //ldat_Reservas = reservas.ConsultarReservasDetallado(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, ldat_Tira.Rows[index]["IdCuentaContable2"].ToString().Trim(), string.Empty, ldat_Tira.Rows[index]["IdPosPre2"].ToString().Trim(), string.Empty, string.Empty, string.Empty, string.Empty);
                            if (ldat_Reservas.Tables[0].Rows.Count != 0)
                            {
                                DataView dv = ldat_Reservas.Tables[0].DefaultView;
                                dv.Sort = "OrdenDeudaInterna ASC";

                                lds_Datos.Columns.Add("IdReserva");
                                lds_Datos.Columns.Add("OrdenDeudaInterna");
                                lds_Datos.Columns.Add("IdPosPre");
                                lds_Datos.Columns.Add("Posicion");
                                lds_Datos.Columns.Add("Monto");

                                foreach (DataRow drForm in dv.ToTable().Rows)
                                {
                                    //if (drForm["IdMoneda"].ToString().Trim().Equals(ldat_AsientoPago.Rows[0]["CodigoAuxiliar"].ToString().Trim()))
                                    if (!drForm["OrdenDeudaInterna"].ToString().Equals(string.Empty) && !drForm["OrdenDeudaInterna"].ToString().Equals("0"))
                                    {
                                        lstr_Monto = wsDeudaInterna.ConsultaMontoReservaSAP(drForm["IdReserva"].ToString().Trim(), drForm["Posicion"].ToString().Trim());
                                        lds_Datos.Rows.Add(
                                            drForm["IdReserva"].ToString(),
                                            drForm["OrdenDeudaInterna"].ToString(),
                                            drForm["IdPosPre"].ToString(),
                                            drForm["Posicion"].ToString(),
                                            lstr_Monto);
                                        reservasError += "Reserva :" + drForm["IdPosPre"].ToString().Trim() + ", Posición: " + drForm["Posicion"].ToString().Trim() + "\n";
                                        ldec_MontoTotal += Convert.ToDecimal(lstr_Monto);
                                    }
                                }

                                if (Convert.ToDecimal(ldec_MontoTotal) >= (ldec_monto * ldec_TipoCambio))
                                {
                                    //Genera el asiento
                                    decimal ldec_Saldo = ldec_monto;

                                    foreach (DataRow drForm in lds_Datos.Rows)
                                    {
                                        if (Convert.ToDecimal(drForm["Monto"]) > 0 && ldec_Saldo >= 0)
                                        {
                                            //lstr_Referencia = lstr_NroValor.Trim().Equals("00") ? "No Asociado" : lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Costo Transaccion";

                                            ldat_Asiento.Rows.Add(
                                                lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                                ldt_FchVencimiento.ToString("dd.MM.yyyy"),
                                                ldat_Tira.Rows[index]["IdCuentaContable2"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdClaveContable2"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim(),
                                                lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim(),
                                                ldat_Tira.Rows[index]["IdCentroCosto2"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdCentroBeneficio2"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdElementoPEP2"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdPosPre2"].ToString().Trim().ToUpper(),
                                                ldat_Tira.Rows[index]["IdCentroGestor2"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdFondo2"].ToString().Trim(),
                                                drForm["IdReserva"].ToString().Trim(),
                                                drForm["Posicion"].ToString().Trim(),
                                                Truncate(ldec_Saldo > Convert.ToDecimal(drForm["Monto"].ToString()) ? Convert.ToDecimal(drForm["Monto"].ToString()) : ldec_Saldo, 2),
                                                lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                                tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                                lstr_Moneda,//tipo
                                                lstr_Operacion +"."+ lstr_NomOperacion //operacion
                                                );
                                        }

                                        //Resta el saldo    
                                        ldec_Saldo = ldec_Saldo - Convert.ToDecimal(drForm["Monto"]);
                                    }
                                }
                                else
                                {
                                    //Almacena en bitácora de que no lo hizo
                                    bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion, "DI"), "Resultado de Contabilización: \n 1 - [E] Monto superior al total de las reservas de la Deuda Interna. Reservas utilizadas: \n" + reservasError, lstr_Operacion, lstr_NroValor + "-" + lstr_Nemotecnico, "G206");
                                    SinError = false;
                                    break;
                                }
                            }
                            else
                            {
                                if (!ldat_Tira.Rows[index]["IdPosPre2"].ToString().Trim().StartsWith("E"))
                                {
                                    ldat_Asiento.Rows.Add(
                                        lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                        ldt_FchVencimiento.ToString("dd.MM.yyyy"),
                                        ldat_Tira.Rows[index]["IdCuentaContable2"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdClaveContable2"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim(),
                                        lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim(),
                                        ldat_Tira.Rows[index]["IdCentroCosto2"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdCentroBeneficio2"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdElementoPEP2"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdPosPre2"].ToString().Trim().ToUpper(),
                                        ldat_Tira.Rows[index]["IdCentroGestor2"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdFondo2"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["DocPresupuestario2"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["PosDocPresupuestario2"].ToString().Trim(),
                                        Truncate(ldec_monto, 2),
                                        lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                        tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                        lstr_Moneda,//tipo
                                        lstr_Operacion +"."+ lstr_NomOperacion //operacion
                                        );
                                }
                                else
                                {
                                    bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion, "DI"), "Resultado de Contabilización: \n 1 - [E] No hay reservas correspondientes a la cuenta " + ldat_Tira.Rows[index]["IdCuentaContable2"].ToString().Trim() + " con fondo " + ldat_Tira.Rows[index]["IdPosPre2"].ToString().Trim(), lstr_Operacion, lstr_NroValor + "-" + lstr_Nemotecnico, "G206");
                                    SinError = false;
                                    break;
                                }
                            }
                        }
                        #endregion
                    }
                }
                #endregion

                if (SinError)
                {
                    lstr_Mensaje = GenerarAsientoAjuste("Prescripción de Título", ldat_Asiento, lstr_Operacion, lstr_NroValor, lstr_Nemotecnico, ldec_TipoCambioColones, ldt_FchModifica);
                    //RAMSES
                    //if (lstr_Mensaje.Contains(""))
                    //dinamica.ConsultarDinamico("UPDATE cf.titulosvalores SET Descripcion = 'Prescrito' where NroCupon = 0 and nrovalor = " + Convert.ToInt32(lstr_NroValor.Trim()) + " and nemotecnico = '" + lstr_Nemotecnico.Trim() + "'");
                }

                #endregion
            }
            else
            {
                #region Cupones Prescritos

                #region Prescripción

                ldat_Asiento = RegistroContable();

                if (true)
                {
                    lstr_Operacion = lstr_Moneda.Equals("USD") ? "ID54" : (lstr_Moneda.Equals("CRC") ? "ID53" : "ID53");

                    DataSet lds_Operaciones = loperacion.ConsultarOperaciones(lstr_Operacion, "IdModulo IN ('DI')", "");
                    if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
                    {
                        lstr_NomOperacion = lds_Operaciones.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
                    }

                    ldat_Tira = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID").Tables[0].Clone();
                    foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", "INT_DEV").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico + "'").CopyToDataTable().Rows)
                        ldat_Tira.ImportRow(dr_tira);

                    foreach (DataRow ldr_Row in ldat_Tira.Rows)
                    {
                        string operacion = ldr_Row["CodigoAuxiliar2"].ToString();

                        int index = ldat_Tira.Rows.IndexOf(ldr_Row);

                        ldec_monto = lstr_Moneda.Equals("USD") ? ldec_InteresBruto : (lstr_Moneda.Equals("CRC") ? ldec_InteresBruto : ldec_InteresBruto * ldec_TipoCambioUDE);

                        lstr_Referencia = lstr_NroValor + "-" + lstr_Nemotecnico + " Prescripción Cupón";

                        SinError = true;

                        #region pospre debe
                        //Validar que el pos pre sea diferente de PP_Balance y que el monto sea mayor al seleccionado
                        string lstr_Monto = string.Empty;
                        DataTable lds_Datos = new DataTable();
                        decimal ldec_MontoTotal = 0;
                        string reservasError = "";
                        string lstr_NuevoPosPrePago = string.Empty;
                        DataSet ldat_Reservas = new DataSet();

                        ldat_Reservas = dinamica.ConsultarDinamico("select * from ma.reservasdetalle where idcuentacontable = '" + ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim() + "' and idpospre = '" + ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim() + "'  and LEFT(idprograma, 4) = year(getdate()) order by idprograma desc");
                        //ldat_Reservas = reservas.ConsultarReservasDetallado(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(), string.Empty, ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(), string.Empty, string.Empty, string.Empty, string.Empty);
                        if (ldat_Reservas.Tables[0].Rows.Count != 0)
                        {
                            DataView dv = ldat_Reservas.Tables[0].DefaultView;
                            dv.Sort = "OrdenDeudaInterna ASC";

                            lds_Datos.Columns.Add("IdReserva");
                            lds_Datos.Columns.Add("OrdenDeudaInterna");
                            lds_Datos.Columns.Add("IdPosPre");
                            lds_Datos.Columns.Add("Posicion");
                            lds_Datos.Columns.Add("Monto");

                            foreach (DataRow drForm in dv.ToTable().Rows)
                            {
                                //if (drForm["IdMoneda"].ToString().Trim().Equals(ldat_AsientoPago.Rows[0]["CodigoAuxiliar"].ToString().Trim()))
                                if (!drForm["OrdenDeudaInterna"].ToString().Equals(string.Empty) && !drForm["OrdenDeudaInterna"].ToString().Equals("0"))
                                {
                                    lstr_Monto = wsDeudaInterna.ConsultaMontoReservaSAP(drForm["IdReserva"].ToString().Trim(), drForm["Posicion"].ToString().Trim());
                                    lds_Datos.Rows.Add(
                                        drForm["IdReserva"].ToString(),
                                        drForm["OrdenDeudaInterna"].ToString(),
                                        drForm["IdPosPre"].ToString(),
                                        drForm["Posicion"].ToString(),
                                        lstr_Monto);
                                    reservasError += "Reserva :" + drForm["IdPosPre"].ToString().Trim() + ", Posición: " + drForm["Posicion"].ToString().Trim() + "\n";
                                    ldec_MontoTotal += Convert.ToDecimal(lstr_Monto);
                                }
                            }

                            if (Convert.ToDecimal(ldec_MontoTotal) >= (ldec_monto * ldec_TipoCambio))
                            {
                                //Genera el asiento
                                decimal ldec_Saldo = ldec_monto;

                                foreach (DataRow drForm in lds_Datos.Rows)
                                {
                                    if (Convert.ToDecimal(drForm["Monto"]) > 0 && ldec_Saldo >= 0)
                                    {
                                        //lstr_Referencia = lstr_NroValor.Trim().Equals("00") ? "No Asociado" : lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Costo Transaccion";

                                        ldat_Asiento.Rows.Add(
                                            lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                            ldt_FchVencimiento.ToString("dd.MM.yyyy"),
                                            ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim(),
                                            lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim(),
                                            ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                            drForm["IdReserva"].ToString().Trim(),
                                            drForm["Posicion"].ToString().Trim(),
                                            Truncate(ldec_Saldo > Convert.ToDecimal(drForm["Monto"].ToString()) ? Convert.ToDecimal(drForm["Monto"].ToString()) : ldec_Saldo, 2),
                                            lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                            tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                            lstr_Moneda,//tipo
                                            lstr_Operacion +"."+ lstr_NomOperacion //operacion
                                            );
                                    }

                                    //Resta el saldo    
                                    ldec_Saldo = ldec_Saldo - Convert.ToDecimal(drForm["Monto"]);
                                }
                            }
                            else
                            {
                                //Almacena en bitácora de que no lo hizo
                                bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion, "DI"), "Resultado de Contabilización: \n 1 - [E] Monto superior al total de las reservas de la Deuda Interna. Reservas utilizadas: \n" + reservasError, lstr_Operacion, lstr_NroValor + "-" + lstr_Nemotecnico, "G206");
                                SinError = false;
                                break;
                            }
                        }
                        else
                        {
                            if (!ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim().StartsWith("E"))
                            {
                                ldat_Asiento.Rows.Add(
                                    lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                    ldt_FchVencimiento.ToString("dd.MM.yyyy"),
                                    ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                    ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                    ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim(),
                                    lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim(),
                                    ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                    ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                    ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                    ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                    ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                    ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                    ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                    ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                    Truncate(ldec_monto, 2),
                                    lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                    tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                    lstr_Moneda,//tipo
                                    lstr_Operacion +"."+ lstr_NomOperacion //operacion
                                    );
                            }
                            else
                            {
                                bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion, "DI"), "Resultado de Contabilización: \n 1 - [E] No hay reservas correspondientes a la cuenta " + ldat_Tira.Rows[0]["IdCuentaContable"].ToString().Trim() + " con fondo " + ldat_Tira.Rows[0]["IdPosPre"].ToString().Trim(), lstr_Operacion, lstr_NroValor + "-" + lstr_Nemotecnico, "G206");
                                SinError = false;
                                break;
                            }
                        }

                        #endregion

                        #region pospre haber

                        lstr_Monto = string.Empty;
                        lds_Datos = new DataTable();
                        ldec_MontoTotal = 0;
                        reservasError = "";
                        lstr_NuevoPosPrePago = string.Empty;
                        ldat_Reservas = new DataSet();

                        if (SinError)
                        {
                            ldat_Reservas = dinamica.ConsultarDinamico("select * from ma.reservasdetalle where idcuentacontable = '" + ldat_Tira.Rows[index]["IdCuentaContable2"].ToString().Trim() + "' and idpospre = '" + ldat_Tira.Rows[index]["IdPosPre2"].ToString().Trim() + "'  and LEFT(idprograma, 4) = year(getdate()) order by idprograma desc");
                            //ldat_Reservas = reservas.ConsultarReservasDetallado(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, ldat_Tira.Rows[index]["IdCuentaContable2"].ToString().Trim(), string.Empty, ldat_Tira.Rows[index]["IdPosPre2"].ToString().Trim(), string.Empty, string.Empty, string.Empty, string.Empty);
                            if (ldat_Reservas.Tables[0].Rows.Count != 0)
                            {
                                DataView dv = ldat_Reservas.Tables[0].DefaultView;
                                dv.Sort = "OrdenDeudaInterna ASC";

                                lds_Datos.Columns.Add("IdReserva");
                                lds_Datos.Columns.Add("OrdenDeudaInterna");
                                lds_Datos.Columns.Add("IdPosPre");
                                lds_Datos.Columns.Add("Posicion");
                                lds_Datos.Columns.Add("Monto");

                                foreach (DataRow drForm in dv.ToTable().Rows)
                                {
                                    //if (drForm["IdMoneda"].ToString().Trim().Equals(ldat_AsientoPago.Rows[0]["CodigoAuxiliar"].ToString().Trim()))
                                    if (!drForm["OrdenDeudaInterna"].ToString().Equals(string.Empty) && !drForm["OrdenDeudaInterna"].ToString().Equals("0"))
                                    {
                                        lstr_Monto = wsDeudaInterna.ConsultaMontoReservaSAP(drForm["IdReserva"].ToString().Trim(), drForm["Posicion"].ToString().Trim());
                                        lds_Datos.Rows.Add(
                                            drForm["IdReserva"].ToString(),
                                            drForm["OrdenDeudaInterna"].ToString(),
                                            drForm["IdPosPre"].ToString(),
                                            drForm["Posicion"].ToString(),
                                            lstr_Monto);
                                        reservasError += "Reserva :" + drForm["IdPosPre"].ToString().Trim() + ", Posición: " + drForm["Posicion"].ToString().Trim() + "\n";
                                        ldec_MontoTotal += Convert.ToDecimal(lstr_Monto);
                                    }
                                }

                                if (Convert.ToDecimal(ldec_MontoTotal) >= (ldec_monto * ldec_TipoCambio))
                                {
                                    //Genera el asiento
                                    decimal ldec_Saldo = ldec_monto;

                                    foreach (DataRow drForm in lds_Datos.Rows)
                                    {
                                        if (Convert.ToDecimal(drForm["Monto"]) > 0 && ldec_Saldo >= 0)
                                        {
                                            //lstr_Referencia = lstr_NroValor.Trim().Equals("00") ? "No Asociado" : lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Costo Transaccion";

                                            ldat_Asiento.Rows.Add(
                                                lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                                ldt_FchVencimiento.ToString("dd.MM.yyyy"),
                                                ldat_Tira.Rows[index]["IdCuentaContable2"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdClaveContable2"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim(),
                                                lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim(),
                                                ldat_Tira.Rows[index]["IdCentroCosto2"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdCentroBeneficio2"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdElementoPEP2"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdPosPre2"].ToString().Trim().ToUpper(),
                                                ldat_Tira.Rows[index]["IdCentroGestor2"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdFondo2"].ToString().Trim(),
                                                drForm["IdReserva"].ToString().Trim(),
                                                drForm["Posicion"].ToString().Trim(),
                                                Truncate(ldec_Saldo > Convert.ToDecimal(drForm["Monto"].ToString()) ? Convert.ToDecimal(drForm["Monto"].ToString()) : ldec_Saldo, 2),
                                                lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                                tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                                lstr_Moneda,//tipo
                                                lstr_Operacion +"."+ lstr_NomOperacion //operacion
                                                );
                                        }

                                        //Resta el saldo    
                                        ldec_Saldo = ldec_Saldo - Convert.ToDecimal(drForm["Monto"]);
                                    }
                                }
                                else
                                {
                                    //Almacena en bitácora de que no lo hizo
                                    bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion, "DI"), "Resultado de Contabilización: \n 1 - [E] Monto superior al total de las reservas de la Deuda Interna. Reservas utilizadas: \n" + reservasError, lstr_Operacion, lstr_NroValor + "-" + lstr_Nemotecnico, "G206");
                                    SinError = false;
                                    break;
                                }
                            }
                            else
                            {
                                if (!ldat_Tira.Rows[index]["IdPosPre2"].ToString().Trim().StartsWith("E"))
                                {
                                    ldat_Asiento.Rows.Add(
                                        lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                        ldt_FchVencimiento.ToString("dd.MM.yyyy"),
                                        ldat_Tira.Rows[index]["IdCuentaContable2"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdClaveContable2"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim(),
                                        lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim(),
                                        ldat_Tira.Rows[index]["IdCentroCosto2"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdCentroBeneficio2"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdElementoPEP2"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdPosPre2"].ToString().Trim().ToUpper(),
                                        ldat_Tira.Rows[index]["IdCentroGestor2"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdFondo2"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["DocPresupuestario2"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["PosDocPresupuestario2"].ToString().Trim(),
                                        Truncate(ldec_monto, 2),
                                        lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                        tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                        lstr_Moneda,//tipo
                                        lstr_Operacion +"."+ lstr_NomOperacion //operacion
                                        );
                                }
                                else
                                {
                                    bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion, "DI"), "Resultado de Contabilización: \n 1 - [E] No hay reservas correspondientes a la cuenta " + ldat_Tira.Rows[index]["IdCuentaContable2"].ToString().Trim() + " con fondo " + ldat_Tira.Rows[index]["IdPosPre2"].ToString().Trim(), lstr_Operacion, lstr_NroValor + "-" + lstr_Nemotecnico, "G206");
                                    SinError = false;
                                    break;
                                }
                            }
                        }
                        #endregion
                    }
                }
                #endregion

                if (SinError)
                {
                    lstr_Mensaje = GenerarAsientoAjuste("Prescripción de Cupón", ldat_Asiento, lstr_Operacion, lstr_NroValor, lstr_Nemotecnico, ldec_TipoCambioColones, ldt_FchModifica);
                    //RAMSES
                    //dinamica.ConsultarDinamico("UPDATE cf.titulosvalores SET Descripcion = 'Prescrito' where NroCupon = 0 and nrovalor = " + Convert.ToInt32(lstr_NroValor.Trim()) + " and nemotecnico = '" + lstr_Nemotecnico.Trim() + "'");
                }

                #endregion
            }

            return lstr_Mensaje;
        }


        public static DataTable RegistroContable()
        {
            DataTable ldat_Asiento = new DataTable();

            try
            {
                ldat_Asiento.Columns.Add("Referencia");
                ldat_Asiento.Columns.Add("Fecha");
                ldat_Asiento.Columns.Add("Cuenta");
                ldat_Asiento.Columns.Add("ClaveContable");
                ldat_Asiento.Columns.Add("Moneda");
                ldat_Asiento.Columns.Add("TextoInfo");
                ldat_Asiento.Columns.Add("CentroCosto");
                ldat_Asiento.Columns.Add("CentroBeneficio");
                ldat_Asiento.Columns.Add("ElementoPEP");
                ldat_Asiento.Columns.Add("PosPre");
                ldat_Asiento.Columns.Add("CentroGestor");
                ldat_Asiento.Columns.Add("Fondo");
                ldat_Asiento.Columns.Add("DocPres");
                ldat_Asiento.Columns.Add("PosDocPres");
                ldat_Asiento.Columns.Add("Monto");
                ldat_Asiento.Columns.Add("PKMovimiento");
                ldat_Asiento.Columns.Add("Texto2");
                ldat_Asiento.Columns.Add("Ref1Tipo");
                ldat_Asiento.Columns.Add("Ref2Operacion");
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return ldat_Asiento;
        }


        public static string GenerarAsientoAjuste(string lstr_TipoCancelacion, DataTable ldat_Asiento, string lstr_IdOperacion, string lstr_NroValor, string lstr_Nemotecnico, decimal ldec_TipoCambio, DateTime ldt_FchModifica)
        {
            //Coleccion de asientos y tipos de asientos requeridos en SAP expuestos por la referencia del servicio
            wrSigafAsientos.ZfiAsiento item_asiento = new wrSigafAsientos.ZfiAsiento();
            wrSigafAsientos.ZfiAsiento[] tabla_asientos = new wrSigafAsientos.ZfiAsiento[ldat_Asiento.Rows.Count];

            //variables de proceso
            string[] item_resAsientosLog = new string[10];
            string logAsiento = string.Empty;
            string flagEstadoAsiento = string.Empty;

            //RAMSES
            DateTime fechaContabilizacion = System.DateTime.Today;

            try
            {
                foreach (DataRow ldr_Row in ldat_Asiento.Rows)
                {
                    item_asiento = new wrSigafAsientos.ZfiAsiento();
                    int index = ldat_Asiento.Rows.IndexOf(ldr_Row);

                    if (index == 0)
                    {
                        item_asiento.Blart = "ID";//Clase de documento
                        item_asiento.Bukrs = "G206";//Sociedad
                        item_asiento.Bldat = ldat_Asiento.Rows[index]["Fecha"].ToString();//Fecha de documento
                        item_asiento.Budat = ldat_Asiento.Rows[index]["Fecha"].ToString();//Fecha de contabilización
                        item_asiento.Bktxt = ldat_Asiento.Rows[index]["Referencia"].ToString();//Referencia
                        //RAMSES
                        fechaContabilizacion = Convert.ToDateTime(ldat_Asiento.Rows[index]["Fecha"].ToString());//Fecha de contabilización
                    }

                    item_asiento.Waers = ldat_Asiento.Rows[index]["Moneda"].ToString();//Moneda 
                    item_asiento.Bschl = ldat_Asiento.Rows[index]["ClaveContable"].ToString();//Clave de contabilización
                    item_asiento.Hkont = ldat_Asiento.Rows[index]["Cuenta"].ToString();//Cuenta de mayor
                    item_asiento.Wrbtr = Convert.ToDecimal(Convert.ToDecimal(ldat_Asiento.Rows[index]["Monto"].ToString()).ToString("0.0000"));//Importe
                    item_asiento.Sgtxt = ldat_Asiento.Rows[index]["TextoInfo"].ToString();//Texto Informativo (50 caracteres)
                    item_asiento.Kostl = ldat_Asiento.Rows[index]["CentroCosto"].ToString();//Centro de Costo
                    item_asiento.Prctr = ldat_Asiento.Rows[index]["CentroBeneficio"].ToString();//Centro de Beneficio
                    item_asiento.Projk = ldat_Asiento.Rows[index]["ElementoPEP"].ToString();//Elemento PEP
                    item_asiento.Fipex = ldat_Asiento.Rows[index]["PosPre"].ToString();//Posición Presupuestaria
                    item_asiento.Fistl = ldat_Asiento.Rows[index]["CentroGestor"].ToString();//Centro Gestor
                    item_asiento.Geber = ldat_Asiento.Rows[index]["Fondo"].ToString();//Fondo
                    item_asiento.Kblnr = ldat_Asiento.Rows[index]["DocPres"].ToString();//Documento Presupuestario
                    item_asiento.Kblpos = ldat_Asiento.Rows[index]["PosDocPres"].ToString();//Posición de documento presupuestario
                    if (ldat_Asiento.Rows[index]["Moneda"].ToString() == "USD")
                        item_asiento.Kursf = Convert.ToDecimal(ldec_TipoCambio.ToString("0.0000"));

                    item_asiento.Xblnr = ldat_Asiento.Rows[index]["PKMovimiento"].ToString();//
                    item_asiento.Bktxt = ldat_Asiento.Rows[index]["Texto2"].ToString();//
                    item_asiento.Xref1Hd = ldat_Asiento.Rows[index]["Ref1Tipo"].ToString();//
                    item_asiento.Xref2Hd = ldat_Asiento.Rows[index]["Ref2Operacion"].ToString();//
                    tabla_asientos[index] = item_asiento;
                }

                //Cargar de Asientos 
                string[] concatenado = new string[8];
                //envio de asiento mediante servicio web hacia SIGAF
                item_resAsientosLog = tasientos.EnviarAsientos(tabla_asientos, "");
                for (int j = 0; j < item_resAsientosLog.Length; j++)
                {
                    int x = j + 1;
                    logAsiento += x + " - " + item_resAsientosLog[j] + " \n ";
                }
                //Registrar en Bitacora de movimientos
                bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_IdOperacion, "DI"), "Resultado de Contabilización: \n" + logAsiento, lstr_IdOperacion, lstr_NroValor + "-" + lstr_Nemotecnico, "");

                string[] a = new string[2];
                if (!logAsiento.Contains("[E]"))
                {
                    string lstr_codAsiento = logAsiento.Substring(logAsiento.IndexOf("BKPFF") + 6, 18);//logAsiento.Length - logAsiento.IndexOf("BKPFF") - 1);
                    string str_CodRes = string.Empty;
                    string str_Msg = string.Empty;
                    /*int int_Consec = 0;
                    int int_Secuencia = 0;*/

                    lcls_CostoTransaccion.ContabilizarCalculosFinancieros(
                        "TitulosValores",
                        null,
                        lstr_NroValor.Trim(),
                        lstr_Nemotecnico.Trim(),
                        "PRE",
                        "SG",
                        ldt_FchModifica, out a[0], out a[1]);
                    //dinamica.ConsultarDinamico(String.Format("UPDATE cf.TitulosValores SET Descripcion = 'Prescrito' WHERE nrocupon={1} AND nrovalor = {2} AND nemotecnico = '{3}'", lstr_NroValor.Trim(), lstr_Nemotecnico.Trim()));
                }
                return logAsiento;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }

}