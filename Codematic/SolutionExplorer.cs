using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Codematic
{
	/// <summary>
	/// SolutionExplorer 的摘要说明。
	/// </summary>
	public class SolutionExplorer : System.Windows.Forms.Form
    {
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.ImageList treeImgs;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton toolStripButton2;
        private ToolStripButton toolStripButton3;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton toolStripButton4;
		private System.ComponentModel.IContainer components;

		public SolutionExplorer()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
			treeView1.ExpandAll();
			treeView1.Scrollable=true;
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("引用", 2, 3);
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("AssemblyInfo.cs", 4, 4);
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("test", 4, 4);
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("ExamForm.cs", 2, 3, new System.Windows.Forms.TreeNode[] {
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Exam", 1, 1, new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode4});
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("解决方案“Exam”(1项目)", 0, 0, new System.Windows.Forms.TreeNode[] {
            treeNode5});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SolutionExplorer));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.treeImgs = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.treeImgs;
            this.treeView1.Location = new System.Drawing.Point(0, 25);
            this.treeView1.Name = "treeView1";
            treeNode1.ImageIndex = 2;
            treeNode1.Name = "";
            treeNode1.SelectedImageIndex = 3;
            treeNode1.Text = "引用";
            treeNode2.ImageIndex = 4;
            treeNode2.Name = "";
            treeNode2.SelectedImageIndex = 4;
            treeNode2.Text = "AssemblyInfo.cs";
            treeNode3.ImageIndex = 4;
            treeNode3.Name = "test";
            treeNode3.SelectedImageIndex = 4;
            treeNode3.Text = "test";
            treeNode4.ImageIndex = 2;
            treeNode4.Name = "";
            treeNode4.SelectedImageIndex = 3;
            treeNode4.Text = "ExamForm.cs";
            treeNode5.ImageIndex = 1;
            treeNode5.Name = "";
            treeNode5.SelectedImageIndex = 1;
            treeNode5.Text = "Exam";
            treeNode6.ImageIndex = 0;
            treeNode6.Name = "";
            treeNode6.SelectedImageIndex = 0;
            treeNode6.Text = "解决方案“Exam”(1项目)";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode6});
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.ShowRootLines = false;
            this.treeView1.Size = new System.Drawing.Size(212, 241);
            this.treeView1.TabIndex = 1;
            // 
            // treeImgs
            // 
            this.treeImgs.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("treeImgs.ImageStream")));
            this.treeImgs.TransparentColor = System.Drawing.Color.Transparent;
            this.treeImgs.Images.SetKeyName(0, "sln.ico");
            this.treeImgs.Images.SetKeyName(1, "csproj.ico");
            this.treeImgs.Images.SetKeyName(2, "Folderclose.gif");
            this.treeImgs.Images.SetKeyName(3, "Folderopen.gif");
            this.treeImgs.Images.SetKeyName(4, "cs.gif");
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("toolStrip1.BackgroundImage")));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripSeparator1,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripSeparator2,
            this.toolStripButton4});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(212, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.ToolTipText = "属性";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "toolStripButton2";
            this.toolStripButton2.ToolTipText = "显示所有文件";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "toolStripButton3";
            this.toolStripButton3.ToolTipText = "刷新";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton4.Text = "toolStripButton4";
            this.toolStripButton4.ToolTipText = "查看代码";
            // 
            // SolutionExplorer
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(212, 266);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "SolutionExplorer";
            this.Text = "SolutionExplorer";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
	}
}
