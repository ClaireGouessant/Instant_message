using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Net
{
    public class MessageConnection
    {
        public static void sendMessage(Stream s, Message msg)// throws IOException
        {
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(s, msg);
            s.Flush();
        }

        public static Message getMessage(Stream s)// throws IOException
        {
            BinaryFormatter bf = new BinaryFormatter();
            return (Message)bf.Deserialize(s);
        }
    }
}
