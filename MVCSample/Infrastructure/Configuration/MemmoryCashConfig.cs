using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCSample.Infrastructure.Configuration
{
    public class MemmoryCashConfig
    {
        public int ExpirationTimeValueInMinutes { get; set; }
        public int ScanningPeriodInMinutes { get; set; }
    }
}
