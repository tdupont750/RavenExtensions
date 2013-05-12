using Lucene.Net.Analysis;

namespace Raven.Extensions.AnalyzerViewer
{
    public class AnalyzerInfo
    {
        public Analyzer LuceneAnalyzer { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public AnalyzerInfo(string name, string description, Analyzer analyzer)
        {
            Name = name;
            Description = description;
            LuceneAnalyzer = analyzer;
        }
    }
}