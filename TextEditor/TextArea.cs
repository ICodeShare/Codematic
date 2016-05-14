using LTP.TextEditor.Actions;
using LTP.TextEditor.Document;
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
namespace LTP.TextEditor
{
	[ToolboxItem(false)]
	public class TextArea : UserControl
	{
		public static bool HiddenMouseCursor;
		private Point virtualTop = new Point(0, 0);
		private TextAreaControl motherTextAreaControl;
		private TextEditorControl motherTextEditorControl;
		private ArrayList bracketshemes = new ArrayList();
		private TextAreaClipboardHandler textAreaClipboardHandler;
		private bool autoClearSelection;
		private ArrayList leftMargins = new ArrayList();
		private ArrayList topMargins = new ArrayList();
		private TextView textView;
		private GutterMargin gutterMargin;
		private FoldMargin foldMargin;
		private IconBarMargin iconBarMargin;
		private SelectionManager selectionManager;
		private Caret caret;
		private ToolTip toolTip = new ToolTip();
		private bool toolTipSet;
		private AbstractMargin lastMouseInMargin;
		private string oldToolTip;
		private AbstractMargin updateMargin;
        public event KeyEventHandler KeyEventHandler;
        public event DialogKeyProcessor DoProcessDialogKey;
		public TextEditorControl MotherTextEditorControl
		{
			get
			{
				return this.motherTextEditorControl;
			}
		}
		public TextAreaControl MotherTextAreaControl
		{
			get
			{
				return this.motherTextAreaControl;
			}
		}
		public SelectionManager SelectionManager
		{
			get
			{
				return this.selectionManager;
			}
		}
		public Caret Caret
		{
			get
			{
				return this.caret;
			}
		}
		public TextView TextView
		{
			get
			{
				return this.textView;
			}
		}
		public GutterMargin GutterMargin
		{
			get
			{
				return this.gutterMargin;
			}
		}
		public FoldMargin FoldMargin
		{
			get
			{
				return this.foldMargin;
			}
		}
		public IconBarMargin IconBarMargin
		{
			get
			{
				return this.iconBarMargin;
			}
		}
		public Encoding Encoding
		{
			get
			{
				return this.motherTextEditorControl.Encoding;
			}
		}
		public int MaxVScrollValue
		{
			get
			{
				return (this.Document.GetVisibleLine(this.Document.TotalNumberOfLines - 1) + 1 + this.TextView.VisibleLineCount * 2 / 3) * this.TextView.FontHeight;
			}
		}
		public Point VirtualTop
		{
			get
			{
				return this.virtualTop;
			}
			set
			{
				Point right = new Point(value.X, Math.Min(this.MaxVScrollValue, Math.Max(0, value.Y)));
				if (this.virtualTop != right)
				{
					this.virtualTop = right;
					this.motherTextAreaControl.VScrollBar.Value = this.virtualTop.Y;
					base.Invalidate();
				}
			}
		}
		public bool AutoClearSelection
		{
			get
			{
				return this.autoClearSelection;
			}
			set
			{
				this.autoClearSelection = value;
			}
		}
		[Browsable(false)]
		public IDocument Document
		{
			get
			{
				return this.motherTextEditorControl.Document;
			}
		}
		public TextAreaClipboardHandler ClipboardHandler
		{
			get
			{
				return this.textAreaClipboardHandler;
			}
		}
		public ITextEditorProperties TextEditorProperties
		{
			get
			{
				return this.motherTextEditorControl.TextEditorProperties;
			}
		}
		public bool EnableCutOrPaste
		{
			get
			{
				if (this.motherTextEditorControl == null)
				{
					return false;
				}
				if (this.TextEditorProperties.UseCustomLine)
				{
					if (this.SelectionManager.HasSomethingSelected && this.Document.CustomLineManager.IsReadOnly(this.SelectionManager.SelectionCollection[0], false))
					{
						return false;
					}
					if (this.Document.CustomLineManager.IsReadOnly(this.Caret.Line, false))
					{
						return false;
					}
				}
				return true;
			}
		}
		private int FirstPhysicalLine
		{
			get
			{
				return this.VirtualTop.Y / this.textView.FontHeight;
			}
		}
		public TextArea(TextEditorControl motherTextEditorControl, TextAreaControl motherTextAreaControl)
		{
			this.motherTextAreaControl = motherTextAreaControl;
			this.motherTextEditorControl = motherTextEditorControl;
			this.caret = new Caret(this);
			this.selectionManager = new SelectionManager(this.Document);
			this.textAreaClipboardHandler = new TextAreaClipboardHandler(this);
			base.ResizeRedraw = true;
			base.SetStyle(ControlStyles.DoubleBuffer, false);
			base.SetStyle(ControlStyles.Opaque, false);
			base.SetStyle(ControlStyles.ResizeRedraw, true);
			base.SetStyle(ControlStyles.Selectable, true);
			this.textView = new TextView(this);
			this.gutterMargin = new GutterMargin(this);
			this.foldMargin = new FoldMargin(this);
			this.iconBarMargin = new IconBarMargin(this);
			this.leftMargins.AddRange(new AbstractMargin[]
			{
				this.iconBarMargin,
				this.gutterMargin,
				this.foldMargin
			});
			this.OptionsChanged();
			new TextAreaMouseHandler(this).Attach();
			new TextAreaDragDropHandler().Attach(this);
			this.bracketshemes.Add(new BracketHighlightingSheme('{', '}'));
			this.bracketshemes.Add(new BracketHighlightingSheme('(', ')'));
			this.bracketshemes.Add(new BracketHighlightingSheme('[', ']'));
			this.caret.PositionChanged += new EventHandler(this.SearchMatchingBracket);
			this.Document.TextContentChanged += new EventHandler(this.TextContentChanged);
			this.Document.FoldingManager.FoldingsChanged += new EventHandler(this.DocumentFoldingsChanged);
		}
		public void UpdateMatchingBracket()
		{
			this.SearchMatchingBracket(null, null);
		}
		private void TextContentChanged(object sender, EventArgs e)
		{
			this.Caret.Position = new Point(0, 0);
			this.SelectionManager.SelectionCollection.Clear();
		}
		private void SearchMatchingBracket(object sender, EventArgs e)
		{
			if (!this.TextEditorProperties.ShowMatchingBracket)
			{
				this.textView.Highlight = null;
				return;
			}
			bool flag = false;
			if (this.caret.Offset == 0)
			{
				if (this.textView.Highlight != null)
				{
					int y = this.textView.Highlight.OpenBrace.Y;
					int y2 = this.textView.Highlight.CloseBrace.Y;
					this.textView.Highlight = null;
					this.UpdateLine(y);
					this.UpdateLine(y2);
				}
				return;
			}
			foreach (BracketHighlightingSheme bracketHighlightingSheme in this.bracketshemes)
			{
				Highlight highlight = bracketHighlightingSheme.GetHighlight(this.Document, this.Caret.Offset - 1);
				if (this.textView.Highlight != null && this.textView.Highlight.OpenBrace.Y >= 0 && this.textView.Highlight.OpenBrace.Y < this.Document.TotalNumberOfLines)
				{
					this.UpdateLine(this.textView.Highlight.OpenBrace.Y);
				}
				if (this.textView.Highlight != null && this.textView.Highlight.CloseBrace.Y >= 0 && this.textView.Highlight.CloseBrace.Y < this.Document.TotalNumberOfLines)
				{
					this.UpdateLine(this.textView.Highlight.CloseBrace.Y);
				}
				this.textView.Highlight = highlight;
				if (highlight != null)
				{
					flag = true;
					break;
				}
			}
			if (flag || this.textView.Highlight != null)
			{
				int y3 = this.textView.Highlight.OpenBrace.Y;
				int y4 = this.textView.Highlight.CloseBrace.Y;
				if (!flag)
				{
					this.textView.Highlight = null;
				}
				this.UpdateLine(y3);
				this.UpdateLine(y4);
			}
		}
		public void SetDesiredColumn()
		{
			this.Caret.DesiredColumn = this.TextView.GetDrawingXPos(this.Caret.Line, this.Caret.Column) + (int)((float)this.VirtualTop.X * this.textView.GetWidth(' '));
		}
		public void SetCaretToDesiredColumn(int caretLine)
		{
			this.Caret.Position = this.textView.GetLogicalColumn(this.Caret.Line, this.Caret.DesiredColumn + (int)((float)this.VirtualTop.X * this.textView.GetWidth(' ')));
		}
		public void OptionsChanged()
		{
			this.UpdateMatchingBracket();
			this.textView.OptionsChanged();
			this.caret.RecreateCaret();
			this.caret.UpdateCaretPosition();
			this.Refresh();
		}
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			this.Cursor = Cursors.Default;
			if (this.lastMouseInMargin != null)
			{
				this.lastMouseInMargin.HandleMouseLeave(EventArgs.Empty);
				this.lastMouseInMargin = null;
			}
		}
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			foreach (AbstractMargin abstractMargin in this.leftMargins)
			{
				if (abstractMargin.DrawingPosition.Contains(e.X, e.Y))
				{
					abstractMargin.HandleMouseDown(new Point(e.X, e.Y), e.Button);
				}
			}
		}
		public void SetToolTip(string text)
		{
			this.toolTipSet = (text != null);
			if (this.oldToolTip == text)
			{
				return;
			}
			if (text == null)
			{
				this.toolTip.SetToolTip(this, null);
			}
			else
			{
				this.toolTip.SetToolTip(this, text);
			}
			this.oldToolTip = text;
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			this.toolTipSet = false;
			base.OnMouseMove(e);
			if (!this.toolTipSet)
			{
				this.SetToolTip(null);
			}
			foreach (AbstractMargin abstractMargin in this.leftMargins)
			{
				if (abstractMargin.DrawingPosition.Contains(e.X, e.Y))
				{
					this.Cursor = abstractMargin.Cursor;
					abstractMargin.HandleMouseMove(new Point(e.X, e.Y), e.Button);
					if (this.lastMouseInMargin != abstractMargin)
					{
						if (this.lastMouseInMargin != null)
						{
							this.lastMouseInMargin.HandleMouseLeave(EventArgs.Empty);
						}
						this.lastMouseInMargin = abstractMargin;
					}
					return;
				}
			}
			if (this.lastMouseInMargin != null)
			{
				this.lastMouseInMargin.HandleMouseLeave(EventArgs.Empty);
				this.lastMouseInMargin = null;
			}
			if (!this.textView.DrawingPosition.Contains(e.X, e.Y))
			{
				this.Cursor = Cursors.Default;
				return;
			}
			this.Cursor = this.textView.Cursor;
		}
		public void Refresh(AbstractMargin margin)
		{
			this.updateMargin = margin;
			base.Invalidate(this.updateMargin.DrawingPosition);
			base.Update();
			this.updateMargin = null;
		}
		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			int num = 0;
			int num2 = 0;
			bool flag = false;
			Graphics graphics = e.Graphics;
			Rectangle clipRectangle = e.ClipRectangle;
			if (this.updateMargin != null)
			{
				try
				{
					this.updateMargin.Paint(graphics, this.updateMargin.DrawingPosition);
				}
				catch (Exception arg)
				{
					Console.WriteLine("Got exception : " + arg);
				}
			}
			if (clipRectangle.Width <= 0 || clipRectangle.Height <= 0)
			{
				return;
			}
			if (this.TextEditorProperties.UseAntiAliasedFont)
			{
				graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			}
			else
			{
				graphics.TextRenderingHint = TextRenderingHint.SystemDefault;
			}
			foreach (AbstractMargin abstractMargin in this.leftMargins)
			{
				if (abstractMargin.IsVisible)
				{
					Rectangle rectangle = new Rectangle(num, num2, abstractMargin.Size.Width, base.Height - num2);
					if (rectangle != abstractMargin.DrawingPosition)
					{
						flag = true;
						abstractMargin.DrawingPosition = rectangle;
					}
					num += abstractMargin.DrawingPosition.Width;
					if (clipRectangle.IntersectsWith(rectangle))
					{
						rectangle.Intersect(clipRectangle);
						if (!rectangle.IsEmpty)
						{
							try
							{
								abstractMargin.Paint(graphics, rectangle);
							}
							catch (Exception arg2)
							{
								Console.WriteLine("Got exception : " + arg2);
							}
						}
					}
				}
			}
			Rectangle rectangle2 = new Rectangle(num, num2, base.Width - num, base.Height - num2);
			if (rectangle2 != this.textView.DrawingPosition)
			{
				flag = true;
				this.textView.DrawingPosition = rectangle2;
			}
			if (clipRectangle.IntersectsWith(rectangle2))
			{
				rectangle2.Intersect(clipRectangle);
				if (!rectangle2.IsEmpty)
				{
					try
					{
						this.textView.Paint(graphics, rectangle2);
					}
					catch (Exception arg3)
					{
						Console.WriteLine("Got exception : " + arg3);
					}
				}
			}
			if (flag)
			{
				try
				{
					this.motherTextAreaControl.AdjustScrollBars(null, null);
				}
				catch (Exception)
				{
				}
			}
			try
			{
				this.Caret.UpdateCaretPosition();
			}
			catch (Exception)
			{
			}
			base.OnPaint(e);
		}
		private void DocumentFoldingsChanged(object sender, EventArgs e)
		{
			this.motherTextAreaControl.AdjustScrollBars(null, null);
		}
		protected virtual bool HandleKeyPress(char ch)
		{
			return this.KeyEventHandler != null && this.KeyEventHandler(ch);
		}
		public void SimulateKeyPress(char ch)
		{
			if (this.Document.ReadOnly)
			{
				return;
			}
			if (this.TextEditorProperties.UseCustomLine)
			{
				if (this.SelectionManager.HasSomethingSelected)
				{
					if (this.Document.CustomLineManager.IsReadOnly(this.SelectionManager.SelectionCollection[0], false))
					{
						return;
					}
				}
				else
				{
					if (this.Document.CustomLineManager.IsReadOnly(this.Caret.Line, false))
					{
						return;
					}
				}
			}
			if (ch < ' ')
			{
				return;
			}
			if (!TextArea.HiddenMouseCursor && this.TextEditorProperties.HideMouseCursor)
			{
				TextArea.HiddenMouseCursor = true;
				Cursor.Hide();
			}
			if (!this.HandleKeyPress(ch))
			{
				this.motherTextEditorControl.BeginUpdate();
				switch (this.Caret.CaretMode)
				{
				case CaretMode.InsertMode:
					this.InsertChar(ch);
					break;
				case CaretMode.OverwriteMode:
					this.ReplaceChar(ch);
					break;
				}
				int line = this.Caret.Line;
				this.Document.FormattingStrategy.FormatLine(this, line, this.Document.PositionToOffset(this.Caret.Position), ch);
				this.motherTextEditorControl.EndUpdate();
			}
		}
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress(e);
			this.SimulateKeyPress(e.KeyChar);
		}
		public bool ExecuteDialogKey(Keys keyData)
		{
			if (this.DoProcessDialogKey != null && this.DoProcessDialogKey(keyData))
			{
				return true;
			}
			if ((keyData == Keys.Back || keyData == Keys.Delete || keyData == Keys.Return) && this.TextEditorProperties.UseCustomLine)
			{
				if (this.SelectionManager.HasSomethingSelected)
				{
					if (this.Document.CustomLineManager.IsReadOnly(this.SelectionManager.SelectionCollection[0], false))
					{
						return true;
					}
				}
				else
				{
					int lineNumberForOffset = this.Document.GetLineNumberForOffset(this.Caret.Offset);
					if (this.Document.CustomLineManager.IsReadOnly(lineNumberForOffset, false))
					{
						return true;
					}
					if (this.Caret.Column == 0 && lineNumberForOffset - 1 >= 0 && keyData == Keys.Back && this.Document.CustomLineManager.IsReadOnly(lineNumberForOffset - 1, false))
					{
						return true;
					}
					if (keyData == Keys.Delete)
					{
						LineSegment lineSegment = this.Document.GetLineSegment(lineNumberForOffset);
						if (lineSegment.Offset + lineSegment.Length == this.Caret.Offset && this.Document.CustomLineManager.IsReadOnly(lineNumberForOffset + 1, false))
						{
							return true;
						}
					}
				}
			}
			IEditAction editAction = this.motherTextEditorControl.GetEditAction(keyData);
			this.AutoClearSelection = true;
			if (editAction != null)
			{
				this.motherTextEditorControl.BeginUpdate();
				try
				{
					IDocument document;
					Monitor.Enter(document = this.Document);
					try
					{
						editAction.Execute(this);
						if (this.SelectionManager.HasSomethingSelected && this.AutoClearSelection && this.Document.TextEditorProperties.DocumentSelectionMode == DocumentSelectionMode.Normal)
						{
							this.SelectionManager.ClearSelection();
						}
					}
					finally
					{
						Monitor.Exit(document);
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine(string.Concat(new object[]
					{
						"Got Exception while executing action ",
						editAction,
						" : ",
						ex.ToString()
					}));
				}
				finally
				{
					this.motherTextEditorControl.EndUpdate();
					this.Caret.UpdateCaretPosition();
				}
				return true;
			}
			return false;
		}
		protected override bool ProcessDialogKey(Keys keyData)
		{
			return this.ExecuteDialogKey(keyData) || base.ProcessDialogKey(keyData);
		}
		public void ScrollToCaret()
		{
			this.motherTextAreaControl.ScrollToCaret();
		}
		public void ScrollTo(int line)
		{
			this.motherTextAreaControl.ScrollTo(line);
		}
		public void BeginUpdate()
		{
			this.motherTextEditorControl.BeginUpdate();
		}
		public void EndUpdate()
		{
			this.motherTextEditorControl.EndUpdate();
		}
		private string GenerateWhitespaceString(int length)
		{
			return new string(' ', length);
		}
		public void InsertChar(char ch)
		{
			bool isUpdating = this.motherTextEditorControl.IsUpdating;
			if (!isUpdating)
			{
				this.BeginUpdate();
			}
			if (char.IsWhiteSpace(ch) && ch != '\t' && ch != '\n')
			{
				ch = ' ';
			}
			bool flag = false;
			if (this.Document.TextEditorProperties.DocumentSelectionMode == DocumentSelectionMode.Normal && this.SelectionManager.SelectionCollection.Count > 0)
			{
				this.Caret.Position = this.SelectionManager.SelectionCollection[0].StartPosition;
				this.SelectionManager.RemoveSelectedText();
				flag = true;
			}
			LineSegment lineSegment = this.Document.GetLineSegment(this.Caret.Line);
			int offset = this.Caret.Offset;
			int num = Math.Min(this.Caret.Column, this.Caret.DesiredColumn);
			if (lineSegment.Length < num && ch != '\n')
			{
				this.Document.Insert(offset, this.GenerateWhitespaceString(num - lineSegment.Length) + ch);
			}
			else
			{
				this.Document.Insert(offset, ch.ToString());
			}
			this.Caret.Column++;
			if (flag)
			{
				this.Document.UndoStack.UndoLast(2);
			}
			if (!isUpdating)
			{
				this.EndUpdate();
				this.UpdateLineToEnd(this.Caret.Line, this.Caret.Column);
			}
		}
		public void InsertString(string str)
		{
			bool isUpdating = this.motherTextEditorControl.IsUpdating;
			if (!isUpdating)
			{
				this.BeginUpdate();
			}
			try
			{
				bool flag = false;
				if (this.Document.TextEditorProperties.DocumentSelectionMode == DocumentSelectionMode.Normal && this.SelectionManager.SelectionCollection.Count > 0)
				{
					this.Caret.Position = this.SelectionManager.SelectionCollection[0].StartPosition;
					this.SelectionManager.RemoveSelectedText();
					flag = true;
				}
				int num = this.Document.PositionToOffset(this.Caret.Position);
				int line = this.Caret.Line;
				LineSegment lineSegment = this.Document.GetLineSegment(this.Caret.Line);
				if (lineSegment.Length < this.Caret.Column)
				{
					int num2 = this.Caret.Column - lineSegment.Length;
					this.Document.Insert(num, this.GenerateWhitespaceString(num2) + str);
					this.Caret.Position = this.Document.OffsetToPosition(num + str.Length + num2);
				}
				else
				{
					this.Document.Insert(num, str);
					this.Caret.Position = this.Document.OffsetToPosition(num + str.Length);
				}
				if (flag)
				{
					this.Document.UndoStack.UndoLast(2);
				}
				if (line != this.Caret.Line)
				{
					this.UpdateToEnd(line);
				}
				else
				{
					this.UpdateLineToEnd(this.Caret.Line, this.Caret.Column);
				}
			}
			finally
			{
				if (!isUpdating)
				{
					this.EndUpdate();
				}
			}
		}
		public void ReplaceChar(char ch)
		{
			bool isUpdating = this.motherTextEditorControl.IsUpdating;
			if (!isUpdating)
			{
				this.BeginUpdate();
			}
			if (this.Document.TextEditorProperties.DocumentSelectionMode == DocumentSelectionMode.Normal && this.SelectionManager.SelectionCollection.Count > 0)
			{
				this.Caret.Position = this.SelectionManager.SelectionCollection[0].StartPosition;
				this.SelectionManager.RemoveSelectedText();
			}
			int line = this.Caret.Line;
			LineSegment lineSegment = this.Document.GetLineSegment(line);
			int num = this.Document.PositionToOffset(this.Caret.Position);
			if (num < lineSegment.Offset + lineSegment.Length)
			{
				this.Document.Replace(num, 1, ch.ToString());
			}
			else
			{
				this.Document.Insert(num, ch.ToString());
			}
			if (!isUpdating)
			{
				this.EndUpdate();
				this.UpdateLineToEnd(line, this.Caret.Column);
			}
			this.Caret.Column++;
		}
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				if (this.caret != null)
				{
					this.caret.PositionChanged -= new EventHandler(this.SearchMatchingBracket);
					this.caret.Dispose();
					this.caret = null;
				}
				if (this.selectionManager != null)
				{
					this.selectionManager.Dispose();
					this.selectionManager = null;
				}
				this.Document.TextContentChanged -= new EventHandler(this.TextContentChanged);
				this.Document.FoldingManager.FoldingsChanged -= new EventHandler(this.DocumentFoldingsChanged);
				this.motherTextAreaControl = null;
				this.motherTextEditorControl = null;
				this.gutterMargin.Dispose();
				this.textView.Dispose();
				this.toolTip.Dispose();
			}
		}
		internal void UpdateLine(int line)
		{
			this.UpdateLines(0, line, line);
		}
		internal void UpdateLines(int lineBegin, int lineEnd)
		{
			this.UpdateLines(0, lineBegin, lineEnd);
		}
		internal void UpdateToEnd(int lineBegin)
		{
			lineBegin = Math.Min(lineBegin, this.FirstPhysicalLine);
			int num = Math.Max(0, lineBegin * this.textView.FontHeight);
			num = Math.Max(0, num - this.virtualTop.Y);
			Rectangle rc = new Rectangle(0, num, base.Width, base.Height - num);
			base.Invalidate(rc);
		}
		internal void UpdateLineToEnd(int lineNr, int xStart)
		{
			this.UpdateLines(xStart, lineNr, lineNr);
		}
		internal void UpdateLine(int line, int begin, int end)
		{
			this.UpdateLines(line, line);
		}
		internal void UpdateLines(int xPos, int lineBegin, int lineEnd)
		{
			this.InvalidateLines((int)((float)xPos * this.TextView.GetWidth(' ')), lineBegin, lineEnd);
		}
		private void InvalidateLines(int xPos, int lineBegin, int lineEnd)
		{
			lineBegin = Math.Max(this.Document.GetVisibleLine(lineBegin), this.FirstPhysicalLine);
			lineEnd = Math.Min(this.Document.GetVisibleLine(lineEnd), this.FirstPhysicalLine + this.textView.VisibleLineCount);
			int num = Math.Max(0, lineBegin * this.textView.FontHeight);
			int num2 = Math.Min(this.textView.DrawingPosition.Height, (1 + lineEnd - lineBegin) * (this.textView.FontHeight + 1));
			Rectangle rc = new Rectangle(0, num - 1 - this.virtualTop.Y, base.Width, num2 + 3);
			base.Invalidate(rc);
		}
	}
}
