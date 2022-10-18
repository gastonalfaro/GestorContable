using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Diagnostics;
using System.Configuration;
using System.Reflection;
using System.Data;
using System.IO;
using System.Net;
using System.Data.SqlTypes;
using System.Globalization;
using Microsoft.Reporting.WebForms;
using eWorld.UI;
using Presentacion.Compartidas;
using Presentacion.wsPC;
using Presentacion.wsSG;
using System.Xml;
using System.Net.Mail;
using System.Text;
using System.Net.Mime;
using System.Web.UI.HtmlControls;
using LogicaNegocio.Seguridad;
using System.Windows.Forms;
using System.Drawing;



namespace Presentacion.Mantenimiento
{
    public partial class frmCodigoSegmento : BASE
    {
        // # region Variables
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        string accion = String.Empty;
        string gstr_Usuario = String.Empty;
        //string gstr_Usuario = clsSesion.Current.LoginUsuario;
        //string gstr_Institucion = clsSesion.Current.SociedadUsr;
        
       protected void Page_Load(object sender, EventArgs e)
        {
            DataSet dsRegistrosIngresados = ws_SGService.uwsConsultarDinamico("select * from [ma].[Direcciones]");
            string RegistrosIncluidos = dsRegistrosIngresados.Tables[0].Rows[0][0].ToString();

            gstr_Usuario = clsSesion.Current.LoginUsuario;
            if (!IsPostBack)
            {
                CargarEntidades();
                CargarCodSegmento("*","");
                txtCodSegmento.Enabled = false;
                btnGuardarCodSegmento.Enabled = false;

            }
        }
        protected void CargarEntidades()
        {

 
            DataSet dsEntidades = ws_SGService.uwsConsultarDinamico("select ltrim(rtrim(IdUnidadConsolidacion)) as IdSociedadGL,(rtrim(IdUnidadConsolidacion) + ' - ' + NomUnidad) as CodyNombre from ma.UnidadesConsolidacion " +
                                                                               "where SUBSTRING(IdUnidadConsolidacion,1,1) in('1','2','3','4','5','6') and Vista = '01' and Estado = 'A'");

            cboEntidadDropDownList.DataSource = dsEntidades; //ws_SGService.uwsConsultarSociedadesGL(null, null, null, null, null);

//            cboEntidadDropDownList.DataSource = ws_SGService.uwsConsultarSociedadesGL(null, null, null, null, null);
            cboEntidadDropDownList.DataBind();
            cboEntidadDropDownList.Items.Insert(0, new ListItem("--Seleccione una entidad--", "0"));
        }

        protected void CargarCodSegmento(string IdEntidad, string pBuscar)
        {
            grdvCodigoSegmento.DataSource = ws_SGService.uwsGetCodigosSegmento(IdEntidad, pBuscar);
            grdvCodigoSegmento.DataBind();
        }

        protected void btnGuardarCodSegmento_Click(object sender, EventArgs e)
        {
            if (cboEntidadDropDownList.SelectedItem.Value.Equals("0"))
            {
                Presentacion.Compartidas.MessageBox.Show("Debe elegir una entidad");
                cboEntidadDropDownList.Focus();
                txtCodSegmento.Text = "";
                txtCodSegmento.Enabled = false;
            }
            else
            {
                if (txtCodSegmento.Text.Trim().Equals(""))
                {
                    Presentacion.Compartidas.MessageBox.Show("El campo Segmento no puede quedar en blanco");
                    txtCodSegmento.Focus();
                }
                else
                {
                    string pIdInstitucion = cboEntidadDropDownList.SelectedItem.Value;
                    string pCodSegmento = txtCodSegmento.Text;

                    accion = GetAccion(pIdInstitucion);

                    Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
                    string sg = ws_SGService.uwsCrearActualizarCodSegmento(accion, pIdInstitucion, pCodSegmento, gstr_Usuario);

                    if (sg.Substring(0, 2).Equals("00"))
                    {
                        Presentacion.Compartidas.MessageBox.Show("Asociación creada con éxito");
                        txtCodSegmento.Text = "";
                        CargarEntidades();
                        CargarCodSegmento("*","");

                    }
                    else
                    {
                        Presentacion.Compartidas.MessageBox.Show(sg);
                        //txtCodSegmento.Text = "";
                    }
                }
            }
        }

        protected void grdvCodigoSegmento_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = grdvCodigoSegmento.SelectedRow;
            string IdSociedad = Convert.ToString(grdvCodigoSegmento.DataKeys[row.RowIndex].Values["IdSociedad"]);
            string IdSegmento = Convert.ToString(grdvCodigoSegmento.DataKeys[row.RowIndex].Values["IdSegmento"]);
            cboEntidadDropDownList.SelectedValue = IdSociedad;
            txtCodSegmento.Text = IdSegmento;
            txtCodSegmento.Enabled = true;
            btnGuardarCodSegmento.Enabled = true;
            accion = "A";
            
        }

        private string GetAccion(string idEntidad)
        {
            //este proceso obtiene el codigo de segmento de una entidad
            DataSet CodSeg = ws_SGService.uwsGetCodigosSegmento(idEntidad, "");
            if (CodSeg.Tables[0].Rows.Count > 0)
            {
                string idSegmento = CodSeg.Tables[0].Rows[0]["IdSegmento"].ToString();
                return "A";
            }
            else
            {
                return "C";
            }

        }

        private void ObtieneCodSegmento(string idEntidad)
        {
            //este proceso obtiene el codigo de segmento de una entidad
            DataSet CodSeg = ws_SGService.uwsGetCodigosSegmento(idEntidad,"");
            string idSegmento = String.Empty;
            if (CodSeg.Tables[0].Rows.Count > 0)
            {
                idSegmento = CodSeg.Tables[0].Rows[0]["IdSegmento"].ToString();
                txtCodSegmento.Enabled = true;
                txtCodSegmento.Text = idSegmento;
                accion = "A";
            }
            else
            {
                accion = "C";
                txtCodSegmento.Enabled = true;
                txtCodSegmento.Text = "";
            }

            btnGuardarCodSegmento.Enabled = true;
        }

        protected void cboEntidadDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cboEntidadDropDownList.SelectedItem.Value.Equals("0"))
            {
                ObtieneCodSegmento(cboEntidadDropDownList.SelectedItem.Value);
                btnGuardarCodSegmento.Enabled = true;
            }
            else
            {
                txtCodSegmento.Text = "";
                txtCodSegmento.Enabled = false;
                btnGuardarCodSegmento.Enabled = false;
            }
        }

        protected void buscarAsociacionGVW(string IdEntidad)
        {

            foreach (GridViewRow Row in grdvCodigoSegmento.Rows)
            {
                if (Row.Cells[1].Text.Contains(IdEntidad))
                {
                    Row.BackColor = System.Drawing.Color.AliceBlue;
                }
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!txtBuscar.Text.Trim().Equals(""))
            {
                CargarCodSegmento("*",txtBuscar.Text);
            }
            else
            {
                CargarCodSegmento("*","");
            }
            txtBuscar.Text = "";
            
        }

        private void CargarInstituciones(string NomSociedad)
        {
            NomSociedad = "%" + NomSociedad + "%";
            string str_consul = "SELECT rtrim(IdSociedadGL) as IdSociedadGL, rtrim(IdSociedadGL) + ' - ' + NomSociedad as CodyNombre FROM ma.SociedadesGL WHERE WHERE NomSociedad like "+NomSociedad+" and Estado ='A' order by IdSociedadGL";

            cboEntidadDropDownList.DataSource = this.ws_SGService.uwsConsultarDinamico(str_consul);
            cboEntidadDropDownList.DataBind();


        }

        protected void grdvCodigoSegmento_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView gv = (GridView)sender;
            gv.PageIndex = e.NewPageIndex;
            CargarCodSegmento("*", "");
        }

        //protected void txtCodSegmento_TextChanged(object sender, EventArgs e)
        //{
        //    string x = txtCodSegmento.Text.Trim();
        //    if (txtCodSegmento.Text.Trim().Equals(""))
        //    {
        //        btnGuardarCodSegmento.Enabled = false;
        //    }
        //    else
        //    {
        //        btnGuardarCodSegmento.Enabled = false;
        //    }
        //}




    }
}