using System;
using System.Collections;
using System.IO;

namespace Authentification
{
    [Serializable]
    public class TextGestAuthentification: AuthentificationManager
    {
        private Hashtable Users;
        private string path;

        public TextGestAuthentification(string path)
        {
            try
            {
                this.path = path;
                this.Users = new Hashtable();          
                this.Users = (Hashtable)SerialTools.Deserialize(path);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void addUser(string login, string password) // New user
        {
            if (Users.ContainsKey(login))
            {
                throw new UserExistsException(login);
            }
            else
            {
                Users.Add(login, password);
                save();
                Console.WriteLine(login + " has been added !");
            }
        }

        public void authentify(string login, string password) // Existing user
        {
            if (Users.ContainsKey(login))
            {
                if (Users[login].Equals(password))
                {
                    Console.WriteLine("Authentification OK !");
                }
                else
                {
                    throw new WrongPasswordException(login);
                }
            }
            else
            {
                throw new UserUnknownException(login);
            }
        }

        public void load() // Load users' data from a file
        {
            try
            {
                this.Users = (Hashtable)SerialTools.Deserialize(path);                
            }
            catch(IOException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void save() // Save users' data in a file
        {
            try
            {
                SerialTools.Serialize(path, Users);
                Console.WriteLine("Save completed");
            }
            catch(IOException e)
            {
                throw new IOException(e.ToString());
            }
        }

        public string createAccount() // New user
        {
            string log = "default";
            string pwd = "default";

            try
            {
                Console.WriteLine("Create an account: ");

                do
                {
                    Console.Write("Login: ");
                    log = Console.ReadLine();
                    Console.Write("Password: ");
                    pwd = Console.ReadLine();
                } while (log.Length == 0 && pwd.Length == 0);

                this.addUser(log, pwd);
                this.authentify(log, pwd);
            }
            catch (UserExistsException e)
            {
                Console.WriteLine(e.login + " has already been added !");
                return createAccount();
            }
            catch (UserUnknownException e)
            {
                Console.WriteLine(e.login + " is unknown ! error during the saving / loading.");
            }
            catch (WrongPasswordException e)
            {
                Console.WriteLine(e.login + " has provided an invalid password !");
                return createAccount();
            }
            catch (IOException e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine("authentified");
            return log;
        }
    }
}
