using Microsoft.Extensions.Configuration;
using MVCSample.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace MVCSample.Infrastructure.Services.Implementations
{
    public class SmsMessageService : IMessageService<Sms>
    {
        public IConfiguration Configuration { get; }

        public SmsMessageService(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void SendMessage()
        {
            TwilioClient.Init(Configuration["TwilioAccountSid"], Configuration["TwilioAuthToken"]);

            var message = MessageResource.Create(
                body: "Hi there!",
                from: new Twilio.Types.PhoneNumber("+13343267077"),
                to: new Twilio.Types.PhoneNumber("+380506946776")
            );
        }
    }
}
