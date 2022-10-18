using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio.CalculosFinancieros.DeudaExterna;
using LogicaNegocio.CalculosFinancieros.DeudaInterna;
using LogicaNegocio.Mantenimiento;
using LogicaNegocio.Seguridad;
using LogicaNegocio.Contingentes;
using log4net;
using log4net.Config;

namespace SGCierreContableDE
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            ILog Log = LogManager.GetLogger("SGCierreContableDE");
            LogicaNegocio.wrSigafAsientos.ZfiAsiento SigafLinea = new LogicaNegocio.wrSigafAsientos.ZfiAsiento(); //wrSigafAsientos.ZfiAsiento();
            LogicaNegocio.wrSigafAsientos.ZfiAsiento[] SigafTablaAsientoPago;
            LogicaNegocio.wrSigafAsientos.ZfiAsiento[] SigafTablaAsientoDev1;
            LogicaNegocio.wrSigafAsientos.ZfiAsiento[] SigafTablaAsientoDev2;
            LogicaNegocio.wrSigafAsientos.ZfiAsientoLog[] SigafAsientoLogPago = new LogicaNegocio.wrSigafAsientos.ZfiAsientoLog[10];
            LogicaNegocio.wrSigafAsientos.ZfiAsientoLog[] SigafAsientoLogDev1 = new LogicaNegocio.wrSigafAsientos.ZfiAsientoLog[10];
            LogicaNegocio.wrSigafAsientos.ZfiAsientoLog[] SigafAsientoLogDev2 = new LogicaNegocio.wrSigafAsientos.ZfiAsientoLog[10];
            DateTime ldt_FechaActual = DateTime.Today;//new DateTime();
            int gint_AnnoActual = ldt_FechaActual.Year;
            DataSet lds_TitulosValores;
            DataSet lds_TiposAsiento;
            clsTituloValor ltitulo = new clsTituloValor();
            
            clsTiposAsiento lasiento = new clsTiposAsiento();
            clsServicios lservicio = new clsServicios();
            clsOperaciones loperacion = new clsOperaciones();
            clsOficinas loficina = new clsOficinas();
            tSociedadGL lsociedadGL = new tSociedadGL();
            clsExpedientes lexpediente = new clsExpedientes();
            clsBitacoraDeMovimientosCuentasExpedientes reg_Bitacora = new clsBitacoraDeMovimientosCuentasExpedientes();

            string lstr_IdModulo = "IdModulo IN ('DE')";
            string lstr_Mensaje = "";
            string lstr_IdSociedadFI = "";
            string lstr_IdCeBe = "";
            string logAsientoPago = string.Empty;
            string logAsientoDev1 = string.Empty;
            string logAsientoDev2 = string.Empty;
            Boolean lbln_ErrorAsientoLinea;
            Boolean lbln_ExisteExpediente;
            Boolean lbln_TieneExpediente;
            Boolean lbln_PeriodoActual;
            try
            {

                //saco todos los Titulos en estado Pagado para ser contabilizados.
                lds_TitulosValores = ltitulo.ConsultarTituloValor("","","","","","","");

                #region Titulos
                //solo itero si el dataset tiene registros
                if (lds_TitulosValores.Tables.Count > 0)
                {

                    //itero en todos los Titulos para  irlos contabilizando
                    for (int i = 0; lds_TitulosValores.Tables["Table"].Rows.Count > i; i++)
                    {

                    
                                lbln_ErrorAsientoLinea = false;
                                /////////////////////////////////////
                                SigafTablaAsientoPago = new LogicaNegocio.wrSigafAsientos.ZfiAsiento[lds_Pagos.Tables["Table"].Rows.Count * 4];
                        
                                int lint_lineaPago = 0;
                                int lint_lineaDev1 = 0;
                                int lint_lineaDev2 = 0;
                                int lint_lineaVerifica = 0;
                                Boolean lbln_Pago = false;
                                Boolean lbln_Dev1 = false;
                                Boolean lbln_Dev2 = false;
                                /////////////////////////////////////
                                //itero en todos los Titulos para irlos contabilizando
                                                                 
                                        //lds_Operaciones = loperacion.ConsultarOperaciones("",lstr_IdModulo,string.Empty);
                                        ////solo itero si el dataset tiene registros
                                        //if (lds_Operaciones.Tables.Count > 0) {

                                        lds_TiposAsiento = lasiento.ConsultarTiposAsiento(lds_TitulosValores.Tables["Table"].Rows[i]["IdSociedadGL"].ToString(),
                                                                                          lstr_IdModulo,
                                                                                          Convert.ToString(lds_Servicios.Tables["Table"].Rows[0]["IdServicio"]),
                                                                                          string.Empty,
                                                                                          string.Empty,
                                                                                          string.Empty,
                                                                                          string.Empty,
                                                                                          string.Empty,
                                                                                          string.Empty);
                                        #region TiposAsiento
                                        //solo itero si el dataset tiene registros
                                        if (lds_TiposAsiento.Tables.Count > 0)
                                        {

                                            //En este punto ya tengo los datos del formulario, el detalle del pago y el detalle del servicio, ya puedo consumir el webservice del asiento
                                            //SigafTablaAsientoPago = new LogicaNegocio.wrSigafAsientos.ZfiAsiento[lds_TiposAsiento.Tables["Table"].Rows.Count * 2];
                                            //int lint_lineaPago = 0;
                                            //itero en todos los Titulos pagados para sacar los servicios pagados e irlos contabilizando
                                            for (int z = 0; lds_TiposAsiento.Tables["Table"].Rows.Count > z; z++)
                                            {
                                                lbln_Pago = false;
                                                lbln_Dev1 = false;
                                                lbln_Dev2 = false;
                                                //Verifico si la linea del asiento está Activa
                                                if (Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["Estado"]).Trim() == "A")
                                                {
                                                    switch (Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["CodigoAuxiliar"]).Trim())
                                                    {
                                                        case "PAGADO":
                                                            lbln_Pago = true;
                                                            lint_lineaVerifica = lint_lineaPago;
                                                            break;
                                                        case "DEVENGO":
                                                            lbln_Dev1 = true;
                                                            lint_lineaVerifica = lint_lineaDev1;
                                                            break;
                                                        case "-DEVENGO":
                                                            lbln_Dev2 = true;
                                                            lint_lineaVerifica = lint_lineaDev2;
                                                            break;
                                                        default:
                                                            break;
                                                    }



                                                    SigafLinea = new LogicaNegocio.wrSigafAsientos.ZfiAsiento();

                                                    SigafLinea.Blart = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["CodigoAuxiliar4"]).Trim(); //Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[i]["IdClaseDoc"]); //"SA"; //BLART = CLASE DE DOCUMENTO
                                                    SigafLinea.Bukrs = lstr_IdSociedadFI;//Convert.ToString(lds_TitulosValores.Tables["Table"].Rows[i]["IdSociedadGL"]).Trim();//"G206";//BUKRS = ID de Sociedad
                                                    if (lint_lineaVerifica == 0)
                                                    {
                                                        SigafLinea.Bldat = ldt_FechaActual.ToString("dd.MM.yyyy");//Convert.ToString(ldt_FechaActual);//"01.10.2015";//bldat = Fecha de Documento
                                                        SigafLinea.Budat = ldt_FechaActual.ToString("dd.MM.yyyy");//Convert.ToString(ldt_FechaActual);//"01.10.2015";//budat = Fecha de Contabilizacion
                                                    }
                                                    else
                                                    {
                                                        SigafLinea.Bldat = "";//Convert.ToString(ldt_FechaActual);//"01.10.2015";//bldat = Fecha de Documento
                                                        SigafLinea.Budat = "";//Convert.ToString(ldt_FechaActual);//"01.10.2015";//budat = Fecha de Contabilizacion
                                                    }
                                                    SigafLinea.Waers = Convert.ToString(lds_Pagos.Tables["Table"].Rows[y]["IdMoneda"]).Trim();//"CRC";//waers = IdMoneda
                                                    SigafLinea.Xblnr = Convert.ToString(lds_TitulosValores.Tables["Table"].Rows[i]["Anno"]) + "." + Convert.ToString(lds_Pagos.Tables["Table"].Rows[y]["IdFormulario"]);//"REF";//Xblnr = Referencia
                                                    SigafLinea.Bktxt = Convert.ToString(lds_Pagos.Tables["Table"].Rows[y]["IdFormulario"]); //"Texto_Cabecera";//Bktxt = "Texto_Cabecera"

                                                    //SigafLinea.Xref1Hd = ; //"REF_1";//Xref1Hd = "REF_1"
                                                    SigafLinea.Xref2Hd = "CI." + Convert.ToString(lds_TitulosValores.Tables["Table"].Rows[i]["Anno"]) + "." + Convert.ToString(lds_Pagos.Tables["Table"].Rows[y]["IdFormulario"]); //"REF_2";//Xref2Hd = "REF_2"
                                                    SigafLinea.Bschl = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdClaveContable"]).Trim(); //"40";//Bschl = Clave Contable
                                                    SigafLinea.Hkont = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCuentaContable"]).Trim(); //"5120302000";//Hkont = Cuenta Contable de Mayor
                                                    SigafLinea.Wrbtr = Convert.ToDecimal(lds_Pagos.Tables["Table"].Rows[y]["Monto"]); //1505;//Wrbtr = Monto Importe
                                                    SigafLinea.Zuonr = "";//Zuonr = "Asignacion"
                                                    SigafLinea.Sgtxt = "";//Sgtxt = "Texto"
                                                    SigafLinea.Fipex = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdPosPre"]).Trim(); //"E-10302";//Fipex = posicion presupuestaria
                                                    SigafLinea.Kostl = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCentroCosto"]).Trim(); //"20613200";//Kostl = Centro de Costo
                                                    SigafLinea.Fistl = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCentroGestor"]).Trim(); //"20613200";//Fistl = Centro Gestor
                                                    SigafLinea.Geber = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdFondo"]).Trim(); //"001";//Geber = Fondo
                                                    SigafLinea.Fkber = "";//Fkber = Area Funcional
                                                    SigafLinea.Xref2 = ""; //Xref2 = Flujo de efectivo

                                                    SigafLinea.Valut = "";//Valut = Fecha de Valor                                                  
                                                    SigafLinea.Umskz = "";//Umskz = Indicador CME
                                                    SigafLinea.Werks = "";//Werks = Centro
                                                    SigafLinea.Xmwst = ""; //Calculo de Impuesto
                                                    SigafLinea.Xref3 = "";//Flujo de Efectivo
                                                    SigafLinea.Zfbdt = "";//Fecha base
                                                    SigafLinea.Zlsch = "";//Via de pago
                                                    SigafLinea.Projk = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdElementoPEP"]).Trim(); ////Projk = Id Elemento PEP
                                                    if (string.IsNullOrEmpty(lstr_IdCeBe) || !SigafLinea.Hkont.StartsWith("4"))
                                                    {
                                                        SigafLinea.Prctr = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCentroBeneficio"]).Trim(); ////Prctr = centro de beneficio
                                                    }
                                                    else
                                                    {
                                                        SigafLinea.Prctr = lstr_IdCeBe;
                                                    }
                                                    SigafLinea.Mwskz = "";//Mwskz = indicador de impuesto
                                                    SigafLinea.Measure = "";//Measure = programa presupuestario
                                                    SigafLinea.Kursf = 0;//Kursf = tipo de cambio
                                                    SigafLinea.Aufnr = "";//Aufnr = Orden
                                                    SigafLinea.Dmbe2 = 0;//Dmbe2 = importe en moneda fuerte
                                                    SigafLinea.Kblnr = Convert.ToString(lds_Pagos.Tables["Table"].Rows[y]["IdReservaPresupuestaria"]).Trim(); //Kblnr = documento presupuestario
                                                    SigafLinea.Kblpos = "";//Kblpos = posicion documento presupuestario
                                                    SigafLinea.Rcomp = "";
                                                    SigafLinea.Buzei = "";
                                                    SigafLinea.Mandt = "";
                                                    SigafLinea.Hbkid = "";//Hbkid = banco propio

                                                    switch (Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["CodigoAuxiliar"]).Trim())
                                                    {
                                                        case "PAGADO":
                                                            SigafLinea.Xref1Hd = "PAGADO"; //"REF_1";//Xref1Hd = "REF_1"
                                                            SigafTablaAsientoPago[lint_lineaPago] = SigafLinea;
                                                            lint_lineaPago++;
                                                            break;
                                                        case "DEVENGO":
                                                            SigafLinea.Xref1Hd = "DEVENGO"; //"REF_1";//Xref1Hd = "REF_1"
                                                            SigafTablaAsientoDev1[lint_lineaDev1] = SigafLinea;
                                                            lint_lineaDev1++;
                                                            break;
                                                        case "-DEVENGO":
                                                            SigafLinea.Xref1Hd = "-DEVENGO"; //"REF_1";//Xref1Hd = "REF_1"
                                                            SigafTablaAsientoDev2[lint_lineaDev2] = SigafLinea;
                                                            lint_lineaDev2++;
                                                            break;
                                                        default:
                                                            break;
                                                    }



                                                    if (!string.IsNullOrEmpty(Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdClaveContable2"])))
                                                    {
                                                        ///cuenta 2
                                                        SigafLinea = new LogicaNegocio.wrSigafAsientos.ZfiAsiento();

                                                        SigafLinea.Blart = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["CodigoAuxiliar4"]).Trim();  //"SA"; //BLART = CLASE DE DOCUMENTO
                                                        SigafLinea.Bukrs = "";//"G206";//BUKRS = ID de Sociedad
                                                        SigafLinea.Bldat = "";//"01.10.2015";//bldat = Fecha de Documento
                                                        SigafLinea.Budat = "";//"01.10.2015";//budat = Fecha de Contabilizacion
                                                        SigafLinea.Waers = "";//"CRC";//waers = IdMoneda
                                                        SigafLinea.Xblnr = "";//"REF";//Xblnr = Referencia
                                                        SigafLinea.Bktxt = ""; //"Texto_Cabecera";//Bktxt = "Texto_Cabecera"
                                                        SigafLinea.Xref1Hd = ""; //"REF_1";//Xref1Hd = "REF_1"
                                                        SigafLinea.Xref2Hd = ""; //"REF_2";//Xref2Hd = "REF_2"
                                                        SigafLinea.Bschl = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdClaveContable2"]).Trim(); //"40";//Bschl = Clave Contable
                                                        SigafLinea.Hkont = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCuentaContable2"]).Trim(); //"5120302000";//Hkont = Cuenta Contable de Mayor
                                                        SigafLinea.Wrbtr = Convert.ToDecimal(lds_Pagos.Tables["Table"].Rows[y]["Monto"]); //1505;//Wrbtr = Monto Importe
                                                        SigafLinea.Zuonr = "";//Zuonr = "Asignacion"
                                                        SigafLinea.Sgtxt = "";//Sgtxt = "Texto"
                                                        SigafLinea.Fipex = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdPosPre2"]).Trim(); //"E-10302";//Fipex = posicion presupuestaria
                                                        SigafLinea.Kostl = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCentroCosto2"]).Trim(); //"20613200";//Kostl = Centro de Costo
                                                        SigafLinea.Fistl = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCentroGestor2"]).Trim(); //"20613200";//Fistl = Centro Gestor
                                                        SigafLinea.Geber = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdFondo2"]).Trim(); //"001";//Geber = Fondo
                                                        SigafLinea.Fkber = "";//Fkber = Area Funcional
                                                        SigafLinea.Xref2 = ""; //Xref2 = Flujo de efectivo

                                                        SigafLinea.Valut = "";//Valut = Fecha de Valor                                                  
                                                        SigafLinea.Umskz = "";//Umskz = Indicador CME
                                                        SigafLinea.Werks = "";//Werks = Centro
                                                        SigafLinea.Xmwst = ""; //Calculo de Impuesto
                                                        SigafLinea.Xref3 = "";//Flujo de Efectivo
                                                        SigafLinea.Zfbdt = "";//Fecha base
                                                        SigafLinea.Zlsch = "";//Via de pago
                                                        SigafLinea.Projk = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdElementoPEP2"]).Trim(); ////Projk = Id Elemento PEP
                                                        if (string.IsNullOrEmpty(lstr_IdCeBe) || !SigafLinea.Hkont.StartsWith("4"))
                                                        {
                                                            SigafLinea.Prctr = Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["IdCentroBeneficio2"]).Trim(); ////Prctr = centro de beneficio
                                                        }
                                                        else
                                                        {
                                                            SigafLinea.Prctr = lstr_IdCeBe;
                                                        }
                                                        SigafLinea.Mwskz = "";//Mwskz = indicador de impuesto
                                                        SigafLinea.Measure = "";//Measure = programa presupuestario
                                                        SigafLinea.Kursf = 0;//Kursf = tipo de cambio
                                                        SigafLinea.Aufnr = "";//Aufnr = Orden
                                                        SigafLinea.Dmbe2 = 0;//Dmbe2 = importe en moneda fuerte
                                                        SigafLinea.Kblnr = Convert.ToString(lds_Pagos.Tables["Table"].Rows[y]["IdReservaPresupuestaria"]).Trim();//Kblnr = documento presupuestario
                                                        SigafLinea.Kblpos = "";//Kblpos = posicion documento presupuestario
                                                        SigafLinea.Rcomp = "";
                                                        SigafLinea.Buzei = "";
                                                        SigafLinea.Mandt = "";
                                                        SigafLinea.Hbkid = "";//Hbkid = banco propio


                                                        switch (Convert.ToString(lds_TiposAsiento.Tables["Table"].Rows[z]["CodigoAuxiliar"]).Trim())
                                                        {
                                                            case "PAGADO":
                                                                SigafTablaAsientoPago[lint_lineaPago] = SigafLinea;
                                                                lint_lineaPago++;
                                                                break;
                                                            case "DEVENGO":
                                                                SigafTablaAsientoDev1[lint_lineaDev1] = SigafLinea;
                                                                lint_lineaDev1++;
                                                                break;
                                                            case "-DEVENGO":
                                                                SigafTablaAsientoDev2[lint_lineaDev2] = SigafLinea;
                                                                lint_lineaDev2++;
                                                                break;
                                                            default:
                                                                break;
                                                        }
                                                    }
                                                    //SigafAsientoLogPago = lasiento.EnviarAsientoSigaf(SigafTablaAsientoPago);
                                                }//if de estado Activo de la linea del asiento
                                            }//for de las lineas del asiento
                                        }//if asiento encontrado
                                        #endregion TiposAsiento                                   
                                /////////////////////////////////////////
                                try
                                {


                                    string[] item_resAsientosLogDev1 = new string[100];
                                    item_resAsientosLogDev1 = lasiento.EnviarAsientoSigaf(SigafTablaAsientoDev1);
                                    for (int w = 0; w < item_resAsientosLogDev1.Length; w++)
                                    {
                                        logAsientoDev1 += "\n" + i + "-" + item_resAsientosLogDev1[i];
                                    }
                                    //MessageBox.Show("Resultado de contabilización: \n\n"+logAsientoPago);
                                    Log.Info("Resultado de contabilización Devengo+: \n\n" + logAsientoDev1);

                                    

                                    //Registro de bitacora de movimientos
                                    try
                                    {

                                        item_resAsientosLogPago = reg_Bitacora.RegistrarBitacoraDeMovimientosCuentasExpedientes(
                                            Convert.ToString(lds_Pagos.Tables["Table"].Rows[i]["IdFormulario"]),
                                            "CI",
                                            lds_TitulosValores.Tables["Table"].Rows[i]["IdSociedadGL"].ToString(),
                                            Convert.ToString(lds_Pagos.Tables["Table"].Rows[i]["IdFormulario"]), //+ "." + Convert.ToString(lds_Pagos.Tables["Table"].Rows[y]["IdPago"]),
                                            "",
                                            0,
                                            0,
                                            0,
                                            0,
                                            logAsientoPago,
                                            ""
                                            );

                                    }//try registro en bitácora
                                    catch (Exception err)
                                    {
                                        item_resAsientosLogPago[0] = err.Message;

                                        Log.Error("Error al registrar en bitácora Pago: " + err.Message);

                                    }//catch

                                    
                                    if (item_resAsientosLogPago[0].Contains("Error en el documento:") 
                                        )
                                    {
                                        lbln_ErrorAsientoLinea = true;


                                    }



                                }//try interfaz
                                catch (Exception ex)
                                {
                                    lstr_Mensaje = ex.ToString();
                                    Log.Error("Error al invocar interfaz asientos Sigaf: " + lstr_Mensaje);
                                    lbln_ErrorAsientoLinea = true;
                                }//catch
                                //////////////////////////////////////
                                                    
                    }//for de Titulos
                }//if de Titulos encontrados
                #endregion Titulos
            }//try 
            catch (Exception ex)
            {
                lstr_Mensaje = ex.ToString();
                Log.Error(lstr_Mensaje);

                Console.WriteLine("Error: " + ex.ToString());
                Console.ReadLine().ToString();
            }//catch
             */
        }//main
    }//class
}//namespace
