using System;
using System.Xml.Serialization;
namespace Maticsoft.CmConfig
{
	[Serializable]
	public class DbSettings
	{
		private string _dbtype;
		private string _server;
		private string _connectstr;
		private string _dbname;
		private bool _connectSimple = true;
		private int _tabloadtype;
		private string _tabloadkeyword;
		private string _procprefix = "";
		private string _projectname = "";
		private string _namespace = "Maticsoft";
		private string _folder = "";
		private string _appframe = "s3";
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
		private string _replaceoldstr = "";
		private string _replacenewstr = "";
		[XmlElement]
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
		[XmlElement]
		public string Server
		{
			get
			{
				return this._server;
			}
			set
			{
				this._server = value;
			}
		}
		[XmlElement]
		public string ConnectStr
		{
			get
			{
				return this._connectstr;
			}
			set
			{
				this._connectstr = value;
			}
		}
		[XmlElement]
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
		[XmlElement]
		public bool ConnectSimple
		{
			get
			{
				return this._connectSimple;
			}
			set
			{
				this._connectSimple = value;
			}
		}
		[XmlElement]
		public int TabLoadtype
		{
			get
			{
				return this._tabloadtype;
			}
			set
			{
				this._tabloadtype = value;
			}
		}
		[XmlElement]
		public string TabLoadKeyword
		{
			get
			{
				return this._tabloadkeyword;
			}
			set
			{
				this._tabloadkeyword = value;
			}
		}
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
		[XmlElement]
		public string ReplacedOldStr
		{
			get
			{
				return this._replaceoldstr;
			}
			set
			{
				this._replaceoldstr = value;
			}
		}
		[XmlElement]
		public string ReplacedNewStr
		{
			get
			{
				return this._replacenewstr;
			}
			set
			{
				this._replacenewstr = value;
			}
		}
	}
}
