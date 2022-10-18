using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebServiceBalanceComprobacion
{
    public class clsConexionSAP
    {
        public DataSet RecibeBalanceComprobacion(wsSAPBCQAS.ZINT_EST_CAB_BALANCE_CONSOL t_cabecera, wsSAPBCQAS.ZINT_EST_POS_BALANCE_CONSOL[] t_posicion)
        {
            DataSet lds_Resultado = new DataSet();
            try
            {
                //wsSAPBC.ZINT_RECIBE_BALANCE_COMPROBA ServicioRBC = new wsSAPBC.ZINT_RECIBE_BALANCE_COMPROBA();
                //wsSAPBC.ZINT_RECIBE_BALANCE_COMPROBA1 MetodoRBC = new wsSAPBC.ZINT_RECIBE_BALANCE_COMPROBA1();
                //wsSAPBC.ZINT_RECIBE_BALANCE_COMPROBAResponse RespuestaRBC = new wsSAPBC.ZINT_RECIBE_BALANCE_COMPROBAResponse();

                //wsSAPBC.ZINT_EST_CAB_BALANCE_CONSOL Cabecera = new wsSAPBC.ZINT_EST_CAB_BALANCE_CONSOL();
                //wsSAPBC.ZINT_EST_POS_BALANCE_CONSOL Posicion = new wsSAPBC.ZINT_EST_POS_BALANCE_CONSOL();
                //wsSAPBC.ZINT_EST_POS_BALANCE_CONSOL[] TablaPosiciones = new wsSAPBC.ZINT_EST_POS_BALANCE_CONSOL[t_posicion.Count()];
                //wsSAPBC.ZINT_EST_MESSAGE2[] MensajesRespuesta = new wsSAPBC.ZINT_EST_MESSAGE2[2];

                
                //wsSAPBCQAS.zint_recibe_balance_comproba ServicioRBC = new wsSAPBCQAS.zint_recibe_balance_comproba();
                //wsSAPBCQAS.ZINT_RECIBE_BALANCE_COMPROBA MetodoRBC = new wsSAPBCQAS.ZINT_RECIBE_BALANCE_COMPROBA();
                wsSAPBCQAS.ZINT_RECIBE_BALANCE_COMPROBA ServicioRBC = new wsSAPBCQAS.ZINT_RECIBE_BALANCE_COMPROBA();
                wsSAPBCQAS.ZINT_RECIBE_BALANCE_COMPROBA1 MetodoRBC = new wsSAPBCQAS.ZINT_RECIBE_BALANCE_COMPROBA1();
                

                wsSAPBCQAS.ZINT_RECIBE_BALANCE_COMPROBAResponse RespuestaRBC = new wsSAPBCQAS.ZINT_RECIBE_BALANCE_COMPROBAResponse();
                wsSAPBCQAS.ZINT_EST_CAB_BALANCE_CONSOL Cabecera = new wsSAPBCQAS.ZINT_EST_CAB_BALANCE_CONSOL();
                wsSAPBCQAS.ZINT_EST_POS_BALANCE_CONSOL Posicion = new wsSAPBCQAS.ZINT_EST_POS_BALANCE_CONSOL();
                wsSAPBCQAS.ZINT_EST_POS_BALANCE_CONSOL[] TablaPosiciones = new wsSAPBCQAS.ZINT_EST_POS_BALANCE_CONSOL[t_posicion.Count()];
                wsSAPBCQAS.ZINT_EST_MESSAGE2[] MensajesRespuesta = new wsSAPBCQAS.ZINT_EST_MESSAGE2[2];

                Cabecera.LEDGER = t_cabecera.LEDGER;
                Cabecera.VISTA = t_cabecera.VISTA;
                Cabecera.VERSION = t_cabecera.VERSION;
                Cabecera.EJERCICIO = t_cabecera.EJERCICIO;
                Cabecera.PERIODO = t_cabecera.PERIODO;
                Cabecera.UNID_CONSOL = t_cabecera.UNID_CONSOL;
                Cabecera.PLAN_POS = t_cabecera.PLAN_POS;

                for (int i = 0; t_posicion.Count() > i; i++)
                {

                    Posicion = new wsSAPBCQAS.ZINT_EST_POS_BALANCE_CONSOL();
                    Posicion.POSICION = t_posicion[i].POSICION;
                    Posicion.SUBPOSICION = t_posicion[i].SUBPOSICION;
                    Posicion.UNID_ASOCIADA = t_posicion[i].UNID_ASOCIADA;
                    Posicion.MONEDA = t_posicion[i].MONEDA;
                    Posicion.SIGNO = t_posicion[i].SIGNO;
                    Posicion.VALOR_ML = t_posicion[i].VALOR_ML;
                    Posicion.VALOR_MT = t_posicion[i].VALOR_MT;
                    Posicion.VALOR_AC = t_posicion[i].VALOR_AC;
                    TablaPosiciones[i] = Posicion;

                }

                MetodoRBC.I_CABECERA = Cabecera;
                MetodoRBC.IT_POSICIONES = TablaPosiciones;
                

                string lstr_user = WebServiceBalanceComprobacion.Properties.Settings.Default.USER_SAP;
                //System.Configuration.ConfigurationManager.AppSettings["USER_SAP"];
                string lstr_pass = WebServiceBalanceComprobacion.Properties.Settings.Default.PASS_SAP;
                //System.Configuration.ConfigurationManager.AppSettings["PASS_SAP"];
                ServicioRBC.Credentials = new System.Net.NetworkCredential(lstr_user, lstr_pass);

                //RespuestaRBC = ServicioRBC.CallZINT_RECIBE_BALANCE_COMPROBA(MetodoRBC); cucurucho
                //RespuestaRBC = ServicioRBC.ZINT_RECIBE_BALANCE_COMPROBA(MetodoRBC); //aqui es donde da error
                RespuestaRBC = ServicioRBC.CallZINT_RECIBE_BALANCE_COMPROBA(MetodoRBC);

                MensajesRespuesta = RespuestaRBC.ET_MESSAGE;

                DataTable ldt_TablaMensajes = new DataTable("Table");
                ldt_TablaMensajes.Columns.Add(new DataColumn("ID", typeof(string)));
                ldt_TablaMensajes.Columns.Add(new DataColumn("MESSAGE", typeof(string)));
                ldt_TablaMensajes.Columns.Add(new DataColumn("NUMBER", typeof(string)));
                ldt_TablaMensajes.Columns.Add(new DataColumn("TYPE", typeof(string)));


                for (int i = 0; MensajesRespuesta.Count() > i; i++)
                {
                    DataRow dr = ldt_TablaMensajes.NewRow();
                    dr["ID"] = MensajesRespuesta[i].ID;
                    dr["MESSAGE"] = MensajesRespuesta[i].MESSAGE;
                    dr["NUMBER"] = MensajesRespuesta[i].NUMBER;
                    dr["TYPE"] = MensajesRespuesta[i].TYPE;
                    ldt_TablaMensajes.Rows.Add(dr);
                }
                lds_Resultado.Tables.Add(ldt_TablaMensajes);
            }
            catch (Exception exc)
            {
                DataTable ldt_TablaMensajes = new DataTable("Table");
                ldt_TablaMensajes.Columns.Add(new DataColumn("ID", typeof(string)));
                ldt_TablaMensajes.Columns.Add(new DataColumn("MESSAGE", typeof(string)));
                ldt_TablaMensajes.Columns.Add(new DataColumn("NUMBER", typeof(string)));
                ldt_TablaMensajes.Columns.Add(new DataColumn("TYPE", typeof(string)));

                DataRow dr = ldt_TablaMensajes.NewRow();
                dr["ID"] = "99";
                dr["MESSAGE"] = exc.Message;
                dr["TYPE"] = "Z";
                ldt_TablaMensajes.Rows.Add(dr);

                lds_Resultado.Tables.Add(ldt_TablaMensajes);
            }

            return lds_Resultado;
        }
    }
}