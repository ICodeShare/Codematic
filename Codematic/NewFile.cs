using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace Codematic
{
    public partial class NewFile : Form
    {

        MainForm mainfrm;
        
        public NewFile(Form mdiParentForm)
        {
            InitializeComponent();
            mainfrm = (MainForm)mdiParentForm;
            InitTreeView();
            InitListView();
            
        }

        #region 初始化tree
        private void InitTreeView()
        {
            TreeNode tnEnviroment = new TreeNode("常规", 0, 1);
            TreeNode tnEditor = new TreeNode("模版", 2, 3);

            this.treeView1.Nodes.Add(tnEnviroment);
            this.treeView1.Nodes.Add(tnEditor);

        }
        #endregion

        #region 初始化项

        private void InitListView()
        {
            this.listView1.Columns.Clear();
            this.listView1.Items.Clear();
            this.listView1.LargeImageList = imageList1;
            //this.listView1.SmallImageList = imglistView;
            this.listView1.View = View.LargeIcon;

            ListViewGroup listViewGroup1 = new ListViewGroup("Codematic 已安装的模版", HorizontalAlignment.Left);
            listViewGroup1.Header = "Codematic 已安装的模版";
            listViewGroup1.Name = "listViewGroup1";
            this.listView1.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1});

            string strname = "文本文件";
            ListViewItem item1 = new ListViewItem(strname, 0);
            item1.SubItems.Add(strname);
            item1.ImageIndex = 0;
            item1.Group = listViewGroup1;
            listView1.Items.AddRange(new ListViewItem[] { item1 });

            strname = "C# 类";
            ListViewItem item2 = new ListViewItem(strname, 0);
            item2.SubItems.Add(strname);
            item2.ImageIndex = 1;
            item2.Group = listViewGroup1;
            listView1.Items.AddRange(new ListViewItem[] { item2 });

            strname = "VB 类";
            ListViewItem item3 = new ListViewItem(strname, 0);
            item3.SubItems.Add(strname);
            item3.ImageIndex = 2;
            item3.Group = listViewGroup1;
            listView1.Items.AddRange(new ListViewItem[] { item3 });

        }

        #endregion



        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count < 1)
            {
                return;
            }
            string selstr = this.listView1.SelectedItems[0].Text;
            switch (selstr)
            {
                case "文本文件":
                    lblTooltip.Text = "创建空白文本文件";
                    break;
                case "C# 类":
                    lblTooltip.Text = "创建空白C# 类";
                    break;
                case "VB 类":
                    lblTooltip.Text = "创建空白VB 类";
                    break;
            }

        }
        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count < 1)
            {
                return;
            }

            string selstr = this.listView1.SelectedItems[0].Text;
            switch (selstr)
            {
                case "文本文件":
                    {
                        string tempfile = Application.StartupPath+@"\tempnewtxt.txt";
                        StreamWriter sw = new StreamWriter(tempfile, false, Encoding.Default);//,false);
                        sw.Write("");
                        sw.Flush();
                        sw.Close();
                        mainfrm.AddTabPage("TextFile1.txt",new CodeEditor(tempfile,"txt"));
                    }
                    break;
                case "C# 类":
                    {
                        string tempfile = Application.StartupPath+@"\Template\Class1.cs";
                        if (!File.Exists(tempfile))
                        {
                            StreamWriter sw = new StreamWriter(tempfile, false, Encoding.Default);//,false);
                            sw.Write("");
                            sw.Flush();
                            sw.Close();
                        }
                        mainfrm.AddTabPage( "Class1.cs",new CodeEditor(tempfile,"cs"));
                    }
                    break;
                case "VB 类":
                    {
                        string tempfile = Application.StartupPath+@"\Template\Class1.vb";
                        if (!File.Exists(tempfile))
                        {
                            StreamWriter sw = new StreamWriter(tempfile, false, Encoding.Default);//,false);
                            sw.Write("");
                            sw.Flush();
                            sw.Close();
                        }
                        mainfrm.AddTabPage("Class1.vb",new CodeEditor(tempfile,"vb"));
                    }
                    break;
                default:
                    //return;
                    break;
            }
            Close();
        }


    }
}