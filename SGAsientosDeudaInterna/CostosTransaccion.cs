using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGAsientosDeudaInterna
{
    public class CostosTransaccion
    {
        private static wsDeudaInterna.wsDeudaInterna wsDeudaInterna = new wsDeudaInterna.wsDeudaInterna();
        private static ws_SGService.wsSistemaGestor ws_SGService = new ws_SGService.wsSistemaGestor();
        private static wsAsientos.ServicioContable wsAsientos = new wsAsientos.ServicioContable();


        public string GeneraCostosTransaccion() 
        {
            string lstr_Mensaje = string.Empty;

            try
            {
                DataSet vCostoTransaccion = wsDeudaInterna.ConsultarCostoTransaccion(null, null, null, null, null);

                foreach (DataTable table in vCostoTransaccion.Tables)
                    foreach (DataRow dr_Costos in table.Rows)
                    {
                        if (!dr_Costos["Estado"].ToString().Trim().Equals("C"))
                          lstr_Mensaje = PrepararContabilizacionCosto(
                                Convert.ToDecimal(dr_Costos["Monto"].ToString()),
                                dr_Costos["NemoTecnico"].ToString(),
                                dr_Costos["IdMoneda"].ToString(),
                                dr_Costos["NroValor"].ToString(),
                                dr_Costos["Detalle"].ToString(),
                                dr_Costos["Fecha"].ToString(),
                                Convert.ToDecimal(dr_Costos["TpoCambio"].ToString()),
                                Convert.ToInt32(dr_Costos["IdCostoTransaccion"].ToString()),
                                Convert.ToDateTime(dr_Costos["FchModifica"].ToString()));
                    }
            }
            catch (Exception ex) { lstr_Mensaje = ex.ToString(); }
            return lstr_Mensaje;
        }

        public string PrepararContabilizacionCosto(decimal ldec_monto,
                                                string lstr_Nemotecnico,
                                                string lstr_Moneda,
                                                string lstr_NroValor,
                                                string lstr_Detalle,
                                                string FchContable,
                                                decimal ldec_TpoCambio,
                                                int lint_IdCostoTransaccion,
                                                DateTime ldt_FchModifica)
        {
            string lstr_IdOperacion1 = string.Empty;
            string lstr_IdOperacion2 = string.Empty;
            string lstr_Mensaje = string.Empty;
            DataTable ldat_Asiento = new DataTable();
            DataTable ldat_AsientoDevengo = new DataTable();
            DataTable ldat_AsientoPago = new DataTable();
            DataSet ldat_Reservas = new DataSet();
            DataTable ldat_TituloValor = new DataTable();
            string FchCont = string.Empty;
            string lstr_NemoInfo = lstr_Nemotecnico;

            try
            {
                #region costo transaccion
                //determina si el costo por transacción está asociado a un título
                //cucurucho solo tenia 7 parametros
                //ldat_TituloValor = wsDeudaInterna.ConsultarTitulosValores(lstr_NroValor, lstr_Nemotecnico, "%", "%", "%", "01/01/1900", "01/01/5000").Tables[0];
                ldat_TituloValor = wsDeudaInterna.ConsultarTitulosValores(lstr_NroValor, lstr_Nemotecnico, "%", "%", "%", "%", "%", "%", "01/01/1900", "01/01/5000").Tables[0];
                if (ldat_TituloValor.Rows.Count != 0)
                {
                    switch (lstr_Moneda)
                    {
                        case "USD":
                            {
                                lstr_IdOperacion1 = "ID02";
                                lstr_IdOperacion2 = "ID04";
                                break;
                            }
                        case "CRCN":
                            {
                                lstr_Moneda = "CRC";
                                lstr_IdOperacion1 = "ID01";
                                lstr_IdOperacion2 = "ID03";
                                break;
                            }
                        case "UDE":
                            {
                                lstr_Moneda = "CRC";
                                lstr_IdOperacion1 = "ID01";
                                lstr_IdOperacion2 = "ID03";
                                break;
                            }
                    }
                }
                else
                {
                    switch (lstr_Moneda)
                    {
                        case "USD":
                            {
                                lstr_IdOperacion1 = "ID06";
                                lstr_Nemotecnico = "";
                                break;
                            }
                        case "CRCN":
                            {
                                lstr_Moneda = "CRC";
                                lstr_IdOperacion1 = "ID05";
                                lstr_Nemotecnico = "";
                                break;
                            }
                        case "UDE":
                            {
                                lstr_Moneda = "CRC";
                                lstr_IdOperacion1 = "ID05";
                                lstr_Nemotecnico = "";
                                break;
                            }
                    }
                }

                if (ldat_TituloValor.Rows.Count != 0)
                {
                    //ldat_AsientoDevengo = ws_SGService.uwsConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_IdOperacion1, "", "", lstr_Moneda, lstr_Nemotecnico, "", "ID").Tables[0].Select("EsNemotecnico='5' AND EsPubPriv='" + lint_EsPublico + "'").CopyToDataTable();
                    ldat_AsientoPago = ws_SGService.uwsConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_IdOperacion2, "", "", lstr_Moneda, lstr_Nemotecnico, "", "ID").Tables[0];
                }
                else
                {
                    ldat_AsientoDevengo = ws_SGService.uwsConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_IdOperacion1, "", "", lstr_Moneda, lstr_Nemotecnico, "", "ID").Tables[0];
                    ldat_AsientoPago = ws_SGService.uwsConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_IdOperacion1, "", "", lstr_Moneda, lstr_Nemotecnico, "", "ID").Tables[0];
                }

                #endregion

                #region asientoDevengo

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

                FchCont = Convert.ToDateTime(FchContable).ToString("dd.MM.yyyy");

                if (lstr_Nemotecnico != "")
                {
                    try
                    {

                        ldat_Asiento.Rows.Add(
                            lstr_NroValor + " " + lstr_Nemotecnico,
                            FchCont,
                            ldat_AsientoDevengo.Rows[0]["IdCuentaContable"].ToString().Trim(),
                            ldat_AsientoDevengo.Rows[0]["IdClaveContable"].ToString().Trim(),
                            ldat_AsientoDevengo.Rows[0]["CodigoAuxiliar"].ToString().Trim(),
                            lstr_Detalle.Trim(),
                            ldat_AsientoDevengo.Rows[0]["IdCentroCosto"].ToString().Trim(),
                            ldat_AsientoDevengo.Rows[0]["IdCentroBeneficio"].ToString().Trim(),
                            ldat_AsientoDevengo.Rows[0]["IdElementoPEP"].ToString().Trim(),
                            ldat_AsientoDevengo.Rows[0]["IdPosPre"].ToString().Trim(),
                            ldat_AsientoDevengo.Rows[0]["IdCentroGestor"].ToString().Trim(),
                            ldat_AsientoDevengo.Rows[0]["IdFondo"].ToString().Trim(),
                            ldat_AsientoDevengo.Rows[0]["DocPresupuestario"].ToString().Trim(),
                            ldat_AsientoDevengo.Rows[0]["PosDocPresupuestario"].ToString().Trim(),
                            ldec_monto
                        );
                        ldat_Asiento.Rows.Add(
                            lstr_NroValor + " " + lstr_Nemotecnico,
                            FchCont,
                            ldat_AsientoDevengo.Rows[0]["IdCuentaContable2"].ToString().Trim(),
                            ldat_AsientoDevengo.Rows[0]["IdClaveContable2"].ToString().Trim(),
                            ldat_AsientoDevengo.Rows[0]["CodigoAuxiliar"].ToString().Trim(),
                            lstr_Detalle.Trim(),
                            ldat_AsientoDevengo.Rows[0]["IdCentroCosto2"].ToString().Trim(),
                            ldat_AsientoDevengo.Rows[0]["IdCentroBeneficio2"].ToString().Trim(),
                            ldat_AsientoDevengo.Rows[0]["IdElementoPEP2"].ToString().Trim(),
                            ldat_AsientoDevengo.Rows[0]["IdPosPre2"].ToString().Trim(),
                            ldat_AsientoDevengo.Rows[0]["IdCentroGestor2"].ToString().Trim(),
                            ldat_AsientoDevengo.Rows[0]["IdFondo2"].ToString().Trim(),
                            ldat_AsientoDevengo.Rows[0]["DocPresupuestario2"].ToString().Trim(),
                            ldat_AsientoDevengo.Rows[0]["PosDocPresupuestario2"].ToString().Trim(),
                            ldec_monto
                        );

                        GenerarAsientoCostoTransaccion(ldat_Asiento, lstr_IdOperacion1 + " " + lstr_NemoInfo, lstr_IdOperacion1, lstr_NroValor + " - " + lstr_NemoInfo, ldec_TpoCambio, lint_IdCostoTransaccion, ldt_FchModifica);
                    }
                    catch
                    {

                    }
                }

                #endregion

                #region pospre
                //Validar que el pos pre sea diferente de PP_Balance y que el monto sea mayor al seleccionado
                string lstr_Monto = string.Empty;
                DataTable lds_Datos = new DataTable();
                decimal ldec_MontoTotal = 0;
                string lstr_NuevoPosPrePago = string.Empty;

                if (ldat_AsientoPago.Rows[0]["IdPosPre"].ToString().Trim().Equals("PP_Balance"))
                {
                    //ldat_Reservas = ws_SGService.uwsConsultarReservaDetallado(string.Empty); cucurucho
                    ldat_Reservas = ws_SGService.uwsConsultarReservaDetallado(string.Empty, "", "", "", "", "", "", "", "", "", "", "", "", "");
                    DataView dv = ldat_Reservas.Tables[0].DefaultView;
                    dv.Sort = "OrdenDeudaInterna ASC";

                    lds_Datos.Columns.Add("IdReserva");
                    lds_Datos.Columns.Add("OrdenDeudaInterna");
                    lds_Datos.Columns.Add("IdPosPre");
                    lds_Datos.Columns.Add("Posicion");
                    lds_Datos.Columns.Add("Monto");

                    foreach (DataRow drForm in dv.ToTable().Rows)
                    {
                        if (drForm["IdMoneda"].ToString().Trim().Equals(ldat_AsientoPago.Rows[0]["CodigoAuxiliar"].ToString().Trim()))
                            if (!drForm["OrdenDeudaInterna"].ToString().Equals(string.Empty) && !drForm["OrdenDeudaInterna"].ToString().Equals("0"))
                            {
                                lstr_Monto = wsDeudaInterna.ConsultaMontoReservaSAP(drForm["IdReserva"].ToString().Trim(), drForm["Posicion"].ToString().Trim());
                                lds_Datos.Rows.Add(
                                    drForm["IdReserva"].ToString(),
                                    drForm["OrdenDeudaInterna"].ToString(),
                                    drForm["IdPosPre"].ToString(),
                                    drForm["Posicion"].ToString(),
                                    lstr_Monto);

                                ldec_MontoTotal += Convert.ToDecimal(lstr_Monto.Replace(",", ""));
                            }
                    }

                    if (Convert.ToDecimal(ldec_MontoTotal) >= ldec_monto)
                    {
                        //Genera el asiento
                        GeneraAsientoPago(
                            ldat_AsientoPago,
                            lstr_NroValor,
                            lstr_Nemotecnico,
                            FchCont,
                            lstr_Detalle,
                            lds_Datos,
                            lstr_IdOperacion1,
                            ldec_monto, 
                            ldec_TpoCambio,
                            lint_IdCostoTransaccion, 
                            ldt_FchModifica);

                    }
                    else
                    {
                        //Almacena en bitácora de que no lo hizo
                        ws_SGService.uwsRegistrarAccionBitacoraCo("DI", "123", "Contabilizar costo de transacción", "Error al contabilizar, monto de costo de transacción superior al total de las reservas de la Deuda Interna", lstr_IdOperacion1, lstr_NroValor + "-" + lstr_Nemotecnico, "G206");
                    }

                }
                //else // SI NO ES PP_BALANCE
                //{
                //    GeneraAsientoPago(
                //        ldat_AsientoPago,
                //        lstr_NroValor,
                //        lstr_Nemotecnico,
                //        FchCont,
                //        lstr_Detalle,
                //        "",
                //        "",
                //        lstr_IdOperacion1);
                //}


                #endregion

            }
            catch (Exception ex)
            {
               lstr_Mensaje = ex.ToString();
            }
            return lstr_Mensaje;
        }

        private static void GeneraAsientoPago(DataTable ldat_AsientoPago,
                                    string lstr_NroValor,
                                    string lstr_Nemotecnico,
                                    string FchCont,
                                    string lstr_Detalle,
                                    DataTable ldt_DatosAsiento,
                                    string lstr_IdOperacion1,
                                    decimal ldec_MontoIndicado,
                                    decimal ldec_TpoCambio,
                                    int lint_IdCostoTransaccion,
                                    DateTime ldt_FchModifica)
        {

            DataTable ldat_Asiento = new DataTable();


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

            try
            {
                //Repetir
                decimal ldec_Saldo = ldec_MontoIndicado;
                foreach (DataRow drForm in ldt_DatosAsiento.Rows)
                {
                    if (Convert.ToDecimal(drForm["Monto"]) > 0 && ldec_Saldo > 0)
                    {
                        ldat_Asiento.Rows.Add(
                            lstr_NroValor + " " + lstr_Nemotecnico,
                            FchCont,
                            ldat_AsientoPago.Rows[0]["IdCuentaContable"].ToString().Trim(),
                            ldat_AsientoPago.Rows[0]["IdClaveContable"].ToString().Trim(),
                            ldat_AsientoPago.Rows[0]["CodigoAuxiliar"].ToString().Trim(),
                            lstr_Detalle.Trim(),
                            ldat_AsientoPago.Rows[0]["IdCentroCosto"].ToString().Trim(),
                            ldat_AsientoPago.Rows[0]["IdCentroBeneficio"].ToString().Trim(),
                            ldat_AsientoPago.Rows[0]["IdElementoPEP"].ToString().Trim(),
                            drForm["IdPosPre"].ToString().Trim().Equals(string.Empty) ? ldat_AsientoPago.Rows[0]["IdPosPre"].ToString().Trim() : drForm["IdPosPre"].ToString().Trim(),
                            ldat_AsientoPago.Rows[0]["IdCentroGestor"].ToString().Trim(),
                            ldat_AsientoPago.Rows[0]["IdFondo"].ToString().Trim(),
                            drForm["IdReserva"].ToString().Trim(),
                            drForm["Posicion"].ToString().Trim(),
                            ldec_Saldo > Convert.ToDecimal(drForm["Monto"].ToString()) ? Convert.ToDecimal(drForm["Monto"].ToString()) : ldec_Saldo
                        );
                    }

                    //Resta el saldo    
                    ldec_Saldo = ldec_Saldo - Convert.ToDecimal(drForm["Monto"]);
                }

                ldat_Asiento.Rows.Add(
                    lstr_NroValor + " " + lstr_Nemotecnico,
                    FchCont,
                    ldat_AsientoPago.Rows[0]["IdCuentaContable2"].ToString().Trim(),
                    ldat_AsientoPago.Rows[0]["IdClaveContable2"].ToString().Trim(),
                    ldat_AsientoPago.Rows[0]["CodigoAuxiliar"].ToString().Trim(),
                    lstr_Detalle.Trim(),
                    ldat_AsientoPago.Rows[0]["IdCentroCosto2"].ToString().Trim(),
                    ldat_AsientoPago.Rows[0]["IdCentroBeneficio2"].ToString().Trim(),
                    ldat_AsientoPago.Rows[0]["IdElementoPEP2"].ToString().Trim(),
                    ldat_AsientoPago.Rows[0]["IdPosPre2"].ToString().Trim(),
                    ldat_AsientoPago.Rows[0]["IdCentroGestor2"].ToString().Trim(),
                    ldat_AsientoPago.Rows[0]["IdFondo2"].ToString().Trim(),
                    ldat_AsientoPago.Rows[0]["DocPresupuestario2"].ToString().Trim(),
                    ldat_AsientoPago.Rows[0]["PosDocPresupuestario2"].ToString().Trim(),
                    ldec_MontoIndicado
                );
                //
                GenerarAsientoCostoTransaccion(ldat_Asiento, lstr_IdOperacion1 + " " + lstr_Nemotecnico, lstr_IdOperacion1, lstr_NroValor + " - " + lstr_Nemotecnico, ldec_TpoCambio, lint_IdCostoTransaccion, ldt_FchModifica);
            }
            catch
            {

            }
        }

        public static string GenerarAsientoCostoTransaccion(DataTable ldat_Asiento, string str_Accion, string str_IDOperacion, string str_IdTransaccion, decimal ldec_MontoIndicado,
             int lint_IdCostoTransaccion, DateTime ldt_FchModifica)
        {
            //Coleccion de asientos y tipos de asientos requeridos en SAP expuestos por la referencia del servicio
            wsAsientos.ZfiAsiento item_asiento = new wsAsientos.ZfiAsiento();
            wsAsientos.ZfiAsiento[] tabla_asientos = new wsAsientos.ZfiAsiento[ldat_Asiento.Rows.Count];

            //variables de proceso
            string[] item_resAsientosLog = new string[10];
            string logAsiento = string.Empty;
            string flagEstadoAsiento = string.Empty;

            //string lstr_Moneda = dbMoneda.SelectedValue;
            //string lstr_Referencia = txtReferencia.Text;

            try
            {
                foreach (DataRow ldr_Row in ldat_Asiento.Rows)
                {
                    item_asiento = new wsAsientos.ZfiAsiento();
                    int index = ldat_Asiento.Rows.IndexOf(ldr_Row);
                    if (index == 0)
                    {

                        item_asiento.Blart = "ID";//Clase de documento
                        item_asiento.Bukrs = "G206";//Sociedad
                        //item_asiento.Werks = ldat_Asiento.Rows[0]["Referencia"].ToString();
                        item_asiento.Bldat = ldat_Asiento.Rows[index]["Fecha"].ToString();//Fecha de documento
                        item_asiento.Budat = ldat_Asiento.Rows[index]["Fecha"].ToString();//Fecha de contabilización
                    }
                    item_asiento.Waers = ldat_Asiento.Rows[index]["Moneda"].ToString();//Moneda 
                    item_asiento.Bschl = ldat_Asiento.Rows[index]["ClaveContable"].ToString();//Clave de contabilización
                    item_asiento.Hkont = ldat_Asiento.Rows[index]["Cuenta"].ToString();//Cuenta de mayor
                    item_asiento.Wrbtr = Convert.ToDecimal(ldat_Asiento.Rows[index]["Monto"].ToString());//Importe
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
                        item_asiento.Kursf = ldec_MontoIndicado;


                    tabla_asientos[index] = item_asiento;
                }

                //Cargar de Asientos 
                string[] concatenado = new string[8];
                //envio de asiento mediante servicio web hacia SIGAF
                //item_resAsientosLog = wsAsientos.EnviarAsientos(tabla_asientos); cucurucho
                item_resAsientosLog = wsAsientos.EnviarAsientos(tabla_asientos,"");
                for (int j = 0; j < item_resAsientosLog.Length; j++)
                {
                    int x = j + 1;
                    logAsiento += x + " - " + item_resAsientosLog[j] + " - ";
                }
                //Registrar en Bitacora de movimientos
                string lstr_resultado = ws_SGService.uwsRegistrarAccionBitacoraCo("DI", "123", str_Accion, "Resultado de Contabilización: " + logAsiento, str_IDOperacion, str_IdTransaccion, "");
                string[] vre;
                //Marcar registro como contabilizado
                if(!logAsiento.Contains("[E]"))
                    vre = wsDeudaInterna.ContabilizarCalculosFinancieros(
                        "CostosTransaccion",
                        lint_IdCostoTransaccion.ToString(), 
                        null,
                        null,
                        "C",
                        "SG",
                        ldt_FchModifica);
                return logAsiento;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

    }
}
