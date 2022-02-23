using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpChat
{
    internal class Server
    {
        public event EventHandler <MultiCastMessage> MultiCastRequest;
        private string _host;
        private int _port;
        private TcpListener tcpListener;
        List<User> _users = new List<User>();
        public Server(string host = "127.0.0.1",int port = 8080)
        {
            _host = host;
            _port = port;
        }
        internal void Start()
        {
            try
            {
                tcpListener = new TcpListener(IPAddress.Parse(_host), _port);
                tcpListener.Start();
                Console.WriteLine($"Server started: {_host}: {_port}");
                while (true)
                {
                    TcpClient client = tcpListener.AcceptTcpClient();
                    User u = new User(client);
                    u.MessageReceived += MessageReceivedHandler;
                    u.UserConnected += UserConnectHandler;
                   
                    MultiCastRequest += u.MultiCatHandler;
                    
                    _users.Add(u);

                    Task.Run(() => u.Proccesing());
                }
                
                //    Start listener
                //    Wait connection
                //    add user
                //    start handling
            }
            catch (Exception)
            {
                throw;
            }
        }
        private void MessageReceivedHandler(object? sender,UserMessageEventArgs e)
        {
            MultiCast(e.Message,sender as User,e.Date);
            Console.WriteLine(e.Date+" "+e.Message);
        }
        private void UserConnectHandler(object? sender, EventArgs e)
        {
            User? u =sender as User;
            Console.WriteLine($"user: {u.UserName} connected");
        }
        private void MultiCast(string message,User? author,DateTime date)
        {
            MultiCastRequest?.Invoke(null, new MultiCastMessage()
            {
                Message = message,
                MessageSender = author,
                Date = date,
            });
        }
        }
}
