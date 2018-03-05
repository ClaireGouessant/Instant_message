namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            TCPServer serv = new TCPServer(8976);
            serv.startServer();
        }
    }
}
