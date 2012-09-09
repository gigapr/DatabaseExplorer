using System;

namespace DatabaseSchemaReader.Contract.Exceptions
{
    public class ConnectionstringArgumentsException : ArgumentException
    {
        public ConnectionstringArgumentsException(string message) : base(message)
        {
        }
    }
}