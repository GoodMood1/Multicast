using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

TcpClient client = new TcpClient();
client.Connect("127.0.0.1", 8080);
NetworkStream stream = client.GetStream();
Console.Write("Enter the username: ");
string us = Console.ReadLine();

byte[] data = Encoding.UTF8.GetBytes(us); 
stream.Write(data, 0, data.Length);

Task.Run(() =>
{
    byte[] buffer = new byte[1024];
    int byteCount = 0;
    string stringData = string.Empty;
    do
    {
        byteCount = stream.Read(buffer);
        stringData += Encoding.UTF8.GetString(buffer, 0, byteCount);
    } while (stream.DataAvailable);
    global::System.Console.WriteLine($"==={stringData}");
});

string message = string.Empty;
while (true)
{
    Console.Write(">>>");
    message = Console.ReadLine();
    data = Encoding.UTF8.GetBytes(message);
    stream.Write(data, 0, data.Length);
}