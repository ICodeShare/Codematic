using System.Drawing;
using LTP.TextEditor;
using LTP.TextEditor.Gui.CompletionWindow;
using LTP.TextEditor.Document;
using LTP.TextEditor.Actions;
namespace Codematic.UserControls
{
    partial class UcCodeView
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtContent_Web = new LTP.TextEditor.TextEditorControl();
            this.txtContent_CS = new LTP.TextEditor.TextEditorControl();
            this.txtContent_SQL = new LTP.TextEditor.TextEditorControl();
            this.txtContent_XML = new LTP.TextEditor.TextEditorControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menu_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();

            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_Save});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 70);


            // 
            // menu1ToolStripMenuItem
            // 
            this.menu_Save.Name = "menu_Save";
            this.menu_Save.Size = new System.Drawing.Size(152, 22);
            this.menu_Save.Text = "保存(&S)";
            this.menu_Save.Click += new System.EventHandler(this.menu_Save_Click);

            // 
            // txtContent_Web
            // 
            //this.txtContent_Web.AcceptsTab = true;
            this.txtContent_Web.Location = new System.Drawing.Point(321, 214);
            this.txtContent_Web.Name = "txtContent_Web";
            this.txtContent_Web.Size = new System.Drawing.Size(200, 200);
            this.txtContent_Web.TabIndex = 0;
            this.txtContent_Web.Text = "";

            this.txtContent_Web.IsIconBarVisible = false;
            this.txtContent_Web.ShowInvalidLines = false;
            this.txtContent_Web.ShowSpaces = false;
            this.txtContent_Web.ShowTabs = false;
            this.txtContent_Web.ShowEOLMarkers = false;
            this.txtContent_Web.ShowVRuler = false;
            this.txtContent_Web.Language = Languages.HTML;
            this.txtContent_Web.Encoding = System.Text.Encoding.Default;
            this.txtContent_Web.Font = new Font("新宋体", 9);
            this.txtContent_Web.ContextMenuStrip = this.contextMenuStrip1;

            // 
            // txtContent_CS
            // 
            //this.txtContent_CS.AcceptsTab = true;
            this.txtContent_CS.Location = new System.Drawing.Point(219, 8);
            this.txtContent_CS.Name = "txtContent_CS";
            this.txtContent_CS.Size = new System.Drawing.Size(200, 200);
            this.txtContent_CS.TabIndex = 0;
            this.txtContent_CS.Text = "";

            this.txtContent_CS.IsIconBarVisible = false;
            this.txtContent_CS.ShowInvalidLines = false;
            this.txtContent_CS.ShowSpaces = false;
            this.txtContent_CS.ShowTabs = false;
            this.txtContent_CS.ShowEOLMarkers = false;
            this.txtContent_CS.ShowVRuler = false;
            this.txtContent_CS.Language = Languages.CSHARP;
            this.txtContent_CS.Encoding = System.Text.Encoding.Default;
            this.txtContent_CS.Font = new Font("新宋体", 9);
            this.txtContent_CS.ContextMenuStrip = this.contextMenuStrip1;


            // 
            // txtContent_SQL
            // 
            //this.txtContent_SQL.AcceptsTab = true;
            this.txtContent_SQL.Location = new System.Drawing.Point(3, 3);
            this.txtContent_SQL.Name = "txtContent_SQL";
            this.txtContent_SQL.Size = new System.Drawing.Size(200, 200);
            this.txtContent_SQL.TabIndex = 0;
            this.txtContent_SQL.Text = "";


            this.txtContent_SQL.IsIconBarVisible = false;
            this.txtContent_SQL.ShowInvalidLines = false;
            this.txtContent_SQL.ShowSpaces = false;
            this.txtContent_SQL.ShowTabs = false;
            this.txtContent_SQL.ShowEOLMarkers = false;
            this.txtContent_SQL.ShowVRuler = false;
            this.txtContent_SQL.Language = Languages.SQL;
            this.txtContent_SQL.Encoding = System.Text.Encoding.Default;
            this.txtContent_SQL.Font = new Font("新宋体", 9);
            this.txtContent_SQL.ContextMenuStrip = this.contextMenuStrip1;

            // 
            // txtContent_XML
            // 
            //this.txtContent_SQL.AcceptsTab = true;
            this.txtContent_XML.Location = new System.Drawing.Point(3, 3);
            this.txtContent_XML.Name = "txtContent_XML";
            this.txtContent_XML.Size = new System.Drawing.Size(200, 200);
            this.txtContent_XML.TabIndex = 0;
            this.txtContent_XML.Text = "";


            this.txtContent_XML.IsIconBarVisible = false;
            this.txtContent_XML.ShowInvalidLines = false;
            this.txtContent_XML.ShowSpaces = false;
            this.txtContent_XML.ShowTabs = false;
            this.txtContent_XML.ShowEOLMarkers = false;
            this.txtContent_XML.ShowVRuler = false;
            this.txtContent_XML.Language = Languages.XML;
            this.txtContent_XML.Encoding = System.Text.Encoding.Default;
            this.txtContent_XML.Font = new Font("新宋体", 9);
            this.txtContent_XML.ContextMenuStrip = this.contextMenuStrip1;

            // 
            // UcCodeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtContent_Web);
            this.Controls.Add(this.txtContent_CS);
            this.Controls.Add(this.txtContent_SQL);
            this.Controls.Add(this.txtContent_XML);
            this.Name = "UcCodeView";
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Size = new System.Drawing.Size(346, 317);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public LTP.TextEditor.TextEditorControl txtContent_SQL;
        public LTP.TextEditor.TextEditorControl txtContent_Web;
        public LTP.TextEditor.TextEditorControl txtContent_CS;
        private LTP.TextEditor.TextEditorControl txtContent_XML;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menu_Save;
    }
}
