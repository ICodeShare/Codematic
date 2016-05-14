using Maticsoft.CmConfig;
using Maticsoft.CodeEngine;
using Maticsoft.CodeHelper;
using Maticsoft.IDBO;
using Maticsoft.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
namespace Maticsoft.CodeBuild
{
	public class BuilderTemp
	{
		protected IDbObject dbobj;
		protected DbSettings dbset;
		private string _dbname;
		private string _tablename;
		private string _tabledescription = "";
		private string TemplateFile = "";
		private List<ColumnInfo> _keys;
		private List<ColumnInfo> _fkeys;
		private List<ColumnInfo> _fieldlist;
		private string _objtype;
		public string DbName
		{
			get
			{
				return this._dbname;
			}
			set
			{
				this._dbname = value;
			}
		}
		public string TableName
		{
			get
			{
				return this._tablename;
			}
			set
			{
				this._tablename = value;
			}
		}
		public string TableDescription
		{
			get
			{
				return this._tabledescription;
			}
			set
			{
				this._tabledescription = value;
			}
		}
		public List<ColumnInfo> Fieldlist
		{
			get
			{
				return this._fieldlist;
			}
			set
			{
				this._fieldlist = value;
			}
		}
		public List<ColumnInfo> Keys
		{
			get
			{
				return this._keys;
			}
			set
			{
				this._keys = value;
			}
		}
		public List<ColumnInfo> FKeys
		{
			get
			{
				return this._fkeys;
			}
			set
			{
				this._fkeys = value;
			}
		}
		public string Fields
		{
			get
			{
				StringPlus stringPlus = new StringPlus();
				foreach (object current in this.Fieldlist)
				{
					stringPlus.Append("'" + current.ToString() + "',");
				}
				stringPlus.DelLastComma();
				return stringPlus.Value;
			}
		}
		public string ObjectType
		{
			get
			{
				return this._objtype;
			}
			set
			{
				this._objtype = value;
			}
		}
		public BuilderTemp(IDbObject idbobj, string dbName, string tableName, string tableDescription, List<ColumnInfo> fieldlist, List<ColumnInfo> keys, List<ColumnInfo> fkeys, string templateFile, DbSettings dbSet, string objectype)
		{
			this.dbobj = idbobj;
			this.DbName = dbName;
			this.TableName = tableName;
			this.TableDescription = tableDescription;
			this.TemplateFile = templateFile;
			this.Fieldlist = fieldlist;
			this.Keys = keys;
			this.FKeys = fkeys;
			this.dbset = dbSet;
			this._objtype = objectype;
		}
		public CodeInfo GetCode()
		{
			CodeInfo codeInfo = new CodeInfo();
			if (this.TemplateFile == null || !File.Exists(this.TemplateFile))
			{
				codeInfo.ErrorMsg = "模板文件不存在！";
				return codeInfo;
			}
			string input = File.ReadAllText(this.TemplateFile);
			CodeGenerator codeGenerator = new CodeGenerator();
			if (this.ObjectType == "proc")
			{
				codeInfo = codeGenerator.GenerateCode(input, new ProcedureHost
				{
					TableList = this.dbobj.GetTablesInfo(this.DbName),
					ViewList = this.dbobj.GetVIEWsInfo(this.DbName),
					ProcedureList = this.dbobj.GetProcInfo(this.DbName),
					TemplateFile = this.TemplateFile,
					DbName = this.DbName,
					TableName = this.TableName,
					TableDescription = this.TableDescription,
					NameSpace = this.dbset.Namepace,
					Folder = this.dbset.Folder,
					DbHelperName = this.dbset.DbHelperName,
					Fieldlist = this.Fieldlist,
					Keys = this.Keys,
					FKeys = this.FKeys,
					DbSet = this.dbset,
					BLLPrefix = this.dbset.BLLPrefix,
					BLLSuffix = this.dbset.BLLSuffix,
					DALPrefix = this.dbset.DALPrefix,
					DALSuffix = this.dbset.DALSuffix,
					DbType = this.dbset.DbType,
					ModelPrefix = this.dbset.ModelPrefix,
					ModelSuffix = this.dbset.ModelSuffix,
					ProcPrefix = this.dbset.ProcPrefix,
					ProjectName = this.dbset.ProjectName,
					TabNameRule = this.dbset.TabNameRule
				});
			}
			else
			{
				codeInfo = codeGenerator.GenerateCode(input, new TableHost
				{
					TableList = this.dbobj.GetTablesInfo(this.DbName),
					ViewList = this.dbobj.GetVIEWsInfo(this.DbName),
					ProcedureList = this.dbobj.GetProcInfo(this.DbName),
					TemplateFile = this.TemplateFile,
					DbName = this.DbName,
					TableName = this.TableName,
					TableDescription = this.TableDescription,
					NameSpace = this.dbset.Namepace,
					Folder = this.dbset.Folder,
					DbHelperName = this.dbset.DbHelperName,
					Fieldlist = this.Fieldlist,
					Keys = this.Keys,
					FKeys = this.FKeys,
					DbSet = this.dbset,
					BLLPrefix = this.dbset.BLLPrefix,
					BLLSuffix = this.dbset.BLLSuffix,
					DALPrefix = this.dbset.DALPrefix,
					DALSuffix = this.dbset.DALSuffix,
					DbType = this.dbset.DbType,
					ModelPrefix = this.dbset.ModelPrefix,
					ModelSuffix = this.dbset.ModelSuffix,
					ProcPrefix = this.dbset.ProcPrefix,
					ProjectName = this.dbset.ProjectName,
					TabNameRule = this.dbset.TabNameRule
				});
			}
			return codeInfo;
		}
		private XmlDocument GetXml(DataRow[] dtrows)
		{
			Stream w = new MemoryStream();
			XmlTextWriter xmlTextWriter = new XmlTextWriter(w, Encoding.UTF8);
			xmlTextWriter.Formatting = Formatting.Indented;
			xmlTextWriter.WriteStartDocument(true);
			xmlTextWriter.WriteStartElement("Schema");
			xmlTextWriter.WriteStartElement("TableName");
			xmlTextWriter.WriteAttributeString("value", "Authors");
			xmlTextWriter.WriteEndElement();
			xmlTextWriter.WriteStartElement("FIELDS");
			for (int i = 0; i < dtrows.Length; i++)
			{
				DataRow dataRow = dtrows[i];
				string value = dataRow["ColumnName"].ToString();
				string value2 = dataRow["TypeName"].ToString();
				xmlTextWriter.WriteStartElement("FIELD");
				xmlTextWriter.WriteAttributeString("Name", value);
				xmlTextWriter.WriteAttributeString("Type", value2);
				xmlTextWriter.WriteEndElement();
			}
			xmlTextWriter.WriteEndElement();
			xmlTextWriter.WriteEndElement();
			xmlTextWriter.Flush();
			xmlTextWriter.Close();
			TextReader txtReader = new StringReader(xmlTextWriter.ToString());
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(txtReader);
			return xmlDocument;
		}
		private XmlDocument GetXml2()
		{
			string filename = "Template\\temp.xml";
			XmlTextWriter xmlTextWriter = new XmlTextWriter(filename, Encoding.UTF8);
			xmlTextWriter.Formatting = Formatting.Indented;
			xmlTextWriter.WriteStartDocument(true);
			xmlTextWriter.WriteStartElement("Schema");
			xmlTextWriter.WriteStartElement("TableName");
			xmlTextWriter.WriteAttributeString("value", this.TableName);
			xmlTextWriter.WriteEndElement();
			xmlTextWriter.WriteStartElement("FIELDS");
			foreach (ColumnInfo current in this.Fieldlist)
			{
				string columnName = current.ColumnName;
				string typeName = current.TypeName;
				string arg_87_0 = current.Length;
				bool arg_8E_0 = current.IsIdentity;
				bool arg_95_0 = current.IsPrimaryKey;
				string description = current.Description;
				string defaultVal = current.DefaultVal;
				xmlTextWriter.WriteStartElement("FIELD");
				xmlTextWriter.WriteAttributeString("Name", columnName);
				xmlTextWriter.WriteAttributeString("Type", CodeCommon.DbTypeToCS(typeName));
				xmlTextWriter.WriteAttributeString("Desc", description);
				xmlTextWriter.WriteAttributeString("defaultVal", defaultVal);
				xmlTextWriter.WriteEndElement();
			}
			xmlTextWriter.WriteEndElement();
			xmlTextWriter.WriteStartElement("PrimaryKeys");
			foreach (ColumnInfo current2 in this.Keys)
			{
				string columnName2 = current2.ColumnName;
				string typeName2 = current2.TypeName;
				string arg_150_0 = current2.Length;
				bool arg_158_0 = current2.IsIdentity;
				bool arg_160_0 = current2.IsPrimaryKey;
				string description2 = current2.Description;
				string defaultVal2 = current2.DefaultVal;
				xmlTextWriter.WriteStartElement("FIELD");
				xmlTextWriter.WriteAttributeString("Name", columnName2);
				xmlTextWriter.WriteAttributeString("Type", CodeCommon.DbTypeToCS(typeName2));
				xmlTextWriter.WriteAttributeString("Desc", description2);
				xmlTextWriter.WriteAttributeString("defaultVal", defaultVal2);
				xmlTextWriter.WriteEndElement();
			}
			xmlTextWriter.WriteEndElement();
			xmlTextWriter.WriteEndElement();
			xmlTextWriter.Flush();
			xmlTextWriter.Close();
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(filename);
			return xmlDocument;
		}
	}
}
