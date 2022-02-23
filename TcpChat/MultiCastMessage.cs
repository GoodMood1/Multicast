using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpChat
{
    internal class MultiCastMessage
    {
        public User? MessageSender { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }

    }
}
