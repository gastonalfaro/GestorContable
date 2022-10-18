using Presentacion.Compartidas;
using LogicaNegocio.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentacion
{
    /// <summary>
    /// Página base de todas las páginas de la solución para facilitar el acceso a funciones o atributos en común
    /// </summary>
    public abstract class BASE : System.Web.UI.Page
    {
        private tSeguridad Seguridad = new tSeguridad();

        protected override void OnError(EventArgs e)
        {
            if (Context.Error != null)
            {
                System.Exception vExcepcion = (Context.Error is System.Web.HttpUnhandledException) ? Context.Error.InnerException : Context.Error;

                if (clsSesion.Current.LoginUsuario.Equals(string.Empty))
                    Seguridad.SaveError(vExcepcion);
                else
                    Seguridad.SaveError(new System.Exception(clsSesion.Current.NomUsuario.ToString() + Environment.NewLine, vExcepcion));
            }
            //
            //fin
            this.Response.Redirect("/Error.aspx");
        }
    }   
}