﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio.Contingentes;

namespace SGAsientosDeudaInterna
{
    public class TitulosReclasificacion
    {
        private static wsDeudaInterna.wsDeudaInterna wsDeudaInterna = new wsDeudaInterna.wsDeudaInterna();
        private static ws_SGService.wsSistemaGestor wsSG = new ws_SGService.wsSistemaGestor();
        private static wsAsientos.ServicioContable wsAsientos = new wsAsientos.ServicioContable();
        
        //static void Main(string[] args)
        //{
        //    // Revisar los titulos titulo fecha valor contra fecha de vencimiento CP < 365, revisar la moneda xq dependiendo de
        //    //la moneda se hace uno u otro asiento, se compara con el nemotécnico
        //    //Generar el asiento --> ID73(dolares) o ID74(colones) --> 3 metodos de costos de transacción
        //    //Titulos reclasificados 
        //    /*
             
        //     */
            
        //    Console.WriteLine("Ejecutar aplicacion:");
        //    TitulosReclasificados();
        //    Console.ReadLine();
        //}

        public string TitulosReclasificados()
        {
            string lstr_Mensaje = string.Empty;
            DateTime ldt_FechaActual = DateTime.Now;
            //DataSet lds_Titulos =  wsDeudaInterna.ConsultarTitulosValores("%", "%", "%", "%", "%","01/01/1900", "01/01/5000"); solo tenia 7 parametros cucurucho
            DataSet lds_Titulos = wsDeudaInterna.ConsultarTitulosValores("%", "%", "%", "%", "%", "%", "%", "%", "01/01/1900", "01/01/5000");

            try
            {
                if (lds_Titulos.Tables["Table"].Rows.Count > 0)
                    foreach (DataRow dr_FilaExpediente in lds_Titulos.Tables["Table"].Rows)
                    {
                        TimeSpan vFecha = Convert.ToDateTime(dr_FilaExpediente["FchVencimiento"].ToString()) - ldt_FechaActual;
                        if (vFecha.Days <= 365)
                        {
                            DataSet lds_TipoCambio = wsSG.uwsConsultarTiposCambio(dr_FilaExpediente["Moneda"].ToString(), DateTime.Now, "", "N");
                            //Almacenar en Bitácora de que cambia estado
                            string[] lstr_Resultado = wsDeudaInterna.CrearTituloReclasificado(
                                   Convert.ToInt32(dr_FilaExpediente["NroValor"].ToString()),
                                   dr_FilaExpediente["NemoTecnico"].ToString(),
                                   dr_FilaExpediente["Tipo"].ToString(),
                                   dr_FilaExpediente["Moneda"].ToString(),
                                   Convert.ToDecimal(dr_FilaExpediente["ValorFacial"].ToString()),
                                   Convert.ToDecimal(dr_FilaExpediente["ValorTransadoBruto"].ToString()),
                                   Convert.ToDecimal(dr_FilaExpediente["ValorTransadoNeto"].ToString()),
                                   Convert.ToDateTime(dr_FilaExpediente["FchValor"].ToString()),
                                   Convert.ToDateTime(dr_FilaExpediente["FchCancelacion"].ToString()),
                                   Convert.ToDateTime(dr_FilaExpediente["FchVencimiento"].ToString()),
                                   dr_FilaExpediente["SistemaNegociacion"].ToString(),
                                   dr_FilaExpediente["Estado"].ToString(),
                                   "SG");
                            //Generar el asiento
                          lstr_Mensaje = PrepararContabilizacionReclasificacion(
                                dr_FilaExpediente["NemoTecnico"].ToString(),
                                dr_FilaExpediente["Moneda"].ToString(),
                                dr_FilaExpediente["NroValor"].ToString(),
                                (dr_FilaExpediente["NroValor"].ToString() + "-" + dr_FilaExpediente["NemoTecnico"].ToString()).ToString(),
                                Convert.ToDecimal(lds_TipoCambio.Tables[0].Rows[0]["Valor"].ToString()),
                                dr_FilaExpediente["Propietario"].ToString(),
                                Convert.ToDecimal(dr_FilaExpediente["ValorFacial"].ToString()),
                                Convert.ToDecimal(dr_FilaExpediente["RendimientoPorDescuento"].ToString()),
                                Convert.ToDecimal(dr_FilaExpediente["ImpuestoPagado"].ToString()),
                                Convert.ToDecimal(dr_FilaExpediente["ValorTransadoNeto"].ToString()));

                        }
                    }
            }
            catch (Exception ex) { lstr_Mensaje = ex.ToString(); }
            return lstr_Mensaje;
        }

        public string PrepararContabilizacionReclasificacion(string lstr_Nemotecnico,
                                                 string lstr_Moneda,
                                                 string lstr_NroValor,
                                                 string lstr_Detalle,
                                                 decimal dec_TipoCambioNoBancario,
                                                 string lstr_Propietario,
                                                 decimal ldec_ValorFacial,
                                                 decimal ldec_RendimientoXDescuento,
                                                 decimal ldec_ImpuestoPagado,
                                                 decimal ldec_ValorTransadoNeto)
        {
            string lstr_Mensaje = string.Empty;
            string lstr_IdOperacion1 = string.Empty;
            Decimal ldec_monto = 0;
            DataTable ldat_Asiento = new DataTable();
            DataTable ldat_AsientoReclasificacion = new DataTable();
            DataTable ldat_AsientoPago = new DataTable();
            DataSet ldat_Reservas = new DataSet();
            DataTable ldat_TituloValor = new DataTable();
            string FchCont = string.Empty;
            string lstr_NemoInfo = lstr_Nemotecnico;
                            
            switch (lstr_Moneda)
            {
                case "USD":
                    lstr_IdOperacion1 = "ID73";
                    break;
                    
                case "CRCN":
                    lstr_Moneda = "CRC";
                    lstr_IdOperacion1 = "ID74";
                    break;
                case "UDE":
                    lstr_Moneda = "CRC";
                    lstr_IdOperacion1 = "ID74";
                    break;

            }
            int lint_EsPublico = 1;
            if (wsSG.uwsConsultarPropietarios(string.Empty, string.Empty, string.Empty, lstr_Propietario).Tables[0].Rows.Count == 0)
                lint_EsPublico = 2;
            
            ldat_AsientoReclasificacion = wsSG.uwsConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_IdOperacion1, "", "", lstr_Moneda, lstr_Nemotecnico, "", "ID").Tables[0].Select("EsNemotecnico='5' AND EsPubPriv='" + lint_EsPublico + "'").CopyToDataTable(); ;
 
           

            FchCont = DateTime.Now.ToString("dd.MM.yyyy");

            foreach (DataRow ldr_Row in ldat_AsientoReclasificacion.Rows) 
            {

                ldat_Asiento = new DataTable();

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

                int index = ldat_AsientoReclasificacion.Rows.IndexOf(ldr_Row);
         
                switch (index)
                {
                    case 0: { ldec_monto = ldec_ValorFacial; break; }
                    case 1: { ldec_monto = ldec_ImpuestoPagado; break; }
                    case 2: { ldec_monto = ldec_RendimientoXDescuento; break; }
                    case 3: { ldec_monto = ldec_ValorTransadoNeto; break; }
                }

                try
                {
                    ldat_Asiento.Rows.Add(
                        lstr_NroValor + " " + lstr_Nemotecnico,
                        FchCont,
                        ldat_AsientoReclasificacion.Rows[0]["IdCuentaContable"].ToString().Trim(),
                        ldat_AsientoReclasificacion.Rows[0]["IdClaveContable"].ToString().Trim(),
                        ldat_AsientoReclasificacion.Rows[0]["CodigoAuxiliar"].ToString().Trim(),
                        lstr_Detalle.Trim(),
                        ldat_AsientoReclasificacion.Rows[0]["IdCentroCosto"].ToString().Trim(),
                        ldat_AsientoReclasificacion.Rows[0]["IdCentroBeneficio"].ToString().Trim(),
                        ldat_AsientoReclasificacion.Rows[0]["IdElementoPEP"].ToString().Trim(),
                        ldat_AsientoReclasificacion.Rows[0]["IdPosPre"].ToString().Trim(),
                        ldat_AsientoReclasificacion.Rows[0]["IdCentroGestor"].ToString().Trim(),
                        ldat_AsientoReclasificacion.Rows[0]["IdFondo"].ToString().Trim(),
                        ldat_AsientoReclasificacion.Rows[0]["DocPresupuestario"].ToString().Trim(),
                        ldat_AsientoReclasificacion.Rows[0]["PosDocPresupuestario"].ToString().Trim(),
                        ldec_monto
                    );
                    ldat_Asiento.Rows.Add(
                        lstr_NroValor + " " + lstr_Nemotecnico,
                        FchCont,
                        ldat_AsientoReclasificacion.Rows[0]["IdCuentaContable2"].ToString().Trim(),
                        ldat_AsientoReclasificacion.Rows[0]["IdClaveContable2"].ToString().Trim(),
                        ldat_AsientoReclasificacion.Rows[0]["CodigoAuxiliar"].ToString().Trim(),
                        lstr_Detalle.Trim(),
                        ldat_AsientoReclasificacion.Rows[0]["IdCentroCosto2"].ToString().Trim(),
                        ldat_AsientoReclasificacion.Rows[0]["IdCentroBeneficio2"].ToString().Trim(),
                        ldat_AsientoReclasificacion.Rows[0]["IdElementoPEP2"].ToString().Trim(),
                        ldat_AsientoReclasificacion.Rows[0]["IdPosPre2"].ToString().Trim(),
                        ldat_AsientoReclasificacion.Rows[0]["IdCentroGestor2"].ToString().Trim(),
                        ldat_AsientoReclasificacion.Rows[0]["IdFondo2"].ToString().Trim(),
                        ldat_AsientoReclasificacion.Rows[0]["DocPresupuestario2"].ToString().Trim(),
                        ldat_AsientoReclasificacion.Rows[0]["PosDocPresupuestario2"].ToString().Trim(),
                        ldec_monto
                    );

                  lstr_Mensaje =  GenerarAsientoAjuste(ldat_Asiento, "Reclasificación de título valor", lstr_IdOperacion1, lstr_NroValor + " - " + lstr_NemoInfo, dec_TipoCambioNoBancario);
                }
                catch { }
            }
            return lstr_Mensaje;
        }


        public static string GenerarAsientoAjuste(
            DataTable ldat_Asiento, 
            string str_Accion, 
            string str_IDOperacion, 
            string str_IdTransaccion,
            Decimal dec_TipoCambioNoBancario)
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
                item_asiento.Blart = "ID";//Clase de documento
                item_asiento.Bukrs = "G206";//Sociedad
                //item_asiento.Werks = ldat_Asiento.Rows[0]["Referencia"].ToString();
                item_asiento.Bldat = ldat_Asiento.Rows[0]["Fecha"].ToString();//Fecha de documento
                item_asiento.Budat = ldat_Asiento.Rows[0]["Fecha"].ToString();//Fecha de contabilización

                item_asiento.Waers = ldat_Asiento.Rows[0]["Moneda"].ToString();//Moneda 
                item_asiento.Bschl = ldat_Asiento.Rows[0]["ClaveContable"].ToString();//Clave de contabilización
                item_asiento.Hkont = ldat_Asiento.Rows[0]["Cuenta"].ToString();//Cuenta de mayor
                item_asiento.Wrbtr = Convert.ToDecimal(ldat_Asiento.Rows[0]["Monto"].ToString());//Importe
                item_asiento.Sgtxt = ldat_Asiento.Rows[0]["TextoInfo"].ToString();//Texto Informativo (50 caracteres)
                item_asiento.Kostl = ldat_Asiento.Rows[0]["CentroCosto"].ToString();//Centro de Costo
                item_asiento.Prctr = ldat_Asiento.Rows[0]["CentroBeneficio"].ToString();//Centro de Beneficio
                item_asiento.Projk = ldat_Asiento.Rows[0]["ElementoPEP"].ToString();//Elemento PEP
                item_asiento.Fipex = ldat_Asiento.Rows[0]["PosPre"].ToString();//Posición Presupuestaria
                item_asiento.Fistl = ldat_Asiento.Rows[0]["CentroGestor"].ToString();//Centro Gestor
                item_asiento.Geber = ldat_Asiento.Rows[0]["Fondo"].ToString();//Fondo
                item_asiento.Kblnr = ldat_Asiento.Rows[0]["DocPres"].ToString();//Documento Presupuestario
                item_asiento.Kblpos = ldat_Asiento.Rows[0]["PosDocPres"].ToString();//Posición de documento presupuestario
                if (ldat_Asiento.Rows[0]["Moneda"].ToString() == "USD")
                    item_asiento.Kursf = Convert.ToDecimal(dec_TipoCambioNoBancario);
                

                tabla_asientos[0] = item_asiento;

                item_asiento = new wsAsientos.ZfiAsiento();

                item_asiento.Waers = ldat_Asiento.Rows[1]["Moneda"].ToString();//Moneda 
                item_asiento.Bschl = ldat_Asiento.Rows[1]["ClaveContable"].ToString();//Clave de contabilización
                item_asiento.Hkont = ldat_Asiento.Rows[1]["Cuenta"].ToString();//Cuenta de mayor
                item_asiento.Wrbtr = Convert.ToDecimal(ldat_Asiento.Rows[1]["Monto"].ToString());//Importe
                item_asiento.Sgtxt = ldat_Asiento.Rows[1]["TextoInfo"].ToString();//Texto Informativo (50 caracteres)
                item_asiento.Kostl = ldat_Asiento.Rows[1]["CentroCosto"].ToString();//Centro de Costo
                item_asiento.Prctr = ldat_Asiento.Rows[1]["CentroBeneficio"].ToString();//Centro de Beneficio
                item_asiento.Projk = ldat_Asiento.Rows[1]["ElementoPEP"].ToString();//Elemento PEP
                item_asiento.Fipex = ldat_Asiento.Rows[1]["PosPre"].ToString();//Posición Presupuestaria
                item_asiento.Fistl = ldat_Asiento.Rows[1]["CentroGestor"].ToString();//Centro Gestor
                item_asiento.Geber = ldat_Asiento.Rows[1]["Fondo"].ToString();//Fondo
                item_asiento.Kblnr = ldat_Asiento.Rows[1]["DocPres"].ToString();//Documento Presupuestario
                item_asiento.Kblpos = ldat_Asiento.Rows[1]["PosDocPres"].ToString();//Posición de documento presupuestario
                if (ldat_Asiento.Rows[1]["Moneda"].ToString() == "USD")
                    item_asiento.Kursf = Convert.ToDecimal(dec_TipoCambioNoBancario);
                

                tabla_asientos[1] = item_asiento;

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
                wsSG.uwsRegistrarAccionBitacoraCo("DI", "123", str_Accion, "Resultado de Contabilización: " + logAsiento, str_IDOperacion, str_IdTransaccion, "");
                return logAsiento;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

    }
}