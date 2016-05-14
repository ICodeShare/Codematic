using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
namespace Codematic
{
	/// <summary>
	/// ProjectExp 的摘要说明。
	/// </summary>
	public class ProjectExp : System.Windows.Forms.Form
	{
		#region
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private WiB.Pinkie.Controls.ButtonXP btn_ProFold;
		private WiB.Pinkie.Controls.ButtonXP btn_TargetFold;
		private System.Windows.Forms.StatusBar statusBar1;
		private System.Windows.Forms.StatusBarPanel statusBarPanel1;
		private WiB.Pinkie.Controls.ButtonXP btnExp;
		private WiB.Pinkie.Controls.ButtonXP btn_Set;
		private System.ComponentModel.IContainer components;		
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ProgressBar progressBar1;
		private WiB.Pinkie.Controls.ButtonXP btn_Cancle;
		#endregion

		ArrayList filearrlist1=new ArrayList(); //筛选的文件列表
		ArrayList filearrlist2=new ArrayList(); //过滤得文件列表
		ArrayList folderarrlist=new ArrayList(); //选中的文件夹
		public static Maticsoft.CmConfig.ProSettings setting=new Maticsoft.CmConfig.ProSettings();		

		public ProjectExp()
		{			
			InitializeComponent();			
		}

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

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ProjectExp));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.btn_ProFold = new WiB.Pinkie.Controls.ButtonXP();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.btn_TargetFold = new WiB.Pinkie.Controls.ButtonXP();
			this.btnExp = new WiB.Pinkie.Controls.ButtonXP();
			this.statusBar1 = new System.Windows.Forms.StatusBar();
			this.statusBarPanel1 = new System.Windows.Forms.StatusBarPanel();
			this.btn_Set = new WiB.Pinkie.Controls.ButtonXP();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.listView1 = new System.Windows.Forms.ListView();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.btn_Cancle = new WiB.Pinkie.Controls.ButtonXP();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textBox2);
			this.groupBox1.Controls.Add(this.btn_ProFold);
			this.groupBox1.Controls.Add(this.textBox1);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.btn_TargetFold);
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(440, 88);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "路径";
			// 
			// textBox2
			// 
			this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox2.Location = new System.Drawing.Point(80, 52);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(288, 21);
			this.textBox2.TabIndex = 1;
			this.textBox2.Text = "";
			// 
			// btn_ProFold
			// 
			this.btn_ProFold._Image = null;
			this.btn_ProFold.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.btn_ProFold.DefaultScheme = false;
			this.btn_ProFold.DialogResult = System.Windows.Forms.DialogResult.None;
			this.btn_ProFold.Image = null;
			this.btn_ProFold.Location = new System.Drawing.Point(368, 21);
			this.btn_ProFold.Name = "btn_ProFold";
			this.btn_ProFold.Scheme = WiB.Pinkie.Controls.ButtonXP.Schemes.Blue;
			this.btn_ProFold.Size = new System.Drawing.Size(57, 23);
			this.btn_ProFold.TabIndex = 43;
			this.btn_ProFold.Text = "选择...";
			this.btn_ProFold.Click += new System.EventHandler(this.btn_ProFold_Click);
			// 
			// textBox1
			// 
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox1.Location = new System.Drawing.Point(80, 22);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(288, 21);
			this.textBox1.TabIndex = 1;
			this.textBox1.Text = "";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(16, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(66, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "选择项目：";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(16, 54);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(66, 17);
			this.label2.TabIndex = 0;
			this.label2.Text = "输出目录：";
			// 
			// btn_TargetFold
			// 
			this.btn_TargetFold._Image = null;
			this.btn_TargetFold.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.btn_TargetFold.DefaultScheme = false;
			this.btn_TargetFold.DialogResult = System.Windows.Forms.DialogResult.None;
			this.btn_TargetFold.Image = null;
			this.btn_TargetFold.Location = new System.Drawing.Point(368, 51);
			this.btn_TargetFold.Name = "btn_TargetFold";
			this.btn_TargetFold.Scheme = WiB.Pinkie.Controls.ButtonXP.Schemes.Blue;
			this.btn_TargetFold.Size = new System.Drawing.Size(57, 23);
			this.btn_TargetFold.TabIndex = 43;
			this.btn_TargetFold.Text = "选择...";
			this.btn_TargetFold.Click += new System.EventHandler(this.btn_TargetFold_Click);
			// 
			// btnExp
			// 
			this.btnExp._Image = null;
			this.btnExp.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.btnExp.DefaultScheme = false;
			this.btnExp.DialogResult = System.Windows.Forms.DialogResult.None;
			this.btnExp.Image = null;
			this.btnExp.Location = new System.Drawing.Point(192, 278);
			this.btnExp.Name = "btnExp";
			this.btnExp.Scheme = WiB.Pinkie.Controls.ButtonXP.Schemes.Blue;
			this.btnExp.Size = new System.Drawing.Size(75, 26);
			this.btnExp.TabIndex = 42;
			this.btnExp.Text = "导出项目";
			this.btnExp.Click += new System.EventHandler(this.btnExp_Click);
			// 
			// statusBar1
			// 
			this.statusBar1.Location = new System.Drawing.Point(0, 319);
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																						  this.statusBarPanel1});
			this.statusBar1.ShowPanels = true;
			this.statusBar1.Size = new System.Drawing.Size(456, 22);
			this.statusBar1.TabIndex = 43;
			this.statusBar1.Text = "statusBar1";
			// 
			// statusBarPanel1
			// 
			this.statusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.statusBarPanel1.Text = "就绪";
			this.statusBarPanel1.Width = 440;
			// 
			// btn_Set
			// 
			this.btn_Set._Image = null;
			this.btn_Set.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.btn_Set.DefaultScheme = false;
			this.btn_Set.DialogResult = System.Windows.Forms.DialogResult.None;
			this.btn_Set.Image = null;
			this.btn_Set.Location = new System.Drawing.Point(72, 278);
			this.btn_Set.Name = "btn_Set";
			this.btn_Set.Scheme = WiB.Pinkie.Controls.ButtonXP.Schemes.Blue;
			this.btn_Set.Size = new System.Drawing.Size(75, 26);
			this.btn_Set.TabIndex = 42;
			this.btn_Set.Text = "设  置";
			this.btn_Set.Click += new System.EventHandler(this.btn_Set_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.listView1);
			this.groupBox2.Location = new System.Drawing.Point(8, 104);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(440, 160);
			this.groupBox2.TabIndex = 44;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "目录信息";
			// 
			// listView1
			// 
			this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listView1.Location = new System.Drawing.Point(3, 17);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(434, 140);
			this.listView1.SmallImageList = this.imageList1;
			this.listView1.TabIndex = 0;
			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// progressBar1
			// 
			this.progressBar1.Location = new System.Drawing.Point(0, 311);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(440, 10);
			this.progressBar1.TabIndex = 45;
			// 
			// btn_Cancle
			// 
			this.btn_Cancle._Image = null;
			this.btn_Cancle.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.btn_Cancle.DefaultScheme = false;
			this.btn_Cancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btn_Cancle.Image = null;
			this.btn_Cancle.Location = new System.Drawing.Point(312, 278);
			this.btn_Cancle.Name = "btn_Cancle";
			this.btn_Cancle.Scheme = WiB.Pinkie.Controls.ButtonXP.Schemes.Blue;
			this.btn_Cancle.Size = new System.Drawing.Size(75, 26);
			this.btn_Cancle.TabIndex = 46;
			this.btn_Cancle.Text = "取  消";
			this.btn_Cancle.Click += new System.EventHandler(this.btn_Cancle_Click);
			// 
			// ProjectExp
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(456, 341);
			this.Controls.Add(this.btn_Cancle);
			this.Controls.Add(this.progressBar1);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.statusBar1);
			this.Controls.Add(this.btnExp);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btn_Set);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ProjectExp";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "WEB项目发布";
			this.Load += new System.EventHandler(this.ProjectExp_Load);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void ProjectExp_Load(object sender, System.EventArgs e)
		{			
			LoadData();	
			CreatListView();
			if(this.textBox1.Text.Trim()!="")
			{
				this.listView1.Items.Clear();
				AddListItem(textBox1.Text.Trim());
			}
		}

		private void LoadData()
		{			
			setting=Maticsoft.CmConfig.ProConfig.GetSettings();
			if(setting==null)
				return;
			string [] filelist1= setting.FileExt.Split(new Char [] { ';'});
			foreach (string str in filelist1) 
			{
				if(str.Trim()!="")
				{					
					filearrlist1.Add(str);
				}
			}
			string [] filelist2= setting.FileExtDel.Split(new Char [] { ';'});
			foreach (string str in filelist2) 
			{
				if(str.Trim()!="")
				{					
					filearrlist2.Add(str);
				}
			}	
				
			this.textBox1.Text=setting.SourceDirectory;		
			this.textBox2.Text=setting.TargetDirectory;
			
		}

		private void CreatListView()
		{
			//创建列表
			this.listView1.Columns.Clear();
			this.listView1.Items.Clear();
					
			this.listView1.View = View.Details;
			this.listView1.GridLines = true;
			this.listView1.FullRowSelect=true;
			this.listView1.CheckBoxes=true;

			listView1.Columns.Add("导出",50, HorizontalAlignment.Left);
			listView1.Columns.Add("文件夹名称", 200, HorizontalAlignment.Left);
		}
		private void AddListItem(string SourceDirectory)
		{
			DirectoryInfo   source = new DirectoryInfo(SourceDirectory);
			if(!source.Exists)
				return;

			DirectoryInfo[] sourceDirectories = source.GetDirectories();   
			for(int j = 0; j < sourceDirectories.Length; ++j)
			{				
				ListViewItem item1 = new ListViewItem("",0);
				string str=sourceDirectories[j].Name;
				item1.SubItems.Add(str);
				item1.Checked=true;
				listView1.Items.AddRange(new ListViewItem[]{item1});
			}
		}
		private void btn_ProFold_Click(object sender, System.EventArgs e)
		{
			FolderBrowserDialog folder=new FolderBrowserDialog();
			DialogResult result=folder.ShowDialog(this);
			if(result==DialogResult.OK)
			{
				this.textBox1.Text=folder.SelectedPath;
				this.listView1.Items.Clear();
				AddListItem(textBox1.Text.Trim());
			}	
			
		}
		private void btn_TargetFold_Click(object sender, System.EventArgs e)
		{
			FolderBrowserDialog folder=new FolderBrowserDialog();
			DialogResult result=folder.ShowDialog(this);
			if(result==DialogResult.OK)
			{
				this.textBox2.Text=folder.SelectedPath;
			}		
		}
		

		//拷贝文件夹：
		public void CopyDirectory(string SourceDirectory, string TargetDirectory)
		{
			DirectoryInfo   source = new DirectoryInfo(SourceDirectory);
			DirectoryInfo   target = new DirectoryInfo(TargetDirectory);
         
			//Check If we have valid source
			if(!source.Exists)
				return;

			if(!target.Exists)
				target.Create();         

			//Copy Files
			FileInfo[] sourceFiles = source.GetFiles(); 
			int filescount=sourceFiles.Length;
			this.progressBar1.Maximum=filescount;			
			for(int i = 0; i < filescount; ++i)
			{
				this.progressBar1.Value=i;
				if(setting.Mode=="Filter")
				{
					if(IsHasitem(sourceFiles[i].Extension,filearrlist1))//筛选法
					{
						File.Copy(sourceFiles[i].FullName, target.FullName + "\\" + sourceFiles[i].Name,true);
					}
				}
				else
				{
					if(!IsHasitem(sourceFiles[i].Extension,filearrlist2))//排除法
					{
						File.Copy(sourceFiles[i].FullName, target.FullName + "\\" + sourceFiles[i].Name,true);
					}
				}
				this.statusBarPanel1.Text=sourceFiles[i].FullName;
			}

			//Copy directories
			DirectoryInfo[] sourceDirectories = source.GetDirectories();   
			for(int j = 0; j < sourceDirectories.Length; ++j)
			{
				if((sourceDirectories[j].Parent.FullName==textBox1.Text.Trim())&&(!IsHasitem(sourceDirectories[j].FullName,folderarrlist)))//是否是选中的文件夹
				{
					continue;
				}
				CopyDirectory(sourceDirectories[j].FullName,target.FullName +"\\" + sourceDirectories[j].Name);
				
			}
		}

		/// <summary>
		/// 判断是否存在
		/// </summary>
		/// <param name="str"></param>
		/// <param name="arrlist"></param>
		/// <returns></returns>
		private bool IsHasitem(string str,ArrayList arrlist)
		{
			for(int n=0;n<arrlist.Count;n++)
			{
				if(arrlist[n].ToString()==str)
				{
					return true;
				}
			}
			return false;
		}

		
		/// <summary>
		/// 得到所有选中的文件夹
		/// </summary>
		private void GetSelFolder()
		{			
			string folder1=this.textBox1.Text.Trim();
			foreach(ListViewItem item in this.listView1.Items)
			{
				if(item.Checked)
				{
					string str=folder1+"\\"+item.SubItems[1].Text;
//					string str=item.SubItems[1].Text;
					folderarrlist.Add(str);
				}				
			}
		}

		/// <summary>
		/// 导出项目
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnExp_Click(object sender, System.EventArgs e)
		{
            string folder1 = this.textBox1.Text.Trim();
			string folder2=this.textBox2.Text.Trim();
			if(folder1=="")
			{
				MessageBox.Show("请选择项目！","完成",MessageBoxButtons.OK,MessageBoxIcon.Information);
				return;
			}
			if(folder2=="")
			{
				MessageBox.Show("请选择输出目录！","完成",MessageBoxButtons.OK,MessageBoxIcon.Information);
				return;
			}

			#region 目录检查
			try
			{
				DirectoryInfo   source = new DirectoryInfo(folder1);
				DirectoryInfo   target = new DirectoryInfo(folder2);		
				if(!source.Exists)
				{
					MessageBox.Show("源目录不存在！","完成",MessageBoxButtons.OK,MessageBoxIcon.Information);
					return;
				}
				if(!target.Exists)
				{
					try
					{
						target.Create();  
					}
					catch
					{
						MessageBox.Show("目标目录不存在！","完成",MessageBoxButtons.OK,MessageBoxIcon.Information);
						return;
					}
				}
			}
			catch
			{
				MessageBox.Show("目录信息有误！","完成",MessageBoxButtons.OK,MessageBoxIcon.Information);
				return;
			}
			#endregion

			setting.SourceDirectory=folder1;
			setting.TargetDirectory=folder2;
			Maticsoft.CmConfig.ProConfig.SaveSettings(setting);
			if((folder1!="")&&(folder2!=""))
			{
				GetSelFolder();
				CopyDirectory(folder1,folder2);
			}
			this.progressBar1.Value=0;
			this.statusBarPanel1.Text="就绪";
			MessageBox.Show("项目成功导出！","完成",MessageBoxButtons.OK,MessageBoxIcon.Information);
		}
	

		private void btn_Set_Click(object sender, System.EventArgs e)
		{
			ProjectSet frmset=new ProjectSet();
			DialogResult result=frmset.ShowDialog(this);
			if(result==DialogResult.OK)
			{
				LoadData();	
			}
		}

		private void btn_Cancle_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
		
	}
}
