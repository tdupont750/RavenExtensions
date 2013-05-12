using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Tokenattributes;

namespace Raven.Extensions.AnalyzerViewer
{
    public class TermFrequenciesView : AnalyzerView
    {
        public override string Name
        {
            get { return "Term Frequencies"; }
        }
        
        public override string GetView(TokenStream tokenStream, out int numberOfTokens)
        {
            var sb = new StringBuilder();
            var termDictionary = new Dictionary<string, int>();

            var termAttr = tokenStream.GetAttribute<ITermAttribute>();

            while (tokenStream.IncrementToken())
            {
                if (termDictionary.Keys.Contains(termAttr.Term))
                    termDictionary[termAttr.Term] = termDictionary[termAttr.Term] + 1;
                else
                    termDictionary.Add(termAttr.Term, 1);
            }

            foreach (var item in termDictionary.OrderBy(x => x.Key))
            {
                sb.Append(item.Key + " [" + item.Value + "]   ");
            }

            numberOfTokens = termDictionary.Count;
            return sb.ToString();
        }
    }
}
