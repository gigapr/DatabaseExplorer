using DatabaseSchemaReader.ConnectionstringBuilder.Validators.Interfaces;
using DatabaseSchemaReader.Contract.BusinessObjects.Interfaces;
using DatabaseSchemaReader.Contract.Exceptions;

namespace DatabaseSchemaReader.ConnectionstringBuilder.Validators
{
    public class ConnectionstringArgumentsValidator : IConnectionstringArgumentsValidator
    {
        public bool Validate(IConnectionstringArguments connectionstringArguments)
        {
            bool isValid;

            var message = string.Empty;

            if (string.IsNullOrEmpty(connectionstringArguments.DataSource))
                message = "DataSource can't be null or Empty";
            else if(string.IsNullOrEmpty(connectionstringArguments.DatabaseName))
                message = "DatabaseName can't be null or Empty";
            else if (string.IsNullOrEmpty(connectionstringArguments.Provider))
                message = "Provider can't be null or Empty";
            else if (string.IsNullOrEmpty(connectionstringArguments.Password) && !string.IsNullOrEmpty(connectionstringArguments.Username))
                message = "Password can't be null or Empty";
            else if (string.IsNullOrEmpty(connectionstringArguments.Username) && !string.IsNullOrEmpty(connectionstringArguments.Password))
                message = "Username can't be null or Empty";

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