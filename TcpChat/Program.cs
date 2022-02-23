using TcpChat;
try
{
    Server server = new Server();
    server.Start();
}
catch (Exception e) 
    {
    Console.WriteLine(e.Message);
    } 
