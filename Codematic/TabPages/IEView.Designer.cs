namespace Codematic
{
    partial class IEView
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IEView));
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.fileBrowserToolStrip = new System.Windows.Forms.ToolStrip();
            this.backFileToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.forwardFileToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.fileToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.txtUrl = new System.Windows.Forms.ToolStripTextBox();
            this.btn_Go = new System.Windows.Forms.ToolStripButton();
            this.fileBrowserToolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(4, 28);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(589, 412);
            this.webBrowser1.TabIndex = 3;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            this.webBrowser1.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.webBrowser1_Navigated);
            

            
            // 
            // fileBrowserToolStrip
            // 
            this.fileBrowserToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backFileToolStripButton,
            this.forwardFileToolStripButton,
            this.fileToolStripSeparator,
            this.txtUrl,
            this.btn_Go});
            this.fileBrowserToolStrip.Location = new System.Drawing.Point(0, 0);
            this.fileBrowserToolStrip.Name = "fileBrowserToolStrip";
            this.fileBrowserToolStrip.Size = new System.Drawing.Size(597, 27);
            this.fileBrowserToolStrip.Stretch = true;
            this.fileBrowserToolStrip.TabIndex = 9;
            this.fileBrowserToolStrip.Text = "toolStrip2";
            // 
            // backFileToolStripButton
            // 
            this.backFileToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("backFileToolStripButton.Image")));
            this.backFileToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.backFileToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.backFileToolStripButton.Name = "backFileToolStripButton";
            this.backFileToolStripButton.Size = new System.Drawing.Size(23, 24);
            this.backFileToolStripButton.Click += new System.EventHandler(this.backFileToolStripButton_Click);
            // 
            // forwardFileToolStripButton
            // 
            this.forwardFileToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.forwardFileToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("forwardFileToolStripButton.Image")));
            this.forwardFileToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.forwardFileToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.forwardFileToolStripButton.Name = "forwardFileToolStripButton";
            this.forwardFileToolStripButton.Size = new System.Drawing.Size(23, 24);
            this.forwardFileToolStripButton.Text = "Forward";
            this.forwardFileToolStripButton.Click += new System.EventHandler(this.forwardFileToolStripButton_Click);
            // 
            // fileToolStripSeparator
            // 
            this.fileToolStripSeparator.Name = "fileToolStripSeparator";
            this.fileToolStripSeparator.Size = new System.Drawing.Size(6, 27);
            // 
            // txtUrl
            // 
            this.txtUrl.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.txtUrl.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.AllUrl;
            this.txtUrl.AutoSize = false;
            this.txtUrl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUrl.Margin = new System.Windows.Forms.Padding(1);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(400, 21);
            this.txtUrl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUrl_KeyDown);
            this.txtUrl.Click += new System.EventHandler(this.txtUrl_Click);
            // 
            // btn_Go
            // 
            this.btn_Go.Image = ((System.Drawing.Image)(resources.GetObject("btn_Go.Image")));
            this.btn_Go.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btn_Go.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Go.Name = "btn_Go";
            this.btn_Go.Size = new System.Drawing.Size(24, 24);
            this.btn_Go.Click += new System.EventHandler(this.btn_Go_Click);
            // 
            // IEView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 440);
            this.Controls.Add(this.fileBrowserToolStrip);
            this.Controls.Add(this.webBrowser1);
            this.Name = "IEView";
            this.Text = "IEView";
            this.fileBrowserToolStrip.ResumeLayout(false);
            this.fileBrowserToolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.ToolStrip fileBrowserToolStrip;
        private System.Windows.Forms.ToolStripButton backFileToolStripButton;
        private System.Windows.Forms.ToolStripButton forwardFileToolStripButton;
        private System.Windows.Forms.ToolStripSeparator fileToolStripSeparator;
        private System.Windows.Forms.ToolStripTextBox txtUrl;
        private System.Windows.Forms.ToolStripButton btn_Go;
    }
}