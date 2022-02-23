using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpChat
{
    internal class UserMessageEventArgs
    {
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public UserMessageEventArgs(DateTime date,string message)
        {
            Date = date;
            Message = message;
        }
    }
}
