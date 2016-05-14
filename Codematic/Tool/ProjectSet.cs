using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Codematic
{
	/// <summary>
	/// ProjectSet 的摘要说明。
	/// </summary>
	public class ProjectSet : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupbox2;
		private WiB.Pinkie.Controls.ButtonXP btn_Add1;
		private System.Windows.Forms.ListBox listBox1;
		private System.Windows.Forms.RadioButton radioButton1;
		private System.Windows.Forms.RadioButton radioButton2;
		private System.Windows.Forms.ListBox listBox2;
		private WiB.Pinkie.Controls.ButtonXP btn_Del1;
		private WiB.Pinkie.Controls.ButtonXP btn_Add2;
		private WiB.Pinkie.Controls.ButtonXP btn_Del2;
		private WiB.Pinkie.Controls.ButtonXP btn_OK;
		private WiB.Pinkie.Controls.ButtonXP btn_Cancel;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;		
		public static Maticsoft.CmConfig.ProSettings setting=new Maticsoft.CmConfig.ProSettings();	
		public ProjectSet()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
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
			this.groupbox2 = new System.Windows.Forms.GroupBox();
			this.btn_Add1 = new WiB.Pinkie.Controls.ButtonXP();
			this.listBox1 = new System.Windows.Forms.ListBox();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.listBox2 = new System.Windows.Forms.ListBox();
			this.btn_Del1 = new WiB.Pinkie.Controls.ButtonXP();
			this.btn_Add2 = new WiB.Pinkie.Controls.ButtonXP();
			this.btn_Del2 = new WiB.Pinkie.Controls.ButtonXP();
			this.btn_OK = new WiB.Pinkie.Controls.ButtonXP();
			this.btn_Cancel = new WiB.Pinkie.Controls.ButtonXP();
			this.groupbox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupbox2
			// 
			this.groupbox2.Controls.Add(this.btn_Add1);
			this.groupbox2.Controls.Add(this.listBox1);
			this.groupbox2.Controls.Add(this.radioButton1);
			this.groupbox2.Controls.Add(this.radioButton2);
			this.groupbox2.Controls.Add(this.listBox2);
			this.groupbox2.Controls.Add(this.btn_Del1);
			this.groupbox2.Controls.Add(this.btn_Add2);
			this.groupbox2.Controls.Add(this.btn_Del2);
			this.groupbox2.Location = new System.Drawing.Point(8, 8);
			this.groupbox2.Name = "groupbox2";
			this.groupbox2.Size = new System.Drawing.Size(448, 136);
			this.groupbox2.TabIndex = 2;
			this.groupbox2.TabStop = false;
			this.groupbox2.Text = "文件规则";
			// 
			// btn_Add1
			// 
			this.btn_Add1._Image = null;
			this.btn_Add1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.btn_Add1.DefaultScheme = false;
			this.btn_Add1.DialogResult = System.Windows.Forms.DialogResult.None;
			this.btn_Add1.Image = null;
			this.btn_Add1.Location = new System.Drawing.Point(160, 40);
			this.btn_Add1.Name = "btn_Add1";
			this.btn_Add1.Scheme = WiB.Pinkie.Controls.ButtonXP.Schemes.Blue;
			this.btn_Add1.Size = new System.Drawing.Size(57, 23);
			this.btn_Add1.TabIndex = 44;
			this.btn_Add1.Text = "增加...";
			this.btn_Add1.Click += new System.EventHandler(this.btn_Add1_Click);
			// 
			// listBox1
			// 
			this.listBox1.ItemHeight = 12;
			this.listBox1.Location = new System.Drawing.Point(80, 16);
			this.listBox1.Name = "listBox1";
			this.listBox1.Size = new System.Drawing.Size(72, 112);
			this.listBox1.TabIndex = 1;
			// 
			// radioButton1
			// 
			this.radioButton1.Location = new System.Drawing.Point(16, 24);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(64, 24);
			this.radioButton1.TabIndex = 0;
			this.radioButton1.Text = "筛选法";
			this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
			// 
			// radioButton2
			// 
			this.radioButton2.Checked = true;
			this.radioButton2.Location = new System.Drawing.Point(240, 24);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(64, 24);
			this.radioButton2.TabIndex = 0;
			this.radioButton2.TabStop = true;
			this.radioButton2.Text = "排除法";
			this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
			// 
			// listBox2
			// 
			this.listBox2.ItemHeight = 12;
			this.listBox2.Location = new System.Drawing.Point(304, 16);
			this.listBox2.Name = "listBox2";
			this.listBox2.Size = new System.Drawing.Size(72, 112);
			this.listBox2.TabIndex = 1;
			// 
			// btn_Del1
			// 
			this.btn_Del1._Image = null;
			this.btn_Del1.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.btn_Del1.DefaultScheme = false;
			this.btn_Del1.DialogResult = System.Windows.Forms.DialogResult.None;
			this.btn_Del1.Image = null;
			this.btn_Del1.Location = new System.Drawing.Point(160, 72);
			this.btn_Del1.Name = "btn_Del1";
			this.btn_Del1.Scheme = WiB.Pinkie.Controls.ButtonXP.Schemes.Blue;
			this.btn_Del1.Size = new System.Drawing.Size(57, 23);
			this.btn_Del1.TabIndex = 44;
			this.btn_Del1.Text = "移 除";
			this.btn_Del1.Click += new System.EventHandler(this.btn_Del1_Click);
			// 
			// btn_Add2
			// 
			this.btn_Add2._Image = null;
			this.btn_Add2.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.btn_Add2.DefaultScheme = false;
			this.btn_Add2.DialogResult = System.Windows.Forms.DialogResult.None;
			this.btn_Add2.Image = null;
			this.btn_Add2.Location = new System.Drawing.Point(384, 40);
			this.btn_Add2.Name = "btn_Add2";
			this.btn_Add2.Scheme = WiB.Pinkie.Controls.ButtonXP.Schemes.Blue;
			this.btn_Add2.Size = new System.Drawing.Size(57, 23);
			this.btn_Add2.TabIndex = 44;
			this.btn_Add2.Text = "增加...";
			this.btn_Add2.Click += new System.EventHandler(this.btn_Add2_Click);
			// 
			// btn_Del2
			// 
			this.btn_Del2._Image = null;
			this.btn_Del2.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.btn_Del2.DefaultScheme = false;
			this.btn_Del2.DialogResult = System.Windows.Forms.DialogResult.None;
			this.btn_Del2.Image = null;
			this.btn_Del2.Location = new System.Drawing.Point(384, 72);
			this.btn_Del2.Name = "btn_Del2";
			this.btn_Del2.Scheme = WiB.Pinkie.Controls.ButtonXP.Schemes.Blue;
			this.btn_Del2.Size = new System.Drawing.Size(57, 23);
			this.btn_Del2.TabIndex = 44;
			this.btn_Del2.Text = "移 除";
			this.btn_Del2.Click += new System.EventHandler(this.btn_Del2_Click);
			// 
			// btn_OK
			// 
			this.btn_OK._Image = null;
			this.btn_OK.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.btn_OK.DefaultScheme = false;
			this.btn_OK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btn_OK.Image = null;
			this.btn_OK.Location = new System.Drawing.Point(232, 160);
			this.btn_OK.Name = "btn_OK";
			this.btn_OK.Scheme = WiB.Pinkie.Controls.ButtonXP.Schemes.Blue;
			this.btn_OK.Size = new System.Drawing.Size(65, 25);
			this.btn_OK.TabIndex = 48;
			this.btn_OK.Text = "确 定";
			this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
			// 
			// btn_Cancel
			// 
			this.btn_Cancel._Image = null;
			this.btn_Cancel.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.btn_Cancel.DefaultScheme = false;
			this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btn_Cancel.Image = null;
			this.btn_Cancel.Location = new System.Drawing.Point(328, 160);
			this.btn_Cancel.Name = "btn_Cancel";
			this.btn_Cancel.Scheme = WiB.Pinkie.Controls.ButtonXP.Schemes.Blue;
			this.btn_Cancel.Size = new System.Drawing.Size(65, 25);
			this.btn_Cancel.TabIndex = 47;
			this.btn_Cancel.Text = "取 消";
			// 
			// ProjectSet
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(466, 199);
			this.Controls.Add(this.btn_OK);
			this.Controls.Add(this.btn_Cancel);
			this.Controls.Add(this.groupbox2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ProjectSet";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "设置规则";
			this.Load += new System.EventHandler(this.ProjectSet_Load);
			this.groupbox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void ProjectSet_Load(object sender, System.EventArgs e)
		{			
			this.listBox1.Enabled=false;
			this.btn_Add1.Enabled=false;
			this.btn_Del1.Enabled=false;
			LoadData();			
		}

		private void LoadData()
		{		
			setting=Maticsoft.CmConfig.ProConfig.GetSettings();

			if(setting.Mode=="Filter")
			{
				this.radioButton1.Checked=true;
			}
			else
			{
				this.radioButton2.Checked=true;
			}

			string [] filelist1= setting.FileExt.Split(new Char [] { ';'});
			foreach (string str in filelist1) 
			{
				if(str.Trim()!="")
				{
					this.listBox1.Items.Add(str);
				}
			}
			string [] filelist2= setting.FileExtDel.Split(new Char [] { ';'});
			foreach (string str in filelist2) 
			{
				if(str.Trim()!="")
				{
					this.listBox2.Items.Add(str);
				}
			}			
		}

		private void btn_Add1_Click(object sender, System.EventArgs e)
		{		
			ProjectExpadd add=new ProjectExpadd();
			DialogResult result=add.ShowDialog(this);
			if(result==DialogResult.OK)
			{
				string strfile=add.textBox1.Text.Trim();
				this.listBox1.Items.Add(strfile);				
			}
		}

		private void btn_Del1_Click(object sender, System.EventArgs e)
		{
			if(listBox1.SelectedItem!=null)
			{
				this.listBox1.Items.Remove(this.listBox1.SelectedItem);
			}		
		}

		private void btn_Add2_Click(object sender, System.EventArgs e)
		{
			ProjectExpadd add=new ProjectExpadd();
			DialogResult result=add.ShowDialog(this);
			if(result==DialogResult.OK)
			{			
				string strfile=add.textBox1.Text.Trim();
				this.listBox2.Items.Add(strfile);				
			}			
		}

		private void btn_Del2_Click(object sender, System.EventArgs e)
		{
			if(listBox2.SelectedItem!=null)
			{				
				this.listBox2.Items.Remove(this.listBox2.SelectedItem);
			}		
		}

		private void SaveSet()
		{
			if(this.radioButton1.Checked)
			{
				setting.Mode="Filter";
			}
			else
			{
				setting.Mode="Del";
			}
			if(this.listBox1.Items.Count>0)
			{
				string filelist="";
				for(int n=0;n<this.listBox1.Items.Count;n++)
				{
					filelist+=listBox1.Items[n].ToString()+";";
				}
				setting.FileExt=filelist.Substring(0,filelist.Length-1);
			}		
			if(this.listBox2.Items.Count>0)
			{
				string filelist2="";
				for(int n=0;n<listBox2.Items.Count;n++)
				{
					filelist2+=listBox2.Items[n].ToString()+";";
				}
				setting.FileExtDel=filelist2.Substring(0,filelist2.Length-1);
			}
			Maticsoft.CmConfig.ProConfig.SaveSettings(setting);
		}

		private void radioButton_CheckedChanged(object sender, System.EventArgs e)
		{
			if(this.radioButton1.Checked)
			{
				this.listBox1.Enabled=true;
				this.btn_Add1.Enabled=true;
				this.btn_Del1.Enabled=true;
				this.listBox2.Enabled=false;
				this.btn_Add2.Enabled=false;
				this.btn_Del2.Enabled=false;
			}
			else
			{
				this.listBox2.Enabled=true;
				this.btn_Add2.Enabled=true;
				this.btn_Del2.Enabled=true;
				this.listBox1.Enabled=false;
				this.btn_Add1.Enabled=false;
				this.btn_Del1.Enabled=false;
			}
		
		}
		private void btn_OK_Click(object sender, System.EventArgs e)
		{
			SaveSet();		
		}

	}
}
