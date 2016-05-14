
using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Runtime.InteropServices ; 
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;
using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;

namespace LTPTextEditor.Editor
{
	/// <summary>
	/// Summary description for TextEditorControlWrapper.
	/// </summary>
	[Serializable]
	public class TextEditorControlWrapper:TextEditorControl
	{
		#region Constructor
		public TextEditorControlWrapper()
		{
			this.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("SQL");
		}
		#endregion
		#region Delegates
		public delegate void KeyPressEventHandler(object sender, KeyEventArgs args);
		public event KeyPressEventHandler KeyPressEvent;

		public delegate void MYMouseRButtonUpEventHandler(object sender, MouseEventArgs args);
		public event MYMouseRButtonUpEventHandler RMouseUpEvent;

		#endregion
		#region Private Members
		WordAndPosition[] _buffer = new WordAndPosition[200000];
		#endregion
		#region Public members
		public InfoMessageCollection _infoMessages = new InfoMessageCollection();
		public int SelectionStart
		{
			get{return this.ActiveTextAreaControl.Caret.Offset;}
			set{return;}
		}

		public string SelectedText
		{
			get
			{
				return this.ActiveTextAreaControl.TextArea.SelectionManager.SelectedText;
				//return "";
			}
			set
			{
				return;
			}
		}
		public string CurrentWord
		{
			get
			{
				int pos = TextUtilities.FindPrevWordStart(this.Document,this.SelectionStart);
				string word = TextUtilities.GetWordAt(this.Document,pos);

				if(word.IndexOf(".")>0)
					return word.Substring(word.IndexOf(".")+1);

				return word;
			}
		}
		#endregion
		#region Private methods
		private void InitializeComponent()
		{
			// 
			// textAreaPanel
			// 
			this.textAreaPanel.Name = "textAreaPanel";
			this.textAreaPanel.Size = new System.Drawing.Size(872, 760);
			// 
			// TextEditorControlWrapper
			// 
			this.Name = "TextEditorControlWrapper";
			this.Size = new System.Drawing.Size(872, 760);

		}
		
		private void RaiseKeyPressEvent(Keys keydata)
		{
			KeyEventArgs e = new KeyEventArgs(keydata);
			KeyPressEvent(this,e);
		}
		private void RaiseRMouseUpEvent()
		{
			if(RMouseUpEvent==null)
				return;

			int x=this.ActiveTextAreaControl.Caret.ScreenPosition.X;
			int y=this.ActiveTextAreaControl.Caret.ScreenPosition.Y;
			
			MouseEventArgs args = new MouseEventArgs(MouseButtons.Right,1,x,y,0);
			RMouseUpEvent(this,args);
		}
		#endregion
		#region Public methods
		public void SetHighLightingStragegy(string token)
		{
			this.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy(token);
		}
		public int GetCharIndexFromPosition(Point position)
		{
			// FIX!
			return 1;
		}
		public int Find(string str,	int start, RichTextBoxFinds options)
		{
			// FIX!
			return 1;
		}
		public void GoToLine(int line)
		{
			// FIX!
			return;
		}
		public Point GetPositionFromCharIndex(int position)
		{
			Point p;
			try
			{
				// FIX!				
				p=this.ActiveTextAreaControl.Caret.ScreenPosition;//.Position;
				return p;
			}
			catch(Exception ex)
			{
				throw ex;
			}
			

		}
		public int GetLineFromCharIndex(int offSet)
		{
			return this.Document.GetLineNumberForOffset(offSet);
		}
		public string GetCurrentWord()
		{
			int pos = this.ActiveTextAreaControl.Caret.Offset;
			string word = TextUtilities.GetWordAt(this.Document,pos);
			if(word.Length==0 && (this.Text.Length>pos-1))
				word = TextUtilities.GetWordAt(this.Document,pos-1);

			return word.Trim();
		}
		public void Select(int startPos, int length)
		{
			Point startPoint = this.ActiveTextAreaControl.TextArea.Document.OffsetToPosition(startPos);
			Point endPoint = this.ActiveTextAreaControl.TextArea.Document.OffsetToPosition(startPos+length);

			this.ActiveTextAreaControl.TextArea.SelectionManager.SetSelection(startPoint,endPoint);
		}
		public string GetText(int startPos, int length)
		{
			return this.Document.TextBufferStrategy.GetText(startPos,length);
		}
		public string Mark(int startPos, int length)
		{
			Point startPoint = GetPositionFromCharIndex(startPos);
			Point endPoint = GetPositionFromCharIndex(startPos+length);
			this.ActiveTextAreaControl.TextArea.SelectionManager.SetSelection(startPoint, endPoint);
			return this.Document.TextBufferStrategy.GetText(startPos,length);
		}
		public void UndoAction()
		{
			this.Undo();
		}
		public ArrayList GetVariables(string Stringmatch)
		{
			ArrayList arrList = new ArrayList();
			string s = this.Text;
			string pat = @"\100+\w+";
			Regex r = new Regex(pat, RegexOptions.IgnoreCase|RegexOptions.Compiled);
			Match m;

			for (m = r.Match(s); m.Success ; m = m.NextMatch())
			{
				if(!arrList.Contains(m.Value))
					arrList.Add(m.Value);
				
			}
			return arrList;
		}
		public void ClearInfoMessages()
		{
			_infoMessages.Clear();
		}
		public void SetSelectionUnderlineStyle(object underlineStyle)
		{
			
			return;
		}
		public void SetSelectionUnderlineColor(object underlineColor)
		{
			return;
		}
		public int GetCharIndexForTableDefenition(string tableName)
		{
			_buffer.Initialize();
			int count = 0;
			string s = this.Text;
			Regex r = new Regex(@"\w+|[^A-Za-z0-9_ \f\t\v]", RegexOptions.IgnoreCase|RegexOptions.Compiled);
			Match m;

			for (m = r.Match(s); m.Success ; m = m.NextMatch()) 
			{
				if( (m.Value.ToUpper()==tableName.ToUpper() && _buffer[count-1].Word.ToUpper() == "FROM") ||
					(m.Value.ToUpper()==tableName.ToUpper() && _buffer[count-1].Word.ToUpper() == "JOIN"))
				{
					return m.Index;
				}
				_buffer[count].Word = m.Value;
				_buffer[count].Position = m.Index;
				_buffer[count].Length = m.Length;
				count++;
				
				
			}
			return -1;
		}
		public void AddInfoMessages(Point StartPoint,InfoMessage.MessageType MessageType, double PercentPositionFromTop, string Message)
		{
			int i = _infoMessages.Add(new InfoMessage(StartPoint,MessageType,PercentPositionFromTop,Message));
			this.Controls.Add(_infoMessages[i].Picture);
		}
		
		public void SetPosition(int pos)
		{
			// fixed setposition bug in code on 6/23/2005
			TextArea textArea = this.ActiveTextAreaControl.TextArea;
			textArea.Caret.Position = this.Document.OffsetToPosition(pos);
		}

		public void SetLine(int line)
		{
			TextArea textArea = this.ActiveTextAreaControl.TextArea;
			textArea.Caret.Column = 0;
			textArea.Caret.Line = line;
			textArea.Caret.UpdateCaretPosition();	
		}

		public void Paste()
		{
			this.ActiveTextAreaControl.TextArea.ClipboardHandler.Paste(null,null);

		}
		public void Copy()
		{
			this.ActiveTextAreaControl.TextArea.ClipboardHandler.Copy(null,null);
		}
		public void Cut()
		{
			this.ActiveTextAreaControl.TextArea.ClipboardHandler.Cut(null,null);
		}
		public void SetReseveredWordsToUpperCase()
		{
			Regex r = new Regex(@"\w+|[^A-Za-z0-9_ \f\t\v]", RegexOptions.IgnoreCase|RegexOptions.Compiled);
			Match m;
			SyntaxReader syntaxReader = new SyntaxReader();

			for (m = r.Match(this.Text); m.Success ; m = m.NextMatch()) 
			{
				if(syntaxReader.IsReservedWord(m.Value.ToUpper()))
				{
					this.Document.Replace(m.Index, m.Length,m.Value.ToUpper());
				}
			}
		}
		
		#endregion	
		#region Events		
		protected override bool ProcessDialogKey(Keys keyData)
		{
			
			RaiseKeyPressEvent(keyData);
			return base.ProcessDialogKey (keyData);
		}

		

		#endregion
		#region classes
		
		public class InfoMessageCollection :CollectionBase	
		{
			public virtual int Add(InfoMessage newInfoMessage)
			{
				return this.List.Add(newInfoMessage);        
			}
			public virtual InfoMessage this[int Index]
			{
				get
				{
					return (InfoMessage)this.List[Index];        
				}
			}
		}
		public class InfoMessage
		{
			public enum MessageType {Info, Warning, Exception};
			public System.Windows.Forms.PictureBox Picture = new PictureBox();
			public Point StartPoint;		
			public double PercentPositionFormTop;
			public InfoMessage(Point startPoint, MessageType messageType, double percentPositionFormTop, string Message)
			{
				this.StartPoint=startPoint;
				this.Picture.Location=startPoint;
				this.Picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
				this.Picture.Size = new System.Drawing.Size(15, 15);
				this.Picture.BackColor = System.Drawing.Color.Transparent;
				this.PercentPositionFormTop=percentPositionFormTop;
				ToolTip toolTip1 = new ToolTip();
				//			toolTip1.AutoPopDelay = 1000;
				//			toolTip1.InitialDelay = 1000;
				//			toolTip1.ReshowDelay = 500;
				toolTip1.ShowAlways = true;
				toolTip1.SetToolTip(this.Picture,Message);


				switch(messageType)
				{
					case MessageType.Info:
						this.Picture.Image=System.Drawing.Image.FromStream(CopyEmbeddedResource("WindowsApplication1.Embedded.infomessage.gif"));
						break;
					case MessageType.Warning:
						this.Picture.Image=System.Drawing.Image.FromStream(CopyEmbeddedResource("WindowsApplication1.Embedded.infowarning.gif"));
						break;
					case MessageType.Exception:
						this.Picture.Image=System.Drawing.Image.FromStream(CopyEmbeddedResource("WindowsApplication1.Embedded.infowarning.gif"));
						break;
					default:
						this.Picture.Image=System.Drawing.Image.FromStream(CopyEmbeddedResource("WindowsApplication1.Embedded.infomessage.gif"));
						break;
				}
			}
			private System.IO.Stream CopyEmbeddedResource(string resource)
			{
				System.Reflection.Assembly a = System.Reflection.Assembly.GetEntryAssembly();
				return a.GetManifestResourceStream(resource);
			}
		 
	
		}
		public class DebugEventArgs: EventArgs 
		{
			public DebugEventArgs(string method) 
			{
				this.method = method;
			}
			public string method;
		}
		public class MouseUpEventArgs: EventArgs 
		{
			public MouseUpEventArgs(MouseButtons Button, int X, int Y) 
			{
				this.Button=Button;
				this.X=X;
				this.Y=Y;
			}
			public MouseButtons Button;
			public int X;
			public int Y;
		}

		//	struct CommentsAndStrings
		//	{
		//		public int Type;
		//		public int Position;
		//		public int Length;
		//	}
		struct WordAndPosition
		{
			public string Word;
			public int Position;
			public int Length;
			public override string ToString()
			{
				string s = "Word = " + Word + ", Position = " + Position + ", Length = " + Length + "\n";
				return s;
			}
		};
		/// <summary>
		/// Used as storage of all Comments and Strings
		/// </summary>
		class ColorPosition
		{
			public ColorPosition(ColorType type, int startPosition, int endPosition)
			{
				StartPosition = startPosition;
				EndPosition = endPosition;
				Type = type;
			}
			public enum ColorType{String, Comment}
			public int StartPosition;
			public int EndPosition;
			public ColorPosition.ColorType Type;
		
		};
		class ColorPositionCollection :ArrayList
		{
			public int Add(ColorPosition colorPosition)
			{
				return base.Add (colorPosition);
			}

		}
		public class API
		{
			[DllImport("User32.dll", CharSet=CharSet.Auto)]
			public static extern int SendMessage(IntPtr hWnd, int message, IntPtr wParam, IntPtr lParam);
			[DllImport( "user32", CharSet = CharSet.Auto )]
			public static extern int SendMessage( HandleRef hWnd, int msg, int wParam, ref Messages.PARAFORMAT lp );
			[DllImport( "user32", CharSet = CharSet.Auto )]
			public static extern int SendMessage( HandleRef hWnd, int msg, int wParam,	ref Messages.CHARFORMAT lp );
		
			[DllImport( "user32", CharSet = CharSet.Auto )]
			public static extern int SendMessage( IntPtr hWnd, 
				int wmsg,
				int wParam,
				int lParam);
		
			
		}

	
		/// <summary>
		/// Definition of message constats
		/// </summary>
		public class Messages
		{
			#region Enums
			// It makes no difference if we use PARAFORMAT or
			// PARAFORMAT2 here, so I have opted for PARAFORMAT2
			[StructLayout( LayoutKind.Sequential )]
				public struct PARAFORMAT
			{
				public int cbSize;
				public uint dwMask;
				public short wNumbering;
				public short wReserved;
				public int dxStartIndent;
				public int dxRightIndent;
				public int dxOffset;
				public short wAlignment;
				public short cTabCount;
				[MarshalAs( UnmanagedType.ByValArray, SizeConst = 32 )]
				public int[] rgxTabs;
        
				// PARAFORMAT2 from here onwards
				public int dySpaceBefore;
				public int dySpaceAfter;
				public int dyLineSpacing;
				public short sStyle;
				public byte bLineSpacingRule;
				public byte bOutlineLevel;
				public short wShadingWeight;
				public short wShadingStyle;
				public short wNumberingStart;
				public short wNumberingStyle;
				public short wNumberingTab;
				public short wBorderSpace;
				public short wBorderWidth;
				public short wBorders;
			}
			[StructLayout( LayoutKind.Sequential )]
				public struct CHARFORMAT
			{
				public int cbSize;
				public uint dwMask;
				public uint dwEffects;
				public int yHeight;
				public int yOffset;
				public int crTextColor;
				public byte bCharSet;
				public byte bPitchAndFamily;
				[MarshalAs( UnmanagedType.ByValArray, SizeConst = 32 )]
				public char[] szFaceName;
        
				// CHARFORMAT2 from here onwards
				public short wWeight;
				public short sSpacing;
				public int crBackColor;
				public int LCID;
				public uint dwReserved;
				public short sStyle;
				public short wKerning;
				public byte bUnderlineType;
				public byte bAnimation;
				public byte bRevAuthor;
			}

			#endregion
			#region Constants
			public const int	WM_USER = 0x0400;
			public const int	EM_STOPGROUPTYPING = WM_USER + 88;
			public const int	EM_GETOLEINTERFACE = WM_USER + 60;
			public const short  WM_PAINT = 0x00f;
			public const short  WM_KEYDOWN = 0x100;
			public const short  WM_KEYUP=0x101;
			public const int	EM_GETTEXTLENGTHEX = WM_USER + 95;
			public const int	WM_VSCROLL = 0x115;
			public const int	EM_UNDO = 0x304;
			public const int WM_RBUTTONUP = 0x215; //???

			public const int	EM_SETEVENTMASK = 1073;
			public const int	EM_GETPARAFORMAT = 1085;
			public const int	EM_SETPARAFORMAT = 1095;
			public const int	EM_SETTYPOGRAPHYOPTIONS = 1226;
			public const int	WM_SETREDRAW = 11;
			public const int	TO_ADVANCEDTYPOGRAPHY = 1;
			public const int	PFM_ALIGNMENT = 8;
			public const int	SCF_SELECTION = 1;

			public const int	CFM_UNDERLINETYPE = 8388608;
			public const int	EM_SETCHARFORMAT = 1092;
			public const int	EM_GETCHARFORMAT = 1082;
			public const int	EM_OUTLINE = WM_USER + 220;
			#endregion
		}
	
		public class Action
		{
			public Action(int Position, string Value, string SelectedText)
			{
				this.Position=Position;
				this.Value=Value;
				this.SelectedText=SelectedText;
			}
			public int Position;
			public string Value ;
			public string SelectedText;
		}
		#endregion
		protected override bool IsInputKey( System.Windows.Forms.Keys keyData )
		{
			bool bIsInputKey = true;

			switch( keyData )
			{
				case Keys.Left:
					break;
				case Keys.Right:
					break;
				case Keys.Down:
					break;
				case Keys.Up:
					break;
				default:
					bIsInputKey = base.IsInputKey(keyData);
					break;
			}

			return bIsInputKey;
		}
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown (e);
		}

	}
}
