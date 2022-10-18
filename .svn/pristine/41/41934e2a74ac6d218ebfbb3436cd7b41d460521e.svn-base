using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using LogicaNegocio.Mantenimiento;
using LogicaNegocio.Seguridad;

namespace LogicaNegocio.CalculosFinancieros.DeudaInterna
{
    public class reclasificacionPlazos
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

        public string reclasifica_plazos(DateTime? lstr_FchInicio = null, DateTime? lstr_FchFin = null, int? lint_NroValor = null, string lstr_Nemotecnico = null)
        {
            int cont = 0;
            string lstr_Mensaje = string.Empty;
            try
            {
                
                //DataTable ldat_TitulosValores = titulo.ConsultarTituloValor("%", "%", "%", "%", "%", "01/01/1900", "01/01/5000").Tables[0].Select("IndicadorCupon ='V'").CopyToDataTable();
                //Consultar titulos
                //TODO: Hay que cambiar el filtro de la fecha FchCancelacion por FchVencimiento
                DateTime? _fchInicio = lstr_FchInicio == null ? DateTime.Today.AddYears(1) : lstr_FchInicio.Value.AddYears(1);
                DateTime? _fchFin = lstr_FchFin == null ? DateTime.Today.AddYears(1) : lstr_FchFin.Value.AddYears(1);

                string query = "select * from cf.titulosvalores where indicadorcupon = 'V' and (nrovalor = '"+lint_NroValor+"' or isnull('"+lint_NroValor+"','') ='') and (nemotecnico = '"+lstr_Nemotecnico+"' or isnull('"+lstr_Nemotecnico+"','') ='') and FchVencimiento between '" + _fchInicio.Value.ToString("yyyy-MM-dd") + "' and '" + _fchFin.Value.ToString("yyyy-MM-dd") + "' and estadovalor = 'Vigente'";

                DataSet totalTitulosValores = dinamica.ConsultarDinamico(query);
                DataTable ldat_TitulosValores = totalTitulosValores.Tables[0];
                DataTable ldat_Nemotecnicos = nemotecnico.ConsultarNemotecnicos(null, null, null, null, null).Tables[0];

                if (ldat_TitulosValores.Rows.Count == 0)
                {
                    lstr_Mensaje = "No hay titulos para reclasificar";
                }
                else 
                {
                    for (int i = 0; i < ldat_TitulosValores.Rows.Count; i++)
                    {
                        try
                        {
                            string lstr_moneda = ldat_Nemotecnicos.Select("IdNemotecnico = '" + ldat_TitulosValores.Rows[i]["NemoTecnico"].ToString() + "'")[0]["IdMoneda"].ToString().Trim().Equals("CRCN")
                               ? "CRC" : ldat_Nemotecnicos.Select("IdNemotecnico = '" + ldat_TitulosValores.Rows[i]["NemoTecnico"].ToString() + "'")[0]["IdMoneda"].ToString().Trim();

                            if (ldat_Nemotecnicos.Select("IdNemotecnico = '" + ldat_TitulosValores.Rows[i]["NemoTecnico"].ToString() + "'")[0]["Estado"].ToString().Trim() == "A"
                                && lstr_moneda == (ldat_TitulosValores.Rows[i]["Moneda"].ToString().Equals("CRCN") ? "CRC" : ldat_TitulosValores.Rows[i]["Moneda"].ToString()))
                            {
                                cont++;
                                lstr_Mensaje = ReclasificarTituloValor(
                                        ldat_TitulosValores.Rows[i]["Tipo"].ToString(),
                                        ldat_TitulosValores.Rows[i]["EstadoValor"].ToString(),
                                    //_fchFin.Value.AddYears(-1),
                                        Convert.ToDateTime(ldat_TitulosValores.Rows[i]["FchVencimiento"].ToString()).AddYears(-1),
                                        _fchFin.Value.AddYears(-1),
                                        ldat_TitulosValores.Rows[i]["Propietario"].ToString(),
                                        ldat_TitulosValores.Rows[i]["PlazoValor"].ToString(),

                                        Convert.ToDecimal(ldat_TitulosValores.Rows[i]["ValorTransadoBruto"].ToString()),
                                        ldat_TitulosValores.Rows[i]["Moneda"].ToString().Equals("CRCN") ? "CRC" : ldat_TitulosValores.Rows[i]["Moneda"].ToString(),

                                        ldat_TitulosValores.Rows[i]["NroValor"].ToString(),
                                        ldat_TitulosValores.Rows[i]["NemoTecnico"].ToString(),
                                        Convert.ToDecimal(ldat_TitulosValores.Rows[i]["ValorFacial"].ToString()),
                                        Convert.ToDecimal(ldat_TitulosValores.Rows[i]["ValorTransadoNeto"].ToString()),

                                        "SINPE: " + ldat_TitulosValores.Rows[i]["ModuloSINPE"].ToString().Trim() + "-" + "T.B: " +
                                        ldat_TitulosValores.Rows[i]["TasaBruta"].ToString().Trim() + "-" + "T.N: " +
                                        ldat_TitulosValores.Rows[i]["TasaNeta"].ToString().Trim() + "-" + "Plazo: " +
                                        ldat_TitulosValores.Rows[i]["PlazoValor"].ToString().Trim(),

                                        ldat_TitulosValores.Rows[i]["ModuloSINPE"].ToString().Trim(),

                                        //añadidos luego
                                        Convert.ToDateTime(ldat_TitulosValores.Rows[i]["FchValor"].ToString()),
                                        Convert.ToDateTime(ldat_TitulosValores.Rows[i]["FchCancelacion"].ToString()),
                                        ldat_TitulosValores.Rows[i]["SistemaNegociacion"].ToString(),

                                        Convert.ToDateTime(ldat_TitulosValores.Rows[i]["FchModifica"].ToString()));
                            }
                        }
                        catch (Exception ex)
                        {
                            //TODO: Insertar mensaje de error
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lstr_Mensaje = ex.ToString();
            }
            cont = 0;
            return lstr_Mensaje;
        }

        private decimal Truncate(decimal value, int length)
        {
            return Math.Truncate(value * 100) / 100;
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
                    vPorcentaje += Decimal.Parse(porcentaje[0].ToString());
                }
                  vPorcentaje = 1 - vPorcentaje;
            }
            catch (Exception ex)
            {

            }
            return vPorcentaje;
        }

        //TODO: Ajustar parametros, copiar metodo registrocontable
        public string ReclasificarTituloValor(
            string lstr_Tipo,
            string lstr_EstadoValor,
            //DateTime ldt_FchValor,
            DateTime ldt_FchVencimiento,
            DateTime ldt_FchCierre,
            //DateTime ldt_FchCancelacion,
            string lstr_Propietario,

            string lstr_Plazo,

            decimal ldec_ValorTransadoBruto,
            string lstr_Moneda,
            string lstr_NroValor,
            string lstr_Nemotecnico,
            decimal ldec_ValorFacial,
            //decimal ldec_RendimientoXDescuento,//descuento
            //decimal ldec_ImpuestoPagado,
            decimal ldec_ValorTransadoNeto,
            //decimal ldec_Premio,//prima
            string lstr_Detalle,
            string lstr_Origen,

            DateTime ldt_FchValor,
            DateTime ldt_FchCancelacion,
            string lstr_SistemaNegociacion,

            DateTime ldt_FchModifica)
        {
            string lstr_Mensaje = string.Empty;
            wsDeudaInterna.wsDeudaInterna wsDeudaInterna = new wsDeudaInterna.wsDeudaInterna();
            DataTable ldat_Asiento = RegistroContable();
            DataTable ldat_Tira = new DataTable();
            string lint_EsPublico = "PUBLICO";
            string lstr_Operacion = string.Empty;
            string lstr_NomOperacion = string.Empty;
            decimal ldec_TipoCambioColones = Convert.ToDecimal(tipocambio.ConsultarTiposCambio("CRCN", ldt_FchVencimiento, "3140", "N").Tables[0].Rows[0]["Valor"].ToString());
            decimal ldec_TipoCambioUDE = Convert.ToDecimal(tipocambio.ConsultarTiposCambio("UDE", ldt_FchVencimiento, "", "N").Tables[0].Rows[0]["Valor"].ToString());
            decimal ldec_monto = 0;
            string lstr_Referencia = "";
            string lstr_PrimaDescuento = "PRIMAS";

            decimal vPorcentajeCanje = get_canje_porcentaje(lstr_Nemotecnico, lstr_NroValor);
            ldec_ValorFacial = ldec_ValorFacial * vPorcentajeCanje;
            ldec_ValorTransadoBruto = ldec_ValorTransadoBruto * vPorcentajeCanje;
            ldec_ValorTransadoNeto = ldec_ValorTransadoNeto * vPorcentajeCanje;

            //Boolean esCortoPlazo = Convert.ToDecimal(lstr_Plazo) <= 360 ? true : false;
            
            //Define si el propietario es público o privado
            if (propietario.ConsultarPropietarios(null, null, null, lstr_Propietario, "S").Tables[0].Rows.Count == 0)
            {
                lint_EsPublico = "PRIVADO";
            }

            //Define si es prima o descuento
            if (ldec_ValorFacial >= ldec_ValorTransadoBruto)
            {
                lstr_PrimaDescuento = "IMP_DEV";
            }

            decimal ldec_TipoCambio = lstr_Moneda.Equals("USD") ? ldec_TipoCambioColones : (lstr_Moneda.Equals("CRC") ? 1 : ldec_TipoCambioUDE);

            bool SinError = true;

            #region Define si el título es a corto plazo y no trasciende en el periodo


            #region extrae las fechas para reclasificar los cupones vigentes
            
            string lstr_FechasVigentes = string.Empty;

            if (lstr_Tipo.Equals("Cero Cupón"))
            {
                lstr_FechasVigentes = "select min(fchinicio) as FchInicio from cf.titulosvalores where nrovalor = " + lstr_NroValor + " and nemotecnico = '" + lstr_Nemotecnico + "' and estadovalor = 'Vigente'";
            }
            else
            {
                lstr_FechasVigentes = "select min(fchinicio) as FchInicio from cf.titulosvalores where nrovalor = " + lstr_NroValor + " and nemotecnico = '" + lstr_Nemotecnico + "' and estadovalor = 'Vigente' and indicadorcupon = 'C'";
            }

            DataSet lds_Fechas = dinamica.ConsultarDinamico(lstr_FechasVigentes);

            DateTime ldt_FchInicioCupon = Convert.ToDateTime(lds_Fechas.Tables[0].Rows[0]["FchInicio"].ToString());
            //DateTime ldt_FchFinCupon = Convert.ToDateTime(lds_Fechas.Tables[0].Rows[0]["FchFin"].ToString());

            #endregion

            #region suma la columna de cupon y descuento
            
            decimal primaDescuento = 0, cupon = 0, interes = 0, cuponInteres = 0;

            string lstr_PrimaDesc = "select isnull(sum(isnull(descuento,0)),0) as Descuento from cf.devengosmensuales where nrovalor = " + lstr_NroValor + " and nemotecnico = '" + lstr_Nemotecnico + "' and convert(date,periodo,103) <= '" + ldt_FchVencimiento.ToString("yyyy-MM-dd") + "'";
            DataSet lds_PrimaDescuento = dinamica.ConsultarDinamico(lstr_PrimaDesc);
            
            if (lds_PrimaDescuento.Tables.Count > 0 && lds_PrimaDescuento.Tables[0].Rows.Count > 0)
            {
                primaDescuento = lds_PrimaDescuento.Tables[0].Rows[0]["Descuento"] == null ? 0 : (decimal)lds_PrimaDescuento.Tables[0].Rows[0]["Descuento"];
            }

            if (!lstr_Tipo.Equals("Cero Cupón"))
            {
                string lstr_Cupon = "select isnull(sum(isnull(cupon,0)),0) as Cupon from cf.devengosmensuales where nrovalor = " + lstr_NroValor + " and nemotecnico = '" + lstr_Nemotecnico + "' and convert(date,periodo,103) <= '" + ldt_FchVencimiento.ToString("yyyy-MM-dd") + "'";
                string lstr_Interes = "select isnull(sum(isnull(cupon,0)),0) as InteresTotal from cf.devengosmensuales where nrovalor = " + lstr_NroValor + " and nemotecnico = '" + lstr_Nemotecnico + "' and convert(date,periodo,103) <= '" + ldt_FchInicioCupon.ToString("yyyy-MM-dd") + "'";

                DataSet lds_Cupon = dinamica.ConsultarDinamico(lstr_Cupon);
                DataSet lds_Interes = dinamica.ConsultarDinamico(lstr_Interes);
                
                if (lds_Cupon.Tables.Count > 0 && lds_Cupon.Tables[0].Rows.Count > 0)
                {
                    cupon = lds_Cupon.Tables[0].Rows[0]["Cupon"] == null ? 0 : (decimal)lds_Cupon.Tables[0].Rows[0]["Cupon"];
                }

                if (lds_Interes.Tables.Count > 0 && lds_Interes.Tables[0].Rows.Count > 0)
                {
                    interes = lds_Interes.Tables[0].Rows[0]["InteresTotal"] == null ? 0 : (decimal)lds_Interes.Tables[0].Rows[0]["InteresTotal"];
                }
                
                cuponInteres = cupon - interes;
            }
            #endregion


            //TODO: quitar if
            if (true)
            {
                lstr_Operacion = lstr_Moneda.Equals("USD") ? "ID73" : "ID74";

                DataSet lds_Operaciones = loperacion.ConsultarOperaciones(lstr_Operacion, "IdModulo IN ('DI')", "");
                if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
                {
                    lstr_NomOperacion = lds_Operaciones.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
                }

                ldat_Tira = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID").Tables[0].Clone();
                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, null, null, null, lstr_Nemotecnico, lint_EsPublico, "ID", "LP", "CAPITAL").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico + "'").CopyToDataTable().Rows)
                    ldat_Tira.ImportRow(dr_tira);

                if (lstr_Tipo.Equals("Cero Cupón"))
                {
                    foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, null, null, null, lstr_Nemotecnico, lint_EsPublico, "ID", "LP", lstr_PrimaDescuento).Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico.Trim() + "'").CopyToDataTable().Rows)
                        ldat_Tira.ImportRow(dr_tira);
                }
                else
                {
                    foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, null, null, null, lstr_Nemotecnico, lint_EsPublico, "ID", "LP", lstr_PrimaDescuento).Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico.Trim() + "'").CopyToDataTable().Rows)
                        ldat_Tira.ImportRow(dr_tira);
                    foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, null, null, null, lstr_Nemotecnico, lint_EsPublico, "ID", "LP", "INT_DEV").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico.Trim() + "'").CopyToDataTable().Rows)
                        ldat_Tira.ImportRow(dr_tira);
                }

                foreach (DataRow ldr_Row in ldat_Tira.Rows)
                {
                    string operacion = ldr_Row["CodigoAuxiliar2"].ToString();

                    int index = ldat_Tira.Rows.IndexOf(ldr_Row);

                    decimal CuponCorrido = Math.Abs(ldec_ValorTransadoNeto - ldec_ValorTransadoBruto);
                    decimal PrimaDescuentoTitulo = Math.Abs(ldec_ValorTransadoBruto - ldec_ValorFacial);
                    decimal Prima = (primaDescuento + PrimaDescuentoTitulo + CuponCorrido);
                    decimal ImpDev = (primaDescuento - PrimaDescuentoTitulo + CuponCorrido);
                    decimal IntDev = (cuponInteres + CuponCorrido);

                    switch (operacion.Trim().ToUpper())
                    {
                        case "CAPITAL": { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorFacial : (lstr_Moneda.Equals("CRC") ? ldec_ValorFacial : ldec_ValorFacial * ldec_TipoCambioUDE); break; }

                        case "PRIMAS": { ldec_monto = lstr_Moneda.Equals("USD") ? Prima : (lstr_Moneda.Equals("CRC") ? Prima : Prima * ldec_TipoCambioUDE); break; }

                        case "IMP_DEV": { ldec_monto = lstr_Moneda.Equals("USD") ? ImpDev : (lstr_Moneda.Equals("CRC") ? ImpDev : ImpDev * ldec_TipoCambioUDE); break; }

                        case "INT_DEV": { ldec_monto = 0; break; }//{ ldec_monto = lstr_Moneda.Equals("USD") ? IntDev : (lstr_Moneda.Equals("CRC") ? IntDev : IntDev * ldec_TipoCambioUDE); break; }
                    }

                    lstr_Referencia = lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Reclasificación";

                    SinError = true;

                    #region pospre debe
                    //Validar que el pos pre sea diferente de PP_Balance y que el monto sea mayor al seleccionado
                    string ClaveContableDebe = "";
                    string ClaveContableHaber = "";

                    string lstr_Monto = string.Empty;
                    DataTable lds_Datos = new DataTable();
                    decimal ldec_MontoTotal = 0;
                    string reservasError = "";
                    string lstr_NuevoPosPrePago = string.Empty;
                    DataSet ldat_Reservas = new DataSet();

                    if (operacion.Trim().Equals("IMP_DEV") || operacion.Trim().Equals("PRIMAS"))
                    {
                        if (ldec_monto > 0)
                        { ClaveContableDebe = "40"; ClaveContableHaber = "50"; }
                        else 
                        { ClaveContableDebe = "50"; ClaveContableHaber = "40"; }
                    }
                    else if (operacion.Trim().Equals("INT_DEV"))
                    {
                        if (ldec_monto > 0)
                        { ClaveContableDebe = "50"; ClaveContableHaber = "40"; }
                        else
                        { ClaveContableDebe = "40"; ClaveContableHaber = "50"; }
                    }
                    else
                    {
                        ClaveContableDebe = ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim();
                        ClaveContableHaber = ldat_Tira.Rows[index]["IdClaveContable2"].ToString().Trim();
                    }

                    ldec_monto = Math.Abs(ldec_monto);

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

                        if (Convert.ToDecimal(ldec_MontoTotal) >= (lstr_Moneda.Equals("USD") ? (ldec_monto * ldec_TipoCambio) : ldec_monto))
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
                                        ldt_FchVencimiento.ToString("dd.MM.yyyy"),
                                        ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                        //ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                        ClaveContableDebe,
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
                                        lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                        tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                        lstr_Moneda,//tipo
                                        lstr_Operacion +"."+ lstr_NomOperacion//operacion
                                        );
                                }

                                //Resta el saldo    
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
                        if (!ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim().StartsWith("E") )
                        {
                            if (Math.Abs(ldec_monto) > 0)
                            {
                                ldat_Asiento.Rows.Add(
                                    lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                    ldt_FchVencimiento.ToString("dd.MM.yyyy"),
                                    ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                    //ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                    ClaveContableDebe,
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
                                    lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                    tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                    lstr_Moneda,//tipo
                                    lstr_Operacion + "." + lstr_NomOperacion//operacion
                                    );
                            }
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
                                decimal ldec_Saldo = ldec_monto;

                                foreach (DataRow drForm in lds_Datos.Rows)
                                {
                                    if (Convert.ToDecimal(drForm["Monto"]) > 0 && ldec_Saldo >= 0)
                                    {
                                        //lstr_Referencia = lstr_NroValor.Trim().Equals("00") ? "No Asociado" : lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Costo Transaccion";

                                        ldat_Asiento.Rows.Add(
                                            lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                            ldt_FchVencimiento.ToString("dd.MM.yyyy"),
                                            ldat_Tira.Rows[index]["IdCuentaContable2"].ToString().Trim(),
                                            //ldat_Tira.Rows[index]["IdClaveContable2"].ToString().Trim(),
                                            ClaveContableHaber,
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
                                            lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                            tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                            lstr_Moneda,//tipo
                                            lstr_Operacion +"."+ lstr_NomOperacion//operacion
                                            );
                                    }

                                    //Resta el saldo    
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
                                if (Math.Abs(ldec_monto) > 0)
                                {
                                    ldat_Asiento.Rows.Add(
                                        lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                        ldt_FchVencimiento.ToString("dd.MM.yyyy"),
                                        ldat_Tira.Rows[index]["IdCuentaContable2"].ToString().Trim(),
                                        //ldat_Tira.Rows[index]["IdClaveContable2"].ToString().Trim(),
                                        ClaveContableHaber,
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
                                        lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                        tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                        lstr_Moneda,//tipo
                                        lstr_Operacion + "." + lstr_NomOperacion //operacion
                                        );
                                }
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

            if (SinError)
            {
                lstr_Mensaje = GenerarAsientoAjuste("Reclasificación LP/CP", ldat_Asiento, lstr_Operacion, lstr_NroValor, lstr_Nemotecnico, lstr_Moneda, ldec_TipoCambio, ldt_FchModifica, ldt_FchCierre, ldt_FchValor, ldt_FchCancelacion, ldt_FchVencimiento, lstr_SistemaNegociacion, lstr_Tipo, ldec_ValorFacial, ldec_ValorTransadoBruto, ldec_ValorTransadoNeto);
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


        public static string GenerarAsientoAjuste(string lstr_TipoCancelacion, DataTable ldat_Asiento, string lstr_IdOperacion, string lstr_NroValor, string lstr_Nemotecnico, string lstr_Moneda, decimal ldec_TipoCambio, DateTime ldt_FchModifica, DateTime ldt_FchCierre, DateTime FchValor, DateTime FchCancelacion, DateTime FchVencimiento, string SistemaNegociacion, string lstr_Tipo, decimal ldec_ValorFacial, decimal ldec_ValorTransadoBruto, decimal ldec_ValorTransadoNeto)
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
                        item_asiento.Bldat = ldt_FchCierre.ToString("dd.MM.yyyy");//Fecha de documento
                        item_asiento.Budat = ldt_FchCierre.ToString("dd.MM.yyyy");//Fecha de contabilización
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
                    logAsiento += x + " - " + item_resAsientosLog[j] + " \n ";
                }
                //Registrar en Bitacora de movimientos
                bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_IdOperacion, "DI"), "Resultado de Contabilización: \n" + logAsiento, lstr_IdOperacion, lstr_NroValor + "-" + lstr_Nemotecnico, "");

                string[] a = new string[2];
                if (!logAsiento.Contains("[E]"))
                {
                    string lstr_dinami = "insert into cf.TituloReclasificado (NroValor, Nemotecnico, NroAsiento, Tipo, Moneda, TipoCambio, ValorFacial, ValorTranBruto, ValorTransNeto, FchValor, FchCancelacion, FchVencimiento, SistemaNegocia) values (" +
                    "" + lstr_NroValor + ", '" +
                    lstr_Nemotecnico + "', " +
                    item_resAsientosLog[0] + ", '" +
                    lstr_Tipo + "', '" +
                    (lstr_Moneda.Equals("CRC") ? "CRCN" : lstr_Moneda) + "', " +
                    ldec_TipoCambio.ToString().Replace(",", ".") + ", " +
                    ldec_ValorFacial.ToString().Replace(",", ".") + ", " +
                    ldec_ValorTransadoBruto.ToString().Replace(",", ".") + ", " +
                    ldec_ValorTransadoNeto.ToString().Replace(",", ".") + ", '" +
                    FchValor.ToString("yyyy-MM-dd") + "', '" +
                    FchCancelacion.ToString("yyyy-MM-dd") + "', '" +
                    FchVencimiento.ToString("yyyy-MM-dd") + "', '" +
                    SistemaNegociacion + "')";

                    //bitacora.ufnRegistrarAccionBitacora("DI", "123", lstr_TipoCancelacion, lstr_dinami, lstr_IdOperacion, lstr_NroValor + "-" + lstr_Nemotecnico, "");

                    dinamica.ConsultarDinamico(lstr_dinami);
                }
                    //lcls_CostoTransaccion.ContabilizarCalculosFinancieros(
                    //    "TitulosValores",
                    //    null,
                    //    lstr_NroValor.Trim(),
                    //    lstr_Nemotecnico.Trim(),
                    //    "CAN",
                    //    "SG",
                    //    ldt_FchModifica, out a[0], out a[1]);
                return logAsiento;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}