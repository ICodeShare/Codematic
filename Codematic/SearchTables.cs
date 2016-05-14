using Maticsoft.CmConfig;
using Maticsoft.CodeHelper;
using Maticsoft.DBFactory;
using Maticsoft.IDBO;
using Maticsoft.Utility;
using Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Codematic
{
    public partial class SearchTables : Form
    {
        private delegate void SetlblStatuCallback(string text);
        private delegate void SetProBar1MaxCallback(int val);
        private delegate void SetProBar1ValCallback(int val);
        private MainForm mainfrm;
        private IDbObject dbobj;
        private DbSettings dbset;
        private Microsoft.Office.Interop.Word.Application WordApp = new Microsoft.Office.Interop.Word.Application();
        private Thread mythread;
        private string currentDbname = "";
        protected string longServername;

        public SearchTables(MainForm mdiParentForm, string longservername)
        {
            InitializeComponent();
            this.mainfrm = mdiParentForm;
            this.longServername = longservername;
            this.dbset = DbConfig.GetSetting(longservername);
            this.dbobj = DBOMaker.CreateDbObj(this.dbset.DbType);
            this.dbobj.DbConnectStr = this.dbset.ConnectStr;
            this.lblServer.Text = this.dbset.Server;
            this.CreatListView();
            this.WorkLoad();
            this.cmboxType.SelectedIndex = 0;
        }

        private void WorkLoad()
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
                    b = this.dbset.DbName;
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
                    goto IL_187;
                }
                using (List<string>.Enumerator enumerator = dBList.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        string current = enumerator.Current;
                        this.cmbDB.Items.Add(current);
                    }
                    goto IL_187;
                }
            }
            this.cmbDB.Items.Add(this.dbset.DbName);
        IL_187:
            if (this.cmbDB.Items.Count > 0)
            {
                this.cmbDB.SelectedIndex = 0;
            }
        }
        private void CreatListView()
        {
            this.listView1.Columns.Clear();
            this.listView1.Items.Clear();
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.GridLines = true;
            this.listView1.FullRowSelect = true;
            this.listView1.CheckBoxes = false;
            this.listView1.Columns.Add("选择", 40, HorizontalAlignment.Left);
            this.listView1.Columns.Add("表名", 200, HorizontalAlignment.Left);
            this.listView1.Columns.Add("字段", 200, HorizontalAlignment.Left);
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.mythread = new Thread(new ThreadStart(this.SearchThread));
                this.mythread.Start();
            }
            catch (Exception ex)
            {
                LogInfo.WriteLog(ex);
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
        private void SearchThread()
        {
            this.currentDbname = this.cmbDB.Text;
            string value = this.txtKeyWord.Text.Trim().ToLower();
            List<string> tables = this.dbobj.GetTables(this.currentDbname);
            this.SetprogressBar1Max(tables.Count);
            this.SetprogressBar1Val(1);
            if (this.chkClearList.Checked)
            {
                this.listView1.Items.Clear();
            }
            int num = 0;
            int num2 = 0;
            foreach (string current in tables)
            {
                if (this.cmboxType.SelectedIndex == 2 && current.ToLower().IndexOf(value) > -1)
                {
                    ListViewItem listViewItem = new ListViewItem("", 0);
                    listViewItem.Checked = true;
                    listViewItem.SubItems.Add(current);
                    listViewItem.SubItems.Add("");
                    this.listView1.Items.AddRange(new ListViewItem[]
					{
						listViewItem
					});
                    num++;
                }
                else
                {
                    List<ColumnInfo> columnList = this.dbobj.GetColumnList(this.currentDbname, current);
                    foreach (ColumnInfo current2 in columnList)
                    {
                        this.SetlblStatuText(current + "-" + current2);
                        if (this.cmboxType.SelectedIndex == 0 && current2.ColumnName.ToLower().IndexOf(value) > -1)
                        {
                            ListViewItem listViewItem2 = new ListViewItem("", 0);
                            listViewItem2.Checked = true;
                            listViewItem2.SubItems.Add(current);
                            listViewItem2.SubItems.Add(current2.ColumnName);
                            this.listView1.Items.AddRange(new ListViewItem[]
							{
								listViewItem2
							});
                            num++;
                        }
                        if (this.cmboxType.SelectedIndex == 1 && current2.TypeName.ToLower().Equals(value))
                        {
                            ListViewItem listViewItem3 = new ListViewItem("", 0);
                            listViewItem3.Checked = true;
                            listViewItem3.SubItems.Add(current);
                            listViewItem3.SubItems.Add(current2.ColumnName);
                            this.listView1.Items.AddRange(new ListViewItem[]
							{
								listViewItem3
							});
                            num++;
                        }
                    }
                }
                this.SetprogressBar1Val(num2 + 1);
                num2++;
            }
            this.SetlblStatuText("共搜索到 " + num.ToString() + "个结果。");
        }
        private void btnExpToWord_Click(object sender, EventArgs e)
        {
            try
            {
                this.currentDbname = this.cmbDB.Text;
                this.mythread = new Thread(new ThreadStart(this.ThreadWork));
                this.mythread.Start();
            }
            catch (Exception ex)
            {
                LogInfo.WriteLog(ex);
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
        private void ThreadWork()
        {
            try
            {
                string text = "数据库名：" + this.currentDbname;
                int count = this.listView1.Items.Count;
                this.SetprogressBar1Max(count);
                this.SetprogressBar1Val(1);
                this.SetlblStatuText("0");
                object value = Missing.Value;
                object obj = "\\endofdoc";
                _Application application = new Microsoft.Office.Interop.Word.Application();
                application.Visible = false;
                _Document document = application.Documents.Add(ref value, ref value, ref value, ref value);
                application.ActiveWindow.View.Type = WdViewType.wdOutlineView;
                application.ActiveWindow.View.SeekView = WdSeekView.wdSeekPrimaryHeader;
                application.ActiveWindow.ActivePane.Selection.InsertAfter("动软自动生成器 www.maticsoft.com");
                application.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                application.ActiveWindow.View.SeekView = WdSeekView.wdSeekMainDocument;
                Paragraph paragraph = document.Content.Paragraphs.Add(ref value);
                paragraph.Range.Text = text;
                paragraph.Range.Font.Bold = 1;
                paragraph.Range.Font.Name = "宋体";
                paragraph.Range.Font.Size = 12f;
                paragraph.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                paragraph.Format.SpaceAfter = 5f;
                paragraph.Range.InsertParagraphAfter();
                System.Data.DataTable tablesExProperty = this.dbobj.GetTablesExProperty(this.currentDbname);
                List<string> list = new List<string>();
                int num = 0;
                foreach (ListViewItem listViewItem in this.listView1.SelectedItems)
                {
                    string text2 = listViewItem.SubItems[1].Text;
                    string text3 = "表名：" + text2;
                    if (!list.Contains(text2))
                    {
                        list.Add(text2);
                        List<ColumnInfo> columnInfoList = this.dbobj.GetColumnInfoList(this.currentDbname, text2);
                        int count2 = columnInfoList.Count;
                        if (columnInfoList != null && columnInfoList.Count > 0)
                        {
                            object range = document.Bookmarks[ref obj].Range;
                            Paragraph paragraph2 = document.Content.Paragraphs.Add(ref range);
                            paragraph2.Range.Text = text3;
                            paragraph2.Range.Font.Bold = 1;
                            paragraph2.Range.Font.Name = "宋体";
                            paragraph2.Range.Font.Size = 10f;
                            paragraph2.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                            paragraph2.Format.SpaceBefore = 15f;
                            paragraph2.Format.SpaceAfter = 1f;
                            paragraph2.Range.InsertParagraphAfter();
                            string text4 = "";
                            if (tablesExProperty != null)
                            {
                                try
                                {
                                    DataRow[] array = tablesExProperty.Select("objname='" + text2 + "'");
                                    if (array.Length > 0 && array[0]["value"] != null)
                                    {
                                        text4 = array[0]["value"].ToString();
                                    }
                                }
                                catch
                                {
                                }
                            }
                            range = document.Bookmarks[ref obj].Range;
                            Paragraph paragraph3 = document.Content.Paragraphs.Add(ref range);
                            paragraph3.Range.Text = text4;
                            paragraph3.Range.Font.Bold = 0;
                            paragraph3.Range.Font.Name = "宋体";
                            paragraph3.Range.Font.Size = 9f;
                            paragraph3.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                            paragraph3.Format.SpaceBefore = 1f;
                            paragraph3.Format.SpaceAfter = 1f;
                            paragraph3.Range.InsertParagraphAfter();
                            Range range2 = document.Bookmarks[ref obj].Range;
                            Table table = document.Tables.Add(range2, count2 + 1, 10, ref value, ref value);
                            table.Range.Font.Name = "宋体";
                            table.Range.Font.Size = 9f;
                            table.Borders.Enable = 1;
                            table.Rows.Height = 10f;
                            table.AllowAutoFit = true;
                            table.Cell(1, 1).Range.Text = "序号";
                            table.Cell(1, 2).Range.Text = "列名";
                            table.Cell(1, 3).Range.Text = "数据类型";
                            table.Cell(1, 4).Range.Text = "长度";
                            table.Cell(1, 5).Range.Text = "小数位";
                            table.Cell(1, 6).Range.Text = "标识";
                            table.Cell(1, 7).Range.Text = "主键";
                            table.Cell(1, 8).Range.Text = "允许空";
                            table.Cell(1, 9).Range.Text = "默认值";
                            table.Cell(1, 10).Range.Text = "说明";
                            table.Columns[1].Width = 33f;
                            table.Columns[3].Width = 60f;
                            table.Columns[4].Width = 33f;
                            table.Columns[5].Width = 43f;
                            table.Columns[6].Width = 33f;
                            table.Columns[7].Width = 33f;
                            table.Columns[8].Width = 43f;
                            for (int i = 0; i < count2; i++)
                            {
                                ColumnInfo columnInfo = columnInfoList[i];
                                string columnOrder = columnInfo.ColumnOrder;
                                string columnName = columnInfo.ColumnName;
                                string typeName = columnInfo.TypeName;
                                string text5 = columnInfo.Length;
                                string scale = columnInfo.Scale;
                                string text6 = (columnInfo.IsIdentity.ToString().ToLower() == "true") ? "是" : "";
                                string text7 = (columnInfo.IsPrimaryKey.ToString().ToLower() == "true") ? "是" : "";
                                string text8 = (columnInfo.Nullable.ToString().ToLower() == "true") ? "是" : "否";
                                string text9 = columnInfo.DefaultVal.ToString();
                                string text10 = columnInfo.Description.ToString();
                                if (text5.Trim() == "-1")
                                {
                                    text5 = "MAX";
                                }
                                table.Cell(i + 2, 1).Range.Text = columnOrder;
                                table.Cell(i + 2, 2).Range.Text = columnName;
                                table.Cell(i + 2, 3).Range.Text = typeName;
                                table.Cell(i + 2, 4).Range.Text = text5;
                                table.Cell(i + 2, 5).Range.Text = scale;
                                table.Cell(i + 2, 6).Range.Text = text6;
                                table.Cell(i + 2, 7).Range.Text = text7;
                                table.Cell(i + 2, 8).Range.Text = text8;
                                table.Cell(i + 2, 9).Range.Text = text9;
                                table.Cell(i + 2, 10).Range.Text = text10;
                            }
                            table.Rows[1].Range.Font.Bold = 1;
                            table.Rows[1].Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                            table.Rows.First.Shading.Texture = WdTextureIndex.wdTexture25Percent;
                        }
                        this.SetprogressBar1Val(num + 1);
                        this.SetlblStatuText("已生成" + (num + 1).ToString());
                        num++;
                    }
                }
                application.Visible = true;
                document.Activate();
                MessageBox.Show("文档生成成功！", "完成", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            catch (Exception ex)
            {
                LogInfo.WriteLog(ex);
                MessageBox.Show("文档生成失败！(" + ex.Message + ")。\r\n请关闭重试。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        public void SetlblStatuText(string text)
        {
            if (this.lblTip.InvokeRequired)
            {
                SearchTables.SetlblStatuCallback method = new SearchTables.SetlblStatuCallback(this.SetlblStatuText);
                base.Invoke(method, new object[]
				{
					text
				});
                return;
            }
            this.lblTip.Text = text;
        }
        public void SetprogressBar1Max(int val)
        {
            if (this.progressBar1.InvokeRequired)
            {
                SearchTables.SetProBar1MaxCallback method = new SearchTables.SetProBar1MaxCallback(this.SetprogressBar1Max);
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
                SearchTables.SetProBar1ValCallback method = new SearchTables.SetProBar1ValCallback(this.SetprogressBar1Val);
                base.Invoke(method, new object[]
				{
					val
				});
                return;
            }
            this.progressBar1.Value = val;
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            base.Close();
        }
        private void 全部表名ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringPlus stringPlus = new StringPlus();
            List<string> list = new List<string>();
            foreach (ListViewItem listViewItem in this.listView1.Items)
            {
                string text = listViewItem.SubItems[1].Text;
                if (!list.Contains(text))
                {
                    list.Add(text);
                    stringPlus.AppendLine(text);
                }
            }
            Clipboard.SetDataObject(stringPlus.Value);
        }
        private void 所选表名ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringPlus stringPlus = new StringPlus();
            foreach (ListViewItem listViewItem in this.listView1.SelectedItems)
            {
                stringPlus.AppendLine(listViewItem.SubItems[1].Text);
            }
            Clipboard.SetDataObject(stringPlus.Value);
        }
        private void 所选字段ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringPlus stringPlus = new StringPlus();
            foreach (ListViewItem listViewItem in this.listView1.SelectedItems)
            {
                stringPlus.AppendLine(listViewItem.SubItems[2].Text);
            }
            Clipboard.SetDataObject(stringPlus.Value);
        }
        private void 全部字段ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringPlus stringPlus = new StringPlus();
            List<string> list = new List<string>();
            foreach (ListViewItem listViewItem in this.listView1.Items)
            {
                string text = listViewItem.SubItems[2].Text;
                if (!list.Contains(text))
                {
                    list.Add(text);
                    stringPlus.AppendLine(text);
                }
            }
            Clipboard.SetDataObject(stringPlus.Value);
        }
        private void 移除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in this.listView1.SelectedItems)
            {
                this.listView1.Items.Remove(item);
            }
        }
        private void 代码生成器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.longServername == "" || this.currentDbname == "")
            {
                return;
            }
            if (this.listView1.SelectedItems.Count > 0)
            {
                string text = this.listView1.SelectedItems[0].SubItems[1].Text;
                this.mainfrm.AddSinglePage(new CodeMaker(this.longServername, this.currentDbname, text), "代码生成器");
                base.Close();
            }
        }
    }
}
