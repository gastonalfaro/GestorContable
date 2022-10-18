using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGAsientosDeudaInterna
{
    public class Program
    {
        static void Main(string[] args)
        {
            Cancelacion can = new Cancelacion();
            PrescripcionTitulos pres = new PrescripcionTitulos();
            CostosTransaccion cos = new CostosTransaccion();
            DiferencialCambiario dif = new DiferencialCambiario();
            TitulosReclasificacion tit = new TitulosReclasificacion();
            CancelacionesAnticipadas cant = new CancelacionesAnticipadas();

            try
            {
                //can.Cancelaciones();
                //pres.PrescripcionesTitulos();
                cos.GeneraCostosTransaccion();
                //dif.DiferencialCambiarios();
                //tit.TitulosReclasificados();
                //cant.CancelacionAnticipada();

                //Cancelacion.Cancelaciones();
                //PrescripcionTitulos.PrescripcionesTitulos();
                //CostosTransaccion.GeneraCostosTransaccion();
                //DiferencialCambiario.DiferencialCambiarios();
                //TitulosReclasificacion.TitulosReclasificados();
                //CancelacionesAnticipadas.CancelacionAnticipada();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.ToString());
                Console.ReadLine().ToString();
            }
        }


    }
}
