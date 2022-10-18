using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using LogicaNegocio.wrServicioDTR;


namespace LogicaNegocio.CapturaIngresos
{
    
    public class clsCapturaIngresos
    {
        

        #region Parametros
            private double numeroletras;
            private double decimalesletras;
            private string cuentacliente;
            //DTR viejo
            private wrServicioDTR.DTR wsDTR = new wrServicioDTR.DTR();
            //DTR nuevo
            private wrServicioDTR1.DTR wsDTR1 = new wrServicioDTR1.DTR();

            //LogicaNegocio.srExisteReservaSAP.ZINT_CONF_EXTISTENCIA_RESERVAClient llamada = new LogicaNegocio.srExisteReservaSAP.ZINT_CONF_EXTISTENCIA_RESERVAClient("srExisteReservaSAP.ZINT_CONF_EXTISTENCIA_RESERVA");
            //LogicaNegocio.srExisteReservaSAP.ZINT_CONF_EXTISTENCIA_RESERVAClient llamada = new ZINT_CONF_EXTISTENCIA_RESERVAClient("binding");
            //LogicaNegocio.srExisteReservaSAP.ZintConfExtistenciaReserva metodo = new LogicaNegocio.srExisteReservaSAP.ZintConfExtistenciaReserva();
            //LogicaNegocio.srExisteReservaSAP.ZintConfExtistenciaReservaRequest request = new LogicaNegocio.srExisteReservaSAP.ZintConfExtistenciaReservaRequest();
            //LogicaNegocio.srExisteReservaSAP.ZintConfExtistenciaReservaResponse response = new LogicaNegocio.srExisteReservaSAP.ZintConfExtistenciaReservaResponse();
        #endregion

        #region Obtener y Asignar
            public double NumeroLetras
            {
                get { return numeroletras; }
                set { numeroletras = value; }
            }
            public double DecimalesLetras
            {
                get { return decimalesletras; }
                set { decimalesletras = value; }
            }
            public string CuentaCliente
            {
                get { return cuentacliente; }
                set { cuentacliente = value; }
            }
        #endregion
        
        #region Metodos
        /// <summary>
        /// Metodo que convierte cualquier numero ingresado por el usuario a su expresion en letras
        /// </summary>
        /// <param name="numeroletras">
        /// Variable numerica para realizar su expresión en letras
        /// </param>
        /// <returns>
        /// Retorna string con la expresión del numero en letras
        /// </returns>
            public string ufnConvertirMontoLetras(decimal numeroletras)
            {
                string Num2Text = "";
                decimal decimalesletras = 0;
                decimalesletras = numeroletras - Math.Truncate(numeroletras);
                numeroletras = Math.Truncate(numeroletras);                
                try
                {
                    if (numeroletras == 0) Num2Text = "CERO";
                    else if (numeroletras == 1) Num2Text = "UNO";
                    else if (numeroletras == 2) Num2Text = "DOS";
                    else if (numeroletras == 3) Num2Text = "TRES";
                    else if (numeroletras == 4) Num2Text = "CUATRO";
                    else if (numeroletras == 5) Num2Text = "CINCO";
                    else if (numeroletras == 6) Num2Text = "SEIS";
                    else if (numeroletras == 7) Num2Text = "SIETE";
                    else if (numeroletras == 8) Num2Text = "OCHO";
                    else if (numeroletras == 9) Num2Text = "NUEVE";
                    else if (numeroletras == 10) Num2Text = "DIEZ";
                    else if (numeroletras == 11) Num2Text = "ONCE";
                    else if (numeroletras == 12) Num2Text = "DOCE";
                    else if (numeroletras == 13) Num2Text = "TRECE";
                    else if (numeroletras == 14) Num2Text = "CATORCE";
                    else if (numeroletras == 15) Num2Text = "QUINCE";
                    else if (numeroletras < 20) Num2Text = "DIECI" + ufnConvertirMontoLetras(numeroletras - 10);
                    else if (numeroletras == 20) Num2Text = "VEINTE";
                    else if (numeroletras < 30) Num2Text = "VEINTI" + ufnConvertirMontoLetras(numeroletras - 20);
                    else if (numeroletras == 30) Num2Text = "TREINTA";
                    else if (numeroletras == 40) Num2Text = "CUARENTA";
                    else if (numeroletras == 50) Num2Text = "CINCUENTA";
                    else if (numeroletras == 60) Num2Text = "SESENTA";
                    else if (numeroletras == 70) Num2Text = "SETENTA";
                    else if (numeroletras == 80) Num2Text = "OCHENTA";
                    else if (numeroletras == 90) Num2Text = "NOVENTA";

                    else if (numeroletras < 100) Num2Text = ufnConvertirMontoLetras(Math.Truncate(numeroletras / 10) * 10) + " Y " + ufnConvertirMontoLetras(numeroletras % 10);
                    else if (numeroletras == 100) Num2Text = "CIEN";
                    else if (numeroletras < 200) Num2Text = "CIENTO " + ufnConvertirMontoLetras(numeroletras - 100);
                    else if ((numeroletras == 200) || (numeroletras == 300) || (numeroletras == 400) || (numeroletras == 600) || (numeroletras == 800)) Num2Text = ufnConvertirMontoLetras(Math.Truncate(numeroletras / 100)) + "CIENTOS";

                    else if (numeroletras == 500) Num2Text = "QUINIENTOS";
                    else if (numeroletras == 700) Num2Text = "SETECIENTOS";
                    else if (numeroletras == 900) Num2Text = "NOVECIENTOS";
                    else if (numeroletras < 1000) Num2Text = ufnConvertirMontoLetras(Math.Truncate(numeroletras / 100) * 100) + " " + ufnConvertirMontoLetras(numeroletras % 100);
                    else if (numeroletras == 1000) Num2Text = "MIL";
                    else if (numeroletras < 2000) Num2Text = "MIL " + ufnConvertirMontoLetras(numeroletras % 1000);
                    else if (numeroletras < 1000000)
                    {
                        Num2Text = ufnConvertirMontoLetras(Math.Truncate(numeroletras / 1000)) + " MIL";
                        if ((numeroletras % 1000) > 0) Num2Text = Num2Text + " " + ufnConvertirMontoLetras(numeroletras % 1000);
                    }

                    else if (numeroletras == 1000000) Num2Text = "UN MILLON";
                    else if (numeroletras < 2000000) Num2Text = "UN MILLON " + ufnConvertirMontoLetras(numeroletras % 1000000);
                    else if (numeroletras < 1000000000000)
                    {
                        Num2Text = ufnConvertirMontoLetras(Math.Truncate(numeroletras / 1000000)) + " MILLONES ";
                        if ((numeroletras - Math.Truncate(numeroletras / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + ufnConvertirMontoLetras(numeroletras - Math.Truncate(numeroletras / 1000000) * 1000000);
                    }
                    else if (numeroletras == 1000000000000) Num2Text = "UN BILLON";
                    else if (numeroletras < 2000000000000) Num2Text = "UN BILLON " + ufnConvertirMontoLetras(numeroletras - Math.Truncate(numeroletras / 1000000000000) * 1000000000000);
                    else
                    {
                        Num2Text = ufnConvertirMontoLetras(Math.Truncate(numeroletras / 1000000000000)) + " BILLONES";
                        if ((numeroletras - Math.Truncate(numeroletras / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + ufnConvertirMontoLetras(numeroletras - Math.Truncate(numeroletras / 1000000000000) * 1000000000000);
                    }
                    if (decimalesletras > 0){
                        string strdecimales = Convert.ToString(decimalesletras );
                        decimalesletras = Convert.ToDecimal(strdecimales.Substring(2) );

                        Num2Text = Num2Text + " CON " + ufnConvertirMontoLetras(decimalesletras) + " CENTIMOS ";


                    }
                }
                catch (Exception ex)
                { }
                return Num2Text;

            }

        /// <smary>
        /// Metodo verificador que valida si una cuenta cliente es valida para enviar al webservice DTR
        /// </summary>
        /// <param name="cuentacliente">
        /// Cuenta cliente para verificar
        /// </param>
        /// <returns>
        /// Retorna falso o verdadero si la cuenta cliente es valida o no
        /// </returns>
            public bool ufnCalcularDígitoVerificador(string cuentacliente)
            {
                //Constantes
                int modulo = 11; //Variable modulo definida en 11 por el requerimiento
                int[] pesos = new int[27] { 8, 9, 1, 2, 3, 4, 5, 6, 7, 8, 9, 1, 2, 3, 4, 5, 6, 7, 8, 9, 1, 2, 3, 4, 5, 6, 7 }; //Arreglo que contiene los pesos por digito hasta 27

                //Variables
                string cuentaC = cuentacliente; //numero de cuenta cliente digitado por el usuario
                char[] cuenta = cuentaC.ToCharArray(); //se convierte el string de la cuenta cliente en un array para ser recorrido
                int indiceUlt = cuenta.Length - 1; //cursor para obtener el ultimo digito
                int indiceC = cuenta.Length - 2; //cursor para recorrer el array de los digitos de la cuenta cliente
                int indiceP = 26; //cursor para recorrer el array de los pesos

                string aux1;
                int DigitoCuenta;
                int DigitoPeso;
                int ValorPosicion;
                int sumaDigitos = 0;
                int nDigito;
                string ultimoDato; //almacena el ulitmo digito de la cuenta cliente

                //Funcion
                try
                {
                    if (cuentacliente.Length == 17 || cuentacliente.Length == 27)
                    {
                        ultimoDato = Convert.ToChar(cuenta[indiceUlt]).ToString();
                        while (indiceC >= 0)
                        {
                            aux1 = Convert.ToChar(cuenta[indiceC]).ToString(); //convierte de ASCII al numero que corresponde
                            DigitoCuenta = pesos[indiceP];
                            DigitoPeso = Convert.ToInt16(aux1);
                            ValorPosicion = DigitoCuenta * DigitoPeso;
                            sumaDigitos = sumaDigitos + ValorPosicion;
                            indiceC--;
                            indiceP--;

                        }
                        nDigito = sumaDigitos % modulo;
                        if (nDigito == 10)
                        {
                            nDigito = 1;
                        }
                        if (nDigito == Convert.ToInt16(ultimoDato))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                { return false; }
            }

        /// <summary>
        /// Funcion que verifica la conexion con el web service del DTR
        /// </summary>
        /// <returns>
        /// Retorna true si el web service se encuentra habilitado
        /// </returns>
            public bool ufnServicioDisponible2()
            {
            //DTR viejo
                HttpWebRequest objRequest = (System.Net.HttpWebRequest)HttpWebRequest.Create(wsDTR.Url);
                HttpWebResponse myHttpWebResponse = (HttpWebResponse)objRequest.GetResponse();
                try
                {
                    if (myHttpWebResponse.StatusCode != HttpStatusCode.OK)
                    {
                        return false;

                    }
                    else
                    {
                        return true;
                    }
                }
                catch (Exception ex) 
                { return false; }
            }

        public bool ufnServicioDisponible()
        {
            //DTR nuevo
            HttpWebRequest objRequest = (System.Net.HttpWebRequest)HttpWebRequest.Create(wsDTR1.Url);
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)objRequest.GetResponse();
            try
            {
                //DTR nuevo *************************************************************
                ////Este código se debe de implementar si if (myHttpWebResponse.StatusCode != HttpStatusCode.OK) no funciona correctamente
                ////producto del cambio de DTR

                //bool servDisponible = true;
                //bool servDisponibleResult = true;
                //wrServicioDTR1.DTR wsDTR2 = new wrServicioDTR1.DTR();
                //wsDTR2.ServicioDisponible(out servDisponible, out servDisponibleResult);

                //if (servDisponible && servDisponibleResult)
                //{
                //    return true;
                //}
                //else
                //{
                //    return false;
                //}
                //****************************************************************************

                if (myHttpWebResponse.StatusCode != HttpStatusCode.OK)
                {
                    return false;

                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            { return false; }
        }
        /// <summary>
        /// Metodo que obtiene la informacion relacionada con la cuenta cliente y numero de cedula
        /// </summary>
        /// <param name="cuentacliente">
        /// Cuenta cliente o IBAN del cliente
        /// </param>
        /// <param name="cedula">
        /// Numero de cedula de la persona dueña de la cuenta cliente
        /// </param>
        /// <returns>
        /// Retorna true si los datos ingresados son validos
        /// </returns>
        public bool ufnObtenerInformacionDestino2(string cuentacliente, string cedula) 
            {
            //DTR viejo
                string nombre_cliente;
                try
                {
                    nombre_cliente = wsDTR.ObtenerInformacionDestino(cuentacliente, cedula).Nombre;
                    if (nombre_cliente == null)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }

                
            }

        public bool ufnObtenerInformacionDestino(string cuentacliente, string cedula)
        {
            string nombre_cliente;
            try
            {
                wrServicioDTR1.ObtenerInformacionCuentaRequestBody infoReq = new wrServicioDTR1.ObtenerInformacionCuentaRequestBody();
                infoReq.IBAN = cuentacliente;
                infoReq.Identificacion = cedula;
                wrServicioDTR1.ObtenerInformacionCuentaResponseBody Respuesta = new wrServicioDTR1.ObtenerInformacionCuentaResponseBody();
                Respuesta = wsDTR1.ObtenerInformacionCuenta(infoReq);

                nombre_cliente = Respuesta.ObtenerInformacionCuentaResult.Nombre;

               
                if (nombre_cliente == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reserva"></param>
        /// <returns></returns>
        public bool ufnExisteReservaPresupuestariaSAP(string reserva)
        {
            //metodo.IReserva = reserva;
            //request.ZintConfExtistenciaReserva = metodo;
            //llamada.ClientCredentials.UserName.UserName = System.Configuration.ConfigurationManager.AppSettings["SAP"];
            //llamada.ClientCredentials.UserName.Password = System.Configuration.ConfigurationManager.AppSettings["PASS_SAP"];
            //response = llamada.ZintConfExtistenciaReserva(metodo);

                return true;

        }

        #endregion

    }
}