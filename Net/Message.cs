using System;
using System.Collections.Generic;

namespace Net
{
    [Serializable]
    public class Message
    {
        public String name;
        public String msg;
        

        public Message(String name, String msg) // send string
        {
            this.name = name;
            this.msg = msg;
        }
		
        public Message(List<String> topics) // send list of chatrooms
        {
            name = "server";
            string tmp = null;
            foreach(String topic in topics)
            {
                tmp += topic;
                tmp += "\n";
            }
            msg = tmp;
        }

        public override string ToString()
        {
            return (string)this.name + ": " + (string)this.msg;
        }
    }
}
