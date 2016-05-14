using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Collections;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Codematic.UserControls;
using Maticsoft.CodeBuild;
using Maticsoft.Utility;
using Maticsoft.CodeHelper;
using System.Threading;
using Maticsoft.AddInManager;
using Maticsoft.CmConfig;
namespace Codematic
{
    public partial class CodeMaker : Form
    {
        private Dictionary<string, int> dtCollection;
        public UcCodeView codeview;
        DbSettings setting;
        Maticsoft.IDBO.IDbObject dbobj;
        Maticsoft.CodeBuild.CodeBuilders cb;//代码生成对象
        string servername;
        string dbname;
        string tablename;
        private Thread thread;
        delegate void SetListCallback();
        DALTypeAddIn cm_daltype;
        DALTypeAddIn cm_blltype;
        DALTypeAddIn cm_webtype;
        NameRule namerule = new NameRule();//类命名规则

        private void InitializeForm()
        {
            this.list_KeyField.Height = 22;
            this.codeview = new UcCodeView();
            this.tabPage2.Controls.Add(this.codeview);
            this.SetListViewMenu("colum");
            this.CreatView();
            this.panel1.Height = 150;
            this.cm_daltype = new DALTypeAddIn("Maticsoft.IBuilder.IBuilderDAL");
            this.cm_daltype.Title = "DAL";
            this.groupBox_DALType.Controls.Add(this.cm_daltype);
            this.cm_daltype.Location = new System.Drawing.Point(30, 13);
            this.cm_blltype = new DALTypeAddIn("Maticsoft.IBuilder.IBuilderBLL");
            this.cm_blltype.Title = "BLL";
            this.groupBox_DALType.Controls.Add(this.cm_blltype);
            this.cm_blltype.Location = new System.Drawing.Point(30, 13);
            this.cm_webtype = new DALTypeAddIn("Maticsoft.IBuilder.IBuilderWeb");
            this.cm_webtype.Title = "Web";
            this.groupBox_DALType.Controls.Add(this.cm_webtype);
            this.cm_webtype.Location = new System.Drawing.Point(30, 13);
            this.tabControl1.SelectedIndex = 0;
        }

        public CodeMaker()
        {
            InitializeComponent();
            this.InitializeForm();
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

        public CodeMaker(string longServername, string DBName, string tableName)
        {
            this.InitializeComponent();
            this.InitializeForm();
            this.servername = longServername;
            this.dbname = DBName;
            this.tablename = tableName;
            this.dbobj = ObjHelper.CreatDbObj(longServername);
            this.SetListViewMenu("column");
            this.BindlistViewCol(this.dbname, this.tablename);
            this.SetCodeBuilders();
            this.SetFormConfig(this.servername);
        }

        private void CodeMaker_Load(object sender, EventArgs e)
        {

        }

        void Showlistview()
        {
            DbView dbView = (DbView)Application.OpenForms["DbView"];
            if (dbView.treeView1.InvokeRequired)
            {
                CodeMaker.SetListCallback method = new CodeMaker.SetListCallback(this.Showlistview);
                try
                {
                    dbView.Invoke(method, null);
                    return;
                }
                catch
                {
                    return;
                }
            }
            this.SetListView(dbView);
        }


        #region 初始化窗体设置
        private void SetFormConfig(string servername)
        {
            if (servername == null || servername.Length == 0)
            {
                return;
            }
            this.setting = DbConfig.GetSetting(servername);
            this.txtProjectName.Text = this.setting.ProjectName;
            this.txtNameSpace.Text = this.setting.Namepace;
            this.txtNameSpace2.Text = this.setting.Folder;
            this.txtProcPrefix.Text = this.setting.ProcPrefix;
            string a;
            if ((a = this.setting.AppFrame.ToLower()) != null)
            {
                if (!(a == "one"))
                {
                    if (!(a == "s3"))
                    {
                        if (a == "f3")
                        {
                            this.radbtn_Frame_F3.Checked = true;
                        }
                    }
                    else
                    {
                        this.radbtn_Frame_S3.Checked = true;
                    }
                }
                else
                {
                    this.radbtn_Frame_One.Checked = true;
                }
            }
            this.radbtn_Type_Click(null, null);
            this.radbtn_Frame_Click(null, null);
            this.radbtn_F3_Click(null, null);
            this.cm_daltype.SetSelectedDALType(this.setting.DALType.Trim());
            this.cm_blltype.SetSelectedDALType(this.setting.BLLType.Trim());
            this.cm_webtype.SetSelectedDALType(this.setting.WebType.Trim());
        }


        #endregion

        #region  选择设置

        #region 类型选择
        private void radbtn_Type_Click(object sender, EventArgs e)
        {
            if (this.radbtn_Type_DB.Checked)
            {
                this.groupBox_DB.Visible = true;
                this.groupBox_AppType.Visible = false;
                this.groupBox_DALType.Visible = false;
                this.groupBox_Method.Visible = false;
                this.groupBox_FrameSel.Visible = false;
                this.groupBox_F3.Visible = false;
                this.groupBox_Web.Visible = false;
            }
            if (this.radbtn_Type_CS.Checked)
            {
                this.groupBox_DB.Visible = false;
                this.groupBox_AppType.Visible = true;
                this.groupBox_DALType.Visible = true;
                this.groupBox_Method.Visible = true;
                this.groupBox_FrameSel.Visible = true;
                this.groupBox_F3.Visible = true;
                this.groupBox_Web.Visible = false;
                this.radbtn_Frame_Click(sender, e);
            }
            if (this.radbtn_Type_Web.Checked)
            {
                this.groupBox_DB.Visible = false;
                this.groupBox_AppType.Visible = false;
                this.groupBox_DALType.Visible = true;
                this.groupBox_Method.Visible = false;
                this.groupBox_FrameSel.Visible = false;
                this.groupBox_F3.Visible = false;
                this.groupBox_Web.Visible = true;
                this.cm_daltype.Visible = false;
                this.cm_blltype.Visible = false;
                this.cm_webtype.Visible = true;
            }

        }
        #endregion

        #region 架构选择
        private void radbtn_Frame_Click(object sender, EventArgs e)
        {
            if (this.radbtn_Frame_One.Checked)
            {
                this.groupBox_DB.Visible = false;
                this.groupBox_F3.Visible = false;
                this.groupBox_DALType.Visible = true;
                this.groupBox_Method.Visible = true;
                this.groupBox_AppType.Visible = false;
                this.groupBox_Web.Visible = false;
            }
            if (this.radbtn_Frame_S3.Checked)
            {
                this.groupBox_DB.Visible = false;
                this.groupBox_F3.Visible = true;
                this.groupBox_DALType.Visible = true;
                this.groupBox_Method.Visible = true;
                this.groupBox_AppType.Visible = false;
                this.groupBox_Web.Visible = false;
                this.radbtn_F3_IDAL.Visible = false;
                this.radbtn_F3_DALFactory.Visible = false;
                this.radbtn_F3_Click(sender, e);
            }
            if (this.radbtn_Frame_F3.Checked)
            {
                this.groupBox_DB.Visible = false;
                this.groupBox_F3.Visible = true;
                this.groupBox_DALType.Visible = true;
                this.groupBox_Method.Visible = true;
                this.groupBox_AppType.Visible = true;
                this.groupBox_Web.Visible = false;
                this.radbtn_F3_IDAL.Visible = true;
                this.radbtn_F3_DALFactory.Visible = true;
                this.radbtn_F3_Click(sender, e);
            }

        }
        #endregion

        #region 层选择(工厂模式的)
        private void radbtn_F3_Click(object sender, EventArgs e)
        {
            if (this.radbtn_F3_Model.Checked)
            {
                this.groupBox_DALType.Visible = false;
                this.groupBox_Method.Visible = false;
                this.groupBox_AppType.Visible = false;
                this.groupBox_Web.Visible = false;
            }
            if (this.radbtn_F3_DAL.Checked)
            {
                this.groupBox_DALType.Visible = true;
                this.cm_daltype.Visible = true;
                this.cm_blltype.Visible = false;
                this.cm_webtype.Visible = false;
                this.groupBox_Method.Visible = true;
                this.groupBox_AppType.Visible = false;
                this.groupBox_Web.Visible = false;
            }
            if (this.radbtn_F3_IDAL.Checked)
            {
                this.groupBox_DALType.Visible = false;
                this.groupBox_Method.Visible = true;
                this.groupBox_AppType.Visible = false;
                this.groupBox_Web.Visible = false;
            }
            if (this.radbtn_F3_DALFactory.Checked)
            {
                this.groupBox_DALType.Visible = false;
                this.groupBox_Method.Visible = false;
                this.groupBox_AppType.Visible = true;
                this.groupBox_Web.Visible = false;
            }
            if (this.radbtn_F3_BLL.Checked)
            {
                this.groupBox_DALType.Visible = true;
                this.cm_daltype.Visible = false;
                this.cm_blltype.Visible = true;
                this.cm_webtype.Visible = false;
                this.groupBox_Method.Visible = true;
                this.groupBox_AppType.Visible = false;
                this.groupBox_Web.Visible = false;
            }
        }
        #endregion

        private void radbtn_DBSel_Click(object sender, EventArgs e)
        {
            if (this.radbtn_DB_Proc.Checked)
            {
                chk_DB_GetMaxID.Visible = true;
                chk_DB_Exists.Visible = true;
                chk_DB_Add.Visible = true;
                chk_DB_Update.Visible = true;
                chk_DB_Delete.Visible = true;
                chk_DB_GetModel.Visible = true;
                chk_DB_GetList.Visible = true;
                txtProcPrefix.Visible = true;
                txtTabname.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;

            }
            else
            {
                chk_DB_GetMaxID.Visible = false;
                chk_DB_Exists.Visible = false;
                chk_DB_Add.Visible = false;
                chk_DB_Update.Visible = false;
                chk_DB_Delete.Visible = false;
                chk_DB_GetModel.Visible = false;
                chk_DB_GetList.Visible = false;
                txtProcPrefix.Visible = false;
                txtTabname.Visible = false;
                label5.Visible = false;
                label6.Visible = false;
                label7.Visible = false;
            }
        }
        #endregion

        #region 设置listview

        public void SetListView(DbView dbviewfrm)
        {
            #region 得到类型对象

            TreeNode selectedNode = dbviewfrm.treeView1.SelectedNode;
            if (selectedNode == null)
            {
                return;
            }
            string key;
            switch (key = selectedNode.Tag.ToString())
            {
                case "serverlist":
                case "server":
                    this.servername = selectedNode.Text;
                    break;
                case "db":
                    this.servername = selectedNode.Parent.Text;
                    this.dbname = selectedNode.Text;
                    break;
                case "table":
                    this.servername = selectedNode.Parent.Parent.Parent.Text;
                    this.dbname = selectedNode.Parent.Parent.Text;
                    this.tablename = selectedNode.Text;
                    this.dbobj = ObjHelper.CreatDbObj(this.servername);
                    this.SetListViewMenu("column");
                    this.BindlistViewCol(this.dbname, this.tablename);
                    break;
                case "view":
                    this.servername = selectedNode.Parent.Parent.Parent.Text;
                    this.dbname = selectedNode.Parent.Parent.Text;
                    this.tablename = selectedNode.Text;
                    this.dbobj = ObjHelper.CreatDbObj(this.servername);
                    this.SetListViewMenu("column");
                    this.BindlistViewCol(this.dbname, this.tablename);
                    break;
                case "column":
                    this.servername = selectedNode.Parent.Parent.Parent.Parent.Text;
                    this.dbname = selectedNode.Parent.Parent.Parent.Text;
                    this.tablename = selectedNode.Parent.Text;
                    this.dbobj = ObjHelper.CreatDbObj(this.servername);
                    this.SetListViewMenu("column");
                    this.BindlistViewCol(this.dbname, this.tablename);
                    break;
                default: this.listView1.Items.Clear(); break;
            }

        IL_256:
            this.SetCodeBuilders();
            this.SetFormConfig(this.servername);
            #endregion

        }
        #endregion
        private void SetCodeBuilders()
        {
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
        }

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
            listView1.Columns.Add("长度", 45, HorizontalAlignment.Left);
            listView1.Columns.Add("小数", 45, HorizontalAlignment.Left);
            listView1.Columns.Add("标识", 45, HorizontalAlignment.Center);
            listView1.Columns.Add("主键", 45, HorizontalAlignment.Center);
            this.listView1.Columns.Add("外键", 45, HorizontalAlignment.Center);
            this.listView1.Columns.Add("允许空", 60, HorizontalAlignment.Center);
            listView1.Columns.Add("默认值", 100, HorizontalAlignment.Left);
            //listView1.Columns.Add("字段说明", 100, HorizontalAlignment.Left);
        }

        private void BindlistViewCol(string Dbname, string TableName)
        {
            this.chk_CS_GetMaxID.Checked = true;
            List<ColumnInfo> collist = dbobj.GetColumnInfoList(Dbname, TableName);
            if ((collist != null) && (collist.Count > 0))
            {
                listView1.Items.Clear();
                list_KeyField.Items.Clear();
                this.chk_CS_GetMaxID.Enabled = true;
                foreach (ColumnInfo col in collist)
                {
                    string order = col.ColumnOrder;
                    string columnName = col.ColumnName;
                    string columnType = col.TypeName;
                    string Length = col.Length;
                    if (columnType == null)
                    {
                        Length = CodeCommon.GetDataTypeLenVal(columnType, Length);
                    }
                    if (dtCollection == null)
                    {
                        dtCollection = new Dictionary<string, int>(6){{"char",0},{
								"nchar",
								1
							},

							{
								"binary",
								2
							},

							{
								"varchar",
								3
							},

							{
								"nvarchar",
								4
							},

							{
								"varbinary",
								5
							}
						};
                    }
                    int num;
                    if (!dtCollection.TryGetValue(columnType, out num))
                    {
                        Length = CodeCommon.GetDataTypeLenVal(columnType, Length);
                        break;
                    }
                    switch (num)
                    {
                        case 0:
                        case 1:
                        case 2:
                            Length = CodeCommon.GetDataTypeLenVal(columnType, Length);
                            break;
                        case 3:
                        case 4:
                        case 5:
                            Length = CodeCommon.GetDataTypeLenVal(columnType.Trim().ToLower(), Length);
                            break;
                        default:
                            Length = CodeCommon.GetDataTypeLenVal(columnType, Length);
                            break;
                    }
              
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
                        this.list_KeyField.Items.Add(columnName);
                        if (IsIdentity == "√")
                        {
                            this.chk_CS_GetMaxID.Checked = false;
                            this.chk_CS_GetMaxID.Enabled = false;
                            this.chk_DB_GetMaxID.Checked = false;
                            this.chk_DB_GetMaxID.Enabled = false;
                            //KeyIsIdentity = true;
                        }
                    }
                    else
                    {
                        ispk = "";
                        if (IsIdentity == "√")
                        {
                            this.list_KeyField.Items.Add(columnName);
                            this.chk_CS_GetMaxID.Checked = false;
                            this.chk_CS_GetMaxID.Enabled = false;
                            this.chk_DB_GetMaxID.Checked = false;
                            this.chk_DB_GetMaxID.Enabled = false;
                            //KeyIsIdentity = true;
                        }
                    }

                    item1.SubItems.Add(ispk);
                    item1.SubItems.Add(isnull);
                    item1.SubItems.Add(defaultVal);

                    listView1.Items.AddRange(new ListViewItem[] { item1 });

                }
            }
            btn_SelAll_Click(null, null);
            txtTabname.Text = TableName;
            txtClassName.Text = TableName;
            lblkeycount.Text = list_KeyField.Items.Count.ToString() + "个主键";
        }
        #endregion

        #region 设定listview右键菜单
        private void SetListViewMenu(string itemType)
        {
            string a;
            if ((a = itemType.ToLower()) != null && !(a == "server") && !(a == "db") && !(a == "table"))
            {
                if (a == "column")
                {
                }
            }
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
                    this.list_KeyField.Items.Add(item.SubItems[1].Text);
                }
            }
            lblkeycount.Text = list_KeyField.Items.Count.ToString() + "个主键";
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// 设置代码控件内容
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="strContent"></param>
        private void SettxtContent(string Type, string strContent)
        {
            codeview.SettxtContent(Type, strContent);
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
        ///// <summary>
        ///// 得到选择的字段集合
        ///// </summary>
        ///// <returns></returns>
        //private List<ColumnInfo> GetFieldlist()
        //{            
        //    List<ColumnInfo> collist = dbobj.GetColumnInfoList(dbname, tablename);

        //    DataTable dt = Maticsoft.CodeHelper.CodeCommon.GetColumnInfoDt(collist);
        //    StringPlus Fields = new StringPlus();
        //    foreach (ListViewItem item in listView1.Items)
        //    {
        //        if (item.Checked)
        //        {
        //            Fields.Append("'" + item.SubItems[1].Text + "',");
        //        }
        //    }
        //    Fields.DelLastComma();
        //    if (dt != null)
        //    {
        //        DataRow[] dtrows;
        //        if (Fields.Value.Length > 0)
        //        {                    
        //            dtrows = dt.Select("ColumnName in (" + Fields.Value + ")", "colorder asc");

        //        }
        //        else
        //        {
        //            dtrows = dt.Select();
        //        }
        //        List<ColumnInfo> keys = new List<ColumnInfo>();
        //        ColumnInfo key;
        //        foreach (DataRow row in dtrows)
        //        {
        //            string Colorder = row["Colorder"].ToString();
        //            string ColumnName = row["ColumnName"].ToString();
        //            string TypeName = row["TypeName"].ToString();
        //            string isIdentity = row["IsIdentity"].ToString();
        //            string IsPK = row["IsPK"].ToString();
        //            string Length = row["Length"].ToString();
        //            string Preci = row["Preci"].ToString();
        //            string Scale = row["Scale"].ToString();
        //            string cisNull = row["cisNull"].ToString();
        //            string DefaultVal = row["DefaultVal"].ToString();
        //            string DeText = row["DeText"].ToString();

        //            key = new ColumnInfo();
        //            key.ColumnOrder = Colorder;
        //            key.ColumnName = ColumnName;
        //            key.TypeName = TypeName;
        //            key.IsIdentity = (isIdentity == "√") ? true : false;
        //            key.IsPrimaryKey = (IsPK == "√") ? true : false;
        //            key.Length = Length;
        //            key.Precision = Preci;
        //            key.Scale = Scale;
        //            key.Nullable = (cisNull == "√") ? true : false;
        //            key.DefaultVal = DefaultVal;
        //            key.Description = DeText;
        //            keys.Add(key);

        //        }
        //        return keys;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        /// <summary>
        /// 得到选择的字段集合
        /// </summary>
        /// <returns></returns>
        private List<ColumnInfo> GetFieldlist()
        {
            List<ColumnInfo> collist = dbobj.GetColumnInfoList(dbname, tablename);
            List<ColumnInfo> collistSel = new List<ColumnInfo>();
            foreach (ColumnInfo col in collist)
            {
                foreach (ListViewItem item in listView1.Items)
                {
                    if (col.ColumnName == item.SubItems[1].Text)
                    {
                        if (item.Checked)
                        {
                            collistSel.Add(col);
                        }
                        break;
                    }
                }
            }
            return collistSel;
        }


        //得到dal层类型
        private string GetDALType()
        {
            string daltype = "";
            daltype = cm_daltype.AppGuid;
            if ((daltype == "") && (daltype == "System.Data.DataRowView"))
            {
                MessageBox.Show("请选择数据层生成类型，或关闭后重试！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return "";
            }
            return daltype;
        }

        //得到bll层类型
        private string GetBLLType()
        {
            string blltype = "";
            blltype = cm_blltype.AppGuid;
            if ((blltype == "") && (blltype == "System.Data.DataRowView"))
            {
                MessageBox.Show("请选择业务层生成类型，或关闭后重试！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return "";
            }
            return blltype;
        }
        //得到web层类型
        public string GetWebType()
        {
            string webtype = "";
            webtype = cm_webtype.AppGuid;
            if ((webtype == "") && (webtype == "System.Data.DataRowView"))
            {
                MessageBox.Show("请选择表示层生成类型，或关闭后重试！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return "";
            }
            return webtype;
        }

        #endregion

        #region 代码生成 按钮

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if (listView1.CheckedItems.Count < 1)
            {
                MessageBox.Show("没有任何可以生成的项！", "请选择", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            if (list_KeyField.Items.Count == 0)
            {
                DialogResult result = MessageBox.Show("没有主键字段和条件字段，你确认要继续生成？", "主键提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    return;
                }
            }

            try
            {
                if (this.radbtn_Type_DB.Checked)
                {
                    CreatDB();
                }
                if (this.radbtn_Type_CS.Checked)
                {
                    CreatCS();
                }
                if (this.radbtn_Type_Web.Checked)
                {
                    CreatWeb();
                }
            }
            catch (System.SystemException ex)
            {
                MessageBox.Show("生成代码失败，请关闭后重新打开再试。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LogInfo.WriteLog(ex.Message);
            }

            #region 保存配置

            if (this.radbtn_Frame_One.Checked)
            {
                setting.AppFrame = "One";
            }
            if (this.radbtn_Frame_S3.Checked)
            {
                setting.AppFrame = "S3";
            }
            if (this.radbtn_Frame_F3.Checked)
            {
                setting.AppFrame = "F3";
            }

            setting.DALType = GetDALType();
            setting.BLLType = GetBLLType();
            setting.ProjectName = txtProjectName.Text;
            setting.Namepace = txtNameSpace.Text;
            setting.Folder = txtNameSpace2.Text;
            setting.ProcPrefix = txtProcPrefix.Text;
            DbConfig.UpdateSettings(this.setting);
            #endregion
        }

        #endregion

        #region 生成 数据库脚本
        private void CreatDB()
        {
            if (this.radbtn_DB_Proc.Checked)
            {
                CreatDBProc();
            }
            else
            {
                CreatDBScript();
            }
        }

        //创建存储过程
        private void CreatDBProc()
        {
            string projectname = this.txtProjectName.Text;
            string snamespace = this.txtNameSpace.Text;
            string snamespace2 = this.txtNameSpace2.Text;
            string classname = this.txtClassName.Text;
            string procPrefix = this.txtProcPrefix.Text;
            string procTabname = this.txtTabname.Text;

            bool Maxid = this.chk_DB_GetMaxID.Checked;
            bool Exists = this.chk_DB_Exists.Checked;
            bool Add = this.chk_DB_Add.Checked;
            bool Update = this.chk_DB_Update.Checked;
            bool Delete = this.chk_DB_Delete.Checked;
            bool GetModel = this.chk_DB_GetModel.Checked;
            bool List = this.chk_DB_GetList.Checked;

            Maticsoft.IDBO.IDbScriptBuilder dsb = ObjHelper.CreatDsb(servername);
            dsb.DbName = dbname;
            dsb.TableName = procTabname;// tablename;
            dsb.ProjectName = projectname;
            dsb.ProcPrefix = procPrefix;
            dsb.Keys = GetKeyFields();
            dsb.Fieldlist = GetFieldlist();
            string strProc = dsb.GetPROCCode(Maxid, Exists, Add, Update, Delete, GetModel, List);
            SettxtContent("SQL", strProc);
        }
        //数据库脚本
        private void CreatDBScript()
        {
            this.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            Maticsoft.IDBO.IDbScriptBuilder dsb = ObjHelper.CreatDsb(servername);
            dsb.Fieldlist = GetFieldlist();
            string strScript = dsb.CreateTabScript(dbname, tablename);
            SettxtContent("SQL", strScript);
            this.Cursor = System.Windows.Forms.Cursors.Default;
        }

        #endregion

        #region 生成 C#代码
        //架构选择
        private void CreatCS()
        {
            if (this.radbtn_Frame_One.Checked)
            {
                CreatCsOne();
            }
            if (this.radbtn_Frame_S3.Checked)
            {
                CreatCsS3();
            }
            if (this.radbtn_Frame_F3.Checked)
            {
                CreatCsF3();
            }
        }

        #region 单类结构
        private void CreatCsOne()
        {
            string procPrefix = this.txtProcPrefix.Text;
            string namepace = this.txtNameSpace.Text.Trim();
            string namepace2 = this.txtNameSpace2.Text.Trim();
            if (namepace2.Trim() != "")
            {
                namepace = namepace + "." + namepace2;
            }
            string classname = this.txtClassName.Text;
            if (classname == "")
            {
                classname = tablename;
            }

            BuilderFrameOne cfo = new BuilderFrameOne(dbobj, dbname, tablename, classname, GetFieldlist(), GetKeyFields(), namepace, namepace2, setting.DbHelperName);

            string dALtype = GetDALType();
            string strCode = cfo.GetCode(dALtype, chk_CS_GetMaxID.Checked, chk_CS_Exists.Checked, chk_CS_Add.Checked,
                chk_CS_Update.Checked, chk_CS_Delete.Checked, chk_CS_GetModel.Checked, chk_CS_GetList.Checked, procPrefix);
            SettxtContent("CS", strCode);
        }
        #endregion

        #region 简单三层

        private void CreatCsS3()
        {
            if (radbtn_F3_Model.Checked)
            {
                CreatCsS3Model();
            }
            if (radbtn_F3_DAL.Checked)
            {
                CreatCsS3DAL();
            }
            if (radbtn_F3_BLL.Checked)
            {
                CreatCsS3BLL();
            }
        }
        private void CreatCsS3Model()
        {
            string namepace = this.txtNameSpace.Text.Trim();
            string namepace2 = this.txtNameSpace2.Text.Trim();
            string modelname = this.txtClassName.Text;
            if (modelname == "")
            {
                modelname = tablename;
            }
            string bllname = modelname;
            string dalname = modelname;
            //命名规则处理
            modelname = NameRule.GetModelClass(modelname, this.setting);
            bllname = NameRule.GetBLLClass(bllname, this.setting);
            dalname = NameRule.GetDALClass(dalname, this.setting);

            BuilderFrameS3 builderFrameS = new BuilderFrameS3(this.dbobj, this.dbname, this.tablename, this.cb.TableDescription, modelname, bllname, dalname, this.GetFieldlist(), this.GetKeyFields(), namepace, namepace2, setting.DbHelperName);
            string strCode = builderFrameS.GetModelCode();
            SettxtContent("CS", strCode);
        }

        private void CreatCsS3DAL()
        {
            string procPrefix = this.txtProcPrefix.Text;
            string namepace = this.txtNameSpace.Text.Trim();
            string namepace2 = this.txtNameSpace2.Text.Trim();
            string modelname = this.txtClassName.Text;
            if (modelname == "")
            {
                modelname = tablename;
            }
            string bllname = modelname;
            string dalname = modelname;
            //命名规则处理
            modelname = NameRule.GetModelClass(modelname,setting);
            bllname = NameRule.GetBLLClass(bllname, setting);
            dalname = NameRule.GetDALClass(dalname, setting);

           // BuilderFrameS3 s3 = new BuilderFrameS3(dbobj, dbname, tablename, modelname, bllname, dalname, GetFieldlist(), GetKeyFields(), namepace, namepace2, setting.DbHelperName);
            BuilderFrameS3 builderFrameS = new BuilderFrameS3(this.dbobj, this.dbname, this.tablename, this.cb.TableDescription, modelname, bllname, dalname, this.GetFieldlist(), this.GetKeyFields(), namepace, namepace2, setting.DbHelperName);
            string dALtype = GetDALType();
            string strCode = builderFrameS.GetDALCode(dALtype, chk_CS_GetMaxID.Checked, chk_CS_Exists.Checked, chk_CS_Add.Checked,
                chk_CS_Update.Checked, chk_CS_Delete.Checked, chk_CS_GetModel.Checked, chk_CS_GetList.Checked, procPrefix);
            SettxtContent("CS", strCode);
        }
        private void CreatCsS3BLL()
        {
            string namepace = this.txtNameSpace.Text.Trim();
            string namepace2 = this.txtNameSpace2.Text.Trim();
            string modelname = this.txtClassName.Text;
            if (modelname == "")
            {
                modelname = tablename;
            }
            string bllname = modelname;
            string dalname = modelname;
            //命名规则处理
            modelname = NameRule.GetModelClass(modelname, this.setting);
            bllname = NameRule.GetBLLClass(bllname, this.setting);
            dalname = NameRule.GetDALClass(dalname, this.setting);

            //BuilderFrameS3 s3 = new BuilderFrameS3(dbobj, dbname, tablename, modelname, bllname, dalname, GetFieldlist(), GetKeyFields(), namepace, namepace2, setting.DbHelperName);
            BuilderFrameS3 builderFrameS = new BuilderFrameS3(this.dbobj, this.dbname, this.tablename, this.cb.TableDescription, modelname, bllname, dalname, this.GetFieldlist(), this.GetKeyFields(), namepace, namepace2, setting.DbHelperName);
            string blltype = GetBLLType();
            string strCode = builderFrameS.GetBLLCode(blltype, chk_CS_GetMaxID.Checked, chk_CS_Exists.Checked, chk_CS_Add.Checked, chk_CS_Update.Checked, chk_CS_Delete.Checked, chk_CS_GetModel.Checked, chk_CS_GetModelByCache.Checked, chk_CS_GetList.Checked);
            SettxtContent("CS", strCode);
        }
        #endregion

        #region 工厂模式三层
        private void CreatCsF3()
        {
            if (radbtn_F3_Model.Checked)
            {
                CreatCsF3Model();
            }
            if (radbtn_F3_DAL.Checked)
            {
                CreatCsF3DAL();
            }
            if (radbtn_F3_IDAL.Checked)
            {
                CreatCsF3IDAL();
            }
            if (radbtn_F3_DALFactory.Checked)
            {
                CreatCsF3DALFactory();
            }
            if (radbtn_F3_BLL.Checked)
            {
                CreatCsF3BLL();
            }
        }
        private void CreatCsF3Model()
        {
            string namepace = this.txtNameSpace.Text.Trim();
            string namepace2 = this.txtNameSpace2.Text.Trim();
            string modelname = this.txtClassName.Text;
            if (modelname == "")
            {
                modelname = tablename;
            }
            string bllname = modelname;
            string dalname = modelname;

            //命名规则处理
            modelname = NameRule.GetModelClass(modelname, this.setting);
            bllname = NameRule.GetBLLClass(bllname, this.setting);
            dalname = NameRule.GetDALClass(dalname, this.setting);

            BuilderFrameF3 builderFrameS = new BuilderFrameF3(this.dbobj, this.dbname, this.tablename, this.cb.TableDescription, modelname, bllname, dalname, this.GetFieldlist(), this.GetKeyFields(), namepace, namepace2, setting.DbHelperName);
            string strCode = builderFrameS.GetModelCode();
            SettxtContent("CS", strCode);
        }
        private void CreatCsF3DAL()
        {
            string procPrefix = this.txtProcPrefix.Text;
            string namepace = this.txtNameSpace.Text.Trim();
            string namepace2 = this.txtNameSpace2.Text.Trim();
            string modelname = this.txtClassName.Text;
            if (modelname == "")
            {
                modelname = tablename;
            }
            string bllname = modelname;
            string dalname = modelname;
            //命名规则处理
            modelname = NameRule.GetModelClass(modelname, this.setting);
            bllname = NameRule.GetBLLClass(bllname, this.setting);
            dalname = NameRule.GetDALClass(dalname, this.setting);

            BuilderFrameF3 builderFrameS = new BuilderFrameF3(this.dbobj, this.dbname, this.tablename, this.cb.TableDescription, modelname, bllname, dalname, this.GetFieldlist(), this.GetKeyFields(), namepace, namepace2, setting.DbHelperName);
            string dALtype = GetDALType();
            string strCode = builderFrameS.GetDALCode(dALtype, chk_CS_GetMaxID.Checked, chk_CS_Exists.Checked, chk_CS_Add.Checked,
                chk_CS_Update.Checked, chk_CS_Delete.Checked, chk_CS_GetModel.Checked, chk_CS_GetList.Checked, procPrefix);
            SettxtContent("CS", strCode);
        }
        private void CreatCsF3IDAL()
        {
            string namepace = this.txtNameSpace.Text.Trim();
            string namepace2 = this.txtNameSpace2.Text.Trim();
            string modelname = this.txtClassName.Text;
            if (modelname == "")
            {
                modelname = tablename;
            }
            string bllname = modelname;
            string dalname = modelname;
            //命名规则处理
            modelname = NameRule.GetModelClass(modelname, this.setting);
            bllname = NameRule.GetBLLClass(bllname, this.setting);
            dalname = NameRule.GetDALClass(dalname, this.setting);

            BuilderFrameF3 builderFrameS = new BuilderFrameF3(this.dbobj, this.dbname, this.tablename, this.cb.TableDescription, modelname, bllname, dalname, this.GetFieldlist(), this.GetKeyFields(), namepace, namepace2, setting.DbHelperName);
            string strCode = builderFrameS.GetIDALCode(chk_CS_GetMaxID.Checked, chk_CS_Exists.Checked, chk_CS_Add.Checked, chk_CS_Update.Checked, chk_CS_Delete.Checked, chk_CS_GetModel.Checked, chk_CS_GetList.Checked, chk_CS_GetList.Checked);
            SettxtContent("CS", strCode);
        }
        private void CreatCsF3DALFactory()
        {
            string namepace = this.txtNameSpace.Text.Trim();
            string namepace2 = this.txtNameSpace2.Text.Trim();
            string modelname = this.txtClassName.Text;
            if (modelname == "")
            {
                modelname = tablename;
            }
            string bllname = modelname;
            string dalname = modelname;
            //命名规则处理
            modelname = NameRule.GetModelClass(modelname, this.setting);
            bllname = NameRule.GetBLLClass(bllname, this.setting);
            dalname = NameRule.GetDALClass(dalname, this.setting);

            BuilderFrameF3 builderFrameS = new BuilderFrameF3(this.dbobj, this.dbname, this.tablename, this.cb.TableDescription, modelname, bllname, dalname, this.GetFieldlist(), this.GetKeyFields(), namepace, namepace2, setting.DbHelperName);
            string strCode = builderFrameS.GetDALFactoryCode();
            SettxtContent("CS", strCode);
        }
        private void CreatCsF3BLL()
        {
            string namepace = this.txtNameSpace.Text.Trim();
            string namepace2 = this.txtNameSpace2.Text.Trim();
            string modelname = this.txtClassName.Text;
            if (modelname == "")
            {
                modelname = tablename;
            }
            string bllname = modelname;
            string dalname = modelname;
            //命名规则处理
            modelname = NameRule.GetModelClass(modelname, this.setting);
            bllname = NameRule.GetBLLClass(bllname, this.setting);
            dalname = NameRule.GetDALClass(dalname, this.setting);

            BuilderFrameF3 builderFrameS = new BuilderFrameF3(this.dbobj, this.dbname, this.tablename, this.cb.TableDescription, modelname, bllname, dalname, this.GetFieldlist(), this.GetKeyFields(), namepace, namepace2, setting.DbHelperName);
            string blltype = GetBLLType();
            string strCode = builderFrameS.GetBLLCode(blltype, this.chk_CS_GetMaxID.Checked, this.chk_CS_Exists.Checked, this.chk_CS_Add.Checked, this.chk_CS_Update.Checked, this.chk_CS_Delete.Checked, this.chk_CS_GetModel.Checked, this.chk_CS_GetModelByCache.Checked, this.chk_CS_GetList.Checked, this.chk_CS_GetList.Checked);
            SettxtContent("CS", strCode);
        }
        #endregion

        #endregion

        #region 生成 Web页面
        private void CreatWeb()
        {
            string namepace = this.txtNameSpace.Text.Trim();
            string folder = this.txtNameSpace2.Text.Trim();
            string modelname = this.txtClassName.Text;
            if (modelname == "")
            {
                modelname = tablename;
            }
            string bllname = modelname;
            //命名规则处理
            modelname = NameRule.GetModelClass(modelname, this.setting);
            bllname = NameRule.GetBLLClass(bllname, this.setting);

            //Maticsoft.BuilderWeb.BuilderWeb bw = new Maticsoft.BuilderWeb.BuilderWeb();            
            //bw.NameSpace = namepace;
            //bw.Fieldlist = GetFieldlist();
            //bw.Keys = GetKeyFields();
            //bw.ModelName = modelname;
            //bw.BLLName = bllname;
            //bw.Folder = folder;

            cb.BLLName = bllname;
            cb.NameSpace = namepace;
            cb.Fieldlist = GetFieldlist();
            cb.Keys = GetKeyFields();
            cb.ModelName = modelname;
            cb.Folder = folder;

            string webtype = GetWebType();
            cb.CreatBuilderWeb(webtype);

            if (radbtn_Web_Aspx.Checked)
            {
                string strCode = cb.GetWebHtmlCode(chk_Web_HasKey.Checked, chk_Web_Add.Checked, chk_Web_Update.Checked, chk_Web_Show.Checked, true);
                SettxtContent("Aspx", strCode);
            }
            else
            {
                string strCode = cb.GetWebCode(chk_Web_HasKey.Checked, chk_Web_Add.Checked, chk_Web_Update.Checked, chk_Web_Show.Checked, true);
                SettxtContent("CS", strCode);
            }
        }
        #endregion


    }
}