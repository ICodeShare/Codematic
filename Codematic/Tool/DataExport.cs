using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.IO;
using System.Text;
namespace Codematic
{
	/// <summary>
	/// DbToWord 的摘要说明。
	/// </summary>
	public class DataExport : System.Windows.Forms.Form
	{
		#region
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label lblServer;
		private System.Windows.Forms.ComboBox cmbDB;
		private System.Windows.Forms.ListBox listTable1;
		private System.Windows.Forms.ListBox listTable2;
		private WiB.Pinkie.Controls.ButtonXP btn_Creat;
		private WiB.Pinkie.Controls.ButtonXP btn_Cancle;
		private System.Windows.Forms.Button btn_Addlist;
		private System.Windows.Forms.Button btn_Add;
		private System.Windows.Forms.Button btn_Del;
		private System.Windows.Forms.Button btn_Dellist;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label labelNum;
		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.TextBox txtTargetFolder;
		private WiB.Pinkie.Controls.ButtonXP btn_TargetFold;
		private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBar2;
		#endregion

        Maticsoft.CodeBuild.CodeBuilders cb;//代码生成对象
        Maticsoft.IDBO.IDbScriptBuilder dsb;
        Maticsoft.IDBO.IDbObject dbobj;
        Maticsoft.CmConfig.DbSettings dbset;//服务器配置
		string DbName="master";		
		Thread mythread;

        public DataExport(string longservername, string dbname)
		{			
			InitializeComponent();			
			DbName=dbname;
            dbset = Maticsoft.CmConfig.DbConfig.GetSetting(longservername);
            dbobj = Maticsoft.DBFactory.DBOMaker.CreateDbObj(dbset.DbType);
            dbobj.DbConnectStr = dbset.ConnectStr;  
            cb = new Maticsoft.CodeBuild.CodeBuilders(dbobj);
            dsb = Maticsoft.DBFactory.DBOMaker.CreateScript(dbset.DbType);// ObjHelper.CreatDsb(longservername);

            this.lblServer.Text = dbset.Server;            
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(DbToScript));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.cmbDB = new System.Windows.Forms.ComboBox();
			this.lblServer = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.labelNum = new System.Windows.Forms.Label();
			this.btn_Addlist = new System.Windows.Forms.Button();
			this.btn_Add = new System.Windows.Forms.Button();
			this.btn_Del = new System.Windows.Forms.Button();
			this.btn_Dellist = new System.Windows.Forms.Button();
			this.listTable2 = new System.Windows.Forms.ListBox();
			this.listTable1 = new System.Windows.Forms.ListBox();
			this.btn_Creat = new WiB.Pinkie.Controls.ButtonXP();
			this.btn_Cancle = new WiB.Pinkie.Controls.ButtonXP();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.txtTargetFolder = new System.Windows.Forms.TextBox();
			this.btn_TargetFold = new WiB.Pinkie.Controls.ButtonXP();
			this.label2 = new System.Windows.Forms.Label();
			this.progressBar2 = new System.Windows.Forms.ProgressBar();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.cmbDB);
			this.groupBox1.Controls.Add(this.lblServer);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(464, 56);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "选择数据库";
			// 
			// cmbDB
			// 
			this.cmbDB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbDB.Location = new System.Drawing.Point(296, 24);
			this.cmbDB.Name = "cmbDB";
			this.cmbDB.Size = new System.Drawing.Size(152, 20);
			this.cmbDB.TabIndex = 2;
			this.cmbDB.SelectedIndexChanged += new System.EventHandler(this.cmbDB_SelectedIndexChanged);
			// 
			// lblServer
			// 
			this.lblServer.AutoSize = true;
			this.lblServer.Location = new System.Drawing.Point(104, 26);
			this.lblServer.Name = "lblServer";
			this.lblServer.Size = new System.Drawing.Size(42, 17);
			this.lblServer.TabIndex = 1;
			this.lblServer.Text = "label2";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 26);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(79, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "当前服务器：";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(216, 26);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(79, 17);
			this.label3.TabIndex = 0;
			this.label3.Text = "选择数据库：";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.progressBar2);
			this.groupBox2.Controls.Add(this.progressBar1);
			this.groupBox2.Controls.Add(this.labelNum);
			this.groupBox2.Controls.Add(this.btn_Addlist);
			this.groupBox2.Controls.Add(this.btn_Add);
			this.groupBox2.Controls.Add(this.btn_Del);
			this.groupBox2.Controls.Add(this.btn_Dellist);
			this.groupBox2.Controls.Add(this.listTable2);
			this.groupBox2.Controls.Add(this.listTable1);
			this.groupBox2.Location = new System.Drawing.Point(8, 112);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(464, 224);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "选择表";
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(48, 192);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(392, 20);
			this.progressBar1.TabIndex = 10;
			// 
			// labelNum
			// 
			this.labelNum.Location = new System.Drawing.Point(16, 191);
			this.labelNum.Name = "labelNum";
			this.labelNum.Size = new System.Drawing.Size(35, 22);
			this.labelNum.TabIndex = 9;
			this.labelNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btn_Addlist
			// 
			this.btn_Addlist.Enabled = false;
			this.btn_Addlist.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btn_Addlist.Location = new System.Drawing.Point(208, 32);
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
			this.btn_Add.Location = new System.Drawing.Point(208, 64);
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
			this.btn_Del.Location = new System.Drawing.Point(208, 96);
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
			this.btn_Dellist.Location = new System.Drawing.Point(208, 128);
			this.btn_Dellist.Name = "btn_Dellist";
			this.btn_Dellist.Size = new System.Drawing.Size(40, 23);
			this.btn_Dellist.TabIndex = 6;
			this.btn_Dellist.Text = "<<";
			this.btn_Dellist.Click += new System.EventHandler(this.btn_Dellist_Click);
			// 
			// listTable2
			// 
			this.listTable2.ItemHeight = 12;
			this.listTable2.Location = new System.Drawing.Point(288, 24);
			this.listTable2.Name = "listTable2";
			this.listTable2.Size = new System.Drawing.Size(152, 148);
			this.listTable2.TabIndex = 1;
			this.listTable2.DoubleClick += new System.EventHandler(this.listTable2_DoubleClick);
			// 
			// listTable1
			// 
			this.listTable1.ItemHeight = 12;
			this.listTable1.Location = new System.Drawing.Point(16, 24);
			this.listTable1.Name = "listTable1";
			this.listTable1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.listTable1.Size = new System.Drawing.Size(152, 148);
			this.listTable1.TabIndex = 0;
			this.listTable1.DoubleClick += new System.EventHandler(this.listTable1_DoubleClick);
			// 
			// btn_Creat
			// 
			this.btn_Creat._Image = null;
			this.btn_Creat.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(236)), ((System.Byte)(233)), ((System.Byte)(216)));
			this.btn_Creat.DefaultScheme = false;
			this.btn_Creat.DialogResult = System.Windows.Forms.DialogResult.None;
			this.btn_Creat.Image = null;
			this.btn_Creat.Location = new System.Drawing.Point(240, 344);
			this.btn_Creat.Name = "btn_Creat";
			this.btn_Creat.Scheme = WiB.Pinkie.Controls.ButtonXP.Schemes.Blue;
			this.btn_Creat.Size = new System.Drawing.Size(75, 26);
			this.btn_Creat.TabIndex = 42;
			this.btn_Creat.Text = "生  成";
			this.btn_Creat.Click += new System.EventHandler(this.btn_Creat_Click);
			// 
			// btn_Cancle
			// 
			this.btn_Cancle._Image = null;
			this.btn_Cancle.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(236)), ((System.Byte)(233)), ((System.Byte)(216)));
			this.btn_Cancle.DefaultScheme = false;
			this.btn_Cancle.DialogResult = System.Windows.Forms.DialogResult.None;
			this.btn_Cancle.Image = null;
			this.btn_Cancle.Location = new System.Drawing.Point(344, 344);
			this.btn_Cancle.Name = "btn_Cancle";
			this.btn_Cancle.Scheme = WiB.Pinkie.Controls.ButtonXP.Schemes.Blue;
			this.btn_Cancle.Size = new System.Drawing.Size(75, 26);
			this.btn_Cancle.TabIndex = 42;
			this.btn_Cancle.Text = "取  消";
			this.btn_Cancle.Click += new System.EventHandler(this.btn_Cancle_Click);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.txtTargetFolder);
			this.groupBox3.Controls.Add(this.btn_TargetFold);
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.Location = new System.Drawing.Point(8, 64);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(464, 48);
			this.groupBox3.TabIndex = 43;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "保存";
			// 
			// txtTargetFolder
			// 
			this.txtTargetFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtTargetFolder.Location = new System.Drawing.Point(72, 16);
			this.txtTargetFolder.Name = "txtTargetFolder";
			this.txtTargetFolder.Size = new System.Drawing.Size(288, 21);
			this.txtTargetFolder.TabIndex = 47;
			this.txtTargetFolder.Text = "";
			// 
			// btn_TargetFold
			// 
			this.btn_TargetFold._Image = null;
			this.btn_TargetFold.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(236)), ((System.Byte)(233)), ((System.Byte)(216)));
			this.btn_TargetFold.DefaultScheme = false;
			this.btn_TargetFold.DialogResult = System.Windows.Forms.DialogResult.None;
			this.btn_TargetFold.Image = null;
			this.btn_TargetFold.Location = new System.Drawing.Point(376, 15);
			this.btn_TargetFold.Name = "btn_TargetFold";
			this.btn_TargetFold.Scheme = WiB.Pinkie.Controls.ButtonXP.Schemes.Blue;
			this.btn_TargetFold.Size = new System.Drawing.Size(57, 23);
			this.btn_TargetFold.TabIndex = 48;
			this.btn_TargetFold.Text = "选择...";
			this.btn_TargetFold.Click += new System.EventHandler(this.btn_TargetFold_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(16, 18);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(54, 17);
			this.label2.TabIndex = 49;
			this.label2.Text = "文件名：";
			// 
			// progressBar2
			// 
			this.progressBar2.Location = new System.Drawing.Point(48, 178);
			this.progressBar2.Name = "progressBar2";
			this.progressBar2.Size = new System.Drawing.Size(392, 10);
			this.progressBar2.TabIndex = 11;
			// 
			// DbToScript
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(480, 382);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.btn_Creat);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btn_Cancle);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DbToScript";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "生成SQL脚本";
			this.Load += new System.EventHandler(this.DbToWord_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void DbToWord_Load(object sender, System.EventArgs e)
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
                this.cmbDB.Items.Add(DbName);
            }
			this.btn_Creat.Enabled=false;	
			this.cmbDB.Text=DbName;
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

		#region 按钮操作

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
		#endregion

		#region listbox操作
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
				this.btn_Creat.Enabled=true;
			}
			else
			{
				this.btn_Del.Enabled=false;
				this.btn_Dellist.Enabled=false;	
				this.btn_Creat.Enabled=false;
			}
		}
		#endregion

		private void btn_Cancle_Click(object sender, System.EventArgs e)
		{		
			
			if((mythread!=null)&&(mythread.IsAlive))
			{
				mythread.Abort();
			}
			this.Close();
		}
		private void btn_Creat_Click(object sender, System.EventArgs e)
		{		
			try
			{
				if(this.txtTargetFolder.Text=="")
				{
					MessageBox.Show("请选择保存文件路径！","完成",MessageBoxButtons.OK,MessageBoxIcon.Information);
					return;
				}
				mythread = new Thread(new ThreadStart(ThreadWork));
				mythread.Start();
			}
			catch(System.Exception ex)
			{
                LogInfo.WriteLog(ex);
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
			}
		}
		void ThreadWork()
		{			
			try
			{
				this.Text="正在生成脚本...";
				this.Cursor=System.Windows.Forms.Cursors.WaitCursor;
				this.btn_Creat.Enabled=false;
				this.btn_Cancle.Enabled=false;
				string dbname=this.cmbDB.Text;
				string strtitle="数据库名："+dbname;
				int tblCount=this.listTable2.Items.Count;

				string filename=this.txtTargetFolder.Text;
				this.progressBar1.Maximum=tblCount;
				this.labelNum.Text="0";		

				#region 循环每个表

				for(int i=0;i<tblCount;i++)
				{
					this.listTable2.SelectedIndex=i;
					string tablename=this.listTable2.Items[i].ToString();
					dsb.CreateTabScript(dbname,tablename,filename,progressBar2);	
				
					this.progressBar1.Value=i+1;
					this.labelNum.Text=(i+1).ToString();	
				}
				#endregion

				this.btn_Creat.Enabled=true;
				this.btn_Cancle.Enabled=true;
				this.Text="脚本全部生成成功！";
				this.Cursor=System.Windows.Forms.Cursors.Default;
				MessageBox.Show("脚本全部生成成功！","完成",MessageBoxButtons.OK,MessageBoxIcon.Information);
			}
			catch(System.Exception ex)
			{
                LogInfo.WriteLog(ex);
				this.btn_Creat.Enabled=true;
				this.btn_Cancle.Enabled=true;
				this.Text="脚本生成失败！";
				this.Cursor=System.Windows.Forms.Cursors.Default;
				MessageBox.Show("脚本生成失败！请检查表名是否规范或其他问题导致。("+ex.Message+")","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
			}
		}

		private void btn_TargetFold_Click(object sender, System.EventArgs e)
		{
			SaveFileDialog sqlsavedlg=new SaveFileDialog();			
			sqlsavedlg.Title="保存当前脚本";
			sqlsavedlg.Filter="sql files (*.sql)|*.sql|All files (*.*)|*.*";
			DialogResult result=sqlsavedlg.ShowDialog(this);
			if(result==DialogResult.OK)
			{
				this.txtTargetFolder.Text=sqlsavedlg.FileName;
			}	
		}


		
	}
}
