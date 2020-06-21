using MVCSample.Infrastructure.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCSample.Infrastructure.Services.Interfaces
{
    public interface IMessageService
    {
        void SendMessage(string toAddress, MessageType messageType);
    }
}
