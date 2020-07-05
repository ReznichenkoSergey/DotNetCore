using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebSockets.Models.Classes
{
    public class MessageInputInfo
    {
        public string Login { get; set; }
        public string Content { get; set; }
        public bool Registered { get; set; }
    }
}
