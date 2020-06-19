using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCSample.Infrastructure.Services.Interfaces
{
    public interface IMessageService<T>
    {
        void SendMessage();
    }

    public class Email { }

    public class Sms { }
}
