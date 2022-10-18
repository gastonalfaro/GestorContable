using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio.Contingentes;
using LogicaNegocio.Mantenimiento;
using LogicaNegocio.Seguridad;
using System.Configuration;
using System.Data.SqlClient;
using System;
using System.IO;
using System.Text;


namespace SGDiferencialCambiario
{
    class Program
    {
        //private static SGDiferencialCambiario.wsAsientos.ServicioContable ws_ContabilizaAsientos = new SGDiferencialCambiario.wsAsientos.ServicioContable();

        //private static wsSG.wsSistemaGestor ws_SGService = new wsSG.wsSistemaGestor();

        private static clsCobrosPagos reg_clsCobrosPagos = new clsCobrosPagos();
        private static clsTiposCambio reg_TiposCambio = new clsTiposCambio();
        private static clsAntiguedadDeSaldos reg_Antiguedad = new clsAntiguedadDeSaldos();
        private static clsDinamico reg_Dinamico = new clsDinamico();
        private static clsTiposAsiento reg_TiposAsiento = new clsTiposAsiento();
        private static clsValoresIndicadoresEco reg_ValoresIndicadoresEco = new clsValoresIndicadoresEco();
        private static tBitacora reg_Bit = new tBitacora();
        private static clsCobrosPagos reg_CP = new clsCobrosPagos();

        private static clsBitacoraDeMovimientosCuentasExpedientes reg_Bita = new clsBitacoraDeMovimientosCuentasExpedientes();

        //private static wsAsientos.ServicioContable asientos = new wsAsientos.ServicioContable();
        private static clsResoluciones lresoluciones = new clsResoluciones();

        private static string str_Mensaje;

        private static string gstr_Usuario = "SG";
       

        static void Main(string[] args)
        {
            int res = 0;
            Console.WriteLine("Ejecutar aplicacion:"); // Prompt
            str_Mensaje = "Ejecutar aplicacion:" + Environment.NewLine;
            DateTime now = DateTime.Now;
            Int32 diasMes = DateTime.DaysInMonth(now.Year, now.Month);
            //ggarcia acá van los procesos automáticos mensuales, algunos procesos se corren el ultimo dia del mes aunque no sean mensuales
            if (now == new DateTime(now.Year, now.Month, diasMes))
            {
                //ScriptMain x = new ScriptMain();
                //x.Main();
                try
                {
                    CalculoDiferencialCambiario();
                }

                catch (Exception e)
                {

                }
            }
            //acá van los procesos que corren todos los dias, independientemente si es fin de mes o no.
            try
            {
                CalculoIncobrabilidad();
            }
            catch (Exception e)
            {

            }
            try
            {
                res = reg_TiposAsiento.EnviarAsientosCI(null, -1);
            }
            catch (Exception e)
            {

            }
            try
            {
                res = reg_TiposAsiento.EnviarAsientosCICT(null, -1);
            }
            catch (Exception e)
            {

            }
            Console.ReadLine();
        }

        private static void CalculoDiferencialCambiario()
        {
            #region variables
            DataTable ldt_MontosExpedientes = new DataTable();
            DataSet lds_MontosExpedientes = new DataSet();

            DateTime? ldt_FechaActual = DateTime.Today;//new DateTime();
            DateTime? ldt_FchInicio = Convert.ToDateTime(Convert.ToString(DateTime.Today.Year) + "-" + Convert.ToString(DateTime.Today.Month) + "-01");

            DataRow[] ldar_Temporal;

            string[] tipocambio = new string[4];

            tipocambio = CargarIndicadoresEco();

            decimal ldec_CompraActual = tipocambio[0] == "" ? 0 : Convert.ToDecimal(tipocambio[0]);
            decimal ldec_VentaActual = tipocambio[1] == "" ? 0 : Convert.ToDecimal(tipocambio[1]);
            decimal ldec_EuroActual = tipocambio[2] == "" ? 0 : Convert.ToDecimal(tipocambio[2]);
            decimal ldec_TBPActual = tipocambio[3] == "" ? 0 : Convert.ToDecimal(tipocambio[3]);
            decimal ldec_TipoCambioActual = 0;

            String[] lstr_Resultado = new String[3] { "", "", "" };
            String[] lstr_ResultadoRegistro = new String[2];

            string lstr_IdModulo = "IdModulo In ('CT')";
            string lstr_Operacion = String.Empty;
            string lstr_TipoExpediente = string.Empty;
            int lint_CantidadLineasAsiento;
            bool lbool_cambioMonto = false;
            string lstr_Leyenda = String.Empty;
            string lstr_Transaccion = "Diferencial Cambiario";
            decimal[] larrdec_Montos;
            decimal[] larrdec_MontosArriba;
            decimal[] larrdec_MontosAbajo;
            String lstr_AsientosResultado = String.Empty;
            #endregion
            string lstr_CodAsiento = "";

            lds_MontosExpedientes = ConsultarMontosExpedientes("", "", 0, 0, "", ldt_FchInicio, ldt_FechaActual);
            ldt_MontosExpedientes = lds_MontosExpedientes.Tables["Table"];

            if ((ldt_MontosExpedientes != null) && (ldt_MontosExpedientes.Rows.Count > 0))
            {
                foreach (DataRow dr_FilaExpediente in ldt_MontosExpedientes.Rows)
                {
                    #region Inicial

                    string resultado = String.Empty;

                    decimal ldec_DifMontoPrincipal = 0;
                    decimal ldec_DifMontoIntereses = 0;

                    decimal ldec_DifIntereses = 0;
                    decimal ldec_DifInteresesMoratorios = 0;
                    decimal ldec_DifCostas = 0;
                    decimal ldec_DifDanoMoral = 0;

                    string lstr_IdExpediente = dr_FilaExpediente["IdExpedienteFK"].ToString();
                    string lstr_IdSociedad = dr_FilaExpediente["IdSociedadGL"].ToString();
                    string lstr_EstadoResolucion = dr_FilaExpediente["EstadoResolucion"].ToString();
                    String lstr_Moneda = dr_FilaExpediente["Moneda"].ToString();
                    lstr_TipoExpediente = ConsultarTipoExpediente(lstr_IdExpediente);
                    #endregion

                    //if ((lstr_IdSociedad.Contains(gstr_IdSociedadGL)))
                    // && (lstr_IdExpediente.Contains("01 - INC - LIQ")))
                    //{
                        str_Mensaje += Environment.NewLine + Environment.NewLine;
                        str_Mensaje += "______________________________________________" + Environment.NewLine;
                        str_Mensaje += "Id Expediente: ";
                        str_Mensaje += lstr_IdExpediente + Environment.NewLine;

                        #region carga data
                        decimal ldec_TipoCambio = Convert.ToDecimal(dr_FilaExpediente["TipoCambio"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["TipoCambio"]);
                        decimal ldec_Tbp = Convert.ToDecimal(dr_FilaExpediente["Tbp"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["Tbp"]);
                        decimal ldec_Tiempo = Convert.ToDecimal(dr_FilaExpediente["Tiempo"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["Tiempo"]);
                        decimal ldec_TipoCambioCierre = Convert.ToDecimal(dr_FilaExpediente["TipoCambioCierre"].ToString().Equals("") ? ldec_TipoCambio : dr_FilaExpediente["TipoCambioCierre"]);

                        decimal ldec_MontoPrincipal = Convert.ToDecimal(dr_FilaExpediente["MontoPrincipal"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoPrincipal"]);
                        decimal ldec_MontoPrincipalColones = Convert.ToDecimal(dr_FilaExpediente["MontoPrincipalColones"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoPrincipalColones"]);
                        decimal ldec_MontoPrincipalCierre = Convert.ToDecimal(dr_FilaExpediente["MontoPrincipalColonesCierre"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoPrincipalColonesCierre"]);

                        decimal ldec_MontoIntereses = Convert.ToDecimal(dr_FilaExpediente["MontoIntereses"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoIntereses"]);
                        decimal ldec_MontoInteresesColones = Convert.ToDecimal(dr_FilaExpediente["MontoInteresesColones"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoInteresesColones"]);
                        decimal ldec_MontoInteresesColonesCierre = Convert.ToDecimal(dr_FilaExpediente["MontoInteresesColonesCierre"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoInteresesColonesCierre"]);

                        decimal ldec_InteresesMoratorios = Convert.ToDecimal(dr_FilaExpediente["InteresesMoratorios"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["InteresesMoratorios"]);
                        decimal ldec_InteresesMoratoriosColones = Convert.ToDecimal(dr_FilaExpediente["InteresesMoratoriosColones"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["InteresesMoratoriosColones"]);
                        decimal ldec_InteresesMoratoriosColonesCierre = Convert.ToDecimal(dr_FilaExpediente["InteresesMoratoriosColonesCierre"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["InteresesMoratoriosColonesCierre"]);

                        decimal ldec_Intereses = Convert.ToDecimal(dr_FilaExpediente["Intereses"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["Intereses"]);
                        decimal ldec_InteresesColones = Convert.ToDecimal(dr_FilaExpediente["InteresesColones"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["InteresesColones"]);
                        decimal ldec_InteresesColonesCierre = Convert.ToDecimal(dr_FilaExpediente["InteresesColonesCierre"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["InteresesColonesCierre"]);

                        decimal ldec_Costas = Convert.ToDecimal(dr_FilaExpediente["Costas"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["Costas"]);
                        decimal ldec_CostasColones = Convert.ToDecimal(dr_FilaExpediente["CostasColones"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["CostasColones"]);
                        decimal ldec_CostasColonesCierre = Convert.ToDecimal(dr_FilaExpediente["CostasColonesCierre"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["CostasColonesCierre"]);

                        decimal ldec_DanoMoral = Convert.ToDecimal(dr_FilaExpediente["DanoMoral"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["DanoMoral"]);
                        decimal ldec_DanoMoralColones = Convert.ToDecimal(dr_FilaExpediente["DanoMoralColones"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["DanoMoralColones"]);
                        decimal ldec_DanoMoralColonesCierre = Convert.ToDecimal(dr_FilaExpediente["DanoMoralColonesCierre"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["DanoMoralColonesCierre"]);

                        Decimal ldec_MontoPrincipalAnterior = Convert.ToDecimal(dr_FilaExpediente["MontoPrincipalAnterior"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoPrincipalAnterior"]);
                        Decimal ldec_MontoInteresesAnterior = Convert.ToDecimal(dr_FilaExpediente["MontoInteresesAnterior"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoInteresesAnterior"]);
                        Decimal ldec_InteresesAnterior = Convert.ToDecimal(dr_FilaExpediente["InteresesAnterior"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["InteresesAnterior"]);
                        Decimal ldec_InteresesMoratoriosAnterior = Convert.ToDecimal(dr_FilaExpediente["InteresesMoratoriosAnterior"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["InteresesMoratoriosAnterior"]);
                        Decimal ldec_CostasAnterior = Convert.ToDecimal(dr_FilaExpediente["CostasAnterior"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["CostasAnterior"]);
                        Decimal ldec_DanoMoralAnterior = Convert.ToDecimal(dr_FilaExpediente["DanoMoralAnterior"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["DanoMoralAnterior"]);
                        #endregion

                        #region tipos cambio
                        lstr_TipoExpediente = ConsultarTipoExpediente(lstr_IdExpediente);

                        str_Mensaje += lstr_TipoExpediente + " : " + lstr_EstadoResolucion + Environment.NewLine;

                        if (lstr_Moneda.Contains("CRC"))
                            ldec_TipoCambioActual = 1;
                        else if (lstr_Moneda.Contains("USD"))
                        {
                            if (lstr_TipoExpediente.Contains("Actor"))
                                ldec_TipoCambioActual = ldec_CompraActual;
                            else if (lstr_TipoExpediente.Contains("Demandado"))
                                ldec_TipoCambioActual = ldec_VentaActual;
                        }
                        else if (lstr_Moneda.Contains("EUR"))
                        {
                            if (lstr_TipoExpediente.Contains("Actor"))
                                ldec_TipoCambioActual = ldec_CompraActual * ldec_EuroActual;
                            else if (lstr_TipoExpediente.Contains("Demandado"))
                                ldec_TipoCambioActual = ldec_VentaActual * ldec_EuroActual;
                        }

                        ldec_TipoCambioActual = Math.Round(ldec_TipoCambioActual, 2);
                        #endregion

                        #region Calculo Diferencial

                        larrdec_MontosArriba = new Decimal[15];
                        larrdec_MontosAbajo = new Decimal[15];

                        if ((ldec_MontoPrincipalCierre == 0) && (ldec_MontoInteresesColonesCierre == 0))
                        {
                            ldec_DifMontoPrincipal = (ldec_MontoPrincipal * ldec_TipoCambioActual) - (ldec_MontoPrincipal * ldec_TipoCambio);
                            ldec_DifMontoIntereses = (ldec_MontoIntereses * ldec_TipoCambioActual) - (ldec_MontoIntereses * ldec_TipoCambio);

                            ldec_MontoPrincipalCierre = ldec_MontoPrincipal * ldec_TipoCambioActual;
                            ldec_MontoInteresesColonesCierre = ldec_MontoIntereses * ldec_TipoCambioActual;
                        }
                        else
                        {
                            ldec_DifMontoPrincipal = (ldec_MontoPrincipal * ldec_TipoCambioActual) - (ldec_MontoPrincipal * ldec_TipoCambioCierre);
                            ldec_DifMontoIntereses = (ldec_MontoIntereses * ldec_TipoCambioActual) - (ldec_MontoIntereses * ldec_TipoCambioCierre);

                            ldec_MontoPrincipalCierre = ldec_MontoPrincipal * ldec_TipoCambioActual;
                            ldec_MontoInteresesColonesCierre = ldec_MontoIntereses * ldec_TipoCambioActual;
                        }

                        if ((ldec_InteresesColonesCierre == 0) || (ldec_InteresesMoratoriosColonesCierre == 0) ||
                            (ldec_CostasColonesCierre == 0) || (ldec_DanoMoralColonesCierre == 0))
                        {
                            ldec_DifIntereses = (ldec_Intereses * ldec_TipoCambioActual) - (ldec_Intereses * ldec_TipoCambio);
                            ldec_DifInteresesMoratorios = (ldec_InteresesMoratorios * ldec_TipoCambioActual) - (ldec_InteresesMoratorios * ldec_TipoCambio);
                            ldec_DifCostas = (ldec_Costas * ldec_TipoCambioActual) - (ldec_Costas * ldec_TipoCambio);
                            ldec_DifDanoMoral = (ldec_DanoMoral * ldec_TipoCambioActual) - (ldec_DanoMoral * ldec_TipoCambio);

                            ldec_InteresesColonesCierre = ldec_Intereses * ldec_TipoCambioActual;
                            ldec_InteresesMoratoriosColonesCierre = ldec_InteresesMoratorios * ldec_TipoCambioActual;
                            ldec_CostasColonesCierre = ldec_Costas * ldec_TipoCambioActual;
                            ldec_DanoMoralColonesCierre = ldec_DanoMoral * ldec_TipoCambioActual;
                        }
                        else
                        {
                            ldec_DifIntereses = (ldec_Intereses * ldec_TipoCambioActual) - (ldec_Intereses * ldec_TipoCambioCierre);
                            ldec_DifInteresesMoratorios = (ldec_InteresesMoratorios * ldec_TipoCambioActual) - (ldec_InteresesMoratorios * ldec_TipoCambioCierre);
                            ldec_DifCostas = (ldec_Costas * ldec_TipoCambioActual) - (ldec_Costas * ldec_TipoCambioCierre);
                            ldec_DifDanoMoral = (ldec_DanoMoral * ldec_TipoCambioActual) - (ldec_DanoMoral * ldec_TipoCambioCierre);

                            ldec_InteresesColonesCierre = ldec_Intereses * ldec_TipoCambioActual;
                            ldec_InteresesMoratoriosColonesCierre = ldec_InteresesMoratorios * ldec_TipoCambioActual;
                            ldec_CostasColonesCierre = ldec_Costas * ldec_TipoCambioActual;
                            ldec_DanoMoralColonesCierre = ldec_DanoMoral * ldec_TipoCambioActual;
                        }

                        if (ldec_DifMontoPrincipal > 0)
                            larrdec_MontosArriba[0] = ldec_DifMontoPrincipal;
                        else if (ldec_DifMontoPrincipal < 0)
                            larrdec_MontosAbajo[0] = ldec_DifMontoPrincipal * -1;

                        if (ldec_DifMontoIntereses > 0)
                            larrdec_MontosArriba[1] = ldec_DifMontoIntereses;
                        else if (ldec_DifMontoIntereses < 0)
                            larrdec_MontosAbajo[1] = ldec_DifMontoIntereses * -1;

                        if (ldec_DifIntereses > 0)
                            larrdec_MontosArriba[1] = ldec_DifIntereses;
                        else if (ldec_DifIntereses < 0)
                            larrdec_MontosAbajo[1] = ldec_DifIntereses * -1;

                        if (ldec_DifInteresesMoratorios > 0)
                            larrdec_MontosArriba[2] = ldec_DifInteresesMoratorios;
                        else if (ldec_DifInteresesMoratorios < 0)
                            larrdec_MontosAbajo[2] = ldec_DifInteresesMoratorios * -1;

                        if (ldec_DifCostas > 0)
                            larrdec_MontosArriba[3] = ldec_DifCostas;
                        else if (ldec_DifCostas < 0)
                            larrdec_MontosAbajo[3] = ldec_DifCostas * -1;

                        if (ldec_DifDanoMoral > 0)
                            larrdec_MontosArriba[4] = ldec_DifDanoMoral;
                        else if (ldec_DifDanoMoral < 0)
                            larrdec_MontosAbajo[4] = ldec_DifDanoMoral * -1;

                        Boolean lbool_continuar = false;
                        for (int j = 0; j < larrdec_MontosArriba.Count(); j++)
                        {
                            if (larrdec_MontosArriba[j] > 0)
                            {
                                lbool_continuar = true;
                            }
                        }
                        #endregion

                        if ((lstr_Moneda.Contains("USD") || lstr_Moneda.Contains("EUR")))
                        {
                            #region envio asientos
                            if ((((ldec_DifMontoPrincipal > 0 || ldec_DifMontoIntereses > 0) ||
                                (ldec_DifIntereses > 0 || ldec_DifInteresesMoratorios > 0 || ldec_DifCostas > 0 || ldec_DifDanoMoral > 0))
                                && lbool_continuar) ||

                                (((ldec_DifMontoPrincipal < 0 || ldec_DifMontoIntereses < 0) ||
                                (ldec_DifIntereses < 0 || ldec_DifInteresesMoratorios < 0 || ldec_DifCostas < 0 || ldec_DifDanoMoral < 0))
                                && !lbool_continuar))
                            {
                                #region diferencial
                                if (ldec_TipoCambioActual > ldec_TipoCambioCierre)
                                {
                                    lbool_continuar = true;
                                }
                                else
                                {
                                    lbool_continuar = false;
                                }

                                if (lstr_TipoExpediente.Contains("Demandado") &&
                                    (lstr_EstadoResolucion.Contains("En Firme") || lstr_EstadoResolucion.Contains("Liquidacion")))
                                {
                                    #region Demandado RF Liq
                                    lint_CantidadLineasAsiento = 12;

                                    if (lbool_continuar)
                                    {
                                        lstr_Resultado = EnviarAsientos2(lstr_IdExpediente, lstr_IdSociedad, lstr_AsientosResultado, lstr_IdModulo, "CT22", lstr_Transaccion, lstr_Leyenda, lbool_cambioMonto, larrdec_MontosArriba, lint_CantidadLineasAsiento, ldec_DifMontoPrincipal, ldec_DifMontoIntereses, out lstr_CodAsiento);

                                        str_Mensaje += lstr_Resultado[1] + Environment.NewLine;
                                        str_Mensaje += lstr_Resultado[2] + Environment.NewLine;
                                    }
                                    else
                                    {
                                        lstr_AsientosResultado = String.Empty;
                                        lstr_Resultado = EnviarAsientos2(lstr_IdExpediente, lstr_IdSociedad, lstr_AsientosResultado, lstr_IdModulo, "CT23", lstr_Transaccion, lstr_Leyenda, lbool_cambioMonto, larrdec_MontosAbajo, lint_CantidadLineasAsiento, ldec_DifMontoPrincipal, ldec_DifMontoIntereses, out lstr_CodAsiento);

                                        str_Mensaje += lstr_Resultado[1] + Environment.NewLine;
                                        str_Mensaje += lstr_Resultado[2] + Environment.NewLine;
                                    }
                                    if (lstr_Resultado.Contains("Contabilizado"))
                                    {
                                        lstr_Resultado[0] = "exito";
                                    }
                                    else
                                    {
                                        lstr_Resultado[0] = "fallo";
                                    }
                                    #endregion
                                }
                                else if (lstr_TipoExpediente.Contains("Demandado"))
                                {
                                    #region Demandado
                                    lint_CantidadLineasAsiento = 4;

                                    if (lbool_continuar)
                                    {
                                        lstr_AsientosResultado = String.Empty;
                                        lstr_Resultado = EnviarAsientos2(lstr_IdExpediente, lstr_IdSociedad, lstr_AsientosResultado, lstr_IdModulo, "CT28", lstr_Transaccion, lstr_Leyenda, lbool_cambioMonto, larrdec_MontosArriba, lint_CantidadLineasAsiento, ldec_DifMontoPrincipal, ldec_DifMontoIntereses, out lstr_CodAsiento);

                                        str_Mensaje += lstr_Resultado[1] + Environment.NewLine;
                                        str_Mensaje += lstr_Resultado[2] + Environment.NewLine;
                                    }
                                    else
                                    {
                                        lstr_AsientosResultado = String.Empty;
                                        lstr_Resultado = EnviarAsientos2(lstr_IdExpediente, lstr_IdSociedad, lstr_AsientosResultado, lstr_IdModulo, "CT29", lstr_Transaccion, lstr_Leyenda, lbool_cambioMonto, larrdec_MontosAbajo, lint_CantidadLineasAsiento, ldec_DifMontoPrincipal, ldec_DifMontoIntereses, out lstr_CodAsiento);

                                        str_Mensaje += lstr_Resultado[1] + Environment.NewLine;
                                        str_Mensaje += lstr_Resultado[2] + Environment.NewLine;
                                    }
                                    if (lstr_Resultado.Contains("Contabilizado"))
                                    {
                                        lstr_Resultado[0] = "exito";
                                    }
                                    else
                                    {
                                        lstr_Resultado[0] = "fallo";
                                    }
                                    #endregion
                                }
                                else if (lstr_TipoExpediente.Contains("Actor") &&
                                    (lstr_EstadoResolucion.Contains("En Firme") || lstr_EstadoResolucion.Contains("Liquidacion")))
                                {
                                    #region Actor RF Liq
                                    lint_CantidadLineasAsiento = 12;
                                    if (lbool_continuar)
                                    {
                                        lstr_AsientosResultado = String.Empty;
                                        lstr_Resultado = EnviarAsientos2(lstr_IdExpediente, lstr_IdSociedad, lstr_AsientosResultado, lstr_IdModulo, "CT24", lstr_Transaccion, lstr_Leyenda, lbool_cambioMonto, larrdec_MontosArriba, lint_CantidadLineasAsiento, ldec_DifMontoPrincipal, ldec_DifMontoIntereses, out lstr_CodAsiento);

                                        str_Mensaje += lstr_Resultado[1] + Environment.NewLine;
                                        str_Mensaje += lstr_Resultado[2] + Environment.NewLine;
                                    }
                                    else
                                    {
                                        lstr_AsientosResultado = String.Empty;

                                        lstr_Resultado = EnviarAsientos2(lstr_IdExpediente, lstr_IdSociedad, lstr_AsientosResultado, lstr_IdModulo, "CT25", lstr_Transaccion, lstr_Leyenda, lbool_cambioMonto, larrdec_MontosAbajo, lint_CantidadLineasAsiento, ldec_DifMontoPrincipal, ldec_DifMontoIntereses, out lstr_CodAsiento);

                                        str_Mensaje += lstr_Resultado[1] + Environment.NewLine;
                                        str_Mensaje += lstr_Resultado[2] + Environment.NewLine;
                                    }
                                    if (lstr_Resultado.Contains("Contabilizado"))
                                    {
                                        lstr_Resultado[0] = "exito";
                                    }
                                    else
                                    {
                                        lstr_Resultado[0] = "fallo";
                                    }
                                    #endregion
                                }


                                #endregion

                                str_Mensaje += "------------------------------------------------" + Environment.NewLine;
                            }
                            #endregion
                        }

                        Int32 lint_IdRes = ConsultarIdRes(lstr_IdExpediente, lstr_IdSociedad, lstr_EstadoResolucion);

                        if (lstr_Resultado[0].Contains("exito") || lstr_Moneda.Contains("CRC"))
                        {
                            try
                            {
                                #region Registro
                                lstr_ResultadoRegistro = //ws_SGService.uwsModificarCobrosPagos(
                                    reg_clsCobrosPagos.ModificarCobrosPagos(
                                    lstr_IdExpediente, lstr_IdSociedad, lint_IdRes,
                                    lstr_Moneda, ldec_TipoCambio, ldec_Tbp, ldec_Tiempo, ldec_TipoCambioActual,

                                    ldec_MontoPrincipal, ldec_MontoPrincipalColones, ldec_MontoPrincipalCierre,
                                    ldec_MontoIntereses, ldec_MontoInteresesColones, ldec_MontoInteresesColonesCierre,
                                    0, 0, 0,
                                    0, 0, 0,
                                    ldec_Intereses, ldec_InteresesColones, ldec_InteresesColonesCierre,
                                    ldec_InteresesMoratorios, ldec_InteresesMoratoriosColones, ldec_InteresesMoratoriosColonesCierre,
                                    ldec_Costas, ldec_CostasColones, ldec_CostasColonesCierre,
                                    ldec_DanoMoral, ldec_DanoMoralColones, ldec_DanoMoralColonesCierre,
                                    ldec_MontoPrincipalAnterior, ldec_MontoInteresesAnterior,
                                    ldec_InteresesAnterior, ldec_CostasAnterior,
                                    ldec_InteresesMoratoriosAnterior, ldec_DanoMoralAnterior,
                                    "Diferencial", "Diferencial");

                                if (lstr_ResultadoRegistro[0].Contains("00"))
                                {
                                    lstr_Resultado[0] = "exito";
                                    str_Mensaje += "Expediente " + lstr_IdExpediente + " modificado con éxito." + Environment.NewLine;
                                }
                                else
                                {
                                    lstr_Resultado[0] = "fallo";
                                    str_Mensaje += "Fallo al modificar expediente " + lstr_IdExpediente + "." + Environment.NewLine +
                                        lstr_ResultadoRegistro[0] + ": " + lstr_ResultadoRegistro[1] + Environment.NewLine;
                                }


                                #endregion
                            }
                            catch (Exception ex)
                            {
                                str_Mensaje += "Fallo al modificar expediente " + lstr_IdExpediente + "." + Environment.NewLine +
                                "Error: " + ex.Message + Environment.NewLine;

                            }
                        }
                    //}

                }

                str_Mensaje += "Fin de Proceso\n";
                str_Mensaje += "------------------------------------------------" + Environment.NewLine;

                GuardarResultadoContabilizacion();
            }
        }

        private static void GuardarResultadoContabilizacion()
        {
            string path = @"C:\inetpub\wwwroot\SistemaGestor\Logs\LogResoluciones " +
                DateTime.Now.Year + "." +
                DateTime.Now.Month + "." +
                DateTime.Now.Day + " " +
                DateTime.Now.Hour + "." +
                DateTime.Now.Minute + "." +
                DateTime.Now.Second + ".txt";
            //string path = @"C:\Logs\DiferencialCambiarioContingentes\LogDifCambiario.txt";

            // This text is added only once to the file.
            //if (!File.Exists(path))
            //{
            // Create a file to write to.
            File.WriteAllText(path, str_Mensaje);
            //}
            string readText = File.ReadAllText(path);

        }

        private static void CalculoIncobrabilidad()
        {
            #region variables
            DataTable ldt_MontosExpedientes = new DataTable();
            DataSet lds_MontosExpedientes = new DataSet();

            DateTime? ldt_FechaActual = DateTime.Today;//new DateTime();
            DateTime? ldt_FchInicio = Convert.ToDateTime(Convert.ToString(DateTime.Today.Year) + "-" + Convert.ToString(DateTime.Today.Month) + "-01");

            DataRow[] ldar_Temporal;

            string[] tipocambio = new string[4];

            tipocambio = CargarIndicadoresEco();
            tipocambio = CargarIndicadoresEco();

            decimal ldec_CompraActual = tipocambio[0] == "" ? 0 : Convert.ToDecimal(tipocambio[0]);
            decimal ldec_VentaActual = tipocambio[1] == "" ? 0 : Convert.ToDecimal(tipocambio[1]);
            decimal ldec_EuroActual = tipocambio[2] == "" ? 0 : Convert.ToDecimal(tipocambio[2]);
            decimal ldec_TBPActual = tipocambio[3] == "" ? 0 : Convert.ToDecimal(tipocambio[3]);
            decimal ldec_TipoCambioActual = 0;

            String[] lstr_Resultado = new String[3] { "", "", "" };
            String[] lstr_ResultadoRegistro = new String[2];

            string lstr_IdModulo = "IdModulo In ('CT')";
            string lstr_Operacion = String.Empty;
            string lstr_TipoExpediente = string.Empty;
            int lint_CantidadLineasAsiento;
            bool lbool_cambioMonto = false;
            string lstr_Leyenda = String.Empty;
            string lstr_Transaccion = "Diferencial Cambiario";
            decimal[] larrdec_Montos;
            decimal[] larrdec_MontosArriba;
            decimal[] larrdec_MontosAbajo;
            String lstr_AsientosResultado = String.Empty;

            //gbool_CambioMes = this.ckbNuevoMes.Checked;
            //gbool_CambioAno = this.ckbNuevoAno.Checked;

            #endregion
            string lstr_CodAsiento = string.Empty;

            lds_MontosExpedientes = ConsultarMontosExpedientes("", "", 0, 0, "", ldt_FchInicio, ldt_FechaActual);
            ldt_MontosExpedientes = lds_MontosExpedientes.Tables["Table"];

            if (ldt_MontosExpedientes.Rows.Count > 0)
            {
                foreach (DataRow dr_FilaExpediente in ldt_MontosExpedientes.Rows)
                {
                    #region Inicial

                    str_Mensaje += "______________________________________________" + Environment.NewLine;
                    str_Mensaje += "Id Expediente: " + Environment.NewLine;
                    str_Mensaje += dr_FilaExpediente["IdExpedienteFK"].ToString() + Environment.NewLine;
                    str_Mensaje += "______________________________________________" + Environment.NewLine;

                    string resultado = String.Empty;

                    decimal ldec_DifMontoPrincipal = 0;
                    decimal ldec_DifMontoIntereses = 0;

                    decimal ldec_DifIntereses = 0;
                    decimal ldec_DifInteresesMoratorios = 0;
                    decimal ldec_DifCostas = 0;
                    decimal ldec_DifDanoMoral = 0;

                    string lstr_IdExpediente = dr_FilaExpediente["IdExpedienteFK"].ToString();
                    string lstr_IdSociedad = dr_FilaExpediente["IdSociedadGL"].ToString();
                    string lstr_EstadoResolucion = dr_FilaExpediente["EstadoResolucion"].ToString();
                    String lstr_Moneda = dr_FilaExpediente["Moneda"].ToString();
                    lstr_TipoExpediente = ConsultarTipoExpediente(lstr_IdExpediente);

                    DateTime ldt_FchModifica = Convert.ToDateTime(dr_FilaExpediente["FchModifica"].ToString());
                    DateTime ldt_FchCreacion = Convert.ToDateTime(dr_FilaExpediente["FchCreacion"].ToString());
                    Int32 gint_Periodo = ldt_FchModifica.Year;
                    #endregion

                    if (lstr_Moneda.Contains("USD") || lstr_Moneda.Contains("EUR") || (lstr_Moneda.Contains("CRC")))
                    {
                        #region carga data
                        Decimal ldec_TipoCambio = Convert.ToDecimal(dr_FilaExpediente["TipoCambio"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["TipoCambio"].ToString());
                        Decimal ldec_Tbp = Convert.ToDecimal(dr_FilaExpediente["Tbp"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["Tbp"].ToString());
                        Decimal ldec_Tiempo = Convert.ToDecimal(dr_FilaExpediente["Tiempo"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["Tiempo"].ToString());
                        Decimal ldec_TipoCambioCierre = Convert.ToDecimal(dr_FilaExpediente["TipoCambioCierre"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["TipoCambioCierre"].ToString());

                        Decimal ldec_MontoPrincipal = Convert.ToDecimal(dr_FilaExpediente["MontoPrincipal"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoPrincipal"]);
                        Decimal ldec_MontoPrincipalColones = Convert.ToDecimal(dr_FilaExpediente["MontoPrincipalColones"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoPrincipalColones"]);
                        Decimal ldec_MontoPrincipalCierre = Convert.ToDecimal(dr_FilaExpediente["MontoPrincipalColonesCierre"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoPrincipalColonesCierre"]);

                        Decimal ldec_MontoIntereses = Convert.ToDecimal(dr_FilaExpediente["MontoIntereses"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoIntereses"]);
                        Decimal ldec_MontoInteresesColones = Convert.ToDecimal(dr_FilaExpediente["MontoInteresesColones"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoInteresesColones"]);
                        Decimal ldec_MontoInteresesColonesCierre = Convert.ToDecimal(dr_FilaExpediente["MontoInteresesColonesCierre"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoInteresesColonesCierre"]);

                        Decimal ldec_InteresesMoratorios = Convert.ToDecimal(dr_FilaExpediente["InteresesMoratorios"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["InteresesMoratorios"]);
                        Decimal ldec_InteresesMoratoriosColones = Convert.ToDecimal(dr_FilaExpediente["InteresesMoratoriosColones"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["InteresesMoratoriosColones"]);
                        Decimal ldec_InteresesMoratoriosColonesCierre = Convert.ToDecimal(dr_FilaExpediente["InteresesMoratoriosColonesCierre"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["InteresesMoratoriosColonesCierre"]);

                        Decimal ldec_Intereses = Convert.ToDecimal(dr_FilaExpediente["Intereses"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["Intereses"]);
                        Decimal ldec_InteresesColones = Convert.ToDecimal(dr_FilaExpediente["InteresesColones"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["InteresesColones"]);
                        Decimal ldec_InteresesColonesCierre = Convert.ToDecimal(dr_FilaExpediente["InteresesColonesCierre"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["InteresesColonesCierre"]);

                        Decimal ldec_Costas = Convert.ToDecimal(dr_FilaExpediente["Costas"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["Costas"]);
                        Decimal ldec_CostasColones = Convert.ToDecimal(dr_FilaExpediente["CostasColones"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["CostasColones"]);
                        Decimal ldec_CostasColonesCierre = Convert.ToDecimal(dr_FilaExpediente["CostasColonesCierre"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["CostasColonesCierre"]);

                        Decimal ldec_DanoMoral = Convert.ToDecimal(dr_FilaExpediente["DanoMoral"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["DanoMoral"]);
                        Decimal ldec_DanoMoralColones = Convert.ToDecimal(dr_FilaExpediente["DanoMoralColones"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["DanoMoralColones"]);
                        Decimal ldec_DanoMoralColonesCierre = Convert.ToDecimal(dr_FilaExpediente["DanoMoralColonesCierre"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["DanoMoralColonesCierre"]);

                        Decimal ldec_MontoPrincipalAnterior = Convert.ToDecimal(dr_FilaExpediente["MontoPrincipalAnterior"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoPrincipalAnterior"]);
                        Decimal ldec_MontoInteresesAnterior = Convert.ToDecimal(dr_FilaExpediente["MontoInteresesAnterior"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoInteresesAnterior"]);
                        Decimal ldec_InteresesAnterior = Convert.ToDecimal(dr_FilaExpediente["InteresesAnterior"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["InteresesAnterior"]);
                        Decimal ldec_InteresesMoratoriosAnterior = Convert.ToDecimal(dr_FilaExpediente["InteresesMoratoriosAnterior"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["InteresesMoratoriosAnterior"]);
                        Decimal ldec_CostasAnterior = Convert.ToDecimal(dr_FilaExpediente["CostasAnterior"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["CostasAnterior"]);
                        Decimal ldec_DanoMoralAnterior = Convert.ToDecimal(dr_FilaExpediente["DanoMoralAnterior"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["DanoMoralAnterior"]);
                        #endregion

                        #region tipos cambio
                        lstr_TipoExpediente = ConsultarTipoExpediente(lstr_IdExpediente);
                        str_Mensaje += lstr_TipoExpediente + Environment.NewLine;

                        if (lstr_Moneda.Contains("CRC"))
                            ldec_TipoCambioActual = 1;
                        else if (lstr_Moneda.Contains("USD"))
                        {
                            if (lstr_TipoExpediente.Contains("Actor"))
                                ldec_TipoCambioActual = ldec_CompraActual;
                            else if (lstr_TipoExpediente.Contains("Demandado"))
                                ldec_TipoCambioActual = ldec_VentaActual;
                        }
                        else if (lstr_Moneda.Contains("EUR"))
                        {
                            if (lstr_TipoExpediente.Contains("Actor"))
                                ldec_TipoCambioActual = ldec_CompraActual * ldec_EuroActual;
                            else if (lstr_TipoExpediente.Contains("Demandado"))
                                ldec_TipoCambioActual = ldec_VentaActual * ldec_EuroActual;
                        }
                        #endregion

                        #region Cálculo Incobrabilidad

                        larrdec_MontosArriba = new Decimal[5];
                        larrdec_MontosAbajo = new Decimal[5];

                        TimeSpan lts_DiferenciaTiempo = DateTime.Today - ldt_FchModifica;

                        int lint_DiferenciaDias = lts_DiferenciaTiempo.Days; //int.Parse(txtbox_cantdias.Text);//lts_DiferenciaTiempo.Days;
                        double lint_Porcentaje = 0;

                        #region Porcentajes Previsiones
                        if (lint_DiferenciaDias >= 1440)
                        {
                            lint_Porcentaje = 1;
                        }
                        else if (lint_DiferenciaDias >= 1260)
                        {
                            lint_Porcentaje = 0.875;
                        }
                        else if (lint_DiferenciaDias >= 1080)
                        {
                            lint_Porcentaje = 0.75;
                        }
                        else if (lint_DiferenciaDias >= 900)
                        {
                            lint_Porcentaje = 0.675;
                        }
                        else if (lint_DiferenciaDias >= 720)
                        {
                            lint_Porcentaje = 0.5;
                        }
                        else if (lint_DiferenciaDias >= 540)
                        {
                            lint_Porcentaje = 0.375;
                        }
                        else if (lint_DiferenciaDias >= 360)
                        {
                            lint_Porcentaje = 0.25;
                        }
                        else if (lint_DiferenciaDias >= 180)
                        {
                            lint_Porcentaje = 0.1;
                        }
                        else if (lint_DiferenciaDias >= 45)
                        {
                            lint_Porcentaje = 0.05;
                        }
                        else if (lint_DiferenciaDias >= 30)
                        {
                            lint_Porcentaje = 0.03;
                        }
                        #endregion

                        if (lstr_TipoExpediente.Contains("Actor") && (lint_DiferenciaDias >= 30) && //lstr_IdSociedad.Contains(gstr_IdSociedadGL) &&
                            (lstr_EstadoResolucion.Contains("En Firme") || lstr_EstadoResolucion.Contains("Liquidacion")))
                        {
                            #region Actor RF Liq
                            lint_CantidadLineasAsiento = 8;

                            if (lstr_EstadoResolucion.Contains("En Firme"))
                            {
                                ldec_MontoPrincipalAnterior = (ldec_MontoPrincipalCierre * Convert.ToDecimal(lint_Porcentaje)) - ldec_MontoPrincipalAnterior;
                                ldec_MontoInteresesAnterior = (ldec_MontoInteresesColonesCierre * Convert.ToDecimal(lint_Porcentaje)) - ldec_MontoInteresesAnterior;

                                if (ldec_MontoPrincipalAnterior < 0)
                                {
                                    ldec_MontoPrincipalAnterior = ldec_MontoPrincipalAnterior * -1;
                                    larrdec_MontosAbajo[0] = ldec_MontoPrincipalAnterior;
                                }
                                else
                                    larrdec_MontosArriba[0] = ldec_MontoPrincipalAnterior;

                                if (ldec_MontoInteresesAnterior < 0)
                                {
                                    ldec_MontoInteresesAnterior = ldec_MontoInteresesAnterior * -1;
                                    larrdec_MontosAbajo[1] = ldec_MontoInteresesAnterior;
                                }
                                else
                                    larrdec_MontosArriba[1] = ldec_MontoInteresesAnterior;

                            }
                            else
                            {
                                ldec_InteresesAnterior = ((ldec_Intereses * ldec_TipoCambioCierre) * Convert.ToDecimal(lint_Porcentaje)) - ldec_InteresesAnterior;
                                ldec_InteresesMoratoriosAnterior = ((ldec_InteresesMoratorios * ldec_TipoCambioCierre) * Convert.ToDecimal(lint_Porcentaje)) - ldec_InteresesMoratoriosAnterior;
                                ldec_CostasAnterior = ((ldec_Costas * ldec_TipoCambioCierre) * Convert.ToDecimal(lint_Porcentaje)) - ldec_CostasAnterior;
                                ldec_DanoMoralAnterior = ((ldec_DanoMoral * ldec_TipoCambioCierre) * Convert.ToDecimal(lint_Porcentaje)) - ldec_DanoMoralAnterior;

                                if (ldec_InteresesAnterior < 0)
                                {
                                    ldec_InteresesAnterior = ldec_InteresesAnterior * -1;
                                    larrdec_MontosAbajo[1] = ldec_InteresesAnterior;
                                }
                                else
                                    larrdec_MontosArriba[1] = ldec_InteresesAnterior;

                                if (ldec_InteresesMoratoriosAnterior < 0)
                                {
                                    ldec_InteresesMoratoriosAnterior = ldec_InteresesMoratoriosAnterior * -1;
                                    larrdec_MontosAbajo[2] = ldec_InteresesMoratoriosAnterior;
                                }
                                else
                                    larrdec_MontosArriba[2] = ldec_InteresesMoratoriosAnterior;

                                if (ldec_CostasAnterior < 0)
                                {
                                    ldec_CostasAnterior = ldec_CostasAnterior * -1;
                                    larrdec_MontosAbajo[3] = ldec_CostasAnterior;
                                }
                                else
                                    larrdec_MontosArriba[3] = ldec_CostasAnterior;

                                if (ldec_DanoMoralAnterior < 0)
                                {
                                    ldec_DanoMoralAnterior = ldec_DanoMoralAnterior * -1;
                                    larrdec_MontosAbajo[4] = ldec_DanoMoralAnterior;
                                }
                                else
                                    larrdec_MontosArriba[4] = ldec_DanoMoralAnterior;

                            }



                            Int32 lint_IdExp = 0;

                            String lstr_query = "SELECT IdExp FROM co.Expedientes exp " +
                                "WHERE exp.IdExpediente ='" + lstr_IdExpediente + "' " +
                                "AND exp.IdSociedadGL ='" + lstr_IdSociedad + "' " +
                                "AND exp.EstadoExpediente = 'Activo'";
                            //"AND cp.EstadoTransaccion != 'estadotran'";

                            DataTable dt_Resoluciones = GetData(lstr_query);

                            foreach (DataRow dr_Resolucion in dt_Resoluciones.Rows)
                            {
                                lint_IdExp = Convert.ToInt32(dr_Resolucion["IdExp"]);
                            }

                            if (larrdec_MontosAbajo != null)
                            {
                                for (int i = 0; i < larrdec_MontosAbajo.Count(); i++)
                                {
                                    if (larrdec_MontosAbajo[i] != null)
                                        larrdec_MontosAbajo[i] = Math.Round(larrdec_MontosAbajo[i], 2);
                                }
                            }

                            String CT = ((gint_Periodo < DateTime.Today.Year)) ? "CT35" : "CT34"; //|| gbool_CambioAno) ? "CT35" : "CT34";
                            lstr_Resultado = EnviarAsientos2(lstr_IdExpediente, lstr_IdSociedad, lstr_AsientosResultado, lstr_IdModulo, CT, lstr_Transaccion, lstr_Leyenda, lbool_cambioMonto, larrdec_MontosAbajo, lint_CantidadLineasAsiento, ldec_DifMontoPrincipal, ldec_DifMontoIntereses, out lstr_CodAsiento);

                            if (lstr_Resultado.Contains("Contabilizado"))
                            {
                                //ws_SGService.uwsRegistrarCobrosPagos(CT,
                                reg_CP.RegistrarCobrosPagos(CT,
                                lstr_IdExpediente, lint_IdExp, "REV", 0, 0,
                                0, 0,
                                larrdec_MontosAbajo == null ? 0 : larrdec_MontosAbajo[0],
                                larrdec_MontosAbajo == null ? 0 : larrdec_MontosAbajo[1],
                                larrdec_MontosAbajo == null ? 0 : larrdec_MontosAbajo[2],
                                larrdec_MontosAbajo == null ? 0 : larrdec_MontosAbajo[3],
                                larrdec_MontosAbajo == null ? 0 : larrdec_MontosAbajo[4],
                                0, 0, 0, 0, 0, 0, 0,
                                0, 0, 0,//Intereses
                                0, 0, 0,
                                0, 0, 0,
                                0, 0, 0,
                                0, 0, 0, 0,//Anteriores

                                "tipotra", "estadotran", DateTime.Today, "Reversion", null);
                            }

                            if (larrdec_MontosArriba != null)
                            {
                                for (int i = 0; i < larrdec_MontosArriba.Count(); i++)
                                {
                                    if (larrdec_MontosArriba[i] != null)
                                        larrdec_MontosArriba[i] = Math.Round(larrdec_MontosArriba[i], 2);
                                }
                            }

                            lstr_Resultado = EnviarAsientos2(lstr_IdExpediente, lstr_IdSociedad, lstr_AsientosResultado, lstr_IdModulo, "CT13", lstr_Transaccion, lstr_Leyenda, lbool_cambioMonto, larrdec_MontosArriba, lint_CantidadLineasAsiento, ldec_DifMontoPrincipal, ldec_DifMontoIntereses, out lstr_CodAsiento);

                            if (lstr_Resultado.Contains("Contabilizado"))
                            {
                                //ws_SGService.uwsRegistrarCobrosPagos("CT13",
                                reg_CP.RegistrarCobrosPagos("CT13",
                                lstr_IdExpediente, lint_IdExp, "REV", 0, 0,
                                0, 0,
                                larrdec_MontosArriba == null ? 0 : larrdec_MontosArriba[0],
                                larrdec_MontosArriba == null ? 0 : larrdec_MontosArriba[1],
                                larrdec_MontosArriba == null ? 0 : larrdec_MontosArriba[2],
                                larrdec_MontosArriba == null ? 0 : larrdec_MontosArriba[3],
                                larrdec_MontosArriba == null ? 0 : larrdec_MontosArriba[4],
                                0, 0, 0, 0, 0, 0, 0,
                                0, 0, 0,//Intereses
                                0, 0, 0,
                                0, 0, 0,
                                0, 0, 0,
                                0, 0, 0, 0,//Anteriores

                                "tipotra", "estadotran", DateTime.Today, "Reversion", null);
                            }

                            #endregion

                            Int32 lint_IdRes = ConsultarIdRes(lstr_IdExpediente, lstr_IdSociedad, lstr_EstadoResolucion);

                            ldec_MontoPrincipalAnterior = Math.Round((ldec_MontoPrincipalCierre * Convert.ToDecimal(lint_Porcentaje)), 2);
                            ldec_MontoInteresesAnterior = Math.Round((ldec_MontoInteresesColonesCierre * Convert.ToDecimal(lint_Porcentaje)), 2);
                            ldec_InteresesAnterior = Math.Round(((ldec_Intereses * ldec_TipoCambioCierre) * Convert.ToDecimal(lint_Porcentaje)), 2);
                            ldec_InteresesMoratoriosAnterior = Math.Round(((ldec_InteresesMoratorios * ldec_TipoCambioCierre) * Convert.ToDecimal(lint_Porcentaje)), 2);
                            ldec_CostasAnterior = Math.Round(((ldec_Costas * ldec_TipoCambioCierre) * Convert.ToDecimal(lint_Porcentaje)), 2);
                            ldec_DanoMoralAnterior = Math.Round(((ldec_DanoMoral * ldec_TipoCambioCierre) * Convert.ToDecimal(lint_Porcentaje)), 2);

                            try
                            {
                                #region Registro
                                lstr_ResultadoRegistro = //ws_SGService.uwsModificarCobrosPagos(
                                    reg_clsCobrosPagos.ModificarCobrosPagos(
                                    lstr_IdExpediente, lstr_IdSociedad, lint_IdRes, lstr_Moneda, ldec_TipoCambioActual,
                                    ldec_Tbp, ldec_Tiempo, ldec_TipoCambioCierre,//cambio de posicion 
                                    ldec_MontoPrincipal, ldec_MontoPrincipalColones, ldec_MontoPrincipalCierre,
                                    ldec_MontoIntereses, ldec_MontoInteresesColones, ldec_MontoInteresesColonesCierre,
                                    0, 0, 0,
                                    0, 0, 0,
                                    ldec_Intereses, ldec_InteresesColones, ldec_InteresesColonesCierre,
                                    ldec_InteresesMoratorios, ldec_InteresesMoratoriosColones, ldec_InteresesMoratoriosColonesCierre,
                                    ldec_Costas, ldec_CostasColones, ldec_CostasColonesCierre,
                                    ldec_DanoMoral, ldec_DanoMoralColones, ldec_DanoMoralColonesCierre,
                                    ldec_MontoPrincipalAnterior, ldec_MontoInteresesAnterior,
                                    ldec_InteresesAnterior, ldec_CostasAnterior,
                                    ldec_InteresesMoratoriosAnterior, ldec_DanoMoralAnterior,
                                    "d", "g");
                                guardarPrevision(lint_IdRes, lstr_IdExpediente, lint_DiferenciaDias, (float)(ldec_MontoPrincipalAnterior +
                                    ldec_MontoInteresesAnterior + ldec_InteresesAnterior + ldec_InteresesMoratoriosAnterior +
                                    ldec_CostasAnterior + ldec_DanoMoralAnterior), (float)(ldec_MontoPrincipalCierre + ldec_MontoInteresesColonesCierre
                                    + (ldec_Intereses * ldec_TipoCambioCierre) + (ldec_InteresesMoratorios * ldec_TipoCambioCierre) +
                                    (ldec_Costas * ldec_TipoCambioCierre) + (ldec_DanoMoral * ldec_TipoCambioCierre))
                                    , (float)(lint_Porcentaje * 100), gstr_Usuario);


                                if (lstr_ResultadoRegistro[0].Contains("00"))
                                {
                                    lstr_Resultado[0] = "exito";
                                    str_Mensaje += "Expediente " + lstr_IdExpediente + " modificado con éxito." + Environment.NewLine;
                                }
                                else
                                {
                                    lstr_Resultado[0] = "fallo";
                                    str_Mensaje += "Fallo al modificar expediente " + lstr_IdExpediente + ". \n" + lstr_ResultadoRegistro[1] + Environment.NewLine;
                                }
                                #endregion
                            }
                            catch { }
                            str_Mensaje += "------------------------------------------------" + Environment.NewLine;
                        }

                        #endregion
                    }
                }

                str_Mensaje += "Fin de Proceso\n";
                str_Mensaje += "------------------------------------------------" + Environment.NewLine;

                GuardarResultadoContabilizacion();

            }
        }

        private static void guardarPrevision(Decimal IdRes, String IdExp, int Dias, float Monto, float total, float porcentaje, String usuario)
        {
            //string[] str_resul = //ws_SGService.uwsCrearAntiguedadDeSaldos
                
            string[] lstr_result = new string[3];
            Boolean res = false;
            int? int_TmpIdAntiguedadSaldos = null;
            try
            {
                res = reg_Antiguedad.CrearAntiguedadDeSaldos(null, null, IdRes.ToString(), IdExp, null, Dias, null, Convert.ToDecimal(Monto), Convert.ToDecimal(total), Convert.ToDecimal(porcentaje), null, usuario,
                 out lstr_result[0], out lstr_result[1], out int_TmpIdAntiguedadSaldos);

                lstr_result[2] = int_TmpIdAntiguedadSaldos.ToString();
            }
            catch (Exception err)
            {
                lstr_result[0] = "99";
                lstr_result[1] = "Error en WS: " + err.Message;

            }
            /* String query = "SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;" +
                             "BEGIN TRANSACTION;" +

                             "UPDATE [co].[AntiguedadDeSaldos]" +
                                "SET [DiasDeCuenta] = " + Dias +
                                   ",[MontoIncobrable] = " + Monto +
                                   ",[DiferenciaAjustar] = " + total +
                                   ",[PorcentajeIncobrable] = " + porcentaje +
                                   ",[UsrModificacion] = '" + usuario + "'" +
                                   ",[FchModificacion] = getdate()" +
                              " WHERE [IdResolucion] = '" + IdRes + "' and [IdExpediente] = '" + IdExp + "'" +

                             "IF @@ROWCOUNT = 0" +
                             "BEGIN " +
                               "INSERT INTO [co].[AntiguedadDeSaldos] " +
                                        "([IdResolucion]" +
                                        ",[IdExpediente]" +
                                        ",[DiasDeCuenta]" +
                                        ",[MontoIncobrable]" +
                                        ",[DiferenciaAjustar]" +
                                        ",[PorcentajeIncobrable]" +
                                        ",[UsrCreacion]" +
                                        ",[FchCreacion]" +
                                        ",[UsrModificacion]" +
                                        ",[FchModificacion])" +
                                  "VALUES" +
                                        "('" + IdRes + "','" + IdExp + "'," + Dias + "," + Monto + "," + total + "," + porcentaje + ",'" + usuario + "',getdate(),'" + usuario + "',getdate())" +
                             "END " +
                             "COMMIT TRANSACTION;";
            
             string lstr_ConnString = ConfigurationManager.ConnectionStrings["GestNICSPDEVConnectionString"].ConnectionString;
             using (SqlConnection con = new SqlConnection(lstr_ConnString))
             {
                 using (SqlCommand cmd = new SqlCommand())
                 {
                     cmd.CommandText = query;
                     using (SqlDataAdapter sda = new SqlDataAdapter())
                     {
                         cmd.Connection = con;
                         cmd.Connection.Open();
                         cmd.ExecuteNonQuery();
                        
                     }
                     //cmd.ExecuteNonQuery(); 
                 }
             }
              */
        }
        
        private static String[] EnviarAsientos2(String str_IdExpediente, String str_Sociedad, String str_AsientosResultado,
            String str_IdModulo, String str_IdOperacion,
            String str_Trasaccion, String str_Leyenda, Boolean lbool_cambio, Decimal[] arrdec_Montos,
            Int32 int_CantidadLineasAsiento,
            Decimal MontoPricipalColones, Decimal MontoInteresColones, out String CodAsiento)
        {
            #region Variables
            //Presentacion.wsAsientos.ZfiAsiento item_asiento = new Presentacion.wsAsientos.ZfiAsiento();
            //Presentacion.wsAsientos.ZfiAsiento item_asiento2 = new Presentacion.wsAsientos.ZfiAsiento();
            //Presentacion.wsAsientos.ZfiAsiento[] tabla_asientos = new Presentacion.wsAsientos.ZfiAsiento[int_CantidadLineasAsiento];

            LogicaNegocio.wrSigafAsientos.ZfiAsiento item_asiento = new LogicaNegocio.wrSigafAsientos.ZfiAsiento();
            LogicaNegocio.wrSigafAsientos.ZfiAsiento item_asiento2 = new LogicaNegocio.wrSigafAsientos.ZfiAsiento();
            LogicaNegocio.wrSigafAsientos.ZfiAsiento[] tabla_asientos = new LogicaNegocio.wrSigafAsientos.ZfiAsiento[int_CantidadLineasAsiento];

            String[] item_resAsientosLog = new String[10];
            String lstr_logAsiento = String.Empty;
            String[] lstr_Resultado = new String[3] { "", "", "" };
            String lstr_Montos = String.Empty;

            String lstr_TipoProcesoTexto = String.Empty;
            String lstr_TipoProceso_CodAux2 = String.Empty;

            String lstr_idTira_CodAux3 = String.Empty;
            String lstr_clsDocumento_CodAux4 = String.Empty;

            String lstr_ClaveContable = String.Empty;
            String lstr_ClaveContable2 = String.Empty;

            Int32 lint_cantLineasAsiento = 0;
            Int32 lint_Contador = 0;
            Int32 lint_cantTiras = 0;
            Int32 lint_contMonto = 0;

            Boolean bool_diferencial = false;

            DateTime ldt_FechaContabilizacion = DateTime.Now;

            Decimal ldec_Monto = 0;
            String str_Usuario = "0000001";

            #endregion
            CodAsiento = "";

            Boolean lbool_continuar = false;
            for (int j = 0; j < arrdec_Montos.Count(); j++)
            {
                if (arrdec_Montos[j] > 0)
                {
                    lbool_continuar = true;
                }
            }

            if (arrdec_Montos != null)
            {
                for (int i = 0; i < arrdec_Montos.Count(); i++)
                {
                    if (arrdec_Montos[i] != null)
                        arrdec_Montos[i] = Math.Round(arrdec_Montos[i], 2);
                }
            }

            if (str_IdOperacion.Contains("CT28") || str_IdOperacion.Contains("CT29"))
            {
                bool_diferencial = true;
            }

            if (lbool_continuar)
            {
                //Tipo de proceso
                lstr_TipoProcesoTexto = ConsultarTipoProcesoExpediente(str_IdExpediente);
                lstr_TipoProceso_CodAux2 = ConsultarOpcionesCatalogos(lstr_TipoProcesoTexto);
                lstr_clsDocumento_CodAux4 = ConsultarClaseDocumento(str_IdModulo, str_IdOperacion);

                //Obtenemos tira de asientos configuradas en el gestor
                DataSet lds_TirasAsientos = ConsultarTiposAsientos2(str_Sociedad, str_IdModulo, str_IdOperacion, lstr_TipoProceso_CodAux2);
                DataTable ldt_TirasAsiento = null;

                lint_cantTiras = lds_TirasAsientos.Tables[0].Rows.Count;

                if (lint_cantTiras > 0)
                {
                    ldt_TirasAsiento = lds_TirasAsientos.Tables[0];

                    //Sacar datos de tiras asientos
                    if ((lint_cantTiras == 2) && !lbool_cambio && !bool_diferencial)
                    {
                        Int32 lint_cont = 0;

                        #region caso simple
                        foreach (DataRow ldr_TiraAsiento in ldt_TirasAsiento.Rows)
                        {
                            //Segun monto a enviar a SIGAF para contabilizar asiento de provision 
                            lstr_idTira_CodAux3 = ldr_TiraAsiento["CodigoAuxiliar3"].ToString();
                            switch (lstr_idTira_CodAux3.Trim())
                            {
                                case "1"://Monto Principal
                                    if (MontoPricipalColones != 0)
                                    {
                                        ////Llenamos los asientos
                                        item_asiento = new LogicaNegocio.wrSigafAsientos.ZfiAsiento();//wsAsientos.ZfiAsiento();
                                        String lstr_info = str_IdOperacion + " : " + str_IdExpediente;
                                        if (lstr_info.Length > 15)
                                            lstr_info = lstr_info.Substring(0, 15);
                                        item_asiento.Xblnr = lstr_info;//REF
                                        item_asiento.Bktxt = "Texto_Cabecera";
                                        item_asiento.Blart = ldr_TiraAsiento["CodigoAuxiliar4"].ToString().Trim();//Clase de documento
                                        item_asiento.Bukrs = ldr_TiraAsiento["Codigo"].ToString().Trim();//Sociedad
                                        item_asiento.Bldat = ldt_FechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de documento
                                        item_asiento.Budat = ldt_FechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de contabilización
                                        item_asiento.Xref1Hd = str_IdExpediente;//numero expediente 
                                        item_asiento.Xref2Hd = str_IdOperacion + "-" + lstr_TipoProceso_CodAux2;//CT10-AG operacion+codigoprocesal expediente


                                        item_asiento.Waers = ldr_TiraAsiento["CodigoAuxiliar"].ToString().Trim();//Moneda 
                                        item_asiento.Bschl = ldr_TiraAsiento["IdClaveContable"].ToString().Trim();//Clave de contabilización
                                        item_asiento.Hkont = ldr_TiraAsiento["IdCuentaContable"].ToString().Trim();//Cuenta de mayor
                                        item_asiento.Wrbtr = MontoPricipalColones;//Importe o monto en colones a contabilizar 

                                        lstr_Montos = lstr_Montos + lint_cont++ + ". 40: " + MontoPricipalColones + "\n";

                                        item_asiento.Zuonr = "Asig_1";
                                        item_asiento.Sgtxt = "SG-Liquidacion";
                                        item_asiento.Projk = ldr_TiraAsiento["IdElementoPEP"].ToString().TrimEnd();
                                        item_asiento.Fipex = ldr_TiraAsiento["IdPosPre"].ToString().TrimEnd();//Posición presupuestaria
                                        item_asiento.Kostl = ldr_TiraAsiento["IdCentroCosto"].ToString();
                                        item_asiento.Fistl = ldr_TiraAsiento["IdCentroGestor"].ToString();
                                        item_asiento.Prctr = ldr_TiraAsiento["IdCentroBeneficio"].ToString();
                                        item_asiento.Measure = ldr_TiraAsiento["IdPrograma"].ToString().TrimEnd();//Programa presupuestario
                                        item_asiento.Geber = ldr_TiraAsiento["IdFondo"].ToString().Trim();//Fondo
                                        item_asiento.Fkber = "";
                                        item_asiento.Xref2 = "";
                                        tabla_asientos[0] = item_asiento;
                                        ///***************************************************Cargar cuenta 50 DEBE*****************************************************/
                                        item_asiento2 = new LogicaNegocio.wrSigafAsientos.ZfiAsiento();//wsAsientos.ZfiAsiento();
                                        item_asiento2.Waers = ldr_TiraAsiento["CodigoAuxiliar"].ToString().Trim();//Moneda 
                                        item_asiento2.Bschl = ldr_TiraAsiento["IdClaveContable2"].ToString().Trim();//Clave de contabilización
                                        item_asiento2.Hkont = ldr_TiraAsiento["IdCuentaContable2"].ToString().Trim();//Cuenta de mayor
                                        item_asiento2.Wrbtr = MontoPricipalColones;//Importe o monto en colones a contabilizar
                                        lstr_Montos = lstr_Montos + lint_cont++ + ". 50: " + MontoPricipalColones + "\n";

                                        item_asiento2.Zuonr = "";
                                        item_asiento2.Sgtxt = "SG-Provision diario";
                                        item_asiento2.Fipex = ldr_TiraAsiento["IdPosPre2"].ToString().TrimEnd();//Posición presupuestaria
                                        item_asiento2.Kostl = ldr_TiraAsiento["IdCentroCosto2"].ToString();
                                        item_asiento2.Fistl = ldr_TiraAsiento["IdCentroGestor2"].ToString();
                                        item_asiento2.Prctr = ldr_TiraAsiento["IdCentroBeneficio2"].ToString();
                                        item_asiento2.Geber = ldr_TiraAsiento["IdFondo2"].ToString().Trim();//Fondo
                                        item_asiento2.Fkber = "";
                                        item_asiento2.Xref2 = "xref2";
                                        tabla_asientos[1] = item_asiento2;
                                    }
                                    break;
                                case "2"://Monto Intereses
                                    if (MontoInteresColones != 0)
                                    {
                                        item_asiento = new LogicaNegocio.wrSigafAsientos.ZfiAsiento();//wsAsientos.ZfiAsiento();
                                        ///***************************************************Cargar cuenta 40 HABER*****************************************************/
                                        if (MontoPricipalColones == 0)
                                        {
                                            String lstr_info = str_IdOperacion + " : " + str_IdExpediente;
                                            if (lstr_info.Length > 15)
                                                lstr_info = lstr_info.Substring(0, 15);
                                            item_asiento.Xblnr = lstr_info;//REF
                                            item_asiento.Bktxt = "Texto_Cabecera";
                                            item_asiento.Blart = ldr_TiraAsiento["CodigoAuxiliar4"].ToString().Trim();//Clase de documento
                                            item_asiento.Bukrs = ldr_TiraAsiento["Codigo"].ToString().Trim();//Sociedad
                                            item_asiento.Bldat = ldt_FechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de documento
                                            item_asiento.Budat = ldt_FechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de contabilización
                                            item_asiento.Waers = ldr_TiraAsiento["CodigoAuxiliar"].ToString().Trim();//Moneda 


                                            item_asiento.Xref1Hd = str_IdExpediente;//numero expediente 
                                            item_asiento.Xref2Hd = str_IdOperacion + "-" + lstr_TipoProceso_CodAux2;//CT01-AG operacion+codigoprocesal expediente

                                        }

                                        item_asiento.Bschl = ldr_TiraAsiento["IdClaveContable"].ToString().Trim();//Clave de contabilización
                                        item_asiento.Hkont = ldr_TiraAsiento["IdCuentaContable"].ToString().Trim();//Cuenta de mayor
                                        item_asiento.Wrbtr = MontoInteresColones;//Importe o monto en colones a contabilizar 

                                        lstr_Montos = lstr_Montos + lint_cont++ + ". 40: " + MontoInteresColones + "\n";

                                        item_asiento.Zuonr = "Asig_1";
                                        item_asiento.Sgtxt = "SG-Provision";
                                        item_asiento.Projk = ldr_TiraAsiento["IdElementoPEP"].ToString().TrimEnd();
                                        item_asiento.Fipex = ldr_TiraAsiento["IdPosPre"].ToString().TrimEnd();//Posición presupuestaria
                                        item_asiento.Kostl = ldr_TiraAsiento["IdCentroCosto"].ToString();
                                        item_asiento.Fistl = ldr_TiraAsiento["IdCentroGestor"].ToString();
                                        item_asiento.Prctr = ldr_TiraAsiento["IdCentroBeneficio"].ToString();
                                        item_asiento.Measure = ldr_TiraAsiento["IdPrograma"].ToString().TrimEnd();//Programa presupuestario
                                        item_asiento.Geber = ldr_TiraAsiento["IdFondo"].ToString().Trim();//Fondo
                                        item_asiento.Fkber = "";
                                        item_asiento.Xref2 = "";
                                        if (MontoPricipalColones == 0)
                                        {
                                            tabla_asientos[0] = item_asiento;
                                        }
                                        else
                                            tabla_asientos[2] = item_asiento;
                                        ///***************************************************Cargar cuenta 50 DEBE*****************************************************/
                                        item_asiento2 = new LogicaNegocio.wrSigafAsientos.ZfiAsiento();//wsAsientos.ZfiAsiento();
                                        item_asiento2.Waers = ldr_TiraAsiento["CodigoAuxiliar"].ToString().Trim();//Moneda 
                                        item_asiento2.Bschl = ldr_TiraAsiento["IdClaveContable2"].ToString().Trim();//Clave de contabilización
                                        item_asiento2.Hkont = ldr_TiraAsiento["IdCuentaContable2"].ToString().Trim();//Cuenta de mayor
                                        item_asiento2.Wrbtr = MontoInteresColones;//Importe o monto en colones a contabilizar
                                        lstr_Montos = lstr_Montos + lint_cont++ + ". 50: " + MontoInteresColones + "\n";


                                        item_asiento2.Projk = ldr_TiraAsiento["IdElementoPEP2"].ToString().TrimEnd();
                                        item_asiento2.Fipex = ldr_TiraAsiento["IdPosPre2"].ToString().TrimEnd();//Posición presupuestaria
                                        item_asiento2.Kostl = ldr_TiraAsiento["IdCentroCosto2"].ToString();
                                        item_asiento2.Fistl = ldr_TiraAsiento["IdCentroGestor2"].ToString();
                                        item_asiento2.Prctr = ldr_TiraAsiento["IdCentroBeneficio2"].ToString();
                                        item_asiento2.Measure = ldr_TiraAsiento["IdPrograma2"].ToString().TrimEnd();//Programa presupuestario
                                        item_asiento2.Zuonr = "Asig_2";
                                        item_asiento2.Sgtxt = "SG-Liquidacion";//char 50
                                        item_asiento2.Geber = ldr_TiraAsiento["IdFondo2"].ToString().Trim();//Fondo
                                        item_asiento2.Fkber = "";
                                        item_asiento2.Xref2 = "xref2";
                                        if (MontoPricipalColones == 0)
                                        {
                                            tabla_asientos[1] = item_asiento2;
                                        }
                                        else
                                            tabla_asientos[3] = item_asiento2;
                                    }
                                    break;
                            }
                        }
                        #endregion
                    }
                    else if (lint_cantTiras >= 2)
                    {
                        lint_Contador = 0;
                        Int32 lint_index = 0;

                        #region casos Complicados
                        foreach (DataRow ldr_TiraAsiento in ldt_TirasAsiento.Rows)
                        {
                            lint_index = ldt_TirasAsiento.Rows.IndexOf(ldr_TiraAsiento);

                            lstr_idTira_CodAux3 = ldr_TiraAsiento["CodigoAuxiliar3"].ToString();
                            lstr_ClaveContable = ldr_TiraAsiento["IdClaveContable"].ToString().Trim();
                            lstr_ClaveContable2 = ldr_TiraAsiento["IdClaveContable2"].ToString().Trim();

                            if ((lint_Contador == 0) && (arrdec_Montos[lint_contMonto] == 0))
                            {
                                lint_contMonto++;
                                continue;
                            }
                            if (lint_Contador == int_CantidadLineasAsiento)
                                break;
                            //if (lint_cantTiras == lint_Contador)
                            //    break;
                            else if ((lint_cantTiras == 4) && (MontoPricipalColones != 0) && (MontoInteresColones == 0))
                            {
                                if ((lint_index == 1) || (lint_index == 3))
                                    continue;
                            }
                            else if ((lint_cantTiras == 4) && (MontoPricipalColones == 0) && (MontoInteresColones != 0))
                            {
                                if ((lint_index == 0) || (lint_index == 2))
                                    continue;
                            }
                            else if ((lint_cantTiras == 6) && (MontoPricipalColones != 0) && (MontoInteresColones == 0))
                            {
                                if ((lint_index == 1) || (lint_index == 3) || (lint_index == 5))
                                    continue;
                            }
                            else if ((lint_cantTiras == 6) && (MontoPricipalColones == 0) && (MontoInteresColones != 0))
                            {
                                if ((lint_index == 0) || (lint_index == 2) || (lint_index == 4))
                                    continue;
                            }

                            ldec_Monto = arrdec_Montos[lint_contMonto++];

                            if ((lstr_ClaveContable.Equals("40") && lstr_ClaveContable2.Equals("50")))
                            {
                                item_asiento = new LogicaNegocio.wrSigafAsientos.ZfiAsiento();//wsAsientos.ZfiAsiento();
                                #region cabecera
                                if (lint_Contador == 0)
                                {
                                    item_asiento.Blart = ldr_TiraAsiento["CodigoAuxiliar4"].ToString().Trim();//Clase de documento
                                    item_asiento.Bukrs = ldr_TiraAsiento["Codigo"].ToString().Trim();//Sociedad
                                    item_asiento.Bldat = ldt_FechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de documento
                                    item_asiento.Budat = ldt_FechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de contabilización
                                    String lstr_info = str_IdOperacion + " : " + str_IdExpediente;
                                    if (lstr_info.Length > 15)
                                        lstr_info = lstr_info.Substring(0, 15);
                                    item_asiento.Xblnr = lstr_info;//REF
                                    item_asiento.Bktxt = "Texto_Cabecera";
                                    item_asiento.Xref1Hd = str_IdExpediente;//numero expediente 
                                    item_asiento.Xref2Hd = str_IdOperacion + "-" + lstr_TipoProceso_CodAux2;//CT01-AG operacion+codigoprocesal expediente
                                }
                                #endregion

                                #region debe 40
                                item_asiento.Wrbtr = ldec_Monto;//Importe o monto en colones a contabilizar 
                                lstr_Montos = lstr_Montos + (lint_Contador + 1) + ". 40: " + ldec_Monto + "\n";
                                item_asiento.Sgtxt = "SG-Liquidacion";
                                item_asiento.Zuonr = "Asig_1";
                                item_asiento.Fkber = "";
                                item_asiento.Xref2 = "";
                                item_asiento.Waers = ldr_TiraAsiento["CodigoAuxiliar"].ToString().Trim();//Moneda 
                                item_asiento.Bschl = ldr_TiraAsiento["IdClaveContable"].ToString().Trim();//Clave de contabilización
                                item_asiento.Hkont = ldr_TiraAsiento["IdCuentaContable"].ToString().Trim();//Cuenta de mayor
                                item_asiento.Kostl = ldr_TiraAsiento["IdCentroCosto"].ToString();
                                item_asiento.Fistl = ldr_TiraAsiento["IdCentroGestor"].ToString();
                                item_asiento.Prctr = ldr_TiraAsiento["IdCentroBeneficio"].ToString();
                                item_asiento.Geber = ldr_TiraAsiento["IdFondo"].ToString().Trim();//Fondo
                                item_asiento.Projk = ldr_TiraAsiento["IdElementoPEP"].ToString().TrimEnd();
                                item_asiento.Fipex = ldr_TiraAsiento["IdPosPre"].ToString().TrimEnd();//Posición presupuestaria
                                item_asiento.Measure = ldr_TiraAsiento["IdPrograma"].ToString().TrimEnd();
                                if (lint_Contador == 0)
                                {
                                    tabla_asientos[lint_Contador] = item_asiento;
                                    lint_Contador++;
                                }
                                else
                                    tabla_asientos[lint_Contador++] = item_asiento;

                                #endregion

                                item_asiento2 = new LogicaNegocio.wrSigafAsientos.ZfiAsiento();//wsAsientos.ZfiAsiento();
                                #region 50 haber
                                //if (lint_cantTiras == lint_Contador)
                                //    break;
                                if (str_IdOperacion.Contains("CT09") && (lint_Contador == 3))
                                    break;
                                if ((lbool_cambio) && (lint_Contador < 2))
                                {
                                    ldec_Monto = arrdec_Montos[lint_contMonto++];
                                }
                                item_asiento2.Wrbtr = ldec_Monto;//Importe o monto en colones a contabilizar
                                lstr_Montos = lstr_Montos + (lint_Contador + 1) + ". 50: " + ldec_Monto + "\n";
                                item_asiento2.Sgtxt = "SG-Provision diario";
                                item_asiento2.Zuonr = "";
                                item_asiento2.Fkber = "";
                                item_asiento2.Xref2 = "xref2";
                                item_asiento2.Waers = ldr_TiraAsiento["CodigoAuxiliar"].ToString().Trim();//Moneda 
                                item_asiento2.Bschl = ldr_TiraAsiento["IdClaveContable2"].ToString().Trim();//Clave de contabilización
                                item_asiento2.Hkont = ldr_TiraAsiento["IdCuentaContable2"].ToString().Trim();//Cuenta de mayor
                                item_asiento2.Kostl = ldr_TiraAsiento["IdCentroCosto2"].ToString();
                                item_asiento2.Fistl = ldr_TiraAsiento["IdCentroGestor2"].ToString();
                                item_asiento2.Prctr = ldr_TiraAsiento["IdCentroBeneficio2"].ToString();
                                item_asiento2.Geber = ldr_TiraAsiento["IdFondo2"].ToString().Trim();//Fondo
                                item_asiento2.Projk = ldr_TiraAsiento["IdElementoPEP2"].ToString().TrimEnd();
                                item_asiento2.Fipex = ldr_TiraAsiento["IdPosPre2"].ToString().TrimEnd();//Posición presupuestaria
                                item_asiento2.Measure = ldr_TiraAsiento["IdPrograma2"].ToString().TrimEnd();
                                tabla_asientos[lint_Contador++] = item_asiento2;
                                #endregion
                            }
                            else if (lstr_ClaveContable.Equals("40"))
                            {
                                item_asiento = new LogicaNegocio.wrSigafAsientos.ZfiAsiento();//wsAsientos.ZfiAsiento();
                                #region cabecera
                                if (lint_Contador == 0)
                                {
                                    item_asiento.Blart = ldr_TiraAsiento["CodigoAuxiliar4"].ToString().Trim();//Clase de documento
                                    item_asiento.Bukrs = ldr_TiraAsiento["Codigo"].ToString().Trim();//Sociedad
                                    item_asiento.Bldat = ldt_FechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de documento
                                    item_asiento.Budat = ldt_FechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de contabilización
                                    String lstr_info = str_IdOperacion + " : " + str_IdExpediente;
                                    if (lstr_info.Length > 15)
                                        lstr_info = lstr_info.Substring(0, 15);
                                    item_asiento.Xblnr = lstr_info;//REF
                                    item_asiento.Bktxt = "Texto_Cabecera";
                                    item_asiento.Xref1Hd = str_IdExpediente;//numero expediente 
                                    item_asiento.Xref2Hd = str_IdOperacion + "-" + lstr_TipoProceso_CodAux2;//CT01-AG operacion+codigoprocesal expediente
                                }
                                #endregion

                                #region debe 40
                                item_asiento.Wrbtr = ldec_Monto;//Importe o monto en colones a contabilizar 
                                lstr_Montos = lstr_Montos + (lint_Contador + 1) + ". 40: " + ldec_Monto + "\n";
                                item_asiento.Sgtxt = "SG-Liquidacion";
                                item_asiento.Zuonr = "Asig_1";
                                item_asiento.Fkber = "";
                                item_asiento.Xref2 = "";
                                item_asiento.Waers = ldr_TiraAsiento["CodigoAuxiliar"].ToString().Trim();//Moneda 
                                item_asiento.Bschl = ldr_TiraAsiento["IdClaveContable"].ToString().Trim();//Clave de contabilización
                                item_asiento.Hkont = ldr_TiraAsiento["IdCuentaContable"].ToString().Trim();//Cuenta de mayor
                                item_asiento.Kostl = ldr_TiraAsiento["IdCentroCosto"].ToString();
                                item_asiento.Fistl = ldr_TiraAsiento["IdCentroGestor"].ToString();
                                item_asiento.Prctr = ldr_TiraAsiento["IdCentroBeneficio"].ToString();
                                item_asiento.Geber = ldr_TiraAsiento["IdFondo"].ToString().Trim();//Fondo
                                item_asiento.Projk = ldr_TiraAsiento["IdElementoPEP"].ToString().TrimEnd();
                                item_asiento.Fipex = ldr_TiraAsiento["IdPosPre"].ToString().TrimEnd();//Posición presupuestaria
                                item_asiento.Measure = ldr_TiraAsiento["IdPrograma"].ToString().TrimEnd();
                                if (lint_Contador == 0)
                                {
                                    tabla_asientos[lint_Contador] = item_asiento;
                                    lint_Contador++;
                                }
                                else
                                    tabla_asientos[lint_Contador++] = item_asiento;
                                #endregion

                            }
                            else if (lstr_ClaveContable.Equals("50"))
                            {
                                item_asiento = new LogicaNegocio.wrSigafAsientos.ZfiAsiento();//wsAsientos.ZfiAsiento();
                                #region cabecera
                                if (lint_Contador == 0)
                                {
                                    item_asiento.Blart = ldr_TiraAsiento["CodigoAuxiliar4"].ToString().Trim();//Clase de documento
                                    item_asiento.Bukrs = ldr_TiraAsiento["Codigo"].ToString().Trim();//Sociedad
                                    item_asiento.Bldat = ldt_FechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de documento
                                    item_asiento.Budat = ldt_FechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de contabilización
                                    String lstr_info = str_IdOperacion + " : " + str_IdExpediente;
                                    if (lstr_info.Length > 15)
                                        lstr_info = lstr_info.Substring(0, 15);
                                    item_asiento.Xblnr = lstr_info;//REF
                                    item_asiento.Bktxt = "Texto_Cabecera";
                                    item_asiento.Xref1Hd = str_IdExpediente;//numero expediente 
                                    item_asiento.Xref2Hd = str_IdOperacion + "-" + lstr_TipoProceso_CodAux2;//CT01-AG operacion+codigoprocesal expediente
                                }
                                #endregion

                                #region haber 50
                                item_asiento.Wrbtr = ldec_Monto;//Importe o monto en colones a contabilizar 
                                lstr_Montos = lstr_Montos + (lint_Contador + 1) + ". 50: " + ldec_Monto + "\n";
                                item_asiento.Sgtxt = "SG-Liquidacion";
                                item_asiento.Zuonr = "Asig_1";
                                item_asiento.Fkber = "";
                                item_asiento.Xref2 = "";
                                item_asiento.Waers = ldr_TiraAsiento["CodigoAuxiliar"].ToString().Trim();//Moneda 
                                item_asiento.Bschl = ldr_TiraAsiento["IdClaveContable"].ToString().Trim();//Clave de contabilización
                                item_asiento.Hkont = ldr_TiraAsiento["IdCuentaContable"].ToString().Trim();//Cuenta de mayor
                                item_asiento.Kostl = ldr_TiraAsiento["IdCentroCosto"].ToString();
                                item_asiento.Fistl = ldr_TiraAsiento["IdCentroGestor"].ToString();
                                item_asiento.Prctr = ldr_TiraAsiento["IdCentroBeneficio"].ToString();
                                item_asiento.Geber = ldr_TiraAsiento["IdFondo"].ToString().Trim();//Fondo
                                item_asiento.Projk = ldr_TiraAsiento["IdElementoPEP"].ToString().TrimEnd();
                                item_asiento.Fipex = ldr_TiraAsiento["IdPosPre"].ToString().TrimEnd();//Posición presupuestaria
                                item_asiento.Measure = ldr_TiraAsiento["IdPrograma"].ToString().TrimEnd();
                                if (lint_Contador == 0)
                                {
                                    tabla_asientos[lint_Contador] = item_asiento;
                                    lint_Contador++;
                                }
                                else
                                    tabla_asientos[lint_Contador++] = item_asiento;
                                #endregion

                            }
                        }
                        #endregion
                    }
                    //Cargar de Asientos 
                    string[] concatenado = new string[8];
                    //envio de asiento mediante servicio web hacia SIGAF
                    try
                    {
                        item_resAsientosLog = //ws_ContabilizaAsientos.EnviarAsientos(tabla_asientos);
                            reg_TiposAsiento.EnviarAsientos(tabla_asientos);
                        Int32 lint_Length = 0;
                        for (int j = 0; j < item_resAsientosLog.Count(); j++)
                        {
                            if (item_resAsientosLog[j].Contains("[E]"))
                                lstr_Resultado[0] = "error";
                            else if (item_resAsientosLog[j].Contains("[S]"))
                            {
                                lint_Length = item_resAsientosLog[j].Length;
                                try
                                {
                                    str_AsientosResultado = str_AsientosResultado + "\n" + item_resAsientosLog[j].ToString().Substring(58, 10);
                                }
                                catch { }
                                lstr_Resultado[0] = "Contabilizado";
                                lstr_Resultado[2] = str_AsientosResultado;

                                try
                                {
                                    //ws_SGService.uwsRegistrarBitacoraMovimientosCuentasExpedientes
                                    reg_Bita.RegistrarBitacoraDeMovimientosCuentasExpedientes    (str_IdExpediente, "CT", str_Sociedad, str_IdOperacion, "", MontoPricipalColones, MontoInteresColones, 0, 0, "Provisión Monto Principal Colones- ", str_Usuario);
                                    //ws_SGService.uwsRegistrarBitacoraMovimientosCuentasExpedientes
                                    reg_Bita.RegistrarBitacoraDeMovimientosCuentasExpedientes    (str_IdExpediente, "CT", str_Sociedad, str_IdOperacion, "", MontoPricipalColones, MontoInteresColones, 0, 0, "Provisión Monto Interes Colones - ", str_Usuario);

                                    //ws_SG.uwsRegistrarCobrosPagos(gstr_IdExpediente, gstr_IdExpediente)
                                }
                                catch { }
                            }
                            else if (item_resAsientosLog[j].Contains("[I]"))
                                lstr_Resultado[0] = "info";

                            lstr_logAsiento += "\n" + (j + 1) + ": " + item_resAsientosLog[j];

                            lstr_Resultado[1] = lstr_logAsiento;
                        }

                        //str_Mensaje += lstr_logAsiento;

                        //ws_SGService.uwsRegistrarAccionBitacoraCo
                        reg_Bit.ufnRegistrarAccionBitacora    ("CT", str_Usuario, "Enviar Asiento", str_IdExpediente + ":" + str_Sociedad +
                            " Operación: " + str_IdOperacion +
                            "Resultado: " + lstr_logAsiento,
                            str_IdExpediente, str_Trasaccion, str_Sociedad);

                        try
                        {

                            String[] lstr_AsientosResultado = new String[3];
                            Int32 lint_IdExp = 0;

                            String lstr_query = "SELECT IdExp FROM co.Expedientes exp " +
                                "WHERE exp.IdExpediente ='" + str_IdExpediente + "' " +
                                "AND exp.IdSociedadGL ='" + str_Sociedad + "' " +
                                "AND exp.EstadoExpediente = 'Activo'";
                            //"AND cp.EstadoTransaccion != 'estadotran'";

                            DataTable dt_Resoluciones = GetData(lstr_query);

                            foreach (DataRow dr_Resolucion in dt_Resoluciones.Rows)
                            {
                                lint_IdExp = Convert.ToInt32(dr_Resolucion["IdExp"]);
                            }
                            if (lstr_Resultado[0].Contains("Contabilizado"))
                            {
                                lstr_AsientosResultado = //ws_SGService.uwsRegistrarCobrosPagos(str_IdOperacion,
                                    reg_CP.RegistrarCobrosPagos(str_IdOperacion,
                                    str_IdExpediente, lint_IdExp, "REV", 0, 0,
                                    0, 0,
                                    arrdec_Montos == null ? MontoPricipalColones : arrdec_Montos[0],
                                    arrdec_Montos == null ? MontoPricipalColones : arrdec_Montos[1],//Monto Pr
                                    arrdec_Montos == null ? 0 : (arrdec_Montos.Count() >= 2 ? arrdec_Montos[2] : 0),
                                    arrdec_Montos == null ? MontoInteresColones : (arrdec_Montos.Count() >= 3 ? arrdec_Montos[3] : 0),
                                    arrdec_Montos == null ? MontoInteresColones : (arrdec_Montos.Count() >= 4 ? arrdec_Montos[4] : 0),
                                    arrdec_Montos == null ? 0 : (arrdec_Montos.Count() >= 5 ? arrdec_Montos[5] : 0),
                                    arrdec_Montos == null ? 0 : (arrdec_Montos.Count() >= 6 ? arrdec_Montos[6] : 0),
                                    arrdec_Montos == null ? 0 : (arrdec_Montos.Count() >= 7 ? arrdec_Montos[7] : 0),
                                    arrdec_Montos == null ? 0 : (arrdec_Montos.Count() >= 8 ? arrdec_Montos[8] : 0),
                                    arrdec_Montos == null ? 0 : (arrdec_Montos.Count() >= 9 ? arrdec_Montos[9] : 0),
                                    arrdec_Montos == null ? 0 : (arrdec_Montos.Count() >= 10 ? arrdec_Montos[10] : 0),
                                    0,
                                    0, 0, 0,//Intereses
                                    0, 0, 0,
                                    0, 0, 0,
                                    0, 0, 0,
                                    0, 0, 0, 0,//Anteriores

                                    "tipotra", "estadotran", DateTime.Today, "Reversion", str_Usuario);
                            }
                        }
                        catch (Exception ex)
                        {
                            str_Mensaje += ex.Message + Environment.NewLine;

                        }
                    }

                    catch (Exception ex)
                    {
                        lstr_Resultado[0] = "error";
                        lstr_Resultado[1] = lstr_logAsiento + "\n" + ex.Message;

                        //ws_SGService.uwsRegistrarAccionBitacoraCo
                            reg_Bit.ufnRegistrarAccionBitacora("CT", str_Usuario, "Enviar Asiento", str_IdExpediente + ":" + str_Sociedad +
                            " Operación: " + str_IdOperacion + "\n" + str_Leyenda + "\n" + lstr_Montos +
                           "\nResultado: " + lstr_Resultado,
                           str_IdExpediente, str_Trasaccion, str_Sociedad);

                        return lstr_Resultado;
                    }
                }
                else
                {
                    lstr_Resultado[0] = "error";
                    lstr_Resultado[1] = "Error: Los datos de consulta del asiento, no fue encontrada en la configuracion del Sistema Gestor.";
                }
            }
            return lstr_Resultado;

        }


        private static DataTable GetData(string lstr_query)
        {
            /*string lstr_ConnString = ConfigurationManager.ConnectionStrings["GestNICSPDEVConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(lstr_ConnString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = lstr_query;
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        using (DataSet ds = new DataSet())
                        {
                            DataTable dt = new DataTable();
                            sda.Fill(dt);
                            return dt;
                        }
                    }
                }
            }*/
            DataSet ds = new DataSet();
            ds = reg_Dinamico.ConsultarDinamico(lstr_query);
            if (ds.Tables.Count > 0)
            {
                return ds.Tables["Table"];
            }
            return null;
        }


        private static DataSet ConsultarTiposAsientos2(String str_Sociedad, string str_modulo, string str_operacion, string str_tipoProcesoExpediente)
        {
            DataSet ds;
            DataSet lds_TirasAsientos;
            String lstr_SociedadFi = string.Empty;

            String lstr_ConsultaSociedades = "SELECT IdSociedadFi from ma.SociedadesFinancieras " +
            "WHERE IdSociedadGL='" + str_Sociedad + "'";

            DataTable lds_NombreSociedades = GetData(lstr_ConsultaSociedades);
            DataRow ldr_NombreSociedad = null;

            if (lds_NombreSociedades.Rows.Count > 0)
            {
                ldr_NombreSociedad = lds_NombreSociedades.Rows[0];
                lstr_SociedadFi = ldr_NombreSociedad["IdSociedadFi"].ToString();
            }


            lds_TirasAsientos = //ws_SGService.uwsConsultarTiposAsiento
                reg_TiposAsiento.ConsultarTiposAsiento (lstr_SociedadFi, str_modulo, str_operacion, "", "", "CRC", str_tipoProcesoExpediente, null, null);

            return lds_TirasAsientos;

        }


        private static void CalculoDiferencialCambiarioOld()
        {
            #region variables
            DataTable ldt_MontosExpedientes = new DataTable();
            DataSet lds_MontosExpedientes = new DataSet();

            DateTime? ldt_FechaActual = DateTime.Today;//new DateTime();
            DateTime? ldt_FchInicio = Convert.ToDateTime(Convert.ToString(DateTime.Today.Year) + "-" + Convert.ToString(DateTime.Today.Month) + "-01");
            
            DataRow[] ldar_Temporal;

            string[] tipocambio = new string[4];

            tipocambio = CargarIndicadoresEco();

            decimal ldec_CompraActual = tipocambio[0] == "" ? 0 : Convert.ToDecimal(tipocambio[0]);
            decimal ldec_VentaActual = tipocambio[1] == "" ? 0 : Convert.ToDecimal(tipocambio[1]);
            decimal ldec_EuroActual = tipocambio[2] == "" ? 0 : Convert.ToDecimal(tipocambio[2]);
            decimal ldec_TBPActual = tipocambio[3] == "" ? 0 : Convert.ToDecimal(tipocambio[3]);
            decimal ldec_TipoCambioActual = 0;

            String[] lstr_Resultado = new String[2];
            String[] lstr_ResultadoRegistro = new String[2];

            string lstr_IdModulo = "IdModulo In ('CT')";
            string lstr_Operacion = String.Empty;
            string lstr_TipoExpediente = string.Empty;
            int lint_CantidadLineasAsiento;
            bool lbool_cambioMonto = false;
            string lstr_Leyenda = String.Empty;
            string lstr_Transaccion = "Diferencial Cambiario";
            decimal[] larrdec_Montos;
            decimal[] larrdec_MontosArriba;
            decimal[] larrdec_MontosAbajo;
            String lstr_AsientosResultado = String.Empty;
            #endregion

            lds_MontosExpedientes = ConsultarMontosExpedientes("", "", 0,0, "", ldt_FchInicio, ldt_FechaActual);
            ldt_MontosExpedientes = lds_MontosExpedientes.Tables["Table"];

            if ((ldt_MontosExpedientes != null) && (ldt_MontosExpedientes.Rows.Count > 0))
            {
                foreach (DataRow dr_FilaExpediente in ldt_MontosExpedientes.Rows)
                {
                    #region Inicial
                    Console.WriteLine("______________________________________________");
                    Console.Write("Id Expediente: ");
                    Console.WriteLine(dr_FilaExpediente["IdExpedienteFK"].ToString());
                    Console.WriteLine("______________________________________________");

                    str_Mensaje += "______________________________________________" + Environment.NewLine;
                    str_Mensaje += "Id Expediente: \n";
                    str_Mensaje += dr_FilaExpediente["IdExpedienteFK"].ToString() + Environment.NewLine;
                    str_Mensaje += "______________________________________________" + Environment.NewLine;

                    string resultado = String.Empty;

                    decimal ldec_DifMontoPrincipal = 0;
                    decimal ldec_DifMontoIntereses = 0;

                    decimal ldec_DifIntereses = 0;
                    decimal ldec_DifInteresesMoratorios = 0;
                    decimal ldec_DifCostas = 0;
                    decimal ldec_DifDanoMoral = 0;

                    string lstr_IdExpediente = dr_FilaExpediente["IdExpedienteFK"].ToString();
                    string lstr_IdSociedad = dr_FilaExpediente["IdSociedadGL"].ToString();
                    string lstr_EstadoResolucion = dr_FilaExpediente["EstadoResolucion"].ToString();
                    String lstr_Moneda = dr_FilaExpediente["Moneda"].ToString();
                    lstr_TipoExpediente = ConsultarTipoExpediente(lstr_IdExpediente);
                    #endregion

                    if (lstr_Moneda.Contains("USD") || lstr_Moneda.Contains("EUR"))
                       // && (lstr_IdExpediente.Contains("01 - INC - LIQ")))
                    {
                        #region carga data
                        decimal ldec_TipoCambio = Convert.ToDecimal(dr_FilaExpediente["TipoCambio"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["TipoCambio"].ToString());
                        decimal ldec_Tbp = Convert.ToDecimal(dr_FilaExpediente["Tbp"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["Tbp"].ToString());
                        decimal ldec_Tiempo = Convert.ToDecimal(dr_FilaExpediente["Tiempo"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["Tiempo"].ToString());

                        decimal ldec_MontoPrincipal = Convert.ToDecimal(dr_FilaExpediente["MontoPrincipal"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoPrincipal"].ToString());
                        decimal ldec_MontoPrincipalColones = Convert.ToDecimal(dr_FilaExpediente["MontoPrincipalColones"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoPrincipalColones"].ToString());
                        decimal ldec_MontoPrincipalCierre = Convert.ToDecimal(dr_FilaExpediente["MontoPrincipalColonesCierre"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoPrincipalColonesCierre"].ToString());

                        decimal ldec_MontoIntereses = Convert.ToDecimal(dr_FilaExpediente["MontoIntereses"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoIntereses"].ToString());
                        decimal ldec_MontoInteresesColones = Convert.ToDecimal(dr_FilaExpediente["MontoInteresesColones"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoInteresesColones"].ToString());
                        decimal ldec_MontoInteresesColonesCierre = Convert.ToDecimal(dr_FilaExpediente["MontoInteresesColonesCierre"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoInteresesColonesCierre"].ToString());

                        decimal ldec_InteresesMoratorios = Convert.ToDecimal(dr_FilaExpediente["InteresesMoratorios"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["InteresesMoratorios"].ToString());
                        decimal ldec_InteresesMoratoriosColones = Convert.ToDecimal(dr_FilaExpediente["InteresesMoratoriosColones"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["InteresesMoratoriosColones"].ToString());
                        decimal ldec_InteresesMoratoriosColonesCierre = Convert.ToDecimal(dr_FilaExpediente["InteresesMoratoriosColonesCierre"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["InteresesMoratoriosColonesCierre"].ToString());

                        decimal ldec_Intereses = Convert.ToDecimal(dr_FilaExpediente["Intereses"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["Intereses"].ToString());
                        decimal ldec_InteresesColones = Convert.ToDecimal(dr_FilaExpediente["InteresesColones"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["InteresesColones"].ToString());
                        decimal ldec_InteresesColonesCierre = Convert.ToDecimal(dr_FilaExpediente["InteresesColonesCierre"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["InteresesColonesCierre"].ToString());

                        decimal ldec_Costas = Convert.ToDecimal(dr_FilaExpediente["Costas"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["Costas"].ToString());
                        decimal ldec_CostasColones = Convert.ToDecimal(dr_FilaExpediente["CostasColones"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["CostasColones"].ToString());
                        decimal ldec_CostasColonesCierre = Convert.ToDecimal(dr_FilaExpediente["CostasColonesCierre"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["CostasColonesCierre"].ToString());

                        decimal ldec_DanoMoral = Convert.ToDecimal(dr_FilaExpediente["DanoMoral"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["DanoMoral"].ToString());
                        decimal ldec_DanoMoralColones = Convert.ToDecimal(dr_FilaExpediente["DanoMoralColones"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["DanoMoralColones"].ToString());
                        decimal ldec_DanoMoralColonesCierre = Convert.ToDecimal(dr_FilaExpediente["DanoMoralColonesCierre"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["DanoMoralColonesCierre"].ToString());
                        #endregion

                        #region tipos cambio
                        lstr_TipoExpediente = ConsultarTipoExpediente(lstr_IdExpediente);
                        //Console.WriteLine(lstr_TipoExpediente);
                        str_Mensaje += lstr_TipoExpediente + Environment.NewLine;

                        if (lstr_Moneda.Contains("CRC"))
                            ldec_TipoCambioActual = 1;
                        else if (lstr_Moneda.Contains("USD"))
                        {
                            if (lstr_TipoExpediente.Contains("Actor"))
                                ldec_TipoCambioActual = ldec_CompraActual;
                            else if (lstr_TipoExpediente.Contains("Demandado"))
                                ldec_TipoCambioActual = ldec_VentaActual;
                        }
                        else if (lstr_Moneda.Contains("EUR"))
                        {
                            if (lstr_TipoExpediente.Contains("Actor"))
                                ldec_TipoCambioActual = ldec_CompraActual * ldec_EuroActual;
                            else if (lstr_TipoExpediente.Contains("Demandado"))
                                ldec_TipoCambioActual = ldec_VentaActual * ldec_EuroActual;
                        }
                        #endregion

                        #region Calculo Diferencial

                        larrdec_MontosArriba = new Decimal[6];
                        larrdec_MontosAbajo = new Decimal[6];

                        if ((ldec_MontoPrincipalCierre == 0) && (ldec_MontoInteresesColonesCierre == 0))
                        {
                            ldec_MontoPrincipalCierre = ldec_MontoPrincipal * ldec_TipoCambio;
                            ldec_MontoInteresesColonesCierre = ldec_MontoIntereses * ldec_TipoCambio;
                        }

                        if ((ldec_InteresesColonesCierre == 0) || (ldec_InteresesMoratoriosColonesCierre == 0) ||
                            (ldec_CostasColonesCierre == 0) || (ldec_DanoMoralColonesCierre == 0))
                        {
                            ldec_InteresesColonesCierre = ldec_Intereses * ldec_TipoCambio;
                            ldec_InteresesMoratoriosColonesCierre = ldec_InteresesMoratorios * ldec_TipoCambio;
                            ldec_CostasColonesCierre = ldec_Costas * ldec_TipoCambio;
                            ldec_DanoMoralColonesCierre = ldec_DanoMoral * ldec_TipoCambio;
                        }

                        ldec_DifMontoPrincipal = (ldec_MontoPrincipal * ldec_TipoCambioActual) - ldec_MontoPrincipalCierre;
                        ldec_DifMontoIntereses = (ldec_MontoIntereses * ldec_TipoCambioActual) - ldec_MontoInteresesColonesCierre;

                        ldec_DifIntereses = (ldec_Intereses * ldec_TipoCambioActual) - ldec_InteresesColonesCierre;
                        ldec_DifInteresesMoratorios = (ldec_InteresesMoratorios * ldec_TipoCambioActual) - ldec_InteresesMoratoriosColonesCierre;
                        ldec_DifCostas = (ldec_Costas * ldec_TipoCambioActual) - ldec_CostasColonesCierre;
                        ldec_DifDanoMoral = (ldec_DanoMoral * ldec_TipoCambioActual) - ldec_DanoMoralColonesCierre;

                        ldec_MontoPrincipalCierre = ldec_MontoPrincipal * ldec_TipoCambioActual;
                        ldec_MontoInteresesColonesCierre = ldec_MontoIntereses * ldec_TipoCambioActual;

                        ldec_InteresesColonesCierre = ldec_Intereses * ldec_TipoCambioActual;
                        ldec_InteresesMoratoriosColonesCierre = ldec_InteresesMoratorios * ldec_TipoCambioActual;
                        ldec_CostasColonesCierre = ldec_Costas * ldec_TipoCambioActual;
                        ldec_DanoMoralColonesCierre = ldec_DanoMoral * ldec_TipoCambioActual;

                        if (ldec_DifMontoPrincipal > 0)
                            larrdec_MontosArriba[0] = ldec_DifMontoPrincipal;
                        else if (ldec_DifMontoPrincipal < 0)
                            larrdec_MontosAbajo[0] = ldec_DifMontoPrincipal * -1;

                        if (ldec_DifMontoIntereses > 0)
                            larrdec_MontosArriba[1] = ldec_DifMontoIntereses;
                        else if (ldec_DifMontoIntereses < 0)
                            larrdec_MontosAbajo[1] = ldec_DifMontoIntereses * -1;

                        if (ldec_DifIntereses > 0)
                            larrdec_MontosArriba[1] = ldec_DifIntereses;
                        else if (ldec_DifIntereses < 0)
                            larrdec_MontosAbajo[1] = ldec_DifIntereses * -1;

                        if (ldec_DifInteresesMoratorios > 0)
                            larrdec_MontosArriba[2] = ldec_DifInteresesMoratorios;
                        else if (ldec_DifInteresesMoratorios < 0)
                            larrdec_MontosAbajo[2] = ldec_DifInteresesMoratorios * -1;

                        if (ldec_DifCostas > 0)
                            larrdec_MontosArriba[3] = ldec_DifCostas;
                        else if (ldec_DifCostas < 0)
                            larrdec_MontosAbajo[3] = ldec_DifCostas * -1;

                        if (ldec_DifDanoMoral > 0)
                            larrdec_MontosArriba[4] = ldec_DifDanoMoral;
                        else if (ldec_DifDanoMoral < 0)
                            larrdec_MontosAbajo[4] = ldec_DifDanoMoral * -1;

                        Boolean lbool_continuar = false;
                        for (int j = 0; j < larrdec_MontosArriba.Count(); j++)
                        {
                            if (larrdec_MontosArriba[j] > 0)
                            {
                                lbool_continuar = true;
                            }
                        }

                        #endregion

                        #region envio asientos
                        if ( ( ((ldec_DifMontoPrincipal > 0 || ldec_DifMontoIntereses > 0) ||
                            (ldec_DifIntereses > 0 || ldec_DifInteresesMoratorios > 0 || ldec_DifCostas > 0 || ldec_DifDanoMoral > 0))
                            && lbool_continuar) ||

                            ( ((ldec_DifMontoPrincipal < 0 || ldec_DifMontoIntereses < 0) ||
                            (ldec_DifIntereses < 0 || ldec_DifInteresesMoratorios < 0 || ldec_DifCostas < 0 || ldec_DifDanoMoral < 0))
                            && !lbool_continuar))
                        {
                            #region diferencial
                            if (lstr_TipoExpediente.Contains("Demandado") &&
                                (lstr_EstadoResolucion.Contains("En Firme") || lstr_EstadoResolucion.Contains("Liquidacion")))
                            {
                                #region Demandado RF Liq
                                lint_CantidadLineasAsiento = 12;

                                if (lbool_continuar)
                                {
                                    lstr_Resultado = EnviarAsientos(lstr_IdExpediente, lstr_IdSociedad, lstr_AsientosResultado, lstr_IdModulo, "CT22", lstr_Transaccion, lstr_Leyenda, lbool_cambioMonto, larrdec_MontosArriba, lint_CantidadLineasAsiento, ldec_DifMontoPrincipal, ldec_DifMontoIntereses, null, null, null, null);
                                    Console.WriteLine(lstr_Resultado[1]);
                                    Console.WriteLine(lstr_Resultado[2]);
                                    Console.WriteLine("------------------------------------------------");

                                    str_Mensaje += lstr_Resultado[1] + Environment.NewLine;
                                    str_Mensaje += lstr_Resultado[2] + Environment.NewLine;
                                    str_Mensaje += "------------------------------------------------" + Environment.NewLine;
                                }
                                else
                                {
                                    lstr_AsientosResultado = String.Empty;
                                    lstr_Resultado = EnviarAsientos(lstr_IdExpediente, lstr_IdSociedad, lstr_AsientosResultado, lstr_IdModulo, "CT23", lstr_Transaccion, lstr_Leyenda, lbool_cambioMonto, larrdec_MontosAbajo, lint_CantidadLineasAsiento, ldec_DifMontoPrincipal, ldec_DifMontoIntereses, null, null, null, null);
                                    Console.WriteLine(lstr_Resultado[1]);
                                    Console.WriteLine(lstr_Resultado[2]);
                                    Console.WriteLine("------------------------------------------------");

                                    str_Mensaje += lstr_Resultado[1] + Environment.NewLine;
                                    str_Mensaje += lstr_Resultado[2] + Environment.NewLine;
                                    str_Mensaje += "------------------------------------------------" + Environment.NewLine;
                                }
                                if (lstr_Resultado.Contains("Contabilizado"))
                                {
                                    lstr_Resultado[0] = "exito";
                                }
                                else
                                {
                                    lstr_Resultado[0] = "fallo";
                                }
                                #endregion
                            }
                            else if (lstr_TipoExpediente.Contains("Demandado"))
                            {
                                #region Demandado
                                lint_CantidadLineasAsiento = 4;

                                if (lbool_continuar)
                                {
                                    lstr_AsientosResultado = String.Empty;
                                    lstr_Resultado = EnviarAsientos(lstr_IdExpediente, lstr_IdSociedad, lstr_AsientosResultado, lstr_IdModulo, "CT28", lstr_Transaccion, lstr_Leyenda, lbool_cambioMonto, larrdec_MontosArriba, lint_CantidadLineasAsiento, ldec_DifMontoPrincipal, ldec_DifMontoIntereses, null, null, null, null);
                                    Console.WriteLine(lstr_Resultado[1]);
                                    Console.WriteLine(lstr_Resultado[2]);
                                    Console.WriteLine("------------------------------------------------");

                                    str_Mensaje += lstr_Resultado[1] + Environment.NewLine;
                                    str_Mensaje += lstr_Resultado[2] + Environment.NewLine;
                                    str_Mensaje += "------------------------------------------------" + Environment.NewLine;
                                }
                                else
                                {
                                    lstr_AsientosResultado = String.Empty;
                                    lstr_Resultado = EnviarAsientos(lstr_IdExpediente, lstr_IdSociedad, lstr_AsientosResultado, lstr_IdModulo, "CT29", lstr_Transaccion, lstr_Leyenda, lbool_cambioMonto, larrdec_MontosAbajo, lint_CantidadLineasAsiento, ldec_DifMontoPrincipal, ldec_DifMontoIntereses, null, null, null, null);
                                    Console.WriteLine(lstr_Resultado[1]);
                                    Console.WriteLine(lstr_Resultado[2]);
                                    Console.WriteLine("------------------------------------------------");

                                    str_Mensaje += lstr_Resultado[1] + Environment.NewLine;
                                    str_Mensaje += lstr_Resultado[2] + Environment.NewLine;
                                    str_Mensaje += "------------------------------------------------" + Environment.NewLine;
                                }
                                if (lstr_Resultado.Contains("Contabilizado"))
                                {
                                    lstr_Resultado[0] = "exito";
                                }
                                else
                                {
                                    lstr_Resultado[0] = "fallo";
                                }
                                #endregion
                            }
                            else if (lstr_TipoExpediente.Contains("Actor") && (lstr_IdExpediente.Contains("521")) &&
                                (lstr_EstadoResolucion.Contains("En Firme") || lstr_EstadoResolucion.Contains("Liquidacion")))
                            {
                                #region Actor RF Liq
                                lint_CantidadLineasAsiento = 12;
                                if (lbool_continuar)
                                {
                                    lstr_AsientosResultado = String.Empty;
                                    lstr_Resultado = EnviarAsientos(lstr_IdExpediente, lstr_IdSociedad, lstr_AsientosResultado, lstr_IdModulo, "CT24", lstr_Transaccion, lstr_Leyenda, lbool_cambioMonto, larrdec_MontosArriba, lint_CantidadLineasAsiento, ldec_DifMontoPrincipal, ldec_DifMontoIntereses, null, null, null, null);
                                    Console.WriteLine(lstr_Resultado[1]);
                                    Console.WriteLine(lstr_Resultado[2]);
                                    Console.WriteLine("------------------------------------------------");

                                    str_Mensaje += lstr_Resultado[1] + Environment.NewLine;
                                    str_Mensaje += lstr_Resultado[2] + Environment.NewLine;
                                    str_Mensaje += "------------------------------------------------" + Environment.NewLine;
                                }
                                else
                                {
                                    lstr_AsientosResultado = String.Empty;
                                    lstr_Resultado = EnviarAsientos(lstr_IdExpediente, lstr_IdSociedad, lstr_AsientosResultado, lstr_IdModulo, "CT25", lstr_Transaccion, lstr_Leyenda, lbool_cambioMonto, larrdec_MontosAbajo, lint_CantidadLineasAsiento, ldec_DifMontoPrincipal, ldec_DifMontoIntereses, null, null, null, null);
                                    Console.WriteLine(lstr_Resultado[1]);
                                    Console.WriteLine(lstr_Resultado[2]);
                                    Console.WriteLine("------------------------------------------------");

                                    str_Mensaje += lstr_Resultado[1] + Environment.NewLine;
                                    str_Mensaje += lstr_Resultado[2] + Environment.NewLine;
                                    str_Mensaje += "------------------------------------------------" + Environment.NewLine;
                                }
                                if (lstr_Resultado.Contains("Contabilizado"))
                                {
                                    lstr_Resultado[0] = "exito";
                                }
                                else
                                {
                                    lstr_Resultado[0] = "fallo";
                                }
                                #endregion
                            }

                            Int32 lint_IdRes = ConsultarIdRes(lstr_IdExpediente, lstr_IdSociedad, lstr_EstadoResolucion);

                            try
                            {
                                #region Registro
                                lstr_ResultadoRegistro = //ws_SGService.uwsModificarCobrosPagos(
                                    reg_clsCobrosPagos.ModificarCobrosPagos(
                                    lstr_IdExpediente, lstr_IdSociedad, lint_IdRes,
                                    lstr_Moneda, ldec_TipoCambio, ldec_Tbp, ldec_Tiempo, ldec_TipoCambioActual,

                                    ldec_MontoPrincipal, ldec_MontoPrincipalColones, ldec_MontoPrincipalCierre,
                                    ldec_MontoIntereses, ldec_MontoInteresesColones, ldec_MontoInteresesColonesCierre,
                                    0, 0, 0,
                                    0, 0, 0,
                                    ldec_Intereses, ldec_InteresesColones, ldec_InteresesColonesCierre,
                                    ldec_InteresesMoratorios, ldec_InteresesMoratoriosColones, ldec_InteresesMoratoriosColonesCierre,
                                    ldec_Costas, ldec_CostasColones, ldec_CostasColonesCierre,
                                    ldec_DanoMoral, ldec_DanoMoralColones, ldec_DanoMoralColonesCierre,
                                    0, 0, 0, 0, 0, 0,
                                    "d", "d");

                                if (lstr_ResultadoRegistro[0].Contains("00"))
                                {
                                    lstr_Resultado[0] = "exito";
                                    Console.WriteLine("Expediente " + lstr_IdExpediente + " modificado con éxito.");

                                    str_Mensaje += "Expediente " + lstr_IdExpediente + " modificado con éxito." + Environment.NewLine;
                                }
                                else
                                {
                                    lstr_Resultado[0] = "fallo";
                                    Console.WriteLine("Fallo al modificar expediente " + lstr_IdExpediente + ". \n" + lstr_ResultadoRegistro[1]);

                                    str_Mensaje += "Fallo al modificar expediente " + lstr_IdExpediente + ". \n" + lstr_ResultadoRegistro[1] + Environment.NewLine;
                                }

                                
                                #endregion
                            }
                            catch { }
                            #endregion

                            Console.WriteLine("------------------------------------------------");
                            str_Mensaje += "------------------------------------------------" + Environment.NewLine;
                        }
                        #endregion
                    }

                }

                Console.WriteLine("Fin de Proceso");
                Console.WriteLine("------------------------------------------------");

                str_Mensaje += "Fin de Proceso\n";
                str_Mensaje += "------------------------------------------------" + Environment.NewLine;

                GuardarResultadoContabilizacion();
                }
        }

        private static void CalculoIncobrabilidadOld()
        {
            #region variables
            DataTable ldt_MontosExpedientes = new DataTable();
            DataSet lds_MontosExpedientes = new DataSet();

            DateTime? ldt_FechaActual = DateTime.Today;//new DateTime();
            DateTime? ldt_FchInicio = Convert.ToDateTime(Convert.ToString(DateTime.Today.Year) + "-" + Convert.ToString(DateTime.Today.Month) + "-01");

            DataRow[] ldar_Temporal;

            string[] tipocambio = new string[4];

            tipocambio = CargarIndicadoresEco();

            decimal ldec_CompraActual = tipocambio[0] == "" ? 0 : Convert.ToDecimal(tipocambio[0]);
            decimal ldec_VentaActual = tipocambio[1] == "" ? 0 : Convert.ToDecimal(tipocambio[1]);
            decimal ldec_EuroActual = tipocambio[2] == "" ? 0 : Convert.ToDecimal(tipocambio[2]);
            decimal ldec_TBPActual = tipocambio[3] == "" ? 0 : Convert.ToDecimal(tipocambio[3]);
            decimal ldec_TipoCambioActual = 0;

            String[] lstr_Resultado = new String[2];
            String[] lstr_ResultadoRegistro = new String[2];

            string lstr_IdModulo = "IdModulo In ('CT')";
            string lstr_Operacion = String.Empty;
            string lstr_TipoExpediente = string.Empty;
            int lint_CantidadLineasAsiento;
            bool lbool_cambioMonto = false;
            string lstr_Leyenda = String.Empty;
            string lstr_Transaccion = "Diferencial Cambiario";
            decimal[] larrdec_Montos;
            decimal[] larrdec_MontosArriba;
            decimal[] larrdec_MontosAbajo;
            String lstr_AsientosResultado = String.Empty;

            #endregion

            lds_MontosExpedientes = ConsultarMontosExpedientes("", "", 0, 0, "", ldt_FchInicio, ldt_FechaActual);
            ldt_MontosExpedientes = lds_MontosExpedientes.Tables["Table"];

            if (ldt_MontosExpedientes.Rows.Count > 0)
            {
                foreach (DataRow dr_FilaExpediente in ldt_MontosExpedientes.Rows)
                {
                    #region Inicial
                    Console.WriteLine("______________________________________________");
                    Console.Write("Id Expediente: ");
                    Console.WriteLine(dr_FilaExpediente["IdExpedienteFK"].ToString());
                    Console.WriteLine("______________________________________________");

                    str_Mensaje += "______________________________________________" + Environment.NewLine;
                    str_Mensaje += "Id Expediente: \n";
                    str_Mensaje += dr_FilaExpediente["IdExpedienteFK"].ToString() + Environment.NewLine;
                    str_Mensaje += "______________________________________________" + Environment.NewLine;

                    string resultado = String.Empty;

                    decimal ldec_DifMontoPrincipal = 0;
                    decimal ldec_DifMontoIntereses = 0;

                    decimal ldec_DifIntereses = 0;
                    decimal ldec_DifInteresesMoratorios = 0;
                    decimal ldec_DifCostas = 0;
                    decimal ldec_DifDanoMoral = 0;

                    string lstr_IdExpediente = dr_FilaExpediente["IdExpedienteFK"].ToString();
                    string lstr_IdSociedad = dr_FilaExpediente["IdSociedadGL"].ToString();
                    string lstr_EstadoResolucion = dr_FilaExpediente["EstadoResolucion"].ToString();
                    String lstr_Moneda = dr_FilaExpediente["Moneda"].ToString();
                    lstr_TipoExpediente = ConsultarTipoExpediente(lstr_IdExpediente);

                    DateTime ldt_FchModifica = Convert.ToDateTime(dr_FilaExpediente["FchModifica"].ToString());
                    DateTime ldt_FchCreacion = Convert.ToDateTime(dr_FilaExpediente["FchCreacion"].ToString());
                    #endregion

                    if ((lstr_Moneda.Contains("USD") || lstr_Moneda.Contains("EUR") || (lstr_Moneda.Contains("CRC")))
                        && (lstr_IdExpediente.Contains("01 - INC - LIQ")))
                    {
                        #region carga data
                        Decimal ldec_TipoCambio = Convert.ToDecimal(dr_FilaExpediente["TipoCambio"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["TipoCambio"].ToString());
                        Decimal ldec_Tbp = Convert.ToDecimal(dr_FilaExpediente["Tbp"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["Tbp"].ToString());
                        Decimal ldec_Tiempo = Convert.ToDecimal(dr_FilaExpediente["Tiempo"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["Tiempo"].ToString());
                        Decimal ldec_TipoCambioCierre = Convert.ToDecimal(dr_FilaExpediente["TipoCambioCierre"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["TipoCambioCierre"].ToString());
                        
                        Decimal ldec_MontoPrincipal = Convert.ToDecimal(dr_FilaExpediente["MontoPrincipal"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoPrincipal"].ToString());
                        Decimal ldec_MontoPrincipalColones = Convert.ToDecimal(dr_FilaExpediente["MontoPrincipalColones"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoPrincipalColones"].ToString());
                        Decimal ldec_MontoPrincipalCierre = Convert.ToDecimal(dr_FilaExpediente["MontoPrincipalColonesCierre"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoPrincipalColonesCierre"].ToString());

                        Decimal ldec_MontoIntereses = Convert.ToDecimal(dr_FilaExpediente["MontoIntereses"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoIntereses"].ToString());
                        decimal ldec_MontoInteresesColones = Convert.ToDecimal(dr_FilaExpediente["MontoInteresesColones"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoInteresesColones"].ToString());
                        decimal ldec_MontoInteresesColonesCierre = Convert.ToDecimal(dr_FilaExpediente["MontoInteresesColonesCierre"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoInteresesColonesCierre"].ToString());

                        decimal ldec_InteresesMoratorios = Convert.ToDecimal(dr_FilaExpediente["InteresesMoratorios"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["InteresesMoratorios"].ToString());
                        decimal ldec_InteresesMoratoriosColones = Convert.ToDecimal(dr_FilaExpediente["InteresesMoratoriosColones"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["InteresesMoratoriosColones"].ToString());
                        decimal ldec_InteresesMoratoriosColonesCierre = Convert.ToDecimal(dr_FilaExpediente["InteresesMoratoriosColonesCierre"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["InteresesMoratoriosColonesCierre"].ToString());

                        decimal ldec_Intereses = Convert.ToDecimal(dr_FilaExpediente["Intereses"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["Intereses"].ToString());
                        decimal ldec_InteresesColones = Convert.ToDecimal(dr_FilaExpediente["InteresesColones"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["InteresesColones"].ToString());
                        decimal ldec_InteresesColonesCierre = Convert.ToDecimal(dr_FilaExpediente["InteresesColonesCierre"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["InteresesColonesCierre"].ToString());

                        decimal ldec_Costas = Convert.ToDecimal(dr_FilaExpediente["Costas"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["Costas"].ToString());
                        decimal ldec_CostasColones = Convert.ToDecimal(dr_FilaExpediente["CostasColones"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["CostasColones"].ToString());
                        decimal ldec_CostasColonesCierre = Convert.ToDecimal(dr_FilaExpediente["CostasColonesCierre"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["CostasColonesCierre"].ToString());

                        decimal ldec_DanoMoral = Convert.ToDecimal(dr_FilaExpediente["DanoMoral"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["DanoMoral"].ToString());
                        decimal ldec_DanoMoralColones = Convert.ToDecimal(dr_FilaExpediente["DanoMoralColones"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["DanoMoralColones"].ToString());
                        decimal ldec_DanoMoralColonesCierre = Convert.ToDecimal(dr_FilaExpediente["DanoMoralColonesCierre"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["DanoMoralColonesCierre"].ToString());

                        Decimal ldec_MontoPrincipalAnterior = Convert.ToDecimal(dr_FilaExpediente["MontoPrincipalAnterior"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoPrincipalAnterior"].ToString());
                        Decimal ldec_MontoInteresesAnterior = Convert.ToDecimal(dr_FilaExpediente["MontoInteresesAnterior"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["MontoInteresesAnterior"].ToString());
                        Decimal ldec_InteresesAnterior = Convert.ToDecimal(dr_FilaExpediente["InteresesAnterior"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["InteresesAnterior"].ToString());
                        Decimal ldec_InteresesMoratoriosAnterior = Convert.ToDecimal(dr_FilaExpediente["InteresesMoratoriosAnterior"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["InteresesMoratoriosAnterior"].ToString());
                        Decimal ldec_CostasAnterior = Convert.ToDecimal(dr_FilaExpediente["CostasAnterior"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["CostasAnterior"].ToString());
                        Decimal ldec_DanoMoralAnterior = Convert.ToDecimal(dr_FilaExpediente["DanoMoralAnterior"].ToString().Equals("") ? "0.0" : dr_FilaExpediente["DanoMoralAnterior"].ToString());
                        #endregion

                        #region tipos cambio
                        lstr_TipoExpediente = ConsultarTipoExpediente(lstr_IdExpediente);
                        Console.WriteLine(lstr_TipoExpediente);
                        str_Mensaje += lstr_TipoExpediente + Environment.NewLine;

                        if (lstr_Moneda.Contains("CRC"))
                            ldec_TipoCambioActual = 1;
                        else if (lstr_Moneda.Contains("USD"))
                        {
                            if (lstr_TipoExpediente.Contains("Actor"))
                                ldec_TipoCambioActual = ldec_CompraActual;
                            else if (lstr_TipoExpediente.Contains("Demandado"))
                                ldec_TipoCambioActual = ldec_VentaActual;
                        }
                        else if (lstr_Moneda.Contains("EUR"))
                        {
                            if (lstr_TipoExpediente.Contains("Actor"))
                                ldec_TipoCambioActual = ldec_CompraActual * ldec_EuroActual;
                            else if (lstr_TipoExpediente.Contains("Demandado"))
                                ldec_TipoCambioActual = ldec_VentaActual * ldec_EuroActual;
                        }
                        #endregion

                        #region Cálculo Incobrabilidad

                        larrdec_MontosArriba = new Decimal[5];
                        larrdec_MontosAbajo = new Decimal[5];

                        TimeSpan lts_DiferenciaTiempo = DateTime.Today - ldt_FchModifica;

                        int lint_DiferenciaDias = lts_DiferenciaTiempo.Days;
                        double lint_Porcentaje = 0;

                        #region Porcentajes Previsiones
                        if (lint_DiferenciaDias > 1440)
                        {
                            lint_Porcentaje = 1;
                        }
                        else if (lint_DiferenciaDias > 1260)
                        {
                            lint_Porcentaje = 0.875;
                        }
                        else if (lint_DiferenciaDias > 1080)
                        {
                            lint_Porcentaje = 0.75;
                        }
                        else if (lint_DiferenciaDias > 900)
                        {
                            lint_Porcentaje = 0.675;
                        }
                        else if (lint_DiferenciaDias > 720)
                        {
                            lint_Porcentaje = 0.5;
                        }
                        else if (lint_DiferenciaDias > 540)
                        {
                            lint_Porcentaje = 0.375;
                        }
                        else if (lint_DiferenciaDias > 360)
                        {
                            lint_Porcentaje = 0.25;
                        }
                        else if (lint_DiferenciaDias > 180)
                        {
                            lint_Porcentaje = 0.1;
                        }
                        else if (lint_DiferenciaDias > 45)
                        {
                            lint_Porcentaje = 0.05;
                        }
                        else if (lint_DiferenciaDias >= 30)
                        {
                            lint_Porcentaje = 0.03;
                        }
                        #endregion

                        if (lstr_TipoExpediente.Contains("Actor") && (lint_DiferenciaDias > 30) &&
                            (lstr_EstadoResolucion.Contains("En Firme") || lstr_EstadoResolucion.Contains("Liquidacion")))
                        {
                            #region Demandado RF Liq
                            lint_CantidadLineasAsiento = 10;
                            bool lbool_continuar = true;
                            if (lbool_continuar)
                            {
                                if (lstr_EstadoResolucion.Contains("En Firme"))
                                {
                                        
                                    if (ldec_MontoPrincipalAnterior == 0)
                                    {
                                        if (lstr_Moneda.Contains("CRC"))
                                        {
                                            ldec_MontoPrincipalAnterior = ldec_MontoPrincipal * Convert.ToDecimal(lint_Porcentaje);
                                            ldec_MontoInteresesAnterior = ldec_MontoIntereses * Convert.ToDecimal(lint_Porcentaje);
                                            larrdec_MontosArriba[0] = ldec_MontoPrincipalAnterior;
                                            larrdec_MontosArriba[1] = ldec_MontoInteresesAnterior;
                                        }
                                        else
                                        {
                                            ldec_MontoPrincipalAnterior = ldec_MontoPrincipalCierre * Convert.ToDecimal(lint_Porcentaje);
                                            ldec_MontoInteresesAnterior = ldec_MontoInteresesColonesCierre * Convert.ToDecimal(lint_Porcentaje);
                                            larrdec_MontosArriba[0] = ldec_MontoPrincipalAnterior;
                                            larrdec_MontosArriba[1] = ldec_MontoInteresesAnterior;
                                        }
                                            
                                    }
                                    else
                                    {
                                        if (lstr_Moneda.Contains("CRC"))
                                        {
                                            ldec_MontoPrincipalAnterior = (ldec_MontoPrincipal * Convert.ToDecimal(lint_Porcentaje)) - ldec_MontoPrincipalAnterior;
                                            ldec_MontoInteresesAnterior = (ldec_MontoIntereses * Convert.ToDecimal(lint_Porcentaje)) - ldec_MontoInteresesAnterior;

                                            if (ldec_MontoPrincipalAnterior < 0)
                                            {
                                                ldec_MontoPrincipalAnterior = ldec_MontoPrincipalAnterior * -1;
                                                larrdec_MontosAbajo[0] = ldec_MontoPrincipalAnterior;
                                            }
                                            else
                                                larrdec_MontosArriba[0] = ldec_MontoPrincipalAnterior;

                                            if (ldec_MontoInteresesAnterior < 0)
                                            {
                                                ldec_MontoInteresesAnterior = ldec_MontoInteresesAnterior * -1;
                                                larrdec_MontosAbajo[1] = ldec_MontoInteresesAnterior;
                                            }
                                            else
                                                larrdec_MontosArriba[1] = ldec_MontoInteresesAnterior;
                                        }
                                        else
                                        {
                                            ldec_MontoPrincipalAnterior = (ldec_MontoPrincipalCierre * Convert.ToDecimal(lint_Porcentaje)) - ldec_MontoPrincipalAnterior;
                                            ldec_MontoInteresesAnterior = (ldec_MontoInteresesColonesCierre * Convert.ToDecimal(lint_Porcentaje)) - ldec_MontoInteresesAnterior;

                                            if (ldec_MontoPrincipalAnterior < 0)
                                            {
                                                ldec_MontoPrincipalAnterior = ldec_MontoPrincipalAnterior * -1;
                                                larrdec_MontosAbajo[0] = ldec_MontoPrincipalAnterior;
                                            }
                                            else
                                                larrdec_MontosArriba[0] = ldec_MontoPrincipalAnterior;

                                            if (ldec_MontoInteresesAnterior < 0)
                                            {
                                                ldec_MontoInteresesAnterior = ldec_MontoInteresesAnterior * -1;
                                                larrdec_MontosAbajo[1] = ldec_MontoInteresesAnterior;
                                            }
                                            else
                                                larrdec_MontosArriba[1] = ldec_MontoInteresesAnterior;
                                        }

                                    }
                                }
                                else
                                {
                                    if (ldec_InteresesMoratoriosAnterior == 0)
                                    {
                                        if (lstr_Moneda.Contains("CRC"))
                                        {
                                            ldec_InteresesAnterior = ldec_Intereses * Convert.ToDecimal(lint_Porcentaje);
                                            ldec_InteresesMoratoriosAnterior = ldec_InteresesMoratorios * Convert.ToDecimal(lint_Porcentaje);
                                            ldec_CostasAnterior = ldec_Costas * Convert.ToDecimal(lint_Porcentaje);
                                            ldec_DanoMoralAnterior = ldec_DanoMoral * Convert.ToDecimal(lint_Porcentaje);

                                            
                                        }
                                        else
                                        {
                                            ldec_InteresesAnterior = ldec_InteresesColonesCierre * Convert.ToDecimal(lint_Porcentaje);
                                            ldec_InteresesMoratoriosAnterior = ldec_InteresesMoratoriosColonesCierre * Convert.ToDecimal(lint_Porcentaje);
                                            ldec_CostasAnterior = ldec_CostasColonesCierre * Convert.ToDecimal(lint_Porcentaje);
                                            ldec_DanoMoralAnterior = ldec_DanoMoralColonesCierre * Convert.ToDecimal(lint_Porcentaje);

                                        }
                                        
                                        larrdec_MontosArriba[1] = ldec_InteresesAnterior;
                                        larrdec_MontosArriba[2] = ldec_InteresesMoratoriosAnterior;
                                        larrdec_MontosArriba[3] = ldec_CostasAnterior;
                                        larrdec_MontosArriba[4] = ldec_DanoMoralAnterior;

                                    }
                                    else
                                    {
                                        if (lstr_Moneda.Contains("CRC"))
                                        {
                                            ldec_InteresesAnterior = (ldec_Intereses * Convert.ToDecimal(lint_Porcentaje)) - ldec_InteresesAnterior;
                                            ldec_InteresesMoratoriosAnterior = (ldec_InteresesMoratorios * Convert.ToDecimal(lint_Porcentaje)) - ldec_InteresesMoratoriosAnterior;
                                            ldec_CostasAnterior = (ldec_Costas * Convert.ToDecimal(lint_Porcentaje)) - ldec_CostasAnterior;
                                            ldec_DanoMoralAnterior = (ldec_DanoMoral * Convert.ToDecimal(lint_Porcentaje)) - ldec_DanoMoralAnterior;
                                        
                                        }
                                        else 
                                        {
                                            ldec_InteresesAnterior = ((ldec_Intereses * ldec_TipoCambioCierre) * Convert.ToDecimal(lint_Porcentaje)) - ldec_InteresesAnterior;
                                            ldec_InteresesMoratoriosAnterior = ((ldec_InteresesMoratorios * ldec_TipoCambioCierre) * Convert.ToDecimal(lint_Porcentaje)) - ldec_InteresesMoratoriosAnterior;
                                            ldec_CostasAnterior = ((ldec_Costas * ldec_TipoCambioCierre) * Convert.ToDecimal(lint_Porcentaje)) - ldec_CostasAnterior;
                                            ldec_DanoMoralAnterior = ((ldec_DanoMoral * ldec_TipoCambioCierre) * Convert.ToDecimal(lint_Porcentaje)) - ldec_DanoMoralAnterior;
                                        }
                                        
                                        if(ldec_InteresesAnterior < 0)
                                        {
                                            ldec_InteresesAnterior = ldec_InteresesAnterior * -1;
                                            larrdec_MontosAbajo[1] = ldec_InteresesAnterior;
                                        }
                                        else
                                            larrdec_MontosArriba[1] = ldec_InteresesAnterior;

                                        if (ldec_InteresesMoratoriosAnterior < 0)
                                        {
                                            ldec_InteresesMoratoriosAnterior = ldec_InteresesMoratoriosAnterior * -1;
                                            larrdec_MontosAbajo[2] = ldec_InteresesMoratoriosAnterior;
                                        }
                                        else
                                            larrdec_MontosArriba[2] = ldec_InteresesMoratoriosAnterior;

                                        if (ldec_CostasAnterior < 0)
                                        {
                                            ldec_CostasAnterior = ldec_CostasAnterior * -1;
                                            larrdec_MontosAbajo[3] = ldec_CostasAnterior;
                                        }
                                        else
                                            larrdec_MontosArriba[3] = ldec_CostasAnterior;

                                        if (ldec_DanoMoralAnterior < 0)
                                        {
                                            ldec_DanoMoralAnterior = ldec_DanoMoralAnterior * -1;
                                            larrdec_MontosAbajo[4] = ldec_DanoMoralAnterior;
                                        }
                                        else
                                            larrdec_MontosArriba[4] = ldec_DanoMoralAnterior;
                                    }
                                }

                                lint_CantidadLineasAsiento = 8;
                                lstr_Resultado = EnviarAsientos(lstr_IdExpediente, lstr_IdSociedad, lstr_AsientosResultado, lstr_IdModulo, "CT34", lstr_Transaccion, lstr_Leyenda, lbool_cambioMonto, larrdec_MontosAbajo, lint_CantidadLineasAsiento, ldec_DifMontoPrincipal, ldec_DifMontoIntereses, null, null, null, null);

                                lstr_Resultado = EnviarAsientos(lstr_IdExpediente, lstr_IdSociedad, lstr_AsientosResultado, lstr_IdModulo, "CT13", lstr_Transaccion, lstr_Leyenda, lbool_cambioMonto, larrdec_MontosArriba, lint_CantidadLineasAsiento, ldec_DifMontoPrincipal, ldec_DifMontoIntereses, null, null, null, null);
                                    
                                Console.WriteLine(lstr_Resultado[1]);
                                Console.WriteLine(lstr_Resultado[2]);
                                Console.WriteLine("------------------------------------------------");

                                str_Mensaje += lstr_Resultado[1] + Environment.NewLine;
                                str_Mensaje += lstr_Resultado[2] + Environment.NewLine;
                                str_Mensaje += "------------------------------------------------" + Environment.NewLine;
                            }
                            else if (lstr_TipoExpediente.Contains("Demandado") &&
                            (lstr_EstadoResolucion.Contains("En Firme") || lstr_EstadoResolucion.Contains("Liquidacion")) &&
                                ( 5 < DateTime.Today.Month ))
                            {
                                lstr_AsientosResultado = String.Empty;

                            if((ldec_MontoPrincipalColones != 0) && (ldec_MontoInteresesColones != 0))
                                {
                                    larrdec_MontosArriba[0] = ldec_MontoPrincipalColones * Convert.ToDecimal( lint_Porcentaje );
                                    larrdec_MontosArriba[1] = ldec_MontoInteresesColonesCierre * Convert.ToDecimal( lint_Porcentaje );
                                }
                                else
                                {
                                    larrdec_MontosArriba[1] = ldec_InteresesColones * Convert.ToDecimal( lint_Porcentaje );
                                    larrdec_MontosArriba[2] = ldec_InteresesMoratoriosColones * Convert.ToDecimal( lint_Porcentaje );
                                    larrdec_MontosArriba[3] = ldec_CostasColones * Convert.ToDecimal( lint_Porcentaje );
                                    larrdec_MontosArriba[4] = ldec_DanoMoralColones * Convert.ToDecimal( lint_Porcentaje );
                                }

                                lstr_Resultado = EnviarAsientos(lstr_IdExpediente, lstr_IdSociedad, lstr_AsientosResultado, lstr_IdModulo, "CT35", lstr_Transaccion, lstr_Leyenda, lbool_cambioMonto, larrdec_MontosAbajo, lint_CantidadLineasAsiento, ldec_DifMontoPrincipal, ldec_DifMontoIntereses, null, null, null, null);
                                Console.WriteLine(lstr_Resultado[1]);
                                Console.WriteLine(lstr_Resultado[2]);
                                Console.WriteLine("------------------------------------------------");

                                str_Mensaje += lstr_Resultado[1] + Environment.NewLine;
                                str_Mensaje += lstr_Resultado[2] + Environment.NewLine;
                                str_Mensaje += "------------------------------------------------" + Environment.NewLine;
                            }
                            if (lstr_Resultado.Contains("Contabilizado"))
                            {
                                lstr_Resultado[0] = "exito";
                            }
                            else
                            {
                                lstr_Resultado[0] = "fallo";
                            }
                            #endregion
                            
                            Int32 lint_IdRes = ConsultarIdRes(lstr_IdExpediente, lstr_IdSociedad, lstr_EstadoResolucion);

                            try
                            {
                                #region Registro
                                lstr_ResultadoRegistro = //ws_SGService.uwsModificarCobrosPagos(
                                    reg_clsCobrosPagos.ModificarCobrosPagos(
                                    lstr_IdExpediente, lstr_IdSociedad, lint_IdRes, lstr_Moneda, ldec_TipoCambioActual,
                                    ldec_TipoCambio, ldec_Tbp, ldec_Tiempo,
                                    ldec_MontoPrincipal, ldec_MontoPrincipalColones, ldec_MontoPrincipalCierre,
                                    ldec_MontoIntereses, ldec_MontoInteresesColones, ldec_MontoInteresesColonesCierre,
                                    0, 0, 0,
                                    0, 0, 0,
                                    ldec_Intereses, ldec_InteresesColones, ldec_InteresesColonesCierre,
                                    ldec_InteresesMoratorios, ldec_InteresesMoratoriosColones, ldec_InteresesMoratoriosColonesCierre,
                                    ldec_Costas, ldec_CostasColones, ldec_CostasColonesCierre,
                                    ldec_DanoMoral, ldec_DanoMoralColones, ldec_DanoMoralColonesCierre,
                                    ldec_MontoPrincipalAnterior, ldec_MontoInteresesAnterior,
                                    ldec_InteresesAnterior, ldec_InteresesMoratoriosAnterior,
                                    ldec_CostasAnterior, ldec_DanoMoralAnterior,

                                    "d", "g");

                                //lstr_ResultadoRegistro = ws_SGService.uwsRegistrarCobrosPagos("",
                                //    lstr_IdExpediente, lint_IdRes, lstr_Moneda, ldec_TipoCambioActual,ldec_Tbp,
                                //    ldec_TipoCambio, 0,
                                //    ldec_MontoPrincipal, ldec_MontoPrincipalColones, ldec_MontoPrincipalCierre,
                                //    ldec_MontoIntereses, ldec_MontoInteresesColones, ldec_MontoInteresesColonesCierre,
                                //    0, 0, 0,
                                //    0, 0, 0,
                                //    ldec_Intereses, ldec_InteresesColones, ldec_InteresesColonesCierre,
                                //    ldec_InteresesMoratorios, ldec_InteresesMoratoriosColones, ldec_InteresesMoratoriosColonesCierre,
                                //    ldec_Costas, ldec_CostasColones, ldec_CostasColonesCierre,
                                //    ldec_DanoMoral, ldec_DanoMoralColones, ldec_DanoMoralColonesCierre,
                                //    0,0,0,0,
                                    
                                //    "tipotra", "estadotran",DateTime.Today, "observe", "user");

                                if (lstr_ResultadoRegistro[0].Contains("00"))
                                {
                                    lstr_Resultado[0] = "exito";
                                    Console.WriteLine("Expediente " + lstr_IdExpediente + " modificado con éxito.");

                                    str_Mensaje += "Expediente " + lstr_IdExpediente + " modificado con éxito." + Environment.NewLine;
                                }
                                else
                                {
                                    lstr_Resultado[0] = "fallo";
                                    Console.WriteLine("Fallo al modificar expediente " + lstr_IdExpediente + ". \n" + lstr_ResultadoRegistro[1]);

                                    str_Mensaje += "Fallo al modificar expediente " + lstr_IdExpediente + ". \n" + lstr_ResultadoRegistro[1] + Environment.NewLine;
                                }
                                #endregion
                            }
                            catch { }
                            Console.WriteLine("------------------------------------------------");
                            str_Mensaje += "------------------------------------------------" + Environment.NewLine;
                        }

                        #endregion
                    }
                }
                        
                Console.WriteLine("Fin de Proceso");
                Console.WriteLine("------------------------------------------------");

                str_Mensaje += "Fin de Proceso\n";
                str_Mensaje += "------------------------------------------------" + Environment.NewLine;

                GuardarResultadoContabilizacion();

            }
        }
           

        public static void GuardarResultadoContabilizacionOld()
        {
            string path = @"C:\Logs\DiferencialCambiarioContingentes\LogDifCambiario.txt";

            // This text is added only once to the file.
            if (!File.Exists(path))
            {
                // Create a file to write to.
                File.WriteAllText(path, str_Mensaje);
            }
            string readText = File.ReadAllText(path);
            //Console.WriteLine(readText);
        
        }

        private static string[] CargarIndicadoresEco()
        {
            DataSet resultCompraUSD = new DataSet();
            DataSet resultVentaUSD = new DataSet();
            DataSet resultCompraEU = new DataSet();
            DataSet resultVentaEU = new DataSet();
            DataSet resultTBP = new DataSet();
            String[] resultado = new String[4];

            resultCompraUSD = reg_TiposCambio.ConsultarTiposCambio("CRCN", DateTime.Today, "3280", "N");//compra antes: 317 //ws_SGService.uwsConsultarTiposCambio("CRCN", DateTime.Today, "3280", "N");//compra antes: 317
            resultVentaUSD = reg_TiposCambio.ConsultarTiposCambio("CRCN", DateTime.Today, "3140", "N");//venta antes: 318
            resultCompraEU = reg_TiposCambio.ConsultarTiposCambio("EUR", DateTime.Now, "333", "N");//euro
            resultTBP = reg_ValoresIndicadoresEco.ConsultarValoresIndicadoresEco("TBP", DateTime.Now, "N");//TBP//ws_SGService.uwsConsultarValoresIndicadoresEco("TBP", DateTime.Now,"N");//TBP

            resultado[0] = String.Format("{0:0.00}", resultCompraUSD.Tables[0].Rows.Count > 0 ? resultCompraUSD.Tables[0].Rows[0]["Valor"] : "00.00"); //condition ? first_expression : second_expression;
            resultado[1] = String.Format("{0:0.00}", resultVentaUSD.Tables[0].Rows.Count > 0 ? resultVentaUSD.Tables[0].Rows[0]["Valor"] : "00.00");
            resultado[2] = String.Format("{0:0.00}", resultCompraEU.Tables[0].Rows.Count > 0 ? resultCompraEU.Tables[0].Rows[0]["Valor"] : "00.00");
            resultado[3] = String.Format("{0:0.00}", resultTBP.Tables[0].Rows.Count > 0 ? resultTBP.Tables[0].Rows[0]["Valor"] : "00.00");
            return resultado;

        }

        private static string ConsultarTipoProcesoExpediente(string idExpediente)
        {
            Conexion nuevaConexion = new Conexion();
            string ds = string.Empty;
            string consult = "Select TipoProcesoExpediente from co.Expedientes where IdExpediente='" + idExpediente + "'";
            DataTable dt2 = nuevaConexion.GetData(consult);
            DataRow campo = null;
            string tipoProceso = string.Empty;
            if (dt2.Rows.Count > 0)
            {
                campo = dt2.Rows[0];
                tipoProceso = campo["TipoProcesoExpediente"].ToString();
            }

            return tipoProceso;
        }

        private static string ConsultarTipoExpediente(string idexpediente)
        {
            Conexion nuevaConexion = new Conexion();
            string str_consul = "Select TipoExpediente from co.Expedientes where IdExpediente='" + idexpediente + "'";
            string tipoExp = string.Empty;
            //Consultar Expedientes
            DataTable exped = nuevaConexion.GetData(str_consul);
            if (exped.Rows.Count > 0)
            {
                DataRow campo = exped.Rows[0];
                tipoExp = campo["TipoExpediente"].ToString();
            }
            else
            {

                tipoExp = "No hay valores encontrados";
            }

            return tipoExp;
        }

        private static Int32 ConsultarIdRes(string str_idexpediente, String str_IdSociedad, String str_EstadoResolucion)
        {
            Conexion nuevaConexion = new Conexion();
            String str_consul = "SELECT res.IdRes FROM co.Expedientes exp " +
                "INNER JOIN co.Resoluciones res " +
                "ON exp.IdExp = res.IdExp " +
                "WHERE exp.IdExpediente = '" + str_idexpediente + "' " +
                "AND exp.IdSociedadGL = '" + str_IdSociedad + "' " +
                "AND exp.EstadoExpediente = 'Activo' " +
                "AND res.EstadoResolucion = '" + str_EstadoResolucion + "'";
                
            Int32 lint_IdRes;

            DataTable exped = nuevaConexion.GetData(str_consul);
            if (exped.Rows.Count > 0)
            {
                DataRow campo = exped.Rows[0];
                lint_IdRes = Convert.ToInt32( campo["IdRes"].ToString());
            }
            else
            {

                lint_IdRes = 0;
            }

            return lint_IdRes;
        }

        private static DataSet ConsultarTiposAsientos(String str_Sociedad, string str_modulo, string str_operacion, string str_tipoProcesoExpediente)
        {
            DataSet ds;
            DataSet lds_TirasAsientos;
            String lstr_SociedadFi = string.Empty;
            Conexion nuevaConexion = new Conexion();

            String lstr_ConsultaSociedades = "SELECT IdSociedadFi from ma.SociedadesFinancieras " +
            "WHERE IdSociedadGL='" + str_Sociedad + "'";

            DataTable lds_NombreSociedades = nuevaConexion.GetData(lstr_ConsultaSociedades);
            DataRow ldr_NombreSociedad = null;

            if (lds_NombreSociedades.Rows.Count > 0)
            {
                ldr_NombreSociedad = lds_NombreSociedades.Rows[0];
                lstr_SociedadFi = ldr_NombreSociedad["IdSociedadFi"].ToString();
            }


            lds_TirasAsientos = reg_TiposAsiento.ConsultarTiposAsiento(lstr_SociedadFi, str_modulo, str_operacion, "", "", "CRC", str_tipoProcesoExpediente, null, null);

            return lds_TirasAsientos;

        }

        private static DataSet ConsultarMontosExpedientes(string lstr_IdExpediente,
            string lstr_IdSociedadGL,
            int lint_IdExp,
            int lint_IdRes,
            string lstr_EstadoResolucion,
            DateTime? ldt_FchInicio,
            DateTime? ldt_FchFin)
        {
            DataSet ds_CobrosPagos = new DataSet();
            ds_CobrosPagos = lresoluciones.ConsultarCobrosPagos(lstr_IdExpediente, lstr_IdSociedadGL, lint_IdExp, lint_IdRes, lstr_EstadoResolucion, ldt_FchInicio, ldt_FchFin);

            return ds_CobrosPagos;
        }

        private static string ConsultarOpcionesCatalogos(string tipoProcesoExpediente)
        {
            Conexion nuevaConexion = new Conexion();
            string ds = string.Empty;
            string consult = "Select ValOpcion,NomOpcion from ma.OpcionesCatalogos where IdCatalogo='30' and NomOpcion='" + tipoProcesoExpediente + "'";
            DataTable dt2 = nuevaConexion.GetData(consult);
            DataRow campo = null;
            string tipoProceso = string.Empty;
            if (dt2.Rows.Count > 0)
            {
                campo = dt2.Rows[0];
                tipoProceso = campo["ValOpcion"].ToString();
            }

            return tipoProceso;
        }

        private static string ConsultarClaseDocumento(string str_modulo, string str_operacion)
        {
            Conexion nuevaConexion = new Conexion();
            DataSet ds;
            string consult = "SELECT IdClaseDoc FROM [ma].[Operaciones] where IdModulo='CT' and IdOperacion='" + str_operacion + "'";
            DataTable dt2 = nuevaConexion.GetData(consult);
            DataRow campo = null;
            string clasDoc = string.Empty;
            if (dt2.Rows.Count > 0)
            {
                campo = dt2.Rows[0];
                clasDoc = campo["IdClaseDoc"].ToString();
            }

            return clasDoc;

        }

        private static String[] EnviarAsientos(String str_IdExpediente, String str_Sociedad, String str_AsientosResultado,
            String str_IdModulo, String str_IdOperacion,
            String str_Trasaccion, String str_Leyenda, Boolean lbool_cambio, Decimal[] arrdec_Montos,
            Int32 int_CantidadLineasAsiento,
            Decimal MontoPricipalColones, Decimal MontoInteresColones,
            Nullable<Decimal> MontoPrincipalSegundo, Nullable<Decimal> MontoInteresSegundo,
            Nullable<Decimal> MontoPrincipalDiferencia, Nullable<Decimal> MontoInteresDiferencia)
        {
            #region Variables
            //SGDiferencialCambiario.wsAsientos.ZfiAsiento item_asiento = new SGDiferencialCambiario.wsAsientos.ZfiAsiento();
            //SGDiferencialCambiario.wsAsientos.ZfiAsiento item_asiento2 = new SGDiferencialCambiario.wsAsientos.ZfiAsiento();
            //SGDiferencialCambiario.wsAsientos.ZfiAsiento[] tabla_asientos = new SGDiferencialCambiario.wsAsientos.ZfiAsiento[int_CantidadLineasAsiento];
            LogicaNegocio.wrSigafAsientos.ZfiAsiento item_asiento = new LogicaNegocio.wrSigafAsientos.ZfiAsiento();
            LogicaNegocio.wrSigafAsientos.ZfiAsiento item_asiento2 = new LogicaNegocio.wrSigafAsientos.ZfiAsiento();
            LogicaNegocio.wrSigafAsientos.ZfiAsiento[] tabla_asientos = new LogicaNegocio.wrSigafAsientos.ZfiAsiento[int_CantidadLineasAsiento];

            String[] item_resAsientosLog = new String[10];
            String lstr_logAsiento = String.Empty;
            String[] lstr_Resultado = new String[3];
            String lstr_Montos = String.Empty;

            String lstr_TipoProcesoTexto = String.Empty;
            String lstr_TipoProceso_CodAux2 = String.Empty;

            String lstr_idTira_CodAux3 = String.Empty;
            String lstr_clsDocumento_CodAux4 = String.Empty;

            String lstr_ClaveContable = String.Empty;
            String lstr_ClaveContable2 = String.Empty;

            Int32 lint_cantLineasAsiento = 0;
            Int32 lint_Contador = 0;
            Int32 lint_cantTiras = 0;
            Int32 lint_contMonto = 0;

            DateTime ldt_FechaContabilizacion = DateTime.Now;

            Decimal ldec_Monto = 0;
            String str_Usuario = "ProcesoMensual";

            #endregion

            Boolean lbool_continuar = false;
            for (int j = 0; j < arrdec_Montos.Count(); j++)
            {
                if (arrdec_Montos[j] > 0)
                {
                    lbool_continuar = true;
                }
            }

            if (lbool_continuar)
            {
                //Tipo de proceso
                lstr_TipoProcesoTexto = ConsultarTipoProcesoExpediente(str_IdExpediente);
                lstr_TipoProceso_CodAux2 = ConsultarOpcionesCatalogos(lstr_TipoProcesoTexto);
                lstr_clsDocumento_CodAux4 = ConsultarClaseDocumento(str_IdModulo, str_IdOperacion);

                //Obtenemos tira de asientos configuradas en el gestor
                DataSet lds_TirasAsientos = ConsultarTiposAsientos(str_Sociedad, str_IdModulo, str_IdOperacion, lstr_TipoProceso_CodAux2);
                DataTable ldt_TirasAsiento = null;

                lint_cantTiras = lds_TirasAsientos.Tables[0].Rows.Count;

                if (lint_cantTiras > 0)
                {
                    ldt_TirasAsiento = lds_TirasAsientos.Tables[0];

                    //Sacar datos de tiras asientos
                    if ((lint_cantTiras == 2) && !lbool_cambio)
                    {
                        Int32 lint_cont = 0;

                        #region caso simple
                        foreach (DataRow ldr_TiraAsiento in ldt_TirasAsiento.Rows)
                        {
                            //Segun monto a enviar a SIGAF para contabilizar asiento de provision 
                            lstr_idTira_CodAux3 = ldr_TiraAsiento["CodigoAuxiliar3"].ToString();
                            switch (lstr_idTira_CodAux3.Trim())
                            {
                                case "1"://Monto Principal
                                    if (MontoPricipalColones != 0)
                                    {
                                        ////Llenamos los asientos
                                        item_asiento = new LogicaNegocio.wrSigafAsientos.ZfiAsiento();
                                        item_asiento.Xblnr = "REF";
                                        item_asiento.Bktxt = "Texto_Cabecera";
                                        item_asiento.Blart = ldr_TiraAsiento["CodigoAuxiliar4"].ToString().Trim();//Clase de documento
                                        item_asiento.Bukrs = ldr_TiraAsiento["Codigo"].ToString().Trim();//Sociedad
                                        item_asiento.Bldat = ldt_FechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de documento
                                        item_asiento.Budat = ldt_FechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de contabilización
                                        item_asiento.Xref1Hd = str_IdExpediente;//numero expediente 
                                        item_asiento.Xref2Hd = str_IdOperacion + "-" + lstr_TipoProceso_CodAux2;//CT01-AG operacion+codigoprocesal expediente


                                        item_asiento.Waers = ldr_TiraAsiento["CodigoAuxiliar"].ToString().Trim();//Moneda 
                                        item_asiento.Bschl = ldr_TiraAsiento["IdClaveContable"].ToString().Trim();//Clave de contabilización
                                        item_asiento.Hkont = ldr_TiraAsiento["IdCuentaContable"].ToString().Trim();//Cuenta de mayor
                                        item_asiento.Wrbtr = MontoPricipalColones;//Importe o monto en colones a contabilizar 

                                        lstr_Montos = lstr_Montos + lint_cont++ + ". 40: " + MontoPricipalColones + "\n";

                                        item_asiento.Zuonr = "Asig_1";
                                        item_asiento.Sgtxt = "SG-Liquidacion";
                                        item_asiento.Projk = ldr_TiraAsiento["IdElementoPEP"].ToString().TrimEnd();
                                        item_asiento.Fipex = ldr_TiraAsiento["IdPosPre"].ToString().TrimEnd();//Posición presupuestaria
                                        item_asiento.Kostl = ldr_TiraAsiento["IdCentroCosto"].ToString();
                                        item_asiento.Fistl = ldr_TiraAsiento["IdCentroGestor"].ToString();
                                        item_asiento.Prctr = ldr_TiraAsiento["IdCentroBeneficio"].ToString();
                                        item_asiento.Measure = ldr_TiraAsiento["IdPrograma"].ToString().TrimEnd();//Programa presupuestario
                                        item_asiento.Geber = ldr_TiraAsiento["IdFondo"].ToString().Trim();//Fondo
                                        item_asiento.Fkber = "";
                                        item_asiento.Xref2 = "";
                                        tabla_asientos[0] = item_asiento;
                                        ///***************************************************Cargar cuenta 50 DEBE*****************************************************/
                                        item_asiento2 = new LogicaNegocio.wrSigafAsientos.ZfiAsiento();
                                        item_asiento2.Waers = ldr_TiraAsiento["CodigoAuxiliar"].ToString().Trim();//Moneda 
                                        item_asiento2.Bschl = ldr_TiraAsiento["IdClaveContable2"].ToString().Trim();//Clave de contabilización
                                        item_asiento2.Hkont = ldr_TiraAsiento["IdCuentaContable2"].ToString().Trim();//Cuenta de mayor
                                        item_asiento2.Wrbtr = MontoPricipalColones;//Importe o monto en colones a contabilizar
                                        lstr_Montos = lstr_Montos + lint_cont++ + ". 50: " + MontoPricipalColones + "\n";

                                        item_asiento2.Zuonr = "";
                                        item_asiento2.Sgtxt = "SG-Provision diario";
                                        item_asiento2.Fipex = ldr_TiraAsiento["IdPosPre2"].ToString().TrimEnd();//Posición presupuestaria
                                        item_asiento2.Kostl = ldr_TiraAsiento["IdCentroCosto2"].ToString();
                                        item_asiento2.Fistl = ldr_TiraAsiento["IdCentroGestor2"].ToString();
                                        item_asiento2.Prctr = ldr_TiraAsiento["IdCentroBeneficio2"].ToString();
                                        item_asiento2.Geber = ldr_TiraAsiento["IdFondo2"].ToString().Trim();//Fondo
                                        item_asiento2.Fkber = "";
                                        item_asiento2.Xref2 = "xref2";
                                        tabla_asientos[1] = item_asiento2;
                                    }
                                    break;
                                case "2"://Monto Intereses
                                    if (MontoInteresColones != 0)
                                    {
                                        item_asiento = new LogicaNegocio.wrSigafAsientos.ZfiAsiento();
                                        ///***************************************************Cargar cuenta 40 HABER*****************************************************/
                                        if (MontoPricipalColones == 0)
                                        {
                                            item_asiento.Xblnr = "REF";
                                            item_asiento.Bktxt = "Texto_Cabecera";
                                            item_asiento.Blart = ldr_TiraAsiento["CodigoAuxiliar4"].ToString().Trim();//Clase de documento
                                            item_asiento.Bukrs = ldr_TiraAsiento["Codigo"].ToString().Trim();//Sociedad
                                            item_asiento.Bldat = ldt_FechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de documento
                                            item_asiento.Budat = ldt_FechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de contabilización
                                            item_asiento.Waers = ldr_TiraAsiento["CodigoAuxiliar"].ToString().Trim();//Moneda 


                                            item_asiento.Xref1Hd = str_IdExpediente;//numero expediente 
                                            item_asiento.Xref2Hd = str_IdOperacion + "-" + lstr_TipoProceso_CodAux2;//CT01-AG operacion+codigoprocesal expediente

                                        }

                                        item_asiento.Bschl = ldr_TiraAsiento["IdClaveContable"].ToString().Trim();//Clave de contabilización
                                        item_asiento.Hkont = ldr_TiraAsiento["IdCuentaContable"].ToString().Trim();//Cuenta de mayor
                                        item_asiento.Wrbtr = MontoInteresColones;//Importe o monto en colones a contabilizar 

                                        lstr_Montos = lstr_Montos + lint_cont++ + ". 40: " + MontoInteresColones + "\n";

                                        item_asiento.Zuonr = "Asig_1";
                                        item_asiento.Sgtxt = "SG-Provision";
                                        item_asiento.Projk = ldr_TiraAsiento["IdElementoPEP"].ToString().TrimEnd();
                                        item_asiento.Fipex = ldr_TiraAsiento["IdPosPre"].ToString().TrimEnd();//Posición presupuestaria
                                        item_asiento.Kostl = ldr_TiraAsiento["IdCentroCosto"].ToString();
                                        item_asiento.Fistl = ldr_TiraAsiento["IdCentroGestor"].ToString();
                                        item_asiento.Prctr = ldr_TiraAsiento["IdCentroBeneficio"].ToString();
                                        item_asiento.Measure = ldr_TiraAsiento["IdPrograma"].ToString().TrimEnd();//Programa presupuestario
                                        item_asiento.Geber = ldr_TiraAsiento["IdFondo"].ToString().Trim();//Fondo
                                        item_asiento.Fkber = "";
                                        item_asiento.Xref2 = "";
                                        if (MontoPricipalColones == 0)
                                        {
                                            tabla_asientos[0] = item_asiento;
                                        }
                                        else
                                            tabla_asientos[2] = item_asiento;
                                        ///***************************************************Cargar cuenta 50 DEBE*****************************************************/
                                        item_asiento2 = new LogicaNegocio.wrSigafAsientos.ZfiAsiento();
                                        item_asiento2.Waers = ldr_TiraAsiento["CodigoAuxiliar"].ToString().Trim();//Moneda 
                                        item_asiento2.Bschl = ldr_TiraAsiento["IdClaveContable2"].ToString().Trim();//Clave de contabilización
                                        item_asiento2.Hkont = ldr_TiraAsiento["IdCuentaContable2"].ToString().Trim();//Cuenta de mayor
                                        item_asiento2.Wrbtr = MontoInteresColones;//Importe o monto en colones a contabilizar
                                        lstr_Montos = lstr_Montos + lint_cont++ + ". 50: " + MontoInteresColones + "\n";


                                        item_asiento2.Projk = ldr_TiraAsiento["IdElementoPEP2"].ToString().TrimEnd();
                                        item_asiento2.Fipex = ldr_TiraAsiento["IdPosPre2"].ToString().TrimEnd();//Posición presupuestaria
                                        item_asiento2.Kostl = ldr_TiraAsiento["IdCentroCosto2"].ToString();
                                        item_asiento2.Fistl = ldr_TiraAsiento["IdCentroGestor2"].ToString();
                                        item_asiento2.Prctr = ldr_TiraAsiento["IdCentroBeneficio2"].ToString();
                                        item_asiento2.Measure = ldr_TiraAsiento["IdPrograma2"].ToString().TrimEnd();//Programa presupuestario
                                        item_asiento2.Zuonr = "Asig_2";
                                        item_asiento2.Sgtxt = "SG-Liquidacion";//char 50
                                        item_asiento2.Geber = ldr_TiraAsiento["IdFondo2"].ToString().Trim();//Fondo
                                        item_asiento2.Fkber = "";
                                        item_asiento2.Xref2 = "xref2";
                                        if (MontoPricipalColones == 0)
                                        {
                                            tabla_asientos[1] = item_asiento2;
                                        }
                                        else
                                            tabla_asientos[3] = item_asiento2;
                                    }
                                    break;
                            }
                        }
                        #endregion
                    }
                    else if (lint_cantTiras >= 2)
                    {
                        lint_Contador = 0;
                        Int32 lint_index = 0;

                        #region casos Complicados
                        foreach (DataRow ldr_TiraAsiento in ldt_TirasAsiento.Rows)
                        {
                            lint_index = ldt_TirasAsiento.Rows.IndexOf(ldr_TiraAsiento);

                            lstr_idTira_CodAux3 = ldr_TiraAsiento["CodigoAuxiliar3"].ToString();
                            lstr_ClaveContable = ldr_TiraAsiento["IdClaveContable"].ToString().Trim();
                            lstr_ClaveContable2 = ldr_TiraAsiento["IdClaveContable2"].ToString().Trim();

                            if ((lint_Contador == 0) && (arrdec_Montos[lint_contMonto] == 0))
                            {
                                lint_contMonto++;
                                continue;
                            }
                            if (lint_Contador == int_CantidadLineasAsiento)
                                break;
                            //if (lint_cantTiras == lint_Contador)
                            //    break;
                            else if ((lint_cantTiras == 4) && (MontoPricipalColones != 0) && (MontoInteresColones == 0))
                            {
                                if ((lint_index == 1) || (lint_index == 3))
                                    continue;
                            }
                            else if ((lint_cantTiras == 4) && (MontoPricipalColones == 0) && (MontoInteresColones != 0))
                            {
                                if ((lint_index == 0) || (lint_index == 2))
                                    continue;
                            }
                            else if ((lint_cantTiras == 6) && (MontoPricipalColones != 0) && (MontoInteresColones == 0))
                            {
                                if ((lint_index == 1) || (lint_index == 3) || (lint_index == 5))
                                    continue;
                            }
                            else if ((lint_cantTiras == 6) && (MontoPricipalColones == 0) && (MontoInteresColones != 0))
                            {
                                if ((lint_index == 0) || (lint_index == 2) || (lint_index == 4))
                                    continue;
                            }

                            ldec_Monto = arrdec_Montos[lint_contMonto++];

                            if ((lstr_ClaveContable.Equals("40") && lstr_ClaveContable2.Equals("50")))
                            {
                                item_asiento = new LogicaNegocio.wrSigafAsientos.ZfiAsiento();
                                #region cabecera
                                if (lint_Contador == 0)
                                {
                                    item_asiento.Blart = ldr_TiraAsiento["CodigoAuxiliar4"].ToString().Trim();//Clase de documento
                                    item_asiento.Bukrs = ldr_TiraAsiento["Codigo"].ToString().Trim();//Sociedad
                                    item_asiento.Bldat = ldt_FechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de documento
                                    item_asiento.Budat = ldt_FechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de contabilización
                                    item_asiento.Xblnr = "REF";
                                    item_asiento.Bktxt = "Texto_Cabecera";
                                    item_asiento.Xref1Hd = str_IdExpediente;//numero expediente 
                                    item_asiento.Xref2Hd = str_IdOperacion + "-" + lstr_TipoProceso_CodAux2;//CT01-AG operacion+codigoprocesal expediente
                                }
                                #endregion

                                #region debe 40
                                item_asiento.Wrbtr = ldec_Monto;//Importe o monto en colones a contabilizar 
                                lstr_Montos = lstr_Montos + (lint_Contador + 1) + ". 40: " + ldec_Monto + "\n";
                                item_asiento.Sgtxt = "SG-Liquidacion";
                                item_asiento.Zuonr = "Asig_1";
                                item_asiento.Fkber = "";
                                item_asiento.Xref2 = "";
                                item_asiento.Waers = ldr_TiraAsiento["CodigoAuxiliar"].ToString().Trim();//Moneda 
                                item_asiento.Bschl = ldr_TiraAsiento["IdClaveContable"].ToString().Trim();//Clave de contabilización
                                item_asiento.Hkont = ldr_TiraAsiento["IdCuentaContable"].ToString().Trim();//Cuenta de mayor
                                item_asiento.Kostl = ldr_TiraAsiento["IdCentroCosto"].ToString();
                                item_asiento.Fistl = ldr_TiraAsiento["IdCentroGestor"].ToString();
                                item_asiento.Prctr = ldr_TiraAsiento["IdCentroBeneficio"].ToString();
                                item_asiento.Geber = ldr_TiraAsiento["IdFondo"].ToString().Trim();//Fondo
                                item_asiento.Projk = ldr_TiraAsiento["IdElementoPEP"].ToString().TrimEnd();
                                item_asiento.Fipex = ldr_TiraAsiento["IdPosPre"].ToString().TrimEnd();//Posición presupuestaria
                                item_asiento.Measure = ldr_TiraAsiento["IdPrograma"].ToString().TrimEnd();
                                if (lint_Contador == 0)
                                {
                                    tabla_asientos[lint_Contador] = item_asiento;
                                    lint_Contador++;
                                }
                                else
                                    tabla_asientos[lint_Contador++] = item_asiento;

                                #endregion

                                item_asiento2 = new LogicaNegocio.wrSigafAsientos.ZfiAsiento();
                                #region 50 haber
                                //if (lint_cantTiras == lint_Contador)
                                //    break;
                                if (str_IdOperacion.Contains("CT09") && (lint_Contador == 3))
                                    break;
                                if ((lbool_cambio) && (lint_Contador < 2))
                                {
                                    ldec_Monto = arrdec_Montos[lint_contMonto++];
                                }
                                item_asiento2.Wrbtr = ldec_Monto;//Importe o monto en colones a contabilizar
                                lstr_Montos = lstr_Montos + (lint_Contador + 1) + ". 50: " + ldec_Monto + "\n";
                                item_asiento2.Sgtxt = "SG-Provision diario";
                                item_asiento2.Zuonr = "";
                                item_asiento2.Fkber = "";
                                item_asiento2.Xref2 = "xref2";
                                item_asiento2.Waers = ldr_TiraAsiento["CodigoAuxiliar"].ToString().Trim();//Moneda 
                                item_asiento2.Bschl = ldr_TiraAsiento["IdClaveContable2"].ToString().Trim();//Clave de contabilización
                                item_asiento2.Hkont = ldr_TiraAsiento["IdCuentaContable2"].ToString().Trim();//Cuenta de mayor
                                item_asiento2.Kostl = ldr_TiraAsiento["IdCentroCosto2"].ToString();
                                item_asiento2.Fistl = ldr_TiraAsiento["IdCentroGestor2"].ToString();
                                item_asiento2.Prctr = ldr_TiraAsiento["IdCentroBeneficio2"].ToString();
                                item_asiento2.Geber = ldr_TiraAsiento["IdFondo2"].ToString().Trim();//Fondo
                                item_asiento2.Projk = ldr_TiraAsiento["IdElementoPEP2"].ToString().TrimEnd();
                                item_asiento2.Fipex = ldr_TiraAsiento["IdPosPre2"].ToString().TrimEnd();//Posición presupuestaria
                                item_asiento2.Measure = ldr_TiraAsiento["IdPrograma2"].ToString().TrimEnd();
                                tabla_asientos[lint_Contador++] = item_asiento2;
                                #endregion
                            }
                            else if (lstr_ClaveContable.Equals("40"))
                            {
                                item_asiento = new LogicaNegocio.wrSigafAsientos.ZfiAsiento();
                                #region cabecera
                                if (lint_Contador == 0)
                                {
                                    item_asiento.Blart = ldr_TiraAsiento["CodigoAuxiliar4"].ToString().Trim();//Clase de documento
                                    item_asiento.Bukrs = ldr_TiraAsiento["Codigo"].ToString().Trim();//Sociedad
                                    item_asiento.Bldat = ldt_FechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de documento
                                    item_asiento.Budat = ldt_FechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de contabilización
                                    item_asiento.Xblnr = "REF";
                                    item_asiento.Bktxt = "Texto_Cabecera";
                                    item_asiento.Xref1Hd = str_IdExpediente;//numero expediente 
                                    item_asiento.Xref2Hd = str_IdOperacion + "-" + lstr_TipoProceso_CodAux2;//CT01-AG operacion+codigoprocesal expediente
                                }
                                #endregion

                                #region debe 40
                                item_asiento.Wrbtr = ldec_Monto;//Importe o monto en colones a contabilizar 
                                lstr_Montos = lstr_Montos + (lint_Contador + 1) + ". 40: " + ldec_Monto + "\n";
                                item_asiento.Sgtxt = "SG-Liquidacion";
                                item_asiento.Zuonr = "Asig_1";
                                item_asiento.Fkber = "";
                                item_asiento.Xref2 = "";
                                item_asiento.Waers = ldr_TiraAsiento["CodigoAuxiliar"].ToString().Trim();//Moneda 
                                item_asiento.Bschl = ldr_TiraAsiento["IdClaveContable"].ToString().Trim();//Clave de contabilización
                                item_asiento.Hkont = ldr_TiraAsiento["IdCuentaContable"].ToString().Trim();//Cuenta de mayor
                                item_asiento.Kostl = ldr_TiraAsiento["IdCentroCosto"].ToString();
                                item_asiento.Fistl = ldr_TiraAsiento["IdCentroGestor"].ToString();
                                item_asiento.Prctr = ldr_TiraAsiento["IdCentroBeneficio"].ToString();
                                item_asiento.Geber = ldr_TiraAsiento["IdFondo"].ToString().Trim();//Fondo
                                item_asiento.Projk = ldr_TiraAsiento["IdElementoPEP"].ToString().TrimEnd();
                                item_asiento.Fipex = ldr_TiraAsiento["IdPosPre"].ToString().TrimEnd();//Posición presupuestaria
                                item_asiento.Measure = ldr_TiraAsiento["IdPrograma"].ToString().TrimEnd();
                                if (lint_Contador == 0)
                                {
                                    tabla_asientos[lint_Contador] = item_asiento;
                                    lint_Contador++;
                                }
                                else
                                    tabla_asientos[lint_Contador++] = item_asiento;
                                #endregion

                            }
                            else if (lstr_ClaveContable.Equals("50"))
                            {
                                item_asiento = new LogicaNegocio.wrSigafAsientos.ZfiAsiento();
                                #region cabecera
                                if (lint_Contador == 0)
                                {
                                    item_asiento.Blart = ldr_TiraAsiento["CodigoAuxiliar4"].ToString().Trim();//Clase de documento
                                    item_asiento.Bukrs = ldr_TiraAsiento["Codigo"].ToString().Trim();//Sociedad
                                    item_asiento.Bldat = ldt_FechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de documento
                                    item_asiento.Budat = ldt_FechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de contabilización
                                    item_asiento.Xblnr = "REF";
                                    item_asiento.Bktxt = "Texto_Cabecera";
                                    item_asiento.Xref1Hd = str_IdExpediente;//numero expediente 
                                    item_asiento.Xref2Hd = str_IdOperacion + "-" + lstr_TipoProceso_CodAux2;//CT01-AG operacion+codigoprocesal expediente
                                }
                                #endregion

                                #region haber 50
                                item_asiento.Wrbtr = ldec_Monto;//Importe o monto en colones a contabilizar 
                                lstr_Montos = lstr_Montos + (lint_Contador + 1) + ". 50: " + ldec_Monto + "\n";
                                item_asiento.Sgtxt = "SG-Liquidacion";
                                item_asiento.Zuonr = "Asig_1";
                                item_asiento.Fkber = "";
                                item_asiento.Xref2 = "";
                                item_asiento.Waers = ldr_TiraAsiento["CodigoAuxiliar"].ToString().Trim();//Moneda 
                                item_asiento.Bschl = ldr_TiraAsiento["IdClaveContable"].ToString().Trim();//Clave de contabilización
                                item_asiento.Hkont = ldr_TiraAsiento["IdCuentaContable"].ToString().Trim();//Cuenta de mayor
                                item_asiento.Kostl = ldr_TiraAsiento["IdCentroCosto"].ToString();
                                item_asiento.Fistl = ldr_TiraAsiento["IdCentroGestor"].ToString();
                                item_asiento.Prctr = ldr_TiraAsiento["IdCentroBeneficio"].ToString();
                                item_asiento.Geber = ldr_TiraAsiento["IdFondo"].ToString().Trim();//Fondo
                                item_asiento.Projk = ldr_TiraAsiento["IdElementoPEP"].ToString().TrimEnd();
                                item_asiento.Fipex = ldr_TiraAsiento["IdPosPre"].ToString().TrimEnd();//Posición presupuestaria
                                item_asiento.Measure = ldr_TiraAsiento["IdPrograma"].ToString().TrimEnd();
                                if (lint_Contador == 0)
                                {
                                    tabla_asientos[lint_Contador] = item_asiento;
                                    lint_Contador++;
                                }
                                else
                                    tabla_asientos[lint_Contador++] = item_asiento;
                                #endregion

                            }
                        }
                        #endregion
                    }
                    //Cargar de Asientos 
                    string[] concatenado = new string[8];
                    //envio de asiento mediante servicio web hacia SIGAF
                    try
                    {
                        item_resAsientosLog = //ws_ContabilizaAsientos.EnviarAsientos(tabla_asientos);
                            reg_TiposAsiento.EnviarAsientos(tabla_asientos);
                        Int32 lint_Length = 0;
                        for (int j = 0; j < item_resAsientosLog.Count(); j++)
                        {
                            if (item_resAsientosLog[j].Contains("[E]"))
                                lstr_Resultado[0] = "error";
                            else if (item_resAsientosLog[j].Contains("[S]"))
                            {
                                lint_Length = item_resAsientosLog[j].Length;
                                try
                                {
                                    str_AsientosResultado = str_AsientosResultado + "\n" + item_resAsientosLog[j].ToString().Substring(58, 10);
                                }
                                catch { }
                                lstr_Resultado[0] = "Contabilizado";
                                lstr_Resultado[2] = str_AsientosResultado;

                                try
                                {
                                    reg_Bita.RegistrarBitacoraDeMovimientosCuentasExpedientes(str_IdExpediente, "CT", str_Sociedad, str_IdOperacion, "", MontoPricipalColones, MontoInteresColones, 0, 0, "Provisión Monto Principal Colones- ", str_Usuario);
                                    reg_Bita.RegistrarBitacoraDeMovimientosCuentasExpedientes(str_IdExpediente, "CT", str_Sociedad, str_IdOperacion, "", MontoPricipalColones, MontoInteresColones, 0, 0, "Provisión Monto Interes Colones - ", str_Usuario);

                                    //ws_SG.uwsRegistrarCobrosPagos(gstr_IdExpediente, gstr_IdExpediente)
                                }
                                catch { }
                            }
                            else if (item_resAsientosLog[j].Contains("[I]"))
                                lstr_Resultado[0] = "info";

                            lstr_logAsiento += "\n" + (j + 1) + ": " + item_resAsientosLog[j];

                            lstr_Resultado[1] = lstr_logAsiento;
                        }

                        reg_Bit.ufnRegistrarAccionBitacora("CT", str_Usuario, "Enviar Asiento", str_IdExpediente + ":" + str_Sociedad +
                            " Operación: " + str_IdOperacion + "\n" + lstr_Montos +
                            "\nResultado: " + lstr_logAsiento,
                            str_IdExpediente, str_Trasaccion, str_Sociedad);

                        try
                        {

                            String[] lstr_AsientosResultado = new String[3];
                            Int32 lint_IdExp = 0;

                            String lstr_query = "SELECT IdExp FROM co.Expedientes exp " +
                                "WHERE exp.IdExpediente ='" + str_IdExpediente + "' " +
                                "AND exp.IdSociedadGL ='" + str_Sociedad + "' " +
                                "AND exp.EstadoExpediente = 'Activo'";

                            Conexion nuevaConexion = new Conexion();
                            DataTable dt_Resoluciones = nuevaConexion.GetData(lstr_query);

                            foreach (DataRow dr_Resolucion in dt_Resoluciones.Rows)
                            {
                                lint_IdExp = Convert.ToInt32(dr_Resolucion["IdExp"]);
                            }

                            lstr_AsientosResultado = reg_CP.RegistrarCobrosPagos(str_IdOperacion,
                                str_IdExpediente, lint_IdExp, "REV", 0, 0,
                                0, 0,
                                arrdec_Montos == null ? MontoPricipalColones : arrdec_Montos[0],
                                arrdec_Montos == null ? MontoInteresColones : arrdec_Montos[1],//Monto Pr
                                arrdec_Montos == null ? 0 : (arrdec_Montos.Count() >= 2 ? arrdec_Montos[2] : 0),
                                arrdec_Montos == null ? 0 : (arrdec_Montos.Count() >= 3 ? arrdec_Montos[3] : 0),
                                arrdec_Montos == null ? 0 : (arrdec_Montos.Count() >= 4 ? arrdec_Montos[4] : 0),
                                arrdec_Montos == null ? 0 : (arrdec_Montos.Count() >= 5 ? arrdec_Montos[5] : 0),
                                arrdec_Montos == null ? 0 : (arrdec_Montos.Count() >= 6 ? arrdec_Montos[6] : 0),
                                arrdec_Montos == null ? 0 : (arrdec_Montos.Count() >= 7 ? arrdec_Montos[7] : 0),
                                arrdec_Montos == null ? 0 : (arrdec_Montos.Count() >= 8 ? arrdec_Montos[8] : 0),
                                arrdec_Montos == null ? 0 : (arrdec_Montos.Count() >= 9 ? arrdec_Montos[9] : 0),
                                arrdec_Montos == null ? 0 : (arrdec_Montos.Count() >= 10 ? arrdec_Montos[10] : 0),
                                0,
                                0, 0, 0,//Intereses
                                0, 0, 0,
                                0, 0, 0,
                                0, 0, 0,
                                0, 0, 0, 0,//Anteriores

                                "tipotra", "estadotran", DateTime.Today, "Reversion", str_Usuario);
                        }
                        catch
                        {

                        }
                    }
                    
                    catch (Exception ex)
                    {
                        lstr_Resultado[0] = "error";
                        lstr_Resultado[1] = lstr_logAsiento + "\n" + ex.Message;

                        reg_Bit.ufnRegistrarAccionBitacora("CT", str_Usuario, "Enviar Asiento", str_IdExpediente + ":" + str_Sociedad +
                            " Operación: " + str_IdOperacion + "\n" + str_Leyenda + "\n" + lstr_Montos +
                           "\nResultado: " + lstr_Resultado,
                           str_IdExpediente, str_Trasaccion, str_Sociedad);

                        return lstr_Resultado;
                    }
                }
                else
                {
                    lstr_Resultado[0] = "error";
                    lstr_Resultado[1] = "Error: Los datos de consulta del asiento, no fue encontrada en la configuracion del Sistema Gestor.";
                }
            }
            return lstr_Resultado;

        }

    }
}
