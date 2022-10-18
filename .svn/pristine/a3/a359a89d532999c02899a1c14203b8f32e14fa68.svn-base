using Presentacion.Compartidas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.CalculosFinancieros
{
    public partial class NotasPoderEjecutivo : System.Web.UI.Page
    {
        private static Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();

        /// <summary>
        /// Lista de archivos a subir
        /// </summary>
        public clsListaArchivos g_ListaArchivos
        {
            get
            {
                if (this.ViewState["Archivos"] == null)
                    return null;

                return (clsListaArchivos)this.ViewState["Archivos"];
            }
            set { this.ViewState["Archivos"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void CargarDDLs()
        {   
            DataTable ldt_OpcionesCatalogos = ws_SGService.uwsConsultarOpcionesCatalogo("51","","","").Tables[0];
            ddlCategoria.DataSource = ldt_OpcionesCatalogos;
            ddlCategoria.DataTextField = "NomOpcion";
            ddlCategoria.DataValueField = "ValOpcion";
            ddlCategoria.DataBind();
        }

        private bool AdjuntarArchivos(long int_IdRev, bool boo_EstaPendiente)
        {
            bool lboo_Resultado = false;

            if (!boo_EstaPendiente)
            {
                foreach (clsArchivos Archivo in g_ListaArchivos.L_ListaArchivos)
                {
                    ws_SGService.uwsGuardarArchivo(Archivo.Lstr_Nombre, Archivo.Lstr_TipoContenido, Archivo.Lint_Tamano,
                        Archivo.Lbyt_Datos, int_IdRev, "", 0, "", 0, "", 0, clsSesion.Current.LoginUsuario);
                }
            }
            else
            {
                foreach (clsArchivos Archivo in g_ListaArchivos.L_ListaArchivos)
                {
                    ws_SGService.uwsGuardarArchivo(Archivo.Lstr_Nombre, Archivo.Lstr_TipoContenido, Archivo.Lint_Tamano,
                        Archivo.Lbyt_Datos, 0, "", 0, "", int_IdRev, "", 0, clsSesion.Current.LoginUsuario);
                }
            }
            lboo_Resultado = true;

            return lboo_Resultado;
        }
    }
}