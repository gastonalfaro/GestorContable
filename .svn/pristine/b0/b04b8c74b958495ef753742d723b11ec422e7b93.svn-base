using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Presentacion.Compartidas
{
    public class clsSeguridadVistas
    {
        private static Presentacion.wsSG.wsSistemaGestor ws_SGService = new Presentacion.wsSG.wsSistemaGestor();
        private static Boolean gbool_Permisos;

        public static Control FindControlRecursive(Control root, string id)
        {
            if (id == string.Empty)
                return null;

            if (root.ID == id)
                return root;

            foreach (Control c in root.Controls)
            {
                Control t = FindControlRecursive(c, id);
                if (t != null)
                {
                    return t;
                }
            }
            return null;
        }

        public static bool MostrarElementos(string str_usuario, MasterPage master_page, string gstr_ModuloActual)
        {
            gbool_Permisos = clsSesion.Current.gbool_Permisos;
            bool lbool_result = false;
            try
            {
                DataSet ldt_PermisosUsuario = new DataSet();
                String[] larr_PermisosModulos = new String[15];
                int lint_cont = 0;

                ldt_PermisosUsuario = ws_SGService.uwsConsultarPermisosUsuarios(str_usuario, gstr_ModuloActual);
                
                for (int i = 0; ldt_PermisosUsuario.Tables["Table"].Rows.Count > i; i++)
                {
                    if ((bool)ldt_PermisosUsuario.Tables["Table"].Rows[i]["Actualizar"])
                    {
                        lbool_result = true;
                        break;
                    }
                } 

                if (lbool_result)
                {
                    if (!string.IsNullOrEmpty(gstr_ModuloActual))
                        ldt_PermisosUsuario = ws_SGService.uwsConsultarPermisosUsuarios(str_usuario, "");

                    for (int i = 0; ldt_PermisosUsuario.Tables["Table"].Rows.Count > i; i++)
                    {
                        string lstr_IdObjeto = ldt_PermisosUsuario.Tables["Table"].Rows[i]["IdObjeto"].ToString();
                        bool lbool_Actualizar = (bool)ldt_PermisosUsuario.Tables["Table"].Rows[i]["Actualizar"];
                        string lstr_IdliEncabezado = "li" + lstr_IdObjeto;

                        if (lbool_Actualizar)
                        {
                            try
                            {
                                HtmlGenericControl hgcMenuEncabezado = (HtmlGenericControl)FindControlRecursive(master_page.Page, lstr_IdliEncabezado);

                                if (hgcMenuEncabezado != null)
                                {
                                    hgcMenuEncabezado.Style["visibility"] = "visible";
                                    hgcMenuEncabezado.Visible = true;
                                }
                            }
                            catch 
                            { 
                                try
                                {
                                    WebControl hgcMenuEncabezado = (WebControl)FindControlRecursive(master_page.Page, lstr_IdliEncabezado);

                                    if (hgcMenuEncabezado != null)
                                        hgcMenuEncabezado.Visible = true;
                                }
                                catch{}
                            }

                            if ((lstr_IdObjeto.StartsWith("OBJ_"))
                                && (lstr_IdObjeto.Count() == 6)
                                && (!gbool_Permisos)
                                && (string.IsNullOrEmpty(gstr_ModuloActual))
                                && (!larr_PermisosModulos.Contains(lstr_IdObjeto.Remove(0, 4))))
                            {
                                string str_modulo = lstr_IdObjeto.Remove(0, 4);
                                
                                    larr_PermisosModulos.SetValue(str_modulo, lint_cont);
                                    lint_cont++;

                                    if (str_modulo.Equals("MA"))
                                    {
                                       clsSesion.Current.gstr_ModuloActual = "MA";
                                    }

                                    if (str_modulo.Equals("DE"))
                                    {
                                        larr_PermisosModulos.SetValue("CF", lint_cont);
                                        lint_cont++;
                                    }
                                    

                            }
                        }
                    }
                    if (string.IsNullOrEmpty(gstr_ModuloActual) && (!gbool_Permisos))
                    {
                        clsSesion.Current.PermisosModulos = larr_PermisosModulos;
                        clsSesion.Current.gbool_Permisos = true;
                    }
                }
            }
            catch { }
            return lbool_result;
        }
    }
}