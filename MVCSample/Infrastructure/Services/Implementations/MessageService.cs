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

        public void SendMessage(string toAddress, MessageType messageType)
        {
            switch(messageType)
            {
                case MessageType.Sms:
                    SendSms(toAddress);
                    break;
                case MessageType.Email:
                    SendEmail(toAddress);
                    break;
            }
        }

        private void SendEmail(string toAddress)
        {
            MimeMessage message = new MimeMessage();
            var from = new MailboxAddress(Configuration["EmailFromPerson"], Configuration["EmailAddress"]);
            message.From.Add(from);
            //
            var to = new MailboxAddress(toAddress.Substring(0, toAddress.IndexOfAny(new char[] { '@' })), toAddress);
            message.To.Add(to);
            message.Subject = Configuration["RegistrationInfo:Title"];
            //
            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = Configuration["RegistrationInfo:TextSimple"];
            bodyBuilder.HtmlBody = Configuration["RegistrationInfo:TextHtml"];
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

        private void SendSms(string toPhone)
        {
            TwilioClient.Init(Configuration["TwilioAccountSid"], Configuration["TwilioAuthToken"]);

            MessageResource.Create(
                body: Configuration["RegistrationInfo:TextSimple"],
                from: new Twilio.Types.PhoneNumber(Configuration["TwilioPhoneSender"]),
                to: new Twilio.Types.PhoneNumber(toPhone)
            );
        }
    }
}
