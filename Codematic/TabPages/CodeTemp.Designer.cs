using System.Drawing;
using LTP.TextEditor;
using LTP.TextEditor.Gui.CompletionWindow;
using LTP.TextEditor.Document;
using LTP.TextEditor.Actions;
namespace Codematic
{
    partial class CodeTemp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodeTemp));
            this.listView1 = new System.Windows.Forms.ListView();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.imglistView = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Run = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.list_KeyField = new System.Windows.Forms.ListBox();
            this.btn_SetKey = new System.Windows.Forms.Button();
            this.btn_SelClear = new System.Windows.Forms.Button();
            this.btn_SelI = new System.Windows.Forms.Button();
            this.btn_SelAll = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtTemplate = new System.Windows.Forms.RichTextBox();// LTP.TextEditor.TextEditorControl();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Dock = System.Windows.Forms.DockStyle.Top;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(593, 168);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 168);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(593, 3);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // imglistView
            // 
            this.imglistView.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglistView.ImageStream")));
            this.imglistView.TransparentColor = System.Drawing.Color.Transparent;
            this.imglistView.Images.SetKeyName(0, "fild2.gif");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 171);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(593, 289);
            this.panel1.TabIndex = 2;
            // 
            // btn_Run
            // 
            this.btn_Run.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Run.Location = new System.Drawing.Point(492, 459);
            this.btn_Run.Name = "btn_Run";
            this.btn_Run.Size = new System.Drawing.Size(75, 23);
            this.btn_Run.TabIndex = 8;
            this.btn_Run.Text = "生成";
            this.btn_Run.UseVisualStyleBackColor = true;
            this.btn_Run.Click += new System.EventHandler(this.btn_Run_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.list_KeyField);
            this.groupBox2.Controls.Add(this.btn_SetKey);
            this.groupBox2.Controls.Add(this.btn_SelClear);
            this.groupBox2.Controls.Add(this.btn_SelI);
            this.groupBox2.Controls.Add(this.btn_SelAll);
            this.groupBox2.Location = new System.Drawing.Point(3, 1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(583, 52);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            // 
            // list_KeyField
            // 
            this.list_KeyField.FormattingEnabled = true;
            this.list_KeyField.ItemHeight = 12;
            this.list_KeyField.Location = new System.Drawing.Point(435, 14);
            this.list_KeyField.Name = "list_KeyField";
            this.list_KeyField.Size = new System.Drawing.Size(129, 28);
            this.list_KeyField.TabIndex = 1;
            // 
            // btn_SetKey
            // 
            this.btn_SetKey.Location = new System.Drawing.Point(321, 17);
            this.btn_SetKey.Name = "btn_SetKey";
            this.btn_SetKey.Size = new System.Drawing.Size(108, 23);
            this.btn_SetKey.TabIndex = 0;
            this.btn_SetKey.Text = "主键(条件)字段";
            this.btn_SetKey.UseVisualStyleBackColor = true;
            this.btn_SetKey.Click += new System.EventHandler(this.btn_SetKey_Click);
            // 
            // btn_SelClear
            // 
            this.btn_SelClear.Location = new System.Drawing.Point(178, 17);
            this.btn_SelClear.Name = "btn_SelClear";
            this.btn_SelClear.Size = new System.Drawing.Size(75, 23);
            this.btn_SelClear.TabIndex = 0;
            this.btn_SelClear.Text = "清空";
            this.btn_SelClear.UseVisualStyleBackColor = true;
            this.btn_SelClear.Click += new System.EventHandler(this.btn_SelClear_Click);
            // 
            // btn_SelI
            // 
            this.btn_SelI.Location = new System.Drawing.Point(97, 17);
            this.btn_SelI.Name = "btn_SelI";
            this.btn_SelI.Size = new System.Drawing.Size(75, 23);
            this.btn_SelI.TabIndex = 0;
            this.btn_SelI.Text = "反选";
            this.btn_SelI.UseVisualStyleBackColor = true;
            this.btn_SelI.Click += new System.EventHandler(this.btn_SelI_Click);
            // 
            // btn_SelAll
            // 
            this.btn_SelAll.Location = new System.Drawing.Point(16, 17);
            this.btn_SelAll.Name = "btn_SelAll";
            this.btn_SelAll.Size = new System.Drawing.Size(75, 23);
            this.btn_SelAll.TabIndex = 0;
            this.btn_SelAll.Text = "全选";
            this.btn_SelAll.UseVisualStyleBackColor = true;
            this.btn_SelAll.Click += new System.EventHandler(this.btn_SelAll_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtTemplate);
            this.groupBox1.Location = new System.Drawing.Point(3, 56);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(583, 233);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "模版";
            // 
            // txtTemplate
            // 
            this.txtTemplate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtTemplate.Location = new System.Drawing.Point(3, 17);
            this.txtTemplate.Name = "txtTemplate";
            this.txtTemplate.Size = new System.Drawing.Size(577, 213);
            this.txtTemplate.TabIndex = 8;
            this.txtTemplate.Text = "";

            //this.txtTemplate.IsIconBarVisible = false;
            //this.txtTemplate.ShowInvalidLines = false;
            //this.txtTemplate.ShowSpaces = false;
            //this.txtTemplate.ShowTabs = false;
            //this.txtTemplate.ShowEOLMarkers = false;
            //this.txtTemplate.ShowVRuler = false;
            //this.txtTemplate.Language = TextEditorControlBase.Languages.XML;
            //this.txtTemplate.Encoding = System.Text.Encoding.Default;
            //this.txtTemplate.Font = new Font("新宋体", 9);

            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 460);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(593, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusLabel1
            // 
            this.StatusLabel1.Name = "StatusLabel1";
            this.StatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // CodeTemp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 482);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_Run);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.listView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CodeTemp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "代码生成";
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ImageList imglistView;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_Run;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox list_KeyField;
        private System.Windows.Forms.Button btn_SetKey;
        private System.Windows.Forms.Button btn_SelClear;
        private System.Windows.Forms.Button btn_SelI;
        private System.Windows.Forms.Button btn_SelAll;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox txtTemplate;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel1;


    }
}