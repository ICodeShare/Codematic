using LTP.TextEditor.Undo;
using System;
using System.Collections;
using System.Drawing;
namespace LTP.TextEditor.Document
{
	public interface IDocument
	{
		event EventHandler UpdateCommited;
		event DocumentEventHandler DocumentAboutToBeChanged;
		event DocumentEventHandler DocumentChanged;
		event EventHandler TextContentChanged;
		ITextEditorProperties TextEditorProperties
		{
			get;
			set;
		}
		UndoStack UndoStack
		{
			get;
		}
		bool ReadOnly
		{
			get;
			set;
		}
		IFormattingStrategy FormattingStrategy
		{
			get;
			set;
		}
		ITextBufferStrategy TextBufferStrategy
		{
			get;
		}
		FoldingManager FoldingManager
		{
			get;
		}
		IHighlightingStrategy HighlightingStrategy
		{
			get;
			set;
		}
		IBookMarkManager BookmarkManager
		{
			get;
		}
		ICustomLineManager CustomLineManager
		{
			get;
		}
		MarkerStrategy MarkerStrategy
		{
			get;
		}
		ArrayList LineSegmentCollection
		{
			get;
		}
		int TotalNumberOfLines
		{
			get;
		}
		string TextContent
		{
			get;
			set;
		}
		int TextLength
		{
			get;
		}
		ArrayList UpdateQueue
		{
			get;
		}
		int GetLineNumberForOffset(int offset);
		LineSegment GetLineSegmentForOffset(int offset);
		LineSegment GetLineSegment(int lineNumber);
		int GetFirstLogicalLine(int lineNumber);
		int GetLastLogicalLine(int lineNumber);
		int GetVisibleLine(int lineNumber);
		int GetNextVisibleLineAbove(int lineNumber, int lineCount);
		int GetNextVisibleLineBelow(int lineNumber, int lineCount);
		void Insert(int offset, string text);
		void Remove(int offset, int length);
		void Replace(int offset, int length, string text);
		char GetCharAt(int offset);
		string GetText(int offset, int length);
		string GetText(ISegment segment);
		Point OffsetToPosition(int offset);
		int PositionToOffset(Point p);
		void RequestUpdate(TextAreaUpdate update);
		void CommitUpdate();
		void UpdateSegmentListOnDocumentChange(ArrayList list, DocumentEventArgs e);
	}
}
