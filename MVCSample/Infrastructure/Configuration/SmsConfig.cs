using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCSample.Infrastructure.Configuration
{
    public class SmsConfig
    {
        public string Content { get; set; }
        public string AccountSid { get; set; }
        public string AuthToken { get; set; }
        public string PhoneSender { get; set; }
    }
}
