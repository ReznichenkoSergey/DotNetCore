using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MVCSample.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MVCSample.Infrastructure.Services.Implementations
{
    public class EmailMessageService : IMessageService<Email>
    {
        IConfiguration Configuration;

        public EmailMessageService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void SendMessage()
        {
            ///Add mail message
            MimeMessage message = new MimeMessage();
            var from = new MailboxAddress("Admin", Configuration["EmailAddress"]);
            message.From.Add(from);

            var to = new MailboxAddress("User", "sergeyrez.sr@gmail.com");
            message.To.Add(to);

            message.Subject = "Email from Admin";

            //Add email body
            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = "Hello from Admin!!!";
            bodyBuilder.HtmlBody = "<h3>Hello from Admin!!!</h3>";
            message.Body = bodyBuilder.ToMessageBody();

            using(SmtpClient client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, ce, e) => true;
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate(Configuration["EmailAddress"], Configuration["EmailPassword"]);

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
