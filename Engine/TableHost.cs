using Maticsoft.CodeHelper;
using System;
using System.Collections.Generic;
namespace Maticsoft.CodeEngine
{
	[Serializable]
	public class TableHost : DatabaseHost
	{
		private string _tablename;
		private string _tabledescription = "";
		private List<ColumnInfo> _keys;
		private List<ColumnInfo> _fkeys;
		private List<ColumnInfo> _fieldlist;
		private string _folder;
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
		public string Folder
		{
			get
			{
				return this._folder;
			}
			set
			{
				this._folder = value;
			}
		}
		public ColumnInfo IdentityKey
		{
			get
			{
				ColumnInfo result = null;
				foreach (ColumnInfo current in this._keys)
				{
					if (current.IsIdentity)
					{
						result = current;
					}
				}
				return result;
			}
		}
		public TableHost()
		{
		}
		public TableHost(string TableName)
		{
		}
	}
}
