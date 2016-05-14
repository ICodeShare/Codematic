using System;
namespace Maticsoft.CodeHelper
{
	public class CodeKey
	{
		private string _keyName;
		private string _keyType;
		private bool _isPK;
		private bool _isIdentity;
		public string KeyName
		{
			get
			{
				return this._keyName;
			}
			set
			{
				this._keyName = value;
			}
		}
		public string KeyType
		{
			get
			{
				return this._keyType;
			}
			set
			{
				this._keyType = value;
			}
		}
		public bool IsPK
		{
			get
			{
				return this._isPK;
			}
			set
			{
				this._isPK = value;
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
	}
}
