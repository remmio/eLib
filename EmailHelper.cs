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
        public static string FromServeur { get; set; } = "from@server.com";


        /// <summary>
        /// 
        /// </summary>
        /// <param name="message">
        /// message.From=new MailAddress(FromServeur);
        /// message.To.Add(new MailAddress ("to@server.com"));
        /// message.Subject="Password Recover";
        /// message.Body="Message";        
        /// </param>
        public static void SendMailByGmail (MailMessage message) {
            var client = new SmtpClient
            {
                Host ="smtp.gmail.com",
                Port = 25,
                Timeout=10000,
                UseDefaultCredentials = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true,
                Credentials = new NetworkCredential("myemail@gmail.com", "password")
            };          
            message.BodyEncoding = Encoding.UTF8;
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                        
            client.Send(message);
        }





    }
}
