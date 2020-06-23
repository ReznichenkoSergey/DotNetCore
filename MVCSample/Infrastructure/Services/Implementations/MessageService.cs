using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MimeKit;
using MVCSample.Infrastructure.Configuration;
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
        private readonly IOptions<InfestationConfiguration> options;
        private readonly IOptions<EMailSenderInfo> optionsEmailConfig;
        private readonly IOptions<RegistrationInfo> optionsRegistrationInfo;
        private readonly IOptions<Configuration.Twilio> optionsTwillio;

        public MessageService(IConfiguration configuration, IOptions<InfestationConfiguration> options, 
            IOptions<EMailSenderInfo> optionsEmailConfig,
            IOptions<RegistrationInfo> optionsRegistrationInfo,
            IOptions<Configuration.Twilio> optionsTwillio)
        {
            Configuration = configuration;
            this.options = options;
            this.optionsEmailConfig = optionsEmailConfig;
            this.optionsRegistrationInfo = optionsRegistrationInfo;
            this.optionsTwillio = optionsTwillio;
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
            var from = new MailboxAddress(optionsEmailConfig.Value.Person, optionsEmailConfig.Value.Address);
            message.From.Add(from);
            //
            var to = new MailboxAddress(toAddress.Substring(0, toAddress.IndexOfAny(new char[] { '@' })), toAddress);
            message.To.Add(to);
            message.Subject = optionsRegistrationInfo.Value.Title;// Configuration["RegistrationInfo:Title"];
            //
            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = optionsRegistrationInfo.Value.TextSimple;// Configuration["RegistrationInfo:TextSimple"];
            bodyBuilder.HtmlBody = optionsRegistrationInfo.Value.TextHtml;// Configuration["RegistrationInfo:TextHtml"];
            message.Body = bodyBuilder.ToMessageBody();

            using(SmtpClient client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, ce, e) => true;
                //client.Connect(Configuration["SmtpServerName"], Configuration.GetValue<int>("SmtpServerPort"), true);
                client.Connect(options.Value.GoogleSmtpServer, options.Value.Port, true);
                //options.Value.GoogleSmtpServer
                client.Authenticate(optionsEmailConfig.Value.Address, optionsEmailConfig.Value.Password);

                client.Send(message);
                client.Disconnect(true);
            }
        }

        private void SendSms(string toPhone)
        {
            TwilioClient.Init(optionsTwillio.Value.TwilioAccountSid, optionsTwillio.Value.TwilioAuthToken);

            MessageResource.Create(
                body: optionsRegistrationInfo.Value.Title, //Configuration["RegistrationInfo:TextSimple"],
                from: new Twilio.Types.PhoneNumber(optionsTwillio.Value.TwilioPhoneSender),
                to: new Twilio.Types.PhoneNumber(toPhone)
            );
        }
    }
}
