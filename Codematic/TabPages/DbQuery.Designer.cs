using System.Drawing;
using LTP.TextEditor;
using LTP.TextEditor.Gui.CompletionWindow;
using LTP.TextEditor.Document;
using LTP.TextEditor.Actions;
namespace Codematic
{
    partial class DbQuery
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
            this.components = new System.ComponentModel.Container();
            //ICSharpCode.TextEditor.Document.DefaultDocument defaultDocument1 = new ICSharpCode.TextEditor.Document.DefaultDocument();
            ICSharpCode.TextEditor.Document.DefaultFormattingStrategy defaultFormattingStrategy1 = new ICSharpCode.TextEditor.Document.DefaultFormattingStrategy();
            ICSharpCode.TextEditor.Document.DefaultHighlightingStrategy defaultHighlightingStrategy1 = new ICSharpCode.TextEditor.Document.DefaultHighlightingStrategy();
            ICSharpCode.TextEditor.Document.GapTextBufferStrategy gapTextBufferStrategy1 = new ICSharpCode.TextEditor.Document.GapTextBufferStrategy();
            ICSharpCode.TextEditor.Document.DefaultTextEditorProperties defaultTextEditorProperties1 = new ICSharpCode.TextEditor.Document.DefaultTextEditorProperties();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DbQuery));
            this.txtContent = new LTPTextEditor.Editor.TextEditorControlWrapper();
            this.cmShortcutMeny = new System.Windows.Forms.ContextMenu();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtInfo = new System.Windows.Forms.RichTextBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.ttParamenterInfo = new System.Windows.Forms.ToolTip(this.components);
            this.ExecutionTimer = new System.Windows.Forms.Timer(this.components);
            this.printDocument = new System.Drawing.Printing.PrintDocument();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pageSetupDialog = new System.Windows.Forms.PageSetupDialog();
            this.cmDragAndDrp = new System.Windows.Forms.ContextMenu();
            this.menuItemObjectName = new System.Windows.Forms.MenuItem();
            this.menuItemSplitter = new System.Windows.Forms.MenuItem();
            this.menuItemSelect1 = new System.Windows.Forms.MenuItem();
            this.menuItemSelect2 = new System.Windows.Forms.MenuItem();
            this.menuItemJoin = new System.Windows.Forms.MenuItem();
            this.menuItemLeftOuterJoin = new System.Windows.Forms.MenuItem();
            this.menuItemRightOuterJoin = new System.Windows.Forms.MenuItem();
            this.menuItemWhere = new System.Windows.Forms.MenuItem();
            this.menuItemOrderBy = new System.Windows.Forms.MenuItem();
            this.menuItemGroupBy = new System.Windows.Forms.MenuItem();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.contextMenu1 = new System.Windows.Forms.ContextMenu();
            this.printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtContent
            // 
            this.txtContent.AllowDrop = true;
            this.txtContent.AutoScroll = true;
            this.txtContent.Dock = System.Windows.Forms.DockStyle.Fill;
            //defaultDocument1.FormattingStrategy = defaultFormattingStrategy1;
            defaultHighlightingStrategy1.Extensions = new string[] {
        ".SQL"};
            //defaultDocument1.HighlightingStrategy = defaultHighlightingStrategy1;
            //defaultDocument1.ReadOnly = false;
            //defaultDocument1.TextBufferStrategy = gapTextBufferStrategy1;
            //defaultDocument1.TextContent = "";
            defaultTextEditorProperties1.AllowCaretBeyondEOL = false;
            defaultTextEditorProperties1.AutoInsertCurlyBracket = true;
            defaultTextEditorProperties1.BracketMatchingStyle = ICSharpCode.TextEditor.Document.BracketMatchingStyle.After;
            defaultTextEditorProperties1.ConvertTabsToSpaces = false;
            defaultTextEditorProperties1.CreateBackupCopy = false;
            defaultTextEditorProperties1.DocumentSelectionMode = ICSharpCode.TextEditor.Document.DocumentSelectionMode.Normal;
            defaultTextEditorProperties1.EnableFolding = true;
            defaultTextEditorProperties1.Encoding = ((System.Text.Encoding)(resources.GetObject("defaultTextEditorProperties1.Encoding")));
            defaultTextEditorProperties1.Font = new System.Drawing.Font("Courier New", 10F);
            defaultTextEditorProperties1.HideMouseCursor = false;
            defaultTextEditorProperties1.IndentStyle = ICSharpCode.TextEditor.Document.IndentStyle.Smart;
            defaultTextEditorProperties1.IsIconBarVisible = false;
            defaultTextEditorProperties1.LineTerminator = "\r\n";
            defaultTextEditorProperties1.LineViewerStyle = ICSharpCode.TextEditor.Document.LineViewerStyle.None;
            defaultTextEditorProperties1.MouseWheelScrollDown = true;
            defaultTextEditorProperties1.MouseWheelTextZoom = true;
            defaultTextEditorProperties1.ShowEOLMarker = false;
            defaultTextEditorProperties1.ShowHorizontalRuler = false;
            defaultTextEditorProperties1.ShowInvalidLines = false;
            defaultTextEditorProperties1.ShowLineNumbers = true;
            defaultTextEditorProperties1.ShowMatchingBracket = true;
            defaultTextEditorProperties1.ShowSpaces = false;
            defaultTextEditorProperties1.ShowTabs = false;
            defaultTextEditorProperties1.ShowVerticalRuler = false;
            defaultTextEditorProperties1.TabIndent = 4;
            defaultTextEditorProperties1.UseAntiAliasedFont = false;
            defaultTextEditorProperties1.UseCustomLine = false;
            defaultTextEditorProperties1.VerticalRulerRow = 80;
            //defaultDocument1.TextEditorProperties = defaultTextEditorProperties1;
            //this.txtContent.Document = defaultDocument1;
            this.txtContent.Encoding = ((System.Text.Encoding)(resources.GetObject("txtContent.Encoding")));
            this.txtContent.IsIconBarVisible = false;
            this.txtContent.Location = new System.Drawing.Point(0, 0);
            this.txtContent.Name = "txtContent";
            this.txtContent.SelectedText = "";
            this.txtContent.SelectionStart = 0;
            this.txtContent.ShowInvalidLines = false;
            this.txtContent.ShowSpaces = false;
            this.txtContent.ShowTabs = false;
            this.txtContent.ShowVRuler = false;
            this.txtContent.Size = new System.Drawing.Size(377, 257);
            this.txtContent.TabIndex = 2;
            this.txtContent.TextEditorProperties = defaultTextEditorProperties1;
            this.txtContent.KeyPressEvent += new LTPTextEditor.Editor.TextEditorControlWrapper.KeyPressEventHandler(this.qcTextEditor_KeyPressEvent);
            this.txtContent.RMouseUpEvent += new LTPTextEditor.Editor.TextEditorControlWrapper.MYMouseRButtonUpEventHandler(this.qcTextEditor_MouseUp);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 404);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.ShowItemToolTips = true;
            this.statusStrip1.Size = new System.Drawing.Size(377, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(362, 17);
            this.toolStripStatusLabel2.Spring = true;
            this.toolStripStatusLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.Location = new System.Drawing.Point(0, 260);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.ShowToolTips = true;
            this.tabControl1.Size = new System.Drawing.Size(377, 144);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.ImageIndex = 0;
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(369, 117);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "结果";
            this.tabPage1.ToolTipText = "查询结果";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(369, 117);
            this.dataGridView1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtInfo);
            this.tabPage2.ImageIndex = 1;
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(369, 117);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "消息";
            this.tabPage2.ToolTipText = "语句执行情况";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtInfo
            // 
            this.txtInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInfo.Location = new System.Drawing.Point(0, 0);
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.Size = new System.Drawing.Size(369, 117);
            this.txtInfo.TabIndex = 0;
            this.txtInfo.Text = "";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "exec.ico");
            this.imageList1.Images.SetKeyName(1, "chat.ico");
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter1.Location = new System.Drawing.Point(0, 257);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(377, 3);
            this.splitter1.TabIndex = 5;
            this.splitter1.TabStop = false;
            // 
            // ttParamenterInfo
            // 
            this.ttParamenterInfo.Active = false;
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "");
            this.imageList2.Images.SetKeyName(1, "");
            this.imageList2.Images.SetKeyName(2, "");
            this.imageList2.Images.SetKeyName(3, "");
            this.imageList2.Images.SetKeyName(4, "");
            this.imageList2.Images.SetKeyName(5, "");
            this.imageList2.Images.SetKeyName(6, "");
            // 
            // pageSetupDialog
            // 
            this.pageSetupDialog.Document = this.printDocument;
            // 
            // cmDragAndDrp
            // 
            this.cmDragAndDrp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemObjectName,
            this.menuItemSplitter,
            this.menuItemSelect1,
            this.menuItemSelect2,
            this.menuItemJoin,
            this.menuItemLeftOuterJoin,
            this.menuItemRightOuterJoin,
            this.menuItemWhere,
            this.menuItemOrderBy,
            this.menuItemGroupBy});
            // 
            // menuItemObjectName
            // 
            this.menuItemObjectName.Index = 0;
            this.menuItemObjectName.Text = "Object name";
            this.menuItemObjectName.Click += new System.EventHandler(this.menuItemObjectName_Click);
            // 
            // menuItemSplitter
            // 
            this.menuItemSplitter.Index = 1;
            this.menuItemSplitter.Text = "-";
            // 
            // menuItemSelect1
            // 
            this.menuItemSelect1.Index = 2;
            this.menuItemSelect1.Text = "SELECT * FROM...";
            this.menuItemSelect1.Click += new System.EventHandler(this.menuItemSelect1_Click);
            // 
            // menuItemSelect2
            // 
            this.menuItemSelect2.Index = 3;
            this.menuItemSelect2.Text = "SELECT [Fields] FROM";
            this.menuItemSelect2.Click += new System.EventHandler(this.menuItemSelect2_Click);
            // 
            // menuItemJoin
            // 
            this.menuItemJoin.Index = 4;
            this.menuItemJoin.Text = "UPDATE...";
            this.menuItemJoin.Click += new System.EventHandler(this.menuItemJoin_Click);
            // 
            // menuItemLeftOuterJoin
            // 
            this.menuItemLeftOuterJoin.Index = 5;
            this.menuItemLeftOuterJoin.Text = "DELETE FROM...";
            this.menuItemLeftOuterJoin.Click += new System.EventHandler(this.menuItemLeftOuterJoin_Click);
            // 
            // menuItemRightOuterJoin
            // 
            this.menuItemRightOuterJoin.Index = 6;
            this.menuItemRightOuterJoin.Text = "INSERT INTO ...";
            this.menuItemRightOuterJoin.Click += new System.EventHandler(this.menuItemRightOuterJoin_Click);
            // 
            // menuItemWhere
            // 
            this.menuItemWhere.Index = 7;
            this.menuItemWhere.Text = "WHERE";
            this.menuItemWhere.Click += new System.EventHandler(this.menuItemWhere_Click);
            // 
            // menuItemOrderBy
            // 
            this.menuItemOrderBy.Index = 8;
            this.menuItemOrderBy.Text = "ORDER BY";
            this.menuItemOrderBy.Click += new System.EventHandler(this.menuItemOrderBy_Click);
            // 
            // menuItemGroupBy
            // 
            this.menuItemGroupBy.Index = 9;
            this.menuItemGroupBy.Text = "GROUP BY";
            this.menuItemGroupBy.Click += new System.EventHandler(this.menuItemGroupBy_Click);
            // 
            // printDialog
            // 
            this.printDialog.Document = this.printDocument;
            // 
            // printPreviewDialog
            // 
            this.printPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog.Document = this.printDocument;
            this.printPreviewDialog.Enabled = true;
            this.printPreviewDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog.Icon")));
            this.printPreviewDialog.Name = "printPreviewDialog";
            this.printPreviewDialog.Visible = false;
            // 
            // DbQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 426);
            this.Controls.Add(this.txtContent);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "DbQuery";
            this.Text = "DbQuery";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DbQuery_FormClosing);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public LTPTextEditor.Editor.TextEditorControlWrapper txtContent;
        private System.Windows.Forms.ContextMenu cmShortcutMeny;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        public  System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.RichTextBox txtInfo;
        private System.Windows.Forms.ToolTip ttParamenterInfo;
        private System.Windows.Forms.Timer ExecutionTimer;
        private System.Drawing.Printing.PrintDocument printDocument;
        private System.Windows.Forms.ImageList imageList2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog;
        private System.Windows.Forms.ContextMenu cmDragAndDrp;
        private System.Windows.Forms.MenuItem menuItemObjectName;
        private System.Windows.Forms.MenuItem menuItemSplitter;
        private System.Windows.Forms.MenuItem menuItemSelect1;
        private System.Windows.Forms.MenuItem menuItemSelect2;
        private System.Windows.Forms.MenuItem menuItemJoin;
        private System.Windows.Forms.MenuItem menuItemLeftOuterJoin;
        private System.Windows.Forms.MenuItem menuItemRightOuterJoin;
        private System.Windows.Forms.MenuItem menuItemWhere;
        private System.Windows.Forms.MenuItem menuItemOrderBy;
        private System.Windows.Forms.MenuItem menuItemGroupBy;
        private System.Windows.Forms.PrintDialog printDialog;
        private System.Windows.Forms.ContextMenu contextMenu1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog;
    }
}