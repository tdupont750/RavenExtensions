using Raven.Client;
using Xunit;

namespace Raven.Extensions.Tests.RavenIndex
{
    public abstract class RavenIndexTestBase<T>
        : IUseFixture<T>
        where T : IRavenIndexData, new()
    {
        public void SetFixture(T data)
        {
            IndexTester = data;
        }

        public IDocumentStore DocumentStore
        {
            get { return IndexTester.DocumentStore; }
        }

        private T IndexTester { get; set; }
    }
}