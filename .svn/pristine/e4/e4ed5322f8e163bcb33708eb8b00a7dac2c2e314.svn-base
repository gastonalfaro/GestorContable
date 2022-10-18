using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Presentacion.Compartidas;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Presentacion
{
    public partial class Principal : BASE
    {
        private Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
		private string gstr_Usuario = String.Empty;
        private String[] garr_permisos;
        private string gstr_ModuloActual = String.Empty;
        
        protected void Page_Load(object sender, EventArgs e)
        {            
            gstr_Usuario = clsSesion.Current.LoginUsuario;

            if (!String.IsNullOrEmpty(gstr_Usuario))
            {
                clsSesion.Current.gbool_Permisos = false;
                clsSeguridadVistas.MostrarElementos(gstr_Usuario, Master, "");
                if (Request.QueryString.ToString().Trim().Equals("card=si"))
                {
                    Session["FIRMA_DIGITAL_LOGIN"] = true;
                }
            }
            else
                Response.Redirect("~/Login.aspx", true);
            
        }

        protected void Unnamed_ServerClick1(object sender, EventArgs e)
        {   
            DataSet ldt_PermisosUsuario = ws_SGService.uwsConsultarPermisosUsuarios(gstr_Usuario, "");


            for (int i = 0; ldt_PermisosUsuario.Tables["Table"].Rows.Count > i; i++)
            {
                string lstr_IdObjeto = ldt_PermisosUsuario.Tables["Table"].Rows[i]["IdObjeto"].ToString();
                bool lbool_Consultar = (bool)ldt_PermisosUsuario.Tables["Table"].Rows[i]["Consultar"];

                if (lbool_Consultar)
                {
                    if (lstr_IdObjeto.Equals("frmCargaEntidad"))
                    {
                        Response.Redirect("~/Consolidacion/frmCargaEntidad.aspx", true);
                    }
                    else if (lstr_IdObjeto.Equals("frmRevisionAnalista"))
                    {
                        Response.Redirect("~/Consolidacion/frmRevisionAnalista.aspx", true);
                    }
                    else if (lstr_IdObjeto.Equals("frmRevisionEntidad"))
                    {
                        Response.Redirect("~/Consolidacion/frmRevisionEntidad.aspx", true);
                    }
                }
            }           
        }

        protected void Unnamed_ServerClick2(object sender, EventArgs e)
        {
            clsSesion.Current.gstr_ModuloActual = "OBJ_MA";
            Response.Redirect("~/Mantenimiento/frmParametros.aspx", true);
        }

    }
}