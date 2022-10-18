﻿using LogicaNegocio.Seguridad;
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
    public class clsContabilizarColocaciones
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
        private static Mantenimiento.clsTiposAsiento tiposAsiento = new Mantenimiento.clsTiposAsiento();
        private string resAsientosLog = string.Empty;
        private static string lstr_separador_decimal = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;

        private tBitacora reg_Bitacora = new tBitacora();
        public clsContabilizarColocaciones()
        {
            CultureInfo culture;
            culture = CultureInfo.CreateSpecificCulture("es-CR");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }

        private decimal Truncate(decimal value, int length)
        {
            return Math.Truncate(value * 100) / 100;
        }

        public string Colocacion(DateTime? ldt_FchInicio, DateTime? ldt_FchFin, Int32? lint_NroValor = -1, string lstr_Nemotecnico = "",bool lbool_manual=false)
        {
            DateTime? _fchInicio = ldt_FchInicio == null ? DateTime.Today : ldt_FchInicio;
            DateTime? _fchFin = ldt_FchFin == null ? DateTime.Today : ldt_FchFin;

            string lstr_Mensaje = "00-Proceso Finalizado, Verifique la bitácora";
            string lstr_Mensaje2 = string.Empty;
            Boolean lbol_Procesar = true;
            try
            {//nrovalor = 3719 and nemotecnico = 'PT' and 
                string lstr_estadovalor = lbool_manual ? "and (estadovalor = 'Cancelada' or estadovalor = 'Vigente')" : "and estadovalor = 'Vigente'";
                string lstr_Query = "select * from cf.titulosvalores where (NroValor = " + lint_NroValor + " or isnull(" + lint_NroValor + ",-1)=-1) and (nemotecnico = '" + lstr_Nemotecnico + "' OR ISNULL('" + lstr_Nemotecnico + "','')='') " + lstr_estadovalor + " and estado != 'C' and tiponegociacion != 'Compra' and indicadorcupon = 'V' and fchvalor between '" + _fchInicio.Value.ToString("yyyy-MM-dd") + "' and '" + _fchFin.Value.ToString("yyyy-MM-dd") + "'";
                DataTable ldat_TitulosValores = dinamica.ConsultarDinamico(lstr_Query).Tables[0];//titulo.ConsultarTituloValor(null, String.Empty, String.Empty, String.Empty, "V", String.Empty, String.Empty, String.Empty, Convert.ToDateTime(ldt_FchInicio), Convert.ToDateTime(ldt_FchFin)).Tables[0];

                //DataTable ldat_tit = dinamica.ConsultarDinamico(lstr_Query).


                DataTable ldat_Nemotecnicos = nemotecnico.ConsultarNemotecnicos(null, null, null, null, null).Tables[0];

                for (int i = 0; i < ldat_TitulosValores.Rows.Count; i++)
                {
                    try
                    {
                        string lstr_moneda = string.Empty;
                        lbol_Procesar = true;
                        try
                        {
                            lstr_moneda = ldat_Nemotecnicos.Select("IdNemotecnico = '" + ldat_TitulosValores.Rows[i]["NemoTecnico"].ToString() + "'")[0]["IdMoneda"].ToString().Trim().Equals("CRCN")
                               ? "CRC" : ldat_Nemotecnicos.Select("IdNemotecnico = '" + ldat_TitulosValores.Rows[i]["NemoTecnico"].ToString() + "'")[0]["IdMoneda"].ToString().Trim();
                        }
                        catch
                        {
                            lstr_moneda = string.Empty;
                            lstr_Mensaje2 += "Error, Nemotecnico no existe o error al determinar moneda " + ldat_TitulosValores.Rows[i]["NemoTecnico"].ToString();
                            //Registrar en Bitacora de movimientos
                            bitacora.ufnRegistrarAccionBitacora("DI", "123", "Colocación Titulo", "Error, Nemotecnico no existe o error al determinar moneda " + ldat_TitulosValores.Rows[i]["NemoTecnico"].ToString(), "", ldat_TitulosValores.Rows[i]["NroValor"].ToString() + "-" + lstr_Nemotecnico, "");
                            lbol_Procesar = false;
                        }
                        if (lbol_Procesar//ldat_Nemotecnicos.Select("IdNemotecnico = '" + ldat_TitulosValores.Rows[i]["NemoTecnico"].ToString() + "'")[0]["Estado"].ToString().Trim() == "A"
                            && lstr_moneda == (ldat_TitulosValores.Rows[i]["Moneda"].ToString().Equals("CRCN") ? "CRC" : ldat_TitulosValores.Rows[i]["Moneda"].ToString()))
                        {
                           
                                lstr_Mensaje2 += ColocaTituloValor(
                                     ldat_TitulosValores.Rows[i]["Tipo"].ToString(),
                                     ldat_TitulosValores.Rows[i]["EstadoValor"].ToString(),
                                     Convert.ToDateTime(ldat_TitulosValores.Rows[i]["FchValor"].ToString()),
                                     Convert.ToDateTime(ldat_TitulosValores.Rows[i]["FchVencimiento"].ToString()),
                                     ldat_TitulosValores.Rows[i]["Propietario"].ToString(),
                                     ldat_TitulosValores.Rows[i]["PlazoValor"].ToString(),

                                     Convert.ToDecimal(ldat_TitulosValores.Rows[i]["ValorTransadoBruto"].ToString()),
                                     ldat_TitulosValores.Rows[i]["Moneda"].ToString().Equals("CRCN") ? "CRC" : ldat_TitulosValores.Rows[i]["Moneda"].ToString(),

                                     ldat_TitulosValores.Rows[i]["NroValor"].ToString(),
                                     ldat_TitulosValores.Rows[i]["NemoTecnico"].ToString(),
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

                                     Convert.ToDateTime(ldat_TitulosValores.Rows[i]["FchModifica"].ToString()),
                                     lbool_manual);
                            
                        }
                    }
                    catch(Exception e1)
                    {
                        lstr_Mensaje2 += e1.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                lstr_Mensaje = "99 - "+ ex.ToString();
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

        public string ColocaTituloValor(
            string lstr_Tipo,
            string lstr_EstadoValor,
            DateTime ldt_FchValor,
            DateTime ldt_FchVencimiento,
            string lstr_Propietario,

            string lstr_PlazoNemo,

            decimal ldec_ValorTransadoBruto,
            string lstr_Moneda,
            string lstr_NroValor,
            string lstr_Nemotecnico,
            decimal ldec_ValorFacial,
            decimal ldec_RendimientoXDescuento,
            decimal ldec_ImpuestoPagado,
            decimal ldec_ValorTransadoNeto,
            decimal ldec_Premio,
            string lstr_Detalle,
            string lstr_Origen,
            DateTime ldt_FchModifica,
            bool lbool_manual)
        {
            string lstr_Mensaje = string.Empty;
            wsDeudaInterna.wsDeudaInterna wsDeudaInterna = new wsDeudaInterna.wsDeudaInterna();
            DataTable ldat_Asiento = RegistroContable();
            DataTable ldat_Tira = new DataTable();
            string lint_EsPublico = "PUBLICO";
            string lstr_Plazo = lstr_PlazoNemo.Replace(".",lstr_separador_decimal).Replace(",",lstr_separador_decimal);
            
            string lstr_TipoPlazo = "CP";
            string lstr_Operacion = string.Empty;
            string lstr_NomOperacion = string.Empty;
            decimal ldec_TipoCambioColones = Convert.ToDecimal(tipocambio.ConsultarTiposCambio("CRCN", ldt_FchValor, "3140", "N").Tables[0].Rows[0]["Valor"].ToString());
            decimal ldec_TipoCambioUDE = 0;//Convert.ToDecimal(tipocambio.ConsultarTiposCambio("UDE", ldt_FchValor, "", "N").Tables[0].Rows[0]["Valor"].ToString());
            decimal ldec_monto = 0;
            decimal ldec_MontoPrincipal = 0; 
            decimal ldec_DescuentoPrima = 0;
            string lstr_Referencia = "";
            bool lbol_EsPrima = true;
            decimal diasAnnos = 1;

            if (propietario.ConsultarPropietarios(string.Empty, string.Empty, string.Empty, lstr_Propietario, "S").Tables[0].Rows.Count == 0)
            {
                lint_EsPublico = "PRIVADO";
            }

            //
            if (lstr_Origen == "Rdi")
                diasAnnos = 360;

            //
            if (ldec_ValorFacial >= ldec_ValorTransadoBruto)
                lbol_EsPrima = false;

            //se tratan las colocaciones de las tres diferentes operaciones, títulos cero cupón, de tasa fija y tasa variable.
            if (lstr_EstadoValor == "Vigente" || lbool_manual)
            {
                switch (lstr_Tipo.ToLower())
                {
                    #region cero cupon
                    case "cero cupón":
                        {
                            #region Define si el título es a corto plazo y no trasciende en el periodo
                            if ((Convert.ToDecimal(lstr_Plazo) <= 360) &&
                                //(lstr_Nemotecnico != "PT") &&
                                (ldt_FchValor.Year == ldt_FchVencimiento.Year))
                            {
                                lstr_TipoPlazo = "CP";
                                ///lstr_Operacion = lstr_Moneda.Equals("CRC") ? "ID08" : "ID10";
                                lstr_Operacion = lstr_Moneda.Equals("USD") ? "ID10" : (lstr_Moneda.Equals("CRC") ? "ID08" : "ID08");

                                DataSet lds_Operaciones = loperacion.ConsultarOperaciones(lstr_Operacion, "IdModulo IN ('DI')", "");
                                if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
                                {
                                    lstr_NomOperacion = lds_Operaciones.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
                                }

                                

                                string lstr_NemotecnicoTemp = lstr_Nemotecnico.Equals("TPTBP") ? "TPTBP" : "";
                                string tira_deuda_politica = lstr_Nemotecnico.Equals("TPTBP") ? ",'DEUDA POLITICA'" : "";
                                string tira_primas = lbol_EsPrima ? ",'PRIMAS'" : ",'IMP_DEV'";
                                //DataSet lds_Tira = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", lstr_Moneda, "", lstr_Nemotecnico, "ID", "CP", lint_EsPublico);
                                //DataSet lds_Tira = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID");

                                string lstr_SQL = "select ta.*, SUBSTRING ( IdCuentaContable ,4 , 1 ) AS EsNemotecnico, SUBSTRING ( IdCuentaContable ,10 , 1 ) AS EsPubPriv, SUBSTRING ( IdCuentaContable ,2 , 1 ) AS EsLargoCortoPlazo from ma.TiposAsiento ta where IdModulo = 'DI' and  IdOperacion = '" + lstr_Operacion + "' and Codigo = '" + "G206" + "' AND CodigoAuxiliar = '" + lstr_Moneda + "' AND (CodigoAuxiliar2 IN ('FONDO','CAPITAL','RENTA'" + tira_primas + tira_deuda_politica + ") or isnull(CodigoAuxiliar2,'')='' ) AND (CodigoAuxiliar3 = '" + lstr_Nemotecnico + "' OR ISNULL(CodigoAuxiliar3,'') = '')  AND (CodigoAuxiliar4 = 'ID') AND (CodigoAuxiliar5 = '" + lstr_TipoPlazo + "' OR ISNULL(CodigoAuxiliar5,'')='') AND (CodigoAuxiliar6 = '" + lint_EsPublico + "' OR ISNULL(CodigoAuxiliar6,'') = '')";
                                DataSet lds_Tira = dinamica.ConsultarDinamico(lstr_SQL);

                                ldat_Tira = lds_Tira.Tables[0];//.Clone();
                                /*DataSet lds_Tira1 = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", lstr_Moneda, "FONDO", lstr_NemotecnicoTemp, "ID", "", "");

                                DataSet lds_Tira2 = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", lstr_Moneda, "CAPITAL", lstr_Nemotecnico, "ID", "CP", lint_EsPublico);

                                DataSet lds_Tira3 = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", lstr_Moneda, "IMP_DEV", lstr_Nemotecnico, "ID", "CP", lint_EsPublico);

                                DataSet lds_Tira4 = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", lstr_Moneda, "RENTA", "", "ID", "CP", lint_EsPublico);
                                try
                                {
                                    foreach (DataRow dr_tira in lds_Tira1.Tables[0].Rows)//.Select("CodigoAuxiliar2 = '" + lstr_NemotecnicoTemp + "'").CopyToDataTable()
                                        ldat_Tira.ImportRow(dr_tira);
                                }
                                catch
                                {

                                }
                                try {

                                    foreach (DataRow dr_tira in lds_Tira2.Tables[0].Rows)//.Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico.Trim() + "'").CopyToDataTable()
                                    ldat_Tira.ImportRow(dr_tira);
                                }
                                catch
                                {

                                }
                                try
                                {
                                    foreach (DataRow dr_tira in lds_Tira3.Tables[0].Rows) //.Select("CodigoAuxiliar2 = '" + lstr_Nemotecnico.Trim() + "'").CopyToDataTable()
                                        ldat_Tira.ImportRow(dr_tira);
                                }
                                catch
                                {

                                }
                                    //foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", "INT_DEV").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico.Trim() + "'").CopyToDataTable().Rows)
                                //    ldat_Tira.ImportRow(dr_tira);
                                //foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", "PRIMAS").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico.Trim() + "'").CopyToDataTable().Rows)
                                //    ldat_Tira.ImportRow(dr_tira);
                                try
                                {


                                    foreach (DataRow dr_tira in lds_Tira4.Tables[0].Rows)
                                        ldat_Tira.ImportRow(dr_tira);
                                }
                                catch
                                {

                                }*/
                                foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                {
                                    string operacion = ldr_Row["CodigoAuxiliar2"].ToString().Trim();

                                    int index = ldat_Tira.Rows.IndexOf(ldr_Row);

                                    switch (operacion.Trim())
                                    {
                                        case "DEUDA POLITICA":
                                        case "":
                                        case "FONDO":
                                            {
                                                switch (lstr_Origen.Trim())
                                                {
                                                    case "Rdd": { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorTransadoBruto : (lstr_Moneda.Equals("CRC") ? ldec_ValorTransadoBruto : ldec_ValorTransadoBruto * ldec_TipoCambioUDE); break; }
                                                    default: { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorTransadoNeto : (lstr_Moneda.Equals("CRC") ? ldec_ValorTransadoNeto : ldec_ValorTransadoNeto * ldec_TipoCambioUDE); break; }

                                                }
                                                break;
                                            }
                                        case "CAPITAL": { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorFacial : (lstr_Moneda.Equals("CRC") ? ldec_ValorFacial : ldec_ValorFacial * ldec_TipoCambioUDE); break; }
                                        case "IMP_DEV": { ldec_monto = lstr_Moneda.Equals("USD") ? (ldec_ValorFacial - ldec_ValorTransadoBruto) : (lstr_Moneda.Equals("CRC") ? (ldec_ValorFacial - ldec_ValorTransadoBruto) : (ldec_ValorFacial - ldec_ValorTransadoBruto) * ldec_TipoCambioUDE); break; }
                                        case "RENTA": { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ImpuestoPagado : (lstr_Moneda.Equals("CRC") ? ldec_ImpuestoPagado : ldec_ImpuestoPagado * ldec_TipoCambioUDE); break; }                                        
                                    }

                                    //switch (index)
                                    //{
                                    //    case 0: { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorTransadoNeto : (lstr_Moneda.Equals("CRC") ? ldec_ValorTransadoNeto : ldec_ValorTransadoNeto * ldec_TipoCambioUDE); break; }
                                    //    case 1: { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorFacial : (lstr_Moneda.Equals("CRC") ? ldec_ValorFacial : ldec_ValorFacial * ldec_TipoCambioUDE); break; }
                                    //    case 2: { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_RendimientoXDescuento : (lstr_Moneda.Equals("CRC") ? ldec_RendimientoXDescuento : ldec_RendimientoXDescuento * ldec_TipoCambioUDE); break; }
                                    //    case 3: { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ImpuestoPagado : (lstr_Moneda.Equals("CRC") ? ldec_ImpuestoPagado : ldec_ImpuestoPagado * ldec_TipoCambioUDE); break; }
                                    //}

                                    DataSet ldat_Reservas = new DataSet();
                                    string lstr_NuevoPosPreDevengo = string.Empty;
                                    //if (ldat_Tira.Rows[0]["IdPosPre"].ToString().Trim().Equals("PP_Balance"))
                                    //{
                                        ldat_Reservas = reservas.ConsultarReservasDetallado(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(), string.Empty, ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(), string.Empty, string.Empty, string.Empty, string.Empty);
                                        //ldat_Reservas = reservas.ConsultarReservasDetallado(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                                        DataView dv = ldat_Reservas.Tables[0].DefaultView;
                                        dv.Sort = "OrdenDeudaInterna ASC";

                                        foreach (DataRow drForm in dv.ToTable().Rows)
                                        {
                                            if (!drForm["OrdenDeudaInterna"].ToString().Equals(string.Empty))
                                                //ldat_AsientoDevengo
                                                if (Convert.ToDecimal(drForm["Monto"]) > 0 && !drForm["OrdenDeudaInterna"].ToString().Equals("0")
                                                    && Convert.ToDecimal(drForm["Monto"]) >= ldec_monto)
                                                {
                                                    lstr_NuevoPosPreDevengo = drForm["IdPosPre"].ToString().Trim();
                                                    break;
                                                }
                                        }
                                    //}

                                        lstr_Referencia = "Colocación " + lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim();

                                    ldat_Asiento.Rows.Add(
                                        lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                        ldt_FchValor.ToString("dd.MM.yyyy"),
                                        ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                        lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim(),
                                        ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                        lstr_NuevoPosPreDevengo.Equals(string.Empty) ? ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim() : lstr_NuevoPosPreDevengo,
                                        ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                        Truncate(ldec_monto, 2),
                                        lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                        tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                        lstr_Moneda,//tipo
                                        lstr_Operacion +"."+lstr_NomOperacion //operacion
                                        );
                                }
                            }
                            #endregion

                            #region Define si el título es a corto plazo, pero trasciende en el periodo
                            else if ((Convert.ToDecimal(lstr_Plazo) <= 360) &&
                                //(lstr_Nemotecnico != "PT") &&
                                (ldt_FchValor.Year != ldt_FchVencimiento.Year))
                            {
                                lstr_TipoPlazo = "CP";
                                lstr_Operacion = lstr_Moneda.Equals("USD") ? "ID09" : (lstr_Moneda.Equals("CRC") ? "ID07" : "ID07");

                                DataSet lds_Operaciones = loperacion.ConsultarOperaciones(lstr_Operacion, "IdModulo IN ('DI')", "");
                                if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
                                {
                                    lstr_NomOperacion = lds_Operaciones.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
                                }


                                //lstr_Operacion = lstr_Moneda.Equals("CRC") ? "ID07" : "ID09";

                                //ldat_Tira = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", lstr_Moneda, lstr_Nemotecnico, "", "ID").Tables[0].Select("EsNemotecnico='5' AND EsPubPriv='" + lint_EsPublico + "'").CopyToDataTable();
                                string lstr_NemotecnicoTemp = lstr_Nemotecnico.Equals("TPTBP") ? "TPTBP" : "";
                                string tira_deuda_politica = lstr_Nemotecnico.Equals("TPTBP") ? ",'DEUDA POLITICA'" : "";
                                string tira_primas = lbol_EsPrima ? ",'PRIMAS'" : ",'IMP_DEV'";
                                string lstr_SQL = "select ta.*, SUBSTRING ( IdCuentaContable ,4 , 1 ) AS EsNemotecnico, SUBSTRING ( IdCuentaContable ,10 , 1 ) AS EsPubPriv, SUBSTRING ( IdCuentaContable ,2 , 1 ) AS EsLargoCortoPlazo from ma.TiposAsiento ta where IdModulo = 'DI' and  IdOperacion = '" + lstr_Operacion + "' and Codigo = '" + "G206" + "' AND CodigoAuxiliar = '" + lstr_Moneda + "' AND (CodigoAuxiliar2 IN ('FONDO','CAPITAL','RENTA'" + tira_primas + tira_deuda_politica + ") or isnull(CodigoAuxiliar2,'')='' ) AND (CodigoAuxiliar3 = '" + lstr_Nemotecnico + "' OR ISNULL(CodigoAuxiliar3,'') = '')  AND (CodigoAuxiliar4 = 'ID') AND (CodigoAuxiliar5 = '" + lstr_TipoPlazo + "' OR ISNULL(CodigoAuxiliar5,'')='') AND (CodigoAuxiliar6 = '" + lint_EsPublico + "' OR ISNULL(CodigoAuxiliar6,'') = '')";
                                DataSet lds_Tira = dinamica.ConsultarDinamico(lstr_SQL);

                                ldat_Tira = lds_Tira.Tables[0];//.Clone();

                                //ldat_Tira = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID").Tables[0].Clone();
                                /*try
                                {
                                    foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_NemotecnicoTemp, "", "ID", "", "FONDO").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_NemotecnicoTemp + "'").CopyToDataTable().Rows)
                                        ldat_Tira.ImportRow(dr_tira);
                                }
                                catch { }
                                try
                                {
                                    foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", "CAPITAL").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico.Trim() + "'").CopyToDataTable().Rows)
                                        ldat_Tira.ImportRow(dr_tira);
                                }
                                catch { }
                                try
                                {
                                    foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", "IMP_DEV").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico.Trim() + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                //foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", "INT_DEV").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico.Trim() + "'").CopyToDataTable().Rows)
                                //    ldat_Tira.ImportRow(dr_tira);
                                //foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", "PRIMAS").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico.Trim() + "'").CopyToDataTable().Rows)
                                //    ldat_Tira.ImportRow(dr_tira);
                                }
                                catch { }
                                try
                                {
                                    foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", lint_EsPublico, "ID", "CP", "RENTA").Tables[0].Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                }
                                catch { }
                                 */
                                foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                {
                                    string operacion = ldr_Row["CodigoAuxiliar2"].ToString();

                                    int index = ldat_Tira.Rows.IndexOf(ldr_Row);

                                    switch (operacion.Trim())
                                    {
                                        case "DEUDA POLITICA":
                                        case "":
                                        case "FONDO": //{ ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorTransadoNeto : (lstr_Moneda.Equals("CRC") ? ldec_ValorTransadoNeto : ldec_ValorTransadoNeto * ldec_TipoCambioUDE); break; }
                                            {
                                                switch (lstr_Origen.Trim())
                                                {
                                                    case "Rdd": { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorTransadoBruto : (lstr_Moneda.Equals("CRC") ? ldec_ValorTransadoBruto : ldec_ValorTransadoBruto * ldec_TipoCambioUDE); break; }
                                                    default: { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorTransadoNeto : (lstr_Moneda.Equals("CRC") ? ldec_ValorTransadoNeto : ldec_ValorTransadoNeto * ldec_TipoCambioUDE); break; }

                                                }
                                                break;
                                            }
                                        case "CAPITAL": { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorFacial : (lstr_Moneda.Equals("CRC") ? ldec_ValorFacial : ldec_ValorFacial * ldec_TipoCambioUDE); break; }
                                        case "IMP_DEV": { ldec_monto = lstr_Moneda.Equals("USD") ? (ldec_ValorFacial - ldec_ValorTransadoBruto) : (lstr_Moneda.Equals("CRC") ? (ldec_ValorFacial - ldec_ValorTransadoBruto) : (ldec_ValorFacial - ldec_ValorTransadoBruto) * ldec_TipoCambioUDE); break; }
                                        case "RENTA": { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ImpuestoPagado : (lstr_Moneda.Equals("CRC") ? ldec_ImpuestoPagado : ldec_ImpuestoPagado * ldec_TipoCambioUDE); break; }
                                    }

                                    DataSet ldat_Reservas = new DataSet();
                                    string lstr_NuevoPosPreDevengo = string.Empty;
                                    //if (ldat_Tira.Rows[0]["IdPosPre"].ToString().Trim().Equals("PP_Balance"))
                                    //{
                                        ldat_Reservas = reservas.ConsultarReservasDetallado(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(), string.Empty, ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(), string.Empty, string.Empty, string.Empty, string.Empty);
                                        //ldat_Reservas = reservas.ConsultarReservasDetallado(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty); ;
                                        DataView dv = ldat_Reservas.Tables[0].DefaultView;
                                        dv.Sort = "OrdenDeudaInterna ASC";

                                        foreach (DataRow drForm in dv.ToTable().Rows)
                                        {
                                            if (!drForm["OrdenDeudaInterna"].ToString().Equals(string.Empty))
                                                //ldat_AsientoDevengo
                                                if (Convert.ToDecimal(drForm["Monto"]) > 0 && !drForm["OrdenDeudaInterna"].ToString().Equals("0")
                                                    && Convert.ToDecimal(drForm["Monto"]) >= ldec_monto)
                                                {
                                                    lstr_NuevoPosPreDevengo = drForm["IdPosPre"].ToString().Trim();
                                                    break;
                                                }
                                        }
                                    //}
                                        lstr_Referencia = "Colocación " + lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim();

                                    ldat_Asiento.Rows.Add(
                                        lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia, ldt_FchValor.ToString("dd.MM.yyyy"),
                                        ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                        lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim(),
                                        ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                        lstr_NuevoPosPreDevengo.Equals(string.Empty) ? ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim() : lstr_NuevoPosPreDevengo,
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
                            }
                            #endregion

                            #region Define si el título es a largo plazo
                            else if ((Convert.ToDecimal(lstr_Plazo) > 360))// && (lstr_Nemotecnico != "PT"))
                            {
                                lstr_TipoPlazo = "LP";
                                lstr_Operacion = lstr_Moneda.Equals("USD") ? "ID09" : (lstr_Moneda.Equals("CRC") ? "ID07" : "ID07");

                                DataSet lds_Operaciones = loperacion.ConsultarOperaciones(lstr_Operacion, "IdModulo IN ('DI')", "");
                                if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
                                {
                                    lstr_NomOperacion = lds_Operaciones.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
                                }

                                //lstr_Operacion = lstr_Moneda.Equals("CRC") ? "ID07" : "ID09";

                                //ldat_Tira = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", lstr_Moneda, lstr_Nemotecnico, "", "ID").Tables[0].Select("EsNemotecnico='5' AND EsPubPriv='" + lint_EsPublico + "'").CopyToDataTable();
                                string lstr_NemotecnicoTemp = lstr_Nemotecnico.Equals("TPTBP") ? "TPTBP" : "";
                                string tira_deuda_politica = lstr_Nemotecnico.Equals("TPTBP") ? ",'DEUDA POLITICA'" : "";
                                string tira_primas = lbol_EsPrima ? ",'PRIMAS'" : ",'IMP_DEV'";
                                string lstr_SQL = "select ta.*, SUBSTRING ( IdCuentaContable ,4 , 1 ) AS EsNemotecnico, SUBSTRING ( IdCuentaContable ,10 , 1 ) AS EsPubPriv, SUBSTRING ( IdCuentaContable ,2 , 1 ) AS EsLargoCortoPlazo from ma.TiposAsiento ta where IdModulo = 'DI' and  IdOperacion = '" + lstr_Operacion + "' and Codigo = '" + "G206" + "' AND CodigoAuxiliar = '" + lstr_Moneda + "' AND (CodigoAuxiliar2 IN ('FONDO','CAPITAL','RENTA'" + tira_primas + tira_deuda_politica + ") or isnull(CodigoAuxiliar2,'')='' ) AND (CodigoAuxiliar3 = '" + lstr_Nemotecnico + "' OR ISNULL(CodigoAuxiliar3,'') = '')  AND (CodigoAuxiliar4 = 'ID') AND (CodigoAuxiliar5 = '" + lstr_TipoPlazo + "' OR ISNULL(CodigoAuxiliar5,'')='') AND (CodigoAuxiliar6 = '" + lint_EsPublico + "' OR ISNULL(CodigoAuxiliar6,'') = '')";
                                DataSet lds_Tira = dinamica.ConsultarDinamico(lstr_SQL);

                                ldat_Tira = lds_Tira.Tables[0];//.Clone();

                                //ldat_Tira = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID").Tables[0].Clone();
                                /*try{
                                    foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_NemotecnicoTemp, "", "ID", "", "FONDO").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_NemotecnicoTemp + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                }
                                catch { }
                                try{
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "LP", "CAPITAL").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico.Trim() + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                }
                                catch { }
                                try{
                                    foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "LP", "IMP_DEV").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico.Trim() + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                //foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "LP", "INT_DEV").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico.Trim() + "'").CopyToDataTable().Rows)
                                //    ldat_Tira.ImportRow(dr_tira);
                                //foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "LP", "PRIMAS").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico.Trim() + "'").CopyToDataTable().Rows)
                                //    ldat_Tira.ImportRow(dr_tira);
                                }
                                catch { }
                                try{
                                    foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", lint_EsPublico, "ID", "", "RENTA").Tables[0].Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                }
                                catch { }*/
                                foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                {
                                    string operacion = ldr_Row["CodigoAuxiliar2"].ToString();

                                    int index = ldat_Tira.Rows.IndexOf(ldr_Row);

                                    switch (operacion.Trim())
                                    {
                                        case "DEUDA POLITICA":
                                        case "":
                                        case "FONDO": //{ ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorTransadoNeto : (lstr_Moneda.Equals("CRC") ? ldec_ValorTransadoNeto : ldec_ValorTransadoNeto * ldec_TipoCambioUDE); break; }
                                            {
                                                switch (lstr_Origen.Trim())
                                                {
                                                    case "Rdd": { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorTransadoBruto : (lstr_Moneda.Equals("CRC") ? ldec_ValorTransadoBruto : ldec_ValorTransadoBruto * ldec_TipoCambioUDE); break; }
                                                    default: { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorTransadoNeto : (lstr_Moneda.Equals("CRC") ? ldec_ValorTransadoNeto : ldec_ValorTransadoNeto * ldec_TipoCambioUDE); break; }

                                                }
                                                break;
                                            }
                                        case "CAPITAL": { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorFacial : (lstr_Moneda.Equals("CRC") ? ldec_ValorFacial : ldec_ValorFacial * ldec_TipoCambioUDE); break; }
                                        case "IMP_DEV": { ldec_monto = lstr_Moneda.Equals("USD") ? (ldec_ValorFacial - ldec_ValorTransadoBruto) : (lstr_Moneda.Equals("CRC") ? (ldec_ValorFacial - ldec_ValorTransadoBruto) : (ldec_ValorFacial - ldec_ValorTransadoBruto) * ldec_TipoCambioUDE); break; }
                                        case "RENTA": { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ImpuestoPagado : (lstr_Moneda.Equals("CRC") ? ldec_ImpuestoPagado : ldec_ImpuestoPagado * ldec_TipoCambioUDE); break; }
                                    }

                                    DataSet ldat_Reservas = new DataSet();
                                    string lstr_NuevoPosPreDevengo = string.Empty;
                                    //if (ldat_Tira.Rows[0]["IdPosPre"].ToString().Trim().Equals("PP_Balance"))
                                    //{
                                    ldat_Reservas = reservas.ConsultarReservasDetallado(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(), string.Empty, ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(), string.Empty, string.Empty, string.Empty, string.Empty);
                                    //ldat_Reservas = reservas.ConsultarReservasDetallado(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty); ;
                                    DataView dv = ldat_Reservas.Tables[0].DefaultView;
                                    dv.Sort = "OrdenDeudaInterna ASC";

                                    foreach (DataRow drForm in dv.ToTable().Rows)
                                    {
                                        if (!drForm["OrdenDeudaInterna"].ToString().Equals(string.Empty))
                                            //ldat_AsientoDevengo
                                            if (Convert.ToDecimal(drForm["Monto"]) > 0 && !drForm["OrdenDeudaInterna"].ToString().Equals("0")
                                                && Convert.ToDecimal(drForm["Monto"]) >= ldec_monto)
                                            {
                                                lstr_NuevoPosPreDevengo = drForm["IdPosPre"].ToString().Trim();
                                                break;
                                            }
                                    }
                                    //}
                                    lstr_Referencia = "Colocación " + lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim();

                                    ldat_Asiento.Rows.Add(
                                        lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                        ldt_FchValor.ToString("dd.MM.yyyy"),
                                        ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                        lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim(),
                                        ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                        lstr_NuevoPosPreDevengo.Equals(string.Empty) ? ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim() : lstr_NuevoPosPreDevengo,
                                        ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                        Math.Round(ldec_monto, 2),
                                        lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                        tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                        lstr_Moneda,//tipo
                                        lstr_Operacion +"."+lstr_NomOperacion //operacion
                                        );
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
                            if ((Convert.ToDecimal(lstr_Plazo) <= diasAnnos) &&
                                (ldt_FchValor.Year == ldt_FchVencimiento.Year))
                            {
                                lstr_TipoPlazo = "CP";
                                lstr_Operacion = lstr_Moneda.Equals("USD") ? "ID10" : (lstr_Moneda.Equals("CRC") ? "ID08" : "ID08");

                                DataSet lds_Operaciones = loperacion.ConsultarOperaciones(lstr_Operacion, "IdModulo IN ('DI')", "");
                                if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
                                {
                                    lstr_NomOperacion = lds_Operaciones.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
                                }

                                string lstr_NemotecnicoTemp = lstr_Nemotecnico.Equals("TPTBP") ? "TPTBP" : "";
                                string tira_deuda_politica = lstr_Nemotecnico.Equals("TPTBP") ? ",'DEUDA POLITICA'" : "";
                                string tira_primas = lbol_EsPrima ? ",'PRIMAS'" : ",'IMP_DEV'";
                                string lstr_SQL = "select ta.*, SUBSTRING ( IdCuentaContable ,4 , 1 ) AS EsNemotecnico, SUBSTRING ( IdCuentaContable ,10 , 1 ) AS EsPubPriv, SUBSTRING ( IdCuentaContable ,2 , 1 ) AS EsLargoCortoPlazo from ma.TiposAsiento ta where IdModulo = 'DI' and  IdOperacion = '" + lstr_Operacion + "' and Codigo = '" + "G206" + "' AND CodigoAuxiliar = '" + lstr_Moneda + "' AND (CodigoAuxiliar2 IN ('FONDO','CAPITAL','RENTA'" + tira_primas + tira_deuda_politica + ") or isnull(CodigoAuxiliar2,'')='' ) AND (CodigoAuxiliar3 = '" + lstr_Nemotecnico + "' OR ISNULL(CodigoAuxiliar3,'') = '')  AND (CodigoAuxiliar4 = 'ID') AND (CodigoAuxiliar5 = '" + lstr_TipoPlazo + "' OR ISNULL(CodigoAuxiliar5,'')='') AND (CodigoAuxiliar6 = '" + lint_EsPublico + "' OR ISNULL(CodigoAuxiliar6,'') = '')";
                                DataSet lds_Tira = dinamica.ConsultarDinamico(lstr_SQL);

                                ldat_Tira = lds_Tira.Tables[0];//.Clone();

                                //ldat_Tira = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID").Tables[0].Clone();
                                
                                /*try{
                                    foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_NemotecnicoTemp, "", "ID", "", "FONDO").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_NemotecnicoTemp + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                }
                                catch { }
                                try{
                                    foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", "CAPITAL").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico.Trim() + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                }
                                catch { }
                                
                                if (lbol_EsPrima)
                                {
                                try{
                                    
                                    foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "LP", "PRIMAS").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico.Trim() + "' and IdClaveContable = 50").CopyToDataTable().Rows)
                                        ldat_Tira.ImportRow(dr_tira);
                                }
                                catch { }
                                
                                }
                                else
                                {
                                    try { 
                                    foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "LP", "IMP_DEV").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico.Trim() + "'").CopyToDataTable().Rows)
                                        ldat_Tira.ImportRow(dr_tira);
                                    }
                                    catch { }
                                }
                                //foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", "INT_DEV").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico.Trim() + "'").CopyToDataTable().Rows)
                                //    ldat_Tira.ImportRow(dr_tira);
                                
                                try{
                                    foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", "PRIMAS").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico.Trim() + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                }
                                catch { }
                                try{
                                    foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", lint_EsPublico, "ID", "CP", "RENTA").Tables[0].Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                }
                                catch { }
                                */
                                    
                                bool esCuponCorrido = true;

                                foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                {
                                    string operacion = ldr_Row["CodigoAuxiliar2"].ToString();
                                    string clavecontable = ldr_Row["IdClaveContable"].ToString();

                                    int index = ldat_Tira.Rows.IndexOf(ldr_Row);

                                    if (lbol_EsPrima)
                                    {
                                        switch (operacion.Trim())
                                        {
                                            case "DEUDA POLITICA":
                                            case "":
                                            case "FONDO": //{ ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorTransadoNeto : (lstr_Moneda.Equals("CRC") ? ldec_ValorTransadoNeto : ldec_ValorTransadoNeto * ldec_TipoCambioUDE); break; }
                                                {
                                                    switch (lstr_Origen.Trim())
                                                    {
                                                        case "Rdd": { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorTransadoBruto : (lstr_Moneda.Equals("CRC") ? ldec_ValorTransadoBruto : ldec_ValorTransadoBruto * ldec_TipoCambioUDE); break; }
                                                        default: { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorTransadoNeto : (lstr_Moneda.Equals("CRC") ? ldec_ValorTransadoNeto : ldec_ValorTransadoNeto * ldec_TipoCambioUDE); break; }

                                                    }
                                                    break;
                                                }
                                            case "CAPITAL": { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorFacial : (lstr_Moneda.Equals("CRC") ? ldec_ValorFacial : ldec_ValorFacial * ldec_TipoCambioUDE); break; }
                                            case "PRIMAS":
                                                {
                                                    if (esCuponCorrido)
                                                    {
                                                        ldec_monto = lstr_Moneda.Equals("USD") ? (ldec_ValorTransadoNeto - ldec_ValorTransadoBruto + ldec_Premio) : (lstr_Moneda.Equals("CRC") ? (ldec_ValorTransadoNeto - ldec_ValorTransadoBruto + ldec_Premio) : (ldec_ValorTransadoNeto - ldec_ValorTransadoBruto + ldec_Premio) * ldec_TipoCambioUDE);
                                                        esCuponCorrido = false;
                                                    }
                                                    else
                                                    {
                                                        ldec_monto = lstr_Moneda.Equals("USD") ? (ldec_ValorTransadoBruto - ldec_ValorFacial) : (lstr_Moneda.Equals("CRC") ? (ldec_ValorTransadoBruto - ldec_ValorFacial) : (ldec_ValorTransadoBruto - ldec_ValorFacial) * ldec_TipoCambioUDE);
                                                    }
                                                    ldec_DescuentoPrima = ldec_monto;
                                                    break;
                                                }
                                            case "RENTA": { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ImpuestoPagado : (lstr_Moneda.Equals("CRC") ? ldec_ImpuestoPagado : ldec_ImpuestoPagado * ldec_TipoCambioUDE); break; }
                                        }
                                    }
                                    else
                                    {
                                        switch (operacion.Trim())
                                        {
                                            case "DEUDA POLITICA":
                                            case "":
                                            case "FONDO": //{ ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorTransadoNeto : (lstr_Moneda.Equals("CRC") ? ldec_ValorTransadoNeto : ldec_ValorTransadoNeto * ldec_TipoCambioUDE); break; }
                                                {
                                                    switch (lstr_Origen.Trim())
                                                    {
                                                        case "Rdd": { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorTransadoBruto : (lstr_Moneda.Equals("CRC") ? ldec_ValorTransadoBruto : ldec_ValorTransadoBruto * ldec_TipoCambioUDE); break; }
                                                        default: { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorTransadoNeto : (lstr_Moneda.Equals("CRC") ? ldec_ValorTransadoNeto : ldec_ValorTransadoNeto * ldec_TipoCambioUDE); break; }

                                                    }
                                                    break;
                                                }
                                            case "CAPITAL": { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorFacial : (lstr_Moneda.Equals("CRC") ? ldec_ValorFacial : ldec_ValorFacial * ldec_TipoCambioUDE); break; }
                                            case "IMP_DEV":
                                                {
                                                    if (clavecontable.Trim() == "40")
                                                    {
                                                        ldec_monto = lstr_Moneda.Equals("USD") ? (ldec_ValorFacial - ldec_ValorTransadoBruto) : (lstr_Moneda.Equals("CRC") ? (ldec_ValorFacial - ldec_ValorTransadoBruto) : (ldec_ValorFacial - ldec_ValorTransadoBruto) * ldec_TipoCambioUDE); break;
                                                    }
                                                    else
                                                    {
                                                        ldec_monto = lstr_Moneda.Equals("USD") ? (ldec_ValorTransadoNeto - ldec_ValorTransadoBruto) : (lstr_Moneda.Equals("CRC") ? (ldec_ValorTransadoNeto - ldec_ValorTransadoBruto) : (ldec_ValorTransadoNeto - ldec_ValorTransadoBruto) * ldec_TipoCambioUDE);
                                                    }

                                                    ldec_DescuentoPrima = ldec_monto;
                                                    break;
                                                }
                                            //case "IMP_DEV": { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_RendimientoXDescuento : (lstr_Moneda.Equals("CRC") ? ldec_RendimientoXDescuento : ldec_RendimientoXDescuento * ldec_TipoCambioUDE); break; }
                                            case "RENTA": { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ImpuestoPagado : (lstr_Moneda.Equals("CRC") ? ldec_ImpuestoPagado : ldec_ImpuestoPagado * ldec_TipoCambioUDE); break; }
                                        }
                                    }

                                    DataSet ldat_Reservas = new DataSet();
                                    string lstr_NuevoPosPreDevengo = string.Empty;
                                    //if (ldat_Tira.Rows[0]["IdPosPre"].ToString().Trim().Equals("PP_Balance"))
                                    //{
                                        ldat_Reservas = reservas.ConsultarReservasDetallado(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(), string.Empty, ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(), string.Empty, string.Empty, string.Empty, string.Empty);
                                        //ldat_Reservas = reservas.ConsultarReservasDetallado(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                                        DataView dv = ldat_Reservas.Tables[0].DefaultView;
                                        dv.Sort = "OrdenDeudaInterna ASC";

                                        foreach (DataRow drForm in dv.ToTable().Rows)
                                        {
                                            if (!drForm["OrdenDeudaInterna"].ToString().Equals(string.Empty))
                                                //ldat_AsientoDevengo
                                                if (Convert.ToDecimal(drForm["Monto"]) > 0 && !drForm["OrdenDeudaInterna"].ToString().Equals("0")
                                                    && Convert.ToDecimal(drForm["Monto"]) >= ldec_monto)
                                                {
                                                    lstr_NuevoPosPreDevengo = drForm["IdPosPre"].ToString().Trim();
                                                    break;
                                                }
                                        }
                                    //}

                                        lstr_Referencia = "Colocación " + lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim();

                                    ldat_Asiento.Rows.Add(
                                        lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                        ldt_FchValor.ToString("dd.MM.yyyy"),
                                        ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                        lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim(),
                                        ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                        lstr_NuevoPosPreDevengo.Equals(string.Empty) ? ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim() : lstr_NuevoPosPreDevengo,
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
                            }
                            #endregion

                            #region Define si el título es a corto plazo, pero trasciende en el periodo
                            else if ((Convert.ToDecimal(lstr_Plazo) <= diasAnnos) &&
                            (ldt_FchValor.Year != ldt_FchVencimiento.Year))
                            {
                                lstr_TipoPlazo = "CP";
                                lstr_Operacion = lstr_Moneda.Equals("USD") ? "ID09" : (lstr_Moneda.Equals("CRC") ? "ID07" : "ID07");

                                DataSet lds_Operaciones = loperacion.ConsultarOperaciones(lstr_Operacion, "IdModulo IN ('DI')", "");
                                if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
                                {
                                    lstr_NomOperacion = lds_Operaciones.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
                                }

                                string lstr_NemotecnicoTemp = lstr_Nemotecnico.Equals("TPTBP") ? "TPTBP" : "";
                                string tira_deuda_politica = lstr_Nemotecnico.Equals("TPTBP") ? ",'DEUDA POLITICA'" : "";
                                string tira_primas = lbol_EsPrima ? ",'PRIMAS'" : ",'IMP_DEV'";
                                string lstr_SQL = "select ta.*, SUBSTRING ( IdCuentaContable ,4 , 1 ) AS EsNemotecnico, SUBSTRING ( IdCuentaContable ,10 , 1 ) AS EsPubPriv, SUBSTRING ( IdCuentaContable ,2 , 1 ) AS EsLargoCortoPlazo from ma.TiposAsiento ta where IdModulo = 'DI' and  IdOperacion = '" + lstr_Operacion + "' and Codigo = '" + "G206" + "' AND CodigoAuxiliar = '" + lstr_Moneda + "' AND (CodigoAuxiliar2 IN ('FONDO','CAPITAL','RENTA'" + tira_primas + tira_deuda_politica + ") or isnull(CodigoAuxiliar2,'')='' ) AND (CodigoAuxiliar3 = '" + lstr_Nemotecnico + "' OR ISNULL(CodigoAuxiliar3,'') = '')  AND (CodigoAuxiliar4 = 'ID') AND (CodigoAuxiliar5 = '" + lstr_TipoPlazo + "' OR ISNULL(CodigoAuxiliar5,'')='') AND (CodigoAuxiliar6 = '" + lint_EsPublico + "' OR ISNULL(CodigoAuxiliar6,'') = '')";
                                DataSet lds_Tira = dinamica.ConsultarDinamico(lstr_SQL);

                                ldat_Tira = lds_Tira.Tables[0];//.Clone();

                                //ldat_Tira = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID").Tables[0].Clone();
                                
                                /*try{
                                    foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_NemotecnicoTemp, "", "ID", "", "FONDO").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_NemotecnicoTemp + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                }
                                catch { }
                                try{
                                    foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", "CAPITAL").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico.Trim() + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                }
                                catch { }
                                
                                if (lbol_EsPrima)
                                {
                                    try { 
                                    foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "LP", "PRIMAS").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico.Trim() + "' and IdClaveContable = 50").CopyToDataTable().Rows)
                                        ldat_Tira.ImportRow(dr_tira);
                                    }
                                    catch { }
                                }
                                else
                                {
                                    try { 
                                    foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "LP", "IMP_DEV").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico.Trim() + "'").CopyToDataTable().Rows)
                                        ldat_Tira.ImportRow(dr_tira);
                                    }
                                    catch { }
                                }
                                //foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", "INT_DEV").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico.Trim() + "'").CopyToDataTable().Rows)
                                //    ldat_Tira.ImportRow(dr_tira);
                                
                                try{
                                    foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", "PRIMAS").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico.Trim() + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                }
                                catch { }
                                try{
                                    foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", lint_EsPublico, "ID", "CP", "RENTA").Tables[0].Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                }
                                catch { }*/
                                
                                    
                                bool esCuponCorrido = true;

                                foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                {
                                    string operacion = ldr_Row["CodigoAuxiliar2"].ToString();
                                    string clavecontable = ldr_Row["IdClaveContable"].ToString();

                                    int index = ldat_Tira.Rows.IndexOf(ldr_Row);

                                    if (lbol_EsPrima)
                                    {
                                        switch (operacion.Trim())
                                        {
                                            case "DEUDA POLITICA":
                                            case "":
                                            case "FONDO": //{ ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorTransadoNeto : (lstr_Moneda.Equals("CRC") ? ldec_ValorTransadoNeto : ldec_ValorTransadoNeto * ldec_TipoCambioUDE); break; }
                                                {
                                                    switch (lstr_Origen.Trim())
                                                    {
                                                        case "Rdd": { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorTransadoBruto : (lstr_Moneda.Equals("CRC") ? ldec_ValorTransadoBruto : ldec_ValorTransadoBruto * ldec_TipoCambioUDE); break; }
                                                        default: { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorTransadoNeto : (lstr_Moneda.Equals("CRC") ? ldec_ValorTransadoNeto : ldec_ValorTransadoNeto * ldec_TipoCambioUDE); break; }

                                                    }
                                                    break;
                                                }
                                            case "CAPITAL": { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorFacial : (lstr_Moneda.Equals("CRC") ? ldec_ValorFacial : ldec_ValorFacial * ldec_TipoCambioUDE); break; }
                                            case "PRIMAS":
                                                {
                                                    if (esCuponCorrido)
                                                    {
                                                        ldec_monto = lstr_Moneda.Equals("USD") ? (ldec_ValorTransadoNeto - ldec_ValorTransadoBruto + ldec_Premio) : (lstr_Moneda.Equals("CRC") ? (ldec_ValorTransadoNeto - ldec_ValorTransadoBruto + ldec_Premio) : (ldec_ValorTransadoNeto - ldec_ValorTransadoBruto + ldec_Premio) * ldec_TipoCambioUDE);
                                                        esCuponCorrido = false;
                                                    }
                                                    else
                                                    {
                                                        ldec_monto = lstr_Moneda.Equals("USD") ? (ldec_ValorTransadoBruto - ldec_ValorFacial) : (lstr_Moneda.Equals("CRC") ? (ldec_ValorTransadoBruto - ldec_ValorFacial) : (ldec_ValorTransadoBruto - ldec_ValorFacial) * ldec_TipoCambioUDE);
                                                    }
                                                    ldec_DescuentoPrima = ldec_monto;
                                                    break;
                                                }
                                            case "RENTA": { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ImpuestoPagado : (lstr_Moneda.Equals("CRC") ? ldec_ImpuestoPagado : ldec_ImpuestoPagado * ldec_TipoCambioUDE); break; }
                                        }
                                    }
                                    else
                                    {
                                        switch (operacion.Trim())
                                        {
                                            case "DEUDA POLITICA":
                                            case "":
                                            case "FONDO": //{ ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorTransadoNeto : (lstr_Moneda.Equals("CRC") ? ldec_ValorTransadoNeto : ldec_ValorTransadoNeto * ldec_TipoCambioUDE); break; }
                                                {
                                                    switch (lstr_Origen.Trim())
                                                    {
                                                        case "Rdd": { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorTransadoBruto : (lstr_Moneda.Equals("CRC") ? ldec_ValorTransadoBruto : ldec_ValorTransadoBruto * ldec_TipoCambioUDE); break; }
                                                        default: { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorTransadoNeto : (lstr_Moneda.Equals("CRC") ? ldec_ValorTransadoNeto : ldec_ValorTransadoNeto * ldec_TipoCambioUDE); break; }

                                                    }
                                                    break;
                                                }
                                            case "CAPITAL": { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorFacial : (lstr_Moneda.Equals("CRC") ? ldec_ValorFacial : ldec_ValorFacial * ldec_TipoCambioUDE); break; }
                                            case "IMP_DEV":
                                                {
                                                    if (clavecontable.Trim() == "40")
                                                    {
                                                        ldec_monto = lstr_Moneda.Equals("USD") ? (ldec_ValorFacial - ldec_ValorTransadoBruto) : (lstr_Moneda.Equals("CRC") ? (ldec_ValorFacial - ldec_ValorTransadoBruto) : (ldec_ValorFacial - ldec_ValorTransadoBruto) * ldec_TipoCambioUDE); break;
                                                    }
                                                    else
                                                    {
                                                        ldec_monto = lstr_Moneda.Equals("USD") ? (ldec_ValorTransadoNeto - ldec_ValorTransadoBruto) : (lstr_Moneda.Equals("CRC") ? (ldec_ValorTransadoNeto - ldec_ValorTransadoBruto) : (ldec_ValorTransadoNeto - ldec_ValorTransadoBruto) * ldec_TipoCambioUDE);
                                                    }
                                                    ldec_DescuentoPrima = ldec_monto;
                                                    break;
                                                }
                                            //case "IMP_DEV": { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_RendimientoXDescuento : (lstr_Moneda.Equals("CRC") ? ldec_RendimientoXDescuento : ldec_RendimientoXDescuento * ldec_TipoCambioUDE); break; }
                                            case "RENTA": { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ImpuestoPagado : (lstr_Moneda.Equals("CRC") ? ldec_ImpuestoPagado : ldec_ImpuestoPagado * ldec_TipoCambioUDE); break; }
                                        }
                                    }

                                    DataSet ldat_Reservas = new DataSet();
                                    string lstr_NuevoPosPreDevengo = string.Empty;
                                    //if (ldat_Tira.Rows[0]["IdPosPre"].ToString().Trim().Equals("PP_Balance"))
                                    //{
                                        //ldat_Reservas = reservas.ConsultarReservasDetallado(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                                    
                                    //
                                    ldat_Reservas = reservas.ConsultarReservasDetallado(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(), string.Empty, ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(), string.Empty, string.Empty, string.Empty, string.Empty);
                                        DataView dv = ldat_Reservas.Tables[0].DefaultView;
                                        dv.Sort = "OrdenDeudaInterna ASC";

                                        foreach (DataRow drForm in dv.ToTable().Rows)
                                        {
                                            if (!drForm["OrdenDeudaInterna"].ToString().Equals(string.Empty))
                                                //ldat_AsientoDevengo
                                                if (Convert.ToDecimal(drForm["Monto"]) > 0 && !drForm["OrdenDeudaInterna"].ToString().Equals("0")
                                                    && Convert.ToDecimal(drForm["Monto"]) >= ldec_monto)
                                                {
                                                    lstr_NuevoPosPreDevengo = drForm["IdPosPre"].ToString().Trim();
                                                    break;
                                                }
                                        }

                                        lstr_Referencia = "Colocación " + lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim();

                                    ldat_Asiento.Rows.Add(
                                        lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                        ldt_FchValor.ToString("dd.MM.yyyy"),
                                        ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                        lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim(),
                                        ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                        lstr_NuevoPosPreDevengo.Equals(string.Empty) ? ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim() : lstr_NuevoPosPreDevengo,
                                        ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                        Truncate(ldec_monto, 2),
                                        lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                        tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                        lstr_Moneda,//tipo
                                        lstr_Operacion +"."+lstr_NomOperacion//operacion
                                        );
                                }
                            }
                            #endregion

                            #region Define si el título es a largo plazo con afectación presupuestaria
                            else if (Convert.ToDecimal(lstr_Plazo) > diasAnnos)
                            {
                                lstr_TipoPlazo = "LP";
                                lstr_Operacion = lstr_Moneda.Equals("USD") ? "ID09" : (lstr_Moneda.Equals("CRC") ? "ID07" : "ID07");

                                DataSet lds_Operaciones = loperacion.ConsultarOperaciones(lstr_Operacion, "IdModulo IN ('DI')", "");
                                if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
                                {
                                    lstr_NomOperacion = lds_Operaciones.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
                                }

                                string lstr_NemotecnicoTemp = lstr_Nemotecnico.Equals("TPTBP") ? "TPTBP" : "";
                                //TODO: Buscar y reemplazar en todos los casos para filtrar la deuda politica solo en caso de tptbp(check con la que ya esta) y IMP_DEV cuando no es prima 
                                string tira_deuda_politica = lstr_Nemotecnico.Equals("TPTBP") ? ",'DEUDA POLITICA'" : "";
                                string tira_primas = lbol_EsPrima ? ",'PRIMAS'" : ",'IMP_DEV'";
                                string lstr_SQL = "select ta.*, SUBSTRING ( IdCuentaContable ,4 , 1 ) AS EsNemotecnico, SUBSTRING ( IdCuentaContable ,10 , 1 ) AS EsPubPriv, SUBSTRING ( IdCuentaContable ,2 , 1 ) AS EsLargoCortoPlazo from ma.TiposAsiento ta where IdModulo = 'DI' and  IdOperacion = '" + lstr_Operacion + "' and Codigo = '" + "G206" + "' AND CodigoAuxiliar = '" + lstr_Moneda + "' AND (CodigoAuxiliar2 IN ('FONDO','CAPITAL','RENTA'" + tira_primas + tira_deuda_politica + ") or isnull(CodigoAuxiliar2,'')='' ) AND (CodigoAuxiliar3 = '" + lstr_Nemotecnico + "' OR ISNULL(CodigoAuxiliar3,'') = '')  AND (CodigoAuxiliar4 = 'ID') AND (CodigoAuxiliar5 = '" + lstr_TipoPlazo + "' OR ISNULL(CodigoAuxiliar5,'')='') AND (CodigoAuxiliar6 = '" + lint_EsPublico + "' OR ISNULL(CodigoAuxiliar6,'') = '')";
                                DataSet lds_Tira = dinamica.ConsultarDinamico(lstr_SQL);

                                ldat_Tira = lds_Tira.Tables[0];//.Clone();

                                //ldat_Tira = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID").Tables[0].Clone();
                                
                                /*try{
                                    foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_NemotecnicoTemp, "", "ID", "", "FONDO").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_NemotecnicoTemp + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                }
                                catch { }
                                try
                                {
                                    foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "LP", "CAPITAL").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico.Trim() + "'").CopyToDataTable().Rows)
                                        ldat_Tira.ImportRow(dr_tira);
                                }
                                catch { }
                                    
                                if (lbol_EsPrima)
                                {
                                    try { 
                                    foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "LP", "PRIMAS").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico.Trim() + "' and IdClaveContable = 50").CopyToDataTable().Rows)
                                        ldat_Tira.ImportRow(dr_tira);
                                    }
                                    catch { }
                                }
                                else
                                {
                                    try { 
                                    foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "LP", "IMP_DEV").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico.Trim() + "'").CopyToDataTable().Rows)
                                        ldat_Tira.ImportRow(dr_tira);
                                    }
                                    catch { }
                                }
                                
                                //foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", "INT_DEV").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico.Trim() + "'").CopyToDataTable().Rows)
                                //    ldat_Tira.ImportRow(dr_tira);
                                if (lbol_EsPrima)
                                {
                                try{
                                    
                                    foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "LP", "PRIMAS").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico.Trim() + "'").CopyToDataTable().Rows)
                                        ldat_Tira.ImportRow(dr_tira);
                                }
                                catch { }
                                
                                    
                                }
                                //foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", lint_EsPublico, "ID", "", "RENTA").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico.Trim() + "'").CopyToDataTable().Rows)
                                //    ldat_Tira.ImportRow(dr_tira);
                                */
                                bool esCuponCorrido = true;

                                //DataRow ldr_Row = new DataRow();
                                bool Prima = false;
                                for (int i = 0; i < ldat_Tira.Rows.Count;i++ )
                                {
                                    DataRow ldr_Row = ldat_Tira.Rows[i];
                                    string operacion = ldr_Row["CodigoAuxiliar2"].ToString();
                                    string clavecontable = ldr_Row["IdClaveContable"].ToString();

                                    int index = ldat_Tira.Rows.IndexOf(ldr_Row);

                                    if (lbol_EsPrima)
                                    {
                                        switch (operacion.Trim())
                                        {
                                            case "DEUDA POLITICA":
                                            case "":
                                            case "FONDO": //{ ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorTransadoNeto : (lstr_Moneda.Equals("CRC") ? ldec_ValorTransadoNeto : ldec_ValorTransadoNeto * ldec_TipoCambioUDE); break; }
                                                {
                                                    switch (lstr_Origen.Trim())
                                                    {
                                                        case "Rdd": { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorTransadoBruto : (lstr_Moneda.Equals("CRC") ? ldec_ValorTransadoBruto : ldec_ValorTransadoBruto * ldec_TipoCambioUDE); break; }
                                                        default: { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorTransadoNeto : (lstr_Moneda.Equals("CRC") ? ldec_ValorTransadoNeto : ldec_ValorTransadoNeto * ldec_TipoCambioUDE); break; }

                                                    }
                                                    break;
                                                }
                                            case "CAPITAL": { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorFacial : (lstr_Moneda.Equals("CRC") ? ldec_ValorFacial : ldec_ValorFacial * ldec_TipoCambioUDE); break; }
                                            case "PRIMAS":
                                                {
                                                    if (esCuponCorrido)
                                                    {
                                                        //Tiene 2 líenas:
                                                        if (!Prima) //Linea 1 Cupón corrido
                                                        {
                                                            ldec_monto = lstr_Moneda.Equals("USD") || lstr_Moneda.Equals("CRC") ? (ldec_ValorTransadoNeto - ldec_ValorTransadoBruto) :  (ldec_ValorTransadoNeto - ldec_ValorTransadoBruto) * ldec_TipoCambioUDE;
                                                            Prima = true;
                                                            i--;
                                                        }
                                                        else 
                                                        {
                                                            ldec_monto = lstr_Moneda.Equals("USD") || lstr_Moneda.Equals("CRC") ?    ldec_Premio :  ldec_Premio * ldec_TipoCambioUDE;
                                                            esCuponCorrido = false;
                                                        }
                                                      
                                                    }
                                                    else
                                                    {
                                                        ldec_monto = lstr_Moneda.Equals("USD") ? (ldec_ValorTransadoBruto - ldec_ValorFacial) : (lstr_Moneda.Equals("CRC") ? (ldec_ValorTransadoBruto - ldec_ValorFacial) : (ldec_ValorTransadoBruto - ldec_ValorFacial) * ldec_TipoCambioUDE);
                                                    }
                                                    ldec_DescuentoPrima = ldec_monto;
                                                    break;
                                                }
                                            case "RENTA": { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ImpuestoPagado : (lstr_Moneda.Equals("CRC") ? ldec_ImpuestoPagado : ldec_ImpuestoPagado * ldec_TipoCambioUDE); break; }
                                        }
                                    }
                                    else
                                    {
                                        switch (operacion.Trim())
                                        {
                                            case "DEUDA POLITICA":
                                            case "":
                                            case "FONDO": //{ ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorTransadoNeto : (lstr_Moneda.Equals("CRC") ? ldec_ValorTransadoNeto : ldec_ValorTransadoNeto * ldec_TipoCambioUDE); break; }
                                                {
                                                    switch (lstr_Origen.Trim())
                                                    {
                                                        case "Rdd": { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorTransadoBruto : (lstr_Moneda.Equals("CRC") ? ldec_ValorTransadoBruto : ldec_ValorTransadoBruto * ldec_TipoCambioUDE); break; }
                                                        default: { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorTransadoNeto : (lstr_Moneda.Equals("CRC") ? ldec_ValorTransadoNeto : ldec_ValorTransadoNeto * ldec_TipoCambioUDE); break; }

                                                    }
                                                    break;
                                                }
                                            case "CAPITAL": { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorFacial : (lstr_Moneda.Equals("CRC") ? ldec_ValorFacial : ldec_ValorFacial * ldec_TipoCambioUDE); break; }
                                            case "IMP_DEV":
                                                {
                                                    if (clavecontable.Trim() == "40")
                                                    {
                                                        ldec_monto = lstr_Moneda.Equals("USD") ? (ldec_ValorFacial - ldec_ValorTransadoBruto) : (lstr_Moneda.Equals("CRC") ? (ldec_ValorFacial - ldec_ValorTransadoBruto) : (ldec_ValorFacial - ldec_ValorTransadoBruto) * ldec_TipoCambioUDE); break;
                                                    }
                                                    else
                                                    {
                                                        ldec_monto = lstr_Moneda.Equals("USD") ? (ldec_ValorTransadoNeto - ldec_ValorTransadoBruto) : (lstr_Moneda.Equals("CRC") ? (ldec_ValorTransadoNeto - ldec_ValorTransadoBruto) : (ldec_ValorTransadoNeto - ldec_ValorTransadoBruto) * ldec_TipoCambioUDE);
                                                    }
                                                    ldec_DescuentoPrima = ldec_monto;
                                                    break;
                                                }
                                            //case "IMP_DEV": { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_RendimientoXDescuento : (lstr_Moneda.Equals("CRC") ? ldec_RendimientoXDescuento : ldec_RendimientoXDescuento * ldec_TipoCambioUDE); break; }
                                            case "RENTA": { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ImpuestoPagado : (lstr_Moneda.Equals("CRC") ? ldec_ImpuestoPagado : ldec_ImpuestoPagado * ldec_TipoCambioUDE); break; }
                                        }
                                    }

                                    DataSet ldat_Reservas = new DataSet();
                                    string lstr_NuevoPosPreDevengo = string.Empty;
                                    //if (ldat_Tira.Rows[0]["IdPosPre"].ToString().Trim().Equals("PP_Balance"))
                                    //{
                                    ldat_Reservas = reservas.ConsultarReservasDetallado(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(), string.Empty, ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(), string.Empty, string.Empty, string.Empty, string.Empty);
                                    //ldat_Reservas = reservas.ConsultarReservasDetallado(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
                                    DataView dv = ldat_Reservas.Tables[0].DefaultView;
                                    dv.Sort = "OrdenDeudaInterna ASC";

                                    foreach (DataRow drForm in dv.ToTable().Rows)
                                    {
                                        if (!drForm["OrdenDeudaInterna"].ToString().Equals(string.Empty))
                                            //ldat_AsientoDevengo
                                            if (Convert.ToDecimal(drForm["Monto"]) > 0 && !drForm["OrdenDeudaInterna"].ToString().Equals("0")
                                                && Convert.ToDecimal(drForm["Monto"]) >= ldec_monto)
                                            {
                                                lstr_NuevoPosPreDevengo = drForm["IdPosPre"].ToString().Trim();
                                                break;
                                            }
                                    }
                                    //}

                                    lstr_Referencia = "Colocación " + lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim();

                                    ldat_Asiento.Rows.Add(
                                        lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                        ldt_FchValor.ToString("dd.MM.yyyy"),
                                        ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                        lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim(),
                                        ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                        lstr_NuevoPosPreDevengo.Equals(string.Empty) ? ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim() : lstr_NuevoPosPreDevengo,
                                        ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                        Truncate(ldec_monto, 2),
                                        lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                        lstr_Referencia,//texto del asiento
                                        lstr_Moneda,//tipo
                                        lstr_Operacion + "." + lstr_NomOperacion //operacion
                                        );
                                }
                            }
                            #endregion
                            break;
                        }
                    #endregion
                }
                lstr_Mensaje = GenerarAsientoAjuste(ldat_Asiento, lstr_Operacion, lstr_NroValor, lstr_Nemotecnico, ldec_TipoCambioColones, ldt_FchModifica, ldec_MontoPrincipal, ldec_DescuentoPrima, ldt_FchVencimiento);
            }
            return lstr_Mensaje;
        }

        public static string GenerarAsientoAjuste(DataTable ldat_Asiento, string lstr_IdOperacion, string lstr_NroValor, string lstr_Nemotecnico, decimal ldec_TipoCambio, DateTime ldt_FchModifica, decimal ldec_MontoPrincipal, decimal ldec_DescuentoPrima, DateTime ldt_FchVencimiento)
        {
            //Coleccion de asientos y tipos de asientos requeridos en SAP expuestos por la referencia del servicio
            wrSigafAsientos.ZfiAsiento item_asiento = new wrSigafAsientos.ZfiAsiento();
            wrSigafAsientos.ZfiAsiento[] tabla_asientos = new wrSigafAsientos.ZfiAsiento[ldat_Asiento.Rows.Count];
            int i = 0;
            i = ldat_Asiento.Rows.Count;
            //variables de proceso

            string lstr_Id = "";
            lstr_Id = lstr_NroValor + '.' + lstr_Nemotecnico;
            DateTime fechaContabilizacion = System.DateTime.Today;
            string[] item_resAsientosLog = new string[10];
            string logAsiento = string.Empty;
            string flagEstadoAsiento = string.Empty;
            DateTime ldt_FechaContabiliza = new DateTime();
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
                        fechaContabilizacion = Convert.ToDateTime(ldat_Asiento.Rows[index]["Fecha"].ToString());//Fecha de contabilización
                        ldt_FechaContabiliza = DateTime.ParseExact(ldat_Asiento.Rows[index]["Fecha"].ToString(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
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
                    logAsiento += x + " - " + item_resAsientosLog[j] + " - ";
                }
                //Registrar en Bitacora de movimientos
                bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_IdOperacion, "DI"), "Resultado de Contabilización: " + logAsiento, lstr_IdOperacion, lstr_NroValor + "-" + lstr_Nemotecnico, "");

                // convertir el 
                //Marcar registro como contabilizado
                
                string[] a = new string[2];
                if (!logAsiento.Contains("[E]"))
                {

                    string lstr_codAsiento = logAsiento.Substring(logAsiento.IndexOf("BKPFF") + 6, 18);//logAsiento.Length - logAsiento.IndexOf("BKPFF") - 1);
                    string str_CodRes = string.Empty;
                    string str_Msg = string.Empty;
                    int int_Consec = 0;
                    int int_Secuencia = 0;

                    lcls_CostoTransaccion.ContabilizarCalculosFinancieros(
                        "TitulosValores",
                        null,
                        lstr_NroValor.Trim(),
                        lstr_Nemotecnico.Trim(),
                        "C",
                        "SG",
                        ldt_FchModifica, out a[0], out a[1]);

                    dinamica.ConsultarDinamico("INSERT INTO cf.ColocacionesTitulos (NroValor," +
                                    "Nemotecnico," +
                                    "NumAsiento," +
                                    "FchContabilizacion," +
                                    "MontoPrincipal," +
                                    "DescuentoPrima," +
                                    "FchVencimiento" +
                                    ")" +
                                    "VALUES (" + 
                                    lstr_NroValor + ", "+
                                    "'" + lstr_Nemotecnico + "', "+
                                    "'" + lstr_codAsiento + "', " +
                                    "'" + ldt_FechaContabiliza.ToString("yyyy/MM/dd") + "', " +
                                    ldec_MontoPrincipal.ToString().Replace(",", ".") + ", " +
                                    ldec_DescuentoPrima.ToString().Replace(",", ".") + ", " +
                                     "'" + ldt_FchVencimiento.ToString("yyyy/MM/dd") + "'" + 
                                    ")");
                    
               //     Console.WriteLine(" Asiento" +  lstr_codAsiento);


                            try{
                                tiposAsiento.CrearAsiento(0, "DI", lstr_IdOperacion, null, lstr_Id, fechaContabilizacion, "", "C", lstr_codAsiento, logAsiento, "SG", out str_CodRes, out str_Msg, out int_Consec);
                                            if (str_CodRes == "00")
                                            {
                                                for (int y = 0; y < i; y++)
                                                {
                                                    tiposAsiento.CrearAsientoLinea(
                                                        int_Consec, 		
                                                        0,		
                                                        tabla_asientos[y].Bldat ,		
                                                        tabla_asientos[y].Blart	,	
                                                        tabla_asientos[y].Bukrs ,		
                                                        tabla_asientos[y].Budat ,		
                                                        tabla_asientos[y].Waers ,
                                                        Convert.ToDecimal(tabla_asientos[y].Kursf),		
                                                        tabla_asientos[y].Xblnr ,		
                                                        tabla_asientos[y].Xref1Hd, 		
                                                        tabla_asientos[y].Xref2Hd, 		
                                                        tabla_asientos[y].Buzei ,		
                                                        tabla_asientos[y].Bktxt ,		
                                                        tabla_asientos[y].Bschl ,		
                                                        tabla_asientos[y].Hkont ,		
                                                        tabla_asientos[y].Umskz ,		
                                                        Convert.ToDecimal(tabla_asientos[y].Wrbtr) ,		
                                                        Convert.ToDecimal(tabla_asientos[y].Dmbe2) ,		
                                                        tabla_asientos[y].Mwskz ,		
                                                        tabla_asientos[y].Xmwst ,		
                                                        tabla_asientos[y].Zfbdt ,		
                                                        tabla_asientos[y].Zuonr ,		
                                                        tabla_asientos[y].Sgtxt ,		
                                                        tabla_asientos[y].Hbkid ,		
                                                        tabla_asientos[y].Zlsch ,		
                                                        tabla_asientos[y].Kostl ,		
                                                        tabla_asientos[y].Prctr ,		
                                                        tabla_asientos[y].Aufnr ,		
                                                        tabla_asientos[y].Projk ,		
                                                        tabla_asientos[y].Fipex ,		
                                                        tabla_asientos[y].Fistl ,		
                                                        tabla_asientos[y].Measure, 		
                                                        tabla_asientos[y].Geber ,	
                                                        tabla_asientos[y].Werks ,		
                                                        tabla_asientos[y].Valut ,		
                                                        tabla_asientos[y].Kblnr ,		
                                                        tabla_asientos[y].Kblpos ,		
                                                        tabla_asientos[y].Rcomp ,	
                                                        tabla_asientos[y].Xref2 ,		
                                                        tabla_asientos[y].Xref3 ,		
                                                        tabla_asientos[y].Fkber ,
                                                        "SG", out str_CodRes, out str_Msg, out int_Secuencia);
                                                }
                                            }
                                        }
                            catch (Exception e1)
                            {

                                //lstr_CodResultado = "01";
                                //lstr_Mensaje = "Error al guardar asiento final. Operación: " + lstr_IdOperacion + ". Acreedor: " +
                                //    lstr_abrevAcreedor + ". " + logAsiento;
                                //resAsientosLog = reg_Bitacora.ufnRegistrarAccionBitacora(str_IdModulo, "1", lstr_IdOperacion + "|" + lstr_abrevAcreedor, "Resultado de Contabilización " + lstr_NomOperacion + " " + lstr_Id + ": " + lstr_Mensaje);
                                ////Log.Info(lstr_Mensaje);
                            }

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