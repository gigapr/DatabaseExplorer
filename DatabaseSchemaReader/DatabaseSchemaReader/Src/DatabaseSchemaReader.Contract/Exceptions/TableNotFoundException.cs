using System;
using DatabaseSchemaReader.Contract.Exceptions.Interfaces;

namespace DatabaseSchemaReader.Contract.Exceptions
{
    public class TableNotFoundException : Exception, ITableNotFoundException 
    {
        public TableNotFoundException(string message) : base(message)
        {
            
        }
    }
}