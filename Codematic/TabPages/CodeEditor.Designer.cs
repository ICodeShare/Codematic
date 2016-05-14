using System.Drawing;
using LTP.TextEditor;
using LTP.TextEditor.Gui.CompletionWindow;
using LTP.TextEditor.Document;
using LTP.TextEditor.Actions;
using System;
namespace Codematic
{
    partial class CodeEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodeEditor));
            this.txtContent = new TextEditorControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menu_Save = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_SaveAs = new System.Windows.Forms.ToolStripMenuItem();
			this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtContent
            // 
            this.txtContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtContent.Location = new System.Drawing.Point(0, 0);
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(368, 376);
            this.txtContent.TabIndex = 0;
            this.txtContent.Text = "";


            this.txtContent.IsIconBarVisible = false;
            this.txtContent.ShowInvalidLines = false;
            this.txtContent.ShowSpaces = false;
            this.txtContent.ShowTabs = false;
            this.txtContent.ShowEOLMarkers = false;
            this.txtContent.ShowVRuler = false;
            this.txtContent.Language = Languages.CSHARP;
            this.txtContent.Encoding = System.Text.Encoding.Default;
            this.txtContent.Font = new Font("新宋体", 9);
            //
            //contextMenuStrip1
            //
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
			{
				this.menu_Save,
				this.menu_SaveAs
			});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 70);
            this.menu_Save.Name = "menu_Save";
            this.menu_Save.Size = new System.Drawing.Size(152, 22);
            this.menu_Save.Text = "保存(&S)";
            this.menu_Save.Click += new EventHandler(this.menu_Save_Click);
            this.menu_SaveAs.Name = "menu_SaveAs";
            this.menu_SaveAs.Size = new System.Drawing.Size(152, 22);
            this.menu_SaveAs.Text = "另存为";
            this.menu_SaveAs.Click += new EventHandler(this.menu_SaveAs_Click);
            // 
            // CodeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 376);
            this.Controls.Add(this.txtContent);
            this.Name = "CodeEditor";
            this.Text = "CodeEditor";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public LTP.TextEditor.TextEditorControl txtContent;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menu_Save;
        private System.Windows.Forms.ToolStripMenuItem menu_SaveAs;
    }
}