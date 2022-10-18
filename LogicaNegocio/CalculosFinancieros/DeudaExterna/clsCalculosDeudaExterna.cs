﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
using log4net;
using log4net.Config;
using LogicaNegocio.Mantenimiento;
using LogicaNegocio.Seguridad;
using LogicaNegocio.CalculosFinancieros.DeudaInterna;
using Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna;

namespace LogicaNegocio.CalculosFinancieros.DeudaExterna
{
    public class clsCalculosDeudaExterna
    {
        private int lint_ContPeriodos = 0;
        private decimal[] gdec_TasaFija;
        private decimal[] gdec_TasaVariable;
        private DateTime[] gdt_Fechas;
        private static clsCalculosDeudaInterna calcDI = new clsCalculosDeudaInterna();
        private static clsTiposAsiento tAsiento = new clsTiposAsiento();
        private static Mantenimiento.clsDinamico dinamica = new Mantenimiento.clsDinamico();
        private static clsTramo ltr_Tramo = new clsTramo();
        private static string lstr_formato_fecha = "dd/MM/yyyy";
        private string[] fechas;
        private int meses = 0;
        private int mesesDev = 0;
        private string[] FchTemp;

        private static readonly ILog Log = LogManager.GetLogger("FileAppender");
        //private wrSigafAsientos.ServicioContable asientos = new wrSigafAsientos.ServicioContable();
        private tBitacora reg_Bitacora = new tBitacora();

        private string resAsientosLog = string.Empty;

        #region Indicadores Financieros

        //public DataTable ActualizaIndicadoresEconomicos(string lstr_usuario)
        //{
        //    DataTable ldt_IndicadoresBCCR = new DataTable();
        //    clsTipoCambioBCCR lcls_TipoCambio = new clsTipoCambioBCCR();
        //    string lstr_FechaActual = "";
        //    string lstr_Indicador = "";
        //    string lstr_Transaccion = "";
        //    decimal ldec_TipoCambio;
        //    DateTime ldt_FechaActual = DateTime.UtcNow.Date;

        //    ldt_IndicadoresBCCR = lcls_TipoCambio.consultarIndicadoresEcoBCCR().Tables[0];

        //    for (int lint_fila = 0; lint_fila < ldt_IndicadoresBCCR.Rows.Count; lint_fila++)
        //    {
        //        ArrayList larr_IndicadoresEco = new ArrayList();

        //        lstr_Indicador = ldt_IndicadoresBCCR.Rows[lint_fila][0].ToString();
        //        lstr_Transaccion = ldt_IndicadoresBCCR.Rows[lint_fila][1].ToString();
        //        lstr_FechaActual = ldt_FechaActual.ToString(lstr_formato_fecha);
        //        ldec_TipoCambio = ObtieneTipoCambioBCCR(lstr_Transaccion, lstr_FechaActual);

        //        larr_IndicadoresEco.Add(lstr_Indicador);
        //        //larr_IndicadoresEco.Add(lstr_Transaccion);
        //        larr_IndicadoresEco.Add(ldt_FechaActual);
        //        larr_IndicadoresEco.Add(ldec_TipoCambio);
        //        larr_IndicadoresEco.Add(lstr_usuario);

        //        lcls_TipoCambio.registrarIndicadoresEcoBCCR(larr_IndicadoresEco);
        //    }

        //    return ldt_IndicadoresBCCR;
        //}

        public DataTable ActualizaIndicadoresEconomicos(string lstr_usuario)
        {
            DataTable ldt_IndicadoresBCCR = new DataTable();
            clsTipoCambioBCCR lcls_TipoCambio = new clsTipoCambioBCCR();
            string lstr_FechaActual = "";
            string lstr_Indicador = "";
            string lstr_Transaccion = "";
            decimal ldec_TipoCambio;
            DateTime ldt_InicioCarga = Convert.ToDateTime("01/04/2016");
            DateTime ldt_FechaActual = DateTime.UtcNow.Date;

            ldt_IndicadoresBCCR = lcls_TipoCambio.consultarIndicadoresEcoBCCR().Tables[0];

            while (ldt_InicioCarga != ldt_FechaActual)
            {
                for (int lint_fila = 0; lint_fila < ldt_IndicadoresBCCR.Rows.Count; lint_fila++)
                {
                    ArrayList larr_IndicadoresEco = new ArrayList();

                    lstr_Indicador = ldt_IndicadoresBCCR.Rows[lint_fila][0].ToString();
                    lstr_Transaccion = ldt_IndicadoresBCCR.Rows[lint_fila][1].ToString();
                    lstr_FechaActual = ldt_InicioCarga.ToString(lstr_formato_fecha);
                    ldec_TipoCambio = ObtieneTipoCambioBCCR(lstr_Transaccion, lstr_FechaActual);

                    larr_IndicadoresEco.Add(lstr_Indicador);
                    //larr_IndicadoresEco.Add(lstr_Transaccion);
                    larr_IndicadoresEco.Add(ldt_InicioCarga);
                    larr_IndicadoresEco.Add(ldec_TipoCambio);
                    larr_IndicadoresEco.Add(lstr_usuario);

                    lcls_TipoCambio.registrarIndicadoresEcoBCCR(larr_IndicadoresEco);
                }
                ldt_InicioCarga = ldt_InicioCarga.AddDays(1);
            }

            return ldt_IndicadoresBCCR;
        }

        //public DataTable ActualizaTipoCambio(string lstr_usuario)
        //{
        //    DataTable ldt_ISOyBCCR = new DataTable();
        //    clsTipoCambioBCCR lcls_TipoCambio = new clsTipoCambioBCCR();
        //    string lstr_FechaActual = "";
        //    string lstr_Indicador = "";
        //    string lstr_IdMoneda = "";
        //    decimal ldec_TipoCambio;
        //    DateTime ldt_FechaActual = DateTime.UtcNow.Date;
        //    DateTime ldt_FechaActualNB = DateTime.UtcNow.Date.AddDays(-1);

        //    ldt_ISOyBCCR = lcls_TipoCambio.consultarISOconBCCR().Tables[0];

        //    for (int lint_fila = 0; lint_fila < ldt_ISOyBCCR.Rows.Count; lint_fila++)
        //    {
        //        ArrayList larr_TipoCambio = new ArrayList();
        //        lstr_IdMoneda = ldt_ISOyBCCR.Rows[lint_fila][0].ToString();
        //        lstr_Indicador = ldt_ISOyBCCR.Rows[lint_fila][2].ToString();
        //        if (lstr_IdMoneda != "CRCN")
        //        {
        //            lstr_FechaActual = ldt_FechaActual.ToString(lstr_formato_fecha);
        //        }
        //        else
        //        {
        //            lstr_FechaActual = ldt_FechaActualNB.ToString(lstr_formato_fecha);
        //        }
        //        ldec_TipoCambio = ObtieneTipoCambioBCCR(lstr_Indicador, lstr_FechaActual);

        //        larr_TipoCambio.Add(lstr_IdMoneda);
        //        if (lstr_IdMoneda != "CRCN")
        //        {
        //            larr_TipoCambio.Add(ldt_FechaActual);
        //        }
        //        else
        //        {
        //            larr_TipoCambio.Add(ldt_FechaActualNB);
        //        }
        //        larr_TipoCambio.Add(lstr_Indicador);
        //        larr_TipoCambio.Add(ldec_TipoCambio);
        //        larr_TipoCambio.Add(lstr_usuario);

        //        lcls_TipoCambio.registrarTipoCambioBCCR(larr_TipoCambio);
        //    }

        //    return ldt_ISOyBCCR;
        //}

        public DataTable ActualizaTipoCambio(string lstr_usuario)
        {
            DataTable ldt_ISOyBCCR = new DataTable();
            clsTipoCambioBCCR lcls_TipoCambio = new clsTipoCambioBCCR();
            string lstr_FechaActual = "";
            string lstr_Indicador = "";
            string lstr_IdMoneda = "";
            decimal ldec_TipoCambio;
            DateTime ldt_InicioCarga = Convert.ToDateTime("23/06/2016");
            DateTime ldt_FechaActual = DateTime.UtcNow.Date;
            DateTime ldt_FechaActualNB = DateTime.UtcNow.Date.AddDays(-1);

            ldt_ISOyBCCR = lcls_TipoCambio.consultarISOconBCCR().Tables[0];

            while (ldt_InicioCarga != ldt_FechaActual)
            {
                for (int lint_fila = 0; lint_fila < ldt_ISOyBCCR.Rows.Count; lint_fila++)
                {
                    ArrayList larr_TipoCambio = new ArrayList();
                    lstr_IdMoneda = ldt_ISOyBCCR.Rows[lint_fila][0].ToString();
                    lstr_Indicador = ldt_ISOyBCCR.Rows[lint_fila][2].ToString();

                    if (lstr_IdMoneda != "CRCN")
                    {
                        lstr_FechaActual = ldt_InicioCarga.ToString(lstr_formato_fecha);
                    }
                    else
                    {
                        lstr_FechaActual = ldt_InicioCarga.ToString(lstr_formato_fecha);
                    }

                    ldec_TipoCambio = ObtieneTipoCambioBCCR(lstr_Indicador, lstr_FechaActual);

                    larr_TipoCambio.Add(lstr_IdMoneda);

                    if (lstr_IdMoneda != "CRCN")
                    {
                        larr_TipoCambio.Add(ldt_InicioCarga);
                    }
                    else
                    {
                        larr_TipoCambio.Add(ldt_InicioCarga);
                    }

                    larr_TipoCambio.Add(lstr_Indicador);
                    larr_TipoCambio.Add(ldec_TipoCambio);
                    larr_TipoCambio.Add(lstr_usuario);

                    lcls_TipoCambio.registrarTipoCambioBCCR(larr_TipoCambio);
                }
                ldt_InicioCarga = ldt_InicioCarga.AddDays(1);
            }

            return ldt_ISOyBCCR;
        }

        /// <summary>
        /// realiza la conversión de monedas varias, a dolares, para almacenarlas en sigaf
        /// </summary>
        /// <param name="lstr_IdMoneda">Parámetro que identifica la moneda extranjera</param>
        /// <param name="ldt_FchPrestamo">Fecha de la transacción</param>
        /// <param name="ldec_MontoOriginal">Monto en la moneda de origen</param>
        /// <returns>Retorna el monto equivalente en dolares de la moneda transformada</returns>
        public decimal ConvertirdorDeMoneda(string lstr_IdMoneda, DateTime ldt_FchPrestamo, decimal ldec_MontoOriginal, out string lstr_Error)
        {
            decimal ldec_TipoCambioDolar = 0;
            decimal ldec_MontoDolares = 0;
            lstr_Error = "";
            DataSet lds_TipoCambio = new DataSet();
            DateTime ldt_FechaActual = DateTime.UtcNow.Date;
            DateTime ldt_HoraCarga = DateTime.ParseExact("09:30" /*pasar por parametro la hora*/ , "HH:mm", CultureInfo.InvariantCulture);
            try
            {
                if (TimeSpan.Compare(ldt_HoraCarga.TimeOfDay, ldt_FechaActual.TimeOfDay) == 1)
                {
                    clsTiposCambio lcls_TipoCambio = new clsTiposCambio();
                    lds_TipoCambio = lcls_TipoCambio.ConsultarTiposCambio(lstr_IdMoneda, ldt_FchPrestamo, null);
                    ldec_TipoCambioDolar = Convert.ToDecimal(lds_TipoCambio.Tables[0].Rows[0].ItemArray[3].ToString());
                    ldec_MontoDolares = ldec_MontoOriginal / ldec_TipoCambioDolar;
                }
                else
                {
                    lstr_Error = "El tipo de cambio no ha sido actualizado por el BCCR";
                }
            }
            catch (Exception ex)
            {
                lstr_Error = ex.ToString();
            }
            
            return ldec_MontoDolares;
        }

        public decimal ObtieneTipoCambioBCCR(string lstr_Indicador, string lstr_Fecha)
        {
            decimal ldec_TipoCambioDolar = 0;
            DataSet lds_TipoCambio = new DataSet();
            try
            {
                //cr.fi.bccr.indicadoreseconomicos.wsindicadoreseconomicos TipoCambio = new cr.fi.bccr.indicadoreseconomicos.wsIndicadoresEconomicos();
                //lds_TipoCambio = TipoCambio.ObtenerIndicadoresEconomicos(lstr_Indicador, lstr_Fecha, lstr_Fecha, "MH_Gestor", "N");

                cr.fi.bccr.indicadoreseconomicos.wsindicadoreseconomicos TipoCambio = new cr.fi.bccr.indicadoreseconomicos.wsindicadoreseconomicos();
                lds_TipoCambio = TipoCambio.ObtenerIndicadoresEconomicos(lstr_Indicador, lstr_Fecha, lstr_Fecha, "MH_DESARROLLO", "N", "AmbDesarrolloNICSP@hotmail.com", "DCALHSRDSA");

                ldec_TipoCambioDolar = Convert.ToDecimal(lds_TipoCambio.Tables[0].Rows[0].ItemArray[2].ToString());
            }
            catch (Exception ex)
            {
                Log.Info(ex.ToString()); 
            }

            return ldec_TipoCambioDolar;
        }

        #endregion

        #region Funciones de desembolsos

        public string registraDesembolsoSIGAF()
        {
            return "";
        }

        #endregion

        public decimal DiferenciaSemestres(DateTime FechaFin, DateTime FechaInicio)
        {
            decimal ldec_temp1 = 0;
            decimal ldec_semestre = 0;
            ldec_temp1 = Math.Abs((FechaFin.Month - FechaInicio.Month) + 12 * (FechaFin.Year - FechaInicio.Year));

            if ((ldec_temp1 / 6) < 0)
            {
                ldec_semestre = 1;
            }
            else
            {
                ldec_semestre = ldec_temp1 / 6;
            }
            return Math.Round(ldec_semestre);
        }

        public decimal DiferenciaMeses(DateTime FechaFin, DateTime FechaInicio)
        {
            return Math.Abs((FechaFin.Month - FechaInicio.Month) + 12 * (FechaFin.Year - FechaInicio.Year));
        }

        public int UltimoDiaMes(DateTime fecha)
        {
            int ultimoDia = 0;
            ultimoDia = DateTime.DaysInMonth(fecha.Year, fecha.Month);
            return ultimoDia;
        }

        public string[] PeriodosSemestrales(DateTime ldec_FchValor, DateTime ldec_FchVencimiento)
        {
            DateTime temp = new DateTime();
            DateTime inicio = new DateTime();
            DateTime fin = new DateTime();
            meses = 0;
            ArrayList arreglo = new ArrayList();

            try
            {
                inicio = ldec_FchValor;
                fin = ldec_FchVencimiento;
                temp = fin;
                //meses = DiferenciaSemestres(ldec_FchVencimiento, ldec_FchValor);

                arreglo.Add(temp.ToString(lstr_formato_fecha));
                meses++;

                while (temp > inicio)
                {
                    temp = temp.AddMonths(-6);
                    arreglo.Add(temp.ToString(lstr_formato_fecha));
                    meses++;
                }
                arreglo.Remove(temp.ToString(lstr_formato_fecha));
                arreglo.Add(inicio.ToString(lstr_formato_fecha));

                fechas = new string[meses];

                int cont = 0;
                for (int i = arreglo.Count; i > 0; i--)
                {
                    fechas[cont] = arreglo[i - 1].ToString();
                    cont++;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return fechas;
        }

        public string[] PeriodosMensuales(DateTime ldec_FchValor, DateTime ldec_FchVencimiento)
        {
            DateTime temp = new DateTime();
            DateTime inicio = new DateTime();
            DateTime fin = new DateTime();
            meses = 0;
            ArrayList arreglo = new ArrayList();

            try
            {
                inicio = ldec_FchValor;
                fin = ldec_FchVencimiento;
                temp = inicio;
                meses = Convert.ToInt32(DiferenciaMeses(ldec_FchVencimiento, ldec_FchValor));

                arreglo.Add(inicio.ToString(lstr_formato_fecha));

                if (inicio.Day != UltimoDiaMes(inicio))
                {
                    meses++;
                    temp = DateTime.ParseExact("" + UltimoDiaMes(inicio) + "/" + inicio.Month.ToString().PadLeft(2, '0') + "/" + inicio.Year, lstr_formato_fecha, CultureInfo.InvariantCulture);
                    arreglo.Add(temp.ToString(lstr_formato_fecha));
                }

                for (int i = 1; i < meses; i++)
                {
                    temp = temp.AddMonths(1);
                    if (temp.Month == fin.Month)
                    {
                        break;
                    }
                    else
                    {
                        arreglo.Add(temp.ToString(lstr_formato_fecha));
                        //arreglo.Add(temp.ToString(lstr_formato_fecha));
                    }
                }

                if (temp != fin)
                {
                    meses++;
                    arreglo.Add(fin.ToString(lstr_formato_fecha));
                }
                else
                {
                    meses++;
                }

                fechas = new string[meses];

                for (int i = 0; i < arreglo.Count; i++)
                {
                    fechas[i] = arreglo[i].ToString();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return fechas;
        }



        public int Dias360(DateTime date, DateTime initialDate)
        {
            int StartDay = initialDate.Day;
            int StartMonth = initialDate.Month;
            int StartYear = initialDate.Year;
            int EndDay = date.Day;
            int EndMonth = date.Month;
            int EndYear = date.Year;

            if (StartDay == 31 || UltimoDiaFebrero(initialDate))
            {
                StartDay = 30;
            }

            if (StartDay == 30 && EndDay == 31)
            {
                EndDay = 30;
            }

            return ((EndYear - StartYear) * 360) + ((EndMonth - StartMonth) * 30) + (EndDay - StartDay);
            /*var dateA = initialDate;

            var dateB = date;

            var dayA = dateA.Day;

            var dayB = dateB.Day;

            if (UltimoDiaFebrero(dateA) && UltimoDiaFebrero(dateB))
                dayB = 30;

            if (dayA == 31 && UltimoDiaFebrero(dateA))
                dayA = 30;

            if (dayA == 30 && dayB == 31)
                dayB = 30;

            int days = (dateB.Year - dateA.Year) * 360 +
                ((dateB.Month + 1) - (dateA.Month + 1)) * 30 + (dayB - dayA);

            return days;*/

        }


        private static bool UltimoDiaFebrero(DateTime date)
        {

            //int lastDay = DateTime.DaysInMonth(date.Year, 2);
            return date.Month == 2 && date.Day == DateTime.DaysInMonth(date.Year, date.Month);
            //return date.Day == lastDay;
        }

        private List<clsConsultarCalculosFlujoEfectivoDE> ConsultaFlujoEfectivoDE(string numero_prestamo, int numero_tramo, DateTime? fechaValor)
        {
            //clsDevengoInteres lcls_CalculoDevengoInteres = new clsDevengoInteres();
            List<clsConsultarCalculosFlujoEfectivoDE> lstConsultaCalculo = new List<clsConsultarCalculosFlujoEfectivoDE>();
            clsConsultarCalculosFlujoEfectivoDE consultaCalculo = new clsConsultarCalculosFlujoEfectivoDE();
            DataSet DsFlujos = consultaCalculo.getCalculosFlujoEfectivoDE(numero_prestamo, numero_tramo, null, fechaValor);

            foreach (DataRow row in DsFlujos.Tables[0].Rows)
            {
                consultaCalculo = new clsConsultarCalculosFlujoEfectivoDE();
                consultaCalculo.Lstr_IdPrestamo = numero_prestamo;
                consultaCalculo.Lint_IdTramo = numero_tramo;
                consultaCalculo.Ldt_FechaHasta = (DateTime)row["Fecha"];
                consultaCalculo.CostoAmortInicio = Convert.ToDecimal(row["CostoAmortInicio"]);
                consultaCalculo.Interes = Convert.ToDecimal(row["Interes"]);
                consultaCalculo.FNE = Convert.ToDecimal(row["FNE"]);
                consultaCalculo.CostoAmortFinal = Convert.ToDecimal(row["CostoAmortFinal"]);
                consultaCalculo.SaldoDevengo = Convert.ToDecimal(row["SaldoDevengo"]);
                consultaCalculo.Tir = Convert.ToDecimal(row["Tir"]);
                lstConsultaCalculo.Add(consultaCalculo);
            }

            return lstConsultaCalculo;

         }
        private List<clsConsultarCalculosFlujoEfectivoDEAgrupa> ConsultaFlujoEfectivoDEAgrupa(string numero_prestamo, int numero_tramo, DateTime? fechaValor)
        {
            //clsDevengoInteres lcls_CalculoDevengoInteres = new clsDevengoInteres();
            List<clsConsultarCalculosFlujoEfectivoDEAgrupa> lstConsultaCalculo = new List<clsConsultarCalculosFlujoEfectivoDEAgrupa>();
            clsConsultarCalculosFlujoEfectivoDEAgrupa consultaCalculo = new clsConsultarCalculosFlujoEfectivoDEAgrupa();
            DataSet DsFlujos = consultaCalculo.getCalculosFlujoEfectivoDEAgrupa(numero_prestamo, numero_tramo, null, fechaValor);

            foreach (DataRow row in DsFlujos.Tables[0].Rows)
            {
                consultaCalculo = new clsConsultarCalculosFlujoEfectivoDEAgrupa();
                consultaCalculo.Lstr_IdPrestamo = numero_prestamo;
                consultaCalculo.Lint_IdTramo = numero_tramo;
                consultaCalculo.Ldt_FechaHasta = (DateTime)row["Fecha"]; 
                consultaCalculo.CostoAmortInicio = row["CostoAmortInicio"].ToString() == "" ? 0 : Convert.ToDecimal(row["CostoAmortInicio"]);
                consultaCalculo.Interes = row["Interes"].ToString() == "" ? 0 : Convert.ToDecimal(row["Interes"]);
                consultaCalculo.FNE = row["FNE"].ToString() == "" ? 0 : Convert.ToDecimal(row["FNE"]);
                consultaCalculo.CostoAmortFinal = row["CostoAmortFinal"].ToString() == "" ? 0 : Convert.ToDecimal(row["CostoAmortFinal"]);
                consultaCalculo.SaldoDevengo = row["SaldoDevengo"].ToString() == "" ? 0 : Convert.ToDecimal(row["SaldoDevengo"]);
                consultaCalculo.Tir = row["Tir"].ToString() == "" ? 0 : Convert.ToDecimal(row["Tir"]);
                lstConsultaCalculo.Add(consultaCalculo);
            }

            return lstConsultaCalculo;

        }

        public string[] PeriodosMensualesDevengo(DateTime ldec_FchValor, DateTime ldec_FchVencimiento)
        {
            DateTime temp = new DateTime();
            DateTime inicio = new DateTime();
            DateTime fin = new DateTime();
            mesesDev = 0;
            ArrayList arreglo = new ArrayList();

            try
            {
                if (ldec_FchValor < ldec_FchVencimiento)
                {
                    inicio = ldec_FchValor;
                    fin = ldec_FchVencimiento;
                }
                else
                {
                    inicio = ldec_FchVencimiento;
                    fin = ldec_FchValor;
                }
                
                temp = inicio;
                mesesDev = Convert.ToInt32(DiferenciaMeses(ldec_FchVencimiento, ldec_FchValor));

                //arreglo.Add(inicio.ToString("dd/MM/yyyy"));

                // if (inicio.Day != UltimoDiaMes(inicio))
                //{
                //mesesDev++;
                int dia = 0;
                if (mesesDev > 0)
                {
                    if (inicio.Month == 2)
                    {
                        if (UltimoDiaMes(inicio) == 28)
                        {
                            dia = UltimoDiaMes(inicio);
                        }
                        else
                        {
                            dia = UltimoDiaMes(inicio) - 1;
                        }
                    }
                    else
                    {
                        if (UltimoDiaMes(inicio) == 30)
                        {
                            dia = UltimoDiaMes(inicio);
                        }
                        else
                        {
                            dia = UltimoDiaMes(inicio) - 1;
                        }
                    }
                    temp = DateTime.ParseExact("" + dia + "/" + inicio.Month.ToString().PadLeft(2, '0') + "/" + inicio.Year, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    arreglo.Add(temp.ToString("dd/MM/yyyy"));
                }
                else
                {
                    arreglo.Add(fin.ToString("dd/MM/yyyy"));
                }
                //}

                for (int i = 1; i < mesesDev; i++)
                {
                    //temp = temp.AddMonths(1);
                    temp = temp.AddMonths(1);
                    dia = 0;
                    if (temp.Month == 2)
                    {
                        if (UltimoDiaMes(temp) == 28)
                        {
                            dia = UltimoDiaMes(temp);
                        }
                        else
                        {
                            dia = UltimoDiaMes(temp) - 1;
                        }
                    }
                    else
                    {
                        if (UltimoDiaMes(temp) == 30)
                        {
                            dia = UltimoDiaMes(temp);
                        }
                        else
                        {
                            dia = UltimoDiaMes(temp) - 1;
                        }
                    }
                    temp = DateTime.ParseExact("" + dia + "/" + temp.Month.ToString().PadLeft(2, '0') + "/" + temp.Year, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if ((temp.Month == fin.Month) && (temp.Year == fin.Year))
                    {
                        break;
                    }
                    else
                    {
                        arreglo.Add(temp.ToString("dd/MM/yyyy"));
                        //arreglo.Add(temp.ToString("dd/MM/yyyy"));
                    }
                }

                if (temp != fin)
                {
                    mesesDev++;
                    arreglo.Add(fin.ToString("dd/MM/yyyy"));
                }
                else
                {
                    mesesDev++;
                }

                fechas = new string[mesesDev];

                for (int i = 0; i < arreglo.Count; i++)
                {
                    fechas[i] = arreglo[i].ToString();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return fechas;
        }

        //TODO: CalculoPeriodosMensuales
        public string CalculoPeriodosMensuales(string numero_prestamo, int numero_tramo, DateTime fechaValor)
        {

            int TotalDiasDif = 0;
            int DiasDif = 0;
            string result = "Flujo efectivo mensual creado correctamente";


            List<clsConsultarCalculosFlujoEfectivoDEAgrupa> LstFlujoEfectivo = ConsultaFlujoEfectivoDEAgrupa(numero_prestamo, numero_tramo, null);



            try
            {
                if (LstFlujoEfectivo[0].Ldt_FechaHasta.Day == 31)
                    LstFlujoEfectivo[0].Ldt_FechaHasta = LstFlujoEfectivo[0].Ldt_FechaHasta.AddDays(-1);
                fechaValor = LstFlujoEfectivo[0].Ldt_FechaHasta;



                foreach (var flujo_efectivo in LstFlujoEfectivo)
                {
                    if (flujo_efectivo.Ldt_FechaHasta.Day == 31)
                        flujo_efectivo.Ldt_FechaHasta = flujo_efectivo.Ldt_FechaHasta.AddDays(-1);
                    FchTemp = PeriodosMensualesDevengo(flujo_efectivo.Ldt_FechaHasta, fechaValor);
                    //if (FchTemp.Length > 2) Array.Resize(ref FchTemp, FchTemp.Length - 1);
                    if (flujo_efectivo.Ldt_FechaHasta == fechaValor) continue;
                    //if ( FchTemp.Length > 6 ) FchTemp = FchTemp.Skip(1).ToArray();
                    DateTime ldt_FchTemp = DateTime.ParseExact(FchTemp[FchTemp.Length - 1], lstr_formato_fecha, CultureInfo.InvariantCulture); ;//Convert.ToDateTime(FchTemp[FchTemp.Length - 1], lstr_formato_fecha);
                    int d = (Dias360(ldt_FchTemp, fechaValor));
                    TotalDiasDif = +(Dias360(ldt_FchTemp, fechaValor));

                    //if (ldt_FchTemp.Month == 2) TotalDiasDif += 2;
                    //if (fechaValor.Month == 2) TotalDiasDif -= 2;//EL Gabo lo comentó porque no cuadraba tabla 3 prestamo 29174000 tramo 6

                    //if (DiferenciaMeses(ldt_FchTemp, fechaValor) == 6 && TotalDiasDif > 180)
                    //    TotalDiasDif = 180;


                    int sumDiasDif = 0;
                    for (int i = 0; i < mesesDev; i++)
                    {
                        DateTime FchTemporal = new DateTime();
                        DateTime FchTemporal1 = new DateTime();
                        DateTime FchTemporal2 = new DateTime();
                        try
                        {
                            if (i == 0)
                            {
                                FchTemporal = fechaValor;
                                DiasDif = +(Dias360(DateTime.ParseExact(FchTemp[i], lstr_formato_fecha, CultureInfo.InvariantCulture), FchTemporal));

                                if (FchTemporal.Month == 2)
                                    DiasDif += 2;

                                if (DiasDif > 30)
                                    DiasDif = 30;//gabo
                                
                            }
                            else
                            {
                                FchTemporal1 = fechaValor;
                                FchTemporal2 = DateTime.ParseExact(FchTemp[i], lstr_formato_fecha, CultureInfo.InvariantCulture);//Convert.ToDateTime(FchTemp[i]);
                                DiasDif = +(Dias360(FchTemporal2, FchTemporal1));

                                if ((FchTemporal2.Month == 2 || FchTemporal1.Month == 2) && DiasDif >= 28)
                                    DiasDif = 30;
                                else
                                {
                                    if (FchTemporal1.Day >= 28 && FchTemporal1.Month == 2)
                                    {
                                        if (DiasDif > 30)
                                            DiasDif = 30;

                                        if (FchTemporal2.Day >= 30)
                                            DiasDif = 30;
                                        //else //Gabo 15-12-2016 le resta dos dias dando resultado incorrecto dias360('28/02/2017','15/03/2017') debe dar 15 y daba 13
                                            //DiasDif = DiasDif - 2;
                                    }
                                    else
                                    {
                                        if (DiasDif > 30)
                                            DiasDif = 30;
                                    }
                                }//gabo
                            }
                            sumDiasDif += DiasDif;

                            if (sumDiasDif > TotalDiasDif)
                                DiasDif -= (sumDiasDif - TotalDiasDif);
                            decimal CostoAmortInicio = (flujo_efectivo.CostoAmortInicio / TotalDiasDif) * DiasDif;
                            decimal Interes = (flujo_efectivo.Interes / TotalDiasDif) * DiasDif;
                            decimal FNE = (flujo_efectivo.FNE / TotalDiasDif) * DiasDif;
                            decimal CostoAmortFinal = (flujo_efectivo.CostoAmortFinal / TotalDiasDif) * DiasDif;
                            decimal SaldoDevengo = (flujo_efectivo.SaldoDevengo / TotalDiasDif) * DiasDif;
                            decimal Tir = flujo_efectivo.Tir;

                            if (DiasDif > 0)
                                new clsCrearCalculosFlujoEfectivoMensualDE(flujo_efectivo.Lstr_IdPrestamo,
                                                                            flujo_efectivo.Lint_IdTramo,
                                                                            DateTime.ParseExact(FchTemp[i], lstr_formato_fecha, CultureInfo.InvariantCulture),//Convert.ToDateTime(FchTemp[i]),
                                                                            CostoAmortInicio,
                                                                            Interes,
                                                                            FNE,
                                                                            CostoAmortFinal,
                                                                            SaldoDevengo,
                                                                            Tir,
                                                                            "WS");

                            fechaValor = DateTime.ParseExact(FchTemp[i], lstr_formato_fecha, CultureInfo.InvariantCulture);//Convert.ToDateTime(FchTemp[i]);

                        }
                        catch
                        { i = mesesDev + 1; }
                    }
                }
            }
            catch (Exception ex)
            {
                result = "Error: " + ex.ToString();
            }
            return result;
        }


        #region devengo

        public decimal CalculoTIR(double[] arreglo)
        {
            //double retorno = Microsoft.VisualBasic.Financial.IRR(ref arreglo, 0);
            return calcDI.CalculoTIR(arreglo); //Convert.ToDecimal(retorno * 100);
        }

        public void Devengo()
        {
            DataTable ldat_Prestamos = new DataTable();
            clsPrestamo lcls_Prestamo = new clsPrestamo();
            clsDevengoInteresDE lcls_DevengoInteresDE = new clsDevengoInteresDE();
            decimal ldec_TIR = 0;
            int lint_IdPrestamo = 0;
            decimal ldec_Monto = 0;
            decimal ldec_ValorFacial = 0;
            decimal ldec_CostAmortIni = 0;
            decimal ldec_Interes = 0;
            decimal ldec_CostAmortFin = 0;
            decimal ldec_Pago = 0;
            decimal ldec_PenultPago = 0;
            decimal ldec_Devengado = 0;
            int cont = 0;
            string mens1 = String.Empty;
            string mens2 = String.Empty;

            try
            {
                // se obtienen los préstamos para calcular el devengo
                ldat_Prestamos = lcls_Prestamo.ConsultarPrestamo("", null, null, "", "", "", "").Tables[0];
                for (int i = 0; i < ldat_Prestamos.Rows.Count; i++)
                {
                    // se obtiene el TIR utilizando la lista de Flujos Netos de Efectivo (FNE)
                    ldec_TIR = CalculoTIR(FlujoEfectivo(ldat_Prestamos.Rows[i]));

                    lint_IdPrestamo = Convert.ToInt32(ldat_Prestamos.Rows[i]["IdPrestamo"].ToString());
                    ldec_Monto = Convert.ToDecimal(ldat_Prestamos.Rows[i]["Monto"].ToString());

                    for (int j = 1; j < lint_ContPeriodos - 1; j++)
                    {
                        if (j == 1)
                        {
                            ldec_CostAmortIni = ldec_Monto;
                            ldec_Interes = ldec_CostAmortIni * (ldec_TIR / 100);
                            ldec_CostAmortFin = ldec_CostAmortIni + ldec_Interes + ldec_Pago;
                            ldec_Devengado = ldec_Interes + ldec_Pago;

                            lcls_DevengoInteresDE.CrearDevengoInteresDE(lint_IdPrestamo, "", Convert.ToDateTime(fechas[j]), ldec_CostAmortIni,
                                ldec_Interes, ldec_Pago, ldec_CostAmortFin, ldec_Devengado, "ACT", "SG", out mens1, out mens2);
                        }
                        else
                        {
                            ldec_CostAmortIni = ldec_CostAmortFin;
                            ldec_Interes = ldec_CostAmortIni * (ldec_TIR / 100);
                            ldec_CostAmortFin = ldec_CostAmortIni + ldec_Interes + ldec_Pago;
                            ldec_Devengado = ldec_Interes + ldec_Pago;

                            lcls_DevengoInteresDE.CrearDevengoInteresDE(lint_IdPrestamo, "", Convert.ToDateTime(fechas[j]), ldec_CostAmortIni,
                                ldec_Interes, ldec_Pago, ldec_CostAmortFin, ldec_Devengado, "ACT", "SG", out mens1, out mens2);
                        }
                        cont = j;
                    }
                    ldec_CostAmortIni = ldec_CostAmortFin;
                    ldec_Interes = ldec_CostAmortIni * (ldec_TIR / 100);
                    ldec_Pago = -ldec_ValorFacial;
                    ldec_CostAmortFin = ldec_CostAmortIni + ldec_Interes + ldec_Pago;
                    ldec_Devengado = ldec_Interes + ldec_PenultPago;

                    lcls_DevengoInteresDE.CrearDevengoInteresDE(lint_IdPrestamo, "", Convert.ToDateTime(fechas[cont + 1]), ldec_CostAmortIni,
                        ldec_Interes, ldec_Pago, ldec_CostAmortFin, ldec_Devengado, "ACT", "SG", out mens1, out mens2);
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public double[] FlujoEfectivo(DataRow ldrw_Prestamo)
        {
            double[] ldec_ArregloTIR;
            string mens1 = String.Empty;
            string mens2 = String.Empty;
            clsCalculoFlujoEfectivoDE lcls_CalculoFlujoEfectivoDE = new clsCalculoFlujoEfectivoDE();
            int lint_IdPrestamo = 0;
            string lstr_Nemotecnico = String.Empty;
            DateTime ldt_FchFirmado = new DateTime();
            DateTime ldt_FchVencimiento = new DateTime();
            decimal ldec_TransNeto = 0;
            decimal ldec_ValorFacial = 0;
            decimal ldec_TransBruto = 0;
            decimal ldec_PrimaDesc = 0;
            decimal ldec_PlazoValor = 0;
            bool lbol_EsLargoPlazo = true;

            decimal ldec_TasaBruta = 0;
            decimal ldec_TasaMensual = 0; //TasaInteres

            try
            {

                lint_IdPrestamo = Convert.ToInt32(ldrw_Prestamo["IdPrestamo"].ToString());
                ldt_FchFirmado = Convert.ToDateTime(ldrw_Prestamo["FchFirmado"].ToString()); //primer registro de la tabla
                ldt_FchVencimiento = Convert.ToDateTime(ldrw_Prestamo["FchFirmado"].ToString()); //ultimo registro de la tabla
                ldec_TransNeto = Convert.ToDecimal(ldrw_Prestamo["Monto"].ToString());
                ldec_ValorFacial = Convert.ToDecimal(ldrw_Prestamo["Monto"].ToString()); //ultimo valor negativo del flujo de efectivo
                ldec_TransBruto = Convert.ToDecimal(ldrw_Prestamo["Monto"].ToString()); //primer valor del flujo de efectivo
                ldec_TasaBruta = Convert.ToDecimal(ldrw_Prestamo["Tasa"].ToString());
                ldec_PlazoValor = Convert.ToDecimal(ldrw_Prestamo["Plazo"].ToString());

                ldec_PrimaDesc = ldec_ValorFacial - ldec_TransBruto;

                if (ldec_PlazoValor <= 365)
                {
                    lbol_EsLargoPlazo = false;
                    ldec_TasaMensual = ldec_TasaBruta / 12;
                }
                else
                {
                    ldec_TasaMensual = ldec_TasaBruta / 2;
                }

                if (lbol_EsLargoPlazo)
                {
                    PeriodosSemestrales(ldt_FchFirmado, ldt_FchVencimiento);
                    lint_ContPeriodos = meses;

                    ldec_ArregloTIR = new double[lint_ContPeriodos];

                    lcls_CalculoFlujoEfectivoDE.CrearCalculoFlujoEfectivoDE(lint_IdPrestamo, "", ldt_FchFirmado.ToString(lstr_formato_fecha), ldec_TasaMensual,
                        0, ldec_TransBruto, "SG", out mens1, out mens2);
                    ldec_ArregloTIR[0] = Convert.ToDouble(ldec_TransBruto);

                    for (int i = 1; i < (lint_ContPeriodos - 1); i++)
                    {
                        lcls_CalculoFlujoEfectivoDE.CrearCalculoFlujoEfectivoDE(lint_IdPrestamo, "", fechas[i], ldec_TasaMensual,
                            0, 0, "SG", out mens1, out mens2);
                        ldec_ArregloTIR[i] = 0;
                    }

                    lcls_CalculoFlujoEfectivoDE.CrearCalculoFlujoEfectivoDE(lint_IdPrestamo, "", ldt_FchVencimiento.ToString(lstr_formato_fecha), ldec_TasaMensual,
                        0, -ldec_ValorFacial, "SG", out mens1, out mens2);
                    ldec_ArregloTIR[lint_ContPeriodos - 1] = Convert.ToDouble(-ldec_ValorFacial);

                    return ldec_ArregloTIR;
                }
                else
                {
                    PeriodosMensuales(ldt_FchFirmado, ldt_FchVencimiento);
                    lint_ContPeriodos = meses;

                    ldec_ArregloTIR = new double[lint_ContPeriodos];

                    lcls_CalculoFlujoEfectivoDE.CrearCalculoFlujoEfectivoDE(lint_IdPrestamo, "", ldt_FchFirmado.ToString(lstr_formato_fecha), ldec_TasaMensual,
                        0, ldec_TransBruto, "SG", out mens1, out mens2);
                    ldec_ArregloTIR[0] = Convert.ToDouble(ldec_TransBruto);

                    for (int i = 1; i < (lint_ContPeriodos - 1); i++)
                    {
                        lcls_CalculoFlujoEfectivoDE.CrearCalculoFlujoEfectivoDE(lint_IdPrestamo, "", fechas[i], ldec_TasaMensual,
                            0, 0, "SG", out mens1, out mens2);
                        ldec_ArregloTIR[i] = 0;
                    }

                    lcls_CalculoFlujoEfectivoDE.CrearCalculoFlujoEfectivoDE(lint_IdPrestamo, "", ldt_FchVencimiento.ToString(lstr_formato_fecha), ldec_TasaMensual,
                        0, -ldec_ValorFacial, "SG", out mens1, out mens2);
                    ldec_ArregloTIR[lint_ContPeriodos - 1] = Convert.ToDouble(-ldec_ValorFacial);

                    return ldec_ArregloTIR;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstr_IdPrestamo"></param>
        /// <param name="lint_IdTramo"></param>
        /// <param name="ldt_FechaDesde"></param>
        /// <param name="ldt_FechaHasta"></param>
        /// <returns></returns>
        public DataSet ConsultaFlujoEfectivoDeudaExt(string lstr_IdPrestamo = null, int? lint_IdTramo = null, DateTime? ldt_FechaDesde = null, DateTime? ldt_FechaHasta = null)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsReporteFlujoEfectivoDeudaExt cr_Procedimiento = new clsReporteFlujoEfectivoDeudaExt(lstr_IdPrestamo, lint_IdTramo, ldt_FechaDesde, ldt_FechaHasta);
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstr_IdPrestamo"></param>
        /// <param name="lint_IdTramo"></param>
        /// <param name="ldt_FechaDesde"></param>
        /// <param name="ldt_FechaHasta"></param>
        /// <returns></returns>
        public DataSet ConsultaPagosDE(string lstr_IdPrestamo = null, int? lint_IdTramo = null, DateTime? ldt_FechaDesde = null, DateTime? ldt_FechaHasta = null)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarPagosDE cr_Procedimiento = new clsConsultarPagosDE(lstr_IdPrestamo, lint_IdTramo, ldt_FechaDesde, ldt_FechaHasta);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstr_IdPrestamo"></param>
        /// <param name="lint_IdTramo"></param>
        /// <param name="ldt_FechaDesde"></param>
        /// <param name="ldt_FechaHasta"></param>
        /// <returns></returns>
        public DataSet ConsultarCalculosFlujoEfectivoDEAgrupa(string lstr_IdPrestamo = null, int? lint_IdTramo = null, DateTime? ldt_FechaDesde = null, DateTime? ldt_FechaHasta = null)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarCalculosFlujoEfectivoDEAgrupa cr_Procedimiento = new clsConsultarCalculosFlujoEfectivoDEAgrupa(lstr_IdPrestamo, lint_IdTramo, ldt_FechaDesde, ldt_FechaHasta);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstr_IdPrestamo"></param>
        /// <param name="lint_IdTramo"></param>
        /// <param name="ldt_FechaDesde"></param>
        /// <param name="ldt_FechaHasta"></param>
        /// <returns></returns>
        public DataSet ConsultarCalculosFlujoEfectivoDE(string lstr_IdPrestamo = null, int? lint_IdTramo = null, DateTime? ldt_FechaDesde = null, DateTime? ldt_FechaHasta = null)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarCalculosFlujoEfectivoDE cr_Procedimiento = new clsConsultarCalculosFlujoEfectivoDE(lstr_IdPrestamo, lint_IdTramo, ldt_FechaDesde, ldt_FechaHasta);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstr_IdPrestamo"></param>
        /// <param name="lint_IdTramo"></param>
        /// <param name="ldt_FechaDesde"></param>
        /// <param name="ldt_FechaHasta"></param>
        /// <returns></returns>
        public DataSet ConsultarCalculosFlujoEfectivoMensualDE(string lstr_IdPrestamo = null, int? lint_IdTramo = null, DateTime? ldt_FechaDesde = null, DateTime? ldt_FechaHasta = null)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarCalculosFlujoEfectivoMensualDE cr_Procedimiento = new clsConsultarCalculosFlujoEfectivoMensualDE(lstr_IdPrestamo, lint_IdTramo, ldt_FechaDesde, ldt_FechaHasta);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstr_IdPrestamo"></param>
        /// <param name="lint_IdTramo"></param>
        /// <param name="ldt_FechaDesde"></param>
        /// <param name="ldt_FechaHasta"></param>
        /// <returns></returns>
        public DataSet ConsultarCalculosDevengoDE(string lstr_IdPrestamo = null, int? lint_IdTramo = null, DateTime? ldt_FechaDesde = null, DateTime? ldt_FechaHasta = null)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarCalculosDevengoDE cr_Procedimiento = new clsConsultarCalculosDevengoDE(lstr_IdPrestamo, lint_IdTramo, ldt_FechaDesde, ldt_FechaHasta);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstr_IdPrestamo"></param>
        /// <param name="lint_IdTramo"></param>
        /// <param name="ldt_FechaDesde"></param>
        /// <param name="ldt_FechaHasta"></param>
        /// <returns></returns>
        public DataSet ConsultarCalculosDevengoMensualDE(string lstr_IdPrestamo = null, int? lint_IdTramo = null, DateTime? ldt_FechaDesde = null, DateTime? ldt_FechaHasta = null)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultarCalculosDevengoMensualDE cr_Procedimiento = new clsConsultarCalculosDevengoMensualDE(lstr_IdPrestamo, lint_IdTramo, ldt_FechaDesde, ldt_FechaHasta);
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstr_IdPrestamo"></param>
        /// <param name="lint_IdTramo"></param>
        /// <param name="ldt_FechaDesde"></param>
        /// <param name="ldt_FechaHasta"></param>
        /// <returns></returns>
        public string[] CrearCalculosFlujoEfectivoDE(string lstr_IdPrestamo,
        int? lint_IdTramo,
        DateTime? ldt_Fecha,
        decimal? ldec_CostoAmortInicio,
        decimal? ldec_Interes,
        decimal? ldec_FNE,
        decimal? ldec_CostoAmortFinal,
        decimal? ldec_SaldoDevengo,
        decimal? ldec_Tir,
        string lstr_UsrCreacion)
        {
            string[] str_Resultado = new string[2];
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsCrearCalculosFlujoEfectivoDE cr_Procedimiento = new clsCrearCalculosFlujoEfectivoDE(lstr_IdPrestamo, 
                    lint_IdTramo, 
                    ldt_Fecha,
                    ldec_CostoAmortInicio,
                    ldec_Interes,
                    ldec_FNE,
                    ldec_CostoAmortFinal,
                    ldec_SaldoDevengo,
                    ldec_Tir,
                    lstr_UsrCreacion);

                str_Resultado[0] = cr_Procedimiento.Lstr_CodigoResultado;
                str_Resultado[1] = cr_Procedimiento.Lstr_MensajeRespuesta;


            }
            catch (Exception ex)
            {
                str_Resultado[0] = "99";
                str_Resultado[1] = ex.ToString();
            }
            return str_Resultado;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstr_IdPrestamo"></param>
        /// <param name="lint_IdTramo"></param>
        /// <param name="ldt_Fecha"></param>
        /// <param name="ldec_CostoAmortInicio"></param>
        /// <param name="ldec_Interes"></param>
        /// <param name="ldec_FNE"></param>
        /// <param name="ldec_CostoAmortFinal"></param>
        /// <param name="ldec_SaldoDevengo"></param>
        /// <param name="lstr_UsrCreacion"></param>
        /// <returns></returns>
        public string[] CrearCalculosFlujoEfectivoMensualDE(string lstr_IdPrestamo,
        int? lint_IdTramo,
        DateTime? ldt_Fecha,
        decimal? ldec_CostoAmortInicio,
        decimal? ldec_Interes,
        decimal? ldec_FNE,
        decimal? ldec_CostoAmortFinal,
        decimal? ldec_SaldoDevengo,
        decimal? ldec_Tir,
        string lstr_UsrCreacion)
        {
            string[] str_Resultado = new string[2];
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsCrearCalculosFlujoEfectivoMensualDE cr_Procedimiento = new clsCrearCalculosFlujoEfectivoMensualDE(lstr_IdPrestamo,
                    lint_IdTramo,
                    ldt_Fecha,
                    ldec_CostoAmortInicio,
                    ldec_Interes,
                    ldec_FNE,
                    ldec_CostoAmortFinal,
                    ldec_SaldoDevengo,
                    ldec_Tir,
                    lstr_UsrCreacion);

                str_Resultado[0] = cr_Procedimiento.Lstr_CodigoResultado;
                str_Resultado[1] = cr_Procedimiento.Lstr_MensajeRespuesta;


            }
            catch (Exception ex)
            {
                str_Resultado[0] = "99";
                str_Resultado[1] = ex.ToString();
            }
            return str_Resultado;
        }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="lstr_IdPrestamo"></param>
       /// <param name="lint_IdTramo"></param>
       /// <param name="ldt_Fecha"></param>
       /// <param name="ldec_MontoDesembolso"></param>
       /// <param name="ldec_MontoAmortizacion"></param>
       /// <param name="ldec_MontoInteres"></param>
       /// <param name="ldec_MontoComision"></param>
       /// <param name="ldec_MontoFlujo"></param>
       /// <param name="lstr_TipoPrestamo"></param>
       /// <param name="lstr_IdMoneda"></param>
       /// <param name="lstr_NbrAcreedor"></param>
       /// <param name="lstr_AbrevAcreedor"></param>
       /// <param name="lstr_UsrCreacion"></param>
       /// <returns></returns>
        public string[] CrearCalculosDevengoDE(string lstr_IdPrestamo,
        int? lint_IdTramo,
        DateTime? ldt_Fecha,
        decimal? ldec_MontoDesembolso,
        decimal? ldec_MontoAmortizacion,
        decimal? ldec_MontoInteres,
        decimal? ldec_MontoComision,
        decimal? ldec_MontoFlujo,
        string lstr_TipoPrestamo,
        string lstr_IdMoneda,
        string lstr_NbrAcreedor,
        string lstr_AbrevAcreedor,
        string lstr_UsrCreacion)
        {

            string[] str_Resultado = new string[2];
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsCrearCalculosDevengoDE cr_Procedimiento = new clsCrearCalculosDevengoDE(lstr_IdPrestamo, 
                    lint_IdTramo, 
                    ldt_Fecha,
                    ldec_MontoDesembolso,
                    ldec_MontoAmortizacion,
                    ldec_MontoInteres,
                    ldec_MontoComision,
                    ldec_MontoFlujo,
                    lstr_TipoPrestamo,
                    lstr_IdMoneda,
                    lstr_NbrAcreedor,
                    lstr_AbrevAcreedor,
                    lstr_UsrCreacion);

                str_Resultado[0] = cr_Procedimiento.Lstr_CodigoResultado;
                str_Resultado[1] = cr_Procedimiento.Lstr_MensajeRespuesta;


            }
            catch (Exception ex)
            {
                str_Resultado[0] = "99";
                str_Resultado[1] = ex.ToString();
            }
            return str_Resultado;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstr_IdPrestamo"></param>
        /// <param name="lint_IdTramo"></param>
        /// <param name="ldt_Fecha"></param>
        /// <param name="ldec_MontoDesembolso"></param>
        /// <param name="ldec_MontoAmortizacion"></param>
        /// <param name="ldec_MontoInteres"></param>
        /// <param name="ldec_MontoComision"></param>
        /// <param name="ldec_MontoFlujo"></param>
        /// <param name="lstr_TipoPrestamo"></param>
        /// <param name="lstr_IdMoneda"></param>
        /// <param name="lstr_NbrAcreedor"></param>
        /// <param name="lstr_AbrevAcreedor"></param>
        /// <param name="lstr_UsrCreacion"></param>
        /// <returns></returns>
        public string[] CrearCalculosDevengoMensualDE(string lstr_IdPrestamo,
        int? lint_IdTramo,
        DateTime? ldt_Fecha,
        decimal? ldec_MontoDesembolso,
        decimal? ldec_MontoAmortizacion,
        decimal? ldec_MontoInteres,
        decimal? ldec_MontoComision,
        decimal? ldec_MontoFlujo,
        string lstr_TipoPrestamo,
        string lstr_IdMoneda,
        string lstr_NbrAcreedor,
        string lstr_AbrevAcreedor,
        string lstr_UsrCreacion)
        {

            string[] str_Resultado = new string[2];
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsCrearCalculosDevengoMensualDE cr_Procedimiento = new clsCrearCalculosDevengoMensualDE(lstr_IdPrestamo,
                    lint_IdTramo,
                    ldt_Fecha,
                    ldec_MontoDesembolso,
                    ldec_MontoAmortizacion,
                    ldec_MontoInteres,
                    ldec_MontoComision,
                    ldec_MontoFlujo,
                    lstr_TipoPrestamo,
                    lstr_IdMoneda,
                    lstr_NbrAcreedor,
                    lstr_AbrevAcreedor,
                    lstr_UsrCreacion);

                str_Resultado[0] = cr_Procedimiento.Lstr_CodigoResultado;
                str_Resultado[1] = cr_Procedimiento.Lstr_MensajeRespuesta;


            }
            catch (Exception ex)
            {
                str_Resultado[0] = "99";
                str_Resultado[1] = ex.ToString();
            }
            return str_Resultado;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstr_IdPrestamo"></param>
        /// <param name="lint_IdTramo"></param>
        /// <param name="ldt_FechaDesde"></param>
        /// <param name="ldt_FechaHasta"></param>
        /// <returns></returns>
        public string[] EliminarCalculosFlujoEfectivoDE(string lstr_IdPrestamo = null, int? lint_IdTramo = null, DateTime? ldt_FechaDesde = null, DateTime? ldt_FechaHasta = null)
        {
            string[] str_Resultado = new string[2];
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsEliminarCalculosFlujoEfectivoDE cr_Procedimiento = new clsEliminarCalculosFlujoEfectivoDE(lstr_IdPrestamo, lint_IdTramo, ldt_FechaDesde, ldt_FechaHasta);

                str_Resultado[0] = cr_Procedimiento.Lstr_CodigoResultado;
                str_Resultado[1] = cr_Procedimiento.Lstr_MensajeRespuesta;


            }
            catch (Exception ex)
            {
                str_Resultado[0] = "99";
                str_Resultado[1] = ex.ToString();
            }
            return str_Resultado;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstr_IdPrestamo"></param>
        /// <param name="lint_IdTramo"></param>
        /// <param name="ldt_FechaDesde"></param>
        /// <param name="ldt_FechaHasta"></param>
        /// <returns></returns>
        public string[] EliminarCalculosFlujoEfectivoMensualDE(string lstr_IdPrestamo = null, int? lint_IdTramo = null, DateTime? ldt_FechaDesde = null, DateTime? ldt_FechaHasta = null)
        {
            string[] str_Resultado = new string[2];
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsEliminarCalculosFlujoEfectivoMensualDE cr_Procedimiento = new clsEliminarCalculosFlujoEfectivoMensualDE(lstr_IdPrestamo, lint_IdTramo, ldt_FechaDesde, ldt_FechaHasta);

                str_Resultado[0] = cr_Procedimiento.Lstr_CodigoResultado;
                str_Resultado[1] = cr_Procedimiento.Lstr_MensajeRespuesta;


            }
            catch (Exception ex)
            {
                str_Resultado[0] = "99";
                str_Resultado[1] = ex.ToString();
            }
            return str_Resultado;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstr_IdPrestamo"></param>
        /// <param name="lint_IdTramo"></param>
        /// <param name="ldt_FechaDesde"></param>
        /// <param name="ldt_FechaHasta"></param>
        /// <returns></returns>
        public string[] EliminarCalculosDevengoDE(string lstr_IdPrestamo = null, int? lint_IdTramo = null, DateTime? ldt_FechaDesde = null, DateTime? ldt_FechaHasta = null)
        {

            string[] str_Resultado = new string[2];
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsEliminarCalculosDevengoDE cr_Procedimiento = new clsEliminarCalculosDevengoDE(lstr_IdPrestamo, lint_IdTramo, ldt_FechaDesde, ldt_FechaHasta);

                str_Resultado[0] = cr_Procedimiento.Lstr_CodigoResultado;
                str_Resultado[1] = cr_Procedimiento.Lstr_MensajeRespuesta;

               
            }
            catch (Exception ex)
            { 
              str_Resultado[0] = "99";
              str_Resultado[1] = ex.ToString();
            }
            return str_Resultado;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstr_IdPrestamo"></param>
        /// <param name="lint_IdTramo"></param>
        /// <param name="ldt_FechaDesde"></param>
        /// <param name="ldt_FechaHasta"></param>
        /// <returns></returns>
        public string[] EliminarCalculosDevengoMensualDE(string lstr_IdPrestamo = null, int? lint_IdTramo = null, DateTime? ldt_FechaDesde = null, DateTime? ldt_FechaHasta = null)
        {

            string[] str_Resultado = new string[2];
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsEliminarCalculosDevengoMensualDE cr_Procedimiento = new clsEliminarCalculosDevengoMensualDE(lstr_IdPrestamo, lint_IdTramo, ldt_FechaDesde, ldt_FechaHasta);

                str_Resultado[0] = cr_Procedimiento.Lstr_CodigoResultado;
                str_Resultado[1] = cr_Procedimiento.Lstr_MensajeRespuesta;


            }
            catch (Exception ex)
            {
                str_Resultado[0] = "99";
                str_Resultado[1] = ex.ToString();
            }
            return str_Resultado;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lstr_IdPrestamo"></param>
        /// <param name="lint_IdTramo"></param>
        /// <param name="ldt_FechaDesde"></param>
        /// <param name="ldt_FechaHasta"></param>
        /// <returns></returns>
        public DataSet ConsultaAmortizaciones(string lstr_IdPrestamo = null, int? lint_IdTramo = null, DateTime? ldt_FechaDesde = null, DateTime? ldt_FechaHasta = null)
        {
            DataSet lds_TablasConsulta = new DataSet();
            try
            {
                clsConsultaAmortizacion cr_Procedimiento = new clsConsultaAmortizacion(lstr_IdPrestamo, lint_IdTramo, null, null, ldt_FechaDesde, null, null, ldt_FechaHasta);
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

        public decimal CalculoXTIR(List<XTIR> listaXtir)
        {
            try
            {
                return calcDI.CalculoXTIR(listaXtir) * 100;
                //return CalcXirr.CalculateXIRR(listaXtir, 0.0000001m, 100) * 100;
            }
            catch (Exception ex)
            {
                System.IO.File.WriteAllText(@"C:\Users\Public\log.txt", ex.Message.ToString());
                return 0;
            }
        }
        public string ContabilizarDevengoDE(out String str_Mensaje, string lstr_IdPrestamo = "", int? lint_IdTramo = null, DateTime? ldt_FchHasta = null)
        {
            Boolean lbln_Resultado = true;
            DateTime? ldt_FechaHasta = ldt_FchHasta;
            string str_Resultado = "";
            str_Mensaje = "00 - Proceso finalizado";
            double uno = 1;
            double dias365 = 365;

            ldt_FechaHasta = (ldt_FechaHasta == null) ? DateTime.Today : ldt_FechaHasta;

            //ldt_FechaHasta = Convert.ToDateTime(ldt_FechaHasta).AddYears(1);

            //ldt_FechaHasta = new DateTime(Convert.ToDateTime(ldt_FechaHasta).Year, Convert.ToDateTime(ldt_FechaHasta).Month, 1).AddMonths(1);
            //ldt_FechaHasta = Convert.ToDateTime(ldt_FechaHasta).AddDays(-1);


            //DateTime ldt_FechaIni = Convert.ToDateTime(ldt_FechaHasta);//.AddDays(1);
            DateTime ldt_FechaIni = Convert.ToDateTime(ldt_FechaHasta).AddDays(1);
            ldt_FechaIni = ldt_FechaIni.AddMonths(-1);


            str_Mensaje = "00 - Proceso finalizado";
            try
            {
                DataSet ds_Tramos = this.ConsultarCalculosFlujoEfectivoMensualDE(lstr_IdPrestamo, lint_IdTramo, ldt_FechaIni, ldt_FechaHasta);//ConsultaFlujoEfectivoDeudaExt(lstr_IdPrestamo, lint_IdTramo, null, ldt_FechaHasta);//saco los tramos//;//saco los tramos
                if (ds_Tramos.Tables.Count > 0 )
                {                    
                    DataTable dt_Tramos = ds_Tramos.Tables[0];
                    int i = 0;
                    foreach (DataRow dr_Tramo in dt_Tramos.Rows)
                    {
                        DataSet ds_TramosInfo = ltr_Tramo.ConsultarTramo(dr_Tramo["IdPrestamo"].ToString(), Convert.ToInt32(dr_Tramo["IdTramo"]));
                        DataTable dt_TramosInfo = ds_TramosInfo.Tables[0];
                        DataRow dr_TramoInfo = null;
                    
                        if (dt_TramosInfo.Rows.Count>0)
                        {
                            dr_TramoInfo = dt_TramosInfo.Rows[0];
                        }
                        if (Convert.ToInt32(dr_Tramo["IdTramo"]) != 0)
                        {
                            // se contabiliza devengo
                            lbln_Resultado = ContabilizarDevengoMensual(dr_Tramo["IdPrestamo"].ToString(), Convert.ToInt32(dr_Tramo["IdTramo"]), dr_TramoInfo["IdMoneda"].ToString(),
                                 Math.Abs(Convert.ToDecimal(dr_Tramo["Interes"])), Math.Abs(Convert.ToDecimal(dr_Tramo["FNE"])), Math.Abs(Convert.ToDecimal(dr_Tramo["SaldoDevengo"])),
                                 Convert.ToDateTime(dr_Tramo["Fecha"]), dr_TramoInfo["NbrAcreedor"].ToString(), dr_TramoInfo["TipoPrestamo"].ToString(), "3", "", out str_Resultado, out str_Mensaje);
                            resAsientosLog = reg_Bitacora.ufnRegistrarAccionBitacora("DE", "1", "Devengo", "Resultado de Contabilización Devengo: " + str_Mensaje);
                            
                        }
                        i++;
                    }
                }
                else
                {

                    resAsientosLog = reg_Bitacora.ufnRegistrarAccionBitacora("DE", "1", "Devengo", "No se encontraron Flujos de Efectivo Mensuales " + lstr_IdPrestamo + lint_IdTramo);
                }
            }
            catch (Exception e)
            {

                str_Mensaje = "99 - " + e.ToString();
            }
            return str_Mensaje;//str_Mensaje;
                   
        }
        public string DevengoDE(out String str_Mensaje, string lstr_IdPrestamo = "", int? lint_IdTramo = null, DateTime? ldt_FchHasta = null, char lchr_Xtir = '1')
        {
            Boolean lbln_Resultado = true;
            DateTime? ldt_FechaHasta = ldt_FchHasta;
            string str_Resultado = "";
            str_Mensaje = "00 - Proceso finalizado";
            double uno = 1;
            double dias365 = 365;

            ldt_FechaHasta = (ldt_FechaHasta == null) ? DateTime.Today : ldt_FechaHasta;
            try
            {
                this.EliminarCalculosDevengoDE(lstr_IdPrestamo, lint_IdTramo, null, null);

                this.EliminarCalculosFlujoEfectivoDE(lstr_IdPrestamo, lint_IdTramo, null, null);

                this.EliminarCalculosFlujoEfectivoMensualDE(lstr_IdPrestamo, lint_IdTramo, null, null);
            }
            catch (Exception ex)
            {

                str_Mensaje = "99 - Error al eliminar "+ex.ToString();
                throw;
                
            }
            //ldt_FechaHasta = Convert.ToDateTime(ldt_FechaHasta).AddYears(1);

            //ldt_FechaHasta = new DateTime(Convert.ToDateTime(ldt_FechaHasta).Year, Convert.ToDateTime(ldt_FechaHasta).Month, 1).AddMonths(1);
            //ldt_FechaHasta = Convert.ToDateTime(ldt_FechaHasta).AddDays(-1);


            DateTime ldt_FechaIni = Convert.ToDateTime(ldt_FechaHasta).AddMonths(-1);
            ldt_FechaIni = ldt_FechaIni.AddDays(1);


            str_Mensaje = "00 - Proceso finalizado";
            try
            {
                DataSet ds_Tramos = ltr_Tramo.ConsultarTramo(lstr_IdPrestamo, lint_IdTramo);//ConsultaFlujoEfectivoDeudaExt(lstr_IdPrestamo, lint_IdTramo, null, ldt_FechaHasta);//saco los tramos//;//saco los tramos
                if (ds_Tramos.Tables.Count > 0 && ds_Tramos.Tables["Table"].Rows.Count > 0)
                {
                    DataTable dt_Tramos = ds_Tramos.Tables[0];
                    DataRow dr_Tramo = null;
                    DataSet ds_Devengos2 = new DataSet();
                    for (int i = 0; i < dt_Tramos.Rows.Count; i++)
                    {
                        dr_Tramo = dt_Tramos.Rows[i];
                        if (Convert.ToInt32(dr_Tramo["IdTramo"]) != 0)
                        {
                            DataSet ds_Devengos = ConsultaFlujoEfectivoDeudaExt(dr_Tramo["IdPrestamo"].ToString(), Convert.ToInt32(dr_Tramo["IdTramo"]), null, null);
                            if (ds_Devengos.Tables.Count > 0 && ds_Devengos.Tables["Table"].Rows.Count > 0)
                            {
                                DataTable dt_Devengos = ds_Devengos.Tables[0];
                                DataRow dr_Devengo = null;
                                DataRow dr_Devengo2 = null;

                                List<XTIR> lstXtir = new List<XTIR>();
                                XTIR xTir;

                                double[] ldec_ArregloTIR = new double[dt_Devengos.Rows.Count];
                                decimal ldec_Tir = 0;
                                for (int y = 0; y < dt_Devengos.Rows.Count; y++)
                                {
                                    dr_Devengo = dt_Devengos.Rows[y];

                                    xTir = new XTIR();
                                    xTir.Monto = Convert.ToDecimal(dr_Devengo["MontoFlujo"].ToString());
                                    xTir.Fecha = Convert.ToDateTime(dr_Devengo["Fecha"].ToString());
                                    lstXtir.Add(xTir);
                                    ldec_ArregloTIR[y] = Convert.ToDouble(dr_Devengo["MontoFlujo"].ToString());
                                    try
                                    {
                                        this.CrearCalculosDevengoDE(dr_Devengo["IdPrestamo"].ToString(), Convert.ToInt32(dr_Devengo["IdTramo"]),
                                            Convert.ToDateTime(dr_Devengo["Fecha"]), Convert.ToDecimal(dr_Devengo["MontoDesembolso"]), Convert.ToDecimal(dr_Devengo["MontoAmortizacion"]),
                                            Convert.ToDecimal(dr_Devengo["MontoIntereses"]), Convert.ToDecimal(dr_Devengo["MontoComision"]), Convert.ToDecimal(dr_Devengo["MontoFlujo"]),
                                            Convert.ToString(dr_Devengo["TipoPrestamo"]), Convert.ToString(dr_Devengo["IdMoneda"]), Convert.ToString(dr_Devengo["NbrAcreedor"]), Convert.ToString(dr_Devengo["AbrevAcreedor"]), "SG");
                                    }
                                    catch (Exception ex)
                                    {

                                        str_Mensaje = "99 - Error al Crear Devengo " + ex.ToString();
                                        throw;
                                    }
                                }

                                DataTable dt_Devengos2 = new DataTable();
                                if (lchr_Xtir != '0')
                                {
                                    ldec_Tir = Convert.ToDecimal(CalculoXTIR(lstXtir));//CalculoTIR(ldec_ArregloTIR);

                                    dt_Devengos2.Columns.Add("Fecha");
                                    dt_Devengos2.Columns.Add("CostoAmortInicio");
                                    dt_Devengos2.Columns.Add("Interes");
                                    dt_Devengos2.Columns.Add("FNE");
                                    dt_Devengos2.Columns.Add("CostoAmortFinal");
                                    dt_Devengos2.Columns.Add("SaldoDevengo");
                                    dt_Devengos2.Columns.Add("TipoPrestamo");
                                    dt_Devengos2.Columns.Add("IdMoneda");
                                    dt_Devengos2.Columns.Add("NbrAcreedor");
                                    dt_Devengos2.Columns.Add("AbrevAcreedor");
                                    decimal ldec_CostoAmortInicio = 0;
                                    decimal ldec_Interes = 0;
                                    decimal ldec_FNE = 0;
                                    decimal ldec_CostoAmortFinal = 0;
                                    decimal ldec_SaldoDevengo = 0;
                                    string lstr_TipoPrestamo = "";
                                    string lstr_IdMoneda = "";
                                    string lstr_NbrAcreedor = "";
                                    string lstr_AbrevAcreedor = "";
                                    int y1 = 0;
                                    
                                    DateTime ldt_Fecha = DateTime.ParseExact("01/01/1900", lstr_formato_fecha, CultureInfo.InvariantCulture);
                                    DateTime ldt_FechaAnt = ldt_Fecha; 
                                    while (y1 < dt_Devengos.Rows.Count)//&& ldt_Fecha < ldt_FechaHasta)
                                    {
                                        dr_Devengo = dt_Devengos.Rows[y1];
                                        ldt_FechaAnt = ldt_Fecha; 
                                        ldt_Fecha = Convert.ToDateTime(dr_Devengo["Fecha"].ToString());
                                        lstr_TipoPrestamo = dr_Devengo["TipoPrestamo"].ToString();
                                        lstr_IdMoneda = dr_Devengo["IdMoneda"].ToString();
                                        lstr_NbrAcreedor = dr_Devengo["NbrAcreedor"].ToString();
                                        lstr_AbrevAcreedor = dr_Devengo["AbrevAcreedor"].ToString();
                                        //if (ldt_Fecha < ldt_FechaHasta)
                                        //{
                                        ldec_CostoAmortInicio = ldec_CostoAmortFinal;
                                        //Convert.ToDecimal((double)ldec_CostAmortIni * (Math.Pow((uno + ((double)ldec_TIR/100)), ((double)difDias / dias365)) - uno));
                                        //ldec_Interes = ldec_CostoAmortInicio * (Math.Pow( ( 1 + ldec_Tir) ,((ldt_Fecha - ldt_FechaAnt).TotalDays/365)-1));
                                        ldec_Interes = Convert.ToDecimal((double)ldec_CostoAmortInicio * (Math.Pow((uno + ((double)ldec_Tir / 100)), ((double)((ldt_Fecha - ldt_FechaAnt).TotalDays) / dias365)) - uno));
                                        ldec_FNE = Convert.ToDecimal(dr_Devengo["MontoFlujo"].ToString());
                                        ldec_CostoAmortFinal = ldec_CostoAmortFinal + ldec_Interes + ldec_FNE;
                                        ldec_SaldoDevengo = ldec_FNE + ldec_Interes;
                                        dt_Devengos2.Rows.Add(ldt_Fecha.ToString(),
                                                            ldec_CostoAmortInicio,
                                                            ldec_Interes,
                                                            ldec_FNE,
                                                            ldec_CostoAmortFinal,
                                                            ldec_SaldoDevengo,
                                                            lstr_TipoPrestamo,
                                                            lstr_IdMoneda,
                                                            lstr_NbrAcreedor,
                                                            lstr_AbrevAcreedor
                                                            );
                                        dr_Devengo2 = dt_Devengos2.Rows[y1];
                                        //}
                                        try
                                        {
                                            this.CrearCalculosFlujoEfectivoDE(dr_Devengo["IdPrestamo"].ToString(), Convert.ToInt32(dr_Devengo["IdTramo"]),
                                                ldt_Fecha, ldec_CostoAmortInicio, ldec_Interes, ldec_FNE, ldec_CostoAmortFinal, ldec_SaldoDevengo, ldec_Tir,"SG");
                                        }
                                        catch (Exception ex)
                                        {

                                            str_Mensaje = "99 - Error al Crear Flujo Efectivo " + ex.ToString();
                                            throw;
                                        }
                                        y1++;
                                    }


                                    // se contabiliza la reclasificación de devengo
                                    /*lbln_Resultado = ContabilizarDevengoMensual(dr_Tramo["IdPrestamo"].ToString(), Convert.ToInt32(dr_Tramo["IdTramo"]), dr_Tramo["IdMoneda"].ToString(),
                                         Math.Abs(Convert.ToDecimal(dr_Devengo2["Interes"])), Math.Abs(Convert.ToDecimal(dr_Devengo2["FNE"])), Math.Abs(Convert.ToDecimal(dr_Devengo2["SaldoDevengo"])), ldt_FchHasta, dr_Tramo["NbrAcreedor"].ToString(), "",
                                        "3", "", out str_Resultado, out str_Mensaje);
                                    resAsientosLog = reg_Bitacora.ufnRegistrarAccionBitacora("DE", "1", "Devengo", "Resultado de Contabilización Devengo: " + str_Mensaje);
                                    */
                                }
                                else
                                {
                                    ldec_Tir = CalculoTIR(ldec_ArregloTIR);
                                    dt_Devengos2.Columns.Add("Fecha");
                                    dt_Devengos2.Columns.Add("CostoAmortInicio");
                                    dt_Devengos2.Columns.Add("Interes");
                                    dt_Devengos2.Columns.Add("FNE");
                                    dt_Devengos2.Columns.Add("CostoAmortFinal");
                                    dt_Devengos2.Columns.Add("SaldoDevengo");
                                    dt_Devengos2.Columns.Add("TipoPrestamo");
                                    dt_Devengos2.Columns.Add("IdMoneda");
                                    dt_Devengos2.Columns.Add("NbrAcreedor");
                                    dt_Devengos2.Columns.Add("AbrevAcreedor");
                                    decimal ldec_CostoAmortInicio = 0;
                                    decimal ldec_Interes = 0;
                                    decimal ldec_FNE = 0;
                                    decimal ldec_CostoAmortFinal = 0;
                                    decimal ldec_SaldoDevengo = 0;
                                    string lstr_TipoPrestamo = "";
                                    string lstr_IdMoneda = "";
                                    string lstr_NbrAcreedor = "";
                                    string lstr_AbrevAcreedor = "";
                                    int y1 = 0;
                                    DateTime ldt_Fecha = DateTime.ParseExact("01/01/1900", lstr_formato_fecha, CultureInfo.InvariantCulture);
                                    while (y1 < dt_Devengos.Rows.Count)//&& ldt_Fecha < ldt_FechaHasta)
                                    {
                                        dr_Devengo = dt_Devengos.Rows[y1];
                                        ldt_Fecha = Convert.ToDateTime(dr_Devengo["Fecha"].ToString());
                                        lstr_TipoPrestamo = dr_Devengo["TipoPrestamo"].ToString();
                                        lstr_IdMoneda = dr_Devengo["IdMoneda"].ToString();
                                        lstr_NbrAcreedor = dr_Devengo["NbrAcreedor"].ToString();
                                        lstr_AbrevAcreedor = dr_Devengo["AbrevAcreedor"].ToString();
                                        //if (ldt_Fecha < ldt_FechaHasta)
                                        //{
                                        ldec_CostoAmortInicio = ldec_CostoAmortFinal;
                                        ldec_Interes = ldec_CostoAmortInicio * ldec_Tir / 100;
                                        ldec_FNE = Convert.ToDecimal(dr_Devengo["MontoFlujo"].ToString());
                                        ldec_CostoAmortFinal = ldec_CostoAmortFinal + ldec_Interes + ldec_FNE;
                                        ldec_SaldoDevengo = ldec_FNE + ldec_Interes;
                                        dt_Devengos2.Rows.Add(ldt_Fecha.ToString(),
                                                            ldec_CostoAmortInicio,
                                                            ldec_Interes,
                                                            ldec_FNE,
                                                            ldec_CostoAmortFinal,
                                                            ldec_SaldoDevengo,
                                                            lstr_TipoPrestamo,
                                                            lstr_IdMoneda,
                                                            lstr_NbrAcreedor,
                                                            lstr_AbrevAcreedor
                                                            );
                                        dr_Devengo2 = dt_Devengos2.Rows[y1];
                                        //}
                                        try
                                        {
                                            this.CrearCalculosFlujoEfectivoDE(dr_Devengo["IdPrestamo"].ToString(), Convert.ToInt32(dr_Devengo["IdTramo"]),
                                                ldt_Fecha, ldec_CostoAmortInicio, ldec_Interes, ldec_FNE, ldec_CostoAmortFinal, ldec_SaldoDevengo,ldec_Tir, "SG");
                                        }
                                        catch (Exception ex)
                                        {

                                            str_Mensaje = "99 - Error al Crear Flujo Efectivo " + ex.ToString();
                                            throw;
                                        }
                                        y1++;
                                    }


                                    // se contabiliza la reclasificación de devengo
                                    /*lbln_Resultado = ContabilizarDevengoMensual(dr_Tramo["IdPrestamo"].ToString(), Convert.ToInt32(dr_Tramo["IdTramo"]), dr_Tramo["IdMoneda"].ToString(),
                                         Math.Abs(Convert.ToDecimal(dr_Devengo2["Interes"])), Math.Abs(Convert.ToDecimal(dr_Devengo2["FNE"])), Math.Abs(Convert.ToDecimal(dr_Devengo2["SaldoDevengo"])), ldt_FchHasta, dr_Tramo["NbrAcreedor"].ToString(), "",
                                        "3", "", out str_Resultado, out str_Mensaje);
                                    resAsientosLog = reg_Bitacora.ufnRegistrarAccionBitacora("DE", "1", "Devengo", "Resultado de Contabilización Devengo: " + str_Mensaje);
                                    */
                                }

                                ds_Devengos2.Tables.Add(dt_Devengos2);
                            }
                            //ariel
                            this.CalculoPeriodosMensuales(dr_Tramo["IdPrestamo"].ToString(), Convert.ToInt32(dr_Tramo["IdTramo"]), DateTime.Today);
                        }                    
                    }
                }
            }
            catch (Exception e)
            {

                str_Mensaje = "99 - " + e.ToString();
            }
            return str_Mensaje;//str_Mensaje;
        }

        public DataSet Reclasificar(out String str_Mensaje, string lstr_IdPrestamo = "", int? lint_IdTramo = null, DateTime? ldt_FchHasta = null, char lchr_Xtir = '1')
        {
            Boolean lbln_Resultado = true;
            DateTime? ldt_FechaHasta = ldt_FchHasta;
            string str_Resultado = "";

            ldt_FechaHasta = (ldt_FechaHasta == null) ? DateTime.Today : ldt_FechaHasta;


            ldt_FechaHasta = Convert.ToDateTime(ldt_FechaHasta).AddYears(1);

            ldt_FechaHasta = new DateTime(Convert.ToDateTime(ldt_FechaHasta).Year, Convert.ToDateTime(ldt_FechaHasta).Month, 1).AddMonths(1);
            ldt_FechaHasta = Convert.ToDateTime(ldt_FechaHasta).AddDays(-1);


            DateTime ldt_FechaIni = Convert.ToDateTime(ldt_FechaHasta).AddMonths(-1);
            ldt_FechaIni = ldt_FechaIni.AddDays(1);
            str_Mensaje = "00 - Proceso finalizado";
            DataSet ds_Devengos2 = new DataSet();
            try
            {
                //DataSet ds_Tramos = ConsultaFlujoEfectivoDeudaExt(lstr_IdPrestamo, lint_IdTramo, ldt_FechaIni, ldt_FechaHasta);//saco los tramos
                DataSet ds_Tramos = ConsultaAmortizaciones(lstr_IdPrestamo, lint_IdTramo, ldt_FechaIni, ldt_FechaHasta);//saco los tramos
                if (ds_Tramos.Tables.Count > 0 && ds_Tramos.Tables["Table"].Rows.Count > 0)
                {
                    DataTable dt_Tramos = ds_Tramos.Tables[0];
                    DataRow dr_Tramo = null;
                    for (int i = 0; i < dt_Tramos.Rows.Count; i++)
                    {
                        dr_Tramo = dt_Tramos.Rows[i];

                        if (Convert.ToDecimal(dr_Tramo["Monto"].ToString()) != 0)
                        {
                            // se contabiliza la reclasificación de la amortización
                            lbln_Resultado = ContabilizarReclasificacion(dr_Tramo["IdPrestamo"].ToString(), Convert.ToInt32(dr_Tramo["IdTramo"].ToString()), dr_Tramo["IdMoneda"].ToString(),
                                Convert.ToDecimal(dr_Tramo["Monto"].ToString()), ldt_FchHasta, dr_Tramo["NbrAcreedor"].ToString(), ""/*dr_Tramo["TipoPrestamo"].ToString()*/,
                                "1", "", out str_Resultado, out str_Mensaje);
                        }
                        //if (Convert.ToDecimal(dr_Tramo["MontoIntereses"].ToString()) != 0)
                        //{
                        //    // se contabiliza la reclasificación de intereses
                        //    lbln_Resultado = ContabilizarReclasificacion(dr_Tramo["IdPrestamo"].ToString(), Convert.ToInt32(dr_Tramo["IdTramo"].ToString()), dr_Tramo["IdMoneda"].ToString(),
                        //        Convert.ToDecimal(dr_Tramo["MontoIntereses"].ToString()), ldt_FchHasta, dr_Tramo["NbrAcreedor"].ToString(), ""/*dr_Tramo["TipoPrestamo"].ToString()*/,
                        //        "2", "", out str_Resultado, out str_Mensaje);
                        //}
                        //DataSet ds_Devengos = ConsultaFlujoEfectivoDeudaExt(dr_Tramo["IdPrestamo"].ToString(), Convert.ToInt32(dr_Tramo["IdTramo"].ToString()), null, null);
                        //if (ds_Devengos.Tables.Count > 0 && ds_Devengos.Tables["Table"].Rows.Count > 0)
                        //{
                        //    DataTable dt_Devengos = ds_Devengos.Tables[0];
                        //    DataRow dr_Devengo = null;
                        //    DataRow dr_Devengo2 = null;

                        //    List<XTIR> lstXtir = new List<XTIR>();
                        //    XTIR xTir;

                        //    double[] ldec_ArregloTIR = new double[dt_Devengos.Rows.Count];
                        //    decimal ldec_Tir = 0;
                        //    for (int y = 0; y < dt_Devengos.Rows.Count; y++)
                        //    {
                        //        dr_Devengo = dt_Devengos.Rows[y];

                        //        xTir = new XTIR();
                        //        xTir.Monto = Convert.ToDecimal(dr_Devengo["MontoFlujo"].ToString());
                        //        xTir.Fecha = Convert.ToDateTime(dr_Devengo["Fecha"].ToString());
                        //        lstXtir.Add(xTir);
                        //        ldec_ArregloTIR[y] = Convert.ToDouble(dr_Devengo["MontoFlujo"].ToString());

                        //    }
                        //    if (lchr_Xtir != '0')
                        //        ldec_Tir = Convert.ToDecimal(CalculoXTIR(lstXtir));//CalculoTIR(ldec_ArregloTIR);
                        //    else
                        //        ldec_Tir = CalculoTIR(ldec_ArregloTIR);
                        //    DataTable dt_Devengos2 = new DataTable();
                        //    dt_Devengos2.Columns.Add("Fecha");
                        //    dt_Devengos2.Columns.Add("CostoAmortInicio");
                        //    dt_Devengos2.Columns.Add("Interes");
                        //    dt_Devengos2.Columns.Add("FNE");
                        //    dt_Devengos2.Columns.Add("CostoAmortFinal");
                        //    dt_Devengos2.Columns.Add("SaldoDevengo");
                        //    dt_Devengos2.Columns.Add("TipoPrestamo");
                        //    dt_Devengos2.Columns.Add("IdMoneda");
                        //    dt_Devengos2.Columns.Add("NbrAcreedor");
                        //    dt_Devengos2.Columns.Add("AbrevAcreedor");
                        //    decimal ldec_CostoAmortInicio = 0;
                        //    decimal ldec_Interes = 0;
                        //    decimal ldec_FNE = 0;
                        //    decimal ldec_CostoAmortFinal = 0;
                        //    decimal ldec_SaldoDevengo = 0;
                        //    string lstr_TipoPrestamo = "";
                        //    string lstr_IdMoneda = "";
                        //    string lstr_NbrAcreedor = "";
                        //    string lstr_AbrevAcreedor = "";
                        //    int y1 = 0;
                        //    DateTime ldt_Fecha = DateTime.ParseExact("01/01/1900", lstr_formato_fecha, CultureInfo.InvariantCulture);
                        //    while (y1 < dt_Devengos.Rows.Count && ldt_Fecha < ldt_FechaHasta)
                        //    {
                        //        dr_Devengo = dt_Devengos.Rows[y1];
                        //        ldt_Fecha = Convert.ToDateTime(dr_Devengo["Fecha"].ToString());
                        //        lstr_TipoPrestamo = dr_Devengo["TipoPrestamo"].ToString();
                        //        lstr_IdMoneda = dr_Devengo["IdMoneda"].ToString();
                        //        lstr_NbrAcreedor = dr_Devengo["NbrAcreedor"].ToString();
                        //        lstr_AbrevAcreedor = dr_Devengo["AbrevAcreedor"].ToString();
                        //        if (ldt_Fecha < ldt_FechaHasta)
                        //        {
                        //            ldec_CostoAmortInicio = ldec_CostoAmortFinal;
                        //            ldec_Interes = ldec_CostoAmortInicio * ldec_Tir / 100;
                        //            ldec_FNE = Convert.ToDecimal(dr_Devengo["MontoFlujo"].ToString());
                        //            ldec_CostoAmortFinal = ldec_CostoAmortFinal + ldec_Interes + ldec_FNE;
                        //            ldec_SaldoDevengo = ldec_FNE + ldec_Interes;
                        //            dt_Devengos2.Rows.Add(ldt_Fecha.ToString(),
                        //                                ldec_CostoAmortInicio,
                        //                                ldec_Interes,
                        //                                ldec_FNE,
                        //                                ldec_CostoAmortFinal,
                        //                                ldec_SaldoDevengo,
                        //                                lstr_TipoPrestamo,
                        //                                lstr_IdMoneda,
                        //                                lstr_NbrAcreedor,
                        //                                lstr_AbrevAcreedor
                        //                                );
                        //            dr_Devengo2 = dt_Devengos2.Rows[y1];
                        //        }
                        //        y1++;
                        //    }


                        //    // se contabiliza la reclasificación de devengo
                        //    lbln_Resultado = ContabilizarReclasificacion(dr_Tramo["IdPrestamo"].ToString(), Convert.ToInt32(dr_Tramo["IdTramo"].ToString()), dr_Tramo["IdMoneda"].ToString(),
                        //       Convert.ToDecimal(dr_Devengo2["SaldoDevengo"]), ldt_FchHasta, dr_Tramo["NbrAcreedor"].ToString(), ""/*dr_Tramo["TipoPrestamo"].ToString()*/,
                        //        "3", "", out str_Resultado, out str_Mensaje);
                        //    ds_Devengos2.Tables.Add(dt_Devengos2);
                        //}
                    }
                }
            }
            catch (Exception e)
            {

                str_Mensaje = "99 - " + e.ToString();
            }
            return ds_Devengos2;//str_Mensaje;
        }


        public bool ContabilizarReclasificacion(string lstr_IdPrestamo, int lint_IdTramo, string lstr_IdMoneda, decimal ldec_Monto,
                DateTime? ldt_FchTipoCambio, string str_abrevAcreedor, string str_tipoPrestamo, string lstr_TipoReclasifica,
                string lstr_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {

            bool bool_resContabilizacion = true;
            bool bool_tipoCambioEncontrado = true;
            clsTiposAsiento tiposAsiento = new clsTiposAsiento();
            clsTiposCambio tiposCambio = new clsTiposCambio();
            string str_idModulo = "IdModulo IN ('DE')";
            string str_sociedad = "G206";
            string str_Moneda = "USD";
            string str_IdOperacion = "";
            String lstr_codAsiento;

            str_CodResultado = "00";
            str_Mensaje = "Contabilizado Correctamente";
            try
            {
                // se revisa la moneda por si se debe realizar un cambio a dolares

                if (lstr_IdMoneda == "CRC" || lstr_IdMoneda == "USD")//&& lstr_IdMoneda != "EUR")
                {

                    str_Moneda = lstr_IdMoneda;
                }
                else
                {
                    
                
                    // se trae el tipo de cambio y se realiza la conversión a USD
                    bool_tipoCambioEncontrado = false;

                    DataSet ds_tipoCambio = tiposCambio.ConsultarTiposCambio(lstr_IdMoneda, ldt_FchTipoCambio, null);
                    if (ds_tipoCambio.Tables.Count > 0 && ds_tipoCambio.Tables["Table"].Rows.Count > 0)
                    {
                        // se realiza el cambio a dolares para procesar el asiento
                        decimal ldec_valor = Convert.ToDecimal(ds_tipoCambio.Tables["Table"].Rows[0]["Valor"]);

                        if (lstr_IdMoneda == "EUR")
                            ldec_Monto *= ldec_valor;
                        else
                            ldec_Monto /= ldec_valor;

                        str_Moneda = "USD";

                        bool_tipoCambioEncontrado = true;
                    }
                
                }

                // si no se encontró el tipo de cambio se genera el error y se notifica
                if (!bool_tipoCambioEncontrado)
                {
                    // error al obtener el tipo de cambio
                    str_CodResultado = "01";
                    str_Mensaje = "Error al obtener el tipo de cambio para contabilizar reclasificación. Moneda: " + lstr_IdMoneda + " Fecha: " + ldt_FchTipoCambio + ". Préstamo: " + lstr_IdPrestamo;

                    Log.Info(str_Mensaje);

                    bool_resContabilizacion = false;
                }
                else
                {

                    str_IdOperacion = "RECLA CXP";

                    

                    if (bool_resContabilizacion)
                        // se procesa la amortización según el id de operación asignado
                        bool_resContabilizacion = tAsiento.EnviarAsientoDE(str_sociedad,
                                                        str_idModulo,
                                                        str_IdOperacion,
                                                        str_tipoPrestamo,
                                                        string.Empty,
                                                        str_Moneda,
                                                        ldec_Monto,
                                                        0, 0, 0, 
                                                        str_abrevAcreedor,
                                                        lstr_IdPrestamo + "." + lint_IdTramo.ToString(),
                                                        lstr_IdPrestamo,
                                                        ldt_FchTipoCambio,
                                                        out str_CodResultado,
                                                        out str_Mensaje,
                                                        out lstr_codAsiento
                         
                                                        );

                    str_IdOperacion = "RECLA CXC";

                   

                    if (bool_resContabilizacion)
                        // se procesa la amortización según el id de operación asignado
                        tAsiento.EnviarAsientoDE(str_sociedad,
                                                        str_idModulo,
                                                        str_IdOperacion,
                                                        str_tipoPrestamo,
                                                        string.Empty,
                                                        str_Moneda,
                                                        ldec_Monto,
                                                        0, 0, 0,
                                                        str_abrevAcreedor,
                                                        lstr_IdPrestamo + "." + lint_IdTramo.ToString(),
                                                        lstr_IdPrestamo,
                                                        ldt_FchTipoCambio,
                                                        out str_CodResultado,
                                                        out str_Mensaje,
                                                        out lstr_codAsiento

                                                        );

                    //Se envia un asiento pero no modifca el codigo de Asiento
                    /*if (str_CodResultado == "00")
                    {
                        ModificarCodigoAsiento(lstr_IdPrestamo, lint_IdTramo, lint_Secuencia, fechaContabilizacion, lstr_IdMoneda, str_codAsiento, lstr_UsrCreacion, out str_CodResultado, out str_Mensaje);
                    }*/

                }

            }
            catch (Exception ex)
            {
                str_CodResultado = "01";
                str_Mensaje = "Error al contabilizar asiento de reclasificación. Operación: " + lstr_IdPrestamo + ". Moneda: " +
                    lstr_IdMoneda + ". " + ex.Message;

                Log.Info(str_Mensaje);
                bool_resContabilizacion = false;
            }

            return bool_resContabilizacion;
        }

        public bool ContabilizarDevengoMensual(string lstr_IdPrestamo, int lint_IdTramo, string lstr_IdMoneda, decimal ldec_MontoInt, decimal ldec_MontoFNE, decimal ldec_MontoSaldo,
                DateTime? ldt_FchTipoCambio, string str_abrevAcreedor, string str_tipoPrestamo, string lstr_TipoReclasifica,
                string lstr_UsrCreacion, out string str_CodResultado, out string str_Mensaje)
        {

            bool bool_resContabilizacion = true;
            bool bool_tipoCambioEncontrado = true;
            clsTiposAsiento tiposAsiento = new clsTiposAsiento();
            clsTiposCambio tiposCambio = new clsTiposCambio();
            string str_idModulo = "IdModulo IN ('DE')";
            string str_sociedad = "G206";
            string str_Moneda = "USD";
            string str_IdOperacion = "";

            str_CodResultado = "00";
            str_Mensaje = "Contabilizado Correctamente";
            try
            {
                // se revisa la moneda por si se debe realizar un cambio a dolares
                if (lstr_IdMoneda != "CRC" && lstr_IdMoneda != "USD") //&& lstr_IdMoneda != "EUR")
                {
                    // se trae el tipo de cambio y se realiza la conversión a USD
                    bool_tipoCambioEncontrado = false;

                    DataSet ds_tipoCambio = tiposCambio.ConsultarTiposCambio(lstr_IdMoneda, ldt_FchTipoCambio, null);
                    if (ds_tipoCambio.Tables.Count > 0 && ds_tipoCambio.Tables["Table"].Rows.Count > 0)
                    {
                        // se realiza el cambio a dolares para procesar el asiento
                        decimal ldec_valor = Convert.ToDecimal(ds_tipoCambio.Tables["Table"].Rows[0]["Valor"]);


                        if (lstr_IdMoneda == "EUR")
                        {
                            ldec_MontoInt *= ldec_valor;
                            ldec_MontoFNE *= ldec_valor;
                            ldec_MontoSaldo *= ldec_valor;
                        }
                        else
                        {
                            ldec_MontoInt /= ldec_valor;
                            ldec_MontoFNE /= ldec_valor;
                            ldec_MontoSaldo /= ldec_valor;
                        }
                        
                        //lstr_IdMoneda = "USD";

                        bool_tipoCambioEncontrado = true;
                    }
                }
                else
                {

                    str_Moneda = lstr_IdMoneda;
                }

                // si no se encontró el tipo de cambio se genera el error y se notifica
                if (!bool_tipoCambioEncontrado)
                {
                    // error al obtener el tipo de cambio
                    str_CodResultado = "01";
                    str_Mensaje = "Error al obtener el tipo de cambio para contabilizar Devengo. Moneda: " + lstr_IdMoneda + " Fecha: " + ldt_FchTipoCambio + ". Préstamo: " + lstr_IdPrestamo;

                    Log.Info(str_Mensaje);

                    bool_resContabilizacion = false;
                }
                else
                {
                    if (ldec_MontoInt > ldec_MontoFNE && ldec_MontoInt > ldec_MontoSaldo) //> ldec_MontoSaldo)//)
                        str_IdOperacion = "DEVENGO+";//en este caso el Saldo de Devengo va a 50
                    else
                    {
                        if (ldec_MontoFNE > ldec_MontoInt && ldec_MontoFNE > ldec_MontoSaldo)
                            str_IdOperacion = "DEVENGO-";//en este caso el Saldo de Devengo va a 40
                        //ldec_MontoInt *= -1;
                        else
                            str_IdOperacion = "DEVENGO*";
                    }

                    String lstr_codAsiento;//variable para ModificarCodigoAsiento

                    if (bool_resContabilizacion)
                        // se procesa la amortización según el id de operación asignado
                        bool_resContabilizacion = tAsiento.EnviarAsientoDE(str_sociedad,
                                                        str_idModulo,
                                                        str_IdOperacion,
                                                        str_tipoPrestamo,
                                                        string.Empty,
                                                        str_Moneda,
                                                        ldec_MontoInt,
                                                        0, ldec_MontoFNE, ldec_MontoSaldo, 
                                                        str_abrevAcreedor,
                                                        lstr_IdPrestamo + "." + lint_IdTramo.ToString(),
                                                        lstr_IdPrestamo,
                                                        ldt_FchTipoCambio,
                                                        out str_CodResultado,
                                                        out str_Mensaje,
                                                        out lstr_codAsiento

                                                        );

                    //Se envia un asiento pero no modifca el codigo de Asiento
                    /*if (str_CodResultado == "00")
                    {
                        ModificarCodigoAsiento(lstr_IdPrestamo, lint_IdTramo, lint_Secuencia, fechaContabilizacion, lstr_IdMoneda, str_codAsiento, lstr_UsrCreacion, out str_CodResultado, out str_Mensaje);
                    }*/

                }

            }
            catch (Exception ex)
            {
                str_CodResultado = "01";
                str_Mensaje = "Error al contabilizar asiento de Devengo. Operación: " + lstr_IdPrestamo + ". Moneda: " +
                    lstr_IdMoneda + ". " + ex.Message;

                Log.Info(str_Mensaje);
                bool_resContabilizacion = false;
            }

            return bool_resContabilizacion;
        }


        private bool EnviarAsiento(string lstr_sociedad, string lstr_idModulo, string lstr_idOperacion, string lstr_tipoPrestamo, string lstr_moneda,
            decimal ldec_monto, string lstr_abrevAcreedor, out string lstr_CodResultado, out string lstr_Mensaje)
        {
            // variables locales
            bool bool_enviado = true;
            clsTiposAsiento tiposAsiento = new clsTiposAsiento();

            lstr_CodResultado = "00";
            lstr_Mensaje = "Asiento Enviado";

            string str_abrevAcreedor = (lstr_tipoPrestamo.Trim() == "4") ? "TITULOS" : lstr_abrevAcreedor;
            // se obtienen las tiras del asiento y se itera sobre ellas
            DataSet tiposA = tiposAsiento.ConsultarTiposAsiento(lstr_sociedad, lstr_idModulo, lstr_idOperacion, null, null, lstr_moneda, str_abrevAcreedor, null, null);
            if (tiposA.Tables.Count > 0 && tiposA.Tables["Table"].Rows.Count > 0)
            {
                DataTable dt_tiras = tiposA.Tables[0];
                DataRow dr_asiento = null;

                // se obtiene la cantidad de líneas que componen este asiento
                int cantidad_registros = tiposA.Tables[0].Rows.Count * 2;

                //Coleccion de asientos y tipos de asientos requeridos en SAP expuestos por la referencia del servicio
                //wsAsientos.ZfiAsiento[] tabla_asientos = new wsAsientos.ZfiAsiento[cantidad_registros];
                wrSigafAsientos.ZfiAsiento[] tabla_asientos = new wrSigafAsientos.ZfiAsiento[cantidad_registros];
                //wsAsientos.ZfiAsiento item_asiento = new wsAsientos.ZfiAsiento();
                //wsAsientos.ZfiAsiento item_asiento2 = new wsAsientos.ZfiAsiento();
                wrSigafAsientos.ZfiAsiento item_asiento = new wrSigafAsientos.ZfiAsiento();
                wrSigafAsientos.ZfiAsiento item_asiento2 = new wrSigafAsientos.ZfiAsiento();

                //variables de proceso de asiento
                string[] item_resAsientosLog = new string[10];
                string logAsiento = string.Empty;

                DateTime fechaContabilizacion = DateTime.Now;
                bool encabezadoEnviado = false;

                try
                {
                    // se itera sobre las tiras que componen el asiento y se construye el arreglo a enviar a SIGAF
                    for (int i = 0; i < cantidad_registros; i++)
                    {
                        dr_asiento = dt_tiras.Rows[i];
                        item_asiento = new wrSigafAsientos.ZfiAsiento();
                        if (dr_asiento["IdClaveContable"].ToString().Trim() != null && dr_asiento["IdClaveContable"].ToString().Trim() != "")
                        {
                            ///*************************************************** Encabezado (solo se contruye una vez) *****************************************************/
                            if (i == 0 && !encabezadoEnviado)
                            {
                                item_asiento.Blart = dr_asiento["CodigoAuxiliar4"].ToString().Trim();//Clase de documento
                                item_asiento.Bukrs = lstr_sociedad;//Sociedad
                                item_asiento.Bldat = fechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de documento
                                item_asiento.Budat = fechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de contabilización

                                encabezadoEnviado = true;
                            }
                            ///***************************************************Cargar cuenta 40 HABER*****************************************************/
                            item_asiento.Waers = lstr_moneda;//Moneda 
                            //item_asiento.Xblnr = "REF";
                            //item_asiento.Bktxt = "Texto_Cabecera";
                            //item_asiento.Xref1Hd = idexpediente;//numero expediente 
                            item_asiento.Xref2Hd = lstr_idOperacion + '-' + lstr_abrevAcreedor;//CT01-AG operacion+codigoprocesal expediente
                            item_asiento.Bschl = dr_asiento["IdClaveContable"].ToString().Trim();//Clave de contabilización
                            item_asiento.Hkont = dr_asiento["IdCuentaContable"].ToString().Trim();//Cuenta de mayor
                            item_asiento.Wrbtr = ldec_monto;//Importe o monto en colones a contabilizar 
                            //item_asiento.Zuonr = "Asig_1";
                            //item_asiento.Sgtxt = "SG-Liquidacion";
                            //item_asiento.Projk = ldat_TiposAsientos.Rows[i]["IdElementoPEP"].ToString().TrimEnd();
                            //item_asiento.Fipex = "NULA_SIN_PRESU";//Posición presupuestaria
                            //item_asiento.Kostl = ldat_TiposAsientos.Rows[i]["IdCentroCosto"].ToString();
                            //item_asiento.Fistl = ldat_TiposAsientos.Rows[i]["IdCentroGestor"].ToString();
                            //item_asiento.Prctr = ldat_TiposAsientos.Rows[i]["IdCentroBeneficio"].ToString();
                            //item_asiento.Measure = ldat_TiposAsientos.Rows[i]["IdPrograma"].ToString().TrimEnd();//Programa presupuestario
                            item_asiento.Geber = dr_asiento["IdFondo"].ToString().Trim();//Fondo
                            //item_asiento.Fkber = "";
                            //item_asiento.Xref2 = "";
                            tabla_asientos[i] = item_asiento;
                        }
                        item_asiento2 = new wrSigafAsientos.ZfiAsiento();
                        if (dr_asiento["IdClaveContable2"].ToString().Trim() != null && dr_asiento["IdClaveContable2"].ToString().Trim() != "")
                        {
                            ///*************************************************** Encabezado (solo se contruye una vez) *****************************************************/
                            if (i == 0 && !encabezadoEnviado)
                            {
                                item_asiento.Blart = dr_asiento["CodigoAuxiliar4"].ToString().Trim();//Clase de documento
                                item_asiento.Bukrs = lstr_sociedad;//Sociedad
                                item_asiento.Bldat = fechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de documento
                                item_asiento.Budat = fechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de contabilización

                                encabezadoEnviado = true;
                            }
                            ///***************************************************Cargar cuenta 50 DEBE*****************************************************/
                            item_asiento2.Waers = lstr_moneda;//Moneda 
                            item_asiento2.Bschl = dr_asiento["IdClaveContable2"].ToString().Trim();//Clave de contabilización
                            item_asiento2.Hkont = dr_asiento["IdCuentaContable2"].ToString().Trim();//Cuenta de mayor
                            item_asiento2.Wrbtr = ldec_monto;//Importe o monto en colones a contabilizar
                            //item_asiento2.Zuonr = "Asig_2";
                            //item_asiento2.Sgtxt = "SG-Liquidacion";//char 50
                            //item_asiento2.Kostl = ldat_TiposAsientos.Rows[i]["IdCentroCosto2"].ToString();
                            //item_asiento2.Fistl = ldat_TiposAsientos.Rows[i]["IdCentroGestor2"].ToString();
                            //item_asiento2.Prctr = ldat_TiposAsientos.Rows[i]["IdCentroBeneficio2"].ToString();
                            item_asiento2.Geber = dr_asiento["IdFondo2"].ToString().Trim();//Fondo
                            //item_asiento2.Fkber = "";
                            //item_asiento2.Xref2 = "xref2";
                            tabla_asientos[i + 1] = item_asiento2;
                        }
                        i++;
                    }

                    //Carga de Asientos 
                    //envio de asiento mediante servicio web hacia SIGAF
                    item_resAsientosLog = tAsiento.EnviarAsientos(tabla_asientos, "");
                    for (int j = 0; j < item_resAsientosLog.Length; j++)
                    {
                        int x = j + 1;
                        logAsiento += "\n" + x + "-" + item_resAsientosLog[j];
                    }
                    //MessageBox.Show("Resultado de contabilización: \n\n"+logAsiento);
                    Log.Info("Resultado de contabilización: \n\n" + logAsiento);
                }
                catch (Exception ex)
                {
                    lstr_CodResultado = "01";
                    lstr_Mensaje = "Error al contabilizar asiento de Devengo (Deuda Externa). Operación: " + lstr_idOperacion + ". Acreedor: " +
                        lstr_abrevAcreedor + ". " + ex.Message;

                    Log.Info(lstr_Mensaje);
                    bool_enviado = false;
                }

                resAsientosLog = reg_Bitacora.ufnRegistrarAccionBitacora("DE", "1", "Devengo", "Resultado de Contabilización Devengo: " + logAsiento);
            
            }

            return bool_enviado;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str_Mensaje"></param>
        /// <param name="lstr_IdPrestamo"></param>
        /// <param name="lint_IdTramo"></param>
        /// <param name="ldt_FchHasta"></param>
        /// <returns></returns>
        public string ReversarDevengoDE(out String str_Mensaje, string lstr_IdPrestamo = "", int? lint_IdTramo = null, DateTime? ldt_FchHasta = null)
        {
            DateTime ldt_FechaHasta = (ldt_FchHasta == null) ? DateTime.Today : Convert.ToDateTime(ldt_FchHasta);
            string str_Resultado = "";
            str_Mensaje = "00 - Proceso finalizado";

            DateTime ldt_FechaIni = ldt_FechaHasta.AddMonths(-1);
            ldt_FechaIni = ldt_FechaIni.AddDays(1);
            DateTime? ldt_FchConsultaIni = null;
            DateTime? ldt_FchConsultaFin = null;
            try
            {
                //saco los prestamos que hayan tenido pago en el mes.
                DataSet ds_PrestamosPago = dinamica.ConsultarDinamico("select distinct idprestamo, idtramo from cf.Amortizaciones where (idprestamo = '" + lstr_IdPrestamo + "' OR ISNULL('" + lstr_IdPrestamo + "','')='') and (idtramo = " + lint_IdTramo.ToString() + " OR ISNULL(" + lint_IdTramo.ToString() + ",-1)=-1) and secuencia !=0 and FchTipoCambio >= '" + ldt_FechaIni.Year + "-" + ldt_FechaIni.Month + "-" + ldt_FechaIni.Day + "' and FchTipoCambio <= '" + ldt_FechaHasta.Year + "-" + ldt_FechaHasta.Month + "-" + ldt_FechaHasta.Day + "' union select distinct idprestamo, idtramo from cf.InteresesPagos where (idprestamo = '" + lstr_IdPrestamo + "' OR ISNULL('" + lstr_IdPrestamo + "','')='') and (idtramo = " + lint_IdTramo.ToString() + " OR ISNULL(" + lint_IdTramo.ToString() + ",-1)=-1) and Secuencia != 0 and FchTipoCambio >= '" + ldt_FechaIni.Year + "-" + ldt_FechaIni.Month + "-" + ldt_FechaIni.Day + "' and FchTipoCambio <= '" + ldt_FechaHasta.Year + "-" + ldt_FechaHasta.Month + "-" + ldt_FechaHasta.Day + "'");

                if (ds_PrestamosPago.Tables.Count > 0) 
                { 
                    DataTable dt_PrestamosPago = ds_PrestamosPago.Tables[0];

                    //debo sacar la fecha del pago anterior al pago del mes, y si es el primer pago, reversar todo desde el inicio
                    foreach (DataRow dr_PrestamoPago in dt_PrestamosPago.Rows)
                    {
                        DataSet ds_Pagos = this.ConsultaPagosDE(dr_PrestamoPago["IdPrestamo"].ToString(), Convert.ToInt32(dr_PrestamoPago["IdTramo"]), null, ldt_FchHasta);//ConsultaFlujoEfectivoDeudaExt(lstr_IdPrestamo, lint_IdTramo, null, ldt_FechaHasta);//saco los tramos//;//saco los tramos

                        if (ds_Pagos.Tables.Count > 0 && ds_Pagos.Tables["Table"].Rows.Count > 0)
                        {
                            DataTable dt_Pagos = ds_Pagos.Tables[0];
                            int ctd = dt_Pagos.Rows.Count;
                            if (ctd > 0)
                            {

                                DataRow dr_Pago = dt_Pagos.Rows[0];
                                ldt_FchConsultaFin = Convert.ToDateTime(dr_Pago["Fecha"]);
                                if (ctd == 1)//solo hay un pago, se debe devolver hasta la fecha inicial del desembolso
                                {
                                    ldt_FchConsultaIni = null;
                                }
                                else
                                {
                                    dr_Pago = dt_Pagos.Rows[1];
                                    ldt_FchConsultaIni = Convert.ToDateTime(dr_Pago["Fecha"]).AddDays(1);//fecha del segundo pago
                                }


                                DataSet ds_Asientos = tAsiento.ConsultarAsientos(null, "DE", dr_PrestamoPago["IdPrestamo"].ToString() + "." + dr_PrestamoPago["IdTramo"].ToString(), "DEVENGO*", null, ldt_FchConsultaIni, ldt_FchConsultaFin);

                                ds_Asientos.Merge(tAsiento.ConsultarAsientos(null, "DE", dr_PrestamoPago["IdPrestamo"].ToString() + "." + dr_PrestamoPago["IdTramo"].ToString(), "DEVENGO+", null, ldt_FchConsultaIni, ldt_FchConsultaFin));

                                ds_Asientos.Merge(tAsiento.ConsultarAsientos(null, "DE", dr_PrestamoPago["IdPrestamo"].ToString() + "." + dr_PrestamoPago["IdTramo"].ToString(), "DEVENGO-", null, ldt_FchConsultaIni, ldt_FchConsultaFin));

                                DataTable dt_Asientos = ds_Asientos.Tables[0];

                                foreach (DataRow dr_Asiento in dt_Asientos.Rows)
                                {
                                    if (dr_Asiento["Estado"].ToString().Trim() == "C" || dr_Asiento["Estado"].ToString().Trim() == "A")//solo se reversa lo que tenga estado C = Contabilizado
                                    {
                                        string str_Codigo = string.Empty;
                                        //string str_Resultado = string.Empty;
                                        this.ReversarAsiento(Convert.ToInt32(dr_Asiento["Consecutivo"]), "", Convert.ToDateTime(ldt_FchHasta), out str_Codigo, out str_Resultado);

                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {

                str_Mensaje = "99 - " + e.ToString();
            }
            return str_Mensaje;//str_Mensaje;
        }

        public bool ReversarAsiento(
              int? lint_Consecutivo, string CodAsiento, DateTime ldt_FechaHasta, out string lstr_CodResultado, out string lstr_Mensaje)
        {
            return tAsiento.ReversarAsiento(lint_Consecutivo, CodAsiento, ldt_FechaHasta, out lstr_CodResultado, out lstr_Mensaje);
        }
        #endregion
    }
}