using Codematic.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WiB.Pinkie.Controls;
namespace Codematic
{
    partial class DbToWeb
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
            this.Text = "DbToWeb";
            this.groupBox3 = new GroupBox();
            this.label5 = new Label();
            this.comboBox1 = new ComboBox();
            this.radioButton2 = new RadioButton();
            this.pictureBox1 = new PictureBox();
            this.label2 = new Label();
            this.labelNum = new Label();
            this.btn_Cancle = new ButtonXP();
            this.btn_Addlist = new Button();
            this.progressBar1 = new ProgressBar();
            this.groupBox1 = new GroupBox();
            this.cmbDB = new ComboBox();
            this.lblServer = new Label();
            this.label1 = new Label();
            this.label3 = new Label();
            this.btn_Add = new Button();
            this.btn_Del = new Button();
            this.btn_Creat = new ButtonXP();
            this.btn_Dellist = new Button();
            this.listTable2 = new ListBox();
            this.groupBox2 = new GroupBox();
            this.listTable1 = new ListBox();
            this.groupBox3.SuspendLayout();
            ((ISupportInitialize)this.pictureBox1).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            base.SuspendLayout();
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.comboBox1);
            this.groupBox3.Controls.Add(this.radioButton2);
            this.groupBox3.Location = new Point(8, 290);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new Size(464, 69);
            this.groupBox3.TabIndex = 51;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "格式";
            this.label5.AutoSize = true;
            this.label5.Location = new Point(142, 33);
            this.label5.Name = "label5";
            this.label5.Size = new Size(65, 12);
            this.label5.TabIndex = 46;
            this.label5.Text = "选择风格：";
            this.label5.Visible = false;
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[]
			{
				"风格一",
				"风格二"
			});
            this.comboBox1.Location = new Point(208, 29);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(121, 20);
            this.comboBox1.TabIndex = 45;
            this.comboBox1.Visible = false;
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new Point(53, 29);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new Size(71, 16);
            this.radioButton2.TabIndex = 0;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "网页格式";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.pictureBox1.Image = Resources.Control;
            this.pictureBox1.Location = new Point(213, 148);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(28, 28);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 53;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(12, 302);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0, 12);
            this.label2.TabIndex = 50;
            this.labelNum.Location = new Point(16, 180);
            this.labelNum.Name = "labelNum";
            this.labelNum.Size = new Size(46, 22);
            this.labelNum.TabIndex = 9;
            this.labelNum.TextAlign = ContentAlignment.MiddleCenter;
            this.btn_Cancle._Image = null;
            this.btn_Cancle.BackColor = Color.FromArgb(0, 240, 240, 240);
            this.btn_Cancle.DefaultScheme = false;
            this.btn_Cancle.DialogResult = 0;
            this.btn_Cancle.Image = null;
            this.btn_Cancle.Location = new Point(350, 371);
            this.btn_Cancle.Name = "btn_Cancle";
            this.btn_Cancle.Scheme = ButtonXP.Schemes.Blue;
            this.btn_Cancle.Size = new Size(75, 26);
            this.btn_Cancle.TabIndex = 49;
            this.btn_Cancle.Text = "取  消";
            this.btn_Cancle.Click += new EventHandler(this.btn_Cancle_Click);
            this.btn_Addlist.Enabled = false;
            this.btn_Addlist.FlatStyle = FlatStyle.Popup;
            this.btn_Addlist.Location = new Point(208, 29);
            this.btn_Addlist.Name = "btn_Addlist";
            this.btn_Addlist.Size = new Size(40, 23);
            this.btn_Addlist.TabIndex = 7;
            this.btn_Addlist.Text = ">>";
            this.btn_Addlist.Click += new EventHandler(this.btn_Addlist_Click);
            this.progressBar1.Location = new Point(64, 182);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new Size(376, 20);
            this.progressBar1.TabIndex = 10;
            this.groupBox1.Controls.Add(this.cmbDB);
            this.groupBox1.Controls.Add(this.lblServer);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new Point(8, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(464, 64);
            this.groupBox1.TabIndex = 46;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择数据库";
            this.cmbDB.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbDB.Location = new Point(296, 24);
            this.cmbDB.Name = "cmbDB";
            this.cmbDB.Size = new Size(152, 20);
            this.cmbDB.TabIndex = 2;
            this.cmbDB.SelectedIndexChanged += new EventHandler(this.cmbDB_SelectedIndexChanged);
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new Point(104, 26);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new Size(41, 12);
            this.lblServer.TabIndex = 1;
            this.lblServer.Text = "label2";
            this.label1.AutoSize = true;
            this.label1.Location = new Point(16, 26);
            this.label1.Name = "label1";
            this.label1.Size = new Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "当前服务器：";
            this.label3.AutoSize = true;
            this.label3.Location = new Point(216, 26);
            this.label3.Name = "label3";
            this.label3.Size = new Size(77, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "选择数据库：";
            this.btn_Add.Enabled = false;
            this.btn_Add.FlatStyle = FlatStyle.Popup;
            this.btn_Add.Location = new Point(208, 60);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new Size(40, 23);
            this.btn_Add.TabIndex = 8;
            this.btn_Add.Text = ">";
            this.btn_Add.Click += new EventHandler(this.btn_Add_Click);
            this.btn_Del.Enabled = false;
            this.btn_Del.FlatStyle = FlatStyle.Popup;
            this.btn_Del.Location = new Point(208, 91);
            this.btn_Del.Name = "btn_Del";
            this.btn_Del.Size = new Size(40, 23);
            this.btn_Del.TabIndex = 5;
            this.btn_Del.Text = "<";
            this.btn_Del.Click += new EventHandler(this.btn_Del_Click);
            this.btn_Creat._Image = null;
            this.btn_Creat.BackColor = Color.FromArgb(0, 240, 240, 240);
            this.btn_Creat.DefaultScheme = false;
            this.btn_Creat.DialogResult = 0;
            this.btn_Creat.Image = null;
            this.btn_Creat.Location = new Point(245, 371);
            this.btn_Creat.Name = "btn_Creat";
            this.btn_Creat.Scheme = ButtonXP.Schemes.Blue;
            this.btn_Creat.Size = new Size(75, 26);
            this.btn_Creat.TabIndex = 48;
            this.btn_Creat.Text = "生  成";
            this.btn_Creat.Click += new EventHandler(this.btn_Creat_Click);
            this.btn_Dellist.Enabled = false;
            this.btn_Dellist.FlatStyle = FlatStyle.Popup;
            this.btn_Dellist.Location = new Point(208, 122);
            this.btn_Dellist.Name = "btn_Dellist";
            this.btn_Dellist.Size = new Size(40, 23);
            this.btn_Dellist.TabIndex = 6;
            this.btn_Dellist.Text = "<<";
            this.btn_Dellist.Click += new EventHandler(this.btn_Dellist_Click);
            this.listTable2.ItemHeight = 12;
            this.listTable2.Location = new Point(288, 24);
            this.listTable2.Name = "listTable2";
            this.listTable2.SelectionMode = SelectionMode.MultiExtended;
            this.listTable2.Size = new Size(152, 148);
            this.listTable2.TabIndex = 1;
            this.listTable2.DoubleClick += new EventHandler(this.listTable2_DoubleClick);
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Controls.Add(this.progressBar1);
            this.groupBox2.Controls.Add(this.labelNum);
            this.groupBox2.Controls.Add(this.btn_Addlist);
            this.groupBox2.Controls.Add(this.btn_Add);
            this.groupBox2.Controls.Add(this.btn_Del);
            this.groupBox2.Controls.Add(this.btn_Dellist);
            this.groupBox2.Controls.Add(this.listTable2);
            this.groupBox2.Controls.Add(this.listTable1);
            this.groupBox2.Location = new Point(8, 78);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(464, 208);
            this.groupBox2.TabIndex = 47;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "选择表";
            this.listTable1.ItemHeight = 12;
            this.listTable1.Location = new Point(16, 24);
            this.listTable1.Name = "listTable1";
            this.listTable1.SelectionMode = SelectionMode.MultiExtended;
            this.listTable1.Size = new Size(152, 148);
            this.listTable1.TabIndex = 0;
            this.listTable1.Click += new EventHandler(this.listTable1_Click);
            this.listTable1.DoubleClick += new EventHandler(this.listTable1_DoubleClick);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(480, 406);
            base.Controls.Add(this.groupBox3);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.btn_Cancle);
            base.Controls.Add(this.groupBox1);
            base.Controls.Add(this.btn_Creat);
            base.Controls.Add(this.groupBox2);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "DbToWeb";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "生成数据库文档";
            base.Load += new EventHandler(this.DbToWeb_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((ISupportInitialize)this.pictureBox1).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private GroupBox groupBox3;
        private ComboBox comboBox1;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private PictureBox pictureBox1;
        private Label label2;
        private Label labelNum;
        private ButtonXP btn_Cancle;
        private Button btn_Addlist;
        private ProgressBar progressBar1;
        private GroupBox groupBox1;
        private ComboBox cmbDB;
        private Label lblServer;
        private Label label1;
        private Label label3;
        private Button btn_Add;
        private Button btn_Del;
        private ButtonXP btn_Creat;
        private Button btn_Dellist;
        private ListBox listTable2;
        private GroupBox groupBox2;
        private ListBox listTable1;
        #endregion
    }
}