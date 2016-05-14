using System;
using System.Xml.Serialization;
namespace Maticsoft.CmConfig
{
	public class ProSettings
	{
		private string _mode = "";
		private string _fileext = "";
		private string _fileextdel = "";
		private string _sourceDirectory;
		private string _targetDirectory;
		[XmlElement]
		public string Mode
		{
			get
			{
				return this._mode;
			}
			set
			{
				this._mode = value;
			}
		}
		[XmlElement]
		public string FileExt
		{
			get
			{
				return this._fileext;
			}
			set
			{
				this._fileext = value;
			}
		}
		[XmlElement]
		public string FileExtDel
		{
			get
			{
				return this._fileextdel;
			}
			set
			{
				this._fileextdel = value;
			}
		}
		[XmlElement]
		public string SourceDirectory
		{
			get
			{
				return this._sourceDirectory;
			}
			set
			{
				this._sourceDirectory = value;
			}
		}
		[XmlElement]
		public string TargetDirectory
		{
			get
			{
				return this._targetDirectory;
			}
			set
			{
				this._targetDirectory = value;
			}
		}
	}
}
