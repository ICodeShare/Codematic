using System;
using System.Drawing;
using System.Text;
namespace LTP.TextEditor.Document
{
	public class DefaultTextEditorProperties : ITextEditorProperties
	{
		private int tabIndent = 4;
		private IndentStyle indentStyle = IndentStyle.Smart;
		private DocumentSelectionMode documentSelectionMode;
		private Encoding encoding = Encoding.UTF8;
		private BracketMatchingStyle bracketMatchingStyle = BracketMatchingStyle.After;
		private bool allowCaretBeyondEOL;
		private bool showMatchingBracket = true;
		private bool showLineNumbers = true;
		private bool showSpaces = true;
		private bool showTabs = true;
		private bool showEOLMarker = true;
		private bool showInvalidLines = true;
		private bool isIconBarVisible = true;
		private bool enableFolding = true;
		private bool showHorizontalRuler;
		private bool showVerticalRuler = true;
		private bool convertTabsToSpaces;
		private bool useAntiAliasedFont;
		private bool createBackupCopy;
		private bool mouseWheelScrollDown = true;
		private bool mouseWheelTextZoom = true;
		private bool hideMouseCursor;
		private int verticalRulerRow = 80;
		private LineViewerStyle lineViewerStyle;
		private string lineTerminator = "\r\n";
		private bool autoInsertCurlyBracket = true;
		private bool useCustomLine;
		public int TabIndent
		{
			get
			{
				return this.tabIndent;
			}
			set
			{
				this.tabIndent = value;
			}
		}
		public IndentStyle IndentStyle
		{
			get
			{
				return this.indentStyle;
			}
			set
			{
				this.indentStyle = value;
			}
		}
		public DocumentSelectionMode DocumentSelectionMode
		{
			get
			{
				return this.documentSelectionMode;
			}
			set
			{
				this.documentSelectionMode = value;
			}
		}
		public bool AllowCaretBeyondEOL
		{
			get
			{
				return this.allowCaretBeyondEOL;
			}
			set
			{
				this.allowCaretBeyondEOL = value;
			}
		}
		public bool ShowMatchingBracket
		{
			get
			{
				return this.showMatchingBracket;
			}
			set
			{
				this.showMatchingBracket = value;
			}
		}
		public bool ShowLineNumbers
		{
			get
			{
				return this.showLineNumbers;
			}
			set
			{
				this.showLineNumbers = value;
			}
		}
		public bool ShowSpaces
		{
			get
			{
				return this.showSpaces;
			}
			set
			{
				this.showSpaces = value;
			}
		}
		public bool ShowTabs
		{
			get
			{
				return this.showTabs;
			}
			set
			{
				this.showTabs = value;
			}
		}
		public bool ShowEOLMarker
		{
			get
			{
				return this.showEOLMarker;
			}
			set
			{
				this.showEOLMarker = value;
			}
		}
		public bool ShowInvalidLines
		{
			get
			{
				return this.showInvalidLines;
			}
			set
			{
				this.showInvalidLines = value;
			}
		}
		public bool IsIconBarVisible
		{
			get
			{
				return this.isIconBarVisible;
			}
			set
			{
				this.isIconBarVisible = value;
			}
		}
		public bool EnableFolding
		{
			get
			{
				return this.enableFolding;
			}
			set
			{
				this.enableFolding = value;
			}
		}
		public bool ShowHorizontalRuler
		{
			get
			{
				return this.showHorizontalRuler;
			}
			set
			{
				this.showHorizontalRuler = value;
			}
		}
		public bool ShowVerticalRuler
		{
			get
			{
				return this.showVerticalRuler;
			}
			set
			{
				this.showVerticalRuler = value;
			}
		}
		public bool ConvertTabsToSpaces
		{
			get
			{
				return this.convertTabsToSpaces;
			}
			set
			{
				this.convertTabsToSpaces = value;
			}
		}
		public bool UseAntiAliasedFont
		{
			get
			{
				return this.useAntiAliasedFont;
			}
			set
			{
				this.useAntiAliasedFont = value;
			}
		}
		public bool CreateBackupCopy
		{
			get
			{
				return this.createBackupCopy;
			}
			set
			{
				this.createBackupCopy = value;
			}
		}
		public bool MouseWheelScrollDown
		{
			get
			{
				return this.mouseWheelScrollDown;
			}
			set
			{
				this.mouseWheelScrollDown = value;
			}
		}
		public bool MouseWheelTextZoom
		{
			get
			{
				return this.mouseWheelTextZoom;
			}
			set
			{
				this.mouseWheelTextZoom = value;
			}
		}
		public bool HideMouseCursor
		{
			get
			{
				return this.hideMouseCursor;
			}
			set
			{
				this.hideMouseCursor = value;
			}
		}
		public Encoding Encoding
		{
			get
			{
				return this.encoding;
			}
			set
			{
				this.encoding = value;
			}
		}
		public int VerticalRulerRow
		{
			get
			{
				return this.verticalRulerRow;
			}
			set
			{
				this.verticalRulerRow = value;
			}
		}
		public LineViewerStyle LineViewerStyle
		{
			get
			{
				return this.lineViewerStyle;
			}
			set
			{
				this.lineViewerStyle = value;
			}
		}
		public string LineTerminator
		{
			get
			{
				return this.lineTerminator;
			}
			set
			{
				this.lineTerminator = value;
			}
		}
		public bool AutoInsertCurlyBracket
		{
			get
			{
				return this.autoInsertCurlyBracket;
			}
			set
			{
				this.autoInsertCurlyBracket = value;
			}
		}
		public Font Font
		{
			get
			{
				return FontContainer.DefaultFont;
			}
			set
			{
				FontContainer.DefaultFont = value;
			}
		}
		public BracketMatchingStyle BracketMatchingStyle
		{
			get
			{
				return this.bracketMatchingStyle;
			}
			set
			{
				this.bracketMatchingStyle = value;
			}
		}
		public bool UseCustomLine
		{
			get
			{
				return this.useCustomLine;
			}
			set
			{
				this.useCustomLine = value;
			}
		}
	}
}
