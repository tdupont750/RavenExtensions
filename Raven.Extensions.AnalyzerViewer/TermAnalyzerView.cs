using System.Text;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Tokenattributes;

namespace Raven.Extensions.AnalyzerViewer
{
    public class TermAnalyzerView : AnalyzerView
    {
        public override string Name
        {
            get { return "Terms"; }
        }

        public override string GetView(TokenStream tokenStream, out int numberOfTokens)
        {
            var sb = new StringBuilder();
            numberOfTokens = 0;

            var termAttr = tokenStream.GetAttribute<ITermAttribute>();

            while (tokenStream.IncrementToken())
            {
                var view = "[" + termAttr.Term + "]   ";
                sb.Append(view);
                numberOfTokens++;
            }

            return sb.ToString();
        }
    }
}