using System.Net;
using System.Net.Mail;
using System.Text;

namespace CLib {

    /// <summary>
    /// Envoyeur des Emails
    /// </summary>
    public static class EmailHelper
    {

        /// <summary>
        /// 
        /// </summary>
        public static string FromServeur { get; set; } = "gestioninscriptions@gmail.com";


        /// <summary>
        /// Envoyer un Email
        /// </summary>
        /// <param name="message">
        /// message.From=new MailAddress(FromServeur);
        /// message.To.Add(new MailAddress ("to@server.com"));
        /// message.Subject="Password Recover";
        /// message.Body="Message";        
        /// </param>
        public static bool SendMailByGmail (MailMessage message) {
            try {
                var client = new SmtpClient {
                    Host="smtp.gmail.com",
                    Port=25,
                    Timeout=100000,
                    UseDefaultCredentials=false,
                    DeliveryMethod=SmtpDeliveryMethod.Network,
                    EnableSsl=true,
                    Credentials=new NetworkCredential("gestioninscriptions@gmail.com", "halidwalid")
                };
                message.From=new MailAddress(FromServeur);
                message.BodyEncoding=Encoding.UTF8;
                message.DeliveryNotificationOptions=DeliveryNotificationOptions.OnFailure;

                client.Send(message);
                return true;
            } catch (System.Exception ex) {
                DebugHelper.WriteException(ex);
                throw;
                //return false;
            }
        }





    }
}
