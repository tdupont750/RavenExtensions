namespace Raven.Extensions.AnalyzerViewer
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tbSourceText = new System.Windows.Forms.TextBox();
            this.tbOutputText = new System.Windows.Forms.TextBox();
            this.cbAnalysers = new System.Windows.Forms.ComboBox();
            this.lblAnalyzer = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.lblStats = new System.Windows.Forms.Label();
            this.lblView = new System.Windows.Forms.Label();
            this.cbViews = new System.Windows.Forms.ComboBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 131);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tbSourceText);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tbOutputText);
            this.splitContainer1.Size = new System.Drawing.Size(513, 221);
            this.splitContainer1.SplitterDistance = 109;
            this.splitContainer1.TabIndex = 1;
            // 
            // tbSourceText
            // 
            this.tbSourceText.AcceptsReturn = true;
            this.tbSourceText.AcceptsTab = true;
            this.tbSourceText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbSourceText.Location = new System.Drawing.Point(0, 0);
            this.tbSourceText.Multiline = true;
            this.tbSourceText.Name = "tbSourceText";
            this.tbSourceText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbSourceText.Size = new System.Drawing.Size(513, 109);
            this.tbSourceText.TabIndex = 0;
            // 
            // tbOutputText
            // 
            this.tbOutputText.AcceptsReturn = true;
            this.tbOutputText.AcceptsTab = true;
            this.tbOutputText.BackColor = System.Drawing.SystemColors.Window;
            this.tbOutputText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbOutputText.Location = new System.Drawing.Point(0, 0);
            this.tbOutputText.Multiline = true;
            this.tbOutputText.Name = "tbOutputText";
            this.tbOutputText.ReadOnly = true;
            this.tbOutputText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbOutputText.Size = new System.Drawing.Size(513, 108);
            this.tbOutputText.TabIndex = 0;
            // 
            // cbAnalysers
            // 
            this.cbAnalysers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAnalysers.FormattingEnabled = true;
            this.cbAnalysers.Location = new System.Drawing.Point(72, 9);
            this.cbAnalysers.Name = "cbAnalysers";
            this.cbAnalysers.Size = new System.Drawing.Size(198, 21);
            this.cbAnalysers.TabIndex = 2;
            // 
            // lblAnalyzer
            // 
            this.lblAnalyzer.AutoSize = true;
            this.lblAnalyzer.Location = new System.Drawing.Point(13, 12);
            this.lblAnalyzer.Name = "lblAnalyzer";
            this.lblAnalyzer.Size = new System.Drawing.Size(53, 13);
            this.lblAnalyzer.TabIndex = 4;
            this.lblAnalyzer.Text = "Analyzer: ";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(6, 41);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(60, 13);
            this.lblDescription.TabIndex = 5;
            this.lblDescription.Text = "Description";
            // 
            // tbDescription
            // 
            this.tbDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDescription.BackColor = System.Drawing.SystemColors.Window;
            this.tbDescription.Location = new System.Drawing.Point(73, 41);
            this.tbDescription.Multiline = true;
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.ReadOnly = true;
            this.tbDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbDescription.Size = new System.Drawing.Size(452, 59);
            this.tbDescription.TabIndex = 6;
            // 
            // lblStats
            // 
            this.lblStats.AutoSize = true;
            this.lblStats.Location = new System.Drawing.Point(12, 115);
            this.lblStats.Name = "lblStats";
            this.lblStats.Size = new System.Drawing.Size(31, 13);
            this.lblStats.TabIndex = 7;
            this.lblStats.Text = "Stats";
            // 
            // lblView
            // 
            this.lblView.AutoSize = true;
            this.lblView.Location = new System.Drawing.Point(277, 13);
            this.lblView.Name = "lblView";
            this.lblView.Size = new System.Drawing.Size(36, 13);
            this.lblView.TabIndex = 8;
            this.lblView.Text = "View: ";
            // 
            // cbViews
            // 
            this.cbViews.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbViews.FormattingEnabled = true;
            this.cbViews.Location = new System.Drawing.Point(319, 9);
            this.cbViews.Name = "cbViews";
            this.cbViews.Size = new System.Drawing.Size(205, 21);
            this.cbViews.TabIndex = 9;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 364);
            this.Controls.Add(this.cbViews);
            this.Controls.Add(this.lblView);
            this.Controls.Add(this.lblStats);
            this.Controls.Add(this.tbDescription);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblAnalyzer);
            this.Controls.Add(this.cbAnalysers);
            this.Controls.Add(this.splitContainer1);
            this.Name = "MainForm";
            this.Text = "Lucene.Net - Analyzer Viewer";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox tbSourceText;
        private System.Windows.Forms.TextBox tbOutputText;
        private System.Windows.Forms.ComboBox cbAnalysers;
        private System.Windows.Forms.Label lblAnalyzer;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox tbDescription;
        private System.Windows.Forms.Label lblStats;
        private System.Windows.Forms.Label lblView;
        private System.Windows.Forms.ComboBox cbViews;
    }
}

