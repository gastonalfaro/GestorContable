using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Globalization;
using Presentacion.Compartidas;
using System.Web.UI.HtmlControls;
using System.Configuration;
//using System.Data.SqlClient;
using Logica.SubirArchivo;
using System.Text;
using System.IO;

namespace Presentacion.Contingentes.ArchivosCO
{
    public class Asiento
    {
        private Presentacion.wsAsientos.ServicioContable ws_ContabilizaAsientos;
        private Presentacion.wsSG.wsSistemaGestor ws_SG;

        private String str_IdExpediente, gstr_Sociedad, gstr_Usuario,
            gstr_MensajeResultadoResoluciones, gstr_AsientosResultado;
    

        public String getAsientosResultado(){
            String temp = gstr_AsientosResultado;
            gstr_AsientosResultado = String.Empty;
            return temp;
        }

        public String getMensajeResultadoResoluciones()
        {
            String temp = gstr_MensajeResultadoResoluciones;
            gstr_MensajeResultadoResoluciones = String.Empty;
            return temp;
        }


        public Asiento()
        {
            ws_ContabilizaAsientos = new Presentacion.wsAsientos.ServicioContable();
            ws_SG = new Presentacion.wsSG.wsSistemaGestor();
        }

        public void definirExpediente(String idexp,String sociedad,String usuario)
        {
            str_IdExpediente = idexp;
            gstr_Sociedad = sociedad;
            gstr_Usuario = usuario;
        }

        public String enviar(String str_IdModulo, String str_IdOperacion, String str_Trasaccion, Boolean lbool_cambio, Decimal[] arrdec_Montos,
             Int32 int_CantidadLineasAsiento, Decimal MontoPrincipal, Decimal MontoInteres, out String CodAsiento)
        {
            #region Variables
            Presentacion.wsAsientos.ZfiAsiento item_asiento = new Presentacion.wsAsientos.ZfiAsiento();
            Presentacion.wsAsientos.ZfiAsiento item_asiento2 = new Presentacion.wsAsientos.ZfiAsiento();
            Presentacion.wsAsientos.ZfiAsiento[] tabla_asientos = new Presentacion.wsAsientos.ZfiAsiento[int_CantidadLineasAsiento];

            String[] item_resAsientosLog = new String[8000];
            String lstr_logAsiento = String.Empty;
            String lstr_Resultado = String.Empty;
            String lstr_Montos = String.Empty;

            String lstr_TipoProcesoTexto = String.Empty;
            String lstr_TipoProceso_CodAux2 = String.Empty;

            String lstr_idTira_CodAux3 = String.Empty;
            String lstr_clsDocumento_CodAux4 = String.Empty;

            String lstr_ClaveContable = String.Empty;
            String lstr_ClaveContable2 = String.Empty;

            Int32 lint_Contador = 0;
            Int32 lint_cantTiras = 0;
            Int32 lint_contMonto = 0;
            Boolean esLiquidacion = false;

            DateTime ldt_FechaContabilizacion = DateTime.Now;

            Decimal ldec_Monto = 0;

            Boolean bool_diferencial = false;

            #endregion

            CodAsiento = "";
            //Tipo de proceso
            lstr_TipoProcesoTexto = ConsultarTipoProcesoExpediente(str_IdExpediente);
            lstr_TipoProceso_CodAux2 = ConsultarOpcionesCatalogos(lstr_TipoProcesoTexto);
            lstr_clsDocumento_CodAux4 = ConsultarClaseDocumento(str_IdModulo, str_IdOperacion);
            Decimal numero_asiento = str_IdOperacion.Contains("CT")? Decimal.Parse(str_IdOperacion.Replace("CT","")):0;

            //Obtenemos tira de asientos configuradas en el gestor
            DataSet lds_TirasAsientos = ConsultarTiposAsientos(str_IdModulo, str_IdOperacion, lstr_TipoProceso_CodAux2);
            DataTable ldt_TirasAsiento = null;

            MontoPrincipal = Math.Round(MontoPrincipal, 2);
            MontoInteres = Math.Round(MontoInteres, 2);

            if (arrdec_Montos != null)
            {
                for (int i = 0; i < arrdec_Montos.Count(); i++)
                {
                    if (arrdec_Montos[i] != null)
                        arrdec_Montos[i] = Math.Round(arrdec_Montos[i], 2);
                }
            }

            #region validaciones para casos especiales

            if (str_IdOperacion.Contains("CT05") || str_IdOperacion.Contains("CT66"))
            {
                lbool_cambio = false;
            }
            else if (str_IdOperacion.Contains("CT69") || str_IdOperacion.Contains("CT70"))
            {
                if (arrdec_Montos!=null && arrdec_Montos.Count() > 3)
                {
                    Decimal RF = arrdec_Montos[0];
                    Decimal RP = arrdec_Montos[1];
                    Decimal dif = arrdec_Montos[2];
                    arrdec_Montos[0] = RP;
                    arrdec_Montos[1] = dif;
                    arrdec_Montos[2] = RF;
                }
            }
            else if (str_IdOperacion.Contains("CT99") || str_IdOperacion.Contains("CT100") ||
                str_IdOperacion.Contains("CT101") || str_IdOperacion.Contains("CT102"))
            {
                lbool_cambio = false;
                esLiquidacion = true;
            }
            else if (str_IdOperacion.Contains("CT95") || str_IdOperacion.Contains("CT96") ||
                str_IdOperacion.Contains("CT97") || str_IdOperacion.Contains("CT98"))
            {
                bool_diferencial = true;
            }
            else
            {
                if (arrdec_Montos != null && arrdec_Montos.Count() > 2 &&
                    str_IdOperacion.Contains("CT71") || str_IdOperacion.Contains("CT72"))
                {
                    lbool_cambio = true;
                    Decimal Monto = arrdec_Montos[1];
                    Decimal intereses = arrdec_Montos[0];
                    arrdec_Montos[0] = Monto;
                    arrdec_Montos[1] = intereses;
                }
            }
            if (str_Trasaccion.Contains("Liquida"))
            {
                esLiquidacion = true;
            }
            #endregion

            lint_cantTiras = lds_TirasAsientos.Tables[0].Rows.Count;

            if (lint_cantTiras > 0)
            {
                ldt_TirasAsiento = lds_TirasAsientos.Tables[0];

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
                                if (MontoPrincipal != 0)
                                {
                                    ////Llenamos los asientos
                                    item_asiento = new wsAsientos.ZfiAsiento();
                                    String lstr_info = str_IdOperacion + " : " + str_IdExpediente;
                                    if (lstr_info.Length > 15)
                                        lstr_info = lstr_info.Substring(0, 15);
                                    item_asiento.Xblnr = lstr_info;//REF
                                    item_asiento.Bktxt = get_operation_name(str_IdOperacion, "CT");
                                    item_asiento.Blart = ldr_TiraAsiento["CodigoAuxiliar4"].ToString().Trim();//Clase de documento
                                    item_asiento.Bukrs = ldr_TiraAsiento["Codigo"].ToString().Trim();//Sociedad
                                    item_asiento.Bldat = ldt_FechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de documento
                                    item_asiento.Budat = ldt_FechaContabilizacion.ToString("dd.MM.yyyy");//Fecha de contabilización
                                    item_asiento.Xref1Hd = str_IdExpediente;//numero expediente 
                                    item_asiento.Xref2Hd = str_IdOperacion + "-" + lstr_TipoProceso_CodAux2;//CT01-AG operacion+codigoprocesal expediente


                                    item_asiento.Waers = ldr_TiraAsiento["CodigoAuxiliar"].ToString().Trim();//Moneda 
                                    item_asiento.Bschl = ldr_TiraAsiento["IdClaveContable"].ToString().Trim();//Clave de contabilización
                                    item_asiento.Hkont = ldr_TiraAsiento["IdCuentaContable"].ToString().Trim();//Cuenta de mayor
                                    item_asiento.Wrbtr = MontoPrincipal;//Importe o monto en colones a contabilizar 

                                    lstr_Montos = lstr_Montos + lint_cont++ + ". 40: " + MontoPrincipal + "\n";

                                    item_asiento.Zuonr = "Asig_1";
                                    item_asiento.Sgtxt = "Modulo Contingente";
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
                                    item_asiento2 = new wsAsientos.ZfiAsiento();
                                    item_asiento2.Waers = ldr_TiraAsiento["CodigoAuxiliar"].ToString().Trim();//Moneda 
                                    item_asiento2.Bschl = ldr_TiraAsiento["IdClaveContable2"].ToString().Trim();//Clave de contabilización
                                    item_asiento2.Hkont = ldr_TiraAsiento["IdCuentaContable2"].ToString().Trim();//Cuenta de mayor
                                    item_asiento2.Wrbtr = MontoPrincipal;//Importe o monto en colones a contabilizar
                                    lstr_Montos = lstr_Montos + lint_cont++ + ". 50: " + MontoPrincipal + "\n";

                                    item_asiento2.Zuonr = "";
                                    item_asiento2.Sgtxt = "Modulo Contingente";
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
                                if (MontoInteres != 0)
                                {
                                    item_asiento = new wsAsientos.ZfiAsiento();
                                    ///***************************************************Cargar cuenta 40 HABER*****************************************************/
                                    if (MontoPrincipal == 0)
                                    {
                                        String lstr_info = str_IdOperacion + " : " + str_IdExpediente;
                                        if (lstr_info.Length > 15)
                                            lstr_info = lstr_info.Substring(0, 15);
                                        item_asiento.Xblnr = lstr_info;//REF
                                        item_asiento.Bktxt = get_operation_name(str_IdOperacion, "CT");
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
                                    item_asiento.Wrbtr = MontoInteres;//Importe o monto en colones a contabilizar 

                                    lstr_Montos = lstr_Montos + lint_cont++ + ". 40: " + MontoInteres + "\n";

                                    item_asiento.Zuonr = "Asig_1";
                                    item_asiento.Sgtxt = "Modulo Contingente";
                                    item_asiento.Projk = ldr_TiraAsiento["IdElementoPEP"].ToString().TrimEnd();
                                    item_asiento.Fipex = ldr_TiraAsiento["IdPosPre"].ToString().TrimEnd();//Posición presupuestaria
                                    item_asiento.Kostl = ldr_TiraAsiento["IdCentroCosto"].ToString();
                                    item_asiento.Fistl = ldr_TiraAsiento["IdCentroGestor"].ToString();
                                    item_asiento.Prctr = ldr_TiraAsiento["IdCentroBeneficio"].ToString();
                                    item_asiento.Measure = ldr_TiraAsiento["IdPrograma"].ToString().TrimEnd();//Programa presupuestario
                                    item_asiento.Geber = ldr_TiraAsiento["IdFondo"].ToString().Trim();//Fondo
                                    item_asiento.Fkber = "";
                                    item_asiento.Xref2 = "";
                                    if (MontoPrincipal == 0)
                                    {
                                        tabla_asientos[0] = item_asiento;
                                    }
                                    else
                                        tabla_asientos[2] = item_asiento;
                                    ///***************************************************Cargar cuenta 50 DEBE*****************************************************/
                                    item_asiento2 = new wsAsientos.ZfiAsiento();
                                    item_asiento2.Waers = ldr_TiraAsiento["CodigoAuxiliar"].ToString().Trim();//Moneda 
                                    item_asiento2.Bschl = ldr_TiraAsiento["IdClaveContable2"].ToString().Trim();//Clave de contabilización
                                    item_asiento2.Hkont = ldr_TiraAsiento["IdCuentaContable2"].ToString().Trim();//Cuenta de mayor
                                    item_asiento2.Wrbtr = MontoInteres;//Importe o monto en colones a contabilizar
                                    lstr_Montos = lstr_Montos + lint_cont++ + ". 50: " + MontoInteres + "\n";


                                    item_asiento2.Projk = ldr_TiraAsiento["IdElementoPEP2"].ToString().TrimEnd();
                                    item_asiento2.Fipex = ldr_TiraAsiento["IdPosPre2"].ToString().TrimEnd();//Posición presupuestaria
                                    item_asiento2.Kostl = ldr_TiraAsiento["IdCentroCosto2"].ToString();
                                    item_asiento2.Fistl = ldr_TiraAsiento["IdCentroGestor2"].ToString();
                                    item_asiento2.Prctr = ldr_TiraAsiento["IdCentroBeneficio2"].ToString();
                                    item_asiento2.Measure = ldr_TiraAsiento["IdPrograma2"].ToString().TrimEnd();//Programa presupuestario
                                    item_asiento2.Zuonr = "Asig_2";
                                    item_asiento2.Sgtxt = "Modulo Contingente";//char 50
                                    item_asiento2.Geber = ldr_TiraAsiento["IdFondo2"].ToString().Trim();//Fondo
                                    item_asiento2.Fkber = "";
                                    item_asiento2.Xref2 = "xref2";
                                    if (MontoPrincipal == 0)
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

                        if (!esLiquidacion && !bool_diferencial)
                        {
                            if (lint_Contador == int_CantidadLineasAsiento)
                                break;
                            if (lint_cantTiras == lint_Contador)
                                break;
                            else if ((lint_cantTiras == 4) && (MontoPrincipal != 0) && (MontoInteres == 0))
                            {
                                if ((lint_index == 1) || (lint_index == 3))
                                    continue;
                            }
                            else if ((lint_cantTiras == 4) && (MontoPrincipal == 0) && (MontoInteres != 0))
                            {
                                if ((lint_index == 0) || (lint_index == 2))
                                    continue;
                            }
                            else if ((lint_cantTiras == 6) && (MontoPrincipal != 0) && (MontoInteres == 0))
                            {
                                if ((lint_index == 1) || (lint_index == 3) || (lint_index == 5))
                                    continue;
                            }
                            else if ((lint_cantTiras == 6) && (MontoPrincipal == 0) && (MontoInteres != 0))
                            {
                                if ((lint_index == 0) || (lint_index == 2) || (lint_index == 4))
                                    continue;
                            }
                        }

                        ldec_Monto = arrdec_Montos[lint_contMonto++];

                        if ((lint_cantTiras == 10) && (ldec_Monto == 0))
                        {
                            continue;
                        }

                        if (ldec_Monto > 0)
                        {

                            if ((lstr_ClaveContable.Equals("40") && lstr_ClaveContable2.Equals("50")))
                            {
                                item_asiento = new wsAsientos.ZfiAsiento();
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
                                    item_asiento.Bktxt = get_operation_name(str_IdOperacion, "CT");
                                    item_asiento.Xref1Hd = str_IdExpediente;//numero expediente 
                                    item_asiento.Xref2Hd = str_IdOperacion + "-" + lstr_TipoProceso_CodAux2;//CT01-AG operacion+codigoprocesal expediente
                                }
                                #endregion

                                #region debe 40
                                item_asiento.Wrbtr = ldec_Monto;//Importe o monto en colones a contabilizar 
                                lstr_Montos = lstr_Montos + (lint_Contador + 1) + ". 40: " + ldec_Monto + "\n";
                                item_asiento.Sgtxt = "Modulo Contingente";
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

                                item_asiento2 = new wsAsientos.ZfiAsiento();
                                #region 50 haber
                                if ((lint_cantTiras == lint_Contador) && (lint_cantTiras != 5))
                                    break;
                                if (str_IdOperacion.Contains("CT09") && (lint_Contador == 3))
                                    break;
                                if ((lbool_cambio) && (lint_Contador < 2))
                                {
                                    ldec_Monto = arrdec_Montos[lint_contMonto++];
                                }
                                item_asiento2.Wrbtr = ldec_Monto;//Importe o monto en colones a contabilizar
                                lstr_Montos = lstr_Montos + (lint_Contador + 1) + ". 50: " + ldec_Monto + "\n";
                                item_asiento2.Sgtxt = "Modulo Contingente";
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
                                item_asiento = new wsAsientos.ZfiAsiento();
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
                                    item_asiento.Bktxt = get_operation_name(str_IdOperacion, "CT");
                                    item_asiento.Xref1Hd = str_IdExpediente;//numero expediente 
                                    item_asiento.Xref2Hd = str_IdOperacion + "-" + lstr_TipoProceso_CodAux2;//CT01-AG operacion+codigoprocesal expediente
                                }
                                #endregion

                                #region debe 40
                                item_asiento.Wrbtr = ldec_Monto;//Importe o monto en colones a contabilizar 
                                lstr_Montos = lstr_Montos + (lint_Contador + 1) + ". 40: " + ldec_Monto + "\n";
                                item_asiento.Sgtxt = "Modulo Contingente";
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
                                item_asiento = new wsAsientos.ZfiAsiento();
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
                                    item_asiento.Bktxt = get_operation_name(str_IdOperacion, "CT");
                                    item_asiento.Xref1Hd = str_IdExpediente;//numero expediente 
                                    item_asiento.Xref2Hd = str_IdOperacion + "-" + lstr_TipoProceso_CodAux2;//CT01-AG operacion+codigoprocesal expediente
                                }
                                #endregion

                                #region haber 50
                                item_asiento.Wrbtr = ldec_Monto;//Importe o monto en colones a contabilizar 
                                lstr_Montos = lstr_Montos + (lint_Contador + 1) + ". 50: " + ldec_Monto + "\n";
                                item_asiento.Sgtxt = "Modulo Contingente";
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
                    }
                    #endregion


                }
                //Cargar de Asientos 
                string[] concatenado = new string[8];
                //envio de asiento mediante servicio web hacia SIGAF
                try
                {
                    Boolean isnull_tabla = true;
                    foreach (Presentacion.wsAsientos.ZfiAsiento row in tabla_asientos)
                    {
                        if (row != null)
                        {
                            isnull_tabla = false;
                            break;
                        }
                    }
                    if (!isnull_tabla)
                    {
                        //item_resAsientosLog = ws_ContabilizaAsientos.EnviarAsientos(tabla_asientos);  *cucurucho
                        item_resAsientosLog = ws_ContabilizaAsientos.EnviarAsientos(tabla_asientos, ""); 
                        //Priscilla, llenar variable Out CodAsiento en caso de que no genere error
                        CodAsiento = "";
                        Int32 lint_Length = 0;
                        for (int j = 0; j < item_resAsientosLog.Count(); j++)
                        {
                            if (item_resAsientosLog[j].Contains("[E]"))
                                lstr_Resultado = "error";
                            else if (item_resAsientosLog[j].Contains("[S]"))
                            {
                                lint_Length = item_resAsientosLog[j].Length;
                                try
                                {
                                    gstr_AsientosResultado = gstr_AsientosResultado + "\n" + item_resAsientosLog[j].ToString().Substring(58, 10);
                                }
                                catch { }
                                lstr_Resultado = "Contabilizado";


                                try
                                {
                                    ws_SG.uwsRegistrarBitacoraMovimientosCuentasExpedientes(str_IdExpediente, "CT", gstr_Sociedad, str_IdOperacion, str_Trasaccion, MontoInteres, MontoInteres, 0, 0, "Provisión Monto Interes Colones - ", gstr_Usuario);

                                    //ws_SG.uwsRegistrarCobrosPagos(str_IdExpediente, str_IdExpediente)
                                }
                                catch { }
                            }
                            else if (item_resAsientosLog[j].Contains("[I]"))
                                lstr_Resultado = "info";

                            lstr_logAsiento += "\n" + (j + 1) + ": " + item_resAsientosLog[j];

                        }

                        //Envia el codigo Asiento
                        if (lstr_Resultado == "Contabilizado")
                        {
                            String cod;
                            cod = lstr_logAsiento.Substring(lstr_logAsiento.IndexOf("BKPFF") + 1, lstr_logAsiento.Length - lstr_logAsiento.IndexOf("BKPFF") - 1);

                            CodAsiento = cod.Substring(5, 18);

                            //CodAsiento = lstr_logAsiento.Substring(lstr_logAsiento.IndexOf("BKPFF") + 1, lstr_logAsiento.Length - lstr_logAsiento.IndexOf("BKPFF") - 1);

                        }
                        //    ws_SG.uwsModificarCodigoAsientoCo(lint_Length,lint_cantTiras,lstr_Resultado,str_IdExpediente,gstr_Sociedad,CodAsiento,gstr_Usuario); 

                        if (MontoPrincipal == 0 && MontoInteres == 0 && lint_cantTiras == 2)
                        {
                            lstr_Resultado = "Contabilizado";
                        }

                        ws_SG.uwsRegistrarAccionBitacoraCo("CT", gstr_Usuario, "Enviar Asiento", str_IdExpediente + ":" + gstr_Sociedad +
                            " Operación: " + str_IdOperacion + "\n" + lstr_Montos +
                            "\nResultado: " + lstr_logAsiento,
                            str_IdExpediente, str_Trasaccion, gstr_Sociedad);

                        gstr_MensajeResultadoResoluciones = gstr_MensajeResultadoResoluciones + Environment.NewLine + lstr_logAsiento;
                        //ws_SG.uwsRegistrarAccionBitacoraCo("CT", gstr_Usuario, "Reversion", str_IdExpediente + " Operación: " + str_IdOperacion + "\nResultado: ",
                        //    str_IdExpediente, str_Trasaccion, gstr_Sociedad);

                        try
                        {
                            if (lstr_Resultado == "Contabilizado")
                            {
                                String[] lstr_AsientosResultado = new String[3];
                                Int32 lint_IdExp = 0;

                                String lstr_query = "SELECT IdExp FROM co.Expedientes exp " +
                                    "WHERE exp.IdExpediente ='" + str_IdExpediente + "' " +
                                    "AND exp.IdSociedadGL ='" + gstr_Sociedad + "' " +
                                    "AND exp.EstadoExpediente = 'Activo'";

                                DataTable dt_Resoluciones = GetData(lstr_query);


                                foreach (DataRow dr_Resolucion in dt_Resoluciones.Rows)
                                {
                                    lint_IdExp = Convert.ToInt32(dr_Resolucion["IdExp"]);
                                }

                                if ((lint_cantTiras == 2) && !lbool_cambio)
                                {
                                    lstr_AsientosResultado = ws_SG.uwsRegistrarCobrosPagos(str_IdOperacion,
                                    str_IdExpediente, lint_IdExp, "REV", 0, 0,
                                    0, 0,
                                    arrdec_Montos == null ? MontoPrincipal : arrdec_Montos[0],
                                    arrdec_Montos == null ? MontoPrincipal : arrdec_Montos[0],
                                    0,
                                    arrdec_Montos == null ? MontoInteres : arrdec_Montos[1],
                                    arrdec_Montos == null ? MontoInteres : arrdec_Montos[1],

                                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                    "tipotra", "estadotran", DateTime.Today, "Reversion", gstr_Usuario);
                                }
                                else if ((lint_cantTiras == 2) && lbool_cambio)
                                {
                                    lstr_AsientosResultado = ws_SG.uwsRegistrarCobrosPagos(str_IdOperacion,
                                    str_IdExpediente, lint_IdExp, "REV", 0, 0,
                                    0, 0,
                                    arrdec_Montos == null ? 0 : (arrdec_Montos.Count() >= 2 ? arrdec_Montos[0] : 0),
                                    arrdec_Montos == null ? 0 : (arrdec_Montos.Count() >= 2 ? arrdec_Montos[1] : 0),
                                    0,
                                    arrdec_Montos == null ? 0 : (arrdec_Montos.Count() >= 2 ? arrdec_Montos[0] : 0),
                                    arrdec_Montos == null ? 0 : (arrdec_Montos.Count() >= 2 ? arrdec_Montos[1] : 0),

                                    0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
                                    "tipotra", "estadotran", DateTime.Today, "Reversion", gstr_Usuario);
                                }
                                else
                                {
                                    lstr_AsientosResultado = ws_SG.uwsRegistrarCobrosPagos(str_IdOperacion,
                                        str_IdExpediente, lint_IdExp, "REV", 0, 0,
                                        0, 0,
                                        arrdec_Montos == null ? 0 : (arrdec_Montos.Count() >= 0 ? arrdec_Montos[0] : 0),
                                        arrdec_Montos == null ? 0 : (arrdec_Montos.Count() >= 1 ? arrdec_Montos[1] : 0),
                                        arrdec_Montos == null ? 0 : (arrdec_Montos.Count() >= 2 ? arrdec_Montos[2] : 0),
                                        arrdec_Montos == null ? 0 : (arrdec_Montos.Count() >= 3 ? arrdec_Montos[3] : 0),
                                        arrdec_Montos == null ? 0 : (arrdec_Montos.Count() >= 4 ? arrdec_Montos[4] : 0),
                                        arrdec_Montos == null ? 0 : (arrdec_Montos.Count() >= 5 ? arrdec_Montos[5] : 0),
                                        arrdec_Montos == null ? 0 : (arrdec_Montos.Count() >= 6 ? arrdec_Montos[6] : 0),
                                        arrdec_Montos == null ? 0 : (arrdec_Montos.Count() >= 7 ? arrdec_Montos[7] : 0),
                                        arrdec_Montos == null ? 0 : (arrdec_Montos.Count() >= 8 ? arrdec_Montos[8] : 0),
                                        arrdec_Montos == null ? 0 : (arrdec_Montos.Count() >= 9 ? arrdec_Montos[9] : 0),
                                        arrdec_Montos == null ? 0 : (arrdec_Montos.Count() >= 10 ? arrdec_Montos[10] : 0),
                                        arrdec_Montos == null ? 0 : (arrdec_Montos.Count() >= 10 ? arrdec_Montos[11] : 0),

                                        0, 0, 0,//Intereses
                                        0, 0, 0,
                                        0, 0, 0,
                                        0, 0, 0,
                                        0, 0, 0, 0,//Anteriores

                                        "tipotra", "estadotran", DateTime.Today, "Reversion", gstr_Usuario);
                                }
                            }
                        }
                        catch (Exception exp)
                        {
                            gstr_MensajeResultadoResoluciones = gstr_MensajeResultadoResoluciones + Environment.NewLine + exp.Message;
                        }
                    }
                    else
                    {
                        lstr_Resultado = "Contabilizado";
                    }
                }
                catch (Exception ex)
                {
                    gstr_MensajeResultadoResoluciones = gstr_MensajeResultadoResoluciones + Environment.NewLine + ex.Message;

                    lstr_Resultado = "Error: " + ex.Message;

                    ws_SG.uwsRegistrarAccionBitacoraCo("CT", gstr_Usuario, "Enviar Asiento", str_IdExpediente + ":" + gstr_Sociedad +
                        " Operación: " + str_IdOperacion + "\n" + lstr_Montos +
                       "\nResultado: " + lstr_Resultado,
                        str_IdExpediente, str_Trasaccion, gstr_Sociedad);
                    
                    return lstr_Resultado;
                }
            }
            else
            {
                lstr_Resultado = "Error: Los datos de consulta del asiento, no fue encontrada en la configuracion del Sistema Gestor.";
            }

            return lstr_Resultado;

        }


        #region Obtencion de datos

        private String ConsultarTipoProcesoExpediente(String idExpediente)
        {
            String lstr_ConsultaTipoExpedientes = "SELECT TipoProcesoExpediente FROM co.Expedientes " +
                "WHERE IdExpediente='" + idExpediente + "'";
            DataTable ldt_TipoExpedientes = GetData(lstr_ConsultaTipoExpedientes);
            DataRow ldr_TipoExpediente = null;

            String lstr_TipoProceso = String.Empty;
            if (ldt_TipoExpedientes.Rows.Count > 0)
            {
                ldr_TipoExpediente = ldt_TipoExpedientes.Rows[0];
                lstr_TipoProceso = ldr_TipoExpediente["TipoProcesoExpediente"].ToString();
            }

            return lstr_TipoProceso;
        }

        private DataTable GetData(string lstr_query)
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
            ds = ws_SG.uwsConsultarDinamico(lstr_query);
            if (ds.Tables.Count > 0)
            {
                return ds.Tables["Table"];
            }
            return null;
        }

        public string get_operation_name(string operation_id, string module_id)
        {
            string operation_name = operation_id;
            try
            {
                string query = string.Format("SELECT substring(NomOperacion,0,25) AS NomOperacion FROM ma.Operaciones where idoperacion = '{0}' and idmodulo = '{1}' and estado = 'A'", operation_id, module_id);
                string temp_name = GetData(query).Rows[0]["NomOperacion"].ToString();
                if (!string.IsNullOrEmpty(temp_name))
                {
                    operation_name = temp_name;
                }
            }
            catch (Exception ex)
            {
                //No existe nombre de operacion
            }
            return operation_name;
        }

        private String ConsultarOpcionesCatalogos(String str_TipoProcesoExpediente)
        {
            String lstr_TipoProcesoExpediente = String.Empty;
            String lstr_ConsultaProcesosExpediente = "SELECT ValOpcion,NomOpcion FROM ma.OpcionesCatalogos " +
                "WHERE IdCatalogo='30' AND Estado = 'A' AND NomOpcion='" + str_TipoProcesoExpediente + "'";
            DataTable ldt_ProcesosExpedientes = GetData(lstr_ConsultaProcesosExpediente);
            DataRow ldr_ProcesoExpediente = null;

            if (ldt_ProcesosExpedientes.Rows.Count > 0)
            {
                ldr_ProcesoExpediente = ldt_ProcesosExpedientes.Rows[0];
                lstr_TipoProcesoExpediente = ldr_ProcesoExpediente["ValOpcion"].ToString();
            }

            return lstr_TipoProcesoExpediente;
        }

        private String ConsultarClaseDocumento(String str_modulo, String str_operacion)
        {
            String consult = "SELECT  IdClaseDoc FROM [ma].[Operaciones] " +
            "WHERE IdModulo='CT' and IdOperacion='" + str_operacion + "'";
            DataTable dt2 = GetData(consult);
            DataRow campo = null;
            string clasDoc = string.Empty;
            if (dt2.Rows.Count > 0)
            {
                campo = dt2.Rows[0];
                clasDoc = campo["IdClaseDoc"].ToString();
            }
            return clasDoc;
        }

        private DataSet ConsultarTiposAsientos(String str_modulo, String str_operacion, String str_tipoProcesoExpediente)
        {
            DataSet lds_TirasAsientos;
            String lstr_SociedadFi = string.Empty;
            String lstr_ConsultaSociedades = "SELECT IdSociedadFi from ma.SociedadesFinancieras " +
            "WHERE IdSociedadGL='" + gstr_Sociedad + "'";

            DataTable lds_NombreSociedades = GetData(lstr_ConsultaSociedades);
            DataRow ldr_NombreSociedad = null;

            if (lds_NombreSociedades.Rows.Count > 0)
            {
                ldr_NombreSociedad = lds_NombreSociedades.Rows[0];
                lstr_SociedadFi = ldr_NombreSociedad["IdSociedadFi"].ToString();
            }

            lds_TirasAsientos = ws_SG.uwsConsultarTiposAsientoDetalle(lstr_SociedadFi.Trim(), str_modulo, str_operacion, String.Empty, String.Empty, "CRC", str_tipoProcesoExpediente, null, null, null, null, null, "ORDER BY Codigo, IdModulo, Secuencia", "N");

            return lds_TirasAsientos;
        }
        
        #endregion

        public void modificarAsiento(int idRes, String numRes,String asiento)
        {
            try
            {
                //TODO: quitar este sql 
                int x = Convert.ToInt32(GetData("SELECT [IdCobroPagoResolucion] FROM [co].[CobrosPagos] where idexpedientefk='" + str_IdExpediente + "' and idresolucionfk = '" + numRes + "'").Rows[0]["IdCobroPagoResolucion"]);
                String[] res = ws_SG.uwsModificarCodigoAsientoCo(idRes, x, numRes, str_IdExpediente, gstr_Sociedad, asiento, gstr_Usuario);

            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo guardar el asiento en la resolución.\nCode: "+ex.Message);
            }
        }   
    }    
}