using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaInterna;
using LogicaNegocio.Mantenimiento;
using LogicaNegocio.Seguridad;

namespace LogicaNegocio.CalculosFinancieros.DeudaInterna
{
    public class clsContabilizaDifCambiario
    {
        private static clsTiposAsiento tasientos = new clsTiposAsiento();
        //private static wsAsientos.ServicioContable wsAsientos = new wsAsientos.ServicioContable();
        private static Mantenimiento.clsTiposCambio tipocambio = new Mantenimiento.clsTiposCambio();
        private static Mantenimiento.clsPropietarios propietario = new Mantenimiento.clsPropietarios();
        private static Mantenimiento.clsNemotecnicos nemotecnico = new Mantenimiento.clsNemotecnicos();
        private static Mantenimiento.clsTiposAsiento tipoasiento = new Mantenimiento.clsTiposAsiento();
        private static clsOperaciones loperacion = new clsOperaciones();
        private static Mantenimiento.clsReservasDetalle reservas = new Mantenimiento.clsReservasDetalle();
        private static Seguridad.tBitacora bitacora = new Seguridad.tBitacora();
        private static clsTituloValor titulo = new clsTituloValor();
        private static tiras tira = new tiras();
        private static clsCostoTransaccion lcls_CostoTransaccion = new clsCostoTransaccion();
        private static Mantenimiento.clsDinamico dinamica = new Mantenimiento.clsDinamico();

        private decimal Truncate(decimal value, int length)
        {
            return Math.Truncate(value * 100) / 100;
        }



        public string DifCambiario(DateTime? ldt_FchContabilizacion, string lstr_Nemotecnico = "")
        {
            string lstr_Mensaje = string.Empty;

            //DateTime date = DateTime.Today;
            DateTime inicioMes = new DateTime(Convert.ToDateTime( ldt_FchContabilizacion).Year, Convert.ToDateTime( ldt_FchContabilizacion).Month, 1);

            inicioMes = inicioMes.AddDays(-1);

            try
            {
                //DataSet ldst_DiferencialCambiario = dinamica.ConsultarDinamico("select * from cf.vTotalAuxiliarUdes " + " where DescripcionCuenta IN ('CAPITAL','IMP_DEV','INT_DEV','PRIMAS') AND (Nemotecnico= '" + lstr_Nemotecnico + "' OR ISNULL('" + lstr_Nemotecnico + "','')='' )");
                DataSet ldst_DiferencialCambiario = ConsultaSaldosNemotecnicosUDES(lstr_Nemotecnico,"","","",inicioMes,ldt_FchContabilizacion );
                DataTable ldat_DiferencialCambiario = ldst_DiferencialCambiario.Tables[0];

                //DataTable ldat_Nemotecnicos = nemotecnico.ConsultarNemotecnicos(null, null, null, null, null).Tables[0];

                //for (int i = 0; i < ldat_DiferencialCambiario.Rows.Count; i++)
                foreach (DataRow dr_DiferencialCambiario in ldat_DiferencialCambiario.Rows)
                {
                    try
                    {
                        lstr_Mensaje += DiferencialCambiario(
                            ldt_FchContabilizacion,
                            dr_DiferencialCambiario["Nemotecnico"].ToString(),//nemotecnico
                            dr_DiferencialCambiario["Propietario"].ToString(),//Tipo de Propietario
                            dr_DiferencialCambiario["Plazo"].ToString(),//Tipo de Plazo
                            dr_DiferencialCambiario["DescripcionCuenta"].ToString(),//Tipo de Cuenta
                            Convert.ToDecimal(dr_DiferencialCambiario["SaldoInicialCRC"].ToString()),//Total de Udes
                            Convert.ToDecimal(dr_DiferencialCambiario["MovimientosCRC"].ToString()),//Total de Udes en Colones
                            Convert.ToDecimal(dr_DiferencialCambiario["SaldoFinalCRC"].ToString()),//Total de Udes
                            ("Ntc: " + dr_DiferencialCambiario["Nemotecnico"].ToString() +
                            "Cta: " + dr_DiferencialCambiario["DescripcionCuenta"].ToString() +
                            "Plz: " + dr_DiferencialCambiario["Plazo"].ToString() +
                            "Prt: " + dr_DiferencialCambiario["Propietario"].ToString())//Detalle de documento
                            )+"\n";
                    }
                    catch (Exception e)
                    {
                        lstr_Mensaje += "Error en " + dr_DiferencialCambiario["Nemotecnico"].ToString() + " " + e.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                lstr_Mensaje = ex.ToString();
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

        public DataSet ConsultaSaldosNemotecnicosUDES(string lstr_Nemotecnico, string lstr_Plazo, string lstr_Propietario, string lstr_CuentaAfectada, DateTime? FechaDesde, DateTime? FechaHasta)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsReporteSaldosNemotecnicosUDES cr_Procedimiento = new clsReporteSaldosNemotecnicosUDES(lstr_Nemotecnico,lstr_Plazo,lstr_Propietario,lstr_CuentaAfectada,FechaDesde,FechaHasta);
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
        public string DiferencialCambiario(
            DateTime? ldt_FchContabilizacion,
            string lstr_Nemotecnico,
            string lstr_Propietario,
            string lstr_Plazo,
            string lstr_DescripcionCuenta,
            decimal ldec_TotalIniColones,
            decimal ldec_TotalMovColones,
            decimal ldec_TotalFinColones,
            string lstr_Detalle)
        {
            string lstr_Mensaje = string.Empty;//= lstr_Nemotecnico + "." + lstr_Propietario + "." + lstr_Plazo + "." + lstr_DescripcionCuenta; //string.Empty;
            wsDeudaInterna.wsDeudaInterna wsDeudaInterna = new wsDeudaInterna.wsDeudaInterna();
            DataTable ldat_Asiento = RegistroContable();
            DataTable ldat_Tira = new DataTable();
            DataTable ldat_Tira2 = new DataTable();
            string lstr_Operacion = string.Empty;
            string lstr_NomOperacion = string.Empty;
            decimal ldec_TipoCambioUDE = Convert.ToDecimal(tipocambio.ConsultarTiposCambio("UDE", ldt_FchContabilizacion, "", "N").Tables[0].Rows[0]["Valor"].ToString());
            decimal ldec_monto = 0;
            string lstr_Referencia = "";
            
            //diferencia de udes registrados contra valor actual de udes
            decimal diferencia = (ldec_TotalFinColones - ldec_TotalMovColones) - ldec_TotalIniColones;//(ldec_TotalUdes * ldec_TipoCambioUDE) - ldec_TotalUdesColones;

            if (diferencia == 0)
            {
                lstr_Mensaje += " No hubo diferencia para el DC: nemotécnico " + lstr_Nemotecnico + " Operación " + lstr_Operacion + " Propietario " + lstr_Propietario + " Plazo " + lstr_Plazo + " Cuenta " + lstr_DescripcionCuenta; 
            }
            else
            {
               
                //define si la diferencia es positiva o negativa
                bool esPositivo = diferencia >= 0 ? true : false; //como son deudas si se revalora es negativo para hacienda

                //define si no hubo errores al generar el asiento
                bool SinError = true;
                bool lbln_Asiento = false;
                #region Contabilización de diferencial

                //ID78 =             SALDO UDES POSITIVO Y AJUSTE DIFERENCIAL POSITIVO = Tira Diferencias de tipo de cambio negativa        40 GASTO Y 50 CTA 
                //ID77 =             SALDO UDES POSITIVO Y AJUSTE DIFERENCIAL NEGATIVO = Diferencias de tipo de cambio positiva                40 CTA Y 50 INGRESO 
                //ID80 =              SALDO NEGATIVO  Y AJUSTE DIFERENCIAL POSITIVO = Diferencias de tipo de cambio positiva                40 INGRESO Y 50 CTA 
                //ID79=               SALDO NEGATIVO  Y AJUSTE DIFERENCIAL NEGATIVO = Diferencias de tipo de cambio negativa                40 CTA Y 50 GASTO 

                if (esPositivo)
                {
                    if (ldec_TotalFinColones < 0){
                        lstr_Operacion = "ID80";
                    }
                    else
                    {
                        lstr_Operacion = "ID78";
                    }
                }
                else
                {
                    if (ldec_TotalFinColones < 0){
                        lstr_Operacion = "ID79";
                    }
                    else
                    {
                        lstr_Operacion = "ID77";
                    }
                }


                DataSet lds_Operaciones = loperacion.ConsultarOperaciones(lstr_Operacion, "IdModulo IN ('DI')", "");
                if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
                {
                    lstr_NomOperacion = lds_Operaciones.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
                }


                ldat_Tira = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID").Tables[0].Clone();

                ldat_Tira2 = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lstr_Propietario, "ID", lstr_Plazo, lstr_DescripcionCuenta).Tables[0];
                if (ldat_Tira2.Rows.Count > 0)
                    foreach (DataRow dr_tira in ldat_Tira2.Select("CodigoAuxiliar2 = '" + lstr_Nemotecnico.Trim() + "'").CopyToDataTable().Rows)
                        ldat_Tira.ImportRow(dr_tira);

                #region RecorreTiras
                foreach (DataRow ldr_Row in ldat_Tira.Rows)
                {
                    lbln_Asiento = true;
                    int index = ldat_Tira.Rows.IndexOf(ldr_Row);
                    ldec_monto = diferencia;

                    lstr_Referencia = lstr_Nemotecnico.Trim() + " Diferencial " + (esPositivo ? "Positivo" : "Negativo");

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

                        if (ldec_MontoTotal >= (ldec_monto))
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
                                        ldt_FchContabilizacion.Value.ToString("dd.MM.yyyy"),
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
                                        lstr_Nemotecnico,//pk
                                        tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                        "UDE",//tipo
                                        lstr_Operacion +"."+lstr_NomOperacion//operacion
                                        );
                                }

                                //Resta el saldo    
                                ldec_Saldo = ldec_Saldo - Convert.ToDecimal(drForm["Monto"]);
                            }
                        }
                        else
                        {
                            //Almacena en bitácora de que no lo hizo
                            bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion, "DI"), "Resultado de Contabilización: \n 1 - [E] Monto superior al total de las reservas de la Deuda Interna. Reservas utilizadas: \n" + reservasError, lstr_Operacion, lstr_Nemotecnico, "G206");
                            SinError = false;
                            break;
                        }
                    }
                    else
                    {
                        if (ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim().Equals(""))
                        {
                            ldat_Asiento.Rows.Add(
                                lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                ldt_FchContabilizacion.Value.ToString("dd.MM.yyyy"),
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
                                lstr_Nemotecnico,//pk
                                tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                "UDE",//tipo
                                lstr_Operacion+"."+lstr_NomOperacion //operacion
                                );
                        }
                        else
                        {
                            bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion, "DI"), "Resultado de Contabilización: \n 1 - [E] No hay reservas correspondientes a la cuenta " + ldat_Tira.Rows[0]["IdCuentaContable"].ToString().Trim() + " con fondo " + ldat_Tira.Rows[0]["IdPosPre"].ToString().Trim(), lstr_Operacion, lstr_Nemotecnico, "G206");
                            SinError = false;
                            break;
                        }
                    }

                    #endregion pospre debe

                    #region pospre haber

                    lstr_Monto = string.Empty;
                    lds_Datos = new DataTable();
                    ldec_MontoTotal = 0;
                    reservasError = "";
                    lstr_NuevoPosPrePago = string.Empty;
                    ldat_Reservas = new DataSet();

                    if (SinError)
                    {
                        ldat_Reservas = dinamica.ConsultarDinamico("select * from ma.reservasdetalle where idcuentacontable = '" + ldat_Tira.Rows[index]["IdCuentaContable2"].ToString().Trim() + "' and idpospre = '" + ldat_Tira.Rows[index]["IdPosPre2"].ToString().Trim() + "' and LEFT(idprograma, 4) = year(getdate()) order by idprograma desc");
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

                            if (Convert.ToDecimal(ldec_MontoTotal) >= (ldec_monto))
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
                                            ldt_FchContabilizacion.Value.ToString("dd.MM.yyyy"),
                                            ldat_Tira.Rows[index]["IdCuentaContable2"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdClaveContable2"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim(),
                                            lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim(),
                                            ldat_Tira.Rows[index]["IdCentroCosto2"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdCentroBeneficio2"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdElementoPEP2"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdPosPre2"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdCentroGestor2"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdFondo2"].ToString().Trim(),
                                            drForm["IdReserva"].ToString().Trim(),
                                            drForm["Posicion"].ToString().Trim(),
                                            Truncate(ldec_Saldo > Convert.ToDecimal(drForm["Monto"].ToString()) ? Convert.ToDecimal(drForm["Monto"].ToString()) : ldec_Saldo, 2),
                                            lstr_Nemotecnico,//pk
                                            tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                            "UDE",//tipo
                                            lstr_Operacion +"."+lstr_NomOperacion//operacion
                                            );
                                    }

                                    //Resta el saldo    
                                    ldec_Saldo = ldec_Saldo - Convert.ToDecimal(drForm["Monto"]);
                                }
                            }
                            else
                            {
                                //Almacena en bitácora de que no lo hizo
                                bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion, "DI"), "Resultado de Contabilización: \n 1 - [E] Monto superior al total de las reservas de la Deuda Interna. Reservas utilizadas: \n" + reservasError, lstr_Operacion, lstr_Nemotecnico, "G206");
                                SinError = false;
                                break;
                            }
                        }
                        else
                        {
                            if (!(ldat_Tira.Rows[index]["IdPosPre2"].ToString().Trim().StartsWith("E"))) //.Equals(""))
                            {
                                ldat_Asiento.Rows.Add(
                                    lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                    ldt_FchContabilizacion.Value.ToString("dd.MM.yyyy"),
                                    ldat_Tira.Rows[index]["IdCuentaContable2"].ToString().Trim(),
                                    ldat_Tira.Rows[index]["IdClaveContable2"].ToString().Trim(),
                                    ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim(),
                                    lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim(),
                                    ldat_Tira.Rows[index]["IdCentroCosto2"].ToString().Trim(),
                                    ldat_Tira.Rows[index]["IdCentroBeneficio2"].ToString().Trim(),
                                    ldat_Tira.Rows[index]["IdElementoPEP2"].ToString().Trim(),
                                    ldat_Tira.Rows[index]["IdPosPre2"].ToString().Trim(),
                                    ldat_Tira.Rows[index]["IdCentroGestor2"].ToString().Trim(),
                                    ldat_Tira.Rows[index]["IdFondo2"].ToString().Trim(),
                                    ldat_Tira.Rows[index]["DocPresupuestario2"].ToString().Trim(),
                                    ldat_Tira.Rows[index]["PosDocPresupuestario2"].ToString().Trim(),
                                    Truncate(ldec_monto, 2),
                                    lstr_Nemotecnico,//pk
                                    tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                    "UDE",//tipo
                                    lstr_Operacion +"."+lstr_NomOperacion//operacion
                                    );
                            }
                            else
                            {
                                bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion, "DI"), "Resultado de Contabilización: \n 1 - [E] No hay reservas correspondientes a la cuenta " + ldat_Tira.Rows[index]["IdCuentaContable2"].ToString().Trim() + " con fondo " + ldat_Tira.Rows[index]["IdPosPre2"].ToString().Trim(), lstr_Operacion, lstr_Nemotecnico, "G206");
                                lstr_Mensaje += "No hay reservas correspondientes a la cuenta " + ldat_Tira.Rows[index]["IdCuentaContable2"].ToString().Trim() + " con fondo " + ldat_Tira.Rows[index]["IdPosPre2"].ToString().Trim() + " " + lstr_Operacion + " " + lstr_Nemotecnico;
                                SinError = false;
                                break;
                            }
                        }
                    }
                    #endregion pospre haber
                }
                #endregion RecorreTiras
                #endregion Contabilización de diferencial
                if (SinError)
                {
                    if (ldat_Asiento.Rows.Count > 0)
                        lstr_Mensaje += GenerarAsientoAjuste(ldat_Asiento, lstr_Operacion, lstr_Nemotecnico, esPositivo);
                    else
                    {
                        lstr_Mensaje += " No se encontró asiento para nemotécnico " + lstr_Nemotecnico + " Operación " + lstr_Operacion + " Propietario " + lstr_Propietario + " Plazo " + lstr_Plazo + " Cuenta " + lstr_DescripcionCuenta;
                        bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion, "DI"), "Resultado de Contabilización: " + lstr_Mensaje, lstr_Operacion, lstr_Nemotecnico, "");

                    }
                }
            }
            return lstr_Mensaje;
        }

        public static string GenerarAsientoAjuste(DataTable ldat_Asiento, string lstr_IdOperacion, string lstr_Nemotecnico, bool esPositivo)
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
                    item_asiento.Kostl = ldat_Asiento.Rows[index]["CentroCosto"].ToString().ToUpper() ;//Centro de Costo
                    item_asiento.Prctr = ldat_Asiento.Rows[index]["CentroBeneficio"].ToString();//Centro de Beneficio
                    item_asiento.Projk = ldat_Asiento.Rows[index]["ElementoPEP"].ToString();//Elemento PEP
                    item_asiento.Fipex = ldat_Asiento.Rows[index]["PosPre"].ToString().ToUpper();//Posición Presupuestaria
                    item_asiento.Fistl = ldat_Asiento.Rows[index]["CentroGestor"].ToString().ToUpper();//Centro Gestor
                    item_asiento.Geber = ldat_Asiento.Rows[index]["Fondo"].ToString();//Fondo
                    item_asiento.Kblnr = ldat_Asiento.Rows[index]["DocPres"].ToString();//Documento Presupuestario
                    item_asiento.Kblpos = ldat_Asiento.Rows[index]["PosDocPres"].ToString();//Posición de documento presupuestario
                    //if (ldat_Asiento.Rows[index]["Moneda"].ToString() == "USD")
                    //    item_asiento.Kursf = Convert.ToDecimal(ldec_TipoCambio.ToString("0.0000"));

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
                bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_IdOperacion, "DI"), "Resultado de Contabilización: " + logAsiento, lstr_IdOperacion, lstr_Nemotecnico, "");

                // convertir el 
                //Marcar registro como contabilizado
                
                string[] a = new string[2];
                //if (!logAsiento.Contains("[E]"))
                //    lcls_CostoTransaccion.ContabilizarCalculosFinancieros(
                //        "TitulosValores",
                //        null,
                //        lstr_NroValor.Trim(),
                //        lstr_Nemotecnico.Trim(),
                //        "C",
                //        "SG",
                //        ldt_FchModifica, out a[0], out a[1]);
                return logAsiento;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}