using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Collections;
using Maticsoft.CodeBuild;
using System.Threading;
using Maticsoft.CodeHelper;
using Maticsoft.Utility;
using Codematic.UserControls;
using Maticsoft.CmConfig;
namespace Codematic
{
    /// <summary>
    /// 模板代码生成
    /// </summary>
    public partial class CodeTemplate : Form
    {
        private string Templatefilename;
        private string objtype = "table";
        private string tableDescription = "";
        MainForm mainfrm;
        private DbSettings dbset;
        Maticsoft.IDBO.IDbObject dbobj;
        string servername;
        string dbname;
        string tablename;
        private Thread thread;
        //private Thread threadCode;
        delegate void SetListCallback();
        public UcCodeView codeview;

        public CodeTemplate()
        {
            InitializeComponent();
        }
        public CodeTemplate(Form mdiParentForm)
        {
            InitializeComponent();
            this.codeview = new UcCodeView();
            this.tabPage2.Controls.Add(this.codeview);
            this.mainfrm = (MainForm)mdiParentForm;
            this.CreatView();
            this.mainfrm.toolBtn_Run.Visible = true;
            DbView dbView = (DbView)Application.OpenForms["DbView"];
            if (dbView != null)
            {
                this.SetListView(dbView);
            }
        }


        #region 设置listview

        void Showlistview()
        {
            DbView dbView = (DbView)Application.OpenForms["DbView"];
            if (dbView.treeView1.InvokeRequired)
            {
                CodeTemplate.SetListCallback method = new CodeTemplate.SetListCallback(this.Showlistview);
                dbView.Invoke(method, null);
                return;
            }
            this.SetListView(dbView);
        }
        public void SetListView(DbView dbviewfrm)
        {
            #region 得到类型对象            
            TreeNode selectedNode = dbviewfrm.treeView1.SelectedNode;
            if (selectedNode == null)
            {
                return;
            }
            this.objtype = selectedNode.Tag.ToString();
            string a;
            if ((a = selectedNode.Tag.ToString()) != null)
            {
                if (a == "table" || a == "view")
                {
                    this.servername = selectedNode.Parent.Parent.Parent.Text;
                    this.dbname = selectedNode.Parent.Parent.Text;
                    this.tablename = selectedNode.Text;
                    this.dbobj = ObjHelper.CreatDbObj(this.servername);
                    this.BindlistViewCol(this.dbname, this.tablename);
                    goto IL_1B7;
                }
                if (a == "column")
                {
                    this.servername = selectedNode.Parent.Parent.Parent.Parent.Text;
                    this.dbname = selectedNode.Parent.Parent.Parent.Text;
                    this.tablename = selectedNode.Parent.Text;
                    this.dbobj = ObjHelper.CreatDbObj(this.servername);
                    this.BindlistViewCol(this.dbname, this.tablename);
                    goto IL_1B7;
                }
                if (a == "proc")
                {
                    this.servername = selectedNode.Parent.Parent.Parent.Text;
                    this.dbname = selectedNode.Parent.Parent.Text;
                    this.tablename = selectedNode.Text;
                    this.dbobj = ObjHelper.CreatDbObj(this.servername);
                    this.BindlistProcCol(this.dbname, this.tablename);
                    goto IL_1B7;
                }
            }
            this.listView1.Items.Clear();
        IL_1B7:
            this.GetTableDesc();
            if (this.servername != null && this.servername.Length > 1)
            {
                this.dbset = DbConfig.GetSetting(this.servername);
            }
            #endregion

        }

        #endregion

        #region 设置模版文本框
        public void SettxtTemplate(string Filename)
        {
            try
            {
                this.Templatefilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Filename);
                StreamReader streamReader = new StreamReader(this.Templatefilename, Encoding.Default);
                string text = streamReader.ReadToEnd();
                streamReader.Close();
                this.txtTemplate.Text = text;
                this.groupBox1.Text = this.Templatefilename;
            }
            catch (Exception ex)
            {
                this.txtTemplate.Text = "加载模板失败:" + ex.Message;
            }
        }
        #endregion

        #region  为listView邦定 列 数据

        private void CreatView()
        {
            //创建列表
            this.listView1.Columns.Clear();
            this.listView1.Items.Clear();
            this.listView1.LargeImageList = this.imglistView;
            this.listView1.SmallImageList = this.imglistView;
            this.listView1.View = View.Details;
            this.listView1.GridLines = true;
            this.listView1.CheckBoxes = true;
            this.listView1.FullRowSelect = true;
            this.listView1.Columns.Add("序号", 60, HorizontalAlignment.Center);
            this.listView1.Columns.Add("列名", 110, HorizontalAlignment.Left);
            this.listView1.Columns.Add("数据类型", 80, HorizontalAlignment.Left);
            this.listView1.Columns.Add("长度", 40, HorizontalAlignment.Left);
            this.listView1.Columns.Add("小数", 40, HorizontalAlignment.Left);
            this.listView1.Columns.Add("标识", 40, HorizontalAlignment.Center);
            this.listView1.Columns.Add("主键", 40, HorizontalAlignment.Center);
            this.listView1.Columns.Add("允许空", 60, HorizontalAlignment.Center);
            this.listView1.Columns.Add("默认值", 100, HorizontalAlignment.Left);
            //listView1.Columns.Add("字段说明", 100, HorizontalAlignment.Left);
        }

        public void GetTableDesc()
        {
            if (this.dbobj != null)
            {
                DataTable tablesExProperty = this.dbobj.GetTablesExProperty(this.dbname);
                if (tablesExProperty != null)
                {
                    try
                    {
                        this.tableDescription = this.tablename;
                        DataRow[] array = tablesExProperty.Select("objname='" + this.tablename + "'");
                        if (array.Length > 0 && array[0]["value"] != null)
                        {
                            this.tableDescription = array[0]["value"].ToString();
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void BindlistViewCol(string Dbname, string TableName)
        {
            List<ColumnInfo> columnInfoList = this.dbobj.GetColumnInfoList(Dbname, TableName);
            if (columnInfoList != null && columnInfoList.Count > 0)
            {
                this.listView1.Items.Clear();
                this.list_KeyField.Items.Clear();
                foreach (ColumnInfo current in columnInfoList)
                {
                    string columnOrder = current.ColumnOrder;
                    string columnName = current.ColumnName;
                    string typeName = current.TypeName;
                    string length = current.Length;
                    string arg_79_0 = current.Precision;
                    string scale = current.Scale;
                    string defaultVal = current.DefaultVal;
                    string arg_90_0 = current.Description;
                    string text = current.IsIdentity ? "√" : "";
                    string text2 = current.IsPrimaryKey ? "√" : "";
                    string text3 = current.Nullable ? "√" : "";
                    ListViewItem listViewItem = new ListViewItem(columnOrder, 0);
                    listViewItem.ImageIndex = 4;
                    listViewItem.SubItems.Add(columnName);
                    listViewItem.SubItems.Add(typeName);
                    listViewItem.SubItems.Add(length);
                    listViewItem.SubItems.Add(scale);
                    listViewItem.SubItems.Add(text);
                    if (text2 == "√" && text3.Trim() == "")
                    {
                        this.list_KeyField.Items.Add(columnName);
                    }
                    else
                    {
                        text2 = "";
                    }
                    listViewItem.SubItems.Add(text2);
                    listViewItem.SubItems.Add(text3);
                    listViewItem.SubItems.Add(defaultVal);
                    this.listView1.Items.AddRange(new ListViewItem[]
					{
						listViewItem
					});
                }
            }
            this.btn_SelAll_Click(null, null);         
        }

        private void BindlistProcCol(string Dbname, string TableName)
        {
            List<ColumnInfo> columnList = this.dbobj.GetColumnList(Dbname, TableName);
            if (columnList != null && columnList.Count > 0)
            {
                this.listView1.Items.Clear();
                this.list_KeyField.Items.Clear();
                foreach (ColumnInfo current in columnList)
                {
                    string columnOrder = current.ColumnOrder;
                    string columnName = current.ColumnName;
                    string typeName = current.TypeName;
                    string length = current.Length;
                    string arg_79_0 = current.Precision;
                    string scale = current.Scale;
                    string arg_88_0 = current.DefaultVal;
                    string description = current.Description;
                    string text = current.IsIdentity ? "√" : "";
                    string text2 = current.IsPrimaryKey ? "√" : "";
                    string text3 = current.Nullable ? "√" : "";
                    ListViewItem listViewItem = new ListViewItem(columnOrder, 0);
                    listViewItem.ImageIndex = 4;
                    listViewItem.SubItems.Add(columnName);
                    listViewItem.SubItems.Add(typeName);
                    listViewItem.SubItems.Add(length);
                    listViewItem.SubItems.Add(scale);
                    listViewItem.SubItems.Add(text);
                    if (text2 == "√" && text3.Trim() == "")
                    {
                        this.list_KeyField.Items.Add(columnName);
                    }
                    else
                    {
                        text2 = "";
                    }
                    listViewItem.SubItems.Add(text2);
                    listViewItem.SubItems.Add(text3);
                    listViewItem.SubItems.Add(description);
                    this.listView1.Items.AddRange(new ListViewItem[]
					{
						listViewItem
					});
                }
            }
            this.btn_SelAll_Click(null, null);
        }
        #endregion

        #region 选择字段按钮
        private void btn_SelAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                if (!item.Checked)
                {
                    item.Checked = true;
                }
            }
        }

        private void btn_SelI_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                item.Checked = !item.Checked;
            }
        }

        private void btn_SelClear_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                item.Checked = false;
            }
        }
        #endregion

        #region  公共方法

        //设定主键的listbox
        private void btn_SetKey_Click(object sender, EventArgs e)
        {
            this.list_KeyField.Items.Clear();
            foreach (ListViewItem listViewItem in this.listView1.Items)
            {
                if (listViewItem.Checked)
                {
                    this.list_KeyField.Items.Add(listViewItem.SubItems[1].Text);
                }
            }

        }

        ////得到主键的哈西表
        //private Hashtable GetKeyFields()
        //{
        //    Hashtable keys = new Hashtable();
        //    foreach (object obj in this.list_KeyField.Items)
        //    {
        //        string str = obj.ToString();
        //        int n = str.IndexOf("(");
        //        string k = str.Substring(0, n);
        //        string v = str.Substring(n + 1, str.Length - k.Length - 2);
        //        keys.Add(k, v);
        //    }
        //    return keys;
        //}

        ////得到选择的字段集合
        //private ArrayList GetFieldlist()
        //{
        //    ArrayList fieldlist = new ArrayList();
        //    foreach (ListViewItem item in listView1.Items)
        //    {
        //        if (item.Checked)
        //        {
        //            fieldlist.Add(item.SubItems[1].Text);
        //        }
        //    }
        //    return fieldlist;
        //}

        private void SettxtContent(string Type, string strContent)
        {
            this.codeview.SettxtContent(Type, strContent);
            this.tabControl1.SelectedIndex = 1;
        }

        /// <summary>
        /// 得到主键的对象信息
        /// </summary>
        /// <returns></returns>
        private List<ColumnInfo> GetKeyFields()
        {
            List<ColumnInfo> columnInfoList = this.dbobj.GetColumnInfoList(this.dbname, this.tablename);
            DataTable columnInfoDt = CodeCommon.GetColumnInfoDt(columnInfoList);
            StringPlus stringPlus = new StringPlus();
            foreach (object current in this.list_KeyField.Items)
            {
                stringPlus.Append("'" + current.ToString() + "',");
            }
            stringPlus.DelLastComma();
            if (columnInfoDt != null)
            {
                DataRow[] array;
                if (stringPlus.Value.Length > 0)
                {
                    array = columnInfoDt.Select("ColumnName in (" + stringPlus.Value + ")", "colorder asc");
                }
                else
                {
                    array = columnInfoDt.Select();
                }
                List<ColumnInfo> list = new List<ColumnInfo>();
                DataRow[] array2 = array;
                for (int i = 0; i < array2.Length; i++)
                {
                    DataRow dataRow = array2[i];
                    string columnOrder = dataRow["Colorder"].ToString();
                    string columnName = dataRow["ColumnName"].ToString();
                    string typeName = dataRow["TypeName"].ToString();
                    string a = dataRow["IsIdentity"].ToString();
                    string a2 = dataRow["IsPK"].ToString();
                    string length = dataRow["Length"].ToString();
                    string precision = dataRow["Preci"].ToString();
                    string scale = dataRow["Scale"].ToString();
                    string a3 = dataRow["cisNull"].ToString();
                    string defaultVal = dataRow["DefaultVal"].ToString();
                    string description = dataRow["DeText"].ToString();
                    list.Add(new ColumnInfo
                    {
                        ColumnOrder = columnOrder,
                        ColumnName = columnName,
                        TypeName = typeName,
                        IsIdentity = a == "√",
                        IsPrimaryKey = a2 == "√",
                        Length = length,
                        Precision = precision,
                        Scale = scale,
                        Nullable = a3 == "√",
                        DefaultVal = defaultVal,
                        Description = description
                    });
                }
                return list;
            }
            return null;
        }

        private List<ColumnInfo> GetFKeyFields()
        {
            return this.dbobj.GetFKeyList(this.dbname, this.tablename);
        }

        /// <summary>
        /// 得到选择的字段集合
        /// </summary>
        /// <returns></returns>
        private List<ColumnInfo> GetFieldlist()
        {
            List<ColumnInfo> collist;
            if (this.objtype == "proc")
            {
                collist = this.dbobj.GetColumnList(this.dbname, this.tablename);
            }
            else
            {
                collist = this.dbobj.GetColumnInfoList(this.dbname, this.tablename);
            }
            DataTable columnInfoDt = CodeCommon.GetColumnInfoDt(collist);
            StringPlus stringPlus = new StringPlus();
            foreach (ListViewItem listViewItem in this.listView1.Items)
            {
                if (listViewItem.Checked)
                {
                    stringPlus.Append("'" + listViewItem.SubItems[1].Text + "',");
                }
            }
            stringPlus.DelLastComma();
            if (columnInfoDt != null)
            {
                DataRow[] array;
                if (stringPlus.Value.Length > 0)
                {
                    array = columnInfoDt.Select("ColumnName in (" + stringPlus.Value + ")", "colorder asc");
                }
                else
                {
                    array = columnInfoDt.Select();
                }
                List<ColumnInfo> list = new List<ColumnInfo>();
                DataRow[] array2 = array;
                for (int i = 0; i < array2.Length; i++)
                {
                    DataRow dataRow = array2[i];
                    string columnOrder = dataRow["Colorder"].ToString();
                    string columnName = dataRow["ColumnName"].ToString();
                    string typeName = dataRow["TypeName"].ToString();
                    string a = dataRow["IsIdentity"].ToString();
                    string a2 = dataRow["IsPK"].ToString();
                    string length = dataRow["Length"].ToString();
                    string precision = dataRow["Preci"].ToString();
                    string scale = dataRow["Scale"].ToString();
                    string a3 = dataRow["cisNull"].ToString();
                    string defaultVal = dataRow["DefaultVal"].ToString();
                    string description = dataRow["DeText"].ToString();
                    list.Add(new ColumnInfo
                    {
                        ColumnOrder = columnOrder,
                        ColumnName = columnName,
                        TypeName = typeName,
                        IsIdentity = a == "√",
                        IsPrimaryKey = a2 == "√",
                        Length = length,
                        Precision = precision,
                        Scale = scale,
                        Nullable = a3 == "√",
                        DefaultVal = defaultVal,
                        Description = description
                    });
                }
                return list;
            }
            return null;
        }


        #endregion

        #region 生成代码

        private void btn_Run_Click(object sender, EventArgs e)
        {
            try
            {
                Run();
                //threadCode = new Thread(new ThreadStart(Run));
                //threadCode.Start();
            }
            catch
            {
            }
        }

        public void Run()
        {
            this.StatusLabel1.Text = "正在生成...";
            try
            {
                string text = this.txtTemplate.Text;
                if (text.Trim() == "")
                {
                    MessageBox.Show("模版内容为空，请先在模版管理器里选择模版！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.StatusLabel1.ForeColor = System.Drawing.Color.Black;
                    this.StatusLabel1.Text = "就绪";
                    return;
                }
                if (this.Templatefilename == null || this.Templatefilename.Length == 0)
                {
                    this.Templatefilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Template\\TemplateFile\\temp~.cmt");
                }
                File.WriteAllText(this.Templatefilename, text, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                this.StatusLabel1.ForeColor = System.Drawing.Color.Red;
                this.StatusLabel1.Text = "模版格式有误：" + ex.Message;
                return;
            }
            string strContent = "";
            CodeInfo codeInfo = new CodeInfo();
            try
            {
                BuilderTemp builderTemp = new BuilderTemp(this.dbobj, this.dbname, this.tablename, this.tableDescription, this.GetFieldlist(), this.GetKeyFields(), this.GetFKeyFields(), this.Templatefilename, this.dbset, this.objtype);
                codeInfo = builderTemp.GetCode();
                if (codeInfo.ErrorMsg != null && codeInfo.ErrorMsg.Length > 0)
                {
                    strContent = string.Concat(new string[]
					{
						codeInfo.Code,
						Environment.NewLine,
						"/*------ 代码生成时出现错误: ------",
						Environment.NewLine,
						codeInfo.ErrorMsg,
						"*/"
					});
                }
                else
                {
                    strContent = codeInfo.Code;
                }
            }
            catch (Exception ex2)
            {
                this.StatusLabel1.ForeColor = System.Drawing.Color.Red;
                this.StatusLabel1.Text = "代码转换失败！" + ex2.Message;
                return;
            }
            this.SettxtContent(codeInfo.FileExtension.Replace(".", ""), strContent);
            this.tabControl1.SelectedIndex = 1;
            if (codeInfo.ErrorMsg != null && codeInfo.ErrorMsg.Length > 0)
            {
                this.StatusLabel1.ForeColor = System.Drawing.Color.Red;
                this.StatusLabel1.Text = "代码生成失败!";
                return;
            }
            this.StatusLabel1.ForeColor = System.Drawing.Color.Green;
            this.StatusLabel1.Text = "代码生成成功。";
        }

        #endregion

        private void txtTemplate_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                string s = sender.ToString();
            }
        }

        private void btnHidelist_Click(object sender, EventArgs e)
        {
            if (this.listView1.Visible)
            {
                this.listView1.Visible = false;
                this.btnHidelist.Text = "显示表格";
                return;
            }
            this.listView1.Visible = true;
            this.btnHidelist.Text = "隐藏表格";
        }
        private void menu_Copy_Click(object sender, EventArgs e)
        {
            string selectedText = this.txtTemplate.ActiveTextAreaControl.SelectionManager.SelectedText;
            Clipboard.SetDataObject(selectedText);
        }
        private void menu_Save_Click(object sender, EventArgs e)
        {
            try
            {
                string text = this.txtTemplate.Text;
                File.WriteAllText(this.Templatefilename, text, Encoding.UTF8);
                MessageBox.Show("保存成功!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败!" + ex.Message);
            }
        }
        private void menu_SaveAs_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "模板另存为";
                string text = this.txtTemplate.Text;
                DialogResult dialogResult = saveFileDialog.ShowDialog(this);
                if (dialogResult == DialogResult.OK)
                {
                    string fileName = saveFileDialog.FileName;
                    File.WriteAllText(fileName, text, Encoding.UTF8);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败!" + ex.Message);
            }
        }
    }
}