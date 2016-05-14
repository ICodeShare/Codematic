using System;
using System.Drawing;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;
using System.IO;
using System.Text;
using System.Data;
using Maticsoft.CodeBuild;
using Maticsoft.Utility;
using Maticsoft.CodeHelper;
using Maticsoft.AddInManager;
using Codematic.UserControls;
namespace Codematic
{
	/// <summary>
	/// 批量代码生成
	/// </summary>
	public partial class CodeExport : Form
	{
		#region
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.Label labelNum;
		private System.Windows.Forms.Button btn_Addlist;
		private System.Windows.Forms.Button btn_Add;
		private System.Windows.Forms.Button btn_Del;
		private System.Windows.Forms.Button btn_Dellist;
		private System.Windows.Forms.ListBox listTable2;
		private System.Windows.Forms.ListBox listTable1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox cmbDB;
		private System.Windows.Forms.Label lblServer;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private WiB.Pinkie.Controls.ButtonXP btn_Cancle;
		private WiB.Pinkie.Controls.ButtonXP btn_Export;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label2;
		private WiB.Pinkie.Controls.ButtonXP btn_TargetFold;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtNamespace;
		private System.Windows.Forms.TextBox txtFolder;
		private System.Windows.Forms.TextBox txtTargetFolder;
		private System.Windows.Forms.GroupBox groupBox5;
        private GroupBox groupBox6;
        private RadioButton radBtn_One;
        private RadioButton radBtn_F3;
        private RadioButton radBtn_S3;
        private TextBox txtDbHelper;
        private Label label6;
        private PictureBox pictureBox1;
        private TextBox txtTabNamepre;
        private Label label7;
        private Label label8;
		#endregion

        DALTypeAddIn cm_daltype;
        DALTypeAddIn cm_blltype;
        DALTypeAddIn cm_webtype;

        Thread mythread;
        string cmcfgfile = Application.StartupPath + @"\cmcfg.ini";
        Maticsoft.Utility.INIFile cfgfile;
        Maticsoft.IDBO.IDbObject dbobj;//数据库对象
        Maticsoft.CodeBuild.CodeBuilders cb;//代码生成对象
        Maticsoft.CmConfig.DbSettings dbset;//服务器配置        
        Maticsoft.CmConfig.ModuleSettings setting;
        NameRule namerule = new NameRule();//表命名规则

        delegate void SetBtnEnableCallback();
        delegate void SetBtnDisableCallback();
        delegate void SetlblStatuCallback(string text);
        delegate void SetProBar1MaxCallback(int val);
        delegate void SetProBar1ValCallback(int val);
        string dbname="";
  

        VSProject vsp = new VSProject();

        #region 构造函数

        public CodeExport(string longservername)
		{			
			InitializeComponent();
            dbset = Maticsoft.CmConfig.DbConfig.GetSetting(longservername);
            dbobj = Maticsoft.DBFactory.DBOMaker.CreateDbObj(dbset.DbType);
            dbobj.DbConnectStr = dbset.ConnectStr;      
            cb = new CodeBuilders(dbobj);
            this.lblServer.Text = dbset.Server;
        }       
        #endregion

        #region Windows 窗体设计器生成的代码
        /// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{            
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CodeExport));
            this.btn_Export = new WiB.Pinkie.Controls.ButtonXP();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_Addlist = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.btn_Del = new System.Windows.Forms.Button();
            this.btn_Dellist = new System.Windows.Forms.Button();
            this.listTable2 = new System.Windows.Forms.ListBox();
            this.listTable1 = new System.Windows.Forms.ListBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.labelNum = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbDB = new System.Windows.Forms.ComboBox();
            this.lblServer = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_Cancle = new WiB.Pinkie.Controls.ButtonXP();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtTargetFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_TargetFold = new WiB.Pinkie.Controls.ButtonXP();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTabNamepre = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDbHelper = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNamespace = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.radBtn_One = new System.Windows.Forms.RadioButton();
            this.radBtn_F3 = new System.Windows.Forms.RadioButton();
            this.radBtn_S3 = new System.Windows.Forms.RadioButton();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Export
            // 
            this.btn_Export._Image = null;
            this.btn_Export.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Export.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.btn_Export.DefaultScheme = false;
            this.btn_Export.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btn_Export.Image = null;
            this.btn_Export.Location = new System.Drawing.Point(371, 483);
            this.btn_Export.Name = "btn_Export";
            this.btn_Export.Scheme = WiB.Pinkie.Controls.ButtonXP.Schemes.Blue;
            this.btn_Export.Size = new System.Drawing.Size(75, 26);
            this.btn_Export.TabIndex = 46;
            this.btn_Export.Text = "导  出";
            this.btn_Export.Click += new System.EventHandler(this.btn_Export_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_Addlist);
            this.groupBox2.Controls.Add(this.btn_Add);
            this.groupBox2.Controls.Add(this.btn_Del);
            this.groupBox2.Controls.Add(this.btn_Dellist);
            this.groupBox2.Controls.Add(this.listTable2);
            this.groupBox2.Controls.Add(this.listTable1);
            this.groupBox2.Location = new System.Drawing.Point(8, 56);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(592, 139);
            this.groupBox2.TabIndex = 44;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "选择表";
            // 
            // btn_Addlist
            // 
            this.btn_Addlist.Enabled = false;
            this.btn_Addlist.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Addlist.Location = new System.Drawing.Point(275, 20);
            this.btn_Addlist.Name = "btn_Addlist";
            this.btn_Addlist.Size = new System.Drawing.Size(40, 23);
            this.btn_Addlist.TabIndex = 7;
            this.btn_Addlist.Text = ">>";
            this.btn_Addlist.Click += new System.EventHandler(this.btn_Addlist_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Enabled = false;
            this.btn_Add.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Add.Location = new System.Drawing.Point(275, 48);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(40, 23);
            this.btn_Add.TabIndex = 8;
            this.btn_Add.Text = ">";
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // btn_Del
            // 
            this.btn_Del.Enabled = false;
            this.btn_Del.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Del.Location = new System.Drawing.Point(275, 76);
            this.btn_Del.Name = "btn_Del";
            this.btn_Del.Size = new System.Drawing.Size(40, 23);
            this.btn_Del.TabIndex = 5;
            this.btn_Del.Text = "<";
            this.btn_Del.Click += new System.EventHandler(this.btn_Del_Click);
            // 
            // btn_Dellist
            // 
            this.btn_Dellist.Enabled = false;
            this.btn_Dellist.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Dellist.Location = new System.Drawing.Point(275, 104);
            this.btn_Dellist.Name = "btn_Dellist";
            this.btn_Dellist.Size = new System.Drawing.Size(40, 23);
            this.btn_Dellist.TabIndex = 6;
            this.btn_Dellist.Text = "<<";
            this.btn_Dellist.Click += new System.EventHandler(this.btn_Dellist_Click);
            // 
            // listTable2
            // 
            this.listTable2.ItemHeight = 12;
            this.listTable2.Location = new System.Drawing.Point(356, 20);
            this.listTable2.Name = "listTable2";
            this.listTable2.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listTable2.Size = new System.Drawing.Size(218, 112);
            this.listTable2.TabIndex = 1;
            this.listTable2.DoubleClick += new System.EventHandler(this.listTable2_DoubleClick);
            // 
            // listTable1
            // 
            this.listTable1.ItemHeight = 12;
            this.listTable1.Location = new System.Drawing.Point(16, 20);
            this.listTable1.Name = "listTable1";
            this.listTable1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listTable1.Size = new System.Drawing.Size(218, 112);
            this.listTable1.TabIndex = 0;
            this.listTable1.DoubleClick += new System.EventHandler(this.listTable1_DoubleClick);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(8, 516);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(592, 18);
            this.progressBar1.TabIndex = 10;
            // 
            // labelNum
            // 
            this.labelNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelNum.Location = new System.Drawing.Point(24, 490);
            this.labelNum.Name = "labelNum";
            this.labelNum.Size = new System.Drawing.Size(46, 22);
            this.labelNum.TabIndex = 9;
            this.labelNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbDB);
            this.groupBox1.Controls.Add(this.lblServer);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(8, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(592, 47);
            this.groupBox1.TabIndex = 43;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择数据库";
            // 
            // cmbDB
            // 
            this.cmbDB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDB.Location = new System.Drawing.Point(391, 20);
            this.cmbDB.Name = "cmbDB";
            this.cmbDB.Size = new System.Drawing.Size(152, 20);
            this.cmbDB.TabIndex = 2;
            this.cmbDB.SelectedIndexChanged += new System.EventHandler(this.cmbDB_SelectedIndexChanged);
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(104, 22);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(41, 12);
            this.lblServer.TabIndex = 1;
            this.lblServer.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "当前服务器：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(311, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "选择数据库：";
            // 
            // btn_Cancle
            // 
            this.btn_Cancle._Image = null;
            this.btn_Cancle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Cancle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.btn_Cancle.DefaultScheme = false;
            this.btn_Cancle.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btn_Cancle.Image = null;
            this.btn_Cancle.Location = new System.Drawing.Point(475, 483);
            this.btn_Cancle.Name = "btn_Cancle";
            this.btn_Cancle.Scheme = WiB.Pinkie.Controls.ButtonXP.Schemes.Blue;
            this.btn_Cancle.Size = new System.Drawing.Size(75, 26);
            this.btn_Cancle.TabIndex = 45;
            this.btn_Cancle.Text = "取  消";
            this.btn_Cancle.Click += new System.EventHandler(this.btn_Cancle_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.pictureBox1);
            this.groupBox3.Controls.Add(this.txtTargetFolder);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.btn_TargetFold);
            this.groupBox3.Location = new System.Drawing.Point(8, 423);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(592, 50);
            this.groupBox3.TabIndex = 47;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "保存位置";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Codematic.Properties.Resources.Control;
            this.pictureBox1.Location = new System.Drawing.Point(10, 15);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(28, 28);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 53;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // txtTargetFolder
            // 
            this.txtTargetFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTargetFolder.Location = new System.Drawing.Point(104, 18);
            this.txtTargetFolder.Name = "txtTargetFolder";
            this.txtTargetFolder.Size = new System.Drawing.Size(395, 21);
            this.txtTargetFolder.TabIndex = 45;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 44;
            this.label2.Text = "输出目录：";
            // 
            // btn_TargetFold
            // 
            this.btn_TargetFold._Image = null;
            this.btn_TargetFold.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.btn_TargetFold.DefaultScheme = false;
            this.btn_TargetFold.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btn_TargetFold.Image = null;
            this.btn_TargetFold.Location = new System.Drawing.Point(505, 17);
            this.btn_TargetFold.Name = "btn_TargetFold";
            this.btn_TargetFold.Scheme = WiB.Pinkie.Controls.ButtonXP.Schemes.Blue;
            this.btn_TargetFold.Size = new System.Drawing.Size(57, 23);
            this.btn_TargetFold.TabIndex = 46;
            this.btn_TargetFold.Text = "选择...";
            this.btn_TargetFold.Click += new System.EventHandler(this.btn_TargetFold_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.txtTabNamepre);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.txtDbHelper);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.txtNamespace);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.txtFolder);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Location = new System.Drawing.Point(12, 199);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(592, 77);
            this.groupBox4.TabIndex = 48;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "参数设定";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(323, 53);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 12);
            this.label8.TabIndex = 4;
            this.label8.Text = "(留空表示不变)";
            // 
            // txtTabNamepre
            // 
            this.txtTabNamepre.Location = new System.Drawing.Point(223, 49);
            this.txtTabNamepre.Name = "txtTabNamepre";
            this.txtTabNamepre.Size = new System.Drawing.Size(100, 21);
            this.txtTabNamepre.TabIndex = 3;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 53);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(209, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "表名直接变为类名后，去掉表名前缀：";
            // 
            // txtDbHelper
            // 
            this.txtDbHelper.Location = new System.Drawing.Point(471, 21);
            this.txtDbHelper.Name = "txtDbHelper";
            this.txtDbHelper.Size = new System.Drawing.Size(100, 21);
            this.txtDbHelper.TabIndex = 1;
            this.txtDbHelper.Text = "DbHelperSQL";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(382, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "数据访问类名：";
            // 
            // txtNamespace
            // 
            this.txtNamespace.Location = new System.Drawing.Point(74, 22);
            this.txtNamespace.Name = "txtNamespace";
            this.txtNamespace.Size = new System.Drawing.Size(86, 21);
            this.txtNamespace.TabIndex = 1;
            this.txtNamespace.Text = "CodematicDemo";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "命名空间：";
            // 
            // txtFolder
            // 
            this.txtFolder.Location = new System.Drawing.Point(263, 22);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(100, 21);
            this.txtFolder.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(188, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "子文件夹名：";
            // 
            // groupBox5
            // 
            this.groupBox5.Location = new System.Drawing.Point(8, 326);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(592, 91);
            this.groupBox5.TabIndex = 49;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "代码模板组件类型";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.radBtn_One);
            this.groupBox6.Controls.Add(this.radBtn_F3);
            this.groupBox6.Controls.Add(this.radBtn_S3);
            this.groupBox6.Location = new System.Drawing.Point(12, 279);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(588, 44);
            this.groupBox6.TabIndex = 50;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "架构";
            // 
            // radBtn_One
            // 
            this.radBtn_One.AutoSize = true;
            this.radBtn_One.Location = new System.Drawing.Point(63, 18);
            this.radBtn_One.Name = "radBtn_One";
            this.radBtn_One.Size = new System.Drawing.Size(71, 16);
            this.radBtn_One.TabIndex = 0;
            this.radBtn_One.Text = "单类结构";
            this.radBtn_One.UseVisualStyleBackColor = true;
            // 
            // radBtn_F3
            // 
            this.radBtn_F3.AutoSize = true;
            this.radBtn_F3.Checked = true;
            this.radBtn_F3.Location = new System.Drawing.Point(300, 18);
            this.radBtn_F3.Name = "radBtn_F3";
            this.radBtn_F3.Size = new System.Drawing.Size(95, 16);
            this.radBtn_F3.TabIndex = 0;
            this.radBtn_F3.TabStop = true;
            this.radBtn_F3.Text = "工厂模式三层";
            this.radBtn_F3.UseVisualStyleBackColor = true;
            // 
            // radBtn_S3
            // 
            this.radBtn_S3.AutoSize = true;
            this.radBtn_S3.Location = new System.Drawing.Point(180, 18);
            this.radBtn_S3.Name = "radBtn_S3";
            this.radBtn_S3.Size = new System.Drawing.Size(71, 16);
            this.radBtn_S3.TabIndex = 0;
            this.radBtn_S3.Text = "简单三层";
            this.radBtn_S3.UseVisualStyleBackColor = true;
            // 
            // CodeExport
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(612, 537);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btn_Export);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_Cancle);
            this.Controls.Add(this.labelNum);
            this.Controls.Add(this.progressBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CodeExport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "导出代码文件";
            this.Load += new System.EventHandler(this.CodeExport_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

        #region 初始化
        private void CodeExport_Load(object sender, System.EventArgs e)
		{
            string mastedb = "master";
            switch (dbobj.DbType)
            {
                case "SQL2000":
                case "SQL2005":
                    mastedb = "master";
                    break;
                case "Oracle":
                case "OleDb":
                    mastedb = dbset.DbName;
                    break;                    
                case "MySQL":
                    mastedb = "mysql";
                    break;
            }
            if ((dbset.DbName == "") || (dbset.DbName == mastedb))            
            {               
                List<string> dblist = dbobj.GetDBList();
                if (dblist != null)
                {
                    if (dblist.Count > 0)
                    {
                        foreach (string dbname in dblist)
                        {
                            this.cmbDB.Items.Add(dbname);
                        }
                    }
                }
            }
            else
            {
                this.cmbDB.Items.Add(dbset.DbName);
            }

			if(this.cmbDB.Items.Count>0)
			{
				this.cmbDB.SelectedIndex=0;
			}
			else
			{
                List<string> tabNames = dbobj.GetTables("");
				this.listTable1.Items.Clear();
				this.listTable2.Items.Clear();
                if (tabNames.Count > 0)
				{
                    foreach (string tabname in tabNames)
					{
                        listTable1.Items.Add(tabname);	
					}
				}
			}
			this.btn_Export.Enabled=false;	
            setting = Maticsoft.CmConfig.ModuleConfig.GetSettings();
            switch (setting.AppFrame)
            {
                case "One":
                    this.radBtn_One.Checked = true;
                    break;
                case "S3":
                    this.radBtn_S3.Checked = true;
                    break;
                case "F3":
                    this.radBtn_F3.Checked = true;
                    break;
            }

            #region 加载插件

            //cm_modeltype = new DALTypeAddIn("LTP.IBuilder.IBuilderModel");
            //cm_modeltype.Title = "Model";
            //groupBox5.Controls.Add(cm_modeltype);
            //cm_modeltype.Location = new System.Drawing.Point(30, 16);
            //cm_modeltype.SetSelectedDALType(setting.BLLType.Trim());  

            cm_daltype = new DALTypeAddIn("LTP.IBuilder.IBuilderDAL");
            cm_daltype.Title = "DAL";
            groupBox5.Controls.Add(cm_daltype);
            cm_daltype.Location = new System.Drawing.Point(30, 16);
            cm_daltype.SetSelectedDALType(setting.DALType);
            
            cm_blltype = new DALTypeAddIn("LTP.IBuilder.IBuilderBLL");
            cm_blltype.Title = "BLL";
            groupBox5.Controls.Add(cm_blltype);
            cm_blltype.Location = new System.Drawing.Point(30, 40);
            cm_blltype.SetSelectedDALType(setting.BLLType.Trim());  
            
            cm_webtype = new DALTypeAddIn("LTP.IBuilder.IBuilderWeb");
            cm_webtype.Title = "Web";
            groupBox5.Controls.Add(cm_webtype);
            cm_webtype.Location = new System.Drawing.Point(30, 64);
            cm_webtype.SetSelectedDALType(setting.WebType.Trim()); 
           
            #endregion
            
            txtDbHelper.Text = setting.DbHelperName;
            if (setting.DbHelperName == "DbHelperSQL")
            {
                switch (dbobj.DbType)
                {
                    case "SQL2000":
                    case "SQL2005":
                        txtDbHelper.Text = "DbHelperSQL";
                        break;
                    case "Oracle":
                        txtDbHelper.Text = "DbHelperOra";
                        break;
                    case "MySQL":
                        txtDbHelper.Text = "DbHelperMySQL";
                        break;
                    case "OleDb":
                        txtDbHelper.Text = "DbHelperOleDb";
                        break;
                }
            }
            txtFolder.Text = setting.Folder;
            txtNamespace.Text = setting.Namepace;

            if (File.Exists(cmcfgfile))
            {
                cfgfile = new Maticsoft.Utility.INIFile(cmcfgfile);
                string lastpath = cfgfile.IniReadValue("Project", "lastpath");
                if (lastpath.Trim() != "")
                {
                    txtTargetFolder.Text = lastpath;
                }
            }
		}

		private void cmbDB_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			string dbname= cmbDB.Text;
            List<string> tabNames = dbobj.GetTables(dbname);
			this.listTable1.Items.Clear();
			this.listTable2.Items.Clear();
            if (tabNames.Count > 0)
            {
                foreach (string tabname in tabNames)
                {
                    listTable1.Items.Add(tabname);
                }
            }	
			
			IsHasItem();

        }

        
        #endregion

        #region listbox 操作

        private void btn_Addlist_Click(object sender, System.EventArgs e)
		{
			int c=this.listTable1.Items.Count;
			for(int i=0;i<c;i++)
			{
				this.listTable2.Items.Add(this.listTable1.Items[i]);
			}
			this.listTable1.Items.Clear();

			IsHasItem();
		}

		private void btn_Add_Click(object sender, System.EventArgs e)
		{
			int c=this.listTable1.SelectedItems.Count;			
			ListBox.SelectedObjectCollection objs=this.listTable1.SelectedItems;
			for(int i=0;i<c;i++)
			{
				this.listTable2.Items.Add(listTable1.SelectedItems[i]);
				
			}
			for(int i=0;i<c;i++)
			{
				if(this.listTable1.SelectedItems.Count>0)
				{
					this.listTable1.Items.Remove(listTable1.SelectedItems[0]);
				}
			}
			IsHasItem();
		}

		private void btn_Del_Click(object sender, System.EventArgs e)
		{
			int c=this.listTable2.SelectedItems.Count;			
			ListBox.SelectedObjectCollection objs=this.listTable2.SelectedItems;
			for(int i=0;i<c;i++)
			{
				this.listTable1.Items.Add(listTable2.SelectedItems[i]);
				
			}
			for(int i=0;i<c;i++)
			{
				if(this.listTable2.SelectedItems.Count>0)
				{
					this.listTable2.Items.Remove(listTable2.SelectedItems[0]);
				}
			}
		
			IsHasItem();
		}

		private void btn_Dellist_Click(object sender, System.EventArgs e)
		{
			int c=this.listTable2.Items.Count;
			for(int i=0;i<c;i++)
			{
				this.listTable1.Items.Add(this.listTable2.Items[i]);
			}
			this.listTable2.Items.Clear();
			IsHasItem();	
		}

		private void listTable1_DoubleClick(object sender, System.EventArgs e)
		{
			if(this.listTable1.SelectedItem!=null)
			{
				this.listTable2.Items.Add(listTable1.SelectedItem);
				this.listTable1.Items.Remove(this.listTable1.SelectedItem);
				IsHasItem();
			}
		}

		private void listTable2_DoubleClick(object sender, System.EventArgs e)
		{
			if(this.listTable2.SelectedItem!=null)
			{
				this.listTable1.Items.Add(listTable2.SelectedItem);
				this.listTable2.Items.Remove(this.listTable2.SelectedItem);
				IsHasItem();
			}
		}
		/// <summary>
		/// 判断listbox有没有项目
		/// </summary>
		private void IsHasItem()
		{
			if(this.listTable1.Items.Count>0)
			{
				this.btn_Add.Enabled=true;
				this.btn_Addlist.Enabled=true;
			}
			else
			{
				this.btn_Add.Enabled=false;
				this.btn_Addlist.Enabled=false;
			}
			if(this.listTable2.Items.Count>0)
			{
				this.btn_Del.Enabled=true;
				this.btn_Dellist.Enabled=true;	
				this.btn_Export.Enabled=true;
			}
			else
			{
				this.btn_Del.Enabled=false;
				this.btn_Dellist.Enabled=false;	
				this.btn_Export.Enabled=false;
			}
		}
		#endregion

        #region 异步控件设置      
        public void SetBtnEnable()
        {
            if (this.btn_Export.InvokeRequired)
            {
                SetBtnEnableCallback d = new SetBtnEnableCallback(SetBtnEnable);
                this.Invoke(d, null);
            }
            else
            {
                this.btn_Export.Enabled = true;
                this.btn_Cancle.Enabled = true;
            }
        }
        public void SetBtnDisable()
        {
            if (this.btn_Export.InvokeRequired)
            {
                SetBtnDisableCallback d = new SetBtnDisableCallback(SetBtnDisable);
                this.Invoke(d, null);
            }
            else
            {
                this.btn_Export.Enabled = false;
                this.btn_Cancle.Enabled = false;
            }
        }
        public void SetlblStatuText(string text)
        {
            if (this.labelNum.InvokeRequired)
            {
                SetlblStatuCallback d = new SetlblStatuCallback(SetlblStatuText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.labelNum.Text = text;
            }
        }
        /// <summary>
        /// 循环网址进度最大值
        /// </summary>
        /// <param name="val"></param>
        public void SetprogressBar1Max(int val)
        {
            if (this.progressBar1.InvokeRequired)
            {
                SetProBar1MaxCallback d = new SetProBar1MaxCallback(SetprogressBar1Max);
                this.Invoke(d, new object[] { val });
            }
            else
            {
                this.progressBar1.Maximum = val;

            }
        }
        /// <summary>
        /// 循环网址进度
        /// </summary>
        /// <param name="val"></param>
        public void SetprogressBar1Val(int val)
        {
            if (this.progressBar1.InvokeRequired)
            {
                SetProBar1ValCallback d = new SetProBar1ValCallback(SetprogressBar1Val);
                this.Invoke(d, new object[] { val });
            }
            else
            {
                this.progressBar1.Value = val;

            }
        }
        #endregion


        #region 按钮
        private void btn_Cancle_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btn_Export_Click(object sender, System.EventArgs e)
		{
            try
            {
                if (this.txtTargetFolder.Text.Trim() == "")
                {
                    MessageBox.Show("目标文件夹为空！");
                    return;
                }
                cfgfile.IniWriteValue("Project", "lastpath", txtTargetFolder.Text.Trim());
                dbname = this.cmbDB.Text;
                pictureBox1.Visible = true;
                mythread = new Thread(new ThreadStart(ThreadWork));
                mythread.Start();
                //ThreadWork();
            }
            catch (System.Exception ex)
            {
                LogInfo.WriteLog(ex);
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }    

        }
        private void btn_TargetFold_Click(object sender, System.EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            DialogResult result = folder.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                this.txtTargetFolder.Text = folder.SelectedPath;
            }
        }
        #endregion

        void ThreadWork()
		{
            SetBtnDisable();                        
			string strnamespace=this.txtNamespace.Text.Trim();
			string strfolder=this.txtFolder.Text.Trim();			            
			int tblCount=this.listTable2.Items.Count;

            SetprogressBar1Max(tblCount);
            SetprogressBar1Val(1);
            SetlblStatuText("0");
			
			cb.DbName=dbname;			
			if(strnamespace!="")
			{
				cb.NameSpace=strnamespace;
                setting.Namepace = strnamespace;
			}					
			cb.Folder=strfolder;
            setting.Folder = strfolder;
            cb.DbHelperName = txtDbHelper.Text.Trim();
            cb.ProcPrefix = setting.ProcPrefix;
            setting.DbHelperName = txtDbHelper.Text.Trim();
            Maticsoft.CmConfig.ModuleConfig.SaveSettings(setting);
            
            #region 循环每个表

            for (int i=0;i<tblCount;i++)
			{
				string tablename=this.listTable2.Items[i].ToString();
				cb.TableName=tablename;
                cb.ModelName = tablename;
                
                string tabpre=txtTabNamepre.Text.Trim();
                if ( tabpre!= "")
                {
                    if (tablename.StartsWith(tabpre))
                    {
                        cb.ModelName = tablename.Substring(tabpre.Length); 
                    }
                }
                string strmodelname = cb.ModelName;
                //命名规则处理
                cb.ModelName = NameRule.GetModelClass(strmodelname, dbset);
                cb.BLLName = NameRule.GetBLLClass(strmodelname, dbset);
                cb.DALName = NameRule.GetDALClass(strmodelname, dbset);

                DataTable dtkey=dbobj.GetKeyName(dbname,tablename);
                List<ColumnInfo> collist = dbobj.GetColumnInfoList(dbname, tablename);
                cb.Fieldlist = collist; 
                cb.Keys = CodeCommon.GetColumnInfos(dtkey); 
                CreatCS();			

                SetprogressBar1Val(i + 1);
                SetlblStatuText((i + 1).ToString());
			}

			#endregion
	
			
            SetBtnEnable();
			MessageBox.Show(this,"文档生成成功！","完成",MessageBoxButtons.OK,MessageBoxIcon.Information);
		}

        #region 生成 C#代码

        //得到数据层类型
        private string GetDALType()
        {
            string daltype = "";
            daltype = cm_daltype.AppGuid;
            if ((daltype == "") && (daltype == "System.Data.DataRowView"))
            {
                MessageBox.Show("选择的数据层类型有误，请关闭后重试！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return "";
            }
            return daltype;                       
            
        }
        //得到bll层类型
        private string GetBLLType()
        {
            string blltype = "";
            blltype = cm_blltype.AppGuid;
            if ((blltype == "") && (blltype == "System.Data.DataRowView"))
            {
                MessageBox.Show("选择的数据层类型有误，请关闭后重试！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return "";
            }
            return blltype;
        }

        //得到数据层类型
        public string GetWebType()
        {
            string webtype = "";
            webtype = cm_webtype.AppGuid;
            if ((webtype == "") && (webtype == "System.Data.DataRowView"))
            {
                MessageBox.Show("选择的表示层类型有误，请关闭后重试！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return "";
            }
            return webtype;
        }

        ////得到Model层类型
        //private string GetModelType()
        //{
        //    string blltype = "";
        //    blltype = cm_blltype.AppGuid;
        //    if ((blltype == "") && (blltype == "System.Data.DataRowView"))
        //    {
        //        MessageBox.Show("选择的数据层类型有误，请关闭后重试！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        return "";
        //    }
        //    return blltype;
        //}

        //架构选择
        private void CreatCS()
        {
            if (this.radBtn_One.Checked)
            {
                CreatCsOne();
            }   
            if (this.radBtn_S3.Checked)
            {
                CreatCsS3();
            }
            if (this.radBtn_F3.Checked)
            {
                CreatCsF3();
            }
                   
        }
               
        //把代码写入指定文件
        private void WriteFile(string Filename,string strCode)
        {
            StreamWriter sw = new StreamWriter(Filename, false, Encoding.Default);//,false);
            sw.Write(strCode);
            sw.Flush();
            sw.Close();
        }

        #region 单类结构
        private void CreatCsOne()
        {
            string strnamespace = this.txtNamespace.Text.Trim();
            string strfolder = this.txtFolder.Text.Trim();
            if (strfolder.Trim() != "")
            {
                cb.NameSpace = strnamespace + "." + strfolder;
                cb.Folder = strfolder;
            }
            string strCode = cb.GetCodeFrameOne(GetDALType(), true, true, true, true, true, true, true);
            
            string TargetFolder = this.txtTargetFolder.Text;
            FolderCheck(TargetFolder);

            string classFolder = TargetFolder + "\\Class";
            FolderCheck(classFolder);
            string filename = classFolder + "\\" + cb.ModelName + ".cs";
            WriteFile(filename, strCode);
        }
        #endregion

        #region 简单三层
        private void CreatCsS3()
        {
            string TargetFolder = this.txtTargetFolder.Text;            
            FolderCheck(TargetFolder);

            #region Model生成
            string modelFolder = TargetFolder + "\\Model";
            if (cb.Folder != "")
            {
                modelFolder = modelFolder + "\\" + cb.Folder;
            }
            FolderCheck(modelFolder);

            string filemodel = modelFolder + "\\" + cb.ModelName + ".cs";
            string strmodel = cb.GetCodeFrameS3Model();
            WriteFile(filemodel, strmodel);
            AddClassFile(modelFolder + "\\Model.csproj", cb.ModelName + ".cs", "");
            #endregion

            #region DAL生成
            string dalFolder = TargetFolder + "\\DAL";
            if (cb.Folder != "")
            {
                dalFolder = dalFolder + "\\" + cb.Folder;
            }
            FolderCheck(dalFolder);
            string filedal = dalFolder + "\\" + cb.DALName + ".cs";
            string strdal = cb.GetCodeFrameS3DAL(GetDALType(), true, true, true, true, true, true, true);
            WriteFile(filedal, strdal);
            AddClassFile(dalFolder + "\\DAL.csproj", cb.DALName + ".cs", "");
            #endregion

            #region BLL生成
            string bllFolder = TargetFolder + "\\BLL";
            if (cb.Folder != "")
            {
                bllFolder = bllFolder + "\\" + cb.Folder;
            }
            FolderCheck(bllFolder);
            string filebll = bllFolder + "\\" + cb.BLLName + ".cs";
            string blltype = GetBLLType();
            string strbll = cb.GetCodeFrameS3BLL(blltype, true, true, true, true, true, true, true, true);
            WriteFile(filebll, strbll);
            AddClassFile(bllFolder + "\\BLL.csproj", cb.BLLName + ".cs", "");
            #endregion


            #region web生成
            string webtype = GetWebType();
            cb.CreatBuilderWeb(webtype);

            string webFolder = TargetFolder + "\\Web";
            if (cb.Folder != "")
            {
                webFolder = webFolder + "\\" + cb.Folder;
            }
            FolderCheck(webFolder);
            FolderCheck(webFolder + "\\" + cb.ModelName);
            string tempstr="";

            #region ADD
            string fileaspx = webFolder + "\\" + cb.ModelName + "\\Add.aspx";
            string fileaspxcs = webFolder + "\\" + cb.ModelName + "\\Add.aspx.cs";
            string fileaspxds = webFolder + "\\" + cb.ModelName + "\\Add.aspx.designer.cs";

            string tempaspx = Application.StartupPath + @"\Template\web\Add.aspx";
            string tempaspxcs = Application.StartupPath + @"\Template\web\Add.aspx.cs";
            string tempaspxds = Application.StartupPath + @"\Template\web\Add.aspx.designer.cs";
            if (File.Exists(tempaspx))
            {
                using (StreamReader sr = new StreamReader(tempaspx, Encoding.Default))
                {
                    string s = cb.GetAddAspx();
                    tempstr = sr.ReadToEnd().Replace(".Demo.Add", "." + cb.ModelName + ".Add").Replace("<$$AddAspx$$>", s);
                    sr.Close(); 　
                }
                WriteFile(fileaspx, tempstr);
            }
            if (File.Exists(tempaspxcs))
            {
                using (StreamReader sr = new StreamReader(tempaspxcs, Encoding.Default))
                {
                    string s = cb.GetAddAspxCs();
                    tempstr = sr.ReadToEnd().Replace(".Demo", "." + cb.ModelName).Replace("<$$AddAspxCs$$>", s);
                    sr.Close();
                }
                WriteFile(fileaspxcs, tempstr);
            }
            if (File.Exists(tempaspxds))
            {
                using (StreamReader sr = new StreamReader(tempaspxds, Encoding.Default))
                {
                    string s = cb.GetAddDesigner();
                    tempstr = sr.ReadToEnd().Replace(".Demo", "." + cb.ModelName).Replace("<$$AddDesigner$$>", s);
                    sr.Close();
                }
                WriteFile(fileaspxds, tempstr);
            }
            //AddClassFile(webFolder + "\\Web.csproj", cb.ModelName + "\\Add.aspx", "2005");
            //AddClassFile(webFolder + "\\Web.csproj", cb.ModelName + "\\Add.aspx.cs", "2005");
            //AddClassFile(webFolder + "\\Web.csproj", cb.ModelName + "\\Add.aspx.designer.cs", "2005");
            #endregion

            #region Modify.aspx
            fileaspx = webFolder + "\\" + cb.ModelName + "\\Modify.aspx";
            fileaspxcs = webFolder + "\\" + cb.ModelName + "\\Modify.aspx.cs";
            fileaspxds = webFolder + "\\" + cb.ModelName + "\\Modify.aspx.designer.cs";

            tempaspx = Application.StartupPath + @"\Template\web\Modify.aspx";
            tempaspxcs = Application.StartupPath + @"\Template\web\Modify.aspx.cs";
            tempaspxds = Application.StartupPath + @"\Template\web\Modify.aspx.designer.cs";
            if (File.Exists(tempaspx))
            {
                using (StreamReader sr = new StreamReader(tempaspx, Encoding.Default))
                {
                    string s = cb.GetUpdateAspx();
                    tempstr = sr.ReadToEnd().Replace(".Demo.Modify", "." + cb.ModelName + ".Modify").Replace("<$$ModifyAspx$$>", s);
                    sr.Close();
                }
                WriteFile(fileaspx, tempstr);
            }
            if (File.Exists(tempaspxcs))
            {
                using (StreamReader sr = new StreamReader(tempaspxcs, Encoding.Default))
                {
                    string s = cb.GetUpdateAspxCs();
                    tempstr = sr.ReadToEnd().Replace(".Demo", "." + cb.ModelName).Replace("<$$ModifyAspxCs$$>", s);
                    sr.Close();
                }
                WriteFile(fileaspxcs, tempstr);
            }
            if (File.Exists(tempaspxds))
            {
                using (StreamReader sr = new StreamReader(tempaspxds, Encoding.Default))
                {
                    string s = cb.GetUpdateDesigner();
                    tempstr = sr.ReadToEnd().Replace(".Demo", "." + cb.ModelName).Replace("<$$ModifyDesigner$$>", s);
                    sr.Close();
                }
                WriteFile(fileaspxds, tempstr);
            }
            //AddClassFile(webFolder + "\\Web.csproj", cb.ModelName + "\\Modify.aspx", "2005");
            //AddClassFile(webFolder + "\\Web.csproj", cb.ModelName + "\\Modify.aspx.cs", "2005");
            //AddClassFile(webFolder + "\\Web.csproj", cb.ModelName + "\\Modify.aspx.designer.cs", "2005");
            #endregion

            #region Show
            fileaspx = webFolder + "\\" + cb.ModelName + "\\Show.aspx";
            fileaspxcs = webFolder + "\\" + cb.ModelName + "\\Show.aspx.cs";
            fileaspxds = webFolder + "\\" + cb.ModelName + "\\Show.aspx.designer.cs";

            tempaspx = Application.StartupPath + @"\Template\web\Show.aspx";
            tempaspxcs = Application.StartupPath + @"\Template\web\Show.aspx.cs";
            tempaspxds = Application.StartupPath + @"\Template\web\Show.aspx.designer.cs";
            if (File.Exists(tempaspx))
            {
                using (StreamReader sr = new StreamReader(tempaspx, Encoding.Default))
                {
                    string s = cb.GetShowAspx();
                    tempstr = sr.ReadToEnd().Replace(".Demo.Show", "." + cb.ModelName + ".Show").Replace("<$$ShowAspx$$>", s);
                    sr.Close();
                }
                WriteFile(fileaspx, tempstr);
            }
            if (File.Exists(tempaspxcs))
            {
                using (StreamReader sr = new StreamReader(tempaspxcs, Encoding.Default))
                {
                    string s = cb.GetShowAspxCs();
                    tempstr = sr.ReadToEnd().Replace(".Demo", "." + cb.ModelName).Replace("<$$ShowAspxCs$$>", s);
                    sr.Close();
                }
                WriteFile(fileaspxcs, tempstr);
            }
            if (File.Exists(tempaspxds))
            {
                using (StreamReader sr = new StreamReader(tempaspxds, Encoding.Default))
                {
                    string s = cb.GetShowDesigner();
                    tempstr = sr.ReadToEnd().Replace(".Demo", "." + cb.ModelName).Replace("<$$ShowDesigner$$>", s);
                    sr.Close();
                }
                WriteFile(fileaspxds, tempstr);
            }
            //AddClassFile(webFolder + "\\Web.csproj", cb.ModelName + "\\Show.aspx", "2005");
            //AddClassFile(webFolder + "\\Web.csproj", cb.ModelName + "\\Show.aspx.cs", "2005");
            //AddClassFile(webFolder + "\\Web.csproj", cb.ModelName + "\\Show.aspx.designer.cs", "2005");
            #endregion
            
            #endregion

            CheckDirectory(TargetFolder);
        }    
        #endregion

        #region 工厂模式三层

        private void CreatCsF3()
        {
            string TargetFolder = this.txtTargetFolder.Text;            
            FolderCheck(TargetFolder);

            #region Model生成
            string modelFolder = TargetFolder + "\\Model";
            if (cb.Folder != "")
            {
                modelFolder = modelFolder + "\\" + cb.Folder;
            }
            FolderCheck(modelFolder);
            string filemodel = modelFolder + "\\" + cb.ModelName + ".cs";
            string strmodel = cb.GetCodeFrameS3Model();
            WriteFile(filemodel, strmodel);
            AddClassFile(modelFolder + "\\Model.csproj", cb.ModelName + ".cs", "");
            #endregion

            #region DAL生成
            string strdbtype = dbobj.DbType;
            if (dbobj.DbType == "SQL2000" || dbobj.DbType == "SQL2005")
            {
                strdbtype = "SQLServer";
            }
            string dalFolder = TargetFolder + "\\" + strdbtype + "DAL";
            if (cb.Folder != "")
            {
                dalFolder = dalFolder + "\\" + cb.Folder;
            }
            FolderCheck(dalFolder);
            string filedal = dalFolder + "\\" + cb.DALName + ".cs";
            string strdal = cb.GetCodeFrameF3DAL(GetDALType(), true, true, true, true, true, true, true);
            WriteFile(filedal, strdal);

            AddClassFile(dalFolder + "\\" + strdbtype + "DAL.csproj", cb.DALName + ".cs", "");
            #endregion

            #region  DALFactory
            string factoryFolder = TargetFolder + "\\DALFactory";
            FolderCheck(factoryFolder);
            string filedalfac = factoryFolder + "\\DataAccess.cs";
            string strdalfac = cb.GetCodeFrameF3DALFactory();
            //已经存在，并且有内容，则追加
            if (File.Exists(filedalfac))
            {
                string temp = File.ReadAllText(filedalfac);
                if (temp.IndexOf("class DataAccess") > 0)
                {
                    strdalfac = cb.GetCodeFrameF3DALFactoryMethod();
                    vsp.AddMethodToClass(filedalfac, strdalfac);
                }
                else
                {
                    strdalfac = cb.GetCodeFrameF3DALFactory();
                    StreamWriter sw = new StreamWriter(filedalfac, true, Encoding.Default);
                    sw.Write(strdalfac);
                    sw.Flush();
                    sw.Close();
                }
            }
            else//否则，新建该文件
            {
                strdalfac = cb.GetCodeFrameF3DALFactory();
                WriteFile(filedalfac, strdalfac);
            }
            #endregion

            #region  IDAL生成
            string idalFolder = TargetFolder + "\\IDAL";
            if (cb.Folder != "")
            {
                idalFolder = idalFolder + "\\" + cb.Folder;
            }
            FolderCheck(idalFolder);
            string fileidal = idalFolder + "\\I" + cb.DALName + ".cs";
            string stridal = cb.GetCodeFrameF3IDAL(true, true, true, true, true, true, true, true);
            WriteFile(fileidal, stridal);
            AddClassFile(idalFolder + "\\IDAL.csproj", "I" + cb.DALName + ".cs", "");
            #endregion

            #region BLL生成
            string bllFolder = TargetFolder + "\\BLL";
            if (cb.Folder != "")
            {
                bllFolder = bllFolder + "\\" + cb.Folder;
            }
            FolderCheck(bllFolder);
            string filebll = bllFolder + "\\" + cb.BLLName + ".cs";
            string blltype = GetBLLType();
            string strbll = cb.GetCodeFrameF3BLL(blltype,true, true, true, true, true, true, true, true);
            WriteFile(filebll, strbll);
            AddClassFile(bllFolder + "\\BLL.csproj", cb.BLLName + ".cs", "");
            #endregion
             
            #region web生成
            string webtype = GetWebType();
            cb.CreatBuilderWeb(webtype);

            string webFolder = TargetFolder + "\\Web";
            if (cb.Folder != "")
            {
                webFolder = webFolder + "\\" + cb.Folder;
            }
            FolderCheck(webFolder);
            FolderCheck(webFolder + "\\" + cb.ModelName);
            string tempstr = "";

            #region ADD
            string fileaspx = webFolder + "\\" + cb.ModelName + "\\Add.aspx";
            string fileaspxcs = webFolder + "\\" + cb.ModelName + "\\Add.aspx.cs";
            string fileaspxds = webFolder + "\\" + cb.ModelName + "\\Add.aspx.designer.cs";

            string tempaspx = Application.StartupPath + @"\Template\web\Add.aspx";
            string tempaspxcs = Application.StartupPath + @"\Template\web\Add.aspx.cs";
            string tempaspxds = Application.StartupPath + @"\Template\web\Add.aspx.designer.cs";
            if (File.Exists(tempaspx))
            {
                using (StreamReader sr = new StreamReader(tempaspx, Encoding.Default))
                {
                    string s = cb.GetAddAspx();
                    tempstr = sr.ReadToEnd().Replace(".Demo.Add", "." + cb.ModelName + ".Add").Replace("<$$AddAspx$$>", s);
                    sr.Close();
                }
                WriteFile(fileaspx, tempstr);
            }
            if (File.Exists(tempaspxcs))
            {
                using (StreamReader sr = new StreamReader(tempaspxcs, Encoding.Default))
                {
                    string s = cb.GetAddAspxCs();
                    tempstr = sr.ReadToEnd().Replace(".Demo", "." + cb.ModelName).Replace("<$$AddAspxCs$$>", s);
                    sr.Close();
                }
                WriteFile(fileaspxcs, tempstr);
            }
            if (File.Exists(tempaspxds))
            {
                using (StreamReader sr = new StreamReader(tempaspxds, Encoding.Default))
                {
                    string s = cb.GetAddDesigner();
                    tempstr = sr.ReadToEnd().Replace(".Demo", "." + cb.ModelName).Replace("<$$AddDesigner$$>", s);
                    sr.Close();
                }
                WriteFile(fileaspxds, tempstr);
            }
            //AddClassFile(webFolder + "\\Web.csproj", cb.ModelName + "\\Add.aspx", "2005");
            //AddClassFile(webFolder + "\\Web.csproj", cb.ModelName + "\\Add.aspx.cs", "2005");
            //AddClassFile(webFolder + "\\Web.csproj", cb.ModelName + "\\Add.aspx.designer.cs", "2005");
            #endregion

            #region Modify.aspx
            fileaspx = webFolder + "\\" + cb.ModelName + "\\Modify.aspx";
            fileaspxcs = webFolder + "\\" + cb.ModelName + "\\Modify.aspx.cs";
            fileaspxds = webFolder + "\\" + cb.ModelName + "\\Modify.aspx.designer.cs";

            tempaspx = Application.StartupPath + @"\Template\web\Modify.aspx";
            tempaspxcs = Application.StartupPath + @"\Template\web\Modify.aspx.cs";
            tempaspxds = Application.StartupPath + @"\Template\web\Modify.aspx.designer.cs";
            if (File.Exists(tempaspx))
            {
                using (StreamReader sr = new StreamReader(tempaspx, Encoding.Default))
                {
                    string s = cb.GetUpdateAspx();
                    tempstr = sr.ReadToEnd().Replace(".Demo.Modify", "." + cb.ModelName + ".Modify").Replace("<$$ModifyAspx$$>", s);
                    sr.Close();
                }
                WriteFile(fileaspx, tempstr);
            }
            if (File.Exists(tempaspxcs))
            {
                using (StreamReader sr = new StreamReader(tempaspxcs, Encoding.Default))
                {
                    string s = cb.GetUpdateAspxCs();
                    tempstr = sr.ReadToEnd().Replace(".Demo", "." + cb.ModelName).Replace("<$$ModifyAspxCs$$>", s);
                    sr.Close();
                }
                WriteFile(fileaspxcs, tempstr);
            }
            if (File.Exists(tempaspxds))
            {
                using (StreamReader sr = new StreamReader(tempaspxds, Encoding.Default))
                {
                    string s = cb.GetUpdateDesigner();
                    tempstr = sr.ReadToEnd().Replace(".Demo", "." + cb.ModelName).Replace("<$$ModifyDesigner$$>", s);
                    sr.Close();
                }
                WriteFile(fileaspxds, tempstr);
            }
            //AddClassFile(webFolder + "\\Web.csproj", cb.ModelName + "\\Modify.aspx", "2005");
            //AddClassFile(webFolder + "\\Web.csproj", cb.ModelName + "\\Modify.aspx.cs", "2005");
            //AddClassFile(webFolder + "\\Web.csproj", cb.ModelName + "\\Modify.aspx.designer.cs", "2005");
            #endregion

            #region Show
            fileaspx = webFolder + "\\" + cb.ModelName + "\\Show.aspx";
            fileaspxcs = webFolder + "\\" + cb.ModelName + "\\Show.aspx.cs";
            fileaspxds = webFolder + "\\" + cb.ModelName + "\\Show.aspx.designer.cs";

            tempaspx = Application.StartupPath + @"\Template\web\Show.aspx";
            tempaspxcs = Application.StartupPath + @"\Template\web\Show.aspx.cs";
            tempaspxds = Application.StartupPath + @"\Template\web\Show.aspx.designer.cs";
            if (File.Exists(tempaspx))
            {
                using (StreamReader sr = new StreamReader(tempaspx, Encoding.Default))
                {
                    string s = cb.GetShowAspx();
                    tempstr = sr.ReadToEnd().Replace(".Demo.Show", "." + cb.ModelName + ".Show").Replace("<$$ShowAspx$$>", s);
                    sr.Close();
                }
                WriteFile(fileaspx, tempstr);
            }
            if (File.Exists(tempaspxcs))
            {
                using (StreamReader sr = new StreamReader(tempaspxcs, Encoding.Default))
                {
                    string s = cb.GetShowAspxCs();
                    tempstr = sr.ReadToEnd().Replace(".Demo", "." + cb.ModelName).Replace("<$$ShowAspxCs$$>", s);
                    sr.Close();
                }
                WriteFile(fileaspxcs, tempstr);
            }
            if (File.Exists(tempaspxds))
            {
                using (StreamReader sr = new StreamReader(tempaspxds, Encoding.Default))
                {
                    string s = cb.GetShowDesigner();
                    tempstr = sr.ReadToEnd().Replace(".Demo", "." + cb.ModelName).Replace("<$$ShowDesigner$$>", s);
                    sr.Close();
                }
                WriteFile(fileaspxds, tempstr);
            }
            //AddClassFile(webFolder + "\\Web.csproj", cb.ModelName + "\\Show.aspx", "2005");
            //AddClassFile(webFolder + "\\Web.csproj", cb.ModelName + "\\Show.aspx.cs", "2005");
            //AddClassFile(webFolder + "\\Web.csproj", cb.ModelName + "\\Show.aspx.designer.cs", "2005");
            #endregion

            #endregion

            CheckDirectory(TargetFolder);

        }
             
        #endregion

        #endregion

        #region 公共方法
        private void FolderCheck(string Folder)
        {
            DirectoryInfo target = new DirectoryInfo(Folder);
            if (!target.Exists)
            {
                target.Create();
            }
        }
        /// <summary>
        ///  修改项目文件
        /// </summary>
        /// <param name="ProjectFile">项目文件名</param>
        /// <param name="classFileName">类文件名</param>
        /// <param name="ProType">项目类型</param>
        private void AddClassFile(string ProjectFile,string classFileName,string ProType)
        {
            if (File.Exists(ProjectFile))
            {                
                switch (ProType)
                {
                    case "2003":
                        vsp.AddClass2003(ProjectFile, classFileName);
                        break;
                    case "2005":
                        vsp.AddClass2005(ProjectFile, classFileName);
                        break;
                    default:
                        vsp.AddClass(ProjectFile, classFileName);
                        break;                        
                }
            }
        }
        /// <summary>
        /// 命名空间名检查
        /// </summary>
        /// <param name="SourceDirectory"></param>
        public void CheckDirectory(string SourceDirectory)
        {
            DirectoryInfo source = new DirectoryInfo(SourceDirectory);
            if (!source.Exists)
                return;

            FileInfo[] sourceFiles = source.GetFiles();
            int filescount = sourceFiles.Length;
            for (int i = 0; i < filescount; ++i)
            {
                //if ((sourceFiles[i].Extension == ".cs") || (sourceFiles[i].Extension == ".ascx") ||
                //    (sourceFiles[i].Extension == ".aspx") || (sourceFiles[i].Extension == ".csproj"))
                //{
                //ReplaceNamespace(sourceFiles[i].FullName, txtNamespace.Text.Trim()); 
                //}
                switch (sourceFiles[i].Extension)
                {
                    case ".csproj":
                        ReplaceNamespaceProj(sourceFiles[i].FullName, txtNamespace.Text.Trim());
                        break;
                    case ".cs":
                    case ".ascx":
                    case ".aspx":
                    case ".asax":
                    case ".master":
                        ReplaceNamespace(sourceFiles[i].FullName, txtNamespace.Text.Trim());
                        break;
                    default:
                        break;
                }
            }

            DirectoryInfo[] sourceDirectories = source.GetDirectories();
            for (int j = 0; j < sourceDirectories.Length; ++j)
            {
                CheckDirectory(sourceDirectories[j].FullName);
            }
        }
        private void ReplaceNamespace(string filename, string spacename)
        {
            StreamReader sr = new StreamReader(filename, Encoding.Default);
            string text = sr.ReadToEnd();
            sr.Close();

            text = text.Replace("<$$namespace$$>", spacename);
            //text = text.Replace("namespace Maticsoft", "namespace " + spacename);
            //text = text.Replace("Inherits=\"Maticsoft", "Inherits=\"" + spacename);

            StreamWriter sw = new StreamWriter(filename, false, Encoding.Default);//,false);
            sw.Write(text);
            sw.Flush();//从缓冲区写入基础流（文件）
            sw.Close();
        }
        private void ReplaceNamespaceProj(string filename, string spacename)
        {
            StreamReader sr = new StreamReader(filename, Encoding.Default);
            string text = sr.ReadToEnd();
            sr.Close();

            text = text.Replace("<AssemblyName>Maticsoft.", "<AssemblyName>" + spacename + ".");
            text = text.Replace("<RootNamespace>Maticsoft.", "<RootNamespace>" + spacename + ".");

            StreamWriter sw = new StreamWriter(filename, false, Encoding.Default);//,false);
            sw.Write(text);
            sw.Flush();//从缓冲区写入基础流（文件）
            sw.Close();
        }
        #endregion
    }
}
