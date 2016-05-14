using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Text;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using Maticsoft.CodeBuild;
using Maticsoft.Utility;
using Maticsoft.CodeHelper;
using System.Threading;
namespace Codematic
{
    public partial class CodeTemp : Form
    {
        MainForm mainfrm;
        Maticsoft.IDBO.IDbObject dbobj;
        string servername;
        string dbname;
        string tablename;
        private Thread thread;
        private Thread threadCode;
        delegate void SetListCallback();

        public CodeTemp(Form mdiParentForm)
        {
            InitializeComponent();
            mainfrm = (MainForm)mdiParentForm;
            CreatView();         
            DbView dbviewfrm = (DbView)Application.OpenForms["DbView"];
            if (dbviewfrm != null)
            {
                //SetListView(dbviewfrm);
                try
                {
                    this.thread = new Thread(new ThreadStart(Showlistview));
                    this.thread.Start();
                }
                catch
                {
                }
            }
        }

        #region 设置listview

        void Showlistview()
        {
            DbView dbviewfrm = (DbView)Application.OpenForms["DbView"];
            //SetListView(dbviewfrm);
            if (dbviewfrm.treeView1.InvokeRequired)
            {
                SetListCallback d = new SetListCallback(Showlistview);
                dbviewfrm.Invoke(d, null);
            }
            else
            {
                SetListView(dbviewfrm);
            }
        }
        public void SetListView(DbView dbviewfrm)
        {
            #region 得到类型对象

            TreeNode SelNode = dbviewfrm.treeView1.SelectedNode;
            if (SelNode == null)
                return;
            switch (SelNode.Tag.ToString())
            {
                case "table":
                case "view":
                    {
                        servername = SelNode.Parent.Parent.Parent.Text;
                        dbname = SelNode.Parent.Parent.Text;
                        tablename = SelNode.Text;
                        dbobj = ObjHelper.CreatDbObj(servername);                       
                        BindlistViewCol(dbname, tablename);
                    }
                    break;
                case "column":
                    {
                        servername = SelNode.Parent.Parent.Parent.Parent.Text;
                        dbname = SelNode.Parent.Parent.Parent.Text;
                        tablename = SelNode.Parent.Text;
                        dbobj = ObjHelper.CreatDbObj(servername);                       
                        BindlistViewCol(dbname, tablename);

                    }
                    break;
                default:
                    {
                        this.listView1.Items.Clear();
                    }
                    break;
            }

            #endregion

        }

        #endregion

        #region  为listView邦定 列 数据

        private void CreatView()
        {
            //创建列表
            this.listView1.Columns.Clear();
            this.listView1.Items.Clear();
            this.listView1.LargeImageList = imglistView;
            this.listView1.SmallImageList = imglistView;
            this.listView1.View = View.Details;
            this.listView1.GridLines = true;
            this.listView1.CheckBoxes = true;
            this.listView1.FullRowSelect = true;

            listView1.Columns.Add("序号", 60, HorizontalAlignment.Center);
            listView1.Columns.Add("列名", 110, HorizontalAlignment.Left);
            listView1.Columns.Add("数据类型", 80, HorizontalAlignment.Left);
            listView1.Columns.Add("长度", 40, HorizontalAlignment.Left);
            listView1.Columns.Add("小数", 40, HorizontalAlignment.Left);
            listView1.Columns.Add("标识", 40, HorizontalAlignment.Center);
            listView1.Columns.Add("主键", 40, HorizontalAlignment.Center);
            listView1.Columns.Add("允许空", 60, HorizontalAlignment.Center);
            listView1.Columns.Add("默认值", 100, HorizontalAlignment.Left);
            //listView1.Columns.Add("字段说明", 100, HorizontalAlignment.Left);
        }

        private void BindlistViewCol(string Dbname, string TableName)
        {
            List<ColumnInfo> collist = dbobj.GetColumnInfoList(Dbname, TableName);
            if ((collist!=null)&&(collist.Count > 0))
            {
                listView1.Items.Clear();
                list_KeyField.Items.Clear();
                foreach (ColumnInfo col in collist)
                {
                    string order = col.ColumnOrder;
                    string columnName = col.ColumnName;
                    string columnType = col.TypeName;
                    string Length = col.Length;
                    string Preci = col.Precision;
                    string Scale = col.Scale;
                    string defaultVal = col.DefaultVal;
                    string description = col.Description;
                    
                    string IsIdentity = (col.IsIdentity) ? "√" : "";
                    string ispk = (col.IsPrimaryKey) ? "√" : "";
                    string isnull = (col.Nullable) ? "√" : "";

                    ListViewItem item1 = new ListViewItem(order, 0);
                    item1.ImageIndex = 4;
                    item1.SubItems.Add(columnName);
                    item1.SubItems.Add(columnType);
                    item1.SubItems.Add(Length);
                    item1.SubItems.Add(Scale);
                    item1.SubItems.Add(IsIdentity);
                    if ((ispk == "√") && (isnull.Trim() == ""))//是主键，非空
                    {
                        this.list_KeyField.Items.Add(columnName + "(" + columnType + ")");
                    }
                    else
                    {
                        ispk = "";
                    }
                    item1.SubItems.Add(ispk);
                    item1.SubItems.Add(isnull);
                    item1.SubItems.Add(defaultVal);

                    listView1.Items.AddRange(new ListViewItem[] { item1 });

                }
            }
            btn_SelAll_Click(null, null);
            //txtTabname.Text = TableName;
            //txtClassName.Text = TableName;
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

        #region  设定主键

        //设定主键的listbox
        private void btn_SetKey_Click(object sender, EventArgs e)
        {
            this.list_KeyField.Items.Clear();
            foreach (ListViewItem item in listView1.Items)
            {
                if (item.Checked)
                {
                    this.list_KeyField.Items.Add(item.SubItems[1].Text + "(" + item.SubItems[2].Text + ")");
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
        /// <summary>
        /// 得到主键的对象信息
        /// </summary>
        /// <returns></returns>
        private List<ColumnInfo> GetKeyFields()
        {
            //DataTable dt = dbobj.GetColumnInfoList(dbname, tablename);
            List<ColumnInfo> collist = dbobj.GetColumnInfoList(dbname, tablename);
            DataTable dt = Maticsoft.CodeHelper.CodeCommon.GetColumnInfoDt(collist);

            StringPlus Fields = new StringPlus();
            foreach (object obj in list_KeyField.Items)
            {
                Fields.Append("'" + obj.ToString() + "',");
            }
            Fields.DelLastComma();
            if (dt != null)
            {
                DataRow[] dtrows;
                if (Fields.Value.Length > 0)
                {
                    dtrows = dt.Select("ColumnName in (" + Fields.Value + ")", "colorder asc");
                }
                else
                {
                    dtrows = dt.Select();
                }
                List<ColumnInfo> keys = new List<ColumnInfo>();
                ColumnInfo key;
                foreach (DataRow row in dtrows)
                {
                    string Colorder = row["Colorder"].ToString();
                    string ColumnName = row["ColumnName"].ToString();
                    string TypeName = row["TypeName"].ToString();
                    string isIdentity = row["IsIdentity"].ToString();
                    string IsPK = row["IsPK"].ToString();
                    string Length = row["Length"].ToString();
                    string Preci = row["Preci"].ToString();
                    string Scale = row["Scale"].ToString();
                    string cisNull = row["cisNull"].ToString();
                    string DefaultVal = row["DefaultVal"].ToString();
                    string DeText = row["DeText"].ToString();

                    key = new ColumnInfo();
                    key.ColumnOrder = Colorder;
                    key.ColumnName = ColumnName;
                    key.TypeName = TypeName;
                    key.IsIdentity = (isIdentity == "√") ? true : false;
                    key.IsPrimaryKey = (IsPK == "√") ? true : false;
                    key.Length = Length;
                    key.Precision = Preci;
                    key.Scale = Scale;
                    key.Nullable = (cisNull == "√") ? true : false;
                    key.DefaultVal = DefaultVal;
                    key.Description = DeText;
                    keys.Add(key);

                }
                return keys;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 得到选择的字段集合
        /// </summary>
        /// <returns></returns>
        private List<ColumnInfo> GetFieldlist()
        {
            //DataTable dt = dbobj.GetColumnInfoList(dbname, tablename);
            List<ColumnInfo> collist = dbobj.GetColumnInfoList(dbname, tablename);
            DataTable dt = Maticsoft.CodeHelper.CodeCommon.GetColumnInfoDt(collist);

            StringPlus Fields = new StringPlus();
            foreach (ListViewItem item in listView1.Items)
            {
                if (item.Checked)
                {
                    Fields.Append("'" + item.SubItems[1].Text + "',");
                }
            }
            Fields.DelLastComma();
            if (dt != null)
            {
                DataRow[] dtrows;
                if (Fields.Value.Length > 0)
                {
                    dtrows = dt.Select("ColumnName in (" + Fields.Value + ")", "colorder asc");
                }
                else
                {
                    dtrows = dt.Select();
                }
                List<ColumnInfo> keys = new List<ColumnInfo>();
                ColumnInfo key;
                foreach (DataRow row in dtrows)
                {
                    string Colorder = row["Colorder"].ToString();
                    string ColumnName = row["ColumnName"].ToString();
                    string TypeName = row["TypeName"].ToString();
                    string isIdentity = row["IsIdentity"].ToString();
                    string IsPK = row["IsPK"].ToString();
                    string Length = row["Length"].ToString();
                    string Preci = row["Preci"].ToString();
                    string Scale = row["Scale"].ToString();
                    string cisNull = row["cisNull"].ToString();
                    string DefaultVal = row["DefaultVal"].ToString();
                    string DeText = row["DeText"].ToString();

                    key = new ColumnInfo();
                    key.ColumnOrder = Colorder;
                    key.ColumnName = ColumnName;
                    key.TypeName = TypeName;
                    key.IsIdentity = (isIdentity == "√") ? true : false;
                    key.IsPrimaryKey = (IsPK == "√") ? true : false;
                    key.Length = Length;
                    key.Precision = Preci;
                    key.Scale = Scale;
                    key.Nullable = (cisNull == "√") ? true : false;
                    key.DefaultVal = DefaultVal;
                    key.Description = DeText;
                    keys.Add(key);

                }
                return keys;
            }
            else
            {
                return null;
            }
        }


        #endregion

       
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

        private void Run()
        {
            StatusLabel1.Text = "正在生成...";
            //Hashtable keys = GetKeyFields();
            //ArrayList fieldlist=GetFieldlist();
            string tempxslt = @"Template\temp.xslt"; //临时xslt文件
            try
            {
                string strContent = txtTemplate.Text;
                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml(strContent);
                xdoc.Save(@"Template\temp.xslt");
            }
            catch(System.Exception ex)
            {
                StatusLabel1.Text = "模版格式有误：" + ex.Message;
                return;
            }

            string strcode = "";
            try
            {
                BuilderTemp bt = new BuilderTemp(dbobj, dbname, tablename, GetFieldlist(), GetKeyFields(), tempxslt);
                strcode = bt.GetCode();
            }
            catch (System.Exception ex)
            {
                StatusLabel1.Text = "代码转换失败！" + ex.Message;
                return;
            }
            string filename = @"Template\tempcode.cs";
            StreamWriter sw = new StreamWriter(filename, false, Encoding.Default);//,false);
            sw.Write(strcode);
            sw.Flush();
            sw.Close();
            StatusLabel1.Text = "生成成功。";
            mainfrm.AddSinglePage(new CodeEditor(filename,"cs"), "Class1.cs");
        }
    }
}