using System;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace HelpingHandsWeb.Services
{
    public class RegistrationService
    {
        private readonly string smtpServer = "smtp.gmail.com";
        private readonly string smtpUsername = "helpinghandsngo.team@gmail.com";
        private readonly string smtpPassword = "Team(12345)";
        private readonly int smtpPort = 587;

        public string GenerateTemporaryPassword(int passwordLength = 12)
        {
            const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+";

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] randomBytes = new byte[passwordLength];
                rng.GetBytes(randomBytes);

                StringBuilder sb = new StringBuilder(passwordLength);
                foreach (byte b in randomBytes)
                {
                    sb.Append(validChars[b % validChars.Length]);
                }

                return sb.ToString();
            }
        }

        public void SendVerificationEmail(string recipientEmail, string subject, string body)
        {
            SmtpClient client = new SmtpClient(smtpServer)
            {
                Port = smtpPort,
                Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                EnableSsl = true,
            };

            MailMessage mail = new MailMessage
            {
                From = new MailAddress(smtpUsername),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };

            mail.To.Add(recipientEmail);

            client.Send(mail);
        }
    }
}
