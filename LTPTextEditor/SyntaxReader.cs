using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
namespace LTPTextEditor
{
	public class SyntaxReader
	{
		[Serializable]
		public class Settings
		{
			public bool HideStartPage;
			public string keyWordColor;
			public string operatorColor;
			public string compareColor;
			public string commentColor;
			public string stringColor;
			public string defaultColor;
			public int Ole_keyWordColor;
			public int Ole_operatorColor;
			public int Ole_compareColor;
			public int Ole_commentColor;
			public int Ole_stringColor;
			public int Ole_defaultColor;
			public string fontFamily;
			public GraphicsUnit fontGraphicsUnit;
			public float fontSize;
			public FontStyle fontStyle;
			public bool RunWithIOStatistics;
			public int DifferencialPercentage;
			public bool ShowFrmDocumentHeader;
		}
		private SyntaxReader.Settings _settings;
		private bool _hideStartPage = true;
		private Color _keyWordColor = Color.Blue;
		private Color _operatorColor = Color.Gray;
		private Color _compareColor = Color.PaleVioletRed;
		private Color _commentColor = Color.Green;
		private Color _stringColor = Color.Red;
		private Color _defaultColor = Color.Black;
		private int _oleKeyWordColor = 16711680;
		private int _oleOperatorColor = 8421504;
		private int _oleCompareColor = 9662683;
		private int _oleCommentColor = 32768;
		private int _oleStringColor = 255;
		private int _oleDefaultColor = -9999997;
		private bool _runWithIOStatistics = true;
		private int _differencialPercentage = 101;
		private bool _showFrmDocumentHeader = true;
		private ArrayList Keywords = new ArrayList();
		private ArrayList Functions = new ArrayList();
		private ArrayList Operands = new ArrayList();
		private ArrayList Compares = new ArrayList();
		private Hashtable sqlReservedWords = new Hashtable();
		public string ReservedWordsRegExPath = "QUERYCOMMANDER\\s|";
		public Font EditorFont;
		public XmlDocument xmlReservedWords;
		public XmlNodeList xmlNodeList;
		public Color KeyWordColor
		{
			get
			{
				return this._keyWordColor;
			}
			set
			{
				this._keyWordColor = value;
			}
		}
		public Color OperatorColor
		{
			get
			{
				return this._operatorColor;
			}
			set
			{
				this._operatorColor = value;
			}
		}
		public Color CompareColor
		{
			get
			{
				return this._compareColor;
			}
			set
			{
				this._compareColor = value;
			}
		}
		public Color CommentColor
		{
			get
			{
				return this._commentColor;
			}
			set
			{
				this._commentColor = value;
			}
		}
		public Color StringColor
		{
			get
			{
				return this._stringColor;
			}
			set
			{
				this._stringColor = value;
			}
		}
		public Color DefaultColor
		{
			get
			{
				return this._defaultColor;
			}
			set
			{
				this._defaultColor = value;
			}
		}
		public bool RunWithIOStatistics
		{
			get
			{
				return this._runWithIOStatistics;
			}
			set
			{
				this._runWithIOStatistics = value;
			}
		}
		public int DifferencialPercentage
		{
			get
			{
				return this._differencialPercentage;
			}
			set
			{
				this._differencialPercentage = value;
			}
		}
		public bool ShowFrmDocumentHeader
		{
			get
			{
				return this._showFrmDocumentHeader;
			}
			set
			{
				this._showFrmDocumentHeader = value;
			}
		}
		public bool HideStartPage
		{
			get
			{
				return this._hideStartPage;
			}
			set
			{
				this._hideStartPage = value;
			}
		}
		public int color_keyword
		{
			get
			{
				return this._oleKeyWordColor;
			}
			set
			{
				this._oleKeyWordColor = value;
			}
		}
		public int color_operator
		{
			get
			{
				return this._oleOperatorColor;
			}
			set
			{
				this._oleOperatorColor = value;
			}
		}
		public int color_compare
		{
			get
			{
				return this._oleCompareColor;
			}
			set
			{
				this._oleCompareColor = value;
			}
		}
		public int color_default
		{
			get
			{
				return this._oleDefaultColor;
			}
			set
			{
				this._oleDefaultColor = value;
			}
		}
		public int color_comment
		{
			get
			{
				return this._oleCommentColor;
			}
			set
			{
				this._oleCommentColor = value;
			}
		}
		public int color_string
		{
			get
			{
				return this._oleStringColor;
			}
			set
			{
				this._oleStringColor = value;
			}
		}
		public bool IsReservedWord(string word)
		{
			return word != null && this.sqlReservedWords.Contains(word.ToUpper());
		}
		public SyntaxReader()
		{
			this.LoadXMLDocuments();
		}
		private void LoadXMLDocuments()
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			Stream manifestResourceStream = executingAssembly.GetManifestResourceStream("WindowsApplication1.QCTextEditor.SQLReservedWords.xml");
			this.xmlReservedWords = new XmlDocument();
			this.xmlReservedWords.Load(manifestResourceStream);
			this.xmlNodeList = this.xmlReservedWords.GetElementsByTagName("SQLReservedWords");
			ArrayList arrayList = new ArrayList();
			foreach (XmlNode xmlNode in this.xmlNodeList[0].ChildNodes)
			{
				if (this.sqlReservedWords.Contains(xmlNode.Name))
				{
					arrayList.Add(xmlNode.Name);
				}
				else
				{
					this.sqlReservedWords.Add(xmlNode.Name, xmlNode.Attributes["type"].Value);
					this.ReservedWordsRegExPath = this.ReservedWordsRegExPath + xmlNode.Name + "\\s|";
				}
			}
			string path = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "EditorSettings.config");
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(SyntaxReader.Settings));
			TextReader textReader = new StreamReader(path);
			this._settings = (SyntaxReader.Settings)xmlSerializer.Deserialize(textReader);
			textReader.Close();
			this._hideStartPage = this._settings.HideStartPage;
			this._keyWordColor = Color.FromName(this._settings.keyWordColor);
			this._commentColor = Color.FromName(this._settings.commentColor);
			this._compareColor = Color.FromName(this._settings.compareColor);
			this._defaultColor = Color.FromName(this._settings.defaultColor);
			this._operatorColor = Color.FromName(this._settings.operatorColor);
			this._stringColor = Color.FromName(this._settings.stringColor);
			this._oleCommentColor = this._settings.Ole_commentColor;
			this._oleCompareColor = this._settings.Ole_compareColor;
			this._oleDefaultColor = this._settings.Ole_defaultColor;
			this._oleKeyWordColor = this._settings.Ole_keyWordColor;
			this._oleOperatorColor = this._settings.Ole_operatorColor;
			this._oleStringColor = this._settings.Ole_stringColor;
			this.EditorFont = new Font(this._settings.fontFamily, this._settings.fontSize, this._settings.fontStyle, this._settings.fontGraphicsUnit);
			this._differencialPercentage = this._settings.DifferencialPercentage;
			this._runWithIOStatistics = this._settings.RunWithIOStatistics;
			this._showFrmDocumentHeader = this._settings.ShowFrmDocumentHeader;
			if (this._differencialPercentage == 0 && !this._runWithIOStatistics)
			{
				this._runWithIOStatistics = true;
				this._differencialPercentage = 101;
			}
		}
		public void FillArrays()
		{
		}
		public bool IsKeyword(string s)
		{
			int num = this.Keywords.BinarySearch(s);
			return num >= 0;
		}
		public bool IsFunction(string s)
		{
			int num = this.Functions.BinarySearch(s);
			return num >= 0;
		}
		public int GetColorRef(string word)
		{
			if (!this.sqlReservedWords.Contains(word))
			{
				return -9999997;
			}
			string text = this.sqlReservedWords[word].ToString();
			string a;
			if ((a = text) != null)
			{
				if (a == "keyword")
				{
					return this._oleKeyWordColor;
				}
				if (a == "operator")
				{
					return this._oleOperatorColor;
				}
				if (a == "compare")
				{
					return this._oleCompareColor;
				}
			}
			return this._oleDefaultColor;
		}
		public Color GetColor(string word)
		{
			if (!this.sqlReservedWords.Contains(word))
			{
				return Color.Black;
			}
			string text = this.sqlReservedWords[word].ToString();
			string a;
			if ((a = text) != null)
			{
				if (a == "keyword")
				{
					return this._keyWordColor;
				}
				if (a == "operator")
				{
					return this._operatorColor;
				}
				if (a == "compare")
				{
					return this._compareColor;
				}
			}
			return Color.Black;
		}
		public void Save(Font f)
		{
			this._settings.keyWordColor = this._keyWordColor.Name;
			this._settings.operatorColor = this._operatorColor.Name;
			this._settings.compareColor = this._compareColor.Name;
			this._settings.commentColor = this._commentColor.Name;
			this._settings.stringColor = this._stringColor.Name;
			this._settings.defaultColor = this._defaultColor.Name;
			this._settings.Ole_commentColor = this._oleCommentColor;
			this._settings.Ole_compareColor = this._oleCompareColor;
			this._settings.Ole_defaultColor = this._oleDefaultColor;
			this._settings.Ole_keyWordColor = this._oleKeyWordColor;
			this._settings.Ole_operatorColor = this._oleOperatorColor;
			this._settings.Ole_stringColor = this._oleStringColor;
			this._settings.fontFamily = f.FontFamily.Name;
			this._settings.fontGraphicsUnit = f.Unit;
			this._settings.fontSize = f.Size;
			this._settings.fontStyle = f.Style;
			this._settings.RunWithIOStatistics = this._runWithIOStatistics;
			this._settings.DifferencialPercentage = this._differencialPercentage;
			this._settings.HideStartPage = this._hideStartPage;
			this._settings.ShowFrmDocumentHeader = this._showFrmDocumentHeader;
			string path = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "EditorSettings.config");
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(SyntaxReader.Settings));
			TextWriter textWriter = new StreamWriter(path);
			xmlSerializer.Serialize(textWriter, this._settings);
			textWriter.Close();
			this.LoadXMLDocuments();
		}
		public void Save()
		{
			this._settings.keyWordColor = this._keyWordColor.Name;
			this._settings.operatorColor = this._operatorColor.Name;
			this._settings.compareColor = this._compareColor.Name;
			this._settings.commentColor = this._commentColor.Name;
			this._settings.stringColor = this._stringColor.Name;
			this._settings.defaultColor = this._defaultColor.Name;
			this._settings.Ole_commentColor = this._oleCommentColor;
			this._settings.Ole_compareColor = this._oleCompareColor;
			this._settings.Ole_defaultColor = this._oleDefaultColor;
			this._settings.Ole_keyWordColor = this._oleKeyWordColor;
			this._settings.Ole_operatorColor = this._oleOperatorColor;
			this._settings.Ole_stringColor = this._oleStringColor;
			this._settings.RunWithIOStatistics = this._runWithIOStatistics;
			this._settings.DifferencialPercentage = this._differencialPercentage;
			this._settings.HideStartPage = this._hideStartPage;
			this._settings.ShowFrmDocumentHeader = this._showFrmDocumentHeader;
			string path = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "EditorSettings.config");
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(SyntaxReader.Settings));
			TextWriter textWriter = new StreamWriter(path);
			xmlSerializer.Serialize(textWriter, this._settings);
			textWriter.Close();
			this.LoadXMLDocuments();
		}
	}
}
