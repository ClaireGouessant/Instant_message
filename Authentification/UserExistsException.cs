using System;
using System.Runtime.Serialization;

namespace Authentification
{
    [Serializable]
    internal class UserExistsException : System.ArgumentException
    {
        public string login;

        public UserExistsException(string Alogin)
        {
            this.login = Alogin;
        }

        public UserExistsException(string message, string Alogin) : base(message)
        {
            this.login = Alogin;
        }

        public UserExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}