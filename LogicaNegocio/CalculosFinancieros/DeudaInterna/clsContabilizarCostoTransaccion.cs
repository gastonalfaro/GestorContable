using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using LogicaNegocio.Mantenimiento;
using LogicaNegocio.Seguridad;

namespace LogicaNegocio.CalculosFinancieros.DeudaInterna
{
    public class clsContabilizarCostoTransaccion
    {
        //private static wsAsientos.ServicioContable wsAsientos = new wsAsientos.ServicioContable();
        private static clsTiposAsiento tasientos = new clsTiposAsiento();
        private static wsDeudaInterna.wsDeudaInterna wsDeudaInterna = new wsDeudaInterna.wsDeudaInterna();
        private static Seguridad.tBitacora bitacora = new Seguridad.tBitacora();
        private static clsTituloValor titulos = new clsTituloValor();
        private static tiras tira = new tiras();
        private static clsCostoTransaccion costo = new clsCostoTransaccion();
        private static Mantenimiento.clsTiposAsiento asientos = new Mantenimiento.clsTiposAsiento();
        private static Mantenimiento.clsOperaciones loperacion = new clsOperaciones();
        private static Mantenimiento.clsReservasDetalle reserva = new Mantenimiento.clsReservasDetalle();
        private static clsCostoTransaccion lcls_CostoTransaccion = new clsCostoTransaccion();
        private static Mantenimiento.clsPropietarios propietario = new Mantenimiento.clsPropietarios();

        public string GeneraCostosTransaccion()
        {
            string lstr_Mensaje = string.Empty;

            try
            {

                DataSet vCostoTransaccion = costo.ConsultarCostoTransaccion(null, null, null, null, null);

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
            string lstr_NomOperacion1 = string.Empty;
            string lstr_NomOperacion2 = string.Empty;
            string lstr_Mensaje = string.Empty;
            DataTable ldat_Asiento = new DataTable();
            DataTable ldat_AsientoDevengo = new DataTable();
            DataTable ldat_AsientoPago = new DataTable();
            DataSet ldat_Reservas = new DataSet();
            DataTable ldat_TituloValor = new DataTable();
            string FchCont = string.Empty;
            string lstr_NemoInfo = lstr_Nemotecnico;
            int lint_EsPublico = 1;
            int lint_Plazo = 1;
            string lstr_MonedaTitulo = "";
            string lstr_MonedaUDE = "";
            string lstr_Propietario = "";

            try
            {
                #region costo transaccion
                int vNunValor = 0;
                vNunValor = (Int32.TryParse(lstr_NroValor, out vNunValor) ? (vNunValor==0 ? -1 : vNunValor) : 0);
                //determina si el costo por transacción está asociado a un título
                //Convert.ToInt32(lstr_NroValor)
                DataSet titulosDataset = titulos.ConsultarTituloValor(vNunValor, lstr_Nemotecnico, String.Empty, String.Empty, "V", String.Empty, String.Empty, String.Empty, Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/5000"),string.Empty);
                if (titulosDataset.Tables.Count > 0)
                {
                    ldat_TituloValor = titulosDataset.Tables[0];
                }
                // titulos.ConsultarTituloValor(lstr_NroValor, lstr_Nemotecnico.Trim(), "%", "%", "%", "01/01/1900", "01/01/5000").Tables[0];//.Select("IndicadorCupon = 'V'").CopyToDataTable();
                
                if (ldat_TituloValor.Rows.Count != 0)
                {
                    lstr_MonedaTitulo = ldat_TituloValor.Rows[0]["Moneda"].ToString().Trim();
                    lstr_Propietario = ldat_TituloValor.Rows[0]["Propietario"].ToString().Trim();

                    if (propietario.ConsultarPropietarios(string.Empty, string.Empty, string.Empty, lstr_Propietario).Tables[0].Rows.Count == 0)
                    {
                        lint_EsPublico = 2;
                    }

                    if ((Convert.ToDateTime(ldat_TituloValor.Rows[0]["FchVencimiento"].ToString()) - Convert.ToDateTime(ldat_TituloValor.Rows[0]["FchValor"].ToString())).Days > 365)
                    {
                        lint_Plazo = 2;
                    }

                    //moneda del pago
                    switch (lstr_Moneda)
                    {
                        case "USD":
                            {
                                //lstr_IdOperacion1 = "ID02";
                                lstr_IdOperacion2 = "ID04";
                                break;
                            }
                        case "CRCN":
                            {
                                lstr_Moneda = "CRC";
                                //lstr_IdOperacion1 = "ID01";
                                lstr_IdOperacion2 = "ID03";
                                break;
                            }
                        case "UDE":
                            {
                                lstr_Moneda = "CRC";
                                lstr_MonedaUDE = "UDE";
                                //lstr_IdOperacion1 = "ID01";
                                lstr_IdOperacion2 = "ID03";
                                break;
                            }
                    }

                    DataSet lds_Operaciones = loperacion.ConsultarOperaciones(lstr_IdOperacion2, "IdModulo IN ('DI')", "");
                    if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
                    {
                        lstr_NomOperacion2 = lds_Operaciones.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
                    }

                    //Moneda del título
                    switch (lstr_MonedaTitulo)
                    {
                        case "USD":
                            {
                                lstr_IdOperacion1 = "ID02";
                                //lstr_IdOperacion2 = "ID04";
                                break;
                            }
                        case "CRCN":
                            {
                                lstr_MonedaTitulo = "CRC";
                                lstr_IdOperacion1 = "ID01";
                                //lstr_IdOperacion2 = "ID03";
                                break;
                            }
                        case "UDE":
                            {
                                lstr_MonedaTitulo = "CRC";
                                lstr_IdOperacion1 = "ID01";
                                //lstr_IdOperacion2 = "ID03";

                                break;
                            }
                    }

                    DataSet lds_Operaciones2 = loperacion.ConsultarOperaciones(lstr_IdOperacion1, "IdModulo IN ('DI')", "");
                    if (lds_Operaciones2.Tables.Count > 0 && lds_Operaciones2.Tables["Table"].Rows.Count > 0)
                    {
                        lstr_NomOperacion1 = lds_Operaciones2.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
                    }

                }
                else
                {
                    switch (lstr_Moneda)
                    {
                        case "USD":
                            {
                                lstr_IdOperacion2 = "ID06";
                                lstr_Nemotecnico = "";
                                break;
                            }
                        case "CRCN":
                            {
                                lstr_Moneda = "CRC";
                                lstr_IdOperacion2 = "ID05";
                                lstr_Nemotecnico = "";
                                break;
                            }
                        case "UDE":
                            {
                                lstr_Moneda = "CRC";
                                lstr_MonedaUDE = "UDE";
                                lstr_IdOperacion2 = "ID05";
                                lstr_Nemotecnico = "";
                                break;
                            }
                    }
                    DataSet lds_Operaciones2 = loperacion.ConsultarOperaciones(lstr_IdOperacion2, "IdModulo IN ('DI')", "");
                    if (lds_Operaciones2.Tables.Count > 0 && lds_Operaciones2.Tables["Table"].Rows.Count > 0)
                    {
                        lstr_NomOperacion2 = lds_Operaciones2.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
                    }


                }



                if (ldat_TituloValor.Rows.Count != 0)
                {
                    ldat_AsientoDevengo = asientos.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_IdOperacion1, "", "", lstr_MonedaTitulo, lstr_Nemotecnico, "", "ID").Tables[0].Select("EsNemotecnico='5' AND EsPubPriv='" + lint_EsPublico + "' AND EsLargoCortoPlazo = '" + lint_Plazo + "'").CopyToDataTable();
                    //ldat_AsientoPago = asientos.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_IdOperacion2, "", "", lstr_Moneda, lstr_Nemotecnico, "", "ID").Tables[0];
                    ldat_AsientoPago = asientos.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_IdOperacion2, "", "", lstr_Moneda, "", "", "ID").Tables[0];
                }
                else
                {
                    //ldat_AsientoDevengo = asientos.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_IdOperacion1, "", "", lstr_Moneda, lstr_Nemotecnico, "", "ID").Tables[0];
                    ldat_AsientoPago = asientos.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_IdOperacion2, "", "", lstr_Moneda, lstr_Nemotecnico, "", "ID").Tables[0];
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
                ldat_Asiento.Columns.Add("PKMovimiento");
                ldat_Asiento.Columns.Add("Texto2");
                ldat_Asiento.Columns.Add("Ref1Tipo");
                ldat_Asiento.Columns.Add("Ref2Operacion");

                FchCont = Convert.ToDateTime(FchContable).ToString("dd.MM.yyyy");

                if (lstr_Nemotecnico != "")
                {
                    try
                    {
                        decimal monto_conversion = 0;

                        if (lstr_Moneda == "USD" && lstr_MonedaTitulo == "CRC")
                        {
                            monto_conversion = Math.Round((ldec_monto * ldec_TpoCambio),4);
                        }
                        else if (lstr_Moneda == "CRC" && lstr_MonedaTitulo == "USD")
                        {
                            monto_conversion = Math.Round((ldec_monto / ldec_TpoCambio), 4);
                        }
                        else
                        {
                            monto_conversion = ldec_monto;
                        }

                        string lstr_Referencia = lstr_NroValor.Trim().Equals("00") ? "No Asociado" : lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Costo Transaccion";

                        ldat_Asiento.Rows.Add(
                            lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                            FchCont,
                            ldat_AsientoDevengo.Rows[0]["IdCuentaContable"].ToString().Trim(),
                            ldat_AsientoDevengo.Rows[0]["IdClaveContable"].ToString().Trim(),
                            ldat_AsientoDevengo.Rows[0]["CodigoAuxiliar"].ToString().Trim(),
                            lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim(),
                            ldat_AsientoDevengo.Rows[0]["IdCentroCosto"].ToString().Trim(),
                            ldat_AsientoDevengo.Rows[0]["IdCentroBeneficio"].ToString().Trim(),
                            ldat_AsientoDevengo.Rows[0]["IdElementoPEP"].ToString().Trim(),
                            ldat_AsientoDevengo.Rows[0]["IdPosPre"].ToString().Trim(),
                            ldat_AsientoDevengo.Rows[0]["IdCentroGestor"].ToString().Trim(),
                            ldat_AsientoDevengo.Rows[0]["IdFondo"].ToString().Trim(),
                            ldat_AsientoDevengo.Rows[0]["DocPresupuestario"].ToString().Trim(),
                            ldat_AsientoDevengo.Rows[0]["PosDocPresupuestario"].ToString().Trim(),
                            lstr_MonedaUDE.Equals("UDE") ? Math.Round((monto_conversion * ldec_TpoCambio), 4) : monto_conversion,
                            lstr_NroValor + "." + lstr_Nemotecnico,//pk
                            tira.get_operation_name(lstr_IdOperacion1, "DI"),//texto2
                            lstr_Moneda,//tipo
                            lstr_IdOperacion1 +"."+lstr_NomOperacion1 //operacion
                            );
                        ldat_Asiento.Rows.Add(
                            lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                            FchCont,
                            ldat_AsientoDevengo.Rows[0]["IdCuentaContable2"].ToString().Trim(),
                            ldat_AsientoDevengo.Rows[0]["IdClaveContable2"].ToString().Trim(),
                            ldat_AsientoDevengo.Rows[0]["CodigoAuxiliar"].ToString().Trim(),
                            lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim(),
                            ldat_AsientoDevengo.Rows[0]["IdCentroCosto2"].ToString().Trim(),
                            ldat_AsientoDevengo.Rows[0]["IdCentroBeneficio2"].ToString().Trim(),
                            ldat_AsientoDevengo.Rows[0]["IdElementoPEP2"].ToString().Trim(),
                            ldat_AsientoDevengo.Rows[0]["IdPosPre2"].ToString().Trim(),
                            ldat_AsientoDevengo.Rows[0]["IdCentroGestor2"].ToString().Trim(),
                            ldat_AsientoDevengo.Rows[0]["IdFondo2"].ToString().Trim(),
                            ldat_AsientoDevengo.Rows[0]["DocPresupuestario2"].ToString().Trim(),
                            ldat_AsientoDevengo.Rows[0]["PosDocPresupuestario2"].ToString().Trim(),
                            lstr_MonedaUDE.Equals("UDE") ? Math.Round((monto_conversion * ldec_TpoCambio), 4) : monto_conversion,
                            lstr_NroValor + "." + lstr_Nemotecnico,//pk
                            tira.get_operation_name(lstr_IdOperacion1, "DI"),//texto2
                            lstr_Moneda,//tipo
                            lstr_IdOperacion1 +"."+ lstr_NomOperacion1//operacion
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

                //if (ldat_AsientoPago.Rows[0]["IdPosPre"].ToString().Trim().Equals("PP_Balance"))
                //{
                //wsSG.wsSistemaGestor wssg = new wsSG.wsSistemaGestor();
                //ldat_Reservas = wssg.uwsConsultarReservaDetallado(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, ldat_AsientoPago.Rows[0]["IdCuentaContable"].ToString().Trim(), string.Empty, ldat_AsientoPago.Rows[0]["IdPosPre"].ToString().Trim(), string.Empty, string.Empty, string.Empty, string.Empty);
                
                //TODO: validacion--> Si es sin titulo no deberia consultar las reservas
                ldat_Reservas = reserva.ConsultarReservasDetallado(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, ldat_AsientoPago.Rows[0]["IdCuentaContable"].ToString().Trim(), string.Empty, ldat_AsientoPago.Rows[0]["IdPosPre"].ToString().Trim(), string.Empty, string.Empty, string.Empty, string.Empty);
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
                            lstr_IdOperacion2,
                            lstr_NomOperacion2,
                            //monto_conversion,
                            //ldec_monto,
                            lstr_MonedaUDE.Equals("UDE") ? Math.Round((ldec_monto * ldec_TpoCambio),4) : ldec_monto,

                            ldec_TpoCambio,
                            lint_IdCostoTransaccion,
                            ldt_FchModifica);
                    }
                    else
                    {
                        //Almacena en bitácora de que no lo hizo
                        bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_IdOperacion2, "DI"), "Error al contabilizar, monto de costo de transacción superior al total de las reservas de la Deuda Interna", lstr_IdOperacion2, lstr_NroValor + "-" + lstr_Nemotecnico, "G206");
                    }
                }
                else if (lstr_NroValor.Trim().Equals("00"))
                {
                    //Genera el asiento
                    GeneraAsientoPago(
                        ldat_AsientoPago,
                        lstr_NroValor,
                        lstr_Nemotecnico,
                        FchCont,
                        lstr_Detalle,
                        lds_Datos,
                        lstr_IdOperacion2,
                        lstr_NomOperacion2,
                        //monto_conversion,
                        //ldec_monto,
                        lstr_MonedaUDE.Equals("UDE") ? Math.Round((ldec_monto * ldec_TpoCambio), 4) : ldec_monto,
                        ldec_TpoCambio,
                        lint_IdCostoTransaccion,
                        ldt_FchModifica);
                }
                else
                {
                    bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_IdOperacion2, "DI"), "Error al contabilizar, No hay reservas correspondientes a la cuenta " + ldat_AsientoPago.Rows[0]["IdCuentaContable"].ToString().Trim() + " con fondo " + ldat_AsientoPago.Rows[0]["IdPosPre"].ToString().Trim(), lstr_IdOperacion2, lstr_NroValor + "-" + lstr_Nemotecnico, "G206");
                }

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
                                    string lstr_NomOperacion1,
                                    decimal ldec_MontoIndicado,
                                    decimal ldec_TpoCambio,
                                    int lint_IdCostoTransaccion,
                                    DateTime ldt_FchModifica)
        {

            DataTable ldat_Asiento = new DataTable();
            string lstr_Referencia = "";

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
            ldat_Asiento.Columns.Add("Ref1Tipo");//texto2
            ldat_Asiento.Columns.Add("Ref2Operacion");

            try
            {
                //Repetir
                decimal ldec_Saldo = ldec_MontoIndicado;
                foreach (DataRow drForm in ldt_DatosAsiento.Rows)
                {
                    if (Convert.ToDecimal(drForm["Monto"]) > 0 && ldec_Saldo > 0)
                    {
                        lstr_Referencia = lstr_NroValor.Trim().Equals("00") ? "No Asociado" : lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Costo Transaccion";

                        ldat_Asiento.Rows.Add(
                            lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                            FchCont,
                            ldat_AsientoPago.Rows[0]["IdCuentaContable"].ToString().Trim(),
                            ldat_AsientoPago.Rows[0]["IdClaveContable"].ToString().Trim(),
                            ldat_AsientoPago.Rows[0]["CodigoAuxiliar"].ToString().Trim(),
                            lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim(),
                            ldat_AsientoPago.Rows[0]["IdCentroCosto"].ToString().Trim(),
                            ldat_AsientoPago.Rows[0]["IdCentroBeneficio"].ToString().Trim(),
                            ldat_AsientoPago.Rows[0]["IdElementoPEP"].ToString().Trim(),
                            drForm["IdPosPre"].ToString().Trim().Equals(string.Empty) ? ldat_AsientoPago.Rows[0]["IdPosPre"].ToString().Trim() : drForm["IdPosPre"].ToString().Trim(),
                            ldat_AsientoPago.Rows[0]["IdCentroGestor"].ToString().Trim(),
                            ldat_AsientoPago.Rows[0]["IdFondo"].ToString().Trim(),
                            drForm["IdReserva"].ToString().Trim(),
                            drForm["Posicion"].ToString().Trim(),
                            ldec_Saldo > Convert.ToDecimal(drForm["Monto"].ToString()) ? Convert.ToDecimal(drForm["Monto"].ToString()) : ldec_Saldo,
                            lstr_NroValor + "." + lstr_Nemotecnico,//pk
                            tira.get_operation_name(lstr_IdOperacion1, "DI"),//texto2
                            "",//lstr_Moneda,//tipo
                            lstr_IdOperacion1 +"."+lstr_NomOperacion1//operacion
                            );
                    }

                    //Resta el saldo    
                    ldec_Saldo = ldec_Saldo - Convert.ToDecimal(drForm["Monto"]);
                }

                if (lstr_NroValor.Trim().Equals("00"))
                {
                    lstr_Referencia = lstr_NroValor.Trim().Equals("00") ? "No Asociado" : lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Costo Transaccion";

                    ldat_Asiento.Rows.Add(
                        lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                        FchCont,
                        ldat_AsientoPago.Rows[0]["IdCuentaContable"].ToString().Trim(),
                        ldat_AsientoPago.Rows[0]["IdClaveContable"].ToString().Trim(),
                        ldat_AsientoPago.Rows[0]["CodigoAuxiliar"].ToString().Trim(),
                        lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim(),
                        ldat_AsientoPago.Rows[0]["IdCentroCosto"].ToString().Trim(),
                        ldat_AsientoPago.Rows[0]["IdCentroBeneficio"].ToString().Trim(),
                        ldat_AsientoPago.Rows[0]["IdElementoPEP"].ToString().Trim(),
                        ldat_AsientoPago.Rows[0]["IdPosPre"].ToString().Trim(),
                        ldat_AsientoPago.Rows[0]["IdCentroGestor"].ToString().Trim(),
                        ldat_AsientoPago.Rows[0]["IdFondo"].ToString().Trim(),
                        ldat_AsientoPago.Rows[0]["DocPresupuestario"].ToString().Trim(),
                        ldat_AsientoPago.Rows[0]["PosDocPresupuestario"].ToString().Trim(),
                        ldec_MontoIndicado,
                        lstr_NroValor + "." + lstr_Nemotecnico,//pk
                        tira.get_operation_name(lstr_IdOperacion1, "DI"),//texto2
                        "",//lstr_Moneda,//tipo
                        lstr_IdOperacion1 + "." + lstr_NomOperacion1//operacion
                        );
                }

                lstr_Referencia = lstr_NroValor.Trim().Equals("00") ? "No Asociado" : lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Costo Transaccion";

                ldat_Asiento.Rows.Add(
                    lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                    FchCont,
                    ldat_AsientoPago.Rows[0]["IdCuentaContable2"].ToString().Trim(),
                    ldat_AsientoPago.Rows[0]["IdClaveContable2"].ToString().Trim(),
                    ldat_AsientoPago.Rows[0]["CodigoAuxiliar"].ToString().Trim(),
                    lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim(),
                    ldat_AsientoPago.Rows[0]["IdCentroCosto2"].ToString().Trim(),
                    ldat_AsientoPago.Rows[0]["IdCentroBeneficio2"].ToString().Trim(),
                    ldat_AsientoPago.Rows[0]["IdElementoPEP2"].ToString().Trim(),
                    ldat_AsientoPago.Rows[0]["IdPosPre2"].ToString().Trim(),
                    ldat_AsientoPago.Rows[0]["IdCentroGestor2"].ToString().Trim(),
                    ldat_AsientoPago.Rows[0]["IdFondo2"].ToString().Trim(),
                    ldat_AsientoPago.Rows[0]["DocPresupuestario2"].ToString().Trim(),
                    ldat_AsientoPago.Rows[0]["PosDocPresupuestario2"].ToString().Trim(),
                    ldec_MontoIndicado,
                    lstr_NroValor + "." + lstr_Nemotecnico,//pk
                    tira.get_operation_name(lstr_IdOperacion1, "DI"),//texto2
                    "",//lstr_Moneda,//tipo
                    lstr_IdOperacion1 + "." + lstr_NomOperacion1 //operacion
                    );
                //
                GenerarAsientoCostoTransaccion(ldat_Asiento, lstr_IdOperacion1 + " " + lstr_Nemotecnico, lstr_IdOperacion1, lstr_NroValor + " - " + lstr_Nemotecnico, ldec_TpoCambio, lint_IdCostoTransaccion, ldt_FchModifica);
            }
            catch(Exception ex)
            {

            }
        }

        public static string GenerarAsientoCostoTransaccion(DataTable ldat_Asiento, string str_Accion, string str_IDOperacion, string str_IdTransaccion, decimal ldec_MontoIndicado,
             int lint_IdCostoTransaccion, DateTime ldt_FchModifica)
        {
            //Coleccion de asientos y tipos de asientos requeridos en SAP expuestos por la referencia del servicio
            wrSigafAsientos.ZfiAsiento item_asiento = new wrSigafAsientos.ZfiAsiento();
            wrSigafAsientos.ZfiAsiento[] tabla_asientos = new wrSigafAsientos.ZfiAsiento[ldat_Asiento.Rows.Count];

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
                    item_asiento = new wrSigafAsientos.ZfiAsiento();
                    int index = ldat_Asiento.Rows.IndexOf(ldr_Row);
                    if (index == 0)
                    {

                        item_asiento.Blart = "ID";//Clase de documento
                        item_asiento.Bukrs = "G206";//Sociedad
                        item_asiento.Bldat = ldat_Asiento.Rows[index]["Fecha"].ToString();//Fecha de documento
                        item_asiento.Budat = ldat_Asiento.Rows[index]["Fecha"].ToString();//Fecha de contabilización
                        item_asiento.Bktxt = ldat_Asiento.Rows[index]["Referencia"].ToString();//Texto de cabecera
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
                        item_asiento.Kursf = ldec_MontoIndicado;

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
                    logAsiento += x + " - " + item_resAsientosLog[j] + " - ";
                }
                //Registrar en Bitacora de movimientos
                string str_AuxAccion = (str_IDOperacion.Equals("ID01") || str_IDOperacion.Equals("ID02")) ? "Devengo Costo Transacción" : "Pagos Costo Transacción";
                string lstr_resultado = bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(str_IDOperacion, "DI"), "Resultado de Contabilización: " + logAsiento, str_IDOperacion, str_IdTransaccion, "");

                //Marcar registro como contabilizado
                string[] a = new string[2];
                if (!logAsiento.Contains("[E]"))
                    lcls_CostoTransaccion.ContabilizarCalculosFinancieros(
                        "CostosTransaccion",
                        lint_IdCostoTransaccion.ToString(),
                        null,
                        null,
                        "C",
                        "SG",
                        ldt_FchModifica, out a[0], out a[1]);
                return logAsiento;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}