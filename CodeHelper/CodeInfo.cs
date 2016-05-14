using System;
namespace Maticsoft.CodeHelper
{
	[Serializable]
	public class CodeInfo
	{
		private string _code;
		private string _errormsg;
		private string _fileExtensionValue = ".cs";
		public string Code
		{
			get
			{
				return this._code;
			}
			set
			{
				this._code = value;
			}
		}
		public string ErrorMsg
		{
			get
			{
				return this._errormsg;
			}
			set
			{
				this._errormsg = value;
			}
		}
		public string FileExtension
		{
			get
			{
				return this._fileExtensionValue;
			}
			set
			{
				this._fileExtensionValue = value;
			}
		}
	}
}
