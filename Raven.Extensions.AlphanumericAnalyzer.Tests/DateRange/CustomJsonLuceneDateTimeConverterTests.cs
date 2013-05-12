using System;
using System.Linq;
using Raven.Client.Embedded;
using Raven.Client.Indexes;
using Xunit;
using Xunit.Extensions;

namespace Raven.Extensions.Tests.DateRange
{
    public class CustomJsonLuceneDateTimeConverterTests : RavenIndexTest
    {
        [Theory]
        [InlineData(@"Created_Range:[{0} TO NULL]",     1,      new[] { 2, 3, 4 })]
        [InlineData(@"Created_Range:[{0} TO NULL]",     24,     new[] { 3, 4 })]
        [InlineData(@"Created_Range:[{0} TO NULL]",     48,     new[] { 4 })]
        [InlineData(@"Created_Range:{{{0} TO NULL}}",   1,      new[] { 3, 4, })]
        [InlineData(@"Created_Range:[NULL TO {0}]",     24,     new[] { 0, 1, 2, 3 })]
        [InlineData(@"Created_Range:[NULL TO {0}]",     48,     new[] { 0, 1, 2, 3 })]
        [InlineData(@"Created_Range:[NULL TO {0}]",     1,      new[] { 0, 1, 2 })]
        [InlineData(@"Created_Range:{{NULL TO {0}}}",   1,      new[] { 0, 1 })]
        [InlineData(@"Created_Range:[{0} TO {0}]",      1,      new[] { 2 })]
        [InlineData(@"Created_Range:[{0} TO {0}]",      24,     new[] { 3 })]
        public void Query(string queryFormat, int hours, int[] expected)
        {
            var dateTime = DateTime.Today.AddHours(hours);
            var luceneDateTime = "Lx" + CustomJsonLuceneDateTimeConverter.DateToString(dateTime);
            var query = String.Format(queryFormat, luceneDateTime);

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

        public static readonly DateTime[] DocText = new[]
        {
            DateTime.Today.AddSeconds(1),
            DateTime.Today.AddMinutes(1),
            DateTime.Today.AddHours(1),
            DateTime.Today.AddDays(1),
            DateTime.Today.AddDays(7)
        };

        protected override void PreInit(EmbeddableDocumentStore documentStore)
        {
            documentStore.Conventions.CustomizeJsonSerializer = s => s.Converters.Insert(0, CustomJsonLuceneDateTimeConverter.Instance);
        }

        protected override void PostInit(EmbeddableDocumentStore documentStore)
        {
            using (var session = documentStore.OpenSession())
            {
                for (int i = 0; i < DocText.Length; i++)
                    session.Store(new Doc
                    {
                        InternalId = i,
                        Created = DocText[i]
                    });

                session.SaveChanges();
            }
        }

        public class Doc
        {
            public int InternalId { get; set; }
            public DateTime Created { get; set; }
        }

        public class Index : AbstractIndexCreationTask<Doc>
        {
            public Index()
            {
                Map = docs => from doc in docs
                              select new
                              {
                                  doc.InternalId,
                                  doc.Created
                              };
            }
        }
    }
}
