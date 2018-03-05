using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Authentification
{
    class SerialTools
    {
        public static void Serialize(string filename, object o)
        {
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryFormatter binf = new BinaryFormatter();
            binf.Serialize(fs, o);
            fs.Close();
        }

        public static object Deserialize(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open);
            BinaryFormatter binf = new BinaryFormatter();
            object o = binf.Deserialize(fs);
            fs.Close();
            return o;
        }
    }
}
