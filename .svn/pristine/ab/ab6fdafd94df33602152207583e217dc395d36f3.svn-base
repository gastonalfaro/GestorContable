using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Globalization;
using LogicaNegocio.Mantenimiento;
using LogicaNegocio.Seguridad;
using System.IO;

namespace LogicaNegocio.CalculosFinancieros.DeudaInterna
{
    public class clsContabilizarPagoCupones
    {
        private static Mantenimiento.clsDinamico dinamica = new Mantenimiento.clsDinamico();
        private static Mantenimiento.clsNemotecnicos nemotecnico = new Mantenimiento.clsNemotecnicos();
        private static Mantenimiento.clsTiposCambio tipocambio = new Mantenimiento.clsTiposCambio();
        private static Mantenimiento.clsPropietarios propietario = new Mantenimiento.clsPropietarios();
        private static Mantenimiento.clsTiposAsiento tipoasiento = new Mantenimiento.clsTiposAsiento();
        private static Mantenimiento.clsOperaciones loperacion = new clsOperaciones();
        private static Seguridad.tBitacora bitacora = new Seguridad.tBitacora();
        private static tiras tira = new tiras();
        //private static wsAsientos.ServicioContable wsAsientos = new wsAsientos.ServicioContable();
        private static clsTiposAsiento tasientos = new clsTiposAsiento();
        private static clsCostoTransaccion lcls_CostoTransaccion = new clsCostoTransaccion();
        private static wsDeudaInterna.wsDeudaInterna wsDeudaInterna = new wsDeudaInterna.wsDeudaInterna();
        private static string lstr_separador_decimal = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
        static string aEmisionSerie;

        private static int identity = 1;

        public string contabilizaPagoCupones(int? lint_NroValor = null, string lstr_Nemotecnico = null, DateTime? lstr_FchInicio = null, DateTime? lstr_FchFin = null)
        {
            string lstr_Mensaje = "00 - Proceso Finalizado";
            try
            {
                aEmisionSerie = "";
                DateTime? _fchInicio = lstr_FchInicio == null ? DateTime.Today : lstr_FchInicio;
                DateTime? _fchFin = lstr_FchFin == null ? DateTime.Today : lstr_FchFin;
                string query = "select b.tipo, b.NroValor, b.NemoTecnico, b.TasaBruta, b.TasaNeta, b.ValorTransadoBruto, b.ValorTransadoNeto, b.Moneda, b.Propietario, " +
                               "b.PlazoValor, b.FchValor, b.FchVencimiento, b.ModuloSINPE, b.FchModifica, b.NroEmisionSerie, a.NroCupon, a.InteresBruto, " +
                               "a.FchCancelacion, a.FchInicio, a.FchVencimiento as FchVencimientoCupon from cf.titulosvalores a " +
                               "inner join cf.titulosvalores b "+
                               "on a.nrovalor = b.nrovalor "+
                               "and a.nemotecnico = b.nemotecnico "+
                               "where a.indicadorcupon = 'C' "+
                               "and b.indicadorcupon = 'V' "+
                               "and a.estadovalor = 'Cancelada' "+
                               "and (a.nrovalor = '" + lint_NroValor + "' or isnull('" + lint_NroValor + "','') ='') " +
                               "and (a.nemotecnico = '" + lstr_Nemotecnico + "' or isnull('" + lstr_Nemotecnico + "','') ='') " +
                               "and a.fchcancelacion between '" + _fchInicio.Value.ToString("yyyy-MM-dd") + "' and '" + _fchFin.Value.ToString("yyyy-MM-dd") + "'";

                DataSet totalTitulosValores = dinamica.ConsultarDinamico(query);
                DataTable ldat_TitulosValores = totalTitulosValores.Tables[0];

                DataTable ldat_Nemotecnicos = nemotecnico.ConsultarNemotecnicos(null, null, null, null, null).Tables[0];

                for (int i = 0; i < ldat_TitulosValores.Rows.Count; i++)
                {
                    try
                    {
                        if (i == 0) 
                        { 
                            aEmisionSerie= ldat_TitulosValores.Rows[i]["NroEmisionSerie"].ToString();
                        }

                        string lstr_moneda = ldat_Nemotecnicos.Select("IdNemotecnico = '" + ldat_TitulosValores.Rows[i]["NemoTecnico"].ToString() + "'")[0]["IdMoneda"].ToString().Trim().Equals("CRCN")
                           ? "CRC" : ldat_Nemotecnicos.Select("IdNemotecnico = '" + ldat_TitulosValores.Rows[i]["NemoTecnico"].ToString() + "'")[0]["IdMoneda"].ToString().Trim();

                        if (ldat_Nemotecnicos.Select("IdNemotecnico = '" + ldat_TitulosValores.Rows[i]["NemoTecnico"].ToString() + "'")[0]["Estado"].ToString().Trim() == "A"
                            && lstr_moneda == (ldat_TitulosValores.Rows[i]["Moneda"].ToString().Equals("CRCN") ? "CRC" : ldat_TitulosValores.Rows[i]["Moneda"].ToString()))
                        {
                            if (true)
                            {
         
                                /**
                                 * Valores:
                                 * 
                                 *  • Valor Facial
                                 *  • Valor transado bruto
                                 *  • Valor transado neto
                                 *  • Fecha de vencimiento
                                 *  • Numero de valor
                                 *  • Nemotécnico
                                 *  • Moneda
                                 *  • Propietario
                                 *  • Estado Valor
                                 * 
                                 * */
                                lstr_Mensaje = PagoCupones(

                                    //Información de título
                                    ldat_TitulosValores.Rows[i]["NroValor"].ToString(),
                                    ldat_TitulosValores.Rows[i]["NemoTecnico"].ToString(),
                                    ldat_TitulosValores.Rows[i]["Tipo"].ToString(),

                                    Convert.ToDecimal(ldat_TitulosValores.Rows[i]["ValorTransadoBruto"].ToString()),
                                    Convert.ToDecimal(ldat_TitulosValores.Rows[i]["ValorTransadoNeto"].ToString()),
                                    ldat_TitulosValores.Rows[i]["Moneda"].ToString().Equals("CRCN") ? "CRC" : ldat_TitulosValores.Rows[i]["Moneda"].ToString(),
                                    ldat_TitulosValores.Rows[i]["Propietario"].ToString(),
                                    ldat_TitulosValores.Rows[i]["PlazoValor"].ToString(),
                                    Convert.ToDateTime(ldat_TitulosValores.Rows[i]["FchValor"].ToString()),
                                    Convert.ToDateTime(ldat_TitulosValores.Rows[i]["FchVencimiento"].ToString()),
                                    ldat_TitulosValores.Rows[i]["ModuloSINPE"].ToString().Trim(),
                                    Convert.ToDateTime(ldat_TitulosValores.Rows[i]["FchModifica"].ToString()),

                                    //Información del cupón
                                    ldat_TitulosValores.Rows[i]["NroCupon"].ToString(),
                                    Convert.ToDecimal(ldat_TitulosValores.Rows[i]["InteresBruto"].ToString()),
                                    Convert.ToDateTime(ldat_TitulosValores.Rows[i]["FchCancelacion"].ToString()),
                                    Convert.ToDateTime(ldat_TitulosValores.Rows[i]["FchInicio"].ToString()),
                                    Convert.ToDateTime(ldat_TitulosValores.Rows[i]["FchVencimientoCupon"].ToString()),
                                    
                                    //Información para asiento
                                    "SINPE: " + ldat_TitulosValores.Rows[i]["ModuloSINPE"].ToString().Trim() + "-" + "T.B: " +
                                    ldat_TitulosValores.Rows[i]["TasaBruta"].ToString().Trim() + "-" + "T.N: " +
                                    ldat_TitulosValores.Rows[i]["TasaNeta"].ToString().Trim() + "-" + "Plazo: " +
                                    ldat_TitulosValores.Rows[i]["PlazoValor"].ToString().Trim());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        //TODO: Insertar mensaje de error
                        lstr_Mensaje = "99 - Error "+ ex.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                lstr_Mensaje = "99 - Error " + ex.ToString();
            }
            return lstr_Mensaje;
        }

        /// <summary>
        /// Obtiene el porcentaje del ajuste del canje mediante una sumatoria de los porcentajes existences
        /// </summary>
        private static Decimal get_canje_porcentaje(String nemotencnico, String nrovalor)
        {
            Decimal vPorcentaje = 0;
            try
            {
                DataTable porcentajes = dinamica.ConsultarDinamico("exec cf.uspGetPorcentajeEmision '" + nemotencnico + "', " + nrovalor + "").Tables[0];
                foreach (DataRow porcentaje in porcentajes.Rows)
                {
                    vPorcentaje = Decimal.Parse(porcentaje[0].ToString());
                }
              //  vPorcentaje = 1 - vPorcentaje;
            }
            catch (Exception ex)
            {

            }
            return vPorcentaje;
        }

        private static decimal Truncate(decimal value, int length)
        {
            return Math.Truncate(value * 100) / 100;
        }

        //TODO: Ajustar parametros, copiar metodo registrocontable
        public static string PagoCupones(
            string lstr_NroValor,
            string lstr_Nemotecnico,

            string lstr_tipo,

            decimal ldec_ValorTransadoBruto,
            decimal ldec_ValorTransadoNeto,
            string lstr_Moneda,
            string lstr_Propietario,
            string lstr_Plazo,
            DateTime ldt_FchValor,
            DateTime ldt_FchVencimiento,
            string lstr_Origen,
            DateTime ldt_FchModifica,

            //Información del cupón
            string lstr_NroCupon,
            decimal ldec_InteresBruto,
            DateTime ldt_FchCancelacion,
            DateTime ldt_FchInicio,
            DateTime ldt_FchVencimientoCupon,
                                    
            //Información para asiento                                    
            string lstr_Detalle)
        {
            string lstr_Mensaje = string.Empty;
            List<string[]> arregloTiras;
            string[] AuxiliaresPrimerPago;
            string[] AuxiliaresSigPago;
            string[] AuxiliaresReversion;
            string lstr_OperacionPago = string.Empty;
            string lstr_OperacionRever = string.Empty;
            string lstr_NomOperacionPago = string.Empty;
            string lstr_NomOperacionRever = string.Empty;
            ClsUtilitarios utilitario = new ClsUtilitarios(); 

            string query = "select isnull(sum(isnull(cupon,0)),0) as SumaInteres from cf.devengosmensuales where nrovalor = " + lstr_NroValor + " and nemotecnico = '" + lstr_Nemotecnico + "' and convert(date, periodo,103) between '" + ldt_FchInicio.AddDays(1).ToString("yyyy-MM-dd") + "' and '" + ldt_FchVencimientoCupon.ToString("yyyy-MM-dd") + "'";

            DataSet totalMontoInteres = dinamica.ConsultarDinamico(query);
            decimal montoInteresTotal = Math.Round(Convert.ToDecimal( totalMontoInteres.Tables[0].Rows.Count.Equals(0) ? "0" : totalMontoInteres.Tables[0].Rows[0]["SumaInteres"].ToString()),2);

            //valida si son distintos hace redondéo a dos decimales
            if (montoInteresTotal != ldec_InteresBruto) 
            {
                ldec_InteresBruto = ldec_InteresBruto - ldec_InteresBruto * get_canje_porcentaje(lstr_Nemotecnico, lstr_NroValor);
                ldec_InteresBruto = Math.Round(ldec_InteresBruto, 2);
            }//

            #region Filtros para las tiras

            //Define el plazo segun el plazo del título
            int diasAños = lstr_Origen.ToUpper().Equals("RDI") ? 360 : 1;
            string esCortoPlazoPago = Convert.ToDecimal(lstr_Plazo) <= diasAños ? "CP" : "LP";
            
            //Define el plazo segun la diferencia entre la fecha actual y la fecha de vencimiento
            string esCortoPlazoRever = string.Empty;

            //Plazo
            if (utilitario.DiferenciaFechas(ldt_FchVencimientoCupon, ldt_FchVencimiento))
                esCortoPlazoRever = "LP";
            else
                esCortoPlazoRever = "CP";

            //Define si el propietario es público o privado
            string lstr_EsPublico = propietario.ConsultarPropietarios(string.Empty, string.Empty, string.Empty, lstr_Propietario, "S").Tables[0].Rows.Count.Equals(0) ? "PRIVADO" : "PUBLICO";

            #endregion

            #region tipo de cambio

            //Define el tipo de cambio
            decimal ldec_TipoCambioColones = Convert.ToDecimal(tipocambio.ConsultarTiposCambio("CRCN", ldt_FchCancelacion, "3140", "N").Tables[0].Rows[0]["Valor"].ToString());
            decimal ldec_TipoCambioUDE =lstr_Moneda.Equals("UDE") ?Convert.ToDecimal(tipocambio.ConsultarTiposCambio("UDE", ldt_FchCancelacion, "", "N").Tables[0].Rows[0]["Valor"].ToString()):0;
            decimal ldec_TipoCambio = lstr_Moneda.Equals("USD") ? ldec_TipoCambioColones : (lstr_Moneda.Equals("CRC") ? 1 : ldec_TipoCambioUDE);

            #endregion

            #region primer cupon

            bool esPrimerCupon = true;
            DataSet lds_Cupones = dinamica.ConsultarDinamico("select count(*) as Numero from cf.titulosvalores where nrovalor = " + lstr_NroValor + " and nemotecnico = '" + lstr_Nemotecnico + "' and estadovalor = 'Cancelada' and indicadorcupon = 'C'");
            DataTable ldat_Cupones = lds_Cupones.Tables[0];
            foreach (DataRow ldr_Cupon in ldat_Cupones.Rows)
            {
                if(!ldr_Cupon["Numero"].ToString().Equals("1"))
                    esPrimerCupon = false;
            }

            #endregion 

            if (esPrimerCupon)
            {
                #region armar tira para primer pago de cupón

                if(esCortoPlazoPago.Equals("CP"))
                    lstr_OperacionPago = lstr_Moneda.Equals("USD") ? "ID15" : (lstr_Moneda.Equals("CRC") ? "ID13" : "ID13");
                else
                    lstr_OperacionPago = lstr_Moneda.Equals("USD") ? "ID16" : (lstr_Moneda.Equals("CRC") ? "ID14" : "ID14");

                DataSet lds_Operaciones = loperacion.ConsultarOperaciones(lstr_OperacionPago, "IdModulo IN ('DI')", "");
                if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
                {
                    lstr_NomOperacionPago = lds_Operaciones.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
                }

                arregloTiras = new List<string[]>();

                AuxiliaresPrimerPago = new string[10];
                AuxiliaresPrimerPago[0] = lstr_OperacionPago; //IdOperacion
                AuxiliaresPrimerPago[1] = null; //CodigoAuxiliar1
                AuxiliaresPrimerPago[2] = null; //CodigoAuxiliar2
                AuxiliaresPrimerPago[3] = null; //CodigoAuxiliar3
                AuxiliaresPrimerPago[4] = "ID"; //CodigoAuxiliar4
                AuxiliaresPrimerPago[5] = null; //CodigoAuxiliar5
                AuxiliaresPrimerPago[6] = "GASTO"; //CodigoAuxiliar6
                AuxiliaresPrimerPago[7] = "Pago Primer Cupón"; //Descripción
                AuxiliaresPrimerPago[8] = "PRIMER_CUPON"; //Descripción
                AuxiliaresPrimerPago[9] = lstr_NomOperacionPago; 
                arregloTiras.Add(AuxiliaresPrimerPago);

                AuxiliaresPrimerPago = new string[10];
                AuxiliaresPrimerPago[0] = lstr_OperacionPago; //IdOperacion
                AuxiliaresPrimerPago[1] = null; //CodigoAuxiliar1
                AuxiliaresPrimerPago[2] = lstr_Nemotecnico.Trim(); //CodigoAuxiliar2
                AuxiliaresPrimerPago[3] = lstr_EsPublico; //CodigoAuxiliar3
                AuxiliaresPrimerPago[4] = "ID"; //CodigoAuxiliar4
                AuxiliaresPrimerPago[5] = esCortoPlazoPago; //CodigoAuxiliar5
                AuxiliaresPrimerPago[6] = "INT_DEV"; //CodigoAuxiliar6
                AuxiliaresPrimerPago[7] = "Cupon Corrido Primer Pago"; //Descripción
                AuxiliaresPrimerPago[8] = "CUPON_CORRIDO"; //Descripción
                AuxiliaresPrimerPago[9] = lstr_NomOperacionPago; 
                arregloTiras.Add(AuxiliaresPrimerPago);

                #endregion
            }
            else
            {
                #region armar tira para siguientes pagos pago de cupón

                if(esCortoPlazoPago.Equals("CP"))
                    lstr_OperacionPago = lstr_Moneda.Equals("USD") ? "ID23" : (lstr_Moneda.Equals("CRC") ? "ID21" : "ID21");
                else
                    lstr_OperacionPago = lstr_Moneda.Equals("USD") ? "ID24" : (lstr_Moneda.Equals("CRC") ? "ID22" : "ID22");

                DataSet lds_Operaciones = loperacion.ConsultarOperaciones(lstr_OperacionPago, "IdModulo IN ('DI')", "");
                if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
                {
                    lstr_NomOperacionPago = lds_Operaciones.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
                }

                arregloTiras = new List<string[]>();

                AuxiliaresSigPago = new string[10];
                AuxiliaresSigPago[0] = lstr_OperacionPago; //IdOperacion
                AuxiliaresSigPago[1] = null; //CodigoAuxiliar1
                AuxiliaresSigPago[2] = null; //CodigoAuxiliar2
                AuxiliaresSigPago[3] = null; //CodigoAuxiliar3
                AuxiliaresSigPago[4] = "ID"; //CodigoAuxiliar4
                AuxiliaresSigPago[5] = esCortoPlazoPago; //CodigoAuxiliar5
                AuxiliaresSigPago[6] = "GASTO"; //CodigoAuxiliar6
                AuxiliaresSigPago[7] = "Pago Siguiente Cupón"; //Descripción
                AuxiliaresSigPago[8] = "SIGUIENTE_CUPON"; //Descripción
                AuxiliaresSigPago[9] = lstr_NomOperacionPago; 
                arregloTiras.Add(AuxiliaresSigPago);

                #endregion
            }

            #region armar tira para reversión de devengo
            
            if(esCortoPlazoRever.Equals("CP"))
                lstr_OperacionRever = lstr_Moneda.Equals("USD") ? "ID19" : (lstr_Moneda.Equals("CRC") ? "ID17" : "ID17");
            else
                lstr_OperacionRever = lstr_Moneda.Equals("USD") ? "ID20" : (lstr_Moneda.Equals("CRC") ? "ID18" : "ID18");

            DataSet lds_OperacionesRever = loperacion.ConsultarOperaciones(lstr_OperacionRever, "IdModulo IN ('DI')", "");
            if (lds_OperacionesRever.Tables.Count > 0 && lds_OperacionesRever.Tables["Table"].Rows.Count > 0)
            {
                lstr_NomOperacionRever = lds_OperacionesRever.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
            }

            //arregloTiras = new List<string[]>();

            AuxiliaresReversion = new string[10];
            AuxiliaresReversion[0] = lstr_OperacionRever; //IdOperacion
            AuxiliaresReversion[1] = null; //CodigoAuxiliar1
            AuxiliaresReversion[2] = lstr_Nemotecnico.Trim(); //CodigoAuxiliar2
            AuxiliaresReversion[3] = lstr_EsPublico; //CodigoAuxiliar3
            AuxiliaresReversion[4] = "ID"; //CodigoAuxiliar4
            AuxiliaresReversion[5] = esCortoPlazoRever; //CodigoAuxiliar5
            AuxiliaresReversion[6] = "INT_DEV"; //CodigoAuxiliar6
            AuxiliaresReversion[7] = "Reversión de Devengo"; //Descripción
            AuxiliaresReversion[8] = "REVERSION_DEVENGO"; //Descripción
            AuxiliaresReversion[9] = lstr_NomOperacionRever; 
            arregloTiras.Add(AuxiliaresReversion);

            #endregion

            PrepararContabilizacion(arregloTiras, (ldec_ValorTransadoNeto - ldec_ValorTransadoBruto), ldec_TipoCambioUDE, lstr_Detalle, ldt_FchCancelacion, ldt_FchVencimientoCupon, ldec_TipoCambio, ldec_InteresBruto, montoInteresTotal, lstr_Moneda, lstr_NroValor, lstr_Nemotecnico, ldec_TipoCambioColones, ldt_FchModifica, lstr_tipo, lstr_Origen, lstr_NroCupon);

            return string.Empty;            
            
        }

        public static string PrepararContabilizacion(List<string[]> arregloTiras, decimal ldec_CuponCorrido,
            decimal ldec_TipoCambioUDE, string lstr_Detalle, DateTime ldt_FchCancelacion, DateTime ldt_FchVencimientoCupon, decimal ldec_TipoCambio, decimal ldec_InteresBruto, decimal ldec_MontoInteresTotal, string lstr_Moneda, string lstr_NroValor, string lstr_Nemotecnico, decimal ldec_TipoCambioColones, DateTime ldt_FchModifica, string lstr_tipo, string lstr_Modulo, string lstr_NroCupon)
        {
            #region Define si el título es a corto plazo y no trasciende en el periodo

            string lstr_Retorno = "";
            
            foreach (string[] auxiliares in arregloTiras)
            {
                DataTable ldat_Asiento = RegistroContable();
                DataTable ldat_Tira = new DataTable();
                bool SinError = true;
                decimal ldec_monto = 0;
                string lstr_Referencia = "";                

                ldat_Tira = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", "", "", "", "", "", "", "ID").Tables[0].Clone();

                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", auxiliares[0], null, null, auxiliares[1], auxiliares[2], auxiliares[3], auxiliares[4], auxiliares[5], auxiliares[6]).Tables[0].Select("CodigoAuxiliar3 = '" + auxiliares[2] + "'").CopyToDataTable().Rows)
                    ldat_Tira.ImportRow(dr_tira);
                
                foreach (DataRow ldr_Row in ldat_Tira.Rows)
                {
                    string operacion = ldr_Row["CodigoAuxiliar2"].ToString();
                    int index = ldat_Tira.Rows.IndexOf(ldr_Row);

                    switch (operacion.Trim().ToUpper())
                    {
                        case "GASTO":
                            {
                                string query = "select isnull(sum(isnull(a.InteresBruto,0)),0) as SumaInteres from cf.titulosvalores a inner join cf.titulosvalores b on a.nemotecnico = b.nemotecnico and a.nrovalor = b.nrovalor " +
                                               " where  b.nroemisionserie='" + aEmisionSerie + "' and a.fchvencimiento='" + ldt_FchVencimientoCupon + "' and a.NroCupon > 0 and a.tipoNegociacion='Compra'";
                                DataSet dsInteres = dinamica.ConsultarDinamico(query);
                                decimal InteresBrutoCompra = Convert.ToDecimal(dsInteres.Tables[0].Rows.Count.Equals(0) ? "0" : dsInteres.Tables[0].Rows[0]["SumaInteres"].ToString());

                                ldec_monto = lstr_Moneda.Equals("USD") ? ldec_InteresBruto -InteresBrutoCompra : (lstr_Moneda.Equals("CRC") ? ldec_InteresBruto - InteresBrutoCompra : (ldec_InteresBruto - InteresBrutoCompra)* ldec_TipoCambioUDE);
                                break;
                            }
                        case "INT_DEV":
                            {
                                if (auxiliares[8].Equals("CUPON_CORRIDO"))
                                {
                                    ldec_monto = lstr_Moneda.Equals("USD") ? ldec_CuponCorrido : (lstr_Moneda.Equals("CRC") ? ldec_CuponCorrido : ldec_CuponCorrido * ldec_TipoCambioUDE);
                                }
                                else
                                {
                                    ldec_monto = lstr_Moneda.Equals("USD") ? ldec_MontoInteresTotal : (lstr_Moneda.Equals("CRC") ? ldec_MontoInteresTotal : ldec_MontoInteresTotal * ldec_TipoCambioUDE);
                                }
                                break;
                            }
                    }

                    //ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorFacial : (lstr_Moneda.Equals("CRC") ? ldec_ValorFacial : ldec_ValorFacial * ldec_TipoCambioUDE);
                    ldec_monto = ldec_monto > 0 ? ldec_monto : ldec_monto * -1;  
                    lstr_Referencia = lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + auxiliares[7];

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

                                    ldat_Asiento.Rows.Add(
                                        lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                        ldt_FchCancelacion.ToString("dd.MM.yyyy"),
                                        ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim(),
                                        lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim(),
                                        ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim().ToUpper(),
                                        ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                        drForm["IdReserva"].ToString().Trim(),
                                        drForm["Posicion"].ToString().Trim(),
                                        Truncate(ldec_SaldoCont > reservaTpoCambio ? reservaTpoCambio : ldec_SaldoCont, 2),
                                        lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                        tira.get_operation_name(auxiliares[0], "DI"),//texto2
                                        lstr_Moneda,//tipo
                                        auxiliares[0] + "." + auxiliares[9] //operacion
                                        );
                                }

                                //Resta el saldo
                                ldec_SaldoCont = ldec_SaldoCont - (Convert.ToDecimal(drForm["Monto"].ToString()) / ldec_TipoCambio);
                                ldec_Saldo = ldec_Saldo - Convert.ToDecimal(drForm["Monto"]);
                            }
                        }
                        else
                        {
                            //Almacena en bitácora de que no lo hizo
                            bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(auxiliares[0], "DI"), "Resultado de Contabilización: \n 1 - [E] Monto superior al total de las reservas de la Deuda Interna. Reservas utilizadas: \n" + reservasError, auxiliares[0], lstr_NroValor + "-" + lstr_Nemotecnico, "G206");
                            SinError = false;
                            break;
                        }
                    }
                    else
                    {
                        if (!ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim().StartsWith("E"))
                        {
                            ldat_Asiento.Rows.Add(
                                lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                ldt_FchCancelacion.ToString("dd.MM.yyyy"),
                                ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim(),
                                lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim(),
                                ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim().ToUpper(),
                                ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                Truncate(ldec_monto, 2),
                                lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                tira.get_operation_name(auxiliares[0], "DI"), //texto2
                                lstr_Moneda,//tipo
                                auxiliares[0] + "." + auxiliares[9] //operacion
                                );
                        }
                        else
                        {
                            bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(auxiliares[0], "DI"), "Resultado de Contabilización: \n 1 - [E] No hay reservas correspondientes a la cuenta " + ldat_Tira.Rows[0]["IdCuentaContable"].ToString().Trim() + " con fondo " + ldat_Tira.Rows[0]["IdPosPre"].ToString().Trim(), auxiliares[0], lstr_NroValor + "-" + lstr_Nemotecnico, "G206");
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
                        ldat_Reservas = dinamica.ConsultarDinamico("select * from ma.reservasdetalle where idcuentacontable = '" + ldat_Tira.Rows[index]["IdCuentaContable2"].ToString().Trim() + "' and idpospre = '" + ldat_Tira.Rows[index]["IdPosPre2"].ToString().Trim() + "'  and LEFT(idprograma, 4) = year(getdate()) order by idprograma desc");
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

                            if (Convert.ToDecimal(ldec_MontoTotal) >= (ldec_monto * ldec_TipoCambio))
                            {
                                //Genera el asiento
                                decimal ldec_SaldoCont = ldec_monto;
                                decimal ldec_Saldo = (ldec_monto * ldec_TipoCambio);

                                foreach (DataRow drForm in lds_Datos.Rows)
                                {
                                    if (Convert.ToDecimal(drForm["Monto"]) > 0 && ldec_Saldo >= 0)
                                    {
                                        //lstr_Referencia = lstr_NroValor.Trim().Equals("00") ? "No Asociado" : lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Costo Transaccion";
                                        decimal reservaTpoCambio = Convert.ToDecimal(drForm["Monto"].ToString()) / ldec_TipoCambio;

                                        ldat_Asiento.Rows.Add(
                                            lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                            ldt_FchCancelacion.ToString("dd.MM.yyyy"),
                                            ldat_Tira.Rows[index]["IdCuentaContable2"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdClaveContable2"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim(),
                                            lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim(),
                                            ldat_Tira.Rows[index]["IdCentroCosto2"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdCentroBeneficio2"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdElementoPEP2"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdPosPre2"].ToString().Trim().ToUpper(),
                                            ldat_Tira.Rows[index]["IdCentroGestor2"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdFondo2"].ToString().Trim(),
                                            drForm["IdReserva"].ToString().Trim(),
                                            drForm["Posicion"].ToString().Trim(),
                                            Truncate(ldec_SaldoCont > reservaTpoCambio ? reservaTpoCambio : ldec_SaldoCont, 2),
                                            lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                            tira.get_operation_name(auxiliares[0], "DI"), //texto2
                                            lstr_Moneda,//tipo
                                            auxiliares[0] + "." + auxiliares[9] //operacion
                                            );
                                    }

                                    //Resta el saldo
                                    ldec_SaldoCont = ldec_SaldoCont - (Convert.ToDecimal(drForm["Monto"].ToString()) / ldec_TipoCambio);
                                    ldec_Saldo = ldec_Saldo - Convert.ToDecimal(drForm["Monto"]);
                                }
                            }
                            else
                            {
                                //Almacena en bitácora de que no lo hizo
                                bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(auxiliares[0], "DI"), "Resultado de Contabilización: \n 1 - [E] Monto superior al total de las reservas de la Deuda Interna. Reservas utilizadas: \n" + reservasError, auxiliares[0], lstr_NroValor + "-" + lstr_Nemotecnico, "G206");
                                SinError = false;
                                break;
                            }
                        }
                        else
                        {
                            if (ldat_Tira.Rows[index]["IdCentroBeneficio2"].ToString().Trim().Equals(""))
                            {
                                ldat_Asiento.Rows.Add(
                                    lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                    ldt_FchCancelacion.ToString("dd.MM.yyyy"),
                                    ldat_Tira.Rows[index]["IdCuentaContable2"].ToString().Trim(),
                                    ldat_Tira.Rows[index]["IdClaveContable2"].ToString().Trim(),
                                    ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim(),
                                    lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim(),
                                    ldat_Tira.Rows[index]["IdCentroCosto2"].ToString().Trim(),
                                    ldat_Tira.Rows[index]["IdCentroBeneficio2"].ToString().Trim(),
                                    ldat_Tira.Rows[index]["IdElementoPEP2"].ToString().Trim(),
                                    ldat_Tira.Rows[index]["IdPosPre2"].ToString().Trim().ToUpper(),
                                    ldat_Tira.Rows[index]["IdCentroGestor2"].ToString().Trim(),
                                    ldat_Tira.Rows[index]["IdFondo2"].ToString().Trim(),
                                    ldat_Tira.Rows[index]["DocPresupuestario2"].ToString().Trim(),
                                    ldat_Tira.Rows[index]["PosDocPresupuestario2"].ToString().Trim(),
                                    Truncate(ldec_monto, 2),
                                    lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                    tira.get_operation_name(auxiliares[0], "DI"), //texto2
                                    lstr_Moneda,//tipo
                                    auxiliares[0] + "." + auxiliares[9]//operacion
                                    );
                            }
                            else
                            {
                                bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(auxiliares[0], "DI"), "Resultado de Contabilización: \n 1 - [E] No hay reservas correspondientes a la cuenta " + ldat_Tira.Rows[index]["IdCuentaContable2"].ToString().Trim() + " con fondo " + ldat_Tira.Rows[index]["IdPosPre2"].ToString().Trim(), auxiliares[0], lstr_NroValor + "-" + lstr_Nemotecnico, "G206");
                                SinError = false;
                                break;
                            }
                        }
                    }
                    #endregion
                }
            #endregion

                if (SinError)
                {
                    //ImprimeAsientos(ldat_Asiento); //usada en pruebas
                    if(ldec_monto.Equals(0))
                        bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(auxiliares[0], "DI"), "Resultado de Contabilización: \n 1 - [I] El monto enviado a contabilizar es cero, no se generó documento.", auxiliares[0], lstr_NroValor + "-" + lstr_Nemotecnico, "G206");
                    else
                        lstr_Retorno = GenerarAsientoAjuste(auxiliares[7], ldat_Asiento, auxiliares[0], lstr_NroValor, lstr_Nemotecnico, (lstr_Moneda.Equals("USD") ? ldec_TipoCambioColones : lstr_Moneda.Equals("UDE") ? ldec_TipoCambioUDE : 1), ldt_FchVencimientoCupon, ldt_FchModifica, 0, lstr_tipo, lstr_Moneda, auxiliares[8], lstr_Modulo, ldt_FchCancelacion, lstr_NroCupon);
                }
                else
                {
                    lstr_Retorno = string.Empty;
                }
            }
            return lstr_Retorno;
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


        public static string GenerarAsientoAjuste(string lstr_TipoCancelacion, DataTable ldat_Asiento, string lstr_IdOperacion, string lstr_NroValor, string lstr_Nemotecnico, decimal ldec_TipoCambio, DateTime ldt_FchVencimientoCupon, DateTime ldt_FchModifica,
            int NroAsiento, string Tipo, string Moneda, string tipoOperacion, string lstr_Modulo, DateTime ldt_FchCancelacion, string lstr_NroCupon)
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
                    if (ldat_Asiento.Rows[index]["Moneda"].ToString() == "USD")
                        item_asiento.Kursf = Convert.ToDecimal(ldec_TipoCambio.ToString("0.0000"));

                    item_asiento.Xblnr = ldat_Asiento.Rows[index]["PKMovimiento"].ToString();//
                    item_asiento.Bktxt = ldat_Asiento.Rows[index]["Texto2"].ToString();//
                    item_asiento.Xref1Hd = ldat_Asiento.Rows[index]["Ref1Tipo"].ToString();//
                    item_asiento.Xref2Hd = ldat_Asiento.Rows[index]["Ref2Operacion"].ToString();//
                    tabla_asientos[index] = item_asiento;
                }
                decimal montoContabilizado = Convert.ToDecimal(Convert.ToDecimal(ldat_Asiento.Rows[0]["Monto"].ToString()).ToString("0.0000"));
                string cuentacont = ldat_Asiento.Rows[0]["Cuenta"].ToString();

                //Cargar de Asientos 
                string[] concatenado = new string[8];
                //envio de asiento mediante servicio web hacia SIGAF
                item_resAsientosLog = tasientos.EnviarAsientos(tabla_asientos, "");
                for (int j = 0; j < item_resAsientosLog.Length; j++)
                {
                    int x = j + 1;
                    logAsiento += x + " - " + item_resAsientosLog[j] + " \n ";
                }
                //Registrar en Bitacora de movimientos
                bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_IdOperacion, "DI"), "Resultado de Contabilización: \n" + logAsiento + "\nFecha de Vencimiento: " + ldt_FchVencimientoCupon.ToString("dd/MM/yyyy"), lstr_IdOperacion, lstr_NroValor + "-" + lstr_Nemotecnico, "");

                string[] a = new string[2];
                if (!logAsiento.Contains("[E]"))
                {
                    string dinam = string.Empty;
                    
                     dinam = "INSERT INTO cf.CuponesPagados" +
                            "(IdCuponPagado, NroValor, Nemotecnico, NroAsiento, FchContable, Monto) " +
                            "VALUES (" + lstr_NroCupon + ", " + lstr_NroValor + ", '" + lstr_Nemotecnico + "', '" + item_resAsientosLog[0] + "', '" + ldt_FchCancelacion.ToString("yyyy-MM-dd") + "', " + montoContabilizado.ToString().Replace(",", ".") + ")";

                      
                   /* dinam = "INSERT INTO cf.PagosCupones" +
                            "(IdPagoCupon, NroValor, Nemotecnico, NroAsiento, FchContabilizacion, MontoPagado) " +
                            "VALUES (" + lstr_NroCupon + ", " + lstr_NroValor + ", '" + lstr_Nemotecnico + "', '" + item_resAsientosLog[0] + "', '" + ldt_FchCancelacion.ToString("yyyy-MM-dd") + "', " + montoContabilizado.ToString().Replace(",", ".") + ")";
                    */
                    dinamica.ConsultarDinamico(dinam);

                    if (tipoOperacion.Equals("REVERSION_DEVENGO"))
                    {
                        dinam = "INSERT INTO cf.ReversionesDevengo (NroValor,Nemotecnico,NroAsiento,Tipo,Moneda,MontoRev,MontoRevCol) VALUES (" + lstr_NroValor + ", '" + lstr_Nemotecnico + "', '" + item_resAsientosLog[0] + "', '" + cuentacont+"|"+Tipo + "', '" + (Moneda.Equals("CRC") ? "CRCN" : Moneda) + "', " + Math.Abs(montoContabilizado).ToString().Replace(",", ".") + ", " + (Math.Abs(montoContabilizado) * ldec_TipoCambio).ToString().Replace(",", ".") + ")";
                        dinamica.ConsultarDinamico(dinam);
                    }
                    else
                    {
                        dinam = "INSERT INTO cf.CuponesPagados (NroValor,Nemotecnico,NroAsiento,NroCupon,FchContable,Monto,Modulo) values (" + lstr_NroValor + ",'" + lstr_Nemotecnico + "','" + item_resAsientosLog[0] + "'," + lstr_NroCupon + ",'" + ldt_FchCancelacion.ToString("yyyy-MM-dd") + "'," + montoContabilizado.ToString().Replace(",", ".") + ",'" + lstr_Modulo + "')";
                        dinamica.ConsultarDinamico(dinam);
                    }
                }

                //    lcls_CostoTransaccion.ContabilizarCalculosFinancieros(
                //        "TitulosValores",
                //        null,
                //        lstr_NroValor.Trim(),
                //        lstr_Nemotecnico.Trim(),
                //        "PGD",
                //        "SG",
                //        ldt_FchModifica, out a[0], out a[1]);
                return logAsiento;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public static void ImprimeAsientos(DataTable tbl)
        {

            string refe = tbl.Rows[0]["Referencia"].ToString().Split(' ')[0];
             string consulta = "";
                foreach (DataRow fila in tbl.Rows) 
                {
                    consulta = "insert into [dbo].[AsientosPrueba] values ( 'Cupon',convert(date,'" + tbl.Rows[0]["Fecha"] + "',103),'" + refe.Split('-')[1] + "'," + refe.Split('-')[0] + ", '" +
                        fila["Cuenta"].ToString() +"',"+fila["ClaveContable"].ToString()+","+fila["Monto"].ToString()+",'"+ fila["PosPre"].ToString()+"','"+fila["CentroGestor"].ToString()+"')";

                    dinamica.ConsultarDinamico(consulta);
                }
         

        }
    }

}