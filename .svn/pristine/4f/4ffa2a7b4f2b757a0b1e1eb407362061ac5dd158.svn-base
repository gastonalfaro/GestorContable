using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LogicaNegocio.Consolidacion;
using System.Data;

namespace Presentacion.Consolidacion
{
    public partial class RevisionEntidad : BASE
    {

        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        //private Presentacion.wsBC.wsBalanceComprobacionSAP wsBalanceComprobacion = new Presentacion.wsBC.wsBalanceComprobacionSAP();
        //private Presentacion.wsBC.tBalanceComprobacionCabecera wsbc_Cabecera = new Presentacion.wsBC.tBalanceComprobacionCabecera();
        //private Presentacion.wsBC.tBalanceComprobacionPosicion wsbc_Posicion = new Presentacion.wsBC.tBalanceComprobacionPosicion();
        //private Presentacion.wsBC.tBalanceComprobacionPosicion[] wsbc_Posiciones = new Presentacion.wsBC.tBalanceComprobacionPosicion[2];
        private Presentacion.wsBC.wsBalanceComprobacion wsBalanceComprobacion = new Presentacion.wsBC.wsBalanceComprobacion();
        private Presentacion.wsBC.ZINT_EST_CAB_BALANCE_CONSOL wsbc_Cabecera = new Presentacion.wsBC.ZINT_EST_CAB_BALANCE_CONSOL();
        private Presentacion.wsBC.ZINT_EST_POS_BALANCE_CONSOL wsbc_Posicion = new Presentacion.wsBC.ZINT_EST_POS_BALANCE_CONSOL();
        private Presentacion.wsBC.ZINT_EST_POS_BALANCE_CONSOL[] wsbc_Posiciones = new Presentacion.wsBC.ZINT_EST_POS_BALANCE_CONSOL[2];

        static DataSet gds_Mensajes = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnBalanceComprobacionSAP_Click(object sender, EventArgs e)
        {
            ConsultarAreasFuncionales();
        }

        private void ConsultarAreasFuncionales()
        {

            try
            {
                #region Prueba
                wsbc_Cabecera.LEDGER = "CS";
                wsbc_Cabecera.VISTA = "01";
                wsbc_Cabecera.VERSION = "100";
                wsbc_Cabecera.EJERCICIO = "2015";
                wsbc_Cabecera.PERIODO = "10";
                wsbc_Cabecera.UNID_CONSOL = "12820";
                wsbc_Cabecera.PLAN_POS = "P1";

                wsbc_Posicion.POSICION = "1110201010";
                wsbc_Posicion.SUBPOSICION = "13";
                wsbc_Posicion.UNID_ASOCIADA = "";
                wsbc_Posicion.MONEDA = "CRC";
                wsbc_Posicion.SIGNO = "+";
                wsbc_Posicion.VALOR_ML = 100000;
                wsbc_Posicion.VALOR_MT = 100000;
                wsbc_Posicion.VALOR_AC = 100000;
                wsbc_Posiciones[0] = wsbc_Posicion;

                wsbc_Posicion.POSICION = "1110201020";
                wsbc_Posicion.SUBPOSICION = "13";
                wsbc_Posicion.UNID_ASOCIADA = "12751";
                wsbc_Posicion.MONEDA = "CRC";
                wsbc_Posicion.SIGNO = "-";
                wsbc_Posicion.VALOR_ML = 100000;
                wsbc_Posicion.VALOR_MT = 100000;
                wsbc_Posicion.VALOR_AC = 100000;
                wsbc_Posiciones[1] = wsbc_Posicion;
                #endregion

                gds_Mensajes = wsBalanceComprobacion.uwsBalanceComprobacion(wsbc_Cabecera, wsbc_Posiciones);

                if (gds_Mensajes.Tables.Count > 0)
                {
                    
                }

                if (gds_Mensajes.Tables["Table"].Rows.Count > 0)
                {
                    grdBalanceRespuesta.DataSource = gds_Mensajes.Tables["Table"];
                    grdBalanceRespuesta.DataBind();
                }
                else
                {
                    grdBalanceRespuesta.DataSource = this.LlenarTablaVacia();
                    grdBalanceRespuesta.DataBind();
                    grdBalanceRespuesta.Rows[0].Visible = false;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaMensajes = new DataTable("Table");
            ldt_TablaMensajes.Columns.Add(new DataColumn("ID", typeof(string)));
            ldt_TablaMensajes.Columns.Add(new DataColumn("MESSAGE", typeof(string)));
            ldt_TablaMensajes.Columns.Add(new DataColumn("NUMBER", typeof(string)));
            ldt_TablaMensajes.Columns.Add(new DataColumn("TYPE", typeof(string)));

            DataRow ldr_FilaTabla = ldt_TablaMensajes.NewRow();
            ldt_TablaMensajes.Rows.Add(ldr_FilaTabla);
            return ldt_TablaMensajes;
            
        }

    }
}