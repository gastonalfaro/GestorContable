using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebServiceContableAsientos
{

    public class cls_CargaContableAsientos
    {
        public string[] EnviarAsientos(WebServiceContableAsientos.wsCC.ZfiAsiento[] asientos, string str_Test = "")
        {
            string[] resultMessages = null;

            try
            {
                //wsCC.Z_FI_CARGA_CONTABLE servicio = new wsCC.Z_FI_CARGA_CONTABLE(); cucurucho
                //wsCC.z_fi_carga_contable servicio = new wsCC.z_fi_carga_contable();
                wsCC.Z_FI_CARGA_CONTABLE servicio = new wsCC.Z_FI_CARGA_CONTABLE();

                wsCC.ZFiCargaContable metodo = new wsCC.ZFiCargaContable();
                wsCC.ZFiCargaContableResponse response = new wsCC.ZFiCargaContableResponse();

                wsCC.ZfiAsiento line_in = new wsCC.ZfiAsiento();
                wsCC.ZfiAsientoLog line_out = new wsCC.ZfiAsientoLog();

                wsCC.ZfiAsiento[] table_in;
                wsCC.ZfiAsientoLog[] table_out = new wsCC.ZfiAsientoLog[8];

                int tamAnno = asientos.Count();
                table_in = new wsCC.ZfiAsiento[tamAnno];

                for (int i = 0; i < table_in.Length; i++)
                {
                    if (asientos[i] != null)
                    {
                        //seteo de datos de entrada a item de asiento a enviar
                        line_in = new wsCC.ZfiAsiento();
                        line_in.Mandt = verificarNull(asientos[i].Mandt).ToString();//Orden
                        line_in.Bldat = verificarNull(asientos[i].Bldat).ToString();//Texto cabecera
                        line_in.Blart = verificarNull(asientos[i].Blart).ToString();
                        line_in.Bukrs = verificarNull(asientos[i].Bukrs).ToString();//"Fecha de documento"
                        line_in.Budat = verificarNull(asientos[i].Budat).ToString();//"Clave de contabilización"
                        line_in.Waers = verificarNull(asientos[i].Waers).ToString();//"Clase de documento"
                        line_in.Kursf = Convert.ToDecimal(verificarNull(asientos[i].Kursf));//Sociedad
                        line_in.Xblnr = verificarNull(asientos[i].Xblnr).ToString();

                        if (verificarNull(asientos[i].Xref1Hd).ToString().Length > 20)
                            line_in.Xref1Hd = verificarNull(asientos[i].Xref1Hd).ToString().Substring(0, 17) + "...";
                        else
                            line_in.Xref1Hd = verificarNull(asientos[i].Xref1Hd).ToString();//Importe en moneda Fuerte

                        line_in.Xref2Hd = verificarNull(asientos[i].Xref2Hd).ToString();//"Posición presupuestaria"
                        line_in.Buzei = verificarNull(asientos[i].Buzei).ToString();//Centro Gestor
                        line_in.Bktxt = verificarNull(asientos[i].Bktxt).ToString();//Fondo
                        line_in.Bschl = verificarNull(asientos[i].Bschl).ToString();//Banco Propio
                        line_in.Hkont = verificarNull(asientos[i].Hkont).ToString();//Cuenta de mayor 
                        line_in.Umskz = verificarNull(asientos[i].Umskz).ToString();//"Documento presupuestario"
                        line_in.Wrbtr = Convert.ToDecimal(verificarNull(asientos[i].Wrbtr));//Posición doc pres
                        line_in.Dmbe2 = Convert.ToDecimal(verificarNull(asientos[i].Dmbe2));//Centro de costo
                        line_in.Mwskz = verificarNull(asientos[i].Mwskz).ToString();//Tipo de Cambio
                        line_in.Xmwst = verificarNull(asientos[i].Xmwst).ToString();
                        line_in.Zfbdt = verificarNull(asientos[i].Zfbdt).ToString();//"Programa presupuestario"
                        line_in.Zuonr = verificarNull(asientos[i].Zuonr).ToString();//"Indicador impuesto"
                        line_in.Sgtxt = verificarNull(asientos[i].Sgtxt).ToString();//"Centro de beneficio"
                        line_in.Hbkid = verificarNull(asientos[i].Hbkid).ToString();//Elemento PEP
                        line_in.Zlsch = verificarNull(asientos[i].Zlsch).ToString();//
                        line_in.Kostl = verificarNull(asientos[i].Kostl).ToString();//Texto
                        line_in.Prctr = verificarNull(asientos[i].Prctr).ToString();//Indicador CME
                        line_in.Aufnr = verificarNull(asientos[i].Aufnr).ToString();//Fecha Valor
                        line_in.Projk = verificarNull(asientos[i].Projk).ToString();//Moneda
                        line_in.Fipex = verificarNull(asientos[i].Fipex).ToString();//Centro
                        line_in.Fistl = verificarNull(asientos[i].Fistl).ToString();//Importe
                        line_in.Measure = verificarNull(asientos[i].Measure).ToString();//Referencia
                        line_in.Geber = verificarNull(asientos[i].Geber).ToString();//"Calculo impuesto"
                        line_in.Werks = verificarNull(asientos[i].Werks).ToString();//Referencia 1
                        line_in.Valut = verificarNull(asientos[i].Valut).ToString();//Referencia 2
                        line_in.Kblnr = verificarNull(asientos[i].Kblnr).ToString();//Fecha base
                        line_in.Kblpos = verificarNull(asientos[i].Kblpos).ToString();//Vía de pago
                        line_in.Rcomp = verificarNull(asientos[i].Rcomp).ToString();//Asignación

                        
                        line_in.Xref2 = verificarNull(asientos[i].Xref2).ToString();//Asignación

                        line_in.Xref3 = verificarNull(asientos[i].Xref3).ToString();//Asignación
                        line_in.Fkber = verificarNull(asientos[i].Fkber).ToString();//Area funcional
                        //asientos coleccion
                        table_in[i] = line_in;
                    }
                }

                metodo.GtAsientos = table_in;
                metodo.ITest = str_Test;

                //string lstr_user = WebServiceContableAsientos.Properties.Settings.Default;
                //string lstr_pass = WebServiceContableAsientos.Properties.Settings.Default.;

                string lstr_user = System.Configuration.ConfigurationManager.AppSettings["USER_SAP"];//usuario
                string lstr_pass = System.Configuration.ConfigurationManager.AppSettings["PASS_SAP"];//contrasena

                servicio.Credentials = new System.Net.NetworkCredential(lstr_user, lstr_pass);

                response = servicio.ZFiCargaContable(metodo);

                //table_out = response.GtLog;

                #region info de contabilizacion

                //resultMessages = new string[response.GtLog.Count()];
                //for (int y = 0; y < response.GtLog.Count(); y++)
                //{
                //    resultMessages[y] = "[" + response.GtLog[y].Type + "] " + response.GtLog[y].Message;
                //}

                #endregion

                #region info de contabilizacion

                //EDITADO PARA RETORNAR NUMERO DE ASIENTO, CUIDADO!
                if (response.GtLog[0].Type.Equals("S"))
                    resultMessages = new string[response.GtLog.Count() + 1];
                else
                    resultMessages = new string[response.GtLog.Count()];

                for (int y = 0; y < response.GtLog.Count(); y++)
                {
                    if (response.GtLog[y].Type.Equals("S"))
                    {
                        resultMessages[y] = response.GtLog[y].Belnr;
                        resultMessages[y+1] = "[" + response.GtLog[y].Type + "] " + response.GtLog[y].Message;
                    }
                    else
                    {
                        resultMessages[y] = "[" + response.GtLog[y].Type + "] " + response.GtLog[y].Message;
                    }
                }

                #endregion

            }
            catch(Exception ex)
            {
                resultMessages = new string[1];
                resultMessages[0] = "[E] " + ex.Message;
            }

            return resultMessages;
        }

        private Object verificarNull(Object valor)
        {
            if (valor == null)
            {
                valor = "";
            }
            return valor;
        }

    }

}