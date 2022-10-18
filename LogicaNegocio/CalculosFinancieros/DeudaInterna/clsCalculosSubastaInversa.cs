using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace LogicaNegocio.CalculosFinancieros.DeudaInterna
{
    public class clsCalculosSubastaInversa
    {
        ArrayList ArregloFecha = new ArrayList();
        int meses = 0;

        public int Days360(DateTime date, DateTime initialDate)
        {
            var dateA = initialDate;
            var dateB = date;
            var dayA = dateA.Day;
            var dayB = dateB.Day;
            if (UltimoDiaDeFebrero(dateA) && UltimoDiaDeFebrero(dateB))
                dayB = 30;
            if (dayA == 31 && UltimoDiaDeFebrero(dateA))
                dayA = 30;
            if (dayA == 30 && dayB == 31)
                dayB = 30;
            int days = (dateB.Year - dateA.Year) * 360 + ((dateB.Month + 1) - (dateA.Month + 1)) * 30 + (dayB - dayA);
            return days;
        }

        private static bool UltimoDiaDeFebrero(DateTime date)
        {
            int lastDay = DateTime.DaysInMonth(date.Year, 2);
            return date.Day == lastDay;
        }

        public decimal CalculoTIR(double[] arreglo)
        {
            double retorno = Microsoft.VisualBasic.Financial.IRR(ref arreglo, 0);
            return Convert.ToDecimal(retorno * 100);
        }

        public void PeriodosSemestrales(DateTime FchInicio, DateTime FchFin)
        {
            //arreglo para almacenar las fechas semestrales
            ArrayList ArregloFecha = new ArrayList();
            //valores de las fechas de inicio y de fin del proceso
            DateTime FchValor = new DateTime();
            DateTime FchVencimiento = new DateTime();
            //fecha temporal utilizada para almacenar el valor de la última fecha
            DateTime FchTemp = new DateTime();

            string fechas = string.Empty;
            //contador de cantidad de periodos del proceso
            int meses = 0;

            FchValor = FchInicio;
            FchVencimiento = FchFin;
            FchTemp = FchVencimiento;

            //recorre las fechas semestralmente hasta consumir los periodos
            while (FchTemp > FchValor)
            {
                if (meses == 0)
                {
                    ArregloFecha.Add(FchVencimiento);
                    FchTemp = FchVencimiento.AddMonths(-6);
                }
                else
                {
                    ArregloFecha.Add(FchTemp);
                    FchTemp = FchTemp.AddMonths(-6);
                }
                if (FchTemp < FchValor)
                {
                    ArregloFecha.Add(FchValor);
                    ArregloFecha.Add(Convert.ToDateTime("01/01/1900"));
                    meses = meses + 2;
                }
                meses++;
            }
        }


        #region devengo Subasta Inversa

        private Decimal CapitalFchSubasta = 0;
        private Decimal ImpDevengarFchSubasta = 0;
        private Decimal CuponCorridoFchSubasta = 0;
        private Decimal ValorEmision = 0;
        private Decimal PorcentajeEmision = 0;
        private Decimal ImpDevenarTranscurrido = 0;
        private Decimal CuponCorridoTranscurrido = 0;
        private Decimal CapitalDeBaja = 0;
        private Decimal ImpDevengarDeBaja = 0;
        private Decimal CuponCorridoDeBaja = 0;
        private Decimal ValorEmisionDeBaja = 0;
        private Decimal EntradaSalidaCaja = 0;
        private Decimal TotalColocado = 0;
        private Decimal NetoSubastado = 0;
        private Decimal TotalNetoBaja = 0;
        private Decimal Diferencia = 0;
        private Decimal Capital = 0;
        private Decimal IntDevengado = 0;
        private Decimal ImpRenta = 0;
        private Decimal Descuento = 0;

        private Decimal InteresFlujo = 0;
        private Decimal InteresDevengo = 0;
        private Decimal CostoAmortFinal = 0;
        private Decimal DescDevengado = 0;
        private Decimal Columna4 = 0;
        private DateTime PenultimaFecha = new DateTime();
        private DateTime FechaUltimoCupon = new DateTime();
        private int DiasPeriodo = 0;
        private Decimal ValorFacialUltimoCupon = 0;
        private Decimal RelacionSubasta = 0;
        private Decimal TransadoNetoUltimoCupon = 0;
        private Decimal Vencimiento = 0;
        private string NroEmision = String.Empty;

        public void CargaVariablesSubasta(string NroEmision, Decimal Vencimiento)
        {
            clsSubastaCanje lcls_Subasta = new clsSubastaCanje();
            try
            {
                InteresFlujo = Convert.ToDecimal(lcls_Subasta.ConsultarInteresFlujo(NroEmision).Tables[0].Rows[0]["Intereses"].ToString());
                InteresDevengo = Convert.ToDecimal(lcls_Subasta.ConsultarInteresDevengo(NroEmision).Tables[0].Rows[0]["Intereses"].ToString());
                CostoAmortFinal = Convert.ToDecimal(lcls_Subasta.ConsultarCostoAmortizacionFinal(NroEmision).Tables[0].Rows[0]["CostoAmortizacionFinal"].ToString());
                DescDevengado = Convert.ToDecimal(lcls_Subasta.ConsultarInteresFlujo(NroEmision).Tables[0].Rows[0]["DescuentoDevengado"].ToString());
                Columna4 = Convert.ToDecimal(lcls_Subasta.ConsultarColumnaDevengoMensualNroSerie(NroEmision).Tables[0].Rows[0]["Columna4"].ToString());
                PenultimaFecha = Convert.ToDateTime(lcls_Subasta.ConsultarPenultimaFechaDevengo(NroEmision).Tables[0].Rows[0]["FchInicio"].ToString());
                FechaUltimoCupon = Convert.ToDateTime(lcls_Subasta.ConsultarNroEmisionCompra(NroEmision).Tables[0].Rows[0]["FchFin"].ToString());
                DiasPeriodo = Days360(FechaUltimoCupon, PenultimaFecha);

                ValorFacialUltimoCupon = Convert.ToDecimal(lcls_Subasta.ConsultarNroEmisionCompra(NroEmision).Tables[0].Rows[0]["ValorFacial"].ToString());
                RelacionSubasta = ValorFacialUltimoCupon / Vencimiento;
                TransadoNetoUltimoCupon = Convert.ToDecimal(lcls_Subasta.ConsultarNroEmisionCompra(NroEmision).Tables[0].Rows[0]["ValorTransadoNeto"].ToString());
                //this.Vencimiento = Vencimiento;
                //this.NroEmision = NroEmision;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }



        //public void DevengoSubastaInversa()
        //{
        //    DataTable ldat_Valores = new DataTable();
        //    clsSubastaInversa lcls_SubastaInversa = new clsSubastaInversa();
        //    clsDevengoInteres lcls_DevengoInteres = new clsDevengoInteres();
        //    decimal ldec_TIR = 0;
        //    string lstr_NroEmision = String.Empty;
        //    decimal ldec_TransBruto = 0;
        //    decimal ldec_ValorFacial = 0;
        //    decimal ldec_CostAmortIni = 0;
        //    decimal ldec_Interes = 0;
        //    decimal ldec_CostAmortFin = 0;
        //    decimal ldec_Pago = 0;
        //    decimal ldec_PenultPago = 0;
        //    decimal ldec_Devengado = 0;
        //    int cont = 0;
        //    string mens1 = String.Empty;
        //    string mens2 = String.Empty;

        //    try
        //    {

        //        //ldat_Valores = lcls_TituloValor.ConsultarTituloValor(null, null, "TasaFija", null, null).Tables[0];
        //        ldat_Valores = lcls_SubastaInversa.ConsultarNroEmisionVenta().Tables[0];

        //        for (int i = 0; i < ldat_Valores.Rows.Count; i++)
        //        {
        //            ldec_TIR = CalculoTIR(FlujoEfectivoSubastaInversa(ldat_Valores.Rows[i]));

        //            lstr_NroEmision = ldat_Valores.Rows[i]["NroEmisionSerie"].ToString();
        //            ldec_TransBruto = Convert.ToDecimal(ldat_Valores.Rows[i]["ValorTransadoBruto"].ToString());
        //            ldec_ValorFacial = Convert.ToDecimal(ldat_Valores.Rows[i]["ValorFacial"].ToString());

        //            //carga las variables de subasta
        //            CargaVariablesSubasta(lstr_NroEmision, ldec_TransBruto);

        //            for (int j = 1; j < lint_ContPeriodos - 1; j++)
        //            {
        //                if (j == 1)
        //                {
        //                    ldec_CostAmortIni = ldec_TransBruto;
        //                    ldec_Interes = ldec_CostAmortIni * (ldec_TIR / 100);
        //                    ldec_Pago = gdec_TasaFija[j];
        //                    //
        //                    IntPrimerDevengo = ldec_Interes;
        //                    PagoPrimerDevengo = ldec_Pago;
        //                    //
        //                    ldec_CostAmortFin = ldec_CostAmortIni + ldec_Interes + ldec_Pago;
        //                    ldec_Devengado = ldec_Interes + ldec_Pago;

        //                    lcls_DevengoInteres.CrearDevengoInteresNroSerie(lstr_NroEmision, Convert.ToDateTime(fechas[j]), ldec_CostAmortIni,
        //                        ldec_Interes, ldec_Pago, ldec_CostAmortFin, ldec_Devengado, "ACT", "SG", out mens1, out mens2);
        //                }
        //                else
        //                {
        //                    ldec_CostAmortIni = ldec_CostAmortFin;
        //                    ldec_Interes = ldec_CostAmortIni * (ldec_TIR / 100);
        //                    ldec_Pago = gdec_TasaFija[j];
        //                    ldec_CostAmortFin = ldec_CostAmortIni + ldec_Interes + ldec_Pago;
        //                    ldec_Devengado = ldec_Interes + ldec_Pago;

        //                    lcls_DevengoInteres.CrearDevengoInteresNroSerie(lstr_NroEmision, Convert.ToDateTime(fechas[j]), ldec_CostAmortIni,
        //                        ldec_Interes, ldec_Pago, ldec_CostAmortFin, ldec_Devengado, "ACT", "SG", out mens1, out mens2);
        //                }
        //                cont = j;
        //                ldec_PenultPago = gdec_TasaFija[j];
        //            }
        //            ldec_CostAmortIni = ldec_CostAmortFin;
        //            ldec_Interes = ldec_CostAmortIni * (ldec_TIR / 100);
        //            ldec_Pago = gdec_TasaFija[cont + 1];
        //            ldec_CostAmortFin = ldec_CostAmortIni + ldec_Interes + ldec_Pago;
        //            ldec_Devengado = ldec_Interes + ldec_PenultPago;

        //            lcls_DevengoInteres.CrearDevengoInteresNroSerie(lstr_NroEmision, Convert.ToDateTime(fechas[cont + 1]), ldec_CostAmortIni,
        //                ldec_Interes, ldec_Pago, ldec_CostAmortFin, ldec_Devengado, "ACT", "SG", out mens1, out mens2);
        //            ldec_Pago = 0;
        //            ldec_Interes = 0;
        //            DevengoMensualNroSerie(NroEmision, PenultimaFecha, FechaUltimoCupon);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //    }
        //}

        //public double[] FlujoEfectivoSubastaInversa(DataRow ldrw_TasaFija)
        //{

        //    double[] ldec_ArregloTIR;
        //    string mens1 = String.Empty;
        //    string mens2 = String.Empty;
        //    clsCalculoFlujoEfectivo lcls_CalculoFlujoEfectivo = new clsCalculoFlujoEfectivo();
        //    string lstr_NroEmision = String.Empty;
        //    DateTime ldt_FchValor = new DateTime();
        //    DateTime ldt_FchVencimiento = new DateTime();
        //    decimal ldec_TransNeto = 0;
        //    decimal ldec_ValorFacial = 0;
        //    decimal ldec_TransBruto = 0;
        //    decimal ldec_PrimaDesc = 0;
        //    decimal ldec_PlazoValor = 0;
        //    bool lbol_EsLargoPlazo = true;
        //    decimal ldec_Intereses = 0;

        //    decimal ldec_TasaBruta = 0;
        //    decimal ldec_TasaMensual = 0; //TasaInteres

        //    try
        //    {
        //        lstr_NroEmision = ldrw_TasaFija["NroEmisionSerie"].ToString();
        //        ldt_FchValor = Convert.ToDateTime(ldrw_TasaFija["FchValor"].ToString()); //primer registro de la tabla
        //        ldt_FchVencimiento = Convert.ToDateTime(ldrw_TasaFija["FchVencimiento"].ToString()); //ultimo registro de la tabla
        //        ldec_TransNeto = Convert.ToDecimal(ldrw_TasaFija["ValorTransadoNeto"].ToString());
        //        ldec_ValorFacial = Convert.ToDecimal(ldrw_TasaFija["ValorFacial"].ToString()); //ultimo valor negativo del flujo de efectivo
        //        ldec_TransBruto = Convert.ToDecimal(ldrw_TasaFija["ValorTransadoBruto"].ToString()); //primer valor del flujo de efectivo
        //        ldec_TasaBruta = Convert.ToDecimal(ldrw_TasaFija["TasaBruta"].ToString());
        //        ldec_PlazoValor = Convert.ToDecimal(ldrw_TasaFija["PlazoValor"].ToString());
        //        ldec_PrimaDesc = ldec_ValorFacial - ldec_TransBruto;

        //        //averigua si el valor es a largo o corto plazo
        //        if (ldec_PlazoValor <= 365)
        //        {
        //            lbol_EsLargoPlazo = false;
        //            ldec_TasaMensual = ldec_TasaBruta / 12;
        //        }
        //        else
        //        {
        //            ldec_TasaMensual = ldec_TasaBruta / 2;
        //        }

        //        if (lbol_EsLargoPlazo)
        //        {
        //            PeriodosSemestrales(ldt_FchValor, ldt_FchVencimiento);
        //            //
        //            FchValor = ldt_FchValor;
        //            //
        //            gint_DiasCuponCorrido = 180 - Days360(ldt_FchValor, gdt_FchUltimoPago);
        //            lint_ContPeriodos = meses + 1;

        //            ldec_ArregloTIR = new double[lint_ContPeriodos];
        //            gdec_TasaFija = new decimal[lint_ContPeriodos];

        //            //lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivoNroSerie(lstr_NroEmision, ldt_FchValor.ToString("dd/MM/yyyy"), ldec_TasaMensual,
        //            //    0, ldec_TransBruto, "SG", out mens1, out mens2);

        //            lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivoNroSerie(lstr_NroEmision, "01/01/1900", ldec_TasaMensual,
        //                0, ldec_TransBruto, "SG", out mens1, out mens2);

        //            lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivoNroSerie(lstr_NroEmision, ldt_FchValor.ToString("dd/MM/yyyy"), ldec_TasaMensual,
        //                    ldec_Intereses, -ldec_Intereses, "SG", out mens1, out mens2);

        //            ldec_ArregloTIR[0] = Convert.ToDouble(ldec_TransBruto);
        //            gdec_TasaFija[0] = ldec_TransBruto;

        //            //
        //            ProxFecha = new DateTime[fechas.Length];

        //            for (int i = 1; i < lint_ContPeriodos - 1; i++)
        //            {
        //                if (i == 1)
        //                {
        //                    ldec_Intereses = +((ldec_ValorFacial * ldec_TasaMensual) / 180) * gint_DiasCuponCorrido;
        //                    //
        //                    //ProxFecha = Convert.ToDateTime(fechas[i]);
        //                    //ProxFecha[i] = Convert.ToDateTime(fechas[i]);
        //                    //
        //                    ldec_ArregloTIR[i] = Convert.ToDouble(-ldec_Intereses);
        //                    gdec_TasaFija[i] = -ldec_Intereses;
        //                }
        //                else
        //                {
        //                    ldec_Intereses = ldec_ValorFacial * ldec_TasaMensual;
        //                    //
        //                    //
        //                }

        //                ProxFecha[i - 1] = Convert.ToDateTime(fechas[i]);

        //                lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivoNroSerie(lstr_NroEmision, fechas[i], ldec_TasaMensual,
        //                    ldec_Intereses, -ldec_Intereses, "SG", out mens1, out mens2);
        //                ldec_ArregloTIR[i] = Convert.ToDouble(-ldec_Intereses);
        //                gdec_TasaFija[i] = -ldec_Intereses;
        //            }

        //            ProxFecha[fechas.Length - 1] = Convert.ToDateTime(ldt_FchVencimiento.ToString("dd/MM/yyyy"));

        //            lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivoNroSerie(lstr_NroEmision, ldt_FchVencimiento.ToString("dd/MM/yyyy"), ldec_TasaMensual,
        //                ldec_Intereses, (-ldec_Intereses - ldec_ValorFacial), "SG", out mens1, out mens2);
        //            ldec_ArregloTIR[lint_ContPeriodos - 1] = Convert.ToDouble(-ldec_Intereses - ldec_ValorFacial);
        //            gdec_TasaFija[lint_ContPeriodos - 1] = (-ldec_Intereses - ldec_ValorFacial);

        //            gint_DiasCuponCorrido = 0;

        //            return ldec_ArregloTIR;
        //        }
        //        else
        //        {
        //            PeriodosMensuales(ldt_FchValor, ldt_FchVencimiento);
        //            lint_ContPeriodos = meses;

        //            ldec_ArregloTIR = new double[lint_ContPeriodos];
        //            gdec_TasaFija = new decimal[lint_ContPeriodos];

        //            //lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivoNroSerie(lstr_NroEmision, ldt_FchValor.ToString("dd/MM/yyyy"), ldec_TasaMensual,
        //            //    0, ldec_TransBruto, "SG", out mens1, out mens2);
        //            lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivoNroSerie(lstr_NroEmision, "01/01/1900", ldec_TasaMensual,
        //                0, ldec_TransBruto, "SG", out mens1, out mens2);

        //            lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivoNroSerie(lstr_NroEmision, ldt_FchValor.ToString("dd/MM/yyyy"), ldec_TasaMensual,
        //                    ldec_Intereses, -ldec_Intereses, "SG", out mens1, out mens2);

        //            ldec_ArregloTIR[0] = Convert.ToDouble(ldec_TransBruto);
        //            gdec_TasaFija[0] = ldec_TransBruto;

        //            ldec_Intereses = ldec_ValorFacial * ldec_TasaMensual;

        //            for (int i = 1; i < lint_ContPeriodos - 1; i++)
        //            {
        //                if (i == 1)
        //                {
        //                    ldec_ArregloTIR[i] = Convert.ToDouble(-ldec_Intereses);
        //                    gdec_TasaFija[i] = -ldec_Intereses;
        //                }
        //                lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivoNroSerie(lstr_NroEmision, fechas[i], ldec_TasaMensual,
        //                    ldec_Intereses, -ldec_Intereses, "SG", out mens1, out mens2);
        //                ldec_ArregloTIR[i] = Convert.ToDouble(-ldec_Intereses);
        //                gdec_TasaFija[i] = -ldec_Intereses;
        //            }

        //            lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivoNroSerie(lstr_NroEmision, ldt_FchVencimiento.ToString("dd/MM/yyyy"), ldec_TasaMensual,
        //                ldec_Intereses, (-ldec_Intereses - ldec_ValorFacial), "SG", out mens1, out mens2);
        //            ldec_ArregloTIR[lint_ContPeriodos - 1] = Convert.ToDouble(-ldec_Intereses - ldec_ValorFacial);
        //            gdec_TasaFija[lint_ContPeriodos - 1] = (-ldec_Intereses - ldec_ValorFacial);

        //            gint_DiasCuponCorrido = 0;

        //            return ldec_ArregloTIR;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //        return null;
        //    }
        //}

        //public void DevengoMensualNroSerie(string lstr_NroEmision, DateTime FchInicio, DateTime FchFin)
        //{
        //    int TotalDiasDif = 0;
        //    int DiasDif = 0;

        //    decimal Columna1 = 0;
        //    decimal Columna2 = 0;
        //    decimal Columna3 = 0;
        //    decimal Columna4 = 0;

        //    string mens1 = String.Empty;
        //    string mens2 = String.Empty;

        //    clsDevengoMensual lcls_DevengoMensual = new clsDevengoMensual();
        //    clsSubastaInversa lcls_Subasta = new clsSubastaInversa();

        //    FchTemp = PeriodosMensualesDevengo(FchInicio, FchFin);//contiene las fechas en el periodo indicado
        //    TotalDiasDif = +(Days360(FchFin, FchInicio));

        //    try
        //    {
        //        for (int i = 0; i < mesesDev; i++)
        //        {
        //            if (i == 0)
        //            {
        //                DateTime FchTemporal = Convert.ToDateTime(FchTemp[i]);
        //                DiasDif = +(Days360(FchTemporal, FchInicio));
        //                if (DiasDif > 30)
        //                {
        //                    DiasDif = 30;
        //                }

        //                Columna1 = CostoAmortFinal;
        //                Columna2 = (InteresDevengo / 180) * DiasDif;
        //                Columna3 = (InteresFlujo / 180) * DiasDif;
        //                Columna4 = +Columna1 + Columna2 - Columna3;
        //            }
        //            else
        //            {
        //                DateTime FchTemporal1 = Convert.ToDateTime(FchTemp[i]);
        //                DateTime FchTemporal2 = Convert.ToDateTime(FchTemp[i - 1]);
        //                DiasDif = +(Days360(FchTemporal1, FchTemporal2));
        //                if (DiasDif > 30)
        //                {
        //                    DiasDif = 30;
        //                }

        //                Columna1 = Columna4;
        //                Columna2 = (InteresDevengo / 180) * DiasDif;
        //                Columna3 = (InteresFlujo / 180) * DiasDif;
        //                Columna4 = +Columna1 + Columna2 - Columna3;
        //            }

        //            if (DiasDif > 0)
        //            {
        //                lcls_DevengoMensual.CrearDevengoMensualNroSerie(lstr_NroEmision, FchTemp[i], DiasDif, Columna1, Columna2, Columna3,
        //                    Columna4, "SG", out mens1, out mens2);
        //            }
        //        }

        //        CapitalFchSubasta = Columna4;
        //        ImpDevengarFchSubasta = (DescDevengado / 180) * TotalDiasDif;
        //        CuponCorridoFchSubasta = (InteresFlujo / 180) * TotalDiasDif;
        //        ValorEmision = +CapitalFchSubasta + ImpDevengarFchSubasta - CuponCorridoFchSubasta;
        //        PorcentajeEmision = ValorEmision / RelacionSubasta;
        //        ImpDevenarTranscurrido = ImpDevengarFchSubasta * RelacionSubasta;
        //        CuponCorridoTranscurrido = CuponCorridoFchSubasta * RelacionSubasta;
        //        CapitalDeBaja = PorcentajeEmision;
        //        ImpDevengarDeBaja = (DescDevengado * RelacionSubasta) - ImpDevenarTranscurrido;
        //        CuponCorridoDeBaja = CuponCorridoTranscurrido;
        //        ValorEmisionDeBaja = CapitalDeBaja - ImpDevengarDeBaja + CuponCorridoDeBaja;
        //        EntradaSalidaCaja = TransadoNetoUltimoCupon;
        //        TotalColocado = Capital + IntDevengado + ImpRenta + Descuento;
        //        NetoSubastado = +TotalColocado - EntradaSalidaCaja;
        //        TotalNetoBaja = ValorEmisionDeBaja;
        //        Diferencia = EntradaSalidaCaja - ValorEmisionDeBaja;

        //        lcls_Subasta.CrearEmisionSubasta(NroEmision, CapitalFchSubasta, ImpDevengarFchSubasta, CuponCorridoFchSubasta, ValorEmision,
        //            PorcentajeEmision, ImpDevenarTranscurrido, CuponCorridoTranscurrido, CapitalDeBaja, ImpDevengarDeBaja,
        //            CuponCorridoDeBaja, ValorEmisionDeBaja, EntradaSalidaCaja, NetoSubastado, TotalNetoBaja, Diferencia, Capital,
        //            InteresDevengo, ImpRenta, Descuento, TotalColocado, "SG", out mens1, out mens2);
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //    }
        //}

        #endregion

    }
}