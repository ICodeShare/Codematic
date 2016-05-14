using Codematic.Properties;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace Codematic
{
    partial class TempView
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("默认", 2, 2);
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("实体类", new System.Windows.Forms.TreeNode[] {
            treeNode1});
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("C#代码");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("VB代码");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("页面");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("代码模版", new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TempView));
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.打开并生成ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑查看ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.新建ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.文件夹ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.模版ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.刷新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.重命名ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.打开所在文件夹ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置模板文件夹ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.备份模板到ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btn_NewFolder = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_NewFile = new System.Windows.Forms.ToolStripButton();
            this.btn_Refrush = new System.Windows.Forms.ToolStripButton();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.ContextMenuStrip = this.contextMenuStrip1;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(0, 25);
            this.treeView1.Name = "treeView1";
            treeNode1.ImageIndex = 2;
            treeNode1.Name = "节点6";
            treeNode1.SelectedImageIndex = 2;
            treeNode1.Text = "默认";
            treeNode2.Name = "节点4";
            treeNode2.Text = "实体类";
            treeNode3.Name = "节点2";
            treeNode3.Text = "C#代码";
            treeNode4.Name = "节点5";
            treeNode4.Text = "VB代码";
            treeNode5.Name = "节点3";
            treeNode5.Text = "页面";
            treeNode6.Name = "节点0";
            treeNode6.Text = "代码模版";
            this.treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode6});
            this.treeView1.SelectedImageIndex = 1;
            this.treeView1.Size = new System.Drawing.Size(171, 343);
            this.treeView1.TabIndex = 1;
            this.treeView1.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeView1_AfterLabelEdit);
            this.treeView1.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeView1_ItemDrag);
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeView1_DragDrop);
            this.treeView1.DoubleClick += new System.EventHandler(this.treeView1_DoubleClick);
            this.treeView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开并生成ToolStripMenuItem,
            this.编辑查看ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.新建ToolStripMenuItem,
            this.刷新ToolStripMenuItem,
            this.删除ToolStripMenuItem,
            this.重命名ToolStripMenuItem,
            this.toolStripMenuItem2,
            this.打开所在文件夹ToolStripMenuItem,
            this.设置模板文件夹ToolStripMenuItem,
            this.备份模板到ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(161, 214);
            // 
            // 打开并生成ToolStripMenuItem
            // 
            this.打开并生成ToolStripMenuItem.Name = "打开并生成ToolStripMenuItem";
            this.打开并生成ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.打开并生成ToolStripMenuItem.Text = "打开生成(&O)";
            this.打开并生成ToolStripMenuItem.Click += new System.EventHandler(this.打开生成ToolStripMenuItem_Click);
            // 
            // 编辑查看ToolStripMenuItem
            // 
            this.编辑查看ToolStripMenuItem.Name = "编辑查看ToolStripMenuItem";
            this.编辑查看ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.编辑查看ToolStripMenuItem.Text = "编辑查看(&V)";
            this.编辑查看ToolStripMenuItem.Click += new System.EventHandler(this.编辑查看ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(157, 6);
            // 
            // 新建ToolStripMenuItem
            // 
            this.新建ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件夹ToolStripMenuItem,
            this.模版ToolStripMenuItem});
            this.新建ToolStripMenuItem.Name = "新建ToolStripMenuItem";
            this.新建ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.新建ToolStripMenuItem.Text = "新建(&N)";
            // 
            // 文件夹ToolStripMenuItem
            // 
            this.文件夹ToolStripMenuItem.Image = global::Codematic.Properties.Resources.Folderclose;
            this.文件夹ToolStripMenuItem.Name = "文件夹ToolStripMenuItem";
            this.文件夹ToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.文件夹ToolStripMenuItem.Text = "文件夹(&F)";
            this.文件夹ToolStripMenuItem.Click += new System.EventHandler(this.文件夹ToolStripMenuItem_Click);
            // 
            // 模版ToolStripMenuItem
            // 
            this.模版ToolStripMenuItem.Image = global::Codematic.Properties.Resources.temp;
            this.模版ToolStripMenuItem.Name = "模版ToolStripMenuItem";
            this.模版ToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.模版ToolStripMenuItem.Text = "模版(&T)";
            this.模版ToolStripMenuItem.Click += new System.EventHandler(this.模版ToolStripMenuItem_Click);
            // 
            // 刷新ToolStripMenuItem
            // 
            this.刷新ToolStripMenuItem.Name = "刷新ToolStripMenuItem";
            this.刷新ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.刷新ToolStripMenuItem.Text = "刷新(&R)";
            this.刷新ToolStripMenuItem.Click += new System.EventHandler(this.刷新ToolStripMenuItem_Click);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.删除ToolStripMenuItem.Text = "删除(&D)";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // 重命名ToolStripMenuItem
            // 
            this.重命名ToolStripMenuItem.Name = "重命名ToolStripMenuItem";
            this.重命名ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.重命名ToolStripMenuItem.Text = "重命名(&M)";
            this.重命名ToolStripMenuItem.Click += new System.EventHandler(this.重命名ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(157, 6);
            // 
            // 打开所在文件夹ToolStripMenuItem
            // 
            this.打开所在文件夹ToolStripMenuItem.Name = "打开所在文件夹ToolStripMenuItem";
            this.打开所在文件夹ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.打开所在文件夹ToolStripMenuItem.Text = "打开所在文件夹";
            this.打开所在文件夹ToolStripMenuItem.Click += new System.EventHandler(this.打开所在文件夹ToolStripMenuItem_Click);
            // 
            // 设置模板文件夹ToolStripMenuItem
            // 
            this.设置模板文件夹ToolStripMenuItem.Name = "设置模板文件夹ToolStripMenuItem";
            this.设置模板文件夹ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.设置模板文件夹ToolStripMenuItem.Text = "设置模板文件夹";
            this.设置模板文件夹ToolStripMenuItem.Click += new System.EventHandler(this.设置模板文件夹ToolStripMenuItem_Click);
            // 
            // 备份模板到ToolStripMenuItem
            // 
            this.备份模板到ToolStripMenuItem.Name = "备份模板到ToolStripMenuItem";
            this.备份模板到ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.备份模板到ToolStripMenuItem.Text = "备份模板到";
            this.备份模板到ToolStripMenuItem.Click += new System.EventHandler(this.备份模板到ToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "Folderclose.gif");
            this.imageList1.Images.SetKeyName(1, "Folderopen.gif");
            this.imageList1.Images.SetKeyName(2, "temp.png");
            this.imageList1.Images.SetKeyName(3, "cs32.gif");
            this.imageList1.Images.SetKeyName(4, "vb.gif");
            this.imageList1.Images.SetKeyName(5, "app.gif");
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(171, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btn_NewFolder
            // 
            this.btn_NewFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_NewFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_NewFolder.Name = "btn_NewFolder";
            this.btn_NewFolder.Size = new System.Drawing.Size(23, 22);
            this.btn_NewFolder.Text = "toolStripButton1";
            this.btn_NewFolder.ToolTipText = "新建文件夹";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btn_NewFile
            // 
            this.btn_NewFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_NewFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_NewFile.Name = "btn_NewFile";
            this.btn_NewFile.Size = new System.Drawing.Size(23, 22);
            this.btn_NewFile.Text = "toolStripButton2";
            this.btn_NewFile.ToolTipText = "新建模版文件";
            // 
            // btn_Refrush
            // 
            this.btn_Refrush.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_Refrush.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_Refrush.Name = "btn_Refrush";
            this.btn_Refrush.Size = new System.Drawing.Size(23, 22);
            this.btn_Refrush.Text = "toolStripButton3";
            this.btn_Refrush.ToolTipText = "刷新";
            // 
            // TempView
            // 
            this.ClientSize = new System.Drawing.Size(171, 368);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "TempView";
            this.Text = "TempView";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripButton btn_NewFolder;
        private ToolStripSeparator toolStripSeparator1;
        private TreeView treeView1;
        private ToolStripButton btn_NewFile;
        private ToolStripButton btn_Refrush;
        private ImageList imageList1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem 打开并生成ToolStripMenuItem;
        private ToolStripMenuItem 编辑查看ToolStripMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem 新建ToolStripMenuItem;
        private ToolStripMenuItem 文件夹ToolStripMenuItem;
        private ToolStripMenuItem 模版ToolStripMenuItem;
        private ToolStripMenuItem 刷新ToolStripMenuItem;
        private ToolStripMenuItem 删除ToolStripMenuItem;
        private ToolStripMenuItem 重命名ToolStripMenuItem;
        private FolderBrowserDialog folderBrowserDialog1;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem 设置模板文件夹ToolStripMenuItem;
        private ToolStripMenuItem 备份模板到ToolStripMenuItem;
        private ToolStripMenuItem 打开所在文件夹ToolStripMenuItem;
    }
}