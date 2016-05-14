using LTP.TextEditor.Undo;
using System;
using System.Collections;
using System.Drawing;
using System.Runtime.CompilerServices;
namespace LTP.TextEditor.Document
{
	internal class DefaultDocument : IDocument
	{
		private bool readOnly;
		private ILineManager lineTrackingStrategy;
		private IBookMarkManager bookmarkManager;
		private ICustomLineManager customLineManager;
		private ITextBufferStrategy textBufferStrategy;
		private IFormattingStrategy formattingStrategy;
		private FoldingManager foldingManager;
		private UndoStack undoStack = new UndoStack();
		private ITextEditorProperties textEditorProperties = new DefaultTextEditorProperties();
		private MarkerStrategy markerStrategy;
		private ArrayList updateQueue = new ArrayList();
        public event DocumentEventHandler DocumentAboutToBeChanged;
        public event DocumentEventHandler DocumentChanged;
        public event EventHandler UpdateCommited;
        public event EventHandler TextContentChanged;
		public MarkerStrategy MarkerStrategy
		{
			get
			{
				return this.markerStrategy;
			}
			set
			{
				this.markerStrategy = value;
			}
		}
		public ITextEditorProperties TextEditorProperties
		{
			get
			{
				return this.textEditorProperties;
			}
			set
			{
				this.textEditorProperties = value;
			}
		}
		public UndoStack UndoStack
		{
			get
			{
				return this.undoStack;
			}
		}
		public ArrayList LineSegmentCollection
		{
			get
			{
				return this.lineTrackingStrategy.LineSegmentCollection;
			}
		}
		public bool ReadOnly
		{
			get
			{
				return this.readOnly;
			}
			set
			{
				this.readOnly = value;
			}
		}
		public ILineManager LineManager
		{
			get
			{
				return this.lineTrackingStrategy;
			}
			set
			{
				this.lineTrackingStrategy = value;
			}
		}
		public ITextBufferStrategy TextBufferStrategy
		{
			get
			{
				return this.textBufferStrategy;
			}
			set
			{
				this.textBufferStrategy = value;
			}
		}
		public IFormattingStrategy FormattingStrategy
		{
			get
			{
				return this.formattingStrategy;
			}
			set
			{
				this.formattingStrategy = value;
			}
		}
		public FoldingManager FoldingManager
		{
			get
			{
				return this.foldingManager;
			}
			set
			{
				this.foldingManager = value;
			}
		}
		public IHighlightingStrategy HighlightingStrategy
		{
			get
			{
				return this.lineTrackingStrategy.HighlightingStrategy;
			}
			set
			{
				this.lineTrackingStrategy.HighlightingStrategy = value;
			}
		}
		public int TextLength
		{
			get
			{
				return this.textBufferStrategy.Length;
			}
		}
		public IBookMarkManager BookmarkManager
		{
			get
			{
				return this.bookmarkManager;
			}
			set
			{
				this.bookmarkManager = value;
			}
		}
		public ICustomLineManager CustomLineManager
		{
			get
			{
				return this.customLineManager;
			}
			set
			{
				this.customLineManager = value;
			}
		}
		public string TextContent
		{
			get
			{
				return this.GetText(0, this.textBufferStrategy.Length);
			}
			set
			{
				this.OnDocumentAboutToBeChanged(new DocumentEventArgs(this, 0, 0, value));
				this.textBufferStrategy.SetContent(value);
				this.lineTrackingStrategy.SetContent(value);
				this.OnDocumentChanged(new DocumentEventArgs(this, 0, 0, value));
				this.OnTextContentChanged(EventArgs.Empty);
			}
		}
		public int TotalNumberOfLines
		{
			get
			{
				return this.lineTrackingStrategy.TotalNumberOfLines;
			}
		}
		public ArrayList UpdateQueue
		{
			get
			{
				return this.updateQueue;
			}
		}
		public void Insert(int offset, string text)
		{
			if (this.readOnly)
			{
				return;
			}
			this.OnDocumentAboutToBeChanged(new DocumentEventArgs(this, offset, -1, text));
			DateTime arg_1D_0 = DateTime.Now;
			this.textBufferStrategy.Insert(offset, text);
			DateTime arg_30_0 = DateTime.Now;
			this.lineTrackingStrategy.Insert(offset, text);
			DateTime arg_43_0 = DateTime.Now;
			this.undoStack.Push(new UndoableInsert(this, offset, text));
			DateTime arg_5C_0 = DateTime.Now;
			this.OnDocumentChanged(new DocumentEventArgs(this, offset, -1, text));
		}
		public void Remove(int offset, int length)
		{
			if (this.readOnly)
			{
				return;
			}
			this.OnDocumentAboutToBeChanged(new DocumentEventArgs(this, offset, length));
			this.undoStack.Push(new UndoableDelete(this, offset, this.GetText(offset, length)));
			this.textBufferStrategy.Remove(offset, length);
			this.lineTrackingStrategy.Remove(offset, length);
			this.OnDocumentChanged(new DocumentEventArgs(this, offset, length));
		}
		public void Replace(int offset, int length, string text)
		{
			if (this.readOnly)
			{
				return;
			}
			this.OnDocumentAboutToBeChanged(new DocumentEventArgs(this, offset, length, text));
			this.undoStack.Push(new UndoableReplace(this, offset, this.GetText(offset, length), text));
			this.textBufferStrategy.Replace(offset, length, text);
			this.lineTrackingStrategy.Replace(offset, length, text);
			this.OnDocumentChanged(new DocumentEventArgs(this, offset, length, text));
		}
		public char GetCharAt(int offset)
		{
			return this.textBufferStrategy.GetCharAt(offset);
		}
		public string GetText(int offset, int length)
		{
			return this.textBufferStrategy.GetText(offset, length);
		}
		public string GetText(ISegment segment)
		{
			return this.GetText(segment.Offset, segment.Length);
		}
		public int GetLineNumberForOffset(int offset)
		{
			return this.lineTrackingStrategy.GetLineNumberForOffset(offset);
		}
		public LineSegment GetLineSegmentForOffset(int offset)
		{
			return this.lineTrackingStrategy.GetLineSegmentForOffset(offset);
		}
		public LineSegment GetLineSegment(int line)
		{
			return this.lineTrackingStrategy.GetLineSegment(line);
		}
		public int GetFirstLogicalLine(int lineNumber)
		{
			return this.lineTrackingStrategy.GetFirstLogicalLine(lineNumber);
		}
		public int GetLastLogicalLine(int lineNumber)
		{
			return this.lineTrackingStrategy.GetLastLogicalLine(lineNumber);
		}
		public int GetVisibleLine(int lineNumber)
		{
			return this.lineTrackingStrategy.GetVisibleLine(lineNumber);
		}
		public int GetNextVisibleLineAbove(int lineNumber, int lineCount)
		{
			return this.lineTrackingStrategy.GetNextVisibleLineAbove(lineNumber, lineCount);
		}
		public int GetNextVisibleLineBelow(int lineNumber, int lineCount)
		{
			return this.lineTrackingStrategy.GetNextVisibleLineBelow(lineNumber, lineCount);
		}
		public Point OffsetToPosition(int offset)
		{
			int lineNumberForOffset = this.GetLineNumberForOffset(offset);
			LineSegment lineSegment = this.GetLineSegment(lineNumberForOffset);
			return new Point(offset - lineSegment.Offset, lineNumberForOffset);
		}
		public int PositionToOffset(Point p)
		{
			if (p.Y >= this.TotalNumberOfLines)
			{
				return 0;
			}
			LineSegment lineSegment = this.GetLineSegment(p.Y);
			return Math.Min(this.TextLength, lineSegment.Offset + Math.Min(lineSegment.Length, p.X));
		}
		public void UpdateSegmentListOnDocumentChange(ArrayList list, DocumentEventArgs e)
		{
			for (int i = 0; i < list.Count; i++)
			{
				ISegment segment = (ISegment)list[i];
				if ((e.Offset <= segment.Offset && segment.Offset <= e.Offset + e.Length) || (e.Offset <= segment.Offset + segment.Length && segment.Offset + segment.Length <= e.Offset + e.Length))
				{
					list.RemoveAt(i);
					i--;
				}
				else
				{
					if (segment.Offset <= e.Offset && e.Offset <= segment.Offset + segment.Length)
					{
						if (e.Text != null)
						{
							segment.Length += e.Text.Length;
						}
						if (e.Length > 0)
						{
							segment.Length -= e.Length;
						}
					}
					else
					{
						if (segment.Offset >= e.Offset)
						{
							if (e.Text != null)
							{
								segment.Offset += e.Text.Length;
							}
							if (e.Length > 0)
							{
								segment.Offset -= e.Length;
							}
						}
					}
				}
			}
		}
		protected void OnDocumentAboutToBeChanged(DocumentEventArgs e)
		{
			if (this.DocumentAboutToBeChanged != null)
			{
				this.DocumentAboutToBeChanged(this, e);
			}
		}
		protected void OnDocumentChanged(DocumentEventArgs e)
		{
			if (this.DocumentChanged != null)
			{
				this.DocumentChanged(this, e);
			}
		}
		public void RequestUpdate(TextAreaUpdate update)
		{
			this.updateQueue.Add(update);
		}
		public void CommitUpdate()
		{
			if (this.UpdateCommited != null)
			{
				this.UpdateCommited(this, EventArgs.Empty);
			}
		}
		protected virtual void OnTextContentChanged(EventArgs e)
		{
			if (this.TextContentChanged != null)
			{
				this.TextContentChanged(this, e);
			}
		}
	}
}
