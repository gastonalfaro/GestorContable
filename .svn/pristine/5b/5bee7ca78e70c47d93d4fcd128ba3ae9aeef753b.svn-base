using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Net.Mail;
using System.Text;
using System.Net.Mime;
using System.Web.UI.HtmlControls;

namespace LogicaNegocio.Consolidacion
{
    public class clsEnviarCorreoPC
    {

        public bool EnviarCorreoPC(int CorreoClientePort, string CorreoClienteHost, string CorreoNetworkCredentialUsuario, string CorreoNetworkCredentialPassWord, string str_CorreoFrom, string str_CorreoTo, string str_CorreoCC, string str_Mensaje, string str_Asunto)
        {
            string str_Resultado = String.Empty;
            try
            {
                // Command line argument must the the SMTP host.
                SmtpClient client = new SmtpClient();
                client.Port = CorreoClientePort; //25;
                client.Host = CorreoClienteHost; //"172.18.100.11";

                //client.EnableSsl = true;
                client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = true;
                //client.Credentials = new System.Net.NetworkCredential("hacienda\\scan", "hacienda01*");
                client.Credentials = new System.Net.NetworkCredential(CorreoNetworkCredentialUsuario, CorreoNetworkCredentialPassWord);

                MailMessage mm = new MailMessage();
                mm.From = new MailAddress(str_CorreoFrom);
                mm.To.Add(str_CorreoTo);
                mm.CC.Add(str_CorreoCC);
                mm.Subject = str_Asunto;
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(str_Mensaje,
                     Encoding.UTF8, MediaTypeNames.Text.Html);
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                mm.AlternateViews.Add(htmlView);
                mm.IsBodyHtml = true;
                mm.Priority = MailPriority.Normal;
                client.Send(mm);

                return true;
            }
            catch (Exception ex)
            {

                throw;

            }
            finally
            {

            }

        }


    }
}