namespace DatabaseSchemaReader.Website.Configurations.Interfaces
{
    public interface IWebsiteConfigurations
    {
        bool IsTestingEnviroment        { get; set; }
        string ConnectionstringName     { get; set; }
    }
}