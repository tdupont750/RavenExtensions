using System;
using System.Collections.Generic;
using System.IO;
using Lucene.Net.Analysis;
using Version = Lucene.Net.Util.Version;

namespace Raven.Extensions
{
    public sealed class AlphanumericAnalyzer : Analyzer
    {
        public const Version DefaultVersion = Version.LUCENE_29;
        public static readonly ISet<string> DefaultStopWords;

        private static readonly string[] AdditionalStopWords = new [] { "s" };

        static AlphanumericAnalyzer()
        {
            var stopWords = new CharArraySet(StopAnalyzer.ENGLISH_STOP_WORDS_SET.Count + AdditionalStopWords.Length, false);
            stopWords.AddAll(StopAnalyzer.ENGLISH_STOP_WORDS_SET);

            foreach (var additionalStopWord in AdditionalStopWords)
                stopWords.Add(additionalStopWord);

            DefaultStopWords = CharArraySet.UnmodifiableSet(stopWords);
        }

        private readonly bool _enableStopPositionIncrements;
        private readonly ISet<string> _stopSet;

        public AlphanumericAnalyzer()
            : this(DefaultVersion, DefaultStopWords)
        {
        }

        public AlphanumericAnalyzer(Version matchVersion, ISet<string> stopWords)
        {
            _enableStopPositionIncrements = StopFilter.GetEnablePositionIncrementsVersionDefault(matchVersion);
            _stopSet = stopWords;
        }

        public override TokenStream TokenStream(String fieldName, TextReader reader)
        {
            TokenStream tokenStream = new AlphanumericTokenizer(reader);
            tokenStream = new LowerCaseFilter(tokenStream);
            tokenStream = new StopFilter(_enableStopPositionIncrements, tokenStream, _stopSet);
            return tokenStream;
        }

        public override TokenStream ReusableTokenStream(String fieldName, TextReader reader)
        {
            var streams = (SavedStreams)PreviousTokenStream;

            if (streams == null)
            {
                streams = new SavedStreams();
                PreviousTokenStream = streams;

                streams.TokenStream = new AlphanumericTokenizer(reader);
                streams.FilteredTokenStream = new LowerCaseFilter(streams.TokenStream);
                streams.FilteredTokenStream = new StopFilter(_enableStopPositionIncrements, streams.FilteredTokenStream, _stopSet);
            }
            else
            {
                streams.TokenStream.Reset(reader);
            }

            return streams.FilteredTokenStream;
        }

        private sealed class SavedStreams
        {
            public AlphanumericTokenizer TokenStream;
            public TokenStream FilteredTokenStream;
        }
    }
}
