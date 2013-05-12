using System;
using System.IO;
using Lucene.Net.Analysis;
using Lucene.Net.Util;

namespace Raven.Extensions
{
    public class AlphanumericTokenizer : CharTokenizer
    {
        public AlphanumericTokenizer(TextReader textReader)
            : base(textReader)
        {
        }

        public AlphanumericTokenizer(AttributeSource source, TextReader textReader)
            : base(source, textReader)
        {
        }

        public AlphanumericTokenizer(AttributeFactory factory, TextReader textReader)
            : base(factory, textReader)
        {
        }

        protected override bool IsTokenChar(char c)
        {
            return Char.IsLetterOrDigit(c);
        }
    }
}