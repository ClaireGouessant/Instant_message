namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            TCPClient c1 = new TCPClient("127.0.0.1", 8976);
            c1.startClient();
        }
    }
}
