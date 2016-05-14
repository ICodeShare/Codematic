
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace Codematic
{
    partial class CodeMakerTran
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
            ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(CodeMakerTran));
            this.groupBox1 = new GroupBox();
            this.label3 = new Label();
            this.cmbox_Field = new ComboBox();
            this.btnAdd = new Button();
            this.radbtn_Update = new RadioButton();
            this.radbtn_Delete = new RadioButton();
            this.radbtn_Insert = new RadioButton();
            this.listTable = new ListBox();
            this.groupBox2 = new GroupBox();
            this.listView1 = new ListView();
            this.contextMenuStrip1 = new ContextMenuStrip(this.components);
            this.删除DToolStripMenuItem = new ToolStripMenuItem();
            this.btn_Create = new Button();
            this.groupBox3 = new GroupBox();
            this.txtNameSpace2 = new TextBox();
            this.txtNameSpace = new TextBox();
            this.label2 = new Label();
            this.label1 = new Label();
            this.tabControl1 = new TabControl();
            this.tabPage1 = new TabPage();
            this.tabPage2 = new TabPage();
            this.imageList1 = new ImageList(this.components);
            this.splitContainer1 = new SplitContainer();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            base.SuspendLayout();
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbox_Field);
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.radbtn_Update);
            this.groupBox1.Controls.Add(this.radbtn_Delete);
            this.groupBox1.Controls.Add(this.radbtn_Insert);
            this.groupBox1.Controls.Add(this.listTable);
            this.groupBox1.Dock = DockStyle.Fill;
            this.groupBox1.Location = new Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(551, 198);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择表和操作";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(305, 82);
            this.label3.Name = "label3";
            this.label3.Size = new Size(65, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "条件字段：";
            this.label3.Visible = false;
            this.cmbox_Field.FormattingEnabled = true;
            this.cmbox_Field.Location = new Point(305, 100);
            this.cmbox_Field.Name = "cmbox_Field";
            this.cmbox_Field.Size = new Size(211, 20);
            this.cmbox_Field.TabIndex = 3;
            this.cmbox_Field.Visible = false;
            this.btnAdd.Location = new Point(425, 127);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new Size(91, 32);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "添加操作";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            this.radbtn_Update.AutoSize = true;
            this.radbtn_Update.Location = new Point(380, 41);
            this.radbtn_Update.Name = "radbtn_Update";
            this.radbtn_Update.Size = new Size(59, 16);
            this.radbtn_Update.TabIndex = 1;
            this.radbtn_Update.TabStop = true;
            this.radbtn_Update.Text = "Update";
            this.radbtn_Update.UseVisualStyleBackColor = true;
            this.radbtn_Update.Click += new EventHandler(this.radbtn_Click);
            this.radbtn_Delete.AutoSize = true;
            this.radbtn_Delete.Location = new Point(453, 41);
            this.radbtn_Delete.Name = "radbtn_Delete";
            this.radbtn_Delete.Size = new Size(59, 16);
            this.radbtn_Delete.TabIndex = 1;
            this.radbtn_Delete.TabStop = true;
            this.radbtn_Delete.Text = "Delete";
            this.radbtn_Delete.UseVisualStyleBackColor = true;
            this.radbtn_Delete.Click += new EventHandler(this.radbtn_Click);
            this.radbtn_Insert.AutoSize = true;
            this.radbtn_Insert.Checked = true;
            this.radbtn_Insert.Location = new Point(307, 41);
            this.radbtn_Insert.Name = "radbtn_Insert";
            this.radbtn_Insert.Size = new Size(59, 16);
            this.radbtn_Insert.TabIndex = 1;
            this.radbtn_Insert.TabStop = true;
            this.radbtn_Insert.Text = "Insert";
            this.radbtn_Insert.UseVisualStyleBackColor = true;
            this.radbtn_Insert.Click += new EventHandler(this.radbtn_Click);
            this.listTable.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left);
            this.listTable.FormattingEnabled = true;
            this.listTable.ItemHeight = 12;
            this.listTable.Location = new Point(9, 20);
            this.listTable.Name = "listTable";
            this.listTable.Size = new Size(271, 172);
            this.listTable.TabIndex = 0;
            this.listTable.SelectedIndexChanged += new EventHandler(this.listTable_SelectedIndexChanged);
            this.groupBox2.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            this.groupBox2.Controls.Add(this.listView1);
            this.groupBox2.Location = new Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(551, 134);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "事务操作";
            this.listView1.ContextMenuStrip = this.contextMenuStrip1;
            this.listView1.Dock = DockStyle.Fill;
            this.listView1.Location = new Point(3, 17);
            this.listView1.Name = "listView1";
            this.listView1.Size = new Size(545, 114);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.contextMenuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.删除DToolStripMenuItem
			});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new Size(118, 26);
            this.删除DToolStripMenuItem.Name = "删除DToolStripMenuItem";
            this.删除DToolStripMenuItem.Size = new Size(152, 22);
            this.删除DToolStripMenuItem.Text = "删除(&D)";
            this.删除DToolStripMenuItem.Click += new EventHandler(this.删除DToolStripMenuItem_Click);
            this.btn_Create.Location = new Point(428, 226);
            this.btn_Create.Name = "btn_Create";
            this.btn_Create.Size = new Size(102, 41);
            this.btn_Create.TabIndex = 2;
            this.btn_Create.Text = "生成代码";
            this.btn_Create.UseVisualStyleBackColor = true;
            this.btn_Create.Click += new EventHandler(this.btn_Create_Click);
            this.groupBox3.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            this.groupBox3.Controls.Add(this.txtNameSpace2);
            this.groupBox3.Controls.Add(this.txtNameSpace);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new Point(0, 137);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(548, 76);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "参数";
            this.txtNameSpace2.Location = new Point(360, 20);
            this.txtNameSpace2.Name = "txtNameSpace2";
            this.txtNameSpace2.Size = new Size(162, 21);
            this.txtNameSpace2.TabIndex = 1;
            this.txtNameSpace.Location = new Point(104, 20);
            this.txtNameSpace.Name = "txtNameSpace";
            this.txtNameSpace.Size = new Size(141, 21);
            this.txtNameSpace.TabIndex = 1;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(269, 24);
            this.label2.Name = "label2";
            this.label2.Size = new Size(89, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "二级命名空间：";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(19, 24);
            this.label1.Name = "label1";
            this.label1.Size = new Size(89, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "顶级命名空间：";
            this.tabControl1.Alignment = TabAlignment.Bottom;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = DockStyle.Fill;
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.Location = new Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(565, 538);
            this.tabControl1.TabIndex = 4;
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.ImageIndex = 0;
            this.tabPage1.Location = new Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new Padding(3);
            this.tabPage1.Size = new Size(557, 511);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "事务设置";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage2.ImageIndex = 1;
            this.tabPage2.Location = new Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new Padding(3);
            this.tabPage2.Size = new Size(547, 511);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "代码查看";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.imageList1.ImageStream = (ImageListStreamer)componentResourceManager.GetObject("imageList1.ImageStream");
            this.imageList1.TransparentColor = Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "tab2.gif");
            this.imageList1.Images.SetKeyName(1, "cs.png");
            this.splitContainer1.Dock = DockStyle.Fill;
            this.splitContainer1.Location = new Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = Orientation.Horizontal;
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox3);
            this.splitContainer1.Panel2.Controls.Add(this.btn_Create);
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new Size(551, 505);
            this.splitContainer1.SplitterDistance = 198;
            this.splitContainer1.TabIndex = 4;
            this.AutoScaleDimensions = new SizeF(6f, 12f);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(565, 538);
            this.Controls.Add(this.tabControl1);
            this.Name = "CodeMakerTran";
            this.Text = "事务代码生成器";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private RadioButton radbtn_Insert;
        private ListBox listTable;
        private RadioButton radbtn_Update;
        private RadioButton radbtn_Delete;
        private ListView listView1;
        private Button btnAdd;
        private Button btn_Create;
        private GroupBox groupBox3;
        private Label label2;
        private Label label1;
        private TextBox txtNameSpace2;
        private TextBox txtNameSpace;
        private ComboBox cmbox_Field;
        private Label label3;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private ImageList imageList1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem 删除DToolStripMenuItem;
        private SplitContainer splitContainer1;
        #endregion
    }
}