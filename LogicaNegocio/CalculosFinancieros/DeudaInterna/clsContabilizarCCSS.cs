using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Collections;
using System.Globalization;
using LogicaNegocio.Mantenimiento;
using LogicaNegocio.Seguridad;

namespace LogicaNegocio.CalculosFinancieros.DeudaInterna
{
    public class clsContabilizarCCSS
    {
        //private static ws_SGService.wsSistemaGestor ws_SGService = new ws_SGService.wsSistemaGestor();
        //private static wsAsientos.ServicioContable wsAsientos = new wsAsientos.ServicioContable();
        private static clsTiposAsiento tasientos = new clsTiposAsiento();
        private static Mantenimiento.clsTiposCambio tipocambio = new Mantenimiento.clsTiposCambio();
        private static Mantenimiento.clsPropietarios propietario = new Mantenimiento.clsPropietarios();
        private static Mantenimiento.clsNemotecnicos nemotecnico = new Mantenimiento.clsNemotecnicos();
        private static Mantenimiento.clsTiposAsiento tipoasiento = new Mantenimiento.clsTiposAsiento();
        private static tiras tira = new tiras();
        private static Mantenimiento.clsOperaciones loperacion = new clsOperaciones();
        private static Mantenimiento.clsReservasDetalle reservas = new Mantenimiento.clsReservasDetalle();
        private static Seguridad.tBitacora bitacora = new Seguridad.tBitacora();
        private static clsTituloValor titulo = new clsTituloValor();
        private static clsCostoTransaccion lcls_CostoTransaccion = new clsCostoTransaccion();
        private static Mantenimiento.clsDinamico dinamica = new Mantenimiento.clsDinamico();
        private static string lstr_separador_decimal = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
        //private static wsSG.wsSistemaGestor ws_SistGest = new wsSG.wsSistemaGestor();

        public String ContabilizacionCCSS(String _nemotecnico, Decimal num_valor, Decimal _monto_efectivo_pagado, Decimal _2110201041, Decimal _2110201050, Decimal _2110201070, Decimal _2110201080, Decimal _2110201090, Decimal _2110201100, Decimal _2116421000, Decimal _2116422000, Decimal _2116423000, Decimal _2116424000)
        {
            string lstr_Mensaje = "exito";
            Dictionary<String, Decimal> _cuentas = new Dictionary<String, Decimal>();
            _cuentas.Add("2110201041",_2110201041);
            _cuentas.Add("2110201050",_2110201050);
            _cuentas.Add("2110201070",_2110201070);
            _cuentas.Add("2110201080",_2110201080);
            _cuentas.Add("2110201090",_2110201090);
            _cuentas.Add("2110201100",_2110201100);
            _cuentas.Add("2116421000",_2116421000);
            _cuentas.Add("2116422000",_2116422000);
            _cuentas.Add("2116423000",_2116423000);
            _cuentas.Add("2116424000",_2116424000);  

            try
            {
                //DataSet data = dinamica.ConsultarDinamico("select * from cf.titulosvalores where indicadorcupon = 'V' and nrovalor = " + num_valor + " and nemotecnico = '" + _nemotecnico + "'");
                //DataTable t = data.Tables[0];
                DataTable ldat_TitulosValores = dinamica.ConsultarDinamico("select * from cf.titulosvalores where estado = 'ACT' and indicadorcupon = 'V' and nrovalor = " + num_valor + " and nemotecnico = '" + _nemotecnico + "'").Tables[0];
                
                //DataTable ldat_TitulosValores = new DataTable();
                foreach (DataRow row in ldat_TitulosValores.Rows)
                {
                    lstr_Mensaje = ContabilizarCCSS(
                        row["NemoTecnico"].ToString().Trim(),
                        row["NroValor"].ToString().Trim(),
                        //Convert.ToDateTime(row["FchCancelacion"].ToString().Trim()),
                        Convert.ToDateTime(row["FchValor"].ToString().Trim()),
                        Convert.ToDateTime(row["FchValor"].ToString().Trim()),
                        Convert.ToDateTime(row["FchVencimiento"].ToString().Trim()),
                        Convert.ToDateTime(row["FchModifica"].ToString().Trim()),
                        Convert.ToDecimal(row["ValorFacial"].ToString().Trim()),
                        Convert.ToDecimal(row["ValorTransadoBruto"].ToString().Trim()),
                        Convert.ToDecimal(row["ValorTransadoNeto"].ToString().Trim()),
                        Convert.ToDecimal(row["Premio"].ToString().Trim()),
                        Convert.ToDecimal(row["RendimientoPorDescuento"].ToString().Trim()),
                        Convert.ToDecimal(row["ImpuestoPagado"].ToString().Trim()),
                        row["Tipo"].ToString().Trim(),
                        row["PlazoValor"].ToString().Trim(),
                        row["Moneda"].ToString().Trim(),
                        row["Propietario"].ToString().Trim(),
                        "SINPE: " + row["ModuloSINPE"].ToString().Trim() + "-" + "T.B: " +
                                     row["TasaBruta"].ToString().Trim() + "-" + "T.N: " +
                                     row["TasaNeta"].ToString().Trim() + "-" + "Plazo: " +
                                     row["PlazoValor"].ToString().Trim(),
                        _monto_efectivo_pagado,
                        _cuentas);
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

        public string ContabilizarCCSS(
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
            String lstr_Detalle,
            Decimal _monto_efectivo_pagado,
            Dictionary<String, Decimal> _cuentas)
        {
            string lstr_Mensaje = "00 - Exito";
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

            if (Convert.ToDecimal(lstr_Plazo) <= 360)
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
                    lstr_Operacion = "ID72";
                }
                else
                {
                    lstr_Operacion = "ID71";
                }
            }
            else
            {
                if (es_corto_plazo)
                {
                    lstr_Operacion = "ID70";
                }
                else
                {
                    lstr_Operacion = "ID69";
                }
            }

            DataSet lds_Operaciones = loperacion.ConsultarOperaciones(lstr_Operacion, "IdModulo IN ('DI')", "");
            if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
            {
                lstr_NomOperacion = lds_Operaciones.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
            }

            //Definicion de variables
            String _prima_descuento_excel = tiene_prima ? "PRIMAS" : "IMP_DEV";

            String _plazo = es_corto_plazo ? "CP" : "LP";
            String _propietario = es_publico ? "PUBLICO" : "PRIVADO";

            #endregion

            #region Carga de tiras

            //Tira Capital
            ldat_Tira = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, _propietario, "ID", _plazo, "CAPITAL").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico + "'").CopyToDataTable();
            //Tira Amortizacion
            if (trasciende)
            {
                ldat_Tira.Merge(tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID", "", "AMORT").Tables[0]);
            }
            //Tira 101 o 102 de gasto
            String num_cuenta_contable = _monto_efectivo_pagado > 0 ? "1114910102" : "1114910101";
            ldat_Tira.Merge(tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID", "", "").Tables[0].Select("IdCuentaContable = '" + num_cuenta_contable + "'").CopyToDataTable().Select("CodigoAuxiliar2 = ''").CopyToDataTable());
            //Tira prima descuento
            ldat_Tira.Merge(tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, _propietario, "ID", _plazo, _prima_descuento_excel).Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico + "'").CopyToDataTable());
            //Tira Devengo
            if (!es_cero_cupon)
            {
                ldat_Tira.Merge(tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, _propietario, "ID", _plazo, "INT_DEV").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico + "'").CopyToDataTable());
            }
            //Tira Renta
            else
            {
                ldat_Tira.Merge(tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", _propietario, "ID", "", "RENTA").Tables[0]);
            }
            //Tira Fondo General
            ldat_Tira.Merge(tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID", "", "FG").Tables[0]);
            //Tiras caja
            foreach (KeyValuePair<String, Decimal> kvp in _cuentas)
            {
                ldat_Tira.Merge(tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", null, "", "ID", "", "").Tables[0].Select("IdCuentaContable = '" + kvp.Key + "'").CopyToDataTable());
            }
            

            #endregion

            #region Calculo de montos
            Queue<Decimal> montos = new Queue<Decimal>();

            //Tira Capital
            montos.Enqueue(Math.Round(ldec_ValorFacial, 2));
            //Tira Amortizacion
            if (trasciende)
            {
                montos.Enqueue(Math.Round(es_cero_cupon ? ldec_ValorTransadoBruto : ldec_ValorFacial, 2));
                montos.Enqueue(Math.Round(es_cero_cupon ? ldec_ValorTransadoBruto : ldec_ValorFacial, 2));
            }
            //Tira de gasto con cuentas 101 o 102
            montos.Enqueue(Math.Round(Math.Abs(_monto_efectivo_pagado), 2));
            //Tira prima descuento
            montos.Enqueue(Math.Round(tiene_prima ? Premio : RendimientoXDescuento, 2));
            //Tira Renta
            montos.Enqueue(Math.Round(Math.Abs(ldec_ValorTransadoBruto - ldec_ValorTransadoNeto), 2));
            //Tira Fondo General
            montos.Enqueue(1);
            montos.Enqueue(1);
            //Tiras cajas
            foreach (KeyValuePair<String, Decimal> kvp in _cuentas)
            {
                montos.Enqueue(Math.Round(kvp.Value, 2));
            }

            #endregion

            #region Construccion del asiento

            String lstr_Referencia = lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Pagos a la CCSS";
            Decimal total40 = 0, total50 = 0;
            foreach (DataRow row in ldat_Tira.Rows)
            {
                int cuentasXtira = row["IdCuentaContable2"].ToString().Trim() == "" ? 1 : 2;
                for (int cuenta = 1; cuenta <= cuentasXtira; cuenta++)
                {
                    String num_cuenta = cuenta == 2 ? "2" : "";
                    Decimal monto = montos.Dequeue() * (lstr_Moneda.Contains("UDE") ? ldec_TipoCambioUDE : 1);
                    if (monto > 0)
                    {
                        Hashtable reservas_tabla = cagarReservas(row, num_cuenta, monto * (lstr_Moneda.Contains("USD") ? ldec_TipoCambioColones : 1));
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
                                    tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                    lstr_Moneda,//tipo
                                    lstr_Operacion +"."+lstr_NomOperacion //operacion
                                    );
                            }
                        }
                        else
                        {
                            bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion, "DI"), _msg, lstr_Operacion, lstr_NroValor + "-" + lstr_Nemotecnico, "");
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
            lstr_Mensaje = GenerarAsientoAjuste("Pago a CCSS", ldat_Asiento, lstr_Operacion, lstr_NroValor, lstr_Nemotecnico, ldec_TipoCambioColones, ldt_FchModifica);
            return lstr_Mensaje;
        }

        public static string GenerarAsientoAjuste(string lstr_TipoCancelacion, DataTable ldat_Asiento, string lstr_IdOperacion, string lstr_NroValor, string lstr_Nemotecnico, decimal ldec_TipoCambio, DateTime ldt_FchModifica)
        {
            string lstr_codAsiento = string.Empty;
            //Coleccion de asientos y tipos de asientos requeridos en SAP expuestos por la referencia del servicio
            wrSigafAsientos.ZfiAsiento item_asiento = new wrSigafAsientos.ZfiAsiento();
            wrSigafAsientos.ZfiAsiento[] tabla_asientos = new wrSigafAsientos.ZfiAsiento[ldat_Asiento.Rows.Count];

            //variables de proceso
            string[] item_resAsientosLog = new string[10];
            string logAsiento = string.Empty;
            string flagEstadoAsiento = string.Empty;
            
            //RAMSES
            DateTime ldt_FechaContabiliza = new DateTime();
            DateTime fechaContabilizacion = System.DateTime.Today;
            string lstr_Id = "";
            lstr_Id = lstr_NroValor + '.' + lstr_Nemotecnico;

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
                        ldt_FechaContabiliza = DateTime.ParseExact(ldat_Asiento.Rows[index]["Fecha"].ToString(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
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
                    lstr_codAsiento = item_resAsientosLog[0];
                }
                //Registrar en Bitacora de movimientos
                bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_IdOperacion, "DI"), "Resultado de Contabilización: \n" + logAsiento, lstr_IdOperacion, lstr_NroValor + "-" + lstr_Nemotecnico, "");

                string[] a = new string[2];
                if (!logAsiento.Contains("[E]"))
                {
                    //RAMSES
                    //lstr_codAsiento = logAsiento.Substring(logAsiento.IndexOf("BKPFF") + 6, 18);//logAsiento.Length - logAsiento.IndexOf("BKPFF") - 1);
                    lcls_CostoTransaccion.ContabilizarCalculosFinancieros(
                        "TitulosValores",
                        null,
                        lstr_NroValor.Trim(),
                        lstr_Nemotecnico.Trim(),
                        "CAN",
                        "SG",
                        ldt_FchModifica, out a[0], out a[1]);
                    //RAMSES UDPDATE
                    String fecha_con_formato_sql = String.Format("{0}/{1}/{2}", fechaContabilizacion.Year, fechaContabilizacion.Month, fechaContabilizacion.Day);
                    String QUERY = String.Format("UPDATE cf.TitulosValores SET asiento = '{0}', FchContablilizado = '{1}' WHERE nrovalor = {2} AND nemotecnico = '{3}'", lstr_codAsiento, fecha_con_formato_sql, lstr_NroValor.Trim(), lstr_Nemotecnico.Trim());
                    dinamica.ConsultarDinamico(QUERY);
                }
                return logAsiento;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }



        public Hashtable cagarReservas(DataRow tira, String cuenta, Decimal monto)
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

            ldat_Reservas = dinamica.ConsultarDinamico("select * from ma.reservasdetalle where idcuentacontable = '" + tira["IdCuentaContable" + cuenta].ToString().Trim() + "' and idpospre = '" + tira["IdPosPre" + cuenta].ToString().Trim() + "' and LEFT(idprograma, 4) = year(getdate()) order by idprograma desc");

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
                            Decimal monto_parcial = monto;
                            lstr_Monto = wsDeudaInterna.ConsultaMontoReservaSAP(drForm["IdReserva"].ToString().Trim(), drForm["Posicion"].ToString().Trim());
                            if (Convert.ToDecimal(lstr_Monto.Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal)) > 0)
                            {
                                if (Convert.ToDecimal(lstr_Monto.Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal)) < monto)
                                {
                                    monto_parcial = Convert.ToDecimal(lstr_Monto.Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal));
                                    monto -= Convert.ToDecimal(lstr_Monto.Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal));
                                }
                                _reserva.Enqueue(drForm["IdReserva"].ToString().Trim());
                                _posicion.Enqueue(drForm["Posicion"].ToString().Trim());
                                _monto.Enqueue(monto_parcial);
                                reservasError += "Posición Presupuestaria: " + drForm["IdPosPre"].ToString().Trim() + "Reserva :" + drForm["IdReserva"].ToString().Trim() + ", Posición: " + drForm["Posicion"].ToString().Trim() + "\n";
                                if (monto == monto_parcial)
                                {
                                    monto = 0;
                                    break;
                                }
                            }
                        }
                    }
                    if (monto > 0)
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
                _monto.Enqueue(monto);
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