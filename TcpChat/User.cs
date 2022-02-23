using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpChat
{
    internal class User
    {
        public event EventHandler<UserMessageEventArgs> MessageReceived;
        public event EventHandler<EventArgs> UserConnected;
        public string Id { get; private set; }
        private string _username;
        private TcpClient _tcpClient;
        private NetworkStream _stream;
        public User(TcpClient client)
        {
            _tcpClient = client;
            Id = Guid.NewGuid().ToString();
        }
        public string UserName
        {
            get{return _username;}
        }
        public void Proccesing()
        {
            try
            {
                _stream = _tcpClient.GetStream();
                _username = DecodeStreamDate();
                UserConnected?.Invoke(this, EventArgs.Empty);
                while (true)
                {
                    try
                    {
                        string message = DecodeStreamDate();
                        MessageReceived?.Invoke(this, new UserMessageEventArgs(DateTime.Now, message));
                    }
                    catch (Exception ex)
                    {
                        //todo: client error event
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                _tcpClient.Close();
            }
        }
        private string DecodeStreamDate()
        {
            byte[] buffer = new byte[1024];
            int byteCount = 0;
            string stringData = string.Empty;
            do
            {
                byteCount = _stream.Read(buffer);
                stringData += Encoding.UTF8.GetString(buffer,0,byteCount);
            } while (_stream.DataAvailable);
            return stringData;
        }
        internal void Disconnect()
        {
            Disconnect();
        }
        public void MultiCatHandler(object? sender,MultiCastMessage e)
        {
            if (Id != e.MessageSender.Id)
            {
                byte[] data = Encoding.UTF8.GetBytes($"{e.Date}, {e.MessageSender.UserName}, {e.Message}");
                _stream.Write(data, 0, data.Length);
            }

        }
    }
}
