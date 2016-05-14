using System;
using System.Drawing;
using System.Windows.Forms;
namespace Codematic
{
    partial class SearchTables
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
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Text = "SearchTables";
            this.groupBox1 = new GroupBox();
            this.chkClearList = new System.Windows.Forms.CheckBox();
            this.cmbDB = new ComboBox();
            this.lblServer = new Label();
            this.label1 = new Label();
            this.label3 = new Label();
            this.btnSearch = new Button();
            this.txtKeyWord = new TextBox();
            this.cmboxType = new ComboBox();
            this.groupBox2 = new GroupBox();
            this.listView1 = new ListView();
            this.contextMenuStrip1 = new ContextMenuStrip(this.components);
            this.复制ToolStripMenuItem = new ToolStripMenuItem();
            this.所选表名ToolStripMenuItem = new ToolStripMenuItem();
            this.全部表名ToolStripMenuItem1 = new ToolStripMenuItem();
            this.所选字段ToolStripMenuItem = new ToolStripMenuItem();
            this.全部字段ToolStripMenuItem = new ToolStripMenuItem();
            this.移除ToolStripMenuItem = new ToolStripMenuItem();
            this.btnExpToWord = new Button();
            this.btnClose = new Button();
            this.lblTip = new Label();
            this.progressBar1 = new ProgressBar();
            this.toolStripMenuItem1 = new ToolStripSeparator();
            this.代码生成器ToolStripMenuItem = new ToolStripMenuItem();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            this.groupBox1.Controls.Add(this.chkClearList);
            this.groupBox1.Controls.Add(this.cmbDB);
            this.groupBox1.Controls.Add(this.lblServer);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.txtKeyWord);
            this.groupBox1.Controls.Add(this.cmboxType);
            this.groupBox1.Location = new System.Drawing.Point(17, 16);
            this.groupBox1.Margin = new Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new Padding(4, 4, 4, 4);
            this.groupBox1.Size = new Size(660, 125);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询选项";
            this.chkClearList.AutoSize = true;
            this.chkClearList.Checked = true;
            this.chkClearList.CheckState = CheckState.Checked;
            this.chkClearList.Location = new System.Drawing.Point(221, 98);
            this.chkClearList.Margin = new Padding(4, 4, 4, 4);
            this.chkClearList.Name = "chkClearList";
            this.chkClearList.Size = new Size(199, 24);
            this.chkClearList.TabIndex = 7;
            this.chkClearList.Text = "清空查询结果列表";
            this.chkClearList.UseVisualStyleBackColor = true;
            this.cmbDB.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbDB.Location = new System.Drawing.Point(415, 28);
            this.cmbDB.Margin = new Padding(4, 4, 4, 4);
            this.cmbDB.Name = "cmbDB";
            this.cmbDB.Size = new Size(201, 23);
            this.cmbDB.TabIndex = 6;
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(159, 30);
            this.lblServer.Margin = new Padding(4, 0, 4, 0);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new Size(0, 15);
            this.lblServer.TabIndex = 5;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 30);
            this.label1.Margin = new Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new Size(97, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "当前服务器：";
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(308, 30);
            this.label3.Margin = new Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new Size(97, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "选择数据库：";
            this.btnSearch.Location = new System.Drawing.Point(552, 60);
            this.btnSearch.Margin = new Padding(4, 4, 4, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new Size(100, 58);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new EventHandler(this.btnSearch_Click);
            this.txtKeyWord.Location = new System.Drawing.Point(209, 61);
            this.txtKeyWord.Margin = new Padding(4, 4, 4, 4);
            this.txtKeyWord.Name = "txtKeyWord";
            this.txtKeyWord.Size = new Size(335, 25);
            this.txtKeyWord.TabIndex = 1;
            this.cmboxType.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmboxType.FormattingEnabled = true;
            this.cmboxType.Items.AddRange(new object[]
			{
				"字段名称包含",
				"字段类型是",
				"表名称包含"
			});
            this.cmboxType.Location = new System.Drawing.Point(8, 61);
            this.cmboxType.Margin = new Padding(4, 4, 4, 4);
            this.cmboxType.Name = "cmboxType";
            this.cmboxType.Size = new Size(191, 23);
            this.cmboxType.TabIndex = 0;
            this.groupBox2.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.groupBox2.Controls.Add(this.listView1);
            this.groupBox2.Location = new System.Drawing.Point(16, 149);
            this.groupBox2.Margin = new Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new Padding(4, 4, 4, 4);
            this.groupBox2.Size = new Size(660, 261);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "查询结果";
            this.listView1.ContextMenuStrip = this.contextMenuStrip1;
            this.listView1.Dock = DockStyle.Fill;
            this.listView1.Location = new System.Drawing.Point(4, 21);
            this.listView1.Margin = new Padding(4, 4, 4, 4);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(651, 235);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.复制ToolStripMenuItem,
				this.移除ToolStripMenuItem,
				this.toolStripMenuItem1,
				this.代码生成器ToolStripMenuItem
			});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new Size(154, 104);
            this.复制ToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.所选表名ToolStripMenuItem,
				this.全部表名ToolStripMenuItem1,
				this.所选字段ToolStripMenuItem,
				this.全部字段ToolStripMenuItem
			});
            this.复制ToolStripMenuItem.Name = "复制ToolStripMenuItem";
            this.复制ToolStripMenuItem.Size = new Size(152, 22);
            this.复制ToolStripMenuItem.Text = "复制";
            this.所选表名ToolStripMenuItem.Name = "所选表名ToolStripMenuItem";
            this.所选表名ToolStripMenuItem.Size = new Size(138, 24);
            this.所选表名ToolStripMenuItem.Text = "所选表名";
            this.所选表名ToolStripMenuItem.Click += new EventHandler(this.所选表名ToolStripMenuItem_Click);
            this.全部表名ToolStripMenuItem1.Name = "全部表名ToolStripMenuItem1";
            this.全部表名ToolStripMenuItem1.Size = new Size(138, 24);
            this.全部表名ToolStripMenuItem1.Text = "全部表名";
            this.全部表名ToolStripMenuItem1.Click += new EventHandler(this.全部表名ToolStripMenuItem_Click);
            this.所选字段ToolStripMenuItem.Name = "所选字段ToolStripMenuItem";
            this.所选字段ToolStripMenuItem.Size = new Size(138, 24);
            this.所选字段ToolStripMenuItem.Text = "所选字段";
            this.所选字段ToolStripMenuItem.Click += new EventHandler(this.所选字段ToolStripMenuItem_Click);
            this.全部字段ToolStripMenuItem.Name = "全部字段ToolStripMenuItem";
            this.全部字段ToolStripMenuItem.Size = new Size(138, 24);
            this.全部字段ToolStripMenuItem.Text = "全部字段";
            this.全部字段ToolStripMenuItem.Click += new EventHandler(this.全部字段ToolStripMenuItem_Click);
            this.移除ToolStripMenuItem.Name = "移除ToolStripMenuItem";
            this.移除ToolStripMenuItem.Size = new Size(152, 22);
            this.移除ToolStripMenuItem.Text = "移除";
            this.移除ToolStripMenuItem.Click += new EventHandler(this.移除ToolStripMenuItem_Click);
            this.btnExpToWord.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            this.btnExpToWord.Location = new System.Drawing.Point(463, 425);
            this.btnExpToWord.Margin = new Padding(4, 4, 4, 4);
            this.btnExpToWord.Name = "btnExpToWord";
            this.btnExpToWord.Size = new Size(100, 29);
            this.btnExpToWord.TabIndex = 1;
            this.btnExpToWord.Text = "导出文档";
            this.btnExpToWord.UseVisualStyleBackColor = true;
            this.btnExpToWord.Click += new EventHandler(this.btnExpToWord_Click);
            this.btnClose.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
            this.btnClose.Location = new System.Drawing.Point(577, 425);
            this.btnClose.Margin = new Padding(4, 4, 4, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new Size(100, 29);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new EventHandler(this.btnClose_Click);
            this.lblTip.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
            this.lblTip.AutoSize = true;
            this.lblTip.Location = new System.Drawing.Point(25, 436);
            this.lblTip.Margin = new Padding(4, 0, 4, 0);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new Size(0, 15);
            this.lblTip.TabIndex = 2;
            this.progressBar1.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.progressBar1.Location = new System.Drawing.Point(4, 456);
            this.progressBar1.Margin = new Padding(4, 4, 4, 4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new Size(684, 25);
            this.progressBar1.TabIndex = 3;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new Size(150, 6);
            this.代码生成器ToolStripMenuItem.Name = "代码生成器ToolStripMenuItem";
            this.代码生成器ToolStripMenuItem.Size = new Size(153, 24);
            this.代码生成器ToolStripMenuItem.Text = "代码生成器";
            this.代码生成器ToolStripMenuItem.Click += new EventHandler(this.代码生成器ToolStripMenuItem_Click);
            base.AutoScaleDimensions = new SizeF(8f, 15f);
            base.ClientSize = new Size(693, 481);
            base.Controls.Add(this.progressBar1);
            base.Controls.Add(this.lblTip);
            base.Controls.Add(this.btnClose);
            base.Controls.Add(this.btnExpToWord);
            base.Controls.Add(this.groupBox2);
            base.Controls.Add(this.groupBox1);
            base.Margin = new Padding(4, 4, 4, 4);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "SearchTables";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "搜索表";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Button btnExpToWord;
        private Button btnClose;
        private ListView listView1;
        private Button btnSearch;
        private TextBox txtKeyWord;
        private ComboBox cmboxType;
        private ComboBox cmbDB;
        private Label lblServer;
        private Label label1;
        private Label label3;
        private Label lblTip;
        private ProgressBar progressBar1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem 复制ToolStripMenuItem;
        private ToolStripMenuItem 所选表名ToolStripMenuItem;
        private ToolStripMenuItem 全部表名ToolStripMenuItem1;
        private ToolStripMenuItem 所选字段ToolStripMenuItem;
        private ToolStripMenuItem 全部字段ToolStripMenuItem;
        private ToolStripMenuItem 移除ToolStripMenuItem;
        private System.Windows.Forms.CheckBox chkClearList;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem 代码生成器ToolStripMenuItem;
        #endregion
    }
}