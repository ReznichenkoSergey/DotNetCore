using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;
using MVCSample.Infrastructure.Configuration;
using MVCSample.Infrastructure.Services.Interfaces;
using Org.BouncyCastle.X509;
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
        private readonly IOptions<InfestationConfiguration> _config;

        public MessageService(IOptions<InfestationConfiguration> config)
        {
            _config = config;
        }

        public void SendMessage(string toAddress, MessageType messageType)
        {
            switch(messageType)
            {
                case MessageType.Sms:
                    SendSms(toAddress);
                    break;
                case MessageType.Email:
                    SendEmail(string.Empty, toAddress);
                    break;
            }
        }

        public void SendMessage(string text, string toAddress, MessageType messageType)
        {
            switch (messageType)
            {
                case MessageType.Sms:
                    SendSms(toAddress);
                    break;
                case MessageType.Email:
                    SendEmail(text, toAddress);
                    break;
            }
        }

        private void SendEmail(string content, string toAddress)
        {
            MimeMessage message = new MimeMessage();
            var from = new MailboxAddress(_config.Value.EmailConfig.SenderName, _config.Value.EmailConfig.SenderEmail);
            message.From.Add(from);
            //
            var to = new MailboxAddress(toAddress.Substring(0, toAddress.IndexOfAny(new char[] { '@' })), toAddress);
            message.To.Add(to);
            message.Subject = _config.Value.EmailConfig.Subject;// Configuration["RegistrationInfo:Title"];
            //
            BodyBuilder bodyBuilder = new BodyBuilder();
            if (string.IsNullOrEmpty(content))
            {
                bodyBuilder.TextBody = _config.Value.EmailConfig.TextContent;// Configuration["RegistrationInfo:TextSimple"];
                bodyBuilder.HtmlBody = _config.Value.EmailConfig.HtmlContent;// Configuration["RegistrationInfo:TextHtml"];
            }
            else
            {
                bodyBuilder.TextBody = content;
                bodyBuilder.HtmlBody = $"<h1>{content}</h1>";
            }
            message.Body = bodyBuilder.ToMessageBody();

            using(SmtpClient client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, ce, e) => true;
                //client.Connect(Configuration["SmtpServerName"], Configuration.GetValue<int>("SmtpServerPort"), true);
                client.Connect(_config.Value.EmailConfig.SmtpServer, _config.Value.EmailConfig.Port, true);
                //options.Value.GoogleSmtpServer
                client.Authenticate(_config.Value.EmailConfig.SenderEmail, _config.Value.EmailConfig.Password);

                client.Send(message);
                client.Disconnect(true);
            }
        }

        private void SendSms(string toPhone)
        {
            TwilioClient.Init(_config.Value.SmsConfig.AccountSid, _config.Value.SmsConfig.AuthToken);
            MessageResource.Create(
                body: _config.Value.SmsConfig.Content, //Configuration["RegistrationInfo:TextSimple"],
                from: new Twilio.Types.PhoneNumber(_config.Value.SmsConfig.PhoneSender),
                to: new Twilio.Types.PhoneNumber(toPhone)
            );
        }
    }
}
