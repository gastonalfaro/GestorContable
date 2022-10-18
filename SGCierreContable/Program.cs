using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio.CalculosFinancieros.DeudaExterna;
using LogicaNegocio.CalculosFinancieros.DeudaInterna;
using LogicaNegocio.CapturaIngresos;
using LogicaNegocio.Mantenimiento;
using LogicaNegocio.Seguridad;
using LogicaNegocio.Contingentes;
using log4net;
using log4net.Config;


namespace SGCierreContable
{
    class Program
    {        
        static void Main(string[] args)
        {
            int res = 0;
            //clsCalculosDeudaInterna x = new clsCalculosDeudaInterna();
            //Double[] y = new Double[] 
            //{ 1820974.0, 3000.0, -5756.15, -10761.83 , 10000000.0, -43047.72,
            //                             30106798.94, -68271.3, -170289.13, -250355.44, 5000000.0,
            //                             -5000000.0, -381598.97, 15000000.0, -500000.0, -562983.37,
            //                             -2267795.17, -576880.87, -552924.55, -547297.23, -523823.26,
            //                             -517713.6, -497455.24, -488129.96, -465620.68, -458546.33, -436519.39, -428962.7};
            //            //{ -1820974.0, -3000.0, 5756.15, 10761.83, -10000000.0, 43047.72,
            //            //                 -30106798.94, 68271.3, 170289.13, 250355.44, -5000000.0,
            //            //                 5000000.0, 381598.97, -15000000.0, 500000.0, 562983.37,
            //            //                 2267795.17, 576880.87, 552924.55, 547297.23, 523823.26,
            //            //                 517713.6, 497455.24, 488129.96, 465620.68, 458546.33, 436519.39, 428962.7};
            //Double z = x.CalculoIRR(y);//28);

            //decimal w = x.CalculoTIR(y);

            clsTiposAsiento ta = new clsTiposAsiento();
            try
            {
                res = ta.EnviarAsientosCI(null, -1);
            }
            catch(Exception e)
            {

            }
            try
            {
                res = ta.EnviarAsientosCICT(null, -1);
            }
            catch (Exception e)
            {

            }
        }//main
    }//class
}//namespace
