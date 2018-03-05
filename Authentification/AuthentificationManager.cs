using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentification
{
    public interface AuthentificationManager
    {
        void addUser(string login, string password);
        void authentify(string login, string password);
        void save();
        void load();
        string createAccount();
    }
}
