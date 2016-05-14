using System;
using System.Xml.Serialization;
namespace Maticsoft.CmConfig
{
	public class AppSettings
	{
		private string _appstart;
		private string _startuppage;
		private string _homepage;
		private string _templatefolder = "Template";
		private bool _setup;
		private string _language = "zh-cn";
		[XmlElement]
		public string AppStart
		{
			get
			{
				return this._appstart;
			}
			set
			{
				this._appstart = value;
			}
		}
		[XmlElement]
		public string StartUpPage
		{
			get
			{
				return this._startuppage;
			}
			set
			{
				this._startuppage = value;
			}
		}
		[XmlElement]
		public string HomePage
		{
			get
			{
				return this._homepage;
			}
			set
			{
				this._homepage = value;
			}
		}
		[XmlElement]
		public string TemplateFolder
		{
			get
			{
				return this._templatefolder;
			}
			set
			{
				this._templatefolder = value;
			}
		}
		[XmlElement]
		public bool Setup
		{
			get
			{
				return this._setup;
			}
			set
			{
				this._setup = value;
			}
		}
		[XmlElement]
		public string Language
		{
			get
			{
				return this._language;
			}
			set
			{
				this._language = value;
			}
		}
	}
}
