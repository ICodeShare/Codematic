using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Codematic
{
	/// <summary>
	/// ClassView 的摘要说明。
	/// </summary>
	public class ClassView : System.Windows.Forms.Form
    {
        private System.Windows.Forms.ImageList toolBarImgs;
		private System.Windows.Forms.TreeView treeView1;
		private System.Windows.Forms.ImageList treeImgs;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton1;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSplitButton toolStripSplitButton1;
		private System.ComponentModel.IContainer components;

		public ClassView()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
			treeView1.ExpandAll();
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
					components.Dispose();//51(aspx)
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClassView));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("ExamForm", 2, 2);
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("节点0", 3, 3);
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Exam", 1, 1, new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("Exam", 0, 0, new System.Windows.Forms.TreeNode[] {
            treeNode3});
            this.toolBarImgs = new System.Windows.Forms.ImageList(this.components);
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.treeImgs = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolBarImgs
            // 
            this.toolBarImgs.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("toolBarImgs.ImageStream")));
            this.toolBarImgs.TransparentColor = System.Drawing.Color.Transparent;
            this.toolBarImgs.Images.SetKeyName(0, "classf.gif");
            this.toolBarImgs.Images.SetKeyName(1, "cn.gif");
            // 
            // treeView1
            // 
            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.treeImgs;
            this.treeView1.Location = new System.Drawing.Point(0, 26);
            this.treeView1.Name = "treeView1";
            treeNode1.ImageIndex = 2;
            treeNode1.Name = "";
            treeNode1.SelectedImageIndex = 2;
            treeNode1.Text = "ExamForm";
            treeNode2.ImageIndex = 3;
            treeNode2.Name = "节点0";
            treeNode2.SelectedImageIndex = 3;
            treeNode2.Text = "节点0";
            treeNode3.ImageIndex = 1;
            treeNode3.Name = "";
            treeNode3.SelectedImageIndex = 1;
            treeNode3.Text = "Exam";
            treeNode4.ImageIndex = 0;
            treeNode4.Name = "";
            treeNode4.SelectedImageIndex = 0;
            treeNode4.Text = "Exam";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4});
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(212, 240);
            this.treeView1.TabIndex = 1;
            // 
            // treeImgs
            // 
            this.treeImgs.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("treeImgs.ImageStream")));
            this.treeImgs.TransparentColor = System.Drawing.Color.Transparent;
            this.treeImgs.Images.SetKeyName(0, "csproj.ico");
            this.treeImgs.Images.SetKeyName(1, "k.gif");
            this.treeImgs.Images.SetKeyName(2, "class.gif");
            this.treeImgs.Images.SetKeyName(3, "interface.gif");
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("toolStrip1.BackgroundImage")));
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripSeparator1,
            this.toolStripSplitButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(212, 26);
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
            this.toolStripButton1.Size = new System.Drawing.Size(23, 23);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 26);
            // 
            // toolStripSplitButton1
            // 
            this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
            this.toolStripSplitButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripSplitButton1.Name = "toolStripSplitButton1";
            this.toolStripSplitButton1.Size = new System.Drawing.Size(35, 23);
            this.toolStripSplitButton1.Text = "toolStripSplitButton1";
            // 
            // ClassView
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(212, 266);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "ClassView";
            this.Text = "ClassView";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
	}
}
