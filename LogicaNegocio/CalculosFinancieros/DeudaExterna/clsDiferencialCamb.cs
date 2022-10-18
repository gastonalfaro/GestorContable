using System;
using System.Globalization;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using LogicaNegocio.Mantenimiento;
using LogicaNegocio.Seguridad;

namespace LogicaNegocio.CalculosFinancieros.DeudaExterna
{
    public class clsDiferencialCamb
    {
        //private static wsDeudaInterna.wsDeudaInterna wsDeudaInterna = new wsDeudaInterna.wsDeudaInterna();
        //private static ws_SGService.wsSistemaGestor ws_SGService = new ws_SGService.wsSistemaGestor();
        //private static wsAsientos.ServicioContable wsAsientos = new wsAsientos.ServicioContable(); 
        private static clsTiposAsiento tasientos = new clsTiposAsiento();
        private static Mantenimiento.clsTiposCambio tipocambio = new Mantenimiento.clsTiposCambio();
        private static Mantenimiento.clsPropietarios propietario = new Mantenimiento.clsPropietarios();
        private static Mantenimiento.clsNemotecnicos nemotecnico = new Mantenimiento.clsNemotecnicos();
        private static Mantenimiento.clsTiposAsiento tipoasiento = new Mantenimiento.clsTiposAsiento();
        private static Mantenimiento.clsReservasDetalle reservas = new Mantenimiento.clsReservasDetalle();
        private static Seguridad.tBitacora bitacora = new Seguridad.tBitacora();
        private static clsSaldoDeudaExt saldos = new clsSaldoDeudaExt();
        private static Mantenimiento.clsDinamico dinamica = new Mantenimiento.clsDinamico();
        private static string lstr_separador_decimal = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;

        public string Diferencial(string lstr_IdPrestamo, int? lint_IdTramo, DateTime ldt_FchFin)
        {
            string lstr_Mensaje = "Proceso Finalizado. Verifique bitácora. ";
            bool lbn_Error = false;
            try
            {

                //DateTime date = DateTime.Today;
                DateTime inicioMes = new DateTime(ldt_FchFin.Year, ldt_FchFin.Month, 1);

                inicioMes = inicioMes.AddDays(-1);

                //DataTable ldat_TitulosValores = titulo.ConsultarTituloValor("%", "%", "%", "%", "%", "01/01/1900", "01/01/5000").Tables[0].Select("IndicadorCupon ='V'").CopyToDataTable();
                DataTable ldat_SaldosDE = saldos.ConsultarSaldosDeudaExt(lstr_IdPrestamo, lint_IdTramo, inicioMes, ldt_FchFin).Tables[0];

                for (int i = 0; i < ldat_SaldosDE.Rows.Count; i++)
                {
                    try
                    {
                        if (ldat_SaldosDE.Rows[i]["IdMoneda"].ToString() != "USD")
                        {
                            lstr_Mensaje = DiferencialCambiario(
                                ldat_SaldosDE.Rows[i]["IdPrestamo"].ToString(),
                                Convert.ToInt32(ldat_SaldosDE.Rows[i]["IdTramo"].ToString()),
                                ldat_SaldosDE.Rows[i]["Acreedor"].ToString(),
                                ldat_SaldosDE.Rows[i]["IdMoneda"].ToString(),
                                Convert.ToDecimal(ldat_SaldosDE.Rows[i]["SaldoInicial"].ToString()),
                                Convert.ToDecimal(ldat_SaldosDE.Rows[i]["Desembolsos"].ToString()),
                                Convert.ToDecimal(ldat_SaldosDE.Rows[i]["Amortizaciones"].ToString()),
                                Convert.ToDecimal(ldat_SaldosDE.Rows[i]["Intereses"].ToString()),
                                Convert.ToDecimal(ldat_SaldosDE.Rows[i]["Comisiones"].ToString()),
                                Convert.ToDecimal(ldat_SaldosDE.Rows[i]["Saldo Final"].ToString()),
                                ldt_FchFin);
                        }
                    }
                    catch(Exception ex)
                    {
                        lbn_Error = true;
                        bitacora.ufnRegistrarAccionBitacora("DE", "123", "Diferencial Cambiario", "Error " + ex.ToString(), "Diferencial", ldat_SaldosDE.Rows[i]["IdPrestamo"].ToString() + "-" + ldat_SaldosDE.Rows[i]["IdTramo"].ToString(), "");
                        lstr_Mensaje += " con errores, verifique la bitácora"; 
                    }
                }
            }
            catch (Exception ex)
            {
                lbn_Error = true;

                lstr_Mensaje += ex.ToString();
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

        public string DiferencialCambiario(
            string IdPrestamo,
            int IdTramo,
            string Acreedor,
            string IdMoneda,
            decimal SaldoInicial,
            decimal Desembolsos,
            decimal Amortizaciones,
            decimal Intereses,
            decimal Comisiones,
            decimal Saldo_x0020_Final,
            DateTime date )
        {
            string lstr_Mensaje = "Código 00: Proceso Finalizado!";
            string lstr_Moneda = "USD";
            DataSet ds_tipoCambio = new DataSet();
            string str_MonedaQry1 = "CRCN";
            string lstr_TransaccionVentaUSD = "3140";
            string lstr_TransaccionCompraUSD = "3280";
            string lstr_Log = string.Empty;
            wsDeudaInterna.wsDeudaInterna wsDeudaInterna = new wsDeudaInterna.wsDeudaInterna();
            clsAmortizacion tAmortiza = new clsAmortizacion();
            DataTable ldat_Asiento = RegistroContable();
            DataTable ldat_Tira = new DataTable();
            DataSet lds_Tira = new DataSet();
            string lstr_Operacion = string.Empty;
            string lstr_OperacionCP = string.Empty;

            #region obtiene los tipos de cambio actuales y pasados

            //DateTime date = DateTime.Today;
            DateTime inicioMes = new DateTime(date.Year, date.Month, 1);
            DateTime finMes = inicioMes.AddMonths(1).AddDays(-1);
            inicioMes = inicioMes.AddDays(-1);
            decimal ldec_TiposCambiosCierre = Convert.ToDecimal(tipocambio.ConsultarTiposCambio(IdMoneda, finMes, string.Empty, "N").Tables[0].Rows[0]["Valor"].ToString());
            decimal ldec_TiposCambiosInicioMes = Convert.ToDecimal(tipocambio.ConsultarTiposCambio(IdMoneda, inicioMes, string.Empty, "N").Tables[0].Rows[0]["Valor"].ToString());
            
            #endregion

            #region obtiene diferencia de monto actual y monto pasado

            decimal ldec_MontoCierre = 0;
            decimal ldec_MontoInicial = 0;
            decimal ldec_monto;
            decimal ldec_CP = 0;
            decimal ldec_CPIni = 0;
            decimal ldec_MontoCierreCP = 0;
            decimal ldec_MontoInicialCP = 0;
            decimal ldec_tipo_cambioUSD = 0;
            if (IdMoneda == "EUR")
            {
                ldec_MontoCierre = Saldo_x0020_Final * ldec_TiposCambiosCierre;
                ldec_MontoInicial = SaldoInicial * ldec_TiposCambiosInicioMes;
            }
            else
            {
                ldec_MontoCierre = Saldo_x0020_Final / ldec_TiposCambiosCierre;
                ldec_MontoInicial = SaldoInicial / ldec_TiposCambiosInicioMes;
            }
            ldec_monto = ldec_MontoCierre - ldec_MontoInicial;

            #endregion

            #region mensajes varios

            string lstr_Referencia = "";
            string lstr_Detalle = "Diferencial Cambiario Canasta Monedas";
            string lstr_Plazo = "LP";

            #endregion

            #region CortoPlazo

            DataSet ds_Tramos = tAmortiza.ConsultarAmortizacion(IdPrestamo, IdTramo, null, null, inicioMes.AddDays(1), "", null, inicioMes.AddDays(1).AddYears(1));//saco los tramos
            if (ds_Tramos.Tables.Count > 0 && ds_Tramos.Tables["Table"].Rows.Count > 0)
            {

                DataTable dt_Tramos = ds_Tramos.Tables[0];
                foreach (DataRow dr_Tramo in dt_Tramos.Rows)
                {
                    ldec_CPIni += Convert.ToDecimal(dr_Tramo["Monto"]);
                }
            }

            ds_Tramos = tAmortiza.ConsultarAmortizacion(IdPrestamo, IdTramo, null, null, finMes, "", null, finMes.AddYears(1));//saco los tramos
            if (ds_Tramos.Tables.Count > 0 && ds_Tramos.Tables["Table"].Rows.Count > 0)
            {

                DataTable dt_Tramos = ds_Tramos.Tables[0];
                foreach (DataRow dr_Tramo in dt_Tramos.Rows)
                {
                    ldec_CP += Convert.ToDecimal(dr_Tramo["Monto"]);
                }
            }
            if (IdMoneda == "EUR")
            {
                ldec_MontoCierreCP = ldec_CP * ldec_TiposCambiosCierre;
                ldec_MontoInicialCP = ldec_CPIni * ldec_TiposCambiosInicioMes;
            }
            else
            {
                ldec_MontoCierreCP = ldec_CP / ldec_TiposCambiosCierre;
                ldec_MontoInicialCP = ldec_CPIni / ldec_TiposCambiosInicioMes;
            }
            ldec_CP = ldec_MontoCierreCP - ldec_MontoInicialCP;

            #endregion CortoPlazo
        


            #region contabilizar CXP

            

            ds_tipoCambio = tipocambio.ConsultarTiposCambio(str_MonedaQry1, finMes, lstr_TransaccionVentaUSD, "N");
            if (ds_tipoCambio.Tables.Count > 0 && ds_tipoCambio.Tables["Table"].Rows.Count > 0)
            {
                // se realiza el cambio a dolares para procesar el asiento
                ldec_tipo_cambioUSD = Convert.ToDecimal(ds_tipoCambio.Tables["Table"].Rows[0]["Valor"]);
            }

            #region obtiene la operación que se va a realizar

            //if (IdPrestamo == "29317000" || IdPrestamo == "24145000" || IdPrestamo == "29320000")
            //{
            //    if (ldec_monto >= 0)
            //        lstr_Operacion = "DC CXP+";
            //    else
            //        lstr_Operacion = "DC CXP-";
            //}
            //else
            //{
            if (ldec_monto >= 0)
                lstr_Operacion = "DC CXP-";
            else
                lstr_Operacion = "DC CXP+";
            if (ldec_CP >= 0)
                lstr_OperacionCP = "DC CXP-";
            else
                lstr_OperacionCP = "DC CXP+";
            //}

            #endregion
            #region CXP LP
            try
            {
                lstr_Plazo = "LP";
                ldat_Asiento.Rows.Clear();
                #region obtiene la tira para contabilizar la diferencia
                lds_Tira = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DE')", lstr_Operacion, "", "", lstr_Plazo, Acreedor, "CAPITAL", "", "", "");
                ldat_Tira = lds_Tira.Tables[0];
                //foreach (DataRow dr_tira in ldat_Tira.Rows)
                 //   ldat_Tira.ImportRow(dr_tira);

                #endregion


                #region prepara el asiento para contabilizar

                if (ldat_Tira.Rows.Count > 0)
                {

                    foreach (DataRow ldr_Row in ldat_Tira.Rows)
                    {
                        decimal ldec_MontoLinea40 = 0;
                        decimal ldec_MontoLinea50 = 0;
                        lstr_Referencia = IdPrestamo.Trim() + "-" + IdTramo.ToString().Trim() + " Diferencial";
                        if (string.IsNullOrEmpty(ldr_Row["CodigoAuxiliar5"].ToString().Trim()))
                            ldr_Row["CodigoAuxiliar5"] = "100";

                        if (string.IsNullOrEmpty(ldr_Row["CodigoAuxiliar6"].ToString().Trim()))
                            ldr_Row["CodigoAuxiliar6"] = "100";
                        ldr_Row["CodigoAuxiliar5"] = ldr_Row["CodigoAuxiliar5"].ToString().Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal);
                        ldr_Row["CodigoAuxiliar6"] = ldr_Row["CodigoAuxiliar6"].ToString().Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal);

                        
                            if (ldr_Row["IdClaveContable"].ToString().Trim() == "40")
                            {
                                ldec_MontoLinea40 = (ldec_monto - ldec_CP) * (Convert.ToDecimal(ldr_Row["CodigoAuxiliar5"]) / 100);
                            }
                            if (ldr_Row["IdClaveContable2"].ToString().Trim() == "50")
                            {
                                ldec_MontoLinea50 = (ldec_monto - ldec_CP) * (Convert.ToDecimal(ldr_Row["CodigoAuxiliar6"]) / 100);
                            }

                            ldat_Asiento.Rows.Add(
                                lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                date.ToString("dd.MM.yyyy"),
                                ldr_Row["IdCuentaContable"].ToString().Trim(),
                                ldr_Row["IdClaveContable"].ToString().Trim(),
                                lstr_Moneda,
                                lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim(),
                                ldr_Row["IdCentroCosto"].ToString().Trim(),
                                ldr_Row["IdCentroBeneficio"].ToString().Trim(),
                                ldr_Row["IdElementoPEP"].ToString().Trim(),
                                ldr_Row["IdPosPre"].ToString().Trim().ToUpper(),
                                ldr_Row["IdCentroGestor"].ToString().Trim(),
                                ldr_Row["IdFondo"].ToString().Trim(),
                                ldr_Row["DocPresupuestario"].ToString().Trim(),
                                ldr_Row["PosDocPresupuestario"].ToString().Trim(),
                                Math.Abs(Math.Round(ldec_MontoLinea40, 2))
                            );
                            ldat_Asiento.Rows.Add(
                                lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                date.ToString("dd.MM.yyyy"),
                                ldr_Row["IdCuentaContable2"].ToString().Trim(),
                                ldr_Row["IdClaveContable2"].ToString().Trim(),
                                lstr_Moneda,
                                lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim(),
                                ldr_Row["IdCentroCosto2"].ToString().Trim(),
                                ldr_Row["IdCentroBeneficio2"].ToString().Trim(),
                                ldr_Row["IdElementoPEP2"].ToString().Trim(),
                                ldr_Row["IdPosPre2"].ToString().Trim(),
                                ldr_Row["IdCentroGestor2"].ToString().Trim(),
                                ldr_Row["IdFondo2"].ToString().Trim(),
                                ldr_Row["DocPresupuestario2"].ToString().Trim(),
                                ldr_Row["PosDocPresupuestario2"].ToString().Trim(),
                                Math.Abs(Math.Round(ldec_MontoLinea50, 2))
                            );
                        

                    }

                    lstr_Log = GenerarAsientoAjuste("Diferencial Cambiario", ldat_Asiento, lstr_Operacion, IdPrestamo, IdTramo.ToString(), ldec_tipo_cambioUSD);

                    
                }
                else
                {
                    lstr_Log = bitacora.ufnRegistrarAccionBitacora("DE", "123", "Diferencial Cambiario", "Resultado de Contabilización: " + "No se localizó la tira para la operación correspondiente", lstr_Operacion, IdPrestamo + "-" + IdTramo.ToString(), "");
                }
                #endregion

            }
            catch (Exception e)
            {
                lstr_Mensaje = "Código 99: " + e.ToString();
            }
            #endregion CXP LP
            #region CXP CP
            try
            {
                lstr_Plazo = "CP";
                ldat_Asiento.Rows.Clear();
                #region obtiene la tira para contabilizar la diferencia
                lds_Tira = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DE')", lstr_OperacionCP, "", "", lstr_Plazo, Acreedor, "CAPITAL", "", "", "");
                ldat_Tira = lds_Tira.Tables[0];
                //foreach (DataRow dr_tira in ldat_Tira.Rows)
                //   ldat_Tira.ImportRow(dr_tira);

                #endregion


                #region prepara el asiento para contabilizar

                if (ldat_Tira.Rows.Count > 0)
                {

                    foreach (DataRow ldr_Row in ldat_Tira.Rows)
                    {
                        decimal ldec_MontoLinea40 = 0;
                        decimal ldec_MontoLinea50 = 0;
                        lstr_Referencia = IdPrestamo.Trim() + "-" + IdTramo.ToString().Trim() + " Diferencial";
                        if (string.IsNullOrEmpty(ldr_Row["CodigoAuxiliar5"].ToString().Trim()))
                            ldr_Row["CodigoAuxiliar5"] = "100";

                        if (string.IsNullOrEmpty(ldr_Row["CodigoAuxiliar6"].ToString().Trim()))
                            ldr_Row["CodigoAuxiliar6"] = "100";

                        ldr_Row["CodigoAuxiliar5"] = ldr_Row["CodigoAuxiliar5"].ToString().Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal);
                        ldr_Row["CodigoAuxiliar6"] = ldr_Row["CodigoAuxiliar6"].ToString().Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal);
                       
                            if (ldr_Row["IdClaveContable"].ToString().Trim() == "40")
                            {
                                ldec_MontoLinea40 = (ldec_CP) * (Convert.ToDecimal(ldr_Row["CodigoAuxiliar5"]) / 100);
                            }
                            if (ldr_Row["IdClaveContable2"].ToString().Trim() == "50")
                            {
                                ldec_MontoLinea50 = (ldec_CP) * (Convert.ToDecimal(ldr_Row["CodigoAuxiliar6"]) / 100);
                            }
                            ldat_Asiento.Rows.Add(
                                 lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                 date.ToString("dd.MM.yyyy"),
                                 ldr_Row["IdCuentaContable"].ToString().Trim(),
                                 ldr_Row["IdClaveContable"].ToString().Trim(),
                                 lstr_Moneda,
                                 lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim(),
                                 ldr_Row["IdCentroCosto"].ToString().Trim(),
                                 ldr_Row["IdCentroBeneficio"].ToString().Trim(),
                                 ldr_Row["IdElementoPEP"].ToString().Trim(),
                                 ldr_Row["IdPosPre"].ToString().Trim().ToUpper(),
                                 ldr_Row["IdCentroGestor"].ToString().Trim(),
                                 ldr_Row["IdFondo"].ToString().Trim(),
                                 ldr_Row["DocPresupuestario"].ToString().Trim(),
                                 ldr_Row["PosDocPresupuestario"].ToString().Trim(),
                                 Math.Abs(Math.Round(ldec_MontoLinea40, 2))
                             );
                            ldat_Asiento.Rows.Add(
                                lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                date.ToString("dd.MM.yyyy"),
                                ldr_Row["IdCuentaContable2"].ToString().Trim(),
                                ldr_Row["IdClaveContable2"].ToString().Trim(),
                                lstr_Moneda,
                                lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim(),
                                ldr_Row["IdCentroCosto2"].ToString().Trim(),
                                ldr_Row["IdCentroBeneficio2"].ToString().Trim(),
                                ldr_Row["IdElementoPEP2"].ToString().Trim(),
                                ldr_Row["IdPosPre2"].ToString().Trim(),
                                ldr_Row["IdCentroGestor2"].ToString().Trim(),
                                ldr_Row["IdFondo2"].ToString().Trim(),
                                ldr_Row["DocPresupuestario2"].ToString().Trim(),
                                ldr_Row["PosDocPresupuestario2"].ToString().Trim(),
                                Math.Abs(Math.Round(ldec_MontoLinea50, 2))
                            );
                       
                    }

                    lstr_Log = GenerarAsientoAjuste("Diferencial Cambiario", ldat_Asiento, lstr_OperacionCP, IdPrestamo, IdTramo.ToString(), ldec_tipo_cambioUSD);


                }
                else
                {
                    lstr_Log = bitacora.ufnRegistrarAccionBitacora("DE", "123", "Diferencial Cambiario", "Resultado de Contabilización: " + "No se localizó la tira para la operación correspondiente", lstr_Operacion, IdPrestamo + "-" + IdTramo.ToString(), "");
                }
                #endregion

            }
            catch (Exception e)
            {
                lstr_Mensaje = "Código 99: " + e.ToString();
            }
            
            #endregion CXCP CP

            #endregion contabilizar CXP

            #region contabilizar CXC


            ds_tipoCambio = tipocambio.ConsultarTiposCambio(str_MonedaQry1, finMes, lstr_TransaccionCompraUSD, "N");
            if (ds_tipoCambio.Tables.Count > 0 && ds_tipoCambio.Tables["Table"].Rows.Count > 0)
            {
                // se realiza el cambio a dolares para procesar el asiento
                ldec_tipo_cambioUSD = Convert.ToDecimal(ds_tipoCambio.Tables["Table"].Rows[0]["Valor"]);
            }

            #region obtiene la operación que se va a realizar

            //if (IdPrestamo == "29317000" || IdPrestamo == "24145000" || IdPrestamo == "29320000")
            //{
            //    if (ldec_monto >= 0)
            //        lstr_Operacion = "DC CXP+";
            //    else
            //        lstr_Operacion = "DC CXP-";
            //}
            //else
            //{
            if (ldec_monto >= 0)
                lstr_Operacion = "DC CXC+";
            else
                lstr_Operacion = "DC CXC-";

            if (ldec_CP >= 0)
                lstr_OperacionCP = "DC CXC+";
            else
                lstr_OperacionCP = "DC CXC-";
            //}

            #endregion

            #region CXC LP
            try
            {
                lstr_Plazo = "LP";
                ldat_Asiento.Rows.Clear();

                #region obtiene la tira para contabilizar la diferencia
                lds_Tira = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DE')", lstr_Operacion, "", "", lstr_Plazo, Acreedor, "CAPITAL", IdPrestamo, "", "");
                ldat_Tira = lds_Tira.Tables[0];
                //foreach (DataRow dr_tira in ldat_Tira.Rows)
                //   ldat_Tira.ImportRow(dr_tira);

                #endregion


                #region prepara el asiento para contabilizar

                if (ldat_Tira.Rows.Count > 0)
                {

                    foreach (DataRow ldr_Row in ldat_Tira.Rows)
                    {
                        
                        decimal ldec_MontoLinea40 = 0;
                        decimal ldec_MontoLinea50 = 0;
                        lstr_Referencia = IdPrestamo.Trim() + "-" + IdTramo.ToString().Trim() + " Diferencial";
                        if (string.IsNullOrEmpty(ldr_Row["CodigoAuxiliar5"].ToString().Trim()))
                            ldr_Row["CodigoAuxiliar5"] = "100";

                        if (string.IsNullOrEmpty(ldr_Row["CodigoAuxiliar6"].ToString().Trim()))
                            ldr_Row["CodigoAuxiliar6"] = "100";

                        ldr_Row["CodigoAuxiliar5"] = ldr_Row["CodigoAuxiliar5"].ToString().Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal);
                        ldr_Row["CodigoAuxiliar6"] = ldr_Row["CodigoAuxiliar6"].ToString().Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal);
                        
                            if (ldr_Row["IdClaveContable"].ToString().Trim() == "40")
                            {
                                ldec_MontoLinea40 = ((ldec_monto - ldec_CP)) * ((Convert.ToDecimal(ldr_Row["CodigoAuxiliar5"]) / 100));
                            }
                            if (ldr_Row["IdClaveContable2"].ToString().Trim() == "50")
                            {
                                ldec_MontoLinea50 = (ldec_monto - ldec_CP) * ((Convert.ToDecimal(ldr_Row["CodigoAuxiliar6"]) / 100));
                            }
                        ldat_Asiento.Rows.Add(
                            lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                            date.ToString("dd.MM.yyyy"),
                            ldr_Row["IdCuentaContable"].ToString().Trim(),
                            ldr_Row["IdClaveContable"].ToString().Trim(),
                            lstr_Moneda,//ldr_Row["CodigoAuxiliar"].ToString().Trim(),
                            lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim(),
                            ldr_Row["IdCentroCosto"].ToString().Trim(),
                            ldr_Row["IdCentroBeneficio"].ToString().Trim(),
                            ldr_Row["IdElementoPEP"].ToString().Trim(),
                            ldr_Row["IdPosPre"].ToString().Trim().ToUpper(),
                            ldr_Row["IdCentroGestor"].ToString().Trim(),
                            ldr_Row["IdFondo"].ToString().Trim(),
                            ldr_Row["DocPresupuestario"].ToString().Trim(),
                            ldr_Row["PosDocPresupuestario"].ToString().Trim(),
                            Math.Abs(Math.Round(ldec_MontoLinea40, 2))
                        );
                        ldat_Asiento.Rows.Add(
                            lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                            date.ToString("dd.MM.yyyy"),
                            ldr_Row["IdCuentaContable2"].ToString().Trim(),
                            ldr_Row["IdClaveContable2"].ToString().Trim(),
                            lstr_Moneda,//ldr_Row["CodigoAuxiliar"].ToString().Trim(),
                            lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim(),
                            ldr_Row["IdCentroCosto2"].ToString().Trim(),
                            ldr_Row["IdCentroBeneficio2"].ToString().Trim(),
                            ldr_Row["IdElementoPEP2"].ToString().Trim(),
                            ldr_Row["IdPosPre2"].ToString().Trim(),
                            ldr_Row["IdCentroGestor2"].ToString().Trim(),
                            ldr_Row["IdFondo2"].ToString().Trim(),
                            ldr_Row["DocPresupuestario2"].ToString().Trim(),
                            ldr_Row["PosDocPresupuestario2"].ToString().Trim(),
                            Math.Abs(Math.Round(ldec_MontoLinea50, 2))
                        );
                        
                    }

                    lstr_Log = GenerarAsientoAjuste("Diferencial Cambiario", ldat_Asiento, lstr_Operacion, IdPrestamo, IdTramo.ToString(), ldec_tipo_cambioUSD);


                }
                //else
                //{
                //    lstr_Log = bitacora.ufnRegistrarAccionBitacora("DE", "123", "Diferencial Cambiario", "Resultado de Contabilización: " + "No se localizó la tira para la operación correspondiente", lstr_Operacion, IdPrestamo + "-" + IdTramo.ToString(), "");
                //}
                #endregion

            }
            catch (Exception e)
            {
                lstr_Mensaje = "Código 99: " + e.ToString();
            }
            #endregion CXC LP
            #region CXC CP
            try
            {
                lstr_Plazo = "CP";
                ldat_Asiento.Rows.Clear();

                #region obtiene la tira para contabilizar la diferencia
                lds_Tira = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DE')", lstr_OperacionCP, "", "", lstr_Plazo, Acreedor, "CAPITAL", IdPrestamo, "", "");
                ldat_Tira = lds_Tira.Tables[0];
                //foreach (DataRow dr_tira in ldat_Tira.Rows)
                //   ldat_Tira.ImportRow(dr_tira);

                #endregion


                #region prepara el asiento para contabilizar

                if (ldat_Tira.Rows.Count > 0)
                {

                    foreach (DataRow ldr_Row in ldat_Tira.Rows)
                    {

                        decimal ldec_MontoLinea40 = 0;
                        decimal ldec_MontoLinea50 = 0;
                        lstr_Referencia = IdPrestamo.Trim() + "-" + IdTramo.ToString().Trim() + " Diferencial";
                        if (string.IsNullOrEmpty(ldr_Row["CodigoAuxiliar5"].ToString().Trim()))
                            ldr_Row["CodigoAuxiliar5"] = "100";

                        if (string.IsNullOrEmpty(ldr_Row["CodigoAuxiliar6"].ToString().Trim()))
                            ldr_Row["CodigoAuxiliar6"] = "100";

                        ldr_Row["CodigoAuxiliar5"] = ldr_Row["CodigoAuxiliar5"].ToString().Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal);
                        ldr_Row["CodigoAuxiliar6"] = ldr_Row["CodigoAuxiliar6"].ToString().Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal);
                        
                            if (ldr_Row["IdClaveContable"].ToString().Trim() == "40")
                            {
                                ldec_MontoLinea40 = (ldec_CP) * (Convert.ToDecimal(ldr_Row["CodigoAuxiliar5"]) / 100);
                            }
                            if (ldr_Row["IdClaveContable2"].ToString().Trim() == "50")
                            {
                                ldec_MontoLinea50 = (ldec_CP) * (Convert.ToDecimal(ldr_Row["CodigoAuxiliar6"]) / 100);
                            }
                            ldat_Asiento.Rows.Add(
                                 lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                 date.ToString("dd.MM.yyyy"),
                                 ldr_Row["IdCuentaContable"].ToString().Trim(),
                                 ldr_Row["IdClaveContable"].ToString().Trim(),
                                 lstr_Moneda,
                                 lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim(),
                                 ldr_Row["IdCentroCosto"].ToString().Trim(),
                                 ldr_Row["IdCentroBeneficio"].ToString().Trim(),
                                 ldr_Row["IdElementoPEP"].ToString().Trim(),
                                 ldr_Row["IdPosPre"].ToString().Trim().ToUpper(),
                                 ldr_Row["IdCentroGestor"].ToString().Trim(),
                                 ldr_Row["IdFondo"].ToString().Trim(),
                                 ldr_Row["DocPresupuestario"].ToString().Trim(),
                                 ldr_Row["PosDocPresupuestario"].ToString().Trim(),
                                 Math.Abs(Math.Round(ldec_MontoLinea40, 2))
                             );
                            ldat_Asiento.Rows.Add(
                                lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                date.ToString("dd.MM.yyyy"),
                                ldr_Row["IdCuentaContable2"].ToString().Trim(),
                                ldr_Row["IdClaveContable2"].ToString().Trim(),
                                lstr_Moneda,
                                lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim(),
                                ldr_Row["IdCentroCosto2"].ToString().Trim(),
                                ldr_Row["IdCentroBeneficio2"].ToString().Trim(),
                                ldr_Row["IdElementoPEP2"].ToString().Trim(),
                                ldr_Row["IdPosPre2"].ToString().Trim(),
                                ldr_Row["IdCentroGestor2"].ToString().Trim(),
                                ldr_Row["IdFondo2"].ToString().Trim(),
                                ldr_Row["DocPresupuestario2"].ToString().Trim(),
                                ldr_Row["PosDocPresupuestario2"].ToString().Trim(),
                                Math.Abs(Math.Round(ldec_MontoLinea50, 2))
                            );
                        
                    }

                    lstr_Log = GenerarAsientoAjuste("Diferencial Cambiario", ldat_Asiento, lstr_OperacionCP, IdPrestamo, IdTramo.ToString(), ldec_tipo_cambioUSD);


                }
                //else
                //{
                //    lstr_Log = bitacora.ufnRegistrarAccionBitacora("DE", "123", "Diferencial Cambiario", "Resultado de Contabilización: " + "No se localizó la tira para la operación correspondiente", lstr_Operacion, IdPrestamo + "-" + IdTramo.ToString(), "");
                //}
                #endregion

            }
            catch (Exception e)
            {
                lstr_Mensaje = "Código 99: " + e.ToString();
            }
            #endregion CXC CP
            #endregion contabilizar CXC
            return lstr_Mensaje;
        }

        public static string GenerarAsientoAjuste(string lstr_TipoCancelacion, DataTable ldat_Asiento, string lstr_IdOperacion, string lstr_NroValor, string lstr_Nemotecnico, decimal ldec_TipoCambio)
        {
            //Coleccion de asientos y tipos de asientos requeridos en SAP expuestos por la referencia del servicio
            wrSigafAsientos.ZfiAsiento item_asiento = new wrSigafAsientos.ZfiAsiento();
            wrSigafAsientos.ZfiAsiento[] tabla_asientos = new wrSigafAsientos.ZfiAsiento[ldat_Asiento.Rows.Count];

            //variables de proceso
            string[] item_resAsientosLog = new string[10];
            string logAsiento = string.Empty;
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
                    item_asiento.Fipex = ldat_Asiento.Rows[index]["PosPre"].ToString();//Posición Presupuestaria
                    item_asiento.Fistl = ldat_Asiento.Rows[index]["CentroGestor"].ToString();//Centro Gestor
                    item_asiento.Geber = ldat_Asiento.Rows[index]["Fondo"].ToString();//Fondo
                    item_asiento.Kblnr = ldat_Asiento.Rows[index]["DocPres"].ToString();//Documento Presupuestario
                    item_asiento.Kblpos = ldat_Asiento.Rows[index]["PosDocPres"].ToString();//Posición de documento presupuestario

                    item_asiento.Xblnr = ldat_Asiento.Rows[index]["PKMovimiento"].ToString();//
                    item_asiento.Bktxt = ldat_Asiento.Rows[index]["Texto2"].ToString();//
                    item_asiento.Xref1Hd = ldat_Asiento.Rows[index]["Ref1Tipo"].ToString();//
                    item_asiento.Xref2Hd = ldat_Asiento.Rows[index]["Ref2Operacion"].ToString();//
                    //item_asiento.Kursf = 0;
                    if (ldat_Asiento.Rows[index]["Moneda"].ToString() == "USD")
                        item_asiento.Kursf = Convert.ToDecimal(ldec_TipoCambio.ToString("0.0000"));

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
                bitacora.ufnRegistrarAccionBitacora("DE", "123", lstr_TipoCancelacion, "Resultado de Contabilización: " + logAsiento, lstr_IdOperacion, lstr_NroValor + "-" + lstr_Nemotecnico, "");

                // convertir el 
                //Marcar registro como contabilizado

                string[] a = new string[2];                
                return logAsiento;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}