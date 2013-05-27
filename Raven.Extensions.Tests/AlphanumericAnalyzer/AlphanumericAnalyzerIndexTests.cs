using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using Raven.Client.Indexes;
using Raven.Extensions.Tests.RavenIndex;
using Xunit;
using Xunit.Extensions;

namespace Raven.Extensions.Tests.AlphanumericAnalyzer
{
    public class AlphanumericAnalyzerIndexTests
        : RavenIndexTestBase<AlphanumericAnalyzerIndexTests.Data>
    {
        [Theory]
        [InlineData(@"Text:Hello",              new[] {0})]
        [InlineData(@"Text:file_name",          new[] {2})]
        [InlineData(@"Text:name*",              new[] {2, 3})]
        [InlineData(@"Text:my AND Text:txt",    new[] {2, 3})]
        public void Query(string query, int[] expectedIds)
        {
            int[] actualIds;

            using (var session = DocumentStore.OpenSession())
            {
                actualIds = session.Advanced
                    .LuceneQuery<Doc>("Index")
                    .Where(query)
                    .SelectFields<int>("InternalId")
                    .WaitForNonStaleResults()
                    .ToArray();
            }

            Assert.Equal(expectedIds, actualIds);
        }

        public class Data : RavenIndexDataBase<Index, Doc>
        {
            protected override ICollection<Doc> Documents
            {
                get
                {
                    return new[]
                    {
                        "Hello, world!",
                        "Goodnight...moon?",
                        "my_file_name_01.txt",
                        "my_file_name01.txt"
                    }
                        .Select((t, i) => new Doc
                        {
                            InternalId = i,
                            Text = t
                        })
                        .ToArray();
                }
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