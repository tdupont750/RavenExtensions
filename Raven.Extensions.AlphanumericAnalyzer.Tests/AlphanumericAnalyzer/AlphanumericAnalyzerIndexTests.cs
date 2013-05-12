using System.Linq;
using Raven.Client.Embedded;
using Raven.Client.Indexes;
using Xunit;
using Xunit.Extensions;

namespace Raven.Extensions.Tests.AlphanumericAnalyzer
{
    public class AlphanumericAnalyzerIndexTests : RavenIndexTest
    {
        [Theory]
        [InlineData(@"Text:Hello",              new[] { 0 })]
        [InlineData(@"Text:file_name",          new[] { 2 })]
        [InlineData(@"Text:name*",              new[] { 2, 3 })]
        [InlineData(@"Text:my AND Text:txt",    new[] { 2, 3 })]
        public void Query(string query, int[] expected)
        {
            int[] actual;

            using (var documentStore = NewDocumentStore<Index>())
            using (var session = documentStore.OpenSession())
            {
                actual = session.Advanced
                    .LuceneQuery<Doc>("Index")
                    .Where(query)
                    .SelectFields<int>("InternalId")
                    .WaitForNonStaleResults()
                    .ToArray();
            }

            Assert.Equal(expected, actual);
        }

        public static readonly string[] DocText = new[]
        {
            "Hello, world!",
            "Goodnight...moon?",
            "my_file_name_01.txt",
            "my_file_name01.txt"
        };

        protected override void PostInit(EmbeddableDocumentStore documentStore)
        {
            using (var session = documentStore.OpenSession())
            {
                for (int i = 0; i < DocText.Length; i++)
                    session.Store(new Doc
                    {
                        InternalId = i,
                        Text = DocText[i]
                    });

                session.SaveChanges();
            }
        }

        public class Doc
        {
            public int InternalId { get; set; }
            public string Text { get; set; }
        }

        public class Index : AbstractIndexCreationTask<Doc>
        {
            public Index()
            {
                Map = docs => from doc in docs
                              select new
                              {
                                  doc.InternalId,
                                  doc.Text
                              };

                Analyzers.Add(d => d.Text, "Raven.Extensions.AlphanumericAnalyzer, Raven.Extensions.AlphanumericAnalyzer");
            }
        }
    }
}