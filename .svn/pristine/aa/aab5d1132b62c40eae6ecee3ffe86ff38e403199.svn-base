using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using LogicaNegocio.Mantenimiento;
using LogicaNegocio.Seguridad;

namespace LogicaNegocio.CalculosFinancieros.DeudaInterna
{
    public class clsContabilizarCancelaciones
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
        private static tiras tira = new tiras();
        private static Mantenimiento.clsReservasDetalle reservas = new Mantenimiento.clsReservasDetalle();
        private static Seguridad.tBitacora bitacora = new Seguridad.tBitacora();
        private static clsTituloValor titulo = new clsTituloValor();
        private static clsCostoTransaccion lcls_CostoTransaccion = new clsCostoTransaccion();
        private static Mantenimiento.clsDinamico dinamica = new Mantenimiento.clsDinamico();
        private static string lstr_separador_decimal = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;

        private decimal Truncate(decimal value, int length)
        {
            return Math.Truncate(value * 100) / 100;
        }

        public string Cancelacion(DateTime? lstr_FchInicio, DateTime? lstr_FchFin)
        {
            DateTime? _fchInicio = lstr_FchInicio == null ? DateTime.Today : lstr_FchInicio;
            DateTime? _fchFin = lstr_FchFin == null ? DateTime.Today : lstr_FchFin;

            string lstr_Mensaje = string.Empty;
            try
            {
                //DataTable ldat_TitulosValores = titulo.ConsultarTituloValor("%", "%", "%", "%", "%", "01/01/1900", "01/01/5000").Tables[0].Select("IndicadorCupon ='V'").CopyToDataTable();

                string query = "select * from cf.titulosvalores "+
                               "where estadovalor = 'Cancelada' "+
                               "and estado != 'CAN' "+
                               "and tiponegociacion != 'Compra' "+
                               "and indicadorcupon = 'V' "+
                               "and FchCancelacion between '" + _fchInicio.Value.ToString("yyyy-MM-dd") + "' and '" + _fchFin.Value.ToString("yyyy-MM-dd") + "'";

                DataSet lds_TitulosValores = dinamica.ConsultarDinamico(query);
                DataTable ldat_TitulosValores = lds_TitulosValores.Tables[0];

                DataSet lds_Nemotecnicos = nemotecnico.ConsultarNemotecnicos(null, null, null, null, null);
                DataTable ldat_Nemotecnicos = lds_Nemotecnicos.Tables[0];

                for (int i = 0; i < ldat_TitulosValores.Rows.Count; i++)
                {
                    try
                    {
                        string lstr_Nemotecnico = ldat_TitulosValores.Rows[i]["NemoTecnico"].ToString().Trim();

                        string lstr_moneda = ldat_Nemotecnicos.Select("IdNemotecnico = '" + lstr_Nemotecnico + "'")[0]["IdMoneda"].ToString().Trim().Equals("CRCN")
                           ? "CRC" : ldat_Nemotecnicos.Select("IdNemotecnico = '" + lstr_Nemotecnico + "'")[0]["IdMoneda"].ToString().Trim();
                        
                        

                        //if (ldat_Nemotecnicos.Select("IdNemotecnico = '" + ldat_TitulosValores.Rows[i]["NemoTecnico"].ToString() + "'")[0]["Estado"].ToString().Trim() == "A"
                        //    && lstr_moneda == ldat_TitulosValores.Rows[i]["Moneda"].ToString())

                        if (ldat_Nemotecnicos.Select("IdNemotecnico = '" + lstr_Nemotecnico + "'")[0]["Estado"].ToString().Trim() == "A"
                            && lstr_moneda == (ldat_TitulosValores.Rows[i]["Moneda"].ToString().Equals("CRCN") ? "CRC" : ldat_TitulosValores.Rows[i]["Moneda"].ToString()))
                        {
                            lstr_Mensaje = CancelarTituloValor(
                                ldat_TitulosValores.Rows[i]["Tipo"].ToString(),
                                ldat_TitulosValores.Rows[i]["EstadoValor"].ToString(),
                                Convert.ToDateTime(ldat_TitulosValores.Rows[i]["FchValor"].ToString()),
                                Convert.ToDateTime(ldat_TitulosValores.Rows[i]["FchVencimiento"].ToString()),
                                Convert.ToDateTime(ldat_TitulosValores.Rows[i]["FchCancelacion"].ToString()),
                                ldat_TitulosValores.Rows[i]["Propietario"].ToString(),
                                ldat_TitulosValores.Rows[i]["PlazoValor"].ToString(),

                                Convert.ToDecimal(ldat_TitulosValores.Rows[i]["ValorTransadoBruto"].ToString()),
                                ldat_TitulosValores.Rows[i]["Moneda"].ToString().Equals("CRCN") ? "CRC" : ldat_TitulosValores.Rows[i]["Moneda"].ToString(),

                                ldat_TitulosValores.Rows[i]["NroValor"].ToString(),
                                ldat_TitulosValores.Rows[i]["NemoTecnico"].ToString(),
                                ldat_TitulosValores.Rows[i]["NroEmisionSerie"].ToString().Trim(),
                                Convert.ToDecimal(ldat_TitulosValores.Rows[i]["ValorFacial"].ToString()),
                                Convert.ToDecimal(ldat_TitulosValores.Rows[i]["RendimientoPorDescuento"].ToString()),
                                Convert.ToDecimal(ldat_TitulosValores.Rows[i]["ImpuestoPagado"].ToString()),
                                Convert.ToDecimal(ldat_TitulosValores.Rows[i]["ValorTransadoNeto"].ToString()),
                                Convert.ToDecimal(ldat_TitulosValores.Rows[i]["Premio"].ToString()),

                                "SINPE: " + ldat_TitulosValores.Rows[i]["ModuloSINPE"].ToString().Trim() + "-" + "T.B: " +
                                ldat_TitulosValores.Rows[i]["TasaBruta"].ToString().Trim() + "-" + "T.N: " +
                                ldat_TitulosValores.Rows[i]["TasaNeta"].ToString().Trim() + "-" + "Plazo: " +
                                ldat_TitulosValores.Rows[i]["PlazoValor"].ToString().Trim(),

                                ldat_TitulosValores.Rows[i]["ModuloSINPE"].ToString().Trim(),

                                Convert.ToDateTime(ldat_TitulosValores.Rows[i]["FchModifica"].ToString()));
                        }
                    }
                    catch(Exception ex)
                    {
                        ex.ToString();
                       string a= ex.StackTrace;
                    }
                }
            }
            catch (Exception ex)
            {
                lstr_Mensaje = ex.ToString();
            }
            return lstr_Mensaje;
        }

        public string CancelarTituloValor(
            string lstr_Tipo,
            string lstr_EstadoValor,
            DateTime ldt_FchValor,
            DateTime ldt_FchVencimiento,
            DateTime ldt_FchCancelacion,
            string lstr_Propietario,

            string lstr_Plazo,

            decimal ldec_ValorTransadoBruto,
            string lstr_Moneda,
            string lstr_NroValor,
            string lstr_Nemotecnico,
            string lstr_emision,
            decimal ldec_ValorFacial,
            decimal ldec_RendimientoXDescuento,//descuento
            decimal ldec_ImpuestoPagado,
            decimal ldec_ValorTransadoNeto,
            decimal ldec_Premio,//prima
            string lstr_Detalle,
            string lstr_Origen,
            DateTime ldt_FchModifica)
        {
            string lstr_Mensaje = string.Empty;
            string lstr_codAsiento = String.Empty;
            wsDeudaInterna.wsDeudaInterna wsDeudaInterna = new wsDeudaInterna.wsDeudaInterna();
            decimal percentaje = get_total_percentace(lstr_emision);

            RegistroContable LineaAsiento;
            List<RegistroContable> Asiento = new List<RegistroContable>();

            RegistroDiferencial LineaDiferencial;
            List<RegistroDiferencial> Diferencial = new List<RegistroDiferencial>();

            DataTable ldat_Tira;
            string lint_EsPublico = "PUBLICO";
            string lstr_Operacion = string.Empty;
            string lstr_NomOperacion = string.Empty;
            decimal ldec_TipoCambioColones = Convert.ToDecimal(tipocambio.ConsultarTiposCambio("CRCN", ldt_FchCancelacion, "3140", "N").Tables[0].Rows[0]["Valor"].ToString());
            decimal ldec_TipoCambioUDE = Convert.ToDecimal(tipocambio.ConsultarTiposCambio("UDE", ldt_FchCancelacion, "", "N").Tables[0].Rows[0]["Valor"].ToString());
            decimal ldec_monto = 0;
            decimal ldec_montoUde = 0;
            string lstr_Referencia = "";
            string lstr_PrimaDescuento = string.Empty;
            string lint_PlazoValor = string.Empty;
            decimal diasAnnos = 1;


            //
            if (lstr_Origen == "Rdi")
                diasAnnos = 360;

            //Define si el propietario es público o privado
            if (propietario.ConsultarPropietarios(string.Empty, string.Empty, string.Empty, lstr_Propietario, "S").Tables[0].Rows.Count == 0)
            {
                lint_EsPublico = "PRIVADO";
            }

            //Prima o Descuento
            if (ldec_ValorFacial >= ldec_ValorTransadoBruto)
                lstr_PrimaDescuento = "INT_DEV_D";
            else
                lstr_PrimaDescuento = "INT_DEV_P";

            //Plazo
            if ((ldt_FchVencimiento - DateTime.Today).Days >= 360)
                lint_PlazoValor = "LP";
            else
                lint_PlazoValor = "CP";

            //Define si trasciende o no el periodo
            if (ldt_FchValor.Year != ldt_FchVencimiento.Year)
            {
                //lstr_Trasciende = "T";
                if(lstr_Nemotecnico == "PT")
                    lstr_PrimaDescuento = lstr_PrimaDescuento + "_PT";
                else
                    lstr_PrimaDescuento = lstr_PrimaDescuento + "_T";
            }
            else
            {
                if (lstr_Nemotecnico == "PT")
                    lstr_PrimaDescuento = lstr_PrimaDescuento + "_PT";
                else
                    lstr_PrimaDescuento = lstr_PrimaDescuento + "_NT";
            }

            decimal ldec_TipoCambio = lstr_Moneda.Equals("USD") ? ldec_TipoCambioColones : (lstr_Moneda.Equals("CRC") ? 1 : ldec_TipoCambioUDE);
            
            bool SinError = true;

                switch (lstr_Tipo.ToLower())
                {
                    #region cero cupon
                    case "cero cupón":
                        {
                            #region Define si el título es a corto plazo y no trasciende en el periodo
                            if ((Convert.ToDecimal(lstr_Plazo) <= 365) && (ldt_FchValor.Year == ldt_FchVencimiento.Year))
                            {
                                lstr_Operacion = lstr_Moneda.Equals("USD") ? "ID43" : (lstr_Moneda.Equals("CRC") ? "ID41" : "ID41");


                                DataSet lds_Operaciones = loperacion.ConsultarOperaciones(lstr_Operacion, "IdModulo IN ('DI')", "");
                                if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
                                {
                                    lstr_NomOperacion = lds_Operaciones.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
                                }

                                ldat_Tira = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID").Tables[0].Clone();
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", "CAPITAL").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", lstr_PrimaDescuento).Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico.Trim() + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);

                                foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                {
                                    string operacion = ldr_Row["CodigoAuxiliar2"].ToString();

                                    int index = ldat_Tira.Rows.IndexOf(ldr_Row);

                                    switch (operacion.Trim())
                                    {
                                        case "CAPITAL": 
                                            {
                                                ldec_monto = percentaje*(lstr_Moneda.Equals("USD") ? ldec_ValorFacial : (lstr_Moneda.Equals("CRC") ? ldec_ValorFacial : ldec_ValorFacial * ldec_TipoCambioUDE));
                                                ldec_montoUde = percentaje*(lstr_Moneda.Equals("UDE") ? ldec_ValorFacial : 0);
                                                break;
                                            }
                                        case "INT_DEV_P_T":
                                        case "INT_DEV_P_PT":
                                        case "INT_DEV_P_NT": 
                                            {
                                                ldec_monto = percentaje *( lstr_Moneda.Equals("USD") ? Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) : (lstr_Moneda.Equals("CRC") ? Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) : Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) * ldec_TipoCambioUDE));
                                                ldec_montoUde = percentaje *( lstr_Moneda.Equals("UDE") ? Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) : 0);
                                                break; 
                                            }
                                        case "INT_DEV_D_T":
                                        case "INT_DEV_D_PT":
                                        case "INT_DEV_D_NT":
                                            {
                                                ldec_monto = percentaje *( lstr_Moneda.Equals("USD") ? Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) : (lstr_Moneda.Equals("CRC") ? Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) : Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) * ldec_TipoCambioUDE));
                                                ldec_montoUde = percentaje *( lstr_Moneda.Equals("UDE") ? Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) : 0);
                                                break; 
                                            }
                                        case "AMORT":
                                            {
                                                ldec_monto = percentaje *( lstr_Moneda.Equals("USD") ? ldec_ValorTransadoBruto : (lstr_Moneda.Equals("CRC") ? ldec_ValorTransadoBruto : ldec_ValorTransadoBruto * ldec_TipoCambioUDE));
                                                ldec_montoUde = percentaje *( lstr_Moneda.Equals("UDE") ? ldec_ValorTransadoBruto : 0);
                                                break; 
                                            }
                                    }

                                    lstr_Referencia = lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Cancelación";

                                    SinError = true;

                                    #region pospre debe
                                    //Validar que el pos pre sea diferente de PP_Balance y que el monto sea mayor al seleccionado
                                    string lstr_Monto = string.Empty;
                                    DataTable lds_Datos = new DataTable();
                                    decimal ldec_MontoTotal = 0;
                                    string reservasError = "";
                                    string lstr_NuevoPosPrePago = string.Empty;
                                    DataSet ldat_Reservas = new DataSet();

                                    ldat_Reservas = dinamica.ConsultarDinamico("select * from ma.reservasdetalle where left(idprograma,4)=year(getdate()) and idcuentacontable = '" + ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim() + "' and idpospre = '" + ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim() + "'");
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
                                                ldec_MontoTotal += Convert.ToDecimal(lstr_Monto.Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal));
                                            }
                                        }

                                        if (Convert.ToDecimal(ldec_MontoTotal) >= (ldec_monto * ldec_TipoCambio))
                                        {
                                            //Genera el asiento
                                            decimal ldec_SaldoCont = ldec_monto;
                                            decimal ldec_Saldo = (ldec_monto * ldec_TipoCambio);

                                            foreach (DataRow drForm in lds_Datos.Rows)
                                            {
                                                if (Convert.ToDecimal(drForm["Monto"]) > 0 && ldec_Saldo >= 0)
                                                {
                                                    decimal reservaTpoCambio = Convert.ToDecimal(drForm["Monto"].ToString()) / ldec_TipoCambio;

                                                    LineaAsiento = new RegistroContable();
                                                    LineaAsiento.Lstr_Referencia = lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia;
                                                    LineaAsiento.Lstr_Fecha = ldt_FchCancelacion.ToString("dd.MM.yyyy");
                                                    LineaAsiento.Lstr_Cuenta = ldr_Row["IdCuentaContable"].ToString().Trim();
                                                    LineaAsiento.Lstr_ClaveContable = ldr_Row["IdClaveContable"].ToString().Trim();
                                                    LineaAsiento.Lstr_Moneda = ldr_Row["CodigoAuxiliar"].ToString().Trim().Substring(0, 3);
                                                    LineaAsiento.Lstr_TextoInfo = lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim();
                                                    LineaAsiento.Lstr_CentroCosto = ldr_Row["IdCentroCosto"].ToString().Trim();
                                                    LineaAsiento.Lstr_CentroBeneficio = ldr_Row["IdCentroBeneficio"].ToString().Trim();
                                                    LineaAsiento.Lstr_ElementoPEP = ldr_Row["IdElementoPEP"].ToString().Trim();
                                                    LineaAsiento.Lstr_PosPre = ldr_Row["IdPosPre"].ToString().Trim().ToUpper(); 
                                                    LineaAsiento.Lstr_CentroGestor = ldr_Row["IdCentroGestor"].ToString().Trim();
                                                    LineaAsiento.Lstr_Fondo = ldr_Row["IdFondo"].ToString().Trim();
                                                    LineaAsiento.Lstr_DocPres = drForm["IdReserva"].ToString().Trim();
                                                    LineaAsiento.Lstr_PosDocPres = drForm["Posicion"].ToString().Trim();
                                                    LineaAsiento.Ldec_Monto = Truncate(ldec_SaldoCont > reservaTpoCambio ? reservaTpoCambio : ldec_SaldoCont, 2);
                                                    LineaAsiento.Ldec_TipoCambio = ldec_TipoCambioColones;
                                                    LineaAsiento.Lstr_MonedaTiutlo = lstr_Moneda;
                                                    Asiento.Add(LineaAsiento);

                                                    LineaDiferencial = new RegistroDiferencial();
                                                    LineaDiferencial.Lstr_MonedaTiutlo = lstr_Moneda;
                                                    LineaDiferencial.Lstr_Nemotecnico = lstr_Nemotecnico;
                                                    LineaDiferencial.Lstr_Plazo = "CP";
                                                    LineaDiferencial.Lstr_Propietario = lint_EsPublico;
                                                    LineaDiferencial.Lstr_DescripcionCuenta = operacion;
                                                    LineaDiferencial.Ldec_TpoCambioUDE = ldec_TipoCambioUDE;
                                                    LineaDiferencial.Ldec_MontoUDE = ldec_montoUde;
                                                    LineaDiferencial.Lstr_Cuenta = ldr_Row["IdCuentaContable"].ToString().Trim();
                                                    LineaDiferencial.Lstr_IdClaveContable = ldr_Row["IdClaveContable"].ToString().Trim();
                                                    Diferencial.Add(LineaDiferencial);

                                                }

                                                //Resta el saldo
                                                ldec_SaldoCont = ldec_SaldoCont - (Convert.ToDecimal(drForm["Monto"].ToString()) / ldec_TipoCambio);
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
                                            LineaAsiento = new RegistroContable();
                                            LineaAsiento.Lstr_Referencia = lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia;
                                            LineaAsiento.Lstr_Fecha = ldt_FchCancelacion.ToString("dd.MM.yyyy");
                                            LineaAsiento.Lstr_Cuenta = ldr_Row["IdCuentaContable"].ToString().Trim();
                                            LineaAsiento.Lstr_ClaveContable = ldr_Row["IdClaveContable"].ToString().Trim();
                                            LineaAsiento.Lstr_Moneda = ldr_Row["CodigoAuxiliar"].ToString().Trim().Substring(0, 3);
                                            LineaAsiento.Lstr_TextoInfo = lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim();
                                            LineaAsiento.Lstr_CentroCosto = ldr_Row["IdCentroCosto"].ToString().Trim();
                                            LineaAsiento.Lstr_CentroBeneficio = ldr_Row["IdCentroBeneficio"].ToString().Trim();
                                            LineaAsiento.Lstr_ElementoPEP = ldr_Row["IdElementoPEP"].ToString().Trim();
                                            LineaAsiento.Lstr_PosPre = ldr_Row["IdPosPre"].ToString().Trim().ToUpper();
                                            LineaAsiento.Lstr_CentroGestor = ldr_Row["IdCentroGestor"].ToString().Trim();
                                            LineaAsiento.Lstr_Fondo = ldr_Row["IdFondo"].ToString().Trim();
                                            LineaAsiento.Lstr_DocPres = ldr_Row["DocPresupuestario"].ToString().Trim();
                                            LineaAsiento.Lstr_PosDocPres = ldr_Row["PosDocPresupuestario"].ToString().Trim();
                                            LineaAsiento.Ldec_Monto = Truncate(ldec_monto, 2);
                                            LineaAsiento.Ldec_TipoCambio = ldec_TipoCambioColones;
                                            LineaAsiento.Lstr_MonedaTiutlo = lstr_Moneda;
                                            Asiento.Add(LineaAsiento);

                                            LineaDiferencial = new RegistroDiferencial();
                                            LineaDiferencial.Lstr_MonedaTiutlo = lstr_Moneda;
                                            LineaDiferencial.Lstr_Nemotecnico = lstr_Nemotecnico;
                                            LineaDiferencial.Lstr_Plazo = "CP";
                                            LineaDiferencial.Lstr_Propietario = lint_EsPublico;
                                            LineaDiferencial.Lstr_DescripcionCuenta = operacion;
                                            LineaDiferencial.Ldec_TpoCambioUDE = ldec_TipoCambioUDE;
                                            LineaDiferencial.Ldec_MontoUDE = ldec_montoUde;
                                            LineaDiferencial.Lstr_Cuenta = ldr_Row["IdCuentaContable"].ToString().Trim();
                                            LineaDiferencial.Lstr_IdClaveContable = ldr_Row["IdClaveContable"].ToString().Trim();
                                            Diferencial.Add(LineaDiferencial);
                                        }
                                        else
                                        {
                                            bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion,"DI"), "Resultado de Contabilización: \n 1 - [E] No hay reservas correspondientes a la cuenta " + ldat_Tira.Rows[0]["IdCuentaContable"].ToString().Trim() + " con fondo " + ldat_Tira.Rows[0]["IdPosPre"].ToString().Trim(), lstr_Operacion, lstr_NroValor + "-" + lstr_Nemotecnico, "G206");
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
                                        ldat_Reservas = dinamica.ConsultarDinamico("select * from ma.reservasdetalle where left(idprograma,4)=year(getdate()) and idcuentacontable = '" + ldat_Tira.Rows[index]["IdCuentaContable2"].ToString().Trim() + "' and idpospre = '" + ldat_Tira.Rows[index]["IdPosPre2"].ToString().Trim() + "'");
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
                                                    reservasError += "Posición Presupuestaria: " + drForm["IdPosPre"].ToString().Trim() + "Reserva :" + drForm["IdReserva"].ToString().Trim() + ", Posición: " + drForm["Posicion"].ToString().Trim() + "\n";
                                                    ldec_MontoTotal += Convert.ToDecimal(lstr_Monto.Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal));
                                                }
                                            }

                                            if (Convert.ToDecimal(ldec_MontoTotal) >= (ldec_monto * ldec_TipoCambio))
                                            {
                                                //Genera el asiento
                                                decimal ldec_SaldoCont = ldec_monto;
                                                decimal ldec_Saldo = (ldec_monto * ldec_TipoCambio);

                                                foreach (DataRow drForm in lds_Datos.Rows)
                                                {
                                                    if (Convert.ToDecimal(drForm["Monto"]) > 0 && ldec_Saldo >= 0)
                                                    {
                                                        decimal reservaTpoCambio = Convert.ToDecimal(drForm["Monto"].ToString()) / ldec_TipoCambio;

                                                        LineaAsiento = new RegistroContable();
                                                        LineaAsiento.Lstr_Referencia = lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia;
                                                        LineaAsiento.Lstr_Fecha = ldt_FchCancelacion.ToString("dd.MM.yyyy");
                                                        LineaAsiento.Lstr_Cuenta = ldr_Row["IdCuentaContable2"].ToString().Trim();
                                                        LineaAsiento.Lstr_ClaveContable = ldr_Row["IdClaveContable2"].ToString().Trim();
                                                        LineaAsiento.Lstr_Moneda = ldr_Row["CodigoAuxiliar"].ToString().Trim().Substring(0, 3);
                                                        LineaAsiento.Lstr_TextoInfo = lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim();
                                                        LineaAsiento.Lstr_CentroCosto = ldr_Row["IdCentroCosto2"].ToString().Trim();
                                                        LineaAsiento.Lstr_CentroBeneficio = ldr_Row["IdCentroBeneficio2"].ToString().Trim();
                                                        LineaAsiento.Lstr_ElementoPEP = ldr_Row["IdElementoPEP2"].ToString().Trim();
                                                        LineaAsiento.Lstr_PosPre = ldr_Row["IdPosPre2"].ToString().Trim();
                                                        LineaAsiento.Lstr_CentroGestor = ldr_Row["IdCentroGestor2"].ToString().Trim();
                                                        LineaAsiento.Lstr_Fondo = ldr_Row["IdFondo2"].ToString().Trim();
                                                        LineaAsiento.Lstr_DocPres = drForm["IdReserva"].ToString().Trim();
                                                        LineaAsiento.Lstr_PosDocPres = drForm["Posicion"].ToString().Trim();
                                                        LineaAsiento.Ldec_Monto = Truncate(ldec_SaldoCont > reservaTpoCambio ? reservaTpoCambio : ldec_SaldoCont, 2);
                                                        LineaAsiento.Ldec_TipoCambio = ldec_TipoCambioColones;
                                                        LineaAsiento.Lstr_MonedaTiutlo = lstr_Moneda;
                                                        Asiento.Add(LineaAsiento);
                                                    }

                                                    //Resta el saldo
                                                    ldec_SaldoCont = ldec_SaldoCont - (Convert.ToDecimal(drForm["Monto"].ToString()) / ldec_TipoCambio);
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
                                                LineaAsiento = new RegistroContable();
                                                LineaAsiento.Lstr_Referencia = lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia;
                                                LineaAsiento.Lstr_Fecha = ldt_FchCancelacion.ToString("dd.MM.yyyy");
                                                LineaAsiento.Lstr_Cuenta = ldr_Row["IdCuentaContable2"].ToString().Trim();
                                                LineaAsiento.Lstr_ClaveContable = ldr_Row["IdClaveContable2"].ToString().Trim();
                                                LineaAsiento.Lstr_Moneda = ldr_Row["CodigoAuxiliar"].ToString().Trim().Substring(0, 3);
                                                LineaAsiento.Lstr_TextoInfo = lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim();
                                                LineaAsiento.Lstr_CentroCosto = ldr_Row["IdCentroCosto2"].ToString().Trim();
                                                LineaAsiento.Lstr_CentroBeneficio = ldr_Row["IdCentroBeneficio2"].ToString().Trim();
                                                LineaAsiento.Lstr_ElementoPEP = ldr_Row["IdElementoPEP2"].ToString().Trim();
                                                LineaAsiento.Lstr_PosPre = ldr_Row["IdPosPre2"].ToString().Trim();
                                                LineaAsiento.Lstr_CentroGestor = ldr_Row["IdCentroGestor2"].ToString().Trim();
                                                LineaAsiento.Lstr_Fondo = ldr_Row["IdFondo2"].ToString().Trim();
                                                LineaAsiento.Lstr_DocPres = ldr_Row["DocPresupuestario2"].ToString().Trim();
                                                LineaAsiento.Lstr_PosDocPres = ldr_Row["PosDocPresupuestario2"].ToString().Trim();
                                                LineaAsiento.Ldec_Monto = Truncate(ldec_monto, 2);
                                                LineaAsiento.Ldec_TipoCambio = ldec_TipoCambioColones;
                                                LineaAsiento.Lstr_MonedaTiutlo = lstr_Moneda;
                                                Asiento.Add(LineaAsiento);
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

                            #region Define si el título es a corto plazo, pero trasciende en el periodo
                            else if ((Convert.ToDecimal(lstr_Plazo) <= 365) &&
                                (ldt_FchValor.Year != ldt_FchVencimiento.Year))
                            {
                                lstr_Operacion = lstr_Moneda.Equals("USD") ? "ID43" : (lstr_Moneda.Equals("CRC") ? "ID41" : "ID41");

                                DataSet lds_Operaciones = loperacion.ConsultarOperaciones(lstr_Operacion, "IdModulo IN ('DI')", "");
                                if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
                                {
                                    lstr_NomOperacion = lds_Operaciones.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
                                }

                                string lstr_NemotecnicoTemp = lstr_Moneda.Equals("USD") ? (lstr_Nemotecnico.Equals("PT$") ? "PT$" : "") : (lstr_Nemotecnico.Equals("PT") ? "PT" : "");

                                ldat_Tira = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID").Tables[0].Clone();
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", "CAPITAL").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", lstr_PrimaDescuento).Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico.Trim() + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID", "CP", "AMORT").Tables[0].Rows)
                                    ldat_Tira.ImportRow(dr_tira);

                                foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                {
                                    string operacion = ldr_Row["CodigoAuxiliar2"].ToString();

                                    int index = ldat_Tira.Rows.IndexOf(ldr_Row);

                                    switch (operacion.Trim().ToUpper())
                                    {
                                        case "CAPITAL":
                                            {
                                                ldec_monto = percentaje*(lstr_Moneda.Equals("USD") ? ldec_ValorFacial : (lstr_Moneda.Equals("CRC") ? ldec_ValorFacial : ldec_ValorFacial * ldec_TipoCambioUDE));
                                                ldec_montoUde = percentaje*(lstr_Moneda.Equals("UDE") ? ldec_ValorFacial : 0);
                                                break;
                                            }
                                        case "INT_DEV_P_T":
                                        case "INT_DEV_P_PT":
                                        case "INT_DEV_P_NT":
                                            {
                                                ldec_monto = percentaje *( lstr_Moneda.Equals("USD") ? Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) : (lstr_Moneda.Equals("CRC") ? Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) : Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) * ldec_TipoCambioUDE));
                                                ldec_montoUde = percentaje *( lstr_Moneda.Equals("UDE") ? Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) : 0);
                                                break;
                                            }
                                        case "INT_DEV_D_T":
                                        case "INT_DEV_D_PT":
                                        case "INT_DEV_D_NT":
                                            {
                                                ldec_monto = percentaje *( lstr_Moneda.Equals("USD") ? Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) : (lstr_Moneda.Equals("CRC") ? Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) : Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) * ldec_TipoCambioUDE));
                                                ldec_montoUde = percentaje *( lstr_Moneda.Equals("UDE") ? Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) : 0);
                                                break;
                                            }
                                        case "AMORT":
                                            {
                                                ldec_monto = percentaje *( lstr_Moneda.Equals("USD") ? ldec_ValorTransadoBruto : (lstr_Moneda.Equals("CRC") ? ldec_ValorTransadoBruto : ldec_ValorTransadoBruto * ldec_TipoCambioUDE));
                                                ldec_montoUde = percentaje *( lstr_Moneda.Equals("UDE") ? ldec_ValorTransadoBruto : 0);
                                                break;
                                            }
                                    }

                                    lstr_Referencia = lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Cancelación";

                                    SinError = true;

                                    #region pospre debe
                                    //Validar que el pos pre sea diferente de PP_Balance y que el monto sea mayor al seleccionado
                                    string lstr_Monto = string.Empty;
                                    DataTable lds_Datos = new DataTable();
                                    decimal ldec_MontoTotal = 0;
                                    string reservasError = "";
                                    string lstr_NuevoPosPrePago = string.Empty;
                                    DataSet ldat_Reservas = new DataSet();

                                    ldat_Reservas = dinamica.ConsultarDinamico("select * from ma.reservasdetalle where left(idprograma,4)=year(getdate()) and idcuentacontable = '" + ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim() + "' and idpospre = '" + ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim() + "'");
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
                                                ldec_MontoTotal += Convert.ToDecimal(lstr_Monto.Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal));
                                            }
                                        }

                                        if (Convert.ToDecimal(ldec_MontoTotal) >= (ldec_monto * ldec_TipoCambio))
                                        {
                                            //Genera el asiento
                                            decimal ldec_SaldoCont = ldec_monto;
                                            decimal ldec_Saldo = (ldec_monto * ldec_TipoCambio);

                                            foreach (DataRow drForm in lds_Datos.Rows)
                                            {
                                                if (Convert.ToDecimal(drForm["Monto"]) > 0 && ldec_Saldo >= 0)
                                                {
                                                    decimal reservaTpoCambio = Convert.ToDecimal(drForm["Monto"].ToString()) / ldec_TipoCambio;

                                                    LineaAsiento = new RegistroContable();
                                                    LineaAsiento.Lstr_Referencia = lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia;
                                                    LineaAsiento.Lstr_Fecha = ldt_FchCancelacion.ToString("dd.MM.yyyy");
                                                    LineaAsiento.Lstr_Cuenta = ldr_Row["IdCuentaContable"].ToString().Trim();
                                                    LineaAsiento.Lstr_ClaveContable = ldr_Row["IdClaveContable"].ToString().Trim();
                                                    LineaAsiento.Lstr_Moneda = ldr_Row["CodigoAuxiliar"].ToString().Trim().Substring(0, 3);
                                                    LineaAsiento.Lstr_TextoInfo = lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim();
                                                    LineaAsiento.Lstr_CentroCosto = ldr_Row["IdCentroCosto"].ToString().Trim();
                                                    LineaAsiento.Lstr_CentroBeneficio = ldr_Row["IdCentroBeneficio"].ToString().Trim();
                                                    LineaAsiento.Lstr_ElementoPEP = ldr_Row["IdElementoPEP"].ToString().Trim();
                                                    LineaAsiento.Lstr_PosPre = ldr_Row["IdPosPre"].ToString().Trim().ToUpper();
                                                    LineaAsiento.Lstr_CentroGestor = ldr_Row["IdCentroGestor"].ToString().Trim();
                                                    LineaAsiento.Lstr_Fondo = ldr_Row["IdFondo"].ToString().Trim();
                                                    LineaAsiento.Lstr_DocPres = drForm["IdReserva"].ToString().Trim();
                                                    LineaAsiento.Lstr_PosDocPres = drForm["Posicion"].ToString().Trim();
                                                    LineaAsiento.Ldec_Monto = Truncate(ldec_SaldoCont > reservaTpoCambio ? reservaTpoCambio : ldec_SaldoCont, 2);
                                                    LineaAsiento.Ldec_TipoCambio = ldec_TipoCambioColones;
                                                    LineaAsiento.Lstr_MonedaTiutlo = lstr_Moneda;
                                                    Asiento.Add(LineaAsiento);

                                                    LineaDiferencial = new RegistroDiferencial();
                                                    LineaDiferencial.Lstr_MonedaTiutlo = lstr_Moneda;
                                                    LineaDiferencial.Lstr_Nemotecnico = lstr_Nemotecnico;
                                                    LineaDiferencial.Lstr_Plazo = "CP";
                                                    LineaDiferencial.Lstr_Propietario = lint_EsPublico;
                                                    LineaDiferencial.Lstr_DescripcionCuenta = operacion;
                                                    LineaDiferencial.Ldec_TpoCambioUDE = ldec_TipoCambioUDE;
                                                    LineaDiferencial.Ldec_MontoUDE = ldec_montoUde;
                                                    LineaDiferencial.Lstr_Cuenta = ldr_Row["IdCuentaContable"].ToString().Trim();
                                                    LineaDiferencial.Lstr_IdClaveContable = ldr_Row["IdClaveContable"].ToString().Trim();
                                                    Diferencial.Add(LineaDiferencial);

                                                }

                                                //Resta el saldo
                                                ldec_SaldoCont = ldec_SaldoCont - (Convert.ToDecimal(drForm["Monto"].ToString()) / ldec_TipoCambio);
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
                                            LineaAsiento = new RegistroContable();
                                            LineaAsiento.Lstr_Referencia = lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia;
                                            LineaAsiento.Lstr_Fecha = ldt_FchCancelacion.ToString("dd.MM.yyyy");
                                            LineaAsiento.Lstr_Cuenta = ldr_Row["IdCuentaContable"].ToString().Trim();
                                            LineaAsiento.Lstr_ClaveContable = ldr_Row["IdClaveContable"].ToString().Trim();
                                            LineaAsiento.Lstr_Moneda = ldr_Row["CodigoAuxiliar"].ToString().Trim().Substring(0, 3);
                                            LineaAsiento.Lstr_TextoInfo = lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim();
                                            LineaAsiento.Lstr_CentroCosto = ldr_Row["IdCentroCosto"].ToString().Trim();
                                            LineaAsiento.Lstr_CentroBeneficio = ldr_Row["IdCentroBeneficio"].ToString().Trim();
                                            LineaAsiento.Lstr_ElementoPEP = ldr_Row["IdElementoPEP"].ToString().Trim();
                                            LineaAsiento.Lstr_PosPre = ldr_Row["IdPosPre"].ToString().Trim().ToUpper();
                                            LineaAsiento.Lstr_CentroGestor = ldr_Row["IdCentroGestor"].ToString().Trim();
                                            LineaAsiento.Lstr_Fondo = ldr_Row["IdFondo"].ToString().Trim();
                                            LineaAsiento.Lstr_DocPres = ldr_Row["DocPresupuestario"].ToString().Trim();
                                            LineaAsiento.Lstr_PosDocPres = ldr_Row["PosDocPresupuestario"].ToString().Trim();
                                            LineaAsiento.Ldec_Monto = Truncate(ldec_monto, 2);
                                            LineaAsiento.Ldec_TipoCambio = ldec_TipoCambioColones;
                                            LineaAsiento.Lstr_MonedaTiutlo = lstr_Moneda;
                                            Asiento.Add(LineaAsiento);

                                            LineaDiferencial = new RegistroDiferencial();
                                            LineaDiferencial.Lstr_MonedaTiutlo = lstr_Moneda;
                                            LineaDiferencial.Lstr_Nemotecnico = lstr_Nemotecnico;
                                            LineaDiferencial.Lstr_Plazo = "CP";
                                            LineaDiferencial.Lstr_Propietario = lint_EsPublico;
                                            LineaDiferencial.Lstr_DescripcionCuenta = operacion;
                                            LineaDiferencial.Ldec_TpoCambioUDE = ldec_TipoCambioUDE;
                                            LineaDiferencial.Ldec_MontoUDE = ldec_montoUde;
                                            LineaDiferencial.Lstr_Cuenta = ldr_Row["IdCuentaContable"].ToString().Trim();
                                            LineaDiferencial.Lstr_IdClaveContable = ldr_Row["IdClaveContable"].ToString().Trim();
                                            Diferencial.Add(LineaDiferencial);
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
                                        ldat_Reservas = dinamica.ConsultarDinamico("select * from ma.reservasdetalle where left(idprograma,4)=year(getdate()) and idcuentacontable = '" + ldat_Tira.Rows[index]["IdCuentaContable2"].ToString().Trim() + "' and idpospre = '" + ldat_Tira.Rows[index]["IdPosPre2"].ToString().Trim() + "'");
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
                                                    reservasError += "Posición Presupuestaria: " + drForm["IdPosPre"].ToString().Trim() + "Reserva :" + drForm["IdReserva"].ToString().Trim() + ", Posición: " + drForm["Posicion"].ToString().Trim() + "\n";
                                                    ldec_MontoTotal += Convert.ToDecimal(lstr_Monto.Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal));
                                                }
                                            }

                                            if (Convert.ToDecimal(ldec_MontoTotal) >= (ldec_monto * ldec_TipoCambio))
                                            {
                                                //Genera el asiento
                                                decimal ldec_SaldoCont = ldec_monto;
                                                decimal ldec_Saldo = (ldec_monto * ldec_TipoCambio);

                                                foreach (DataRow drForm in lds_Datos.Rows)
                                                {
                                                    if (Convert.ToDecimal(drForm["Monto"]) > 0 && ldec_Saldo >= 0)
                                                    {
                                                        decimal reservaTpoCambio = Convert.ToDecimal(drForm["Monto"].ToString()) / ldec_TipoCambio;

                                                        LineaAsiento = new RegistroContable();
                                                        LineaAsiento.Lstr_Referencia = lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia;
                                                        LineaAsiento.Lstr_Fecha = ldt_FchCancelacion.ToString("dd.MM.yyyy");
                                                        LineaAsiento.Lstr_Cuenta = ldr_Row["IdCuentaContable2"].ToString().Trim();
                                                        LineaAsiento.Lstr_ClaveContable = ldr_Row["IdClaveContable2"].ToString().Trim();
                                                        LineaAsiento.Lstr_Moneda = ldr_Row["CodigoAuxiliar"].ToString().Trim().Substring(0, 3);
                                                        LineaAsiento.Lstr_TextoInfo = lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim();
                                                        LineaAsiento.Lstr_CentroCosto = ldr_Row["IdCentroCosto2"].ToString().Trim();
                                                        LineaAsiento.Lstr_CentroBeneficio = ldr_Row["IdCentroBeneficio2"].ToString().Trim();
                                                        LineaAsiento.Lstr_ElementoPEP = ldr_Row["IdElementoPEP2"].ToString().Trim();
                                                        LineaAsiento.Lstr_PosPre = ldr_Row["IdPosPre2"].ToString().Trim();
                                                        LineaAsiento.Lstr_CentroGestor = ldr_Row["IdCentroGestor2"].ToString().Trim();
                                                        LineaAsiento.Lstr_Fondo = ldr_Row["IdFondo2"].ToString().Trim();
                                                        LineaAsiento.Lstr_DocPres = drForm["IdReserva"].ToString().Trim();
                                                        LineaAsiento.Lstr_PosDocPres = drForm["Posicion"].ToString().Trim();
                                                        LineaAsiento.Ldec_Monto = Truncate(ldec_SaldoCont > reservaTpoCambio ? reservaTpoCambio : ldec_SaldoCont, 2);
                                                        LineaAsiento.Ldec_TipoCambio = ldec_TipoCambioColones;
                                                        LineaAsiento.Lstr_MonedaTiutlo = lstr_Moneda;
                                                        Asiento.Add(LineaAsiento);
                                                    }

                                                    //Resta el saldo
                                                    ldec_SaldoCont = ldec_SaldoCont - (Convert.ToDecimal(drForm["Monto"].ToString()) / ldec_TipoCambio);
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
                                            if (!ldat_Tira.Rows[index]["IdPosPre2"].ToString().Trim().StartsWith("E") )
                                            {
                                                LineaAsiento = new RegistroContable();
                                                LineaAsiento.Lstr_Referencia = lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia;
                                                LineaAsiento.Lstr_Fecha = ldt_FchCancelacion.ToString("dd.MM.yyyy");
                                                LineaAsiento.Lstr_Cuenta = ldr_Row["IdCuentaContable2"].ToString().Trim();
                                                LineaAsiento.Lstr_ClaveContable = ldr_Row["IdClaveContable2"].ToString().Trim();
                                                LineaAsiento.Lstr_Moneda = ldr_Row["CodigoAuxiliar"].ToString().Trim().Substring(0, 3);
                                                LineaAsiento.Lstr_TextoInfo = lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim();
                                                LineaAsiento.Lstr_CentroCosto = ldr_Row["IdCentroCosto2"].ToString().Trim();
                                                LineaAsiento.Lstr_CentroBeneficio = ldr_Row["IdCentroBeneficio2"].ToString().Trim();
                                                LineaAsiento.Lstr_ElementoPEP = ldr_Row["IdElementoPEP2"].ToString().Trim();
                                                LineaAsiento.Lstr_PosPre = ldr_Row["IdPosPre2"].ToString().Trim();
                                                LineaAsiento.Lstr_CentroGestor = ldr_Row["IdCentroGestor2"].ToString().Trim();
                                                LineaAsiento.Lstr_Fondo = ldr_Row["IdFondo2"].ToString().Trim();
                                                LineaAsiento.Lstr_DocPres = ldr_Row["DocPresupuestario2"].ToString().Trim();
                                                LineaAsiento.Lstr_PosDocPres = ldr_Row["PosDocPresupuestario2"].ToString().Trim();
                                                LineaAsiento.Ldec_Monto = Truncate(ldec_monto, 2);
                                                LineaAsiento.Ldec_TipoCambio = ldec_TipoCambioColones;
                                                LineaAsiento.Lstr_MonedaTiutlo = lstr_Moneda;
                                                Asiento.Add(LineaAsiento);
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

                            #region Define si el título es a largo plazo
                            else if ((Convert.ToDecimal(lstr_Plazo) > 365))// && (lstr_Nemotecnico != "PT"))
                            {
                                lstr_Operacion = lstr_Moneda.Equals("USD") ? "ID44" : (lstr_Moneda.Equals("CRC") ? "ID42" : "ID42");

                                DataSet lds_Operaciones = loperacion.ConsultarOperaciones(lstr_Operacion, "IdModulo IN ('DI')", "");
                                if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
                                {
                                    lstr_NomOperacion = lds_Operaciones.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
                                }

                                ldat_Tira = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID").Tables[0].Clone();
                                try
                                {
                                    foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", "CAPITAL").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico + "'").CopyToDataTable().Rows)
                                        ldat_Tira.ImportRow(dr_tira);
                                }
                                catch
                                {

                                }
                                //Error en el que cae
                                try
                                {
                                    foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", lstr_PrimaDescuento).Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico.Trim() + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                 }
                                catch
                                {

                                }
                                try
                                {
                                    foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID", "CP", "AMORT").Tables[0].Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                }
                                catch
                                {

                                }
                                foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                {
                                    string operacion = ldr_Row["CodigoAuxiliar2"].ToString();

                                    int index = ldat_Tira.Rows.IndexOf(ldr_Row);

                                    switch (operacion.Trim())
                                    {
                                        case "CAPITAL":
                                            {
                                                ldec_monto = percentaje*(lstr_Moneda.Equals("USD") ? ldec_ValorFacial : (lstr_Moneda.Equals("CRC") ? ldec_ValorFacial : ldec_ValorFacial * ldec_TipoCambioUDE));
                                                ldec_montoUde = percentaje * (lstr_Moneda.Equals("UDE") ? ldec_ValorFacial : 0);
                                                break;
                                            }
                                        case "INT_DEV_P_T":
                                        case "INT_DEV_P_PT":
                                        case "INT_DEV_P_NT":
                                            {
                                                ldec_monto = percentaje *( lstr_Moneda.Equals("USD") ? Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) : (lstr_Moneda.Equals("CRC") ? Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) : Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) * ldec_TipoCambioUDE));
                                                ldec_montoUde = percentaje *( lstr_Moneda.Equals("UDE") ? Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) : 0);
                                                break;
                                            }
                                        case "INT_DEV_D_T":
                                        case "INT_DEV_D_PT":
                                        case "INT_DEV_D_NT":
                                            {
                                                ldec_monto = percentaje *( lstr_Moneda.Equals("USD") ? Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) : (lstr_Moneda.Equals("CRC") ? Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) : Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) * ldec_TipoCambioUDE));
                                                ldec_montoUde = percentaje *( lstr_Moneda.Equals("UDE") ? Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) : 0);
                                                break;
                                            }
                                        case "AMORT":
                                            {
                                                ldec_monto = percentaje *( lstr_Moneda.Equals("USD") ? ldec_ValorTransadoBruto : (lstr_Moneda.Equals("CRC") ? ldec_ValorTransadoBruto : ldec_ValorTransadoBruto * ldec_TipoCambioUDE));
                                                ldec_montoUde = percentaje *( lstr_Moneda.Equals("UDE") ? ldec_ValorTransadoBruto : 0);
                                                break;
                                            }
                                    }

                                    lstr_Referencia = lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Cancelación";

                                    SinError = true;

                                    #region pospre debe
                                    //Validar que el pos pre sea diferente de PP_Balance y que el monto sea mayor al seleccionado
                                    string lstr_Monto = string.Empty;
                                    DataTable lds_Datos = new DataTable();
                                    decimal ldec_MontoTotal = 0;
                                    string reservasError = "";
                                    string lstr_NuevoPosPrePago = string.Empty;
                                    DataSet ldat_Reservas = new DataSet();

                                    ldat_Reservas = dinamica.ConsultarDinamico("select * from ma.reservasdetalle where left(idprograma,4)=year(getdate()) and idcuentacontable = '" + ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim() + "' and idpospre = '" + ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim() + "'");
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
                                                ldec_MontoTotal += Convert.ToDecimal(lstr_Monto.Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal));
                                            }
                                        }

                                        if (Convert.ToDecimal(ldec_MontoTotal) >= (ldec_monto * ldec_TipoCambio))
                                        {
                                            //Genera el asiento
                                            decimal ldec_SaldoCont = ldec_monto;
                                            decimal ldec_Saldo = (ldec_monto * ldec_TipoCambio);

                                            foreach (DataRow drForm in lds_Datos.Rows)
                                            {
                                                if (Convert.ToDecimal(drForm["Monto"]) > 0 && ldec_Saldo >= 0)
                                                {
                                                    decimal reservaTpoCambio = Convert.ToDecimal(drForm["Monto"].ToString()) / ldec_TipoCambio;

                                                    LineaAsiento = new RegistroContable();
                                                    LineaAsiento.Lstr_Referencia = lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia;
                                                    LineaAsiento.Lstr_Fecha = ldt_FchCancelacion.ToString("dd.MM.yyyy");
                                                    LineaAsiento.Lstr_Cuenta = ldr_Row["IdCuentaContable"].ToString().Trim();
                                                    LineaAsiento.Lstr_ClaveContable = ldr_Row["IdClaveContable"].ToString().Trim();
                                                    LineaAsiento.Lstr_Moneda = ldr_Row["CodigoAuxiliar"].ToString().Trim().Substring(0, 3);
                                                    LineaAsiento.Lstr_TextoInfo = lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim();
                                                    LineaAsiento.Lstr_CentroCosto = ldr_Row["IdCentroCosto"].ToString().Trim();
                                                    LineaAsiento.Lstr_CentroBeneficio = ldr_Row["IdCentroBeneficio"].ToString().Trim();
                                                    LineaAsiento.Lstr_ElementoPEP = ldr_Row["IdElementoPEP"].ToString().Trim();
                                                    LineaAsiento.Lstr_PosPre = ldr_Row["IdPosPre"].ToString().Trim().ToUpper();
                                                    LineaAsiento.Lstr_CentroGestor = ldr_Row["IdCentroGestor"].ToString().Trim();
                                                    LineaAsiento.Lstr_Fondo = ldr_Row["IdFondo"].ToString().Trim();
                                                    LineaAsiento.Lstr_DocPres = drForm["IdReserva"].ToString().Trim();
                                                    LineaAsiento.Lstr_PosDocPres = drForm["Posicion"].ToString().Trim();
                                                    LineaAsiento.Ldec_Monto = Truncate(ldec_SaldoCont > reservaTpoCambio ? reservaTpoCambio : ldec_SaldoCont, 2);
                                                    LineaAsiento.Ldec_TipoCambio = ldec_TipoCambioColones;
                                                    LineaAsiento.Lstr_MonedaTiutlo = lstr_Moneda;
                                                    Asiento.Add(LineaAsiento);

                                                    LineaDiferencial = new RegistroDiferencial();
                                                    LineaDiferencial.Lstr_MonedaTiutlo = lstr_Moneda;
                                                    LineaDiferencial.Lstr_Nemotecnico = lstr_Nemotecnico;
                                                    LineaDiferencial.Lstr_Plazo = "CP";
                                                    LineaDiferencial.Lstr_Propietario = lint_EsPublico;
                                                    LineaDiferencial.Lstr_DescripcionCuenta = operacion;
                                                    LineaDiferencial.Ldec_TpoCambioUDE = ldec_TipoCambioUDE;
                                                    LineaDiferencial.Ldec_MontoUDE = ldec_montoUde;
                                                    LineaDiferencial.Lstr_Cuenta = ldr_Row["IdCuentaContable"].ToString().Trim();
                                                    LineaDiferencial.Lstr_IdClaveContable = ldr_Row["IdClaveContable"].ToString().Trim();
                                                    Diferencial.Add(LineaDiferencial);

                                                }

                                                //Resta el saldo
                                                ldec_SaldoCont = ldec_SaldoCont - (Convert.ToDecimal(drForm["Monto"].ToString()) / ldec_TipoCambio);
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
                                            LineaAsiento = new RegistroContable();
                                            LineaAsiento.Lstr_Referencia = lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia;
                                            LineaAsiento.Lstr_Fecha = ldt_FchCancelacion.ToString("dd.MM.yyyy");
                                            LineaAsiento.Lstr_Cuenta = ldr_Row["IdCuentaContable"].ToString().Trim();
                                            LineaAsiento.Lstr_ClaveContable = ldr_Row["IdClaveContable"].ToString().Trim();
                                            LineaAsiento.Lstr_Moneda = ldr_Row["CodigoAuxiliar"].ToString().Trim().Substring(0, 3);
                                            LineaAsiento.Lstr_TextoInfo = lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim();
                                            LineaAsiento.Lstr_CentroCosto = ldr_Row["IdCentroCosto"].ToString().Trim();
                                            LineaAsiento.Lstr_CentroBeneficio = ldr_Row["IdCentroBeneficio"].ToString().Trim();
                                            LineaAsiento.Lstr_ElementoPEP = ldr_Row["IdElementoPEP"].ToString().Trim();
                                            LineaAsiento.Lstr_PosPre = ldr_Row["IdPosPre"].ToString().Trim().ToUpper();
                                            LineaAsiento.Lstr_CentroGestor = ldr_Row["IdCentroGestor"].ToString().Trim();
                                            LineaAsiento.Lstr_Fondo = ldr_Row["IdFondo"].ToString().Trim();
                                            LineaAsiento.Lstr_DocPres = ldr_Row["DocPresupuestario"].ToString().Trim();
                                            LineaAsiento.Lstr_PosDocPres = ldr_Row["PosDocPresupuestario"].ToString().Trim();
                                            LineaAsiento.Ldec_Monto = Truncate(ldec_monto, 2);
                                            LineaAsiento.Ldec_TipoCambio = ldec_TipoCambioColones;
                                            LineaAsiento.Lstr_MonedaTiutlo = lstr_Moneda;
                                            Asiento.Add(LineaAsiento);

                                            LineaDiferencial = new RegistroDiferencial();
                                            LineaDiferencial.Lstr_MonedaTiutlo = lstr_Moneda;
                                            LineaDiferencial.Lstr_Nemotecnico = lstr_Nemotecnico;
                                            LineaDiferencial.Lstr_Plazo = "CP";
                                            LineaDiferencial.Lstr_Propietario = lint_EsPublico;
                                            LineaDiferencial.Lstr_DescripcionCuenta = operacion;
                                            LineaDiferencial.Ldec_TpoCambioUDE = ldec_TipoCambioUDE;
                                            LineaDiferencial.Ldec_MontoUDE = ldec_montoUde;
                                            LineaDiferencial.Lstr_Cuenta = ldr_Row["IdCuentaContable"].ToString().Trim();
                                            LineaDiferencial.Lstr_IdClaveContable = ldr_Row["IdClaveContable"].ToString().Trim();
                                            Diferencial.Add(LineaDiferencial);
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
                                        ldat_Reservas = dinamica.ConsultarDinamico("select * from ma.reservasdetalle where left(idprograma,4)=year(getdate()) and idcuentacontable = '" + ldat_Tira.Rows[index]["IdCuentaContable2"].ToString().Trim() + "' and idpospre = '" + ldat_Tira.Rows[index]["IdPosPre2"].ToString().Trim() + "'");
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
                                                    reservasError += "Posición Presupuestaria: " + drForm["IdPosPre"].ToString().Trim() + "Reserva :" + drForm["IdReserva"].ToString().Trim() + ", Posición: " + drForm["Posicion"].ToString().Trim() + "\n";
                                                    ldec_MontoTotal += Convert.ToDecimal(lstr_Monto.Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal));
                                                }
                                            }

                                            if (Convert.ToDecimal(ldec_MontoTotal) >= (ldec_monto * ldec_TipoCambio))
                                            {
                                                //Genera el asiento
                                                decimal ldec_SaldoCont = ldec_monto;
                                                decimal ldec_Saldo = (ldec_monto * ldec_TipoCambio);

                                                foreach (DataRow drForm in lds_Datos.Rows)
                                                {
                                                    if (Convert.ToDecimal(drForm["Monto"]) > 0 && ldec_Saldo >= 0)
                                                    {
                                                        decimal reservaTpoCambio = Convert.ToDecimal(drForm["Monto"].ToString()) / ldec_TipoCambio;

                                                        LineaAsiento = new RegistroContable();
                                                        LineaAsiento.Lstr_Referencia = lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia;
                                                        LineaAsiento.Lstr_Fecha = ldt_FchCancelacion.ToString("dd.MM.yyyy");
                                                        LineaAsiento.Lstr_Cuenta = ldr_Row["IdCuentaContable2"].ToString().Trim();
                                                        LineaAsiento.Lstr_ClaveContable = ldr_Row["IdClaveContable2"].ToString().Trim();
                                                        LineaAsiento.Lstr_Moneda = ldr_Row["CodigoAuxiliar"].ToString().Trim().Substring(0, 3);
                                                        LineaAsiento.Lstr_TextoInfo = lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim();
                                                        LineaAsiento.Lstr_CentroCosto = ldr_Row["IdCentroCosto2"].ToString().Trim();
                                                        LineaAsiento.Lstr_CentroBeneficio = ldr_Row["IdCentroBeneficio2"].ToString().Trim();
                                                        LineaAsiento.Lstr_ElementoPEP = ldr_Row["IdElementoPEP2"].ToString().Trim();
                                                        LineaAsiento.Lstr_PosPre = ldr_Row["IdPosPre2"].ToString().Trim();
                                                        LineaAsiento.Lstr_CentroGestor = ldr_Row["IdCentroGestor2"].ToString().Trim();
                                                        LineaAsiento.Lstr_Fondo = ldr_Row["IdFondo2"].ToString().Trim();
                                                        LineaAsiento.Lstr_DocPres = drForm["IdReserva"].ToString().Trim();
                                                        LineaAsiento.Lstr_PosDocPres = drForm["Posicion"].ToString().Trim();
                                                        LineaAsiento.Ldec_Monto = Truncate(ldec_SaldoCont > reservaTpoCambio ? reservaTpoCambio : ldec_SaldoCont, 2);
                                                        LineaAsiento.Ldec_TipoCambio = ldec_TipoCambioColones;
                                                        LineaAsiento.Lstr_MonedaTiutlo = lstr_Moneda;
                                                        Asiento.Add(LineaAsiento);
                                                    }

                                                    //Resta el saldo
                                                    ldec_SaldoCont = ldec_SaldoCont - (Convert.ToDecimal(drForm["Monto"].ToString()) / ldec_TipoCambio);
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
                                                LineaAsiento = new RegistroContable();
                                                LineaAsiento.Lstr_Referencia = lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia;
                                                LineaAsiento.Lstr_Fecha = ldt_FchCancelacion.ToString("dd.MM.yyyy");
                                                LineaAsiento.Lstr_Cuenta = ldr_Row["IdCuentaContable2"].ToString().Trim();
                                                LineaAsiento.Lstr_ClaveContable = ldr_Row["IdClaveContable2"].ToString().Trim();
                                                LineaAsiento.Lstr_Moneda = ldr_Row["CodigoAuxiliar"].ToString().Trim().Substring(0, 3);
                                                LineaAsiento.Lstr_TextoInfo = lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim();
                                                LineaAsiento.Lstr_CentroCosto = ldr_Row["IdCentroCosto2"].ToString().Trim();
                                                LineaAsiento.Lstr_CentroBeneficio = ldr_Row["IdCentroBeneficio2"].ToString().Trim();
                                                LineaAsiento.Lstr_ElementoPEP = ldr_Row["IdElementoPEP2"].ToString().Trim();
                                                LineaAsiento.Lstr_PosPre = ldr_Row["IdPosPre2"].ToString().Trim();
                                                LineaAsiento.Lstr_CentroGestor = ldr_Row["IdCentroGestor2"].ToString().Trim();
                                                LineaAsiento.Lstr_Fondo = ldr_Row["IdFondo2"].ToString().Trim();
                                                LineaAsiento.Lstr_DocPres = ldr_Row["DocPresupuestario2"].ToString().Trim();
                                                LineaAsiento.Lstr_PosDocPres = ldr_Row["PosDocPresupuestario2"].ToString().Trim();
                                                LineaAsiento.Ldec_Monto = Truncate(ldec_monto, 2);
                                                LineaAsiento.Ldec_TipoCambio = ldec_TipoCambioColones;
                                                LineaAsiento.Lstr_MonedaTiutlo = lstr_Moneda;
                                                Asiento.Add(LineaAsiento);
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

                            break;
                        }
                    #endregion

                    #region tasa fija y tasa variable
                    case "tasa fija":
                        //No hay codigo --- Asi estaba
                        //break;
                    case "tasa variable":
                        {
                            #region Define si el título es a corto plazo y no trasciende en el periodo
                            if ((Convert.ToDecimal(lstr_Plazo) <= diasAnnos) &&
                                (ldt_FchValor.Year == ldt_FchVencimiento.Year))
                            {
                                lstr_Operacion = lstr_Moneda.Equals("USD") ? "ID43" : (lstr_Moneda.Equals("CRC") ? "ID41" : "ID41");

                                DataSet lds_Operaciones = loperacion.ConsultarOperaciones(lstr_Operacion, "IdModulo IN ('DI')", "");
                                if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
                                {
                                    lstr_NomOperacion = lds_Operaciones.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
                                }

                                string lstr_NemotecnicoTemp = lstr_Moneda.Equals("USD") ? (lstr_Nemotecnico.Equals("PT$") ? "PT$" : "") : (lstr_Nemotecnico.Equals("PT") ? "PT" : "");

                                ldat_Tira = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID").Tables[0].Clone();
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", "CAPITAL").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", lstr_PrimaDescuento).Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico.Trim() + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);

                                foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                {
                                    string operacion = ldr_Row["CodigoAuxiliar2"].ToString();

                                    int index = ldat_Tira.Rows.IndexOf(ldr_Row);

                                    switch (operacion.Trim())
                                    {
                                        case "CAPITAL":
                                            {
                                                ldec_monto = percentaje*(lstr_Moneda.Equals("USD") ? ldec_ValorFacial : (lstr_Moneda.Equals("CRC") ? ldec_ValorFacial : ldec_ValorFacial * ldec_TipoCambioUDE));
                                                ldec_montoUde = percentaje*(lstr_Moneda.Equals("UDE") ? ldec_ValorFacial : 0);
                                                break;
                                            }
                                        case "INT_DEV_P_T":
                                        case "INT_DEV_P_PT":
                                        case "INT_DEV_P_NT":
                                            {
                                                ldec_monto = percentaje *( lstr_Moneda.Equals("USD") ? Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) : (lstr_Moneda.Equals("CRC") ? Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) : Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) * ldec_TipoCambioUDE));
                                                ldec_montoUde = percentaje *( lstr_Moneda.Equals("UDE") ? Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) : 0);
                                                break;
                                            }
                                        case "INT_DEV_D_T":
                                        case "INT_DEV_D_PT":
                                        case "INT_DEV_D_NT":
                                            {
                                                ldec_monto = percentaje *( lstr_Moneda.Equals("USD") ? Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) : (lstr_Moneda.Equals("CRC") ? Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) : Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) * ldec_TipoCambioUDE));
                                                ldec_montoUde = percentaje *( lstr_Moneda.Equals("UDE") ? Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) : 0);
                                                break;
                                            }
                                        case "AMORT":
                                            {
                                                ldec_monto = percentaje *( lstr_Moneda.Equals("USD") ? ldec_ValorFacial : (lstr_Moneda.Equals("CRC") ? ldec_ValorFacial : ldec_ValorFacial * ldec_TipoCambioUDE));
                                                ldec_montoUde = percentaje *( lstr_Moneda.Equals("UDE") ? ldec_ValorFacial : 0);
                                                break;
                                            }
                                    }

                                    lstr_Referencia = lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Cancelación";

                                    SinError = true;

                                    #region pospre debe
                                    //Validar que el pos pre sea diferente de PP_Balance y que el monto sea mayor al seleccionado
                                    string lstr_Monto = string.Empty;
                                    DataTable lds_Datos = new DataTable();
                                    decimal ldec_MontoTotal = 0;
                                    string reservasError = "";
                                    string lstr_NuevoPosPrePago = string.Empty;
                                    DataSet ldat_Reservas = new DataSet();

                                    ldat_Reservas = dinamica.ConsultarDinamico("select * from ma.reservasdetalle where left(idprograma,4)=year(getdate()) and idcuentacontable = '" + ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim() + "' and idpospre = '" + ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim() + "'");
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
                                                ldec_MontoTotal += Convert.ToDecimal(lstr_Monto.Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal));
                                            }
                                        }

                                        if (Convert.ToDecimal(ldec_MontoTotal) >= (ldec_monto * ldec_TipoCambio))
                                        {
                                            //Genera el asiento
                                            decimal ldec_SaldoCont = ldec_monto;
                                            decimal ldec_Saldo = (ldec_monto * ldec_TipoCambio);

                                            foreach (DataRow drForm in lds_Datos.Rows)
                                            {
                                                if (Convert.ToDecimal(drForm["Monto"]) > 0 && ldec_Saldo >= 0)
                                                {
                                                    decimal reservaTpoCambio = Convert.ToDecimal(drForm["Monto"].ToString()) / ldec_TipoCambio;

                                                    LineaAsiento = new RegistroContable();
                                                    LineaAsiento.Lstr_Referencia = lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia;
                                                    LineaAsiento.Lstr_Fecha = ldt_FchCancelacion.ToString("dd.MM.yyyy");
                                                    LineaAsiento.Lstr_Cuenta = ldr_Row["IdCuentaContable"].ToString().Trim();
                                                    LineaAsiento.Lstr_ClaveContable = ldr_Row["IdClaveContable"].ToString().Trim();
                                                    LineaAsiento.Lstr_Moneda = ldr_Row["CodigoAuxiliar"].ToString().Trim().Substring(0, 3);
                                                    LineaAsiento.Lstr_TextoInfo = lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim();
                                                    LineaAsiento.Lstr_CentroCosto = ldr_Row["IdCentroCosto"].ToString().Trim();
                                                    LineaAsiento.Lstr_CentroBeneficio = ldr_Row["IdCentroBeneficio"].ToString().Trim();
                                                    LineaAsiento.Lstr_ElementoPEP = ldr_Row["IdElementoPEP"].ToString().Trim();
                                                    LineaAsiento.Lstr_PosPre = ldr_Row["IdPosPre"].ToString().Trim().ToUpper();
                                                    LineaAsiento.Lstr_CentroGestor = ldr_Row["IdCentroGestor"].ToString().Trim();
                                                    LineaAsiento.Lstr_Fondo = ldr_Row["IdFondo"].ToString().Trim();
                                                    LineaAsiento.Lstr_DocPres = drForm["IdReserva"].ToString().Trim();
                                                    LineaAsiento.Lstr_PosDocPres = drForm["Posicion"].ToString().Trim();
                                                    LineaAsiento.Ldec_Monto = Truncate(ldec_SaldoCont > reservaTpoCambio ? reservaTpoCambio : ldec_SaldoCont, 2);
                                                    LineaAsiento.Ldec_TipoCambio = ldec_TipoCambioColones;
                                                    LineaAsiento.Lstr_MonedaTiutlo = lstr_Moneda;
                                                    Asiento.Add(LineaAsiento);

                                                    LineaDiferencial = new RegistroDiferencial();
                                                    LineaDiferencial.Lstr_MonedaTiutlo = lstr_Moneda;
                                                    LineaDiferencial.Lstr_Nemotecnico = lstr_Nemotecnico;
                                                    LineaDiferencial.Lstr_Plazo = "CP";
                                                    LineaDiferencial.Lstr_Propietario = lint_EsPublico;
                                                    LineaDiferencial.Lstr_DescripcionCuenta = operacion;
                                                    LineaDiferencial.Ldec_TpoCambioUDE = ldec_TipoCambioUDE;
                                                    LineaDiferencial.Ldec_MontoUDE = ldec_montoUde;
                                                    LineaDiferencial.Lstr_Cuenta = ldr_Row["IdCuentaContable"].ToString().Trim();
                                                    LineaDiferencial.Lstr_IdClaveContable = ldr_Row["IdClaveContable"].ToString().Trim();
                                                    Diferencial.Add(LineaDiferencial);

                                                }

                                                //Resta el saldo
                                                ldec_SaldoCont = ldec_SaldoCont - (Convert.ToDecimal(drForm["Monto"].ToString()) / ldec_TipoCambio);
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
                                            LineaAsiento = new RegistroContable();
                                            LineaAsiento.Lstr_Referencia = lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia;
                                            LineaAsiento.Lstr_Fecha = ldt_FchCancelacion.ToString("dd.MM.yyyy");
                                            LineaAsiento.Lstr_Cuenta = ldr_Row["IdCuentaContable"].ToString().Trim();
                                            LineaAsiento.Lstr_ClaveContable = ldr_Row["IdClaveContable"].ToString().Trim();
                                            LineaAsiento.Lstr_Moneda = ldr_Row["CodigoAuxiliar"].ToString().Trim().Substring(0, 3);
                                            LineaAsiento.Lstr_TextoInfo = lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim();
                                            LineaAsiento.Lstr_CentroCosto = ldr_Row["IdCentroCosto"].ToString().Trim();
                                            LineaAsiento.Lstr_CentroBeneficio = ldr_Row["IdCentroBeneficio"].ToString().Trim();
                                            LineaAsiento.Lstr_ElementoPEP = ldr_Row["IdElementoPEP"].ToString().Trim();
                                            LineaAsiento.Lstr_PosPre = ldr_Row["IdPosPre"].ToString().Trim().ToUpper();
                                            LineaAsiento.Lstr_CentroGestor = ldr_Row["IdCentroGestor"].ToString().Trim();
                                            LineaAsiento.Lstr_Fondo = ldr_Row["IdFondo"].ToString().Trim();
                                            LineaAsiento.Lstr_DocPres = ldr_Row["DocPresupuestario"].ToString().Trim();
                                            LineaAsiento.Lstr_PosDocPres = ldr_Row["PosDocPresupuestario"].ToString().Trim();
                                            LineaAsiento.Ldec_Monto = Truncate(ldec_monto, 2);
                                            LineaAsiento.Ldec_TipoCambio = ldec_TipoCambioColones;
                                            LineaAsiento.Lstr_MonedaTiutlo = lstr_Moneda;
                                            Asiento.Add(LineaAsiento);

                                            LineaDiferencial = new RegistroDiferencial();
                                            LineaDiferencial.Lstr_MonedaTiutlo = lstr_Moneda;
                                            LineaDiferencial.Lstr_Nemotecnico = lstr_Nemotecnico;
                                            LineaDiferencial.Lstr_Plazo = "CP";
                                            LineaDiferencial.Lstr_Propietario = lint_EsPublico;
                                            LineaDiferencial.Lstr_DescripcionCuenta = operacion;
                                            LineaDiferencial.Ldec_TpoCambioUDE = ldec_TipoCambioUDE;
                                            LineaDiferencial.Ldec_MontoUDE = ldec_montoUde;
                                            LineaDiferencial.Lstr_Cuenta = ldr_Row["IdCuentaContable"].ToString().Trim();
                                            LineaDiferencial.Lstr_IdClaveContable = ldr_Row["IdClaveContable"].ToString().Trim();
                                            Diferencial.Add(LineaDiferencial);
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
                                        ldat_Reservas = dinamica.ConsultarDinamico("select * from ma.reservasdetalle where left(idprograma,4)=year(getdate()) and idcuentacontable = '" + ldat_Tira.Rows[index]["IdCuentaContable2"].ToString().Trim() + "' and idpospre = '" + ldat_Tira.Rows[index]["IdPosPre2"].ToString().Trim() + "'");
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
                                                    reservasError += "Posición Presupuestaria: " + drForm["IdPosPre"].ToString().Trim() + "Reserva :" + drForm["IdReserva"].ToString().Trim() + ", Posición: " + drForm["Posicion"].ToString().Trim() + "\n";
                                                    ldec_MontoTotal += Convert.ToDecimal(lstr_Monto.Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal));
                                                }
                                            }

                                            if (Convert.ToDecimal(ldec_MontoTotal) >= (ldec_monto * ldec_TipoCambio))
                                            {
                                                //Genera el asiento
                                                decimal ldec_SaldoCont = ldec_monto;
                                                decimal ldec_Saldo = (ldec_monto * ldec_TipoCambio);

                                                foreach (DataRow drForm in lds_Datos.Rows)
                                                {
                                                    if (Convert.ToDecimal(drForm["Monto"]) > 0 && ldec_Saldo >= 0)
                                                    {
                                                        decimal reservaTpoCambio = Convert.ToDecimal(drForm["Monto"].ToString()) / ldec_TipoCambio;

                                                        LineaAsiento = new RegistroContable();
                                                        LineaAsiento.Lstr_Referencia = lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia;
                                                        LineaAsiento.Lstr_Fecha = ldt_FchCancelacion.ToString("dd.MM.yyyy");
                                                        LineaAsiento.Lstr_Cuenta = ldr_Row["IdCuentaContable2"].ToString().Trim();
                                                        LineaAsiento.Lstr_ClaveContable = ldr_Row["IdClaveContable2"].ToString().Trim();
                                                        LineaAsiento.Lstr_Moneda = ldr_Row["CodigoAuxiliar"].ToString().Trim().Substring(0, 3);
                                                        LineaAsiento.Lstr_TextoInfo = lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim();
                                                        LineaAsiento.Lstr_CentroCosto = ldr_Row["IdCentroCosto2"].ToString().Trim();
                                                        LineaAsiento.Lstr_CentroBeneficio = ldr_Row["IdCentroBeneficio2"].ToString().Trim();
                                                        LineaAsiento.Lstr_ElementoPEP = ldr_Row["IdElementoPEP2"].ToString().Trim();
                                                        LineaAsiento.Lstr_PosPre = ldr_Row["IdPosPre2"].ToString().Trim();
                                                        LineaAsiento.Lstr_CentroGestor = ldr_Row["IdCentroGestor2"].ToString().Trim();
                                                        LineaAsiento.Lstr_Fondo = ldr_Row["IdFondo2"].ToString().Trim();
                                                        LineaAsiento.Lstr_DocPres = drForm["IdReserva"].ToString().Trim();
                                                        LineaAsiento.Lstr_PosDocPres = drForm["Posicion"].ToString().Trim();
                                                        LineaAsiento.Ldec_Monto = Truncate(ldec_SaldoCont > reservaTpoCambio ? reservaTpoCambio : ldec_SaldoCont, 2);
                                                        LineaAsiento.Ldec_TipoCambio = ldec_TipoCambioColones;
                                                        LineaAsiento.Lstr_MonedaTiutlo = lstr_Moneda;
                                                        Asiento.Add(LineaAsiento);
                                                    }

                                                    //Resta el saldo
                                                    ldec_SaldoCont = ldec_SaldoCont - (Convert.ToDecimal(drForm["Monto"].ToString()) / ldec_TipoCambio);
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
                                                LineaAsiento = new RegistroContable();
                                                LineaAsiento.Lstr_Referencia = lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia;
                                                LineaAsiento.Lstr_Fecha = ldt_FchCancelacion.ToString("dd.MM.yyyy");
                                                LineaAsiento.Lstr_Cuenta = ldr_Row["IdCuentaContable2"].ToString().Trim();
                                                LineaAsiento.Lstr_ClaveContable = ldr_Row["IdClaveContable2"].ToString().Trim();
                                                LineaAsiento.Lstr_Moneda = ldr_Row["CodigoAuxiliar"].ToString().Trim().Substring(0, 3);
                                                LineaAsiento.Lstr_TextoInfo = lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim();
                                                LineaAsiento.Lstr_CentroCosto = ldr_Row["IdCentroCosto2"].ToString().Trim();
                                                LineaAsiento.Lstr_CentroBeneficio = ldr_Row["IdCentroBeneficio2"].ToString().Trim();
                                                LineaAsiento.Lstr_ElementoPEP = ldr_Row["IdElementoPEP2"].ToString().Trim();
                                                LineaAsiento.Lstr_PosPre = ldr_Row["IdPosPre2"].ToString().Trim();
                                                LineaAsiento.Lstr_CentroGestor = ldr_Row["IdCentroGestor2"].ToString().Trim();
                                                LineaAsiento.Lstr_Fondo = ldr_Row["IdFondo2"].ToString().Trim();
                                                LineaAsiento.Lstr_DocPres = ldr_Row["DocPresupuestario2"].ToString().Trim();
                                                LineaAsiento.Lstr_PosDocPres = ldr_Row["PosDocPresupuestario2"].ToString().Trim();
                                                LineaAsiento.Ldec_Monto = Truncate(ldec_monto, 2);
                                                LineaAsiento.Ldec_TipoCambio = ldec_TipoCambioColones;
                                                LineaAsiento.Lstr_MonedaTiutlo = lstr_Moneda;
                                                Asiento.Add(LineaAsiento);
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

                            #region Define si el título es a corto plazo, pero trasciende en el periodo
                            else if ((Convert.ToDecimal(lstr_Plazo) <= diasAnnos) &&
                            (ldt_FchValor.Year != ldt_FchVencimiento.Year))
                            {
                                lstr_Operacion = lstr_Moneda.Equals("USD") ? "ID43" : (lstr_Moneda.Equals("CRC") ? "ID41" : "ID41");

                                DataSet lds_Operaciones = loperacion.ConsultarOperaciones(lstr_Operacion, "IdModulo IN ('DI')", "");
                                if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
                                {
                                    lstr_NomOperacion = lds_Operaciones.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
                                }

                                string lstr_NemotecnicoTemp = lstr_Moneda.Equals("USD") ? (lstr_Nemotecnico.Equals("PT$") ? "PT$" : "") : (lstr_Nemotecnico.Equals("PT") ? "PT" : "");

                                ldat_Tira = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID").Tables[0].Clone();
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", "CAPITAL").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", lstr_PrimaDescuento).Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico.Trim() + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID", "CP", "AMORT").Tables[0].Rows)
                                    ldat_Tira.ImportRow(dr_tira);

                                foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                {
                                    string operacion = ldr_Row["CodigoAuxiliar2"].ToString();

                                    int index = ldat_Tira.Rows.IndexOf(ldr_Row);

                                    switch (operacion.Trim())
                                    {
                                        case "CAPITAL":
                                            {
                                                ldec_monto = percentaje*(lstr_Moneda.Equals("USD") ? ldec_ValorFacial : (lstr_Moneda.Equals("CRC") ? ldec_ValorFacial : ldec_ValorFacial * ldec_TipoCambioUDE));
                                                ldec_montoUde = percentaje*(lstr_Moneda.Equals("UDE") ? ldec_ValorFacial : 0);
                                                break;
                                            }
                                        case "INT_DEV_P_T":
                                        case "INT_DEV_P_PT":
                                        case "INT_DEV_P_NT":
                                            {
                                                ldec_monto = percentaje *( lstr_Moneda.Equals("USD") ? Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) : (lstr_Moneda.Equals("CRC") ? Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) : Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) * ldec_TipoCambioUDE));
                                                ldec_montoUde = percentaje *( lstr_Moneda.Equals("UDE") ? Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) : 0);
                                                break;
                                            }
                                        case "INT_DEV_D_T":
                                        case "INT_DEV_D_PT":
                                        case "INT_DEV_D_NT":
                                            {
                                                ldec_monto = percentaje *( lstr_Moneda.Equals("USD") ? Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) : (lstr_Moneda.Equals("CRC") ? Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) : Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) * ldec_TipoCambioUDE));
                                                ldec_montoUde = percentaje *( lstr_Moneda.Equals("UDE") ? Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) : 0);
                                                break;
                                            }
                                        case "AMORT":
                                            {
                                                ldec_monto = percentaje *( lstr_Moneda.Equals("USD") ? ldec_ValorFacial : (lstr_Moneda.Equals("CRC") ? ldec_ValorFacial : ldec_ValorFacial * ldec_TipoCambioUDE));
                                                ldec_montoUde = percentaje *( lstr_Moneda.Equals("UDE") ? ldec_ValorFacial : 0);
                                                break;
                                            }
                                    }

                                    lstr_Referencia = lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Cancelación";

                                    SinError = true;

                                    #region pospre debe
                                    //Validar que el pos pre sea diferente de PP_Balance y que el monto sea mayor al seleccionado
                                    string lstr_Monto = string.Empty;
                                    DataTable lds_Datos = new DataTable();
                                    decimal ldec_MontoTotal = 0;
                                    string reservasError = "";
                                    string lstr_NuevoPosPrePago = string.Empty;
                                    DataSet ldat_Reservas = new DataSet();

                                    ldat_Reservas = dinamica.ConsultarDinamico("select * from ma.reservasdetalle where left(idprograma,4)=year(getdate()) and idcuentacontable = '" + ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim() + "' and idpospre = '" + ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim() + "'");
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
                                                ldec_MontoTotal += Convert.ToDecimal(lstr_Monto.Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal));
                                            }
                                        }

                                        if (Convert.ToDecimal(ldec_MontoTotal) >= (ldec_monto * ldec_TipoCambio))
                                        {
                                            //Genera el asiento
                                            decimal ldec_SaldoCont = ldec_monto;
                                            decimal ldec_Saldo = (ldec_monto * ldec_TipoCambio);

                                            foreach (DataRow drForm in lds_Datos.Rows)
                                            {
                                                if (Convert.ToDecimal(drForm["Monto"]) > 0 && ldec_Saldo >= 0)
                                                {
                                                    decimal reservaTpoCambio = Convert.ToDecimal(drForm["Monto"].ToString()) / ldec_TipoCambio;

                                                    LineaAsiento = new RegistroContable();
                                                    LineaAsiento.Lstr_Referencia = lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia;
                                                    LineaAsiento.Lstr_Fecha = ldt_FchCancelacion.ToString("dd.MM.yyyy");
                                                    LineaAsiento.Lstr_Cuenta = ldr_Row["IdCuentaContable"].ToString().Trim();
                                                    LineaAsiento.Lstr_ClaveContable = ldr_Row["IdClaveContable"].ToString().Trim();
                                                    LineaAsiento.Lstr_Moneda = ldr_Row["CodigoAuxiliar"].ToString().Trim().Substring(0, 3);
                                                    LineaAsiento.Lstr_TextoInfo = lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim();
                                                    LineaAsiento.Lstr_CentroCosto = ldr_Row["IdCentroCosto"].ToString().Trim();
                                                    LineaAsiento.Lstr_CentroBeneficio = ldr_Row["IdCentroBeneficio"].ToString().Trim();
                                                    LineaAsiento.Lstr_ElementoPEP = ldr_Row["IdElementoPEP"].ToString().Trim();
                                                    LineaAsiento.Lstr_PosPre = ldr_Row["IdPosPre"].ToString().Trim().ToUpper();
                                                    LineaAsiento.Lstr_CentroGestor = ldr_Row["IdCentroGestor"].ToString().Trim();
                                                    LineaAsiento.Lstr_Fondo = ldr_Row["IdFondo"].ToString().Trim();
                                                    LineaAsiento.Lstr_DocPres = drForm["IdReserva"].ToString().Trim();
                                                    LineaAsiento.Lstr_PosDocPres = drForm["Posicion"].ToString().Trim();
                                                    LineaAsiento.Ldec_Monto = Truncate(ldec_SaldoCont > reservaTpoCambio ? reservaTpoCambio : ldec_SaldoCont, 2);
                                                    LineaAsiento.Ldec_TipoCambio = ldec_TipoCambioColones;
                                                    LineaAsiento.Lstr_MonedaTiutlo = lstr_Moneda;
                                                    Asiento.Add(LineaAsiento);

                                                    LineaDiferencial = new RegistroDiferencial();
                                                    LineaDiferencial.Lstr_MonedaTiutlo = lstr_Moneda;
                                                    LineaDiferencial.Lstr_Nemotecnico = lstr_Nemotecnico;
                                                    LineaDiferencial.Lstr_Plazo = "CP";
                                                    LineaDiferencial.Lstr_Propietario = lint_EsPublico;
                                                    LineaDiferencial.Lstr_DescripcionCuenta = operacion;
                                                    LineaDiferencial.Ldec_TpoCambioUDE = ldec_TipoCambioUDE;
                                                    LineaDiferencial.Ldec_MontoUDE = ldec_montoUde;
                                                    LineaDiferencial.Lstr_Cuenta = ldr_Row["IdCuentaContable"].ToString().Trim();
                                                    LineaDiferencial.Lstr_IdClaveContable = ldr_Row["IdClaveContable"].ToString().Trim();
                                                    Diferencial.Add(LineaDiferencial);

                                                }

                                                //Resta el saldo
                                                ldec_SaldoCont = ldec_SaldoCont - (Convert.ToDecimal(drForm["Monto"].ToString()) / ldec_TipoCambio);
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
                                            LineaAsiento = new RegistroContable();
                                            LineaAsiento.Lstr_Referencia = lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia;
                                            LineaAsiento.Lstr_Fecha = ldt_FchCancelacion.ToString("dd.MM.yyyy");
                                            LineaAsiento.Lstr_Cuenta = ldr_Row["IdCuentaContable"].ToString().Trim();
                                            LineaAsiento.Lstr_ClaveContable = ldr_Row["IdClaveContable"].ToString().Trim();
                                            LineaAsiento.Lstr_Moneda = ldr_Row["CodigoAuxiliar"].ToString().Trim().Substring(0, 3);
                                            LineaAsiento.Lstr_TextoInfo = lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim();
                                            LineaAsiento.Lstr_CentroCosto = ldr_Row["IdCentroCosto"].ToString().Trim();
                                            LineaAsiento.Lstr_CentroBeneficio = ldr_Row["IdCentroBeneficio"].ToString().Trim();
                                            LineaAsiento.Lstr_ElementoPEP = ldr_Row["IdElementoPEP"].ToString().Trim();
                                            LineaAsiento.Lstr_PosPre = ldr_Row["IdPosPre"].ToString().Trim().ToUpper();
                                            LineaAsiento.Lstr_CentroGestor = ldr_Row["IdCentroGestor"].ToString().Trim();
                                            LineaAsiento.Lstr_Fondo = ldr_Row["IdFondo"].ToString().Trim();
                                            LineaAsiento.Lstr_DocPres = ldr_Row["DocPresupuestario"].ToString().Trim();
                                            LineaAsiento.Lstr_PosDocPres = ldr_Row["PosDocPresupuestario"].ToString().Trim();
                                            LineaAsiento.Ldec_Monto = Truncate(ldec_monto, 2);
                                            LineaAsiento.Ldec_TipoCambio = ldec_TipoCambioColones;
                                            LineaAsiento.Lstr_MonedaTiutlo = lstr_Moneda;
                                            Asiento.Add(LineaAsiento);

                                            LineaDiferencial = new RegistroDiferencial();
                                            LineaDiferencial.Lstr_MonedaTiutlo = lstr_Moneda;
                                            LineaDiferencial.Lstr_Nemotecnico = lstr_Nemotecnico;
                                            LineaDiferencial.Lstr_Plazo = "CP";
                                            LineaDiferencial.Lstr_Propietario = lint_EsPublico;
                                            LineaDiferencial.Lstr_DescripcionCuenta = operacion;
                                            LineaDiferencial.Ldec_TpoCambioUDE = ldec_TipoCambioUDE;
                                            LineaDiferencial.Ldec_MontoUDE = ldec_montoUde;
                                            LineaDiferencial.Lstr_Cuenta = ldr_Row["IdCuentaContable"].ToString().Trim();
                                            LineaDiferencial.Lstr_IdClaveContable = ldr_Row["IdClaveContable"].ToString().Trim();
                                            Diferencial.Add(LineaDiferencial);
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
                                        ldat_Reservas = dinamica.ConsultarDinamico("select * from ma.reservasdetalle where left(idprograma,4)=year(getdate()) and idcuentacontable = '" + ldat_Tira.Rows[index]["IdCuentaContable2"].ToString().Trim() + "' and idpospre = '" + ldat_Tira.Rows[index]["IdPosPre2"].ToString().Trim() + "'");
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
                                                    reservasError += "Posición Presupuestaria: " + drForm["IdPosPre"].ToString().Trim() + "Reserva :" + drForm["IdReserva"].ToString().Trim() + ", Posición: " + drForm["Posicion"].ToString().Trim() + "\n";
                                                    ldec_MontoTotal += Convert.ToDecimal(lstr_Monto.Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal));
                                                }
                                            }

                                            if (Convert.ToDecimal(ldec_MontoTotal) >= (ldec_monto * ldec_TipoCambio))
                                            {
                                                //Genera el asiento
                                                decimal ldec_SaldoCont = ldec_monto;
                                                decimal ldec_Saldo = (ldec_monto * ldec_TipoCambio);

                                                foreach (DataRow drForm in lds_Datos.Rows)
                                                {
                                                    if (Convert.ToDecimal(drForm["Monto"]) > 0 && ldec_Saldo >= 0)
                                                    {
                                                        decimal reservaTpoCambio = Convert.ToDecimal(drForm["Monto"].ToString()) / ldec_TipoCambio;

                                                        LineaAsiento = new RegistroContable();
                                                        LineaAsiento.Lstr_Referencia = lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia;
                                                        LineaAsiento.Lstr_Fecha = ldt_FchCancelacion.ToString("dd.MM.yyyy");
                                                        LineaAsiento.Lstr_Cuenta = ldr_Row["IdCuentaContable2"].ToString().Trim();
                                                        LineaAsiento.Lstr_ClaveContable = ldr_Row["IdClaveContable2"].ToString().Trim();
                                                        LineaAsiento.Lstr_Moneda = ldr_Row["CodigoAuxiliar"].ToString().Trim().Substring(0, 3);
                                                        LineaAsiento.Lstr_TextoInfo = lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim();
                                                        LineaAsiento.Lstr_CentroCosto = ldr_Row["IdCentroCosto2"].ToString().Trim();
                                                        LineaAsiento.Lstr_CentroBeneficio = ldr_Row["IdCentroBeneficio2"].ToString().Trim();
                                                        LineaAsiento.Lstr_ElementoPEP = ldr_Row["IdElementoPEP2"].ToString().Trim();
                                                        LineaAsiento.Lstr_PosPre = ldr_Row["IdPosPre2"].ToString().Trim();
                                                        LineaAsiento.Lstr_CentroGestor = ldr_Row["IdCentroGestor2"].ToString().Trim();
                                                        LineaAsiento.Lstr_Fondo = ldr_Row["IdFondo2"].ToString().Trim();
                                                        LineaAsiento.Lstr_DocPres = drForm["IdReserva"].ToString().Trim();
                                                        LineaAsiento.Lstr_PosDocPres = drForm["Posicion"].ToString().Trim();
                                                        LineaAsiento.Ldec_Monto = Truncate(ldec_SaldoCont > reservaTpoCambio ? reservaTpoCambio : ldec_SaldoCont, 2);
                                                        LineaAsiento.Ldec_TipoCambio = ldec_TipoCambioColones;
                                                        LineaAsiento.Lstr_MonedaTiutlo = lstr_Moneda;
                                                        Asiento.Add(LineaAsiento);
                                                    }

                                                    //Resta el saldo
                                                    ldec_SaldoCont = ldec_SaldoCont - (Convert.ToDecimal(drForm["Monto"].ToString()) / ldec_TipoCambio);
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
                                                LineaAsiento = new RegistroContable();
                                                LineaAsiento.Lstr_Referencia = lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia;
                                                LineaAsiento.Lstr_Fecha = ldt_FchCancelacion.ToString("dd.MM.yyyy");
                                                LineaAsiento.Lstr_Cuenta = ldr_Row["IdCuentaContable2"].ToString().Trim();
                                                LineaAsiento.Lstr_ClaveContable = ldr_Row["IdClaveContable2"].ToString().Trim();
                                                LineaAsiento.Lstr_Moneda = ldr_Row["CodigoAuxiliar"].ToString().Trim().Substring(0, 3);
                                                LineaAsiento.Lstr_TextoInfo = lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim();
                                                LineaAsiento.Lstr_CentroCosto = ldr_Row["IdCentroCosto2"].ToString().Trim();
                                                LineaAsiento.Lstr_CentroBeneficio = ldr_Row["IdCentroBeneficio2"].ToString().Trim();
                                                LineaAsiento.Lstr_ElementoPEP = ldr_Row["IdElementoPEP2"].ToString().Trim();
                                                LineaAsiento.Lstr_PosPre = ldr_Row["IdPosPre2"].ToString().Trim();
                                                LineaAsiento.Lstr_CentroGestor = ldr_Row["IdCentroGestor2"].ToString().Trim();
                                                LineaAsiento.Lstr_Fondo = ldr_Row["IdFondo2"].ToString().Trim();
                                                LineaAsiento.Lstr_DocPres = ldr_Row["DocPresupuestario2"].ToString().Trim();
                                                LineaAsiento.Lstr_PosDocPres = ldr_Row["PosDocPresupuestario2"].ToString().Trim();
                                                LineaAsiento.Ldec_Monto = Truncate(ldec_monto, 2);
                                                LineaAsiento.Ldec_TipoCambio = ldec_TipoCambioColones;
                                                LineaAsiento.Lstr_MonedaTiutlo = lstr_Moneda;
                                                Asiento.Add(LineaAsiento);
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

                            #region Define si el título es a largo plazo con afectación presupuestaria
                            else if (Convert.ToDecimal(lstr_Plazo) > diasAnnos)
                            {
                                lstr_Operacion = lstr_Moneda.Equals("USD") ? "ID44" : (lstr_Moneda.Equals("CRC") ? "ID42" : "ID42");

                                DataSet lds_Operaciones = loperacion.ConsultarOperaciones(lstr_Operacion, "IdModulo IN ('DI')", "");
                                if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
                                {
                                    lstr_NomOperacion = lds_Operaciones.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
                                }

                                ldat_Tira = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID").Tables[0].Clone();
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", "CAPITAL").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", lstr_PrimaDescuento).Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico.Trim() + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID", "CP", "AMORT").Tables[0].Rows)
                                    ldat_Tira.ImportRow(dr_tira);

                                foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                {
                                    string operacion = ldr_Row["CodigoAuxiliar2"].ToString();

                                    int index = ldat_Tira.Rows.IndexOf(ldr_Row);

                                    switch (operacion.Trim())
                                    {
                                        case "CAPITAL":
                                            {
                                                ldec_monto = percentaje*(lstr_Moneda.Equals("USD") ? ldec_ValorFacial : (lstr_Moneda.Equals("CRC") ? ldec_ValorFacial : ldec_ValorFacial * ldec_TipoCambioUDE));
                                                ldec_montoUde = percentaje*(lstr_Moneda.Equals("UDE") ? ldec_ValorFacial : 0);
                                                break;
                                            }
                                        case "INT_DEV_P_T":
                                        case "INT_DEV_P_PT":
                                        case "INT_DEV_P_NT":
                                            {
                                                ldec_monto = percentaje *( lstr_Moneda.Equals("USD") ? Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) : (lstr_Moneda.Equals("CRC") ? Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) : Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) * ldec_TipoCambioUDE));
                                                ldec_montoUde = percentaje * (lstr_Moneda.Equals("UDE") ? Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) : 0);
                                                break;
                                            }
                                        case "INT_DEV_D_T":
                                        case "INT_DEV_D_PT":
                                        case "INT_DEV_D_NT":
                                            {
                                                ldec_monto = percentaje*(lstr_Moneda.Equals("USD") ? Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) : (lstr_Moneda.Equals("CRC") ? Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) : Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) * ldec_TipoCambioUDE));
                                                ldec_montoUde = percentaje* (lstr_Moneda.Equals("UDE") ? Math.Abs(ldec_ValorFacial - ldec_ValorTransadoBruto) : 0);
                                                break;
                                            }
                                        case "AMORT":
                                            {
                                                ldec_monto = percentaje * (lstr_Moneda.Equals("USD") ? ldec_ValorFacial : (lstr_Moneda.Equals("CRC") ? ldec_ValorFacial : ldec_ValorFacial * ldec_TipoCambioUDE));
                                                ldec_montoUde = percentaje * (lstr_Moneda.Equals("UDE") ? ldec_ValorFacial : 0);
                                                break;
                                            }
                                    }

                                    lstr_Referencia = lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Cancelación";
                                    
                                    SinError = true;

                                    #region pospre debe
                                    //Validar que el pos pre sea diferente de PP_Balance y que el monto sea mayor al seleccionado
                                    string lstr_Monto = string.Empty;
                                    DataTable lds_Datos = new DataTable();
                                    decimal ldec_MontoTotal = 0;
                                    string reservasError = "";
                                    string lstr_NuevoPosPrePago = string.Empty;
                                    DataSet ldat_Reservas = new DataSet();

                                    ldat_Reservas = dinamica.ConsultarDinamico("select * from ma.reservasdetalle where left(idprograma,4)=year(getdate()) and idcuentacontable = '" + ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim() + "' and idpospre = '" + ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim() + "'");
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
                                                ldec_MontoTotal += Convert.ToDecimal(lstr_Monto.Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal));
                                            }
                                        }

                                        if (Convert.ToDecimal(ldec_MontoTotal) >= (ldec_monto * ldec_TipoCambio))
                                        {
                                            //Genera el asiento
                                            decimal ldec_SaldoCont = ldec_monto;
                                            decimal ldec_Saldo = (ldec_monto * ldec_TipoCambio);

                                            foreach (DataRow drForm in lds_Datos.Rows)
                                            {
                                                if (Convert.ToDecimal(drForm["Monto"]) > 0 && ldec_Saldo >= 0)
                                                {
                                                    decimal reservaTpoCambio = Convert.ToDecimal(drForm["Monto"].ToString()) / ldec_TipoCambio;

                                                    LineaAsiento = new RegistroContable();
                                                    LineaAsiento.Lstr_Referencia = lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia;
                                                    LineaAsiento.Lstr_Fecha = ldt_FchCancelacion.ToString("dd.MM.yyyy");
                                                    LineaAsiento.Lstr_Cuenta = ldr_Row["IdCuentaContable"].ToString().Trim();
                                                    LineaAsiento.Lstr_ClaveContable = ldr_Row["IdClaveContable"].ToString().Trim();
                                                    LineaAsiento.Lstr_Moneda = ldr_Row["CodigoAuxiliar"].ToString().Trim().Substring(0, 3);
                                                    LineaAsiento.Lstr_TextoInfo = lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim();
                                                    LineaAsiento.Lstr_CentroCosto = ldr_Row["IdCentroCosto"].ToString().Trim();
                                                    LineaAsiento.Lstr_CentroBeneficio = ldr_Row["IdCentroBeneficio"].ToString().Trim();
                                                    LineaAsiento.Lstr_ElementoPEP = ldr_Row["IdElementoPEP"].ToString().Trim();
                                                    LineaAsiento.Lstr_PosPre = ldr_Row["IdPosPre"].ToString().Trim().ToUpper();
                                                    LineaAsiento.Lstr_CentroGestor = ldr_Row["IdCentroGestor"].ToString().Trim();
                                                    LineaAsiento.Lstr_Fondo = ldr_Row["IdFondo"].ToString().Trim();
                                                    LineaAsiento.Lstr_DocPres = drForm["IdReserva"].ToString().Trim();
                                                    LineaAsiento.Lstr_PosDocPres = drForm["Posicion"].ToString().Trim();
                                                    LineaAsiento.Ldec_Monto = Truncate(ldec_SaldoCont > reservaTpoCambio ? reservaTpoCambio : ldec_SaldoCont, 2);
                                                    LineaAsiento.Ldec_TipoCambio = ldec_TipoCambioColones;
                                                    LineaAsiento.Lstr_MonedaTiutlo = lstr_Moneda;
                                                    Asiento.Add(LineaAsiento);

                                                    LineaDiferencial = new RegistroDiferencial();
                                                    LineaDiferencial.Lstr_MonedaTiutlo = lstr_Moneda;
                                                    LineaDiferencial.Lstr_Nemotecnico = lstr_Nemotecnico;
                                                    LineaDiferencial.Lstr_Plazo = "CP";
                                                    LineaDiferencial.Lstr_Propietario = lint_EsPublico;
                                                    LineaDiferencial.Lstr_DescripcionCuenta = operacion;
                                                    LineaDiferencial.Ldec_TpoCambioUDE = ldec_TipoCambioUDE;
                                                    LineaDiferencial.Ldec_MontoUDE = ldec_montoUde;
                                                    LineaDiferencial.Lstr_Cuenta = ldr_Row["IdCuentaContable"].ToString().Trim();
                                                    LineaDiferencial.Lstr_IdClaveContable = ldr_Row["IdClaveContable"].ToString().Trim();
                                                    Diferencial.Add(LineaDiferencial);

                                                }

                                                //Resta el saldo
                                                ldec_SaldoCont = ldec_SaldoCont - (Convert.ToDecimal(drForm["Monto"].ToString()) / ldec_TipoCambio);
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
                                            LineaAsiento = new RegistroContable();
                                            LineaAsiento.Lstr_Referencia = lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia;
                                            LineaAsiento.Lstr_Fecha = ldt_FchCancelacion.ToString("dd.MM.yyyy");
                                            LineaAsiento.Lstr_Cuenta = ldr_Row["IdCuentaContable"].ToString().Trim();
                                            LineaAsiento.Lstr_ClaveContable = ldr_Row["IdClaveContable"].ToString().Trim();
                                            LineaAsiento.Lstr_Moneda = ldr_Row["CodigoAuxiliar"].ToString().Trim().Substring(0, 3);
                                            LineaAsiento.Lstr_TextoInfo = lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim();
                                            LineaAsiento.Lstr_CentroCosto = ldr_Row["IdCentroCosto"].ToString().Trim();
                                            LineaAsiento.Lstr_CentroBeneficio = ldr_Row["IdCentroBeneficio"].ToString().Trim();
                                            LineaAsiento.Lstr_ElementoPEP = ldr_Row["IdElementoPEP"].ToString().Trim();
                                            LineaAsiento.Lstr_PosPre = ldr_Row["IdPosPre"].ToString().Trim().ToUpper();
                                            LineaAsiento.Lstr_CentroGestor = ldr_Row["IdCentroGestor"].ToString().Trim();
                                            LineaAsiento.Lstr_Fondo = ldr_Row["IdFondo"].ToString().Trim();
                                            LineaAsiento.Lstr_DocPres = ldr_Row["DocPresupuestario"].ToString().Trim();
                                            LineaAsiento.Lstr_PosDocPres = ldr_Row["PosDocPresupuestario"].ToString().Trim();
                                            LineaAsiento.Ldec_Monto = Truncate(ldec_monto, 2);
                                            LineaAsiento.Ldec_TipoCambio = ldec_TipoCambioColones;
                                            LineaAsiento.Lstr_MonedaTiutlo = lstr_Moneda;
                                            Asiento.Add(LineaAsiento);

                                            LineaDiferencial = new RegistroDiferencial();
                                            LineaDiferencial.Lstr_MonedaTiutlo = lstr_Moneda;
                                            LineaDiferencial.Lstr_Nemotecnico = lstr_Nemotecnico;
                                            LineaDiferencial.Lstr_Plazo = "CP";
                                            LineaDiferencial.Lstr_Propietario = lint_EsPublico;
                                            LineaDiferencial.Lstr_DescripcionCuenta = operacion;
                                            LineaDiferencial.Ldec_TpoCambioUDE = ldec_TipoCambioUDE;
                                            LineaDiferencial.Ldec_MontoUDE = ldec_montoUde;
                                            LineaDiferencial.Lstr_Cuenta = ldr_Row["IdCuentaContable"].ToString().Trim();
                                            LineaDiferencial.Lstr_IdClaveContable = ldr_Row["IdClaveContable"].ToString().Trim();
                                            Diferencial.Add(LineaDiferencial);
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
                                        ldat_Reservas = dinamica.ConsultarDinamico("select * from ma.reservasdetalle where left(idprograma,4)=year(getdate()) and idcuentacontable = '" + ldat_Tira.Rows[index]["IdCuentaContable2"].ToString().Trim() + "' and idpospre = '" + ldat_Tira.Rows[index]["IdPosPre2"].ToString().Trim() + "'");
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
                                                    reservasError += "Posición Presupuestaria: " + drForm["IdPosPre"].ToString().Trim() + "Reserva :" + drForm["IdReserva"].ToString().Trim() + ", Posición: " + drForm["Posicion"].ToString().Trim() + "\n";
                                                    ldec_MontoTotal += Convert.ToDecimal(lstr_Monto.Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal));
                                                }
                                            }

                                            if (Convert.ToDecimal(ldec_MontoTotal) >= (ldec_monto * ldec_TipoCambio))
                                            {
                                                //Genera el asiento
                                                decimal ldec_SaldoCont = ldec_monto;
                                                decimal ldec_Saldo = (ldec_monto * ldec_TipoCambio);

                                                foreach (DataRow drForm in lds_Datos.Rows)
                                                {
                                                    if (Convert.ToDecimal(drForm["Monto"]) > 0 && ldec_Saldo >= 0)
                                                    {
                                                        decimal reservaTpoCambio = Convert.ToDecimal(drForm["Monto"].ToString()) / ldec_TipoCambio;

                                                        LineaAsiento = new RegistroContable();
                                                        LineaAsiento.Lstr_Referencia = lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia;
                                                        LineaAsiento.Lstr_Fecha = ldt_FchCancelacion.ToString("dd.MM.yyyy");
                                                        LineaAsiento.Lstr_Cuenta = ldr_Row["IdCuentaContable2"].ToString().Trim();
                                                        LineaAsiento.Lstr_ClaveContable = ldr_Row["IdClaveContable2"].ToString().Trim();
                                                        LineaAsiento.Lstr_Moneda = ldr_Row["CodigoAuxiliar"].ToString().Trim().Substring(0, 3);
                                                        LineaAsiento.Lstr_TextoInfo = lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim();
                                                        LineaAsiento.Lstr_CentroCosto = ldr_Row["IdCentroCosto2"].ToString().Trim();
                                                        LineaAsiento.Lstr_CentroBeneficio = ldr_Row["IdCentroBeneficio2"].ToString().Trim();
                                                        LineaAsiento.Lstr_ElementoPEP = ldr_Row["IdElementoPEP2"].ToString().Trim();
                                                        LineaAsiento.Lstr_PosPre = ldr_Row["IdPosPre2"].ToString().Trim();
                                                        LineaAsiento.Lstr_CentroGestor = ldr_Row["IdCentroGestor2"].ToString().Trim();
                                                        LineaAsiento.Lstr_Fondo = ldr_Row["IdFondo2"].ToString().Trim();
                                                        LineaAsiento.Lstr_DocPres = drForm["IdReserva"].ToString().Trim();
                                                        LineaAsiento.Lstr_PosDocPres = drForm["Posicion"].ToString().Trim();
                                                        LineaAsiento.Ldec_Monto = Truncate(ldec_SaldoCont > reservaTpoCambio ? reservaTpoCambio : ldec_SaldoCont, 2);
                                                        LineaAsiento.Ldec_TipoCambio = ldec_TipoCambioColones;
                                                        LineaAsiento.Lstr_MonedaTiutlo = lstr_Moneda;
                                                        Asiento.Add(LineaAsiento);
                                                    }

                                                    //Resta el saldo
                                                    ldec_SaldoCont = ldec_SaldoCont - (Convert.ToDecimal(drForm["Monto"].ToString()) / ldec_TipoCambio);
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
                                                LineaAsiento = new RegistroContable();
                                                LineaAsiento.Lstr_Referencia = lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia;
                                                LineaAsiento.Lstr_Fecha = ldt_FchCancelacion.ToString("dd.MM.yyyy");
                                                LineaAsiento.Lstr_Cuenta = ldr_Row["IdCuentaContable2"].ToString().Trim();
                                                LineaAsiento.Lstr_ClaveContable = ldr_Row["IdClaveContable2"].ToString().Trim();
                                                LineaAsiento.Lstr_Moneda = ldr_Row["CodigoAuxiliar"].ToString().Trim().Substring(0, 3);
                                                LineaAsiento.Lstr_TextoInfo = lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim();
                                                LineaAsiento.Lstr_CentroCosto = ldr_Row["IdCentroCosto2"].ToString().Trim();
                                                LineaAsiento.Lstr_CentroBeneficio = ldr_Row["IdCentroBeneficio2"].ToString().Trim();
                                                LineaAsiento.Lstr_ElementoPEP = ldr_Row["IdElementoPEP2"].ToString().Trim();
                                                LineaAsiento.Lstr_PosPre = ldr_Row["IdPosPre2"].ToString().Trim();
                                                LineaAsiento.Lstr_CentroGestor = ldr_Row["IdCentroGestor2"].ToString().Trim();
                                                LineaAsiento.Lstr_Fondo = ldr_Row["IdFondo2"].ToString().Trim();
                                                LineaAsiento.Lstr_DocPres = ldr_Row["DocPresupuestario2"].ToString().Trim();
                                                LineaAsiento.Lstr_PosDocPres = ldr_Row["PosDocPresupuestario2"].ToString().Trim();
                                                LineaAsiento.Ldec_Monto = Truncate(ldec_monto, 2);
                                                LineaAsiento.Ldec_TipoCambio = ldec_TipoCambioColones;
                                                LineaAsiento.Lstr_MonedaTiutlo = lstr_Moneda;
                                                Asiento.Add(LineaAsiento);
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
                            break;
                        }
                    #endregion
                }
                if (SinError)
                {
                    lstr_Mensaje = GenerarAsientoAjuste("Cancelacion Título", Asiento, Diferencial, lstr_Operacion, lstr_NomOperacion, lstr_NroValor, lstr_Nemotecnico, ldec_TipoCambioColones, ldt_FchModifica);
                }
            //}
            return lstr_Mensaje;
        }

        public static string GenerarAsientoAjuste( string lstr_TipoCancelacion, List<RegistroContable> Asiento, List<RegistroDiferencial> Diferencial, string lstr_IdOperacion, string lstr_NomOperacion, string lstr_NroValor, string lstr_Nemotecnico, decimal ldec_TipoCambio, DateTime ldt_FchModifica )
        {
            Type elementType = typeof(RegistroContable);
            DataSet ds = new DataSet();
            DataTable t = new DataTable();
            ds.Tables.Add(t);

            //add a column to table for each public property on T
            foreach (var propInfo in elementType.GetProperties())
            {
                t.Columns.Add(propInfo.Name, propInfo.PropertyType);
            }

            //go through each property on T and add each value to the table .... usada en pruebas
            //foreach (RegistroContable item in Asiento)
            //{
            //    DataRow row = t.NewRow();
            //    foreach (var propInfo in elementType.GetProperties())
            //    {
            //        row[propInfo.Name] = propInfo.GetValue(item, null);
            //    }
            //    t.Rows.Add(row);
            //}
            //ImprimeAsientos(t);


            //Coleccion de asientos y tipos de asientos requeridos en SAP expuestos por la referencia del servicio
            wrSigafAsientos.ZfiAsiento item_asiento = new wrSigafAsientos.ZfiAsiento();
            wrSigafAsientos.ZfiAsiento[] tabla_asientos = new wrSigafAsientos.ZfiAsiento[Asiento.Count];

            //variables de proceso
            string lstr_codAsiento = string.Empty;
            string[] item_resAsientosLog = new string[10];
            DateTime fechaContabilizacion = System.DateTime.Today;
            string logAsiento = string.Empty;
            string flagEstadoAsiento = string.Empty;

            try
            {
                foreach (RegistroContable lrc_Linea in Asiento)
                {
                    item_asiento = new wrSigafAsientos.ZfiAsiento();
                    int index = Asiento.IndexOf(lrc_Linea);

                    if (index == 0)
                    {
                        item_asiento.Blart = "ID";//Clase de documento
                        item_asiento.Bukrs = "G206";//Sociedad
                        item_asiento.Bldat = lrc_Linea.Lstr_Fecha;//Fecha de documento
                        item_asiento.Budat = lrc_Linea.Lstr_Fecha;//Fecha de contabilización
                        item_asiento.Bktxt = lrc_Linea.Lstr_Referencia;//Referencia
                    }

                    item_asiento.Waers = lrc_Linea.Lstr_Moneda;//Moneda 
                    item_asiento.Bschl = lrc_Linea.Lstr_ClaveContable;//Clave de contabilización
                    item_asiento.Hkont = lrc_Linea.Lstr_Cuenta;//Cuenta de mayor
                    item_asiento.Wrbtr = Convert.ToDecimal(lrc_Linea.Ldec_Monto.ToString("0.0000"));//Importe
                    item_asiento.Sgtxt = lrc_Linea.Lstr_TextoInfo;//Texto Informativo (50 caracteres)
                    item_asiento.Kostl = lrc_Linea.Lstr_CentroCosto;//Centro de Costo
                    item_asiento.Prctr = lrc_Linea.Lstr_CentroBeneficio;//Centro de Beneficio
                    item_asiento.Projk = lrc_Linea.Lstr_ElementoPEP;//Elemento PEP
                    item_asiento.Fipex = lrc_Linea.Lstr_PosPre.ToUpper();//Posición Presupuestaria
                    item_asiento.Fistl = lrc_Linea.Lstr_CentroGestor;//Centro Gestor
                    item_asiento.Geber = lrc_Linea.Lstr_Fondo;//Fondo
                    item_asiento.Kblnr = lrc_Linea.Lstr_DocPres;//Documento Presupuestario
                    item_asiento.Kblpos = lrc_Linea.Lstr_PosDocPres;//Posición de documento presupuestario

                    item_asiento.Xblnr = lstr_NroValor+"."+lstr_Nemotecnico;//ldat_Asiento.Rows[index]["PKMovimiento"].ToString();//
                    item_asiento.Bktxt = "";//ldat_Asiento.Rows[index]["Texto2"].ToString();//
                    item_asiento.Xref1Hd = lstr_TipoCancelacion;//ldat_Asiento.Rows[index]["Ref1Tipo"].ToString();//
                    item_asiento.Xref2Hd = lstr_IdOperacion+"."+lstr_NomOperacion;//ldat_Asiento.Rows[index]["Ref2Operacion"].ToString();//
                    if (lrc_Linea.Lstr_Moneda == "USD")
                        item_asiento.Kursf = Convert.ToDecimal(lrc_Linea.Ldec_TipoCambio.ToString("0.0000"));

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
                    String QUERY = String.Format("UPDATE cf.TitulosValores SET asiento = '{0}', FchContablilizado = '{1}', Estado = 'CAN' WHERE nrovalor = {2} AND nemotecnico = '{3}'", lstr_codAsiento, fecha_con_formato_sql, lstr_NroValor.Trim(), lstr_Nemotecnico.Trim());
                    dinamica.ConsultarDinamico(QUERY);

                    /*
                    if (Asiento[0].Lstr_MonedaTiutlo.Equals("UDE"))
                    {
                        string lstr_codAsiento = logAsiento.Substring(logAsiento.IndexOf("BKPFF") + 6, 18);

                        foreach (RegistroDiferencial ldr_Diferencial in Diferencial)
                        {

                            string query = "INSERT INTO [cf].[TitulosValores] " +
                            "([FchAccion],[Nemotecnico],[Plazo],[Propietario],[CuentaAfectada], " +
                            "[DescripcionCuenta],[TpoCambioUDE],[MontoUDE],[IdClaveContable],[DesTipoOperacion],[UsrCreacion],[FchCreacion]) VALUES (" +
                            "'" + Convert.ToDateTime(Asiento[0].Lstr_Fecha).ToString("yyyy-MM-dd") + "', " +
                            "'" + ldr_Diferencial.Lstr_Nemotecnico + "', " +
                            "'" + ldr_Diferencial.Lstr_Plazo + "', " +
                            "'" + ldr_Diferencial.Lstr_Propietario + "', " +
                            "'" + ldr_Diferencial.Lstr_Cuenta + "', " +
                            "'" + ldr_Diferencial.Lstr_DescripcionCuenta.Substring(0, 7) + "', " +
                            ldr_Diferencial.Ldec_TpoCambioUDE + ", " +
                            ldr_Diferencial.Ldec_MontoUDE + ", " +
                            "'" + ldr_Diferencial.Lstr_IdClaveContable + "', " +
                            "'Cancelación', " +
                            "'123', " +
                            "'" + Convert.ToDateTime(Asiento[0].Lstr_Fecha).ToString("yyyy-MM-dd") + "')";
                            dinamica.ConsultarDinamico(query);
                        }
                    }

                    lcls_CostoTransaccion.ContabilizarCalculosFinancieros(
                        "TitulosValores",
                        null,
                        lstr_NroValor.Trim(),
                        lstr_Nemotecnico.Trim(),
                        "CAN",
                        "SG",
                        ldt_FchModifica, out a[0], out a[1]);
                     * */
                }
                return logAsiento;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public decimal get_total_percentace(string emision)
        {
            decimal total_percentaje = 0;
            Mantenimiento.clsDinamico dinamico = new Mantenimiento.clsDinamico();
            try
            {

                //DataTable all_percertajes = dinamico.ConsultarDinamico(string.Format("select  PorcEmision/100 as porcentaje FROM Cf.CanjeResumenSerie where nroemisionserie = '{0}' AND fchcanje = (select max(fchcanje) from Cf.CanjeResumenSerie a where a.nroemisionserie =c.nroemisionserie)", emision)).Tables[0];
                DataTable Titulo = dinamico.ConsultarDinamico("select top 1 Nemotecnico, NroValor FROM cf.TitulosValores WHERE NroEmisionSerie ='"+emision+"'").Tables[0];
                DataTable porcentajes = dinamico.ConsultarDinamico("exec cf.uspGetPorcentajeEmision '" + Titulo.Rows[0][0].ToString() + "', " + Titulo.Rows[0][1].ToString() + "").Tables[0];
                foreach (DataRow percentaje_row in porcentajes.Rows)
                {
                    decimal percentaje = decimal.Parse(percentaje_row[0].ToString());
                    total_percentaje = percentaje;
                }
                total_percentaje = 1 - total_percentaje;
            }
            catch 
            {
                throw;
            }
            return total_percentaje;
        }


        public static void ImprimeAsientos(DataTable tbl)
        {

            string refe = tbl.Rows[0]["Lstr_Referencia"].ToString().Split(' ')[0];
            string consulta = "";
            foreach (DataRow fila in tbl.Rows)
            {
                consulta = "insert into [dbo].[AsientosPrueba] values ( 'Cancela',convert(date,'" + tbl.Rows[0]["Lstr_Fecha"] + "',103),'" + refe.Split('-')[1] + "'," + refe.Split('-')[0] + ", '" +
                    fila["Lstr_Cuenta"].ToString() + "'," + fila["Lstr_ClaveContable"].ToString() + "," + fila["Ldec_Monto"].ToString() + ",'" + fila["Lstr_PosPre"].ToString() + "','" + fila["Lstr_CentroGestor"].ToString() + "')";

                dinamica.ConsultarDinamico(consulta);
            }
        }


    }
}