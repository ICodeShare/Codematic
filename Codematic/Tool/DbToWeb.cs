using Maticsoft.CmConfig;
using Maticsoft.CodeHelper;
using Maticsoft.DBFactory;
using Maticsoft.IDBO;
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
    public partial class DbToWeb : Form
    {

        private delegate void SetBtnEnableCallback();
        private delegate void SetBtnDisableCallback();
        private delegate void SetlblStatuCallback(string text);
        private delegate void SetProBar1MaxCallback(int val);
        private delegate void SetProBar1ValCallback(int val);
        private Loading load = new Loading();
        private Thread mythread;
        private IDbObject dbobj;
        private DbSettings dbset;
        private Label label5;
        private string dbname = "";
        public DbToWeb(string longservername)
        {
            InitializeComponent();
            this.dbset = DbConfig.GetSetting(longservername);
            this.dbobj = DBOMaker.CreateDbObj(this.dbset.DbType);
            this.dbobj.DbConnectStr = this.dbset.ConnectStr;
            this.lblServer.Text = this.dbset.Server;
        }

        private void DbToWeb_Load(object sender, EventArgs e)
		{
			this.btn_Creat.Enabled = false;
			this.ThreadWorkLoad();
			this.comboBox1.SelectedIndex = 0;
		}
		private void ThreadWorkLoad()
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
					goto IL_189;
				}
				using (List<string>.Enumerator enumerator = dBList.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string current = enumerator.Current;
						this.cmbDB.Items.Add(current);
					}
					goto IL_189;
				}
			}
			this.cmbDB.Items.Add(this.dbset.DbName);
			IL_189:
			if (this.cmbDB.Items.Count > 0)
			{
				this.cmbDB.SelectedIndex = 0;
				return;
			}
			List<string> tables = this.dbobj.GetTables("");
			this.listTable1.Items.Clear();
			this.listTable2.Items.Clear();
			if (tables.Count > 0)
			{
				this.SetprogressBar1Max(tables.Count);
				this.SetprogressBar1Val(1);
				this.SetlblStatuText("");
				int val = 1;
				foreach (string current2 in tables)
				{
					this.listTable1.Items.Add(current2);
					this.SetprogressBar1Val(val);
					this.SetlblStatuText(current2);
				}
			}
		}
		private void cmbDB_SelectedIndexChanged(object sender, EventArgs e)
		{
			string text = this.cmbDB.Text;
			List<string> tables = this.dbobj.GetTables(text);
			this.listTable1.Items.Clear();
			this.listTable2.Items.Clear();
			if (tables.Count > 0)
			{
				foreach (string current in tables)
				{
					this.listTable1.Items.Add(current);
				}
			}
			this.IsHasItem();
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
			ListBox.SelectedObjectCollection arg_1C_0 = this.listTable2.SelectedItems;
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
		private void listTable1_Click(object sender, EventArgs e)
		{
			if (this.listTable1.SelectedItem != null)
			{
				this.IsHasItem();
			}
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
				this.btn_Creat.Enabled = true;
				return;
			}
			this.btn_Del.Enabled = false;
			this.btn_Dellist.Enabled = false;
			this.btn_Creat.Enabled = false;
		}
		public void SetBtnEnable()
		{
			if (this.btn_Creat.InvokeRequired)
			{
				DbToWeb.SetBtnEnableCallback method = new DbToWeb.SetBtnEnableCallback(this.SetBtnEnable);
				base.Invoke(method, null);
				return;
			}
			this.btn_Creat.Enabled = true;
		}
		public void SetBtnDisable()
		{
			if (this.btn_Creat.InvokeRequired)
			{
				DbToWeb.SetBtnDisableCallback method = new DbToWeb.SetBtnDisableCallback(this.SetBtnDisable);
				base.Invoke(method, null);
				return;
			}
			this.btn_Creat.Enabled = false;
		}
		public void SetlblStatuText(string text)
		{
			if (this.labelNum.InvokeRequired)
			{
				DbToWeb.SetlblStatuCallback method = new DbToWeb.SetlblStatuCallback(this.SetlblStatuText);
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
				DbToWeb.SetProBar1MaxCallback method = new DbToWeb.SetProBar1MaxCallback(this.SetprogressBar1Max);
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
				DbToWeb.SetProBar1ValCallback method = new DbToWeb.SetProBar1ValCallback(this.SetprogressBar1Val);
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
		private void btn_Creat_Click(object sender, EventArgs e)
		{
			try
			{
				this.dbname = this.cmbDB.Text;
				this.pictureBox1.Visible = true;
				this.mythread = new Thread(new ThreadStart(this.ThreadWorkhtml));
				this.mythread.Start();
			}
			catch (Exception ex)
			{
				LogInfo.WriteLog(ex);
				MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
		}
		private void ThreadWorkhtml()
		{
			try
			{
				this.SetBtnDisable();
				string str = "数据库名：" + this.dbname;
				int count = this.listTable2.Items.Count;
				this.SetprogressBar1Max(count);
				this.SetprogressBar1Val(1);
				this.SetlblStatuText("0");
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("<div class=\"styledb\">" + str + "</div>");
				DataTable tablesExProperty = this.dbobj.GetTablesExProperty(this.dbname);
				for (int i = 0; i < count; i++)
				{
					string text = this.listTable2.Items[i].ToString();
					string str2 = "表名：" + text;
					string str3 = "";
					if (tablesExProperty != null)
					{
						try
						{
							DataRow[] array = tablesExProperty.Select("objname='" + text + "'");
							if (array.Length > 0 && array[0]["value"] != null)
							{
								str3 = array[0]["value"].ToString();
							}
						}
						catch
						{
						}
					}
					List<ColumnInfo> columnInfoList = this.dbobj.GetColumnInfoList(this.dbname, text);
					int count2 = columnInfoList.Count;
					if (columnInfoList != null && columnInfoList.Count > 0)
					{
						stringBuilder.Append("<div class=\"styletab\">" + str2 + "</div>");
						stringBuilder.Append("<div  align=\"left\">" + str3 + "</div>");
						stringBuilder.Append("<div><table border=\"0\" cellpadding=\"5\" cellspacing=\"0\" width=\"90%\">");
						if (this.comboBox1.SelectedIndex == 0)
						{
							stringBuilder.Append("<tr><td bgcolor=\"#FBFBFB\">");
							stringBuilder.Append("<table cellspacing=\"0\" cellpadding=\"5\" border=\"1\" width=\"100%\" bordercolorlight=\"#D7D7E5\" bordercolordark=\"#D3D8E0\">");
							stringBuilder.Append("<tr bgcolor=\"#F0F0F0\">");
						}
						else
						{
							stringBuilder.Append("<tr><td bgcolor=\"#F5F9FF\">");
							stringBuilder.Append("<table cellspacing=\"0\" cellpadding=\"5\" border=\"1\" width=\"100%\" bordercolorlight=\"#4F7FC9\" bordercolordark=\"#D3D8E0\">");
							stringBuilder.Append("<tr bgcolor=\"#E3EFFF\">");
						}
						stringBuilder.Append("<td>序号</td>");
						stringBuilder.Append("<td>列名</td>");
						stringBuilder.Append("<td>数据类型</td>");
						stringBuilder.Append("<td>长度</td>");
						stringBuilder.Append("<td>小数位</td>");
						stringBuilder.Append("<td>标识</td>");
						stringBuilder.Append("<td>主键</td>");
						stringBuilder.Append("<td>允许空</td>");
						stringBuilder.Append("<td>默认值</td>");
						stringBuilder.Append("<td>说明</td>");
						stringBuilder.Append("</tr>");
						for (int j = 0; j < count2; j++)
						{
							ColumnInfo columnInfo = columnInfoList[j];
							string columnOrder = columnInfo.ColumnOrder;
							string columnName = columnInfo.ColumnName;
							string typeName = columnInfo.TypeName;
							string text2 = (columnInfo.Length == "") ? "&nbsp;" : columnInfo.Length;
							string str4 = (columnInfo.Scale == "") ? "&nbsp;" : columnInfo.Scale;
							string str5 = (columnInfo.IsIdentity.ToString().ToLower() == "true") ? "是" : "&nbsp;";
							string str6 = (columnInfo.IsPrimaryKey.ToString().ToLower() == "true") ? "是" : "&nbsp;";
							string str7 = (columnInfo.Nullable.ToString().ToLower() == "true") ? "是" : "否";
							string str8 = (columnInfo.DefaultVal.ToString().Trim() == "") ? "&nbsp;" : columnInfo.DefaultVal.ToString();
							string str9 = (columnInfo.Description.ToString().Trim() == "") ? "&nbsp;" : columnInfo.Description.ToString();
							if (text2.Trim() == "-1")
							{
								text2 = "MAX";
							}
							stringBuilder.Append("<tr>");
							stringBuilder.Append("<td>" + columnOrder + "</td>");
							stringBuilder.Append("<td>" + columnName + "</td>");
							stringBuilder.Append("<td>" + typeName + "</td>");
							stringBuilder.Append("<td>" + text2 + "</td>");
							stringBuilder.Append("<td>" + str4 + "</td>");
							stringBuilder.Append("<td>" + str5 + "</td>");
							stringBuilder.Append("<td>" + str6 + "</td>");
							stringBuilder.Append("<td>" + str7 + "</td>");
							stringBuilder.Append("<td>" + str8 + "</td>");
							stringBuilder.Append("<td align=\"left\" >" + str9 + "</td>");
							stringBuilder.Append("</tr>");
						}
					}
					stringBuilder.Append("</table>");
					stringBuilder.Append("</td>");
					stringBuilder.Append("</tr>");
					stringBuilder.Append("</table>");
					stringBuilder.Append("</div>");
					this.SetprogressBar1Val(i + 1);
					this.SetlblStatuText((i + 1).ToString());
				}
				string value = "";
				string path = Application.StartupPath + "\\Template\\table.htm";
				if (File.Exists(path))
				{
					using (StreamReader streamReader = new StreamReader(path, Encoding.Default))
					{
						value = streamReader.ReadToEnd().Replace("<$$tablestruct$$>", stringBuilder.ToString());
						streamReader.Close();
					}
					SaveFileDialog saveFileDialog = new SaveFileDialog();
					saveFileDialog.Title = "保存表结构";
					saveFileDialog.Filter = "htm files (*.htm)|*.htm|All files (*.*)|*.*";
					DialogResult dialogResult = saveFileDialog.ShowDialog(this);
					if (dialogResult == DialogResult.OK)
					{
						string fileName = saveFileDialog.FileName;
						StreamWriter streamWriter = new StreamWriter(fileName, false, Encoding.Default);
						streamWriter.Write(value);
						streamWriter.Flush();
						streamWriter.Close();
					}
				}
				this.SetBtnEnable();
				MessageBox.Show(this, "文档生成成功！", "完成", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
			catch (Exception ex)
			{
				LogInfo.WriteLog(ex);
				MessageBox.Show(this, "文档生成失败！(" + ex.Message + ")。\r\n请关闭重试。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
	}
}
