using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using LTP.ConvertCS2VB;
using System.IO;
using System.Text;
using System.Threading;
using LTP.TextEditor;
using LTP.TextEditor.Gui.CompletionWindow;
using LTP.TextEditor.Document;
using LTP.TextEditor.Actions;
namespace Codematic
{
	/// <summary>
	/// ConvertCS2VB 的摘要说明。
	/// </summary>
	public class ConverteCS2VB : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPageCS;
		private System.Windows.Forms.TabPage tabPageVB;
		private LTP.TextEditor.TextEditorControl txtCSharp;
		private LTP.TextEditor.TextEditorControl txtVB;
		private System.Windows.Forms.StatusBar statusBar1;
		private System.Windows.Forms.StatusBarPanel statusBarPanel1;
		private System.Windows.Forms.StatusBarPanel statusBarPanel2;
		private System.Windows.Forms.ImageList imageList1;
		private WiB.Pinkie.Controls.ButtonXP buttonXP3;
		private WiB.Pinkie.Controls.ButtonXP btnLoad;
		private WiB.Pinkie.Controls.ButtonXP btnConvert;
		private WiB.Pinkie.Controls.ButtonXP btnSave;
		private System.ComponentModel.IContainer components;

		public ConverteCS2VB()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ConverteCS2VB));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPageCS = new System.Windows.Forms.TabPage();
			this.txtCSharp = new LTP.TextEditor.TextEditorControl();
			this.tabPageVB = new System.Windows.Forms.TabPage();
			this.txtVB = new LTP.TextEditor.TextEditorControl();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.statusBar1 = new System.Windows.Forms.StatusBar();
			this.statusBarPanel1 = new System.Windows.Forms.StatusBarPanel();
			this.statusBarPanel2 = new System.Windows.Forms.StatusBarPanel();
			this.btnLoad = new WiB.Pinkie.Controls.ButtonXP();
			this.btnConvert = new WiB.Pinkie.Controls.ButtonXP();
			this.btnSave = new WiB.Pinkie.Controls.ButtonXP();
			this.buttonXP3 = new WiB.Pinkie.Controls.ButtonXP();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPageCS.SuspendLayout();
			this.tabPageVB.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel2)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.btnLoad);
			this.groupBox1.Controls.Add(this.btnConvert);
			this.groupBox1.Controls.Add(this.btnSave);
			this.groupBox1.Controls.Add(this.buttonXP3);
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(688, 56);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "操作";
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.tabControl1);
			this.groupBox2.Location = new System.Drawing.Point(8, 72);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(688, 360);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "代码";
			// 
			// tabControl1
			// 
			this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
			this.tabControl1.Controls.Add(this.tabPageCS);
			this.tabControl1.Controls.Add(this.tabPageVB);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.ImageList = this.imageList1;
			this.tabControl1.Location = new System.Drawing.Point(3, 17);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(682, 340);
			this.tabControl1.TabIndex = 0;
			// 
			// tabPageCS
			// 
			this.tabPageCS.Controls.Add(this.txtCSharp);
			this.tabPageCS.ImageIndex = 0;
			this.tabPageCS.Location = new System.Drawing.Point(4, 4);
			this.tabPageCS.Name = "tabPageCS";
			this.tabPageCS.Size = new System.Drawing.Size(674, 311);
			this.tabPageCS.TabIndex = 0;
			this.tabPageCS.Text = "C#代码";
			// 
			// txtCSharp
			// 
			this.txtCSharp.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtCSharp.Location = new System.Drawing.Point(0, 0);
			this.txtCSharp.Name = "txtCSharp";
			this.txtCSharp.Size = new System.Drawing.Size(674, 311);
			this.txtCSharp.TabIndex = 0;
			this.txtCSharp.Text = "";
			this.txtCSharp.IsIconBarVisible = false;
			this.txtCSharp.ShowInvalidLines = false;
			this.txtCSharp.ShowSpaces = false;
			this.txtCSharp.ShowTabs = false;
			this.txtCSharp.ShowEOLMarkers=false;
			this.txtCSharp.ShowVRuler=false;
			this.txtCSharp.Language=Languages.CSHARP;
			this.txtCSharp.Encoding = System.Text.Encoding.Default;
			this.txtCSharp.Font=new Font("新宋体",9);
			

			// 
			// tabPageVB
			// 
			this.tabPageVB.Controls.Add(this.txtVB);
			this.tabPageVB.ImageIndex = 1;
			this.tabPageVB.Location = new System.Drawing.Point(4, 4);
			this.tabPageVB.Name = "tabPageVB";
			this.tabPageVB.Size = new System.Drawing.Size(674, 311);
			this.tabPageVB.TabIndex = 1;
			this.tabPageVB.Text = "VB.NET代码";
			// 
			// txtVB
			// 
			this.txtVB.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtVB.Location = new System.Drawing.Point(0, 0);
			this.txtVB.Name = "txtVB";
			this.txtVB.Size = new System.Drawing.Size(674, 311);
			this.txtVB.TabIndex = 0;
			this.txtVB.Text = "";

			this.txtVB.IsIconBarVisible = false;
			this.txtVB.ShowInvalidLines = false;
			this.txtVB.ShowSpaces = false;
			this.txtVB.ShowTabs = false;
			this.txtVB.ShowEOLMarkers=false;
			this.txtVB.ShowVRuler=false;
			this.txtVB.Language=Languages.VBNET;
			this.txtVB.Encoding = System.Text.Encoding.Default;
			this.txtVB.Font=new Font("新宋体",9);

			// 
			// imageList1
			// 
			this.imageList1.ImageSize = new System.Drawing.Size(18, 18);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// statusBar1
			// 
			this.statusBar1.Location = new System.Drawing.Point(0, 432);
			this.statusBar1.Name = "statusBar1";
			this.statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																						  this.statusBarPanel1,
																						  this.statusBarPanel2});
			this.statusBar1.ShowPanels = true;
			this.statusBar1.Size = new System.Drawing.Size(704, 22);
			this.statusBar1.TabIndex = 2;
			// 
			// statusBarPanel1
			// 
			this.statusBarPanel1.Text = "就绪";
			this.statusBarPanel1.Width = 200;
			// 
			// statusBarPanel2
			// 
			this.statusBarPanel2.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.statusBarPanel2.Width = 488;
			// 
			// btnLoad
			// 
			this.btnLoad._Image = null;
			this.btnLoad.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(236)), ((System.Byte)(233)), ((System.Byte)(216)));
			this.btnLoad.DefaultScheme = false;
			this.btnLoad.DialogResult = System.Windows.Forms.DialogResult.None;
			this.btnLoad.Image = null;
			this.btnLoad.Location = new System.Drawing.Point(120, 16);
			this.btnLoad.Name = "btnLoad";
			this.btnLoad.Scheme = WiB.Pinkie.Controls.ButtonXP.Schemes.Blue;
			this.btnLoad.Size = new System.Drawing.Size(75, 26);
			this.btnLoad.TabIndex = 42;
			this.btnLoad.Text = "加载文件";
			this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
			// 
			// btnConvert
			// 
			this.btnConvert._Image = null;
			this.btnConvert.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(236)), ((System.Byte)(233)), ((System.Byte)(216)));
			this.btnConvert.DefaultScheme = false;
			this.btnConvert.DialogResult = System.Windows.Forms.DialogResult.None;
			this.btnConvert.Image = null;
			this.btnConvert.Location = new System.Drawing.Point(224, 16);
			this.btnConvert.Name = "btnConvert";
			this.btnConvert.Scheme = WiB.Pinkie.Controls.ButtonXP.Schemes.Blue;
			this.btnConvert.Size = new System.Drawing.Size(75, 26);
			this.btnConvert.TabIndex = 42;
			this.btnConvert.Text = "立即转换";
			this.btnConvert.Click += new System.EventHandler(this.btnConvert_Click);
			// 
			// btnSave
			// 
			this.btnSave._Image = null;
			this.btnSave.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(236)), ((System.Byte)(233)), ((System.Byte)(216)));
			this.btnSave.DefaultScheme = false;
			this.btnSave.DialogResult = System.Windows.Forms.DialogResult.None;
			this.btnSave.Image = null;
			this.btnSave.Location = new System.Drawing.Point(328, 16);
			this.btnSave.Name = "btnSave";
			this.btnSave.Scheme = WiB.Pinkie.Controls.ButtonXP.Schemes.Blue;
			this.btnSave.Size = new System.Drawing.Size(75, 26);
			this.btnSave.TabIndex = 42;
			this.btnSave.Text = "保存为...";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// buttonXP3
			// 
			this.buttonXP3._Image = null;
			this.buttonXP3.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(236)), ((System.Byte)(233)), ((System.Byte)(216)));
			this.buttonXP3.DefaultScheme = false;
			this.buttonXP3.DialogResult = System.Windows.Forms.DialogResult.None;
			this.buttonXP3.Image = null;
			this.buttonXP3.Location = new System.Drawing.Point(432, 16);
			this.buttonXP3.Name = "buttonXP3";
			this.buttonXP3.Scheme = WiB.Pinkie.Controls.ButtonXP.Schemes.Blue;
			this.buttonXP3.Size = new System.Drawing.Size(75, 26);
			this.buttonXP3.TabIndex = 42;
			this.buttonXP3.Text = "退  出";
			this.buttonXP3.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// ConvertCS2VB
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(704, 454);
			this.Controls.Add(this.statusBar1);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ConverteCS2VB";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "转换C#代码到VB.NET";
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPageCS.ResumeLayout(false);
			this.tabPageVB.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel2)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="cFileName"></param>
		/// <returns></returns>
		public string FileToStr(string cFileName)
		{			
//			StreamReader oReader = System.IO.File.OpenText(cFileName);				
//			string lcString = oReader.ReadToEnd();						
//			oReader.Close();	
			StreamReader srFile=new StreamReader(cFileName,Encoding.Default);
			string lcString=srFile.ReadToEnd();
			srFile.Close();
			return lcString;
		}
		/// <summary>
		///
		/// </summary>
		/// <returns></returns>
		private string GetFile()
		{	
			string lcFile = "";	
			string lcFilter = "C# Files (*.cs)|*.cs|All files (*.*)|*.*" ;	
			//int lnFilterIndex = 2;	
			//string lcTitle = "Open";

			//Create the OpenFileDialog (note that the FileDialog class is an abstract and cannot be used directly)	
			OpenFileDialog ofd = new OpenFileDialog();		
			ofd.Filter = lcFilter;

			ofd.RestoreDirectory = true;	
			ofd.InitialDirectory = System.IO.Directory.GetCurrentDirectory();				
			if(ofd.ShowDialog() != DialogResult.Cancel)	
			{				
				lcFile = ofd.FileName;	
			}			
			return lcFile;
		}
		private void btnLoad_Click(object sender, System.EventArgs e)
		{
			string cFile = "";
			cFile = this.GetFile();
			if(cFile.Length > 0)
			{
				this.txtCSharp.Text = this.FileToStr(cFile);	
			}
			string path=Application.StartupPath;
			try
			{
				//ass.KeyWordFormat(txtCSharp,"CSHARP",path);
			}
			catch{}
			this.tabControl1.SelectedIndex=0;
		}

		private void btnConvert_Click(object sender, System.EventArgs e)
		{
			this.statusBarPanel1.Text="正在转换，请稍候...";
			CSharpToVBConverter o = new CSharpToVBConverter();
			this.txtVB.Text = o.Execute(this.txtCSharp.Text);

			string path=Application.StartupPath;
			try
			{
				//ass.KeyWordFormat(txtVB,"VB",path);
			}
			catch{}
			//Change the Page in the Tab Control so we can see the changes
			//this.tabControl1.SelectedTab = this.tabControl1.TabPages[1];
			this.tabControl1.SelectedIndex=1;
			this.statusBarPanel1.Text="转换完成。";

//			try
//			{
//				mythread = new Thread(new ThreadStart(ThreadWork));
//				mythread.Start();
//			}
//			catch(System.Exception er)
//			{
//				MessageBox.Show("转换时发生错误："+er.Message,"提示",MessageBoxButtons.OK,MessageBoxIcon.Stop);
//			}		
			
		}
		void ThreadWork()
		{	
			this.statusBarPanel1.Text="正在转换，请稍候...";
			CSharpToVBConverter o = new CSharpToVBConverter();
			this.txtVB.Text = o.Execute(this.txtCSharp.Text);

			string path=Application.StartupPath;
			try
			{
				//ass.KeyWordFormat(txtVB,"VB",path);
			}
			catch{}
			//Change the Page in the Tab Control so we can see the changes
			//this.tabControl1.SelectedTab = this.tabControl1.TabPages[1];
			this.tabControl1.SelectedIndex=1;
			this.statusBarPanel1.Text="转换完成。";
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			SaveFileDialog sqlsavedlg=new SaveFileDialog();
			sqlsavedlg.Title="保存当前查询";
			string text="";

			if(this.tabControl1.SelectedIndex==0)
			{
				sqlsavedlg.Filter="C# Files (*.cs)|*.cs|All files (*.*)|*.*" ;	
				text=this.txtCSharp.Text;
			}
			else
			{
				sqlsavedlg.Filter="VB.NET Files (*.vb)|*.vb|All files (*.*)|*.*" ;	
				text=this.txtVB.Text;
			}
			DialogResult dlgresult=sqlsavedlg.ShowDialog(this);
			if(dlgresult==DialogResult.OK)
			{
				string filename=sqlsavedlg.FileName;
				StreamWriter sw=new StreamWriter(filename,false,Encoding.Default);//,false);
				sw.Write(text);
				sw.Flush();
				sw.Close();
			}		
		}

		private void btnExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
