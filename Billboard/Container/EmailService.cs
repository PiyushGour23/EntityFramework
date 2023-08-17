using Billboard.Helper;
using Billboard.Service;
using DocumentFormat.OpenXml.Wordprocessing;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MailKit.Net.Smtp;


namespace Billboard.Container
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings settings;
        public EmailService(IOptions<EmailSettings> options) 
        {
            this.settings = options.Value;
        }

        public async Task SendEmail(MailRequest mailRequest)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(settings.Email);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;

            var builder = new BodyBuilder();

            byte[] filebytes;
            if(System.IO.File.Exists("Attachments/Piyush_CV.pdf"))
            {
                FileStream file = new FileStream("Attachments/Piyush_CV.pdf", FileMode.Open, FileAccess.Read);
                using(var sr = new MemoryStream())
                {
                    file.CopyTo(sr);
                    filebytes = sr.ToArray();

                }
                builder.Attachments.Add("attachment.pdf", filebytes, ContentType.Parse("application/octet-stream"));
            }

            byte[] filebyte;
            if (System.IO.File.Exists("Attachments/TCS_Cares.pdf"))
            {
                FileStream file = new FileStream("Attachments/TCS_Cares.pdf", FileMode.Open, FileAccess.Read);
                using (var sr = new MemoryStream())
                {
                    file.CopyTo(sr);
                    filebyte = sr.ToArray();

                }
                builder.Attachments.Add("attachment2.pdf", filebyte, ContentType.Parse("application/octet-stream"));
            }

            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(settings.Host, settings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(settings.Email, settings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);


        }
    }
}
