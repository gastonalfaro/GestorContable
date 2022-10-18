using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Globalization;
using System.Collections;
using LogicaNegocio.Mantenimiento;
using LogicaNegocio.Seguridad;

namespace LogicaNegocio.CalculosFinancieros.DeudaInterna
{
    public class clsContabilizarCancelacionAnticipada
    {
        //private static wsDeudaInterna.wsDeudaInterna wsDeudaInterna = new wsDeudaInterna.wsDeudaInterna();
        //private static ws_SGService.wsSistemaGestor ws_SGService = new ws_SGService.wsSistemaGestor();
        //private static wsAsientos.ServicioContable wsAsientos = new wsAsientos.ServicioContable();
        private static clsTiposAsiento tasientos = new clsTiposAsiento();
        private static Mantenimiento.clsTiposCambio tipocambio = new Mantenimiento.clsTiposCambio();
        private static Mantenimiento.clsPropietarios propietario = new Mantenimiento.clsPropietarios();
        private static Mantenimiento.clsNemotecnicos nemotecnico = new Mantenimiento.clsNemotecnicos();
        private static Mantenimiento.clsTiposAsiento tipoasiento = new Mantenimiento.clsTiposAsiento();
        private static Mantenimiento.clsOperaciones loperacion = new clsOperaciones();
        private static Mantenimiento.clsReservasDetalle reservas = new Mantenimiento.clsReservasDetalle();
        private static Seguridad.tBitacora bitacora = new Seguridad.tBitacora();
        private static clsTituloValor titulo = new clsTituloValor();
        private static tiras tira = new tiras();
        private static clsCostoTransaccion lcls_CostoTransaccion = new clsCostoTransaccion();
        private static Mantenimiento.clsDinamico dinamica = new Mantenimiento.clsDinamico();
        private static string lstr_separador_decimal = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;

        private decimal Truncate(decimal value, int length)
        {
            return Math.Truncate(value * 100) / 100;
        }

        public string Cancelacion(String _nemotecnico, Decimal num_valor, DateTime fecha_cancelacion, Decimal valor_facial, Decimal transado_bruto, Decimal rendimiento_descuento, Decimal premio, Decimal impuesto_pagado)
        {
            string lstr_Mensaje = String.Empty;
            try
            {
                string query = "select * from cf.titulosvalores "+ 
                "where indicadorcupon = 'V' and nrovalor = " + num_valor + " and nemotecnico = '" + _nemotecnico + "'";

                DataSet lds_TitulosValores = dinamica.ConsultarDinamico(query);
                DataTable ldat_TitulosValores = lds_TitulosValores.Tables[0];
                if (ldat_TitulosValores.Rows.Count > 0)
                {
                    foreach (DataRow fila in ldat_TitulosValores.Rows)
                    {

                        lstr_Mensaje = CancelarTituloValorAnticipado(
                            fila["NemoTecnico"].ToString(),
                            fila["NroValor"].ToString(),
                            Convert.ToDateTime(fecha_cancelacion),
                            Convert.ToDateTime(fila["FchValor"].ToString()),
                            Convert.ToDateTime(fila["FchVencimiento"].ToString()),
                            Convert.ToDateTime(fila["FchModifica"].ToString()),
                            Convert.ToDecimal(fila["ValorFacial"].ToString()),
                            Convert.ToDecimal(fila["ValorTransadoBruto"].ToString()),
                            Convert.ToDecimal(fila["ValorTransadoNeto"].ToString()),
                            Convert.ToDecimal(fila["Premio"].ToString()),
                            Convert.ToDecimal(fila["RendimientoPorDescuento"].ToString()),
                            Convert.ToDecimal(fila["ImpuestoPagado"].ToString()),
                            fila["Tipo"].ToString(),
                            fila["PlazoValor"].ToString(),
                            fila["Moneda"].ToString(),
                            fila["Propietario"].ToString(),
                            fila["ModuloSINPE"].ToString(),
                            "SINPE: " + fila["ModuloSINPE"].ToString().Trim() + "-" + "T.B: " +
                                        fila["TasaBruta"].ToString().Trim() + "-" + "T.N: " +
                                        fila["TasaNeta"].ToString().Trim() + "-" + "Plazo: " +
                                        fila["PlazoValor"].ToString().Trim(),
                            valor_facial, transado_bruto, rendimiento_descuento, premio, impuesto_pagado
                            );
                    }
                }
                else
                {
                    lstr_Mensaje = string.Format("Resultado de Contabilización:\n 1 - [E] Aún no se han cargado títulos asociados al nemotécnico: {0} con número de valor: {1}", _nemotecnico, num_valor);
                    //Registrar en Bitacora de movimientos
                    bitacora.ufnRegistrarAccionBitacora("DI", "123", "Cancelacion Anticipada Titulo", lstr_Mensaje, "", num_valor + "-" + _nemotecnico, "");
                }
            }
            catch (Exception ex)
            {
                lstr_Mensaje = "Error " + ex.ToString();
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

        public string CancelarTituloValorAnticipado(
            String lstr_Nemotecnico,
            String lstr_NroValor,
            DateTime ldt_FchCancelacion,
            DateTime ldt_FchValor,
            DateTime ldt_FchVencimiento,
            DateTime ldt_FchModifica,
            Decimal ldec_ValorFacial,
            Decimal ldec_ValorTransadoBruto,
            Decimal ldec_ValorTransadoNeto,
            Decimal Premio,
            Decimal RendimientoXDescuento,
            Decimal ImpuestoPagado,
            String lstr_Tipo,
            String lstr_Plazo,
            String lstr_Moneda,
            String lstr_Propietario,
            String lstr_Origen,
            String lstr_Detalle,
            Decimal xls_ValorFacial,
            Decimal xls_TransadoBruto,
            Decimal xls_RendimientoXDescuento,
            Decimal xls_Premio,
            Decimal xls_ImpuestoPagado
            )
        {
            string lstr_Mensaje = "exito";
            //wsDeudaInterna.wsDeudaInterna wsDeudaInterna = new wsDeudaInterna.wsDeudaInterna();
            DataTable ldat_Asiento = RegistroContable();
            DataTable ldat_Tira = new DataTable();
            Boolean es_publico = false, trasciende = false, tiene_prima = false, es_corto_plazo = false, es_cero_cupon = false;
            String lstr_Operacion = string.Empty;
            String lstr_NomOperacion = string.Empty;
            Decimal ldec_TipoCambioColones = Convert.ToDecimal(tipocambio.ConsultarTiposCambio("CRCN", ldt_FchValor, "3140", "N").Tables[0].Rows[0]["Valor"].ToString());
            Decimal ldec_TipoCambioUDE = Convert.ToDecimal(tipocambio.ConsultarTiposCambio("UDE", ldt_FchValor, "", "N").Tables[0].Rows[0]["Valor"].ToString());
            #region Definicion de banderas

            if (propietario.ConsultarPropietarios(string.Empty, string.Empty, string.Empty, lstr_Propietario, "S").Tables[0].Rows.Count != 0)
            {
                es_publico = true;
            }

            if (ldt_FchValor.Year != ldt_FchVencimiento.Year)
            {
                trasciende = true;
            }


            if (ldec_ValorFacial < ldec_ValorTransadoBruto)
            {
                tiene_prima = true;
            }

            // En Rde se maneja en años, en Rdi en días, por ello la validación
            if (Convert.ToDecimal(lstr_Plazo) <= (lstr_Origen.ToUpper().Equals("RDE") ? 1 : 360))
            {
                es_corto_plazo = true;
            }

            if (lstr_Tipo.ToLower().Contains("cero"))
            {
                es_cero_cupon = true;
            }

            if (lstr_Moneda.Equals("USD"))
            {
                if (es_corto_plazo)
                {
                    lstr_Operacion = "ID47";
                }
                else
                {
                    lstr_Operacion = "ID48";
                }
            }
            else
            {
                if (es_corto_plazo)
                {
                    lstr_Operacion = "ID45";
                }
                else
                {
                    lstr_Operacion = "ID46";
                }
            }
            //No retorna nada
            DataSet lds_Operaciones = loperacion.ConsultarOperaciones(lstr_Operacion, "IdModulo IN ('DI')", "");
            if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
            {
                lstr_NomOperacion = lds_Operaciones.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
            }

            //Caso PT
            String _descuento = "DES";
            String _prima = "PRI";
            String _terminacion = "_T";
            if (es_corto_plazo)
            {
                if (lstr_Nemotecnico.Contains("PT"))
                {
                    _terminacion = "_PT";
                }
                else if (!trasciende)
                {
                    _terminacion = "_NT";
                }
            }

            //Definicion de variables
            String _prima_descuento = tiene_prima ? _prima + _terminacion : _descuento + _terminacion;
            String _prima_descuento_excel = tiene_prima ? "PRIMAS" : "IMP_DEV";
            String _devengo = "INT_DEV_" + (tiene_prima ? "P" : "D");

            String _plazo = es_corto_plazo ? "CP" : "LP";
            String _propietario = es_publico ? "PUBLICO" : "PRIVADO";

            #endregion

            #region Carga de tiras
            //Error
            //Tira Capital
            ldat_Tira = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, _propietario, "ID", _plazo, "CAPITAL").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico + "'").CopyToDataTable();
            //Tira Amortizacion
            if (trasciende && _prima_descuento.Contains("_T"))
            {
                ldat_Tira.Merge(tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID", _plazo, "AMORT").Tables[0]);
            }
            //Tira prima descuento del titulo y excel
            ldat_Tira.Merge(tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID", _plazo, _prima_descuento).Tables[0]);
            //Tira prima descuento del excel
            ldat_Tira.Merge(tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, _propietario, "ID", _plazo, _prima_descuento_excel).Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico + "'").CopyToDataTable());
            //Tira Devengo
            if (!es_cero_cupon)
            {
                ldat_Tira.Merge(tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, _propietario, "ID", _plazo, _devengo).Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico + "'").CopyToDataTable());
            }
            //Tira Renta
            else
            {
                ldat_Tira.Merge(tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", _propietario, "ID", "", "RENTA").Tables[0]);
            }   

            #endregion

            #region Calculo de montos
            Queue<Decimal> montos = new Queue<Decimal>();
            Decimal devengo_cupon = obtenerDevengo(lstr_Nemotecnico, lstr_NroValor, ldt_FchCancelacion, es_cero_cupon, "cupon");
            Decimal devengo_descuento = obtenerDevengo(lstr_Nemotecnico, lstr_NroValor, ldt_FchCancelacion, es_cero_cupon, "descuento");
            Decimal devengo_interes = obtenerDevengo(lstr_Nemotecnico, lstr_NroValor, ldt_FchCancelacion, es_cero_cupon, "interestotal");
            //Tira Capital

            montos.Enqueue(Math.Round(ldec_ValorFacial, 2));
            montos.Enqueue(Math.Round(xls_ValorFacial + (es_cero_cupon ? 0 : xls_RendimientoXDescuento), 2));
            //Tira Amortizacion
            if (trasciende && _prima_descuento.Contains("_T"))
            {
                montos.Enqueue(Math.Round(es_cero_cupon ? xls_TransadoBruto : xls_ValorFacial, 2));
                montos.Enqueue(Math.Round(es_cero_cupon ? xls_TransadoBruto : xls_ValorFacial, 2));
            }
            //Tira prima descuento del titulo y excel
            montos.Enqueue(Math.Round(tiene_prima ? xls_Premio : xls_RendimientoXDescuento, 2));
            montos.Enqueue(Math.Round(tiene_prima ? xls_Premio : xls_RendimientoXDescuento, 2));
            montos.Enqueue(Math.Round(Math.Abs(tiene_prima ? xls_Premio : xls_RendimientoXDescuento - devengo_interes), 2));
            //Tira prima descuento del excel
            montos.Enqueue(Math.Round(Math.Abs(tiene_prima ? Premio : RendimientoXDescuento - devengo_descuento), 2));
            //Tira Devengo
            if (!es_cero_cupon)
            {
                montos.Enqueue(Math.Round(devengo_cupon, 2));
            }
            //Tira Renta
            else
            {
                montos.Enqueue(Math.Round(Math.Abs(xls_ImpuestoPagado - ImpuestoPagado), 2));
            }

            #endregion

            #region Construccion del asiento

            String lstr_Referencia = lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Cancelación Anticipada";
            Decimal total40 = 0, total50 = 0;
            foreach (DataRow row in ldat_Tira.Rows)
            {
                int cuentasXtira = row["IdClaveContable2"].ToString().Trim() == "" ? 1 : 2;
                for (int cuenta = 1; cuenta <= cuentasXtira; cuenta++)
                {
                    String num_cuenta = cuenta == 2 ? "2" : "";
                    Decimal monto = montos.Dequeue();
                    if (monto > 0)
                    {
                        Hashtable reservas_tabla = cagarReservas(row, num_cuenta, (monto * (lstr_Moneda.Contains("USD") ? ldec_TipoCambioColones : 1)), (lstr_Moneda.Contains("USD") ? ldec_TipoCambioColones : 1));
                        Queue<String> _reserva = (Queue<String>)(reservas_tabla["reserva"]);
                        Queue<String> _posicion = (Queue<String>)(reservas_tabla["posicion"]);
                        Queue<Decimal> _monto = (Queue<Decimal>)(reservas_tabla["monto"]);
                        String _msg = reservas_tabla["msg"].ToString();
                        if (_msg.Contains("exito"))
                        {
                            while (_reserva.Count > 0)
                            {
                                ldat_Asiento.Rows.Add(
                                    lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                    ldt_FchCancelacion.ToString("dd.MM.yyyy"),
                                    row["IdCuentaContable" + num_cuenta].ToString().Trim(),
                                    row["IdClaveContable" + num_cuenta].ToString().Trim(),
                                    row["CodigoAuxiliar"].ToString().Trim(),
                                    lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim(),
                                    row["IdCentroCosto" + num_cuenta].ToString().Trim(),
                                    row["IdCentroBeneficio" + num_cuenta].ToString().Trim(),
                                    row["IdElementoPEP" + num_cuenta].ToString().Trim(),
                                    row["IdPosPre" + num_cuenta].ToString().Trim(),
                                    row["IdCentroGestor" + num_cuenta].ToString().Trim(),
                                    row["IdFondo" + num_cuenta].ToString().Trim(),
                                    _reserva.Dequeue(),
                                    _posicion.Dequeue(),
                                    _monto.Dequeue() / (lstr_Moneda.Contains("USD") ? ldec_TipoCambioColones : 1),
                                    lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                    tira.get_operation_name(lstr_Operacion, "DI"),//texto2 TODO: cambiar por tira.get_operation_name(lstr_IdOperacion, "DI")
                                    lstr_Moneda,//tipo
                                    lstr_Operacion +"."+ lstr_NomOperacion//operacion
                                    );
                            }
                        }
                        else
                        {
                            return _msg;
                        }
                        if (row["IdClaveContable" + num_cuenta].ToString().Trim().Contains("40"))
                        {
                            total40 += monto;
                        }
                        else
                        {
                            total50 += monto;
                        }
                    }
                }
            }
            decimal diff_total = total40 - total50;
            if (Math.Abs(diff_total) < 10)
            {
                for (int row = ldat_Asiento.Rows.Count - 1; row >= 0; row--)
                {
                    if (ldat_Asiento.Rows[row]["ClaveContable"].ToString().Trim().Contains("50"))
                    {
                        ldat_Asiento.Rows[row]["Monto"] = Convert.ToDecimal(ldat_Asiento.Rows[row]["Monto"]) + diff_total;
                        break;
                    }
                }
            }
            #endregion
            //Envio de asiento
            lstr_Mensaje = GenerarAsientoAjuste("Cancelacion Anticipada Titulo", ldat_Asiento, lstr_Operacion, lstr_NroValor, lstr_Nemotecnico, ldec_TipoCambioColones, ldt_FchModifica);
            return lstr_Mensaje;
        }

        public Decimal obtenerDevengo(String nemotecnico, String num_valor, DateTime fecha, Boolean es_cero_cupon, String columna)
        {
            
            try
            {                
                String comp_fch = "";
                Decimal result;
                if (!es_cero_cupon)
                {
                    comp_fch = " and CONVERT(datetime, periodo, 103) > '";
                    String query_fecha_ultimo_pago = "select top 1 convert(varchar(10),CONVERT(datetime, periodo, 103),101) as periodo from cf.calculosflujoefectivo where nemotecnico = '" + nemotecnico + "' and nrovalor = " + num_valor + "  and CONVERT(datetime, periodo, 103) < '" + fecha.Month + "/" + fecha.Day + "/" + fecha.Year + "' order by  CONVERT(datetime, periodo, 103) desc";
                    comp_fch += dinamica.ConsultarDinamico(query_fecha_ultimo_pago).Tables[0].Rows[0]["periodo"].ToString() + "'";
                }                
                String filtro_fecha = " and CONVERT(datetime, periodo, 103) < '" + fecha.Month + "/" + fecha.Day + "/" + fecha.Year + "'" + comp_fch;
                String sum_meses_completos = "select isnull(abs(sum(" + columna + ")),0) as devengo from cf.devengosmensuales where nemotecnico = '" + nemotecnico + "' and nrovalor = " + num_valor + filtro_fecha;
                String devengo_dias = "select isnull(abs(sum(" + columna + "/diasperiodo*(" + fecha.Day + "))),0) as devengo from cf.devengosmensuales where nemotecnico = '" + nemotecnico + "' and nrovalor = " + num_valor + " and year(CONVERT(datetime, periodo, 103)) = " + fecha.Year + " and month(CONVERT(datetime, periodo, 103)) = " + fecha.Month + "";
                result = Convert.ToDecimal(dinamica.ConsultarDinamico(sum_meses_completos).Tables[0].Rows[0]["devengo"]);
                result += Convert.ToDecimal(dinamica.ConsultarDinamico(devengo_dias).Tables[0].Rows[0]["devengo"]);
                return result;
            }
            catch (Exception ex)
            {
                string lstr_Mensaje = string.Format("Resultado de Contabilización:\n 1 - [E] Aún no se calculado el devengo asociado al nemotécnico: {0} con número de valor: {1}", nemotecnico, num_valor);
                //Registrar en Bitacora de movimientos
                bitacora.ufnRegistrarAccionBitacora("DI", "123", "Cancelacion Anticipada Titulo", lstr_Mensaje, "", num_valor + "-" + nemotecnico, "");
                throw;
            }
        }

        public static string GenerarAsientoAjuste(string lstr_TipoCancelacion, DataTable ldat_Asiento, string lstr_IdOperacion, string lstr_NroValor, string lstr_Nemotecnico, decimal ldec_TipoCambio, DateTime ldt_FchModifica)
        {
            //Coleccion de asientos y tipos de asientos requeridos en SAP expuestos por la referencia del servicio
            wrSigafAsientos.ZfiAsiento item_asiento = new wrSigafAsientos.ZfiAsiento();
            wrSigafAsientos.ZfiAsiento[] tabla_asientos = new wrSigafAsientos.ZfiAsiento[ldat_Asiento.Rows.Count];

            //variables de proceso
            string lstr_codAsiento = string.Empty;
            string[] item_resAsientosLog = new string[10];
            string logAsiento = string.Empty;
             DateTime fechaContabilizacion = System.DateTime.Today;
            string flagEstadoAsiento = string.Empty;

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
                    }

                    item_asiento.Waers = ldat_Asiento.Rows[index]["Moneda"].ToString();//Moneda 
                    item_asiento.Bschl = ldat_Asiento.Rows[index]["ClaveContable"].ToString();//Clave de contabilización
                    item_asiento.Hkont = ldat_Asiento.Rows[index]["Cuenta"].ToString();//Cuenta de mayor
                    item_asiento.Wrbtr = Convert.ToDecimal(Convert.ToDecimal(ldat_Asiento.Rows[index]["Monto"].ToString()).ToString("0.0000"));//Importe
                    item_asiento.Sgtxt = ldat_Asiento.Rows[index]["TextoInfo"].ToString();//Texto Informativo (50 caracteres)
                    item_asiento.Kostl = ldat_Asiento.Rows[index]["CentroCosto"].ToString();//Centro de Costo
                    item_asiento.Prctr = ldat_Asiento.Rows[index]["CentroBeneficio"].ToString();//Centro de Beneficio
                    item_asiento.Projk = ldat_Asiento.Rows[index]["ElementoPEP"].ToString();//Elemento PEP
                    item_asiento.Fipex = ldat_Asiento.Rows[index]["PosPre"].ToString().ToUpper();//Posición Presupuestaria
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
                    lstr_codAsiento = item_resAsientosLog[0];
                } 
                //Registrar en Bitacora de movimientos
                bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_IdOperacion, "DI"), "Resultado de Contabilización: \n" + logAsiento, lstr_IdOperacion, lstr_NroValor + "-" + lstr_Nemotecnico, "");

                string[] a = new string[2];
                if (!logAsiento.Contains("[E]"))
                {

                    //CHANTO
                    String fecha_con_formato_sql = String.Format("{0}/{1}/{2}", fechaContabilizacion.Year, fechaContabilizacion.Month, fechaContabilizacion.Day);
                    String QUERY = String.Format("UPDATE cf.TitulosValores SET asiento = '{0}', FchContablilizado = '{1}' WHERE nrovalor = {2} AND nemotecnico = '{3}'", lstr_codAsiento, fecha_con_formato_sql, lstr_NroValor.Trim(), lstr_Nemotecnico.Trim());
                    dinamica.ConsultarDinamico(QUERY);
                    /*
                        lcls_CostoTransaccion.ContabilizarCalculosFinancieros(
                            "TitulosValores",
                            null,
                            lstr_NroValor.Trim(),
                            lstr_Nemotecnico.Trim(),
                            "CAN",
                            "SG",
                            ldt_FchModifica, out a[0], out a[1]);*/
                }
                return logAsiento;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public Hashtable cagarReservas(DataRow tira, String cuenta, Decimal montoColones, Decimal tipoCambio)
        {
            //Validar que el pos pre sea diferente de PP_Balance y que el monto sea mayor al seleccionado
            string lstr_Monto = string.Empty;
            DataTable lds_Datos = new DataTable();
            decimal ldec_MontoTotal = 0;
            string reservasError = "";
            string lstr_NuevoPosPrePago = string.Empty;
            DataSet ldat_Reservas = new DataSet();
            wsDeudaInterna.wsDeudaInterna wsDeudaInterna = new wsDeudaInterna.wsDeudaInterna();

            Queue<String> _reserva = new Queue<String>();
            Queue<String> _posicion = new Queue<String>();
            Queue<Decimal> _monto = new Queue<Decimal>();
            String _msg = "exito";

            Hashtable result = new Hashtable();

            ldat_Reservas = dinamica.ConsultarDinamico("select * from ma.reservasdetalle where idcuentacontable = '" + tira["IdCuentaContable" + cuenta].ToString().Trim() + "' and idpospre = '" + tira["IdPosPre" + cuenta].ToString().Trim() + "'  and LEFT(idprograma, 4) = year(getdate()) order by idprograma desc");

            #region Con Reserva
            if (tira["IdPosPre" + cuenta].ToString().Trim().StartsWith("E"))
            {
                if (ldat_Reservas.Tables[0].Rows.Count != 0)
                {
                    DataView dv = ldat_Reservas.Tables[0].DefaultView;
                    dv.Sort = "OrdenDeudaInterna ASC";

                    foreach (DataRow drForm in dv.ToTable().Rows)
                    {
                        if (!drForm["OrdenDeudaInterna"].ToString().Equals(string.Empty) && !drForm["OrdenDeudaInterna"].ToString().Equals("0"))
                        {
                            Decimal monto_parcial = montoColones;
                            Decimal monto_parcial_moneda = montoColones / tipoCambio;

                            lstr_Monto = wsDeudaInterna.ConsultaMontoReservaSAP(drForm["IdReserva"].ToString().Trim(), drForm["Posicion"].ToString().Trim());
                            if (Convert.ToDecimal(lstr_Monto.Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal)) > 0)
                            {
                                if (Convert.ToDecimal(lstr_Monto.Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal)) < montoColones)
                                {
                                    monto_parcial = Convert.ToDecimal(lstr_Monto.Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal));
                                    monto_parcial_moneda = Truncate(monto_parcial / tipoCambio , 2);

                                    //montoColones = montoColones - Convert.ToDecimal(lstr_Monto.Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal));
                                    montoColones = montoColones - (monto_parcial_moneda * tipoCambio);
                                }
                                _reserva.Enqueue(drForm["IdReserva"].ToString().Trim());
                                _posicion.Enqueue(drForm["Posicion"].ToString().Trim());
                                _monto.Enqueue( monto_parcial_moneda * tipoCambio );
                                //_monto.Enqueue(monto_parcial);
                                reservasError += "Posición Presupuestaria: " + drForm["IdPosPre"].ToString().Trim() + "Reserva :" + drForm["IdReserva"].ToString().Trim() + ", Posición: " + drForm["Posicion"].ToString().Trim() + "\n";
                                if (montoColones == monto_parcial)
                                {
                                    montoColones = 0;
                                    break;
                                }
                            }
                        }
                    }
                    if (montoColones > 0)
                    {
                        _msg = "Resultado de Contabilización: \n 1 - [E] Monto superior al total de las reservas de la Deuda Interna. Reservas utilizadas: \n" + reservasError;
                    }
                }
                else
                {
                    _msg = "Resultado de Contabilización: \n 1 - [E] No hay reservas correspondientes a la cuenta " + tira["IdCuentaContable" + cuenta].ToString().Trim() + " con fondo " + tira["IdPosPre" + cuenta].ToString().Trim();
                }
            }
            #endregion

            #region Sin Reserva
            else
            {
                _reserva.Enqueue(tira["DocPresupuestario" + cuenta].ToString().Trim());
                _posicion.Enqueue(tira["PosDocPresupuestario" + cuenta].ToString().Trim());
                _monto.Enqueue(montoColones);
            }
            #endregion

            result.Add("msg", _msg);
            result.Add("reserva", _reserva);
            result.Add("posicion", _posicion);
            result.Add("monto", _monto);
            return result;
        }
    }
}