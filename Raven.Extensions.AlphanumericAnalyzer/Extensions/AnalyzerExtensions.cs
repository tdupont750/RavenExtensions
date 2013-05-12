using System.Collections.Generic;
using System.IO;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Tokenattributes;

namespace Raven.Extensions
{
    public static class AnalyzerExtensions
    {
        public static IList<string> GetTokens(this Analyzer analyzer, string input, bool reuseStream = false)
        {
            const string fieldName = "testFieldName";

            var results = new List<string>();

            var stringReader = new StringReader(input);

            var tokenStream = reuseStream
                ? analyzer.ReusableTokenStream(fieldName, stringReader)
                : analyzer.TokenStream(fieldName, stringReader);

            var termAttr = tokenStream.GetAttribute<ITermAttribute>();

            while (tokenStream.IncrementToken())
            {
                var term = termAttr.Term;
                results.Add(term);
            }

            return results;
        }
    }
}
