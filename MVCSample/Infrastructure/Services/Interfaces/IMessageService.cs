using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCSample.Infrastructure.Services.Interfaces
{
    public interface IMessageService
    {
        void SendMessage(IMyMessage message);
    }

    public class Email : IMyMessage
    {
        public string TextContent { get; set; }
        public string ContentHtml { get; set; }
        public string ToPerson { get; set; }
        public string ToAddress { get; set; }
        public string Subject { get; set; }

        public Email(string toAddress, string toPerson)
        {
            ToPerson = toPerson;
            ToAddress = toAddress;
        }
    }

    public class Sms : IMyMessage
    {
        public string TextContent { get; set; }
        public string ToAddress { get; set; }
        public Sms(string toPhone)
        {
            ToAddress = toPhone;
        }
    }
}
