using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Raven.Client;
using Raven.Client.Embedded;
using Raven.Client.Indexes;
using Raven.Extensions.Tests.RavenIndex;
using Raven.Imports.Newtonsoft.Json;
using Xunit;

namespace Raven.Extensions.Tests.DateRange
{
    public class DateRangePerformanceIndexTests
        : RavenIndexTestBase<DateRangePerformanceIndexTests.Data>
    {
        private static readonly Random Random = new Random();

        [Fact]
        public void LongIsFasterThanString()
        {
            using (var session = DocumentStore.OpenSession())
                for (var i = 0; i < 10; i++)
                {
                    var random = Random.Next(-1000, 1000);
                    var dateTime = DateTime.Now.AddHours(random);

                    var stringResult = GetStringResult(session, dateTime);
                    var longResult = GetLongResult(session, dateTime);

                    Assert.Equal(stringResult.TotalResults, longResult.TotalResults);
                    Assert.True(stringResult.ElapsedMilliseconds >= longResult.ElapsedMilliseconds);

                    Trace.WriteLine(String.Empty);
                }
        }

        private Result GetStringResult(IDocumentSession session, DateTime dateTime)
        {
            var sw = Stopwatch.StartNew();

            RavenQueryStatistics stats;

            session.Query<Doc>("Index")
                .Statistics(out stats)
                .Customize(x => x.WaitForNonStaleResults(TimeSpan.FromSeconds(30)))
                .Where(m => m.Created > dateTime)
                .ToList();

            sw.Stop();

            var message = String.Format("String - TotalResults: {0} - ElapsedMilliseconds: {1}", stats.TotalResults, sw.ElapsedMilliseconds);
            Trace.WriteLine(message);

            return new Result
            {
                ElapsedMilliseconds = sw.ElapsedMilliseconds,
                TotalResults = stats.TotalResults
            };
        }

        private Result GetLongResult(IDocumentSession session, DateTime dateTime)
        {
            var dateTimeLong = CustomJsonLuceneDateTimeConverter.DateToString(dateTime).Replace("\"", String.Empty);
            var lucene = String.Format("Modified_Range: [Lx{0} TO NULL]", dateTimeLong);

            var sw = Stopwatch.StartNew();

            var results = session.Advanced
                .LuceneQuery<Doc>("Index")
                .Where(lucene)
                .WaitForNonStaleResults()
                .QueryResult;

            sw.Stop();

            var message = String.Format("Long - TotalResults: {0} - ElapsedMilliseconds: {1}", results.TotalResults, sw.ElapsedMilliseconds);
            Trace.WriteLine(message);

            return new Result
            {
                ElapsedMilliseconds = sw.ElapsedMilliseconds,
                TotalResults = results.TotalResults
            };
        }

        public class Result
        {
            public int TotalResults { get; set; }
            public long ElapsedMilliseconds { get; set; }
        }

        public class Data : RavenIndexDataBase<Index, Doc>
        {
            protected override ICollection<Doc> Documents
            {
                get
                {
                    var documents = new List<Doc>();
                    var random = new Random();

                    for (var i = 1; i <= 20000; i++)
                    {
                        var r = random.Next(-1000, 1000);
                        var dateTime = DateTime.Now.AddHours(r);
                        documents.Add(new Doc
                        {
                            InternalId = i,
                            Created = dateTime,
                            Modified = dateTime
                        });
                    }

                    return documents;
                }
            }
        }

        public class Doc
        {
            public int InternalId { get; set; }

            public DateTime Created { get; set; }

            [JsonConverter(typeof (CustomJsonLuceneDateTimeConverter))]
            public DateTime Modified { get; set; }
        }

        public class Index : AbstractIndexCreationTask<Doc>
        {
            public Index()
            {
                Map = docs => from doc in docs
                              select new
                              {
                                  doc.InternalId,
                                  doc.Created,
                                  doc.Modified
                              };
            }
        }
    }
}
