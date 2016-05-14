using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Codematic.UserControls;
using Maticsoft.CmConfig;
using System.Diagnostics;
namespace Codematic
{
    public partial class TempView : Form
    {
        private MainForm mainfrm;
        TempNode TreeClickNode; //右键菜单单击的节点
        DataSet ds ; //菜单数据
        Maticsoft.CmConfig.AppSettings settings;
        string tempfilepath = "temptree.xml"; //菜单树文件
        private string TemplateFolder;

        public TempView(Form mdiParentForm)
        {
            InitializeComponent();
            this.settings = AppConfig.GetSettings();
            if (this.settings.TemplateFolder == "Template" || this.settings.TemplateFolder == "Template\\TemplateFile" || this.settings.TemplateFolder.Length == 0)
            {
                this.TemplateFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Template\\TemplateFile");
            }
            else
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(this.settings.TemplateFolder);
                if (directoryInfo.Exists)
                {
                    this.TemplateFolder = this.settings.TemplateFolder;
                }
                else
                {
                    this.TemplateFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Template\\TemplateFile");
                }
            }
            this.CreateFolderTree(this.TemplateFolder);
            //settings = Maticsoft.CmConfig.AppConfig.GetSettings();
            //LoadTreeview();
        }

        #region 初始化Treeview项
        private void CreateFolderTree(string templateFolder)
        {
            this.treeView1.Nodes.Clear();
            TempNode tempNode = new TempNode("代码模版");
            tempNode.NodeType = "root";
            tempNode.FilePath = templateFolder;
            tempNode.ImageIndex = 0;
            tempNode.SelectedImageIndex = 0;
            tempNode.Expand();
            this.treeView1.Nodes.Add(tempNode);
            this.LoadFolderTree(tempNode, templateFolder);
        }

        private void LoadFolderTree(TreeNode parentnode, string templateFolder)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(templateFolder);
            if (!directoryInfo.Exists)
            {
                return;
            }
            DirectoryInfo[] directories = directoryInfo.GetDirectories();
            for (int i = 0; i < directories.Length; i++)
            {
                TempNode tempNode = new TempNode(directories[i].Name);
                tempNode.NodeType = "folder";
                tempNode.FilePath = directories[i].FullName;
                tempNode.ImageIndex = 0;
                tempNode.SelectedImageIndex = 1;
                parentnode.Nodes.Add(tempNode);
                this.LoadFolderTree(tempNode, directories[i].FullName);
            }
            FileInfo[] files = directoryInfo.GetFiles();
            int num = files.Length;
            for (int j = 0; j < num; j++)
            {
                if (files[j].Extension == ".tt" || files[j].Extension == ".cmt" || files[j].Extension == ".aspx" || files[j].Extension == ".cs" || files[j].Extension == ".vb")
                {
                    TempNode tempNode2 = new TempNode(files[j].Name);
                    tempNode2.FilePath = files[j].FullName;
                    string extension;
                    if ((extension = files[j].Extension) == null)
                    {
                        goto IL_218;
                    }
                    if (!(extension == ".tt"))
                    {
                        if (!(extension == ".cmt"))
                        {
                            if (!(extension == ".cs"))
                            {
                                if (!(extension == ".vb"))
                                {
                                    if (!(extension == ".aspx"))
                                    {
                                        goto IL_218;
                                    }
                                    tempNode2.NodeType = "aspx";
                                    tempNode2.ImageIndex = 5;
                                    tempNode2.SelectedImageIndex = 5;
                                }
                                else
                                {
                                    tempNode2.NodeType = "vb";
                                    tempNode2.ImageIndex = 4;
                                    tempNode2.SelectedImageIndex = 4;
                                }
                            }
                            else
                            {
                                tempNode2.NodeType = "cs";
                                tempNode2.ImageIndex = 3;
                                tempNode2.SelectedImageIndex = 3;
                            }
                        }
                        else
                        {
                            tempNode2.NodeType = "cmt";
                            tempNode2.ImageIndex = 2;
                            tempNode2.SelectedImageIndex = 2;
                        }
                    }
                    else
                    {
                        tempNode2.NodeType = "tt";
                        tempNode2.ImageIndex = 2;
                        tempNode2.SelectedImageIndex = 2;
                    }
                IL_228:
                    parentnode.Nodes.Add(tempNode2);
                    goto IL_236;
                IL_218:
                    tempNode2.ImageIndex = 2;
                    tempNode2.SelectedImageIndex = 2;
                    goto IL_228;
                }
            IL_236: ;
            }
        }

        private void LoadTreeview()
        {
            ds = new DataSet(); //菜单数据
            treeView1.Nodes.Clear();
            TempNode rootNode = new TempNode("代码模版");
            rootNode.NodeType = "root";
            rootNode.ImageIndex = 0;
            rootNode.SelectedImageIndex = 0;
            rootNode.Expand();
            treeView1.Nodes.Add(rootNode);
                        
            ds.ReadXml(tempfilepath);
            DataTable dt=ds.Tables[0];
            
            DataRow[] drs = dt.Select("ParentID= " + 0); //选出所有一级节点	
            foreach (DataRow r in drs)
            {
                string nodeid = r["NodeID"].ToString();
                string text = r["Text"].ToString();
                string filepath = r["FilePath"].ToString();
                string nodetype = r["NodeType"].ToString();
                
                TempNode node = new TempNode(text);
                node.NodeID = nodeid;
                node.NodeType = nodetype;
                node.FilePath = filepath;
                if (nodetype == "folder")
                {
                    node.ImageIndex = 0;
                    node.SelectedImageIndex = 1;
                }
                else
                {
                    node.ImageIndex = 2;
                    node.SelectedImageIndex = 2;
                }                
                rootNode.Nodes.Add(node);
                
                int sonparentid = int.Parse(nodeid); // or =location
                CreateNode(sonparentid,node,dt);
            }
            

        }

        //邦定任意节点
        public void CreateNode(int parentid, TreeNode parentnode, DataTable dt)
        {
            DataRow[] drs = dt.Select("ParentID= " + parentid); //选出所有子节点			
            foreach (DataRow r in drs)
            {
                string nodeid = r["NodeID"].ToString();
                string text = r["Text"].ToString();
                string filepath = r["FilePath"].ToString();
                string nodetype = r["NodeType"].ToString();
                
                TempNode node = new TempNode(text);
                node.NodeID = nodeid;
                node.NodeType = nodetype;
                node.FilePath = filepath;
                if (nodetype == "folder")
                {
                    node.ImageIndex = 0;
                    node.SelectedImageIndex = 1;
                }
                else
                {
                    node.ImageIndex = 2;
                    node.SelectedImageIndex = 2;
                }                        

                parentnode.Nodes.Add(node);

                int sonparentid = int.Parse(nodeid);// or =location
                CreateNode(sonparentid, node, dt);

            }//endforeach		

        }


        #endregion

        #region treeView1操作

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {            
        }
        private void treeView1_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                Point mpt = new Point(e.X, e.Y);
                TreeClickNode = (TempNode)this.treeView1.GetNodeAt(mpt);
                this.treeView1.SelectedNode = TreeClickNode;
                if (TreeClickNode != null)
                {                   
                    if (e.Button == MouseButtons.Right)
                    {
                        CreatMenu(TreeClickNode.NodeType);
                        contextMenuStrip1.Show(treeView1, mpt);
                    }
                }
            }
            catch
            {
            }
        }

        #region 创建treeview 右键菜单

        private void CreatMenu(string NodeType)
        {
            this.打开并生成ToolStripMenuItem.Visible = false;
            this.编辑查看ToolStripMenuItem.Visible = false;
            this.新建ToolStripMenuItem.Visible = false;
            this.设置模板文件夹ToolStripMenuItem.Visible = false;
            this.备份模板到ToolStripMenuItem.Visible = false;
            if (NodeType != null)
            {
                if (NodeType == "root")
                {
                    this.新建ToolStripMenuItem.Visible = true;
                    this.打开所在文件夹ToolStripMenuItem.Visible = true;
                    this.设置模板文件夹ToolStripMenuItem.Visible = true;
                    this.备份模板到ToolStripMenuItem.Visible = true;
                    return;
                }
                if (NodeType == "folder")
                {
                    this.新建ToolStripMenuItem.Visible = true;
                    this.设置模板文件夹ToolStripMenuItem.Visible = false;
                    this.备份模板到ToolStripMenuItem.Visible = false;
                    this.打开所在文件夹ToolStripMenuItem.Visible = true;
                    return;
                }
            }
            this.打开并生成ToolStripMenuItem.Visible = true;
            this.编辑查看ToolStripMenuItem.Visible = true;
            this.打开所在文件夹ToolStripMenuItem.Visible = false;
        }
        private void treeView1_DragDrop(object sender, DragEventArgs e)
        {
        }
        private void treeView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
        }
        #endregion

        private string GetMaxNodeID(DataTable dt)
        {
            int num = 1;
            foreach (DataRow dataRow in dt.Rows)
            {
                string s = dataRow["NodeID"].ToString();
                if (num < int.Parse(s))
                {
                    num = int.Parse(s);
                }
            }
            return (num + 1).ToString();
        }
        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            TempNode tempNode = (TempNode)this.treeView1.SelectedNode;
            string arg_17_0 = tempNode.NodeID;
            string nodeType = tempNode.NodeType;
            string a;
            if ((a = nodeType) != null)
            {
                if (!(a == "tt") && !(a == "cmt") && !(a == "vb") && !(a == "aspx") && !(a == "cs"))
                {
                    return;
                }
                this.打开生成ToolStripMenuItem_Click(sender, e);
            }
        }
        private void 打开生成ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TempNode tempNode = (TempNode)this.treeView1.SelectedNode;
                string arg_17_0 = tempNode.NodeID;
                string text = tempNode.Text;
                string filePath = tempNode.FilePath;
                string nodeType = tempNode.NodeType;
                if (filePath.Trim() != "")
                {
                    string a;
                    if ((a = nodeType) != null)
                    {
                        if (a == "folder")
                        {
                            goto IL_DF;
                        }
                        if (a == "tt" || a == "cmt")
                        {
                            CodeTemplate codeTemplate = (CodeTemplate)Application.OpenForms["CodeTemplate"];
                            if (codeTemplate != null)
                            {
                                codeTemplate.SettxtTemplate(filePath);
                                goto IL_DF;
                            }
                            MessageBox.Show("尚未打开模版代码生成器！请选中表，然后右键打开模版代码生成器。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            goto IL_DF;
                        }
                    }
                    if (File.Exists(filePath))
                    {
                        this.mainfrm.AddTabPage(text, new CodeEditor(filePath, nodeType, false));
                    }
                }
                else
                {
                    MessageBox.Show("所选文件已经不存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            IL_DF: ;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void 编辑查看ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TempNode tempNode = (TempNode)this.treeView1.SelectedNode;
                string arg_17_0 = tempNode.NodeID;
                string text = tempNode.Text;
                string filePath = tempNode.FilePath;
                string nodeType = tempNode.NodeType;
                if (filePath.Trim() != "" && nodeType != "folder")
                {
                    if (File.Exists(filePath))
                    {
                        this.mainfrm.AddTabPage(text, new CodeEditor(filePath, nodeType, false));
                    }
                }
                else
                {
                    MessageBox.Show("所选文件已经不存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void 文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TempNode tempNode = (TempNode)this.treeView1.SelectedNode;
                string text = tempNode.FilePath + "\\新文件夹";
                if (!Directory.Exists(text))
                {
                    Directory.CreateDirectory(text);
                }
                TempNode tempNode2 = new TempNode("新文件夹");
                tempNode2.FilePath = text;
                tempNode2.NodeType = "folder";
                tempNode2.ImageIndex = 0;
                tempNode2.SelectedImageIndex = 1;
                tempNode.Nodes.Add(tempNode2);
                tempNode.Expand();
                this.treeView1.SelectedNode = tempNode2;
                this.treeView1.LabelEdit = true;
                if (!tempNode2.IsEditing)
                {
                    tempNode2.BeginEdit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public string GetFileName(string path, string oldname, out string filename)
        {
            string text = path + "\\" + oldname + ".cmt";
            filename = oldname;
            int num = 1;
            while (File.Exists(text))
            {
                filename = oldname + num;
                text = path + "\\" + filename + ".cmt";
                num++;
            }
            return text;
        }
        private void 模版ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TempNode tempNode = (TempNode)this.treeView1.SelectedNode;
                string text = tempNode.FilePath + "\\新建模板.cmt";
                string text2 = "新建模板";
                text = this.GetFileName(tempNode.FilePath, text2, out text2);
                StreamWriter streamWriter = new StreamWriter(text, false, Encoding.UTF8);
                streamWriter.Close();
                TempNode tempNode2 = new TempNode(text2 + ".cmt");
                tempNode2.FilePath = text;
                tempNode2.NodeType = "cmt";
                tempNode2.ImageIndex = 2;
                tempNode2.SelectedImageIndex = 2;
                tempNode.Nodes.Add(tempNode2);
                this.treeView1.SelectedNode = tempNode2;
                this.treeView1.LabelEdit = true;
                if (!tempNode2.IsEditing)
                {
                    tempNode2.BeginEdit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.CreateFolderTree(this.TemplateFolder);
        }
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TempNode tempNode = (TempNode)this.treeView1.SelectedNode;
                string arg_17_0 = tempNode.NodeID;
                string arg_1E_0 = tempNode.Text;
                string filePath = tempNode.FilePath;
                string nodeType = tempNode.NodeType;
                if ((nodeType == "tt" || nodeType == "cmt" || nodeType == "aspx" || nodeType == "cs" || nodeType == "vb") && File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                if (nodeType == "folder")
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(filePath);
                    if (directoryInfo.Exists)
                    {
                        directoryInfo.Delete();
                    }
                }
                this.treeView1.Nodes.Remove(tempNode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void 重命名ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                TempNode tempNode = (TempNode)this.treeView1.SelectedNode;
                if (tempNode != null && tempNode.Parent != null)
                {
                    this.treeView1.SelectedNode = tempNode;
                    this.treeView1.LabelEdit = true;
                    if (!tempNode.IsEditing)
                    {
                        tempNode.BeginEdit();
                    }
                }
                else
                {
                    MessageBox.Show("没有选择节点或该节点是根节点.\n", "无效选择");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (e.Label != null)
            {
                if (e.Label.Length > 0)
                {
                    if (e.Label.IndexOfAny(new char[]
					{
						'@',
						',',
						'!'
					}) == -1)
                    {
                        try
                        {
                            e.Node.EndEdit(false);
                            TempNode tempNode = (TempNode)e.Node;
                            string nodeType = tempNode.NodeType;
                            string text = e.Label;
                            int num = text.LastIndexOf(".");
                            if (num == 0)
                            {
                                MessageBox.Show("无效的文件名", "节点编辑");
                                return;
                            }
                            if (text.LastIndexOf(".") < 0)
                            {
                                text += ".cmt";
                            }
                            else
                            {
                                if (text.Substring(num).ToLower() != ".cmt")
                                {
                                    DialogResult dialogResult = MessageBox.Show("如果要更改文件扩展名，则该文件可能无法使用。是否确实要更改它？", "节点编辑", MessageBoxButtons.YesNo);
                                    if (dialogResult != DialogResult.Yes)
                                    {
                                        return;
                                    }
                                }
                            }
                            string filePath = tempNode.FilePath;
                            if (nodeType == "tt" || nodeType == "cmt" || nodeType == "aspx" || nodeType == "cs" || nodeType == "vb")
                            {
                                int length = filePath.LastIndexOf("\\");
                                string text2 = filePath.Substring(0, length) + "\\" + text;
                                File.Move(filePath, text2);
                                tempNode.FilePath = text2;
                            }
                            if (nodeType == "folder")
                            {
                                int length2 = filePath.LastIndexOf("\\");
                                string text2 = filePath.Substring(0, length2) + "\\" + text;
                                Directory.Move(filePath, text2);
                            }
                            goto IL_200;
                        }
                        catch (Exception ex)
                        {
                            e.CancelEdit = true;
                            MessageBox.Show("重命名失败！请稍候再试。" + ex.Message);
                            goto IL_200;
                        }
                    }
                    e.CancelEdit = true;
                    MessageBox.Show("无效节点或无效字符: '@','.', ',', '!'", "节点编辑");
                    e.Node.BeginEdit();
                }
                else
                {
                    e.CancelEdit = true;
                    MessageBox.Show("无效节点或节点名称不能为空！", "节点编辑");
                    e.Node.BeginEdit();
                }
            IL_200:
                this.treeView1.LabelEdit = false;
            }
        }
        private void 设置模板文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = this.folderBrowserDialog1.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                string selectedPath = this.folderBrowserDialog1.SelectedPath;
                AppSettings appSettings = AppConfig.GetSettings();
                appSettings.TemplateFolder = selectedPath;
                AppConfig.SaveSettings(appSettings);
                this.TemplateFolder = selectedPath;
                this.CreateFolderTree(selectedPath);
            }
        }
        private void 备份模板到ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = this.folderBrowserDialog1.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                string selectedPath = this.folderBrowserDialog1.SelectedPath;
                this.CopyDirectory(this.TemplateFolder, selectedPath);
                MessageBox.Show("备份完成！");
                try
                {
                    new Process();
                    Process.Start("explorer.exe", selectedPath);
                }
                catch
                {
                    MessageBox.Show("打开目标文件夹失败，请手动打开该文件夹。");
                }
            }
        }
        public void CopyDirectory(string SourceDirectory, string TargetDirectory)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(SourceDirectory);
            DirectoryInfo directoryInfo2 = new DirectoryInfo(TargetDirectory);
            if (!directoryInfo.Exists)
            {
                return;
            }
            if (!directoryInfo2.Exists)
            {
                directoryInfo2.Create();
            }
            FileInfo[] files = directoryInfo.GetFiles();
            int num = files.Length;
            for (int i = 0; i < num; i++)
            {
                File.Copy(files[i].FullName, directoryInfo2.FullName + "\\" + files[i].Name, true);
            }
            DirectoryInfo[] directories = directoryInfo.GetDirectories();
            for (int j = 0; j < directories.Length; j++)
            {
                this.CopyDirectory(directories[j].FullName, directoryInfo2.FullName + "\\" + directories[j].Name);
            }
        }
        private void 打开所在文件夹ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                new Process();
                Process.Start("explorer.exe", this.TemplateFolder);
            }
            catch
            {
                MessageBox.Show("打开目标文件夹失败，请手动打开该文件夹。");
            }
        }
        #endregion

        

    }
}