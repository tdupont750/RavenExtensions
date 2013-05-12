using Raven.Client.Embedded;
using Raven.Client.Indexes;

namespace Raven.Extensions.Tests
{
    public abstract class RavenIndexTest
    {
        protected EmbeddableDocumentStore NewDocumentStore<T>()
            where T : AbstractIndexCreationTask, new()
        {
            var documentStore = new EmbeddableDocumentStore
            {
                Configuration =
                {
                    RunInUnreliableYetFastModeThatIsNotSuitableForProduction = true,
                    DefaultStorageTypeName = "munin",
                    RunInMemory = true
                }
            };

            PreInit(documentStore);
            documentStore.Initialize();
            PostInit(documentStore);

            var defaultIndex = new RavenDocumentsByEntityName();
            defaultIndex.Execute(documentStore);

            var customIndex = new T();
            customIndex.Execute(documentStore);

            return documentStore;
        }

        protected virtual void PreInit(EmbeddableDocumentStore documentStore)
        {
        }

        protected virtual void PostInit(EmbeddableDocumentStore documentStore)
        {
        }
    }
}