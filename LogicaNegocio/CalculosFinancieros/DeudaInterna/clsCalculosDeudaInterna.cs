
using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Datos.ConexionSQL;
using Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaExterna;
using Datos.ConexionSQL.Procedimientos.CalculosFinancieros.DeudaInterna;
using System.Data;
using log4net;
using log4net.Config;
using System.Data.OleDb;
using System.Globalization;
using System.Collections;
using Microsoft.VisualBasic;
using LogicaNegocio.wsDeudaInterna;
using LogicaNegocio.Mantenimiento;
//using Microsoft.Office.Interop.Excel;

namespace LogicaNegocio.CalculosFinancieros.DeudaInterna
{
    public class clsCalculosDeudaInterna
    {
        private static Seguridad.tBitacora bitacora = new Seguridad.tBitacora();
        private static Mantenimiento.clsTiposCambio tipocambio = new Mantenimiento.clsTiposCambio();
        private static Mantenimiento.clsDinamico dinamico = new Mantenimiento.clsDinamico();
        private static tiras tira = new tiras();
        //private wsAsientos.ServicioContable asientos = new wsAsientos.ServicioContable();
        private static clsTiposAsiento tasientos = new clsTiposAsiento();

        private static clsOperaciones loperacion = new clsOperaciones();
        private wsBccrRds1.Extractos wsBccrRds = new wsBccrRds1.Extractos();
        
       

        private static string lstr_separador_decimal = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;
        #region Constructor
        public clsCalculosDeudaInterna()
        {
            CultureInfo culture;
            culture = CultureInfo.CreateSpecificCulture("es-CR");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }
        private decimal Truncate(decimal value, int length)
        {
            return Math.Truncate(value * 100) / 100;
        }
        #endregion

        #region variables
        private int lint_ContPeriodos = 0;
        private decimal[] gdec_TasaFija;
        private decimal[] gdec_TasaVariable;
        private DateTime[] gdt_Fechas;
        private DateTime gdt_FchUltimoPago;
        private int gint_DiasCuponCorrido;

        private string[] fechas;
        private int meses = 0;
        private int mesesDev = 0;
        private string lstr_ResultadoAsientoCanjeSubasta = "";
        private DateTime FchValor;
        //private DateTime ProxFecha;
        private DateTime[] ProxFecha;
        private decimal IntPrimerDevengo;
        private decimal PagoPrimerDevengo;

        private decimal[] VIntPrimerDevengo;
        private decimal[] VPagoPrimerDevengo;
        private decimal[] VFlujoEfectivo;
        private int Periodicidad;

        private string[] FchTemp;
        private double LOW_RATE = 0.01;
        private double HIGH_RATE = 0.99;
        private double MAX_ITERATION = 1000;
        private double PRECISION_REQ = 0.00000001;
        private decimal FlujoUnCupon = 0;


        #endregion

        #region Obtención de cupones, valores y saldos

        public wsBccrRds1.Cupon[] CuponesPorFchCancelacion(DateTime ldt_FchCancelacion, string lstr_TipoDeuda)
        {
            try
            {
                return wsBccrRds.ListarCuponesPorFechaCancelacion(ldt_FchCancelacion, lstr_TipoDeuda);
            }
            catch(Exception ex)
            {
                string err = ex.StackTrace.ToString();
                return null;
            }
        }

        public wsBccrRds1.Cupon[] CuponesPorFchConstitucion(DateTime ldt_FchConstitucion, string lstr_TipoDeuda)
        {
            try
            {
                return wsBccrRds.ListarCuponesPorFechaConstitucion(ldt_FchConstitucion, lstr_TipoDeuda);
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        public wsBccrRds1.Cupon[] CuponesPorFchValor(DateTime ldt_FchValor, string lstr_TipoDeuda)
        {
            try
            {
                wsBccrRds1.Extractos ws = new wsBccrRds1.Extractos();
                return ws.ListarCuponesPorFechaValor(ldt_FchValor, lstr_TipoDeuda);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public wsBccrRds1.Valor[] ValoresPorFchCancelacion(DateTime ldt_FchCancelacion, string lstr_TipoDeuda)
        {
            try
            {
                return wsBccrRds.ListarValoresPorFechaCancelacion(ldt_FchCancelacion, lstr_TipoDeuda);
            }
            catch
            {
                return null;
            }
        }

        public wsBccrRds1.Valor[] ValoresPorFchConstitucion(DateTime ldt_FchConstitucion, string lstr_TipoDeuda)
        {
            try
            {
                return wsBccrRds.ListarValoresPorFechaConstitucion(ldt_FchConstitucion, lstr_TipoDeuda);
            }
            catch
            {
                return null;
            }
        }

        public wsBccrRds1.Valor[] ValoresPorFchValor(DateTime ldt_FchValor, string lstr_TipoDeuda)
        {
            try
            {
                wsBccrRds1.Extractos ws = new wsBccrRds1.Extractos();
                return ws.ListarValoresPorFechaValor(ldt_FchValor, lstr_TipoDeuda);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public wsBccrRds1.Saldo[] ResumenSaldos(DateTime ldt_Fecha, string lstr_TipoDeuda)
        {
            try
            {
                return wsBccrRds.ResumenSaldos(ldt_Fecha, lstr_TipoDeuda);
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region Guardar en base de datos

        public void ActualizarTitulosValoresValores(string lstr_FechaInicio, string lstr_FechaFin)
        {
            DateTime ldt_FchInicio = DateTime.ParseExact(lstr_FechaInicio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime ldt_FchFin = DateTime.ParseExact(lstr_FechaFin, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime ldt_FchRecorrido = ldt_FchInicio;

            string[] lstr_TipoDeuda = new string[3];
            lstr_TipoDeuda[0] = "Rdi";
            lstr_TipoDeuda[1] = "Rde";
            lstr_TipoDeuda[2] = "Rdd";

            try
            {
                foreach (string tipo in lstr_TipoDeuda)
                {
                    while (ldt_FchRecorrido <= ldt_FchFin)
                    {
                        foreach (wsBccrRds1.Valor valor in ValoresPorFchValor(ldt_FchRecorrido, tipo))
                        {
                            try
                            {
                                string Serie = string.Empty;

                                if (!string.IsNullOrEmpty((valor.NumeroEmisionSerie + valor.NumeroSerie).Trim()))
                                    Serie = (valor.NumeroEmisionSerie + valor.NumeroSerie).Trim();
                                else
                                    Serie = (valor.NumeroEmisionSerie + valor.NumeroSerie).Trim();

                                Mantenimiento.clsDinamico dinamico = new Mantenimiento.clsDinamico();
                                dinamico.ConsultarDinamico("UPDATE cf.titulosValores  " +
                                                           "SET NroEmisionSerie = '" + Serie + "' " +
                                                           "WHERE IndicadorCupon = 'V' And Nemotecnico = '" + valor.Nemotecnico.Trim() + "' " +
                                                           "And NroValor = " + valor.NumValor.ToString());
                            }
                            catch (Exception ex)
                            {
                                string direccion = System.Configuration.ConfigurationManager.AppSettings["DireccionConfigs"];
                                direccion += "log.txt";
                                if (!System.IO.File.Exists(direccion))
                                    System.IO.File.Create(direccion).Dispose();

                                System.IO.File.AppendAllText(direccion, string.Format("{0}{1}", ex.ToString() + " / " + DateTime.Now.ToString(), Environment.NewLine));
                            }
                        }
                        ldt_FchRecorrido = ldt_FchRecorrido.AddDays(1);
                    }
                    ldt_FchRecorrido = ldt_FchInicio;
                }

            }
            catch (Exception ex)
            {
                string direccion = System.Configuration.ConfigurationManager.AppSettings["DireccionConfigs"];
                direccion += "log.txt";
                if (!System.IO.File.Exists(direccion))
                    System.IO.File.Create(direccion).Dispose();

                System.IO.File.AppendAllText(direccion, string.Format("{0}{1}", ex.ToString() + " / " + DateTime.Now.ToString(), Environment.NewLine));
            }



        }

        public void CargarValoresYCupones(string lstr_FechaInicio, string lstr_FechaFin, int lint_TipoFecha)
        {
            string resultado = String.Empty;
            string lstr_resultado = String.Empty;
            string lstr_codigo = String.Empty;
            clsTituloValor lcls_TituloValor = new clsTituloValor();

            DateTime ldt_FchInicio = DateTime.ParseExact(lstr_FechaInicio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime ldt_FchFin = DateTime.ParseExact(lstr_FechaFin, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime ldt_FchRecorrido = ldt_FchInicio;

            string[] lstr_TipoDeuda = new string[3];
            lstr_TipoDeuda[0] = "Rdi";
            lstr_TipoDeuda[1] = "Rde";
            lstr_TipoDeuda[2] = "Rdd";

            try
            {
                foreach (string tipo in lstr_TipoDeuda)
                {
                    ldt_FchRecorrido = ldt_FchInicio;

                    while (ldt_FchRecorrido <= ldt_FchFin)
                    {
                        switch (lint_TipoFecha)
                        {
                            case 1:
                                {

                                    #region Cupones fchCancelacion

                                    foreach (wsBccrRds1.Cupon cupon in CuponesPorFchCancelacion(ldt_FchRecorrido, tipo))
                                    {
                                        string lstr_CodMoneda = null;
                                        switch (cupon.CodMoneda.ToString())
                                        {
                                            case "Colones": lstr_CodMoneda = "CRCN"; break;
                                            case "Dolares": lstr_CodMoneda = "USD"; break;
                                            case "Euros": lstr_CodMoneda = "EUR"; break;
                                            case "UnidadesDeDesarrollo": lstr_CodMoneda = "UDE"; break;
                                        }

                                        lcls_TituloValor.CrearTituloValor(
                                            cupon.NumValor,
                                            cupon.NumCupon,
                                            cupon.Estado.ToString(),
                                            cupon.Nemotecnico, null,
                                            cupon.TipoNegociacion.ToString(),
                                            lstr_CodMoneda, 0,
                                            Convert.ToDateTime("01/01/1900"), null,
                                            cupon.FechaCancelacion,
                                            cupon.FechaVencimiento,
                                            cupon.FechaConstitucion, 0, 0,
                                            cupon.TasaBruta,
                                            cupon.TasaNeta, 0, null,
                                            Convert.ToDateTime("01/01/1900"),
                                            cupon.FechaInicio, null, null, null, null, 0,
                                            cupon.InteresBruto,
                                            cupon.InteresBrutoEfectivo,
                                            cupon.InteresNeto,
                                            cupon.InteresNetoAcumulado,
                                            cupon.ImpuestoVencido,
                                            cupon.ImpuestoEfectivo, 0, 0,
                                            tipo, null, "C",
                                            tipo, "ACT", "SG",
                                            cupon.DescripcionNegociacion,string.Empty,string.Empty,
                                            out lstr_resultado, out lstr_codigo);
                                    }

                                    #endregion

                                    #region valores FchCancelacion

                                    foreach (wsBccrRds1.Valor valor in ValoresPorFchCancelacion(ldt_FchRecorrido, tipo))
                                    {
                                        string lstr_CodMoneda = null;
                                        switch (valor.CodMoneda.ToString())
                                        {
                                            case "Colones": lstr_CodMoneda = "CRCN"; break;
                                            case "Dolares": lstr_CodMoneda = "USD"; break;
                                            case "Euros": lstr_CodMoneda = "EUR"; break;
                                            case "UnidadesDeDesarrollo": lstr_CodMoneda = "UDE"; break;
                                        }

                                        lcls_TituloValor.CrearTituloValor(
                                            valor.NumValor, 0,
                                            valor.Estado.ToString(),
                                            valor.Nemotecnico,
                                            valor.TipoValor.ToString(),
                                            valor.TipoNegociacion.ToString(),
                                            lstr_CodMoneda,
                                            valor.ValorFacial,
                                            valor.FechaValor,
                                            valor.PlazoValor.ToString(),
                                            valor.FechaCancelacion,
                                            valor.FecVencimiento,
                                            valor.FechaConstitucion,
                                            valor.ValorTransadoBruto,
                                            valor.ValorTransadoNeto,
                                            valor.TasaBruta,
                                            valor.TasaNeta,
                                            valor.Margen,
                                            valor.NumeroEmisionSerie + valor.NumeroSerie,
                                            valor.FechaValor,
                                            valor.FechaValor,
                                            valor.Propietario, null,
                                            valor.SistemaNegociacion.ToString(),
                                            valor.MotivoAnulacion,
                                            valor.RendimientoPorDescuento, 0, 0, 0,
                                            valor.InteresNetoAcumulado, 0, 0,
                                            valor.ImpuestoPagado,
                                            valor.Premio,
                                            tipo, null, "V",
                                            tipo, "ACT", "SG",
                                            valor.DescripcionNegociacion, valor.NumeroIdentificacion, valor.TipoIdentificacion.ToString(),
                                            out lstr_resultado, out lstr_codigo);
                                    }

                                    #endregion

                                    break;
                                }
                            case 2:
                                {

                                    #region Cupones fchConstitucion

                                    foreach (wsBccrRds1.Cupon cupon in CuponesPorFchConstitucion(ldt_FchRecorrido, tipo))
                                    {
                                        string lstr_CodMoneda = null;
                                        switch (cupon.CodMoneda.ToString())
                                        {
                                            case "Colones": lstr_CodMoneda = "CRCN"; break;
                                            case "Dolares": lstr_CodMoneda = "USD"; break;
                                            case "Euros": lstr_CodMoneda = "EUR"; break;
                                            case "UnidadesDeDesarrollo": lstr_CodMoneda = "UDE"; break;
                                        }

                                        lcls_TituloValor.CrearTituloValor(
                                            cupon.NumValor,
                                            cupon.NumCupon,
                                            cupon.Estado.ToString(),
                                            cupon.Nemotecnico, null,
                                            cupon.TipoNegociacion.ToString(),
                                            lstr_CodMoneda, 0,
                                            Convert.ToDateTime("01/01/1900"), null,
                                            cupon.FechaCancelacion,
                                            cupon.FechaVencimiento,
                                            cupon.FechaConstitucion, 0, 0,
                                            cupon.TasaBruta,
                                            cupon.TasaNeta, 0, null,
                                            Convert.ToDateTime("01/01/1900"),
                                            cupon.FechaInicio, null, null, null, null, 0,
                                            cupon.InteresBruto,
                                            cupon.InteresBrutoEfectivo,
                                            cupon.InteresNeto,
                                            cupon.InteresNetoAcumulado,
                                            cupon.ImpuestoVencido,
                                            cupon.ImpuestoEfectivo, 0, 0,
                                            tipo, null, "C",
                                            tipo, "ACT", "SG",
                                            cupon.DescripcionNegociacion, string.Empty, string.Empty,
                                            out lstr_resultado, out lstr_codigo);
                                    }

                                    #endregion

                                    #region valores FchConstitucion

                                    foreach (wsBccrRds1.Valor valor in ValoresPorFchConstitucion(ldt_FchRecorrido, tipo))
                                    {
                                        string lstr_CodMoneda = null;
                                        switch (valor.CodMoneda.ToString())
                                        {
                                            case "Colones": lstr_CodMoneda = "CRCN"; break;
                                            case "Dolares": lstr_CodMoneda = "USD"; break;
                                            case "Euros": lstr_CodMoneda = "EUR"; break;
                                            case "UnidadesDeDesarrollo": lstr_CodMoneda = "UDE"; break;
                                        }

                                        lcls_TituloValor.CrearTituloValor(
                                            valor.NumValor, 0,
                                            valor.Estado.ToString(),
                                            valor.Nemotecnico,
                                            valor.TipoValor.ToString(),
                                            valor.TipoNegociacion.ToString(),
                                            lstr_CodMoneda,
                                            valor.ValorFacial,
                                            valor.FechaValor,
                                            valor.PlazoValor.ToString(),
                                            valor.FechaCancelacion,
                                            valor.FecVencimiento,
                                            valor.FechaConstitucion,
                                            valor.ValorTransadoBruto,
                                            valor.ValorTransadoNeto,
                                            valor.TasaBruta,
                                            valor.TasaNeta,
                                            valor.Margen,
                                            valor.NumeroEmisionSerie + valor.NumeroSerie,
                                            valor.FechaValor,
                                            valor.FechaValor,
                                            valor.Propietario, null,
                                            valor.SistemaNegociacion.ToString(),
                                            valor.MotivoAnulacion,
                                            valor.RendimientoPorDescuento, 0, 0, 0,
                                            valor.InteresNetoAcumulado, 0, 0,
                                            valor.ImpuestoPagado,
                                            valor.Premio,
                                            tipo, null, "V",
                                            tipo, "ACT", "SG",
                                            valor.DescripcionNegociacion, valor.NumeroIdentificacion,
                                            valor.TipoIdentificacion.ToString(),
                                            out lstr_resultado, out lstr_codigo);
                                    }

                                    #endregion

                                    break;
                                }
                            case 3:
                                {

                                    #region Cupones fchValor
                                    foreach (wsBccrRds1.Cupon cupon in CuponesPorFchValor(ldt_FchRecorrido, tipo))
                                    {
                                        try
                                        {
                                            string lstr_CodMoneda = null;
                                            switch (cupon.CodMoneda.ToString())
                                            {
                                                case "Colones": lstr_CodMoneda = "CRCN"; break;
                                                case "Dolares": lstr_CodMoneda = "USD"; break;
                                                case "Euros": lstr_CodMoneda = "EUR"; break;
                                                case "UnidadesDeDesarrollo": lstr_CodMoneda = "UDE"; break;
                                            }

                                            lcls_TituloValor.CrearTituloValor(
                                                cupon.NumValor,
                                                cupon.NumCupon,
                                                cupon.Estado.ToString(),
                                                cupon.Nemotecnico, null,
                                                cupon.TipoNegociacion.ToString(),
                                                lstr_CodMoneda, 0,
                                                Convert.ToDateTime("01/01/1900"), null,
                                                cupon.FechaCancelacion,
                                                cupon.FechaVencimiento,
                                                cupon.FechaConstitucion, 0, 0,
                                                cupon.TasaBruta,
                                                cupon.TasaNeta, 0, null,
                                                Convert.ToDateTime("01/01/1900"),
                                                cupon.FechaInicio, null, null, null, null, 0,
                                                cupon.InteresBruto,
                                                cupon.InteresBrutoEfectivo,
                                                cupon.InteresNeto,
                                                cupon.InteresNetoAcumulado,
                                                cupon.ImpuestoVencido,
                                                cupon.ImpuestoEfectivo, 0, 0,
                                                tipo, null, "C",
                                                tipo, "ACT", "SG",
                                                cupon.DescripcionNegociacion, string.Empty, string.Empty,
                                                out lstr_resultado, out lstr_codigo);

                                        }
                                        catch (Exception ex)
                                        {
                                            string direccion = System.Configuration.ConfigurationManager.AppSettings["DireccionConfigs"];
                                            direccion += "log.txt";
                                            if (!System.IO.File.Exists(direccion))
                                                System.IO.File.Create(direccion).Dispose();

                                            System.IO.File.AppendAllText(direccion, string.Format("{0}{1}", ex.ToString() + " / " + DateTime.Now.ToString(), Environment.NewLine));
                                        }
                                    }

                                    #endregion

                                    #region valores FchValor

                                    foreach (wsBccrRds1.Valor valor in ValoresPorFchValor(ldt_FchRecorrido, tipo))
                                    {
                                        try
                                        {
                                            string lstr_CodMoneda = null;
                                            switch (valor.CodMoneda.ToString())
                                            {
                                                case "Colones": lstr_CodMoneda = "CRCN"; break;
                                                case "Dolares": lstr_CodMoneda = "USD"; break;
                                                case "Euros": lstr_CodMoneda = "EUR"; break;
                                                case "UnidadesDeDesarrollo": lstr_CodMoneda = "UDE"; break;
                                            }

                                            lcls_TituloValor.CrearTituloValor(
                                                valor.NumValor, 0,
                                                valor.Estado.ToString(),
                                                valor.Nemotecnico,
                                                valor.TipoValor.ToString(),
                                                valor.TipoNegociacion.ToString(),
                                                lstr_CodMoneda,
                                                valor.ValorFacial,
                                                valor.FechaValor,
                                                valor.PlazoValor.ToString(),
                                                valor.FechaCancelacion,
                                                valor.FecVencimiento,
                                                valor.FechaConstitucion,
                                                valor.ValorTransadoBruto,
                                                valor.ValorTransadoNeto,
                                                valor.TasaBruta,
                                                valor.TasaNeta,
                                                valor.Margen,
                                                valor.NumeroEmisionSerie + valor.NumeroSerie,
                                                valor.FechaValor,
                                                valor.FechaValor,
                                                valor.Propietario, null,
                                                valor.SistemaNegociacion.ToString(),
                                                valor.MotivoAnulacion,
                                                valor.RendimientoPorDescuento, 0, 0, 0,
                                                valor.InteresNetoAcumulado, 0, 0,
                                                valor.ImpuestoPagado,
                                                valor.Premio,
                                                tipo, null, "V",
                                                tipo, "ACT", "SG",
                                                valor.DescripcionNegociacion, valor.NumeroIdentificacion, valor.TipoIdentificacion.ToString(),
                                                out lstr_resultado, out lstr_codigo);
                                        }
                                        catch (Exception ex)
                                        {
                                            string direccion = System.Configuration.ConfigurationManager.AppSettings["DireccionConfigs"];
                                            direccion += "log.txt";
                                            if (!System.IO.File.Exists(direccion))
                                                System.IO.File.Create(direccion).Dispose();

                                            System.IO.File.AppendAllText(direccion, string.Format("{0}{1}", ex.ToString() + " / " + DateTime.Now.ToString(), Environment.NewLine));
                                        }
                                    }
                                    #endregion

                                    break;
                                }
                        }
                        ldt_FchRecorrido = ldt_FchRecorrido.AddDays(1);
                    }
                    ldt_FchRecorrido = ldt_FchInicio;
                }
                /*
                dinamico.ConsultarDinamico(" UPDATE A "+
                " SET DescripcionNegociacion = 'Canje/Inversa/Precio'," +
	            " TipoNegociacion='Compra'"+
                " FROM cf.titulosvalores A"+
                " INNER JOIN cf.titulosvalores B"+
                " on A.NroValor  = B.NroValor"+ 
                " and A.Nemotecnico = B.Nemotecnico"+
                " WHERE B.MotivoAnulacion = 'Canjeado'"+
                " AND A.ModuloSINPE = 'Rdi'");*/
            }
            catch (Exception ex)
            {
                string direccion = System.Configuration.ConfigurationManager.AppSettings["DireccionConfigs"];
                direccion += "log.txt";
                if (!System.IO.File.Exists(direccion))
                    System.IO.File.Create(direccion).Dispose();

                System.IO.File.AppendAllText(direccion, string.Format("{0}{1}", ex.ToString() + " / " + DateTime.Now.ToString(), Environment.NewLine));
            }
        }

        public void CargarValoresYCuponesRDE(string lstr_FechaInicio, string lstr_FechaFin, int lint_TipoFecha)
        {
            string resultado = String.Empty;
            string lstr_resultado = String.Empty;
            string lstr_codigo = String.Empty;
            clsTituloValor lcls_TituloValor = new clsTituloValor();

            DateTime ldt_FchInicio = DateTime.ParseExact(lstr_FechaInicio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime ldt_FchFin = DateTime.ParseExact(lstr_FechaFin, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime ldt_FchRecorrido = ldt_FchInicio;

            string[] lstr_TipoDeuda = new string[1];
            lstr_TipoDeuda[0] = "Rdd";


            try
            {
                foreach (string tipo in lstr_TipoDeuda)
                {
                    while (ldt_FchRecorrido <= ldt_FchFin)
                    {
                        switch (lint_TipoFecha)
                        {
                            case 1:
                                {

                                    #region Cupones fchCancelacion

                                    foreach (wsBccrRds1.Cupon cupon in CuponesPorFchCancelacion(ldt_FchRecorrido, tipo))
                                    {
                                        string lstr_CodMoneda = null;
                                        switch (cupon.CodMoneda.ToString())
                                        {
                                            case "Colones": lstr_CodMoneda = "CRCN"; break;
                                            case "Dolares": lstr_CodMoneda = "USD"; break;
                                            case "Euros": lstr_CodMoneda = "EUR"; break;
                                            case "UnidadesDeDesarrollo": lstr_CodMoneda = "UDE"; break;
                                        }

                                        lcls_TituloValor.CrearTituloValor(
                                            cupon.NumValor,
                                            cupon.NumCupon,
                                            cupon.Estado.ToString(),
                                            cupon.Nemotecnico, null,
                                            cupon.TipoNegociacion.ToString(),
                                            lstr_CodMoneda, 0,
                                            Convert.ToDateTime("01/01/1900"), null,
                                            cupon.FechaCancelacion,
                                            cupon.FechaVencimiento,
                                            cupon.FechaConstitucion, 0, 0,
                                            cupon.TasaBruta,
                                            cupon.TasaNeta, 0, null,
                                            Convert.ToDateTime("01/01/1900"),
                                            cupon.FechaInicio, null, null, null, null, 0,
                                            cupon.InteresBruto,
                                            cupon.InteresBrutoEfectivo,
                                            cupon.InteresNeto,
                                            cupon.InteresNetoAcumulado,
                                            cupon.ImpuestoVencido,
                                            cupon.ImpuestoEfectivo, 0, 0,
                                            tipo, null, "C",
                                            tipo, "ACT", "SG",
                                            cupon.DescripcionNegociacion, string.Empty, string.Empty,
                                            out lstr_resultado, out lstr_codigo);
                                    }

                                    #endregion

                                    #region valores FchCancelacion

                                    foreach (wsBccrRds1.Valor valor in ValoresPorFchCancelacion(ldt_FchRecorrido, tipo))
                                    {
                                        string lstr_CodMoneda = null;
                                        switch (valor.CodMoneda.ToString())
                                        {
                                            case "Colones": lstr_CodMoneda = "CRCN"; break;
                                            case "Dolares": lstr_CodMoneda = "USD"; break;
                                            case "Euros": lstr_CodMoneda = "EUR"; break;
                                            case "UnidadesDeDesarrollo": lstr_CodMoneda = "UDE"; break;
                                        }

                                        lcls_TituloValor.CrearTituloValor(
                                            valor.NumValor, 0,
                                            valor.Estado.ToString(),
                                            valor.Nemotecnico,
                                            valor.TipoValor.ToString(),
                                            valor.TipoNegociacion.ToString(),
                                            lstr_CodMoneda,
                                            valor.ValorFacial,
                                            valor.FechaValor,
                                            valor.PlazoValor.ToString(),
                                            valor.FechaCancelacion,
                                            valor.FecVencimiento,
                                            valor.FechaConstitucion,
                                            valor.ValorTransadoBruto,
                                            valor.ValorTransadoNeto,
                                            valor.TasaBruta,
                                            valor.TasaNeta,
                                            valor.Margen,
                                            valor.NumeroEmisionSerie + valor.NumeroSerie,
                                            valor.FechaValor,
                                            valor.FechaValor,
                                            valor.Propietario, null,
                                            valor.SistemaNegociacion.ToString(),
                                            valor.MotivoAnulacion,
                                            valor.RendimientoPorDescuento, 0, 0, 0,
                                            valor.InteresNetoAcumulado, 0, 0,
                                            valor.ImpuestoPagado,
                                            valor.Premio,
                                            tipo, null, "V",
                                            tipo, "ACT", "SG",
                                            valor.DescripcionNegociacion, valor.NumeroIdentificacion, valor.TipoIdentificacion.ToString(),
                                            out lstr_resultado, out lstr_codigo);
                                    }

                                    #endregion

                                    break;
                                }
                            case 2:
                                {

                                    #region Cupones fchConstitucion

                                    foreach (wsBccrRds1.Cupon cupon in CuponesPorFchConstitucion(ldt_FchRecorrido, tipo))
                                    {
                                        string lstr_CodMoneda = null;
                                        switch (cupon.CodMoneda.ToString())
                                        {
                                            case "Colones": lstr_CodMoneda = "CRCN"; break;
                                            case "Dolares": lstr_CodMoneda = "USD"; break;
                                            case "Euros": lstr_CodMoneda = "EUR"; break;
                                            case "UnidadesDeDesarrollo": lstr_CodMoneda = "UDE"; break;
                                        }

                                        lcls_TituloValor.CrearTituloValor(
                                            cupon.NumValor,
                                            cupon.NumCupon,
                                            cupon.Estado.ToString(),
                                            cupon.Nemotecnico, null,
                                            cupon.TipoNegociacion.ToString(),
                                            lstr_CodMoneda, 0,
                                            Convert.ToDateTime("01/01/1900"), null,
                                            cupon.FechaCancelacion,
                                            cupon.FechaVencimiento,
                                            cupon.FechaConstitucion, 0, 0,
                                            cupon.TasaBruta,
                                            cupon.TasaNeta, 0, null,
                                            Convert.ToDateTime("01/01/1900"),
                                            cupon.FechaInicio, null, null, null, null, 0,
                                            cupon.InteresBruto,
                                            cupon.InteresBrutoEfectivo,
                                            cupon.InteresNeto,
                                            cupon.InteresNetoAcumulado,
                                            cupon.ImpuestoVencido,
                                            cupon.ImpuestoEfectivo, 0, 0,
                                            tipo, null, "C",
                                            tipo, "ACT", "SG",
                                            cupon.DescripcionNegociacion, string.Empty, string.Empty,
                                            out lstr_resultado, out lstr_codigo);
                                    }

                                    #endregion

                                    #region valores FchConstitucion

                                    foreach (wsBccrRds1.Valor valor in ValoresPorFchConstitucion(ldt_FchRecorrido, tipo))
                                    {
                                        string lstr_CodMoneda = null;
                                        switch (valor.CodMoneda.ToString())
                                        {
                                            case "Colones": lstr_CodMoneda = "CRCN"; break;
                                            case "Dolares": lstr_CodMoneda = "USD"; break;
                                            case "Euros": lstr_CodMoneda = "EUR"; break;
                                            case "UnidadesDeDesarrollo": lstr_CodMoneda = "UDE"; break;
                                        }

                                        lcls_TituloValor.CrearTituloValor(
                                            valor.NumValor, 0,
                                            valor.Estado.ToString(),
                                            valor.Nemotecnico,
                                            valor.TipoValor.ToString(),
                                            valor.TipoNegociacion.ToString(),
                                            lstr_CodMoneda,
                                            valor.ValorFacial,
                                            valor.FechaValor,
                                            valor.PlazoValor.ToString(),
                                            valor.FechaCancelacion,
                                            valor.FecVencimiento,
                                            valor.FechaConstitucion,
                                            valor.ValorTransadoBruto,
                                            valor.ValorTransadoNeto,
                                            valor.TasaBruta,
                                            valor.TasaNeta,
                                            valor.Margen,
                                            valor.NumeroEmisionSerie + valor.NumeroSerie,
                                            valor.FechaValor,
                                            valor.FechaValor,
                                            valor.Propietario, null,
                                            valor.SistemaNegociacion.ToString(),
                                            valor.MotivoAnulacion,
                                            valor.RendimientoPorDescuento, 0, 0, 0,
                                            valor.InteresNetoAcumulado, 0, 0,
                                            valor.ImpuestoPagado,
                                            valor.Premio,
                                            tipo, null, "V",
                                            tipo, "ACT", "SG",
                                            valor.DescripcionNegociacion,valor.NumeroIdentificacion , valor.TipoIdentificacion.ToString(),
                                            out lstr_resultado, out lstr_codigo);
                                    }

                                    #endregion

                                    break;
                                }
                            case 3:
                                {

                                    #region Cupones fchValor
                                    foreach (wsBccrRds1.Cupon cupon in CuponesPorFchValor(ldt_FchRecorrido, tipo))
                                    {
                                        try
                                        {
                                            string lstr_CodMoneda = null;
                                            switch (cupon.CodMoneda.ToString())
                                            {
                                                case "Colones": lstr_CodMoneda = "CRCN"; break;
                                                case "Dolares": lstr_CodMoneda = "USD"; break;
                                                case "Euros": lstr_CodMoneda = "EUR"; break;
                                                case "UnidadesDeDesarrollo": lstr_CodMoneda = "UDE"; break;
                                            }

                                            lcls_TituloValor.CrearTituloValor(
                                                cupon.NumValor,
                                                cupon.NumCupon,
                                                cupon.Estado.ToString(),
                                                cupon.Nemotecnico, null,
                                                cupon.TipoNegociacion.ToString(),
                                                lstr_CodMoneda, 0,
                                                Convert.ToDateTime("01/01/1900"), null,
                                                cupon.FechaCancelacion,
                                                cupon.FechaVencimiento,
                                                cupon.FechaConstitucion, 0, 0,
                                                cupon.TasaBruta,
                                                cupon.TasaNeta, 0, null,
                                                Convert.ToDateTime("01/01/1900"),
                                                cupon.FechaInicio, null, null, null, null, 0,
                                                cupon.InteresBruto,
                                                cupon.InteresBrutoEfectivo,
                                                cupon.InteresNeto,
                                                cupon.InteresNetoAcumulado,
                                                cupon.ImpuestoVencido,
                                                cupon.ImpuestoEfectivo, 0, 0,
                                                tipo, null, "C",
                                                tipo, "ACT", "SG",
                                                cupon.DescripcionNegociacion, string.Empty, string.Empty,
                                                out lstr_resultado, out lstr_codigo);

                                        }
                                        catch (Exception ex)
                                        {
                                            string direccion = System.Configuration.ConfigurationManager.AppSettings["DireccionConfigs"];
                                            direccion += "log.txt";
                                            if (!System.IO.File.Exists(direccion))
                                                System.IO.File.Create(direccion).Dispose();

                                            System.IO.File.AppendAllText(direccion, string.Format("{0}{1}", ex.ToString() + " / " + DateTime.Now.ToString(), Environment.NewLine));
                                        }
                                    }

                                    #endregion

                                    #region valores FchValor

                                    foreach (wsBccrRds1.Valor valor in ValoresPorFchValor(ldt_FchRecorrido, tipo))
                                    {
                                        try
                                        {
                                            string lstr_CodMoneda = null;
                                            switch (valor.CodMoneda.ToString())
                                            {
                                                case "Colones": lstr_CodMoneda = "CRCN"; break;
                                                case "Dolares": lstr_CodMoneda = "USD"; break;
                                                case "Euros": lstr_CodMoneda = "EUR"; break;
                                                case "UnidadesDeDesarrollo": lstr_CodMoneda = "UDE"; break;
                                            }

                                            lcls_TituloValor.CrearTituloValor(
                                                valor.NumValor, 0,
                                                valor.Estado.ToString(),
                                                valor.Nemotecnico,
                                                valor.TipoValor.ToString(),
                                                valor.TipoNegociacion.ToString(),
                                                lstr_CodMoneda,
                                                valor.ValorFacial,
                                                valor.FechaValor,
                                                valor.PlazoValor.ToString(),
                                                valor.FechaCancelacion,
                                                valor.FecVencimiento,
                                                valor.FechaConstitucion,
                                                valor.ValorTransadoBruto,
                                                valor.ValorTransadoNeto,
                                                valor.TasaBruta,
                                                valor.TasaNeta,
                                                valor.Margen,
                                                valor.NumeroEmisionSerie + valor.NumeroSerie,
                                                valor.FechaValor,
                                                valor.FechaValor,
                                                valor.Propietario, null,
                                                valor.SistemaNegociacion.ToString(),
                                                valor.MotivoAnulacion,
                                                valor.RendimientoPorDescuento, 0, 0, 0,
                                                valor.InteresNetoAcumulado, 0, 0,
                                                valor.ImpuestoPagado,
                                                valor.Premio,
                                                tipo, null, "V",
                                                tipo, "ACT", "SG",
                                                valor.DescripcionNegociacion, valor.NumeroIdentificacion, valor.TipoIdentificacion.ToString(),
                                                out lstr_resultado, out lstr_codigo);
                                        }
                                        catch (Exception ex)
                                        {
                                            string direccion = System.Configuration.ConfigurationManager.AppSettings["DireccionConfigs"];
                                            direccion += "log.txt";
                                            if (!System.IO.File.Exists(direccion))
                                                System.IO.File.Create(direccion).Dispose();

                                            System.IO.File.AppendAllText(direccion, string.Format("{0}{1}", ex.ToString() + " / " + DateTime.Now.ToString(), Environment.NewLine));
                                        }
                                    }
                                    #endregion

                                    break;
                                }
                        }
                        ldt_FchRecorrido = ldt_FchRecorrido.AddDays(1);
                    }
                    ldt_FchRecorrido = ldt_FchInicio;
                }
            }
            catch (Exception ex)
            {
                string direccion = System.Configuration.ConfigurationManager.AppSettings["DireccionConfigs"];
                direccion += "log.txt";
                if (!System.IO.File.Exists(direccion))
                    System.IO.File.Create(direccion).Dispose();

                System.IO.File.AppendAllText(direccion, string.Format("{0}{1}", ex.ToString() + " / " + DateTime.Now.ToString(), Environment.NewLine));
            }
        }

        #endregion

        public int Dias360(DateTime? date, DateTime? initialDate)
        {
            return Dias360(date.Value, initialDate.Value);
        }

        public int Dias360(DateTime date, DateTime initialDate)
        {
            var dateA = initialDate;

            var dateB = date;

            var dayA = dateA.Day;

            var dayB = dateB.Day;

            if (UltimoDiaFebrero(dateA) && UltimoDiaFebrero(dateB))
                dayB = 30;

            if (dayA == 31 && UltimoDiaFebrero(dateA))
                dayA = 30;

            if (dayA == 30 && dayB == 31)
                dayB = 30;

            if (dayA == 31)
                dayA = 30;

            int days = (dateB.Year - dateA.Year) * 360 +
                ((dateB.Month + 1) - (dateA.Month + 1)) * 30 + (dayB - dayA);

            return days;

        }

        private static bool UltimoDiaFebrero(DateTime date)
        {

            int lastDay = DateTime.DaysInMonth(date.Year, 2);

            return date.Day == lastDay;
        }

        public decimal DiferenciaSemestres(DateTime FechaFin, DateTime FechaInicio)
        {
            decimal ldec_temp1 = 0;
            decimal ldec_semestre = 0;
            ldec_temp1 = Math.Abs((FechaFin.Month - FechaInicio.Month) + 12 * (FechaFin.Year - FechaInicio.Year));

            if ((ldec_temp1 / 6) < 0)
            {
                ldec_semestre = 1;
            }
            else
            {
                ldec_semestre = ldec_temp1 / 6;
            }
            return Math.Round(ldec_semestre);
        }

        public decimal DiferenciaMeses(DateTime FechaFin, DateTime FechaInicio)
        {
            return Math.Abs((FechaFin.Month - FechaInicio.Month) + 12 * (FechaFin.Year - FechaInicio.Year));
        }

        public int UltimoDiaMes(DateTime fecha)
        {
            int ultimoDia = 0;
            ultimoDia = DateTime.DaysInMonth(fecha.Year, fecha.Month);
            return ultimoDia;
        }

        public string[] PeriodosTitulo(DateTime ldec_FchValor, DateTime ldec_FchVencimiento, List<PeriodosCupones> _lstPeriodos)
        {
            try
            {
                meses = 0;
                Periodicidad = 0;
                fechas = new string[_lstPeriodos.Count() + 1];
                foreach (PeriodosCupones periodo in _lstPeriodos)
                {
                    fechas[meses] = periodo.FchInicio.ToString("dd/MM/yyyy");
                    if (meses == 1)
                        Periodicidad = (periodo.Periodicidad < 0? periodo.Periodicidad * -1: periodo.Periodicidad);
                    meses++;
                }

                fechas[meses] = ldec_FchVencimiento.ToString("dd/MM/yyyy");
                gdt_FchUltimoPago = ldec_FchValor.AddMonths(-Periodicidad);
                Periodicidad = Periodicidad == 0 ? 1 : Periodicidad;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return fechas;
        }


        public string[] PeriodosSemestrales(DateTime ldec_FchValor, DateTime ldec_FchVencimiento)
        {
            DateTime temp = new DateTime();
            DateTime inicio = new DateTime();
            DateTime fin = new DateTime();
            meses = 0;
            ArrayList arreglo = new ArrayList();

            try
            {
                inicio = ldec_FchValor;
                fin = ldec_FchVencimiento;
                temp = fin;
                //meses = DiferenciaSemestres(ldec_FchVencimiento, ldec_FchValor);

                arreglo.Add(temp.ToString("dd/MM/yyyy"));
                meses++;

                while (temp > inicio)
                {
                    temp = temp.AddMonths(-6);
                    arreglo.Add(temp.ToString("dd/MM/yyyy"));
                    meses++;
                }
                gdt_FchUltimoPago = temp;
                arreglo.Remove(temp.ToString("dd/MM/yyyy"));
                arreglo.Add(inicio.ToString("dd/MM/yyyy"));
                //PruebaFechas
                fechas = new string[meses];

                int cont = 0;
                for (int i = arreglo.Count; i > 0; i--)
                {
                    fechas[cont] = arreglo[i - 1].ToString();
                    cont++;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return fechas;
        }

        public string[] PeriodosMensuales(DateTime ldec_FchValor, DateTime ldec_FchVencimiento)
        {
            DateTime temp = new DateTime();
            DateTime inicio = new DateTime();
            DateTime fin = new DateTime();
            meses = 0;
            ArrayList arreglo = new ArrayList();

            try
            {
                inicio = ldec_FchValor;
                fin = ldec_FchVencimiento;
                temp = inicio;
                meses = Convert.ToInt32(DiferenciaMeses(ldec_FchVencimiento, ldec_FchValor));

                arreglo.Add(inicio.ToString("dd/MM/yyyy"));

                if (inicio.Day != UltimoDiaMes(inicio))
                {
                    meses++;
                    temp = DateTime.ParseExact("" + UltimoDiaMes(inicio) + "/" + inicio.Month.ToString().PadLeft(2, '0') + "/" + inicio.Year, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    arreglo.Add(temp.ToString("dd/MM/yyyy"));
                }

                for (int i = 1; i < meses; i++)
                {
                    temp = temp.AddMonths(1);
                    if (temp.Month == fin.Month)
                    {
                        break;
                    }
                    else
                    {
                        arreglo.Add(temp.ToString("dd/MM/yyyy"));
                        //arreglo.Add(temp.ToString("dd/MM/yyyy"));
                    }
                }

                if (temp != fin)
                {
                    meses++;
                    arreglo.Add(fin.ToString("dd/MM/yyyy"));
                }
                else
                {
                    meses++;
                }

                fechas = new string[meses];

                for (int i = 0; i < arreglo.Count; i++)
                {
                    fechas[i] = arreglo[i].ToString();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return fechas;
        }

        public string[] PeriodosMensualesDevengo(DateTime ldec_FchValor, DateTime ldec_FchVencimiento)
        {
            DateTime temp = new DateTime();
            DateTime inicio = new DateTime();
            DateTime fin = new DateTime();
            mesesDev = 0;
            ArrayList arreglo = new ArrayList();

            try
            {
                inicio = ldec_FchValor;
                fin = ldec_FchVencimiento;
                temp = inicio;
                mesesDev = Convert.ToInt32(DiferenciaMeses(ldec_FchVencimiento, ldec_FchValor));

                //arreglo.Add(inicio.ToString("dd/MM/yyyy"));

               // if (inicio.Day != UltimoDiaMes(inicio))
                //{
                    //mesesDev++;
                    int dia = 0;
                    if (mesesDev > 0)
                    {
                        if (inicio.Month == 2)
                        {
                            if (UltimoDiaMes(inicio) == 28)
                            {
                                dia = UltimoDiaMes(inicio);
                            }
                            else
                            {
                                dia = UltimoDiaMes(inicio) - 1;
                            }
                        }
                        else
                        {
                            if (UltimoDiaMes(inicio) == 30)
                            {
                                dia = UltimoDiaMes(inicio);
                            }
                            else
                            {
                                dia = UltimoDiaMes(inicio) - 1;
                            }
                        }
                        temp = DateTime.ParseExact("" + dia + "/" + inicio.Month.ToString().PadLeft(2, '0') + "/" + inicio.Year, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        arreglo.Add(temp.ToString("dd/MM/yyyy"));
                    }
                    else
                    {
                        arreglo.Add(fin.ToString("dd/MM/yyyy"));
                    }
                //}

                for (int i = 1; i < mesesDev; i++)
                {
                    //temp = temp.AddMonths(1);
                    temp = temp.AddMonths(1);
                    dia = 0;
                    if (temp.Month == 2)
                    {
                        if (UltimoDiaMes(temp) == 28)
                        {
                            dia = UltimoDiaMes(temp);
                        }
                        else
                        {
                            dia = UltimoDiaMes(temp) - 1;
                        }
                    }
                    else
                    {
                        if (UltimoDiaMes(temp) == 30)
                        {
                            dia = UltimoDiaMes(temp);
                        }
                        else
                        {
                            dia = UltimoDiaMes(temp) - 1;
                        }
                    }
                    temp = DateTime.ParseExact("" + dia + "/" + temp.Month.ToString().PadLeft(2, '0') + "/" + temp.Year, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    if (temp.Month == fin.Month)
                    {
                        break;
                    }
                    else
                    {
                        arreglo.Add(temp.ToString("dd/MM/yyyy"));
                        //arreglo.Add(temp.ToString("dd/MM/yyyy"));
                    }
                }

                if (temp != fin)
                {
                    mesesDev++;
                    arreglo.Add(fin.ToString("dd/MM/yyyy"));
                }
                else
                {
                    mesesDev++;
                }

                fechas = new string[mesesDev];

                for (int i = 0; i < arreglo.Count; i++)
                {
                    fechas[i] = arreglo[i].ToString();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return fechas;
        }

        public void DevengoMensual(int lint_NroValor, string lstr_Nemotecnico, List<clsConsultaDevengoInteres> _LstDevengoInteres, DateTime _fechaValor, decimal _diferencia)
        {
            int TotalDiasDif = 0;
            int DiasDif = 0;
            decimal InteresTotal = 0;
            decimal CuponTotal = 0;
            decimal DiferenciaAsiento = 0;
            int contDevengo = 1;
            
            clsContabilizarDevengoInt ContabilizarDevengo = new clsContabilizarDevengoInt();

            string mens1 = String.Empty;
            string mens2 = String.Empty;

            try
            {
                clsDevengoMensual clsDevengoMensual = new clsDevengoMensual();
                foreach (var devengo in _LstDevengoInteres)
                {
                    
                    FchTemp = PeriodosMensualesDevengo(_fechaValor, devengo.Anno1);
                    TotalDiasDif = +(Dias360(devengo.Anno1, _fechaValor));

                     if (DiferenciaMeses(devengo.Anno1, _fechaValor) == 6 && TotalDiasDif > 180)
                        TotalDiasDif = 180;

                    for (int i = 0; i < mesesDev; i++)
                    {
                        try
                        {
                            if (i == 0)
                            {
                                DateTime FchTemporal = _fechaValor;
                                DiasDif = +(Dias360(Convert.ToDateTime(FchTemp[i]), FchTemporal));

                                if (FchTemporal.Month == 2)
                                    DiasDif += 2;

                                if (DiasDif > 30)
                                    DiasDif = 30;
                            }
                            else
                            {
                                DateTime FchTemporal1 = _fechaValor;
                                DateTime FchTemporal2 = Convert.ToDateTime(FchTemp[i]);
                                DiasDif = +(Dias360(FchTemporal2, FchTemporal1));

                                if (FchTemporal2.Month == 2 && DiasDif >= 28)
                                    DiasDif = 30;
                                else
                                {
                                    if (FchTemporal1.Day >= 28 && FchTemporal1.Month == 2)
                                    {
                                        if (DiasDif > 30)
                                            DiasDif = 30;

                                        if (FchTemporal2.Day >= 30)
                                            DiasDif = 30;
                                        else
                                            DiasDif = DiasDif - 2;    
                                    }
                                    else
                                    {
                                        if (DiasDif > 30)
                                            DiasDif = 30;
                                    }
                                }
                            }

                            InteresTotal = decimal.Round((+(devengo.Intereses1 / TotalDiasDif) * DiasDif),2);
                            CuponTotal = decimal.Round(((devengo.Pago1 / TotalDiasDif) * DiasDif), 2);
                            Descuento = decimal.Round((+(InteresTotal + CuponTotal)), 2);
                            DiferenciaAsiento = decimal.Round(((InteresTotal + CuponTotal) - Descuento),2);


                            if (DiferenciaAsiento >= 0)
                                Descuento = Descuento - DiferenciaAsiento;
                            else
                                Descuento = Descuento + -(DiferenciaAsiento);

                            if (contDevengo == _LstDevengoInteres.Count)
                            {
                                if (i == mesesDev - 1)
                                {
                                    if (_diferencia >= 0)
                                    {
                                        InteresTotal = InteresTotal - _diferencia;
                                        Descuento = Descuento - _diferencia;
                                    }
                                    else
                                    {
                                        InteresTotal = InteresTotal + -(_diferencia);
                                        Descuento = Descuento + -(_diferencia);
                                    }  
                                }
                            }

                            if (DiasDif > 0)
                                clsDevengoMensual.CrearDevengoMensual(lint_NroValor, lstr_Nemotecnico, FchTemp[i], devengo.IdFlujoEfectivoFK1, DiasDif, InteresTotal, CuponTotal,
                                                                       Descuento, "SG", out mens1, out mens2);

                            //ContabilizarDevengo.DevengoIntereses(lint_NroValor.ToString(), lstr_Nemotecnico, _fechaValor);
                                
                            _fechaValor = Convert.ToDateTime(FchTemp[i]);
                            DiferenciaAsiento = 0;
                            
                        }
                        catch
                        { i = mesesDev + 1; }
                    }
                    contDevengo++;
                }

                Mantenimiento.clsDinamico dinamico = new Mantenimiento.clsDinamico();
                dinamico.ConsultarDinamico("UPDATE [cf].[DevengosMensuales] " +
                                           "SET Descuento = Descuento + ((InteresTotal + Cupon) - Descuento) " +
                                           "WHERE(InteresTotal + Cupon) - Descuento <> 0");

               Decimal porcentaje = get_canje_percentaje(lstr_Nemotecnico, lint_NroValor.ToString());
                
                dinamico.ConsultarDinamico("DECLARE @Diferencia decimal(22,4) " +
                           "SET @Diferencia = " +
                           "                     (SELECT ( " +
                           "                               (SELECT SUM(DESCUENTO) FROM [cf].[DevengosMensuales] where nrovalor = " + lint_NroValor.ToString() + "  and Nemotecnico = '" + lstr_Nemotecnico + "') - " +
                           "                               (SELECT (( (valortransadoNeto * "+porcentaje+") - (valortransadoBruto * "+porcentaje+")) + ( (valortransadoBruto * "+porcentaje+" )- (valorfacial *"+porcentaje+") ) ) * -1 as Cuadre FROM cf.titulosvalores " +
                           "                                WHERE nrovalor = " + lint_NroValor.ToString() + " and Nemotecnico = '" + lstr_Nemotecnico + "' and indicadorcupon = 'V'))) " +
                           "UPDATE [cf].[DevengosMensuales] " +
                           "SET InteresTotal =  CASE WHEN @Diferencia >= 0 THEN InteresTotal  - @Diferencia ELSE InteresTotal + (@Diferencia * -1) end, " +
                           "    Descuento =  CASE WHEN @Diferencia >= 0 THEN (InteresTotal  - @Diferencia) + Cupon ELSE (InteresTotal + (@Diferencia * -1)) + Cupon end " +
                           "WHERE IdDevengoMensual = " +
                           "                         (Select max(IdDevengoMensual) from [cf].[DevengosMensuales] " +
                           "                          where nrovalor = " + lint_NroValor.ToString() + " and Nemotecnico = '" + lstr_Nemotecnico + "') " +
                           "AND nrovalor = " + lint_NroValor.ToString() + " and Nemotecnico = '" + lstr_Nemotecnico + "' ");
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DevengoMensual(int lint_NroValor, string lstr_Nemotecnico, DateTime _fechaValor, decimal _diferencia)
        {
            //TODO: Devengo mensual ejm Ariel

            int TotalDiasDif = 0;
            int DiasDif = 0;
            decimal InteresTotal = 0;
            decimal CuponTotal = 0;
            decimal Descuento = 0;
            decimal DiferenciaAsiento = 0;
            string mens1 = String.Empty;
            string mens2 = String.Empty;

            int contDevengo = 1;

            clsContabilizarDevengoInt ContabilizarDevengo = new clsContabilizarDevengoInt();

            clsDevengoMensual clsDevengoMensual = new clsDevengoMensual();
            List<clsConsultaDevengoInteres> LstDevengoInteres = ConsultaDevengoInteres(lint_NroValor, lstr_Nemotecnico);

            try
            {

                foreach (var devengo in LstDevengoInteres)
                {
                    FchTemp = PeriodosMensualesDevengo(_fechaValor, devengo.Anno1);
                    TotalDiasDif = +(Dias360(devengo.Anno1, _fechaValor));

                    if (DiferenciaMeses(devengo.Anno1, _fechaValor) == 6 && TotalDiasDif > 180)
                        TotalDiasDif = 180;

                    for (int i = 0; i < mesesDev; i++)
                    {
                        try
                        {
                            if (i == 0)
                            {
                                DateTime FchTemporal = _fechaValor;
                                DiasDif = +(Dias360(Convert.ToDateTime(FchTemp[i]), FchTemporal));

                                if (FchTemporal.Month == 2)
                                    DiasDif += 2;

                                if (DiasDif > 30)
                                    DiasDif = 30;
                            }
                            else
                            {
                                DateTime FchTemporal1 = _fechaValor;
                                DateTime FchTemporal2 = Convert.ToDateTime(FchTemp[i]);
                                DiasDif = +(Dias360(FchTemporal2, FchTemporal1));

                                if (FchTemporal2.Month == 2 && DiasDif >= 28)
                                    DiasDif = 30;
                                else
                                {
                                    if (FchTemporal1.Day >= 28 && FchTemporal1.Month == 2)
                                    {
                                        if (DiasDif > 30)
                                            DiasDif = 30;

                                        if (FchTemporal2.Day >= 30)
                                            DiasDif = 30;
                                        else
                                            DiasDif = DiasDif - 2;
                                    }
                                    else
                                    {
                                        if (DiasDif > 30)
                                            DiasDif = 30;
                                    }
                                }
                            }

                            InteresTotal = +(devengo.Intereses1 / TotalDiasDif) * DiasDif;

                            if (LstDevengoInteres.Count == 1)
                                CuponTotal = -FlujoUnCupon;
                            else
                                CuponTotal = (devengo.Pago1 / TotalDiasDif) * DiasDif;

                            Descuento = +(InteresTotal + CuponTotal);
                            DiferenciaAsiento = (InteresTotal + CuponTotal) - Descuento;

                            if (DiferenciaAsiento >= 0)
                                Descuento = Descuento - DiferenciaAsiento;
                            else
                                Descuento = Descuento + DiferenciaAsiento;

                            if (contDevengo == LstDevengoInteres.Count)
                            {
                                if (i == mesesDev - 1)
                                {
                                    if (_diferencia >= 0)
                                    {
                                        InteresTotal = InteresTotal - _diferencia;
                                        Descuento = Descuento - _diferencia;
                                    }
                                    else
                                    {
                                        InteresTotal = InteresTotal + -(_diferencia);
                                        Descuento = Descuento + -(_diferencia);
                                    }
                                }
                            }

                            if (DiasDif > 0)
                                clsDevengoMensual.CrearDevengoMensual(lint_NroValor, lstr_Nemotecnico, FchTemp[i], 1, DiasDif, InteresTotal, CuponTotal,
                                                                       Descuento, "SG", out mens1, out mens2);

                            //ContabilizarDevengo.DevengoIntereses(lint_NroValor.ToString(), lstr_Nemotecnico, _fechaValor);

                            _fechaValor = Convert.ToDateTime(FchTemp[i]);
                            DiferenciaAsiento = 0;
                        }
                        catch
                        { i = mesesDev + 1; }
                    }
                    contDevengo++;
                }

                Mantenimiento.clsDinamico dinamico = new Mantenimiento.clsDinamico();
                dinamico.ConsultarDinamico("UPDATE [cf].[DevengosMensuales] " +  
                                           "SET Descuento = Descuento + ((InteresTotal + Cupon) - Descuento) " +
                                           "WHERE(InteresTotal + Cupon) - Descuento <> 0");

                Decimal porcentaje = get_canje_percentaje(lstr_Nemotecnico, lint_NroValor.ToString());
                
                dinamico.ConsultarDinamico("DECLARE @Diferencia decimal(22,4) " +
                           "SET @Diferencia = " +
                           "                     (SELECT ( " +
                           "                               (SELECT SUM(DESCUENTO) FROM [cf].[DevengosMensuales] where nrovalor = " + lint_NroValor.ToString() + "  and Nemotecnico = '" + lstr_Nemotecnico + "') - " +
                           "                               (SELECT (( (valortransadoNeto * "+porcentaje+") - (valortransadoBruto * "+porcentaje+")) + ( (valortransadoBruto * "+porcentaje+" )- (valorfacial *"+porcentaje+") ) ) * -1 as Cuadre FROM cf.titulosvalores " +
                           "                                WHERE nrovalor = " + lint_NroValor.ToString() + " and Nemotecnico = '" + lstr_Nemotecnico + "' and indicadorcupon = 'V'))) " +
                           "UPDATE [cf].[DevengosMensuales] " +
                           "SET InteresTotal =  CASE WHEN @Diferencia >= 0 THEN InteresTotal  - @Diferencia ELSE InteresTotal + (@Diferencia * -1) end, " +
                           "    Descuento =  CASE WHEN @Diferencia >= 0 THEN (InteresTotal  - @Diferencia) + Cupon ELSE (InteresTotal + (@Diferencia * -1)) + Cupon end " +
                           "WHERE IdDevengoMensual = " +
                           "                         (Select max(IdDevengoMensual) from [cf].[DevengosMensuales] " +
                           "                          where nrovalor = " + lint_NroValor.ToString() + " and Nemotecnico = '" + lstr_Nemotecnico + "') " +
                           "AND nrovalor = " + lint_NroValor.ToString() + " and Nemotecnico = '" + lstr_Nemotecnico + "' ");


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DevengoMensualCeroCupon(int lint_NroValor, string lstr_Nemotecnico)
        {
            decimal InteresTotal = 0;
            decimal CuponTotal = 0;
            decimal Descuento = 0;
            int mesAnt = 0;
            string mens1 = String.Empty;
            string mens2 = String.Empty;
            clsDevengoMensual clsDevengoMensual = new clsDevengoMensual();
            List<clsConsultaDevengoInteres> LstDevengoInteres = ConsultaDevengoInteres(lint_NroValor, lstr_Nemotecnico);
            List<clsConsultaDevengoInteres> Mensuales = new List<clsConsultaDevengoInteres>();
            clsConsultaDevengoInteres mensual;
            int diasPeriodo = 0;

            try
            {
                for (int i = 0; i <= LstDevengoInteres.Count() - 1; i++)
                {
                    if (i == 0)
                        mesAnt = LstDevengoInteres[i].Anno1.Month;

                    if (mesAnt == LstDevengoInteres[i].Anno1.Month)
                    {
                        InteresTotal += LstDevengoInteres[i].Intereses1;
                        CuponTotal += LstDevengoInteres[i].Pago1;
                        Descuento += LstDevengoInteres[i].DescuentoDevengado1;
                        diasPeriodo++;

                        if (i == LstDevengoInteres.Count() - 1)
                        {
                            mensual = new clsConsultaDevengoInteres();
                            mensual.DescuentoDevengado1 = InteresTotal;
                            mensual.Pago1 = 0;
                            mensual.Intereses1 = InteresTotal;
                            mensual.CostoAmortizacionInicial1 = diasPeriodo;
                            mensual.Anno1 = LstDevengoInteres[i].Anno1;
                            Mensuales.Add(mensual);
                        }

                        mesAnt = LstDevengoInteres[i].Anno1.Month;
                    }
                    else
                    {
                        mensual = new clsConsultaDevengoInteres();
                        mensual.DescuentoDevengado1 = Descuento;
                        mensual.Pago1 = CuponTotal;
                        mensual.Intereses1 = InteresTotal;
                        mensual.Anno1 = LstDevengoInteres[i].Anno1.AddDays(-1);
                        mensual.CostoAmortizacionInicial1 = diasPeriodo;
                        Mensuales.Add(mensual);

                        InteresTotal = LstDevengoInteres[i].Intereses1;
                        CuponTotal = LstDevengoInteres[i].Pago1;
                        Descuento = LstDevengoInteres[i].DescuentoDevengado1;
                        diasPeriodo = 1;

                        if (i == LstDevengoInteres.Count() - 1)
                        {
                            mensual = new clsConsultaDevengoInteres();
                            mensual.DescuentoDevengado1 = InteresTotal;
                            mensual.Pago1 = 0;
                            mensual.Intereses1 = InteresTotal;
                            mensual.CostoAmortizacionInicial1 = diasPeriodo;
                            mensual.Anno1 = LstDevengoInteres[i].Anno1;
                            Mensuales.Add(mensual);
                        }

                        mesAnt = LstDevengoInteres[i].Anno1.Month;
                    }                   
                }

                foreach (var devengo in Mensuales)
                {
                    devengo.CostoAmortizacionInicial1 = devengo.CostoAmortizacionInicial1 > 30 ? 30 : devengo.CostoAmortizacionInicial1;

                    if (devengo.Anno1.Month == 2 && devengo.CostoAmortizacionInicial1 == 28)
                        devengo.CostoAmortizacionInicial1 = 30;

                    clsDevengoMensual.CrearDevengoMensual(lint_NroValor, lstr_Nemotecnico, devengo.Anno1.ToString("dd/MM/yyyy"), 1, (int)devengo.CostoAmortizacionInicial1, devengo.Intereses1, devengo.Pago1,
                                                            devengo.Intereses1, "SG", out mens1, out mens2);
                }

                Mantenimiento.clsDinamico dinamico = new Mantenimiento.clsDinamico();
                Decimal porcentaje = get_canje_percentaje(lstr_Nemotecnico, lint_NroValor.ToString());

                dinamico.ConsultarDinamico("DECLARE @Diferencia decimal(22,4) " +
                           "SET @Diferencia = " +
                           "                     (SELECT ( " +
                           "                               (SELECT SUM(DESCUENTO) FROM [cf].[DevengosMensuales] where nrovalor = " + lint_NroValor.ToString() + "  and Nemotecnico = '" + lstr_Nemotecnico + "') - " +
                           "                               (SELECT (valortransadoBruto * " + porcentaje + ") - (valorfacial *" + porcentaje + ")) * -1 as Cuadre FROM cf.titulosvalores " +
                           "                                WHERE nrovalor = " + lint_NroValor.ToString() + " and Nemotecnico = '" + lstr_Nemotecnico + "' and indicadorcupon = 'V'))) " +
                           "UPDATE [cf].[DevengosMensuales] " +
                           "SET InteresTotal =  CASE WHEN @Diferencia >= 0 THEN InteresTotal  - @Diferencia ELSE InteresTotal + (@Diferencia * -1) end, " +
                           "    Descuento =  CASE WHEN @Diferencia >= 0 THEN (InteresTotal  - @Diferencia) + Cupon ELSE (InteresTotal + (@Diferencia * -1)) + Cupon end " +
                           "WHERE IdDevengoMensual = " +
                           "                         (Select max(IdDevengoMensual) from [cf].[DevengosMensuales] " +
                           "                          where nrovalor = " + lint_NroValor.ToString() + " and Nemotecnico = '" + lstr_Nemotecnico + "') " +
                           "AND nrovalor = " + lint_NroValor.ToString() + " and Nemotecnico = '" + lstr_Nemotecnico + "' ");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public decimal CalculoTIR(double[] arreglo)
        {
            try
            {
                double retorno = 0;
                try
                {
                    retorno = Microsoft.VisualBasic.Financial.IRR(ref arreglo, .1);
                }
                catch (Exception E)
                {
                    retorno = Microsoft.VisualBasic.Financial.IRR(ref arreglo, -.1);
                }
                return Convert.ToDecimal(retorno * 100);
            }
            catch
            {
                return 0;
            }
        }

        //        public double CalculoIRR(double[] cf)
        //{
        //    int numOfFlows = cf.Length;
        // int i = 0,j = 0;
        // double m = 0.0;
        // double old = 0.00;
        // double new1 = 0.00;
        // double oldguessRate = LOW_RATE;
        // double newguessRate = LOW_RATE;
        // double guessRate = LOW_RATE;
        // double lowGuessRate = LOW_RATE;
        // double highGuessRate = HIGH_RATE;
        // double npv = 0.0;
        // double denom = 0.0;
        // for(i=0; i<MAX_ITERATION; i++)
        // {
        //  npv = 0.00;
        //  for(j=0; j<numOfFlows; j++)
        //  {
        //   denom =  Math.Pow ((1 + guessRate),j);
        //   npv = npv + (cf[j]/denom);
        //  }
        //   /* Stop checking once the required precision is achieved */
        //  if((npv > 0) && (npv < PRECISION_REQ))
        //   break;
        //  if(old == 0)
        //   old = npv;
        //  else
        //   old = new1;
        //  new1 = npv;
        //  if(i > 0)
        //  {
        //   if(old < new1)
        //   {
        //    if(old < 0 && new1 < 0)
        //     highGuessRate = newguessRate;
        //    else
        //     lowGuessRate = newguessRate;
        //   }
        //   else
        //   {
        //    if(old > 0 && new1 > 0)
        //     lowGuessRate = newguessRate;
        //    else
        //     highGuessRate = newguessRate;
        //   }
        //  }
        //  oldguessRate = guessRate;
        //  guessRate = (lowGuessRate + highGuessRate) / 2;
        //  newguessRate = guessRate;
        // }
        // return guessRate;
        //}

        public List<Cupones> CuponesPorTitulo(string NroValor, string Nemotecnico, out decimal _periodicidad)
        {
            _periodicidad = 1;
            clsTituloValor cupones = new clsTituloValor();
            DataTable listaCupon = new DataTable();
            DataTable dtCupones = new DataTable();
            List<Cupones> Cupones = new List<Cupones>();
            
            try
            {
                listaCupon = cupones.ConsultarTituloValor(Convert.ToInt32(NroValor), Nemotecnico, String.Empty, String.Empty,"C", String.Empty, String.Empty, String.Empty, Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/5000"),string.Empty).Tables[0];
                DataView dv = listaCupon.DefaultView;
                dv.RowFilter = "EstadoValor in ('Vigente','Cancelada','Prescrita')";
                dv.Sort = "NroCupon ASC";
                dtCupones = dv.ToTable();
                //TODO:
                Decimal porcentajeCanje = get_canje_percentaje(Nemotecnico, NroValor);

                foreach (DataRow dr_cupon in dtCupones.Rows)
                {
                    Cupones cupon = new Cupones();
                    cupon.TasaBruta = Convert.ToDecimal(dr_cupon["TasaBruta"].ToString());
                    cupon.TasaNeta = Convert.ToDecimal(dr_cupon["TasaNeta"].ToString());
                    cupon.InteresBruto = Convert.ToDecimal(dr_cupon["InteresBruto"].ToString()) * porcentajeCanje;
                    cupon.InteresNeto = Convert.ToDecimal(dr_cupon["InteresNeto"].ToString()) * porcentajeCanje;
                    cupon.FechaInicio = Convert.ToDateTime(dr_cupon["FchInicio"].ToString());
                    cupon.FechaFin = Convert.ToDateTime(dr_cupon["FchVencimiento"].ToString());
                    cupon.Periodicidad = (cupon.FechaFin.Month - cupon.FechaInicio.Month);
                    Cupones.Add(cupon);
                }

                if (listaCupon.Rows.Count == 1)
                    _periodicidad = Cupones[0].Periodicidad == 0 ? 1 : Cupones[0].Periodicidad;
                else
                    _periodicidad = Cupones[1].Periodicidad > 0 ? Cupones[1].Periodicidad : Cupones[1].Periodicidad * -1;

                _periodicidad = _periodicidad == 0 ? 1 : _periodicidad;

                return Cupones;
            }
            catch (Exception ex)
            {
                return Cupones;
            }
        }

        public List<PeriodosCupones> FechasCupones(string NroValor, string Nemotecnico)
        {
            clsTituloValor cupones = new clsTituloValor();
            DataTable listaCupon = new DataTable();
            DataTable Cupones = new DataTable();
            List<PeriodosCupones> periodicidad = new List<PeriodosCupones>();

            try
            {
                listaCupon = null;// cupones.ConsultarTituloValor(NroValor, Nemotecnico, "%", "%", "%", "01/01/1900", "01/01/5000").Tables[0].Select("IndicadorCupon = 'C'").CopyToDataTable();
                DataView dv = listaCupon.DefaultView;
                dv.Sort = "NroCupon ASC";
                Cupones = dv.ToTable();

                foreach (DataRow dr_cupon in Cupones.Rows)
                {
                    PeriodosCupones periodo = new PeriodosCupones();
                    periodo.FchInicio = Convert.ToDateTime(dr_cupon["FchInicio"].ToString());
                    periodo.FchFin = Convert.ToDateTime(dr_cupon["FchVencimiento"].ToString());
                    periodo.Periodicidad = (periodo.FchFin.Month - periodo.FchInicio.Month);

                    periodicidad.Add(periodo);
                }

                return periodicidad;

            }
            catch
            {
                return periodicidad;
            }
            
        }

        public static double Xirr(List<double> montos, List<DateTime> fechas)
        {
            var xlApp = new Microsoft.Office.Interop.Excel.Application();

            var datesAsDoubles = new List<double>();
            foreach (var date in fechas)
            {
                var totalDays = (date - DateTime.MinValue).TotalDays;
                datesAsDoubles.Add(totalDays);
            }

            var valuesArray = montos.ToArray();
            var datesArray = datesAsDoubles.ToArray();

            return xlApp.WorksheetFunction.Xirr(valuesArray, datesArray);
        }

        //TODO: cambiar a decimal
        public decimal CalculoXTIR(List<XTIR> listaXtir)
        {
            try
            {
                //TODO: Funcion principal de la XTIR
                return CalcXirr.CalculateXIRR(listaXtir, 0.00000000000001m, 100);
            }
            catch (Exception ex)
            {
                System.IO.File.WriteAllText(@"C:\Users\Public\log.txt", ex.Message.ToString());
                return 0;
            }
        }

        public static decimal MonthDifference(DateTime FechaFin, DateTime FechaInicio)
        {
            return Math.Abs((FechaFin.Month - FechaInicio.Month) + 12 * (FechaFin.Year - FechaInicio.Year));

        }
        public decimal CalcularDiasDeDiferencia(DateTime primerFecha, DateTime segundaFecha)
        {
            TimeSpan diferencia;
            diferencia = primerFecha - segundaFecha;

            return diferencia.Days;
        }

        #region devengo cero cupon
        public void DevengoCeroCupon(int _nroValor, string _Nemotecnico, string exacto)//(string NroValor, string Nemotecnico)
        {
            DataTable ldat_Valores = new DataTable();
            clsTituloValor lcls_TituloValor = new clsTituloValor();
            clsDevengoInteres lcls_DevengoInteres = new clsDevengoInteres();
            clsContabilizarDevengoInt ContabilizarDevengo = new clsContabilizarDevengoInt();
            decimal ldec_TIR = 0;
            int lint_NroValor = 0;
            string lstr_Nemotecnico = String.Empty;
            decimal ldec_TransBruto = 0;
            decimal ldec_ValorFacial = 0;
            decimal ldec_CostAmortIni = 0;
            decimal ldec_Interes = 0;
            decimal ldec_CostAmortFin = 0;
            decimal ldec_Pago = 0;
            decimal ldec_PenultPago = 0;
            decimal ldec_Devengado = 0;
            double redondeo = 0.02;
            int cont = 0;
            string mens1 = String.Empty;
            string mens2 = String.Empty;
            double x = 1.0;
            double y = 365.0;

            decimal sumaIntereses = 0;
            decimal montoprevio = 0;

            try
            {

                if (exacto.Equals("S"))
                {
                    ldat_Valores = lcls_TituloValor.ConsultarTituloValor(_nroValor, _Nemotecnico, String.Empty, String.Empty, String.Empty, "Cero Cupón", String.Empty, "Vigente", Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/5000"), string.Empty).Tables[0];
                }
                else if(exacto.Equals("N"))
                {
                    ldat_Valores = lcls_TituloValor.ConsultarTituloValor(null, String.Empty, String.Empty, String.Empty, String.Empty, "Cero Cupón", String.Empty, "Vigente", Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/5000"), string.Empty).Tables[0];

                    if (_nroValor > 0)
                    {
                        var strExpr = " NroValor > " + _nroValor.ToString();
                        ldat_Valores = ldat_Valores.Select(strExpr).CopyToDataTable();
                    }
                }

                ldat_Valores = ajusteTitulosCompra(ldat_Valores);

                //for (int i = 0; i < ldat_Valores.Rows.Count; i++)
                foreach(DataRow ldr_valor in ldat_Valores.Rows)
                {
                    lint_NroValor = Convert.ToInt32(ldr_valor["NroValor"].ToString());
                    lstr_Nemotecnico = ldr_valor["Nemotecnico"].ToString();
                    ldec_TransBruto = Convert.ToDecimal(ldr_valor["ValorTransadoBruto"].ToString());
                    ldec_ValorFacial = Convert.ToDecimal(ldr_valor["ValorFacial"].ToString());

                    if (ldec_ValorFacial == ldec_TransBruto)
                    {
                        FlujoEfectivoCeroCupon(ldr_valor);
                        ldec_TIR = 0;
                    }
                    else
                    {
                        //FlujoEfectivoCeroCupon(ldat_Valores.Rows[i]);
                        //ldec_TIR = 0;
                        ldec_TIR = Convert.ToDecimal(CalculoXTIR(FlujoEfectivoCeroCupon(ldr_valor)));
                    }

                    for (int j = 1; j < lint_ContPeriodos - 1; j++)
                    {
                        if (j == 1)
                        {
                            ldec_CostAmortIni = ldec_TransBruto;                            
                            //ldec_Interes = ldec_CostAmortIni * (ldec_TIR / 100);
                            ldec_Interes = ldec_CostAmortIni * Convert.ToDecimal((Math.Pow((1 + Convert.ToDouble(ldec_TIR)), Convert.ToDouble(x / y)) - 1));
                            ldec_Interes = ldec_Interes + Convert.ToDecimal(redondeo);
                            //ldec_Interes = ldec_CostAmortIni * (Convert.ToDecimal(Math.Pow((1 + Convert.ToDouble(ldec_TIR)), (1 / 365)))-1);
                            ldec_CostAmortFin = ldec_CostAmortIni + ldec_Interes + ldec_Pago;
                            ldec_Devengado = ldec_Interes + ldec_Pago;

                            DateTime FchPeriodo = Convert.ToDateTime(fechas[j]);

                            lcls_DevengoInteres.CrearDevengoInteres(lint_NroValor, lstr_Nemotecnico,
                                //Convert.ToDateTime(fechas[j])
                                FchPeriodo, 1
                                , ldec_CostAmortIni,
                                ldec_Interes, ldec_Pago, ldec_CostAmortFin, ldec_Devengado, ldec_TIR, "ACT", "SG", out mens1, out mens2);
                            montoprevio = ldec_Interes;

                            sumaIntereses += ldec_Interes;

                            //saca si es el último día del periodo                            
                            if(FchPeriodo.Day == DateTime.DaysInMonth(FchPeriodo.Year, FchPeriodo.Month))
                            {
                                //ContabilizarDevengo.DevengoInteresesCeroCupon(lint_NroValor.ToString(), lstr_Nemotecnico, sumaIntereses, FchPeriodo);
                                sumaIntereses = 0;
                            }
                            //
                        }
                        else
                        {
                            ldec_CostAmortIni = ldec_CostAmortFin;
                            ldec_Interes = ldec_CostAmortIni * Convert.ToDecimal((Math.Pow((1 + Convert.ToDouble(ldec_TIR)), Convert.ToDouble(x / y)) - 1));
                            ldec_Interes = ldec_Interes + Convert.ToDecimal(redondeo);
                            ldec_CostAmortFin = ldec_CostAmortIni + ldec_Interes + ldec_Pago;
                            //ldec_Devengado = ldec_Interes + ldec_Pago;
                            ldec_Devengado = ldec_Interes + montoprevio;
                            montoprevio = ldec_Devengado;

                            DateTime FchPeriodo = Convert.ToDateTime(fechas[j]);

                            lcls_DevengoInteres.CrearDevengoInteres(lint_NroValor, lstr_Nemotecnico,
                                //Convert.ToDateTime(fechas[j])
                                Convert.ToDateTime(fechas[j]),1
                                , ldec_CostAmortIni,
                                ldec_Interes, ldec_Pago, ldec_CostAmortFin, ldec_Devengado, ldec_TIR, "ACT", "SG", out mens1, out mens2);
                            //saca si es el último día del periodo    

                            sumaIntereses += ldec_Interes;

                            if (FchPeriodo.Day == DateTime.DaysInMonth(FchPeriodo.Year, FchPeriodo.Month))
                            {
                                //ContabilizarDevengo.DevengoInteresesCeroCupon(lint_NroValor.ToString(), lstr_Nemotecnico, sumaIntereses, FchPeriodo);
                                sumaIntereses = 0;
                            }
                            //
                        }

                        cont = j;
                    }
                    ldec_CostAmortIni = ldec_CostAmortFin;
                    ldec_Interes = ldec_CostAmortIni * Convert.ToDecimal((Math.Pow((1 + Convert.ToDouble(ldec_TIR)), Convert.ToDouble(x / y)) - 1));
                    ldec_Pago = -ldec_ValorFacial;
                    ldec_CostAmortFin = ldec_CostAmortIni + ldec_Interes + ldec_Pago;
                    //ldec_Devengado = ldec_Interes + ldec_PenultPago;
                    ldec_Devengado = ldec_Interes + montoprevio;

                    DateTime FchPeriodos = Convert.ToDateTime(fechas[cont + 1]);

                    lcls_DevengoInteres.CrearDevengoInteres(lint_NroValor, lstr_Nemotecnico,
                        //Convert.ToDateTime(fechas[cont + 1])
                        FchPeriodos, 1
                        , ldec_CostAmortIni,
                        ldec_Interes, ldec_Pago, ldec_CostAmortFin, ldec_Devengado, ldec_TIR, "ACT", "SG", out mens1, out mens2);
                    ldec_Pago = 0;
                    //DevengoMensual(lint_NroValor, lstr_Nemotecnico);
                    //saca si es el último día del periodo

                    Mantenimiento.clsDinamico dinamico = new Mantenimiento.clsDinamico();
                    dinamico.ConsultarDinamico("DECLARE @IdDevengoInteres int DECLARE @Diferencia decimal(22,4) " +
                               "SET @IdDevengoInteres = (Select max(IdDevengoInteres) from [cf].[DevengosIntereses] where nrovalor = " + lint_NroValor.ToString() + "  and Nemotecnico = '" + lstr_Nemotecnico + "') " +
                               "SET @Diferencia	   = ((SELECT ((valortransadoBruto - valorfacial)) * -1 as Cuadre " +
                               "                       FROM cf.titulosvalores WHERE indicadorcupon = 'V' and nrovalor = " + lint_NroValor.ToString() + "  and Nemotecnico = '" + lstr_Nemotecnico + "') - " +
                               "                      (Select DescuentoDevengado from [cf].[DevengosIntereses] " +
                               "                       where IdDevengoInteres = @IdDevengoInteres and nrovalor = " + lint_NroValor.ToString() + " and Nemotecnico = '" + lstr_Nemotecnico + "')) " +
                               "UPDATE [cf].[DevengosIntereses] " +
                               "SET Intereses =  CASE WHEN @Diferencia >= 0 THEN Intereses  + @Diferencia ELSE Intereses - (@Diferencia * -1) end, " +
                               "    DescuentoDevengado =  (SELECT (valortransadoBruto - valorfacial) * -1 FROM cf.titulosvalores " +
                               "                           WHERE indicadorcupon = 'V' and nrovalor = " + lint_NroValor.ToString() + " and Nemotecnico = '" + lstr_Nemotecnico + "'), " +
                               "    CostoAmortizacionFinal = 0 " +
                               "WHERE IdDevengoInteres =  @IdDevengoInteres " +
                               "AND nrovalor = " + lint_NroValor.ToString() + " and Nemotecnico = '" + lstr_Nemotecnico + "' ");


                    DevengoMensualCeroCupon(lint_NroValor, lstr_Nemotecnico);

                    sumaIntereses += ldec_Interes;
                    //ContabilizarDevengo.DevengoInteresesCeroCupon(lint_NroValor.ToString(), lstr_Nemotecnico, sumaIntereses, FchPeriodos);
                    //
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public List<XTIR> FlujoEfectivoCeroCupon(DataRow ldrw_CeroCupon)
        {
            double[] ldec_ArregloTIR;
            string mens1 = String.Empty;
            string mens2 = String.Empty;
            clsCalculoFlujoEfectivo lcls_CalculoFlujoEfectivo = new clsCalculoFlujoEfectivo();
            int lint_NroValor = 0;
            string lstr_Nemotecnico = String.Empty;
            DateTime ldt_FchValor = new DateTime();
            DateTime ldt_FchVencimiento = new DateTime();
            decimal ldec_TransNeto = 0;
            decimal ldec_ValorFacial = 0;
            decimal ldec_TransBruto = 0;
            decimal ldec_PrimaDesc = 0;
            decimal ldec_PlazoValor = 0;
            bool lbol_EsLargoPlazo = true;
            List<XTIR> lstXtir = new List<XTIR>();
            XTIR xTir;

            decimal ldec_TasaBruta = 0;
            decimal ldec_TasaMensual = 0; //TasaInteres

            DateTime ldec_FchRecorrido = new DateTime();

            try
            {

                lint_NroValor = Convert.ToInt32(ldrw_CeroCupon["NroValor"].ToString());
                lstr_Nemotecnico = ldrw_CeroCupon["Nemotecnico"].ToString();
                ldt_FchValor = Convert.ToDateTime(ldrw_CeroCupon["FchValor"].ToString()); //primer registro de la tabla
                ldt_FchVencimiento = Convert.ToDateTime(ldrw_CeroCupon["FchVencimiento"].ToString()); //ultimo registro de la tabla
                ldec_TransNeto = Convert.ToDecimal(ldrw_CeroCupon["ValorTransadoNeto"].ToString());
                ldec_ValorFacial = Convert.ToDecimal(ldrw_CeroCupon["ValorFacial"].ToString()); //ultimo valor negativo del flujo de efectivo
                ldec_TransBruto = Convert.ToDecimal(ldrw_CeroCupon["ValorTransadoBruto"].ToString()); //primer valor del flujo de efectivo
                ldec_TasaBruta = Convert.ToDecimal(ldrw_CeroCupon["TasaBruta"].ToString());
                ldec_PlazoValor = Convert.ToDecimal(ldrw_CeroCupon["PlazoValor"].ToString());

                ldec_PrimaDesc = ldec_ValorFacial - ldec_TransBruto;

                //averigua si el valor es a largo o corto plazo
                //if (ldec_PlazoValor <= 365)
                //{
                //    lbol_EsLargoPlazo = false;
                //    ldec_TasaMensual = ldec_TasaBruta / 12;
                //}
                //else
                //{
                //    ldec_TasaMensual = ldec_TasaBruta / 2;
                //}
                
                

                //if (lbol_EsLargoPlazo)
                //{
                //    //PeriodosSemestrales(ldt_FchValor, ldt_FchVencimiento);

                //    //lint_ContPeriodos = meses;

                //    //ldec_ArregloTIR = new double[lint_ContPeriodos];

                //    //lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivo(lint_NroValor, lstr_Nemotecnico, ldt_FchValor.ToString("dd/MM/yyyy"), ldec_TasaMensual,
                //    //    0, ldec_TransBruto, "SG", out mens1, out mens2);
                //    //ldec_ArregloTIR[0] = Convert.ToDouble(ldec_TransBruto);

                //    //int i = 1;
                //    //ldec_FchRecorrido = ldt_FchValor;
                //    //while (ldec_FchRecorrido <= ldt_FchVencimiento)                    
                //    ////for (int i = 1; i < (lint_ContPeriodos - 1); i++)
                //    //{
                //    //    ldec_FchRecorrido = ldec_FchRecorrido.AddDays(1);
                //    //    //lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivo(lint_NroValor, lstr_Nemotecnico, fechas[i], ldec_TasaMensual,
                //    //    //    0, 0, "SG", out mens1, out mens2);
                //    //    lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivo(lint_NroValor, lstr_Nemotecnico, ldec_FchRecorrido.ToString("dd/MM/yyyy"), ldec_TasaMensual,
                //    //        0, 0, "SG", out mens1, out mens2);
                //    //    ldec_ArregloTIR[i] = 0;
                //    //    i++;                        
                //    //}

                //    //lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivo(lint_NroValor, lstr_Nemotecnico, ldt_FchVencimiento.ToString("dd/MM/yyyy"), ldec_TasaMensual,
                //    //    0, -ldec_ValorFacial, "SG", out mens1, out mens2);
                //    //ldec_ArregloTIR[lint_ContPeriodos - 1] = Convert.ToDouble(-ldec_ValorFacial);

                //    //return ldec_ArregloTIR;

                //    //PeriodosMensuales(ldt_FchValor, ldt_FchVencimiento);
                //    //lint_ContPeriodos = meses;
                //    lint_ContPeriodos = 0;
                //    ldec_FchRecorrido = ldt_FchValor;

                //    while (ldec_FchRecorrido < ldt_FchVencimiento)
                //    {
                //        lint_ContPeriodos++;
                //        ldec_FchRecorrido = ldec_FchRecorrido.AddDays(1);
                //    }
                //    lint_ContPeriodos++;

                //    ldec_ArregloTIR = new double[lint_ContPeriodos];

                //    fechas = new string[lint_ContPeriodos];

                //    lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivo(lint_NroValor, lstr_Nemotecnico, ldt_FchValor.ToString("dd/MM/yyyy"), ldec_TasaMensual,
                //        0, ldec_TransBruto, "SG", out mens1, out mens2);
                //    ldec_ArregloTIR[0] = Convert.ToDouble(ldec_TransBruto);

                //    //fechas[0] = ldt_FchValor.ToString("dd/MM/yyyy");

                //    int i = 1;
                //    ldec_FchRecorrido = ldt_FchValor;

                //    while (ldec_FchRecorrido < ldt_FchVencimiento)
                //    //for (int i = 1; i < (lint_ContPeriodos - 1); i++)
                //    {
                //        //lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivo(lint_NroValor, lstr_Nemotecnico, fechas[i], ldec_TasaMensual,
                //        //    0, 0, "SG", out mens1, out mens2);
                //        lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivo(lint_NroValor, lstr_Nemotecnico, ldec_FchRecorrido.ToString("dd/MM/yyyy"), ldec_TasaMensual,
                //            0, 0, "SG", out mens1, out mens2);
                //        ldec_ArregloTIR[i] = 0;
                //        fechas[i - 1] = ldec_FchRecorrido.ToString("dd/MM/yyyy");
                //        ldec_FchRecorrido = ldec_FchRecorrido.AddDays(1);
                //        i++;
                //    }

                //    //for (int i = 1; i < (lint_ContPeriodos - 1); i++)
                //    //{
                //    //    lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivo(lint_NroValor, lstr_Nemotecnico, fechas[i], ldec_TasaMensual,
                //    //        0, 0, "SG", out mens1, out mens2);
                //    //    ldec_ArregloTIR[i] = 0;
                //    //}

                //    lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivo(lint_NroValor, lstr_Nemotecnico, ldt_FchVencimiento.ToString("dd/MM/yyyy"), ldec_TasaMensual,
                //        0, -ldec_ValorFacial, "SG", out mens1, out mens2);
                //    ldec_ArregloTIR[lint_ContPeriodos - 1] = Convert.ToDouble(-ldec_ValorFacial);
                //    fechas[lint_ContPeriodos - 1] = ldt_FchVencimiento.ToString("dd/MM/yyyy");
                //    return ldec_ArregloTIR;

                //}
                //else
                //{
                    //PeriodosMensuales(ldt_FchValor, ldt_FchVencimiento);
                    //lint_ContPeriodos = meses;
                    lint_ContPeriodos = 0;
                    ldec_FchRecorrido = ldt_FchValor;

                    while (ldec_FchRecorrido < ldt_FchVencimiento)
                    {                        
                        lint_ContPeriodos++;
                        ldec_FchRecorrido = ldec_FchRecorrido.AddDays(1);
                    }
                    lint_ContPeriodos++;

                    ldec_TasaMensual = ldec_TasaBruta / 12;

                    ldec_ArregloTIR = new double[lint_ContPeriodos];

                    fechas = new string[lint_ContPeriodos];

                    lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivo(lint_NroValor, lstr_Nemotecnico, ldt_FchValor.ToString("dd/MM/yyyy"), ldec_TasaMensual,
                        0, ldec_TransBruto,"", "SG", out mens1, out mens2);
                    ldec_ArregloTIR[0] = Convert.ToDouble(ldec_TransBruto);


                    //fechas[0] = ldt_FchValor.ToString("dd/MM/yyyy");
                    xTir = new XTIR();
                    xTir.Monto = ldec_TransBruto;
                    xTir.Fecha = ldt_FchValor;
                    lstXtir.Add(xTir);

                    int i = 1;
                    ldec_FchRecorrido = ldt_FchValor;

                    while (ldec_FchRecorrido < ldt_FchVencimiento)
                    //for (int i = 1; i < (lint_ContPeriodos - 1); i++)
                    {
                        //lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivo(lint_NroValor, lstr_Nemotecnico, fechas[i], ldec_TasaMensual,
                        //    0, 0, "SG", out mens1, out mens2);
                        lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivo(lint_NroValor, lstr_Nemotecnico, ldec_FchRecorrido.ToString("dd/MM/yyyy"), ldec_TasaMensual,
                            0, 0,"", "SG", out mens1, out mens2);
                        ldec_ArregloTIR[i] = 0;
                        fechas[i-1] = ldec_FchRecorrido.ToString("dd/MM/yyyy");
                        ldec_FchRecorrido = ldec_FchRecorrido.AddDays(1);

                        xTir = new XTIR();
                        xTir.Monto = 0;
                        xTir.Fecha = ldec_FchRecorrido;
                        lstXtir.Add(xTir);

                        i++;
                    }

                    //for (int i = 1; i < (lint_ContPeriodos - 1); i++)
                    //{
                    //    lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivo(lint_NroValor, lstr_Nemotecnico, fechas[i], ldec_TasaMensual,
                    //        0, 0, "SG", out mens1, out mens2);
                    //    ldec_ArregloTIR[i] = 0;
                    //}

                    lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivo(lint_NroValor, lstr_Nemotecnico, ldt_FchVencimiento.ToString("dd/MM/yyyy"), ldec_TasaMensual,
                        0, -ldec_ValorFacial,"", "SG", out mens1, out mens2);
                    ldec_ArregloTIR[lint_ContPeriodos - 1] = Convert.ToDouble(-ldec_ValorFacial);
                    fechas[lint_ContPeriodos - 1] = ldt_FchVencimiento.ToString("dd/MM/yyyy");

                    xTir = new XTIR();
                    xTir.Monto = -ldec_ValorFacial;
                    xTir.Fecha = ldt_FchVencimiento;
                    lstXtir.Add(xTir);
                    return lstXtir;
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region devengo tasa fija

        private bool VerificaPeriodicidadCompleta(List<XTIR> pLstTir)
        {
            int x = 0;
            bool completa = true;
            DateTime fechaAnterior = DateTime.Now;
            decimal diasAnt = 0;
            foreach (var tir in pLstTir)
            {
                if (x != 0)
                {
                    if (x > 1)
                    {
                        if (x > 2)
                        {
                            if (DiferenciaMeses(tir.Fecha, fechaAnterior) != diasAnt)
                                completa = false;
                        }
                        diasAnt = DiferenciaMeses(tir.Fecha, fechaAnterior);
                    }
                    fechaAnterior = tir.Fecha;
                }
                x++;
            }

            return completa;
        }
        public void DevengoTasaFija(int _nroValor, string _Nemotecnico, string exacto)
        {
            DataTable ldat_Valores = new DataTable();
            clsTituloValor lcls_TituloValor = new clsTituloValor();
            clsDevengoInteres lcls_DevengoInteres = new clsDevengoInteres();
            List<clsConsultaCalculoFlujoEfectivo> lstConsultaCalculo = new List<clsConsultaCalculoFlujoEfectivo>();
            decimal ldec_TIR = 0;
            int lint_NroValor = 0;
            string lstr_Nemotecnico = String.Empty;
            decimal ldec_TransBruto = 0;
            decimal ldec_ValorFacial = 0;
            decimal ldec_CostAmortIni = 0;
            decimal ldec_Interes = 0;
            decimal ldec_CostAmortFin = 0;
            decimal ldec_Pago = 0;
            decimal primerFlujo = 0;
            decimal ldec_Devengado = 0;
            int cont = 0;
            string mens1 = String.Empty;
            string mens2 = String.Empty;
            DateTime fechaValor;
            decimal montoCuponCorrido = 0;
            decimal transNeto = 0;
            bool esTirNormal = true;
            DateTime fechaAnterior = DateTime.Now;
            decimal difDias = 0;
            double uno = 1;
            double dias365 = 365;
            decimal difAsiento = 0;

            try
            {
                if (exacto.Equals("S"))
                {
                    ldat_Valores = lcls_TituloValor.ConsultarTituloValor(_nroValor, _Nemotecnico, String.Empty, String.Empty, String.Empty, "Tasa Fija", String.Empty, "Vigente", Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/5000"), string.Empty).Tables[0];
                }
                else if (exacto.Equals("N"))
                {
                    ldat_Valores = lcls_TituloValor.ConsultarTituloValor(null, String.Empty, String.Empty, String.Empty, String.Empty, "Tasa Fija", String.Empty, "Vigente", Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/5000"), string.Empty).Tables[0];

                    if (_nroValor > 0)
                    {
                        var strExpr = " NroValor > " + _nroValor.ToString();
                        ldat_Valores = ldat_Valores.Select(strExpr).CopyToDataTable();
                    }
                }

                ldat_Valores = ajusteTitulosCompra(ldat_Valores);

                foreach(DataRow ldr_Valor in ldat_Valores.Rows)
                {
                    //ldec_TIR = CalculoTIR(FlujoEfectivoTF(ldat_Valores.Rows[i]));

                    List<XTIR> lstTir = FlujoEfectivoTF(ldr_Valor);

                    lstr_Nemotecnico = ldr_Valor["Nemotecnico"].ToString();
                    ldec_TransBruto = Convert.ToDecimal(ldr_Valor["ValorTransadoBruto"].ToString());
                    ldec_ValorFacial = Convert.ToDecimal(ldr_Valor["ValorFacial"].ToString());
                    transNeto = Convert.ToDecimal(ldr_Valor["ValorTransadoNeto"].ToString());
                    lint_NroValor = Convert.ToInt32(ldr_Valor["NroValor"].ToString());
                    fechaValor = Convert.ToDateTime(ldr_Valor["FchValor"]);
                    montoCuponCorrido = transNeto - ldec_TransBruto;

                    if (montoCuponCorrido > 0 || !VerificaPeriodicidadCompleta(lstTir))
                    {
                        ldec_TIR = (decimal)CalculoXTIR(lstTir) * 100;
                        esTirNormal = false;
                    }  
                    else
                    {
                        double[] vTir = new double[lstTir.Count()];
                        int vect = 0;
                        foreach (XTIR ir in lstTir)
                        {
                            vTir[vect] = (double)ir.Monto;
                            vect++;
                        }
                        ldec_TIR = CalculoTIR(vTir);
                        esTirNormal = true;
                    }

                    lstConsultaCalculo = ConsultaFlujoEfectivo(lint_NroValor, lstr_Nemotecnico);
                                       
                    foreach (var flujoEfectivo in lstConsultaCalculo)
                    {
                        if (cont != 0)
                        {
                            if (cont == 1)
                                ldec_CostAmortIni = ldec_TransBruto + montoCuponCorrido;
                            else
                                ldec_CostAmortIni = ldec_CostAmortFin;

                            if (cont < lstConsultaCalculo.Count() - 1)
                            {
                                if (esTirNormal)
                                    ldec_Interes = ldec_CostAmortIni * (ldec_TIR / 100);
                                else
                                {
                                    difDias = CalcularDiasDeDiferencia(Convert.ToDateTime(flujoEfectivo.Periodo), fechaAnterior);
                                    ldec_Interes = Convert.ToDecimal((double)ldec_CostAmortIni * (Math.Pow((uno + ((double)ldec_TIR/100)), ((double)difDias / dias365)) - uno));
                                    //ldec_Interes = ldec_CostAmortIni * (ldec_TIR / 100);
                                }
                                    
                                ldec_Pago = flujoEfectivo.FlujoEfectivo;
                                ldec_CostAmortFin = ldec_CostAmortIni + ldec_Interes + ldec_Pago;
                                ldec_Devengado = ldec_Interes + ldec_Pago;
                            }
                            else
                            {
                                if (lstConsultaCalculo.Count() - 1 == 1)
                                {
                                    if (esTirNormal)
                                        ldec_Interes = ldec_CostAmortIni * (ldec_TIR / 100);
                                    else
                                    {
                                        difDias = CalcularDiasDeDiferencia(Convert.ToDateTime(flujoEfectivo.Periodo), fechaAnterior);
                                        ldec_Interes = Convert.ToDecimal((double)ldec_CostAmortIni * (Math.Pow((uno + ((double)ldec_TIR / 100)), ((double)difDias / dias365)) - uno));
                                        //ldec_Interes = ldec_CostAmortIni * (ldec_TIR / 100);
                                    }

                                    ldec_Pago = flujoEfectivo.FlujoEfectivo;
                                    ldec_CostAmortFin = (ldec_CostAmortIni + ldec_Interes + ldec_Pago);
                                    ldec_Devengado = ldec_Interes - flujoEfectivo.Interes;

                                    if (ldec_CostAmortFin >= 0)
                                        ldec_Devengado = ldec_Devengado - ldec_CostAmortFin;
                                    else
                                        ldec_Devengado = ldec_Devengado + ldec_CostAmortFin;

                                    ldec_CostAmortFin = 0;

                                }
                                else
                                {
                                    ldec_CostAmortIni = ldec_CostAmortFin;

                                    if (esTirNormal)
                                        ldec_Interes = ldec_CostAmortIni * (ldec_TIR / 100);
                                    else
                                    {
                                        difDias = CalcularDiasDeDiferencia(Convert.ToDateTime(flujoEfectivo.Periodo), fechaAnterior);
                                        ldec_Interes = Convert.ToDecimal((double)ldec_CostAmortIni * (Math.Pow((uno + ((double)ldec_TIR / 100)), ((double)difDias / dias365)) - uno));
                                        //ldec_Interes = ldec_CostAmortIni * (ldec_TIR / 100);
                                    }

                                    ldec_Pago = -flujoEfectivo.Interes;
                                    ldec_CostAmortFin = (ldec_CostAmortIni + ldec_Interes + ldec_Pago) - ldec_ValorFacial;
                                    ldec_Devengado = ldec_Interes + ldec_Pago;
                                    difAsiento = ldec_CostAmortFin;

                                    if (ldec_CostAmortFin >= 0)
                                        ldec_Devengado = ldec_Devengado - ldec_CostAmortFin;
                                    else
                                        ldec_Devengado = ldec_Devengado + ldec_CostAmortFin;

                                    ldec_CostAmortFin = 0;

                                }
                            }

                            lcls_DevengoInteres.CrearDevengoInteres(lint_NroValor, lstr_Nemotecnico, Convert.ToDateTime(flujoEfectivo.Periodo), 1, ldec_CostAmortIni,
                                                                    ldec_Interes, ldec_Pago, ldec_CostAmortFin, ldec_Devengado, ldec_TIR, "ACT", "SG",
                                                                    out mens1, out mens2);

                            fechaAnterior = Convert.ToDateTime(flujoEfectivo.Periodo);
                        }
                        else
                        {
                            primerFlujo = flujoEfectivo.FlujoEfectivo;
                            fechaAnterior = fechaValor;
                        }
                        cont++;
                    }

                    Mantenimiento.clsDinamico dinamico = new Mantenimiento.clsDinamico();
                    dinamico.ConsultarDinamico("UPDATE R " +
                                               "SET R.DESCUENTODEVENGADO = R.DESCUENTODEVENGADO + ((R.INTERESES + R.PAGO) - R.DESCUENTODEVENGADO) " +
                                               "FROM [cf].[DevengosIntereses] R " +
                                               "INNER JOIN CF.TITULOSVALORES B ON R.NROVALOR = B.NROVALOR AND R.Nemotecnico = B.Nemotecnico " +
                                               "WHERE(R.INTERESES + R.PAGO) - R.DESCUENTODEVENGADO <> 0 " +
                                               "AND B.TIPO IN('Tasa Fija') AND B.INDICADORCUPON = 'V' ");

                    DevengoMensual(lint_NroValor, lstr_Nemotecnico, fechaValor, difAsiento);
                    clsContabilizarDevengoInt ContabilizarDevengo = new clsContabilizarDevengoInt();
                    //ContabilizarDevengo.DevengoIntereses(lint_NroValor.ToString(), lstr_Nemotecnico, DateTime.Now);

                    cont = 0;
                    difAsiento = 0;
                }

            }
            catch (Exception ex)
            {
                string direccion = System.Configuration.ConfigurationManager.AppSettings["DireccionConfigs"];
                direccion += "log.txt";
                if (!System.IO.File.Exists(direccion))
                    System.IO.File.Create(direccion).Dispose();

                System.IO.File.AppendAllText(direccion, string.Format("{0}{1}", ex.ToString() + " / Valor: " + lint_NroValor.ToString() + " Nemo: " + lstr_Nemotecnico + " / Fecha: " + DateTime.Now.ToString(), Environment.NewLine));
            }
        }

        public List<XTIR> FlujoEfectivoTF(DataRow _drTasaFija)
        {
            clsCalculoFlujoEfectivo lcls_CalculoFlujoEfectivo = new clsCalculoFlujoEfectivo();
            List<XTIR> lstXtir = new List<XTIR>();
            XTIR xTir;
            string mens1 = String.Empty;
            string mens2 = String.Empty;
            int nroValor = 0;
            string Nemotecnico = String.Empty;
            DateTime fchValor = new DateTime();
            DateTime fchVencimiento = new DateTime();
            decimal transNeto = 0;
            decimal valorFacial = 0;
            decimal transBruto = 0;
            decimal plazoValor = 0;
            decimal tasaBruta = 0;
            decimal tasa = 0; //TasaInteres
            decimal montoCuponCorrido = 0;
            decimal periodicidad;
            decimal flujoEfectivo = 0;
            bool esLargoPlazo = true;
            int contCupon = 0;
            FlujoUnCupon = 0;

            try
            {
                nroValor = Convert.ToInt32(_drTasaFija["NroValor"].ToString());
                Nemotecnico = _drTasaFija["Nemotecnico"].ToString();
                fchValor = Convert.ToDateTime(_drTasaFija["FchValor"].ToString()); //primer registro de la tabla
                fchVencimiento = Convert.ToDateTime(_drTasaFija["FchVencimiento"].ToString()); //ultimo registro de la tabla
                transNeto = Convert.ToDecimal(_drTasaFija["ValorTransadoNeto"].ToString());
                valorFacial = Convert.ToDecimal(_drTasaFija["ValorFacial"].ToString()); //ultimo valor negativo del flujo de efectivo
                transBruto = Convert.ToDecimal(_drTasaFija["ValorTransadoBruto"].ToString()); //primer valor del flujo de efectivo
                tasaBruta = Convert.ToDecimal(_drTasaFija["TasaBruta"].ToString());
                plazoValor = Convert.ToDecimal(_drTasaFija["PlazoValor"].ToString());
                montoCuponCorrido = transNeto - transBruto;

                List<Cupones> lstCupones = CuponesPorTitulo(nroValor.ToString(), Nemotecnico, out periodicidad);

                int indice_cupones_a_eliminar = -1;

                foreach (Cupones cupon in lstCupones)
                {
                    //Valida que el cupón esté en el periodo hábil la fecha de vencimiento debe ser mayor a la fecha de valor de título
                    if (cupon.FechaFin.CompareTo(fchValor) < 0)
                    {
                        indice_cupones_a_eliminar = lstCupones.IndexOf(cupon);
                    }
                    else
                    {
                        break;
                    }
                }
                if (indice_cupones_a_eliminar == 0)
                {
                    lstCupones.RemoveAt(0);
                }
                else if (indice_cupones_a_eliminar > 0)
                {
                    //cantidad de registros a eliminar es igual al índice + 1
                    indice_cupones_a_eliminar+=1;
                    //elimina el rango desde 0 (indice) hasta indice_cupones_a_eliminar (cantidad de registros) 
                    lstCupones.RemoveRange(0, indice_cupones_a_eliminar);
                }

                if (plazoValor <= 1)
                {
                    esLargoPlazo = false;
                    tasa = (tasaBruta / 12) / 100;
                }
                else
                    tasa = (tasaBruta / (12 / periodicidad) ) / 100;

                if (esLargoPlazo)
                {
                    xTir = new XTIR();
                    contCupon = 1;
                    xTir.Fecha = fchValor;
                    xTir.Monto = transBruto + montoCuponCorrido;
                    lstXtir.Add(xTir);

                    lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivo(nroValor, Nemotecnico, fchValor.ToString("dd/MM/yyyy"), tasa,
                                                                        0, (transBruto + montoCuponCorrido), "", "SG", out mens1, out mens2);

                    foreach (var cupon in lstCupones)
                    {
                        if (contCupon == lstCupones.Count())
                            flujoEfectivo = -(cupon.InteresBruto + valorFacial);
                        else
                            flujoEfectivo = -cupon.InteresBruto;

                        xTir = new XTIR();
                        xTir.Fecha = cupon.FechaFin;
                        xTir.Monto = flujoEfectivo;
                        lstXtir.Add(xTir);

                        if (lstCupones.Count == 1)
                            FlujoUnCupon += cupon.InteresBruto;

                        lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivo(nroValor, Nemotecnico, cupon.FechaFin.ToString("dd/MM/yyyy"), tasa,
                                                                            cupon.InteresBruto, flujoEfectivo, "", "SG", out mens1, out mens2);
                        contCupon++;
                    }
                }

                return lstXtir;

                //double[] vTir = new double[lstXtir.Count()];
                //int vect = 0;
                //foreach (XTIR ir in lstXtir)
                //{
                //    vTir[vect] = (double)ir.Monto;
                //    vect++;
                //}

                //return vTir;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      
        public double[] FlujoEfectivoTasaFija(DataRow ldrw_TasaFija)
        {
            double[] ldec_ArregloTIR;
            string mens1 = String.Empty;
            string mens2 = String.Empty;
            clsCalculoFlujoEfectivo lcls_CalculoFlujoEfectivo = new clsCalculoFlujoEfectivo();
            int lint_NroValor = 0;
            string lstr_Nemotecnico = String.Empty;
            DateTime ldt_FchValor = new DateTime();
            DateTime ldt_FchVencimiento = new DateTime();
            decimal ldec_TransNeto = 0;
            decimal ldec_ValorFacial = 0;
            decimal ldec_TransBruto = 0;
            decimal ldec_PrimaDesc = 0;
            decimal ldec_PlazoValor = 0;
            bool lbol_EsLargoPlazo = true;
            decimal ldec_Intereses = 0;

            decimal ldec_TasaBruta = 0;
            decimal ldec_TasaMensual = 0; //TasaInteres
            decimal montoCuponCorrido = 0;

            try
            {

                lint_NroValor = Convert.ToInt32(ldrw_TasaFija["NroValor"].ToString());
                lstr_Nemotecnico = ldrw_TasaFija["Nemotecnico"].ToString();
                ldt_FchValor = Convert.ToDateTime(ldrw_TasaFija["FchValor"].ToString()); //primer registro de la tabla
                ldt_FchVencimiento = Convert.ToDateTime(ldrw_TasaFija["FchVencimiento"].ToString()); //ultimo registro de la tabla
                ldec_TransNeto = Convert.ToDecimal(ldrw_TasaFija["ValorTransadoNeto"].ToString());
                ldec_ValorFacial = Convert.ToDecimal(ldrw_TasaFija["ValorFacial"].ToString()); //ultimo valor negativo del flujo de efectivo
                ldec_TransBruto = Convert.ToDecimal(ldrw_TasaFija["ValorTransadoBruto"].ToString()); //primer valor del flujo de efectivo
                ldec_TasaBruta = Convert.ToDecimal(ldrw_TasaFija["TasaBruta"].ToString());
                ldec_PlazoValor = Convert.ToDecimal(ldrw_TasaFija["PlazoValor"].ToString());

                montoCuponCorrido = ldec_TransNeto - ldec_TransBruto;

                ldec_PrimaDesc = ldec_ValorFacial - ldec_TransBruto;

                //averigua si el valor es a largo o corto plazo
                if (ldec_PlazoValor <= 1)
                {
                    lbol_EsLargoPlazo = false;
                    ldec_TasaMensual = (ldec_TasaBruta / 12) / 100;
                }
                else
                {
                    ldec_TasaMensual = (ldec_TasaBruta / 2) / 100;
                }

                List<PeriodosCupones> lst = FechasCupones(lint_NroValor.ToString(), lstr_Nemotecnico);

                if (lbol_EsLargoPlazo)
                {

                    PeriodosTitulo(ldt_FchValor, ldt_FchVencimiento, lst);

                    //
                   //PeriodosSemestrales(ldt_FchValor, ldt_FchVencimiento);
                    //
                    FchValor = ldt_FchValor;
                    //
                    gint_DiasCuponCorrido = (Periodicidad * 30) - Dias360(ldt_FchValor, gdt_FchUltimoPago);
                    lint_ContPeriodos = meses;

                    ldec_ArregloTIR = new double[lint_ContPeriodos + 1];
                    gdec_TasaFija = new decimal[lint_ContPeriodos + 1];
                    VFlujoEfectivo = new decimal[lint_ContPeriodos + 1];

                    lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivo(lint_NroValor, lstr_Nemotecnico, ldt_FchValor.ToString("dd/MM/yyyy"), ldec_TasaMensual,
                        0, ldec_TransBruto,"", "SG", out mens1, out mens2);

                    ldec_ArregloTIR[0] = Convert.ToDouble(ldec_TransBruto);
                    gdec_TasaFija[0] = ldec_TransBruto;
                    VFlujoEfectivo[0] = ldec_TransBruto;

                    //
                    ProxFecha = new DateTime[fechas.Length];

                    for (int i = 1; i < lint_ContPeriodos; i++)
                    {
                        if ((i == 1) && (gint_DiasCuponCorrido != 0))
                        {
                            if (montoCuponCorrido != 0)
                                ldec_Intereses = +(((ldec_ValorFacial * ldec_TasaMensual) / 180) * gint_DiasCuponCorrido) - montoCuponCorrido;
                            else
                                ldec_Intereses = +((ldec_ValorFacial * ldec_TasaMensual) / 180) * gint_DiasCuponCorrido;
                        }
                        else
                        {
                            if ((montoCuponCorrido != 0) && (i == 1))
                                ldec_Intereses = (ldec_ValorFacial * ldec_TasaMensual) - montoCuponCorrido;
                            else
                                ldec_Intereses = ldec_ValorFacial * ldec_TasaMensual;
                        }

                        ProxFecha[i - 1] = Convert.ToDateTime(fechas[i]);

                        lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivo(lint_NroValor, lstr_Nemotecnico, fechas[i], ldec_TasaMensual,
                            ldec_Intereses, -ldec_Intereses,"", "SG", out mens1, out mens2);
                        ldec_ArregloTIR[i] = Convert.ToDouble(-ldec_Intereses);
                        VFlujoEfectivo[i] = ldec_Intereses;
                        gdec_TasaFija[i] = -ldec_Intereses;
                    }

                    if (lst.Count > 0)
                    {
                        ProxFecha[fechas.Length - 2] = Convert.ToDateTime(ldt_FchVencimiento);
                        //ProxFecha[fechas.Length - 1] = Convert.ToDateTime(ldt_FchVencimiento.ToString("dd/MM/yyyy"), new CultureInfo("en-CA"));

                        decimal mesesDiferencia = MonthDifference(ldt_FchVencimiento, (fechas.Length == 2 ? ProxFecha[fechas.Length - 2] : ProxFecha[fechas.Length - 3]));

                        if (mesesDiferencia != 0)
                            ldec_Intereses = (ldec_Intereses / Periodicidad) * mesesDiferencia;
                        else
                        {
                            decimal diasDiferencia = CalcularDiasDeDiferencia(ldt_FchVencimiento, (fechas.Length == 2 ? ProxFecha[fechas.Length - 2] : ProxFecha[fechas.Length - 3]));
                            if (diasDiferencia != 0)
                                ldec_Intereses = ((ldec_Intereses / Periodicidad) / 30) * diasDiferencia;

                        }

                        lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivo(lint_NroValor, lstr_Nemotecnico, ldt_FchVencimiento.ToString("dd/MM/yyyy"), ldec_TasaMensual,
                            ldec_Intereses, (-ldec_Intereses - ldec_ValorFacial), "", "SG", out mens1, out mens2);
                        ldec_ArregloTIR[lint_ContPeriodos] = Convert.ToDouble(-ldec_Intereses) - Convert.ToDouble(ldec_ValorFacial);
                        gdec_TasaFija[lint_ContPeriodos] = (-ldec_Intereses);
                        //VFlujoEfectivo[lint_ContPeriodos - 1] = VFlujoEfectivo[lint_ContPeriodos - 1] * -1;
                        gint_DiasCuponCorrido = 0;
                        VFlujoEfectivo[lint_ContPeriodos] = ldec_Intereses;
                    }

                    return ldec_ArregloTIR;
                }
                else
                {
                    PeriodosMensuales(ldt_FchValor, ldt_FchVencimiento);
                    lint_ContPeriodos = meses;

                    ldec_ArregloTIR = new double[lint_ContPeriodos];
                    gdec_TasaFija = new decimal[lint_ContPeriodos];
                    VFlujoEfectivo = new decimal[lint_ContPeriodos];

                    lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivo(lint_NroValor, lstr_Nemotecnico, ldt_FchValor.ToString("dd/MM/yyyy"), ldec_TasaMensual,
                        0, ldec_TransBruto,"", "SG", out mens1, out mens2);
                    ldec_ArregloTIR[0] = Convert.ToDouble(ldec_TransBruto);
                    gdec_TasaFija[0] = ldec_TransBruto;
                    VFlujoEfectivo[0] = ldec_TransBruto;

                    ldec_Intereses = ldec_ValorFacial * ldec_TasaMensual;

                    for (int i = 1; i < lint_ContPeriodos - 1; i++)
                    {
                        lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivo(lint_NroValor, lstr_Nemotecnico, fechas[i], ldec_TasaMensual,
                            ldec_Intereses, -ldec_Intereses,"", "SG", out mens1, out mens2);
                        ldec_ArregloTIR[i] = Convert.ToDouble(-ldec_Intereses);
                        gdec_TasaFija[i] = -ldec_Intereses;
                        VFlujoEfectivo[i] = -ldec_Intereses;
                    }

                    lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivo(lint_NroValor, lstr_Nemotecnico, ldt_FchVencimiento.ToString("dd/MM/yyyy"), ldec_TasaMensual,
                        ldec_Intereses, (-ldec_Intereses - ldec_ValorFacial),"", "SG", out mens1, out mens2);
                    ldec_ArregloTIR[lint_ContPeriodos - 1] = Convert.ToDouble(-ldec_Intereses - ldec_ValorFacial);
                    gdec_TasaFija[lint_ContPeriodos - 1] = (-ldec_Intereses - ldec_ValorFacial);
                    VFlujoEfectivo[lint_ContPeriodos - 1] = VFlujoEfectivo[lint_ContPeriodos - 1] * -1;

                    gint_DiasCuponCorrido = 0;

                    return ldec_ArregloTIR;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return null;
            }
        }
        #endregion

        #region devengo tasa variable
        public void DevengoTasaVariable(int _nroValor, string _Nemotecnico, string exacto)
         {
            DataTable ldat_Valores = new DataTable();
            clsTituloValor lcls_TituloValor = new clsTituloValor();
            clsDevengoInteres lcls_DevengoInteres = new clsDevengoInteres();
            List<clsConsultaCalculoFlujoEfectivo> LstCalculoEfectivo = new List<clsConsultaCalculoFlujoEfectivo>();
            List<clsConsultaDevengoInteres> LstDevengoInteres = new List<clsConsultaDevengoInteres>();
            decimal ldec_TIR = 0;
            int lint_NroValor = 0;
            string lstr_Nemotecnico = String.Empty;
            decimal ldec_TransBruto = 0;
            decimal ldec_ValorFacial = 0;
            decimal ldec_CostAmortIni = 0;
            decimal ldec_Interes = 0;
            decimal ldec_CostAmortFin = 0;
            decimal ldec_Pago = 0;
            decimal ldec_PenultPago = 0;
            decimal ldec_Devengado = 0;
            int cont = 0;
            string mens1 = String.Empty;
            string mens2 = String.Empty;
            DateTime fechaValor;
            decimal primerFlujo = 0;
            decimal montoCuponCorrido = 0;
            decimal transNeto = 0;
            bool esTirNormal = true;
            DateTime fechaAnterior = DateTime.Now;
            decimal difDias = 0;
            double uno = 1;
            double dias365 = 365;
            decimal difAsiento = 0;

            try
            {
                if (exacto.Equals("S"))
                {
                   ldat_Valores = lcls_TituloValor.ConsultarTituloValor(_nroValor, _Nemotecnico, String.Empty, String.Empty, String.Empty, "Tasa Variable", String.Empty, "Vigente", Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/5000"), string.Empty).Tables[0];
                }
                else if (exacto.Equals("N"))
                {
                    ldat_Valores = lcls_TituloValor.ConsultarTituloValor(null, String.Empty, String.Empty, String.Empty, String.Empty, "Tasa Variable", String.Empty, "Vigente", Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/5000"), string.Empty).Tables[0];

                    if (_nroValor > 0)
                    {
                        var strExpr = " NroValor > " + _nroValor.ToString();
                        ldat_Valores = ldat_Valores.Select(strExpr).CopyToDataTable();
                    }
                }

                ldat_Valores = ajusteTitulosCompra(ldat_Valores);

                foreach(DataRow ldr_Valor in ldat_Valores.Rows)
                {
                    //ldec_TIR = CalculoTIR(FlujoEfectivoTV(ldat_Valores.Rows[i], out LstCalculoEfectivo));

                    List<XTIR> lstTir = FlujoEfectivoTV(ldr_Valor, out LstCalculoEfectivo);

                    lint_NroValor = Convert.ToInt32(ldr_Valor["NroValor"].ToString());
                    lstr_Nemotecnico = ldr_Valor["Nemotecnico"].ToString();
                    ldec_TransBruto = Convert.ToDecimal(ldr_Valor["ValorTransadoBruto"].ToString());
                    ldec_ValorFacial = Convert.ToDecimal(ldr_Valor["ValorFacial"].ToString());
                    fechaValor = Convert.ToDateTime(ldr_Valor["FchValor"]);
                    transNeto = Convert.ToDecimal(ldr_Valor["ValorTransadoNeto"].ToString());
                    montoCuponCorrido = transNeto - ldec_TransBruto;

                    if (montoCuponCorrido > 0 || !VerificaPeriodicidadCompleta(lstTir))
                    {
                        ldec_TIR = (decimal)CalculoXTIR(lstTir) * 100;
                        esTirNormal = false;
                    }
                    else
                    {
                        double[] vTir = new double[lstTir.Count()];
                        int vect = 0;
                        foreach (XTIR ir in lstTir)
                        {
                            vTir[vect] = (double)ir.Monto;
                            vect++;
                        }
                        ldec_TIR = CalculoTIR(vTir);
                        esTirNormal = true;
                    }

                    LstCalculoEfectivo = ConsultaFlujoEfectivo(lint_NroValor, lstr_Nemotecnico);

                    foreach (var flujoEfectivo in LstCalculoEfectivo)
                    {
                        if (cont != 0)
                        {
                            if (cont == 1)
                                ldec_CostAmortIni = ldec_TransBruto + montoCuponCorrido;
                            else
                                ldec_CostAmortIni = ldec_CostAmortFin;

                            if (cont < LstCalculoEfectivo.Count() - 1)
                            {
                                //ldec_Interes = ldec_CostAmortIni * (ldec_TIR / 100);
                                if (esTirNormal)
                                    ldec_Interes = ldec_CostAmortIni * (ldec_TIR / 100);
                                else
                                {
                                    difDias = CalcularDiasDeDiferencia(Convert.ToDateTime(flujoEfectivo.Periodo), fechaAnterior);
                                    ldec_Interes = Convert.ToDecimal((double)ldec_CostAmortIni * (Math.Pow((uno + ((double)ldec_TIR / 100)), ((double)difDias / dias365)) - uno));
                                }

                                ldec_Pago = flujoEfectivo.FlujoEfectivo;
                                ldec_CostAmortFin = ldec_CostAmortIni + ldec_Interes + ldec_Pago;
                                ldec_Devengado = ldec_Interes + ldec_Pago;
                            }
                            else
                            {
                                ldec_CostAmortIni = ldec_CostAmortFin;
                                //ldec_Interes = ldec_CostAmortIni * (ldec_TIR / 100);

                                if (esTirNormal)
                                    ldec_Interes = ldec_CostAmortIni * (ldec_TIR / 100);
                                else
                                {
                                    difDias = CalcularDiasDeDiferencia(Convert.ToDateTime(flujoEfectivo.Periodo), fechaAnterior);
                                    ldec_Interes = Convert.ToDecimal((double)ldec_CostAmortIni * (Math.Pow((uno + ((double)ldec_TIR / 100)), ((double)difDias / dias365)) - uno));
                                }

                                ldec_Pago = -flujoEfectivo.Interes;
                                ldec_CostAmortFin = (ldec_CostAmortIni + ldec_Interes + ldec_Pago) - ldec_ValorFacial;
                                ldec_Devengado = ldec_Interes + ldec_Pago;
                                difAsiento = ldec_CostAmortFin;

                                if (ldec_CostAmortFin >= 0)
                                    ldec_Devengado = ldec_Devengado - ldec_CostAmortFin;
                                else
                                    ldec_Devengado = ldec_Devengado + -(ldec_CostAmortFin);

                                ldec_CostAmortFin = 0;
                            }



                            lcls_DevengoInteres.CrearDevengoInteres(lint_NroValor, lstr_Nemotecnico, Convert.ToDateTime(flujoEfectivo.Periodo), flujoEfectivo.IdFlujoEfectivo, ldec_CostAmortIni,
                                                                    ldec_Interes, ldec_Pago, ldec_CostAmortFin, ldec_Devengado, ldec_TIR, "ACT", "SG",
                                                                    out mens1, out mens2);

                            fechaAnterior = Convert.ToDateTime(flujoEfectivo.Periodo);

                        }
                        else
                        {
                            primerFlujo = flujoEfectivo.FlujoEfectivo;
                            fechaAnterior = fechaValor;
                        }
                            
                        cont++;
                    }


                    Mantenimiento.clsDinamico dinamico = new Mantenimiento.clsDinamico();
                    dinamico.ConsultarDinamico("UPDATE R " +
                                               "SET R.DESCUENTODEVENGADO = R.DESCUENTODEVENGADO + ((R.INTERESES + R.PAGO) - R.DESCUENTODEVENGADO) " +
                                               "FROM [cf].[DevengosIntereses] R " +
                                               "INNER JOIN CF.TITULOSVALORES B ON R.NROVALOR = B.NROVALOR AND R.Nemotecnico = B.Nemotecnico " +
                                               "WHERE(R.INTERESES + R.PAGO) - R.DESCUENTODEVENGADO <> 0 " +
                                               "AND B.TIPO IN('Tasa Variable') AND B.INDICADORCUPON = 'V' ");

                    LstDevengoInteres = ConsultaDevengoInteres(lint_NroValor, lstr_Nemotecnico);
                    DevengoMensual(lint_NroValor, lstr_Nemotecnico, LstDevengoInteres, fechaValor, difAsiento);

                    difAsiento = 0;
                    cont = 0;
                }

            }
            catch (Exception ex)
            {
                string direccion = System.Configuration.ConfigurationManager.AppSettings["DireccionConfigs"];
                direccion += "log.txt";
                if (!System.IO.File.Exists(direccion))
                    System.IO.File.Create(direccion).Dispose();

                System.IO.File.AppendAllText(direccion, string.Format("{0}{1}", ex.ToString() + " / Valor: " + lint_NroValor.ToString() + " Nemo: " + lstr_Nemotecnico + " / Fecha: " + DateTime.Now.ToString(), Environment.NewLine));
            }
        }

        private void EliminarCalculos(int _nroValor, string _Nemotecnico, int _flujoEfectivo)
        {
            Mantenimiento.clsDinamico dinamico = new Mantenimiento.clsDinamico();
            int idDevengoInteres = 0;
            DataSet ds = dinamico.ConsultarDinamico("select top 1 iddevengoInteres from [cf].[DevengosIntereses] where nrovalor = " +
                                                            _nroValor.ToString() + " and Nemotecnico = '" +
                                                            _Nemotecnico + "' and IdFlujoefectivoFK = " + _flujoEfectivo.ToString() +
                                                            " order by iddevengoInteres asc");
            if (ds.Tables[0].Rows.Count != 0)
                idDevengoInteres = (int)ds.Tables[0].Rows[0]["iddevengoInteres"];

            dinamico.ConsultarDinamico("Delete [cf].[DevengosMensuales] where nrovalor = " +_nroValor.ToString() + " and Nemotecnico = '" +_Nemotecnico + "' and IdDevengoIntFK > " + idDevengoInteres.ToString());

            dinamico.ConsultarDinamico("Delete [cf].[DevengosIntereses] where nrovalor = " +_nroValor.ToString() + " and Nemotecnico = '" +_Nemotecnico + "' and IdFlujoefectivoFK > " + _flujoEfectivo.ToString());

            dinamico.ConsultarDinamico("Delete [cf].[CalculosFlujoEfectivo] where nrovalor = " +_nroValor.ToString() + " and Nemotecnico = '" +_Nemotecnico + "' and IdFlujoEfectivo > " + _flujoEfectivo.ToString());


        }

        private List<clsConsultaDevengoInteres> ConsultaDevengoInteres(int _lint_NroValor, string _lstr_Nemotecnico)
        {
            clsDevengoInteres lcls_CalculoDevengoInteres = new clsDevengoInteres();
            List<clsConsultaDevengoInteres> lstConsultaCalculo = new List<clsConsultaDevengoInteres>();
            clsConsultaDevengoInteres consultaCalculo;
            DataSet DsFlujos = lcls_CalculoDevengoInteres.ConsultarDevengoInteres(_lint_NroValor.ToString(), _lstr_Nemotecnico);

            foreach (DataRow row in DsFlujos.Tables[0].Rows)
            {
                consultaCalculo = new clsConsultaDevengoInteres();
                consultaCalculo.Lint_NumValor = _lint_NroValor.ToString();
                consultaCalculo.Lstr_Nemotecnico = _lstr_Nemotecnico;
                consultaCalculo.Anno1 = (DateTime)row["Anno"];
                consultaCalculo.IdDevengoInteres1 = (int)row["IdDevengoInteres"];
                consultaCalculo.IdFlujoEfectivoFK1 = (int)row["IdFlujoEfectivoFK"];
                consultaCalculo.CostoAmortizacionInicial1 = (decimal)row["CostoAmortizacionInicial"];
                consultaCalculo.Intereses1 = (decimal)row["Intereses"];
                consultaCalculo.Pago1 = (decimal)row["Pago"];
                consultaCalculo.CostoAmortizacionFinal1 = (decimal)row["CostoAmortizacionFinal"];
                consultaCalculo.DescuentoDevengado1 = (decimal)row["DescuentoDevengado"];
                consultaCalculo.Tir1 = (decimal)row["TIR"];
                lstConsultaCalculo.Add(consultaCalculo);
            }

            return lstConsultaCalculo;

         }

        private List<clsConsultaDevengoInteresCanje> ConsultaDevengoInteresCanje(int _lint_NroValor, string _lstr_Nemotecnico)
        {
            clsDevengoInteres lcls_CalculoDevengoInteres = new clsDevengoInteres();
            List<clsConsultaDevengoInteresCanje> lstConsultaCalculo = new List<clsConsultaDevengoInteresCanje>();
            clsConsultaDevengoInteresCanje consultaCalculo;
            DataSet DsFlujos = lcls_CalculoDevengoInteres.ConsultarDevengoInteresCanje(_lint_NroValor.ToString(), _lstr_Nemotecnico);

            foreach (DataRow row in DsFlujos.Tables[0].Rows)
            {
                consultaCalculo = new clsConsultaDevengoInteresCanje();
                consultaCalculo.Lint_NumValor = _lint_NroValor.ToString();
                consultaCalculo.Lstr_Nemotecnico = _lstr_Nemotecnico;
                consultaCalculo.Anno1 = (DateTime)row["Anno"];
                consultaCalculo.IdDevengoInteres1 = (int)row["IdDevengoInteres"];
                consultaCalculo.IdFlujoEfectivoFK1 = (int)row["IdFlujoEfectivoFK"];
                consultaCalculo.CostoAmortizacionInicial1 = (decimal)row["CostoAmortizacionInicial"];
                consultaCalculo.Intereses1 = (decimal)row["Intereses"];
                consultaCalculo.Pago1 = (decimal)row["Pago"];
                consultaCalculo.CostoAmortizacionFinal1 = (decimal)row["CostoAmortizacionFinal"];
                consultaCalculo.DescuentoDevengado1 = (decimal)row["DescuentoDevengado"];
                consultaCalculo.Tir1 = (decimal)row["TIR"];
                lstConsultaCalculo.Add(consultaCalculo);
            }

            return lstConsultaCalculo;

        }

        private List<clsConsultaCalculoFlujoEfectivo> ConsultaFlujoEfectivo(int _lint_NroValor, string _lstr_Nemotecnico)
        {
            try
            {
                clsCalculoFlujoEfectivo lcls_CalculoFlujoEfectivo = new clsCalculoFlujoEfectivo();
                List<clsConsultaCalculoFlujoEfectivo> lstConsultaCalculo = new List<clsConsultaCalculoFlujoEfectivo>();
                clsConsultaCalculoFlujoEfectivo consultaCalculo;
                DataSet DsFlujos = lcls_CalculoFlujoEfectivo.ConsultarCalculoFlujoEfectivo(_lint_NroValor.ToString(), _lstr_Nemotecnico);

                foreach (DataRow row in DsFlujos.Tables[0].Rows)
                {
                    consultaCalculo = new clsConsultaCalculoFlujoEfectivo();
                    consultaCalculo.Lint_NumValor = _lint_NroValor.ToString();
                    consultaCalculo.Lstr_Nemotecnico = _lstr_Nemotecnico;
                    consultaCalculo.Periodo = row["Periodo"].ToString();
                    consultaCalculo.IdFlujoEfectivo = (int)row["IdFlujoEfectivo"];
                    consultaCalculo.TasaInteres = (decimal)row["TasaInteres"];
                    consultaCalculo.Interes = (decimal)row["Intereses"];
                    consultaCalculo.FlujoEfectivo = (decimal)row["FlujoEfectivo"];
                    consultaCalculo.NroAsiento = row["NroAsiento"].Equals(null) ? "0" : row["NroAsiento"].ToString();
                    lstConsultaCalculo.Add(consultaCalculo);
                }
                return lstConsultaCalculo.OrderBy(a => a.IdFlujoEfectivo).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            } 
        }

        public List<XTIR> FlujoEfectivoTV(DataRow ldrw_TasaVariable, out List<clsConsultaCalculoFlujoEfectivo> _LstCalculoEfectivo)
        {
            clsCalculoFlujoEfectivo lcls_CalculoFlujoEfectivo = new clsCalculoFlujoEfectivo();
            List<clsConsultaCalculoFlujoEfectivo> lstConsultaCalculo = new List<clsConsultaCalculoFlujoEfectivo>();
            List<XTIR> lstXtir = new List<XTIR>();
            XTIR xTir;
            string mens1 = String.Empty;
            string mens2 = String.Empty;
            int nroValor = 0;
            string Nemotecnico = String.Empty;
            DateTime fchValor = new DateTime();
            DateTime fchVencimiento = new DateTime();
            decimal transNeto = 0;
            decimal valorFacial = 0;
            decimal transBruto = 0;
            decimal plazoValor = 0;
            decimal tasaBruta = 0;
            string tasa = ""; //TasaInteres
            decimal montoCuponCorrido = 0;
            decimal tasaVariable = 0;
            decimal margen = 0;
            decimal periodicidad;
            decimal flujoEfectivo = 0;
            bool esLargoPlazo = true;
            int contCupon = 0;
            DateTime fechaTasa;
            decimal primaDesc = 0;
            decimal tasaCalculo = 0;
            decimal tasaMensual = 0;
            string asiento = "0";
            decimal flujoReal = 0;
            string tipoTasa = string.Empty;
            DateTime fechaAnteriorCupon = DateTime.Now;

            clsConsultaCalculoFlujoEfectivo primerCalculoEfectivo = null;
            clsConsultaCalculoFlujoEfectivo UltimoCalculoEfectivo = null;

            try
            {
                nroValor = Convert.ToInt32(ldrw_TasaVariable["NroValor"].ToString());
                Nemotecnico = ldrw_TasaVariable["Nemotecnico"].ToString();
                fchValor = Convert.ToDateTime(ldrw_TasaVariable["FchValor"].ToString()); //primer registro de la tabla
                fchVencimiento = Convert.ToDateTime(ldrw_TasaVariable["FchVencimiento"].ToString()); //ultimo registro de la tabla
                transNeto = Convert.ToDecimal(ldrw_TasaVariable["ValorTransadoNeto"].ToString());
                valorFacial = Convert.ToDecimal(ldrw_TasaVariable["ValorFacial"].ToString()); //ultimo valor negativo del flujo de efectivo
                transBruto = Convert.ToDecimal(ldrw_TasaVariable["ValorTransadoBruto"].ToString()); //primer valor del flujo de efectivo
                tasaBruta = Convert.ToDecimal(ldrw_TasaVariable["TasaBruta"].ToString());
                tipoTasa = ldrw_TasaVariable["TasaNeta"].ToString();
                tasaVariable = Convert.ToDecimal(string.IsNullOrEmpty(tipoTasa) ? "0,0" : tipoTasa);
                margen = Convert.ToDecimal(ldrw_TasaVariable["Margen"].ToString());
                tasa = ldrw_TasaVariable["TasaVariable"].ToString().Trim();
                plazoValor = Convert.ToDecimal(ldrw_TasaVariable["PlazoValor"].ToString());
                montoCuponCorrido = transNeto - transBruto;

                lstConsultaCalculo = ConsultaFlujoEfectivo(nroValor, Nemotecnico);
                List<Cupones> lstCupones = CuponesPorTitulo(nroValor.ToString(), Nemotecnico, out periodicidad);
                int indice_cupones_a_eliminar = -1;
                foreach (Cupones cupon in lstCupones)
                {
                    //Valida que el cupón esté en el periodo hábil la fecha de vencimiento debe ser mayor a la fecha de valor de título
                    if (cupon.FechaFin.CompareTo(fchValor) < 0)
                    {
                        indice_cupones_a_eliminar = lstCupones.IndexOf(cupon);
                    }
                    else
                    {
                        break;
                    }
                }
                if (indice_cupones_a_eliminar == 0)
                {
                    lstCupones.RemoveAt(0);
                }
                else if (indice_cupones_a_eliminar > 0)
                {
                    //cantidad de registros a eliminar es igual al índice + 1
                    indice_cupones_a_eliminar += 1;
                    //elimina el rango desde 0 (indice) hasta indice_cupones_a_eliminar (cantidad de registros) 
                    lstCupones.RemoveRange(0, indice_cupones_a_eliminar);
                }
                

                if (lstConsultaCalculo.Count != 0)
                {
                    primerCalculoEfectivo = (from A in lstConsultaCalculo
                                             where A.NroAsiento == "0"
                                             orderby A.IdFlujoEfectivo ascending
                                             select A).FirstOrDefault();

                    UltimoCalculoEfectivo = (from A in lstConsultaCalculo
                                             where A.NroAsiento != "0"
                                             orderby A.IdFlujoEfectivo descending
                                             select A).FirstOrDefault();

                    if (UltimoCalculoEfectivo == null)
                        UltimoCalculoEfectivo = primerCalculoEfectivo;

                }

                if (UltimoCalculoEfectivo != null)
                {
                    fechaTasa = Convert.ToDateTime(UltimoCalculoEfectivo.Periodo);
                    EliminarCalculos((int)nroValor, Nemotecnico.Trim(), UltimoCalculoEfectivo.IdFlujoEfectivo);
                }
                else
                    fechaTasa = fchValor;

                if (!string.IsNullOrEmpty(tasa))
                {
                    Mantenimiento.clsValoresIndicadoresEco lcls_ValoresIndicadoresEco = new Mantenimiento.clsValoresIndicadoresEco();
                    tasaVariable = (decimal)lcls_ValoresIndicadoresEco.ConsultarValoresIndicadoresEco(tasa, fechaTasa).Tables[0].Rows[0]["Valor"];
                }
                    
                tasaCalculo = tasaVariable + margen;
                primaDesc = valorFacial - transBruto;

                if (plazoValor <= 1)
                {
                    esLargoPlazo = false;
                    tasaMensual = (tasaCalculo / 12) / 100;
                }
                else
                    tasaMensual = (tasaCalculo / (12 / periodicidad)) / 100;


                if (esLargoPlazo)
                {
                    xTir = new XTIR();
                    contCupon = 1;
                    xTir.Fecha = fchValor;
                    xTir.Monto = transBruto + montoCuponCorrido;
                    lstXtir.Add(xTir);

                    if (primerCalculoEfectivo == null)
                        lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivo(nroValor, Nemotecnico, fchValor.ToString("dd/MM/yyyy"), tasaMensual,
                                                                            0, (transBruto + montoCuponCorrido), asiento, "SG", out mens1, out mens2);

                    foreach (var cupon in lstCupones)
                    {
                        if (contCupon == lstCupones.Count())
                        {
                            if (cupon.InteresBruto == 0)
                            {
                                if (lstCupones.Count() > 1)
                                {
                                    decimal dia360 = Dias360(cupon.FechaFin, fechaAnteriorCupon);
                                    decimal diasPeriodo = periodicidad * 30;

                                    if (dia360 != diasPeriodo)
                                    {
                                        decimal diaDiferencia = CalcularDiasDeDiferencia(cupon.FechaFin, fechaAnteriorCupon);
                                        cupon.InteresBruto = (valorFacial) * tasaMensual * (dia360 / diasPeriodo);

                                    }
                                    else
                                        cupon.InteresBruto = (valorFacial) * tasaMensual;
                                }
                                else
                                    cupon.InteresBruto = (valorFacial) * tasaMensual;

                                //TODO: Cambio de devengo ultimo cupon

                            }
                                
                            flujoEfectivo = -(cupon.InteresBruto + valorFacial);  
                        }  
                        else
                            flujoEfectivo = -cupon.InteresBruto;

                        flujoReal = flujoEfectivo;

                        if (cupon.TasaBruta != 0)
                            tasaMensual = (cupon.TasaBruta / (12 / periodicidad)) / 100;


                        if (flujoEfectivo == 0)
                        {
                            //tasaMensual = (tasaMensual / (12 / periodicidad)) / 100;
                            flujoEfectivo = -(valorFacial * tasaMensual);
                            cupon.InteresBruto = -(flujoEfectivo);
                        }

                        xTir = new XTIR();
                        xTir.Fecha = cupon.FechaFin;
                        xTir.Monto = flujoEfectivo;
                        lstXtir.Add(xTir);

                        lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivo(nroValor, Nemotecnico, cupon.FechaFin.ToString("dd/MM/yyyy"), tasaMensual,
                                                                            cupon.InteresBruto, flujoEfectivo, asiento, "SG", out mens1, out mens2);

                        if (flujoReal != 0)
                            tasaMensual = (cupon.TasaBruta / (12 / periodicidad)) / 100;

                        contCupon++;
                        fechaAnteriorCupon = cupon.FechaFin;
                    }

                }

                _LstCalculoEfectivo = ConsultaFlujoEfectivo(nroValor, Nemotecnico);
                return lstXtir;


                //    double[] vTir = new double[lstXtir.Count()];
                //int vect = 0;
                //foreach (XTIR ir in lstXtir)
                //{
                //    vTir[vect] = (double)ir.Monto;
                //    vect++;
                //}

                //return vTir;

            }
            catch (Exception ex)
            {
                _LstCalculoEfectivo = null;
                throw ex;
            }
        }


        public double[] FlujoEfectivoTasaVariable(DataRow ldrw_TasaVariable, out List<clsConsultaCalculoFlujoEfectivo> _LstCalculoEfectivo)
        {
            //int lint_ContPeriodos = 0;
            double[] ldec_ArregloTIR;
            string mens1 = String.Empty;
            string mens2 = String.Empty;
            clsCalculoFlujoEfectivo lcls_CalculoFlujoEfectivo = new clsCalculoFlujoEfectivo();
            List<clsConsultaCalculoFlujoEfectivo> lstConsultaCalculo = new List<clsConsultaCalculoFlujoEfectivo>();
            clsConsultaCalculoFlujoEfectivo primerCalculoEfectivo = null;
            clsConsultaCalculoFlujoEfectivo UltimoCalculoEfectivo = null;
            int lint_NroValor = 0;
            string lstr_Nemotecnico = String.Empty;
            DateTime ldt_FchValor = new DateTime();
            DateTime ldt_FchVencimiento = new DateTime();
            decimal ldec_TransNeto = 0;
            decimal ldec_ValorFacial = 0;
            decimal ldec_TransBruto = 0;
            decimal ldec_PrimaDesc = 0;
            decimal ldec_PlazoValor = 0;
            bool lbol_EsLargoPlazo = true;
            string tasaTitulo = "";
            decimal ldec_Intereses = 0;
            DateTime fechaTasa;
            

            //string[] lstr_TasaVariable;
            decimal ldec_TasaCalculo = 0;
            string asiento = "0";
            decimal ldec_TasaVariable = 0;
            decimal ldec_Margen = 0;

            decimal ldec_TasaMensual = 0; //TasaInteres 
            decimal montoCuponCorrido = 0;

            try
            {

                lint_NroValor = Convert.ToInt32(ldrw_TasaVariable["NroValor"].ToString());
                lstr_Nemotecnico = ldrw_TasaVariable["Nemotecnico"].ToString();
                ldt_FchValor = Convert.ToDateTime(ldrw_TasaVariable["FchValor"].ToString()); //primer registro de la tabla
                ldt_FchVencimiento = Convert.ToDateTime(ldrw_TasaVariable["FchVencimiento"].ToString()); //ultimo registro de la tabla
                ldec_TransNeto = Convert.ToDecimal(ldrw_TasaVariable["ValorTransadoNeto"].ToString());
                ldec_ValorFacial = Convert.ToDecimal(ldrw_TasaVariable["ValorFacial"].ToString()); //ultimo valor negativo del flujo de efectivo
                ldec_TransBruto = Convert.ToDecimal(ldrw_TasaVariable["ValorTransadoBruto"].ToString()); //primer valor del flujo de efectivo
                ldec_TasaVariable = Convert.ToDecimal(ldrw_TasaVariable["TasaVariableValor"].ToString().Equals("") ? "0.0" : ldrw_TasaVariable["TasaVariableValor"].ToString());
                ldec_Margen = Convert.ToDecimal(ldrw_TasaVariable["Margen"].ToString());
                ldec_PlazoValor = Convert.ToDecimal(ldrw_TasaVariable["PlazoValor"].ToString());
                tasaTitulo = ldrw_TasaVariable["TasaVariable"].ToString().Trim();
                montoCuponCorrido = ldec_TransNeto - ldec_TransBruto;


                lstConsultaCalculo = ConsultaFlujoEfectivo(lint_NroValor, lstr_Nemotecnico);

                if (lstConsultaCalculo.Count != 0)
                {
                    primerCalculoEfectivo = (from A in lstConsultaCalculo
                                              where A.NroAsiento == "0"
                                              orderby A.IdFlujoEfectivo ascending
                                              select A).FirstOrDefault();   

                    UltimoCalculoEfectivo = (from A in lstConsultaCalculo
                                             where A.NroAsiento != "0"
                                             orderby A.IdFlujoEfectivo descending
                                             select A).FirstOrDefault();

                    if (UltimoCalculoEfectivo == null)
                        UltimoCalculoEfectivo = primerCalculoEfectivo;

                }

                if (UltimoCalculoEfectivo != null)
                { 
                    fechaTasa = Convert.ToDateTime(UltimoCalculoEfectivo.Periodo);
                    EliminarCalculos((int)lint_NroValor, lstr_Nemotecnico.Trim(), UltimoCalculoEfectivo.IdFlujoEfectivo);
                }
                else
                    fechaTasa = ldt_FchValor;


                Mantenimiento.clsValoresIndicadoresEco lcls_ValoresIndicadoresEco = new Mantenimiento.clsValoresIndicadoresEco();
                ldec_TasaVariable = (decimal)lcls_ValoresIndicadoresEco.ConsultarValoresIndicadoresEco(tasaTitulo,fechaTasa).Tables[0].Rows[0]["Valor"];
                    
                ldec_TasaCalculo = ldec_TasaVariable + ldec_Margen;

                ldec_PrimaDesc = ldec_ValorFacial - ldec_TransBruto;
                //averigua si el valor es a largo o corto plazo
                if (ldec_PlazoValor <= 1)
                {
                    lbol_EsLargoPlazo = false;
                    ldec_TasaMensual = (ldec_TasaCalculo / 12) / 100;
                }
                else
                {
                    ldec_TasaMensual = (ldec_TasaCalculo / 2) / 100;
                }

                List<PeriodosCupones> lst = FechasCupones(lint_NroValor.ToString(), lstr_Nemotecnico);

                if (lbol_EsLargoPlazo)
                {
                    PeriodosTitulo(ldt_FchValor, ldt_FchVencimiento, lst);

                    //PeriodosSemestrales(ldt_FchValor, ldt_FchVencimiento);
                    //
                    FchValor = ldt_FchValor;
                    //
                    //gint_DiasCuponCorrido = 180 - Dias360(ldt_FchValor, gdt_FchUltimoPago);
                    gint_DiasCuponCorrido = (Periodicidad * 30) - Dias360(ldt_FchValor, gdt_FchUltimoPago);
                    lint_ContPeriodos = meses;

                    ldec_ArregloTIR = new double[lint_ContPeriodos + 1];
                    gdec_TasaVariable = new decimal[lint_ContPeriodos + 1];
                    VFlujoEfectivo = new decimal[lint_ContPeriodos + 1];

                    if (primerCalculoEfectivo == null)
                    {
                        lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivo(lint_NroValor, lstr_Nemotecnico, ldt_FchValor.ToString("dd/MM/yyyy"), ldec_TasaMensual,
                                                                            0, ldec_TransBruto, asiento, "SG", out mens1, out mens2);
                    }

                    ldec_ArregloTIR[0] = Convert.ToDouble(ldec_TransBruto);
                    gdec_TasaVariable[0] = ldec_TransBruto;
                    VFlujoEfectivo[0] = ldec_TransBruto;

                    //
                    ProxFecha = new DateTime[lint_ContPeriodos];

                    for (int i = 1; i < lint_ContPeriodos; i++)
                    {
                        if ((i == 1) && (gint_DiasCuponCorrido != 0))
                        {
                            if (montoCuponCorrido != 0)
                                ldec_Intereses = +(((ldec_ValorFacial * ldec_TasaMensual) / 180) * gint_DiasCuponCorrido) - montoCuponCorrido;
                            else
                                ldec_Intereses = +((ldec_ValorFacial * ldec_TasaMensual) / 180) * gint_DiasCuponCorrido;
                            //
                            //ProxFecha = Convert.ToDateTime(fechas[i]);
                            //ProxFecha[i-1] = Convert.ToDateTime(fechas[i]);
                            //
                        }
                        else
                        {
                            if ((montoCuponCorrido != 0) && (i == 1))
                                ldec_Intereses = (ldec_ValorFacial * ldec_TasaMensual) - montoCuponCorrido;
                            else
                                ldec_Intereses = (ldec_ValorFacial * ldec_TasaMensual);



                            //
                        }

                        ProxFecha[i - 1] = Convert.ToDateTime(fechas[i]);

                        lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivo(lint_NroValor, lstr_Nemotecnico, fechas[i], ldec_TasaMensual,
                            ldec_Intereses, -ldec_Intereses, asiento, "SG", out mens1, out mens2);
                        ldec_ArregloTIR[i] = Convert.ToDouble(-ldec_Intereses);
                        VFlujoEfectivo[i] = ldec_Intereses;
                        gdec_TasaVariable[i] = -ldec_Intereses;
                    }

                    if (lst.Count > 0)
                    {
                        if (lst.Count == 2)
                            ldec_Intereses = (ldec_ValorFacial * ldec_TasaMensual);

                        //if (lint_ContPeriodos ==1)
                        //    ProxFecha[lint_ContPeriodos - 1] = Convert.ToDateTime(ldt_FchVencimiento.ToString("dd/MM/yyyy"));
                        //else
                        ProxFecha[lint_ContPeriodos - 1] = Convert.ToDateTime(ldt_FchVencimiento.ToString("dd/MM/yyyy"));
                        //ProxFecha[lint_ContPeriodos - 1] = Convert.ToDateTime(ldt_FchVencimiento.ToString("dd/MM/yyyy"));

                        decimal mesesDiferencia = MonthDifference(ldt_FchVencimiento, (fechas.Length == 2 ? ProxFecha[fechas.Length - 2] : ProxFecha[fechas.Length - 2]));

                        if (mesesDiferencia != 0)
                            ldec_Intereses = (ldec_Intereses / Periodicidad) * mesesDiferencia;
                        else
                        {
                            decimal diasDiferencia = CalcularDiasDeDiferencia(ldt_FchVencimiento, (fechas.Length == 2 ? ProxFecha[fechas.Length - 2] : ProxFecha[fechas.Length - 2]));
                            if (diasDiferencia != 0)
                                ldec_Intereses = ((ldec_Intereses / Periodicidad) / 30) * diasDiferencia;
                        }


                        lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivo(lint_NroValor, lstr_Nemotecnico, ldt_FchVencimiento.ToString("dd/MM/yyyy"), ldec_TasaMensual,
                            ldec_Intereses, (-ldec_Intereses - ldec_ValorFacial), asiento, "SG", out mens1, out mens2);

                        ldec_ArregloTIR[lint_ContPeriodos] = Convert.ToDouble(-ldec_Intereses) - Convert.ToDouble(ldec_ValorFacial);
                        gdec_TasaVariable[lint_ContPeriodos] = (-ldec_Intereses);
                        VFlujoEfectivo[lint_ContPeriodos] = ldec_Intereses;
                        gint_DiasCuponCorrido = 0;

                        //ldec_ArregloTIR[lint_ContPeriodos - 1] = Convert.ToDouble(-ldec_Intereses - ldec_ValorFacial);
                        //gdec_TasaVariable[lint_ContPeriodos - 1] = (-ldec_Intereses - ldec_ValorFacial);
                        //VFlujoEfectivo[lint_ContPeriodos - 1] = VFlujoEfectivo[lint_ContPeriodos - 1] * -1;

                    }

                    _LstCalculoEfectivo = ConsultaFlujoEfectivo(lint_NroValor, lstr_Nemotecnico);
                    return ldec_ArregloTIR;
                }
                else
                {
                    PeriodosMensuales(ldt_FchValor, ldt_FchVencimiento);
                    lint_ContPeriodos = meses;

                    ldec_ArregloTIR = new double[lint_ContPeriodos];
                    gdec_TasaVariable = new decimal[lint_ContPeriodos];
                    VFlujoEfectivo = new decimal[lint_ContPeriodos];

                    lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivo(lint_NroValor, lstr_Nemotecnico, ldt_FchValor.ToString("dd/MM/yyyy"), ldec_TasaMensual,
                        0, ldec_TransBruto,asiento, "SG", out mens1, out mens2);
                    ldec_ArregloTIR[0] = Convert.ToDouble(ldec_TransBruto);
                    gdec_TasaVariable[0] = ldec_TransBruto;

                    ldec_Intereses = ldec_ValorFacial * ldec_TasaMensual;

                    for (int i = 1; i < lint_ContPeriodos - 1; i++)
                    {
                        lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivo(lint_NroValor, lstr_Nemotecnico, fechas[i], ldec_TasaMensual,
                            ldec_Intereses, -ldec_Intereses,"", "SG", out mens1, out mens2);
                        ldec_ArregloTIR[i] = Convert.ToDouble(-ldec_Intereses);
                        gdec_TasaVariable[i] = -ldec_Intereses;
                        VFlujoEfectivo[i] = -ldec_Intereses;
                    }

                    lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivo(lint_NroValor, lstr_Nemotecnico, ldt_FchVencimiento.ToString("dd/MM/yyyy"), ldec_TasaMensual,
                        ldec_Intereses, (-ldec_Intereses - ldec_ValorFacial), asiento, "SG", out mens1, out mens2);
                    ldec_ArregloTIR[lint_ContPeriodos - 1] = Convert.ToDouble(-ldec_Intereses - ldec_ValorFacial);
                    gdec_TasaVariable[lint_ContPeriodos - 1] = (-ldec_Intereses - ldec_ValorFacial);
                    VFlujoEfectivo[lint_ContPeriodos - 1] = VFlujoEfectivo[lint_ContPeriodos - 1] * -1;

                    gint_DiasCuponCorrido = 0;

                    _LstCalculoEfectivo = ConsultaFlujoEfectivo(lint_NroValor, lstr_Nemotecnico);
                    return ldec_ArregloTIR;
                }
            }
            catch (Exception ex)
            {
                _LstCalculoEfectivo = null;
                ex.ToString();
                return null;
            }
        }

        public string[] PorcentajeNemotecnico(string lstr_IdNemotecnico)
        {
            try
            {
                string[] lstr_TasasVariables = new string[2];
                Mantenimiento.clsNemotecnicos lcls_Nemotecnicos = new Mantenimiento.clsNemotecnicos();
                Mantenimiento.clsValoresIndicadoresEco lcls_ValoresIndicadoresEco = new Mantenimiento.clsValoresIndicadoresEco();
                //Mantenimiento.clsIndicadoresEconomicos lcls_IndicadoresEconomicos = new Mantenimiento.clsIndicadoresEconomicos();
                string lstr_IndicadorEco = String.Empty;
                lstr_TasasVariables[0] = lcls_Nemotecnicos.ConsultarNemotecnicos(lstr_IdNemotecnico, null, null, null, null).Tables[0].Rows[0]["IdTasa"].ToString();
                lstr_IndicadorEco = lstr_TasasVariables[0];
                lstr_TasasVariables[1] = lcls_ValoresIndicadoresEco.ConsultarValoresIndicadoresEco(lstr_IndicadorEco, DateTime.Today.Date).Tables[0].Rows[0]["Valor"].ToString();
                //lstr_TasasVariables[1] = lcls_IndicadoresEconomicos.ConsultarIndicadoresEconomicos(lstr_IndicadorEco, null, null).Tables[0].Rows[0]["Transaccion"].ToString();
                return lstr_TasasVariables;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return null;
            }
        }
        #endregion

        #region devengo subasta y canje

        private Decimal CapitalFchSubasta = 0;
        private Decimal ImpDevengarFchSubasta = 0;
        private Decimal CuponCorridoFchSubasta = 0;
        private Decimal ValorEmision = 0;
        private Decimal PorcentajeEmision = 0;
        private Decimal ImpDevenarTranscurrido = 0;
        private Decimal CuponCorridoTranscurrido = 0;
        private Decimal CapitalDeBaja = 0;
        private Decimal ImpDevengarDeBaja = 0;
        private Decimal CuponCorridoDeBaja = 0;
        private Decimal ValorEmisionDeBaja = 0;
        private Decimal EntradaSalidaCaja = 0;
        private Decimal TotalColocado = 0;
        private Decimal NetoSubastado = 0;
        private Decimal TotalNetoBaja = 0;
        private Decimal Diferencia = 0;
        private Decimal Capital = 0;
        private Decimal IntDevengado = 0;
        private Decimal ImpRenta = 0;
        private Decimal Descuento = 0;

        private Decimal InteresFlujo = 0;
        private Decimal InteresDevengo = 0;
        private Decimal CostoAmortFinal = 0;
        private Decimal DescDevengado = 0;
        private Decimal Columna4 = 0;
        private DateTime PenultimaFecha = new DateTime();
        private DateTime FechaUltimoCupon = new DateTime();
        private int DiasPeriodo = 0;
        private Decimal ValorFacialUltimoCupon = 0;
        private Decimal RelacionSubasta = 0;
        private Decimal TransadoNetoUltimoCupon = 0;
        private Decimal Vencimiento = 0;
        private string NroEmision = String.Empty;

        public void CargaVariablesSubastaCanje(string NroEmision, Decimal Vencimiento, string lstr_SistemaNegociacionCompra)
        {
            clsSubastaCanje lcls_Subasta = new clsSubastaCanje();
            try
            {
                InteresFlujo = Convert.ToDecimal(lcls_Subasta.ConsultarInteresFlujo(NroEmision).Tables[0].Rows[0]["Intereses"].ToString());
                InteresDevengo = Convert.ToDecimal(lcls_Subasta.ConsultarInteresDevengo(NroEmision).Tables[0].Rows[0]["Intereses"].ToString());
                CostoAmortFinal = Convert.ToDecimal(lcls_Subasta.ConsultarCostoAmortizacionFinal(NroEmision).Tables[0].Rows[0]["CostoAmortizacionFinal"].ToString());
                DescDevengado = Convert.ToDecimal(lcls_Subasta.ConsultarInteresFlujo(NroEmision).Tables[0].Rows[0]["DescuentoDevengado"].ToString());
                Columna4 = Convert.ToDecimal(lcls_Subasta.ConsultarColumnaDevengoMensualNroSerie(NroEmision).Tables[0].Rows[0]["Columna4"].ToString());
                PenultimaFecha = Convert.ToDateTime(lcls_Subasta.ConsultarPenultimaFechaDevengo(NroEmision).Tables[0].Rows[0]["FchInicio"].ToString());
                FechaUltimoCupon = Convert.ToDateTime(lcls_Subasta.ConsultarNroEmisionCompra(NroEmision).Tables[0].Rows[0]["FchFin"].ToString());
                DiasPeriodo = Dias360(FechaUltimoCupon, PenultimaFecha);

                ValorFacialUltimoCupon = Convert.ToDecimal(lcls_Subasta.ConsultarNroEmisionCompra(NroEmision, lstr_SistemaNegociacionCompra).Tables[0].Rows[0]["ValorFacial"].ToString());
                RelacionSubasta = ValorFacialUltimoCupon / Vencimiento;
                TransadoNetoUltimoCupon = Convert.ToDecimal(lcls_Subasta.ConsultarNroEmisionCompra(NroEmision, lstr_SistemaNegociacionCompra).Tables[0].Rows[0]["ValorTransadoNeto"].ToString());
                //this.Vencimiento = Vencimiento;
                //this.NroEmision = NroEmision;
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public void DevengoSubastaCanje(string lstr_SistemaNegociacionCompra, string lstr_SistemaNegociacionVenta)
        {
            DataTable ldat_Valores = new DataTable();
            clsSubastaCanje lcls_SubastaInversa = new clsSubastaCanje();
            clsDevengoInteres lcls_DevengoInteres = new clsDevengoInteres();
            decimal ldec_TIR = 0;
            string lstr_NroEmision = String.Empty;
            decimal ldec_TransBruto = 0;
            decimal ldec_ValorFacial = 0;
            decimal ldec_CostAmortIni = 0;
            decimal ldec_Interes = 0;
            decimal ldec_CostAmortFin = 0;
            decimal ldec_Pago = 0;
            decimal ldec_PenultPago = 0;
            decimal ldec_Devengado = 0;
            int cont = 0;
            string mens1 = String.Empty;
            string mens2 = String.Empty;

            try
            {

                //ldat_Valores = lcls_TituloValor.ConsultarTituloValor(null, null, "TasaFija", null, null).Tables[0];
                ldat_Valores = lcls_SubastaInversa.ConsultarNroEmisionVenta(lstr_SistemaNegociacionVenta).Tables[0];

                for (int i = 0; i < ldat_Valores.Rows.Count; i++)
                {
                    ldec_TIR = CalculoTIR(FlujoEfectivoSubastaCanje(ldat_Valores.Rows[i]));

                    lstr_NroEmision = ldat_Valores.Rows[i]["NroEmisionSerie"].ToString();
                    ldec_TransBruto = Convert.ToDecimal(ldat_Valores.Rows[i]["ValorTransadoBruto"].ToString());
                    ldec_ValorFacial = Convert.ToDecimal(ldat_Valores.Rows[i]["ValorFacial"].ToString());

                    //carga las variables de subasta
                    CargaVariablesSubastaCanje(lstr_NroEmision, ldec_TransBruto, lstr_SistemaNegociacionCompra);

                    for (int j = 1; j < lint_ContPeriodos - 1; j++)
                    {
                        if (j == 1)
                        {
                            ldec_CostAmortIni = ldec_TransBruto;
                            ldec_Interes = ldec_CostAmortIni * (ldec_TIR / 100);
                            ldec_Pago = gdec_TasaFija[j];
                            //
                            IntPrimerDevengo = ldec_Interes;
                            PagoPrimerDevengo = ldec_Pago;
                            //
                            ldec_CostAmortFin = ldec_CostAmortIni + ldec_Interes + ldec_Pago;
                            ldec_Devengado = ldec_Interes + ldec_Pago;

                            lcls_DevengoInteres.CrearDevengoInteresNroSerie(lstr_NroEmision, Convert.ToDateTime(fechas[j]), ldec_CostAmortIni,
                                ldec_Interes, ldec_Pago, ldec_CostAmortFin, ldec_Devengado, "ACT", "SG", out mens1, out mens2);
                        }
                        else
                        {
                            ldec_CostAmortIni = ldec_CostAmortFin;
                            ldec_Interes = ldec_CostAmortIni * (ldec_TIR / 100);
                            ldec_Pago = gdec_TasaFija[j];
                            ldec_CostAmortFin = ldec_CostAmortIni + ldec_Interes + ldec_Pago;
                            ldec_Devengado = ldec_Interes + ldec_Pago;

                            lcls_DevengoInteres.CrearDevengoInteresNroSerie(lstr_NroEmision, Convert.ToDateTime(fechas[j]), ldec_CostAmortIni,
                                ldec_Interes, ldec_Pago, ldec_CostAmortFin, ldec_Devengado, "ACT", "SG", out mens1, out mens2);
                        }
                        cont = j;
                        ldec_PenultPago = gdec_TasaFija[j];
                    }
                    ldec_CostAmortIni = ldec_CostAmortFin;
                    ldec_Interes = ldec_CostAmortIni * (ldec_TIR / 100);
                    ldec_Pago = gdec_TasaFija[cont + 1];
                    ldec_CostAmortFin = ldec_CostAmortIni + ldec_Interes + ldec_Pago;
                    ldec_Devengado = ldec_Interes + ldec_PenultPago;

                    lcls_DevengoInteres.CrearDevengoInteresNroSerie(lstr_NroEmision, Convert.ToDateTime(fechas[cont + 1]), ldec_CostAmortIni,
                        ldec_Interes, ldec_Pago, ldec_CostAmortFin, ldec_Devengado, "ACT", "SG", out mens1, out mens2);
                    ldec_Pago = 0;
                    ldec_Interes = 0;
                    DevengoMensualNroSerie(NroEmision, PenultimaFecha, FechaUltimoCupon);
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public double[] FlujoEfectivoSubastaCanje(DataRow ldrw_TasaFija)
        {

            double[] ldec_ArregloTIR;
            string mens1 = String.Empty;
            string mens2 = String.Empty;
            clsCalculoFlujoEfectivo lcls_CalculoFlujoEfectivo = new clsCalculoFlujoEfectivo();
            string lstr_NroEmision = String.Empty;
            DateTime ldt_FchValor = new DateTime();
            DateTime ldt_FchVencimiento = new DateTime();
            decimal ldec_TransNeto = 0;
            decimal ldec_ValorFacial = 0;
            decimal ldec_TransBruto = 0;
            decimal ldec_PrimaDesc = 0;
            decimal ldec_PlazoValor = 0;
            bool lbol_EsLargoPlazo = true;
            decimal ldec_Intereses = 0;

            decimal ldec_TasaBruta = 0;
            decimal ldec_TasaMensual = 0; //TasaInteres

            try
            {
                lstr_NroEmision = ldrw_TasaFija["NroEmisionSerie"].ToString();
                ldt_FchValor = Convert.ToDateTime(ldrw_TasaFija["FchValor"].ToString()); //primer registro de la tabla
                ldt_FchVencimiento = Convert.ToDateTime(ldrw_TasaFija["FchVencimiento"].ToString()); //ultimo registro de la tabla
                ldec_TransNeto = Convert.ToDecimal(ldrw_TasaFija["ValorTransadoNeto"].ToString());
                ldec_ValorFacial = Convert.ToDecimal(ldrw_TasaFija["ValorFacial"].ToString()); //ultimo valor negativo del flujo de efectivo
                ldec_TransBruto = Convert.ToDecimal(ldrw_TasaFija["ValorTransadoBruto"].ToString()); //primer valor del flujo de efectivo
                ldec_TasaBruta = Convert.ToDecimal(ldrw_TasaFija["TasaBruta"].ToString());
                ldec_PlazoValor = Convert.ToDecimal(ldrw_TasaFija["PlazoValor"].ToString());
                ldec_PrimaDesc = ldec_ValorFacial - ldec_TransBruto;

                //averigua si el valor es a largo o corto plazo
                if (ldec_PlazoValor <= 365)
                {
                    lbol_EsLargoPlazo = false;
                    ldec_TasaMensual = (ldec_TasaBruta / 100) / 12;
                }
                else
                {
                    ldec_TasaMensual = (ldec_TasaBruta / 100) / 2;
                }

                if (lbol_EsLargoPlazo)
                {
                    PeriodosSemestrales(ldt_FchValor, ldt_FchVencimiento);
                    //
                    FchValor = ldt_FchValor;
                    //
                    gint_DiasCuponCorrido = 180 - Dias360(ldt_FchValor, gdt_FchUltimoPago);
                    lint_ContPeriodos = meses + 1;

                    ldec_ArregloTIR = new double[lint_ContPeriodos];
                    gdec_TasaFija = new decimal[lint_ContPeriodos];

                    //lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivoNroSerie(lstr_NroEmision, ldt_FchValor.ToString("dd/MM/yyyy"), ldec_TasaMensual,
                    //    0, ldec_TransBruto, "SG", out mens1, out mens2);

                    lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivoNroSerie(lstr_NroEmision, "01/01/1900", ldec_TasaMensual,
                        0, ldec_TransBruto, "SG", out mens1, out mens2);

                    lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivoNroSerie(lstr_NroEmision, ldt_FchValor.ToString("dd/MM/yyyy"), ldec_TasaMensual,
                            ldec_Intereses, -ldec_Intereses, "SG", out mens1, out mens2);

                    ldec_ArregloTIR[0] = Convert.ToDouble(ldec_TransBruto);
                    gdec_TasaFija[0] = ldec_TransBruto;

                    //
                    ProxFecha = new DateTime[fechas.Length];

                    for (int i = 1; i < lint_ContPeriodos - 1; i++)
                    {
                        if (i == 1)
                        {
                            ldec_Intereses = +((ldec_ValorFacial * ldec_TasaMensual) / 180) * gint_DiasCuponCorrido;
                            //
                            //ProxFecha = Convert.ToDateTime(fechas[i]);
                            //ProxFecha[i] = Convert.ToDateTime(fechas[i]);
                            //
                            ldec_ArregloTIR[i] = Convert.ToDouble(-ldec_Intereses);
                            gdec_TasaFija[i] = -ldec_Intereses;
                        }
                        else
                        {
                            ldec_Intereses = ldec_ValorFacial * ldec_TasaMensual;
                            //
                            //
                        }

                        ProxFecha[i - 1] = Convert.ToDateTime(fechas[i]);

                        lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivoNroSerie(lstr_NroEmision, fechas[i], ldec_TasaMensual,
                            ldec_Intereses, -ldec_Intereses, "SG", out mens1, out mens2);
                        ldec_ArregloTIR[i] = Convert.ToDouble(-ldec_Intereses);
                        gdec_TasaFija[i] = -ldec_Intereses;
                    }

                    ProxFecha[fechas.Length - 1] = Convert.ToDateTime(ldt_FchVencimiento.ToString("dd/MM/yyyy"));

                    lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivoNroSerie(lstr_NroEmision, ldt_FchVencimiento.ToString("dd/MM/yyyy"), ldec_TasaMensual,
                        ldec_Intereses, (-ldec_Intereses - ldec_ValorFacial), "SG", out mens1, out mens2);
                    ldec_ArregloTIR[lint_ContPeriodos - 1] = Convert.ToDouble(-ldec_Intereses - ldec_ValorFacial);
                    gdec_TasaFija[lint_ContPeriodos - 1] = (-ldec_Intereses - ldec_ValorFacial);

                    gint_DiasCuponCorrido = 0;

                    return ldec_ArregloTIR;
                }
                else
                {
                    PeriodosMensuales(ldt_FchValor, ldt_FchVencimiento);
                    lint_ContPeriodos = meses;

                    ldec_ArregloTIR = new double[lint_ContPeriodos];
                    gdec_TasaFija = new decimal[lint_ContPeriodos];

                    //lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivoNroSerie(lstr_NroEmision, ldt_FchValor.ToString("dd/MM/yyyy"), ldec_TasaMensual,
                    //    0, ldec_TransBruto, "SG", out mens1, out mens2);
                    lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivoNroSerie(lstr_NroEmision, "01/01/1900", ldec_TasaMensual,
                        0, ldec_TransBruto, "SG", out mens1, out mens2);

                    lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivoNroSerie(lstr_NroEmision, ldt_FchValor.ToString("dd/MM/yyyy"), ldec_TasaMensual,
                            ldec_Intereses, -ldec_Intereses, "SG", out mens1, out mens2);

                    ldec_ArregloTIR[0] = Convert.ToDouble(ldec_TransBruto);
                    gdec_TasaFija[0] = ldec_TransBruto;

                    ldec_Intereses = ldec_ValorFacial * ldec_TasaMensual;

                    for (int i = 1; i < lint_ContPeriodos - 1; i++)
                    {
                        if (i == 1)
                        {
                            ldec_ArregloTIR[i] = Convert.ToDouble(-ldec_Intereses);
                            gdec_TasaFija[i] = -ldec_Intereses;
                        }
                        lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivoNroSerie(lstr_NroEmision, fechas[i], ldec_TasaMensual,
                            ldec_Intereses, -ldec_Intereses, "SG", out mens1, out mens2);
                        ldec_ArregloTIR[i] = Convert.ToDouble(-ldec_Intereses);
                        gdec_TasaFija[i] = -ldec_Intereses;
                    }

                    lcls_CalculoFlujoEfectivo.CrearCalculoFlujoEfectivoNroSerie(lstr_NroEmision, ldt_FchVencimiento.ToString("dd/MM/yyyy"), ldec_TasaMensual,
                        ldec_Intereses, (-ldec_Intereses - ldec_ValorFacial), "SG", out mens1, out mens2);
                    ldec_ArregloTIR[lint_ContPeriodos - 1] = Convert.ToDouble(-ldec_Intereses - ldec_ValorFacial);
                    gdec_TasaFija[lint_ContPeriodos - 1] = (-ldec_Intereses - ldec_ValorFacial);

                    gint_DiasCuponCorrido = 0;

                    return ldec_ArregloTIR;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return null;
            }
        }

        public void DevengoMensualNroSerie(string lstr_NroEmision, DateTime FchInicio, DateTime FchFin)
        {
            int TotalDiasDif = 0;
            int DiasDif = 0;

            decimal Columna1 = 0;
            decimal Columna2 = 0;
            decimal Columna3 = 0;
            decimal Columna4 = 0;

            string mens1 = String.Empty;
            string mens2 = String.Empty;

            clsDevengoMensual lcls_DevengoMensual = new clsDevengoMensual();
            clsSubastaCanje lcls_Subasta = new clsSubastaCanje();

            FchTemp = PeriodosMensualesDevengo(FchInicio, FchFin);//contiene las fechas en el periodo indicado
            TotalDiasDif = +(Dias360(FchFin, FchInicio));

            try
            {
                for (int i = 0; i < mesesDev; i++)
                {
                    if (i == 0)
                    {
                        DateTime FchTemporal = Convert.ToDateTime(FchTemp[i]);
                        DiasDif = +(Dias360(FchTemporal, FchInicio));
                        if (DiasDif > 30)
                        {
                            DiasDif = 30;
                        }

                        Columna1 = CostoAmortFinal;
                        Columna2 = (InteresDevengo / 180) * DiasDif;
                        Columna3 = (InteresFlujo / 180) * DiasDif;
                        Columna4 = +Columna1 + Columna2 - Columna3;
                    }
                    else
                    {
                        DateTime FchTemporal1 = Convert.ToDateTime(FchTemp[i]);
                        DateTime FchTemporal2 = Convert.ToDateTime(FchTemp[i - 1]);
                        DiasDif = +(Dias360(FchTemporal1, FchTemporal2));
                        if (DiasDif > 30)
                        {
                            DiasDif = 30;
                        }

                        Columna1 = Columna4;
                        Columna2 = (InteresDevengo / 180) * DiasDif;
                        Columna3 = (InteresFlujo / 180) * DiasDif;
                        Columna4 = +Columna1 + Columna2 - Columna3;
                    }

                    if (DiasDif > 0)
                    {
                        lcls_DevengoMensual.CrearDevengoMensualNroSerie(lstr_NroEmision, FchTemp[i], DiasDif, Columna1, Columna2, Columna3,
                            Columna4, "SG", out mens1, out mens2);
                    }
                }

                CapitalFchSubasta = Columna4;
                ImpDevengarFchSubasta = (DescDevengado / 180) * TotalDiasDif;
                CuponCorridoFchSubasta = (InteresFlujo / 180) * TotalDiasDif;
                ValorEmision = +CapitalFchSubasta + ImpDevengarFchSubasta - CuponCorridoFchSubasta;
                PorcentajeEmision = ValorEmision / RelacionSubasta;
                ImpDevenarTranscurrido = ImpDevengarFchSubasta * RelacionSubasta;
                CuponCorridoTranscurrido = CuponCorridoFchSubasta * RelacionSubasta;
                CapitalDeBaja = PorcentajeEmision;
                ImpDevengarDeBaja = (DescDevengado * RelacionSubasta) - ImpDevenarTranscurrido;
                CuponCorridoDeBaja = CuponCorridoTranscurrido;
                ValorEmisionDeBaja = CapitalDeBaja - ImpDevengarDeBaja + CuponCorridoDeBaja;
                EntradaSalidaCaja = TransadoNetoUltimoCupon;
                TotalColocado = Capital + IntDevengado + ImpRenta + Descuento;
                NetoSubastado = +TotalColocado - EntradaSalidaCaja;
                TotalNetoBaja = ValorEmisionDeBaja;
                Diferencia = EntradaSalidaCaja - ValorEmisionDeBaja;

                lcls_Subasta.CrearEmisionSubasta(NroEmision, CapitalFchSubasta, ImpDevengarFchSubasta, CuponCorridoFchSubasta, ValorEmision,
                    PorcentajeEmision, ImpDevenarTranscurrido, CuponCorridoTranscurrido, CapitalDeBaja, ImpDevengarDeBaja,
                    CuponCorridoDeBaja, ValorEmisionDeBaja, EntradaSalidaCaja, NetoSubastado, TotalNetoBaja, Diferencia, Capital,
                    InteresDevengo, ImpRenta, Descuento, TotalColocado, "SG", out mens1, out mens2);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public DataTable RegistroContable()
        {
            DataTable ldat_Asiento = new DataTable();

            try
            {
                ldat_Asiento.Columns.Add("Referencia");
                ldat_Asiento.Columns.Add("Fecha");
                ldat_Asiento.Columns.Add("Cuenta");
                ldat_Asiento.Columns.Add("ClaveContable");
                ldat_Asiento.Columns.Add("Moneda");
                ldat_Asiento.Columns.Add("TextoInfo");
                ldat_Asiento.Columns.Add("CentroCosto");
                ldat_Asiento.Columns.Add("CentroBeneficio");
                ldat_Asiento.Columns.Add("ElementoPEP");
                ldat_Asiento.Columns.Add("PosPre");
                ldat_Asiento.Columns.Add("CentroGestor");
                ldat_Asiento.Columns.Add("Fondo");
                ldat_Asiento.Columns.Add("DocPres");
                ldat_Asiento.Columns.Add("PosDocPres");
                ldat_Asiento.Columns.Add("Monto");
                ldat_Asiento.Columns.Add("PKMovimiento");
                ldat_Asiento.Columns.Add("Texto2");
                ldat_Asiento.Columns.Add("Ref1Tipo");
                ldat_Asiento.Columns.Add("Ref2Operacion");
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return ldat_Asiento;
        }

        public void ContabilizarDevengoCeroCupon(
            string lstr_Tipo,
            string lstr_EstadoValor,
            DateTime ldt_FchValor, //periodo
            //DateTime ldt_FchVencimiento, //
            string lstr_Propietario,
            string lstr_Plazo,
            decimal ldec_ValorTransadoBruto,

            //string lstr_Moneda,
            string lstr_NroValor,
            string lstr_Nemotecnico,
            decimal ldec_Intereses,
            string lstr_Detalle)
        {

            wsDeudaInterna.wsDeudaInterna wsDeudaInterna = new wsDeudaInterna.wsDeudaInterna();
            DataTable ldat_Asiento = RegistroContable();
            DataTable ldat_Tira = new DataTable();
            int lint_EsPublico = 1;
            string lstr_Operacion = string.Empty;
            string lstr_NomOperacion = string.Empty;
            string lstr_Moneda = wsDeudaInterna.ConsultarTitulosValores(lstr_NroValor, lstr_Nemotecnico, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "01/01/1900", "01/01/5000").Tables[0].Rows[0]["Moneda"].ToString().Equals("CRCN") ? "CRC" : wsDeudaInterna.ConsultarTitulosValores(lstr_NroValor, lstr_Nemotecnico, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "01/01/1900", "01/01/5000").Tables[0].Rows[0]["Moneda"].ToString();
            decimal ldec_monto = 0;

            Mantenimiento.clsPropietarios lcls_Propietarios = new Mantenimiento.clsPropietarios();
            Mantenimiento.clsTiposAsiento lcls_TiposAsiento = new Mantenimiento.clsTiposAsiento();

            if (lcls_Propietarios.ConsultarPropietarios(string.Empty, string.Empty, string.Empty, lstr_Propietario, "S").Tables[0].Rows.Count == 0)
            {
                lint_EsPublico = 2;
            }

            //se tratan las colocaciones de las tres diferentes operaciones, títulos cero cupón, de tasa fija y tasa variable.
            #region Dev Mens
            //Define si no trasciende en el periodo
            if (lstr_Moneda == "CRC")
            {
                lstr_Operacion = "ID70";
            }
            else
            {
                lstr_Operacion = "ID72";
            }


            DataSet lds_Operaciones = loperacion.ConsultarOperaciones(lstr_Operacion, "IdModulo IN ('DI')", "");
            if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
            {
                lstr_NomOperacion = lds_Operaciones.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
            }

            ldat_Tira = lcls_TiposAsiento.ConsultarTiposAsiento("G206", "IdModulo IN ('DI')", lstr_Operacion, "", "", lstr_Moneda, lstr_Nemotecnico, "", "ID").Tables[0].Select("EsNemotecnico='5' AND EsPubPriv='" + lint_EsPublico + "'").CopyToDataTable();

            foreach (DataRow ldr_Row in ldat_Tira.Rows)
            {
                int index = ldat_Tira.Rows.IndexOf(ldr_Row);

                ldec_monto = ldec_Intereses;

                ldat_Asiento.Rows.Add(
                    lstr_NroValor + " " + lstr_Nemotecnico,
                    ldt_FchValor.ToString("dd.MM.yyyy"),
                    ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                    ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                    ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                    lstr_Detalle.Trim(),
                    ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                    ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                    ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                    ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                    ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                    ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                    ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                    ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                    Math.Round(ldec_monto, 2),
                    lstr_NroValor+"."+lstr_Nemotecnico,//pk
                    tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                    lstr_Moneda,//tipo
                    lstr_Operacion + "." + lstr_NomOperacion //operacion
                    );
            }
            #endregion
            GenerarAsientoAjuste(ldat_Asiento, lstr_Operacion, lstr_NroValor, lstr_Nemotecnico);
        }

        public string GenerarAsientoAjuste(DataTable ldat_Asiento, string lstr_IdOperacion, string lstr_NroValor, string lstr_Nemotecnico)
        {
            //Coleccion de asientos y tipos de asientos requeridos en SAP expuestos por la referencia del servicio
            wrSigafAsientos.ZfiAsiento item_asiento = new wrSigafAsientos.ZfiAsiento();
            wrSigafAsientos.ZfiAsiento[] tabla_asientos = new wrSigafAsientos.ZfiAsiento[ldat_Asiento.Rows.Count];

            //variables de proceso
            string[] item_resAsientosLog = new string[10];
            string logAsiento = string.Empty;
            string flagEstadoAsiento = string.Empty;
            Seguridad.tBitacora lcls_Bitacora = new Seguridad.tBitacora();

            int cont = 0;

            //string lstr_Moneda = dbMoneda.SelectedValue;
            //string lstr_Referencia = txtReferencia.Text;

            try
            {
                foreach (DataRow ldr_Row in ldat_Asiento.Rows)
                {
                    item_asiento = new wrSigafAsientos.ZfiAsiento();
                    int index = ldat_Asiento.Rows.IndexOf(ldr_Row);

                    if (index == 0)
                    {
                        item_asiento.Blart = "ID";//Clase de documento
                        item_asiento.Bukrs = "G206";//Sociedad
                        //item_asiento.Werks = ldat_Asiento.Rows[0]["Referencia"].ToString();
                        item_asiento.Bldat = ldat_Asiento.Rows[ldat_Asiento.Rows.IndexOf(ldr_Row)]["Fecha"].ToString();//Fecha de documento
                        item_asiento.Budat = ldat_Asiento.Rows[ldat_Asiento.Rows.IndexOf(ldr_Row)]["Fecha"].ToString();//Fecha de contabilización
                    }

                    item_asiento.Waers = ldat_Asiento.Rows[index]["Moneda"].ToString();//Moneda 
                    item_asiento.Bschl = ldat_Asiento.Rows[index]["ClaveContable"].ToString();//Clave de contabilización
                    item_asiento.Hkont = ldat_Asiento.Rows[index]["Cuenta"].ToString();//Cuenta de mayor
                    item_asiento.Wrbtr = Convert.ToDecimal(Convert.ToDecimal(ldat_Asiento.Rows[index]["Monto"].ToString()).ToString("0.0000"));//Importe
                    item_asiento.Sgtxt = ldat_Asiento.Rows[index]["TextoInfo"].ToString();//Texto Informativo (50 caracteres)
                    item_asiento.Kostl = ldat_Asiento.Rows[index]["CentroCosto"].ToString();//Centro de Costo
                    item_asiento.Prctr = ldat_Asiento.Rows[index]["CentroBeneficio"].ToString();//Centro de Beneficio
                    item_asiento.Projk = ldat_Asiento.Rows[index]["ElementoPEP"].ToString();//Elemento PEP
                    //item_asiento.Xref2Hd = ldat_Asiento.Rows[index]["PosPre"].ToString();//Posición Presupuestaria
                    //ggarcia
                    item_asiento.Fipex = ldat_Asiento.Rows[index]["PosPre"].ToString();//Posición Presupuestaria
                    item_asiento.Fistl = ldat_Asiento.Rows[index]["CentroGestor"].ToString();//Centro Gestor
                    item_asiento.Geber = ldat_Asiento.Rows[index]["Fondo"].ToString();//Fondo
                    item_asiento.Kblnr = ldat_Asiento.Rows[index]["DocPres"].ToString();//Documento Presupuestario
                    item_asiento.Kblpos = ldat_Asiento.Rows[index]["PosDocPres"].ToString();//Posición de documento presupuestario

                    item_asiento.Xblnr =  ldat_Asiento.Rows[index]["PKMovimiento"].ToString();//
                    item_asiento.Bktxt  = ldat_Asiento.Rows[index]["Texto2"].ToString();//
                    item_asiento.Xref1Hd = ldat_Asiento.Rows[index]["Ref1Tipo"].ToString();//
                    item_asiento.Xref2Hd = ldat_Asiento.Rows[index]["Ref2Operacion"].ToString();//

                    tabla_asientos[index] = item_asiento;
                }

                //Cargar de Asientos 
                string[] concatenado = new string[8];
                //envio de asiento mediante servicio web hacia SIGAF
                item_resAsientosLog = tasientos.EnviarAsientos(tabla_asientos,"");
                for (int j = 0; j < item_resAsientosLog.Length; j++)
                {
                    int x = j + 1;
                    logAsiento += x + " - " + item_resAsientosLog[j] + " - ";
                }
                //Registrar en Bitacora de movimientos

                lcls_Bitacora.ufnRegistrarAccionBitacora("DI", "usr", "Contabilizacion Devengo Mensual", "Resultado de Contabilización: " + logAsiento, lstr_IdOperacion, lstr_NroValor + "-" + lstr_Nemotecnico);
                return logAsiento;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public string GenerarAsientoCanje(DataTable ldat_Asiento, string lstr_IdOperacion, string lstr_NroValor, string lstr_Nemotecnico, DateTime lstr_FchCanje, Decimal TipoCambio)
        {
            //Coleccion de asientos y tipos de asientos requeridos en SAP expuestos por la referencia del servicio
            wrSigafAsientos.ZfiAsiento item_asiento = new wrSigafAsientos.ZfiAsiento();
            wrSigafAsientos.ZfiAsiento[] tabla_asientos = new wrSigafAsientos.ZfiAsiento[ldat_Asiento.Rows.Count];
            clsTituloValor lcls_TituloValor = new clsTituloValor();
            clsDinamico consulta = new clsDinamico();
            //variables de proceso
            string[] item_resAsientosLog = new string[10];
            string logAsiento = string.Empty;
            string flagEstadoAsiento = string.Empty;
            decimal ldec_Total40 = 0;
            decimal ldec_Total50 = 0;
            decimal ldec_Diferencia40y50 = 0;
            //string lstr_Nemotecnico = "";
            int lint_NroValor = 0;
            int index = 0;
            Seguridad.tBitacora lcls_Bitacora = new Seguridad.tBitacora();
            var strExpr = "";


        
            DataSet ldas_Comprados = new DataSet();
            DataSet ldas_Colocados = new DataSet();
            DataSet ldas_Emision = new DataSet();
            DataTable ldat_Comprados = new DataTable();
            DataTable ldat_Colocados = new DataTable();
            DataTable ldat_Emision = new DataTable();


             //string lstr_Moneda = dbMoneda.SelectedValue;
            //string lstr_Referencia = txtReferencia.Text;

            try
            {
                foreach (DataRow ldr_Row in ldat_Asiento.Rows)
                {
                    item_asiento = new wrSigafAsientos.ZfiAsiento();
                     index = ldat_Asiento.Rows.IndexOf(ldr_Row);

                    if (index == 0)
                    {
                        item_asiento.Blart = "ID";//Clase de documento
                        item_asiento.Bukrs = "G206";//Sociedad
                        //item_asiento.Werks = ldat_Asiento.Rows[0]["Referencia"].ToString();
                        item_asiento.Bldat = ldat_Asiento.Rows[ldat_Asiento.Rows.IndexOf(ldr_Row)]["Fecha"].ToString();//Fecha de documento
                        item_asiento.Budat = ldat_Asiento.Rows[ldat_Asiento.Rows.IndexOf(ldr_Row)]["Fecha"].ToString();//Fecha de contabilización
                        item_asiento.Bktxt = "Canje";//Referencia
                    }

                    item_asiento.Waers = ldat_Asiento.Rows[index]["Moneda"].ToString();//Moneda 
                    item_asiento.Bschl = ldat_Asiento.Rows[index]["ClaveContable"].ToString();//Clave de contabilización
                    item_asiento.Hkont = ldat_Asiento.Rows[index]["Cuenta"].ToString();//Cuenta de mayor
                    item_asiento.Wrbtr = Convert.ToDecimal(Convert.ToDecimal(ldat_Asiento.Rows[index]["Monto"].ToString()).ToString("0.0000"));//Importe
                    item_asiento.Sgtxt = ldat_Asiento.Rows[index]["TextoInfo"].ToString();//Texto Informativo (50 caracteres)
                    item_asiento.Kostl = ldat_Asiento.Rows[index]["CentroCosto"].ToString();//Centro de Costo
                    item_asiento.Prctr = ldat_Asiento.Rows[index]["CentroBeneficio"].ToString();//Centro de Beneficio
                    item_asiento.Projk = ldat_Asiento.Rows[index]["ElementoPEP"].ToString();//Elemento PEP
                    item_asiento.Fipex = ldat_Asiento.Rows[index]["PosPre"].ToString();//Posición Presupuestaria
                    item_asiento.Fistl = ldat_Asiento.Rows[index]["CentroGestor"].ToString();//Centro Gestor
                    item_asiento.Geber = ldat_Asiento.Rows[index]["Fondo"].ToString();//Fondo
                    item_asiento.Kblnr = ldat_Asiento.Rows[index]["DocPres"].ToString();//Documento Presupuestario
                    item_asiento.Kblpos = ldat_Asiento.Rows[index]["PosDocPres"].ToString();//Posición de documento presupuestario


                    item_asiento.Xblnr = ldat_Asiento.Rows[index]["PKMovimiento"].ToString();//
                    item_asiento.Bktxt = ldat_Asiento.Rows[index]["Texto2"].ToString();//
                    item_asiento.Xref1Hd = ldat_Asiento.Rows[index]["Ref1Tipo"].ToString();//
                    item_asiento.Xref2Hd = ldat_Asiento.Rows[index]["Ref2Operacion"].ToString();//

                    if (ldat_Asiento.Rows[index]["Moneda"].ToString() == "USD")
                        item_asiento.Kursf = Convert.ToDecimal(TipoCambio.ToString("0.0000"));
                    
                    if (item_asiento.Bschl == "40")
                        ldec_Total40 += item_asiento.Wrbtr;
                    else
                        ldec_Total50 += item_asiento.Wrbtr;

                    tabla_asientos[index] = item_asiento;
                }


                ldec_Diferencia40y50 = ldec_Total40 - ldec_Total50;

                Boolean lbl_cuadrado = false;
                wrSigafAsientos.ZfiAsiento[] tabla_asientos2 = new wrSigafAsientos.ZfiAsiento[index + 1];
                for (int y = 0; y < index + 1; y++)
                {
                    tabla_asientos2[y] = tabla_asientos[y];

                    if (!lbl_cuadrado && ldec_Diferencia40y50 != 0)
                    {
                        if (ldec_Diferencia40y50 > 0 && ldec_Diferencia40y50 < 1 && tabla_asientos2[y].Bschl == "50")
                        {//es mayor el 40 a los 50, subirle la diferencia al 50
                            tabla_asientos2[y].Wrbtr += ldec_Diferencia40y50;
                            lbl_cuadrado = true;
                        }
                        else
                        {//es mayor el 40 a los 50, subirle la diferencia al 50
                            if (ldec_Diferencia40y50 < 0 && ldec_Diferencia40y50 > -1 && tabla_asientos2[y].Bschl == "40")
                            {//es mayor el 50 a los 40, subirle la diferencia al 40

                                tabla_asientos2[y].Wrbtr += Math.Abs(ldec_Diferencia40y50);
                                lbl_cuadrado = true;
                            }
                        }
                    }
                }//for int y


                //Cargar de Asientos 
                string[] concatenado = new string[8];
                //envio de asiento mediante servicio web hacia SIGAF
                item_resAsientosLog = tasientos.EnviarAsientos(tabla_asientos,"X");

               
                for (int j = 0; j < item_resAsientosLog.Length; j++)
                {
                    int x = j + 1;
                    logAsiento += x + " - " + item_resAsientosLog[j] + " - ";
                }
                //Registrar en Bitacora de movimientos

               
             if (!logAsiento.Contains("[E]"))
                {
                    logAsiento = string.Empty ;
                    item_resAsientosLog = new string[10];
                    item_resAsientosLog = tasientos.EnviarAsientos(tabla_asientos, "");


                    for (int j = 0; j < item_resAsientosLog.Length; j++)
                    {
                        int x = j + 1;
                        logAsiento += x + " - " + item_resAsientosLog[j] + " - ";
                    }
                    lstr_ResultadoAsientoCanjeSubasta = "Asiento generado exitosamente";
                    consulta.ConsultarDinamico("UPDATE [cf].[CanjeEmisionDetalle] SET Contabilizacion = 1 where FchPago = '" + lstr_FchCanje.ToString("yyyy.MM.dd") + "' and IdentificadorCanje = 'C' ");

                    // Modificar Descripcion a toda la emision
                    ldas_Emision = consulta.ConsultarDinamico("SELECT * FROM [cf].[CanjeEmisionDetalle] WHERE FchPago = '" + lstr_FchCanje.ToString("yyyy.MM.dd") + "'  and IdentificadorCanje = 'C' ");
                    ldat_Emision = ldas_Emision.Tables["Table"];

                    
                    foreach (DataRow row in ldat_Emision.Rows)
                    {

                             lint_NroValor = Convert.ToInt32(row["NroValor"]);
                             lstr_Nemotecnico= row["Nemotecnico"].ToString().Trim();

                             consulta.ConsultarDinamico("UPDATE [cf].[TitulosValores] SET [Descripcion] = 'Canje' WHERE [NroValor] = " + lint_NroValor + " AND [Nemotecnico] = '" + lstr_Nemotecnico + "' and EstadoValor = 'Vigente'");
                    }

                    // Modificar Descripcion a todos los comprados
                    ldat_Comprados = lcls_TituloValor.ConsultarTituloValor(null, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "Compra", "Vigente", Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/5000"), String.Empty).Tables[0];
                     strExpr = "  TipoNegociacion = 'Compra' AND FchValor= ' " + lstr_FchCanje.ToString("yyyy.MM.dd") + "' ";
                     ldat_Comprados = ldat_Comprados.Select(strExpr).CopyToDataTable();

                
                   foreach (DataRow row in ldat_Comprados.Rows)
                    {

                             lint_NroValor = Convert.ToInt32(row["NroValor"]);
                             lstr_Nemotecnico= row["Nemotecnico"].ToString().Trim();

                             consulta.ConsultarDinamico("UPDATE [cf].[TitulosValores] SET [Descripcion] = 'Canje' WHERE [NroValor] = " + lint_NroValor + " AND [Nemotecnico] = '" + lstr_Nemotecnico + "' and EstadoValor = 'Vigente'");
                    }

                   // Modificar Descripcion a todos los colocados
                   ldas_Colocados = consulta.ConsultarDinamico("SELECT * FROM  [cf].[TitulosCanjeSubasta] WHERE FchCanje = '" + lstr_FchCanje.ToString("yyyy.MM.dd") + "'");
                   ldat_Colocados = ldas_Colocados.Tables["Table"];


                   foreach (DataRow row in ldat_Colocados.Rows)
                   {

                       lint_NroValor = Convert.ToInt32(row["NroValor"]);
                       lstr_Nemotecnico = row["Nemotecnico"].ToString().Trim();

                       consulta.ConsultarDinamico("UPDATE [cf].[TitulosValores] SET [Descripcion] = 'Canje' WHERE [NroValor] = " + lint_NroValor + " AND [Nemotecnico] = '" + lstr_Nemotecnico + "' and EstadoValor = 'Vigente'");
                   }
                
                
                
                }
                else
                {
                    lstr_ResultadoAsientoCanjeSubasta = "Error al generar el asiento";
                }

                lcls_Bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_IdOperacion, "DI"), "Resultado de Contabilización: " + logAsiento, lstr_IdOperacion, lstr_NroValor + "-" + lstr_Nemotecnico);
                return logAsiento;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public string GenerarAsientoSubasta(DataTable ldat_Asiento, string lstr_IdOperacion, string lstr_NroValor, string lstr_Nemotecnico,DateTime lstr_FchCanje,Decimal TipoCambio)
        {
            Seguridad.tBitacora lcls_Bitacora = new Seguridad.tBitacora();
          

            //Coleccion de asientos y tipos de asientos requeridos en SAP expuestos por la referencia del servicio
            wrSigafAsientos.ZfiAsiento item_asiento = new wrSigafAsientos.ZfiAsiento();
            wrSigafAsientos.ZfiAsiento[] tabla_asientos = new wrSigafAsientos.ZfiAsiento[ldat_Asiento.Rows.Count];
            clsDinamico consulta = new clsDinamico();
            clsTituloValor lcls_TituloValor = new clsTituloValor();
            //variables de proceso
            string[] item_resAsientosLog = new string[10];
            string logAsiento = string.Empty;
            string flagEstadoAsiento = string.Empty;
            decimal ldec_Total40 = 0;
            decimal ldec_Total50 = 0;
            decimal ldec_Diferencia40y50 = 0;
            int index = 0;
            var strExpr = "";
         
            //string lstr_Nemotecnico = "";
            int lint_NroValor = 0;
            DataSet ldas_Comprados = new DataSet();
            DataSet ldas_Colocados = new DataSet();
            DataSet ldas_Emision = new DataSet();
            DataTable ldat_Comprados = new DataTable();
            DataTable ldat_Colocados = new DataTable();
            DataTable ldat_Emision = new DataTable();


            try
            {
                foreach (DataRow ldr_Row in ldat_Asiento.Rows)
                {
                    item_asiento = new wrSigafAsientos.ZfiAsiento();
                    index = ldat_Asiento.Rows.IndexOf(ldr_Row);

                    if (index == 0)
                    {
                        item_asiento.Blart = "ID";//Clase de documento
                        item_asiento.Bukrs = "G206";//Sociedad
                        //item_asiento.Werks = ldat_Asiento.Rows[0]["Referencia"].ToString();
                        item_asiento.Bldat = ldat_Asiento.Rows[ldat_Asiento.Rows.IndexOf(ldr_Row)]["Fecha"].ToString();//Fecha de documento
                        item_asiento.Budat = ldat_Asiento.Rows[ldat_Asiento.Rows.IndexOf(ldr_Row)]["Fecha"].ToString();//Fecha de contabilización
                        item_asiento.Bktxt = "Subasta";//Referencia
                    }

                    item_asiento.Waers = ldat_Asiento.Rows[index]["Moneda"].ToString();//Moneda 
                    item_asiento.Bschl = ldat_Asiento.Rows[index]["ClaveContable"].ToString();//Clave de contabilización
                    item_asiento.Hkont = ldat_Asiento.Rows[index]["Cuenta"].ToString();//Cuenta de mayor
                    item_asiento.Wrbtr = Convert.ToDecimal(Convert.ToDecimal(ldat_Asiento.Rows[index]["Monto"].ToString()).ToString("0.0000"));//Importe
                    item_asiento.Sgtxt = ldat_Asiento.Rows[index]["TextoInfo"].ToString();//Texto Informativo (50 caracteres)
                    item_asiento.Kostl = ldat_Asiento.Rows[index]["CentroCosto"].ToString();//Centro de Costo
                    item_asiento.Prctr = ldat_Asiento.Rows[index]["CentroBeneficio"].ToString();//Centro de Beneficio
                    item_asiento.Projk = ldat_Asiento.Rows[index]["ElementoPEP"].ToString();//Elemento PEP
                    item_asiento.Fipex = ldat_Asiento.Rows[index]["PosPre"].ToString();//Posición Presupuestaria
                    item_asiento.Fistl = ldat_Asiento.Rows[index]["CentroGestor"].ToString();//Centro Gestor
                    item_asiento.Geber = ldat_Asiento.Rows[index]["Fondo"].ToString();//Fondo
                    item_asiento.Kblnr = ldat_Asiento.Rows[index]["DocPres"].ToString();//Documento Presupuestario
                    item_asiento.Kblpos = ldat_Asiento.Rows[index]["PosDocPres"].ToString();//Posición de documento presupuestario

                    item_asiento.Xblnr = ldat_Asiento.Rows[index]["PKMovimiento"].ToString();//
                    item_asiento.Bktxt = ldat_Asiento.Rows[index]["Texto2"].ToString();//
                    item_asiento.Xref1Hd = ldat_Asiento.Rows[index]["Ref1Tipo"].ToString();//
                    item_asiento.Xref2Hd = ldat_Asiento.Rows[index]["Ref2Operacion"].ToString();//

                    if (ldat_Asiento.Rows[index]["Moneda"].ToString() == "USD")
                        item_asiento.Kursf = Convert.ToDecimal(TipoCambio.ToString("0.0000"));

                    

                    if (item_asiento.Bschl == "40")
                        ldec_Total40 += item_asiento.Wrbtr;
                    else
                        ldec_Total50 += item_asiento.Wrbtr;

                    tabla_asientos[index] = item_asiento;
                }

                ldec_Diferencia40y50 = ldec_Total40 - ldec_Total50;

                Boolean lbl_cuadrado = false;
                wrSigafAsientos.ZfiAsiento[] tabla_asientos2 = new wrSigafAsientos.ZfiAsiento[index + 1];
                for (int y = 0; y < index + 1; y++)
                {
                    tabla_asientos2[y] = tabla_asientos[y];

                    if (!lbl_cuadrado && ldec_Diferencia40y50 != 0)
                    {
                        if (ldec_Diferencia40y50 > 0 && ldec_Diferencia40y50 < 1 && tabla_asientos2[y].Bschl == "50")
                        {//es mayor el 40 a los 50, subirle la diferencia al 50
                            tabla_asientos2[y].Wrbtr += ldec_Diferencia40y50;
                            lbl_cuadrado = true;
                        }
                        else
                        {//es mayor el 40 a los 50, subirle la diferencia al 50
                            if (ldec_Diferencia40y50 < 0 && ldec_Diferencia40y50 > -1 && tabla_asientos2[y].Bschl == "40")
                            {//es mayor el 50 a los 40, subirle la diferencia al 40

                                tabla_asientos2[y].Wrbtr += Math.Abs(ldec_Diferencia40y50);
                                lbl_cuadrado = true;
                            }
                        }
                    }
                }//for int y



                //Cargar de Asientos 
                string[] concatenado = new string[8];
                //envio de asiento mediante servicio web hacia SIGAF
                item_resAsientosLog = tasientos.EnviarAsientos(tabla_asientos,"X");
                for (int j = 0; j < item_resAsientosLog.Length; j++)
                {
                    int x = j + 1;
                    logAsiento += x + " - " + item_resAsientosLog[j] + " - ";
                }
                //Registrar en Bitacora de movimientos

             
                if (!logAsiento.Contains("[E]"))
                {
                    logAsiento = string.Empty;
                    item_resAsientosLog = new string[10];
                    item_resAsientosLog = tasientos.EnviarAsientos(tabla_asientos, "");
                    for (int j = 0; j < item_resAsientosLog.Length; j++)
                    {
                        int x = j + 1;
                        logAsiento += x + " - " + item_resAsientosLog[j] + " - ";
                    }
                    lstr_ResultadoAsientoCanjeSubasta = "Asiento generado exitosamente";
                    consulta.ConsultarDinamico("UPDATE [cf].[CanjeEmisionDetalle] SET Contabilizacion = 1 where FchPago = '" + lstr_FchCanje.ToString("yyyy.MM.dd") + "' and IdentificadorCanje = 'S' ");

                    // Modificar Descripcion a toda la emision
                    ldas_Emision = consulta.ConsultarDinamico("SELECT * FROM [cf].[CanjeEmisionDetalle] WHERE FchPago = '" + lstr_FchCanje.ToString("yyyy.MM.dd") + "'  and IdentificadorCanje = 'S' ");
                    ldat_Emision = ldas_Emision.Tables["Table"];


                    foreach (DataRow row in ldat_Emision.Rows)
                    {

                        lint_NroValor = Convert.ToInt32(row["NroValor"]);
                        lstr_Nemotecnico = row["Nemotecnico"].ToString().Trim();

                        consulta.ConsultarDinamico("UPDATE [cf].[TitulosValores] SET [Descripcion] = 'Subasta' WHERE [NroValor] = " + lint_NroValor + " AND [Nemotecnico] = '" + lstr_Nemotecnico + "' and EstadoValor = 'Vigente'");
                    }

                    // Modificar Descripcion a todos los comprados
                    ldat_Comprados = lcls_TituloValor.ConsultarTituloValor(null, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "Vigente", Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/5000"), String.Empty).Tables[0];
                    strExpr = "  TipoNegociacion = 'Compra' AND FchValor= ' " + lstr_FchCanje.ToString("yyyy.MM.dd") + "' ";
                    ldat_Comprados = ldat_Comprados.Select(strExpr).CopyToDataTable();


                    foreach (DataRow row in ldat_Comprados.Rows)
                    {

                        lint_NroValor = Convert.ToInt32(row["NroValor"]);
                        lstr_Nemotecnico = row["Nemotecnico"].ToString().Trim();

                        consulta.ConsultarDinamico("UPDATE [cf].[TitulosValores] SET [Descripcion] = 'Subasta' WHERE [NroValor] = " + lint_NroValor + " AND [Nemotecnico] = '" + lstr_Nemotecnico + "' and EstadoValor = 'Vigente'");
                    }
                
                
                }
                else
                {
                    lstr_ResultadoAsientoCanjeSubasta = "Error al generar el asiento";
                }
                string str = lcls_Bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_IdOperacion, "DI"), "Resultado de Contabilización: " + logAsiento, lstr_IdOperacion, lstr_NroValor + "-" + lstr_Nemotecnico);
                
                return logAsiento;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void CrearTituloCanjeSubasta(string lstr_NroEmisionSerie, int lint_NumValor, string lstr_Nemotecnico, string FchCanje, string lstr_TituloCompraEmision)
        {
            try
            {


                Mantenimiento.clsDinamico dinamico = new Mantenimiento.clsDinamico();
                string sql = "Insert into [cf].[TitulosCanjeSubasta] " +
                                           "Values (" + lint_NumValor.ToString() + ",'" + lstr_Nemotecnico + "','" + lstr_NroEmisionSerie + "',Convert(Datetime,'" + FchCanje + "',103),'" + lstr_NroEmisionSerie + "','SG',GETDATE(),'SG',GETDATE())";
                dinamico.ConsultarDinamico(sql);

                string direccion = System.Configuration.ConfigurationManager.AppSettings["DireccionConfigs"];
                direccion += "log.txt";
                if (!System.IO.File.Exists(direccion))
                    System.IO.File.Create(direccion).Dispose();

                System.IO.File.AppendAllText(direccion, string.Format("{0}{1}", sql , Environment.NewLine));
            }
            catch (Exception ex)
            {
                string direccion = System.Configuration.ConfigurationManager.AppSettings["DireccionConfigs"];
                direccion += "log.txt";
                if (!System.IO.File.Exists(direccion))
                    System.IO.File.Create(direccion).Dispose();

                System.IO.File.AppendAllText(direccion, string.Format("{0}{1}", ex.ToString() + " / Valor: " + lint_NumValor.ToString() + " Nemo: " + lstr_Nemotecnico + " / Fecha: " + DateTime.Now.ToString(), Environment.NewLine));
            }

        }
        /// <summary>
        /// Calcula el canje de la Fecha que se envía.
        /// </summary>
        public void CalcularCanjeSubasta(string lstr_IdentificadorCanje, string _Fecha)
        {
            DataTable ldat_Valores = new DataTable();
            DataTable ldat_TituloSerie = new DataTable();
            DataTable ldat_InfoTituloSerie = new DataTable();
            clsTituloValor lcls_TituloValor = new clsTituloValor();
            clsDevengoInteres lcls_DevengoInteres = new clsDevengoInteres();
            List<clsConsultaCalculoFlujoEfectivo> lstConsultaCalculo = new List<clsConsultaCalculoFlujoEfectivo>();
            DateTime ldt_FchInicio = Convert.ToDateTime(_Fecha);


            int lint_NroValor = 0;
            string lstr_Nemotecnico = String.Empty;
            string lstr_NroEmisionSerie = String.Empty;
            string lint_NumValor = String.Empty;
            DateTime ltd_FchValorCanje = DateTime.Now;
            decimal montoCuponCorrido = 0;
            decimal transNeto = 0;
            bool esTirNormal = true;
            decimal ldec_TransBruto = 0;

            int lint_NumValorTit = 0;
            string lstr_NemotecnicoTit = String.Empty;
            DateTime ldt_Anno = DateTime.Now;
            int lint_IdFlujoEfectivoFK = 0;
            decimal ldec_CostoAmortizacionInicial = 0;
            decimal ldec_Intereses = 0;
            decimal ldec_Pago = 0;
            decimal ldec_CostoAmortizacionFinal = 0;
            decimal ldec_DescuentoDevengado = 0;
            decimal ldec_TIR = 0;
            string lstr_Estado = String.Empty;
            string lstr_UsrCreacion = String.Empty;
            string tipoTitulo = string.Empty;

            clsConsultaDevengoInteres devengoInteres = new clsConsultaDevengoInteres();
            DateTime _fechaValor = DateTime.Now;

            string mens1 = String.Empty;
            string mens2 = String.Empty;

            int DiasDif = 0;
            decimal InteresTotal = 0;
            decimal CuponTotal = 0;
            decimal DescuentoDevengo = 0;
            int TotalDiasDif = 0;
            decimal ldec_CostAmortFin = 0;
            DateTime PeriodoAnterior = DateTime.Now;
            try
            {

                    //Obtener titulos compra canje
                    ldat_Valores = lcls_TituloValor.ConsultarTituloValor(null, String.Empty, String.Empty, String.Empty, String.Empty, string.Empty,"Compra", "Vigente", ldt_FchInicio, ldt_FchInicio, String.Empty).Tables[0];


                    string strExpr = " TipoNegociacion = 'Compra' ";

                    if (lstr_IdentificadorCanje.Equals("C"))
                        strExpr += "and DescripcionNegociacion in ('Canje/Lici/Precio','Canje/Lici/Rend','Canje/Inversa/Precio','Canje/Inversa/Rend') ";
                    else
                        strExpr += "and DescripcionNegociacion in ('Canje/Inversa/Precio','Canje/Inversa/Rend') ";

                    ldat_Valores = ldat_Valores.Select(strExpr).CopyToDataTable();

                    for (int i = 0; i < ldat_Valores.Rows.Count; i++)
                    {

                    lstr_NroEmisionSerie = ldat_Valores.Rows[i]["NroEmisionSerie"].ToString();
                    ltd_FchValorCanje = Convert.ToDateTime(ldat_Valores.Rows[i]["FchValor"]);
                    lstr_NroEmisionSerie = lstr_NroEmisionSerie.Trim();

                    Mantenimiento.clsDinamico dinamico = new Mantenimiento.clsDinamico();

                    DataSet dsConta = dinamico.ConsultarDinamico("select Top 1 ISNULL(Contabilizacion,0) from [cf].[CanjeEmisionDetalle] where NroEmisionSerie = '" + lstr_NroEmisionSerie + "' AND Convert(Varchar(8),FchPago,112) = Convert(Varchar(8),(Convert(DATETIME,'" + _Fecha + "',103)),112) ");
                    bool Contabilizacion = false;

                    if (dsConta.Tables[0].Rows.Count != 0)
                        Contabilizacion = (bool)dsConta.Tables[0].Rows[0][0];

                    if (!Contabilizacion)
                    {
                        if (!string.IsNullOrEmpty(lstr_NroEmisionSerie))
                        {

                            //Obtener titulos pertenecen a serie compra 
                            ldat_TituloSerie = lcls_TituloValor.ConsultarTituloValor(null, String.Empty, String.Empty, String.Empty, "V", string.Empty, String.Empty, "Vigente", Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/5000"), lstr_NroEmisionSerie).Tables[0];
                            var strExprTitulo = " TipoNegociacion <> 'Compra' ";
                            ldat_TituloSerie = ldat_TituloSerie.Select(strExprTitulo).CopyToDataTable();
                        }

                        for (int i1 = 0; i1 < ldat_TituloSerie.Rows.Count; i1++)
                        {
                            lint_NumValor = ldat_TituloSerie.Rows[i1]["NroValor"].ToString();
                            lstr_Nemotecnico = ldat_TituloSerie.Rows[i1]["Nemotecnico"].ToString();
                            ldec_TransBruto = Convert.ToDecimal(ldat_TituloSerie.Rows[i1]["ValorTransadoBruto"].ToString());
                            transNeto = Convert.ToDecimal(ldat_TituloSerie.Rows[i1]["ValorTransadoNeto"].ToString());
                            tipoTitulo = ldat_TituloSerie.Rows[i1]["Tipo"].ToString();
                            montoCuponCorrido = transNeto - ldec_TransBruto;

                            if (montoCuponCorrido > 0)
                                esTirNormal = false;
                            else
                                esTirNormal = true;

                            //Obtener devengo de cada titulo 
                            ldat_InfoTituloSerie = lcls_DevengoInteres.ConsultarDevengoInteres(lint_NumValor, lstr_Nemotecnico).Tables[0];

                            for (int i2 = 0; i2 < ldat_InfoTituloSerie.Rows.Count; i2++)
                            {
                                lint_NumValorTit = Convert.ToInt32(ldat_InfoTituloSerie.Rows[i2]["NroValor"]);
                                lstr_NemotecnicoTit = ldat_InfoTituloSerie.Rows[i2]["Nemotecnico"].ToString();
                                ldt_Anno = Convert.ToDateTime(ldat_InfoTituloSerie.Rows[i2]["Anno"]);
                                lint_IdFlujoEfectivoFK = Convert.ToInt32(ldat_InfoTituloSerie.Rows[i2]["IdFlujoEfectivoFK"]);
                                ldec_CostoAmortizacionInicial = Convert.ToDecimal(ldat_InfoTituloSerie.Rows[i2]["CostoAmortizacionInicial"].ToString());
                                ldec_Intereses = Convert.ToDecimal(ldat_InfoTituloSerie.Rows[i2]["Intereses"].ToString());
                                ldec_Pago = Convert.ToDecimal(ldat_InfoTituloSerie.Rows[i2]["Pago"].ToString());
                                ldec_CostoAmortizacionFinal = Convert.ToDecimal(ldat_InfoTituloSerie.Rows[i2]["CostoAmortizacionFinal"].ToString());
                                ldec_DescuentoDevengado = Convert.ToDecimal(ldat_InfoTituloSerie.Rows[i2]["DescuentoDevengado"].ToString());
                                ldec_TIR = Convert.ToDecimal(ldat_InfoTituloSerie.Rows[i2]["TIR"].ToString());

                                devengoInteres.Lint_NumValor = Convert.ToString(lint_NumValorTit);
                                devengoInteres.Lstr_Nemotecnico = lstr_NemotecnicoTit;
                                devengoInteres.Anno1 = ldt_Anno;
                                devengoInteres.IdFlujoEfectivoFK1 = lint_IdFlujoEfectivoFK;
                                devengoInteres.CostoAmortizacionInicial1 = ldec_CostoAmortizacionInicial;
                                devengoInteres.Intereses1 = ldec_Intereses;
                                devengoInteres.Pago1 = ldec_Pago;
                                devengoInteres.CostoAmortizacionFinal1 = ldec_CostoAmortizacionFinal;
                                devengoInteres.DescuentoDevengado1 = ldec_DescuentoDevengado;
                                devengoInteres.Tir1 = ldec_TIR;                              

                                if (tipoTitulo != "Cero Cupón")
                                {
                                    if (ltd_FchValorCanje >= ldt_Anno)
                                    {
                                        devengoInteres = new clsConsultaDevengoInteres();
                                        devengoInteres.Lint_NumValor = Convert.ToString(lint_NumValorTit);
                                        devengoInteres.Lstr_Nemotecnico = lstr_NemotecnicoTit;
                                        devengoInteres.Anno1 = ldt_Anno;
                                        devengoInteres.IdFlujoEfectivoFK1 = lint_IdFlujoEfectivoFK;
                                        devengoInteres.CostoAmortizacionInicial1 = ldec_CostoAmortizacionInicial;
                                        devengoInteres.Intereses1 = ldec_Intereses;
                                        devengoInteres.Pago1 = ldec_Pago;
                                        devengoInteres.CostoAmortizacionFinal1 = ldec_CostoAmortizacionFinal;
                                        devengoInteres.DescuentoDevengado1 = ldec_DescuentoDevengado;
                                        devengoInteres.Tir1 = ldec_TIR;
                                    }
                                    else
                                    {
                                        lcls_DevengoInteres.CrearDevengoInteresCanje(Convert.ToInt32(devengoInteres.Lint_NumValor), devengoInteres.Lstr_Nemotecnico, devengoInteres.Anno1,
                                                    devengoInteres.IdFlujoEfectivoFK1, devengoInteres.CostoAmortizacionInicial1, devengoInteres.Intereses1, devengoInteres.Pago1,
                                                    devengoInteres.CostoAmortizacionFinal1, devengoInteres.DescuentoDevengado1, devengoInteres.Tir1, lstr_Estado, lstr_IdentificadorCanje,ldt_FchInicio, "SG",
                                                    out mens1, out mens2);

                                        break;
                                    }
                                }
                                else
                                {
                                    if (ltd_FchValorCanje == ldt_Anno)
                                    {
                                        devengoInteres = new clsConsultaDevengoInteres();
                                        devengoInteres.Lint_NumValor = Convert.ToString(lint_NumValorTit);
                                        devengoInteres.Lstr_Nemotecnico = lstr_NemotecnicoTit;
                                        devengoInteres.Anno1 = ldt_Anno;
                                        devengoInteres.IdFlujoEfectivoFK1 = lint_IdFlujoEfectivoFK;
                                        devengoInteres.CostoAmortizacionInicial1 = ldec_CostoAmortizacionInicial;
                                        devengoInteres.Intereses1 = InteresTotal;
                                        devengoInteres.Pago1 = ldec_Pago;
                                        devengoInteres.CostoAmortizacionFinal1 = ldec_CostoAmortizacionFinal;
                                        devengoInteres.DescuentoDevengado1 = ldec_DescuentoDevengado;
                                        devengoInteres.Tir1 = ldec_TIR;

                                        lcls_DevengoInteres.CrearDevengoInteresCanje(Convert.ToInt32(devengoInteres.Lint_NumValor), devengoInteres.Lstr_Nemotecnico, devengoInteres.Anno1,
                                                                                    devengoInteres.IdFlujoEfectivoFK1, devengoInteres.CostoAmortizacionInicial1, devengoInteres.Intereses1, devengoInteres.Pago1,
                                                                                    devengoInteres.CostoAmortizacionFinal1, devengoInteres.DescuentoDevengado1, devengoInteres.Tir1, lstr_Estado, lstr_IdentificadorCanje, ldt_FchInicio, "SG",
                                                                                    out mens1, out mens2);
                                    }
                                    else
                                        InteresTotal += ldec_Intereses;

                                }
                            }//for i2

                            DevengoMensualCanje(Convert.ToInt32(lint_NumValor), lstr_Nemotecnico, ltd_FchValorCanje, lstr_IdentificadorCanje, tipoTitulo, out PeriodoAnterior);

                        }//for i1

                        CalcularCanjesEmision(lstr_NroEmisionSerie, lstr_IdentificadorCanje, PeriodoAnterior, ltd_FchValorCanje);

                    }//if
                    
                    }//for i
                    AjusteCanjeDevengo(lstr_IdentificadorCanje, _Fecha);
            }
            catch (Exception ex)
            {
                string direccion = System.Configuration.ConfigurationManager.AppSettings["DireccionConfigs"];
                direccion += "log.txt";
                if (!System.IO.File.Exists(direccion))
                    System.IO.File.Create(direccion).Dispose();

                System.IO.File.AppendAllText(direccion, string.Format("{0}{1}", ex.ToString() + " / Valor: " + lint_NroValor.ToString() + " Nemo: " + lstr_Nemotecnico + " / Fecha: " + DateTime.Now.ToString(), Environment.NewLine));
            }
        }

        public void DevengoMensualCanje(int lint_NroValor, string lstr_Nemotecnico, DateTime _fechaValor, string lstr_IdentificadorCanje, string _TipoTitulo ,out DateTime _PeriodoAnt)
        {

            int TotalDiasDif = 0;
            int DiasDif = 0;
            decimal InteresTotal = 0;
            decimal CuponTotal = 0;
            decimal Descuento = 0;
            string mens1 = String.Empty;
            string mens2 = String.Empty;

            clsDevengoMensual mensualCanje = new clsDevengoMensual();
            clsDevengoMensual clsDevengoMensual = new clsDevengoMensual();
            List<clsConsultaDevengoInteres> LstDevengoInteres = ConsultaDevengoInteres(lint_NroValor, lstr_Nemotecnico);
            clsConsultaDevengoInteres devengoInteres = new clsConsultaDevengoInteres();
            clsConsultaDevengoInteres devengoInteresAct = new clsConsultaDevengoInteres();
            DataSet dsMensual = mensualCanje.ConsultarDevengoMensual(lint_NroValor.ToString(), lstr_Nemotecnico);
            DateTime fechaCanje = new DateTime(_fechaValor.Year, _fechaValor.Month, _fechaValor.Day <= 30 ? _fechaValor.Day : 30);
            DateTime PeriodoAnt = DateTime.Now;
            DateTime PeriodoMenAnt = DateTime.Now;

            try
            {
                if (_TipoTitulo != "Cero Cupón")
                {
                    if (LstDevengoInteres.Count > 0)
                    {
                        for (int x = 0; x < LstDevengoInteres.Count; x++)
                        {
                            if (LstDevengoInteres[x].Anno1 > fechaCanje)
                            {
                                TotalDiasDif = Dias360(LstDevengoInteres[x].Anno1, PeriodoAnt);
                                devengoInteresAct = LstDevengoInteres[x];

                                if (TotalDiasDif == 0)
                                    TotalDiasDif = 1;

                                break;
                            }
                            else
                            {
                                PeriodoAnt = LstDevengoInteres[x].Anno1;
                                devengoInteres = new clsConsultaDevengoInteres();
                                devengoInteres = LstDevengoInteres[x];
                            }
                        }
                    }

                    for (int i = 0; i < dsMensual.Tables[0].Rows.Count; i++)
                    {
                        DateTime periodo = Convert.ToDateTime(dsMensual.Tables[0].Rows[i]["Periodo"].ToString());
                        if (periodo.Day == 31)
                            periodo = new DateTime(periodo.Year, periodo.Month, 30);

                        

                        if (fechaCanje >= periodo)
                        {
                            PeriodoMenAnt = periodo;
                            if (periodo > PeriodoAnt)
                            {
                                clsDevengoMensual.CrearDevengoMensualCanje(lint_NroValor, lstr_Nemotecnico, dsMensual.Tables[0].Rows[i]["Periodo"].ToString(), 1, (int)dsMensual.Tables[0].Rows[i]["DiasPeriodo"],
                                                       (decimal)dsMensual.Tables[0].Rows[i]["InteresTotal"], (decimal)dsMensual.Tables[0].Rows[i]["Cupon"], (decimal)dsMensual.Tables[0].Rows[i]["Descuento"], lstr_IdentificadorCanje, "SG", out mens1, out mens2, fechaCanje);

                               // PeriodoMenAnt = Convert.ToDateTime(dsMensual.Tables[0].Rows[i]["Periodo"].ToString());
                                if (fechaCanje == PeriodoMenAnt)
                                    break;
                            }
                        }
                        else
                        {
                            //DiasDif = +(Dias360(Convert.ToDateTime(dsMensual.Tables[0].Rows[i]["Periodo"].ToString()), fechaCanje));
                            DiasDif = +(Dias360(fechaCanje, PeriodoMenAnt));

                            if (PeriodoMenAnt.Month == 2)
                            {
                                if ((PeriodoMenAnt.Day == 29) || (PeriodoMenAnt.Day == 28))
                                    DiasDif = DiasDif - 2;
                            }

                            if (DiasDif > 30)
                                DiasDif = 30;

                            InteresTotal = decimal.Round((+(devengoInteresAct.Intereses1 / TotalDiasDif) * DiasDif), 2);
                            CuponTotal = decimal.Round(((devengoInteresAct.Pago1 / TotalDiasDif) * DiasDif), 2);
                            Descuento = decimal.Round((+(InteresTotal + CuponTotal)), 2);

                            if (DiasDif > 0)
                                clsDevengoMensual.CrearDevengoMensualCanje(lint_NroValor, lstr_Nemotecnico, fechaCanje.ToString(), 1, DiasDif, InteresTotal, CuponTotal,
                                                                           Descuento, lstr_IdentificadorCanje, "SG", out mens1, out mens2, fechaCanje);

                            break;
                        }
                    }

                }
                else
                {
                    for (int i = 0; i < dsMensual.Tables[0].Rows.Count; i++)
                    {
                        DateTime periodo = Convert.ToDateTime(dsMensual.Tables[0].Rows[i]["Periodo"].ToString());
                        if ((fechaCanje.Month == periodo.Month) && (fechaCanje.Year == periodo.Year))
                        {
                            decimal intTot = 0;
                            int diasPer = 0;
                            for (int x = 0; x < LstDevengoInteres.Count; x++)
                            {
                                if ((fechaCanje.Month == LstDevengoInteres[x].Anno1.Month) && (fechaCanje.Year == LstDevengoInteres[x].Anno1.Year))
                                {
                                    intTot += LstDevengoInteres[x].Intereses1;
                                    diasPer++;

                                    if (fechaCanje == LstDevengoInteres[x].Anno1)
                                        break;

                                }
                            }
                            clsDevengoMensual.CrearDevengoMensualCanje(lint_NroValor, lstr_Nemotecnico, fechaCanje.ToShortDateString(), 1, diasPer,
                                                                      intTot, 0, intTot, lstr_IdentificadorCanje, "SG", out mens1, out mens2, fechaCanje);
                            break;

                        }
                        else
                            clsDevengoMensual.CrearDevengoMensualCanje(lint_NroValor, lstr_Nemotecnico, dsMensual.Tables[0].Rows[i]["Periodo"].ToString(), 1, (int)dsMensual.Tables[0].Rows[i]["DiasPeriodo"],
                                                                      (decimal)dsMensual.Tables[0].Rows[i]["InteresTotal"], (decimal)dsMensual.Tables[0].Rows[i]["Cupon"], (decimal)dsMensual.Tables[0].Rows[i]["Descuento"], lstr_IdentificadorCanje, "SG", out mens1, out mens2, fechaCanje);

                    }
                }


                Mantenimiento.clsDinamico dinamico = new Mantenimiento.clsDinamico();
                dinamico.ConsultarDinamico("UPDATE [cf].[DevengosMensualesCanje] " +
                                           "SET Descuento = Descuento + ((InteresTotal + Cupon) - Descuento) " +
                                           "WHERE(InteresTotal + Cupon) - Descuento <> 0");
                _PeriodoAnt = PeriodoAnt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///  Realiza la contabilización de los canjes
        /// </summary>
        public string CrearAsientoCanje(string FechaCanjeEmision)
        {
            
            DataTable ldat_Valores = new DataTable();
            DataSet ldas_Tiras = new DataSet();
            DataSet ldas_TablaArriba = new DataSet();
            DataSet ldas_TablaAbajo = new DataSet();
            DataSet ldas_Colocados = new DataSet();
            DataSet ldas_ColocadosCapital = new DataSet();
            DataSet ldas_ColocadosCanje = new DataSet();
            DataSet ldas_CompradosCanje = new DataSet();
            DataSet ldas_CompradosCPLP = new DataSet();
            DataTable ldat_TablaArriba = new DataTable();
            DataTable ldat_TablaAbajo = new DataTable();
            DataTable ldat_Colocados = new DataTable();
            DataTable ldat_ColocadosCanje = new DataTable();
            DataTable ldat_CompradosCanje = new DataTable();
            DataTable ldat_ColocadosCapital = new DataTable();
            DataTable ldat_CompradosCPLP = new DataTable();
            DataTable ldat_Tira = new DataTable();
            DataTable ldat_Nemotecnico = new DataTable();
            DataSet ldas_Nemotecnico = new DataSet();

            clsTituloValor lcls_TituloValor = new clsTituloValor();
            clsDevengoInteres lcls_DevengoInteres = new clsDevengoInteres();
            Mantenimiento.clsPropietarios lcls_Propietarios = new Mantenimiento.clsPropietarios();
            Mantenimiento.clsTiposAsiento lcls_TiposAsiento = new Mantenimiento.clsTiposAsiento();
            clsTiposAsiento tus_TipoAsiento = new clsTiposAsiento();
            clsDinamico consulta = new clsDinamico();
            clsTiposAsiento asientos = new clsTiposAsiento();

            int lint_NroValor = 0;
            string lstr_Nemotecnico = String.Empty;
            string lstr_Propietario = String.Empty;
            string lstr_Moneda = String.Empty;
            string lstr_NumValor = String.Empty;
            string lstr_Operacion = String.Empty;
            string lstr_NomOperacion = String.Empty;
            decimal ldec_monto = 0;
            string lstr_Tipo = String.Empty;
            string lstr_NroValorColocado = String.Empty;
            string lstr_NemotecnicoColocado = String.Empty;
            DateTime lstr_FchCanje = Convert.ToDateTime(FechaCanjeEmision);
            string lstr_Plazo = String.Empty;
            string lstr_Serie = String.Empty;


            decimal ldec_ImportesDevengarArribaCorto = 0;
            decimal ldec_InteresDevengarArribaCorto = 0;
            decimal ldec_PrimaArribaCorto = 0;
            decimal ldec_ImportesDevengarArribaLargo= 0;
            decimal ldec_InteresDevengarArribaLargo = 0;
            decimal ldec_PrimaArribaLargo = 0;

            decimal ldec_ValorFacialCorto = 0;
            decimal ldec_ValorFacialLargo = 0;
            decimal ldec_DiferenciaCompradosColocados = 0;
            decimal ldec_ValorFacialComprados = 0;
            decimal ldec_ImportesDevengarArriba = 0;
            decimal ldec_InteresDevengarArriba = 0;
            decimal ldec_Diferencia = 0;
            decimal ldec_DiferenciaTransados = 0;
            decimal ldec_CapitalAbajo = 0;
            decimal ldec_ImportesDevengarAbajo = 0;
            decimal ldec_PrimaAbajo = 0;
            decimal ldec_PrimaArriba = 0;
            decimal ldec_InteresDevengarAbajo = 0;
            //decimal  ldec_PlazoValor = 0;
            //string lstr_PlazoValor = "";
            int lint_DiferenciaPlazoValor = 0;
            decimal ldec_ValorFacialColocado = 0;
            decimal ldec_DiferenciaValorFacial = 0;
            decimal ldec_NetoBaja = 0;
            decimal ldec_NetoBajaDiferencia = 0;
            string lstr_ModuloSINPE = "";
            decimal ldec_ValorFacial = 0;
            decimal ldec_ValorTransadoBruto = 0;
            decimal ldec_ValorTransadoNeto = 0;
            decimal ldec_CupoCorridoColocadoPrima = 0;
            decimal ldec_CupoCorridoColocadoDescuento = 0;
            decimal ldec_TransadoNetoColocado = 0;
            decimal ldec_TransadoBrutoColocado = 0;
            decimal ldec_PrimasColocados = 0;
            decimal ldec_DescuentoColocados = 0;
            decimal ldec_BajaFaciales = 0;
            decimal ldec_DiferenciaCupoCorridoColocado = 0;
            int lint_ContadorPago = 0;
            int lint_ContadorPrima = 0;
            int lint_contadorImpDev = 0;

            int lint_ContadorPrimaAsientoLargo = 0;
            int lint_ContadorDescuentoAsientoLargo = 0;
            decimal ldec_ValorNetoComprados = 0;
            decimal ldec_ValorNetoColocados = 0;
            string lstr_IdClaveContable = String.Empty;
            string resultado = "";
            DateTime ltd_FchVencimiento = DateTime.Now;
            DateTime ltd_FchCanjeTabla = DateTime.Now;
            DateTime ltd_FchValor = DateTime.Now;
            bool lstr_Contabilizacion;
            int lint_ContadorAsiento = 0;
            string lstr_SerieTitulo = String.Empty;
            int lint_AñoVencimiento = 0;
            int lint_AñoValor = 0;
            int lint_DiferenciaTrasciende = 0;
            int lint_ContadorPrimaAsiento = 0;
            int lint_ContadorDescuentoAsiento = 0;
            bool SinError = true;
            string lstr_NemotecnicoAnterior = "";
            DataTable ldat_Asiento = RegistroContable();
            decimal ldec_DiferenciaPrimaColocados = 0;
            int lint_ContadorCapitalAsiento = 0;
            int lint_ContadorCuponAsiento = 0;
            int? secuencia = null;
            decimal ldec_TotalValorFacialColocado = 0;
            string lstr_OrderBy = " ORDER BY Codigo, IdModulo, Secuencia";
            string lstr_NemotecnicoComprados = "";

            clsConsultaDevengoInteres devengoInteres = new clsConsultaDevengoInteres();
            DateTime _fechaValor = DateTime.Now;
            Boolean esDescuento = true;

            try
            {

                
                ldas_TablaAbajo = consulta.ConsultarDinamico("SELECT * FROM [cf].[CanjeEmisionDetalle] where Convert(Varchar(8),FchPago,112) = Convert(Varchar(8),(Convert(DATETIME,'" + lstr_FchCanje.ToShortDateString() + "',103)),112)");
                ldat_TablaAbajo = ldas_TablaAbajo.Tables["Table"];

                for (int j = 0; j < ldat_TablaAbajo.Rows.Count; j++)
                {
                    lstr_Contabilizacion = Convert.ToBoolean(ldat_TablaAbajo.Rows[j]["Contabilizacion"]);
                    lstr_SerieTitulo = ldat_TablaAbajo.Rows[j]["NroEmisionSerie"].ToString();
                    ltd_FchCanjeTabla = Convert.ToDateTime(ldat_TablaAbajo.Rows[j]["FchPago"]);

                    if (lstr_Contabilizacion == true)
                    {
                        lint_ContadorAsiento = 1;
                    }
                }

                if (lint_ContadorAsiento == 0)
                {

                    if (ltd_FchCanjeTabla.ToString("yyyy.MM.dd") == lstr_FchCanje.ToString("yyyy.MM.dd"))
                    {

                        var strExpr = "";
                        //Obtener titulos compra canje
                        ldat_Valores = lcls_TituloValor.ConsultarTituloValor(null, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "Compra", "Vigente", Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/5000"), String.Empty).Tables[0];
                        strExpr = " TipoNegociacion = 'Compra' AND [NroEmisionSerie]=  '" + lstr_SerieTitulo + "' AND FchValor= ' " + lstr_FchCanje.ToString("yyyy.MM.dd") + " '";
                        ldat_Valores = ldat_Valores.Select(strExpr).CopyToDataTable();

                        for (int j = 0; j < ldat_Valores.Rows.Count; j++)
                        {
                            lstr_FchCanje = Convert.ToDateTime(ldat_Valores.Rows[j]["FchValor"]);
                            lstr_Moneda = ldat_Valores.Rows[j]["Moneda"].ToString();
                        }

                        ldat_Valores = lcls_TituloValor.ConsultarTituloValor(null, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "Compra", "Vigente", Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/5000"), String.Empty).Tables[0];
                        strExpr = "  TipoNegociacion = 'Compra' AND FchValor= ' " + lstr_FchCanje.ToString("yyyy.MM.dd") + "' ";
                        ldat_Valores = ldat_Valores.Select(strExpr).CopyToDataTable();

                        for (int i1 = 0; i1 < ldat_Valores.Rows.Count; i1++)
                        {
                            ldec_ValorFacialComprados += Convert.ToDecimal(ldat_Valores.Rows[i1]["ValorFacial"]);
                            ldec_ValorNetoComprados += Convert.ToDecimal(ldat_Valores.Rows[i1]["ValorTransadoNeto"]);

                        }


                        decimal ldec_TipoCambioColones = Convert.ToDecimal(tipocambio.ConsultarTiposCambio("CRCN", lstr_FchCanje, "3140", "N").Tables[0].Rows[0]["Valor"].ToString());
                        decimal ldec_TipoCambioUDE = Convert.ToDecimal(tipocambio.ConsultarTiposCambio("UDE", lstr_FchCanje, "", "N").Tables[0].Rows[0]["Valor"].ToString());


                        ldas_TablaAbajo = consulta.ConsultarDinamico("SELECT * FROM [cf].[ResumenCanje] WHERE Convert(Varchar(8),FchCanje,112) = Convert(Varchar(8),(Convert(DATETIME,'" + lstr_FchCanje.ToShortDateString() + "',103)),112)");
                        ldat_TablaAbajo = ldas_TablaAbajo.Tables["Table"];

                        foreach (DataRow row in ldat_TablaAbajo.Rows)
                        {
                            ldec_CapitalAbajo = Convert.ToDecimal(row["Capital"]);
                            ldec_ImportesDevengarAbajo = Convert.ToDecimal(row["Descuento"]);
                            ldec_PrimaAbajo = Convert.ToDecimal(row["Prima"]);
                            ldec_InteresDevengarAbajo = Convert.ToDecimal(row["InteresesDevengado"]);
                            ldec_Diferencia = Convert.ToDecimal(row["TitulosDiferencia"]);
                            ldec_DiferenciaTransados = Convert.ToDecimal(row["EntradaCaja"]);
                            ldec_DiferenciaTransados = Math.Abs(ldec_DiferenciaTransados);
                            ldec_BajaFaciales = Convert.ToDecimal(row["BajaFaciales"]);
                            ldec_ValorNetoColocados = Convert.ToDecimal(row["TotaColocado"]);
                        }


                        //ldas_TablaArriba = consulta.ConsultarDinamico("SELECT * FROM [cf].[CanjeResumenSerie] WHERE Convert(Varchar(8),FchCanje,112) = Convert(Varchar(8),(Convert(DATETIME,'" + lstr_FchCanje.ToShortDateString() + "',103)),112) AND IdentificadorCanje = 'C' ");
                        //ldat_TablaArriba = ldas_TablaArriba.Tables["Table"];                   


                        decimal ldec_TipoCambio = lstr_Moneda.Equals("USD") ? ldec_TipoCambioColones : (lstr_Moneda.Equals("CRCN") ? 1 : ldec_TipoCambioUDE);
                      
                        ///----------------------------------------------------------------------------------------------------///
                        ///     Regiones de Tiras en Colones
                        ///----------------------------------------------------------------------------------------------------///
                        #region colones
                        if (lstr_Moneda == "CRCN")
                        {

                            lint_ContadorPago = 0;
                            lint_ContadorPrima = 0;
                            ldat_Valores = lcls_TituloValor.ConsultarTituloValor(null, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "Compra", "Vigente", Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/5000"), String.Empty).Tables[0];
                            strExpr = " TipoNegociacion = 'Compra' AND FchValor= '" + lstr_FchCanje.ToString("yyyy.MM.dd") + "'";
                            ldat_Valores = ldat_Valores.Select(strExpr).CopyToDataTable();

                            for (int i = 0; i < 1; i++)
                            {
                                lstr_NumValor = ldat_Valores.Rows[i]["NroValor"].ToString();
                                lstr_Moneda = ldat_Valores.Rows[i]["Moneda"].ToString();
                                lstr_Nemotecnico = ldat_Valores.Rows[i]["Nemotecnico"].ToString();
                                lstr_Propietario = ldat_Valores.Rows[i]["Propietario"].ToString();             
                                ltd_FchVencimiento = Convert.ToDateTime(ldat_Valores.Rows[i]["FchVencimiento"]);
                                lstr_Serie = ldat_Valores.Rows[i]["NroEmisionSerie"].ToString().Trim();
                                lstr_Tipo = ldat_Valores.Rows[i]["Tipo"].ToString().Trim();
                                lstr_ModuloSINPE = ldat_Valores.Rows[i]["ModuloSINPE"].ToString();
                                ldec_ValorFacial = Convert.ToDecimal(ldat_Valores.Rows[i]["ValorFacial"]);
                                ldec_ValorTransadoBruto = Convert.ToDecimal(ldat_Valores.Rows[i]["ValorTransadoBruto"]);
                                ldec_ValorTransadoNeto = Convert.ToDecimal(ldat_Valores.Rows[i]["ValorTransadoNeto"]);
                                lint_AñoValor = lstr_FchCanje.Year;
                                lint_AñoVencimiento = ltd_FchVencimiento.Year;

                                if (lstr_Nemotecnico != lstr_NemotecnicoAnterior)
                                {
                                    lstr_NemotecnicoAnterior = lstr_Nemotecnico;
                            

                                ldas_Colocados = consulta.ConsultarDinamico("SELECT Top 1 * FROM [cf].[TitulosValores] WHERE NroEmisionSerie = '" + lstr_Serie + "' AND TipoNegociacion != 'Compra' ORDER BY FchValor ASC");
                                ldat_Colocados = ldas_Colocados.Tables["Table"];

                                foreach (DataRow row in ldat_Colocados.Rows)
                                {
                                    ltd_FchValor = Convert.ToDateTime(row["FchValor"].ToString());
                                }
                                TimeSpan ts = lstr_FchCanje - ltd_FchValor;
                                lint_DiferenciaPlazoValor = ts.Days;


                                TimeSpan ts1 = lstr_FchCanje - ltd_FchVencimiento;
                                lint_DiferenciaTrasciende = ts1.Days;


                                ldas_TablaArriba = consulta.ConsultarDinamico("SELECT * FROM [cf].[CanjeResumenSerie] WHERE [NroEmisionSerie] =  '" + lstr_Serie + "' AND Convert(Varchar(8),FchCanje,112) = Convert(Varchar(8),(Convert(DATETIME,'" + lstr_FchCanje.ToShortDateString() + "',103)),112)");
                                ldat_TablaArriba = ldas_TablaArriba.Tables["Table"];

                                foreach (DataRow row in ldat_TablaArriba.Rows)
                                {
                                    ldec_NetoBaja += Convert.ToDecimal(row["NetoBaja"]);
                                }

                                ldec_NetoBajaDiferencia = ldec_ValorTransadoNeto - ldec_NetoBaja;

                                if (lstr_Propietario == "ND")
                                {
                                    lstr_Propietario = "PRIVADO";
                                }
                                else
                                {

                                    if (lcls_Propietarios.ConsultarPropietarios(string.Empty, string.Empty, string.Empty, lstr_Propietario, "S").Tables[0].Rows.Count == 0)
                                {
                                    lstr_Propietario = "PRIVADO";
                                }
                                else
                                {
                                    lstr_Propietario = "PUBLICO";
                                }

                                }

                                if (lint_DiferenciaPlazoValor <= 361)
                                {
                                    lstr_Plazo = "CP";

                                }
                                else
                                {
                                    lstr_Plazo = "LP";
                                }

                                if ((ldec_ValorNetoComprados - ldec_ValorNetoColocados) > 0)
                                {

                                    lstr_IdClaveContable = "50";
                                }
                                else
                                {
                                    lstr_IdClaveContable = "40";
                                }


                                ldec_DiferenciaCompradosColocados = ldec_CapitalAbajo - ldec_ValorFacialComprados;
                                ldec_DiferenciaCompradosColocados = Math.Abs(ldec_DiferenciaCompradosColocados);

//-----------------------------------------------------------------Regiones ID63 y ID64---------------------------------------------
                                if (ldec_CapitalAbajo > ldec_ValorFacialComprados)
                                {
                                    #region ID63


                                    lstr_Operacion = "ID63";

                                    DataSet lds_Operaciones = loperacion.ConsultarOperaciones(lstr_Operacion, "IdModulo IN ('DI')", "");
                                    if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
                                    {
                                        lstr_NomOperacion = lds_Operaciones.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
                                    }
                                    var strExprTitulo = "";
                                    string lstr_Monto = string.Empty;
                                    DataTable lds_Datos = new DataTable();
                                    decimal ldec_MontoTotal = 0;
                                    string reservasError = "";
                                    string lstr_NuevoPosPrePago = string.Empty;
                                    DataSet ldat_Reservas = new DataSet();
                                
                                    // Vacias
                                    string cod_aux2 = (lstr_Nemotecnico.StartsWith("PT") ? "PT" : "");
                                    ldas_Tiras = tus_TipoAsiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "", "", "", secuencia, lstr_OrderBy);
                                    ldat_Tira = ldas_Tiras.Tables["Table"];
                                    strExprTitulo = " ([CodigoAuxiliar2] = '') AND [IdClaveContable] = '" + lstr_IdClaveContable + "' ";
                                    ldat_Tira = ldat_Tira.Select(strExprTitulo).CopyToDataTable();

                                    if (lint_ContadorPago == 0)
                                    {
                                        lint_ContadorPago += lint_ContadorPago + 1;
                                        foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                        {
                                            int index = ldat_Tira.Rows.IndexOf(ldr_Row);
                                            ldec_monto = ldec_DiferenciaTransados;

                                            ldat_Asiento.Rows.Add(
                                                lstr_NumValor + " " + lstr_Nemotecnico,
                                                lstr_FchCanje.ToString("dd.MM.yyyy"),
                                                ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                                "CANJE COLONES",
                                                ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                                Math.Round(ldec_monto, 2),
                                                lstr_NumValor + "." + lstr_Nemotecnico,//pk
                                                tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                                lstr_Moneda,//tipo
                                                lstr_Operacion + "." + lstr_NomOperacion //operacion
                                                );
                                        }

                                    }



                                    // Vacias

                                    // Normales
                                   
                                    //Colocacion
                                    ldas_Nemotecnico = consulta.ConsultarDinamico("SELECT DISTINCT(Nemotecnico) FROM  [cf].[TitulosCanjeSubasta] WHERE FchCanje = '" + lstr_FchCanje.ToString("yyyy.MM.dd") + "'");
                                    ldat_Nemotecnico = ldas_Nemotecnico.Tables["Table"];
                                    string lstr_NemotecnicoColocados;
                                    foreach (DataRow row in ldat_Nemotecnico.Rows)
                                    {
                                        lstr_NemotecnicoColocados = row["Nemotecnico"].ToString().Trim();
                                        ldec_DiferenciaPrimaColocados = 0;
                                        ldec_CupoCorridoColocadoDescuento = 0;
                                        ldec_TotalValorFacialColocado = 0;
                                        ldec_DescuentoColocados = 0;
                                        ldec_PrimasColocados = 0;
                                        lint_ContadorDescuentoAsiento = 0;
                                        lint_ContadorPrimaAsiento = 0;
                                        lint_ContadorCuponAsiento = 0;
                                        ldas_Colocados = consulta.ConsultarDinamico("SELECT * FROM  [cf].[TitulosCanjeSubasta] WHERE FchCanje = '" + lstr_FchCanje.ToString("yyyy.MM.dd") + "' AND Nemotecnico ='" + lstr_NemotecnicoColocados + "'");
                                        ldat_Colocados = ldas_Colocados.Tables["Table"];
                                        lint_ContadorCapitalAsiento = 0;


                                        foreach (DataRow row1 in ldat_Colocados.Rows)
                                        {
                                            lstr_NroValorColocado = row1["NroValor"].ToString().Trim();
                                            lstr_Nemotecnico = row1["Nemotecnico"].ToString().Trim();

                                            ldas_ColocadosCanje = consulta.ConsultarDinamico("SELECT * FROM [cf].[TitulosValores] WHERE [NroValor] = '" + lstr_NroValorColocado + "' AND [Nemotecnico] = '" + lstr_Nemotecnico + "' AND EstadoValor = 'Vigente' AND IndicadorCupon = 'V' AND FchValor = '" + lstr_FchCanje.ToString("yyyy.MM.dd") + "'");
                                            ldat_ColocadosCanje = ldas_ColocadosCanje.Tables["Table"];

                                            foreach (DataRow row2 in ldat_ColocadosCanje.Rows)
                                            {
                                                ldec_ValorFacialColocado = Convert.ToDecimal(row2["ValorFacial"]);
                                                ldec_TransadoNetoColocado = Convert.ToDecimal(row2["ValorTransadoNeto"]);
                                                ldec_TransadoBrutoColocado = Convert.ToDecimal(row2["ValorTransadoBruto"]);
                                            }

                                            esDescuento = ldec_ValorFacial >= ldec_ValorTransadoBruto;
                                            ldec_DiferenciaPrimaColocados = ldec_ValorFacialColocado - ldec_TransadoBrutoColocado;
                                            ldec_DiferenciaCupoCorridoColocado = ldec_TransadoNetoColocado - ldec_TransadoBrutoColocado;
                                            ldec_TotalValorFacialColocado += ldec_ValorFacialColocado;

                                            if (ldec_DiferenciaPrimaColocados > 0)
                                            {
                                                ldec_DescuentoColocados += ldec_DiferenciaPrimaColocados;
                                                //ldec_CupoCorridoColocadoDescuento += ldec_DiferenciaCupoCorridoColocado;
                                            }
                                            if (ldec_DiferenciaPrimaColocados < 0)
                                            {
                                                ldec_PrimasColocados += ldec_DiferenciaPrimaColocados;
                                                //ldec_CupoCorridoColocadoPrima += ldec_DiferenciaCupoCorridoColocado;
                                                //ldec_PrimasColocados = ldec_PrimasColocados;
                                            }

                                            ldec_CupoCorridoColocadoDescuento += ldec_DiferenciaCupoCorridoColocado;
                                       
                                        }

                                        ldas_ColocadosCapital = consulta.ConsultarDinamico("SELECT TOP 1 tcs.Nemotecnico, count (tv.Nemotecnico ) As NumeroVeces FROM (SELECT DISTINCT Nemotecnico, fchcanje FROM [cf].[TitulosCanjeSubasta]) tcs LEFT OUTER JOIN [cf].[TitulosValores] tv ON tv.Nemotecnico = tcs.Nemotecnico AND tv.FchValor = tcs.FchCanje AND TV.IndicadorCupon = 'V' AND TV.TipoNegociacion = 'Compra' WHERE tcs.FchCanje = '" + lstr_FchCanje.ToString("yyyy.MM.dd") + "' GROUP BY tcs.Nemotecnico ORDER BY count (tv.Nemotecnico ) ASC ");
                                        ldat_ColocadosCapital = ldas_ColocadosCapital.Tables["Table"];
                                        string lstr_NumeroNemotecnicoColocado = "";

                                        int lint_NemotecnicoNumeroVeces = 0;
                                        foreach (DataRow row3 in ldat_ColocadosCapital.Rows)
                                        {
                                            lint_NemotecnicoNumeroVeces = Convert.ToInt32(row3["NumeroVeces"]);
                                            lstr_NumeroNemotecnicoColocado = row3["Nemotecnico"].ToString().Trim();

                                        }



                                        
                                        
                                        ldec_DiferenciaValorFacial = ldec_ValorFacialColocado - ldec_ValorFacial;
                                        ldec_DiferenciaValorFacial = Math.Abs(ldec_DiferenciaValorFacial);

                                        ldas_Tiras = tus_TipoAsiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_NemotecnicoColocados, lstr_Propietario, "", lstr_Plazo, "", secuencia, lstr_OrderBy);
                                        ldat_Tira = ldas_Tiras.Tables["Table"];



                                        foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                        {

                                            int index = ldat_Tira.Rows.IndexOf(ldr_Row);
                                            ldec_monto = 0;
                                            string IdClaveContable = ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim();
                                            string Tipo = ldat_Tira.Rows[index]["CodigoAuxiliar2"].ToString().Trim();
                                            string Pospre = ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim();
                                            string l_strLetra = Pospre.Substring(0, 1);

                                  

                                            switch (Tipo + IdClaveContable)
                                            {

                                                case "CAPITAL50":

                                                    if (l_strLetra == "I" && lstr_NumeroNemotecnicoColocado == lstr_Nemotecnico)
                                                    {
                                                        ldec_monto = ldec_DiferenciaCompradosColocados;
                                                        ldec_monto = Math.Abs(ldec_monto);

                                                    }
                                                    else
                                                    {
                                                        if (lstr_NumeroNemotecnicoColocado == lstr_Nemotecnico)
                                                        {
                                                            if (ldec_TotalValorFacialColocado > ldec_DiferenciaCompradosColocados)
                                                            {
                                                                ldec_monto = ldec_TotalValorFacialColocado - ldec_DiferenciaCompradosColocados;
                                                                ldec_monto = Math.Abs(ldec_monto);
                                                            }

                                                        }
                                                        else
                                                        {
                                                            if (l_strLetra != "I")
                                                            {
                                                                if (lint_ContadorCapitalAsiento == 0)
                                                                {
                                                                    ldec_monto = ldec_TotalValorFacialColocado;
                                                                    ldec_monto = Math.Abs(ldec_monto);
                                                                    lint_ContadorCapitalAsiento = 1;
                                                                }
                                                            }
                                                           
                                                        }
                                                    }
                                                   
                                                      

                                                    break;
                                                case "IMP_DEV40":
                                                    
                                                    if (ldec_DiferenciaPrimaColocados == 0)
                                                    {
                                                        ldec_monto = 0;
                                                    }
                                                    else
                                                    {   
                                                        if (lint_ContadorDescuentoAsiento == 0 )
                                                        {
                                                            ldec_monto = ldec_DescuentoColocados;
                                                            lint_ContadorDescuentoAsiento = 1;
                                                        }
                                                        
                                                    }
                                                    break;


                                                case "IMP_DEV50":

                                                    if (lint_ContadorCuponAsiento == 0)
                                                    {
                                                        ldec_monto = ldec_CupoCorridoColocadoDescuento;
                                                        ldec_monto = Math.Abs(ldec_monto);
                                                        lint_ContadorCuponAsiento = 1;
                                                    }

                                                    if (!esDescuento)
                                                    {
                                                        for (int iRow = 0; iRow < ldat_Tira.Rows.Count; iRow++)
                                                        {
                                                            string IdClaveContable2 = ldat_Tira.Rows[iRow]["IdClaveContable"].ToString().Trim();
                                                            string Tipo2 = ldat_Tira.Rows[iRow]["CodigoAuxiliar2"].ToString().Trim();
                                                            if ((Tipo2 + IdClaveContable2).Equals("PRIMAS50"))
                                                            {
                                                                index = iRow;
                                                            }
                                                        }
                                                    }
                                                   
                                                    break;

                                                case "PRIMAS50":


                                                    if (ldec_DiferenciaPrimaColocados == 0)
                                                    {
                                                        ldec_monto = 0;
                                                    }
                                                    else
                                                    {
                                                        if (lint_ContadorPrimaAsiento == 0)
                                                        {
                                                            ldec_monto = Math.Abs(ldec_PrimasColocados);
                                                            lint_ContadorPrimaAsiento = 1;
                                                        }
                                                       
                                                    }

                                                    break;

                                            }

                                                    if (ldec_monto != 0)
                                                    {


                                                        ldat_Asiento.Rows.Add(
                                                    lstr_NumValor + " " + lstr_NemotecnicoColocados,
                                                    lstr_FchCanje.ToString("dd.MM.yyyy"),
                                                    ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                                    ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                                    ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                                    "CANJE COLONES",
                                                    ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                                    ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                                    ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                                    ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                                    ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                                    ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                                    ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                                    ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                                    Math.Round(ldec_monto, 2),
                                                    lstr_NumValor + "." + lstr_Nemotecnico,//pk
                                                    tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                                    lstr_Moneda,//tipo
                                                    lstr_Operacion + "." + lstr_NomOperacion //operacion
                                                    );
                                                    }
                                        }
                                    }


                                    //Colocacion

                                    //Compras

                                    ldas_CompradosCanje = consulta.ConsultarDinamico("SELECT DISTINCT (Nemotecnico) FROM [cf].[TitulosValores] WHERE TipoNegociacion = 'Compra' AND  FchValor = '" + lstr_FchCanje.ToString("yyyy.MM.dd") + "' and IndicadorCupon = 'V'  AND EstadoValor = 'Vigente'");
                                    ldat_CompradosCanje = ldas_CompradosCanje.Tables["Table"];

                                    foreach (DataRow row in ldat_CompradosCanje.Rows)
                                    {
                                        lstr_NemotecnicoComprados = row["Nemotecnico"].ToString().Trim();
                                        lint_ContadorDescuentoAsiento = 0;
                                        lint_ContadorPrimaAsiento = 0;
                                        lint_ContadorPrimaAsientoLargo = 0;
                                        lint_ContadorDescuentoAsientoLargo = 0;
                                 
                                        ldec_ImportesDevengarArribaCorto = 0;
                                        ldec_InteresDevengarArribaCorto = 0;
                                        ldec_PrimaArribaCorto = 0;
                                        ldec_ValorFacialCorto = 0;
                                        ldec_ImportesDevengarArribaLargo = 0;
                                        ldec_InteresDevengarArribaLargo = 0;
                                        ldec_PrimaArribaLargo = 0;
                                        ldec_ValorFacialLargo = 0;

                                        ldas_CompradosCPLP = consulta.ConsultarDinamico("SELECT * FROM [cf].[TitulosValores] WHERE TipoNegociacion = 'Compra' AND  FchValor = '" + lstr_FchCanje.ToString("yyyy.MM.dd") + "' AND Nemotecnico = '" + lstr_NemotecnicoComprados  + "'and IndicadorCupon = 'V'  AND EstadoValor = 'Vigente'");
                                        ldat_CompradosCPLP = ldas_CompradosCPLP.Tables["Table"];

                                        foreach (DataRow row1 in ldat_CompradosCPLP.Rows)
                                        {

                                            ltd_FchVencimiento = Convert.ToDateTime(row1["FchVencimiento"]);
                                            ldec_ValorFacial = Convert.ToDecimal(row1["ValorFacial"]);
                                            lstr_Serie = row1["NroEmisionSerie"].ToString().Trim();

                                            TimeSpan ts3 = ltd_FchVencimiento-lstr_FchCanje;
                                            lint_DiferenciaTrasciende = ts3.Days;

                                            if (lint_DiferenciaTrasciende <= 361)
                                            {
                                                //lstr_Plazo = "CP";
                                                 //ldec_ValorFacialCorto +=ldec_ValorFacial;

                                                ldas_TablaArriba = consulta.ConsultarDinamico("select cr.*, tv.Nemotecnico from cf.CanjeResumenSerie as cr inner join  cf.TitulosValores tv on tv.NroEmisionSerie = cr.NroEmisionSerie and tv.TipoNegociacion = 'Compra' and tv.FchValor = cr.FchCanje WHERE  cr.NroEmisionSerie  = '" + lstr_Serie + "' AND Convert(Varchar(8),FchCanje,112) = Convert(Varchar(8),(Convert(DATETIME,'" + lstr_FchCanje.ToShortDateString() + "',103)),112) AND   RTRIM(REPLACE(IdentificadorCanje,' ','')) = 'C'");
                                                ldat_TablaArriba = ldas_TablaArriba.Tables["Table"];

                                                foreach (DataRow row2 in ldat_TablaArriba.Rows)
                                                {

                                                    ldec_ImportesDevengarArribaCorto += Convert.ToDecimal(row2["InteresBaja"]);
                                                    ldec_InteresDevengarArribaCorto += Convert.ToDecimal(row2["EmisionDarBaja"]);
                                                    ldec_PrimaArribaCorto += Convert.ToDecimal(row2["InteresBajaPrima"]);
                                                    ldec_ValorFacialCorto += Convert.ToDecimal(row2["PorcentajeEmision"]);
                                                }

                                            }
                                            else
                                            {
                                                //lstr_Plazo = "LP";
                                                //ldec_ValorFacialLargo += ldec_ValorFacial;

                                                ldas_TablaArriba = consulta.ConsultarDinamico("select cr.*, tv.Nemotecnico from cf.CanjeResumenSerie as cr inner join  cf.TitulosValores tv on tv.NroEmisionSerie = cr.NroEmisionSerie and tv.TipoNegociacion = 'Compra' and tv.FchValor = cr.FchCanje WHERE  cr.NroEmisionSerie  = '" + lstr_Serie + "' AND Convert(Varchar(8),FchCanje,112) = Convert(Varchar(8),(Convert(DATETIME,'" + lstr_FchCanje.ToShortDateString() + "',103)),112) AND IdentificadorCanje = 'C'");
                                                ldat_TablaArriba = ldas_TablaArriba.Tables["Table"];

                                                foreach (DataRow row2 in ldat_TablaArriba.Rows)
                                                {

                                                    ldec_ImportesDevengarArribaLargo += Convert.ToDecimal(row2["InteresBaja"]);
                                                    ldec_InteresDevengarArribaLargo += Convert.ToDecimal(row2["EmisionDarBaja"]);
                                                    ldec_PrimaArribaLargo += Convert.ToDecimal(row2["InteresBajaPrima"]);
                                                    ldec_ValorFacialLargo += Convert.ToDecimal(row2["PorcentajeEmision"]);
                                                }

                                            }


                                        }
                                       
                                   // }



                                    ldas_Tiras = tus_TipoAsiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_NemotecnicoComprados, lstr_Propietario, "", "LP", "", secuencia, lstr_OrderBy);
                                    ldat_Tira = ldas_Tiras.Tables["Table"];


                                    foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                    {

                                        int index = ldat_Tira.Rows.IndexOf(ldr_Row);
                                        ldec_monto = 0;
                                        string IdClaveContable = ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim();
                                        string Tipo = ldat_Tira.Rows[index]["CodigoAuxiliar2"].ToString().Trim();
                                        

                                        switch (Tipo + IdClaveContable)
                                        {
                                            case "CAPITAL40":
                                                ldec_monto = Math.Abs(ldec_ValorFacialLargo);
                                                break;
                                          
                                            case "IMP_DEV40":
                                                //if (lint_ContadorDescuentoAsientoLargo == 1 || lint_ContadorDescuentoAsientoLargo == 2)
                                                //{
                                                    if (ldec_ImportesDevengarArribaLargo < 0)
                                                    {
                                                        ldec_monto = Math.Abs(ldec_ImportesDevengarArribaLargo);
                                                    }
                                                    else
                                                    {
                                                        ldec_monto = 0;
                                                    }
                                                //}
                                                //lint_ContadorDescuentoAsientoLargo = lint_ContadorDescuentoAsientoLargo + 1;
                                                break;
                                            case "IMP_DEV50":

                                                //if (lint_ContadorDescuentoAsientoLargo == 1 || lint_ContadorDescuentoAsientoLargo == 2)
                                                //{
                                                    if (ldec_ImportesDevengarArribaLargo > 0)
                                                    {
                                                        ldec_monto = Math.Abs(ldec_ImportesDevengarArribaLargo);
                                                    }
                                                    else
                                                    {
                                                        ldec_monto = 0;
                                                    }  
                                                //}
                                                //lint_ContadorDescuentoAsientoLargo = lint_ContadorDescuentoAsientoLargo + 1;

                                                break;
                                            case "INT_DEV40":
                                                ldec_monto = Math.Abs(ldec_InteresDevengarArribaLargo);
                                                break;
                                            case "INT_DEV50":
                                                ldec_monto = 0;
                                                break;
                                            case "PRIMAS40":
                                                //if (lint_ContadorPrimaAsientoLargo == 1 || lint_ContadorPrimaAsientoLargo == 2)
                                                //{
                                                    if (ldec_PrimaArribaLargo > 0)
                                                    {
                                                        ldec_monto = 0;
                                                    }
                                                    else
                                                    {
                                                        ldec_monto = Math.Abs(ldec_PrimaArribaLargo);
                                                    }
                                                //}
                                                //lint_ContadorPrimaAsientoLargo = lint_ContadorPrimaAsientoLargo + 1;
                                                break;
                                            case "PRIMAS50":

                                                //if (lint_ContadorPrimaAsientoLargo == 1 || lint_ContadorPrimaAsientoLargo == 2)
                                                //{
                                                    if (ldec_PrimaArribaLargo > 0)
                                                    {
                                                        ldec_monto = Math.Abs(ldec_PrimaArribaLargo);
                                                    }
                                                    else
                                                    {
                                                        ldec_monto = 0;
                                                    }
                                                //}
                                                //lint_ContadorPrimaAsientoLargo = lint_ContadorPrimaAsientoLargo + 1;
                                                 

                                                break;
                                        }



                                        if (ldec_monto != 0)
                                        {


                                            ldat_Asiento.Rows.Add(
                                        lstr_NumValor + " " + lstr_NemotecnicoComprados,
                                        lstr_FchCanje.ToString("dd.MM.yyyy"),
                                        ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                        "CANJE COLONES",
                                        ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                        Math.Round(ldec_monto, 2),
                                        lstr_NumValor + "." + lstr_Nemotecnico,//pk
                                        tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                        lstr_Moneda,//tipo
                                        lstr_Operacion + "." + lstr_NomOperacion//operacion
                                        );
                                        }


                                    }


                                    ldas_Tiras = tus_TipoAsiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_NemotecnicoComprados, lstr_Propietario, "", lstr_Plazo, "", secuencia, lstr_OrderBy);
                                    ldat_Tira = ldas_Tiras.Tables["Table"];


                                    foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                    {

                                        int index = ldat_Tira.Rows.IndexOf(ldr_Row);
                                        ldec_monto = 0;
                                        string IdClaveContable = ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim();
                                        string Tipo = ldat_Tira.Rows[index]["CodigoAuxiliar2"].ToString().Trim();


                                        switch (Tipo + IdClaveContable)
                                        {
                                            case "CAPITAL40":
                                                ldec_monto = Math.Abs(ldec_ValorFacialCorto);
                                                break;

                                            case "IMP_DEV40":
                                                //if (lint_ContadorDescuentoAsiento == 2 || lint_ContadorDescuentoAsiento == 3)
                                                //{
                                                    if (ldec_ImportesDevengarArribaCorto < 0)
                                                    {
                                                        ldec_monto = Math.Abs(ldec_ImportesDevengarArribaCorto);
                                                    }
                                                    else
                                                    {
                                                        ldec_monto = 0;
                                                    }
                                                //}
                                                //lint_ContadorDescuentoAsiento = lint_ContadorDescuentoAsiento + 1;
                                                break;
                                            case "IMP_DEV50":

                                                //if (lint_ContadorDescuentoAsiento == 1 || lint_ContadorDescuentoAsiento == 2)
                                                //{
                                                    if (ldec_ImportesDevengarArribaCorto > 0)
                                                    {
                                                        ldec_monto = Math.Abs(ldec_ImportesDevengarArribaCorto);
                                                    }
                                                    else
                                                    {
                                                        ldec_monto = 0;
                                                    }
                                                //}
                                                //lint_ContadorDescuentoAsiento = lint_ContadorDescuentoAsiento + 1;

                                                break;
                                            case "INT_DEV40":
                                                ldec_monto = Math.Abs(ldec_InteresDevengarArribaCorto);
                                                break;
                                            case "INT_DEV50":
                                                ldec_monto = 0;
                                                break;
                                            case "PRIMAS40":
                                                //if (lint_ContadorPrimaAsiento == 1 || lint_ContadorPrimaAsiento == 2)
                                                //{
                                                    if (ldec_PrimaArribaCorto > 0)
                                                    {
                                                        ldec_monto = 0;
                                                    }
                                                    else
                                                    {
                                                        ldec_monto = Math.Abs(ldec_PrimaArribaCorto);
                                                    }
                                                //}
                                                //lint_ContadorPrimaAsiento = lint_ContadorPrimaAsiento + 1;
                                                break;
                                            case "PRIMAS50":

                                                //if (lint_ContadorPrimaAsiento == 1 || lint_ContadorPrimaAsiento == 2)
                                                //{
                                                    if (ldec_PrimaArribaCorto > 0)
                                                    {
                                                        ldec_monto = Math.Abs(ldec_PrimaArribaCorto);
                                                    }
                                                    else
                                                    {
                                                        ldec_monto = 0;
                                                    }
                                                //}
                                                //lint_ContadorPrimaAsiento = lint_ContadorPrimaAsiento + 1;


                                                break;
                                        }



                                        if (ldec_monto != 0)
                                        {


                                            ldat_Asiento.Rows.Add(
                                        lstr_NumValor + " " + lstr_NemotecnicoComprados,
                                        lstr_FchCanje.ToString("dd.MM.yyyy"),
                                        ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                        "CANJE COLONES",
                                        ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                        Math.Round(ldec_monto, 2),
                                        lstr_NumValor + "." + lstr_Nemotecnico,//pk
                                        tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                        lstr_Moneda,//tipo
                                        lstr_Operacion + "." + lstr_NomOperacion//operacion
                                        );

                                        }


                                    }

                                     }//-------------    NUEVA
                                    //Normales

                                    //T

                                    lstr_Monto = string.Empty;
                                    lds_Datos = new DataTable();
                                    ldec_MontoTotal = 0;
                                    reservasError = "";
                                    lstr_NuevoPosPrePago = string.Empty;
                                    ldat_Reservas = new DataSet();

                                    ldas_Tiras = tus_TipoAsiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "", "", "", secuencia, lstr_OrderBy);
                                    ldat_Tira = ldas_Tiras.Tables["Table"];
                                    strExprTitulo = (lstr_Nemotecnico.StartsWith("PT")) ? " ([CodigoAuxiliar2] = 'PT') " : " ([CodigoAuxiliar2] = 'TL_D') ";
                                    ldat_Tira = ldat_Tira.Select(strExprTitulo).CopyToDataTable();




                                    //if (lint_DiferenciaPlazoValor <= 361)
                                    //{
                                    //    if (Diferencia < 0)
                                    //    {
                                    //        strExprTitulo = " ([CodigoAuxiliar2] = 'NT_P') ";
                                    //        ldat_Tira = ldat_Tira.Select(strExprTitulo).CopyToDataTable();
                                    //    }
                                    //    else
                                    //    {
                                    //        strExprTitulo = " ([CodigoAuxiliar2] = 'NT_D') ";
                                    //        ldat_Tira = ldat_Tira.Select(strExprTitulo).CopyToDataTable();
                                    //    }

                                    //}
                                    //else
                                    //{
                                    //    if (ldec_ValorFacial - ldec_ValorTransadoBruto >= 0)
                                    //    {
                                    //        if (Diferencia < 0)
                                    //        {
                                    //            strExprTitulo = " ([CodigoAuxiliar2] = 'TC_P') ";
                                    //            ldat_Tira = ldat_Tira.Select(strExprTitulo).CopyToDataTable();
                                    //        }
                                    //        else
                                    //        {
                                    //            strExprTitulo = " ([CodigoAuxiliar2] = 'TC_D') ";
                                    //            ldat_Tira = ldat_Tira.Select(strExprTitulo).CopyToDataTable();
                                    //        }

                                    //    }
                                    //    else
                                    //    {
                                    //        if (Diferencia < 0)
                                    //        {
                                    //            strExprTitulo = " ([CodigoAuxiliar2] = 'TL_P') ";
                                    //            ldat_Tira = ldat_Tira.Select(strExprTitulo).CopyToDataTable();
                                    //        }
                                    //        else
                                    //        {
                                                
                                    //        }

                                    //    }
                                    //}

                                    foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                    {
                                        int index = ldat_Tira.Rows.IndexOf(ldr_Row);
                                        ldec_monto = ldec_Diferencia;


                                        ldat_Reservas = consulta.ConsultarDinamico("select * from ma.reservasdetalle where idcuentacontable = '" + ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim() + "' and idpospre = '" + ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim() + "' AND IsNull(OrdenDeudaInterna,0) != 0  and LEFT(idprograma, 4) = year(getdate()) Order by OrdenDeudaInterna ASC ");
                                        //ldat_Reservas = reservas.ConsultarReservasDetallado(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(), string.Empty, ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(), string.Empty, string.Empty, string.Empty, string.Empty);
                                        if (ldat_Reservas.Tables[0].Rows.Count != 0)
                                        {
                                            DataView dv = ldat_Reservas.Tables[0].DefaultView;
                                            dv.Sort = "OrdenDeudaInterna ASC";

                                            lds_Datos.Columns.Add("IdReserva");
                                            lds_Datos.Columns.Add("OrdenDeudaInterna");
                                            lds_Datos.Columns.Add("IdPosPre");
                                            lds_Datos.Columns.Add("Posicion");
                                            lds_Datos.Columns.Add("Monto");

                                            foreach (DataRow drForm in dv.ToTable().Rows)
                                            {
                                                //if (drForm["IdMoneda"].ToString().Trim().Equals(ldat_AsientoPago.Rows[0]["CodigoAuxiliar"].ToString().Trim()))
                                                if (!drForm["OrdenDeudaInterna"].ToString().Equals(string.Empty) && !drForm["OrdenDeudaInterna"].ToString().Equals("0"))
                                                {
                                                    lstr_Monto = asientos.ConsultaMontoReservaSAP(drForm["IdReserva"].ToString().Trim(), drForm["Posicion"].ToString().Trim());
                                                    lstr_Monto = lstr_Monto.Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal);
                                                    if (Convert.ToDecimal(lstr_Monto) > 0)
                                                    {
                                                        lds_Datos.Rows.Add(
                                                            drForm["IdReserva"].ToString(),
                                                            drForm["OrdenDeudaInterna"].ToString(),
                                                            drForm["IdPosPre"].ToString(),
                                                            drForm["Posicion"].ToString(),
                                                            lstr_Monto);
                                                        reservasError += "Posición Presupuestaria: " + drForm["IdPosPre"].ToString().Trim() + "Reserva :" + drForm["IdReserva"].ToString().Trim() + ", Posición: " + drForm["Posicion"].ToString().Trim() + "\n";
                                                        ldec_MontoTotal += Convert.ToDecimal(lstr_Monto);
                                                    }
                                                }
                                            }

                                            if (Convert.ToDecimal(ldec_MontoTotal) >= (Math.Abs(ldec_monto) * ldec_TipoCambio))
                                            {
                                                //Genera el asiento
                                                decimal ldec_Saldo = ldec_monto;

                                                foreach (DataRow drForm in lds_Datos.Rows)
                                                {
                                                    if (Convert.ToDecimal(drForm["Monto"]) > 0 && ldec_Saldo >= 0)
                                                    {
                                                        ldat_Asiento.Rows.Add(
                                                        lstr_NumValor + " " + lstr_NemotecnicoComprados,
                                                        lstr_FchCanje.ToString("dd.MM.yyyy"),
                                                        ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                                        "CANJE COLONES",
                                                        ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                                        drForm["IdReserva"].ToString().Trim(),
                                                        drForm["Posicion"].ToString().Trim(),
                                                        Truncate(ldec_Saldo > Convert.ToDecimal(drForm["Monto"].ToString()) ? Convert.ToDecimal(drForm["Monto"].ToString()) : ldec_Saldo, 2),
                                                        lstr_NumValor + "." + lstr_Nemotecnico,//pk
                                                        tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                                        lstr_Moneda,//tipo
                                                        lstr_Operacion +"." + lstr_NomOperacion//operacion
                                                        );


                                                    }

                                                    //Resta el saldo    
                                                    ldec_Saldo = ldec_Saldo - Convert.ToDecimal(drForm["Monto"]);
                                                }
                                            }
                                            else
                                            {
                                                //Almacena en bitácora de que no lo hizo

                                                bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion, "DI"), "Resultado de Contabilización: \n 1 - [E] Monto superior al total de las reservas de la Deuda Interna. Reservas utilizadas: \n" + reservasError, lstr_Operacion, lstr_NumValor + "-" + lstr_Nemotecnico, "G206");
                                                SinError = false;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            if (!ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim().StartsWith("E"))
                                            {
                                                ldat_Asiento.Rows.Add(
                                                lstr_NumValor + " " + lstr_NemotecnicoComprados,
                                                lstr_FchCanje.ToString("dd.MM.yyyy"),
                                                ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                                "CANJE DOLARES",
                                                ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                                Truncate(ldec_monto, 2),
                                                lstr_NumValor + "." + lstr_Nemotecnico,//pk
                                                tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                                lstr_Moneda,//tipo
                                                lstr_Operacion +"." + lstr_NomOperacion//operacion
                                                );


                                            }
                                            else
                                            {
                                                bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion, "DI"), "Resultado de Contabilización: \n 1 - [E] No hay reservas correspondientes a la cuenta " + ldat_Tira.Rows[0]["IdCuentaContable"].ToString().Trim() + " con fondo " + ldat_Tira.Rows[0]["IdPosPre"].ToString().Trim(), lstr_Operacion, lstr_NumValor + "-" + lstr_Nemotecnico, "G206");
                                                SinError = false;
                                                break;
                                            }

                                        }

                                    }

                                    //T


                                    #endregion
                                }
                                else
                                {
                                    #region ID64
                                    lstr_Operacion = "ID64";

                                    DataSet lds_Operaciones = loperacion.ConsultarOperaciones(lstr_Operacion, "IdModulo IN ('DI')", "");
                                    if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
                                    {
                                        lstr_NomOperacion = lds_Operaciones.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
                                    }
                                    var strExprTitulo = "";
                                    string lstr_Monto = string.Empty;
                                    DataTable lds_Datos = new DataTable();
                                    decimal ldec_MontoTotal = 0;
                                    string reservasError = "";
                                    string lstr_NuevoPosPrePago = string.Empty;
                                    DataSet ldat_Reservas = new DataSet();
                                    // Vacias
                                    string cod_aux2 = (lstr_Nemotecnico.StartsWith("PT") ? "PT" : "");
                                    ldas_Tiras = tus_TipoAsiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "", "", "", secuencia, lstr_OrderBy);
                                    ldat_Tira = ldas_Tiras.Tables["Table"];
                                    strExprTitulo = " ([CodigoAuxiliar2] = '') AND [IdClaveContable]  = '" + lstr_IdClaveContable + "' ";
                                    ldat_Tira = ldat_Tira.Select(strExprTitulo).CopyToDataTable();

                                    if (lint_ContadorPago == 0)
                                    {
                                        lint_ContadorPago = lint_ContadorPago + 1;

                                        foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                        {
                                            int index = ldat_Tira.Rows.IndexOf(ldr_Row);
                                            ldec_monto = ldec_DiferenciaTransados;

                                            ldat_Asiento.Rows.Add(
                                                lstr_NumValor + " " + lstr_Nemotecnico,
                                                lstr_FchCanje.ToString("dd.MM.yyyy"),
                                                ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                                "CANJE DOLARES",
                                                ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                                Math.Round(ldec_monto, 2),
                                                lstr_NumValor + "." + lstr_Nemotecnico,//pk
                                                tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                                lstr_Moneda,//tipo
                                                lstr_Operacion +"." + lstr_NomOperacion//operacion
                                                );
                                        }
                                    }



                                    // Vacias


                                    // Normales

                                    //Colocacion
                                    ldas_Nemotecnico = consulta.ConsultarDinamico("SELECT DISTINCT(Nemotecnico) FROM  [cf].[TitulosCanjeSubasta] WHERE FchCanje = '" + lstr_FchCanje.ToString("yyyy.MM.dd") + "'");
                                    ldat_Nemotecnico = ldas_Nemotecnico.Tables["Table"];
                                    string lstr_NemotecnicoColocados;
                                    foreach (DataRow row in ldat_Nemotecnico.Rows)
                                    {
                                        lstr_NemotecnicoColocados = row["Nemotecnico"].ToString().Trim();
                                        ldec_DiferenciaPrimaColocados = 0;
                                        ldec_CupoCorridoColocadoDescuento = 0;
                                        ldec_TotalValorFacialColocado = 0;
                                        ldec_DescuentoColocados = 0;
                                        ldec_PrimasColocados = 0;
                                        lint_ContadorDescuentoAsiento = 0;
                                        lint_ContadorPrimaAsiento = 0;
                                        lint_ContadorCuponAsiento = 0;
                                        ldas_Colocados = consulta.ConsultarDinamico("SELECT * FROM  [cf].[TitulosCanjeSubasta] WHERE FchCanje = '" + lstr_FchCanje.ToString("yyyy.MM.dd") + "' AND Nemotecnico ='" + lstr_NemotecnicoColocados + "'");
                                        ldat_Colocados = ldas_Colocados.Tables["Table"];



                                        foreach (DataRow row1 in ldat_Colocados.Rows)
                                        {
                                            lstr_NroValorColocado = row1["NroValor"].ToString().Trim();
                                            lstr_Nemotecnico = row1["Nemotecnico"].ToString().Trim();

                                            ldas_ColocadosCanje = consulta.ConsultarDinamico("SELECT * FROM [cf].[TitulosValores] WHERE [NroValor] = '" + lstr_NroValorColocado + "' AND [Nemotecnico] = '" + lstr_Nemotecnico + "' AND EstadoValor = 'Vigente' AND IndicadorCupon = 'V' AND FchValor = '" + lstr_FchCanje.ToString("yyyy.MM.dd") + "'");
                                            ldat_ColocadosCanje = ldas_ColocadosCanje.Tables["Table"];

                                            foreach (DataRow row2 in ldat_ColocadosCanje.Rows)
                                            {
                                                ldec_ValorFacialColocado = Convert.ToDecimal(row2["ValorFacial"]);
                                                ldec_TransadoNetoColocado = Convert.ToDecimal(row2["ValorTransadoNeto"]);
                                                ldec_TransadoBrutoColocado = Convert.ToDecimal(row2["ValorTransadoBruto"]);
                                            }

                                            esDescuento = ldec_ValorFacial >= ldec_ValorTransadoBruto;
                                            ldec_DiferenciaPrimaColocados = ldec_ValorFacialColocado - ldec_TransadoBrutoColocado;
                                            ldec_DiferenciaCupoCorridoColocado = ldec_TransadoNetoColocado - ldec_TransadoBrutoColocado;
                                            ldec_TotalValorFacialColocado += ldec_ValorFacialColocado;

                                            if (ldec_DiferenciaPrimaColocados > 0)
                                            {
                                                ldec_DescuentoColocados += ldec_DiferenciaPrimaColocados;
                                                //ldec_CupoCorridoColocadoDescuento += ldec_DiferenciaCupoCorridoColocado;
                                            }
                                            if (ldec_DiferenciaPrimaColocados < 0)
                                            {
                                                ldec_PrimasColocados += ldec_DiferenciaPrimaColocados;
                                                //ldec_CupoCorridoColocadoPrima += ldec_DiferenciaCupoCorridoColocado;
                                                //ldec_PrimasColocados = ldec_PrimasColocados;
                                            }

                                            ldec_CupoCorridoColocadoDescuento += ldec_DiferenciaCupoCorridoColocado;

                                        }

                                        ldas_ColocadosCapital = consulta.ConsultarDinamico("SELECT TOP 1 tcs.Nemotecnico, count (tv.Nemotecnico ) As NumeroVeces FROM (SELECT DISTINCT Nemotecnico, fchcanje FROM [cf].[TitulosCanjeSubasta]) tcs LEFT OUTER JOIN [cf].[TitulosValores] tv ON tv.Nemotecnico = tcs.Nemotecnico AND tv.FchValor = tcs.FchCanje AND TV.IndicadorCupon = 'V' AND TV.TipoNegociacion = 'Compra' WHERE tcs.FchCanje = '" + lstr_FchCanje.ToString("yyyy.MM.dd") + "' GROUP BY tcs.Nemotecnico ORDER BY count (tv.Nemotecnico ) ASC ");
                                        ldat_ColocadosCapital = ldas_ColocadosCapital.Tables["Table"];
                                        string lstr_NumeroNemotecnicoColocado = "";

                                        int lint_NemotecnicoNumeroVeces = 0;
                                        foreach (DataRow row3 in ldat_ColocadosCapital.Rows)
                                        {
                                            lint_NemotecnicoNumeroVeces = Convert.ToInt32(row3["NumeroVeces"]);
                                            lstr_NumeroNemotecnicoColocado = row3["Nemotecnico"].ToString().Trim();

                                        }





                                        ldec_DiferenciaValorFacial = ldec_ValorFacialColocado - ldec_ValorFacial;
                                        ldec_DiferenciaValorFacial = Math.Abs(ldec_DiferenciaValorFacial);

                                        ldas_Tiras = tus_TipoAsiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_NemotecnicoColocados, lstr_Propietario, "", lstr_Plazo, "", secuencia, lstr_OrderBy);
                                        ldat_Tira = ldas_Tiras.Tables["Table"];



                                        foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                        {

                                            int index = ldat_Tira.Rows.IndexOf(ldr_Row);
                                            ldec_monto = 0;
                                            string IdClaveContable = ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim();
                                            string Tipo = ldat_Tira.Rows[index]["CodigoAuxiliar2"].ToString().Trim();
                                            string Pospre = ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim();
                                            string l_strLetra = Pospre.Substring(0, 1);



                                            switch (Tipo + IdClaveContable)
                                            {

                                                case "CAPITAL50":

                                                    if (l_strLetra == "E" && lstr_NumeroNemotecnicoColocado == lstr_Nemotecnico)
                                                    {
                                                        ldec_monto = ldec_DiferenciaCompradosColocados;
                                                        ldec_monto = Math.Abs(ldec_monto);

                                                    }
                                                    else
                                                    {
                                                        if (lstr_NumeroNemotecnicoColocado == lstr_Nemotecnico)
                                                        {
                                                            //if (ldec_TotalValorFacialColocado > ldec_DiferenciaCompradosColocados)
                                                            //{
                                                                ldec_monto = ldec_TotalValorFacialColocado;// -ldec_DiferenciaCompradosColocados;
                                                                ldec_monto = Math.Abs(ldec_monto);
                                                            //}

                                                        }
                                                        else
                                                        {
                                                            if (l_strLetra != "E")
                                                            {
                                                                if (lint_ContadorCapitalAsiento == 0)
                                                                {
                                                                    ldec_monto = ldec_TotalValorFacialColocado;
                                                                    ldec_monto = Math.Abs(ldec_monto);
                                                                    lint_ContadorCapitalAsiento = 1;
                                                                }
                                                            }

                                                        }
                                                    }



                                                    break;
                                                case "IMP_DEV40":

                                                    if (ldec_DiferenciaPrimaColocados == 0)
                                                    {
                                                        ldec_monto = 0;
                                                    }
                                                    else
                                                    {
                                                        if (lint_ContadorDescuentoAsiento == 0)
                                                        {
                                                            ldec_monto = ldec_DescuentoColocados;
                                                            lint_ContadorDescuentoAsiento = 1;
                                                        }

                                                    }
                                                    break;


                                                case "IMP_DEV50":

                                                    if (lint_ContadorCuponAsiento == 0)
                                                    {
                                                        ldec_monto = ldec_CupoCorridoColocadoDescuento;
                                                        ldec_monto = Math.Abs(ldec_monto);
                                                        lint_ContadorCuponAsiento = 1;
                                                    }


                                                    if (!esDescuento)
                                                    {
                                                        for (int iRow = 0; iRow < ldat_Tira.Rows.Count; iRow++)
                                                        {
                                                            string IdClaveContable2 = ldat_Tira.Rows[iRow]["IdClaveContable"].ToString().Trim();
                                                            string Tipo2 = ldat_Tira.Rows[iRow]["CodigoAuxiliar2"].ToString().Trim();
                                                            if ((Tipo2 + IdClaveContable2).Equals("PRIMAS50"))
                                                            {
                                                                index = iRow;
                                                            }
                                                        }
                                                    }

                                                    break;

                                                case "PRIMAS50":


                                                    if (ldec_DiferenciaPrimaColocados == 0)
                                                    {
                                                        ldec_monto = 0;
                                                    }
                                                    else
                                                    {
                                                        if (lint_ContadorPrimaAsiento == 0)
                                                        {
                                                            ldec_monto = Math.Abs(ldec_PrimasColocados);
                                                            lint_ContadorPrimaAsiento = 1;
                                                        }

                                                    }

                                                    break;

                                            }

                                            if (ldec_monto != 0)
                                            {


                                                ldat_Asiento.Rows.Add(
                                            lstr_NumValor + " " + lstr_NemotecnicoColocados,
                                            lstr_FchCanje.ToString("dd.MM.yyyy"),
                                            ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                            "CANJE COLONES",
                                            ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                            Math.Round(ldec_monto, 2),
                                            lstr_NumValor + "." + lstr_Nemotecnico,//pk
                                            tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                            lstr_Moneda,//tipo
                                            lstr_Operacion +"." + lstr_NomOperacion//operacion
                                            );

                                            }
                                        }
                                    }


                                    //Colocacion

                                    //Compras

                                    ldas_CompradosCanje = consulta.ConsultarDinamico("SELECT DISTINCT (Nemotecnico) FROM [cf].[TitulosValores] WHERE TipoNegociacion = 'Compra' AND  FchValor = '" + lstr_FchCanje.ToString("yyyy.MM.dd") + "' and IndicadorCupon = 'V'  AND EstadoValor = 'Vigente'");
                                    ldat_CompradosCanje = ldas_CompradosCanje.Tables["Table"];

                                    foreach (DataRow row in ldat_CompradosCanje.Rows)
                                    {
                                        lstr_NemotecnicoComprados = row["Nemotecnico"].ToString().Trim();
                                        lint_ContadorDescuentoAsiento = 0;
                                        lint_ContadorPrimaAsiento = 0;
                                        lint_ContadorPrimaAsientoLargo = 0;
                                        lint_ContadorDescuentoAsientoLargo = 0;
                                        ldec_ImportesDevengarArribaCorto = 0;
                                        ldec_InteresDevengarArribaCorto = 0;
                                        ldec_PrimaArribaCorto = 0;
                                        ldec_ValorFacialCorto = 0;
                                        ldec_ImportesDevengarArribaLargo = 0;
                                        ldec_InteresDevengarArribaLargo = 0;
                                        ldec_PrimaArribaLargo = 0;
                                        ldec_ValorFacialLargo = 0;

                                        ldas_CompradosCPLP = consulta.ConsultarDinamico("SELECT * FROM [cf].[TitulosValores] WHERE TipoNegociacion = 'Compra' AND  FchValor = '" + lstr_FchCanje.ToString("yyyy.MM.dd") + "' AND Nemotecnico = '" + lstr_NemotecnicoComprados + "'and IndicadorCupon = 'V'  AND EstadoValor = 'Vigente'");
                                        ldat_CompradosCPLP = ldas_CompradosCPLP.Tables["Table"];

                                        foreach (DataRow row1 in ldat_CompradosCPLP.Rows)
                                        {

                                            ltd_FchVencimiento = Convert.ToDateTime(row1["FchVencimiento"]);
                                            ldec_ValorFacial = Convert.ToDecimal(row1["ValorFacial"]);
                                            lstr_Serie = row1["NroEmisionSerie"].ToString().Trim();

                                            TimeSpan ts3 = ltd_FchVencimiento - lstr_FchCanje;
                                            lint_DiferenciaTrasciende = ts3.Days;

                                            if (lint_DiferenciaTrasciende <= 361)
                                            {
                                                //lstr_Plazo = "CP";
                                                //ldec_ValorFacialCorto +=ldec_ValorFacial;

                                                ldas_TablaArriba = consulta.ConsultarDinamico("select cr.*, tv.Nemotecnico from cf.CanjeResumenSerie as cr inner join  cf.TitulosValores tv on tv.NroEmisionSerie = cr.NroEmisionSerie and tv.TipoNegociacion = 'Compra' and tv.FchValor = cr.FchCanje WHERE  cr.NroEmisionSerie  = '" + lstr_Serie + "' AND Convert(Varchar(8),FchCanje,112) = Convert(Varchar(8),(Convert(DATETIME,'" + lstr_FchCanje.ToShortDateString() + "',103)),112) AND IdentificadorCanje = 'C'");
                                                ldat_TablaArriba = ldas_TablaArriba.Tables["Table"];

                                                foreach (DataRow row2 in ldat_TablaArriba.Rows)
                                                {

                                                    ldec_ImportesDevengarArribaCorto += Convert.ToDecimal(row2["InteresBaja"]);
                                                    ldec_InteresDevengarArribaCorto += Convert.ToDecimal(row2["EmisionDarBaja"]);
                                                    ldec_PrimaArribaCorto += Convert.ToDecimal(row2["InteresBajaPrima"]);
                                                    ldec_ValorFacialCorto += Convert.ToDecimal(row2["PorcentajeEmision"]);
                                                }

                                            }
                                            else
                                            {
                                                //lstr_Plazo = "LP";
                                                //ldec_ValorFacialLargo += ldec_ValorFacial;

                                                ldas_TablaArriba = consulta.ConsultarDinamico("select cr.*, tv.Nemotecnico from cf.CanjeResumenSerie as cr inner join  cf.TitulosValores tv on tv.NroEmisionSerie = cr.NroEmisionSerie and tv.TipoNegociacion = 'Compra' and tv.FchValor = cr.FchCanje WHERE  cr.NroEmisionSerie  = '" + lstr_Serie + "' AND Convert(Varchar(8),FchCanje,112) = Convert(Varchar(8),(Convert(DATETIME,'" + lstr_FchCanje.ToShortDateString() + "',103)),112) AND IdentificadorCanje = 'C'");
                                                ldat_TablaArriba = ldas_TablaArriba.Tables["Table"];

                                                foreach (DataRow row2 in ldat_TablaArriba.Rows)
                                                {

                                                    ldec_ImportesDevengarArribaLargo += Convert.ToDecimal(row2["InteresBaja"]);
                                                    ldec_InteresDevengarArribaLargo += Convert.ToDecimal(row2["EmisionDarBaja"]);
                                                    ldec_PrimaArribaLargo += Convert.ToDecimal(row2["InteresBajaPrima"]);
                                                    ldec_ValorFacialLargo += Convert.ToDecimal(row2["PorcentajeEmision"]);
                                                }

                                            }


                                        }

                                        // }



                                        ldas_Tiras = tus_TipoAsiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_NemotecnicoComprados, lstr_Propietario, "", lstr_Plazo, "", secuencia, lstr_OrderBy);
                                        ldat_Tira = ldas_Tiras.Tables["Table"];


                                        foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                        {

                                            int index = ldat_Tira.Rows.IndexOf(ldr_Row);
                                            ldec_monto = 0;
                                            string IdClaveContable = ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim();
                                            string Tipo = ldat_Tira.Rows[index]["CodigoAuxiliar2"].ToString().Trim();


                                            switch (Tipo + IdClaveContable)
                                            {
                                                case "CAPITAL40":
                                                    ldec_monto = Math.Abs(ldec_ValorFacialLargo);
                                                    break;

                                                case "IMP_DEV40":
                                                    //if (lint_ContadorDescuentoAsientoLargo == 1 || lint_ContadorDescuentoAsientoLargo == 2)
                                                    //{
                                                        if (ldec_ImportesDevengarArribaLargo < 0)
                                                        {
                                                            ldec_monto = Math.Abs(ldec_ImportesDevengarArribaLargo);
                                                        }
                                                        else
                                                        {
                                                            ldec_monto = 0;
                                                        }
                                                    //}
                                                    //lint_ContadorDescuentoAsientoLargo = lint_ContadorDescuentoAsientoLargo + 1;
                                                    break;
                                                case "IMP_DEV50":

                                                    //if (lint_ContadorDescuentoAsientoLargo == 1 || lint_ContadorDescuentoAsientoLargo == 2)
                                                    //{
                                                        if (ldec_ImportesDevengarArribaLargo > 0)
                                                        {
                                                            ldec_monto = Math.Abs(ldec_ImportesDevengarArribaLargo);
                                                        }
                                                        else
                                                        {
                                                            ldec_monto = 0;
                                                        }
                                                    //}
                                                    //lint_ContadorDescuentoAsientoLargo = lint_ContadorDescuentoAsientoLargo + 1;

                                                    break;
                                                case "INT_DEV40":
                                                    ldec_monto = Math.Abs(ldec_InteresDevengarArribaLargo);
                                                    break;
                                                case "INT_DEV50":
                                                    ldec_monto = 0;
                                                    break;
                                                case "PRIMAS40":
                                                    //if (lint_ContadorPrimaAsientoLargo == 1 || lint_ContadorPrimaAsientoLargo == 2)
                                                    //{
                                                    if (ldec_PrimaArribaLargo > 0)
                                                    {
                                                        ldec_monto = 0;
                                                    }
                                                    else
                                                    {
                                                        ldec_monto = Math.Abs(ldec_PrimaArribaLargo);
                                                    }
                                                    //}
                                                    //lint_ContadorPrimaAsientoLargo = lint_ContadorPrimaAsientoLargo + 1;
                                                    break;
                                                case "PRIMAS50":

                                                    //if (lint_ContadorPrimaAsientoLargo == 1 || lint_ContadorPrimaAsientoLargo == 2)
                                                    //{
                                                    if (ldec_PrimaArribaLargo > 0)
                                                    {
                                                        ldec_monto = Math.Abs(ldec_PrimaArribaLargo);
                                                    }
                                                    else
                                                    {
                                                        ldec_monto = 0;
                                                    }
                                                    //}
                                                    //lint_ContadorPrimaAsientoLargo = lint_ContadorPrimaAsientoLargo + 1;


                                                    break;
                                            }



                                            if (ldec_monto != 0)
                                            {


                                                ldat_Asiento.Rows.Add(
                                            lstr_NumValor + " " + lstr_NemotecnicoComprados,
                                            lstr_FchCanje.ToString("dd.MM.yyyy"),
                                            ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                            "CANJE COLONES",
                                            ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                            Math.Round(ldec_monto, 2),
                                            lstr_NumValor + "." + lstr_Nemotecnico,//pk
                                            tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                            lstr_Moneda,//tipo
                                            lstr_Operacion + "." + lstr_NomOperacion//operacion
                                            );

                                            }


                                        }


                                        ldas_Tiras = tus_TipoAsiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_NemotecnicoComprados, lstr_Propietario, "", "CP", "", secuencia, lstr_OrderBy);
                                        ldat_Tira = ldas_Tiras.Tables["Table"];


                                        foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                        {

                                            int index = ldat_Tira.Rows.IndexOf(ldr_Row);
                                            ldec_monto = 0;
                                            string IdClaveContable = ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim();
                                            string Tipo = ldat_Tira.Rows[index]["CodigoAuxiliar2"].ToString().Trim();


                                            switch (Tipo + IdClaveContable)
                                            {
                                                case "CAPITAL40":
                                                    ldec_monto = Math.Abs(ldec_ValorFacialCorto);
                                                    break;

                                                case "IMP_DEV40":
                                                    //if (lint_ContadorDescuentoAsiento == 2 || lint_ContadorDescuentoAsiento == 3)
                                                    //{
                                                        if (ldec_ImportesDevengarArribaCorto < 0)
                                                        {
                                                            ldec_monto = Math.Abs(ldec_ImportesDevengarArribaCorto);
                                                        }
                                                        else
                                                        {
                                                            ldec_monto = 0;
                                                        }
                                                    //}
                                                    //lint_ContadorDescuentoAsiento = lint_ContadorDescuentoAsiento + 1;
                                                    break;
                                                case "IMP_DEV50":

                                                    //if (lint_ContadorDescuentoAsiento == 1 || lint_ContadorDescuentoAsiento == 2)
                                                    //{
                                                        if (ldec_ImportesDevengarArribaCorto > 0)
                                                        {
                                                            ldec_monto = Math.Abs(ldec_ImportesDevengarArribaCorto);
                                                        }
                                                        else
                                                        {
                                                            ldec_monto = 0;
                                                        }
                                                    //}
                                                    //lint_ContadorDescuentoAsiento = lint_ContadorDescuentoAsiento + 1;

                                                    break;
                                                case "INT_DEV40":
                                                    ldec_monto = Math.Abs(ldec_InteresDevengarArribaCorto);
                                                    break;
                                                case "INT_DEV50":
                                                    ldec_monto = 0;
                                                    break;
                                                case "PRIMAS40":
                                                    //if (lint_ContadorPrimaAsiento == 1 || lint_ContadorPrimaAsiento == 2)
                                                    //{
                                                    if (ldec_PrimaArribaCorto > 0)
                                                    {
                                                        ldec_monto = 0;
                                                    }
                                                    else
                                                    {
                                                        ldec_monto = Math.Abs(ldec_PrimaArribaCorto);
                                                    }
                                                    //}
                                                    //lint_ContadorPrimaAsiento = lint_ContadorPrimaAsiento + 1;
                                                    break;
                                                case "PRIMAS50":

                                                    //if (lint_ContadorPrimaAsiento == 1 || lint_ContadorPrimaAsiento == 2)
                                                    //{
                                                    if (ldec_PrimaArribaCorto > 0)
                                                    {
                                                        ldec_monto = Math.Abs(ldec_PrimaArribaCorto);
                                                    }
                                                    else
                                                    {
                                                        ldec_monto = 0;
                                                    }
                                                    //}
                                                    //lint_ContadorPrimaAsiento = lint_ContadorPrimaAsiento + 1;


                                                    break;
                                            }



                                            if (ldec_monto != 0)
                                            {


                                                ldat_Asiento.Rows.Add(
                                            lstr_NumValor + " " + lstr_NemotecnicoComprados,
                                            lstr_FchCanje.ToString("dd.MM.yyyy"),
                                            ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                            "CANJE COLONES",
                                            ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                            Math.Round(ldec_monto, 2),
                                            lstr_NumValor + "." + lstr_Nemotecnico,//pk
                                            tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                            lstr_Moneda,//tipo
                                            lstr_Operacion + "." + lstr_NomOperacion //operacion
                                            );

                                            }


                                        }
                                    }//-------------


                                    //Normales

                                    //T

                                    ldas_Tiras = tus_TipoAsiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "", "", "", secuencia, lstr_OrderBy);
                                    ldat_Tira = ldas_Tiras.Tables["Table"];
                                    strExprTitulo = (lstr_Nemotecnico.StartsWith("PT")) ? " ([CodigoAuxiliar2] = 'PT') " : " ([CodigoAuxiliar2] = 'TL_D') ";
                                    ldat_Tira = ldat_Tira.Select(strExprTitulo).CopyToDataTable();



                                    foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                    {
                                        int index = ldat_Tira.Rows.IndexOf(ldr_Row);
                                        ldec_monto = ldec_Diferencia;


                                        ldat_Reservas = consulta.ConsultarDinamico("select * from ma.reservasdetalle where idcuentacontable = '" + ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim() + "' and idpospre = '" + ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim() + "' AND IsNull(OrdenDeudaInterna,0) != 0  and LEFT(idprograma, 4) = year(getdate()) Order by OrdenDeudaInterna ASC");
                                        //ldat_Reservas = reservas.ConsultarReservasDetallado(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(), string.Empty, ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(), string.Empty, string.Empty, string.Empty, string.Empty);
                                        if (ldat_Reservas.Tables[0].Rows.Count != 0)
                                        {
                                            DataView dv = ldat_Reservas.Tables[0].DefaultView;
                                            dv.Sort = "OrdenDeudaInterna ASC";

                                            lds_Datos.Columns.Add("IdReserva");
                                            lds_Datos.Columns.Add("OrdenDeudaInterna");
                                            lds_Datos.Columns.Add("IdPosPre");
                                            lds_Datos.Columns.Add("Posicion");
                                            lds_Datos.Columns.Add("Monto");

                                            foreach (DataRow drForm in dv.ToTable().Rows)
                                            {
                                                //if (drForm["IdMoneda"].ToString().Trim().Equals(ldat_AsientoPago.Rows[0]["CodigoAuxiliar"].ToString().Trim()))
                                                if (!drForm["OrdenDeudaInterna"].ToString().Equals(string.Empty) && !drForm["OrdenDeudaInterna"].ToString().Equals("0"))
                                                {
                                                    lstr_Monto = asientos.ConsultaMontoReservaSAP(drForm["IdReserva"].ToString().Trim(), drForm["Posicion"].ToString().Trim());
                                                    lstr_Monto = lstr_Monto.Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal);
                                                    if (Convert.ToDecimal(lstr_Monto) > 0)
                                                    {
                                                        lds_Datos.Rows.Add(
                                                            drForm["IdReserva"].ToString(),
                                                            drForm["OrdenDeudaInterna"].ToString(),
                                                            drForm["IdPosPre"].ToString(),
                                                            drForm["Posicion"].ToString(),
                                                            lstr_Monto);
                                                        reservasError += "Posición Presupuestaria: " + drForm["IdPosPre"].ToString().Trim() + "Reserva :" + drForm["IdReserva"].ToString().Trim() + ", Posición: " + drForm["Posicion"].ToString().Trim() + "\n";
                                                        ldec_MontoTotal += Convert.ToDecimal(lstr_Monto);
                                                    }
                                                }
                                            }

                                            if (Convert.ToDecimal(ldec_MontoTotal) >= (Math.Abs(ldec_monto) * ldec_TipoCambio))
                                            {
                                                //Genera el asiento
                                                decimal ldec_Saldo = ldec_monto;

                                                foreach (DataRow drForm in lds_Datos.Rows)
                                                {
                                                    if (Convert.ToDecimal(drForm["Monto"]) > 0 && ldec_Saldo >= 0)
                                                    {
                                                        ldat_Asiento.Rows.Add(
                                                        lstr_NumValor + " " + lstr_Nemotecnico,
                                                        lstr_FchCanje.ToString("dd.MM.yyyy"),
                                                        ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                                        "CANJE GASTO DOLARES",
                                                        ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                                        drForm["IdReserva"].ToString().Trim(),
                                                        drForm["Posicion"].ToString().Trim(),
                                                        Truncate(ldec_Saldo > Convert.ToDecimal(drForm["Monto"].ToString()) ? Convert.ToDecimal(drForm["Monto"].ToString()) : ldec_Saldo, 2),
                                                        lstr_NumValor + "." + lstr_Nemotecnico,//pk
                                                        tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                                        lstr_Moneda,//tipo
                                                        lstr_Operacion +"." + lstr_NomOperacion//operacion
                                                        );


                                                    }

                                                    //Resta el saldo    
                                                    ldec_Saldo = ldec_Saldo - Convert.ToDecimal(drForm["Monto"]);
                                                }
                                            }
                                            else
                                            {
                                                //Almacena en bitácora de que no lo hizo

                                                bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion, "DI"), "Resultado de Contabilización: \n 1 - [E] Monto superior al total de las reservas de la Deuda Interna. Reservas utilizadas: \n" + reservasError, lstr_Operacion, lstr_NumValor + "-" + lstr_Nemotecnico, "G206");
                                                SinError = false;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            if (!ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim().StartsWith("E"))
                                            {
                                                ldat_Asiento.Rows.Add(
                                                lstr_NumValor + " " + lstr_Nemotecnico,
                                                lstr_FchCanje.ToString("dd.MM.yyyy"),
                                                ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                                "CANJE DOLARES",
                                                ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                                Truncate(ldec_monto, 2),
                                                lstr_NumValor + "." + lstr_Nemotecnico,//pk
                                                tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                                lstr_Moneda,//tipo
                                                lstr_Operacion + "." + lstr_NomOperacion//operacion
                                                );


                                            }
                                            else
                                            {
                                                bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion, "DI"), "Resultado de Contabilización: \n 1 - [E] No hay reservas correspondientes a la cuenta " + ldat_Tira.Rows[0]["IdCuentaContable"].ToString().Trim() + " con fondo " + ldat_Tira.Rows[0]["IdPosPre"].ToString().Trim(), lstr_Operacion, lstr_NumValor + "-" + lstr_Nemotecnico, "G206");
                                                SinError = false;
                                                break;
                                            }

                                        }

                                    }

                                    //T

                                    //Amortizacion

                                    lstr_Monto = string.Empty;
                                    lds_Datos = new DataTable();
                                    ldec_MontoTotal = 0;
                                    reservasError = "";
                                    lstr_NuevoPosPrePago = string.Empty;
                                    ldat_Reservas = new DataSet();

                                    ldas_Tiras = tus_TipoAsiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "", lstr_Plazo, "AMORT", secuencia, lstr_OrderBy);
                                    ldat_Tira = ldas_Tiras.Tables["Table"];
                                    //strExprTitulo = (lstr_Nemotecnico.StartsWith("PT")) ? " ([CodigoAuxiliar2] = 'PT') " : " ([CodigoAuxiliar2] = 'TL_D') ";
                                    //ldat_Tira = ldat_Tira.Select(strExprTitulo).CopyToDataTable();


                                    //Validar que el pos pre sea diferente de PP_Balance y que el monto sea mayor al seleccionado




                                    foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                    {
                                        int index = ldat_Tira.Rows.IndexOf(ldr_Row);
                                        ldec_monto = ldec_DiferenciaCompradosColocados;

                                        ldec_monto = Math.Abs(ldec_monto);


                                        ldat_Reservas = consulta.ConsultarDinamico("select * from ma.reservasdetalle where idcuentacontable = '" + ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim() + "' and idpospre = '" + ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim() + "' AND IsNull(OrdenDeudaInterna,0) != 0  and LEFT(idprograma, 4) = year(getdate()) Order by OrdenDeudaInterna ASC");
                                        //ldat_Reservas = reservas.ConsultarReservasDetallado(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(), string.Empty, ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(), string.Empty, string.Empty, string.Empty, string.Empty);
                                        if (ldat_Reservas.Tables[0].Rows.Count != 0)
                                        {
                                            DataView dv = ldat_Reservas.Tables[0].DefaultView;
                                            dv.Sort = "OrdenDeudaInterna ASC";

                                            lds_Datos.Columns.Add("IdReserva");
                                            lds_Datos.Columns.Add("OrdenDeudaInterna");
                                            lds_Datos.Columns.Add("IdPosPre");
                                            lds_Datos.Columns.Add("Posicion");
                                            lds_Datos.Columns.Add("Monto");

                                            foreach (DataRow drForm in dv.ToTable().Rows)
                                            {
                                                //if (drForm["IdMoneda"].ToString().Trim().Equals(ldat_AsientoPago.Rows[0]["CodigoAuxiliar"].ToString().Trim()))
                                                if (!drForm["OrdenDeudaInterna"].ToString().Equals(string.Empty) && !drForm["OrdenDeudaInterna"].ToString().Equals("0"))
                                                {
                                                    lstr_Monto = asientos.ConsultaMontoReservaSAP(drForm["IdReserva"].ToString().Trim(), drForm["Posicion"].ToString().Trim());
                                                    lstr_Monto = lstr_Monto.Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal);
                                                    if (Convert.ToDecimal(lstr_Monto) > 0)
                                                    {
                                                        lds_Datos.Rows.Add(
                                                            drForm["IdReserva"].ToString(),
                                                            drForm["OrdenDeudaInterna"].ToString(),
                                                            drForm["IdPosPre"].ToString(),
                                                            drForm["Posicion"].ToString(),
                                                            lstr_Monto);
                                                        reservasError += "Posición Presupuestaria: " + drForm["IdPosPre"].ToString().Trim() + "Reserva :" + drForm["IdReserva"].ToString().Trim() + ", Posición: " + drForm["Posicion"].ToString().Trim() + "\n";
                                                        ldec_MontoTotal += Convert.ToDecimal(lstr_Monto);
                                                    }
                                                }
                                            }

                                            if (Convert.ToDecimal(ldec_MontoTotal) >= (Math.Abs(ldec_monto) * ldec_TipoCambio))
                                            {
                                                //Genera el asiento
                                                decimal ldec_Saldo = ldec_monto;

                                                foreach (DataRow drForm in lds_Datos.Rows)
                                                {
                                                    if (Convert.ToDecimal(drForm["Monto"]) > 0 && ldec_Saldo >= 0)
                                                    {
                                                        ldat_Asiento.Rows.Add(
                                                        lstr_NumValor + " " + lstr_Nemotecnico,
                                                        lstr_FchCanje.ToString("dd.MM.yyyy"),
                                                        ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                                        "CANJE DOLARES",
                                                        ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                                        drForm["IdReserva"].ToString().Trim(),
                                                        drForm["Posicion"].ToString().Trim(),
                                                        Truncate(ldec_Saldo > Convert.ToDecimal(drForm["Monto"].ToString()) ? Convert.ToDecimal(drForm["Monto"].ToString()) : ldec_Saldo, 2),
                                                        lstr_NumValor + "." + lstr_Nemotecnico,//pk
                                                        tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                                        lstr_Moneda,//tipo
                                                        lstr_Operacion +"." + lstr_NomOperacion//operacion
                                                        );


                                                    }

                                                    //Resta el saldo    
                                                    ldec_Saldo = ldec_Saldo - Convert.ToDecimal(drForm["Monto"]);
                                                }
                                            }
                                            else
                                            {
                                                //Almacena en bitácora de que no lo hizo

                                                bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion, "DI"), "Resultado de Contabilización: \n 1 - [E] Monto superior al total de las reservas de la Deuda Interna. Reservas utilizadas: \n" + reservasError, lstr_Operacion, lstr_NumValor + "-" + lstr_Nemotecnico, "G206");
                                                SinError = false;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            if (!ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim().StartsWith("E"))
                                            {
                                                ldat_Asiento.Rows.Add(
                                                lstr_NumValor + " " + lstr_Nemotecnico,
                                                lstr_FchCanje.ToString("dd.MM.yyyy"),
                                                ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                                "CANJE COLONES",
                                                ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                                Truncate(ldec_monto, 2),
                                                lstr_NumValor + "." + lstr_Nemotecnico,//pk
                                                tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                                lstr_Moneda,//tipo
                                                lstr_Operacion +"." + lstr_NomOperacion//operacion
                                                );


                                            }
                                            else
                                            {
                                                bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion, "DI"), "Resultado de Contabilización: \n 1 - [E] No hay reservas correspondientes a la cuenta " + ldat_Tira.Rows[0]["IdCuentaContable"].ToString().Trim() + " con fondo " + ldat_Tira.Rows[0]["IdPosPre"].ToString().Trim(), lstr_Operacion, lstr_NumValor + "-" + lstr_Nemotecnico, "G206");
                                                SinError = false;
                                                break;
                                            }

                                        }

                                    }

                                    // Amortizacion
                                    #endregion
                                }

                                }//Cierra Validacion Nemo Tecnico
                            }   //Cierra For Titulos     
                        }//Cierra Colones
                        #endregion


                        ///----------------------------------------------------------------------------------------------------
                        ///     Regiones de Tiras en Dólares
                        ///----------------------------------------------------------------------------------------------------
                        #region dolares
                        if (lstr_Moneda == "USD")
                        {
                            lint_ContadorPago = 0;
                            ldat_Valores = lcls_TituloValor.ConsultarTituloValor(null, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "Compra", "Vigente", Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/5000"), String.Empty).Tables[0];
                            strExpr = " TipoNegociacion = 'Compra' AND FchValor= ' " + lstr_FchCanje.ToString("yyyy.MM.dd") + " '";
                            ldat_Valores = ldat_Valores.Select(strExpr).CopyToDataTable();

                            for (int i = 0; i < 1; i++)
                            {

                                lstr_NumValor = ldat_Valores.Rows[i]["NroValor"].ToString();
                                lstr_Moneda = ldat_Valores.Rows[i]["Moneda"].ToString();
                                lstr_Nemotecnico = ldat_Valores.Rows[i]["Nemotecnico"].ToString();
                                lstr_Propietario = ldat_Valores.Rows[i]["Propietario"].ToString();
                                ltd_FchVencimiento = Convert.ToDateTime(ldat_Valores.Rows[i]["FchVencimiento"]);
                                lstr_Serie = ldat_Valores.Rows[i]["NroEmisionSerie"].ToString().Trim();
                                lstr_Tipo = ldat_Valores.Rows[i]["Tipo"].ToString().Trim();
                                lstr_ModuloSINPE = ldat_Valores.Rows[i]["ModuloSINPE"].ToString();
                                ldec_ValorFacial = Convert.ToDecimal(ldat_Valores.Rows[i]["ValorFacial"]);
                                ldec_ValorTransadoBruto = Convert.ToDecimal(ldat_Valores.Rows[i]["ValorTransadoBruto"]);
                                ldec_ValorTransadoNeto = Convert.ToDecimal(ldat_Valores.Rows[i]["ValorTransadoNeto"]);
                                string cod_aux2 = (lstr_Nemotecnico.StartsWith("PT") ? "PT" : "");


                                ldas_Colocados = consulta.ConsultarDinamico("SELECT Top 1 * FROM [cf].[TitulosValores] WHERE NroEmisionSerie = '" + lstr_Serie + "' AND TipoNegociacion != 'Compra' ORDER BY FchValor ASC");
                                ldat_Colocados = ldas_Colocados.Tables["Table"];

                                foreach (DataRow row in ldat_Colocados.Rows)
                                {
                                    ltd_FchValor = Convert.ToDateTime(row["FchValor"].ToString());
                                }
                                TimeSpan ts = lstr_FchCanje - ltd_FchValor;
                                lint_DiferenciaPlazoValor = ts.Days;


                                ldas_TablaArriba = consulta.ConsultarDinamico("SELECT * FROM [cf].[CanjeResumenSerie] WHERE [NroEmisionSerie] =  '" + lstr_Serie + "' AND Convert(Varchar(8),FchCanje,112) = Convert(Varchar(8),(Convert(DATETIME,'" + lstr_FchCanje.ToShortDateString() + "',103)),112)");
                                ldat_TablaArriba = ldas_TablaArriba.Tables["Table"];

                                foreach (DataRow row in ldat_TablaArriba.Rows)
                                {
                                    ldec_NetoBaja = Convert.ToDecimal(row["NetoBaja"]);
                                }

                                ldec_NetoBajaDiferencia = ldec_ValorTransadoNeto - ldec_NetoBaja;

                                if (lcls_Propietarios.ConsultarPropietarios(string.Empty, string.Empty, string.Empty, lstr_Propietario, "S").Tables[0].Rows.Count == 0)
                                {
                                    lstr_Propietario = "PRIVADO";
                                }
                                else
                                {
                                    lstr_Propietario = "PUBLICO";
                                }


                                if (lint_DiferenciaPlazoValor <= 361)
                                {
                                    lstr_Plazo = "CP";
                                }
                                else
                                {
                                    lstr_Plazo = "LP";
                                }

                                if ((ldec_ValorNetoComprados - ldec_ValorNetoColocados) > 0)
                                {
                                    lstr_IdClaveContable = "50";
                                }
                                else
                                {
                                    lstr_IdClaveContable = "40";
                                }

                                ldec_DiferenciaCompradosColocados = ldec_CapitalAbajo - ldec_ValorFacialComprados;
                                ldec_DiferenciaCompradosColocados = Math.Abs(ldec_DiferenciaCompradosColocados);

//-----------------------------------------------------------------Regiones ID65 y ID66---------------------------------------------
                                if (ldec_CapitalAbajo > ldec_ValorFacialComprados)
                                {
                                    #region ID65
                                    lstr_Operacion = "ID65";

                                    DataSet lds_Operaciones = loperacion.ConsultarOperaciones(lstr_Operacion, "IdModulo IN ('DI')", "");
                                    if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
                                    {
                                        lstr_NomOperacion = lds_Operaciones.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
                                    }
                                    var strExprTitulo = "";
                                    string lstr_Monto = string.Empty;
                                    DataTable lds_Datos = new DataTable();
                                    decimal ldec_MontoTotal = 0;
                                    string reservasError = "";
                                    string lstr_NuevoPosPrePago = string.Empty;
                                    DataSet ldat_Reservas = new DataSet();
                                    
                                    // Vacias
                                    ldas_Tiras = tus_TipoAsiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "", "", "", secuencia, lstr_OrderBy);
                                    ldat_Tira = ldas_Tiras.Tables["Table"];
                                    strExprTitulo = "([CodigoAuxiliar2] = '') AND [IdClaveContable] = '" + lstr_IdClaveContable + "' ";
                                    ldat_Tira = ldat_Tira.Select(strExprTitulo).CopyToDataTable();

                                    if (lint_ContadorPago == 0)
                                    {
                                        lint_ContadorPago += lint_ContadorPago + 1;
                                        foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                        {
                                            int index = ldat_Tira.Rows.IndexOf(ldr_Row);
                                            ldec_monto = ldec_DiferenciaTransados;

                                            ldat_Asiento.Rows.Add(
                                                lstr_NumValor + " " + lstr_Nemotecnico,
                                                lstr_FchCanje.ToString("dd.MM.yyyy"),
                                                ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                                "CANJE COLOCACION DOLARES",
                                                ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                                Math.Round(ldec_monto, 2),
                                                lstr_NumValor + "." + lstr_Nemotecnico,//pk
                                                tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                                lstr_Moneda,//tipo
                                                lstr_Operacion +"."+ lstr_NomOperacion//operacion
                                                );
                                        }//foreach
                                    }//if

                                    // Vacias

                                    // Normales

                                    //Colocacion
                                    ldas_Nemotecnico = consulta.ConsultarDinamico("SELECT DISTINCT(Nemotecnico) FROM  [cf].[TitulosCanjeSubasta] WHERE FchCanje = '" + lstr_FchCanje.ToString("yyyy.MM.dd") + "'");
                                    ldat_Nemotecnico = ldas_Nemotecnico.Tables["Table"];
                                    string lstr_NemotecnicoColocados;
                                    foreach (DataRow row in ldat_Nemotecnico.Rows)
                                    {
                                        lstr_NemotecnicoColocados = row["Nemotecnico"].ToString().Trim();
                                        ldec_DiferenciaPrimaColocados = 0;
                                        ldec_CupoCorridoColocadoDescuento = 0;
                                        ldec_TotalValorFacialColocado = 0;
                                        ldec_DescuentoColocados = 0;
                                        ldec_PrimasColocados = 0;
                                        lint_ContadorDescuentoAsiento = 0;
                                        lint_ContadorPrimaAsiento = 0;
                                        lint_ContadorCuponAsiento = 0;
                                        ldas_Colocados = consulta.ConsultarDinamico("SELECT * FROM  [cf].[TitulosCanjeSubasta] WHERE FchCanje = '" + lstr_FchCanje.ToString("yyyy.MM.dd") + "' AND Nemotecnico ='" + lstr_NemotecnicoColocados + "'");
                                        ldat_Colocados = ldas_Colocados.Tables["Table"];



                                        foreach (DataRow row1 in ldat_Colocados.Rows)
                                        {
                                            lstr_NroValorColocado = row1["NroValor"].ToString().Trim();
                                            lstr_Nemotecnico = row1["Nemotecnico"].ToString().Trim();

                                            ldas_ColocadosCanje = consulta.ConsultarDinamico("SELECT * FROM [cf].[TitulosValores] WHERE [NroValor] = '" + lstr_NroValorColocado + "' AND [Nemotecnico] = '" + lstr_Nemotecnico + "' AND EstadoValor = 'Vigente' AND IndicadorCupon = 'V' AND FchValor = '" + lstr_FchCanje.ToString("yyyy.MM.dd") + "'");
                                            ldat_ColocadosCanje = ldas_ColocadosCanje.Tables["Table"];

                                            foreach (DataRow row2 in ldat_ColocadosCanje.Rows)
                                            {
                                                ldec_ValorFacialColocado = Convert.ToDecimal(row2["ValorFacial"]);
                                                ldec_TransadoNetoColocado = Convert.ToDecimal(row2["ValorTransadoNeto"]);
                                                ldec_TransadoBrutoColocado = Convert.ToDecimal(row2["ValorTransadoBruto"]);
                                            }

                                            esDescuento = ldec_ValorFacial >= ldec_ValorTransadoBruto;
                                            ldec_DiferenciaPrimaColocados = ldec_ValorFacialColocado - ldec_TransadoBrutoColocado;
                                            ldec_DiferenciaCupoCorridoColocado = ldec_TransadoNetoColocado - ldec_TransadoBrutoColocado;
                                            ldec_TotalValorFacialColocado += ldec_ValorFacialColocado;

                                            if (ldec_DiferenciaPrimaColocados > 0)
                                            {
                                                ldec_DescuentoColocados += ldec_DiferenciaPrimaColocados;
                                                //ldec_CupoCorridoColocadoDescuento += ldec_DiferenciaCupoCorridoColocado;
                                            }
                                            if (ldec_DiferenciaPrimaColocados < 0)
                                            {
                                                ldec_PrimasColocados += ldec_DiferenciaPrimaColocados;
                                                //ldec_CupoCorridoColocadoPrima += ldec_DiferenciaCupoCorridoColocado;
                                                //ldec_PrimasColocados = ldec_PrimasColocados;
                                            }

                                            ldec_CupoCorridoColocadoDescuento += ldec_DiferenciaCupoCorridoColocado;

                                        }

                                        ldas_ColocadosCapital = consulta.ConsultarDinamico("SELECT TOP 1 tcs.Nemotecnico, count (tv.Nemotecnico ) As NumeroVeces FROM (SELECT DISTINCT Nemotecnico, fchcanje FROM [cf].[TitulosCanjeSubasta]) tcs LEFT OUTER JOIN [cf].[TitulosValores] tv ON tv.Nemotecnico = tcs.Nemotecnico AND tv.FchValor = tcs.FchCanje AND TV.IndicadorCupon = 'V' AND TV.TipoNegociacion = 'Compra' WHERE tcs.FchCanje = '" + lstr_FchCanje.ToString("yyyy.MM.dd") + "' GROUP BY tcs.Nemotecnico ORDER BY count (tv.Nemotecnico ) ASC ");
                                        ldat_ColocadosCapital = ldas_ColocadosCapital.Tables["Table"];
                                        string lstr_NumeroNemotecnicoColocado = "";

                                        int lint_NemotecnicoNumeroVeces = 0;
                                        foreach (DataRow row3 in ldat_ColocadosCapital.Rows)
                                        {
                                            lint_NemotecnicoNumeroVeces = Convert.ToInt32(row3["NumeroVeces"]);
                                            lstr_NumeroNemotecnicoColocado = row3["Nemotecnico"].ToString().Trim();

                                        }


                                        ldec_DiferenciaValorFacial = ldec_ValorFacialColocado - ldec_ValorFacial;
                                        ldec_DiferenciaValorFacial = Math.Abs(ldec_DiferenciaValorFacial);

                                        ldas_Tiras = tus_TipoAsiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_NemotecnicoColocados, lstr_Propietario, "", lstr_Plazo, "", secuencia, lstr_OrderBy);
                                        ldat_Tira = ldas_Tiras.Tables["Table"];


                                        foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                        {

                                            int index = ldat_Tira.Rows.IndexOf(ldr_Row);
                                            ldec_monto = 0;
                                            string IdClaveContable = ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim();
                                            string Tipo = ldat_Tira.Rows[index]["CodigoAuxiliar2"].ToString().Trim();
                                            string Pospre = ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim();
                                            string l_strLetra = Pospre.Substring(0, 1);



                                            switch (Tipo + IdClaveContable)
                                            {

                                                case "CAPITAL50":

                                                    if (l_strLetra == "I" && lstr_NumeroNemotecnicoColocado == lstr_Nemotecnico)
                                                    {
                                                        ldec_monto = ldec_DiferenciaCompradosColocados;
                                                        ldec_monto = Math.Abs(ldec_monto);

                                                    }
                                                    else
                                                    {
                                                        if (lstr_NumeroNemotecnicoColocado == lstr_Nemotecnico)
                                                        {
                                                            if (ldec_TotalValorFacialColocado > ldec_DiferenciaCompradosColocados)
                                                            {
                                                                ldec_monto = ldec_TotalValorFacialColocado - ldec_DiferenciaCompradosColocados;
                                                                ldec_monto = Math.Abs(ldec_monto);
                                                            }

                                                        }
                                                        else
                                                        {
                                                            if (l_strLetra != "I")
                                                            {
                                                                if (lint_ContadorCapitalAsiento == 0)
                                                                {
                                                                    ldec_monto = ldec_TotalValorFacialColocado;
                                                                    ldec_monto = Math.Abs(ldec_monto);
                                                                    lint_ContadorCapitalAsiento = 1;
                                                                }
                                                            }

                                                        }
                                                    }



                                                    break;
                                                case "IMP_DEV40":

                                                    if (ldec_DiferenciaPrimaColocados == 0)
                                                    {
                                                        ldec_monto = 0;
                                                    }
                                                    else
                                                    {
                                                        if (lint_ContadorDescuentoAsiento == 0)
                                                        {
                                                            ldec_monto = ldec_DescuentoColocados;
                                                            lint_ContadorDescuentoAsiento = 1;
                                                        }

                                                    }
                                                    break;


                                                case "IMP_DEV50":

                                                    if (lint_ContadorCuponAsiento == 0)
                                                    {
                                                        ldec_monto = ldec_CupoCorridoColocadoDescuento;
                                                        ldec_monto = Math.Abs(ldec_monto);
                                                        lint_ContadorCuponAsiento = 1;
                                                    }

                                                    if (!esDescuento)
                                                    {
                                                        for (int iRow = 0; iRow < ldat_Tira.Rows.Count; iRow++)
                                                        {
                                                            string IdClaveContable2 = ldat_Tira.Rows[iRow]["IdClaveContable"].ToString().Trim();
                                                            string Tipo2 = ldat_Tira.Rows[iRow]["CodigoAuxiliar2"].ToString().Trim();
                                                            if ((Tipo2 + IdClaveContable2).Equals("PRIMAS50"))
                                                            {
                                                                index = iRow;
                                                            }
                                                        }
                                                    }

                                                    break;

                                                case "PRIMAS50":


                                                    if (ldec_DiferenciaPrimaColocados == 0)
                                                    {
                                                        ldec_monto = 0;
                                                    }
                                                    else
                                                    {
                                                        if (lint_ContadorPrimaAsiento == 0)
                                                        {
                                                            ldec_monto = Math.Abs(ldec_PrimasColocados);
                                                            lint_ContadorPrimaAsiento = 1;
                                                        }

                                                    }

                                                    break;

                                            }

                                            if (ldec_monto != 0)
                                            {


                                                ldat_Asiento.Rows.Add(
                                            lstr_NumValor + " " + lstr_NemotecnicoColocados,
                                            lstr_FchCanje.ToString("dd.MM.yyyy"),
                                            ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                            "CANJE COLONES",
                                            ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                            Math.Round(ldec_monto, 2),
                                            lstr_NumValor + "." + lstr_Nemotecnico,//pk
                                            tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                            lstr_Moneda,//tipo
                                            lstr_Operacion +"."+lstr_NomOperacion//operacion
                                            );

                                            }
                                        }
                                    }


                                    //Colocacion

                                    //Compras

                                    ldas_CompradosCanje = consulta.ConsultarDinamico("SELECT DISTINCT (Nemotecnico) FROM [cf].[TitulosValores] WHERE TipoNegociacion = 'Compra' AND  FchValor = '" + lstr_FchCanje.ToString("yyyy.MM.dd") + "' and IndicadorCupon = 'V'  AND EstadoValor = 'Vigente'");
                                    ldat_CompradosCanje = ldas_CompradosCanje.Tables["Table"];

                                    foreach (DataRow row in ldat_CompradosCanje.Rows)
                                    {
                                        lstr_NemotecnicoComprados = row["Nemotecnico"].ToString().Trim();
                                        lint_ContadorDescuentoAsiento = 0;
                                        lint_ContadorPrimaAsiento = 0;
                                        lint_ContadorPrimaAsientoLargo = 0;
                                        lint_ContadorDescuentoAsientoLargo = 0;

                                        //--------NUEVA                                        
                                        ldec_ImportesDevengarArribaCorto = 0;
                                        ldec_InteresDevengarArribaCorto = 0;
                                        ldec_PrimaArribaCorto = 0;
                                        ldec_ValorFacialCorto = 0;
                                        ldec_ImportesDevengarArribaLargo = 0;
                                        ldec_InteresDevengarArribaLargo = 0;
                                        ldec_PrimaArribaLargo = 0;
                                        ldec_ValorFacialLargo = 0;

                                        ldas_CompradosCPLP = consulta.ConsultarDinamico("SELECT * FROM [cf].[TitulosValores] WHERE TipoNegociacion = 'Compra' AND  FchValor = '" + lstr_FchCanje.ToString("yyyy.MM.dd") + "' AND Nemotecnico = '" + lstr_NemotecnicoComprados + "'and IndicadorCupon = 'V'  AND EstadoValor = 'Vigente'");
                                        ldat_CompradosCPLP = ldas_CompradosCPLP.Tables["Table"];

                                        foreach (DataRow row1 in ldat_CompradosCPLP.Rows)
                                        {

                                            ltd_FchVencimiento = Convert.ToDateTime(row1["FchVencimiento"]);
                                            ldec_ValorFacial = Convert.ToDecimal(row1["ValorFacial"]);
                                            lstr_Serie = row1["NroEmisionSerie"].ToString().Trim();

                                            TimeSpan ts3 = ltd_FchVencimiento - lstr_FchCanje;
                                            lint_DiferenciaTrasciende = ts3.Days;

                                            if (lint_DiferenciaTrasciende <= 361)
                                            {
                                                //lstr_Plazo = "CP";
                                                //ldec_ValorFacialCorto +=ldec_ValorFacial;

                                                ldas_TablaArriba = consulta.ConsultarDinamico("select cr.*, tv.Nemotecnico from cf.CanjeResumenSerie as cr inner join  cf.TitulosValores tv on tv.NroEmisionSerie = cr.NroEmisionSerie and tv.TipoNegociacion = 'Compra' and tv.FchValor = cr.FchCanje WHERE  cr.NroEmisionSerie  = '" + lstr_Serie + "' AND Convert(Varchar(8),FchCanje,112) = Convert(Varchar(8),(Convert(DATETIME,'" + lstr_FchCanje.ToShortDateString() + "',103)),112) AND IdentificadorCanje = 'C'");
                                                ldat_TablaArriba = ldas_TablaArriba.Tables["Table"];

                                                foreach (DataRow row2 in ldat_TablaArriba.Rows)
                                                {

                                                    ldec_ImportesDevengarArribaCorto += Convert.ToDecimal(row2["InteresBaja"]);
                                                    ldec_InteresDevengarArribaCorto += Convert.ToDecimal(row2["EmisionDarBaja"]);
                                                    ldec_PrimaArribaCorto += Convert.ToDecimal(row2["InteresBajaPrima"]);
                                                    ldec_ValorFacialCorto += Convert.ToDecimal(row2["PorcentajeEmision"]);
                                                }

                                            }
                                            else
                                            {
                                                //lstr_Plazo = "LP";
                                                //ldec_ValorFacialLargo += ldec_ValorFacial;

                                                ldas_TablaArriba = consulta.ConsultarDinamico("select cr.*, tv.Nemotecnico from cf.CanjeResumenSerie as cr inner join  cf.TitulosValores tv on tv.NroEmisionSerie = cr.NroEmisionSerie and tv.TipoNegociacion = 'Compra' and tv.FchValor = cr.FchCanje WHERE  cr.NroEmisionSerie  = '" + lstr_Serie + "' AND Convert(Varchar(8),FchCanje,112) = Convert(Varchar(8),(Convert(DATETIME,'" + lstr_FchCanje.ToShortDateString() + "',103)),112) AND IdentificadorCanje = 'C'");
                                                ldat_TablaArriba = ldas_TablaArriba.Tables["Table"];

                                                foreach (DataRow row2 in ldat_TablaArriba.Rows)
                                                {

                                                    ldec_ImportesDevengarArribaLargo += Convert.ToDecimal(row2["InteresBaja"]);
                                                    ldec_InteresDevengarArribaLargo += Convert.ToDecimal(row2["EmisionDarBaja"]);
                                                    ldec_PrimaArribaLargo += Convert.ToDecimal(row2["InteresBajaPrima"]);
                                                    ldec_ValorFacialLargo += Convert.ToDecimal(row2["PorcentajeEmision"]);
                                                }

                                            }


                                        }

                                //    }



                                    ldas_Tiras = tus_TipoAsiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_NemotecnicoComprados, lstr_Propietario, "", "LP", "", secuencia, lstr_OrderBy);
                                    ldat_Tira = ldas_Tiras.Tables["Table"];


                                    foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                    {

                                        int index = ldat_Tira.Rows.IndexOf(ldr_Row);
                                        ldec_monto = 0;
                                        string IdClaveContable = ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim();
                                        string Tipo = ldat_Tira.Rows[index]["CodigoAuxiliar2"].ToString().Trim();


                                        switch (Tipo + IdClaveContable)
                                        {
                                            case "CAPITAL40":
                                                ldec_monto = Math.Abs(ldec_ValorFacialLargo);
                                                break;

                                            case "IMP_DEV40":
                                                //if (lint_ContadorDescuentoAsientoLargo == 1 || lint_ContadorDescuentoAsientoLargo == 2)
                                                //{
                                                    if (ldec_ImportesDevengarArribaLargo < 0)
                                                    {
                                                        ldec_monto = Math.Abs(ldec_ImportesDevengarArribaLargo);
                                                    }
                                                    else
                                                    {
                                                        ldec_monto = 0;
                                                    }
                                                //}
                                                //lint_ContadorDescuentoAsientoLargo = lint_ContadorDescuentoAsientoLargo + 1;
                                                break;
                                            case "IMP_DEV50":

                                                //if (lint_ContadorDescuentoAsientoLargo == 1 || lint_ContadorDescuentoAsientoLargo == 2)
                                                //{
                                                    if (ldec_ImportesDevengarArribaLargo > 0)
                                                    {
                                                        ldec_monto = Math.Abs(ldec_ImportesDevengarArribaLargo);
                                                    }
                                                    else
                                                    {
                                                        ldec_monto = 0;
                                                    }
                                                //}
                                                //lint_ContadorDescuentoAsientoLargo = lint_ContadorDescuentoAsientoLargo + 1;

                                                break;
                                            case "INT_DEV40":
                                                ldec_monto = Math.Abs(ldec_InteresDevengarArribaLargo);
                                                break;
                                            case "INT_DEV50":
                                                ldec_monto = 0;
                                                break;
                                            case "PRIMAS40":
                                                //if (lint_ContadorPrimaAsientoLargo == 1 || lint_ContadorPrimaAsientoLargo == 2)
                                                //{
                                                    if (ldec_PrimaArribaLargo > 0)
                                                    {
                                                        ldec_monto = 0;
                                                    }
                                                    else
                                                    {
                                                        ldec_monto = Math.Abs(ldec_PrimaArribaLargo);
                                                    }
                                                //}
                                                //lint_ContadorPrimaAsientoLargo = lint_ContadorPrimaAsientoLargo + 1;
                                                break;
                                            case "PRIMAS50":

                                                //if (lint_ContadorPrimaAsientoLargo == 1 || lint_ContadorPrimaAsientoLargo == 2)
                                                //{
                                                    if (ldec_PrimaArribaLargo > 0)
                                                    {
                                                        ldec_monto = Math.Abs(ldec_PrimaArribaLargo);
                                                    }
                                                    else
                                                    {
                                                        ldec_monto = 0;
                                                    }
                                                //}
                                                //lint_ContadorPrimaAsientoLargo = lint_ContadorPrimaAsientoLargo + 1;


                                                break;
                                        }



                                        if (ldec_monto != 0)
                                        {


                                            ldat_Asiento.Rows.Add(
                                        lstr_NumValor + " " + lstr_NemotecnicoComprados,
                                        lstr_FchCanje.ToString("dd.MM.yyyy"),
                                        ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                        "CANJE COLONES",
                                        ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                        Math.Round(ldec_monto, 2),
                                        lstr_NumValor + "." + lstr_Nemotecnico,//pk
                                        tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                        lstr_Moneda,//tipo
                                        lstr_Operacion+"."+lstr_NomOperacion //operacion
                                        );

                                        }


                                    }


                                    ldas_Tiras = tus_TipoAsiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_NemotecnicoComprados, lstr_Propietario, "", lstr_Plazo , "", secuencia, lstr_OrderBy);
                                    ldat_Tira = ldas_Tiras.Tables["Table"];


                                    foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                    {

                                        int index = ldat_Tira.Rows.IndexOf(ldr_Row);
                                        ldec_monto = 0;
                                        string IdClaveContable = ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim();
                                        string Tipo = ldat_Tira.Rows[index]["CodigoAuxiliar2"].ToString().Trim();


                                        switch (Tipo + IdClaveContable)
                                        {
                                            case "CAPITAL40":
                                                ldec_monto = Math.Abs(ldec_ValorFacialCorto);
                                                break;

                                            case "IMP_DEV40":
                                                //if (lint_ContadorDescuentoAsiento == 2 || lint_ContadorDescuentoAsiento == 3)
                                                //{
                                                    if (ldec_ImportesDevengarArribaCorto < 0)
                                                    {
                                                        ldec_monto = Math.Abs(ldec_ImportesDevengarArribaCorto);
                                                    }
                                                    else
                                                    {
                                                        ldec_monto = 0;
                                                    }
                                                //}
                                                //lint_ContadorDescuentoAsiento = lint_ContadorDescuentoAsiento + 1;
                                                break;
                                            case "IMP_DEV50":

                                                //if (lint_ContadorDescuentoAsiento == 1 || lint_ContadorDescuentoAsiento == 2)
                                                //{
                                                    if (ldec_ImportesDevengarArribaCorto > 0)
                                                    {
                                                        ldec_monto = Math.Abs(ldec_ImportesDevengarArribaCorto);
                                                    }
                                                    else
                                                    {
                                                        ldec_monto = 0;
                                                    }
                                                //}
                                                //lint_ContadorDescuentoAsiento = lint_ContadorDescuentoAsiento + 1;

                                                break;
                                            case "INT_DEV40":
                                                ldec_monto = Math.Abs(ldec_InteresDevengarArribaCorto);
                                                break;
                                            case "INT_DEV50":
                                                ldec_monto = 0;
                                                break;
                                            case "PRIMAS40":
                                                //if (lint_ContadorPrimaAsiento == 1 || lint_ContadorPrimaAsiento == 2)
                                                //{
                                                    if (ldec_PrimaArribaCorto > 0)
                                                    {
                                                        ldec_monto = 0;
                                                    }
                                                    else
                                                    {
                                                        ldec_monto = Math.Abs(ldec_PrimaArribaCorto);
                                                    }
                                                //}
                                                //lint_ContadorPrimaAsiento = lint_ContadorPrimaAsiento + 1;
                                                break;
                                            case "PRIMAS50":

                                                //if (lint_ContadorPrimaAsiento == 1 || lint_ContadorPrimaAsiento == 2)
                                                //{
                                                    if (ldec_PrimaArribaCorto > 0)
                                                    {
                                                        ldec_monto = Math.Abs(ldec_PrimaArribaCorto);
                                                    }
                                                    else
                                                    {
                                                        ldec_monto = 0;
                                                    }
                                                //}
                                                //lint_ContadorPrimaAsiento = lint_ContadorPrimaAsiento + 1;


                                                break;
                                        }



                                        if (ldec_monto != 0)
                                        {


                                            ldat_Asiento.Rows.Add(
                                        lstr_NumValor + " " + lstr_NemotecnicoComprados,
                                        lstr_FchCanje.ToString("dd.MM.yyyy"),
                                        ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                        "CANJE COLONES",
                                        ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                        Math.Round(ldec_monto, 2),
                                        lstr_NumValor + "." + lstr_Nemotecnico,//pk
                                        tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                        lstr_Moneda,//tipo
                                        lstr_Operacion+"."+lstr_NomOperacion //operacion
                                        );

                                        }


                                    }


                                    }//-------------    NUEVA

                                    //Normales

                                    //T

                                    lstr_Monto = string.Empty;
                                    lds_Datos = new DataTable();
                                    ldec_MontoTotal = 0;
                                    reservasError = "";
                                    lstr_NuevoPosPrePago = string.Empty;
                                    ldat_Reservas = new DataSet();

                                    ldas_Tiras = tus_TipoAsiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "", "", "", secuencia, lstr_OrderBy);
                                    ldat_Tira = ldas_Tiras.Tables["Table"];
                                    strExprTitulo = (lstr_Nemotecnico.StartsWith("PT")) ? " ([CodigoAuxiliar2] = 'PT') " : " ([CodigoAuxiliar2] = 'TL_D') ";
                                    ldat_Tira = ldat_Tira.Select(strExprTitulo).CopyToDataTable();



                                    foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                    {
                                        int index = ldat_Tira.Rows.IndexOf(ldr_Row);
                                        ldec_monto = ldec_Diferencia;


                                        ldat_Reservas = consulta.ConsultarDinamico("select * from ma.reservasdetalle where idcuentacontable = '" + ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim() + "' and idpospre = '" + ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim() + "' AND IsNull(OrdenDeudaInterna,0) != 0  and LEFT(idprograma, 4) = year(getdate()) Order by OrdenDeudaInterna ASC");
                                        //ldat_Reservas = reservas.ConsultarReservasDetallado(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(), string.Empty, ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(), string.Empty, string.Empty, string.Empty, string.Empty);
                                        if (ldat_Reservas.Tables[0].Rows.Count != 0)
                                        {
                                            DataView dv = ldat_Reservas.Tables[0].DefaultView;
                                            dv.Sort = "OrdenDeudaInterna ASC";

                                            if (lds_Datos.Columns.Count == 0) 
                                            {
                                                lds_Datos.Columns.Add("IdReserva");
                                                lds_Datos.Columns.Add("OrdenDeudaInterna");
                                                lds_Datos.Columns.Add("IdPosPre");
                                                lds_Datos.Columns.Add("Posicion");
                                                lds_Datos.Columns.Add("Monto");
                                            }
                                           

                                            foreach (DataRow drForm in dv.ToTable().Rows)
                                            {
                                                //if (drForm["IdMoneda"].ToString().Trim().Equals(ldat_AsientoPago.Rows[0]["CodigoAuxiliar"].ToString().Trim()))
                                                if (!drForm["OrdenDeudaInterna"].ToString().Equals(string.Empty) && !drForm["OrdenDeudaInterna"].ToString().Equals("0"))
                                                {
                                                    lstr_Monto = asientos.ConsultaMontoReservaSAP(drForm["IdReserva"].ToString().Trim(), drForm["Posicion"].ToString().Trim());
                                                    lstr_Monto = lstr_Monto.Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal);
                                                    if (Convert.ToDecimal(lstr_Monto) > 0)
                                                    {
                                                        lds_Datos.Rows.Add(
                                                            drForm["IdReserva"].ToString(),
                                                            drForm["OrdenDeudaInterna"].ToString(),
                                                            drForm["IdPosPre"].ToString(),
                                                            drForm["Posicion"].ToString(),
                                                            lstr_Monto);
                                                        reservasError += "Posición Presupuestaria: " + drForm["IdPosPre"].ToString().Trim() + "Reserva :" + drForm["IdReserva"].ToString().Trim() + ", Posición: " + drForm["Posicion"].ToString().Trim() + "\n";
                                                        ldec_MontoTotal += Convert.ToDecimal(lstr_Monto);
                                                    }
                                                }
                                            }

                                            if (Convert.ToDecimal(ldec_MontoTotal) >= (Math.Abs(ldec_monto) * ldec_TipoCambio))
                                            {
                                                //Genera el asiento
                                                decimal ldec_Saldo = ldec_monto;

                                                foreach (DataRow drForm in lds_Datos.Rows)
                                                {
                                                    if (Convert.ToDecimal(drForm["Monto"]) > 0 && ldec_Saldo >= 0)
                                                    {
                                                        ldat_Asiento.Rows.Add(
                                                        lstr_NumValor + " " + lstr_Nemotecnico,
                                                        lstr_FchCanje.ToString("dd.MM.yyyy"),
                                                        ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                                        "CANJE COLOCACION DOLARES",
                                                        ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                                        drForm["IdReserva"].ToString().Trim(),
                                                        drForm["Posicion"].ToString().Trim(),
                                                        Truncate(ldec_Saldo > Convert.ToDecimal(drForm["Monto"].ToString()) ? Convert.ToDecimal(drForm["Monto"].ToString()) : ldec_Saldo, 2),
                                                        lstr_NumValor + "." + lstr_Nemotecnico,//pk
                                                        tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                                        lstr_Moneda,//tipo
                                                        lstr_Operacion +"."+lstr_NomOperacion //operacion
                                                        );


                                                    }

                                                    //Resta el saldo    
                                                    ldec_Saldo = ldec_Saldo - Convert.ToDecimal(drForm["Monto"]);
                                                }
                                            }
                                            else
                                            {
                                                //Almacena en bitácora de que no lo hizo

                                                bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion, "DI"), "Resultado de Contabilización: \n 1 - [E] Monto superior al total de las reservas de la Deuda Interna. Reservas utilizadas: \n" + reservasError, lstr_Operacion, lstr_NumValor + "-" + lstr_Nemotecnico, "G206");
                                                SinError = false;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            if (!ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim().StartsWith("E"))
                                            {
                                                ldat_Asiento.Rows.Add(
                                                lstr_NumValor + " " + lstr_Nemotecnico,
                                                lstr_FchCanje.ToString("dd.MM.yyyy"),
                                                ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                                "CANJE COLOCACION DOLARES",
                                                ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                                Truncate(ldec_monto, 2),
                                                lstr_NumValor + "." + lstr_Nemotecnico,//pk
                                                tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                                lstr_Moneda,//tipo
                                                lstr_Operacion +"."+lstr_NomOperacion//operacion
                                                );


                                            }
                                            else
                                            {
                                                bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion, "DI"), "Resultado de Contabilización: \n 1 - [E] No hay reservas correspondientes a la cuenta " + ldat_Tira.Rows[0]["IdCuentaContable"].ToString().Trim() + " con fondo " + ldat_Tira.Rows[0]["IdPosPre"].ToString().Trim(), lstr_Operacion, lstr_NumValor + "-" + lstr_Nemotecnico, "G206");
                                                SinError = false;
                                                break;
                                            }

                                        }

                                    }

                                    //T
                                    #endregion
                                }
                                else
                                {
                                    #region ID66

                                    lstr_Operacion = "ID66";

                                    DataSet lds_Operaciones = loperacion.ConsultarOperaciones(lstr_Operacion, "IdModulo IN ('DI')", "");
                                    if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
                                    {
                                        lstr_NomOperacion = lds_Operaciones.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
                                    }
                                    var strExprTitulo = "";
                                    string lstr_Monto = string.Empty;
                                    DataTable lds_Datos = new DataTable();
                                    decimal ldec_MontoTotal = 0;
                                    string reservasError = "";
                                    string lstr_NuevoPosPrePago = string.Empty;
                                    DataSet ldat_Reservas = new DataSet();
                                    // Vacias
                                    cod_aux2 = (lstr_Nemotecnico.StartsWith("PT") ? "PT" : "");
                                    ldas_Tiras = tus_TipoAsiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "", "", "", secuencia, lstr_OrderBy);
                                    ldat_Tira = ldas_Tiras.Tables["Table"];
                                    strExprTitulo = " ([CodigoAuxiliar2] = '') AND [IdClaveContable]  = '" + lstr_IdClaveContable + "' ";
                                    ldat_Tira = ldat_Tira.Select(strExprTitulo).CopyToDataTable();

                                    if (lint_ContadorPago == 0)
                                    {
                                        lint_ContadorPago = lint_ContadorPago + 1;

                                        foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                        {
                                            int index = ldat_Tira.Rows.IndexOf(ldr_Row);
                                            ldec_monto = ldec_DiferenciaTransados < 0 ? ldec_DiferenciaTransados * -1 : ldec_DiferenciaTransados;

                                            ldat_Asiento.Rows.Add(
                                                lstr_NumValor + " " + lstr_Nemotecnico,
                                                lstr_FchCanje.ToString("dd.MM.yyyy"),
                                                ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                                "CANJE GASTO DOLARES",
                                                ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                                Math.Round(ldec_monto, 2),
                                                lstr_NumValor + "." + lstr_Nemotecnico,//pk
                                                tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                                lstr_Moneda,//tipo
                                                lstr_Operacion +"."+lstr_NomOperacion//operacion
                                                );
                                        }
                                    }



                                    // Vacias


                                    // Normales


                                    //Colocacion
                                    ldas_Nemotecnico = consulta.ConsultarDinamico("SELECT DISTINCT(Nemotecnico) FROM  [cf].[TitulosCanjeSubasta] WHERE FchCanje = '" + lstr_FchCanje.ToString("yyyy.MM.dd") + "'");
                                    ldat_Nemotecnico = ldas_Nemotecnico.Tables["Table"];
                                    string lstr_NemotecnicoColocados;
                                    foreach (DataRow row in ldat_Nemotecnico.Rows)
                                    {
                                        lstr_NemotecnicoColocados = row["Nemotecnico"].ToString().Trim();
                                        ldec_DiferenciaPrimaColocados = 0;
                                        ldec_CupoCorridoColocadoDescuento = 0;
                                        ldec_TotalValorFacialColocado = 0;
                                        ldec_DescuentoColocados = 0;
                                        ldec_PrimasColocados = 0;
                                        lint_ContadorDescuentoAsiento = 0;
                                        lint_ContadorPrimaAsiento = 0;
                                        lint_ContadorCuponAsiento = 0;
                                        ldas_Colocados = consulta.ConsultarDinamico("SELECT * FROM  [cf].[TitulosCanjeSubasta] WHERE FchCanje = '" + lstr_FchCanje.ToString("yyyy.MM.dd") + "' AND Nemotecnico ='" + lstr_NemotecnicoColocados + "'");
                                        ldat_Colocados = ldas_Colocados.Tables["Table"];



                                        foreach (DataRow row1 in ldat_Colocados.Rows)
                                        {
                                            lstr_NroValorColocado = row1["NroValor"].ToString().Trim();
                                            lstr_Nemotecnico = row1["Nemotecnico"].ToString().Trim();

                                            ldas_ColocadosCanje = consulta.ConsultarDinamico("SELECT * FROM [cf].[TitulosValores] WHERE [NroValor] = '" + lstr_NroValorColocado + "' AND [Nemotecnico] = '" + lstr_Nemotecnico + "' AND EstadoValor = 'Vigente' AND IndicadorCupon = 'V' AND FchValor = '" + lstr_FchCanje.ToString("yyyy.MM.dd") + "'");
                                            ldat_ColocadosCanje = ldas_ColocadosCanje.Tables["Table"];

                                            foreach (DataRow row2 in ldat_ColocadosCanje.Rows)
                                            {
                                                ldec_ValorFacialColocado = Convert.ToDecimal(row2["ValorFacial"]);
                                                ldec_TransadoNetoColocado = Convert.ToDecimal(row2["ValorTransadoNeto"]);
                                                ldec_TransadoBrutoColocado = Convert.ToDecimal(row2["ValorTransadoBruto"]);
                                            }

                                            esDescuento = ldec_ValorFacial >= ldec_ValorTransadoBruto;
                                            ldec_DiferenciaPrimaColocados = ldec_ValorFacialColocado - ldec_TransadoBrutoColocado;
                                            ldec_DiferenciaCupoCorridoColocado = ldec_TransadoNetoColocado - ldec_TransadoBrutoColocado;
                                            ldec_TotalValorFacialColocado += ldec_ValorFacialColocado;

                                            if (ldec_DiferenciaPrimaColocados > 0)
                                            {
                                                ldec_DescuentoColocados += ldec_DiferenciaPrimaColocados;
                                                //ldec_CupoCorridoColocadoDescuento += ldec_DiferenciaCupoCorridoColocado;
                                            }
                                            if (ldec_DiferenciaPrimaColocados < 0)
                                            {
                                                ldec_PrimasColocados += ldec_DiferenciaPrimaColocados;
                                                //ldec_CupoCorridoColocadoPrima += ldec_DiferenciaCupoCorridoColocado;
                                                //ldec_PrimasColocados = ldec_PrimasColocados;
                                            }

                                            ldec_CupoCorridoColocadoDescuento += ldec_DiferenciaCupoCorridoColocado;

                                        }

                                        ldas_ColocadosCapital = consulta.ConsultarDinamico("SELECT TOP 1 tcs.Nemotecnico, count (tv.Nemotecnico ) As NumeroVeces FROM (SELECT DISTINCT Nemotecnico, fchcanje FROM [cf].[TitulosCanjeSubasta]) tcs LEFT OUTER JOIN [cf].[TitulosValores] tv ON tv.Nemotecnico = tcs.Nemotecnico AND tv.FchValor = tcs.FchCanje AND TV.IndicadorCupon = 'V' AND TV.TipoNegociacion = 'Compra' WHERE tcs.FchCanje = '" + lstr_FchCanje.ToString("yyyy.MM.dd") + "' GROUP BY tcs.Nemotecnico ORDER BY count (tv.Nemotecnico ) ASC ");
                                        ldat_ColocadosCapital = ldas_ColocadosCapital.Tables["Table"];
                                        string lstr_NumeroNemotecnicoColocado = "";

                                        int lint_NemotecnicoNumeroVeces = 0;
                                        foreach (DataRow row3 in ldat_ColocadosCapital.Rows)
                                        {
                                            lint_NemotecnicoNumeroVeces = Convert.ToInt32(row3["NumeroVeces"]);
                                            lstr_NumeroNemotecnicoColocado = row3["Nemotecnico"].ToString().Trim();

                                        }





                                        ldec_DiferenciaValorFacial = ldec_ValorFacialColocado - ldec_ValorFacial;
                                        ldec_DiferenciaValorFacial = Math.Abs(ldec_DiferenciaValorFacial);

                                        ldas_Tiras = tus_TipoAsiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_NemotecnicoColocados, lstr_Propietario, "", lstr_Plazo, "", secuencia, lstr_OrderBy);
                                        ldat_Tira = ldas_Tiras.Tables["Table"];



                                        foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                        {

                                            int index = ldat_Tira.Rows.IndexOf(ldr_Row);
                                            ldec_monto = 0;
                                            string IdClaveContable = ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim();
                                            string Tipo = ldat_Tira.Rows[index]["CodigoAuxiliar2"].ToString().Trim();
                                            string Pospre = ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim();
                                            string l_strLetra = Pospre.Substring(0, 1);



                                            switch (Tipo + IdClaveContable)
                                            {

                                                case "CAPITAL50":

                                                    if (l_strLetra == "E" && lstr_NumeroNemotecnicoColocado == lstr_Nemotecnico)
                                                    {
                                                        ldec_monto = ldec_DiferenciaCompradosColocados;
                                                        ldec_monto = Math.Abs(ldec_monto);

                                                    }
                                                    else
                                                    {
                                                        if (lstr_NumeroNemotecnicoColocado == lstr_Nemotecnico)
                                                        {
                                                          //  if (ldec_TotalValorFacialColocado > ldec_DiferenciaCompradosColocados)
                                                            //{
                                                                ldec_monto = ldec_TotalValorFacialColocado;// -ldec_DiferenciaCompradosColocados;
                                                                ldec_monto = Math.Abs(ldec_monto);
                                                            //}

                                                        }
                                                        else
                                                        {
                                                            if (l_strLetra != "E")
                                                            {
                                                                if (lint_ContadorCapitalAsiento == 0)
                                                                {
                                                                    ldec_monto = ldec_TotalValorFacialColocado;
                                                                    ldec_monto = Math.Abs(ldec_monto);
                                                                    lint_ContadorCapitalAsiento = 1;
                                                                }
                                                            }

                                                        }
                                                    }



                                                    break;
                                                case "IMP_DEV40":

                                                    if (ldec_DiferenciaPrimaColocados == 0)
                                                    {
                                                        ldec_monto = 0;
                                                    }
                                                    else
                                                    {
                                                        if (lint_ContadorDescuentoAsiento == 0)
                                                        {
                                                            ldec_monto = ldec_DescuentoColocados;
                                                            lint_ContadorDescuentoAsiento = 1;
                                                        }

                                                    }
                                                    break;


                                                case "IMP_DEV50":

                                                    if (lint_ContadorCuponAsiento == 0)
                                                    {
                                                        ldec_monto = ldec_CupoCorridoColocadoDescuento;
                                                        ldec_monto = Math.Abs(ldec_monto);
                                                        lint_ContadorCuponAsiento = 1;
                                                    }

                                                    if (!esDescuento)
                                                    {
                                                        for (int iRow = 0; iRow < ldat_Tira.Rows.Count; iRow++)
                                                        {
                                                            string IdClaveContable2 = ldat_Tira.Rows[iRow]["IdClaveContable"].ToString().Trim();
                                                            string Tipo2 = ldat_Tira.Rows[iRow]["CodigoAuxiliar2"].ToString().Trim();
                                                            if ((Tipo2 + IdClaveContable2).Equals("PRIMAS50"))
                                                            {
                                                                index = iRow;
                                                            }
                                                        }
                                                    }

                                                    break;

                                                case "PRIMAS50":


                                                    if (ldec_DiferenciaPrimaColocados == 0)
                                                    {
                                                        ldec_monto = 0;
                                                    }
                                                    else
                                                    {
                                                        if (lint_ContadorPrimaAsiento == 0)
                                                        {
                                                            ldec_monto = Math.Abs(ldec_PrimasColocados);
                                                            lint_ContadorPrimaAsiento = 1;
                                                        }

                                                    }

                                                    break;

                                            }

                                            if (ldec_monto != 0)
                                            {


                                                ldat_Asiento.Rows.Add(
                                            lstr_NumValor + " " + lstr_NemotecnicoColocados,
                                            lstr_FchCanje.ToString("dd.MM.yyyy"),
                                            ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                            "CANJE COLONES",
                                            ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                            Math.Round(ldec_monto, 2),
                                            lstr_NumValor + "." + lstr_Nemotecnico,//pk
                                            tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                            lstr_Moneda,//tipo
                                            lstr_Operacion +"."+lstr_NomOperacion//operacion
                                            );

                                            }
                                        }
                                    }


                                    //Colocacion

                                    //Compras

                                    ldas_CompradosCanje = consulta.ConsultarDinamico("SELECT DISTINCT (Nemotecnico) FROM [cf].[TitulosValores] WHERE TipoNegociacion = 'Compra' AND  FchValor = '" + lstr_FchCanje.ToString("yyyy.MM.dd") + "' and IndicadorCupon = 'V'  AND EstadoValor = 'Vigente'");
                                    ldat_CompradosCanje = ldas_CompradosCanje.Tables["Table"];

                                    foreach (DataRow row in ldat_CompradosCanje.Rows)
                                    {
                                        lstr_NemotecnicoComprados = row["Nemotecnico"].ToString().Trim();
                                        lint_ContadorDescuentoAsiento = 0;
                                        lint_ContadorPrimaAsiento = 0;
                                        lint_ContadorPrimaAsientoLargo = 0;
                                        lint_ContadorDescuentoAsientoLargo = 0;

// ----------NUEVA                                        
                                        ldec_ImportesDevengarArribaCorto = 0;
                                        ldec_InteresDevengarArribaCorto = 0;
                                        ldec_PrimaArribaCorto = 0;
                                        ldec_ValorFacialCorto = 0;
                                        ldec_ImportesDevengarArribaLargo = 0;
                                        ldec_InteresDevengarArribaLargo = 0;
                                        ldec_PrimaArribaLargo = 0;
                                        ldec_ValorFacialLargo = 0;

                                        ldas_CompradosCPLP = consulta.ConsultarDinamico("SELECT * FROM [cf].[TitulosValores] WHERE TipoNegociacion = 'Compra' AND  FchValor = '" + lstr_FchCanje.ToString("yyyy.MM.dd") + "' AND Nemotecnico = '" + lstr_NemotecnicoComprados + "'and IndicadorCupon = 'V'  AND EstadoValor = 'Vigente'");
                                        ldat_CompradosCPLP = ldas_CompradosCPLP.Tables["Table"];

                                        foreach (DataRow row1 in ldat_CompradosCPLP.Rows)
                                        {

                                            ltd_FchVencimiento = Convert.ToDateTime(row1["FchVencimiento"]);
                                            ldec_ValorFacial = Convert.ToDecimal(row1["ValorFacial"]);
                                            lstr_Serie = row1["NroEmisionSerie"].ToString().Trim();

                                            TimeSpan ts3 = ltd_FchVencimiento - lstr_FchCanje;
                                            lint_DiferenciaTrasciende = ts3.Days;

                                            if (lint_DiferenciaTrasciende <= 361)
                                            {
                                                //lstr_Plazo = "CP";
                                                //ldec_ValorFacialCorto +=ldec_ValorFacial;

                                                ldas_TablaArriba = consulta.ConsultarDinamico("select cr.*, tv.Nemotecnico from cf.CanjeResumenSerie as cr inner join  cf.TitulosValores tv on tv.NroEmisionSerie = cr.NroEmisionSerie and tv.TipoNegociacion = 'Compra' and tv.FchValor = cr.FchCanje WHERE  cr.NroEmisionSerie  = '" + lstr_Serie + "' AND Convert(Varchar(8),FchCanje,112) = Convert(Varchar(8),(Convert(DATETIME,'" + lstr_FchCanje.ToShortDateString() + "',103)),112) AND IdentificadorCanje = 'C'");
                                                ldat_TablaArriba = ldas_TablaArriba.Tables["Table"];

                                                foreach (DataRow row2 in ldat_TablaArriba.Rows)
                                                {

                                                    ldec_ImportesDevengarArribaCorto += Convert.ToDecimal(row2["InteresBaja"]);
                                                    ldec_InteresDevengarArribaCorto += Convert.ToDecimal(row2["EmisionDarBaja"]);
                                                    ldec_PrimaArribaCorto += Convert.ToDecimal(row2["InteresBajaPrima"]);
                                                    ldec_ValorFacialCorto += Convert.ToDecimal(row2["PorcentajeEmision"]);
                                                }

                                            }
                                            else
                                            {
                                                //lstr_Plazo = "LP";
                                                //ldec_ValorFacialLargo += ldec_ValorFacial;

                                                ldas_TablaArriba = consulta.ConsultarDinamico("select cr.*, tv.Nemotecnico from cf.CanjeResumenSerie as cr inner join  cf.TitulosValores tv on tv.NroEmisionSerie = cr.NroEmisionSerie and tv.TipoNegociacion = 'Compra' and tv.FchValor = cr.FchCanje WHERE  cr.NroEmisionSerie  = '" + lstr_Serie + "' AND Convert(Varchar(8),FchCanje,112) = Convert(Varchar(8),(Convert(DATETIME,'" + lstr_FchCanje.ToShortDateString() + "',103)),112) AND IdentificadorCanje = 'C'");
                                                ldat_TablaArriba = ldas_TablaArriba.Tables["Table"];

                                                foreach (DataRow row2 in ldat_TablaArriba.Rows)
                                                {

                                                    ldec_ImportesDevengarArribaLargo += Convert.ToDecimal(row2["InteresBaja"]);
                                                    ldec_InteresDevengarArribaLargo += Convert.ToDecimal(row2["EmisionDarBaja"]);
                                                    ldec_PrimaArribaLargo += Convert.ToDecimal(row2["InteresBajaPrima"]);
                                                    ldec_ValorFacialLargo += Convert.ToDecimal(row2["PorcentajeEmision"]);
                                                }

                                            }


                                        }

                                   // }



                                    ldas_Tiras = tus_TipoAsiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_NemotecnicoComprados, lstr_Propietario, "", "LP", "", secuencia, lstr_OrderBy);
                                    ldat_Tira = ldas_Tiras.Tables["Table"];


                                    foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                    {

                                        int index = ldat_Tira.Rows.IndexOf(ldr_Row);
                                        ldec_monto = 0;
                                        string IdClaveContable = ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim();
                                        string Tipo = ldat_Tira.Rows[index]["CodigoAuxiliar2"].ToString().Trim();


                                        switch (Tipo + IdClaveContable)
                                        {
                                            case "CAPITAL40":
                                                ldec_monto = Math.Abs(ldec_ValorFacialLargo);
                                                break;

                                            case "IMP_DEV40":
                                                //if (lint_ContadorDescuentoAsientoLargo == 1 || lint_ContadorDescuentoAsientoLargo == 2)
                                                //{
                                                    if (ldec_ImportesDevengarArribaLargo < 0)
                                                    {
                                                        ldec_monto = Math.Abs(ldec_ImportesDevengarArribaLargo);
                                                    }
                                                    else
                                                    {
                                                        ldec_monto = 0;
                                                    }
                                                //}
                                                //lint_ContadorDescuentoAsientoLargo = lint_ContadorDescuentoAsientoLargo + 1;
                                                break;
                                            case "IMP_DEV50":

                                                //if (lint_ContadorDescuentoAsientoLargo == 1 || lint_ContadorDescuentoAsientoLargo == 2)
                                                //{
                                                    if (ldec_ImportesDevengarArribaLargo > 0)
                                                    {
                                                        ldec_monto = Math.Abs(ldec_ImportesDevengarArribaLargo);
                                                    }
                                                    else
                                                    {
                                                        ldec_monto = 0;
                                                    }
                                                //}
                                                //lint_ContadorDescuentoAsientoLargo = lint_ContadorDescuentoAsientoLargo + 1;

                                                break;
                                            case "INT_DEV40":
                                                ldec_monto = Math.Abs(ldec_InteresDevengarArribaLargo);
                                                break;
                                            case "INT_DEV50":
                                                ldec_monto = 0;
                                                break;
                                            case "PRIMAS40":
                                                //if (lint_ContadorPrimaAsientoLargo == 1 || lint_ContadorPrimaAsientoLargo == 2)
                                                //{
                                                    if (ldec_PrimaArribaLargo > 0)
                                                    {
                                                        ldec_monto = 0;
                                                    }
                                                    else
                                                    {
                                                        ldec_monto = Math.Abs(ldec_PrimaArribaLargo);
                                                    }
                                                //}
                                                //lint_ContadorPrimaAsientoLargo = lint_ContadorPrimaAsientoLargo + 1;
                                                break;
                                            case "PRIMAS50":

                                                //if (lint_ContadorPrimaAsientoLargo == 1 || lint_ContadorPrimaAsientoLargo == 2)
                                                //{
                                                    if (ldec_PrimaArribaLargo > 0)
                                                    {
                                                        ldec_monto = Math.Abs(ldec_PrimaArribaLargo);
                                                    }
                                                    else
                                                    {
                                                        ldec_monto = 0;
                                                    }
                                                //}
                                                //lint_ContadorPrimaAsientoLargo = lint_ContadorPrimaAsientoLargo + 1;


                                                break;
                                        }



                                        if (ldec_monto != 0)
                                        {


                                            ldat_Asiento.Rows.Add(
                                        lstr_NumValor + " " + lstr_NemotecnicoComprados,
                                        lstr_FchCanje.ToString("dd.MM.yyyy"),
                                        ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                        "CANJE COLONES",
                                        ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                        Math.Round(ldec_monto, 2),
                                        lstr_NumValor + "." + lstr_Nemotecnico,//pk
                                        tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                        lstr_Moneda,//tipo
                                        lstr_Operacion +"."+lstr_NomOperacion//operacion
                                        );

                                        }


                                    }


                                    ldas_Tiras = tus_TipoAsiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_NemotecnicoComprados, lstr_Propietario, "", "CP", "", secuencia, lstr_OrderBy);
                                    ldat_Tira = ldas_Tiras.Tables["Table"];


                                    foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                    {

                                        int index = ldat_Tira.Rows.IndexOf(ldr_Row);
                                        ldec_monto = 0;
                                        string IdClaveContable = ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim();
                                        string Tipo = ldat_Tira.Rows[index]["CodigoAuxiliar2"].ToString().Trim();


                                        switch (Tipo + IdClaveContable)
                                        {
                                            case "CAPITAL40":
                                                ldec_monto = Math.Abs(ldec_ValorFacialCorto);
                                                break;

                                            case "IMP_DEV40":
                                                //if (lint_ContadorDescuentoAsiento == 2 || lint_ContadorDescuentoAsiento == 3)
                                                //{
                                                    if (ldec_ImportesDevengarArribaCorto < 0)
                                                    {
                                                        ldec_monto = Math.Abs(ldec_ImportesDevengarArribaCorto);
                                                    }
                                                    else
                                                    {
                                                        ldec_monto = 0;
                                                    }
                                                //}
                                                //lint_ContadorDescuentoAsiento = lint_ContadorDescuentoAsiento + 1;
                                                break;
                                            case "IMP_DEV50":

                                                //if (lint_ContadorDescuentoAsiento == 1 || lint_ContadorDescuentoAsiento == 2)
                                                //{
                                                    if (ldec_ImportesDevengarArribaCorto > 0)
                                                    {
                                                        ldec_monto = Math.Abs(ldec_ImportesDevengarArribaCorto);
                                                    }
                                                    else
                                                    {
                                                        ldec_monto = 0;
                                                    }
                                                //}
                                                //lint_ContadorDescuentoAsiento = lint_ContadorDescuentoAsiento + 1;

                                                break;
                                            case "INT_DEV40":
                                                ldec_monto = Math.Abs(ldec_InteresDevengarArribaCorto);
                                                break;
                                            case "INT_DEV50":
                                                ldec_monto = 0;
                                                break;
                                            case "PRIMAS40":
                                                //if (lint_ContadorPrimaAsiento == 1 || lint_ContadorPrimaAsiento == 2)
                                                //{
                                                    if (ldec_PrimaArribaCorto > 0)
                                                    {
                                                        ldec_monto = 0;
                                                    }
                                                    else
                                                    {
                                                        ldec_monto = Math.Abs(ldec_PrimaArribaCorto);
                                                    }
                                                //}
                                                //lint_ContadorPrimaAsiento = lint_ContadorPrimaAsiento + 1;
                                                break;
                                            case "PRIMAS50":

                                                //if (lint_ContadorPrimaAsiento == 1 || lint_ContadorPrimaAsiento == 2)
                                                //{
                                                    if (ldec_PrimaArribaCorto > 0)
                                                    {
                                                        ldec_monto = Math.Abs(ldec_PrimaArribaCorto);
                                                    }
                                                    else
                                                    {
                                                        ldec_monto = 0;
                                                    }
                                                //}
                                                //lint_ContadorPrimaAsiento = lint_ContadorPrimaAsiento + 1;


                                                break;
                                        }



                                        if (ldec_monto != 0)
                                        {


                                            ldat_Asiento.Rows.Add(
                                        lstr_NumValor + " " + lstr_NemotecnicoComprados,
                                        lstr_FchCanje.ToString("dd.MM.yyyy"),
                                        ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                        "CANJE COLONES",
                                        ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                        Math.Round(ldec_monto, 2),
                                        lstr_NumValor + "." + lstr_Nemotecnico,//pk
                                        tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                        lstr_Moneda,//tipo
                                        lstr_Operacion +"."+lstr_NomOperacion//operacion
                                        );

                                        }


                                    }

                                    }//-------------    NUEVA

                                    //Normales

                                    //T

                                    ldas_Tiras = tus_TipoAsiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "", "", "", secuencia, lstr_OrderBy);
                                    ldat_Tira = ldas_Tiras.Tables["Table"];
                                    strExprTitulo = (lstr_Nemotecnico.StartsWith("PT")) ? " ([CodigoAuxiliar2] = 'PT') " : " ([CodigoAuxiliar2] = 'TL_D') ";
                                    ldat_Tira = ldat_Tira.Select(strExprTitulo).CopyToDataTable();


                                 

                                    foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                    {
                                        int index = ldat_Tira.Rows.IndexOf(ldr_Row);
                                        ldec_monto = ldec_Diferencia;


                                        ldat_Reservas = consulta.ConsultarDinamico("select * from ma.reservasdetalle where idcuentacontable = '" + ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim() + "' and idpospre = '" + ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim() + "' AND IsNull(OrdenDeudaInterna,0) != 0  and LEFT(idprograma, 4) = year(getdate()) Order by OrdenDeudaInterna ASC");
                                        //ldat_Reservas = reservas.ConsultarReservasDetallado(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(), string.Empty, ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(), string.Empty, string.Empty, string.Empty, string.Empty);
                                        if (ldat_Reservas.Tables[0].Rows.Count != 0)
                                        {
                                            DataView dv = ldat_Reservas.Tables[0].DefaultView;
                                            dv.Sort = "OrdenDeudaInterna ASC";

                                            lds_Datos.Columns.Add("IdReserva");
                                            lds_Datos.Columns.Add("OrdenDeudaInterna");
                                            lds_Datos.Columns.Add("IdPosPre");
                                            lds_Datos.Columns.Add("Posicion");
                                            lds_Datos.Columns.Add("Monto");

                                            foreach (DataRow drForm in dv.ToTable().Rows)
                                            {
                                                //if (drForm["IdMoneda"].ToString().Trim().Equals(ldat_AsientoPago.Rows[0]["CodigoAuxiliar"].ToString().Trim()))
                                                if (!drForm["OrdenDeudaInterna"].ToString().Equals(string.Empty) && !drForm["OrdenDeudaInterna"].ToString().Equals("0"))
                                                {
                                                    lstr_Monto = asientos.ConsultaMontoReservaSAP(drForm["IdReserva"].ToString().Trim(), drForm["Posicion"].ToString().Trim());
                                                    lstr_Monto = lstr_Monto.Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal);
                                                    if (Convert.ToDecimal(lstr_Monto) > 0)
                                                    {
                                                        lds_Datos.Rows.Add(
                                                            drForm["IdReserva"].ToString(),
                                                            drForm["OrdenDeudaInterna"].ToString(),
                                                            drForm["IdPosPre"].ToString(),
                                                            drForm["Posicion"].ToString(),
                                                            lstr_Monto);
                                                        reservasError += "Posición Presupuestaria: " + drForm["IdPosPre"].ToString().Trim() + "Reserva :" + drForm["IdReserva"].ToString().Trim() + ", Posición: " + drForm["Posicion"].ToString().Trim() + "\n";
                                                        ldec_MontoTotal += Convert.ToDecimal(lstr_Monto);
                                                    }
                                                }
                                            }

                                            if (Convert.ToDecimal(ldec_MontoTotal) >= (Math.Abs(ldec_monto) * ldec_TipoCambio))
                                            {
                                                //Genera el asiento
                                                decimal ldec_Saldo = ldec_monto;

                                                foreach (DataRow drForm in lds_Datos.Rows)
                                                {
                                                    if (Convert.ToDecimal(drForm["Monto"]) > 0 && ldec_Saldo >= 0)
                                                    {
                                                        ldat_Asiento.Rows.Add(
                                                        lstr_NumValor + " " + lstr_Nemotecnico,
                                                        lstr_FchCanje.ToString("dd.MM.yyyy"),
                                                        ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                                        "CANJE GASTO DOLARES",
                                                        ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                                        drForm["IdReserva"].ToString().Trim(),
                                                        drForm["Posicion"].ToString().Trim(),
                                                        Truncate(ldec_Saldo > Convert.ToDecimal(drForm["Monto"].ToString()) ? Convert.ToDecimal(drForm["Monto"].ToString()) : ldec_Saldo, 2),
                                                        lstr_NumValor + "." + lstr_Nemotecnico,//pk
                                                        tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                                        lstr_Moneda,//tipo
                                                        lstr_Operacion +"."+lstr_NomOperacion//operacion
                                                        );


                                                    }

                                                    //Resta el saldo    
                                                    ldec_Saldo = ldec_Saldo - Convert.ToDecimal(drForm["Monto"]);
                                                }
                                            }
                                            else
                                            {
                                                //Almacena en bitácora de que no lo hizo

                                                bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion, "DI"), "Resultado de Contabilización: \n 1 - [E] Monto superior al total de las reservas de la Deuda Interna. Reservas utilizadas: \n" + reservasError, lstr_Operacion, lstr_NumValor + "-" + lstr_Nemotecnico, "G206");
                                                SinError = false;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            if (!ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim().StartsWith("E"))
                                            {
                                                ldat_Asiento.Rows.Add(
                                                lstr_NumValor + " " + lstr_Nemotecnico,
                                                lstr_FchCanje.ToString("dd.MM.yyyy"),
                                                ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                                "CANJE GASTO DOLARES",
                                                ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                                Truncate(ldec_monto, 2),
                                                lstr_NumValor + "." + lstr_Nemotecnico,//pk
                                                tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                                lstr_Moneda,//tipo
                                                lstr_Operacion +"."+lstr_NomOperacion//operacion
                                                );


                                            }
                                            else
                                            {
                                                bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion, "DI"), "Resultado de Contabilización: \n 1 - [E] No hay reservas correspondientes a la cuenta " + ldat_Tira.Rows[0]["IdCuentaContable"].ToString().Trim() + " con fondo " + ldat_Tira.Rows[0]["IdPosPre"].ToString().Trim(), lstr_Operacion, lstr_NumValor + "-" + lstr_Nemotecnico, "G206");
                                                SinError = false;
                                                break;
                                            }

                                        }

                                    }

                                    //T

                                    //Amortizacion

                                    lstr_Monto = string.Empty;
                                    lds_Datos = new DataTable();
                                    ldec_MontoTotal = 0;
                                    reservasError = "";
                                    lstr_NuevoPosPrePago = string.Empty;
                                    ldat_Reservas = new DataSet();

                                    ldas_Tiras = tus_TipoAsiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "", lstr_Plazo, "AMORT", secuencia, lstr_OrderBy);
                                    ldat_Tira = ldas_Tiras.Tables["Table"];
                                    //strExprTitulo = (lstr_Nemotecnico.StartsWith("PT")) ? " ([CodigoAuxiliar2] = 'PT') " : " ([CodigoAuxiliar2] = 'AMORT') ";
                                    //ldat_Tira = ldat_Tira.Select(strExprTitulo).CopyToDataTable();


                                    //Validar que el pos pre sea diferente de PP_Balance y que el monto sea mayor al seleccionado




                                    foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                    {
                                        int index = ldat_Tira.Rows.IndexOf(ldr_Row);
                                        ldec_monto = ldec_DiferenciaCompradosColocados;

                                        ldec_monto = Math.Abs(ldec_monto);


                                        ldat_Reservas = consulta.ConsultarDinamico("select * from ma.reservasdetalle where idcuentacontable = '" + ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim() + "' and idpospre = '" + ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim() + "' AND IsNull(OrdenDeudaInterna,0) != 0  and LEFT(idprograma, 4) = year(getdate()) Order by OrdenDeudaInterna ASC");
                                        //ldat_Reservas = reservas.ConsultarReservasDetallado(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(), string.Empty, ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(), string.Empty, string.Empty, string.Empty, string.Empty);
                                        if (ldat_Reservas.Tables[0].Rows.Count != 0)
                                        {
                                            DataView dv = ldat_Reservas.Tables[0].DefaultView;
                                            dv.Sort = "OrdenDeudaInterna ASC";

                                            lds_Datos.Columns.Add("IdReserva");
                                            lds_Datos.Columns.Add("OrdenDeudaInterna");
                                            lds_Datos.Columns.Add("IdPosPre");
                                            lds_Datos.Columns.Add("Posicion");
                                            lds_Datos.Columns.Add("Monto");

                                            foreach (DataRow drForm in dv.ToTable().Rows)
                                            {
                                                //if (drForm["IdMoneda"].ToString().Trim().Equals(ldat_AsientoPago.Rows[0]["CodigoAuxiliar"].ToString().Trim()))
                                                if (!drForm["OrdenDeudaInterna"].ToString().Equals(string.Empty) && !drForm["OrdenDeudaInterna"].ToString().Equals("0"))
                                                {
                                                    lstr_Monto = asientos.ConsultaMontoReservaSAP(drForm["IdReserva"].ToString().Trim(), drForm["Posicion"].ToString().Trim());
                                                    lstr_Monto = lstr_Monto.Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal);
                                                    if (Convert.ToDecimal(lstr_Monto) > 0)
                                                    {
                                                        lds_Datos.Rows.Add(
                                                            drForm["IdReserva"].ToString(),
                                                            drForm["OrdenDeudaInterna"].ToString(),
                                                            drForm["IdPosPre"].ToString(),
                                                            drForm["Posicion"].ToString(),
                                                            lstr_Monto);
                                                        reservasError += "Posición Presupuestaria: " + drForm["IdPosPre"].ToString().Trim() + "Reserva :" + drForm["IdReserva"].ToString().Trim() + ", Posición: " + drForm["Posicion"].ToString().Trim() + "\n";
                                                        ldec_MontoTotal += Convert.ToDecimal(lstr_Monto);
                                                    }
                                                }
                                            }

                                            if (Convert.ToDecimal(ldec_MontoTotal) >= (Math.Abs(ldec_monto) * ldec_TipoCambio))
                                            {
                                                //Genera el asiento
                                                decimal ldec_Saldo = ldec_monto;

                                                foreach (DataRow drForm in lds_Datos.Rows)
                                                {
                                                    if (Convert.ToDecimal(drForm["Monto"]) > 0 && ldec_Saldo >= 0)
                                                    {
                                                        ldat_Asiento.Rows.Add(
                                                        lstr_NumValor + " " + lstr_Nemotecnico,
                                                        lstr_FchCanje.ToString("dd.MM.yyyy"),
                                                        ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                                        "CANJE GASTO DOLARES",
                                                        ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                                        drForm["IdReserva"].ToString().Trim(),
                                                        drForm["Posicion"].ToString().Trim(),
                                                        Truncate(ldec_Saldo > Convert.ToDecimal(drForm["Monto"].ToString()) ? Convert.ToDecimal(drForm["Monto"].ToString()) : ldec_Saldo, 2),
                                                        lstr_NumValor + "." + lstr_Nemotecnico,//pk
                                                        tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                                        lstr_Moneda,//tipo
                                                        lstr_Operacion +"."+lstr_NomOperacion//operacion
                                                        );


                                                    }

                                                    //Resta el saldo    
                                                    ldec_Saldo = ldec_Saldo - Convert.ToDecimal(drForm["Monto"]);
                                                }
                                            }
                                            else
                                            {
                                                //Almacena en bitácora de que no lo hizo

                                                bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion, "DI"), "Resultado de Contabilización: \n 1 - [E] Monto superior al total de las reservas de la Deuda Interna. Reservas utilizadas: \n" + reservasError, lstr_Operacion, lstr_NumValor + "-" + lstr_Nemotecnico, "G206");
                                                SinError = false;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            if (!ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim().StartsWith("E"))
                                            {
                                                ldat_Asiento.Rows.Add(
                                                lstr_NumValor + " " + lstr_Nemotecnico,
                                                lstr_FchCanje.ToString("dd.MM.yyyy"),
                                                ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                                "CANJE GASTO COLONES",
                                                ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                                ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                                Truncate(ldec_monto, 2),
                                                lstr_NumValor + "." + lstr_Nemotecnico,//pk
                                                tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                                lstr_Moneda,//tipo
                                                lstr_Operacion +"."+lstr_NomOperacion//operacion
                                                );


                                            }
                                            else
                                            {
                                                bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion, "DI"), "Resultado de Contabilización: \n 1 - [E] No hay reservas correspondientes a la cuenta " + ldat_Tira.Rows[0]["IdCuentaContable"].ToString().Trim() + " con fondo " + ldat_Tira.Rows[0]["IdPosPre"].ToString().Trim(), lstr_Operacion, lstr_NumValor + "-" + lstr_Nemotecnico, "G206");
                                                SinError = false;
                                                break;
                                            }

                                        }

                                    }

                                    // Amortizacion
#endregion
                                }
                            }

                        }
                        #endregion

                        //----------------------------------Fin de las regiones de monedas
                        if (SinError == true)
                        {
                            GenerarAsientoCanje(ldat_Asiento, lstr_Operacion, lstr_NumValor, lstr_Nemotecnico, lstr_FchCanje, ldec_TipoCambio);
                        }
                    }
                }
                else
                {
                    lstr_ResultadoAsientoCanjeSubasta = "Ya existe un asiento para esa fecha";
                }              
            }
            catch (Exception ex)
            {
                return lstr_ResultadoAsientoCanjeSubasta = "Error al generar el asiento";
                //string direccion = System.Configuration.ConfigurationManager.AppSettings["DireccionConfigs"];
                //direccion += "log.txt";
                //if (!System.IO.File.Exists(direccion))
                //    System.IO.File.Create(direccion).Dispose();

                //System.IO.File.AppendAllText(direccion, string.Format("{0}{1}", ex.ToString() + " / Valor: " + lint_NroValor.ToString() + " Nemo: " + lstr_Nemotecnico + " / Fecha: " + DateTime.Now.ToString(), Environment.NewLine));
            }
            return lstr_ResultadoAsientoCanjeSubasta;

        }//Fin de CrearAsientoCanje



        public string CrearAsientoSubasta(string FechaSubasta)
        {
            DataTable ldat_Valores = new DataTable();
            DataSet ldas_Tiras = new DataSet();
            DataSet ldas_TablaArriba = new DataSet();
            DataSet ldas_TransadoNetoComprados = new DataSet();
            DataSet ldas_TablaAbajo = new DataSet();
            DataTable ldat_TablaArriba = new DataTable();
            DataTable ldat_TablaAbajo = new DataTable();
            DataTable ldat_Tira = new DataTable();
            DataTable ldat_Colocados = new DataTable();
            DataSet ldas_Colocados = new DataSet();

            clsTituloValor lcls_TituloValor = new clsTituloValor();
            clsDevengoInteres lcls_DevengoInteres = new clsDevengoInteres();
            Mantenimiento.clsPropietarios lcls_Propietarios = new Mantenimiento.clsPropietarios();
            Mantenimiento.clsTiposAsiento lcls_TiposAsiento = new Mantenimiento.clsTiposAsiento();
            clsTiposAsiento tus_TipoAsiento = new clsTiposAsiento();
            clsDinamico consulta = new clsDinamico();
            clsTiposAsiento asientos = new clsTiposAsiento();

            string lstr_Nemotecnico = String.Empty;
            string lstr_Propietario = String.Empty;
            string lstr_Moneda = String.Empty;
            string lstr_NumValor = String.Empty;
            string lstr_Operacion = String.Empty;
            string lstr_NomOperacion = String.Empty;

            decimal ldec_monto = 0;


            DateTime lstr_FchCanje = Convert.ToDateTime(FechaSubasta);
            string lstr_Plazo = String.Empty;


            decimal ldec_ImportesDevengarArriba = 0;
            decimal ldec_InteresDevengarArriba = 0;
            //decimal ldec_PlazoValor = 0;
            //string lstr_PlazoValor = "";
            int lint_DiferenciaPlazoValor = 0;
            string lstr_Serie = String.Empty;
            string lstr_ModuloSINPE = "";
            decimal ldec_ValorFacial = 0;
            decimal ldec_ValorTransadoBruto = 0;
            decimal ldec_Prima = 0;
            decimal ldec_NetoBaja = 0;
            decimal ldec_NetoBajaDiferencia = 0;
            decimal ldec_ValorTransadoNeto = 0;
            string lstr_Tipo = String.Empty;
            decimal ldec_PrimaComprados = 0;
            decimal ldec_DescuentoComprados = 0;
            decimal ldec_BajaFaciales = 0;
            decimal ldec_CuponCorrido = 0;
            int lint_ContadorAsiento = 0;
            string lstr_SerieTitulo = String.Empty ;
            DateTime ltd_FchVencimiento = DateTime.Now;
            DateTime ltd_FchSubasta = DateTime.Now;
            bool lstr_Contabilizacion;
            bool SinError = true;
          
            DataTable ldat_Asiento = RegistroContable();
            DateTime ltd_FchValor = System.DateTime.Now;
            int lint_DiferenciaTrasciende = 0;
            int lint_AñoValor = 0;
            int lint_AñoVencimiento = 0;
            decimal ldec_Diferencia = 0;

            int? secuencia = null;
            string lstr_OrderBy = " ORDER BY Codigo, IdModulo, Secuencia";


            clsConsultaDevengoInteres devengoInteres = new clsConsultaDevengoInteres();
            DateTime _fechaValor = DateTime.Now;



            try
            {

              
                ldas_TablaAbajo = consulta.ConsultarDinamico("SELECT * FROM [cf].[CanjeEmisionDetalle] where Convert(Varchar(8),FchPago,112) = Convert(Varchar(8),(Convert(DATETIME,'" + lstr_FchCanje.ToShortDateString() + "',103)),112) ");
                ldat_TablaAbajo = ldas_TablaAbajo.Tables["Table"];
            
                for (int j = 0; j < ldat_TablaAbajo.Rows.Count; j++)
                {
                    lstr_Contabilizacion = Convert.ToBoolean(ldat_TablaAbajo.Rows[j]["Contabilizacion"]);
                    lstr_SerieTitulo = ldat_TablaAbajo.Rows[j]["NroEmisionSerie"].ToString();
                    ltd_FchSubasta = Convert.ToDateTime(ldat_TablaAbajo.Rows[j]["FchPago"]);

                    if (lstr_Contabilizacion == true)
                    {
                        lint_ContadorAsiento = 1;
                    }
                }
               
                if (lint_ContadorAsiento == 0)
                {

                    if (ltd_FchSubasta.ToString("yyyy.MM.dd") == lstr_FchCanje.ToString("yyyy.MM.dd"))
                    {

                 
                        var strExpr = "";
                        //Obtener titulos compra canje  ConsultarTituloValor(int? lint_NumValor, string lstr_Nemotecnico, string lint_NumCupon, string lstr_Garantia, string lstr_IndicadorCupon, string lstr_Tipo, string lstr_TipoNegociacion, string lstr_EstadoValor, DateTime ldt_FchInicio, DateTime ldt_FchFin, string lstr_NroEmisionSerie)
                        ldat_Valores = lcls_TituloValor.ConsultarTituloValor(null, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "Compra", "Vigente", Convert.ToDateTime(lstr_FchCanje.ToString("yyyy.MM.dd")), Convert.ToDateTime(lstr_FchCanje.ToString("yyyy.MM.dd")), lstr_SerieTitulo).Tables[0];

                        strExpr = " TipoNegociacion = 'Compra' AND [NroEmisionSerie]=  '" + lstr_SerieTitulo + "' AND FchValor = '" + lstr_FchCanje.ToString("yyyy.MM.dd") + "'";
                        ldat_Valores = ldat_Valores.Select(strExpr).CopyToDataTable();

                        for (int j = 0; j < ldat_Valores.Rows.Count; j++)
                        {
                            //lstr_FchCanje = Convert.ToDateTime(ldat_Valores.Rows[j]["FchValor"]);
                            lstr_Moneda = ldat_Valores.Rows[j]["Moneda"].ToString();
                        }

                        
                        decimal ldec_TipoCambioColones = Convert.ToDecimal(tipocambio.ConsultarTiposCambio("CRCN", lstr_FchCanje, "3140", "N").Tables[0].Rows[0]["Valor"].ToString());
                        decimal ldec_TipoCambioUDE = Convert.ToDecimal(tipocambio.ConsultarTiposCambio("UDE", lstr_FchCanje, "", "N").Tables[0].Rows[0]["Valor"].ToString());



                        ldas_TablaArriba = consulta.ConsultarDinamico("SELECT * FROM [cf].[CanjeResumenSerie] WHERE Convert(Varchar(8),FchCanje,112) = Convert(Varchar(8),(Convert(DATETIME,'" + lstr_FchCanje.ToShortDateString() + "',103)),112) AND RTRIM(REPLACE(IdentificadorCanje ,' ', ''))  = 'S' ");
                        ldat_TablaArriba = ldas_TablaArriba.Tables["Table"];

                        foreach (DataRow row in ldat_TablaArriba.Rows)
                        {
                            ldec_InteresDevengarArriba += Convert.ToDecimal(row["EmisionDarBaja"]);
                            ldec_ImportesDevengarArriba += Convert.ToDecimal(row["InteresBaja"]);
                            ldec_Prima += Convert.ToDecimal(row["InteresBajaPrima"]);
                          
                        }

                       
                       
                        decimal ldec_TipoCambio = lstr_Moneda.Equals("USD") ? ldec_TipoCambioColones : (lstr_Moneda.Equals("CRCN") ? 1 : ldec_TipoCambioUDE);

                        #region colones
                        //if (lstr_Moneda == "CRCN")
                        //{


                            if (lstr_Moneda == "USD")
                            {
                                lstr_Operacion = "ID68";
                            }
                            if (lstr_Moneda == "CRCN")
                            {
                                lstr_Operacion = "ID67";
                            }

                            DataSet lds_Operaciones = loperacion.ConsultarOperaciones(lstr_Operacion, "IdModulo IN ('DI')", "");
                            if (lds_Operaciones.Tables.Count > 0 && lds_Operaciones.Tables["Table"].Rows.Count > 0)
                            {
                                lstr_NomOperacion = lds_Operaciones.Tables["Table"].Rows[0]["NomOperacion"].ToString().Trim();
                            }
                            ldat_Valores = lcls_TituloValor.ConsultarTituloValor(null, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, "Compra", "Vigente", Convert.ToDateTime(lstr_FchCanje.ToString("yyyy.MM.dd")), Convert.ToDateTime(lstr_FchCanje.ToString("yyyy.MM.dd")), String.Empty).Tables[0];
                            strExpr = " TipoNegociacion = 'Compra' AND FchValor = '" + lstr_FchCanje.ToString("yyyy.MM.dd") + "'"; 
                            ldat_Valores = ldat_Valores.Select(strExpr).CopyToDataTable();


                            for (int i = 0; i < ldat_Valores.Rows.Count; i++)
                            {

                                lstr_NumValor = ldat_Valores.Rows[i]["NroValor"].ToString();
                                lstr_Moneda = ldat_Valores.Rows[i]["Moneda"].ToString();
                                lstr_Nemotecnico = ldat_Valores.Rows[i]["Nemotecnico"].ToString();
                                lstr_Propietario = ldat_Valores.Rows[i]["Propietario"].ToString();
                                lstr_FchCanje = Convert.ToDateTime(ldat_Valores.Rows[i]["FchValor"]);
                                ltd_FchVencimiento = Convert.ToDateTime(ldat_Valores.Rows[i]["FchVencimiento"]);
                                lstr_ModuloSINPE = ldat_Valores.Rows[i]["ModuloSINPE"].ToString();
                                ldec_ValorFacial = Convert.ToDecimal(ldat_Valores.Rows[i]["ValorFacial"]);
                                ldec_ValorTransadoBruto = Convert.ToDecimal(ldat_Valores.Rows[i]["ValorTransadoBruto"]);
                                ldec_ValorTransadoNeto = Convert.ToDecimal(ldat_Valores.Rows[i]["ValorTransadoNeto"]);
                                lstr_Serie = ldat_Valores.Rows[i]["NroEmisionSerie"].ToString().Trim();
                                lstr_Tipo = ldat_Valores.Rows[i]["Tipo"].ToString().Trim();
                                lint_AñoValor = lstr_FchCanje.Year;
                                lint_AñoVencimiento = ltd_FchVencimiento.Year;


                                ldas_Colocados = consulta.ConsultarDinamico("SELECT Top 1 * FROM [cf].[TitulosValores] WHERE NroEmisionSerie = '" + lstr_Serie + "' AND TipoNegociacion != 'Compra' ORDER BY FchValor ASC");
                                ldat_Colocados = ldas_Colocados.Tables["Table"];

                                foreach (DataRow row in ldat_Colocados.Rows)
                                {
                                    ltd_FchValor = Convert.ToDateTime(row["FchValor"].ToString());
                                }


                                TimeSpan ts = lstr_FchCanje - ltd_FchValor;
                                lint_DiferenciaPlazoValor = ts.Days;

                                TimeSpan ts1 = lstr_FchCanje - ltd_FchVencimiento;
                                lint_DiferenciaTrasciende = ts1.Days;



                                ldas_TablaAbajo = consulta.ConsultarDinamico("EXEC cf.uspConsultarResumenSubasta '" + lstr_Serie + "','" + lstr_FchCanje.ToString("yyyy.MM.dd") + "'");
                                ldat_TablaAbajo = ldas_TablaAbajo.Tables["Table"];

                                for (int j = 0; j < ldat_TablaAbajo.Rows.Count; j++)
                                {
                                    ldec_Diferencia = Convert.ToDecimal(ldat_TablaAbajo.Rows[j]["Diferencia"]);
                                }




                                ldas_TablaArriba = consulta.ConsultarDinamico("SELECT * FROM [cf].[CanjeResumenSerie] WHERE [NroEmisionSerie] =  '" + lstr_Serie + "' and Convert(Varchar(8),FchCanje,112) = Convert(Varchar(8),(Convert(DATETIME,'" + lstr_FchCanje.ToShortDateString() + "',103)),112) and  RTRIM(REPLACE(IdentificadorCanje ,' ', '')) = 'S'");
                                ldat_TablaArriba = ldas_TablaArriba.Tables["Table"];

                                foreach (DataRow row in ldat_TablaArriba.Rows)
                                {
                                    ldec_NetoBaja = Convert.ToDecimal(row["NetoBaja"]);
                                    ldec_PrimaComprados = Convert.ToDecimal(row["InteresBajaPrima"]);
                                    ldec_DescuentoComprados = Convert.ToDecimal(row["InteresBaja"]);
                                    ldec_BajaFaciales = Convert.ToDecimal(row["PorcentajeEmision"]);
                                    ldec_BajaFaciales = Math.Abs(ldec_BajaFaciales);
                                    ldec_CuponCorrido = Convert.ToDecimal(row["EmisionDarBaja"]);
                                }


                                ldec_NetoBajaDiferencia = ldec_ValorTransadoNeto - ldec_NetoBaja;

                                if (lcls_Propietarios.ConsultarPropietarios(string.Empty, string.Empty, string.Empty, lstr_Propietario, "S").Tables[0].Rows.Count == 0)
                                {
                                    lstr_Propietario = "PRIVADO";
                                }
                                else
                                {
                                    lstr_Propietario = "PUBLICO";
                                }



                                if (lint_DiferenciaTrasciende <= 361)
                                {
                                    lstr_Plazo = "CP";

                                }
                                else
                                {
                                    lstr_Plazo = "LP";
                                }



                                var strExprTitulo = "";
                                string lstr_Monto = string.Empty;
                                DataTable lds_Datos = new DataTable();
                                decimal ldec_MontoTotal = 0;
                                string reservasError = "";
                                string lstr_NuevoPosPrePago = string.Empty;
                                DataSet ldat_Reservas = new DataSet();


                                // Vacias
                                string cod_aux2 = (lstr_Nemotecnico.StartsWith("PT") ? "PT" : "");
                                ldas_Tiras = tus_TipoAsiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "", "", "", secuencia, lstr_OrderBy);
                                ldat_Tira = ldas_Tiras.Tables["Table"];
                                strExprTitulo = " ([CodigoAuxiliar2] = '') ";
                                ldat_Tira = ldat_Tira.Select(strExprTitulo).CopyToDataTable();



                                foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                {
                                    int index = ldat_Tira.Rows.IndexOf(ldr_Row);
                                    ldec_monto = Math.Abs(ldec_ValorTransadoNeto);

                                    ldat_Asiento.Rows.Add(
                                        lstr_NumValor + " " + lstr_Nemotecnico,
                                        lstr_FchCanje.ToString("dd.MM.yyyy"),
                                        ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                        "SUBASTA INVERSA",
                                        ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                        ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                        Math.Round(ldec_monto, 2),
                                        lstr_NumValor + "." + lstr_Nemotecnico,//pk
                                        tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                        lstr_Moneda,//tipo
                                        lstr_Operacion +"."+lstr_NomOperacion //operacion
                                        );
                                }

                                // Vacias

                                // Normales

                                ldas_Tiras = tus_TipoAsiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", lstr_Nemotecnico, lstr_Propietario, "", lstr_Plazo, "", secuencia, lstr_OrderBy);
                                ldat_Tira = ldas_Tiras.Tables["Table"];

                                foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                {
                                    int index = ldat_Tira.Rows.IndexOf(ldr_Row);
                                    ldec_monto = 0;
                                    string IdClaveContable = ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim();
                                    string Tipo = ldat_Tira.Rows[index]["CodigoAuxiliar2"].ToString().Trim();
                                    switch (Tipo + IdClaveContable)
                                    {
                                        case "CAPITAL40":
                                            ldec_monto = ldec_BajaFaciales;
                                            break;
                                        case "IMP_DEV40":
                                            if (ldec_DescuentoComprados < 0)
                                            {
                                                ldec_monto = Math.Abs(ldec_DescuentoComprados);
                                            }
                                            else
                                            {
                                                ldec_monto = 0;
                                            }  
                                            break;
                                        case "IMP_DEV50":
                                            if (ldec_DescuentoComprados > 0)
                                            {
                                                ldec_monto = Math.Abs(ldec_DescuentoComprados);
                                            }
                                            else
                                            {
                                                ldec_monto = 0;
                                            }  
                                            break;
                                        case "INT_DEV40":
                                            ldec_monto = ldec_CuponCorrido;
                                            if (ldec_monto < 0)
                                            {
                                                bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion, "DI"), "Resultado de Contabilización: \n 1 - [E] Error en la naturaleza del monto: \n", String.Empty, lstr_NumValor + "-" + lstr_Nemotecnico, "G206");
                                                SinError = false;
                                                break;
                                            }
                                            break;
                                        case "PRIMAS40":
                                            if (ldec_PrimaComprados > 0)
                                            {
                                                ldec_monto = 0;
                                            }
                                            else
                                            {
                                                ldec_monto = Math.Abs(ldec_PrimaComprados); 
                                            }   
                                            break;
                                        case "PRIMAS50":
                                            if (ldec_PrimaComprados > 0)
                                            {
                                                ldec_monto = Math.Abs(ldec_PrimaComprados); 
                                            }
                                            else
                                            {
                                                ldec_monto = 0;
                                            } 
                                            break;
                                    }

                                    if (ldec_monto != 0)
                                    {
                                      


                                        ldat_Asiento.Rows.Add(
                                       lstr_NumValor + " " + lstr_Nemotecnico,
                                       lstr_FchCanje.ToString("dd.MM.yyyy"),
                                       ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                       ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                       ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                       "SUBASTA INVERSA",
                                       ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                       ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                       ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                       ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                       ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                       ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                       ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                       ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                       Math.Round(ldec_monto, 2),
                                        lstr_NumValor + "." + lstr_Nemotecnico,//pk
                                        tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                        lstr_Moneda,//tipo
                                        lstr_Operacion +"."+lstr_NomOperacion//operacion
                                        );

                                    }

                                }

                                // Normales


                                //T

                                ldas_Tiras = tus_TipoAsiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "", "", "", secuencia, lstr_OrderBy);
                                ldat_Tira = ldas_Tiras.Tables["Table"];
                          
                                //.........................................filtra el tipo de tira
                                if (lstr_Nemotecnico.StartsWith("PT"))
                                {
                                    strExprTitulo = " ([CodigoAuxiliar2] = 'PT') ";
                                    ldat_Tira = ldat_Tira.Select(strExprTitulo).CopyToDataTable();
                                }
                                else if (ldec_NetoBajaDiferencia < 0)
                                {
                                    strExprTitulo = " ([CodigoAuxiliar2] = 'TL_P') ";
                                    ldat_Tira = ldat_Tira.Select(strExprTitulo).CopyToDataTable();
                                }
                                else
                                {
                                    strExprTitulo = " ([CodigoAuxiliar2] = 'TL_D') ";
                                    ldat_Tira = ldat_Tira.Select(strExprTitulo).CopyToDataTable();
                                }




                                foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                {
                                    int index = ldat_Tira.Rows.IndexOf(ldr_Row);
                                    ldec_monto = Math.Abs(ldec_NetoBajaDiferencia);


                                    ldat_Reservas = consulta.ConsultarDinamico("select * from ma.reservasdetalle where idcuentacontable = '" + ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim() + "' and idpospre = '" + ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim() + "' AND IsNull(OrdenDeudaInterna,0) != 0  and LEFT(idprograma, 4) = year(getdate()) Order by OrdenDeudaInterna ASC, IdReserva Desc");
                                    //ldat_Reservas = reservas.ConsultarReservasDetallado(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(), string.Empty, ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(), string.Empty, string.Empty, string.Empty, string.Empty);
                                    if (ldat_Reservas.Tables[0].Rows.Count != 0)
                                    {
                                        DataView dv = ldat_Reservas.Tables[0].DefaultView;
                                        dv.Sort = "OrdenDeudaInterna ASC";

                                        lds_Datos.Columns.Add("IdReserva");
                                        lds_Datos.Columns.Add("OrdenDeudaInterna");
                                        lds_Datos.Columns.Add("IdPosPre");
                                        lds_Datos.Columns.Add("Posicion");
                                        lds_Datos.Columns.Add("Monto");

                                        foreach (DataRow drForm in dv.ToTable().Rows)
                                        {
                                            //if (drForm["IdMoneda"].ToString().Trim().Equals(ldat_AsientoPago.Rows[0]["CodigoAuxiliar"].ToString().Trim()))
                                            if (!drForm["OrdenDeudaInterna"].ToString().Equals(string.Empty) && !drForm["OrdenDeudaInterna"].ToString().Equals("0"))
                                            {
                                                lstr_Monto = asientos.ConsultaMontoReservaSAP(drForm["IdReserva"].ToString().Trim(), drForm["Posicion"].ToString().Trim());
                                                lstr_Monto = lstr_Monto.Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal);
                                                if (Convert.ToDecimal(lstr_Monto) > 0)
                                                {
                                                    lds_Datos.Rows.Add(
                                                        drForm["IdReserva"].ToString(),
                                                        drForm["OrdenDeudaInterna"].ToString(),
                                                        drForm["IdPosPre"].ToString(),
                                                        drForm["Posicion"].ToString(),
                                                        lstr_Monto);
                                                    reservasError += "Posición Presupuestaria: " + drForm["IdPosPre"].ToString().Trim() + "Reserva :" + drForm["IdReserva"].ToString().Trim() + ", Posición: " + drForm["Posicion"].ToString().Trim() + "\n";
                                                    ldec_MontoTotal += Convert.ToDecimal(lstr_Monto);
                                                }
                                            }
                                        }
                                        //decimal x = (Math.Abs(ldec_monto) * ldec_TipoCambio);

                                        if (ldec_MontoTotal >= (Math.Abs(ldec_monto) * ldec_TipoCambio))
                                        {
                                            //Genera el asiento
                                            decimal ldec_Saldo = ldec_monto * ldec_TipoCambio;

                                            foreach (DataRow drForm in lds_Datos.Rows)
                                            {
                                                if (Convert.ToDecimal(drForm["Monto"]) > 0 && ldec_Saldo >= 0)
                                                {
                                                    // if (Convert.ToDecimal(drForm["Monto"]) >= (Math.Abs(ldec_Saldo))) //* ldec_TipoCambio))
                                                    // {


                                                    ldat_Asiento.Rows.Add(
                                                    lstr_NumValor + " " + lstr_Nemotecnico,
                                                    lstr_FchCanje.ToString("dd.MM.yyyy"),
                                                    ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                                    ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                                    ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                                    "SUBASTA INVERSA",
                                                    ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                                    ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                                    ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                                    ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                                    ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                                    ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                                    drForm["IdReserva"].ToString().Trim(),
                                                    drForm["Posicion"].ToString().Trim(),
                                                        //Truncate(ldec_monto, 2));
                                                    Truncate(ldec_Saldo > Convert.ToDecimal(drForm["Monto"].ToString()) ? Convert.ToDecimal(drForm["Monto"].ToString()) / ldec_TipoCambio : ldec_Saldo / ldec_TipoCambio, 2),
                                                    lstr_NumValor + "." + lstr_Nemotecnico,//pk
                                                    tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                                    lstr_Moneda,//tipo
                                                    lstr_Operacion +"."+lstr_NomOperacion//operacion
                                                    );

                                                    /* }
                                                     else
                                                     {

                                                         ldat_Asiento.Rows.Add(
                                                         lstr_NumValor + " " + lstr_Nemotecnico,
                                                         lstr_FchCanje.ToString("dd.MM.yyyy"),
                                                         ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                                         ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                                         ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                                         "SUBASTA INVERSA",
                                                         ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                                         ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                                         ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                                         ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                                         ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                                         ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                                         drForm["IdReserva"].ToString().Trim(),
                                                         drForm["Posicion"].ToString().Trim(),
                                                             //Truncate(ldec_monto, 2));
                                                         Truncate(ldec_Saldo > Convert.ToDecimal(drForm["Monto"].ToString()) ? Convert.ToDecimal(drForm["Monto"].ToString()) : ldec_Saldo, 2));

                                                     }*/
                                                    //Resta el saldo    
                                                    ldec_Saldo = ldec_Saldo - Convert.ToDecimal(drForm["Monto"]);
                                                }


                                            }
                                        }


                                        //if (Convert.ToDecimal(ldec_MontoTotal) >= (Math.Abs(ldec_monto) * ldec_TipoCambio))
                                        //{
                                        //    //Genera el asiento
                                        //    decimal ldec_Saldo = ldec_monto;

                                        //    foreach (DataRow drForm in lds_Datos.Rows)
                                        //    {
                                        //        if (Convert.ToDecimal(drForm["Monto"]) > 0 && ldec_Saldo >= 0)
                                        //        {
                                        //            ldat_Asiento.Rows.Add(
                                        //            lstr_NumValor + " " + lstr_Nemotecnico,
                                        //            lstr_FchCanje.ToString("dd.MM.yyyy"),
                                        //            ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                        //            ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                        //            ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                        //            "SUBASTA INVERSA",
                                        //            ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                        //            ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                        //            ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                        //            ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                        //            ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                        //            ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                        //            drForm["IdReserva"].ToString().Trim(),
                                        //            drForm["Posicion"].ToString().Trim(),
                                        //            Truncate(ldec_Saldo > Convert.ToDecimal(drForm["Monto"].ToString()) ? Convert.ToDecimal(drForm["Monto"].ToString()) : ldec_Saldo, 2));


                                        //        }

                                        //        //Resta el saldo    
                                        //        ldec_Saldo = ldec_Saldo - Convert.ToDecimal(drForm["Monto"]);
                                        //    }
                                        //}
                                        else
                                        {
                                            //Almacena en bitácora de que no lo hizo

                                            bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion, "DI"), "Resultado de Contabilización: \n 1 - [E] Monto superior al total de las reservas de la Deuda Interna. Reservas utilizadas: \n" + reservasError, lstr_Operacion, lstr_NumValor + "-" + lstr_Nemotecnico, "G206");
                                            SinError = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (!ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim().StartsWith("E"))
                                        {
                                            ldat_Asiento.Rows.Add(
                                            lstr_NumValor + " " + lstr_Nemotecnico,
                                            lstr_FchCanje.ToString("dd.MM.yyyy"),
                                            ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                            "SUBASTA INVERSA",
                                            ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                            Truncate(ldec_monto, 2),
                                            lstr_NumValor + "." + lstr_Nemotecnico,//pk
                                            tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                            lstr_Moneda,//tipo
                                            lstr_Operacion +"."+lstr_NomOperacion//operacion
                                            );


                                        }
                                        else
                                        {
                                            bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion, "DI"), "Resultado de Contabilización: \n 1 - [E] No hay reservas correspondientes a la cuenta " + ldat_Tira.Rows[0]["IdCuentaContable"].ToString().Trim() + " con fondo " + ldat_Tira.Rows[0]["IdPosPre"].ToString().Trim(), lstr_Operacion, lstr_NumValor + "-" + lstr_Nemotecnico, "G206");
                                            SinError = false;
                                            break;
                                        }

                                    }

                                }

                                //T

                                //Amortizacion

                                lstr_Monto = string.Empty;
                                lds_Datos = new DataTable();
                                ldec_MontoTotal = 0;
                                reservasError = "";
                                lstr_NuevoPosPrePago = string.Empty;
                                ldat_Reservas = new DataSet();

                                ldas_Tiras = tus_TipoAsiento.ConsultarTiposAsiento("", "IdModulo IN ('DI')", lstr_Operacion, "", "", "", "", "", "", "", "AMORT", secuencia, lstr_OrderBy);
                                ldat_Tira = ldas_Tiras.Tables["Table"];
                                strExprTitulo = " ([CodigoAuxiliar5] = 'LP') ";
                                ldat_Tira = ldat_Tira.Select(strExprTitulo).CopyToDataTable();

                              


                                //Validar que el pos pre sea diferente de PP_Balance y que el monto sea mayor al seleccionado

                                foreach (DataRow ldr_Row in ldat_Tira.Rows)
                                {
                                    int index = ldat_Tira.Rows.IndexOf(ldr_Row);
                                    if (lstr_Tipo == "Cero Cupón")
                                    {
                                        ldec_monto = Math.Abs(ldec_ValorTransadoBruto);
                                    }
                                    else
                                    {
                                        ldec_monto = Math.Abs(ldec_ValorFacial);
                                    }



                                    ldat_Reservas = consulta.ConsultarDinamico("select * from ma.reservasdetalle where idcuentacontable = '" + ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim() + "' and idpospre = '" + ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim() + "' AND IsNull(OrdenDeudaInterna,0) != 0  and LEFT(idprograma, 4) = year(getdate()) Order by OrdenDeudaInterna ASC");
                                    //ldat_Reservas = reservas.ConsultarReservasDetallado(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(), string.Empty, ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(), string.Empty, string.Empty, string.Empty, string.Empty);
                                    if (ldat_Reservas.Tables[0].Rows.Count != 0)
                                    {
                                        DataView dv = ldat_Reservas.Tables[0].DefaultView;
                                        dv.Sort = "OrdenDeudaInterna ASC";

                                        lds_Datos.Columns.Add("IdReserva");
                                        lds_Datos.Columns.Add("OrdenDeudaInterna");
                                        lds_Datos.Columns.Add("IdPosPre");
                                        lds_Datos.Columns.Add("Posicion");
                                        lds_Datos.Columns.Add("Monto");

                                        foreach (DataRow drForm in dv.ToTable().Rows)
                                        {
                                            //if (drForm["IdMoneda"].ToString().Trim().Equals(ldat_AsientoPago.Rows[0]["CodigoAuxiliar"].ToString().Trim()))
                                            if (!drForm["OrdenDeudaInterna"].ToString().Equals(string.Empty) && !drForm["OrdenDeudaInterna"].ToString().Equals("0"))
                                            {
                                                lstr_Monto = asientos.ConsultaMontoReservaSAP(drForm["IdReserva"].ToString().Trim(), drForm["Posicion"].ToString().Trim());
                                                lstr_Monto = lstr_Monto.Replace(",", lstr_separador_decimal).Replace(".", lstr_separador_decimal);
                                                if (Convert.ToDecimal(lstr_Monto) > 0)
                                                {
                                                    lds_Datos.Rows.Add(
                                                        drForm["IdReserva"].ToString(),
                                                        drForm["OrdenDeudaInterna"].ToString(),
                                                        drForm["IdPosPre"].ToString(),
                                                        drForm["Posicion"].ToString(),
                                                        lstr_Monto);
                                                    reservasError += "Posición Presupuestaria: " + drForm["IdPosPre"].ToString().Trim() + "Reserva :" + drForm["IdReserva"].ToString().Trim() + ", Posición: " + drForm["Posicion"].ToString().Trim() + "\n";
                                                    ldec_MontoTotal += Convert.ToDecimal(lstr_Monto);
                                                }
                                             }
                                        }

                                        if (Convert.ToDecimal(ldec_MontoTotal) >= (Math.Abs(ldec_monto) * ldec_TipoCambio))
                                        {
                                            //Genera el asiento
                                            decimal ldec_Saldo = ldec_monto * ldec_TipoCambio;

                                            foreach (DataRow drForm in lds_Datos.Rows)
                                            {
                                                if (Convert.ToDecimal(drForm["Monto"]) > 0 && ldec_Saldo >= 0)
                                                {
                                                   // if (Convert.ToDecimal(drForm["Monto"]) >= (Math.Abs(ldec_Saldo))) //* ldec_TipoCambio))
                                                   // {

                                                 
                                                    ldat_Asiento.Rows.Add(
                                                    lstr_NumValor + " " + lstr_Nemotecnico,
                                                    lstr_FchCanje.ToString("dd.MM.yyyy"),
                                                    ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                                    ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                                    ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                                    "SUBASTA INVERSA",
                                                    ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                                    ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                                    ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                                    ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                                    ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                                    ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                                    drForm["IdReserva"].ToString().Trim(),
                                                    drForm["Posicion"].ToString().Trim(),
                                                    //Truncate(ldec_monto, 2));
                                                    Truncate(ldec_Saldo > Convert.ToDecimal(drForm["Monto"].ToString()) ? Convert.ToDecimal(drForm["Monto"].ToString()) / ldec_TipoCambio : ldec_Saldo / ldec_TipoCambio, 2),
                                                    lstr_NumValor + "." + lstr_Nemotecnico,//pk
                                                    tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                                    lstr_Moneda,//tipo
                                                    lstr_Operacion +"."+lstr_NomOperacion//operacion
                                                    );

                                                   /* }
                                                    else
                                                    {

                                                        ldat_Asiento.Rows.Add(
                                                        lstr_NumValor + " " + lstr_Nemotecnico,
                                                        lstr_FchCanje.ToString("dd.MM.yyyy"),
                                                        ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                                        "SUBASTA INVERSA",
                                                        ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                                        ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                                        drForm["IdReserva"].ToString().Trim(),
                                                        drForm["Posicion"].ToString().Trim(),
                                                            //Truncate(ldec_monto, 2));
                                                        Truncate(ldec_Saldo > Convert.ToDecimal(drForm["Monto"].ToString()) ? Convert.ToDecimal(drForm["Monto"].ToString()) : ldec_Saldo, 2));

                                                    }*/
                                                    //Resta el saldo    
                                                    ldec_Saldo = ldec_Saldo - Convert.ToDecimal(drForm["Monto"]);
                                                }

                                               
                                            }
                                        }
                                        else
                                        {
                                            //Almacena en bitácora de que no lo hizo

                                            bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion, "DI"), "Resultado de Contabilización: \n 1 - [E] Monto superior al total de las reservas de la Deuda Interna. Reservas utilizadas: \n" + reservasError, lstr_Operacion, lstr_NumValor + "-" + lstr_Nemotecnico, "G206");
                                            SinError = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (!ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim().StartsWith("E"))                                            
                                        {
                                            ldat_Asiento.Rows.Add(
                                            lstr_NumValor + " " + lstr_Nemotecnico,
                                            lstr_FchCanje.ToString("dd.MM.yyyy"),
                                            ldat_Tira.Rows[index]["IdCuentaContable"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdClaveContable"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["CodigoAuxiliar"].ToString().Trim().Substring(0, 3),
                                            "SUBASTA INVERSA",
                                            ldat_Tira.Rows[index]["IdCentroCosto"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdCentroBeneficio"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdElementoPEP"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdPosPre"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdCentroGestor"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["IdFondo"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["DocPresupuestario"].ToString().Trim(),
                                            ldat_Tira.Rows[index]["PosDocPresupuestario"].ToString().Trim(),
                                            Truncate(ldec_monto, 2),
                                            lstr_NumValor + "." + lstr_Nemotecnico,//pk
                                            tira.get_operation_name(lstr_Operacion, "DI"),//texto2
                                            lstr_Moneda,//tipo
                                            lstr_Operacion +"."+lstr_NomOperacion//operacion
                                            );


                                        }
                                        else
                                        {
                                            bitacora.ufnRegistrarAccionBitacora("DI", "123", tira.get_operation_name(lstr_Operacion, "DI"), "Resultado de Contabilización: \n 1 - [E] No hay reservas correspondientes a la cuenta " + ldat_Tira.Rows[0]["IdCuentaContable"].ToString().Trim() + " con fondo " + ldat_Tira.Rows[0]["IdPosPre"].ToString().Trim(), lstr_Operacion, lstr_NumValor + "-" + lstr_Nemotecnico, "G206");
                                            SinError = false;
                                            break;
                                        }

                                    }

                                }
                                //Amortizacion




                            }

                        #endregion

                   
                   
                        
                        if (SinError == true)
                        {
                            
                            GenerarAsientoSubasta(ldat_Asiento, lstr_Operacion, lstr_NumValor, lstr_Nemotecnico, lstr_FchCanje, ldec_TipoCambio);
                         
                        }
                    }
                   
                }
                else
                {
                    lstr_ResultadoAsientoCanjeSubasta = "Ya existe un asiento para esa fecha";
                }
            }
          
            catch (Exception ex)
            {

                return lstr_ResultadoAsientoCanjeSubasta = "Error al generar el asiento";
                //string direccion = System.Configuration.ConfigurationManager.AppSettings["DireccionConfigs"];
                //direccion += "log.txt";
                //if (!System.IO.File.Exists(direccion))
                //    System.IO.File.Create(direccion).Dispose();

                //System.IO.File.AppendAllText(direccion, string.Format("{0}{1}", ex.ToString() + " / Valor: " + lint_NroValor.ToString() + " Nemo: " + lstr_Nemotecnico + " / Fecha: " + DateTime.Now.ToString(), Environment.NewLine));
            }

            return lstr_ResultadoAsientoCanjeSubasta;
        }


        public void CalcularCanjesEmision(string _NroSerie, string lstr_IdentificadorCanje, DateTime PeriodoAnterior, DateTime FechaCanje)
        {
            try
            {

                Mantenimiento.clsDinamico dinamico = new Mantenimiento.clsDinamico();
                dinamico.ConsultarDinamico("Insert Into [cf].[CanjeEmisionDetalle] " +
                                           "SELECT D.NroEmisionSerie, " +
                                           "	   D.NroValor,D.Nemotecnico,D.Periodo, " +
                                           "	   D.CostoAmortizacionInicial, " +
                                           "	   SUM (D.Interes), " +
                                           "       CostoAmortizacionInicial + sum(D.Interes) + sum(D.Cupon), " +
                                           "       Sum (D.Descuento), " +
                                           "       'SG',Getdate(),'SG',Getdate(), " +
                                           "       Sum (D.Cupon), '" + lstr_IdentificadorCanje + "', 0 " +
                                           "FROM   (SELECT B.NroEmisionSerie, B.NroValor, B.Nemotecnico, CONVERT (DATETIME, '" + FechaCanje.ToShortDateString() + "', 103) AS Periodo, " +
                                           "               C.CostoAmortizacionInicial, A.InteresTotal AS Interes,(C.CostoAmortizacionInicial + A.InteresTotal + A.Cupon) As final, " +
                                           "               A.Descuento AS Descuento, A.Cupon AS Cupon " +
                                           "        FROM [cf].[DevengosMensualesCanje] A " +
                                           "        INNER JOIN [cf].titulosvalores B ON A.NroValor = B.NroValor AND A.Nemotecnico = B.Nemotecnico " +
                                           "        INNER JOIN [cf].[DevengosInteresesCanje] C ON A.NroValor = C.NroValor AND A.Nemotecnico = C.Nemotecnico " +
                                           //"        WHERE B.IndicadorCupon = 'V' AND B.NroEmisionSerie = '" + _NroSerie + "' and C.Anno between (select top 1 X.* from (select fchvalor from cf.titulosvalores where nroemisionserie='" + _NroSerie + "' and tipoNegociacion='Compra' and fchvalor < CONVERT (date, '" + FechaCanje.ToShortDateString() + "', 103) union select '1900-01-01' as fchvalor) X order by fchvalor desc ) and CONVERT (date,'" + FechaCanje.ToShortDateString() + "', 103)) D " + 
                                           "        WHERE B.IndicadorCupon = 'V' AND B.NroEmisionSerie = '" + _NroSerie + "' and C.FchCanje = CONVERT(date,'" + FechaCanje.ToShortDateString() + "', 103)" +
                                           " and CONVERT(date,A.FchCanje,103) = CONVERT(date, '" + FechaCanje.ToShortDateString() + "', 103)) D " + 
                                           " group by  D.NroEmisionSerie,D.NroValor,D.Nemotecnico,D.CostoAmortizacionInicial,D.Periodo ");



                dinamico.ConsultarDinamico("Insert Into [cf].[CanjeEmision] " +
                                           "Select NroEmisionSerie, FchPago, Sum(CostoAmortizacionInicial), Sum(Intereses), Sum(CostoAmortizacionFinal), Sum(DescuentoDevengado), " +
                                           "       'SG',Getdate(),'SG',Getdate(), '" + lstr_IdentificadorCanje + "' " +
                                           "from   [cf].[CanjeEmisionDetalle] " +
                                           "WHERE NroEmisionSerie = '" + _NroSerie + "' AND Convert(Varchar(12),FchPago,112) = Convert(Varchar(12),(Convert(DATETIME,'" + FechaCanje.ToShortDateString() + "',103)),112) " +
                                           "group by NroEmisionSerie, FchPago ");

                dinamico.ConsultarDinamico("Exec [cf].[uspRegistrarCanjeEmisionResumen] '" + _NroSerie.Trim() + "','" + FechaCanje.ToShortDateString() + "','" + lstr_IdentificadorCanje+"'");

                if(lstr_IdentificadorCanje.Equals("C"))
                    dinamico.ConsultarDinamico("Exec [cf].[uspRegistrarResumenCanje] '" + _NroSerie.Trim() + "','" + lstr_IdentificadorCanje + "','" + FechaCanje.ToShortDateString() + "' "); 


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void AjusteCanjeDevengo(string lstr_IdentificadorCanje, string _Fecha)         
        {
            DataTable ldat_Valores = new DataTable();
            DataTable ldat_TituloSerie = new DataTable();
            DataTable ldat_InfoTituloSerie = new DataTable();
            clsTituloValor lcls_TituloValor = new clsTituloValor();
            DateTime ldt_FchInicio = Convert.ToDateTime(_Fecha);
            Mantenimiento.clsDinamico dinamico = new Mantenimiento.clsDinamico();
            DataSet dsFechas = new DataSet();
            try
            {
                //Obtener titulos compra canje
                ldat_Valores = lcls_TituloValor.ConsultarTituloValor(null, String.Empty, String.Empty, String.Empty, String.Empty, string.Empty, "Compra", "Vigente", ldt_FchInicio, ldt_FchInicio, String.Empty).Tables[0];
                string strExpr = " TipoNegociacion = 'Compra' ";
                if (lstr_IdentificadorCanje.Equals("C"))
                    strExpr += "and DescripcionNegociacion in ('Canje/Lici/Precio','Canje/Lici/Rend','Canje/Inversa/Precio','Canje/Inversa/Rend') ";
                else
                    strExpr += "and DescripcionNegociacion in ('Canje/Inversa/Precio','Canje/Inversa/Rend') ";

                ldat_Valores = ldat_Valores.Select(strExpr).CopyToDataTable();

                for (int i = 0; i < ldat_Valores.Rows.Count; i++)
                {
                    String lstr_NroEmisionSerie = ldat_Valores.Rows[i]["NroEmisionSerie"].ToString().Trim();
                    if (!string.IsNullOrEmpty(lstr_NroEmisionSerie))
                    {
                        //Obtener titulos pertenecen a serie compra 
                        ldat_TituloSerie = lcls_TituloValor.ConsultarTituloValor(null, String.Empty, String.Empty, String.Empty, "V", string.Empty, String.Empty, "Vigente", Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/5000"), lstr_NroEmisionSerie).Tables[0];
                        var strExprTitulo = " TipoNegociacion <> 'Compra' ";
                        ldat_TituloSerie = ldat_TituloSerie.Select(strExprTitulo).CopyToDataTable();

                        for (int i1 = 0; i1 < ldat_TituloSerie.Rows.Count; i1++)
                        {
                            int lint_NumValor = int.Parse(ldat_TituloSerie.Rows[i1]["NroValor"].ToString().Trim());
                            string lstr_Nemotecnico = ldat_TituloSerie.Rows[i1]["Nemotecnico"].ToString();

                            //contabiliza el devengo al día del canje
                            ContabilizaDevengoCanje(_Fecha, lstr_Nemotecnico, lint_NumValor.ToString());

                            dinamico.ConsultarDinamico("delete from cf.devengosintereses where nemotecnico = '" + lstr_Nemotecnico + "' and nrovalor = " + lint_NumValor + "");
                            dinamico.ConsultarDinamico("delete from cf.calculosflujoefectivo where nemotecnico = '" + lstr_Nemotecnico + "' and nrovalor = " + lint_NumValor + "");
                            dinamico.ConsultarDinamico("delete from cf.devengosmensuales where nemotecnico = '" + lstr_Nemotecnico + "' and nrovalor = " + lint_NumValor + "");

                            DevengoCeroCupon(lint_NumValor, lstr_Nemotecnico, "S");
                            DevengoTasaFija(lint_NumValor, lstr_Nemotecnico, "S");
                            DevengoTasaVariable(lint_NumValor, lstr_Nemotecnico, "S");

                            //obtiene las fechas de canje para ajustar el devengo del periodo en que hubo canje
                            dsFechas = dinamico.ConsultarDinamico("SELECT distinct e.FchCanje FROM cf.CanjeEmision e "+
                            "inner join cf.CanjeEmisionDetalle d ON e.NroEmisionSerie = d.NroEmisionSerie "+
                            "WHERE d.Nemotecnico ='"+lstr_Nemotecnico+"' AND d.NroValor = "+lint_NumValor+"  order by e.FchCanje");

                            //valida que no venga vacío y ejecuta el ajuste de las fechas de canje retornadas
                            if (dsFechas.Tables.Count > 0) 
                            {
                                if (dsFechas.Tables[0].Rows.Count != 0) 
                                {
                                   DateTime vFecha = new DateTime();
                                   foreach (DataRow row in dsFechas.Tables[0].Rows)
                                    {
                                        vFecha = Convert.ToDateTime(row["FchCanje"]);
                                        dinamico.ConsultarDinamico("exec cf.uspAjusteReporteDevengo '"+vFecha.ToString("dd/MM/yyyy")+"', '"+lstr_Nemotecnico+"', "+lint_NumValor);
                                   }
                                }//fin
                            }

                            /*
                            //consultor dinamico para sacar el ultimo porcentaje                            
                            float porcentaje = float.Parse(dinamico.ConsultarDinamico("select top 1 (100-PorcEmision)/100 as porcentaje FROM Cf.CanjeResumenSerie	WHERE NroEmisionSerie = '" + lstr_NroEmisionSerie + "' and Convert(Varchar(8),FchCanje,112) = Convert(Varchar(8),(Convert(DATETIME,'" + _Fecha + "',103)),112) order by fchcreacion desc").Tables[0].Rows[0]["porcentaje"].ToString());
                            //if de validacion para saber si la fecha de canje existe en el devengo
                            DataRow primerDevengoAfectado = dinamico.ConsultarDinamico("select top 1 * from cf.devengosmensuales where nrovalor = " + lint_NumValor + " and nemotecnico = '" + lstr_Nemotecnico + "' and convert(date,periodo,103) >= convert(date,'" + _Fecha + "',103) order by convert(date,periodo,103)").Tables[0].Rows[0];
                            DateTime periodo = DateTime.Parse(primerDevengoAfectado["Periodo"].ToString());
                            if (periodo.CompareTo(ldt_FchInicio) > 0)
                            {
                                //si no existe ingresar nueva row y hacer ajuste
                                int diasCalculados = Days360(periodo,ldt_FchInicio);
                                int diasOriginales = int.Parse(primerDevengoAfectado["DiasPeriodo"].ToString());
                                float interes = float.Parse(primerDevengoAfectado["InteresTotal"].ToString()) / diasOriginales * (diasOriginales - diasCalculados);
                                float cupon = float.Parse(primerDevengoAfectado["Cupon"].ToString()) / diasOriginales * (diasOriginales - diasCalculados);
                                float descuento = float.Parse(primerDevengoAfectado["Descuento"].ToString()) / diasOriginales * (diasOriginales - diasCalculados);
                                //Crea una nueva para la fecha de canje
                                dinamico.ConsultarDinamico(String.Format("INSERT INTO [cf].[DevengosMensuales] ( [NroValor],[NemoTecnico],[Periodo],[DiasPeriodo],[InteresTotal],[Cupon],[Descuento],[UsrCreacion],[FchCreacion],[UsrModifica],[FchModifica]) VALUES ({0},'{1}','{2}',{3},{4},{5},{6},'CANJE',GETDATE(),'CANJE',GETDATE())", lint_NumValor, lstr_Nemotecnico, _Fecha, diasOriginales - diasCalculados, interes, cupon, descuento));
                                //Edita la existente
                                dinamico.ConsultarDinamico(String.Format("update cf.devengosmensuales set diasperiodo = {0}, interestotal = interestotal * {1}/{2}, cupon = cupon * {1}/{2}, descuento = descuento * {1}/{2} where nrovalor = {3} and nemotecnico = '{4}' and convert(date,periodo,103) = convert(date,'{5}',103)", diasCalculados, diasCalculados, diasOriginales, lint_NumValor, lstr_Nemotecnico, primerDevengoAfectado["Periodo"].ToString()));
                            }                            
                            //update de montos para todo lo que este despues de la fecha de canje
                            dinamico.ConsultarDinamico("update cf.devengosmensuales set interestotal = interestotal * " + porcentaje + ", cupon = cupon * " + porcentaje + ", descuento = descuento * " + porcentaje + " where nrovalor = " + lint_NumValor + " and nemotecnico = '" + lstr_Nemotecnico + "' and convert(date,periodo,103) > convert(date,'" + _Fecha + "',103)");
                            */
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }        
        }

        public int Days360(DateTime date, DateTime initialDate)
        {
            var dateA = initialDate;
            var dateB = date;
            var dayA = dateA.Day;
            var dayB = dateB.Day;
            if (UltimoDiaDeFebrero(dateA) && UltimoDiaDeFebrero(dateB))
                dayB = 30;
            if (dayA == 31 && UltimoDiaDeFebrero(dateA))
                dayA = 30;
            if (dayA == 30 && dayB == 31)
                dayB = 30;
            int days = (dateB.Year - dateA.Year) * 360 + ((dateB.Month + 1) - (dateA.Month + 1)) * 30 + (dayB - dayA);
            return days;
        }
        private static bool UltimoDiaDeFebrero(DateTime date)
        {
            int lastDay = DateTime.DaysInMonth(date.Year, 2);
            return date.Day == lastDay;
        }


        /// <summary>
        /// Obtiene el porcentaje del ajuste del canje mediante una sumatoria de los porcentajes existences
        /// </summary>
        private static Decimal get_canje_percentaje(String nemotencnico, String nrovalor)
        {
            Decimal vPorcentaje = 0;
            try
            {
                DataTable porcentajes = dinamico.ConsultarDinamico("exec cf.uspGetPorcentajeEmision '" + nemotencnico + "', " + nrovalor + "").Tables[0];
                foreach (DataRow porcentaje in porcentajes.Rows)
                {
                    vPorcentaje = Decimal.Parse(porcentaje[0].ToString());
                }
                vPorcentaje = 1 - vPorcentaje;
            }
            catch(Exception ex)
            {
                
            }            
            return vPorcentaje;
        }

        private static DataTable ajusteTitulosCompra(DataTable ldat_Valores)
        {
            String lstr_Nemotecnico = "";
            String lint_NroValor = "";
            Decimal porcentajeCanje = 0;

            foreach (DataRow ldr_Valor in ldat_Valores.Rows)
            {      
                lstr_Nemotecnico = ldr_Valor["Nemotecnico"].ToString();
                lint_NroValor = ldr_Valor["NroValor"].ToString();
                porcentajeCanje = get_canje_percentaje(lstr_Nemotecnico, lint_NroValor);
                //Asignacion de nuevos valores
                ldr_Valor["ValorFacial"] = Convert.ToDecimal(ldr_Valor["ValorFacial"].ToString()) * porcentajeCanje;
                ldr_Valor["ValorTransadoNeto"] = Convert.ToDecimal(ldr_Valor["ValorTransadoNeto"].ToString()) * porcentajeCanje;
                ldr_Valor["ValorTransadoBruto"] = Convert.ToDecimal(ldr_Valor["ValorTransadoBruto"].ToString()) * porcentajeCanje;
            }
            return ldat_Valores;
        }

        /// <summary>
        /// Ingresa el ajuste de cada emision a la fecha indicada
        /// </summary>
        /// <param name="fechaFin"></param>
        public void AjustaHistoriaEmision(string fechaFin) 
        {
            DataTable Series = dinamico.ConsultarDinamico("SELECT DISTINCT NroEmisionSerie FROM cf.TitulosValores WHERE TipoNegociacion='Compra' AND NroEmisionSerie IS NOT NULL").Tables[0];

            foreach (DataRow vFila in Series.Rows) 
            {
                dinamico.ConsultarDinamico("exec cf.uspInsertaHistoriaEmision '" + vFila[0].ToString() + "', '" + fechaFin + "'");
            }
        }//fin


        /// <summary>
        /// Contabiliza el devengo el dia del canje
        /// </summary>
        /// <param name="lstr_FchFin"></param>
        /// <param name="lstr_pNemotecnico"></param>
        /// <param name="lstr_pNroValor"></param>
        public void ContabilizaDevengoCanje(string lstr_FchFin, string lstr_pNemotecnico, string lstr_pNroValor) 
        {
            DateTime? FchInicio = null;
            try {
                    FchInicio = DateTime.ParseExact(lstr_FchFin, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                catch (Exception ex)
                {
                
                }
                clsContabilizarDevengoInt devengo = new clsContabilizarDevengoInt();
                devengo.DevengoPorFecha(FchInicio, FchInicio, lstr_pNroValor, lstr_pNemotecnico, "");
        }

        #endregion
    }
}