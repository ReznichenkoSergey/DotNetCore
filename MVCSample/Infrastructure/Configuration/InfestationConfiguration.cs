using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCSample.Infrastructure.Configuration
{
    public class InfestationConfiguration: IInfestationConfig
    {
        public EmailConfig EmailConfig { get; set; }
        public SmsConfig SmsConfig { get; set; }
    }

    public interface IInfestationConfig
    {
        EmailConfig EmailConfig { get; }
        SmsConfig SmsConfig { get; }
    }
}
