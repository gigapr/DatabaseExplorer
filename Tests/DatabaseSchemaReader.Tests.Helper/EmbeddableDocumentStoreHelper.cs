using Raven.Client.Embedded;

namespace DatabaseSchemaReader.Tests.Helper
{
    public class EmbeddableDocumentStoreHelper
    {
        public static EmbeddableDocumentStore GetInMemorySession()
        {
            var documentStore = new EmbeddableDocumentStore
            {
                RunInMemory = true,
                UseEmbeddedHttpServer = true
            };

            documentStore.Initialize();

            return documentStore;
        } 
    }
}