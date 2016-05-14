using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Data;
using Maticsoft.CodeBuild;
using Maticsoft.CodeHelper;
using Maticsoft.Utility;
using Maticsoft.AddInManager;
using Codematic.UserControls;
namespace Codematic
{
	/// <summary>
	/// NewProjectDB 的摘要说明。
	/// </summary>
	public partial class NewProjectDB : Form
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
        private System.Windows.Forms.GroupBox groupBox5;
        private Label label5;
        private TextBox txtFolder;
        private Label label4;
        private TextBox txtNamespace;
        private Label label6;
        private TextBox txtDbHelper;
        private GroupBox groupBox4;
        private GroupBox groupBox7;
        private PictureBox pictureBox1;
        private Label label8;
        private TextBox txtTabNamepre;
        private Label label7;
        private WiB.Pinkie.Controls.ButtonXP buttonXP1;
		#endregion

        DALTypeAddIn cm_daltype;
        DALTypeAddIn cm_blltype;
        DALTypeAddIn cm_webtype;


		Thread mythread;
        Maticsoft.IDBO.IDbObject dbobj;//数据库对象
        Maticsoft.CodeBuild.CodeBuilders cb;//代码生成对象
        Maticsoft.CmConfig.DbSettings dbset;//服务器配置        
        Maticsoft.CmConfig.ModuleSettings setting;
        delegate void SetBtnEnableCallback();
        delegate void SetBtnDisableCallback();
        delegate void SetlblStatuCallback(string text);
        delegate void SetProBar1MaxCallback(int val);
        delegate void SetProBar1ValCallback(int val);
        NewProject np;
        
        string ProOutPath="";//输出路径
        string AppFrame = "S3";//架构选择
        string ProName = "";
        VSProject vsp = new VSProject();
        
        string dbname = "";
        NameRule namerule = new NameRule();//表命名规则

        /// <summary>
        /// 
        /// </summary>
        /// <param name="longservername">服务器连接名</param>
        /// <param name="fromForm">选择窗体</param>
        /// <param name="proOutPath">输出路径</param>
        /// <param name="appFrame">架构选择</param>
        public NewProjectDB(string longservername, NewProject fromForm, string proOutPath, string appFrame, string proName)
		{			
			InitializeComponent();
            dbset = Maticsoft.CmConfig.DbConfig.GetSetting(longservername);
            dbobj = Maticsoft.DBFactory.DBOMaker.CreateDbObj(dbset.DbType);
            dbobj.DbConnectStr = dbset.ConnectStr;            
            cb = new CodeBuilders(dbobj);
            this.lblServer.Text = dbset.Server;
            np = fromForm;
            ProOutPath = proOutPath;
            AppFrame = appFrame;
            ProName = proName;
            fromForm.Hide();
        }
        

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewProjectDB));
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
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.buttonXP1 = new WiB.Pinkie.Controls.ButtonXP();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFolder = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNamespace = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDbHelper = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTabNamepre = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            this.btn_Export.Location = new System.Drawing.Point(369, 407);
            this.btn_Export.Name = "btn_Export";
            this.btn_Export.Scheme = WiB.Pinkie.Controls.ButtonXP.Schemes.Blue;
            this.btn_Export.Size = new System.Drawing.Size(75, 26);
            this.btn_Export.TabIndex = 46;
            this.btn_Export.Text = "开始生成";
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
            this.groupBox2.Location = new System.Drawing.Point(8, 54);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(542, 140);
            this.groupBox2.TabIndex = 44;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "选择表";
            // 
            // btn_Addlist
            // 
            this.btn_Addlist.Enabled = false;
            this.btn_Addlist.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Addlist.Location = new System.Drawing.Point(249, 20);
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
            this.btn_Add.Location = new System.Drawing.Point(249, 49);
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
            this.btn_Del.Location = new System.Drawing.Point(249, 77);
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
            this.btn_Dellist.Location = new System.Drawing.Point(249, 106);
            this.btn_Dellist.Name = "btn_Dellist";
            this.btn_Dellist.Size = new System.Drawing.Size(40, 23);
            this.btn_Dellist.TabIndex = 6;
            this.btn_Dellist.Text = "<<";
            this.btn_Dellist.Click += new System.EventHandler(this.btn_Dellist_Click);
            // 
            // listTable2
            // 
            this.listTable2.ItemHeight = 12;
            this.listTable2.Location = new System.Drawing.Point(323, 20);
            this.listTable2.Name = "listTable2";
            this.listTable2.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listTable2.Size = new System.Drawing.Size(198, 112);
            this.listTable2.TabIndex = 1;
            this.listTable2.DoubleClick += new System.EventHandler(this.listTable2_DoubleClick);
            // 
            // listTable1
            // 
            this.listTable1.ItemHeight = 12;
            this.listTable1.Location = new System.Drawing.Point(16, 20);
            this.listTable1.Name = "listTable1";
            this.listTable1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listTable1.Size = new System.Drawing.Size(198, 112);
            this.listTable1.TabIndex = 0;
            this.listTable1.DoubleClick += new System.EventHandler(this.listTable1_DoubleClick);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(43, 371);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(503, 16);
            this.progressBar1.TabIndex = 10;
            // 
            // labelNum
            // 
            this.labelNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelNum.Location = new System.Drawing.Point(10, 414);
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
            this.groupBox1.Location = new System.Drawing.Point(8, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(542, 47);
            this.groupBox1.TabIndex = 43;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "选择数据库";
            // 
            // cmbDB
            // 
            this.cmbDB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDB.Location = new System.Drawing.Point(361, 20);
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
            this.label3.Location = new System.Drawing.Point(281, 22);
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
            this.btn_Cancle.Location = new System.Drawing.Point(460, 407);
            this.btn_Cancle.Name = "btn_Cancle";
            this.btn_Cancle.Scheme = WiB.Pinkie.Controls.ButtonXP.Schemes.Blue;
            this.btn_Cancle.Size = new System.Drawing.Size(75, 26);
            this.btn_Cancle.TabIndex = 45;
            this.btn_Cancle.Text = "取消";
            this.btn_Cancle.Click += new System.EventHandler(this.btn_Cancle_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Location = new System.Drawing.Point(8, 271);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(542, 86);
            this.groupBox5.TabIndex = 49;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "代码模板组件类型";
            // 
            // groupBox7
            // 
            this.groupBox7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox7.Location = new System.Drawing.Point(16, 390);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(530, 4);
            this.groupBox7.TabIndex = 51;
            this.groupBox7.TabStop = false;
            // 
            // buttonXP1
            // 
            this.buttonXP1._Image = null;
            this.buttonXP1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXP1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(212)))), ((int)(((byte)(208)))), ((int)(((byte)(200)))));
            this.buttonXP1.DefaultScheme = false;
            this.buttonXP1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.buttonXP1.Image = null;
            this.buttonXP1.Location = new System.Drawing.Point(278, 408);
            this.buttonXP1.Name = "buttonXP1";
            this.buttonXP1.Scheme = WiB.Pinkie.Controls.ButtonXP.Schemes.Blue;
            this.buttonXP1.Size = new System.Drawing.Size(75, 26);
            this.buttonXP1.TabIndex = 45;
            this.buttonXP1.Text = "< 上一步";
            this.buttonXP1.Click += new System.EventHandler(this.buttonXP1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(182, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "文件夹名：";
            // 
            // txtFolder
            // 
            this.txtFolder.Location = new System.Drawing.Point(246, 17);
            this.txtFolder.Name = "txtFolder";
            this.txtFolder.Size = new System.Drawing.Size(90, 21);
            this.txtFolder.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "命名空间：";
            // 
            // txtNamespace
            // 
            this.txtNamespace.Location = new System.Drawing.Point(76, 17);
            this.txtNamespace.Name = "txtNamespace";
            this.txtNamespace.Size = new System.Drawing.Size(90, 21);
            this.txtNamespace.TabIndex = 1;
            this.txtNamespace.Text = "Maticsoft";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(352, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "数据访问类：";
            // 
            // txtDbHelper
            // 
            this.txtDbHelper.Location = new System.Drawing.Point(436, 17);
            this.txtDbHelper.Name = "txtDbHelper";
            this.txtDbHelper.Size = new System.Drawing.Size(90, 21);
            this.txtDbHelper.TabIndex = 1;
            this.txtDbHelper.Text = "DbHelperSQL";
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
            this.groupBox4.Location = new System.Drawing.Point(8, 196);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(542, 69);
            this.groupBox4.TabIndex = 48;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "参数设定";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(324, 46);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "(留空表示不变)";
            // 
            // txtTabNamepre
            // 
            this.txtTabNamepre.Location = new System.Drawing.Point(224, 42);
            this.txtTabNamepre.Name = "txtTabNamepre";
            this.txtTabNamepre.Size = new System.Drawing.Size(100, 21);
            this.txtTabNamepre.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(209, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "表名直接变为类名后，去掉表名前缀：";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.Image = global::Codematic.Properties.Resources.Control;
            this.pictureBox1.Location = new System.Drawing.Point(12, 363);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(28, 28);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 52;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            // 
            // NewProjectDB
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(562, 445);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.btn_Export);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonXP1);
            this.Controls.Add(this.btn_Cancle);
            this.Controls.Add(this.labelNum);
            this.Controls.Add(this.progressBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewProjectDB";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择要生成的数据表，输出代码文件";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.NewProjectDB_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

        #region 初始化
        private void NewProjectDB_Load(object sender, System.EventArgs e)
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
            IsHasItem();
			this.btn_Export.Enabled=false;		
            setting = Maticsoft.CmConfig.ModuleConfig.GetSettings();            

            #region 加载插件
            

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
                if (ProOutPath == "")
                {
                    MessageBox.Show("目标文件夹为空！");
                    return;
                }
                string sourcefolder = Application.StartupPath + "\\Template\\CodematicDemoS3";
                
                switch (AppFrame)
                {
                    case "One":                        
                        break;
                    case "S3":
                        sourcefolder = Application.StartupPath + "\\Template\\CodematicDemoS3";
                        break;
                    case "S3p":
                        sourcefolder = Application.StartupPath + "\\Template\\CodematicDemoS3p";
                        break;
                    case "F3":
                        sourcefolder = Application.StartupPath + "\\Template\\CodematicDemoF3";
                        break;
                }

                #region 目录检查
                try
                {
                    DirectoryInfo source = new DirectoryInfo(sourcefolder);
                    DirectoryInfo target = new DirectoryInfo(ProOutPath);
                    if (!source.Exists)
                    {
                        MessageBox.Show("项目模板不存在或该版本非完整安装版本，请去官方网站下载最新版本安装再试！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (!target.Exists)
                    {
                        try
                        {
                            target.Create();
                        }
                        catch
                        {
                            MessageBox.Show("目标目录不存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("目录信息有误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                #endregion

                #region 复制项目文件

                //mythread = new Thread(new ThreadStart(ThreadCopyFile));
                //mythread.Start();
                CopyDirectory(sourcefolder, ProOutPath);

                #endregion

                dbname = this.cmbDB.Text;
                pictureBox1.Visible = true;
                
                mythread = new Thread(new ThreadStart(ThreadWork));
                mythread.Start();
                //ThreadWork();
            }
            catch (System.Exception er)
            {
                MessageBox.Show(er.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }    

        }

        #region  复制项目文件

        //void ThreadCopyFile()
        //{
        //    CopyDirectory(folder1, folder2);
        //}
        public void CopyDirectory(string SourceDirectory, string TargetDirectory)
        {
            DirectoryInfo source = new DirectoryInfo(SourceDirectory);
            DirectoryInfo target = new DirectoryInfo(TargetDirectory);
            //Check If we have valid source
            if (!source.Exists)
                return;
            if (!target.Exists)
                target.Create();
            //Copy Files
            FileInfo[] sourceFiles = source.GetFiles();
            int filescount = sourceFiles.Length;
            for (int i = 0; i < filescount; ++i)
            {
                if ((sourceFiles[i].Extension == ".sln") || (sourceFiles[i].Extension == ".suo"))
                {
                    File.Copy(sourceFiles[i].FullName, target.FullName + "\\" + ProName + sourceFiles[i].Extension, true);
                }
                else
                {
                    File.Copy(sourceFiles[i].FullName, target.FullName + "\\" + sourceFiles[i].Name, true);
                }
            }
            //Copy directories
            DirectoryInfo[] sourceDirectories = source.GetDirectories();
            for (int j = 0; j < sourceDirectories.Length; ++j)
            {
                CopyDirectory(sourceDirectories[j].FullName, target.FullName + "\\" + sourceDirectories[j].Name);
            }
        }

        #endregion

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
			}					
			cb.Folder=strfolder;            
            cb.ProcPrefix = setting.ProcPrefix;
            cb.DbHelperName = txtDbHelper.Text.Trim();                        
                      
            #region 循环每个表

            for (int i=0;i<tblCount;i++)
			{
				string tablename=this.listTable2.Items[i].ToString();
				cb.TableName=tablename;
				cb.ModelName=tablename;
                string tabpre = txtTabNamepre.Text.Trim();
                if (tabpre != "")
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


                DataTable dtkey = dbobj.GetKeyName(dbname, tablename);
                List<ColumnInfo> dt = dbobj.GetColumnInfoList(dbname, tablename);
                cb.Fieldlist = dt;
                cb.Keys = Maticsoft.CodeHelper.CodeCommon.GetColumnInfos(dtkey);
                CreatCS();	
                				
                SetprogressBar1Val(i + 1);
                SetlblStatuText((i + 1).ToString());	
			}

			#endregion
				
            SetBtnEnable();
			MessageBox.Show(this,"项目工程生成成功！","完成",MessageBoxButtons.OK,MessageBoxIcon.Information);
            Process proc = new Process();
            Process.Start("explorer.exe", ProOutPath);
            this.Close();
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

        //得到web层类型
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

        //架构选择
        private void CreatCS()
        {
            switch (AppFrame)
            {
                case "One":
                    CreatCsOne();
                    break;
                case "S3":
                    CreatCsS3();
                    break;
                case "S3p":
                    CreatCsS3p();
                    break;
                case "F3":
                    CreatCsF3();
                    break;
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

            string TargetFolder = ProOutPath;
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
            string TargetFolder = ProOutPath;
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
                    tempstr = sr.ReadToEnd().Replace(".Demo", "." + cb.ModelName ).Replace("<$$AddAspxCs$$>", s);
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

        #region 简单三层(完整方案)

        private void CreatCsS3p()
        {
            string TargetFolder = ProOutPath;
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

        #region 工厂模式三层

        private void CreatCsF3()
        {
            string TargetFolder = ProOutPath;
            FolderCheck(TargetFolder);

            #region Model
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

            #region DAL
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


            #region DALFactory
            string factoryFolder = TargetFolder + "\\DALFactory";
            FolderCheck(factoryFolder);
            string filedalfac = factoryFolder + "\\DataAccess.cs";
            string strdalfac = cb.GetCodeFrameF3DALFactory();
            //已经存在，并且有内容，则追加
            if (File.Exists(filedalfac))
            {
                string temp = File.ReadAllText(filedalfac, Encoding.Default);
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

            #region IDAL
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

            #region BLL
            string bllFolder = TargetFolder + "\\BLL";
            if (cb.Folder != "")
            {
                bllFolder = bllFolder + "\\" + cb.Folder;
            }
            FolderCheck(bllFolder);
            string filebll = bllFolder + "\\" + cb.BLLName + ".cs";
            string blltype = GetBLLType();
            string strbll = cb.GetCodeFrameF3BLL(blltype, true, true, true, true, true, true, true, true);
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
        private void ReplaceNamespace(string filename,string spacename)
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


        private void buttonXP1_Click(object sender, EventArgs e)
        {
            this.Hide();
            np.Show();

        }
    }
}
