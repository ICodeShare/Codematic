using System;
using System.Xml.Serialization;
namespace Maticsoft.CmConfig
{
	public class ModuleSettings
	{
		private string _procprefix = "UP_";
		private string _projectname = "demo";
		private string _namespace = "Maticsoft";
		private string _folder = "Folder";
		private string _appframe = "f3";
		private string _daltype = "";
		private string _blltype = "";
		private string _webtype = "";
		private string _editfont = "新宋体";
		private float _editfontsize = 9f;
		private string _dbHelperName = "DbHelperSQL";
		private string modelPrefix = "";
		private string modelSuffix = "";
		private string bllPrefix = "";
		private string bllSuffix = "";
		private string dalPrefix = "";
		private string dalSuffix = "";
		private string tabnameRule = "same";
		private string _webTemplatepath = "";
		[XmlElement]
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
		[XmlElement]
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
		[XmlElement]
		public string Namepace
		{
			get
			{
				return this._namespace;
			}
			set
			{
				this._namespace = value;
			}
		}
		[XmlElement]
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
		[XmlElement]
		public string AppFrame
		{
			get
			{
				return this._appframe;
			}
			set
			{
				this._appframe = value;
			}
		}
		[XmlElement]
		public string DALType
		{
			get
			{
				return this._daltype;
			}
			set
			{
				this._daltype = value;
			}
		}
		[XmlElement]
		public string BLLType
		{
			get
			{
				return this._blltype;
			}
			set
			{
				this._blltype = value;
			}
		}
		[XmlElement]
		public string WebType
		{
			get
			{
				return this._webtype;
			}
			set
			{
				this._webtype = value;
			}
		}
		[XmlElement]
		public string EditFont
		{
			get
			{
				return this._editfont;
			}
			set
			{
				this._editfont = value;
			}
		}
		[XmlElement]
		public float EditFontSize
		{
			get
			{
				return this._editfontsize;
			}
			set
			{
				this._editfontsize = value;
			}
		}
		[XmlElement]
		public string DbHelperName
		{
			get
			{
				return this._dbHelperName;
			}
			set
			{
				this._dbHelperName = value;
			}
		}
		[XmlElement]
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
		[XmlElement]
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
		[XmlElement]
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
		[XmlElement]
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
		[XmlElement]
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
		[XmlElement]
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
		[XmlElement]
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
		[XmlElement]
		public string WebTemplatePath
		{
			get
			{
				return this._webTemplatepath;
			}
			set
			{
				this._webTemplatepath = value;
			}
		}
	}
}
