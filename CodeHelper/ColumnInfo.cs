using System;
namespace Maticsoft.CodeHelper
{
	[Serializable]
	public class ColumnInfo
	{
		private string _colorder;
		private string _columnName;
		private string _typeName = "";
		private string _length = "";
		private string _precision = "";
		private string _scale = "";
		private bool _isIdentity;
		private bool _isprimaryKey;
		private bool _isForeignKey;
		private bool _nullable;
		private string _defaultVal = "";
		private string _description = "";
		public string ColumnOrder
		{
			get
			{
				return this._colorder;
			}
			set
			{
				this._colorder = value;
			}
		}
		public string ColumnName
		{
			get
			{
				return this._columnName;
			}
			set
			{
				this._columnName = value;
			}
		}
		public string TypeName
		{
			get
			{
				return this._typeName;
			}
			set
			{
				this._typeName = value;
			}
		}
		public string Length
		{
			get
			{
				return this._length;
			}
			set
			{
				this._length = value;
			}
		}
		public string Precision
		{
			get
			{
				return this._precision;
			}
			set
			{
				this._precision = value;
			}
		}
		public string Scale
		{
			get
			{
				return this._scale;
			}
			set
			{
				this._scale = value;
			}
		}
		public bool IsIdentity
		{
			get
			{
				return this._isIdentity;
			}
			set
			{
				this._isIdentity = value;
			}
		}
		public bool IsPrimaryKey
		{
			get
			{
				return this._isprimaryKey;
			}
			set
			{
				this._isprimaryKey = value;
			}
		}
		public bool IsForeignKey
		{
			get
			{
				return this._isForeignKey;
			}
			set
			{
				this._isForeignKey = value;
			}
		}
		public bool Nullable
		{
			get
			{
				return this._nullable;
			}
			set
			{
				this._nullable = value;
			}
		}
		public string DefaultVal
		{
			get
			{
				return this._defaultVal;
			}
			set
			{
				this._defaultVal = value;
			}
		}
		public string Description
		{
			get
			{
				return this._description;
			}
			set
			{
				this._description = value;
			}
		}
	}
}
