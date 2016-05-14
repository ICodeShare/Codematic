using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
namespace Codematic
{
    public partial class NewProject : Form
    {
        Thread mythread;
        string cmcfgfile = Application.StartupPath + @"\cmcfg.ini";
        Maticsoft.Utility.INIFile cfgfile;
        string folder1 = "";//源目录
        string folder2 = "";
        string ProName = "";




        public NewProject()
        {
            InitializeComponent();
            InitTreeView();
            InitListView();
            btn_ok.Enabled = false;            
        }
        private void NewProject_Load(object sender, EventArgs e)
        {
            if (File.Exists(cmcfgfile))
            {
                cfgfile = new Maticsoft.Utility.INIFile(cmcfgfile);
                string lastpath = cfgfile.IniReadValue("Project", "lastpath");
                if (lastpath.Trim() != "")
                {
                    txtProPath.Text = lastpath;
                }
            }
        }

        #region listview
        private void InitTreeView()
        {
            TreeNode tnEnviroment = new TreeNode("常规", 0, 1);
            //TreeNode tnEditor = new TreeNode("模版架构", 2, 3);

            this.treeView1.Nodes.Add(tnEnviroment);
            //this.treeView1.Nodes.Add(tnEditor);

        }
        private void InitListView()
        {
            this.listView1.Columns.Clear();
            this.listView1.Items.Clear();
            this.listView1.LargeImageList = imageList1;
            //this.listView1.SmallImageList = imglistView;
            this.listView1.View = View.LargeIcon;
            listView1.HideSelection = false;

            ListViewGroup listViewGroup1 = new ListViewGroup("Codematic 已安装的模版", HorizontalAlignment.Left);
            listViewGroup1.Header = "Codematic 已安装的模版";
            listViewGroup1.Name = "listViewGroup1";
            this.listView1.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1});

            string strname = "单类结构";
            //ListViewItem item1 = new ListViewItem(strname, 0);
            //item1.SubItems.Add(strname);
            //item1.ImageIndex = 0;
            //item1.Group = listViewGroup1;
            //listView1.Items.AddRange(new ListViewItem[] { item1 });

            strname = "简单三层结构";
            ListViewItem item2 = new ListViewItem(strname, 0);
            item2.SubItems.Add(strname);
            item2.ImageIndex = 1;
            item2.Group = listViewGroup1;            
            listView1.Items.AddRange(new ListViewItem[] { item2 });

            strname = "工厂模式结构";
            ListViewItem item3 = new ListViewItem(strname, 0);
            item3.SubItems.Add(strname);
            item3.ImageIndex = 2;
            item3.Group = listViewGroup1;
            listView1.Items.AddRange(new ListViewItem[] { item3 });

            strname = "简单三层结构(管理)";
            ListViewItem item4 = new ListViewItem(strname, 0);
            item4.SubItems.Add(strname);
            item4.ImageIndex = 1;
            item4.Group = listViewGroup1;
            listView1.Items.AddRange(new ListViewItem[] { item4 });
                        
            item2.Focused=true;
            item2.Selected = true;

        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count < 1)
            {
                return;
            }
            string selstr = listView1.SelectedItems[0].Text;
            
            switch (selstr)
            {
                case "单类结构":
                    lblTooltip.Text = "创建单类结构的项目";
                    btn_ok.Enabled = false;   
                    break;
                case "简单三层结构":
                    lblTooltip.Text = "创建简单三层结构的项目(基于VS2005)";
                    btn_ok.Enabled = true;   
                    break;
                case "简单三层结构(管理)":
                    lblTooltip.Text = "创建包含基本系统管理功能的简单三层结构的项目(基于VS2008)";
                    btn_ok.Enabled = true;
                    break;
                case "工厂模式结构":
                    lblTooltip.Text = "创建工厂模式结构的项目(基于VS2005)";
                    btn_ok.Enabled = true;   
                    break;
            }
        }
        #endregion

        private void btn_browser_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            DialogResult result = folder.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                this.txtProPath.Text = folder.SelectedPath;
            }
        }

        #region 按钮
        private void btn_ok_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count < 1)
            {
                return;
            }            
            string selstr = this.listView1.SelectedItems[0].Text;            
            string daltype = "S3";
            switch (selstr)
            {
                case "单类结构":
                    {
                        daltype = "One";
                        MessageBox.Show("该功能暂不支持，可以选择其他功能继续使用。");
                    }
                    break;
                case "简单三层结构":
                    {
                        folder1 = Application.StartupPath + "\\Template\\CodematicDemoS3";
                        daltype = "S3";                        
                    }
                    break;
                case "简单三层结构(管理)":
                    {
                        folder1 = Application.StartupPath + "\\Template\\CodematicDemoS3p";
                        daltype = "S3p";
                    }
                    break;
                case "工厂模式结构":
                    {
                        folder1 = Application.StartupPath + "\\Template\\CodematicDemoF3";
                        daltype = "F3";                        
                    }
                    break;
                default:
                    break;
            }
            
            #region 复制目录
            folder2 = txtProPath.Text.Trim();
            ProName = txtProName.Text.Trim();
            cfgfile.IniWriteValue("Project", "lastpath", folder2);
            if (ProName != "")
            {
                folder2 += "\\" + ProName;
            }
            else
            {
                MessageBox.Show("请输入项目名称！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (txtProPath.Text.Trim() == "")
            {
                MessageBox.Show("请选择输出目录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string longservername = FormCommon.GetDbViewSelServer();
            if (longservername == "")
            {
                MessageBox.Show("你尚未选择要生成代码的数据库信息！\r\n请先建立数据库连接。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            #region 目录检查
            //try
            //{
            //    DirectoryInfo source = new DirectoryInfo(folder1);
            //    DirectoryInfo target = new DirectoryInfo(folder2);
            //    if (!source.Exists)
            //    {
            //        MessageBox.Show("源目录已经不存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        return;
            //    }
            //    if (!target.Exists)
            //    {
            //        try
            //        {
            //            target.Create();
            //        }
            //        catch
            //        {
            //            MessageBox.Show("目标目录不存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            return;
            //        }
            //    }
            //}
            //catch
            //{
            //    MessageBox.Show("目录信息有误！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            #endregion
                        
            //mythread = new Thread(new ThreadStart(ThreadWork));
            //mythread.Start();            

            NewProjectDB npb = new NewProjectDB(longservername, this, folder2, daltype, ProName);
            npb.Show();

            #endregion
            
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
            
        }
        #endregion

        #region  复制项目

        void ThreadWork()
        {
            CopyDirectory(folder1, folder2);
        }
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

        
    }
}