using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MVCSample.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace MVCSample.Infrastructure.Services.Implementations
{
    public class MessageService : IMessageService
    {
        private readonly IConfiguration Configuration;

        public MessageService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void SendMessage(IMyMessage message)
        {
            if (message is Sms)
                SendSms(message as Sms);
            else if (message is Email)
                SendEmail(message as Email);
        }

        private void SendEmail(Email myMessage)
        {
            MimeMessage message = new MimeMessage();
            var from = new MailboxAddress(Configuration["EmailFromPerson"], Configuration["EmailAddress"]);
            message.From.Add(from);
            //
            var to = new MailboxAddress(myMessage.ToPerson, myMessage.ToAddress);
            message.To.Add(to);
            message.Subject = myMessage.Subject;
            //
            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = myMessage.TextContent;
            bodyBuilder.HtmlBody = myMessage.ContentHtml;
            message.Body = bodyBuilder.ToMessageBody();

            using(SmtpClient client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, ce, e) => true;
                client.Connect(Configuration["SmtpServerName"], Configuration.GetValue<int>("SmtpServerPort"), true);
                client.Authenticate(Configuration["EmailAddress"], Configuration["EmailPassword"]);

                client.Send(message);
                client.Disconnect(true);
            }
        }

        private void SendSms(Sms myMessage)
        {
            TwilioClient.Init(Configuration["TwilioAccountSid"], Configuration["TwilioAuthToken"]);

            MessageResource.Create(
                body: myMessage.TextContent,
                from: new Twilio.Types.PhoneNumber(Configuration["TwilioPhoneSender"]),
                to: new Twilio.Types.PhoneNumber(myMessage.ToAddress)
            );
        }
    }
}
