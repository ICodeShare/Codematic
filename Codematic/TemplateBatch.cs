using Codematic.UserControls;
using Maticsoft.CmConfig;
using Maticsoft.CodeBuild;
using Maticsoft.CodeHelper;
using Maticsoft.DBFactory;
using Maticsoft.IDBO;
using Maticsoft.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Codematic
{
    public partial class TemplateBatch : Form
    {
        private delegate void SetBtnEnableCallback();
        private delegate void SetBtnDisableCallback();
        private delegate void SetlblStatuCallback(string text);
        private delegate void SetProBar1MaxCallback(int val);
        private delegate void SetProBar1ValCallback(int val);
        private Thread mythread;
        private string cmcfgfile = Application.StartupPath + "\\cmcfg.ini";
        private INIFile cfgfile;
        private IDbObject dbobj;
        private DbSettings dbset;
        private AppSettings settings;
        private string dbname = "";
        private string TemplateFolder;
        private VSProject vsp = new VSProject();
        private bool isProc;
        public TemplateBatch(string longservername, bool isproc)
        {
            InitializeComponent(); 
            this.dbset = DbConfig.GetSetting(longservername);
            this.dbobj = DBOMaker.CreateDbObj(this.dbset.DbType);
            this.dbobj.DbConnectStr = this.dbset.ConnectStr;
            this.lblServer.Text = this.dbset.Server;
            this.txtFolder.Text = this.dbset.Folder;
            this.isProc = isproc;
            if (this.isProc)
            {
                this.groupBox2.Text = "选择存储过程";
            }
        }

        private void TemplateBatch_Load(object sender, EventArgs e)
        {
            string b = "master";
            string dbType;
            switch (dbType = this.dbobj.DbType)
            {
                case "SQL2000":
                case "SQL2005":
                case "SQL2008":
                case "SQL2012":
                    b = "master";
                    break;
                case "Oracle":
                case "OleDb":
                    b = "";
                    this.label3.Visible = false;
                    this.cmbDB.Visible = false;
                    break;
                case "MySQL":
                    b = "mysql";
                    break;
                case "SQLite":
                    b = "sqlite_master";
                    break;
            }
            if (this.dbset.DbName == "" || this.dbset.DbName == b)
            {
                List<string> dBList = this.dbobj.GetDBList();
                if (dBList == null || dBList.Count <= 0)
                {
                    goto IL_19B;
                }
                using (List<string>.Enumerator enumerator = dBList.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        string current = enumerator.Current;
                        this.cmbDB.Items.Add(current);
                    }
                    goto IL_19B;
                }
            }
            this.cmbDB.Items.Add(this.dbset.DbName);
        IL_19B:
            if (this.cmbDB.Items.Count > 0)
            {
                this.cmbDB.SelectedIndex = 0;
            }
            else
            {
                this.listTable1.Items.Clear();
                this.listTable2.Items.Clear();
                List<string> list;
                if (this.isProc)
                {
                    list = this.dbobj.GetProcs("");
                }
                else
                {
                    list = this.dbobj.GetTableViews("");
                }
                if (list.Count > 0)
                {
                    foreach (string current2 in list)
                    {
                        this.listTable1.Items.Add(current2);
                    }
                }
            }
            this.btn_Export.Enabled = false;
            if (File.Exists(this.cmcfgfile))
            {
                this.cfgfile = new INIFile(this.cmcfgfile);
                string text = this.cfgfile.IniReadValue("Project", "lastpath");
                if (text.Trim() != "")
                {
                    this.txtTargetFolder.Text = text;
                }
            }
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
            this.CheckTemplate();
            this.IsHasItem();
        }
        private void cmbDB_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text = this.cmbDB.Text;
            List<string> list;
            if (this.isProc)
            {
                list = this.dbobj.GetProcs(text);
            }
            else
            {
                list = this.dbobj.GetTableViews(text);
            }
            this.listTable1.Items.Clear();
            this.listTable2.Items.Clear();
            if (list.Count > 0)
            {
                foreach (string current in list)
                {
                    this.listTable1.Items.Add(current);
                }
            }
            this.IsHasItem();
        }
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
                if ((files[j].Extension == ".tt" || files[j].Extension == ".cmt" || files[j].Extension == ".aspx" || files[j].Extension == ".cs" || files[j].Extension == ".vb") && !("temp~.cmt" == files[j].Name))
                {
                    TempNode tempNode2 = new TempNode(files[j].Name);
                    tempNode2.FilePath = files[j].FullName;
                    string extension;
                    if ((extension = files[j].Extension) == null)
                    {
                        goto IL_231;
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
                                        goto IL_231;
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
                IL_241:
                    parentnode.Nodes.Add(tempNode2);
                    goto IL_24F;
                IL_231:
                    tempNode2.ImageIndex = 2;
                    tempNode2.SelectedImageIndex = 2;
                    goto IL_241;
                }
            IL_24F: ;
            }
        }
        private void btn_Addlist_Click(object sender, EventArgs e)
        {
            int count = this.listTable1.Items.Count;
            for (int i = 0; i < count; i++)
            {
                this.listTable2.Items.Add(this.listTable1.Items[i]);
            }
            this.listTable1.Items.Clear();
            this.IsHasItem();
        }
        private void btn_Add_Click(object sender, EventArgs e)
        {
            int count = this.listTable1.SelectedItems.Count;
            ListBox.SelectedObjectCollection arg_1C_0 = this.listTable1.SelectedItems;
            for (int i = 0; i < count; i++)
            {
                this.listTable2.Items.Add(this.listTable1.SelectedItems[i]);
            }
            for (int j = 0; j < count; j++)
            {
                if (this.listTable1.SelectedItems.Count > 0)
                {
                    this.listTable1.Items.Remove(this.listTable1.SelectedItems[0]);
                }
            }
            this.IsHasItem();
        }
        private void btn_Del_Click(object sender, EventArgs e)
        {
            int count = this.listTable2.SelectedItems.Count;
            for (int i = 0; i < count; i++)
            {
                this.listTable1.Items.Add(this.listTable2.SelectedItems[i]);
            }
            for (int j = 0; j < count; j++)
            {
                if (this.listTable2.SelectedItems.Count > 0)
                {
                    this.listTable2.Items.Remove(this.listTable2.SelectedItems[0]);
                }
            }
            this.IsHasItem();
        }
        private void btn_Dellist_Click(object sender, EventArgs e)
        {
            int count = this.listTable2.Items.Count;
            for (int i = 0; i < count; i++)
            {
                this.listTable1.Items.Add(this.listTable2.Items[i]);
            }
            this.listTable2.Items.Clear();
            this.IsHasItem();
        }
        private void listTable1_DoubleClick(object sender, EventArgs e)
        {
            if (this.listTable1.SelectedItem != null)
            {
                this.listTable2.Items.Add(this.listTable1.SelectedItem);
                this.listTable1.Items.Remove(this.listTable1.SelectedItem);
                this.IsHasItem();
            }
        }
        private void listTable2_DoubleClick(object sender, EventArgs e)
        {
            if (this.listTable2.SelectedItem != null)
            {
                this.listTable1.Items.Add(this.listTable2.SelectedItem);
                this.listTable2.Items.Remove(this.listTable2.SelectedItem);
                this.IsHasItem();
            }
        }
        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            TempNode tempNode = (TempNode)this.treeView1.SelectedNode;
            if (tempNode == null)
            {
                return;
            }
            string arg_1B_0 = tempNode.NodeID;
            string arg_22_0 = tempNode.Text;
            string filePath = tempNode.FilePath;
            string nodeType = tempNode.NodeType;
            string a;
            if ((a = nodeType) != null && (a == "tt" || a == "cmt" || a == "vb" || a == "aspx" || a == "cs"))
            {
                if (filePath.Trim() != "")
                {
                    string text = filePath.Replace(this.TemplateFolder + "\\", "");
                    if (!this.hasTheFile(text))
                    {
                        this.listBoxTemplate.Items.Add(text);
                    }
                }
                else
                {
                    MessageBox.Show("所选文件已经不存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            this.CheckTemplate();
        }
        private void btnAddTemp_Click(object sender, EventArgs e)
        {
            this.treeView1_DoubleClick(null, null);
        }
        private void btnRemoveTemp_Click(object sender, EventArgs e)
        {
            int count = this.listBoxTemplate.SelectedItems.Count;
            for (int i = 0; i < count; i++)
            {
                if (this.listBoxTemplate.SelectedItems.Count > 0)
                {
                    this.listBoxTemplate.Items.Remove(this.listBoxTemplate.SelectedItems[0]);
                }
            }
            this.CheckTemplate();
        }
        private void btnClearTemp_Click(object sender, EventArgs e)
        {
            this.listBoxTemplate.Items.Clear();
            this.CheckTemplate();
        }
        private void IsHasItem()
        {
            if (this.listTable1.Items.Count > 0)
            {
                this.btn_Add.Enabled = true;
                this.btn_Addlist.Enabled = true;
            }
            else
            {
                this.btn_Add.Enabled = false;
                this.btn_Addlist.Enabled = false;
            }
            if (this.listTable2.Items.Count > 0)
            {
                this.btn_Del.Enabled = true;
                this.btn_Dellist.Enabled = true;
                if (this.listBoxTemplate.Items.Count > 0)
                {
                    this.btn_Export.Enabled = true;
                    this.SetlblStatuText("当前选择：" + this.listTable2.Items.Count.ToString());
                    return;
                }
            }
            else
            {
                this.btn_Del.Enabled = false;
                this.btn_Dellist.Enabled = false;
                this.btn_Export.Enabled = false;
            }
        }
        private void CheckTemplate()
        {
            if (this.listBoxTemplate.Items.Count > 0)
            {
                this.btnRemoveTemp.Enabled = true;
                this.btnClearTemp.Enabled = true;
                this.btn_Export.Enabled = true;
                return;
            }
            this.btnRemoveTemp.Enabled = false;
            this.btnClearTemp.Enabled = false;
            this.btn_Export.Enabled = false;
        }
        private bool hasTheFile(string tempFilename)
        {
            bool result = false;
            foreach (object current in this.listBoxTemplate.Items)
            {
                if (current.ToString() == tempFilename)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
        public void SetBtnEnable()
        {
            if (this.btn_Export.InvokeRequired)
            {
                TemplateBatch.SetBtnEnableCallback method = new TemplateBatch.SetBtnEnableCallback(this.SetBtnEnable);
                base.Invoke(method, null);
                return;
            }
            this.btn_Export.Enabled = true;
            this.btn_Cancle.Enabled = true;
        }
        public void SetBtnDisable()
        {
            if (this.btn_Export.InvokeRequired)
            {
                TemplateBatch.SetBtnDisableCallback method = new TemplateBatch.SetBtnDisableCallback(this.SetBtnDisable);
                base.Invoke(method, null);
                return;
            }
            this.btn_Export.Enabled = false;
            this.btn_Cancle.Enabled = false;
        }
        public void SetlblStatuText(string text)
        {
            if (this.labelNum.InvokeRequired)
            {
                TemplateBatch.SetlblStatuCallback method = new TemplateBatch.SetlblStatuCallback(this.SetlblStatuText);
                base.Invoke(method, new object[]
				{
					text
				});
                return;
            }
            this.labelNum.Text = text;
        }
        public void SetprogressBar1Max(int val)
        {
            if (this.progressBar1.InvokeRequired)
            {
                TemplateBatch.SetProBar1MaxCallback method = new TemplateBatch.SetProBar1MaxCallback(this.SetprogressBar1Max);
                base.Invoke(method, new object[]
				{
					val
				});
                return;
            }
            this.progressBar1.Maximum = val;
        }
        public void SetprogressBar1Val(int val)
        {
            if (this.progressBar1.InvokeRequired)
            {
                TemplateBatch.SetProBar1ValCallback method = new TemplateBatch.SetProBar1ValCallback(this.SetprogressBar1Val);
                base.Invoke(method, new object[]
				{
					val
				});
                return;
            }
            this.progressBar1.Value = val;
        }
        private void btn_Cancle_Click(object sender, EventArgs e)
        {
            base.Close();
        }
        private void btn_Export_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txtTargetFolder.Text.Trim() == "")
                {
                    MessageBox.Show("目标文件夹为空！");
                }
                else
                {
                    string text = this.txtTargetFolder.Text;
                    if (!Directory.Exists(text))
                    {
                        Directory.CreateDirectory(text);
                    }
                    this.cfgfile.IniWriteValue("Project", "lastpath", this.txtTargetFolder.Text.Trim());
                    this.dbname = this.cmbDB.Text;
                    this.pictureBox1.Visible = true;
                    this.mythread = new Thread(new ThreadStart(this.ThreadWork));
                    this.mythread.Start();
                }
            }
            catch (Exception ex)
            {
                LogInfo.WriteLog(ex);
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
        private void btn_TargetFold_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult dialogResult = folderBrowserDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                this.txtTargetFolder.Text = folderBrowserDialog.SelectedPath;
            }
        }
        private void ThreadWork()
        {
            try
            {
                this.SetBtnDisable();
                int count = this.listTable2.Items.Count;
                int count2 = this.listBoxTemplate.Items.Count;
                this.SetlblStatuText("0");
                this.dbset.Folder = this.txtFolder.Text.Trim();
                DataTable tablesExProperty = this.dbobj.GetTablesExProperty(this.dbname);
                CodeInfo codeInfo = null;
                bool flag = false;
                if (this.radbtn_TempMerger.Checked)
                {
                    this.SetprogressBar1Max(count2);
                    this.SetprogressBar1Val(1);
                    for (int i = 0; i < count2; i++)
                    {
                        string text = this.listBoxTemplate.Items[i].ToString();
                        string text2 = this.TemplateFolder + "\\" + text;
                        if (File.Exists(text2))
                        {
                            StringBuilder stringBuilder = new StringBuilder();
                            string objectype = this.isProc ? "proc" : "table";
                            for (int j = 0; j < count; j++)
                            {
                                string text3 = this.listTable2.Items[j].ToString();
                                string tableDescription = text3;
                                if (tablesExProperty != null)
                                {
                                    try
                                    {
                                        DataRow[] array = tablesExProperty.Select("objname='" + text3 + "'");
                                        if (array.Length > 0 && array[0]["value"] != null)
                                        {
                                            tableDescription = array[0]["value"].ToString();
                                        }
                                    }
                                    catch
                                    {
                                    }
                                }
                                List<ColumnInfo> fieldlist;
                                if (this.isProc)
                                {
                                    fieldlist = this.dbobj.GetColumnList(this.dbname, text3);
                                }
                                else
                                {
                                    fieldlist = this.dbobj.GetColumnInfoList(this.dbname, text3);
                                }
                                List<ColumnInfo> keyList = this.dbobj.GetKeyList(this.dbname, text3);
                                List<ColumnInfo> fKeyList = this.dbobj.GetFKeyList(this.dbname, text3);
                                try
                                {
                                    codeInfo = new CodeInfo();
                                    BuilderTemp builderTemp = new BuilderTemp(this.dbobj, this.dbname, text3, tableDescription, fieldlist, keyList, fKeyList, text2, this.dbset, objectype);
                                    codeInfo = builderTemp.GetCode();
                                    if (codeInfo.ErrorMsg != null && codeInfo.ErrorMsg.Length > 0)
                                    {
                                        flag = true;
                                        if (codeInfo.ErrorMsg.IndexOf("无法将类型为“Maticsoft.CodeEngine.TableHost”的对象强制转换为类型“Maticsoft.CodeEngine.ProcedureHost”") > 0)
                                        {
                                            stringBuilder.AppendLine(codeInfo.Code + Environment.NewLine + "/*代码生成时出现错误: 无法将表和视图与存储过程的模板进行匹配，请选择正确的模板！*/");
                                        }
                                        else
                                        {
                                            if (codeInfo.ErrorMsg.IndexOf("无法将类型为“Maticsoft.CodeEngine.ProcedureHost”的对象强制转换为类型“Maticsoft.CodeEngine.TableHost”") > 0)
                                            {
                                                stringBuilder.AppendLine(codeInfo.Code + Environment.NewLine + "/*代码生成时出现错误: 无法将存储过程与表和视图的模板进行匹配，请选择正确的模板！*/");
                                            }
                                            else
                                            {
                                                stringBuilder.AppendLine(string.Concat(new string[]
												{
													codeInfo.Code,
													Environment.NewLine,
													"/*------ 代码生成时出现错误: ------",
													Environment.NewLine,
													codeInfo.ErrorMsg,
													"*/"
												}));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        stringBuilder.AppendLine(codeInfo.Code);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    flag = true;
                                    stringBuilder.AppendLine(ex.Message);
                                }
                            }
                            this.SetprogressBar1Val(i + 1);
                            this.SetlblStatuText((i + 1).ToString());
                            string path = Path.Combine(this.txtTargetFolder.Text, text.ToString());
                            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
                            string text4 = this.txtTargetFolder.Text;
                            if (!Directory.Exists(text4))
                            {
                                Directory.CreateDirectory(text4);
                            }
                            if (this.isProc)
                            {
                                string path2 = Path.Combine(text4, this.dbname + codeInfo.FileExtension);
                                File.AppendAllText(path2, stringBuilder.ToString(), Encoding.UTF8);
                            }
                            else
                            {
                                string path3 = Path.Combine(text4, fileNameWithoutExtension + codeInfo.FileExtension);
                                File.WriteAllText(path3, stringBuilder.ToString(), Encoding.UTF8);
                            }
                        }
                    }
                }
                else
                {
                    this.SetprogressBar1Max(count);
                    this.SetprogressBar1Val(1);
                    for (int k = 0; k < count; k++)
                    {
                        string text5 = this.listTable2.Items[k].ToString();
                        string tableDescription2 = text5;
                        if (tablesExProperty != null)
                        {
                            try
                            {
                                DataRow[] array2 = tablesExProperty.Select("objname='" + text5 + "'");
                                if (array2.Length > 0 && array2[0]["value"] != null)
                                {
                                    tableDescription2 = array2[0]["value"].ToString();
                                }
                            }
                            catch
                            {
                            }
                        }
                        List<ColumnInfo> fieldlist2;
                        if (this.isProc)
                        {
                            fieldlist2 = this.dbobj.GetColumnList(this.dbname, text5);
                        }
                        else
                        {
                            fieldlist2 = this.dbobj.GetColumnInfoList(this.dbname, text5);
                        }
                        List<ColumnInfo> keyList2 = this.dbobj.GetKeyList(this.dbname, text5);
                        List<ColumnInfo> fKeyList2 = this.dbobj.GetFKeyList(this.dbname, text5);
                        foreach (object current in this.listBoxTemplate.Items)
                        {
                            string text6 = this.TemplateFolder + "\\" + current.ToString();
                            if (File.Exists(text6))
                            {
                                string text7 = "";
                                string objectype2 = this.isProc ? "proc" : "table";
                                try
                                {
                                    codeInfo = new CodeInfo();
                                    BuilderTemp builderTemp2 = new BuilderTemp(this.dbobj, this.dbname, text5, tableDescription2, fieldlist2, keyList2, fKeyList2, text6, this.dbset, objectype2);
                                    codeInfo = builderTemp2.GetCode();
                                    if (codeInfo.ErrorMsg != null && codeInfo.ErrorMsg.Length > 0)
                                    {
                                        flag = true;
                                        if (codeInfo.ErrorMsg.IndexOf("无法将类型为“Maticsoft.CodeEngine.TableHost”的对象强制转换为类型“Maticsoft.CodeEngine.ProcedureHost”") > 0)
                                        {
                                            text7 = codeInfo.Code + Environment.NewLine + "/*代码生成时出现错误: 无法将表和视图与存储过程的模板进行匹配，请选择正确的模板！*/";
                                        }
                                        else
                                        {
                                            if (codeInfo.ErrorMsg.IndexOf("无法将类型为“Maticsoft.CodeEngine.ProcedureHost”的对象强制转换为类型“Maticsoft.CodeEngine.TableHost”") > 0)
                                            {
                                                text7 = codeInfo.Code + Environment.NewLine + "/*代码生成时出现错误: 无法将存储过程与表和视图的模板进行匹配，请选择正确的模板！*/";
                                            }
                                            else
                                            {
                                                text7 = string.Concat(new string[]
												{
													codeInfo.Code,
													Environment.NewLine,
													"/*------ 代码生成时出现错误: ------",
													Environment.NewLine,
													codeInfo.ErrorMsg,
													"*/"
												});
                                            }
                                        }
                                    }
                                    else
                                    {
                                        text7 = codeInfo.Code;
                                    }
                                }
                                catch (Exception ex2)
                                {
                                    flag = true;
                                    text7 += ex2.Message;
                                }
                                string path4 = Path.Combine(this.txtTargetFolder.Text, current.ToString());
                                string fileNameWithoutExtension2 = Path.GetFileNameWithoutExtension(path4);
                                string text8 = Path.Combine(Path.GetDirectoryName(path4), fileNameWithoutExtension2);
                                if (!Directory.Exists(text8))
                                {
                                    Directory.CreateDirectory(text8);
                                }
                                if (this.isProc)
                                {
                                    string path5 = Path.Combine(text8, this.dbname + codeInfo.FileExtension);
                                    File.AppendAllText(path5, text7, Encoding.UTF8);
                                }
                                else
                                {
                                    string path6 = Path.Combine(text8, text5 + codeInfo.FileExtension);
                                    File.WriteAllText(path6, text7, Encoding.UTF8);
                                }
                            }
                        }
                        this.SetprogressBar1Val(k + 1);
                        this.SetlblStatuText((k + 1).ToString());
                    }
                }
                this.SetBtnEnable();
                if (flag)
                {
                    MessageBox.Show(this, "代码生成完成，但有错误，请查看生成代码！", "完成", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    MessageBox.Show(this, "代码生成成功！", "完成", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
            catch (Exception ex3)
            {
                LogInfo.WriteLog(ex3);
                MessageBox.Show(ex3.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
        private void WriteFile(string Filename, string strCode)
        {
            StreamWriter streamWriter = new StreamWriter(Filename, false, Encoding.UTF8);
            streamWriter.Write(strCode);
            streamWriter.Flush();
            streamWriter.Close();
        }
        private void FolderCheck(string Folder)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Folder);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
        }
        private void AddClassFile(string ProjectFile, string classFileName, string ProType)
        {
            if (File.Exists(ProjectFile))
            {
                if (ProType != null)
                {
                    if (ProType == "2003")
                    {
                        this.vsp.AddClass2003(ProjectFile, classFileName);
                        return;
                    }
                    if (ProType == "2005")
                    {
                        this.vsp.AddClass2005(ProjectFile, classFileName);
                        return;
                    }
                }
                this.vsp.AddClass(ProjectFile, classFileName);
            }
        }
        public void CheckDirectory(string SourceDirectory)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(SourceDirectory);
            if (!directoryInfo.Exists)
            {
                return;
            }
            FileInfo[] files = directoryInfo.GetFiles();
            int num = files.Length;
            for (int i = 0; i < num; i++)
            {
                string extension;
                switch (extension = files[i].Extension)
                {
                }
            }
            DirectoryInfo[] directories = directoryInfo.GetDirectories();
            for (int j = 0; j < directories.Length; j++)
            {
                this.CheckDirectory(directories[j].FullName);
            }
        }
        private void ReplaceNamespace(string filename, string spacename)
        {
            StreamReader streamReader = new StreamReader(filename, Encoding.Default);
            string text = streamReader.ReadToEnd();
            streamReader.Close();
            text = text.Replace("<$$namespace$$>", spacename);
            StreamWriter streamWriter = new StreamWriter(filename, false, Encoding.UTF8);
            streamWriter.Write(text);
            streamWriter.Flush();
            streamWriter.Close();
        }
        private void ReplaceNamespaceProj(string filename, string spacename)
        {
            StreamReader streamReader = new StreamReader(filename, Encoding.Default);
            string text = streamReader.ReadToEnd();
            streamReader.Close();
            text = text.Replace("<AssemblyName>Maticsoft.", "<AssemblyName>" + spacename + ".");
            text = text.Replace("<RootNamespace>Maticsoft.", "<RootNamespace>" + spacename + ".");
            StreamWriter streamWriter = new StreamWriter(filename, false, Encoding.UTF8);
            streamWriter.Write(text);
            streamWriter.Flush();
            streamWriter.Close();
        }
        private string CutTableDescription(string TableName, string TableDescription)
        {
            string result;
            if (TableDescription.Trim().Length > 0)
            {
                int val = TableDescription.IndexOf(";");
                int val2 = TableDescription.IndexOf("，");
                int val3 = TableDescription.IndexOf(",");
                int num = Math.Min(val, val2);
                if (num < 0)
                {
                    num = Math.Max(val, val2);
                }
                num = Math.Min(num, val3);
                if (num < 0)
                {
                    num = Math.Max(val, val2);
                }
                if (num > 0)
                {
                    result = TableDescription.Trim().Substring(0, num);
                }
                else
                {
                    if (TableDescription.Trim().Length > 10)
                    {
                        result = TableDescription.Trim().Substring(0, 10);
                    }
                    else
                    {
                        result = TableDescription.Trim();
                    }
                }
            }
            else
            {
                result = TableName;
            }
            return result;
        }
        private void btnInputTxt_Click(object sender, EventArgs e)
        {
            CodeExpTabInput codeExpTabInput = new CodeExpTabInput();
            DialogResult dialogResult = codeExpTabInput.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                this.listTable2.Items.Clear();
                string[] lines = codeExpTabInput.txtTableList.Lines;
                for (int i = 0; i < lines.Length; i++)
                {
                    string item = lines[i];
                    this.listTable2.Items.Add(item);
                }
            }
            this.IsHasItem();
        }
    }
}
