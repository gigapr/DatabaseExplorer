using System;
using DatabaseSchemaReader.ConnectionstringBuilder.Extensions;
using DatabaseSchemaReader.ConnectionstringBuilder.Validators.Interfaces;
using DatabaseSchemaReader.Contract.BusinessObjects;
using DatabaseSchemaReader.Contract.BusinessObjects.Interfaces;
using DatabaseSchemaReader.Contract.Exceptions;

namespace DatabaseSchemaReader.ConnectionstringBuilder.Validators
{
    public class SqlServerConnectionstringArgumentsValidator : IConnectionstringArgumentsValidator
    {
        public bool Validate(IConnectionstringArguments connectionstringArguments)
        {
            bool isValid;

            var arguments = (SqlServerConnectionstringArguments)connectionstringArguments;

            var message = string.Empty;

            if (string.IsNullOrEmpty(arguments.DataSource))
            {
                message = "DataSource can't be null or Empty";
            }
            else if (string.IsNullOrEmpty(arguments.DatabaseName))
            {
                message = "DatabaseName can't be null or Empty";
            }
            else if (string.IsNullOrEmpty(arguments.Provider))
            {
                message = "Provider can't be null or Empty";
            }
            else if (string.IsNullOrEmpty(arguments.Password) && !string.IsNullOrEmpty(arguments.Username))
            {
                message = "Password can't be null or Empty";
            }
            else if (string.IsNullOrEmpty(arguments.Username) && !string.IsNullOrEmpty(arguments.Password))
            {
                message = "Username can't be null or Empty";
            }

            if (string.IsNullOrEmpty(message))
            {
                isValid = true;
            }
            else
            {
                throw new ConnectionstringArgumentsException(message);
            }

            return isValid;             
        }
    }
}