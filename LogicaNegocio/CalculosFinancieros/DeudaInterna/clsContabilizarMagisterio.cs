using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using LogicaNegocio.Mantenimiento;
using LogicaNegocio.Seguridad;

namespace LogicaNegocio.CalculosFinancieros.DeudaInterna
{
    public class clsContabilizarMagisterio
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

        public string ContabilizacionMagisterio(int lint_NroValor, string lstr_Nemotecnico)
        {
            string lstr_Mensaje = string.Empty;
            try
            {
                DataTable ldat_TitulosValores = dinamica.ConsultarDinamico("select * from cf.titulosvalores where sistemanegociacion = 'Traslado Cuotas Magisterio Nacional' and nrovalor = " + lint_NroValor + " and nemotecnico = '" + lstr_Nemotecnico + "' and Estado='ACT' ").Tables[0];// titulo.ConsultarTituloValor("%", "%", "%", "%", "%", "01/01/1900", "01/01/5000").Tables[0].Select("IndicadorCupon ='V'").CopyToDataTable();
                DataTable ldat_Nemotecnicos = nemotecnico.ConsultarNemotecnicos(String.Empty, String.Empty, String.Empty, String.Empty, String.Empty).Tables[0];
                
                //for (int i = 0; i < ldat_TitulosValores.Rows.Count; i++)
                foreach(DataRow tituloMagisterio in ldat_TitulosValores.Rows)
                {
                    try
                    {
                        string lstr_moneda = ldat_Nemotecnicos.Select("IdNemotecnico = '" + tituloMagisterio["NemoTecnico"].ToString().Trim() + "'")[0]["IdMoneda"].ToString().Trim().Equals("CRCN")
                           ? "CRC" : ldat_Nemotecnicos.Select("IdNemotecnico = '" + tituloMagisterio["NemoTecnico"].ToString().Trim() + "'")[0]["IdMoneda"].ToString().Trim();

                        if (ldat_Nemotecnicos.Select("IdNemotecnico = '" + tituloMagisterio["NemoTecnico"].ToString().Trim() + "'")[0]["Estado"].ToString().Trim() == "A"
                            && lstr_moneda == (tituloMagisterio["Moneda"].ToString().Trim().Equals("CRCN") ? "CRC" : tituloMagisterio["Moneda"].ToString().Trim()))
                        {
                            if (tituloMagisterio["EstadoValor"].ToString().Trim() == "Vigente")
                                //&& tituloCCSS.Lstr_TipoNegociacion.Trim() != "Compra")
                            {
                                //DataTable ldat_TitulosValores = titulo.ConsultarTituloValor(tituloMagisterio.Lint_NumValor, tituloMagisterio.Lstr_Nemotecnico, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/5000")).Tables[0];

                                lstr_Mensaje = ContabilizarMagisterio(
                                     tituloMagisterio["Tipo"].ToString().Trim(),
                                     tituloMagisterio["EstadoValor"].ToString().Trim(),
                                     Convert.ToDateTime(tituloMagisterio["FchValor"].ToString().Trim()),
                                     Convert.ToDateTime(tituloMagisterio["FchVencimiento"].ToString().Trim()),
                                     tituloMagisterio["EntidadCustodia"].ToString().Trim(),
                                     tituloMagisterio["PlazoValor"].ToString().Trim(),

                                     Math.Round(Convert.ToDecimal(tituloMagisterio["ValorTransadoBruto"].ToString().Trim()),2),
                                     tituloMagisterio["Moneda"].ToString().Trim().Equals("CRCN") ? "CRC" : tituloMagisterio["Moneda"].ToString().Trim(),

                                     tituloMagisterio["NroValor"].ToString().Trim(),
                                     tituloMagisterio["NemoTecnico"].ToString().Trim(),
                                     Math.Round(Convert.ToDecimal(tituloMagisterio["ValorFacial"].ToString().Trim()),2),
                                     Math.Round(Convert.ToDecimal(tituloMagisterio["RendimientoPorDescuento"].ToString().Trim()),2),
                                     Math.Round(Convert.ToDecimal(tituloMagisterio["ImpuestoPagado"].ToString().Trim()),2),
                                     Math.Round(Convert.ToDecimal(tituloMagisterio["ValorTransadoNeto"].ToString().Trim()),2),
                                     Math.Round(Convert.ToDecimal(tituloMagisterio["Premio"].ToString().Trim()), 2),

                                     "SINPE: " + tituloMagisterio["ModuloSINPE"].ToString().Trim() + "-" + "T.B: " +
                                     tituloMagisterio["TasaBruta"].ToString().Trim() + "-" + "T.N: " +
                                     tituloMagisterio["TasaNeta"].ToString().Trim() + "-" + "Plazo: " +
                                     tituloMagisterio["PlazoValor"].ToString().Trim());
                            }
                        }
                    }
                    catch(Exception ex)
                    {

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
                ldat_Asiento.Columns.Add("Sociedad");
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

        public string ContabilizarMagisterio(
            string lstr_Tipo,
            string lstr_EstadoValor,
            DateTime ldt_FchValor,
            DateTime ldt_FchVencimiento,
            string lstr_Propietario,

            string lstr_Plazo,

            decimal ldec_ValorTransadoBruto,
            string lstr_Moneda,
            string lstr_NroValor,
            string lstr_Nemotecnico,
            decimal ldec_ValorFacial,
            decimal ldec_RendimientoXDescuento,
            decimal ldec_ImpuestoPagado,
            decimal ldec_ValorTransadoNeto,
            decimal ldec_Premio,
            string lstr_Detalle)
        {
            string lstr_codAsiento = String.Empty;
            string lstr_Mensaje = string.Empty;
            wsDeudaInterna.wsDeudaInterna wsDeudaInterna = new wsDeudaInterna.wsDeudaInterna();
            DataTable ldat_Asiento = RegistroContable();
            DataTable ldat_Tira = new DataTable();
            string lint_EsPublico = "PUBLICO";
            string lstr_Operacion = string.Empty;
            string lstr_NomOperacion = string.Empty;
            decimal ldec_TipoCambioColones = Convert.ToDecimal(tipocambio.ConsultarTiposCambio("CRCN", ldt_FchValor, "3140", "N").Tables[0].Rows[0]["Valor"].ToString());
            decimal ldec_TipoCambioUDE = Convert.ToDecimal(tipocambio.ConsultarTiposCambio("UDE", ldt_FchValor, "", "N").Tables[0].Rows[0]["Valor"].ToString());
            decimal ldec_monto = 0;
            string lstr_Referencia = "";
            string lstr_PrimaDescuento = "PRIMAS";
            decimal total40 = 0, total50 = 0;


            if (propietario.ConsultarPropietarios(string.Empty, string.Empty, string.Empty, lstr_Propietario, "S").Tables[0].Rows.Count == 0)
            {
                lint_EsPublico = "PRIVADO";
            }

            if (ldec_ValorFacial > ldec_ValorTransadoBruto)
            {
                lstr_PrimaDescuento = "IMP_DEV";
            }

            decimal ldec_TipoCambio = lstr_Moneda.Equals("USD") ? ldec_TipoCambioColones : (lstr_Moneda.Equals("CRC") ? 1 : ldec_TipoCambioUDE);

            bool SinError = true;

            //se tratan las cancelaciones de las tres diferentes operaciones, títulos cero cupón, de tasa fija y tasa variable.
            if (lstr_EstadoValor == "Vigente")
            {
                switch (lstr_Tipo.ToLower())
                {
                    #region cero cupon
                    case "cero cupón":
                        {
                            #region Define si el título es a corto plazo y no trasciende en el periodo
                            if ((Convert.ToDecimal(lstr_Plazo) <= 360) && (ldt_FchValor.Year == ldt_FchVencimiento.Year))
                            {
                                lstr_Operacion = "ID50";

                                DataSet lds_Operaciones = loperacion.ConsultarOperaciones(lstr_Operacion, "IdModulo IN ('DI')", "");
                                if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
                                {
                                    lstr_NomOperacion = lds_Operaciones.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
                                }

                                ldat_Tira = tipoasiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID").Tables[0].Clone();
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID", "", "GASTO").Tables[0].Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID", "CP", "AMORT").Tables[0].Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID", "", "FG").Tables[0].Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", "CAPITAL").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", lstr_PrimaDescuento).Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", "INT_DEV").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);

                                foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                {
                                    int index = ldat_Tira.Rows.IndexOf(ldr_Row);
                                    string operacion = ldr_Row["CodigoAuxiliar2"].ToString();
                                    //si la moneda viene en ude se debe colinizar
                                    decimal interes = 0, primas = 0, impuesto = 0, tc_ude = lstr_Moneda.Equals("UDE") ? ldec_TipoCambioUDE : 1;
                                    if (ldec_ValorTransadoNeto - ldec_ValorTransadoBruto > 0)
                                    {
                                        interes = ldec_ValorTransadoNeto*tc_ude - ldec_ValorTransadoBruto*tc_ude;
                                    }
                                    if (ldec_ValorFacial - ldec_ValorTransadoBruto > 0)
                                    {
                                        impuesto = ldec_ValorFacial*tc_ude - ldec_ValorTransadoBruto*tc_ude;
                                    }
                                    else
                                    {
                                        primas = Math.Abs(ldec_ValorFacial*tc_ude - ldec_ValorTransadoBruto*tc_ude);
                                    }

                                    switch (operacion.ToUpper().Trim())
                                    {
                                        case "GASTO": { ldec_monto = ldec_ValorTransadoNeto * tc_ude; break; }
                                        case "AMORTIZA": { ldec_monto = ldec_ValorFacial * tc_ude; break; }
                                        case "FG": { ldec_monto = 1; break; }
                                        case "CAPITAL": { ldec_monto = ldec_ValorFacial * tc_ude; break; }
                                        case "PRIMAS": { ldec_monto = primas * tc_ude; break; }
                                        case "IMP_DEV": { ldec_monto = impuesto * tc_ude; break; }
                                        case "INT_DEV": { ldec_monto = interes * tc_ude; break; }
                                    }
                                    ldec_monto = Math.Round(ldec_monto, 2);
                                    if (ldr_Row["IdClaveContable"].ToString().Trim().Contains("40"))
                                    {
                                        total40 += ldec_monto;
                                    }
                                    else
                                    {
                                        total50 += ldec_monto;
                                    }
                                    lstr_Referencia = lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Magisterio";

                                    #region pospre
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
                                                reservasError += "Posición Presupuestaria: " + drForm["IdPosPre"].ToString().Trim() + "Reserva :" + drForm["IdReserva"].ToString().Trim() + ", Posición: " + drForm["Posicion"].ToString().Trim() + "\n";
                                                ldec_MontoTotal += Convert.ToDecimal(lstr_Monto);
                                            }
                                        }

                                        if (Convert.ToDecimal(ldec_MontoTotal) >= ldec_monto)
                                        {
                                            //Genera el asiento
                                            decimal ldec_Saldo = ldec_monto;

                                            foreach (DataRow drForm in lds_Datos.Rows)
                                            {
                                                if (Convert.ToDecimal(drForm["Monto"]) > 0 && ldec_Saldo > 0)
                                                {
                                                    //lstr_Referencia = lstr_NroValor.Trim().Equals("00") ? "No Asociado" : lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Costo Transaccion";
                                                    lstr_Referencia = lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Magisterio";
                                                    ldat_Asiento.Rows.Add(
                                                        ldat_Tira.Rows[index]["Codigo"].ToString().Trim(),
                                                        lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                                        ldt_FchValor.ToString("dd.MM.yyyy"),
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
                                                        (ldec_Saldo > Convert.ToDecimal(drForm["Monto"].ToString()) ? Convert.ToDecimal(drForm["Monto"].ToString()) : ldec_Saldo),
                                                        lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                                        tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                                        lstr_Moneda,//tipo
                                                        lstr_Operacion+"."+ lstr_NomOperacion //operacion
                                                        );
                                                }

                                                //Resta el saldo    
                                                ldec_Saldo = ldec_Saldo - Convert.ToDecimal(drForm["Monto"]);
                                            }
                                        }
                                        else if (ldec_monto <= 0)
                                        {
                                            //Almacena en bitácora de que no lo hizo
                                            bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion, "DI"), "Resultado de Contabilización: \n 1 - [E] Monto superior al total de las reservas de la Deuda Interna. Reservas utilizadas: \n" + reservasError, lstr_Operacion, lstr_NroValor + "-" + lstr_Nemotecnico, "G206");
                                            SinError = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim().Equals("") && ldec_monto > 0)
                                        {
                                            lstr_Referencia = lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Magisterio";

                                            ldat_Asiento.Rows.Add(
                                                ldat_Tira.Rows[index]["Codigo"].ToString().Trim(),
                                                lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                                ldt_FchValor.ToString("dd.MM.yyyy"),
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
                                                (ldec_monto),
                                                lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                                tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                                lstr_Moneda,//tipo
                                                lstr_Operacion +"."+lstr_NomOperacion//operacion
                                                );
                                        }
                                        else if (ldec_monto > 0)
                                        {
                                            bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion, "DI"), "Resultado de Contabilización: \n 1 - [E] No hay reservas correspondientes a la cuenta " + ldat_Tira.Rows[0]["IdCuentaContable"].ToString().Trim() + " con fondo " + ldat_Tira.Rows[0]["IdPosPre"].ToString().Trim(), lstr_Operacion, lstr_NroValor + "-" + lstr_Nemotecnico, "G206");
                                            SinError = false;
                                            break;
                                        }

                                    }

                                    #endregion
                                    
                                }
                            }
                            #endregion

                            #region Define si el título es a corto plazo, pero trasciende en el periodo
                            else if ((Convert.ToDecimal(lstr_Plazo) <= 360) && (ldt_FchValor.Year != ldt_FchVencimiento.Year))
                            {
                                lstr_Operacion = "ID49";

                                DataSet lds_Operaciones = loperacion.ConsultarOperaciones(lstr_Operacion, "IdModulo IN ('DI')", "");
                                if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
                                {
                                    lstr_NomOperacion = lds_Operaciones.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
                                }

                                ldat_Tira = tipoasiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID").Tables[0].Clone();
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID", "", "GASTO").Tables[0].Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID", "CP", "AMORT").Tables[0].Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID", "", "FG").Tables[0].Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", "CAPITAL").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", lstr_PrimaDescuento).Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", "INT_DEV").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);

                                foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                {
                                    int index = ldat_Tira.Rows.IndexOf(ldr_Row);
                                    string operacion = ldr_Row["CodigoAuxiliar2"].ToString();
                                    decimal interes = 0, primas = 0, impuesto = 0, tc_ude = lstr_Moneda.Equals("UDE") ? ldec_TipoCambioUDE : 1;
                                    if (ldec_ValorTransadoNeto - ldec_ValorTransadoBruto > 0)
                                    {
                                        interes = ldec_ValorTransadoNeto*tc_ude - ldec_ValorTransadoBruto*tc_ude;
                                    }
                                    if (ldec_ValorFacial - ldec_ValorTransadoBruto > 0)
                                    {
                                        impuesto = ldec_ValorFacial*tc_ude - ldec_ValorTransadoBruto*tc_ude;
                                    }
                                    else
                                    {
                                        primas = Math.Abs(ldec_ValorFacial*tc_ude - ldec_ValorTransadoBruto*tc_ude);
                                    }

                                    switch (operacion.ToUpper().Trim())
                                    {
                                        case "GASTO": { ldec_monto = ldec_ValorTransadoNeto * tc_ude; break; }
                                        case "AMORT": { ldec_monto = ldec_ValorFacial * tc_ude; break; }
                                        case "FG": { ldec_monto = 1; break; }
                                        case "CAPITAL": { ldec_monto = ldec_ValorFacial * tc_ude; break; }
                                        case "PRIMAS": { ldec_monto = primas; break; }
                                        case "IMP_DEV": { ldec_monto = impuesto; break; }
                                        case "INT_DEV": { ldec_monto = interes; break; }
                                    }
                                    ldec_monto = Math.Round(ldec_monto, 2);
                                    if (ldr_Row["IdClaveContable"].ToString().Trim().Contains("40"))
                                    {
                                        total40 += ldec_monto;
                                    }
                                    else
                                    {
                                        total50 += ldec_monto;
                                    }

                                    #region pospre
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
                                                reservasError += "Posición Presupuestaria: " + drForm["IdPosPre"].ToString().Trim() + "Reserva :" + drForm["IdReserva"].ToString().Trim() + ", Posición: " + drForm["Posicion"].ToString().Trim() + "\n";
                                                ldec_MontoTotal += Convert.ToDecimal(lstr_Monto);
                                            }
                                        }

                                        if (Convert.ToDecimal(ldec_MontoTotal) >= ldec_monto)
                                        {
                                            //Genera el asiento
                                            decimal ldec_Saldo = ldec_monto;

                                            foreach (DataRow drForm in lds_Datos.Rows)
                                            {
                                                if (Convert.ToDecimal(drForm["Monto"]) > 0 && ldec_Saldo > 0)
                                                {
                                                    //lstr_Referencia = lstr_NroValor.Trim().Equals("00") ? "No Asociado" : lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Costo Transaccion";
                                                    lstr_Referencia = lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Magisterio";

                                                    ldat_Asiento.Rows.Add(
                                                        ldat_Tira.Rows[index]["Codigo"].ToString().Trim(),
                                                        lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                                        ldt_FchValor.ToString("dd.MM.yyyy"),
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
                                                        (ldec_Saldo > Convert.ToDecimal(drForm["Monto"].ToString()) ? Convert.ToDecimal(drForm["Monto"].ToString()) : ldec_Saldo),
                                                        lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                                        tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                                        lstr_Moneda,//tipo
                                                        lstr_Operacion +"."+lstr_NomOperacion//operacion
                                                        );
                                                }

                                                //Resta el saldo    
                                                ldec_Saldo = ldec_Saldo - Convert.ToDecimal(drForm["Monto"]);
                                            }
                                        }
                                        else if (ldec_monto > 0)
                                        {
                                            //Almacena en bitácora de que no lo hizo
                                            bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion, "DI"), "Resultado de Contabilización: \n 1 - [E] Monto superior al total de las reservas de la Deuda Interna. Reservas utilizadas: \n" + reservasError, lstr_Operacion, lstr_NroValor + "-" + lstr_Nemotecnico, "G206");
                                            SinError = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim().Equals("") && ldec_monto > 0)
                                        {
                                            lstr_Referencia = lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Magisterio";

                                            ldat_Asiento.Rows.Add(
                                                ldat_Tira.Rows[index]["Codigo"].ToString().Trim(),
                                                lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                                ldt_FchValor.ToString("dd.MM.yyyy"),
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
                                                (ldec_monto),
                                                lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                                tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                                lstr_Moneda,//tipo
                                                lstr_Operacion+"."+lstr_NomOperacion //operacion
                                                );
                                        }
                                        else if (ldec_monto > 0)
                                        {
                                            bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion, "DI"), "Resultado de Contabilización: \n 1 - [E] No hay reservas correspondientes a la cuenta " + ldat_Tira.Rows[0]["IdCuentaContable"].ToString().Trim() + " con fondo " + ldat_Tira.Rows[0]["IdPosPre"].ToString().Trim(), lstr_Operacion, lstr_NroValor + "-" + lstr_Nemotecnico, "G206");
                                            SinError = false;
                                            break;
                                        }

                                    }

                                    #endregion
                                }
                            }
                            #endregion

                            #region Define si el título es a largo plazo
                            else if ((Convert.ToDecimal(lstr_Plazo) > 365))// && (lstr_Nemotecnico != "PT"))
                            {
                                lstr_Operacion = "ID49";

                                DataSet lds_Operaciones = loperacion.ConsultarOperaciones(lstr_Operacion, "IdModulo IN ('DI')", "");
                                if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
                                {
                                    lstr_NomOperacion = lds_Operaciones.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
                                }

                                ldat_Tira = tipoasiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID").Tables[0].Clone();
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID", "", "GASTO").Tables[0].Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID", "LP", "AMORT").Tables[0].Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID", "", "FG").Tables[0].Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "LP", "CAPITAL").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "LP", lstr_PrimaDescuento).Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "LP", "INT_DEV").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);

                                foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                {
                                    int index = ldat_Tira.Rows.IndexOf(ldr_Row);
                                    string operacion = ldr_Row["CodigoAuxiliar2"].ToString();
                                    decimal interes = 0, primas = 0, impuesto = 0, tc_ude = lstr_Moneda.Equals("UDE") ? ldec_TipoCambioUDE : 1;
                                    if (ldec_ValorTransadoNeto - ldec_ValorTransadoBruto > 0)
                                    {
                                        interes = ldec_ValorTransadoNeto*tc_ude - ldec_ValorTransadoBruto*tc_ude;
                                    }
                                    if (ldec_ValorFacial - ldec_ValorTransadoBruto > 0)
                                    {
                                        impuesto = ldec_ValorFacial*tc_ude - ldec_ValorTransadoBruto*tc_ude;
                                    }
                                    else
                                    {
                                        primas = Math.Abs(ldec_ValorFacial*tc_ude - ldec_ValorTransadoBruto*tc_ude);
                                    }

                                    switch (operacion.ToUpper().Trim())
                                    {
                                        case "GASTO": { ldec_monto = ldec_ValorTransadoNeto * tc_ude; break; }
                                        case "AMORT": { ldec_monto = ldec_ValorFacial * tc_ude; break; }
                                        case "FG": { ldec_monto = 1; break; }
                                        case "CAPITAL": { ldec_monto = ldec_ValorFacial * tc_ude; break; }
                                        case "PRIMAS": { ldec_monto = primas; break; }
                                        case "IMP_DEV": { ldec_monto = impuesto; break; }
                                        case "INT_DEV": { ldec_monto = interes; break; }
                                    }
                                    ldec_monto = Math.Round(ldec_monto, 2);
                                    if (ldr_Row["IdClaveContable"].ToString().Trim().Contains("40"))
                                    {
                                        total40 += ldec_monto;
                                    }
                                    else
                                    {
                                        total50 += ldec_monto;
                                    }

                                    #region pospre
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
                                                reservasError += "Posición Presupuestaria: " + drForm["IdPosPre"].ToString().Trim() + "Reserva :" + drForm["IdReserva"].ToString().Trim() + ", Posición: " + drForm["Posicion"].ToString().Trim() + "\n";
                                                ldec_MontoTotal += Convert.ToDecimal(lstr_Monto);
                                            }
                                        }

                                        if (Convert.ToDecimal(ldec_MontoTotal) >= ldec_monto)
                                        {
                                            //Genera el asiento
                                            decimal ldec_Saldo = ldec_monto;

                                            foreach (DataRow drForm in lds_Datos.Rows)
                                            {
                                                if (Convert.ToDecimal(drForm["Monto"]) > 0 && ldec_Saldo > 0)
                                                {
                                                    lstr_Referencia = lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Magisterio";
                                                    //lstr_Referencia = lstr_NroValor.Trim().Equals("00") ? "No Asociado" : lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Costo Transaccion";

                                                    ldat_Asiento.Rows.Add(
                                                        ldat_Tira.Rows[index]["Codigo"].ToString().Trim(),
                                                        lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                                        ldt_FchValor.ToString("dd.MM.yyyy"),
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
                                                        (ldec_Saldo > Convert.ToDecimal(drForm["Monto"].ToString()) ? Convert.ToDecimal(drForm["Monto"].ToString()) : ldec_Saldo),
                                                        lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                                        tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                                        lstr_Moneda,//tipo
                                                        lstr_Operacion +"."+lstr_NomOperacion //operacion
                                                        );
                                                }

                                                //Resta el saldo    
                                                ldec_Saldo = ldec_Saldo - Convert.ToDecimal(drForm["Monto"]);
                                            }
                                        }
                                        else if (ldec_monto > 0)
                                        {
                                            //Almacena en bitácora de que no lo hizo
                                            bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion, "DI"), "Resultado de Contabilización: \n 1 - [E] Monto superior al total de las reservas de la Deuda Interna. Reservas utilizadas: \n" + reservasError, lstr_Operacion, lstr_NroValor + "-" + lstr_Nemotecnico, "G206");
                                            SinError = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim().Equals("") && ldec_monto > 0)
                                        {
                                            ldat_Asiento.Rows.Add(
                                                ldat_Tira.Rows[index]["Codigo"].ToString().Trim(),
                                                lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                                ldt_FchValor.ToString("dd.MM.yyyy"),
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
                                                (ldec_monto),
                                                lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                                tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                                lstr_Moneda,//tipo
                                                lstr_Operacion+"."+lstr_NomOperacion //operacion
                                                );
                                        }
                                        else if (ldec_monto > 0)
                                        {
                                            bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion, "DI"), "Resultado de Contabilización: \n 1 - [E] No hay reservas correspondientes a la cuenta " + ldat_Tira.Rows[0]["IdCuentaContable"].ToString().Trim() + " con fondo " + ldat_Tira.Rows[0]["IdPosPre"].ToString().Trim(), lstr_Operacion, lstr_NroValor + "-" + lstr_Nemotecnico, "G206");
                                            SinError = false;
                                            break;
                                        }

                                    }

                                    #endregion
                                }
                            }
                            #endregion

                            break;
                        }
                    #endregion

                    #region tasa fija y tasa variable
                    case "tasa fija":
                    case "tasa variable":
                        {
                            #region Define si el título es a corto plazo y no trasciende en el periodo
                            if ((Convert.ToDecimal(lstr_Plazo) <= 1) &&
                                (ldt_FchValor.Year == ldt_FchVencimiento.Year))
                            {
                                lstr_Operacion = "ID50";

                                DataSet lds_Operaciones = loperacion.ConsultarOperaciones(lstr_Operacion, "IdModulo IN ('DI')", "");
                                if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
                                {
                                    lstr_NomOperacion = lds_Operaciones.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
                                }

                                ldat_Tira = tipoasiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID").Tables[0].Clone();
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID", "", "GASTO").Tables[0].Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID", "CP", "AMORT").Tables[0].Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID", "", "FG").Tables[0].Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", "CAPITAL").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", lstr_PrimaDescuento).Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", "INT_DEV").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);

                                foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                {
                                    int index = ldat_Tira.Rows.IndexOf(ldr_Row);
                                    string operacion = ldr_Row["CodigoAuxiliar2"].ToString();
                                    // si la moneda  viene en ude se debe colinizar
                                    decimal interes = 0, primas = 0, impuesto = 0, tc_ude = lstr_Moneda.Equals("UDE") ? ldec_TipoCambioUDE : 1;
                                    if (ldec_ValorTransadoNeto - ldec_ValorTransadoBruto > 0)
                                    {
                                        interes = ldec_ValorTransadoNeto*tc_ude - ldec_ValorTransadoBruto*tc_ude;
                                    }
                                    if (ldec_ValorFacial - ldec_ValorTransadoBruto > 0)
                                    {
                                        impuesto = ldec_ValorFacial*tc_ude - ldec_ValorTransadoBruto*tc_ude;
                                    }
                                    else
                                    {
                                        primas = Math.Abs(ldec_ValorFacial*tc_ude - ldec_ValorTransadoBruto*tc_ude);
                                    }

                                    switch (operacion.ToUpper().Trim())
                                    {
                                        case "GASTO": { ldec_monto = ldec_ValorTransadoNeto * tc_ude; break; }
                                        case "AMORTIZA": { ldec_monto = ldec_ValorFacial * tc_ude; break; }
                                        case "FG": { ldec_monto = 1; break; }
                                        case "CAPITAL": { ldec_monto = ldec_ValorFacial * tc_ude; break; }
                                        case "PRIMAS": { ldec_monto = primas * tc_ude; break; }
                                        case "IMP_DEV": { ldec_monto = impuesto * tc_ude; break; }
                                        case "INT_DEV": { ldec_monto = interes * tc_ude; break; }
                                    }
                                    ldec_monto = Math.Round(ldec_monto, 2);
                                    if (ldr_Row["IdClaveContable"].ToString().Trim().Contains("40"))
                                    {
                                        total40 += ldec_monto;
                                    }
                                    else
                                    {
                                        total50 += ldec_monto;
                                    }
                                    lstr_Referencia = lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Magisterio";

                                    #region pospre
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
                                                reservasError += "Posición Presupuestaria: " + drForm["IdPosPre"].ToString().Trim() + "Reserva :" + drForm["IdReserva"].ToString().Trim() + ", Posición: " + drForm["Posicion"].ToString().Trim() + "\n";
                                                ldec_MontoTotal += Convert.ToDecimal(lstr_Monto);
                                            }
                                        }

                                        if (Convert.ToDecimal(ldec_MontoTotal) >= ldec_monto)
                                        {
                                            //Genera el asiento
                                            decimal ldec_Saldo = ldec_monto;

                                            foreach (DataRow drForm in lds_Datos.Rows)
                                            {
                                                if (Convert.ToDecimal(drForm["Monto"]) > 0 && ldec_Saldo > 0)
                                                {
                                                    //lstr_Referencia = lstr_NroValor.Trim().Equals("00") ? "No Asociado" : lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Costo Transaccion";
                                                    lstr_Referencia = lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Magisterio";

                                                    ldat_Asiento.Rows.Add(
                                                        ldat_Tira.Rows[index]["Codigo"].ToString().Trim(),
                                                        lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                                        ldt_FchValor.ToString("dd.MM.yyyy"),
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
                                                        (ldec_Saldo > Convert.ToDecimal(drForm["Monto"].ToString()) ? Convert.ToDecimal(drForm["Monto"].ToString()) : ldec_Saldo),
                                                        lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                                        tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                                        lstr_Moneda,//tipo
                                                        lstr_Operacion +"."+lstr_NomOperacion//operacion
                                                        );
                                                }

                                                //Resta el saldo    
                                                ldec_Saldo = ldec_Saldo - Convert.ToDecimal(drForm["Monto"]);
                                            }
                                        }
                                        else if (ldec_monto > 0)
                                        {
                                            //Almacena en bitácora de que no lo hizo
                                            bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion, "DI"), "Resultado de Contabilización: \n 1 - [E] Monto superior al total de las reservas de la Deuda Interna. Reservas utilizadas: \n" + reservasError, lstr_Operacion, lstr_NroValor + "-" + lstr_Nemotecnico, "G206");
                                            SinError = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim().Equals("") && ldec_monto > 0)
                                        {
                                            lstr_Referencia = lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Magisterio";

                                            ldat_Asiento.Rows.Add(
                                                ldat_Tira.Rows[index]["Codigo"].ToString().Trim(),
                                                lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                                ldt_FchValor.ToString("dd.MM.yyyy"),
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
                                                (ldec_monto),
                                                lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                                tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                                lstr_Moneda,//tipo
                                                lstr_Operacion+"."+lstr_NomOperacion //operacion
                                                );
                                        }
                                        else if (ldec_monto > 0)
                                        {
                                            bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion, "DI"), "Resultado de Contabilización: \n 1 - [E] No hay reservas correspondientes a la cuenta " + ldat_Tira.Rows[0]["IdCuentaContable"].ToString().Trim() + " con fondo " + ldat_Tira.Rows[0]["IdPosPre"].ToString().Trim(), lstr_Operacion, lstr_NroValor + "-" + lstr_Nemotecnico, "G206");
                                            SinError = false;
                                            break;
                                        }

                                    }

                                    #endregion
                                }
                            }
                            #endregion

                            #region Define si el título es a corto plazo, pero trasciende en el periodo
                            else if ((Convert.ToDecimal(lstr_Plazo) <= 1) &&
                            (ldt_FchValor.Year != ldt_FchVencimiento.Year))
                            {
                                lstr_Operacion = "ID50";

                                DataSet lds_Operaciones = loperacion.ConsultarOperaciones(lstr_Operacion, "IdModulo IN ('DI')", "");
                                if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
                                {
                                    lstr_NomOperacion = lds_Operaciones.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
                                }

                                ldat_Tira = tipoasiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID").Tables[0].Clone();
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID", "", "GASTO").Tables[0].Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID", "CP", "AMORT").Tables[0].Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID", "", "FG").Tables[0].Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", "CAPITAL").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", lstr_PrimaDescuento).Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", "INT_DEV").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);

                                foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                {
                                    int index = ldat_Tira.Rows.IndexOf(ldr_Row);
                                    string operacion = ldr_Row["CodigoAuxiliar2"].ToString();
                                    //si la moneda viene en ude se debe colinizar
                                    decimal interes = 0, primas = 0, impuesto = 0, tc_ude =  lstr_Moneda.Equals("UDE") ? ldec_TipoCambioUDE : 1; 
                                    if (ldec_ValorTransadoNeto - ldec_ValorTransadoBruto > 0)
                                    {
                                        interes = ldec_ValorTransadoNeto - ldec_ValorTransadoBruto;
                                    }
                                    if (ldec_ValorFacial - ldec_ValorTransadoBruto > 0)
                                    {
                                        impuesto = ldec_ValorFacial - ldec_ValorTransadoBruto;
                                    }
                                    else
                                    {
                                        primas = Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto);
                                    }

                                    switch (operacion.ToUpper().Trim())
                                    {
                                        case "GASTO": { ldec_monto = ldec_ValorTransadoNeto * tc_ude; break; }
                                        case "AMORTIZA": { ldec_monto = ldec_ValorFacial * tc_ude; break; }
                                        case "FG": { ldec_monto = 1; break; }
                                        case "CAPITAL": { ldec_monto = ldec_ValorFacial * tc_ude; break; }
                                        case "PRIMAS": { ldec_monto = primas * tc_ude; break; }
                                        case "IMP_DEV": { ldec_monto = impuesto * tc_ude; break; }
                                        case "INT_DEV": { ldec_monto = interes * tc_ude; break; }
                                    }
                                    ldec_monto = Math.Round(ldec_monto, 2);
                                    if (ldr_Row["IdClaveContable"].ToString().Trim().Contains("40"))
                                    {
                                        total40 += ldec_monto;
                                    }
                                    else
                                    {
                                        total50 += ldec_monto;
                                    }
                                    lstr_Referencia = lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Magisterio";

                                    #region pospre
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
                                                reservasError += "Posición Presupuestaria: " + drForm["IdPosPre"].ToString().Trim() + "Reserva :" + drForm["IdReserva"].ToString().Trim() + ", Posición: " + drForm["Posicion"].ToString().Trim() + "\n";
                                                ldec_MontoTotal += Convert.ToDecimal(lstr_Monto);
                                            }
                                        }

                                        if (Convert.ToDecimal(ldec_MontoTotal) >= ldec_monto )
                                        {
                                            //Genera el asiento
                                            decimal ldec_Saldo = ldec_monto;

                                            foreach (DataRow drForm in lds_Datos.Rows)
                                            {
                                                if (Convert.ToDecimal(drForm["Monto"]) > 0 && ldec_Saldo > 0)
                                                {
                                                    //lstr_Referencia = lstr_NroValor.Trim().Equals("00") ? "No Asociado" : lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Costo Transaccion";
                                                    lstr_Referencia = lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Magisterio";

                                                    ldat_Asiento.Rows.Add(
                                                        ldat_Tira.Rows[index]["Codigo"].ToString().Trim(),
                                                        lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                                        ldt_FchValor.ToString("dd.MM.yyyy"),
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
                                                        (ldec_Saldo > Convert.ToDecimal(drForm["Monto"].ToString()) ? Convert.ToDecimal(drForm["Monto"].ToString()) : ldec_Saldo),
                                                        lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                                        tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                                        lstr_Moneda,//tipo
                                                        lstr_Operacion+"."+lstr_NomOperacion //operacion
                                                        );
                                                }

                                                //Resta el saldo    
                                                ldec_Saldo = ldec_Saldo - Convert.ToDecimal(drForm["Monto"]);
                                            }
                                        }
                                        else if (ldec_monto > 0)
                                        {
                                            //Almacena en bitácora de que no lo hizo
                                            bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion, "DI"), "Resultado de Contabilización: \n 1 - [E] Monto superior al total de las reservas de la Deuda Interna. Reservas utilizadas: \n" + reservasError, lstr_Operacion, lstr_NroValor + "-" + lstr_Nemotecnico, "G206");
                                            SinError = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim().Equals("") && ldec_monto > 0)
                                        {
                                            lstr_Referencia = lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Magisterio";

                                            ldat_Asiento.Rows.Add(
                                                ldat_Tira.Rows[index]["Codigo"].ToString().Trim(),
                                                lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                                ldt_FchValor.ToString("dd.MM.yyyy"),
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
                                                (ldec_monto),
                                                lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                                tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                                lstr_Moneda,//tipo
                                                lstr_Operacion+"."+lstr_NomOperacion //operacion
                                                );
                                        }
                                        else if (ldec_monto > 0)
                                        {
                                            bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion, "DI"), "Resultado de Contabilización: \n 1 - [E] No hay reservas correspondientes a la cuenta " + ldat_Tira.Rows[0]["IdCuentaContable"].ToString().Trim() + " con fondo " + ldat_Tira.Rows[0]["IdPosPre"].ToString().Trim(), lstr_Operacion, lstr_NroValor + "-" + lstr_Nemotecnico, "G206");
                                            SinError = false;
                                            break;
                                        }

                                    }

                                    #endregion
                                }
                            }
                            #endregion

                            #region Define si el título es a largo plazo con afectación presupuestaria
                            else if (Convert.ToDecimal(lstr_Plazo) > 1)
                            {
                                lstr_Operacion = "ID49";

                                DataSet lds_Operaciones = loperacion.ConsultarOperaciones(lstr_Operacion, "IdModulo IN ('DI')", "");
                                if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
                                {
                                    lstr_NomOperacion = lds_Operaciones.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
                                }

                                ldat_Tira = tipoasiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID").Tables[0].Clone();
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID", "", "GASTO").Tables[0].Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID", "LP", "AMORT").Tables[0].Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID", "", "FG").Tables[0].Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "LP", "CAPITAL").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "LP", lstr_PrimaDescuento).Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "LP", "INT_DEV").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);

                                foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                {
                                    int index = ldat_Tira.Rows.IndexOf(ldr_Row);
                                    string operacion = ldr_Row["CodigoAuxiliar2"].ToString();
                                    // si la moneda viene en ude se debe colinizar
                                    decimal interes = 0, primas = 0, impuesto = 0, tc_ude = lstr_Moneda.Equals("UDE") ? ldec_TipoCambioUDE : 1;
                                    if (ldec_ValorTransadoNeto - ldec_ValorTransadoBruto > 0)
                                    {
                                        interes = ldec_ValorTransadoNeto - ldec_ValorTransadoBruto;
                                    }
                                    if (ldec_ValorFacial - ldec_ValorTransadoBruto > 0)
                                    {
                                        impuesto = ldec_ValorFacial - ldec_ValorTransadoBruto;
                                    }
                                    else
                                    {
                                        primas = Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto);
                                    }

                                    switch (operacion.ToUpper().Trim())
                                    {
                                        case "GASTO": { ldec_monto = ldec_ValorTransadoNeto * tc_ude; break; }
                                        case "AMORTIZA": { ldec_monto = ldec_ValorFacial * tc_ude; break; }
                                        case "FG": { ldec_monto = 1; break; }
                                        case "CAPITAL": { ldec_monto = ldec_ValorFacial * tc_ude; break; }
                                        case "PRIMAS": { ldec_monto = primas * tc_ude; break; }
                                        case "IMP_DEV": { ldec_monto = impuesto * tc_ude; break; }
                                        case "INT_DEV": { ldec_monto = interes * tc_ude; break; }
                                    }

                                    ldec_monto = Math.Round(ldec_monto, 2);
                                    if (ldr_Row["IdClaveContable"].ToString().Trim().Contains("40"))
                                    {
                                        total40 += ldec_monto;
                                    }
                                    else
                                    {
                                        total50 += ldec_monto;
                                    }
                                    lstr_Referencia = lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Magisterio";

                                    #region pospre
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
                                                reservasError += "Posición Presupuestaria: " + drForm["IdPosPre"].ToString().Trim() + "Reserva :" + drForm["IdReserva"].ToString().Trim() + ", Posición: " + drForm["Posicion"].ToString().Trim() + "\n";
                                                ldec_MontoTotal += Convert.ToDecimal(lstr_Monto);
                                            }
                                        }

                                        if (Convert.ToDecimal(ldec_MontoTotal) >= (ldec_monto))
                                        //if (Convert.ToDecimal(ldec_MontoTotal) >= (ldec_monto * ldec_TipoCambio))
                                        {
                                            //Genera el asiento
                                            decimal ldec_SaldoCont = ldec_monto;
                                            decimal ldec_Saldo = (ldec_monto * ldec_TipoCambio);

                                            foreach (DataRow drForm in lds_Datos.Rows)
                                            {
                                                if (Convert.ToDecimal(drForm["Monto"]) > 0 && ldec_Saldo >= 0)
                                                {
                                                    decimal reservaTpoCambio = 0;

                                                    if (lstr_Moneda.Equals("USD"))
                                                        reservaTpoCambio = Convert.ToDecimal(drForm["Monto"].ToString()) / ldec_TipoCambio;
                                                    else
                                                        reservaTpoCambio = Convert.ToDecimal(drForm["Monto"].ToString()) ;
                                                    lstr_Referencia = lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Magisterio";

                                                    ldat_Asiento.Rows.Add(
                                                        ldat_Tira.Rows[index]["Codigo"].ToString().Trim(),
                                                        lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                                        ldt_FchValor.ToString("dd.MM.yyyy"),
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
                                                        (ldec_SaldoCont > reservaTpoCambio ? reservaTpoCambio : ldec_SaldoCont),
                                                        lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                                        tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                                        lstr_Moneda,//tipo
                                                        lstr_Operacion+"."+lstr_NomOperacion //operacion
                                                        );
                                                }

                                                //Resta el saldo
                                                ldec_SaldoCont = ldec_SaldoCont - (Convert.ToDecimal(drForm["Monto"].ToString()) / ldec_TipoCambio);
                                                ldec_Saldo = ldec_Saldo - Convert.ToDecimal(drForm["Monto"]);
                                            }
                                        }
                                        else if (ldec_monto > 0)
                                        {
                                            //Almacena en bitácora de que no lo hizo
                                            bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion, "DI"), "Resultado de Contabilización: \n 1 - [E] Monto superior al total de las reservas de la Deuda Interna. Reservas utilizadas: \n" + reservasError, lstr_Operacion, lstr_NroValor + "-" + lstr_Nemotecnico, "G206");
                                            SinError = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (!ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim().StartsWith("E") && ldec_monto > 0)
                                        {//if (ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim().Equals("")
                                            lstr_Referencia = lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Magisterio";
                                            ldat_Asiento.Rows.Add(
                                                ldat_Tira.Rows[index]["Codigo"].ToString().Trim(),
                                                lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                                ldt_FchValor.ToString("dd.MM.yyyy"),
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
                                                (ldec_monto),
                                                lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                                tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                                lstr_Moneda,//tipo
                                                lstr_Operacion+"."+lstr_NomOperacion //operacion
                                                );
                                        }
                                        else if (ldec_monto > 0)
                                        {
                                            bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion, "DI"), "Resultado de Contabilización: \n 1 - [E] No hay reservas correspondientes a la cuenta " + ldat_Tira.Rows[0]["IdCuentaContable"].ToString().Trim() + " con fondo " + ldat_Tira.Rows[0]["IdPosPre"].ToString().Trim(), lstr_Operacion, lstr_NroValor + "-" + lstr_Nemotecnico, "G206");
                                            SinError = false;
                                            break;
                                        }
                                         
                                    }

                                    #endregion
                                }
                            }
                            #endregion
                            break;
                        }
                    #endregion
                }
                if (SinError)
                {
                    decimal diff_total = total40 - total50;
                    for (int row = ldat_Asiento.Rows.Count - 1; row >= 0; row--)
                    {
                        if (ldat_Asiento.Rows[row]["ClaveContable"].ToString().Trim().Contains("50"))
                        {
                            ldat_Asiento.Rows[row]["Monto"] = Convert.ToDecimal(ldat_Asiento.Rows[row]["Monto"]) + diff_total;
                            break;
                        }
                    }                    
                    lstr_Mensaje = GenerarAsientoAjuste(ldat_Asiento, lstr_Operacion, lstr_NroValor, lstr_Nemotecnico, ldec_TipoCambioColones);
                }
            }
            return lstr_Mensaje;
        }

        public static string GenerarAsientoAjuste(DataTable ldat_Asiento, string lstr_IdOperacion, string lstr_NroValor, string lstr_Nemotecnico, decimal ldec_TipoCambio)
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
                        item_asiento.Bldat = ldat_Asiento.Rows[index]["Fecha"].ToString();//Fecha de documento
                        item_asiento.Budat = ldat_Asiento.Rows[index]["Fecha"].ToString();//Fecha de contabilización
                        item_asiento.Bktxt = ldat_Asiento.Rows[index]["Referencia"].ToString();//Referencia
                        fechaContabilizacion = Convert.ToDateTime(ldat_Asiento.Rows[index]["Fecha"].ToString());//Fecha de contabilización

                    }
                    
                    item_asiento.Bukrs = ldat_Asiento.Rows[index]["Sociedad"].ToString();//Sociedad
                    item_asiento.Waers = ldat_Asiento.Rows[index]["Moneda"].ToString();//Moneda 
                    item_asiento.Bschl = ldat_Asiento.Rows[index]["ClaveContable"].ToString();//Clave de contabilización
                    item_asiento.Hkont = ldat_Asiento.Rows[index]["Cuenta"].ToString();//Cuenta de mayor
                    item_asiento.Wrbtr = Convert.ToDecimal(Convert.ToDecimal(ldat_Asiento.Rows[index]["Monto"].ToString()).ToString("0.00"));//Importe
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
                        item_asiento.Kursf = Convert.ToDecimal(ldec_TipoCambio.ToString("0.00"));

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
                bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_IdOperacion, "DI"), "Resultado de Contabilización: " + logAsiento, lstr_IdOperacion, lstr_NroValor + "-" + lstr_Nemotecnico, "");

                // convertir el 
                //Marcar registro como contabilizado

                string[] a = new string[2];
                if (!logAsiento.Contains("[E]"))
                {
                    //RAMSES
                    //lstr_codAsiento = logAsiento.Substring(logAsiento.IndexOf("BKPFF") + 6, 18);//logAsiento.Length - logAsiento.IndexOf("BKPFF") - 1);
                    //string str_CodRes = string.Empty;
                    //string str_Msg = string.Empty;

                    //RAMSES UDPDATE
                    String fecha_con_formato_sql = String.Format("{0}/{1}/{2}", fechaContabilizacion.Year, fechaContabilizacion.Month, fechaContabilizacion.Day) ;
                    String QUERY = String.Format("UPDATE cf.TitulosValores SET asiento = '{0}', FchContablilizado = '{1}', Estado='CAN' WHERE nrovalor = {2} AND nemotecnico = '{3}'", lstr_codAsiento, fecha_con_formato_sql, lstr_NroValor.Trim(), lstr_Nemotecnico.Trim());
                    dinamica.ConsultarDinamico(QUERY);
                }
                //ESTO YA ESTABA COMENTADO
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