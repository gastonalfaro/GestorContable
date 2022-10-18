using System;
using System.Data;

namespace SGAsientosDeudaInterna
{
    public class Colocaciones
    {
        private static wsDeudaInterna.wsDeudaInterna wsDeudaInterna = new wsDeudaInterna.wsDeudaInterna();
        private static ws_SGService.wsSistemaGestor ws_SGService = new ws_SGService.wsSistemaGestor();
        private static wsAsientos.ServicioContable wsAsientos = new wsAsientos.ServicioContable();

        public string Colocacion() 
        {
            string lstr_Mensaje = string.Empty;
            try{
                //cucurucho solo tenia 7 parametros
                //DataSet ldat_TitulosValores = wsDeudaInterna.ConsultarTitulosValores("%", "%", "%", "%", "%", "01/01/1900", "01/01/5000");
                DataSet ldat_TitulosValores = wsDeudaInterna.ConsultarTitulosValores("%", "%", "%", "%", "%", "%", "%", "%", "01/01/1900", "01/01/5000");
            

            DataTable ldat_Nemotecnicos = ws_SGService.uwsConsultarNemotecnicos(null, null, null, null, null).Tables[0];

            for (int i = 0; i < ldat_TitulosValores.Tables[0].Rows.Count; i++)
            {
                string lstr_moneda = ldat_Nemotecnicos.Select("IdNemotecnico = '" + ldat_TitulosValores.Tables[0].Rows[i]["NemoTecnico"].ToString() + "'")[0]["IdMoneda"].ToString().Trim().Equals("CRCN")
                   ? "CRC" : ldat_Nemotecnicos.Select("IdNemotecnico = '" + ldat_TitulosValores.Tables[0].Rows[i]["NemoTecnico"].ToString() + "'")[0]["IdMoneda"].ToString().Trim();
                             
                
                //if (ldat_Nemotecnicos.Select("IdNemotecnico = '" + ldat_TitulosValores.Tables[0].Rows[i]["NemoTecnico"].ToString() + "'")[0]["Estado"].ToString().Trim() == "A"
                //    && lstr_moneda == ldat_TitulosValores.Tables[0].Rows[i]["Moneda"].ToString())

                if (ldat_Nemotecnicos.Select("IdNemotecnico = '" + ldat_TitulosValores.Tables[0].Rows[i]["NemoTecnico"].ToString() + "'")[0]["Estado"].ToString().Trim() == "A"
                    && lstr_moneda == (ldat_TitulosValores.Tables[0].Rows[i]["Moneda"].ToString().Equals("CRCN") ? "CRC" : ldat_TitulosValores.Tables[0].Rows[i]["Moneda"].ToString()))
                {
                    if (ldat_TitulosValores.Tables[0].Rows[i]["EstadoValor"].ToString() == "Vigente"
                        && ldat_TitulosValores.Tables[0].Rows[i]["Estado"].ToString().Trim() != "C")
                    {
                       lstr_Mensaje = ColocaTituloValor(
                            ldat_TitulosValores.Tables[0].Rows[i]["Tipo"].ToString(),
                            ldat_TitulosValores.Tables[0].Rows[i]["EstadoValor"].ToString(),
                            Convert.ToDateTime(ldat_TitulosValores.Tables[0].Rows[i]["FchValor"].ToString()),
                            Convert.ToDateTime(ldat_TitulosValores.Tables[0].Rows[i]["FchVencimiento"].ToString()),
                            ldat_TitulosValores.Tables[0].Rows[i]["Propietario"].ToString(),
                            ldat_TitulosValores.Tables[0].Rows[i]["PlazoValor"].ToString(),
                            Convert.ToDecimal(ldat_TitulosValores.Tables[0].Rows[i]["ValorTransadoBruto"].ToString()),
                            ldat_TitulosValores.Tables[0].Rows[i]["Moneda"].ToString().Equals("CRCN") ? "CRC" : ldat_TitulosValores.Tables[0].Rows[i]["Moneda"].ToString(),

                            ldat_TitulosValores.Tables[0].Rows[i]["NroValor"].ToString(),
                            ldat_TitulosValores.Tables[0].Rows[i]["NemoTecnico"].ToString(),
                            Convert.ToDecimal(ldat_TitulosValores.Tables[0].Rows[i]["ValorFacial"].ToString()),
                            Convert.ToDecimal(ldat_TitulosValores.Tables[0].Rows[i]["RendimientoPorDescuento"].ToString()),
                            Convert.ToDecimal(ldat_TitulosValores.Tables[0].Rows[i]["ImpuestoPagado"].ToString()),
                            Convert.ToDecimal(ldat_TitulosValores.Tables[0].Rows[i]["ValorTransadoNeto"].ToString()),
                            Convert.ToDecimal(ldat_TitulosValores.Tables[0].Rows[i]["Premio"].ToString()),
                            "Colocación de títulos valores",
                            Convert.ToDateTime(ldat_TitulosValores.Tables[0].Rows[i]["FchModifica"].ToString()));
                    }
                }
            }
            }catch(Exception ex)
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
            string lstr_Detalle,
            DateTime ldt_FchModifica)
        {
            string lstr_Mensaje = string.Empty;
            wsDeudaInterna.wsDeudaInterna wsDeudaInterna = new wsDeudaInterna.wsDeudaInterna();
            DataTable ldat_Asiento = RegistroContable();
            DataTable ldat_Tira = new DataTable();
            int lint_EsPublico = 1;
            string lstr_Operacion = string.Empty;
            decimal ldec_TipoCambio = Convert.ToDecimal(ws_SGService.uwsConsultarTiposCambio("CRCN", DateTime.Today, "3140", "N").Tables[0].Rows[0]["Valor"].ToString());
            //string lstr_Moneda = wsDeudaInterna.ConsultarTitulosValores(lstr_NroValor, lstr_Nemotecnico, "%", "%", "%", "01/01/1900", "01/01/5000").Tables[0].Rows[0]["Moneda"].ToString();
            decimal ldec_monto = 0;
            if (ws_SGService.uwsConsultarPropietarios(string.Empty, string.Empty, string.Empty, lstr_Propietario).Tables[0].Rows.Count == 0)
            {
                lint_EsPublico = 2;
            }

            //se tratan las colocaciones de las tres diferentes operaciones, títulos cero cupón, de tasa fija y tasa variable.
            if (lstr_EstadoValor == "Vigente")
            {
                switch (lstr_Tipo.ToLower())
                {
                    #region cero cupon
                    case "cero cupón":
                        {
                            //Define si el título es a corto plazo y no trasciende en el periodo
                            if ((Convert.ToInt32(lstr_Plazo) <= 365) &&
                                (lstr_Nemotecnico != "PT") &&
                                (ldt_FchValor.Year == ldt_FchVencimiento.Year))
                            {
                                lstr_Operacion = lstr_Moneda.Equals("CRC") ? "ID08" : "ID10";

                                ldat_Tira = ws_SGService.uwsConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", lstr_Moneda, lstr_Nemotecnico, "", "ID").Tables[0].Select("EsNemotecnico='5' AND EsPubPriv='" + lint_EsPublico + "'").CopyToDataTable();

                                foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                {
                                    int index = ldat_Tira.Rows.IndexOf(ldr_Row);

                                    switch (index)
                                    {
                                        case 0: { ldec_monto = ldec_ValorFacial; break; }
                                        case 1: { ldec_monto = ldec_ImpuestoPagado; break; }
                                        case 2: { ldec_monto = ldec_RendimientoXDescuento; break; }
                                        case 3: { ldec_monto = ldec_ValorTransadoNeto; break; }
                                    }

                                    DataSet ldat_Reservas = new DataSet();
                                    string lstr_NuevoPosPreDevengo = string.Empty;
                                    if (ldat_Tira.Rows[0]["IdPosPre"].ToString().Trim().Equals("PP_Balance"))
                                    {
                                        //ldat_Reservas = ws_SGService.uwsConsultarReservaDetallado(string.Empty); cucurucho
                                        ldat_Reservas = ws_SGService.uwsConsultarReservaDetallado(string.Empty, "", "", "", "", "", "", "", "", "", "", "", "", "");
                                        DataView dv = ldat_Reservas.Tables[0].DefaultView;
                                        dv.Sort = "OrdenDeudaInterna ASC";

                                        foreach (DataRow drForm in dv.ToTable().Rows)
                                        {
                                            if (!drForm["OrdenDeudaInterna"].ToString().Equals(string.Empty))
                                                //ldat_AsientoDevengo
                                                if (Convert.ToInt32(drForm["Monto"]) > 0 && !drForm["OrdenDeudaInterna"].ToString().Equals("0")
                                                    && Convert.ToDecimal(drForm["Monto"]) >= ldec_monto)
                                                {
                                                    lstr_NuevoPosPreDevengo = drForm["IdPosPre"].ToString().Trim();
                                                    break;
                                                }
                                        }
                                    }

                                    ldat_Asiento.Rows.Add(
                                        lstr_NroValor + " " + lstr_Nemotecnico,
                                        ldt_FchValor.ToString("dd.MM.yyyy"),
                                        ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                        lstr_Detalle.Trim(),
                                        ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                        lstr_NuevoPosPreDevengo.Equals(string.Empty) ? ldat_Tira.Rows[0]["IdPosPre"].ToString().Trim() : lstr_NuevoPosPreDevengo,
                                        ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                        Math.Round(ldec_monto, 2));
                                }
                            }
                            //Define si el título es a corto plazo, pero trasciende en el periodo
                            else if ((Convert.ToInt32(lstr_Plazo) <= 365) &&
                                (lstr_Nemotecnico != "PT") &&
                                (ldt_FchValor.Year != ldt_FchVencimiento.Year))
                            {
                                lstr_Operacion = lstr_Moneda.Equals("CRC") ? "ID07" : "ID09";

                                ldat_Tira = ws_SGService.uwsConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", lstr_Moneda, lstr_Nemotecnico, "", "ID").Tables[0].Select("EsNemotecnico='5' AND EsPubPriv='" + lint_EsPublico + "'").CopyToDataTable();

                                foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                {
                                    int index = ldat_Tira.Rows.IndexOf(ldr_Row);

                                    switch (index)
                                    {
                                        case 0: { ldec_monto = ldec_ValorFacial; break; }
                                        case 1: { ldec_monto = ldec_ImpuestoPagado; break; }
                                        case 2: { ldec_monto = ldec_RendimientoXDescuento; break; }
                                        case 3: { ldec_monto = ldec_ValorTransadoNeto; break; }
                                    }


                                    DataSet ldat_Reservas = new DataSet();
                                    string lstr_NuevoPosPreDevengo = string.Empty;
                                    if (ldat_Tira.Rows[0]["IdPosPre"].ToString().Trim().Equals("PP_Balance"))
                                    {
                                        //ldat_Reservas = ws_SGService.uwsConsultarReservaDetallado(string.Empty); cucurucho
                                        ldat_Reservas = ws_SGService.uwsConsultarReservaDetallado(string.Empty, "", "", "", "", "", "", "", "", "", "", "", "", "");
                                        DataView dv = ldat_Reservas.Tables[0].DefaultView;
                                        dv.Sort = "OrdenDeudaInterna ASC";

                                        foreach (DataRow drForm in dv.ToTable().Rows)
                                        {
                                            if (!drForm["OrdenDeudaInterna"].ToString().Equals(string.Empty))
                                                //ldat_AsientoDevengo
                                                if (Convert.ToInt32(drForm["Monto"]) > 0 && !drForm["OrdenDeudaInterna"].ToString().Equals("0")
                                                    && Convert.ToDecimal(drForm["Monto"]) >= ldec_monto)
                                                {
                                                    lstr_NuevoPosPreDevengo = drForm["IdPosPre"].ToString().Trim();
                                                    break;
                                                }
                                        }
                                    }

                                    ldat_Asiento.Rows.Add(
                                        lstr_NroValor + " " + lstr_Nemotecnico,
                                        ldt_FchValor.ToString("dd.MM.yyyy"),
                                        ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                        lstr_Detalle.Trim(),
                                        ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                        lstr_NuevoPosPreDevengo.Equals(string.Empty) ? ldat_Tira.Rows[0]["IdPosPre"].ToString().Trim() : lstr_NuevoPosPreDevengo,
                                        ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                        Math.Round(ldec_monto, 2));
                                }
                            }
                            //Define si el título es a largo plazo
                            else if ((Convert.ToInt32(lstr_Plazo) > 365) &&
                                (lstr_Nemotecnico != "PT"))
                            {
                                if (lstr_Moneda == "CRC")
                                {
                                    lstr_Operacion = "__";
                                }
                                else
                                {
                                    lstr_Operacion = "__";
                                }
                                ldat_Tira = ws_SGService.uwsConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", lstr_Moneda, lstr_Nemotecnico, "", "ID").Tables[0].Select("EsNemotecnico='5' AND EsPubPriv='" + lint_EsPublico + "'").CopyToDataTable();
                            }
                            //Define si el título es de deuda de tesorería
                            else if (lstr_Nemotecnico == "PT")
                            {
                                if (lstr_Moneda == "CRC")
                                {
                                    lstr_Operacion = "__";
                                }
                                else
                                {
                                    lstr_Operacion = "__";
                                }
                                ldat_Tira = ws_SGService.uwsConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", lstr_Moneda, lstr_Nemotecnico, "", "ID").Tables[0].Select("EsNemotecnico='5' AND EsPubPriv='" + lint_EsPublico + "'").CopyToDataTable();
                            }
                            break;
                        }
                    #endregion

                    #region tasa fija y tasa variable
                    case "tasa fija":
                    case "tasa variable":
                        {
                            int CortoLargoPlazo = 0;
                            string ctaContableFondo = "";
                            string ctaContableImpuesto = "";

                            //............................................................//

                            //Define si el título es a corto plazo y no trasciende en el periodo
                            if (//(Convert.ToInt32(lstr_Plazo) <= 365) &&
                                (ldt_FchValor.Year == ldt_FchVencimiento.Year))
                            {
                                lstr_Operacion = lstr_Moneda.Equals("CRC") ? "ID08" : "ID10";
                                ctaContableFondo = lstr_Nemotecnico.Equals("TPTBP") ? "2119901010" : (lstr_Moneda.Equals("CRC") ? "1114910101" : "1114920101");
                                ctaContableImpuesto = lint_EsPublico.Equals(1) ? "4110302011" : "4110302021";
                                CortoLargoPlazo = 1;

                                ldat_Tira = ws_SGService.uwsConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, ctaContableFondo, "", "", "", "", "ID").Tables[0].Clone();
                                foreach (DataRow dr_tira in ws_SGService.uwsConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, ctaContableFondo, "", "", "", "", "ID").Tables[0].Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in ws_SGService.uwsConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, ctaContableImpuesto, "", "", "", "", "ID").Tables[0].Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in ws_SGService.uwsConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", lstr_Moneda, lstr_Nemotecnico, "", "ID").Tables[0].Select("EsNemotecnico='5' AND EsPubPriv='" + lint_EsPublico + "' AND EsLargoCortoPlazo='" + CortoLargoPlazo + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);

                                foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                {
                                    int index = ldat_Tira.Rows.IndexOf(ldr_Row);

                                    switch (index)
                                    {
                                        case 0: { ldec_monto = ldec_ValorTransadoNeto; break; }
                                        case 1: { ldec_monto = ldec_ImpuestoPagado; break; }
                                        case 2: { ldec_monto = ldec_ValorFacial; break; }
                                        case 3: { ldec_monto = ldec_RendimientoXDescuento; break; }
                                        case 4: { ldec_monto = (ldec_ValorTransadoNeto - ldec_ValorTransadoBruto); break; }
                                        case 5: { ldec_monto = (ldec_Premio); break; }
                                    }

                                    DataSet ldat_Reservas = new DataSet();
                                    string lstr_NuevoPosPreDevengo = string.Empty;
                                    if (ldat_Tira.Rows[0]["IdPosPre"].ToString().Trim().Equals("PP_Balance"))
                                    {
                                        //ldat_Reservas = ws_SGService.uwsConsultarReservaDetallado(string.Empty);  cucurucho
                                        ldat_Reservas = ws_SGService.uwsConsultarReservaDetallado(string.Empty, "", "", "", "", "", "", "", "", "", "", "", "", "");

                                        DataView dv = ldat_Reservas.Tables[0].DefaultView;
                                        dv.Sort = "OrdenDeudaInterna ASC";

                                        foreach (DataRow drForm in dv.ToTable().Rows)
                                        {
                                            if (!drForm["OrdenDeudaInterna"].ToString().Equals(string.Empty))
                                                //ldat_AsientoDevengo
                                                if (Convert.ToInt32(drForm["Monto"]) > 0 && !drForm["OrdenDeudaInterna"].ToString().Equals("0")
                                                    && Convert.ToDecimal(drForm["Monto"]) >= ldec_monto)
                                                {
                                                    lstr_NuevoPosPreDevengo = drForm["IdPosPre"].ToString().Trim();
                                                    break;
                                                }
                                        }
                                    }

                                    ldat_Asiento.Rows.Add(
                                        lstr_NroValor + " " + lstr_Nemotecnico,
                                        ldt_FchValor.ToString("dd.MM.yyyy"),
                                        ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                        lstr_Detalle.Trim(),
                                        ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                        lstr_NuevoPosPreDevengo.Equals(string.Empty) ? ldat_Tira.Rows[0]["IdPosPre"].ToString().Trim() : lstr_NuevoPosPreDevengo,
                                        ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                        Math.Round(ldec_monto, 2));
                                }
                            }
                            //Define si el título es a corto plazo, pero trasciende en el periodo
                            else //if //((Convert.ToInt32(lstr_Plazo) <= 365) &&
                            //(ldt_FchValor.Year != ldt_FchVencimiento.Year))
                            {
                                lstr_Operacion = lstr_Moneda.Equals("CRC") ? "ID07" : "ID09";
                                ctaContableFondo = lstr_Nemotecnico.Equals("TPTBP") ? "2119901010" : (lstr_Moneda.Equals("CRC") ? "1114910101" : "1114920101");
                                ctaContableImpuesto = lint_EsPublico.Equals(1) ? "4110302011" : "4110302021";
                                CortoLargoPlazo = Convert.ToInt32(lstr_Plazo) <= 365 ? 1 : 2;

                                ldat_Tira = ws_SGService.uwsConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, ctaContableFondo, "", "", "", "", "ID").Tables[0].Clone();
                                foreach (DataRow dr_tira in ws_SGService.uwsConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, ctaContableFondo, "", "", "", "", "ID").Tables[0].Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in ws_SGService.uwsConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, ctaContableImpuesto, "", "", "", "", "ID").Tables[0].Rows)
                                    ldat_Tira.ImportRow(dr_tira);
                                foreach (DataRow dr_tira in ws_SGService.uwsConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", lstr_Moneda, lstr_Nemotecnico, "", "ID").Tables[0].Select("EsNemotecnico='5' AND EsPubPriv='" + lint_EsPublico + "' AND EsLargoCortoPlazo='" + CortoLargoPlazo + "'").CopyToDataTable().Rows)
                                    ldat_Tira.ImportRow(dr_tira);

                                foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                {
                                    int index = ldat_Tira.Rows.IndexOf(ldr_Row);

                                    switch (index)
                                    {
                                        case 0: { ldec_monto = ldec_ValorTransadoNeto; break; }
                                        case 1: { ldec_monto = ldec_ImpuestoPagado; break; }
                                        case 2: { ldec_monto = ldec_ValorFacial; break; }
                                        case 3: { ldec_monto = ldec_RendimientoXDescuento; break; }
                                        case 4: { ldec_monto = (ldec_ValorTransadoNeto - ldec_ValorTransadoBruto); break; }
                                        case 5: { ldec_monto = (ldec_Premio); break; }
                                    }

                                    DataSet ldat_Reservas = new DataSet();
                                    string lstr_NuevoPosPreDevengo = string.Empty;
                                    if (ldat_Tira.Rows[0]["IdPosPre"].ToString().Trim().Equals("PP_Balance"))
                                    {
                                        //ldat_Reservas = ws_SGService.uwsConsultarReservaDetallado(string.Empty); cucurucho
                                        ldat_Reservas = ws_SGService.uwsConsultarReservaDetallado(string.Empty, "", "", "", "", "", "", "", "", "", "", "", "", "");
                                        DataView dv = ldat_Reservas.Tables[0].DefaultView;
                                        dv.Sort = "OrdenDeudaInterna ASC";

                                        foreach (DataRow drForm in dv.ToTable().Rows)
                                        {
                                            if (!drForm["OrdenDeudaInterna"].ToString().Equals(string.Empty))
                                                //ldat_AsientoDevengo
                                                if (Convert.ToInt32(drForm["Monto"]) > 0 && !drForm["OrdenDeudaInterna"].ToString().Equals("0")
                                                    && Convert.ToDecimal(drForm["Monto"]) >= ldec_monto)
                                                {
                                                    lstr_NuevoPosPreDevengo = drForm["IdPosPre"].ToString().Trim();
                                                    break;
                                                }
                                        }
                                    }

                                    ldat_Asiento.Rows.Add(
                                        lstr_NroValor + " " + lstr_Nemotecnico,
                                        ldt_FchValor.ToString("dd.MM.yyyy"),
                                        ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                        lstr_Detalle.Trim(),
                                        ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                        lstr_NuevoPosPreDevengo.Equals(string.Empty) ? ldat_Tira.Rows[0]["IdPosPre"].ToString().Trim() : lstr_NuevoPosPreDevengo,
                                        ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                        Math.Round(ldec_monto, 2));
                                }
                            }
                            //Define si el título es a largo plazo con afectación presupuestaria
                            //else if (Convert.ToInt32(lstr_Plazo) > 365)
                            //{

                            //}
                            break;
                        }
                    #endregion
                }
              lstr_Mensaje = GenerarAsientoAjuste(ldat_Asiento, lstr_Operacion, lstr_NroValor, lstr_Nemotecnico, ldec_TipoCambio, ldt_FchValor);
            }
            return lstr_Mensaje;
        }

        public static string GenerarAsientoAjuste(DataTable ldat_Asiento, string lstr_IdOperacion, string lstr_NroValor, string lstr_Nemotecnico, decimal ldec_TipoCambio, DateTime ldt_FchModifica)
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
                    item_asiento.Xref2Hd = ldat_Asiento.Rows[index]["PosPre"].ToString();//Posición Presupuestaria
                    item_asiento.Fistl = ldat_Asiento.Rows[index]["CentroGestor"].ToString();//Centro Gestor
                    item_asiento.Geber = ldat_Asiento.Rows[index]["Fondo"].ToString();//Fondo
                    item_asiento.Kblnr = ldat_Asiento.Rows[index]["DocPres"].ToString();//Documento Presupuestario
                    item_asiento.Kblpos = ldat_Asiento.Rows[index]["PosDocPres"].ToString();//Posición de documento presupuestario
                    if (ldat_Asiento.Rows[index]["Moneda"].ToString() == "USD")
                        item_asiento.Kursf = ldec_TipoCambio;

                    tabla_asientos[index] = item_asiento;
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
                ws_SGService.uwsRegistrarAccionBitacoraCo("DI", "123", "Colocación Titulo", "Resultado de Contabilización: " + logAsiento, lstr_IdOperacion, lstr_NroValor + "-" + lstr_Nemotecnico, "");
                
                // convertir el 
                //Marcar registro como contabilizado
                if (!logAsiento.Contains("[E]"))
                    wsDeudaInterna.ContabilizarCalculosFinancieros(
                        "TitulosValores",
                        null,
                        lstr_NroValor,
                        lstr_Nemotecnico,
                        "C",
                        "SG",
                        ldt_FchModifica);
                return logAsiento;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    
    }
}
