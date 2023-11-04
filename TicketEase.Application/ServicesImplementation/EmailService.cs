using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using TicketEase.Application.Interfaces.Services;
using TicketEase.Domain.Entities;

namespace TicketEase.Application.ServicesImplementation
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings emailSettings;
        public EmailService(IOptions<EmailSettings> options)
        {
            this.emailSettings = options.Value;
        }
        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            var email = new MimeMessage();

            email.Sender = MailboxAddress.Parse(emailSettings.Email);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();

            byte[] fileBytes1;
            byte[] fileBytes2;

            if (System.IO.File.Exists("Attachment/img8.jpg") && System.IO.File.Exists("Attachment/img3.jpg"))
            {
                // Attach the first file
                using (var ms1 = new MemoryStream())
                {
                    using (FileStream file1 = new FileStream("Attachment/img8.jpg", FileMode.Open, FileAccess.Read))
                    {
                        file1.CopyTo(ms1);
                        fileBytes1 = ms1.ToArray();
                    }
                    builder.Attachments.Add("attachment1.jpg", fileBytes1, ContentType.Parse("application/octet-stream"));
                }

                // Attach the second file
                using (var ms2 = new MemoryStream())
                {
                    using (FileStream file2 = new FileStream("Attachment/img3.jpg", FileMode.Open, FileAccess.Read))
                    {
                        file2.CopyTo(ms2);
                        fileBytes2 = ms2.ToArray();
                    }
                    builder.Attachments.Add("attachment2.jpg", fileBytes2, ContentType.Parse("application/octet-stream"));
                }
            }

            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            try
            {
                smtp.Connect(emailSettings.Host, emailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(emailSettings.Email, emailSettings.Password);
                await smtp.SendAsync(email);
                // smtp.Disconnect(true);
            }
            catch (Exception ex)
            {

                throw new InvalidOperationException(ex.Message);
            }
            finally
            {
                smtp.Disconnect(true);

                smtp.Dispose();
            }
        }
    }
}
