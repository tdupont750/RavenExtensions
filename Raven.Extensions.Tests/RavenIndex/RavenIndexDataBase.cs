using System;
using System.Collections.Generic;
using Raven.Client;
using Raven.Client.Embedded;
using Raven.Client.Indexes;

namespace Raven.Extensions.Tests.RavenIndex
{
    public abstract class RavenIndexDataBase<TIndex, TDoc>
        : IRavenIndexData
        where TIndex : AbstractIndexCreationTask, new()
    {
        private bool _isDisposed;

        public IDocumentStore DocumentStore { get; private set; }

        protected RavenIndexDataBase()
        {
            DocumentStore = NewDocumentStore();
        }

        ~RavenIndexDataBase()
        {
            Dispose(false);
        }

        protected abstract ICollection<TDoc> Documents { get; }

        private EmbeddableDocumentStore NewDocumentStore()
        {
            var documentStore = new EmbeddableDocumentStore
            {
                Configuration =
                {
                    RunInUnreliableYetFastModeThatIsNotSuitableForProduction = true,
                    RunInMemory = true
                }
            };

            PreInit(documentStore);
            documentStore.Initialize();
            PostInit(documentStore);

            // Create Default Index
            var defaultIndex = new RavenDocumentsByEntityName();
            defaultIndex.Execute(documentStore);

            // Create Custom Index
            var customIndex = new TIndex();
            customIndex.Execute(documentStore);

            // Insert Documents from Abstract Property
            using (var bulkInsert = documentStore.BulkInsert())
                foreach (var document in Documents)
                    bulkInsert.Store(document);

            return documentStore;
        }

        protected virtual void PreInit(EmbeddableDocumentStore documentStore)
        {
        }

        protected virtual void PostInit(EmbeddableDocumentStore documentStore)
        {
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (_isDisposed)
                return;

            if (disposing)
                GC.SuppressFinalize(this);

            if (DocumentStore != null)
            {
                DocumentStore.Dispose();
                DocumentStore = null;
            }

            _isDisposed = true;
        }
    }
}