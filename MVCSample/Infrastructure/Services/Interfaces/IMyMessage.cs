using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCSample.Infrastructure.Services.Interfaces
{
    public interface IMyMessage
    {
        string  TextContent { get; set; }
        string ToAddress { get; set; }
    }
}
