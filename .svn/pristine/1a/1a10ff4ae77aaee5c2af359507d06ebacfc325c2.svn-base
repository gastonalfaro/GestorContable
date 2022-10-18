using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace Presentacion.Mantenimiento
{
    public partial class frmCuentasBancarias : BASE
    {
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();

        protected DataSet gds_Datos
        {
            get
            {
                if (ViewState["gds_Datos"] == null)
                    ViewState["gds_Datos"] = new DataSet();
                return (DataSet)ViewState["gds_Datos"];
            }
            set
            {
                ViewState["gds_Datos"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
                CargarDatos(this.Request.QueryString["Num"]);
        }

        protected void btnCtaBancoVolver_Click(object sender, EventArgs e)
        {
            this.Response.Redirect("~/Mantenimiento/frmBancos.aspx");
        }

        private void CargarDatos(string pIDBanco)
        {
            //string str_IdBanco, string str_IdBancoPropio, string str_IdCuentaBancaria, string str_CuentaBancaria, string str_IdCuentaContable, string str_IdSociedadFi
            gds_Datos = ws_SGService.uwsConsultarBancosCuentas(pIDBanco, "", "", "", "","");

            if (gds_Datos.Tables["Table"].Rows.Count > 0)
                grdvCuentasBancos.DataSource = gds_Datos.Tables["Table"];
            else
               grdvCuentasBancos.DataSource = this.LlenarTablaVacia();
             
            grdvCuentasBancos.DataBind();
        }

        private DataTable LlenarTablaVacia()
        {
            DataTable ldt_TablaVacia = new DataTable();
            ldt_TablaVacia.Columns.Add("IDCuentaBancaria", typeof(string));
            ldt_TablaVacia.Columns.Add("CuentaBancaria", typeof(string));
            ldt_TablaVacia.Columns.Add("IDCuentaContable", typeof(string));
            ldt_TablaVacia.Columns.Add("IDSociedadFi", typeof(string));
            ldt_TablaVacia.Columns.Add("TipoCuenta", typeof(string));
            DataRow ldr_FilaTabla = ldt_TablaVacia.NewRow();
            ldt_TablaVacia.Rows.Add(ldr_FilaTabla);
            return ldt_TablaVacia;
        }

        protected void grdvBancos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdvCuentasBancos.PageIndex = e.NewPageIndex;
            grdvCuentasBancos.DataSource = gds_Datos.Tables["Table"];

            grdvCuentasBancos.DataBind();
            //CargarDatos("");
        }
    }
}