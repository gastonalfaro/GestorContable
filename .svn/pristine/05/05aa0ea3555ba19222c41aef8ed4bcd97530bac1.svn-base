using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio.CalculosFinancieros.DeudaInterna;
using LogicaNegocio.CapturaIngresos;
using LogicaNegocio.Mantenimiento;
using LogicaNegocio.Seguridad;
using LogicaNegocio.Contingentes;

namespace SGDiferencialCambiario
{
    class DifCambiarioDeudaInterna
    {
        //private static wsSG.wsSistemaGestor ws_SGService = new wsSG.wsSistemaGestor();
        //private static wsAsientos.ServicioContable asientos = new wsAsientos.ServicioContable();

        private static void CalculoDiferencialCambiarioContingentes()
        {
            DataSet lds_ConsultaTitulos = new DataSet();
            DataTable ldt_TitulosValores = new DataTable();
            clsTituloValor lcl_TituloValor = new clsTituloValor();
            lds_ConsultaTitulos = lcl_TituloValor.ConsultarTituloValor(null, "", "", "", "", "", "", "", Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/5000"), "");
            if (lds_ConsultaTitulos != null)
            {
                ldt_TitulosValores = lds_ConsultaTitulos.Tables[0];
            }

            if (ldt_TitulosValores.Rows.Count > 0)
            {
                foreach (DataRow dr_TituloValor in ldt_TitulosValores.Rows)
                {

                    //Console.WriteLine("NUEVO EXPEDIENTE");
                    //Console.Write("Expediente Num: ");
                    //Console.WriteLine(dr_TituloValor["IdExpedienteFK"].ToString());
                    string resultado = String.Empty;
                    decimal ldec_DifValorFacial = 0;
                    decimal ldec_DifValorTransadoBruto = 0;
                    decimal ldec_DifValorTransadoNeto = 0;
                    decimal ldec_DifRendimientoPorDescuento = 0;
                    decimal ldec_DifImpuestoVencido = 0;
                    decimal ldec_DifImpuestoPagado = 0;
                    decimal ldec_DifImpuestoEfectivo = 0;
                    decimal ldec_DifPremio = 0;
                    string idModulo = "IdModulo In ('DI')";
                    string lstr_Operacion = String.Empty;
                    string lstr_NroValor = dr_TituloValor["NroValor"].ToString();
                    decimal ldec_ValorFacial = Convert.ToDecimal(dr_TituloValor["ValorFacialColones"].ToString());
                    decimal ldec_ValorFacialCierre = Convert.ToDecimal(dr_TituloValor["ValorFacialColonesCierre"].ToString());
                    decimal ldec_ValorTransadoBruto = Convert.ToDecimal(dr_TituloValor["ValorTransadoBrutoColones"].ToString());
                    decimal ldec_ValorTransadoBrutoCierre = Convert.ToDecimal(dr_TituloValor["ValorTransadoBrutoColonesCierre"].ToString());
                    decimal ldec_ValorTransadoNeto = Convert.ToDecimal(dr_TituloValor["ValorTransadoNetoColones"].ToString());
                    decimal ldec_ValorTransadoNetoCierre = Convert.ToDecimal(dr_TituloValor["ValorTransadoNetoColonesCierre"].ToString());
                    decimal ldec_RendimientoPorDescuento = Convert.ToDecimal(dr_TituloValor["RendimientoPorDescuentoColones"].ToString());
                    decimal ldec_RendimientoPorDescuentoCierre = Convert.ToDecimal(dr_TituloValor["RendimientoPorDescuentoColonesCierre"].ToString());
                    decimal ldec_ImpuestoVencido = Convert.ToDecimal(dr_TituloValor["ImpuestoVencidoColones"].ToString());
                    decimal ldec_ImpuestoVencidoCierre = Convert.ToDecimal(dr_TituloValor["ImpuestoVencidoColonesCierre"].ToString());
                    decimal ldec_ImpuestoPagado = Convert.ToDecimal(dr_TituloValor["ImpuestoPagadoColones"].ToString());
                    decimal ldec_ImpuestoPagadoCierre = Convert.ToDecimal(dr_TituloValor["ImpuestoPagadoColonesCierre"].ToString());
                    decimal ldec_ImpuestoEfectivo = Convert.ToDecimal(dr_TituloValor["ImpuestoEfectivoColones"].ToString());
                    decimal ldec_ImpuestoEfectivoCierre = Convert.ToDecimal(dr_TituloValor["ImpuestoEfectivoColonesCierre"].ToString());
                    decimal ldec_Premio = Convert.ToDecimal(dr_TituloValor["PremioColones"].ToString());
                    decimal ldec_PremioCierre = Convert.ToDecimal(dr_TituloValor["PremioColonesCierre"].ToString());
                    ldec_DifValorFacial = ldec_ValorFacialCierre - ldec_ValorFacial;
                    ldec_DifValorTransadoBruto = ldec_ValorTransadoBrutoCierre - ldec_ValorTransadoBruto;
                    ldec_DifValorTransadoNeto = ldec_ValorTransadoNetoCierre - ldec_ValorTransadoNeto;
                    ldec_DifRendimientoPorDescuento = ldec_RendimientoPorDescuentoCierre - ldec_RendimientoPorDescuento;
                    ldec_DifImpuestoVencido = ldec_ImpuestoVencidoCierre - ldec_ImpuestoVencido;
                    ldec_DifImpuestoPagado = ldec_ImpuestoPagadoCierre - ldec_ImpuestoPagado;
                    ldec_DifImpuestoEfectivo = ldec_ImpuestoEfectivoCierre - ldec_ImpuestoEfectivoCierre;
                    ldec_DifPremio = ldec_PremioCierre - ldec_Premio;
                    //if (ConsultarTipoExpediente(lstr_NroValor).Equals("Demandado"))
                    //{
                    //    if (ldec_DifValorFacial > 0)
                    //    {
                    //        lstr_Operacion = "CT22";

                    //    }
                    //    else
                    //    {
                    //        ldec_DifValorFacial = ldec_DifValorFacial * -1;
                    //        ldec_ValorTransadoBruto = ldec_ValorTransadoBruto * -1;
                    //        ldec_DifValorTransadoNeto = ldec_DifValorTransadoNeto * -1;
                    //        ldec_DifRendimientoPorDescuento = ldec_DifRendimientoPorDescuento * -1;
                    //        ldec_DifImpuestoVencido = ldec_DifImpuestoVencido * -1;
                    //        lstr_Operacion = "CT23";
                    //    }
                    //    EnviarAsientosDemanda(idModulo, lstr_Operacion, lstr_NroValor, ldec_DifValorFacial,
                    //    ldec_ValorTransadoBruto, ldec_DifValorTransadoNeto, ldec_DifRendimientoPorDescuento, ldec_DifImpuestoVencido);

                    //}
                    //else if (ConsultarTipoExpediente(lstr_NroValor).Equals("Actor"))
                    //{
                    //    if (ldec_ValorFacialCierre > ldec_ValorFacial)
                    //    {
                    //        lstr_Operacion = "CT24";
                    //        resultado = EnviarAsientosActor(idModulo, lstr_Operacion, lstr_NroValor, ldec_DifValorFacial,
                    //    ldec_ValorTransadoBruto, ldec_DifValorTransadoNeto, ldec_DifRendimientoPorDescuento, ldec_DifImpuestoVencido);
                    //        Console.WriteLine(resultado);
                    //        Console.WriteLine("------------------------------------------------");

                    //    }
                    //    else
                    //    {
                    //        ldec_DifValorFacial = ldec_DifValorFacial * -1;
                    //        ldec_ValorTransadoBruto = ldec_ValorTransadoBruto * -1;
                    //        ldec_DifValorTransadoNeto = ldec_DifValorTransadoNeto * -1;
                    //        ldec_DifRendimientoPorDescuento = ldec_DifRendimientoPorDescuento * -1;
                    //        ldec_DifImpuestoVencido = ldec_DifImpuestoVencido * -1;
                    //        lstr_Operacion = "CT26";
                    //        resultado = EnviarAsientosActorMenor(idModulo, lstr_Operacion, lstr_NroValor, ldec_DifValorFacial,
                    //            ldec_ValorTransadoBruto);
                    //        Console.WriteLine(resultado);
                    //        Console.WriteLine("------------------------------------------------");
                    //    }
                    //}

                }
            }
        }
    }
}
