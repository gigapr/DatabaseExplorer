using DatabaseSchemaReader.Contract.BusinessObjects.Interfaces;

namespace DatabaseSchemaReader.ConnectionstringBuilder.Validators.Interfaces
{
    public interface IConnectionstringArgumentsValidator   
    {
        bool Validate(IConnectionstringArguments connectionstringArguments);
    }
}