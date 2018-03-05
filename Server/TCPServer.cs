using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Net;
using Chat_Project;
using System.Collections;
using System.Collections.Generic;


namespace Server
{
    class TCPServer
    {
        private int port;

        public static ArrayList chatterList = new ArrayList();
        public static ArrayList TcpClientList = new ArrayList();

        public TCPServer(int p)
        {
            port = p;
        }

        public void startServer()
        {
            TcpListener l = new TcpListener(new IPAddress(new byte[] { 127, 0, 0, 1 }), port);
            l.Start();
            
            TopicsManager gt = new TextGestTopics();
            List<ChatRoom> ChatRoomsList = new List<ChatRoom>();
            
            while (true) // Exchanges with the clients 
            {
                try
                {
                    TcpClient comm = l.AcceptTcpClient(); // Connection with client
                    Console.WriteLine("Connection established @" + comm);
                    Console.WriteLine("Computing operation");

                    string response;
                    Message mes = new Message(gt.listTopics());
                    do // Send : Join or Create a Chatroom
                    {  // Send : Names of opened chatrooms
                        MessageConnection.sendMessage(comm.GetStream(), new Message("server", "1 Join a chatroom \n2 Create a chatroom"));
                        MessageConnection.sendMessage(comm.GetStream(), mes);

                        mes = MessageConnection.getMessage(comm.GetStream());
                        response = mes.msg;
                    } while (!response.Equals("1") && !response.Equals("2"));

                    Chatter chatter = new TextChatter(mes.name); // New user of a chatroom
                    chatterList.Add(chatter);
                    TcpClientList.Add(comm);

                    if (response.Equals("1")) // Join an existing chatroom
                    {
                        do
                        {
                            MessageConnection.sendMessage(comm.GetStream(), new Message("serveur", "Please, choose a chatroom: "));
                            mes = MessageConnection.getMessage(comm.GetStream());
                            if ((!(gt.IsInside(mes.msg)))) Console.WriteLine("This chatroom does not exist.");
                        } while (mes.msg.Length == 0 || (!(gt.IsInside(mes.msg))));
                    }
                    else // Create a chatroom
                    {
                        do
                        {
                            MessageConnection.sendMessage(comm.GetStream(), new Message("serveur", "Please, give a name to your topic: "));
                            mes = MessageConnection.getMessage(comm.GetStream());
                        } while (mes.msg.Length == 0 || gt.IsInside(mes.msg));

                        gt.createTopic(mes.msg);
                        ChatRoomsList.Add(gt.joinTopic(mes.msg));
                    }
                    
                    foreach(ChatRoom cr in ChatRoomsList)
                    {
                        if (mes.msg.Equals(cr.getTopic()))
                        {
                            cr.join(chatter); // Add the chatter in the chatroom
                            new Thread(new Receiver(comm, cr).run).Start(); // Create Threads to listen and send messages
                            TcpClient commGet = l.AcceptTcpClient();

                            chatterList.Add(chatter);
                            TcpClientList.Add(commGet);

                            new Thread(new Receiver(commGet, cr).run).Start();

                            Message join = new Message("server", "Welcome to " + mes.name + " in the " + mes.msg + " chatroom");
                            broadcast(join, cr);//send welcome message
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public static void broadcast(Message myMessage, ChatRoom cr) // Send to each client of a chatroom
        {
            for (int i = 0; i < cr.count(); i++)
            {
                for (int j = 0; j < TcpClientList.Count; j++)
                {
                    if (cr.chatter(i) == chatterList[j])
                    {
                        TcpClient client = (TcpClient)TcpClientList[j];
                        MessageConnection.sendMessage(client.GetStream(), myMessage);
                    }
                }
            }
        }
    }

    class Receiver // Thread to listen clients and broadcast to the chatroom
    {
        private TcpClient comm;
        private ChatRoom cr;

        public Receiver(TcpClient s, ChatRoom cr)
        {
            comm = s;
            this.cr = cr;
        }

        public void run()
        {
            while (true)
            {
                Message mes = MessageConnection.getMessage(comm.GetStream());// read expression
                Console.WriteLine(mes.ToString());
                TCPServer.broadcast(mes, cr);//send message to other members of the chatroom
            }
        }
    }
}
