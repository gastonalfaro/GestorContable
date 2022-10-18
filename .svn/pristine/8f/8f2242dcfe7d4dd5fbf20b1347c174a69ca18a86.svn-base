using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using LogicaNegocio.Mantenimiento;
using LogicaNegocio.Seguridad;

namespace LogicaNegocio.CalculosFinancieros.DeudaInterna
{
    public class clsReclasificacionLPCP
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

        public string Cancelacion(string lstr_FchInicio, string lstr_FchFin)
        {
            string lstr_Mensaje = string.Empty;
            try
            {
                DataTable ldat_TitulosValores = titulo.ConsultarTituloValor(null, String.Empty, String.Empty, String.Empty, "V", String.Empty, String.Empty, String.Empty, Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/5000"),string.Empty).Tables[0];

                for (int i = 0; i < ldat_TitulosValores.Rows.Count; i++)
                {
                    try
                    {
                        lstr_Mensaje = ReclasificarTituloValor(
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
                            Convert.ToDecimal(ldat_TitulosValores.Rows[i]["ValorFacial"].ToString()),
                            Convert.ToDecimal(ldat_TitulosValores.Rows[i]["RendimientoPorDescuento"].ToString()),
                            Convert.ToDecimal(ldat_TitulosValores.Rows[i]["ImpuestoPagado"].ToString()),
                            Convert.ToDecimal(ldat_TitulosValores.Rows[i]["ValorTransadoNeto"].ToString()),
                            Convert.ToDecimal(ldat_TitulosValores.Rows[i]["Premio"].ToString()),

                            "SINPE: " + ldat_TitulosValores.Rows[i]["ModuloSINPE"].ToString().Trim() + "-" + "T.B: " +
                            ldat_TitulosValores.Rows[i]["TasaBruta"].ToString().Trim() + "-" + "T.N: " +
                            ldat_TitulosValores.Rows[i]["TasaNeta"].ToString().Trim() + "-" + "Plazo: " +
                            ldat_TitulosValores.Rows[i]["PlazoValor"].ToString().Trim(),

                            Convert.ToDateTime(ldat_TitulosValores.Rows[i]["FchModifica"].ToString()));
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

        public string CancelarTituloValorAnticipado(
            DateTime ldt_FchVencimiento,
            string lstr_Propietario,
            string lstr_Plazo,

            decimal ldec_ValorTransadoBruto,
            string lstr_Moneda,
            string lstr_NroValor,
            string lstr_Nemotecnico,
            decimal ldec_ValorFacial,
            decimal ldec_RendimientoXDescuento,//descuento
            decimal ldec_ImpuestoPagado,
            decimal ldec_ValorTransadoNeto,
            decimal ldec_Premio,//prima
            string lstr_Detalle,
            DateTime ldt_FchModifica)
        {
            string lstr_Mensaje = string.Empty;
            wsDeudaInterna.wsDeudaInterna wsDeudaInterna = new wsDeudaInterna.wsDeudaInterna();
            DataTable ldat_Asiento = RegistroContable();
            DataTable ldat_Tira = new DataTable();
            string lint_EsPublico = "PUBLICO";
            string lstr_Operacion = string.Empty;
            string lstr_NomOperacion = string.Empty;
            decimal ldec_TipoCambioColones = Convert.ToDecimal(tipocambio.ConsultarTiposCambio("CRCN", DateTime.Today, "3140", "N").Tables[0].Rows[0]["Valor"].ToString());
            decimal ldec_TipoCambioUDE = Convert.ToDecimal(tipocambio.ConsultarTiposCambio("UDE", DateTime.Today, "", "N").Tables[0].Rows[0]["Valor"].ToString());
            decimal ldec_monto = 0;
            string lstr_Referencia = "";

            //Define si el propietario es público o privado
            if (propietario.ConsultarPropietarios(string.Empty, string.Empty, string.Empty, lstr_Propietario, "S").Tables[0].Rows.Count == 0)
            {
                lint_EsPublico = "PRIVADO";
            }

            //se tratan las cancelaciones de las tres diferentes operaciones, títulos cero cupón, de tasa fija y tasa variable.
            //if (lstr_EstadoValor == "Cancelada")
            //{
                #region cero cupon
                            
                lstr_Operacion = lstr_Moneda.Equals("USD") ? "ID73" : (lstr_Moneda.Equals("CRC") ? "ID74" : "ID74");

                DataSet lds_Operaciones = loperacion.ConsultarOperaciones(lstr_Operacion, "IdModulo IN ('DI')", "");
                if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
                {
                    lstr_NomOperacion = lds_Operaciones.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
                }

                ldat_Tira = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID").Tables[0].Clone();
                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "LP", "CAPITAL").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico + "'").CopyToDataTable().Rows)
                    ldat_Tira.ImportRow(dr_tira);
                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "LP", "IMP_DEV").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico + "'").CopyToDataTable().Rows)
                    ldat_Tira.ImportRow(dr_tira);
                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "LP", "INT_DEV").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico + "'").CopyToDataTable().Rows)
                    ldat_Tira.ImportRow(dr_tira);

                foreach (DataRow ldr_Row in ldat_Tira.Rows)
                {
                    int index = ldat_Tira.Rows.IndexOf(ldr_Row);

                    switch (index)
                    {
                        case 0:
                            {
                                ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorFacial : (lstr_Moneda.Equals("CRC") ? ldec_ValorFacial : ldec_ValorFacial * ldec_TipoCambioUDE); break;
                            }
                        case 1:
                            {
                                ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorFacial : (lstr_Moneda.Equals("CRC") ? ldec_ValorFacial : ldec_ValorFacial * ldec_TipoCambioUDE); break;
                            }
                        case 2:
                            {
                                ldec_monto = 0; break;// lstr_Moneda.Equals("USD") ? ldec_ValorFacial : (lstr_Moneda.Equals("CRC") ? ldec_ValorFacial : ldec_ValorFacial * ldec_TipoCambioUDE); break;
                            }
                    }

                    lstr_Referencia = lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Reclasificacion LP-CP";

                    //ldat_Asiento.Rows.Add(
                    //    lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                    //    ldt_FchCancelacion.ToString("dd.MM.yyyy"),
                    //    ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                    //    ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                    //    ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                    //    lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim(),
                    //    ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                    //    ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                    //    ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                    //    lstr_NuevoPosPreDevengo.Equals(string.Empty) ? ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim() : lstr_NuevoPosPreDevengo,
                    //    ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                    //    ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                    //    ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                    //    ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                    //    Math.Round(ldec_monto, 2));

                    ldat_Asiento.Rows.Add(
                        lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                        DateTime.Today.ToString("dd.MM.yyyy"),
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
                        Math.Round(ldec_monto, 2),
                        lstr_NroValor + "." + lstr_Nemotecnico,//pk
                        tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                        lstr_Moneda,//tipo
                        lstr_Operacion +"."+ lstr_NomOperacion //operacion
                        );
                    ldat_Asiento.Rows.Add(
                        lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                        DateTime.Today.ToString("dd.MM.yyyy"),
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
                        Math.Round(ldec_monto, 2),
                        lstr_NroValor + "." + lstr_Nemotecnico,//pk
                        tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                        lstr_Moneda,//tipo
                        lstr_Operacion +"."+ lstr_NomOperacion//operacion
                        );
                }
                #endregion
                lstr_Mensaje = GenerarAsientoAjuste("Reclasificación LP-CP", ldat_Asiento, lstr_Operacion, lstr_NroValor, lstr_Nemotecnico, ldec_TipoCambioColones, ldt_FchModifica);
            //}
            return lstr_Mensaje;
        }

        public string ReclasificarTituloValor(
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
            decimal ldec_ValorFacial,
            decimal ldec_RendimientoXDescuento,//descuento
            decimal ldec_ImpuestoPagado,
            decimal ldec_ValorTransadoNeto,
            decimal ldec_Premio,//prima
            string lstr_Detalle,
            DateTime ldt_FchModifica)
        {
            string lstr_Mensaje = string.Empty;
            wsDeudaInterna.wsDeudaInterna wsDeudaInterna = new wsDeudaInterna.wsDeudaInterna();
            DataTable ldat_Asiento = RegistroContable();
            DataTable ldat_Tira = new DataTable();
            string lint_EsPublico = "PUBLICO";
            string lstr_Operacion = string.Empty;
            string lstr_NomOperacion = string.Empty;
            decimal ldec_TipoCambioColones = Convert.ToDecimal(tipocambio.ConsultarTiposCambio("CRCN", ldt_FchCancelacion, "3140", "N").Tables[0].Rows[0]["Valor"].ToString());
            decimal ldec_TipoCambioUDE = Convert.ToDecimal(tipocambio.ConsultarTiposCambio("UDE", ldt_FchCancelacion, "", "N").Tables[0].Rows[0]["Valor"].ToString());
            decimal ldec_monto = 0;
            string lstr_Referencia = "";
            string lstr_Trasciende = "NT";
            string lstr_PrimaDescuento = "PRIMA";

            //Define si el propietario es público o privado
            if (propietario.ConsultarPropietarios(string.Empty, string.Empty, string.Empty, lstr_Propietario, "S").Tables[0].Rows.Count == 0)
            {
                lint_EsPublico = "PRIVADO";
            }

            //Define si trasciende o no el periodo
            if (ldt_FchValor.Year != ldt_FchVencimiento.Year)
            {
                lstr_Trasciende = "T";
            }

            //Define si es prima o descuento
            if (ldec_ValorFacial > ldec_ValorTransadoBruto)
            {
                lstr_PrimaDescuento = "DESCUENTO";
            }

            //se tratan las cancelaciones de las tres diferentes operaciones, títulos cero cupón, de tasa fija y tasa variable.
            //if (lstr_EstadoValor == "Cancelada")
            //{
                switch (lstr_Tipo.ToLower())
                {
                    #region cero cupon
                    case "cero cupón":
                        {
                            #region Define si el título es a corto plazo y no trasciende en el periodo
                            if ((Convert.ToDecimal(lstr_Plazo) <= 365) &&
                                //(lstr_Nemotecnico != "PT") &&
                                (ldt_FchValor.Year == ldt_FchVencimiento.Year))
                            {
                                lstr_Operacion = lstr_Moneda.Equals("USD") ? "ID43" : (lstr_Moneda.Equals("CRC") ? "ID41" : "ID41");

                                DataSet lds_Operaciones = loperacion.ConsultarOperaciones(lstr_Operacion, "IdModulo IN ('DI')", "");
                                if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
                                {
                                    lstr_NomOperacion = lds_Operaciones.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
                                }

                                //ldat_Tira = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", "CAPITAL").Tables[0];

                                string lstr_NemotecnicoTemp = lstr_Moneda.Equals("USD") ? (lstr_Nemotecnico.Equals("PT$") ? "PT$" : "") : (lstr_Nemotecnico.Equals("PT") ? "PT" : "");

                                ldat_Tira = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID").Tables[0].Clone();
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", "CAPITAL").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_NemotecnicoTemp, "", "ID", lstr_NemotecnicoTemp.Equals("") ? lstr_Trasciende : "", lstr_PrimaDescuento).Tables[0].Select("CodigoAuxiliar3 = '" + lstr_NemotecnicoTemp.Trim() + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);

                                foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                {
                                    int index = ldat_Tira.Rows.IndexOf(ldr_Row);

                                    switch (index)
                                    {
                                        case 0:
                                            { 
                                                ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorFacial : (lstr_Moneda.Equals("CRC") ? ldec_ValorFacial : ldec_ValorFacial * ldec_TipoCambioUDE); break; 
                                            }
                                        case 1:
                                            {
                                                if (lstr_PrimaDescuento == "PRIMA")
                                                {
                                                    ldec_monto = lstr_Moneda.Equals("USD") ? ldec_Premio : (lstr_Moneda.Equals("CRC") ? ldec_Premio : ldec_Premio * ldec_TipoCambioUDE); break;
                                                }
                                                else
                                                {
                                                    ldec_monto = lstr_Moneda.Equals("USD") ? ldec_RendimientoXDescuento : (lstr_Moneda.Equals("CRC") ? ldec_RendimientoXDescuento : ldec_RendimientoXDescuento * ldec_TipoCambioUDE); break;
                                                }
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

                                    lstr_Referencia = lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Cancelación";

                                    //ldat_Asiento.Rows.Add(
                                    //    lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                    //    ldt_FchCancelacion.ToString("dd.MM.yyyy"),
                                    //    ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                    //    ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                    //    ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                    //    lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim(),
                                    //    ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                    //    ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                    //    ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                    //    lstr_NuevoPosPreDevengo.Equals(string.Empty) ? ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim() : lstr_NuevoPosPreDevengo,
                                    //    ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                    //    ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                    //    ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                    //    ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                    //    Math.Round(ldec_monto, 2));

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
                                        ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                        Math.Round(ldec_monto, 2),
                                        lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                        tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                        lstr_Moneda,//tipo
                                        lstr_Operacion +"."+ lstr_NomOperacion //operacion
                                        );
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
                                        ldat_Tira.Rows[index]["IdPosPre2"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdCentroGestor2"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdFondo2"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["DocPresupuestario2"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["PosDocPresupuestario2"].ToString().Trim(),
                                        Math.Round(ldec_monto, 2),
                                        lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                        tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                        lstr_Moneda,//tipo
                                        lstr_Operacion +"."+ lstr_NomOperacion //operacion
                                        );
                                }
                            }
                            #endregion

                            #region Define si el título es a corto plazo, pero trasciende en el periodo
                            else if ((Convert.ToDecimal(lstr_Plazo) <= 365) &&
                                //(lstr_Nemotecnico != "PT") &&
                                (ldt_FchValor.Year != ldt_FchVencimiento.Year))
                            {
                                lstr_Operacion = lstr_Moneda.Equals("USD") ? "ID43" : (lstr_Moneda.Equals("CRC") ? "ID41" : "ID41");

                                DataSet lds_Operaciones = loperacion.ConsultarOperaciones(lstr_Operacion, "IdModulo IN ('DI')", "");
                                if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
                                {
                                    lstr_NomOperacion = lds_Operaciones.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
                                }

                                //ldat_Tira = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", "CAPITAL").Tables[0];

                                //foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                //{
                                //    int index = ldat_Tira.Rows.IndexOf(ldr_Row);

                                //    switch (index)
                                //    {
                                //        case 0: { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorFacial : (lstr_Moneda.Equals("CRC") ? ldec_ValorFacial : ldec_ValorFacial * ldec_TipoCambioUDE); break; }
                                //    }
                                string lstr_NemotecnicoTemp = lstr_Moneda.Equals("USD") ? (lstr_Nemotecnico.Equals("PT$") ? "PT$" : "") : (lstr_Nemotecnico.Equals("PT") ? "PT" : "");

                                ldat_Tira = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID").Tables[0].Clone();
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", "CAPITAL").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_NemotecnicoTemp, "", "ID", lstr_NemotecnicoTemp.Equals("") ? lstr_Trasciende : "", lstr_PrimaDescuento).Tables[0].Select("CodigoAuxiliar3 = '" + lstr_NemotecnicoTemp.Trim() + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID", "CP", "AMORT").Tables[0].Rows)
                                    ldat_Tira.ImportRow(dr_tira);

                                foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                {
                                    int index = ldat_Tira.Rows.IndexOf(ldr_Row);

                                    switch (index)
                                    {
                                        case 0:
                                            {
                                                ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorFacial : (lstr_Moneda.Equals("CRC") ? ldec_ValorFacial : ldec_ValorFacial * ldec_TipoCambioUDE); break;
                                            }
                                        case 1:
                                            {
                                                if (lstr_PrimaDescuento == "PRIMA")
                                                {
                                                    ldec_monto = lstr_Moneda.Equals("USD") ? ldec_Premio : (lstr_Moneda.Equals("CRC") ? ldec_Premio : ldec_Premio * ldec_TipoCambioUDE); break;
                                                }
                                                else
                                                {
                                                    ldec_monto = lstr_Moneda.Equals("USD") ? ldec_RendimientoXDescuento : (lstr_Moneda.Equals("CRC") ? ldec_RendimientoXDescuento : ldec_RendimientoXDescuento * ldec_TipoCambioUDE); break;
                                                }
                                            }
                                        case 2:
                                            {
                                                ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorTransadoBruto : (lstr_Moneda.Equals("CRC") ? ldec_ValorTransadoBruto : ldec_ValorTransadoBruto * ldec_TipoCambioUDE); break;
                                            }
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
                                    lstr_Referencia = lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Cancelación";

                                    //ldat_Asiento.Rows.Add(
                                    //    lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia, ldt_FchCancelacion.ToString("dd.MM.yyyy"),
                                    //    ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                    //    ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                    //    ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                    //    lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim(),
                                    //    ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                    //    ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                    //    ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                    //    lstr_NuevoPosPreDevengo.Equals(string.Empty) ? ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim() : lstr_NuevoPosPreDevengo,
                                    //    ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                    //    ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                    //    ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                    //    ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                    //    Math.Round(ldec_monto, 2));

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
                                        ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                        Math.Round(ldec_monto, 2),
                                        lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                        tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                        lstr_Moneda,//tipo
                                        lstr_Operacion +"."+ lstr_NomOperacion //operacion
                                        );
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
                                        ldat_Tira.Rows[index]["IdPosPre2"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdCentroGestor2"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdFondo2"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["DocPresupuestario2"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["PosDocPresupuestario2"].ToString().Trim(),
                                        Math.Round(ldec_monto, 2),
                                        lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                        tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                        lstr_Moneda,//tipo
                                        lstr_Operacion +"."+ lstr_NomOperacion//operacion
                                        );
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

                                //ldat_Tira = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "LP", "CAPITAL").Tables[0];

                                //foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                //{
                                //    int index = ldat_Tira.Rows.IndexOf(ldr_Row);

                                //    switch (index)
                                //    {
                                //        case 0: { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorFacial : (lstr_Moneda.Equals("CRC") ? ldec_ValorFacial : ldec_ValorFacial * ldec_TipoCambioUDE); break; }
                                //    }
                                ldat_Tira = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID").Tables[0].Clone();
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "LP", "CAPITAL").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID", lstr_Trasciende, lstr_PrimaDescuento).Tables[0].Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID", "LP", "AMORT").Tables[0].Rows)
                                    ldat_Tira.ImportRow(dr_tira);

                                foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                {
                                    int index = ldat_Tira.Rows.IndexOf(ldr_Row);

                                    switch (index)
                                    {
                                        case 0:
                                            {
                                                ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorFacial : (lstr_Moneda.Equals("CRC") ? ldec_ValorFacial : ldec_ValorFacial * ldec_TipoCambioUDE); break;
                                            }
                                        case 1:
                                            {
                                                if (lstr_PrimaDescuento == "PRIMA")
                                                {
                                                    ldec_monto = lstr_Moneda.Equals("USD") ? ldec_Premio : (lstr_Moneda.Equals("CRC") ? ldec_Premio : ldec_Premio * ldec_TipoCambioUDE); break;
                                                }
                                                else
                                                {
                                                    ldec_monto = lstr_Moneda.Equals("USD") ? ldec_RendimientoXDescuento : (lstr_Moneda.Equals("CRC") ? ldec_RendimientoXDescuento : ldec_RendimientoXDescuento * ldec_TipoCambioUDE); break;
                                                }
                                            }

                                        case 2:
                                            {
                                                ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorTransadoBruto : (lstr_Moneda.Equals("CRC") ? ldec_ValorTransadoBruto : ldec_ValorTransadoBruto * ldec_TipoCambioUDE); break;
                                            }
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
                                    lstr_Referencia = lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Cancelación";

                                    //ldat_Asiento.Rows.Add(
                                    //    lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                    //    ldt_FchCancelacion.ToString("dd.MM.yyyy"),
                                    //    ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                    //    ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                    //    ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                    //    lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim(),
                                    //    ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                    //    ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                    //    ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                    //    lstr_NuevoPosPreDevengo.Equals(string.Empty) ? ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim() : lstr_NuevoPosPreDevengo,
                                    //    ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                    //    ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                    //    ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                    //    ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                    //    Math.Round(ldec_monto, 2));

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
                                        ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                        Math.Round(ldec_monto, 2),
                                        lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                        tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                        lstr_Moneda,//tipo
                                        lstr_Operacion +"."+ lstr_NomOperacion//operacion
                                        );
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
                                        ldat_Tira.Rows[index]["IdPosPre2"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdCentroGestor2"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdFondo2"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["DocPresupuestario2"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["PosDocPresupuestario2"].ToString().Trim(),
                                        Math.Round(ldec_monto, 2),
                                        lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                        tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                        lstr_Moneda,//tipo
                                        lstr_Operacion +"."+ lstr_NomOperacion//operacion
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
                            if ((Convert.ToDecimal(lstr_Plazo) <= 1) &&
                                (ldt_FchValor.Year == ldt_FchVencimiento.Year))
                            {
                                lstr_Operacion = lstr_Moneda.Equals("USD") ? "ID43" : (lstr_Moneda.Equals("CRC") ? "ID41" : "ID41");

                                DataSet lds_Operaciones = loperacion.ConsultarOperaciones(lstr_Operacion, "IdModulo IN ('DI')", "");
                                if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
                                {
                                    lstr_NomOperacion = lds_Operaciones.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
                                }

                                //ldat_Tira = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", "CAPITAL").Tables[0];

                                //foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                //{
                                //    int index = ldat_Tira.Rows.IndexOf(ldr_Row);

                                //    switch (index)
                                //    {
                                //        case 0: { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorFacial : (lstr_Moneda.Equals("CRC") ? ldec_ValorFacial : ldec_ValorFacial * ldec_TipoCambioUDE); break; }
                                //    }
                                string lstr_NemotecnicoTemp = lstr_Moneda.Equals("USD") ? (lstr_Nemotecnico.Equals("PT$") ? "PT$" : "") : (lstr_Nemotecnico.Equals("PT") ? "PT" : "");

                                ldat_Tira = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID").Tables[0].Clone();
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", "CAPITAL").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_NemotecnicoTemp, "", "ID", lstr_NemotecnicoTemp.Equals("") ? lstr_Trasciende : "", lstr_PrimaDescuento).Tables[0].Select("CodigoAuxiliar3 = '" + lstr_NemotecnicoTemp + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);

                                foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                {
                                    int index = ldat_Tira.Rows.IndexOf(ldr_Row);

                                    switch (index)
                                    {
                                        case 0:
                                            {
                                                ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorFacial : (lstr_Moneda.Equals("CRC") ? ldec_ValorFacial : ldec_ValorFacial * ldec_TipoCambioUDE); break;
                                            }
                                        case 1:
                                            {
                                                if (lstr_PrimaDescuento == "PRIMA")
                                                {
                                                    ldec_monto = lstr_Moneda.Equals("USD") ? ldec_Premio : (lstr_Moneda.Equals("CRC") ? ldec_Premio : ldec_Premio * ldec_TipoCambioUDE); break;
                                                }
                                                else
                                                {
                                                    ldec_monto = lstr_Moneda.Equals("USD") ? ldec_RendimientoXDescuento : (lstr_Moneda.Equals("CRC") ? ldec_RendimientoXDescuento : ldec_RendimientoXDescuento * ldec_TipoCambioUDE); break;
                                                }
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

                                    lstr_Referencia = lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Cancelación";

                                    //ldat_Asiento.Rows.Add(
                                    //    lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                    //    ldt_FchCancelacion.ToString("dd.MM.yyyy"),
                                    //    ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                    //    ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                    //    ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                    //    lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim(),
                                    //    ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                    //    ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                    //    ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                    //    lstr_NuevoPosPreDevengo.Equals(string.Empty) ? ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim() : lstr_NuevoPosPreDevengo,
                                    //    ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                    //    ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                    //    ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                    //    ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                    //    Math.Round(ldec_monto, 2));

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
                                        ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                        Math.Round(ldec_monto, 2),
                                        lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                        tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                        lstr_Moneda,//tipo
                                        lstr_Operacion +"."+ lstr_NomOperacion//operacion
                                        );
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
                                        ldat_Tira.Rows[index]["IdPosPre2"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdCentroGestor2"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdFondo2"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["DocPresupuestario2"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["PosDocPresupuestario2"].ToString().Trim(),
                                        Math.Round(ldec_monto, 2),
                                        lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                        tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                        lstr_Moneda,//tipo
                                        lstr_Operacion +"."+ lstr_NomOperacion //operacion
                                        );
                                }
                            }
                            #endregion

                            #region Define si el título es a corto plazo, pero trasciende en el periodo
                            else if ((Convert.ToDecimal(lstr_Plazo) <= 1) &&
                            (ldt_FchValor.Year != ldt_FchVencimiento.Year))
                            {
                                lstr_Operacion = lstr_Moneda.Equals("USD") ? "ID43" : (lstr_Moneda.Equals("CRC") ? "ID41" : "ID41");

                                DataSet lds_Operaciones = loperacion.ConsultarOperaciones(lstr_Operacion, "IdModulo IN ('DI')", "");
                                if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
                                {
                                    lstr_NomOperacion = lds_Operaciones.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
                                }

                                //ldat_Tira = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", "CAPITAL").Tables[0];

                                //foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                //{
                                //    int index = ldat_Tira.Rows.IndexOf(ldr_Row);

                                //    switch (index)
                                //    {
                                //        case 0: { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorFacial : (lstr_Moneda.Equals("CRC") ? ldec_ValorFacial : ldec_ValorFacial * ldec_TipoCambioUDE); break; }
                                //    }
                                string lstr_NemotecnicoTemp = lstr_Moneda.Equals("USD") ? (lstr_Nemotecnico.Equals("PT$") ? "PT$" : "") : (lstr_Nemotecnico.Equals("PT") ? "PT" : "");

                                ldat_Tira = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID").Tables[0].Clone();
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "CP", "CAPITAL").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_NemotecnicoTemp, "", "ID", lstr_NemotecnicoTemp.Equals("") ? lstr_Trasciende : "", lstr_PrimaDescuento).Tables[0].Select("CodigoAuxiliar3 = '" + lstr_NemotecnicoTemp.Trim() + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID", "CP", "AMORT").Tables[0].Rows)
                                    ldat_Tira.ImportRow(dr_tira);

                                foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                {
                                    int index = ldat_Tira.Rows.IndexOf(ldr_Row);

                                    switch (index)
                                    {
                                        case 0:
                                            {
                                                ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorFacial : (lstr_Moneda.Equals("CRC") ? ldec_ValorFacial : ldec_ValorFacial * ldec_TipoCambioUDE); break;
                                            }
                                        case 1:
                                            {
                                                if (lstr_PrimaDescuento == "PRIMA")
                                                {
                                                    ldec_monto = lstr_Moneda.Equals("USD") ? ldec_Premio : (lstr_Moneda.Equals("CRC") ? ldec_Premio : ldec_Premio * ldec_TipoCambioUDE); break;
                                                }
                                                else
                                                {
                                                    ldec_monto = lstr_Moneda.Equals("USD") ? ldec_RendimientoXDescuento : (lstr_Moneda.Equals("CRC") ? ldec_RendimientoXDescuento : ldec_RendimientoXDescuento * ldec_TipoCambioUDE); break;
                                                }
                                            }

                                        case 2:
                                            {
                                                ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorFacial : (lstr_Moneda.Equals("CRC") ? ldec_ValorFacial : ldec_ValorFacial * ldec_TipoCambioUDE); break;
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

                                    lstr_Referencia = lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Cancelación";

                                    //ldat_Asiento.Rows.Add(
                                    //    lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                    //    ldt_FchCancelacion.ToString("dd.MM.yyyy"),
                                    //    ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                    //    ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                    //    ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                    //    lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim(),
                                    //    ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                    //    ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                    //    ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                    //    lstr_NuevoPosPreDevengo.Equals(string.Empty) ? ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim() : lstr_NuevoPosPreDevengo,
                                    //    ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                    //    ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                    //    ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                    //    ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                    //    Math.Round(ldec_monto, 2));

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
                                        ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                        Math.Round(ldec_monto, 2),
                                        lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                        tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                        lstr_Moneda,//tipo
                                        lstr_Operacion +"."+ lstr_NomOperacion //operacion
                                        );
                                    
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
                                        ldat_Tira.Rows[index]["IdPosPre2"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdCentroGestor2"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdFondo2"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["DocPresupuestario2"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["PosDocPresupuestario2"].ToString().Trim(),
                                        Math.Round(ldec_monto, 2),
                                        lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                        tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                        lstr_Moneda,//tipo
                                        lstr_Operacion +"."+ lstr_NomOperacion//operacion
                                        );

                                    System.IO.File.WriteAllText(@"C:\Users\Public\log.txt", ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim() + " - " + ldat_Tira.Rows[index]["IdCuentaContable2"].ToString().Trim());
                                }
                            }
                            #endregion

                            #region Define si el título es a largo plazo con afectación presupuestaria
                            else if (Convert.ToDecimal(lstr_Plazo) > 1)
                            {
                                lstr_Operacion = lstr_Moneda.Equals("USD") ? "ID44" : (lstr_Moneda.Equals("CRC") ? "ID42" : "ID42");

                                DataSet lds_Operaciones = loperacion.ConsultarOperaciones(lstr_Operacion, "IdModulo IN ('DI')", "");
                                if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
                                {
                                    lstr_NomOperacion = lds_Operaciones.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
                                }

                                //ldat_Tira = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "LP", "CAPITAL").Tables[0];

                                //foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                //{
                                //    int index = ldat_Tira.Rows.IndexOf(ldr_Row);

                                //    switch (index)
                                //    {
                                //        case 0: { ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorFacial : (lstr_Moneda.Equals("CRC") ? ldec_ValorFacial : ldec_ValorFacial * ldec_TipoCambioUDE); break; }
                                //    }

                                ldat_Tira = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID").Tables[0].Clone();
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", "LP", "CAPITAL").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID", lstr_Trasciende, lstr_PrimaDescuento).Tables[0].Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID", "LP", "AMORT").Tables[0].Rows)
                                    ldat_Tira.ImportRow(dr_tira);

                                foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                {
                                    int index = ldat_Tira.Rows.IndexOf(ldr_Row);

                                    switch (index)
                                    {
                                        case 0:
                                            {
                                                ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorFacial : (lstr_Moneda.Equals("CRC") ? ldec_ValorFacial : ldec_ValorFacial * ldec_TipoCambioUDE); break;
                                            }
                                        case 1:
                                            {
                                                if (lstr_PrimaDescuento == "PRIMA")
                                                {
                                                    ldec_monto = lstr_Moneda.Equals("USD") ? ldec_Premio : (lstr_Moneda.Equals("CRC") ? ldec_Premio : ldec_Premio * ldec_TipoCambioUDE); break;
                                                }
                                                else
                                                {
                                                    ldec_monto = lstr_Moneda.Equals("USD") ? ldec_RendimientoXDescuento : (lstr_Moneda.Equals("CRC") ? ldec_RendimientoXDescuento : ldec_RendimientoXDescuento * ldec_TipoCambioUDE); break;
                                                }
                                            }

                                        case 2:
                                            {
                                                ldec_monto = lstr_Moneda.Equals("USD") ? ldec_ValorFacial : (lstr_Moneda.Equals("CRC") ? ldec_ValorFacial : ldec_ValorFacial * ldec_TipoCambioUDE); break;
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

                                    lstr_Referencia = lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Cancelación";

                                    //ldat_Asiento.Rows.Add(
                                    //    lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia,
                                    //    ldt_FchCancelacion.ToString("dd.MM.yyyy"),
                                    //    ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                    //    ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                    //    ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                    //    lstr_Detalle.Trim().Length > 50 ? lstr_Detalle.Trim().Substring(0, 47) + "..." : lstr_Detalle.Trim(),
                                    //    ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                    //    ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                    //    ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                    //    lstr_NuevoPosPreDevengo.Equals(string.Empty) ? ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim() : lstr_NuevoPosPreDevengo,
                                    //    ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                    //    ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                    //    ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                    //    ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                    //    Math.Round(ldec_monto, 2));


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
                                        ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                        Math.Round(ldec_monto, 2),
                                        lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                        tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                        lstr_Moneda,//tipo
                                        lstr_Operacion +"."+ lstr_NomOperacion//operacion
                                        );
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
                                        ldat_Tira.Rows[index]["IdPosPre2"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdCentroGestor2"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdFondo2"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["DocPresupuestario2"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["PosDocPresupuestario2"].ToString().Trim(),
                                        Math.Round(ldec_monto, 2),
                                        lstr_NroValor + "." + lstr_Nemotecnico,//pk
                                        tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                        lstr_Moneda,//tipo
                                        lstr_Operacion +"."+ lstr_NomOperacion //operacion
                                        );
                                }
                            }
                            #endregion
                            break;
                        }
                    #endregion
                }
                lstr_Mensaje = GenerarAsientoAjuste("Cancelacion Título", ldat_Asiento, lstr_Operacion, lstr_NroValor, lstr_Nemotecnico, ldec_TipoCambioColones, ldt_FchModifica);
            //}
            return lstr_Mensaje;
        }

        public static string GenerarAsientoAjuste(string lstr_TipoCancelacion, DataTable ldat_Asiento, string lstr_IdOperacion, string lstr_NroValor, string lstr_Nemotecnico, decimal ldec_TipoCambio, DateTime ldt_FchModifica)
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
                    lcls_CostoTransaccion.ContabilizarCalculosFinancieros(
                        "TitulosValores",
                        null,
                        lstr_NroValor.Trim(),
                        lstr_Nemotecnico.Trim(),
                        "CAN",
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