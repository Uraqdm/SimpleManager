using System.Net;
using System.Net.Mail;

namespace SimpleManager.Handlers
{
    class MailHandler
    {
        #region Properties

        private static MailAddress CompanyMailAddress { get; } 
        private static string CompanyMailPassword { get; }
        private static SmtpClient SmtpClient { get; }
        
        #endregion

        #region Public methods

        /// <summary>
        /// Отправляет сообщение на указанный электронный адресс с указанным сообщением.
        /// </summary>
        /// <param name="to">Кому отправить письмо</param>
        /// <param name="mesage">Текст письма</param>
        public static void SendMessage(MailAddress to, string message = "Test mail")
        {
            MailMessage sendingMessage = new MailMessage(CompanyMailAddress, to)
            {
                Body = message,
                Subject = "Регистрация в MyManager"
            };
            SmtpClient.Send(sendingMessage);
        }

        #endregion

        #region Constructors

        static MailHandler()
        {
            CompanyMailAddress = new MailAddress("uimanov.iun@kemerovorea.ru", "Test");
            CompanyMailPassword = "8TJ1rzt6";
            SmtpClient = new SmtpClient("smtp.mail.ru", 587)
            {
                Credentials = new NetworkCredential(CompanyMailAddress.ToString(), CompanyMailPassword),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
        }

        #endregion
    }
}
