using MailKit.Net.Smtp;
using MimeKit;

namespace Gateway.Services
{
    public static class EmailService
    {
        /// <summary>
        /// Sends a verification mail to an account.
        /// </summary>
        /// <param name="account">The account the mail needs to be send to.</param>
        /// <param name="verificationCode">The verification code that needs to be in the mail.</param>
        public static void SendVerificationMail(string email, string verificationCode)
        {
            MimeMessage mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress("Stugether", "no-reply@stugether.wafoe.nl"));
            mailMessage.To.Add(new MailboxAddress($"Stugether gebruiker", email));
            mailMessage.Subject = "Welkom bij Stugether! Dit is je verificatiecode.";
            mailMessage.Body = new TextPart("plain")
            {
                Text = $"Welkom bij het Stugether platform! We hopen dat je veel plezier gaat hebben met de applicatie. " +
                $"Vul je verificatiecode in bij het registreren. Je verificatiecode is: {verificationCode}"
            };

            using (SmtpClient smtpClient = new SmtpClient())
            {
                smtpClient.Connect("stugether.wafoe.nl", 465, true);
                smtpClient.Authenticate("no-reply@stugether.wafoe.nl", @"Z:`bX_H&FmJ)QX+AmxbkV\5;72&,N9~,");
                smtpClient.Send(mailMessage);
                smtpClient.Disconnect(true);
            }
        }
    }
}
