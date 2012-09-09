using System;
using DatabaseSchemaReader.WebHost.Exceptions.Interfaces;

namespace DatabaseSchemaReader.WebHost.Exceptions
{
    public class DatabaseTypeException : ArgumentException, IDatabaseTypeException
    {
        public DatabaseTypeException(string message) : base(message)
        {
            
        }
    }
}