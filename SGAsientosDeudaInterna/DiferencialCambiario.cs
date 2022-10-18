using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio.Contingentes;
using System.Collections.Generic;

namespace SGAsientosDeudaInterna
{
    public class DiferencialCambiario
    {
        private static wsDeudaInterna.wsDeudaInterna wsDeudaInterna = new wsDeudaInterna.wsDeudaInterna();
        private static ws_SGService.wsSistemaGestor ws_SGService = new ws_SGService.wsSistemaGestor();
        private static wsAsientos.ServicioContable wsAsientos = new wsAsientos.ServicioContable();
        
        //static void Main(string[] args)
        //{   
        //    ///*
        //    // * Se recorre el DataSet con costos de transacción, se obtiene el monto y el día de la creación
        //    // *  x1 = Monto * TipoCambio(FechaCreacion)
        //    // *  x2 = Monto * TipoCambio(Hoy)
        //    // *  x1 - x2 = Resultado
        //    // *  Si resultado es positivo = ID77
        //    // *  Si resultado es negativo = ID78
        //    // */
            
        //    Console.WriteLine("Ejecutar aplicación:");
        //    DiferencialCambiarios();
        //    Console.ReadLine();
        //}

        public void DiferencialCambiarios()
        {
            string lstr_Mensaje = string.Empty;
            //decimal ldec_montoInicial = 0;            
            decimal ldec_montoActual = 0;
            DateTime ldt_FechaActual = DateTime.Now;
            DataSet lds_Titulos = wsDeudaInterna.ConsultarCostoTransaccion(string.Empty, string.Empty, string.Empty, ldt_FechaActual.ToString(), string.Empty);

            try
            {
                if (lds_Titulos.Tables["Table"].Rows.Count > 0)
                    foreach (DataRow dr_Costos in lds_Titulos.Tables["Table"].Rows)
                    {
                        if (dr_Costos["Moneda"].ToString().Equals("UDE"))
                        {
                            DataSet lds_TipoCambioActual = ws_SGService.uwsConsultarTiposCambio("UDE", ldt_FechaActual, string.Empty, string.Empty);

                            ldec_montoActual = Convert.ToDecimal(dr_Costos["Monto"].ToString()) *
                                Convert.ToDecimal(lds_TipoCambioActual.Tables[0].Rows[0]["Valor"].ToString());

                            PrepararDiferencialCambiario(
                                Convert.ToInt32(dr_Costos["IdCostoTransaccion"].ToString()),
                                Convert.ToDecimal(dr_Costos["MontoColones"].ToString()),
                                Convert.ToDecimal(ldec_montoActual),
                                dr_Costos["NemoTecnico"].ToString(),
                                (ldec_montoActual - (Convert.ToDecimal(dr_Costos["Monto"].ToString())) > 0) ? "ID77" : "ID78",
                                dr_Costos["NroValor"].ToString(),
                                "Valoración de la moneda: " + dr_Costos["NroValor"].ToString() + "-" + dr_Costos["NemoTecnico"].ToString(),
                                ldt_FechaActual.ToString(),
                                dr_Costos["ModuloSINPE"].ToString(),
                                dr_Costos["Estado"].ToString(),
                                Convert.ToDateTime(dr_Costos["FchModifica"].ToString()),
                                Convert.ToDecimal(dr_Costos["TpoCambio"].ToString()));
                        }
                    }
            }
            catch (Exception ex) { lstr_Mensaje = ex.ToString(); }
        }


        public static void PrepararDiferencialCambiario(
            int lint_IdCostoTransaccion,
            decimal ldec_montoInicial,
            decimal ldec_montoActual,
            string lstr_Nemotecnico,
            string lstr_IdOperacion,
            string lstr_NroValor,
            string lstr_Detalle,
            string FchContable,
            string lstr_ModuloSINPE,
            string lstr_Estado,
            DateTime ldt_FchModifica,
            decimal ldec_TpoCambio)
        {
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
              
                if (ldat_TituloValor.Rows.Count > 0)
                    ldat_AsientoDevengo = ws_SGService.uwsConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_IdOperacion, "", "", "UDE", lstr_Nemotecnico, "", "ID").Tables[0];
                

                ldat_AsientoPago = ws_SGService.uwsConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_IdOperacion, "", "", "UDE", lstr_Nemotecnico, "", "ID").Tables[0];
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

                if (lstr_Nemotecnico != string.Empty)
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
                            ldec_montoActual - ldec_montoInicial
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
                            ldec_montoActual - ldec_montoInicial
                        );

                        GenerarAsientoCostoTransaccion(ldat_Asiento, lstr_IdOperacion + " " + lstr_NemoInfo, lstr_IdOperacion, lstr_NroValor + " - " + lstr_NemoInfo, lint_IdCostoTransaccion, ldec_montoActual, lstr_ModuloSINPE, lstr_Estado, ldt_FchModifica, ldec_TpoCambio);
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

                    if (Convert.ToDecimal(ldec_MontoTotal) >= (ldec_montoActual - ldec_montoInicial))
                    {
                        //Genera el asiento
                        GeneraAsientoAjuste(
                            ldat_AsientoPago,
                            lstr_NroValor,
                            lstr_Nemotecnico,
                            FchCont,
                            lstr_Detalle,
                            lds_Datos,
                            lstr_IdOperacion,
                            ldec_montoInicial,
                            ldec_montoActual,
                            lint_IdCostoTransaccion,
                            lstr_ModuloSINPE,
                            lstr_Estado,
                            ldt_FchModifica, 
                            ldec_TpoCambio);

                    }
                    else
                    {
                        //Almacena en bitácora de que no lo hizo
                        ws_SGService.uwsRegistrarAccionBitacoraCo("DI", "123", "Contabilizar costo de transacción", "Error al contabilizar, monto de costo de transacción superior al total de las reservas de la Deuda Interna", lstr_IdOperacion, lstr_NroValor + "-" + lstr_Nemotecnico, "G206");
                    }

                }
               
                #endregion

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private static void GeneraAsientoAjuste(DataTable ldat_AsientoAjuste,
                                    string lstr_NroValor,
                                    string lstr_Nemotecnico,
                                    string FchCont,
                                    string lstr_Detalle, 
                                    DataTable ldt_DatosAsiento, 
                                    string lstr_IdOperacion1, 
                                    decimal ldec_montoInicial,
                                    decimal ldec_montoActual,
                                    int lint_IdCostoTransaccion,
                                    string lstr_ModuloSINPE,
                                    string lstr_Estado,
                                    DateTime ldt_FchModifica,
                                    decimal ldec_TpoCambio)
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
                //Repetirn
                decimal ldec_Saldo = (ldec_montoActual - ldec_montoInicial);
                foreach (DataRow drForm in ldt_DatosAsiento.Rows)
                {
                    if (Convert.ToDecimal(drForm["Monto"]) > 0 && ldec_Saldo > 0)
                    {
                        ldat_Asiento.Rows.Add(
                            lstr_NroValor + " " + lstr_Nemotecnico,
                            FchCont,
                            ldat_AsientoAjuste.Rows[0]["IdCuentaContable"].ToString().Trim(),
                            ldat_AsientoAjuste.Rows[0]["IdClaveContable"].ToString().Trim(),
                            ldat_AsientoAjuste.Rows[0]["CodigoAuxiliar"].ToString().Trim(),
                            lstr_Detalle.Trim(),
                            ldat_AsientoAjuste.Rows[0]["IdCentroCosto"].ToString().Trim(),
                            ldat_AsientoAjuste.Rows[0]["IdCentroBeneficio"].ToString().Trim(),
                            ldat_AsientoAjuste.Rows[0]["IdElementoPEP"].ToString().Trim(),
                            drForm["IdPosPre"].ToString().Trim().Equals(string.Empty) ? ldat_AsientoAjuste.Rows[0]["IdPosPre"].ToString().Trim() : drForm["IdPosPre"].ToString().Trim(),
                            ldat_AsientoAjuste.Rows[0]["IdCentroGestor"].ToString().Trim(),
                            ldat_AsientoAjuste.Rows[0]["IdFondo"].ToString().Trim(),
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
                    ldat_AsientoAjuste.Rows[0]["IdCuentaContable2"].ToString().Trim(),
                    ldat_AsientoAjuste.Rows[0]["IdClaveContable2"].ToString().Trim(),
                    ldat_AsientoAjuste.Rows[0]["CodigoAuxiliar"].ToString().Trim(),
                    lstr_Detalle.Trim(),
                    ldat_AsientoAjuste.Rows[0]["IdCentroCosto2"].ToString().Trim(),
                    ldat_AsientoAjuste.Rows[0]["IdCentroBeneficio2"].ToString().Trim(),
                    ldat_AsientoAjuste.Rows[0]["IdElementoPEP2"].ToString().Trim(),
                    ldat_AsientoAjuste.Rows[0]["IdPosPre2"].ToString().Trim(),
                    ldat_AsientoAjuste.Rows[0]["IdCentroGestor2"].ToString().Trim(),
                    ldat_AsientoAjuste.Rows[0]["IdFondo2"].ToString().Trim(),
                    ldat_AsientoAjuste.Rows[0]["DocPresupuestario2"].ToString().Trim(),
                    ldat_AsientoAjuste.Rows[0]["PosDocPresupuestario2"].ToString().Trim(),
                    (ldec_montoActual - ldec_montoInicial)
                );
                //
                GenerarAsientoCostoTransaccion(ldat_Asiento, lstr_IdOperacion1 + " " + lstr_Nemotecnico, lstr_IdOperacion1, lstr_NroValor + " - " + lstr_Nemotecnico, lint_IdCostoTransaccion, ldec_montoActual, lstr_ModuloSINPE, lstr_Estado, ldt_FchModifica, ldec_TpoCambio);
            }
            catch
            {

            }
        }

        public static string GenerarAsientoCostoTransaccion(
            DataTable ldat_Asiento, 
            string str_Accion, 
            string str_IDOperacion, 
            string str_IdTransaccion, 
            int lint_IdCostoTransaccion, 
            decimal ldec_MontoColones,
            string lstr_ModuloSINPE,
            string lstr_Estado,
            DateTime ldt_FchModifica,
            decimal ldec_TpoCambio)
        {
            //Coleccion de asientos y tipos de asientos requeridos en SAP expuestos por la referencia del servicio
            wsAsientos.ZfiAsiento item_asiento = new wsAsientos.ZfiAsiento();
            wsAsientos.ZfiAsiento[] tabla_asientos = new wsAsientos.ZfiAsiento[ldat_Asiento.Rows.Count];

            //variables de proceso
            string[] item_resAsientosLog = new string[10];
            string logAsiento = string.Empty;
            string flagEstadoAsiento = string.Empty;
            
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
                   
                    tabla_asientos[index] = item_asiento;


                    //
                    //Actualiza el monto del costo de transacción por el monto actual
                    string[] lstr_Referencia = ldat_Asiento.Rows[0]["Referencia"].ToString().Split(' ');
                    wsDeudaInterna.ModificarCostoTransaccion(
                        lint_IdCostoTransaccion, 
                        lstr_Referencia[0],
                        lstr_Referencia[1],
                        Convert.ToDateTime(ldat_Asiento.Rows[0]["Fecha"].ToString()), 
                        "UDE", 
                        Convert.ToDecimal(ldat_Asiento.Rows[0]["Monto"].ToString()), 
                        ldec_MontoColones,
                        ldec_TpoCambio,
                        ldat_Asiento.Rows[0]["TextoInfo"].ToString(), 
                        lstr_ModuloSINPE, 
                        lstr_Estado, 
                        "SG",
                        ldt_FchModifica);
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
                ws_SGService.uwsRegistrarAccionBitacoraCo("DI", "123", str_Accion, "Resultado de Contabilización: " + logAsiento, str_IDOperacion, str_IdTransaccion, "");
                

                return logAsiento;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }


    }
}