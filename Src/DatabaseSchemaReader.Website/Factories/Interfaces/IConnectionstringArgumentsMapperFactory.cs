using DatabaseSchemaReader.Website.Mappers.Interfaces;

namespace DatabaseSchemaReader.Website.Factories.Interfaces
{
    public interface IConnectionstringArgumentsMapperFactory
    {
        IConnectionstringArgumentsMapper Make(string databaseType);
    }
}