using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;

namespace Raven.Extensions.AnalyzerViewer
{
    public partial class MainForm : Form
    {
        public BindingList<AnalyzerInfo> AnalyzerList = new BindingList<AnalyzerInfo>();
        public BindingList<AnalyzerView> AnalyzerViews = new BindingList<AnalyzerView>();

        public MainForm()
        {
            InitializeComponent();

            AnalyzerList.Add(new AnalyzerInfo("Keyword Analyzer", "\"Tokenizes\" the entire stream as a single token.",  new Lucene.Net.Analysis.KeywordAnalyzer()));
            AnalyzerList.Add(new AnalyzerInfo("Whitespace Analyzer", "An Analyzer that uses WhitespaceTokenizer.",  new WhitespaceAnalyzer()));
            AnalyzerList.Add(new AnalyzerInfo("Stop Analyzer", "Filters LetterTokenizer with LowerCaseFilter and StopFilter.", new StopAnalyzer(Lucene.Net.Util.Version.LUCENE_29)));
            AnalyzerList.Add(new AnalyzerInfo("Simple Analyzer", "An Analyzer that filters LetterTokenizer with LowerCaseFilter.",  new Lucene.Net.Analysis.SimpleAnalyzer()));
            AnalyzerList.Add(new AnalyzerInfo("Standard Analyzer", "Filters StandardTokenizer with StandardFilter, LowerCaseFilter and StopFilter, using a list of English stop words.", new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_29)));
            AnalyzerList.Add(new AnalyzerInfo("Alphanumeric Analyzer", "Meh", new AlphanumericAnalyzer()));

            AnalyzerViews.Add(new TermAnalyzerView());
            AnalyzerViews.Add(new TermFrequenciesView());
            
            tbDescription.DataBindings.Add(new Binding("Text", AnalyzerList, "Description"));

            cbAnalysers.DisplayMember = "Name";
            cbAnalysers.ValueMember = "LuceneAnalyzer";
            cbAnalysers.DataSource = AnalyzerList;

            cbViews.DisplayMember = "Name";
            cbViews.DataSource = AnalyzerViews;

            cbAnalysers.SelectedIndex = 0;
            cbViews.SelectedIndex = 0;

            cbAnalysers.SelectedValueChanged += new EventHandler(cbAnalysers_SelectedValueChanged);
            cbViews.SelectedValueChanged += new EventHandler(cbViews_SelectedValueChanged);
            tbSourceText.TextChanged += new EventHandler(tbSourceText_TextChanged);

            tbSourceText.Text = "The quick brown fox jumped over the lazy dog.";
            AnalyzeText();
        }

        void cbViews_SelectedValueChanged(object sender, EventArgs e)
        {
            AnalyzeText();            
        }

        void tbSourceText_TextChanged(object sender, EventArgs e)
        {
            AnalyzeText();
        }

        void cbAnalysers_SelectedValueChanged(object sender, EventArgs e)
        {
            AnalyzeText();
        }

        public void AnalyzeText()
        {
            var analyzer = cbAnalysers.SelectedValue as Analyzer;

            var termCounter = 0;

            if (analyzer != null)
            {
                var view = (AnalyzerView) cbViews.SelectedValue;

                var stringReader = new StringReader(tbSourceText.Text);

                var tokenStream = analyzer.TokenStream("defaultFieldName", stringReader);

                tbOutputText.Text =  view.GetView(tokenStream, out termCounter).Trim();
            }

            lblStats.Text = string.Format("Total of {0} Term(s) Found.", termCounter);
        }
    }
}
