using Codematic.UserControls;
using Maticsoft.CmConfig;
using Maticsoft.CodeBuild;
using Maticsoft.CodeHelper;
using Maticsoft.IDBO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Codematic
{
    public partial class CodeMakerTran : Form
    {
        private delegate void SetListCallback();
        private UcCodeView codeview;
        private DbSettings dbset;
        private IDbObject dbobj;
        private CodeBuilders cb;
        private string servername;
        private string dbname;
        private string tablename;
        private DALTypeAddIn cm_daltype;
        private Thread thread;
        private List<string> selTab = new List<string>();
        public CodeMakerTran(string Dbname)
        {
            InitializeComponent();
            this.dbname = Dbname;
            this.codeview = new UcCodeView();
            this.tabPage2.Controls.Add(this.codeview);
            this.CreatView();
            DbView dbView = (DbView)Application.OpenForms["DbView"];
            if (dbView != null)
            {
                try
                {
                    this.thread = new Thread(new ThreadStart(this.Showlistview));
                    this.thread.Start();
                }
                catch
                {
                }
            }
        }
        private void SetFormConfig(string servername)
        {
            this.dbset = DbConfig.GetSetting(servername);
            this.txtNameSpace.Text = this.dbset.Namepace;
            this.txtNameSpace2.Text = this.dbset.Folder;
            this.cm_daltype = new DALTypeAddIn("Maticsoft.IBuilder.IBuilderDALMTran");
            this.cm_daltype.Title = "DAL";
            this.groupBox3.Controls.Add(this.cm_daltype);
            this.cm_daltype.Location = new Point(30, 70);
            this.cm_daltype.SetSelectedDALType(this.dbset.DALType.Trim());
            this.tabControl1.SelectedIndex = 0;
        }
        private void Showlistview()
        {
            DbView dbView = (DbView)Application.OpenForms["DbView"];
            if (dbView.treeView1.InvokeRequired)
            {
                CodeMakerTran.SetListCallback method = new CodeMakerTran.SetListCallback(this.Showlistview);
                dbView.Invoke(method, null);
                return;
            }
            this.SetListView(dbView);
        }
        public void SetListView(DbView dbviewfrm)
        {
            TreeNode selectedNode = dbviewfrm.treeView1.SelectedNode;
            if (selectedNode == null)
            {
                return;
            }
            string a;
            if ((a = selectedNode.Tag.ToString()) != null)
            {
                if (a == "server")
                {
                    this.servername = selectedNode.Text;
                    goto IL_1E6;
                }
                if (a == "table")
                {
                    this.servername = selectedNode.Parent.Parent.Parent.Text;
                    this.dbname = selectedNode.Parent.Parent.Text;
                    this.tablename = selectedNode.Text;
                    this.dbobj = ObjHelper.CreatDbObj(this.servername);
                    this.BindTablist(this.dbname);
                    goto IL_1E6;
                }
                if (a == "view")
                {
                    this.servername = selectedNode.Parent.Parent.Parent.Text;
                    this.dbname = selectedNode.Parent.Parent.Text;
                    this.tablename = selectedNode.Text;
                    this.dbobj = ObjHelper.CreatDbObj(this.servername);
                    this.BindTablist(this.dbname);
                    goto IL_1E6;
                }
                if (a == "column")
                {
                    this.servername = selectedNode.Parent.Parent.Parent.Parent.Text;
                    this.dbname = selectedNode.Parent.Parent.Parent.Text;
                    this.dbobj = ObjHelper.CreatDbObj(this.servername);
                    this.BindTablist(this.dbname);
                    goto IL_1E6;
                }
                if (a == "db")
                {
                    this.servername = selectedNode.Parent.Text;
                    this.dbname = selectedNode.Text;
                    this.dbobj = ObjHelper.CreatDbObj(this.servername);
                    this.BindTablist(this.dbname);
                    goto IL_1E6;
                }
            }
            this.listView1.Items.Clear();
        IL_1E6:
            if (this.dbobj != null)
            {
                this.cb = new CodeBuilders(this.dbobj);
                DataTable tablesExProperty = this.dbobj.GetTablesExProperty(this.dbname);
                if (tablesExProperty != null)
                {
                    try
                    {
                        DataRow[] array = tablesExProperty.Select("objname='" + this.tablename + "'");
                        if (array.Length > 0 && array[0]["value"] != null)
                        {
                            this.cb.TableDescription = array[0]["value"].ToString();
                        }
                    }
                    catch
                    {
                    }
                }
            }
            this.SetFormConfig(this.servername);
        }
        private void BindTablist(string Dbname)
        {
            List<TableInfo> tablesInfo = this.dbobj.GetTablesInfo(Dbname);
            if (tablesInfo != null && tablesInfo.Count > 0)
            {
                this.listTable.Items.Clear();
                foreach (TableInfo current in tablesInfo)
                {
                    string tabName = current.TabName;
                    this.listTable.Items.Add(tabName);
                }
            }
        }
        private void listTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listTable.SelectedItem != null && this.listTable.Text != "System.Data.DataRowView")
            {
                string tableName = this.listTable.SelectedItem.ToString();
                this.BindlistViewCol(this.dbname, tableName);
            }
        }
        private void CreatView()
        {
            this.listView1.Columns.Clear();
            this.listView1.Items.Clear();
            this.listView1.View = View.Details;
            this.listView1.GridLines = true;
            this.listView1.FullRowSelect = true;
            this.listView1.Columns.Add("表", 200, HorizontalAlignment.Left);
            this.listView1.Columns.Add("类名", 200, HorizontalAlignment.Left);
            this.listView1.Columns.Add("操作", 80, HorizontalAlignment.Left);
            this.listView1.Columns.Add("条件字段", 150, HorizontalAlignment.Left);
        }
        private void BindlistViewCol(string Dbname, string TableName)
        {
            List<ColumnInfo> columnInfoList = this.dbobj.GetColumnInfoList(Dbname, TableName);
            if (columnInfoList != null && columnInfoList.Count > 0)
            {
                this.cmbox_Field.Items.Clear();
                foreach (ColumnInfo current in columnInfoList)
                {
                    string arg_47_0 = current.ColumnOrder;
                    string columnName = current.ColumnName;
                    string arg_55_0 = current.TypeName;
                    this.cmbox_Field.Items.Add(columnName);
                }
                if (this.cmbox_Field.Items.Count > 0)
                {
                    this.cmbox_Field.SelectedIndex = 0;
                }
            }
        }
        private void 删除DToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in this.listView1.SelectedItems)
            {
                this.listView1.Items.Remove(item);
            }
        }
        private void radbtn_Click(object sender, EventArgs e)
        {
            if (this.radbtn_Insert.Checked)
            {
                this.label3.Visible = false;
                this.cmbox_Field.Visible = false;
                return;
            }
            this.label3.Visible = true;
            this.cmbox_Field.Visible = true;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.listTable.SelectedItem == null)
            {
                return;
            }
            string text = this.listTable.SelectedItem.ToString();
            string text2 = this.radbtn_Insert.Text;
            string text3 = "";
            if (this.radbtn_Update.Checked)
            {
                text2 = this.radbtn_Update.Text;
                text3 = this.cmbox_Field.Text;
            }
            if (this.radbtn_Delete.Checked)
            {
                text2 = this.radbtn_Delete.Text;
                text3 = this.cmbox_Field.Text;
            }
            if (this.selTab.Contains(text + text2))
            {
                return;
            }
            ListViewItem listViewItem = new ListViewItem(text, 0);
            listViewItem.Checked = true;
            listViewItem.ImageIndex = -1;
            listViewItem.SubItems.Add(text);
            listViewItem.SubItems.Add(text2);
            listViewItem.SubItems.Add(text3);
            this.selTab.Add(text + text2);
            this.listView1.Items.AddRange(new ListViewItem[]
			{
				listViewItem
			});
        }
        private List<ModelTran> GetModelTranlist()
        {
            List<ModelTran> list = new List<ModelTran>();
            foreach (ListViewItem listViewItem in this.listView1.Items)
            {
                ModelTran modelTran = new ModelTran();
                modelTran.DbName = this.dbname;
                modelTran.TableName = listViewItem.SubItems[0].Text;
                modelTran.ModelName = listViewItem.SubItems[1].Text;
                modelTran.Action = listViewItem.SubItems[2].Text;
                string text = listViewItem.SubItems[3].Text;
                List<ColumnInfo> columnInfoList = this.dbobj.GetColumnInfoList(this.dbname, modelTran.TableName);
                modelTran.Fieldlist = columnInfoList;
                modelTran.Keys = new List<ColumnInfo>();
                foreach (ColumnInfo current in columnInfoList)
                {
                    if (current.ColumnName == text)
                    {
                        modelTran.Keys.Add(current);
                    }
                }
                list.Add(modelTran);
            }
            return list;
        }
        private void SettxtContent(string Type, string strContent)
        {
            this.codeview.SettxtContent(Type, strContent);
            this.tabControl1.SelectedIndex = 1;
        }
        private string GetDALType()
        {
            string appGuid = this.cm_daltype.AppGuid;
            if (appGuid == "" || appGuid == "System.Data.DataRowView")
            {
                MessageBox.Show("选择的数据层类型有误，请关闭后重试！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return "";
            }
            return appGuid;
        }
        private void btn_Create_Click(object sender, EventArgs e)
        {
            string nameSpace = this.txtNameSpace.Text.Trim();
            string folder = this.txtNameSpace2.Text.Trim();
            List<ModelTran> modelTranlist = this.GetModelTranlist();
            if (modelTranlist.Count == 0)
            {
                return;
            }
            BuilderFrameS3 builderFrameS = new BuilderFrameS3(this.dbobj, this.dbname, nameSpace, folder, this.dbset.DbHelperName);
            string dALType = this.GetDALType();
            string dALCodeMTran = builderFrameS.GetDALCodeMTran(dALType, modelTranlist);
            this.SettxtContent("CS", dALCodeMTran);
        }
    }
}
