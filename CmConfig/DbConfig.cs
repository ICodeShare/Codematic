using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Windows.Forms;
namespace Maticsoft.CmConfig
{
	public class DbConfig
	{
		private static string fileName = Application.StartupPath + "\\DbSetting.config";
		public static DbSettings[] GetSettings()
		{
			DbSettings[] result;
			try
			{
				DataSet dataSet = new DataSet();
				ArrayList arrayList = new ArrayList();
				if (File.Exists(DbConfig.fileName))
				{
					dataSet.ReadXml(DbConfig.fileName);
					if (dataSet.Tables.Count > 0)
					{
						foreach (DataRow dr in dataSet.Tables[0].Rows)
						{
							DbSettings value = DbConfig.TranDbSettings(dataSet, dr);
							arrayList.Add(value);
						}
					}
				}
				DbSettings[] array = (DbSettings[])arrayList.ToArray(typeof(DbSettings));
				result = array;
			}
			catch
			{
				result = null;
			}
			return result;
		}
		public static DbSettings TranDbSettings(DataSet ds, DataRow dr)
		{
			DbSettings dbSettings = new DbSettings();
			dbSettings.DbType = dr["DbType"].ToString();
			dbSettings.Server = dr["Server"].ToString();
			dbSettings.ConnectStr = dr["ConnectStr"].ToString();
			dbSettings.DbName = dr["DbName"].ToString();
			if (ds.Tables[0].Columns.Contains("ConnectSimple") && dr["ConnectSimple"] != null && dr["ConnectSimple"].ToString().Length > 0)
			{
				dbSettings.ConnectSimple = bool.Parse(dr["ConnectSimple"].ToString());
			}
			if (ds.Tables[0].Columns.Contains("TabLoadtype") && dr["TabLoadtype"] != null && dr["TabLoadtype"].ToString().Length > 0)
			{
				dbSettings.TabLoadtype = int.Parse(dr["TabLoadtype"].ToString());
			}
			if (ds.Tables[0].Columns.Contains("TabLoadKeyword") && dr["TabLoadKeyword"] != null && dr["TabLoadKeyword"].ToString().Length > 0)
			{
				dbSettings.TabLoadKeyword = dr["TabLoadKeyword"].ToString();
			}
			if (ds.Tables[0].Columns.Contains("ProcPrefix") && dr["ProcPrefix"] != null)
			{
				dbSettings.ProcPrefix = dr["ProcPrefix"].ToString();
			}
			if (ds.Tables[0].Columns.Contains("ProjectName") && dr["ProjectName"] != null)
			{
				dbSettings.ProjectName = dr["ProjectName"].ToString();
			}
			if (ds.Tables[0].Columns.Contains("Namepace") && dr["Namepace"] != null && dr["Namepace"].ToString().Length > 0)
			{
				dbSettings.Namepace = dr["Namepace"].ToString();
			}
			if (ds.Tables[0].Columns.Contains("Folder") && dr["Folder"] != null)
			{
				dbSettings.Folder = dr["Folder"].ToString();
			}
			if (ds.Tables[0].Columns.Contains("AppFrame") && dr["AppFrame"] != null && dr["AppFrame"].ToString().Length > 0)
			{
				dbSettings.AppFrame = dr["AppFrame"].ToString();
			}
			if (ds.Tables[0].Columns.Contains("DALType") && dr["DALType"] != null && dr["DALType"].ToString().Length > 0)
			{
				dbSettings.DALType = dr["DALType"].ToString();
			}
			if (ds.Tables[0].Columns.Contains("BLLType") && dr["BLLType"] != null && dr["BLLType"].ToString().Length > 0)
			{
				dbSettings.BLLType = dr["BLLType"].ToString();
			}
			if (ds.Tables[0].Columns.Contains("WebType") && dr["WebType"] != null && dr["WebType"].ToString().Length > 0)
			{
				dbSettings.WebType = dr["WebType"].ToString();
			}
			if (ds.Tables[0].Columns.Contains("EditFont") && dr["EditFont"] != null && dr["EditFont"].ToString().Length > 0)
			{
				dbSettings.EditFont = dr["EditFont"].ToString();
			}
			if (ds.Tables[0].Columns.Contains("EditFontSize") && dr["EditFontSize"] != null && dr["EditFontSize"].ToString().Length > 0)
			{
				dbSettings.EditFontSize = float.Parse(dr["EditFontSize"].ToString());
			}
			if (ds.Tables[0].Columns.Contains("DbHelperName") && dr["DbHelperName"] != null && dr["DbHelperName"].ToString().Length > 0)
			{
				dbSettings.DbHelperName = dr["DbHelperName"].ToString();
			}
			if (ds.Tables[0].Columns.Contains("ModelPrefix") && dr["ModelPrefix"] != null)
			{
				dbSettings.ModelPrefix = dr["ModelPrefix"].ToString();
			}
			if (ds.Tables[0].Columns.Contains("ModelSuffix") && dr["ModelSuffix"] != null)
			{
				dbSettings.ModelSuffix = dr["ModelSuffix"].ToString();
			}
			if (ds.Tables[0].Columns.Contains("BLLPrefix") && dr["BLLPrefix"] != null)
			{
				dbSettings.BLLPrefix = dr["BLLPrefix"].ToString();
			}
			if (ds.Tables[0].Columns.Contains("BLLSuffix") && dr["BLLSuffix"] != null)
			{
				dbSettings.BLLSuffix = dr["BLLSuffix"].ToString();
			}
			if (ds.Tables[0].Columns.Contains("DALPrefix") && dr["DALPrefix"] != null)
			{
				dbSettings.DALPrefix = dr["DALPrefix"].ToString();
			}
			if (ds.Tables[0].Columns.Contains("DALSuffix") && dr["DALSuffix"] != null)
			{
				dbSettings.DALSuffix = dr["DALSuffix"].ToString();
			}
			if (ds.Tables[0].Columns.Contains("TabNameRule") && dr["TabNameRule"] != null && dr["TabNameRule"].ToString().Length > 0)
			{
				dbSettings.TabNameRule = dr["TabNameRule"].ToString();
			}
			if (ds.Tables[0].Columns.Contains("WebTemplatePath") && dr["WebTemplatePath"] != null && dr["WebTemplatePath"].ToString().Length > 0)
			{
				dbSettings.WebTemplatePath = dr["WebTemplatePath"].ToString();
			}
			if (ds.Tables[0].Columns.Contains("ReplacedOldStr") && dr["ReplacedOldStr"] != null && dr["ReplacedOldStr"].ToString().Length > 0)
			{
				dbSettings.ReplacedOldStr = dr["ReplacedOldStr"].ToString();
			}
			if (ds.Tables[0].Columns.Contains("ReplacedNewStr") && dr["ReplacedNewStr"] != null && dr["ReplacedNewStr"].ToString().Length > 0)
			{
				dbSettings.ReplacedNewStr = dr["ReplacedNewStr"].ToString();
			}
			return dbSettings;
		}
		public static DataSet GetSettingDs()
		{
			DataSet result;
			try
			{
				DataSet dataSet = new DataSet();
				if (File.Exists(DbConfig.fileName))
				{
					dataSet.ReadXml(DbConfig.fileName);
				}
				result = dataSet;
			}
			catch
			{
				result = null;
			}
			return result;
		}
		public static DbSettings GetSetting(string loneServername)
		{
			string dbName = "";
			string serverip;
			string dbType;
			if (loneServername.StartsWith("(local)"))
			{
				int num = 7;
				serverip = "(local)";
				int num2 = loneServername.IndexOf(")", num);
				dbType = loneServername.Substring(num + 1, num2 - num - 1);
				if (loneServername.Length > num2 + 1)
				{
					dbName = loneServername.Substring(num2 + 2).Replace(")", "");
				}
			}
			else
			{
				int num3 = loneServername.IndexOf("(");
				serverip = loneServername.Substring(0, num3);
				int num4 = loneServername.IndexOf(")", num3);
				dbType = loneServername.Substring(num3 + 1, num4 - num3 - 1);
				if (loneServername.Length > num4 + 1)
				{
					dbName = loneServername.Substring(num4 + 2).Replace(")", "");
				}
			}
			return DbConfig.GetSetting(dbType, serverip, dbName);
		}
		public static DbSettings GetSetting(string DbType, string Serverip, string DbName)
		{
			DbSettings result;
			try
			{
				DbSettings dbSettings = null;
				DataSet dataSet = new DataSet();
				if (File.Exists(DbConfig.fileName))
				{
					dataSet.ReadXml(DbConfig.fileName);
					if (dataSet.Tables.Count > 0)
					{
						string text = string.Concat(new string[]
						{
							"DbType='",
							DbType,
							"' and Server='",
							Serverip,
							"'"
						});
						if (DbName.Trim() != "")
						{
							text = text + " and DbName='" + DbName + "'";
						}
						DataRow[] array = dataSet.Tables[0].Select(text);
						if (array.Length > 0)
						{
							DataRow dr = array[0];
							dbSettings = DbConfig.TranDbSettings(dataSet, dr);
						}
					}
				}
				result = dbSettings;
			}
			catch
			{
				result = null;
			}
			return result;
		}
		public static DataTable CreateDataTable()
		{
			DataTable dataTable = new DataTable("DBServer");
			DataColumn dataColumn = new DataColumn();
			dataColumn.DataType = Type.GetType("System.String");
			dataColumn.ColumnName = "DbType";
			dataTable.Columns.Add(dataColumn);
			dataColumn = new DataColumn();
			dataColumn.DataType = Type.GetType("System.String");
			dataColumn.ColumnName = "Server";
			dataTable.Columns.Add(dataColumn);
			dataColumn = new DataColumn();
			dataColumn.DataType = Type.GetType("System.String");
			dataColumn.ColumnName = "ConnectStr";
			dataTable.Columns.Add(dataColumn);
			dataColumn = new DataColumn();
			dataColumn.DataType = Type.GetType("System.String");
			dataColumn.ColumnName = "DbName";
			dataTable.Columns.Add(dataColumn);
			dataColumn = new DataColumn();
			dataColumn.DataType = Type.GetType("System.Boolean");
			dataColumn.ColumnName = "ConnectSimple";
			dataTable.Columns.Add(dataColumn);
			dataColumn = new DataColumn();
			dataColumn.DataType = Type.GetType("System.Int32");
			dataColumn.ColumnName = "TabLoadtype";
			dataTable.Columns.Add(dataColumn);
			dataColumn = new DataColumn();
			dataColumn.DataType = Type.GetType("System.String");
			dataColumn.ColumnName = "TabLoadKeyword";
			dataTable.Columns.Add(dataColumn);
			dataColumn = new DataColumn();
			dataColumn.DataType = Type.GetType("System.String");
			dataColumn.ColumnName = "ProcPrefix";
			dataTable.Columns.Add(dataColumn);
			dataColumn = new DataColumn();
			dataColumn.DataType = Type.GetType("System.String");
			dataColumn.ColumnName = "ProjectName";
			dataTable.Columns.Add(dataColumn);
			dataColumn = new DataColumn();
			dataColumn.DataType = Type.GetType("System.String");
			dataColumn.ColumnName = "Namepace";
			dataTable.Columns.Add(dataColumn);
			dataColumn = new DataColumn();
			dataColumn.DataType = Type.GetType("System.String");
			dataColumn.ColumnName = "Folder";
			dataTable.Columns.Add(dataColumn);
			dataColumn = new DataColumn();
			dataColumn.DataType = Type.GetType("System.String");
			dataColumn.ColumnName = "AppFrame";
			dataTable.Columns.Add(dataColumn);
			dataColumn = new DataColumn();
			dataColumn.DataType = Type.GetType("System.String");
			dataColumn.ColumnName = "DALType";
			dataTable.Columns.Add(dataColumn);
			dataColumn = new DataColumn();
			dataColumn.DataType = Type.GetType("System.String");
			dataColumn.ColumnName = "BLLType";
			dataTable.Columns.Add(dataColumn);
			dataColumn = new DataColumn();
			dataColumn.DataType = Type.GetType("System.String");
			dataColumn.ColumnName = "WebType";
			dataTable.Columns.Add(dataColumn);
			dataColumn = new DataColumn();
			dataColumn.DataType = Type.GetType("System.String");
			dataColumn.ColumnName = "EditFont";
			dataTable.Columns.Add(dataColumn);
			dataColumn = new DataColumn();
			dataColumn.DataType = Type.GetType("System.Double");
			dataColumn.ColumnName = "EditFontSize";
			dataTable.Columns.Add(dataColumn);
			dataColumn = new DataColumn();
			dataColumn.DataType = Type.GetType("System.String");
			dataColumn.ColumnName = "DbHelperName";
			dataTable.Columns.Add(dataColumn);
			dataColumn = new DataColumn();
			dataColumn.DataType = Type.GetType("System.String");
			dataColumn.ColumnName = "ModelPrefix";
			dataTable.Columns.Add(dataColumn);
			dataColumn = new DataColumn();
			dataColumn.DataType = Type.GetType("System.String");
			dataColumn.ColumnName = "ModelSuffix";
			dataTable.Columns.Add(dataColumn);
			dataColumn = new DataColumn();
			dataColumn.DataType = Type.GetType("System.String");
			dataColumn.ColumnName = "BLLPrefix";
			dataTable.Columns.Add(dataColumn);
			dataColumn = new DataColumn();
			dataColumn.DataType = Type.GetType("System.String");
			dataColumn.ColumnName = "BLLSuffix";
			dataTable.Columns.Add(dataColumn);
			dataColumn = new DataColumn();
			dataColumn.DataType = Type.GetType("System.String");
			dataColumn.ColumnName = "DALPrefix";
			dataTable.Columns.Add(dataColumn);
			dataColumn = new DataColumn();
			dataColumn.DataType = Type.GetType("System.String");
			dataColumn.ColumnName = "DALSuffix";
			dataTable.Columns.Add(dataColumn);
			dataColumn = new DataColumn();
			dataColumn.DataType = Type.GetType("System.String");
			dataColumn.ColumnName = "TabNameRule";
			dataTable.Columns.Add(dataColumn);
			dataColumn = new DataColumn();
			dataColumn.DataType = Type.GetType("System.String");
			dataColumn.ColumnName = "WebTemplatePath";
			dataTable.Columns.Add(dataColumn);
			dataColumn = new DataColumn();
			dataColumn.DataType = Type.GetType("System.String");
			dataColumn.ColumnName = "ReplacedOldStr";
			dataTable.Columns.Add(dataColumn);
			dataColumn = new DataColumn();
			dataColumn.DataType = Type.GetType("System.String");
			dataColumn.ColumnName = "ReplacedNewStr";
			dataTable.Columns.Add(dataColumn);
			return dataTable;
		}
		public static void AddColForTable(DataTable table)
		{
			if (!table.Columns.Contains("DbType"))
			{
				DataColumn dataColumn = new DataColumn();
				dataColumn.DataType = Type.GetType("System.String");
				dataColumn.ColumnName = "DbType";
				table.Columns.Add(dataColumn);
			}
			if (!table.Columns.Contains("Server"))
			{
				DataColumn dataColumn = new DataColumn();
				dataColumn.DataType = Type.GetType("System.String");
				dataColumn.ColumnName = "Server";
				table.Columns.Add(dataColumn);
			}
			if (!table.Columns.Contains("ConnectStr"))
			{
				DataColumn dataColumn = new DataColumn();
				dataColumn.DataType = Type.GetType("System.String");
				dataColumn.ColumnName = "ConnectStr";
				table.Columns.Add(dataColumn);
			}
			if (!table.Columns.Contains("DbName"))
			{
				DataColumn dataColumn = new DataColumn();
				dataColumn.DataType = Type.GetType("System.String");
				dataColumn.ColumnName = "DbName";
				table.Columns.Add(dataColumn);
			}
			if (!table.Columns.Contains("ConnectSimple"))
			{
				DataColumn dataColumn = new DataColumn();
				dataColumn.DataType = Type.GetType("System.Boolean");
				dataColumn.ColumnName = "ConnectSimple";
				table.Columns.Add(dataColumn);
			}
			if (!table.Columns.Contains("TabLoadtype"))
			{
				DataColumn dataColumn = new DataColumn();
				dataColumn.DataType = Type.GetType("System.Int32");
				dataColumn.ColumnName = "TabLoadtype";
				table.Columns.Add(dataColumn);
			}
			if (!table.Columns.Contains("TabLoadKeyword"))
			{
				DataColumn dataColumn = new DataColumn();
				dataColumn.DataType = Type.GetType("System.String");
				dataColumn.ColumnName = "TabLoadKeyword";
				table.Columns.Add(dataColumn);
			}
			if (!table.Columns.Contains("ProcPrefix"))
			{
				DataColumn dataColumn = new DataColumn();
				dataColumn.DataType = Type.GetType("System.String");
				dataColumn.ColumnName = "ProcPrefix";
				table.Columns.Add(dataColumn);
			}
			if (!table.Columns.Contains("ProjectName"))
			{
				DataColumn dataColumn = new DataColumn();
				dataColumn.DataType = Type.GetType("System.String");
				dataColumn.ColumnName = "ProjectName";
				table.Columns.Add(dataColumn);
			}
			if (!table.Columns.Contains("Namepace"))
			{
				DataColumn dataColumn = new DataColumn();
				dataColumn.DataType = Type.GetType("System.String");
				dataColumn.ColumnName = "Namepace";
				table.Columns.Add(dataColumn);
			}
			if (!table.Columns.Contains("Folder"))
			{
				DataColumn dataColumn = new DataColumn();
				dataColumn.DataType = Type.GetType("System.String");
				dataColumn.ColumnName = "Folder";
				table.Columns.Add(dataColumn);
			}
			if (!table.Columns.Contains("AppFrame"))
			{
				DataColumn dataColumn = new DataColumn();
				dataColumn.DataType = Type.GetType("System.String");
				dataColumn.ColumnName = "AppFrame";
				table.Columns.Add(dataColumn);
			}
			if (!table.Columns.Contains("DALType"))
			{
				DataColumn dataColumn = new DataColumn();
				dataColumn.DataType = Type.GetType("System.String");
				dataColumn.ColumnName = "DALType";
				table.Columns.Add(dataColumn);
			}
			if (!table.Columns.Contains("BLLType"))
			{
				DataColumn dataColumn = new DataColumn();
				dataColumn.DataType = Type.GetType("System.String");
				dataColumn.ColumnName = "BLLType";
				table.Columns.Add(dataColumn);
			}
			if (!table.Columns.Contains("WebType"))
			{
				DataColumn dataColumn = new DataColumn();
				dataColumn.DataType = Type.GetType("System.String");
				dataColumn.ColumnName = "WebType";
				table.Columns.Add(dataColumn);
			}
			if (!table.Columns.Contains("EditFont"))
			{
				DataColumn dataColumn = new DataColumn();
				dataColumn.DataType = Type.GetType("System.String");
				dataColumn.ColumnName = "EditFont";
				table.Columns.Add(dataColumn);
			}
			if (!table.Columns.Contains("EditFontSize"))
			{
				DataColumn dataColumn = new DataColumn();
				dataColumn.DataType = Type.GetType("System.Double");
				dataColumn.ColumnName = "EditFontSize";
				table.Columns.Add(dataColumn);
			}
			if (!table.Columns.Contains("DbHelperName"))
			{
				DataColumn dataColumn = new DataColumn();
				dataColumn.DataType = Type.GetType("System.String");
				dataColumn.ColumnName = "DbHelperName";
				table.Columns.Add(dataColumn);
			}
			if (!table.Columns.Contains("ModelPrefix"))
			{
				DataColumn dataColumn = new DataColumn();
				dataColumn.DataType = Type.GetType("System.String");
				dataColumn.ColumnName = "ModelPrefix";
				table.Columns.Add(dataColumn);
			}
			if (!table.Columns.Contains("ModelSuffix"))
			{
				DataColumn dataColumn = new DataColumn();
				dataColumn.DataType = Type.GetType("System.String");
				dataColumn.ColumnName = "ModelSuffix";
				table.Columns.Add(dataColumn);
			}
			if (!table.Columns.Contains("BLLPrefix"))
			{
				DataColumn dataColumn = new DataColumn();
				dataColumn.DataType = Type.GetType("System.String");
				dataColumn.ColumnName = "BLLPrefix";
				table.Columns.Add(dataColumn);
			}
			if (!table.Columns.Contains("BLLSuffix"))
			{
				DataColumn dataColumn = new DataColumn();
				dataColumn.DataType = Type.GetType("System.String");
				dataColumn.ColumnName = "BLLSuffix";
				table.Columns.Add(dataColumn);
			}
			if (!table.Columns.Contains("DALPrefix"))
			{
				DataColumn dataColumn = new DataColumn();
				dataColumn.DataType = Type.GetType("System.String");
				dataColumn.ColumnName = "DALPrefix";
				table.Columns.Add(dataColumn);
			}
			if (!table.Columns.Contains("DALSuffix"))
			{
				DataColumn dataColumn = new DataColumn();
				dataColumn.DataType = Type.GetType("System.String");
				dataColumn.ColumnName = "DALSuffix";
				table.Columns.Add(dataColumn);
			}
			if (!table.Columns.Contains("TabNameRule"))
			{
				DataColumn dataColumn = new DataColumn();
				dataColumn.DataType = Type.GetType("System.String");
				dataColumn.ColumnName = "TabNameRule";
				table.Columns.Add(dataColumn);
			}
			if (!table.Columns.Contains("WebTemplatePath"))
			{
				DataColumn dataColumn = new DataColumn();
				dataColumn.DataType = Type.GetType("System.String");
				dataColumn.ColumnName = "WebTemplatePath";
				table.Columns.Add(dataColumn);
			}
			if (!table.Columns.Contains("ReplacedOldStr"))
			{
				DataColumn dataColumn = new DataColumn();
				dataColumn.DataType = Type.GetType("System.String");
				dataColumn.ColumnName = "ReplacedOldStr";
				table.Columns.Add(dataColumn);
			}
			if (!table.Columns.Contains("ReplacedNewStr"))
			{
				DataColumn dataColumn = new DataColumn();
				dataColumn.DataType = Type.GetType("System.String");
				dataColumn.ColumnName = "ReplacedNewStr";
				table.Columns.Add(dataColumn);
			}
		}
		private static DataRow NewDataRow(DataTable dt, DbSettings dbobj)
		{
			DbConfig.AddColForTable(dt);
			DataRow dataRow = dt.NewRow();
			dataRow["DbType"] = dbobj.DbType;
			dataRow["Server"] = dbobj.Server;
			dataRow["ConnectStr"] = dbobj.ConnectStr;
			dataRow["DbName"] = dbobj.DbName;
			dataRow["ConnectSimple"] = dbobj.ConnectSimple;
			dataRow["TabLoadtype"] = dbobj.TabLoadtype;
			dataRow["TabLoadKeyword"] = dbobj.TabLoadKeyword;
			dataRow["ProcPrefix"] = dbobj.ProcPrefix;
			dataRow["ProjectName"] = dbobj.ProjectName;
			dataRow["Namepace"] = dbobj.Namepace;
			dataRow["Folder"] = dbobj.Folder;
			dataRow["AppFrame"] = dbobj.AppFrame;
			dataRow["DALType"] = dbobj.DALType;
			dataRow["BLLType"] = dbobj.BLLType;
			dataRow["WebType"] = dbobj.WebType;
			dataRow["EditFont"] = dbobj.EditFont;
			dataRow["EditFontSize"] = dbobj.EditFontSize;
			dataRow["DbHelperName"] = dbobj.DbHelperName;
			dataRow["ModelPrefix"] = dbobj.ModelPrefix;
			dataRow["ModelSuffix"] = dbobj.ModelSuffix;
			dataRow["BLLPrefix"] = dbobj.BLLPrefix;
			dataRow["BLLSuffix"] = dbobj.BLLSuffix;
			dataRow["DALPrefix"] = dbobj.DALPrefix;
			dataRow["DALSuffix"] = dbobj.DALSuffix;
			dataRow["TabNameRule"] = dbobj.TabNameRule;
			dataRow["WebTemplatePath"] = dbobj.WebTemplatePath;
			dataRow["ReplacedOldStr"] = dbobj.ReplacedOldStr;
			dataRow["ReplacedNewStr"] = dbobj.ReplacedNewStr;
			return dataRow;
		}
		public static int AddSettings(DbSettings dbobj)
		{
			int result;
			try
			{
				DataSet dataSet = new DataSet();
				if (!File.Exists(DbConfig.fileName))
				{
					DataTable dataTable = DbConfig.CreateDataTable();
					DataRow row = DbConfig.NewDataRow(dataTable, dbobj);
					dataTable.Rows.Add(row);
					dataSet.Tables.Add(dataTable);
				}
				else
				{
					dataSet.ReadXml(DbConfig.fileName);
					if (dataSet.Tables.Count > 0)
					{
						DataRow[] array = dataSet.Tables[0].Select(string.Concat(new string[]
						{
							"DbType='",
							dbobj.DbType,
							"' and Server='",
							dbobj.Server,
							"' and DbName='",
							dbobj.DbName,
							"'"
						}));
						if (array.Length > 0)
						{
							result = 2;
							return result;
						}
						DataRow row2 = DbConfig.NewDataRow(dataSet.Tables[0], dbobj);
						dataSet.Tables[0].Rows.Add(row2);
					}
					else
					{
						DataTable dataTable2 = DbConfig.CreateDataTable();
						DataRow row3 = DbConfig.NewDataRow(dataTable2, dbobj);
						dataTable2.Rows.Add(row3);
						dataSet.Tables.Add(dataTable2);
					}
				}
				dataSet.WriteXml(DbConfig.fileName);
				result = 1;
			}
			catch
			{
				result = 0;
			}
			return result;
		}
		public static void UpdateSettings(DbSettings dbobj)
		{
			try
			{
				DataSet dataSet = new DataSet();
				if (!File.Exists(DbConfig.fileName))
				{
					DataTable dataTable = DbConfig.CreateDataTable();
					DataRow row = DbConfig.NewDataRow(dataTable, dbobj);
					dataTable.Rows.Add(row);
					dataSet.Tables.Add(dataTable);
				}
				else
				{
					dataSet.ReadXml(DbConfig.fileName);
					if (dataSet.Tables.Count > 0)
					{
						DataRow[] array = dataSet.Tables[0].Select(string.Concat(new string[]
						{
							"DbType='",
							dbobj.DbType,
							"' and Server='",
							dbobj.Server,
							"' and DbName='",
							dbobj.DbName,
							"'"
						}));
						if (array.Length > 0)
						{
							DbConfig.AddColForTable(dataSet.Tables[0]);
							DataRow dataRow = array[0];
							dataRow["DbType"] = dbobj.DbType;
							dataRow["Server"] = dbobj.Server;
							dataRow["ConnectStr"] = dbobj.ConnectStr;
							dataRow["DbName"] = dbobj.DbName;
							dataRow["ConnectSimple"] = dbobj.ConnectSimple;
							dataRow["TabLoadtype"] = dbobj.TabLoadtype;
							dataRow["TabLoadKeyword"] = dbobj.TabLoadKeyword;
							dataRow["ProcPrefix"] = dbobj.ProcPrefix;
							dataRow["ProjectName"] = dbobj.ProjectName;
							dataRow["Namepace"] = dbobj.Namepace;
							dataRow["Folder"] = dbobj.Folder;
							dataRow["AppFrame"] = dbobj.AppFrame;
							dataRow["DALType"] = dbobj.DALType;
							dataRow["BLLType"] = dbobj.BLLType;
							dataRow["WebType"] = dbobj.WebType;
							dataRow["EditFont"] = dbobj.EditFont;
							dataRow["EditFontSize"] = dbobj.EditFontSize;
							dataRow["DbHelperName"] = dbobj.DbHelperName;
							dataRow["ModelPrefix"] = dbobj.ModelPrefix;
							dataRow["ModelSuffix"] = dbobj.ModelSuffix;
							dataRow["BLLPrefix"] = dbobj.BLLPrefix;
							dataRow["BLLSuffix"] = dbobj.BLLSuffix;
							dataRow["DALPrefix"] = dbobj.DALPrefix;
							dataRow["DALSuffix"] = dbobj.DALSuffix;
							dataRow["TabNameRule"] = dbobj.TabNameRule;
							dataRow["WebTemplatePath"] = dbobj.WebTemplatePath;
							dataRow["ReplacedOldStr"] = dbobj.ReplacedOldStr;
							dataRow["ReplacedNewStr"] = dbobj.ReplacedNewStr;
						}
						else
						{
							DataRow row2 = DbConfig.NewDataRow(dataSet.Tables[0], dbobj);
							dataSet.Tables[0].Rows.Add(row2);
						}
					}
					else
					{
						DataTable dataTable2 = DbConfig.CreateDataTable();
						DataRow row3 = DbConfig.NewDataRow(dataTable2, dbobj);
						dataTable2.Rows.Add(row3);
						dataSet.Tables.Add(dataTable2);
					}
				}
				dataSet.WriteXml(DbConfig.fileName);
			}
			catch
			{
				throw new Exception("保存配置信息失败！");
			}
		}
		public static void DelSetting(string DbType, string Serverip, string DbName)
		{
			try
			{
				DataSet dataSet = new DataSet();
				if (File.Exists(DbConfig.fileName))
				{
					dataSet.ReadXml(DbConfig.fileName);
					if (dataSet.Tables.Count > 0)
					{
						string text = string.Concat(new string[]
						{
							"DbType='",
							DbType,
							"' and Server='",
							Serverip,
							"'"
						});
						if (DbName.Trim() != "" && DbName.Trim() != "master")
						{
							text = text + " and DbName='" + DbName + "'";
						}
						DataRow[] array = dataSet.Tables[0].Select(text);
						if (array.Length > 0)
						{
							dataSet.Tables[0].Rows.Remove(array[0]);
						}
						dataSet.Tables[0].AcceptChanges();
					}
				}
				dataSet.WriteXml(DbConfig.fileName);
			}
			catch
			{
			}
		}
	}
}
