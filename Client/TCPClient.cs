using System;
using System.Net.Sockets;
using Authentification;
using Net;
using System.Threading;

namespace Client
{
    class TCPClient
    {
        private string hostname;
        private int port;
        private string name;

        public TCPClient(string h, int p)
        {
            hostname = h;
            port = p;
            name = "default";
        }

        public void startClient()
        {
            string path = @".\users.txt";
            AuthentificationManager am = new TextGestAuthentification(path);
            string response;
			
            do //Have you already an acount?
            {
                Console.WriteLine("1 Sign in \n2 Sign up");
                response = Console.ReadLine();
            } while (!response.Equals("1") && !response.Equals("2"));

            if (response.Equals("1")) // Have already an account
            {
                string pwd;
                Console.Write("Login: ");
                name = Console.ReadLine();
                Console.Write("Password: ");
                pwd = Console.ReadLine();
                am.authentify(name, pwd);
            }
            else // Does not have an account
            {
                name = am.createAccount();
            }

            try //Exchanges with the server
            {
                TcpClient comm = new TcpClient(hostname, port);

                Message menu = MessageConnection.getMessage(comm.GetStream());//Connection with the server
                Console.WriteLine("Connection established\n");
                String resp;
                
                Message choices = MessageConnection.getMessage(comm.GetStream());//Chatroom list
                Console.WriteLine("The opened topics are:");
                if (choices.msg == null) Console.WriteLine("nothing");
                else Console.WriteLine(choices.msg);

                Console.WriteLine(menu.msg);//Join or Create a chatroom?
                do {
                    resp = Console.ReadLine();
                } while (!resp.Equals("1") && !resp.Equals("2"));
                
                Message myChoice = new Message((String)name, resp);
                MessageConnection.sendMessage(comm.GetStream(), myChoice);

                Console.WriteLine(MessageConnection.getMessage(comm.GetStream()).msg);//nameof the chatroom?
                do { response = Console.ReadLine(); } while (response.Length == 0);
                MessageConnection.sendMessage(comm.GetStream(), new Message((String)name, response));

                Console.Clear();
                Thread destination = new Thread(getMessages);//thread to listen messages broadcasted by the serveur
                destination.Start();

                while (true)//Send to the serveur messages writed by the user
                {
                    string msg = Console.ReadLine();
                    Message message = new Message((String)name, (String)msg);
                    Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);//Move position in the console
                    Console.Write("\r");//Write the received message on the sended message
                    MessageConnection.sendMessage(comm.GetStream(), message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Connexion failed: ", e.Message);
            }
            finally
            {
                Pause();
            }
        }

        public void getMessages()//thread to listen messages broadcasted by the server
        {
            TcpClient comm = new TcpClient(hostname, port);
            while (true)
            {
                Message mes = MessageConnection.getMessage(comm.GetStream());
                Console.WriteLine(mes.ToString());
            }
        }

        public void Pause()
        {
            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }
    }
}
