using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LogicaNegocio.Consolidacion;

namespace WebServiceBalanceComprobacionSAP
{
    public class clsConexionSAP
    {
        public String[] RecibeBalanceComprobacion(tBalanceComprobacionCabecera t_cabecera, tBalanceComprobacionPosicion[] t_posicion)
        {

            wsrBalanceComprobacion.ZINT_RECIBE_BALANCE_COMPROBA ServicioRBC = new wsrBalanceComprobacion.ZINT_RECIBE_BALANCE_COMPROBA();
            wsrBalanceComprobacion.ZINT_RECIBE_BALANCE_COMPROBA1 MetodoRBC = new wsrBalanceComprobacion.ZINT_RECIBE_BALANCE_COMPROBA1();
            wsrBalanceComprobacion.ZINT_RECIBE_BALANCE_COMPROBAResponse RespuestaRBC = new wsrBalanceComprobacion.ZINT_RECIBE_BALANCE_COMPROBAResponse();

            wsrBalanceComprobacion.ZINT_EST_CAB_BALANCE_CONSOL Cabecera = new wsrBalanceComprobacion.ZINT_EST_CAB_BALANCE_CONSOL();
            wsrBalanceComprobacion.ZINT_EST_POS_BALANCE_CONSOL Posicion = new wsrBalanceComprobacion.ZINT_EST_POS_BALANCE_CONSOL();
            wsrBalanceComprobacion.ZINT_EST_POS_BALANCE_CONSOL[] TablaPosiciones = new wsrBalanceComprobacion.ZINT_EST_POS_BALANCE_CONSOL[2];
            wsrBalanceComprobacion.ZINT_EST_MESSAGE2[] MensajesRespuesta = new wsrBalanceComprobacion.ZINT_EST_MESSAGE2[2];

            String[] lstr_Resultado = new String[0];

            //#region Prueba
            //Cabecera.LEDGER = "CS";
            //Cabecera.VISTA = "01";
            //Cabecera.VERSION = "100";
            //Cabecera.EJERCICIO = "2015";
            //Cabecera.PERIODO = "10";
            //Cabecera.UNID_CONSOL = "12820";
            //Cabecera.PLAN_POS = "P1";

            //Posicion.POSICION = "1110201010";
            //Posicion.SUBPOSICION = "13";
            //Posicion.UNID_ASOCIADA = "";
            //Posicion.MONEDA = "CRC";
            //Posicion.SIGNO = "+";
            //Posicion.VALOR_ML = 100000;
            //Posicion.VALOR_MT = 100000;
            //Posicion.VALOR_AC = 100000;
            //TablaPosiciones[0] = Posicion;
   
            //Posicion.POSICION = "1110201020";
            //Posicion.SUBPOSICION = "13";
            //Posicion.UNID_ASOCIADA = "12751";
            //Posicion.MONEDA = "CRC";
            //Posicion.SIGNO = "-";
            //Posicion.VALOR_ML = 100000;
            //Posicion.VALOR_MT = 100000;
            //Posicion.VALOR_AC = 100000;
            //TablaPosiciones[1] = Posicion;
            //#endregion

            Cabecera.LEDGER = t_cabecera.Lstr_ledger;
            Cabecera.VISTA = t_cabecera.Lstr_vista;
            Cabecera.VERSION = t_cabecera.Lstr_version;
            Cabecera.EJERCICIO = t_cabecera.Lstr_ejercicio;
            Cabecera.PERIODO = t_cabecera.Lstr_periodo;
            Cabecera.UNID_CONSOL = t_cabecera.Lstr_unid_consol;
            Cabecera.PLAN_POS = t_cabecera.Lstr_plan_pos;

            for (int i = 0; t_posicion.Count() > i; i++)
            {
                Posicion.POSICION = t_posicion[i].Lstr_posicion;
                Posicion.SUBPOSICION = t_posicion[i].Lstr_subposicion;
                Posicion.UNID_ASOCIADA = t_posicion[i].Lstr_unid_asocia;
                Posicion.MONEDA = t_posicion[i].Lstr_moneda;
                Posicion.SIGNO = t_posicion[i].Lstr_signo;
                Posicion.VALOR_ML = t_posicion[i].Ldec_valor_ml;
                Posicion.VALOR_MT = t_posicion[i].Ldec_valor_mt;
                Posicion.VALOR_AC = t_posicion[i].Ldec_valor_ac;
                TablaPosiciones[i] = Posicion;
            }


            MetodoRBC.I_CABECERA = Cabecera;
            MetodoRBC.IT_POSICIONES = TablaPosiciones;

            string lstr_user = System.Configuration.ConfigurationManager.AppSettings["USER_SAP"];
            string lstr_pass = System.Configuration.ConfigurationManager.AppSettings["PASS_SAP"];
            ServicioRBC.Credentials = new System.Net.NetworkCredential(lstr_user, lstr_pass);

            RespuestaRBC = ServicioRBC.CallZINT_RECIBE_BALANCE_COMPROBA( MetodoRBC );

            MensajesRespuesta = RespuestaRBC.ET_MESSAGE;

            lstr_Resultado = new String[MensajesRespuesta.Count()];
            for (int i = 0; MensajesRespuesta.Count() > i; i++)
            {
                lstr_Resultado[i] = "[1]ID: " + MensajesRespuesta[i].ID + " [2]Mensaje: " + MensajesRespuesta[i].MESSAGE + 
                    " [3]Num: " + MensajesRespuesta[i].NUMBER + " [4]Tipo: " + MensajesRespuesta[i].TYPE  + '\n';
            }
            return lstr_Resultado;
        }
        
    }
}