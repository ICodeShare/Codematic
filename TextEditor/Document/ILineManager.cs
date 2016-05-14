using System;
using System.Collections;
namespace LTP.TextEditor.Document
{
	public interface ILineManager
	{
		event LineManagerEventHandler LineCountChanged;
		event LineLengthEventHandler LineLengthChanged;
		ArrayList LineSegmentCollection
		{
			get;
		}
		int TotalNumberOfLines
		{
			get;
		}
		IHighlightingStrategy HighlightingStrategy
		{
			get;
			set;
		}
		int GetLineNumberForOffset(int offset);
		LineSegment GetLineSegmentForOffset(int offset);
		LineSegment GetLineSegment(int lineNumber);
		void Insert(int offset, string text);
		void Remove(int offset, int length);
		void Replace(int offset, int length, string text);
		void SetContent(string text);
		int GetFirstLogicalLine(int lineNumber);
		int GetLastLogicalLine(int lineNumber);
		int GetVisibleLine(int lineNumber);
		int GetNextVisibleLineAbove(int lineNumber, int lineCount);
		int GetNextVisibleLineBelow(int lineNumber, int lineCount);
	}
}
