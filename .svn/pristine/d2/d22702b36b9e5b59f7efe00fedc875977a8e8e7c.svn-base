using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using Microsoft.VisualBasic;
using LogicaNegocio.Mantenimiento;
using LogicaNegocio.Seguridad;
using System.IO;

namespace LogicaNegocio.CalculosFinancieros.DeudaInterna
{
    public class clsContabilizarDevengoInt
    {
        private tBitacora reg_Bitacora = new tBitacora();
        public clsContabilizarDevengoInt()
        {
            CultureInfo culture;
            culture = CultureInfo.CreateSpecificCulture("es-CR");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }

        //private static wsAsientos.ServicioContable wsAsientos = new wsAsientos.ServicioContable();
        private static clsTiposAsiento tasientos = new clsTiposAsiento();
        private static Mantenimiento.clsTiposCambio tipocambio = new Mantenimiento.clsTiposCambio();
        private static Mantenimiento.clsPropietarios propietario = new Mantenimiento.clsPropietarios();
        private static Mantenimiento.clsNemotecnicos nemotecnico = new Mantenimiento.clsNemotecnicos();
        private static Mantenimiento.clsTiposAsiento tipoasiento = new Mantenimiento.clsTiposAsiento();
        private static Mantenimiento.clsOperaciones loperacion = new clsOperaciones();
        private static Mantenimiento.clsReservasDetalle reservas = new Mantenimiento.clsReservasDetalle();
        private static Seguridad.tBitacora bitacora = new Seguridad.tBitacora();
        private static clsDevengoInteres titulo = new clsDevengoInteres();
        private static tiras tira = new tiras();
        private static clsCostoTransaccion lcls_CostoTransaccion = new clsCostoTransaccion();
        private static Mantenimiento.clsDinamico dinamico = new Mantenimiento.clsDinamico();
        private static Mantenimiento.clsTiposAsiento tiposAsiento = new Mantenimiento.clsTiposAsiento();
        private static ClsUtilitarios utilitario = new ClsUtilitarios();
        private string resAsientosLog = string.Empty;

        public string DevengoPorFecha(DateTime? ldec_FchInicio, DateTime? ldec_FchFin, string lstr_pNroValor = "", string lstr_pNemotecnico = "", string lstr_TipoNemotecnico = "")
        {
            string lstr_TipoValor = String.Empty;
            string lstr_NroValor = String.Empty;
            string lstr_Nemotecnico = String.Empty;
            string res = "00 - Proceso finalizado";
            //string lstr_SQL = "select * from cf.DevengosMensuales where (NroValor = '" + lstr_pNroValor + "' or isnull('" + lstr_pNroValor + "','') ='') and (Nemotecnico = '" + lstr_pNemotecnico + "' or isnull('" + lstr_pNemotecnico + "','') ='') and convert(date,Periodo,103) between '" + ldec_FchInicio.Value.ToString("yyyy-MM-dd") + "' and '" + ldec_FchFin.Value.ToString("yyyy-MM-dd") + "' order by IdDevengoMensual";

            string lstr_SQL = "select b.* from (select * from ma.nemotecnicos where (TipoNemotecnico like '" + lstr_TipoNemotecnico + "' or isnull('" + lstr_TipoNemotecnico + "','') ='')) as a " +
            "inner join " +
            "(select * from cf.DevengosMensuales where (NroValor = '" + lstr_pNroValor + "' or isnull('" + lstr_pNroValor + "','') ='') and (Nemotecnico = '" + lstr_pNemotecnico + "' or isnull('" + lstr_pNemotecnico + "','') ='') and convert(date,Periodo,103) between '" + ldec_FchInicio.Value.ToString("yyyy-MM-dd") + "' and '" + ldec_FchFin.Value.ToString("yyyy-MM-dd") + "') as b " +
            "on a.IdNemotecnico = b.nemotecnico " +
            "order by b.IdDevengoMensual";

            DataTable ldat_DevengoMensual = dinamico.ConsultarDinamico(lstr_SQL).Tables[0];

            if (ldat_DevengoMensual.Rows.Count==0) 
            {
                string consulta = "DECLARE @FCanje NVARCHAR(50) = '" + ldec_FchFin.Value.ToShortDateString() + "', " +
                                   "@Nemotecnico NVARCHAR(50) ='" + lstr_pNemotecnico + "', " +
                                   "@NroValor INT =" + lstr_pNroValor 
                                   +" DECLARE @fchCanje DATETIME,@Periodo DATETIME, @InicioP DATETIME, @DiasOriginales FLOAT,	@DiasPeriodo FLOAT; " 
                                   +" SET @DiasOriginales =0; SET @DiasPeriodo =0;  SET @fchCanje = convert(date,@FCanje,103); "
                                   +" set @Periodo = (	select TOP 1 convert(date,periodo,103)  "
                                   +"					From cf.DevengosMensuales where nemotecnico = @Nemotecnico 	and nrovalor = @NroValor  "
                                   +"					and MONTH(convert(date,periodo,103)) = MONTH(@fchCanje) and YEAR(convert(date,periodo,103)) = YEAR(@fchCanje) "
                                   +"					and @fchCanje < = convert(date,periodo,103) ORDER BY convert(date,periodo,103)  ASC	) "
                                   +" SET @InicioP =  (	select max(convert(date,periodo,103)) 	From cf.DevengosMensuales  "
                                   +"					where nemotecnico = @Nemotecnico and nrovalor = @NroValor and convert(date,periodo,103) <= @fchCanje 	)"
                                   +" IF((SELECT MONTH(@InicioP)) != (SELECT MONTH(@fchCanje)) ) "
                                   +" BEGIN "
                                   +"	SET @InicioP = DATEADD(mm,DATEDIFF(mm,0,@fchCanje),0) "
                                   +"	SET @DiasOriginales= +1 "
                                   +"	SET @DiasPeriodo = +1 "
                                   +" END"
                                   +" SET @DiasOriginales=@DiasOriginales+ DATEDIFF(day,@InicioP,@Periodo) ;" 
                                   +" SET @DiasPeriodo = @DiasPeriodo + DATEDIFF(day,@InicioP,@fchCanje);"
                                   +" IF((SELECT MONTH(@Periodo)) = 2) "
                                   +" BEGIN 	SET @DiasOriginales = 30 END "
                                   +" SELECT NroValor, Nemotecnico, @FCanje as Periodo, '' as IdDevengoMensual, IdDevengoIntFK,"
                                   +" @DiasPeriodo as DiasPeriodo , InteresTotal / @DiasOriginales * @DiasPeriodo as InteresTotal, "
                                   +" Cupon / @DiasOriginales * @DiasPeriodo as Cupon , Descuento / @DiasOriginales *@DiasPeriodo as Descuento,"
                                   +" UsrCreacion, FchCreacion, 'CANJE', FchModifica,FchContabilizacion,Asiento "
                                   +" FROM cf.DevengosMensuales WHERE nemotecnico = @Nemotecnico AND nrovalor = @NroValor AND periodo = convert(varchar,@Periodo,103);";

    //                               " DECLARE @fchCanje DATETIME, @Periodo DATETIME, @InicioP DATETIME, @DiasOriginales FLOAT;" +
    //                               " SET @fchCanje = convert(date,@FCanje,103);" +
    //" set @Periodo = (	select convert(date,periodo,103) From cf.DevengosMensuales where nemotecnico =@Nemotecnico and nrovalor = @NroValor" +
    //" and MONTH(convert(date,periodo,103)) = MONTH(@fchCanje) and YEAR(convert(date,periodo,103)) = YEAR(@fchCanje))" +
    //" SET @InicioP =(SELECT DATEADD(mm,DATEDIFF(mm,0,@Periodo),0) 'Primer día del mes actual');	SET @DiasOriginales= DATEDIFF(day,@InicioP,@Periodo) +1;" +
    //" IF((SELECT MONTH(@Periodo)) = 2) BEGIN SET @DiasOriginales = 30 END " +
    //" SELECT NroValor, Nemotecnico, @FCanje as Periodo, '' as IdDevengoMensual, IdDevengoIntFK, (select DATEDIFF(day,@InicioP,@fchCanje))+1 as DiasPeriodo , InteresTotal / @DiasOriginales * (DATEDIFF(day,@InicioP,@fchCanje)+1) as InteresTotal, Cupon / @DiasOriginales * (DATEDIFF(day,@InicioP,@fchCanje)+1) as Cupon , Descuento / @DiasOriginales * (DATEDIFF(day,@InicioP,@fchCanje)+1) as Descuento, UsrCreacion, FchCreacion, 'CANJE', FchModifica,FchContabilizacion,Asiento FROM cf.DevengosMensuales WHERE nemotecnico = @Nemotecnico AND nrovalor = @NroValor AND periodo = convert(varchar,@Periodo,103);";

                ldat_DevengoMensual = dinamico.ConsultarDinamico(consulta).Tables[0];
            }

            try
            {
                foreach (DataRow fila in ldat_DevengoMensual.Rows)
                {
                    lstr_NroValor = fila["Nrovalor"].ToString().Trim();
                    lstr_Nemotecnico = fila["NemoTecnico"].ToString().Trim();
                    DataTable dt_titulos = dinamico.ConsultarDinamico("select tipo from cf.titulosvalores where nrovalor = '" + lstr_NroValor + "' and nemotecnico = '" + lstr_Nemotecnico + "' and IndicadorCupon = 'V'").Tables[0];
                    
                    if (dt_titulos.Rows.Count>0)
                    {
                        lstr_TipoValor =  dt_titulos.Rows[0]["Tipo"].ToString().Trim();
                        if (lstr_TipoValor.Equals("Cero Cupón"))
                        {
                            res = DevengoInteresesCeroCupon(fila);
                        }
                        else
                        {
                            res = DevengoIntereses(fila);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res = "99 - Error: " + ex.ToString();
            }
            return res;
        }

        public string DevengoIntereses(DataRow ldr_DevengoMensual)
        {
            string lstr_Mensaje = "00 - Proceso Finalizado";
            try
            {
                lstr_Mensaje = ContabilizaDevengoInt(
                    ldr_DevengoMensual["NroValor"].ToString(),
                    ldr_DevengoMensual["NemoTecnico"].ToString(),
                    ldr_DevengoMensual["Periodo"].ToString(),
                    ldr_DevengoMensual["IdDevengoMensual"].ToString(),
                    ldr_DevengoMensual["IdDevengoIntFK"].ToString(),
                    Convert.ToInt32(ldr_DevengoMensual["DiasPeriodo"].ToString()),
                    Convert.ToDecimal(ldr_DevengoMensual["InteresTotal"].ToString()),
                    Convert.ToDecimal(ldr_DevengoMensual["Cupon"].ToString()),
                    Convert.ToDecimal(ldr_DevengoMensual["Descuento"].ToString()),
                    Convert.ToDateTime(ldr_DevengoMensual["FchModifica"].ToString()));
            }
            catch (Exception ex)
            {
                lstr_Mensaje = "99 - Error: " + ex.ToString();
            }
            return lstr_Mensaje;
        }

        public string DevengoInteresesCeroCupon(DataRow ldr_DevengoMensual)
        {
            string lstr_Mensaje = "00 - Proceso finalizado";
            try
            {
                lstr_Mensaje = ContabilizaDevengoIntCeroCupon(
                    ldr_DevengoMensual["NroValor"].ToString(),
                    ldr_DevengoMensual["NemoTecnico"].ToString(),
                    Convert.ToDecimal(ldr_DevengoMensual["InteresTotal"].ToString()),
                    //campos nuevos
                    Convert.ToDecimal(ldr_DevengoMensual["Cupon"].ToString()),
                    Convert.ToDecimal(ldr_DevengoMensual["Descuento"].ToString()),
                    //
                    Convert.ToDateTime(ldr_DevengoMensual["Periodo"].ToString()),
                    Convert.ToDateTime(ldr_DevengoMensual["FchModifica"].ToString()));
            }
            catch (Exception ex)
            {
                lstr_Mensaje = "99 - Error: " + ex.ToString();
            }
            return lstr_Mensaje;
        }

        private decimal Truncate(decimal value, int length)
        {
            return Math.Truncate(value * 100) / 100;
        }

        public string ContabilizaDevengoInt
            (string lstr_NroValor,
            string lstr_Nemotecnico,
            string lstr_Periodo,
            string lstr_IdDevengoMensual,
            string lstr_IdDevengoIntFK,
            int lint_DiasPeriodo,
            decimal ldec_InteresTotal,
            decimal ldec_Cupon,
            decimal ldec_Descuento,
            DateTime ldt_FchModifica)
        {
            string lstr_Mensaje = string.Empty;
            string lstr_codAsiento = String.Empty;
            try
            {

                wsDeudaInterna.wsDeudaInterna wsDeudaInterna = new wsDeudaInterna.wsDeudaInterna();
                DataTable ldat_Asiento = new DataTable();
                //Ejemplo Datatable
                // DataTable ldat_Asiento = RegistroContable();
                //DataTable ldat_Tira = new DataTable();

                RegistroContable LineaAsiento;
                List<RegistroContable> Asiento = new List<RegistroContable>();

                DataTable ldat_Tira;
                string lint_EsPublico = string.Empty;
                string lint_PlazoValor = string.Empty;
                string primadescuento = string.Empty;
                string lstr_Operacion = string.Empty;
                string lstr_NomOperacion = string.Empty;
                string lstr_Trasciende = string.Empty;
                string lstr_InteresesTira = string.Empty;

                DataRow ldr_TituloValor = dinamico.ConsultarDinamico("select * from cf.titulosvalores where nrovalor = " + lstr_NroValor.Trim() + " and nemotecnico = '" + lstr_Nemotecnico.Trim() + "' and indicadorcupon = 'V'").Tables[0].Rows[0];

                //Tipos de cambio
                decimal ldec_TipoCambioColones = Convert.ToDecimal(tipocambio.ConsultarTiposCambio("CRCN", DateTime.Today/*ldt_FchValor*/, "3140", "N").Tables[0].Rows[0]["Valor"].ToString());
                decimal ldec_TipoCambioUDE = Convert.ToDecimal(tipocambio.ConsultarTiposCambio("UDE", DateTime.Today/*ldt_FchValor*/, "", "N").Tables[0].Rows[0]["Valor"].ToString());

                decimal ldec_monto = 0;
                string lstr_Referencia = "";

                //Determina si no tiene ni prima ni descuento
                bool sinPrimaDescuento = Convert.ToDecimal(ldr_TituloValor["ValorFacial"].ToString()) == Convert.ToDecimal(ldr_TituloValor["ValorTransadoBruto"].ToString()) ? true : false;

                DateTime FchPeriodo = Convert.ToDateTime(lstr_Periodo);
                DateTime FchVencimiento = Convert.ToDateTime(ldr_TituloValor["FchVencimiento"]);

                //Plazo
                if (utilitario.DiferenciaFechas(FchPeriodo, FchVencimiento))
                    lint_PlazoValor = "LP";
                else
                    lint_PlazoValor = "CP";

                //Propietario
                if (propietario.ConsultarPropietarios(string.Empty, string.Empty, string.Empty, ldr_TituloValor["Propietario"].ToString(), "S").Tables[0].Rows.Count == 0)
                    lint_EsPublico = "PRIVADO";
                else
                    lint_EsPublico = "PUBLICO";

                //Trascendencia
                if (Convert.ToDateTime(ldr_TituloValor["FchValor"].ToString()).Year != Convert.ToDateTime(ldr_TituloValor["FchVencimiento"].ToString()).Year)
                    lstr_Trasciende = "T";
                else
                    lstr_Trasciende = "NT";

                //Moneda
                string lstr_Moneda = ldr_TituloValor["Moneda"].ToString();

                //Prima o Descuento
                if (Convert.ToDecimal(ldr_TituloValor["ValorFacial"].ToString()) >= Convert.ToDecimal(ldr_TituloValor["ValorTransadoBruto"].ToString()))
                    primadescuento = "IMP_DEV"; //Descuento
                else
                    primadescuento = "PRIMAS";

                //Operación de devengo
                lstr_Operacion = lstr_Moneda.ToString().Equals("USD") ? "ID12" : (lstr_Moneda.ToString().Equals("CRC") ? "ID11" : "ID11");

                DataSet lds_Operaciones = loperacion.ConsultarOperaciones(lstr_Operacion, "IdModulo IN ('DI')", "");
                if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
                {
                    lstr_NomOperacion = lds_Operaciones.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
                }

                lstr_InteresesTira = (lstr_Nemotecnico.StartsWith("PT") ? "PT" : lstr_Trasciende);

                //Construcción de la tira
                ldat_Tira = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID").Tables[0].Clone();

                DataRow[] rows = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID", "", lstr_InteresesTira).Tables[0].Select("CodigoAuxiliar3 = ''");
                DataTable dt_tiras = rows.Count() > 0 ? rows.CopyToDataTable() : ldat_Tira.Clone();
                foreach (DataRow dr_tira in dt_tiras.Rows)
                    ldat_Tira.ImportRow(dr_tira);

                rows = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", lint_PlazoValor, "INT_DEV").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico.Trim() + "'");
                dt_tiras = rows.Count() > 0 ? rows.CopyToDataTable() : ldat_Tira.Clone();
                foreach (DataRow dr_tira in dt_tiras.Rows)
                    ldat_Tira.ImportRow(dr_tira);

                if (primadescuento != "NA")
                {
                    rows = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", lint_PlazoValor, primadescuento).Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico.Trim() + "'");
                    dt_tiras = rows.Count() > 0 ? rows.CopyToDataTable() : ldat_Tira.Clone();
                    foreach (DataRow dr_tira in dt_tiras.Rows)
                        ldat_Tira.ImportRow(dr_tira);
                }

                foreach (DataRow ldr_Row in ldat_Tira.Rows)
                {
                    string operacion = ldr_Row["CodigoAuxiliar2"].ToString().Trim();

                    switch (operacion)
                    {
                        case "T":
                        case "NT":
                        case "PT": { ldec_monto = ldr_TituloValor["Moneda"].ToString().Equals("USD") ? ldec_InteresTotal : (ldr_TituloValor["Moneda"].ToString().Equals("CRCN") ? ldec_InteresTotal : ldec_InteresTotal * ldec_TipoCambioUDE); break; }
                        case "INT_DEV": { ldec_monto = ldr_TituloValor["Moneda"].ToString().Equals("USD") ? ldec_Cupon : (ldr_TituloValor["Moneda"].ToString().Equals("CRCN") ? ldec_Cupon : ldec_Cupon * ldec_TipoCambioUDE); break; }
                        case "PRIMAS": { ldec_monto = ldr_TituloValor["Moneda"].ToString().Equals("USD") ? ldec_Descuento : (ldr_TituloValor["Moneda"].ToString().Equals("CRCN") ? ldec_Descuento : ldec_Descuento * ldec_TipoCambioUDE); break; }
                        case "IMP_DEV": { ldec_monto = ldr_TituloValor["Moneda"].ToString().Equals("USD") ? ldec_Descuento : (ldr_TituloValor["Moneda"].ToString().Equals("CRCN") ? ldec_Descuento : ldec_Descuento * ldec_TipoCambioUDE); break; }
                    }

                    lstr_Referencia = lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Devengo";

                    LineaAsiento = new RegistroContable();

                    LineaAsiento.Lstr_Referencia = lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia;
                    LineaAsiento.Lstr_Fecha = Convert.ToDateTime(lstr_Periodo).ToString("dd.MM.yyyy");
                    LineaAsiento.Lstr_Cuenta = ldr_Row["IdCuentaContable"].ToString().Trim();

                    //LineaAsiento.Lstr_ClaveContable =
                    //    (operacion.Equals("IMP_DEV")) ? 
                    //    (sinPrimaDescuento ? (ldec_monto > 0 ? "50" : "40") : ldr_Row["IdClaveContable"].ToString().Trim()) : 
                    //    ldr_Row["IdClaveContable"].ToString().Trim();

                    if (operacion.Equals("IMP_DEV"))
                    {
                        if (ldec_monto > 0)
                        {
                            LineaAsiento.Lstr_ClaveContable = "50";
                        }
                        else
                        {
                            LineaAsiento.Lstr_ClaveContable = "40";
                        }
                    }
                    else if (operacion.Equals("PRIMAS"))
                    {
                        if (ldec_monto > 0)
                        {
                            LineaAsiento.Lstr_ClaveContable = "50";
                        }
                        else
                        {
                            LineaAsiento.Lstr_ClaveContable = "40";
                        }
                    }
                    else
                    {
                        LineaAsiento.Lstr_ClaveContable = ldr_Row["IdClaveContable"].ToString().Trim();
                    }

                    LineaAsiento.Lstr_Moneda = ldr_Row["CodigoAuxiliar"].ToString().Trim().Substring(0, 3);
                    LineaAsiento.Lstr_TextoInfo = "".Trim().Length > 50 ? "".Trim().Substring(0, 47) + "..." : "".Trim();
                    LineaAsiento.Lstr_CentroCosto = ldr_Row["IdCentroCosto"].ToString().Trim();
                    LineaAsiento.Lstr_CentroBeneficio = ldr_Row["IdCentroBeneficio"].ToString().Trim();
                    LineaAsiento.Lstr_ElementoPEP = ldr_Row["IdElementoPEP"].ToString().Trim();
                    LineaAsiento.Lstr_PosPre = ldr_Row["IdPosPre"].ToString().Trim().ToUpper(); 
                    LineaAsiento.Lstr_CentroGestor = ldr_Row["IdCentroGestor"].ToString().Trim();
                    LineaAsiento.Lstr_Fondo = ldr_Row["IdFondo"].ToString().Trim();
                    LineaAsiento.Lstr_DocPres = ldr_Row["DocPresupuestario"].ToString().Trim();
                    LineaAsiento.Lstr_PosDocPres = ldr_Row["PosDocPresupuestario"].ToString().Trim();
                    LineaAsiento.Ldec_Monto = Truncate(Math.Abs(ldec_monto), 2);
                    LineaAsiento.Ldec_TipoCambio = ldec_TipoCambioColones;

                    Asiento.Add(LineaAsiento);
                }
                GenerarAsientoAjuste(Asiento, lstr_Operacion, lstr_NomOperacion, lstr_NroValor, lstr_Nemotecnico, ldt_FchModifica, Convert.ToDateTime(FchPeriodo), ldec_InteresTotal, ldec_Cupon, ldec_Descuento);

                return lstr_Mensaje;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public string ContabilizaDevengoIntCeroCupon
            (string lstr_NroValor,
            string lstr_Nemotecnico,
            decimal ldec_Interes,
            decimal ldec_Cupon,
            decimal ldec_Descuento,
            DateTime ldec_FchAccion,
            DateTime ldt_FchModifica)
        {
            try
            {
                string lstr_Mensaje = string.Empty;
                wsDeudaInterna.wsDeudaInterna wsDeudaInterna = new wsDeudaInterna.wsDeudaInterna();
                DataTable ldat_Asiento = new DataTable();

                RegistroContable LineaAsiento;
                List<RegistroContable> Asiento = new List<RegistroContable>();

                DataTable ldat_Tira = new DataTable();
                string lint_EsPublico = string.Empty;
                string lint_PlazoValor = string.Empty;
                string primadescuento = string.Empty;
                string lstr_Operacion = string.Empty;
                string lstr_NomOperacion = string.Empty;
                string lstr_Trasciende = string.Empty;
                string lstr_InteresesTira = string.Empty;

                DataRow ldr_TituloValor = dinamico.ConsultarDinamico("select * from cf.titulosvalores where nrovalor = " + lstr_NroValor.Trim() + " and nemotecnico = '" + lstr_Nemotecnico.Trim() + "' and indicadorcupon = 'V'").Tables[0].Rows[0];

                decimal ldec_TipoCambioColones = Convert.ToDecimal(tipocambio.ConsultarTiposCambio("CRCN", DateTime.Today/*ldt_FchValor*/, "3140", "N").Tables[0].Rows[0]["Valor"].ToString());
                decimal ldec_TipoCambioUDE = Convert.ToDecimal(tipocambio.ConsultarTiposCambio("UDE", DateTime.Today/*ldt_FchValor*/, "", "N").Tables[0].Rows[0]["Valor"].ToString());

                decimal ldec_monto = 0;
                string lstr_Referencia = "";

                DateTime FchVencimiento = Convert.ToDateTime(ldr_TituloValor["FchVencimiento"]);

                //Plazo
                if (utilitario.DiferenciaFechas(ldec_FchAccion, FchVencimiento))
                    lint_PlazoValor = "LP";
                else
                    lint_PlazoValor = "CP";

                //Propietario
                if (propietario.ConsultarPropietarios(string.Empty, string.Empty, string.Empty, ldr_TituloValor["Propietario"].ToString(), "S").Tables[0].Rows.Count == 0)
                    lint_EsPublico = "PRIVADO";
                else
                    lint_EsPublico = "PUBLICO";

                //Trascendencia
                if (Convert.ToDateTime(ldr_TituloValor["FchValor"].ToString()).Year != Convert.ToDateTime(ldr_TituloValor["FchVencimiento"].ToString()).Year)
                    lstr_Trasciende = "T";
                else
                    lstr_Trasciende = "NT";

                //Moneda
                string lstr_Moneda = ldr_TituloValor["Moneda"].ToString();

                //Operación de devengo
                lstr_Operacion = lstr_Moneda.ToString().Equals("USD") ? "ID12" : (lstr_Moneda.Equals("CRC") ? "ID11" : "ID11");

                DataSet lds_Operaciones = loperacion.ConsultarOperaciones(lstr_Operacion, "IdModulo IN ('DI')", "");
                if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
                {
                    lstr_NomOperacion = lds_Operaciones.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
                }

                lstr_InteresesTira = (lstr_Nemotecnico.StartsWith("PT") ? "PT" : lstr_Trasciende);

                //Construcción de la tira
                ldat_Tira = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID").Tables[0].Clone();

                DataRow[] rows = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "ID", "", lstr_InteresesTira).Tables[0].Select("CodigoAuxiliar3 = ''");
                DataTable dt_tiras = rows.Count() > 0 ? rows.CopyToDataTable() : ldat_Tira.Clone();
                foreach (DataRow dr_tira in dt_tiras.Rows)
                    ldat_Tira.ImportRow(dr_tira);

                rows = tipoasiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lint_EsPublico, "ID", lint_PlazoValor, "IMP_DEV").Tables[0].Select("CodigoAuxiliar3 = '" + lstr_Nemotecnico.Trim() + "'");
                dt_tiras = rows.Count() > 0 ? rows.CopyToDataTable() : ldat_Tira.Clone();
                foreach (DataRow dr_tira in dt_tiras.Rows)
                    ldat_Tira.ImportRow(dr_tira);

                foreach (DataRow ldr_Row in ldat_Tira.Rows)
                {
                    string operacion = ldr_Row["CodigoAuxiliar2"].ToString().Trim();

                    switch (operacion)
                    {
                        case "T":
                        case "NT":
                        case "PT": { ldec_monto = ldec_Interes; break; }
                        case "IMP_DEV": { ldec_monto = ldec_Interes; break; }
                    }

                    lstr_Referencia = lstr_NroValor.Trim() + "-" + lstr_Nemotecnico.Trim() + " Devengo";

                    LineaAsiento = new RegistroContable();

                    LineaAsiento.Lstr_Referencia = lstr_Referencia.Length > 25 ? lstr_Referencia.Substring(0, 22) + "..." : lstr_Referencia;
                    LineaAsiento.Lstr_Fecha = Convert.ToDateTime(ldec_FchAccion).ToString("dd.MM.yyyy");
                    LineaAsiento.Lstr_Cuenta = ldr_Row["IdCuentaContable"].ToString().Trim();
                    LineaAsiento.Lstr_ClaveContable = ldr_Row["IdClaveContable"].ToString().Trim();
                    LineaAsiento.Lstr_Moneda = ldr_Row["CodigoAuxiliar"].ToString().Trim().Substring(0, 3);
                    LineaAsiento.Lstr_TextoInfo = "".Trim().Length > 50 ? "".Trim().Substring(0, 47) + "..." : "".Trim();
                    LineaAsiento.Lstr_CentroCosto = ldr_Row["IdCentroCosto"].ToString().Trim();
                    LineaAsiento.Lstr_CentroBeneficio = ldr_Row["IdCentroBeneficio"].ToString().Trim();
                    LineaAsiento.Lstr_ElementoPEP = ldr_Row["IdElementoPEP"].ToString().Trim();
                    LineaAsiento.Lstr_PosPre = ldr_Row["IdPosPre"].ToString().Trim().ToUpper();
                    LineaAsiento.Lstr_CentroGestor = ldr_Row["IdCentroGestor"].ToString().Trim();
                    LineaAsiento.Lstr_Fondo = ldr_Row["IdFondo"].ToString().Trim();
                    LineaAsiento.Lstr_DocPres = ldr_Row["DocPresupuestario"].ToString().Trim();
                    LineaAsiento.Lstr_PosDocPres = ldr_Row["PosDocPresupuestario"].ToString().Trim();
                    LineaAsiento.Ldec_Monto = Truncate(Math.Abs(ldec_monto), 2);
                    LineaAsiento.Ldec_TipoCambio = ldec_TipoCambioColones;

                    Asiento.Add(LineaAsiento);
                }
                GenerarAsientoAjuste(Asiento, lstr_Operacion, lstr_NomOperacion, lstr_NroValor, lstr_Nemotecnico, ldt_FchModifica, ldec_FchAccion, ldec_Interes, ldec_Cupon, ldec_Descuento);
                return lstr_Mensaje;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public static void GenerarAsientoAjuste(List<RegistroContable> lrc_Asiento, string lstr_IdOperacion, string lstr_NomOperacion, string lstr_NroValor, string lstr_Nemotecnico, DateTime ldt_FchModifica, DateTime ldec_FchAccion, decimal ldec_InteresTotal, decimal ldec_Cupon, decimal ldec_Descuento)
        {
            //Coleccion de asientos y tipos de asientos requeridos en SAP expuestos por la referencia del servicio
            string lstr_codAsiento = string.Empty;
            wrSigafAsientos.ZfiAsiento item_asiento = new wrSigafAsientos.ZfiAsiento();
            wrSigafAsientos.ZfiAsiento[] tabla_asientos = new wrSigafAsientos.ZfiAsiento[lrc_Asiento.Count];
            int i = 0;
            i = lrc_Asiento.Count();
            decimal ldec_Total40 = 0;
            decimal ldec_Total50 = 0;
            decimal ldec_Diferencia40y50 = 0;
            int index = 0;
            //variables de proceso

            string lstr_Id = "";
            lstr_Id = lstr_NroValor + '.' + lstr_Nemotecnico;
            DateTime fechaContabilizacion = System.DateTime.Today;
            string[] item_resAsientosLog = new string[10];
            string logAsiento = string.Empty;
            string flagEstadoAsiento = string.Empty;
            DateTime ldt_FechaContabiliza = new DateTime();

            //---------borrar luego
            Type elementType = typeof(RegistroContable);
            DataSet ds = new DataSet();
            DataTable t = new DataTable();
            ds.Tables.Add(t);

            //add a column to table for each public property on T
            foreach (var propInfo in elementType.GetProperties())
            {
                t.Columns.Add(propInfo.Name, propInfo.PropertyType);
            }

            //go through each property on T and add each value to the table  usadas en pruebas
           // foreach (RegistroContable item in lrc_Asiento)
           // {
           //     if (item != null)
           //     {
           //         DataRow row = t.NewRow();
           //         foreach (var propInfo in elementType.GetProperties())
           //         {
           //             row[propInfo.Name] = propInfo.GetValue(item, null);
           //         }
           //         t.Rows.Add(row);
           //     }
           // }
          
           //ImprimeAsientos(t);
             //----------------------------------


            try
            {
                foreach (RegistroContable lrc_Linea in lrc_Asiento)
                {
                    item_asiento = new wrSigafAsientos.ZfiAsiento();
                    index = lrc_Asiento.IndexOf(lrc_Linea);

                    if (index == 0)
                    {
                        item_asiento.Blart = "ID";//Clase de documento
                        item_asiento.Bukrs = "G206";//Sociedad
                        item_asiento.Bldat = lrc_Linea.Lstr_Fecha;//Fecha de documento
                        item_asiento.Budat = lrc_Linea.Lstr_Fecha;//Fecha de contabilización
                        //ldt_FechaContabiliza = Convert.ToDateTime(lrc_Asiento);
                        ldt_FechaContabiliza = DateTime.ParseExact(lrc_Linea.Lstr_Fecha.ToString(), "dd.MM.yyyy", CultureInfo.InvariantCulture);

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
                    item_asiento.Fipex = lrc_Linea.Lstr_PosPre.ToUpper(); ;//Posición Presupuestaria
                    item_asiento.Fistl = lrc_Linea.Lstr_CentroGestor;//Centro Gestor
                    item_asiento.Geber = lrc_Linea.Lstr_Fondo;//Fondo
                    item_asiento.Kblnr = lrc_Linea.Lstr_DocPres;//Documento Presupuestario
                    item_asiento.Kblpos = lrc_Linea.Lstr_PosDocPres;//Posición de documento presupuestario

                    item_asiento.Xblnr = lstr_NroValor + "." + lstr_Nemotecnico;//ldat_Asiento.Rows[index]["PKMovimiento"].ToString();//
                    //item_asiento.Bktxt = "";//ldat_Asiento.Rows[index]["Texto2"].ToString();//
                    item_asiento.Xref1Hd = "";//ldat_Asiento.Rows[index]["Ref1Tipo"].ToString();//
                    item_asiento.Xref2Hd = lstr_IdOperacion +"."+ lstr_NomOperacion ;//ldat_Asiento.Rows[index]["Ref2Operacion"].ToString();//
                    if (lrc_Linea.Lstr_Moneda == "USD")
                        item_asiento.Kursf = Convert.ToDecimal(lrc_Linea.Ldec_TipoCambio.ToString("0.0000"));

                    if (item_asiento.Bschl == "40")
                    {
                        ldec_Total40 += item_asiento.Wrbtr;

                    }
                    else
                    {
                        ldec_Total50 += item_asiento.Wrbtr;

                    }
                    tabla_asientos[index] = item_asiento;
                }

                ldec_Diferencia40y50 = ldec_Total40 - ldec_Total50;

                Boolean lbl_cuadrado = false;
                wrSigafAsientos.ZfiAsiento[] tabla_asientos2 = new wrSigafAsientos.ZfiAsiento[index + 1];
                for (int y = 0; y < index + 1; y++)
                {
                    tabla_asientos2[y] = tabla_asientos[y];

                    if (!lbl_cuadrado && ldec_Diferencia40y50 != 0)
                    {
                        if (ldec_Diferencia40y50 > 0 && ldec_Diferencia40y50 < 1 && tabla_asientos2[y].Bschl == "50")
                        {//es mayor el 40 a los 50, subirle la diferencia al 50
                            tabla_asientos2[y].Wrbtr += ldec_Diferencia40y50;
                            lbl_cuadrado = true;
                        }
                        else
                        {//es mayor el 40 a los 50, subirle la diferencia al 50
                            if (ldec_Diferencia40y50 < 0 && ldec_Diferencia40y50 > -1 && tabla_asientos2[y].Bschl == "40")
                            {//es mayor el 50 a los 40, subirle la diferencia al 40

                                tabla_asientos2[y].Wrbtr += Math.Abs(ldec_Diferencia40y50);
                                lbl_cuadrado = true;
                            }
                        }
                    }
                }//for int y

                //Cargar de Asientos 
                string[] concatenado = new string[8];
                //envio de asiento mediante servicio web hacia SIGAF
                item_resAsientosLog = tasientos.EnviarAsientos(tabla_asientos2, "");
                for (int j = 0; j < item_resAsientosLog.Length; j++)
                {
                    int x = j + 1;
                    logAsiento += x + " - " + item_resAsientosLog[j] + " \n ";
                    lstr_codAsiento = item_resAsientosLog[0];
                }

                //Registrar en Bitacora de movimientos
                bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_IdOperacion, "DI"), "Resultado de Contabilización: " + logAsiento, lstr_IdOperacion, lstr_NroValor + "-" + lstr_Nemotecnico, "");


                string[] a = new string[2];
                //CHANTO
                if (!logAsiento.Contains("[E]"))
                {
                    //CHANTO
                    String fecha_con_formato_sql = String.Format("{0}/{1}/{2}", fechaContabilizacion.Year, fechaContabilizacion.Month, fechaContabilizacion.Day);
                    //String QUERY = String.Format("UPDATE cf.TitulosValores SET asiento = '{0}', FchContablilizado = '{1}' WHERE nrovalor = {2} AND nemotecnico = '{3}'", lstr_codAsiento, fecha_con_formato_sql, lstr_NroValor.Trim(), lstr_Nemotecnico.Trim());
                    String QUERY = String.Format("UPDATE cf.DevengosMensuales SET Asiento = '{0}', FchContabilizacion = '{1}' WHERE nrovalor = {2} AND nemotecnico = '{3}' AND periodo ='{4}'", lstr_codAsiento, fecha_con_formato_sql, lstr_NroValor.Trim(), lstr_Nemotecnico.Trim(), ldec_FchAccion.ToShortDateString());
                    dinamico.ConsultarDinamico(QUERY);

                }
                
            }
            catch (Exception ex)
            {
                // return ex.ToString();
            }
        }

        public static void ImprimeAsientos(DataTable tbl) 
        {
           
             string refe = tbl.Rows[0]["Lstr_Referencia"].ToString().Split(' ')[0];
             string consulta = "";
                foreach (DataRow fila in tbl.Rows) 
                {
                    consulta = "insert into [dbo].[AsientosPrueba] values ( 'Devengo',convert(date,'" + tbl.Rows[0]["Lstr_Fecha"] + "',103),'" + refe.Split('-')[1] + "'," + refe.Split('-')[0] + ", '" +
                        fila["Lstr_Cuenta"].ToString() +"',"+fila["Lstr_ClaveContable"].ToString()+","+fila["Ldec_Monto"].ToString()+",'"+ fila["Lstr_PosPre"].ToString()+"','"+fila["Lstr_CentroGestor"].ToString()+"')";

                    dinamico.ConsultarDinamico(consulta);
                }
         }

    }
}
