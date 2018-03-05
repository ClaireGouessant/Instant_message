using System;
using System.Collections.Generic;
using System.Linq;

namespace Chat_Project
{
    public class TextChatRoom : ChatRoom
    {
        public List<Chatter> listChatters;
        String topic;

        public TextChatRoom(String aTopic)
        {
            this.topic = aTopic;
            this.listChatters = new List<Chatter>();
        }
        
        public string getTopic()
        {
            return this.topic;
        }
        
        public void join(Chatter c) // Add a chatter to a chatroom
        {
            try
            {
                this.listChatters.Add(c);
                Console.WriteLine("(Message from Chatroom: " + this.getTopic() + " ) " + c.getAlias() + " has join the room.");
            }
            catch(NullReferenceException)
            {
                Console.WriteLine("Chatter NULL");
            }
        }

        public int count() // Number of chatrooms
        {
            return listChatters.Count();
        }

        public Chatter chatter (int i)
        {
            return listChatters[i];
        }
    }
}
