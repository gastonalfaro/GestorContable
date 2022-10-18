using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio.CalculosFinancieros.DeudaExterna;
using LogicaNegocio.Seguridad;
using LogicaNegocio.Mantenimiento;

namespace SGTipoCambio
{
    class Program
    {
        //private static SGTpoCambio.wsSG.wsSistemaGestor ws_SGService = new SGTpoCambio.wsSG.wsSistemaGestor();

        private static tBitacora bit = new tBitacora();
        private static clsTiposCambio tCambio = new clsTiposCambio();
        private static clsValoresIndicadoresEco iEco = new clsValoresIndicadoresEco();

        static void Main(string[] args)
        {
            try
            {
                tCambio.CargarTiposCambio(DateTime.Today.AddDays(-1).ToString("dd/MM/yyyy"));
                tCambio.ActualizarTiposCambio();
                //ws_SGService.CargarTiposCambio(DateTime.Today.AddDays(-1).ToString("dd/MM/yyyy"));
                //ws_SGService.ActualizarTiposCambio();
                iEco.CargarIndicadoresEco(DateTime.Today.AddDays(-1).ToString("dd/MM/yyyy"));
                iEco.ActualizarIndicadoresEco();
                //ws_SGService.CargarIndicadoresEco(DateTime.Today.AddDays(-1).ToString("dd/MM/yyyy"));
                //ws_SGService.ActualizarIndicadoresEco();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.ToString());
                Console.ReadLine().ToString();
                bit.ufnRegistrarAccionBitacora("MA", "1", "Proceso", "Error al actualizar tipos de cambio y valores de indicadores económicos " + ex.ToString(), "TC", "Actualizar", "11206");
            }
        }
    }
}
