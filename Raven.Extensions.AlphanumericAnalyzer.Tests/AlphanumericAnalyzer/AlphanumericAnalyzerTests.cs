using System.Linq;
using Lucene.Net.Analysis.Standard;
using Xunit;
using Xunit.Extensions;

namespace Raven.Extensions.Tests.AlphanumericAnalyzer
{
    public class AlphanumericAnalyzerTests
    {
        [Theory]
        [InlineData("my_file_name")]
        [InlineData("my-file-name")]
        [InlineData("my file name")]
        public void EquivalentOutput(string input)
        {
            var customAnalyzer = new Extensions.AlphanumericAnalyzer();
            var customOutput = customAnalyzer.GetTokens(input);

            var standardAnalyzer = new StandardAnalyzer(Extensions.AlphanumericAnalyzer.DefaultVersion);
            var standardOutput = standardAnalyzer.GetTokens(input);

            Assert.Equal(standardOutput, customOutput);
        }

        [Theory]
        [InlineData("my_file_name01.txt")]
        [InlineData("my_file_name.txt")]
        [InlineData("tdupont750@gmail.com")]
        [InlineData("don't")]
        public void DifferentOutput(string input)
        {
            var customAnalyzer = new Extensions.AlphanumericAnalyzer();
            var customOutput = customAnalyzer.GetTokens(input);

            var standardAnalyzer = new StandardAnalyzer(Extensions.AlphanumericAnalyzer.DefaultVersion);
            var standardOutput = standardAnalyzer.GetTokens(input);

            Assert.True(customOutput.Count > standardOutput.Count);
        }

        [Theory]
        [InlineData("Hello, world!",            new[] { "hello", "world" })]
        [InlineData("my_file_name01.txt",       new[] { "my", "file", "name01", "txt" })]
        [InlineData("my_file_name_01.txt",      new[] { "my", "file", "name", "01", "txt" })]
        [InlineData("tdupont@blizzard.com",     new[] { "tdupont", "blizzard", "com" })]
        [InlineData("don't",                    new[] { "don", "t" })]
        public void SingleGetTokens(string input, string[] expected)
        {
            var analyzer = new Extensions.AlphanumericAnalyzer();
            var actual = analyzer.GetTokens(input).ToArray();
            Assert.Equal(expected, actual);
        }
    }
}