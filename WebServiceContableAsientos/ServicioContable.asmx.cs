using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebServiceContableAsientos
{
    /// <summary>
    /// Summary description for ServicioContable
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ServicioContable : System.Web.Services.WebService
    {

        [WebMethod]
        public String[] EnviarAsientos(WebServiceContableAsientos.wsCC.ZfiAsiento[] tabla_asientos, string str_Test = "")
        {

            //llamada a metodo
            cls_CargaContableAsientos llamadaWSSAP = new cls_CargaContableAsientos();
            return llamadaWSSAP.EnviarAsientos(tabla_asientos, str_Test );
            //return "Hello World";
        }
    }
}
