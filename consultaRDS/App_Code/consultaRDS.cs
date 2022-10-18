using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.ServiceModel;
using servicioSINPE;
/// <summary>
/// Summary description for consultaRDS
/// </summary>

[WebService(Namespace = "http://localhost/consultaRDS/consultaRDS.asmx/")]
//[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class consultaRDS : System.Web.Services.WebService {

    InformacionDeuda proxy;
    public consultaRDS () {

        proxy = new InformacionDeuda();
        proxy.SetPolicy("SINPE.Cliente");
    }

    [WebMethod]
    public Cupon[] ListarCuponesPorFechaCancelacion(string fechaCancelacion, string tipo)
    {
        return proxy.ListarCuponesPorFechaCancelacion(DateTime.Parse(fechaCancelacion), (tipo.ToUpper() == "RDI") ? CodigoServicio.Rdi : ((tipo.ToUpper() == "RDE") ? CodigoServicio.Rde : CodigoServicio.Rdd));
    }

    [WebMethod]
    public Cupon[] ListarCuponesPorFechaValor(string fechaValor, string tipo)
    {
        return proxy.ListarCuponesPorFechaValor(DateTime.Parse(fechaValor), (tipo.ToUpper() == "RDI") ? CodigoServicio.Rdi : ((tipo.ToUpper() == "RDE") ? CodigoServicio.Rde : CodigoServicio.Rdd));
    }

    [WebMethod]
    public Cupon[] ListarCuponesPorFechaConstitucion(string fechaConstitucion, string tipo)
    {
        return proxy.ListarCuponesPorFechaConstitucion(DateTime.Parse(fechaConstitucion), (tipo.ToUpper() == "RDI") ? CodigoServicio.Rdi : ((tipo.ToUpper() == "RDE") ? CodigoServicio.Rde : CodigoServicio.Rdd));
    }

    [WebMethod]
    public Valor[] ListarValoresPorFechaCancelacion(string fechaCancelacion, string tipo)
    {
        return proxy.ListarValoresPorFechaCancelacion(DateTime.Parse(fechaCancelacion), (tipo.ToUpper() == "RDI") ? CodigoServicio.Rdi : ((tipo.ToUpper() == "RDE") ? CodigoServicio.Rde : CodigoServicio.Rdd));
    }

    [WebMethod]
    public Valor[] ListarValoresPorFechaConstitucion(string fechaConstitucion, string tipo)
    {
        return proxy.ListarValoresPorFechaConstitucion(DateTime.Parse(fechaConstitucion), (tipo.ToUpper() == "RDI") ? CodigoServicio.Rdi : ((tipo.ToUpper() == "RDE") ? CodigoServicio.Rde : CodigoServicio.Rdd));
    }

    [WebMethod]
    public Valor[] ListarValoresPorFechaValor(string fechaValor, string tipo)
    {
        return proxy.ListarValoresPorFechaValor(DateTime.Parse(fechaValor), (tipo.ToUpper() == "RDI") ? CodigoServicio.Rdi : ((tipo.ToUpper() == "RDE") ? CodigoServicio.Rde : CodigoServicio.Rdd));
    }

    [WebMethod]
    public Saldo[] ResumenSaldos(string fecha, string tipo)
    {
        return proxy.ResumenSaldos(DateTime.Parse(fecha), (tipo.ToUpper() == "RDI") ? CodigoServicio.Rdi : ((tipo.ToUpper() == "RDE") ? CodigoServicio.Rde : CodigoServicio.Rdd));
    }    
    
}
