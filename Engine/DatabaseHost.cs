using Maticsoft.CmConfig;
using Maticsoft.CodeHelper;
using System;
using System.Collections.Generic;
namespace Maticsoft.CodeEngine
{
	[Serializable]
	public class DatabaseHost : TemplateHost
	{
		private string _dbname;
		private string _dbhelperName = "DbHelperSQL";
		private List<TableInfo> _tablelist;
		private List<TableInfo> _viewlist;
		private List<TableInfo> _procedurelist;
		private string _dbtype;
		private string _procprefix = "";
		private string _projectname = "";
		private DbSettings _dbset;
		private string modelPrefix = "";
		private string modelSuffix = "";
		private string bllPrefix = "";
		private string bllSuffix = "";
		private string dalPrefix = "";
		private string dalSuffix = "";
		private string tabnameRule = "same";
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
		public string DbHelperName
		{
			get
			{
				return this._dbhelperName;
			}
			set
			{
				this._dbhelperName = value;
			}
		}
		public List<TableInfo> ViewList
		{
			get
			{
				return this._viewlist;
			}
			set
			{
				this._viewlist = value;
			}
		}
		public List<TableInfo> ProcedureList
		{
			get
			{
				return this._procedurelist;
			}
			set
			{
				this._procedurelist = value;
			}
		}
		public List<TableInfo> TableList
		{
			get
			{
				return this._tablelist;
			}
			set
			{
				this._tablelist = value;
			}
		}
		public string DbType
		{
			get
			{
				return this._dbtype;
			}
			set
			{
				this._dbtype = value;
			}
		}
		public DbSettings DbSet
		{
			get
			{
				return this._dbset;
			}
			set
			{
				this._dbset = value;
			}
		}
		public string ProcPrefix
		{
			get
			{
				return this._procprefix;
			}
			set
			{
				this._procprefix = value;
			}
		}
		public string ProjectName
		{
			get
			{
				return this._projectname;
			}
			set
			{
				this._projectname = value;
			}
		}
		public string ModelPrefix
		{
			get
			{
				return this.modelPrefix;
			}
			set
			{
				this.modelPrefix = value;
			}
		}
		public string ModelSuffix
		{
			get
			{
				return this.modelSuffix;
			}
			set
			{
				this.modelSuffix = value;
			}
		}
		public string BLLPrefix
		{
			get
			{
				return this.bllPrefix;
			}
			set
			{
				this.bllPrefix = value;
			}
		}
		public string BLLSuffix
		{
			get
			{
				return this.bllSuffix;
			}
			set
			{
				this.bllSuffix = value;
			}
		}
		public string DALPrefix
		{
			get
			{
				return this.dalPrefix;
			}
			set
			{
				this.dalPrefix = value;
			}
		}
		public string DALSuffix
		{
			get
			{
				return this.dalSuffix;
			}
			set
			{
				this.dalSuffix = value;
			}
		}
		public string TabNameRule
		{
			get
			{
				return this.tabnameRule;
			}
			set
			{
				this.tabnameRule = value;
			}
		}
		public string DbParaHead
		{
			get
			{
				return CodeCommon.DbParaHead(this.DbType);
			}
		}
		public string DbParaDbType
		{
			get
			{
				return CodeCommon.DbParaDbType(this.DbType);
			}
		}
		public string preParameter
		{
			get
			{
				return CodeCommon.preParameter(this.DbType);
			}
		}
		public string GetModelClass(string TabName)
		{
			return this._dbset.ModelPrefix + this.TabNameRuled(TabName) + this._dbset.ModelSuffix;
		}
		public string GetBLLClass(string TabName)
		{
			return this._dbset.BLLPrefix + this.TabNameRuled(TabName) + this._dbset.BLLSuffix;
		}
		public string GetDALClass(string TabName)
		{
			return this._dbset.DALPrefix + this.TabNameRuled(TabName) + this._dbset.DALSuffix;
		}
		private string TabNameRuled(string TabName)
		{
			string text = TabName;
			if (this._dbset.ReplacedOldStr.Length > 0)
			{
				text = text.Replace(this._dbset.ReplacedOldStr, this._dbset.ReplacedNewStr);
			}
			string a;
			if ((a = this._dbset.TabNameRule.ToLower()) != null)
			{
				if (!(a == "lower"))
				{
					if (!(a == "upper"))
					{
						if (!(a == "firstupper"))
						{
							if (!(a == "same"))
							{
							}
						}
						else
						{
							string str = text.Substring(0, 1).ToUpper();
							text = str + text.Substring(1);
						}
					}
					else
					{
						text = text.ToUpper();
					}
				}
				else
				{
					text = text.ToLower();
				}
			}
			return text;
		}
	}
}
