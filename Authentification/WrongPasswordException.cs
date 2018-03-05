using System;
using System.Runtime.Serialization;

namespace Authentification
{
    [Serializable]
    internal class WrongPasswordException : Exception
    {
        public string login;

        public WrongPasswordException(string Alogin)
        {
            this.login = Alogin;
        }

        public WrongPasswordException(string message, string Alogin) : base(message)
        {
        }

        public WrongPasswordException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WrongPasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}