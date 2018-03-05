using System;
using System.Runtime.Serialization;

namespace Authentification
{
    [Serializable]
    internal class UserUnknownException : Exception
    {
        public string login;

        public UserUnknownException(string Alogin)
        {
            this.login = Alogin;
        }

        public UserUnknownException(string message, string Alogin) : base(message)
        {
            this.login = Alogin;
        }

        public UserUnknownException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserUnknownException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}