using Lucene.Net.Analysis;

namespace Raven.Extensions.AnalyzerViewer
{
    public abstract class AnalyzerView
    {
        public abstract string Name { get; }

        public abstract string GetView(TokenStream tokenStream, out int numberOfTokens);
    }
}