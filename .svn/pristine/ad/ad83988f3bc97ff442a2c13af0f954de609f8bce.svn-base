using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using LogicaNegocio.Consolidacion;

namespace WebServiceBalanceComprobacionSAP
{
    /// <summary>
    /// Summary description for wsBalanceComprobacionSAP
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class wsBalanceComprobacionSAP : System.Web.Services.WebService
    {
        clsConexionSAP cxnSAP = new clsConexionSAP();

        [WebMethod]
        public String[] uwsBalanceComprobacion(tBalanceComprobacionCabecera t_Cabecera, tBalanceComprobacionPosicion[] t_Posicion)
        {
            String[] lstr_Respuesta = cxnSAP.RecibeBalanceComprobacion(t_Cabecera, t_Posicion);

            return lstr_Respuesta;
        }
    }
}
